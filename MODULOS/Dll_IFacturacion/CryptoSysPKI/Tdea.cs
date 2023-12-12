namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Tdea
    {
        public const int BlockSize = 8;

        private Tdea()
        {
        }

        public static string Decrypt(string inputHex, string keyHex, Mode mode, string ivHex)
        {
            string strMode = MyInternals.ModeString(mode);
            StringBuilder output = new StringBuilder(inputHex.Length);
            if (TDEA_HexMode(output, inputHex, keyHex, 0, strMode, ivHex) != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static byte[] Decrypt(byte[] input, byte[] key, Mode mode, byte[] iv)
        {
            string strMode = MyInternals.ModeString(mode);
            byte[] output = new byte[input.Length];
            if (TDEA_BytesMode(output, input, input.Length, key, 0, strMode, iv) != 0)
            {
                output = new byte[0];
            }
            return output;
        }

        public static string Decrypt(string inputStr, string keyStr, Mode mode, string ivStr, EncodingBase encodingBase)
        {
            string strMode = MyInternals.ModeString(mode);
            StringBuilder output = new StringBuilder(inputStr.Length);
            int num = -999;
            switch (encodingBase)
            {
                case EncodingBase.Base64:
                    num = TDEA_B64Mode(output, inputStr, keyStr, 0, strMode, ivStr);
                    break;

                case EncodingBase.Base16:
                    num = TDEA_HexMode(output, inputStr, keyStr, 0, strMode, ivStr);
                    break;
            }
            if (num != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static string Encrypt(string inputHex, string keyHex, Mode mode, string ivHex)
        {
            string strMode = MyInternals.ModeString(mode);
            StringBuilder output = new StringBuilder(inputHex.Length);
            if (TDEA_HexMode(output, inputHex, keyHex, 1, strMode, ivHex) != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static byte[] Encrypt(byte[] input, byte[] key, Mode mode, byte[] iv)
        {
            string strMode = MyInternals.ModeString(mode);
            byte[] output = new byte[input.Length];
            if (TDEA_BytesMode(output, input, input.Length, key, 1, strMode, iv) != 0)
            {
                output = new byte[0];
            }
            return output;
        }

        public static string Encrypt(string inputStr, string keyStr, Mode mode, string ivStr, EncodingBase encodingBase)
        {
            string strMode = MyInternals.ModeString(mode);
            StringBuilder output = new StringBuilder(inputStr.Length);
            int num = -999;
            switch (encodingBase)
            {
                case EncodingBase.Base64:
                    num = TDEA_B64Mode(output, inputStr, keyStr, 1, strMode, ivStr);
                    break;

                case EncodingBase.Base16:
                    num = TDEA_HexMode(output, inputStr, keyStr, 1, strMode, ivStr);
                    break;
            }
            if (num != 0)
            {
                return string.Empty;
            }
            return output.ToString();
        }

        public static int FileDecrypt(string fileOut, string fileIn, string keyHex, Mode mode, string ivHex)
        {
            string strMode = MyInternals.ModeString(mode);
            return TDEA_File(fileOut, fileIn, Cnv.FromHex(keyHex), 0, strMode, Cnv.FromHex(ivHex));
        }

        public static int FileDecrypt(string fileOut, string fileIn, byte[] key, Mode mode, byte[] iv)
        {
            string strMode = MyInternals.ModeString(mode);
            return TDEA_File(fileOut, fileIn, key, 0, strMode, iv);
        }

        public static int FileEncrypt(string fileOut, string fileIn, string keyHex, Mode mode, string ivHex)
        {
            string strMode = MyInternals.ModeString(mode);
            return TDEA_File(fileOut, fileIn, Cnv.FromHex(keyHex), 1, strMode, Cnv.FromHex(ivHex));
        }

        public static int FileEncrypt(string fileOut, string fileIn, byte[] key, Mode mode, byte[] iv)
        {
            string strMode = MyInternals.ModeString(mode);
            return TDEA_File(fileOut, fileIn, key, 1, strMode, iv);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int TDEA_B64Mode(StringBuilder output, string input, string strB64Key, int bEncrypt, string strMode, string sB64IV);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int TDEA_BytesMode(byte[] output, byte[] input, int nbytes, byte[] key, int bEncrypt, string strMode, byte[] iv);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int TDEA_File(string strFileOut, string strFileIn, byte[] key, int bEncrypt, string strMode, byte[] iv);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int TDEA_HexMode(StringBuilder output, string input, string strHexKey, int bEncrypt, string strMode, string sHexIV);
    }
}

