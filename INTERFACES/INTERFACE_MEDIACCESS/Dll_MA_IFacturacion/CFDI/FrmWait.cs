using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;


using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;

namespace Dll_MA_IFacturacion.CFDI
{
    public partial class FrmWait : FrmBaseExt 
    {
        public FrmWait()
        {
            InitializeComponent();
        }
    }
}
