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

namespace Farmacia
{
    public partial class FrmCodigoDeBarras : FrmBaseExt 
    {
        clsGrid grid; 

        public FrmCodigoDeBarras()
        {
            InitializeComponent();

            grid = new clsGrid(ref grdBarras, this);
            grid.ImprimirEtiquetas = true;

            this.ShowInTaskbar = true; 
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.ExportarPDF(); 
        }
    }
}
