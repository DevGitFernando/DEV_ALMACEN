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

namespace Dll_MA_IFacturacion.Catalogos
{
    public partial class FrmMunicipios : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        public FrmMunicipios()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);            
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";
            txtIdEstado.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Estado válido, verifique.");
                txtIdEstado.Focus();
            }

            if (bRegresa && txtMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
                txtMunicipio.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim()== "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción(Nombre) del Municipio, verifique.");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                GrabarInformacion(1);
            }
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Estado para la cancelación de Municipio, verifique.");
            }

            if (bRegresa && (txtMunicipio.Text.Trim() == "" || txtMunicipio.Text.Trim() == "*"))
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
            }

            if (bRegresa)
            {
                if (General.msjCancelar("¿ Desea cancelar la información del Municipio ?") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarCancelacion())
            {
                GrabarInformacion(2);
            }
        }

        private void GrabarInformacion(int Opcion)
        {
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
                sMsj = "Ocurrió un error al cancelar la información";

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else 
            {
                string sSql = string.Format(" Exec spp_Mtto_CatMunicipios '{0}', '{1}', '{2}', '{3}' ",
                    txtIdEstado.Text, txtMunicipio.Text, txtDescripcion.Text, Opcion);

                cnn.IniciarTransaccion();
                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GrabarInformacion");
                    General.msjError(sMsj);
                }
                else
                {
                    cnn.CompletarTransaccion();
                    leer.Leer();
                    General.msjUser(leer.Campo("Mensaje"));
                    LimpiarPantalla();
                }

                cnn.Cerrar();
            }
        }

        #endregion Botones

        private void FrmMunicipios_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.F6:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;

                case Keys.F7:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtMunicipio.Text = "";
            txtDescripcion.Text = "";
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "txtIdEstado_KeyDown");
                if (!leer.Leer())
                {
                    txtIdEstado.Focus();
                }
                else
                {
                    CargarInfEstado();
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdEstado_KeyDown");
                if (leer.Leer())
                {
                    CargarInfEstado();
                }
            }
        }

        private void CargarInfEstado()
        {
            txtIdEstado.Enabled = false; 
            txtIdEstado.Text = leer.Campo("IdEstado");
            lblEstado.Text = leer.Campo("Descripcion");
        }

        private void txtMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtMunicipio.Text.Trim() == "" || txtMunicipio.Text.Trim() == "*")
            {
                txtMunicipio.Enabled = false;
                txtMunicipio.Text = "*";
            }
            else
            {
                leer.DataSetClase = Consultas.Municipios(txtIdEstado.Text, txtMunicipio.Text, "txtMunicipio_Validating");
                if (leer.Leer())
                {
                    CargarDatosMunicipio();
                }
                else
                {
                    txtMunicipio.Text = "";
                    txtDescripcion.Text = "";
                    e.Cancel = true;
                }
            }
        }

        private void txtMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Municipios(txtIdEstado.Text, "txtMunicipio_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosMunicipio();
                }
                else
                {
                    txtMunicipio.Text = "";
                    txtDescripcion.Text = "";
                    txtMunicipio.Focus();
                }
            }
        }

        private void CargarDatosMunicipio()
        {
            lblStatus.Visible = false;
            txtMunicipio.Text = leer.Campo("IdMunicipio");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblStatus.Visible = true;
                General.msjUser("El Municipio actualmente se encuentra cancelado.");
            }
        }

        private void btnEstados_Click(object sender, EventArgs e)
        {
            FrmEstados f = new FrmEstados();
            f.ShowDialog(); 
        }
    }
}
