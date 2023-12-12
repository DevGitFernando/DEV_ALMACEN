namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Hmac
    {
        private Hmac()
        {
        }

        public static byte[] BytesFromBytes(byte[] message, byte[] key, HashAlgorithm hashAlg)
        {
            byte[] digest = new byte[MyInternals.HashBytes(hashAlg)];
            HMAC_Bytes(digest, digest.Length, message, message.Length, key, key.Length, (int) hashAlg);
            return digest;
        }

        public static string HexFromBytes(byte[] message, byte[] key, HashAlgorithm hashAlg)
        {
            StringBuilder szHexDigest = new StringBuilder(0x80);
            HMAC_HexFromBytes(szHexDigest, szHexDigest.Capacity, message, message.Length, key, key.Length, (int) hashAlg);
            return szHexDigest.ToString();
        }

        public static string HexFromHex(string messageHex, string keyHex, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            HMAC_HexFromHex(sbHexDigest, sbHexDigest.Capacity, messageHex, keyHex, (int) hashAlg);
            return sbHexDigest.ToString();
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HMAC_Bytes(byte[] digest, int digLen, byte[] lpMessage, int messageLen, byte[] lpKey, int keyLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HMAC_HexFromBytes(StringBuilder szHexDigest, int nOutChars, byte[] lpMessage, int messageLen, byte[] lpKey, int keyLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HMAC_HexFromHex(StringBuilder sbHexDigest, int digLen, string szMessageHex, string szKeyHex, int flags);
    }
}

