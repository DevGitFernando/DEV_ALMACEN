using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms; 

namespace Dll_IFacturacion.CryptoSysPKI
{
    /// <summary>
    /// Online Certificate Status Protocol (OCSP)
    /// </summary>
    internal class Ocsp
    {
        const string sDllLocation = "diCrPKI.dll";

        private Ocsp()
        { }	// Static methods only, so hide constructor.

        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int OCSP_MakeRequest(StringBuilder szOutput, int nOutChars, string szIssuerCert, string szCertFileOrSerialNum, string szExtensions, int nOptions);
        /// <summary>
        /// Creates an Online Certification Status Protocol (OCSP) request as a base64 string. 
        /// </summary>
        /// <param name="issuerCert">name of issuer's X.509 certificate file (or base64 representation)</param>
        /// <param name="certFileOrSerialNumber">either the name of X.509 certificate file to be checked or its serial number in hexadecimal format preceded by #x</param>
        /// <param name="hashAlg">Hash algorithm to be used (default 0 is SHA-1)</param>
        /// <returns>A base64 string suitable for an OCSP request to an Online Certificate Status Manager or an empty string on error.</returns>
        /// <remarks>The issuer's X.509 certficate must be specified. 
        /// The certificate to be checked can either be specified directly as a filename 
        /// or as a serialNumber in hexadecimal format, e.g. "#x01deadbeef". 
        /// If the latter format is used, it must be in hexadecimal format, 
        /// so the serial number 10 would be passed as "#x0a". 
        /// It is an error (NO_MATCH_ERROR) if the issuer's name of the certificate to be checked 
        /// does not match the subject name of the issuer's certificate.
        /// </remarks>
        public static string MakeRequest(string issuerCert, string certFileOrSerialNumber, HashAlgorithm hashAlg)
        {
            StringBuilder sb = new StringBuilder(0);
            int n = OCSP_MakeRequest(sb, 0, issuerCert, certFileOrSerialNumber, "", (int)hashAlg);
            if (n <= 0) return string.Empty;
            sb = new StringBuilder(n);
            OCSP_MakeRequest(sb, sb.Capacity, issuerCert, certFileOrSerialNumber, "", (int)hashAlg);
            return sb.ToString();
        }
        [DllImport(sDllLocation, CharSet = CharSet.Ansi)]
        static extern int OCSP_ReadResponse(StringBuilder szOutput, int nOutChars, string szResponseFile, string szIssuerCert, string szExtensions, int nOptions);
        /// <summary>
        /// Reads a response to an Online Certification Status Protocol (OCSP) request and outputs the main results in text form.
        /// </summary>
        /// <param name="responseFile">name of the file containing the response data in BER format.</param>
        /// <param name="issuerCert">(optional) name of issuer's X.509 certificate file (or base64 representation)</param>
        /// <returns>A text string outlining the main results in the response data or an empty string on error.</returns>
        /// <remarks>Note that a revoked certificate will still result in a "Successful response", so check the CertStatus. 
        /// The issuer's X.509 certficate <c>issuerCert</c> is optional. 
        /// If provided, it will be used to check the signature on the OCSP reponse and and an error 
        /// will result if the signature is not valid. 
        /// <b>CAUTION:</b> For some CAs (e.g. VeriSign) the key used to sign the OCSP response is not the same as 
        /// the key in the issuer's certificate, so specifying the issuer's certificate in this case will result 
        /// in a signature error. If you can separately obtain the certificate used to sign the OCSP response, 
        /// then specify this as the <c>issuerCert</c>; otherwise leave as the empty string <c>""</c>.
        /// </remarks>
        public static string ReadResponse(string responseFile, string issuerCert)
        {
            StringBuilder sb = new StringBuilder(0);
            int n = OCSP_ReadResponse(sb, 0, responseFile, issuerCert, "", 0);
            if (n <= 0) return string.Empty;
            sb = new StringBuilder(n);
            OCSP_ReadResponse(sb, sb.Capacity, responseFile, issuerCert, "", 0);
            return sb.ToString();
        }
    }

}
