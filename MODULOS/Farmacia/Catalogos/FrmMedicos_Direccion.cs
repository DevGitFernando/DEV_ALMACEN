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


namespace Farmacia.Catalogos
{
    public partial class FrmMedicos_Direccion : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmMedicos_Direccion()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }

        private void FrmMedicos_Direccion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(2);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false);
            txtId.Focus();
        }


        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Médico inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtDireccion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la dirección, verifique.");
                txtDireccion.Focus();
            }

            return bRegresa;
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

                    sSql = String.Format("Exec spp_Mtto_CatMedicos_Direccion '{0}', '{1}', '{2}', '{3}', '{4}'",
                            DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), txtDireccion.Text.Trim(), iOpcion);

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
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Medicos(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            txtId.Text = myLeer.Campo("IdMedico");
            txtId.Enabled = false;
            lblMedico.Text = myLeer.Campo("NombreCompleto");
            txtDireccion.Text = myLeer.Campo("Direccion");

            IniciaToolBar(true, true, true);

            if (myLeer.Campo("StatusDireccion") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                IniciaToolBar(true, true, false);
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Medicos(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }
    }
}
