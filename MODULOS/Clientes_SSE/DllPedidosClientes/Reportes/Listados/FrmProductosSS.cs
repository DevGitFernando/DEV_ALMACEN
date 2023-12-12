using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using DllFarmaciaSoft;

namespace DllPedidosClientes.Reportes.Listados
{
    public partial class FrmProductosSS : FrmBaseExt
    {
        clsDatosCliente DatosCliente;

        public FrmProductosSS()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
        }

        private void FrmProductosSS_Load(object sender, EventArgs e)
        {
            rdoRptMedicamento.Checked = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            rdoRptMedicamento.Checked = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa;
            DatosCliente.Funcion = "btnImprimir_Click()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;

            myRpt.NombreReporte = "PtoVta_ProductosSS";

            myRpt.Add("NombreEmpresa", DtGeneralPedidos.EncabezadoPrincipal);

            if (rdoRptMedicamento.Checked == true)
            {
                myRpt.Add("TipoProducto", "02");
            }
            else
            {
                myRpt.Add("TipoProducto", "01");
            }

            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();

            bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada, InfoWeb, datosC);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
    }
}
