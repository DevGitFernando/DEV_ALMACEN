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
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmReportesFacturacion : FrmBaseExt 
    {
        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid Grid;
        FrmListaDeSubFarmacias SubFarmacias;

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        string sIdPublicoGeneral =  "0001";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false; 

        public FrmReportesFacturacion()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Grid = new clsGrid(ref grdReporte, this);

            CargarTitulosEncabezadoReportes(""); 
            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 

            //cboReporte.Add("PtoVta_Admon_CostoPorUnidad.rpt", "Costo por Unidad");
            //cboReporte.Add("PtoVta_Admon_CostoPorUnidadDetalle.rpt", "Detalle Costo por Unidad");

            //// Modificacion requerida por la operacion de Guanajuato 
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumos_Agrupado.rpt", "Concentrado de Dispensación Agrupado");

            cboReporte.Add("PtoVta_Admon_ConcentradoInsumos", "Concentrado de Dispensación"); 
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosDesglozado", "Concentrado de Dispensación desglozado");
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosPrograma", "Concentrado de Dispensación Por Programa");
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosProgramaTotalizado", "Concentrado de Dispensación Por Programa Totalizado");  

            cboReporte.Add("PtoVta_Admon_Validacion", "Detallado de Dispensación (Validación)", "0");
            cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación no licitado)", "1");
            cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación licitado)", "2");

            //////// Jesús Díaz 2K120919.1820 
            ////cboReporte.Add("PtoVta_Admon_Validacion_NoSurtido", "Detallado de Dispensación (No surtido)");
            ////cboReporte.Add("PtoVta_Admon_Validacion_Documentos", "Detallado de Dispensación (Documentos de canje)"); 

            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecio", "Claves SSA sin precio asignado");
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecioDetallado", "Claves SSA sin precio asignado detallado"); 

            //cboReporte.Add("PtoVta_Admon_CostoPorReceta.rpt", "Costo por Receta");

            //cboReporte.Add("PtoVta_Admon_CostoPorPaciente.rpt", "Costo por Paciente");
            //cboReporte.Add("PtoVta_Admon_PacienteDetalle.rpt", "Detallado por Paciente");

            //cboReporte.Add("PtoVta_Admon_CostoPorMedico.rpt", "Costo por Médico");
            //cboReporte.Add("PtoVta_Admon_MedicoDetalle.rpt", "Detallado por Médico");

            //cboReporte.Add("PtoVta_Admon_Diagnosticos.rpt", "Incidencias Epidemiologicas"); 

            cboReporte.SelectedIndex = 0; 
        }

        private void CargarTitulosEncabezadoReportes(string IdEstado)
        {
            string sSql =
                string.Format("Select TituloEncabezadoReporte as Titulo, (IdTitulo + ' - ' + TituloEncabezadoReporte) as Descripcion " +
                " From CFG_EX_Validacion_Titulos_Reportes (NoLock) Where IdEstado = '{0}' and Status = 'A' " +
                " Order By IdTitulo ",
                IdEstado); 

            cboTitulosReporte.Clear();
            cboTitulosReporte.Add("", "<< Default >>");

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al cargar la lista de titulos de reportes de validación.");
            }
            else
            {
                cboTitulosReporte.Add(leer.DataSetClase, true, "Titulo", "Descripcion");
            }

            cboTitulosReporte.SelectedIndex = 0;
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos 
        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select distinct IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and ( U.IdFarmacia <> '{2}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
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
            if (txtCte.Text.Trim() != "")
            {
                //leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCte_Validating");
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), "", "txtCte_Validating");
                if (leer.Leer())
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
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, "txtCte_KeyDown");
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
            if (txtSubCte.Text != "")
            {
                //leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(),"txtSubCte_Validating");
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtCte_Validating");
                if (leer.Leer())
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
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //leer.DataSetClase = Ayuda.SubClientes_Buscar("txtCte_KeyDown", txtCte.Text.Trim());
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), "txtSubCte_KeyDown");
                if (leer.Leer())
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
                    //leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
                    leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_Validating");
                    if (leer.Leer())
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
            txtPro.Text = leer.Campo("IdPrograma");
            lblPro.Text = leer.Campo("Programa");
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtPro_KeyDown");
                if (leer.Leer())
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
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(), "txtSubPro_Validating");
                if (leer.Leer())
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
            txtSubPro.Text = leer.Campo("IdSubPrograma");
            lblSubPro.Text = leer.Campo("SubPrograma");
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, cboEstados.Data, cboFarmacias.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
            }
        }
        #endregion Buscar SubPrograma

        #region Impresion  
        private void ObtenerRutaReportes()
        {
            //string sSql = string.Format(" Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
            //    " From Net_CFGC_Parametros (NoLock) " +
            //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' ", 
            //    cboEstados.Data, cboFarmacias.Data, "RutaReportes");
            //if (!leerWeb.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "ObtenerRutaReportes");
            //    General.msjError("Ocurrió un error al obtener la Ruta de Reportes de la Farmacia."); 
            //}
            //else
            //{
            //    leerWeb.Leer();
            //    sUrl_RutaReportes = leerWeb.Campo("Valor");     
            //}
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
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            bool bRegresa = false;
            int iMostrarSubFarmacias = chkMostrarSubFarmacias.Checked ? 1 : 0; 

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central. 
                // Se utilizan los datos de Conexión de la farmacia seleccionada. 

                DatosCliente.Funcion = "Imprimir()"; 
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                ////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = cboReporte.Data + "";
                myRpt.TituloReporte = cboReporte.Text; 

                string sValor = "";
                try
                {
                    sValor = (string)cboReporte.ItemActual.Item;
                }
                catch { }

                if (sValor == "0" || sValor == "1" || sValor == "2")
                {
                    myRpt.Add("MostrarSubFarmacias", iMostrarSubFarmacias);
                    myRpt.Add("MostrarPaquetes", chkMostrarPaquetes.Checked ? 1 : 0);
                    myRpt.Add("TitutoEncabezadoReportes", cboTitulosReporte.Data); 

                    if (sValor == "1" || sValor == "2")
                    {
                        myRpt.Add("IdGrupoPrecios", Convert.ToInt32(sValor));
                        myRpt.Add("IdPerfilAtencion", 0);
                        myRpt.Add("IdSubPerfilAtencion", 0);
                    }
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

                //////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    conexionWeb.Url = General.Url;
                ////    conexionWeb.Timeout = 300000;
                ////    //////myRpt.CargarReporte(true); 

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            cboEstados.Enabled = false;
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0; 


            // Grid.Limpiar(); 
            Fg.IniciaControles(this, true); 
            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = ""; 

            FrameCliente.Enabled = bValor;
            FrameInsumos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            FrameListaReportes.Enabled = bValor;

            cboTitulosReporte.Enabled = false;
            cboTitulosReporte.SelectedIndex = 0; 

            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false; 
            }

            txtCte.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false; 
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false; 
            cboFarmacias.Enabled = false; 

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
            // LlenarGrid();
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
            Grid.Limpiar();
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            Grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            Grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
            Grid.Limpiar();
        }

        #endregion Eventos

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

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

            // bEjecutando = true; 
            int iTipoInsumo = 0, iTipoDispensacion = 0, iTipoInsumoMedicamento = 0;
            int iMostrarPrecios = chkMostrarPrecios.Checked ? 1 : 0;


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

            /* 
            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos '{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, txtCte.Text, txtSubCte.Text, txtPro.Text,
                txtSubPro.Text, iTipoDispensacion, dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoInsumo, iTipoInsumoMedicamento, sSubFarmacias);
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion (NoLock) ");
            */ 

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', " +
                " @IdPrograma = '{5}', @IdSubPrograma = '{6}', @TipoDispensacion = '{7}', " +
                " @FechaInicial = '{8}', @FechaFinal = '{9}', @TipoInsumo = '{10}', @TipoInsumoMedicamento = '{11}', @SubFarmacias = '{12}', " +
                " @MostrarPrecios = '{13}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text, txtSubCte.Text, 
                txtPro.Text, txtSubPro.Text, iTipoDispensacion,
                dtpFechaInicial.Text, dtpFechaFinal.Text, iTipoInsumo, iTipoInsumoMedicamento, sSubFarmacias, iMostrarPrecios);
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion (NoLock) ");  

            ////string sSql = string.Format("Select top 1 * From tmpRptAdmonDispensacion (NoLock) ");


            Grid.Limpiar(false);
            // leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leer = new clsLeer(ref cnnUnidad); 
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leerLocal.DatosConexion, leer.Error, "ObtenerInformacion()");
                    General.msjError("Ocurrió un error al obtener la información del reporte.");
                }
                else
                {
                    //ObtenerInformacionExcel();
                    if (leer.Leer())
                    {
                        // Grid.LlenarGrid(leer.DataSetClase); 
                        btnImprimir.Enabled = true;
                        bSeEncontroInformacion = true;
                        ObtenerRutaReportes();
                        btnExportarExcel.Enabled = true;
                    }
                    else
                    {
                        bSeEncontroInformacion = false;
                        btnExportarExcel.Enabled = false;
                        // General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }

                bSeEjecuto = true; 
                bEjecutando = false; // Cursor.Current  
            }
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false; 
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
                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true; 

                if (!bSeEncontroInformacion) 
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

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                CargarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true;
                CargarTitulosEncabezadoReportes(cboEstados.Data); 
                CargarFarmacias();
            } 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString(); 
                // cboFarmacias.Enabled = false;
            }
        }

        private void FrmReportesFacturacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void CargarSubFarmacias()
        {
            SubFarmacias = new FrmListaDeSubFarmacias(cboEstados.Data, cboFarmacias.Data);
            SubFarmacias.AliasTabla = "L.";
            SubFarmacias.Estado = cboEstados.Data;
            SubFarmacias.Farmacia = cboFarmacias.Data; 
            SubFarmacias.EsParaSP = true;
            SubFarmacias.MostrarDetalle();
            sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
        }

        //private void ObtenerInformacionExcel()
        //{
        //    string sSql = string.Format("Select *, Convert(varchar(16), GetDate(), 120) as FechaImpresion From tmpRptAdmonDispensacion (NoLock) ");

        //    if (validarDatosDeConexion())
        //    {
        //        cnnUnidad = new clsConexionSQL(DatosDeConexion);
        //        cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
        //        cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

        //        leerExportarExcel = new clsLeer(ref cnnUnidad);
        //        if (!leer.Exec(sSql))
        //        {
        //            Error.GrabarError(leerLocal.DatosConexion, leerExportarExcel.Error, "ObtenerInformacionExcel()");
        //            General.msjError("Ocurrió un error al obtener la información del reporte.");
        //        }
        //        else
        //        {
        //            leerExportarExcel.DataSetClase = leer.DataSetClase;
        //        }

        //        bSeEjecuto = true;
        //        bEjecutando = false; // Cursor.Current  
        //    }
        //    this.Cursor = Cursors.Default;
        //} 

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //    int iRow = 2;
            //    string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + cboFarmacias.Data + ".xls";
            //    string sPeriodo = "";

            //    this.Cursor = Cursors.WaitCursor;
            //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            //    if (!bRegresa)
            //    {
            //        this.Cursor = Cursors.Default;
            //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //    }
            //    else
            //    {
            //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //        xpExcel.AgregarMarcaDeTiempo = false;

            //        if (xpExcel.PrepararPlantilla(sNombreFile))
            //        {
            //            xpExcel.GeneraExcel();

            //            //Se pone el encabezado
            //            leerExportarExcel.RegistroActual = 1;
            //            leerExportarExcel.Leer();
            //            xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
            //            iRow++;
            //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
            //            iRow++;

            //            sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //                General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
            //            xpExcel.Agregar(sPeriodo, iRow, 2);

            //            iRow = 6;
            //            xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

            //            // Se ponen los detalles
            //            leerExportarExcel.RegistroActual = 1;
            //            iRow = 9;
            //            while (leerExportarExcel.Leer())
            //            {
            //                xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
            //                xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
            //                xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
            //                xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
            //                xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
            //                xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
            //                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
            //                xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
            //                xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
            //                xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
            //                xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
            //                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
            //                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
            //                xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
            //                xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
            //                xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

            //                iRow++;
            //            }

            //            // Finalizar el Proceso 
            //            xpExcel.CerrarDocumento();

            //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //            {
            //                xpExcel.AbrirDocumentoGenerado();
            //            }
            //        }
            //    }
            //    this.Cursor = Cursors.Default;
        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sValor = "";

            chkMostrarPaquetes.Enabled = false;

            try
            {
                sValor = (string)cboReporte.ItemActual.Item;
            }
            catch { }

            if (sValor == "0" || sValor == "1" || sValor == "2")
            {
                chkMostrarPaquetes.Enabled = true;
            }

            cboTitulosReporte.Enabled = chkMostrarPaquetes.Enabled;
            if (!cboTitulosReporte.Enabled) cboTitulosReporte.SelectedIndex = 0; 
        }
    } 
}
