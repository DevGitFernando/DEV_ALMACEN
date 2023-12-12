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

namespace MA_Facturacion.CuentasPorPagar
{
    public partial class FrmBancos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmBancos()
        {
            InitializeComponent();

            cnn.SetConnectionString();

            myLeer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmBancos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            InicializarBotones(true, false);
            lblCancelado.Visible = false;
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text != "")
            {
                GrabarInformacion(1);
            }
            else
            {
                General.msjAviso("Debe capturar una descripción, verifique porfavor.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (ValidaDatos())
                GrabarInformacion(2);
        }

        private void InicializarBotones(bool Guadar, bool Cancelar)
        {
            btnGuardar.Enabled = Guadar;
            btnCancelar.Enabled = Cancelar;
        }
        #endregion Botones

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                txtId.Text = Fg.PonCeros(txtId.Text, 3);
                //string sSql = string.Format("Select * From CatBancos Where IdBanco = '{0}'", txtId.Text.Trim());
                //if (myLeer.Exec(sSql))
                //{
                myLeer.DataSetClase = Consultas.Bancos(txtId.Text, "txtId_Validating()");
                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Bancos("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void GrabarInformacion(int iOpcion)
        {
            string sMsjErr = "Ocurrió un error al guardar la información.";

            if (iOpcion != 1)
            {
                sMsjErr = "Ocurrió un error al cancelar la información.";
            }


            if (cnn.Abrir())
            {
                cnn.IniciarTransaccion();

                string sSql = String.Format("Exec spp_Mtto_CatBancos '{0}', '{1}', {2}",
                        txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion);

                if (myLeer.Exec(sSql))
                {
                    myLeer.Leer();
                    cnn.CompletarTransaccion();
                    General.msjUser(myLeer.Campo("Mensaje")); //Este mensaje lo genera el SP
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    Error.GrabarError(myLeer, "btnGuardar_Click");
                    General.msjError(sMsjErr);
                }

                cnn.Cerrar();
            }
            else
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
        }

        private void CargaDatos()
        {
            txtId.Text = myLeer.Campo("IdBanco");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            InicializarBotones(false, true);

            txtId.Enabled = false;
            if (myLeer.Campo("Status") == "C")
            {
                InicializarBotones(true, false);
                lblCancelado.Visible = true;
            }            
        }
   }
}
