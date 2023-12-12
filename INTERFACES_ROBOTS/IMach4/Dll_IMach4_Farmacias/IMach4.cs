using System;
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
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4
{    
    public static class IMach4
    {
        #region Declaracion de Variables 
        public static readonly int Registros_K = 10; 

        private static string sModulo = "Dll_IMach4";
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

        private static string sIdPersonalConectado = "";
        private static string sNombrePersonal = "";
        private static string sLoginUsuario = "";
        private static string sPassUser = "";
        private static bool bEsUsuarioTipoAdministrador = false;
        private static string sRutaReportes = "";

        private static DateTime dFechaMenorSistema = new DateTime();
        private static string[] sListaArboles = { "CFGC", "CFGN", "CFGS" };

        private static clsDatosApp dpDatosApp = new clsDatosApp("Dll_IMach4", Application.ProductVersion);
        public static string RutaLogo = Application.StartupPath + @"\SII_LOGO.jpg";
        // private static clsInfoUsuario infUsuario = new clsInfoUsuario(); 
        private static clsParametrosIMach pParametros; 

        // Configuracion de Terminal 
        private static string Name = "IMach4"; 
        private static clsConexionSQL cnn;
        private static clsLeer leer;
        private static clsGrabarError Error;

        private static bool bEsServidorInterface = false; 
        private static bool bEsClienteIMach4 = false;
        private static bool bEsClienteIMach4_Validado = false;

        private static string sIdCliente = ""; 
        private static string sTerminal = ""; 
        private static bool bTerminalValida = false; 

        private static string sPuertoDeSalida = ""; 
        #endregion Declaracion de Variables 
        
        static IMach4()
        {
            dFechaMenorSistema = Convert.ToDateTime("2010-05-01");
            //// SC_SolutionsSystem.Usuarios_y_Permisos.clsAbrirForma.AssemblyActual("DllFarmaciaSoft"); 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            //dpDatosApp = new clsDatosApp("DllFarmaciaSoft", "1.0.5.1"); 
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);

            // Error = new clsGrabarError(IMach4.DatosApp, "IMach4");
            Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name);
        }

        #region Propiedades 
        public static clsParametrosIMach Parametros
        {
            get { return pParametros; }
            set { pParametros = value; }
        }

        public static bool EsServidorDeInterface
        {
            get { return false;  }
        }

        public static string IdCliente
        {
            get { return sIdCliente; }
        }

        public static bool EsClienteIMach4
        {
            get { return false; }

        }

        public static string Terminal
        {
            get { return ""; }
        }

        public static string PuertoDeDispensacion
        {
            get { return ""; } 
        }         
        #endregion Propiedades
        
        #region Propiedades Generales
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
            get { return ""; }
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

        public static string RutaReportes
        {
            get
            {
                return "";
            }
            set { sRutaReportes = value; }
        }        
        #endregion Propiedades Generales 

        #region Funciones y Procedimientos Publicos 
        public static bool EsInterface()
        {
            bool bRegresa = false;           
            return bRegresa; 
        }

        public static void ReiniciarConexion()
        {
        }

        public static DataSet Permisos()
        {
            leer = new clsLeer(ref cnn); 
            DataSet dtsPermisos = new DataSet(); 
            return dtsPermisos;
        }

        public static void ValidarPuntoDeVenta()
        {
        }

        private static bool IMach4Instalado()
        {
            bool bRegresa = false; 
            return bRegresa;
        }

        private static bool ValidarTerminal()
        {
            bool bRegresa = true;
            return bRegresa; 
        }

        private static bool ValidarStatusTerminal()
        {
            bool bRegresa = false; 
            return bRegresa; 
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
