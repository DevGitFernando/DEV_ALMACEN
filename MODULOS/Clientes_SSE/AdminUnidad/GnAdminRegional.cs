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
using DllPedidosClientes;

namespace AdminUnidad
{
    class GnAdminUnidad
    {
        //private static string sModulo = "OficinaCentral";
        //private static string sVersion = ""; // Application.ProductVersion;

        private static clsDatosApp dpDatosApp = new clsDatosApp("Administracion Unidad", "");
        private static clsParametrosClienteUnidad pParametros;
        private static string sRutaReportes = "";

        static GnAdminUnidad()
        {
            clsAbrirForma.AssemblyActual("Administracion Unidad");
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

        public static clsParametrosClienteUnidad Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        #endregion Propiedades 
    }
}
