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
using Dll_IFacturacion.CFDI.geCFD_33;

using SC_SolutionsSystem.OfficeOpenXml.Data;

namespace Dll_IFacturacion.XSA
{
    public partial class FrmCFDI_Facturas_ImportarDatos : FrmBaseExt
    {
        #region Declaracion de Variables 
        enum Cols
        {
            Ninguna = 0, 
            //IdClave = 1, ClaveSSA = 2, Descripcion = 3, PrecioUnitario = 4, Cantidad = 5, 
            //TasaIva = 6, SubTotal = 7, Iva = 8, Total = 9, UnidadDeMedida = 10, TipoImpuesto = 11, 
            //ClaveLote = 12, Caducidad = 13 

            IdClave = 1, SAT_Producto_Servicio, ClaveSSA, Descripcion, PrecioUnitario, Cantidad, 
            TasaIva, SubTotal, Iva, Total, SAT_UnidadDeMedida, UnidadDeMedida, TipoImpuesto, 
            ClaveLote, Caducidad 

        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnFacturar = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leerFacturar; 
        clsListView list;
        clsAyudas ayuda;
        clsConsultas consulta;

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
        bool bTienePermitida_FacturacionManual = true;

        eVersionCFDI tpVersionCFDI = eVersionCFDI.Version__3_3;

        //Thread thRemision;
        Thread thFacturaElectronica;
        cfdTipoDePlantilla tipoDocumento = cfdTipoDePlantilla.FAC;
        string sTipoDoctoFactura = "001"; 

        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";
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

        string sIdCliente = "";
        string sIdEstablecimiento = "";
        string sIdEstablecimiento_Receptor = "";

        bool VistaPrevia = false;
        long PK = 0;
        string sReferencia = "";
        string sReferencia_02 = "";
        string sReferencia_03 = "";
        string sReferencia_04 = "";
        string sReferencia_05 = "";
        string sObservaciones_01 = "";
        string sObservaciones_02 = "";
        string sObservaciones_03 = "";
        int iTipoDocumento = 0;
        int iTipoInsumo = 0;
        string sRubroFinanciamiento = "";
        string sFuenteFinanciamiento = "";
        bool bErrorEnImportes = false;

        bool bInformacionValidada = false;

        string sXMLFormaPago = "PAGO EN UNA SOLA EXHIBICIÓN";
        string sXMLCondicionesPago = "Crédito";
        string sXMLMetodoPago = "No identificado";
        string sXMLObservacionesPago = ""; 
        PACs_Timbrado tpPAC = PACs_Timbrado.PAX;
        bool bImplementa_ClaveSSA_Base___Identificador = DtIFacturacion.ImplementaClaveSSA_Base__Identificador;

        clsInfoEmisor infoEmisor = null; //new clsInfoEmisor();
        Dll_IFacturacion.CFDI.CFDFunctions.cMain CFDFunct = null; //new CFDFunctions.cMain();
        clsGenCFD CFD = null; //new clsGenCFD();      
        clsGenCFDI_33_Factura CFDI_FD = null; //new clsGenCFDI_Ex();
        clsGenCFDI_33_Factura myCFDI = null; //new clsGenCFDI_Ex();


        DataSet dtsSeries = new DataSet();
        DataSet dtsInformacionSAT = new DataSet(); 
        int iErrores_ClavesSAT = 0;

        #endregion Declaracion de Variables de Facturacion

        public FrmCFDI_Facturas_ImportarDatos() 
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


            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            ////formaMetodoPago = new clsFormaMetodoPago(eTipoDeFacturacion.Manual_Excel);
            ////formaMetodoPago.TipoDeFacturacion = eTipoDeFacturacion.Manual_Excel;

            list = new clsListView(listvClaves);

            tpPAC = DtIFacturacion.PAC_Informacion.PAC;
            iErrores_ClavesSAT = 0;
            //// Anexar al documento la Remisión relacionada 
            // sReferencia = "R" + sFolioRemision; 

            chkClaveSSA_Base___EsIdentificador.BackColor = General.BackColorBarraMenu;
            chkForzarInformacionSAT.BackColor = General.BackColorBarraMenu;
            chkOmitirValidacionDeImportes.BackColor = General.BackColorBarraMenu; 
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
        private void FrmCFDI_Facturas_ImportarDatos_Load(object sender, EventArgs e)
        {
            //list.LimpiarItems();
            listvClaves.Items.Clear(); 


            InicializaPantalla();
            //TiposDeDocumentos(); 
            Obtener_UsoCFDI(); 
            ObtenerSeries_y_Folios();
            ObtenerInformacion_Empresa_y_Sucursal();
            CargarFormas_Metodos_DePago();
            UnidadesDeMedida();

            Cargar_CatalogoInformacionSAT(); 
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
            btnValidarDatos.Enabled = Facturar; 

            if(!bTienePermitida_FacturacionManual)
            {
                btnFacturar.Enabled = false;
                btnValidarDatos.Enabled = false;
            }
        }

        private void InicializaPantalla()
        {
            lblTimbresDisponibles.Text = "Consultar timbres disponibles";
            lblCantidadConLetra.Text = "";
            lblCantidadConLetra.BorderStyle = BorderStyle.None;

            chkClaveSSA_Base___EsIdentificador.Checked = false;
            chkClaveSSA_Base___EsIdentificador.Visible = bImplementa_ClaveSSA_Base___Identificador;
            chkClaveSSA_Base___EsIdentificador.Enabled = chkClaveSSA_Base___EsIdentificador.Visible;

            chkForzarInformacionSAT.Enabled = false;
            iErrores_ClavesSAT = 0;

            tmProceso.Enabled = false;
            tmProceso.Interval = 500; 
            Fg.IniciaControles();
            //list.LimpiarItems(); 
            listvClaves.Items.Clear(); 

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
                sReferencia_02 = "";
                sReferencia_03 = "";
                sReferencia_04 = "";
                sReferencia_05 = "";


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
                    sObservaciones_01 != "" || sObservaciones_02 != "" || sObservaciones_03 != "" || sReferencia != "" || sReferencia_02 != "" || sReferencia_03 != "" || 
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
            f.sReferencia_02 = sReferencia_02;
            f.sReferencia_03 = sReferencia_03;
            f.sReferencia_04 = sReferencia_04;
            f.sReferencia_05 = sReferencia_05;

            f.sRubroFinanciamiento = sRubroFinanciamiento;
            f.sFuenteFinanciamiento = sFuenteFinanciamiento;
            f.iTipoDocumento = iTipoDocumento;
            f.iTipoInsumo = iTipoInsumo;

            f.sIdCliente = sIdCliente;
            f.sIdEstablecimiento = sIdEstablecimiento;
            f.sIdEstablecimiento_Receptor = sIdEstablecimiento_Receptor;

            f.ShowDialog();

            bInformacionValidada = f.InformacionValida;
            sObservaciones_01 = f.sObservaciones_01;
            sObservaciones_02 = f.sObservaciones_02;
            sObservaciones_03 = f.sObservaciones_03;
            sReferencia = f.sReferencia;
            sReferencia_02 = f.sReferencia_02;
            sReferencia_03 = f.sReferencia_03;
            sReferencia_04 = f.sReferencia_04;
            sReferencia_05 = f.sReferencia_05;

            sRubroFinanciamiento = f.sRubroFinanciamiento;
            sFuenteFinanciamiento = f.sFuenteFinanciamiento;
            iTipoDocumento = f.iTipoDocumento;
            iTipoInsumo = f.iTipoInsumo;

            sIdCliente = f.sIdCliente;
            sIdEstablecimiento = f.sIdEstablecimiento;
            sIdEstablecimiento_Receptor = f.sIdEstablecimiento_Receptor;
        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            MostrarPago(false);
            //General.CargarPantalla("FrmCFDI_NotaDeCredito", "Dll_IFacturacion", this.MdiParent); 
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

            if (bRegresa && cboUsosCFDI.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Uso de CFDI válido, verifique.");
                cboUsosCFDI.Focus();
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
                General.msjUser("No existen detalles para facturar, verifique."); 
            }

            if (bRegresa && cboHojas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una hoja válida del archivo de excel, verifique."); 
                cboHojas.Focus(); 
            }

            if (bRegresa && cboHojas.Enabled)
            {
                bRegresa = false;
                General.msjUser("No ha bloqueado la hoja a integrar al documento electrónico, verifique."); 
            }

            if (bRegresa && formaMetodoPago.IdFormaDePago == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Forma de pago válida, verifique."); 
                //cboFormasDePago.Focus(); 
                btnPago.Focus(); 
            }

            if (bRegresa && formaMetodoPago.IdMetodoPago == "0" )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Método de pago válido, verifique.");
                //cboMetodosDePago.Focus();
                btnPago.Focus(); 
            }

            if (bRegresa && !bInformacionValidada)
            {
                bRegresa = false;
                General.msjError("Información adicional incompleta, verifique.");
                btnObservacionesGral.Focus();
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

            chkClaveSSA_Base___EsIdentificador.Enabled = Bloquear;
            chkForzarInformacionSAT.Enabled = Bloquear;
            chkOmitirValidacionDeImportes.Enabled = Bloquear; 

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
            clsLeer leerMetodosDePago = new clsLeer();

            string sSql = "";
            string sStoreEnc = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados ";
            string sStoreDet = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_Detalles_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados_Detalles ";
            string sStoreDet_MetodoDePago = VistaPrevia ? " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago_VP " : " spp_Mtto_FACT_CFD_Documentos_Generados_MetodosDePago ";
            string sIdSerie = cboSeries.ItemActual.GetItem("IdentificadorSerie");
            sSerieDocumento = cboSeries.Data;

            sXMLFormaPago = formaMetodoPago.IdFormaDePago;
            sXMLCondicionesPago = formaMetodoPago.ObservacionesPago;
            sXMLMetodoPago = formaMetodoPago.MetodoPago;
            sXMLObservacionesPago = formaMetodoPago.Observaciones;
            leerMetodosDePago.DataSetClase = formaMetodoPago.ListaDeMetodosDePago; 

            sSql = string.Format("Exec {0} \n" +
                "\t@IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @IdTipoDocumento = '{4}', \n" +
                "\t@IdentificadorSerie = '{5}', @Serie = '{6}', @NombreDocumento = '{7}', @Folio = '{8}', @Importe = '{9}', \n" +
                "\t@RFC = '{10}', @NombreReceptor = '{11}', \n" +
                "\t@IdCFDI = '{12}', @Status = '{13}', @IdPersonalCancela = '{14}', \n" +
                "\t@Observaciones_01 = '{15}', @Observaciones_02 = '{16}', @Observaciones_03 = '{17}', @Referencia = '{18}', \n" +
                "\t@XMLFormaPago = '{19}', @XMLCondicionesPago = '{20}', @XMLMetodoPago = '{21}', \n" +
                "\t@TipoDocumento = '{22}', @TipoInsumo = '{23}', @IdRubroFinanciamiento = '{24}', @IdFuenteFinanciamiento = '{25}', \n" +
                "\t@IdPersonalEmite = '{26}', @UsoDeCFDI = '{27}', @Referencia_02 = '{28}', @Referencia_03 = '{29}', @Referencia_04 = '{30}',  @Referencia_05 = '{31}', \n" +
                "\t@IdEstablecimiento = '{32}', @IdEstablecimiento_Receptor = '{33}' \n",
                sStoreEnc,
                sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoFactura,
                sIdSerie, sSerieDocumento, sNombreDocumento, sFolioDocumento, dTotal.ToString(sFormato).Replace(",", ""),
                sRFC_Receptor, txtClienteNombre.Text.Trim(), sIdCFDI, "A", "",
                sObservaciones_01, sObservaciones_02, sObservaciones_03, sReferencia,
                sXMLFormaPago, sXMLCondicionesPago, sXMLMetodoPago, 
                iTipoDocumento, iTipoInsumo, sRubroFinanciamiento, sFuenteFinanciamiento, DtGeneral.IdPersonal, cboUsosCFDI.Data,
                sReferencia_02, sReferencia_03, sReferencia_04, sReferencia_05,
                sIdEstablecimiento, sIdEstablecimiento_Receptor
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

                for (int Renglon = 1; Renglon <= list.Registros; Renglon++)
                {
                    Identificador = Renglon.ToString();

                    sSAT_Producto_Servicio = list.GetValue(Renglon, (int)Cols.SAT_Producto_Servicio); 
                    sSAT_UnidadDeMedida = list.GetValue(Renglon, (int)Cols.SAT_UnidadDeMedida);  

                    Clave = list.GetValue(Renglon, (int)Cols.ClaveSSA);
                    Descripcion = list.GetValue(Renglon, (int)Cols.Descripcion);
                    PrecioUnitario = list.GetValueDouble(Renglon, (int)Cols.PrecioUnitario);
                    Cantidad = list.GetValueDouble(Renglon, (int)Cols.Cantidad); 
                    TasaIva = list.GetValueDouble(Renglon, (int)Cols.TasaIva);
                    SubTotal = list.GetValueDouble(Renglon, (int)Cols.SubTotal);
                    Iva = list.GetValueDouble(Renglon, (int)Cols.Iva);
                    Total = list.GetValueDouble(Renglon, (int)Cols.Total);
                    UnidadDeMedida = list.GetValue(Renglon, (int)Cols.UnidadDeMedida);
                    TipoImpuesto = list.GetValue(Renglon, (int)Cols.TipoImpuesto);
                    ClaveLote = list.GetValue(Renglon, (int)Cols.ClaveLote);
                    Caducidad = list.GetValue(Renglon, (int)Cols.Caducidad);
                    bCaducidadAsignada = false;



                    if (chkClaveSSA_Base___EsIdentificador.Checked)
                    {
                        Identificador = list.GetValue(Renglon, (int)Cols.IdClave);
                    }


                    //////// Modificacion solicitada por el Gobierno de Puebla, aplicable a todas las operaciones //2016.09.02.1356
                    if (ClaveLote != "")
                    {
                        if (!ClaveLote.ToUpper().Contains("LOTE"))
                        {
                            Descripcion += string.Format("  LOTE : {0}", ClaveLote);
                        }
                        else
                        {
                            Descripcion += string.Format("  {0}", ClaveLote);
                        }
                        

                        if (Caducidad != "")
                        {
                            bCaducidadAsignada = true; 
                            Descripcion += string.Format(", CADUCIDAD : {0}", Caducidad);
                        }
                    }

                    if (!bCaducidadAsignada && Caducidad != "")
                    {
                        Descripcion += string.Format("  CADUCIDAD : {0}", Caducidad);
                    }
                    //////// Modificacion solicitada por el Gobierno de Puebla, aplicable a todas las operaciones //2016.09.02.1356


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
                    DtIFacturacion.GenerarImpresionWebBrowser(VistaPrevia, sFileName, true, DtIFacturacion.GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null, false);
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
            dSubTotal = list.TotalizarColumnaDouble((int)Cols.SubTotal);
            dTotal = list.TotalizarColumnaDouble((int)Cols.Total); 

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
            for (int i = 1; i <= list.Registros; i++)
            {
                xsaCfdiConcepto item = new xsaCfdiConcepto();

                item.ID_Interno = list.GetValue(i, (int)Cols.IdClave);
                item.ID_Externo = list.GetValue(i, (int)Cols.ClaveSSA);
                item.Descripcion = list.GetValue(i, (int)Cols.Descripcion);
                item.ValorUnitario = Convert.ToDouble(list.GetValue(i, (int)Cols.PrecioUnitario));
                item.Cantidad = Convert.ToDouble(list.GetValue(i, (int)Cols.Cantidad));
                item.Importe = Convert.ToDouble(list.GetValue(i, (int)Cols.SubTotal));
                item.UnidadDeMedida = list.GetValue(i, (int)Cols.UnidadDeMedida); 

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

            //for (int i = 1; i <= list.Registros; i++)
            //{
            //    dTasaIva = list.GetValueDouble(i, (int)Cols.TasaIva);
            //    dIva = list.GetValueDouble(i, (int)Cols.Iva);
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
            string sImpresion = VistaPrevia ? " spp_FACT_CFDI_ObtenerDatos_VP " : " spp_FACT_CFDI_ObtenerDatos ";

            clsConexionSQL cnnCFDI = new clsConexionSQL(General.DatosConexion);
            clsLeer leerCFDI = new clsLeer(ref cnnCFDI);

            //if (!bRegresa)
            {
                CFDI_FD = new clsGenCFDI_33_Factura();
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
            Conceptos(1, list.RenglonActivo); 
        }

        private void btnEliminarConcepto_Click(object sender, EventArgs e)
        {
            if (list.Registros > 0)
            {
                list.EliminarRenglonSeleccionado(); 
            }
        }

        private void btnModificarConcepto_Click(object sender, EventArgs e)
        {
            Conceptos(2, list.RenglonActivo); 
        }

        private void Conceptos(int Tipo, int Renglon)
        {
            int iID = 0; 
            string Identificador = list.GetValue(Renglon, (int)Cols.IdClave);
            string Clave = list.GetValue(Renglon, (int)Cols.ClaveSSA);
            string Descripcion = list.GetValue(Renglon, (int)Cols.Descripcion);
            double PrecioUnitario = list.GetValueDouble(Renglon, (int)Cols.PrecioUnitario);
            int Cantidad = list.GetValueInt(Renglon, (int)Cols.Cantidad);
            double TasaIva = list.GetValueDouble(Renglon, (int)Cols.TasaIva);
            double SubTotal = list.GetValueDouble(Renglon, (int)Cols.SubTotal);
            double Iva = list.GetValueDouble(Renglon, (int)Cols.Iva);
            double Total = list.GetValueDouble(Renglon, (int)Cols.Total);
            string UnidadDeMedida = list.GetValue(Renglon, (int)Cols.UnidadDeMedida);
            string TipoImpuesto = list.GetValue(Renglon, (int)Cols.TipoImpuesto);


            if (Identificador == "")
            {
                Identificador = "0"; 
            } 

            if (Tipo == 1)
            {
                FrmDetallesDocumentos f = new FrmDetallesDocumentos();

                f.UnidadesDeMedida = dtsUnidadesDeMedida; 
                f.Identificador = iID.ToString(); 
                f.ShowDialog();

                if (f.Guardado)
                {
                    Identificador = f.Identificador;
                    Clave = f.Clave;
                    Descripcion = f.Descripcion;
                    UnidadDeMedida = f.UnidadDeMedida;
                    Cantidad = f.Cantidad;
                    PrecioUnitario = f.PrecioUnitario;
                    TasaIva = f.TasaIva;
                    SubTotal = f.SubTotal;
                    Iva = f.Iva;
                    Total = f.Total;
                    TipoImpuesto = f.TipoImpuesto; 

                    ListViewItem itmX = listvClaves.Items.Add(Identificador);
                    itmX.SubItems.Add(Clave);
                    itmX.SubItems.Add(Descripcion);
                    itmX.SubItems.Add(PrecioUnitario.ToString(sFormato));
                    itmX.SubItems.Add(Cantidad.ToString(sFormatoIva));
                    itmX.SubItems.Add(TasaIva.ToString(sFormatoIva));
                    itmX.SubItems.Add(SubTotal.ToString(sFormato));
                    itmX.SubItems.Add(Iva.ToString(sFormato));
                    itmX.SubItems.Add(Total.ToString(sFormato));
                    itmX.SubItems.Add(UnidadDeMedida);
                    itmX.SubItems.Add(TipoImpuesto);
                }
            }

            if (Tipo == 2)
            {
                FrmDetallesDocumentos f = new FrmDetallesDocumentos();

                f.UnidadesDeMedida = dtsUnidadesDeMedida; 
                f.Identificador = Identificador;
                f.Clave = Clave;
                f.Descripcion = Descripcion;
                f.UnidadDeMedida = UnidadDeMedida;
                f.Cantidad = Cantidad;
                f.PrecioUnitario = PrecioUnitario;
                f.TasaIva = TasaIva;
                f.SubTotal = SubTotal;
                f.Iva = Iva;
                f.Total = Total;
                f.TipoImpuesto = TipoImpuesto; 

                f.ShowDialog(); 

                if (f.Guardado)
                {
                    Identificador = f.Identificador;
                    Clave = f.Clave;
                    Descripcion = f.Descripcion;
                    UnidadDeMedida = f.UnidadDeMedida;
                    Cantidad = f.Cantidad;
                    PrecioUnitario = f.PrecioUnitario;
                    TasaIva = f.TasaIva;
                    SubTotal = f.SubTotal;
                    Iva = f.Iva;
                    Total = f.Total;
                    TipoImpuesto = f.TipoImpuesto; 

                    list.SetValue(Renglon, (int)Cols.IdClave, Identificador);
                    list.SetValue(Renglon, (int)Cols.ClaveSSA, Clave);
                    list.SetValue(Renglon, (int)Cols.Descripcion, Descripcion);
                    list.SetValue(Renglon, (int)Cols.UnidadDeMedida, UnidadDeMedida);
                    list.SetValue(Renglon, (int)Cols.Cantidad, Cantidad.ToString(sFormatoIva));
                    list.SetValue(Renglon, (int)Cols.PrecioUnitario, PrecioUnitario.ToString(sFormato));
                    list.SetValue(Renglon, (int)Cols.TasaIva, TasaIva.ToString(sFormatoIva));
                    list.SetValue(Renglon, (int)Cols.SubTotal, SubTotal.ToString(sFormato));
                    list.SetValue(Renglon, (int)Cols.Iva, Iva.ToString(sFormato));
                    list.SetValue(Renglon, (int)Cols.Total, Total.ToString(sFormato));
                    list.SetValue(Renglon, (int)Cols.TipoImpuesto, TipoImpuesto);                     
                }
            }

            MostrarPago(false);
        }
        #endregion Menu de Conceptos

        #region Importar Excel 
        SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce excel; 
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
            lblCantidadConLetra.Text = ""; 
            lblRegistros.Text = string.Format("Registros : {0}", 0);
            lblSubTotal.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblIva.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblTotal.Text = string.Format("{0}", iValor.ToString(sFormato));

            lblSubTotal_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblIva_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));
            lblTotal_Calculado.Text = string.Format("{0}", iValor.ToString(sFormato));  
        }

        private void IniciarEtiquetas_CalculoImportes(bool Mostrar)
        {
            lblTitulo_SubTotal_Calculado.Visible = Mostrar;
            lblTitulo_Iva_Calculado.Visible = Mostrar;
            lblTitulo_Total_Calculado.Visible = Mostrar; 

            lblSubTotal_Calculado.Visible = Mostrar;
            lblIva_Calculado.Visible = Mostrar;
            lblTotal_Calculado.Visible = Mostrar;

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
            lblCantidadConLetra.Text = ""; 
            sFile_In = "";
            lblArchivoExcel.Text = "";
            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;
            listvClaves.Items.Clear();

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
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        private void btnBloquearHoja_Click(object sender, EventArgs e)
        {
            if(iErrores_ClavesSAT != 0)
            {
                General.msjError("Se encontraron algunas Claves SAT inválidas (texto en color rojo), es necesario corregirlas antes de continuar.");
            } 
            else
            {
                if(bErrorEnImportes)
                {
                    MensajeErrorImportes();
                } 
                else
                {
                    cboHojas.Enabled = !cboHojas.Enabled;
                }
            }
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            //////MostrarEnProceso(true, 1);
            //////FrameResultado.Text = sTitulo;

            excel = new SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0; 
            listvClaves.Items.Clear();
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
            excel = new SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce(sFile_In);
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
            bool bOmitirValidacionDeImportes = chkOmitirValidacionDeImportes.Checked; 

            int iID = 0;
            string Identificador = "";
            string sSAT_Producto_Servicio = "";
            string sSAT_UnidadDeMedida = ""; 
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
            int iRedondeoDecimales = 2; 
            ListViewItem itmX = null;
            double dRevision = 0;
            string sValorRevision = "";

            double dDiferencia = 0;
            double dlSubTotal = 0;
            double dlIVA = 0;
            double dlTotal = 0;

            double dlSubTotal_Local = 0;
            double dlIVA_Local = 0;
            double dlTotal_Local = 0;


            double dlSubTotal_Local_Aux = 0;
            double dlIVA_Local_Aux = 0;
            double dlTotal_Local_Aux = 0;

            iErrores_ClavesSAT = 0; 


            //sFormato = "###,###,###,###,##0.#0";

            bool bIdentificador_ClaveSSA = chkClaveSSA_Base___EsIdentificador.Checked;
            bool bForzarInformacionSAT = chkForzarInformacionSAT.Checked; 

            ////IniciaToolBar(false, false, false, bRegresa, false, false, false);
            ////FrameResultado.Text = sTitulo;
            //list.LimpiarItems();
            excel.LeerHoja(cboHojas.Data);
            listvClaves.Items.Clear();

            bErrorEnImportes = false; 
            IniciarEtiquetasExcel();
            IniciarEtiquetas_CalculoImportes(false);

            dSubTotal = 0;
            dIva = 0;
            dTotal = 0;

            dSubTotal = 0;
            dIva = 0;
            dTotal = 0;

            ////FrameResultado.Text = sTitulo;
            iRegistrosHoja = excel.Registros;
            bRegresa = iRegistrosHoja > 0;
            listvClaves.BeginUpdate(); 

            while(excel.Leer())
            {
                ////FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //list.CargarDatos(excel.DataSetClase, true, true);
                iID++;

                Identificador = iID.ToString();
                Identificador = excel.Campo("Clave SSA");

                if (bIdentificador_ClaveSSA)
                {
                    Identificador = excel.Campo("Clave SSA Base");

                    ////if( Identificador == "" ) 
                    ////{
                    ////    Identificador = excel.Campo("Clave SSA"); 
                    ////}
                }

                Identificador = Identificador == "" ? iID.ToString() : Identificador;

                sSAT_Producto_Servicio = excel.Campo("SAT Producto Servicio");
                sSAT_UnidadDeMedida = excel.Campo("SAT Unidad de Medida");
                
                Clave = excel.Campo("Clave SSA"); 
                Descripcion = string.Format("{0}", excel.Campo("Descripcion"));

                if (excel.Campo("Presentacion") != "")
                {
                    Descripcion = string.Format("{0} << {1} >>", excel.Campo("Descripcion"), excel.Campo("Presentacion"));
                }
                                
                Descripcion = Descripcion.Replace("'", "´");
                Descripcion = Descripcion.Replace(Fg.Comillas(), "´");
                Descripcion = Descripcion.Replace("\n", ""); 

                UnidadDeMedida = excel.Campo("UnidadDeMedida");
                Cantidad = excel.CampoDouble("Cantidad");
                PrecioUnitario = excel.CampoDouble("PrecioUnitario");
                TasaIva = excel.CampoInt("TasaIva");
                SubTotal = Math.Round(excel.CampoDouble("SubTotal"), iRedondeoDecimales);   //Math.Round(Cantidad * PrecioUnitario, 2);   
                Iva = Math.Round(excel.CampoDouble("Iva"), iRedondeoDecimales);   // Math.Round(SubTotal * (TasaIva / 100.00), 2);
                Total = Math.Round(excel.CampoDouble("Total"), iRedondeoDecimales);    //Math.Round(SubTotal + Iva, 2);   
                TipoImpuesto = excel.Campo("Impuesto");
                TipoImpuesto = "IVA";
                ClaveLote = excel.Campo("ClaveLote").Replace("'", "");

                Caducidad = "";
                if (excel.Campo("Caducidad") != "") 
                {
                    ////Caducidad = General.FechaYMD(Convert.ToDateTime(excel.Campo("Caducidad")));

                    ////Caducidad = General.FechaYMD(Convert.ToDateTime("2019/05/01"));
                    ////Caducidad = General.FechaYMD(Convert.ToDateTime("2019.05.01"));

                    ////Caducidad = General.FechaYMD(Convert.ToDateTime("01.05.2019"));

                    Caducidad = General.FechaYMD(excel.CampoFecha("Caducidad"));
                }

                sValorRevision = Convert.ToDouble(Cantidad * PrecioUnitario).ToString("#################0.#0");
                dRevision = Convert.ToDouble(sValorRevision);


                //dlSubTotal = Math.Round(Convert.ToDouble(Convert.ToDouble(Cantidad * PrecioUnitario).ToString("#################0.#0")), iRedondeoDecimales);
                //dlIVA = Math.Round(dlSubTotal * ((double)TasaIva / 100.0), iRedondeoDecimales);
                //dlTotal = Math.Round(dlSubTotal + dlIVA, iRedondeoDecimales);

                //dlSubTotal = Math.Round(Truncate(Cantidad * PrecioUnitario, 4), iRedondeoDecimales);
                //dlIVA = Math.Round(Truncate(dlSubTotal * ((double)TasaIva / 100.0), iRedondeoDecimales), iRedondeoDecimales);
                //dlTotal = dlSubTotal + dlIVA; 


                ////if ( SubTotal != dlSubTotal ) 
                ////{
                ////    SubTotal = dlSubTotal;
                ////    Iva = dlIVA;
                ////    Total = dlTotal;
                ////    Total = SubTotal + Iva;
                ////}

                ////if (Iva != dlIVA)
                ////{
                ////    Iva = dlIVA; 
                ////}



                //// Recalculo de datos 
                //PrecioUnitario = Math.Truncate(PrecioUnitario * 100) / 100;
                dlSubTotal_Local_Aux = Math.Truncate((PrecioUnitario * Cantidad) * 100) / 100;
                dlIVA_Local_Aux = Math.Truncate(((PrecioUnitario * Cantidad) * ((double)TasaIva / 100.0)) * 100) / 100;
                dlTotal_Local_Aux = Math.Truncate( (dlSubTotal_Local_Aux + dlIVA_Local_Aux) * 100 ) / 100;

                ///// ORIGINAL 
                PrecioUnitario = DtIFacturacion.Truncate(PrecioUnitario, 2);
                dlSubTotal_Local = DtIFacturacion.Truncate(PrecioUnitario * Cantidad, 2);
                dlIVA_Local = DtIFacturacion.Truncate(((PrecioUnitario * Cantidad) * ((double)TasaIva / 100.0)), 2);
                dlTotal_Local = DtIFacturacion.Truncate(dlSubTotal_Local + dlIVA_Local, 2);


                //PrecioUnitario = DtIFacturacion.Truncate(PrecioUnitario, 2);
                //dlSubTotal_Local = DtIFacturacion.Truncate(DtIFacturacion.Redondear(PrecioUnitario * Cantidad, 2), 2);
                //dlIVA_Local = DtIFacturacion.Truncate(DtIFacturacion.Redondear(((PrecioUnitario * Cantidad) * ((double)TasaIva / 100.0)), 2), 2);
                //dlTotal_Local = DtIFacturacion.Truncate(DtIFacturacion.Redondear(dlSubTotal_Local + dlIVA_Local, 2), 2);


                //PrecioUnitario = DtIFacturacion.Truncate(PrecioUnitario, 4);
                //dlSubTotal_Local = DtIFacturacion.Redondear(DtIFacturacion.Redondear(PrecioUnitario * Cantidad, 4), 2);
                //dlIVA_Local = DtIFacturacion.Truncate(((PrecioUnitario * Cantidad) * ((double)TasaIva / 100.0)), 2);
                //dlTotal_Local = DtIFacturacion.Redondear(dlSubTotal_Local + dlIVA_Local, 2);


                if(SubTotal != dlSubTotal_Local || Iva != dlIVA_Local || Total != dlTotal_Local)
                {
                    dlTotal_Local += 0;
                }



                dSubTotal += dlSubTotal_Local;
                dIva += dlIVA_Local;
                dTotal += dlTotal_Local;

                //dSubTotal += Math.Round((Cantidad * PrecioUnitario), iRedondeoDecimales);
                //dIva += Math.Round((Cantidad * PrecioUnitario) * ((double)TasaIva / 100.0), iRedondeoDecimales);
                //dTotal += Math.Round((Cantidad * PrecioUnitario) * (1 + (double)TasaIva / 100.0), iRedondeoDecimales);


                //// Recalculo de datos 




                ////dSubTotal += dlSubTotal;
                ////dIva += dlIVA;
                ////dTotal += dlTotal;



                if (bForzarInformacionSAT)
                {
                    sSAT_Producto_Servicio = excel.Campo("SAT Producto Servicio");
                    sSAT_UnidadDeMedida = excel.Campo("SAT Unidad de Medida");

                    GetInformacionSAT(Clave, ref sSAT_Producto_Servicio, ref sSAT_UnidadDeMedida); 
                }


                itmX = listvClaves.Items.Add(Identificador);
                itmX.SubItems.Add(sSAT_Producto_Servicio);
                itmX.SubItems.Add(Clave);
                itmX.SubItems.Add(Descripcion);
                itmX.SubItems.Add(PrecioUnitario.ToString(sFormato));
                itmX.SubItems.Add(Cantidad.ToString(sFormatoIva));
                itmX.SubItems.Add(TasaIva.ToString(sFormatoIva));
                itmX.SubItems.Add(SubTotal.ToString(sFormato));
                itmX.SubItems.Add(Iva.ToString(sFormato));
                itmX.SubItems.Add(Total.ToString(sFormato));
                itmX.SubItems.Add(sSAT_UnidadDeMedida);
                itmX.SubItems.Add(UnidadDeMedida);
                itmX.SubItems.Add(TipoImpuesto);
                itmX.SubItems.Add(ClaveLote);
                itmX.SubItems.Add(Caducidad);

                ValidarInformacionSAT(iID); 
                // iID 
            }
            listvClaves.EndUpdate(); 




            IniciaToolBarExcel(true, true, true, false, true);             
            lblRegistros.Text = string.Format("Registros : {0}", iRegistrosHoja.ToString("###, ###, ###, ##0"));
            lblSubTotal.Text = string.Format("{0}", list.TotalizarColumnaDouble((int)Cols.SubTotal).ToString(sFormato));
            lblIva.Text = string.Format("{0}", list.TotalizarColumnaDouble((int)Cols.Iva).ToString(sFormato));
            lblTotal.Text = string.Format("{0}", list.TotalizarColumnaDouble((int)Cols.Total).ToString(sFormato));



            SubTotal = DtIFacturacion.Truncate(list.TotalizarColumnaDouble((int)Cols.SubTotal), 2);
            Iva = DtIFacturacion.Truncate(list.TotalizarColumnaDouble((int)Cols.Iva), 2);
            Total = DtIFacturacion.Truncate(list.TotalizarColumnaDouble((int)Cols.Total), 2);
            lblCantidadConLetra.Text = General.LetraMoneda(Total);
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000); 


            lblSubTotal_Calculado.Text = dSubTotal.ToString(sFormato);
            lblIva_Calculado.Text = dIva .ToString(sFormato);
            lblTotal_Calculado.Text = dTotal.ToString(sFormato);

            dTotal = DtIFacturacion.Truncate(dTotal, 2);
            Total = DtIFacturacion.Truncate(Total, 2);

            bErrorEnImportes = false;
            if(!bOmitirValidacionDeImportes)
            {
                bErrorEnImportes = dTotal != Total;
            }


            dDiferencia = Math.Abs(DtIFacturacion.Truncate(dTotal, 2) - DtIFacturacion.Truncate(Total, 2));
            if(bErrorEnImportes)
            {
                bErrorEnImportes = !(dDiferencia <= 0.01);
            }

            IniciarEtiquetas_CalculoImportes(bErrorEnImportes);
            Application.DoEvents();


            if(iErrores_ClavesSAT != 0)
            {
                General.msjError("Se detectarón algunas Claves SAT inválidas, no es posible generar el documento electrónico."); 
            }

            //MostrarPago(true);


                ////// btnGuardar.Enabled = bRegresa;
                ////IniciaToolBar(true, true, true, bRegresa, false, false, true);
                return bRegresa;
        }

        public static double Truncate(double Valor, int Decimales)
        {
            double dRegresa = 0;
            double dRegresa_Auxiliar = Valor;
            decimal step = (decimal)Math.Pow(10, Decimales);
            int tmp = (int)Math.Truncate(step * (decimal)Valor);

            dRegresa = (double)(tmp / step);

            return dRegresa; // (double)(tmp / step);
        }


        private void GetInformacionSAT(string ClaveSSA, ref string ClaveProducto, ref string ClaveUnidad)
        {
            string sSAT_Producto = ClaveProducto;
            string sSAT_Unidad = ClaveUnidad;
            clsLeer leerSAT = new clsLeer();

            try
            {
                leerSAT.DataRowsClase = dtsInformacionSAT.Tables[0].Select(string.Format("ClaveSSA = '{0}' ", ClaveSSA));

                if (!leerSAT.Leer())
                {
                    ClaveUnidad = sSAT_Unidad;
                    ClaveProducto = sSAT_Producto;
                }
                else
                {
                    ClaveProducto = leerSAT.Campo("SAT_ClaveDeProducto_Servicio");
                    ClaveUnidad = leerSAT.Campo("SAT_UnidadDeMedida"); 
                }
            }
            catch 
            {
                ClaveUnidad = sSAT_Unidad;
                ClaveProducto = sSAT_Producto; 
            }
        }

        private void ValidarInformacionSAT(int Renglon)
        {
            string sSAT_Producto = list.GetValue(Renglon, (int)Cols.SAT_Producto_Servicio);
            string sSAT_Unidad = list.GetValue(Renglon, (int)Cols.SAT_UnidadDeMedida);
            bool bRegresa = false;
            clsLeer leerSAT = new clsLeer(ref cnn);
            string sSql = "";

            try
            {

                sSql = string.Format(
                "Select \n " +
                "    (select count(Clave) as Clave  from FACT_CFDI_Productos_Servicios where Clave = '{0}' ) as ClaveSAT, \n" +
                "    (select count(IdUnidad) as IdUnidad  from FACT_CFD_UnidadesDeMedida where IdUnidad = '{1}' ) as UnidadDeMedida \n",
                sSAT_Producto, sSAT_Unidad);

                if(!leerSAT.Exec(sSql))
                {
                    Error.GrabarError(leerSAT, "ValidarInformacionSAT");
                } 
                else
                {
                    if(!leerSAT.Leer())
                    {
                        iErrores_ClavesSAT++;
                    } 
                    else
                    {
                        bRegresa = leerSAT.CampoBool("ClaveSAT") && leerSAT.CampoBool("UnidadDeMedida"); 
                    }
                }
            }
            catch
            {
            }

            if(!bRegresa)
            {
                iErrores_ClavesSAT++;
                list.ColorRowsTexto(Renglon, System.Drawing.Color.Red);
            }

        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear;
        }
        #endregion Importar Excel

        private void cboHojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            listvClaves.Items.Clear();
            IniciarEtiquetasExcel();
            IniciarEtiquetas_CalculoImportes(false);
        }

        private void Cargar_CatalogoInformacionSAT()
        {
            string sSql = ""; 
                
            sSql = string.Format(
                "Select * " +
                "From vw_Claves_Precios_Asignados (NoLock) " + 
                "Where Status = 'A' And IdEstado = '{0}' and Status = 'A' ", //and IdCliente = '{1}' and IdSubCliente = '{2}' ", 
                    DtGeneral.EstadoConectado); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_CatalogoInformacionSAT()");
                General.msjError("Ocurrió un error al obtener los Catálogos del SAT");
            }
            else
            {
                dtsInformacionSAT = leer.DataSetClase;
            }
        }

        private void FrmCFDI_Facturas_ImportarDatos_Shown( object sender, EventArgs e )
        {
            if(!DtIFacturacion.Facturacion_PermisoEspecial())
            {
                General.msjAviso("La generación de facturas manuales esta deshabilitada.");

                if (!DtGeneral.EsEquipoDeDesarrollo)
                {
                    bTienePermitida_FacturacionManual = false;
                    InicializaToolBar(true, false);
                }
            }
        }

        private void btnEstablecimientos_Click( object sender, EventArgs e )
        {
            FrmFactEstablecimientos f = new FrmFactEstablecimientos(TiposDeEstablecimiento.Emisor, "", "");
            f.ShowInTaskbar = false;
            f.ShowDialog();
        }
    }
}
