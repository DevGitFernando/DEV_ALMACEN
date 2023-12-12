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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES
using Dll_MA_IFacturacion.xsasvrCancelCFDService;
using Dll_MA_IFacturacion.xsasvrCFDService;
using Dll_MA_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 

namespace Dll_MA_IFacturacion.XSA
{
    public class xsawsGenerarDocumento
    {
        xsaWebServices xsa;
        FileReceiverServicePortTypeClient fileReceiver;
        clsDatosConexion datosDeConexion;
        clsGrabarError Error;
        int iNumIntentosTimbrado = 120; 
        string sMensajeDeError = ""; 

        string sUrlTimbrado = "";
        bool bOcurrioError_AlGenerar = false;

        #region Constructor y Destructor de Clase 
        public xsawsGenerarDocumento(string Servidor, clsDatosConexion Conexion)
        {
            sUrlTimbrado = Servidor;
            datosDeConexion = Conexion; 
            xsa = new xsaWebServices(sUrlTimbrado);

            fileReceiver = xsa.GetReceiver();
            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "xsawsGenerarDocumento");
            Error.NombreLogErorres = "xsa_CtlErrores"; 
        }
        #endregion Constructor y Destructor de Clase 

        #region Propiedades 
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
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos 
        public bool GenerarDocumento(string Key, string RFC, string Empresa_o_Sucursal, string TipoDocumento, string NombreDocumento, string ContenidoDocumento)
        {
            bool bRegresa = false;
            string sStatusDocumento = "";
            bool bProcesoActivo = true;
            int iIntentosGenerar = 0;
            string sMsjError = "";

            //// iNumIntentosTimbrado = (1 * 120) * 500;  ==> 1 Minutos 
            iNumIntentosTimbrado = 6 * 60; 

            try 
            {
                sMensajeDeError = ""; 
                bOcurrioError_AlGenerar = false;
                fileReceiver.guardarDocumento(Key + "-" + RFC, Empresa_o_Sucursal, TipoDocumento, NombreDocumento, ContenidoDocumento); 

                while (bProcesoActivo)
                {
                    System.Threading.Thread.Sleep(500); 
                    sStatusDocumento = fileReceiver.obtenerEstadoDocumento(RFC, NombreDocumento, Key);
                    sMsjError = sStatusDocumento.Substring(0, 5); 

                    if (sStatusDocumento.ToUpper() == "Generado".ToUpper())
                    {
                        bProcesoActivo = false;
                        bRegresa = true; 
                    }
                    else if (sMsjError.ToUpper() == "Error".ToUpper())
                    {
                        bProcesoActivo = false;
                        bOcurrioError_AlGenerar = true;
                        sMensajeDeError = sStatusDocumento; 
                    }
                    else if (sStatusDocumento.ToUpper() == "EnProceso".ToUpper())
                    {
                        bProcesoActivo = true; 
                    }

                    iIntentosGenerar++;
                    if (iIntentosGenerar > iNumIntentosTimbrado && bProcesoActivo)
                    {
                        bProcesoActivo = false;
                        bOcurrioError_AlGenerar = true;  
                    }
                }
            }
            catch (Exception ex) 
            {
                sMensajeDeError = ex.Message; 
                bOcurrioError_AlGenerar = true; 
                // Error.GrabarError(ex.Message, "GenerarDocumento()"); 
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
