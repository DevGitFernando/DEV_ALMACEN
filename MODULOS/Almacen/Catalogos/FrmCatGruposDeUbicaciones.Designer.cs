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
    public partial class FrmCatGruposDeUbicaciones : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        //clsAyudas Ayuda;
        //clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmCatGruposDeUbicaciones()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            //Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            //Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
        }

        #region Buscar
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            string sSql = "";

            if (txtId.Text.Trim() == "")
            {
                IniciaToolBar(true, true, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {

                sSql = string.Format("Select * From CFGC_ALMN__GruposDeUbicaciones (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdGrupo = '{3}'",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtId.Text.Trim(), 3));

                //myLeer.DataSetClase = Consultas.Rotacion(sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), "txtId_Validating");

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtId_Validating");
                    General.msjError("Ocurrio un error al obtener la información.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        CargaDatos();
                    }
                    else
                    {
                        General.msjAviso("No se encontró información.");
                        btnNuevo_Click(null, null);
                    }
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdGrupo");
            txtNombre.Text = myLeer.Campo("NombreGrupo");
            
            txtId.Enabled = false;

            IniciaToolBar(true, true, true);
            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtNombre.Enabled = false;
                IniciaToolBar(true, true, false);
            }

        }
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    myLeer.DataSetClase = Ayuda.Rotacion(sEmpresa, sEstado, sFarmacia, "txtId_KeyDown");

            //    if (myLeer.Leer())
            //    {
            //        CargaDatos();
            //    }
            //}
        }
        #endregion Buscar 

        #region Botones 
        private void FrmCatRotacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            GuardarInformacion(1, true);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciaToolBar(true, false, false);
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string message = "¿ Desea cancelar el tipo de Grupo seleccionado ?";

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


        private void GuardarInformacion(int iOpcion)
        {
            GuardarInformacion(iOpcion, false);
        }

        private void GuardarInformacion(int iOpcion, bool bEsGeneral)
        { 
            string sSql = "", sMensaje = "";

            if (bEsGeneral)
            {
                txtId.Text = "000";
                txtNombre.Text = "General";
            }

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

                    sSql = String.Format("Exec spp_Mtto_CFGC_ALMN__GruposDeUbicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                            sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), txtNombre.Text.Trim(), iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        if (!bEsGeneral)
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
                General.msjUser("Clave de Grupo inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el nombre del grupo, verifique.");
                txtNombre.Focus();
            }

            //if (bRegresa && (txtOrden.Text == "" || txtOrden.Text == "0"))
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha capturado el orden de rotación, verifique.");
            //    txtOrden.Focus();
            //}

            return bRegresa;
        }
        #endregion Funciones
    }
}
