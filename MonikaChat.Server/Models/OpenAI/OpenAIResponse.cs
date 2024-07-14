using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIResponse
	{
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("object")]
        public string Object { get; set; } = string.Empty;

        [JsonProperty("created")]
		public int Created { get; set; }

        [JsonProperty("model")]
		public string Model { get; set; } = string.Empty;

        [JsonProperty("system_fingerprint")]
		public string SystemFingerprint { get; set; } = string.Empty;

        [JsonProperty("choices")]
		public OpenAIChoice[] Choices { get; set; } = [];

        [JsonProperty("usage")]
		public OpenAIUsage Usage { get; set; } = new OpenAIUsage();
	}
}
