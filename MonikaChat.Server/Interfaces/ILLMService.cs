using MonikaChat.Shared.Models.AIInteractions;

namespace MonikaChat.Server.Interfaces
{
    public interface ILLMService
	{
		Task<AIChatInteraction> SendMessage(AIChatInteraction input, string promptTemplate = "");

		Task<double[]> GetEmbedding(string text, string apiKey);

		Task<bool> TestKey(string apiKey);
	}
}
