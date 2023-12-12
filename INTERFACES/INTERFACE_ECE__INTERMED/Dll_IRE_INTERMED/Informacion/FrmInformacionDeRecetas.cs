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
using DllFarmaciaSoft.wsFarmacia;
using Dll_IRE_INTERMED.Clases;

namespace Dll_IRE_INTERMED.Informacion
{
    public partial class FrmInformacionDeRecetas : FrmBaseExt
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

        TipoProcesoReceta tpTipoProcesoReceta = TipoProcesoReceta.Ninguno; 
        string sReporte = "";
        string sRutaReportes = "";
        string sRutaSalida = "";
        // clsImprimir myRpt;
        clsDatosCliente datosCliente;
        // clsConexionClienteUnidad datosConexionUnidad;
        string sUrl = ""; 
        bool bImpresionWeb = false;
        
        bool bRecetasDescargadas = false;
        bool bCanceladoPorError = false;
        public bool bCanceladoPorUsuario = false;
        // bool bReporteRemoto = false; 

        ClsReplicacioneRecetaElectronica replicacion; 

        #endregion

        #region Constructores y Destructores de Clase
        public FrmInformacionDeRecetas( TipoProcesoReceta TipoDeProceso )
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            tpTipoProcesoReceta = TipoDeProceso;

            btnDetener.Enabled = false;


            if(TipoDeProceso == TipoProcesoReceta.EnviarInformacionSurtido)
            {
                this.Text = "Envió de información de las recetas atendidas"; 
            }

        }
        #endregion Constructores y Destructores de Clase

        #region Funciones y Procedimientos Privados
        private void FrmInformacionDeRecetas_Load(object sender, EventArgs e)
        {
            replicacion = new ClsReplicacioneRecetaElectronica();

            tmIniciarProceso.Interval = 1500;
            tmIniciarProceso.Enabled = true;
            tmIniciarProceso.Start(); 
        }

        private void tmIniciarProceso_Tick(object sender, EventArgs e)
        {
            tmIniciarProceso.Stop();
            tmIniciarProceso.Enabled = false;

            thrReporte = new Thread(this.ProcesarSolicitudDeInformacion_Thread);
            thrReporte.Name = "DescargarRecetas";
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
                bRecetasDescargadas = false;
                this.Hide(); 
            }
        } 
        #endregion Funciones y Procedimientos Privados 

        public bool ProcesarSolicitudDeInformacion()
        {
            this.ShowDialog();

            return bRecetasDescargadas; 
        }

        private void ProcesarSolicitudDeInformacion_Thread()
        {
            bool bRegresa = false;
            byte[] btReporte = null;


            bRecetasDescargadas = false;

            try
            {
                tmRevisarGeneracion.Enabled = true;
                tmRevisarGeneracion.Interval = ((500) * (60)) * 1;
                tmRevisarGeneracion.Start();

                if(tpTipoProcesoReceta == TipoProcesoReceta.DescargaRecetasMasivo)
                {
                    bRecetasDescargadas = replicacion.ObtenerRecetasElectronicas();
                }

                if(tpTipoProcesoReceta == TipoProcesoReceta.EnviarInformacionSurtido)
                {
                    bRecetasDescargadas = replicacion.EnviarRecetasElectronicasAtendidas();
                }

                tmRevisarGeneracion.Stop();
                tmRevisarGeneracion.Enabled = false; 
            }
            catch (Exception ex)
            {
                Error.LogError("GenerarReporte_Thread .... " + ex.Message);
            }


            // Forzar el cierre de la ventana 
            this.Hide();


            ////if (!bCanceladoPorError)
            ////{
            ////    bRecetasDescargadas = bRegresa;
            ////    this.Hide();
            ////}
        }

        private void FrmInformacionDeRecetas_FormClosing(object sender, FormClosingEventArgs e)
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