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
using DllFarmaciaSoft;

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmPendientesDeRemisionar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsListView lst;


        public FrmPendientesDeRemisionar()
        {
            InitializeComponent();
        }

        private void FrmPendientesDeRemisionar_Load(object sender, EventArgs e)
        {
            myLeer = new clsLeer(ref cnn);
            lst = new clsListView(lstDetalles);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lst.Limpiar();
            rdoTodo.Checked = true;
            lblTotalPiezas.Text = "";
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            lst.Limpiar();
            string sConsignacionVenta = "";
            string sWhere = string.Format("Where D.IdEmpresa = '{0}' And D.IdEstado = '{1}'", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);
            
            if (rdoConsignacion.Checked)
            {
                sConsignacionVenta = " And ClaveLote like '%*%'";
            }

            if (rdoVenta.Checked)
            {
                sConsignacionVenta = " And ClaveLote Not like '%*%'";
            }

            sWhere += sConsignacionVenta;

            string sSql = string.Format("Select 'Id Farmacia' = D.IdFarmacia, F.Farmacia, 'Número de piezas' = cast(Sum(CantidadVendida) as int)" +
                        "From VentasDet_Lotes  D(NoLock) " +
                        "Inner Join vw_Farmacias F (NoLock) On (D.IdEstado = F.IdEstado And D.IdFarmacia = F.IdFarmacia) " +
                        "{0} " +
                        "Group By D.IdFarmacia, F.Farmacia " +
                        "Order BY D.IdFarmacia", sWhere);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "btnEjecutar_Click()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else 
            {
                if (!myLeer.Leer())
                {
                    General.msjUser("No existe para mostrar.");
                } 
                else 
                {
                    lst.CargarDatos(myLeer.DataSetClase, true, true);
                    lst.AnchoColumna(1, 100);
                    lst.AnchoColumna(2, 400);
                    lst.AnchoColumna(3, 120);
                }
            }

            // Totaliza 
            lblTotalPiezas.Text = lst.TotalizarColumnaDouble(3).ToString("#,###,###,###,###0");

        }
    }
}
