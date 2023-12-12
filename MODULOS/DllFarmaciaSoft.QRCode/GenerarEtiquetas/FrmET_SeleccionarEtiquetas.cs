using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode.Codec;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.QRCode;

using SC_SolutionsSystem.QRCode;
using SC_SolutionsSystem.SistemaOperativo;


namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    public partial class FrmET_SeleccionarEtiquetas : FrmBaseExt
    {
        public bool GenerarEtiquetas = false; 
        public int EtiquetadoManual = 0;
        public int FolioInicial = 0;
        public int FolioFinal = 0;

        public FrmET_SeleccionarEtiquetas()
        {
            InitializeComponent();

            rdo_01_Default.Checked = true;

            nm_01_Desde.Value = 1;
            nm_02_Hasta.Value = 1;
        }

        private void btnAceptar_Click( object sender, EventArgs e )
        {

            GenerarEtiquetas = true;

            if(rdo_01_Default.Checked)
            {
                EtiquetadoManual = 0;
                FolioInicial = 0;
                FolioFinal = 0;
            }

            if(rdo_02_Personalizado.Checked)
            {
                EtiquetadoManual = 1;
                FolioInicial = (int)nm_01_Desde.Value;
                FolioFinal = (int)nm_02_Hasta.Value;
            }

            this.Hide(); 
        }

        private void btnCancelar_Click( object sender, EventArgs e )
        {
            GenerarEtiquetas = false;
            this.Hide();
        }

        private void rdo_01_Default_CheckedChanged( object sender, EventArgs e )
        {
            nm_01_Desde.Enabled = false;
            nm_02_Hasta.Enabled = false; 
        }

        private void rdo_02_Personalizado_CheckedChanged( object sender, EventArgs e )
        {
            nm_01_Desde.Enabled = true;
            nm_02_Hasta.Enabled = true;
        }
    }
}
