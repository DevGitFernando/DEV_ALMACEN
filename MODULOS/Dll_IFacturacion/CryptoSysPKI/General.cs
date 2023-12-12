namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class General
    {
        private const int PKI_GEN_PLATFORM = 0x40;

        private General()
        {
        }

        public static string CompileTime()
        {
            StringBuilder sbTimestamp = new StringBuilder(0x40);
            PKI_CompileTime(sbTimestamp, sbTimestamp.Capacity);
            return sbTimestamp.ToString();
        }

        public static int ErrorCode()
        {
            return PKI_ErrorCode();
        }

        public static string ErrorLookup(int errCode)
        {
            StringBuilder sbErrMsg = new StringBuilder(0x80);
            if (PKI_ErrorLookup(sbErrMsg, sbErrMsg.Capacity, errCode) > 0)
            {
                return sbErrMsg.ToString();
            }
            return string.Empty;
        }

        public static int IsWin64()
        {
            return PKI_LicenceType(0x40);
        }

        public static string LastError()
        {
            StringBuilder sbErrMsg = new StringBuilder(0);
            sbErrMsg = new StringBuilder(PKI_LastError(null, 0));
            PKI_LastError(sbErrMsg, sbErrMsg.Capacity);
            return sbErrMsg.ToString();
        }

        public static char LicenceType()
        {
            return (char) PKI_LicenceType(0);
        }

        public static string ModuleName()
        {
            StringBuilder sbModuleName = new StringBuilder(0);
            sbModuleName = new StringBuilder(PKI_ModuleName(sbModuleName, 0, 0));
            PKI_ModuleName(sbModuleName, sbModuleName.Capacity, 0);
            return sbModuleName.ToString();
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_CompileTime(StringBuilder sbTimestamp, int nLen);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_ErrorCode();
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_ErrorLookup(StringBuilder sbErrMsg, int nMsgLen, int nErrCode);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_LastError(StringBuilder sbErrMsg, int nMsgLen);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_LicenceType(int reserved);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_ModuleName(StringBuilder sbModuleName, int nLen, int reserved);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_PowerUpTests(int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PKI_Version(byte[] dummyMajor, byte[] dummyMinor);
        public static string Platform()
        {
            StringBuilder sbModuleName = new StringBuilder(0);
            sbModuleName = new StringBuilder(PKI_ModuleName(sbModuleName, 0, 0x40));
            PKI_ModuleName(sbModuleName, sbModuleName.Capacity, 0x40);
            return sbModuleName.ToString();
        }

        public static int PowerUpTests()
        {
            return PKI_PowerUpTests(0);
        }

        public static int Version()
        {
            return PKI_Version(null, null);
        }
    }
}

