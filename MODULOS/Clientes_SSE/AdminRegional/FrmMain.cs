using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using System.Threading;
using System.Timers;
using System.IO;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllPedidosClientes;
// using DllFarmaciaSoft.Informacion;
using DllPedidosClientes.Usuarios_y_Permisos;

namespace AdminRegional
{
    public partial class FrmMain : Form
    {

        DllPedidosClientes.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        // string sVersion = ""; // Transferencia.Version;

        CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        Thread hilo;

        bool bBuscandoUpdate = false;

        public FrmMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            clsAbrirForma.AssemblyActual("Administracion Regional");
            General.CargarImagenMDI(this, Color.White, DtGeneralPedidos.RutaLogo); 

            General.IconoSistema = this.Icon;

            // DtGeneralPedidos.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "SII-Regional";
            // MessageBox.BackColor = Global.FormaBackColor;


            //// Especificar el Tipo de Cliente Conectado 
            DtGeneralPedidos.ClienteConectado = TipoDeClienteExterno.Administracion_Regional; 


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
                switch ( e.KeyCode )
                {
                    case Keys.F11:
                        CargarParametros(); 
                        break; 

                    case Keys.F12:
                        DtGeneralPedidos.InformacionConexion();
                        break;

                    default:
                        break; 
                } 
            }
        } 

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllPedidosClientes.Usuarios_y_Permisos.clsEdoLogin();
            Login.Arbol = "CTER"; 

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnAdminRegional.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneralPedidos.EstadoConectado + " -- " + DtGeneralPedidos.EstadoConectadoNombre; // +"      " + " Farmacia : " + DtGeneralPedidos.FarmaciaConectada + " -- " + DtGeneralPedidos.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;

                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "";

                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneralPedidos.NombrePersonal;

                bRegresa = true;
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = Login.Permisos;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show();

                this.Text = " Módulo : " + Navegador.NombreModulo + "         "; ///// +DtGeneralPedidos.EmpresaConectadaNombre;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                ////////// 2K110710.2200 Jesus Diaz   No son necesarios los Parametros 
                //////// Cargar los Parametros del sistema 
                //////// GnAdminRegional.Parametros = new clsParametrosClienteRegional(General.DatosConexion, GnAdminRegional.DatosApp, "CTER");

                //////// if (DtGeneralPedidos.EsEquipoDeDesarrollo)
                //////{
                //////    GnAdminRegional.Parametros = new clsParametrosClienteRegional(General.Url, General.ArchivoIni, GnAdminRegional.DatosApp, "CTER");
                //////    GnAdminRegional.Parametros.CargarParametros(DtGeneralPedidos.EsEquipoDeDesarrollo);
                //////    DtGeneralPedidos.TipoDeConexion = GnAdminRegional.TipoDeConexion;
                    
                //////    ///////// QUITAR 
                //////    ////DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad;  
                //////}

                //////// Pasar la ruta de reportes al General 
                //////DtGeneralPedidos.RutaReportes = GnAdminRegional.RutaReportes;
                //////DtGeneralPedidos.EncabezadoPrincipal = GnAdminRegional.EncabezadoPrincipal;
                //////DtGeneralPedidos.EncabezadoSecundario = GnAdminRegional.EncabezadoSecundario;


                CargarParametros(); 

                // Obtener solo una vez los Estados y Farmacias 
                if (!DtGeneralPedidos.ObtenerEstados_Farmacias())
                {
                    // Application.Exit(); 
                }

                //////// Checar la version instalada 
                //////string[] sModulos = { "AdminRegional", "DllPedidosClientes" };
                //////DtGeneralPedidos.RevisarVersion(sModulos);
                RevisarVersionModulos(); 

                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, false); 

                tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed);
                tmUpdaterModulo.Enabled = true;
                tmUpdaterModulo.Start(); 
            }
            else
            {
                Application.Exit(); // this.Close();
            }

            return bRegresa;
        }

        private void CargarParametros()
        {
            GnAdminRegional.Parametros = new clsParametrosClienteRegional(General.Url, General.ArchivoIni, GnAdminRegional.DatosApp, "CTER");
            GnAdminRegional.Parametros.CargarParametros(DtGeneralPedidos.EsEquipoDeDesarrollo);
            
            // Tipo de conexion que se implementa en las pantallas 
            DtGeneralPedidos.TipoDeConexion = GnAdminRegional.TipoDeConexion;


            // Pasar la ruta de reportes al General 
            DtGeneralPedidos.RutaReportes = GnAdminRegional.RutaReportes;
            DtGeneralPedidos.EncabezadoPrincipal = GnAdminRegional.EncabezadoPrincipal;
            DtGeneralPedidos.EncabezadoSecundario = GnAdminRegional.EncabezadoSecundario;

            GnAdminRegional.SubClienteNombre = GnAdminRegional.SubClienteNombre;
        }

        private void RevisarVersionModulos()
        {
            Thread thVersion = new Thread(this.RevisarVersionInstaladaModulos);
            thVersion.Name = "RevisarVersionModulosInstalados";
            thVersion.Start();
        }

        private void RevisarVersionInstaladaModulos()
        {
            // Checar la version instalada 
            string[] sModulos = { "AdminRegional", "DllPedidosClientes" };
            DtGeneralPedidos.RevisarVersion(sModulos);
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
            }
        }

        private void bntRegistroErrores_Click(object sender, EventArgs e)
        {
        }


        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            DtGeneralPedidos.MostrarCambiarPasswordUsuario();
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdaterModulo.Stop();
            tmUpdaterModulo.Enabled = false;

            btnBuscarActualizaciones.Enabled = false;
            if (!DtGeneralPedidos.EsEquipoDeDesarrollo)
            {
                hilo = new Thread(this.ChecarVersion);
                hilo.Name = "UpdateAdminRegional";
                hilo.Start();
            }
        }

        private void ChecarVersion()
        {
            bBuscandoUpdate = true; 
            if (!DtGeneralPedidos.EsEquipoDeDesarrollo)
            {
                if (buscarUpdate.CheckVersionModulo())
                {
                    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Administración Regional. \n\n" +
                        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                    {
                        buscarUpdate.DescargarActualizacion();
                    }
                }
            }

            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Interval = (1000 * 30) * 10;
            tmUpdaterModulo.Start();

            bBuscandoUpdate = false;
            btnBuscarActualizaciones.Enabled = true; 
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
            {
                tmUpdaterModulo_Elapsed(null, null);
            }
        }
    }
}
