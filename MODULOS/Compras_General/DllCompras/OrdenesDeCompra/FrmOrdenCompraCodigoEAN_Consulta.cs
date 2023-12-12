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
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmOrdenCompraCodigoEAN_Consulta : DllCompras.OrdenesDeCompra.FrmOrdenCompraCodigoEAN_Base
    {
        public FrmOrdenCompraCodigoEAN_Consulta():base(ProcesosOrdenDeCompra.Consulta) 
        {
            //InitializeComponent();
        }
    }
}
