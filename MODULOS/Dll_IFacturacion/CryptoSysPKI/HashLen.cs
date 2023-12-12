namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;

    internal enum HashLen
    {
        PKI_MAX_HASH_BYTES = 0x40,
        PKI_MAX_HASH_CHARS = 0x80,
        PKI_MD2_BYTES = 0x10,
        PKI_MD2_CHARS = 0x20,
        PKI_MD5_BYTES = 0x10,
        PKI_MD5_CHARS = 0x20,
        PKI_SHA1_BYTES = 20,
        PKI_SHA1_CHARS = 40,
        PKI_SHA224_BYTES = 0x1c,
        PKI_SHA224_CHARS = 0x38,
        PKI_SHA256_BYTES = 0x20,
        PKI_SHA256_CHARS = 0x40,
        PKI_SHA384_BYTES = 0x30,
        PKI_SHA384_CHARS = 0x60,
        PKI_SHA512_BYTES = 0x40,
        PKI_SHA512_CHARS = 0x80
    }
}

