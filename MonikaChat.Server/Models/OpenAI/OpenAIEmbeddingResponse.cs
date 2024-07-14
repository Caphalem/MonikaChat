using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIEmbeddingResponse
	{
        [JsonProperty("object")]
        public string Object { get; set; } = string.Empty;

        [JsonProperty("data")]
        public OpenAIEmbedding[] Data { get; set; } = [];

        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        [JsonProperty("usage")]
        public OpenAIUsage Usage { get; set; } = new OpenAIUsage();
    }
}
