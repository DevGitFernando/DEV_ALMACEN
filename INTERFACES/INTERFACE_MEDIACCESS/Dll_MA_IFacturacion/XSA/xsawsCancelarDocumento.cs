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
    public class xsawsCancelarDocumento
    {
        xsaWebServices xsa; 
        CancelCFDServicePortTypeClient fileCancel;
        clsDatosConexion datosDeConexion;
        clsGrabarError Error;

        string sUrlTimbrado = "";
        bool bOcurrioError_AlGenerar = false;

        public xsawsCancelarDocumento(string Servidor, clsDatosConexion Conexion)
        {
            sUrlTimbrado = Servidor;
            datosDeConexion = Conexion; 
            xsa = new xsaWebServices(sUrlTimbrado);

            fileCancel = xsa.GetCancelCFD();
            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "xsawsCancelarDocumento");
            Error.NombreLogErorres = "xsa_CtlErrores"; 
        }

        public bool ExisteError
        {
            get { return bOcurrioError_AlGenerar; }
        }

        public bool CancelarDocumento(string Key, string RFC, string Serie, string Folio)
        {
            bool bRegresa = false; 
            long lFolio = 0; 

            try
            {
                lFolio = (long)Convert.ToInt32(Folio.Replace(",", ""));
                ////clsGrabarError.LogFileError(string.Format("Serie: {0}  Folio: {1}  RFC: {2}   Key: {3}", Serie, lFolio, RFC, Key)); 
                bRegresa = Convert.ToBoolean(fileCancel.cancelaCFD(Serie, lFolio, RFC, Key)); 
            }
            catch (Exception ex)
            {
                Error.GrabarError(ex.Message, "CancelarDocumento()"); 
            }

            bOcurrioError_AlGenerar = !bRegresa; 
            return bRegresa; 
        }
    }
}
