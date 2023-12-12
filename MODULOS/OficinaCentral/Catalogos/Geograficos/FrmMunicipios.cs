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
    public partial class FrmMunicipios : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        public FrmMunicipios()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            ayuda = new clsAyudas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);

            CargarEstados();
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";
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
            if ( validaDatos() )
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

            if (bRegresa && (txtMunicipio.Text.Trim() == "" || txtMunicipio.Text.Trim() == "*"))
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
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
                string sSql = string.Format(" Exec spp_Mtto_CatMunicipios '{0}', '{1}', '{2}', '{3}' ",
                    cboEstados.Data, txtMunicipio.Text, txtDescripcion.Text, Opcion);

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

        private void FrmMunicipios_Load(object sender, EventArgs e)
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
            if (txtMunicipio.Text.Trim() == "" || txtMunicipio.Text.Trim() == "*")
            {
                txtMunicipio.Enabled = false;
                txtMunicipio.Text = "*";
            }
            else
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
            lblStatus.Visible = false;
            txtMunicipio.Text = leer.Campo("IdMunicipio");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblStatus.Visible = true;
                General.msjUser("El Municipio actualmente se encuentra cancelado.");
            }
        }
    }
}
