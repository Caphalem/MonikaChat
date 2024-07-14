using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIEmbedding
	{
        [JsonProperty("object")]
        public string Object { get; set; } = string.Empty;

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("embedding")]
        public double[] Embedding { get; set; } = [];
    }
}
