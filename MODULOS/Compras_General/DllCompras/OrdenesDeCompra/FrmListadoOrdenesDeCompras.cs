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
    public partial class FrmListadoOrdenesDeCompras : FrmBaseExt
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

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Proveedor = 2, FechaRegistro = 3, Status = 4, FechaColocacion = 5, Importe = 6
        }

        public FrmListadoOrdenesDeCompras()
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
            myGrid.AjustarAnchoColumnasAutomatico = true;
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
            CargarFoliosOrdenCompras();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            myGrid.Limpiar(false);

            txtProveedor.Focus();
        }

        private void CargarFoliosOrdenCompras() 
        {
            string sWhereProv = "", sWhereFechas = "", sWherePer = "";

            if (txtProveedor.Text.Trim() != "")
            {
                sWhereProv = string.Format(" And IdProveedor = '{0}' ", txtProveedor.Text );
            }

            if (txtIdPersonal.Text.Trim() != "")
            {
                sWherePer = string.Format(" And IdPersonal = '{0}' ", txtIdPersonal.Text);
            }

            if (!chkTodasLasFechas.Checked)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            

            string sSql = string.Format( " Select Folio, Proveedor, CONVERT(varchar(10),FechaRegistro,120) As FechaRegistro, " +
	                            " Case When Status = 'C' Then 'CANCELADA' " +
			                           " When Status = 'OC' Then 'ORDEN COLOCADA' " + 
		                            " Else 'ORDEN NO COLOCADA' " +
		                            " End As StatusOrdenCompra, " +
                            " CONVERT(varchar(10),FechaColocacion,120) As FechaColocacion, Sum(Importe) As ImporteTotal " +
                            " From vw_Impresion_OrdenesCompras_Claves (Nolock) " +
                            " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'  {3}  {4}  {5} " +
                            " Group By Folio, Proveedor, FechaRegistro, Status, FechaColocacion " +
                            " Order By Folio ", sEmpresa, sEstado, sFarmacia, sWhereProv, sWherePer, sWhereFechas);

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
                }
                else
                {
                    General.msjAviso("No se encontro información con los criterios especificados, verifique.");
                }
            }
        }
        #endregion Funciones

        #region Eventos_Grid
        private void grdOrdenCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFolioOrden = "";

            sFolioOrden = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);

            if (sFolioOrden.Trim() != "")
            {
                FrmOrdenCompraManual OrdenCompra = new FrmOrdenCompraManual();
                OrdenCompra.MostrarDetalleOrdenCompra(sFolioOrden, true);
            }
        }
        #endregion Eventos_Grid

        #region Eventos_Proveedor
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
        #endregion Eventos_Proveedor

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "")
            {
                myLeer = new clsLeer(ref ConexionLocal);
                myLeer.DataSetClase = Consultas.Personal(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtIdPersonal.Text, "txtIdPersonal_Validating");
                if (myLeer.Leer())
                {
                    txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                    lblNomPersonal.Text = myLeer.Campo("NombreCompleto");
                }
            }
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Personal("txtIdPersonal_KeyDown", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                {
                    txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                    lblNomPersonal.Text = myLeer.Campo("NombreCompleto");
                }
            }
        }
    }
}
