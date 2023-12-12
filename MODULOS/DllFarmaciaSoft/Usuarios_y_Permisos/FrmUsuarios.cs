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

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmUsuarios : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();

        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsAyudas Ayuda;

        public string IdEstado = "";
        public string IdFarmacia = "";
        public string Estado = "";
        public string Farmacia = "";
        public string IdPersonal = "";

        public FrmUsuarios()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError("Configuracion", Application.ProductVersion, this.Name);

        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            if (IdPersonal != "")
            {
                txtIdPersonal.Enabled = false;
                txtIdPersonal.Text = IdPersonal;
                txtIdPersonal_Validating(null, null);
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;
            Fg.IniciaControles(this, true);

            txtPassword.PasswordChar = '*';
            txtPasswordCon.PasswordChar = '*'; 
            lblEstado.Text = Estado;
            lblFarmacia.Text = Farmacia;

            txtIdPersonal.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ( validarDatos() )
                GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(2);
        }

        private void btnGenerarPassword_Click(object sender, EventArgs e)
        {
            string sCadena = ""; 
            string sFechaExec = "";
            DateTime dtPass = DateTime.Now;

            sFechaExec = General.FechaYMD(dtPass, "");
            sFechaExec += General.Hora(dtPass, ""); 
            sCadena = Fg.Mid(clsMD5.GenerarMD5(sFechaExec), 1, 8);

            txtPassword.Text = sCadena;
            txtPasswordCon.Text = sCadena; 
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            bRegresa = validarLogin();

            if (bRegresa && txtIdPersonal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado la Clave de personal, verifique.");
                txtIdPersonal.Focus();
            }

            if(bRegresa && !DtGeneral.EsEquipoDeDesarrollo)
            {
                if(bRegresa && (txtPassword.Text.Trim() == "" || txtPasswordCon.Text.Trim() == ""))
                {
                    bRegresa = false;
                    General.msjUser("Password incorrecto, verifique por favor.");
                    txtPassword.Focus();
                }
            }
            else
            {
                if ( bRegresa && (txtPassword.Text.Trim().ToUpper() != txtPasswordCon.Text.Trim().ToUpper()) )
                {
                    bRegresa = false;
                    General.msjUser("Los passwords no son iguales, verifique por favor.");
                    txtPassword.Focus();
                }
            }

            return bRegresa;
        }

        private bool validarLogin()
        {
            bool bRegresa = true;
            string sLoginx = txtLogin.Text.Trim().ToUpper();

            string sSql = string.Format(" Select * From Net_Usuarios (NoLock) Where IdEstado = '{0}' and IdSucursal = '{1}' and LoginUser = '{2}' ", IdEstado, IdFarmacia, txtLogin.Text.Trim() );

            if (sLoginx == "SA".ToUpper() || sLoginx == "ADMIN" || sLoginx == "ADMINISTRADOR".ToUpper())
            {
                bRegresa = false;
                General.msjUser(" El login " + txtLogin.Text.ToUpper() + " es inválido, asigne un Login diferente.");
                txtLogin.Focus();
            }

            if (bRegresa)
            {
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    General.msjError("Ocurrió un error al validar el Login de usuario.");
                    Error.GrabarError(leer, "validarLogin()");
                }
                else
                {
                    if (leer.Leer())
                    {
                        if (leer.Campo("IdPersonal") != txtIdPersonal.Text)
                        {
                            bRegresa = false;
                            General.msjUser(" El login  [ " + txtLogin.Text.ToUpper() + " ]  ya se encuentra asignado a otro personal,\n asigne un Login diferente. ");
                            //txtLogin.Text = "";
                            txtLogin.Focus();
                        }
                    }
                }
            }

            return bRegresa;
        }

        private void GuardarInformacion(int Tipo)
        {
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sCadena = IdEstado + IdFarmacia + txtIdPersonal.Text + txtPassword.Text.ToUpper();
                string sPass = crypto.PasswordEncriptar(sCadena);
                
                string sSql = string.Format("Exec spp_Net_Usuarios_Mtto '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ", IdEstado, IdFarmacia, txtIdPersonal.Text, txtLogin.Text, sPass, Tipo.ToString());

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al grabar el usuario.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    General.msjUser("Información guardada satisfactoriamente.");
                    btnNuevo_Click(null, null);
                }

                cnn.Cerrar();
            }
            else
                General.msjAviso("No se pudo conectar con el servidor, intente de nuevo.");
        }

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "" )
            {
                query.MostrarMsjSiLeerVacio = true;
                leer.DataSetClase = query.Personal(IdEstado, IdFarmacia, txtIdPersonal.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    txtIdPersonal.Enabled = false;
                    CargarDatosPersonal();
                }
            }
        }

        private void CargarDatosPersonal()
        {
            lblCancelado.Visible = false;
            string sNombre = leer.Campo("Nombre") + " " + leer.Campo("ApPaterno") + " " + leer.Campo("ApMaterno");

            txtIdPersonal.Text = leer.Campo("IdPersonal");
            lblNombrePersonal.Text = sNombre;
            BuscaLogin();

            if (leer.Campo("Status").ToUpper() == "C")
            {
                //btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                lblCancelado.Visible = true;
                General.msjUser("El personal " + sNombre + " actualmente se encuentra cancelado.");
            }
            ////else
            ////{
            ////    txtIdPersonal.Text = leer.Campo("IdPersonal");
            ////    lblNombrePersonal.Text = sNombre;
            ////    BuscaLogin();
            ////}
        }

        private void BuscaLogin() 
        {
            int iLargo = IdEstado.Length + IdFarmacia.Length + txtIdPersonal.Text.Length;
 
            string sSql = string.Format("Select * From Net_Usuarios (NoLock) " + 
                " Where IdEstado = '{0}' and IdSucursal = '{1}' and IdPersonal = '{2}' ", 
                IdEstado, IdFarmacia, txtIdPersonal.Text);

            if (!leer.Exec(sSql))
            {
                General.Error.GrabarError(leer.Error, General.DatosConexion, "", "", "", "", leer.QueryEjecutado);
                General.msjError("Ocurrió un error al obtener los datos del Login.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtIdPersonal.Enabled = false;
                    txtLogin.Enabled = false;
                    txtLogin.Text = leer.Campo("LoginUser");

                    try
                    {
                        // txtPassword.Text = crypto.PasswordDesencriptar(leer.Campo("Password")).Substring(10); 
                        txtPassword.Text = crypto.PasswordDesencriptar(leer.Campo("Password")).Substring(iLargo);
                    }
                    catch 
                    {
                        txtPassword.Text = "";  
                    }

                    txtPasswordCon.Text = txtPassword.Text;

                    if (leer.Campo("Status").ToUpper() == "C" )
                    {
                        lblCancelado.Visible = true;
                        General.msjUser("El login " + txtLogin.Text.ToUpper() + " actualmente se encuentra cancelado." );
                    }
                }
            } 
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Personal("txtIdPersonal_KeyDown", IdEstado, IdFarmacia);
                if (leer.Leer())
                    CargarDatosPersonal();
            }
        }

        private void encriptarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            txtPasswordCon.PasswordChar = '*'; 
        }

        private void desencriptarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '\0';
            txtPasswordCon.PasswordChar = '\0'; 
        }
    }
}
