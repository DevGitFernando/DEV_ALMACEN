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
    public partial class FrmReportesFacturacionUnidad : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        // clsGrid Grid;

        // string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = ""; 
        // string sUrl_RutaReportes = "";
        string sIdEstado = DtGeneralPedidos.EstadoConectado;
        string sIdFarmacia = DtGeneralPedidos.FarmaciaConectada;
        string sIdPublicoGeneral = DtGeneralPedidos.PublicoGeneral;
        string sTablaFarmacia = "";
        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        public FrmReportesFacturacionUnidad()
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
            //Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            Consultas = new clsConsultas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, true);
                        
            //Ayuda = new clsAyudas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);

            Ayuda = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, true);

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);            
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;
            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 
                    
            cboReporte.Add("CteUni_Admon_ConcentradoInsumos.rpt", "Concentrado de Dispensación");
            cboReporte.Add("CteUni_Admon_ConcentradoInsumosDesglozado.rpt", "Concentrado de Dispensación desglozado");
            cboReporte.Add("CteUni_Admon_ConcentradoInsumosPrograma.rpt", "Concentrado de Dispensación Por Programa");
            cboReporte.Add("CteUni_Admon_ConcentradoInsumosProgramaTotalizado.rpt", "Concentrado de Dispensación Por Programa Totalizado");

            cboReporte.Add("CteUni_Admon_Validacion.rpt", "Detallado de Dispensación (Validación)");

            cboReporte.Add("CteUni_Admon_DiagnosticosDetallado.rpt", "Incidencias Epidemiologicas");

            cboReporte.Add("CteUni_Admon_MedicosDetallado.rpt", "Detallado Por Medico");
            cboReporte.Add("CteUni_Admon_CostoPorMedico.rpt", "Costo Por Medico");

            cboReporte.Add("CteUni_Admon_PacientesDet.rpt", "Detallado Por Paciente");
            cboReporte.Add("CteUni_Admon_CostoPorPaciente.rpt", "Costo Por Paciente");

            cboReporte.Add("CteUni_Admon_ServiciosAreas.rpt", "Detallado Por Servicios y Areas");

            cboReporte.SelectedIndex = 0; 
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

                ////string sSql = "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";
                ////if (!leerWeb.Exec(sSql))
                ////{
                ////    Error.GrabarError(leerWeb, "CargarEstados()");
                ////    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                ////}
                ////else
                ////{
                ////    cboEstados.Add(leerWeb.DataSetClase, true, "IdEstado", "Estado");
                ////}
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

                ////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                ////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                ////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);
                //////Grid.Limpiar(false);

                ////if (!leerWeb.Exec(sSqlFarmacias))
                ////{
                ////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                ////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                ////}
                ////else
                ////{
                ////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                ////    //Grid.LlenarGrid(leer.DataSetClase);
                ////    //grdReporte.Focus();
                ////}
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

        #region Buscar Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leerWeb.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), "", "txtCte_Validating");
                if (leerWeb.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }

        }

        private void CargarDatosCliente()
        {
            txtCte.Text = leerWeb.Campo("IdCliente");
            lblCte.Text = leerWeb.Campo("NombreCliente");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerWeb.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, "txtCte_KeyDown");
                if (leerWeb.Leer())
                {
                    CargarDatosCliente();
                }
            }

        }
        #endregion Buscar Cliente

        #region Buscar SubCliente 
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leerWeb.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtCte_Validating");
                if (leerWeb.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }

        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Text = leerWeb.Campo("IdSubCliente");
            lblSubCte.Text = leerWeb.Campo("NombreSubCliente");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerWeb.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), "txtSubCte_KeyDown");
                if (leerWeb.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }
        #endregion Buscar SubCliente

        #region Buscar Programa 
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPro.Text.Trim() != "")
            {
                {
                    leerWeb.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_Validating");
                    if (leerWeb.Leer())
                    {
                        CargarDatosProgramas();
                    }
                    else
                    {
                        lblPro.Text = "";
                        txtPro.Focus();
                    }
                }
            }
        }

        private void CargarDatosProgramas()
        {
            txtPro.Text = leerWeb.Campo("IdPrograma");
            lblPro.Text = leerWeb.Campo("Programa");
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerWeb.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtPro_KeyDown");
                if (leerWeb.Leer())
                {
                    CargarDatosProgramas();
                }
            }
        }
        #endregion Buscar Programa

        #region Buscar SubPrograma
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPro.Text.Trim() != "")
            {
                leerWeb.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(), "txtSubPro_Validating");
                if (leerWeb.Leer())
                {
                    CargarDatosSubProgramas();
                }
                else
                {
                    lblSubPro.Text = "";
                    txtSubPro.Focus();
                }
            }
        }

        private void CargarDatosSubProgramas()
        {
            txtSubPro.Text = leerWeb.Campo("IdSubPrograma");
            lblSubPro.Text = leerWeb.Campo("SubPrograma");
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerWeb.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, sIdEstado, sIdFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_KeyDown");
                if (leerWeb.Leer())
                {
                    CargarDatosSubProgramas();
                }
            }
        }
        #endregion Buscar SubPrograma

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

            if (bRegresa && cboReporte.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
                cboReporte.Focus(); 
            }

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
                myRpt.NombreReporte = cboReporte.Data + ""; 

                //if (General.ImpresionViaWeb)
                {
                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();

                    //////conexionWeb.Url = General.Url;
                    ////////conexionWeb.Url = "http://phosrv1/wsCliente/wsClienteUnidad.asmx";
                    //////conexionWeb.Timeout = 300000;
                    //////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC); 
                    //////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);


                    // bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

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
            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 

            FrameCliente.Enabled = bValor;
            FrameInsumos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            FrameListaReportes.Enabled = bValor;

            CargarEstados();
            txtCte.Focus();

            // BorrarTablas(); 
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

            FrameCliente.Enabled = false;
            FrameInsumos.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            FrameListaReportes.Enabled = false;

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

        #region Eventos
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
            txtPro.Text = "";
            //Grid.Limpiar();
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            txtPro.Text = "";
            //Grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            txtSubPro.Text = "";
            //Grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
            //Grid.Limpiar();
        }

        #endregion Eventos

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
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";

            // bEjecutando = true; 
            int iTipoInsumo = 0, iTipoDispensacion = 0, iTipoInsumoMedicamento = 0;
            

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispConsignacion.Checked)
                iTipoDispensacion = 1;

            if (rdoTpDispVenta.Checked)
                iTipoDispensacion = 2;


            // Determinar que tipo de producto se mostrar 
            if (rdoInsumosMedicamento.Checked)
                iTipoInsumo = 1;
            
            if (rdoInsumoMatCuracion.Checked)
                iTipoInsumo = 2;

            // Determinar si pertenecen o no a Seguro Popular
            if (rdoInsumoMedicamentoSP.Checked)
            {
                iTipoInsumo = 1;
                iTipoInsumoMedicamento = 1;
            }

            if (rdoInsumoMedicamentoNOSP.Checked)
            {
                iTipoInsumo = 1;
                iTipoInsumoMedicamento = 2;
            }

            sTablaFarmacia = "CTE_FarmaciasProcesar";

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos_Unidad '{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
               cboEstados.Data, DtGeneralPedidos.FarmaciaConectada, txtCte.Text, txtSubCte.Text, txtPro.Text,
                txtSubPro.Text, iTipoDispensacion, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoInsumo, iTipoInsumoMedicamento);
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion_Unidad (NoLock) ");

            try 
            {
                leer.Reset();
                leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia); 
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
            FrameCliente.Enabled = true;
            FrameInsumos.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameListaReportes.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameListaReportes.Enabled = true;                
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;

                if (bSeEncontroInformacion)
                {
                    btnEjecutar.Enabled = false;
                    btnImprimir.Enabled = true; 
                }
                else 
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles();

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
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

        private void LevantaForma()
        {
            FrmListadoBeneficiarios f = new FrmListadoBeneficiarios(General.DatosConexion);
            f.MostrarListaBeneficiarios(General.DatosConexion);
        }

        private void listadoBeneficiariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevantaForma();
        }

        private void BorrarTablas()
        {
            string sSql = string.Format(" Exec spp_Mtto_BorrarTablasRpt_Clientes ");

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(leerWeb, "BorrarTablas()");
                General.msjError("Ocurrió un error al borrar las tablas.");
            }
            else
            {

            }
        }

        private void FrmReportesFacturacionUnidad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                //// Habilitar 
                // BorrarTablas(); 
            } 
        }

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
