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

using Dll_IFacturacion; 

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmReporteador_Remisiones_Manuales : FrmBaseExt
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
        clsGrid grid;
        //DataSet dtsProgramas, dtsSubProgramas;

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

        public FrmReporteador_Remisiones_Manuales()
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

            FrameProceso.Left = 220;
            FrameProceso.Top = 84;
            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 

            grid = new clsGrid(ref grdUnidades, this);
            grid.AjustarAnchoColumnasAutomatico = true;

            ObtenerInsumos();
            ObtenerFinanciamiento();
            CargarFormatosDeImpresion();
        }

        private void FrmReporteador_Remisiones_Manuales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void BloquearControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            cboFormatosDeImpresion.Enabled = bBloquear;
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

            chkMarcarDesmarcar.Enabled = bBloquear; 

            chkEXCEL.Enabled = bBloquear;
            chkPDF.Enabled = !Bloquear; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            IniciarToolBar(true, true, false);

            iRenlgonEnProceso = 0;
            sRutaDestino = "";
            bFolderDestino = false;

            ////btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            grid.Limpiar();
            grid.SetOrder((int)Cols.Procesado, (int)Cols.Procesado, false);
            Fg.IniciaControles(this, true);
            BloquearControles(false); 

            ////btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = "";

            rdoRM_Todo.Checked = true;
            rdoOIN_Todos.Checked = true;
            rdoInsumoAmbos.Checked = true;
            chkFolios.Checked = false;
            chkFechas.Checked = true; 

            chkPDF.Checked = true;
            chkEXCEL.Checked = false;


            //chkPDF.Enabled = false;
            //chkEXCEL.Enabled = false;


            //////sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //////sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            //////lblDirectorioTrabajo.Text = sRutaDestino;
            //////bFolderDestino = true;

            General.FechaSistemaObtener();
            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_REMISIONES\{0}", General.FechaYMD(General.FechaSistema, ""));
            lblDirectorioTrabajo.Text = sRutaDestino;
            bFolderDestino = true;

            if (!DtGeneral.EsAdministrador)
            {
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarRemision_DelPeriodo();
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                IniciarProcesamiento();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Imprimir(); 
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
            btnGenerarDocumentos.Enabled = Generar;
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = true;

            if (bRegresa)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
                }
            }

            if(bRegresa && cboFormatosDeImpresion.SelectedIndex == 0 )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboFormatosDeImpresion.Focus();
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
            string sSql = ""; 
            string sFiltro = "";
            int iTipoDeRemision = 0;
            int iOrigenInsumo = 0;
            int iTipoInsumo = 0;

            if(rdoRM_Producto.Checked) iTipoDeRemision = 1;
            if(rdoRM_Servicio.Checked) iTipoDeRemision = 2;


            if(rdoOIN_Venta.Checked) iOrigenInsumo = 1;
            if(rdoOIN_Consignacion.Checked) iOrigenInsumo = 2;

            if(rdoInsumoMedicamento.Checked)iTipoInsumo = 1;
            if(rdoInsumoMaterialDeCuracion.Checked) iTipoInsumo = 2;



            ////if (cboFinanciamiento.Data != "0")
            ////{
            ////    sFiltro += string.Format(" And IdFinanciamiento= '{0}' ", cboFinanciamiento.Data);
            ////}

            ////if (cboTipoInsumo.Data != "0")
            ////{
            ////    sFiltro += string.Format(" And TipoDeRemision = '{0}' ", cboTipoInsumo.Data);
            ////}

            ////if (!rdoRM_Todo.Checked)
            ////{
            ////    sFiltro += string.Format(" And TipoDeRemision in ( {0} ) ", rdoRM_Producto.Checked ? " 1, 3, 4, 5 " : " 2, 6 "); 
            ////}

            ////if (!rdoOIN_Todos.Checked)
            ////{
            ////    sFiltro += string.Format(" And OrigenInsumo = {0} ", rdoOIN_Venta.Checked ? " 0 " : " 1 ");
            ////}

            ////if (!rdoInsumoAmbos.Checked)
            ////{
            ////    sFiltro += string.Format(" And TipoInsumo in ( {0} ) ", rdoInsumoMedicamento.Checked ? " 2 " : " 0, 1 ");
            ////}

            ////if (chkFolios.Checked)
            ////{
            ////    if (txtFolioInicial.Text != "")
            ////    {
            ////        sFiltro += string.Format(" and FolioRemision >= '{0}' ", Fg.PonCeros(txtFolioInicial.Text, 10));
            ////        if (txtFolioFinal.Text != "")
            ////        {
            ////            sFiltro += string.Format(" and FolioRemision <= '{0}' ", Fg.PonCeros(txtFolioFinal.Text, 10));
            ////        }
            ////    }
            ////}

            ////if (chkFechas.Checked)
            ////{
            ////    sFiltro += string.Format(" and convert(varchar(10), FechaRemision, 120) between '{0}' and '{1}' ",  
            ////        General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));
            ////}

            ////sSql = string.Format(
            ////    "Select FolioRemision, convert(varchar(10), FechaRemision, 120) as FechaRemision, IdFinanciamiento, Financiamiento, TipoDeRemision, TipoDeRemisionDesc, " +
            ////    "Total As Importe, 0 as Procesar, 0 as Procesado, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario " +
            ////    "From vw_FACT_Remisiones_Informacion_Resumen (NoLock) " +
            ////    "Where IdEmpresa = '{0}' and IdEstado = '{1}'  {2} " +
            ////    "Order By FolioRemision ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sFiltro );



            sSql = string.Format("Exec spp_FACT_CFD_GetRemisionesManuales_Descarga \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', \n" +
                    "\t@IdFuenteFinanciamiento = '{5}', @IdFinanciamiento = '{6}', \n" +
                    "\t@SegmentoTipoDeRemision = '{7}', @TipoDeRemision = '{8}', @OrigenDeInsumos = '{9}', @TipoDeInsumo = '{10}', \n" +
                    "\t@AplicarFiltroFolios = '{11}', @FolioInicial = '{12}', @FolioFinal = '{13}', \n" +
                    "\t@AplicarFiltroFechas = '{14}', @FechaInicial = '{15}', @FechaFinal = '{16}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text, txtSubCte.Text, 
                    txtIdFuenteFinanciamiento.Text, txtIdFinanciamiento.Text,  
                    //cboTipoInsumo.Data, 
                    cboTipoInsumo.Data, iTipoDeRemision, iOrigenInsumo, iTipoInsumo, 
                    Convert.ToInt32(chkFolios.Checked), txtFolioInicial.Text, txtFolioFinal.Text,
                    Convert.ToInt32(chkFechas.Checked), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value) 
                );  

            grid.Limpiar(true);
            chkMarcarDesmarcar.Checked = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la lista de remisiones.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciarToolBar(true, true, false);
                    General.msjUser("No se encontro información con los criterios especificados.");
                }
                else
                {
                    IniciarToolBar(true, true, true);
                    grid.LlenarGrid(leer.DataSetClase, false, false);
                }
            }
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            ////btnImprimir.Enabled = false;
            ////btnExportarExcel.Enabled = false;

            cboTipoInsumo.Enabled = false;

            // bloqueo principal 
            IniciarToolBar(false, false, false);
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            ////grid.SetValue((int)Cols.Inicio, "");
            ////grid.SetValue((int)Cols.Fin, "");
            ////grid.SetValue((int)Cols.Procesando, ""); 

            BloquearControles(true); 

            MostrarEnProceso(true);

            btnDirectorio.Enabled = false;

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.SetValue((int)Cols.Procesado, 0);

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private string ObtenerMarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa = string.Format("{0}:{1}:{2}", Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2));

            return sRegresa;
        }

        private void ObtenerInformacion()
        {
            clsRemision_GenerarDocumentos documentos = new clsRemision_GenerarDocumentos();
            documentos.RemisionesManuales = true; 

            string sFolioRemision = "";
            string sDescripcion = "";
            string sFarmaciaDispensacion = "";
            string sBeneficiario = "";
            bool bDocumentoGenerado = false;
            string sDocto_01_Generados = System.IO.Path.Combine(sRutaDestino, "Documentos_01_Generados.txt");
            string sDocto_02_Error = System.IO.Path.Combine(sRutaDestino, "Documentos_02_Errores.txt");
            bool bDocumentosInicializados = false; 

            DtIFacturacion.EliminarArchivo(sDocto_01_Generados);
            DtIFacturacion.EliminarArchivo(sDocto_02_Error);

            StreamWriter swExito = null; // = new StreamWriter(sDocto_01_Generados, false);
            StreamWriter swError = null; // = new StreamWriter(sDocto_02_Error, false);

            bEjecutando = true;
            documentos.GenerarDirectorio_Farmacia = true;
            documentos.Generar_EXCEL = chkEXCEL.Checked;
            documentos.Generar_PDF = chkPDF.Checked;
            documentos.RutaDestinoReportes = sRutaDestino;

            grid.SetOrder((int)Cols.Procesado, (int)Cols.Procesado, false);

            for(int i = 1; i <= grid.Rows; i++)
            {
                grid.ColorRenglon(i, Color.White);
            }


            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sFolioRemision = grid.GetValue(i, (int)Cols.FolioRemision);
                    sFarmaciaDispensacion = grid.GetValue(i, (int)Cols.IdFarmaciaDispensacion) + "__" + grid.GetValue(i, (int)Cols.FarmaciaDispensacion);
                    sBeneficiario = grid.GetValue(i, (int)Cols.Referencia_Beneficiario) + "__" + grid.GetValue(i, (int)Cols.Referencia_NombreBeneficiario);

                    sDescripcion = sFarmaciaDispensacion;

                    if (sBeneficiario != "__")
                    {
                        sDescripcion = sFarmaciaDispensacion + "_" + sBeneficiario;
                    }


                    sDescripcion = sDescripcion.Replace(" ", "_").Replace("-", "_");

                    ////documentos.GenerarRemisiones(sIdFadocumentos.acia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                    bDocumentoGenerado = documentos.GenerarDocumentos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRemision, sDescripcion, cboFormatosDeImpresion.Data);
                    grid.SetValue(i, (int)Cols.Procesado, bDocumentoGenerado);

                    if(!bDocumentosInicializados)
                    {
                        bDocumentosInicializados = true; 
                        swExito = new StreamWriter(sDocto_01_Generados, false);
                        swError = new StreamWriter(sDocto_02_Error, false);
                    }


                    grid.ColorRenglon(i, bDocumentoGenerado ? Color.GreenYellow : Color.Red);
                    if(bDocumentoGenerado)
                    {
                        swExito.WriteLine(sFolioRemision);
                    }
                    else
                    {
                        swError.WriteLine(sFolioRemision); 
                    }
                }
            }

            if(bDocumentosInicializados)
            {
                swExito.Close();
                swError.Close();
            }

            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);

            BloquearControles(false); 

            MostrarEnProceso(false);

            grid.SetOrder((int)Cols.Procesado, (int)Cols.Procesado, true);
            Application.DoEvents(); 
        }
        #endregion Procesar Informacion

        #region Funciones
        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 220;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private void CargarFormatosDeImpresion()
        {
            string sSql = string.Format(
                "Select NombreFormato, DescripcionDeUso \n" +
                "From FACT_Remisiones_FormatosDeImpresion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' and EsManual = 1 \n" +
                "Order by Orden ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            cboFormatosDeImpresion.Clear();
            cboFormatosDeImpresion.Add("0", "<< Seleccione >>");

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
                {
                    cboFormatosDeImpresion.Add(leer.DataSetClase, true, "NombreFormato", "DescripcionDeUso");
                }
            }

            cboFormatosDeImpresion.SelectedIndex = 0;
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

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {

        }

        private void btnIntegrar_RemisionesManuales_Click( object sender, EventArgs e )
        {
            FrmRemisionesManuales_ImportarInformacion f = new FrmRemisionesManuales_ImportarInformacion();

            f.ShowInTaskbar = false;
            f.ShowDialog();
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            } 
        }
    }
}
