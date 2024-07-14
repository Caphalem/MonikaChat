using MonikaChat.Shared.Interfaces;

namespace MonikaChat.Shared.Models
{
    public class ConversationMemory : ConversationMemoryEssentials, IVectorObject
	{
		public string SessionId { get; set; } = Guid.NewGuid().ToString();

		public double[] Embedding { get; set; } = [];

		public double[] GetVector() => Embedding;
	}
}
