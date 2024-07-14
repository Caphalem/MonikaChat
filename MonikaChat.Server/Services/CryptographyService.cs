using Microsoft.Extensions.Options;
using MonikaChat.Server.Models.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace MonikaChat.Server.Services
{
    public class CryptographyService
    {
        private readonly CryptographyOptions _options;

        public CryptographyService(IOptions<CryptographyOptions> options)
        {
			_options = options.Value;
		}

		public byte[] DecryptWithPrivateKey(string encryptedData)
		{
			byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

			return DecryptWithPrivateKey(encryptedBytes);
		}

		public byte[] DecryptWithPrivateKey(byte[] encryptedBytes)
		{
			using (RSA rsa = RSA.Create())
			{
				rsa.FromXmlString(_options.PrivateKey);
				byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);

				return decryptedBytes;
			}
		}

		public string DecryptAESGCMData(byte[] encryptedDataBytes, byte[] iv, byte[] aesKey)
		{
			// Validate IV length for AES-GCM
			if (iv.Length != 12)
			{
				throw new ArgumentException("Invalid IV length for AES-GCM. Expected 12 bytes.");
			}

			// Separate the tag from the encrypted data
			int tagSizeInBytes = 16;
			byte[] tag = new byte[tagSizeInBytes];
			byte[] ciphertext = new byte[encryptedDataBytes.Length - tagSizeInBytes];

			Array.Copy(encryptedDataBytes, encryptedDataBytes.Length - tagSizeInBytes, tag, 0, tagSizeInBytes);
			Array.Copy(encryptedDataBytes, 0, ciphertext, 0, ciphertext.Length);

			// Decrypt the data using AES-GCM
			using (AesGcm aesGcm = new AesGcm(aesKey, tagSizeInBytes))
			{
				byte[] decryptedBytes = new byte[ciphertext.Length];
				aesGcm.Decrypt(iv, ciphertext, tag, decryptedBytes);

				return Encoding.UTF8.GetString(decryptedBytes);
			}
		}

		public (byte[] encryptedData, byte[] iv) EncryptAESGCMData(string data, byte[] aesKey)
		{
			byte[] dataBytes = Encoding.UTF8.GetBytes(data);
			int tagSizeInBytes = 16;

			byte[] encryptedBytes = new byte[dataBytes.Length]; // Only for the encrypted data
			byte[] tag = new byte[tagSizeInBytes]; // Separate buffer for the tag
			byte[] iv = new byte[12]; // AES-GCM needs a 12-byte IV (nonce)
			RandomNumberGenerator.Fill(iv); // Generate a new IV for each encryption

			using (AesGcm aesGcm = new AesGcm(aesKey, tagSizeInBytes))
			{
				// Encrypt the data; encryptedBytes will be filled with the ciphertext
				aesGcm.Encrypt(iv, dataBytes, encryptedBytes, tag);

				// Now we need to append the tag to the encrypted data
				byte[] encryptedDataWithTag = new byte[encryptedBytes.Length + tag.Length];
				Array.Copy(encryptedBytes, 0, encryptedDataWithTag, 0, encryptedBytes.Length);
				Array.Copy(tag, 0, encryptedDataWithTag, encryptedBytes.Length, tag.Length);

				return (encryptedDataWithTag, iv);
			}
		}

	}
}
