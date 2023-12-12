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
using Microsoft.VisualBasic.FileIO;
////using Dll_IMach4;

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmOrdenesDeCompra_PreRecepcion : FrmBaseExt 
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
            CodEAN = 1, 
            Codigo, 
            Descripcion,  
            ClaveLote, 
            FechaCaducidad, 
            Cantidad 
        }

        #region Constructor 
        public FrmOrdenesDeCompra_PreRecepcion()
        {
            InitializeComponent();

            General.Pantalla.AjustarTamaño(this, 90, 80);

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


            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        /// <summary>
        /// Obtener la url para descargar las Ordenes de Compra 
        /// </summary>
        private void GetUrl_ServidorCompras()
        {
            try
            {
                // DtGeneral.UrlServidorCentral = "http://LapJesus/wsAlmacenOX/wsOficinaCentral.asmx";            
                sUrlServidorOrdenesDeCompra = DtGeneral.UrlServidorCentral;
                OrdenesWeb.Url = sUrlServidorOrdenesDeCompra; //  DtGeneral.UrlServidorCentral;
                //OrdenesWeb.Url = "http://lapfernando/wsPuebla/wsOficinaCentral.asmx"; 

                bServidorCompras_EnLinea = true;
            }
            catch
            {
                bServidorCompras_EnLinea = false;
                General.msjAviso(sMensajeNoConexion_ServidorCompras);
            }

        }
        #endregion Constructor

        private void FrmOrdenDeCompraFarmacia_Load(object sender, EventArgs e)
        {
            InicializarPantalla();

            // 2K130810.1345    
            GetUrl_ServidorCompras();
        }

        #region Limpiar 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool CerrarRecepcion )
        {
            ////if (GnFarmacia.ManejaUbicaciones)
            ////{
            ////    if (GnFarmacia.ManejaUbicacionesEstandar)
            ////    {
            ////        if (!DtGeneral.CFG_UbicacionesEstandar)
            ////        {
            ////            Guardar = false;
            ////        }
            ////    }
            ////}

            btnGuardar.Enabled = Guardar;
            ////btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnCerrarOrden.Enabled = CerrarRecepcion; 
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

            ////btnVerificar.Enabled = false;
            bModificarCaptura = false;
            bFolioGuardado = true;
            IniciarToolBar(false, false, true, false);
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = false;
            sEstadoGenera_OC = ""; 
            sFarmaciaGenera_OC = "";
            

            SKU = new clsSKU();
            SKU.IdEmpresa = sEmpresa;
            SKU.IdEstado = sEstado;
            SKU.IdFarmacia = sFarmacia;
            SKU.TipoDeMovimiento = sIdTipoMovtoInv;


            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                IniciarToolBar(false, false, false, false);


                ////btnChkList.Enabled = false;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                //myGrid.BloqueaColumna(false, (int)Cols.Costo);

                myGrid.Limpiar(true);


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
            ////FolderBrowserDialog folder = new FolderBrowserDialog();
            ////string sRuta = "";
            ////clsLeer doctos = new clsLeer();
            ////string sNombreDocto = ""; 

            ////if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            ////{
            ////    sRuta = folder.SelectedPath;
            ////    doctos.DataSetClase = dtsDocumentos;
            ////    while (doctos.Leer())
            ////    {
            ////        sNombreDocto = string.Format("{0}__{1}__{2}", txtFolio.Text, txtOrden.Text, doctos.Campo("NombreDocto"));
            ////        Fg.ConvertirStringB64EnArchivo(sNombreDocto, sRuta, doctos.Campo("ContenidoDocto"), true);
            ////        //lst.SetValue(lst.Registros, 1, doctos.Campo("NombreArchivo"));
            ////    }

            ////    General.msjUser("Documentos descargados");
            ////    General.AbrirDirectorio(sRuta); 
            ////}
        }

        private void btnCargarDocumentos_Click(object sender, EventArgs e)
        {
            ////if (bDocumentosGuardados)
            ////{
            ////    filesAdd.DescargarDoctos = bDocumentosGuardados;
            ////    filesAdd.DocumentosGuardados = dtsDocumentos;
            ////}

            ////filesAdd.Show(true);

            ////if (filesAdd.SeGuardoInformacion)
            ////{
            ////    listaDocumentos.LimpiarItems();
            ////    int iRow = 0;
            ////    foreach (cslArchivo item in filesAdd.Documentos)
            ////    {
            ////        iRow++;
            ////        listaDocumentos.AddRow();
            ////        listaDocumentos.SetValue(iRow, 1, item.NombreArchivo);
            ////    }
            ////}
            //////Documentos
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

                if (!myLeer.Leer())
                {
                    SolicitarOCLocal();
                }
                else
                {
                    //sMensaje = String.Format("La orden de compra ya fue registrada con el Folio :  {0}, verifique.", myLeer.Campo("Folio"));
                    //SolicitarOCLocal();

                    CargaEncabezadoFolio();
                    CargaDetallesFolio();

                    //txtFolio_Validating(null, null);
                    //General.msjUser(sMensaje); 
                }
                MarcarProductos_Ampuleo();
                ///ValidarProductos_ConPromocionRegalo(); 
            }
            Consultas.MostrarMsjSiLeerVacio = true;
        }

        private void MarcarProductos_Ampuleo()
        {
            ////Color colorAmpuleo = Color.Yellow;

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

        private bool SolicitarOCLocal()
        {
            bool bRegresa = true;

            clsLeer leerDatos = new clsLeer();
            DataSet dtsEncabezado = new DataSet();
            DataSet dtsDetalles = new DataSet();

            clsGetOrdenDeCompra OrdenCompra = new clsGetOrdenDeCompra(General.DatosConexion, sEmpresa, sEstado, sOrigen, sFarmacia, txtOrden.Text);

            leerDatos.DataSetClase = OrdenCompra.InformacionOrdenCompra();

            if (leerDatos.Tablas > 1)
            {
                dtsEncabezado.Tables.Add(leerDatos.Tabla("OrdenesDeComprasEnc").Copy());
                dtsDetalles.Tables.Add(leerDatos.Tabla("OrdenesDeComprasDet").Copy());

                CargarEncabezadoOrden(dtsEncabezado);
                CargarDetallesOrden(dtsDetalles);
                CargarLotesOrden(dtsDetalles);
                //btnVerificar.Enabled = true;
            }
            else
            {
                bRegresa = false;
            }

            if (!bRegresa)
            {
                SolicitarOrdenDeCompra();
            }

            return bRegresa;
        }

        private void SolicitarOrdenDeCompra()
        {
            if (!bServidorCompras_EnLinea)
            {
                txtOrden.Text = "";
                General.msjAviso(sMensajeNoConexion_ServidorCompras);
                txtOrden.Focus();
            }
            else
            {
                clsDescargarOrdenDeCompra f = new clsDescargarOrdenDeCompra(General.DatosConexion, sUrlServidorOrdenesDeCompra, sEmpresa, sEstado, sOrigen, sFarmacia, txtOrden.Text);

                if (!f.Descargar())
                {
                    txtOrden.Text = "";
                    txtOrden.Focus();
                }
                else
                {
                    //////CargarEncabezadoOrden(f.Encabezado);
                    //////CargarDetallesOrden(f.Detalles);
                    //////CargarLotesOrden(f.Lotes); 

                    if (!f.OrdenGuardada)
                    {
                        IniciarToolBar(false, false, false, false);
                        General.msjAviso("Ocurrio un error, favor de reportarlo a sistemas.");
                    }
                    else
                    {
                        SolicitarOCLocal();
                    }
                }
            }
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

            ////dtpFechaPromesaEntrega.Value = leerDatos.CampoFecha("FechaRequeridaEntrega");
            txtIdProveedor.Text = leerDatos.Campo("IdProveedor");
            lblProveedor.Text = leerDatos.Campo("Proveedor");
            txtIdProveedor.Enabled = false;
            txtOrden.Enabled = false;

            bModificarCaptura = false;
            IniciarToolBar(true, false, false, true);

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
            ////bModificarCaptura = true;
            ////clsLeer leerDatos = new clsLeer();
            ////leerDatos.DataSetClase = Datos; 


            ////string sCodigo = "", sCodEAN = "";

            ////while (leerDatos.Leer())
            ////{
            ////    sCodigo = leerDatos.Campo("IdProducto");
            ////    sCodEAN = leerDatos.Campo("CodigoEAN");

            ////    myLlenaDatos.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, true, "CargarLotesCodigoEAN()");
            ////    if (Consultas.Ejecuto)
            ////    {
            ////        // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
            ////        myLlenaDatos.Leer();
            ////        Lotes.AddLotes(myLlenaDatos.DataSetClase);

            ////        if (GnFarmacia.ManejaUbicaciones)
            ////        {
            ////            myLlenaDatos.DataSetClase = Consultas.LotesDeCodigoEAN_Ubicacion_Estandar(sEmpresa, sEstado, sFarmacia, iIdRack, iIdNivel, iIdEntrepaño, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
            ////            if (Consultas.Ejecuto)
            ////            {
            ////                myLlenaDatos.Leer();
            ////                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            ////            }
            ////        }

            ////        ////mostrarOcultarLotes();
                    
            ////    }
            ////}
        }
        #endregion Buscar Orden de Compra

        #region Buscar Folio de Orden de Compra 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            bModificarCaptura = true;
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.OrdenesCompras_PreRecepcion_Enc(sEmpresa, sEstado, sOrigen, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!myLeer.Leer())
                {
                    bContinua = false; 
                }
                else
                {
                    bFolioGuardado = true; 
                    IniciarToolBar(true, false, false, true);
                    bModificarCaptura = false;
                    
                    CargaEncabezadoFolio();
                }
                ////else
                ////{
                ////    bContinua = false;
                ////}

                ////if (bContinua)
                ////{
                ////    if (!CargaDetallesFolio())
                ////    {
                ////        bContinua = false;
                ////    }
                ////}

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
            ////dtpFechaDocto.MinDate = dtpPaso.MinDate;
            ////dtpFechaDocto.MaxDate = dtpPaso.MaxDate; 
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioOrden = txtFolio.Text;
            txtOrden.Text = myLeer.Campo("FolioOrdenCompraReferencia");
            sEstadoGenera_OC = myLeer.Campo("EstadoGenera");
            sFarmaciaGenera_OC = myLeer.Campo("FarmaciaGenera");  

            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");

            txtFolioDeIngreso.Text = myLeer.Campo("FolioDeIngreso"); 

            ////chkEsFacturaOriginal.Enabled = false; 
            ////chkEsFacturaOriginal.Checked = myLeer.CampoBool("EsFacturaOriginal"); 

            ////txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            ////txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            ////txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            ////txtTotalFactura.Text = myLeer.CampoDouble("ImporteFactura").ToString();

            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            ////dtpFechaPromesaEntrega.Value = myLeer.CampoFecha("FechaPromesaEntrega"); 
            ////dtpFechaDocto.Value = myLeer.CampoFecha("FechaDocto");
            ////dtpFechaVenceDocto.Value = myLeer.CampoFecha("FechaVenceDocto");

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            ////dtpFechaDocto.Enabled = false;
            txtFolioDeIngreso.Enabled = true;
            txtObservaciones.Enabled = true; 

            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = true;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;                
            }

            if (myLeer.CampoBool("EsRecepcionConcluida"))
            {
                txtFolioDeIngreso.Enabled = false;
                txtObservaciones.Enabled = false;
                IniciarToolBar(false, false, true, false); 
            }


            CargaDetallesFolio(); 

        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;
            clsLeer leerDocumentos = new clsLeer();

            myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_PreRecepcion_Det(sEmpresa, sEstado, sOrigen, sFarmacia, sFolioOrden, "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }

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
            if (ValidaDatos(1))
            {
                GuardarInformacion(1); 
            }
        }

        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            if (ValidaDatos(2))
            {
                GuardarInformacion(2);
            }
        }

        private bool GuardarInformacion(int Tipo)
        { 
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = false; // btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnRecepciones = btnCerrarOrden.Enabled;
            bool bBtnImprimir_OC = false; // btnImprimirOrdenDeCompra.Enabled;  
            bExceso = false;
            sMensaje = "Información de documentos guardada satisfactoriamente.";


            if (!ConexionLocal.Abrir())
            {
                Error.LogError(ConexionLocal.MensajeError);
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                IniciarToolBar();
                ConexionLocal.IniciarTransaccion();

                bContinua = GrabarEncabezado(Tipo); 


                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    txtFolio.Text = sFolioOrden;
                    ConexionLocal.CompletarTransaccion();
                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                    // btnNuevo_Click(null, null);
                    IniciarToolBar(false, false, true, false);
                    ImprimirFolio();
                }
                else
                {
                    ConexionLocal.DeshacerTransaccion();
                    if (!bExceso)
                    {
                        txtFolio.Text = "*";
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrio un error al guardar la recepción.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnRecepciones);
                        //btnNuevo_Click(null, null);
                    }

                }

                ConexionLocal.Cerrar();
            }

            return bContinua;

        }

        private bool GrabarEncabezado(int Tipo)
        {
            bool bRegresa = true;
            string sSql = "";
            int iConcluirRecepcion = Tipo == 1 ? 0 : 1;
            string sFolioIngreso = txtFolioDeIngreso.Text == "" ? "" : Fg.PonCeros(txtFolioDeIngreso.Text.Trim(), 8); 

            sSql = String.Format("Exec spp_Mtto_PreRecepcion_OrdenesDeComprasEnc \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDeRecepcion = '{3}', \n" +
                "\t@EsRecepcionConcluida = '{4}', @FolioOrdenCompra = '{5}', @FolioOrdenCompraReferencia = '{6}', \n" +
                "\t@IdPersonal = '{7}', @IdProveedor = '{8}', @ReferenciaDocto = '{9}', \n" +
                "\t@Observaciones = '{10}', @iOpcion = '{11}' \n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, iConcluirRecepcion, sFolioIngreso, txtOrden.Text, 
                DtGeneral.IdPersonal, txtIdProveedor.Text, txtReferenciaDocto.Text,
                txtObservaciones.Text.Trim(), 1);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioMovto = myLeer.Campo("Folio");
                sFolioOrden = myLeer.Campo("Folio");

                if (Tipo == 1)
                {
                    bRegresa = GrabarDetalle();
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "", sCodigoEAN = "", sIdProducto = "";
            int iRenglon = 0, iCantidadPrometidaCajas = 0, iCantidadRecibida = 0;
            double dCostoUnitario = 0, dTasaIva = 0, dSubTotal = 0, dImpteIva = 0, dImporte = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            bool bRegistrar = false;
            string sLote = "";
            string sFechaCaducidad = ""; 
            //double dPorcentaje = 0.0000;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sLote = myGrid.GetValue(i, (int)Cols.ClaveLote);
                sFechaCaducidad = General.FechaYMD(myGrid.GetValueFecha(i, (int)Cols.FechaCaducidad)) ;

                iCantidadRecibida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iRenglon = i; 

                if (sIdProducto != "" && iCantidadRecibida > 0)
                {
                    sSql += string.Format("Exec spp_Mtto_PreRecepcion_OrdenesDeComprasDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDeRecepcion = '{3}', \n" +
                        "\t@Renglon = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Caducidad = '{8}', \n" +
                        "\t@CantidadRecibida = '{9}', @iOpcion = '{10}' \n\n", 
                        sEmpresa, sEstado, sFarmacia, sFolioOrden, i, sIdProducto, sCodigoEAN, 
                        sLote, sFechaCaducidad, iCantidadRecibida, iOpcion); 
                }
            }


            bRegresa = sSql == "" ? false :true; 
            if (bRegresa )
            {
                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
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
                myRpt.NombreReporte = "PtoVta_PreRecepcion_Orden_Compras.rpt";

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
        private bool ValidaDatos(int Tipo)
        {
            bool bRegresa = true;


            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtIdProveedor.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Proveedor inválida, verifique.");
                txtIdProveedor.Focus();
            }

            if (bRegresa && txtReferenciaDocto.Text == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferenciaDocto.Focus();
            }

            if (bRegresa && txtObservaciones.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && Tipo == 2)
            {
                if (txtOrden.Text == "")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Orden de Compra inválido, verifique.");
                    txtOrden.Focus();
                }
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                bRegresa = myGrid.TotalizarColumna((int)Cols.Cantidad) > 0;
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para la recepcion\n y/o capturar cantidades para al menos un lote, verifique.");
            }

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

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            Cols colActiva = (Cols)myGrid.ActiveCol;

            if (!bModificarProductos)
            {
                colActiva = Cols.Ninguna;
            }

            //if (colActiva == Cols.CodEAN)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (colActiva == Cols.Ninguna)
                    {
                        General.msjUser(sMsj_NoModificar_OC);
                    }
                    else
                    {
                        ////myLeer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                        myLeer.DataSetClase = Ayuda.ProductosOrdenCompra(sEmpresa, sEstado, txtOrden.Text, "grdProductos_KeyDown");
                        if (myLeer.Leer())
                        {
                            CargaDatosProducto();
                        }
                    }
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    int iRow = myGrid.ActiveRow;
                    myGrid.DeleteRow(iRow);
                }
                catch { }

                if (myGrid.Rows == 0)
                {
                    myGrid.Limpiar(true);
                }
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblCancelado.Visible == false)
            {
                //if (!myGrid.BuscaRepetido(myLeer.Campo("CodigoEAN"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    //myGrid.SetValue(iRowActivo, (int)Cols.HabilitarCaptura, 1);

                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);

                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.ClaveLote);

                }
                ////else
                ////{
                ////    General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                ////    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                    
                ////    //limpiarColumnas();

                ////    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                ////    myGrid.EnviarARepetido();
                ////}

            }

            // grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if (!bModificarProductos)  //// Permite agregar productos no contenidos en la Orden de Compra Original 
                    {
                        General.msjUser(sMsj_NoModificar_OC);
                    }
                    else
                    {
                        if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                        {
                            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "")
                            {
                                myGrid.Rows = myGrid.Rows + 1;
                                myGrid.ActiveRow = myGrid.Rows;
                                myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
                            }
                        }
                    }
                }
            }
        }



        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            //switch (myGrid.ActiveCol)
            //{
            //    case 1: // Si se cambia el Codigo, se limpian las columnas
            //        {
            //            limpiarColumnas();
            //        }
            //        break;
            //}

        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sValor = "";
            bool bEsEAN_Unico = true;
            Cols colActiva = (Cols)myGrid.ActiveCol;

            if (!bModificarProductos)
            {
                colActiva = Cols.Ninguna;
            }

            switch (colActiva)
            {
                case Cols.CodEAN:
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

                        if (sValor != "")
                        {
                            if (EAN.EsValido(sValor))
                            {
                                ////myLeer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                                myLeer.DataSetClase = Consultas.ProductosOrdenCompra(sEmpresa, sEstado, txtOrden.Text, sValor, "grdProductos_EditModeOff");
                                if (myLeer.Leer())
                                {
                                    if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                    {
                                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                        myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                    }
                                    else
                                    {
                                        if (!bEsEAN_Unico)
                                        {
                                            myLeer.GuardarDatos(1, "CodigoEAN", sValor);
                                            //myLeer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                        }

                                        CargaDatosProducto();
                                    }
                                }
                                else
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                }
                            }
                            else
                            {
                                //General.msjError(sMsjEanInvalido);
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
                                SendKeys.Send("");
                            }
                        }
                    }

                    break;
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }
        #endregion Grid    
    } // Llaves de la Clase
} // Llaves del NameSpace
