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

namespace DllFarmaciaSoft.OrdenesDeCompra
{
    public partial class FrmCatGruposRecepcionProv : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsAyudas ayuda;

        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        public FrmCatGruposRecepcionProv()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);

            query = new clsConsultas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            ayuda = new clsAyudas(General.DatosConexion, sModulo, this.Name, sVersion, true);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);
        }

        private void FrmCatGruposRecepcionProv_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                GrabarInformacion(1);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                GrabarInformacion(2);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblStatus.Visible = false;
            IniciaBotones(false, false, false);
            txtGrupo.Focus();
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Eventos
        private void txtGrupo_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtGrupo.Text.Trim() == "")
            {
                txtGrupo.Text = "*";
                txtGrupo.Enabled = false;
                IniciaBotones(true, false, false);
            }
            else
            {
                sSql = string.Format(" Select * From COM_Cat_Grupos_Recepcion Where IdGrupo = '{0}' ", Fg.PonCeros(txtGrupo.Text, 3));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtGrupo_Validating");
                    General.msjError("Ocurrio un error al consultar el grupo.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtGrupo.Text = leer.Campo("IdGrupo");
                        txtDescripcion.Text = leer.Campo("DescripcionGrupo");

                        IniciaBotones(true, true, false);

                        if (leer.Campo("Status") == "C")
                        {
                            lblStatus.Visible = true;
                            IniciaBotones(true, false, false);
                        }
                    }
                    else
                    {
                        General.msjAviso("No se encontro el grupo capturado.");
                    }
                }
            }
        }
        #endregion Eventos

        #region GuardarInformacion
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtGrupo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Clave de grupo incorrecta.");
                txtGrupo.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar la descripción del grupo.");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        private void GrabarInformacion(int Opcion)
        {
            string sMsj = "Ocurrió un error al guardar la información";

            if (Opcion == 2)
            {
                sMsj = "Ocurrió un error al cancelar la información";
            }

            if (cnn.Abrir())
            {
                string sSql = string.Format(" Exec spp_Mtto_COM_Cat_Grupos_Recepcion '{0}', '{1}', '{2}' ",
                                            txtGrupo.Text, txtDescripcion.Text, Opcion);

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
                    LimpiaPantalla();
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No fue posible establecer conexión con el servidor. Intente de nuevo.");
            }
        }
        #endregion GuardarInformacion
    }
}
