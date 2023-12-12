using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllRecetaElectronica.ECE
{
    internal partial class FrmListadoRecetasElectronicas : FrmBaseExt
    {

        clsListView lst;
        DataSet dtsInformacionDeRecetas;
        string sValorSeleccionado = "";
        clsRecetaElectronica Receta;

        public FrmListadoRecetasElectronicas(DataSet InformacionDeRecetas)
        {
            InitializeComponent();

            dtsInformacionDeRecetas = InformacionDeRecetas; 

            lst = new clsListView(listviewRecetas);
            //lst.PermitirAjusteDeColumnas = false;
            Receta = new clsRecetaElectronica();

            if (RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.SIGHO)
            {
                btnRecetasElectronicas.Enabled = true;
            }
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

        private void btnRecetasElectronicas_Click(object sender, EventArgs e)
        {
            ExpedienteElectronico_Interface tipoInterface = RecetaElectronica.Receta.Interface;


            if (tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch (tipoInterface)
                {

                    case ExpedienteElectronico_Interface.SIGHO:
                        RecetaElectronica.Receta.SeleccionarRecetasParaSurtido();
                        sValorSeleccionado = RecetaElectronica.Receta.FolioRegistro;
                        break;
                }
            }

            if (sValorSeleccionado != "")
            {
                this.Hide();
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmImpresionDeReceta f = new FrmImpresionDeReceta();
            f.ShowDialog();
        }
    }
}
