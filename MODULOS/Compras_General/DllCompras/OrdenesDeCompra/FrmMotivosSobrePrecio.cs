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

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmMotivosSobrePrecio : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmMotivosSobrePrecio()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
        }

        private void FrmMotivosSobrePrecio_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                IniciaToolBar(true, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.CatMotivos(txtId.Text.Trim(), "txtId_Validating()");

                if (myLeer.Leer())
                {
                    CargarDatos();
                }
                else
                {
                    btnNuevo_Click(this, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.CatMotivos("txtIdProveedor_KeyDown()");

                if (myLeer.Leer())
                {
                    CargarDatos();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false);
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        #region Funciones
        private void IniciaToolBar(bool Guardar, bool Cancelar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private void Guardar(int iOpcion)
        {
            string sSql = "", sMensaje = "";

            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_MotivosSobrePrecio '{0}', '{1}', {2}", txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "Guardar()");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de motivo inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No capturado la descripción, verifique.");
                txtDescripcion.Focus();
            }

            return bRegresa;
        }

        private void CargarDatos()
        {
            txtId.Text = myLeer.Campo("Folio");
            txtId.Enabled = false;
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            IniciaToolBar(true, true);

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtDescripcion.Enabled = false;
                IniciaToolBar(true, false);
            }
        }

        #endregion Funciones
    }
}
