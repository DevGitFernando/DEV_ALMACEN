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
using Farmacia.Ventas;
using DllFarmaciaSoft;

namespace Farmacia.AlmacenJuris
{
    public partial class FrmPedidosRC_Surtibles : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            Empresa = 1, Estado = 2, Jurisdiccion = 3, Farmacia = 4, Pedido = 5,
            EstatusPedido = 6, NumeroSurtimientos = 7, Fecha = 8, CentroSalud = 9, 
            CantidadSolicitada = 10, CantidadEntregada = 11
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        // FrmVentas Ventas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmPedidosRC_Surtibles()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            grid = new clsGrid(ref grdPedidos, this);
            grid.EstiloGrid(eModoGrid.SeleccionSimple);
        }

        private void FrmPedidosRC_Surtibles_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.Limpiar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";

            sSql = string.Format(" Exec spp_AMLJ_PedidosRC_Surtibles ");            

            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al Obtener la lista de Pedidos por Surtir.");
            }
            else
            {
                if (!leer.Leer())
                    General.msjUser("No existen Pedidos pendientes de Surtir.");
                else
                    grid.LlenarGrid(leer.DataSetClase);
            }
        }
        #endregion Botones

        #region Grid
        
        private void grdPedidos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string IdEmpresaCSGN = "", IdEstadoCSGN = "", IdJurisdiccion = "", IdFarmaciaCSGN = "", sFolioCSGN = "";
            int iCantidadSurtida = 0;

            IdEmpresaCSGN = grid.GetValue(grid.ActiveRow, (int)Cols.Empresa);
            IdEstadoCSGN = grid.GetValue(grid.ActiveRow, (int)Cols.Estado);
            IdJurisdiccion = grid.GetValue(grid.ActiveRow, (int)Cols.Jurisdiccion);
            IdFarmaciaCSGN = grid.GetValue(grid.ActiveRow, (int)Cols.Farmacia);
            sFolioCSGN = grid.GetValue(grid.ActiveRow, (int)Cols.Pedido);
            iCantidadSurtida = grid.GetValueInt(grid.ActiveRow, (int)Cols.CantidadSolicitada);

            FrmVentas Ventas = new FrmVentas();
            Ventas.MostrarDetalle(IdEmpresaCSGN, IdEstadoCSGN, IdJurisdiccion, IdFarmaciaCSGN, sFolioCSGN, iCantidadSurtida);

            btnEjecutar_Click(this, null);
        }

        #endregion Grid

    }
}
