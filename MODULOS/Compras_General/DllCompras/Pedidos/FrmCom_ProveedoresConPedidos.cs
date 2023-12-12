using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using DllCompras.OrdenesDeCompra;

namespace DllCompras.Pedidos
{
    public partial class FrmCom_ProveedoresConPedidos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        bool bEsInicio = true;

        string sEmpresaRegional = "";
        string sEstadoRegional = "";

        string sFormato = "#,###,###,##0.###0";
        string sFormatoInt = "#,###,###,##0";
        string sFolioPedido = "";
        string sUnidad = "";

        private enum Cols
        {
            Ninguna = 0,
            IdProveedor = 1, Proveedor = 2, Cantidad = 3, Importe = 4
        }

        public FrmCom_ProveedoresConPedidos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdProveedores, this);
            grid.EstiloGrid(eModoGrid.ModoRow); 
        }

        
        private void FrmCom_ProveedoresConPedidos_Load(object sender, EventArgs e)
        {
            actualizarToolStripMenuItem_Click(null, null);
            bEsInicio = false; 
        }

       
        private void generarPrePedidoProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////string sIdProveedor = "", sNomProv = "";
            ////FrmCom_GenerarPedidoProveedor frm = new FrmCom_GenerarPedidoProveedor();

            ////sIdProveedor = grid.GetValue(grid.ActiveRow, 1);
            ////sNomProv = grid.GetValue(grid.ActiveRow, 2);

            ////frm.MostrarPedidoParaProveedor(sIdProveedor, sNomProv);

            string sIdProveedor = "";
            bool bGeneraAutomatica = true;
            FrmOrdenCompraCodigoEAN Frm = new FrmOrdenCompraCodigoEAN();

            sIdProveedor = grid.GetValue(grid.ActiveRow, 1);
            
            Frm.GenerarOrdenDeCompra_Automatica(sEmpresaRegional, sEstadoRegional, sIdProveedor, bGeneraAutomatica, sFolioPedido, sUnidad);

            MostraListadoProveedores(sEmpresaRegional, sEstadoRegional, sUnidad);
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void MostraListadoProveedores(string EmpresaRegional, string EstadoRegional, string Unidad)
        {
            string sSql = "";
            double dImpteTotal = 0;
            int iCantidad = 0;

            sEmpresaRegional = EmpresaRegional;
            sEstadoRegional = EstadoRegional;
            sUnidad = Unidad;

            sSql = string.Format(" SELECT IdProveedor, NombreProveedor, " +
                                  " Sum(Cant_A_Pedir) As CantidadTotal, Sum( Precio * Cant_A_Pedir) As ImporteTotal, FolioPedido " +
                                  " FROM Com_Pedidos_Compras ( NOLOCK ) " +
                                  " Where GUID = '{0}'" + 
                                  " Group by IdProveedor, NombreProveedor, FolioPedido " +
                                  " Having sum(Cant_A_Pedir) > 0 " + 
                                  " ORDER BY NombreProveedor ", GnCompras.GUID);

            grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "MostraListadoProveedores()"); 
                General.msjError("Ocurrió un error al obtener la Lista De Proveedores.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    sFolioPedido = leer.Campo("FolioPedido");
                    dImpteTotal = grid.TotalizarColumnaDou((int)Cols.Importe);
                    lblTotalImpte.Text = dImpteTotal.ToString(sFormato);

                    iCantidad = grid.TotalizarColumna((int)Cols.Cantidad);
                    lblCantidad.Text = iCantidad.ToString(sFormatoInt);

                    if ( bEsInicio ) 
                        this.ShowDialog();      
                }
                else
                {
                    General.msjUser("No se han Asignado Claves-CodigoEAN para pedido a Proveedor.");
                    this.Hide();
                }
            } 
        }
    }
}
