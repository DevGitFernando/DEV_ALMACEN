using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

namespace DllFarmaciaAuditor.Reporte
{
    public partial class FrmClavesDispensadas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid myGrid;

        string sEmpresa = DllFarmaciaSoft.DtGeneral.EmpresaConectada;
        string sEstado = DllFarmaciaSoft.DtGeneral.EstadoConectado;
        string sFarmacia = DllFarmaciaSoft.DtGeneral.FarmaciaConectada;
        // string sFormato = "###,###,##0";

        private enum Cols
        {
            Ninguno = 0,
            IdEstado = 1, Estado = 2, IdFarmacia = 3, Farmacia = 4, ClaveSSA = 5, DescripcionSal = 6, Cantidad = 7
        }

        public FrmClavesDispensadas()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdClaves, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.Limpiar(false);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            myGrid.SetOrder(true); 

        }

        private void FrmClavesDispensadas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            myGrid.Limpiar(false);
            IniciaToolBar(true, true, false);
            dtpFechaInicial.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = String.Format("Select E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, P.ClaveSSA, P.DescripcionSal, " + 
                "Sum(D.CantidadVendida) as CantidadVendida " +
                "From VentasEnc E(NoLock) " +
                "Inner Join VentasDet D(NoLock) On( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta) " +
                "Inner Join vw_Productos P(NoLock) On (D.IdProducto = P.IdProducto ) " + 
                "Inner Join vw_Farmacias F(NoLock) On (D.IdEstado = F.IdEstado And D.IdFarmacia = F.IdFarmacia ) " +
                "Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = '{2}' " +
                "And Convert(varchar(10), E.FechaRegistro, 120 ) Between '{3}' And '{4}' " + 
                "Group By E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, P.ClaveSSA, P.DescripcionSal " + 
                "Order By P.ClaveSSA ",
                sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    IniciaToolBar(true, false, true);
                    dtpFechaInicial.Enabled = false;
                    dtpFechaFinal.Enabled = false;
                }
                else
                {
                    General.msjUser("No existe información para mostrar bajo los criterios seleccionados");
                }
            }
            else
            {
                Error.GrabarError(myLeer, "btnEjecutar_Click");
                General.msjUser("Ocurrió un error al obtener la informacion. Intente de nuevo por favor.");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            myGrid.ExportarExcel(true);
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

    }
}
