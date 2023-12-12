using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Farmacia.Ventas
{
    public partial class FrmBeneficiarios_Alertas : FrmBaseExt
    {
        clsListView lst;
        DataSet dtsListaBeneficiarios;

        public FrmBeneficiarios_Alertas(DataSet ListaBeneficiarios)
        {
            InitializeComponent();

            lst = new clsListView(lstBeneficiarios);
            lst.PermitirAjusteDeColumnas = false;
            dtsListaBeneficiarios = ListaBeneficiarios; 
        }

        private void FrmBeneficiarios_Alertas_Shown(object sender, EventArgs e)
        {
            lst.CargarDatos(dtsListaBeneficiarios, true, false);
            lst.AnchoColumna(1, 480); 
        }
    }
}
