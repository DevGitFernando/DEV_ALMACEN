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

namespace Farmacia.VentasRecetaElectronica
{
    internal partial class FrmDescargarRecetaElectronica : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        Farmacia.wsRecetarioElectronico.wsRecetario conexionWeb = null; 
         

        Thread thrReporte; 

        // bool bConexionWeb = false;
        // bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;

        // clsImprimir myRpt;
        // clsDatosCliente datosCliente;
        // clsConexionClienteUnidad datosConexionUnidad; 
        // bool bImpresionWeb = false;

        string sUrl = "";
        string sIdEstado = "";
        string sCLUES = "";
        string sFolioRecetaElectronica = "";

        bool bOrdenDescargada = false;
        public DataSet dtsDatosOrden = new DataSet();

        // bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 
        #endregion Declaracion de variables

        #region Constructores 
        public FrmDescargarRecetaElectronica(string Url, string Estado, string CLUES, string Folio)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sUrl = Url;
            this.sIdEstado = Estado;
            this.sCLUES = CLUES;
            this.sFolioRecetaElectronica = Folio; 
        } 
        #endregion Constructores

        #region Funciones y Procedimientos Privados
        private void FrmDescargarRecetaElectronica_Load(object sender, EventArgs e)
        {

            this.Height = 95;
            this.Width = 260;
            FrameProceso.Left = 6;
            FrameProceso.Top = 20;
            this.Height = 105;

            ////if (DtGeneral.EsEquipoDeDesarrollo)
            ////{
            ////    toolStripProceso.Visible = true;
            ////    this.Height += toolStripProceso.Height;
            ////    FrameProceso.Top += toolStripProceso.Height;
               
            ////    FrameProceso.Top = 20;
            ////    this.Height = 105;
            ////} 

            tmIniciarProceso.Interval = 1500;
            tmIniciarProceso.Enabled = true;
            tmIniciarProceso.Start(); 
        }

        private void tmIniciarProceso_Tick(object sender, EventArgs e)
        {
            tmIniciarProceso.Stop();
            tmIniciarProceso.Enabled = false;

            thrReporte = new Thread(this.DescargarRecetaElectronica_Thread);
            thrReporte.Name = "DescargandoRecetaElectronica";
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
                bOrdenDescargada = false;
                this.Hide(); 
            }
        } 
        #endregion Funciones y Procedimientos Privados 

        public bool Descargar()
        {
            bOrdenDescargada = false; 
            this.ShowDialog();

            return bOrdenDescargada; 
        }

        private void DescargarRecetaElectronica_Thread()
        {
            // bool bRegresa = false;
            // byte[] btReporte = null;

            bOrdenDescargada = false; 
            try
            { 
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1; 
                tmRevisarGeneracion.Start();


                conexionWeb = new wsRecetarioElectronico.wsRecetario();
                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = 500000;

                dtsDatosOrden = conexionWeb.GetRecetaInformacion(sIdEstado, sCLUES, sFolioRecetaElectronica);
                bOrdenDescargada = true; 
            }
            catch (Exception ex)
            {
                Error.LogError("Descargar .... " + ex.Message);
            }

            if (!bCanceladoPorError)
            {
                // bReporteGenerado = bRegresa;
                this.Hide();
            }
        }

        private void FrmDescargarRecetaElectronica_FormClosing(object sender, FormClosingEventArgs e)
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

            CancelarDescarga();
            CancelarDescarga();

            this.Hide();
        }

        private void CancelarDescarga()
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