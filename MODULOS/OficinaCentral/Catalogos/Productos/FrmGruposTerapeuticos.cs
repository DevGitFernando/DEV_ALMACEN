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
    public partial class FrmGruposTerapeuticos : FrmBaseExt
    {
        clsDatosApp DatosApp = GnOficinaCentral.DatosApp;
        clsDatosConexion DatosCnn = General.DatosConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmGruposTerapeuticos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(DatosCnn, DatosApp, this.Name);
            query = new clsConsultas(DatosCnn, DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DatosApp, this.Name);

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lblCancelado.Visible = false;
            txtIdGrupo.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtIdGrupo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Grupo inválido, verifique.");
                txtIdGrupo.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Descripción del grupo terapeutico, verifique.");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                GrabarInformacion(1);
            }
        }

        private void GrabarInformacion(int Opcion)
        {
            string sMsj = "Información guardada satisfactoriamente.";
            string sMsjError = "Ocurrió un error al grabar la información";

            string sCadena = "";

            if (Opcion == 2)
            {
                sMsj = "Información cancelada satisfactoriamente.";
                sMsjError = "Ocurrió un error al cancelar la información"; 
            }

            if (cnn.Abrir())
            {
                string sSql = string.Format("Exec spp_Mtto_CatGruposTerapeuticos '{0}', '{1}', '{2}' ", txtIdGrupo.Text, txtDescripcion.Text, Opcion.ToString());

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (!leer.Exec(sSql))
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(leer, "GrabarInformacion()");
                    General.msjError(sMsjError);
                }
                else 
                {
                    cnn.CompletarTransaccion();
                    leer.Leer();
                    if (Opcion == 1)
                    {
                        sMsj = leer.Campo("Mensaje");
                    }
                    General.msjUser(sMsj);
                    LimpiarPantalla();
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo");
            }
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;

            if (txtIdGrupo.Text.Trim() == "" || txtIdGrupo.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Clave de Grupo inválido, verifique.");
                txtDescripcion.Focus();
            }

            if (bRegresa)
            {
                if (General.msjCancelar("¿ Desea cancelar el Grupo Terapeutico ?") == DialogResult.No)
                    bRegresa = false;
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarCancelacion())
                GrabarInformacion(2);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        private void txtIdGrupo_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            lblCancelado.Visible = false;

            if (txtIdGrupo.Text.Trim() == "" || txtIdGrupo.Text.Trim() == "*")
            {
                txtIdGrupo.Enabled = false;
                txtIdGrupo.Text = "*";
            }
            else
            {
                leer.DataSetClase = query.GruposTerapeuticos(txtIdGrupo.Text, "txtIdGrupo_Validating");

                sCadena = query.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                    CargarDatosGrupo();
            }
        }

        private void txtIdGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.GruposTerapeuticos("txtIdGrupo_KeyDown");

                sCadena = ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (leer.Leer())
                    CargarDatosGrupo();
            }
        }

        private void CargarDatosGrupo()
        {
            txtIdGrupo.Enabled = false;
            txtIdGrupo.Text = leer.Campo("IdGrupoTerapeutico");
            txtDescripcion.Text = leer.Campo("Descripcion");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                General.msjUser("El Grupo Terapeutico actualmente esta cancelado.");
            }
        }
    }
}
