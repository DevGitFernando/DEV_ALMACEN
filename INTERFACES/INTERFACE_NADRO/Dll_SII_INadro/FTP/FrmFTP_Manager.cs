using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FTP; 


using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro;
using Dll_SII_INadro.GenerarArchivos; 

namespace Dll_SII_INadro.FTP
{
    public partial class FrmFTP_Manager : FrmBaseExt 
    {
        public FrmFTP_Manager()
        {
            InitializeComponent();
        }
    }
}
