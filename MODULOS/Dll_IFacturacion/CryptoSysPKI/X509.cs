namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class X509
    {
        private X509()
        {
        }

        public static string CertExpiresOn(string certFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_CertExpiresOn(certFile, sbOutput, 0, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_CertExpiresOn(certFile, sbOutput, sbOutput.Capacity, 0);
            return sbOutput.ToString();
        }

        public static string CertIssuedOn(string certFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_CertIssuedOn(certFile, sbOutput, 0, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_CertIssuedOn(certFile, sbOutput, sbOutput.Capacity, 0);
            return sbOutput.ToString();
        }

        public static string CertIssuerName(string certFile, string delimiter)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_CertIssuerName(certFile, sbOutput, 0, delimiter, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_CertIssuerName(certFile, sbOutput, sbOutput.Capacity, delimiter, 0);
            return sbOutput.ToString();
        }

        public static bool CertIsValidNow(string certFile)
        {
            return (X509_CertIsValidNow(certFile, 0) == 0);
        }

        public static int CertRequest(string reqFile, string privateKeyFile, string distName, string password, Options options)
        {
            return X509_CertRequest(reqFile, privateKeyFile, distName, "", password, (int) options);
        }

        public static string CertSerialNumber(string certFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_CertSerialNumber(certFile, sbOutput, 0, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_CertSerialNumber(certFile, sbOutput, sbOutput.Capacity, 0);
            return sbOutput.ToString();
        }

        public static string CertSubjectName(string certFile, string delimiter)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_CertSubjectName(certFile, sbOutput, 0, delimiter, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_CertSubjectName(certFile, sbOutput, sbOutput.Capacity, delimiter, 0);
            return sbOutput.ToString();
        }

        public static string CertThumb(string certFile, HashAlgorithm hashAlg)
        {
            StringBuilder sbHash = new StringBuilder(0);
            int capacity = X509_CertThumb(certFile, sbHash, 0, (int) hashAlg);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbHash = new StringBuilder(capacity);
            X509_CertThumb(certFile, sbHash, sbHash.Capacity, (int) hashAlg);
            return sbHash.ToString();
        }

        public static int GetCertFromP7Chain(string outputFile, string inputFile, int index)
        {
            return X509_GetCertFromP7Chain(outputFile, inputFile, index, 0);
        }

        public static int GetCertFromPFX(string outputFile, string inputFile)
        {
            return X509_GetCertFromPFX(outputFile, inputFile, "", 0);
        }

        public static string HashIssuerAndSN(string certFile, HashAlgorithm algorithm)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = X509_HashIssuerAndSN(certFile, sbOutput, 0, (int) algorithm);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            X509_HashIssuerAndSN(certFile, sbOutput, sbOutput.Capacity, (int) algorithm);
            return sbOutput.ToString();
        }

        public static int KeyUsageFlags(string certFile)
        {
            return X509_KeyUsageFlags(certFile);
        }

        public static int MakeCert(string certFile, string issuerCert, string subjectPubkeyFile, string issuerPvkInfoFile, int certNum, int yearsValid, string distName, string extensions, KeyUsageOptions keyUsageOptions, string password, Options options)
        {
            return X509_MakeCert(certFile, issuerCert, subjectPubkeyFile, issuerPvkInfoFile, certNum, yearsValid, distName, extensions, (int) keyUsageOptions, password, (int) options);
        }

        public static int MakeCertSelf(string certFile, string privateKeyFile, int certNum, int yearsValid, string distName, string extensions, KeyUsageOptions keyUsageOptions, string password, Options options)
        {
            return X509_MakeCertSelf(certFile, privateKeyFile, certNum, yearsValid, distName, extensions, (int) keyUsageOptions, password, (int) options);
        }

        public static string QueryCert(string inputFile, string query)
        {
            return QueryCert(inputFile, query, Options.None);
        }

        public static string QueryCert(string inputFile, string query, Options options)
        {
            int num;
            int nOptions = (int) options;
            StringBuilder szDataOut = new StringBuilder(0);
            if (X509_QueryCert(null, 0, inputFile, query, 0x100000) == 2)
            {
                num = X509_QueryCert(szDataOut, 0, inputFile, query, nOptions);
                if (num <= 0)
                {
                    return string.Empty;
                }
                szDataOut = new StringBuilder(num);
                X509_QueryCert(szDataOut, szDataOut.Capacity, inputFile, query, nOptions);
            }
            else
            {
                num = X509_QueryCert(szDataOut, 0, inputFile, query, nOptions);
            }
            if (szDataOut.Length == 0)
            {
                szDataOut.Append(num);
            }
            return szDataOut.ToString();
        }

        public static string ReadStringFromFile(string certFile)
        {
            StringBuilder szOutput = new StringBuilder(0);
            int capacity = X509_ReadStringFromFile(szOutput, 0, certFile, 0);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            szOutput = new StringBuilder(capacity);
            X509_ReadStringFromFile(szOutput, szOutput.Capacity, certFile, 0);
            return szOutput.ToString();
        }

        public static int SaveFileFromString(string newCertFile, string certString, bool inPEMFormat)
        {
            int nOptions = inPEMFormat ? 0x10000 : 0x20000;
            return X509_SaveFileFromString(newCertFile, certString, nOptions);
        }

        public static int VerifyCert(string certToVerify, string issuerCert)
        {
            return X509_VerifyCert(certToVerify, issuerCert, 0);
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertExpiresOn(string strCertFile, StringBuilder sbOutput, int nOutputLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertIssuedOn(string strCertFile, StringBuilder sbOutput, int nOutputLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertIssuerName(string strCertFile, StringBuilder sbOutput, int nOutputLen, string strDelim, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertIsValidNow(string strCertFile, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertRequest(string reqfile, string epkfile, string distName, string reserved, string password, int optionFlags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertSerialNumber(string strCertFile, StringBuilder sbOutput, int nOutputLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertSubjectName(string strCertFile, StringBuilder sbOutput, int nOutputLen, string strDelim, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_CertThumb(string strCertFile, StringBuilder sbHash, int hashlen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_GetCertFromP7Chain(string strOutputFile, string strP7cFile, int nIndex, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_GetCertFromPFX(string strOutputFile, string strPfxFile, string strReserved, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_HashIssuerAndSN(string strCertFile, StringBuilder sbOutput, int nOutputLen, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_KeyUsageFlags(string szCertFile);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_MakeCert(string certfile, string issuerCert, string subjectPubkeyFile, string issuerPvkInfoFile, int certnum, int yearsvalid, string distName, string email, int keyUsageFlags, string password, int optionFlags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_MakeCertSelf(string certfile, string epkfile, int certnum, int yearsvalid, string distName, string email, int keyUsageFlags, string password, int optionFlags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_QueryCert(StringBuilder szDataOut, int nOutChars, string szCertFile, string szQuery, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_ReadStringFromFile(StringBuilder szOutput, int nOutChars, string szCertFile, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_SaveFileFromString(string szNewCertFile, string szCertString, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int X509_VerifyCert(string strCertToVerify, string strIssuerCert, int flags);

        [Flags]
        public enum KeyUsageOptions
        {
            CrlSign = 0x40,
            DataEncipherment = 8,
            DecipherOnly = 0x100,
            DigitalSignature = 1,
            EncipherOnly = 0x80,
            KeyAgreement = 0x10,
            KeyCertSign = 0x20,
            KeyEncipherment = 4,
            None = 0,
            NonRepudiation = 2
        }

        [Flags]
        public enum Options
        {
            AuthKeyId = 0x1000000,
            FormatBinary = 0x20000,
            FormatPem = 0x10000,
            Latin1 = 0x400000,
            NoBasicConstraints = 0x2000000,
            None = 0,
            RequestKludge = 0x100000,
            SetAsCA = 0x4000000,
            SigAlg_Md2WithRSAEncryption = 2,
            SigAlg_Md5WithRSAEncryption = 1,
            SigAlg_Sha1WithRSAEncryption = 0,
            SigAlg_Sha224WithRSAEncryption = 6,
            SigAlg_Sha256WithRSAEncryption = 3,
            SigAlg_Sha384WithRSAEncryption = 4,
            SigAlg_Sha512WithRSAEncryption = 5,
            UTF8String = 0x800000,
            VersionOne = 0x8000000
        }
    }
}

