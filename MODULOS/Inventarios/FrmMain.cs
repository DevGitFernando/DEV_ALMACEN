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

// using Farmacia; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Informacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;

// using DllFarmaciaAuditor;

namespace Inventarios
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

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leerVersion;
        clsDatosApp datosDeModulo; 
        // CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        // System.Timers.Timer tmUpdaterModulo;
        // Thread hilo; 

        // string sNombreVersionSII = "Farmacia.SII";
        // string sVersionSII = "0.0.0.0";

        // string sNombreVersionSII_Ext = "FarmaciaExt.SII";
        // string sVersionSII_Ext = "0.0.0.0";

        public FrmMain()
        {
            InitializeComponent(); 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false; 

            clsAbrirForma.AssemblyActual("Inventarios");
            datosDeModulo = clsAbrirForma.DatosApp;
            // GnFarmacia.DatosApp = clsAbrirForma.DatosApp;
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "Inventarios";

            // General.BackColorBarraMenu = Color.LightGray;
            // MessageBox.BackColor = Global.FormaBackColor;


            ////////BarraDeStatus.Panels[lblMach4.Name].Text = "I";
            ////////BarraDeStatus.Panels[lblMach4.Name].MinWidth = 0; 
            ////////BarraDeStatus.Panels[lblMach4.Name].Width = 0; 
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

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            string sArbol = "PFAR"; 
            DtGeneral.SoloMostrarUnidadesConfiguradas = true;
            DtGeneral.ModuloEnEjecucion = TipoModulo.Auditor; 

            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "INV";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + datosDeModulo.Version;
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


                ////BarraDeStatus.Panels[lblFechaSistema.Name].Text = "Fecha de Sistema : " + General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"); 
                tmDatosPersonalConectado.Enabled = true;
                tmDatosPersonalConectado.Start();

                // Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnInventarios.RutaReportes;


                ////////// Revisar si el Equipo es el Servidor de Datos de la Red 
                //////GnFarmacia.EsServidorLocal(); 
                //////if (GnFarmacia.EsServidorDeRedLocal)
                //////{
                //////    GetVersionSII();
                //////    buscarUpdate = new CheckVersion(sNombreVersionSII, sVersionSII, sNombreVersionSII_Ext, sVersionSII_Ext, true); 
                //////}
                //////else
                //////{
                //////    FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                //////    buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, sNombreVersionSII_Ext, sVersionSII_Ext, false); 
                //////}

                //////// Determinar si se activa el Servicio Cliente considerando diversos parametros 
                //////if (GnFarmacia.EjecutarServicioCliente)
                //////{
                //////    tmServicioInformacion.Enabled = true;
                //////    tmServicioInformacion.Start();
                //////}

                ////if ( IMach4.EsClienteIMach4 )
                ////    BarraDeStatus.Panels[lblMach4.Name].Width = 8;

                ////// Checar la version instalada 
                ////// string[] sModulos = { "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" }; 
                ////// DtGeneral.RevisarVersion(sModulos); 
                RevisarVersionModulos(); 

                //wsFarmacia.wsCnnCliente x = new Farmacia.wsFarmacia.wsCnnCliente();
                //General.Url = x.Url; 

                //////tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                //////tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed); 
                //////tmUpdaterModulo.Enabled = true;
                //////tmUpdaterModulo.Start(); 

                //tmUpdater.Enabled = true;
                //tmUpdater.Interval = (1000 * 10);
                //tmUpdater.Start(); 

                //// Jesús Diaz 2K150929.1440 
                if (!DtGeneral.ValidarVersion_Modulo_vs_BaseDeDatos(datosDeModulo))
                {
                    Application.Exit(); // this.Close();
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
            // Checar la version instalada 
            string[] sModulos = { "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" }; 
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
                Navegador.Activate();
            }
        }

        private void tmDatosPersonalConectado_Tick(object sender, EventArgs e)
        {
            BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;
            //BarraDeStatus.Panels[lblFechaSistema.Name].Text = "Fecha de Sistema : " + General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"); 
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

        private void tmServicioInformacion_Tick(object sender, EventArgs e)
        {
        }

        private void tmUpdaterModulo_Elapsed(object sender, ElapsedEventArgs e)
        {
            ////tmUpdaterModulo.Stop();
            ////tmUpdaterModulo.Enabled = false;

            ////hilo = new Thread(this.ChecarVersion);
            ////hilo.Name = "UpdateAuditorFarmacia";
            ////hilo.Start();
        }

        private void ChecarVersion()
        {
            ////if (!DtGeneral.EsEquipoDeDesarrollo)
            ////{
            ////    if (buscarUpdate.CheckVersionModulo())
            ////    {
            ////        if (General.msjConfirmar("Se encontro una actualización para el Módulo de Farmacia. \n\n" +
            ////            " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
            ////        {
            ////            buscarUpdate.DescargarActualizacion();
            ////        }
            ////    }
            ////}

            HabilitarActualizaciones(); 
        }

        private void HabilitarActualizaciones()
        {
            ////tmUpdaterModulo.Enabled = true;
            ////tmUpdaterModulo.Interval = (1000 * 60) * 5;
            ////tmUpdaterModulo.Start(); 
        }

        private bool GetVersionSII()
        {
            bool bRegresa = false;
            ////sNombreVersionSII = "Farmacia.SII";
            ////sVersionSII = "0.0.0.0";

            ////sNombreVersionSII_Ext = "FarmaciaExt.SII";
            ////sVersionSII_Ext = "0.0.0.0";

            ////string sSql = string.Format( " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
            ////    " From Net_Versiones (NoLock) " + 
            ////    " Where Tipo = 1 " + 
            ////    " Order By IdVersion desc ");

            ////string sSql2 = string.Format(" Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
            ////    " From Net_Versiones (NoLock) " +
            ////    " Where Tipo = 2 " + 
            ////    " Order By IdVersion desc "); 


            ////if (!leerVersion.Exec(sSql))
            ////{
            ////}
            ////else
            ////{
            ////    if (leerVersion.Leer())
            ////    {
            ////        bRegresa = true;
            ////        // sNombreVersionSII = leerVersion.Campo("NombreVersion");
            ////        sVersionSII = leerVersion.Campo("Version");

            ////        if (!leerVersion.Exec(sSql2))
            ////        {
            ////        }
            ////        else
            ////        {
            ////            if (leerVersion.Leer())
            ////            {
            ////                bRegresa = true;
            ////                // sNombreVersionSII = leerVersion.Campo("NombreVersion");
            ////                sVersionSII_Ext = leerVersion.Campo("Version"); 
            ////            }
            ////        } 
            ////    }
            ////}
            return bRegresa;
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            tmUpdaterModulo_Elapsed(null, null); 
        }
    }
}
