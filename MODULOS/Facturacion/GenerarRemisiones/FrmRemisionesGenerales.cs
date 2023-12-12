using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO; 
using System.Windows.Forms;


// Implementacion de hilos 
using System.Threading;

using ClosedXML.Excel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using Dll_IFacturacion;



namespace Facturacion.GenerarRemisiones
{
    public partial class FrmRemisionesGenerales : FrmBaseExt
    {
        #region Enumeradores 
        enum cols
        {
            ninguno,
            CodigoEAN, Idproducto, Descripcion, CantidadVendida, CantidadRemision_Insumo, CantidadRemision_Admon,
            PorRemisionar_Insumo, PorRemisionar_Admon
        }

        enum ColsRemisiones
        {
            Ninguno = 0,
            FolioRemision = 1,
            Fecha,
            FuenteFinancimiento,
            ClaveFinanciamiento,
            Financiamiento,
            ClaveRemision,
            TipoDeRemision,
            Importe,
            Seleccionar
        }

        enum Cols_FF
        {
            Ninguna = 0, 
            IdFuenteFinanciento = 1, 
            TipoDeFuente, 
            EsDiferencial, 
            AplicarDiferencial, 
            IdFinanciamiento, 
            Financiamiento, 
            IdCliente, Cliente, 
            IdSubCliente, SubCliente 
        }

        enum Cols_FoliosVenta
        {
            Ninguna = 0,
            IdAlmacen = 1,
            Folio,
            Fecha,
            IdCliente, Cliente,
            IdSubCliente, SubCliente
        }

        enum Cols_Farmacias
        {
            Ninguna = 0,
            IdFarmacia = 1,
            Farmacia,
            Remisionar,
            Procesada,
            NumeroDeRemisionesGeneradas
        }
        enum Cols_Farmacias_Folios
        {
            Ninguna = 0,
            IdFarmacia = 1,
            Farmacia,
            IdCliente, 
            Cliente, 
            IdSubCliente, 
            SubCliente, 
            FolioVenta 
        }
        enum Cols_Programas
        {
            Ninguna = 0,
            IdPrograma = 1,
            Programa,
            IdSubPrograma,
            SubPrograma
        }

        enum Cols_Claves
        {
            Ninguno = 0,
            ClaveSSA = 1,
            Descripcion
        }

        private enum Cols_Documentos
        {
            Ninguno,
            Fecha = 1,
            FolioRelacion,
            NombreDocumento,
            TipoDeUnidades,
            TipoDeUnidades_Desc,
            Procesa_Venta,
            Procesa_Venta_Desc,
            Procesa_Consigna,
            Procesa_Consigna_Desc,
            Procesa_Producto,
            Procesa_Producto_Desc,
            Procesa_Servicio,
            Procesa_Servicio_Desc,
            Procesar
        }

        private enum Cols_Facturas
        {
            Ninguno, 
            Serie = 1, 
            Folio, 
            SerieFolio, 
            Fecha, 
            Cliente, 
            FuenteFinanciamiento, 
            Financiamiento, 
            TipoDocumento, 
            TipoInsumo, 
            Procesa_Producto, 
            Procesa_Servicio, 
            Procesa_Medicamento, 
            Procesa_MaterialDeCuracion, 
            Procesar  
        }
        #endregion Enumeradores 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRemisiones = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerDetalles;
        clsLeer leerRemisiones;
        clsLeer leerExportarExcel;
        clsGrid myGrid;
        clsGrid gridUnidades;
        clsGrid gridFolios;
        clsGrid gridFF;
        clsGrid gridProgramas;
        clsGrid gridClavesExclusivas;
        clsGrid gridClavesExcluidas;

        System.Threading.Thread thHilo;
        System.Threading.Thread thHilo_RemisionesGeneradas;

        clsListView lst;
        clsGrid gridFacturas;
        clsGrid gridDocumentosComprobacion;

        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        DataSet dtsFarmacias;
        DataSet dtsDatos = new DataSet();
        eEsquemaDeFacturacion tpFacturacion = DtIFacturacion.EsquemaDeFacturacion;
        eTipoRemision tpTipoDeRemision = eTipoRemision.Ninguno;
        bool bEsquemaDeFacturacionValido = false;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sFolio = "", sMensaje = "";
        double dMontoFinanciamiento = 0;
        bool bEsExcedente_Rubro = false;
        bool bEsExcedente_Concepto = false;
        string sFormato = "#,###,###,##0.###0";
        string sIdentificador = "";

        //Para Auditoria
        clsAuditoria auditoria;

        //Hilos
        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        bool bFrameUnidades = true;
        bool bFrameFolios = false;
        bool bObteniendoRemisiones = false; 

        string sListaDeSeleccion = "";
        DataSet dtsProgramas_SubProgramas = new DataSet();

        string sTipoDe_FuenteDeFinanciamiento = "";
        bool bEsDiferencial = false;

        FrmDescargarVenta info;
        string sGUID = "";
        string sUrl = "";

        eTipoDeUnidades tipoDeUnidades = eTipoDeUnidades.Ninguna;
        string sStoreDeProceso = "";
        List<string> listaGuids = new List<string>();
        int iAlto_Lst_Resultados = 0;


        string sDato_01_FuenteFinanciamiento = "";
        string sDato_02_Financiamiento = "";
        string sDato_03_IdCliente = "";
        string sDato_04_IdSubCliente = "";
        string sDato_05_EsDiferencial = "";

        string sDato_06_IdFarmacia = "";
        string sDato_07_FoliosDeVenta = "";
        string sDato_08_FechaInicial = "";
        string sDato_09_FechaFinal = "";
        string sDato_10_ProgramasDeAtencion = "";

        string sDato_11_EsRelacionFactura = "";
        string sDato_12_Serie = "";
        string sDato_13_Folio = "";
        string sDato_14_FacturaEnCajas = "";
        string sDato_15_RelacionPorMontos = "";
        string sDato_16_ProcesarSoloClavesConReferencias = "";

        string sDato_17_EsDocumentoDeComprobacion = "";
        string sDato_18_DocumentoDeComprobacion = "";

        string sDato_19_ListaClavesExclusivas = "";
        string sDato_20_ListaClavesExcluidas = "";

        int iDatos_21_Procesa_Venta = 0;
        int iDatos_22_Procesa_Consigna = 0;
        int iDatos_23_Procesa_Producto = 0;
        int iDatos_24_Procesa_Servicio = 0;
        int iDatos_25_Procesa_Medicamento = 0;
        int iDatos_26_Procesa_MaterialDeCuracion = 0;

        bool bModoDebug = false;
        bool bMostrar = true; 

        public FrmRemisionesGenerales() : this(eTipoDeUnidades.Farmacias)
        {
        }
        public FrmRemisionesGenerales(eTipoDeUnidades TipoDeUnidades)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            tipoDeUnidades = TipoDeUnidades;


            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            leerDetalles = new clsLeer(ref cnn);
            leerRemisiones = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnnRemisiones);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);


            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            ////myGrid = new clsGrid(ref grdProductos, this);
            ////myGrid.AjustarAnchoColumnasAutomatico = true; 
            //scTabControlExt1.TabPages[0].

            lst = new clsListView(listView_RemisionesGeneradas);
            lst.PermitirAjusteDeColumnas = true;
            lst.OrdenarColumnas = false;

            gridUnidades = new clsGrid(ref grdFarmacias, this);
            gridUnidades.AjustarAnchoColumnasAutomatico = true;
            gridUnidades.EstiloDeGrid = eModoGrid.ModoRow;

            gridFolios = new clsGrid(ref grdFolios_x_Farmacia, this);
            gridFolios.AjustarAnchoColumnasAutomatico = true;
            gridFolios.EstiloDeGrid = eModoGrid.ModoRow;

            gridFF = new clsGrid(ref grdFuentesDeFinanciamiento, this);
            gridFF.AjustarAnchoColumnasAutomatico = true;
            gridFF.EstiloDeGrid = eModoGrid.ModoRow;

            gridProgramas = new clsGrid(ref grdProgramasSubProgramas, this);
            gridProgramas.AjustarAnchoColumnasAutomatico = true;
            gridProgramas.EstiloDeGrid = eModoGrid.ModoRow;


            gridClavesExclusivas = new clsGrid(ref grdClavesExclusivas, this);
            gridClavesExclusivas.AjustarAnchoColumnasAutomatico = true;
            gridClavesExclusivas.EstiloDeGrid = eModoGrid.ModoRow;

            gridClavesExcluidas = new clsGrid(ref grdClavesExcluidas, this);
            gridClavesExcluidas.AjustarAnchoColumnasAutomatico = true;
            gridClavesExcluidas.EstiloDeGrid = eModoGrid.ModoRow;

            gridFacturas = new clsGrid(ref grdFacturas, this);
            gridFacturas.AjustarAnchoColumnasAutomatico = true;
            gridFacturas.EstiloDeGrid = eModoGrid.ModoRow;

            gridDocumentosComprobacion = new clsGrid(ref grdDocumentos, this);
            gridDocumentosComprobacion.AjustarAnchoColumnasAutomatico = true;
            gridDocumentosComprobacion.EstiloDeGrid = eModoGrid.ModoRow;


            ////gridFolios = new clsGrid(ref grdListaDeFoliosDeVenta, this);
            ////gridFolios.AjustarAnchoColumnasAutomatico = true;

            SetTitulos();

            CargarFormatosDeImpresion();
            //CargarUnidades();

            CargarNivelDeInformacion();
            CargarTiposDeBeneficiarios();
            CargarOrigenDeDispensacion();

            chkActualizarListadoDeRemisiones.BackColor = General.BackColorBarraMenu;
            iAlto_Lst_Resultados = Frame_28_RemisionesGeneradas.Height;

            //FrameOrdenDeEjecucion.Visible = DtGeneral.EsEquipoDeDesarrollo; 
        }

        private void SetDebug()
        {
            if (DtGeneral.EsEquipoDeDesarrollo)
            {
                bMostrar = !bMostrar;

                Frame_Parametros_Ejecucion.Visible = bMostrar; 

                ////if (bMostrar)
                ////{
                     
                ////    scTabControlExt1.TabPages[0].Show();
                ////    scTabControlExt1.SelectedIndex = 0; 
                ////}
                ////else
                ////{
                ////    scTabControlExt1.TabPages[0].Hide();
                ////}
                
            }
        }

        private void FrmRemisionesGenerales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    SetDebug(); 
                    break;

                default:
                    break;
            }
        }

        private void FrmRemisionesGenerales_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void SetTitulos()
        {
            string sTitulo = "";
            string sTitulo_Unidades = "";

            switch (tipoDeUnidades)
            {
                case eTipoDeUnidades.Farmacias:
                    sTitulo = "Generar remisiones de farmacias";
                    sTitulo_Unidades = "Farmacias";
                    this.Name = "FrmRemisionesGenerales";
                    break;

                case eTipoDeUnidades.FarmaciasUnidosis:
                    sTitulo = "Generar remisiones de farmacias unidosis";
                    sTitulo_Unidades = "Farmacias unidosis";
                    this.Name = "FrmRemisionesGenerales_02_FarmaciasUnidosis";
                    break;

                case eTipoDeUnidades.Almacenes:
                    sTitulo = "Generar remisiones de almacenes";
                    sTitulo_Unidades = "Almacenes";
                    this.Name = "FrmRemisionesGenerales_03_Almacenes";
                    break;

                case eTipoDeUnidades.AlmacenesUnidosis:
                    sTitulo = "Generar remisiones de almacenes unidosis";
                    sTitulo_Unidades = "Almacenes unidosis";
                    this.Name = "FrmRemisionesGenerales_04_AlmacenesUnidosis";
                    break;

                default:
                    break;
            }

            this.Text = sTitulo;
            tabPage_05_Unidades.Text = sTitulo_Unidades;

        }

        #region Log 
        private int iTamFile = (1024 * 1024) * 5;
        private string RequestsLogPath = "Log_Remisiones.txt";

        private void validateSize()
        {
            string sFile = Path.Combine(Application.StartupPath, RequestsLogPath);

            if (File.Exists(sFile))
            {
                FileInfo fl = new FileInfo(sFile);
                if (fl.Length >= iTamFile) 
                { 
                    try 
                    { 
                        //// Eliminar el archivo original. 
                        File.Delete(sFile); 
                    } 
                    catch { } 
                }
            }
        }

        public void LogProceso( string Instruccion )
        {
            validateSize();

            StreamWriter writer = new StreamWriter(RequestsLogPath, true);
            writer.WriteLine(String.Format("----{0}", DateTime.Now.ToString()));
            writer.WriteLine("----\tCadena: ");
            writer.WriteLine(Instruccion);
            writer.WriteLine("-----------------------------------------");
            writer.WriteLine();
            writer.Flush();
            writer.Close();
        }
        #endregion Log 

        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();
            bObteniendoRemisiones = false;


            bMostrar = false;
            scTabControlExt1.TabPages[0].Hide();


            SetRemisionesGeneradas(0);

            chk_Procesar_01_Documentos.Checked = true;
            chk_Procesar_02_Facturas.Checked = true;
            chk_Procesar_03_Dispensacion.Checked = true;

            rdo_Ejecutar_01_SI.Checked = true; 


            listaGuids = new List<string>();
            lst.LimpiarItems();
            gridFF.Limpiar(false);
            gridUnidades.Limpiar();
            gridFolios.Limpiar(false); 
            gridProgramas.Limpiar(false);
            gridClavesExclusivas.Limpiar(false);
            gridClavesExcluidas.Limpiar(false);
            gridFacturas.Limpiar(false);
            gridDocumentosComprobacion.Limpiar(false);

            chkEsComplemento.Enabled = false; 
            Frame_08_FacturasAnticipadas.Enabled = false;

            SetEnProceso(false);

            CargarUnidades();
            CargarDocumentosDeComprobacion(false);
            CargarDocumentosFacturas(false);

            btnEjecutar.Enabled = true;
            btnGenerarRemisiones.Enabled = true;
            btnExportarPDF.Enabled = false;
            //Frame_01_InformacionVenta.Enabled = true;

            ////rdo_TP_01_FolioEspecifico.Checked = true; 
            ////chkVentaDirecta.Checked = false;
            ////nmPorcentaje.Text = "0.00";

            ////BloquearControles(true, Frame_01_InformacionVenta);
            txtFactura_Folio.Enabled = false;
            txtFactura_Serie.Enabled = false;

            txtFactura_Folio.Text = "";
            txtFactura_Serie.Text = "";

            //rdoOrdenEjecucionProceso_01.Enabled = false;
            //rdoOrdenEjecucionProceso_02.Enabled = false; 
            //rdoOrdenEjecucionProceso_03.Checked = true;

            SetTipoDeProceso_A_Ejecutar(); 

            ////myGrid.Limpiar(false);
            ////gridUnidades.Limpiar(false);

            SetFrames(true);
            chkEsComplemento.Enabled = false; 
            ////cboAlmacen.Focus();



            scTabControlExt1.SelectTab(tabPage_01_Parametros.Name);
        }

        private void SetTipoDeProceso_A_Ejecutar()
        {
            //FrameOrdenDeEjecucion.Visible = false;
            //FrameOrdenDeEjecucion.Visible = true;

            rdoOrdenEjecucionProceso_01.Enabled = false;
            rdoOrdenEjecucionProceso_02.Enabled = false;
            rdoOrdenEjecucionProceso_03.Enabled = false;

            rdoOrdenEjecucionProceso_01.Checked = false;
            rdoOrdenEjecucionProceso_02.Checked = false;
            rdoOrdenEjecucionProceso_03.Checked = false;

            switch (tipoDeUnidades)
            {
                case eTipoDeUnidades.Farmacias:
                case eTipoDeUnidades.FarmaciasUnidosis:
                case eTipoDeUnidades.Almacenes:
                case eTipoDeUnidades.AlmacenesUnidosis:
                    rdoOrdenEjecucionProceso_03.Checked = true;
                    break;

                    //rdoOrdenEjecucionProceso_02.Checked = true;
                    //break;

                default:
                    break;
            }

        }

        private void SetEnProceso(bool EnProceso)
        {
            if (!EnProceso)
            {
                int iPos = Frame_29_StatusDeProceso.Top - iAlto_Lst_Resultados; 

                Frame_28_RemisionesGeneradas.Height = iAlto_Lst_Resultados;
                Frame_28_RemisionesGeneradas.Height = iAlto_Lst_Resultados + iPos + Frame_29_StatusDeProceso.Height;  
            }
            else
            {
                Frame_28_RemisionesGeneradas.Height = iAlto_Lst_Resultados;
            }

            Application.DoEvents();
        }

        private void SetFrames(bool Habilitar)
        {
            //Frame_02_FuentesFinanciamiento.Enabled = Habilitar;
            //Frame_03_InformacionGeneral.Enabled = Habilitar;
            //Frame_04_TipoRemision.Enabled = Habilitar;
            //Frame_05_OrigenInsumo.Enabled = Habilitar;
            //Frame_06_TipoInsumo.Enabled = Habilitar;
            //Frame_07_OrigenDispensacion.Enabled = Habilitar;
            //Frame_08_FacturasAnticipadas.Enabled = Habilitar;

            BloquearControles(Habilitar, Frame_02_FuentesFinanciamiento);
            BloquearControles(Habilitar, Frame_03_InformacionGeneral);
            BloquearControles(Habilitar, Frame_04_TipoRemision);
            BloquearControles(Habilitar, Frame_05_OrigenInsumo);
            BloquearControles(Habilitar, Frame_06_TipoInsumo);
            BloquearControles(Habilitar, Frame_07_OrigenDispensacion);
            chkEsComplemento.Enabled = false; 

            BloquearControles(Habilitar, Frame_08_FacturasAnticipadas);
            BloquearControles(false, Frame_08_FacturasAnticipadas);
            ////BloquearControles(Habilitar, Frame_09_VentaDirecta);
            BloquearControles(Habilitar, Frame_10_FormatosDeImpresion);
            BloquearControles(Habilitar, Frame_11_RangoDeFechas);

            BloquearControles(Habilitar, Frame_21_ProgramasSubProgramas);
            BloquearControles(Habilitar, Frame_22_ClavesExclusivas);
            BloquearControles(Habilitar, Frame_23_ClavesExcluidas);
            BloquearControles(Habilitar, Frame_24_InformacionAdicional);

            //BloquearControles(Habilitar, Frame_25_Unidades);
            BloquearControles(Habilitar, Frame_29_TipoDeProceso); 
            BloquearControles(Habilitar, Frame_26_Documentos);
            BloquearControles(Habilitar, Frame_27_Facturas);


            chkHabiltarProceso_x_FoliosEspecificos.Enabled = Habilitar; 
            gridUnidades.BloqueaColumna(!Habilitar, Cols_Farmacias.Remisionar);
            gridDocumentosComprobacion.BloqueaColumna(!Habilitar, Cols_Documentos.Procesar);
            gridFacturas.BloqueaColumna(!Habilitar, Cols_Facturas.Procesar);
        }

        private void BloquearControles(bool Habilitar, GroupBox Frame)
        {
            foreach (Control obj in Frame.Controls)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).ReadOnly = !Habilitar;
                }

                if (obj is ComboBox)
                {
                    ((ComboBox)obj).Enabled = Habilitar;
                }

                if (obj is CheckBox)
                {
                    ((CheckBox)obj).Enabled = Habilitar;
                }

                if (obj is DateTimePicker)
                {
                    ((DateTimePicker)obj).Enabled = Habilitar;
                }

                if (obj is NumericUpDown)
                {
                    ((NumericUpDown)obj).Enabled = Habilitar;
                }

                if (obj is Button)
                {
                    ((Button)obj).Enabled = Habilitar;
                }

                if (obj is ToolStrip)
                {
                    ((ToolStrip)obj).Enabled = Habilitar;
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }


        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //////if(validarInformacion_AlmacenFolioDeVenta())
            //////{
            //////    txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
            //////    info = new FrmDescargarVenta(sUrl, sIdEmpresa, sIdEstado, cboAlmacen.Data, txtFolio.Text);
            //////    if (info.Descargar())
            //////    {
            //////        if (InformacioDeVenta())
            //////        {
            //////            SetFrames(true);
            //////            btnEjecutar.Enabled = false;
            //////            btnGenerarRemisiones.Enabled = true;
            //////            chkEsComplemento.Checked = chkOrigenInformacion.Checked;
            //////            chkEsComplemento.Enabled = false; 
            //////            //Frame_01_InformacionVenta.Enabled = false;
            //////            BloquearControles(false, Frame_01_InformacionVenta);
            //////        }
            //////    }
            //////    else
            //////    {
            //////        sMensaje = info.sMensaje;
            //////    }
            //////}
        }

        private void btnGenerarRemisiones_Click(object sender, EventArgs e)
        {
            timerRemisiones.Stop();
            timerRemisiones.Enabled = false;

            toolStripBarraMenu.Enabled = false;
            SetFrames(false);
            SetEnProceso(true);
            Application.DoEvents();

            bEjecutando = true;
            thHilo = new Thread(thGenerarRemisiones);
            thHilo.Name = "GenerarRemisiones";
            thHilo.Start();

            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            Application.DoEvents();
            
            thHilo_RemisionesGeneradas = new Thread(thRemisionesGeneradas);
            thHilo_RemisionesGeneradas.Name = "RemisionesGeneradas";
            thHilo_RemisionesGeneradas.Start();
            Application.DoEvents();

        }

        private void btnNuevo_Documentos_Click(object sender, EventArgs e)
        {
            CargarDocumentosDeComprobacion(true);
        }

        private void btnEjecutar_Documentos_Click(object sender, EventArgs e)
        {
            CargarDocumentosDeComprobacion(true);
        }

        private void btnNuevo_Facturas_Click(object sender, EventArgs e)
        {
            CargarDocumentosFacturas(true);
        }

        private void btnEjecutar_Facturas_Click(object sender, EventArgs e)
        {
            CargarDocumentosFacturas(true);
        }

        private void thGenerarRemisiones()
        {
            bEjecutando = true; 
            if (GenerarRemisiones())
            {
                if (!chkRollBackTransaccion.Checked)
                {
                    General.msjUser("Remisiones generadas satisfactoriamente.");
                }
                else
                {
                    General.msjAviso("Remisiones generadas satisfactoriamente sin aplicar.");
                }

                ////if (chkGenerarDocumentos.Checked)
                ////{
                ////    Imprimir();
                ////}
            }

            toolStripBarraMenu.Enabled = true;
            SetFrames(true);
            Application.DoEvents();
        }

        private void thRemisionesGeneradas()
        {
            while (bEjecutando)
            {
                Application.DoEvents();
                GetRemisionesGeneradas();
                Application.DoEvents();

                System.Threading.Thread.Sleep(1000 * 30); 
            }

            Application.DoEvents();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            bool bregresa = true;

            if (chkGenerarDocumentos.Checked)
            {
                if (cboFormatosDeImpresion.SelectedIndex == 0)
                {
                    bregresa = false;
                    General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                    cboFormatosDeImpresion.Focus();
                }
            }

            if (bregresa)
            {
                Imprimir();
            }
        }

        private void btnNuevoRemisiones_Click(object sender, EventArgs e)
        {
            chkActualizarListadoDeRemisiones.Checked = true;
            lst.LimpiarItems();
        }

        private void btnEjecutar_Remisiones_Click(object sender, EventArgs e)
        {
            //////GetRemisionesGeneradas();
            ////System.Threading.Thread hilo = new Thread(GetRemisionesGeneradas);
            ////hilo.Name = "bgGetRemisionesGeneradas";
            ////hilo.Start();

            GetRemisionesGeneradas(); 
        }

        private void btnExportar_Remisiones_Click(object sender, EventArgs e)
        {
            ExportarExcel();
            //System.Threading.Thread hilo = new Thread(ExportarExcel);
            //hilo.Name = "bgDescargarRemisionesGeneradas";
            //hilo.Start();

        }
        #endregion Botones 

        #region Combos 
        private void CargarFormatosDeImpresion()
        {
            string sSql = string.Format(
                "Select NombreFormato, DescripcionDeUso \n" +
                "From FACT_Remisiones_FormatosDeImpresion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' \n" +
                "Order by Orden ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            cboFormatosDeImpresion.Clear();
            cboFormatosDeImpresion.Add("0", "<< Seleccione >>");

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    cboFormatosDeImpresion.Add(leer.DataSetClase, true, "NombreFormato", "DescripcionDeUso");
                }
            }

            cboFormatosDeImpresion.SelectedIndex = 0;
        }

        private void CargarUnidades()
        {
            string sFiltro = "EsAlmacen = '1' ";
            string sSql = "";

            sStoreDeProceso = " spp_FACT_GenerarRemisiones_x_TipoDeProceso____Farmacias ";

            switch (tipoDeUnidades)
            {
                case eTipoDeUnidades.Farmacias:
                    sFiltro = " EsAlmacen = 0 and EsUnidosis = 0 ";
                    break;

                case eTipoDeUnidades.FarmaciasUnidosis:
                    sFiltro = " EsAlmacen = 0 and EsUnidosis = 1 ";
                    break;

                case eTipoDeUnidades.Almacenes:
                    sFiltro = " EsAlmacen = 1 and EsUnidosis = 0 ";
                    sStoreDeProceso = " spp_FACT_GenerarRemisiones_x_TipoDeProceso_Principal_Almacen ";
                    break;

                case eTipoDeUnidades.AlmacenesUnidosis:
                    sFiltro = " EsAlmacen = 1 and EsUnidosis = 1 ";
                    break;

                default:
                    break;
            }

            sSql = string.Format(
                "Select F.IdFarmacia, F.Farmacia, 0 as Remisionar \n" +
                "From vw_Farmacias F (NoLock) \n" +
                "Inner Join FACT_CFG_Farmacias C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) \n" +
                "Where F.IdEstado = '{0}' and {1} \n" + 
                "Order by F.IdFarmacia ", DtGeneral.EstadoConectado, sFiltro);

            gridUnidades.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarUnidades");
            }
            else
            {
                gridUnidades.LlenarGrid(leer.DataSetClase);
            }
        }

        private void CargarDocumentosDeComprobacion(bool MensajeVacio)
        {
            string sSql = ""; 
            int iTipoDeUnidades = tipoDeUnidades == eTipoDeUnidades.Almacenes || tipoDeUnidades == eTipoDeUnidades.Farmacias ? 1 : 2;  


            sSql = string.Format("Exec spp_FACT_INFO_Comprobacion_Documentos \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeUnidades = '{3}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeUnidades); 

            gridDocumentosComprobacion.Limpiar(false); 
            if (!leer.Exec(sSql)) 
            {
                base.Error.GrabarError(this.leer, "CargarDocumentosDeComprobacion"); 
                General.msjError("Ocurrió un error al obtener la información de Documentos a comprobar."); 
            }
            else 
            {
                if (!leer.Leer()) 
                { 
                    if (MensajeVacio) General.msjUser("No existen documentos para comprobar."); 
                } 
                else 
                { 
                    gridDocumentosComprobacion.LlenarGrid(this.leer.DataSetClase, false, false); 
                } 
            }

        }

        private void CargarDocumentosFacturas(bool MensajeVacio)
        {
            string sSql = "";
            int iTipoDeUnidades = tipoDeUnidades == eTipoDeUnidades.Almacenes || tipoDeUnidades == eTipoDeUnidades.Farmacias ? 1 : 2;


            sSql = string.Format("Exec spp_FACT_INFO_Comprobacion_Facturas \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeUnidades = '{3}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeUnidades);

            gridFacturas.Limpiar(false);
            if (!this.leer.Exec(sSql))
            {
                base.Error.GrabarError(this.leer, "CargarDocumentosFacturas");
                General.msjError("Ocurrió un error al obtener la información de Facturas.");
            }
            else
            {
                if (!leer.Leer())
                {
                    if (MensajeVacio) General.msjUser("No existen documentos para comprobar.");
                }
                else
                {
                    gridFacturas.LlenarGrid(this.leer.DataSetClase, false, false);
                }
            }
        }
        private void CargarNivelDeInformacion()
        {
            //  ----   1 => General (Primer nivel de informacion) | 2 ==> Farmacia FF (Segundo nivel de informacion) | 3 ==> Ventas directas por Jurisdiccion  

            cboNivelDeInformacion.Clear();
            cboNivelDeInformacion.Add("0", "<< Seleccione >>");
            cboNivelDeInformacion.Add("1", "General (Primer nivel de informacion)");
            cboNivelDeInformacion.Add("2", "Farmacia FF (Segundo nivel de informacion)");
            cboNivelDeInformacion.Add("3", "Ventas directas por Jurisdiccion");

            cboNivelDeInformacion.SelectedIndex = 0;
        }

        private void CargarTiposDeBeneficiarios()
        {
            // ----   0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén>

            cboTipoDeBeneficiarios.Clear();
            cboTipoDeBeneficiarios.Add("00", "<< Seleccione >>");
            cboTipoDeBeneficiarios.Add("0", "Todos");
            cboTipoDeBeneficiarios.Add("1", "General <Solo farmacia>");
            cboTipoDeBeneficiarios.Add("2", "Hospitales <Solo almacén>");
            cboTipoDeBeneficiarios.Add("3", "Jurisdicciones <Solo almacén>");

            cboTipoDeBeneficiarios.SelectedIndex = 0;
        }

        private void CargarOrigenDeDispensacion()
        {
            // ---- 0 ==> Todo | 1 ==> Ventas (Excluir Vales) | 2 ==> Vales ( Ventas originadas de un vale )  

            cboOrigenDispensacion.Clear();
            cboOrigenDispensacion.Add("00", "<< Seleccione >>");
            cboOrigenDispensacion.Add("0", "Dispensación y Vales");
            cboOrigenDispensacion.Add("1", "Dispensación (Excluir Vales)");
            cboOrigenDispensacion.Add("2", "Vales ( Ventas originadas de un vale ) ");

            cboOrigenDispensacion.SelectedIndex = 0;
        }
        #endregion Combos 

        #region Controles 
        #region Buscar Rubro
        private void txtRubro_TextChanged(object sender, EventArgs e)
        {
            lblRubro.Text = "";
            txtConcepto.Text = "";
            lblConcepto.Text = "";
            lblIdCliente.Text = "";
            lblCliente.Text = "";
            lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtRubro_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            bEsExcedente_Rubro = false;
            if (txtRubro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtRubro.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    e.Cancel =  !CargarDatosRubro();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtRubro.Text = "";
                    lblRubro.Text = "";
                    txtRubro.Focus();
                }
            }
        }

        private bool CargarDatosRubro()
        {
            bool bRegresa = true;
            string sMensaje = ""; 

            if (tipoDeUnidades == eTipoDeUnidades.Almacenes || tipoDeUnidades == eTipoDeUnidades.Farmacias)
            {
                if (leer.CampoInt("TipoDeUnidades") != 1)
                {
                    bRegresa = false;
                    sMensaje = "La fuente de financiamiento no es para unidades ordinarias."; 
                }
            }

            if (tipoDeUnidades == eTipoDeUnidades.AlmacenesUnidosis || tipoDeUnidades == eTipoDeUnidades.FarmaciasUnidosis)
            {
                if (leer.CampoInt("TipoDeUnidades") != 2)
                {
                    bRegresa = false;
                    sMensaje = "La fuente de financiamiento no es para unidades de dosis unitaria.";
                }
            }


            if (!bRegresa)
            {
                General.msjError(sMensaje);
                txtRubro.Text = ""; 
            }
            else
            {
                CargarDatosRubro_Agregar(); 
            }

            return bRegresa; 

        }

        private void CargarDatosRubro_Agregar()
        {
            txtRubro.Text = leer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");

            lblIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");

            sTipoDe_FuenteDeFinanciamiento = leer.Campo("EsParaExcedente_Descripcion");
            bEsDiferencial = leer.CampoBool("EsDiferencial");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtRubro.Text = "";
                lblRubro.Text = "";
            }

            ////Obtener_ListaProgramasAtencion();
        }

        private void txtRubro_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtConcepto_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            dMontoFinanciamiento = 0.0000;
            bEsExcedente_Concepto = false;
            if (txtConcepto.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosConcepto();
                    ////ObtenerMontoPorAplicar();
                }
                else
                {
                    txtConcepto.Text = "";
                    lblConcepto.Text = "";
                    txtConcepto.Focus();
                }
            }
        }

        private void CargarDatosConcepto()
        {
            txtConcepto.Text = leer.Campo("IdFinanciamiento");
            lblConcepto.Text = leer.Campo("Financiamiento");
            dMontoFinanciamiento = leer.CampoDouble("MontoDetalle");
            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtConcepto.Text = "";
                lblConcepto.Text = "";
            }

            ////if(ValidarClaves_Financiamiento())
            ////{
            ////    btnEjecutar.Enabled = false;
            ////}
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            lblConcepto.Text = "";
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtRubro.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }
        }
        #endregion Buscar Concepto

        #region Buscar Programa
        #endregion Buscar Programa

        #region Buscar SubPrograma
        #endregion Buscar SubPrograma
        #endregion Controles 

        #region Generar remisiones 
        private bool ValidarDatos()
        {
            bool bregresa = true;


            if (gridFF.Rows == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado las Fuentes de financiamiento a procesar, verifique.");
                txtRubro.Focus();
            }

            if (cboNivelDeInformacion.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Nivel de información inválida, verifique.");
                cboNivelDeInformacion.Focus();
            }

            return bregresa;
        }
        private bool GenerarRemisiones()
        {
            bool bRegresa = false;
            string sMensaje = "";
            string sSql = "";
            int iOrdenDeEjecucion = 0;

            bEjecutando = true;
            btnGenerarRemisiones.Enabled = false;
            //listaGuids = new List<string>();

            iOrdenDeEjecucion = rdoOrdenEjecucionProceso_01.Checked ? 1 : 2;
            iOrdenDeEjecucion = rdoOrdenEjecucionProceso_02.Checked ? 2 : 1;
            iOrdenDeEjecucion = rdoOrdenEjecucionProceso_03.Checked ? 3 : iOrdenDeEjecucion;

            if (!cnn.Abrir())
            {
                btnGenerarRemisiones.Enabled = true;
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                scTabControlExt1.SelectTab(tabPage_08_RemisionesGeneradas.Name);
                Application.DoEvents();

                cnn.IniciarTransaccion();
                sGUID = Guid.NewGuid().ToString();   // Referencia unica para todas las remisiones generadas

                listaGuids.Add(sGUID);

                //bRegresa = leer.Exec(sSql);
                if (GenerarRemisiones___ProcesarRemisiones(iOrdenDeEjecucion))
                {
                    bRegresa = GenerarRemisiones_11_ActualizarInformacionAdicional();
                }

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "GenerarRemisiones");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al generar las remisiones.");
                    btnGenerarRemisiones.Enabled = true;
                }
                else
                {
                    if (!VerificarRemisionesGeneradas())
                    {
                        cnn.DeshacerTransaccion();

                        scTabControlExt1.SelectTab(tabPage_01_Parametros.Name);
                        Application.DoEvents();

                        General.msjAviso("No se generaron remisiones con los criterios especificados.");
                        btnGenerarRemisiones.Enabled = true;
                        bRegresa = false;
                    }
                    else
                    {
                        if (!chkRollBackTransaccion.Checked)
                        {
                            cnn.CompletarTransaccion();
                        }
                        else
                        {
                            cnn.DeshacerTransaccion(); 
                        }
                    }
                }

                cnn.Cerrar();
            }

            SetEnProceso(false);
            btnGenerarRemisiones.Enabled = true;

            bEjecutando = false;

            return bRegresa;

        }

        private bool GenerarRemisiones_11_ActualizarInformacionAdicional()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_FACT_Remisiones_InformacionAdicional \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @GUID = '{3}', \n" +
                "\t@Info_01 = '{4}', @Info_02 = '{5}', @Info_03 = '{6}', @Info_04 = '{7}', @Info_05 = '{8}'\n" +
                "",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID,
                txtInfoAdicional_01.Text.Trim(), txtInfoAdicional_02.Text.Trim(), txtInfoAdicional_03.Text.Trim(),
                txtInfoAdicional_04.Text.Trim(), txtInfoAdicional_05.Text.Trim()
                );

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private void timerRemisiones_Tick(object sender, EventArgs e)
        {
            timerRemisiones.Stop();
            timerRemisiones.Enabled = false;

            //if (bEjecutando)
            //{
            //    GetRemisionesGeneradas();
            //}
            //else
            //{
            //    timerRemisiones.Enabled = true;
            //    timerRemisiones.Start();
            //}
        }

        private void GetRemisionesGeneradas()
        {
            if (chkActualizarListadoDeRemisiones.Checked)
            {
                if (!bObteniendoRemisiones)
                {
                    bObteniendoRemisiones = true; 

                    GetRemisionesGeneradas_Interno();

                    bObteniendoRemisiones = false; 
                }
            }
        }

        private void GetRemisionesGeneradas_Interno()
        {
            string sSql = "";
            string sListaGUIDS = GetLista_GUIDS();

            sSql = string.Format("Exec spp_FACT_RPT_RemisionesGeneradas @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @Identificador_UUID = ' {3} ' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sListaGUIDS);

            btnNuevoRemisiones.Enabled = false;
            btnEjecutar_Remisiones.Enabled = false;
            btnExportar_Remisiones.Enabled = false;

            if (!leerExportarExcel.Exec(sSql))
            {
                Error.GrabarError(leerExportarExcel, "GetRemisionesGeneradas");
            }
            else
            {
                if (leerExportarExcel.Leer())
                {
                    lst.LimpiarItems();
                    lst.CargarDatos(leerExportarExcel.DataSetClase, true, true);
                    Application.DoEvents();
                }
            }

            btnNuevoRemisiones.Enabled = leerExportarExcel.Registros > 0;
            btnEjecutar_Remisiones.Enabled = btnNuevoRemisiones.Enabled;
            btnExportar_Remisiones.Enabled = btnNuevoRemisiones.Enabled;

            SetRemisionesGeneradas(lst.Registros); 
            //lblNumeroDeRemisionesGeneradas.Text = string.Format("{0} remisiones generadas", lst.Registros);
        }

        private void SetRemisionesGeneradas(int Valor)
        {
            lblNumeroDeRemisionesGeneradas.Text = string.Format("{0} remisiones generadas", Valor.ToString("#,###,###,##0"));
        }
        private bool GenerarRemisiones___ProcesarRemisiones(int OrdenDeEjecucion)
        {
            bool bRegresa = false;

            if (chkHabiltarProceso_x_FoliosEspecificos.Checked)
            {
                OrdenDeEjecucion = 3; 
            }

            /// Desmarcar todos los renglones 
            gridUnidades.SetValue(Cols_Farmacias.Procesada, 0);

            if (OrdenDeEjecucion == 1)
            {
                bRegresa = GenerarRemisiones_01_ProcesarRemisiones(); 
            }

            if (OrdenDeEjecucion == 2)
            {
                bRegresa = GenerarRemisiones_02_Procesar_x_Farmacias();
            }

            if (OrdenDeEjecucion == 3)
            {
                bRegresa = GenerarRemisiones_03_Procesar_x_Farmacias_Masivo();
            }

            return bRegresa; 
        }

        private bool GenerarRemisiones_01_ProcesarRemisiones()
        {
            bool bRegresa = true;
            string sSql = "";

            sDato_07_FoliosDeVenta = ""; 
            sDato_08_FechaInicial = General.FechaYMD(dtpFechaInicial.Value);
            sDato_09_FechaFinal = General.FechaYMD(dtpFechaFinal.Value);
            sDato_10_ProgramasDeAtencion = GetLista_ProgramasDeAtencion();
            sDato_19_ListaClavesExclusivas = GetLista_Claves(gridClavesExclusivas);
            sDato_20_ListaClavesExcluidas = GetLista_Claves(gridClavesExcluidas);


            //// Procesar por Fuente de Financiamiento 
            for (int i = 1; i <= gridFF.Rows; i++)
            {
                sDato_01_FuenteFinanciamiento = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                sDato_02_Financiamiento = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                sDato_03_IdCliente = gridFF.GetValue(i, Cols_FF.IdCliente);
                sDato_04_IdSubCliente = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();
                sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.AplicarDiferencial).ToString(); 

                if (bRegresa)
                {
                    //// Procesar Unidades 
                    for (int j = 1; j <= gridUnidades.Rows; j++)
                    {
                        if (gridUnidades.GetValueBool(j, Cols_Farmacias.Remisionar))
                        {
                            sDato_06_IdFarmacia = gridUnidades.GetValue(j, Cols_Farmacias.IdFarmacia);

                            #region Proceso de remisiones 

                            #region Documentos de comprobacion 
                            if (bRegresa && gridDocumentosComprobacion.Rows > 0)
                            {
                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                for (int iDocumentos = 1; iDocumentos <= gridDocumentosComprobacion.Rows; iDocumentos++)
                                {
                                    if (gridDocumentosComprobacion.GetValueBool(iDocumentos, Cols_Documentos.Procesar))
                                    {
                                        sDato_18_DocumentoDeComprobacion = gridDocumentosComprobacion.GetValue(iDocumentos, Cols_Documentos.FolioRelacion);

                                        iDatos_21_Procesa_Venta = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Venta);
                                        iDatos_22_Procesa_Consigna = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Consigna);
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = 1;
                                        iDatos_26_Procesa_MaterialDeCuracion = 1;

                                        //// Procesar remisiones relacionadas a documentos de comprobación ( Excepciones generales ) 
                                        sSql = PrepararParametrosRemisiones_DocumentosDeComprobacion
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_18_DocumentoDeComprobacion,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );
                                        if (!leer.Exec(sSql))
                                        {
                                            bRegresa = false;
                                            break;
                                        }
                                        GetRemisionesGeneradas();
                                    }
                                }
                            }
                            #endregion Documentos de comprobacion 

                            #region Relacion de facturas previas  
                            if (bRegresa && gridFacturas.Rows > 0)
                            {
                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";


                                //// Procesar remisiones relacionadas a facturas manuales 
                                for (int iFacturas = 1; iFacturas <= gridFacturas.Rows; iFacturas++)
                                {
                                    if (gridDocumentosComprobacion.GetValueBool(iFacturas, Cols_Documentos.Procesar))
                                    {
                                        sDato_11_EsRelacionFactura = "1";
                                        sDato_12_Serie = gridFacturas.GetValue(iFacturas, Cols_Facturas.Serie);
                                        sDato_13_Folio = gridFacturas.GetValue(iFacturas, Cols_Facturas.Folio);

                                        sDato_14_FacturaEnCajas = "0";
                                        sDato_15_RelacionPorMontos = "0";
                                        sDato_16_ProcesarSoloClavesConReferencias = "0";

                                        iDatos_21_Procesa_Venta = 1;
                                        iDatos_22_Procesa_Consigna = 0;
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Medicamento); ;
                                        iDatos_26_Procesa_MaterialDeCuracion = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesar); ;

                                        sSql = PrepararParametrosRemisiones_FacturasRelacionadas
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_12_Serie, sDato_13_Folio, sDato_14_FacturaEnCajas,
                                                sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );

                                        if (!leer.Exec(sSql))
                                        {
                                            bRegresa = false;
                                            break;
                                        }
                                        GetRemisionesGeneradas();
                                    }
                                }
                            }
                            #endregion Relacion de facturas previas  

                            #region Remisiones normales   
                            if (bRegresa)
                            {
                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                iDatos_21_Procesa_Venta = chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
                                iDatos_22_Procesa_Consigna = chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;
                                iDatos_23_Procesa_Producto = chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
                                iDatos_24_Procesa_Servicio = chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
                                iDatos_25_Procesa_Medicamento = chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
                                iDatos_26_Procesa_MaterialDeCuracion = chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;

                                //// Procesar remisiones normales 
                                sSql = PrepararParametrosRemisiones
                                    (
                                    sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                    sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                    sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                    sDato_11_EsRelacionFactura, sDato_12_Serie, sDato_13_Folio,
                                    sDato_14_FacturaEnCajas, sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                    sDato_17_EsDocumentoDeComprobacion, sDato_18_DocumentoDeComprobacion,
                                    sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                    iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                    iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                    );

                                if (!leer.Exec(sSql))
                                {
                                    bRegresa = false;
                                    break;
                                }
                                GetRemisionesGeneradas();
                            }
                            #endregion Remisiones normales   

                            #endregion Proceso de remisiones 

                        }

                        //GetRemisionesGeneradas();

                        //// verificar si ocurrio algun error 
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }

                //// verificar si ocurrio algun error 
                if (!bRegresa)
                {
                    break;
                }
            }

            return bRegresa;
        }

        private bool GenerarRemisiones_02_Procesar_x_Farmacias()
        {
            bool bRegresa = true;
            string sSql = "";
            string sSql_01_Documentos = "";
            string sSql_02_Facturas = "";
            string sSql_03_RemisionesGenerales = "";
            string sSql_99_Concentrado = "";

            sDato_07_FoliosDeVenta = ""; 
            sDato_08_FechaInicial = General.FechaYMD(dtpFechaInicial.Value);
            sDato_09_FechaFinal = General.FechaYMD(dtpFechaFinal.Value);
            sDato_10_ProgramasDeAtencion = GetLista_ProgramasDeAtencion();
            sDato_19_ListaClavesExclusivas = GetLista_Claves(gridClavesExclusivas);
            sDato_20_ListaClavesExcluidas = GetLista_Claves(gridClavesExcluidas);

            //// Procesar Unidades 
            for (int j = 1; j <= gridUnidades.Rows && bRegresa; j++)
            {
                if (gridUnidades.GetValueBool(j, Cols_Farmacias.Remisionar))
                {
                    sDato_06_IdFarmacia = gridUnidades.GetValue(j, Cols_Farmacias.IdFarmacia);

                    lblUnidadEnProceso.Text = string.Format(" {0} -- {1} ", sDato_06_IdFarmacia, gridUnidades.GetValue(j, Cols_Farmacias.Farmacia)); 

                    sSql_01_Documentos = "";
                    sSql_02_Facturas = "";
                    sSql_03_RemisionesGenerales = "";
                    sSql_99_Concentrado = ""; 

                    if (bRegresa)
                    {
                        #region Fuentes de Financiamiento 
                        //// Procesar por Fuente de Financiamiento 
                        for (int i = 1; i <= gridFF.Rows; i++)
                        {
                            sDato_01_FuenteFinanciamiento = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                            sDato_02_Financiamiento = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                            sDato_03_IdCliente = gridFF.GetValue(i, Cols_FF.IdCliente);
                            sDato_04_IdSubCliente = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                            sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();
                            sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.AplicarDiferencial).ToString(); 

                            #region Proceso de remisiones 
                            //sSql_01_Documentos = "";
                            //sSql_02_Facturas = "";
                            //sSql_03_RemisionesGenerales = "";


                            #region Documentos de comprobacion 
                            if (bRegresa && gridDocumentosComprobacion.Rows > 0)
                            {
                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                for (int iDocumentos = 1; iDocumentos <= gridDocumentosComprobacion.Rows; iDocumentos++)
                                {
                                    if (gridDocumentosComprobacion.GetValueBool(iDocumentos, Cols_Documentos.Procesar))
                                    {
                                        sDato_18_DocumentoDeComprobacion = gridDocumentosComprobacion.GetValue(iDocumentos, Cols_Documentos.FolioRelacion);

                                        iDatos_21_Procesa_Venta = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Venta);
                                        iDatos_22_Procesa_Consigna = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Consigna);
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = 1;
                                        iDatos_26_Procesa_MaterialDeCuracion = 1;

                                        //// Procesar remisiones relacionadas a documentos de comprobación ( Excepciones generales ) 
                                        sSql = PrepararParametrosRemisiones_DocumentosDeComprobacion
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_18_DocumentoDeComprobacion,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );

                                        sSql_01_Documentos += string.Format("{0}\n\n\n", sSql);
                                        ////if (!leer.Exec(sSql))
                                        ////{
                                        ////    bRegresa = false;
                                        ////    break;
                                        ////}
                                        ////GetRemisionesGeneradas();
                                    }
                                }

                                ////if (!leer.Exec(sSql_01_Documentos))
                                ////{
                                ////    bRegresa = false;
                                ////    ////break;
                                ////}
                                ////else
                                ////{
                                ////    GetRemisionesGeneradas();
                                ////}
                            }
                            #endregion Documentos de comprobacion 

                            #region Relacion de facturas previas  
                            if (bRegresa && gridFacturas.Rows > 0)
                            {
                                //sSql_02_Facturas = "";

                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";


                                //// Procesar remisiones relacionadas a facturas manuales 
                                for (int iFacturas = 1; iFacturas <= gridFacturas.Rows; iFacturas++)
                                {
                                    if (gridDocumentosComprobacion.GetValueBool(iFacturas, Cols_Documentos.Procesar))
                                    {
                                        sDato_11_EsRelacionFactura = "1";
                                        sDato_12_Serie = gridFacturas.GetValue(iFacturas, Cols_Facturas.Serie);
                                        sDato_13_Folio = gridFacturas.GetValue(iFacturas, Cols_Facturas.Folio);

                                        sDato_14_FacturaEnCajas = "0";
                                        sDato_15_RelacionPorMontos = "0";
                                        sDato_16_ProcesarSoloClavesConReferencias = "0";

                                        iDatos_21_Procesa_Venta = 1;
                                        iDatos_22_Procesa_Consigna = 0;
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesa_Medicamento); ;
                                        iDatos_26_Procesa_MaterialDeCuracion = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Facturas.Procesar); ;

                                        sSql = PrepararParametrosRemisiones_FacturasRelacionadas
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_12_Serie, sDato_13_Folio, sDato_14_FacturaEnCajas,
                                                sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );

                                        sSql_02_Facturas += string.Format("{0}\n\n\n", sSql);
                                        //if (!leer.Exec(sSql))
                                        //{
                                        //    bRegresa = false;
                                        //    break;
                                        //}
                                        //GetRemisionesGeneradas();
                                    }
                                }

                                ////if (!leer.Exec(sSql_02_Facturas))
                                ////{
                                ////    bRegresa = false;
                                ////    ////break;
                                ////}
                                ////else
                                ////{
                                ////    GetRemisionesGeneradas();
                                ////}
                            }
                            #endregion Relacion de facturas previas  

                            #region Remisiones normales   
                            if (bRegresa)
                            {
                                //sSql_03_RemisionesGenerales = "";

                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                iDatos_21_Procesa_Venta = chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
                                iDatos_22_Procesa_Consigna = chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;
                                iDatos_23_Procesa_Producto = chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
                                iDatos_24_Procesa_Servicio = chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
                                iDatos_25_Procesa_Medicamento = chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
                                iDatos_26_Procesa_MaterialDeCuracion = chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;

                                //// Procesar remisiones normales 
                                sSql = PrepararParametrosRemisiones
                                    (
                                    sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                    sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                    sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                    sDato_11_EsRelacionFactura, sDato_12_Serie, sDato_13_Folio,
                                    sDato_14_FacturaEnCajas, sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                    sDato_17_EsDocumentoDeComprobacion, sDato_18_DocumentoDeComprobacion,
                                    sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                    iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                    iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                    );

                                sSql_03_RemisionesGenerales += string.Format("{0}\n\n\n", sSql);
                                //if (!leer.Exec(sSql))
                                //{
                                //    bRegresa = false;
                                //    ////break;
                                //}
                                //else
                                //{
                                //    GetRemisionesGeneradas();
                                //}
                            }
                            #endregion Remisiones normales   

                            #endregion Proceso de remisiones 

                            //// verificar si ocurrio algun error 
                            if (!bRegresa)
                            {
                                break;
                            }
                        }
                        #endregion Fuentes de Financiamiento 


                        #region Ejecución concentrada por farmacia 

                        //sSql_99_Concentrado = string.Format("{0}\n\n\n{1}\n\n\n{2}\n\n\n\n", sSql_01_Documentos, sSql_02_Facturas, sSql_03_RemisionesGenerales);

                        sSql_99_Concentrado = string.Format("{0}\n{1}\n\n\n{0}\n\n\n", "---- Comprobación de documentos ", sSql_01_Documentos);
                        sSql_99_Concentrado += string.Format("{0}\n{1}\n\n\n{0}\n\n\n", "---- Facturas anticipadas ", sSql_02_Facturas);
                        sSql_99_Concentrado += string.Format("{0}\n{1}\n\n\n{0}\n\n\n", "---- Remisiones generales ", sSql_03_RemisionesGenerales);


                        sSql_99_Concentrado = "";
                        if (sSql_01_Documentos != "")
                        {
                            sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n\n\n{0}", "---- Comprobación de documentos ", sSql_01_Documentos);
                        }

                        if (sSql_02_Facturas != "")
                        {
                            sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n\n\n{0}", "---- Facturas anticipadas ", sSql_02_Facturas);
                        }

                        if (sSql_03_RemisionesGenerales != "")
                        {
                            sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n\n\n{0}", "---- Remisiones generales ", sSql_03_RemisionesGenerales);
                        }


                        if (!leer.Exec(sSql_99_Concentrado))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            GetRemisionesGeneradas();
                        }
                        #endregion Ejecución concentrada por farmacia 

                        gridUnidades.SetValue(j, Cols_Farmacias.Procesada, 1);
                    }
                }

                //GetRemisionesGeneradas();

                //// verificar si ocurrio algun error 
                if (!bRegresa)
                {
                    break;
                }
            }

            return bRegresa;
        }

        private bool GenerarRemisiones_03_Procesar_x_Farmacias_Masivo()
        {
            bool bRegresa = true;
            string sSql = "";
            string sSql_01_Documentos = "";
            string sSql_02_Facturas = "";
            string sSql_03_RemisionesGenerales = "";
            string sSql_99_Concentrado = "";
            bool bProcesar_Remisiones_General = true; 


            if (chkHabiltarProceso_x_FoliosEspecificos.Checked)
            {
                sDato_06_IdFarmacia = string.Format("''{0}''", txtIdFarmacia.Text);
                sDato_07_FoliosDeVenta = GetLista_FoliosFarmacia();
            }
            else
            {
                sDato_07_FoliosDeVenta = ""; 
                sDato_06_IdFarmacia = GetLista_Farmacias();
            }


            sDato_08_FechaInicial = General.FechaYMD(dtpFechaInicial.Value);
            sDato_09_FechaFinal = General.FechaYMD(dtpFechaFinal.Value);
            sDato_10_ProgramasDeAtencion = GetLista_ProgramasDeAtencion();
            sDato_19_ListaClavesExclusivas = GetLista_Claves(gridClavesExclusivas);
            sDato_20_ListaClavesExcluidas = GetLista_Claves(gridClavesExcluidas);

            //// Procesar Unidades 
            ////for (int j = 1; j <= gridUnidades.Rows && bRegresa; j++)
            {
                ////if (gridUnidades.GetValueBool(j, Cols_Farmacias.Remisionar))
                {
                    //sDato_06_IdFarmacia = gridUnidades.GetValue(j, Cols_Farmacias.IdFarmacia);

                    lblUnidadEnProceso.Text = "";  //string.Format(" {0} -- {1} ", sDato_06_IdFarmacia, gridUnidades.GetValue(j, Cols_Farmacias.Farmacia));

                    if (bRegresa)
                    {
                        #region Fuentes de Financiamiento 
                        //// Procesar por Fuente de Financiamiento 
                        for (int i = 1; i <= gridFF.Rows; i++)
                        {
                            sDato_01_FuenteFinanciamiento = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                            sDato_02_Financiamiento = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                            sDato_03_IdCliente = gridFF.GetValue(i, Cols_FF.IdCliente);
                            sDato_04_IdSubCliente = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                            sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();
                            sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.AplicarDiferencial).ToString();

                            sSql_01_Documentos = "";
                            sSql_02_Facturas = "";
                            sSql_03_RemisionesGenerales = "";
                            sSql_99_Concentrado = "";

                            lblUnidadEnProceso.Text = string.Format(" {0} -- {1} ", sDato_02_Financiamiento, gridFF.GetValue(i, Cols_FF.Financiamiento));

                            #region Proceso de remisiones 
                            //sSql_01_Documentos = "";
                            //sSql_02_Facturas = "";
                            //sSql_03_RemisionesGenerales = "";


                            #region Documentos de comprobacion 
                            if (bRegresa && gridDocumentosComprobacion.Rows > 0)
                            {
                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                for (int iDocumentos = 1; iDocumentos <= gridDocumentosComprobacion.Rows; iDocumentos++)
                                {
                                    if (gridDocumentosComprobacion.GetValueBool(iDocumentos, Cols_Documentos.Procesar))
                                    {
                                        sDato_18_DocumentoDeComprobacion = gridDocumentosComprobacion.GetValue(iDocumentos, Cols_Documentos.FolioRelacion);

                                        iDatos_21_Procesa_Venta = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Venta);
                                        iDatos_22_Procesa_Consigna = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Consigna);
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iDocumentos, Cols_Documentos.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = 1;
                                        iDatos_26_Procesa_MaterialDeCuracion = 1;

                                        //// Procesar remisiones relacionadas a documentos de comprobación ( Excepciones generales ) 
                                        sSql = PrepararParametrosRemisiones_DocumentosDeComprobacion
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_18_DocumentoDeComprobacion,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );

                                        sSql_01_Documentos += string.Format("\n{0}", sSql);
                                        ////if (!leer.Exec(sSql))
                                        ////{
                                        ////    bRegresa = false;
                                        ////    break;
                                        ////}
                                        ////GetRemisionesGeneradas();
                                    }
                                }

                                ////if (!leer.Exec(sSql_01_Documentos))
                                ////{
                                ////    bRegresa = false;
                                ////    ////break;
                                ////}
                                ////else
                                ////{
                                ////    GetRemisionesGeneradas();
                                ////}
                            }
                            #endregion Documentos de comprobacion 

                            #region Relacion de facturas previas  
                            if (bRegresa && gridFacturas.Rows > 0)
                            {
                                //sSql_02_Facturas = "";

                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";


                                //// Procesar remisiones relacionadas a facturas manuales 
                                for (int iFacturas = 1; iFacturas <= gridFacturas.Rows; iFacturas++)
                                {
                                    if (gridFacturas.GetValueBool(iFacturas, Cols_Facturas.Procesar))
                                    {
                                        sDato_11_EsRelacionFactura = "1";
                                        sDato_12_Serie = gridFacturas.GetValue(iFacturas, Cols_Facturas.Serie);
                                        sDato_13_Folio = gridFacturas.GetValueInt(iFacturas, Cols_Facturas.Folio).ToString();

                                        sDato_14_FacturaEnCajas = "0";
                                        sDato_15_RelacionPorMontos = "0";
                                        sDato_16_ProcesarSoloClavesConReferencias = "0";

                                        iDatos_21_Procesa_Venta = 1;
                                        iDatos_22_Procesa_Consigna = 0;
                                        iDatos_23_Procesa_Producto = gridFacturas.GetValueInt(iFacturas, Cols_Facturas.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridFacturas.GetValueInt(iFacturas, Cols_Facturas.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = gridFacturas.GetValueInt(iFacturas, Cols_Facturas.Procesa_Medicamento); 
                                        iDatos_26_Procesa_MaterialDeCuracion = gridFacturas.GetValueInt(iFacturas, Cols_Facturas.Procesar); ;

                                        sSql = PrepararParametrosRemisiones_FacturasRelacionadas
                                            (
                                                sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                                sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                                sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                                sDato_12_Serie, sDato_13_Folio, sDato_14_FacturaEnCajas,
                                                sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                                sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                                iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                                iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                            );

                                        sSql_02_Facturas += string.Format("\n{0}", sSql);
                                        //if (!leer.Exec(sSql))
                                        //{
                                        //    bRegresa = false;
                                        //    break;
                                        //}
                                        //GetRemisionesGeneradas();
                                    }
                                }

                                ////if (!leer.Exec(sSql_02_Facturas))
                                ////{
                                ////    bRegresa = false;
                                ////    ////break;
                                ////}
                                ////else
                                ////{
                                ////    GetRemisionesGeneradas();
                                ////}
                            }
                            #endregion Relacion de facturas previas  

                            #region Remisiones normales   
                            if (bRegresa)
                            {
                                //sSql_03_RemisionesGenerales = "";

                                //// Resetear parámetros 
                                sDato_11_EsRelacionFactura = "0";
                                sDato_12_Serie = "";
                                sDato_13_Folio = "";
                                sDato_14_FacturaEnCajas = "0";
                                sDato_15_RelacionPorMontos = "0";
                                sDato_16_ProcesarSoloClavesConReferencias = "0";
                                sDato_17_EsDocumentoDeComprobacion = "0";
                                sDato_18_DocumentoDeComprobacion = "";

                                iDatos_21_Procesa_Venta = chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
                                iDatos_22_Procesa_Consigna = chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;
                                iDatos_23_Procesa_Producto = chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
                                iDatos_24_Procesa_Servicio = chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
                                iDatos_25_Procesa_Medicamento = chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
                                iDatos_26_Procesa_MaterialDeCuracion = chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;

                                //// Procesar remisiones normales 
                                sSql = PrepararParametrosRemisiones
                                    (
                                    sDato_01_FuenteFinanciamiento, sDato_02_Financiamiento, Convert.ToInt32(sDato_05_EsDiferencial),
                                    sDato_03_IdCliente, sDato_04_IdSubCliente, sDato_06_IdFarmacia, sDato_07_FoliosDeVenta,
                                    sDato_08_FechaInicial, sDato_09_FechaFinal, sDato_10_ProgramasDeAtencion,
                                    sDato_11_EsRelacionFactura, sDato_12_Serie, sDato_13_Folio,
                                    sDato_14_FacturaEnCajas, sDato_15_RelacionPorMontos, sDato_16_ProcesarSoloClavesConReferencias,
                                    sDato_17_EsDocumentoDeComprobacion, sDato_18_DocumentoDeComprobacion,
                                    sDato_19_ListaClavesExclusivas, sDato_20_ListaClavesExcluidas,
                                    iDatos_21_Procesa_Venta, iDatos_22_Procesa_Consigna, iDatos_23_Procesa_Producto, iDatos_24_Procesa_Servicio,
                                    iDatos_25_Procesa_Medicamento, iDatos_26_Procesa_MaterialDeCuracion
                                    );

                                sSql_03_RemisionesGenerales += string.Format("\n{0}", sSql);
                                //if (!leer.Exec(sSql))
                                //{
                                //    bRegresa = false;
                                //    ////break;
                                //}
                                //else
                                //{
                                //    GetRemisionesGeneradas();
                                //}
                            }
                            #endregion Remisiones normales   

                            #endregion Proceso de remisiones 

                            #region Ejecución concentrada por Fuente de Financiamiento 

                            sSql_99_Concentrado = string.Format("{0}\n{1}\n{0}\n\n\n", "---- Comprobación de documentos ", sSql_01_Documentos);
                            sSql_99_Concentrado += string.Format("{0}\n{1}\n{0}\n\n\n", "---- Facturas anticipadas ", sSql_02_Facturas);
                            sSql_99_Concentrado += string.Format("{0}\n{1}\n{0}\n\n\n", "---- Remisiones generales ", sSql_03_RemisionesGenerales);


                            if (!chk_Procesar_01_Documentos.Checked) sSql_01_Documentos = "";
                            if (!chk_Procesar_02_Facturas.Checked) sSql_02_Facturas = "";
                            if (!chk_Procesar_03_Dispensacion.Checked) sSql_03_RemisionesGenerales = "";


                            sSql_99_Concentrado = "";
                            if (sSql_01_Documentos != "")
                            {
                                sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n{0}\n", "---- Comprobación de documentos ", sSql_01_Documentos);
                            }

                            if (sSql_02_Facturas != "")
                            {
                                sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n{0}\n", "---- Facturas anticipadas ", sSql_02_Facturas);
                            }

                            if (sSql_03_RemisionesGenerales != "")
                            {
                                sSql_99_Concentrado += string.Format("\n\n\n{0}\n{1}\n{0}\n", "---- Remisiones generales ", sSql_03_RemisionesGenerales);
                            }

                            //LogProceso("");
                            //LogProceso("Inicio");
                            //LogProceso(DtIFacturacion.QuitarSaltoDeLinea(sSql_99_Concentrado));
                            LogProceso(sSql_99_Concentrado);


                            if (rdo_Ejecutar_01_SI.Checked)
                            {
                                if (!leer.Exec(sSql_99_Concentrado))
                                {
                                    bRegresa = false;
                                    break;
                                }
                                else
                                {
                                    //GetRemisionesGeneradas();
                                }
                            }

                            //LogProceso("Fin");
                            //LogProceso(""); 

                            #endregion Ejecución concentrada por Fuente de Financiamiento 

                        }
                        #endregion Fuentes de Financiamiento 
                    }
                }
            }

            return bRegresa;
        }
        #region Get Listados 
        private string GetLista_ProgramasDeAtencion()
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            string sValor_02 = ""; 
            int iItems = 0;

            for (int i = 1; i <= gridProgramas.Rows; i++)
            {
                {
                    sValor = gridProgramas.GetValue(i, Cols_Programas.IdPrograma);
                    sValor_02 = gridProgramas.GetValue(i, Cols_Programas.IdSubPrograma);
                    sRegresa += string.Format("'{0}{1}', ", sValor, sValor_02);
                }
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }

        private string GetLista_GUIDS()
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            string sValor_02 = "";
            int iItems = 0;

            foreach (string sItem in listaGuids)
            {
                sRegresa += string.Format("''{0}'', ", sItem);
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }

        private string GetLista_Farmacias()
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            int iItems = 0;

            for (int i = 1; i <= gridUnidades.Rows; i++)
            {
                if ( gridUnidades.GetValueBool(i, Cols_Farmacias.Remisionar) )
                {
                    sValor = gridUnidades.GetValue(i, Cols_Claves.ClaveSSA);
                    sRegresa += string.Format("''{0}'', ", sValor);
                }
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }

        private string GetLista_FoliosFarmacia()
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            int iItems = 0;

            for (int i = 1; i <= gridFolios.Rows; i++)
            {
                //if (gridFolios.GetValueBool(i, Cols_Farmacias_Folios.FolioVenta))
                {
                    sValor = gridFolios.GetValue(i, Cols_Farmacias_Folios.FolioVenta);
                    sRegresa += string.Format("''{0}'', ", sValor);
                }
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }
        private string GetLista_Claves(clsGrid objGrid)
        {
            string sRegresa = "";
            string sSegmento = "";
            string sValor = "";
            int iItems = 0;

            for (int i = 1; i <= objGrid.Rows; i++)
            {
                sValor = objGrid.GetValue(i, Cols_Claves.ClaveSSA);
                sRegresa += string.Format("'{0}', ", sValor);
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }
        #endregion Get Listados 

        #region Varios 
        private void Imprimir()
        { 
            clsRemision_GenerarDocumentos documentos = new clsRemision_GenerarDocumentos();
            string sFolioRemision = "";
            string sDescripcion = "";
            string sFarmaciaDispensacion = "";
            string sBeneficiario = "";
            string sRutaDestino = "";
            clsLeer leerDatos = new clsLeer();
            int iDocumentosGenerados = 0;
            bool bDocumentoGenerado = false; 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_REMISIONES\{0}", General.FechaYMD(General.FechaSistema, ""));

            bEjecutando = true;
            documentos.GenerarDirectorio_Farmacia = true;
            documentos.Generar_EXCEL = false;
            documentos.Generar_PDF = true;
            documentos.RutaDestinoReportes = sRutaDestino;

            leerRemisiones.RegistroActual = 0;
            //leer.RegistroActual = 0;

            for(int i = 1; i <= gridUnidades.Rows; i++)
            {
                if(gridUnidades.GetValueBool(i, ColsRemisiones.Seleccionar))
                {
                    leerDatos.DataRowsClase = leerRemisiones.DataTableClase.Select( string.Format(" FolioRemision = '{0}' ", gridUnidades.GetValue(i, ColsRemisiones.FolioRemision) ));
                    if(leerDatos.Leer())
                    {
                        //sFolioRemision = leerDatos.Campo("FolioRemision");  // grid.GetValue(i, (int)Cols.FolioRemision);
                        //sFarmaciaDispensacion = "___SV" + txtFolio.Text + "__" + leerDatos.Campo("IdFarmaciaDispensacion") + "__" + leerDatos.Campo("FarmaciaDispensacion");
                        //sBeneficiario = leerDatos.Campo("Referencia_Beneficiario") + "__" + leerDatos.Campo("Referencia_NombreBeneficiario");

                        //sDescripcion = sFarmaciaDispensacion;

                        //if(sBeneficiario != "__")
                        //{
                        //    sDescripcion = sFarmaciaDispensacion + "_" + sBeneficiario;
                        //}

                        //sDescripcion = sDescripcion.Replace(" ", "_").Replace("-", "_");
                        //bDocumentoGenerado = documentos.GenerarDocumentos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRemision, sDescripcion, cboFormatosDeImpresion.Data);
                        //iDocumentosGenerados += bDocumentoGenerado ? 1 : 0;
                    }
                }
            }

            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            //IniciarToolBar(true, true, true);

            //BloquearControles(false);

            //MostrarEnProceso(false);

            if(iDocumentosGenerados > 0)
            {
                General.AbrirDirectorio(sRutaDestino);
            }
        }

        private void chkEsRelacionFacturaPrevia_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEsRelacionFacturaPrevia.Checked)
            {
                txtFactura_Serie.Text = "";
                txtFactura_Folio.Text = "";
                txtFactura_Serie.Enabled = false;
                txtFactura_Folio.Enabled = false;
            }
            else
            {
                txtFactura_Serie.Enabled = true;
                txtFactura_Folio.Enabled = true;
            }
        }
        #endregion Varios 

        #region Preparar SQL Remisiones 
        private string PrepararParametrosRemisiones_FacturasRelacionadas(string IdFuenteFinanciamiento, string IdFinanciamiento, int EsDiferencial, string IdCliente, string IdSubCliente,
            string IdFarmacia, string FolioDeVenta, string FechaInicial, string FechaFinal,
            string Criterio_ProgramasAtencion, 
            string Serie, string Folio, string FacturaEnCajas, string RelacionPorMontos, string ProcesarSoloClavesConReferencias,
            string ListaClavesSSA_Exclusivas, string ListaClavesSSA_Excluidas,
            int Procesa_Venta, int Procesa_Consigna, int Procesa_Producto, int Procesa_Servicio,
            int Procesa_Medicamento, int Procesa_MaterialDeCuracion
        )
        {
            string sRegresa = "";
            string sEsRelacionFactura = "1";
            string sEsDocumentoComprobacion = "0";

            sRegresa = PrepararParametrosRemisiones(
                    IdFuenteFinanciamiento, IdFinanciamiento, EsDiferencial, IdCliente, IdSubCliente,
                    IdFarmacia, FolioDeVenta, FechaInicial, FechaFinal, Criterio_ProgramasAtencion,
                    sEsRelacionFactura, Serie, Folio, FacturaEnCajas, RelacionPorMontos, ProcesarSoloClavesConReferencias,
                    sEsDocumentoComprobacion, "", ListaClavesSSA_Exclusivas, ListaClavesSSA_Excluidas,
                    Procesa_Venta, Procesa_Consigna, Procesa_Producto, Procesa_Servicio,
                    Procesa_Medicamento, Procesa_MaterialDeCuracion
                    );

            return sRegresa;
        }
        private string PrepararParametrosRemisiones_DocumentosDeComprobacion(string IdFuenteFinanciamiento, string IdFinanciamiento, int EsDiferencial, string IdCliente, string IdSubCliente,
            string IdFarmacia, string FolioDeVenta, string FechaInicial, string FechaFinal,
            string Criterio_ProgramasAtencion, string DocumentoComprobacion,
            string ListaClavesSSA_Exclusivas, string ListaClavesSSA_Excluidas,
            int Procesa_Venta, int Procesa_Consigna, int Procesa_Producto, int Procesa_Servicio,
            int Procesa_Medicamento, int Procesa_MaterialDeCuracion
        )
        {
            string sRegresa = "";
            string sEsRelacionFactura = "0";
            string sEsDocumentoComprobacion = "1"; 

            sRegresa = PrepararParametrosRemisiones(
                    IdFuenteFinanciamiento, IdFinanciamiento, EsDiferencial, IdCliente, IdSubCliente,
                    IdFarmacia, FolioDeVenta, FechaInicial, FechaFinal, Criterio_ProgramasAtencion,
                    sEsRelacionFactura, "", "", "0", "0", "0",
                    sEsDocumentoComprobacion, DocumentoComprobacion,
                    ListaClavesSSA_Exclusivas, ListaClavesSSA_Excluidas,
                    Procesa_Venta, Procesa_Consigna, Procesa_Producto, Procesa_Servicio,
                    Procesa_Medicamento, Procesa_MaterialDeCuracion 
                    );

            return sRegresa;
        }

        private string PrepararParametrosRemisiones(string IdFuenteFinanciamiento, string IdFinanciamiento, int EsDiferencial, string IdCliente, string IdSubCliente, 
            string IdFarmacia, string FolioDeVenta, string FechaInicial, string FechaFinal, 
            string Criterio_ProgramasAtencion, 
            string EsRelacionFactura, string Serie, string Folio, string FacturaEnCajas, string RelacionPorMontos, string ProcesarSoloClavesConReferencias,
            string EsDocumentoDeComprobacion, string DocumentoDeComprobacion,
            string ListaClavesSSA_Exclusivas, string ListaClavesSSA_Excluidas,
            int Procesa_Venta, int Procesa_Consigna, int Procesa_Producto, int Procesa_Servicio,
            int Procesa_Medicamento, int Procesa_MaterialDeCuracion
        )
        {
            bool bRegresa = false;
            string sSql = "";
            int iMostrarResultado = 0;

            string sIdFarmacia = IdFarmacia; 
            int iTipoProcesoRemision = 0;
            int iBeneficiarios_x_Jurisdiccion = chkBeneficiarios_x_Jurisdiccion.Checked ? 1 : 0;
            int iProcesarParcialidades = chkProcesarParcialidades.Checked ? 1 : 0;
            int iProcesarCantidadesExcedentes = chkProcesarCantidadesExcedentes.Checked ? 1 : 0;
            int iAsignarReferencias = chkAsignarReferencias.Checked ? 1 : 0;

            int iProcesar_Producto = Procesa_Producto; //chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
            int iProcesar_Servicio = Procesa_Servicio; //chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
            int iProcesar_Servicio_Consigna = (Procesa_Servicio == 1 && Procesa_Consigna == 1) ? 1 : 0; // chkTipoDeRemision_02_Servicio.Checked && chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;

            int iProcesar_Medicamento = Procesa_Medicamento; //chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
            int iProcesar_MaterialDeCuracion = Procesa_MaterialDeCuracion; //chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;
            int iProcesar_Venta = Procesa_Venta; //chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
            int iProcesar_Consigna = Procesa_Consigna; //chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;

            int iTipoDeRemision = 0;
            //string sCriterio_ProgramasAtencion = "";
            int iMontoAFacturar = 0;
            int iIdTipoProducto = 0; // Todo, se revisa en base a Procesar X ITEM 
            int iEsExcedente = 0;
            int iTipoDispensacion = 0;

            // Trabajar en base a fecha de dispensación 
            int iFechaRevision = 1; //tipoDeUnidades == eTipoDeUnidades.Almacenes ? 3 : 1;


            string sListaFoliosDeVenta = FolioDeVenta;
            int iAplicarDocumentos = chkAplicarDocumentos.Checked ? 1 : 0;
            int iEsProgramasEspeciales = chkEsProgramaEspecial.Checked ? 1 : 0;
            string sIdBeneficiario = "";
            string sIdBeneficiario_01_Menor = "";
            string sIdBeneficiario_02_Mayor = "";
            int iRemision_General = chkEsRemisionGeneral.Checked ? 1 : 0;

            string sClaveSSA = ListaClavesSSA_Exclusivas;
            string sListaClavesSSA_Excluidas = ListaClavesSSA_Excluidas;

            //string EsRelacionFactura, string Serie, string Folio, string FacturaEnCajas, string RelacionPorMontos, string ProcesarSoloClavesConReferencias

            int iEsRelacionFacturaPrevia = Convert.ToInt32("0" + EsRelacionFactura); // ? 0 : 1; //chkEsRelacionFacturaPrevia.Checked ? 1 : 0;
            int iEsFacturaPreviaEnCajas = Convert.ToInt32("0" + FacturaEnCajas); // ? 0 : 1; //chkEsFacturaPreviaEnCajas.Checked ? 1 : 0;
            string sSerie = Serie; //chkEsRelacionFacturaPrevia.Checked ? txtFactura_Serie.Text.Trim() : "";
            string sFolio = Folio; // chkEsRelacionFacturaPrevia.Checked ? txtFactura_Folio.Text.Trim() : "";
            int iEsRelacionMontos = Convert.ToInt32("0" + RelacionPorMontos); // == "" ? 0 : 1; //chkEsRelacionDeMontos.Checked ? 1 : 0;
            int iProcesar_SoloClavesReferenciaRemisiones = Convert.ToInt32("0" + ProcesarSoloClavesConReferencias); // == "" ? 0 : 1; // chkProcesar_SoloClavesReferenciaRemisiones.Checked ? 1 : 0;


            int iExcluirCantidadesConDecimales = chkExcluirCantidadesConDecimales.Checked ? 1 : 0;
            int iSeparar__Venta_y_Vales = chkSeparar__Venta_y_Vales.Checked ? 1 : 0;
            int iEsRemision_Complemento = EsDiferencial;//chkEsComplemento.Checked ? 1 : 0;

            iTipoProcesoRemision = 1; // Este parámetro siempre debe ser 1 
            ////if (iEsRemision_Complemento == 1)
            ////{
            ////    iTipoProcesoRemision = 2;
            ////}

            ////sIdFarmacia = IdFarmacia == "" ? string.Format("''") : IdFarmacia = string.Format("[ {0} ]", IdFarmacia); 
            Criterio_ProgramasAtencion = Criterio_ProgramasAtencion == "" ? string.Format("''") : Criterio_ProgramasAtencion = string.Format("[ {0} ]", Criterio_ProgramasAtencion);
            sClaveSSA = sClaveSSA == "" ? string.Format("''") : sClaveSSA = string.Format("[ {0} ]", sClaveSSA);
            sListaClavesSSA_Excluidas = sListaClavesSSA_Excluidas == "" ? string.Format("''") : sListaClavesSSA_Excluidas = string.Format("[ {0} ]", sListaClavesSSA_Excluidas);

            //sIdFarmacia = sIdFarmacia == "" ? string.Format("''") : sIdFarmacia = string.Format("[ {0} ]", sIdFarmacia);


            sSql = string.Format("Exec {0} \n", sStoreDeProceso); 
            sSql +=
                string.Format("" +
                    " \t@NivelInformacion_Remision = '{0}', @Beneficiarios_x_Jurisdiccion = '{1}', \n" +
                    " \t@ProcesarParcialidades = '{2}', @ProcesarCantidadesExcedentes = '{3}', @AsignarReferencias = '{4}', \n" +
                    " \t@Procesar_Producto = '{5}', @Procesar_Servicio = '{6}', @Procesar_Servicio_Consigna = '{7}', \n" +
                    " \t@Procesar_Medicamento = '{8}', @Procesar_MaterialDeCuracion = '{9}', @Procesar_Venta = '{10}', @Procesar_Consigna = '{11}', \n" +
                    " \n" +
                    " \t@IdEmpresa = '{12}', @IdEstado = '{13}', @IdFarmaciaGenera = '{14}', @TipoDeRemision = '{15}', @IdFarmacia = '{16}', \n" +
                    " \t@IdCliente = '{17}', @IdSubCliente = '{18}', @IdFuenteFinanciamiento = '{19}', @IdFinanciamiento = '{20}', @Criterio_ProgramasAtencion = {21}, \n" +
                    " \n" +
                    " \t@FechaInicial = '{22}', @FechaFinal = '{23}', @iMontoFacturar = '{24}', @IdPersonalFactura = '{25}', @Observaciones = '{26}', \n" +
                    " \t@IdTipoProducto = '{27}', @EsExcedente = '{28}', @Identificador = '{29}', @TipoDispensacion = '{30}', \n" + 
                    " \t@ClaveSSA = {31}, @FechaDeRevision = '{32}', @FoliosVenta = '{33}', \n" +
                    " \n" +
                    " \t@TipoDeBeneficiario = '{34}', @Aplicar_ImporteDocumentos = '{35}', @EsProgramasEspeciales = '{36}', \n" +
                    " \t@IdBeneficiario = '{37}', @IdBeneficiario_MayorIgual = '{38}', @IdBeneficiario_MenorIgual = '{39}', \n" + 
                    " \t@Remision_General = '{40}', @ClaveSSA_ListaExclusion = {41}, \n" +
                    " \t@EsRelacionFacturaPrevia = '{42}', @FacturaPreviaEnCajas = '{43}', @Serie = '{44}', @Folio = '{45}', @EsRelacionMontos = '{46}', \n" +
                    " \t@Procesar_SoloClavesReferenciaRemisiones = '{47}', @ExcluirCantidadesConDecimales = '{48}', \n" + 
                    " \t@Separar__Venta_y_Vales = '{49}', @TipoDispensacion_Venta = '{50}', @EsRemision_Complemento = '{51}', \n" +
                    " \t@MostrarResultado = '{52}', @TipoProcesoRemision = '{53}', \n" +
                    " \t@EsRelacionDocumentoPrevio = '{54}', @FolioRelacionDocumento = '{55}' \n",


                    cboNivelDeInformacion.Data, iBeneficiarios_x_Jurisdiccion,
                    iProcesarParcialidades, iProcesarCantidadesExcedentes, iAsignarReferencias,
                    iProcesar_Producto, iProcesar_Servicio, iProcesar_Servicio_Consigna,

                    iProcesar_Medicamento, iProcesar_MaterialDeCuracion, iProcesar_Venta, iProcesar_Consigna,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeRemision, sIdFarmacia,
                    IdCliente, IdSubCliente, IdFuenteFinanciamiento, IdFinanciamiento, Criterio_ProgramasAtencion,


                    FechaInicial, FechaFinal, iMontoAFacturar, DtGeneral.IdPersonal, "",
                    iIdTipoProducto, iEsExcedente, sGUID, iTipoDispensacion, sClaveSSA, iFechaRevision, sListaFoliosDeVenta,

                    cboTipoDeBeneficiarios.Data, iAplicarDocumentos, iEsProgramasEspeciales,
                    sIdBeneficiario, sIdBeneficiario_01_Menor, sIdBeneficiario_02_Mayor, iRemision_General, sListaClavesSSA_Excluidas,

                    iEsRelacionFacturaPrevia, iEsFacturaPreviaEnCajas, sSerie, sFolio, iEsRelacionMontos,

                    iProcesar_SoloClavesReferenciaRemisiones, iExcluirCantidadesConDecimales, iSeparar__Venta_y_Vales, cboOrigenDispensacion.Data, iEsRemision_Complemento,

                    iMostrarResultado, iTipoProcesoRemision,
                    EsDocumentoDeComprobacion, DocumentoDeComprobacion
                );

            ////sSql += string.Format(
            ////    "\n\n" +
            ////    "Select distinct R.*, F.IdFarmacia As IdFarmaciaDispensacion, F.Farmacia As FarmaciaDispensacion\n" +
            ////    "From vw_FACT_Remisiones R (NoLock)\n" +
            ////    "Inner Join FACT_Remisiones_Detalles D (NoLock)\n" +
            ////    "   On (R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision)\n" +
            ////    "Inner Join vw_Farmacias F (NoLock) On (R.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia)\n" +
            ////    "Where R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' and GUID = '{3}'  ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID);

            return sSql;
        }
        #endregion Preparar SQL Remisiones 

        private bool VerificarRemisionesGeneradas()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql += string.Format(
                "\n\n" +
                "Select distinct R.*, F.IdFarmacia As IdFarmaciaDispensacion, F.Farmacia As FarmaciaDispensacion\n" +
                "From vw_FACT_Remisiones R (NoLock)\n" +
                "Inner Join FACT_Remisiones_Detalles D (NoLock)\n" +
                "   On (R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmacia = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision)\n" +
                "Inner Join vw_Farmacias F (NoLock) On (R.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia)\n" +
                "Where R.IdEmpresa = '{0}' and R.IdEstado = '{1}' and R.IdFarmacia = '{2}' and GUID = '{3}'  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID);

            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "VerificarRemisionesGeneradas");
            }
            else
            {
                bRegresa = leer.Leer();
                leerRemisiones.DataSetClase = leer.DataSetClase;
            }

            return bRegresa;  
        }
        #endregion Generar Remisiones
        
        #region Fuentes de Financiamiento 
        private void grdFuentesDeFinanciamiento_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridFF, e);
        }
        private void btnAgregarFuentes_Click( object sender, EventArgs e )
        {
            if(txtRubro.Text.Trim() == "")
            {
                General.msjUser("No ha capturado una Configuración válida, verifique.");
            }
            else
            {
                int iRenglon = gridFF.Rows + 1;
                
                gridFF.AddRow(); 
                gridFF.SetValue(iRenglon, Cols_FF.IdFuenteFinanciento, txtRubro.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.TipoDeFuente, sTipoDe_FuenteDeFinanciamiento); 
                gridFF.SetValue(iRenglon, Cols_FF.EsDiferencial, bEsDiferencial);
                gridFF.SetValue(iRenglon, Cols_FF.AplicarDiferencial, bEsDiferencial);

                gridFF.SetValue(iRenglon, Cols_FF.IdFinanciamiento, txtConcepto.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.Financiamiento, lblConcepto.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.IdCliente, lblIdCliente.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.Cliente, lblCliente.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.IdSubCliente, lblIdSubCliente.Text); 
                gridFF.SetValue(iRenglon, Cols_FF.SubCliente, lblSubCliente.Text); 


                sTipoDe_FuenteDeFinanciamiento = "";
                bEsDiferencial = false;

                Fg.IniciaControles(this, true, Frame_02_FuentesFinanciamiento);
                txtRubro.Focus(); 
            }
        }

        private void btnLimpiarFF_Click( object sender, EventArgs e )
        {
            gridFF.Limpiar(false);
            txtRubro.Focus();
        }
        #endregion Fuentes de Financiamiento 

        #region Farmacias 
        private void chkHabiltarProceso_x_FoliosEspecificos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHabiltarProceso_x_FoliosEspecificos.Checked)
            {
                gridUnidades.Limpiar(false);
                txtIdFarmacia.Focus();
            }
            else
            {
                gridFolios.Limpiar(false);
                CargarUnidades(); 
            }
        }
        private void chkMarcarDesmarcarFarmacias_CheckedChanged(object sender, EventArgs e)
        {
            gridUnidades.SetValue(Cols_Farmacias.Remisionar, chkMarcarDesmarcarFarmacias.Checked);
        }

        #region Farmacias - Folios  
        private void txtIdFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblFarmacia.Text = "";
            lblIdCliente_Venta.Text = "";
            lblCliente_Venta.Text = "";
            lblIdSubCliente_Venta.Text = "";
            lblSubCliente_Venta.Text = "";
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias(DtGeneral.EstadoConectado, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                if (leer.Leer())
                {
                    CargarFarmacia();
                }
            }
        }

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Farmacias(DtGeneral.EstadoConectado, "txtIdFarmacia_KeyDown");
                if (leer.Leer())
                {
                    CargarFarmacia();
                }
            }
        }
        private void CargarFarmacia()
        {
            txtIdFarmacia.ReadOnly = true; 
            txtIdFarmacia.Text = leer.Campo("IdFarmacia");
            lblFarmacia.Text = leer.Campo("Farmacia");
        }

        private void txtFolioVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFolioVenta_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text.Trim() != "" && txtFolioVenta.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas(sIdEmpresa, sIdEstado, txtIdFarmacia.Text, txtFolioVenta.Text.Trim(), "txtFolioVenta_Validating");
                if (!leer.Leer())
                {
                    txtFolioVenta.Text = "";
                    txtFolioVenta.Focus(); 
                }
                else 
                {
                    CargarInformacion_FolioVenta(); 
                }
            }
        }

        private void CargarInformacion_FolioVenta()
        {
            txtFolioVenta.Text = leer.Campo("Folio");
            lblIdCliente_Venta.Text = leer.Campo("IdCliente");
            lblCliente_Venta.Text = leer.Campo("NombreCliente");
            lblIdSubCliente_Venta.Text = leer.Campo("IdSubCliente");
            lblSubCliente_Venta.Text = leer.Campo("NombreSubCliente");
        }

        private void grdFolios_x_Farmacia_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridFolios, e);

            txtIdFarmacia.ReadOnly = false;
            txtIdFarmacia.Text = "";
            txtIdFarmacia.Focus(); 
        }

        private void btnAgregarFarmaciaFolio_Click(object sender, EventArgs e)
        {

            if (txtIdFarmacia.Text.Trim() == "" || txtFolioVenta.Text.Trim() == "")
            {
                General.msjUser("Información de Folio de venta incompleta, verifique.");
            }
            else
            {
                int iRenglon = gridFolios.Rows + 1;

                gridFolios.AddRow();
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.IdFarmacia, txtIdFarmacia.Text);
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.Farmacia, lblFarmacia.Text);

                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.IdCliente, lblIdCliente_Venta.Text);
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.Cliente, lblCliente_Venta.Text);
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.IdSubCliente, lblIdSubCliente_Venta.Text);
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.SubCliente, lblSubCliente_Venta.Text);
                gridFolios.SetValue(iRenglon, Cols_Farmacias_Folios.FolioVenta, txtFolioVenta.Text);

                txtFolioVenta.Text = "";

                txtFolioVenta.Focus();
            }
        }

        private void btnLimpiarFarmaciaFolio_Click(object sender, EventArgs e)
        {
            gridFolios.Limpiar(false);
            txtFolioVenta.Focus();
        }
        #endregion Farmacias - Folios  
        #endregion Farmacias 

        #region Programa - SubPrograma 
        private void EliminarRenglonGrid(clsGrid objGrid, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (!bEjecutando)
                    {
                        int iRow = objGrid.ActiveRow;
                        objGrid.DeleteRow(iRow);
                    }
                }
                catch { }
            }
        }
        private void txtIdPrograma_TextChanged(object sender, EventArgs e)
        {
            lblPrograma.Text = "";
            txtIdSubPrograma.Text = ""; 
        }

        private void txtIdPrograma_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPrograma.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Programas(txtIdPrograma.Text, "txtIdPrograma_Validating");
                if (!leer.Leer())
                {
                    e.Cancel = false; 
                }
                else 
                {
                    CargarPrograma();
                }
            }
        }

        private void txtIdPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Programas("txtIdPrograma_KeyDown");
                if (leer.Leer())
                {
                    CargarPrograma();
                }
            }
        }

        private void txtIdSubPrograma_TextChanged(object sender, EventArgs e)
        {
            lblSubPrograma.Text = "";
        }

        private void txtIdSubPrograma_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPrograma.Text.Trim() != "" && txtIdSubPrograma.Text.Trim() != "" )
            {
                leer.DataSetClase = Consultas.SubProgramas(txtIdPrograma.Text, txtIdSubPrograma.Text, "txtIdSubPrograma_Validating");
                if (!leer.Leer())
                {
                    e.Cancel = false;
                }
                else
                {
                    CargarSubPrograma();
                }
            }
        }

        private void txtIdSubPrograma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtIdPrograma.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayudas.SubProgramas("txtIdPrograma_KeyDown", txtIdPrograma.Text);
                    if (leer.Leer())
                    {
                        CargarSubPrograma();
                    }
                }
            }
        }

        private void CargarPrograma()
        {
            txtIdPrograma.Text = leer.Campo("IdPrograma");
            lblPrograma.Text = leer.Campo("Programa");
        }
        private void CargarSubPrograma()
        {
            txtIdSubPrograma.Text = leer.Campo("IdSubPrograma");
            lblSubPrograma.Text = leer.Campo("SubPrograma");
        }

        private void grdProgramasSubProgramas_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridProgramas, e);
        }

        private void btnAgregarPrograma_Click(object sender, EventArgs e)
        {
            if (txtIdPrograma.Text.Trim() == "" || txtIdSubPrograma.Text.Trim() == "")
            {
                General.msjUser("Información de Programa-SubPrograma incompleta, verifique.");
            }
            else
            {
                int iRenglon = gridProgramas.Rows + 1;

                gridProgramas.AddRow();
                gridProgramas.SetValue(iRenglon, Cols_Programas.IdPrograma, txtIdPrograma.Text);
                gridProgramas.SetValue(iRenglon, Cols_Programas.Programa, lblPrograma.Text);
                gridProgramas.SetValue(iRenglon, Cols_Programas.IdSubPrograma, txtIdSubPrograma.Text);
                gridProgramas.SetValue(iRenglon, Cols_Programas.SubPrograma, lblSubPrograma.Text);

                txtIdPrograma.Text = "";
                txtIdSubPrograma.Text = ""; 

                txtIdPrograma.Focus();
            }
        }

        private void btnLimpiarPrograma_Click(object sender, EventArgs e)
        {
            gridProgramas.Limpiar(false);
            txtIdPrograma.Focus();
        }
        #endregion Programa - SubPrograma 

        #region Claves Exclusivas 
        private void txtClaveSSA_Exclusiva_TextChanged(object sender, EventArgs e)
        {
            lblClaveExclusiva.Text = "";
        }

        private void txtClaveSSA_Exclusiva_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA_Exclusiva.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA_Exclusiva.Text, true, "txtClaveSSA_Exclusiva_Validating");
                if (leer.Leer())
                {
                    CargarClaveExclusiva();
                }
            }
        }

        private void txtClaveSSA_Exclusiva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtClaveSSA_Exclusiva_KeyDown");
                if (leer.Leer())
                {
                    CargarClaveExclusiva();
                }
            }
        }

        private void CargarClaveExclusiva()
        {
            txtClaveSSA_Exclusiva.Text = leer.Campo("ClaveSSA");
            lblClaveExclusiva.Text = leer.Campo("DescripcionClave");
        }
        private void grdClavesExclusivas_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridClavesExclusivas, e);
        }
        private void btnAgregarClaveExclusiva_Click(object sender, EventArgs e)
        {
            if (txtClaveSSA_Exclusiva.Text.Trim() == "")
            {
                General.msjUser("Información de Clave incompleta, verifique.");
            }
            else
            {
                int iRenglon = gridClavesExclusivas.Rows + 1;

                gridClavesExclusivas.AddRow();
                gridClavesExclusivas.SetValue(iRenglon, Cols_Claves.ClaveSSA, txtClaveSSA_Exclusiva.Text);
                gridClavesExclusivas.SetValue(iRenglon, Cols_Claves.Descripcion, lblClaveExclusiva.Text);

                txtClaveSSA_Exclusiva.Text = "";

                txtClaveSSA_Exclusiva.Focus();
            }
        }

        private void btnLimpiarClaveExclusiva_Click(object sender, EventArgs e)
        {
            gridClavesExclusivas.Limpiar(false);
            txtClaveSSA_Exclusiva.Focus();
        }
        #endregion Claves Exclusivas 

        #region Claves Excluidas  
        private void txtClaveSSA_Excluida_TextChanged(object sender, EventArgs e)
        {
            lblClaveExcluida.Text = "";
        }

        private void txtClaveSSA_Excluida_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA_Excluida.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA_Excluida.Text, true, "txtClaveSSA_Exclusiva_Validating");
                if (leer.Leer())
                {
                    CargarClaveExclusiva();
                }
            }
        }

        private void txtClaveSSA_Excluida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtClaveSSA_Excluida_KeyDown");
                if (leer.Leer())
                {
                    CargarClaveExcluida();
                }
            }
        }
        private void CargarClaveExcluida()
        {
            txtClaveSSA_Excluida.Text = leer.Campo("ClaveSSA");
            lblClaveExcluida.Text = leer.Campo("DescripcionClave");
        }
        private void grdClavesExcluidas_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridClavesExcluidas, e);
        }

        private void btnAgregarClaveExcluida_Click(object sender, EventArgs e)
        {
            if (txtClaveSSA_Exclusiva.Text.Trim() == "")
            {
                General.msjUser("Información de Clave incompleta, verifique.");
            }
            else
            {
                int iRenglon = gridClavesExcluidas.Rows + 1;

                gridClavesExcluidas.AddRow();
                gridClavesExcluidas.SetValue(iRenglon, Cols_Claves.ClaveSSA, txtClaveSSA_Excluida.Text);
                gridClavesExcluidas.SetValue(iRenglon, Cols_Claves.Descripcion, lblClaveExcluida.Text);

                txtClaveSSA_Excluida.Text = "";

                txtClaveSSA_Excluida.Focus();
            }
        }

        private void rdoOrdenEjecucionProceso_03_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEsComplemento_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiarClaveExcluida_Click(object sender, EventArgs e)
        {
            gridClavesExcluidas.Limpiar(false);
            txtClaveSSA_Excluida.Focus();
        }
        #endregion Claves Excluidas 

        #region Documentos de Facturas 
        ////private void txtSerie_TextChanged(object sender, EventArgs e)
        ////{
        ////    txtFolio.Text = "";
        ////    limpiarDatos_CFDI();
        ////}
        ////private void txtSerie_Validating(object sender, CancelEventArgs e)
        ////{

        ////}
        ////private void txtFolio_TextChanged(object sender, EventArgs e)
        ////{
        ////    limpiarDatos_CFDI(); ;
        ////}
        ////private void txtFolio_Validating(object sender, CancelEventArgs e)
        ////{
        ////    if (txtSerie.Text.Trim() != "" && txtFolio.Text.Trim() != "")
        ////    {
        ////        e.Cancel = !validarInformacionDeCFDI(); 
        ////    }
        ////}

        ////private bool validarInformacionDeCFDI()
        ////{
        ////    bool bRegresa = false; 
        ////    string sSql = string.Format("Select * \n" +
        ////            "From vw_FACT_CFD_DocumentosElectronicos (NoLock) \n" +
        ////            "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' \n",
        ////            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtSerie.Text.Trim(), txtFolio.Text.Trim()
        ////            ); // + Fg.PonCeros(IdCliente, 4) + "'";

        ////    if (!leer.Exec(sSql))
        ////    {
        ////        Error.GrabarError(leer, "validarInformacionDeCFDI");
        ////        General.msjError("Ocurrió un error al validar la información del Documento Electrónico.");
        ////    }
        ////    else
        ////    {
        ////        if (!leer.Leer())
        ////        {
        ////            General.msjAviso("Información de Documento CFDI no encontrada, verifique.");
        ////        }
        ////        else
        ////        {
        ////            bRegresa = true;

        ////            if (bRegresa && leer.Campo("IdTipoDocumento") != "001")
        ////            {
        ////                bRegresa = false;
        ////                General.msjAviso("El documento electrónico no del tipo Factura, verifique.");
        ////                txtSerie.Focus(); 
        ////            }

        ////            if (bRegresa && leer.Campo("StatusDocto").ToUpper() != "A")
        ////            {
        ////                bRegresa = false;
        ////                General.msjAviso("El documento electrónico no cuenta con Status Activo, verifique.");
        ////                txtSerie.Focus();
        ////            }

        ////            if (bRegresa && leer.CampoBool("EsRelacionConRemisiones"))
        ////            {
        ////                bRegresa = false;
        ////                General.msjAviso("El documento electrónico fue generado con relación de remisiones, es inválido para su procesamiento, verifique.");
        ////                txtSerie.Focus();
        ////            }

        ////            if (bRegresa)
        ////            {
        ////                lblCFDI_FechaExpedicion.Text = string.Format("{0}", leer.CampoFecha("FechaRegistro"));
        ////                lblCFDI_ClienteNombre.Text = leer.Campo("NombreReceptor");
        ////                lblCFDI_FuenteFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
        ////                lblCFDI_Financiamiento.Text = leer.Campo("Financiamiento");
        ////                lblCFDI_TipoDocumentoDescripcion.Text = leer.Campo("TipoDocumentoDescripcion");
        ////                lblCFDI_TipoDeInsumoDescripcion.Text = leer.Campo("TipoInsumoDescripcion");
        ////            }
        ////        }
        ////    }

        ////    return bRegresa; 
        ////}
        ////private void grdFacturas_KeyDown(object sender, KeyEventArgs e)
        ////{
        ////    EliminarRenglonGrid(gridFacturas, e);
        ////}
        ////private void btnAgregarFacturas_Click(object sender, EventArgs e)
        ////{
        ////    if (txtSerie.Text.Trim() == "" || txtFolio.Text.Trim() == "")
        ////    {
        ////        General.msjUser("Información de Documento CFDI incompleta, verifique.");
        ////    }
        ////    else
        ////    {
        ////        string sValor = string.Format("{0}{1}", txtSerie.Text.Trim(), txtFolio.Text.Trim());
        ////        //if (gridFacturas.BuscarRepetidos("", (int)Cols_Facturas..SerieFolio) != 0)

        ////        int[] Columnas = { (int)Cols_Facturas..Serie, (int)Cols_Facturas..Folio };

        ////        if (gridFacturas.BuscarRepetidosColumnas(sValor, Columnas) != 0)
        ////        {
        ////            General.msjUser("Documento electrónico previamente cargado.");
        ////            txtFolio.Focus();
        ////        }
        ////        else
        ////        {
        ////            int iRenglon = gridFacturas.Rows + 1;

        ////            gridFacturas.AddRow();
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..Serie, txtSerie.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..Folio, txtFolio.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..SerieFolio, sValor);

        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..Fecha, lblCFDI_FechaExpedicion.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..Cliente, lblCFDI_ClienteNombre.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..FuenteFinanciamiento, lblCFDI_FuenteFinanciamiento.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..Financiamiento, lblCFDI_Financiamiento.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..TipoDocumento, lblCFDI_TipoDocumentoDescripcion.Text);
        ////            gridFacturas.SetValue(iRenglon, Cols_Facturas..TipoInsumo, lblCFDI_TipoDeInsumoDescripcion.Text);


        ////            txtSerie.Text = "";
        ////            txtFolio.Text = "";
        ////            limpiarDatos_CFDI();

        ////            txtSerie.Focus();
        ////        }
        ////    }
        ////}
        ////private void groupBox5_Enter(object sender, EventArgs e)
        ////{

        ////}
        ////private void limpiarDatos_CFDI()
        ////{
        ////    lblCFDI_FechaExpedicion.Text = "";
        ////    lblCFDI_ClienteNombre.Text = "";
        ////    lblCFDI_FuenteFinanciamiento.Text = "";
        ////    lblCFDI_Financiamiento.Text = "";
        ////    lblCFDI_TipoDocumentoDescripcion.Text = "";
        ////    lblCFDI_FechaExpedicion.Text = "";
        ////    lblCFDI_TipoDeInsumoDescripcion.Text = "";
        ////}
        ////private void btnLimpiarFacturas_Click(object sender, EventArgs e)
        ////{
        ////    gridFacturas.Limpiar(false);
        ////}
        #endregion Documentos de Facturas 



        #region Exportar Excel 
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

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            leer.RegistroActual = 1;
            ////xpExcel.MostrarAvanceProceso = true; 
            ////xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

            clsLeer exportarExcel = new clsLeer();
            clsLeer dtsLocal = new clsLeer();


            sNombre = "REMISIONES GENERADAS";
            sNombreHoja = "REMISIONES";

            if (sNombre.Trim().Length >= 10)
            {
                sNombreHoja = sNombre.Substring(0, 10);
            }


            //iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                ////exportarExcel.DataTableClase = leerExportarExcel.Tabla("Resultados");
                ////iHojas_Totales = exportarExcel.Registros;

                //while (exportarExcel.Leer())
                {
                    generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, string.Format(sNombre));
                    generarExcel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);

                    iRenglon = 8;
                    //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                    generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerExportarExcel.DataSetClase);

                    //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                    generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                    //GC.AddMemoryPressure(100000);
                    GC.Collect();

                    //generarExcel.CerraArchivo();
                    iHojas_Agregadas++;

                    ///generarExcel.GuardarDocumento(iHojas_Agregadas < iHojas_Totales);
                }

                //generarExcel.CerraArchivo();
                if (iHojas_Agregadas > 0)
                {
                    //generarExcel.CerraArchivo_Stream(); 
                    generarExcel.CerrarArchivo();
                    generarExcel.AbrirDocumentoGenerado(true);
                }
            }
            Application.DoEvents();
        }
        #endregion Exportar Excel 
    }
}
