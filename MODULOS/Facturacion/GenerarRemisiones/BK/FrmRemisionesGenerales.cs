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
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;

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
            NumeroDeRemisionesGeneradas 
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
            Fecha,
            FolioRelacion,
            NombreDocumento,
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

        private enum Cols_Factuas
        {
            Ninguno,
            Serie,
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
        clsLeer leer, leerDetalles;
        clsLeer leerRemisiones;
        clsGrid myGrid;
        clsGrid gridUnidades;
        clsGrid gridFolios;
        clsGrid gridFF;
        clsGrid gridProgramas;
        clsGrid gridClavesExclusivas;
        clsGrid gridClavesExcluidas;

        System.Threading.Thread thHilo; 

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

        string sListaDeSeleccion = "";
        DataSet dtsProgramas_SubProgramas = new DataSet();

        string sTipoDe_FuenteDeFinanciamiento = "";
        bool bEsDiferencial = false;

        FrmDescargarVenta info;
        string sGUID = "";
        string sUrl = "";

        eTipoDeUnidades tipoDeUnidades = eTipoDeUnidades.Ninguna;
        string sStoreDeProceso = ""; 
        public FrmRemisionesGenerales() : this(eTipoDeUnidades.Farmacias)
        {
        }
        public FrmRemisionesGenerales(eTipoDeUnidades TipoDeUnidades)
        {
            InitializeComponent();

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

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);


            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);


            ////myGrid = new clsGrid(ref grdProductos, this);
            ////myGrid.AjustarAnchoColumnasAutomatico = true; 

            gridUnidades = new clsGrid(ref grdFarmacias, this);
            gridUnidades.AjustarAnchoColumnasAutomatico = true;

            gridFF = new clsGrid(ref grdFuentesDeFinanciamiento, this);
            gridFF.AjustarAnchoColumnasAutomatico = true;


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


        #region Botones 
        private void InicializarPantalla()
        {
            Fg.IniciaControles();

            gridFF.Limpiar(false);
            gridUnidades.Limpiar();
            gridProgramas.Limpiar(false);
            gridClavesExclusivas.Limpiar(false);
            gridClavesExcluidas.Limpiar(false);
            gridFacturas.Limpiar(false);
            gridDocumentosComprobacion.Limpiar(false);

            Frame_08_FacturasAnticipadas.Enabled = false; 

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



            ////myGrid.Limpiar(false);
            ////gridUnidades.Limpiar(false);

            SetFrames(true);
            ////cboAlmacen.Focus();
        }

        private void SetFrames( bool Habilitar )
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
            BloquearControles(Habilitar, Frame_08_FacturasAnticipadas);
            BloquearControles(false, Frame_08_FacturasAnticipadas);
            ////BloquearControles(Habilitar, Frame_09_VentaDirecta);
            BloquearControles(Habilitar, Frame_10_FormatosDeImpresion); 
        }

        private void BloquearControles( bool Habilitar, GroupBox Frame )
        {
            foreach(Control obj in Frame.Controls)
            {
                if(obj is TextBox)
                {
                    ((TextBox)obj).ReadOnly = !Habilitar;
                }

                if(obj is ComboBox)
                {
                    ((ComboBox)obj).Enabled = Habilitar;
                }

                if(obj is CheckBox)
                {
                    ((CheckBox)obj).Enabled = Habilitar;
                }

                if(obj is DateTimePicker)
                {
                    ((DateTimePicker)obj).Enabled = Habilitar;
                }
                if (obj is NumericUpDown)
                {
                    ((NumericUpDown)obj).Enabled = Habilitar;
                }
            }
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }


        private void btnEjecutar_Click( object sender, EventArgs e )
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
            thHilo = new Thread(thGenerarRemisiones);
            thHilo.Name = "GenerarRemisiones";
            thHilo.Start();
        }

        private void thGenerarRemisiones()
        {
            if (GenerarRemisiones())
            {
                General.msjUser("Remisiones generadas satisfactoriamente.");

                ////if (chkGenerarDocumentos.Checked)
                ////{
                ////    Imprimir();
                ////}
            }
        }

        private void btnExportarPDF_Click( object sender, EventArgs e )
        {
            bool bregresa = true;

            if(chkGenerarDocumentos.Checked)
            {
                if(cboFormatosDeImpresion.SelectedIndex == 0)
                {
                    bregresa = false;
                    General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                    cboFormatosDeImpresion.Focus();
                }
            }

            if(bregresa)
            {
                Imprimir();
            }
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

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
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
                "Where F.IdEstado = '{0}' and {1} ", DtGeneral.EstadoConectado, sFiltro);

            gridUnidades.Limpiar();
            if(!leer.Exec(sSql))
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
            sSql = string.Format("Exec spp_FACT_INFO_Comprobacion_Documentos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{3}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            gridDocumentosComprobacion.Limpiar();
            if (!this.leer.Exec(sSql))
            {
                base.Error.GrabarError(this.leer, "CargarDocumentosDeComprobacion");
                General.msjError("Ocurri\x00f3 un error al obtener la información de Documentos a comprobar.");
            }
            else if (!leer.Leer())
            {
                if (MensajeVacio)
                {
                    General.msjUser("No existen documentos para comprobar.");
                }
            }
            else
            {
                gridDocumentosComprobacion.LlenarGrid(this.leer.DataSetClase, false, false);
            }
        }

        private void CargarDocumentosFacturas(bool MensajeVacio)
        {
            string sSql = "";
            sSql = string.Format("Exec spp_FACT_INFO_Comprobacion_Facturas \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{3}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            gridFacturas.Limpiar();
            if (!this.leer.Exec(sSql))
            {
                base.Error.GrabarError(this.leer, "CargarDocumentosFacturas");
                General.msjError("Ocurri\x00f3 un error al obtener la información de Facturas.");
            }
            else if (!leer.Leer())
            {
                if (MensajeVacio)
                {
                    General.msjUser("No existen facturas para comprobar.");
                }
            }
            else
            {
               gridFacturas.LlenarGrid(this.leer.DataSetClase, false, false);
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
        private void txtRubro_TextChanged( object sender, EventArgs e )
        {
            lblRubro.Text = "";
            txtConcepto.Text = "";
            lblConcepto.Text = "";
            lblIdCliente.Text = "";
            lblCliente.Text = "";
            lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtRubro_Validating( object sender, CancelEventArgs e )
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            bEsExcedente_Rubro = false;
            if(txtRubro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtRubro.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosRubro();
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

        private void CargarDatosRubro()
        {
            txtRubro.Text = leer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");

            lblIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");

            sTipoDe_FuenteDeFinanciamiento = leer.Campo("EsParaExcedente_Descripcion");
            bEsDiferencial = leer.CampoBool("EsDiferencial");

            if(leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtRubro.Text = "";
                lblRubro.Text = "";
            }

            ////Obtener_ListaProgramasAtencion();
        }

        private void txtRubro_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtPrograma_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }
        #endregion Buscar Rubro

        #region Buscar Concepto
        private void txtConcepto_Validating( object sender, CancelEventArgs e )
        {
            string sCadena = "";
            leer = new clsLeer(ref cnn);

            dMontoFinanciamiento = 0.0000;
            bEsExcedente_Concepto = false;
            if(txtConcepto.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating");
                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
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
            if(leer.Campo("Status") == "C")
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

        private void txtConcepto_TextChanged( object sender, EventArgs e )
        {
            lblConcepto.Text = ""; 
        }

        private void txtConcepto_KeyDown( object sender, KeyEventArgs e )
        {
            string sCadena = "";
            if(e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtRubro.Text.Trim());

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if(leer.Leer())
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
        private bool validarInformacion_AlmacenFolioDeVenta()
        {
            bool bRegresa = true;

            ////if(bRegresa && cboAlmacen.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjAviso("No ha seleccionado un Almacén válido, verifique.");
            ////    cboAlmacen.Focus();
            ////}
            
            ////if(bRegresa && txtFolio.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjAviso("No ha capturado un folio de venta válido, verifique.");
            ////    txtFolio.Focus();
            ////}

            ////if(bRegresa)
            ////{
            ////    if(General.msjConfirmar("La información actualizada de Venta se descargará directamente del Almacén, \n\n¿ Desea continuar ?") != DialogResult.Yes)
            ////    {
            ////        bRegresa = false; 
            ////    }
            ////}

            return bRegresa;  
        }
        private bool GenerarRemisiones()
        {
            bool bRegresa = false;
            string sMensaje = "";
            string sSql = ""; 

            btnGenerarRemisiones.Enabled = false;

            if(!cnn.Abrir())
            {
                btnGenerarRemisiones.Enabled = true;
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                cnn.IniciarTransaccion();
                sGUID = Guid.NewGuid().ToString();   // Referencia unica para todas las remisiones generadas


                //bRegresa = leer.Exec(sSql);
                if (GenerarRemisiones_01_ProcesarRemisiones())
                {
                    bRegresa = GenerarRemisiones_02_ActualizarInformacionAdicional();
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
                    if(!VerificarRemisionesGeneradas())
                    {
                        cnn.DeshacerTransaccion();
                        General.msjAviso("No se generaron remisiones con los criterios especificados.");
                        btnGenerarRemisiones.Enabled = true;
                        bRegresa = false; 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                    }
                }

                cnn.Cerrar(); 
            }

            btnGenerarRemisiones.Enabled = true;

            return bRegresa; 

        }

        private bool GenerarRemisiones_02_ActualizarInformacionAdicional()
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

        private bool GenerarRemisiones_01_ProcesarRemisiones()
        {
            bool bRegresa = true;
            string sSql = "";
            string sDato_01_FuenteFinanciamiento = "";
            string sDato_02_Financiamiento = "";
            string sDato_03_IdCliente = "";
            string sDato_04_IdSubCliente = "";
            string sDato_05_EsDiferencial = "";

            string sDato_06_IdFarmacia = "";
            string sDato_07_FoliosDeVenta = "";
            string sDato_08_FechaInicial = General.FechaYMD(dtpFechaInicial.Value);
            string sDato_09_FechaFinal = General.FechaYMD(dtpFechaFinal.Value);
            string sDato_10_ProgramasDeAtencion = GetLista_ProgramasDeAtencion();

            string sDato_11_EsRelacionFactura = "";
            string sDato_12_Serie = "";
            string sDato_13_Folio = "";
            string sDato_14_FacturaEnCajas = "";
            string sDato_15_RelacionPorMontos = "";
            string sDato_16_ProcesarSoloClavesConReferencias = "";

            string sDato_17_EsDocumentoDeComprobacion = "";
            string sDato_18_DocumentoDeComprobacion = "";

            string sDato_19_ListaClavesExclusivas = GetLista_Claves(gridClavesExclusivas);
            string sDato_20_ListaClavesExcluidas = GetLista_Claves(gridClavesExcluidas);

            int iDatos_21_Procesa_Venta = chkOrigenInsumo_01_Venta.Checked ? 1 : 0;
            int iDatos_22_Procesa_Consigna  = chkOrigenInsumo_02_Consignacion.Checked ? 1 : 0;
            int iDatos_23_Procesa_Producto = chkTipoDeRemision_01_Producto.Checked ? 1 : 0;
            int iDatos_24_Procesa_Servicio = chkTipoDeRemision_02_Servicio.Checked ? 1 : 0;
            int iDatos_25_Procesa_Medicamento = chkTipoDeInsumo_01_Medicamento.Checked ? 1 : 0;
            int iDatos_26_Procesa_MaterialDeCuracion = chkTipoDeInsumo_02_MaterialDeCuracion.Checked ? 1 : 0;


            //// Procesar por Fuente de Financiamiento 
            for (int i = 1; i <= gridFF.Rows; i++)
            {
                sDato_01_FuenteFinanciamiento = gridFF.GetValue(i, Cols_FF.IdFuenteFinanciento);
                sDato_02_Financiamiento = gridFF.GetValue(i, Cols_FF.IdFinanciamiento);
                sDato_03_IdCliente = gridFF.GetValue(i, Cols_FF.IdCliente);
                sDato_04_IdSubCliente = gridFF.GetValue(i, Cols_FF.IdSubCliente);
                sDato_05_EsDiferencial = gridFF.GetValueInt(i, Cols_FF.EsDiferencial).ToString();

                if (bRegresa)
                {
                    //// Procesar Unidades 
                    for (int j = 1; j <= gridUnidades.Rows; j++)
                    {
                        if (gridUnidades.GetValueBool(j, Cols_Farmacias.Remisionar))
                        {
                            sDato_06_IdFarmacia = gridUnidades.GetValue(j, Cols_Farmacias.IdFarmacia);

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
                                        sDato_12_Serie = gridFacturas.GetValue(iFacturas, Cols_Factuas.Serie);
                                        sDato_13_Folio = gridFacturas.GetValue(iFacturas, Cols_Factuas.Folio);

                                        sDato_14_FacturaEnCajas = "0";
                                        sDato_15_RelacionPorMontos = "0";
                                        sDato_16_ProcesarSoloClavesConReferencias = "0";

                                        iDatos_21_Procesa_Venta = 1;
                                        iDatos_22_Procesa_Consigna = 0;
                                        iDatos_23_Procesa_Producto = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Factuas.Procesa_Producto);
                                        iDatos_24_Procesa_Servicio = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Factuas.Procesa_Servicio);
                                        iDatos_25_Procesa_Medicamento = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Factuas.Procesa_Medicamento); ;
                                        iDatos_26_Procesa_MaterialDeCuracion = gridDocumentosComprobacion.GetValueInt(iFacturas, Cols_Factuas.Procesar); ;

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
                                sDato_18_DocumentoDeComprobacion = "0";

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
                            }
                            #endregion Remisiones normales   
                        }

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

                    ////iItems++;
                    ////if (iItems == 5)
                    ////{
                    ////    sRegresa += "\t\t" + sSegmento + "\n";
                    ////    iItems = 0;
                    ////    sSegmento = "";
                    ////}
                }
            }

            ////if (sSegmento != "")
            ////{
            ////    sRegresa += "\t\t" + sSegmento + "\n";
            ////}

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
                {
                    sValor = objGrid.GetValue(i, Cols_Claves.ClaveSSA);
                    sRegresa += string.Format("'{0}', ", sValor);

                    ////iItems++;
                    ////if (iItems == 5)
                    ////{
                    ////    sRegresa += "\t\t" + sSegmento + "\n";
                    ////    iItems = 0;
                    ////    sSegmento = "";
                    ////}
                }
            }

            ////if (sSegmento != "")
            ////{
            ////    sRegresa += "\t\t" + sSegmento + "\n";
            ////}

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }

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


            string sClaveSSA = ListaClavesSSA_Exclusivas;
            string sListaFoliosDeVenta = FolioDeVenta;
            int iAplicarDocumentos = chkAplicarDocumentos.Checked ? 1 : 0;
            int iEsProgramasEspeciales = chkEsProgramaEspecial.Checked ? 1 : 0;
            string sIdBeneficiario = "";
            string sIdBeneficiario_01_Menor = "";
            string sIdBeneficiario_02_Mayor = "";
            int iRemision_General = chkEsRemisionGeneral.Checked ? 1 : 0;
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

            iTipoProcesoRemision = 1; 
            if (iEsRemision_Complemento == 1)
            {
                iTipoProcesoRemision = 2;
            }


            Criterio_ProgramasAtencion = Criterio_ProgramasAtencion == "" ? string.Format("''") : Criterio_ProgramasAtencion = string.Format("[ {0} ]", Criterio_ProgramasAtencion);
            sClaveSSA = sClaveSSA == "" ? string.Format("''") : sClaveSSA = string.Format("[ {0} ]", sClaveSSA);
            sListaClavesSSA_Excluidas = sListaClavesSSA_Excluidas == "" ? string.Format("''") : sListaClavesSSA_Excluidas = string.Format("[ {0} ]", sListaClavesSSA_Excluidas);


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
                    " \t@MostrarResultado = '{52}', @TipoProcesoRemision = '{53}' \n" +
                    " \t@EsRelacionDocumentoPrevio = '{54}', @FolioRelacionDocumento = '{55}' \n",


                    cboNivelDeInformacion.Data, iBeneficiarios_x_Jurisdiccion,
                    iProcesarParcialidades, iProcesarCantidadesExcedentes, iAsignarReferencias,
                    iProcesar_Producto, iProcesar_Servicio, iProcesar_Servicio_Consigna,

                    iProcesar_Medicamento, iProcesar_MaterialDeCuracion, iProcesar_Venta, iProcesar_Consigna,
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeRemision, IdFarmacia,
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

        private bool ValidarDatos_VentaDirecta()
        {
            bool bregresa = true;


            //////if (txtRubro.Text.Trim() == "")
            //////{
            //////    bregresa = false;
            //////    General.msjUser("No selecciono un rubro, verifique.");
            //////    txtRubro.Focus();
            //////}

            //////if (txtConcepto.Text.Trim() == "" && bregresa)
            //////{
            //////    bregresa = false;
            //////    General.msjUser("No selecciono un Concepto, verifique.");
            //////    txtConcepto.Focus();
            //////}


            ////if(gridFF.Rows == 0)
            ////{
            ////    bregresa = false;
            ////    General.msjUser("No ha seleccionado las Fuentes de financiamiento a procesar, verifique.");
            ////    txtRubro.Focus();
            ////}

            ////if (myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0 && bregresa)
            ////{
            ////    bregresa = false;
            ////    General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
            ////}

            ////if (Convert.ToDouble(nmPorcentaje.Text) == 0.00 && bregresa)
            ////{
            ////    bregresa = false;
            ////    General.msjUser("El porcentaje no puede ser igual cero, verifique.");
            ////    txtConcepto.Focus();
            ////}

            if(bregresa && cboFormatosDeImpresion.SelectedIndex == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboFormatosDeImpresion.Focus();
            }

            return bregresa;
        }

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

            if (cboTipoDeBeneficiarios.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Tipo de beneficiario inválido, verifique.");
                cboTipoDeBeneficiarios.Focus();
            }

            if (!(chkTipoDeRemision_01_Producto.Checked || chkTipoDeRemision_02_Servicio.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un tipo de remisión, verifique.");
                chkTipoDeRemision_01_Producto.Focus();
            }

            if (!(chkOrigenInsumo_01_Venta.Checked || chkOrigenInsumo_02_Consignacion.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un origen de insumo, verifique.");
                chkOrigenInsumo_01_Venta.Focus();
            }

            if (!(chkTipoDeInsumo_01_Medicamento.Checked || chkTipoDeInsumo_02_MaterialDeCuracion.Checked) && bregresa)
            {
                bregresa = false;
                General.msjUser("Seleccione un tipo de insumo, verifique.");
                chkTipoDeInsumo_01_Medicamento.Focus();
            }

            if (cboOrigenDispensacion.SelectedIndex == 0 && bregresa)
            {
                bregresa = false;
                General.msjUser("Origen de dispensación inválida, verifique.");
                cboOrigenDispensacion.Focus();
            }

            ////if (chkTipoDeRemision_01_Producto.Checked && bregresa)
            ////{
            ////    if(rdo_TP_01_FolioEspecifico.Checked)
            ////    {
            ////        if(myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0)
            ////        {
            ////            bregresa = false;
            ////            General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
            ////        }
            ////    }
            ////}

            ////if (chkTipoDeRemision_02_Servicio.Checked && bregresa)
            ////{
            ////    if(rdo_TP_01_FolioEspecifico.Checked)
            ////    {
            ////        if(myGrid.TotalizarColumnaDou(cols.PorRemisionar_Admon) == 0.0)
            ////        {
            ////            bregresa = false;
            ////            General.msjUser("La venta directa no contiene cantidad disponible de servicio para remisionar, verifique.");
            ////        }
            ////    }
            ////}

            if (chkEsRelacionFacturaPrevia.Checked && bregresa)
            {
                if (txtFactura_Folio.Text == "" || txtFactura_Serie.Text == "")
                {
                    bregresa = false;
                    General.msjUser("Indique el folio y la serie de la factura previa.");
                    txtFactura_Serie.Focus();
                }
            }

            if(bregresa && chkGenerarDocumentos.Checked && cboFormatosDeImpresion.SelectedIndex == 0)
            {
                bregresa = false;
                General.msjUser("No ha seleccionado un formato de impresión, verifique.");
                cboFormatosDeImpresion.Focus();
            }

            return bregresa;
        }


        private bool ValidarTotales()
        {
            bool bregresa = true;

            if (chkTipoDeRemision_01_Producto.Checked && bregresa)
            {
                if (myGrid.TotalizarColumnaDou(cols.PorRemisionar_Insumo) == 0)
                {
                    bregresa = false;
                    General.msjUser("La venta directa no contiene cantidad disponible de producto para remisionar, verifique.");
                }
            }

            if (chkTipoDeRemision_02_Servicio.Checked && bregresa)
            {
                if (myGrid.TotalizarColumnaDou(cols.PorRemisionar_Admon) == 0.0)
                {
                    bregresa = false;
                    General.msjUser("La venta directa no contiene cantidad disponible de servicio para remisionar, verifique.");
                }
            }

            return bregresa;
        }
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

        private void chkMarcarDesmarcarFarmacias_CheckedChanged(object sender, EventArgs e)
        {
            gridUnidades.SetValue(Cols_Farmacias.Remisionar, chkMarcarDesmarcarFarmacias.Checked);
        }

        private bool InformacioDeVenta__FoliosDeRemision()
        {
            bool bRegresa = false;

            return bRegresa;
        }

        #region Programa - SubPrograma 
        private void EliminarRenglonGrid(clsGrid objGrid, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    int iRow = objGrid.ActiveRow;
                    objGrid.DeleteRow(iRow);
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

        private void btnLimpiarClaveExcluida_Click(object sender, EventArgs e)
        {
            gridClavesExcluidas.Limpiar(false);
            txtClaveSSA_Excluida.Focus();
        }
        #endregion Claves Excluidas 

        #region Documentos de Facturas 
        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            txtFolio.Text = "";
            limpiarDatos_CFDI();
        }
        private void txtSerie_Validating(object sender, CancelEventArgs e)
        {

        }
        private void txtFolio_TextChanged(object sender, EventArgs e)
        {
            limpiarDatos_CFDI(); ;
        }
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtSerie.Text.Trim() != "" && txtFolio.Text.Trim() != "")
            {
                e.Cancel = !validarInformacionDeCFDI(); 
            }
        }

        private bool validarInformacionDeCFDI()
        {
            bool bRegresa = false; 
            string sSql = string.Format("Select * \n" +
                    "From vw_FACT_CFD_DocumentosElectronicos (NoLock) \n" +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtSerie.Text.Trim(), txtFolio.Text.Trim()
                    ); // + Fg.PonCeros(IdCliente, 4) + "'";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "validarInformacionDeCFDI");
                General.msjError("Ocurrió un error al validar la información del Documento Electrónico.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("Información de Documento CFDI no encontrada, verifique.");
                }
                else
                {
                    bRegresa = true;

                    if (bRegresa && leer.Campo("IdTipoDocumento") != "001")
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico no del tipo Factura, verifique.");
                        txtSerie.Focus(); 
                    }

                    if (bRegresa && leer.Campo("StatusDocto").ToUpper() != "A")
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico no cuenta con Status Activo, verifique.");
                        txtSerie.Focus();
                    }

                    if (bRegresa && leer.CampoBool("EsRelacionConRemisiones"))
                    {
                        bRegresa = false;
                        General.msjAviso("El documento electrónico fue generado con relación de remisiones, es inválido para su procesamiento, verifique.");
                        txtSerie.Focus();
                    }

                    if (bRegresa)
                    {
                        lblCFDI_FechaExpedicion.Text = string.Format("{0}", leer.CampoFecha("FechaRegistro"));
                        lblCFDI_ClienteNombre.Text = leer.Campo("NombreReceptor");
                        lblCFDI_FuenteFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
                        lblCFDI_Financiamiento.Text = leer.Campo("Financiamiento");
                        lblCFDI_TipoDocumentoDescripcion.Text = leer.Campo("TipoDocumentoDescripcion");
                        lblCFDI_TipoDeInsumoDescripcion.Text = leer.Campo("TipoInsumoDescripcion");
                    }
                }
            }

            return bRegresa; 
        }
        private void grdFacturas_KeyDown(object sender, KeyEventArgs e)
        {
            EliminarRenglonGrid(gridFacturas, e);
        }
        private void btnAgregarFacturas_Click(object sender, EventArgs e)
        {
            if (txtSerie.Text.Trim() == "" || txtFolio.Text.Trim() == "")
            {
                General.msjUser("Información de Documento CFDI incompleta, verifique.");
            }
            else
            {
                string sValor = string.Format("{0}{1}", txtSerie.Text.Trim(), txtFolio.Text.Trim());
                //if (gridFacturas.BuscarRepetidos("", (int)Cols_Factuas.SerieFolio) != 0)

                int[] Columnas = { (int)Cols_Factuas.Serie, (int)Cols_Factuas.Folio };

                if (gridFacturas.BuscarRepetidosColumnas(sValor, Columnas) != 0)
                {
                    General.msjUser("Documento electrónico previamente cargado.");
                    txtFolio.Focus();
                }
                else
                {
                    int iRenglon = gridFacturas.Rows + 1;

                    gridFacturas.AddRow();
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.Serie, txtSerie.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.Folio, txtFolio.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.SerieFolio, sValor);

                    gridFacturas.SetValue(iRenglon, Cols_Factuas.Fecha, lblCFDI_FechaExpedicion.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.Cliente, lblCFDI_ClienteNombre.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.FuenteFinanciamiento, lblCFDI_FuenteFinanciamiento.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.Financiamiento, lblCFDI_Financiamiento.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.TipoDocumento, lblCFDI_TipoDocumentoDescripcion.Text);
                    gridFacturas.SetValue(iRenglon, Cols_Factuas.TipoInsumo, lblCFDI_TipoDeInsumoDescripcion.Text);


                    txtSerie.Text = "";
                    txtFolio.Text = "";
                    limpiarDatos_CFDI();

                    txtSerie.Focus();
                }
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void limpiarDatos_CFDI()
        {
            lblCFDI_FechaExpedicion.Text = "";
            lblCFDI_ClienteNombre.Text = "";
            lblCFDI_FuenteFinanciamiento.Text = "";
            lblCFDI_Financiamiento.Text = "";
            lblCFDI_TipoDocumentoDescripcion.Text = "";
            lblCFDI_FechaExpedicion.Text = "";
            lblCFDI_TipoDeInsumoDescripcion.Text = "";
        }
        private void btnLimpiarFacturas_Click(object sender, EventArgs e)
        {
            gridFacturas.Limpiar(false);
        }
        #endregion Documentos de Facturas 
    }
}
