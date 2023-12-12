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

namespace Dll_IGPI.Inventario
{
    public partial class FrmCargarMatch4 : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsLeer leer;
        // clsGrid grid; 

        public FrmCargarMatch4()
        {
            InitializeComponent();
        }
    }
}
