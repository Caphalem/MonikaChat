using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIRequest
	{
		[JsonProperty("model")]
        public string Model { get; set; } = string.Empty;

        [JsonProperty("messages")]
		public OpenAIMessage[] Messages { get; set; } = [];
	}
}
