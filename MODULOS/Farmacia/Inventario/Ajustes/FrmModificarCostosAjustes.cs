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

namespace Farmacia.Inventario
{
    public partial class FrmModificarCostosAjustes : FrmBaseExt 
    {
        public bool bAplicarCambio = false;
        public bool bBaseDiferente = false;
        public decimal CostoBase = 0;
        public decimal CostoAnterior = 0;
        public decimal CostoNuevo = 0;

        public FrmModificarCostosAjustes()
        {
            InitializeComponent(); 
        }

        private void FrmModificarCostosAjustes_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            lblCostoBase.Text = CostoBase.ToString();
            lblCostoAnteriorAnterior.Text = CostoAnterior.ToString();
            txtCostoNuevo.Minimum = 0;
            txtCostoNuevo.Value = Convert.ToDecimal(CostoAnterior);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bAplicarCambio = true;

            bBaseDiferente = false;
            
            CostoNuevo = Convert.ToDecimal(txtCostoNuevo.Value);

            if (CostoBase != CostoNuevo)
            {
                bBaseDiferente = true;
            }


            this.Hide(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        

    }
}
