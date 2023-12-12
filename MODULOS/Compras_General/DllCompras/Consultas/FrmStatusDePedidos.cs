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

namespace DllCompras.Consultas
{
    public partial class FrmStatusDePedidos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;
        clsConsultas query;

        string sIdPersonal = DtGeneral.IdPersonal;

        FrmDetallesPedidos Detalles;
        FrmListadoOCProductos Ordenes;

        private enum Cols
        {
            Ninguna = 0,
            FolioPedido = 1, IdFarmacia = 2, Farmacia = 3, IdTipoPedido = 4, TipoDeClaves = 5, TipoPedido = 6, StatusPedido = 7,
            StatusDePedido = 8, FolioPed_Unidad = 9, Fecha = 10
        }

        public FrmStatusDePedidos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version, false);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdPedidosDisponibles, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            Cargar_Empresas();
        }

        private void FrmStatusDePedidos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Combos
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
        #endregion Combos

        #region Eventos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();            
        }

        private void grdPedidosDisponibles_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            
        }
        #endregion Eventos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid = new clsGrid(ref grdPedidosDisponibles, this);
            grid.Limpiar(false);            
            cboEmpresas.Focus();
            cboEmpresas.Enabled = true;
            cboEdo.Enabled = true;
            cboEmpresas.SelectedIndex = 0;
            cboEdo.SelectedIndex = 0;

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
            CargarDatosPedidos();
        }
        #endregion Botones

        #region Funciones
        private bool CargarDatosPedidos()
        {
            bool bRegresa = true;
            string sEstado = "", sEmpresa = "", sSql = "", sWhereFechas = "";

            if (!chkTodasLasFechas.Checked)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            
            if (cboEmpresas.SelectedIndex > 0 && cboEdo.SelectedIndex > 0)
            {
                sEmpresa = cboEmpresas.Data;
                sEstado = cboEdo.Data;

                grid.Limpiar();
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    sSql = string.Format(" Select Folio, IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, " + 
                        " StatusPedido, StatusDePedido, " +
                                        " FolioPedidoUnidad, Convert(varchar (10), FechaRegistro, 120) As Fecha  " +
                                        " From vw_COM_OCEN_REG_PedidosEnc (Nolock) " +
                                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' {2} ",
                                        cboEmpresas.Data, cboEdo.Data, sWhereFechas );
                }
                else
                {
                    sSql = string.Format(" Select Folio, IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, " + 
                        " StatusPedido, StatusDePedido, Folio, " +
                                        " Convert(varchar (10), FechaRegistro, 120) As Fecha  " +
                                        " From vw_COM_OCEN_PedidosEnc (Nolock) " +
                                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' {2} ",
                                        cboEmpresas.Data, cboEdo.Data, sWhereFechas );
                }

                if (!leer.Exec(sSql))
                {
                    Error.LogError(leer.MensajeError);
                    General.msjError("Ocurrió un error al obtener la lista de Pedidos.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        grid.LlenarGrid(leer.DataSetClase, false, false);
                        grdPedidosDisponibles.Focus();
                        cboEmpresas.Enabled = false;
                        cboEdo.Enabled = false;
                    }
                    else
                    {
                        General.msjUser("No se encontro información de Pedidos.");
                        bRegresa = false;
                        cboEdo.Focus();
                    }
                }
            }
            else
            {
                General.msjAviso("No ha seleccionado Empresa y Estado, verifique.");
                cboEmpresas.Focus();
            }

            grid.BloqueaColumna(true);

            return bRegresa;
        }
        #endregion Funciones

        #region Eventos_Menu
        private void verDetallesDePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void verDetallesDePedidoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string sTipoPedido = "", sEmpresa = "", sEstado = "", sFolio = "", sFarmacia = "", sFolioUnidad = "";

            sEmpresa = cboEmpresas.Data;
            sEstado = cboEdo.Data;
            sFarmacia = grid.GetValue(grid.ActiveRow, (int)Cols.IdFarmacia);
            sTipoPedido = grid.GetValue(grid.ActiveRow, (int)Cols.IdTipoPedido);
            sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.FolioPedido);
            sFolioUnidad = grid.GetValue(grid.ActiveRow, (int)Cols.FolioPed_Unidad);

            Detalles = new FrmDetallesPedidos();
            Detalles.MostrarPedido(sEmpresa, sEstado, sFarmacia, sTipoPedido, sFolio, sFolioUnidad);
            Detalles = null;
        }

        private void desabilitarPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sFolioUnidad = "", sFolio = "", sFecha = "", sEmpresa = "", sEstado = "", sUnidad = "";
            
            sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.FolioPedido);
            sFolioUnidad = grid.GetValue(grid.ActiveRow, (int)Cols.FolioPed_Unidad);
            sFecha = grid.GetValue(grid.ActiveRow, (int)Cols.Fecha);
            sUnidad = grid.GetValue(grid.ActiveRow, (int)Cols.IdFarmacia);

            sEmpresa = cboEmpresas.Data;
            sEstado = cboEdo.Data;

            Ordenes = new FrmListadoOCProductos();
            Ordenes.CargarFoliosOrdenCompras(sEmpresa, sEstado, sFolio, sFolioUnidad, sFecha, sUnidad );
            Ordenes = null;
        }
        #endregion Eventos_Menu
        
    }
}
