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
using System.Security.Cryptography;
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
using Dll_IFacturacion.paxsvrRecepcionService;
using Dll_IFacturacion.paxsvrCancelacionService;  
#endregion USING WEB SERVICES 

using Dll_IFacturacion;
using SC_SolutionsSystem.FuncionesGenerales;  

namespace Dll_IFacturacion.PAX
{
    public class paxwsGenerarDocumento
    {
        paxWebServices pax;
        wcfRecepcionASMXSoapClient fileReceiver;
        wcfCancelaASMXSoapClient fileCancel; 
        

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

        PAC_Info pacInfo = new PAC_Info();

        string sXmlConTimbreFiscal = "";
        string sXmlAcuseCancelacion = "";

        string sEncabezado = ""; 

        #region Constructor y Destructor de Clase 
        public paxwsGenerarDocumento(clsDatosConexion Conexion) :this(Conexion, DtIFacturacion.PAC_Informacion) 
        {
        }

        public paxwsGenerarDocumento(clsDatosConexion Conexion, PAC_Info Informacion_PAC)
        {
            bEnProduccion = Informacion_PAC.EnProduccion;
            sUrlTimbrado = Informacion_PAC.Url;
            sUsuarioTimbrado = Informacion_PAC.Usuario;
            sPasswordTimbrado = Informacion_PAC.Password;

            datosDeConexion = Conexion;
            pax = new paxWebServices(sUrlTimbrado);

            fileReceiver = pax.GetReceiver();
            fileCancel = pax.GetCancelCFD();            

            //"<?xml version="1.0" encoding="UTF-8"?>";
            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());

            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "paxwsGenerarDocumento");
            Error.NombreLogErorres = "pax_CtlErrores";

            //pacInfo = DtIFacturacion.PAC_Informacion; 
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
            ArrayOfString sListaUUIDs = new ArrayOfString(); 

            try
            {
                sMensajeDeError = "";
                bOcurrioError_AlGenerar = false;

                fileCancel = pax.GetCancelCFD();
                sListaUUIDs.Add(UUID);
                sRegresa = fileCancel.fnCancelarXML(sListaUUIDs, RFC_Emisor, 0, sUsuarioTimbrado, sPasswordTimbrado);

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
                    if (leerResultadoCancelacion.Campo("UUIDEStatus").Trim() == "201" )
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

                fileReceiver = pax.GetReceiver(); 
                sRegresa = fileReceiver.fnEnviarXML(sXmlEnvio, TipoDocumento.ToLower(), 0, sUsuarioTimbrado, sPasswordTimbrado, "3.2");

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
