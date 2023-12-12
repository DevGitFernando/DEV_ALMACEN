#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms; 
using System.Threading;
using System.ServiceModel; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IFacturacion.Configuracion; 
#endregion USING

#region USING WEB SERVICES
using Dll_IFacturacion.xsasvrCancelCFDService;
using Dll_IFacturacion.xsasvrCFDService;
using Dll_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 

using Dll_IFacturacion.CFDI;
using Dll_IFacturacion.CFDI.CFDFunctions;
using Dll_IFacturacion.CFDI.CFDFunctionsEx;
using Dll_IFacturacion.CFDI.geCFD; 

namespace Dll_IFacturacion.XSA
{
    public partial class FrmGenerarCDFIs_Masivo : FrmBaseExt
    {
        #region Declaracion de Variables 
        enum Cols
        {
            Ninguna = 0, 
            Remision, FuenteFinanciamiento, SubTotal, IVA, Total, Serie, Folio, Facturar, Procesado, Estatus 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leerFacturar;
        clsLeer leerMetodosDePago;
        clsListView list;
        clsAyudas ayuda;
        clsConsultas consulta;

        clsGrid grid; 

        clsLeer Empresa = new clsLeer();
        clsLeer Sucursal = new clsLeer();
        clsLeer Domicilio = new clsLeer();
        clsLeer DatosReceptor = new clsLeer();
        clsFormaMetodoPago formaMetodoPago;
        DataSet dtsUnidadesDeMedida = new DataSet();
        DataSet dtsListaImpuestos = new DataSet(); 

        private bool bProcesando = false; 
        //private bool bFacturaGenerada = false;
        private string sSerieDocumento = "";
        private string sFolioDocumento = "";
        private string sIdCFDI = "";
        private string sNombreReceptor = ""; 

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;
        private string sFolioRemision = "";
        private bool bInformacionEmisor = false;

        //Thread thRemision;
        Thread thFacturaElectronica;
        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        string sTipoDoctoFactura = "001"; 

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sPrefijo_Folfer = string.Format("");
        string sRutaDestino_Archivos = "";
        bool bFolderDestino = false;

        int iRenglonEnProceso = 0;
        string sFolioIntegracion = "";
        #endregion Declaracion de Variables

        #region Declaracion de Variables de Facturacion 
        xsaCFDI cfdi;
        string xsaDireccionServicioTimbrado = "";
        string sRFC = "";
        string sRFC_Receptor = ""; 
        string sKey = "";
        string sNombreEmpresa_o_Sucursal = "";
        string sNombreDocumento = ""; 
        string sPlantillaDocumento = "";
        //string sTipoDoctoFactura = "001"; 

        string sTipoDeDocumento = ""; 
        bool bErrorAlGenerarFolio = false;
        bool bDocumentoElectronico_Generado = false; 

        double dSubTotal = 0;
        double dTasaIva = 0; 
        double dIva = 0;
        double dTotal = 0;
        double dImporteACobrar = 0;
        string sObservacionesDocumento = "";
        string sObservacionesCondicionesDePago = "";
        string sObservacionesPago = "";

        bool VistaPrevia = false;
        long PK = 0;
        string sReferencia = "";
        string sObservaciones_01 = "";
        string sObservaciones_02 = "";
        string sObservaciones_03 = "";
        int iTipoDocumento = 0;
        int iTipoInsumo = 0;
        string sRubroFinanciamiento = "";
        string sFuenteFinanciamiento = ""; 

        string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        string sXMLCondicionesPago = "Crédito";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = "";
        string sMetodoDePago_Masivo = "";
        string sReferenciaPago_Masivo = ""; 

        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;
        bool bImplementa_ClaveSSA_Base___Identificador = DtIFacturacion.ImplementaClaveSSA_Base__Identificador;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_33_Factura CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_Factura myCFDI = null; //new clsGenCFDI_Ex();
        string sResultadoProceso = ""; 

        DataSet dtsSeries = new DataSet();
        eVersionCFDI tpVersionCFDI = eVersionCFDI.Version__3_3;

        int iDocumentos_A_Procesar = 0;
        int iDocumentos_Procesados = 0;
        int iDocumentos_Generados = 0;
        int iErrores_Generados = 0; 

        #endregion Declaracion de Variables de Facturacion

        public FrmGenerarCDFIs_Masivo() 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            this.sIdEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioRemision = ""; //FolioRemision;

            // Inicializar el CFDI 
            cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);

            // Inicializar el CFDI 
            cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 


            //216, 230

            // this.Top = 212; 
            this.FrameDetalles.Height = 440;
            this.FrameProceso.Top = 230;
            this.FrameProceso.Left = 215; 

            leer = new clsLeer(ref cnn);
            leerFacturar = new clsLeer(ref cnnFacturar); 
            leerMetodosDePago = new clsLeer(); 
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            ////formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel);
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            grid = new clsGrid(ref grdRemisiones, this);
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true; 
            tpPAC = DtIFacturacion.PAC_Informacion.PAC; 

            //// Anexar al documento la Remisión relacionada 
            // sReferencia = "R" + sFolioRemision; 

            lblElapsedTime.BackColor = General.BackColorBarraMenu; 
            General.Pantalla.AjustarAncho(this, 90); 

        }

        #region Propiedades 
        public string IdEmpresa
        {
            get { return sIdEmpresa; }
        }

        public string IdEstado
        {
            get { return sIdEstado; }
        }

        public string IdFarmacia
        {
            get { return sIdFarmacia; }
        }

        public string Remision 
        {
            get { return sFolioRemision; }
        }

        public bool DocumentoGenerado
        {
            get { return bDocumentoElectronico_Generado; }
        }

        public string Serie
        {
            get { return sSerieDocumento; }
        }

        public string Folio
        {
            get { return sFolioDocumento; }
        }

        public string FolioDocumentoElectronico 
        {
            get { return (sSerieDocumento + " - " + sFolioDocumento); }
        }
        #endregion Propiedades 

        #region Form
        private void FrmGenerarCDFIs_Masivo_Load(object sender, EventArgs e)
        {

            InicializaPantalla();
            //TiposDeDocumentos(); 
            ObtenerSeries_y_Folios();
            ObtenerInformacion_Empresa_y_Sucursal();
            CargarFormas_Metodos_DePago();
            UnidadesDeMedida(); 
        }
        #endregion Form

        #region Botones 
        private void InicializaToolBar(bool Facturar)
        {
            InicializaToolBar(true, Facturar); 
        }

        private void InicializaToolBar(bool Nuevo, bool Facturar)
        {
            btnNuevo.Enabled = Nuevo;
            //btnObtenerDetalles.Enabled = Remision;
            // btnObtenerDetalles.Enabled = false; 
            btnFacturar.Enabled = Facturar; 
        }

        private void InicializaPantalla()
        {
            lblTimbresDisponibles.Text = "Consultar timbres disponibles";
            lblElapsedTime.Visible = false;
            lblElapsedTime.Text = ""; 

            tmProceso.Enabled = false;
            tmProceso.Interval = 500; 
            Fg.IniciaControles();
            grid.Limpiar(false);
            grid.BloqueaColumna(false, (int)Cols.Facturar); 


            btnObservacionesGral.Enabled = false;
            btnPago.Enabled = false;
            chkAplicarMascara.Checked = false;


            iDocumentos_A_Procesar = 0; 
            iDocumentos_Procesados = 0;
            iDocumentos_Generados = 0;
            iErrores_Generados = 0;

            dSubTotal = 0;
            dIva = 0;
            dTotal = 0;

            lbl_Proceso_01___TotalDocumentos.Text = "";
            lbl_Proceso_02___DocumentosGenerados.Text = "";
            lbl_Proceso_03___ErroresGenerados.Text = "";

            lbl_Proceso_01___TotalDocumentos.Visible = false;
            lbl_Proceso_02___DocumentosGenerados.Visible = false;
            lbl_Proceso_03___ErroresGenerados.Visible = false; 


            InicializaToolBar(true); 
            MostrarEnProceso(false); 

            bDocumentoElectronico_Generado = false;
            bErrorAlGenerarFolio = false;
            //lblCantidadConLetra.Text = "";

            sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
            sXMLCondicionesPago = "Crédito";
            sXMLMetodoPago = "No identificado";

            formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel, tpVersionCFDI);
            MostrarPago(true); 
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            if (ResetObservaciones())
            {
                sObservacionesDocumento = "";
                sObservacionesCondicionesDePago = sObservacionesDocumento;
                sObservacionesPago = "";

                sObservaciones_01 = "";
                sObservaciones_02 = "";
                sObservaciones_03 = "";
                sReferencia = "";

                iTipoDocumento = 0;
                iTipoInsumo = 0;
                sRubroFinanciamiento = "";
                sFuenteFinanciamiento = ""; 

                formaMetodoPago = new clsFormaMetodoPago();
            }

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\FACTURACION_MASIVA{0}\", sPrefijo_Folfer);
            lblDirectorioTrabajo.Text = sRutaDestino; 
            DtIFacturacion.CrearDirectorio(sRutaDestino);

            //// Peticion de Tamulipas 
            if(DtIFacturacion.ImplementaMascaras)
            {
                chkAplicarMascara.Checked = true;
                //if(DtIFacturacion.ForzarImplementacionDeMascaras)
                {
                    chkAplicarMascara.Enabled = !DtIFacturacion.ForzarImplementacionDeMascaras; 
                }
            }



            IniciarEtiquetasExcel(); 
            txtId.Focus(); 
        }

        private void IniciarEtiquetasExcel()
        {
            int iValor = 0;
            lblSubTotal.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblIva.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblTotal.Text = string.Format("{0}", iValor.ToString(sFormato));

            lblSubTotal_Facturado.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblIva_Facturado.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblTotal_Facturado.Text = string.Format("{0}", iValor.ToString(sFormato));
        }
        private bool ResetObservaciones()
        {
            bool bRegresa = false;
            bool bConfirmar = false;
            string sMensaje = "¿ Desea utilizar las ultimas Observaciones, Referencia e Información de pago capturadas ?"; 

            if (DtIFacturacion.ReutilizarObservacionesCapturadas)
            {
                if (sObservacionesDocumento != "" || sObservacionesCondicionesDePago != "" || sObservacionesPago != "" ||
                    sObservaciones_01 != "" || sObservaciones_02 != "" || sObservaciones_03 != "" || sReferencia != "" ||
                    iTipoDocumento != 0 || iTipoInsumo != 0 || sRubroFinanciamiento != "" || sFuenteFinanciamiento != "")
                {
                    bConfirmar = true;
                }

                if (bConfirmar)
                {
                    bRegresa = General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes;
                }
            }

            return bRegresa;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnConsultarTimbres_Click(object sender, EventArgs e)
        {
            btnConsultarTimbres.Enabled = false;
            Application.DoEvents();

            Dll_IFacturacion.CFDI.Timbrar.clsTimbrar.Preparar_ConexionTimbrado(DtGeneral.EmpresaDatos.RFC, DtIFacturacion.PAC_Informacion);
            lblTimbresDisponibles.Text = string.Format("Timbres disponibles  {0}", Dll_IFacturacion.CFDI.Timbrar.clsTimbrar.ConsultarCreditos());

            Application.DoEvents();
            btnConsultarTimbres.Enabled = true; 
        }


        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = ""; 
            int iTipoDeRemision = 0;
            int iOrigenInsumo = 0;
            int iTipoInsumo = 0;

            if(rdoRM_Producto.Checked)
                iTipoDeRemision = 1;
            if(rdoRM_Servicio.Checked)
                iTipoDeRemision = 2;


            if(rdoOIN_Venta.Checked) iOrigenInsumo = 1;
            if(rdoOIN_Consignacion.Checked) iOrigenInsumo = 2;

            if(rdoInsumoMedicamento.Checked) iTipoInsumo = 1;
            if(rdoInsumoMaterialDeCuracion.Checked) iTipoInsumo = 2;


            sSql = string.Format("Exec spp_FACT_CFD_GetRemisionesFacturables \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', \n" +
                "\t@IdCliente = '{3}', @IdSubCliente = '{4}', \n" +
                "\t@TipoDeRemision = '{5}', @OrigenDeInsumos = '{6}', @TipoDeInsumo = '{7}', @AplicarFiltroFolios = '{8}', \n" +
                "\t@FolioInicial = '{9}', @FolioFinal = '{10}', @AplicarFiltroFechas = '{11}', @FechaInicial = '{12}', @FechaFinal = '{13}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                
                txtCte.Text, txtSubCte.Text,

                iTipoDeRemision, iOrigenInsumo, iTipoInsumo,
                Convert.ToInt32(chkFolios.Checked), txtFolioInicial.Text, txtFolioFinal.Text,
                Convert.ToInt32(chkFechas.Checked), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value)

                );


            grid.Limpiar(false);
            grid.BloqueaColumna(false, (int)Cols.Facturar); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click()");
                General.msjError("Ocurrió un error al obtener el listado de remisiones disponibles para facturar."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontraron Remisiones disponibles para facturar, verifique.");
                }
                else
                {
                    grid.LlenarGrid(leer.DataSetClase, false, false); 
                }
            }

            lblSubTotal.Text = string.Format("{0}", grid.TotalizarColumnaDou((int)Cols.SubTotal).ToString(sFormato));
            lblIva.Text = string.Format("{0}", grid.TotalizarColumnaDou((int)Cols.IVA).ToString(sFormato));
            lblTotal.Text = string.Format("{0}", grid.TotalizarColumnaDou((int)Cols.Total).ToString(sFormato));  
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            VistaPrevia = false; 
            if (validarGenerarDocumento())
            {
                Preparar_Tablas_TimbradoMasivo();

                DtIFacturacion.CrearDirectorio(sRutaDestino); 
                VistaPrevia = false;
                thGenerarDocto(); 
            }
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            VistaPrevia = true;
            if (validarGenerarDocumento())
            {
                VistaPrevia = true;
                thGenerarDocto();
            }
        }

        private void btnObservacionesGral_Click(object sender, EventArgs e)
        {
            FrmObservaciones f = new FrmObservaciones();

            f.FacturacionDeRemisiones = true;
            f.sObservaciones_01 = sObservaciones_01;
            f.sObservaciones_02 = sObservaciones_02;
            f.sObservaciones_03 = sObservaciones_03;
            f.sReferencia = sReferencia;
            f.iTipoDocumento = iTipoDocumento;
            f.iTipoInsumo = iTipoInsumo; 

            f.ShowDialog();

            sObservaciones_01 = f.sObservaciones_01;
            sObservaciones_02 = f.sObservaciones_02;
            sObservaciones_03 = f.sObservaciones_03;
            sReferencia = f.sReferencia;
            iTipoDocumento = f.iTipoDocumento;
            iTipoInsumo = f.iTipoInsumo; 
        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            MostrarPago(false);
        }

        private void MostrarPago(bool CierreAutomatico)
        {
            dImporteACobrar = Convert.ToDouble("0" + lblTotal.Text.Replace(",", "").Replace(" ", ""));
            formaMetodoPago.ImporteACobrar = dImporteACobrar;
            formaMetodoPago.Show(CierreAutomatico); 
        }

        private void btnCondicionesDePago_Click(object sender, EventArgs e)
        {
            clsObservaciones f = new clsObservaciones();
            f.Encabezado = "Condiciones de pago";
            f.MaxLength = 250; 
            f.Show();
            if (f.Exito)
            {
                sObservacionesCondicionesDePago = f.Observaciones;
            }
        }

        private void btnObservaciones_Click(object sender, EventArgs e)
        {
            clsObservaciones f = new clsObservaciones();
            f.Encabezado = "Observaciones de pago";
            f.MaxLength = 250;
            f.Show();
            if (f.Exito)
            {
                sObservacionesPago = f.Observaciones;
            }
        }
        private void btlCliente_Click(object sender, EventArgs e)
        {
            FrmFactClientes f = new FrmFactClientes(); 
            f.MostrarClientes(txtId.Text.Trim());

            if (f.ClienteNuevo)
            {
                txtId.Enabled = false; 
                txtId.Text = f.Cliente; 
                lblCliente.Text = f.ClienteNombre;
                txtClienteNombre.Text = f.ClienteNombre;
                txtId_Validating(null, null); 
            }
        }
        #endregion Botones

        #region Informacion de Empresa y Sucursal 
        private void ObtenerInformacion_Empresa_y_Sucursal()
        {
            string sSql = "";

            sSql = string.Format("Select  " +
                 " IdEmpresa, Nombre, RFC, KeyLicencia, NombreProveedor, DireccionUrl, Telefonos, Status, Actualizado " +
                 " From FACT_CFD_Empresas (NoLock) " +
                 " Where IdEmpresa = '{0}' \n\n", sIdEmpresa);

            sSql += string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Nombre, RFC, Status, Actualizado " +
                " From FACT_CFD_Sucursales (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n\n",
                sIdEmpresa, sIdEstado, sIdFarmacia);

            sSql += string.Format("Select Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia " +
                " From FACT_CFD_Sucursales_Domicilio (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n\n",
                sIdEmpresa, sIdEstado, sIdFarmacia);

            sSql = string.Format("Exec spp_Mtto_FACT_CFD_Informacion_Empresa_y_Sucursal '{0}', '{1}', '{2}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia);


            bInformacionEmisor = false;
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion_Empresa_y_Sucursal"); 
            }
            else
            {
                if (leer.Leer())
                {
                    bInformacionEmisor = true; 
                    leer.RenombrarTabla(1, "Empresa");
                    leer.RenombrarTabla(2, "Sucursal");
                    leer.RenombrarTabla(3, "Domicilio");

                    Empresa.DataTableClase = leer.Tabla("Empresa");
                    Sucursal.DataTableClase = leer.Tabla("Sucursal");
                    Domicilio.DataTableClase = leer.Tabla("Domicilio");

                    DatosTimbrado(); 
                }
            }
        }

        private void DatosTimbrado()
        {
            Empresa.Leer();
            Sucursal.Leer();
            Domicilio.Leer(); 

            sRFC = Empresa.Campo("RFC");
            sKey = Empresa.Campo("KeyLicencia"); 
            xsaDireccionServicioTimbrado = Empresa.Campo("DireccionUrl"); 
            sNombreEmpresa_o_Sucursal = Sucursal.Campo("Nombre");
        }
        #endregion Informacion de Empresa y Sucursal

        #region Formas y Metodos de Pago 
        private void CargarFormas_Metodos_DePago()
        {
            //////cboFormasDePago.Clear();
            //////cboFormasDePago.Add();
            //////cboMetodosDePago.Clear();
            //////cboMetodosDePago.Add();

            //////cboFormasDePago.Add(consulta.CFDI_FormasDePago(""), true, "Descripcion", "NombreFormaDePago");
            //////cboMetodosDePago.Add(consulta.CFDI_MetodosDePago(""), true, "Descripcion", "NombreMetodoDePago"); 

            //////cboFormasDePago.SelectedIndex = 0;
            //////cboMetodosDePago.SelectedIndex = 0; 
        }
        #endregion Formas y Metodos de Pago 

        #region Series y Folios 
        private void TiposDeDocumentos()
        {
            ////cboTiposDocumentos.Clear();
            ////cboTiposDocumentos.Add();
            ////cboTiposDocumentos.Filtro = string.Format(" Status = 'A' ");
            ////cboTiposDocumentos.Add(consulta.CFDI_TipoDeDocumentos("", "TiposDeDocumentos()"), true, "IdTipoDocumento", "Documento"); 
            ////cboTiposDocumentos.SelectedIndex = 0; 
        } 

        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add();
            dtsSeries = consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, "", "ObtenerSeries_y_Folios()");
            cboSeries.Filtro = string.Format(" IdTipoDocumento = '{0}' ", sTipoDoctoFactura);
            cboSeries.Add(dtsSeries, true, "Serie", "Serie"); 

            // cboSeries.Add(consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoFactura, "ObtenerSeries_y_Folios()"), true, "Serie", "Serie");  
            cboSeries.SelectedIndex = 0; 
        }
        #endregion Series y Folios

        #region Unidades de Medida 
        private void UnidadesDeMedida()
        {
            dtsUnidadesDeMedida = consulta.CFDI_UnidadesDeMedida("", "UnidadesDeMedida()"); 
        }
        #endregion Unidades de Medida

        #region Validaciones
        private bool validarRemision()
        {
            bool bRegresa = false;

            ////if (General.msjConfirmar("El proceso de obtención de detalles de remisión puede demorar unos minutos, ¿ Desea continuar ?") == DialogResult.Yes)
            ////{
            ////    bRegresa = true; 
            ////}

            return bRegresa; 
        }

        private bool validarGenerarDocumento()
        {
            bool bRegresa = true;
            sNombreReceptor = txtClienteNombre.Text;

            if (!bInformacionEmisor)
            {
                bRegresa = false;
                General.msjUser("No se cuenta con la información de la sucursal, no es posible generar la factura sin estos datos."); 
            }

            if (bRegresa & txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Cliente al cual se le emitira la factura, verifique.");
                txtId.Focus(); 
            }

            if (bRegresa && cboSeries.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Serie válida, verifique."); 
                cboSeries.Focus(); 
            }

            if (bRegresa && grid.Rows <= 0 )
            {
                bRegresa = false;
                General.msjUser("No existen remisiones para facturar, verifique."); 
            }

            //if (bRegresa && formaMetodoPago.IdFormaDePago == "0")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado una Forma de pago válida, verifique."); 
            //    //cboFormasDePago.Focus(); 
            //    btnPago.Focus(); 
            //}

            //if (bRegresa && formaMetodoPago.IdMetodoPago == "0" )
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado un Método de pago válido, verifique.");
            //    //cboMetodosDePago.Focus();
            //    btnPago.Focus(); 
            //}

            return bRegresa; 
        }
        #endregion Validaciones

        #region Funciones y Procedimientos Privados
        private void TituloProceso(string Titulo)
        {
            FrameProceso.Text = Titulo; 
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 140;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void cboSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            // lblUltimoFolio.Text = "";
            if (cboSeries.SelectedIndex != 0)
            {
            //    lblUltimoFolio.Text = cboSeries.ItemActual.GetItem("FolioUtilizado"); 
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtClienteNombre.Text = ""; 
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text != "")
            {
                leer.DataSetClase = consulta.CFDI_Clientes(txtId.Text, "txtId_Validating"); 
                if( leer.Leer())
                {
                    DatosCliente(); 
                } 
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.CFDI_Clientes("txtId_KeyDown");
                if (leer.Leer())
                {
                    DatosCliente();
                }
            }
        }

        private void DatosCliente()
        {
            // txtId.Enabled = false; 
            if (leer.Campo("Status").ToUpper() != "A")
            {
                txtId.Text = "";
                General.msjAviso("El cliente capturado esta cancelado, no es posible emitirle documentos.");
                txtId.Focus();
            }
            else
            {
                txtId.Text = leer.Campo("IdCliente");
                lblCliente.Text = leer.Campo("Nombre");
                txtClienteNombre.Text = leer.Campo("Nombre");
                DatosReceptor.DataSetClase = leer.DataSetClase;
                //DatosDeReceptor(); 
            }
        }

        private void tmProceso_Tick(object sender, EventArgs e)
        {
            if (!bProcesando) 
            {
                tmProceso.Stop(); 
                tmProceso.Enabled = false; 
                BloquearControles(false); 

                //////// No fue posible generar el folio de factura electronica. 
                ////if (bErrorAlGenerarFolio)
                ////{
                ////    IniciaToolBarExcel(true, true, true, true, true);
                ////    General.msjAviso("No fue posible generar el documento electrónico, intente de nuevo.");  
                ////}
                ////else
                ////{
                ////    if (tpPAC == PACs_Timbrado.Tralix)
                ////    {
                ////        ImprimirDocumento();
                ////    }
                ////    else
                ////    {

                ////        ImprimirDocumento_Timbrado();
                ////        if (!VistaPrevia)
                ////        {
                ////            InicializaPantalla();
                ////        }
                ////    }
                ////}
            }
        }
        #endregion Eventos 

        #region Generar Factura Electrónica 
        private void BloquearControles(bool Bloquear)
        {
            Bloquear = !Bloquear; 
            btnNuevo.Enabled = Bloquear;
            btnFacturar.Enabled = Bloquear;
            btnDirectorio.Enabled = Bloquear;

            chkMarcarDesmarcarTodo.Enabled = Bloquear; 
            txtId.Enabled = Bloquear;
            txtClienteNombre.Enabled = Bloquear; 
            btlCliente.Enabled = Bloquear;
            cboSeries.Enabled = Bloquear;
            //btnObservacionesGral.Enabled = Bloquear;
            //btnPago.Enabled = Bloquear;

        }

        private void thGenerarDocto()
        {

            // Control del Timer 
            bProcesando = true;

            tmProceso.Enabled = true;
            tmProceso.Start();

            BloquearControles(true);

            thFacturaElectronica = new Thread(Timbrar);
            thFacturaElectronica.Name = "Timbrado masivo de facturas";
            thFacturaElectronica.Start(); 
        }

        ////private ArrayList GetExtras(DataSet CamposAdicionales)
        ////{
        ////    ArrayList Datos = new ArrayList();
        ////    DatosExtraImpresion dato = new DatosExtraImpresion();
        ////    clsLeer datosAdicionales = new clsLeer();
        ////    datosAdicionales.DataSetClase = CamposAdicionales;
        ////    datosAdicionales.Leer();


        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "StatusDocumento";
        ////    dato.Valor = datosAdicionales.Campo("StatusDocumento");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Observaciones_01";
        ////    dato.Valor = datosAdicionales.Campo("Observaciones_01");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Observaciones_02";
        ////    dato.Valor = datosAdicionales.Campo("Observaciones_02");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Observaciones_03";
        ////    dato.Valor = datosAdicionales.Campo("Observaciones_03");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Referencia";
        ////    dato.Valor = datosAdicionales.Campo("Referencia");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Referencia_02";
        ////    dato.Valor = datosAdicionales.Campo("Referencia_02");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Referencia_03";
        ////    dato.Valor = datosAdicionales.Campo("Referencia_03");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Referencia_04";
        ////    dato.Valor = datosAdicionales.Campo("Referencia_04");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "Referencia_05";
        ////    dato.Valor = datosAdicionales.Campo("Referencia_05");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "TipoDeDocumento";
        ////    dato.Valor = datosAdicionales.Campo("TipoDocumentoDescripcion");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "TipoDeInsumo";
        ////    dato.Valor = datosAdicionales.Campo("TipoInsumoDescripcion");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "RubroFinanciamiento";
        ////    dato.Valor = datosAdicionales.Campo("RubroFinanciamento");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "FuenteFinanciamiento";
        ////    dato.Valor = datosAdicionales.Campo("Financiamiento");
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "CantidadLetra";
        ////    dato.Valor = General.LetraMoneda(datosAdicionales.CampoDouble("Total"));
        ////    Datos.Add(dato);

        ////    dato = new DatosExtraImpresion();
        ////    dato.Nombre = "FechaCancelacionCFDI";
        ////    dato.Valor = datosAdicionales.Campo("FechaCancelacionDocumento");
        ////    Datos.Add(dato);

        ////    Datos.Add(dato);

        ////    return Datos;
        ////}

        private bool ImprimirDocumento_Timbrado()
        {
            bool bRegresa = false;
            bool bVisualizar = true;
            string sImpresion = VistaPrevia ? " spp_FACT_CFDI_GetListaComprobantes_VP " : " spp_FACT_CFDI_GetListaComprobantes ";
            string sSql = string.Format("Exec spp_FACT_CFDI_GetListaComprobantes @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, PK);

            string sMensaje = "¿ Desea ver la vista previa del documento electrónico ? ";
            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir()");
            }
            else
            {
                if (!leer.Leer())
                {
                }
                else
                {
                    sFileName = leer.Campo("NombreFiles");
                    sXML = leer.Campo("uf_Xml_Timbrado");
                    sPDF = leer.Campo("uf_xml_Impresion");
                    sEmail = leer.Campo("EmailCliente");

                    bRegresa = true;
                    DtIFacturacion.InvocaVisor = this;
                    DtIFacturacion.EsTimbradoMasivo = true;
                    DtIFacturacion.DirectorioAlternoArchivosGenerados = sRutaDestino; 
                    DtIFacturacion.GenerarImpresionWebBrowser(VistaPrevia, sFileName, true, DtIFacturacion.GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null, true);
                }
            }

            return bRegresa;
        }

        private void Timbrar()
        {
            //GenerarTimbre();
            MostrarEnProceso(true);

            BloquearControles(true);
            grid.BloqueaColumna(true, (int)Cols.Facturar);
            grid.SetValue((int)Cols.Estatus, "");

            sSerieDocumento = cboSeries.Data;
            sXMLFormaPago = formaMetodoPago.IdFormaDePago;
            sXMLCondicionesPago = formaMetodoPago.ObservacionesPago;
            sXMLMetodoPago = formaMetodoPago.MetodoPago;
            sXMLObservacionesPago = formaMetodoPago.Observaciones;
            
            leerMetodosDePago.DataSetClase = formaMetodoPago.ListaDeMetodosDePago;
            if (leerMetodosDePago.Leer())
            {
                sMetodoDePago_Masivo = leerMetodosDePago.Campo("IdFormaDePago");
                sReferenciaPago_Masivo = leerMetodosDePago.Campo("Referencia"); 
            }

            iDocumentos_A_Procesar = grid.TotalizarColumna((int)Cols.Facturar);
            iDocumentos_Procesados = 0;
            iDocumentos_Generados = 0;
            iErrores_Generados = 0;

            dSubTotal = 0;
            dIva = 0;
            dTotal = 0;

            lbl_Proceso_01___TotalDocumentos.Text = "";
            lbl_Proceso_02___DocumentosGenerados.Text = "";
            lbl_Proceso_03___ErroresGenerados.Text = "";

            lbl_Proceso_01___TotalDocumentos.Visible = true;
            lbl_Proceso_02___DocumentosGenerados.Visible = true;
            lbl_Proceso_03___ErroresGenerados.Visible = true;


            lbl_Proceso_01___TotalDocumentos.Text = string.Format("Procesado {0} de {1} ", 0, iDocumentos_A_Procesar);

            lblElapsedTime.Visible = true;
            General.Cronometro.Etiqueta = lblElapsedTime;
            General.Cronometro.Start(); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Facturar))
                {
                    iRenglonEnProceso = i;
                    sFolioRemision = grid.GetValue(i, (int)Cols.Remision);

                    grid.SetValue(iRenglonEnProceso, (int)Cols.Estatus, "Procesando");

                    Guardar_Documento();
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);

                    lbl_Proceso_01___TotalDocumentos.Text = string.Format("Procesado {0} de {1} ", iDocumentos_Procesados, iDocumentos_A_Procesar);
                    lbl_Proceso_02___DocumentosGenerados.Text = string.Format("Documentos generados :  {0} ", iDocumentos_Generados);
                    lbl_Proceso_03___ErroresGenerados.Text = string.Format("Errores detectados :  {0} ", iErrores_Generados);


                    lblSubTotal_Facturado.Text = string.Format("{0}", dSubTotal.ToString(sFormato));
                    lblIva_Facturado.Text = string.Format("{0}", dIva.ToString(sFormato));
                    lblTotal_Facturado.Text = string.Format("{0}", dTotal.ToString(sFormato));  
                }
            }

            MostrarEnProceso(false);

            // Control del Timer 
            bProcesando = false;
            General.Cronometro.Stop(); 
        }

        private bool Guardar_Documento()
        {
            bool bRegresa = false;

            if (!cnnFacturar.Abrir())
            {
                grid.SetValue(iRenglonEnProceso, "Error de conexión");
            }
            else
            {
                cnnFacturar.IniciarTransaccion();

                bRegresa = GenerarTimbre_PAC();

                if (!bRegresa)
                {
                    iErrores_Generados++;
                    PK = 0;
                    Error.GrabarError(leerFacturar, "Guardar_Documento()");
                    cnnFacturar.DeshacerTransaccion();
                    grid.SetValue(iRenglonEnProceso, (int)Cols.Estatus, string.Format("Error al generar CFDI ... {0}", sResultadoProceso));
                }
                else
                {
                    cnnFacturar.CompletarTransaccion();
                    iDocumentos_Generados++; 
                    ////if (!VistaPrevia)
                    {
                        ////txtFolio.Text = Fg.PonCeros(sFolio, 10);
                        grid.SetValue(iRenglonEnProceso, (int)Cols.Serie, sSerieDocumento);
                        grid.SetValue(iRenglonEnProceso, (int)Cols.Folio, sFolioDocumento);
                        grid.SetValue(iRenglonEnProceso, (int)Cols.Estatus, "CFDI generado.");

                        dSubTotal += grid.GetValueDou(iRenglonEnProceso, (int)Cols.SubTotal);
                        dIva += grid.GetValueDou(iRenglonEnProceso, (int)Cols.IVA);
                        dTotal += grid.GetValueDou(iRenglonEnProceso, (int)Cols.Total);
                    }
                }

                cnnFacturar.Cerrar();

                if (bRegresa)
                {
                    //Imprimir(sImpresion, VistaPrevia);
                    ImprimirDocumento_Timbrado(); 
                }
            }


            iDocumentos_Procesados++;
            return bRegresa;
        }


        #endregion Generar Factura Electrónica 

        #region Timbrado 
        private bool GenerarTimbre()
        {
            bool bRegresa = false; 

            switch (tpPAC)
            {
                case PACs_Timbrado.Tralix:
                    bRegresa = GenerarTimbre_Tralix();
                    break;

                default:
                    sTipoDeDocumento = cboSeries.ItemActual.GetItem("TipoDeDocumento");
                    CFDI.Timbrar.clsTimbrar.TipoDeDocumento = sTipoDeDocumento; 
                    bRegresa = GenerarTimbre_PAC();
                    break;
            }

            return bRegresa;
        }

        private bool GenerarTimbre_Tralix()
        {
            bool bRegresa = false;
            string sTipoDocumento = xsaTipoDocumento.TipoDeDocumento(tipoDocumento);
            sTipoDocumento = cboSeries.ItemActual.GetItem("NombreDocumento");

            xsawsGenerarDocumento documento = new xsawsGenerarDocumento(xsaDireccionServicioTimbrado, General.DatosConexion);
            bRegresa = documento.GenerarDocumento(sKey, sRFC, sNombreEmpresa_o_Sucursal, sTipoDocumento, sNombreDocumento, sPlantillaDocumento);

            return bRegresa;
        }

        private bool Preparar_Tablas_TimbradoMasivo()
        {
            bool bRegresa = false;
            string sSql = "Exec spp_FACT___Preparar_Timbrado_Masivo "; 

            leerFacturar.Exec(sSql); 

            return bRegresa; 
        }


        private bool GenerarTimbre_PAC()
        {
            bool bRegresa = false;
            string sSql = "";
            string sGUID = Guid.NewGuid().ToString(); 
            long pk = PK;
            long pkResult = 0;
            int iAplicarMascara = chkAplicarMascara.Checked ? 1 : 0; 
            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI); 
            

            sSql = string.Format("Exec spp_FACT_CFDI_GetInformacionTimbrado \n " +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}', @Aplicar_Mascara = '{4}', \n " +
                "\t@IdPersonal = '{5}', @IdClienteFiscal = '{6}', @Serie = '{7}', @XMLFormaPago = '{8}', @XMLCondicionesPago = '{9}', \n " +
                "\t@XMLMetodoPago = '{10}', @MetodoDePago = '{11}', @ReferenciaDePago = '{12}', @Identificador_UUID = '{13}', @IncrementarFolios = '{14}', @UsoDeCFDI = '{15}' \n ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRemision, iAplicarMascara, DtGeneral.IdPersonal,
                txtId.Text.Trim(), sSerieDocumento, sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago, sMetodoDePago_Masivo, sReferenciaPago_Masivo, 
                sGUID, 1, DtIFacturacion.Factura_UsoCDFI);

            if (!leerFacturar.Exec(sSql))
            {
                Error.GrabarError(leerFacturar, "GenerarTimbrar()");                 
            }
            else
            {
                if (!leerFacturar.Leer())
                {
                    sResultadoProceso = "No se encontro información para generar la factura.";
                }
                else
                {
                    clsLeer leerRespuesta = new clsLeer();
                    CFDI_FD = new clsGenCFDI_33_Factura();
                    CFDI_FD.Conexion = cnnFacturar;
                    CFDI_FD.myComprobante.DatosComprobante = leerFacturar.DataSetClase;

                    leerRespuesta.DataTableClase = leerFacturar.Tabla(1); 
                    leerRespuesta.Leer();
                    sFolioDocumento = leerRespuesta.CampoInt("Folio").ToString();
                    PK = leerRespuesta.CampoInt("Keyx"); 

                    CFDI_FD.EsVistaPrevia = VistaPrevia;
                    CFDI_FD.PAC = tpPAC;
                    pkResult = CFDI_FD.obtenerFacturaElectronica(CFDI_FD.myComprobante);
                    bRegresa = pkResult > 0;

                    if (!bRegresa)
                    {
                        sResultadoProceso = CFDI_FD.LastError; 
                    }
                    else 
                    {
                        sResultadoProceso = "CFDI generado satisfactoriamente."; 
                    }
                }
            }

            return bRegresa;
        }

        private bool DescargarDocumentos()
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.Tralix:
                    bRegresa = DescargarDocumentos_Tralix();
                    break;

                default:
                    bRegresa = true;
                    break;
            }

            return bRegresa;
        }

        private bool DescargarDocumentos_Tralix()
        {
            bool bRegresa = false;
            xsawsDescargarDocumentos docto = new xsawsDescargarDocumentos(xsaDireccionServicioTimbrado, General.DatosConexion);

            bRegresa = docto.Descargar(sRFC, sKey, cfdi.Encabezado.Serie, cfdi.Encabezado.Folio);
            cfdi.Documento_PDF = docto.PDF;
            cfdi.Documento_XML = docto.XML;

            return bRegresa;
        }
        #endregion Timbrado  

        #region General 
        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                sRutaDestino += string.Format(@"\FACTURACION_MASIVA{0}\", sPrefijo_Folfer);
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
                DtIFacturacion.CrearDirectorio(sRutaDestino); 
            } 
        }

        private void chkMarcarDesmarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Facturar, chkMarcarDesmarcarTodo.Checked); 
        }
        #endregion General

        #region Base Remisiones
        private void rdoBaseRemision_Normal_CheckedChanged(object sender, EventArgs e)
        {
            chkGenerarNotasDeCredito.Enabled = false; 
        }

        private void rdoBaseRemision_AsociadaFactura_CheckedChanged(object sender, EventArgs e)
        {
            chkGenerarNotasDeCredito.Enabled = true; 
        }
        #endregion Base Remisiones
    }
}
