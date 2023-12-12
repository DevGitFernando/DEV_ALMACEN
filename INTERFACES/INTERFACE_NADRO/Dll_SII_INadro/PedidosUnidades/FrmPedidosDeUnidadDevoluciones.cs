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
using DllFarmaciaSoft.Devoluciones;

namespace Dll_SII_INadro.PedidosUnidades 
{
    public partial class FrmPedidosDeUnidadDevoluciones : FrmBaseExt 
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
        clsCodigoEAN EAN = new clsCodigoEAN();

        TiposDeInventario tpInventarioModulo = TiposDeInventario.Venta;

        clsDatosCliente DatosCliente;
        
        clsDevoluciones Dev;
        TipoDevolucion tpDevolucion = TipoDevolucion.OrdenCompra;

        
        bool bEsConsultaExterna = false; 
        
        string sFolioOrden = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;        
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");        
        string sFormato = "#,###,##0.###0";        
        bool bFolioGuardado = false;
        

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, CantidadPrometidaCajas = 5, CantidadPrometidaPiezas = 6,
            Cantidad = 7, CantDev = 8, Costo = 9, Importe = 10, ImporteIva = 11, ImporteTotal = 12, PorcSurtimiento = 13, 
            CantidadPrometidaCajasRecibida = 14
        }

        #region Constructor 
        public FrmPedidosDeUnidadDevoluciones()
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
            
            myLeer = new clsLeer(ref ConexionLocal); 
            myLlenaDatos = new clsLeer(ref ConexionLocal); 
            myLeerLotes = new clsLeer(ref ConexionLocal);            

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        #endregion Constructor
        
        private void FrmOrdenesDeCompraDevoluciones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this,null);             
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
            
            bFolioGuardado = true;
            IniciarToolBar(false, false, true);
            lblRecibida.Text = "RECIBIDA";
            lblRecibida.Visible = false;


            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                IniciarToolBar(false, false, false);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
                Lotes.ManejoLotes = OrigenManejoLotes.OrdenesDeCompra;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                dtpFechaPromesaEntrega.Enabled = false;
                dtpFechaVenceDocto.Enabled = false;
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

                myGrid.Limpiar(true);
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
        }                       
        #endregion Buscar Orden de Compra

        #region Buscar Folio de Orden de Compra 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                //Revisar myLeer.DataSetClase = Consultas.OrdenesCompras_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    bFolioGuardado = true; 
                    IniciarToolBar(true, false, true);
                    
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
            }

            if (!bContinua)
                txtFolio.Focus();
            
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
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");

            //chkEsCompraPromocion.Checked = myLeer.CampoBool("EsPromocionRegalo"); 

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
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
            bool bRegresa = false;

            //Revisar myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioOrden, "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            CargarDetallesLotesFolio();

            return bRegresa;                 
        }

        private void CargarDetallesLotesFolio()
        {
            myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes();
            //Revisar myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Lotes_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(myLlenaDatos.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                //Revisar myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Lotes_Ubicaciones_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioOrden, "CargarDetallesLotesFolio");
                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            }
        }

        #endregion Buscar Folio de Orden de Compra     

        #region Guardar/Actualizar Folio 
        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
           

            EliminarRenglonesVacios();
            if (ValidaDatos())
            {
                IniciarToolBar();
                Dev = new clsDevoluciones(sEmpresa, sEstado, sFarmacia, ConexionLocal.DatosConexion);

                // Agregar los datos 
                Dev.Folio = "*";
                //Dev.FolioCompra = txtFolio.Text;
                Dev.Tipo = tpDevolucion;
                Dev.Referencia = txtFolio.Text;
                Dev.FechaOperacionDeSistema = GnFarmacia.FechaOperacionSistema;
                Dev.IdPersonal = DtGeneral.IdPersonal;
                Dev.Observaciones = txtObservaciones.Text;
                Dev.SubTotal = Convert.ToDouble(txtSubTotal.Text);
                Dev.Iva = Convert.ToDouble(txtIva.Text);
                Dev.Total = Convert.ToDouble(txtTotal.Text);

                // Agregar los Productos 
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    clsProducto P = new clsProducto();
                    P.IdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                    P.CodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                    P.Unidad = 1;
                    P.TasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                    P.Cantidad = myGrid.GetValueDou(i, (int)Cols.CantDev);
                    P.Valor = myGrid.GetValueDou(i, (int)Cols.Costo);
                    Dev.AddProducto(P);
                }

                // Agregar los Lotes 
                Dev.Lotes = Lotes;

                if (Dev.Guardar())
                {
                    ImprimirFolio();
                    btnNuevo_Click(null, null);
                }
                else
                {
                    IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                }
            }

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
        #endregion Imprimir

        #region Folios de Entrada 
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            FrmDevolucionesImpresion Devoluciones;
            Devoluciones = new FrmDevolucionesImpresion();
            Devoluciones.MostrarPantalla(txtFolio.Text.Trim(), tpDevolucion, (int)TipoDeVenta.Ninguna);
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

            // No validar los Totales cuando es Promocion - Regalo 
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

            // No validar los Totales cuando es Promocion - Regalo 
            if (!chkEsCompraPromocion.Checked)
            {
                if (bRegresa && float.Parse(txtTotal.Text) <= 0)
                {
                    bRegresa = false;
                    General.msjUser("El Total debe ser mayor a cero");
                    txtTotal.Focus();
                }
            }

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
                if (myGrid.TotalizarColumna((int)Cols.CantDev) == 0)
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para la Devolución de Orden de Compra\n y/o capturar cantidades para al menos un lote, verifique.");
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
                //Revisar myLeer.DataSetClase = Ayuda.Folios_Compras(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown");                

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                    CargaDetallesFolio();
                }                
            }

        }        

        private void dtpFechaDocto_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaVenceDocto.Value = dtpFechaRegistro.Value.AddMonths(1);
        }

        private void dtpFechaDocto_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaDocto_ValueChanged(null, null);
        }        
        #endregion Eventos  

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ////if (myGrid.ActiveCol == 1)
            ////{
            ////    if (e.KeyCode == Keys.F1)
            ////    {
            ////        //myLeer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
            ////        myLeer.DataSetClase = Ayuda.ProductosOrdenCompra(sEmpresa, sEstado, txtOrden.Text, "grdProductos_KeyDown");
            ////        if (myLeer.Leer())
            ////        {
            ////            CargaDatosProducto();
            ////        }
            ////    }
            ////}

            ////if (e.KeyCode == Keys.Delete) 
            ////    removerLotes(); 
        }

        private void CargaDatosProducto()
        {
            ////int iRowActivo = myGrid.ActiveRow;

            ////if (lblCancelado.Visible == false)
            ////{
            ////    if ( !myGrid.BuscaRepetido( myLeer.Campo("CodigoEAN"), iRowActivo, 1) )
            ////    {
            ////        myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
            ////        myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
            ////        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));

            ////        // Cuando es Promocion Regalo el costo de entrada debe ser 0 
            ////        if (!chkEsCompraPromocion.Checked)
            ////        {
            ////            myGrid.SetValue(iRowActivo, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));
            ////        }
            ////        else
            ////        {
            ////            myGrid.SetValue(iRowActivo, (int)Cols.Costo, 0);
            ////        } 

            ////        myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
            ////        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
            ////        myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo);

            ////        // Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
            ////        if (IMach4.EsClienteIMach4)
            ////        {
            ////            GnFarmacia.ValidarCodigoIMach4(myGrid, myLeer.CampoBool("EsMach4"), iRowActivo);
            ////        }

            ////        CargarLotesCodigoEAN();
            ////    }
            ////    else
            ////    {
            ////        General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
            ////        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
            ////        limpiarColumnas();
            ////        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
            ////        myGrid.EnviarARepetido(); 
            ////    }

            ////}

            // grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            ////if (!bFolioGuardado)
            ////{
            ////    if (lblCancelado.Visible == false)
            ////    {
            ////        if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            ////        {
            ////            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" )
            ////            {
            ////                myGrid.Rows = myGrid.Rows + 1;
            ////                myGrid.ActiveRow = myGrid.Rows;
            ////                myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
            ////            }
            ////        }
            ////    }
            ////}

        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            ////switch (myGrid.ActiveCol)
            ////{
            ////    case 1: // Si se cambia el Codigo, se limpian las columnas
            ////        {
            ////            limpiarColumnas();
            ////        }
            ////        break;
            ////}

        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ////string sValor = "";

            ////switch (myGrid.ActiveCol)
            ////{
            ////    case 1:                    
            ////        {
            ////            sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

            ////            if (sValor != "")
            ////            {
            ////                if (EAN.EsValido(sValor))
            ////                {
            ////                    ////myLeer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
            ////                    myLeer.DataSetClase = Consultas.ProductosOrdenCompra(sEmpresa, sEstado, txtOrden.Text, sValor, "grdProductos_EditModeOff");
            ////                    if (myLeer.Leer())
            ////                    {
            ////                        CargaDatosProducto();
            ////                    }
            ////                    else
            ////                    {
            ////                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
            ////                        myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
            ////                    }
            ////                }
            ////                else
            ////                {
            ////                    //General.msjError(sMsjEanInvalido);
            ////                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
            ////                    myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
            ////                    SendKeys.Send("");
            ////                }
            ////            }
            ////        }

            ////        break;                
            ////} 
        }

        private void limpiarColumnas()
        {
            ////for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            ////{
            ////    myGrid.SetValue( myGrid.ActiveRow, i, "");
            ////} 
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

            //Revisar myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                myLeer.Leer();
                Lotes.AddLotes(myLeer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    //Revisar myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, "CargarLotesCodigoEAN()");
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
                }
                catch { }

                if (myGrid.Rows == 0)
                    myGrid.Limpiar(true);
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

                    Lotes.PermitirLotesNuevosConsignacion = false;

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucionDeOrdenesDeCompras;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    //Se envia la cantidad prometida del medicamento.
                    //Lotes.Cantidad = myGrid.GetValueInt(iRow, (int)Cols.CantidadPrometidaCajas);

                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.CantDev, Lotes.Cantidad);

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

            IniciarToolBar(false, false, false);
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
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString();
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString();
        }
        #endregion Funciones
        
    } // Llaves de la Clase
} // Llaves del NameSpace
