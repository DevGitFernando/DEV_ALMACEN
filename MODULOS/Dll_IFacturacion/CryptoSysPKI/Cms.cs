namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Cms
    {
        private const int PKI_CMS_FORMAT_BASE64 = 0x10000;

        private Cms()
        {
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_GetSigDataDigest(StringBuilder sbHexDigestOut, int nDigestLen, string strFileIn, string strX509File, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeDetachedSig(string strFileOut, string strHexDigest, string strCertList, string strRSAPrivateKey, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeEnvData(string strFileOut, string strFileIn, string strCertList, string sSeed, int nSeedLen, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeEnvDataFromString(string strFileOut, string strDataIn, string strCertList, string sSeed, int nSeedLen, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeSigData(string strFileOut, string strFileIn, string strCertList, string strRSAPrivateKey, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeSigDataFromSigValue(string strFileOut, byte[] abSigValue, int nSigLen, byte[] abData, int nDataLen, string strCertList, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_MakeSigDataFromString(string strFileOut, string strDataIn, string strCertList, string strRSAPrivateKey, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_QueryEnvData(StringBuilder sbDataOut, int nDataOutLen, string strFileIn, string strQuery, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_QuerySigData(StringBuilder sbDataOut, int nDataOutLen, string strFileIn, string strQuery, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_ReadEnvData(string strFileOut, string strFileIn, string strX509File, string strRSAPrivateKey, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_ReadEnvDataToString(StringBuilder sbDataOut, int nDataOutLen, string strFileIn, string strX509File, string strRSAPrivateKey, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_ReadSigData(string strFileOut, string strFileIn, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_ReadSigDataToString(StringBuilder sbDataOut, int nDataOutLen, string strFileIn, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int CMS_VerifySigData(string strFileIn, string strCertFile, string strHexDigest, int nOptions);
        public static string GetSigDataDigest(string inputFile, string certFile, bool inputIsBase64)
        {
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            StringBuilder sbHexDigestOut = new StringBuilder(0x80);
            if (CMS_GetSigDataDigest(sbHexDigestOut, sbHexDigestOut.Capacity, inputFile, certFile, nOptions) < 0)
            {
                return string.Empty;
            }
            return sbHexDigestOut.ToString();
        }

        public static int GetSigHashAlgorithm(string inputFile, string certFile, bool inputIsBase64)
        {
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            StringBuilder sbHexDigestOut = new StringBuilder(0x80);
            return CMS_GetSigDataDigest(sbHexDigestOut, sbHexDigestOut.Capacity, inputFile, certFile, nOptions);
        }

        public static int MakeDetachedSig(string outputFile, string hexDigest, string certList, string privateKey, Options options)
        {
            return CMS_MakeDetachedSig(outputFile, hexDigest, certList, privateKey, (int) options);
        }

        public static int MakeEnvData(string outputFile, string inputFile, string certList, Options options)
        {
            int nOptions = (int) options;
            return CMS_MakeEnvData(outputFile, inputFile, certList, "", 0, nOptions);
        }

        public static int MakeEnvData(string outputFile, string inputFile, string certList, CipherAlgorithm cipherAlg, KeyEncrAlgorithm keyEncrAlg, HashAlgorithm hashAlg, EnvDataOptions advOptions)
        {
            int nOptions = (int) ((advOptions | ((EnvDataOptions) ((int) cipherAlg))) | ((EnvDataOptions) ((int) keyEncrAlg)));
            return CMS_MakeEnvData(outputFile, inputFile, certList, "", 0, nOptions);
        }

        public static int MakeEnvDataFromString(string outputFile, string inputData, string certList, Options options)
        {
            int nOptions = (int) options;
            return CMS_MakeEnvDataFromString(outputFile, inputData, certList, "", 0, nOptions);
        }

        public static int MakeEnvDataFromString(string outputFile, string inputData, string certList, CipherAlgorithm cipherAlg, KeyEncrAlgorithm keyEncrAlg, HashAlgorithm hashAlg, EnvDataOptions advOptions)
        {
            int nOptions = (int) ((advOptions | ((EnvDataOptions) ((int) cipherAlg))) | ((EnvDataOptions) ((int) keyEncrAlg)));
            return CMS_MakeEnvDataFromString(outputFile, inputData, certList, "", 0, nOptions);
        }

        public static int MakeSigData(string outputFile, string inputFile, string certList, string privateKey, Options options)
        {
            return CMS_MakeSigData(outputFile, inputFile, certList, privateKey, (int) options);
        }

        public static int MakeSigDataFromSigValue(string outputFile, byte[] sigValue, byte[] contentData, string certList, Options options)
        {
            return CMS_MakeSigDataFromSigValue(outputFile, sigValue, sigValue.Length, contentData, contentData.Length, certList, (int) options);
        }

        public static int MakeSigDataFromString(string outputFile, string inputData, string certList, string privateKey, Options options)
        {
            return CMS_MakeSigDataFromString(outputFile, inputData, certList, privateKey, (int) options);
        }

        public static string QueryEnvData(string inputFile, string query, bool inputIsBase64)
        {
            int num;
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            StringBuilder sbDataOut = new StringBuilder(0);
            if (CMS_QueryEnvData(null, 0, inputFile, query, 0x100000) == 2)
            {
                num = CMS_QueryEnvData(sbDataOut, 0, inputFile, query, nOptions);
                if (num <= 0)
                {
                    return string.Empty;
                }
                sbDataOut = new StringBuilder(num);
                CMS_QueryEnvData(sbDataOut, sbDataOut.Capacity, inputFile, query, nOptions);
            }
            else
            {
                num = CMS_QueryEnvData(sbDataOut, 0, inputFile, query, nOptions);
            }
            if (sbDataOut.Length == 0)
            {
                sbDataOut.Append(num);
            }
            return sbDataOut.ToString();
        }

        public static string QuerySigData(string inputFile, string query, bool inputIsBase64)
        {
            int num;
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            StringBuilder sbDataOut = new StringBuilder(0);
            if (CMS_QuerySigData(null, 0, inputFile, query, 0x100000) == 2)
            {
                num = CMS_QuerySigData(sbDataOut, 0, inputFile, query, nOptions);
                if (num <= 0)
                {
                    return string.Empty;
                }
                sbDataOut = new StringBuilder(num);
                CMS_QuerySigData(sbDataOut, sbDataOut.Capacity, inputFile, query, nOptions);
            }
            else
            {
                num = CMS_QuerySigData(sbDataOut, 0, inputFile, query, nOptions);
            }
            if (sbDataOut.Length == 0)
            {
                sbDataOut.Append(num);
            }
            return sbDataOut.ToString();
        }

        public static int ReadEnvDataToFile(string outputFile, string inputFile, string x509File, string privateKey, Options options)
        {
            return CMS_ReadEnvData(outputFile, inputFile, x509File, privateKey, (int) options);
        }

        public static string ReadEnvDataToString(string inputFile, string x509File, string privateKey, Options options)
        {
            StringBuilder sbDataOut = new StringBuilder(0);
            int capacity = CMS_ReadEnvDataToString(sbDataOut, 0, inputFile, x509File, privateKey, (int) options);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbDataOut = new StringBuilder(capacity);
            CMS_ReadEnvDataToString(sbDataOut, sbDataOut.Capacity, inputFile, x509File, privateKey, (int) options);
            return sbDataOut.ToString();
        }

        public static int ReadSigDataToFile(string outputFile, string inputFile, bool inputIsBase64)
        {
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            return CMS_ReadSigData(outputFile, inputFile, nOptions);
        }

        public static string ReadSigDataToString(string inputFile, bool inputIsBase64)
        {
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            StringBuilder sbDataOut = new StringBuilder(0);
            int capacity = CMS_ReadSigDataToString(sbDataOut, 0, inputFile, nOptions);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbDataOut = new StringBuilder(capacity);
            CMS_ReadSigDataToString(sbDataOut, sbDataOut.Capacity, inputFile, nOptions);
            return sbDataOut.ToString();
        }

        public static int VerifySigData(string inputFile, string certFile, string hexDigest, bool inputIsBase64)
        {
            int nOptions = inputIsBase64 ? 0x10000 : 0;
            return CMS_VerifySigData(inputFile, certFile, hexDigest, nOptions);
        }

        [Flags]
        public enum EnvDataOptions
        {
            FormatBase64 = 0x10000,
            None = 0
        }

        public enum KeyEncrAlgorithm
        {
            Default = 0,
            Rsa_Pkcs1v1_5 = 0
        }

        [Flags]
        public enum Options
        {
            AddSignTime = 0x1000,
            AddSmimeCapabilities = 0x2000,
            AltAlgId = 0x4000000,
            Default = 0,
            ExcludeCerts = 0x100,
            ExcludeData = 0x200,
            FormatBase64 = 0x10000,
            IncludeAttributes = 0x800,
            NoOuter = 0x2000000,
            UseMD5 = 1
        }
    }
}

