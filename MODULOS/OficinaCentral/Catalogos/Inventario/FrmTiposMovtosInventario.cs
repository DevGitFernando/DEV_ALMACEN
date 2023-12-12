using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;

namespace OficinaCentral.Catalogos
{
    public partial class FrmTiposMovtosInventario : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
            
        public FrmTiposMovtosInventario()
        {
            InitializeComponent();
            Leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Consultas.MostrarMsjSiLeerVacio = true;
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.Modulo, GnOficinaCentral.Version, this.Name);

            cboEfecto.Add("0", "<<Seleccione>>");
            cboEfecto.Add("E", "Entrada");
            cboEfecto.Add("S", "Salida");
        }

        private void FrmTiposMovtosInventario_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar Movimiento
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                Leer.DataSetClase = Consultas.MovtosTiposInventario( txtId.Text.Trim(), "txtId_Validating");

                if (Leer.Leer())
                {
                    CargarDatos();
                }
                else
                {
                    txtId.Enabled = false;
                    InicializaBotones(true, true, false, false);
                }
            }
        }
        private void CargarDatos()
        {
            //Se hace de esta manera por la ayuda.            
            txtId.Text = Leer.Campo("TipoMovto");
            txtDescripcion.Text = Leer.Campo("DescMovimiento");
            cboEfecto.Data = Leer.Campo("Efecto_Movto");
            chkEsMovtoGeneral.Checked = Leer.CampoBool("EsMovtoGral");
            chkPermiteCaducados.Checked = Leer.CampoBool("PermiteCaducados");
            txtId.Enabled = false;
            InicializaBotones(true, true, true, false);

            //Se bloquean los siguientes 2 ya que solo se puede modificar los checkbox.
            txtDescripcion.Enabled = false;
            cboEfecto.Enabled = false;

            if (Leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtDescripcion.Enabled = false;
                cboEfecto.Enabled = false;
                chkEsMovtoGeneral.Enabled = false;
                chkPermiteCaducados.Enabled = false;
                InicializaBotones(true, true, false, false);
            }

        }
        #endregion Buscar Movimiento

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            InicializaBotones(true, false, false, false);
            txtId.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "", sIdTipoMovto_Inv_ContraMovto = "", sEfecto_ContraMovto = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iEsMovtoGral = 0, iPermiteCaducados = 0;

            if(chkEsMovtoGeneral.Checked)
                iEsMovtoGral = 1;
            
            if(chkPermiteCaducados.Checked)
                iPermiteCaducados = 1;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_Movtos_Inv_Tipos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ", 
                    txtId.Text.Trim(), txtDescripcion.Text.Trim(), cboEfecto.Data, iEsMovtoGral, sIdTipoMovto_Inv_ContraMovto, 
                    sEfecto_ContraMovto, iPermiteCaducados, iOpcion);

                    if (Leer.Exec(sSql))
                    {
                        if(Leer.Leer())
                            sMensaje = String.Format("{0}", Leer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(Leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    cnn.Cerrar();
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
            string message = "¿ Desea eliminar el Movimiento seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_Movtos_Inv_Tipos '{0}', '', '', '', '', '', '', '{1}' ",
                    txtId.Text.Trim(), iOpcion);

                    if (Leer.Exec(sSql))
                    {
                        if (Leer.Leer())
                            sMensaje = String.Format("{0}", Leer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(Leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
            }

        }
        #endregion Botones

        #region Funciones

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Clave Movto");
                txtId.Focus();                
            }

            if (bRegresa && txtDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese la Descripción");
                txtDescripcion.Focus();
            }

            if (bRegresa && cboEfecto.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione el Efecto Movto");
                cboEfecto.Focus();
            }

            return bRegresa;
        }

        private void InicializaBotones(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones
    }
}
