using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.ExportarExcel;
using Almacen.wsAlmacen;

namespace Almacen.PedidosEspeciales
{
    public partial class FrmRegistroPedidosEspeciales : Almacen.PedidosEspeciales.FrmRegistroPedidosEspeciales_Base
    {
        public FrmRegistroPedidosEspeciales(): base(TipoDePedidoElectronico.Transferencias)
        {
        }
    }
}
