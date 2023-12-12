﻿using System;
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
    public partial class FrmUnidadesDeMedida : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas_CFDI Ayuda = new clsAyudas_CFDI();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas_CFDI Consultas;

        public FrmUnidadesDeMedida()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
        }

        private void FrmUnidadesDeMedida_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;

            IniciaToolBar(true, false, false, false);
            txtId.Focus();
        }


        #region Buscar Familia 
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "") 
            {
                txtId.Enabled = false;
                txtId.Text = "*";
                IniciaToolBar(true, true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.UnidadesDeMedida(Fg.PonCeros(txtId.Text.Trim(), 4), "txtId_Validating");
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
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.UnidadesDeMedida("txtId_KeyDown");
                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdUnidadDeMedida");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtId.Enabled = false;

            IniciaToolBar(true, true, true, false);
            if (myLeer.Campo("Status") == "C")
            {
                IniciaToolBar(true, true, false, false);
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }
 
        }

        #endregion Buscar Familia

        #region Guardar/Actualizar Familia

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir())
                {
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CFDI_UnidadesDeMedida '{0}', '{1}', {2} ",
                            txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion );

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
                        General.msjError("Ocurrio un error al guardar la informacion.");
                        //btnNuevo_Click(null, null);
                        
                    }

                    ConexionLocal.Cerrar();
                }  
            } 
        }

        #endregion Guardar/Actualizar Familia

        #region Eliminar Familia

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Unidad de Medida seleccionada ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista la Familia.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (!ConexionLocal.Abrir())
                        {
                            General.msjErrorAlAbrirConexion();
                        }
                        else 
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatUnidadesDeMedida '{0}', '', {1} ",
                                    txtId.Text.Trim(), iOpcion); 

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
                                Error.GrabarError(myLeer, "btnCancelar_Click");
                                General.msjError("Ocurrio un error al eliminar el Banco.");
                                //btnNuevo_Click(null, null);
                            }

                            ConexionLocal.Cerrar();
                        }
                    }
                }
            }
        }

        #endregion Eliminar Familia

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtId.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Clave Banco por favor");
                txtId.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                General.msjUser("No ha capturado una Descripción válida, verifique.");
                txtDescripcion.Focus();
                bRegresa = false;
            }
            
            return bRegresa;
        }

        #endregion Validaciones de Controles

        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }


    } //Llaves de la clase
}
