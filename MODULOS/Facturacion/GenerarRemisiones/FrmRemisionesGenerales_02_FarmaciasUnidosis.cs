using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;

using Facturacion; 
using Dll_IFacturacion;

namespace Facturacion.GenerarRemisiones
{
    public partial class FrmRemisionesGenerales_02_FarmaciasUnidosis : FrmRemisionesGenerales
    {
        public FrmRemisionesGenerales_02_FarmaciasUnidosis():base(eTipoDeUnidades.FarmaciasUnidosis) 
        {
            InitializeComponent();
        }
    }
}
