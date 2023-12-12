using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace Almacen.OrdenCompra
{
    public partial class FrmCambioDePrecio : FrmBaseExt
    {
        public bool PrecioAsignado = false;
        public double dPrecio = 0.0000;
        public string sObservaciones = "";

        public FrmCambioDePrecio()
        {
            InitializeComponent();
        }

        private void FrmCambioDePrecio_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtPrecio.Value = Convert.ToDecimal(dPrecio);
            txtPrecioAnterior.Text = dPrecio.ToString();
            txtObservaciones.Text = "";
            txtPrecioAnterior.Enabled = false; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtPrecio.Text) <= 0.0000)
            {
                General.msjAviso("No ha capturado un Precio Nuevo válido, verifique.");
            }
            else
            {
                if (txtObservaciones.Text.Trim().Length == 0)
                {
                    General.msjAviso("No ha capturado Observaciones válidas, verifique.");
                }
                else
                {
                    PrecioAsignado = true;
                    dPrecio = Convert.ToDouble(txtPrecio.Value);
                    sObservaciones = txtObservaciones.Text.Trim();
                    this.Hide();
                }
            }
        }
    }
}
