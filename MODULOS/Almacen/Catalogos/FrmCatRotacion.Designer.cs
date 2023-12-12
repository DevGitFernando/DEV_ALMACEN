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

namespace Almacen.Catalogos
{
    public partial class FrmCatRotacion : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmCatRotacion()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
        }

        #region Buscar Personal
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                IniciaToolBar(true, true, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.Rotacion(sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), "txtId_Validating");
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

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdRotacion");
            txtNombre.Text = myLeer.Campo("NombreRotacion");
            txtOrden.Text = myLeer.Campo("Orden");
            txtId.Enabled = false;

            IniciaToolBar(true, true, true);
            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtNombre.Enabled = false;
                txtOrden.Enabled = false;
                IniciaToolBar(true, true, false);
            }

        }
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Rotacion(sEmpresa, sEstado, sFarmacia, "txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }
        #endregion Buscar Personal 

        #region Botones 
        private void FrmCatRotacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false);
            txtOrden.Text = "";
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string message = "¿ Desea cancelar el tipo de rotación seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                GuardarInformacion(2);
            }
        }
        #endregion Botones 

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }


        private void GuardarInformacion(int iOpcion )
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

                    sSql = String.Format("Exec spp_Mtto_CFGC_ALMN__Rotacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                            sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), txtNombre.Text.Trim(), txtOrden.Text, iOpcion);

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
                        Error.GrabarError(myLeer, "GuardarInformacion");
                        General.msjError("Ocurrió un error al guardar la Información.");
                        //btnNuevo_Click(null, null);

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
                General.msjUser("Clave de Rotación inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el nombre de la Rotación, verifique.");
                txtNombre.Focus();
            }

            if (bRegresa && (txtOrden.Text == "" || txtOrden.Text == "0"))
            {
                bRegresa = false;
                General.msjUser("No ha capturado el orden de rotación, verifique.");
                txtOrden.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        private SC_ControlsCS.scTextBoxExt txtOrden;                
    }
}
