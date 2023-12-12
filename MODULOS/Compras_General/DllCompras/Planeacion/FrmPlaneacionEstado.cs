using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllCompras.planeacion
{
    public partial class FrmPlaneacionEstado : FrmBaseExt
    {
        public FrmPlaneacionEstado()
        {
            InitializeComponent();
        }

        private void FrmPlaneacionEstado_Load(object sender, EventArgs e)
        {
            btnEjecutar.Visible = false;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }
    }
}
