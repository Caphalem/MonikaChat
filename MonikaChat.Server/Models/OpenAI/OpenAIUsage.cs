using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIUsage
	{
        [JsonProperty("prompt_tokens")]
        public short PromptTokens { get; set; }

		[JsonProperty("completion_tokens")]
		public short CompletionTokens { get; set; }

		[JsonProperty("total_tokens")]
		public short TotalTokens { get; set; }
	}
}
