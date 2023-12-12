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

namespace OficinaCentral.Catalogos
{
    public partial class FrmColonias : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        public FrmColonias()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, sModulo, this.Name, sVersion);
            ayuda = new clsAyudas(General.DatosConexion, sModulo, this.Name, sVersion);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);

            CargarEstados();
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblStatus.Visible = false;
            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado, verifique.");
                cboEstados.Focus();
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
                GrabarInformacion(1);
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;

            if (bRegresa && cboEstados.SelectedIndex == 0)
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
                    bRegresa = false;
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if ( validarCancelacion() )
                GrabarInformacion(2);
        }

        private void GrabarInformacion(int Opcion)
        {
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
                sMsj = "Ocurrió un error al cancelar la información";

            if (cnn.Abrir())
            {
                string sSql = string.Format(" Exec spp_Mtto_CatColonias '{0}', '{1}', '{2}', '{3}', '{4}' ",
                    cboEstados.Data, txtMunicipio.Text, txtColonia.Text, txtDescripcion.Text, Opcion);

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
            else
                General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
        }
        #endregion Botones

        private void FrmColonias_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "EstadoNombre");
            cboEstados.SelectedIndex = 0;
        }

        private void cboEstados_Validating(object sender, CancelEventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
                cboEstados.Enabled = false;
        }

        private void txtMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtMunicipio.Text.Trim() != "" )
            {
                leer.DataSetClase = query.Municipios(cboEstados.Data, txtMunicipio.Text, "txtMunicipio_Validating");
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
                leer.DataSetClase = ayuda.Municipios("txtMunicipio_KeyDown", cboEstados.Data);
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
                    leer.DataSetClase = query.Colonias(cboEstados.Data, txtMunicipio.Text, txtColonia.Text, "txtColonia_Validating");
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
                leer.DataSetClase = ayuda.Colonias("txtColonia_KeyDown", cboEstados.Data, txtMunicipio.Text);
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

    }
}
