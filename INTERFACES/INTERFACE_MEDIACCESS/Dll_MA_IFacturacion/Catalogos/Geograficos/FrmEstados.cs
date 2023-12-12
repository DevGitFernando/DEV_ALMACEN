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

namespace Dll_MA_IFacturacion.Catalogos
{
    public partial class FrmEstados : FrmBaseExt
    {
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmEstados()
        {
            InitializeComponent();
            //con.SetConnectionString();
            
            leer = new clsLeer(ref con);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.F6:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;

                case Keys.F7:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sDato="";
            if (txtId.Text.Trim() == "")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
            }
            else
            {
                sDato = string.Format("SELECT * FROM CatEstados (nolock) WHERE IdEstado= '{0}' ", Fg.PonCeros(txtId.Text, 2));

                if (!leer.Exec(sDato))
                {
                    Error.GrabarError(leer, "txtId_Validating");
                    General.msjError("Error al buscar edo");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtId.Text = leer.Campo("IdEstado");
                        txtDescripcion.Text = leer.Campo(2);
                        txtClave.Text = leer.Campo("ClaveRENAPO");
                        txtId.Enabled = false;

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                            txtDescripcion.Enabled = false;
                            txtClave.Enabled = false;
                        }
                    }
                    else
                    {
                        General.msjError("El Estado no Existe");
                        txtId.Text = "";
                        txtId.Focus();
                    }
                }
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            //string sMensaje = "";

            if (ValidaDatos())
            {
                
                if (con.Abrir())
                {
                    iOpcion = 1;
                    con.IniciarTransaccion();

                    string sSql = string.Format(" Exec spp_Mtto_CatEstados '{0}', '{1}', '{2}','{3}'", txtId.Text, txtDescripcion.Text, txtClave.Text, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        con.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {
                        //sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        con.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser("La Información se Grabo satisfactoriamente con el Id : " + leer.Campo(1));
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
            txtId.Focus();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if (txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese el nombre por favor");
                txtDescripcion.Focus();
            }

            if (bRegresa && txtClave.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la claveRENAPO por favor");
                txtClave.Focus();
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            string message = "¿ Desea eliminar el Estado seleccionado ?";

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

                            string sSql = string.Format(" Exec spp_Mtto_CatEstados '{0}', '{1}', '{2}','{3}'", txtId.Text, txtDescripcion.Text, txtClave.Text, iOpcion);
                            if (!leer.Exec(sSql))
                            {
                                con.DeshacerTransaccion();
                                General.msjError("Ocurrió un error al actualizar la información");
                            }
                            else
                            {
                                con.CompletarTransaccion();
                                leer.Leer();
                                General.msjUser("Información actualizada satisfactoriamente de Id Estado : " + leer.Campo(1));
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

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtId_KeyDown");

                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdEstado");
                    txtDescripcion.Text = leer.Campo(2);
                    txtClave.Text = leer.Campo("ClaveRENAPO");
                    txtId.Enabled = false;

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                        txtDescripcion.Enabled = false;
                        txtClave.Enabled = false;
                    }
                }
            }
        }
    }
}
