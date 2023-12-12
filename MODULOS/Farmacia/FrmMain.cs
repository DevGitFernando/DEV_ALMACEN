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
using DllTransferenciaSoft;
using DllTransferenciaSoft.Informacion;

//using Dll_IMach4;
using DllRobotDispensador;
using Farmacia.VentasDispensacion; 

namespace Farmacia
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        clsGenerarMenu menuArbol; 
        // string sVersion = Transferencia.Version;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leerVersion; 
        CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
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
        bool bForzarActualizacion = true; 

        Random x_tiempo_actualizacion; // = new Random(30);

        public FrmMain()
        {
            InitializeComponent();

            btnGetInformacion.Visible = false;
            btnExportarInformacion.Visible = false;
            btnInformacion.Visible = false; 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Farmacia 
            DtGeneral.ModuloEnEjecucion = TipoModulo.Farmacia; 

            CheckForIllegalCrossThreadCalls = false; 

            clsAbrirForma.AssemblyActual("Farmacia");
            datosDeModulo = clsAbrirForma.DatosApp;
            // GnFarmacia.DatosApp = clsAbrirForma.DatosApp;
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            mnPrincipal.Visible = false;
            if(!DtGeneral.MenuEnFormaDeArbol)
            {
                mnPrincipal.Visible = true;
                btnNavegador.Visible = false;
                btnNavegador.Enabled = false; 
            }
            this.Refresh(); 


            DtGeneral.ConexionOficinaCentral = false;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "FarmaciaPtoVta";

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
                    case Keys.D0:
                        GnFarmacia.GenerarExcepcionesDeVales(); 
                        break;

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
            DtGeneral.SoloMostrarUnidadesConfiguradas = true; 
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "PFAR";

            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + datosDeModulo.Version; //// GnFarmacia.DatosApp.Version;
            if (Login.AutenticarUsuario())
            {
                //////BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Farmacia : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal; 


                //////MessageBox.Show(Login.Sucursal.ToString());
                bRegresa = true;
                if(DtGeneral.MenuEnFormaDeArbol)
                {
                    Navegador = new FrmNavegador();
                    Navegador.MdiParent = this;
                    Navegador.Permisos = Login.Permisos;
                    //////Navegador.ListaIconos = imgNavegacion_2;
                    Navegador.ListaIconos = imgNavegacion;
                    Navegador.Posicion = ePosicion.Izquierda;
                    Login = null;
                    Navegador.Show();
                    Navegador.Activate();

                    ////this.Text = " Módulo " + Navegador.NombreModulo;
                    this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre;
                }
                else
                {
                    General.ArbolDeNavegacion = Login.Permisos;

                    mnPrincipal.Visible = true;
                    menuArbol = new clsGenerarMenu(General.ArbolDeNavegacion, mnPrincipal, this);
                    menuArbol.ListaIconos = imgNavegacion;
                    menuArbol.MostrarIconos = true;
                    menuArbol.GenerarMenu();
                    menuArbol.MostrarMenu = true;

                    this.Text = " Módulo : " + mnPrincipal.Items[0].Text + "         " + DtGeneral.EmpresaConectadaNombre;
                }


                //// Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.SinLimite; 

                ////// 
                cnn = new clsConexionSQL(General.DatosConexion); 
                leerVersion = new clsLeer(ref cnn); 


                // Cargar los Parametros del sistema 
                GnFarmacia.Parametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp, 
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.ArbolModulo);
                GnFarmacia.Parametros.CargarParametros();
                GnFarmacia.CargarDatosPublicoGeneral();

                DtGeneral.ConfirmacionConHuellas = GnFarmacia.ConfirmacionConHuellas;
                DtGeneral.DiasARevisarpedidosCedis = GnFarmacia.DiasARevisarpedidosCedis;

                ///////// Quitar 
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";
                ////GnFarmacia.RutaReportes = DtGeneral.RutaReportes; 

                BarraDeStatus.Panels[lblFechaSistema.Name].Text = "Fecha de Sistema : " + General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"); 
                tmDatosPersonalConectado.Enabled = true;
                tmDatosPersonalConectado.Start(); 

                // Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnFarmacia.RutaReportes;
                DtGeneral.DiasAdicionalesCierreTickets = GnFarmacia.DiasAdicionalesCierreTickets; 

                // Revisar si la unidad cuenta con servidor dedicado 
                DtGeneral.UnidadConServidorDedicado = GnFarmacia.UnidadConServidorDedicado; 

                //// Revisar que la Unidad maneje Robot Mach4 
                GnFarmacia.VerificarInterface();                

                // Revisar si el Equipo es el Servidor de Datos de la Red 
                GnFarmacia.EsServidorLocal();
                DtGeneral.DatosDeServicioWebUpdater.Servidor = DtGeneral.DatosDeServicioWeb.Servidor;
                DtGeneral.DatosDeServicioWebUpdater.WebService = DtGeneral.DatosDeServicioWeb.WebService;
                DtGeneral.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater"; 

                if (GnFarmacia.EsServidorDeRedLocal)
                {
                    GetVersionSII();
                    buscarUpdate = new CheckVersion(sNombreVersionSII, sVersionSII, sNombreVersionSII_Ext, sVersionSII_Ext, true); 
                }
                else
                {
                    FileVersionInfo f = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                    buscarUpdate = new CheckVersion(f.OriginalFilename, f.FileVersion, sNombreVersionSII_Ext, sVersionSII_Ext, false); 
                }

                // Determinar si se activa el Servicio Cliente considerando diversos parametros 
                if (GnFarmacia.EjecutarServicioCliente)
                {
                    tmServicioInformacion.Enabled = true;
                    tmServicioInformacion.Start();
                }

                if (RobotDispensador.Robot.EsClienteInterface)
                {
                    BarraDeStatus.Panels[lblMach4.Name].Text = "I - " + RobotDispensador.Robot.PuertoDeSalida; 
                    //BarraDeStatus.Panels[lblMach4.Name].Width = 8; 
                    BarraDeStatus.Panels[lblMach4.Name].AutoSize = StatusBarPanelAutoSize.Contents; 
                    BarraDeStatus.Panels[lblMach4.Name].ToolTipText = BarraDeStatus.Panels[lblMach4.Name].Text; 
                }

                // Revisa si la unida cuenta con la base de datos del padron 
                DtGeneral.BuscarConfiguracionPadron(); 

                //////// Checar la version instalada 
                //////string[] sModulos = { "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" }; 
                //////DtGeneral.RevisarVersion(sModulos); 
                RevisarVersionModulos(); 


                //wsFarmacia.wsCnnCliente x = new Farmacia.wsFarmacia.wsCnnCliente();
                //General.Url = x.Url; 

                tmSesionDeUsuario.Interval = (1000 * 60) * 2;
                tmSesionDeUsuario.Enabled = true;
                tmSesionDeUsuario.Start(); 

                tmUpdaterModulo = new System.Timers.Timer((1000 * 30) * 2);
                tmUpdaterModulo.Elapsed += new ElapsedEventHandler(this.tmUpdaterModulo_Elapsed); 
                tmUpdaterModulo.Enabled = true;
                tmUpdaterModulo.Start();
                x_tiempo_actualizacion = new Random(30);

                tmUpdateEncontrado = new System.Timers.Timer((1000) * 2);
                tmUpdateEncontrado.Elapsed += new ElapsedEventHandler(this.tmUpdateEncontrado_Elapsed);
                tmUpdateEncontrado.Enabled = true;
                ////tmUpdateEncontrado.Start();

                //// Configurar la ejecución de la depuracion de Logs 
                HabilitarLimpiezaLogs();

                ////// Configurar la generacion del historico de existencias 
                //if (GnFarmacia.EsServidorDeRedLocal) 
                //{ 
                //    HabilitarGenerarExistenciaHistorico();
                //}  

                // || DtGeneral.EsAdministrador 
                //DtGeneral.EsAdministrador = false;
                //GnFarmacia.EsServidorDeRedLocal = false; 

                // Mostrar la Solicitud de Catalogos solo en Servidores y a Administradores 
                if (GnFarmacia.EsServidorDeRedLocal || DtGeneral.EsAdministrador || DtGeneral.EsEquipoDeDesarrollo) 
                {
                    btnGetInformacion.Visible = true;
                    btnExportarInformacion.Visible = GnFarmacia.ExportaInformacion; 
                }

                // Administradores tienen acceso a toda la información. Ventas  
                if (DtGeneral.EsAdministrador)
                {
                    GnFarmacia.MostrarLotesSinExistencia = true;
                    //btnInformacion.Visible = true; 
                }

                ///////// QUITAR ESTO 
                /////GnFarmacia.ManejaUbicaciones = true; 

                //tmUpdater.Enabled = true;
                //tmUpdater.Interval = (1000 * 10);
                //tmUpdater.Start(); 


                //////////Revisar si se encontro MAC servidor                   
                if (!DtGeneral.EsEquipoDeDesarrollo)
                {
                    if (!GnFarmacia.ExisteMAC_Servidor())
                    {
                        General.msjAviso("No se encontro ningún servidor de la unidad.\n" +
                            "Favor de reportarlo a Sistemas.");
                        Application.Exit();
                    }
                }

                //// Jesús Diaz 2K150929.1440 
                if (!DtGeneral.ValidarVersion_Modulo_vs_BaseDeDatos(datosDeModulo))
                {
                    Application.Exit(); // this.Close();
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

                ChecarVersion();
                if (bSeEncontroUpdate)
                {
                    tmUpdateEncontrado_Elapsed(null, null);
                }
                tmUpdateEncontrado.Enabled = true;

                DtGeneral.ValidaTransferenciasTransito_DiasConfirmacion();
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
            string[] sModulos = { "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" };
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

        private void tmSesionDeUsuario_Tick(object sender, EventArgs e)
        {
            tmSesionDeUsuario.Stop();         
            tmSesionDeUsuario.Enabled = false;

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                Application.Exit();
            }

            tmSesionDeUsuario.Interval = (1000 * 60) * 4;
            tmSesionDeUsuario.Enabled = true;
            tmSesionDeUsuario.Start();         
        }

        private void tmServicioInformacion_Tick(object sender, EventArgs e)
        {
            tmServicioInformacion.Stop();
            tmServicioInformacion.Enabled = false;


            GnFarmacia.RevisarServicioDeInformacion();
            if (GnFarmacia.EjecutarServicioCliente)
            {
                tmServicioInformacion.Enabled = true;
                tmServicioInformacion.Start();
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
                hilo.Name = "UpdateFarmacia";
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
            bool bActualizar = false; 

            if (!bBuscandoUpdate)
            {
                if (bSeEncontroUpdate)
                {
                    bSeEncontroUpdate = false;
                    if (bForzarActualizacion)
                    {
                        General.msjUser("Se encontro una actualización para el Módulo de Farmacia. \n\n" +
                        " Guarde todos los documentos abiertos, a continuación se instalara la actualización.");
                        bActualizar = true;
                    }
                    else
                    {
                        bActualizar = General.msjConfirmar("Se encontro una actualización para el Módulo de Farmacia. \n\n" +
                        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes;
                    }

                    if (bActualizar)
                    {
                        buscarUpdate.DescargarActualizacion();
                    }

                    if (bForzarActualizacion)
                    {
                        bForzarActualizacion = false;
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
            if ( !DtGeneral.EsEquipoDeDesarrollo ) 
            {
                bSeEncontroUpdate = buscarUpdate.CheckVersionModulo();
                ////{
                ////    if (General.msjConfirmar("Se encontro una actualización para el Módulo de Farmacia. \n\n" +
                ////        " ¿ Desea descargarla en este momento ?") == DialogResult.Yes)
                ////    {
                ////        buscarUpdate.DescargarActualizacion();
                ////    }
                ////}
            }

            HabilitarActualizaciones();
            bBuscandoUpdate = false;
            btnBuscarActualizaciones.Enabled = !bBuscandoUpdate; 
        }

        private void RegistrarRevisionDeVersion()
        {
            string sFile = Application.StartupPath + @"\Log_CheckUpdate.txt";
            string sTime = MarcaTiempo();
            int iTamFile = (1024 * 1024) * 1;
            string sTimer = Fg.PonCeros((tmUpdaterModulo.Interval / 60000).ToString(), 4); 

            try
            {
                StreamWriter f;
                if (File.Exists(sFile))
                {
                    FileInfo fl = new FileInfo(sFile);
                    if (fl.Length >= iTamFile)
                    {
                        try
                        {
                            File.Delete(sFile);
                        }
                        catch { }
                    }

                    f = new StreamWriter(sFile, true);
                }
                else
                {
                    f = new StreamWriter(sFile);
                }

                f.WriteLine(sTime + "_" + sTimer + " ==> Buscando Actualizacion de Modulo : " + GnFarmacia.Modulo + " " + GnFarmacia.Version ); 
                f.Close();
            }
            catch { }
        }

        private string MarcaTiempo()
        {
            string sMarca = "";
            basGenerales Fg = new basGenerales();
            DateTime dt = DateTime.Now;

            sMarca += Fg.PonCeros(dt.Year, 4);
            sMarca += Fg.PonCeros(dt.Month, 2);
            sMarca += Fg.PonCeros(dt.Day, 2);
            sMarca += "_";
            sMarca += Fg.PonCeros(dt.Hour, 2);
            sMarca += Fg.PonCeros(dt.Minute, 2);
            sMarca += Fg.PonCeros(dt.Second, 2);

            return sMarca;
        }

        private int Temporizador()
        {
            int iRegresa = 0; 
            //Random x = new Random(30);

            //iRegresa = x.Next(5, 30);

            return iRegresa;
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

        private void HabilitarLimpiezaLogs()
        {
            int i = x_tiempo_actualizacion.Next(5, 10);

            tmCleanUp.Enabled = true;
            tmCleanUp.Interval = (1000 * 60) * i;
            tmCleanUp.Start();
        }

        private void HabilitarGenerarExistenciaHistorico()
        {
            int i = x_tiempo_actualizacion.Next(15, 30);

            tmCheckExistencia.Enabled = true;
            tmCheckExistencia.Interval = (1000 * 60) * i;
            tmCheckExistencia.Start();
        }

        private bool GetVersionSII()
        {
            bool bRegresa = false;
            sNombreVersionSII = "Farmacia.SII";
            sVersionSII = "0.0.0.0";

            sNombreVersionSII_Ext = "FarmaciaExt.SII";
            sVersionSII_Ext = "0.0.0.0";

            string sSql = string.Format( " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones (NoLock) " + 
                " Where Tipo = 1 " + 
                " Order By IdVersion desc ");

            string sSql2 = string.Format(" Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones (NoLock) " +
                " Where Tipo = 2 " + 
                " Order By IdVersion desc "); 


            if (!leerVersion.Exec(sSql))
            {
            }
            else
            {
                if (leerVersion.Leer())
                {
                    bRegresa = true;
                    // sNombreVersionSII = leerVersion.Campo("NombreVersion");
                    sVersionSII = leerVersion.Campo("Version");

                    if (!leerVersion.Exec(sSql2))
                    {
                    }
                    else
                    {
                        if (leerVersion.Leer())
                        {
                            bRegresa = true;
                            // sNombreVersionSII = leerVersion.Campo("NombreVersion");
                            sVersionSII_Ext = leerVersion.Campo("Version"); 
                        }
                    } 
                }
            }
            return bRegresa;
        }

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
            if (!bBuscandoUpdate)
            {
                tmUpdaterModulo_Elapsed(null, null);
            }
        }

        private void btnGetInformacion_Click(object sender, EventArgs e)
        {
            DllFarmaciaSoft.GetInformacionManual.FrmGruposDeInformacion f = new DllFarmaciaSoft.GetInformacionManual.FrmGruposDeInformacion();

            ////f.MdiParent = this; 
            f.ShowDialog(this);  
        }

        private void btnExportarInformacion_Click(object sender, EventArgs e)
        {
            Transferencia.ExportarInformacionBD(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////FrmColorProductosIMach f = new FrmColorProductosIMach(menuArbol);
            ////f.MdiParent = this; 
            ////f.Show(); 

            Devoluciones_De_Transferencias.FrmDevolucionDeTransferencias_Entrada f = new Devoluciones_De_Transferencias.FrmDevolucionDeTransferencias_Entrada();
            f.MdiParent = this;
            f.Show(); 

        }

        private void btnInformacion_Click(object sender, EventArgs e)
        {
            RevisarVersionInstaladaModulos(true); 
        }

        #region Limpiar tablas de proceso de lotes 
        Thread thClean;

        private void tmCleanUp_Tick(object sender, EventArgs e)
        {
            tmCleanUp.Stop(); 
            tmCleanUp.Enabled = false;

            thClean = new Thread(LimpiarTablasDeControl);
            thClean.Name = "";
            thClean.Start(); 
        }

        private void LimpiarTablasDeControl()
        {
            try
            {
                clsConexionSQL con = new clsConexionSQL(); 
                con.DatosConexion.Servidor = cnn.DatosConexion.Servidor;
                con.DatosConexion.BaseDeDatos = cnn.DatosConexion.BaseDeDatos;
                con.DatosConexion.Usuario = cnn.DatosConexion.Usuario;
                con.DatosConexion.Password = cnn.DatosConexion.Password;
                con.DatosConexion.Puerto = cnn.DatosConexion.Puerto; 

                string sSql = "Exec spp_Mtto_Limpiar_Tablas_ListaLotes ";
                clsLeer leer = new clsLeer(ref con); 

                leer.Exec(sSql); 
            }
            catch
            {
            }
            finally
            {
                Application.DoEvents();
                tmCleanUp.Enabled = true;
                tmCleanUp.Start();
            }
        }
        #endregion Limpiar tablas de proceso de lotes

        #region Generar historico de existencia
        Thread thHistoricoExitencia;

        private void tmCheckExistencia_Tick(object sender, EventArgs e)
        {
            tmCheckExistencia.Stop();
            tmCheckExistencia.Enabled = false;

            thHistoricoExitencia = new Thread(GenerarHistoricoExistencia);
            thHistoricoExitencia.Name = "GenerarHistoricoExistencia";
            thHistoricoExitencia.Start();

            DtGeneral.ValidaTransferenciasTransito_DiasConfirmacion();
        }

        private void GenerarHistoricoExistencia_Arranque()
        {
            thHistoricoExitencia = new Thread(GenerarHistoricoExistencia);
            thHistoricoExitencia.Name = "GenerarHistoricoExistencia";
            thHistoricoExitencia.Start();
        }

        private void GenerarHistoricoExistencia()
        {
            try 
            {
                string sSql = string.Format("Exec spp_PRCS_DeterminarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", 
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                clsLeer leer = new clsLeer(ref cnn);

                leer.Exec(sSql); 
            }
            catch
            {
            }
            finally
            {
                Application.DoEvents();
                tmCheckExistencia.Enabled = true;
                tmCheckExistencia.Start();
            }
        }
        #endregion Generar historico de existencia 
    }
}
