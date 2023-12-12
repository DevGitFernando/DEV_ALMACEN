namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Cnv
    {
        private Cnv()
        {
        }

        public static string Base64Filter(string s)
        {
            if (s == null)
            {
                return string.Empty;
            }
            StringBuilder sbOutput = new StringBuilder(s.Length);
            int length = CNV_B64Filter(sbOutput, s, s.Length);
            if (length <= 0)
            {
                return string.Empty;
            }
            return sbOutput.ToString(0, length);
        }

        public static int CheckUTF8(string s)
        {
            return CNV_CheckUTF8(s);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_B64Filter(StringBuilder sbOutput, string input, int len);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_B64StrFromBytes(StringBuilder sbOutput, int nOutChars, byte[] input, int nbytes);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_BytesFromB64Str(byte[] output, int out_len, string input);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_BytesFromHexStr(byte[] output, int out_len, string input);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_CheckUTF8(string s);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_HexFilter(StringBuilder sboutput, string input, int len);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CNV_HexStrFromBytes(StringBuilder sboutput, int out_len, byte[] input, int in_len);
        public static byte[] FromHex(string s)
        {
            if (s == null)
            {
                return new byte[0];
            }
            int num = s.Length / 2;
            byte[] output = new byte[num];
            num = CNV_BytesFromHexStr(output, num, s);
            if (output.Length != num)
            {
                byte[] destinationArray = new byte[num];
                Array.Copy(output, destinationArray, num);
                output = destinationArray;
            }
            return output;
        }

        public static string HexFilter(string s)
        {
            if (s == null)
            {
                return string.Empty;
            }
            StringBuilder sboutput = new StringBuilder(s.Length);
            int length = CNV_HexFilter(sboutput, s, s.Length);
            if (length <= 0)
            {
                return string.Empty;
            }
            return sboutput.ToString(0, length);
        }

        public static string StringFromHex(string s)
        {
            byte[] bytes = FromHex(s);
            if (bytes.Length == 0)
            {
                return string.Empty;
            }
            return Encoding.Default.GetString(bytes);
        }

        public static string ToHex(byte[] binaryData)
        {
            int length = binaryData.Length;
            int capacity = 2 * length;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder sboutput = new StringBuilder(capacity);
            capacity = CNV_HexStrFromBytes(sboutput, capacity, binaryData, length);
            return sboutput.ToString(0, capacity);
        }
    }
}

