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
    public partial class FrmSubFamilias : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        private bool bEscape = false;

        //Para Auditoria
        clsAuditoria auditoria;

        public FrmSubFamilias()
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

        private void FrmSubFamilias_Load(object sender, EventArgs e)
        {
            LlenaFamilias();
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            //txtId.Focus();
            cboFamilias.Focus();
        }

        #region Llena Familias

        private bool LlenaFamilias()
        {
            bool bRegresa = false;
            myLeer = new clsLeer(ref ConexionLocal);

            myLeer.DataSetClase = Consultas.ComboFamilias("LlenaFamilias");
            if (myLeer.Leer())
            {
                bRegresa = true;                
                LlenaComboFamilias();
            }
            //else
            //    this.Close();

            return bRegresa;

        }

        private void LlenaComboFamilias()
        {
            //Se hace de esta manera para la ayuda.
            cboFamilias.Add("0", "<< Seleccione >>");
            cboFamilias.Add(myLeer.DataSetClase, true);
            cboFamilias.SelectedIndex = 0;

            //cboFamilias.Add(myLeer.Campo("IdFamilia"), myLeer.Campo("Descripcion"));
        }

        #endregion Llena Familias

        #region Buscar SubFamilia

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            string IdFamilia = "", sCadena = "";
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
                    IdFamilia = cboFamilias.Data; //Se obtiene el Numero de Familia del Combo. NOTA: El numero esta oculto.
                    myLeer.DataSetClase = Consultas.SubFamilias(IdFamilia, txtId.Text.Trim(), "txtId_Validating");

                    sCadena = Consultas.QueryEjecutado.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosUni("*", sCadena);

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
            txtId.Text = myLeer.Campo("IdSubFamilia");
            txtDescripcion.Text = myLeer.Campo("Descripcion");
            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
            }

        }

        #endregion Buscar SubFamilia

        #region Guardar/Actualizar SubFamilia

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdFamilia = "", sCadena = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sIdFamilia = cboFamilias.Data; //Se obtiene el numero de Familia el cual esta oculto.
                    sSql = String.Format("Exec spp_Mtto_CatSubFamilias '{0}', '{1}', '{2}', {3} ",
                            sIdFamilia, txtId.Text.Trim(), txtDescripcion.Text.Trim(), iOpcion);

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

        #endregion Guardar/Actualizar SubFamilia

        #region Eliminar SubFamilia

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdFamilia = "", sCadena = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar la SubFamilia seleccionada ?";

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

                            sIdFamilia = cboFamilias.Data; //Se obtiene el numero de Familia el cual esta oculto.
                            sSql = String.Format("Exec spp_Mtto_CatSubFamilias '{0}', '{1}', '', {2} ",
                                    sIdFamilia, txtId.Text.Trim(), iOpcion);

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
                                General.msjError("Ocurrió un error al eliminar la SubFamilia.");
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
                General.msjUser("Esta SubFamilia ya esta cancelada");
            }


        }

        #endregion Eliminar SubFamilia

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            int i = 0;

            for (i = 0; i <= 1; i++)
            {
                if (cboFamilias.Data == "0")
                {
                    General.msjUser("Seleccione una Familia por favor");
                    txtId.Focus();
                    bRegresa = false;
                    break;
                }
                if (txtId.Text == "")
                {
                    General.msjUser("Ingrese la Clave SubFamilia por favor");
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

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                string sIdFamilia = "";

                sIdFamilia = cboFamilias.Data; //Se obtiene el numero de Familia el cual esta oculto.
                myLeer.DataSetClase = Ayuda.SubFamilias("txtId_KeyDown", sIdFamilia );

                sCadena = Ayuda.QueryEjecutado.Replace("'", "\"");
                auditoria.GuardarAud_MovtosUni("*", sCadena);

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
                cboFamilias.Focus();
            }

        }

        private void cboFamilias_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboFamilias_Validating(object sender, CancelEventArgs e)
        {
            txtId.Enabled = true;
            txtId.Focus();
        }

        #endregion Eventos

    } //Llaves de la clase
}
