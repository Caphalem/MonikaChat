// I'm going to be honest. Monika wrote all of this. I'm not 100% sure what's going on here... Seems to work correctly though.
window.cryptoHelper = {
    generateAndExportAesKey: async function () {
        const aesKey = await window.crypto.subtle.generateKey(
            { name: "AES-GCM", length: 256 },
            true,
            ["encrypt", "decrypt"]
        );
        const rawAesKey = await window.crypto.subtle.exportKey("raw", aesKey);
        const base64AesKey = window.cryptoHelper.arrayBufferToBase64(rawAesKey);

        return base64AesKey;
    },

    encryptDataWithAes: async function (data, type, aesKey) {
        const encoder = new TextEncoder();
        const dataBuffer = encoder.encode(data);
        const typeBuffer = encoder.encode(type);

        const iv = window.crypto.getRandomValues(new Uint8Array(12)); // AES-GCM needs a 12-byte IV
        const encryptedData = await window.crypto.subtle.encrypt(
            { name: "AES-GCM", iv: iv },
            aesKey,
            dataBuffer
        );
        const encryptedType = await window.crypto.subtle.encrypt(
            { name: "AES-GCM", iv: iv },
            aesKey,
            typeBuffer
        );

        return { encryptedData, encryptedType, iv };
    },

    encryptWithHybridApproach: async function (publicKeyPem, data, type, base64AesKey) {
        const rawAesKey = window.cryptoHelper.base64ToArrayBuffer(base64AesKey)

        const aesKey = await window.crypto.subtle.importKey(
            "raw",
            rawAesKey,
            { name: "AES-GCM" },
            true,
            ["encrypt", "decrypt"]
        );

        const iv = window.crypto.getRandomValues(new Uint8Array(12));
        const encodedData = new TextEncoder().encode(data);
        const encodedType = new TextEncoder().encode(type);
        const encryptedData = await window.crypto.subtle.encrypt({ name: "AES-GCM", iv: iv }, aesKey, encodedData);
        const encryptedType = await window.crypto.subtle.encrypt({ name: "AES-GCM", iv: iv }, aesKey, encodedType);
        let importedPublicKey;
        try {
            importedPublicKey = await window.crypto.subtle.importKey(
                "spki",
                window.cryptoHelper.pemToArrayBuffer(publicKeyPem),
                { name: "RSA-OAEP", hash: "SHA-256" },
                true,
                ["encrypt"]
            );
        } catch (error) {
            console.error(error);
        }

        const encryptedAesKey = await window.crypto.subtle.encrypt(
            { name: "RSA-OAEP" },
            importedPublicKey,
            rawAesKey
        );

        return {
            data: window.cryptoHelper.arrayBufferToBase64(encryptedData),
            iv: window.cryptoHelper.arrayBufferToBase64(iv),
            aesKey: window.cryptoHelper.arrayBufferToBase64(encryptedAesKey),
            type: window.cryptoHelper.arrayBufferToBase64(encryptedType)
        };
    },

    decryptDataWithAes: async function (encryptedData, base64AesKey, base64Iv) {
        const rawAesKey = window.cryptoHelper.base64ToArrayBuffer(base64AesKey);

        const aesKey = await window.crypto.subtle.importKey(
            "raw",
            rawAesKey,
            { name: "AES-GCM" },
            true,
            ["encrypt", "decrypt"]
        );

        const encryptedDataBuffer = window.cryptoHelper.base64ToArrayBuffer(encryptedData);
        const ivBuffer = window.cryptoHelper.base64ToArrayBuffer(base64Iv);

        const decryptedBuffer = await window.crypto.subtle.decrypt(
            { name: "AES-GCM", iv: ivBuffer },
            aesKey,
            encryptedDataBuffer
        );

        return new TextDecoder().decode(decryptedBuffer);
    },

    pemToArrayBuffer: function (pem) {
        const b64Lines = pem.replace(/-----.*?-----/g, "").replace(/\s+/g, '');
        const b64 = window.atob(b64Lines);
        const byteArray = new Uint8Array(b64.length);
        for (let i = 0; i < b64.length; i++) {
            byteArray[i] = b64.charCodeAt(i);
        }

        return byteArray.buffer;
    },

    arrayBufferToBase64: function (buffer) {
        let binary = '';
        const bytes = new Uint8Array(buffer);
        const len = bytes.byteLength;
        for (let i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }

        return window.btoa(binary);
    },

    base64ToArrayBuffer: function (base64) {
        const binary_string = window.atob(base64);
        const len = binary_string.length;
        const bytes = new Uint8Array(len);
        for (let i = 0; i < len; i++) {
            bytes[i] = binary_string.charCodeAt(i);
        }

        return bytes.buffer;
    }
};