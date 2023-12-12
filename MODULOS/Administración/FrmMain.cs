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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Informacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllTransferenciaSoft;
using DllAdministracion;

namespace Administracion
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        string sVersion = Transferencia.Version;

        CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        Thread hilo;
        bool bBuscandoUpdate = false; 
        Random x_tiempo_actualizacion; // = new Random(30);


        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            clsAbrirForma.AssemblyActual("Administración");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "Administración";
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
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "ADMI";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnAdministracion.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                bRegresa = true;
                Navegador = new FrmNavegador();
                Navegador.MdiParent = this;
                Navegador.Permisos = Login.Permisos;
                //Navegador.ListaIconos = imgNavegacion_2;
                Navegador.ListaIconos = imgNavegacion;
                Navegador.Posicion = ePosicion.Izquierda;
                Login = null;
                Navegador.Show();

                this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.SinLimite; 

                // Cargar los Parametros del sistema 
                GnAdministracion.Parametros = new clsParametrosAdministracion(General.DatosConexion, GnAdministracion.DatosApp, "ADMI");
                GnAdministracion.Parametros.CargarParametros();

                // Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnAdministracion.RutaReportes;

                // Checar la version instalada 
                string[] sModulos = { "Administración", "DllAdministracion" };
                DtGeneral.RevisarVersion(sModulos);

                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, false); 

                tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed);
                tmUpdaterModulo.Enabled = true;
                tmUpdaterModulo.Start();
                x_tiempo_actualizacion = new Random(30); 
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
            DtGeneral.MostrarLogErrores();

            //FrmListadoFarmacias f = new FrmListadoFarmacias();
            //f.ShowDialog(); 
        }


        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarCambiarPasswordUsuario();
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdaterModulo.Stop();
            tmUpdaterModulo.Enabled = false;

            btnBuscarActualizaciones.Enabled = false; 
            hilo = new Thread(this.ChecarVersion);
            hilo.Name = "UpdateAdministracion";
            hilo.Start(); 
        }

        private void ChecarVersion()
        {
            bBuscandoUpdate = true;
            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                if (buscarUpdate.CheckVersionModulo()) 
                {
                    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Administración. \n\n" + 
                        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                    {
                        buscarUpdate.DescargarActualizacion();
                    }
                }
            }

            //////tmUpdaterModulo.Enabled = true;
            //////tmUpdaterModulo.Interval = (1000 * 30) * 10;
            //////tmUpdaterModulo.Start();

            HabilitarActualizaciones();
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

        private void HabilitarActualizaciones()
        {
            // Se detecto probable carga de trabajo para el Servidor de Actualizaciones. 
            // Los tiempos de consulta son variables, buscando disminuir la cargar para el servidor.

            //Random x_tiempo_actualizacion = new Random(30);
            int i = x_tiempo_actualizacion.Next(5, 20);

            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Interval = (1000 * 60) * i;
            tmUpdaterModulo.Start();
        }
    }
}
