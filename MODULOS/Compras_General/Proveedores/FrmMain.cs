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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllProveedores;
using DllProveedores.Usuarios_y_Permisos;

namespace Proveedores
{
    public partial class FrmMain : Form
    {
        DllProveedores.Usuarios_y_Permisos.clsLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        // string sVersion = Transferencia.Version;

        CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        Thread hilo; 

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            clsAbrirForma.AssemblyActual("Proveedores");
            // General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);
            General.CargarImagenMDI(this, Color.White, "");
            General.IconoSistema = this.Icon;

            // DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "Proveedores";
            // MessageBox.BackColor = Global.FormaBackColor;


            if (!bUsuarioLogeado)
            {
                pfLoginServidor();
            }

            General.ServidorEnRedLocal = true;

        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllProveedores.Usuarios_y_Permisos.clsLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "PROV";

            //// Ocultar los Tags 
            //BarraDeStatus.Panels[lblFarmacia.Name].Width = 0;
            //BarraDeStatus.Panels[lblServidor.Name].Width = 0;
            //BarraDeStatus.Panels[lblBaseDeDatos.Name].Width = 0;

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnProveedores.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Proveedor : " + GnProveedores.NombreProveedor; 
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + GnProveedores.XmlConfig.GetValues("Servidor");
                ////BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                ////BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = Login.Permisos;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show();

                this.Text = " Módulo : " + Navegador.NombreModulo; //  +"         " + " Proveedores ";  //DtGeneral.EmpresaConectadaNombre;


                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30; 

                // Cargar los Parametros del sistema 
                GnProveedores.Parametros = new clsParametrosProveedores(General.Url, GnProveedores.DatosApp, "PROV");
                GnProveedores.Parametros.CargarParametros();

                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, false);

                tmUpdaterModulo = new System.Timers.Timer((1000 * 60) * 1);
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
            ////DtGeneral.MostrarLogErrores();
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            GnProveedores.MostrarCambiarPasswordUsuario();
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdaterModulo.Stop();
            tmUpdaterModulo.Enabled = false;

            hilo = new Thread(this.ChecarVersion);
            hilo.Name = "UpdateProveedores";
            hilo.Start();
        }

        private void ChecarVersion()
        {
            if (buscarUpdate.CheckVersionModulo())
            {
                if (General.msjConfirmar("Se encontro una actualización para el Módulo de Proveedores. \n\n" +
                    " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                {
                    buscarUpdate.DescargarActualizacion();
                }
            }

            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Interval = (1000 * 60) * 5;
            tmUpdaterModulo.Start();
        } 
    }
}
