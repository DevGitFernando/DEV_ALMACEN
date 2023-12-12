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
using Dll_MA_IFacturacion.CFDI;

using Dll_MA_IFacturacion.CFDI.Timbrar;
#endregion USING

namespace Dll_MA_IFacturacion.XSA
{
    public partial class FrmDocumentosGenerados : FrmBaseExt
    {
        enum Cols
        {
            TipoDocumento = 1, FechaRegistro = 2, Serie = 3, Folio = 4, Importe = 5,
            RFC = 6, NombreReceptor = 7, 
            
            Status = 8, StatusDocumento = 9, 
            
            Identificador = 10,
            PAC = 11, NombrePAC = 12, UUID = 13, Observaciones_01 = 14, Observaciones_02 = 15,
            Observaciones_03 = 16, Referencia = 17, TipoDocumentoDescripcion = 18, TipoInsumoDescripcion = 19, RubroFinanciamiento = 20,
            Financiamiento = 21, Cancelacion_Url = 22, Cancelacion_Usuario = 23, Cancelacion_Password = 24
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsListView list;
        clsDatosCliente DatosDeCliente;

        clsAyudas_CFDI ayuda;
        clsConsultas_CFDI consultaCFDI;
        clsConsultas consultas; 

        private string sIdEmpresa = DtGeneral.EmpresaConectada;
        private string sIdEstado = DtGeneral.EstadoConectado;
        private string sIdFarmacia = DtGeneral.FarmaciaConectada;

        string sIdCFDI = ""; 
        string sDocumento_XML = "";
        string sDocumento_PDF = ""; 

        clsLeer Empresa = new clsLeer();
        clsLeer Sucursal = new clsLeer();
        clsLeer Domicilio = new clsLeer();

        //xsawsCancelarDocumento cancelarDocto; 
        string xsaDireccionServicioTimbrado = "";
        string sRFC = "";
        string sRFC_Receptor = "";
        string sKey = "";
        string sNombreEmpresa_o_Sucursal = "";
        string sNombreDocumento = "";
        string sPlantillaDocumento = "";
        DataSet dtsSeries = new DataSet();
        DataSet dtsFarmacias;

        string sSerie = "";
        string sFolio = "";
        string sStatus = "";
        string sMotivoDeCancelacion = "";
        PACs_Timbrado tpPAC = PACs_Timbrado.Ninguno;

        string sCancelar_Doctos_Electronicos = "CANCELAR_DOCUMENTOS_ELECTRONICOS";
        bool bCancelar_Doctos_Electronicos = false;

        string sGenerar_TimbreCFDI_XML = "GENERAR_TIMBRE_CFDI_XML";
        bool bGenerar_TimbreCFDI_XML = false; 


        public FrmDocumentosGenerados()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consultaCFDI = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            DatosDeCliente = new clsDatosCliente(General.DatosApp, this.Name, "");



            list = new clsListView(listDocumentos);
            list.OrdenarColumnas = true;

            ObtenerEstados(); 
        }

        #region Form 
        private void FrmDocumentosGenerados_Load(object sender, EventArgs e)
        {
            InicializaPantalla();
            SolicitarPermisosUsuario();

            TiposDeDocumentos();
            ObtenerSeries_y_Folios(); 
            ObtenerInformacion_Empresa_y_Sucursal(); 
        }
        #endregion Form

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bCancelar_Doctos_Electronicos = DtGeneral.PermisosEspeciales.TienePermiso(sCancelar_Doctos_Electronicos);
            bGenerar_TimbreCFDI_XML = DtGeneral.PermisosEspeciales.TienePermiso(sGenerar_TimbreCFDI_XML); 
        }
        #endregion Permisos de Usuario

        #region Botones 
        private void InicializaPantalla()
        {
            lblTimbresDisponibles.Text = ""; 
            sDocumento_XML = "";
            sDocumento_PDF = ""; 

            Fg.IniciaControles();
            list.LimpiarItems();

            btnImprimir.Enabled = false; 
            chkPeriodo.Checked = true; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDocumentos(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(); 
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
        #endregion Botones

        #region Tipos de Documentos 
        private void ObtenerEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");

            cboEstados.Add(consultas.EstadosConFarmacias("ObtenerEstados"), true, "IdEstado", "NombreEstado");

            cboEstados.Data = DtGeneral.EstadoConectado;

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0; 
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = consultas.Farmacias(cboEstados.Data, "ObtenerFarmacias");
            
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            cboFarmacias.SelectedIndex = 0; 
        }

        private void TiposDeDocumentos()
        {
            // DataSet dtsTipos = consulta.CFDI_TipoDeDocumentos("", "TiposDeDocumentos()"); 
            cboTiposDocumentos.Clear();
            cboTiposDocumentos.Add("*", "TODOS");
            cboTiposDocumentos.Filtro = string.Format(" Status = 'A' ");
            cboTiposDocumentos.Add(consultaCFDI.CFDI_TipoDeDocumentos("", "TiposDeDocumentos()"), true, "IdTipoDocumento", "Documento");
            cboTiposDocumentos.SelectedIndex = 0; 
        }

        private void ObtenerSeries_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add("*", "TODOS");
            dtsSeries = consultaCFDI.CFDI_Series_y_Folios(sIdEmpresa, "", "", "", "ObtenerSeries_y_Folios()");
            // cboSeries.Add(consulta.CFDI_Series_y_Folios(sIdEmpresa, sIdEstado, sIdFarmacia, sTipoDoctoFactura, "ObtenerSeries_y_Folios()"), true, "Serie", "Serie");  
            cboSeries.SelectedIndex = 0;
        }
        #endregion Tipos de Documentos

        #region Eventos
        private void Cargar__Series_y_Folios()
        {
            cboSeries.Clear();
            cboSeries.Add("*", "TODOS");

            if (cboTiposDocumentos.SelectedIndex != 0)
            {
                cboSeries.Filtro = string.Format(" IdTipoDocumento = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' ",
                    cboTiposDocumentos.Data, cboEstados.Data, cboFarmacias.Data);
                cboSeries.Add(dtsSeries, true, "Serie", "Serie");
            }

            cboSeries.SelectedIndex = 0;
        }

        private void cboTiposDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar__Series_y_Folios();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            lblCliente.Text = "";
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text != "")
            {
                leer.DataSetClase = consultaCFDI.CFDI_Clientes(txtId.Text, true, "txtId_Validating");
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
            txtId.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Nombre");
            sRFC_Receptor = leer.Campo("RFC"); 
        }
        #endregion Eventos 

        #region Funciones y Procedimientos Privados
        private void CargarDocumentos()
        {
            string sSql = string.Format("Select F.TipoDeDocumento, F.FechaRegistro, F.Serie, F.Folio, F.Importe, " +
                " F.RFC, F.NombreReceptor, F.StatusDocto, F.StatusDocumento, F.Identificador, F.IdPAC, F.NombrePAC, F.UUID, " +
                " F.Observaciones_01, F.Observaciones_02, F.Observaciones_03, F.Referencia, " +
                " F.TipoDocumentoDescripcion, F.TipoInsumoDescripcion, F.RubroFinanciamento, F.Financiamiento, " +
                " P.UrlProduccion as Url, C.Usuario, C.Password, F.FechaCancelacion, F.IdTipoDocumento,  \n" +
                //" dbo.fg_FACT_NotasDeCredito_Tiene_NotadeCredito(F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.Serie, F.Folio) As TieneNotaDeCredito, \n" +
                " F.TienePagoRelacionado " +
                " From vw_FACT_CFD_DocumentosElectronicos F (NoLock) " +
                " Left Join CFDI_Emisores_PAC C (NoLock) On ( F.IdEmpresa = C.IdEmpresa and F.IdPAC = C.IdPAC )  " +
                " Left Join CFDI_PACs P (NoLock) On ( P.IdPAC = C.IdPAC ) " + 
                " Where F.IdEmpresa = '{0}' and F.IdEstado = '{1}' and F.IdFarmacia = '{2}' ", 
                DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data);

            if (txtId.Text.Trim() != "")
            {
                sSql += string.Format(" and F.RFC = '{0}' ", sRFC_Receptor); 
            }

            if (cboTiposDocumentos.SelectedIndex != 0)
            {
                sSql += string.Format(" and F.IdTipoDocumento = '{0}' ", cboTiposDocumentos.Data);

                if (cboSeries.SelectedIndex != 0)
                {
                    sSql += string.Format(" and F.Serie = '{0}' ", cboSeries.Data);

                    if (txtFolioInicial.Text != "")
                    {
                        sSql += string.Format(" and F.Folio >= '{0}' ", txtFolioInicial.Text);
                        if (txtFolioFinal.Text != "")
                        {
                            sSql += string.Format(" and F.Folio <= '{0}' ", txtFolioFinal.Text);
                        }
                    }
                }
            }

            if (!chkPeriodo.Checked)
            {
                sSql += string.Format(" and convert(varchar(10), F.FechaRegistro, 120) Between '{0}' and '{1}' ",
                    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));  
            }

            sSql += " Order by F.FechaRegistro desc, F.Serie, F.Folio ";

            btnImprimir.Enabled = false; 
            list.LimpiarItems();

            cboEstados.Enabled = false;
            cboFarmacias.Enabled = false; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDocumentos()"); 
                General.msjError("Ocurrió un error al obtener la lista de documentos generados."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados."); 
                }
                else
                {
                    list.CargarDatos(leer.DataSetClase, false, false);
                    btnImprimir.Enabled = true; 
                }
            }

            cboEstados.Enabled = !btnImprimir.Enabled;
            cboFarmacias.Enabled = !btnImprimir.Enabled; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Impresion 
        private void listDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sStatus = list.GetValue((int)Cols.Status);
            bool bDescargar = true;
            bool bCancelar = true; 

            if (tpPAC != PACs_Timbrado.Tralix) 
            {
                bDescargar = false;
            }

            if (sStatus.ToUpper() != "A")
            {
                bCancelar = false; 
            }

            btnCancelarDocto.Enabled = bCancelar; 
            toolStripSeparador_02.Visible = bDescargar;
            btnDescargarArchivos.Visible = bDescargar;

            toolStripSeparador_04.Visible = !bDescargar;
            btnActualizarTimbreCFDI.Visible = !bDescargar; 

            if (!bCancelar_Doctos_Electronicos)
            {
                btnCancelarDocto.Enabled = false; 
            }
        }

        private void listDocumentos_DoubleClick(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio);
            int iIdentificador = list.GetValueInt(list.RenglonActivo, (int)Cols.Identificador);
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sIdTipoDocumento = list.LeerItem().Campo("IdTipoDocumento"); //list.GetValue(list.RenglonActivo, (int)Cols.IdTipoDocumento);

            if (list.Registros > 0)
            {
                if (sSerie != "")
                {
                    ImprimirDocumento(sSerie, sFolio, iIdentificador, sIdTipoDocumento);
                }
            }
        }

        private void ImprimirDocumento(string Serie, string Folio, int Identificador, string sIdTipoDocumento)
        {
            if (tpPAC == PACs_Timbrado.Tralix)
            {
                ImprimirDocumento_Tralix(Serie, Folio);
            }
            else
            {
                ImprimirDocumento_PAC(Serie, Folio, Identificador, sIdTipoDocumento, false);
            }
        }

        private void ImprimirDocumento_Tralix(string Serie, string Folio)
        {
            xsaImprimirDocumento print = new xsaImprimirDocumento(DtGeneral.EmpresaConectada,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.DatosConexion);
            print.Imprimir(Serie, Folio, this);
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
            dato.Valor = list.LeerItem().Campo("StatusDocto");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_01";
            dato.Valor = list.LeerItem().Campo("Observaciones_01");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_02";
            dato.Valor = list.LeerItem().Campo("Observaciones_02");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_03";
            dato.Valor = list.LeerItem().Campo("Observaciones_03");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia";
            dato.Valor = list.LeerItem().Campo("Referencia");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeDocumento";
            dato.Valor = list.LeerItem().Campo("TipoDocumentoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeInsumo";
            dato.Valor = list.LeerItem().Campo("TipoInsumoDescripcion");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "RubroFinanciamiento";
            dato.Valor = list.LeerItem().Campo("RubroFinanciamento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FuenteFinanciamiento";
            dato.Valor = list.LeerItem().Campo("Financiamiento");
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "CantidadLetra";
            dato.Valor = General.LetraMoneda(list.LeerItem().CampoDouble("Importe"));
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FechaCancelacionCFDI";
            dato.Valor = list.LeerItem().Campo("FechaCancelacion");
            Datos.Add(dato);

            return Datos;
        }

        private bool ImprimirDocumento_PAC(string Serie, string Folio, int Identificador, string IdTipoDocumento, bool ActualizarInformacionTimbreFiscal)
        {
            bool bRegresa = true;
            bool bVisualizar = true;
            string sImpresion = " spp_CFDI_GetListaComprobantes ";
            string sSql = string.Format("Exec {0} ", sImpresion) + string.Format(" @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data, Identificador);

            string sMensaje = "¿ Desea ver la vista previa del documento electrónico ? ";
            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";

            TipoDeDocumentoElectronico tpTipoCFDI = (TipoDeDocumentoElectronico)Convert.ToInt32("0" + IdTipoDocumento);


            if (ActualizarInformacionTimbreFiscal)
            {
                bRegresa = ActualizarTimbreFiscal(Serie, Folio, Identificador); 
            }

            if (bRegresa)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Imprimir()");
                    General.msjError("Ocurrió un error al generar la impresión del comprobante.");
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
                        GenerarImpresion(tpTipoCFDI, sFileName, sPDF, sXML, leer.DataSetClase);
                        //DtIFacturacion.GenerarImpresionWebBrowser(false, sFileName, true, GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null);
                    }
                }
            }

            return bRegresa;
        }

        private void GenerarImpresion(TipoDeDocumentoElectronico TipoDeDocumento, string FileName, string PDF, string XML, DataSet DatosExtra)
        {
            bool bVistaPrevia = false;
            bool bVisualizar = true;
            bool bForzarDatosAuxiliares = true;

            ArrayList Datos = new ArrayList();
            Datos = GetExtras(DatosExtra);


            switch (TipoDeDocumento)
            {
                case TipoDeDocumentoElectronico.Factura:
                    DtIFacturacion.GenerarImpresionWebBrowser(bVistaPrevia, FileName, bForzarDatosAuxiliares, Datos, PDF, XML, bVisualizar, null, false);
                    break;

                case TipoDeDocumentoElectronico.NotaDeCredito:
                    DtIFacturacion.GenerarImpresionWebBrowser_NotasDeCredito(bVistaPrevia, FileName, bForzarDatosAuxiliares, Datos, PDF, XML, bVisualizar, null, false);
                    break;

                case TipoDeDocumentoElectronico.ComplementoDePago:
                    DtIFacturacion.GenerarImpresionWebBrowser_ComplementoDePagos(bVistaPrevia, FileName, bForzarDatosAuxiliares, Datos, PDF, XML, bVisualizar, null, false);
                    break;

            }
        }

        private bool ActualizarTimbreFiscal(string Serie, string Folio, int Identificador)
        {
            bool bRegresa = false; 
            string sImpresion = " spp_FACT_CFDI_GetListaComprobantes ";
            string sSql = string.Format("Exec {0} ", sImpresion) + string.Format(" @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Identificador);

            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";
            string sRutaTmpImpresion = DtIFacturacion.RutaCFDI_DocumentosImpresion + @"\" + DtGeneral.EmpresaDatos.RFC;

            bool bArchivo = false; 
            DataSet dtsXML = new DataSet();
            DataSet dtsPDF = new DataSet();

            clsLeer leerXML = new clsLeer();
            clsLeer leerXML_Inf = new clsLeer();
            clsLeer leerPDF = new clsLeer();
            clsLeer leerPDF_Inf = new clsLeer();
            string sUUID = "";
            string ruta_CBB = "";
            string sRFC_Emisor = "";
            string sRFC_Receptor = "";
            string sImporte = ""; 

            Dll_MA_IFacturacion.CFDI.geCFD.clsCFDI baseCFDI = new CFDI.geCFD.clsCFDI();
            Dll_MA_IFacturacion.CFDI.geCBB.clsCBB CBB = new CFDI.geCBB.clsCBB(); 

            try
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ActualizarTimbreFiscal()");
                    General.msjError("Ocurrió un error al obtener la informacinón del comprobante.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("No se encontró información del comprobante solicitado.");
                    }
                    else
                    {
                        sFileName = leer.Campo("NombreFiles") + "___Update";
                        sXML = leer.Campo("uf_Xml_Timbrado");
                        sPDF = leer.Campo("uf_xml_Impresion");


                        bArchivo = Fg.ConvertirStringB64EnArchivo(sFileName + ".xml", sRutaTmpImpresion, sPDF, true);

                        ruta_CBB = sRutaTmpImpresion + @"\" + sFileName + ".cbb";
                        sFileName = sRutaTmpImpresion + @"\" + sFileName + ".xml";


                        dtsPDF.ReadXml(sFileName, XmlReadMode.ReadSchema);
                        dtsXML.ReadXml(new XmlTextReader(new StringReader(sXML)));
                        File.Delete(sFileName);

                        leerXML.DataSetClase = dtsXML;                       
                        leerXML.RenombrarTabla("TimbreFiscalDigital", "TimbreFiscal");
                        leerXML_Inf.DataTableClase = leerXML.Tabla("TimbreFiscal");

                        //// Reemplazar al informacion del Timbre Fiscal 
                        leerPDF_Inf.DataTableClase = leerXML.Tabla("TimbreFiscal"); 
                        leerPDF_Inf.Leer();
                        object[] obj2 = { leerPDF_Inf.Campo("FechaTimbrado"), leerPDF_Inf.Campo("UUID"), 
                                            leerPDF_Inf.Campo("NoCertificadoSAT"), leerPDF_Inf.Campo("SelloCFD"), 
                                            leerPDF_Inf.Campo("SelloSAT"), leerPDF_Inf.Campo("Version") };
                        dtsPDF.Tables["TimbreFiscal"].Rows.Clear(); 
                        dtsPDF.Tables["TimbreFiscal"].Rows.Add(obj2);



                        leerXML_Inf.DataTableClase = leerXML.Tabla("TimbreFiscal");
                        leerXML_Inf.Leer();
                        sUUID = leerXML_Inf.Campo("UUID").Replace(",", "").Replace(" ", "");

                        leerXML_Inf.DataTableClase = leerXML.Tabla("Emisor");
                        leerXML_Inf.Leer();
                        sRFC_Emisor = leerXML_Inf.Campo("RFC");

                        leerXML_Inf.DataTableClase = leerXML.Tabla("Receptor");
                        leerXML_Inf.Leer();
                        sRFC_Receptor = leerXML_Inf.Campo("RFC");

                        leerXML_Inf.DataTableClase = leerXML.Tabla("Comprobante");
                        leerXML_Inf.Leer();
                        sImporte = leerXML_Inf.Campo("Total"); 


                        File.Delete(ruta_CBB);
                        CBB.getImgCBB(sRFC_Emisor, sRFC_Receptor, sImporte, sUUID, ruta_CBB); 
                        object[] objImagenes = new object[1];
                        objImagenes[0] = File.ReadAllBytes(ruta_CBB);
                        dtsPDF.Tables["CBB"].Rows.Clear();
                        dtsPDF.Tables["CBB"].Rows.Add(objImagenes);


                        //// Formateo de información 
                        File.Delete(sFileName);
                        dtsPDF.WriteXml(sFileName, XmlWriteMode.WriteSchema);
                        sPDF = Fg.ConvertirArchivoEnStringB64(sFileName);                                                
                        ////File.Delete(sFileName);

                        sSql = string.Format("Update F Set uf_Xml_Impresion = '{5}', uf_FolioSAT = '{6}', uf_CBB = '{7}' " +
                            "From FACT_CFDI_XML F (NoLock) " + 
                            "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Serie, Folio.Replace(",", "").Replace(" ", ""), sPDF,
                            sUUID, Fg.ConvertirArchivoEnBytes(ruta_CBB));
                        File.Delete(ruta_CBB);

                        if (!leer.Exec(sSql))
                        {
                            Error.GrabarError(leer, "ActualizarTimbreFiscal()");
                            General.msjError("Ocurrió un error al actualizar la informacinón del comprobante.");
                        }
                        else
                        {
                            bRegresa = true;
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                General.msjError("Ocurrió un error al procesar el comprobante solicitado.");
            }

            return bRegresa; 
        }

        private void Imprimir()
        {
            bool bRegresa = true;

            DatosDeCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;            

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "FACT_DocumentosGenerados";
            myRpt.TituloReporte = "Listado de documentos electrónicos generados"; 

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@RFC", sRFC_Receptor); 
            myRpt.Add("@IdTipoDocto", cboTiposDocumentos.Data);
            myRpt.Add("@PeriodoTodo", chkPeriodo.Checked);
            myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
            myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosDeCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion

        #region Informacion de Empresa y Sucursal
        private void ObtenerInformacion_Empresa_y_Sucursal()
        {
            string sSql = string.Format("Select IdEmpresa, NombreFiscal, NombreComercial, RFC, EsPersonaFisica, PublicoGeneral_AplicaIva, Telefonos, " + 
                " Fax, Email, DomExpedicion_DomFiscal, Pais, Estado, Municipio, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, " + 
                " EPais, EEstado, EMunicipio, EColonia, ECalle, ENoExterior, ENoInterior, ECodigoPostal, EReferencia, Status " + 
                " From CFDI_Emisores (NoLock) " + 
                " Where IdEmpresa = '{0}' \n\n", sIdEmpresa);

            ////////sSql += string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Nombre, RFC, Status, Actualizado " +
            ////////    " From FACT_CFD_Sucursales (NoLock) " +
            ////////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n\n",
            ////////    sIdEmpresa, sIdEstado, sIdFarmacia);

            ////////sSql += string.Format("Select Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia " +
            ////////    " From FACT_CFD_Sucursales_Domicilio (NoLock) " +
            ////////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' \n\n",
            ////////    sIdEmpresa, sIdEstado, sIdFarmacia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerInformacion_Empresa_y_Sucursal");
            }
            else
            {
                if (leer.Leer())
                {
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
            ////sKey = Empresa.Campo("KeyLicencia");
            ////xsaDireccionServicioTimbrado = Empresa.Campo("DireccionUrl");
            ////sNombreEmpresa_o_Sucursal = Sucursal.Campo("Nombre");

            ///// Inicializar la cancelación de documentos 
            //cancelarDocto = new xsawsCancelarDocumento(xsaDireccionServicioTimbrado, General.DatosConexion);

            clsTimbrar.EnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
            clsTimbrar.RFC_Emisor = sRFC; 
            clsTimbrar.Url = DtIFacturacion.PAC_Informacion.Url;
            clsTimbrar.Usuario = DtIFacturacion.PAC_Informacion.Usuario;
            clsTimbrar.Password = DtIFacturacion.PAC_Informacion.Password;
            clsTimbrar.CertificadoPKCS12 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
            clsTimbrar.PasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;
            clsTimbrar.PAC = DtIFacturacion.PAC_Informacion.PAC; 

        }
        #endregion Informacion de Empresa y Sucursal

        #region Menu 
        private void btnActualizarTimbreCFDI_Click(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio);
            string sStatus = list.GetValue((int)Cols.Status);
            int iIdentificador = list.GetValueInt(list.RenglonActivo, (int)Cols.Identificador);
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sIdTipoDocumento = list.LeerItem().Campo("IdTipoDocumento"); //list.GetValue(list.RenglonActivo, (int)Cols.IdTipoDocumento);
            bool bRegresa = true;

            if (list.Registros > 0)
            {
                if (tpPAC == PACs_Timbrado.Tralix)
                {
                    bRegresa = false;
                    General.msjAviso("El documento seleccionado no es candidato para Generar nuevamente el Timbre Fiscal.");
                }

                if (bRegresa)
                {
                    bRegresa = sSerie != "" ? true : false;
                }

                if (bRegresa)
                {
                    ImprimirDocumento_PAC(sSerie, sFolio, iIdentificador, sIdTipoDocumento, true); 
                }
            }
        }

        private void btnImprimirDocto_Click(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio);
            string sStatus = list.GetValue((int)Cols.Status);
            int iIdentificador = list.GetValueInt(list.RenglonActivo, (int)Cols.Identificador);
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sIdTipoDocumento = list.LeerItem().Campo("IdTipoDocumento"); //list.GetValue(list.RenglonActivo, (int)Cols.IdTipoDocumento);
            bool bRegresa = true;  

            if (list.Registros > 0)
            {
                if (tpPAC == PACs_Timbrado.Tralix)
                {
                    if (sStatus.ToUpper() != "A")
                    {
                        bRegresa = false;
                        General.msjAviso("El documento seleccionado ya se encuentra cancelado.");
                    }
                }

                if (bRegresa) 
                {
                    bRegresa = sSerie != "" ? true : false;
                }

                if (bRegresa) 
                {
                    ImprimirDocumento(sSerie, sFolio, iIdentificador, sIdTipoDocumento);
                }
            }
        }

        private void btnCancelarDocto_Click(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio).Replace(",", "");
            string sStatus = list.GetValue((int)Cols.Status);

            string sUrl_Cancelacion = list.GetValue((int)Cols.Cancelacion_Url);
            string sUsuario_Cancelacion = list.GetValue((int)Cols.Cancelacion_Usuario);
            string sPassword_Cancelacion = list.GetValue((int)Cols.Cancelacion_Password); 


            string sMotivoDeCancelacion = ""; 
            bool bRegresa = true;
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sUUID = list.LeerItem().Campo("UUID");

            if (list.Registros > 0)
            {
                if (sStatus.ToUpper() != "A")
                {
                    bRegresa = false;
                    General.msjAviso("El documento seleccionado ya se encuentra cancelado.");
                }

                if (bRegresa)
                {
                    bRegresa = sSerie != "" ? true : false;
                }

                if (bRegresa)
                {
                    clsObservaciones ob = new clsObservaciones();
                    ob.Encabezado = "Motivo de cancelación del documento";
                    ob.MaxLength = 200;
                    ob.Show();
                    sMotivoDeCancelacion = ob.Observaciones;
                    bRegresa = ob.Exito;
                }
            }

            if (bRegresa)
            {
                //FrmCancelarDocumentos f = new FrmCancelarDocumentos(xsaDireccionServicioTimbrado, sKey, sRFC, sSerie, sFolio, sMotivoDeCancelacion, tpPAC, sUUID);
                FrmCancelarDocumentos f = new FrmCancelarDocumentos(
                    DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data, sRFC,
                    sSerie, sFolio, sMotivoDeCancelacion, tpPAC, sUUID);
                f.ShowDialog();

                if (f.CancelacionExito)
                {
                    General.msjAviso("El documento electrónico se cancelo satisfactoriamente.");
                    CargarDocumentos();
                }
                else
                {
                    string sMensaje = "No fue posible realizar la cancelación del documento electrónico."; 
                    if (tpPAC != PACs_Timbrado.Tralix)
                    {
                        sMensaje += "\n\n" + clsTimbrar.MensajeError;
                    }

                    General.msjError(sMensaje);
                }
            }
        }

        private void btnDescargarArchivos_Click(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio);
            string sStatus = list.GetValue((int)Cols.Status);
            string sUrl = list.GetValue((int)Cols.Cancelacion_Url);
            string sPassword = list.GetValue((int)Cols.Cancelacion_Password);
            bool bRegresa = true;

            if (list.Registros > 0)
            {
                if (sStatus.ToUpper() != "A")
                {
                    bRegresa = false;
                    General.msjAviso("El documento seleccionado ya se encuentra cancelado.");
                }

                if (bRegresa)
                {
                    bRegresa = sSerie != "" ? true : false;
                }

                if (bRegresa)
                {
                    xsawsDescargarDocumentos docto = new xsawsDescargarDocumentos(sUrl, General.DatosConexion);
                    bRegresa = docto.Descargar(sRFC, sPassword, sSerie, sFolio);

                    sIdCFDI = ""; 
                    sDocumento_XML = docto.PDF;
                    sDocumento_PDF = docto.XML;

                    GuardarDocumentos_XML_PDF(sSerie, sFolio); 
                }
            }
        }

        private bool GuardarDocumentos_XML_PDF(string Serie, string Folio)
        {
            bool bRegresa = true;

            Folio = Folio.Replace(",", "").Trim();
            sDocumento_XML = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, sDocumento_XML));
            sDocumento_PDF = Fg.ConvertirArchivoEnStringB64(Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, sDocumento_PDF));

            string sSql = string.Format("Select FormatoXML, FormatoPDF " +
                "	From FACT_CFD_Documentos_Generados D (NoLock)  " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",                
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Serie, Folio);

            if (!leer.Exec(sSql)) 
            {
                bRegresa = false;
                Error.GrabarError(leer, "GuardarDocumentos_XML_PDF()");
            }
            else 
            {
                if (leer.Leer())
                {
                    if (leer.Campo("FormatoXML") == "" || leer.Campo("FormatoPDF") == "") 
                    {
                        sSql = string.Format("Update D Set FormatoXML = '{5}', FormatoPDF = '{6}', TieneXML = 1, TienePDF = 1 " +
                        "	From FACT_CFD_Documentos_Generados D (NoLock)  " +
                        "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' " +
                        "  ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        Serie, Folio, sDocumento_XML, sDocumento_PDF);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "GuardarDocumentos_XML_PDF()");
                        } 
                    }
                }
            }

            if (bRegresa)
            {
                General.msjUser("Descarga de documentos finalizada correctamente.");
            }
            else
            {
                General.msjUser("Ocurrió un error al descargar los documentos.");
            }

            return bRegresa;
        }

        #endregion Menu 

        private void cboSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bActivo = false; 

            bActivo = cboSeries.SelectedIndex > 0; 

            txtFolioInicial.Enabled = bActivo;
            txtFolioFinal.Enabled = bActivo; 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            if (cboEstados.SelectedIndex != 0)
            {
                ObtenerFarmacias(); 
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSeries.Clear();
            cboSeries.Add("*", "TODOS");
            cboSeries.SelectedIndex = 0;

            if (cboFarmacias.SelectedIndex != 0)
            {
                Cargar__Series_y_Folios(); 
            }
        }

        private void descargarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sSerie = list.GetValue((int)Cols.Serie);
            string sFolio = list.GetValue((int)Cols.Folio).Replace(",", "");
            string sStatus = list.GetValue((int)Cols.Status);

            string sUrl_Cancelacion = list.GetValue((int)Cols.Cancelacion_Url);
            string sUsuario_Cancelacion = list.GetValue((int)Cols.Cancelacion_Usuario);
            string sPassword_Cancelacion = list.GetValue((int)Cols.Cancelacion_Password); 


            string sMotivoDeCancelacion = ""; 
            bool bRegresa = true;
            tpPAC = (PACs_Timbrado)(list.LeerItem().CampoInt("IdPAC"));
            string sUUID = list.LeerItem().Campo("UUID");

            if (list.Registros > 0)
            {
                if (sStatus.ToUpper() != "A")
                {
                    bRegresa = false;
                    General.msjAviso("El documento seleccionado ya se encuentra cancelado.");
                }
                else
                {
                    descargarXML(sSerie, sFolio, sUUID); 
                }
            }
        }

        private void descargarXML(string Serie, string Folio, string UUID)
        {
            bool bRegresa = false;
            string sSql = "";

            switch (tpPAC)
            {
                default:
                    clsTimbrar.PAC = tpPAC;
                    clsTimbrar.RFC_Emisor = sRFC; // DtIFacturacion.EmisorRFC;
                    bRegresa = clsTimbrar.DescargarXML(UUID);
                    break;
            }


            if (bRegresa) 
            {
                sSql = string.Format(
                    "Set DateFormat YMD Update X Set uf_Xml_Timbrado = '{6}' " +
                    "From CFDI_XML X (NoLock) " +
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' and uf_FolioSAT = '{5}' ",
                    DtGeneral.EmpresaConectada, cboEstados.Data, cboFarmacias.Data, Serie, Folio, UUID, clsTimbrar.XmlConTimbreFiscal );

                bRegresa = leer.Exec(sSql);
                sSql = sSql + "";

                if (bRegresa)
                {
                    General.msjUser("XML descargado satisfactoriamente.");
                }
                else
                {
                    General.msjError(clsTimbrar.MensajeError);
                }
            }
        }
    }
}
