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

//////using DllFarmaciaSoft;
//////using DllFarmaciaSoft.Informacion;
//////using DllFarmaciaSoft.Usuarios_y_Permisos;
using Dll_SII_IMediaccess;

using DllRecursosHumanos;
using DllRecursosHumanos.Usuarios_y_Permisos;

namespace MA_Recursos_Humanos
{
    public partial class FrmMain : Form
    {
        DllRecursosHumanos.Usuarios_y_Permisos.clsLogin Login;
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
            DllRecursosHumanos.DtGeneral.ModuloEnEjecucion = DllRecursosHumanos.TipoModulo.Regional;
            ////DllFarmaciaSoft.DtGeneral.ModuloMA_EnEjecucion = TipoModulo_MA.Ninguno; 


            CheckForIllegalCrossThreadCalls = false; 

            clsAbrirForma.AssemblyActual("MA Recursos Humanos");
            datosDeModulo = clsAbrirForma.DatosApp; 
            // GnFarmacia.DatosApp = clsAbrirForma.DatosApp;
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DllRecursosHumanos.DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "MA Recursos Humanos";

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
                        ////if (DtGeneral.EsAdministrador)
                        ////{
                        ////    RevisarVersionInstaladaModulos(true); 
                        ////}
                        break; 

                    case Keys.F12:
                        DllRecursosHumanos.DtGeneral.InformacionConexion(); 
                        break; 

                    default:
                        break; 
                }
            }
        } 

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllRecursosHumanos.Usuarios_y_Permisos.clsLogin(); //  ("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "RHM";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + datosDeModulo.Version; //// GnFarmacia.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Unidad : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
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
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.SinLimite; 

                ////// 
                cnn = new clsConexionSQL(General.DatosConexion); 
                leerVersion = new clsLeer(ref cnn);

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                //////// Cargar los Parametros del sistema 
                GnRecursosHumanos.ParametrosRH = new clsParametrosRH(General.DatosConexion, GnRecursosHumanos.DatosApp, "RHM");
                GnRecursosHumanos.ParametrosRH.CargarParametros();

                //////// Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnRecursosHumanos.RutaReportes;

                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, sNombreVersionSII_Ext, sVersionSII_Ext, false);

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
            string[] sModulos = { "MA Farmacia", "Farmacia" };
            ////DtGeneral.RevisarVersion(sModulos, MostrarInterface);
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

        private void ChecarVersion()
        {
            bBuscandoUpdate = true;
            btnBuscarActualizaciones.Enabled = !bBuscandoUpdate;
            ////bool bConfirmarDescarga = DtGeneral.ConfirmarBusquedaDeActualizaciones();

            ////////if (bConfirmarDescarga)
            ////{
            ////    bSeEncontroUpdate = buscarUpdate.CheckVersionModulo();
            ////    ////{
            ////    ////    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Farmacia. \n\n" +
            ////    ////        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
            ////    ////    {
            ////    ////        buscarUpdate.DescargarActualizacion();
            ////    ////    }
            ////    ////}
            ////}

           
            bBuscandoUpdate = false;
            btnBuscarActualizaciones.Enabled = !bBuscandoUpdate; 
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

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            RevisarVersionInstaladaModulos(true);
        }

    }
}
