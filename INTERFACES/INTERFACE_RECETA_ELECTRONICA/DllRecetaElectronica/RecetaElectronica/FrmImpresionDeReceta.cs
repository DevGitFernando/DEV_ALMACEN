using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;


namespace DllRecetaElectronica.ECE
{
    public partial class FrmImpresionDeReceta : FrmBaseExt
    {
        clsListView lst;
        clsDatosCliente DatosCliente;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sValorSeleccionado = "";

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmImpresionDeReceta()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            lst = new clsListView(listviewRecetas);
        }

        private void FrmImpresionDeReceta_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtReceta.Text = "";
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            lst.LimpiarItems();

            if (txtReceta.Text.Trim() != "" | txtNombre.Text.Trim() != "" | txtApPaterno.Text.Trim() != "" | txtMaterno.Text.Trim() != "")
            {

                sSql = string.Format("Exec spp_INT_RE_AMPM__RecetasElectronicas_0008_ObtenerRecetasParaImpresion " +
                    "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                    " @FolioReceta = '{3}', @NombreBeneficiario = '{4}', @ApPaternoBeneficiario = '{5}', @ApMaternoBeneficiario = '{6}'",
                    sIdEmpresa, sIdEstado, sIdFarmacia, txtReceta.Text.Trim(), txtNombre.Text.Trim(), txtApPaterno.Text.Trim(), txtMaterno.Text.Trim());


                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "SeleccionarRecetasParaSurtido");
                    General.msjAviso("Ocurrió un error al obtener la información de recetas.");
                }
                else
                {
                    lst.CargarDatos(leer.DataSetClase, true, false);
                }
            }
            else
            {
                General.msjAviso("Debe usar al menos un filtro.");
            }
        }

        private void listviewRecetas_DoubleClick(object sender, EventArgs e)
        {
            sValorSeleccionado = lst.LeerItem().Campo("Secuenciador");

            if (sValorSeleccionado != "")
            {
                bool bRegresa = false;

                DatosCliente.Funcion = "ImprimirCompra()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                // clsReporteador Reporteador = new clsReporteador(ref myRpt, ref DatosCliente); 

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "INT_Receta__AMPM.rpt";

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@FolioReceta", sValorSeleccionado);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
    }
}
