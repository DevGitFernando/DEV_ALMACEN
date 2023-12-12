using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Web.Services;

using System.Threading;
using System.Timers;
using System.IO;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace DllRegistrosSanitarios
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        clsPing Ping = new clsPing();

        //CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        System.Timers.Timer tmUpdateEncontrado;

        Thread hilo;
        bool bBuscandoUpdate = false;
        bool bSeEncontroUpdate = false;

        string sNameSpace = "DllRegistrosSanitarios";

        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager manager; // = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager(); 

        public FrmMain()
        {
            InitializeComponent();

            ////DtGeneral.Modulo_Compras_EnEjecucion = TipoModuloCompras.Central;

            clsAbrirForma.AssemblyActual("Registros Sanitarios");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "OficinaCentral";
            manager = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager(); 

            //GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios = @"D:\BASES_DE_DATOS\SQLite";
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F11:
                        // DtGeneral.ValidarModulosAbiertos();
                        break;

                    case Keys.F12:
                        DtGeneral.InformacionConexion();
                        break;

                    default:
                        break;
                }
            }
        } 

        private bool pfLoginServidor()
        {
            bool bRegresa = false, bConexionEstablecida = false;
            DllFarmaciaSoft.wsOficinaCentral.wsCnnOficinaCentral webService = new DllFarmaciaSoft.wsOficinaCentral.wsCnnOficinaCentral();
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "";
            clsDatosConexion DatosCnn;

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + "Registros Sanitarios " + " " + GnRegistrosSanitarios.DatosApp.Version;
            if (!manager.ExisteArchivo())
            {
                Application.Exit(); 
            }
            else 
            ////if (Login.AutenticarUsuario())
            {

                ////bRegresa = true;
                ////Navegador = new FrmNavegador();
                ////Navegador.MdiParent = this;
                ////Navegador.Permisos = Login.Permisos;
                //////Navegador.ListaIconos = imgNavegacion_2;
                ////Navegador.ListaIconos = imgNavegacion;
                ////Navegador.Posicion = ePosicion.Izquierda;
                ////Login = null;
                ////Navegador.Show(); 

                //this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;

                ObtenerConfiguracion();
                webService.Url = General.Url;

                try
                {
                    dtsCnn = webService.ConexionEx(General.ArchivoIni);
                    bConexionEstablecida = true;
                }
                catch (Exception ex) { }


                if (bConexionEstablecida)
                {
                    // Inicializar el resto de las variables General.es
                    DatosCnn = new clsDatosConexion(dtsCnn);

                    General.CadenaDeConexion = DatosCnn.CadenaDeConexion;
                    General.ServidorEnRedLocal = Ping.Ping(DatosCnn.ServidorPing);
                    General.DatosConexion = DatosCnn;

                    // Precargar los datos de conexion 
                    //bConexionEstablecida = ObtenerDatosParaIniciarSesion_Regional();
                }


                bRegresa = true; 
                ObtenerConfiguracion(); 

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                GnRegistrosSanitarios.Parametros = new clsParametrosOficinaCentral(General.DatosConexion, GnRegistrosSanitarios.DatosApp, "COMP");
                GnRegistrosSanitarios.Parametros.CargarParametros();

                DtGeneral.RutaReportes = GnRegistrosSanitarios.RutaReportes;

            }
            ////else
            ////{
            ////    Application.Exit(); // this.Close();
            ////}

            return bRegresa;
        }

        private void ObtenerConfiguracion()
        {
            clsLeer leerIni = new clsLeer();

            string sConexionWeb = "";
            string sServidor = "";
            string sWebService = "";
            string sDireccion = "";
            string sPaginaASMX = "";
            string sImpresionViaWeb = "";
            string sSSL = ""; 



            leerIni.ReadXml(manager.XmlFile);
            leerIni.Leer();
            sConexionWeb = leerIni.Campo("ConexionWeb");
            sSSL = leerIni.Campo("SSL");
            sSSL = sSSL == "1" ? "s" : "";

            sServidor = leerIni.Campo("Servidor");
            sWebService = leerIni.Campo("WebService");
            sDireccion = string.Format("http{0}://{1}/{2}", sSSL, sServidor, sWebService);
            sPaginaASMX = leerIni.Campo("PaginaASMX");
            sImpresionViaWeb = leerIni.Campo("TipoImpresion");
            SC_SolutionsSystem.Idiomas.Idiomas.Idioma = leerIni.Campo("Lenguaje");

            // sPaginaASMX = "wsConexion.asmx";

            DataSet dtsCnn = new DataSet();
            DataSet dtsP = new DataSet();

            // Crear la conexion al servicio web, el servicio web siempre debe ser instalado
            // aunque este no sea utilizado por la aplicacion, esto para administrar en un solo
            // lugar el usuario y password de acceso al servidor de base de datos.

            General.WebService = null;
            General.PaginaASMX = sPaginaASMX;
            General.Url = sDireccion + General.PaginaASMX;
            General.UsarWebService = true;

        }

        private void btnSincronizarDatos_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmSincronizarDatos", sNameSpace, this); 
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            General.CargarPantalla("FrmRegistrosSanitariosReporte", sNameSpace, this); 
        }
    }
}
