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
    public partial class FrmCFDI_NotaDeCredito_A_Detalle : FrmBaseExt
    {
        #region Declaracion de Variables
        enum Cols
        {
            Ninguna = 0,
            //IdClave = 1, ClaveSSA = 2, Descripcion = 3, PrecioUnitario = 4, Cantidad = 5, 
            //TasaIva = 6, SubTotal = 7, Iva = 8, Total = 9, UnidadDeMedida = 10, TipoImpuesto = 11, 
            //ClaveLote = 12, Caducidad = 13 

            //IdClave = 1, SAT_Producto_Servicio, ClaveSSA, Descripcion, PrecioUnitario, Cantidad,
            //TasaIva, SubTotal, Iva, Total, SAT_UnidadDeMedida, UnidadDeMedida, TipoImpuesto,
            //ClaveLote, Caducidad

            Serie, Folio, UUID, Importe_Base, Anteriores, ClaveSSA, TasaIva, Importe_A_Aplicar, Importe_Iva, Importe_Sin_Iva

        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsDatosCliente DatoCliente;
        wsCnnCliente conexionWeb;
        clsLeer leer;
        clsLeer leerFacturar;
        clsLeerWebExt leerWeb;
        clsGrid myGrid;
        clsAyudas ayuda;
        clsConsultas consulta;

        clsLeer Empresa = new clsLeer();
        clsLeer Sucursal = new clsLeer();
        clsLeer Domicilio = new clsLeer();
        clsLeer DatosReceptor = new clsLeer();
        clsFormaMetodoPago formaMetodoPago;
        DataSet dtsUnidadesDeMedida = new DataSet();
        DataSet dtsListaImpuestos = new DataSet();
        Cols ColActiva = Cols.Ninguna;

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
        string sUrl = "";

        eVersionCFDI tpVersionCFDI = eVersionCFDI.Version__3_3;

        //Thread thRemision;
        Thread thFacturaElectronica;
        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        string sTipoDoctoNotaDeCredito = "002";
        //string sTipoDoctoFactura = "001";


        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";

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
        clsGenCFDI_33_NotaDeCredito CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_NotaDeCredito myCFDI = null; //new clsGenCFDI_Ex();


        DataSet dtsSeries = new DataSet();
        #endregion Declaracion de Variables de Facturacion

        public FrmCFDI_NotaDeCredito_A_Detalle()
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
            this.FrameProceso.Top = 270;

            DatoCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            

            ////formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel);
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            //list = new clsListView(listvClaves);
            myGrid = new clsGrid(ref grdNota, this);
            myGrid.AjustarAnchoColumnasAutomatico = true; 
            ////myGrid.AnchoColumna((int)Cols.Serie, 100);
            ////myGrid.AnchoColumna((int)Cols.Folio, 100);
            ////myGrid.AnchoColumna((int)Cols.UUID, 100);
            ////myGrid.AnchoColumna((int)Cols.Importe_Base, 100);
            ////myGrid.AnchoColumna((int)Cols.Anteriores, 100);
            ////myGrid.AnchoColumna((int)Cols.TasaIva, 100);
            ////myGrid.AnchoColumna((int)Cols.Importe_A_Aplicar, 100);
            ////myGrid.AnchoColumna((int)Cols.Importe_Iva, 100);
            ////myGrid.AnchoColumna((int)Cols.Importe_Sin_Iva, 100);



            //Serie, Folio, UUID, Importe_Base, Anteriores, TasaIva, Importe_A_Aplicar, Importe_Iva, Importe_Sin_Iva


            tpPAC = DtIFacturacion.PAC_Informacion.PAC;

            //// Anexar al documento la Remisión relacionada 
            // sReferencia = "R" + sFolioRemision; 

            ////chkClaveSSA_Base___EsIdentificador.BackColor = General.BackColorBarraMenu;
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
        private void FrmCFDI_NotaDeCredito_A_Detalle_Load(object sender, EventArgs e)
        {
            //myGrid.LimpiarItems();
            //listvClaves.Items.Clear();


            InicializaPantalla();
            //TiposDeDocumentos(); 
            //Obtener_UsoCFDI();
            ObtenerSeries_y_Folios();
            ObtenerInformacion_Empresa_y_Sucursal();
            CargarFormas_Metodos_DePago();
            UnidadesDeMedida();
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
            lblTimbresDisponibles.Text = "Consultar timbres disponibles";
            lblCantidadConLetra.Text = "";
            lblCantidadConLetra.BorderStyle = BorderStyle.None;

            ////chkClaveSSA_Base___EsIdentificador.Checked = false;
            ////chkClaveSSA_Base___EsIdentificador.Visible = bImplementa_ClaveSSA_Base___Identificador;
            ////chkClaveSSA_Base___EsIdentificador.Enabled = chkClaveSSA_Base___EsIdentificador.Visible;

            tmProceso.Enabled = false;
            tmProceso.Interval = 500;
            Fg.IniciaControles();
            //myGrid.LimpiarItems(); 
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

            sFile_In = "";
            //cboHojas.Clear();
            //cboHojas.Add();
            //cboHojas.SelectedIndex = 0;
            bErrorEnImportes = false;
            //IniciaToolBarExcel(true, true, false, false, false);

            myGrid.Limpiar(true);

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
            f.bEsNotaDeCredito = true;

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
            try
            {
                //CierreAutomatico = false; 
                dImporteACobrar = Convert.ToDouble("0" + lblTotal.Text.Replace(",", "").Replace(" ", ""));
                formaMetodoPago.ImporteACobrar = dImporteACobrar;
                formaMetodoPago.Show(CierreAutomatico);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCondicionesDePago_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
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
            //FrmFactClientes f = new FrmFactClientes();
            //f.MostrarClientes(txtId.Text.Trim());

            //if (f.ClienteNuevo)
            //{
            //    txtId.Enabled = false;
            //    txtId.Text = f.Cliente;
            //    lblCliente.Text = f.ClienteNombre;
            //    txtClienteNombre.Text = f.ClienteNombre;
            //    txtId_Validating(null, null);
            //}

            PK = 6; 
            VistaPrevia = false;
            GenerarTimbre_PAC();
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
        //private void Obtener_UsoCFDI()
        //{
        //    cboUsosCFDI.Clear();
        //    cboUsosCFDI.Add();
        //    cboUsosCFDI.Add(consulta.CFDI_UsosDeCFDI("Obtener_UsoCFDI()"), true, "Clave", "UsoDeCFDI");
        //    cboUsosCFDI.SelectedIndex = 0;
        //}

        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add();
            dtsSeries = consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, "", "ObtenerSeries_y_Folios()");
            cboSeries.Filtro = string.Format(" IdTipoDocumento = '{0}' ", sTipoDoctoNotaDeCredito);
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

        private void MensajeErrorImportes()
        {
            General.msjError("Se detectaron incongruencias en SubTotal, IVA y Total de la plantilla cargada, verifique.");
        }

        private bool validarGenerarDocumento()
        {
            bool bRegresa = true;
            sNombreReceptor = txtClienteNombre.Text;

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

            //if (bRegresa && cboUsosCFDI.SelectedIndex == 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado una Uso de CFDI válido, verifique.");
            //    cboUsosCFDI.Focus();
            //}

            if (bRegresa && cboSeries.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Serie válida, verifique.");
                cboSeries.Focus();
            }

            if (bRegresa && myGrid.Rows == 0)
            {
                bRegresa = false;
                General.msjUser("No existen detalles para facturar, verifique.");
            }

            ////if (bRegresa && cboHojas.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado una hoja válida del archivo de excel, verifique.");
            ////    cboHojas.Focus();
            ////}

            ////if (bRegresa && cboHojas.Enabled)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha bloqueado la hoja a integrar al documento electrónico, verifique.");
            ////}

            //if (bRegresa && formaMetodoPago.IdFormaDePago == "0")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado una Forma de pago válida, verifique.");
            //    //cboFormasDePago.Focus(); 
            //    btnPago.Focus();
            //}

            //if (bRegresa && formaMetodoPago.IdMetodoPago == "0")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado un Método de pago válido, verifique.");
            //    //cboMetodosDePago.Focus();
            //    btnPago.Focus();
            //}

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha ingresado una descripción, verifique.");
                //cboMetodosDePago.Focus();
                txtDescripcion.Focus();
            }

            if (bRegresa && myGrid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar)  == 0.000)
            {
                bRegresa = false;
                General.msjUser("No ha ingresado la cantidad a aplicar, verifique.");
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
            lblCliente.Text = "";
            txtClienteNombre.Text = "";
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
                txtClienteNombre.Text = leer.Campo("Nombre");
                DatosReceptor.DataSetClase = leer.DataSetClase;
                DatosDeReceptor();
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
                    //IniciaToolBarExcel(true, true, true, true, true);
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
            txtClienteNombre.Enabled = Bloquear;
            btlCliente.Enabled = Bloquear;
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

            //IniciaToolBarExcel(false, false, false, false, false);
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

        private bool RegistrarDocumentoElectronico() // sTipoDoctoFactura
        {
            bool bRegresa = false;
            string sSAT_Producto_Servicio = "";
            string sSAT_UnidadDeMedida = "";

            string Identificador = "";
            string Clave = "";
            string Descripcion = "";
            double PrecioUnitario = 0;
            double Cantidad = 0;
            double TasaIva = 0;
            double SubTotal = 0;
            double Iva = 0;
            double Total = 0;
            string UnidadDeMedida = "";
            string TipoImpuesto = "";
            string ClaveLote = "";
            string Caducidad = "";
            bool bCaducidadAsignada = false;

            string sSerie_Relacionada = "";
	        int iFolio_Relacionado = 0;
	        string sUUID_Relacionado = "";
            double dTotal_Base = 0;
            bool bDetalleGuardado = false;

            double dPrecio_General = myGrid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva);
            double dSubTotal_General = myGrid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva);
            double dIVA_General = myGrid.TotalizarColumnaDou((int)Cols.Importe_Iva);
            double dTotal_General = myGrid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar); 

            clsLeer leerMetodosDePago = new clsLeer();

            string sSql = "";
            string sStoreEnc = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados ";
            string sStoreDet = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_Detalles_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados_Detalles ";
            string sStoreDet_MetodoDePago = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago ";
            string sStoreDoctosRelacionados = VistaPrevia ? " spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP " : " spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados ";
            string sIdSerie = cboSeries.ItemActual.GetItem("IdentificadorSerie");
            sSerieDocumento = cboSeries.Data;

            sXMLFormaPago = DtIFacturacion.NotaDeCredito_MetodoDePago;//formaMetodoPago.IdFormaDePago;
            sXMLCondicionesPago = "";// formaMetodoPago.ObservacionesPago;
            sXMLMetodoPago = DtIFacturacion.NotaDeCredito_FormaDePago;//formaMetodoPago.MetodoPago;
            sXMLObservacionesPago = "";//formaMetodoPago.Observaciones;
            leerMetodosDePago.DataSetClase = formaMetodoPago.ListaDeMetodosDePago;

            sSql = string.Format("Exec {0} " +
                " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdTipoDocumento = '{4}', " +
                " @IdentificadorSerie = '{5}', @Serie = '{6}', @NombreDocumento = '{7}', @Folio = '{8}', @Importe = '{9}', " +
                " @RFC = '{10}', @NombreReceptor = '{11}', " +
                " @IdCFDI = '{12}', @Status = '{13}', @IdPersonalCancela = '{14}', " +
                " @Observaciones_01 = '{15}', @Observaciones_02 = '{16}', @Observaciones_03 = '{17}', @Referencia = '{18}', " +
                " @XMLFormaPago = '{19}', @XMLCondicionesPago = '{20}', @XMLMetodoPago = '{21}', " +
                " @TipoDocumento = '{22}', @TipoInsumo = '{23}', @IdRubroFinanciamiento = '{24}', @IdFuenteFinanciamiento = '{25}', @IdPersonalEmite = '{26}', " +
                " @UsoDeCFDI = '{27}', @TipoRelacion = '{28}' ",
                sStoreEnc,
                sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoNotaDeCredito,
                sIdSerie, sSerieDocumento, sNombreDocumento, sFolioDocumento, dTotal.ToString(sFormato).Replace(",", ""),
                sRFC_Receptor, txtClienteNombre.Text.Trim(), sIdCFDI, "A", "",
                sObservaciones_01, sObservaciones_02, sObservaciones_03, sReferencia,
                sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago,
                iTipoDocumento, iTipoInsumo, sRubroFinanciamiento, sFuenteFinanciamiento, DtGeneral.IdPersonal,
                DtIFacturacion.NotaDeCredito_UsoCDFI, DtIFacturacion.NotaDeCredito_TipoDeRelacionCFDI
                );

            if (!leerFacturar.Exec(sSql))
            {
                bErrorAlGenerarFolio = true;
                Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico");
            }
            else
            {
                leerFacturar.Leer();
                PK = leerFacturar.CampoInt("FolioDocumento");

                bDocumentoElectronico_Generado = true;
                bRegresa = true;

                for (int Renglon = 1; Renglon <= myGrid.Rows; Renglon++)
                {
                    Identificador = Renglon.ToString();

                    sSAT_Producto_Servicio = DtIFacturacion.NotaDeCredito_SAT_ClaveProducto;  //myGrid.GetValue(Renglon, (int)Cols.SAT_Producto_Servicio);
                    sSAT_UnidadDeMedida = DtIFacturacion.NotaDeCredito_SAT_UnidadDeMedida; // myGrid.GetValue(Renglon, (int)Cols.SAT_UnidadDeMedida);

                    Clave = myGrid.GetValue(Renglon, (int)Cols.ClaveSSA);
                    Descripcion = txtDescripcion.Text;
                    PrecioUnitario = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Sin_Iva);
                    Cantidad = 1;
                    TasaIva = myGrid.GetValueDou(Renglon, (int)Cols.TasaIva);
                    SubTotal = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Sin_Iva);
                    Iva = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Iva);
                    Total = myGrid.GetValueDou(Renglon, (int)Cols.Importe_A_Aplicar);
                    UnidadDeMedida = "PZA";// myGrid.GetValue(Renglon, (int)Cols.UnidadDeMedida);
                    TipoImpuesto = "IVA";//myGrid.GetValue(Renglon, (int)Cols.TipoImpuesto);
                    ClaveLote = "";// myGrid.GetValue(Renglon, (int)Cols.ClaveLote);
                    Caducidad = "";//myGrid.GetValue(Renglon, (int)Cols.Caducidad);
                    bCaducidadAsignada = false;

                    sSerie_Relacionada = myGrid.GetValue(Renglon, (int)Cols.Serie);
                    iFolio_Relacionado = myGrid.GetValueInt(Renglon, (int)Cols.Folio);
                    sUUID_Relacionado = myGrid.GetValue(Renglon, (int)Cols.UUID);
                    dTotal_Base = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Base);
                    SubTotal = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Sin_Iva);
                    Iva = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Iva);
                    Total = myGrid.GetValueDou(Renglon, (int)Cols.Importe_A_Aplicar);

                    Descripcion += "";
                    Identificador = string.Format("{0}{1}-{2}", sSerie_Relacionada, sFolioDocumento, Clave); 

                    sSql = string.Format("Exec {0} " +
                        " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Serie = '{4}', @Folio = '{5}', " +
                        " @Identificador = '{6}', @Clave = '{7}', @DescripcionConcepto = '{8}', @UnidadDeMedida = '{9}', " +
                        " @Cantidad = '{10}', @PrecioUnitario = '{11}', @TasaIva = '{12}', @SubTotal = '{13}', @Iva = '{14}', @Total = '{15}', " +
                        " @TipoImpuesto = '{16}', @Partida = '{17}', @SAT_ClaveProducto_Servicio = '{18}', @SAT_UnidadDeMedida = '{19}'  ",
                        sStoreDet,
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento,
                        Identificador, Clave, Descripcion, UnidadDeMedida,
                        Cantidad.ToString().Replace(",", ""), dPrecio_General.ToString().Replace(",", ""), TasaIva.ToString().Replace(",", ""),
                        dSubTotal_General.ToString().Replace(",", ""), dIVA_General.ToString().Replace(",", ""), dTotal_General.ToString().Replace(",", ""), TipoImpuesto, Renglon,
                        sSAT_Producto_Servicio, sSAT_UnidadDeMedida);

                    //// Guardado a detalle 
                    sSql = string.Format("Exec {0} " +
                        " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Serie = '{4}', @Folio = '{5}', " +
                        " @Identificador = '{6}', @Clave = '{7}', @DescripcionConcepto = '{8}', @UnidadDeMedida = '{9}', " +
                        " @Cantidad = '{10}', @PrecioUnitario = '{11}', @TasaIva = '{12}', @SubTotal = '{13}', @Iva = '{14}', @Total = '{15}', " +
                        " @TipoImpuesto = '{16}', @Partida = '{17}', @SAT_ClaveProducto_Servicio = '{18}', @SAT_UnidadDeMedida = '{19}'  ",
                        sStoreDet,
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento,
                        Identificador, Clave, Descripcion, UnidadDeMedida,
                        Cantidad.ToString().Replace(",", ""), PrecioUnitario.ToString().Replace(",", ""), TasaIva.ToString().Replace(",", ""),
                        SubTotal.ToString().Replace(",", ""), Iva.ToString().Replace(",", ""), Total.ToString().Replace(",", ""), TipoImpuesto, Renglon,
                        sSAT_Producto_Servicio, sSAT_UnidadDeMedida);

                    if (!leerFacturar.Exec(sSql))
                    {
                        bDocumentoElectronico_Generado = false;
                        bRegresa = false;

                        bErrorAlGenerarFolio = true;
                        Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico");
                        break;
                    }



                    sSql = string.Format("Exec {0} " +
                        " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @Serie = '{4}', @Folio = '{5}', " +
                        " @Serie_Relacionada = '{6}',  @Folio_Relacionado = '{7}', " +
 	                    " @UUID_Relacionado = '{8}', @Total_Base = {9}, @SubTotal = {10}, " +
                        " @IVA = {11}, @Total = {12}  ",
                        sStoreDoctosRelacionados,
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento,
                        sSerie_Relacionada, iFolio_Relacionado, sUUID_Relacionado, 
                        dTotal_Base, SubTotal, Iva, Total);

                    if (sUUID_Relacionado == "")
                    {
                        bDocumentoElectronico_Generado = false;
                        bRegresa = false;

                        bErrorAlGenerarFolio = true;
                        Error.GrabarError(leerFacturar, string.Format("No se encontro el UUID de la Serie {0} y Folio {1}", sSerie_Relacionada, iFolio_Relacionado));
                    }
                    else
                    {
                        if (!leerFacturar.Exec(sSql))
                        {
                            bDocumentoElectronico_Generado = false;
                            bRegresa = false;

                            bErrorAlGenerarFolio = true;
                            Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico");
                            break;
                        }
                    }
                }

                if (bRegresa)
                {
                    leerMetodosDePago.RegistroActual = 1;
                    while (leerMetodosDePago.Leer())
                    {
                        if (leerMetodosDePago.CampoDouble("Importe") > 0)
                        {
                            sSql = string.Format("Exec {0} ", sStoreDet_MetodoDePago);
                            sSql += string.Format(
                           " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = '{4}', " +
                           " @IdMetodoDePago = '{5}', @Importe = '{6}', @Referencia = '{7}' ",
                           sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento,
                           leerMetodosDePago.Campo("IdFormaDePago"), leerMetodosDePago.CampoDouble("Importe"), leerMetodosDePago.Campo("Referencia"));

                            if (!leerFacturar.Exec(sSql))
                            {
                                bDocumentoElectronico_Generado = false;
                                bRegresa = false;

                                bErrorAlGenerarFolio = true;
                                Error.GrabarError(leerFacturar, "RegistrarFacturaElectronica");
                                break;
                            }
                        }
                    }
                }
            }

            if (bRegresa)
            {
                bRegresa = ObtenerImpuestos();
            }

            return bRegresa;
        }

        private bool ObtenerImpuestos()
        {
            bool bRegresa = true;
            string sTabla = VistaPrevia ? " FACT_CFD_Documentos_Generados_Detalles_VP " : " FACT_CFD_Documentos_Generados_Detalles ";

            string sSql = string.Format(
                "Select TipoImpuesto, TasaIva, sum(Iva) as Importe " +
                "From {0} D (NoLock) " +
                "Where IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' and Serie = '{4}' and Folio = '{5}' " +
                "Group by TipoImpuesto, TasaIva", sTabla, sIdEmpresa, sIdEstado, sIdFarmacia, sSerieDocumento, sFolioDocumento);

            if (!leerFacturar.Exec(sSql))
            {
                bDocumentoElectronico_Generado = false;
                bRegresa = false;

                bErrorAlGenerarFolio = true;
                Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico");
            }
            else
            {
                dtsListaImpuestos = leerFacturar.DataSetClase;
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
                    sSql += string.Format("Delete From FACT_CFD_Documentos_Generados_MetodosDePago_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    sSql += string.Format("Delete From FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);


                    leer.Exec(sSql);
                }
                else
                {
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
                    General.msjError("Ocurrió un error al obtener el Folio de factura electrónica.");
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
                    GuardarDocumentos_XML_PDF();
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
                    DtIFacturacion.GenerarImpresionWebBrowser(VistaPrevia, sFileName, false, DtIFacturacion.GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null, false, TipoDeDocumentoElectronico.NotaDeCredito);
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
                PrepararDocumento();
                if (RegistrarDocumentoElectronico())
                {
                    PrepararDocumento();
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

        private void PrepararDocumento()
        {
            if (tpPAC == PACs_Timbrado.Tralix)
            {
                DateTime dtFechaHora = General.FechaSistemaObtener().AddMinutes(-3);

                // Inicializar el CFDI 
                cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);

                sSerieDocumento = cboSeries.Data;
                // sFolioDocumento = sFolioDocumento;

                Encabezado(dtFechaHora);
                EncabezadoExt(dtFechaHora);
                DatosDeReceptor();
                DatosDePago();
                DatosDeEmbarque();
                Conceptos();
                ImpuestosTrasladados();
                ImpuestosRetenidos();
                Aduana();
                EnvioDocumentos();

                sIdCFDI = cfdi.Encabezado.Identificador_CFDI;
                sNombreDocumento = sIdCFDI + ".txt";
                sPlantillaDocumento = cfdi.GenerarPlantilla();
            }
        }

        private void Encabezado(DateTime FechaHora)
        {
            dSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva);
            dTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar);

            cfdi.TipoDocumento = tipoDocumento;
            cfdi.Encabezado.Serie = sSerieDocumento;
            cfdi.Encabezado.Folio = sFolioDocumento;
            cfdi.Encabezado.FechaHora = FechaHora;
            cfdi.Encabezado.SubTotal = dSubTotal;
            cfdi.Encabezado.Total = dTotal;
            cfdi.Encabezado.Moneda = cfdMoneda.Ninguno;
            cfdi.Encabezado.Observaciones_01 = sObservaciones_01;
            cfdi.Encabezado.Observaciones_02 = sObservaciones_02;
            cfdi.Encabezado.Observaciones_03 = sObservaciones_03;
            cfdi.Encabezado.Referencia = sReferencia;
        }

        private void EncabezadoExt(DateTime FechaHora)
        {
            Domicilio.RegistroActual = 1;
            Domicilio.Leer();
            cfdi.EncabezadoAdicional.NumeroCtaPago = formaMetodoPago.ListaCuentaDePago_Tralix;
            cfdi.EncabezadoAdicional.Serie = sSerieDocumento;
            cfdi.EncabezadoAdicional.Folio = sFolioDocumento;
            cfdi.EncabezadoAdicional.FechaHora = FechaHora;
            cfdi.EncabezadoAdicional.Total = dTotal;
            cfdi.EncabezadoAdicional.ExpedidoEn = Domicilio.Campo("Estado") + ", " + Domicilio.Campo("Municipio");

        }

        private void DatosDeReceptor()
        {
            DatosReceptor.Leer();
            DatosReceptor.RegistroActual = 1;
            sRFC_Receptor = DatosReceptor.Campo("RFC");

            if (tpPAC == PACs_Timbrado.Tralix)
            {
                cfdi.Receptor.IdReceptor = DatosReceptor.Campo("IdCliente");
                cfdi.Receptor.RFC = DatosReceptor.Campo("RFC");
                cfdi.Receptor.Nombre = DatosReceptor.Campo("Nombre");
                cfdi.Receptor.Nombre = sNombreReceptor;  //// Requerimiento de Oaxaca, modificar el Nombre relacioado al RFC 

                cfdi.Receptor.Pais = DatosReceptor.Campo("Pais");
                cfdi.Receptor.Estado = DatosReceptor.Campo("Estado");
                cfdi.Receptor.Municipio = DatosReceptor.Campo("Municipio");
                cfdi.Receptor.Localidad = DatosReceptor.Campo("Localidad");
                cfdi.Receptor.Colonia = DatosReceptor.Campo("Colonia");
                cfdi.Receptor.Calle = DatosReceptor.Campo("Calle");
                cfdi.Receptor.NumExterior = DatosReceptor.Campo("NoExterior");
                cfdi.Receptor.NumInterior = DatosReceptor.Campo("NoInterior");
                cfdi.Receptor.CodigoPostal = DatosReceptor.Campo("CodigoPostal");
                cfdi.Receptor.Referencia = DatosReceptor.Campo("Referencia");
            }
        }

        private void DatosDePago()
        {
            ////cfdi.Pago.FormaDePago = cboFormasDePago.Data;
            ////cfdi.Pago.CondicionesDePago = sObservacionesCondicionesDePago;
            ////cfdi.Pago.MetodoDePago = cboMetodosDePago.Data;
            ////cfdi.Pago.Observaciones = sObservacionesPago;

            ////if (chkVencimiento.Checked)
            ////{
            ////    cfdi.Pago.FechaDeVencimiento = General.FechaYMD(dtpFechaVencimiento.Value); 
            ////}

            cfdi.Pago.FormaDePago = formaMetodoPago.FormaDePago;
            cfdi.Pago.CondicionesDePago = formaMetodoPago.ObservacionesPago;
            cfdi.Pago.MetodoDePago = formaMetodoPago.ListadMetodoPago_Tralix;
            cfdi.Pago.Observaciones = formaMetodoPago.Observaciones;

            if (formaMetodoPago.TieneFechaVencimiento)
            {
                cfdi.Pago.FechaDeVencimiento = General.FechaYMD(formaMetodoPago.FechaVencimiento);
            }

        }

        private void DatosDeEmbarque()
        {
            ////cfdi.Receptor.IdReceptor = DatosReceptor.Campo("IdCliente");
            ////cfdi.Receptor.RFC = DatosReceptor.Campo("RFC");
            ////cfdi.Receptor.Nombre = DatosReceptor.Campo("Nombre");
            ////cfdi.Receptor.Pais = DatosReceptor.Campo("Pasi");
            ////cfdi.Receptor.Estado = DatosReceptor.Campo("Estado");
            ////cfdi.Receptor.Municipio = DatosReceptor.Campo("Municipio");
            ////cfdi.Receptor.Localidad = DatosReceptor.Campo("Localidad");
            ////cfdi.Receptor.Colonia = DatosReceptor.Campo("Colonia");
            ////cfdi.Receptor.Calle = DatosReceptor.Campo("Calle");
            ////cfdi.Receptor.NumExterior = DatosReceptor.Campo("NoExterior");
            ////cfdi.Receptor.NumInterior = DatosReceptor.Campo("NoInterior");
            ////cfdi.Receptor.CodigoPostal = DatosReceptor.Campo("CodigoPostal");
            ////cfdi.Receptor.Referencia = DatosReceptor.Campo("Referencia");
        }

        private void Conceptos()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                xsaCfdiConcepto item = new xsaCfdiConcepto();

                item.ID_Interno = i.ToString();//myGrid.GetValue(i, (int)Cols.IdClave);
                item.ID_Externo = "";// myGrid.GetValue(i, (int)Cols.ClaveSSA);
                item.Descripcion = txtDescripcion.Text.Trim();//myGrid.GetValue(i, (int)Cols.Descripcion);
                item.ValorUnitario = Convert.ToDouble(myGrid.GetValueDou(i, (int)Cols.Importe_Sin_Iva));
                item.Cantidad = 1;// Convert.ToDouble(myGrid.GetValue(i, (int)Cols.Cantidad));
                item.Importe = Convert.ToDouble(myGrid.GetValueDou(i, (int)Cols.Importe_A_Aplicar));
                item.UnidadDeMedida =  DtIFacturacion.NotaDeCredito_SAT_UnidadDeMedida;// myGrid.GetValue(i, (int)Cols.UnidadDeMedida);

                cfdi.Conceptos.Add(item);
            }
        }

        private void ImpuestosTrasladados()
        {
            clsLeer impuestos = new clsLeer();
            impuestos.DataSetClase = dtsListaImpuestos;

            while (impuestos.Leer())
            {
                dTasaIva = impuestos.CampoDouble("TasaIva");
                dIva = impuestos.CampoDouble("Importe");
                cfdi.ImpuestosTrasladados.Add(impuestos.Campo("TipoImpuesto"), dTasaIva, dIva);
            }

            //for (int i = 1; i <= myGrid.Registros; i++)
            //{
            //    dTasaIva = myGrid.GetValueDouble(i, (int)Cols.TasaIva);
            //    dIva = myGrid.GetValueDouble(i, (int)Cols.Iva);
            //    cfdi.ImpuestosTrasladados.Add(cfdImpuestosTrasladados.IVA, dTasaIva, dIva);
            //}
        }

        private void ImpuestosRetenidos()
        {
        }

        private void Aduana()
        {
        }

        private void EnvioDocumentos()
        {
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
            string sImpresion = VistaPrevia ? " spp_FACT_CFDI_NotaDeCredito_ObtenerDatos_VP " : " spp_FACT_CFDI_NotaDeCredito_ObtenerDatos ";

            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI);

            //if (!bRegresa)
            {
                CFDI_FD = new clsGenCFDI_33_NotaDeCredito();
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
        //private void btnAgregarConcepto_Click(object sender, EventArgs e)
        //{
        //    Conceptos(1, myGrid.ActiveRow);
        //}

        //private void btnEliminarConcepto_Click(object sender, EventArgs e)
        //{
        //    if (myGrid.Rows > 0)
        //    {
        //        myGrid.DeleteRow(myGrid.ActiveRow);
        //    }
        //}

        //private void btnModificarConcepto_Click(object sender, EventArgs e)
        //{
        //    Conceptos(2, myGrid.ActiveRow);
        //}

        //private void Conceptos(int Tipo, int Renglon)
        //{
        //    int iID = 0;
        //    string Identificador = Renglon.ToString();// myGrid.GetValue(Renglon, (int)Cols.IdClave);
        //    string Clave = ""; //myGrid.GetValue(Renglon, (int)Cols.ClaveSSA);
        //    string Descripcion = txtDescripcion.Text.Trim();//myGrid.GetValue(Renglon, (int)Cols.Descripcion);
        //    double PrecioUnitario = myGrid.GetValueDou(Renglon, (int)Cols.Importe_A_Aplicar);
        //    int Cantidad = 1;// myGrid.GetValueInt(Renglon, (int)Cols.Cantidad);
        //    double TasaIva = myGrid.GetValueDou(Renglon, (int)Cols.TasaIva);
        //    double SubTotal = myGrid.GetValueDou(Renglon, (int)Cols.Importe_A_Aplicar);
        //    double Iva = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Iva);
        //    double Total = myGrid.GetValueDou(Renglon, (int)Cols.Importe_Total);
        //    string UnidadDeMedida = DtIFacturacion.Factura_SAT_UnidadDeMedida__Servicio;// myGrid.GetValue(Renglon, (int)Cols.UnidadDeMedida);
        //    string TipoImpuesto = "IVA"; //myGrid.GetValue(Renglon, (int)Cols.TipoImpuesto);


        //    if (Identificador == "")
        //    {
        //        Identificador = "0";
        //    }

        //    if (Tipo == 1)
        //    {
        //        FrmDetallesDocumentos f = new FrmDetallesDocumentos();

        //        f.UnidadesDeMedida = dtsUnidadesDeMedida;
        //        f.Identificador = iID.ToString();
        //        f.ShowDialog();

        //        if (f.Guardado)
        //        {
        //            Identificador = f.Identificador;
        //            Clave = f.Clave;
        //            Descripcion = f.Descripcion;
        //            UnidadDeMedida = f.UnidadDeMedida;
        //            Cantidad = f.Cantidad;
        //            PrecioUnitario = f.PrecioUnitario;
        //            TasaIva = f.TasaIva;
        //            SubTotal = f.SubTotal;
        //            Iva = f.Iva;
        //            Total = f.Total;
        //            TipoImpuesto = f.TipoImpuesto;

        //            //ListViewItem itmX = listvClaves.Items.Add(Identificador);
        //            //itmX.SubItems.Add(Clave);
        //            //itmX.SubItems.Add(Descripcion);
        //            //itmX.SubItems.Add(PrecioUnitario.ToString(sFormato));
        //            //itmX.SubItems.Add(Cantidad.ToString(sFormatoIva));
        //            //itmX.SubItems.Add(TasaIva.ToString(sFormatoIva));
        //            //itmX.SubItems.Add(SubTotal.ToString(sFormato));
        //            //itmX.SubItems.Add(Iva.ToString(sFormato));
        //            //itmX.SubItems.Add(Total.ToString(sFormato));
        //            //itmX.SubItems.Add(UnidadDeMedida);
        //            //itmX.SubItems.Add(TipoImpuesto);
        //        }
        //    }

        //    if (Tipo == 2)
        //    {
        //        FrmDetallesDocumentos f = new FrmDetallesDocumentos();

        //        f.UnidadesDeMedida = dtsUnidadesDeMedida;
        //        f.Identificador = Identificador;
        //        f.Clave = Clave;
        //        f.Descripcion = Descripcion;
        //        f.UnidadDeMedida = UnidadDeMedida;
        //        f.Cantidad = Cantidad;
        //        f.PrecioUnitario = PrecioUnitario;
        //        f.TasaIva = TasaIva;
        //        f.SubTotal = SubTotal;
        //        f.Iva = Iva;
        //        f.Total = Total;
        //        f.TipoImpuesto = TipoImpuesto;

        //        f.ShowDialog();

        //        if (f.Guardado)
        //        {
        //            Identificador = f.Identificador;
        //            Clave = f.Clave;
        //            Descripcion = f.Descripcion;
        //            UnidadDeMedida = f.UnidadDeMedida;
        //            Cantidad = f.Cantidad;
        //            PrecioUnitario = f.PrecioUnitario;
        //            TasaIva = f.TasaIva;
        //            SubTotal = f.SubTotal;
        //            Iva = f.Iva;
        //            Total = f.Total;
        //            TipoImpuesto = f.TipoImpuesto;

        //            myGrid.SetValue(Renglon, (int)Cols.IdClave, Identificador);
        //            myGrid.SetValue(Renglon, (int)Cols.ClaveSSA, Clave);
        //            myGrid.SetValue(Renglon, (int)Cols.Descripcion, Descripcion);
        //            myGrid.SetValue(Renglon, (int)Cols.UnidadDeMedida, UnidadDeMedida);
        //            myGrid.SetValue(Renglon, (int)Cols.Cantidad, Cantidad.ToString(sFormatoIva));
        //            myGrid.SetValue(Renglon, (int)Cols.PrecioUnitario, PrecioUnitario.ToString(sFormato));
        //            myGrid.SetValue(Renglon, (int)Cols.TasaIva, TasaIva.ToString(sFormatoIva));
        //            myGrid.SetValue(Renglon, (int)Cols.SubTotal, SubTotal.ToString(sFormato));
        //            myGrid.SetValue(Renglon, (int)Cols.Iva, Iva.ToString(sFormato));
        //            myGrid.SetValue(Renglon, (int)Cols.Total, Total.ToString(sFormato));
        //            myGrid.SetValue(Renglon, (int)Cols.TipoImpuesto, TipoImpuesto);
        //        }
        //    }

        //    MostrarPago(false);
        //}
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



        private void IniciaToolBarExcel(bool Nuevo, bool Abrir, bool LeerArchivo, bool LeerHoja, bool BloquearHojas)
        {
            //btnNuevoExcel.Enabled = Nuevo;
            //btnAbrirExcel.Enabled = Abrir;
            //btnLeerExcel.Enabled = LeerArchivo;
            //btnBloquearHoja.Enabled = BloquearHojas;
        }

        private void btnNuevoExcel_Click(object sender, EventArgs e)
        {
            lblCantidadConLetra.Text = "";
            sFile_In = "";
            ////listvClaves.Items.Clear();

            ////IniciarEtiquetasExcel();
            ////IniciarEtiquetas_CalculoImportes(false);
            IniciaToolBarExcel(true, true, false, false, false);
        }

        private void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Notas de Crédito masivas";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false;

            ////if (openExcel.FileName != "")
                if (openExcel.ShowDialog() == DialogResult.OK)
                {
                    sFile_In = openExcel.FileName;
                    //lblArchivoExcel.Text = sFile_In;

                    //cboHojas.Clear();
                    //cboHojas.Add();
                    //cboHojas.SelectedIndex = 0;
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
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        //private void btnBloquearHoja_Click(object sender, EventArgs e)
        //{
        //    if (bErrorEnImportes)
        //    {
        //        MensajeErrorImportes();
        //    }
        //    else
        //    {
        //        ////cboHojas.Enabled = !cboHojas.Enabled;
        //    }
        //}

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
            //listvClaves.Items.Clear();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            ////cboHojas.SelectedIndex = 0;
            ////btnLeerExcel.Enabled = bHabilitar;
            //IniciaToolBarExcel(true, true, bHabilitar, false, false);

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

            ////cboHojas.Clear();
            ////cboHojas.Add();
            ////cboHojas.SelectedIndex = 0;
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                ////cboHojas.Add(sHoja, sHoja);
            }

            ////cboHojas.SelectedIndex = 0;
            ////btnLeerExcel.Enabled = bHabilitar;
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            ////MostrarEnProceso(true, 2);
            ////lblProcesados.Visible = false;

            lblCantidadConLetra.Text = "";
            LeerHoja();

            BloqueaHojas(false);

            bLeyendoHoja = false;
            ////MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            //FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                leer.DataSetClase = excel.DataSetClase;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if (bRegresa)
            {
                bRegresa = ValidarColumnas();
            }

            return bRegresa;
        }

        private bool ValidarColumnas()
        {
            bool bRegresa = false;

            bRegresa = leer.ExisteTablaColumna(1, "Serie") && leer.ExisteTablaColumna(1, "Folio") && leer.ExisteTablaColumna(1, "TasaIVA") && leer.ExisteTablaColumna(1, "Importe_A_Aplicar");


            if (!bRegresa)
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
            bulk.DestinationTableName = "FACT_CFDI_NotasDeCredito__CargaMasiva";

            if (leer.ExisteTablaColumna(1, "IdEmpresa")) bulk.AddColumn("IdEmpresa", "IdEmpresa");
            if (leer.ExisteTablaColumna(1, "IdEstado")) bulk.AddColumn("IdEstado", "IdEstado");
            if (leer.ExisteTablaColumna(1, "IdFarmacia")) bulk.AddColumn("IdFarmacia", "IdFarmacia");


            bulk.AddColumn("Serie", "Serie");
            bulk.AddColumn("Folio", "Folio");
            bulk.AddColumn("TasaIVA", "TasaIVA");
            bulk.AddColumn("Importe_A_Aplicar", "Importe_A_Aplicar");
            if (leer.ExisteTablaColumna(1, "Clave SSA")) bulk.AddColumn("Clave SSA", "ClaveSSA");

            leerFacturar = new clsLeer(ref cnn); 
            leerFacturar.Exec("Truncate table FACT_CFDI_NotasDeCredito__CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if (bRegresa)
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
            string sSql = string.Format("Exec spp_PRCS_FACT__NotasDeCredito_Validar_Datos  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @RFC = '{3}', @Detallado = 1  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sRFC_Receptor);

            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla.";
                Error.GrabarError(leer, "validarInformacion()");
            }
            else
            {
                if (ValidarInformacion_Entrada()) 
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


            bActivarProceso = false; 
            leer.RenombrarTabla(1, "Resultados");
            leerResultado.DataTableClase = leer.Tabla(1);
            while (leerResultado.Leer())
            {
                leer.RenombrarTabla(leerResultado.CampoInt("Orden"), leerResultado.Campo("NombreTabla"));
            }

            dtsResultados = leer.DataSetClase;
            dtsResultados.Tables.Remove("Resultados");
            leer.DataSetClase = dtsResultados;

            foreach (DataTable dt in leer.DataSetClase.Tables)
            {
                if (!bActivarProceso)
                {
                    leerValidacion.DataTableClase = dt.Copy();
                    bActivarProceso = leerValidacion.Registros > 0;
                }
            }

            bRegresa = !bActivarProceso;

            return bRegresa;
        }

        private bool CargarInformacion_Validada()        
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_PRCS_FACT__NotasDeCredito_ObtenerDatos  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @RFC = '{3}'   ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sRFC_Receptor);

            myGrid.Limpiar(false); 
            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                sMensajeError_CargarPlantilla = "Ocurrió un error al validar los datos de la plantilla.";
                Error.GrabarError(leer, "CargarInformacion_Validada()");
            }
            else
            {
                bRegresa = true;
                myGrid.LlenarGrid(leer.DataSetClase, false, false); 
            }

            lblRegistros.Text = string.Format("Registros : {0}", myGrid.Rows.ToString("###, ###, ###, ##0"));
            lblSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva).ToString("###, ###, ###, ##0.0000");
            lblIva.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_Iva).ToString("###, ###, ###, ##0.0000");
            lblTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar).ToString("###, ###, ###, ##0.0000");

            return bRegresa; 
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            //lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");
        }

        private void BloqueaHojas(bool Bloquear)
        {
            //cboHojas.Enabled = !Bloquear;
        }
        #endregion Importar Excel


        private void grdNota_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;

            switch (ColActiva)
            {
                case Cols.Serie:
                case Cols.Folio:
                    
                    string sSerie = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Serie);
                    string sFolio = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);

                    if (sSerie != "" && sFolio != "")
                    {
                        BuscarDocumento(sSerie, sFolio);

                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_A_Aplicar, 0.0000);
                    }
                    else
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.UUID, "");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_Base, "");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.TasaIva, "");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Anteriores, "");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_A_Aplicar, 0.0000);
                    }
    
                    break;
                
                case Cols.Importe_A_Aplicar:


                    double dCantidad_A_APlicar  = myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Importe_A_Aplicar);
                    double dCantidad_Base = myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Importe_Base);
                    double dCantidad_Anteior = myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Anteriores);
                    double dTasaIva = myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.TasaIva);


                    if (dCantidad_Base < (dCantidad_A_APlicar + dCantidad_Anteior))
                    {
                        dCantidad_A_APlicar = dCantidad_Base - dCantidad_Anteior;
                        General.msjAviso("La Cantidad por aplicar no puede ser mayor a la cantidad Disponible.");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_A_Aplicar, dCantidad_A_APlicar);
                    }

                    double dSubTotal = dCantidad_A_APlicar * 100 / (100 + dTasaIva);
                    double dImporteIva = dCantidad_A_APlicar - dSubTotal;

                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_Sin_Iva, dSubTotal);
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_Iva, dImporteIva);


                   break;

            }
            //Totalizar();
            lblRegistros.Text = string.Format("Registros : {0}", myGrid.Rows.ToString("###, ###, ###, ##0"));
            lblSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_Sin_Iva).ToString("###, ###, ###, ##0.0000");
            lblIva.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_Iva).ToString("###, ###, ###, ##0.0000");
            lblTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe_A_Aplicar).ToString("###, ###, ###, ##0.0000");
        }


        private void BuscarDocumento(string sSerie, string sFolio)
        {
            bool bEncontrado = false;
            bool bConError = false;

            string sSql = string.Format("Select *, " +
                                           "dbo.fg_FACT_NotasDeCredito_TasaIva(IdEmpresa, IdEstado, IdFarmacia, Serie, Folio) As TasaIva " +
                                           "From vw_FACT_CFD_DocumentosElectronicos (NoLock) " +
                                           "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' ",
                                           IdEmpresa, IdEstado, IdFarmacia, sSerie, sFolio); // + Fg.PonCeros(IdCliente, 4) + "'";

            //leer.DataSetClase = consulta.CFDI_Documentos(sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio, "grdNota_EditModeOff");
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "grdNota_EditModeOff()");
                General.msjError("Ocurrió un error al obtener los datos.");
                bConError = true;
            }
            else
            {
                if (leer.Leer())
                {
                    bEncontrado = true;
                }
            }

            if (!bEncontrado & !bConError)
            {
                if (validarDatosDeConexion())
                {
                    if (!leerWeb.Exec(sSql))
                    {
                        Error.GrabarError(leerWeb, "grdNota_EditModeOff()");
                        General.msjError("Ocurrió un error al obtener los datos.");
                        bConError = true;
                    }
                    else
                    {
                        if (leerWeb.Leer())
                        {
                            bEncontrado = true;
                            leer.DataSetClase = leerWeb.DataSetClase;
                            leer.Leer();
                        }
                    }
                }
            }

            if (!bConError)
            {
                if (bEncontrado)
                {
                    string sRFC = leer.Campo("RFC");
                    if (sRFC_Receptor != sRFC)
                    {
                        General.msjAviso("La factura pertenece a otro cliente.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                        myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.Folio);
                        bEncontrado = false;
                    }
                    else
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.UUID, leer.Campo("UUID"));
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe_Base, leer.CampoDouble("Total"));
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.TasaIva, leer.CampoDouble("TasaIva"));
                    }
                }
                else
                {
                    General.msjAviso("Documento no encontrado, verifique.");
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.Folio);
                }
            }

            if (!bConError)
            {
                sSql = string.Format("Select dbo.fg_FACT_NotasDeCredito_Total_Previo('{0}', '{1}', '{2}', '{3}', '{4}') As Anterior",
                                     IdEmpresa, IdEstado, IdFarmacia, sSerie, sFolio);

                if (bEncontrado)
                {
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "grdNota_EditModeOff()");
                        General.msjError("Ocurrió un error al obtener los datos.");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Anteriores, leer.CampoDouble("Anterior"));
                        }
                    }
                }
            }
        }

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
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con el servidor, intente de nuevo.");
            }

            return bRegresa;
        }

        private void ObtenerServidoresRemotos()
        {
            string sSql = string.Format("Select * From CFGCF_ConfigurarConexion (NoLock) Where Status = 'A'");

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    sUrl = string.Format("http://{0}/{1}/{2}.asmx",
                        leer.Campo("Servidor"), leer.Campo("WebService"), leer.Campo("PaginaWeb"));
                }
            }

        }

    }
}
