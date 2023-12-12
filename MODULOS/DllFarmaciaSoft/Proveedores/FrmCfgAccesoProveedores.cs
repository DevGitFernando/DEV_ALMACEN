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

namespace DllFarmaciaSoft.Proveedores
{
    public partial class FrmCfgAccesoProveedores : FrmBaseExt
    {

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();

        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsAyudas Ayuda;

        // string sIdProveedor = "";

        #region Constructores 
        public FrmCfgAccesoProveedores()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }
        #endregion Constructores 

        #region Funciones y Procedimientos Publicos 
        public void ShowLogin(string IdProveedor)
        {
            txtIdProveedor.Text = IdProveedor; 
            txtIdProveedor_Validating(null, null);
            this.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Botones
        private void Limpiar()
        {
            Fg.IniciaControles();

            txtPassword.PasswordChar = '*';
            txtPasswordCon.PasswordChar = '*'; 

            txtLogin.Enabled = false; 
            txtIdProveedor.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
                GuardarInformacion("A");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardarInformacion("C");
        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            FrmParametrosProveedores f = new FrmParametrosProveedores(txtIdProveedor.Text);
            f.ShowDialog();

            try
            {
                f.Close();
                f = null; 
            }
            catch { }
        }
        #endregion Botones 


        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado la Clave de Proveedor, verifique.");
                txtIdProveedor.Focus();
            }

            if (bRegresa && (txtPassword.Text.Trim() == "" || txtPasswordCon.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Password incorrecto, verifique por favor.");
                txtPassword.Focus();
            }
            else
            {
                if (bRegresa && (txtPassword.Text.Trim().ToUpper() != txtPasswordCon.Text.Trim().ToUpper()))
                {
                    bRegresa = false;
                    General.msjUser("Los passwords no son iguales, verifique por favor.");
                    txtPassword.Focus();
                }
            }

            return bRegresa;
        }

        private void GuardarInformacion(string Tipo)
        {
            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                // txtLogin.Text = txtIdProveedor.Text; 
                string sCadena = txtIdProveedor.Text.ToUpper() + txtLogin.Text.ToUpper() + txtPassword.Text.ToUpper();
                string sPass = crypto.PasswordEncriptar(sCadena);

                string sSql = string.Format("Exec spp_Net_Proveedores_Mtto '{0}', '{1}', '{2}', '{3}' ", txtIdProveedor.Text, txtLogin.Text, sPass, Tipo);

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

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                query.MostrarMsjSiLeerVacio = true;
                leer.DataSetClase = query.Proveedores(txtIdProveedor.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    txtIdProveedor.Enabled = false;
                    CargarDatosPersonal();
                }
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.NetProveedores("txtIdPersonal_KeyDown");
                if (leer.Leer())
                    CargarDatosPersonal();
            }
        }

        private void CargarDatosPersonal()
        {
            lblCancelado.Visible = false;
            string sNombre = leer.Campo("Nombre");
            if (leer.Campo("Status").ToUpper() == "C")
            {
                General.msjUser("El Proveedor " + sNombre + " actualmente se encuentra cancelado.");
            }
            else
            {
                txtIdProveedor.Text = leer.Campo("IdProveedor");
                // txtLogin.Enabled = false;
                // txtLogin.Text = txtIdProveedor.Text; 
                lblNombrePersonal.Text = sNombre;
                BuscaLogin();
            }
        }

        private void BuscaLogin()
        {
            string sSql = string.Format("Select * From Net_Proveedores (NoLock) where IdProveedor = '{0}' ", txtIdProveedor.Text);

            if (!leer.Exec(sSql))
            {
                General.Error.GrabarError(leer.Error, General.DatosConexion, "", "", "", "", leer.QueryEjecutado);
                General.msjError("Ocurrió un error al obtener los datos del Login.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtIdProveedor.Enabled = false;
                    txtLogin.Enabled = false;
                    txtLogin.Text = leer.Campo("LoginProv");

                    try
                    {
                        txtPassword.Text = crypto.PasswordDesencriptar(leer.Campo("Password")).Substring(txtIdProveedor.Text.Length + txtLogin.Text.Length);
                    }
                    catch
                    {
                        txtPassword.Text = "";
                    }

                    txtPasswordCon.Text = txtPassword.Text;

                    if (leer.Campo("Status").ToUpper() == "C")
                    {
                        lblCancelado.Visible = true;
                        General.msjUser("El login " + txtLogin.Text.ToUpper() + " actualmente se encuentra cancelado.");
                    }
                }
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
