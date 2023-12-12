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
    internal partial class FrmRecetasElectronicas_Claves : FrmBaseExt
    {

        clsListView lst;
        DataSet dtsInformacionDeRecetas;
        string sValorSeleccionado = "";

        public FrmRecetasElectronicas_Claves(DataSet InformacionDeRecetas)
        {
            InitializeComponent();

            dtsInformacionDeRecetas = InformacionDeRecetas; 

            lst = new clsListView(listviewRecetas);
            //lst.PermitirAjusteDeColumnas = false; 
        }

        ////private void FrmRecetasElectronicas_Claves_Load(object sender, EventArgs e)
        ////{
        ////    lst.CargarDatos(dtsInformacionDeRecetas, true, false); 
        ////}

        protected override void OnLoad(EventArgs e)
        {
            double posicion_y = 1.45;
            double posicion_x = 1.02; 
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - (int)(((double)this.Width) * posicion_x), screen.WorkingArea.Bottom - (int)(((double)this.Height) * posicion_y));
            base.OnLoad(e);

            //PlaceLowerRight();
            //base.OnLoad(e);

            lst.CargarDatos(dtsInformacionDeRecetas, true, false);
            lst.AnchoColumna(1, 100);
            lst.AnchoColumna(2, 120);
            lst.AnchoColumna(3, 120);
            lst.AnchoColumna(4, 400);
        }

        ////public void MostrarInformacion()
        ////{
        ////    this.ShowDialog();            
        ////}

        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                {
                    rightmost = screen;
                }
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }

        private void FrmRecetasElectronicas_Claves_FormClosing(object sender, FormClosingEventArgs e)
        {
            RecetaElectronica.Receta.MostrandoDetallesReceta = false; 
        }
    }
}
