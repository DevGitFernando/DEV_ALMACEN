namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;

    public class Pfx
    {
        private const int PKI_PFX_NO_PRIVKEY = 0x10;

        private Pfx()
        {
        }

        public static int MakeFile(string fileToMake, string certFile, string privateKeyFile, string password, string friendlyName, bool excludePrivateKey)
        {
            return PFX_MakeFile(fileToMake, certFile, privateKeyFile, password, friendlyName, excludePrivateKey ? 0x10 : 0);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PFX_MakeFile(string strFileOut, string strCertFile, string strKeyFile, string strPassword, string strFriendlyName, int options);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PFX_VerifySig(string strFileName, string strPassword, int options);
        public static bool SignatureIsValid(string fileName, string password)
        {
            if (PFX_VerifySig(fileName, password, 0) != 0)
            {
                return false;
            }
            return true;
        }
    }
}

