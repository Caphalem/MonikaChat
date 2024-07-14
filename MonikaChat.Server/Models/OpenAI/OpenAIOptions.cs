namespace MonikaChat.Server.Models.OpenAI
{
	public class OpenAIOptions
	{
		public const string Name = "OpenAI";

        public string Model { get; set; } = string.Empty;

		public int ModelContextSize { get; set; }

		public string EmbeddingModel { get; set; } = string.Empty;

        public string BaseURL { get; set; } = string.Empty;

		public string CompletionsEndpoint { get; set; } = string.Empty;

        public string EmbeddingsEndpoint { get; set; } = string.Empty;
    }
}
