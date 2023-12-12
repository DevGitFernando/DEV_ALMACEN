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
        string sFormato = "##,###,###,###,##0";
        clsRecetaElectronica Receta;

        public FrmListadoRecetasElectronicas(DataSet InformacionDeRecetas)
        {
            InitializeComponent();

            dtsInformacionDeRecetas = InformacionDeRecetas; 

            lst = new clsListView(listviewRecetas);
            //lst.PermitirAjusteDeColumnas = false;
            Receta = new clsRecetaElectronica();
        }

        private void ConfigurarInterface()
        {
            btnImprimir.Enabled = false;
            btnRecetasElectronicas.Enabled = false;
            btnEstadisticas.Enabled = false;

            if(RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.SESEQ)
            {
                btnEstadisticas.Enabled = true; 
            }

            if(RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.INTERMED)
            {
                btnRecetasElectronicas.Enabled = true;
                btnRecetasElectronicas_Especifico.Enabled = true;
                btnRecetasElectronicas_EnviarRespuesta.Enabled = true; 
            }

            if (RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.SIGHO)
            {
                btnRecetasElectronicas_Especifico.Enabled = true;
            }

            if (RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.AMPM)
            {
                btnImprimir.Enabled = true;
            }


            btnRecetasElectronicas.Visible = btnRecetasElectronicas.Enabled;
            btnRecetasElectronicas_Especifico.Visible = btnRecetasElectronicas_Especifico.Enabled;
            btnRecetasElectronicas_EnviarRespuesta.Visible = btnRecetasElectronicas_EnviarRespuesta.Enabled;
            btnEstadisticas.Visible = btnEstadisticas.Enabled;

            btnImprimir.Visible = btnImprimir.Enabled;
        }

        #region Botones 
        private void CargarRecetas()
        {
            RecetaElectronica.Receta.FolioRegistro = "";
            lblRecetas.Text = "0";
            
            lst.LimpiarItems();
            lst.CargarDatos(dtsInformacionDeRecetas, true, false);
            lblRecetas.Text = lst.Registros.ToString(sFormato);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtReferencia.Text = ""; 
            RecetaElectronica.Receta.FolioRegistro = txtReferencia.Text.Trim();
            dtsInformacionDeRecetas = RecetaElectronica.Receta.ActualizarListadoDeRecetas();

            CargarRecetas();
        }

        private void btnRecetasElectronicas_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            ExpedienteElectronico_Interface tipoInterface = RecetaElectronica.Receta.Interface;

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.INTERMED:
                        bRegresa = RecetaElectronica.Receta.DescargarRecetas();
                        break;

                    default:
                        General.msjAviso("La interface de receta electrónica no cuenta con esta función habilitada.");
                        break;
                }
            }

            //// Forzar la actualizacion del listado 
            //if(bRegresa)
            {
                dtsInformacionDeRecetas = RecetaElectronica.Receta.ActualizarListadoDeRecetas();
                CargarRecetas(); 
            }
        }

        private void btnRecetasElectronicas_Especifico_Click( object sender, EventArgs e )
        {
            bool bRegresa = false; 
            ExpedienteElectronico_Interface tipoInterface = RecetaElectronica.Receta.Interface;

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.INTERMED:
                    case ExpedienteElectronico_Interface.SIGHO:
                        bRegresa = RecetaElectronica.Receta.DescargarRecetaEspecifica();
                        sValorSeleccionado = RecetaElectronica.Receta.FolioRegistro;
                        break;

                    default:
                        General.msjAviso("La interface de receta electrónica no cuenta con esta función habilitada.");
                        break;
                }
            }

            if(bRegresa)
            {
                dtsInformacionDeRecetas = RecetaElectronica.Receta.ActualizarListadoDeRecetas();
                CargarRecetas();
            }
        }

        private void btnRecetasElectronicas_EnviarRespuesta_Click( object sender, EventArgs e )
        {
            bool bRegresa = false;
            ExpedienteElectronico_Interface tipoInterface = RecetaElectronica.Receta.Interface;

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.INTERMED:
                        bRegresa = RecetaElectronica.Receta.EnviarRecetasAtendidas();
                        break;

                    default:
                        General.msjAviso("La interface de receta electrónica no cuenta con esta función habilitada.");
                        break;
                }
            }
        }

        private void btnEstadisticas_Click( object sender, EventArgs e )
        {
            bool bRegresa = false;
            ExpedienteElectronico_Interface tipoInterface = RecetaElectronica.Receta.Interface;

            if(tipoInterface == ExpedienteElectronico_Interface.Ninguno)
            {
                General.msjError("No se ha configurado el tipo de Interface de Expediente Electrónico.");
            }
            else
            {
                switch(tipoInterface)
                {
                    case ExpedienteElectronico_Interface.SESEQ:
                        bRegresa = RecetaElectronica.Receta.VisualizarEstadisticas();
                        break;

                    default:
                        General.msjAviso("La interface de receta electrónica no cuenta con esta función habilitada.");
                        break;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FrmImpresionDeReceta f = new FrmImpresionDeReceta();
            f.ShowDialog();
        }
        #endregion Botones 

        private void FrmListadoRecetasElectronicas_Load(object sender, EventArgs e)
        {
            ConfigurarInterface();

            CargarRecetas();
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

        private void btnBusqueda_Click( object sender, EventArgs e )
        {
            //if(bRegresa)
            {
                RecetaElectronica.Receta.FolioRegistro = txtReferencia.Text.Trim();
                dtsInformacionDeRecetas = RecetaElectronica.Receta.ActualizarListadoDeRecetas();

                CargarRecetas();
            }
        }
    }
}
