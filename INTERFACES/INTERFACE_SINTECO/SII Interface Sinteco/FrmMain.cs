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
using DllFarmaciaSoft.Pedidos; 
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace SII_Interface_Sinteco
{
    public partial class FrmMain : Form
    {
        DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        //bool bConexionWeb = false;
        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();
        FrmNavegador Navegador;
        ////FrmMonitor_RFID Monitor;
        // string sVersion = Transferencia.Version;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leerVersion;
        //CheckVersion buscarUpdate; // = new CheckVersion("Farmacia.SII", "0.0.0.0"); 
        System.Timers.Timer tmUpdaterModulo; 
        Thread hilo;

        string sNombreVersionSII = "Farmacia.SII";
        string sVersionSII = "0.0.0.0";

        string sNombreVersionSII_Ext = "FarmaciaExt.SII";
        string sVersionSII_Ext = "0.0.0.0";
        bool bBuscandoUpdate = false;
        Random x_tiempo_actualizacion; // = new Random(30);

        public FrmMain()
        {
            InitializeComponent();
            
            btnInformación.Visible = false; 
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Farmacia 
            DtGeneral.ModuloEnEjecucion = TipoModulo.AlmacenUnidosis; 

            CheckForIllegalCrossThreadCalls = false;

            clsAbrirForma.AssemblyActual("SII Interface Sinteco");
            // GnFarmacia.DatosApp = clsAbrirForma.DatosApp;
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo);

            General.IconoSistema = this.Icon;

            DtGeneral.ConexionOficinaCentral = false; 
            General.MultiplesEntidades = true; 
            General.bModoDesarrollo = false; 
            General.SolicitarConfiguracionWeb = true; 
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "AlmacenPtoVta"; 

            if (!bUsuarioLogeado)
            {
                if (!validarModuloDeFarmacia())
                {
                    Application.Exit(); 
                }
                else 
                {
                    pfLoginServidor();
                } 
            }

            General.ServidorEnRedLocal = true;


        }

        private bool validarModuloDeFarmacia()
        {
            bool bRegresa = true;
            string sFile = Application.StartupPath + @"\Farmacia.exe";

            ////if (!File.Exists(sFile))
            ////{
            ////    bRegresa = false;
            ////    General.msjAviso("No se encontro el módulo Farmacia.exe no es posible abrir el módulo de Almacén, favor de reportarlo a sistemas."); 
            ////}

            return bRegresa; 
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F10:
                        if (DtGeneral.EsEquipoDeDesarrollo)
                        {
                            ////Gn_Monitor_RFID.Leyendo_Informacion_RFID = !Gn_Monitor_RFID.Leyendo_Informacion_RFID;
                        }
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

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            DtGeneral.SoloMostrarUnidadesConfiguradas = true; 
            Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("SELECT * FROM ctl_Sucursales (nolock)", "Sucursales");
            Login.Arbol = "ISTC";

            ////// GnFarmacia.CargarModulo("DllFarmaciaSoft.QRCode"); 
            ////// GnFarmacia.CargarModulo("SII INT Nadro");


            BarraDeStatus.Panels[lblModulo.Name].Text = "Módulo : " + Application.ProductName + " " + Gn_ISINTECO.DatosApp.Version; 
            if (Login.AutenticarUsuario()) 
            {
                //BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + Application.ProductVersion;
                BarraDeStatus.Panels[lblFarmacia.Name].Text = "Estado : " + DtGeneral.EstadoConectado + " -- " + DtGeneral.EstadoConectadoNombre + "      " + " Almacén : " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal; 


                ////////MessageBox.Show(Login.Sucursal.ToString());
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

                ///// this.Text = " Módulo " + Navegador.NombreModulo;
                this.Text = " Módulo : " + Navegador.NombreModulo + "         " + DtGeneral.EmpresaConectadaNombre; 


                //// Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30; 

                ////// 
                cnn = new clsConexionSQL(General.DatosConexion); 
                leerVersion = new clsLeer(ref cnn);


                ////// Cargar los Parametros del sistema 
                GnFarmacia.Parametros = new clsParametrosPtoVta(General.DatosConexion, GnFarmacia.DatosApp, 
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "ALMN");
                GnFarmacia.Parametros.CargarParametros(false);
                GnFarmacia.CargarDatosPublicoGeneral(); 

                BarraDeStatus.Panels[lblFechaSistema.Name].Text = "Fecha de Sistema : " + General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"); 
                tmDatosPersonalConectado.Enabled = true;
                tmDatosPersonalConectado.Start();

                //// Pasar la ruta de reportes al General 
                DtGeneral.RutaReportes = GnFarmacia.RutaReportes;  
                DtGeneral.DiasAdicionalesCierreTickets = GnFarmacia.DiasAdicionalesCierreTickets;

                //// Revisar si la unidad cuenta con servidor dedicado 
                DtGeneral.UnidadConServidorDedicado = GnFarmacia.UnidadConServidorDedicado; 

                //////// Revisar que la Unidad maneje Robot Mach4 
                //////GnFarmacia.VerificarInterface(); 


                // Revisar si el Equipo es el Servidor de Datos de la Red 
                GnFarmacia.EsServidorLocal();
                DtGeneral.DatosDeServicioWebUpdater.Servidor = DtGeneral.DatosDeServicioWeb.Servidor;
                DtGeneral.DatosDeServicioWebUpdater.WebService = DtGeneral.DatosDeServicioWeb.WebService;
                DtGeneral.DatosDeServicioWebUpdater.PaginaASMX = "wsUpdater";


                btnNavegador_Click(null, null); 

                //////// Determinar si se activa el Servicio Cliente considerando diversos parametros 
                ////if (GnFarmacia.EjecutarServicioCliente)
                ////{
                ////    tmServicioInformacion.Enabled = true;
                ////    tmServicioInformacion.Start();
                ////}

                ////if (GnFarmacia.ManejaUbicaciones)
                ////{
                ////    // BarraDeStatus.Panels[lblMach4.Name].Width = 10;
                ////    BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + GnFarmacia.DatosApp.Version + "_u "; ; 
                ////}

                ////// Checar la version instalada 
                ////string[] sModulos = { "Almacen", "Servicio Cliente", "Configuración Servicio Cliente" }; 
                ////DtGeneral.RevisarVersion(sModulos);
                ////RevisarVersionModulos();

                //wsFarmacia.wsCnnCliente x = new Farmacia.wsFarmacia.wsCnnCliente();
                //General.Url = x.Url; 

                tmSesionDeUsuario.Interval = (1000 * 60) * 2;
                tmSesionDeUsuario.Enabled = false;
                tmSesionDeUsuario.Stop(); 


                // || DtGeneral.EsAdministrador 
                //DtGeneral.EsAdministrador = false;
                //GnFarmacia.EsServidorDeRedLocal = false; 

                ////////// Mostrar la Solicitud de Catalogos solo en Servidores y a Administradores 
                ////if (GnFarmacia.EsServidorDeRedLocal || DtGeneral.EsAdministrador)
                ////{
                ////    btnGetInformacion.Visible = true;                     
                ////}

                ////////// Administradores tienen acceso a toda la información. Ventas  
                ////if (DtGeneral.EsAdministrador)
                ////{
                ////    GnFarmacia.MostrarLotesSinExistencia = true;
                ////}


                //////////Revisar si se encontro MAC servidor                   
                if (!DtGeneral.EsEquipoDeDesarrollo)
                {
                    //////if (!GnFarmacia.ExisteMAC_Servidor())
                    //////{
                    //////    General.msjAviso("No se encontro ningún servidor de la unidad.\n" +
                    //////        "Favor de reportarlo a Sistemas.");
                    //////    Application.Exit(); 
                    //////}
                }


                //tmUpdater.Enabled = true;
                //tmUpdater.Interval = (1000 * 10);
                //tmUpdater.Start(); 
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
            ////////// Checar la version instalada 
            ////string[] sModulos = { "", "Almacen", "Farmacia", "Servicio Cliente", "Configuración Servicio Cliente" };
            ////DtGeneral.RevisarVersion(sModulos, MostrarInterface);
        }

        private void btnNavegador_Click(object sender, EventArgs e)
        {
            //////if (!Gn_Monitor_RFID.MonitorCargado)
            //////{
            //////    Monitor = new FrmMonitor_RFID();
            //////    Monitor.MdiParent = this;
            //////    Monitor.Show();
            //////    Monitor.Activate();
            //////}
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

        private void btnBuscarActualizaciones_Click(object sender, EventArgs e)
        {
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////////if (Gn_Monitor_RFID.Leyendo_Informacion_RFID)
            ////////{
            ////////    e.Cancel = true;
            ////////    General.msjUser("En este momento se esta ejectuando la extracción de información RFID, no es posible cerrar la aplicación.");
            ////////}
        }
    }
}
