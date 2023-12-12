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
    public partial class FrmColonias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;       

        public FrmColonias()
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
            txtIdEstado.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdEstado.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado capturado un Estado válido, verifique.");
                txtIdEstado.Focus();
            }

            if (bRegresa && txtMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
                txtMunicipio.Focus();
            }

            if (bRegresa && txtColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Colonia inválida, verifique.");
                txtColonia.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim()== "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción(Nombre) de la Colonia, verifique.");
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

            if (bRegresa && txtIdEstado.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado para la cancelación de Municipio, verifique.");
            }

            if (bRegresa && txtMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
            }

            if (bRegresa && (txtColonia.Text.Trim() == "" || txtColonia.Text.Trim() == "*"))
            {
                bRegresa = false;
                General.msjUser("Clave de Colonia inválida, verifique.");
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
                string sSql = string.Format(" Exec spp_Mtto_CatColonias '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                    txtIdEstado.Text, txtMunicipio.Text, txtColonia.Text, txtDescripcion.Text, txtCodigoPostal.Text, Opcion);

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

        private void FrmColonias_Load(object sender, EventArgs e)
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
            lblMunicipio.Text = ""; 
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
            if (txtMunicipio.Text.Trim() != "" )
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
            txtMunicipio.Enabled = false;
            txtMunicipio.Text = leer.Campo("IdMunicipio");
            lblMunicipio        .Text = leer.Campo("Descripcion");
        }

        private void txtColonia_Validating(object sender, CancelEventArgs e)
        {
            lblStatus.Visible = false;
            if (txtMunicipio.Text.Trim() != "")
            {
                if (txtColonia.Text.Trim() == "" || txtColonia.Text.Trim() == "*")
                {
                    txtColonia.Enabled = false;
                    txtColonia.Text = "*";
                }
                else
                {
                    leer.DataSetClase = Consultas.Colonias(txtIdEstado.Text, txtMunicipio.Text, txtColonia.Text, "txtColonia_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosColonia();
                    }
                    else
                    {
                        txtColonia.Text = "";
                        txtDescripcion.Text = "";
                        e.Cancel = true;
                    }
                }
            }
        }

        private void CargarDatosColonia()
        {
            txtColonia.Enabled = false;
            txtColonia.Text = leer.Campo("IdColonia");
            txtDescripcion.Text = leer.Campo("Descripcion");
            txtCodigoPostal.Text = leer.Campo("CodigoPostal"); 

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblStatus.Visible = true;
                General.msjUser("La Colonia actualmente se encuentra cancelada.");
            }
        }

        private void txtColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Colonias(txtIdEstado.Text, txtMunicipio.Text, "txtColonia_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosColonia();
                }
                else
                {
                    txtColonia.Text = "";
                    txtDescripcion.Text = "";
                    txtColonia.Focus();
                }
            }
        }

        private void btnEstados_Click(object sender, EventArgs e)
        {
            FrmEstados f = new FrmEstados();
            f.ShowDialog(); 
        }

        private void btnMunicipios_Click(object sender, EventArgs e)
        {
            FrmMunicipios f = new FrmMunicipios();
            f.ShowDialog(); 
        }

    }
}
