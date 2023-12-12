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

namespace DllProveedores.Usuarios_y_Permisos
{
    public partial class FrmCambiarPassword : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeerWeb leer;
        clsCriptografo crypto = new clsCriptografo();

        string IdEstado = ""; //DtGeneral.EstadoConectado;
        string IdFarmacia = ""; //DtGeneral.FarmaciaConectada;
        string IdPersonal = ""; //DtGeneral.IdPersonal;

        public FrmCambiarPassword()
        {
            InitializeComponent(); 
            leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            //txtIdPersonal.Enabled = false; 
            txtLogin.Enabled = false;
            txtLogin.Text = GnProveedores.Usuario;  // DtGeneral.LoginUsuario;

            //txtIdPersonal.Text = ""; //DtGeneral.IdPersonal;
            //lblNombrePersonal.Text = ""; //DtGeneral.NombrePersonal;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
                GuardarInformacion(1);
        }

        private bool validarDatos()
        {
            bool bRegresa = true;
            string sCadena = GnProveedores.IdProveedor + GnProveedores.Usuario + txtPasswordAnterior.Text.ToUpper(); 
            string sPass = crypto.PasswordEncriptar(sCadena); 

            if (bRegresa && sPass != GnProveedores.Password)  //DtGeneral.PasswordUsuario)
            {
                bRegresa = false;
                General.msjUser("Password anterior incorrecto, verifique.");
                txtPasswordAnterior.Focus();
            }

            if (bRegresa && (txtPassword.Text.Trim() == "" || txtPasswordCon.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Password incorrecto, verifique.");
                txtPassword.Focus();
            }
            else
            {
                if (bRegresa && (txtPassword.Text.Trim().ToUpper() != txtPasswordCon.Text.Trim().ToUpper()))
                {
                    bRegresa = false;
                    General.msjUser("Los passwords no son iguales, verifique.");
                    txtPassword.Focus();
                }
            }

            return bRegresa;
        }

        private void GuardarInformacion(int Tipo)
        {

            string sCadena = GnProveedores.IdProveedor + GnProveedores.Usuario + txtPassword.Text.ToUpper(); 
            string sPass = crypto.PasswordEncriptar(sCadena);

            string sSql = string.Format("Exec spp_Net_Usuarios_Mtto '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ", 
                IdEstado, IdFarmacia, "txtIdPersonal.Text", txtLogin.Text, sPass, Tipo.ToString());

            sSql = string.Format("Exec spp_Net_Proveedores_Mtto '{0}', '{1}', '{2}', '{3}' ", 
                GnProveedores.IdProveedor, GnProveedores.Usuario, sPass, "A");

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al grabar el usuario.");
            }
            else
            {
                GnProveedores.Password = sPass; 
                General.msjUser("Contraseña modificada satisfactoriamente.");
                this.Close(); 
            }
        }

        private void FrmCambiarPassword_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
    }
}
