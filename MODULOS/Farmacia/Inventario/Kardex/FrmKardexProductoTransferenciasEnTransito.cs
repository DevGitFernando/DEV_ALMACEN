using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Farmacia.Inventario
{
    public partial class FrmKardexProductoTransferenciasEnTransito : FrmBaseExt
    {
        clsListView list;

        public FrmKardexProductoTransferenciasEnTransito()
        {
            InitializeComponent();
        }

        public FrmKardexProductoTransferenciasEnTransito(DataSet dtsEnTransito)
        {
            InitializeComponent();
            list = new clsListView(listMovimientos);

            list.Limpiar();
            list.CargarDatos(dtsEnTransito, true, true);
            list.AnchoColumna(1, 90); 
            list.AnchoColumna(2, 80); 
            list.AnchoColumna(3, 370); 
            list.AnchoColumna(4, 100);

            lblTransferencias.Text = list.Registros.ToString("###,###,###,##0");
            //double b = list.TotalizarColumnaDouble(4);
            lblTotalPiezas.Text = list.TotalizarColumnaDouble(4).ToString("###,###,###,##0");
        }
    }
}
