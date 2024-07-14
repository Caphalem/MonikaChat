using Newtonsoft.Json;

namespace MonikaChat.Shared.Models
{
    public class HybridEncryptedDTO
    {
        [JsonProperty("iv")]
		public string Iv { get; set; } = string.Empty;

        [JsonProperty("aesKey")]
        public string AesKey { get; set; } = string.Empty;

        [JsonProperty("type")]
		public string Type { get; set; } = string.Empty;

        [JsonProperty("data")]
		public string Data { get; set; } = string.Empty;
	}
}
