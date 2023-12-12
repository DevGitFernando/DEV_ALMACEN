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
using SC_SolutionsSystem.Usuarios_y_Permisos;
//using DllFarmaciaSoft;


namespace DllAdministracion
{
    public static class GnAdministracion
    {
        //private static string sModulo = "Administracion";
        //private static string sVersion = ""; // Application.ProductVersion;

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllAdministracion", "");
        private static clsParametrosAdministracion pParametros;
        private static string sRutaReportes = "";

        static GnAdministracion()
        {
            ////clsAbrirForma.AssemblyActual("DllAdministracion");
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

        #region PUBLICO GENERAL
        static string sIdClientePublicoGral = "0000";
        public static void CargarDatosPublicoGeneral()
        {
            sIdClientePublicoGral = pParametros.GetValor("CtePubGeneral");
        }

        public static string PublicoGral
        {
            get { return sIdClientePublicoGral; }
        }
        #endregion PUBLICO GENERAL

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

        public static clsParametrosAdministracion Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        #endregion Propiedades 
    }
}
