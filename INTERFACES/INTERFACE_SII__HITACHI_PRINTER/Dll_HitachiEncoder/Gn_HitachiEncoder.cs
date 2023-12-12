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

namespace Dll_HitachiEncoder
{
    public class Gn_HitachiEncoder
    {
        #region Declaracion de Variables 
        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");

        private static bool bConexionWebCEDIS_Establecida = false; 
        private static clsDatosWebService datosDeWebServicePedidos = new clsDatosWebService();

        #endregion Declaracion de Variables

        #region Constructor 
        static Gn_HitachiEncoder()
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
        #endregion Propiedades 
    }
}
