using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.CodificacionDM
{
    public partial class FrmLotesSubFarmacias : FrmBaseExt
    {
        enum cols { Idproducto = 1, CodigoEAN = 2, IdSubFarmacia = 3, SubFarmacia = 4, ClaveLote = 5 }

        public string sIdproducto = "", sCodigoEAN = "", sIdSubFarmacia = "", sSubFarmacia = "", sClaveLote = "";
        clsListView lst;

        public FrmLotesSubFarmacias()
        {
            InitializeComponent();
            lst = new clsListView(lstLotes);
        }

        private void FrmLotesSubFarmacias_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
        }

        public void show(clsLeer Leer)
        {
            lst.CargarDatos(Leer.DataSetClase);
            this.ShowDialog();
        }

        private void lstLotes_DoubleClick(object sender, EventArgs e)
        {
            if (lst.GetValue((int)cols.CodigoEAN) != "")
            {
                sIdproducto = lst.GetValue((int)cols.Idproducto);
                sCodigoEAN = lst.GetValue((int)cols.CodigoEAN);
                sIdSubFarmacia = lst.GetValue((int)cols.IdSubFarmacia);
                this.Hide();
            }
        }
    }
}
