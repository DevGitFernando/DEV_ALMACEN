namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Cipher
    {
        private Cipher()
        {
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CIPHER_Bytes(int fEncrypt, byte[] output, byte[] input, int nbytes, byte[] key, byte[] iv, string algAndMode, int options);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CIPHER_File(int fEncrypt, string strFileOut, string strFileIn, byte[] key, byte[] iv, string algAndMode, int options);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CIPHER_Hex(int fEncrypt, StringBuilder output, int outlen, string input, string strHexKey, string sHexIV, string algAndMode, int options);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CIPHER_KeyUnwrap(byte[] output, int nOutBytes, byte[] data, int nDataLen, byte[] kek, int nKekLen, int options);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CIPHER_KeyWrap(byte[] output, int nOutBytes, byte[] data, int nDataLen, byte[] kek, int nKekLen, int options);
        public static byte[] Decrypt(byte[] input, byte[] key, byte[] iv, CipherAlgorithm cipherAlg, Mode mode)
        {
            byte[] output = new byte[input.Length];
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            if (CIPHER_Bytes(0, output, input, input.Length, key, iv, "", options) != 0)
            {
                output = new byte[0];
            }
            return output;
        }

        public static string Decrypt(string inputHex, string keyHex, string ivHex, CipherAlgorithm cipherAlg, Mode mode)
        {
            StringBuilder output = new StringBuilder(inputHex.Length);
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            if (CIPHER_Hex(0, output, output.Length, inputHex, keyHex, ivHex, "", options) != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static string Encrypt(string inputHex, string keyHex, string ivHex, CipherAlgorithm cipherAlg, Mode mode)
        {
            StringBuilder output = new StringBuilder(inputHex.Length);
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            if (CIPHER_Hex(1, output, output.Length, inputHex, keyHex, ivHex, "", options) != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv, CipherAlgorithm cipherAlg, Mode mode)
        {
            byte[] output = new byte[input.Length];
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            if (CIPHER_Bytes(1, output, input, input.Length, key, iv, "", options) != 0)
            {
                output = new byte[0];
            }
            return output;
        }

        public static int FileDecrypt(string fileOut, string fileIn, byte[] key, byte[] iv, CipherAlgorithm cipherAlg, Mode mode)
        {
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            return CIPHER_File(0, fileOut, fileIn, key, iv, "", options);
        }

        public static int FileDecrypt(string fileOut, string fileIn, string keyHex, string ivHex, CipherAlgorithm cipherAlg, Mode mode)
        {
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            return CIPHER_File(0, fileOut, fileIn, Cnv.FromHex(keyHex), Cnv.FromHex(ivHex), "", options);
        }

        public static int FileEncrypt(string fileOut, string fileIn, string keyHex, string ivHex, CipherAlgorithm cipherAlg, Mode mode)
        {
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            return CIPHER_File(1, fileOut, fileIn, Cnv.FromHex(keyHex), Cnv.FromHex(ivHex), "", options);
        }

        public static int FileEncrypt(string fileOut, string fileIn, byte[] key, byte[] iv, CipherAlgorithm cipherAlg, Mode mode)
        {
            int options = (int) (cipherAlg | ((CipherAlgorithm) ((int) mode)));
            return CIPHER_File(1, fileOut, fileIn, key, iv, "", options);
        }

        public static byte[] KeyUnwrap(byte[] data, byte[] kek, CipherAlgorithm cipherAlg)
        {
            int options = (int) cipherAlg;
            int num2 = CIPHER_KeyUnwrap(null, 0, data, data.Length, kek, kek.Length, options);
            if (num2 <= 0)
            {
                return new byte[0];
            }
            byte[] output = new byte[num2];
            if (CIPHER_KeyUnwrap(output, output.Length, data, data.Length, kek, kek.Length, options) <= 0)
            {
                output = new byte[0];
            }
            return output;
        }

        public static byte[] KeyWrap(byte[] data, byte[] kek, CipherAlgorithm cipherAlg)
        {
            int options = (int) cipherAlg;
            int num2 = CIPHER_KeyWrap(null, 0, data, data.Length, kek, kek.Length, options);
            if (num2 <= 0)
            {
                return new byte[0];
            }
            byte[] output = new byte[num2];
            if (CIPHER_KeyWrap(output, output.Length, data, data.Length, kek, kek.Length, options) <= 0)
            {
                output = new byte[0];
            }
            return output;
        }
    }
}

