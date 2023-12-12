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
//using Dll_IMach4;
using DllRobotDispensador; 

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmPedidosDistribuidor : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;
        DllFarmaciaSoft.clsAyudas Ayuda;

        clsLeerWebExt leerWeb;
        clsLeer leerPedido;
        clsLeer leer;
        TiposDeInventario tpInventarioModulo = TiposDeInventario.Venta; 

        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        clsGrid myGrid;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        bool bEsClienteInterface = RobotDispensador.Robot.EsClienteInterface;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();

        bool bEsConsultaExterna = false; 
        bool bContinua = true;
        bool bModificarCaptura = true;
        string sFolioCompra = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovto = "";

        string sIdTipoMovtoInv = "EPD";
        string sTipoES = "E";
        string sFormato = "#,###,##0.###0";

        bool bFolioGuardado = false;

        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        string sUrlAlmacenRegional = GnFarmacia.UrlAlmacenRegional;
        string sHostAlmacenRegional = GnFarmacia.HostAlmacenRegional;
        string sIdFarmaciaAlmacenRegional = GnFarmacia.IdFarmaciaAlmacenRegional;

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            PrecioMaxPublico = 6,
            Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        }


        public FrmPedidosDistribuidor()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);

            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leerPedido = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);

            cnnRegional = new clsConexionSQL();
            cnnRegional.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnnRegional.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnnRegional.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnnRegional.DatosConexion.Password = General.DatosConexion.Password;
            cnnRegional.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref ConexionLocal, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmPedidosDistribuidor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this,null);             
        }

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

        #region Limpiar 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void LimpiarPantalla()
        {
            bModificarCaptura = false;
            bFolioGuardado = true;
            IniciarToolBar(false, false, false);
            myGrid.EstiloGrid(eModoGrid.Normal); 

            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                //dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
                Lotes.ManejoLotes = OrigenManejoLotes.Compras;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                //dtpFechaVenceDocto.Enabled = false;
                //dtpFechaDocto.Enabled = true;

                txtSubTotal.Text = "0.0000";
                txtIva.Text = "0.0000";
                txtTotal.Text = "0.0000";

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.Limpiar(false);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                myGrid.BloqueaColumna(false, (int)Cols.Costo);


                // myGrid.Limpiar(true);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                // Asegurarnos de que la fecha maxima sea la del sistema, para evitar errores 
                //dtpFechaDocto.MinDate = General.FechaSistema.AddMonths(-1);
                //dtpFechaDocto.MaxDate = General.FechaSistema;
                dtsPedido = null;            
                dtsPedido = PreparaDtsPedidos();
                txtFolio.Focus();                
            }
        }
        #endregion Limpiar

        #region Buscar Folio 
        ////public void MostrarFolioCompra(string Empresa, string Estado, string Farmacia, string Folio )
        ////{
        ////    this.bEsConsultaExterna = true; 
        ////    IniciarToolBar(false, false, false);

        ////    Fg.IniciaControles(this, true);
        ////    grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
        ////    //dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
        ////    Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
        ////    Lotes.ManejoLotes = OrigenManejoLotes.Compras;


        ////    sEmpresa = Empresa;
        ////    sEstado = Estado;
        ////    sFarmacia = Farmacia; 
        ////    txtFolio.Text = Folio;
        ////    txtFolio_Validating(null, null);
        ////    btnNuevo.Enabled = false; 

        ////    this.ShowDialog();
        ////    // bEsConsultaExterna = false; 
        ////}

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            bModificarCaptura = true;
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false);
            myGrid.Limpiar(false); 

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_PedidosEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), sIdTipoMovtoInv, "txtFolio_Validating");
                if (!myLeer.Leer())
                {
                    bContinua = false;
                    txtFolio.Text = ""; 
                }
                else
                {
                    bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    bModificarCaptura = false;
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }
                    //else
                    //    Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.                    
                }
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
            
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioCompra = txtFolio.Text;
            txtIdProveedor.Text = myLeer.Campo("IdDistribuidor");
            lblProveedor.Text = myLeer.Campo("Distribuidor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaPedido");           

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");            
            txtIdPersonal.Enabled = false; 
            txtIdPersonal.Text = myLeer.Campo("IdPersonal");
            lblPersonal.Text = myLeer.Campo("NombrePersonal"); 

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;                
            }
            
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;

            myLlenaDatos.DataSetClase = Consultas.Folio_PedidosDet(sEmpresa, sEstado, sFarmacia, sFolioCompra, "txtFolio_Validating");
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
            myGrid.EstiloGrid(eModoGrid.ModoRow); 

            return bRegresa;                 
        }

        private void CargarDetallesLotesFolio()
        {
            myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes();
            myLlenaDatos.DataSetClase = Consultas.Folio_PedidosDet_Lotes(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(myLlenaDatos.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                myLlenaDatos.DataSetClase = Consultas.FolioDetLotes_Pedidos_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolio");
                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            }
        }

        #endregion Buscar Folio

        #region Grabar informacion
        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, "",
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
                bRegresa = GrabarDetalle();
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
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
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
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
                            if (!GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto))
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

        private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
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
                                bRegresa = GrabarDetalleLotesUbicaciones(L);
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotesUbicaciones(clsLotes Lote)
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
        #endregion Grabar informacion

        #region Grabar_Informacion_Regional

        public static DataSet PreparaDtsPedidos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Sentencias");

            dtClave.Columns.Add("Query", Type.GetType("System.String"));            
            dts.Tables.Add(dtClave);

            return dts.Clone();
        }

        private void InsertarQuerys(string sQuery)
        {
            object[] x = { sQuery };
            dtsPedido.Tables[0].Rows.Add(x);
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlAlmacenRegional, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlAlmacenRegional;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHostAlmacenRegional;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(myLeer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                
            }

            return bRegresa;
        }

        private bool GrabarPedidoDistribuidorRegional()
        {
            bool bRegresa = true;
            bool bContinua = true;
            string sQuery = "";

            if (validarDatosDeConexion())
            {
                cnnRegional = new clsConexionSQL(DatosDeConexion);
                cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnRegional.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leerPedido = new clsLeer(ref cnnRegional);

                if (cnnRegional.Abrir())
                {
                    IniciarToolBar();
                    cnnRegional.IniciarTransaccion();

                    leer.DataSetClase = dtsPedido;

                    while (leer.Leer())
                    {
                        sQuery = leer.Campo("Query");

                        if (!leerPedido.Exec(sQuery))
                        {                            
                            bContinua = false;
                            break;
                        }
                    }
                    
                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnnRegional.CompletarTransaccion();                        
                    }
                    else
                    {
                        bRegresa = false;
                        cnnRegional.DeshacerTransaccion();
                    }

                    cnnRegional.Cerrar();
                }
                else
                {
                    Error.LogError(cnnRegional.MensajeError);
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
            else
            {
                bRegresa = false;
            }
            return bRegresa;

        }
        #endregion Grabar_Informacion_Regional

        #region Guardar/Actualizar Folio

        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled; 

            ////if (txtFolio.Text != "*") 
            ////{
            ////    MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            ////}
            ////else
            {
                EliminarRenglonesVacios();
                if (validaDatos())
                {
                    if (ConexionLocal.Abrir())
                    {
                        IniciarToolBar(); 
                        ConexionLocal.IniciarTransaccion();

                        if (GrabarEncabezado())
                        {
                            if (Guarda_Encabezado_Pedido())
                            {
                                bContinua = AfectarExistencia(true, false);
                            }
                        }                       

                        if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolioCompra;
                            ConexionLocal.CompletarTransaccion(); 
                            //////if (GrabarPedidoDistribuidorRegional())
                            //////{
                            //////    ActualizarPedidosDistribuidor();
                            //////}
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            // btnNuevo_Click(null, null);
                            IniciarToolBar(false, false, true);
                            ImprimirCompra(true);
                        }
                        else
                        {
                            txtFolio.Text = "*"; 
                            ConexionLocal.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                            //btnNuevo_Click(null, null);

                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        Error.LogError(ConexionLocal.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }

                }
            }

        }

        private bool Guarda_Encabezado_Pedido()
        {
            bool bRegresa = true;
            string sSql = "", sQuery = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_PedidosEnc " + 
                                "'{0}', '{1}', '{2}', " + 
                                "'{3}', '{4}', '{5}', " + 
                                "'{6}', '{7}', '{8}', " +
                                "'{9}', '{10}', '{11}', '{12}', '{13}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sFolioMovto, 
                            txtIdPersonal.Text, txtIdProveedor.Text, txtReferenciaDocto.Text, 
                            txtObservaciones.Text.Trim(),
                            txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""),
                            txtTotal.Text.Trim().Replace(",", ""), iOpcion);            

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioCompra = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");
                sQuery = sSql.Replace("spp_Mtto_PedidosEnc", "spp_Mtto_PedidosEnvioEnc");
                sQuery = sQuery.Replace("*", sFolioCompra);
                InsertarQuerys(sQuery); 
                bRegresa = Guarda_Detalles_Pedido();
            }

            return bRegresa;
        }

        private bool Guarda_Detalles_Pedido()
        {
            bool bRegresa = true;
            string sSql = "", sCodigoEAN = "", sIdProducto = "", sQuery = "";
            // string sEstado = DtGeneral.EstadoConectado, sFarmacia = DtGeneral.FarmaciaConectada;
            int iUnidadDeEntrada = 0, iRenglon = 0, iCantidadRecibida = 0;
            double dCostoUnitario = 0, dTasaIva = 0, dSubTotal = 0, dImpteIva = 0,dImporte = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sCodigoEAN =  myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidadRecibida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.Costo);
                dSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                dImpteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                dImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);

                iUnidadDeEntrada = 1; //Este dato es Temporal ya que obtendra valor con la Clase.                    
                iRenglon = i;

                if (sIdProducto != "" && iCantidadRecibida > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_PedidosDet " +
                                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " + 
                                "'{6}', '{7}', '{8}', '{9}', '{10}', " +
                                "'{11}','{12}', '{13}', '{14}' ", 
                            sEmpresa, sEstado, sFarmacia, sFolioCompra, sIdProducto, sCodigoEAN, iRenglon,
                            iUnidadDeEntrada, iCantidadRecibida, dCostoUnitario, dTasaIva, dSubTotal,
                            dImpteIva, dImporte, iOpcion);

                    sQuery = sSql.Replace("spp_Mtto_PedidosDet", "spp_Mtto_PedidosEnvioDet");
                    InsertarQuerys(sQuery);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!Guarda_Lotes_Pedido(sIdProducto, sCodigoEAN, dCostoUnitario, i))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool Guarda_Lotes_Pedido(string IdProducto, string CodigoEAN, double Costo, int Renglon)
        {
            bool bRegresa = true;
            string sSql = "", sQuery = "", sSentencia = ""; 
            string sEstado = DtGeneral.EstadoConectado, sFarmacia = DtGeneral.FarmaciaConectada;
            
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
            foreach (clsLotes L in ListaLotes)
            {
                if (IdProducto != "" && L.Cantidad > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_PedidosDet_Lotes " +
                                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioCompra, IdProducto, CodigoEAN, L.ClaveLote,
                            Renglon, L.Cantidad, iOpcion);

                    sQuery = sSql.Replace("spp_Mtto_PedidosDet_Lotes", "spp_Mtto_PedidosEnvioDet_Lotes");
                    InsertarQuerys(sQuery);

                    // Registrar el producto en las tablas de existencia 
                    sSentencia = string.Format("Exec spp_Mtto_PedidosEnvioDet_Lotes_Registrar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                         sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioCompra, IdProducto, CodigoEAN, L.ClaveLote,
                                         Renglon, L.Cantidad, General.FechaYMD(L.FechaCaducidad, "-"), iOpcion);

                    InsertarQuerys(sSentencia);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GuardaPedidosDet_Lotes_Ubicaciones(L, iOpcion, Renglon);
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GuardaPedidosDet_Lotes_Ubicaciones(clsLotes Lote, int iOpcion, int Renglon)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_PedidosDet_Lotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                         " '{9}', '{10}', '{11}', '{12}', '{13}' ",
                                         DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioCompra, L.IdProducto, L.CodigoEAN,
                                         L.ClaveLote, Renglon.ToString(), L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ActualizarPedidosDistribuidor()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Update PedidosEnc Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n " +
                                " Update PedidosDet Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n " +
                                " Update PedidosDet_Lotes Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}'  ",
                                sEmpresa, sEstado, sFarmacia, sFolioCompra);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ActualizarPedidosDistribuidor");
                General.msjError("Ocurrió un error al Actualizar la información.");
                bRegresa = false;
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

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' " + 
                "\n" +
                " Exec spp_INV_ActualizarCostoPromedio '{0}', '{1}', '{2}', '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bool bRegresa = myLeer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioMovto);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(ConexionLocal, sFolioMovto);
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
        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                    LimpiarPantalla();
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Entrada inválido, verifique.");
                    txtFolio.Focus(); 
                }
            }

            return bRegresa;
        }

        private void ImprimirCompra(bool Confirmar)
        {
            bool bRegresa = false;

            if (validarImpresion(Confirmar))
            {
                DatosCliente.Funcion = "ImprimirCompra()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Pedidos_Distribuidor.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true); 
                ////}
                ////else 
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar; 
                ////}


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
            ImprimirCompra(false);
        }
        #endregion Imprimir

        #region Validaciones de Controles
        private bool validaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";
                        
            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Compra inválido, verifique.");
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

            ////////////No validar los Totales cuando es Promocion - Regalo 
            //////if (!chkEsCompraPromocion.Checked) 
            //{ 
            //    if (bRegresa && float.Parse(txtSubTotal.Text) <= 0) 
            //    { 
            //        bRegresa = false; 
            //        General.msjUser("El SubTotal debe ser mayor a cero"); 
            //        txtSubTotal.Focus(); 
            //    } 
            //} 

            //if (float.Parse(txtIva.Text) <= 0 && bRegresa)
            //{
            //    bRegresa = false;
            //    General.msjUser("El Iva debe ser mayor a cero");
            //    txtIva.Focus();
            //}

            // No validar los Totales cuando es Promocion - Regalo 
            //if (!chkEsCompraPromocion.Checked)
            //{
            //    if (bRegresa && float.Parse(txtTotal.Text) <= 0)
            //    {
            //        bRegresa = false;
            //        General.msjUser("El Total debe ser mayor a cero");
            //        txtTotal.Focus();
            //    }
            //}

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            ////if (!ComparaTotales(true))
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("El Subtotal, Iva y Total de la Entrada no coinciden con el calculado por el sistema. \nEstos datos ha sido corregidos.");
            ////    myGrid.ActiveRow = 1;
            ////    grdProductos.Focus();
            ////}
            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un pedido de distribuidor, verifique por favor.";
                ///bRegresa = opPermisosEspeciales.VerificarPermisos("PEDIDO_DISTRIBUIDOR", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("PEDIDO_DISTRIBUIDOR", sMsjNoEncontrado);
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
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    if (Lotes.CantidadTotal == 0)
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para la compra\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;

        }

        private bool validarCantidadesCapturadas()
        {
            bool bRegresa = true;
            DataSet dtsProductosDiferencias = clsLotes.PreparaDtsRevision();
            // string sSql = "", 
            string sIdProducto = "", sCodigoEAN = "", sDescripcion = "";
            // int iRenglon = 0, 
            int iCantidad = 0, iCantidadLotes = 0;

            for (int i = 1; i < myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sDescripcion = myGrid.GetValue(i, (int)Cols.Descripcion);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                iCantidadLotes = 0;
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);
                foreach (clsLotes L in ListaLotes)
                {
                    iCantidadLotes += L.Cantidad;
                }

                if (iCantidad != iCantidadLotes)
                {
                    bRegresa = false;
                    object[] obj = { sIdProducto, sCodigoEAN, sDescripcion, iCantidad, iCantidadLotes };
                    dtsProductosDiferencias.Tables[0].Rows.Add(obj);
                }
            }

            // Se encontraron diferencias 
            if (!bRegresa)
            {
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Compra no puede ser completada.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Checar por incongruencias 
                myLeer.DataSetClase = Ayuda.Folios_Pedidos(sEmpresa, sEstado, sFarmacia, sIdTipoMovtoInv, "txtFolio_KeyDown");
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
            if (e.KeyCode == Keys.F1)
            {
                // myLeer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");
                myLlenaDatos.DataSetClase = Ayuda.Distribuidores("txtIdProveedor_KeyDown");
                
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
                myLlenaDatos.DataSetClase = Consultas.Distribuidores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLlenaDatos.Leer())
                {
                    CargaDatosProveedor();
                    myGrid.Limpiar(true);
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
                txtIdProveedor.Text = myLlenaDatos.Campo("IdDistribuidor");
                lblProveedor.Text = myLlenaDatos.Campo("NombreDistribuidor"); 
            }
            else
            {
                General.msjUser("El Distribuidor " + myLlenaDatos.Campo("NombreDistribuidor") + " actualmente se encuentra cancelado, verifique. "); 
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
            //dtpFechaVenceDocto.Value = dtpFechaRegistro.Value.AddMonths(1);
        }

        private void dtpFechaDocto_Validating(object sender, CancelEventArgs e)
        {
            //dtpFechaDocto_ValueChanged(null, null);
        }
        #endregion Eventos  

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        CargaDatosProducto();
                    }
                }
            }

            if (e.KeyCode == Keys.Delete) 
                removerLotes(); 
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
                    myGrid.SetValue(iRowActivo, (int)Cols.PrecioMaxPublico, myLeer.Campo("PrecioMaxPublico"));

                    // Cuando es compra Promocion Regalo el costo de entrada debe ser 0 
                    //if (!chkEsCompraPromocion.Checked)
                    {
                        myGrid.SetValue(iRowActivo, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));
                    }
                    

                    myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo);

                    //// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    if (bEsClienteInterface)
                    {
                        GnFarmacia.ValidarCodigoIMach4(myGrid, myLeer.CampoBool("EsMach4"), iRowActivo);
                    }

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

                            // Bloquear la celda de Costo cuando se Promocion - Regalo 
                            //if (chkEsCompraPromocion.Checked)
                                //myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols.Costo);

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

            switch (myGrid.ActiveCol)
            {
                case 1:                    
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

                        if (sValor != "")
                        {
                            if (EAN.EsValido(sValor))
                            {
                                myLeer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
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
                                    ////CargaDatosProducto();
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

            // ComparaTotales(true);
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
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private bool ComparaTotales()
        {
            return ComparaTotales(false);
        }

        private bool ComparaTotales(bool MostrarDatos)
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
                if (MostrarDatos)
                {
                    txtSubTotal.Text = dSubTotalGrid.ToString(sFormato);
                    txtIva.Text = dIvaGrid.ToString(sFormato);
                    txtTotal.Text = dTotalGrid.ToString(sFormato);
                } 
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
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                myLeer.Leer();
                Lotes.AddLotes(myLeer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, false, "CargarLotesCodigoEAN()");
                    if (Consultas.Ejecuto)
                    {
                        myLeer.Leer();
                        Lotes.AddLotesUbicaciones(myLeer.DataSetClase);
                    }
                }

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
                    ComparaTotales(true);
                }
                catch { }

                if (myGrid.Rows == 0)
                    myGrid.Limpiar(true);
            }
        }

        private void mostrarOcultarLotes()
        {
            // Asegurar que el Grid tenga el Foco.
            if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                int iRow = myGrid.ActiveRow;

                if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);//Codigo
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
                    Lotes.EsEntrada = true;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    Lotes.PermitirLotesNuevosConsignacion = false; 

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bModificarCaptura; //true; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCaptura; //true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Costo);

                    //Totalizar();
                    ComparaTotales(true); 
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes 

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void chkEsCompraPromocion_CheckedChanged(object sender, EventArgs e)
        {

        }        

    } // Llaves de la Clase
} // Llaves del NameSpace
