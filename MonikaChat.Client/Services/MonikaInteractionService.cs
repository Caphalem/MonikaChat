using Microsoft.AspNetCore.Components;
using MonikaChat.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using MonikaChat.Client.Interfaces;
using MonikaChat.Shared.Models.AIInteractions;

namespace MonikaChat.Client.Services
{
    public class MonikaInteractionService : IAIInteractionService
    {
		private const string PUBLIC_KEY_PEM = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxrPUX8CRAnBahV+uC9zs
7c7HQBbYCJG4xKVFhmiheqDXAT62dLeo8V0C3KzmIjQNOAwrOYwKs3P2vl/Hg9i8
I226i//JSks0LI3zRqTjYcxW5lwWrpHoKgkjt1qfoQ6GV/Hgyl9n9weUhEmbgQER
lcaxy1yDZ4UvzYwYvBwhRgXFOKJu54438p5OBqXVEZc40cjhL4eyM6XA29Abgv1K
+F+j9UD4sD6MmSpRRQf0TZfKqS+HF3RYfDn/UaVnaoLOn/AqJYW8X/FW51VvhnbN
+BWeiB5sZeaFPT7PrwNK3dJzHKFlEZjnyjAYZtPFGMewHMu7Vol3n8v8FqfFecUR
jQIDAQAB
-----END PUBLIC KEY-----";
		private const string INTERACT_ENDPOINT = "monika/interact";

        private readonly NavigationManager _navigationManager;
		private readonly HttpClient _httpClient;
		private readonly CryptographyService _cryptographyService;

        public MonikaInteractionService(NavigationManager navigationManager, HttpClient httpClient, CryptographyService cryptographyService)
		{
			_navigationManager = navigationManager;
			_httpClient = httpClient;
			_cryptographyService = cryptographyService;
        }

		public async Task<AIChatInteraction> SendMessage(AIChatInteraction chatStatus)
		{
            AIChatInteraction? chatInteractionResponse = await SendInteraction<AIChatInteraction>(chatStatus, AIInteractionTypes.MESSAGE);

			if (chatInteractionResponse == null)
			{
				throw new HttpRequestException("The message interaction response is null!");
			}

			return chatInteractionResponse;
        }

		public async Task<AIChatInteraction> AnalyzeMemoriesAndRespond(AIRememberInteraction memories)
		{
            AIChatInteraction? chatInteractionResponse = await SendInteraction<AIChatInteraction>(memories, AIInteractionTypes.REMEMBER);

            if (chatInteractionResponse == null)
            {
                throw new HttpRequestException("The remembrance interaction response is null!");
            }

            return chatInteractionResponse;
        }

		public async Task<string> GetLastConversationSummary(AILastConversationInteraction lastConversation)
		{
			string? lastConversationSummary = await SendInteraction<string>(lastConversation, AIInteractionTypes.LAST_CONVERSATION);

			if (string.IsNullOrWhiteSpace(lastConversationSummary))
			{
				throw new HttpRequestException("The last conversation summary response is null!");
			}

			return lastConversationSummary;
		}

		public async Task<double[]> GetEmbedding(string text, string apiKey)
		{
			AIEmbeddingInteraction requestBody = new AIEmbeddingInteraction
			{
				Text = text,
				APIKey = apiKey
			};

			double[]? responseEmbedding = await SendInteraction<double[]>(requestBody, AIInteractionTypes.EMBEDDING);

			return responseEmbedding ?? [];
		}

		public async Task<bool> TestKey(string apiKey)
		{
			AITestInteraction test = new AITestInteraction
			{
				APIKey = apiKey
			};
			bool testResult = await SendInteraction<bool>(test, AIInteractionTypes.TEST);

			return testResult;
		}

		private async Task<T?> SendInteraction<T>(object data, string type)
		{
			string url = new Uri(new Uri(_navigationManager.BaseUri), INTERACT_ENDPOINT).ToString();
			string serializedData = JsonConvert.SerializeObject(data);

			string base64AesKey = await _cryptographyService.GenerateAndExportAesKey();
			HybridEncryptedDTO encryptedData = await _cryptographyService.EncryptWithHybridApproachAsync(PUBLIC_KEY_PEM, serializedData, type, base64AesKey);

			var httpResponse = await _httpClient.PostAsJsonAsync(url, encryptedData);

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Oh gosh... Something went wrong with the http request. The response code is not a successful one! It says: {httpResponse.StatusCode}");
			}

			HybridEncryptedDTO? encryptedResponse = await httpResponse.Content.ReadFromJsonAsync<HybridEncryptedDTO>();

			if (encryptedResponse == null)
			{
                throw new HttpRequestException("Oh jeez... Something went wrong with the http request. The response object from the server could not be deserialized and is null!");
			}
			else
			{
				string serializedResponse = await _cryptographyService.DecryptWithAesKeyAsync(encryptedResponse.Data, base64AesKey, encryptedResponse.Iv);
				T? responseData = JsonConvert.DeserializeObject<T>(serializedResponse);

				if (responseData == null)
				{
                    throw new HttpRequestException("Oh jeez... Something went wrong with the http request. The response data object from the server could not be deserialized and is null!");
				}
				else
				{
					return responseData;
				}
			}
		}
	}
}
