using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmOrdenCompraManual : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        string sFormato = "#,###,###,##0.###0";
        bool bHabilitar = false;
        bool bModal = false;
        int iTipoOrden = 1;

        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Precio = 4, Descuento = 5, TasaIva = 6,
            Iva = 7, PrecioUnitario = 8, Cantidad = 9, Importe = 10
        }

        public FrmOrdenCompraManual()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            CargarEmpresas();
            CargarEstados();
        }

        private void FrmPedidosCEDIS_Load(object sender, EventArgs e)
        {
            if (bModal)
            {
                btnNuevo.Enabled = false;
                IniciarToolBar(false, false, true, false);
                btnExportarPDF.Enabled = true;
                HabilitarCampos(false, false, false, false, false, false);
                myGrid.BloqueaRenglon(true);
            }
            else
            {
                LimpiarPantalla();
            }
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Cerrar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnExportarPDF.Enabled = Imprimir; 
            btnCerrarOrden.Enabled = Cerrar;
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false);

            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;

            cboEmpresas.SelectedIndex = 0;
            cboEstados.SelectedIndex = 0;

            dtpFechaRegistro.Enabled = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;
            bHabilitar = false;
            txtPedido.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Buscar Pedido 
        private void txtPedido_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            //IniciarToolBar(false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtPedido.Text.Trim() == "")
            {
                txtPedido.Enabled = false;
                txtPedido.Text = "*";
                IniciarToolBar(true, false, false, true);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_OrdenesCompra_Manual_Enc(sEmpresa, sEstado, sFarmacia, txtPedido.Text.Trim(), iTipoOrden, "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    bContinua = true;
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
                    bContinua = CargaDetallesFolio();
                }
            }

            if (!bContinua)
            {
                txtPedido.Focus();
            }
        }

        private void CargaEncabezadoFolio()
        {
            bool bHabilitada = true;
            //Se hace de esta manera para la ayuda.
            txtPedido.Text = myLeer.Campo("Folio");
            sFolioPedido = txtPedido.Text;
            cboEmpresas.Data = myLeer.Campo("FacturarA");
            txtProveedor.Text = myLeer.Campo("IdProveedor");
            lblNomProv.Text = myLeer.Campo("Proveedor");
            cboEstados.Data = myLeer.Campo("EstadoEntrega");
            txtIdFarmacia.Text = myLeer.Campo("EntregarEn");
            lblEntregarEn.Text = myLeer.Campo("FarmaciaEntregarEn");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            lblDomicilio.Text = myLeer.Campo("Domicilio");       
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRequeridaEntrega.Value = myLeer.CampoFecha("FechaRequeridaEntrega");          
            

            //IniciarToolBar(false, false, true, false);
            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(false, false, true, false);
                lblStatus.Text = "CANCELADA";
                lblStatus.Visible = true;
                HabilitarCampos(false, false, false, false, false, false);
                bHabilitar = true;
                bHabilitada = false;
            }

            if (myLeer.Campo("Status") == "OC")
            {
                IniciarToolBar(false, true, true, false);
                lblStatus.Text = "ORDEN COLOCADA";
                lblStatus.Visible = true;
                HabilitarCampos(false, false, false, false, false, false);
                bHabilitar = true;
                bHabilitada = false;
            }

            if (bHabilitada)
            {
                HabilitarCampos(false, true, true, true, true, true);
                IniciarToolBar(true, true, true, true);
            }

            if (bModal)
            {
                btnNuevo.Enabled = false;
                IniciarToolBar(false, false, true, false);
                btnExportarPDF.Enabled = true;
                HabilitarCampos(false, false, false, false, false, false);
                myGrid.BloqueaRenglon(true);
            }
           
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.Folio_OrdenCompra_Manual_Det(sEmpresa, sEstado, sFarmacia, txtPedido.Text.Trim(), "CargaDetallesFolio()");
            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }

            // Bloquear grid completo
            CalcularTotalImporte();
            myGrid.BloqueaRenglon(bHabilitar);

            return bRegresa;
        }

        private void HabilitarCampos(bool bOrden, bool bProveedor, bool bEstado, bool bEntregarEn, bool bObservaciones, bool bFechaReq)
        {
            txtPedido.Enabled = bOrden;
            cboEmpresas.Enabled = bEstado;
            txtProveedor.Enabled = bProveedor;
            cboEstados.Enabled = bEstado;
            txtIdFarmacia.Enabled = bEntregarEn;
            txtObservaciones.Enabled = bObservaciones;
            dtpFechaRequeridaEntrega.Enabled = bFechaReq;
        }

        #endregion Buscar Pedido

        #region Guardar Informacion
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            //if (txtPedido.Text != "*")
            //{
            //    MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            //}
            //else
            //{
                EliminarRenglonesVacios();
                CalcularTotalImporte();
                if (ValidaDatos())
                {
                    if (ConexionLocal.Abrir())
                    {
                        ConexionLocal.IniciarTransaccion();

                        bContinua = GrabarEncabezado(1);

                        if (bContinua)
                        {
                            bContinua = GrabarDetalle(); 
                        }

                        if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                        {
                            txtPedido.Text = sFolioPedido;
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciarToolBar(false, false, true, false);
                            Imprimir(true);
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            ConexionLocal.DeshacerTransaccion(); 
                            General.msjError("Ocurrió un error al guardar la información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        General.msjAviso(General.MsjErrorAbrirConexion);
                    }

                }
            //}

        }

        private bool GrabarEncabezado(int iOpcion)
        {
            bool bRegresa = true;
            int iEsAutomatica = 0;

            string sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
                                        " '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPedido.Text.Trim(), cboEmpresas.Data, 
                txtProveedor.Text, DtGeneral.IdPersonal, cboEstados.Data, txtIdFarmacia.Text, dtpFechaRequeridaEntrega.Text, 
                txtObservaciones.Text, iTipoOrden, iEsAutomatica, iOpcion); 

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");                
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdClaveSSA = "";
            int iCantidad = 0;
            double dPrecio = 0, dDescuento = 0, dTasaIva = 0, dIva = 0, dImporte = 0, dPrecioUnitario = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);                
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dPrecio = myGrid.GetValueDou(i, (int)Cols.Precio);
                dDescuento = myGrid.GetValueDou(i, (int)Cols.Descuento);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                dIva = myGrid.GetValueDou(i, (int)Cols.Iva);
                dPrecioUnitario = myGrid.GetValueDou(i, (int)Cols.PrecioUnitario);
                dImporte = myGrid.GetValueDou(i, (int)Cols.Importe);

                if (sIdClaveSSA != "")
                {
                    sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'  ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido, 
                        sIdClaveSSA, iCantidad, dPrecio, dDescuento, dTasaIva, dIva, dPrecioUnitario, dImporte); 

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        #endregion Guardar Informacion

        #region Eliminar Informacion 
        private void btnCancelar_Click(object sender, EventArgs e)
        {           
            bool bRegresa = false;
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea cancelar el Folio seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    bRegresa = GrabarEncabezado(iOpcion);

                    if (bRegresa)
                    { 
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar el Folio.");
                        //btnNuevo_Click(null, null);
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }            
        }
        #endregion Eliminar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            Imprimir(false, true);
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtPedido.Text.Trim() == "" || txtPedido.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de pedido inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            Imprimir(Confirmacion, false); 
        }

        private void Imprimir(bool Confirmacion, bool Exportar)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sNombre = "INT-OC-" + txtPedido.Text.Trim() + ".pdf";

                myRpt.RutaReporte = GnCompras.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", Fg.PonCeros(txtPedido.Text, 8));
                myRpt.NombreReporte = "COM_OrdenDeCompra_Manual";

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                if (Exportar)
                {
                    bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                }
                else
                {
                    // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Eventos 
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text != "")
            {
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    myLeer.DataSetClase = Consultas.AlmacenesCompras(cboEstados.Data, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                }

                if (myLeer.Leer())
                {
                    CargarDatosDeFarmacia();
                }
                else
                {
                    txtIdFarmacia.Text = "";
                    txtIdFarmacia.Focus();
                    lblEntregarEn.Text = "";
                }
            }
        }
        #endregion Eventos

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == (int)Cols.ClaveSSA)
            {
                if (e.KeyCode == Keys.F1)
                {
                    //myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    myLeer.DataSetClase = ayuda.ClavesSSA_Productos_Proveedor(txtProveedor.Text, "grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                        //CargaDatosSal();
                        ObtenerDatosSal();
                    }

                }

                if (e.KeyCode == Keys.Delete)
                {
                    myGrid.DeleteRow(myGrid.ActiveRow);

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                }
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (lblStatus.Visible == false)
            {
                if (btnCerrarOrden.Enabled)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, 1);
                        }
                    }
                }
                CalcularTotalImporte();
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case (int)Cols.ClaveSSA:
                    {
                        ObtenerDatosSal();
                    }

                    break;
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
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

        private void ObtenerDatosSal()
        {
            string sCodigo = ""; // , sSql = "";           

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Clave no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                
                //myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosSal()");
                //sSql = string.Format(" Select S.ClaveSSA, S.IdClaveSSA_Sal, S.Descripcion, L.Precio, " + 
                //                     " L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, 0 As Cantidad, 0.0 As Importe " +
                //                     " From CatClavesSSA_Sales S (NoLock) " +
                //                     " Inner Join COM_OCEN_ListaDePrecios_Claves L (NoLock) " +
                //                        " On( S.IdClaveSSA_Sal = L.IdClaveSSA ) Where L.IdClaveSSA = '{0}' ", Fg.PonCeros(sCodigo, 4));

                //if (!myLeer.Exec(sSql))
                //{
                //    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                //    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                //}
                //else
                //{
                myLeer.DataSetClase = Consultas.ClavesSSA_Productos_Proveedor(txtProveedor.Text, sCodigo, "ObtenerDatosSal()");
                if (!myLeer.Leer())
                {
                    //General.msjUser("Clave no encontrada ó no esta Asignada a la Farmacia.");
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                }
                else
                {
                    CargaDatosSal();
                }
                //}
           }
            
        }        

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));                    
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Precio, myLeer.Campo("Precio"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descuento, myLeer.Campo("Descuento"));
                    myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Iva, myLeer.Campo("Iva"));
                    myGrid.SetValue(iRowActivo, (int)Cols.PrecioUnitario, myLeer.Campo("PrecioUnitario"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Importe, myLeer.Campo("Importe"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
                else
                {
                    General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtPedido.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de pedido inválido, verifique.");
                txtPedido.Focus();
            }

            if (bRegresa && cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado A quien se va a Facturar, verifique.");
                cboEmpresas.Focus();
            }

            if (bRegresa && txtProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Proveedor inválido, verifique.");
                txtProveedor.Focus();
            }

            if (bRegresa && cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && txtIdFarmacia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture donde se entregará la Orden de compra, verifique.");
                txtIdFarmacia.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No han capturado las observaciones, verifique.");
                txtObservaciones.Focus();
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
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    ////if ( int.Parse( lblUnidades.Text ) == 0 )
                    ////{
                    ////    bRegresa = false;
                    ////}
                    ////else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para la Orden de Compra\n y/o capturar cantidades para al menos una Clave, verifique.");
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Buscar_Proveedor
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                myLeer = new clsLeer(ref ConexionLocal);
                myLeer.DataSetClase = Consultas.Proveedores(txtProveedor.Text, "txtProveedor_Validating");
                if (myLeer.Leer())
                {
                    txtProveedor.Text = myLeer.Campo("IdProveedor");
                    lblNomProv.Text = myLeer.Campo("Nombre");
                }
            }
        }
        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Proveedores("txtProveedor_KeyDown");
                {
                    txtProveedor.Text = myLeer.Campo("IdProveedor");
                    lblNomProv.Text = myLeer.Campo("Nombre");
                }

            }
        }
        #endregion Buscar_Proveedor

        #region Funciones
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.EstadosConFarmacias("CargarEstados"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
                        
        }        

        private void CargarDatosDeFarmacia()
        {
            txtIdFarmacia.Text = myLeer.Campo("IdFarmacia");
            lblEntregarEn.Text = myLeer.Campo("Farmacia");
            lblDomicilio.Text = myLeer.Campo("Domicilio") + " " + myLeer.Campo("Colonia") + ", " + myLeer.Campo("Municipio") + " " + myLeer.Campo("Estado");
        }

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(myLeer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CalcularTotalImporte()
        {
            double dImpteTotal = 0;
            dImpteTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            lblImpteTotal.Text = dImpteTotal.ToString(sFormato);
        }

        public void MostrarDetalleOrdenCompra(string sFolioOrden, bool bModalForma)
        {
            txtPedido.Text = sFolioOrden;
            txtPedido_Validating(null, null);
            bModal = bModalForma;
            this.ShowDialog();
        }
        #endregion Funciones

        #region Colocar_OrdenCompra
        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iOpcion = 3; //La opcion 3 indica que se cierra la orden para que sea colocada
            string message = "¿ Desea Cerrar la Orden de Compra ?";

            if (General.msjConfirmar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    bRegresa = GrabarEncabezado(iOpcion);

                    if (bRegresa)
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCerrarOrden_Click");
                        General.msjError("Ocurrió un error al cerrar la Orden de Compra.");                        
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }
        #endregion Colocar_OrdenCompra
        
    }
}
