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
    public partial class FrmConcentradoPedidosOCEN : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Contenido = 4, UnidadesAComprar = 5, UnidadesAsignadas = 6 
        }

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sNomPersonal = DtGeneral.NombrePersonal;
        string sFechaSistema = General.FechaYMD(GnCompras.FechaOperacionSistema, "-");

        // string sNombreTablaConcentrado = "";
        string sFolioPedido = "", sIdTipoPedido = "";
        string sUnidad = "";

        bool bExterna = false;

        public FrmConcentradoPedidosOCEN()
        {
            InitializeComponent(); 

            // Inicializar las Variables Generales 
            leer = new clsLeer(ref cnn);

            grid = new clsGrid(ref grdPedidos, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grid.Limpiar(false);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DllCompras.GnCompras.DatosApp, this.Name);

            // sNombreTablaConcentrado = " COM_OCEN_Concentrado_Pedidos_Claves ";
            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                //sNombreTablaConcentrado = " COM_OCEN_REG_Concentrado_Pedidos_Claves ";
                // sNombreTablaConcentrado = " COM_Concentrado_Pedidos_Claves_OCEN ";
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
            GnCompras.GenerarTablaPedidosClaves();
            if (!bExterna)
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
            else
            {
                InicializarToolBar(false, false);                
            }

            //lblIdPersonal.Text = DtGeneral.IdPersonal;
            //lblPersonal.Text = DtGeneral.NombrePersonal;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            ////string sSql = string.Format(" SELECT cp.IdClaveSSA, vp.ClaveSSA, vp.DescripcionSal, vp.ContenidoPaquete, " +
            ////    " cp.Cant_Requerida as Cant_Requerida, 0 as Cant_Asignada " +
            ////    " FROM {0} cp (NoLock) " +
            ////    " INNER JOIN  vw_ClavesSSA_Sales vp " +
            ////    " ON ( cp.IdClaveSSA = vp.IdClaveSSA_Sal  ) " +
            ////    " Where cp.IdEmpresa = '{1}' And cp.IdEstado = '{2}' And cp.IdFarmacia = '{3}' And cp.IdPersonal = '{4}' And cp.Cant_Requerida > 0 ",
            ////    sNombreTablaConcentrado, cboEmpresas.Data, cboEdo.Data, sFarmacia, sPersonal);

            if (bExterna)
            {
                ActualizarStatusPedido();
            }

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                sSql = string.Format(" Select IdClaveSSA, ClaveSSA, Descripcion, ContenidoPaquete, CantASurtir, 0 As Cant_Asignada " +
                                    " From vw_COM_OCEN_REG_PedidosDet_Claves (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' And IdTipoPedido = '{4}' And CantASurtir > 0 ",
                                    sEmpresa, sEstado, sUnidad, sFolioPedido, sIdTipoPedido);
            }
            else
            {
                sSql = string.Format(" Select IdClaveSSA, ClaveSSA, Descripcion, ContenidoPaquete, CantAComprar, 0 As Cant_Asignada " +
                                    " From vw_COM_OCEN_PedidosDet_Claves (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' And IdTipoPedido = '{4}' And CantAComprar > 0 ",
                                    sEmpresa, sEstado, sUnidad, sFolioPedido, sIdTipoPedido);
            }

            grid.Limpiar(false);
            //GnCompras.GenerarTablaPedidosClaves();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click"); 
            }
            else 
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    Cargar_CantidadesAsignadas();

                    if (bExterna)
                    {
                        InicializarToolBar(false, false);
                    }
                    else
                    {
                        InicializarToolBar(true, false);
                    }
                }
                else
                {
                    General.msjUser("No existe información para mostrar.");
                    if (bExterna)
                    {
                        this.Hide();
                    }
                }
            }
        }
        #endregion Botones

        #region Menu
        private void mostrarProveedoresPorClaveCodigoEANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string IdClaveSSA = grid.GetValue(grid.ActiveRow, (int)Cols.IdClaveSSA);
            //string CodigoEAN = grid.GetValue(grid.ActiveRow, (int)Cols.CodigoEAN);
            int iCantidad = grid.GetValueInt(grid.ActiveRow, (int)Cols.UnidadesAComprar);            

            FrmSeleccionProveedorProductos Pedidos = new FrmSeleccionProveedorProductos();
            Pedidos.MostrarProveedoresPorProducto(IdClaveSSA, iCantidad, sFolioPedido);
            //grid.SetValue(grid.ActiveRow, (int)Cols.UnidadesAsignadas, Pedidos.TotalUnidades);
            Cargar_CantidadesAsignadas();
        }

        private void mostrarListaDeProveedoresConPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCom_ProveedoresConPedidos Proveedores = new FrmCom_ProveedoresConPedidos();
            Proveedores.MostraListadoProveedores(cboEmpresas.Data, cboEdo.Data, sUnidad);
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

        private void Cargar_CantidadesAsignadas()
        {
            string sSql = "";
            for (int iRow = 1; grid.Rows >= iRow; iRow++)
            {
                sSql = string.Format("Select Sum(Cant_A_Pedir) As Cantidad From Com_Pedidos_Compras (NoLock) Where GUID = '{0}' And IdClaveSSA = {1} And Cant_A_Pedir > 0",
                        GnCompras.GUID, grid.GetValue(iRow, (int)Cols.IdClaveSSA));
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnEjecutar_Click");
                }
                else
                {
                    if (leer.Leer())
                    {
                        grid.SetValue(iRow, (int)Cols.UnidadesAsignadas, leer.CampoInt("Cantidad"));
                    }
                }
            }
        }

        #endregion Funciones

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();
        }

        public void MostrarClaves(string Empresa, string Estado, string Unidad, string FolioPedido, string IdTipoPedido, bool bEsExterna)
        {
            string sSql = "";

            Cargar_Empresas();
            cboEmpresas.Data = Empresa;
            Cargar_Estados();
            cboEdo.Data = Estado;
            cboEmpresas.Enabled = false;
            cboEdo.Enabled = false;
            sFolioPedido = FolioPedido;
            sUnidad = Unidad;
            bExterna = bEsExterna;

            if (bExterna)
            {
                sEmpresa = Empresa;
                sEstado = Estado;
                sFolioPedido = FolioPedido;
                sIdTipoPedido = IdTipoPedido;
            }

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                sSql = string.Format(" Select IdClaveSSA, ClaveSSA, Descripcion, ContenidoPaquete, CantASurtir, 0 As Cant_Asignada " +
                                    " From vw_COM_OCEN_REG_PedidosDet_Claves (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' And IdTipoPedido = '{4}' And CantASurtir > 0 ",
                                    Empresa, Estado, sUnidad, FolioPedido, IdTipoPedido);
            }
            else
            {
                sSql = string.Format(" Select IdClaveSSA, ClaveSSA, Descripcion, ContenidoPaquete, CantAComprar, 0 As Cant_Asignada " +
                                    " From vw_COM_OCEN_PedidosDet_Claves (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' And IdTipoPedido = '{4}' And CantAComprar > 0 ",
                                    Empresa, Estado, sUnidad, FolioPedido, IdTipoPedido);
            }
            
            grid.Limpiar(false);
            GnCompras.GenerarTablaPedidosClaves();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "MostrarClaves");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    Cargar_CantidadesAsignadas();
                    this.ShowDialog();
                }
                else
                {
                    General.msjUser("No existe información para mostrar");
                }
            }
        }

        private void ActualizarStatusPedido()
        {
            int iOpcion = 0;
            string sSql = "";

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                sSql = string.Format(" Exec spp_Mtto_COM_OCEN_REG_ActualizaStatus '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                    sEmpresa, sEstado, sFolioPedido, sIdTipoPedido, DtGeneral.IdPersonal, iOpcion, sUnidad);
            }
            else
            {
                sSql = string.Format(" Exec spp_Mtto_COM_OCEN_ActualizaStatus '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                                    sEmpresa, sEstado, sFolioPedido, sIdTipoPedido, DtGeneral.IdPersonal, iOpcion, sUnidad);                
            }
            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizarStatusPedido()");
                General.msjError(" Ocurrió Un Error al Actualizar Status del Pedido ");
            }            
        }
    }
}
