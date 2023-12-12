using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmCentrosDeSalud : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLeer2;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        
        public FrmCentrosDeSalud()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            myLeer2 = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);
        }

        private void FrmCentrosDeSalud_Load(object sender, EventArgs e)
        {
            LlenaEstados();
            LimpiarPantalla();
        }

        #region Llenado de Combos

        private void LlenaEstados()
        {
            myLeer = new clsLeer(ref ConexionLocal);

            cboEstados.Add("0", "<< Seleccione >>");
            myLeer.DataSetClase = Consultas.ComboEstados("LlenaEstados"); ;
            if (myLeer.Leer())
            {
                cboEstados.Add(myLeer.DataSetClase, true);
                cboEstados.SelectedIndex = 0;
            }
        }

        private void LlenaJurisdicciones()
        {
            myLeer = new clsLeer(ref ConexionLocal);

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");

            Consultas.MostrarMsjSiLeerVacio = false;
            myLeer.DataSetClase = Consultas.Jurisdicciones(cboEstados.Data, "LlenaJurisdicciones"); ;
            if (myLeer.Leer())
            {
                cboJurisdicciones.Add(myLeer.DataSetClase, true, "IdJurisdiccion", "Descripcion"); 
                cboJurisdicciones.SelectedIndex = 0;
            }

            Consultas.MostrarMsjSiLeerVacio = true;
        }

        #endregion Llenado de Combos

        #region Buscar Municipio

        private void txtMunicipio_Validating(object sender, CancelEventArgs e)
        {
            myLeer2 = new clsLeer(ref ConexionLocal);

            if ( txtMunicipio.Text.Trim() != "" )
            {
                myLeer2.DataSetClase = Consultas.Municipios(cboEstados.Data, txtMunicipio.Text.Trim(), "txtMunicipio_Validating");

                if (myLeer2.Leer())
                    CargaDatosMunicipio();
                else
                {
                    txtMunicipio.Text = "";
                    lblMunicipio.Text = "";
                    txtMunicipio.Focus();
                }
            }
            
        }

        private void CargaDatosMunicipio()
        {
            //Se hace de esta manera para la ayuda.
            txtMunicipio.Text = myLeer2.Campo("IdMunicipio");
            lblMunicipio.Text = myLeer2.Campo("Descripcion");
            //txtMunicipio.Enabled = false;
        }

        #endregion Buscar Municipio

        #region Buscar Centro

        private void txtCentro_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtCentro.Text.Trim() == "" || txtCentro.Text.Trim() == "*")
            {
                txtCentro.Enabled = false;
                txtCentro.Text = "*";
                IniciarToolBar(true, false, false); 
            }
            else
            {
                IniciarToolBar(true, true, false); 
                myLeer.DataSetClase = Consultas.CentrosDeSalud( cboEstados.Data, txtCentro.Text, "txtCentro_Validating");

                if (myLeer.Leer())
                    CargaDatosCentro();
                else
                {
                    txtCentro.Text = "";
                    txtDescripcion.Text = "";
                    txtCentro.Focus();
                }
            }            

        }

        private void CargaDatosCentro()
        {
            //Se hace de esta manera para la ayuda.
            cboEstados.Data = myLeer.Campo("IdEstado");
            cboJurisdicciones.Data = myLeer.Campo("IdJurisdiccion");
            txtMunicipio.Text = myLeer.Campo("IdMunicipio");
            txtMunicipio_Validating(this, null);
            txtCentro.Text = myLeer.Campo("IdCentro");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtCentro.Enabled = false;
            cboEstados.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(true, false, false); // Se bloquean solamente los botones Cancelar e Imprimir.

                lblCancelado.Visible = true;
                txtCentro.Enabled = false;
                txtDescripcion.Enabled = false;
            }
        }            


        #endregion Buscar Centro

        #region Botones

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            IniciarToolBar(false, false, false);

            lblCancelado.Text = "CANCELADO"; //Se pone a pie ya que el inicia controles lo limpia.
            lblCancelado.Visible = false;

            cboEstados.SelectedIndex = 0;
            cboEstados.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatCentrosDeSalud '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                            cboEstados.Data, txtCentro.Text.Trim(), txtMunicipio.Text, cboJurisdicciones.Data, txtDescripcion.Text.Trim(), iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

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
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }

            }   

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Centro de Salud seleccionado ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtCentro_Validating(txtCentro.Text, null);//Se manda llamar este evento para validar que exista el Centro de Salud.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatCentrosDeSalud '{0}', '{1}', '', '', '', '{2}' ",
                            cboEstados.Data, txtCentro.Text.Trim(), iOpcion);


                            if (myLeer.Exec(sSql))
                            {
                                if (myLeer.Leer())
                                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

                                ConexionLocal.CompletarTransaccion();
                                General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                btnNuevo_Click(null, null);
                            }
                            else
                            {
                                ConexionLocal.DeshacerTransaccion();
                                Error.GrabarError(myLeer, "btnCancelar_Click");
                                General.msjError("Ocurrió un error al eliminar el Centro de Salud.");
                            }

                            ConexionLocal.Cerrar();
                        }
                        else
                        {
                            General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                        }

                    }
                }
            }
            else
            {
                General.msjUser("Este Centro de Salud ya esta cancelado");
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un Estado válido, verifique.");
                cboEstados.Focus();                
            }

            if (bRegresa && cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una Jurisdicción válida, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && lblMunicipio.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Municipio, verifique.");
                txtMunicipio.Focus();
            }

            if (bRegresa && txtCentro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Centro inválida, verifique.");
                txtCentro.Focus();
            }

            if ( bRegresa && txtDescripcion.Text == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre del Centro, verifique.");
                txtDescripcion.Focus();
            }
           
            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaJurisdicciones();
            txtCentro.Text = "";
            txtMunicipio.Text = "";
            lblMunicipio.Text = "";
            txtDescripcion.Text = "";

            cboJurisdicciones.SelectedIndex = 0;
            txtCentro.Enabled = true;
            
        }

        private void txtMunicipio_TextChanged(object sender, EventArgs e)
        {
            lblMunicipio.Text = "";
        }


        private void txtMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer2.DataSetClase = Ayuda.Municipios("txtMunicipio_KeyDown", cboEstados.Data);

                if (myLeer2.Leer())
                {
                    CargaDatosMunicipio();
                }
            }
        }

        private void txtCentro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.CentrosDeSalud("txtId_KeyDown", cboEstados.Data );

                if (myLeer.Leer())
                {
                    CargaDatosCentro();
                }
            }

        }
   

        #endregion Eventos

        

    }
}
