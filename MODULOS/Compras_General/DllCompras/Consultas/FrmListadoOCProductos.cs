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
using DllCompras.OrdenesDeCompra;

namespace DllCompras.Consultas
{
    public partial class FrmListadoOCProductos : FrmBaseExt
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
        string sFolioPedido = "", sFolioPedidoUnidad = "";
        string sFormato = "#,###,###,##0.###0";

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Proveedor = 2, FechaRegistro = 3, Status = 4, FechaColocacion = 5, Importe = 6
        }

        public FrmListadoOCProductos()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdOrdenCompras, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdOrdenCompras.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            //Cargar_Empresas();
        }

        private void FrmListadoOrdenesDeCompras_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }        

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFoliosOrdenCompras("", "", "", "", "", "");
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            ////Fg.IniciaControles(this, true);
            ////myGrid.Limpiar(false);
            dtpFechaPedido.Enabled = false;
            grdOrdenCompras.Focus();
            CalcularTotalImporte();
        }

        public void CargarFoliosOrdenCompras(string Empresa, string Estado, string FolioPedido, string FolioPedidoUnidad, string Fecha, string Unidad) 
        {
            int iEsCentral = 0;
            sFolioPedido = FolioPedido;
            sFolioPedidoUnidad = FolioPedidoUnidad;

            sEmpresa = Empresa;
            sEstado = Estado;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                iEsCentral = 1;
            }

            string sSql = string.Format(" Select E.Folio, E.Proveedor, CONVERT(varchar(10),E.FechaRegistro,120) As FechaRegistro, " +
                                    " Case When E.Status = 'C' Then 'CANCELADA' When E.Status = 'OC' Then 'ORDEN COLOCADA' " +
                                        " Else 'ORDEN NO COLOCADA' End As StatusOrdenCompra, " +
                                " CONVERT(varchar(10),E.FechaColocacion,120) As FechaColocacion, Sum(D.Importe) As ImporteTotal " +
                                " From vw_OrdenesCompras_Claves_Enc E (Nolock)" +
                                " Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock)" +
                                    " On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioOrden ) " +
                                " Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = '{2}' " +
                                " And E.EntregarEn = '{3}' And E.FolioPedidoUnidad = '{4}' And E.EsCentral = {5} " +
                                " Group By E.Folio, E.Proveedor, E.FechaRegistro, E.Status, E.FechaColocacion " +
                                " Order By E.Folio ", sEmpresa, sEstado, sFarmacia, Unidad, FolioPedidoUnidad, iEsCentral);

            myGrid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarFoliosOrdenCompras()");
                General.msjError("Ocurrió un error al obtener la lista de Ordenes de Compras.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    lblPedido.Text = sFolioPedido;
                    lblPedidoUnidad.Text = sFolioPedidoUnidad;
                    dtpFechaPedido.Value = Convert.ToDateTime(Fecha); 
                    this.ShowDialog();
                }
                else
                {
                    General.msjAviso("No se ha asigando Ordenes de compra al Pedido seleccionado, verifque."); 
                    //General.msjAviso("No se han Asignado Ordenes de Compra al Pedido Seleccionado");
                    this.Hide();
                }
            }
        }

        private void CalcularTotalImporte()
        {
            double dImpteTotal = 0;
            dImpteTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            lblImpteTotal.Text = dImpteTotal.ToString(sFormato);
        }
        #endregion Funciones

        #region Eventos_Grid
        private void grdOrdenCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFolioOrden = "";

            sFolioOrden = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);

            if (sFolioOrden.Trim() != "")
            {
                FrmOrdenCompraCodigoEAN OrdenCompra = new FrmOrdenCompraCodigoEAN();
                OrdenCompra.MostrarDetalleOrdenCompra( sEmpresa, sEstado, sFolioOrden, true);
            }
        }
        #endregion Eventos_Grid        
    }
}
