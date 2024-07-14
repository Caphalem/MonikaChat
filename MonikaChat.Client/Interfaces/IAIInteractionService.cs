using MonikaChat.Shared.Models.AIInteractions;

namespace MonikaChat.Client.Interfaces
{
    public interface IAIInteractionService
    {
        Task<AIChatInteraction> SendMessage(AIChatInteraction chatStatus);

        Task<AIChatInteraction> AnalyzeMemoriesAndRespond(AIRememberInteraction memories);

		Task<double[]> GetEmbedding(string text, string apiKey);

        Task<string> GetLastConversationSummary(AILastConversationInteraction lastConversation);

		Task<bool> TestKey(string apiKey);
    }
}
