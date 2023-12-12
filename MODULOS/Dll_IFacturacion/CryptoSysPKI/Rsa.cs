namespace Dll_IFacturacion.CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Rsa
    {
        private const int KEYGEN_INDICATE = 0x1000000;
        private const int PKI_KEY_FORMAT_PEM = 0x10000;
        private const int PKI_KEY_FORMAT_SSL = 0x20000;
        private const int PKI_PBE_PBES2 = 0x1000;
        private const int PKI_XML_EXCLPRIVATE = 0x10;
        private const int PKI_XML_HEXBINARY = 0x100;
        private const int PKI_XML_RSAKEYVALUE = 1;
        private const int PRIME_TESTS = 0x40;

        private Rsa()
        {
        }

        public static int CheckKey(string intKeyString)
        {
            return RSA_CheckKey(intKeyString, 0);
        }

        public static int CheckKey(StringBuilder sbKeyString)
        {
            return RSA_CheckKey(sbKeyString.ToString(), 0);
        }

        public static byte[] DecodeDigestForSignature(byte[] data)
        {
            int num = RSA_DecodeMsg(null, 0, data, data.Length, 0x20);
            if (num <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[num];
            if (RSA_DecodeMsg(abOutput, abOutput.Length, data, data.Length, 0x20) < 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] DecodeDigestForSignature(byte[] data, bool getFullDigestInfo)
        {
            int num;
            if (getFullDigestInfo)
            {
                num = 0x2020;
            }
            else
            {
                num = 0x20;
            }
            int num2 = RSA_DecodeMsg(null, 0, data, data.Length, num);
            if (num2 <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[num2];
            if (RSA_DecodeMsg(abOutput, abOutput.Length, data, data.Length, num) < 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] DecodeMsg(byte[] data, EncodeFor method)
        {
            int num = RSA_DecodeMsg(null, 0, data, data.Length, (int) method);
            if (num <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[num];
            if (RSA_DecodeMsg(abOutput, abOutput.Length, data, data.Length, (int) method) < 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] DecodeMsgForEncryption(byte[] data, EME method)
        {
            int num = RSA_DecodeMsg(null, 0, data, data.Length, (int) method);
            if (num <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[num];
            if (RSA_DecodeMsg(abOutput, abOutput.Length, data, data.Length, (int) method) < 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] DecodeMsgIso9796(byte[] data, int keyBits)
        {
            int nOptions = 0x100000 + keyBits;
            if (keyBits <= 0)
            {
                return new byte[0];
            }
            int num2 = RSA_DecodeMsg(null, 0, data, data.Length, nOptions);
            if (num2 <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[num2];
            if (RSA_DecodeMsg(abOutput, abOutput.Length, data, data.Length, nOptions) < 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] EncodeDigestForSignature(int keyBytes, byte[] digest, HashAlgorithm hashAlg)
        {
            int nOptions = (((int) hashAlg) + 0x20) + 0x1000;
            if (keyBytes <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[keyBytes];
            if (RSA_EncodeMsg(abOutput, abOutput.Length, digest, digest.Length, nOptions) != 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] EncodeMsg(int keyBytes, byte[] message, EncodeFor method)
        {
            if (keyBytes <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[keyBytes];
            if (RSA_EncodeMsg(abOutput, abOutput.Length, message, message.Length, (int) method) != 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] EncodeMsgForEncryption(int keyBytes, byte[] message, EME method)
        {
            if (keyBytes <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[keyBytes];
            if (RSA_EncodeMsg(abOutput, abOutput.Length, message, message.Length, (int) method) != 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] EncodeMsgForSignature(int keyBytes, byte[] message, HashAlgorithm hashAlg)
        {
            int nOptions = ((int) hashAlg) + 0x20;
            if (keyBytes <= 0)
            {
                return new byte[0];
            }
            byte[] abOutput = new byte[keyBytes];
            if (RSA_EncodeMsg(abOutput, abOutput.Length, message, message.Length, nOptions) != 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static byte[] EncodeMsgIso9796(byte[] message, int keyBits)
        {
            int nOptions = 0x100000 + keyBits;
            if (keyBits <= 0)
            {
                return new byte[0];
            }
            int num2 = (keyBits + 7) / 8;
            byte[] abOutput = new byte[num2];
            if (RSA_EncodeMsg(abOutput, abOutput.Length, message, message.Length, nOptions) != 0)
            {
                return new byte[0];
            }
            return abOutput;
        }

        public static string FromXMLString(string xmlString, bool excludePrivateParams)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int nOptions = excludePrivateParams ? 0x10 : 0;
            int capacity = RSA_FromXMLString(sbOutput, 0, xmlString, nOptions);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            RSA_FromXMLString(sbOutput, sbOutput.Capacity, xmlString, nOptions);
            return sbOutput.ToString();
        }

        public static int GetPrivateKeyFromPFX(string outputFile, string pfxFile)
        {
            return RSA_GetPrivateKeyFromPFX(outputFile, pfxFile, 0);
        }

        public static StringBuilder GetPublicKeyFromCert(string certFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = RSA_GetPublicKeyFromCert(sbOutput, 0, certFile, 0);
            if (capacity > 0)
            {
                sbOutput = new StringBuilder(capacity);
                RSA_GetPublicKeyFromCert(sbOutput, sbOutput.Capacity, certFile, 0);
            }
            return sbOutput;
        }

        public static int KeyBits(string strRsaKey)
        {
            return RSA_KeyBits(strRsaKey);
        }

        public static int KeyBits(StringBuilder sbRsaKey)
        {
            return RSA_KeyBits(sbRsaKey.ToString());
        }

        public static int KeyBytes(string strRsaKey)
        {
            return RSA_KeyBytes(strRsaKey);
        }

        public static int KeyBytes(StringBuilder sbRsaKey)
        {
            return RSA_KeyBytes(sbRsaKey.ToString());
        }

        public static int KeyHashCode(string intKeyString)
        {
            return RSA_KeyHashCode(intKeyString);
        }

        public static int KeyHashCode(StringBuilder sbKeyString)
        {
            return RSA_KeyHashCode(sbKeyString.ToString());
        }

        public static int KeyMatch(string privateKey, string publicKey)
        {
            return RSA_KeyMatch(privateKey, publicKey);
        }

        public static int KeyMatch(StringBuilder sbPrivateKey, StringBuilder sbPublicKey)
        {
            return RSA_KeyMatch(sbPrivateKey.ToString(), sbPublicKey.ToString());
        }

        public static int MakeKeys(string publicKeyFile, string privateKeyFile, int bits, PublicExponent exponent, int iterCount, string password, PbeOptions cryptOption, bool showProgress)
        {
            int nOptions = (showProgress ? 0x1000000 : 0) | (int)cryptOption;
            return RSA_MakeKeys(publicKeyFile, privateKeyFile, bits, (int) exponent, 0x40, iterCount, password, null, 0, nOptions);
        }

        public static int MakeKeys(string publicKeyFile, string privateKeyFile, int bits, PublicExponent exponent, int iterCount, string password, PbeOptions cryptOption, bool showProgress, byte[] seedBytes)
        {
            int nOptions = (showProgress ? 0x1000000 : 0) | (int)cryptOption;
            return RSA_MakeKeys(publicKeyFile, privateKeyFile, bits, (int) exponent, 0x40, iterCount, password, seedBytes, seedBytes.Length, nOptions);
        }

        public static int MakeKeys(string publicKeyFile, string privateKeyFile, int bits, PublicExponent exponent, int iterCount, string password, CipherAlgorithm cipherAlg, HashAlgorithm hashAlg, Format fileFormat, bool showProgress)
        {
            int nOptions = (int) (((((CipherAlgorithm) 0x1000) | cipherAlg) | ((CipherAlgorithm) ((int) hashAlg))) | ((CipherAlgorithm) ((int) fileFormat)));
            if (showProgress)
            {
                nOptions |= 0x1000000;
            }
            return RSA_MakeKeys(publicKeyFile, privateKeyFile, bits, (int) exponent, 0x40, iterCount, password, null, 0, nOptions);
        }

        public static byte[] RawPrivate(byte[] data, string privateKeyStr)
        {
            byte[] destinationArray = new byte[data.Length];
            Array.Copy(data, destinationArray, data.Length);
            if (RSA_RawPrivate(destinationArray, destinationArray.Length, privateKeyStr, 0) != 0)
            {
                return new byte[0];
            }
            return destinationArray;
        }

        public static byte[] RawPrivate(byte[] data, string privateKeyStr, int options)
        {
            byte[] destinationArray = new byte[data.Length];
            Array.Copy(data, destinationArray, data.Length);
            if (RSA_RawPrivate(destinationArray, destinationArray.Length, privateKeyStr, options) != 0)
            {
                return new byte[0];
            }
            return destinationArray;
        }

        public static byte[] RawPublic(byte[] data, string publicKeyStr)
        {
            byte[] destinationArray = new byte[data.Length];
            Array.Copy(data, destinationArray, data.Length);
            if (RSA_RawPublic(destinationArray, destinationArray.Length, publicKeyStr, 0) != 0)
            {
                return new byte[0];
            }
            return destinationArray;
        }

        public static byte[] RawPublic(byte[] data, string publicKeyStr, int options)
        {
            byte[] destinationArray = new byte[data.Length];
            Array.Copy(data, destinationArray, data.Length);
            if (RSA_RawPublic(destinationArray, destinationArray.Length, publicKeyStr, options) != 0)
            {
                return new byte[0];
            }
            return destinationArray;
        }

        public static StringBuilder ReadEncPrivateKey(string privateKeyFile, string password)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = RSA_ReadEncPrivateKey(sbOutput, 0, privateKeyFile, password, 0);
            if (capacity > 0)
            {
                sbOutput = new StringBuilder(capacity);
                RSA_ReadEncPrivateKey(sbOutput, sbOutput.Capacity, privateKeyFile, password, 0);
            }
            return sbOutput;
        }

        public static StringBuilder ReadPrivateKeyInfo(string prikeyinfoFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = RSA_ReadPrivateKeyInfo(sbOutput, 0, prikeyinfoFile, 0);
            if (capacity > 0)
            {
                sbOutput = new StringBuilder(capacity);
                RSA_ReadPrivateKeyInfo(sbOutput, sbOutput.Capacity, prikeyinfoFile, 0);
            }
            return sbOutput;
        }

        public static StringBuilder ReadPublicKey(string keyFile)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = RSA_ReadPublicKey(sbOutput, 0, keyFile, 0);
            if (capacity > 0)
            {
                sbOutput = new StringBuilder(capacity);
                RSA_ReadPublicKey(sbOutput, sbOutput.Capacity, keyFile, 0);
            }
            return sbOutput;
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_CheckKey(string strKeyString, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_DecodeMsg(byte[] abOutput, int nOutputLen, byte[] abInput, int nInputLen, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_EncodeMsg(byte[] abOutput, int nOutputLen, byte[] abMessage, int nMsgLen, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_FromXMLString(StringBuilder sbOutput, int nOutputLen, string szXmlString, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_GetPrivateKeyFromPFX(string strOutputFile, string strPFXFile, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_GetPublicKeyFromCert(StringBuilder sbOutput, int nOutputLen, string strCertFile, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_KeyBits(string strRsaKey64);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_KeyBytes(string strRsaKey64);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_KeyHashCode(string szKeyString);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_KeyMatch(string szPrivateKey, string szPublicKey);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_MakeKeys(string strPubKeyFile, string strPVKFile, int nBits, int nExpFermat, int nCount, int nTests, string strPassword, byte[] lpSeed, int nSeedLen, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_RawPrivate(byte[] abData, int nDataLen, string privateKeyStr, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_RawPublic(byte[] abData, int nDataLen, string publicKeyStr, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_ReadEncPrivateKey(StringBuilder sbOutput, int nOutputLen, string strPVKFile, string strPassword, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_ReadPrivateKeyInfo(StringBuilder sbOutput, int nOutputLen, string strKeyFile, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_ReadPublicKey(StringBuilder sbOutput, int nOutputLen, string strKeyFile, int flags);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_SaveEncPrivateKey(string strFileOut, string strKeyString, int nCount, string strPassword, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_SavePrivateKeyInfo(string strFileOut, string strKeyString, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_SavePublicKey(string strFileOut, string strKeyString, int nOptions);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int RSA_ToXMLString(StringBuilder sbOutput, int nOutputLen, string szKeyString, int nOptions);
        public static int SaveEncPrivateKey(string outputFile, string privateKey, int iterationCount, string password, PbeOptions pbeOption, Format format)
        {
            int nOptions = (int) (pbeOption | ((PbeOptions) ((int) format)));
            return RSA_SaveEncPrivateKey(outputFile, privateKey, iterationCount, password, nOptions);
        }

        public static int SaveEncPrivateKey(string outputFile, string privateKey, int iterationCount, string password, CipherAlgorithm cipherAlg, HashAlgorithm hashAlg, Format format)
        {
            int nOptions = (int) (((((CipherAlgorithm) 0x1000) | cipherAlg) | ((CipherAlgorithm) ((int) hashAlg))) | ((CipherAlgorithm) ((int) format)));
            return RSA_SaveEncPrivateKey(outputFile, privateKey, iterationCount, password, nOptions);
        }

        public static int SavePrivateKeyInfo(string outputFile, string privateKey, Format format)
        {
            return RSA_SavePrivateKeyInfo(outputFile, privateKey, (int) format);
        }

        public static int SavePublicKey(string outputFile, string publicKey, Format format)
        {
            return RSA_SavePublicKey(outputFile, publicKey, (int) format);
        }

        public static string ToXMLString(string intKeyString, XmlOptions options)
        {
            StringBuilder sbOutput = new StringBuilder(0);
            int capacity = RSA_ToXMLString(sbOutput, 0, intKeyString, (int) options);
            if (capacity <= 0)
            {
                return string.Empty;
            }
            sbOutput = new StringBuilder(capacity);
            RSA_ToXMLString(sbOutput, sbOutput.Capacity, intKeyString, (int) options);
            return sbOutput.ToString();
        }

        public enum EME
        {
            OAEP = 0x10,
            PKCSv1_5 = 0
        }

        public enum EncodeFor
        {
            Encryption = 0,
            Encryption_OAEP = 0x10,
            Signature = 0x20
        }

        public enum Format
        {
            Binary = 0,
            Default = 0,
            PEM = 0x10000,
            SSL = 0x20000
        }

        public enum PbeOptions
        {
            Default = 0,
            PbeWithMD2AndDES_CBC = 2,
            PbeWithMD5AndDES_CBC = 1,
            PbeWithSHA1AndDES_CBC = 3,
            PbeWithSHAAnd_KeyTripleDES_CBC = 0,
            Pkcs5PBES2_des_EDE3_CBC = 4
        }

        public enum PublicExponent
        {
            Exp_EQ_3,
            Exp_EQ_5,
            Exp_EQ_17,
            Exp_EQ_257,
            Exp_EQ_65537
        }

        [Flags]
        public enum XmlOptions
        {
            ExcludePrivateParams = 0x10,
            ForceRSAKeyValue = 1,
            HexBinaryFormat = 0x100
        }
    }
}

