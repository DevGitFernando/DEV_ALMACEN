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
using Dll_MA_IFacturacion.Configuracion; 
#endregion USING

#region USING WEB SERVICES
using Dll_MA_IFacturacion.xsasvrCancelCFDService;
using Dll_MA_IFacturacion.xsasvrCFDService;
using Dll_MA_IFacturacion.xsasvrFileReceiverService;
#endregion USING WEB SERVICES 

using Dll_MA_IFacturacion.CFDI;
using Dll_MA_IFacturacion.CFDI.CFDFunctions;
using Dll_MA_IFacturacion.CFDI.CFDFunctionsEx;
using Dll_MA_IFacturacion.CFDI.geCFD;

using Dll_MA_IFacturacion.Catalogos; 

namespace Dll_MA_IFacturacion.XSA
{
    public partial class FrmFacturarRemision : FrmBaseExt
    {
        #region Declaracion de Variables 
        ////enum Cols
        ////{
        ////    IdClave = 1, ClaveSSA = 2, Descripcion = 3, PrecioUnitario = 4, Cantidad = 5, 
        ////    TasaIva = 6, SubTotal = 7, Iva = 8, Total = 9, UnidadDeMedida = 10 
        ////}

        enum Cols
        {
            Ninguna = 0,
            IdProducto = 1,
            SAT_ClaveProductoServicio, SAT_UnidadDeMedida, 
            CodigoEAN, DescripcionComercial, PrecioUnitario, PorcentajeCobro, PrecioUnitarioFinal,
            Cantidad, TasaIva, SubTotal, Iva, Importe, UnidadDeMedida
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leerFacturar; 
        clsListView list;
        clsAyudas_CFDI ayuda;
        clsConsultas_CFDI consulta;

        clsLeer Empresa = new clsLeer();
        clsLeer Sucursal = new clsLeer();
        clsLeer Domicilio = new clsLeer();
        clsLeer DatosReceptor = new clsLeer();
        clsFormaMetodoPago formaMetodoPago;
        DataSet dtsListaImpuestos = new DataSet();

        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        private bool bProcesando = false; 
        //private bool bFacturaGenerada = false;
        private string sSerieFactura = "";
        private string sFolioFactura = "";
        private string sIdCFDI = "";
        private string sNombreReceptor = "";

        string sTipoDeDocumento = ""; 
        private string sIdEmpresa = "";
        private string sIdEstado = "";
        private string sIdFarmacia = "";
        private string sIdPersonal = ""; 
        private string sFolioRemision = "";
        private string sIdEstadoReferencia = "";
        private string sIdFarmaciaReferencia = "";
        private string sFarmaciaReferencia = "";
        private eTipoRemision tpTipoDeRemision = eTipoRemision.Ninguno;
        private int iInformacionDetallada = 1;
        private bool bRemisionConcentrada = false; 
        private bool bExisteDescripcionConcentrada = false; 
        private bool bInformacionEmisor = false;
        private int iAltoListView = 0; 

        Thread thRemision;
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
        string sTipoDoctoFactura = "001";

        string sGUID = ""; 

        bool bErrorAlGenerarFolio = false;
        bool bFacturaElectronica_Generada = false;

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";

        string sFolioFacturaElectronica = "";
        double dSubTotalSinGrabar_Remision = 0;
        double dSubTotalGrabado_Remision = 0;
        double dIva_Remision = 0;
        double dTotal_Remision = 0;        


        double dSubTotal = 0;
        double dTasaIva = 0; 
        double dIva = 0;
        double dTotal = 0;
        string sObservacionesDocumento = "";
        string sObservacionesCondicionesDePago = "";
        string sObservacionesPago = "";

        bool bVistaPrevia = false;
        long PK = 0;
        string sFolioFactura_Interna = "";
        string sObservaciones_Factura = "";
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
        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_MA_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_33_Factura CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_Factura myCFDI = null; //new clsGenCFDI_Ex();

        #endregion Declaracion de Variables de Facturacion

        public FrmFacturarRemision(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioRemision, eTipoRemision TipoDeRemision,
            string Observaciones_Factura, 
            double SubTotalSinGrabar_Remision, double SubTotalGrabado_Remision, double Iva_Remision, double Total_Remision, string IdEstadoReferencia, string IdFarmaciaReferencia, string FarmaciaReferencia) 
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            dSubTotalSinGrabar_Remision = SubTotalSinGrabar_Remision;
            dSubTotalGrabado_Remision = SubTotalGrabado_Remision;
            dIva_Remision = Iva_Remision;
            dTotal_Remision = Total_Remision;
            sObservaciones_Factura = Observaciones_Factura; 

            this.sIdEmpresa = IdEmpresa;
            this.sIdEstado = IdEstado;
            this.sIdFarmacia = IdFarmacia;
            this.sFolioRemision = FolioRemision;
            this.tpTipoDeRemision = TipoDeRemision;
            sIdEstadoReferencia = IdEstadoReferencia;
            sIdFarmaciaReferencia = IdFarmaciaReferencia;
            sFarmaciaReferencia = FarmaciaReferencia;

            this.Text += " : " + string.Format("{0} -- {1} ", sFolioRemision, TipoDeRemision.ToString().ToUpper()); 
            
            // Inicializar el CFDI 
            cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia); 


            // this.Top = 212; 
            this.FrameDetalles.Height = 400;
            this.Height = FrameDetalles.Top + FrameDetalles.Height + 40; 
            this.FrameProceso.Top = 220; 
            this.FrameProceso.Top = (this.FrameDetalles.Top + ( this.FrameDetalles.Height / 2));
            this.FrameProceso.Top -= (this.FrameProceso.Height / 2); 
            iAltoListView = listvClaves.Height; 


            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            formaMetodoPago = new clsFormaMetodoPago(); 

            list = new clsListView(listvClaves);

            tpPAC = DtIFacturacion.PAC_Informacion.PAC; 

            // Anexar al documento la Remisión relacionada 
            sReferencia = "R" + sFolioRemision; 
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

        public bool FacturaGenerada
        {
            get { return bFacturaElectronica_Generada; }
        }

        public bool VistaPrevia
        {
            get { return bVistaPrevia; }
        }

        public string Serie
        {
            get { return sSerieFactura; }
        }

        public string Folio
        {
            get { return sFolioFactura; }
        }

        public string FolioFacturaElectronica 
        {
            get { return (sSerieFactura + " - " + sFolioFactura); }
        }

        public string FolioFactura_Interna
        {
            get { return sFolioFactura_Interna; }
        }
        #endregion Propiedades 

        #region Form
        private void FrmFacturarRemision_Load(object sender, EventArgs e)
        {
            list.LimpiarItems(); 
            InicializaPantalla(); 
            ObtenerSeries_y_Folios();
            ObtenerInformacion_Empresa_y_Sucursal();
            CargarFormas_Metodos_DePago(); 
        }
        #endregion Form

        #region Botones 
        private void InicializaToolBar(bool Remision, bool Facturar)
        {
            InicializaToolBar(true, Remision, Facturar); 
        }

        private void InicializaToolBar(bool Nuevo, bool Remision, bool Facturar)
        {
            btnNuevo.Enabled = Nuevo;
            btnObtenerDetalles.Enabled = Remision;
            btnFacturar.Enabled = Facturar; 
        }

        private void InicializaPantalla()
        {
            lblTimbresDisponibles.Text = ""; 
            iInformacionDetallada = 1;
            bRemisionConcentrada = false;
            bExisteDescripcionConcentrada = false;

            dTotal = 0;
            formaMetodoPago = new clsFormaMetodoPago();

            FrameConceptoEspecial.Enabled = false;
            chkConceptoEncabezado.Enabled = false;
            txtIdConceptoEncabezado.Enabled = false; 

            list.LimpiarItems();
            AjustarListView(bRemisionConcentrada); 

            tmProceso.Enabled = false;
            tmProceso.Interval = 500; 
            Fg.IniciaControles();
            InicializaToolBar(true, false); 
            MostrarEnProceso(false);

            bFacturaElectronica_Generada = false;
            bErrorAlGenerarFolio = false; 

            sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
            sXMLCondicionesPago = "Crédito";
            sXMLMetodoPago = "No identificado";
            btnValidarDatos.Visible = DtIFacturacion.PAC_Informacion.PAC != PACs_Timbrado.Tralix;
            btnValidarDatos.Enabled = btnValidarDatos.Visible;
            toolStripSeparatorVistaPrevia.Visible = btnValidarDatos.Visible; 

            if (ResetObservaciones())
            {
                sObservacionesDocumento = "";
                sObservacionesCondicionesDePago = sObservacionesDocumento;
                sObservacionesPago = "";

                sObservaciones_01 = "";
                sObservaciones_02 = "";
                sObservaciones_03 = "";
                sReferencia = "";
                formaMetodoPago = new clsFormaMetodoPago();
            }

            chkConceptoEncabezado.Checked = false;
            txtIdConceptoEncabezado.Enabled = false; 

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
                    sObservaciones_01 != "" || sObservaciones_02 != "" || sObservaciones_03 != "" || sReferencia != "")
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

        private void AjustarListView(bool Concentrado)
        {
            listvClaves.Height = iAltoListView; 
            listvClaves.ContextMenuStrip = null;
            lblMensajes.Visible = false; 

            if (Concentrado)
            {
                listvClaves.Height = iAltoListView - lblMensajes.Height - 0;
                listvClaves.ContextMenuStrip = mnAgregarDescripcion;
                lblMensajes.Visible = true; 
            }

            this.Refresh();
            System.Threading.Thread.Sleep(100); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnObtenerDetalles_Click(object sender, EventArgs e)
        {
            if (validarRemision())
            {
                thObtenerDetallesDeRemision(); 
            }
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            bVistaPrevia = false; 
            if (validarGenerarFactura())
            {
                thFacturar(); 
            }
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            bVistaPrevia = true;
            if (validarGenerarFactura())
            {
                thFacturar();
            }
        }

        private void btnConsultarTimbres_Click(object sender, EventArgs e)
        {
            btnConsultarTimbres.Enabled = false;
            Application.DoEvents();

            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.Preparar_ConexionTimbrado(DtIFacturacion.EmisorRFC, DtIFacturacion.PAC_Informacion); 
            lblTimbresDisponibles.Text = string.Format("Timbres disponibles  {0}", Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.ConsultarCreditos()); 

            Application.DoEvents(); 
            btnConsultarTimbres.Enabled = true; 
        }

        private void btnObservacionesGral_Click(object sender, EventArgs e)
        {
            FrmObservaciones f = new FrmObservaciones();

            f.sObservaciones_01 = sObservaciones_01;
            f.sObservaciones_02 = sObservaciones_02;
            f.sObservaciones_03 = sObservaciones_03;
            f.sReferencia = sReferencia; 
            f.ShowDialog();

            sObservaciones_01 = f.sObservaciones_01;
            sObservaciones_02 = f.sObservaciones_02;
            sObservaciones_03 = f.sObservaciones_03;
            sReferencia = f.sReferencia; 
        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            if (dTotal == 0)
            {
                General.msjUser("No se ha cargado la información para la generación del documento electrónico, verifique."); 
            }
            else 
            {
                formaMetodoPago.ImporteACobrar = dTotal;
                formaMetodoPago.Show();
            }
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
            FrmClientes f = new FrmClientes(); 
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
        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add();
            cboSeries.Add(consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoFactura, "ObtenerSeries_y_Folios()"), true, "Serie", "Serie");  
            cboSeries.SelectedIndex = 0; 
        }
        #endregion Series y Folios

        #region Validaciones 
        private bool validarRemision()
        {
            bool bRegresa = true;
            iInformacionDetallada = 1;
            bRemisionConcentrada = false;
            bExisteDescripcionConcentrada = false; 

            if (tpTipoDeRemision == eTipoRemision.Administracion) 
            {
                if (DtIFacturacion.DocumentoAdministracion == eDocumentoAdministracion.Ninguno) 
                {
                    bRegresa = false;
                    General.msjAviso("No se ha determinado el tipo de Documento de Administración."); 
                }

                if (bRegresa)
                {
                    if (DtIFacturacion.DocumentoAdministracion == eDocumentoAdministracion.Concentrado)
                    {
                        if (General.msjConfirmar("¿ Desea cagar la información de la remisión de forma concentrada ?", "Información de remisión") == DialogResult.Yes)
                        {
                            iInformacionDetallada = 0;
                            bRemisionConcentrada = true;
                        }
                    }
                }
            }

            if (bRegresa) 
            {
                if (General.msjConfirmar("El proceso de obtención de detalles de remisión puede demorar unos minutos.\n\n¿ Desea continuar ?") == DialogResult.No) 
                {
                    bRegresa = false; 
                }
            }

            AjustarListView(bRemisionConcentrada); 
            return bRegresa; 
        }

        private bool validarGenerarFactura()
        {
            bool bRegresa = true;
            sNombreReceptor = txtClienteNombre.Text;

            ////if (!bInformacionEmisor)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No se cuenta con la información de la sucursal, no es posible generar la factura sin estos datos."); 
            ////}

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

            if (bRegresa && list.Registros == 0)
            {
                bRegresa = false;
                General.msjUser("No existen detalles de remisión para facturar, verifique."); 
            }

            if (bRegresa && iInformacionDetallada == 0)
            {
                if ( !bExisteDescripcionConcentrada ) 
                {
                    bRegresa = false;
                    General.msjUser("No ha capturado la descripción para la factura electrónica, verifique."); 
                } 
            }

            if (bRegresa && formaMetodoPago.IdFormaDePago == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Forma de pago válida, verifique."); 
                //cboFormasDePago.Focus(); 
                btnPago.Focus(); 
            }

            //////if (bRegresa && formaMetodoPago.IdMetodoPago == "0" )
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha seleccionado un Método de pago válido, verifique.");
            //////    //cboMetodosDePago.Focus();
            //////    btnPago.Focus(); 
            //////}

            if (bRegresa && !formaMetodoPago.ImportePagadoCompleto)
            {
                bRegresa = false;
                General.msjUser("La informacón de Métodos de pago esta incompleta, verifique.");
                btnPago.Focus();
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
                FrameProceso.Left = 202;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private void thObtenerDetallesDeRemision()
        {
            InicializaToolBar(false, false, false);
            Thread.Sleep(500); 

            thRemision = new Thread(ObtenerDetallesDeRemision);
            thRemision.Name = "ObtenerRemision";
            thRemision.Start(); 
        }

        private void ObtenerDetallesDeRemision()
        {
            ObtenerDetallesDeRemision_Insumo(iInformacionDetallada); 

            /* 
            if (tpTipoDeRemision == eTipoRemision.Insumo)
            {
                ObtenerDetallesDeRemision_Insumo(iInformacionDetallada);
            }
            else 
            {
                switch (DtIFacturacion.DocumentoAdministracion)
                {
                    case eDocumentoAdministracion.Concentrado:
                        ObtenerDetallesDeRemision_Administracion_Concentrada(); 
                        break; 

                    case eDocumentoAdministracion.Detallado:
                        ObtenerDetallesDeRemision_Administracion_Detallada(); 
                        break;
                    default:
                        break;
                }
            } 
            */  
        }

        private void ObtenerDetallesDeRemision_Administracion_Concentrada()
        {
            ObtenerDetallesDeRemision_Insumo(0);
        }

        private void ObtenerDetallesDeRemision_Administracion_Detallada()
        {
            ObtenerDetallesDeRemision_Insumo(1); 
        }

        private void ObtenerDetallesDeRemision_Insumo(int Detallado)
        {
            bool bFacturar = false;
            string sSql = string.Format("Exec spp_INT_MA__FACT_GetInformacion_Remision " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}', @Detallado = '{4}' ",
                sIdEmpresa, sIdEstado, sIdFarmacia, sFolioRemision, Detallado); 

            dSubTotal = 0;
            dIva = 0;
            dTotal = 0;

            list.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerDetallesDeRemision()");
                General.msjError("Ocurrió un error al obtener la información de la Remisión."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontrarón detalles para la Remisión solicitada."); 
                }
                else
                {
                    bFacturar = true;  
                    list.CargarDatos(leer.DataSetClase);
                    dSubTotal = list.TotalizarColumnaDouble((int)Cols.SubTotal);
                    dIva = list.TotalizarColumnaDouble((int)Cols.Iva);
                    dTotal = list.TotalizarColumnaDouble((int)Cols.Importe); 
                } 
            }

            list.AnchoColumna((int)Cols.IdProducto, 80);
            list.AnchoColumna((int)Cols.CodigoEAN, 100);
            list.AnchoColumna((int)Cols.DescripcionComercial, 300);
            list.AnchoColumna((int)Cols.PrecioUnitario, 80);
            list.AnchoColumna((int)Cols.PorcentajeCobro, 80);
            list.AnchoColumna((int)Cols.PrecioUnitarioFinal, 80);


            list.AnchoColumna((int)Cols.Cantidad, 70);
            list.AnchoColumna((int)Cols.TasaIva, 60);
            list.AnchoColumna((int)Cols.SubTotal, 90);
            list.AnchoColumna((int)Cols.Iva, 60);
            list.AnchoColumna((int)Cols.Importe, 90);
            list.AnchoColumna((int)Cols.UnidadDeMedida, 120); 

            // Final de Proceso 
            InicializaToolBar(true, !bFacturar, bFacturar);         
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos
        private void cboSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUltimoFolio.Text = "";
            if (cboSeries.SelectedIndex != 0)
            {
                lblUltimoFolio.Text = cboSeries.ItemActual.GetItem("FolioUtilizado"); 
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
                leer.DataSetClase = consulta.CFDI_Clientes(txtId.Text, false, "txtId_Validating"); 
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
                lblRFC.Text = leer.Campo("RFC");
                lblCliente.Text = leer.Campo("Nombre");
                txtClienteNombre.Text = leer.Campo("Nombre");
                DatosReceptor.DataSetClase = leer.DataSetClase;
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
                    General.msjAviso("No fue posible generar la factura electrónica, intente de nuevo.");
                }
                else
                {
                    //if ( !VistaPrevia )
                    {
                        if (bFacturaElectronica_Generada)
                        {
                            ImprimirDocumento_Timbrado();
                            if (!bVistaPrevia)
                            {
                                this.Hide();
                            }
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
            btnObtenerDetalles.Enabled = Bloquear;
            btnFacturar.Enabled = Bloquear;

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

        private double getNumber(string s)
        {
            try
            {
                if ((s.Trim() == "") || (s.Trim() == "."))
                {
                    return 0.0;
                }
                string str = "";
                foreach (char ch in s)
                {
                    str = str + ("0123456789.".Contains(ch.ToString()) ? ch.ToString() : "");
                }

                return Convert.ToDouble(str);
            }
            catch (Exception)
            {
                return 0.0;
            }
        }

        private bool RegistrarFacturaElectronica()
        {
            bool bRegresa = false;
            clsLeer leerMetodosDePago = new clsLeer();

            string sSql = "";
            string sStoreEnc = VistaPrevia ? " spp_Mtto_CFDI_Documentos_Generados_VP " : " spp_Mtto_CFDI_Documentos_Generados ";
            string sStoreDet = VistaPrevia ? " spp_Mtto_CFDI_Documentos_Generados_Detalles_VP " : " spp_Mtto_CFDI_Documentos_Generados_Detalles ";
            string sStoreDet_MetodoDePago = VistaPrevia ? " spp_Mtto_CFDI_Documentos_MetodosDePago_VP " : " spp_Mtto_CFDI_Documentos_MetodosDePago ";
            string sIdSerie = cboSeries.ItemActual.GetItem("IdentificadorSerie");

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


            sRFC_Receptor = lblRFC.Text; 
            sGUID = Guid.NewGuid().ToString(); 
            sSql = string.Format("Exec {0} " +
                " @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdTipoDocumento = '{4}', " +
                " @IdentificadorSerie = '{5}', @Serie = '{6}', @NombreDocumento = '{7}', @Folio = '{8}', @Importe = '{9}', " +
                " @RFC = '{10}', @NombreReceptor = '{11}', " +
                " @IdCFDI = '{12}', @Status = '{13}', @IdPersonalCancela = '{14}', " +
                " @Observaciones_01 = '{15}', @Observaciones_02 = '{16}', @Observaciones_03 = '{17}', @Referencia = '{18}', " +
                " @XMLFormaPago = '{19}', @XMLCondicionesPago = '{20}', @XMLMetodoPago = '{21}', @IdPersonalEmite = '{22}', @GUID = '{23}', " +
                " @IdFarmaciaReferencia = '{24}',  @IdEstadoReferencia = '{25}', @UsoDeCFDI = '{26}'    ",
                sStoreEnc,
                sIdEmpresa, sIdEstado, sIdFarmacia, "001",
                sIdSerie, sSerieFactura, sNombreDocumento, sFolioFactura, dTotal.ToString(sFormato).Replace(",", ""),
                sRFC_Receptor, txtClienteNombre.Text.Trim(), sIdCFDI, "A", "",
                sObservaciones_01, sObservaciones_02, sObservaciones_03, sReferencia,
                sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago, DtGeneral.IdPersonal, sGUID, sIdFarmaciaReferencia, sIdEstadoReferencia,
                DtIFacturacion.Factura_UsoCDFI 
                );

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
                        sSAT_Producto_Servicio = list.GetValue(Renglon, (int)Cols.SAT_ClaveProductoServicio);
                        sSAT_UnidadDeMedida = list.GetValue(Renglon, (int)Cols.SAT_UnidadDeMedida);

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
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format("Exec spp_INT_MA__FACT_Facturar_Remisiones " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioFactura = '{3}', @TipoDeFactura = '{4}', @FolioRemision = '{5}', " +
                " @Serie = '{6}', @FolioFacturaElectronica = '{7}', @IdPersonalFactura = '{8}', " + 
                " @SubTotalSinGrabar = '{9}', @SubTotalGrabado = '{10}', @Iva = '{11}', @Total = '{12}', @Observaciones = '{13}', @Status = '{14}'  ",
                sIdEmpresa, sIdEstado, sIdFarmacia, "*", (int)tpTipoDeRemision, sFolioRemision,
                sSerieFactura, sFolioFactura, DtGeneral.IdPersonal, dSubTotalSinGrabar_Remision, dSubTotalGrabado_Remision, dIva_Remision, dTotal_Remision, 
                sObservaciones_Factura, 'A');
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
                if (leerFacturar.Leer())
                {
                    bRegresa = true;
                    sFolioFactura_Interna = leerFacturar.Campo("Folio");
                }
            }

            return bRegresa;
        }

        private bool ObtenerImpuestos()
        {
            bool bRegresa = true;
            ////////string sTabla = VistaPrevia ? " FACT_CFD_Documentos_Generados_Detalles_VP " : " FACT_CFD_Documentos_Generados_Detalles "; 
            ////////string sSql = string.Format(
            ////////    "Select TipoImpuesto, TasaIva, sum(Iva) as Importe " +
            ////////    "From {0} D (NoLock) " +
            ////////    "Where IdEmpresa = '{1}' and IdEstado = '{2}' and IdFarmacia = '{3}' and Serie = '{4}' and Folio = '{5}' " +
            ////////    "Group by TipoImpuesto, TasaIva", sTabla, sIdEmpresa, sIdEstado, sIdFarmacia, sSerieFactura, sFolioFactura);

            ////////if (!leerFacturar.Exec(sSql))
            ////////{
            ////////    bFacturaElectronica_Generada = false;
            ////////    bRegresa = false;

            ////////    bErrorAlGenerarFolio = true;
            ////////    Error.GrabarError(leerFacturar, "RegistrarDocumentoElectronico");
            ////////}
            ////////else
            ////////{
            ////////    dtsListaImpuestos = leerFacturar.DataSetClase;
            ////////}

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

                    sSql = string.Format("Delete From CFDI_Documentos_VP_Conceptos " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    sSql += string.Format("Delete From CFDI_Documentos_VP " +
                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' \n",
                        sIdEmpresa, sIdEstado, sIdFarmacia, sSerie, sFolio);
                    leer.Exec(sSql);
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
                            ////if (leerFacturar.CampoInt("FolioGenerado") > leerFacturar.CampoInt("FolioFinal"))
                            ////{
                            ////    bErrorAlGenerarFolio = true;
                            ////    Error.GrabarError(string.Format("Se agotaron los folios disponibles de la Serie {0} ", cboSeries.Data),
                            ////        "GenerarFolioFacturaElectronica");
                            ////}
                            ////else
                            {
                                bRegresa = true;
                                sFolioFactura = leerFacturar.CampoInt("FolioGenerado").ToString();
                                PK = Convert.ToInt32("0" + sFolioFactura);
                            }
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
                
                //////// Asegurar que se descargaron los documentos 
                //if (DescargarDocumentos())
                //{
                //    GuardarDocumentos_XML_PDF();
                //}
            }
        }

        private bool GuardarDocumentos_XML_PDF()
        {
            bool bRegresa = true; 
            ////string sDocumento_XML = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, cfdi.Documento_XML));
            ////string sDocumento_PDF = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, cfdi.Documento_PDF));
            ////string sSql = string.Format("Exec spp_Mtto_FACT_CFD_Documentos_Generados_Formatos  @IdCFDI = '{0}', @XML = '{1}', @PDF = '{2}' ",
            ////    sIdCFDI, sDocumento_XML, sDocumento_PDF);

            ////if (!leer.Exec(sSql))
            ////{
            ////    bRegresa = false;
            ////}

            return bRegresa;
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

        ////////private void PrepararFactura()
        ////////{
        ////////    DateTime dtFechaHora = General.FechaSistemaObtener().AddMinutes(-3);

        ////////    // Inicializar el CFDI 
        ////////    cfdi = new xsaCFDI(IdEmpresa, IdEstado, IdFarmacia); 

        ////////    sSerieFactura = cboSeries.Data;
        ////////    // sFolioFactura = sFolioFactura;

        ////////    Encabezado(dtFechaHora);
        ////////    EncabezadoExt(dtFechaHora);
        ////////    DatosDeReceptor();
        ////////    DatosDePago();
        ////////    DatosDeEmbarque();
        ////////    Conceptos();
        ////////    ImpuestosTrasladados();
        ////////    ImpuestosRetenidos();
        ////////    Aduana();
        ////////    EnvioDocumentos();

        ////////    sIdCFDI = cfdi.Encabezado.Identificador_CFDI;
        ////////    sNombreDocumento = sIdCFDI + ".txt"; 
        ////////    sPlantillaDocumento =  cfdi.GenerarPlantilla(); 
        ////////}

        ////////private void Encabezado(DateTime FechaHora)
        ////////{
        ////////    cfdi.TipoDocumento = tipoDocumento; 
        ////////    cfdi.Encabezado.Serie = sSerieFactura;
        ////////    cfdi.Encabezado.Folio = sFolioFactura;
        ////////    cfdi.Encabezado.FechaHora = FechaHora;
        ////////    cfdi.Encabezado.SubTotal = dSubTotal;
        ////////    cfdi.Encabezado.Total = dTotal;
        ////////    cfdi.Encabezado.Moneda = cfdMoneda.Ninguno;
        ////////    cfdi.Encabezado.Observaciones_01 = sObservaciones_01;
        ////////    cfdi.Encabezado.Observaciones_02 = sObservaciones_02;
        ////////    cfdi.Encabezado.Observaciones_03 = sObservaciones_03;
        ////////    cfdi.Encabezado.Referencia = sReferencia; 
        ////////}

        ////////private void EncabezadoExt(DateTime FechaHora)
        ////////{
        ////////    Domicilio.RegistroActual = 1; 
        ////////    Domicilio.Leer();
        ////////    cfdi.EncabezadoAdicional.NumeroCtaPago = "";
        ////////    cfdi.EncabezadoAdicional.Serie = sSerieFactura;
        ////////    cfdi.EncabezadoAdicional.Folio = sFolioFactura;
        ////////    cfdi.EncabezadoAdicional.FechaHora = FechaHora;
        ////////    cfdi.EncabezadoAdicional.Total = dTotal;
        ////////    cfdi.EncabezadoAdicional.ExpedidoEn = Domicilio.Campo("Estado") + ", " + Domicilio.Campo("Municipio"); 

        ////////}

        ////////private void DatosDeReceptor()
        ////////{
        ////////    DatosReceptor.Leer();
        ////////    DatosReceptor.RegistroActual = 1;
        ////////    sRFC_Receptor = DatosReceptor.Campo("RFC"); 
        ////////    cfdi.Receptor.IdReceptor = DatosReceptor.Campo("IdCliente");
        ////////    cfdi.Receptor.RFC = DatosReceptor.Campo("RFC");
        ////////    cfdi.Receptor.Nombre = DatosReceptor.Campo("Nombre");
        ////////    cfdi.Receptor.Nombre = sNombreReceptor;  //// Requerimiento de Oaxaca, modificar el Nombre relacioado al RFC 
             
        ////////    cfdi.Receptor.Pais = DatosReceptor.Campo("Pais");
        ////////    cfdi.Receptor.Estado = DatosReceptor.Campo("Estado");
        ////////    cfdi.Receptor.Municipio = DatosReceptor.Campo("Municipio");
        ////////    cfdi.Receptor.Localidad = DatosReceptor.Campo("Localidad");
        ////////    cfdi.Receptor.Colonia = DatosReceptor.Campo("Colonia");
        ////////    cfdi.Receptor.Calle = DatosReceptor.Campo("Calle");
        ////////    cfdi.Receptor.NumExterior = DatosReceptor.Campo("NoExterior");
        ////////    cfdi.Receptor.NumInterior = DatosReceptor.Campo("NoInterior");
        ////////    cfdi.Receptor.CodigoPostal = DatosReceptor.Campo("CodigoPostal");
        ////////    cfdi.Receptor.Referencia = DatosReceptor.Campo("Referencia"); 
        ////////}

        ////////private void DatosDePago()
        ////////{
        ////////    ////cfdi.Pago.FormaDePago = cboFormasDePago.Data;
        ////////    ////cfdi.Pago.CondicionesDePago = sObservacionesCondicionesDePago;
        ////////    ////cfdi.Pago.MetodoDePago = cboMetodosDePago.Data;
        ////////    ////cfdi.Pago.Observaciones = sObservacionesPago;

        ////////    ////if (chkVencimiento.Checked)
        ////////    ////{
        ////////    ////    cfdi.Pago.FechaDeVencimiento = General.FechaYMD(dtpFechaVencimiento.Value); 
        ////////    ////}

        ////////    cfdi.Pago.FormaDePago = formaMetodoPago.FormaDePago;
        ////////    cfdi.Pago.CondicionesDePago = formaMetodoPago.ObservacionesPago;
        ////////    cfdi.Pago.MetodoDePago = formaMetodoPago.MetodoPago;
        ////////    cfdi.Pago.Observaciones = formaMetodoPago.Observaciones;

        ////////    if (formaMetodoPago.TieneFechaVencimiento)
        ////////    {
        ////////        cfdi.Pago.FechaDeVencimiento = General.FechaYMD(formaMetodoPago.FechaVencimiento);
        ////////    }

        ////////}

        ////////private void DatosDeEmbarque()
        ////////{
        ////////    ////cfdi.Receptor.IdReceptor = DatosReceptor.Campo("IdCliente");
        ////////    ////cfdi.Receptor.RFC = DatosReceptor.Campo("RFC");
        ////////    ////cfdi.Receptor.Nombre = DatosReceptor.Campo("Nombre");
        ////////    ////cfdi.Receptor.Pais = DatosReceptor.Campo("Pasi");
        ////////    ////cfdi.Receptor.Estado = DatosReceptor.Campo("Estado");
        ////////    ////cfdi.Receptor.Municipio = DatosReceptor.Campo("Municipio");
        ////////    ////cfdi.Receptor.Localidad = DatosReceptor.Campo("Localidad");
        ////////    ////cfdi.Receptor.Colonia = DatosReceptor.Campo("Colonia");
        ////////    ////cfdi.Receptor.Calle = DatosReceptor.Campo("Calle");
        ////////    ////cfdi.Receptor.NumExterior = DatosReceptor.Campo("NoExterior");
        ////////    ////cfdi.Receptor.NumInterior = DatosReceptor.Campo("NoInterior");
        ////////    ////cfdi.Receptor.CodigoPostal = DatosReceptor.Campo("CodigoPostal");
        ////////    ////cfdi.Receptor.Referencia = DatosReceptor.Campo("Referencia");
        ////////}

        //////private void Conceptos()
        //////{
        //////    // 2K131213.1210 Jesús Díaz 
        //////    if (chkConceptoEncabezado.Checked ) 
        //////    {
        //////        xsaCfdiConcepto item = new xsaCfdiConcepto();

        //////        item.ID_Interno = DtGeneral.EstadoConectado + txtIdConceptoEncabezado.Text;
        //////        item.ID_Externo = DtGeneral.EstadoConectado + txtIdConceptoEncabezado.Text;
        //////        item.Descripcion = lblConceptoEncabezado.Text;
        //////        item.ValorUnitario = 0;
        //////        item.Cantidad = 0;
        //////        item.Importe = 0;
        //////        item.UnidadDeMedida = "NO APLICA";

        //////        cfdi.Conceptos.Add(item); 
        //////    }


        //////    for (int i = 1; i <= list.Registros; i++)
        //////    {
        //////        xsaCfdiConcepto item = new xsaCfdiConcepto();

        //////        item.ID_Interno = list.GetValue(i, (int)Cols.IdProducto);
        //////        item.ID_Externo = list.GetValue(i, (int)Cols.CodigoEAN);
        //////        item.Descripcion = list.GetValue(i, (int)Cols.DescripcionComercial);
        //////        item.ValorUnitario = Convert.ToDouble(list.GetValue(i, (int)Cols.PrecioUnitario));
        //////        item.Cantidad = Convert.ToDouble(list.GetValue(i, (int)Cols.Cantidad));
        //////        item.Importe = Convert.ToDouble(list.GetValue(i, (int)Cols.SubTotal));
        //////        item.UnidadDeMedida = list.GetValue(i, (int)Cols.UnidadDeMedida); 

        //////        cfdi.Conceptos.Add(item); 
        //////    }
        //////}

        //////private void ImpuestosTrasladados()
        //////{
        //////    clsLeer impuestos = new clsLeer();
        //////    impuestos.DataSetClase = dtsListaImpuestos;

        //////    while (impuestos.Leer())
        //////    {
        //////        dTasaIva = impuestos.CampoDouble("TasaIva");
        //////        dIva = impuestos.CampoDouble("Importe");
        //////        cfdi.ImpuestosTrasladados.Add(impuestos.Campo("TipoImpuesto"), dTasaIva, dIva);
        //////    }

        //////    //for (int i = 1; i <= list.Registros; i++)
        //////    //{
        //////    //    dTasaIva = list.GetValueDouble(i, (int)Cols.TasaIva);
        //////    //    dIva = list.GetValueDouble(i, (int)Cols.Iva);
        //////    //    cfdi.ImpuestosTrasladados.Add(cfdImpuestosTrasladados.IVA, dTasaIva, dIva);
        //////    //}
        //////}

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

            bFacturaElectronica_Generada = bRegresa;

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

        #region Impresion 
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
            bool bRegresa = false;
            bool bVisualizar = true;
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
        #endregion Impresion 

        #region Conceptos Especiales
        private void agregarDescripciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sDescripcion = list.GetValue(1, (int)Cols.DescripcionComercial);
            FrmDescripcion_FacturaConcentrada f = new FrmDescripcion_FacturaConcentrada(sDescripcion);
            f.ShowDialog();

            if (f.DescripcionCapturada) 
            {
                list.SetValue(1, (int)Cols.DescripcionComercial, f.Descripcion);
            }
        }

        private void DatosConceptoEspecial()
        {
            txtIdConceptoEncabezado.Text = leer.Campo("IdConcepto");
            lblConceptoEncabezado.Text = leer.Campo("Descripcion"); 
        }

        private void chkConceptoEncabezado_CheckedChanged(object sender, EventArgs e)
        {
            txtIdConceptoEncabezado.Enabled = false;
            txtIdConceptoEncabezado.Text = ""; 
            lblConceptoEncabezado.Text = ""; 

            if (chkConceptoEncabezado.Checked)
            {
                txtIdConceptoEncabezado.Enabled = true;
                lblConceptoEncabezado.Text = "";
                txtIdConceptoEncabezado.Focus(); 
            }
        }

        private void txtIdConceptoEncabezado_TextChanged(object sender, EventArgs e)
        {
            lblConceptoEncabezado.Text = ""; 
        }

        private void txtIdConceptoEncabezado_Validating(object sender, CancelEventArgs e)
        {
            ////if (txtIdConceptoEncabezado.Text != "")
            ////{
            ////    leer.DataSetClase = consulta.CFDI_Conceptos_Especiales(DtGeneral.EstadoConectado, txtIdConceptoEncabezado.Text, "txtId_Validating");
            ////    if (leer.Leer())
            ////    {
            ////        DatosConceptoEspecial();
            ////    }
            ////}
        }

        private void txtIdConceptoEncabezado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.CFDI_Conceptos_Especiales(DtGeneral.EstadoConectado, "txtId_KeyDown");
                if (leer.Leer())
                {
                    DatosConceptoEspecial();
                }
            }
        }
        #endregion Conceptos Especiales
    }
}
