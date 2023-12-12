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
    public partial class FrmCatRechazos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        string sModulo = GnOficinaCentral.Modulo;
        string sVersion = GnOficinaCentral.Version;

        public FrmCatRechazos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(sModulo, sVersion, this.Name);
        }

        private void FrmCatRechazos_Load(object sender, EventArgs e)
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
            txtRechazo.Focus();
        }

        private void IniciaBotones(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones

        #region Eventos
        private void txtRechazo_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtRechazo.Text.Trim() == "")
            {
                txtRechazo.Text = "*";
                txtRechazo.Enabled = false;
                IniciaBotones(true, false, false);
            }
            else
            {
                sSql = string.Format(" Select * From COM_Cat_Rechazos Where IdRechazo = '{0}' ", Fg.PonCeros(txtRechazo.Text, 3));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtRechazo_Validating");
                    General.msjError("Ocurrio un error al consultar el Rechazo.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtRechazo.Text = leer.Campo("IdRechazo");
                        txtDescripcion.Text = leer.Campo("Descripcion");

                        IniciaBotones(true, true, false);

                        if (leer.Campo("Status") == "C")
                        {
                            lblStatus.Visible = true;
                            IniciaBotones(true, false, false);
                        }
                    }
                    else
                    {
                        General.msjAviso("No se encontro el Rechazo capturado.");
                    }
                }
            }
        }
        #endregion Eventos

        #region GuardarInformacion
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtRechazo.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Clave de rechazo incorrecta.");
                txtRechazo.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Favor de capturar la descripción del rechazo.");
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
                string sSql = string.Format(" Exec spp_Mtto_COM_Cat_Rechazos '{0}', '{1}', '{2}' ",
                                            txtRechazo.Text, txtDescripcion.Text, Opcion);

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
