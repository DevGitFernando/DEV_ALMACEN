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
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
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
using Dll_IFacturacion.CFDI.geCFD_33;

using DllFarmaciaSoft.wsFarmacia;

namespace Dll_IFacturacion.XSA
{
    public partial class FrmCFDI_ComplementoDePago : FrmBaseExt
    {
        #region Declaracion de Variables
        enum Cols
        {
            Ninguna = 0,

            Serie = 1, Folio, UUID, Parcialidad, ValorNominal, TotalPagado, SaldoAnterior, Abono, SaldoFinal            
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer leer;
        clsLeer leerFacturar;
        clsListView list;
        clsGrid grid; 
        clsAyudas ayuda;
        clsConsultas consulta;
        clsDatosCliente DatoCliente;
        clsLeerWebExt leerWeb;
        wsCnnCliente conexionWeb;
        string sUrl = "";

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

        Cols ColActiva = Cols.Ninguna;
        eVersionCFDI tpVersionCFDI = eVersionCFDI.Version__3_3;

        //Thread thRemision;
        Thread thFacturaElectronica;
        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        string sTipoDoctoCFDI = "007";

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";

        bool bLimpiando = true; 
        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bErrorAlCargarPlantilla = false;
        string sMensajeError_CargarPlantilla = "";

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
        //string sTipoDoctoCFDI = "001"; 

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
        bool bErrorEnImportes = false;

        string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        string sXMLCondicionesPago = "Crédito";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = "";
        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;
        bool bImplementa_ClaveSSA_Base___Identificador = DtIFacturacion.ImplementaClaveSSA_Base__Identificador;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_33_Pago CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_Pago myCFDI = null; //new clsGenCFDI_Ex();


        DataSet dtsSeries = new DataSet();
        DataSet dtsCuentas_Emisor = new DataSet();
        DataSet dtsCuentas_Receptor = new DataSet();

        string sStoreEnc = "";
        string sStoreDet_Detalles = "";
        string sStoreDet_MetodoDePago = "";
        string sStoreDet_Detalles_UUID = "";

        //             string sStoreDet_MetodoDePago = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago ";

        //string sStoreEnc_Real = " spp_Mtto_FACT_CFDI_PG_01__Pagos ";
        string sStoreEnc_Real = " spp_Mtto_FACT_CFD_Documentos_Generados ";
        string sStoreDet_Detalles_Real = " spp_Mtto_FACT_CFDI_PG_02__Pagos_Detalles ";
        string sStoreDet_MetodoDePago_Real = " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago ";
        string sStoreDet_Detalles_UUID_Real = " spp_Mtto_FACT_CFDI_PG_03__Pagos_DoctosRelacionados ";

        //string sStoreEnc_VP = " spp_Mtto_FACT_CFDI_PG_01__Pagos_VP ";
        string sStoreEnc_VP = " spp_Mtto_FACT_CFD_Documentos_Generados_VP ";
        string sStoreDet_Detalles_VP = " spp_Mtto_FACT_CFDI_PG_02__Pagos_Detalles_VP ";
        string sStoreDet_MetodoDePago_VP = " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP ";
        string sStoreDet_Detalles_UUID_VP = " spp_Mtto_FACT_CFDI_PG_03__Pagos_DoctosRelacionados_VP ";



        #endregion Declaracion de Variables de Facturacion

        public FrmCFDI_ComplementoDePago()
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

            // this.Top = 212; 
            this.FrameDetalles.Height = 440;
            this.FrameProceso.Top = 120;


            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            DatoCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();


            ////formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel);
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            //list = new clsListView(listvClaves);
            grid = new clsGrid(ref grdUUIDs, this);
            grid.AjustarAnchoColumnasAutomatico = true;
            grdUUIDs.EditModeReplace = true;
            chkCargaManual.BackColor = toolStrip1.BackColor; 

            tpPAC = DtIFacturacion.PAC_Informacion.PAC;

            tmProceso_LoadInformacion.Enabled = false;

            //// Anexar al documento la Remisión relacionada 
            // sReferencia = "R" + sFolioRemision; 

            //chkClaveSSA_Base___EsIdentificador.BackColor = General.BackColorBarraMenu;
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
        private void FrmCFDI_ComplementoDePago_Load(object sender, EventArgs e)
        {
            //list.LimpiarItems();
            //listvClaves.Items.Clear();
            grid.Limpiar(true); 

            InicializaPantalla();
            //TiposDeDocumentos(); 
            Obtener_UsoCFDI();
            ObtenerSeries_y_Folios();
            ObtenerInformacion_Empresa_y_Sucursal();

            InicializarCombos_Cuentas();
            Cargar__FormasDePago(); 
            Cargar__Monedas();
            ObtenerServidoresRemotos();
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
            bLimpiando = true;

            lblTimbresDisponibles.Text = "Consultar timbres disponibles";
            ////lblCantidadConLetra.Text = "";
            ////lblCantidadConLetra.BorderStyle = BorderStyle.None;

            ////chkClaveSSA_Base___EsIdentificador.Checked = false;
            ////chkClaveSSA_Base___EsIdentificador.Visible = bImplementa_ClaveSSA_Base___Identificador;
            ////chkClaveSSA_Base___EsIdentificador.Enabled = chkClaveSSA_Base___EsIdentificador.Visible;

            grid.Limpiar(true); 
            //grid.BloqueaGrid(true); 


            tmProceso.Enabled = false;
            tmProceso.Interval = 500;
            Fg.IniciaControles();
            //list.LimpiarItems(); 
            //listvClaves.Items.Clear();

            InicializaToolBar(true);
            MostrarEnProceso(false);

            bDocumentoElectronico_Generado = false;
            bErrorAlGenerarFolio = false;
            //lblCantidadConLetra.Text = "";

            sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
            sXMLCondicionesPago = "Crédito";
            sXMLMetodoPago = "No identificado";

            formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel, tpVersionCFDI);
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            btnValidarDatos.Visible = DtIFacturacion.PAC_Informacion.PAC != PACs_Timbrado.Tralix;
            btnValidarDatos.Enabled = btnValidarDatos.Visible;
            toolStripSeparatorVistaPrevia.Visible = btnValidarDatos.Visible;

            txtRelacion__Serie.Enabled = false;
            txtRelacion__Folio.Enabled = false;
            txtRelacion__UUID.Enabled = false;

            txtRelacion__Serie.ReadOnly = false;
            txtRelacion__Folio.ReadOnly = false;
            txtRelacion__UUID.ReadOnly = false;


            nmImportePago.ReadOnly = true; 
            sFile_In = "";
            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;
            bErrorEnImportes = false;
            IniciarEtiquetasExcel();
            IniciaToolBarExcel(true, true, false, false, false);

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

            bLimpiando = false;
            txtId.Focus();
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

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            VistaPrevia = false;
            if (validarGenerarDocumento())
            {
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
            ////try
            ////{
            ////    //CierreAutomatico = false; 
            ////    dImporteACobrar = Convert.ToDouble("0" + lblTotal.Text.Replace(",", "").Replace(" ", ""));
            ////    formaMetodoPago.ImporteACobrar = dImporteACobrar;
            ////    formaMetodoPago.Show(CierreAutomatico);
            ////}
            ////catch (Exception ex)
            ////{
            ////}
        }

        private void btnCondicionesDePago_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    clsObservaciones f = new clsObservaciones();
            ////    f.Encabezado = "Condiciones de pago";
            ////    f.MaxLength = 250;
            ////    f.Show();
            ////    if (f.Exito)
            ////    {
            ////        sObservacionesCondicionesDePago = f.Observaciones;
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////}
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
            ////FrmFactClientes f = new FrmFactClientes();
            ////f.MostrarClientes(txtId.Text.Trim());

            ////if (f.ClienteNuevo)
            ////{
            ////    txtId.Enabled = false;
            ////    txtId.Text = f.Cliente;
            ////    lblCliente.Text = f.ClienteNombre;
            ////    txtClienteNombre.Text = f.ClienteNombre;
            ////    txtId_Validating(null, null);
            ////}
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

        #region Cargar combos 
        private void InicializarCombos_Cuentas()
        {
            Cargar__InformacionEmisor();
            Cargar__InformacionReceptor();

            Inicializar_Cbo_CuentaEmisor();
            Inicializar_Cbo_CuentaBeneficiario();
        }

        private void Cargar__InformacionEmisor()
        {
            cboEmisor_RFC_BancoOrigen.Clear();
            cboEmisor_RFC_BancoOrigen.Add("", "<< Seleccione >>");

            cboEmisor_RFC_BancoOrigen.Add(consulta.CFDI_Pagos__EmisorBancos_Cuentas(false, ""), true, "RFC_Banco", "NombreBanco");
            cboEmisor_RFC_BancoOrigen.SelectedIndex = 0;

            dtsCuentas_Emisor = consulta.CFDI_Pagos__EmisorBancos_Cuentas(true, ""); 
        }

        private void Cargar__InformacionReceptor()
        {
            cboReceptor_RFC_BancoDestino.Clear();
            cboReceptor_RFC_BancoDestino.Add("", "<< Seleccione >>");

            cboReceptor_RFC_BancoDestino.Add(consulta.CFDI_Pagos__ReceptorBancos_Cuentas(false, ""), true, "RFC_Banco", "NombreBanco");
            cboReceptor_RFC_BancoDestino.SelectedIndex = 0;

            dtsCuentas_Receptor = consulta.CFDI_Pagos__ReceptorBancos_Cuentas(true, "");  
        }

        private void Cargar__FormasDePago()
        {
            cboFormasDePago.Clear();
            cboFormasDePago.Add("", "<< Seleccione >>");
            cboFormasDePago.Add(consulta.CFDI_ComplementoPagos__FormasDePago(DtIFacturacion.Pago_FormaDePago, "Cargar__FormasDePago()"), true, "IdFormaDePago", "Descripcion_FormaDePago");
            cboFormasDePago.SelectedIndex = 0;
        }

        private void Cargar__Monedas()
        {
            cboMonedas.Clear();
            cboMonedas.Add("", "<< Seleccione >>");
            cboMonedas.Add(consulta.CFDI_Monedas(DtIFacturacion.Pago_Moneda, ""), true, "Clave", "Moneda");
            cboMonedas.SelectedIndex = 0;

            if (DtIFacturacion.Pago_Moneda != "")
            {
                cboMonedas.Data = DtIFacturacion.Pago_Moneda;
                cboMonedas.Enabled = false; 
            }
        }

        private void Obtener_UsoCFDI()
        {
            cboUsosCFDI.Clear();
            cboUsosCFDI.Add();
            cboUsosCFDI.Add(consulta.CFDI_UsosDeCFDI("Obtener_UsoCFDI()"), true, "Clave", "UsoDeCFDI");
            cboUsosCFDI.SelectedIndex = 0;
        }

        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add();
            dtsSeries = consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, "", "ObtenerSeries_y_Folios()");
            cboSeries.Filtro = string.Format(" IdTipoDocumento = '{0}' ", sTipoDoctoCFDI);
            cboSeries.Add(dtsSeries, true, "Serie", "Serie");

            // cboSeries.Add(consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoCFDI, "ObtenerSeries_y_Folios()"), true, "Serie", "Serie");  
            cboSeries.SelectedIndex = 0;
        }
        #endregion Cargar combos

        ////#region Unidades de Medida
        ////private void UnidadesDeMedida()
        ////{
        ////    dtsUnidadesDeMedida = consulta.CFDI_UnidadesDeMedida("", "UnidadesDeMedida()");
        ////}
        ////#endregion Unidades de Medida

        #region Validaciones
        private bool validarRemision()
        {
            bool bRegresa = false;

            return bRegresa;
        }

        private void MensajeErrorImportes()
        {
            General.msjError("Se detectaron incongruencias en SubTotal, IVA y Total de la plantilla cargada, verifique.");
        }

        private bool validarGenerarDocumento()
        {
            bool bRegresa = true;
            ////sNombreReceptor = txtClienteNombre.Text;

            ////Forzar la actualizacion de los datos de pago 
            MostrarPago(true);

            if (bErrorEnImportes)
            {
                bRegresa = false;
                MensajeErrorImportes();
            }

            if (bRegresa & !bInformacionEmisor)
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

            if(bRegresa && cboFormasDePago.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Forma de Pago válida, verifique.");
                cboFormasDePago.Focus();
            }

            if(bRegresa && cboMonedas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Moneda válida, verifique.");
                cboMonedas.Focus();
            }

            if(bRegresa && nmImportePago.Value <= 0 )
            {
                bRegresa = false;
                General.msjUser("El monto pagado debe ser mayor a Cero, verifique.");
                nmImportePago.Focus();
            }

            if(!chkCargaManual.Checked)
            {
                if(bRegresa && cboHojas.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado una hoja válida del archivo de excel, verifique.");
                    cboHojas.Focus();
                }

                if(bRegresa && cboHojas.Enabled)
                {
                    bRegresa = false;
                    General.msjUser("No ha bloqueado la hoja a integrar al documento electrónico, verifique.");
                }
            }

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
            ////lblCliente.Text = "";
            ////txtClienteNombre.Text = "";
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text != "")
            {
                leer.DataSetClase = consulta.CFDI_Clientes(txtId.Text, "txtId_Validating");
                if (leer.Leer())
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
                sRFC_Receptor = leer.Campo("RFC");
            }
        }

        private void tmProceso_Tick(object sender, EventArgs e)
        {
            if (!bProcesando)
            {
                tmProceso.Stop();
                tmProceso.Enabled = false;
                BloquearControles(false);

                // No fue posible generar el folio de factura electronica. 
                if (bErrorAlGenerarFolio)
                {
                    IniciaToolBarExcel(true, true, true, true, true);
                    General.msjAviso("No fue posible generar el documento electrónico, intente de nuevo.");
                }
                else
                {
                    if (tpPAC == PACs_Timbrado.Tralix)
                    {
                        ImprimirDocumento();
                    }
                    else
                    {

                        ImprimirDocumento_Timbrado();
                        if (!VistaPrevia)
                        {
                            InicializaPantalla();
                        }
                    }
                }
            }
        }
        #endregion Eventos

        #region Generar Factura Electrónica
        private void BloquearControles(bool Bloquear)
        {
            Bloquear = !Bloquear;
            btnNuevo.Enabled = Bloquear;
            // btnObtenerDetalles.Enabled = Bloquear;
            btnFacturar.Enabled = Bloquear;
            btnValidarDatos.Enabled = Bloquear;

            txtId.Enabled = Bloquear;
            //txtClienteNombre.Enabled = Bloquear;
            //btlCliente.Enabled = Bloquear;
            cboSeries.Enabled = Bloquear;
            ////cboFormasDePago.Enabled = Bloquear;
            ////btnCondicionesDePago.Enabled = Bloquear;
            ////cboMetodosDePago.Enabled = Bloquear;

            ////chkVencimiento.Enabled = Bloquear;
            ////dtpFechaVencimiento.Enabled = Bloquear;
            ////btnObservaciones.Enabled = Bloquear;
            btnObservacionesGral.Enabled = Bloquear;
            //btnPago.Enabled = Bloquear;

            //chkClaveSSA_Base___EsIdentificador.Enabled = Bloquear;

            IniciaToolBarExcel(false, false, false, false, false);
        }

        private void thGenerarDocto()
        {

            // Control del Timer 
            bProcesando = true;

            tmProceso.Enabled = true;
            tmProceso.Start();

            BloquearControles(true);

            thFacturaElectronica = new Thread(GenerarDocumento);
            thFacturaElectronica.Name = "DocumentoElectrónico";
            thFacturaElectronica.Start();
        }

        private bool RegistrarDocumentoElectronico() // sTipoDoctoCFDI
        {
            bool bRegresa = false;
            string sSAT_Producto_Servicio = "";
            string sSAT_UnidadDeMedida = "";
            string sTipoDeRelacion_CFDI = txtRelacion__UUID.Text.Trim() == "" ? "" : "04"; 

            string Identificador = "";
            string sSerie = "";
            int iFolio = 0;
            string sUUID = "";
            int iParcialidad = 0;
            double dImporte_SaldoAnterior  = 0;
            double dImporte_Pagado = 0;
            double dImporte_SaldoInsoluto = 0;
            

            bool bCaducidadAsignada = false;
            clsLeer leerMetodosDePago = new clsLeer();

            string sSql = "";
            string sIdSerie = cboSeries.ItemActual.GetItem("IdentificadorSerie");
            sSerieDocumento = cboSeries.Data;

            sStoreEnc = VistaPrevia ? sStoreEnc_VP : sStoreEnc_Real;
            sStoreDet_Detalles = VistaPrevia ? sStoreDet_Detalles_VP : sStoreDet_Detalles_Real;
            sStoreDet_MetodoDePago = VistaPrevia ? sStoreDet_MetodoDePago_VP : sStoreDet_MetodoDePago_Real;
            sStoreDet_Detalles_UUID = VistaPrevia ? sStoreDet_Detalles_UUID_VP : sStoreDet_Detalles_UUID_Real;


            sXMLFormaPago = formaMetodoPago.IdFormaDePago;
            sXMLCondicionesPago = formaMetodoPago.ObservacionesPago;
            sXMLMetodoPago = formaMetodoPago.MetodoPago;
            sXMLObservacionesPago = formaMetodoPago.Observaciones;
            
            
            leerMetodosDePago.DataSetClase = formaMetodoPago.ListaDeMetodosDePago;
            sXMLFormaPago = "PUE";
            sXMLCondicionesPago = "CONTADO";
            sXMLMetodoPago = "NO IDENTIFICADO";



            ////sSql = string.Format("Exec {0} ", sStoreEnc); 
            ////sSql += string.Format(
            ////    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @UsoDeCFDI = '{3}', @IdTipoDocumento = '{4}', @IdentificadorSerie = '{5}', " + 
            ////    " @Serie = '{6}', @Folio = '{7}', @RFC = '{8}', @NombreReceptor = '{9}', @Status = '{10}', @IdPersonalCancela = '{11}', " + 
            ////    " @Observaciones_01 = '{12}', @Observaciones_02 = '{13}', @Observaciones_03 = '{14}', @XMLFormaPago = '{15}', @XMLMetodoPago = '{16}', @IdPersonalEmite = '{17}' ",
            ////    sIdEmpresa, sIdEstado, sIdFarmacia, cboUsosCFDI.Data, sTipoDoctoCFDI,
            ////    sIdSerie, sSerieDocumento, sFolioDocumento,
            ////    sRFC_Receptor, "NombreCliente", "A", "",
            ////    sObservaciones_01, sObservaciones_02, sObservaciones_03, sXMLFormaPago, sXMLMetodoPago, DtGeneral.IdPersonal );


            dTotal = Convert.ToDouble(nmImportePago.Value);
            sSql = string.Format("Exec {0} " +
                " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdTipoDocumento = '{4}', " +
                " @IdentificadorSerie = '{5}', @Serie = '{6}', @NombreDocumento = '{7}', @Folio = '{8}', @Importe = '{9}', " +
                " @RFC = '{10}', @NombreReceptor = '{11}', " +
                " @IdCFDI = '{12}', @Status = '{13}', @IdPersonalCancela = '{14}', " +
                " @Observaciones_01 = '{15}', @Observaciones_02 = '{16}', @Observaciones_03 = '{17}', @Referencia = '{18}', " +
                " @XMLFormaPago = '{19}', @XMLCondicionesPago = '{20}', @XMLMetodoPago = '{21}', " +
                " @TipoDocumento = '{22}', @TipoInsumo = '{23}', @IdRubroFinanciamiento = '{24}', @IdFuenteFinanciamiento = '{25}', @IdPersonalEmite = '{26}', " +
                " @UsoDeCFDI = '{27}', @TipoRelacion = '{28}', @SAT_ClaveDeConfirmacion = '{29}', @CFDI_Relacionado_CPago = '{30}', " +
                " @Serie_Relacionada_CPago = '{31}', @Folio_Relacionado_CPago = '{32}', @TienePagoRelacionado = '{33}' ",
                sStoreEnc,
                sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoCFDI,
                sIdSerie, sSerieDocumento, sNombreDocumento, sFolioDocumento, dTotal.ToString(sFormato).Replace(",", ""),
                sRFC_Receptor, lblCliente.Text.Trim(), sIdCFDI, "A", "",
                sObservaciones_01, sObservaciones_02, sObservaciones_03, sReferencia,
                sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago,
                iTipoDocumento, iTipoInsumo, sRubroFinanciamiento, sFuenteFinanciamiento, DtGeneral.IdPersonal,
                DtIFacturacion.Pago_UsoCDFI, sTipoDeRelacion_CFDI, "", txtRelacion__UUID.Text.Trim(), txtRelacion__Serie.Text.Trim(), txtRelacion__Folio.Text.Trim(), 1
                );


            if (!leerFacturar.Exec(sSql))
            {
                bErrorAlGenerarFolio = true;
                Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico_P01");
            }
            else
            {
                leerFacturar.Leer();
                PK = leerFacturar.CampoInt("FolioDocumento");

                bDocumentoElectronico_Generado = true;
                bRegresa = true;


                sSql = string.Format("Exec {0} ", sStoreDet_Detalles);
                sSql += string.Format(
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = '{4}', @FechaDePago = '{5}', @HoraDePago = '{6}', @FormaDePago = '{7}', " + 
                    " @Moneda = '{8}', @TipoDeCambio = '{9}', @MontoDePago = '{10}', @NumeroDeOperacion = '{11}', " + 
                    " @RfcEmisorCtaOrd = '{12}', @NomBancoOrdExt = '{13}', @CtaOrdenante = '{14}', @RfcEmisorCtaBen = '{15}', @CtaBeneficiario = '{16}', " + 
                    " @TipoCadPago = '{17}', @CertificadoPago = '{18}', @CadenaPago = '{19}', @SelloPago = '{20}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento, General.FechaYMD(dtpFechaDePago.Value), General.Hora(dtHoraDePago.Value), 
                    cboFormasDePago.Data, cboMonedas.Data, 1, nmImportePago.Value, txtNumeroDeOperacion.Text,
                    cboEmisor_RFC_BancoOrigen.Data, cboEmisor_RFC_BancoOrigen.Data, cboEmisor_CuentaOrdenante.Data, 
                    cboReceptor_RFC_BancoDestino.Data, cboReceptor_CuentaBeneficiario.Data, "", "", "", "" );
                if (!leerFacturar.Exec(sSql))
                {
                    bRegresa = false; 
                    bErrorAlGenerarFolio = true;
                    Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico_P02");
                }

                sSql = string.Format("Exec {0} ", sStoreDet_MetodoDePago);
                sSql += string.Format(
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = '{4}', " +
                    " @IdMetodoDePago = '{5}', @Importe = '{6}', @Referencia = '{7}' ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento, cboFormasDePago.Data, 0, "****"
                    );
                if(!leerFacturar.Exec(sSql))
                {
                    bRegresa = false;
                    bErrorAlGenerarFolio = true;
                    Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico_P03");
                }


                if (bRegresa)
                {
                    for (int Renglon = 1; Renglon <= grid.Rows; Renglon++)
                    {
                        Identificador = Renglon.ToString();
                        sSerie = grid.GetValue(Renglon, Cols.Serie);
                        iFolio = grid.GetValueInt(Renglon, Cols.Folio);
                        sUUID = grid.GetValue(Renglon, Cols.UUID);

                        iParcialidad = grid.GetValueInt(Renglon, Cols.Parcialidad); 
                        dImporte_SaldoAnterior = DtGeneral.Redondear(grid.GetValueDou(Renglon, Cols.SaldoAnterior), 4);
                        dImporte_Pagado = DtGeneral.Redondear(grid.GetValueDou(Renglon, Cols.Abono), 4);
                        dImporte_SaldoInsoluto = DtGeneral.Redondear(grid.GetValueDou(Renglon, Cols.SaldoFinal), 4);

                        sSql = string.Format("Exec {0} ", sStoreDet_Detalles_UUID);
                        sSql += string.Format(
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = '{4}', \n" +
                        " @Serie_Relacionada = '{5}', @Folio_Relacionado = '{6}', @UUID_Relacionado = '{7}', \n" +
                        " @Moneda = '{8}', @TipoCambio = '{9}', @MetodoDePago = '{10}', @NumParcialidad = '{11}', \n" +
                        " @Importe_SaldoAnterior = '{12}', @Importe_Pagado = '{13}', @Importe_SaldoInsoluto = '{14}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento,
                        sSerie, iFolio, sUUID, cboMonedas.Data, 1, cboFormasDePago.Data, iParcialidad, dImporte_SaldoAnterior, dImporte_Pagado, dImporte_SaldoInsoluto
                        );

                        
                        if (!leerFacturar.Exec(sSql))
                        {
                            bDocumentoElectronico_Generado = false;
                            bRegresa = false;

                            bErrorAlGenerarFolio = true;
                            Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico_P04");
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GenerarFolioDocumentoElectronico()
        {
            bool bRegresa = false;
            string sSql = "";
            cnnFacturar = new clsConexionSQL(General.DatosConexion);
            leerFacturar = new clsLeer(ref cnnFacturar);

            bErrorAlGenerarFolio = false;
            if (!cnnFacturar.Abrir())
            {
                bErrorAlGenerarFolio = true;
                General.msjErrorAlAbrirConexion();
            }
            else
            {

                cnnFacturar.IniciarTransaccion();

                if (VistaPrevia)
                {
                    bRegresa = true;
                    string sSerie = cboSeries.Data;
                    string sFolio = (Convert.ToInt32("0" + "0") + 1).ToString();
                    sFolioDocumento = sFolio;

                    sSql = string.Format("Delete From FACT_CFD_Documentos_Generados_Detalles_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    sSql += string.Format("Delete From FACT_CFD_Documentos_Generados_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);

                    sSql = "Select 1 as Dato";
                    leer.Exec(sSql);
                }
                else
                {
                    ///// Este SP es estandar para todos los tipos de CFDI 
                    sSql = string.Format("Exec spp_FACT_CFD_GetFolio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, cboSeries.Data);

                    if (!leerFacturar.Exec(sSql))
                    {
                        bErrorAlGenerarFolio = true;
                        Error.GrabarError(leerFacturar, "GenerarFolioDocumentoElectronico");
                    }
                    else
                    {
                        if (!leerFacturar.Leer())
                        {
                            bErrorAlGenerarFolio = true;
                            Error.GrabarError(leerFacturar, "GenerarFolioDocumentoElectronico");
                        }
                        else
                        {
                            if (leerFacturar.CampoInt("FolioGenerado") > leerFacturar.CampoInt("FolioFinal"))
                            {
                                bErrorAlGenerarFolio = true;
                                Error.GrabarError(string.Format("Se agotaron los folios disponibles de la Serie {0} ", cboSeries.Data),
                                    "GenerarFolioDocumentoElectronico");
                            }
                            else
                            {
                                bRegresa = true;
                                sFolioDocumento = leerFacturar.CampoInt("FolioGenerado").ToString();
                                PK = Convert.ToInt32("0" + sFolioDocumento);
                            }
                        }
                    }
                }

                if (bErrorAlGenerarFolio)
                {
                    GenerarFolioDocumentoElectronico_Terminar();
                    General.msjError("Ocurrió un error al obtener el Folio de CFDI.");
                }
            }

            return bRegresa;
        }

        private void GenerarFolioDocumentoElectronico_Terminar()
        {
            /////// Aplicar el foliador 
            if (bErrorAlGenerarFolio)
            {
                cnnFacturar.DeshacerTransaccion();
            }
            else
            {
                cnnFacturar.CompletarTransaccion();

                // Asegurar que se descargaron los documentos 
                if (DescargarDocumentos())
                {
                    ////GuardarDocumentos_XML_PDF();
                }
            }
        }

        private bool GuardarDocumentos_XML_PDF()
        {
            bool bRegresa = true;
            string sDocumento_XML = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, cfdi.Documento_XML));
            string sDocumento_PDF = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, cfdi.Documento_PDF));
            string sSql = string.Format("Exec spp_Mtto_FACT_CFD_Documentos_Generados_Formatos  @IdCFDI = '{0}', @XML = '{1}', @PDF = '{2}' ",
                sIdCFDI, sDocumento_XML, sDocumento_PDF);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GuardarDocumentos_XML_PDF()");
            }

            return bRegresa;
        }

        private void ImprimirDocumento()
        {
            xsaImprimirDocumento print = new xsaImprimirDocumento(sIdEmpresa, sIdEstado, IdFarmacia, General.DatosConexion);
            print.Imprimir(sSerieDocumento, sFolioDocumento, this);
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
            string sSql = string.Format("Exec {0} ", sImpresion) + string.Format(" @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, PK);

            string sMensaje = "¿ Desea ver la vista previa del documento electrónico ? ";
            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir()");
                General.msjError("Ocurrió un error al generar la impresión del comprobante, intente imprimir desde el panel principal.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjError("No fue posible generar la impresión del documento.");
                }
                else
                {
                    sFileName = leer.Campo("NombreFiles");
                    sXML = leer.Campo("uf_Xml_Timbrado");
                    sPDF = leer.Campo("uf_xml_Impresion");
                    sEmail = leer.Campo("EmailCliente");

                    bRegresa = true;
                    ////if (!VistaPrevia)
                    ////{
                    ////    if (General.msjConfirmar(sMensaje, "¿ Desea visualisar el documento generado ?") == System.Windows.Forms.DialogResult.No)
                    ////    {
                    ////        bVisualizar = false;
                    ////    }
                    ////}

                    DtIFacturacion.InvocaVisor = this;
                    DtIFacturacion.EsTimbradoMasivo = false;
                    DtIFacturacion.GenerarImpresionWebBrowser_ComplementoDePagos(VistaPrevia, sFileName, true, DtIFacturacion.GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null, false);
                }
            }

            return bRegresa;
        }

        private void GenerarDocumento()
        {
            MostrarEnProceso(true);
            bDocumentoElectronico_Generado = false;
            if (GenerarFolioDocumentoElectronico())
            {
                if (RegistrarDocumentoElectronico())
                {
                    if (!GenerarTimbre())
                    {
                        bErrorAlGenerarFolio = true;
                    }
                }

                GenerarFolioDocumentoElectronico_Terminar();
            }

            MostrarEnProceso(false);

            // Control del Timer 
            bProcesando = false;
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

        private bool GenerarTimbre_PAC()
        {
            bool bRegresa = false;
            long pk = PK;
            long pkResult = 0;
            string sImpresion = VistaPrevia ? " spp_FACT_CFDI_ComplementoPagos_ObtenerDatos_VP " : " spp_FACT_CFDI_ComplementoPagos_ObtenerDatos ";

            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI);

            //if (!bRegresa)
            {
                CFDI_FD = new clsGenCFDI_33_Pago();
                CFDI_FD.Conexion = cnnFacturar;
                CFDI_FD.myComprobante.DatosComprobante = DatosDeComprobante(sImpresion, ref bRegresa);

                if (bRegresa)
                {
                    CFDI_FD.EsVistaPrevia = VistaPrevia;
                    CFDI_FD.PAC = tpPAC;
                    pkResult = CFDI_FD.obtenerFacturaElectronica(CFDI_FD.myComprobante);
                    bRegresa = pkResult > 0;
                }
            }

            return bRegresa;
        }

        private DataSet DatosDeComprobante(string Impresion, ref bool Datos)
        {
            string sSql = string.Format("Exec {0} @Identificador = {1} ", Impresion, PK);

            Datos = leer.Exec(sSql);
            if (!Datos)
            {
                Error.GrabarError(leer, "DatosDeComprobante");
            }

            return leer.DataSetClase;
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

        #region Menu de Conceptos
        private void btnAgregarConcepto_Click(object sender, EventArgs e)
        {
            //Conceptos(1, list.RenglonActivo);
        }

        private void btnEliminarConcepto_Click(object sender, EventArgs e)
        {
            //if (list.Registros > 0)
            //{
            //    list.EliminarRenglonSeleccionado();
            //}
        }

        private void btnModificarConcepto_Click(object sender, EventArgs e)
        {
            //Conceptos(2, list.RenglonActivo);
        }

        private void Conceptos(int Tipo, int Renglon)
        {
        }
        #endregion Menu de Conceptos

        #region Importar Excel
        clsLeerExcel excel;
        clsListView lst;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        bool bLeyendoHoja = false;
        string sFile_In = "";
        string sTitulo = "";

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        private void IniciarEtiquetasExcel()
        {
            int iValor = 0;
            ////lblCantidadConLetra.Text = "";
            ////lblRegistros.Text = string.Format("Registros : {0}", 0);
            ////lblSubTotal.Text = string.Format("{0}", iValor.ToString(sFormato));
            ////lblIva.Text = string.Format("{0}", iValor.ToString(sFormato));
            ////lblTotal.Text = string.Format("{0}", iValor.ToString(sFormato));

            ////lblSubTotal_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));
            ////lblIva_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));
            ////lblTotal_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));
        }

        private void IniciarEtiquetas_CalculoImportes(bool Mostrar)
        {
            ////lblTitulo_SubTotal_Calculado.Visible = Mostrar;
            ////lblTitulo_Iva_Calculado.Visible = Mostrar;
            ////lblTitulo_Total_Calculado.Visible = Mostrar;

            ////lblSubTotal_Calculado.Visible = Mostrar;
            ////lblIva_Calculado.Visible = Mostrar;
            ////lblTotal_Calculado.Visible = Mostrar;

            if (bErrorEnImportes)
            {
                MensajeErrorImportes();
            }
        }

        private void IniciaToolBarExcel(bool Nuevo, bool Abrir, bool LeerArchivo, bool LeerHoja, bool BloquearHojas)
        {
            btnNuevoExcel.Enabled = Nuevo;
            btnAbrirExcel.Enabled = Abrir;
            btnLeerExcel.Enabled = LeerArchivo;
            btnBloquearHoja.Enabled = BloquearHojas;
        }

        private void btnNuevoExcel_Click(object sender, EventArgs e)
        {
            ////lblCantidadConLetra.Text = "";
            sFile_In = "";
            lblArchivoExcel.Text = "";
            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;
            ////listvClaves.Items.Clear();

            IniciarEtiquetasExcel();
            IniciarEtiquetas_CalculoImportes(false);
            IniciaToolBarExcel(true, true, false, false, false);
        }

        private void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos concentrados de facturación";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;
                lblArchivoExcel.Text = sFile_In;

                cboHojas.Clear();
                cboHojas.Add();
                cboHojas.SelectedIndex = 0;
                this.Refresh();


                IniciaToolBarExcel(false, false, false, false, false);
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start();
            }
        }

        private void btnLeerExcel_Click(object sender, EventArgs e)
        {
            IniciaToolBarExcel(false, false, false, false, false);

            tmProceso_LoadInformacion.Enabled = true;
            tmProceso_LoadInformacion.Interval = 1000;
            tmProceso_LoadInformacion.Start();

            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        private void btnBloquearHoja_Click(object sender, EventArgs e)
        {
            if (bErrorEnImportes)
            {
                MensajeErrorImportes();
            }
            else
            {
                cboHojas.Enabled = !cboHojas.Enabled;
            }
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            //////MostrarEnProceso(true, 1);
            //////FrameResultado.Text = sTitulo;

            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;
            ////listvClaves.Items.Clear();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnLeerExcel.Enabled = bHabilitar;
            IniciaToolBarExcel(true, true, bHabilitar, false, false);

            BloqueaHojas(false);
            //MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar;

        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnLeerExcel.Enabled = bHabilitar;
        }

        private void thLeerHoja()
        {
            bActivarProceso = false; 
            bValidandoInformacion = true; 
            BloqueaHojas(true);
            ////MostrarEnProceso(true, 2);
            ////lblProcesados.Visible = false;

            ////lblCantidadConLetra.Text = "";
            LeerHoja();

            BloqueaHojas(false);

            bLeyendoHoja = false;
            ////MostrarEnProceso(false, 2);
            bValidandoInformacion = false; 
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            //FrameResultado.Text = sTitulo;
            if(excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if(bRegresa)
            {
                bRegresa = ValidarColumnas();
            }

            return bRegresa;
        }

        private bool ValidarColumnas()
        {
            bool bRegresa = false;

            bRegresa = excel.ExisteTablaColumna(1, "Serie") && excel.ExisteTablaColumna(1, "Folio") && excel.ExisteTablaColumna(1, "Importe_Pagado");


            if(!bRegresa)
            {
                General.msjError("La plantilla no contiene todas las columnas requeridas.");
            }
            else
            {
                bRegresa = CargarInformacionDeHoja();
            }

            return bRegresa;
        }

        private bool CargarInformacionDeHoja()
        {
            bool bRegresa = false;
            //lblProcesados.Visible = true;


            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);

            bulk.ClearColumns();
            bulk.DestinationTableName = "FACT_CFDI_ComplementoDePagos__CargaMasiva";

            if(leer.ExisteTablaColumna(1, "IdEmpresa")) bulk.AddColumn("IdEmpresa", "IdEmpresa");
            if(leer.ExisteTablaColumna(1, "IdEstado")) bulk.AddColumn("IdEstado", "IdEstado");
            if(leer.ExisteTablaColumna(1, "IdFarmacia")) bulk.AddColumn("IdFarmacia", "IdFarmacia");

            bulk.AddColumn("Serie", "Serie");
            bulk.AddColumn("Folio", "Folio");
            bulk.AddColumn("Importe_Pagado", "Importe_Pagado");

            leer.Exec("Truncate table FACT_CFDI_ComplementoDePagos__CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if(bRegresa)
            {
                bRegresa = validarInformacion();
            }
            else
            {
                bErrorAlCargarPlantilla = true;
                sMensajeError_CargarPlantilla = bulk.Error.Message;
            }

            return bRegresa;
        }

        private bool validarInformacion()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_FACT__ComplementoDePagos_Validar_Datos  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @RFC = '{3}', @Detallado = 0   ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sRFC_Receptor);

            if(!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla.";
                Error.GrabarError(leer, "validarInformacion()");
            }
            else
            {
                if(ValidarInformacion_Entrada())
                {
                    bRegresa = CargarInformacion_Validada();
                }
            }

            return bRegresa;
        }

        private bool ValidarInformacion_Entrada()
        {
            bool bRegresa = true;
            clsLeer leerValidacion = new clsLeer();
            clsLeer leerResultado = new clsLeer();
            DataSet dtsResultados = new DataSet();
            int iErrores = 0;

            bActivarProceso = false;
            leer.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = leer.Tabla(1);
            while(leerResultado.Leer())
            {
                leer.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = leer.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            leer.DataSetClase = dtsResultados;
            dtsResultados = new DataSet("Resultados");

            foreach(DataTable dt in leer.DataSetClase.Tables)
            {
                //if(!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    iErrores += leerValidacion.Registros > 0 ? 1 :0;
                    if(leerValidacion.Registros > 0)
                    {
                        dtsResultados.Tables.Add(dt.Copy()); 
                    }
                }
            }

            bActivarProceso = iErrores == 0;
            bRegresa = bActivarProceso;

            if(!bRegresa)
            {
                leer.DataSetClase = dtsResultados;  
            }

            return bRegresa;
        }

        private bool CargarInformacion_Validada()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_FACT__ComplementoDePagos_ObtenerDatos  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @RFC = '{3}'   ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sRFC_Receptor);

            grid.Limpiar(false);
            if(!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla.";
                Error.GrabarError(leer, "CargarInformacion_Validada()");
            }
            else
            {
                bRegresa = true;
                grid.LlenarGrid(leer.DataSetClase, false, false);
            }

            nmImportePago.Value = Convert.ToDecimal(grid.TotalizarColumnaDou(Cols.Abono)); 

            //lblRegistros.Text = string.Format("Registros : {0}", grid.Rows.ToString("###, ###, ###, ##0"));
            //lblSubTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva).ToString("###, ###, ###, ##0.0000");
            //lblIva.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Iva).ToString("###, ###, ###, ##0.0000");
            //lblTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar).ToString("###, ###, ###, ##0.0000");

            return bRegresa;
        }

        private void bulk_RowsCopied( RowsCopiedEventArgs e )
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled( RowsCopiedEventArgs e )
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error( RowsCopiedEventArgs e )
        {
            //lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private void BloqueaHojas( bool Bloquear )
        {
            //cboHojas.Enabled = !Bloquear;
        }
        #endregion Importar Excel

        private void cboHojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////listvClaves.Items.Clear();
            IniciarEtiquetasExcel();
            IniciarEtiquetas_CalculoImportes(false);
        }

        #region Validaciones de Pago 
        private void chkTieneDocumentoRelacionado_CheckedChanged(object sender, EventArgs e)
        {
            txtRelacion__Serie.Enabled = chkTieneDocumentoRelacionado.Checked;
            txtRelacion__Folio.Enabled = chkTieneDocumentoRelacionado.Checked;
            txtRelacion__UUID.Enabled = chkTieneDocumentoRelacionado.Checked;

            if (txtRelacion__Serie.Enabled)
            {
                txtRelacion__Serie.Focus(); 
            }
        }

        private void Inicializar_Cbo_CuentaEmisor()
        {
            cboEmisor_CuentaOrdenante.Clear();
            cboEmisor_CuentaOrdenante.Add("", "<< Seleccione >>");
            cboEmisor_CuentaOrdenante.SelectedIndex = 0;
        }

        private void Inicializar_Cbo_CuentaBeneficiario()
        {
            cboReceptor_CuentaBeneficiario.Clear();
            cboReceptor_CuentaBeneficiario.Add("", "<< Seleccione >>");
            cboReceptor_CuentaBeneficiario.SelectedIndex = 0;
        }

        private void cboEmisor_RFC_BancoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inicializar_Cbo_CuentaEmisor();
            if (cboEmisor_RFC_BancoOrigen.SelectedIndex != 0)
            {
                cboEmisor_CuentaOrdenante.Filtro = string.Format(" RFC_Banco = '{0}' ", cboEmisor_RFC_BancoOrigen.Data);
                cboEmisor_CuentaOrdenante.Add(dtsCuentas_Emisor, true, "NumeroDeCuenta", "CuentaBanco"); 
            }
        }

        private void cboReceptor_RFC_BancoDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inicializar_Cbo_CuentaBeneficiario();
            if (cboReceptor_RFC_BancoDestino.SelectedIndex != 0)
            {
                cboReceptor_CuentaBeneficiario.Filtro = string.Format(" RFC_Banco = '{0}' ", cboReceptor_RFC_BancoDestino.Data);
                cboReceptor_CuentaBeneficiario.Add(dtsCuentas_Receptor, true, "NumeroDeCuenta", "CuentaBanco"); 
            }
        }
        #endregion Validaciones de Pago

        private void btnEmisor_AgregarCuenta_Click(object sender, EventArgs e)
        {
            FrmBancosCuentas_Emisor Emisor = new FrmBancosCuentas_Emisor();
            Emisor.ShowDialog();

            Cargar__InformacionEmisor(); 
        }

        private void btnReceptor_AgregarCuenta_Click(object sender, EventArgs e)
        {
            FrmBancosCuentas_Receptor Receptor = new FrmBancosCuentas_Receptor();
            Receptor.ShowDialog();

            Cargar__InformacionReceptor(); 
        }

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, "Facturación", DatoCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx("Facturación"));

                bRegresa = true;
            }
            catch(Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
            }

            return bRegresa;
        }

        private void ObtenerServidoresRemotos()
        {
            string sSql = string.Format("Select * From CFGCF_ConfigurarConexion (NoLock) Where Status = 'A'");

            if(leer.Exec(sSql))
            {
                if(leer.Leer())
                {
                    sUrl = string.Format("http://{0}/{1}/{2}.asmx", leer.Campo("Servidor"), leer.Campo("WebService"), leer.Campo("PaginaWeb"));
                }
            }

        }

        private void grdUUIDs_Advance( object sender, FarPoint.Win.Spread.AdvanceEventArgs e )
        {
            if(chkCargaManual.Checked)
            {
                if((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
                {
                    if(grid.GetValue(grid.ActiveRow, Cols.Folio) != "" && grid.GetValueDou(grid.ActiveRow, Cols.Abono) != 0.0)
                    {
                        grid.Rows = grid.Rows + 1;
                        grid.ActiveRow = grid.Rows;
                        grid.SetActiveCell(grid.Rows, 1);
                    }
                }
            }
        }

        private void grdUUIDs_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.Delete)
            {
                try
                {
                    int iRow = grid.ActiveRow;
                    grid.DeleteRow(iRow);
                }
                catch { }

                if(grid.Rows == 0)
                {
                    grid.Limpiar(true);
                }
            }

            //////lblRegistros.Text = string.Format("Registros : {0}", grid.Rows.ToString("###, ###, ###, ##0"));
            //////lblSubTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva).ToString("###, ###, ###, ##0.0000");
            //////lblIva.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Iva).ToString("###, ###, ###, ##0.0000");
            //////lblTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar).ToString("###, ###, ###, ##0.0000");

        }

        private void grdUUIDs_EditModeOff( object sender, EventArgs e )
        {
            ColActiva = (Cols)grid.ActiveCol;
            string sValorBuscar = "";

            switch(ColActiva)
            {
                case Cols.Serie:
                case Cols.Folio:

                    string sSerie = grid.GetValue(grid.ActiveRow, (int)Cols.Serie);
                    string sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);

                    if(sSerie != "" && sFolio != "")
                    {
                        //sValorBuscar = cboSubFarmacias.Data + lblCodigo.Text + sLote;

                        grid.SetValue(grid.ActiveRow, (int)Cols.Serie, "");
                        grid.SetValue(grid.ActiveRow, (int)Cols.Folio, "");

                        sValorBuscar = sFolio + sSerie;
                        int[] Columnas = { (int)Cols.Folio, (int)Cols.Serie };

                        if(grid.BuscarRepetidosColumnas(sValorBuscar, Columnas) == 0)
                        {
                            BuscarDocumento(sSerie, sFolio);

                            grid.SetValue(grid.ActiveRow, (int)Cols.Abono, 0.0000);
                        }
                        else
                        {
                            General.msjAviso("la Factura se encuentra duplicada, verifique.");
                        }
                    }
                    else
                    {
                        //grid.SetValue(grid.ActiveRow, (int)Cols.UUID, "");
                        //grid.SetValue(grid.ActiveRow, (int)Cols.Importe_Base, "");
                        //grid.SetValue(grid.ActiveRow, (int)Cols.Anteriores, "");
                        //grid.SetValue(grid.ActiveRow, (int)Cols.Importe_A_Aplicar, 0.0000);
                    }

                    break;

                case Cols.Abono:


                    double dCantidad_A_APlicar = grid.GetValueDou(grid.ActiveRow, (int)Cols.Abono);
                    double dCantidad_Base = grid.GetValueDou(grid.ActiveRow, (int)Cols.ValorNominal);
                    double dCantidad_Anterior = grid.GetValueDou(grid.ActiveRow, (int)Cols.SaldoAnterior);


                    if(dCantidad_A_APlicar > dCantidad_Anterior)
                    {
                        dCantidad_A_APlicar = dCantidad_Anterior;
                        General.msjAviso("El importe del Pago a Aplicar no puede ser mayor al Saldo por Aplicar");
                        grid.SetValue(grid.ActiveRow, (int)Cols.Abono, dCantidad_A_APlicar);
                    }

                    //double dSubTotal = dCantidad_A_APlicar * 100 / (100 + dTasaIva);
                    //double dImporteIva = dCantidad_A_APlicar - dSubTotal;

                    //grid.SetValue(grid.ActiveRow, (int)Cols.Importe_Sin_Iva, dSubTotal);
                    //grid.SetValue(grid.ActiveRow, (int)Cols.Importe_Iva, dImporteIva);


                    break;

            }
            //////////Totalizar();
            ////lblRegistros.Text = string.Format("Registros : {0}", grid.Rows.ToString("###, ###, ###, ##0"));
            ////lblSubTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva).ToString("###, ###, ###, ##0.0000");
            ////lblIva.Text = grid.TotalizarColumnaDou((int)Cols.Importe_Iva).ToString("###, ###, ###, ##0.0000");
            ////lblTotal.Text = grid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar).ToString("###, ###, ###, ##0.0000");

            nmImportePago.Value = Convert.ToDecimal(grid.TotalizarColumnaDou((int)Cols.Abono));
        }

        private void BuscarDocumento( string sSerie, string sFolio )
        {
            bool bEncontrado = false;
            bool bConError = false;
            string sSql = "";
            double dSaldoAnterior_Local = 0;
            double dTotal_Local = 0;
            double dTotalAbonos_Local = 0;

            // fg_FACT_ComplementoPagos_Parcialidad
            // vw_FACT_CFD_DocumentosElectronicos 
            sSql = string.Format(
                "Select *, \n"+
                " dbo.fg_FACT_ComplementoPagos_Parcialidad ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) As Parcialidad, \n" +
                " dbo.fg_FACT_ComplementoPagos_Acumulado ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) As ImporteTotal_Abonos \n" +  
                "From vw_FACT_CFD_DocumentosElectronicos (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' \n",
                IdEmpresa, IdEstado, IdFarmacia, sSerie, sFolio); // + Fg.PonCeros(IdCliente, 4) + "'";

            //leer.DataSetClase = consulta.CFDI_Documentos(sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio, "grdUUIDs_EditModeOff");
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "grdUUIDs_EditModeOff()");
                General.msjError("Ocurrió un error al obtener los datos.");
                bConError = true;
            }
            else
            {
                if(leer.Leer())
                {
                    bEncontrado = true;
                }
            }

            if(!bEncontrado & !bConError)
            {
                if(validarDatosDeConexion())
                {
                    if(!leerWeb.Exec(sSql))
                    {
                        Error.GrabarError(leerWeb, "grdUUIDs_EditModeOff()");
                        General.msjError("Ocurrió un error al obtener los datos.");
                        bConError = true;
                    }
                    else
                    {
                        if(leerWeb.Leer())
                        {
                            bEncontrado = true;
                            leer.DataSetClase = leerWeb.DataSetClase;
                            leer.Leer();
                        }
                    }
                }
            }

            if(!bConError)
            {
                if(bEncontrado)
                {
                    string sRFC = leer.Campo("RFC");
                    if(sRFC_Receptor != sRFC)
                    {
                        //General.msjAviso("La factura pertenece a otro cliente.");
                        grid.LimpiarRenglon(grid.ActiveRow);
                        grid.SetActiveCell(grid.ActiveRow, (int)Cols.Folio);
                        bEncontrado = false;
                        General.msjAviso("El CFDI Factura pertenece a un cliente distinto al seleccionado.");
                    }
                    else
                    {

                        dTotal_Local = DtGeneral.Redondear(leer.CampoDouble("Total"), 4);
                        dTotalAbonos_Local = DtGeneral.Redondear(leer.CampoDouble("ImporteTotal_Abonos"), 4);

                        grid.SetValue(grid.ActiveRow, (int)Cols.Serie, sSerie);
                        grid.SetValue(grid.ActiveRow, (int)Cols.Folio, sFolio);

                        grid.SetValue(grid.ActiveRow, (int)Cols.UUID, leer.Campo("UUID"));
                        
                        grid.SetValue(grid.ActiveRow, (int)Cols.Parcialidad, leer.CampoDouble("Parcialidad"));
                        grid.SetValue(grid.ActiveRow, (int)Cols.ValorNominal, dTotal_Local);
                        grid.SetValue(grid.ActiveRow, (int)Cols.TotalPagado, dTotalAbonos_Local);

                        dSaldoAnterior_Local = DtGeneral.Redondear(dTotal_Local - dTotalAbonos_Local, 4);
                        grid.SetValue(grid.ActiveRow, (int)Cols.SaldoAnterior, dSaldoAnterior_Local);
                        grid.SetValue(grid.ActiveRow, (int)Cols.Abono, 0);
                        grid.SetValue(grid.ActiveRow, (int)Cols.SaldoFinal, 0);
                    }
                }
                else
                {
                    General.msjAviso("Documento no encontrado, verifique.");
                    grid.LimpiarRenglon(grid.ActiveRow);
                    grid.SetActiveCell(grid.ActiveRow, (int)Cols.Folio);
                }
            }

            ////if(!bConError)
            ////{
            ////    sSql = string.Format("Select dbo.fg_FACT_NotasDeCredito_Total_Previo('{0}', '{1}', '{2}', '{3}', '{4}') As Anterior",
            ////                         IdEmpresa, IdEstado, IdFarmacia, sSerie, sFolio);

            ////    if(bEncontrado)
            ////    {
            ////        if(!leer.Exec(sSql))
            ////        {
            ////            Error.GrabarError(leer, "grdUUIDs_EditModeOff()");
            ////            General.msjError("Ocurrió un error al obtener los datos.");
            ////        }
            ////        else
            ////        {
            ////            if(leer.Leer())
            ////            {
            ////                grid.SetValue(grid.ActiveRow, (int)Cols.SaldoAnterior, leer.CampoDouble("Anterior"));
            ////            }
            ////        }
            ////    }
            ////}
        }
        #endregion Grid 

        #region Carga multiple 
        private void chkMultiples_CheckedChanged( object sender, EventArgs e )
        {

        }

        private void chkCargaManual_CheckedChanged( object sender, EventArgs e )
        {
            string sSerie = grid.GetValue(1, (int)Cols.Serie);
            string sFolio = grid.GetValue(1, (int)Cols.Folio);

            btnBloquearHoja.Enabled = chkCargaManual.Checked;
            //grid.BloqueaGrid(!chkCargaManual.Checked);

            grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Serie);
            grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Folio);
            grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Abono);
            
            if(!bLimpiando)
            {
                if(grid.Rows >= 1 && sSerie != "" && sFolio != "")
                {
                    if(General.msjConfirmar("El modificar este parámetro borrará la información de CFDIs previamente capturara. \n\n ¿ Desea continuar ?") == DialogResult.Yes)
                    {
                        grid.Limpiar(true);
                    }
                    else
                    {
                        chkCargaManual.Checked = !chkCargaManual.Checked;
                        btnBloquearHoja.Enabled = chkCargaManual.Checked;

                        //grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Serie);
                        //grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Folio);
                        //grid.BloqueaColumna(!chkCargaManual.Checked, (int)Cols.Abono);
                    }
                }
            }


        }

        private void tmProceso_LoadInformacion_Tick( object sender, EventArgs e )
        {
            tmProceso_LoadInformacion.Stop();
            tmProceso_LoadInformacion.Enabled = false;

            if(!bValidandoInformacion)
            {
                {
                    if(bActivarProceso)
                    {
                        //BloqueaControles(true);
                        IniciaToolBarExcel(true, true, true, true, true);
                    }
                    else
                    {
                        IniciaToolBarExcel(true, true, true, true, false);
                        FrmIncidencias f = new FrmIncidencias("Complemento de pago", leer.DataSetClase);
                        f.ShowDialog(this);

                        //////BloqueaControles(false);
                        //////IniciarToolBar(true, true, false);
                        ////if(bErrorAlCargarPlantilla)
                        ////{
                        ////    General.msjAviso(sMensajeError_CargarPlantilla);
                        ////}
                        ////else
                        ////{
                        ////    if(!bErrorAlValidar)
                        ////    {
                        ////        FrmIncidencias f = new FrmIncidencias("Complemento de pago", leer.DataSetClase);
                        ////        f.ShowDialog(this);
                        ////    }
                        ////    else
                        ////    {
                        ////        General.msjError(sMensajeError_CargarPlantilla);
                        ////    }
                        ////}
                    }
                }
            }
            else
            {
                tmProceso_LoadInformacion.Enabled = true;
                tmProceso_LoadInformacion.Start();
            }
        }
        #endregion Carga multiple 

        #region Sustitucion de CFDI
        private void txtRelacion__Serie_TextChanged( object sender, EventArgs e )
        {
            txtRelacion__Folio.Text = "";
            txtRelacion__UUID.Text = ""; 
        }

        private void txtRelacion__Folio_TextChanged( object sender, EventArgs e )
        {
            txtRelacion__UUID.Text = "";
        }

        private void txtRelacion__Serie_Validating( object sender, CancelEventArgs e )
        {

        }

        private void txtRelacion__Folio_Validating( object sender, CancelEventArgs e )
        {
            if(txtRelacion__Serie.Text.Trim() != "")
            {
                if(txtRelacion__Folio.Text.Trim() != "")
                {
                    if(!validar__SerieFolio_Relacionado())
                    {
                        txtRelacion__Serie.Text = "";

                        e.Cancel = true; 
                    }
                }
            }
        }

        private bool validar__SerieFolio_Relacionado()
        {
            bool bRegresa = true;
            string sSql = "";

            // IdTipoDocumento, Serie, Folio, RFC, Status, CFDI_Relacionado_CPago, Serie_Relacionada_CPago, Folio_Relacionado_CPago  

            sSql = string.Format(
                "Select E.* -- , IsNull(X.uf_CanceladoSAT, 0) as CanceladoSAT  \n " +
                "From vw_FACT_CFD_DocumentosElectronicos E (NoLock) \n" +
                //"Inner Join FACT_CFDI_XML X (NoLock) On( E.IdEmpresa = X.IdEmpresa and E.IdEstado = X.IdEstado and E.IdFarmacia = X.IdFarmacia and E.Serie = X.Serie And E.Folio = X.Folio)  \n " +
                "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' and E.Serie = '{3}' and E.Folio = '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtRelacion__Serie.Text.Trim(), txtRelacion__Folio.Text.Trim()
            );

            if(!leer.Exec(sSql))
            {
                bRegresa = false; 
                Error.GrabarError(leer, "validar__SerieFolio_Relacionado"); 
            }
            else
            {
                if(!leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No se encontro información del Documento solicitado.");
                }
                else
                {
                    if(bRegresa && sRFC_Receptor != leer.Campo("RFC"))
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI Pago pertenece a un cliente distinto al seleccionado.");
                    }

                    if(bRegresa && sTipoDoctoCFDI != leer.Campo("IdTipoDocumento"))
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI no es un Complemento de Pago, verifique.");
                    }

                    if(bRegresa && leer.CampoInt("CanceladoSAT") == 0)
                    {
                        bRegresa = false;
                        General.msjAviso("El CFDI Pago tiene Status 'ACTIVO'.\nSe requiere que este cancelado para sustituirlo con uno nuevo, verifique.");
                    }

                    if(bRegresa)
                    {
                        txtRelacion__Serie.ReadOnly = true;
                        txtRelacion__Folio.ReadOnly = true; 
                        txtRelacion__UUID.ReadOnly = true; 
                        txtRelacion__UUID.Text = leer.Campo("UUID");   
                    }
                }
            }

            return bRegresa;  
        }
        #endregion Sustitucion de CFDI

    }
}
