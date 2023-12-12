using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;


namespace Dll_MA_IFacturacion.Catalogos
{
    public partial class FrmTelefono : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas_CFDI Ayuda = new clsAyudas_CFDI();
        clsConsultas_CFDI Consultas;

        public bool Exito = false;
        int iTipoProceso = 0;
        public string sIdTipoTelefono = "";
        public string sTipoTelefono = "";
        public string sTelefono = "";
        public string sStatus = ""; 

        public FrmTelefono()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            Fg.IniciaControles(); 
        }

        #region Form 
        private void FrmTelefono_Load(object sender, EventArgs e)
        {
            InicializaPantalla();
            CargaInicial(); 
        }

        private void CargaInicial()
        {
            if (iTipoProceso != 1)
            {
                txtIdTipoTelefono.Text = sIdTipoTelefono;
                txtTelefono.Text = sTelefono;
                chkActivo.Checked = sStatus == "A" ? true : false;

                txtIdTipoTelefono_Validating(null, null);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                ////case Keys.F5:
                ////    if (btnNuevo.Enabled)
                ////    {
                ////        btnNuevo_Click(null, null);
                ////    }
                ////    break;

                ////case Keys.F6:
                ////    if (btnGuardar.Enabled)
                ////    {
                ////        btnGuardar_Click(null, null);
                ////    }
                ////    break;

                ////case Keys.F7:
                ////    if (btnCancelar.Enabled)
                ////    {
                ////        btnCancelar_Click(null, null);
                ////    }
                ////    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
        #endregion Form

        #region Botones 
        private void InicializaPantalla()
        {
            Fg.IniciaControles();
            txtTelefono.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                Exito = true;
                sIdTipoTelefono = txtIdTipoTelefono.Text.Trim();
                sTipoTelefono = lblTipoTelefono.Text;
                sTelefono = txtTelefono.Text.Trim();
                sStatus = chkActivo.Checked ? "ACTIVO" : "CANCELADO";
                this.Hide();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdTipoTelefono.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Tipo de Teléfono válido, verifique.");
                txtIdTipoTelefono.Focus();
            }

            if (bRegresa && txtTelefono.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Teléfono válido, verifique.");
                txtTelefono.Focus();
            }

            return bRegresa;
        }

        public bool MostrarInterface(int TipoProceso, string IdTipoTelefono, string TipoTelefono, string Telefono, string Status)
        {
            iTipoProceso = TipoProceso;
            sIdTipoTelefono = IdTipoTelefono;
            sTipoTelefono = TipoTelefono;
            sTelefono = Telefono;
            sStatus = Status; 

            this.ShowDialog(); 
            return Exito; 
        }

        #region Telefonos
        private void CargarInfTipoTelefono()
        {
            txtIdTipoTelefono.Text = leer.Campo("IdTipoTelefono");
            lblTipoTelefono.Text = leer.Campo("Descripcion");
        }

        private void txtIdTipoTelefono_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdTipoTelefono.Text != "")
            {
                leer.DataSetClase = Consultas.Telefonos(txtIdTipoTelefono.Text, "txtIdTipoCorreo_Validating");
                if (!leer.Leer())
                {
                    txtIdTipoTelefono.Focus();
                }
                else
                {
                    CargarInfTipoTelefono();
                }
            }
        }

        private void txtIdTipoTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Telefonos("txtIdTipoTelefono_KeyDown");
                if (leer.Leer())
                {
                    CargarInfTipoTelefono();
                }
            }
        }
        #endregion Telefonos  

        #region Botones auxiliares
        private void btnTiposTelefono_Click(object sender, EventArgs e)
        {
            FrmTiposTelefonos f = new FrmTiposTelefonos();
            f.ShowDialog();
        }
        #endregion Botones auxiliares 
    }
}
