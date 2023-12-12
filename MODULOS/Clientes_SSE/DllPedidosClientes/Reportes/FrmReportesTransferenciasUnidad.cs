using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmReportesTransferenciasUnidad : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;        
        // clsGrid Grid;

        // string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = ""; 
        // string sUrl_RutaReportes = "";
        string sTablaFarmacia = "";

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        public FrmReportesTransferenciasUnidad()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ReportesFacturacionUnidad");
            leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            // Se crea la instancia del objeto de la clase de auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);            
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;
            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            ////cboReporte.Clear();
            ////cboReporte.Add(); // Agrega Item Default 
                    
            ////cboReporte.Add("CteUni_Admon_ConcentradoInsumos.rpt", "Concentrado de Dispensación");
            ////cboReporte.Add("CteUni_Admon_ConcentradoInsumosDesglozado.rpt", "Concentrado de Dispensación desglozado");
            ////cboReporte.Add("CteUni_Admon_ConcentradoInsumosPrograma.rpt", "Concentrado de Dispensación Por Programa");
            ////cboReporte.Add("CteUni_Admon_ConcentradoInsumosProgramaTotalizado.rpt", "Concentrado de Dispensación Por Programa Totalizado");

            ////cboReporte.Add("CteUni_Admon_Validacion.rpt", "Detallado de Dispensación (Validación)");

            ////cboReporte.Add("CteUni_Admon_DiagnosticosDetallado.rpt", "Incidencias Epidemiologicas");

            ////cboReporte.Add("CteUni_Admon_MedicosDetallado.rpt", "Detallado Por Medico");
            ////cboReporte.Add("CteUni_Admon_CostoPorMedico.rpt", "Costo Por Medico");

            ////cboReporte.Add("CteUni_Admon_PacientesDet.rpt", "Detallado Por Paciente");
            ////cboReporte.Add("CteUni_Admon_CostoPorPaciente.rpt", "Costo Por Paciente");

            ////cboReporte.Add("CteUni_Admon_ServiciosAreas.rpt", "Detallado Por Servicios y Areas");

            ////cboReporte.SelectedIndex = 0; 
        } 

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            //CargarEstados();
        }

        #region Cargar Combos         

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

                //////string sSql = "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";
                //////if (!leerWeb.Exec(sSql))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarEstados()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                //////}
                //////else
                //////{
                //////    cboEstados.Add(leerWeb.DataSetClase, true, "IdEstado", "Estado");
                //////}
            }
            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
        }
                
        private void CargarFarmacias()
        {
            if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                //////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                //////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                //////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, General.EntidadConectada);
                ////////Grid.Limpiar(false);

                //////if (!leerWeb.Exec(sSqlFarmacias))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                //////}
                //////else
                //////{
                //////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                //////    //Grid.LlenarGrid(leer.DataSetClase);
                //////    //grdReporte.Focus();
                //////}
            }
            cboFarmacias.SelectedIndex = 0;
            cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
            cboFarmacias.Enabled = false;

            txtFarmacia.Text = DtGeneralPedidos.FarmaciaConectada;
            lblFarmacia.Text = DtGeneralPedidos.FarmaciaConectadaNombre;
            txtFarmacia.Enabled = false;

        }

        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Impresion  
        private void ObtenerRutaReportes()
        {
            
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            ////if (bRegresa && cboReporte.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            ////    cboReporte.Focus(); 
            ////}

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {           
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

                DatosCliente.Funcion = "Imprimir()";              
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sEstado = cboEstados.Data;
                //string sFarmacia = cboFarmacias.Data;
                string sFarmacia = Fg.PonCeros(txtFarmacia.Text, 4);
                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = "Rpt_CteReg_Seguimiento_Transferencias_Entrada";
                myRpt.NombreReporte = "Rpt_CteUnidad_Seguimiento_Transferencias_Entrada";

                if (rdoTS.Checked)
                {
                    myRpt.NombreReporte = "Rpt_CteReg_Seguimiento_Transferencias_Salida"; //  cboReporte.Data + ""; 
                    myRpt.NombreReporte = "Rpt_CteUnidad_Seguimiento_Transferencias_Salida";
                }

                //if (General.ImpresionViaWeb)
                {
                    ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    ////DataSet datosC = DatosCliente.DatosCliente();

                    ////conexionWeb.Url = General.Url;
                    ////conexionWeb.Timeout = 300000;
                    ////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC);
                    ////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();
                    bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 
                }
                //else
                //{
                    // Lineas para pruebas locales ///////
                    //myRpt.CargarReporte(true);
                    //bRegresa = !myRpt.ErrorAlGenerar;
                    //////////////////////////////////////
                //}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true); 
            rdoTE.Checked = true;
            rdoInsumosAmbos.Checked = true;
            rdoTpDisAmbos.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 

            FrameTransferencias.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;

            CargarEstados(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
            {
                IniciarProcesamiento();
            }
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;           

            FrameTransferencias.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
               
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }
        #endregion Botones 

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                //leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniOficinaCentral, DatosCliente);

                conexionWeb.Url = General.Url;//sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniOficinaCentral));

                //DatosDeConexion.Servidor = sHost;
                bRegresa = true; 
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
                ActivarControles(); 
            }

            return bRegresa; 
        }

        private void ObtenerInformacion()
        {           
            bEjecutando = true;
            string sStore = "";
            string sTabla = ""; 
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";
            // bEjecutando = true; 
            int iTipoDispensacion = 0, iTipoInsumo = 0;

            sTablaFarmacia = "CTE_FarmaciasProcesar";

            sStore = " spp_Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Entrada ";
            sTabla = " Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Entrada ";
            // sTabla = " Rpt_CteUnidad_Seguimiento_Transferencias_Entrada "; 
            if (rdoTS.Checked)
            {
                sStore = " spp_Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Salida ";
                sTabla = " Rpt_CteUnidad_Impresion_Seguimiento_Transferencias_Salida ";
                // sTabla = " Rpt_CteUnidad_Seguimiento_Transferencias_Salida ";
            }

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 1;
            }

            if (rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 2;
            }

            // Determinar que tipo de producto se mostrar 
            if (rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            string sSql = ""; 
            sSql = string.Format("Set Dateformat YMD " + 
                " Exec  {0} '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'  ",
                sStore, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoDispensacion, iTipoInsumo); 
            sSql += "\n " + string.Format( "Select top 1 * From {0} (NoLock) ", sTabla );

            try
            {
                leer.Reset(); 
                leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia);
                sSql = sTablaFarmacia; 
            }
            catch { } 


            if (leer.SeEncontraronErrores())
            {
                btnImprimir.Enabled = false;
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {

                btnImprimir.Enabled = true;
                bSeEncontroInformacion = true;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    bSeEjecuto = true;
                }
            }

            bEjecutando = false;

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosUni("*", sCadena);
           
            //////if (FarmaciasAProcesar())            
            //////{                 
            ////    if (!leer.Exec(sSql))
            ////    {
            ////        Error.GrabarError(leer, "ObtenerInformacion()");
            ////        General.msjError("Ocurrió un error al obtener la información del reporte.");
            ////    }
            ////    else
            ////    {
            ////        if (leer.Leer())
            ////        {                        
            ////            btnImprimir.Enabled = true;
            ////            bSeEncontroInformacion = true;
            ////            //ObtenerRutaReportes();
            ////        }
            ////        else
            ////        {
            ////            bSeEncontroInformacion = false;                        
            ////        }

            ////        sCadena = sSql.Replace("'", "\"");
            ////        auditoria.GuardarAud_MovtosUni("*", sCadena);
            ////    }

            ////    bSeEjecuto = true; 
            ////    bEjecutando = false;  
            //////}
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            FrameTransferencias.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = false;

                if (!bSeEncontroInformacion) 
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles(); 

                    if ( bSeEjecuto ) 
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }       

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;                
                CargarFarmacias();
            }
        }

        #region FrameFechas
        private void FrameFechas_Enter(object sender, EventArgs e)
        {

        }

        private void FrameInsumos_Enter(object sender, EventArgs e)
        {

        }

        private void FrameListaReportes_Enter(object sender, EventArgs e)
        {

        }

        private void FrameDispensacion_Enter(object sender, EventArgs e)
        {

        }
        #endregion FrameFechas

        //private bool FarmaciasAProcesar()
        //{
        //    bool bReturn = false;
        //    string sEdo = "", sFar = "";
        //    sEdo = cboEstados.Data;

        //    string sSql = string.Format("Delete From CTE_FarmaciasProcesar " );

        //    if (!leer.Exec(sSql))
        //    {
        //        Error.GrabarError(leer, "FarmaciasAProcesar()");
        //        General.msjError("Ocurrió un error al borrar tabla.");
        //        bReturn = false;
        //    }
        //    else
        //    {
        //        for (int i = 1; i <= Grid.Rows; i++)
        //        {
        //            if( Grid.GetValueBool(i,3) )
        //            {
        //                sFar = Grid.GetValue(i,1);
                        
        //               string sQuery = string.Format(" Insert Into CTE_FarmaciasProcesar " +
        //                                    " Select '{0}','{1}','A',0 ", sEdo, sFar );
        //               if (!leer.Exec(sQuery))
        //                {
        //                    Error.GrabarError(leer, "FarmaciasAProcesar()");
        //                    General.msjError("Ocurrió un error al Insertar Farmacias a Procesar.");
        //                    bReturn = false;
        //                    break;
        //                }
        //                else
        //                {
        //                    bReturn = true;
        //                }
        //            }
        //        }
        //    }

        //    return bReturn;
        //}       

        //private void chkTodos_CheckedChanged(object sender, EventArgs e)
        //{
        //    Grid.SetValue(3, chkTodos.Checked);
        //}
    } 
}
