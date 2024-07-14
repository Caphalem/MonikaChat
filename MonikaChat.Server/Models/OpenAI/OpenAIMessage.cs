using Newtonsoft.Json;

namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIMessage
	{
		[JsonProperty("role")]
		public string Role { get; set; } = string.Empty;

		[JsonProperty("name")]
		public string? Name { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; } = string.Empty;
	}
}
