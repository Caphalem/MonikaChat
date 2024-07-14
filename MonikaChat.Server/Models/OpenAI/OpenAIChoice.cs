using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIChoice
	{
        [JsonProperty("index")]
        public short Index { get; set; }

        [JsonProperty("message")]
        public OpenAIMessage Message { get; set; } = new OpenAIMessage();

        [JsonProperty("finish_reason")]
		public string FinishReason { get; set; } = string.Empty;
	}
}
