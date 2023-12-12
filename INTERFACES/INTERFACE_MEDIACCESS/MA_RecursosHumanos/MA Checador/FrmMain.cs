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
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllRecursosHumanos;
using DllRecursosHumanos.HuellasDigitales;
using DllRecursosHumanos.Usuarios_y_Permisos;

using DllTransferenciaSoft;
using DllTransferenciaSoft.Zip;
using DllTransferenciaSoft.IntegrarInformacion;

namespace MA_Checador
{
	public partial class FrmMain: Form
	{
        DllRecursosHumanos.Usuarios_y_Permisos.clsLogin Login;

        bool bUsuarioLogeado = false;
        basGenerales Fg = new basGenerales();
        DataSet dtsCnn = new DataSet();

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error; 
        int iDiasDeRevision = 0;
        DataSet dtsRegistrosDeChecador = new DataSet(); 

        Thread thDescargarHuellas = null;
        Thread thValidarConexiones = null;
        Thread thSincronizacionAutomatica = null;
        bool bSincronizacionAutomatica = false; 


        //////// Arrancar el checador
        ////FrmChecador Checador;
        ////wsCnnHuellas.wsHuellas huellas;
        ////clsHuellas registrarHuellas;
        ////string sDirectorio = Application.StartupPath + @"\DescargarDeHuellas\";


		public FrmMain()
		{
            CheckForIllegalCrossThreadCalls = false; 

			InitializeComponent();

            double iAncho = (int)Screen.PrimaryScreen.WorkingArea.Width;
            double iPanel_1 = iAncho * 0.9;
            double iPanel_2 = iAncho * 0.1; 

            clsAbrirForma.AssemblyActual("MA Checador");
            General.CargarImagenMDI(this, Color.White, DtGeneral.RutaLogo); 
            General.IconoSistema = this.Icon; 

            ////splitContainer_Gral.Panel1MinSize = (int)iPanel_1;
            ////splitContainer_Gral.Panel2MinSize = (int)iPanel_2; 

            splitContainer_Gral.Dock = DockStyle.Top;
            splitContainer_Gral.BorderStyle = BorderStyle.FixedSingle;
            splitContainer_Gral.Height = 93;

            lblProcesoIntegracion.Text = "";
		}

        private void FrmMain_Load(object sender, EventArgs e)
        {

            DtGeneral.ConexionOficinaCentral = true;
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "MA Recursos Humanos";

            ////btnEntrada.Enabled = false;
            ////btnSalida.Enabled = false;
            this.WindowState = FormWindowState.Maximized;

            System.Threading.Thread.Sleep(50);

            btnDescargarHuellas.Enabled = false;
            btnEnviarRegistroChecador.Enabled = false; 

            tmAutenticacion.Enabled = true;
            tmAutenticacion.Interval = 250;
            tmAutenticacion.Start(); 

            ////if (!bUsuarioLogeado)
            ////{
            ////    pfLoginServidor();
            ////}

            ////General.ServidorEnRedLocal = true;
        }

        private bool pfLoginServidor()
        {
            bool bRegresa = false;
            Login = new DllRecursosHumanos.Usuarios_y_Permisos.clsLogin();
            Login.Arbol = "RHM";


            BarraDeStatus.Panels[lblModulo.Name].Text = "Modulo : " + Application.ProductName + " " + DtGeneral.DatosApp.Version;
            if (Login.AutenticarServicio())
            {
                DtGeneral.NombrePersonal = "ADMINISTRADOR";
                BarraDeStatus.Panels[lblServidor.Name].Text = "Servidor : " + General.DatosConexion.Servidor;
                BarraDeStatus.Panels[lblBaseDeDatos.Name].Text = "Base de Datos : " + General.DatosConexion.BaseDeDatos;
                BarraDeStatus.Panels[lblUsuarioConectado.Name].Text = "Usuario : " + DtGeneral.NombrePersonal;

                // Ajustar el Tiempo de Espera para Conexion 
                General.DatosConexion.ConectionTimeOut = SC_SolutionsSystem.Data.TiempoDeEspera.Limite30;

                ////btnEntrada.Enabled = true; 
                ////btnSalida.Enabled = true;
                bUsuarioLogeado = true;


                Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
                BorrarDirectorios();


                //////registrarHuellas = new clsHuellas(); 
                //////huellas = new wsCnnHuellas.wsHuellas();
                ConfigurarConexion_Huellas();

                //////tmSincronizacion.Interval = (( 1000 * 60) * 60) * 3;
                //////tmSincronizacion.Interval = ((1000 * 60) * 20);
                //////tmSincronizacion.Enabled = true;
                //////tmSincronizacion.Start(); 

                //Checador = new FrmChecador();
                ////Checador.MdiParent = this;
                //Fg.CentrarForma(Checador);
                //Checador.Show();

            }
            else
            {
                Application.Exit(); // this.Close();
            }

            return bRegresa;
        }

        private void ConfigurarConexion_Huellas()
        {
            //////thValidarConexiones = new Thread(thread_ConfigurarConexion_Huellas);
            //////thValidarConexiones.Name = "Validando conexiones";
            //////thValidarConexiones.Start(); 
        }

        private void thread_ConfigurarConexion_Huellas()
        {
            //////////huellas.Url = "http://intermed.homeip.net/wsHuellas/wsHuellas.asmx";
            //////btnDescargarHuellas.Enabled = false;
            //////btnEnviarRegistroChecador.Enabled = false;

            //////string sSql = "select Url, DiasRevision From CFG_SRV_Huellas (NoLock) ";
            //////cnn = new clsConexionSQL(General.DatosConexion); 
            //////leer = new clsLeer(ref cnn);

            //////if (!leer.Exec(sSql))
            //////{
            //////    Error.GrabarError(leer, "ConfigurarConexion_Huellas()");
            //////}
            //////else
            //////{
            //////    if (!leer.Leer())
            //////    {
            //////        Error.GrabarError("No se encontro configuración de huellas.", "ConfigurarConexion_Huellas()");
            //////    }
            //////    else 
            //////    {
            //////        try
            //////        {
            //////            huellas.Url = leer.Campo("Url");
            //////            iDiasDeRevision = leer.CampoInt("DiasRevision");

            //////            if (!huellas.TestConexion())
            //////            {
            //////                General.msjError("No fue posible establecer conexión con el servidor de huellas."); 
            //////            }
            //////            else 
            //////            {
            //////                btnDescargarHuellas.Enabled = true;
            //////                btnEnviarRegistroChecador.Enabled = true;
            //////            }
            //////        }
            //////        catch (Exception ex)
            //////        {
            //////            General.msjError(ex.Message); 
            //////        }
            //////    }
            //////}

            //////try
            //////{
            //////    thValidarConexiones = null; 
            //////}
            //////catch (Exception ex)
            //////{
            //////    sSql = ex.Message;
            //////}
        }

        #region Botones_Checador
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if (bUsuarioLogeado)
            {
                //////Checador = new FrmChecador();
                ////////Checador.MdiParent = this;
                //////Fg.CentrarForma(Checador);
                //////Checador.Parent = this; 
                //////Checador.LevantaPantalla(1);

                if (DtChecador.Entrada == 0 && DtChecador.Salida == 0)
                {
                    General.CargarPantalla("FrmChecadorEntrada", "MA Checador", this);
                }
            }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (bUsuarioLogeado)
            {
                ////Checador = new FrmChecador();
                //////Checador.MdiParent = this;
                ////Fg.CentrarForma(Checador);
                ////Checador.Parent = this; 
                ////Checador.LevantaPantalla(2);

                if (DtChecador.Entrada == 0 && DtChecador.Salida == 0)
                {
                    General.CargarPantalla("FrmChecadorSalida", "MA Checador", this);
                }
            }
        }
        #endregion Botones_Checador

        private void tmAutenticacion_Tick(object sender, EventArgs e)
        {
            tmAutenticacion.Stop();
            tmAutenticacion.Enabled = false; 

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
                        // CargarParametros();
                        break;

                    case Keys.F12:
                        DtGeneral.InformacionConexion();
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnDescargarHuellas_Click(object sender, EventArgs e)
        {
            //////btnDescargarHuellas.Enabled = false;
            //////btnEnviarRegistroChecador.Enabled = false;             
            //////btnEntrada.Enabled = false;
            //////btnSalida.Enabled = false;

            //////////General.msjAviso("Iniciando subproceso");
            //////////DescargarHuellas();

            //////thDescargarHuellas = new Thread(DescargarHuellas);
            //////thDescargarHuellas.Start(); 
        }

        private void BorrarDirectorios()
        {
            //////try
            //////{
            //////    if (Directory.Exists(sDirectorio))
            //////    {
            //////        foreach (string sFile in Directory.GetFiles("*.sql"))
            //////        {
            //////            File.Delete(sFile); 
            //////        }
            //////    }

            //////    try 
            //////    {
            //////        Directory.CreateDirectory(sDirectorio);
            //////    }
            //////    catch { } 
            //////}
            //////catch { } 
        }

        private void DescargarHuellas()
        {
            //////////General.msjAviso("Iniciando descarga de huellas");

            //////DataSet dtsHuellas = new DataSet();
            //////string sRegresa = "";
            //////bool bRegresa = false; 
            //////byte[] btRegresa = null;
            //////string sDirectorio = Application.StartupPath + @"\DescargarDeHuellas\";
            //////ZipUtil zip = new ZipUtil();
            //////string sFile_Huellas = Path.Combine(sDirectorio, "Huellas"); 
            
            //////clsCliente cliente; // = new clsCliente(General.DatosConexion);
            //////// cliente.EsIntegracionManual = true; 
            
            //////try
            //////{

            //////    cliente = new clsCliente(General.DatosConexion);
            //////    cliente.EsIntegracionManual = true;
            //////    lblProcesoIntegracion.Text = "Descargando actualización de huellas  ";
            //////    lblProcesoIntegracion.Visible = true;
            //////    Application.DoEvents();
            //////    System.Threading.Thread.Sleep(100);
            //////    Application.DoEvents();

            //////    try
            //////    {
            //////        btRegresa = huellas.DescargarInformacion();
            //////        bRegresa = true;
            //////    }
            //////    catch(Exception ex)
            //////    {
            //////        bRegresa = false;
            //////        sRegresa = "00 " + ex.Message; 
            //////    }

            //////    try 
            //////    {
            //////        BorrarDirectorios();
            //////        lblProcesoIntegracion.Visible = true;
            //////        lblProcesoIntegracion.Text = "Integrando actualización de huellas   ";
            //////        Application.DoEvents();

            //////        if (bRegresa)
            //////        {
            //////            Fg.ConvertirBytesEnArchivo("Huellas.SII", sDirectorio, btRegresa, true);
            //////            zip.Descomprimir(sDirectorio, sFile_Huellas + ".SII");

            //////            cliente.RutaIntegracionManual = sDirectorio;
            //////            cliente.ArchivoIntegracionManual = "Huellas";
            //////            bRegresa = cliente.Integrar();
            //////        }

            //////        lblProcesoIntegracion.Text = "";
            //////        lblProcesoIntegracion.Visible = false;
            //////    }
            //////    catch(Exception ex)
            //////    {
            //////        sRegresa = "01 " + ex.Message; 
            //////    }

            //////    if (!bSincronizacionAutomatica)
            //////    {
            //////        if (bRegresa)
            //////        {
            //////            General.msjUser("Información de huellas actualizada satisfactoriamente.");
            //////        }
            //////        else
            //////        {
            //////            General.msjUser("No fue posible actualizar la información de huellas.");
            //////        }
            //////    }
            //////}
            //////catch (Exception ex) 
            //////{
            //////    sRegresa = "02 " + ex.Message; 
            //////}

            //////if (!bSincronizacionAutomatica)
            //////{
            //////    if (!bRegresa)
            //////    {
            //////        General.msjError(sRegresa);
            //////    }

            //////    btnDescargarHuellas.Enabled = true;
            //////    btnEnviarRegistroChecador.Enabled = true;
            //////    btnEntrada.Enabled = true;
            //////    btnSalida.Enabled = true;
            //////}
        }

        private void btnEnviarRegistroChecador_Click(object sender, EventArgs e)
        {
            //////if (ObtenerInformacionDeChecadas())
            //////{
            //////    btnDescargarHuellas.Enabled = false;
            //////    btnEnviarRegistroChecador.Enabled = false;
            //////    btnEntrada.Enabled = false;
            //////    btnSalida.Enabled = false;

            //////    thDescargarHuellas = new Thread(EnviarRegistroDeChecador);
            //////    thDescargarHuellas.Start();
            //////}
        }

        private bool ObtenerInformacionDeChecadas()
        {
            bool bRegresa = false;
            //////string sSql = string.Format("Exec spp_CFG_ObtenerDatos__Checador @Tabla = 'Checador_Personal',  @DiasDeRevision = {0} ", iDiasDeRevision);

            //////dtsRegistrosDeChecador = new DataSet(); 
            //////if (!leer.Exec(sSql))
            //////{
            //////    Error.GrabarError(leer, "ObtenerInformacionDeChecadas()");
            //////    General.msjError("Ocurrió un error al obtener la información del checador.");
            //////}
            //////else
            //////{
            //////    if (leer.Leer())
            //////    {
            //////        dtsRegistrosDeChecador = leer.DataSetClase;
            //////        bRegresa = true; 
            //////    }
            //////}

            return bRegresa;
        }

        private void EnviarRegistroDeChecador()
        {
            //////string sRegresa = "";
            //////bool bRegresa = false;
            //////string sSql = "";
            //////int iRegistros = 0; 
            //////clsLeer leerInformacion = new clsLeer(ref cnn); 

            //////try
            //////{
            //////    leerInformacion.DataSetClase = dtsRegistrosDeChecador; 

            //////    lblProcesoIntegracion.Text = "Actualizando registros de checador";
            //////    lblProcesoIntegracion.Visible = true;
            //////    Application.DoEvents();

            //////    System.Threading.Thread.Sleep(100);
            //////    Application.DoEvents();                             
            //////    huellas.Timeout = (60 * 1000) * 5;


            //////    while (leer.Leer())
            //////    {
            //////        sSql += leer.Campo(1) + "\n " ;
            //////        iRegistros++;

            //////        if (iRegistros == 50)
            //////        {
            //////            bRegresa = huellas.IntegrarInformacion_RegistroChecadorDetalle(sSql);
            //////            sSql = "";
            //////            iRegistros = 0; 
            //////            if (!bRegresa)
            //////            {
            //////                break;
            //////            }
            //////        }
            //////    }

            //////    if (iRegistros > 0 && sSql != "")
            //////    {
            //////        bRegresa = huellas.IntegrarInformacion_RegistroChecadorDetalle(sSql);
            //////        sSql = "";
            //////    }



            //////    lblProcesoIntegracion.Text = "";
            //////    lblProcesoIntegracion.Visible = false;

            //////    if (!bSincronizacionAutomatica)
            //////    {
            //////        if (bRegresa)
            //////        {
            //////            General.msjUser("Información actualizada satisfactoriamente.");
            //////        }
            //////        else
            //////        {
            //////            General.msjUser("No fue posible actualizar la información.");
            //////        }
            //////    }
            //////}
            //////catch (Exception ex)
            //////{
            //////    sRegresa = ex.Message;
            //////}

            //////if (!bSincronizacionAutomatica)
            //////{
            //////    btnDescargarHuellas.Enabled = true;
            //////    btnEnviarRegistroChecador.Enabled = true;
            //////    btnEntrada.Enabled = true;
            //////    btnSalida.Enabled = true;
            //////}
        }

        private void tmSincronizacion_Tick(object sender, EventArgs e)
        {
            //////tmSincronizacion.Enabled = false;
            //////tmSincronizacion.Stop(); 

            //////if (ObtenerInformacionDeChecadas())
            //////{
            //////    btnDescargarHuellas.Enabled = false; 
            //////    btnEnviarRegistroChecador.Enabled = false;
            //////    btnEntrada.Enabled = false;
            //////    btnSalida.Enabled = false;

            //////    bSincronizacionAutomatica = true;

            //////    thSincronizacionAutomatica = new Thread(Sincronizacion_Automatica);
            //////    thSincronizacionAutomatica.Start();
            //////}
            //////else
            //////{
            //////    tmSincronizacion.Interval = ((1000 * 60) * 60) * 3;
            //////    tmSincronizacion.Enabled = true;
            //////    tmSincronizacion.Start(); 
            //////}

            ////tmSincronizacion.Enabled = true; 
        }

        private void Sincronizacion_Automatica() 
        {

            //////try
            //////{
            //////    EnviarRegistroDeChecador();
            //////}
            //////catch { }


            //////try
            //////{
            //////    DescargarHuellas();
            //////}
            //////catch { }


            ///////// Habilitar los botones 
            //////btnDescargarHuellas.Enabled = true;
            //////btnEnviarRegistroChecador.Enabled = true;
            //////btnEntrada.Enabled = true;
            //////btnSalida.Enabled = true;

            //////bSincronizacionAutomatica = false;

            //////tmSincronizacion.Interval = ((1000 * 60) * 60) * 3;
            //////tmSincronizacion.Enabled = true;
            //////tmSincronizacion.Start(); 

        }
	}
}
