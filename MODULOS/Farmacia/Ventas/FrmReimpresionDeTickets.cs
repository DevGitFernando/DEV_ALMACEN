using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

namespace Farmacia.Ventas
{
    public partial class FrmReimpresionDeTickets : FrmBaseExt
    {
        clsDatosCliente DatosCliente;
        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsConsultas query;
        clsLeer leer = new clsLeer();
        double dImporte = 0;

        public FrmReimpresionDeTickets()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "Impresion");
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Ninguno);

            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, false);

            this.Height = 135;
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            btnImprimir.Enabled = false;
            rdoVentaContado.Checked = false;
            rdoVentaCredito.Checked = false;

            if (txtFolio.Text.Trim() != "")
            {
                leer.DataSetClase = query.FolioEnc_Ventas(DtGeneral.EmpresaConectada,
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    btnImprimir.Enabled = true;
                    txtFolio.Enabled = false;
                    txtFolio.Text = leer.Campo("Folio");
                    dImporte = leer.CampoDouble("Total");

                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") == TipoDeVenta.Publico)
                        rdoVentaContado.Checked = true;

                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") == TipoDeVenta.Credito)
                        rdoVentaCredito.Checked = true;

                    rdoVentaContado.Enabled = false;
                    rdoVentaCredito.Enabled = false;

                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Folio de venta no encontrado, verifique.");                    
                }
            }
            //else
            //{
            //    General.msjUser("Ingrese un Folio por favor");
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;
            Fg.IniciaControles();

            rdoVentaContado.Enabled = true;
            rdoVentaCredito.Enabled = true;

            rdoVentaContado.Checked = false;
            rdoVentaCredito.Checked = false;

            //// chkMostrarImpresionEnPantalla.Checked = false; 
            chkMostrarImpresionEnPantalla.Checked = DtGeneral.EsAlmacen;
            SendKeys.Send("{TAB}");
            txtFolio.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            txtFolio_Validating(this, null);

            if (txtFolio.Text.Trim() == "")
            {
                General.msjUser("No ha capturado un Folio de Venta para reeimpresión, verifique.");
            }
            else 
            {
                if (rdoVentaContado.Checked)
                {
                    VtasImprimir.TipoDeReporte = TipoReporteVenta.Contado;
                }

                if (rdoVentaCredito.Checked)
                {
                    VtasImprimir.TipoDeReporte = TipoReporteVenta.Credito;
                }

                VtasImprimir.Importe = dImporte; 
                VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;  
                VtasImprimir.Imprimir(txtFolio.Text);
            } 
        } 
    }
}
