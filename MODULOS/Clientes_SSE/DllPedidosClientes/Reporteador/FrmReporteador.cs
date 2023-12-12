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

////using DllFarmaciaSoft;
////using DllFarmaciaSoft.Conexiones;
////using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllPedidosClientes.Reporteador
{
    internal partial class FrmReporteador : FrmBaseExt 
    {
        #region Declaracion de variables
        //basGenerales Fg = new basGenerales();
        // DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion conexionWeb = null; 
        DllPedidosClientes.wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb = null; 

        Thread thrReporte; 

        // bool bConexionWeb = false;
        // bool bConectando = true;
        public bool bExisteFileConfig = true;
        public bool bConexionEstablecida = false;

        clsImprimir myRpt;
        // clsDatosCliente datosCliente;
        string sUrl = ""; 
        bool bImpresionWeb = false;
        
        bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        // bool bReporteRemoto = false;

        string sEstado = "";
        string sFarmacia = "";
        DataSet dtsInformacionReporteWeb = new DataSet();
        DataSet dtsInformacionCliente = new DataSet();
        bool bEsRegional = false; 

        #endregion
        public FrmReporteador()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public FrmReporteador(clsImprimir Reporte, string Url, string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente, bool EsRegional)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            myRpt = Reporte;
            sUrl = Url;
            sEstado = Estado;
            sFarmacia = Farmacia;
            dtsInformacionReporteWeb = InformacionReporteWeb;
            dtsInformacionCliente = InformacionCliente;
            bEsRegional = EsRegional; 
        }

        #region Funciones y Procedimientos Privados 
        private void FrmReporteador_Load(object sender, EventArgs e)
        {
            tmIniciarProceso.Interval = 1500;
            tmIniciarProceso.Enabled = true;
            tmIniciarProceso.Start(); 
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
            
            this.ShowDialog(); 

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

                ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////DataSet datosC = datosCliente.DatosCliente();

                conexionWeb = new DllPedidosClientes.wsCnnClienteAdmin.wsCnnClientesAdmin(); // new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = 500000;

                // Reporte generado usando el Regional como puente para conexiones VPN 
                if (bEsRegional)
                {
                    btReporte = conexionWeb.ReporteExtendidoGeneral(sEstado, sFarmacia, dtsInformacionReporteWeb, dtsInformacionCliente); 
                }
                else 
                {
                    if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad)
                    {
                        btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, dtsInformacionReporteWeb, dtsInformacionCliente);
                    }
                    else
                    {
                        btReporte = conexionWeb.Reporte(dtsInformacionReporteWeb, dtsInformacionCliente);
                    }
                } 


                tmRevisarGeneracion.Stop();
                tmRevisarGeneracion.Enabled = false;

                bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true, false); 
            }
            catch 
            { 
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
    }
}