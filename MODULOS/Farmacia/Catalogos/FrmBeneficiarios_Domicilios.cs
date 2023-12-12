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

namespace Farmacia.Catalogos
{
    public partial class FrmBeneficiarios_Domicilios : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdBeneficiario = ""; 

        public FrmBeneficiarios_Domicilios()
        {
            InitializeComponent();

            leer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            //////sIdCliente = "0002";
            //////sIdSubCliente = "0006";
            //////sIdBeneficiario = "00000004"; 
        }

        #region Form 
        private void FrmBeneficiarios_Domicilios_Load(object sender, EventArgs e)
        {
            CargarBeneficiarios();
        }
        #endregion Form

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            txtIdEstado.Text = "";
            txtDomicilio.Text = "";
            txtReferencia.Text = "";
            txtCodigoPostal.Text = "";
            txtTelefonos.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

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

                    sSql = String.Format("Exec spp_Mtto_Beneficiarios_Domicilios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', {12} ",
                            sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario,
                            txtIdEstado.Text.Trim(), txtIdMunicipio.Text.Trim(), txtIdColonia.Text.Trim(), txtCodigoPostal.Text.Trim(),
                      txtDomicilio.Text.Trim(), txtReferencia.Text.Trim(), txtTelefonos.Text.Trim(), iOpcion);

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                        }

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);

                    }

                    ConexionLocal.Cerrar();
                }
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones y Procedimientos Publicos 
        public void MostrarBeneficiarioDomicilio(string IdCliente, string IdSubCliente, string IdBeneficiario)
        {
            sIdCliente = IdCliente;
            sIdSubCliente = IdSubCliente;
            sIdBeneficiario = IdBeneficiario; 

            this.ShowDialog(); 
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // int i = 0;

            if (txtIdEstado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Estado inválida, verifique.");
                txtIdEstado.Focus();
            }

            if (bRegresa && txtIdMunicipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Municipio inválida, verifique.");
                txtIdMunicipio.Focus();
            }

            if (bRegresa && txtIdColonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Colonia inválida, verifique.");
                txtIdColonia.Focus();
            }

            if (bRegresa && txtDomicilio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No capturado el Domicilio, verifique.");
                txtDomicilio.Focus();
            }

            if (bRegresa && txtReferencia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Referencia, verifique.");
                txtReferencia.Focus();
            }

            if (bRegresa && txtCodigoPostal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Codigo Postal, verifque.");
                txtCodigoPostal.Focus();
            }

            if (bRegresa && txtTelefonos.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Telefono, verifque.");
                txtTelefonos.Focus();
            }

            return bRegresa;
        }

        private void CargarBeneficiarios()
        {
            leer.DataSetClase = Consultas.Beneficiarios_Domicilio(sEstado, sFarmacia, sIdCliente, sIdSubCliente, sIdBeneficiario, "CargarBeneficiarios()");
            if (leer.Leer())
            {
                lblNombre.Text = leer.Campo("IdBeneficiario") + " -- " + leer.Campo("NombreCompleto");
                txtIdEstado.Text = leer.Campo("IdEstado_D");
                lblEstado.Text = leer.Campo("Estado_D");
                txtIdMunicipio.Text = leer.Campo("IdMunicipio_D");
                lblMunicipio.Text = leer.Campo("Municipio_D");
                txtIdColonia.Text = leer.Campo("IdColonia_D");
                lblColonia.Text = leer.Campo("Colonia_D");
                txtCodigoPostal.Text = leer.Campo("CodigoPostal");
                txtDomicilio.Text = leer.Campo("Direccion");
                txtReferencia.Text = leer.Campo("Referencia");
                txtTelefonos.Text = leer.Campo("Telefonos");
            }
        }

        #endregion Funciones y Procedimientos Privados

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() == "")
            {
                txtIdEstado.Text = "";
                lblEstado.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text, "txtIdEstado_Validating");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
                else
                {
                    General.msjUser("Clave de Estado no encontrada, verifique.");
                    txtIdEstado.Text = "";
                    lblEstado.Text = "";
                    txtIdEstado.Focus();
                }
            }
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
            txtIdMunicipio.Text = "";
            txtIdColonia.Text = "";
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Estados("txtIdMedico_KeyDown");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
            }
        }

        private void txtIdMunicipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdMunicipio.Text.Trim() == "")
            {
                txtIdMunicipio.Text = "";
                lblMunicipio.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Municipios(txtIdEstado.Text, txtIdMunicipio.Text, "txtIdMunicipio_Validating");
                if (leer.Leer())
                {
                    txtIdMunicipio.Text = leer.Campo("IdMunicipio");
                    lblMunicipio.Text = leer.Campo("Descripcion");
                }
                else
                {
                    General.msjUser("Clave de Municipio no encontrada, verifique.");
                    txtIdMunicipio.Text = "";
                    lblMunicipio.Text = "";
                    txtIdMunicipio.Focus();
                }
            }
        }

        private void txtIdMunicipio_TextChanged(object sender, EventArgs e)
        {
            lblMunicipio.Text = "";
            txtIdColonia.Text = "";
        }

        private void txtIdMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Municipios("txtIdMedico_KeyDown", txtIdEstado.Text.Trim());
                if (leer.Leer())
                {
                    txtIdMunicipio.Text = leer.Campo("IdMunicipio");
                    lblMunicipio.Text = leer.Campo("Descripcion");
                }
            }
        }

        private void txtIdColonia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdColonia.Text.Trim() == "")
            {
                txtIdColonia.Text = "";
                lblColonia.Text = "";
            }
            else
            {
                leer.DataSetClase = Consultas.Colonias(txtIdEstado.Text, txtIdMunicipio.Text, txtIdColonia.Text.Trim(), "txtIdColonia_Validating");
                if (leer.Leer())
                {
                    txtIdColonia.Text = leer.Campo("IdColonia");
                    lblColonia.Text = leer.Campo("Descripcion");
                }
                else
                {
                    General.msjUser("Clave de Colonia no encontrada, verifique.");
                    txtIdColonia.Text = "";
                    lblColonia.Text = "";
                    txtIdColonia.Focus();
                }
            }
        }

        private void txtIdColonia_TextChanged(object sender, EventArgs e)
        {
            lblColonia.Text = "";
        }

        private void txtIdColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.Colonias("txtIdMedico_KeyDown", txtIdEstado.Text.Trim(), txtIdMunicipio.Text.Trim());
                if (leer.Leer())
                {
                    txtIdColonia.Text = leer.Campo("IdColonia");
                    lblColonia.Text = leer.Campo("Descripcion");
                }
            }
        }
    }
}
