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
    public partial class FrmReportesSalidasUnidad : FrmBaseExt
    {
        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;   
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid Grid;

        FrmListaDeSubFarmacias SubFarmacias;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "", sTablaClavesSSA = "";

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet();
        DataSet dtsClaves = new DataSet();

        DataSet dtsClavesProcesar = new DataSet();
        string sTablaFarmacia = "CTE_FarmaciasProcesar";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        public FrmReportesSalidasUnidad()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ReportesFacturacionUnidad");
            //leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);

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
            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);

            // Se crea la instancia del objeto de la clase de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);            
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;

            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);

            grdClaves.Sheets[0].Columns[2 - 1].AllowAutoSort = true;
            grdClaves.Sheets[0].Columns[3 - 1].AllowAutoSort = true;
            grdClaves.Sheets[0].Columns[4 - 1].AllowAutoSort = true; 


            CargarListaReportes();
            CargarClavesRegistradas(); 
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

        private void CargarClavesRegistradas()
        {
            string sSql = string.Format("Select Distinct IdClaveSSA_Sal as IdClaveSSA, 0 as Procesar, " + 
                " ClaveSSA, DescripcionSal as DescripcionClave " + 
	            " From vw_ExistenciaPorSales (NoLock) " + 
            " Where IdEmpresa <> '' Order by DescripcionSal ");

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(leerWeb, "CargarClavesRegistradas()");
                General.msjError("Ocurrió un error al cargar la lista de Claves registradas."); 
            }
            else
            {
                // Leer el contenido aunque venga vacio 
                dtsClaves = leerWeb.DataSetClase; 
            }
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            ////cboEstados.Clear();
            ////cboEstados.Add();
            ////cboFarmacias.Clear();
            ////cboFarmacias.Add();

            btnNuevo_Click(null, null);            
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
            if( cboFarmacias.NumeroDeItems == 0 )
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                ////////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                ////////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                ////////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, General.EntidadConectada);

                ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
                ////{
                ////    sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                ////                    " From vw_Farmacias_Urls U (NoLock) " +
                ////                    " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                ////                    " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                ////                    " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                ////                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);
                ////}

                ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                ////{
                ////    sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                ////                    " From vw_Farmacias_Urls U (NoLock) " +
                ////                    " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                ////                    " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                ////                    " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                ////                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);
                ////}

                //Grid.Limpiar(false);

                if (!leerWeb.Exec(sSqlFarmacias))
                {
                    Error.GrabarError(leerWeb, "CargarFarmacias()");
                    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                }
                else
                {
                    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                    //Grid.LlenarGrid(leer.DataSetClase);
                    //grdReporte.Focus();
                }
            }
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
                string sFarmacia = txtFarmacia.Text;
                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;

                if (rdoRecVentas.Checked)
                {
                    if (rdoClaves.Checked)
                    {
                        myRpt.NombreReporte = "Rpt_Cte_Salidas_ClavesSSAMensuales";
                    }
                    if (rdoAnioClaves.Checked)
                    {
                        myRpt.NombreReporte = "Rpt_Cte_Salidas_ClavesSSAMensual_Anual";
                    }
                }
                else
                {
                    if (rdoClaves.Checked)
                    {
                        myRpt.NombreReporte = "Rpt_Cte_ValesSalidas_ClavesSSAMensuales";
                    }
                    if (rdoAnioClaves.Checked)
                    {
                        myRpt.NombreReporte = "Rpt_Cte_ValesSalidas_ClavesSSAMensual_Anual";
                    }
                }
                

                //if (General.ImpresionViaWeb)
                {
                    ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    ////DataSet datosC = DatosCliente.DatosCliente();

                    //////conexionWeb.Url = General.Url;
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
            // rdoTE.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 

            // FrameTransferencias.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;


            rdoTpDispAmbos.Checked = true;
            rdoClaves.Checked = true; 

            // Cargar Estados y Farmacias 
            CargarEstados();

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            {
                cboFarmacias.SelectedIndex = 0;
                txtFarmacia.Focus();
            }
            else // if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                //cboFarmacias.SelectedIndex = 0;
                cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
                cboFarmacias.Enabled = false;
                txtFarmacia.Text = DtGeneralPedidos.FarmaciaConectada;
                lblFarmacia.Text = DtGeneralPedidos.FarmaciaConectadaNombre;
                txtFarmacia.Enabled = false;
            }


            Grid.Limpiar(); 
            Grid.LlenarGrid(dtsClaves);

            dtsClavesProcesar = PreparaDtsClaves();
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

            // FrameTransferencias.Enabled = false;
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

            //////try
            //////{
            //////    //leerWeb = new clsLeerWeb(sUrl, "SII_Unidad", DatosCliente); 
            //////    leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

            //////    conexionWeb.Url = sUrl;
            //////    //DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx("SII_Unidad"));
            //////    DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

            //////    DatosDeConexion.Servidor = sHost;
            //////    bRegresa = true; 
            //////}
            //////catch (Exception ex1)
            //////{
            //////    Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
            //////    General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
            //////    ActivarControles(); 
            //////}

            return bRegresa; 
        }

        private void ObtenerInformacion()
        {           
            bEjecutando = true;
            string sStore = "", sStoreVales = "";
            string sTabla = ""; 
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";
            // bEjecutando = true; 
            int iTipoDispensacion = 0, iTipoInsumo = 0;

            sTablaClavesSSA = "CTE_ClavesAProcesar";

            sStore = " spp_Rpt_VentasPorClaveMensual ";
            sStoreVales = " spp_Rpt_Vales_VentasPorClaveMensual ";
            sTabla = " Rpt_DispensacionVentasPorClaveMensual ";
            

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispVenta.Checked)
                iTipoDispensacion = 1;

            if (rdoTpDispConsignacion.Checked)
                iTipoDispensacion = 2;

            if (rdoTpDispAmbos.Checked)
                iTipoDispensacion = 0;

            // Determinar que tipo de producto se mostrara 
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
                " Exec  {0} '{1}', '*', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' " +
                " Exec  {8} '{1}',      '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                sStore, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "", dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoDispensacion, iTipoInsumo, sStoreVales); 
            sSql += "\n " + string.Format( "Select top 1 * From {0} (NoLock) ", sTabla );

            if (ClavesAProcesar())
            {
                try
                {
                    leer.Reset();
                    conexionWeb.Timeout = 300000;
                    leer.DataSetClase = conexionWeb.SentenciaClaves(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, sTablaClavesSSA, dtsClavesProcesar); 
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
                    try
                    {
                        leer.Reset();
                        conexionWeb.Timeout = 500000;
                        //leer.DataSetClase = conexionWeb.SentenciaClaves(cboEstados.Data, cboFarmacias.Data, sSql, sTablaClavesSSA, dtsClavesProcesar);
                        //leer.DataTableClase = conexionWeb.EjecutarSentencia(cboEstados.Data, cboFarmacias.Data, sSql, sTablaClavesSSA);
                        leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia); 
                    }
                    catch { }

                    if (leer.SeEncontraronErrores())
                    {
                        btnImprimir.Enabled = false;
                        bSeEncontroInformacion = false;
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
                }

                bEjecutando = false;

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosReg(sCadena, General.Url);

            }
            ////if (validarDatosDeConexion())            
            ////{
            ////    cnnUnidad = new clsConexionSQL(DatosDeConexion);
            ////    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
            ////    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

            ////    leer = new clsLeer(ref cnnUnidad);

            ////    if (ClavesAProcesar())
            ////    {

            ////        if (!leer.Exec(sSql))
            ////        {
            ////            Error.GrabarError(leer, "ObtenerInformacion()");
            ////            General.msjError("Ocurrió un error al obtener la información del reporte.");
            ////        }
            ////        else
            ////        {
            ////            if (leer.Leer())
            ////            {
            ////                btnImprimir.Enabled = true;
            ////                bSeEncontroInformacion = true;
            ////                //ObtenerRutaReportes();
            ////            }
            ////            else
            ////            {
            ////                bSeEncontroInformacion = false;
            ////            }

            ////            sCadena = sSql.Replace("'", "\"");
            ////            auditoria.GuardarAud_MovtosUni("*", sCadena);
            ////        }
            ////    }

            ////    bSeEjecuto = true; 
            ////    bEjecutando = false;  
            ////}
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid                

        #region Eventos

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

                    if (bSeEjecuto)
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

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            Grid.SetValue(2, chkTodos.Checked);
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                ////////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                ////////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                //// cboFarmacias.Enabled = false;
            }
        }

        private void FrmReportesSalidasUnidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        #endregion Eventos

        #region Funciones

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            // FrameTransferencias.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
        }

        private bool ClavesAProcesar()
        {
            bool bReturn = true;
            string sEdo = "", sFar = "", sIdClaveSSA = "", sClaveSSA = "";

            sEdo = cboEstados.Data;
            //sFar = cboFarmacias.Data;
            sFar = txtFarmacia.Text;

            dtsClavesProcesar = null;
            dtsClavesProcesar = PreparaDtsClaves();
            
            for (int i = 1; i <= Grid.Rows; i++)
            {
                if (Grid.GetValueBool(i, 2))
                {
                    sIdClaveSSA = Grid.GetValue(i, 1);
                    sClaveSSA = Grid.GetValue(i, 3);

                    object[] x = { sEdo, sFar, sIdClaveSSA, sClaveSSA };

                    dtsClavesProcesar.Tables[0].Rows.Add(x);
                    ////string sQuery = string.Format(" Insert Into CTE_ClavesAProcesar " +
                    ////                     " Select '{0}', '{1}', '{2}', '{3}', 'A',0 ", sEdo, sFar, sIdClaveSSA, sClaveSSA);
                    ////if (!leer.Exec(sQuery))
                    ////{
                    ////    Error.GrabarError(leer, "ClavesAProcesar()");
                    ////    General.msjError("Ocurrió un error al Insertar Claves a Procesar.");
                    ////    bReturn = false;
                    ////    break;
                    ////}
                    ////else
                    ////{
                    ////    bReturn = true;
                    ////}
                }
            }

            if (dtsClavesProcesar == null)
            {
                bReturn = false;
            }

            return bReturn;
        }

        private void CargarSubFarmacias()
        {
            SubFarmacias = new FrmListaDeSubFarmacias();
            SubFarmacias.AliasTabla = "L.";
            SubFarmacias.Estado = DtGeneralPedidos.EstadoConectado;
            SubFarmacias.Farmacia = Fg.PonCeros(txtFarmacia.Text, 4);
            SubFarmacias.EsParaSP = true;
            SubFarmacias.MostrarDetalle();
            sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
        }

        public static DataSet PreparaDtsClaves()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("ClavesSSA");

            dtClave.Columns.Add("IdEstado", Type.GetType("System.String"));
            dtClave.Columns.Add("IdFarmacia", Type.GetType("System.String"));
            dtClave.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtClave.Columns.Add("ClaveSSA", Type.GetType("System.String"));
            dts.Tables.Add(dtClave);
            
            return dts.Clone();
        }

        #endregion Funciones

        #region Eventos_Farmacia
        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias_UrlsActivas(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }

        private void txtFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Farmacias_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data);

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }
        #endregion Eventos_Farmacia
    } 
}
