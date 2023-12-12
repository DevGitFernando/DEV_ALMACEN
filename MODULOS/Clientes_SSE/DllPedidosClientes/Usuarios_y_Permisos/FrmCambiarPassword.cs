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

namespace DllPedidosClientes.Usuarios_y_Permisos
{
    public partial class FrmCambiarPassword : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();

        // string IdEstado = ""; //DtGeneral.EstadoConectado;
        // string IdFarmacia = ""; //DtGeneral.FarmaciaConectada;
        // string IdPersonal = ""; //DtGeneral.IdPersonal;

        public FrmCambiarPassword()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            //txtIdPersonal.Enabled = false; 
            txtLogin.Enabled = false;
            txtLogin.Text = DtGeneralPedidos.LoginUsuario;  // DtGeneral.LoginUsuario;

            //txtIdPersonal.Text = ""; //DtGeneral.IdPersonal;
            //lblNombrePersonal.Text = ""; //DtGeneral.NombrePersonal;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GuardarInformacion(1);
            }
        }

        private bool validarDatos()
        {
            // DtGeneralPedidos.LoginUsuario
            bool bRegresa = true;
            string sCadena = "";
            string sPass = "";

            sCadena = DtGeneralPedidos.EstadoConectado + DtGeneralPedidos.LoginUsuario + txtPasswordAnterior.Text.ToUpper();
            //if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                sCadena = DtGeneralPedidos.EstadoConectado + DtGeneralPedidos.FarmaciaConectada + DtGeneralPedidos.LoginUsuario + txtPasswordAnterior.Text.ToUpper();
            }
            sPass = crypto.PasswordEncriptar(sCadena); 

            // string sCadena = GnProveedores.IdProveedor + DtGeneralPedidos.LoginUsuario + txtPasswordAnterior.Text.ToUpper(); 
            // string sCadena = DtGeneralPedidos.LoginUsuario + DtGeneralPedidos.LoginUsuario + txtPasswordAnterior.Text.ToUpper(); 
            // string sPass = crypto.PasswordEncriptar(sCadena); 

            if (bRegresa && sPass != DtGeneralPedidos.PasswordUsuario)  //DtGeneral.PasswordUsuario)
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
            string sSql = "";
            // bool bRegresa = true;
            string sCadena = "";
            string sPass = "";

            sCadena = DtGeneralPedidos.EstadoConectado + DtGeneralPedidos.LoginUsuario + txtPassword.Text.ToUpper();
            sPass = crypto.PasswordEncriptar(sCadena); 
            sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.IdPersonal, DtGeneralPedidos.NombrePersonal, 
                txtLogin.Text, sPass, 1);

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                sCadena = DtGeneralPedidos.EstadoConectado + DtGeneralPedidos.FarmaciaConectada + DtGeneralPedidos.LoginUsuario + txtPassword.Text.ToUpper();
                sPass = crypto.PasswordEncriptar(sCadena);
                // sSql = string.Format("Exec spp_Mtto_Net_Unidad_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ", 
                //    cboEstados.Data, cboFarmacias.Data, txtIdUsuario.Text, txtNombre.Text.Trim(), txtLogin.Text, sPass, Tipo.ToString());

                sSql = string.Format("Exec spp_Mtto_Net_Unidad_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada, 
                    DtGeneralPedidos.IdPersonal, DtGeneralPedidos.NombrePersonal, DtGeneralPedidos.LoginUsuario, sPass, 1);
            }

            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al realizar el cambio de password.");
            }
            else
            {
                DtGeneralPedidos.PasswordUsuario = sPass;
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
