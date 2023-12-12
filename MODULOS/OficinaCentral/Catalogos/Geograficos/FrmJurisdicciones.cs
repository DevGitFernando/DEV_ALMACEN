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
    public partial class FrmJurisdicciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        public FrmJurisdicciones()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            cboEdo.Clear();
            cboEdo.Add("0", "<< Seleccione >>");
            cboEdo.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "EstadoNombre");
            cboEdo.SelectedIndex = 0;

        }

        private void FrmJurisdicciones_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            lblCancelado.Visible = false;
            cboEdo.SelectedIndex = 0;
            cboEdo.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (cboEdo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Estado de la Jurisdicción, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && txtIdJuris.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Jurisdicción inválida, verifique.");
                txtIdJuris.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción de la Jurisdicción, verifique.");
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

            if (bRegresa && cboEdo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado para la cancelación de Jurisdicción, verifique.");
            }

            if (bRegresa &&  (txtIdJuris.Text.Trim() == "" || txtIdJuris.Text.Trim() == "*"))
            {
                bRegresa = false;
                General.msjUser("Clave de Jurisdicción inválida, verifique.");
            }

            if ( bRegresa )
            {
                if (General.msjCancelar("¿ Desea cancelar la información de la Jurisdicción ?") == DialogResult.No)
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

            if ( Opcion == 2 )
                sMsj = "Ocurrió un error al cancelar la información";

            if (cnn.Abrir())
            {
                string sSql = string.Format(" Exec spp_Mtto_CatJurisdicciones '{0}', '{1}', '{2}', '{3}' ", 
                    cboEdo.Data, txtIdJuris.Text, txtDescripcion.Text, Opcion);

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

        private void cboEdo_Validating(object sender, CancelEventArgs e)
        {
            if (cboEdo.SelectedIndex != 0)
                cboEdo.Enabled = false;
        }

        private void txtIdJuris_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            if (txtIdJuris.Text.Trim() == "" || txtIdJuris.Text.Trim() == "*")
            {
                txtIdJuris.Enabled = false;
                txtIdJuris.Text = "*";
            }
            else
            {
                leer.DataSetClase = query.Jurisdicciones(cboEdo.Data, txtIdJuris.Text, "txtIdJuris_Validating");
                if (leer.Leer())
                {
                    CargarDatosJurisdiccion(); 
                }
            }
        }

        private void CargarDatosJurisdiccion()
        {
            txtIdJuris.Enabled = false;
            txtIdJuris.Text = leer.Campo("IdJurisdiccion");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                General.msjUser("La Jurisdicción actualmente se encuentra cancelada.");
            }
        }

        private void txtIdJuris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Jurisdicciones("txtIdJuris_KeyDown", cboEdo.Data);
                if (leer.Leer())
                    CargarDatosJurisdiccion();
            }
        }

    }
}
