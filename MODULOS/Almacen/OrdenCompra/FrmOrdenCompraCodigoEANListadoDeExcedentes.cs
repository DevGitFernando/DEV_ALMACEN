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

using DllFarmaciaSoft;

namespace Almacen.OrdenCompra
{
    public partial class FrmOrdenCompraCodigoEANListadoDeExcedentes : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        public clsGrid myGrid;
        public bool bExito = false;

        public enum Cols : int { IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Porcentaje = 4, Observaciones = 5}


        public FrmOrdenCompraCodigoEANListadoDeExcedentes()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.Limpiar(false);
            myGrid.Rows = 0;
        }

        private void FrmOrdenCompraCodigoEANListadoDeExcedentes_Load(object sender, EventArgs e)
        {
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            myGrid.SetValue((int)Cols.Observaciones, "");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bcontinua = true;
            for (int i = 1; i <= myGrid.Rows && bcontinua; i++)
            {
                if (myGrid.GetValue(i, (int)Cols.Observaciones).Trim() == "")
                {
                    bcontinua = false;
                    General.msjAviso("Debe capturar observaciones para todos los productos, verifique.");
                }
            }

            if (bcontinua)
            {
                bExito = true;
                this.Hide();
            }
        }

        public void Shows()
        {
            this.ShowDialog();
        }
    }
}
