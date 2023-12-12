using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace SC_CompressLib
{
    public static class GnCompress
    {
        // Datos de identificacion del Dll
        private static string sModulo = "SC_SolutionsSystem";
        private static string sVersion = "2.0.5.1";
        private static clsDatosApp dpDatosApp = new clsDatosApp("SC_SolutionsSystem", "3.0.0.0");

        static GnCompress()
        {
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
            get { return sModulo; }
        }

        public static string Version
        {
            get { return sVersion; }
        }
        #endregion Propieades Dll
    }
}
