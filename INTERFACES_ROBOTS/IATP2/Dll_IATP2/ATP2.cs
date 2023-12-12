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

namespace Dll_IATP2
{
    public enum EventoLog
    {
        DecodificacionDeDatos = 1,  
        EnvioDeDatos = 2, 
        RecepcionDeDatosPuros = 4, 
        RecepcionDeDatos = 5, 

        Conexion = 100,  
        Error = 101, 
        Desconexion = 102, 
        ErrorRecepcion = 103 
    }

    public static class IATP2
    {
        #region Declaracion de Variables 
        public static readonly int Registros_K = 10;

        // private static string sModulo = "Dll_IATP2";
        // private static string sVersion = "1.0.0.0";

        private static basGenerales Fg = new basGenerales();
        // private static string sIdOficinaCentral = "OC";
        // private static string sIdOficinaCentralNombre = "Oficina Central";
        // private static string sIdFarmaciaCentral = "00SC";
        // private static string sIdFarmaciaCentralNombre = "Central";

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

        private static clsDatosApp dpDatosApp = new clsDatosApp("Dll_IATP2", Application.ProductVersion);
        public static string RutaLogo = Application.StartupPath + @"\SII_LOGO.jpg";
        private static clsParametrosIATP2 pParametros; 

        // Configuracion de Terminal 
        private static string Name = "IATP2"; 
        private static clsConexionSQL cnn;
        private static clsLeer leer;
        private static clsGrabarError Error;

        private static bool bAPT2Instalado = false;
        private static bool bAPT2Instalado_Validado = false; 
        private static bool bEsServidorInterface = false;
        private static bool bEsClienteIATP2 = false;
        private static bool bEsClienteIATP2_Validado = false;

        private static string sIdCliente = ""; 
        private static string sTerminal = "";
        private static bool bTerminalValida = false;

        private static bool bProbar_R = false;
        private static bool bSolicitud_De_Producto = false;
        private static bool bSolicitud_De_Existencia__A = false; 

        ////private static string sPuertoDeSalida = "";
        ////private static string sProtocolVersion = "0113";
        ////private static bool bEsMultiDosis = false;

        private static string sServidorTCP_IP = "";
        private static int iPuertoTCP_IP_Remoto = 0;
        private static int iPuertoTCP_IP_Local = 0;
        private static string sPuertoDeSalida = "";
        private static string sProtocolVersion = "0113";
        private static bool bEsMultiDosis = false;
        private static bool bHabilitarLogDeTexto = false; 

        private static string sFileOutput_K = "";


        private static string sRutaRepositorio_OrdenesAcondicionamiento = "";
        private static string sRutaRepositorio_OrdenesAcondicionamiento_Respuesta = "";
        #endregion Declaracion de Variables 
        
        static IATP2()
        {
            dFechaMenorSistema = Convert.ToDateTime("2018-02-01");
            //// SC_SolutionsSystem.Usuarios_y_Permisos.clsAbrirForma.AssemblyActual("DllFarmaciaSoft"); 
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            //dpDatosApp = new clsDatosApp("DllFarmaciaSoft", "1.0.5.1"); 

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);

            // Error = new clsGrabarError(IATP2.DatosApp, "IATP2");
            Error = new clsGrabarError(General.DatosConexion, IATP2.DatosApp, Name);

            //// Cargar parametros de operación Mach4-Interface 
            //CargarParametrosIATP2(); 
        }

        #region Propiedades 
        private static void CargarParametrosIATP2()
        {
            pParametros = new clsParametrosIATP2(General.DatosConexion, IATP2.DatosApp);
            pParametros.CargarParametros();
        }

        public static clsParametrosIATP2 Parametros
        {
            get
            {
                if (pParametros == null)
                {
                    CargarParametrosIATP2();
                }

                return pParametros;
            }
            set { pParametros = value; }
        }

        public static bool EsServidorDeInterface
        {
            get { return bEsServidorInterface;  }
        }

        public static string IdCliente
        {
            get { return sIdCliente;  }
        }

        public static bool EsClienteIATP2
        {
            get { return bEsClienteIATP2; }
            ////set 
            ////{
            ////    // Solo se puede asignar este valor una vez por cada inicio de sesión.
            ////    if (!bEsClienteIATP2_Validado)
            ////    {
            ////        bEsClienteIATP2_Validado = true; 
            ////        bEsClienteIATP2 = value; 
            ////    }
            ////}
        }

        public static string Terminal
        {
            get { return sTerminal; }
        }

        public static bool EsTerminalValida
        {
            get { return bTerminalValida; } 
        }

        public static string PuertoDeDispensacion
        {
            get { return sPuertoDeSalida; } 
        }

        public static bool ATP2_Instalado
        {
            get 
            {
                if (!bAPT2Instalado_Validado)
                {
                    IATP2_Configurable(); 
                }
                return bAPT2Instalado;  
            }
        }

        public static void Mensaje__ATP2_NoInstalado()
        {
            General.msjAviso("La Unidad conectada no puede ser configurada para utilizar el equipo Unidosis ATP2."); 
        }

        public static string RutaRepositorio_OrdenesAcondicionamiento
        {
            get { return sRutaRepositorio_OrdenesAcondicionamiento; }
            set { sRutaRepositorio_OrdenesAcondicionamiento = value; }
        }

        public static string RutaRepositorio_OrdenesAcondicionamiento_Respuesta
        {
            get { return sRutaRepositorio_OrdenesAcondicionamiento_Respuesta; }
            set { sRutaRepositorio_OrdenesAcondicionamiento_Respuesta = value; }
        }

        public static bool EsMultiDosis
        {
            get { return bEsMultiDosis; }
            set { bEsMultiDosis = value; }
        }

        public static bool HabilitarLogDeTexto
        {
            get { return bHabilitarLogDeTexto; }
            set { bHabilitarLogDeTexto = value; }
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
            get { return sPassUser; }
            set 
            {
                if (sPassUser == "")
                {
                    sPassUser = value;
                }
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
                ////if (sRutaReportes == "")
                ////{
                ////    sRutaReportes = pParametros.GetValor("RutaReportes");
                ////}
                return sRutaReportes;
            }
            set { sRutaReportes = value; }
        }
        #endregion Propiedades Generales 

        #region Funciones y Procedimientos Publicos 
        public static bool EsInterface()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select IdTerminal, Nombre, replace(MAC_Address, '-', '') as MAC_Address, Status, Actualizado " +
                " From IMach_CFGC_Terminales (NoLock) " + 
                " " + 
                " Where replace(MAC_Address, '-', '') = '{0}' and EsDeSistema = 1 ", General.MacAddress); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "EsInterface()");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("La Terminal Conectada no es el Servidor de Comunicación con el Robot, contactar a Sistemas.");
                }
                else
                {
                    bRegresa = true;
                    bEsClienteIATP2 = false; 
                    sTerminal = leer.Campo("IdTerminal"); 
                }
            }

            bEsServidorInterface = bRegresa; 
            return bRegresa; 
        }

        public static void ReiniciarConexion()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, IATP2.DatosApp, Name);
        }

        public static DataSet Permisos()
        {
            leer = new clsLeer(ref cnn); 
            DataSet dtsPermisos = new DataSet(); 
            string sSql = string.Format(" Exec sp_Navegacion '{0}' ", "IM4");

            if (!leer.Exec("Arbol", sSql))
            {
                Error.GrabarError(leer, "Permisos()");
                General.msjError("Ocurrió un error al Cargar los Permisos."); 
            }
            else
            {
                dtsPermisos = leer.DataSetClase; 
            }
            return dtsPermisos;
        }

        public static void ValidarPuntoDeVenta()
        {
            leer = new clsLeer(ref cnn);
            //Error = new clsGrabarError(IATP2.DatosApp, Name);
            Error = new clsGrabarError(General.DatosConexion, IATP2.DatosApp, Name);

            // Revisar si la Unidad tiene IATP2 
            if (IATP2Instalado()) 
            {
                if (ValidarTerminal())
                {
                    if (ValidarStatusTerminal())
                    {
                        ValidarMultipicking(); 
                    }
                }
            }
        }

        private static bool IATP2_Configurable()
        {
            string sSql = string.Format(" Select Name From Sysobjects (NoLock) Where Name = 'IATP2_CFGC_Clientes' and xType = 'U' ",
                IATP2.EstadoConectado, IATP2.FarmaciaConectada); 

            bAPT2Instalado = false;
            bAPT2Instalado_Validado = true; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "IATP2Instalado()");
            }
            else
            {
                bAPT2Instalado = leer.Leer();
            }
            return bAPT2Instalado;
        }

        private static bool IATP2Instalado()
        {
            bool bRegresa = false;
            string sSql = string.Format(" Select Name From Sysobjects (NoLock) Where Name = 'IATP2_CFGC_Clientes' and xType = 'U' ",
                IATP2.EstadoConectado, IATP2.FarmaciaConectada);

            bAPT2Instalado = false; 
            if ( IATP2_Configurable() )
            {
                ////if (leer.Leer())
                {
                    sSql = string.Format(" Select * " +
                        " From IATP2_CFGC_Clientes (NoLock) where IdEstado = '{0}' and IdFarmacia = '{1}' ",
                        IATP2.EstadoConectado, IATP2.FarmaciaConectada);

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "IATP2Instalado()");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            sIdCliente = leer.Campo("IdCliente");
                            bRegresa = true;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private static bool ValidarTerminal()
        {
            bool bRegresa = true;
            string sFuncion = "ValidarTerminal()";
            string sSql = string.Format(" Select * From IATP2_CFGC_Terminales (NoLock) Where replace(MAC_Address, '-', '') = '{0}' ", General.MacAddress.Replace("-", ""));

            bTerminalValida = false; 
            if (!bEsClienteIATP2_Validado)
            {
                bRegresa = false;
                bEsClienteIATP2_Validado = true; 
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, sFuncion);
                    General.msjError("Ocurrió un error al Válidar la Terminal.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        Error.GrabarError("Terminal  " + General.NombreEquipo + " No se encuentra dada de Alta como terminal para Robot.  ", sFuncion);
                        General.msjAviso("La Terminal Actual no esta registrada para trabajar con el Robot Dispensador.\n\nReportarlo al Departamento de Sistemas.");
                    }
                    else
                    {
                        if (leer.Campo("Status").ToUpper() == "A")
                        {
                            sTerminal = leer.Campo("IdTerminal"); 
                            bTerminalValida = true;
                            bRegresa = true; 
                        }
                        else if (leer.Campo("Status").ToUpper() == "C")
                        {
                            General.msjAviso("La Terminal Conectada actualmente se encuentra cancelada, no podra solicitar Medicamentos del Robot."); 
                        }
                    }
                }
            }

            return bRegresa; 
        }

        private static bool ValidarStatusTerminal()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select IdCliente, IdTerminal, Asignada, Activa, PuertoDispensacion, Status, Actualizado " +
                " From IATP2_CFGC_Clientes_Terminales (NoLock) " + 
                " Where IdCliente = '{0}' and IdTerminal = '{1}' and Activa = 1 and Status = 'A' ", sIdCliente, sTerminal );

            bEsClienteIATP2 = false; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarStatusTerminal()"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("La Terminal Conectada no esta registrada para trabajar con el Robot, contactar a Sistemas."); 
                }
                else 
                {
                    bRegresa = true;
                    bEsClienteIATP2 = true;
                    sPuertoDeSalida = leer.Campo("PuertoDispensacion"); 
                }
            }

            return bRegresa; 
        }

        private static bool ValidarMultipicking()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select NombreParametro, Valor, Descripcion " +
                " From IATP2_Net_Parametros (NoLock) " +
                " Where NombreParametro = 'Multipicking' ");

            IATP2.EsMultiDosis = false; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarStatusTerminal()");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    IATP2.EsMultiDosis = leer.CampoBool("Valor");
                }
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

    }
}
