using Microsoft.AspNetCore.Mvc;
using MonikaChat.Server.Interfaces;
using MonikaChat.Server.Services;
using MonikaChat.Shared.Models;
using MonikaChat.Shared.Models.AIInteractions;
using Newtonsoft.Json;

namespace MonikaChat.Server.Controllers
{
    [ApiController]
	[Route("monika")]
	public class MonikaController : ControllerBase
	{
        private readonly MonikaService _monikaService;
		private readonly ILLMService _llmService;
		private readonly CryptographyService _cryptographyService;

		public MonikaController(MonikaService monikaService, ILLMService llmService, CryptographyService cryptographyService)
        {
			_monikaService = monikaService;
			_llmService = llmService;
			_cryptographyService = cryptographyService;
		}

		[HttpPost("interact")]
		public async Task<IActionResult> Interact([FromBody] HybridEncryptedDTO input)
		{
			// Using only one endpoint to make it slightly more confusing to a third party to understand what's going on

			// Convert Base64 strings to byte arrays
			byte[] encryptedDataBytes = Convert.FromBase64String(input.Data);
			byte[] encryptedTypeBytes = Convert.FromBase64String(input.Type);
			byte[] iv = Convert.FromBase64String(input.Iv);
			byte[] encryptedAesKey = Convert.FromBase64String(input.AesKey);

			// Decrypt the AES key using RSA
			byte[] aesKey = _cryptographyService.DecryptWithPrivateKey(encryptedAesKey);

			HybridEncryptedDTO result;

			try
			{
				string type = _cryptographyService.DecryptAESGCMData(encryptedTypeBytes, iv, aesKey);
				string data = _cryptographyService.DecryptAESGCMData(encryptedDataBytes, iv, aesKey);

				switch (type)
				{
					// Handle a chat type interaction
					case AIInteractionTypes.MESSAGE:
						AIChatInteraction chatInteraction = DeserializeInteraction<AIChatInteraction>(data);
						AIChatInteraction chatInteractionResult = await _monikaService.SendMessage(chatInteraction);
						result = BuildResult(chatInteractionResult, aesKey, input);

						break;
					// Handle an embedding type interaction
					case AIInteractionTypes.EMBEDDING:
						AIEmbeddingInteraction embeddingInteraction = DeserializeInteraction<AIEmbeddingInteraction>(data);
						double[] embedding = await _llmService.GetEmbedding(embeddingInteraction.Text, embeddingInteraction.APIKey);
						result = BuildResult(embedding, aesKey, input);

						break;
					// Handle a memory type interaction
					case AIInteractionTypes.REMEMBER:
						AIRememberInteraction rememberInteraction = DeserializeInteraction<AIRememberInteraction>(data);
						AIChatInteraction response = await _monikaService.Remember(rememberInteraction);
						result = BuildResult(response, aesKey, input);

						break;
					// Handle a last conversation summary interaction
					case AIInteractionTypes.LAST_CONVERSATION:
						AILastConversationInteraction lastConversationInteraction = DeserializeInteraction<AILastConversationInteraction>(data);
						string lastConversationSummary = await _monikaService.RememberLastConversation(lastConversationInteraction);
						result = BuildResult(lastConversationSummary, aesKey, input);

						break;
					// Handle API Key validity
					case AIInteractionTypes.TEST:
						AITestInteraction aITestInteraction = DeserializeInteraction<AITestInteraction>(data);
						bool testResult = await _llmService.TestKey(aITestInteraction.APIKey);
						result = BuildResult(testResult, aesKey, input);

						break;
					default:
						throw new ArgumentNullException("Unknown interaction type.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(result);
		}

		private T DeserializeInteraction<T>(string data)
		{
			T? interaction = JsonConvert.DeserializeObject<T>(data);

			if (interaction == null)
			{
				throw new ArgumentNullException("Failed to decrypt input.");
			}

			return interaction;
		}

		private HybridEncryptedDTO BuildResult(object data, byte[] aesKey, HybridEncryptedDTO input)
		{
			// Encrypt resulting data using the same AES Key with a new IV
			// The Client knows the AES key due to it generating it during this transaction
			// while the Server knows the AES key because it can decrypt it using the private RSA key

			(byte[] encryptedResultDataBytes, byte[] resultIv) = _cryptographyService.EncryptAESGCMData(JsonConvert.SerializeObject(data), aesKey);

			HybridEncryptedDTO result = new HybridEncryptedDTO
			{
				AesKey = input.AesKey,
				Iv = Convert.ToBase64String(resultIv),
				Type = input.Type,
				Data = Convert.ToBase64String(encryptedResultDataBytes)
			};

			return result;
		}
	}
}
