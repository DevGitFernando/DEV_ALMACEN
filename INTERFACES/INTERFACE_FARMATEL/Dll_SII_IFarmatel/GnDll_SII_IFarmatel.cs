using System;
using System.Collections;
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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

namespace Dll_SII_IFarmatel
{
    public enum TipoDeDocumento
    {
        Ninguno = 0,
        Existencias = 1, Surtidos = 2, Recibos = 3, Remisiones = 4, TomaDeExistencia = 5, Catalogos = 100
    }

    public static class GnDll_SII_IFarmatel
    {
        #region Declaracion de Variables 
        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");

        private static bool bConexionWebCEDIS_Establecida = false; 
        private static clsDatosWebService datosDeWebServicePedidos = new clsDatosWebService();

        private static IFarmatel i_Farmatel = null; 

        #endregion Declaracion de Variables

        #region Constructor 
        static GnDll_SII_IFarmatel()
        {
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
        }
        #endregion Constructor

        #region Propieades Dll
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }
        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }
        public static string Version
        {
            get { return dpDatosApp.Version; }
        }
        #endregion Propieades Dll

        #region Servicios Web
        public static void SetConexionWebCEDIS()
        {
            if (!bConexionWebCEDIS_Establecida )
            {
                string sUrl = GnFarmacia.UrlAlmacenRegional; 

                datosDeWebServicePedidos.Servidor = DtGeneral.DatosDeServicioWebCEDIS.Servidor;
                datosDeWebServicePedidos.WebService = DtGeneral.DatosDeServicioWebCEDIS.WebService;
                datosDeWebServicePedidos.PaginaASMX = DtGeneral.DatosDeServicioWebCEDIS.PaginaASMX;
                datosDeWebServicePedidos.PaginaASMX = "wsInterfaceAlmacen"; 
            }
        }

        public static clsDatosWebService DatosDeServicioWebPedidos
        {
            get { return datosDeWebServicePedidos; }
            set { datosDeWebServicePedidos = value; }
        }
        #endregion Servicios Web 

        #region Propiedades
        #endregion Propiedades

        #region Reportes 
        private static clsParametros_SII_IFarmatel__PtoVta pParametros;
        private static string sRutaReportes = "";

        private static string sRPT_Remisiones_Receta = "";
        private static string sRPT_Remisiones_Colectivo = "";

        public static clsParametros_SII_IFarmatel__PtoVta Parametros 
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        public static string RutaReportes
        {
            get
            {
                if (sRutaReportes == "")
                {
                    sRutaReportes = pParametros.GetValor("RutaReportes");
                }
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }
        #endregion Reportes 

        #region Interface Farmatel 
        public static IFarmatel Interface_Pedidos
        {
            get 
            {
                if (i_Farmatel == null)
                {
                    i_Farmatel = new IFarmatel(); 
                }

                return i_Farmatel; 
            }
        }
        #endregion Interface Farmatel 
    }

}
