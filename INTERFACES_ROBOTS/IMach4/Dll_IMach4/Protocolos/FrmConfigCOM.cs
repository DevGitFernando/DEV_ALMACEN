using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public partial class FrmConfigCOM : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        public FrmConfigCOM()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
