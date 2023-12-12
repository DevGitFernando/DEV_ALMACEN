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
////REV  using DllFarmaciaSoft.Email; 


namespace Dll_MA_IFacturacion.Catalogos
{
    public partial class FrmEmail : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas_CFDI Ayuda = new clsAyudas_CFDI();
        clsConsultas_CFDI Consultas;

        static DataSet dtsProveedoresEMail; 

        public bool Exito = false;
        int iTipoProceso = 0;
        public string sIdTipoMail = "";
        public string sTipoMail = "";
        public string sEMails = "";
        public string sStatus = ""; 

        public FrmEmail()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            CargarProveedoresDeEmail(false); 
            Fg.IniciaControles(); 
        }

        #region Form 
        private void FrmEmail_Load(object sender, EventArgs e)
        {
            InicializaPantalla();
            CargaInicial(); 
        }

        private void CargaInicial()
        {
            if (iTipoProceso != 1)
            {
                sEMails = sEMails.ToLower(); 
                string[] sDatosMail = sEMails.Split('@'); 

                txtIdTipoCorreo.Text = sIdTipoMail;
                chkActivo.Checked = sStatus == "A" ? true : false;

                try
                {
                    txtCorreo.Text = sDatosMail[0];
                }
                catch { }

                try
                {
                    cboProveedoresEMail.Data = sDatosMail[1];
                }
                catch { }


                txtIdTipoCorreo_Validating(null, null);
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
            txtCorreo.Focus(); 
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
                sIdTipoMail = txtIdTipoCorreo.Text.Trim();
                sTipoMail = lblTipoCorreo.Text;
                sEMails = txtCorreo.Text.Trim().ToLower() + "@" + cboProveedoresEMail.Text;
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

            if (bRegresa && txtIdTipoCorreo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Tipo de Correo válido, verifique.");
                txtIdTipoCorreo.Focus(); 
            }

            if (bRegresa && txtCorreo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Correo válido, verifique.");
                txtCorreo.Focus();
            }

            if (bRegresa && cboProveedoresEMail.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Proveedor de Correo válido, verifique.");
                cboProveedoresEMail.Focus(); 

                //////bRegresa = validarEMail(); 
                //bRegresa = EmailValidator.EsCorreoValido(txtCorreo.Text); 
            }

            if (bRegresa)
            {
                if (!DtIFacturacion.EMail_Valido(txtCorreo.Text + "@" + cboProveedoresEMail.Text))
                {
                    bRegresa = false;
                    General.msjUser("El formato del Correo es incorrecto, verifique.");
                }
            }

            return bRegresa; 
        }

        public bool MostrarInterface(int TipoProceso, string IdTipoMail, string TipoMail, string EMail, string Status)
        {
            iTipoProceso = TipoProceso;
            sIdTipoMail = IdTipoMail;
            sTipoMail = TipoMail;
            sEMails = EMail;
            sStatus = Status; 

            this.ShowDialog(); 
            return Exito; 
        }

        #region Emails 
        private void CargarInfTipoCorreo()
        {
            txtIdTipoCorreo.Text = leer.Campo("IdTipoEMail");
            lblTipoCorreo.Text = leer.Campo("Descripcion");
        }

        private void txtIdTipoCorreo_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdTipoCorreo.Text != "")
            {
                leer.DataSetClase = Consultas.Correos(txtIdTipoCorreo.Text, "txtIdTipoCorreo_Validating");
                if (!leer.Leer())
                {
                    txtIdTipoCorreo.Focus();
                }
                else
                {
                    CargarInfTipoCorreo();
                }
            }
        }

        private void txtIdTipoCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Correos("txtIdTipoCorreo_KeyDown");
                if (leer.Leer())
                {
                    CargarInfTipoCorreo();
                }
            }
        }
        #endregion Emails 

        #region Proveedores de EMail 
        private void CargarProveedoresDeEmail(bool ForzarActualizacion)
        {
            if (ForzarActualizacion)
            {
                dtsProveedoresEMail = null;
            }

            if (dtsProveedoresEMail == null)
            {
                dtsProveedoresEMail = Consultas.ProveedoresEMail("", "CargarProveedoresDeEmail()"); 
            }

            cboProveedoresEMail.Clear();
            cboProveedoresEMail.Add();
            cboProveedoresEMail.Add(dtsProveedoresEMail, true, "Descripcion", "Descripcion"); 
            cboProveedoresEMail.SelectedIndex = 0;
        }
        #endregion Proveedores de EMail

        #region Botones auxiliares
        private void btnTiposEmail_Click(object sender, EventArgs e)
        {
            FrmTiposEmail f = new FrmTiposEmail();
            f.ShowDialog(); 
        }

        private void btnProveedorEMail_Click(object sender, EventArgs e)
        {
            FrmProveedoresEMail f = new FrmProveedoresEMail();
            f.ShowDialog();

            if (f.Guardo)
            {
                CargarProveedoresDeEmail(true);
            }
        }
        #endregion Botones auxiliares
    }
}
