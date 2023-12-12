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

using DllProveedores;
using DllProveedores.Consultas;

namespace DllProveedores.ConfirmarPedidos
{
    public partial class FrmListaPedidosConfirmar : FrmBaseExt 
    {
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrid Grid;
        FrmConfirmarPedidosClaveSSA Sales;
        FrmConfirmarPedidosCodigosEAN Productos;

        private enum Cols
        {
            Pedido = 1, Fecha = 2, IdTipoPedido = 3, Descripcion = 4 
        }

        public FrmListaPedidosConfirmar()
        {
            InitializeComponent();
            Grid = new clsGrid(ref grdPedidos, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
        }

        private void FrmListaPedidosConfirmar_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            btnEjecutar_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            IniciaToolBar(true, true, false);
            Grid.Limpiar(false);
            grdPedidos.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = String.Format("Select Folio, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, IdTipoPedido, TipoPedidoDescripcion " + 
                " From vw_COM_OCEN_Pedidos_Proveedor (NoLock) " +
                " Where IdProveedor = '{0}' And IdTipoPedido In( '03' ) And StatusPedido = 'PG' ", GnProveedores.IdProveedor);

            Grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                // Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el listado de pedidos");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);
                    Grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existen pedidos para confirmar");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion Botones

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Grid
        private void grdPedidos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sPedido = Grid.GetValue(Grid.ActiveRow, (int)Cols.Pedido);
            string sTipoPedido = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdTipoPedido);

            if (sTipoPedido == "05")
            {
                Productos = new FrmConfirmarPedidosCodigosEAN();
                Productos.MostrarDetalle(sPedido);
                //Productos.ShowDialog();
                Productos = null; // Se destruye el objeto
            }
            else
            {                
                Sales = new FrmConfirmarPedidosClaveSSA();
                Sales.MostrarDetalle(sPedido);
                //Sales.ShowDialog();
                Sales = null; // Se destruye el objeto
            }

            btnEjecutar_Click(null, null);
        }
        #endregion Grid
    }
}
