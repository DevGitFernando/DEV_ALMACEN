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
    public partial class FrmCambiarPassword : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsCriptografo crypto = new clsCriptografo();
        clsDatosCliente datosCliente;

        string IdEstado = DtGeneral.EstadoConectado;
        string IdFarmacia = DtGeneral.FarmaciaConectada;
        string IdPersonal = DtGeneral.IdPersonal;
        string sSqlUpdate;

        public FrmCambiarPassword()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            datosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "CambiarPassword");

            chkUpdateGeneral.Checked = !DtGeneral.EsEquipoDeDesarrollo; 
            chkUpdateGeneral.Visible = DtGeneral.EsEquipoDeDesarrollo; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            chkUpdateGeneral.Checked = !DtGeneral.EsEquipoDeDesarrollo;
            chkUpdateGeneral.Visible = DtGeneral.EsEquipoDeDesarrollo; 

            txtIdPersonal.Enabled = false;
            txtLogin.Enabled = false;

            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblNombrePersonal.Text = DtGeneral.NombrePersonal;
            txtLogin.Text = DtGeneral.LoginUsuario;
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
            bool bRegresa = true;
            string sCadena = IdEstado + IdFarmacia + txtIdPersonal.Text + txtPasswordAnterior.Text.ToUpper();
            string sPass = crypto.PasswordEncriptar(sCadena);

            if (bRegresa && sPass != DtGeneral.PasswordUsuario)
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
            string sMsj = ""; 

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            }
            else 
            {
                cnn.IniciarTransaccion();

                string sCadena = IdEstado + IdFarmacia + txtIdPersonal.Text + txtPassword.Text.ToUpper();
                string sPass = crypto.PasswordEncriptar(sCadena);

                sSqlUpdate = string.Format("Update Net_Usuarios Set Password = '{3}', " + 
                    " FechaUpdate = getdate(), Status = 'A', Actualizado = 0 " + 
			        " Where IdEstado = '{0}' and IdSucursal = '{1}' and IdPersonal = '{2}' " , 
                    IdEstado, IdFarmacia, txtIdPersonal.Text, sPass);

                if (!leer.Exec(sSqlUpdate))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al modificar la contraseña.");
                }
                else
                {
                    cnn.CompletarTransaccion();
                    DtGeneral.PasswordUsuario = sPass;
                    sMsj = "Contraseña modificada satisfactoriamente en la unidad.";


                    if (chkUpdateGeneral.Checked)
                    {
                        CambiarPassServidorRegional(ref sMsj);
                    }


                    General.msjUser(sMsj); 

                    this.Close(); 
                }

                cnn.Cerrar();
            }
        }

        private void FrmCambiarPassword_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private bool CambiarPassServidorRegional(ref string Mensaje)
        {
            bool bRegresa = false;

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.Auditor)
            {
                if (DtGeneral.UrlServidorRegional != "")
                {
                    clsLeerWebExt leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);

                    if (!leerWeb.Exec(sSqlUpdate))
                    {
                        Error.GrabarError(General.DatosConexion, leerWeb.Error, "CambiarPassServidorRegional()");
                        //General.msjError("Ocurrió un error al grabar el nuevo password en el central.");
                    }
                    else
                    {
                        bRegresa = true; 
                        if (!CambiarPassServidorCentral(ref Mensaje))
                        {
                            Mensaje = "Contraseña modificada satisfactoriamente en la unidad y regional.";
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool CambiarPassServidorCentral(ref string Mensaje)
        {
            bool bRegresa = false;

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.Auditor )
            {
                if (DtGeneral.UrlServidorCentral != "")
                {
                    clsLeerWebExt leerWeb = new clsLeerWebExt(DtGeneral.UrlServidorCentral, DtGeneral.CfgIniOficinaCentral, datosCliente);

                    if (!leerWeb.Exec(sSqlUpdate))
                    {
                        Error.GrabarError(General.DatosConexion, leerWeb.Error, "CambiarPassServidorCentral()");
                        //General.msjError("Ocurrió un error al grabar el nuevo password en el central.");
                    }
                    else
                    {
                        bRegresa = true;
                        Mensaje = "Contraseña modificada satisfactoriamente en general.";
                    }
                }
            }

            return bRegresa;
        }
    }
}
