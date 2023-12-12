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
    public partial class FrmServiciosAreas : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        private bool bEscape = false;
        private bool bServicioCancelado = false;

        public FrmServiciosAreas()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

        }

        private void FrmServiciosAreas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);

            bServicioCancelado = false;
            lblCancelado.Visible = false;
            txtServicio.Focus();
        }

        #region Buscar Servicio

        private void txtServicio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtServicio.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Servicios(txtServicio.Text.Trim(), "txtServicio_Validating");
                if (myLeer.Leer())
                    CargaDatosServicio();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatosServicio()
        {
            //Se hace de esta manera para la ayuda.
            txtServicio.Text = myLeer.Campo("IdServicio");
            lblServicio.Text = myLeer.Campo("Descripcion");
            txtServicio.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                bServicioCancelado = true;
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }
        }

        #endregion Buscar Servicio

        #region Buscar Area

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string IdServicio = "";
            myLeer = new clsLeer(ref ConexionLocal);

            if (!bEscape)
            {
                if (txtId.Text.Trim() == "" || txtId.Text.Trim() == "*")
                {
                    txtId.Enabled = false;
                    txtId.Text = "*";
                }
                else
                {
                    IdServicio = txtServicio.Text; //Se obtiene el Numero de Familia del Combo. NOTA: El numero esta oculto.
                    myLeer.DataSetClase = Consultas.ServiciosAreas(IdServicio, txtId.Text.Trim(), "txtId_Validating");

                    if (myLeer.Leer())
                        CargaDatos();
                    else
                    {
                        txtId.Text = "";
                        txtDescripcion.Text = "";
                        txtId.Focus();
                    }
                }
            }
            bEscape = false;
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdArea");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }

        }

        #endregion Buscar Area

        #region Guardar/Actualizar Area

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdServicio = "" ;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if ( ! bServicioCancelado )
            {
                if (ValidaDatos())
                {
                    if (ConexionLocal.Abrir())
                    {
                        ConexionLocal.IniciarTransaccion();

                        sIdServicio = txtServicio.Text; //Se obtiene el numero de Familia el cual esta oculto.
                        sSql = String.Format("Exec spp_Mtto_CatServicios_Areas '{0}', '{1}', '{2}', {3} ",
                                sIdServicio, txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion);

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
            else
            {
                General.msjUser("El Id Servicio seleccionado esta cancelado");
            }

        }

        #endregion Guardar/Actualizar Area

        #region Eliminar Area

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdServicio = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Area seleccionada ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista la Familia.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sIdServicio = txtServicio.Text; //Se obtiene el numero de Familia el cual esta oculto.
                            sSql = String.Format("Exec spp_Mtto_CatServicios_Areas '{0}', '{1}', '', {2} ",
                                    sIdServicio, txtId.Text.Trim(), iOpcion);

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
                                General.msjError("Ocurrió un error al eliminar el Area.");
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
            }
            else
            {
                General.msjUser("Esta Area ya esta cancelada");
            }


        }

        #endregion Eliminar Area

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int i = 0;

            for (i = 0; i <= 1; i++)
            {
                if (txtServicio.Text == "")
                {
                    General.msjUser("Ingrese la Clave Servicio por favor");
                    txtServicio.Focus();
                    bRegresa = false;
                    break;
                }
                if (txtId.Text == "")
                {
                    General.msjUser("Ingrese la Clave Area por favor");
                    txtId.Focus();
                    bRegresa = false;
                    break;
                }

                if (txtDescripcion.Text == "")
                {
                    General.msjUser("Ingrese la Descripción por favor");
                    txtDescripcion.Focus();
                    bRegresa = false;
                    break;
                }
            }
            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string sIdServicio = "";

                sIdServicio = txtServicio.Text; //Se obtiene el numero de Familia el cual esta oculto.
                myLeer.DataSetClase = Ayuda.Servicios("txtId_KeyDown" );

                if (myLeer.Leer())
                {
                    CargaDatosServicio();
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                string sIdServicio = "";

                sIdServicio = txtServicio.Text; //Se obtiene el numero de Familia el cual esta oculto.
                myLeer.DataSetClase = Ayuda.ServiciosAreas("txtId_KeyDown", sIdServicio );

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                bEscape = true; //Se asigna true para que no entre en el TxtId_Validating
                if( txtId.Text == "*")
                    txtId.Text ="";

                txtId.Enabled = true;
                txtServicio.Focus();
            }

        }

        private void txtServicio_TextChanged(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            txtId.Text = "";
            txtDescripcion.Text = "";
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtId.Enabled = true;
                txtId.Focus();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            txtDescripcion.Text = "";
        }        

        #endregion Eventos
       

    } //Llaves de la clase
}
