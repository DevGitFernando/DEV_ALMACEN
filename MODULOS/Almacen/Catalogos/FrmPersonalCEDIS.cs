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
    public partial class FrmPersonalCEDIS : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmPersonalCEDIS()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            LlenarPuestos();
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
                myLeer.DataSetClase = Consultas.PersonalCEDIS(sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    LimpiarPantalla();
                }
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdPersonal");
            txtNombre.Text = myLeer.Campo("Personal");
            cboPuestos.Data = myLeer.Campo("IdPuesto");

            txtIdPersonal_Relacionado.Text = myLeer.Campo("IdPersonal_Relacionado");

            txtId.Enabled = false;
            btnHuellas.Enabled = true;

            IniciaToolBar(true, true, true);
            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtId.Enabled = false;
                txtNombre.Enabled = false;
                cboPuestos.Enabled = false;
                IniciaToolBar(true, true, false);
            }

        }
        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.PersonalCEDIS(sEmpresa, sEstado, sFarmacia, Puestos_CEDIS.Ninguno, "txtId_KeyDown");

                if (myLeer.Leer())
                {
                    CargaDatos();
                }
            }
        }
        #endregion Buscar Personal 

        #region Botones 
        private void FrmPersonalCEDIS_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            
            IniciaToolBar(true, false, false);
            btnHuellas.Enabled = false;
            txtNombre.Enabled = false;

            txtId.Focus();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string message = "¿ Desea eliminar el Personal seleccionado ?";

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
        private void LlenarPuestos()
        {
            myLeer = new clsLeer(ref ConexionLocal);

            cboPuestos.Clear();  
            cboPuestos.Add("0", "<< Seleccione >>");

            myLeer.DataSetClase = Consultas.PuestosCEDIS("", "LlenarPuestos()");
            if (myLeer.Leer())
            {
                cboPuestos.Add(myLeer.DataSetClase, true);
            }
            cboPuestos.SelectedIndex = 0;
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

                    sSql = String.Format("Exec spp_Mtto_CatPersonalCEDIS \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', \n" +
                        "\t@IdPersonal_Relacionado = '{4}', @Nombre = '{5}', @IdPuesto = '{6}', @iOpcion = '{7}' \n",
                        sEmpresa, sEstado, sFarmacia, txtId.Text.Trim(), txtIdPersonal_Relacionado.Text.Trim(), 
                        txtNombre.Text.Trim(), cboPuestos.Data, iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        LimpiarPantalla();
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
                General.msjUser("Operativo no valido. Favor de verificar.");
                txtId.Focus();
            }

            //if (bRegresa && txtNombre.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha capturado el nombre del personal, verifique.");
            //    txtNombre.Focus();
            //}

            if (bRegresa && cboPuestos.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione un puesto. Favor de verificar.");
                cboPuestos.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones

        private void btnHuellas_Click(object sender, EventArgs e)
        {
            FrmHuellasPersonal f = new FrmHuellasPersonal();
            string sReferencia_Huella = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + txtId.Text + cboPuestos.Data;
            f.RegistrarHuellas(sReferencia_Huella, txtId.Text, txtNombre.Text); 
        }

        #region Personal Relacionado 
        private void txtIdPersonal_Relacionado_TextChanged( object sender, EventArgs e )
        {
            //txtNombre.Text = "";
        }

        private void txtIdPersonal_Relacionado_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Personal("txtIdPersonal_Relacionado_KeyDown", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada); 
                if (myLeer.Leer())
                {
                    CargarDatosDePersonalRelacionado();
                }
            }
        }
        private void txtIdPersonal_Relacionado_Validating( object sender, CancelEventArgs e )
        {
            if(txtIdPersonal_Relacionado.Text.Trim() != "" )
            {
                myLeer.DataSetClase = Consultas.Personal(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtIdPersonal_Relacionado.Text, "txtIdPersonal_Relacionado_Validating");
                if(myLeer.Leer())
                {
                    CargarDatosDePersonalRelacionado();
                }
            }
        }

        private void CargarDatosDePersonalRelacionado()
        {
            txtIdPersonal_Relacionado.Text = myLeer.Campo("IdPersonal");
            txtNombre.Text = myLeer.Campo("NombreCompleto");
        }
        #endregion Personal Relacionado 
    }
}
