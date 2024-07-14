// Some CryptoJS for local API Key encryption/decryption
window.cryptoJSHelper = {
    encrypt: function (plainText, passphrase) {
        var encrypted = CryptoJS.AES.encrypt(plainText, passphrase).toString();

        return encrypted;
    },

    decrypt: function (cipherText, passphrase) {
        var bytes = CryptoJS.AES.decrypt(cipherText, passphrase);
        var decrypted = bytes.toString(CryptoJS.enc.Utf8);

        return decrypted;
    }
};
