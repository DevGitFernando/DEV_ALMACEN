using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    public partial class FrmPersonal : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConsultas query; // = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsFarmacias = new DataSet();

        public FrmPersonal()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, "Configuracion", "FrmPersonal", Application.ProductVersion, true);
            Ayuda = new clsAyudas(General.DatosConexion, "OficinaCentral", this.Name, Application.ProductVersion);
        }

        private void FrmPersonal_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
            CargarEstados();
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.ComboEstados("CargarEstados()"), true, "IdEstado", "EstadoNombre");

            leer.Exec("Select IdEstado, IdFarmacia, ( IdFarmacia + ' -- ' + NombreFarmacia ) as NombreFarmacia From CatFarmacias (NoLock)");
            dtsFarmacias = leer.DataSetClase;

            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            cboSucursales.Clear();
            cboSucursales.Add("0", "<< Seleccione >>");

            if (cboEstados.SelectedIndex != 0)
                cboSucursales.Add(dtsFarmacias.Tables[0].Select("IdEstado = '" + cboEstados.Data + "'"), true, "IdFarmacia", "NombreFarmacia");

            cboSucursales.SelectedIndex = 0;
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            FrameDatosPersonal.Enabled = false;
            dtpFechaReg.Enabled = false;
            txtIdPersonal.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GrabarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GrabarInformacion(2);
        }

        private void GrabarInformacion(int Tipo)
        {
            string sSql = string.Format(" Exec spp_Mtto_CatPersonal '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ", 
                cboEstados.Data, cboSucursales.Data, txtIdPersonal.Text, txtNombre.Text, txtApPaterno.Text, txtApMaterno.Text, Tipo.ToString());
            string sMsj = "Ocurrió un error al guardar la informaciíon.";

            if (Tipo != 1)
                sMsj = "Ocurrió un error al cancelar la información.";

            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    General.Error.GrabarError(leer.Error, General.DatosConexion, "Configuracion", Application.ProductVersion, this.Name, "GrabarInformacion", leer.QueryEjecutado);
                    General.msjError(sMsj);
                }
                else
                {
                    cnn.CompletarTransaccion();
                    leer.Leer();
                    General.msjUser(leer.Campo(2));
                    LimpiarPantalla();
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No se establecio conexión al servidor, intente de nuevo.");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones 

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        private void cboSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSucursales.SelectedIndex != 0)
                FrameDatosPersonal.Enabled = true;
            else
                FrameDatosPersonal.Enabled = false;
        }

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() == "" || txtIdPersonal.Text.Trim() == "*")
            {
                txtIdPersonal.Enabled = false;
                txtIdPersonal.Text = "*";
            }
            else
            {
                query.MostrarMsjSiLeerVacio = true;
                leer.DataSetClase = query.Personal(cboEstados.Data, cboSucursales.Data, txtIdPersonal.Text, "txtIdPersonal_Validating");

                if (leer.Leer())
                {
                    CargarDatosPersonal();
                }
                else
                {
                    Fg.IniciaControles(this, true, FrameDatosPersonal);
                }
            }
        }

        private void CargarDatosPersonal()
        {
            txtIdPersonal.Text = leer.Campo("IdPersonal");
            txtNombre.Text = leer.Campo("Nombre");
            txtApPaterno.Text = leer.Campo("ApPaterno");
            txtApMaterno.Text = leer.Campo("ApMaterno");

            dtpFechaReg.Value = leer.CampoFecha("FechaRegistro");

            lblCancelado.Visible = false;
            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                General.msjUser("El personal con la clave " +  txtIdPersonal.Text + " actualmente se encuentra cancelado.");
            }
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Personal("txtIdPersonal_KeyDown", cboEstados.Data, cboSucursales.Data);
                if (leer.Leer())
                    CargarDatosPersonal();
            }
        }

        private void cboSucursales_Validated(object sender, EventArgs e)
        {
            lblCancelado.Visible = false;
            if (cboSucursales.SelectedIndex != 0)
            {
                Fg.IniciaControles(this, true, FrameDatosPersonal);
                dtpFechaReg.Enabled = false;
                txtIdPersonal.Focus();
            }
        }

    }
}
