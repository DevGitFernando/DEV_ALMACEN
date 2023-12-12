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

namespace OficinaCentral.Catalogos.Devoluciones
{
    public partial class FrmMotivosDevoluciones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;       
        clsConsultas Consultas;

        public FrmMotivosDevoluciones()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);
        }

        private void FrmMotivosDevoluciones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(false, false);
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 0;
            bool bContinua = true;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    iOpcion = 1;
                    cnn.IniciarTransaccion();

                    bContinua = GuardaInformacion(iOpcion);
                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al grabar la información");
                    }
                    else
                    {                        
                        cnn.CompletarTransaccion();
                        leer.Leer();
                        General.msjUser("La Información se Grabo satisfactoriamente con el Id : " + leer.Campo(1));
                        btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
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
            string message = "¿ Desea Cancelar el Motivo seleccionado ?";
            bool bContinua = true;

            if (ValidaDatos())
            {
                if (lblCancelado.Visible == false)
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (cnn.Abrir())
                        {
                            iOpcion = 2;
                            cnn.IniciarTransaccion();

                            bContinua = GuardaInformacion(iOpcion);
                            if (!bContinua)
                            {
                                cnn.DeshacerTransaccion();
                                General.msjError("Ocurrió un error al actualizar la información");
                            }
                            else
                            {
                                cnn.CompletarTransaccion();
                                leer.Leer();
                                General.msjUser("Información actualizada satisfactoriamente de Id Motivo : " + leer.Campo(1));
                                btnNuevo_Click(null, null);
                            }

                            cnn.Cerrar();
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

        #region Eventos
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtId.Text.Trim() == "")
            {
                txtId.Text = "*";
                txtId.Enabled = false;
                IniciaToolBar(true, false);
            }
            else
            {
                leer.DataSetClase = Consultas.Motivos_Dev_Transferencia(Fg.PonCeros(txtId.Text, 2), "txtId_Validating");
                if (leer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    General.msjError("El Motivo no Existe");
                    txtId.Text = "";
                    txtId.Focus();
                }                
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Motivos_Dev_Transferencia("txtId_KeyDown");

                if (leer.Leer())
                {
                    CargaDatos();
                }
            }
        }
        #endregion Eventos

        #region Funciones
        private void IniciaToolBar(bool Guardar, bool Cancelar)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private void CargaDatos()
        {
            txtId.Text = leer.Campo("IdMotivo");
            txtDescripcion.Text = leer.Campo("Descripcion");
            txtId.Enabled = false;

            IniciaToolBar(true, true);

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtDescripcion.Enabled = false;
                IniciaToolBar(true, false);
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if (txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la descripción por favor");
                txtDescripcion.Focus();
            }            

            return bRegresa;
        }

        private bool GuardaInformacion(int iOpcion)
        {
            bool bRegresa = true;

            string sSql = string.Format(" Exec spp_Mtto_CatMotivos_Dev_Transferencia '{0}', '{1}', '{2}' ", txtId.Text, txtDescripcion.Text, iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;                
            }            

            return bRegresa;
        }
        #endregion Funciones
    }
}
