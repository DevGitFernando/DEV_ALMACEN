namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Rng
    {
        private const int PKI_RNG_SEED_BYTES = 0x40;
        public const int SeedFileSize = 0x40;

        private Rng()
        {
        }

        public static byte[] Bytes(int numBytes)
        {
            byte[] output = new byte[numBytes];
            RNG_Bytes(output, output.Length, null, 0);
            return output;
        }

        public static byte[] Bytes(int numBytes, byte[] arrSeed)
        {
            byte[] output = new byte[numBytes];
            RNG_Bytes(output, output.Length, arrSeed, 0);
            return output;
        }

        public static byte[] Bytes(int numBytes, string seedStr)
        {
            byte[] output = new byte[numBytes];
            byte[] bytes = Encoding.Default.GetBytes(seedStr);
            RNG_Bytes(output, output.Length, bytes, 0);
            return output;
        }

        public static byte[] BytesWithPrompt(int numBytes)
        {
            byte[] lpOutput = new byte[numBytes];
            RNG_BytesWithPrompt(lpOutput, lpOutput.Length, "", 0);
            return lpOutput;
        }

        public static byte[] BytesWithPrompt(int numBytes, string prompt, Strength strength)
        {
            byte[] lpOutput = new byte[numBytes];
            int nOptions = (int) strength;
            RNG_BytesWithPrompt(lpOutput, lpOutput.Length, prompt, nOptions);
            return lpOutput;
        }

        public static bool Initialize(string seedFile)
        {
            return (RNG_Initialize(seedFile, 0) == 0);
        }

        public static bool MakeSeedFile(string seedFile)
        {
            return (RNG_MakeSeedFile(seedFile, "", 0) == 0);
        }

        public static int Number(int lower, int upper)
        {
            return RNG_Number(lower, upper);
        }

        public static byte Octet()
        {
            byte[] output = new byte[1];
            RNG_Bytes(output, output.Length, null, 0);
            return output[0];
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_Bytes(byte[] output, int out_len, byte[] seed, int seedlen);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_BytesWithPrompt(byte[] lpOutput, int nOutputLen, string szPrompt, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_Initialize(string seedFile, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_MakeSeedFile(string seedFile, string prompt, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_Number(int lower, int upper);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_Test(string szFileName, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RNG_UpdateSeedFile(string szSeedFile, int nOptions);
        public static bool Test(string resultFile)
        {
            return (RNG_Test(resultFile, 0) == 0);
        }

        public static bool UpdateSeedFile(string seedFile)
        {
            return (RNG_UpdateSeedFile(seedFile, 0) == 0);
        }

        public enum Strength
        {
            Bits_112 = 0,
            Bits_128 = 1,
            Default = 0
        }
    }
}

