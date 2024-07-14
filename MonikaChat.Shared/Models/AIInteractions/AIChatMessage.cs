using Newtonsoft.Json;

namespace MonikaChat.Shared.Models.AIInteractions
{
    public class AIChatMessage
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
