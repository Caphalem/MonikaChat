using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
    public class AIChatInteraction : AIInteractionBase
    {
        [JsonProperty("history")]
        public IEnumerable<AIChatMessage> History { get; set; } = new List<AIChatMessage>();

        [JsonProperty("currentMessage")]
        public AIChatMessage CurrentMessage { get; set; } = new AIChatMessage();

        [JsonProperty("spriteClickEvent")]
        public bool IsSpriteClickEvent = false;

        [JsonProperty("currentDateTime")]
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;

        [JsonProperty("lastConversationSummary")]
        public string LastConversationSummary { get; set; } = string.Empty;
    }
}
