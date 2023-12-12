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
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

namespace DllRegistrosSanitarios
{
    public static class GnRegistrosSanitarios
    {

        #region Declaracion de variables
        //private static string sModulo = "Compras";
        //private static string sVersion = "";

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllRegistrosSanitarios", "");
        private static clsParametrosOficinaCentral pParametros;
        private static string sRutaReportes = "";
        private static bool bLectorDeHuellas = false;
        private static bool bConfirmacionConHuellas = false;

        private static int iNumCompras = 0;
        private static int iPorcMaxCompras = 10;
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;

        private static string sRutaRegistrosSanitarios = "";
        private static string sRuta_DB_RegistrosSanitarios = "";
        private static string sDB_RegistrosSanitarios = ""; 

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" }; 
        #endregion Declaracion de variables

        static GnRegistrosSanitarios()
        {
            clsAbrirForma.AssemblyActual("DllRegistrosSanitarios");
            dpDatosApp = clsAbrirForma.DatosApp;

            sRutaRegistrosSanitarios = Application.StartupPath + @"\RegistrosSanitarios\";
            sRuta_DB_RegistrosSanitarios = sRutaRegistrosSanitarios; // Application.StartupPath + @"\RegistrosSanitarios\";

            sDB_RegistrosSanitarios = "SII_RegistrosSanitario.s3db";

            CrearDirectorios();
            CrearBaseDeDatos(); 

        }

        #region Propieades Dll
        public static clsParametrosOficinaCentral Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

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
        public static string DaseDeDatos_SQLite
        {
            get { return Path.Combine(sRuta_DB_RegistrosSanitarios, sDB_RegistrosSanitarios); }
        }

        public static string DB_RegistrosSanitarios
        {
            get { return sDB_RegistrosSanitarios; }
            set { sDB_RegistrosSanitarios = value; }
        }

        public static string Ruta_DB_RegistrosSanitarios
        {
            get { return sRuta_DB_RegistrosSanitarios; }
            set { sRuta_DB_RegistrosSanitarios = value; }
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
        #endregion Propiedades  

        #region Funciones y Procedimientos Privados  
        private static void CrearDirectorios()
        {
            if (!Directory.Exists(sRuta_DB_RegistrosSanitarios))
            {
                Directory.CreateDirectory(sRuta_DB_RegistrosSanitarios); 
            }
        }

        private static void CrearBaseDeDatos()
        {
            if (!File.Exists(Path.Combine(sRuta_DB_RegistrosSanitarios, sDB_RegistrosSanitarios)))
            {
                General.Fg.ConvertirBytesEnArchivo(sDB_RegistrosSanitarios, sRuta_DB_RegistrosSanitarios, Properties.Resources.SII_RegistrosSanitario, true);                  
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
