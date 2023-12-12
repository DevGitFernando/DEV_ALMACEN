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

using DllFarmaciaSoft;

namespace DllFarmaciaSoft
{
    public class GnOficinaCentral
    {
        //private static string sModulo = "OficinaCentral";
        //private static string sVersion = ""; // Application.ProductVersion;

        private static clsDatosApp dpDatosApp = new clsDatosApp("OficinaCentral", "");
        private static clsParametrosOficinaCentral pParametros;
        private static string sRutaReportes = "";

        static GnOficinaCentral()
        {
            clsAbrirForma.AssemblyActual("OficinaCentral");
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

        #region Especiales
        public static void CargarModulo(string NombreModulo)
        {
            clsAbrirForma.AssemblyActual(NombreModulo);
            dpDatosApp = clsAbrirForma.DatosApp;
        }
        #endregion Especiales

        #region Propiedades 
        static bool bPermiteCambioPrecios = false;
        static bool bConfirmacionConHuellas = false;
        public static clsParametrosOficinaCentral Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
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

        public static bool SePermiteModificarPreciosLicitacion
        {
            get
            {
                try
                {
                    string sValor = pParametros.GetValor("PermitirCambioDePreciosLicitacion"); 
                    if ( sValor !="" )
                        bPermiteCambioPrecios = Convert.ToBoolean(sValor);
                }
                catch { }
                
                return bPermiteCambioPrecios;
            }
        }

        public static bool ConfirmacionConHuellas
        {
            get
            {
                try
                {
                    string sValor = pParametros.GetValor("ConfirmacionConHuellas");
                    if (sValor != "")
                        bConfirmacionConHuellas = Convert.ToBoolean(sValor);
                }
                catch { }

                return bConfirmacionConHuellas;
            }
        } 

        #endregion Propiedades 
    }
}
