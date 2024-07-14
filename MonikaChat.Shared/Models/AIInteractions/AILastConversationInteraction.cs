using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
	public class AILastConversationInteraction : AIInteractionBase
	{
        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("lastConversation")]
        public ConversationMemory LastConversation { get; set; } = new ConversationMemory();
    }
}
