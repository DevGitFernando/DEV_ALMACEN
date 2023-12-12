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

namespace DllFarmaciaSoft.QRCode
{
    public partial class FrmQR_Reader : FrmBaseExt
    {
        clsLeer leer = new clsLeer();
        clsListView lst; 

        public FrmQR_Reader(DataSet Datos)
        {
            InitializeComponent(); 
            this.MinimumSize = new Size(700, 400);

            lst = new clsListView(listvwDatos);
            leer.DataSetClase = Datos; 

            cboTablas.Clear();
            cboTablas.Add(); 
        }

        private void FrmQR_Reader_Load(object sender, EventArgs e)
        {
            CargarTablas(); 
        }

        private void CargarTablas()
        {
            string sTabla = "";
            cboTablas.Clear();
            cboTablas.Add(); 

            for (int i = 1; i <= leer.Tablas; i++)
            {
                sTabla = leer.Tabla(i).TableName;
                cboTablas.Add(sTabla, sTabla);
            }

            cboTablas.SelectedIndex = 0; 
            if (cboTablas.NumeroDeItems > 0) 
            { 
                cboTablas.SelectedIndex = 1; 
            }
        }

        private void cboTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsLeer leerTabla = new clsLeer();
            leerTabla.DataTableClase = leer.Tabla(cboTablas.Data);

            lst.Limpiar();
            lst.CargarDatos(leerTabla.DataSetClase, true, true); 
        }
    }
}
