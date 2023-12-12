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

namespace MA_Facturacion.GenerarRemisiones
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
            //rdoTodo.Checked = true;
            lblTotalPiezas.Text = "";
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            lst.Limpiar();
            //string sConsignacionVenta = "";
            //string sWhere = string.Format("Where D.IdEmpresa = '{0}' And D.IdEstado = '{1}' And EnRemision = 0 ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);
            
            //if (rdoConsignacion.Checked)
            //{
            //    sConsignacionVenta = " And ClaveLote like '%*%'";
            //}

            //if (rdoVenta.Checked)
            //{
            //    sConsignacionVenta = " And ClaveLote Not like '%*%'";
            //}

            //sWhere += sConsignacionVenta;

            string sSql = string.Format("Exec spp_INT_MA__FACT_Rpt_SinRemisionar @IdEmpresa = '{0}', @IdEstado = '{1}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);

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
                    lst.AnchoColumna(1, 80);
                    lst.AnchoColumna(2, 350);
                    lst.AnchoColumna(7, 100);
                }
            }

            // Totaliza 
            lblTotalPiezas.Text = lst.TotalizarColumnaDouble(7).ToString("#,###,###,###,###0");

        }
    }
}
