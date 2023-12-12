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

namespace Farmacia.Vales_Servicio_A_Domicilio
{
    public partial class FrmDiferenciasCant_Claves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsGrid myGrid;
        DataSet dtsClavesDiferencias;

        public FrmDiferenciasCant_Claves(DataSet DtsClaves)
        {
            InitializeComponent();
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true;

            dtsClavesDiferencias = DtsClaves; 
        }

        private void FrmDiferenciasCant_Claves_Load(object sender, EventArgs e)
        {
            myGrid.LlenarGrid(dtsClavesDiferencias); 
        }
    }
}
