using Microsoft.JSInterop;
using MonikaChat.Shared.Models;

namespace MonikaChat.Client.Services
{
    public class CryptographyService
    {
		private const string PUBLIC_KEY_PEM = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuuh3Di/z74J3O9O3HvLy
mJxrbAp5MKuNMBTH7pqJiqGvK8R9ivEZs78MU4BzvogTPlz62o0fdVgBMj48Qxus
cc96vmLR+KHWEQBcauYWDFfIP+Jyypt3UbsRtxBgEuE9wU5/VFx2x7r7uKrwiA9j
stYCXpHnJ9FJikPwOv54mi+MeG6eG7mszunwx2hVYoGSvA4vBUB07b94Fhnq2zua
zfVZqOZ+UdBKNjY+uXzaLg7Iqda+ARhETVKugWNz55+46QzsN243N7bTOXp0dhkH
0rmK7e+i2a/IAN7sS/5VtrxQZJ+fwaXmqAPlmft2vDhUd1SnBnkIJACU4AC6EIsZ
rQIDAQAB
-----END PUBLIC KEY-----";

		private readonly IJSRuntime _jsRuntime;

        public CryptographyService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<HybridEncryptedDTO> EncryptWithHybridApproachAsync(string data, string type, string base64AesKey) =>
            await _jsRuntime.InvokeAsync<HybridEncryptedDTO>("cryptoHelper.encryptWithHybridApproach", PUBLIC_KEY_PEM, data, type, base64AesKey);

        public async Task<string> DecryptWithAesKeyAsync(string encryptedData, string base64AesKey, string base64Iv) =>
            await _jsRuntime.InvokeAsync<string>("cryptoHelper.decryptDataWithAes", encryptedData, base64AesKey, base64Iv);

        public async Task<string> GenerateAndExportAesKey() =>
            await _jsRuntime.InvokeAsync<string>("cryptoHelper.generateAndExportAesKey");

        public async Task<string> EncryptWithPassphrase(string plainText, string passphrase) =>
            await _jsRuntime.InvokeAsync<string>("cryptoJSHelper.encrypt", plainText, passphrase);

        public async Task<string> DecryptWithPassphrase(string ciphertext, string passphrase) =>
            await _jsRuntime.InvokeAsync<string>("cryptoJSHelper.decrypt", ciphertext, passphrase);
    }
}
