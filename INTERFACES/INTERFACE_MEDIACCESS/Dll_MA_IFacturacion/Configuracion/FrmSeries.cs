using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Dll_MA_IFacturacion.Configuracion
{
    internal partial class FrmSeries : FrmBaseExt 
    {
        public bool Guardado = false;
        public string sSerie = "", sIdTipoDocto = "", sNombreTipoDocto = "", sNombreDocumento = "";
        public string sFolioInicio = "", sFolioFinal = "", sUltimoFolio = "", sStatus = "";
        private clsLeer listaTiposDeDocumentos = new clsLeer();

        int iFolioInicial = 0;
        int iFolioFinal = 0;
        int iUltimoFolio = 0; 

        public FrmSeries()
        {
            InitializeComponent();
        }

        private void FrmSeries_Load(object sender, EventArgs e)
        {
        }

        public clsLeer TiposDeDocumentos
        {
            set
            {
                listaTiposDeDocumentos = value;
                cboTipoDoctos.Clear();
                cboTipoDoctos.Add();
                cboTipoDoctos.Add(listaTiposDeDocumentos.DataSetClase, true, "IdTipoDocumento", "Documento");
                cboTipoDoctos.SelectedIndex = 0;
            }
        }

        public void CargarPantalla()
        {
            txtFolioUtilizado.Text = "0";
            this.ShowDialog();
        }

        public void CargarPantalla(string Serie, string TipoDocto, string FolioInicial, string FolioFinal, string UltimoFolioUtilizado, string Status)
        {
            txtSerie.Text = Serie;
            cboTipoDoctos.Data = TipoDocto;
            txtFolioInicial.Text = FolioInicial;
            txtFolioFinal.Text = FolioFinal;
            txtFolioUtilizado.Text = UltimoFolioUtilizado;
            chkStatus.Checked = Status.ToUpper() == "A" ? true : false;

            if (UltimoFolioUtilizado != "0")
            {
                btnNuevo.Enabled = false;
                //btnGuardar.Enabled = false; 
                txtSerie.Enabled = false;
                cboTipoDoctos.Enabled = false;
                txtFolioInicial.Enabled = false;
                txtFolioFinal.Enabled = false;
                txtFolioUtilizado.Enabled = false;
            }

            this.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            sSerie = txtSerie.Text;
            sIdTipoDocto = cboTipoDoctos.Data;
            sNombreTipoDocto = cboTipoDoctos.Text;
            sNombreTipoDocto = cboTipoDoctos.Text;
            sFolioInicio = txtFolioInicial.Text;
            sFolioFinal = txtFolioFinal.Text;
            sUltimoFolio = txtFolioUtilizado.Text;
            sStatus = chkStatus.Checked ? "A" : "C";

            Guardado = true;

            this.Hide(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();            
        }

        private void txtFolioInicial_TextChanged(object sender, EventArgs e)
        {
            Calcular_UltimoFolio();
        }

        private void txtFolioFinal_TextChanged(object sender, EventArgs e)
        {
            Calcular_UltimoFolio();
        }

        private void Calcular_UltimoFolio()
        {
            iFolioInicial = Convert.ToInt32("0" + txtFolioInicial.Text.Trim().Replace(",", ""));
            iFolioFinal = Convert.ToInt32("0" + txtFolioFinal.Text.Trim().Replace(",", ""));
            iUltimoFolio = iFolioInicial - 1;

            txtFolioUtilizado.Text = (iUltimoFolio > 0 ? iUltimoFolio : 0).ToString();
        }
    }
}
