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
using SC_SolutionsSystem.Reportes;
using DllFarmaciaSoft;

namespace DllCompras.OrdenCompra
{
    public partial class FrmListadoDePrePedidosProveedores : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;

        clsAyudas Ayuda;
        clsConsultas Consultas;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;

        private enum Cols
        {
            Ninguno = 0,
            IdProveedor = 1, Proveedor = 2, FolioPedido = 3, FechaExpedicion = 4, Status = 5, FechaRespuesta = 6 
        }

        public FrmListadoDePrePedidosProveedores()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new clsGrabarError(General.DatosConexion, GnCompras.DatosApp, this.Name);

            Grid = new clsGrid(ref grdPedidosProveedores, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmListadoDePrePedidosProveedores_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar Proveedor
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Proveedores(txtProveedor.Text.Trim(), "txtProveedor_Validating");

                if (leer.Leer())
                {
                    CargarDatosProveedor();
                }
            }
        }

        private void CargarDatosProveedor()
        {
            //Se hace de esta manera por la ayuda.
            txtProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Nombre");

            txtProveedor.Enabled = false;
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Proveedores("txtProveedor_Validating");

                if (leer.Leer())
                {
                    CargarDatosProveedor();
                }
            }
        }
        #endregion Buscar Proveedor

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            txtProveedor.Enabled = false;
            Grid.Limpiar();
            rdoTodos.Checked = true;
            rdoTodos.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sWhereProveedor = "";

            if( rdoProveedor.Checked )
            {
                sWhereProveedor = String.Format(" And IdProveedor = '{0}' ", txtProveedor.Text.Trim());
            }

            string sSql = String.Format("Select IdProveedor, NombreProveedor, Folio, Convert( varchar(10), FechaRegistro, 120) , " +
                " StatusPedidoDescAux, Convert( varchar(10), FechaRespuesta, 120 ) as FechaRespuesta" +
                " From vw_COM_OCEN_Pedidos_Proveedor (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And StatusPedido In ( 'PG', 'PCP', 'PRP' ) {2} and IdPersonal = '{3}' " +
                " And Folio Not In " + 
                " (	Select Folio From vw_COM_OCEN_Pedidos_Proveedor (NoLock) " + 
                "   Where IdEmpresa = '{0}' And IdEstado = '{1}' And StatusPedido = 'PRP' {2} and IdPersonal = '{3}' " + 
                "   And Convert( varchar(10), FechaRespuesta, 120 ) <= Convert( varchar(10), GetDate()-3, 120 ) " +
                " )" +  
                " Order by Folio ", sEmpresa, sEstado, sWhereProveedor, DtGeneral.IdPersonal);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existe información para mostrar.");
                    if (rdoProveedor.Checked)
                    {
                        rdoProveedor.Focus();
                    }
                    else
                    {
                        rdoTodos.Focus();
                    }
                }
            }
            else
            {
                Error.GrabarError(leer, "");
                General.msjUser("Ocurrió un error al obtener la información.");
            }

        }
        #endregion Botones

        #region Funciones
        private void InicializarToolbar(bool bNuevo, bool bEjecutar)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
        }
        #endregion Funciones

        private void generarOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sIdProveedor = "", sFolioPedido = "", sStatus = "";
            int iRenglon = Grid.ActiveRow;

            sIdProveedor = Grid.GetValue(iRenglon, (int)Cols.IdProveedor);
            sFolioPedido = Grid.GetValue(iRenglon, (int)Cols.FolioPedido);
            sStatus = Grid.GetValue(iRenglon, (int)Cols.Status);

            if (sStatus.ToUpper() != "Confirmado".ToUpper())
            {
                General.msjUser("El Pre-Pedido seleccionado no esta Confirmado.");
            }
            else
            {
                FrmCom_GenerarOrdenDeCompraProveedor OrdCom = new FrmCom_GenerarOrdenDeCompraProveedor();
                OrdCom.MostrarOrdenDeCompraProveedor(sIdProveedor, sFolioPedido);

                Grid.Limpiar(false);
                btnEjecutar_Click(null, null);
            }
        }

        private void rdoProveedor_CheckedChanged(object sender, EventArgs e)
        {
            Grid.Limpiar(false);
            if (rdoProveedor.Checked)
            {
                txtProveedor.Enabled = true;
            }
            else
            {
                txtProveedor.Text = "";
                lblProveedor.Text = "";
                txtProveedor.Enabled = false;
            }
        } 
    }
}
