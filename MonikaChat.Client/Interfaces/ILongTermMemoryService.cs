using MonikaChat.Shared.Models;
using MonikaChat.Shared.Models.AIInteractions;

namespace MonikaChat.Client.Interfaces
{
    public interface ILongTermMemoryService
	{
		Task<ConversationMemory?> LoadConversation(string sessionId);

		Task<List<ConversationMemory>> LoadAllConversations();

		Task SaveConversation(string sessionId, AIChatInteraction chatStatus);

		string CheckIfRememberCommand(string message);

		Task<(AIChatInteraction, List<ConversationMemory>)> Remember(string question, AIChatInteraction chatStatus, List<ConversationMemory> loadedConversationList);

		Task<(string, List<ConversationMemory>)> GetLatestConversationMemory(string username, string apiKey, List<ConversationMemory> loadedConversationList);

		Task ClearMemory();

		Task EnsureDb();
	}
}
