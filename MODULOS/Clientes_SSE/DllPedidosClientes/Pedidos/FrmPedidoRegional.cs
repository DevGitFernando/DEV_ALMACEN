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

namespace DllPedidosClientes.Pedidos
{
public partial class FrmPedidoRegional : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsConsultas Consultas;
        //clsAyudas ayuda;
        clsGrid myGrid;
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        string sEstado = DtGeneralPedidos.EstadoConectado;

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Cantidad = 4
        }

        public FrmPedidoRegional()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            //ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneralPedidos.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;           
        }

        private void FrmPedidosSecretaria_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Limpiar

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnImprimir.Enabled = false;
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            IniciarToolBar(false, false, false);
 
            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;

            txtIdPersonal.Enabled = false;
            txtIdPersonal.Text = DtGeneralPedidos.IdPersonal;
            lblPersonal.Text = DtGeneralPedidos.NombrePersonal;

            dtpFechaRegistro.Enabled = false;
            lblUnidades.Text = "0";            
            
            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        
        #endregion Limpiar

        #region Buscar Folio

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
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
                myLeer.DataSetClase = Consultas.PedidoEspecialRegional_Enc( sEstado, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    // IniciarToolBar(false, true, true);
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
                }
            }

            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();

            if (!bContinua)
                txtFolio.Focus();
        }

        private void CargaEncabezadoFolio()
        {            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio");
            sFolioPedido = txtFolio.Text;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            dtpFechaRequerida.Value = myLeer.CampoFecha("FechaRequeridaEntrega");
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            
            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            
            if (myLeer.Campo("StatusPedido") == "C")
            {   
                IniciarToolBar(false, false, true);                
                lblStatus.Text = "CANCELADO";
                lblStatus.Visible = true;
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;
            string sSql = String.Format("Select ClaveSSA, IdClaveSSA, DescripcionSal, Cantidad_Solicitada " + 
                " From vw_CTE_PedidoEspecialRegional_Det (NoLock) " + 
                "Where IdEstado = '{0}' and Folio = '{1}' ", DtGeneralPedidos.EstadoConectado, txtFolio.Text);

            if (myLlenaDatos.Exec(sSql))
            {
                if (myLlenaDatos.Leer())
                {
                    myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                }
                else
                {
                    bRegresa = false;
                    General.msjUser("El Folio ingresado no contiene detalles. Verifique.");
                }
            }
            else
            {
                bRegresa = false;
                General.msjUser("Ocurrió un error al obtener los detalles del Folio.");
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            return bRegresa;
        }

        #endregion Buscar Folio

        #region Guardar Informacion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (txtFolio.Text != "*")
            {
                MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                EliminarRenglonesVacios();
                if (ValidaDatos())
                {
                    if (ConexionLocal.Abrir())
                    {
                        ConexionLocal.IniciarTransaccion();

                        if (GrabarEncabezado())
                            bContinua = GrabarDetalle();
                        

                        if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolioPedido;
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciarToolBar(false, false, true);
                            //ImprimirPedido();
                            btnNuevo_Click(this, null);
                        }
                        else
                        {
                            ConexionLocal.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la Información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                    }

                }
            }

        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            int iOpcion = 1;
            string sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_CTE_PedidoEspecialRegional_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEstado, txtFolio.Text, DtGeneralPedidos.IdPersonal, General.FechaYMD(dtpFechaRequerida.Value, "-"), txtObservaciones.Text.Trim(), iOpcion);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje");
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdClaveSSA = "", sClaveSSA = "";
            int iCantidad = 0, iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                if (sIdClaveSSA != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_CTE_PedidoEspecialRegional_Det '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                         sEstado, sFolioPedido, sIdClaveSSA, iCantidad, iOpcion );
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
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea cancelar el Folio seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_CTE_PedidoEspecialRegional_Enc '{0}', '{1}', '', '', '', '{2}' ",
                        sEstado, txtFolio.Text, iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

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
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }            

        }


        #endregion Eliminar Informacion

        #region Imprimir Informacion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion Imprimir Informacion

        #region Eventos

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
        }
        
        #endregion Eventos

        #region Grid

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    //myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    //if (myLeer.Leer())
                    //{
                    //    myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
                    //    CargaDatosSal();
                    //}

                }

                if (e.KeyCode == Keys.Delete)
                {
                    myGrid.DeleteRow(myGrid.ActiveRow);

                    if (myGrid.Rows == 0)
                        myGrid.Limpiar(true);
                }

            }
            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (lblStatus.Visible == false)
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
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1:
                    {
                        ObtenerDatosSal();
                    }

                    break;
            }
            lblUnidades.Text = myGrid.TotalizarColumna(4).ToString();
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
            string sCodigo = "", sSql = "";
            // int iCantidad = 0;

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                //sSql = string.Format("Exec spp_SalesPedidosSecretaria '{0}', '{1}', '{2}' ", sIdClienteEstado, Fg.PonCeros(sCodigo, 4), sCodigo);
                sSql = String.Format("Select ClaveSSA, IdClaveSSA_Sal, DescripcionSal, 0.0000 as Cantidad " + 
                    " From vw_ClavesSSA_Sales(NoLock) " + 
                    " Where ClaveSSA = '{0}' ", sCodigo);

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                }
                else
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("Sal no encontrada ó no esta Asignada al Cliente.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal();
                    }
                }
            }
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("DescripcionSal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
                else
                {
                    General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }

        private void grdProductos_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
        }

        #endregion Grid

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Pedido inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese las observaciones por favor.");
                txtObservaciones.Focus();
            }

            if (bRegresa && ( dtpFechaRequerida.Value <= dtpFechaRegistro.Value ) )
            {
                bRegresa = false;
                General.msjUser("La Fecha Requerida de Entrega tiene que ser mayor a la Fecha de Registro.");
                dtpFechaRequerida.Focus();
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
                    if ( int.Parse( lblUnidades.Text ) == 0 )
                    {
                        bRegresa = false;
                    }
                    else
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
                General.msjUser("Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.");

            return bRegresa;

        }

        #endregion Validaciones de Controles


    }
}
