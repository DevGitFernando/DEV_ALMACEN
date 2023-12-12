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
using Dll_SII_INadro;
using Dll_SII_INadro.wsIntAlmacen; 

namespace Dll_SII_INadro.PedidosUnidades
{
    internal partial class FrmDescargarPedidoUnidad : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        wsIntAlmacen.wsInterfaceAlmacen conexionWeb = null; 

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
        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolioReferenciaPedido = "";

        bool bOrdenDescargada = false;
        public DataSet dtsDatosOrden = new DataSet();

        // bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 
        #endregion Declaracion de variables

        #region Constructores 
        public FrmDescargarPedidoUnidad(string Url, string Empresa, string Estado, string Farmacia, string Folio)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sUrl = Url;
            this.sIdEmpresa = Empresa;
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.sFolioReferenciaPedido = Folio; 
        } 
        #endregion Constructores

        #region Funciones y Procedimientos Privados
        private void FrmDescargarOC_Load(object sender, EventArgs e)
        {

            this.Height = 107;
            this.Width = 413;
            //413, 107

            FrameProceso.Left = 7;
            FrameProceso.Top = 30;
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

            thrReporte = new Thread(this.DescargarPedido_Thread);
            thrReporte.Name = "DescargandoPedido";
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

        private void DescargarPedido_Thread()
        {
            // bool bRegresa = false;
            // byte[] btReporte = null;

            bOrdenDescargada = false; 
            try
            { 
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1; 
                tmRevisarGeneracion.Start();


                conexionWeb = new wsIntAlmacen.wsInterfaceAlmacen();
                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = 500000;

                dtsDatosOrden = conexionWeb.InformacionDePedido(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioReferenciaPedido);
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

        private void FrmDescargarOC_FormClosing(object sender, FormClosingEventArgs e)
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