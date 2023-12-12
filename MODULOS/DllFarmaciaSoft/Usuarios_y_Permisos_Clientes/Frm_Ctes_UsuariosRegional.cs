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

namespace DllFarmaciaSoft.Usuarios_y_Permisos_Clientes
{
    public partial class Frm_Ctes_UsuariosRegional : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();
        DataSet dtsFarmacias;

        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsAyudas Ayuda;

        //public string IdEstado = "";
        //public string IdFarmacia = "";
        //public string Estado = "";
        //public string Farmacia = "";
        //public string IdPersonal = "";

        public Frm_Ctes_UsuariosRegional()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "Configuracion", this.Name, Application.ProductVersion);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError("Configuracion", Application.ProductVersion, this.Name);
            CargarEstados();
        }

        private void Frm_Ctes_UsuariosRegional_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            //if (IdPersonal != "")
            //{
            //    txtIdUsuario.Enabled = false;
            //    txtIdUsuario.Text = IdPersonal;
            //    txtIdPersonal_Validating(null, null);
            //}

        }

        #region Buscar Usuario
        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdUsuario.Text.Trim() == "")
            {
                txtIdUsuario.Text = "*";
                txtIdUsuario.Enabled = false;
                InicializaToolBar(true, true, false, false);
            }
            else
            {
                query.MostrarMsjSiLeerVacio = true;
                leer.DataSetClase = query.Regional_Usuarios(cboEstados.Data, "", txtIdUsuario.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    InicializaToolBar(true, true, true, false);
                    txtIdUsuario.Enabled = false;
                    CargarDatosPersonal();
                }
                else
                {
                    txtIdUsuario.Focus();
                }
            }
        }

        private void CargarDatosPersonal()
        {
            lblCancelado.Visible = false;
            
            cboEstados.Data = leer.Campo("IdEstado");
            txtIdUsuario.Text = leer.Campo("IdUsuario");
            txtNombre.Text = leer.Campo("Nombre");
            txtLogin.Text = leer.Campo("Login");

            txtPassword.Text = crypto.PasswordDesencriptar(leer.Campo("Password")).Substring(2 + txtLogin.Text.Length);
            txtPasswordCon.Text = txtPassword.Text;            

            if (leer.Campo("Status").ToUpper() == "C")
            {
                InicializaToolBar(true, true, false, false);
                lblCancelado.Visible = true;                
                Fg.BloqueaControles(this, false);
                //General.msjUser("El Usuario " + txtNombre.Text.Trim() + " actualmente se encuentra cancelado.");
            }
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Regional_Usuarios("txtIdPersonal_KeyDown", cboEstados.Data);
                if (leer.Leer())
                    CargarDatosPersonal();
            }
        }

        #endregion Buscar Usuario 

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;
            Fg.IniciaControles(this, true);
            InicializaToolBar(true, false, false, false);

            txtPassword.PasswordChar = '*';
            txtPasswordCon.PasswordChar = '*';
            cboEstados.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(2);
        }

        private void GuardarInformacion(int Tipo)
        {
            string sMensaje = "";
            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {
                cnn.IniciarTransaccion();

                string sCadena = cboEstados.Data + cboFarmacias.Data + txtLogin.Text + txtPassword.Text.ToUpper();
                string sPass = crypto.PasswordEncriptar(sCadena);

                string sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                    cboEstados.Data, cboFarmacias.Data, txtIdUsuario.Text, txtNombre.Text.Trim(), txtLogin.Text, sPass, Tipo.ToString());

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al guardar la información.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);
                        btnNuevo_Click(null, null);
                    }
                }

                cnn.Cerrar();
            } 
        }

        #endregion Botones        

        #region Funciones
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (!lblCancelado.Visible) //Si el lblCancelado esta visible, significa que se va a activar el Usuario.
            {
                bRegresa = validarLogin();
            }

            if (bRegresa && cboEstados.SelectedIndex == 0 )
            {
                bRegresa = false;
                General.msjUser("No se ha seleccionado el Estado, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado el Nombre, verifique.");
                txtNombre.Focus();
            }

            if (bRegresa && txtLogin.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado el Login, verifique.");
                txtLogin.Focus();
            }

            if (bRegresa && txtIdUsuario.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado la Clave de personal, verifique.");
                txtIdUsuario.Focus();
            }

            if (bRegresa && (txtPassword.Text.Trim() == "" || txtPasswordCon.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Password incorrecto, verifique por favor.");
                txtPassword.Focus();
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

            string sSql = string.Format(" Select * From Net_Regional_Usuarios (NoLock) Where IdEstado = '{0}' and Login = '{1}' ", 
                cboEstados.Data, txtLogin.Text.Trim());

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
                        if (leer.Campo("IdPersonal") != txtIdUsuario.Text)
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

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            cboFarmacias.SelectedIndex = 0;

            dtsFarmacias = query.Farmacias("CargarFarmacias()");
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            if (cboEstados.SelectedIndex != 0)
            {
                try
                {
                    cboFarmacias.Add(dtsFarmacias.Tables[0].Select(string.Format("IdEstado = '{0}'", cboEstados.Data)), true, "IdFarmacia", "NombreFarmacia");
                }
                catch { }
            }
            cboFarmacias.SelectedIndex = 0; 
        }

        private void InicializaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

    }
}
