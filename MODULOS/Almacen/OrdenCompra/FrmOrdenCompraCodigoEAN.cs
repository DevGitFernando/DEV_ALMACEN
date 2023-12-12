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


namespace Almacen.OrdenCompra
{
    public partial class FrmOrdenCompraCodigoEAN : Almacen.OrdenCompra.FrmOrdenCompraCodigoEAN_Base
    {
        public FrmOrdenCompraCodigoEAN() : base(ProcesosOrdenDeCompra.Registro)
        {
            //InitializeComponent();
        }
    }
}