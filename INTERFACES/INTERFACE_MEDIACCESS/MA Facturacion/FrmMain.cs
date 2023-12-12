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

using Dll_MA_IFacturacion;
using Dll_SII_IMediaccess; 

namespace MA_Facturacion
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        // string sVersion = ""; // Transferencia.Version;

        Dll_SII_IMediaccess.CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo;
        System.Timers.Timer tmUpdateEncontrado;

        Thread hilo;
        string sNombreVersionSII = "Farmacia.SII";
        string sVersionSII = "0.0.0.0";

        string sNombreVersionSII_Ext = "FarmaciaExt.SII";
        string sVersionSII_Ext = "0.0.0.0";
        bool bBuscandoUpdate = false;
        bool bSeEncontroUpdate = false;

        //////Para Auditoria
        // clsAuditoria auditoria;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //// Servidor Regional, solo cuando se conecta a un Servidor Regional  
            DtGeneral.ModuloEnEjecucion = TipoModulo.Facturacion;
            DtIFacturacion.ModuloActivo = Modulo_CFDI.Facturacion_Centralizada; 
            DtIFacturacion.EsquemaDeFacturacion = eEsquemaDeFacturacion.Libre; 


            clsAbrirForma.AssemblyActual("MA Facturacion");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "MA Facturación";
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
                    ////case Keys.F10:
                    ////    if  (DtGeneral.EsEquipoDeDesarrollo ) 
                    ////    {
                    ////        Dll_MA_IFacturacion.PACs.FD.FrmInterface_FD fx = new Dll_MA_IFacturacion.PACs.FD.FrmInterface_FD(DtIFacturacion.PAC_Informacion);
                    ////        fx.ShowDialog();
                    ////    }
                    ////    break;

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

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "MFCT";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + DtIFacturacion.DatosApp.Version;
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

                //// Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                //// Cargar los Parametros del sistema 
                DtIFacturacion.Parametros = new clsParametrosFacturacion(General.DatosConexion, GnFarmacia.DatosApp,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.ArbolModulo);
                DtIFacturacion.Parametros.CargarParametros();


                //// Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = DtIFacturacion.RutaReportes;

                //// Preparar las variables para generar CFDI's
                DtIFacturacion.InicializarInformacionEmisor();


                RevisarVersionModulos();

                ////FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                ////buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, false);


                DtGeneral.DatosDeServicioWebUpdater.Servidor = DtGeneral.DatosDeServicioWeb.Servidor;
                DtGeneral.DatosDeServicioWebUpdater.WebService = DtGeneral.DatosDeServicioWeb.WebService;
                DtGeneral.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater";
                
                FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                buscarUpdate = new Dll_SII_IMediaccess.CheckVersion(f.OriginalFilename, f.FileVersion, sNombreVersionSII_Ext, sVersionSII_Ext, false);



                //////tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                //////tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed);
                //////tmUpdaterModulo.Enabled = true;
                //////tmUpdaterModulo.Start();

                //////tmUpdateEncontrado = new System.Timers.Timer((1000) * 2);
                //////tmUpdateEncontrado.Elapsed += new ElapsedEventHandler(this.tmUpdateEncontrado_Elapsed);
                //////tmUpdateEncontrado.Enabled = true;
                //////////tmUpdateEncontrado.Start();



                ////////////////// 2K120625.1640 
                ////////////////// Crear la instacia para el objeto de la clase de Auditoria
                ////////////auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                ////////////                             DtGeneral.IdPersonal, DtGeneral.IdSesion, General.Modulo, this.Name, General.Version);

                ////////////auditoria.GuardarAud_LoginUni();
                ////////////DtGeneral.IdSesion = clsAuditoria.Sesion;
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
            string[] sModulos = { "Configuracion", "OficinaCentral", "Facturacion", "Dll_IFacturacion" };
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
            //////tmUpdaterModulo.Stop();
            //////tmUpdaterModulo.Enabled = false;

            //////tmUpdateEncontrado.Enabled = true;
            //////tmUpdateEncontrado.Start();


            //////bool bConfirmarDescarga = DtGeneral.ConfirmarBusquedaDeActualizaciones(); 
            //////if (bConfirmarDescarga)
            //////{
            //////    hilo = new Thread(this.ChecarVersion);
            //////    hilo.Name = "UpdateFacturacion";
            //////    hilo.Start();
            //////}
            //////else
            //////{
            //////    HabilitarActualizaciones();
            //////    bBuscandoUpdate = false; 
            //////}
        }

        private void tmUpdateEncontrado_Elapsed(object sender, ElapsedEventArgs e)
        {
            //////tmUpdateEncontrado.Stop();
            //////tmUpdateEncontrado.Enabled = false;

            //////if (!bBuscandoUpdate)
            //////{
            //////    if (bSeEncontroUpdate)
            //////    {
            //////        bSeEncontroUpdate = false; 
            //////        if (General.msjConfirmar("Se encontro una actualización para el Módulo de Facturación. \n\n" +
            //////            " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
            //////        {
            //////            buscarUpdate.DescargarActualizacion();
            //////        }
            //////    }
            //////}
            //////else
            //////{
            //////    tmUpdateEncontrado.Enabled = true;
            //////    tmUpdateEncontrado.Start();
            //////}
        }

        private void ChecarVersion()
        {
            //////bBuscandoUpdate = true;
            //////btnBuscarActualizaciones.Enabled = !bBuscandoUpdate;
            //////////bool bConfirmarDescarga = DtGeneral.ConfirmarBusquedaDeActualizaciones();

            //////////if (bConfirmarDescarga)
            //////{
            //////    bSeEncontroUpdate = buscarUpdate.CheckVersionModulo(); 
            //////    ////{
            //////    ////    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Facturación. \n\n" +
            //////    ////        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
            //////    ////    {
            //////    ////        buscarUpdate.DescargarActualizacion();
            //////    ////    }
            //////    ////}
            //////}

            ////////tmUpdaterModulo.Enabled = true;
            ////////tmUpdaterModulo.Interval = (1000 * 30) * 10;
            ////////tmUpdaterModulo.Start();
            //////HabilitarActualizaciones(); 
            //////bBuscandoUpdate = false;
            //////btnBuscarActualizaciones.Enabled = !bBuscandoUpdate;
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

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
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
}
