#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Reflection;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Data.Odbc;
using System.ServiceModel;
using System.ServiceModel.Channels;


using Dll_IFacturacion;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.PAC_VirtualSoft;
//using Dll_IFacturacion.paxsvrCancelacionService;
#endregion USING WEB SERVICES

using Dll_IFacturacion;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IFacturacion.VirtualSoft
{
    public class vswsGenerarDocumento
    {
        vsWebServices pax;
        //ServiceClient fileReceiver;
        //ServiceClient fileCancel;


        clsDatosConexion datosDeConexion;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales();
        int iNumIntentosTimbrado = 120;
        string sMensajeDeError = "";
        bool bOcurrioError_AlGenerar = false;

        bool bEnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
        string sUrlTimbrado = DtIFacturacion.PAC_Informacion.Url;
        string sUsuarioTimbrado = DtIFacturacion.PAC_Informacion.Usuario;
        string sPasswordTimbrado = DtIFacturacion.PAC_Informacion.Password;
        static string sRFC_Emisor = "";

        byte[] byCertificado = DtIFacturacion.PAC_Informacion.Certificado;
        byte[] byKey = DtIFacturacion.PAC_Informacion.Key;
        string sCertificadoPKCS12_Base64 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
        string sPasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;
        int iCreditos_Disponibles = 0; 

        string sXmlConTimbreFiscal = "";
        string sXmlAcuseCancelacion = "";

        string sEncabezado = "";

        #region Constructor y Destructor de Clase
        public vswsGenerarDocumento(clsDatosConexion Conexion): this(Conexion, DtIFacturacion.PAC_Informacion) 
        {
        }

        public vswsGenerarDocumento(clsDatosConexion Conexion, PAC_Info Informacion_PAC)
        {
            bEnProduccion = Informacion_PAC.EnProduccion;
            sUrlTimbrado = Informacion_PAC.Url;
            sUsuarioTimbrado = Informacion_PAC.Usuario;
            sPasswordTimbrado = Informacion_PAC.Password;

            datosDeConexion = Conexion;
            pax = new vsWebServices(sUrlTimbrado);

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AcceptAllCertificatePolicy);

            //fileReceiver = pax.GetReceiver();
            //fileCancel = pax.GetReceiver();

            //"<?xml version="1.0" encoding="UTF-8"?>";
            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "vswsGenerarDocumento");
            Error.NombreLogErorres = "vs_CtlErrores";
        }

        private bool AcceptAllCertificatePolicy(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion Constructor y Destructor de Clase

        #region Propiedades
        public bool Error_AlGenerar
        {
            get { return bOcurrioError_AlGenerar; }
        }

        public string MensajeError
        {
            get
            {
                string sMsj = "";
                if (bOcurrioError_AlGenerar)
                {
                    sMsj = sMensajeDeError;
                }
                return sMsj;
            }
        }

        public string RFC_Emisor
        {
            get { return sRFC_Emisor; }
            set { sRFC_Emisor = value; }
        }

        public int Creditos_Disponibles
        {
            get { return iCreditos_Disponibles; }
        }

        public string XmlConTimbreFiscal
        {
            get { return sXmlConTimbreFiscal; }
        }

        public string XmlAcuseCancelacion
        {
            get { return sXmlAcuseCancelacion; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Privados
        public bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public bool CancelarDocumento(string UUID, string RFC_Emisor, string ClaveMotivoCancelacion_SAT, string UUID_Relacionado )
        {
            bool bRegresa = false;
            int iRegresa = 0;
            string sRegresa = "";
            vsWebServices xsa = new vsWebServices(sUrlTimbrado);
            IService Timbrar;

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                Timbrar = xsa.GetReceiver();
                //Timbrar.cancelarXML(sUsuarioTimbrado, sPasswordTimbrado, RFC_Emisor, UUID, byCertificado, byKey, ref sMensajeDeError, ref sXmlAcuseCancelacion, out iRegresa, out bRegresa);
                iRegresa = Timbrar.cancelarXML(sUsuarioTimbrado, sPasswordTimbrado, RFC_Emisor, UUID, byCertificado, byKey, ClaveMotivoCancelacion_SAT, UUID_Relacionado, ref sMensajeDeError, ref sXmlAcuseCancelacion);


                //if (sRegresa.Contains("<cfdi:Comprobante"))
                if (iRegresa == 1)
                {
                    bRegresa = true;
                    //sXmlConTimbreFiscal = sRegresa;
                }
                else
                {
                    ////sMensajeDeError = sRegresa;
                    bRegresa = false;
                    bOcurrioError_AlGenerar = true;
                }
            }
            catch (Exception ex)
            {
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }


            if (bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            return bRegresa;
        }

        public bool GenerarDocumento(string XML_Base, string TipoDocumento)
        {
            bool bRegresa = false;
            int iRegresa = 0; 
            string sRegresa = "";
            string sXmlEnvio = XML_Base;
            vsWebServices xsa = new vsWebServices(sUrlTimbrado);
            IService Timbrar;

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;
                //sXmlEnvio = toUTF8(XML_Base.Replace(sEncabezado, ""));
                sXmlEnvio = toUTF8(XML_Base);

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                ////fileReceiver = pax.GetReceiver();
                ////fileReceiver = new ServiceClient("BasicHttpBinding_IService");  //, "http://facturacion.virtualsoft.com.mx:7512/timbradoXMLDemo/Service.svc"); 

                Timbrar = xsa.GetReceiver();
                //Timbrar.timbrarXML(sUsuarioTimbrado, sPasswordTimbrado, sXmlEnvio, ref sXmlConTimbreFiscal, ref sMensajeDeError, out iRegresa, out bRegresa);
                iRegresa = Timbrar.timbrarXML(sUsuarioTimbrado, sPasswordTimbrado, sXmlEnvio, ref sXmlConTimbreFiscal, ref sMensajeDeError);


                //if (sRegresa.Contains("<cfdi:Comprobante"))
                if ( iRegresa == 1 ) 
                {
                    bRegresa = true;                  
                    //sXmlConTimbreFiscal = sRegresa;
                }
                else 
                {
                    bRegresa = false;
                    //sMensajeDeError = sMensajeDeError;
                    bOcurrioError_AlGenerar = true;
                }
            }
            catch (Exception ex)
            {
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            if (bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            return bRegresa;
        }

        public bool ConsultarCreditos()
        {
            PAC_Info RFC_Emisor = new PAC_Info();
            RFC_Emisor.Usuario = sUsuarioTimbrado;
            RFC_Emisor.Password = sPasswordTimbrado;
            RFC_Emisor.Url = sUrlTimbrado;

            return ConsultarCreditos(RFC_Emisor);
        }

        public bool ConsultarCreditos(PAC_Info RFC_Emisor)
        {
            bool bRegresa = false;
            int iRegresa = 0;
            string sRegresa = "";
            DataSet dtsInformacion = new DataSet();
            clsLeer leerResultado = new clsLeer(); 
            vsWebServices xsa = new vsWebServices(sUrlTimbrado);
            IService Timbrar;

            try
            {
                bOcurrioError_AlGenerar = false;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                Timbrar = xsa.GetReceiver();
                //ResetRespuesta();

                //Timbrar.verificarVigencia(RFC_Emisor.Usuario, RFC_Emisor.Password, ref sXmlConTimbreFiscal, ref sMensajeDeError, out iRegresa, out bRegresa);
                iRegresa = Timbrar.verificarVigencia(RFC_Emisor.Usuario, RFC_Emisor.Password, ref sXmlConTimbreFiscal, ref sMensajeDeError);


                if (iRegresa == 1)
                {
                    bRegresa = true;
                    
                    dtsInformacion = new DataSet();
                    dtsInformacion.ReadXml(new XmlTextReader(new StringReader(sXmlConTimbreFiscal)));
                    leerResultado.DataRowsClase = dtsInformacion.Tables["detalle"].Select(" Campo = 'FoliosRestantes' ");

                    if (leerResultado.Leer())
                    {
                        iRegresa = leerResultado.CampoInt("Valor"); 
                    }

                    //sXmlConTimbreFiscal = sRegresa;
                }
                else
                {
                    ////sMensajeDeError = sRegresa;
                    bOcurrioError_AlGenerar = true;
                }

            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            if (bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            iCreditos_Disponibles = iRegresa; 

            return bRegresa;
        }

        public bool DescargarXML(string RFC, string UUID)
        {
            bool bRegresa = false;
            int iRegresa = 0;
            string sRegresa = "";
            string sXmlEnvio = ""; 
            vsWebServices xsa = new vsWebServices(sUrlTimbrado);
            IService Timbrar;

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                Timbrar = xsa.GetReceiver();
                //Timbrar.obtenerXML(sUsuarioTimbrado, sPasswordTimbrado, UUID, RFC, ref sXmlConTimbreFiscal, ref sMensajeDeError, out iRegresa, out bRegresa);
                iRegresa = Timbrar.obtenerXML(sUsuarioTimbrado, sPasswordTimbrado, UUID, RFC, ref sXmlConTimbreFiscal, ref sMensajeDeError);

                if(iRegresa == 1)
                {
                    bRegresa = true;
                }
                else
                {
                    bRegresa = false;
                    bOcurrioError_AlGenerar = true;
                }
            }
            catch(Exception ex)
            {
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            if(bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
