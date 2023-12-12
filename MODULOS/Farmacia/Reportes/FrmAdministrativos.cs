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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.ExportarExcel;

namespace Farmacia.Reportes 
{
    public partial class FrmAdministrativos : FrmBaseExt 
    {
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid Grid;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;
        string sSubFarmacias = "";

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        string sMostrar_Precios_Validacion = "MOSTRAR_PRECIOS_REPORTES_VALIDACION";

        public FrmAdministrativos()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            leerExportarExcel = new clsLeer(ref cnn);

            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Grid = new clsGrid(ref grdReporte, this);


            CargarTitulosEncabezadoReportes(); 
            CargarListaReportes();
            
        }

        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 

            cboReporte.Add("PtoVta_Admon_CostoPorUnidad.rpt", "Costo por Unidad");
            cboReporte.Add("PtoVta_Admon_CostoPorUnidadDetalle.rpt", "Detalle Costo por Unidad");

            //// Modificacion requerida por la operacion de Guanajuato 
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumos_Agrupado.rpt", "Concentrado de Dispensación Agrupado");


            cboReporte.Add("PtoVta_Admon_ConcentradoInsumos.rpt", "Concentrado de Dispensación");
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosDesglozado.rpt", "Concentrado de Dispensación desglozado");
            cboReporte.Add("PtoVta_Admon_ConcentradoInsumosPrograma.rpt", "Concentrado de Dispensación Por Programa");

            cboReporte.Add("PtoVta_Admon_ProductoDetalle.rpt", "Detallado de Dispensación por Producto");


            //cboReporte.Add("PtoVta_Admon_Validacion_Concentrado.rpt", "Detallado de Dispensación (Validación)", "0");
            cboReporte.Add("PtoVta_Admon_Validacion_Resumen.rpt", "Resúmen de Dispensación", "3-1");
            cboReporte.Add("PtoVta_Admon_Validacion_Resumen.rpt", "Resúmen de Dispensación ( Tipo de insumo )", "3-2");
            cboReporte.Add("PtoVta_Admon_Validacion.rpt", "Detallado de Dispensación (Validación)", "0");
            cboReporte.Add("PtoVta_Admon_Validacion_DerechoHabiencia.rpt", "Detallado de Dispensación (Validación por Derechohabiencia)", "0");
            cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación no licitado)", "1");
            cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación licitado)", "2");


            //////// Jesús Díaz 2K120919.1820 
            ////cboReporte.Add("PtoVta_Admon_Validacion_NoSurtido", "Detallado de Dispensación (No surtido)");
            ////cboReporte.Add("PtoVta_Admon_Validacion_Documentos", "Detallado de Dispensación (Documentos de canje)"); 

            cboReporte.Add("PtoVta_Admon_CostoPorReceta.rpt", "Costo por Receta");
            cboReporte.Add("PtoVta_Admon_CostoPorPaciente.rpt", "Costo por Paciente");
            cboReporte.Add("PtoVta_Admon_PacienteDetalle.rpt", "Detallado por Paciente");

            cboReporte.Add("PtoVta_Admon_CostoPorMedico.rpt", "Costo por Médico");
            cboReporte.Add("PtoVta_Admon_MedicoDetalle.rpt", "Detallado por Médico");

            cboReporte.Add("PtoVta_Admon_Diagnosticos.rpt", "Incidencias Epidemiologicas"); 

            cboReporte.SelectedIndex = 0;
        }

        private void CargarTitulosEncabezadoReportes()
        {
            string sSql =
                string.Format("Select TituloEncabezadoReporte as Titulo, (IdTitulo + ' - ' + TituloEncabezadoReporte) as Descripcion " +
                " From CFG_EX_Validacion_Titulos_Reportes (NoLock) Where IdEstado = '{0}' and Status = 'A' " +
                " Order By IdTitulo ", 
                DtGeneral.EstadoConectado); 

            cboTitulosReporte.Clear();
            cboTitulosReporte.Add("", "<< Default >>"); 

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al cargar la lista de titulos de reportes de validación.");
                Error.GrabarError(leer, "CargarTitulosEncabezadoReportes()"); 
            }
            else
            {
                cboTitulosReporte.Add(leer.DataSetClase, true, "Titulo", "Descripcion"); 
            }

            cboTitulosReporte.SelectedIndex = 0; 
        }

        #region FORM
        private void FrmAdministrativos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void FrmAdministrativos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void CargarSubFarmacias()
        {
            FrmListaDeSubFarmacias SubFarmacias = new FrmListaDeSubFarmacias(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada); 
            SubFarmacias.AliasTabla = "L.";
            SubFarmacias.Estado = DtGeneral.EstadoConectado;
            SubFarmacias.Farmacia = DtGeneral.FarmaciaConectada;
            SubFarmacias.EsParaSP = true;
            SubFarmacias.MostrarDetalle();
            sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
        }
        #endregion FORM 

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
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCte_Validating");
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
            lblCte.Text = leer.Campo("Nombre");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
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
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(),"txtSubCte_Validating");
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
            lblSubCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.SubClientes_Buscar("txtCte_KeyDown", txtCte.Text.Trim());
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
                    leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
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
            lblPro.Text = leer.Campo("Descripcion");
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
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
                leer.DataSetClase = Consultas.SubProgramas(txtPro.Text, txtSubPro.Text, "txtSubPro_Validating");
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
            lblSubPro.Text = leer.Campo("Descripcion");
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", txtPro.Text);
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
            }
        }
        #endregion Buscar SubPrograma

        #region Impresion 
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

            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = cboReporte.Data + "";
                myRpt.TituloReporte = cboReporte.Text; 

                string sValor = "";
                string sValor_Item = "";
                try
                {
                    sValor = (string)cboReporte.ItemActual.Item;
                }
                catch { }

                if ( sValor == "0" || sValor == "1" || sValor == "2" || sValor.Contains("3") ) // == "3")
                {
                    if( sValor == "0" || sValor == "1" || sValor == "2" )
                    {
                        myRpt.Add("MostrarSubFarmacias", 0);
                        myRpt.Add("MostrarPaquetes", chkMostrarPaquetes.Checked ? 1 : 0);
                        myRpt.Add("MostrarLotes", chkMostrarLotes.Checked ? 1 : 0);
                        myRpt.Add("TitutoEncabezadoReportes", cboTitulosReporte.Data);
                    }

                    if(sValor.Contains("3"))
                    {
                        myRpt.Add("TitutoEncabezadoReportes", cboTitulosReporte.Data);

                        if(sValor.Contains("-1"))
                        {
                            sValor_Item = "0"; 
                        }

                        if(sValor.Contains("-2"))
                        {
                            sValor_Item = "1";
                        }

                        myRpt.Add("SepararTipoDeInsumos", Convert.ToInt32(sValor_Item));
                    }

                    if ( sValor == "1" || sValor == "2" )
                    {
                            myRpt.Add("IdGrupoPrecios", Convert.ToInt32(sValor));
                            myRpt.Add("IdPerfilAtencion", 0);
                            myRpt.Add("IdSubPerfilAtencion", 0);
                        }
                    }
               
                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                ////////////if (General.ImpresionViaWeb)
                ////////////{
                ////////////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////////////    DataSet datosC = DatosCliente.DatosCliente();

                ////////////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////////////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////////////}
                ////////////else
                ////////////{
                ////////////    myRpt.CargarReporte(true);
                ////////////    bRegresa = !myRpt.ErrorAlGenerar;
                ////////////}

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
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
            // Grid.Limpiar(); 
            BloqueaControles(false);
            Fg.IniciaControles(this, true); 
            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;
            rdoOrigenDispensacion_01__General.Checked = true;
            rdoOrdenamiento_01__ClaveSSA.Checked = true; 

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false; 
            sSubFarmacias = ""; 

            FrameCliente.Enabled = bValor;
            FrameInsumos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            FrameListaReportes.Enabled = bValor;
            FrameOrigenDispensacion.Enabled = bValor;

            //////////////// REMOVER 20200126.0935 
            chkMostrarPrecios.Enabled = DtGeneral.EsAdministrador;
            chkMostrarPrecios.Visible = DtGeneral.EsAdministrador;
            chkMostrarPrecios.Checked = false;

            if(DtGeneral.EsAdministrador)
            {
                chkMostrarPrecios.Enabled = true;
                chkMostrarPrecios.Visible = true;
            }
            else
            {
                chkMostrarPrecios.Enabled = DtGeneral.PermisosEspeciales.TienePermiso(sMostrar_Precios_Validacion);
                chkMostrarPrecios.Visible = DtGeneral.PermisosEspeciales.TienePermiso(sMostrar_Precios_Validacion);
            }


            cboTitulosReporte.Enabled = false;
            cboTitulosReporte.SelectedIndex = 0; 

            txtCte.Focus(); 
        }

        private void BloqueaControles(bool Bloquear)
        { 
            bool bValor = !Bloquear;
            
            txtCte.Enabled = bValor;
            txtSubCte.Enabled = bValor;
            txtPro.Enabled = bValor;
            txtSubPro.Enabled = bValor;

            rdoInsumosAmbos.Enabled = bValor;
            rdoInsumosMedicamento.Enabled = bValor;
            rdoInsumoMedicamentoSP.Enabled = bValor;
            rdoInsumoMedicamentoNOSP.Enabled = bValor;
            rdoInsumoMatCuracion.Enabled = bValor;

            rdoTpDispAmbos.Enabled = bValor;
            rdoTpDispVenta.Enabled = bValor;
            rdoTpDispConsignacion.Enabled = bValor;

            dtpFechaInicial.Enabled = bValor;
            dtpFechaFinal.Enabled = bValor;

            chkMostrarPaquetes.Enabled = bValor;
            chkMostrarPrecios.Enabled = bValor;
            chkMostrarLotes.Enabled = bValor;
            chkMostrarDevoluciones.Enabled = bValor;


            rdoOrigenDispensacion_01__General.Enabled = bValor;
            rdoOrigenDispensacion_02_Dispensacion.Enabled = bValor;
            rdoOrigenDispensacion_03_Vales.Enabled = bValor;

            rdoOrdenamiento_01__ClaveSSA.Enabled = bValor;
            rdoOrdenamiento_02__DescripcionClaveSSA.Enabled = bValor;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false; 
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;  

            //FrameCliente.Enabled = false;
            //FrameInsumos.Enabled = false; 
            //FrameDispensacion.Enabled = false;
            //FrameFechas.Enabled = false;
            //FrameListaReportes.Enabled = false;

            chkMostrarPaquetes.Enabled = false;
            chkMostrarPrecios.Enabled = false;
            chkMostrarLotes.Enabled = false;
            cboReporte.Enabled = false;
            cboTitulosReporte.Enabled = false; 
            BloqueaControles(true);


            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000); 

            _workerThread = new Thread(this.LlenarGrid);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            // LlenarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (ObtenerInformacionExcel())
            {
                ExportarExcel_Validacion(); 
            }
        }

        private void ExportarExcel_Validacion()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            clsLeer encabezadoExcel = new clsLeer();
            clsLeer detalladoExcel = new clsLeer(); 

            string sNombreHoja = "Detallado_Dispensacion";
            string sNombreDocumento = "Reporte de dispensación";

            int iHoja = 1; 
            int iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            leerExportarExcel.RenombrarTabla(1, "Encabezado");
            leerExportarExcel.RenombrarTabla(2, "Detallado");

            encabezadoExcel.DataTableClase = leerExportarExcel.Tabla("Encabezado");
            detalladoExcel.DataTableClase = leerExportarExcel.Tabla("Detallado");

            encabezadoExcel.Leer();
            detalladoExcel.Leer();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, encabezadoExcel.Campo("Empresa"));
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, encabezadoExcel.Campo("Farmacia"));
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, encabezadoExcel.Campo("TituloReporte"));
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 9;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, detalladoExcel.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

        }

        //private void ExportarExcel_Validacion_Obsoleto()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + DtGeneral.FarmaciaConectada + ".xls";
        //    string sPeriodo = "";


        //    this.Cursor = Cursors.WaitCursor;
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion,xls", DatosCliente);

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
        //        this.Cursor = Cursors.Default; 
        //    }
        //}

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
        private void LlenarGrid()
        {
            this.Cursor = Cursors.WaitCursor;           

            bEjecutando = true; 
            int iTipoInsumo = 0, iTipoDispensacion = 0, iTipoInsumoMedicamento = 0;
            int iMostrarPrecios = chkMostrarPrecios.Checked ? 1 : 0;
            int iMostrarDevoluciones = chkMostrarDevoluciones.Checked ? 1 : 0;
            int iOrigenDispensacion = 0;
            int iOrdenamiento_Concentrados = rdoOrdenamiento_01__ClaveSSA.Checked ? 1 : 2;

            // Determinar el tipo de dispensacion a mostrar 
            if(rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 1;
            }

            if(rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 2;
            }


            // Determinar que tipo de producto se mostrar 
            if(rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if(rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }


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

            if(!rdoOrigenDispensacion_01__General.Checked)
            {
                iOrigenDispensacion = rdoOrigenDispensacion_02_Dispensacion.Checked ? 1 : 2;
            }


            string sSql = string.Format("Set Dateformat YMD \nExec spp_Rpt_Administrativos \n" +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', \n" +
                " @IdPrograma = '{5}', @IdSubPrograma = '{6}', @TipoDispensacion = '{7}', \n" +
                " @FechaInicial = '{8}', @FechaFinal = '{9}', @TipoInsumo = '{10}', @TipoInsumoMedicamento = '{11}', @SubFarmacias = '{12}', \n" +
                " @MostrarPrecios = {13}, @MostrarDevoluciones = '{14}', @OrigenDispensacion = '{15}', @Ordenamiento = '{16}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text, txtSubCte.Text, txtPro.Text,
                txtSubPro.Text, iTipoDispensacion, dtpFechaInicial.Text, dtpFechaFinal.Text,
                iTipoInsumo, iTipoInsumoMedicamento, sSubFarmacias, iMostrarPrecios, iMostrarDevoluciones, iOrigenDispensacion, iOrdenamiento_Concentrados); 
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion (NoLock) ");  
            

            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información del reporte.");
            }
            else
            {
                if (leer.Leer())
                {
                    // Grid.LlenarGrid(leer.DataSetClase); 
                    btnImprimir.Enabled = true;
                    bSeEncontroInformacion = true; 
                }
                else
                {
                    bSeEncontroInformacion = false; 
                    // General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }

            bEjecutando = false; // Cursor.Current  
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        #region Generar Exportar Excel 
        private bool ObtenerInformacionExcel()
        {
            bool bRegresa = false;
            int iMostrarAgrupado = chkMostrarPaquetes.Checked ? 1 : 0;
            int iMostrarPrecios = chkMostrarPrecios.Checked ? 1 : 0; 
            string sSql = ""; 


            sSql = string.Format("Exec spp_Rpt_Administrativos_ToExcel  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @MostrarAgrupado = '{3}', @MostrarPrecios = '{4}' ",
                  DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iMostrarAgrupado, iMostrarPrecios); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacionExcel()");
                General.msjError("Ocurrió un error al obtener la información del reporte.");
            }
            else
            {
                bRegresa = leer.Leer(); 
                leerExportarExcel.DataSetClase = leer.DataSetClase;
            }

            return bRegresa; 
        }
        #endregion Generar Exportar Excel

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameListaReportes.Enabled = true; 
                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;

                if (bSeEncontroInformacion)
                {
                    cboReporte.Enabled = true;
                }
                else 
                {
                    _workerThread.Interrupt();
                    _workerThread = null; 

                    btnEjecutar.Enabled = true;
                    BloqueaControles(false);
                    cboReporte.Enabled = true;
                    cboTitulosReporte.Enabled = true; 

                    ////FrameCliente.Enabled = true; 
                    ////FrameInsumos.Enabled = true;
                    ////FrameDispensacion.Enabled = true;
                    ////FrameFechas.Enabled = true;
                    ////FrameListaReportes.Enabled = true; 
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }

        private void rdoInsumoMedicamentoSP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoInsumoMedicamentoNOSP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sValor = "";

            btnExportarExcel.Enabled = false; 
            chkMostrarPaquetes.Enabled = false;
            chkMostrarLotes.Enabled = false; 

            try
            {
                sValor = (string)cboReporte.ItemActual.Item;
            }
            catch { }

            if (sValor == "0" || sValor == "1" || sValor == "2")
            {
                chkMostrarPaquetes.Enabled = true;
                chkMostrarLotes.Enabled = true;
                btnExportarExcel.Enabled = true; 
            }

            cboTitulosReporte.Enabled = chkMostrarPaquetes.Enabled;
            if (!cboTitulosReporte.Enabled) cboTitulosReporte.SelectedIndex = 0; 

        }


    } 
}
