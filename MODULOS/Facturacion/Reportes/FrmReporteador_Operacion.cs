using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


using Dll_IFacturacion; 

namespace Facturacion.Reportes
{
    public partial class FrmReporteador_Operacion : FrmBaseExt
    {
        enum Cols
        {
            Ninguna = 0,
            FolioRemision, FechaRemision, FuenteFinanciamiento, IdFinanciamiento, Financiamiento, 
            TipoDeRemision, TipoDeRemisionDesc, Importe, Procesar, Procesado,
            IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario

            ////IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5, Inicio = 6, Fin = 7, Procesando = 8
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerExcel;
        clsLeer leerLocal;
        clsLeerWebExt leerWeb;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerSubFarmacias;
        clsLeer leerProgramas_Catalogo;
        clsLeer leerSubProgramas_Catalogo;
        clsLeer leerProgramas;
        clsLeer leerSubProgramas;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        //////wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet();

        string sIdPublicoGeneral = "0001";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = "";
        bool bFolderDestino = false;

        string sProceso = "";
        int iRenlgonEnProceso = 0;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        DataSet dtsFarmacias, dtsEstado;
        DataSet dtsDatos = new DataSet();

        public FrmReporteador_Operacion()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            //////conexionWeb = new wsFarmacia.wsCnnCliente();
            //////conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmReporteadorRemisiones");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerSubFarmacias = new clsLeer(ref cnn);
            leerProgramas_Catalogo = new clsLeer(ref cnn);
            leerSubProgramas_Catalogo = new clsLeer(ref cnn);
            leerProgramas = new clsLeer(ref cnn);
            leerSubProgramas = new clsLeer(ref cnn);

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            ////lblTitulo_FiltroSubFarmacia.BackColor = General.BackColorBarraMenu; 

            //this.Width = 1024;
            //this.Height = 570;

            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 


            ObtenerInsumos();
            ObtenerFinanciamiento();
            CargarReporteOperativos();
        }

        private void FrmReporteador_Remisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void BloquearControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            cboReporte.Enabled = bBloquear;
            //cboTipoInsumo.Enabled = bBloquear;

            rdoRM_Servicio.Enabled = bBloquear;
            rdoRM_Producto.Enabled = bBloquear;
            rdoRM_Todo.Enabled = bBloquear;

            rdoOIN_Venta.Enabled = bBloquear;
            rdoOIN_Consignacion.Enabled = bBloquear;
            rdoOIN_Todos.Enabled = bBloquear;

            rdoInsumoMedicamento.Enabled = bBloquear;
            rdoInsumoMaterialDeCuracion.Enabled = bBloquear;
            rdoInsumoAmbos.Enabled = bBloquear;

            txtFolioInicial.Enabled = bBloquear;
            txtFolioFinal.Enabled = bBloquear;
            chkFolios.Enabled = bBloquear;


            dtpFechaInicial.Enabled = bBloquear;
            dtpFechaFinal.Enabled = bBloquear; 
            chkFechas.Enabled = bBloquear;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            IniciarToolBar(true, true, false);

            iRenlgonEnProceso = 0;
            sRutaDestino = "";
            bFolderDestino = false;

            Fg.IniciaControles(this, true);
            BloquearControles(false); 

            ////btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = "";

            rdoRM_Todo.Checked = true;
            rdoOIN_Todos.Checked = true;
            rdoInsumoAmbos.Checked = true;
            rdoFechas_01_Remision.Checked = true;
            chkFolios.Checked = false;
            chkFechas.Checked = true;


            //chkPDF.Enabled = false;
            //chkEXCEL.Enabled = false;


            //////sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //////sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            //////lblDirectorioTrabajo.Text = sRutaDestino;
            //////bFolderDestino = true;

            General.FechaSistemaObtener();
            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_REMISIONES\{0}", General.FechaYMD(General.FechaSistema, ""));
            bFolderDestino = true;

            if (!DtGeneral.EsAdministrador)
            {
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                CargarRemision_DelPeriodo();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //if (validarProcesamiento())
            {
                //IniciarProcesamiento();
                
                ExportarExcel();
                //ExportarExcel__GemBox();

            }
        }

        #region Buscar Informacion Dispensacion
        #region Cliente -- Sub-Cliente
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCte.Text = "";
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada.");
                    txtCte.Text = "";
                    lblCliente.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }

        private void CargarDatosCliente()
        {
            txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada");
                    txtSubCte.Text = "";
                    lblSubCliente.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.SubClientes("txtSubCte_KeyDown", txtCte.Text);
                    if (leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente
        #endregion Buscar Informacion Dispensacion

        #region Buscar Fuente de Financiamiento 
        private void txtIdFuenteFinanciamiento_TextChanged( object sender, EventArgs e )
        {
            lblFuenteFinanciamiento.Text = "";
            txtIdFinanciamiento.Text = "";
            lblTipoFuenteFinanciamiento.Text = ""; 
            txtCte.Enabled = true; 
            txtSubCte.Enabled = true;
        }

        private void txtIdFuenteFinanciamiento_Validating( object sender, CancelEventArgs e )
        {

            if(txtIdFuenteFinanciamiento.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtIdFuenteFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating");

                if(leer.Leer())
                {
                    CargarDatosRubro();
                }
                else
                {
                    txtIdFuenteFinanciamiento.Text = "";
                    lblFuenteFinanciamiento.Text = "";
                    txtIdFuenteFinanciamiento.Focus();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtIdFuenteFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblFuenteFinanciamiento.Text = leer.Campo("Estado");

            txtCte.Enabled = false; 
            txtCte.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");
            lblTipoFuenteFinanciamiento.Text = leer.CampoBool("EsParaExcedente") ? "EXCEDENTE": "ORDINARIO";

            if(leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtIdFuenteFinanciamiento.Text = "";
                lblFuenteFinanciamiento.Text = "";
            }
        }

        private void txtIdFuenteFinanciamiento_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                if(leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Fuente de Financiamiento 

        #region Buscar Financiamiento
        private void txtIdFinanciamiento_TextChanged( object sender, EventArgs e )
        {
            lblFinanciamiento.Text = "";
            //txtCte.Text = "";
        }

        private void txtIdFinanciamiento_Validating( object sender, CancelEventArgs e )
        {

            if(txtIdFinanciamiento.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtIdFuenteFinanciamiento.Text.Trim(), txtIdFinanciamiento.Text.Trim(), "txtIdFuenteFinanciamiento_Validating");

                if(leer.Leer())
                {
                    CargarDatosConcepto();
                }
                else
                {
                    txtIdFinanciamiento.Text = "";
                    lblFinanciamiento.Text = "";
                    txtIdFinanciamiento.Focus();
                }
            }
        }

        private void CargarDatosConcepto()
        {
            txtIdFinanciamiento.Text = leer.Campo("IdFinanciamiento");
            lblFinanciamiento.Text = leer.Campo("Financiamiento");

            if(leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtIdFinanciamiento.Text = "";
                lblFinanciamiento.Text = "";
            }
        }

        private void txtIdFinanciamiento_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtIdFuenteFinanciamiento.Text.Trim());
                if(leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }
        }
        #endregion Buscar Financiamiento

        #region Procesar Informacion
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Generar;
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = true;

            if(bRegresa && cboReporte.SelectedIndex == 0 )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboReporte.Focus();
            }

            return bRegresa;
        }

        private void CrearDirectorioDestino()
        {
            string sDir = "000__GENERAL"; //// cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            string sMarcaTiempo = "";

            sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema);
            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "____" + sMarcaTiempo;

            if (!Directory.Exists(sRutaDestino_Archivos))
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private void CargarRemision_DelPeriodo()
        {
            clsLeer leerResultado = new clsLeer();

            string sSql = ""; 
            string sFiltro = "";
            int iTipoDeRemision = 0;
            int iOrigenInsumo = 0;
            int iTipoInsumo = 0;
            int iTipoDeFecha = rdoFechas_01_Remision.Checked ? 1 : 2;

            if(rdoRM_Producto.Checked) iTipoDeRemision = 1;
            if(rdoRM_Servicio.Checked) iTipoDeRemision = 2;


            if(rdoOIN_Venta.Checked) iOrigenInsumo = 1;
            if(rdoOIN_Consignacion.Checked) iOrigenInsumo = 2;

            if(rdoInsumoMedicamento.Checked)iTipoInsumo = 1;
            if(rdoInsumoMaterialDeCuracion.Checked) iTipoInsumo = 2;


            sSql = string.Format("Exec spp_FACT_Rpt_ReporteDeOperacion \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', \n" +
                    "\t@IdFuenteFinanciamiento = '{5}', @IdFinanciamiento = '{6}', \n" +
                    "\t@SegmentoTipoDeRemision = '{7}', @TipoDeRemision = '{8}', @OrigenDeInsumos = '{9}', @TipoDeInsumo = '{10}', \n" +
                    "\t@AplicarFiltroFolios = '{11}', @FolioInicial = '{12}', @FolioFinal = '{13}', \n" +
                    "\t@TipoDeFecha = '{14}', @AplicarFiltroFechas = '{15}', @FechaInicial = '{16}', @FechaFinal = '{17}', @iOpcion = '{18}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text, txtSubCte.Text, 
                    txtIdFuenteFinanciamiento.Text, txtIdFinanciamiento.Text,  
                    //cboTipoInsumo.Data, 
                    cboTipoInsumo.Data, iTipoDeRemision, iOrigenInsumo, iTipoInsumo, 
                    Convert.ToInt32(chkFolios.Checked), txtFolioInicial.Text, txtFolioFinal.Text,

                    iTipoDeFecha, 
                    Convert.ToInt32(chkFechas.Checked), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), cboReporte.Data
                );

            IniciarToolBar(false, false, false);
            Application.DoEvents();

            if (!leerExportarExcel.Exec(sSql))
            {
                Error.GrabarError(leerExportarExcel, "");
                General.msjError("Ocurrió un error al obtener la lista de remisiones.");
                IniciarToolBar(true, true, false);
            }
            else
            {
                if (!leerExportarExcel.Leer())
                {
                    IniciarToolBar(true, true, false);
                    General.msjUser("No se encontro información con los criterios especificados.");
                }
                else
                {

                    leerExportarExcel.RenombrarTabla(1, "Resultados");
                    leerResultado.DataTableClase = leerExportarExcel.Tabla(1);
                    while (leerResultado.Leer())
                    {
                        leerExportarExcel.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
                    }


                    IniciarToolBar(true, true, true);
                    //grid.LlenarGrid(leer.DataSetClase, false, false);
                }
            }

            Application.DoEvents();
        }

        //private void IniciarProcesamiento()
        //{
        //    bSeEncontroInformacion = false;
        //    btnNuevo.Enabled = false;
        //    btnEjecutar.Enabled = false;
        //    ////btnImprimir.Enabled = false;
        //    ////btnExportarExcel.Enabled = false;

        //    cboTipoInsumo.Enabled = false;

        //    // bloqueo principal 
        //    IniciarToolBar(false, false, false);
        //    ////grid.SetValue((int)Cols.Inicio, "");
        //    ////grid.SetValue((int)Cols.Fin, "");
        //    ////grid.SetValue((int)Cols.Procesando, ""); 

        //    BloquearControles(true); 

        //    MostrarEnProceso(true);

        //    // bSeEjecuto = false;
        //    tmEjecuciones.Enabled = true;
        //    tmEjecuciones.Start();


        //    Cursor.Current = Cursors.WaitCursor;
        //    System.Threading.Thread.Sleep(1000);

        //    _workerThread = new Thread(this.ObtenerInformacion);
        //    _workerThread.Name = "GenerandoValidacion";
        //    _workerThread.Start();
        //    // LlenarGrid(); 
        //}

        private string ObtenerMarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa = string.Format("{0}:{1}:{2}", Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2));

            return sRegresa;
        }

        private void ExportarExcel()
        {

            bEjecutando = true;
            bEjecutando = false;
            string sAño = "", sNombre = "", sNombreHoja = "";
            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;
            int iHojas_Totales = 0;
            int iHojas_Agregadas = 0;
            string sFileName = ""; 

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            leer.RegistroActual = 1;
            ////xpExcel.MostrarAvanceProceso = true; 
            ////xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

            clsLeer exportarExcel = new clsLeer();
            clsLeer dtsLocal = new clsLeer();


            sNombre = cboReporte.SelectedItem.ToString().Trim();
            sNombreHoja = sNombre;

            if (sNombre.Trim().Length >= 10)
            {
                sNombreHoja = sNombre.Substring(0, 10);
            }


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;
            GC.Collect(); 

            if(generarExcel.PrepararPlantilla(sNombre))
            {
                exportarExcel.DataTableClase = leerExportarExcel.Tabla("Resultados");
                iHojas_Totales = exportarExcel.Registros; 

                while (exportarExcel.Leer())
                {
                    if (iHojas_Agregadas > 0)
                    {
                        //generarExcel.CerraArchivo();
                        //generarExcel.AbrirArchivo(generarExcel.RutaArchivo, generarExcel.NombreArchivo);
                    }

                    sNombreHoja = exportarExcel.Campo("NombreTabla");
                    dtsLocal.DataTableClase = leerExportarExcel.Tabla(sNombreHoja);

                    sFileName = string.Format(@"{0}_{1}{2}", generarExcel.NombreDocumento, sNombreHoja, generarExcel.Extension);
                    generarExcel.CrearArchivo(generarExcel.RutaArchivo, sFileName);

                    generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                    generarExcel.Theme = XLTableTheme.None;

                    ////generarExcel.ArchivoExcelOpenXml.Workbook.Worksheets.Add(sNombreHoja);


                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, string.Format(cboReporte.SelectedItem.ToString() + " " + sNombreHoja));
                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);

                    iRenglon = 8;
                    //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                    generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsLocal.DataSetClase);

                    //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                    generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                    //GC.AddMemoryPressure(100000);
                    GC.Collect();

                    //generarExcel.CerraArchivo();
                    iHojas_Agregadas++;
                    iRenglon = iHojas_Totales; 

                    //generarExcel.GuardarDocumento(iHojas_Agregadas < iHojas_Totales);
                    generarExcel.GuardarDocumento(true);
                }

                //generarExcel.CerraArchivo();
                if ( iHojas_Agregadas > 0 )
                {
                    //generarExcel.CerraArchivo_Stream(); 
                    //generarExcel.CerrarArchivo();
                    generarExcel.AbrirDirectorioDestino(true);
                }
            }

            BloquearControles(false); 

            MostrarEnProceso(false);

            Application.DoEvents(); 
        }

        ////private void ExportarExcel__GemBox()
        ////{

        ////    bEjecutando = true;
        ////    bEjecutando = false;
        ////    string sAño = "", sNombre = "", sNombreHoja = "";
        ////    int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;
        ////    int iHojas_Totales = 0;
        ////    int iHojas_Agregadas = 0;
        ////    string sFileName = "";

        ////    // bloqueo principal 
        ////    Cursor.Current = Cursors.Default;
        ////    IniciarToolBar(true, true, true);

        ////    clsGenerarExcel generarExcel = new clsGenerarExcel();
        ////    leer.RegistroActual = 1;
        ////    ////xpExcel.MostrarAvanceProceso = true; 
        ////    ////xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

        ////    clsLeer exportarExcel = new clsLeer();
        ////    clsLeer dtsLocal = new clsLeer();


        ////    //if (generarExcel.PrepararPlantilla(sNombre))
        ////    {
        ////        exportarExcel.DataTableClase = leerExportarExcel.Tabla("Resultados");
        ////        iHojas_Totales = exportarExcel.Registros;

        ////        while (exportarExcel.Leer())
        ////        { 
        ////            sNombreHoja = exportarExcel.Campo("NombreTabla");
        ////            dtsLocal.DataTableClase = leerExportarExcel.Tabla(sNombreHoja);

        ////            sFileName = string.Format(@"{0}_{1}{2}", @"F:\Users\JesusDiaz\Desktop\REPORTES\", sNombreHoja, ".xlsx");
        ////            //generarExcel.CrearArchivo(generarExcel.RutaArchivo, sFileName); 

        ////            iHojas_Agregadas++;


        ////            //// Set license key to use GemBox.Spreadsheet in Free mode.
        ////            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

        ////            //// Create a new empty Excel file.
        ////            var workbook = new GemBox.Spreadsheet.ExcelFile();

        ////            //// Create a new worksheet and set cell A1 value to 'Hello world!'.
        ////            var worksheet = workbook.Worksheets.Add(sNombreHoja);

        ////            worksheet.Cells[iRenglon, iColBase].Worksheet.InsertDataTable(dtsLocal.DataTableClase); 



        ////            //// Save to XLSX file.
        ////            workbook.Save(sFileName);

        ////        }

        ////    }

        ////    BloquearControles(false);

        ////    MostrarEnProceso(false);

        ////    Application.DoEvents();
        ////}
        #endregion Procesar Informacion

        #region Funciones
        private void MostrarEnProceso(bool Mostrar)
        {
            //if (Mostrar)
            //{
            //    FrameProceso.Left = 220;
            //}
            //else
            //{
            //    FrameProceso.Left = this.Width + 100;
            //}
        }

        private void CargarReporteOperativos()
        {
            string sSql = string.Format(
                "Select \n" + 
                "\tIdReporte, Descripcion, \n" +
                "\tProcesa_Producto, Procesa_Servicio, (case when Procesa_Producto = 1 and Procesa_Servicio = 1 then 1 else 0 end) as Procesa_Remision_Ambos, \n" + 
                "\tProcesa_Venta, Procesa_Consigna, (case when Procesa_Venta = 1 and Procesa_Consigna = 1 then 1 else 0 end) as Procesa_Origen_Ambos, \n" +
                "\tProcesa_Medicamento, Procesa_MaterialDeCuracion, (case when Procesa_Medicamento = 1 and Procesa_MaterialDeCuracion = 1 then 1 else 0 end) as Procesa_Insumo_Ambos \n" + 
                "From FACT_Reportes_Operativos (NoLock) \n" +
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and Status = 'A' ",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            cboReporte.Clear();
            cboReporte.Add("0", "<< Seleccione >>");

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
                {
                    cboReporte.Add(leer.DataSetClase, true, "IdReporte", "Descripcion");
                }
            }

            cboReporte.SelectedIndex = 0;
        }

        private void ObtenerInsumos()
        {
            string sSql = string.Format(
                "Select TipoDeRemision, (cast(TipoDeRemision as varchar(10)) + ' - ' + Descripcion) As TipoDeRemisionDescripcion \n" +
                "From FACT_TiposDeRemisiones (NoLock) \n" +
                "Where Status = 'A' \n");

            cboTipoInsumo.Clear();
            cboTipoInsumo.Add("0", "<< Seleccione >>");
            //cboTipoInsumo.Add("1", "1 - INSUMOS");
            //cboTipoInsumo.Add("2", "2 - ADMINISTRACIÓN");
            //cboTipoInsumo.Add("3", "3 - INSUMOS INCREMENTO");
            //cboTipoInsumo.Add("4", "4 - INSUMOS VENTA DIRECTA");
            //cboTipoInsumo.Add("4", "5 - INSUMOS VENTA DIRECTA");
            //cboTipoInsumo.Add("6", "6 - ADMINISTRACIÓN VENTA DIRECTA");	

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
                {
                    cboTipoInsumo.Add(leer.DataSetClase, true, "TipoDeRemision", "TipoDeRemisionDescripcion");
                }
            }

            cboTipoInsumo.SelectedIndex = 0;
        }

        private void ObtenerFinanciamiento()
        {
            ////cboFinanciamiento.Clear();
            ////cboFinanciamiento.Add("0", "<< Seleccione >>");

            ////string sSql = string.Format(
            ////    "Select IdFinanciamiento As IdFinanciamiento, (IdFinanciamiento + ' - ' + Descripcion) As Financiamiento \n" +
            ////    "From FACT_Fuentes_De_Financiamiento_Detalles (NoLock) \n" +
            ////    "Where Status = 'A' \n");

            ////if(leer.Exec(sSql))
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        cboFinanciamiento.Add(leer.DataSetClase, true, "IdFinanciamiento", "Financiamiento");
            ////    }
            ////}

        }

        #endregion Funciones

        private void cboReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Procesa_Venta, Procesa_Consigna, Procesa_Origen_Ambos 
            //Procesa_Medicamento, Procesa_MaterialDeCuracion, Procesa_Insumo_Ambos
            clsLeer leerItem = new clsLeer();

            try
            {
                leerItem.DataRowClase = (DataRow)cboReporte.ItemActual.Item;
                leerItem.Leer();
            }
            catch { }

            rdoRM_Producto.Checked = false;
            rdoRM_Servicio.Checked = false;
            rdoRM_Todo.Checked = false;

            rdoOIN_Venta.Checked = false;
            rdoOIN_Consignacion.Checked = false;
            rdoOIN_Todos.Checked = false;

            rdoInsumoMedicamento.Checked = false;
            rdoInsumoMaterialDeCuracion.Checked = false;
            rdoInsumoAmbos.Checked = false;


            //Procesa_Producto, Procesa_Servicio, (case when Procesa_Producto = 1 and Procesa_Servicio = 1 then 1 else 0 end) as Procesa_Remision_Ambos

            rdoRM_Producto.Enabled = leerItem.CampoBool("Procesa_Producto");
            rdoRM_Servicio.Enabled = leerItem.CampoBool("Procesa_Servicio");
            rdoRM_Todo.Enabled = leerItem.CampoBool("Procesa_Remision_Ambos");

            rdoOIN_Venta.Enabled = leerItem.CampoBool("Procesa_Venta");
            rdoOIN_Consignacion.Enabled = leerItem.CampoBool("Procesa_Consigna");
            rdoOIN_Todos.Enabled = leerItem.CampoBool("Procesa_Origen_Ambos");

            rdoInsumoMedicamento.Enabled = leerItem.CampoBool("Procesa_Medicamento");
            rdoInsumoMaterialDeCuracion.Enabled = leerItem.CampoBool("Procesa_MaterialDeCuracion");
            rdoInsumoAmbos.Enabled = leerItem.CampoBool("Procesa_Insumo_Ambos");



            rdoRM_Producto.Checked = leerItem.CampoBool("Procesa_Producto");
            rdoRM_Servicio.Checked = leerItem.CampoBool("Procesa_Servicio");
            rdoRM_Todo.Checked = leerItem.CampoBool("Procesa_Remision_Ambos");

            rdoOIN_Venta.Checked = leerItem.CampoBool("Procesa_Venta");
            rdoOIN_Consignacion.Checked = leerItem.CampoBool("Procesa_Consigna");
            rdoOIN_Todos.Checked = leerItem.CampoBool("Procesa_Origen_Ambos");

            rdoInsumoMedicamento.Checked = leerItem.CampoBool("Procesa_Medicamento");
            rdoInsumoMaterialDeCuracion.Checked = leerItem.CampoBool("Procesa_MaterialDeCuracion");
            rdoInsumoAmbos.Checked = leerItem.CampoBool("Procesa_Insumo_Ambos");

        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {

        }
    }
}
