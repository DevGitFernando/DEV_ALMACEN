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

using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion; 

namespace Dll_SII_INadro.PedidosUnidades
{
    public partial class FrmPedidosDeUnidad : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;

        Dll_SII_INadro.clsConsultas Consultas;
        Dll_SII_INadro.clsAyudas Ayuda;

        DllFarmaciaSoft.clsConsultas ConsultasGeneral;
        DllFarmaciaSoft.clsAyudas AyudaGeneral;

        DataSet dtsCargarDatos = new DataSet();
        clsGrid myGrid;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();

        TiposDeInventario tpInventarioModulo = TiposDeInventario.Consignacion;
        TiposDeSubFarmacia tpSubFarmacias = TiposDeSubFarmacia.ConsignacionEmulaVenta; 

        clsDatosCliente DatosCliente;
        wsIntAlmacen.wsInterfaceAlmacen conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        wsIntAlmacen.wsInterfaceAlmacen OrdenesWeb;

        clsVerificarCantidadesPedidoUnidad VerificarCantidades;


        string sUrlServidorPedidosDeUnidades = "";
        bool bServidorCompras_EnLinea = false;
        string sMensajeNoConexion_ServidorCompras = "No fue posible establecer conexión con el Servidor de Pedidos";

        bool bEsConsultaExterna = false; 
        bool bContinua = true;
        bool bModificarCaptura = true;
        string sFolioOrden = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
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
        bool bEsNoSolicitado = false;
        bool bModuloCargado = false; 

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, CantidadPrometidaCajas = 5, CantidadPrometidaPiezas = 6, 
            Cantidad = 7, Costo = 8, Importe = 9, ImporteIva = 10, ImporteTotal = 11, PorcSurtimiento = 12, 
            CantidadPrometidaCajasRecibida = 13
        }

        #region Constructor 
        public FrmPedidosDeUnidad():this(false) 
        {
        }

        public FrmPedidosDeUnidad(bool EsNoSolicitado)
        {
            InitializeComponent();
            ConexionLocal = new clsConexionSQL();
            ConexionLocal.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ConexionLocal.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ConexionLocal.DatosConexion.Usuario = General.DatosConexion.Usuario;
            ConexionLocal.DatosConexion.Password = General.DatosConexion.Password;
            ConexionLocal.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");
            conexionWeb = new wsIntAlmacen.wsInterfaceAlmacen();
            conexionWeb.Url = General.Url;

            OrdenesWeb = new wsIntAlmacen.wsInterfaceAlmacen(); 


            myLeer = new clsLeer(ref ConexionLocal);  
            myLlenaDatos = new clsLeer(ref ConexionLocal);  
            myLeerLotes = new clsLeer(ref ConexionLocal);

            Consultas = new Dll_SII_INadro.clsConsultas(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);
            Ayuda = new Dll_SII_INadro.clsAyudas(General.DatosConexion, GnDll_SII_INadro.DatosApp, this.Name);

            ConsultasGeneral = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            AyudaGeneral = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name); 


            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            bEsNoSolicitado = EsNoSolicitado; 
        }

        /// <summary>
        /// Obtener la url para descargar las Ordenes de Compra 
        /// </summary>
        private void GetUrl_ServidorCompras()
        {
            try
            {
                // DtGeneral.UrlServidorCentral = "http://LapJesus/wsAlmacenOX/wsOficinaCentral.asmx";            
                sUrlServidorPedidosDeUnidades = GnDll_SII_INadro.DatosDeServicioWebPedidos.DireccionUrl; // GnFarmacia.UrlAlmacenRegional;
                OrdenesWeb.Url = sUrlServidorPedidosDeUnidades; //  DtGeneral.UrlServidorCentral;
                //OrdenesWeb.Url = "http://lapfernando/wsPuebla/wsOficinaCentral.asmx"; 

                bServidorCompras_EnLinea = true; 
            }
            catch 
            {
                bServidorCompras_EnLinea = false;
                General.msjAviso(sMensajeNoConexion_ServidorCompras); 
            }

            if (General.EsEquipoDeDesarrollo)
            {
                lblUrl.Text = sUrlServidorPedidosDeUnidades;
                lblUrl.Visible = General.EsEquipoDeDesarrollo; 
            }

        }
        #endregion Constructor

        #region Form 
        private void FrmOrdenDeCompraFarmacia_Load(object sender, EventArgs e)
        {
            if (bEsNoSolicitado)
            {
                this.Text += ": No solicitado ";
            }

            btnNuevo_Click(this,null);

            // 2K130810.1345    
            GetUrl_ServidorCompras(); 
        }

        private void FrmPedidosDeUnidad_Shown(object sender, EventArgs e)
        {
            if (!bModuloCargado)
            {
                bModuloCargado = true;
                if (bEsNoSolicitado)
                {
                    General.msjUser("Solo captura de insumos no incluidos en el pedido original");
                }
            }
        }
        #endregion Form

        #region Limpiar
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Recepciones, bool Imprimir_OrdenDeCompra)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnRecepcionesOC.Enabled = Recepciones;
            btnImprimirOrdenDeCompra.Enabled = Imprimir_OrdenDeCompra; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblUrl.BorderStyle = BorderStyle.None;
            lblUrl.Visible = General.EsEquipoDeDesarrollo; 

            bModificarCaptura = false;
            bFolioGuardado = true;
            IniciarToolBar(false, false, true, false, false);
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = false;
            sEstadoGenera_OC = ""; 
            sFarmaciaGenera_OC = "";

            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                IniciarToolBar(false, false, false, false, false);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo, tpSubFarmacias);
                Lotes.ManejoLotes = OrigenManejoLotes.OrdenesDeCompra;
                // tpSubFarmacias

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                dtpFechaPromesaEntrega.Enabled = false;
                dtpFechaVenceDocto.Enabled = false;
                dtpFechaVenceDocto.Value.AddMonths(1); 
                dtpFechaDocto.Enabled = true;

                txtSubTotal.Text = "0.0000";                
                txtIva.Text = "0.0000";                
                txtTotal.Text = "0.0000";                

                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                //myGrid.BloqueaColumna(false, (int)Cols.Costo);

                myGrid.Limpiar(false);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

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
        #endregion Limpiar

        #region Buscar Orden de Compra 
        private void txtOrden_Validated(object sender, EventArgs e)
        {
            VerificarPedidoDescargado(txtOrden.Text, true); 
        }

        private void VerificarPedidoDescargado(string ReferenciaDePedido, bool Captura)
        {
            // string sMensaje = ""; 

            if (ReferenciaDePedido.Trim() != "")
            {
                ////// Se verifica que no se le haya dado entrada
                Consultas.MostrarMsjSiLeerVacio = false;
                //txtOrden.Text = ReferenciaDePedido;
                myLeer.DataSetClase = Consultas.ND_Pedidos_Ingresados(sEmpresa, sEstado, sFarmacia, ReferenciaDePedido.Trim(), "VerificarPedidoDescargado");

                if (!myLeer.Leer())
                {
                    if (!bEsNoSolicitado)
                    {
                        SolicitarPedidoLocal();
                    }
                    else 
                    {
                        IniciarToolBar(false, false, false, false, false); 
                        General.msjUser("Referencia de Pedido no cuenta con ingresos previos, verifique."); 
                    }
                }
                else
                {
                    //sMensaje = String.Format("La orden de compra ya fue registrada con el Folio :  {0}, verifique.", myLeer.Campo("Folio"));
                    CargarFoliosOC(ReferenciaDePedido, Captura); 
                    //General.msjUser(sMensaje); 
                }
                MarcarProductos_Ampuleo(); 
            }
            Consultas.MostrarMsjSiLeerVacio = true;
        }

        private void MarcarProductos_Ampuleo()
        {
            Color colorAmpuleo = Color.Yellow; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValueDou(i, (int)Cols.CantidadPrometidaCajas) != myGrid.GetValueDou(i, (int)Cols.CantidadPrometidaPiezas))
                {
                    myGrid.ColorCelda(i, (int)Cols.Descripcion, colorAmpuleo);
                    myGrid.ColorCelda(i, (int)Cols.CantidadPrometidaCajas, colorAmpuleo);
                    myGrid.ColorCelda(i, (int)Cols.CantidadPrometidaPiezas, colorAmpuleo);
                }
            }
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
                clsDescargarPedido f = new clsDescargarPedido(General.DatosConexion, sUrlServidorPedidosDeUnidades, sEmpresa, sEstado, sFarmacia, txtOrden.Text);

                if (!f.Descargar())
                {
                    txtOrden.Text = "";
                    txtOrden.Focus();
                }
                else
                {
                    CargarEncabezadoPedido(f.Encabezado);
                    CargarDetallesOrden(f.Detalles);
                    CargarLotesOrden(f.Lotes);

                    if (!f.OrdenGuardada)
                    {
                        IniciarToolBar(false, false, false, false, false);
                        General.msjAviso("Ocurrio un error, favor de reportarlo a sistemas.");
                    }
                }
            }
        }

        private bool SolicitarPedidoLocal()
        {
            bool bRegresa = true;

            clsLeer leerDatos = new clsLeer();
            DataSet dtsEncabezado = new DataSet();
            DataSet dtsDetalles = new DataSet();

            clsGetPedidoUnidad PedidosDeUnidad = new clsGetPedidoUnidad(General.DatosConexion, sEmpresa, sEstado, sFarmacia, txtOrden.Text);

            leerDatos.DataSetClase = PedidosDeUnidad.InformacionPedido();

            if (leerDatos.Tablas > 1) 
            {
                dtsEncabezado.Tables.Add(leerDatos.Tabla(PedidosDeUnidad.Encabezado).Copy());
                dtsDetalles.Tables.Add(leerDatos.Tabla(PedidosDeUnidad.Detalle).Copy());

                CargarEncabezadoPedido(dtsEncabezado);
                CargarDetallesOrden(dtsDetalles);
                CargarLotesOrden(dtsDetalles);
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

        private bool CargarEncabezadoPedido(DataSet Datos) 
        {
            bool bRegresa = false;
            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = Datos; 

            bRegresa = true;
            leerDatos.Leer(); 

            txtOrden.Text = leerDatos.Campo("Folio");
            sEstadoGenera_OC = leerDatos.Campo("IdEstado");
            sFarmaciaGenera_OC = leerDatos.Campo("IdFarmacia");

            txtReferenciaDocto.Enabled = false;
            txtReferenciaDocto.Text = leerDatos.Campo("ReferenciaFolioPedido");  

            dtpFechaPromesaEntrega.Value = leerDatos.CampoFecha("FechaRequeridaEntrega");
            txtIdProveedor.Text = leerDatos.Campo("IdProveedor");
            lblProveedor.Text = leerDatos.Campo("Proveedor");
            txtIdProveedor.Enabled = false;
            txtOrden.Enabled = false;

            bModificarCaptura = false;
            IniciarToolBar(true, false, false, true, true); 

            return bRegresa;
        }

        private void CargarDetallesOrden(DataSet Datos)
        {
            clsLeer leerDatos = new clsLeer();
            leerDatos.DataSetClase = Datos; 

            myGrid.Limpiar(false);
            if (leerDatos.Leer())
            {
                myGrid.LlenarGrid(leerDatos.DataSetClase);
                Totalizar();
            }

            myGrid.BackColorColsBlk = Color.WhiteSmoke; 
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                myGrid.BloqueaCelda(true, Color.WhiteSmoke, i, (int)Cols.CodEAN);
            }

            ////myGrid.BackColorColsBlk = Color.WhiteSmoke; 
            ////myGrid.BloqueaColumna(true, (int)Cols.CodEAN, true);
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

                myLlenaDatos.DataSetClase = Consultas.ND_LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, tpSubFarmacias, "CargarLotesCodigoEAN()");
                if (Consultas.Ejecuto)
                {
                    //////// Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    myLlenaDatos.Leer();
                    Lotes.AddLotes(myLlenaDatos.DataSetClase);

                    ////if (GnFarmacia.ManejaUbicaciones) 
                    ////{
                    ////    //Revisar myLlenaDatos.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
                    ////    if (Consultas.Ejecuto)
                    ////    {
                    ////        myLlenaDatos.Leer();
                    ////        Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
                    ////    }
                    ////}

                    ////mostrarOcultarLotes();
                    
                }
            }
        }

        private void CargarFoliosOC(string ReferenciaDePedido, bool Captura)
        {
            FrmFoliosPedidosUnidad FoliosOC;
            FoliosOC = new FrmFoliosPedidosUnidad();
            FoliosOC.MostrarPantalla(ReferenciaDePedido.Trim());
            sFolioEntrada = FoliosOC.Folio;

            if (sFolioEntrada.Trim() != "") 
            {
                txtFolio.Text = sFolioEntrada; 
                txtFolio_Validating(null, null);
            }
            else
            {
                if (!FoliosOC.CargarOC)
                {
                    if (Captura)
                    {
                        txtOrden.Text = "";
                        txtOrden.Focus();
                    }
                }
                else 
                {
                    SolicitarPedidoLocal();
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
                myLeer.DataSetClase = Consultas.ND_Pedidos_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    bFolioGuardado = true; 
                    IniciarToolBar(false, false, true, true, true);
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
            txtOrden.Text = myLeer.Campo("FolioPedidoReferencia");
            sEstadoGenera_OC = myLeer.Campo("EstadoGenera");
            sFarmaciaGenera_OC = myLeer.Campo("FarmaciaGenera");  

            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaFolioPedido");

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
            lblRecibida.Text = "RECIBIDO";
            lblRecibida.Visible = true;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;                
            }
            
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;

            myLlenaDatos.DataSetClase = Consultas.ND_Pedidos_Det(sEmpresa, sEstado, sFarmacia, sFolioOrden, "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            CargarDetallesLotesFolio();

            return bRegresa;                 
        }

        private void CargarDetallesLotesFolio()
        {
            myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes(); 
            myLlenaDatos.DataSetClase = Consultas.ND_Pedidos_Det_Lotes(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(myLlenaDatos.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                myLlenaDatos.DataSetClase = Consultas.ND_Pedidos_Det_Lotes__Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargarDetallesLotesFolio");
                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            }
        }

        #endregion Buscar Folio de Orden de Compra     

        #region Guardar/Actualizar Folio 
        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnRecepciones = btnRecepcionesOC.Enabled;
            bool bBtnImprimir_OC = btnImprimirOrdenDeCompra.Enabled;  
            bExceso = false;

            if (txtFolio.Text != "*")
            {
                MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                if (ValidaDatos())
                {
                    EliminarRenglonesVacios();
                    if (ValidarOrdenPedido())
                    {
                        if (!ConexionLocal.Abrir())
                        {
                            Error.LogError(ConexionLocal.MensajeError);
                            General.msjErrorAlAbriConexion(); 
                        }
                        else 
                        {
                            IniciarToolBar();
                            ConexionLocal.IniciarTransaccion();

                            if (Guarda_Encabezado_Orden()) 
                            {
                                //// if (GrabarEncabezado())
                                {
                                    bContinua = AfectarExistencia(true, true);
                                }
                            }

                            if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                            {
                                txtFolio.Text = sFolioOrden;
                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                // btnNuevo_Click(null, null);
                                IniciarToolBar(false, false, true, false, false);
                                ImprimirFolio();
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                if (!bExceso)
                                {
                                    txtFolio.Text = "*";
                                    Error.GrabarError(myLeer, "btnGuardar_Click");
                                    General.msjError("Ocurrio un error al guardar la informacion.");
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

        private bool ValidarOrdenPedido() 
        {
            bool bRegresa = true;

            if (!bEsNoSolicitado) 
            {
                VerificarCantidades = new clsVerificarCantidadesPedidoUnidad();
                bRegresa = VerificarCantidades.VerificarCantidadesConExceso(Lotes, txtReferenciaDocto.Text);
            }

            return bRegresa;
        }

        private bool Guarda_Encabezado_Orden()
        {
            bool bRegresa = true;
            string sSql = "", sFechaRegistro = General.FechaYMD(dtpFechaRegistro.Value, "-"); 
            string sFechaPromesa = General.FechaYMD(dtpFechaPromesaEntrega.Value, "-");
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsFacturaOriginal = 0;
            int iEsNoSolicitado = Convert.ToInt32(bEsNoSolicitado); 

            //fPorcSurtimiento = myGrid.TotalizarColumnaDou((int)Cols.PorcSurtimiento) / myGrid.Rows;
            if (chkEsFacturaOriginal.Checked)
            {
                iEsFacturaOriginal = 1;
            }

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_INT_ND_Pedidos_Recepcion_001_Enc " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @FolioPedidoReferencia = '{4}', @IdPersonal = '{5}', " + 
                " @FechaRegistro = '{6}', @IdProveedor = '{7}', @ReferenciaFolioPedido = '{8}', @Observaciones = '{9}', " +
                " @SubTotal = '{10}', @Iva = '{11}', @Total = '{12}', @FechaPromesaEntrega = '{13}', @EsNoSolicitado = '{14}', @iOpcion = '{15}' ", 
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, txtOrden.Text.Trim(),
                txtIdPersonal.Text, sFechaRegistro, txtIdProveedor.Text, txtReferenciaDocto.Text, 
                txtObservaciones.Text.Trim(),
                txtSubTotal.Text.Trim().Replace(",", ""), 
                txtIva.Text.Trim().Replace(",", ""), 
                txtTotal.Text.Trim().Replace(",", ""),
                sFechaPromesa, iEsNoSolicitado, iOpcion);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioOrden = myLeer.Campo("Clave");
                txtFolio.Text = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");

                if (MovtoInv_GrabarEncabezado())
                {
                    Guarda_Detalles_Orden(); 
                }
            }

            ////if (bRegresa)
            ////{
            ////    bRegresa = ValidarCantidadesOrdenCompra();
            ////}

            return bRegresa;
        }

        private bool Guarda_Detalles_Orden()
        {
            bool bRegresa = true;
            string sSql = "", sCodigoEAN = "", sIdProducto = "";
            int iRenglon = 0, iCantidadPrometidaCajas = 0, iCantidadRecibida = 0;
            double dCostoUnitario = 0, dTasaIva = 0, dSubTotal = 0, dImpteIva = 0,dImporte = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            //double dPorcentaje = 0.0000;
            
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sCodigoEAN =  myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidadPrometidaCajas = myGrid.GetValueInt( i, (int)Cols.CantidadPrometidaCajas);
                iCantidadRecibida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.Costo);
                dSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                dImpteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                dImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);
                //dPorcentaje = myGrid.GetValueDou(i, (int)Cols.PorcSurtimiento);
                iRenglon = i;

                if (sIdProducto != "" && iCantidadRecibida > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_INT_ND_Pedidos_Recepcion_002_Det " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', " + 
                        " @IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @CantidadPrometida = '{7}', @Cant_Recibida = '{8}', " + 
                        " @CostoUnitario = '{9}', @TasaIva = '{10}', @SubTotal = '{11}', @ImpteIva = '{12}', @Importe = '{13}', @iOpcion = '{14}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioOrden, sIdProducto, sCodigoEAN, iRenglon,
                        iCantidadPrometidaCajas, iCantidadRecibida, dCostoUnitario, dTasaIva, dSubTotal,
                        dImpteIva, dImporte, iOpcion);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!Guarda_Lotes_Orden(sIdProducto, sCodigoEAN, dCostoUnitario, i))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool Guarda_Lotes_Orden(string IdProducto, string CodigoEAN, double Costo, int Renglon)
        {
            bool bRegresa = true;
            string sSql = "";
            string sEstado = DtGeneral.EstadoConectado, sFarmacia = DtGeneral.FarmaciaConectada;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
            foreach (clsLotes L in ListaLotes)
            {
                if (IdProducto != "" && L.Cantidad > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_INT_ND_Pedidos_Recepcion_003_Lotes " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioPedido = '{4}', " + 
                        " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}', @CantidadRecibida = '{9}', @iOpcion = '{10}' ",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioOrden, IdProducto, CodigoEAN, L.ClaveLote, Renglon, L.Cantidad, iOpcion);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = Guarda_Lotes_Orden_Ubicaciones(L, iOpcion);
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool Guarda_Lotes_Orden_Ubicaciones(clsLotes Lote, int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_INT_ND_Pedidos_Recepcion_004_Ubicaciones " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioPedido = '{4}', " + 
                        " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', " + 
                        " @IdPasillo = '{8}', @IdEstante = '{9}', @IdEntrepaño = '{10}', @CantidadRecibida = '{11}', @iOpcion = '{12}' ",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioOrden, L.IdProducto, L.CodigoEAN,
                        L.ClaveLote, L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }


        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 

            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar) 
                Inv = AfectarInventario.Aplicar;

            // No Costo cuando es Promocion - Regalo, solo se afecta la existencia  
            if (!chkEsCompraPromocion.Checked)
            {
                if (AfectarCosto)
                    Costo = AfectarCostoPromedio.Afectar;
            }


            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' " + 
                "\n" +
                " Exec spp_INV_ActualizarCostoPromedio '{0}', '{1}', '{2}', '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);
            return myLeer.Exec(sSql);
            
        }

        private bool ValidarCantidadesOrdenCompra()
        {
            bool bRegresa = true;

            ////string sSql = string.Format(" Exec spp_ValidaCantidadesExcedidas '{0}', '{1}', '{2}', '{3}' ",
            ////                            sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtOrden.Text, 8));

            ////if (!myLeer.Exec(sSql))
            ////{
            ////    bRegresa = false;
            ////    General.msjError("Ocurrio un Error al Validar las Cantidades");
            ////}
            ////else
            ////{
            ////    if (myLeer.Leer())
            ////    {
            ////        bRegresa = false;
            ////        bExceso = true;
            ////        General.msjAviso("Se esta excediendo en la cantidad requeridad de los siguientes Productos.");
            ////        FrmProductosCantidadesExcedidas f = new FrmProductosCantidadesExcedidas(myLeer.DataSetClase);
            ////        f.ShowDialog();
            ////    }
            ////}

            return bRegresa;
        }
        #endregion Guardar/Actualizar Folio

        #region Grabar informacion Movimientos
        private bool MovtoInv_GrabarEncabezado()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, sFolioOrden, sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioMovto = myLeer.Campo("Folio");
                bRegresa = MovtoInv_GrabarDetalle();
            }

            return bRegresa;
        }

        private bool MovtoInv_GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                //iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "" && iCantidad > 0) 
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos '{0}', '{1}', '{2}', '{3}' " +
                                         "Exec spp_Mtto_FarmaciaProductos_CodigoEAN '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                         sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                             sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            nTasaIva, iCantidad, nCosto, nImporte, 'A');
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            if (!MovtoInv_GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto))
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool MovtoInv_GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                         sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            if (GnFarmacia.ManejaUbicaciones)
                            {
                                bRegresa = MovtoInv_GrabarDetalleLotesUbicaciones(L);
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool MovtoInv_GrabarDetalleLotesUbicaciones(clsLotes Lote)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                                         DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                                         L.Pasillo, L.Estante, L.Entrepano);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto,
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A');

                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }
        #endregion Grabar informacion Movimientos

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
                    General.msjError("Ocurrió un error al cargar el reporte.");
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

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir

        #region Folios de Entrada 
        private void btnRecepcionesOC_Click(object sender, EventArgs e)
        {
            VerificarPedidoDescargado(txtReferenciaDocto.Text, false); 
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
            
            //// No validar los Totales cuando es Promocion - Regalo 
            if (!chkEsCompraPromocion.Checked)
            {
                if (bRegresa && float.Parse(txtSubTotal.Text) <= 0)
                {
                    bRegresa = false;
                    General.msjUser("El SubTotal debe ser mayor a cero");
                    txtSubTotal.Focus();
                }
            }

            //if (float.Parse(txtIva.Text) <= 0 && bRegresa)
            //{
            //    bRegresa = false;
            //    General.msjUser("El Iva debe ser mayor a cero");
            //    txtIva.Focus();
            //}

            //////// No validar los Totales cuando es Promocion - Regalo 
            if (!chkEsCompraPromocion.Checked)
            {
                if (bRegresa && float.Parse(txtTotal.Text) <= 0)
                {
                    bRegresa = false;
                    General.msjUser("El Total debe ser mayor a cero");
                    txtTotal.Focus();
                }
            }

            ////////// No validar los Totales cuando es Promocion - Regalo 
            //if (!chkEsCompraPromocion.Checked)
            //{
            //    if (bRegresa && float.Parse(txtTotalFactura.Text) <= 0)
            //    {
            //        bRegresa = false;
            //        General.msjUser("El Total factura debe ser mayor a cero");
            //        txtTotalFactura.Focus();
            //    }
            //}

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (!ComparaTotales())
            {
                bRegresa = false;
                General.msjUser("El Subtotal, Iva y Total de la Factura no coinciden con el calculado por el sistema. \nEstos datos ha sido corregidos.");
                myGrid.ActiveRow = 1;
                grdProductos.Focus();
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
                //////////////// Jesús Díaz 2K120730.1110 
                ////////if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                ////////{
                ////////    bRegresa = false;
                ////////}
                ////////else
                ////////{
                ////////    if (Lotes.CantidadTotal == 0)
                ////////    {
                ////////        bRegresa = false;
                ////////    }
                ////////    else
                ////////    {
                ////////        for (int i = 1; i <= myGrid.Rows; i++)
                ////////        {
                ////////            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
                ////////            {
                ////////                bRegresa = false;
                ////////                break;
                ////////            }
                ////////        }
                ////////    }
                ////////}
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
                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

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
                //Revisar myLeer.DataSetClase = Ayuda.Folios_Compras(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown"); 

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
            if (e.KeyCode == Keys.F1)
            {
                // myLeer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");
                //Revisar myLlenaDatos.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");
                
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            myLlenaDatos = new clsLeer(ref ConexionLocal);

            if (txtIdProveedor.Text.Trim() != "")
            {
                //Revisar myLlenaDatos.DataSetClase = Consultas.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }
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
            dtpFechaVenceDocto.Value = dtpFechaRegistro.Value.AddMonths(1);
        }

        private void dtpFechaDocto_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaDocto_ValueChanged(null, null);
        }

        //private void chkEsCompraPromocion_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkEsCompraPromocion.Checked)
        //    {
        //        chkEsCompraPromocion.Enabled = false;
        //        //myGrid.BloqueaColumna(true, (int)Cols.Costo);
        //        myGrid.SetValue((int)Cols.Costo, 0);
        //    }
        //}
        #endregion Eventos  

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == (int)Cols.CodEAN)
            {
                if (e.KeyCode == Keys.F1)
                {
                    //myLeer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                    if (!bEsNoSolicitado)
                    {
                        myLeer.DataSetClase = Ayuda.ProductosPedido(sEmpresa, sEstado, txtReferenciaDocto.Text, "grdProductos_KeyDown");
                    }
                    else
                    {
                        myLeer.DataSetClase = AyudaGeneral.Productos("grdProductos_KeyDown");
                    }
                    
                    if (myLeer.Leer())
                    {
                        CargaDatosProducto();
                    }
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                removerLotes();
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblCancelado.Visible == false)
            {
                if ( !myGrid.BuscaRepetido( myLeer.Campo("CodigoEAN"), iRowActivo, 1) )
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));

                    // Cuando es Promocion Regalo el costo de entrada debe ser 0 
                    if (!chkEsCompraPromocion.Checked)
                    {
                        myGrid.SetValue(iRowActivo, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));
                    }
                    else
                    {
                        myGrid.SetValue(iRowActivo, (int)Cols.Costo, 0);
                    } 

                    myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo); 

                    CargarLotesCodigoEAN();
                }
                else
                {
                    General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                    myGrid.EnviarARepetido(); 
                }

            }

            // grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" )
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
                        }
                    }
                }
            }

        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1: // Si se cambia el Codigo, se limpian las columnas
                    {
                        limpiarColumnas();
                    }
                    break;
            }

        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sValor = "";
            bool bEsEAN_Unico = true;
            Cols colActiva = (Cols)myGrid.ActiveCol;

            switch (colActiva)
            {
                case Cols.CodEAN:                    
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

                        if (sValor != "")
                        {
                            if (EAN.EsValido(sValor))
                            {

                                if (!bEsNoSolicitado)
                                {
                                    myLeer.DataSetClase = Consultas.ND_ProductosPedido(sEmpresa, sEstado, txtOrden.Text, sValor, "grdProductos_EditModeOff");
                                }
                                else
                                {
                                    myLeer.DataSetClase = ConsultasGeneral.Productos(sValor, "grdProductos_EditModeOff");
                                }


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

            Totalizar(); 
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue( myGrid.ActiveRow, i, "");
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

        private bool ComparaTotales()
        {
            bool bIguales = false;
            double dSubTotalText = 0, dIvaText = 0, dTotalText = 0;
            double dSubTotalGrid = 0, dIvaGrid = 0, dTotalGrid = 0;            

            //Se obtienen los totales de los Textbox
            dSubTotalText = Math.Round(Convert.ToDouble(txtSubTotal.Text),4, MidpointRounding.ToEven);
            dIvaText = Math.Round(Convert.ToDouble(txtIva.Text), 4, MidpointRounding.ToEven);
            dTotalText = Math.Round(Convert.ToDouble(txtTotal.Text), 4, MidpointRounding.ToEven);

            //Se obtienen los totales del Grid.
            dSubTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.Importe), 4, MidpointRounding.ToEven);
            dIvaGrid = Math.Round( myGrid.TotalizarColumnaDou((int)Cols.ImporteIva), 4, MidpointRounding.ToEven);
            dTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal), 4, MidpointRounding.ToEven);

            // Se compara que sean iguales.
            if ((dSubTotalText == dSubTotalGrid) && (dIvaText == dIvaGrid) && (dTotalText == dTotalGrid))
            {
                bIguales = true;
            }
            else
            {
                txtSubTotal.Text = dSubTotalGrid.ToString(sFormato);
                txtIva.Text = dIvaGrid.ToString(sFormato);
                txtTotal.Text = dTotalGrid.ToString(sFormato);
            }
            
            return bIguales;
        }
        #endregion Grid       

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            
            string sCodigo = myGrid.GetValue(myGrid.ActiveRow,(int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

            myLeer.DataSetClase = Consultas.ND_LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, tpSubFarmacias, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                ////////// Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                ////myLeer.Leer();
                ////Lotes.AddLotes(myLeer.DataSetClase);

                ////if (GnFarmacia.ManejaUbicaciones)
                ////{
                ////    //////Revisar myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
                ////    if (Consultas.Ejecuto)
                ////    {
                ////        myLeer.Leer();
                ////        Lotes.AddLotesUbicaciones(myLeer.DataSetClase);
                ////    }
                ////}

                mostrarOcultarLotes();
            }
            
        }

        private void removerLotes()
        {
            if (!bFolioGuardado)
            {
                try
                {
                    int iRow = myGrid.ActiveRow;
                    Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                    myGrid.DeleteRow(iRow);
                }
                catch { }

                if (myGrid.Rows == 0)
                {
                    myGrid.Limpiar(true);
                }

                Totalizar(); 
            }
        }

        private void mostrarOcultarLotes()
        {
            int iCantPrometida = 0;
            // Asegurar que el Grid tenga el Foco.
            if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                int iRow = myGrid.ActiveRow;

                if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    iCantPrometida = myGrid.GetValueInt(iRow, (int)Cols.CantidadPrometidaCajas);

                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);//Codigo
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
                    Lotes.EsEntrada = true;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    Lotes.PermitirLotesNuevosConsignacion = true; 

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bModificarCaptura; //true; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCaptura; //true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default; 

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    //Se envia la cantidad prometida del medicamento.
                    //Lotes.Cantidad = myGrid.GetValueInt(iRow, (int)Cols.CantidadPrometidaCajas);

                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);

                    myGrid.SetValue(iRow, (int)Cols.CantidadPrometidaCajas, iCantPrometida);
                    //myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Costo);

                    Totalizar();
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes 

        #region Funciones
        public void MostrarOrdenCompra(string Empresa, string Estado, string Farmacia, string Folio)
        {
            this.bEsConsultaExterna = true;
            Fg.IniciaControles(this, true);

            IniciarToolBar(false, false, false, false, false);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
            Lotes.ManejoLotes = OrigenManejoLotes.Compras;


            sEmpresa = Empresa;
            sEstado = Estado;
            sFarmacia = Farmacia;
            txtFolio.Text = Folio;
            txtFolio_Validating(null, null);
            this.ShowDialog();
            // bEsConsultaExterna = false; 
        }

        private void Totalizar()
        {
            txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString();
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString();
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString();
        }
        #endregion Funciones 

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this); 
        }
        
    } // Llaves de la Clase
} // Llaves del NameSpace
