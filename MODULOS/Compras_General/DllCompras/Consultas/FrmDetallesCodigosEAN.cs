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
    public partial class FrmDetallesCodigosEAN : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;        

        string sEmpresa = "";
        string sEstado = "";        
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");        

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, CodigoEAN = 2, Descripcion = 3, Cantidad = 4, Importe = 5
        }

        public FrmDetallesCodigosEAN()
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
            //CargarGrid();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            ////Fg.IniciaControles(this, true);
            ////myGrid.Limpiar(false);
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;            
        }

        public void CargarPantalla(string Empresa, string Estado, string Proveedor, string IdClaveSSA, string FechaIni, string FechaFin, bool bFecha) 
        {
            Fg.IniciaControles(this, true);

            sEmpresa = Empresa;
            sEstado = Estado;

            string sWhereFechas = "";


            if (bFecha)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), E.FechaRegistro, 120) Between '{0}' and '{1}' ", FechaIni, FechaFin);
            }

            string sSql = string.Format(" Select E.Folio, D.CodigoEAN, P.Descripcion, D.Cantidad, D.Importe, " +
                        " E.IdProveedor, E.Proveedor, P.ClaveSSA, P.DescripcionSal, P.IdClaveSSA_Sal, P.Presentacion_ClaveSSA, P.ContenidoPaquete_ClaveSSA  " +
	                    " From vw_OrdenesCompras_Claves_Enc E (Nolock) " +
	                    " Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock) " +
		                    " On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioOrden ) " +
	                    " Inner Join vw_Productos_CodigoEAN P (Nolock) On ( D.CodigoEAN = P.CodigoEAN ) " + 
	                    " Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = '{2}' " + 
	                    " And E.IdProveedor = '{3}' And P.IdClaveSSA_Sal = '{4}' {5}  Order By E.Folio",
                        sEmpresa, sEstado, sFarmacia, Proveedor, IdClaveSSA, sWhereFechas);

            myGrid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarGrid()");
                General.msjError("Ocurrió un error al obtener la lista de Ordenes de Compras.");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    General.msjAviso("No se encontro nnformación con los criterios especificados, verifique.");
                    this.Hide();
                }
                else
                {
                    CargaDatos(); 
                }
            }
        }

        private void CargaDatos()
        {
            lblClaveSSA.Text = myLeer.Campo("ClaveSSA");
            lblIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = myLeer.Campo("DescripcionSal");

            lblPresentacion.Text = myLeer.Campo("Presentacion_ClaveSSA");
            lblContPaquete.Text = myLeer.Campo("ContenidoPaquete_ClaveSSA");

            lblIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor");

            myGrid.LlenarGrid(myLeer.DataSetClase);

            this.ShowDialog();
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
