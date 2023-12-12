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

using Microsoft.VisualBasic;

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

using Dll_IRE_AMPM;
using Dll_IRE_AMPM.wsClases;
//using Dll_IRE_AMPM.wsAcuseProcesos_cnnInterna;

namespace Dll_IRE_AMPM.wsClases
{
    public class EnviarInformacionAcuses
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer acuses;
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolioVenta = "";
        string sFolio_SIADISSEP = "";

        public EnviarInformacionAcuses(clsDatosConexion DatosDeConexion)
        {
            datosDeConexion = DatosDeConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            acuses = new clsLeer(); 
            Error = new clsGrabarError(datosDeConexion, GnDll_SII_AMPM.DatosApp, "EnviarAcuses"); 
        }

        public void EnviarAcuses()
        {
            string sSql = string.Format("Exec ssp___InformacionEnvio ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "EnviarAcuses()"); 
            }
            else
            {
                leer.RenombrarTabla(1, "SurtidoRecetas");
                leer.RenombrarTabla(2, "CancelacionRecetas"); 
                
                acuses = new clsLeer();
                acuses.DataTableClase = leer.Tabla(1);
                while (acuses.Leer())
                {
                    sIdEmpresa = acuses.Campo("IdEmpresa");
                    sIdEstado = acuses.Campo("IdEstado");
                    sIdFarmacia = acuses.Campo("IdFarmacia");
                    sFolioVenta = acuses.Campo("FolioVenta");                     

                    EnviarAcuses_SurtidoRecetas(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta); 
                }

                acuses = new clsLeer();
                acuses.DataTableClase = leer.Tabla(2);
                while (acuses.Leer())
                {
                    sIdEmpresa = acuses.Campo("IdEmpresa");
                    sIdEstado = acuses.Campo("IdEstado");
                    sIdFarmacia = acuses.Campo("IdFarmacia");
                    sFolio_SIADISSEP = acuses.Campo("FolioInterface"); 
                    
                    EnviarAcuses_Cancelaciones(sIdEmpresa, sIdEstado, sIdFarmacia, sFolio_SIADISSEP); 
                }
            }

        }

        private bool EnviarAcuses_SurtidoRecetas(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta)
        {
            bool bRegresa = false; 
            //ResponseAcuseXML respuesta = new ResponseAcuseXML(datosDeConexion, IdEmpresa, IdEstado, IdFarmacia);
            //bRegresa = respuesta.EnviarAcusesReceta(FolioVenta, true);
            return bRegresa; 
        }

        private bool EnviarAcuses_Cancelaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio_SIADISSEP)
        {
            bool bRegresa = false; 
            //ResponseAcuseXML respuesta = new ResponseAcuseXML(datosDeConexion, IdEmpresa, IdEstado, IdFarmacia);
            //bRegresa = respuesta.EnviarAcusesCancelacionReceta(Folio_SIADISSEP, true);
            return bRegresa; 
        }
    }
}
