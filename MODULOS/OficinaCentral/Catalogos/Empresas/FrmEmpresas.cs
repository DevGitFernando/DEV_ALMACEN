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
    public partial class FrmEmpresas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        public FrmEmpresas()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            myLeer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Ayuda = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnOficinaCentral.DatosApp, this.Name);
        }

        private void FrmLaboratorios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            txtId.Focus();
        }


        #region Buscar Empresa
        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() == "")
            {
                txtId.Enabled = false;
                txtId.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.Empresas(txtId.Text.Trim(), "txtId_Validating");
                if (myLeer.Leer())
                    CargaDatos();
                else
                    btnNuevo_Click(null, null);
            }
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtId.Text = myLeer.Campo("IdEmpresa");
            txtNombre.Text = myLeer.Campo("Nombre");
            txtNombreCorto.Text = myLeer.Campo("NombreCorto");
            txtRFC.Text = myLeer.Campo("RFC");
            chkEsConsignacion.Checked = myLeer.CampoBool("EsDeConsignacion");
            txtEdoCiudad.Text = myLeer.Campo("EdoCiudad");
            txtColonia.Text = myLeer.Campo("Colonia");
            txtDomicilio.Text = myLeer.Campo("Domicilio");
            txtCodigoPostal.Text = myLeer.Campo("CodigoPostal");
            txtId.Enabled = false;

            if (myLeer.Campo("Status").ToUpper() == "C")
            {
                lblCancelado.Visible = true;
                //txtId.Enabled = false;
                //txtNombre.Enabled = false;
                //txtNombreCorto.Enabled = false;
                General.msjUser("La empresa actualmente se encuentra cancelada.");
            }
        }
        #endregion Buscar Empresa

        #region Botones 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
                GrabarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarCancelacion())
                GrabarInformacion(2);
        }

        private void GrabarInformacion(int iOpcion)
        {
            string sSql = "", sMensaje = "";
            string sMsjErr = "Ocurrió un error al guardar la información.";
            int iEsConsignacion = 0;

            if (iOpcion != 1)
                sMsjErr = "Ocurrió un error al cancelar la información.";

            if (chkEsConsignacion.Checked)
                iEsConsignacion = 1;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = String.Format("Exec spp_Mtto_CatEmpresas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                            txtId.Text.Trim(), txtNombre.Text.Trim(), txtNombreCorto.Text.Trim(),
                            txtRFC.Text.Trim(), iEsConsignacion, txtEdoCiudad.Text, txtColonia.Text, txtCodigoPostal.Text, 
                            txtDomicilio.Text.Trim(), iOpcion); 

                    if (myLeer.Exec(sSql))
                    {
                        myLeer.Leer();
                        sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError(sMsjErr);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
            }             
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtId.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Empresa inválida, verifique.");
                txtId.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre de la Empresa, verifique.");
                txtNombre.Focus();
            }

            if (bRegresa && txtNombreCorto.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Nombre corto de la Empresa, verifique.");
                txtNombreCorto.Focus();
            }

            if (bRegresa && txtRFC.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el RFC, verifique.");
                txtRFC.Focus();
            }

            if (bRegresa && txtEdoCiudad.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Estado-Ciudad, verifique.");
                txtEdoCiudad.Focus();
            }

            if (bRegresa && txtColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Colonia, verifique.");
                txtColonia.Focus();
            }

            if (bRegresa && txtDomicilio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Domicilio, verifique.");
                txtDomicilio.Focus();
            }

            return bRegresa;
        }
        #endregion Botones

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Empresas("txtId_KeyDown");
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
