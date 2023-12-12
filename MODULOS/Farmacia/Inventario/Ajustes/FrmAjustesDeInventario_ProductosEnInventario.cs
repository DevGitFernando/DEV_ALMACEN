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
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.Reportes;

namespace Farmacia.Inventario
{
    public partial class FrmAjustesDeInventario_ProductosEnInventario : FrmBaseExt
    {
        DataSet dtsDatos = new DataSet();
        clsListView lst; 

        public FrmAjustesDeInventario_ProductosEnInventario(DataSet Productos)
        {
            InitializeComponent();
            dtsDatos = Productos; 

            lst = new clsListView(lstvProductos); 
        }

        private void FrmAjustesDeInventario_ProductosEnInventario_Load(object sender, EventArgs e)
        {
            lst.CargarDatos(dtsDatos, false, false); 
        }

        private void FrmAjustesDeInventario_ProductosEnInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                this.Hide(); 
            }
        }
    }
}
