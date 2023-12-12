using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.OrdenesDeCompra;

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmListadoOrdenesDeCompraStatus : FrmBaseExt
    {
        clsLeer leer;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsConsultas Consultas;
        clsListView lst;
        clsGetOrdenDeCompra OrdenCompra;
        private enum Cols { IdEmpresa = 1, IdEstado = 2, IdFarmacia = 3, FolioOrden	= 4, IdProveedor = 5, proveedor = 6, IdStatus = 7, Status = 8 }


        public FrmListadoOrdenesDeCompraStatus()
        {
            InitializeComponent();

            lst = new clsListView(lstPedidos);
            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        private void FrmListadoOrdenesDeCompraStatus_Load(object sender, EventArgs e)
        {
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            leer.Conexion.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            lstPedidos.ContextMenuStrip = menu;
            ActulizarStatus();
            CargarEmpresas();
        }

        private void cboEmpresas_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref con);

            lst.LimpiarItems();

            leer.DataSetClase = Consultas.ComboEstados("LlenaEstados");
            if (leer.Leer())
            {
                cboEdo.Clear();
                cboEdo.Add("0", "<< Seleccione >>");
                cboEdo.Add(leer.DataSetClase, true, "IdEstado", "EstadoNombre");
                cboEdo.SelectedIndex = 0;

                cboFar.Clear();
                cboFar.Add("0", "<< Seleccione >>");
                cboFar.SelectedIndex = 0;
            }
        }

        private void cboEdo_Validating(object sender, CancelEventArgs e)
        {
            string sIdEdo = "";
            lst.LimpiarItems();
            leer = new clsLeer(ref con);
            sIdEdo = cboEdo.Data;
            leer.DataSetClase = Consultas.ComboFarmacias(sIdEdo, "LlenaFarmacias");
            if (leer.Leer())
            {
                cboFar.Clear();
                cboFar.Add("0", "<< Seleccione >>");
                cboFar.Add(leer.DataSetClase.Tables[0].Select("IdEstado = '" + cboEdo.Data + "'"), true, "IdFarmacia", "NombreFarmacia_Cbo");
                cboFar.SelectedIndex = 0;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sWhere = "";

            if (ValidaDatos())
            {
                if (txtInicial.Text.Trim() != "")
                {
                   sWhere = string.Format(" And FolioOrden >= '{0}'", txtInicial.Text.Trim());
                }

                if (txtFinal.Text != "")
                {
                    sWhere += string.Format(" And FolioOrden <= '{0}'", txtFinal.Text.Trim());
                }

                string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProveedor, Proveedor, IdStatus, Nombre " +
                        "From vw_COM_OCEN_OrdenesCompra_Claves_Status " +
                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' {3}" +
                        "Order By IdEmpresa, IdEstado, IdFarmacia, FolioOrden ",
                            cboEmpresas.Data, cboEdo.Data, cboFar.Data, sWhere);

                lst.LimpiarItems();

                if (con.Abrir())
                {
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "btnEjecutar_Click()");
                        General.msjError("Ocurrió un error al obtener la lista de Ordenes de Compras.");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            lst.CargarDatos(leer.DataSetClase);
                        }
                        else
                        {
                            General.msjAviso("No se encontro información con los criterios especificados, verifique.");
                        }
                    }
                }
                con.Cerrar();
            }
        }

        private void txtInicial_Validating(object sender, CancelEventArgs e)
        {
            if (txtInicial.Text != "")
            {
                txtInicial.Text = Fg.PonCeros(txtInicial.Text, 8);
            }
            lst.LimpiarItems();
        }

        private void txtFinal_Validating(object sender, CancelEventArgs e)
        {
            if (txtFinal.Text != "")
            {
                txtFinal.Text = Fg.PonCeros(txtFinal.Text, 8);
            }
            lst.LimpiarItems();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (lst.GetValue(lst.RenglonActivo, (int)Cols.IdStatus) != "")
            {
                OrdenCompra = new clsGetOrdenDeCompra(General.DatosConexion, cboEmpresas.Data, cboEdo.Data, DtGeneral.FarmaciaConectada, cboFar.Data, lst.GetValue(lst.RenglonActivo, (int)Cols.FolioOrden));
                OrdenCompra.SiguienteStatus(lst.GetValue(lst.RenglonActivo, (int)Cols.IdStatus), false);
            }
            btnEjecutar_Click(this, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lst.LimpiarItems();
            CargarEmpresas();

            txtInicial.Text = "";
            txtFinal.Text = "";
        }

        private void CargarEmpresas()
        {
            lst.LimpiarItems();
            cboEmpresas.Clear();
            cboEmpresas.Add("0", "<< Seleccione >>");
            cboEmpresas.Add(Consultas.Empresas("CargarEmpresas()"), true, "IdEmpresa", "NombreEmpresa_Cbo");
            cboEmpresas.SelectedIndex = 0;

            cboEdo.Clear();
            cboEdo.Add("0", "<< Seleccione >>");
            cboEdo.SelectedIndex = 0;

            cboFar.Clear();
            cboFar.Add("0", "<< Seleccione >>");
            cboFar.SelectedIndex = 0;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0 && bRegresa)
            {
                General.msjAviso("No ha seleccionado la Empresa, verifique.");
                bRegresa = false;
            }

            if (cboEdo.SelectedIndex == 0 && bRegresa)
            {
                General.msjAviso("No ha seleccionado el Estado, verifique.");
                bRegresa = false;
            }

            if (cboFar.SelectedIndex == 0 && bRegresa)
            {
                General.msjAviso("No ha seleccionado la Farmacia, verifique.");
                bRegresa = false;
            }

            if (txtInicial.Text == "" && bRegresa)
            {
                General.msjAviso("No ha seleccionado el Folio Inicial, verifique.");
                bRegresa = false;
            }

            if (txtFinal.Text == "" && bRegresa)
            {
                General.msjAviso("No ha seleccionado el Folio Final, verifique.");
                bRegresa = false;
            }

            return bRegresa;
        }

        private void ActulizarStatus()
        {
            string sSql = string.Format("Exec spp_CargaOrdenesCompra_Claves_Status");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActulizarStatus");
                General.msjError("Ocurrió un error al actualizar las Ordenes de Compras.");
            }
        }




    }
}
