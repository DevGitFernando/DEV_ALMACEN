using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using CryptoSysPKI = Dll_IFacturacion.CryptoSysPKI;
using Dll_IFacturacion.CryptoSysPKI;

using SC_SolutionsSystem.FuncionesGenerales; 
namespace Dll_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsSelloDigital
    {
        string sRutaCertificado = "";
        string sRutaPrivateKey = "";
        string sPasswordSello = "";

        string sNoSerie = "";
        string sNoSerieDecimal = "";
        string sPublicKey = "";
        string sNombreEmisor = "";
        string sCertificado = ""; 

        StringBuilder sbPublicKey;
        StringBuilder sbPassword;
        StringBuilder sbPrivateKey;


        string sDato = "";
        string sFechaExpiraCertificado = "";
        string sFechaUsoCertificado = ""; 
        bool bEsValido = false;
        string sAsunto = "";

        DateTime dFechaInicial = DateTime.Now; 
        DateTime dFechaFinal = DateTime.Now;


        X509Certificate2 _CertificadoPublico; 

        //public clsSelloDigital()
        //{ 
        //}

        public clsSelloDigital(string Certificado, string LlavePrivada, string Password)
        {
            sRutaCertificado = Certificado;
            sRutaPrivateKey = LlavePrivada;
            sPasswordSello = Password; 
        }

        public void GetDatos()
        {
            string sDatos = "";
            bool bFirma = true; 

            _CertificadoPublico = new X509Certificate2(sRutaCertificado);


            sDatos = FirmarSH1withRSA("");
            bFirma = VerificarFirmaSH1withRSA(sDato, ""); 

            //byte[] bytesTexto = System.Text.Encoding.UTF8.GetBytes(pDato);
            //byte[] bytesFirmados = Convert.FromBase64String(pFirma);
        }

        public string FirmarSH1withRSA(string CadenaOriginal)
        {

            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(); // = (RSACryptoServiceProvider)_CertificadoPublico.PrivateKey;
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();

            
            byte[] bytesFirmados = RSA.SignData(System.Text.Encoding.UTF8.GetBytes(CadenaOriginal), hasher);
            string sFirma = Convert.ToBase64String(_CertificadoPublico.GetSerialNumber());
            sFirma = _CertificadoPublico.SubjectName.Name ;

            return sFirma;

        }

        public bool VerificarFirmaSH1withRSA(string CadenaOriginal, string Firma)
        {

            byte[] bytesTexto = System.Text.Encoding.UTF8.GetBytes(CadenaOriginal);
            byte[] bytesFirmados = Convert.FromBase64String(Firma);
            bool bRegresa = false;

            RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)_CertificadoPublico.PublicKey.Key;
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            bRegresa = RSA.VerifyData(bytesTexto, hasher, bytesFirmados); 

            return bRegresa;

        }

        public void ObtenerDatos()
        {
            sFechaExpiraCertificado = X509.CertExpiresOn(sRutaCertificado);
            sFechaUsoCertificado = X509.CertIssuedOn(sRutaCertificado);
            sNombreEmisor = X509.CertIssuerName(sRutaCertificado, ";");
            // bEsValido = X509.CertIsValidNow(sRutaCertificado); 

            sNoSerie = X509.CertSerialNumber(sRutaCertificado);
            sCertificado = X509.ReadStringFromFile(sRutaCertificado); 

            // uint num = uint.Parse(sNoSerie, System.Globalization.NumberStyles.AllowHexSpecifier);

            basGenerales Fg = new basGenerales();
            sNoSerieDecimal = ""; 
            for (int i = 0; i <= (sNoSerie.Length / 2)-1; i++)
            {
                sNoSerieDecimal += Fg.Char(Convert.ToInt32(sNoSerie.Substring(i * 2, 2), 16));
            }
            

            ////sAsunto = X509.CertSubjectName(sRutaCertificado, ";");
            ////////// X509.CertThumb(sRutaCertificado, HashAlgorithm.Md5); 

            //////////X509.ReadStringFromFile();

            ////X509Certificate2 x = new X509Certificate2(sRutaCertificado, sPasswordSello);
            ////sNoSerie = x.SerialNumber;
            ////sPublicKey = x.GetPublicKeyString();
            ////sNombreEmisor = x.IssuerName.Decode(X500DistinguishedNameFlags.UseUTF8Encoding); 

            ////dFechaInicial = x.NotBefore;
            ////dFechaFinal = x.NotAfter;

            sbPassword = new StringBuilder(sPasswordSello); 
            sbPrivateKey = Rsa.ReadEncPrivateKey(sRutaPrivateKey, sbPassword.ToString());
            // sbPassword = X509. ; 
        }

        public string NumeroSerieDeCertificado
        {
            get { return sNoSerie; }
        }

        public string NumeroDeCertificado
        {
            get { return sNoSerieDecimal; }
        }

        public string Certificado
        {
            get { return sCertificado; }
        }

        public string ObtenerSerie(string CadenaOriginal)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(CadenaOriginal);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        public string ObtenerMD5(string CadenaOriginal)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(CadenaOriginal);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5; 
        }

        private void CargarCertificado()
        {
            X509Certificate2 cer;
            string sDatos = ""; 

            cer = new X509Certificate2(sRutaCertificado);

            sDato = cer.GetPublicKeyString(); 
        }

        public string GenerarSelloDigital(string CadenaOriginal)
        {
            string sRegresa = "";
            byte[] b;
            byte[] block;
            int keyBytes;
            string sMD5 = ObtenerMD5(CadenaOriginal);

            CargarCertificado(); 

            _CertificadoPublico = new X509Certificate2(sRutaCertificado);
            sRegresa = FirmarSH1withRSA(sMD5); 


            sbPrivateKey = Rsa.ReadEncPrivateKey(sRutaPrivateKey, sbPassword.ToString());
            keyBytes = Rsa.KeyBytes(sbPrivateKey.ToString());



            // Convert directly to a byte array using the System.Text.Encoding function
            b = System.Text.Encoding.UTF8.GetBytes(CadenaOriginal);

            // Encode this data ready for signing into an `Encoded Message For Signature' block
            // using PKCS#1 v1.5 method and the MD5 hash algorithm.
            block = Rsa.EncodeMsgForSignature(keyBytes, b, CryptoSysPKI.HashAlgorithm.Md5);
            //sMD5 = System.Text.Encoding.UTF8.GetString(block); 

            // Now sign using the RSA private key
            block = Rsa.RawPrivate(block, sbPrivateKey.ToString());

            sRegresa = System.Convert.ToBase64String(block); 

            return sRegresa; 
        }
    
    }
}
