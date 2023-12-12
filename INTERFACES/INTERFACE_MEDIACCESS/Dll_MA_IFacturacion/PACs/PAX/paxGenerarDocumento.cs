#region USING
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
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


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES
using Dll_MA_IFacturacion.PAC_pax_svrRecepcionService;
using Dll_MA_IFacturacion.PAC_pax_svrCancelacionService;

using Dll_MA_IFacturacion.PAC_pax_test_svrRecepcionService;
using Dll_MA_IFacturacion.PAC_pax_test_svrCancelacionService;  
#endregion USING WEB SERVICES 

using Dll_MA_IFacturacion;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_MA_IFacturacion.PACs.PAX
{
    public class paxwsGenerarDocumento
    {
        paxWebServices pax;
        Dll_MA_IFacturacion.PAC_pax_svrRecepcionService.wcfRecepcionASMXSoapClient fileReceiver;
        Dll_MA_IFacturacion.PAC_pax_test_svrRecepcionService.wcfRecepcionASMXSoapClient fileReceiverTest;
        Dll_MA_IFacturacion.PAC_pax_svrCancelacionService.wcfCancelaASMXSoapClient fileCancel;
        Dll_MA_IFacturacion.PAC_pax_test_svrCancelacionService.wcfCancelaASMXSoapClient fileCancelTest;


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

        string sCertificadoPKCS12_Base64 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
        string sPasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;

        string sXmlConTimbreFiscal = "";
        string sXmlAcuseCancelacion = "";

        string sEncabezado = ""; 

        #region Constructor y Destructor de Clase 
        public paxwsGenerarDocumento(clsDatosConexion Conexion)
        {
            datosDeConexion = Conexion;
            pax = new paxWebServices(sUrlTimbrado);

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AcceptAllCertificatePolicy);


            fileReceiver = pax.GetReceiver();
            fileReceiverTest = pax.GetReceiverTest();

            fileCancel = pax.GetCancelCFD();
            fileCancelTest = pax.GetCancelCFDTest();
            ////fileCancel = pax.GetCancelCFD();      

            //"<?xml version="1.0" encoding="UTF-8"?>";
            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "paxwsGenerarDocumento");
            Error.NombreLogErorres = "pax_CtlErrores";


            if (!bEnProduccion)
            {
                sUsuarioTimbrado = "WSDL_PAX";
                sPasswordTimbrado = "wqfCr8O3xLfEhMOHw4nEjMSrxJnvv7bvvr4cVcKuKkBEM++/ke+/gCPvv4nvvrfvvaDvvb/vvqTvvoA=";
            }
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
        private string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        public bool CancelarDocumento(string UUID, string RFC_Emisor)
        {
            bool bRegresa = false;
            string sRegresa = "";
            PAC_pax_svrCancelacionService.ArrayOfString sListaUUIDs = new PAC_pax_svrCancelacionService.ArrayOfString();
            PAC_pax_test_svrCancelacionService.ArrayOfString sListaUUIDs_Test = new PAC_pax_test_svrCancelacionService.ArrayOfString();

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;

                if (bEnProduccion)
                {
                    fileCancel = pax.GetCancelCFD();
                    sListaUUIDs.Add(UUID);
                    sRegresa = fileCancel.fnCancelarXML(sListaUUIDs, RFC_Emisor, 0, sUsuarioTimbrado, sPasswordTimbrado);
                }
                else
                {
                    fileCancelTest = pax.GetCancelCFDTest();
                    sListaUUIDs_Test.Add(UUID);
                    sRegresa = fileCancelTest.fnCancelarXML(sListaUUIDs_Test, RFC_Emisor, 0, sUsuarioTimbrado, sPasswordTimbrado);
                }


                DataSet ds = new DataSet();
                ds.ReadXml(new XmlTextReader(new StringReader(sRegresa)));

                clsLeer leerRespuesta = new clsLeer();
                clsLeer leerResultadoCancelacion = new clsLeer();
                leerRespuesta.DataSetClase = ds;
                leerResultadoCancelacion.DataTableClase = leerRespuesta.Tabla("Folios");

                if (!leerResultadoCancelacion.Leer())
                {
                    sMensajeDeError = sRegresa;
                    bOcurrioError_AlGenerar = true;
                }
                else
                {
                    if (leerResultadoCancelacion.Campo("UUIDEStatus").Trim() == "201")
                    {
                        bRegresa = true;
                        sXmlAcuseCancelacion = sEncabezado + " " + sRegresa;
                    }
                    else
                    {
                        sMensajeDeError = leerResultadoCancelacion.Campo("UUIDdescripcion").Trim();
                        bOcurrioError_AlGenerar = true;
                    }
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
            string sRegresa = "";
            string sXmlEnvio = XML_Base;

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;
                sXmlEnvio = toUTF8(XML_Base.Replace(sEncabezado, ""));

                if (bEnProduccion)
                {
                    fileReceiver = pax.GetReceiver();
                    sRegresa = fileReceiver.fnEnviarXML(sXmlEnvio, TipoDocumento.ToLower(), 0, sUsuarioTimbrado, sPasswordTimbrado, "3.2");
                }
                else
                {
                    fileReceiverTest = pax.GetReceiverTest();
                    sRegresa = fileReceiverTest.fnEnviarXML(sXmlEnvio, TipoDocumento.ToLower(), 0, sUsuarioTimbrado, sPasswordTimbrado, "3.2");
                }

                if (sRegresa.Contains("<cfdi:Comprobante"))
                {
                    bRegresa = true;
                    sXmlConTimbreFiscal = sEncabezado + " " + sRegresa;
                }
                else
                {
                    sMensajeDeError = sRegresa;
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
        #endregion Funciones y Procedimientos Publicos
    }
}
