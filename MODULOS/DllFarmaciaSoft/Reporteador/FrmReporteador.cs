using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllFarmaciaSoft.Reporteador
{
    internal partial class FrmReporteador : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWeb = null; 

        Thread thrReporte; 

        // bool bConexionWeb = false;
        // bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;

        clsImprimir myRpt;
        clsDatosCliente datosCliente;
        clsConexionClienteUnidad datosConexionUnidad;
        string sUrl = ""; 
        bool bImpresionWeb = false;
        
        bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false; 
        bool bReporteRemoto = false;
        bool bMostrarInterface = true; 

        #endregion

        public FrmReporteador()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        public FrmReporteador(clsImprimir Reporte, clsDatosCliente DatosTerminal)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            myRpt = Reporte;
            datosCliente = DatosTerminal; 
        }

        public FrmReporteador(clsImprimir Reporte, clsDatosCliente DatosTerminal, clsConexionClienteUnidad ConexionUnidad)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            bReporteRemoto = true;
            myRpt = Reporte;
            datosCliente = DatosTerminal;
            datosConexionUnidad = ConexionUnidad; 
        }

        public bool MostrarInterface
        {
            get { return bMostrarInterface; }
            set { bMostrarInterface = value; }
        }

        #region Funciones y Procedimientos Privados 
        private void FrmReporteador_Load(object sender, EventArgs e)
        {
            lblTituloReporte.Visible = false; 
            this.Height = 130;
            this.Width = 460;

            FrameProceso.Left = 8;
            FrameProceso.Top = 25;
            FrameProceso.Height = 64;
            FrameProceso.Width = 428;
            this.Height = 130;

            if (DtGeneral.EsEquipoDeDesarrollo)
            {
                //toolStripProceso.Visible = true;
                //this.Height += toolStripProceso.Height;
                //FrameProceso.Top += toolStripProceso.Height;
               
                //FrameProceso.Top = 20;
                //this.Height = 105;
            }

            if(myRpt.TituloReporte != "" && myRpt.TituloReporte.ToUpper() != "Reporte".ToUpper())
            {
                //FrameProceso.Height = 86;
                lblTituloReporte.Visible = true;
                lblTituloReporte.Text = myRpt.TituloReporte;
                //AjustarAncho();
                //this.Height += 25;
                this.Height += 5;
            }
            else
            {
                this.FrameProceso.Height -= lblTituloReporte.Height;
                this.Height -= lblTituloReporte.Height;
                this.Height += 5;
            }

            tmIniciarProceso.Interval = 1500;
            tmIniciarProceso.Enabled = true;
            tmIniciarProceso.Start(); 
        }

        private void AjustarAncho()
        {
            int width = lblTituloReporte.Width; 
            int widthBase = lblTituloReporte.Width;
            int newWidth = 0;
            int diferencia = 0; 
            bool bAjustar = false;

            try
            {
                Graphics g = base.CreateGraphics();
                Font font = base.Font;

                newWidth = (int)g.MeasureString(lblTituloReporte.Text, font).Width;
                if (width < newWidth)
                {
                    width = newWidth;
                    diferencia = newWidth - widthBase; 
                }

                bAjustar = diferencia > 0 ? true : false;
                if (bAjustar)
                {
                    this.Width += diferencia; 
                }
            }
            catch { }
        }

        private void tmIniciarProceso_Tick(object sender, EventArgs e)
        {
            tmIniciarProceso.Stop();
            tmIniciarProceso.Enabled = false;

            thrReporte = new Thread(this.GenerarReporte_Thread);
            thrReporte.Name = "GenerandoReporte";
            thrReporte.Start();
        }

        private void tmRevisarGeneracion_Tick(object sender, EventArgs e)
        {
            //tmRevisarGeneracion
            try
            {
                bCanceladoPorError = true; 
                thrReporte.Abort();
            }
            catch
            {
            }
            finally 
            {
                bReporteGenerado = false;
                this.Hide(); 
            }
        } 
        #endregion Funciones y Procedimientos Privados 

        public bool GenerarReporte(string Url, bool ImpresionWeb)
        {
            sUrl = Url;
            bImpresionWeb = ImpresionWeb;

            if (!bMostrarInterface)
            {
                GenerarReporte_Thread(); 
            }
            else 
            {
                this.ShowDialog();
            }

            return bReporteGenerado; 
        }

        public void GenerarReporte_Thread()
        {
            bool bRegresa = false;
            byte[] btReporte = null;

            try
            {
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1;
                tmRevisarGeneracion.Start();

                if (bImpresionWeb)
                {
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = datosCliente.DatosCliente();

                    conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                    conexionWeb.Url = sUrl;
                    conexionWeb.Timeout = 5000000;

                    if (bReporteRemoto)
                    {
                        // Reporte generado usando el Regional como puente para conexiones VPN 
                        btReporte = conexionWeb.ReporteRemoto(datosConexionUnidad.dtsInformacion, datosC, InfoWeb);
                    }
                    else
                    {
                        btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                    }

                    tmRevisarGeneracion.Stop();
                    tmRevisarGeneracion.Enabled = false;

                    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true, false); 
                }
                else
                {
                    // Conexion local no requiere tiempo limite 
                    tmRevisarGeneracion.Stop();
                    tmRevisarGeneracion.Enabled = false; 

                    myRpt.CargarReporte(false, false); 
                    bRegresa = !myRpt.ErrorAlGenerar; 
                }
            }
            catch (Exception ex)
            {
                Error.LogError("GenerarReporte_Thread .... " + ex.Message);
            }

            if (!bCanceladoPorError)
            {
                bReporteGenerado = bRegresa;
                this.Hide();
            }
        }

        private void FrmReporteador_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch { } 
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            tmRevisarGeneracion.Stop();
            tmRevisarGeneracion.Enabled = false;

            bCanceladoPorError = false;
            bCanceladoPorUsuario = true;

            CancelarReporte();
            CancelarReporte();

            this.Hide();
        }

        private void CancelarReporte()
        {
            //tmRevisarGeneracion
            try
            {
                try
                {
                    conexionWeb = null;
                    thrReporte.Abort();
                    conexionWeb = null;
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source; 
                }

                thrReporte.Abort();                 
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }  
        }
    }
}