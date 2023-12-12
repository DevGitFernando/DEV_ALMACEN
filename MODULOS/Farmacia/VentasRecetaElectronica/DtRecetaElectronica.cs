using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.GetInformacionManual;
using DllFarmaciaSoft.Reporteador;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.SistemaOperativo;

namespace Farmacia.VentasRecetaElectronica 
{
    public static class DtRecetaElectronica
    {
        #region Declaracion de Variables
        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", Application.ProductVersion);

        private static bool bBusquedaConfiguracionRealizada = false;
        private static bool bManejaRecetaElectronica = false;

        private static string sIdUMedica = ""; 
        private static string sCLUES = "";
        private static string sNombreUnidadMedica = "";
        private static string sUrlRepositorioRecetasElectronicas = ""; 


        #endregion Declaracion de Variables

        #region Constructores 
        static DtRecetaElectronica()
        {
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());

            if (!bBusquedaConfiguracionRealizada)
            {
                GetConfiguracion(); 
            }
        }
        #endregion DtRecetaElectronica 

        #region Propiedades Publicas 
        public static bool ManejaRecetaElectronica
        {
            get { return bManejaRecetaElectronica; }
        }

        public static string IdUMedica
        {
            get { return sIdUMedica; }
            set { sIdUMedica = value; }
        }

        public static string CLUES
        {
            get { return sCLUES; }
            set { sCLUES = value; }
        }

        public static string NombreUnidadMedica
        {
            get { return sNombreUnidadMedica; }
            set { sNombreUnidadMedica = value; }
        }

        public static string UrlRepositorio
        {
            get { return sUrlRepositorioRecetasElectronicas; }
            set { sUrlRepositorioRecetasElectronicas = value; }
        }
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos 
        public static void BuscarConfiguracionRecetaElectronica()
        {
            if (!bBusquedaConfiguracionRealizada)
            {              
                GetConfiguracion();
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private static void GetConfiguracion()
        {
            bBusquedaConfiguracionRealizada = true;

            try
            {

                clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
                clsLeer leer = new clsLeer(ref cnn);
                clsGrabarError Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, "DtRecetaElectronica");

                string sSql = string.Format("Select  IdEstado, IdFarmacia, IdUMedica, CLUES, NombreUnidadMedica, UrlRepositorioRecetas, ServicioActivo " +
                    " From REC_CFG_RecetasElectronicas (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' ",
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "GetConfiguracion()");
                }
                else
                {
                    if (leer.Leer())
                    {
                        bManejaRecetaElectronica = leer.CampoBool("ServicioActivo");
                        sIdUMedica = leer.Campo("IdUMedica");
                        sCLUES = leer.Campo("CLUES");
                        sNombreUnidadMedica = leer.Campo("NombreUnidadMedica");
                        sUrlRepositorioRecetasElectronicas = leer.Campo("UrlRepositorioRecetas");
                    }
                }
            }
            catch { } 

        }
        #endregion Funciones y Procedimientos Privados
    }
}
