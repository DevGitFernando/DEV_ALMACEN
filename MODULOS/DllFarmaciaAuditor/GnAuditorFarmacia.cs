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
using SC_SolutionsSystem.Usuarios_y_Permisos;
using DllFarmaciaSoft;

using DllFarmaciaAuditor; 

using FarPoint.Win.Spread;

namespace DllFarmaciaAuditor
{
    public static class GnAuditorFarmacia
    {
        #region Declaracion de variables  
        //private static string sModulo = "Farmacia";
        //private static string sVersion = "";

        private static int iMesesCaducaMedicamento = 1;
        // private static string sIdCaja = "01";

        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");
        private static clsParametrosPtoVta pParametros;
        private static string sRutaReportes = "";
        private static DateTime dtpFechaOperacionSistema = DateTime.Now;
        private static double dTipoDeCambio = -1;

        private static bool bMostrarPreciosCostos = false;
        private static string sModuloTransferencia = "Servicio Cliente.exe";
        private static string sRutaServicio = Application.StartupPath + @"\\" + sModuloTransferencia;

        private static clsGrabarError Error = new clsGrabarError();
        // private static bool bEsServidorLocal = false; 
        private static bool bExisteServicio = File.Exists(sRutaServicio);
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");
        private static Color colorProductosIMach = Color.Yellow;
        // private static bool bValidarSesionUsuario = false; 

        public static string[] ListaFormas = { "FrmMain", "FrmNavegador" };
        #endregion Declaracion de variables

        static GnAuditorFarmacia()
        {
            ////clsAbrirForma.AssemblyActual("DllFarmaciaAuditor");
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

        #region Propiedades
        /// <summary>
        /// Indica los meses de caducidad a partir de la fecha actual que 
        /// puede tener un lote de medicamento para ser dado de alta.
        /// 2009-01-01 + 12 == > 2010-01-01
        /// </summary>
        public static int MesesCaducaMedicamento
        {
            get { return iMesesCaducaMedicamento; }
            set { iMesesCaducaMedicamento = value; }
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

        public static bool MostrarPrecios_y_Costos
        {
            get { return bMostrarPreciosCostos; }
            set { bMostrarPreciosCostos = value; }
        }

        #region Cierres de Periodos 
        private static int iDiasAdicionalesCierreTickets = 30;
        private static string sDiasAdicionalesCierreTickets = ""; 

        public static int DiasAdicionalesCierreTickets
        {
            get 
            {
                if (sDiasAdicionalesCierreTickets == "")
                {
                    sDiasAdicionalesCierreTickets = pParametros.GetValor("CierreDeTicketsDiasAdicionalesPermitido");
                    iDiasAdicionalesCierreTickets = pParametros.GetValorInt("CierreDeTicketsDiasAdicionalesPermitido"); 
                } 
                return iDiasAdicionalesCierreTickets; 
            } 

            set { iDiasAdicionalesCierreTickets = value; }
        }
        #endregion Cierres de Periodos

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

        public static clsParametrosPtoVta Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        } 
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos  
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private static void ObtenerTipoDeCambio()
        {
            dTipoDeCambio = 0;
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sSql = "Select * From Net_CFGC_TipoCambio (NoLock) ";
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener el Tipo de Cambio.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("El Tipo de Cambio de Dollar no esta configurado.\nNo sera posible recibir pago en Dolares.\n\nReportarlo al Departamento de Sistemas.");
                }
                else
                {
                    dTipoDeCambio = leer.CampoDouble("TipoDeCambio");
                }
            }
        }
        #endregion Funciones y Procedimientos Privados

    }
}
