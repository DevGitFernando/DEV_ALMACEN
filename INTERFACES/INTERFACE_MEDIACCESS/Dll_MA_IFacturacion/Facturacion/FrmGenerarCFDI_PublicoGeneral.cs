#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Drawing; 
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
using Dll_MA_IFacturacion; 
using Dll_MA_IFacturacion.Configuracion;
#endregion USING

#region USING WEB SERVICES
using Dll_MA_IFacturacion.xsasvrCancelCFDService;
using Dll_MA_IFacturacion.xsasvrCFDService;
using Dll_MA_IFacturacion.xsasvrFileReceiverService;
using Dll_MA_IFacturacion.XSA;
#endregion USING WEB SERVICES


using Dll_MA_IFacturacion.Catalogos; 
using Dll_MA_IFacturacion.CFDI;
using Dll_MA_IFacturacion.CFDI.CFDFunctions;
using Dll_MA_IFacturacion.CFDI.CFDFunctionsEx;
using Dll_MA_IFacturacion.CFDI.geCFD; 


namespace Dll_MA_IFacturacion.Facturacion
{
    public partial class FrmGenerarCFDI_PublicoGeneral : FrmBaseExt
    {
        #region Declaracion de Variables
        ////enum Cols
        ////{
        ////    IdClave = 1, ClaveSSA = 2, Descripcion = 3, PrecioUnitario = 4, Cantidad = 5,
        ////    TasaIva = 6, SubTotal = 7, Iva = 8, Total = 9, UnidadDeMedida = 10, TipoImpuesto = 11
        ////}

        enum Cols
        {
            Ninguna = 0,
            CodigoEAN, IdProducto, DescripcionComercial, PrecioUnitario, PorcentajeCobro, PrecioUnitarioFinal,
            Cantidad, TasaIva, SubTotal, Iva, Importe, UnidadDeMedida,
            SAT_ClaveProductoServicio, SAT_UnidadDeMedida, ProductoParaFacturacion    
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerFacturar;
        clsListView list;
        clsAyudas_CFDI Ayuda;
        clsConsultas_CFDI ConsultasCFDI;

        clsConsultas Consultas;
        TipoDeVenta tpVenta = TipoDeVenta.Ninguna; 

        clsLeer Empresa = new clsLeer();
        clsLeer Sucursal = new clsLeer();
        clsLeer Domicilio = new clsLeer();
        clsLeer DatosReceptor = new clsLeer();
        clsFormaMetodoPago formaMetodoPago;
        DataSet dtsUnidadesDeMedida = new DataSet();
        DataSet dtsListaImpuestos = new DataSet();

        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        private bool bProcesando = false;
        //private bool bFacturaGenerada = false;
        private string sSerieDocumento = "";
        private string sFolioDocumento = "";
        private string sIdCFDI = "";
        
        private string sSerieFactura = "";
        private string sFolioFactura = "";
        string sIdCliente = "";

        bool bErrorAlGenerarFolio = false;
        bool bFacturaElectronica_Generada = false;
        string sGUID = ""; 

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;
        private string sFolioRemision = "";
        private string sFolioVenta = ""; 
        private bool bInformacionEmisor = false;
        string sTipoDoctoFactura = "001";
        bool bFolioDeVentaFacturado = false;
        bool bProductos_NoConfigurados_SAT = false; 

        clsGrid myGrid;

        double fSubTotal_SinGravar = 0; 
        double fSubTotal_Gravado = 0; 
        double fIva = 0;
        double fTotal = 0;

        double dDescuentoCopago = 0;
        double dTotal_a_Pagar = 0;
        ////clsValidar_Elegibilidad localEligibilidad;

        //Thread thRemision;
        Thread thFacturaElectronica;
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
        private string sNombreReceptor = "";

        string sTipoDeDocumento = "";
        //bool bErrorAlGenerarFolio = false;
        bool bDocumentoElectronico_Generado = false;

        double dSubTotal = 0;
        double dTasaIva = 0;
        double dIva = 0;
        double dTotal = 0;
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
        string sXMLCondicionesPago = "Contado";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = "";
        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_MA_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_33_Factura CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_Factura myCFDI = null; //new clsGenCFDI_Ex();
        bool bSerieConfigurada = false; 


        DataSet dtsSeries = new DataSet();

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";
        //string sFormato = "$ #,#0.00";
        //string sFormatoIva = "$ #,#0.00";

        SC_ControlsCS.scComboBoxExt cboSeries = new SC_ControlsCS.scComboBoxExt();
        ListView lstFacturacion = new ListView();

        string sFileName_Cliente = "";
        string sIdClienteSeleccionado = "";
        string sRutaTmpImpresion = DtIFacturacion.RutaCFDI_DocumentosImpresion + @"\" + DtGeneral.EmpresaDatos.RFC;

        #endregion Declaracion de Variables de Facturacion

        public FrmGenerarCFDI_PublicoGeneral()
        {
            InitializeComponent();
            DtIFacturacion.ModuloActivo = Modulo_CFDI.Facturacion_Sucursal;

            DtIFacturacion.InicialiarParametros__Facturacion(); 


            CheckForIllegalCrossThreadCalls = false;

            sIdEmpresa = DtGeneral.EmpresaConectada;
            sIdEstado = DtGeneral.EstadoConectado;
            sIdFarmacia = DtGeneral.FarmaciaConectada;
            this.sFolioRemision = ""; //FolioRemision;

            ////// Inicializar el CFDI 
            ////cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnFacturar.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            ////// this.Top = 212; 
            ////this.FrameDetalles.Height = 464;
            ////this.FrameProceso.Top = 270;

            ////lblCantidadConLetra.BorderStyle = BorderStyle.None;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ConsultasCFDI = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Ninguna, eVersionCFDI.Version__3_3);

            list = new clsListView(lstFacturacion);

            ////tpPAC = DtIFacturacion.PAC_Informacion.PAC;

            //// Anexar al documento la Remisión relacionada 
            // sReferencia = "R" + sFolioRemision; 

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);

            DtIFacturacion.InicializarInformacionEmisor(); 

        }

        private void FrmGenerarCFDI_PublicoGeneral_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
            Obtener_UsoCFDI(); 
            ObtenerSeries_y_Folios();
        }

        #region Botones 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Imprimir, bool EnviarEmail)
        {
            btnFacturar.Enabled = Guardar;
            btnValidarDatos.Enabled = Guardar;

            btnImprimir.Enabled = Imprimir;
            btnSendEmail.Enabled = EnviarEmail; 
        }

        private void InicializarPantalla()
        {
            bProductos_NoConfigurados_SAT = false;
            Fg.IniciaControles();
            list = new clsListView(lstFacturacion);

            IniciarToolBar(); 

            btnCliente.Enabled = true;
            btnObservacionesGral.Enabled = true;
            btnPago.Enabled = true; 

            bFolioDeVentaFacturado = false;  
            sIdCliente = ""; 
            lblCancelado.Text = "CANCELADO";
            lblCancelado.Visible = false;
            lblSerieFactura.Text = "";
            lblSerieFactura.Visible = false; 

            myGrid.Limpiar(false);
            grdProductos.Enabled = false;

            dtpFechaRegistro.Enabled = false;
            ////lblRazonSocial.Enabled = false; 

            txtSubTotal_Gravado_AntesDescuento.Enabled = false;
            txtSubTotal_NoGravado_AntesDescuento.Enabled = false;
            txtDescuento.Enabled = false;

            txtSubTotal_Gravado.Enabled = false;
            txtSubTotal_NoGravado.Enabled = false;
            txtIVA.Enabled = false;
            txtImporte_Factura.Enabled = false;


            fSubTotal_Gravado = 0;
            fSubTotal_SinGravar = 0;
            fIva = 0;
            fTotal = 0; 
            txtSubTotal_Gravado.Text = fSubTotal_Gravado.ToString(sFormato);
            txtSubTotal_NoGravado.Text = fSubTotal_SinGravar.ToString(sFormato);
            txtIVA.Text = fIva.ToString(sFormato);
            txtImporte_Factura.Text = fTotal.ToString(sFormato);


            MostrarEnProceso(false);
            bFacturaElectronica_Generada = false;
            bErrorAlGenerarFolio = false;

            sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
            sXMLCondicionesPago = "Crédito";
            sXMLMetodoPago = "No identificado";


            txtRFC.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla(); 
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            VistaPrevia = false;
            if (validarGenerarFactura())
            {
                thFacturar();
            }
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {

        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FrmClientes f = new FrmClientes();
            f.MostrarClientes(sIdCliente);

            if (f.ClienteNuevo)
            {
                sIdCliente = f.Cliente; 
                txtRFC.Enabled = false;
                txtRFC.Text = f.RFC;
                lblRazonSocial.Text = f.ClienteNombre; 
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
            formaMetodoPago.ImporteACobrar = fTotal; 
            formaMetodoPago.Show(); 
        }

        private void btnDatosFiscales_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            VistaPrevia = false;
            DtIFacturacion.EsEnvioDeFactura_EMail = false; 
            ImprimirDocumento_Timbrado(); 
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            EnviarFactura_EMail(); 
        }

        private void EnviarFactura_EMail()
        {
            string sMail = "";
            string sMensaje = DtIFacturacion.DatosEmail.MensajePredeterminado;
            string sIdCliente_Docto = "";
            FrmClienteEMails mails;

            try
            {
                ////sFileName = lvwRecordsComprobantes.SelectedItems[0].SubItems[21].Text;
                ////sMail = lvwRecordsComprobantes.SelectedItems[0].SubItems[24].Text;
            }
            catch { }

            VistaPrevia = false;
            DtIFacturacion.EsEnvioDeFactura_EMail = true; 
            ImprimirDocumento_Timbrado(false);

            if (File.Exists(Path.Combine(sRutaTmpImpresion, sFileName_Cliente + ".xml")))
            {
                if (File.Exists(Path.Combine(sRutaTmpImpresion, sFileName_Cliente + ".pdf")))
                {
                    mails = new FrmClienteEMails(EnvioDeCorreos.Clientes, sIdCliente, sRutaTmpImpresion, sFileName_Cliente);
                    mails.ShowDialog();
                }
            }
        }
        #endregion Botones

        #region Informacion 
        #region Datos generales 
        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            lblRazonSocial.Text = ""; 
        }

        private void txtRFC_Validated(object sender, EventArgs e)
        {
            if (txtRFC.Text != "")
            {
                leer.DataSetClase = ConsultasCFDI.CFDI_Clientes(txtRFC.Text, false, true, "txtId_Validating");
                if (leer.Leer())
                {
                    DatosCliente();
                }
            }
        }

        private void txtRFC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.CFDI_Clientes(false, "txtId_KeyDown");
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
                txtRFC.Text = "";
                General.msjAviso("El cliente capturado esta cancelado, no es posible emitirle documentos.");
                txtRFC.Focus();
            }
            else
            {
                txtRFC.Enabled = false;
                sIdCliente = leer.Campo("IdCliente");
                txtRFC.Text = leer.Campo("RFC");
                lblRazonSocial.Text = leer.Campo("Nombre");
                DatosReceptor.DataSetClase = leer.DataSetClase;
            }
        }

        private void txtFolio_Validated(object sender, EventArgs e)
        {            
            lblCancelado.Visible = false;

            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas(sIdEmpresa, sIdEstado, sIdFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Folio de venta no encontrado, Verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    IniciarToolBar(true, false, false);
                    txtFolio.Enabled = false;
                    grdProductos.Enabled = true;
                    sFolioVenta = leer.Campo("Folio");
                    txtFolio.Text = sFolioVenta;

                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Text = "CANCELADO";
                    }
                    else
                    {
                        tpVenta = (TipoDeVenta)leer.CampoInt("TipoDeVenta");
                        lblCancelado.Text = tpVenta == TipoDeVenta.Credito ? "VENTA ASEGURADO" : "VENTA PÚBLICO GENERAL";
                    }


                    lblCancelado.Visible = true;
                    Application.DoEvents();
                    this.Refresh();

                    CargaDetallesVenta();
                }
            }
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion Datos generales

        #region Datos de venta 
        private bool CargaDetallesVenta()
        {
            bool bRegresa = true;
            list.Limpiar();
            bFolioDeVentaFacturado = false; 

            leer.DataSetClase = ConsultasCFDI.CFDI_Datos_FacturacionEnSitio(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioVenta, "CargaDetallesVenta");
            if (leer.Leer())
            {
                sIdCliente = leer.Campo("IdCliente");
                cboUsosCFDI.Data = leer.Campo("UsoDeCFDI");
                myGrid.LlenarGrid(leer.DataSetClase, false, false);
                list.CargarDatos(leer.DataSetClase, true, true);

                bFolioDeVentaFacturado = leer.CampoBool("Facturado"); 
            }
            else
            {
                bRegresa = false;
            }

            // myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BloqueaColumna(true, 1);
            ValidarProductos_SAT(); 
            CalcularTotales();


            if (bFolioDeVentaFacturado)
            {
                ///lblCancelado.Text += " FACTURADO :  ";
                lblCancelado.Text += string.Format("        FACTURADO : {0}-{1}", leer.Campo("Serie"), leer.CampoInt("Folio")); 

                BloquearControles(true);
                btnNuevo.Enabled = true;

                txtRFC.Text = leer.Campo("RFC");
                lblRazonSocial.Text = leer.Campo("NombreCliente"); 
                PK = leer.CampoInt("Identificador");

                //lblSerieFactura.Visible = true;
                //lblSerieFactura.Text = string.Format("{0}-{1}", leer.Campo("Serie"), leer.CampoInt("Folio")); 

                IniciarToolBar(false, true, true);
            }


            return bRegresa;
        }

        private void ValidarProductos_SAT()
        {
            Color colorValidar = Color.Red; 
            int iErrores = 0;
            bProductos_NoConfigurados_SAT = false; 

            for(int i = 1; i <= myGrid.Rows; i++)
            {
                if (!myGrid.GetValueBool(i, (int)Cols.ProductoParaFacturacion))
                {
                    myGrid.ColorRenglon(i, colorValidar); 
                    iErrores++;
                }
            }

            bProductos_NoConfigurados_SAT = iErrores > 0;
        }

        private void CalcularTotales()
        {
            double sSubTotal_SinGravar = 0; 
            double sSubTotal = 0; 
            double sIva = 0;
            double sTotal = 0;

            fSubTotal_SinGravar = 0; 
            fSubTotal_Gravado = 0;
            fIva = 0;
            fTotal = 0;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if ( myGrid.GetValueDou(i, (int)Cols.TasaIva) == 0 )
                {
                    fSubTotal_SinGravar += DtGeneral.Redondear(myGrid.GetValueDou(i, (int)Cols.SubTotal), 2); 
                }
                else 
                {
                    fSubTotal_Gravado += DtGeneral.Redondear(myGrid.GetValueDou(i, (int)Cols.SubTotal), 2); 
                }
            }


            fIva = DtGeneral.Redondear(myGrid.TotalizarColumnaDou((int)Cols.Iva), 2);
            fTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou((int)Cols.Importe), 2);


            txtSubTotal_Gravado.Text = fSubTotal_Gravado.ToString(sFormato);
            txtSubTotal_NoGravado.Text = fSubTotal_SinGravar.ToString(sFormato);
            txtIVA.Text = fIva.ToString(sFormato);
            txtImporte_Factura.Text = fTotal.ToString(sFormato); 

        }

        #endregion Datos de venta
        #endregion Informacion

        #region Facturacion Electronica 
        private void Obtener_UsoCFDI()
        {
            cboUsosCFDI.Clear();
            cboUsosCFDI.Add();
            cboUsosCFDI.Add(ConsultasCFDI.CFDI_UsosDeCFDI("Obtener_UsoCFDI()"), true, "Clave", "UsoDeCFDI");
            cboUsosCFDI.SelectedIndex = 0;
        } 

        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add();
            cboSeries.Add(ConsultasCFDI.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoFactura, "ObtenerSeries_y_Folios()"), true, "Serie", "Serie");
            cboSeries.SelectedIndex = 0;

            if (cboSeries.NumeroDeItems > 1)
            {
                bSerieConfigurada = true; 
                cboSeries.SelectedIndex = 1;
            }

        }
        private bool validarGenerarFactura()
        {
            bool bRegresa = true;
            sNombreReceptor = lblRazonSocial.Text;

            ////if (!bInformacionEmisor)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No se cuenta con la información de la sucursal, no es posible generar la factura sin estos datos."); 
            ////}

            if (bRegresa & txtRFC.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Cliente al cual se le emitira la factura, verifique.");
                txtRFC.Focus();
            }

            if (bRegresa && !bSerieConfigurada)
            {
                bRegresa = false;
                General.msjUser("La farmacia no cuenta con una Serie de facturación válida, verifique.");
                //cboSeries.Focus();
            }

            if (bRegresa && list.Registros == 0)
            {
                bRegresa = false;
                General.msjUser("No existen detalles para generar la factura, verifique.");
            }

            if (bRegresa && formaMetodoPago.IdFormaDePago == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Forma de pago válida, verifique.");
                //btnPago.Focus();
            }

            //////if (bRegresa && formaMetodoPago.IdMetodoPago == "0")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha seleccionado un Método de pago válido, verifique.");
            //////    //cboMetodosDePago.Focus();
            //////    //btnPago.Focus();
            //////}

            if (bRegresa && !formaMetodoPago.ImportePagadoCompleto)
            {
                bRegresa = false;
                General.msjUser("La informacón de Métodos de pago esta incompleta, verifique.");
                btnPago.Focus();
            }

            if (bRegresa && bProductos_NoConfigurados_SAT)
            { 
                bRegresa = false;
                General.msjUser("Se detectaron algunos productos que no cumplen con los requisitos del SAT, verifique."); 
            }

            return bRegresa;
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
                    General.msjAviso("No fue posible generar la factura electrónica, intente de nuevo.");
                }
                else
                {
                    if (!VistaPrevia)
                    {
                        if (bFacturaElectronica_Generada)
                        {
                            DtIFacturacion.EsEnvioDeFactura_EMail = false;
                            if (ImprimirDocumento_Timbrado())
                            {
                                EnviarFactura_EMail(); 
                            }

                            ////this.Hide();
                            InicializarPantalla(); 
                        }
                    }
                }
            }
        }

        private void BloquearControles(bool Bloquear)
        {
            Bloquear = !Bloquear;
            btnNuevo.Enabled = Bloquear;
            btnFacturar.Enabled = Bloquear;
            btnValidarDatos.Enabled = Bloquear;
            cboUsosCFDI.Enabled = Bloquear;

            btnCliente.Enabled = Bloquear;
            cboSeries.Enabled = Bloquear;
            txtRFC.Enabled = Bloquear; 
            btnObservacionesGral.Enabled = Bloquear;
            btnPago.Enabled = Bloquear;
        }

        private void thFacturar()
        {

            // Control del Timer 
            bProcesando = true;

            tmProceso.Enabled = true;
            tmProceso.Start();

            BloquearControles(true);

            thFacturaElectronica = new Thread(Facturar);
            thFacturaElectronica.Name = "FacturaElectrónica";
            thFacturaElectronica.Start();
        }

        private void Facturar()
        {
            MostrarEnProceso(true);
            bFacturaElectronica_Generada = false;
            if (GenerarFolioFacturaElectronica())
            {
                ////PrepararFactura(); 
                if (RegistrarFacturaElectronica())
                {
                    ////PrepararFactura(); 
                    if (!GenerarTimbre())
                    {
                        bErrorAlGenerarFolio = true;
                    }
                }

                GenerarFolioFacturaElectronica_Terminar();
            }

            MostrarEnProceso(false);

            // Control del Timer 
            bProcesando = false;
        }

        private void TituloProceso(string Titulo)
        {
            FrameProceso.Text = Titulo;
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 130;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private bool RegistrarFacturaElectronica()
        {
            bool bRegresa = false;
            string sSql = "";
            string sStoreEnc = VistaPrevia ? " spp_Mtto_CFDI_Documentos_Generados_VP " : " spp_Mtto_CFDI_Documentos_Generados ";
            string sStoreDet = VistaPrevia ? " spp_Mtto_CFDI_Documentos_Generados_Detalles_VP " : " spp_Mtto_CFDI_Documentos_Generados_Detalles ";
            string sStoreDet_MetodoDePago = VistaPrevia ? " spp_Mtto_CFDI_Documentos_MetodosDePago_VP " : " spp_Mtto_CFDI_Documentos_MetodosDePago ";
            string sIdSerie = cboSeries.ItemActual.GetItem("IdentificadorSerie");
            clsLeer leerMetodosDePago = new clsLeer();

            string sSAT_Producto_Servicio = "";
            string sSAT_UnidadDeMedida = "";
            string sIdProducto = "";
            string sCodigoEAN = "";
            string Descripcion = "";
            double PrecioUnitario = 0;

            double DescuentoPorc = 0;
            double PrecioUnitarioFinal = 0;

            int Cantidad = 0;
            double TasaIva = 0;
            double SubTotal = 0;
            double Iva = 0;
            double Total = 0;
            string UnidadDeMedida = "";



            sSerieFactura = cboSeries.Data;

            sXMLFormaPago = formaMetodoPago.IdFormaDePago;
            sXMLCondicionesPago = formaMetodoPago.ObservacionesPago;
            sXMLMetodoPago = formaMetodoPago.MetodoPago;
            sXMLObservacionesPago = formaMetodoPago.Observaciones;
            leerMetodosDePago.DataSetClase = formaMetodoPago.ListaDeMetodosDePago;


            sRFC_Receptor = txtRFC.Text;
            sGUID = Guid.NewGuid().ToString();
            sSql = string.Format("Exec {0} " +
                " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdTipoDocumento = '{4}', " +
                " @IdentificadorSerie = '{5}', @Serie = '{6}', @NombreDocumento = '{7}', @Folio = '{8}', @Importe = '{9}', " +
                " @RFC = '{10}', @NombreReceptor = '{11}', " +
                " @IdCFDI = '{12}', @Status = '{13}', @IdPersonalCancela = '{14}', " +
                " @Observaciones_01 = '{15}', @Observaciones_02 = '{16}', @Observaciones_03 = '{17}', @Referencia = '{18}', " +
                " @XMLFormaPago = '{19}', @XMLCondicionesPago = '{20}', @XMLMetodoPago = '{21}', @IdPersonalEmite = '{22}', @GUID = '{23}', @UsoDeCFDI = '{24}'  ",
                sStoreEnc,
                sIdEmpresa, sIdEstado, sIdFarmacia, "001",
                sIdSerie, sSerieFactura, sNombreDocumento, sFolioFactura, fTotal.ToString(sFormato).Replace(",", ""),
                sRFC_Receptor, lblRazonSocial.Text.Trim(), sIdCFDI, "A", "",
                sObservaciones_01, sObservaciones_02, sObservaciones_03, sReferencia,
                sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago, DtGeneral.IdPersonal, sGUID, cboUsosCFDI.Data );

            if (!leerFacturar.Exec(sSql))
            {
                bErrorAlGenerarFolio = true;
                Error.GrabarError(leerFacturar, "RegistrarFacturaElectronica");
            }
            else
            {
                //bFacturaElectronica_Generada = true; 

                if (!leerFacturar.Leer())
                {
                    Error.GrabarError("Error al obtener el foliador del proceso", "RegistrarFacturaElectronica()");
                }
                else
                {
                    bRegresa = true;
                    PK = leerFacturar.CampoInt("FolioDocumento");

                    for (int Renglon = 1; Renglon <= list.Registros; Renglon++)
                    {
                        sSAT_Producto_Servicio = list.GetValue(Renglon, (int)Cols.SAT_ClaveProductoServicio);
                        sSAT_UnidadDeMedida = list.GetValue(Renglon, (int)Cols.SAT_UnidadDeMedida);  

                        sIdProducto = list.GetValue(Renglon, (int)Cols.IdProducto);
                        sCodigoEAN = list.GetValue(Renglon, (int)Cols.CodigoEAN);
                        Descripcion = list.GetValue(Renglon, (int)Cols.DescripcionComercial);
                        PrecioUnitario = list.GetValueDouble(Renglon, (int)Cols.PrecioUnitario);

                        DescuentoPorc = 100.00 - list.GetValueDouble(Renglon, (int)Cols.PorcentajeCobro);
                        PrecioUnitarioFinal = list.GetValueDouble(Renglon, (int)Cols.PrecioUnitarioFinal);

                        Cantidad = list.GetValueInt(Renglon, (int)Cols.Cantidad);
                        TasaIva = list.GetValueDouble(Renglon, (int)Cols.TasaIva);
                        SubTotal = list.GetValueDouble(Renglon, (int)Cols.SubTotal);
                        Iva = list.GetValueDouble(Renglon, (int)Cols.Iva);
                        Total = list.GetValueDouble(Renglon, (int)Cols.Importe);
                        UnidadDeMedida = list.GetValue(Renglon, (int)Cols.UnidadDeMedida);

                        sSql = string.Format("Exec {0} ", sStoreDet);
                        sSql += string.Format(
                            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = '{4}', " +
                            " @Partida = '{5}', @IdProducto = '{6}', @CodigoEAN = '{7}', @UnidadDeMedida = '{8}', @Cantidad = '{9}', " +
                            " @PrecioUnitario = '{10}', @DescuentoPorc = '{11}', @PrecioUnitarioFinal = '{12}', @TasaIva = '{13}', " +
                            " @SubTotal = '{14}', @Iva = '{15}', @Total = '{16}', @TipoImpuesto = '{17}', @GUID = '{18}', " +
                            " @SAT_ClaveProducto_Servicio = '{19}', @SAT_UnidadDeMedida = '{20}'  ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, sSerieFactura, sFolioFactura,
                            Renglon, sIdProducto, sCodigoEAN, UnidadDeMedida, Cantidad,
                            PrecioUnitario, DescuentoPorc, PrecioUnitarioFinal, TasaIva,
                            SubTotal, Iva, Total, "IVA", sGUID, sSAT_Producto_Servicio, sSAT_UnidadDeMedida);

                        if (!leerFacturar.Exec(sSql))
                        {
                            bFacturaElectronica_Generada = false;
                            bRegresa = false;

                            bErrorAlGenerarFolio = true;
                            Error.GrabarError(leerFacturar, "RegistrarFacturaElectronica");
                            break;
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
                               " @IdMetodoDePago = '{5}', @Importe = '{6}', @Referencia = '{7}', @GUID = '{8}' ",
                               sIdEmpresa, sIdEstado, sIdFarmacia, sSerieFactura, sFolioFactura,
                               leerMetodosDePago.Campo("IdFormaDePago"), leerMetodosDePago.CampoDouble("Importe"), leerMetodosDePago.Campo("Referencia"), sGUID);

                                if (!leerFacturar.Exec(sSql))
                                {
                                    bFacturaElectronica_Generada = false;
                                    bRegresa = false;

                                    bErrorAlGenerarFolio = true;
                                    Error.GrabarError(leerFacturar, "RegistrarFacturaElectronica");
                                    break;
                                }
                            }
                        }
                    }
                }

            }

            if (bRegresa)
            {
                if (!VistaPrevia)
                {
                    bRegresa = GuardarDatosFactura();
                }
            }

            return bRegresa;
        }

        private bool GuardarDatosFactura()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_INT_MA__FACT_Facturar_Venta " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', " + 
                " @Serie = '{4}', @FolioFacturaElectronica = '{5}', @TipoFacturacion = '{6}'  ",
                sIdEmpresa, sIdEstado, sIdFarmacia, txtFolio.Text.Trim(), sSerieFactura, sFolioFactura, (int)tpVenta);
            if (!leerFacturar.Exec(sSql))
            {
                bFacturaElectronica_Generada = false;
                bRegresa = false;

                bErrorAlGenerarFolio = true;
                ////sFuncionError = "GuardarDatosFactura()";
                Error.GrabarError(leerFacturar, "GuardarDatosFactura()");
            }
            else
            {
                ////if (leer.Leer())
                ////{
                ////    bRegresa = true;
                ////}
            }

            return bRegresa;
        }

        private bool GenerarFolioFacturaElectronica()
        {
            bool bRegresa = false;  // sFolioFactura 
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
                    sFolioFactura = sFolio;

                    sSql = string.Format("Delete From FACT_CFD_Documentos_Generados_Detalles_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    sSql += string.Format("Delete From FACT_CFD_Documentos_Generados_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    leerFacturar.Exec(sSql);
                }
                else
                {
                    sSql = string.Format("Exec spp_CFDI_GetFolio  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}' ",
                        sIdEmpresa, sIdEstado, sIdFarmacia, cboSeries.Data);

                    if (!leerFacturar.Exec(sSql))
                    {
                        bErrorAlGenerarFolio = true;
                        Error.GrabarError(leerFacturar, "GenerarFolioFacturaElectronica");
                    }
                    else
                    {
                        if (!leerFacturar.Leer())
                        {
                            bErrorAlGenerarFolio = true;
                            Error.GrabarError(leerFacturar, "GenerarFolioFacturaElectronica");
                        }
                        else
                        {
                            bRegresa = true;
                            sFolioFactura = leerFacturar.CampoInt("FolioGenerado").ToString();
                            PK = Convert.ToInt32("0" + sFolioFactura);
                        }
                    }
                }

                if (bErrorAlGenerarFolio)
                {
                    GenerarFolioFacturaElectronica_Terminar();
                    General.msjError("Ocurrió un error al obtener el Folio de factura electrónica.");
                }
            }

            return bRegresa;
        }

        private void GenerarFolioFacturaElectronica_Terminar()
        {
            /////// Aplicar el foliador 
            if (bErrorAlGenerarFolio)
            {
                cnnFacturar.DeshacerTransaccion();
            }
            else
            {
                cnnFacturar.CompletarTransaccion();
            }
        }

        private bool GenerarTimbre()
        {
            bool bRegresa = false;

            switch (tpPAC)
            {
                case PACs_Timbrado.Tralix:
                    bRegresa = false;
                    break;

                default:
                    sTipoDeDocumento = cboSeries.ItemActual.GetItem("TipoDeDocumento");
                    CFDI.Timbrar.clsTimbrar.TipoDeDocumento = sTipoDeDocumento;
                    bRegresa = GenerarTimbre_PAC();
                    break;
            }

            bFacturaElectronica_Generada = bRegresa;
            return bRegresa;
        }

        private bool GenerarTimbre_PAC()
        {
            bool bRegresa = false;
            long pk = PK;
            long pkResult = 0;
            string sImpresion = VistaPrevia ? " spp_CFDI_ObtenerDatos_VP " : " spp_CFDI_ObtenerDatos ";

            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI);

            //if (!bRegresa)
            {
                CFDI_FD = new clsGenCFDI_33_Factura();
                CFDI_FD.Conexion = cnnFacturar;
                CFDI_FD.GUID = sGUID;
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

        private ArrayList GetExtras(DataSet CamposAdicionales)
        {
            ArrayList Datos = new ArrayList();
            DatosExtraImpresion dato = new DatosExtraImpresion();
            clsLeer datosAdicionales = new clsLeer();
            datosAdicionales.DataSetClase = CamposAdicionales;
            datosAdicionales.Leer();

            dato = new DatosExtraImpresion();
            dato.Nombre = "StatusDocumento";
            dato.Valor = datosAdicionales.Campo("StatusDocumento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_01";
            dato.Valor = datosAdicionales.Campo("Observaciones_01");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_02";
            dato.Valor = datosAdicionales.Campo("Observaciones_02");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_03";
            dato.Valor = datosAdicionales.Campo("Observaciones_03");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia";
            dato.Valor = datosAdicionales.Campo("Referencia");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeDocumento";
            dato.Valor = datosAdicionales.Campo("TipoDocumentoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeInsumo";
            dato.Valor = datosAdicionales.Campo("TipoInsumoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "RubroFinanciamiento";
            dato.Valor = datosAdicionales.Campo("RubroFinanciamento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FuenteFinanciamiento";
            dato.Valor = datosAdicionales.Campo("Financiamiento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "CantidadLetra";
            dato.Valor = General.LetraMoneda(datosAdicionales.CampoDouble("Total"));
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FechaCancelacionCFDI";
            dato.Valor = datosAdicionales.Campo("FechaCancelacionDocumento");
            Datos.Add(dato);

            return Datos;
        }

        private bool ImprimirDocumento_Timbrado()
        {
            return ImprimirDocumento_Timbrado(true); 
        }

        private bool ImprimirDocumento_Timbrado(bool MostrarEnPantalla)
        {
            bool bRegresa = false;
            bool bVisualizar = MostrarEnPantalla;
            string sImpresion = VistaPrevia ? " spp_CFDI_GetListaComprobantes_VP " : " spp_CFDI_GetListaComprobantes ";
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
                    sFileName_Cliente = sFileName; 
                    sXML = leer.Campo("uf_Xml_Timbrado");
                    sPDF = leer.Campo("uf_xml_Impresion");
                    sEmail = leer.Campo("EmailCliente");

                    bRegresa = true;

                    DtIFacturacion.InvocaVisor = this;
                    DtIFacturacion.GenerarImpresionWebBrowser(VistaPrevia, sFileName, true, GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null);
                }
            }

            return bRegresa;
        }
        #endregion Facturacion Electronica

        private void btnAbrirDirectorio_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaTmpImpresion); 
        }
    }
}
