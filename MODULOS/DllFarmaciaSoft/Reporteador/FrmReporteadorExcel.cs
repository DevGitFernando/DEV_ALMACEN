using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using Microsoft.VisualBasic; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllFarmaciaSoft.Reporteador
{
    internal partial class FrmReporteadorExcel : FrmBaseExt
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

        string sReporte = "";
        string sRutaReportes = "";
        string sRutaSalida = "";
        // clsImprimir myRpt;
        clsDatosCliente datosCliente;
        // clsConexionClienteUnidad datosConexionUnidad;
        string sUrl = ""; 
        bool bImpresionWeb = false;
        
        bool bReporteGenerado = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 
        #endregion

        #region Constructores y Destructores de Clase
        public FrmReporteadorExcel()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }

        public FrmReporteadorExcel(string Reporte, string RutaReportes, clsDatosCliente DatosTerminal)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            sReporte = Reporte.ToUpper();
            sRutaReportes = RutaReportes; 

            if (!sReporte.Contains(".XLS"))
            {
                sReporte = sReporte + ".xls";
            }

            datosCliente = DatosTerminal; 
            sRutaSalida = Path.Combine(Application.StartupPath + @"\\Plantillas\", "");  
        }
        #endregion Constructores y Destructores de Clase

        #region Funciones y Procedimientos Privados
        private void FrmReporteadorExcel_Load(object sender, EventArgs e)
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

                // if (bImpresionWeb)
                {

                    conexionWeb = new DllFarmaciaSoft.wsFarmaciaSoftGn.wsConexion();
                    conexionWeb.Url = sUrl;
                    conexionWeb.Timeout = 500000;


                    // Reporte generado usando el Regional como puente para conexiones VPN 
                    btReporte = conexionWeb.ReporteExcel(sReporte, sRutaReportes);

                    if (btReporte != null)
                    {
                        if (!Directory.Exists(sRutaSalida))
                        {
                            Directory.CreateDirectory(sRutaSalida); 
                        }

                        try
                        {
                            //// Forzar el borrado del archivo existente 
                            if (File.Exists(sRutaSalida + sReporte))
                            {
                                File.Delete(sRutaSalida + sReporte); 
                            }
                        }
                        catch { }

                        Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaSalida + sReporte, btReporte, false);
                        bRegresa = true;
                    }

                    tmRevisarGeneracion.Stop();
                    tmRevisarGeneracion.Enabled = false; 
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

        private void FrmReporteadorExcel_FormClosing(object sender, FormClosingEventArgs e)
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