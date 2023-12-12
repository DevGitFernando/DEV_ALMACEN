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
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.xsasvrCancelCFDService;
using Dll_IFacturacion.xsasvrCFDService;
using Dll_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 

namespace Dll_IFacturacion.XSA
{
    public class xsawsDescargarDocumentos
    {

        xsaWebServices xsa;
        clsDatosConexion datosDeConexion;
        clsGrabarError Error;
        //string sMensajeDeError = ""; 

        string sUrlTimbrado = "";
        //bool bOcurrioError_AlGenerar = false;
        string sUrlDocumentos = "";
        string sRutaCFDI_Documentos = DtIFacturacion.RutaCFDI_DocumentosGenerados;

        bool bXML = false;
        bool bPDF = false;
        string sXML = "";
        string sPDF = ""; 

        #region Constructor y Destructor de Clase 
        public xsawsDescargarDocumentos(string Servidor, clsDatosConexion Conexion)
        {
            sUrlTimbrado = Servidor;
            datosDeConexion = Conexion; 
            xsa = new xsaWebServices(sUrlTimbrado);

            sUrlDocumentos = xsa.DescargarDocumentos; 
            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "xsawsDescargarDocumentos");
            Error.NombreLogErorres = "xsa_CtlErrores"; 
        }
        #endregion Constructor y Destructor de Clase 

        #region Propiedades 
        public string XML
        {
            get { return sXML; }
        }

        public string PDF
        {
            get { return sPDF; }
        }

        public bool ExisteXML
        {
            get { return bXML; }
        }

        public bool ExistePDF
        {
            get { return bPDF; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public bool Descargar(string RFC, string Key, string Serie, string Folio)
        {
            bool bRegresa = false;
            Folio = Folio.Replace(",", "").Trim();

            // WebBrowser down = new WebBrowser();

            bXML = ArmarCadena(RFC, Key, Serie, Folio, cfdFormatoDocumento.PDF);
            bPDF = ArmarCadena(RFC, Key, Serie, Folio, cfdFormatoDocumento.XML);

            if (bXML || bPDF)
            {
                bRegresa = true; 
            }

            return bRegresa; 
        }

        private bool ArmarCadena(string RFC, string Key, string Serie, string Folio, cfdFormatoDocumento Formato)
        {
            bool bRegresa = false;
            string sRegresa = "";  
            string sDocumentoName = RFC + "___" + Serie + "_" + Folio + "." + Formato.ToString(); 

            string sDocumento = Path.Combine(sRutaCFDI_Documentos, sDocumentoName); 

            sRegresa = string.Format("{0}?serie={1}&folio={2}&tipo={3}&rfc={4}&key={5}", 
                sUrlDocumentos, Serie, Folio, Formato.ToString(), RFC, Key);

            FrmDescargarDocumentos f = new FrmDescargarDocumentos(sRegresa, sDocumento);
            f.ShowDialog();
            bRegresa = f.ExisteDocumento;

            // Guardar el nombre del documento 
            if (bRegresa)
            {
                switch (Formato)
                {
                    case cfdFormatoDocumento.XML:
                        sXML = sDocumentoName;
                        break; 

                    case cfdFormatoDocumento.PDF:
                        sPDF = sDocumentoName; 
                        break; 
                }
            }
            return bRegresa; 
        }

        #endregion Funciones y Procedimientos Publicos
    }
}
