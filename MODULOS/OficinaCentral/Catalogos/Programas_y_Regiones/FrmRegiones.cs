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
    public partial class FrmRegiones : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmRegiones()
        {
            InitializeComponent();
            con.SetConnectionString();

            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        private void txtIdReg_Validating(object sender, CancelEventArgs e)
        {
            string sDato = "";

            if (txtIdReg.Text.Trim() == "")
            {
                txtIdReg.Text = "*";
                txtIdReg.Enabled = false;
            }
            else
            {
                sDato = string.Format(" SELECT * FROM CatRegiones WHERE IdRegion ='{0}' ", Fg.PonCeros(txtIdReg.Text, 2));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtIdReg_Validating");
                    General.msjError("Error al buscar Region");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtIdReg.Text = leer.Campo("IdRegion");
                        txtDescripcion.Text = leer.Campo("Descripcion");
                        txtIdReg.Enabled = false;

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            txtDescripcion.Enabled = false;
                        }
                    }
                    else
                    {
                        General.msjError("La Region no existe");
                        txtIdReg.Focus();
                    }
                }
            }
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

                    string sSql = string.Format(" Exec spp_Mtto_CatRegiones '{0}', '{1}', '{2}'", txtIdReg.Text, txtDescripcion.Text, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        con.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        con.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {                      
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtIdReg.Focus();
        }
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción por favor");
                txtDescripcion.Focus();
            }
            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string sMensaje = "";
            string message = "¿ Desea eliminar la Región seleccionada ?";

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

                            string sSql = string.Format(" Exec spp_Mtto_CatRegiones '{0}', '{1}', '{2}'", txtIdReg.Text, txtDescripcion.Text, iOpcion);
                            if (!leer.Exec(sSql))
                            {
                                con.DeshacerTransaccion();
                                General.msjError("Ocurrió un error al actualizar la información");
                            }
                            else
                            {
                                con.CompletarTransaccion();
                                leer.Leer();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
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
                    General.msjError("No se puede cancelar, ya esta cancelada");
                }
            }
        }

        private void txtIdReg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Regiones("txtId_KeyDown");

                if (leer.Leer())
                {
                    txtIdReg.Text = leer.Campo("IdRegion");
                    txtDescripcion.Text = leer.Campo("Descripcion");
                    txtIdReg.Enabled = false;

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtDescripcion.Enabled = false;
                    }
                }
            }
        }
    }
}
