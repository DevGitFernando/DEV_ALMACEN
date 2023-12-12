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

using SII_Interface_Sinteco.Conexion; 

namespace SII_Interface_Sinteco
{
    public class Gn_ISINTECO
    {
        #region Declaracion de Variables
        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");

        private static bool bConexionWebCEDIS_Establecida = false;
        private static clsDatosWebService datosDeWebServicePedidos = new clsDatosWebService();

        private static bool bLeyendo_Informacion_RFID = false;
        private static bool bMonitor_Cargado = false;
        private static string sDireccion_RFID = "192.168.254.221";

        private static ISinteco i_Sinteco = null; 

        #endregion Declaracion de Variables

        #region Constructor
        static Gn_ISINTECO()
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
        #endregion Servicios Web

        #region Propiedades
        public static bool Leyendo_Informacion_RFID
        {
            get { return bLeyendo_Informacion_RFID; }
            set { bLeyendo_Informacion_RFID = value; }
        }

        public static bool MonitorCargado
        {
            get { return bMonitor_Cargado; }
            set { bMonitor_Cargado = value; }
        }

        public static string Direccion_RFID
        {
            get { return sDireccion_RFID; }
            set { sDireccion_RFID = value;  } 
        }
        #endregion Propiedades

        #region Interface SINTECO
        public static ISinteco Interface_SINTECO
        {
            get
            {
                if (i_Sinteco == null)
                {
                    i_Sinteco = new ISinteco();
                }

                return i_Sinteco;
            }
        }
        #endregion Interface SINTECO
    }
}
