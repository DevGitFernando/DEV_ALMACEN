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

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmListadoPedidosDistribuidor : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        FrmRegistroSalidaPedidos Salidas;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        private enum Cols
        {
            Ninguna = 0,
            IdEmpresa = 1, IdEstado = 2, IdFarmacia = 3, Farmacia = 4, Folio = 5, FechaRegistro = 6
        }

        public FrmListadoPedidosDistribuidor()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            Grid = new clsGrid(ref grdExistencia, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            Grid.Limpiar(false);
            ObtenerPedidos();
        }

        #endregion Botones

        #region Grid 
        private void ObtenerPedidos()
        {
            string sSql = string.Format(" Select IdEmpresa, IdEstado, IdFarmacia, Farmacia, Folio, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro " +
                " From vw_PedidosEnvioEnc (NoLock) " +
                " Where Folio Not In ( Select FolioPedidoRef as Folio From PedidosDistEnc(NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' ) ", sEmpresa, sEstado, sFarmacia);

            Grid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información para mostrar.");
                }
                else
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                }
            }
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFolio = "", sFarmacia = "";
            int iRenglon = Grid.ActiveRow;

            sFolio = Grid.GetValue(iRenglon, (int)Cols.Folio);
            sFarmacia = Grid.GetValue(iRenglon, (int)Cols.IdFarmacia);

            if (sFolio != "")
            {
                Salidas = new FrmRegistroSalidaPedidos();
                Salidas.MostrarDetalles(sEmpresa, sEstado, sFarmacia, sFolio);
                btnNuevo_Click(null, null);
            }
        }
        #endregion Grid        

    }
}
