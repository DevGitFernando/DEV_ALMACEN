using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;

namespace Almacen.Pedidos
{
    public partial class FrmRegresarASurtido : FrmBaseExt
    {
        public string sObservaciones = "";
        public bool bContinua = false;

        public FrmRegresarASurtido()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bContinua = true;
            sObservaciones = txtObservaciones.Text;
            if (sObservaciones != "")
            {
                this.Hide();
            }
            else
            {
                General.msjAviso("Falta capturar las observaciones, verifique porfavor.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bContinua = false;
            this.Hide();
        }
    }
}
