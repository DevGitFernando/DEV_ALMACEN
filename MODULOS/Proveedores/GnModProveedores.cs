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

using DllProveedores;

namespace Proveedores
{
    public static class GnModProveedores
    {

        #region Declaracion de variables
        private static string sModulo = "DllProveedores";
        private static string sVersion = "";

        private static clsDatosApp dpDatosApp = new clsDatosApp("DllProveedores", "");
        private static clsParametrosProveedores pParametros;
        private static string sRutaReportes = "";
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" };
        #endregion Declaracion de variables

        static GnModProveedores()
        {
            clsAbrirForma.AssemblyActual("Proveedores");
            dpDatosApp = clsAbrirForma.DatosApp; 
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

        public static DateTime FechaOperacionSistema
        {
            get
            {
                DateTime dt = General.FechaSistema;
                try
                {
                    dt = Convert.ToDateTime(pParametros.GetValor("FechaOperacionSistema"));
                }
                catch
                {
                }
                return dt;
            }
        }

        public static clsParametrosProveedores Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }
        #endregion Propiedades 
    }
}
