using Microsoft.JSInterop;
using MonikaChat.Shared.Models;

namespace MonikaChat.Client.Services
{
    public class CryptographyService
    {
        private readonly IJSRuntime _jsRuntime;

        public CryptographyService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<HybridEncryptedDTO> EncryptWithHybridApproachAsync(string publicKeyPem, string data, string type, string base64AesKey) =>
            await _jsRuntime.InvokeAsync<HybridEncryptedDTO>("cryptoHelper.encryptWithHybridApproach", publicKeyPem, data, type, base64AesKey);

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
