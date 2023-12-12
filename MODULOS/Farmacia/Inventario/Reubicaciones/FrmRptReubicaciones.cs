using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace Farmacia.Inventario.Reubicaciones
{
    public partial class FrmRptReubicaciones : FrmBaseExt 
    {
        clsDatosCliente DatosCliente;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        public FrmRptReubicaciones()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            Leer = new clsLeer(ref cnn);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmRptReubicaciones_Load(object sender, EventArgs e)
        {
            IniciarPantalla(); 
        }

        private void IniciarPantalla()
        {
            Fg.IniciaControles();
            txtPersonal.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarPantalla();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir("PtoVta_Reubicaciones.rpt");
        }

        private void btnImprimirConcentrado_Click(object sender, EventArgs e)
        {
            Imprimir("PtoVta_Reubicaciones_Confirmacion.rpt");
        }

        private void Imprimir(string NombreReporte)
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "ImprimirInventario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = NombreReporte;

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
            myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));
            myRpt.Add("@IdPersonal", txtPersonal.Text.Trim());

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }

        private void txtPersonal_TextChanged(object sender, EventArgs e)
        {
            lblPersonal.Text = "";
        }

        private void txtPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Leer.DataSetClase = Ayuda.Personal("txtClaveSSA_KeyDown", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                if (Leer.Leer())
                {
                    CargarDatosPersonal();
                }
            }
        }

        private void CargarDatosPersonal()
        {
            txtPersonal.Text = Leer.Campo("IdPersonal");
            lblPersonal.Text = Leer.Campo("NombreCompleto");
        }

        private void txtPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtPersonal.Text.Trim() != "")
            {
                Leer.DataSetClase = Consultas.Personal(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPersonal.Text.Trim(), "txtPersonal_Validating()");
                if (Leer.Leer())
                {
                    CargarDatosPersonal();
                }
                else
                {
                    txtPersonal.Focus();
                }
            }
        }
    }
}
