using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Dll_IFacturacion.Configuracion
{
    internal partial class FrmSeries : FrmBaseExt 
    {
        public bool Guardado = false;
        public string sAño = "", sAprobacion = "", sSerie = "", sIdTipoDocto = "", sNombreTipoDocto = "", sNombreDocumento = "";
        public string sFolioInicio = "", sFolioFinal = "", sUltimoFolio = "";
        private clsLeer listaTiposDeDocumentos = new clsLeer(); 

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

        public void CargarPantalla(string Año, string Aprobacion, string Serie, string IdTipoDocto, string NombreDocumento, string FolioInicial, string FolioFinal, string UltimoFolioUtilizado)
        {
            txtAño.Text = Año;
            txtAprobacion.Text = Aprobacion;
            txtSerie.Text = Serie;
            cboTipoDoctos.Data = IdTipoDocto; 
            txtNombreDocumento.Text = NombreDocumento; 
            txtFolioInicial.Text = FolioInicial;
            txtFolioFinal.Text = FolioFinal;
            txtFolioUtilizado.Text = UltimoFolioUtilizado;

            if ( UltimoFolioUtilizado != "0" )
            {
                btnNuevo.Enabled = false; 
                btnGuardar.Enabled = false; 
                txtAño.Enabled = false;
                txtAprobacion.Enabled = false;
                txtSerie.Enabled = false;
                cboTipoDoctos.Enabled = false; 
                txtNombreDocumento.Enabled = false; 
                txtFolioInicial.Enabled = false;
                txtFolioFinal.Enabled = false;
                txtFolioUtilizado.Enabled = false; 
            }

            this.ShowDialog(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            sAño = txtAño.Text;
            sAprobacion = txtAprobacion.Text;
            sSerie = txtSerie.Text;
            sIdTipoDocto = cboTipoDoctos.Data;
            sNombreTipoDocto = cboTipoDoctos.Text; 
            sNombreDocumento = txtNombreDocumento.Text; 
            sFolioInicio = txtFolioInicial.Text;
            sFolioFinal = txtFolioFinal.Text;
            sUltimoFolio = txtFolioUtilizado.Text;

            Guardado = true;

            this.Hide(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            txtAño.Focus(); 
        }
    }
}
