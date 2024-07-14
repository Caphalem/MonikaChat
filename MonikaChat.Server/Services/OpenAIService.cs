using Microsoft.Extensions.Options;
using MonikaChat.Server.Interfaces;
using MonikaChat.Server.Models.OpenAI;
using MonikaChat.Shared.Models;
using MonikaChat.Shared.Models.AIInteractions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MonikaChat.Server.Services
{
    public class OpenAIService : ILLMService
	{
		private const int MODEL_CONTEXT_SIZE_SLACK = 5; // Leave 5% of model's maximum context window size as slack for it to handle the interaction

		private readonly HttpClient _httpClient;
		private readonly OpenAIOptions _options;

		public OpenAIService(HttpClient httpClient, IOptions<OpenAIOptions> options)
		{
			_options = options.Value;
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(_options.BaseURL);
		}

		public async Task<AIChatInteraction> SendMessage(AIChatInteraction input, string promptTemplate = "")
		{
			if (string.IsNullOrWhiteSpace(input.APIKey))
			{
				throw new ArgumentException("No API key provided.");
			}

			input.CurrentMessage.Role = OpenAIRoleNames.USER_ROLE;

			// Build user message based on whether a prompt tamplate is used or not
			string userMessage = string.IsNullOrWhiteSpace(promptTemplate) ? input.CurrentMessage.Message : $"{promptTemplate}{input.CurrentMessage.Name}: {input.CurrentMessage.Message}";
			AIChatMessage currentUserChatMessage = new AIChatMessage
			{
				Name = input.CurrentMessage.Name,
				Role = OpenAIRoleNames.USER_ROLE,
				Message = userMessage
			};

			input.History = AdjustHistory(input.History, currentUserChatMessage);

			OpenAIRequest requestBody = new OpenAIRequest
			{
				Model = _options.Model,
				Messages = input.History.Select(ToOpenAIMessage).Append(ToOpenAIMessage(currentUserChatMessage)).ToArray()
			};

			// When making multiple requests with an HttpClient in a single instance, you can't modify the HttpClient's properties after the first request.
			// Luckily, we don't need to but we do need to check whether the API key is set (subsequent request) or not (first request).
			if (_httpClient.DefaultRequestHeaders.Authorization == null)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", input.APIKey);
			};

			using StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
#if DEBUG
			Console.WriteLine($"Sending... {JsonConvert.SerializeObject(requestBody)}");
#endif
			using HttpResponseMessage response = await _httpClient.PostAsync(_options.CompletionsEndpoint, jsonContent);
			response.EnsureSuccessStatusCode();

			string jsonResponseString = await response.Content.ReadAsStringAsync();
#if DEBUG
			Console.WriteLine($"Receiving... {jsonResponseString}");
#endif
			OpenAIResponse? openAIResponse = JsonConvert.DeserializeObject<OpenAIResponse>(jsonResponseString);

			if (openAIResponse == null)
			{
				throw new HttpRequestException($"Response code 200 but something went wrong deserializing Open AI Response. Response: {jsonResponseString}");
			}

			if (openAIResponse.Choices.Length == 0)
			{
				throw new HttpRequestException($"Response code 200 but there is no response message from AI model.");
			}

			AIChatMessage aiResponseMessage = ToChatMessage(openAIResponse.Choices[0].Message);
			AIChatInteraction result = new AIChatInteraction
			{
				History = input.History.Append(input.CurrentMessage).Append(aiResponseMessage),
				LastConversationSummary = input.LastConversationSummary,
				CurrentMessage = aiResponseMessage
			};

			return result;
		}

		public async Task<double[]> GetEmbedding(string text, string apiKey)
		{
			if (string.IsNullOrWhiteSpace(apiKey))
			{
				throw new ArgumentException("No API key provided.");
			}

			OpenAIEmbeddingRequest requestBody = new OpenAIEmbeddingRequest
			{
				Model = _options.EmbeddingModel,
				Input = text
			};

			// When making multiple requests with an HttpClient in a single instance, you can't modify the HttpClient's properties after the first request.
			// Luckily, we don't need to but we do need to check whether the API key is set (subsequent request) or not (first request).
			if (_httpClient.DefaultRequestHeaders.Authorization == null)
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
			};

			using StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
			using HttpResponseMessage response = await _httpClient.PostAsync(_options.EmbeddingsEndpoint, jsonContent);
			response.EnsureSuccessStatusCode();

			string jsonResponseString = await response.Content.ReadAsStringAsync();

			OpenAIEmbeddingResponse? openAIResponse = JsonConvert.DeserializeObject<OpenAIEmbeddingResponse>(jsonResponseString);

			if (openAIResponse == null)
			{
				throw new HttpRequestException($"Response code 200 but something went wrong deserializing Open AI Embedding Response. Response: {jsonResponseString}");
			}

			if (openAIResponse.Data.Length == 0)
			{
				throw new HttpRequestException($"Response code 200 but there is no response data from Embedding AI model.");
			}

			return openAIResponse.Data[0].Embedding;
		}

		public async Task<bool> TestKey(string apiKey)
		{
			try
			{
				AIChatInteraction testInteraction = new AIChatInteraction
				{
					APIKey = apiKey,
					History = new List<AIChatMessage>
					{
						new AIChatMessage
						{
							Role = OpenAIRoleNames.SYSTEM_ROLE,
							Message = "You can only respond with 'OK'."
						}
					},
					CurrentMessage = new AIChatMessage
					{
						Role = OpenAIRoleNames.USER_ROLE,
						Message = "Testing"
					}
				};

				await SendMessage(testInteraction);

				return true;
			}
			catch (Exception ex)
			{
				throw new HttpRequestException($"Invalid key. {ex.Message}");
			}
		}

		private IEnumerable<AIChatMessage> AdjustHistory(IEnumerable<AIChatMessage> currentHistory, AIChatMessage currentMessage)
		{
			// Another way of doing this is summarizing the whole history and adding the summary as a single message into the history
			// but, with context windows this massive, by the time the user fills it up, the first message will likely be irrelevant
			// and this way preserves more details of the conversation.

			int currentInteractionTokens = CountAllInteractionTokens(currentHistory, currentMessage);
			List<AIChatMessage> history = currentHistory.ToList();
			int maxAllowedTokensInContext = CalculateModelContextSlack();

			// Trimming the history so the model doesn't explode if it's too big for it to handle
			while (currentInteractionTokens > maxAllowedTokensInContext)
			{
				AIChatMessage? oldestUserMessage = currentHistory.FirstOrDefault(m => m.Role == OpenAIRoleNames.USER_ROLE);
				AIChatMessage? oldestAIMessage = currentHistory.FirstOrDefault(m => m.Role == OpenAIRoleNames.AI_ROLE);

				if (oldestUserMessage == null && oldestAIMessage == null)
				{
					break;
				}

				if (oldestUserMessage != null)
				{
					history.Remove(oldestUserMessage);
				}

				if (oldestAIMessage != null)
				{
					history.Remove(oldestAIMessage);
				}

				currentInteractionTokens = CountAllInteractionTokens(history, currentMessage);
			}

			return history;
		}

		private int CountAllInteractionTokens(IEnumerable<AIChatMessage> currentHistory, AIChatMessage currentMessage)
		{
			Tiktoken.Encoder tokenEncoder = new Tiktoken.Encoder(new Tiktoken.Encodings.O200KBase());

			int allCurrentTokens = tokenEncoder.CountTokens(currentMessage.Message);
			foreach (AIChatMessage chatMessage in currentHistory)
			{
				allCurrentTokens += tokenEncoder.CountTokens(chatMessage.Message);
			}

			return allCurrentTokens;
		}

		private int CalculateModelContextSlack()
		{
			int modelContextSlack = _options.ModelContextSize * (100 - MODEL_CONTEXT_SIZE_SLACK) / 100;

			return modelContextSlack;
		}

		private OpenAIMessage ToOpenAIMessage(AIChatMessage message) =>
			new OpenAIMessage { Role = message.Role, Name = message.Name, Content = message.Message };

		private AIChatMessage ToChatMessage(OpenAIMessage message) =>
			new AIChatMessage { Role = message.Role, Name = message.Name, Message = message.Content };
	}
}
