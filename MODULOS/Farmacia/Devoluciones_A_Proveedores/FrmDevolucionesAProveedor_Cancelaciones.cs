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

namespace Farmacia.Devoluciones_A_Proveedores
{
    public partial class FrmDevolucionesAProveedor_Cancelaciones : FrmBaseExt
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
        clsDevoluciones Dev;

        TipoDevolucion tpDevolucion = TipoDevolucion.Dev_Proveedor;

        // bool bContinua = true;
        string sFolioCompra = ""; // , sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";

        // string sIdTipoMovtoInv = "EC";
        // string sTipoES = "E";

        clsMotivosDevoluciones motivodev;

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, CantDev = 6,
            Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        }

        public FrmDevolucionesAProveedor_Cancelaciones()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;


            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);
        }

        private void FrmDevolucionesAProveedor_Cancelaciones_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
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
                        InicializarPantalla();
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

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }

        private void InicializarPantalla() 
        {
            IniciarToolBar(false, false, false); 
            Fg.IniciaControles(this, false);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

            Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Venta);
            Lotes.ManejoLotes = OrigenManejoLotes.Compras;

            // Estos campos deben ir deshabilitados son campos controlados 
            dtpFechaRegistro.Enabled = false;
            dtpFechaVenceDocto.Enabled = false;
            dtpFechaDocto.Enabled = false;

            txtSubTotal.Text = "0.0000";
            txtIva.Text = "0.0000";
            txtTotal.Text = "0.0000";

            myGrid.Limpiar(true);
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;
            txtIdPersonal.Enabled = false;

            btnMotivosDev.Enabled = false;
            motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);

            //// Asegurarnos de que la fecha maxima sea la del sistema, para evitar errores 
            //dtpFechaDocto.MinDate = General.FechaSistema.AddMonths(-1);
            //dtpFechaDocto.MaxDate = General.FechaSistema;

            btnEjecutar.Enabled = false;
            txtObservaciones.Enabled = true;
            txtFolio.Enabled = true;
            txtFolio.Focus();

        }

        #endregion Limpiar

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            myLeer = new clsLeer(ref ConexionLocal);

            IniciarToolBar(false, false, false); 
            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Focus();
                e.Cancel = true;
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_DevolucionesProveedorEnc(DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                }
                else
                {
                    bContinua = false;
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                        bContinua = false;
                    //else
                    //    Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.                    
                }
            }

            IniciarToolBar(true, false, false);
            if ( chkEsCompraPromocion.Checked)
            {
                IniciarToolBar(false, false, false);
                General.msjUser("El Folio de Compra es Promoción ó Regalo, no es posible cancelar la compra.");
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
            lblCancelado.Visible = false;

            //Se hace de esta manera para la ayuda.
            txtFolio.Enabled = false;
            txtFolio.Text = myLeer.Campo("Folio");  //FolioCompra 
            sFolioCompra = txtFolio.Text;
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor"); 
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");

            //chkEsCompraPromocion.Checked = myLeer.CampoBool("EsPromocionRegalo"); 

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            dtpFechaDocto.Value = myLeer.CampoFecha("FechaDocto");
            dtpFechaVenceDocto.Value = myLeer.CampoFecha("FechaVenceDocto");

            btnMotivosDev.Enabled = true;

            if (myLeer.Campo("Status").ToUpper() == "C")
                lblCancelado.Visible = true;

            if (myLeer.Campo("Status").ToUpper() == "D")
            {
                btnEjecutar.Enabled = true;
                General.msjUser("El Folio de Devolución a Proveedor ya cuenta con una devolución parcial.");
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;
            myLlenaDatos = new clsLeer(ref ConexionLocal);

            myLlenaDatos.DataSetClase = Consultas.Folio_DevolucionesProveedorDet(DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioCompra, "txtFolio_Validating");
            if(myLlenaDatos.Leer())
            {
                bRegresa = true;
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }


            CargarDetallesLotesFolio();
            return bRegresa;
        }

        private void CargarDetallesLotesFolio()
        {
            myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes();
            myLlenaDatos.DataSetClase = Consultas.Folio_DevolucionesProveedorDet_Lotes(DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolio");
            Lotes.AddLotes(myLlenaDatos.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                myLlenaDatos.DataSetClase = Consultas.Folio_DevolucionesProveedorDet_Lotes_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolio");
                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            }
        }
        #endregion Buscar Folio

        #region Guardar/Actualizar Folio
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled; 

            //if (txtFolio.Text != "*") 
            //{
            //    MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            //}
            //else
            {
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
                    Dev.NombreOperacion = "DEVOLUCION_A_PROVEEDOR";
                    Dev.MsjSinPermiso = "El usuario no tiene permiso para aplicar una devolución a proveedor, verifique por favor."; 

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

                    Dev.MotivosDev = motivodev.Retorno;

                    if (Dev.Guardar())
                    {
                        InicializarPantalla();
                    }
                    else
                    {
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);  
                    } 
                }
            }

        }
        #endregion Guardar/Actualizar Folio

        #region Eliminar

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion Eliminar

        #region Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
        }
        #endregion Imprimir

        #region Ejecutar 
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            FrmDevolucionesImpresion Devoluciones;
            Devoluciones = new FrmDevolucionesImpresion();
            Devoluciones.MostrarPantalla(txtFolio.Text.Trim(), tpDevolucion, (int)TipoDeVenta.Ninguna);
        }
        #endregion Ejecutar 

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;
            string sIdProducto = "";

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

            if (bRegresa && float.Parse(txtSubTotal.Text) <= 0)
            {
                bRegresa = false;
                General.msjUser("El SubTotal debe ser mayor a cero");
                txtSubTotal.Focus();
            }

            //if (float.Parse(txtIva.Text) <= 0 && bRegresa)
            //{
            //    bRegresa = false;
            //    General.msjUser("El Iva debe ser mayor a cero");
            //    txtIva.Focus();
            //}

            if (bRegresa && float.Parse(txtTotal.Text) <= 0)
            {
                bRegresa = false;
                General.msjUser("El Total debe ser mayor a cero");
                txtTotal.Focus();
            }

            if (bRegresa && myGrid.Rows == 1)
            {
                sIdProducto = myGrid.GetValue(1, 2); //Se verifica que el renglon sea valido.
                sIdProducto.Trim();
                sIdProducto.Replace("(null)", "");
                if (sIdProducto == "")
                {
                    bRegresa = false;
                    General.msjUser("Debe ingresar al menos un Producto");
                    myGrid.ActiveRow = 1;
                    grdProductos.Focus();
                }

            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos(); 
            }

            if (bRegresa && !ComparaTotales())
            {
                bRegresa = false;
                General.msjUser("El Subtotal, Iva y Total de la Factura no coinciden con el calculado por el sistema. \n Estos datos ha sido corregidos.");
                myGrid.ActiveRow = 1;
                grdProductos.Focus();
            }

            if (bRegresa && !motivodev.Marco)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado motivos de Devolución, verifique.");
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
                    bRegresa = false;
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para la Devolución de Venta\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;

        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Checar por incongruencias 
                myLeer.DataSetClase = Ayuda.Folios_Devoluciones_A_Proveedor(DtGeneral.EmpresaConectada, sEstado, sFarmacia, "txtFolio_KeyDown");
                //myLeer.DataSetClase = Ayuda.FolioCompras(sEstado, sFarmacia, "txtFolio_KeyDown");

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();

                    CargaDetallesFolio();
                    Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.
                }

            }

        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // myLeer.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");
                myLlenaDatos.DataSetClase = Ayuda.Proveedores("txtIdProveedor_KeyDown");

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
                myLlenaDatos.DataSetClase = Consultas.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLlenaDatos.Leer())
                    CargaDatosProveedor();
                else
                    txtIdProveedor.Focus();
            }

        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda.
            txtIdProveedor.Text = myLlenaDatos.Campo("IdProveedor");
            lblProveedor.Text = myLlenaDatos.Campo("Nombre");
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


        #endregion Eventos

        #region Grid

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            //if (myGrid.ActiveCol == 1)
            //{
            //    if (e.KeyCode == Keys.F1)
            //    {
            //        myLeer.DataSetClase = Ayuda.ProductosEstado(sEstado, "grdProductos_KeyDown");
            //        if (myLeer.Leer())
            //        {
            //            CargaDatosProducto();
            //        }

            //    }
            //}

        }

        private void CargaDatosProducto()
        {
            //int iRowActivo = myGrid.ActiveRow;

            //if (lblCancelado.Visible == false)
            //{
            //    if ( !myGrid.BuscaRepetido( myLeer.Campo("CodigoEAN"), iRowActivo, 1) )
            //    {
            //        myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
            //        myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
            //        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
            //        myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
            //        //myGrid.BloqueaCelda(false, iRowActivo, 5);
            //        myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo);

            //        CargarLotesCodigoEAN();
            //    }
            //    else
            //    {
            //        General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
            //        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
            //        limpiarColumnas();
            //        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
            //    }

            //}

            //// grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            //if ( lblCancelado.Visible == false )
            //{
            //    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            //    {
            //        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Costo) != "")
            //        {
            //            myGrid.Rows = myGrid.Rows + 1;
            //            myGrid.ActiveRow = myGrid.Rows;
            //            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
            //        }
            //    }
            //}

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
            //string sValor = "";

            //switch (myGrid.ActiveCol)
            //{
            //    case 1:                    
            //        {
            //            sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

            //            myLeer.DataSetClase = Consultas.ProductosEstado( sEstado, sValor, "grdProductos_EditModeOff");
            //            if (myLeer.Leer())
            //            {
            //                CargaDatosProducto();
            //            }
            //            else
            //            {
            //                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            //                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
            //            }
            //        }

            //        break;                
            //}
        }

        private void limpiarColumnas()
        {
            //for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            //{
            //    myGrid.SetValue( myGrid.ActiveRow, i, "");
            //} 
        }

        private void EliminarRenglonesVacios()
        {
            //for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            //{
            //    if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
            //        myGrid.DeleteRow(i);
            //}

            //if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            //    myGrid.AddRow();
        }

        private bool ComparaTotales()
        {
            bool bIguales = false;
            double dSubTotalText = 0, dIvaText = 0, dTotalText = 0;
            double dSubTotalGrid = 0, dIvaGrid = 0, dTotalGrid = 0;

            //Se obtienen los totales de los Textbox
            dSubTotalText = Math.Round(Convert.ToDouble(txtSubTotal.Text), 4, MidpointRounding.ToEven);
            dIvaText = Math.Round(Convert.ToDouble(txtIva.Text), 4, MidpointRounding.ToEven);
            dTotalText = Math.Round(Convert.ToDouble(txtTotal.Text), 4, MidpointRounding.ToEven);

            //Se obtienen los totales del Grid.
            dSubTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.Importe), 4, MidpointRounding.ToEven);
            dIvaGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.ImporteIva), 4, MidpointRounding.ToEven);
            dTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal), 4, MidpointRounding.ToEven);

            // Se compara que sean iguales.
            if ((dSubTotalText == dSubTotalGrid) && (dIvaText == dIvaGrid) && (dTotalText == dTotalGrid))
                bIguales = true;
            else
            {
                txtSubTotal.Text = dSubTotalGrid.ToString(sFormato);
                txtIva.Text = dIvaGrid.ToString(sFormato);
                txtTotal.Text = dTotalGrid.ToString(sFormato);
            }

            return bIguales;
        }

        private void Totalizar()
        {
            txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString(sFormato);
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString(sFormato);
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString(sFormato);
        }

        #endregion Grid

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                myLeer.Leer();
                Lotes.AddLotes(myLeer.DataSetClase);
                mostrarOcultarLotes();
            }
        }

        private void removerLotes()
        {
            //int iRow = myGrid.ActiveRow;
            //Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
            //myGrid.DeleteRow(iRow);
            ////Totalizar();

            //if (myGrid.Rows == 0)
            //    myGrid.Limpiar(true);

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
                    Lotes.EsEntrada = true; // Asegurar que solo se pueda dar salida al maximo de piezas recibidas 
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    // Lotes.EsCancelacionCompras = true;
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucion_Dev_A_Proveedor;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.CantDev, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
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

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnMotivosDev_Click(object sender, EventArgs e)
        {
            motivodev.MotivosDevolucion();
        }

    } // Llaves de la Clase
} // Llaves del NameSpace
