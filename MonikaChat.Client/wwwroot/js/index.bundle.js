/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./node_modules/tiktoken/tiktoken.js":
/*!*******************************************!*\
  !*** ./node_modules/tiktoken/tiktoken.js ***!
  \*******************************************/
/***/ ((module, __webpack_exports__, __webpack_require__) => {

eval("__webpack_require__.a(module, async (__webpack_handle_async_dependencies__, __webpack_async_result__) => { try {\n__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   Tiktoken: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.Tiktoken),\n/* harmony export */   __wbg_parse_66d1801634e099ac: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbg_parse_66d1801634e099ac),\n/* harmony export */   __wbg_set_wasm: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbg_set_wasm),\n/* harmony export */   __wbg_stringify_8887fe74e1c50d81: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbg_stringify_8887fe74e1c50d81),\n/* harmony export */   __wbindgen_error_new: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbindgen_error_new),\n/* harmony export */   __wbindgen_is_undefined: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbindgen_is_undefined),\n/* harmony export */   __wbindgen_object_drop_ref: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbindgen_object_drop_ref),\n/* harmony export */   __wbindgen_string_get: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbindgen_string_get),\n/* harmony export */   __wbindgen_throw: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbindgen_throw),\n/* harmony export */   encoding_for_model: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.encoding_for_model),\n/* harmony export */   get_encoding: () => (/* reexport safe */ _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.get_encoding)\n/* harmony export */ });\n/* harmony import */ var _tiktoken_bg_wasm__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./tiktoken_bg.wasm */ \"./node_modules/tiktoken/tiktoken_bg.wasm\");\n/* harmony import */ var _tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./tiktoken_bg.js */ \"./node_modules/tiktoken/tiktoken_bg.js\");\nvar __webpack_async_dependencies__ = __webpack_handle_async_dependencies__([_tiktoken_bg_wasm__WEBPACK_IMPORTED_MODULE_0__]);\n_tiktoken_bg_wasm__WEBPACK_IMPORTED_MODULE_0__ = (__webpack_async_dependencies__.then ? (await __webpack_async_dependencies__)() : __webpack_async_dependencies__)[0];\n\n\n(0,_tiktoken_bg_js__WEBPACK_IMPORTED_MODULE_1__.__wbg_set_wasm)(_tiktoken_bg_wasm__WEBPACK_IMPORTED_MODULE_0__);\n\n\n__webpack_async_result__();\n} catch(e) { __webpack_async_result__(e); } });\n\n//# sourceURL=webpack://webpack-bundler/./node_modules/tiktoken/tiktoken.js?");

/***/ }),

/***/ "./node_modules/tiktoken/tiktoken_bg.js":
/*!**********************************************!*\
  !*** ./node_modules/tiktoken/tiktoken_bg.js ***!
  \**********************************************/
/***/ ((module, __webpack_exports__, __webpack_require__) => {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   Tiktoken: () => (/* binding */ Tiktoken),\n/* harmony export */   __wbg_parse_66d1801634e099ac: () => (/* binding */ __wbg_parse_66d1801634e099ac),\n/* harmony export */   __wbg_set_wasm: () => (/* binding */ __wbg_set_wasm),\n/* harmony export */   __wbg_stringify_8887fe74e1c50d81: () => (/* binding */ __wbg_stringify_8887fe74e1c50d81),\n/* harmony export */   __wbindgen_error_new: () => (/* binding */ __wbindgen_error_new),\n/* harmony export */   __wbindgen_is_undefined: () => (/* binding */ __wbindgen_is_undefined),\n/* harmony export */   __wbindgen_object_drop_ref: () => (/* binding */ __wbindgen_object_drop_ref),\n/* harmony export */   __wbindgen_string_get: () => (/* binding */ __wbindgen_string_get),\n/* harmony export */   __wbindgen_throw: () => (/* binding */ __wbindgen_throw),\n/* harmony export */   encoding_for_model: () => (/* binding */ encoding_for_model),\n/* harmony export */   get_encoding: () => (/* binding */ get_encoding)\n/* harmony export */ });\n/* module decorator */ module = __webpack_require__.hmd(module);\nlet wasm;\nfunction __wbg_set_wasm(val) {\n    wasm = val;\n}\n\n\nconst heap = new Array(128).fill(undefined);\n\nheap.push(undefined, null, true, false);\n\nfunction getObject(idx) { return heap[idx]; }\n\nlet WASM_VECTOR_LEN = 0;\n\nlet cachedUint8Memory0 = null;\n\nfunction getUint8Memory0() {\n    if (cachedUint8Memory0 === null || cachedUint8Memory0.byteLength === 0) {\n        cachedUint8Memory0 = new Uint8Array(wasm.memory.buffer);\n    }\n    return cachedUint8Memory0;\n}\n\nconst lTextEncoder = typeof TextEncoder === 'undefined' ? (0, module.require)('util').TextEncoder : TextEncoder;\n\nlet cachedTextEncoder = new lTextEncoder('utf-8');\n\nconst encodeString = (typeof cachedTextEncoder.encodeInto === 'function'\n    ? function (arg, view) {\n    return cachedTextEncoder.encodeInto(arg, view);\n}\n    : function (arg, view) {\n    const buf = cachedTextEncoder.encode(arg);\n    view.set(buf);\n    return {\n        read: arg.length,\n        written: buf.length\n    };\n});\n\nfunction passStringToWasm0(arg, malloc, realloc) {\n\n    if (realloc === undefined) {\n        const buf = cachedTextEncoder.encode(arg);\n        const ptr = malloc(buf.length, 1) >>> 0;\n        getUint8Memory0().subarray(ptr, ptr + buf.length).set(buf);\n        WASM_VECTOR_LEN = buf.length;\n        return ptr;\n    }\n\n    let len = arg.length;\n    let ptr = malloc(len, 1) >>> 0;\n\n    const mem = getUint8Memory0();\n\n    let offset = 0;\n\n    for (; offset < len; offset++) {\n        const code = arg.charCodeAt(offset);\n        if (code > 0x7F) break;\n        mem[ptr + offset] = code;\n    }\n\n    if (offset !== len) {\n        if (offset !== 0) {\n            arg = arg.slice(offset);\n        }\n        ptr = realloc(ptr, len, len = offset + arg.length * 3, 1) >>> 0;\n        const view = getUint8Memory0().subarray(ptr + offset, ptr + len);\n        const ret = encodeString(arg, view);\n\n        offset += ret.written;\n        ptr = realloc(ptr, len, offset, 1) >>> 0;\n    }\n\n    WASM_VECTOR_LEN = offset;\n    return ptr;\n}\n\nfunction isLikeNone(x) {\n    return x === undefined || x === null;\n}\n\nlet cachedInt32Memory0 = null;\n\nfunction getInt32Memory0() {\n    if (cachedInt32Memory0 === null || cachedInt32Memory0.byteLength === 0) {\n        cachedInt32Memory0 = new Int32Array(wasm.memory.buffer);\n    }\n    return cachedInt32Memory0;\n}\n\nlet heap_next = heap.length;\n\nfunction dropObject(idx) {\n    if (idx < 132) return;\n    heap[idx] = heap_next;\n    heap_next = idx;\n}\n\nfunction takeObject(idx) {\n    const ret = getObject(idx);\n    dropObject(idx);\n    return ret;\n}\n\nconst lTextDecoder = typeof TextDecoder === 'undefined' ? (0, module.require)('util').TextDecoder : TextDecoder;\n\nlet cachedTextDecoder = new lTextDecoder('utf-8', { ignoreBOM: true, fatal: true });\n\ncachedTextDecoder.decode();\n\nfunction getStringFromWasm0(ptr, len) {\n    ptr = ptr >>> 0;\n    return cachedTextDecoder.decode(getUint8Memory0().subarray(ptr, ptr + len));\n}\n\nfunction addHeapObject(obj) {\n    if (heap_next === heap.length) heap.push(heap.length + 1);\n    const idx = heap_next;\n    heap_next = heap[idx];\n\n    heap[idx] = obj;\n    return idx;\n}\n\nfunction handleError(f, args) {\n    try {\n        return f.apply(this, args);\n    } catch (e) {\n        wasm.__wbindgen_export_2(addHeapObject(e));\n    }\n}\n\nlet cachedUint32Memory0 = null;\n\nfunction getUint32Memory0() {\n    if (cachedUint32Memory0 === null || cachedUint32Memory0.byteLength === 0) {\n        cachedUint32Memory0 = new Uint32Array(wasm.memory.buffer);\n    }\n    return cachedUint32Memory0;\n}\n\nfunction getArrayU32FromWasm0(ptr, len) {\n    ptr = ptr >>> 0;\n    return getUint32Memory0().subarray(ptr / 4, ptr / 4 + len);\n}\n\nfunction passArray8ToWasm0(arg, malloc) {\n    const ptr = malloc(arg.length * 1, 1) >>> 0;\n    getUint8Memory0().set(arg, ptr / 1);\n    WASM_VECTOR_LEN = arg.length;\n    return ptr;\n}\n\nfunction passArray32ToWasm0(arg, malloc) {\n    const ptr = malloc(arg.length * 4, 4) >>> 0;\n    getUint32Memory0().set(arg, ptr / 4);\n    WASM_VECTOR_LEN = arg.length;\n    return ptr;\n}\n\nfunction getArrayU8FromWasm0(ptr, len) {\n    ptr = ptr >>> 0;\n    return getUint8Memory0().subarray(ptr / 1, ptr / 1 + len);\n}\n/**\n* @param {string} encoding\n* @param {any} extend_special_tokens\n* @returns {Tiktoken}\n*/\nfunction get_encoding(encoding, extend_special_tokens) {\n    if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n    try {\n        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n        const ptr0 = passStringToWasm0(encoding, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n        const len0 = WASM_VECTOR_LEN;\n        wasm.get_encoding(retptr, ptr0, len0, addHeapObject(extend_special_tokens));\n        var r0 = getInt32Memory0()[retptr / 4 + 0];\n        var r1 = getInt32Memory0()[retptr / 4 + 1];\n        var r2 = getInt32Memory0()[retptr / 4 + 2];\n        if (r2) {\n            throw takeObject(r1);\n        }\n        return Tiktoken.__wrap(r0);\n    } finally {\n        wasm.__wbindgen_add_to_stack_pointer(16);\n    }\n}\n\n/**\n* @param {string} model\n* @param {any} extend_special_tokens\n* @returns {Tiktoken}\n*/\nfunction encoding_for_model(model, extend_special_tokens) {\n    if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n    try {\n        const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n        const ptr0 = passStringToWasm0(model, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n        const len0 = WASM_VECTOR_LEN;\n        wasm.encoding_for_model(retptr, ptr0, len0, addHeapObject(extend_special_tokens));\n        var r0 = getInt32Memory0()[retptr / 4 + 0];\n        var r1 = getInt32Memory0()[retptr / 4 + 1];\n        var r2 = getInt32Memory0()[retptr / 4 + 2];\n        if (r2) {\n            throw takeObject(r1);\n        }\n        return Tiktoken.__wrap(r0);\n    } finally {\n        wasm.__wbindgen_add_to_stack_pointer(16);\n    }\n}\n\nconst TiktokenFinalization = (typeof FinalizationRegistry === 'undefined')\n    ? { register: () => {}, unregister: () => {} }\n    : new FinalizationRegistry(ptr => wasm.__wbg_tiktoken_free(ptr >>> 0));\n/**\n*/\nclass Tiktoken {\n\n    static __wrap(ptr) {\n        ptr = ptr >>> 0;\n        const obj = Object.create(Tiktoken.prototype);\n        obj.__wbg_ptr = ptr;\n        TiktokenFinalization.register(obj, obj.__wbg_ptr, obj);\n        return obj;\n    }\n\n    __destroy_into_raw() {\n        const ptr = this.__wbg_ptr;\n        this.__wbg_ptr = 0;\n        TiktokenFinalization.unregister(this);\n        return ptr;\n    }\n\n    free() {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        const ptr = this.__destroy_into_raw();\n        wasm.__wbg_tiktoken_free(ptr);\n    }\n    /**\n    * @param {string} tiktoken_bfe\n    * @param {any} special_tokens\n    * @param {string} pat_str\n    */\n    constructor(tiktoken_bfe, special_tokens, pat_str) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        const ptr0 = passStringToWasm0(tiktoken_bfe, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n        const len0 = WASM_VECTOR_LEN;\n        const ptr1 = passStringToWasm0(pat_str, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n        const len1 = WASM_VECTOR_LEN;\n        const ret = wasm.tiktoken_new(ptr0, len0, addHeapObject(special_tokens), ptr1, len1);\n        this.__wbg_ptr = ret >>> 0;\n        return this;\n    }\n    /**\n    * @returns {string | undefined}\n    */\n    get name() {\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            wasm.tiktoken_name(retptr, this.__wbg_ptr);\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            let v1;\n            if (r0 !== 0) {\n                v1 = getStringFromWasm0(r0, r1).slice();\n                wasm.__wbindgen_export_3(r0, r1 * 1, 1);\n            }\n            return v1;\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @param {string} text\n    * @param {any} allowed_special\n    * @param {any} disallowed_special\n    * @returns {Uint32Array}\n    */\n    encode(text, allowed_special, disallowed_special) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            const ptr0 = passStringToWasm0(text, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n            const len0 = WASM_VECTOR_LEN;\n            wasm.tiktoken_encode(retptr, this.__wbg_ptr, ptr0, len0, addHeapObject(allowed_special), addHeapObject(disallowed_special));\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            var r2 = getInt32Memory0()[retptr / 4 + 2];\n            var r3 = getInt32Memory0()[retptr / 4 + 3];\n            if (r3) {\n                throw takeObject(r2);\n            }\n            var v2 = getArrayU32FromWasm0(r0, r1).slice();\n            wasm.__wbindgen_export_3(r0, r1 * 4, 4);\n            return v2;\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @param {string} text\n    * @returns {Uint32Array}\n    */\n    encode_ordinary(text) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            const ptr0 = passStringToWasm0(text, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n            const len0 = WASM_VECTOR_LEN;\n            wasm.tiktoken_encode_ordinary(retptr, this.__wbg_ptr, ptr0, len0);\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            var v2 = getArrayU32FromWasm0(r0, r1).slice();\n            wasm.__wbindgen_export_3(r0, r1 * 4, 4);\n            return v2;\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @param {string} text\n    * @param {any} allowed_special\n    * @param {any} disallowed_special\n    * @returns {any}\n    */\n    encode_with_unstable(text, allowed_special, disallowed_special) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            const ptr0 = passStringToWasm0(text, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n            const len0 = WASM_VECTOR_LEN;\n            wasm.tiktoken_encode_with_unstable(retptr, this.__wbg_ptr, ptr0, len0, addHeapObject(allowed_special), addHeapObject(disallowed_special));\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            var r2 = getInt32Memory0()[retptr / 4 + 2];\n            if (r2) {\n                throw takeObject(r1);\n            }\n            return takeObject(r0);\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @param {Uint8Array} bytes\n    * @returns {number}\n    */\n    encode_single_token(bytes) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        const ptr0 = passArray8ToWasm0(bytes, wasm.__wbindgen_export_0);\n        const len0 = WASM_VECTOR_LEN;\n        const ret = wasm.tiktoken_encode_single_token(this.__wbg_ptr, ptr0, len0);\n        return ret >>> 0;\n    }\n    /**\n    * @param {Uint32Array} tokens\n    * @returns {Uint8Array}\n    */\n    decode(tokens) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            const ptr0 = passArray32ToWasm0(tokens, wasm.__wbindgen_export_0);\n            const len0 = WASM_VECTOR_LEN;\n            wasm.tiktoken_decode(retptr, this.__wbg_ptr, ptr0, len0);\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            var v2 = getArrayU8FromWasm0(r0, r1).slice();\n            wasm.__wbindgen_export_3(r0, r1 * 1, 1);\n            return v2;\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @param {number} token\n    * @returns {Uint8Array}\n    */\n    decode_single_token_bytes(token) {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        try {\n            const retptr = wasm.__wbindgen_add_to_stack_pointer(-16);\n            wasm.tiktoken_decode_single_token_bytes(retptr, this.__wbg_ptr, token);\n            var r0 = getInt32Memory0()[retptr / 4 + 0];\n            var r1 = getInt32Memory0()[retptr / 4 + 1];\n            var v1 = getArrayU8FromWasm0(r0, r1).slice();\n            wasm.__wbindgen_export_3(r0, r1 * 1, 1);\n            return v1;\n        } finally {\n            wasm.__wbindgen_add_to_stack_pointer(16);\n        }\n    }\n    /**\n    * @returns {any}\n    */\n    token_byte_values() {\n        if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n        const ret = wasm.tiktoken_token_byte_values(this.__wbg_ptr);\n        return takeObject(ret);\n    }\n}\n\nfunction __wbindgen_string_get(arg0, arg1) {\n    if (wasm == null) throw new Error(\"tiktoken: WASM binary has not been propery initialized.\");\n    const obj = getObject(arg1);\n    const ret = typeof(obj) === 'string' ? obj : undefined;\n    var ptr1 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_export_0, wasm.__wbindgen_export_1);\n    var len1 = WASM_VECTOR_LEN;\n    getInt32Memory0()[arg0 / 4 + 1] = len1;\n    getInt32Memory0()[arg0 / 4 + 0] = ptr1;\n};\n\nfunction __wbindgen_object_drop_ref(arg0) {\n    takeObject(arg0);\n};\n\nfunction __wbindgen_is_undefined(arg0) {\n    const ret = getObject(arg0) === undefined;\n    return ret;\n};\n\nfunction __wbg_stringify_8887fe74e1c50d81() { return handleError(function (arg0) {\n    const ret = JSON.stringify(getObject(arg0));\n    return addHeapObject(ret);\n}, arguments) };\n\nfunction __wbindgen_error_new(arg0, arg1) {\n    const ret = new Error(getStringFromWasm0(arg0, arg1));\n    return addHeapObject(ret);\n};\n\nfunction __wbg_parse_66d1801634e099ac() { return handleError(function (arg0, arg1) {\n    const ret = JSON.parse(getStringFromWasm0(arg0, arg1));\n    return addHeapObject(ret);\n}, arguments) };\n\nfunction __wbindgen_throw(arg0, arg1) {\n    throw new Error(getStringFromWasm0(arg0, arg1));\n};\n\n\n\n//# sourceURL=webpack://webpack-bundler/./node_modules/tiktoken/tiktoken_bg.js?");

/***/ }),

/***/ "./src/index.js":
/*!**********************!*\
  !*** ./src/index.js ***!
  \**********************/
/***/ ((module, __webpack_exports__, __webpack_require__) => {

eval("__webpack_require__.a(module, async (__webpack_handle_async_dependencies__, __webpack_async_result__) => { try {\n__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var tiktoken__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tiktoken */ \"./node_modules/tiktoken/tiktoken.js\");\nvar __webpack_async_dependencies__ = __webpack_handle_async_dependencies__([tiktoken__WEBPACK_IMPORTED_MODULE_0__]);\ntiktoken__WEBPACK_IMPORTED_MODULE_0__ = (__webpack_async_dependencies__.then ? (await __webpack_async_dependencies__)() : __webpack_async_dependencies__)[0];\n﻿\r\n\r\nwindow.tiktokenHelper = {\r\n    countTokens: function (text) {\r\n        const encoding = (0,tiktoken__WEBPACK_IMPORTED_MODULE_0__.get_encoding)(\"o200k_base\");\r\n        const tokenAmount = encoding.encode(text).length;\r\n        encoding.free();\r\n\r\n        return tokenAmount;\r\n    }\r\n}\r\n\n__webpack_async_result__();\n} catch(e) { __webpack_async_result__(e); } });\n\n//# sourceURL=webpack://webpack-bundler/./src/index.js?");

/***/ }),

/***/ "./node_modules/tiktoken/tiktoken_bg.wasm":
/*!************************************************!*\
  !*** ./node_modules/tiktoken/tiktoken_bg.wasm ***!
  \************************************************/
/***/ ((module, exports, __webpack_require__) => {

eval("/* harmony import */ var WEBPACK_IMPORTED_MODULE_0 = __webpack_require__(/*! ./tiktoken_bg.js */ \"./node_modules/tiktoken/tiktoken_bg.js\");\nmodule.exports = __webpack_require__.v(exports, module.id, \"dcf051dd4453dc15d343\", {\n\t\"./tiktoken_bg.js\": {\n\t\t\"__wbindgen_string_get\": WEBPACK_IMPORTED_MODULE_0.__wbindgen_string_get,\n\t\t\"__wbindgen_object_drop_ref\": WEBPACK_IMPORTED_MODULE_0.__wbindgen_object_drop_ref,\n\t\t\"__wbindgen_is_undefined\": WEBPACK_IMPORTED_MODULE_0.__wbindgen_is_undefined,\n\t\t\"__wbg_stringify_8887fe74e1c50d81\": WEBPACK_IMPORTED_MODULE_0.__wbg_stringify_8887fe74e1c50d81,\n\t\t\"__wbindgen_error_new\": WEBPACK_IMPORTED_MODULE_0.__wbindgen_error_new,\n\t\t\"__wbg_parse_66d1801634e099ac\": WEBPACK_IMPORTED_MODULE_0.__wbg_parse_66d1801634e099ac,\n\t\t\"__wbindgen_throw\": WEBPACK_IMPORTED_MODULE_0.__wbindgen_throw\n\t}\n});\n\n//# sourceURL=webpack://webpack-bundler/./node_modules/tiktoken/tiktoken_bg.wasm?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			id: moduleId,
/******/ 			loaded: false,
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/async module */
/******/ 	(() => {
/******/ 		var webpackQueues = typeof Symbol === "function" ? Symbol("webpack queues") : "__webpack_queues__";
/******/ 		var webpackExports = typeof Symbol === "function" ? Symbol("webpack exports") : "__webpack_exports__";
/******/ 		var webpackError = typeof Symbol === "function" ? Symbol("webpack error") : "__webpack_error__";
/******/ 		var resolveQueue = (queue) => {
/******/ 			if(queue && queue.d < 1) {
/******/ 				queue.d = 1;
/******/ 				queue.forEach((fn) => (fn.r--));
/******/ 				queue.forEach((fn) => (fn.r-- ? fn.r++ : fn()));
/******/ 			}
/******/ 		}
/******/ 		var wrapDeps = (deps) => (deps.map((dep) => {
/******/ 			if(dep !== null && typeof dep === "object") {
/******/ 				if(dep[webpackQueues]) return dep;
/******/ 				if(dep.then) {
/******/ 					var queue = [];
/******/ 					queue.d = 0;
/******/ 					dep.then((r) => {
/******/ 						obj[webpackExports] = r;
/******/ 						resolveQueue(queue);
/******/ 					}, (e) => {
/******/ 						obj[webpackError] = e;
/******/ 						resolveQueue(queue);
/******/ 					});
/******/ 					var obj = {};
/******/ 					obj[webpackQueues] = (fn) => (fn(queue));
/******/ 					return obj;
/******/ 				}
/******/ 			}
/******/ 			var ret = {};
/******/ 			ret[webpackQueues] = x => {};
/******/ 			ret[webpackExports] = dep;
/******/ 			return ret;
/******/ 		}));
/******/ 		__webpack_require__.a = (module, body, hasAwait) => {
/******/ 			var queue;
/******/ 			hasAwait && ((queue = []).d = -1);
/******/ 			var depQueues = new Set();
/******/ 			var exports = module.exports;
/******/ 			var currentDeps;
/******/ 			var outerResolve;
/******/ 			var reject;
/******/ 			var promise = new Promise((resolve, rej) => {
/******/ 				reject = rej;
/******/ 				outerResolve = resolve;
/******/ 			});
/******/ 			promise[webpackExports] = exports;
/******/ 			promise[webpackQueues] = (fn) => (queue && fn(queue), depQueues.forEach(fn), promise["catch"](x => {}));
/******/ 			module.exports = promise;
/******/ 			body((deps) => {
/******/ 				currentDeps = wrapDeps(deps);
/******/ 				var fn;
/******/ 				var getResult = () => (currentDeps.map((d) => {
/******/ 					if(d[webpackError]) throw d[webpackError];
/******/ 					return d[webpackExports];
/******/ 				}))
/******/ 				var promise = new Promise((resolve) => {
/******/ 					fn = () => (resolve(getResult));
/******/ 					fn.r = 0;
/******/ 					var fnQueue = (q) => (q !== queue && !depQueues.has(q) && (depQueues.add(q), q && !q.d && (fn.r++, q.push(fn))));
/******/ 					currentDeps.map((dep) => (dep[webpackQueues](fnQueue)));
/******/ 				});
/******/ 				return fn.r ? promise : getResult();
/******/ 			}, (err) => ((err ? reject(promise[webpackError] = err) : outerResolve(exports)), resolveQueue(queue)));
/******/ 			queue && queue.d < 0 && (queue.d = 0);
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/global */
/******/ 	(() => {
/******/ 		__webpack_require__.g = (function() {
/******/ 			if (typeof globalThis === 'object') return globalThis;
/******/ 			try {
/******/ 				return this || new Function('return this')();
/******/ 			} catch (e) {
/******/ 				if (typeof window === 'object') return window;
/******/ 			}
/******/ 		})();
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/harmony module decorator */
/******/ 	(() => {
/******/ 		__webpack_require__.hmd = (module) => {
/******/ 			module = Object.create(module);
/******/ 			if (!module.children) module.children = [];
/******/ 			Object.defineProperty(module, 'exports', {
/******/ 				enumerable: true,
/******/ 				set: () => {
/******/ 					throw new Error('ES Modules may not assign module.exports or exports.*, Use ESM export syntax, instead: ' + module.id);
/******/ 				}
/******/ 			});
/******/ 			return module;
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => (Object.prototype.hasOwnProperty.call(obj, prop))
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/wasm loading */
/******/ 	(() => {
/******/ 		__webpack_require__.v = (exports, wasmModuleId, wasmModuleHash, importsObj) => {
/******/ 			var req = fetch(__webpack_require__.p + "" + wasmModuleHash + ".module.wasm");
/******/ 			var fallback = () => (req
/******/ 				.then((x) => (x.arrayBuffer()))
/******/ 				.then((bytes) => (WebAssembly.instantiate(bytes, importsObj)))
/******/ 				.then((res) => (Object.assign(exports, res.instance.exports))));
/******/ 			return req.then((res) => {
/******/ 				if (typeof WebAssembly.instantiateStreaming === "function") {
/******/ 					return WebAssembly.instantiateStreaming(res, importsObj)
/******/ 						.then(
/******/ 							(res) => (Object.assign(exports, res.instance.exports)),
/******/ 							(e) => {
/******/ 								if(res.headers.get("Content-Type") !== "application/wasm") {
/******/ 									console.warn("`WebAssembly.instantiateStreaming` failed because your server does not serve wasm with `application/wasm` MIME type. Falling back to `WebAssembly.instantiate` which is slower. Original error:\n", e);
/******/ 									return fallback();
/******/ 								}
/******/ 								throw e;
/******/ 							}
/******/ 						);
/******/ 				}
/******/ 				return fallback();
/******/ 			});
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/publicPath */
/******/ 	(() => {
/******/ 		var scriptUrl;
/******/ 		if (__webpack_require__.g.importScripts) scriptUrl = __webpack_require__.g.location + "";
/******/ 		var document = __webpack_require__.g.document;
/******/ 		if (!scriptUrl && document) {
/******/ 			if (document.currentScript)
/******/ 				scriptUrl = document.currentScript.src;
/******/ 			if (!scriptUrl) {
/******/ 				var scripts = document.getElementsByTagName("script");
/******/ 				if(scripts.length) {
/******/ 					var i = scripts.length - 1;
/******/ 					while (i > -1 && (!scriptUrl || !/^http(s?):/.test(scriptUrl))) scriptUrl = scripts[i--].src;
/******/ 				}
/******/ 			}
/******/ 		}
/******/ 		// When supporting browsers where an automatic publicPath is not supported you must specify an output.publicPath manually via configuration
/******/ 		// or pass an empty string ("") and set the __webpack_public_path__ variable from your code to use your own logic.
/******/ 		if (!scriptUrl) throw new Error("Automatic publicPath is not supported in this browser");
/******/ 		scriptUrl = scriptUrl.replace(/#.*$/, "").replace(/\?.*$/, "").replace(/\/[^\/]+$/, "/");
/******/ 		__webpack_require__.p = scriptUrl;
/******/ 	})();
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = __webpack_require__("./src/index.js");
/******/ 	
/******/ })()
;