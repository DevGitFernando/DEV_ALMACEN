using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmOrdenesDeComprasColocadasProveedor : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer, myLeer3;
        clsLeer myLlenaDatos;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsListView lst1;
        clsListView lst2;
        clsListView lst3;
        clsDatosCliente DatosCliente;
        //clsExportarExcelPlantilla xpExcel;

        public FrmOrdenesDeComprasColocadasProveedor()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            lst1 = new clsListView(lstPedidos);
            lst2 = new clsListView(lstUltima);
            lst3 = new clsListView(lstCompras);
            myLeer = new clsLeer(ref ConexionLocal);
            myLeer3 = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
        }

        private void FrmOrdenesDeComprasColocadasProveedor_Load(object sender, EventArgs e)
        {
            lstPedidos.ContextMenuStrip = menu;
            Limpiar();
        }

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            Fg.IniciaControles();
            txtProveedor.Text = "";
            lblNomProv.Text = "";
            
            dtpFechaInicial.Value = General.FechaSistemaObtener().AddYears(-1);
            dtpFechaInicial.MaxDate = General.FechaSistemaObtener();
            dtpFechaFinal.MaxDate = General.FechaSistemaObtener();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
            if (txtProveedor.Text.Trim() != "")
            {
                string sSql = string.Format("Select IdClaveSSA_Sal, ClaveSSA, DescripcionSAl, Sum(Cantidad) As Cantidad, Sum(Importe) As Importe " +
                                "From COM_OCEN_OrdenesCompra_Claves_Enc M (NoLock) " +
                                "Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) " +
                                "   On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioOrden = D.FolioOrden )" +
                                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )" +
                                "Where M.Status = 'OC' And IdProveedor = '{0}' And Convert(Varchar(10), FechaRegistro, 120) Between '{1}' And '{2}' " +
                                "Group By IdClaveSSA_Sal, ClaveSSA, DescripcionSAl Order By ClaveSSA",
                                 txtProveedor.Text.Trim(), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));
                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        lst1.LimpiarItems();
                        lst1.CargarDatos(myLeer.DataSetClase);
                    }
                }
            }
            else
            {
                General.msjAviso("No a seleccionado el Proveedor, verifique por favor.");
            }
        }

        private void btnUltimaCompra_Click(object sender, EventArgs e)
        {
            if (lst1.GetValue(lst1.RenglonActivo, 1) != "")
            {
                string sSql = string.Format("Select Top 1 M.FolioOrden As Folio, Convert(varchar(10), FechaRegistro, 120) As Fecha, D.Importe, Cantidad " +
                                "From COM_OCEN_OrdenesCompra_Claves_Enc M (NoLock) " +
                                "Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) " +
                                "	On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioOrden = D.FolioOrden ) " +
                                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) " +
                                "Where M.Status = 'OC' And IdProveedor = '{0}' And IdClaveSSA_Sal = '{1}' " +
                                "Order By M.FolioOrden Desc", txtProveedor.Text.Trim(), lst1.GetValue(lst1.RenglonActivo, 1));

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        grpUltima.Text = "Ultima Compra: " + lst1.GetValue(lst1.RenglonActivo, 2) + " - " + lst1.GetValue(lst1.RenglonActivo, 3);
                        lst2.LimpiarItems();
                        lst2.CargarDatos(myLeer.DataSetClase);
                    }
                }
            }
        }

        private void comprasDelProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lst1.GetValue(lst1.RenglonActivo, 1) != "")
            {
                string sSql = string.Format("Select M.Idproveedor, R.Nombre As Proveedor, M.FolioOrden, Convert(varchar(10), FechaRegistro, 120) As Fecha, " +
                            "D.Importe,  Cantidad, FE.Farmacia As EntregarEn " +
                            "From COM_OCEN_OrdenesCompra_Claves_Enc M (NoLock) " +
                            "Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (NoLock) " +
                            "		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioOrden = D.FolioOrden ) " +
                            "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )" +
                            "Inner Join CatProveedores R (NoLock) On (M.Idproveedor = R.Idproveedor) " +
                            "Inner Join vw_Farmacias FE (NoLock) On (M.EstadoEntrega = FE.IdEstado And M.EntregarEn = FE.IdFarmacia) " +
                            "Where M.Status = 'OC' And IdClaveSSA_Sal = '{0}' And Convert(Varchar(10), FechaRegistro, 120) Between '{1}' And '{2}' " +
                            "Order By M.FolioOrden Desc", lst1.GetValue(lst1.RenglonActivo, 1), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));

                if (myLeer3.Exec(sSql))
                {
                    if (myLeer3.Leer())
                    {
                        grpCompras.Text = lst1.GetValue(lst1.RenglonActivo, 2) + " - " + lst1.GetValue(lst1.RenglonActivo, 3);
                        lst3.LimpiarItems();
                        lst3.CargarDatos(myLeer3.DataSetClase, false, false);
                    }
                }
            }
        }

        private void btnExportarPlantilla_Click(object sender, EventArgs e)
        {
            //int iCol = 2;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpte_OrdenesColocadas.xls";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpte_OrdenesColocadas.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla("COM_Rpte_OrdenesColocadas.xls"))
            //    {
            //        xpExcel.GeneraExcel();
            //        xpExcel.Agregar("De la Fechas : " + General.FechaYMD(dtpFechaInicial.Value) + " a la " + General.FechaYMD(dtpFechaFinal.Value), 3, 2);
            //        xpExcel.Agregar("De la clave SSA : " + lst1.GetValue(lst1.RenglonActivo, 2) + " - " + lst1.GetValue(lst1.RenglonActivo, 3), 4, 2);

            //        myLeer3.RegistroActual = 1;
            //        for (int iRow = 8; myLeer3.Leer(); iRow++)
            //        {
            //            iCol = 2;
            //            xpExcel.Agregar(myLeer3.Campo("Idproveedor"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("Proveedor"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("FolioOrden"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("Fecha"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("Importe"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("Cantidad"), iRow, iCol++);
            //            xpExcel.Agregar(myLeer3.Campo("EntregarEn"), iRow, iCol++);
            //        }

            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //    this.Cursor = Cursors.Default;
            //}
        }
    }
}
