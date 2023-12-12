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

//using DllPedidosClientes.Clases;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmReportesUnitarios : FrmBaseExt
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

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        // string sSqlFarmacias = "";
        string sUrl= "";
        string sHost = "";
        // string sUrl_RutaReportes = "";
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

        public FrmReportesUnitarios()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            // leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente); 
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente); 


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn); 
            leerLocal = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);
            // Ayuda = new clsAyudas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            Ayuda = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);
  
            // Clase de Movimientos de Auditoria 
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            //Grid = new clsGrid(ref grdReporte, this);
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;
            CargarListaReportes();
        }
        
        private void FrmReportesUnitarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            //CargarEstados();
        }

        #region Reportes
        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 

            ////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            ////{
            ////    cboReporte.Add("CteReg_Admon_ConcentradoInsumos_Secretaria.rpt", "Concentrado de Dispensación");
            ////    cboReporte.Add("CteReg_Admon_ConcentradoInsumosDesglozado_Secretaria.rpt", "Concentrado de Dispensación desglozado");
            ////    cboReporte.Add("CteReg_Admon_ConcentradoInsumosPrograma_Secretaria.rpt", "Concentrado de Dispensación Por Programa");
            ////    cboReporte.Add("CteReg_Admon_ConcentradoInsumosProgramaTotalizado_Secretaria.rpt", "Concentrado de Dispensación Por Programa Totalizado");

            ////    cboReporte.Add("CteReg_Admon_Validacion_Secretaria.rpt", "Detallado de Dispensación (Validación)");

            ////    cboReporte.Add("CteReg_Admon_Diagnosticos.rpt", "Incidencias Epidemiologicas concentrado");
            ////    cboReporte.Add("CteReg_Admon_DiagnosticosDetallado.rpt", "Incidencias Epidemiologicas detallado");

            ////    cboReporte.Add("CteReg_MedicosDetallado_Secretaria.rpt", "Detallado Por Medico");
            ////    cboReporte.Add("CteReg_CostoPorMedico_Secretaria.rpt", "Costo Por Medico");

            ////    cboReporte.Add("CteReg_PacientesDet_Secretaria.rpt", "Detallado Por Paciente");
            ////    cboReporte.Add("CteReg_CostoPorPaciente_Secretaria.rpt", "Costo Por Paciente");

            ////    cboReporte.Add("CteReg_ServiciosAreas_Secretaria.rpt", "Detallado Por Servicios y Areas");
            ////}
            ////else
            {
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
            }

            cboReporte.SelectedIndex = 0;
        }
        #endregion Reportes

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
                ////    // Error.GrabarError(leerWeb, "CargarEstados()");
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

                ////////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                ////////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                ////////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);            
                ////////Grid.Limpiar(false);

                //////sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                //////                " From vw_Farmacias_Urls U (NoLock) " +
                //////                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                //////                " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                //////                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                //////                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                //////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                //////{
                //////    sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                //////                    " From vw_Farmacias_Urls U (NoLock) " +
                //////                    " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                //////                    " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                //////                    " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                //////                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);
                //////}

                //////if (!leerWeb.Exec(sSqlFarmacias))
                //////{
                //////    // Error.GrabarError(leerWeb, "CargarFarmacias()");
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
            if (txtCliente.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtCliente.Text.Trim(), "", "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjAviso("Clave de Cliente no encontrado, verifique.");
                    txtCliente.Text = "";
                    lblCliente.Text = "";
                    txtCliente.Focus();
                }
            }

        }

        private void CargarDatosCliente()
        {
            txtCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("NombreCliente");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            }

        }
        #endregion Buscar Cliente

        #region Buscar SubCliente 
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCliente.Text != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjAviso("Clave de SubCliente no encontrado, verifique.");
                    txtSubCliente.Text = "";
                    lblSubCliente.Text = "";
                    txtSubCliente.Focus();
                }
            }

        }

        private void CargarDatosSubCliente()
        {
            txtSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("NombreSubCliente");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtCliente.Text.Trim(), "txtSubCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }
        #endregion Buscar SubCliente

        #region Buscar Beneficiario 
        private void txtBeneficiario_Validating(object sender, CancelEventArgs e)
        {
            string sIdEstado = cboEstados.Data, sIdFarmacia = Fg.PonCeros(txtFarmacia.Text, 4), sIdCliente = txtCliente.Text, sIdSubCliente = txtSubCliente.Text;

            if (txtBeneficiario.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Beneficiarios(sIdEstado, sIdFarmacia, sIdCliente, sIdSubCliente, txtBeneficiario.Text, "txtBeneficiario_Validating");
                if (leer.Leer())
                {
                    CargarDatosBeneficiario();
                }
                else
                {
                    lblBeneficiario.Text = "";
                    txtBeneficiario.Text = "";
                    General.msjAviso("Clave de Beneficiario no encontrado, verifique.");
                    txtBeneficiario.Focus();                    
                }
            }
        }
        private void CargarDatosBeneficiario()
        {
            txtBeneficiario.Text = leer.Campo("IdBeneficiario");
            lblBeneficiario.Text = leer.Campo("NombreCompleto");
        }
        #endregion Buscar Beneficiario 

        #region Buscar Medico
        private void txtMedico_Validating(object sender, CancelEventArgs e)
        {
            if (txtMedico.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Medicos(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtMedico.Text, "txtIdMedico_Validating");
                if (leer.Leer())
                {
                    CargarDatosMedico();
                }
                else
                {
                    txtMedico.Text = "";
                    lblMedico.Text = "";
                    General.msjAviso("Clave de Medico no encontrado, verifique.");
                    txtMedico.Focus();
                }
            }
        }

        private void CargarDatosMedico()
        {
            txtMedico.Text = leer.Campo("IdMedico");
            lblMedico.Text = leer.Campo("NombreCompleto");
            lblIdEspecialidad.Text = leer.Campo("IdEspecialidad");
            lblEspecialidad.Text = leer.Campo("Especialidad");
            lblCedula.Text = leer.Campo("NumCedula");
        }
        #endregion Buscar Medico

        #region Buscar Clave
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    CargarDatosClave();
                }
                else
                {
                    txtClaveSSA.Text = "";
                    lblClave.Text = "";
                    General.msjAviso("ClaveSSA no encontrada, verifique.");
                    txtClaveSSA.Focus();
                }
            }
        }

        private void CargarDatosClave()
        {
            txtClaveSSA.Text = leer.Campo("ClaveSSA");
            lblClave.Text = leer.Campo("DescripcionSal");
        }

        #endregion Buscar Clave

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas("001", cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtFolio.Text.Trim(), "txtIdMedico_Validating");
                if (leer.Leer())
                {
                    txtFolio.Text = leer.Campo("Folio");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
                    lblPersonal.Text = leer.Campo("IdPersonal");
                    lblNombrePersonal.Text = leer.Campo("NombrePersonal");
                }
                else
                {
                    txtFolio.Text = "";
                    General.msjAviso("Folio de Venta no encontrado, verifique.");
                    txtFolio.Focus();
                }
            }
        }
        #endregion Buscar Folio

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
                string sEstado = cboEstados.Data, sFarmacia = Fg.PonCeros(txtFarmacia.Text, 4);

                // El reporte se localiza fisicamente en el Servidor Regional ó Central.              
                DatosCliente.Funcion = "Imprimir()";              
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad_Directo)
                {
                    myRpt = new clsImprimir(DatosDeConexion);
                }

                // byte[] btReporte = null;

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;               
                myRpt.NombreReporte = cboReporte.Data + ""; 

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

                //else
                //{
                    //// Lineas para pruebas locales ///////
                    //myRpt.CargarReporte(true);
                    //bRegresa = !myRpt.ErrorAlGenerar;
                    ////////////////////////////////////////
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    auditoria.GuardarAud_MovtosReg(myRpt.NombreReporte, General.Url);
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
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 

            FrameBeneficiario.Enabled = bValor;
            FrameMedico.Enabled = bValor;
            FrameClave.Enabled = bValor;
            FrameFolio.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            FrameListaReportes.Enabled = bValor;
            tabReportes.Enabled = true;

            txtCliente.Text = "";
            txtSubCliente.Text = "";
            txtBeneficiario.Text = "";
            txtMedico.Text = "";
            txtClaveSSA.Text = "";
            txtFolio.Text = "";

            //pagBeneficiario.SelectedTab(0);

            CargarEstados();
            cboEstados.Focus();

            if (sUrl != "")
            {
                // BorrarTablas();
            }

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
                cboFarmacias.Enabled = false;
                txtFarmacia.Text = DtGeneralPedidos.FarmaciaConectada;
                lblFarmacia.Text = DtGeneralPedidos.FarmaciaConectadaNombre;
                txtFarmacia.Enabled = false;
            }
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

            FrameBeneficiario.Enabled = false;
            FrameMedico.Enabled = false;
            FrameClave.Enabled = false;
            FrameFolio.Enabled = false;
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
            lblCliente.Text = "";
            txtSubCliente.Text = "";
            lblSubCliente.Text = "";
            txtBeneficiario.Text = "";
            lblBeneficiario.Text = "";
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
            txtBeneficiario.Text = "";
            lblBeneficiario.Text = "";
        }

        private void txtMedico_TextChanged(object sender, EventArgs e)
        {
            lblMedico.Text = "";
            lblIdEspecialidad.Text = "";
            lblEspecialidad.Text = "";
            lblCedula.Text = "";
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClave.Text = "";
        }

        private void txtBeneficiario_TextChanged(object sender, EventArgs e)
        {
            lblBeneficiario.Text = "";
        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            lblPersonal.Text = "";
            lblNombrePersonal.Text = "";
        }

        #endregion Eventos

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
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
            int iTipoInsumo = 0, iTipoDispensacion = 0, iTipoInsumoMedicamento = 0, iMostrarPrecio = 0;
            // string sCadena = "", 
            string sPrograma = "", sSubPrograma = "";

            //Las siguientes variables obtienen el valor de los textbox debido a que estos se limpian al cambiar de pestaña.
            string sBeneficiario = txtBeneficiario.Text.Trim(), sMedico = txtMedico.Text.Trim(), sClave = txtClaveSSA.Text.Trim(), sFolio = txtFolio.Text.Trim();

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            {
                iMostrarPrecio = 1; 
            }

            sTablaFarmacia = "CTE_FarmaciasProcesar";
            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos_Unidad '{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
                " '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}'  ",
               cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), txtCliente.Text, txtSubCliente.Text, sPrograma, sSubPrograma,
               iTipoDispensacion, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoInsumo, iTipoInsumoMedicamento, iMostrarPrecio,
               sBeneficiario, sMedico, sClave, sFolio);
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion_Unidad (NoLock) ");


            //////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Regional)
            //////{
            //////    iMostrarPrecio = 1; 
            //////    sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos_Secretaria '{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
            //////                    " '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}'  ",
            //////                   cboEstados.Data, txtCliente.Text, txtSubCliente.Text, sPrograma, sSubPrograma,
            //////                   iTipoDispensacion, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoInsumo, iTipoInsumoMedicamento, iMostrarPrecio,
            //////                   sBeneficiario, sMedico, sClave, sFolio);
            //////    sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion_Secretaria (NoLock) ");
            //////} 


            //////try
            //////{
            //////    leer.Reset();
            //////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia);
            //////}
            //////catch { }

            leer.Reset();
            leer.DataSetClase = GetInformacion(sSql);
            if (leer.SeEncontraronErrores())
            {
                btnImprimir.Enabled = false;
                bSeEncontroInformacion = false;
                tabReportes.Enabled = true;
                //btnNuevo.Enabled = true;
                //General.msjError("Ocurrió un error al obtener la información del reporte");

                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true;
                    bSeEncontroInformacion = true;
                    tabReportes.Enabled = false;
                }
                else
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    bSeEjecuto = true;
                    tabReportes.Enabled = true;
                }
            }
            bEjecutando = false;

            //if (validarDatosDeConexion())
            //{
            //    cnnUnidad = new clsConexionSQL(DatosDeConexion);
            //    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
            //    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

            //    leer = new clsLeer(ref cnnUnidad);

            //    if (FarmaciasAProcesar())
            //    {
            //        if (!leer.Exec(sSql))
            //        {
            //            Error.GrabarError(leer, "ObtenerInformacion()");
            //            General.msjError("Ocurrió un error al obtener la información del reporte.");
            //        }
            //        else
            //        {
            //            if (leer.Leer())
            //            {
            //                btnImprimir.Enabled = true;
            //                bSeEncontroInformacion = true;
            //                tabReportes.Enabled = false;
            //                ObtenerRutaReportes();
            //            }
            //            else
            //            {
            //                bSeEncontroInformacion = false;
            //                tabReportes.Enabled = true;
            //            }

            //            sCadena = sSql.Replace("'","\"");
            //            auditoria.GuardarAud_MovtosReg(sCadena, sUrl);
            //        }

            //        bSeEjecuto = true;
            //        bEjecutando = false;
            //    }
            //}
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        #region Funciones 
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            FrameBeneficiario.Enabled = true;
            //FrameInsumos.Enabled = true;
            //FrameDispensacion.Enabled = true;
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

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;
            string sEdo = "", sFar = "";
            sEdo = cboEstados.Data;

            string sSql = string.Format("Delete From CTE_FarmaciasProcesar ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "FarmaciasAProcesar()");
                General.msjError("Ocurrió un error al borrar tabla.");
                bReturn = false;
            }
            else
            {
                sFar = Fg.PonCeros(txtFarmacia.Text, 4);

                string sQuery = string.Format(" Insert Into CTE_FarmaciasProcesar " +
                                     " Select '{0}','{1}','A',0 ", sEdo, sFar);
                if (!leer.Exec(sQuery))
                {
                    Error.GrabarError(leer, "FarmaciasAProcesar()");
                    General.msjError("Ocurrió un error al Insertar la Farmacia a Procesar.");
                    bReturn = false;
                    //break;
                }
                else
                {
                    bReturn = true;
                }
            }

            return bReturn;
        }

        //private void chkTodos_CheckedChanged(object sender, EventArgs e)
        //{
        //    Grid.SetValue(3, chkTodos.Checked);
        //}

        private void FrmReportesUnitarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                //// Habilitar 
                // BorrarTablas(); 
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                //////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                //////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                ////////// cboFarmacias.Enabled = false;
            }
        }

        private void BorrarTablas()
        {
            string sSql = string.Format("Exec spp_Mtto_BorrarTablasRpt_Clientes ");

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(leerWeb, "BorrarTablas()");
                General.msjError("Ocurrió un error al borrar las tablas.");
            }
            else
            {

            }
        }

        private void LevantaForma()
        {
            FrmListadoBeneficiarios f = new FrmListadoBeneficiarios(DatosDeConexion);
            f.MostrarListaBeneficiarios(DatosDeConexion);
        }

        private void listadoDeBeneficiariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevantaForma();
        }

        #endregion Funciones

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

        #region TabControl
        private void tabReportes_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (pagBeneficiario.Focus())
            {
                Fg.IniciaControles(this, true, FrameMedico);
                Fg.IniciaControles(this, true, FrameClave);
                Fg.IniciaControles(this, true, FrameFolio);
                dtpFechaRegistro.Enabled = false;
            }
            else if( pagMedico.Focus())
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                Fg.IniciaControles(this, true, FrameClave);
                Fg.IniciaControles(this, true, FrameFolio);
                dtpFechaRegistro.Enabled = false;
            }
            else if (pagClave.Focus())
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                Fg.IniciaControles(this, true, FrameMedico);
                Fg.IniciaControles(this, true, FrameFolio);
                dtpFechaRegistro.Enabled = false;
            }
            else if (pagFolio.Focus())
            {
                Fg.IniciaControles(this, true, FrameBeneficiario);
                Fg.IniciaControles(this, true, FrameMedico);
                Fg.IniciaControles(this, true, FrameClave);
                dtpFechaRegistro.Enabled = false;
            }
        }
        
        #endregion TabControl

        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias_UrlsActivas(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");

                    sUrl = leer.Campo("Url");
                    sHost = leer.Campo("Servidor");

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
                leer.DataSetClase = Ayuda.Farmacias_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data);

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

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            switch (DtGeneralPedidos.TipoDeConexion)
            {
                case TipoDeConexion.Regional:
                    dts = GetInformacionRegional(Cadena);
                    break;

                case TipoDeConexion.Unidad:
                    dts = GetInformacionUnidad(Cadena);
                    break;

                case TipoDeConexion.Unidad_Directo:
                    dts = GetInformacionUnidad_Directo(Cadena);
                    break;

                default:
                    break;
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                conexionWeb.Url = General.Url;
                dts = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionUnidad_Directo(string Cadena)
        {
            DataSet dts = new DataSet();

            if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);

                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }
        #endregion Conexiones
    } 
}
