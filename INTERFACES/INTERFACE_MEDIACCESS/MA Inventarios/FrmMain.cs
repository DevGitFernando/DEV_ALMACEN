using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Timers;

using System.IO;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using SC_SolutionsSystem.SistemaOperativo; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Informacion;
using DllFarmaciaSoft.Usuarios_y_Permisos; 

using Dll_SII_IMediaccess;

namespace MA_Inventarios
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        // clsGenearMenu menuArbol; 
        // string sVersion = Transferencia.Version;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leerVersion;
        CheckVersion buscarUpdate; 
        clsDatosApp datosDeModulo; 
        System.Timers.Timer tmUpdaterModulo;
        System.Timers.Timer tmUpdateEncontrado;
        Thread hilo; 

        string sNombreVersionSII = "Farmacia.SII";
        string sVersionSII = "0.0.0.0";

        string sNombreVersionSII_Ext = "FarmaciaExt.SII";
        string sVersionSII_Ext = "0.0.0.0";
        bool bBuscandoUpdate = false;
        bool bSeEncontroUpdate = false;

        Random x_tiempo_actualizacion; // = new Random(30);

        public FrmMain()
        {
            InitializeComponent();

            ////btnGetInformacion.Visible = false;
            ////btnExportarInformacion.Visible = false;
            ////btnInformación.Visible = false; 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Farmacia 
            DtGeneral.ModuloEnEjecucion = TipoModulo.Farmacia;
            DtGeneral.ModuloMA_EnEjecucion = TipoModulo_MA.Farmacia; 

            CheckForIllegalCrossThreadCalls = false; 

            clsAbrirForma.AssemblyActual("MA Inventarios");
            datosDeModulo = clsAbrirForma.DatosApp; 
            // GnFarmacia.DatosApp = clsAbrirForma.DatosApp;
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "MA Farmacia";

            // General.BackColorBarraMenu = Color.LightGray;
            // MessageBox.BackColor = Global.FormaBackColor;


            BarraDeStatus.Panels[lblMach4.Name].Text = "I";
            BarraDeStatus.Panels[lblMach4.Name].MinWidth = 0; 
            BarraDeStatus.Panels[lblMach4.Name].Width = 0; 
            
            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

            // Quitar 
            // GnFarmacia.MostrarPrecioVentaEnVentaCredito = true;

            //DtGeneral.EsAdministrador = false;
            //GnFarmacia.MostrarPrecios_y_Costos = false;

        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F11:
                        if (DtGeneral.EsAdministrador)
                        {
                            RevisarVersionInstaladaModulos(true); 
                        }
                        break; 

                    case Keys.F12: 
                        DtGeneral.InformacionConexion(); 
                        break; 

                    default:
                        break; 
                }
            }
        } 

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////if (!DtGeneral.ValidarModulosAbiertos())
            ////{
            ////    if (General.msjConfirmar("Se encontrarón opciones de sistema abiertas, ¿ Desea salir del sistema ?") == DialogResult.No)
            ////    {
            ////        e.Cancel = true; 
            ////    }
            ////}
        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            string sArbol = "PFAR"; 
            DtGeneral.SoloMostrarUnidadesConfiguradas = true; 
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "INV";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + datosDeModulo.Version; //// GnFarmacia.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal; 


                //MessageBox.Show(Login.Sucursal.ToString());
                bRegresa = true;
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = Login.Permisos;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show(); 
                Navegador.Activate();

                // this.Text = " Módulo " + Navegador.NombreModulo;
                this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;


                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                ////// 
                cnn = new clsConexionSQL(General.DatosConexion);
                leerVersion = new clsLeer(ref cnn);


                //////// Cargar los Parametros del sistema 
                if (DtGeneral.EsAlmacen)
                {
                    sArbol = "ALMN";
                }

                GnInventarios.Parametros = new clsParametrosPtoVta(General.DatosConexion, GnInventarios.DatosApp,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sArbol);
                GnInventarios.Parametros.CargarParametros();


                GnFarmacia.Parametros = new clsParametrosPtoVta(General.DatosConexion, GnInventarios.DatosApp,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sArbol);
                GnFarmacia.Parametros.CargarParametros();



                // Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnInventarios.RutaReportes; 



                ////// Revisar si el Equipo es el Servidor de Datos de la Red 
                GnFarmacia.EsServidorLocal();
                DtGeneral.DatosDeServicioWebUpdater.Servidor = DtGeneral.DatosDeServicioWeb.Servidor;
                DtGeneral.DatosDeServicioWebUpdater.WebService = DtGeneral.DatosDeServicioWeb.WebService;
                DtGeneral.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater";



                if (GnFarmacia.EsServidorDeRedLocal) 
                {
                    buscarUpdate = new CheckVersion(sNombreVersionSII, sVersionSII, sNombreVersionSII_Ext, sVersionSII_Ext, true);
                }
                else
                {
                    FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                    buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, sNombreVersionSII_Ext, sVersionSII_Ext, false);
                }

                //////// Checar la version instalada 
                //////string[] sModulos = { "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" }; 
                //////DtGeneral.RevisarVersion(sModulos); 
                RevisarVersionModulos(); 


                //wsFarmacia.wsCnnCliente x = new Farmacia.wsFarmacia.wsCnnCliente();
                //General.Url = x.Url; 

                //////tmSesionDeUsuario.Interval = (1000 * 60) * 2;
                //////tmSesionDeUsuario.Enabled = true;
                //////tmSesionDeUsuario.Start(); 

                // Administradores tienen acceso a toda la información. Ventas  
                if (DtGeneral.EsAdministrador)
                {
                    GnFarmacia.MostrarLotesSinExistencia = true;
                    //btnInformacion.Visible = true; 
                }


                //// Jesús Diaz 2K121108.1055 
                if (DtGeneral.EsServidorDeRedLocal && DtGeneral.UnidadConServidorDedicado)
                {
                    btnNavegador.Enabled = false;
                    btnNavegador.Visible = false;
                    btnCambiarPassword.Enabled = false;
                    btnCambiarPassword.Visible = false; 

                    try
                    {
                        Navegador.Close();
                    }
                    catch { }

                    General.msjAviso("El equipo actual es el servidor de la unidad, esta configurado como servidor dedicado, no es posible ingresar al módulo.");
                }
            }
            else
            {
                Application.Exit(); // this.Close();
            }

            return bRegresa;
        }

        private void RevisarVersionModulos()
        {
            Thread thVersion = new Thread(this.RevisarVersionInstaladaModulos);
            thVersion.Name = "RevisarVersionModulosInstalados";
            thVersion.Start();
        }

        private void RevisarVersionInstaladaModulos()
        {
            RevisarVersionInstaladaModulos(false);
        }

        private void RevisarVersionInstaladaModulos(bool MostrarInterface)
        {
            // Checar la version instalada 
            string[] sModulos = { "MA Farmacia", "Farmacia" };
            DtGeneral.RevisarVersion(sModulos, MostrarInterface);
        }

        private void btnNavegador_Click(object sender, EventArgs e)
        {
            if (!General.NavegadorCargado)
            {
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = General.ArbolDeNavegacion;
                Navegador.ListaIconos = General.IconosNavegacion;
                Navegador.Show();
                Navegador.Activate();
            }
        }

        private void tmDatosPersonalConectado_Tick(object sender, EventArgs e)
        {
            BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;
            BarraDeStatus.Panels[lblFechaSistema.Name].Text = "Fecha de Sistema : " + General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"); 
        }

        private void bntRegistroErrores_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarLogErrores();
            //FrmListadoFarmacias f = new FrmListadoFarmacias();
            //f.ShowDialog(); 
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarCambiarPasswordUsuario();
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
            {
                bBuscandoUpdate = true;
                buscarUpdate.CheckVersionModulo();
                bBuscandoUpdate = false;
            }
        }
    }
}
