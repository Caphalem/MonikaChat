using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
    public class AIRememberInteraction
    {
        [JsonProperty("chatStatus")]
        public AIChatInteraction ChatStatus { get; set; } = new AIChatInteraction();

        [JsonProperty("conversations")]
        public IEnumerable<ConversationMemoryEssentials> Conversations { get; set; } = new List<ConversationMemoryEssentials>();

        [JsonProperty("question")]
        public string Question { get; set; } = string.Empty;
    }
}
