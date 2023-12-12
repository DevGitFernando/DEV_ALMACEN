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

namespace DllFarmaciaSoft.IntercambioCartaCanje
{
    internal partial class FrmDescargarCartaCanje : FrmBaseExt
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoIniManager Ini; // = new clsIniManager();
        DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWeb = null;
        clsConexionClienteUnidad conexionCte;
        clsDatosCliente DatosCliente;

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
        string sFolioCartaCanje = "";

        bool bOrdenDescargada = false;
        public DataSet dtsDatosCartaCanje = new DataSet();

        // bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 
        #endregion Declaracion de variables

        #region Constructores 
        public FrmDescargarCartaCanje(string Url, string Empresa, string Estado, string Farmacia, string Folio)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sUrl = Url;
            this.sIdEmpresa = Empresa;
            this.sIdEstado = Estado;
            this.sIdFarmacia = Farmacia;
            this.sFolioCartaCanje = Folio; 
        } 
        #endregion Constructores

        #region Funciones y Procedimientos Privados
        private void FrmDescargarOC_Load(object sender, EventArgs e)
        {

            ////this.Height = 95;
            ////this.Width = 260;
            ////FrameProceso.Left = 6;
            ////FrameProceso.Top = 20;
            ////this.Height = 105;

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

            thrReporte = new Thread(this.DescargarOrdenDeCompra_Thread);
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

        private void DescargarOrdenDeCompra_Thread()
        {
            string sSql = "";
            clsLeer leerOrden = new clsLeer();
            // bool bRegresa = false;
            // byte[] btReporte = null;

            //conexionCte = new clsConexionClienteUnidad();
            //conexionCte.Empresa = sIdEmpresa;
            //conexionCte.Estado = sIdEstado;
            //conexionCte.Farmacia = sIdFarmacia;
            //conexionCte.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
            //conexionCte.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");

            bOrdenDescargada = false; 
            try
            { 
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1; 
                tmRevisarGeneracion.Start();


                conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = 500000;

                //dtsDatosCartaCanje = conexionWeb.InformacionOrdenCompra(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioCartaCanje);

                sSql = string.Format(
                        "Exec spp_ObtenerInformacionCartaCanje " + 
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sFolioCartaCanje); 

                dtsDatosCartaCanje = conexionWeb.ExecuteExt(DatosCliente.DatosCliente(), DtGeneral.CfgIniOficinaCentral, sSql);
                bOrdenDescargada = true;

                leerOrden.DataSetClase = dtsDatosCartaCanje;

                if (!leerOrden.SeEncontraronErrores())
                {
                    leerOrden.RenombrarTabla(1, "RutasDistribucionEnc");
                    //leerOrden.RenombrarTabla(2, "RutasDistribucionDet_CartasCanje");

                    //leerOrden.RenombrarTabla(3, "Vehiculos_SQL");
                    //leerOrden.RenombrarTabla(4, "RutasDistribucionEnc_SQL");
                    //leerOrden.RenombrarTabla(5, "RutasDistribucionDet_SQL");
                    //leerOrden.RenombrarTabla(6, "RutasDistribucionDet_CartasCanje_SQL");
                }

                dtsDatosCartaCanje = leerOrden.DataSetClase;
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