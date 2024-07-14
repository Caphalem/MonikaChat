using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
    public class AIEmbeddingInteraction : AIInteractionBase
    {
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;
    }
}
