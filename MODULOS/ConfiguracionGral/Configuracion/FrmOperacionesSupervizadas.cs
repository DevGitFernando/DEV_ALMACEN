using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 

namespace Configuracion.Configuracion
{
    public partial class FrmOperacionesSupervizadas : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;
        // clsGrid myGrid; 

        public FrmOperacionesSupervizadas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnConfiguracion.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnConfiguracion.DatosApp, this.Name); 
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            lblCancelado.Visible = false; 
            Fg.IniciaControles();
            txtId.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Operación inválida, verifique.");
                txtId.Focus(); 
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Nombre inválido, verifique.");
                txtNombre.Focus(); 
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Descripción inválida, verifique."); 
                txtDescripcion.Focus(); 
            }

            return bRegresa; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
                GuardarInformacion(1); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarCancelacion())
                GuardarInformacion(2); 
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "" || txtId.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Clave de Operación inválida, verifique."); 
                txtId.Focus(); 
            }

            if (bRegresa)
            {
                if (General.msjConfirmar("¿ Desea cancelar la información en pantalla ?") == DialogResult.No)
                    bRegresa = false;
            }

            return bRegresa; 
        } 

        private void GuardarInformacion(int Opcion)
        {
            string sMsjExito = "Información guardada satisfactoriamente.";
            string sMsjError = "Ocurrió un error al guardar la información."; 
            string sSql = string.Format(" Exec spp_Net_Operaciones_Supervisadas '{0}', '{1}', '{2}', '{3}' ", 
                txtId.Text, txtNombre.Text, txtDescripcion.Text, Opcion.ToString());

            if (Opcion == 2)
            {
                sMsjExito = "Información cancelada satisfactoriamente.";
                sMsjError = "Ocurrió un error al cancelar la información."; 
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GuardarInformacion");
                General.msjError(sMsjError); 
            }
            else
            {
                General.msjUser(sMsjExito);
                LimpiarPantalla(); 
            }
        } 
        #endregion Botones 

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*"; 
            }
            else 
            {
                leer.DataSetClase = query.OperacionesSupervidas(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    CargarDatos(); 
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.OperacionesSupervidas("txtId_KeyDown");
                if (leer.Leer())
                {
                    CargarDatos();
                }
            } 
        } 

        private void CargarDatos()
        {
            txtId.Enabled = false;
            txtId.Text = leer.Campo("IdOperacion");
            txtNombre.Enabled = false; 
            txtNombre.Text = leer.Campo("Nombre");

            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                General.msjUser("La Operación Supervizada actualmente se encuentra cancelada."); 
            }
        }
    }
}
