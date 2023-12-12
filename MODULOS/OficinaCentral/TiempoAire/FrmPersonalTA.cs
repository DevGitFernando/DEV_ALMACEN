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

namespace OficinaCentral.TiempoAire
{
    public partial class FrmPersonalTA : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmPersonalTA()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);
        }

        private void FrmPersonalTA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnImprimir.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            IniciarToolBar(false, false, false);
            txtId.Focus();
        }


        #region Buscar Personal

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtId.Text.Trim() == "")
            {
                IniciarToolBar(true, false, false);
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                IniciarToolBar(true, true, false);
                myLeer.DataSetClase = Consultas.PersonalTA( txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdPersonal");
            txtNombre.Text = myLeer.Campo("Nombre");            
            txtId.Enabled = false;

            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(true, false, false);
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtNombre.Enabled = false;
            }
 
        }

        #endregion Buscar Personal

        #region Guardar/Actualizar Personal

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            if (ValidaDatos())
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatPersonalTA '{0}', '{1}', '{2}' ",
                            txtId.Text.Trim(), txtNombre.Text.Trim(), iOpcion );

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

        #endregion Guardar/Actualizar Personal

        #region Eliminar Personal

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea eliminar el Personal seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatPersonalTA '{0}', '', '{1}' ",
                            txtId.Text.Trim(), iOpcion);

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
                        General.msjError("Ocurrió un error al eliminar el Personal.");
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

        #endregion Eliminar Personal

        #region Validaciones de Controles

        private bool ValidaDatos()
        {
            bool bRegresa = true;
                        
            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Personal inválida, verifque.");
                txtId.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "" )
            {
                bRegresa = false;
                General.msjUser("No capturado el Nombre, verifique.");
                txtNombre.Focus();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.PersonalTA("txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            } 
        }

        #endregion Eventos

    } //Llaves de la clase
}
