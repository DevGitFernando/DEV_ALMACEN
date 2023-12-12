namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Hash
    {
        private const int HASH_MODE_TEXT = 0x10000;

        private Hash()
        {
        }

        public static byte[] BytesFromBytes(byte[] message, HashAlgorithm hashAlg)
        {
            byte[] digest = new byte[MyInternals.HashBytes(hashAlg)];
            HASH_Bytes(digest, digest.Length, message, message.Length, (int) hashAlg);
            return digest;
        }

        public static byte[] BytesFromFile(string fileName, HashAlgorithm hashAlg)
        {
            byte[] digest = new byte[MyInternals.HashBytes(hashAlg)];
            HASH_File(digest, digest.Length, fileName, (int) hashAlg);
            return digest;
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HASH_Bytes(byte[] digest, int digLen, byte[] aMessage, int messageLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HASH_File(byte[] digest, int digLen, string strFileName, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HASH_HexFromBytes(StringBuilder sbHexDigest, int digLen, byte[] aMessage, int messageLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HASH_HexFromFile(StringBuilder sbHexDigest, int digLen, string strFileName, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int HASH_HexFromHex(StringBuilder sbHexDigest, int digLen, string strMessageHex, int flags);
        public static string HexFromBytes(byte[] message, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            HASH_HexFromBytes(sbHexDigest, sbHexDigest.Capacity, message, message.Length, (int) hashAlg);
            return sbHexDigest.ToString();
        }

        public static string HexFromFile(string fileName, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            HASH_HexFromFile(sbHexDigest, sbHexDigest.Capacity, fileName, (int) hashAlg);
            return sbHexDigest.ToString();
        }

        public static string HexFromHex(string messageHex, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            HASH_HexFromHex(sbHexDigest, sbHexDigest.Capacity, messageHex, (int) hashAlg);
            return sbHexDigest.ToString();
        }

        public static string HexFromString(string message, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            byte[] bytes = Encoding.Default.GetBytes(message);
            HASH_HexFromBytes(sbHexDigest, sbHexDigest.Capacity, bytes, bytes.Length, (int) hashAlg);
            return sbHexDigest.ToString();
        }

        public static string HexFromTextFile(string fileName, HashAlgorithm hashAlg)
        {
            StringBuilder sbHexDigest = new StringBuilder(0x80);
            HASH_HexFromFile(sbHexDigest, sbHexDigest.Capacity, fileName, ((int) hashAlg) | 0x10000);
            return sbHexDigest.ToString();
        }
    }
}

