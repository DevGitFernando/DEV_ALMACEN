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

namespace Farmacia.Vales
{
    public partial class FrmCatParentescos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsConsultas Consultas;

        public FrmCatParentescos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false);
            txtId.Enabled = true;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Grabar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Grabar(2);
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref cnn);

            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
                btnGuardar.Enabled = true;
            }
            else
            {
                leer.DataSetClase = Consultas.Parentescos(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("Folio");
                    txtDescripcion.Text = leer.Campo("Descripcion");
                    IniciaToolBar(true, true, true);

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        IniciaToolBar(true, true, false);
                    }
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private void Grabar(int iOpcion)
        {
            string sSql = string.Format("Exec spp_Mtto_Parentesco '{0}', '{1}', {2}",
                txtId.Text, txtDescripcion.Text, iOpcion);

            if (txtDescripcion.Text.Trim() != "")
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();
                    if (!leer.Exec(sSql))
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "Grabar");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else
                    {
                        leer.Leer();
                        cnn.CompletarTransaccion();
                        General.msjUser(leer.Campo("Mensaje"));
                        btnNuevo_Click(null, null);
                    }
                    cnn.Cerrar();
                }
            }
            else
            {
                General.msjUser("No ha especificado la descripción del parentesco, verifique.");
                txtDescripcion.Focus();
            }
        }
    }
}
