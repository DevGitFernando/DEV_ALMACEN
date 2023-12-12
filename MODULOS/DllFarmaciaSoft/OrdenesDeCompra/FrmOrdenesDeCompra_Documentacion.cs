using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using System.Web.UI.Design;
////using Dll_IMach4;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmOrdenesDeCompra_Documentacion : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;        
        clsAyudas Ayuda;
        
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsGrid myGrid;
        clsLotes Lotes;
        clsSKU SKU; 
        clsCodigoEAN EAN = new clsCodigoEAN();

        TiposDeInventario tpInventarioModulo = TiposDeInventario.Venta;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        wsFarmacia.wsCnnCliente OrdenesWeb;

        clsVerificarCantidadesOC VerificarCantidades;

        clsAddListaArchivos filesAdd;
        clsAddListaArchivos filesAnexos;
        DataSet dtsDocumentos = new DataSet();
        bool bDocumentosGuardados = false;
        clsListView listaDocumentos; 


        string sUrlServidorOrdenesDeCompra = "";
        bool bServidorCompras_EnLinea = false;
        string sMensajeNoConexion_ServidorCompras = "No fue posible establecer conexión con el Servidor de Ordenes de Compra";

        bool bEsConsultaExterna = false; 
        bool bContinua = true;
        bool bModificarCaptura = true;
        string sFolioOrden = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado;
        string sOrigen = GnFarmacia.UnidadComprasCentrales; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        string sFolioMovto = "";
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sEstadoGenera_OC = "";
        string sFarmaciaGenera_OC = ""; 

        // DataSet dtsOrdenCompra;
        // DataSet dtsOCLocal;

        string sIdTipoMovtoInv = "EOC";
        string sTipoES = "E";
        string sFormato = "#,###,##0.###0";
        string sFolioEntrada = "";
        bool bFolioGuardado = false;
        bool bExceso = false;

        string sMsj_NoModificar_OC = "No es posible agregar y/o modificar productos de la Orden de Compra.";  
        bool bModificarProductos = GnFarmacia.OrdenesDeCompra__ModificarListaDeProductos;
        bool bModificarPrecios = GnFarmacia.OrdenesDeCompra__ModificarPrecios;

        int iIdRack = 0, iIdNivel = 0, iIdEntrepaño = 0;
        string sNombrePosicion = "ORDEN_COMPRAS";
        clsLeer leer;
        clsLeer leerUBI;

        clsCheckListRecepcionProveedor chkList;
        clsLeer leerCosto;

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, CantidadPrometidaCajas = 5, CantidadPrometidaPiezas = 6, 
            Cantidad = 7, Costo = 8, Importe = 9, ImporteIva = 10, ImporteTotal = 11, PorcSurtimiento = 12, 
            CantidadPrometidaCajasRecibida = 13, HabilitarCaptura = 14 
        }

        #region Constructor 
        public FrmOrdenesDeCompra_Documentacion()
        {
            InitializeComponent();
            ConexionLocal = new clsConexionSQL();
            ConexionLocal.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ConexionLocal.DatosConexion.Puerto = General.DatosConexion.Puerto;
            ConexionLocal.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ConexionLocal.DatosConexion.Usuario = General.DatosConexion.Usuario;
            ConexionLocal.DatosConexion.Password = General.DatosConexion.Password;
            ConexionLocal.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            OrdenesWeb = new wsFarmacia.wsCnnCliente(); 

            myLeer = new clsLeer(ref ConexionLocal); 
            myLlenaDatos = new clsLeer(ref ConexionLocal); 
            myLeerLotes = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);
            leerUBI = new clsLeer(ref ConexionLocal);
            leerCosto = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref ConexionLocal, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);
            DtGeneral.PermisosEspeciales_Biometricos.CargarPermisos();

            listaDocumentos = new clsListView(ltsDocumentos);
            listaDocumentos.OrdenarColumnas = false;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }
        #endregion Constructor
        
        private void FrmOrdenDeCompraFarmacia_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Recepciones, bool Imprimir_OrdenDeCompra)
        {
            if (GnFarmacia.ManejaUbicaciones)
            {
                if (GnFarmacia.ManejaUbicacionesEstandar)
                {
                    if (!DtGeneral.CFG_UbicacionesEstandar)
                    {
                        Guardar = false;
                    }
                }
            }

            btnGuardar.Enabled = Guardar;
            ////btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            ////btnRecepcionesOC.Enabled = Recepciones;
            ////btnImprimirOrdenDeCompra.Enabled = Imprimir_OrdenDeCompra;
            ////btnVerificar.Enabled = Imprimir_OrdenDeCompra;            
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            filesAdd = new clsAddListaArchivos();
            filesAnexos = new clsAddListaArchivos();
            dtsDocumentos = new DataSet();
            bDocumentosGuardados = false;
            listaDocumentos.LimpiarItems(); 

            ////btnVerificar.Enabled = false;
            bModificarCaptura = false;
            bFolioGuardado = true;
            IniciarToolBar(false, false, true, false, false);
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = false;
            sEstadoGenera_OC = ""; 
            sFarmaciaGenera_OC = "";
            chkEsCompraPromocion.Visible = false;

            SKU = new clsSKU();
            SKU.IdEmpresa = sEmpresa;
            SKU.IdEstado = sEstado;
            SKU.IdFarmacia = sFarmacia;
            SKU.TipoDeMovimiento = sIdTipoMovtoInv;


            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                IniciarToolBar(false, false, false, false, false);
                dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

                ////btnChkList.Enabled = false;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                dtpFechaPromesaEntrega.Enabled = false;
                dtpFechaVenceDocto.Enabled = false;
                dtpFechaVenceDocto.Value.AddMonths(1); 
                dtpFechaDocto.Enabled = true;

                txtSubTotal.Text = "0.0000";                
                txtIva.Text = "0.0000";                
                txtTotal.Text = "0.0000";
                txtTotalFactura.Text = "0.0000";

                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;
                txtTotalFactura.Enabled = false; 

                ////// Reiniciar Grid por Completo 
                ////myGrid = new clsGrid(ref grdProductos, this);
                ////myGrid.BackColorColsBlk = Color.White;
                ////grdProductos.EditModeReplace = true;
                //myGrid.BloqueaColumna(false, (int)Cols.Costo);

                //myGrid.Limpiar(true);
                ////txtIdPersonal.Text = DtGeneral.IdPersonal;
                ////lblPersonal.Text = DtGeneral.NombrePersonal;
                ////txtIdPersonal.Enabled = false;

                // Asegurarnos de que la fecha maxima sea la del sistema, para evitar errores 
                dtpFechaDocto.MinDate = General.FechaSistema.AddMonths(-1);
                dtpFechaDocto.MaxDate = General.FechaSistema;

                //txtFolio.Text = "*";
                //txtFolio.Enabled = false;
                txtFolio.Focus();
            }
            else
            {
                txtFolio.Focus();
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            string sRuta = "";
            clsLeer doctos = new clsLeer();
            string sNombreDocto = ""; 

            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sRuta = folder.SelectedPath;
                doctos.DataSetClase = dtsDocumentos;
                while (doctos.Leer())
                {
                    sNombreDocto = string.Format("{0}__{1}__{2}", txtFolio.Text, txtOrden.Text, doctos.Campo("NombreDocto"));
                    Fg.ConvertirStringB64EnArchivo(sNombreDocto, sRuta, doctos.Campo("ContenidoDocto"), true);
                    //lst.SetValue(lst.Registros, 1, doctos.Campo("NombreArchivo"));
                }

                General.msjUser("Documentos descargados");
                General.AbrirDirectorio(sRuta); 
            }
        }

        private void btnCargarDocumentos_Click(object sender, EventArgs e)
        {
            if (bDocumentosGuardados)
            {
                filesAdd.DescargarDoctos = bDocumentosGuardados;
                filesAdd.DocumentosGuardados = dtsDocumentos;
            }

            filesAdd.Show(true);

            if (filesAdd.SeGuardoInformacion)
            {
                listaDocumentos.LimpiarItems();
                int iRow = 0;
                foreach (cslArchivo item in filesAdd.Documentos)
                {
                    iRow++;
                    listaDocumentos.AddRow();
                    listaDocumentos.SetValue(iRow, 1, item.NombreArchivo);
                }
            }
            //Documentos
        }

        private void chkEsCompraPromocion_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion Limpiar

        #region Buscar Orden de Compra 
        private void txtOrden_Validated(object sender, EventArgs e)
        {
            // string sMensaje = ""; 

            if (txtOrden.Text.Trim() != "")
            {
                // Se verifica que no se le haya dado entrada
                Consultas.MostrarMsjSiLeerVacio = false; 
                txtOrden.Text = Fg.PonCeros(txtOrden.Text, 8); 
                myLeer.DataSetClase = Consultas.OrdenesCompras_Ingresadas(sEmpresa, sEstado, sOrigen, sFarmacia, txtOrden.Text.Trim(), "txtFolio_Validating");

                if (myLeer.Leer())
                {
                    //CargarFoliosOC(); 
                }
                MarcarProductos_Ampuleo();
                ///ValidarProductos_ConPromocionRegalo(); 
            }
            Consultas.MostrarMsjSiLeerVacio = true;
        }

        private void MarcarProductos_Ampuleo()
        {
            Color colorAmpuleo = Color.Yellow; 

            ////for (int i = 1; i <= myGrid.Rows; i++)
            ////{
            ////    if (myGrid.GetValueDou(i, (int)Cols.CantidadPrometidaCajas) != myGrid.GetValueDou(i, (int)Cols.CantidadPrometidaPiezas))
            ////    {
            ////        myGrid.ColorCelda(i, (int)Cols.Descripcion, colorAmpuleo);
            ////        myGrid.ColorCelda(i, (int)Cols.CantidadPrometidaCajas, colorAmpuleo);
            ////        myGrid.ColorCelda(i, (int)Cols.CantidadPrometidaPiezas, colorAmpuleo);
            ////    }
            ////}
        }
        private bool CargarEncabezadoOrden(DataSet Datos) 
        {
            bool bRegresa = false;
            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = Datos; 

            bRegresa = true;
            leerDatos.Leer(); 

            txtOrden.Text = leerDatos.Campo("Folio");
            sEstadoGenera_OC = leerDatos.Campo("IdEstado");
            sFarmaciaGenera_OC = leerDatos.Campo("IdFarmacia"); 

            dtpFechaPromesaEntrega.Value = leerDatos.CampoFecha("FechaRequeridaEntrega");
            txtIdProveedor.Text = leerDatos.Campo("IdProveedor");
            lblProveedor.Text = leerDatos.Campo("Proveedor");
            txtIdProveedor.Enabled = false;
            txtOrden.Enabled = false;

            bModificarCaptura = false;
            IniciarToolBar(true, false, false, true, true);
            //btnChkList.Enabled = true;

            return bRegresa;
        }

        private void CargarDetallesOrden(DataSet Datos)
        {
            ////clsLeer leerDatos = new clsLeer();
            ////leerDatos.DataSetClase = Datos; 

            ////myGrid.Limpiar(false);
            ////if (leerDatos.Leer())
            ////{
            ////    myGrid.LlenarGrid(leerDatos.DataSetClase);
            ////    Totalizar();
            ////} 

            ////////for (int i = 1; i <= myGrid.Rows; i++)
            ////////{
            ////////    myGrid.BloqueaCelda(true, Color.WhiteSmoke, i, (int)Cols.CodEAN);
            ////////}

            ////myGrid.BackColorColsBlk = Color.WhiteSmoke;
            ////myGrid.BloqueaColumna(!bModificarPrecios, (int)Cols.CodEAN, true);
            ////myGrid.BloqueaColumna(!bModificarPrecios, (int)Cols.Costo, true); 

        }

        private void CargarLotesOrden(DataSet Datos)
        {
            bModificarCaptura = true;
            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = Datos; 


            string sCodigo = "", sCodEAN = "";

            while (leerDatos.Leer())
            {
                sCodigo = leerDatos.Campo("IdProducto");
                sCodEAN = leerDatos.Campo("CodigoEAN");

                myLlenaDatos.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, true, "CargarLotesCodigoEAN()");
                if (Consultas.Ejecuto)
                {
                    // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    myLlenaDatos.Leer();
                    Lotes.AddLotes(myLlenaDatos.DataSetClase);

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        myLlenaDatos.DataSetClase = Consultas.LotesDeCodigoEAN_Ubicacion_Estandar(sEmpresa, sEstado, sFarmacia, iIdRack, iIdNivel, iIdEntrepaño, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
                        if (Consultas.Ejecuto)
                        {
                            myLlenaDatos.Leer();
                            Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
                        }
                    }

                    ////mostrarOcultarLotes();
                    
                }
            }
        }
        #endregion Buscar Orden de Compra

        #region Buscar Folio de Orden de Compra 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            bModificarCaptura = true;
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.OrdenesCompras_Enc(sEmpresa, sEstado, sOrigen, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    bFolioGuardado = true; 
                    IniciarToolBar(true, false, false, false, false);
                    bModificarCaptura = false;
                    CargaEncabezadoFolio();
                }
                else
                {
                    bContinua = false;
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }
                }

                MarcarProductos_Ampuleo(); 
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }
            
        }

        private void CargaEncabezadoFolio()
        {
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker();
            dtpFechaDocto.MinDate = dtpPaso.MinDate;
            dtpFechaDocto.MaxDate = dtpPaso.MaxDate; 
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioOrden = txtFolio.Text;
            txtOrden.Text = myLeer.Campo("FolioOrdenCompraReferencia");
            sEstadoGenera_OC = myLeer.Campo("EstadoGenera");
            sFarmaciaGenera_OC = myLeer.Campo("FarmaciaGenera");  

            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");

            chkEsFacturaOriginal.Enabled = false; 
            chkEsFacturaOriginal.Checked = myLeer.CampoBool("EsFacturaOriginal"); 

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            txtTotalFactura.Text = myLeer.CampoDouble("ImporteFactura").ToString();

            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            dtpFechaPromesaEntrega.Value = myLeer.CampoFecha("FechaPromesaEntrega"); 
            dtpFechaDocto.Value = myLeer.CampoFecha("FechaDocto");
            dtpFechaVenceDocto.Value = myLeer.CampoFecha("FechaVenceDocto");

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            dtpFechaDocto.Enabled = false;
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = true;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;                
            }
            
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;
            clsLeer leerDocumentos = new clsLeer(); 

            dtsDocumentos = Consultas.OrdenesCompras_Documentacion(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargaDetallesFolio");
            leerDocumentos.DataSetClase = dtsDocumentos; 

            bDocumentosGuardados = leerDocumentos.Leer();

            leerDocumentos.RegistroActual = 1;

            listaDocumentos.LimpiarItems();
            int iRow = 0;
            while (leerDocumentos.Leer())
            {
                iRow++;
                listaDocumentos.AddRow();
                listaDocumentos.SetValue(iRow, 1, leerDocumentos.Campo("NombreDocto"));
            }

            ////myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det(sEmpresa, sEstado, sOrigen, sFarmacia, sFolioOrden, "txtFolio_Validating");
            ////if(myLlenaDatos.Leer())
            ////{
            ////    myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            ////}
            ////else
            ////{
            ////    bRegresa = false;
            ////}

                ////// Bloquear grid completo 
                ////myGrid.BloqueaRenglon(true);

                ////CargarDetallesLotesFolio();

            return bRegresa;                 
        }

        private void CargarDetallesLotesFolio()
        {
            ////myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes();
            ////myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Lotes(sEmpresa, sEstado, sOrigen, sFarmacia, sFolioOrden, "CargarDetallesLotesMovimiento");
            ////Lotes.AddLotes(myLlenaDatos.DataSetClase);

            ////if (GnFarmacia.ManejaUbicaciones)
            ////{
            ////    myLlenaDatos.DataSetClase = Consultas.FolioDetLotes_OrdenCompra_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargarDetallesLotesFolio");
            ////    Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            ////}
        }

        #endregion Buscar Folio de Orden de Compra     

        #region Guardar/Actualizar Folio 
        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = false; // btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnRecepciones = false; // btnRecepcionesOC.Enabled;
            bool bBtnImprimir_OC = false; // btnImprimirOrdenDeCompra.Enabled;  
            bExceso = false;
            sMensaje = "Información de documentos guardada satisfactoriamente.";

            //////if (txtFolio.Text != "*")
            //////{
            //////    MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            //////}
            //////else
            {
                //if (ValidarOrdenCompra())
                {
                    if (ValidaDatos())
                    {
                        if (!ConexionLocal.Abrir())
                        {
                            Error.LogError(ConexionLocal.MensajeError);
                            General.msjErrorAlAbrirConexion(); 
                        }
                        else
                        {
                            IniciarToolBar();
                            ConexionLocal.IniciarTransaccion();

                            bContinua = Guarda_Compra_Documentos(filesAdd); 


                            if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                            {
                                ////txtFolio.Text = SKU.Foliador;
                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                // btnNuevo_Click(null, null);
                                IniciarToolBar(false, false, true, false, false);
                                //ImprimirFolio();
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                if (!bExceso)
                                {
                                    txtFolio.Text = "*";
                                    Error.GrabarError(myLeer, "btnGuardar_Click");
                                    General.msjError("Ocurrio un error al guardar los documentos.");
                                    IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnRecepciones, bBtnImprimir_OC);
                                    //btnNuevo_Click(null, null);
                                }

                            }

                            ConexionLocal.Cerrar();
                        }
                    }
                }
            }

        }

        private bool Guarda_Compra_Documentos(clsAddListaArchivos Documentos)
        {
            bool bRegresa = true;  
            string sSql = "";
            int iDocumento = 0; 

            foreach (cslArchivo fileCompra in Documentos.Documentos)
            {
                iDocumento++;

                sSql = String.Format("Exec spp_Mtto_OrdenesDeCompras_Documentacion " +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrdenCompra = '{3}', \n" +
                    "\t@IdDocumento = '{4}', @MD5 = '{5}', @IdTipoDocumento = '{6}', @NombreDocumento = '{7}', @Contenido = '{8}', \n" +
                    "\t@IdPersonal = '{9}', @iOpcion = '{10}' \n",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text,
                    iDocumento, fileCompra.MD5, "0000", fileCompra.NombreArchivo, fileCompra.ContenidoArchivo, DtGeneral.IdPersonal, 1); 

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion Guardar/Actualizar Folio


        #region Eliminar

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion Eliminar

        #region Imprimir
        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private void ImprimirFolio()
        {
            bool bRegresa = false; 

            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirFolio()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Recepcion_Orden_Compras.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Ocurrió un error al cargar el reporte.");
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFolio();
        }

        private void btnImprimirOrdenDeCompra_Click(object sender, EventArgs e)
        {
            // Permite imprimir la orden de compras original 
            bool bRegresa = true;
            //dImporte = Importe; 

            // if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstadoGenera_OC);
                myRpt.Add("IdFarmacia", sFarmaciaGenera_OC);
                myRpt.Add("Folio", Fg.PonCeros(txtOrden.Text, 8));
                myRpt.NombreReporte = "COM_OrdenDeCompra_CodigosEAN";

                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                //bRegresa = DtGeneral.GenerarReporte(sUrlServidorOrdenesDeCompra, General.ImpresionViaWeb, myRpt, DatosCliente);
                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Ocurrió un error al cargar el reporte.");
                    }
                }
            }
        }
        #endregion Imprimir

        #region Folios de Entrada 
        private void btnRecepcionesOC_Click(object sender, EventArgs e)
        {
        }
        #endregion Folios de Entrada

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";
                        
            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio inválido, verifique.");
                txtFolio.Focus();                
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            ////if (myGrid.Rows == 0)
            ////{
            ////    bRegresa = false;
            ////}
            ////else
            ////{
            ////    bRegresa = myGrid.TotalizarColumna((int)Cols.Cantidad) > 0; 
            ////    //////////////// Jesús Díaz 2K120730.1110 
            ////    ////////if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
            ////    ////////{
            ////    ////////    bRegresa = false;
            ////    ////////}
            ////    ////////else
            ////    ////////{
            ////    ////////    if (Lotes.CantidadTotal == 0)
            ////    ////////    {
            ////    ////////        bRegresa = false;
            ////    ////////    }
            ////    ////////    else
            ////    ////////    {
            ////    ////////        for (int i = 1; i <= myGrid.Rows; i++)
            ////    ////////        {
            ////    ////////            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
            ////    ////////            {
            ////    ////////                bRegresa = false;
            ////    ////////                break;
            ////    ////////            }
            ////    ////////        }
            ////    ////////    }
            ////    ////////}
            ////} 

            ////if (!bRegresa)
            ////{
            ////    General.msjUser("Debe capturar al menos un producto para la recepcion\n y/o capturar cantidades para al menos un lote, verifique.");
            ////}

            return bRegresa;

        }
        #endregion Validaciones de Controles

        #region Eventos
        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Checar por incongruencias 
                myLeer.DataSetClase = Ayuda.Folios_Compras(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown");
                //myLeer.DataSetClase = Ayuda.FolioCompras(sEstado, sFarmacia, "txtFolio_KeyDown");

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                    CargaDetallesFolio();
                }
                //Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.
            }

        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {

        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 

            if (myLlenaDatos.Campo("Status").ToUpper() == "A")
            {
                txtIdProveedor.Text = myLlenaDatos.Campo("IdProveedor"); 
                lblProveedor.Text = myLlenaDatos.Campo("Nombre"); 
            }
            else
            {
                General.msjUser("El Proveedor " + myLlenaDatos.Campo("Nombre") +  " actualmente se encuentra cancelado, verifique. " ); 
                txtIdProveedor.Text = "";
                lblProveedor.Text = "";
                txtIdProveedor.Focus(); 
            }
        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void dtpFechaDocto_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFechaDocto_Validating(object sender, CancelEventArgs e)
        {
        }
        #endregion Eventos  

        #region Funciones
        private void Totalizar()
        {
            ////txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString();
            ////txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString();
            ////txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString();
        }
        #endregion Funciones 

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            FrmVerificarIngresosOrdenDeCompra f = new FrmVerificarIngresosOrdenDeCompra();
            f.Verificar(txtOrden.Text);
        }
    } // Llaves de la Clase
} // Llaves del NameSpace
