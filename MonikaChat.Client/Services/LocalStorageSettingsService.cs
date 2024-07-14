using Blazored.LocalStorage;
using MonikaChat.Client.Interfaces;

namespace MonikaChat.Client.Services
{
    public class LocalStorageSettingsService : ISettingsService
    {
        private const int MIN_TEXT_SPEED = 0;
        private const int MAX_TEXT_SPEED = 10;
        private const int DEFAULT_TEXT_SPEED = 5;
        private const string USERNAME_SETTING_NAME = "username";
        private const string API_KEY_SETTING_NAME = "apiKey";
        private const string TEXT_SPEED_SETTING_NAME = "textSpeed";
        private const string ANIMATION_SETTING_NAME = "backgroundAnimationToggle";
        private const string HIDDEN_MODE_SETTING_NAME = "hiddenModeToggle";

        private readonly ILocalStorageService _localStorage;
        private readonly CryptographyService _cryptographyService;

        public LocalStorageSettingsService(ILocalStorageService localStorage, CryptographyService cryptographyService)
        {
            _localStorage = localStorage;
            _cryptographyService = cryptographyService;
        }

		public int MinTextSpeed => MIN_TEXT_SPEED;

		public int MaxTextSpeed => MAX_TEXT_SPEED;

		public int DefaultTextSpeed => DEFAULT_TEXT_SPEED;

        // Since I can't use Singletons in WebAssembly, I have these crazy getters and setters

		public async Task<string> GetUsername() =>
			await _localStorage.GetItemAsync<string>(USERNAME_SETTING_NAME) ?? string.Empty;

		public async Task SetUsername(string username)
		{
			if (string.IsNullOrWhiteSpace(username))
			{
				throw new ArgumentNullException("Username is empty.");
			}

			await _localStorage.SetItemAsync(USERNAME_SETTING_NAME, username);
		}

		public async Task<string> GetEncryptedAPIKey() =>
			await _localStorage.GetItemAsync<string>(API_KEY_SETTING_NAME) ?? string.Empty;

		public async Task SetEncryptedAPIKey(string encryptedAPIKey)
		{
			await _localStorage.SetItemAsync(API_KEY_SETTING_NAME, encryptedAPIKey);
		}

		public async Task<int> GetTextSpeed() =>
			await _localStorage.GetItemAsync<int>(TEXT_SPEED_SETTING_NAME);

		public async Task SetTextSpeed(int textSpeed)
		{
			textSpeed = ValidateTextSpeed(textSpeed);
			await _localStorage.SetItemAsync(TEXT_SPEED_SETTING_NAME, textSpeed);
		}

		public async Task<bool> GetBackgroundAnimationToggle() =>
			await _localStorage.GetItemAsync<bool>(ANIMATION_SETTING_NAME);

		public async Task SetBackgroundAnimationToggle(bool backgroundAnimationToggle)
		{
			await _localStorage.SetItemAsync(ANIMATION_SETTING_NAME, backgroundAnimationToggle);
		}

		public async Task<bool> GetHiddenModeToggle() =>
			await _localStorage.GetItemAsync<bool>(HIDDEN_MODE_SETTING_NAME);

		public async Task SetHiddenModeToggle(bool hiddenModeToggle)
		{
			await _localStorage.SetItemAsync(HIDDEN_MODE_SETTING_NAME, hiddenModeToggle);
		}

		public async Task<string> DecryptAPIKey(string passphrase)
        {
            if (string.IsNullOrWhiteSpace(passphrase))
            {
                throw new ArgumentNullException($"Passphrase is empty.");
            }

			string encryptedAPIKey = await GetEncryptedAPIKey();

            if (string.IsNullOrWhiteSpace(encryptedAPIKey))
            {
                throw new ArgumentNullException($"Encrypted API Key is empty.");
            }

			string apiKey = await _cryptographyService.DecryptWithPassphrase(encryptedAPIKey, passphrase);

            return apiKey;
        }

        public async Task<string> EncryptAPIKey(string passphrase, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(passphrase))
            {
                throw new ArgumentNullException($"Passphrase is empty.");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException($"API Key is empty.");
            }

			string encryptedAPIKey = await _cryptographyService.EncryptWithPassphrase(apiKey, passphrase);
			await SetEncryptedAPIKey(encryptedAPIKey);

            return encryptedAPIKey;
        }

        private int ValidateTextSpeed(int textSpeed)
        {
			if (textSpeed > MAX_TEXT_SPEED)
			{
				textSpeed = MAX_TEXT_SPEED;
			}

			if (textSpeed < MIN_TEXT_SPEED)
			{
				textSpeed = MIN_TEXT_SPEED;
			}

            return textSpeed;
		}
    }
}
