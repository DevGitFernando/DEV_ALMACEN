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
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllCompras;

namespace Compras
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        // string sVersion = Transferencia.Version;

        clsAuditoria auditoria;

        CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        System.Timers.Timer tmUpdateEncontrado;

        Thread hilo;
        bool bBuscandoUpdate = false;
        bool bSeEncontroUpdate = false;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Compras 
            DtGeneral.Modulo_Compras_EnEjecucion = TipoModuloCompras.Central;


            clsAbrirForma.AssemblyActual("Compras");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "Compras";
            // MessageBox.BackColor = Global.FormaBackColor;


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
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "COMP";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnCompras.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Unidad : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
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


                //// Cargar los Parametros del sistema 
                GnCompras.Parametros = new clsParametrosOficinaCentral(General.DatosConexion, GnCompras.DatosApp, "COMP");
                GnCompras.Parametros.CargarParametros();

                GnCompras.ParametrosCompras = new clsParametrosCompras(General.DatosConexion, GnCompras.DatosApp, 
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "COMP");
                GnCompras.ParametrosCompras.CargarParametros();


                //// Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnCompras.RutaReportes;
                DtGeneral.ConfirmacionConHuellas = GnCompras.ConfirmacionConHuellas; 

                ////string s = GnCompras.NombreTablaCompras; 

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                ////// Checar la version instalada 
                ////string[] sModulos = { "Compras" };
                ////DtGeneral.RevisarVersion(sModulos);
                RevisarVersionModulos();

                ConfigurarActualizacion(); 

                // Crear la instacia para el objeto de la clase de Auditoria
                auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                             DtGeneral.IdPersonal, DtGeneral.IdSesion, General.Modulo, this.Name, General.Version);

                auditoria.GuardarAud_LoginUni();
                DtGeneral.IdSesion = clsAuditoria.Sesion;
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
            //// Checar la version instalada 
            // string[] sModulos = { "Compras", "Compras Regional", "DllCompras" };
            string[] sModulos = { "Compras", "DllCompras" };
            DtGeneral.RevisarVersion(sModulos);
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
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            DtGeneral.MostrarCambiarPasswordUsuario();
        }

        private void ConfigurarActualizacion()
        {
            FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, "Updater Compras.exe", "C");

            tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
            tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed);
            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Start();

            tmUpdateEncontrado = new System.Timers.Timer((1000) * 2);
            tmUpdateEncontrado.Elapsed += new ElapsedEventHandler(this.tmUpdateEncontrado_Elapsed);
            tmUpdateEncontrado.Enabled = true;
            ////tmUpdateEncontrado.Start();
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
            {
                tmUpdaterModulo_Elapsed(null, null);
            } 
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdaterModulo.Stop();
            tmUpdaterModulo.Enabled = false;

            tmUpdateEncontrado.Enabled = true;
            tmUpdateEncontrado.Start();


            bool bConfirmarDescarga = DtGeneral.ConfirmarBusquedaDeActualizaciones();
            if (bConfirmarDescarga)
            {
                hilo = new Thread(this.ChecarVersion);
                hilo.Name = "UpdateComprasCentral";
                hilo.Start();
            }
            else
            {
                HabilitarActualizaciones();
                bBuscandoUpdate = false;
            }
        }

        private void tmUpdateEncontrado_Elapsed(object sender, ElapsedEventArgs e)
        {
            tmUpdateEncontrado.Stop();
            tmUpdateEncontrado.Enabled = false;

            if (!bBuscandoUpdate)
            {
                if (bSeEncontroUpdate)
                {
                    bSeEncontroUpdate = false;
                    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Compras Central. \n\n" +
                                           " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                    {
                        buscarUpdate.DescargarActualizacion();
                    }
                }
            }
            else
            {
                tmUpdateEncontrado.Enabled = true;
                tmUpdateEncontrado.Start();
            }
        }

        private void ChecarVersion()
        {
            bBuscandoUpdate = true;
            btnBuscarActualizaciones.Enabled = !bBuscandoUpdate;
            ////bool bConfirmarDescarga = DtGeneral.ConfirmarBusquedaDeActualizaciones();

            ////if (bConfirmarDescarga)
            {
                bSeEncontroUpdate = buscarUpdate.CheckVersionModulo();
                ////{
                ////    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Facturación. \n\n" +
                ////        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                ////    {
                ////        buscarUpdate.DescargarActualizacion();
                ////    }
                ////}
            }

            //tmUpdaterModulo.Enabled = true;
            //tmUpdaterModulo.Interval = (1000 * 30) * 10;
            //tmUpdaterModulo.Start();
            HabilitarActualizaciones();
            bBuscandoUpdate = false;
            btnBuscarActualizaciones.Enabled = !bBuscandoUpdate; 
        }

        private void HabilitarActualizaciones()
        {
            // Se detecto probable carga de trabajo para el Servidor de Actualizaciones. 
            // Los tiempos de consulta son variables, buscando disminuir la cargar para el servidor.

            Random x = new Random(30);
            int i = x.Next(10, 20);

            tmUpdaterModulo.Enabled = true;
            tmUpdaterModulo.Interval = (1000 * 60) * i;
            tmUpdaterModulo.Start();
        }

    }
}
