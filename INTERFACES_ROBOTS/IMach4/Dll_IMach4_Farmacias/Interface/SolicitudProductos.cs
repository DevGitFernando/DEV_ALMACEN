using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;

using Dll_IMach4;
using Dll_IMach4.Protocolos;


namespace Dll_IMach4.Interface
{
    public static class SolicitudProductos
    {
        const int TmMinuto = 60;
        const int TmSegundo = 1000;

        static string Name = "Dll_IMach4.Interface.SolicitudProductos";
        static clsConexionSQL cnn;
        static clsLeer leer;
        static clsGrabarError Error;

        static int iTiempo = TmSegundo * (15);
        static Timer temporizador;

        static string sOrder = "";
        static string sOrderDesc = " Desc ";

        #region Constructor de Clase
        static SolicitudProductos()
        {
            ////cnn = new clsConexionSQL(General.DatosConexion);
            ////leer = new clsLeer(ref cnn);
            ////Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name); 
        }
        #endregion Constructor de Clase

        #region Propiedades
        public static double TiempoEnSegundosDecimales
        {
            get { return 0; }
            set { iTiempo = 0; }
        }

        public static int TiempoEnSegundos
        {
            get { return 0; }
            set { iTiempo = 0; }
        }

        public static int TiempoEnMinutos
        {
            get { return 0; }
            set { iTiempo = 0; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static void Iniciar()
        {
        }

        public static void Detener()
        {
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static void Solicitar_Productos(object sender, EventArgs e)
        {
        }

        private static void ObtenerSolicitud()
        {
        }
        #endregion Funciones y Procedimientos Privados
    }
}
