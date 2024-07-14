namespace MonikaChat.Client.Interfaces
{
    public interface ISettingsService
    {
        int MinTextSpeed { get; }

		int MaxTextSpeed { get; }

		int DefaultTextSpeed { get; }

        Task<string> GetUsername();

        Task SetUsername(string username);

        Task<string> GetEncryptedAPIKey();

        Task SetEncryptedAPIKey(string encryptedAPIKey);

        Task<int> GetTextSpeed();

		Task SetTextSpeed(int textSpeed);

        Task<bool> GetBackgroundAnimationToggle();

        Task SetBackgroundAnimationToggle(bool backgroundAnimationToggle);

        Task<bool> GetHiddenModeToggle();

        Task SetHiddenModeToggle(bool hiddenModeToggle);

		Task<string> DecryptAPIKey(string passphrase);

        Task<string> EncryptAPIKey(string passphrase, string apiKey);
    }
}
