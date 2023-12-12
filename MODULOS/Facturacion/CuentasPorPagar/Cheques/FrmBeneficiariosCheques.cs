using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

namespace Facturacion.CuentasPorPagar
{
    public partial class FrmBeneficiariosCheques : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmBeneficiariosCheques()
        {
            InitializeComponent();

            cnn.SetConnectionString();

            myLeer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmBeneficiariosCheques_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            rdoFisica.Checked = true;
            Fg.IniciaControles(this, true);
            InicializarBotones(true, false);
            lblCancelado.Visible = false;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validacion())
            {
                GrabarInformacion(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (ValidaDatos())
            GrabarInformacion(2);
        }

        private void InicializarBotones(bool Guadar, bool Cancelar)
        {
            btnGuardar.Enabled = Guadar;
            btnCancelar.Enabled = Cancelar;
        }
        #endregion Botones

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                txtId.Text = Fg.PonCeros(txtId.Text, 6);
                string sSql = string.Format("Select * From CNT_CatChequesBeneficiarios Where IdBeneficiario = '{0}'", txtId.Text.Trim());
                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        CargaDatos();
                    }
                    else
                    {
                        General.msjAviso("No se a encontrado el beneficiario.");
                        btnNuevo_Click(null, null);
                    }
                }
                else
                {
                    Error.GrabarError(myLeer, "txtId_Validating");

                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.BeneficiarioChequera("txtBeneficiario_KeyDown()");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        private void GrabarInformacion(int iOpcion)
        {
            bool bEsMoral = rdoMoral.Checked;
            string sMsjErr = "Ocurrió un error al guardar la información.";

            if (iOpcion != 1)
            {
                sMsjErr = "Ocurrió un error al cancelar la información.";
            }

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sSql = String.Format("Exec spp_Mtto_CatChequesBeneficiarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, {9}",
                        txtId.Text.Trim(), txtDescripcion.Text.Trim(), txtRFC.Text.Trim(), txtTelefono.Text.Trim(),
                        txtEstado.Text.Trim(), txtMunicipio.Text.Trim(), txtDireccion.Text.Trim(), txtCP.Text.Trim(), bEsMoral, iOpcion);
                if (myLeer.Exec(sSql))
                {
                    myLeer.Leer();
                    cnn.CompletarTransaccion();
                    General.msjUser(myLeer.Campo("Mensaje")); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(myLeer, "btnGuardar_Click");
                    General.msjError(sMsjErr);
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
        }

        private void CargaDatos()
        {
            txtId.Text = myLeer.Campo("IdBeneficiario");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtRFC.Text = myLeer.Campo("RFC");
            txtTelefono.Text = myLeer.Campo("Telefono");
            txtEstado.Text = myLeer.Campo("Estado");
            txtMunicipio.Text = myLeer.Campo("Municipio");
            txtDireccion.Text = myLeer.Campo("Direccion");
            txtCP.Text = myLeer.Campo("CP");
            rdoFisica.Checked = !myLeer.CampoBool("Esmoral");
            rdoMoral.Checked = myLeer.CampoBool("Esmoral");
            InicializarBotones(true, true);

            txtRFC.Enabled = false;
            txtId.Enabled = false;
            if (myLeer.Campo("Status") == "C")
            {
                InicializarBotones(true, false);
                lblCancelado.Visible = true;
            }
        }

        private bool Validacion()
        {
            bool bContinua = true;

            if (txtDescripcion.Text == "")
            {
                General.msjAviso("Debe capturar un nombre, verifique porfavor.");
                bContinua = false;
            }

            if (txtRFC.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar un RFC, verifique porfavor.");
                bContinua = false;
            }

            if ( txtRFC.Text.Length < 9 && bContinua)
            {
                General.msjAviso("El RFC debe de tener mas de 9 caracteres, verifique porfavor.");
                bContinua = false;
            }

            if (txtId.Text == "*" && bContinua)
            {
                string sSql = string.Format("Select * From CNT_CatChequesBeneficiarios Where RFC = '{0}'", txtRFC.Text.Trim());

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        General.msjAviso("El RFC es repetido, verifique porfavor.");
                        bContinua = false;
                    }
                }
                else
                {
                    General.msjError("Ocurrió un error, intente denuevo.");
                    Error.GrabarError(myLeer, "Validacion()");

                }
            }

            if (txtTelefono.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar un telefono, verifique porfavor.");
                bContinua = false;
            }

            if (txtEstado.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar un estado, verifique porfavor.");
                bContinua = false;
            }

            if (txtMunicipio.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar un municipio, verifique porfavor.");
                bContinua = false;
            }

            if (txtDireccion.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar la dirección del beneficiario, verifique porfavor.");
                bContinua = false;
            }

            if (txtCP.Text == "" && bContinua)
            {
                General.msjAviso("Debe capturar un codigo postal, verifique porfavor.");
                bContinua = false;
            }

            return bContinua;
        }
    }
}
