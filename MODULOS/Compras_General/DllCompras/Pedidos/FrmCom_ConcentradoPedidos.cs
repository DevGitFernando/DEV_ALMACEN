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


namespace DllCompras.Pedidos
{
    public partial class FrmCom_ConcentradoPedidos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, CodigoEAN = 3, Descripcion = 4, Contenido = 5, UnidadesAComprar = 6, UnidadesAsignadas = 7 
        }

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sNomPersonal = DtGeneral.NombrePersonal;
        string sFechaSistema = General.FechaYMD(GnCompras.FechaOperacionSistema, "-");

        string sNombreTablaConcentrado = ""; 

        public FrmCom_ConcentradoPedidos()
        {
            InitializeComponent(); 

            // Inicializar las Variables Generales 
            leer = new clsLeer(ref cnn);

            grid = new clsGrid(ref grdPedidos, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.Limpiar(false);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);

            sNombreTablaConcentrado = " COM_OCEN_Concentrado_Pedidos_Claves ";
            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                sNombreTablaConcentrado = " COM_OCEN_REG_Concentrado_Pedidos_Claves ";
            }

            Cargar_Empresas();
            cboEmpresas.Focus();
        }

        private void FrmCom_ConcentradoPedidos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(false);
            InicializarToolBar(true, true);

            if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format(" SELECT cp.IdClaveSSA, vp.ClaveSSA, cp.CodigoEAN, vp.DescripcionSal, vp.ContenidoPaquete, " +
                " ceiling(cp.Cant_Requerida / (vp.ContenidoPaquete * 1.0) ) as Cant_Requerida, 0 as Cant_Asignada " +
                " FROM {0} cp (NoLock) " +
                " INNER JOIN  vw_Productos_CodigoEAN vp " +
                " ON ( cp.IdClaveSSA = vp.IdClaveSSA_Sal AND cp.CodigoEAN = vp.CodigoEAN ) " +
                " Where cp.IdEmpresa = '{1}' And cp.IdEstado = '{2}' And cp.IdFarmacia = '{3}' And cp.IdPersonal = '{4}' And cp.Cant_Requerida > 0 ",
                sNombreTablaConcentrado, cboEmpresas.Data, cboEdo.Data, sFarmacia, sPersonal);

            grid.Limpiar(false);
            GnCompras.GenerarTablaPedidosClaves();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click"); 
            }
            else 
            {
                if (!leer.Leer())
                {
                    General.msjUser("No existe información para mostrar.");
                }
                else 
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    InicializarToolBar(true, false);
                }
            }
        }
        #endregion Botones

        #region Menu
        private void mostrarProveedoresPorClaveCodigoEANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IdClaveSSA = grid.GetValue(grid.ActiveRow, (int)Cols.IdClaveSSA);
            string CodigoEAN = grid.GetValue(grid.ActiveRow, (int)Cols.CodigoEAN);
            int iCantidad = grid.GetValueInt(grid.ActiveRow, (int)Cols.UnidadesAComprar);

            FrmCom_CriteriosParaPedidos Pedidos = new FrmCom_CriteriosParaPedidos();
            Pedidos.MostrarProveedoresPorClaveSSA(IdClaveSSA, CodigoEAN, iCantidad);
            grid.SetValue(grid.ActiveRow, (int)Cols.UnidadesAsignadas, Pedidos.TotalUnidades); 
        }

        private void mostrarListaDeProveedoresConPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCom_ProveedoresConPedidos Proveedores = new FrmCom_ProveedoresConPedidos();
            Proveedores.MostraListadoProveedores(cboEmpresas.Data, cboEdo.Data, "");
            btnEjecutar_Click(null, null);
        }      
        #endregion Menu 

        private void grdPedidos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        #region Funciones 
        private void InicializarToolBar(bool bNuevo, bool bEjecutar)
        {
            btnNuevo.Enabled = bNuevo;
            btnEjecutar.Enabled = bEjecutar;
        }

        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (leer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa);
            if (leer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add();
                cboEdo.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }
        #endregion Funciones

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();
        }
    }
}
