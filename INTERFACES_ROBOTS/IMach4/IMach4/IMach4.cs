using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.IO.Ports;
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
using SC_SolutionsSystem.FuncionesGenerales;


namespace Dll_IMach4
{ 
    public static class Dll_IMach4
    {
        public static int Registros_K = 10;

        private static string sModulo = "Dll_Dll_IMach4";
        private static string sVersion = "1.0.0.0";

        private static basGenerales Fg = new basGenerales();
        private static string sIdOficinaCentral = "OC";
        private static string sIdOficinaCentralNombre = "Oficina Central";
        private static string sIdFarmaciaCentral = "00SC";
        private static string sIdFarmaciaCentralNombre = "Central";

        private static string sIdEmpresa = "001";
        private static string sNombreEmpresa = "Intercontinental de Medicamentos";

        private static string sIdEstado = "";
        private static string sNombreEstado = "";
        private static string sClaveRenapo = "";
        private static string sIdFarmacia = "";
        private static string sNombreFarmacia = "";
        private static string sIdJurisdiccion = "";
        private static string sNombreJurisdiccion = "";
        private static bool bManejaVentaPublicoGral = false;
        private static bool bManejaControlados = false;
        private static bool bEsTipoAlmacen = false;
        private static bool bEsEmpDeConsignacion = false;

        private static bool bOficinaCentral = false;
        private static bool bConfOficinaCentral = false;

        private static string sIdPersonalConectado = "";
        private static string sNombrePersonal = "";
        private static string sLoginUsuario = "";
        private static string sPassUser = "";
        private static bool bEsUsuarioTipoAdministrador = false;
        private static string sRutaReportes = "";

        private static DateTime dFechaMenorSistema = new DateTime();
        private static string[] sListaArboles = { "CFGC", "CFGN", "CFGS" };

        private static clsDatosApp dpDatosApp = new clsDatosApp("Dll_Dll_IMach4", Application.ProductVersion);
        public static string RutaLogo = Application.StartupPath + @"\SII_LOGO.jpg";
        //private static clsInfoUsuario infUsuario = new clsInfoUsuario();

        static Dll_IMach4()
        {
            dFechaMenorSistema = Convert.ToDateTime("2009-05-01");
            // SC_SolutionsSystem.Usuarios_y_Permisos.clsAbrirForma.AssemblyActual("DllFarmaciaSoft"); 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            dpDatosApp = new clsDatosApp("DllFarmaciaSoft", "1.0.5.1");
        }

        #region Propiedades

        //public static clsInfoUsuario InfoUsuario
        //{
        //    get { return infUsuario; }
        //    set { infUsuario = value; }
        //}

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

        public static string EmpresaConectada
        {
            get { return sIdEmpresa; }
            set { sIdEmpresa = Fg.PonCeros(value, 3); }
        }

        public static string EmpresaConectadaNombre
        {
            get { return sNombreEmpresa; }
            set { sNombreEmpresa = value; }
        }

        public static bool EsEmpresaDeConsignacion
        {
            get { return bEsEmpDeConsignacion; }
            set { bEsEmpDeConsignacion = value; }
        }

        public static string EstadoConectado
        {
            get { return sIdEstado; }
            set { sIdEstado = Fg.PonCeros(value, 2); }
        }

        public static string EstadoConectadoNombre
        {
            get { return sNombreEstado; }
            set { sNombreEstado = value; }
        }

        public static string ClaveRENAPO
        {
            get { return sClaveRenapo; }
            set { sClaveRenapo = value.ToUpper(); }
        }

        public static string FarmaciaConectada
        {
            get { return sIdFarmacia; }
            set { sIdFarmacia = Fg.PonCeros(value, 4); }
        }

        public static string FarmaciaConectadaNombre
        {
            get { return sNombreFarmacia; }
            set { sNombreFarmacia = value; }
        }

        public static string IdPersonal
        {
            get { return sIdPersonalConectado; }
            set { sIdPersonalConectado = value; }
        }

        public static string NombrePersonal
        {
            get { return sNombrePersonal; }
            set { sNombrePersonal = value; }
        }

        public static string LoginUsuario
        {
            get { return sLoginUsuario; }
            set { sLoginUsuario = value; }
        }

        public static string PasswordUsuario
        {
            get { return sPassUser; }
            set
            {
                if (sPassUser == "")
                    sPassUser = value;
            }
        }

        public static bool EsAdministrador
        {
            get { return bEsUsuarioTipoAdministrador; }
            set { bEsUsuarioTipoAdministrador = value; }
        }

        public static DateTime FechaMinimaSistema
        {
            get { return dFechaMenorSistema; }
            set { dFechaMenorSistema = value; }
        }

        public static bool ManejaVentaPublico
        {
            get { return bManejaVentaPublicoGral; }
            set { bManejaVentaPublicoGral = value; }
        }

        public static bool ManejaControlados
        {
            get { return bManejaControlados; }
            set { bManejaControlados = value; }
        }

        public static bool EsAlmacen
        {
            get { return bEsTipoAlmacen; }
            set { bEsTipoAlmacen = value; }
        }

        public static string RutaReportes
        {
            get { return sRutaReportes; }
            set { sRutaReportes = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static void MostrarCambiarPasswordUsuario()
        {
            //FrmCambiarPassword pass = new FrmCambiarPassword();
            //pass.ShowDialog();
        }

        public static void MostrarLogErrores()
        {
            //if (DtGeneral.EsAdministrador)
            //{
            //    FrmListadoDeErrores ex = new FrmListadoDeErrores();
            //    ex.ShowDialog();
            //}
            //else
            //{
            //    General.msjUser("El usuario conectado no tiene permisos para accesar a esta opción.");
            //}
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
