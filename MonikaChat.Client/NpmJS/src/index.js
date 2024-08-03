import { get_encoding } from "tiktoken";

window.tiktokenHelper = {
    countTokens: function (text) {
        const encoding = get_encoding("o200k_base");
        const tokenAmount = encoding.encode(text).length;
        encoding.free();

        return tokenAmount;
    }
}
