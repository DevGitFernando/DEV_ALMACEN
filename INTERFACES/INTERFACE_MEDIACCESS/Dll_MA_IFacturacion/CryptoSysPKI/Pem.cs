namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;

    public class Pem
    {
        private Pem()
        {
        }

        public static int FileFromBinFile(string fileToMake, string fileIn, string header, int lineLen)
        {
            return PEM_FileFromBinFile(fileToMake, fileIn, header, lineLen);
        }

        public static int FileToBinFile(string fileToMake, string fileIn)
        {
            return PEM_FileToBinFile(fileToMake, fileIn);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PEM_FileFromBinFile(string szOutputFile, string szFileIn, string szHeader, int nLineLen);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PEM_FileToBinFile(string szOutputFile, string szFileIn);
    }
}

