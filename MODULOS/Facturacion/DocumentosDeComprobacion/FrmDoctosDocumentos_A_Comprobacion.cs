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
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.SQL;

using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft; 

namespace Facturacion.DocumentosDeComprobacion
{
    public partial class FrmDoctosDocumentos_A_Comprobacion : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer, leerDetalles;
        clsGrid grid;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        //FrmFuentesDeFinaciamiento_Claves Claves;

        string sFormatoMonto = "###, ###, ###, ###, ###, ##0.###0";
        string sFormatoPiezas = "###, ###, ###, ###, ###, ##0"; 
        string sFolio = "";
        string sMensaje = "";
        bool bInicioDeModulo = true;
        bool bRevisandoSeleccion = false;
        bool bCapturaDeClavesHabilitada = false;
        int iTipoClasificacionSSA = 0; 
        // bool bFolioGuardado = false;
        
        //Para Auditoria
        clsAuditoria auditoria;
        clsSC_File fileLoad;

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        clsLeerExcelOpenOficce excel;

        string sNombreHoja = "Documento".ToUpper();
        bool bExisteHoja = false;

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        string sTitulo = "";
        string sFile_In = "";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bErrorAlCargarPlantilla = false;
        string sMensajeError_CargarPlantilla = "";

        int iPosicion_Oculta = 0;
        int iPosicion_Mostrar_Top = 0;
        int iPosicion_Mostrar_Left = 0;

        string sGUID = "";
        bool bDescargarDocumento = false;
        string sFileName = "";
        string sFile_B64 = "";
        string sRutaDestino = ""; 

        private enum Cols
        {
            Ninguna = 0,
            Clave = 1,
            Descripcion, 
            ContenidoPaquete, 
            Cantidad_A_Comprobar,
            Cantidad_A_Comprobar_Cajas, 
            Cantidad_Comprobada,
            Cantidad_Comprobada_Cajas, 
            Cantidad_x_Comprobar 
        }

        public FrmDoctosDocumentos_A_Comprobacion()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref ConexionLocal);
            leerDetalles = new clsLeer(ref ConexionLocal);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

            fileLoad = new clsSC_File(); 

            grid = new clsGrid(ref grdClaves, this);
            grid.BackColorColsBlk = Color.White;
            grid.EstiloDeGrid = eModoGrid.ModoRow;
            grid.AjustarAnchoColumnasAutomatico = true;
            //grdClaves.EditModeReplace = true;
            grid.SetOrder(true); 
        }

        private void FrmFuentesDeFinaciamiento_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            bInicioDeModulo = false; 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sGUID = "";
            bInicioDeModulo = true;
            bDescargarDocumento = false; 
            Fg.IniciaControles();

            rdoOrigen_01_Venta.Checked = true;
            rdoComprobacion_01_Producto.Checked = false;
            rdoComprobacion_02_Servicio.Checked = false;

            rdoOrigen_01_Venta.Enabled = true;
            rdoOrigen_02_Consignacion.Enabled = true;
            rdoComprobacion_01_Producto.Enabled = true;
            rdoComprobacion_02_Servicio.Enabled = true;


            btnDirectorio.Enabled = true;
            dtpFechaRegistro.Enabled = false;

            ////txtSerie.ReadOnly = false; 
            ////txtFolio_CFDI.ReadOnly = false; 

            IniciarToolBar(false, false, false);
            grid.Limpiar(false);
            bCapturaDeClavesHabilitada = false; 

            bInicioDeModulo = false; 
            txtFolio.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                GuardarInformacion();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ////FrmFuentesDeFinaciamiento_ImportarConfiguraciones f = new FrmFuentesDeFinaciamiento_ImportarConfiguraciones();

            ////f.ShowInTaskbar = false;
            ////f.ShowDialog(this);
        }
        private void btnImportarConfiguraciones_Click( object sender, EventArgs e )
        {
            ////FrmFuentesDeFinaciamiento_ImportarConfiguraciones f = new FrmFuentesDeFinaciamiento_ImportarConfiguraciones();

            ////f.ShowInTaskbar = false; 
            ////f.ShowDialog(this);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ////bool bRegresa = false;
            ////DatosCliente.Funcion = "btnImprimir_Click()";
            ////clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            ////myRpt.RutaReporte = DtGeneral.RutaReportes;
            ////myRpt.NombreReporte = "FACT_FuentesDeFinanciamiento.rpt";

            ////myRpt.Add("Folio", txtFolio.Text);

            ////bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            ////if (bRegresa)
            ////{
            ////    btnNuevo_Click(null, null);
            ////}
            ////else
            ////{
            ////    General.msjError("Ocurrió un error al cargar el reporte.");
            ////}
            
        }
        #endregion Botones

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bCargarInformacion = true; 

            if (txtFolio.Text.Trim() == "")
            {
                sFolio = "*";
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciarToolBar(true, false, false);

                //grid.AddRow();
                //grid.SetValue(1, (int)Cols.IdClave, "*");
            }
            else
            {
                leer.DataSetClase = Consultas.Comprobacion_Documentos(txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else 
                {
                    CargarDatosEncabezado();
                }
            }

        }

        private void CargarDatosEncabezado()
        {
            double dMonto = 0;
            int iPiezas = 0;
            int iTipoDeFuenteDeFinanciamiento = 0;
            int iTipoDeUnidades = 0;
            iTipoClasificacionSSA = 0;

            bRevisandoSeleccion = true; 
            txtFolio.Text = leer.Campo("FolioRelacion");
            sFolio = txtFolio.Text;
            txtFolio.Enabled = false;

            chkFacturaEnCajas.Checked = leer.CampoBool("DocumentoEnCajas");
            txtIdFuenteFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblFuenteDeFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

            lblIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");

            txtNumeroDeReferenciaDocumento.Text = leer.Campo("ReferenciaDocumento");
            chkFacturaEnCajas.Checked = leer.CampoBool("EsDocumentoEnCajas");

            rdoOrigen_01_Venta.Checked = leer.CampoBool("Procesa_Venta");
            rdoOrigen_02_Consignacion.Checked = leer.CampoBool("Procesa_Consigna");
            rdoComprobacion_01_Producto.Checked = leer.CampoBool("Procesa_Producto");
            rdoComprobacion_02_Servicio.Checked = leer.CampoBool("Procesa_Servicio");

            iTipoDeUnidades = leer.CampoInt("TipoDeUnidades");
            rdoTipoUnidades_01_Ordinarias.Checked = iTipoDeUnidades == 1 ? true : false;
            rdoTipoUnidades_02_DosisUnitaria.Checked = iTipoDeUnidades == 2 ? true : false;


            txtNumeroDeReferenciaDocumento.Enabled = false;
            chkFacturaEnCajas.Enabled = false;

            sFileName = leer.Campo("NombreDocumento"); 
            sFile_B64 = leer.Campo("Documento");


            rdoTipoUnidades_01_Ordinarias.Enabled = false;
            rdoTipoUnidades_02_DosisUnitaria.Enabled = false;

            rdoOrigen_01_Venta.Enabled = false;
            rdoOrigen_02_Consignacion.Enabled = false;
            rdoComprobacion_02_Servicio.Enabled = false;
            rdoComprobacion_01_Producto.Enabled = false;
            rdoComprobacion_02_Servicio.Enabled = false;


            btnDirectorio.Enabled = true;
            bDescargarDocumento = true; 

            IniciarToolBar(false, false, true);
            //if (leer.Campo("Status") == "C")
            //{                
            //    General.msjUser("El Cliente seleccionado se encuentra cancelado, verifique");
            //}

            Fg.BloqueaControles(this, false, FramePrincipal);


            Validar_CFDI(txtFolio.Text, 1);


            ////txtAlias.Enabled = true; 
            bRevisandoSeleccion = false; 
        }

        private void CargarDatosDetalle()
        {
            ////leerDetalles.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtFolio.Text.Trim(), "txtFolio_Validating");
            ////if (leerDetalles.Leer())
            ////{
            ////    grid.LlenarGrid(leerDetalles.DataSetClase, false, false); 
            ////} 
        }
        #endregion Buscar Folio

        #region Funciones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnImportarConfiguraciones.Enabled = Guardar; 
        }

        private bool GuardarInformacion()
        {
            bool bContinua = false;

            if (!ConexionLocal.Abrir())
            {
                Error.LogError(ConexionLocal.MensajeError);
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                ConexionLocal.IniciarTransaccion();

                bContinua = GuardarEncabezado();

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    txtFolio.Text = sFolio;
                    ConexionLocal.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    IniciarToolBar(false, false, true);
                    btnImprimir_Click(null, null);
                }
                else
                {
                    txtFolio.Text = "*";
                    txtFolio.Text = sFolio;
                    ConexionLocal.DeshacerTransaccion();
                    Error.GrabarError(leer, "btnGuardar_Click");
                    General.msjError("Ocurrió un error al guardar la Información.");
                    IniciarToolBar(true, false, false);

                }

                ConexionLocal.Cerrar();
            }

            return bContinua;

        }
        private bool GuardarEncabezado()
        {
            bool bRegresa = true;
            int iOpcion = 1;
            int iTipoClasificacionSSA = 1;
            string sSql = "";
            int iVenta = rdoOrigen_01_Venta.Checked ? 1 : 0;
            int iConsigna = rdoOrigen_02_Consignacion.Checked ? 1 : 0;
            int iTipoDeUnidades = rdoTipoUnidades_01_Ordinarias.Checked ? 1 : 2;

            int iProducto = rdoComprobacion_01_Producto.Checked ? 1 : 0;
            int iServicio = rdoComprobacion_02_Servicio.Checked ? 1 : 0;


            ////if (rdoExclusivoControlados.Checked) iTipoClasificacionSSA = 1;
            ////if (rdoExclusivoLibres.Checked) iTipoClasificacionSSA = 2;
            ////if (rdoTodos.Checked) iTipoClasificacionSSA = 3;

            sSql = string.Format("Exec spp_FACT_Comprobacion_Documentos \n" +
                "\t@FolioRelacion = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', \n" + 
                "\t@IdFuenteFinanciamiento = '{4}', @IdFinanciamiento = '{5}', \n" + 
                "\t@ReferenciaDocumento = '{6}', @NombreDocumento = '{7}', @MD5_Documento = '{8}', @Documento = '{9}', \n" +
                "\t@Procesa_Venta = '{10}', @Procesa_Consigna = '{11}', \n" +
                "\t@Procesa_Producto = '{12}', @Procesa_Servicio = '{13}', @DocumentoEnCajas = '{14}', @TipoDeUnidades = '{15}' \n",
                txtFolio.Text.Trim(),
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                txtIdFuenteFinanciamiento.Text, "",
                txtNumeroDeReferenciaDocumento.Text, fileLoad.Name, fileLoad.MD5, fileLoad.Codificar,
                iVenta, iConsigna, iProducto, iServicio, Convert.ToInt32(chkFacturaEnCajas.Checked), iTipoDeUnidades
                );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolio = leer.Campo("Folio");
                sMensaje = leer.Campo("Mensaje");

                bRegresa = GuardarDetalle();
            }

            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true; 
            bool bActivar = true;
            string sSql = "";
            string sClaveSSA = "";
            int iContenidoPaquete = 0;
            int iCantidad_A_Comprobar = 0;
            int iCantidad_A_Comprobar_Cajas = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                sClaveSSA = grid.GetValue(i, Cols.Clave);
                iContenidoPaquete = grid.GetValueInt(i, Cols.ContenidoPaquete);
                iCantidad_A_Comprobar = grid.GetValueInt(i, Cols.Cantidad_A_Comprobar);
                iCantidad_A_Comprobar_Cajas = grid.GetValueInt(i, Cols.Cantidad_A_Comprobar_Cajas);

                sSql += string.Format("Exec spp_FACT_Comprobacion_Documentos_Detalles \n" +
                    "\t@FolioRelacion = '{0}', @ClaveSSA = '{1}', @ContenidoPaquete = '{2}', @Cantidad_A_Comprobar = '{3}', @CantidadAgrupada_A_Comprobar = '{4}' \n\n",
                    sFolio, sClaveSSA, iContenidoPaquete, iCantidad_A_Comprobar, iCantidad_A_Comprobar_Cajas);
            }

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }


        private bool ValidaDatos()
        {
            bool bRegresa = true;
            grid.ColorRenglon(Color.White);


            if (bRegresa && txtIdFuenteFinanciamiento.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("No ha especificado la Fuente de financiamiento del Documento, verifique.");
                txtIdFuenteFinanciamiento.Focus();
            }

            if (bRegresa && txtNumeroDeReferenciaDocumento.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("No ha especificado el Número de referecia (identificación) del Documento, verifique.");
                txtNumeroDeReferenciaDocumento.Focus();
            }

            if (bRegresa)
            {
                //if ( !(chkComprueba_01_Producto.Checked && chkComprueba_02_Servicio.Checked) ) 
                //{
                //    bRegresa = false;
                //    General.msjError("No ha especificado alguno de los tipos de Comprobación, verifique.");
                //    chkComprueba_01_Producto.Focus();
                //}
            }

            if (bRegresa && fileLoad.MD5 == "")
            {
                bRegresa = false;
                General.msjError("No se ha cargado el documento con la información a comprobar, verifique.");
                btnDirectorio.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCantidades(); 
            }

            return bRegresa;
        }

        private bool validarCantidades()
        {
            bool bRegresa = true;

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueInt(i, Cols.Cantidad_A_Comprobar) == 0)
                {
                    bRegresa = false;
                    grid.ColorRenglon(i, Color.Red); 
                }
            }

            if (!bRegresa)
            {
                General.msjError("Se detectaron registros con Cantidad en CERO, verifique."); 
            }

            return bRegresa; 
        }
        #endregion Funciones  

        #region Documentos de Facturas 
        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            //txtFolio_CFDI.Text = "";
            limpiarDatos_CFDI();
        }
        private void txtSerie_Validating(object sender, CancelEventArgs e)
        {

        }
        private void txtFolio_CFDI_TextChanged(object sender, EventArgs e)
        {
            limpiarDatos_CFDI(); ;
        }
        private void txtFolio_CFDI_Validating(object sender, CancelEventArgs e)
        {
            ////if (txtSerie.Text.Trim() != "" && txtFolio_CFDI.Text.Trim() != "")
            ////{
            ////    e.Cancel = !validarInformacionDeCFDI();
            ////}
        }

        private bool validarInformacionDeCFDI()
        {
            bool bRegresa = false;
            ////string sSql = string.Format("Select * \n" +
            ////        "From vw_FACT_CFD_DocumentosElectronicos (NoLock) \n" +
            ////        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' \n",
            ////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtSerie.Text.Trim(), txtFolio_CFDI.Text.Trim()
            ////        ); // + Fg.PonCeros(IdCliente, 4) + "'";

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "validarInformacionDeCFDI");
            ////    General.msjError("Ocurrió un error al validar la información del Documento Electrónico.");
            ////}
            ////else
            ////{
            ////    if (!leer.Leer())
            ////    {
            ////        General.msjAviso("Información de Documento CFDI no encontrada, verifique.");
            ////    }
            ////    else
            ////    {
            ////        bRegresa = true;

            ////        if (bRegresa && leer.Campo("IdTipoDocumento") != "001")
            ////        {
            ////            bRegresa = false;
            ////            General.msjAviso("El documento electrónico no del tipo Factura, verifique.");
            ////            txtSerie.Focus();
            ////        }

            ////        if (bRegresa && leer.Campo("StatusDocto").ToUpper() != "A")
            ////        {
            ////            bRegresa = false;
            ////            General.msjAviso("El documento electrónico no cuenta con Status Activo, verifique.");
            ////            txtSerie.Focus();
            ////        }

            ////        if (bRegresa && leer.CampoBool("EsRelacionConRemisiones"))
            ////        {
            ////            bRegresa = false;
            ////            General.msjAviso("El documento electrónico fue generado con relación de remisiones, es inválido para su procesamiento, verifique.");
            ////            txtSerie.Focus();
            ////        }

            ////        if (bRegresa)
            ////        {
            ////            txtSerie.Enabled = false;
            ////            txtFolio_CFDI.Enabled = false; 

            ////            lblCFDI_FechaExpedicion.Text = string.Format("{0}", leer.CampoFecha("FechaRegistro"));
            ////            lblCFDI_ClienteNombre.Text = leer.Campo("NombreReceptor");
            ////            lblCFDI_FuenteFinanciamiento.Text = leer.Campo("FuenteFinanciamiento");
            ////            lblCFDI_Financiamiento.Text = leer.Campo("Financiamiento");
            ////            lblCFDI_TipoDocumentoDescripcion.Text = leer.Campo("TipoDocumentoDescripcion");
            ////            lblCFDI_TipoDeInsumoDescripcion.Text = leer.Campo("TipoInsumoDescripcion");

            ////            Validar_CFDI(); 
            ////        }
            ////    }
            ////}

            return bRegresa;
        }

        private bool Validar_CFDI(string FolioRelacion, int TipoProceso)
        {
            bool bRegresa = false; 
            string sSql = string.Format("Exec spp_FACT_Comprobacion_Documentos__Validar \n" +
                "\t@FolioRelacion = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @GUID = '{3}', @DocumentoEnCajas = '{4}', @TipoProceso = '{5}' \n",
                //DtGeneral.EmpresaConectada, 
                FolioRelacion, 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sGUID, Convert.ToInt32(chkFacturaEnCajas.Checked), TipoProceso 
                );

            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar_CFDI()");
                General.msjError("Ocurrió un error al validar los datos del documento.");
            }
            else
            {
                if (leer.Leer())
                {
                    //if (leer.CampoBool("EsRegistrada"))
                    //{
                    //    btnGuardar.Enabled = false;
                    //    chkFacturaEnCajas.Enabled = false;
                    //    txtFolio.Text = leer.Campo("FolioRelacion");
                    //}

                    grid.LlenarGrid(leer.DataSetClase);
                }
            }

            return bRegresa; 
        }

        private void chkFacturaEnCajas_CheckedChanged(object sender, EventArgs e)
        {
            if (sGUID != "")
            {
                Validar_CFDI("", 0);
            }
        }

        private void FramePrincipal_Enter(object sender, EventArgs e)
        {

        }

        private void chkComprueba_02_Servicio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkComprueba_01_Producto_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void limpiarDatos_CFDI()
        {
            ////lblCFDI_FechaExpedicion.Text = "";
            ////lblCFDI_ClienteNombre.Text = "";
            ////lblCFDI_FuenteFinanciamiento.Text = "";
            ////lblCFDI_Financiamiento.Text = "";
            ////lblCFDI_TipoDocumentoDescripcion.Text = "";
            ////lblCFDI_FechaExpedicion.Text = "";
            ////lblCFDI_TipoDeInsumoDescripcion.Text = "";
        }
        #endregion Documentos de Facturas 

        #region Fuentes de financiamiento 
        private void txtIdFuenteFinanciamiento_TextChanged(object sender, EventArgs e)
        {
            lblFuenteDeFinanciamiento.Text = "";
            lblIdCliente.Text = "";
            lblCliente.Text = "";
            lblIdSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtIdFuenteFinanciamiento_KeyDown(object sender, KeyEventArgs e)
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

        private void txtIdFuenteFinanciamiento_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFuenteFinanciamiento.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtIdFuenteFinanciamiento.Text.Trim(), "txtRubro_Validating");

                if (leer.Leer())
                {
                    CargarDatosRubro();
                }
                else
                {
                    txtIdFuenteFinanciamiento.Text = "";
                    lblFuenteDeFinanciamiento.Text = "";
                    txtIdFuenteFinanciamiento.Focus();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtIdFuenteFinanciamiento.Text = leer.Campo("IdFuenteFinanciamiento");
            lblFuenteDeFinanciamiento.Text = leer.Campo("Estado") + " -- " + leer.Campo("NumeroDeContrato");

            lblIdCliente.Text = leer.Campo("IdCliente");
            lblCliente.Text = leer.Campo("Cliente");
            lblIdSubCliente.Text = leer.Campo("IdSubCliente");
            lblSubCliente.Text = leer.Campo("SubCliente");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("Fuente de financiamiento seleccionada se encuentra cancelada, verifique.");
                txtIdFuenteFinanciamiento.Text = "";
                lblFuenteDeFinanciamiento.Text = "";
            }

            ////Obtener_ListaProgramasAtencion();
        }
        #endregion Fuentes de financiamiento 

        #region Archivo de Excel 
        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            if (!bDescargarDocumento)
            {
                SeleccionarArchivo(); 
            }
            else
            {
                DescargarArchivo(); 
            }
        }

        private void DescargarArchivo()
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;

                if (Fg.ConvertirStringB64EnArchivo(sFileName, sRutaDestino, sFile_B64, true))
                {
                    General.AbrirDirectorio(sRutaDestino); 
                }
            }

        }

        private void SeleccionarArchivo() 
        {
            openExcel.Title = "Archivos de Pedidos";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;
                fileLoad = new clsSC_File(openExcel.FileName);
                lblDirectorioTrabajo.Text = openExcel.FileName; 
                chkFacturaEnCajas.Checked = true;


                ////IniciaToolBar(false, false, false, false, false, false, false);
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start();
            }
        }

        private DataSet FormatearCampo(DataSet Datos, string NombreColumna, TypeCode typeCode)
        {
            clsLeer datosProceso = new clsLeer();

            try
            {
                datosProceso.DataSetClase = Datos;

                if (datosProceso.ExisteTablaColumna(sNombreHoja, NombreColumna))
                {
                    datosProceso.DataSetClase = TipoColumna(Datos, sNombreHoja, NombreColumna, typeCode);
                }
            }
            catch
            {
                datosProceso.DataSetClase = Datos;
            }

            return datosProceso.DataSetClase;
        }

        private DataSet FormatearDatos()
        {
            DataSet datosProceso = excel.DataSetClase;

            datosProceso = FormatearCampo(datosProceso, "ClaveSSA", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "Cantidad_A_Comprobar", TypeCode.Double);
            datosProceso = FormatearCampo(datosProceso, "Cantidad_Distribuida", TypeCode.Double);

            return datosProceso;

        }

        private static DataSet AgregarColumna(DataSet Datos, string Tabla, string Columna, string TipoDeDatos, string ValorDefault)
        {
            DataSet dts = Datos.Copy();
            DataTable dtConceptos;
            DataColumn dtColumnaNueva;
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Datos;
            if (leer.ExisteTabla(Tabla))
            {
                dtConceptos = leer.Tabla(Tabla);
                if (!leer.ExisteTablaColumna(Tabla, Columna))
                {
                    dtColumnaNueva = new DataColumn(Columna, System.Type.GetType(string.Format("System.{0}", TipoDeDatos)));
                    dtColumnaNueva.DefaultValue = ValorDefault;
                    dtConceptos.Columns.Add(dtColumnaNueva);

                    dts.Tables.Remove(Tabla);
                    dts.Tables.Add(dtConceptos.Copy());
                }
            }

            return dts.Copy();
        }
        private static DataSet TipoColumna(DataSet Datos, string Tabla, string NombreColumna, TypeCode typeCode)
        {
            DataSet dts = Datos.Copy();
            DataTable dt;
            clsLeer leer = new clsLeer();
            int iRenglones = 0;

            leer.DataSetClase = Datos;
            dt = leer.Tabla(Tabla);

            try
            {
                Type type = Type.GetType("System." + typeCode);


                // Agregar columna Temporal
                DataColumn dc = new DataColumn(NombreColumna + "_new", type);
                string sValue = "";

                //Darle la posición de a la columna nueva de la original
                int ordinal = dt.Columns[NombreColumna].Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                //leer el valor y convertirlo 
                foreach (DataRow dr in dt.Rows)
                {
                    iRenglones++;

                    try
                    {
                        if (typeCode == TypeCode.Boolean)
                        {
                            switch (dr[NombreColumna].ToString())
                            {
                                case "0":
                                    dr[dc.ColumnName] = false;
                                    break;
                                case "1":
                                    dr[dc.ColumnName] = true;
                                    break;
                                default:
                                    dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                    break;
                            }
                        }
                        else
                        {
                            if (typeCode == TypeCode.Decimal || typeCode == TypeCode.Double || typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64)
                            {
                                sValue = dr[NombreColumna].ToString().Replace(",", "");

                                //dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                dr[dc.ColumnName] = Convert.ChangeType(sValue, typeCode);
                            }
                            else
                            {
                                dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                            }
                        }
                    }
                    catch (Exception exception_01)
                    {
                        exception_01 = null;
                    }
                }


                // remover columna vieja
                dt.Columns.Remove(NombreColumna);


                // Cambiar nombre a columna nueva
                dc.ColumnName = NombreColumna;
            }
            catch (Exception exception)
            {
                exception = null;
            }

            leer.DataTableClase = dt;

            return leer.DataSetClase.Copy();
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bRegresa = false;

            bValidandoInformacion = true;
            bErrorAlCargarPlantilla = false;
            sMensajeError_CargarPlantilla = "";
            bErrorAlValidar = false;

            //MostrarEnProceso(true, 1);

            excel = new clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            //lst.Limpiar();
            Thread.Sleep(1000);

            //bRegresa = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja").ToUpper();
                if (sHoja == sNombreHoja)
                {
                    bRegresa = true;
                    break;
                }
            }

            bExisteHoja = bRegresa;
            if (!bRegresa)
            {
                General.msjError(string.Format("El documento no contiene la hoja llamada {0} ", sNombreHoja));
            }
            else 
            {
                bRegresa = LeerHoja();
            }

            //MostrarEnProceso(false, 1);
            bValidandoInformacion = false;
            bActivarProceso = bRegresa;

        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            //FrameResultado.Text = sTitulo;
            //lst.Limpiar();
            excel.LeerHoja(sNombreHoja);

            //FrameResultado.Text = sTitulo;
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));
                //lst.CargarDatos(excel.DataSetClase, true, true);
            }

            if (bRegresa)
            {
                bRegresa = CargarInformacionDeHoja();
            }

            return bRegresa;
        }

        private bool CargarInformacionDeHoja()
        {
            bool bRegresa = false;
            sGUID = Guid.NewGuid().ToString();
            //lblProcesados.Visible = true;
            //lblProcesados.Text = "";
            Application.DoEvents();


            excel.DataSetClase = FormatearDatos();

            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);

            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);


            if (!excel.ExisteTablaColumna(1, "GUID"))
            {
                excel.DataSetClase = AgregarColumna(excel.DataSetClase, sNombreHoja, "GUID", "String", sGUID);
            }


            bulk.ClearColumns();
            bulk.DestinationTableName = "FACT_Remisiones__RelacionDocumentos__CargaMasiva";
            bulk.AddColumn("GUID", "GUID");
            bulk.AddColumn("ClaveSSA", "ClaveSSA");
            bulk.AddColumn("Cantidad_A_Comprobar", "Cantidad_A_Comprobar");
            bulk.AddColumn("Cantidad_Distribuida", "Cantidad_Distribuida");


            leer.Exec("Truncate table FACT_Remisiones__RelacionDocumentos__CargaMasiva ");
            bRegresa = bulk.WriteToServer(excel.DataSetClase); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 


            if (bRegresa)
            {
                //bRegresa = validarInformacion();
                bRegresa = Validar_CFDI("", 0); 
            }
            else
            {
                bErrorAlCargarPlantilla = true;
                sMensajeError_CargarPlantilla = bulk.Error.Message;
            }

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
        #endregion Archivo de Excel 

        #region Radio Buttons 
        private void rdoOrigen_01_Venta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOrigen_01_Venta.Checked)
            {
                rdoComprobacion_01_Producto.Checked = false;
                rdoComprobacion_02_Servicio.Checked = false;

                rdoComprobacion_01_Producto.Enabled = true;
                rdoComprobacion_02_Servicio.Enabled = true;
            }
        }

        private void rdoOrigen_02_Consignacion_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOrigen_02_Consignacion.Checked)
            {
                rdoComprobacion_01_Producto.Checked = false;
                rdoComprobacion_02_Servicio.Checked = true;

                rdoComprobacion_01_Producto.Enabled = false;
                rdoComprobacion_02_Servicio.Enabled = true;
            }
            else
            {
                rdoComprobacion_01_Producto.Checked = false;
                rdoComprobacion_02_Servicio.Checked = false;

                rdoComprobacion_01_Producto.Enabled = true;
                rdoComprobacion_02_Servicio.Enabled = true;
            }
        }
        #endregion Radio Buttons 
    }
}
