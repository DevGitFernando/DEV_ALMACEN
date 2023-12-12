using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 

namespace Almacen.Pedidos
{
    public partial class FrmModificarCantidadSurtida : FrmBaseExt 
    {
        public bool AplicarCambio = false;
        public int CantidadRequerida = 0;
        public int CantidadAnterior = 0;
        public int CantidadNueva = 0;
        public string Observaciones = ""; 

        public FrmModificarCantidadSurtida()
        {
            InitializeComponent(); 
        }

        private void FrmModificarCantidadSurtida_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            lblCantidadAnterior.Text = CantidadAnterior.ToString();
            txtCantidadNueva.Minimum = 0;
            txtCantidadNueva.Maximum = CantidadRequerida;
            txtCantidadNueva.Value = CantidadAnterior;

            txtObservaciones.Text = Observaciones; 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AplicarCambio = true; 
            CantidadNueva = Convert.ToInt32(txtCantidadNueva.Value);
            Observaciones = txtObservaciones.Text; 
            this.Hide(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }
    }
}
