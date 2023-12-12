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
using OficinaCentral;

namespace OficinaCentral.Catalogos
{
    public partial class FrmClasificacionesSSA : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmClasificacionesSSA()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
        }

        private void FrmClasificacionesSSA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtId.Focus();
        }


        #region Buscar Clasificacion

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.ClasificacionesSSA(txtId.Text.Trim(), "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click( null, null );
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdClasificacion");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }
 
        }

        #endregion Buscar Clasificacion

        #region Guardar/Actualizar Clasificacion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatClasificacionesSSA '{0}', '{1}', {2} ",
                            txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion );

                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

                    if (myLeer.Exec(sSql))
                    {
                        if( myLeer.Leer() )
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

        #endregion Guardar/Actualizar Clasificacion

        #region Eliminar Clasificacion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la Clasificacion SSA seleccionada ?";

            //Se verifica que no este cancelada.
            if (lblCancelado.Visible == false)
            {
                txtId_Validating(txtId.Text, null);//Se manda llamar este evento para validar que exista la Clasificacion.
                if (txtDescripcion.Text.Trim() != "") //Si no esta vacio, significa que si existe.
                {
                    if (General.msjCancelar(message) == DialogResult.Yes)
                    {
                        if (ConexionLocal.Abrir())
                        {
                            ConexionLocal.IniciarTransaccion();

                            sSql = String.Format("Exec spp_Mtto_CatClasificacionesSSA '{0}', '', {1} ",
                                    txtId.Text.Trim(), iOpcion);

                            sCadena = sSql.Replace("'", "\"");
                            auditoria.GuardarAud_MovtosUni("*", sCadena);

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
                                General.msjError("Ocurrió un error al eliminar la Clasificacion SSA.");
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
                General.msjUser("Esta Clasificacion SSA ya esta cancelada");
            }


        }

        #endregion Eliminar Clasificacion

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int i = 0;

            for (i = 0; i <= 1; i++)
            {
                if (txtId.Text == "")
                {
                    General.msjUser("Ingrese la Clave Clasificacion por favor");
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

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.ClasificacionesSSA("txtId_KeyDown");

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

    } //Llaves de la clase
}
