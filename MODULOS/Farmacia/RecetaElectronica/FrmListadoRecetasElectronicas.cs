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

namespace Farmacia.ECE
{
    internal partial class FrmListadoRecetasElectronicas : FrmBaseExt
    {

        clsListView lst;
        DataSet dtsInformacionDeRecetas;
        string sValorSeleccionado = ""; 

        public FrmListadoRecetasElectronicas(DataSet InformacionDeRecetas)
        {
            InitializeComponent();

            dtsInformacionDeRecetas = InformacionDeRecetas; 

            lst = new clsListView(listviewRecetas);
            //lst.PermitirAjusteDeColumnas = false; 
        }

        private void FrmListadoRecetasElectronicas_Load(object sender, EventArgs e)
        {
            lst.CargarDatos(dtsInformacionDeRecetas, true, false); 
        }

        public string SeleccionarReceta()
        {
            sValorSeleccionado = "";

            this.ShowDialog(); 
            
            return sValorSeleccionado;
        }

        private void listviewRecetas_DoubleClick(object sender, EventArgs e)
        {
            sValorSeleccionado = lst.LeerItem().Campo("Secuenciador");

            if (sValorSeleccionado != "")
            {
                this.Hide(); 
            }
        }
    }
}
