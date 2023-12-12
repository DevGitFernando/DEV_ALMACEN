//////using System;
//////using System.Collections.Generic;
//////using System.Text;

//////namespace Dll_IFacturacion.CryptoSysPKI
//////{
//////    /// <summary>
//////    /// Cipher Mode
//////    /// </summary>
//////    enum Mode
//////    {
//////        /// <summary>
//////        /// Electronic Code Book mode
//////        /// </summary>
//////        ECB = 0x000,
//////        /// <summary>
//////        /// Cipher Block Chaining mode
//////        /// </summary>
//////        CBC = 0x100,
//////        /// <summary>
//////        /// Output Feedback mode
//////        /// </summary>
//////        OFB = 0x200,
//////        /// <summary>
//////        /// Cipher Feedback mode
//////        /// </summary>
//////        CFB = 0x300,
//////        /// <summary>
//////        /// Counter mode
//////        /// </summary>
//////        CTR = 0x400,
//////    }

//////    /// <summary>
//////    /// Block Cipher Algorithm
//////    /// </summary>
//////    enum CipherAlgorithm
//////    {
//////        /// <summary>
//////        /// Triple DES (TDEA, 3DES, des-ede3)
//////        /// </summary>
//////        Tdea = 0x10,
//////        /// <summary>
//////        /// AES-128
//////        /// </summary>
//////        Aes128 = 0x20,
//////        /// <summary>
//////        /// AES-192
//////        /// </summary>
//////        Aes192 = 0x30,
//////        /// <summary>
//////        /// AES-256
//////        /// </summary>
//////        Aes256 = 0x40,
//////    }

//////    /// <summary>
//////    /// Base for encoding methods
//////    /// </summary>
//////    enum EncodingBase
//////    {
//////        /// <summary>
//////        /// Base64 encoding
//////        /// </summary>
//////        Base64,
//////        /// <summary>
//////        /// Base16 encoding (i.e. hexadecimal)
//////        /// </summary>
//////        Base16,
//////    }

//////    /// <summary>
//////    /// Message Digest Hash Algorithm
//////    /// </summary>
//////    enum HashAlgorithm
//////    {
//////        /// <summary>
//////        /// SHA-1 (as per FIPS PUB 180-2)
//////        /// </summary>
//////        Sha1 = 0,
//////        /// <summary>
//////        /// MD5 (as per RFC 1321)
//////        /// </summary>
//////        Md5 = 1,
//////        /// <summary>
//////        /// MD2 (as per RFC 1319)
//////        /// </summary>
//////        Md2 = 2,
//////        /// <summary>
//////        /// SHA-256 (as per FIPS PUB 180-2)
//////        /// </summary>
//////        Sha256 = 3,
//////        /// <summary>
//////        /// SHA-384 (as per FIPS PUB 180-2)
//////        /// </summary>
//////        Sha384 = 4,
//////        /// <summary>
//////        /// SHA-512 (as per FIPS PUB 180-2)
//////        /// </summary>
//////        Sha512 = 5,
//////        /// <summary>
//////        /// SHA-224 (as per FIPS PUB 180-2, change notice 1, 2004-02-25)
//////        /// </summary>
//////        Sha224 = 6,
//////    }

//////    // Constants we use internally
//////    enum Direction
//////    {
//////        Encrypt = 1,
//////        Decrypt = 0
//////    }
//////    enum SignatureType
//////    {
//////        PKI_SIG_SHA1RSA = 0,
//////        PKI_SIG_MD5RSA = 1,
//////        PKI_SIG_MD2RSA = 2,
//////    }
//////    enum HashLen
//////    {
//////        PKI_SHA1_BYTES = 20,
//////        PKI_SHA224_BYTES = 28,
//////        PKI_SHA256_BYTES = 32,
//////        PKI_SHA384_BYTES = 48,
//////        PKI_SHA512_BYTES = 64,
//////        PKI_MD5_BYTES = 16,
//////        PKI_MD2_BYTES = 16,
//////        PKI_MAX_HASH_BYTES = 64,

//////        PKI_SHA1_CHARS = 40,
//////        PKI_SHA224_CHARS = 56,
//////        PKI_SHA256_CHARS = 64,
//////        PKI_SHA384_CHARS = 96,
//////        PKI_SHA512_CHARS = 128,
//////        PKI_MD5_CHARS = 32,
//////        PKI_MD2_CHARS = 32,
//////        PKI_MAX_HASH_CHARS = 128
//////    }
//////    enum Emsig
//////    {
//////        PKI_EMSIG_DEFAULT = 0x20,
//////        PKI_EMSIG_DIGESTONLY = 0x1000,
//////        PKI_EMSIG_DIGINFO = 0x2000,
//////        PKI_EMSIG_ISO9796 = 0x100000,
//////    }
//////    enum myQuery
//////    {
//////        PKI_QUERY_GETTYPE = 0x100000,
//////        PKI_QUERY_NUMBER = 1,
//////        PKI_QUERY_STRING = 2,
//////    }
//////}
