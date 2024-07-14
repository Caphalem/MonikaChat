using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIEmbeddingRequest
	{
        [JsonProperty("input")]
        public string Input { get; set; } = string.Empty;

        [JsonProperty("model")]
        public string Model { get; set; } = string.Empty;
    }
}
