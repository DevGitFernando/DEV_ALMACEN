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

namespace DllProveedores.OrdenesCompra
{
    public partial class FrmListaDeOrdenesDeCompra : FrmBaseExt 
    {
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrid Grid;        

        private enum Cols
        {
            Pedido = 1, Fecha = 2, IdTipoPedido = 3, Descripcion = 4
        }

        public FrmListaDeOrdenesDeCompra()
        {
            InitializeComponent();

            Grid = new clsGrid(ref grdPedidos, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
        }

        private void FrmListaDeOrdenesDeCompra_Load(object sender, EventArgs e)
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
            string sSql = String.Format("Select Folio, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 'A' as IdTipoPedido, 'Orden de Compra' as TipoPedido " +
                " From vw_COM_OCEN_Ordenes_Compra_Enc (NoLock) " +
                " Where IdProveedor = '{0}' And StatusPedido = 'A' ", GnProveedores.IdProveedor);

            Grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
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
        private void IniciaToolBar(bool bNuevo, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Funciones

        private void grdPedidos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFolio = "";
            sFolio = Grid.GetValue(Grid.ActiveRow, (int)Cols.Pedido);

            FrmOrdCompClavesSSA ClavesSSA = new FrmOrdCompClavesSSA();
            ClavesSSA.LlenarEncabezado(sFolio);

            btnEjecutar_Click(null, null);      
        }

    }
}
