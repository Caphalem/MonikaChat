using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
    public abstract class AIInteractionBase
    {
        [JsonProperty("apiKey")]
        public string APIKey { get; set; } = string.Empty;
    }
}
