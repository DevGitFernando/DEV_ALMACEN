using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Usuarios_y_Permisos;

namespace Configuracion 
{
    public static class GnConfiguracion 
    {
        private static bool bEsOficinaCentral = false;
        private static clsDatosApp dpDatosApp; // = new clsDatosApp("Configuracion", Application.ProductVersion);

        static GnConfiguracion() 
        {
            //////string sRuta = Environment.SystemDirectory.Substring(0,1) + ":\\OficinaCentral.txt" ;
            //////bEsOficinaCentral = File.Exists(sRuta);

            //////clsAbrirForma.AssemblyActual("Configuracion");
            //////dpDatosApp = clsAbrirForma.DatosApp;

            ////// Jesús Díaz 2K120711.0900 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
        }

        public static bool EsOficinaCentral
        {
            get { return bEsOficinaCentral; }
        }

        #region Propieades Modulo 
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }
        #endregion Propieades Modulo 

        #region Especiales
        public static void CargarModulo(string NombreModulo)
        {
            clsAbrirForma.AssemblyActual(NombreModulo);
            dpDatosApp = clsAbrirForma.DatosApp;
        }
        #endregion Especiales
    }
}
