using MonikaChat.Client.Interfaces;
using MonikaChat.Shared.Helpers;
using MonikaChat.Shared.Models;
using MonikaChat.Shared.Models.AIInteractions;
using System.Globalization;
using System.Text.RegularExpressions;
using TG.Blazor.IndexedDB;

namespace MonikaChat.Client.Services
{
    public class IndexedDbLongTermAIMemoryService : ILongTermMemoryService
	{
		public static string DbName = "MonikaDB";
		public static int DbVersion = 1;
		public static string StoreName = "Conversations";
		public static StoreSchema StoreSchema = new StoreSchema
		{
			Name = StoreName,
			PrimaryKey = new IndexSpec { Name = "sessionId", KeyPath = "sessionId", Auto = false, Unique = true },
			Indexes = new List<IndexSpec>
			{
				new IndexSpec { Name = "timestamp", KeyPath = "timestamp", Auto = false, Unique = false },
				new IndexSpec { Name = "history", KeyPath = "history", Auto = false, Unique = false }
			}
		};

		private const int RECALL_CONVERSATION_COUNT = 3; // When remembering, how many past conversations to analyze that might have the answer
		private const string DATE_TIME_TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss";

		private Regex _remembranceRegex = new Regex(@"\{\{Remember: (.*?)\}\}", RegexOptions.IgnoreCase);

		private readonly IndexedDBManager _indexedDbManager;
		private readonly IAIInteractionService _aiInteractionService;

		public IndexedDbLongTermAIMemoryService(IndexedDBManager indexedDbManager, IAIInteractionService aiInteractionService)
		{
			_indexedDbManager = indexedDbManager;
			_aiInteractionService = aiInteractionService;
		}

		public async Task<ConversationMemory?> LoadConversation(string sessionId) =>
			await _indexedDbManager.GetRecordById<string, ConversationMemory>((await GetStore()).Name, sessionId);

		public async Task SaveConversation(string sessionId, AIChatInteraction chatStatus)
		{
			string history = string.Join(" \n", chatStatus.History.Where(m => m.Role != OpenAIRoleNames.SYSTEM_ROLE).Select(m => $"{m.Name}: {m.Message}"));
			double[] historyEmbedding = await _aiInteractionService.GetEmbedding(history, chatStatus.APIKey);

            ConversationMemory snippet = new ConversationMemory
			{
				SessionId = sessionId,
				History = history,
				Embedding = historyEmbedding
			};

			StoreRecord<ConversationMemory> storeRecord = new StoreRecord<ConversationMemory>
			{
				Storename = _indexedDbManager.Stores[0].Name,
				Data = snippet
			};

			string storeName = (await GetStore()).Name;
			ConversationMemory conversation = await _indexedDbManager.GetRecordById<string, ConversationMemory>(storeName, sessionId);

			if (conversation != null)
			{
				// Updating an existing history
				conversation.History = snippet.History;
				storeRecord.Data = conversation;
				await _indexedDbManager.UpdateRecord<ConversationMemory>(storeRecord);
			}
			else
			{
				// Adding a new entry and setting the timestamp.
				// Using non-UTC DateTime because it's more useful to Monika when talking to the user and referring to the time.
				storeRecord.Data.Timestamp = DateTime.Now.ToString(DATE_TIME_TIMESTAMP_FORMAT);
				await _indexedDbManager.AddRecord<ConversationMemory>(storeRecord);
			}
		}

		public string CheckIfRememberCommand(string message)
		{
			Match match = _remembranceRegex.Match(message);

			if (match.Success && !string.IsNullOrEmpty(match.Groups[1].Value))
			{
				return match.Groups[1].Value;
			}

			return string.Empty;
		}

		public async Task<(AIChatInteraction, List<ConversationMemory>)> Remember(string question, AIChatInteraction chatStatus, List<ConversationMemory> loadedConversationList)
		{
			double[] memoryQueryEmbedding;

			if (loadedConversationList.Count == 0)
			{
				// If the local storage conversation memories are not loaded, load them asynchronously alongside the message embedding
				// we don't care about having the latest current memories as it's already in the current context (history)
				Task<double[]> memoryQueryEmbeddingTask = _aiInteractionService.GetEmbedding(question, chatStatus.APIKey);
				Task<List<ConversationMemory>> allConversationsTask = _indexedDbManager.GetRecords<ConversationMemory>((await GetStore()).Name);

				await Task.WhenAll(memoryQueryEmbeddingTask, allConversationsTask);

				memoryQueryEmbedding = memoryQueryEmbeddingTask.Result;
				loadedConversationList = allConversationsTask.Result;
			}
			else
			{
				// Otherwise, just get the embedding
				memoryQueryEmbedding = await _aiInteractionService.GetEmbedding(question, chatStatus.APIKey);
			}

			IEnumerable<ConversationMemory> rememberedConversations = VectorMathHelper.FindNearestConversations(memoryQueryEmbedding, loadedConversationList, RECALL_CONVERSATION_COUNT);
			List<ConversationMemoryEssentials> conversationMemoryList = rememberedConversations
				.Select(c => new ConversationMemoryEssentials
				{
					Timestamp = c.Timestamp,
					History = c.History
				})
				.ToList();

			AIRememberInteraction requestBody = new AIRememberInteraction
			{
				ChatStatus = chatStatus,
				Question = question,
				Conversations = conversationMemoryList
			};

			AIChatInteraction result = await _aiInteractionService.AnalyzeMemoriesAndRespond(requestBody);

            return (result, loadedConversationList);
		}

		public async Task<(string, List<ConversationMemory>)> GetLatestConversationMemory(string username, string apiKey, List<ConversationMemory> loadedConversationList)
		{
			string result = $"No summary. This Is the Very First conversation that you are having with {username}.";
			// In case the conversations haven't been loaded yet
			if (loadedConversationList.Count == 0)
			{
				loadedConversationList = await _indexedDbManager.GetRecords<ConversationMemory>((await GetStore()).Name);
			}

			Dictionary<DateTime, ConversationMemory> conversationMemoryDictionary = new Dictionary<DateTime, ConversationMemory>();
			ConversationMemory latestConversationMemory;
			foreach (ConversationMemory conversationMemory in loadedConversationList)
			{
				if (DateTime.TryParseExact(conversationMemory.Timestamp, DATE_TIME_TIMESTAMP_FORMAT, null, DateTimeStyles.None, out DateTime dateTime))
				{
					conversationMemoryDictionary.Add(dateTime, conversationMemory);
				}
			}

			latestConversationMemory = conversationMemoryDictionary.OrderByDescending(c => c.Key).FirstOrDefault().Value;

			// If the latest memory was not found then 'latestConversationMemory' will not be null but rather default so need to check the sessionId
			if (string.IsNullOrWhiteSpace(latestConversationMemory?.SessionId))
			{
				return (result, loadedConversationList);
			}

			AILastConversationInteraction requestBody = new AILastConversationInteraction
			{
				APIKey = apiKey,
				LastConversation = latestConversationMemory,
				Username = username
			};

			result = await _aiInteractionService.GetLastConversationSummary(requestBody);

			return (result, loadedConversationList);
		}

		public async Task ClearMemory() =>
			await _indexedDbManager.ClearStore((await GetStore()).Name);

		public async Task EnsureDb()
		{
			StoreSchema? store = _indexedDbManager.Stores.FirstOrDefault(s => string.Equals(s?.Name, StoreName));

			if (store == null)
			{
				await _indexedDbManager.AddNewStore(StoreSchema);
				store = _indexedDbManager.Stores.FirstOrDefault(s => string.Equals(s?.Name, StoreName));
			}
		}

		private async Task<StoreSchema> GetStore()
		{
			await EnsureDb();

			StoreSchema store = _indexedDbManager.Stores.First(s => string.Equals(s.Name, StoreName));

			return store;
		}
	}
}
