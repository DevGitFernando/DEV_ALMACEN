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
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

using FarPoint.Win.Spread;

namespace DllFarmaciaSoft
{
    public static class GnInventarios
    {
        #region Declaracion de variables  
        //private static string sModulo = "Farmacia";
        //private static string sVersion = "";

        // private static int iMesesCaducaMedicamento = 1;
        // private static string sIdCaja = "01";

        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");
        private static clsParametrosPtoVta pParametros;
        private static string sRutaReportes = "";
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;
        // private static double dTipoDeCambio = -1;

        // private static bool bMostrarPreciosCostos = false;
        private static string sModuloTransferencia = "Servicio Cliente.exe";
        private static string sRutaServicio = Application.StartupPath + @"\\" + sModuloTransferencia;

        private static clsGrabarError Error = new clsGrabarError();
        // private static bool bEsServidorLocal = false; 
        private static bool bExisteServicio = File.Exists(sRutaServicio);
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");
        private static Color colorProductosIMach = Color.Yellow;
        // private static bool bValidarSesionUsuario = false; 

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" };
        #endregion Declaracion de variables

        static GnInventarios()
        {
            ////clsAbrirForma.AssemblyActual("DllFarmaciaAuditor");
            ////dpDatosApp = clsAbrirForma.DatosApp;

            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
        }

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

        #region Propiedades 
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

        public static clsParametrosPtoVta Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        } 
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos  
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        #endregion Funciones y Procedimientos Privados

    }
}
