using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem.FuncionesGrid; 

namespace OficinaCentral
{
    public partial class Form1 : Form
    {
        clsGrid grid; 

        public Form1()
        {
            InitializeComponent();

            grid = new clsGrid(ref grdDatos, this); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //grdDatos.OpenExcel("", FarPoint.Excel.ExcelOpenFlags.NoFlagsSet); 
            btnNuevo_Click(null, null); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grid.Reset(); 
        }

        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {
            string sRuta = @"C:\Users\JesusDiaz\Desktop\TEST_EXCEL\Faltantes_Pedidos___INTERMED.xls";
            grdDatos.OpenExcel(sRuta, FarPoint.Excel.ExcelOpenFlags.NoFlagsSet); 
        }
        #endregion Botones
    }
}
