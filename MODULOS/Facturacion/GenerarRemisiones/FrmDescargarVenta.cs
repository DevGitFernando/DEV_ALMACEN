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

namespace Facturacion.GenerarRemisiones
{
    internal partial class FrmDescargarVenta : FrmBaseExt
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
        public string sMensaje = "";

        // clsImprimir myRpt;
        // clsDatosCliente datosCliente;
        // clsConexionClienteUnidad datosConexionUnidad; 
        // bool bImpresionWeb = false;
        ClsRemision_Ventas Info;

        string sUrl = "";
        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";
        string sFolio = "";

        public bool bVentaDescargada = false;
        public DataSet dtsDatosOrden = new DataSet();

        // bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 
        #endregion Declaracion de variables

        #region Constructores 
        public FrmDescargarVenta(string Url, string Empresa, string Estado, string Farmacia, string Folio)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sUrl = Url;
            this.sIdEmpresa = Empresa;
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.sFolio = Folio;
            Info = new ClsRemision_Ventas(sUrl, Empresa, Estado, Farmacia, Folio);
        } 
        #endregion Constructores

        #region Funciones y Procedimientos Privados
        private void FrmDescargarVenta_Load(object sender, EventArgs e)
        {

            //this.Height = 95;
            //this.Width = 260;
            //FrameProceso.Left = 6;
            //FrameProceso.Top = 20;
            //this.Height = 105;

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

            thrReporte = new Thread(this.DescargarVenta_Thread);
            thrReporte.Name = "DescargandoOrdenDeCompra ";
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
                bVentaDescargada = false;
                this.Hide(); 
            }
        } 
        #endregion Funciones y Procedimientos Privados 

        public bool Descargar()
        {
            bVentaDescargada = false; 
            this.ShowDialog();

            return bVentaDescargada; 
        }

        private void DescargarVenta_Thread()
        {
            bool bRegresa = true;
            // byte[] btReporte = null;

            bVentaDescargada = false; 
            try
            { 
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1; 
                tmRevisarGeneracion.Start();


                //conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                //conexionWeb.Url = sUrl;
                //conexionWeb.Timeout = 500000;

                //dtsDatosOrden = conexionWeb.InformacionOrdenCompra(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioOrdenCompra);

                bVentaDescargada = Info.InformacionOrdenCompra();

                if(bVentaDescargada)
                {
                    bRegresa = Info.RegistrarEnRegional();
                   
                    if (!bRegresa)
                    {
                        bVentaDescargada = false;
                        sMensaje = Info.sMensaje;
                        General.msjError(sMensaje);
                    }
                }
                else
                {
                    sMensaje = "No se encontró la información de la venta.";
                    bRegresa = false;
                    General.msjUser(sMensaje);
                }
                
            }
            catch (Exception ex)
            {
                Error.LogError("Descargar .... " + ex.Message);
                Error.GrabarError(General.DatosConexion, ex, "DescargarVenta_Thread"); 
                //General.msjError(ex.Message);
            }

            if (!bCanceladoPorError)
            {
                // bReporteGenerado = bRegresa;
                this.Hide();
            }
        }

        private void FrmDescargarVenta_FormClosing(object sender, FormClosingEventArgs e)
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