using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmProgramas : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmProgramas()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        #region Eventos

        private void txtIdPro_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "";

            if (txtIdPro.Text.Trim() == "")
            {
                txtIdPro.Text = "*";
                txtIdPro.Enabled = false;
            }
            else
            {
                sDato = string.Format("SELECT * FROM CatProgramas WHERE IdPrograma='{0}' ", Fg.PonCeros(txtIdPro.Text, 4));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtIdPro_Validating");
                    General.msjError("Error al consultar el Programa");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtIdPro.Text = leer.Campo("IdPrograma");
                        txtDescripcion.Text = leer.Campo("Descripcion");
                        txtIdPro.Enabled = false;

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            txtDescripcion.Enabled = false;
                        }
                    }
                    else
                    {
                        General.msjError("El Programa no existe");
                        txtIdPro.Focus();
                    }
                }
            }
        }

        private void txtIdPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Programas("txtId_KeyDown");

                if (leer.Leer())
                {
                    txtIdPro.Text = leer.Campo("IdPrograma");
                    txtDescripcion.Text = leer.Campo("Descripcion");
                    txtIdPro.Enabled = false;

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtDescripcion.Enabled = false;
                    }
                }
            }
        }

        #endregion Eventos

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el Id del Programa por favor");
                txtIdPro.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtIdPro.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string sMensaje = "";
            if (ValidaDatos())
            {                
                if (con.Abrir())
                {
                    iOpcion = 1;
                    con.IniciarTransaccion();

                    string sSql = string.Format(" Exec spp_Mtto_CatProgramas '{0}', '{1}', '{2}'", txtIdPro.Text, txtDescripcion.Text, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        con.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        con.CompletarTransaccion();
                        leer.Leer();
                        //MessageBox.Show(sMensaje); //Este mensaje lo genera el SP
                        General.msjUser("Información guardada satisfactoriamente Id Programa : " + leer.Campo(1));
                        btnNuevo_Click(null, null);
                    }

                    con.Cerrar();
                }
                else
                {
                    General.msjError("Sin conexion al servidor.");
                }                
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string message = "¿ Desea eliminar el Programa seleccionado ?";

            if (ValidaDatos())
            {
                if (lblCancelado.Visible == false)
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (con.Abrir())
                        {
                            iOpcion = 2;
                            con.IniciarTransaccion();

                            string sSql = string.Format(" Exec spp_Mtto_CatProgramas '{0}', '{1}', '{2}'", txtIdPro.Text, txtDescripcion.Text, iOpcion);
                            if (!leer.Exec(sSql))
                            {
                                con.DeshacerTransaccion();
                                Error.GrabarError(leer, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al actualizar la información");
                            }
                            else
                            {
                                con.CompletarTransaccion();
                                leer.Leer();
                                General.msjUser("Información actualizada satisfactoriamente." + leer.Campo(1));
                                btnNuevo_Click(null, null);
                            }

                            con.Cerrar();
                        }
                        else
                        {
                            General.msjError("Sin conexion al servidor.");
                        }
                    }
                }
                else
                {
                    General.msjError("No se puede cancelar, ya esta cancelado");
                }
            }
        }

        #endregion Botones


    }
}
