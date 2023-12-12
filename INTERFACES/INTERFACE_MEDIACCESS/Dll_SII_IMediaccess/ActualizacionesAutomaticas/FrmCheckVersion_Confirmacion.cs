using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using Dll_SII_IMediaccess;

namespace Dll_SII_IMediaccess
{
    internal partial class FrmCheckVersion_Confirmacion : FrmBaseExt
    {
        public bool Actualizar = false;

        public FrmCheckVersion_Confirmacion()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Actualizar = true;
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Actualizar = false;
            this.Hide();
        }

        private void FrmCheckVersion_Confirmacion_Load(object sender, EventArgs e)
        {
            btnCancelar.Focus(); 
        }
    }
}
