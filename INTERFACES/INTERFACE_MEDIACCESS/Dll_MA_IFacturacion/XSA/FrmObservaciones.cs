#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Threading;
using System.ServiceModel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_MA_IFacturacion.Configuracion;
#endregion USING

namespace Dll_MA_IFacturacion.XSA
{
    public partial class FrmObservaciones : FrmBaseExt
    {
        public string sReferencia = ""; 
        public string sObservaciones_01 = "";
        public string sObservaciones_02 = "";
        public string sObservaciones_03 = ""; 
        public int iTipoDocumento = 0;
        public int iTipoInsumo = 0; 
        public string sRubroFinanciamiento = "";
        public string sFuenteFinanciamiento = ""; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas; 

        Size tamForm;
        Size tamForm_PAC; 

        public FrmObservaciones()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Ayudas = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);  

            tamForm = new Size(585, 440);
            tamForm_PAC = new Size(585, 450);

            this.Size = tamForm;
            FrameIdentificadorDeDocumento.Top += 20;
            FrameIdentificadorDeDocumento.Visible = false;
            this.Size = tamForm_PAC; 

            //////if (DtIFacturacion.PAC_Informacion.PAC == PACs_Timbrado.Tralix)
            //////{
            //////    this.Size = tamForm; 
            //////    FrameIdentificadorDeDocumento.Top += 20;
            //////    FrameIdentificadorDeDocumento.Visible = false; 
            //////}
            //////else 
            //////{
            //////    this.Size = tamForm_PAC; 
            //////}

            
        }

        #region Form 
        private void FrmObservaciones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }
        #endregion Form

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtObservaciones_01.Text = sObservaciones_01;
            txtObservaciones_02.Text = sObservaciones_02;
            txtObservaciones_03.Text = sObservaciones_03;
            txtReferencia.Text = sReferencia;

            rdoVenta.Checked = iTipoDocumento == 1 ? true : false;
            rdoAdministracion.Checked = iTipoDocumento == 2 ? true : false;
            rdoMaterialDeCuracion.Checked = iTipoInsumo == 1 ? true : false;
            rdoMedicamento.Checked = iTipoInsumo == 2 ? true : false;

            txtRubro.Text = sRubroFinanciamiento;
            txtConcepto.Text = sFuenteFinanciamiento;

            if (sRubroFinanciamiento != "")
            {
                txtRubro_Validating(null, null);
                if (sFuenteFinanciamiento != "")
                {
                    txtConcepto_Validating(null, null);
                }
            }
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            sObservaciones_01 = txtObservaciones_01.Text;
            sObservaciones_02 = txtObservaciones_02.Text;
            sObservaciones_03 = txtObservaciones_03.Text;
            sReferencia = txtReferencia.Text;

            iTipoDocumento = 0;
            if (rdoVenta.Checked)
            {
                iTipoDocumento = 1;
            }
            if (rdoAdministracion.Checked)
            {
                iTipoDocumento = 2;
            }

            iTipoInsumo = 0;
            if (rdoMaterialDeCuracion.Checked)
            {
                iTipoInsumo = 1;
            }
            if (rdoMaterialDeCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            sRubroFinanciamiento = txtRubro.Text;
            sFuenteFinanciamiento = txtConcepto.Text; 

            this.Hide(); 
        }
        #endregion Botones 

        #region Fuentes de Financiamiento 
        private void txtRubro_TextChanged(object sender, EventArgs e)
        {
            lblRubro.Text = "";
            txtConcepto.Text = "";
            lblConcepto.Text = ""; 
        }

        private void txtRubro_Validating(object sender, CancelEventArgs e)
        {
            if (txtRubro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Encabezado(txtRubro.Text.Trim(), "txtRubro_Validating"); 
                if (!leer.Leer())
                {
                    txtRubro.Text = "";
                    lblRubro.Text = "";
                    txtRubro.Focus();   
                }
                else
                {
                    CargarDatosRubro();
                }
            }
        }

        private void txtRubro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Encabezado("txtRubro_KeyDown"); 
                if (leer.Leer())
                {
                    CargarDatosRubro();
                }
            }
        }

        private void CargarDatosRubro()
        {
            txtRubro.Text = leer.Campo("IdFuenteFinanciamiento");
            lblRubro.Text = leer.Campo("Estado");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rubro seleccionado se encuentra cancelado. Verifique");
                txtRubro.Text = "";
                lblRubro.Text = "";
            }
        }

        private void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            lblConcepto.Text = ""; 
        }

        private void txtConcepto_Validating(object sender, CancelEventArgs e)
        {
            if (txtConcepto.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FuentesDeFinanciamiento_Detalle(txtRubro.Text.Trim(), txtConcepto.Text.Trim(), "txtRubro_Validating"); 
                if (!leer.Leer())
                {
                    txtConcepto.Text = "";
                    lblConcepto.Text = "";
                    txtConcepto.Focus();
                }
                else
                {
                    CargarDatosConcepto();
                }
            }
        }

        private void txtConcepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FuentesDeFinanciamiento_Detalle("txtPrograma_KeyDown", txtRubro.Text.Trim());
                if (leer.Leer())
                {
                    CargarDatosConcepto();
                }
            }       
        }

        private void CargarDatosConcepto()
        {
            txtConcepto.Text = leer.Campo("IdFinanciamiento");
            lblConcepto.Text = leer.Campo("Financiamiento");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Financiamiento seleccionado se encuentra cancelado. Verifique");
                txtConcepto.Text = "";
                lblConcepto.Text = "";
            }
        }
        #endregion Fuentes de Financiamiento
    }
}
