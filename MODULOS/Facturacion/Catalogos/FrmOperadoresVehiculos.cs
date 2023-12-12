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
using SC_SolutionsSystem.FuncionesGenerales; 

using DllFarmaciaSoft;
using Dll_IFacturacion;

namespace Facturacion.Catalogos
{
    public partial class FrmOperadoresVehiculos : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerColonia;
        clsConsultas Consultas;
        clsAyudas ayuda; 


        DataSet dtsFarmacias;
        string sIdEstado = "";
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sModulo = DtGeneral.ArbolModulo;
        string sIdPublicoGral = GnFarmacia.PublicoGral;

        bool bCPValido = false;

        public FrmOperadoresVehiculos()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerColonia = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtIFacturacion.DatosApp, this.Name);

            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

        }

        private void FrmOperadoresVehiculos_Load(object sender, EventArgs e)
        {
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            cboColonia.Clear();
            //HabilitarControles(false);
            txtOperador.Focus();
            
        }

        //private void HabilitarControles(bool Habilitar)
        //{
        //    //txtCP.Enabled = Habilitar;
        //    txtCliente.Enabled = Habilitar;
        //    txtSubCliente.Enabled = Habilitar;
        //    txtBeneficiario.Enabled = Habilitar;
        //    txtPais.Enabled = Habilitar;
        //    txtClaveEstado.Enabled = Habilitar;
        //    txtClaveMun.Enabled = Habilitar;
        //    txtClaveLocalidad.Enabled = Habilitar;

        //    cboColonia.Enabled = Habilitar;
        //    txtDom.Enabled = Habilitar;
        //    txtNumExterno.Enabled = Habilitar;
        //    txtNumInterno.Enabled = Habilitar;
        //    txtReferencia.Enabled = Habilitar;
        //}

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if(ValidarDatos())
            {
                Guardar();
            }
        }


        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtOperador.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de operador inválido, verifique.");
                txtOperador.Focus();
            }

            if (bRegresa && !bCPValido)
            {
                bRegresa = false;
                General.msjUser("Código postal inválido, verifique.");
                txtCP.Focus();
            }

            if (bRegresa && cboColonia.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado la colonia, verifique.");
                cboColonia.Focus();
            }


            if (bRegresa && txtDom.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Domicilio inválido, verifique.");
                txtDom.Focus();
            }

            if (bRegresa && txtReferencia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferencia.Focus();
            }

            return bRegresa;

        }

        private void Guardar()
        {
            string sSql = "", sMensaje = "";

            if (!cnn.Abrir())
            {
                General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            }
            else
            {
                cnn.IniciarTransaccion();

                sSql = String.Format("EXEC spp_Mtto_FACT_CFDI_OperadoresVehiculos \n" + // '{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                        " \t@IdPersonal = '{0}', @Nombre = '{1}', @ApPaterno =  '{2}', @ApMaterno = '{3}', @FechaDeNacimiento = '{4}', \n" +
                        " \t@RFC = '{5}', @CURP = '{6}', @NumeroDeLicencia = '{7}', @Email = '{8}', \n" +
                        " \t@ClavePais = '{9}', @ClaveEstado = '{10}', @ClaveMunicipio = '{11}', \n" +
                        " \t@ClaveColonia = '{12}', @ClaveLocalidad = '{13}', @ClaveCodigoPostal = '{14}', @Direccion = '{15}', @NumeroExterior = '{16}', @NumeroInterior = '{17}', @Referencia = '{18}' \n",
                        txtOperador.Text, txtNombre.Text.Trim(), txtPaterno.Text.Trim(), txtMaterno.Text.Trim(), General.FechaYMD(dtpFechaNacimiento.Value),
                        txtRFC.Text, txtCurp.Text, txtLicencia.Text, txtEMail.Text,
                        txtPais.Text, txtClaveEstado.Text, txtClaveMun.Text,
                        cboColonia.Data, txtClaveLocalidad.Text, txtCP.Text, txtDom.Text, txtNumExterno.Text, txtNumInterno.Text, txtReferencia.Text
                        );

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        sMensaje = leer.Campo("Mensaje");
                    }

                    cnn.CompletarTransaccion();
                    General.msjUser(sMensaje);
                    btnNuevo_Click(null, null);
                }
                else
                {
                    cnn.DeshacerTransaccion();
                    General.Error.GrabarError(leer.Error, cnn.DatosConexion, "Facturacion", "1", this.Name, "", leer.QueryEjecutado);
                    General.msjError("Ocurrió un error al guardar la información.");

                }

                cnn.Cerrar();
            }
        }

        #endregion Botones


        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lst.Limpiar();
            //if (cboEstados.SelectedIndex != 0)
            //{
            //    CargarConceptos(); 
            //}
        }


        private void BuscarDireccion()
        {
            string Colonia = "";
            string sSql = string.Format("Select * From vw_FACT_CFDI_OperadoresVehiculos Where IdPersonal = '{0}' ",
                                    Fg.PonCeros(txtOperador.Text, 8));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarDireccion()");
                General.msjError("Ocurrió un error al cargar la dirección.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtOperador.Text = leer.Campo("IdPersonal");
                    txtOperador.Enabled = false;
                    txtNombre.Text = leer.Campo("Nombre");
                    txtPaterno.Text = leer.Campo("ApPaterno");
                    txtMaterno.Text = leer.Campo("ApMaterno");
                    dtpFechaNacimiento.Value = leer.CampoFecha("FechaNacimiento");


                    txtRFC.Text = leer.Campo("RFC");
                    txtCurp.Text = leer.Campo("CURP");
                    txtLicencia.Text = leer.Campo("NumeroDeLicencia");
                    txtEMail.Text = leer.Campo("EMAIL");

                    bCPValido = true;
                    txtCP.Text = leer.Campo("ClaveCodigoPostal");
                    txtPais.Text = leer.Campo("ClavePais");
                    lblPais.Text = leer.Campo("Pais");
                    txtClaveEstado.Text = leer.Campo("ClaveEstado");
                    lblClaveEstado.Text = leer.Campo("Estado");
                    txtClaveMun.Text = leer.Campo("ClaveMunicipio");
                    lblClaveMun.Text = leer.Campo("Municipio");
                    txtClaveLocalidad.Text = leer.Campo("ClaveLocalidad");
                    lblClaveLocalidad.Text = leer.Campo("Localidad");

                    CargarColonias();

                    cboColonia.Focus();
                    cboColonia.Data = Colonia = leer.Campo("ClaveColonia");
                    txtDom.Text = leer.Campo("Direccion");
                    txtNumExterno.Text = leer.Campo("NumeroExterior");
                    txtNumInterno.Text = leer.Campo("NumeroInterior");
                    txtReferencia.Text = leer.Campo("Referencia");
                }
                else
                {
                    General.msjUser("Operador no encontrada, verifique.");
                    txtOperador.Text = "";
                }
            }
        }

        private void BuscarCodigoPostal()
        {
            string sSql = string.Format("Select * From vw_FACT_CFDI_SAT_Geograficos_06_CodigosPostales Where ClaveCodigoPostal = '{0}' ", txtCP.Text);

            bCPValido = false;

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConceptos()");
                General.msjError("Ocurrió un error al cargar el codigo postal.");
            }
            else
            {
                if(leer.Leer())
                {
                    bCPValido = true;

                    txtPais.Text = leer.Campo("IdPais");
                    lblPais.Text = leer.Campo("Pais");
                    txtClaveEstado.Text = leer.Campo("ClaveEstado");
                    lblClaveEstado.Text = leer.Campo("Estado");
                    txtClaveMun.Text = leer.Campo("ClaveMunicipio");
                    lblClaveMun.Text = leer.Campo("Municipio");
                    txtClaveLocalidad.Text = leer.Campo("ClaveLocalidad");
                    lblClaveLocalidad.Text = leer.Campo("Localidad");

                    CargarColonias();
                }
            }
        }

        private void CargarColonias()
        {
            string sSql = string.Format("Select * From vw_FACT_CFDI_SAT_Geograficos_05_Colonias Where CodigoPostal = '{0}' ", txtCP.Text);

            if (!leerColonia.Exec(sSql))
            {
                Error.GrabarError(leerColonia, "CargarConceptos()");
                General.msjError("Ocurrió un error al cargar la lista de colonias.");
            }
            else
            {
                if (leerColonia.Leer())
                {
                    //HabilitarControles(false);
                    cboColonia.Clear(); 
                    cboColonia.Add();
                    cboColonia.Add(leerColonia.DataSetClase, true, "IdColonia", "Colonia");
                    cboColonia.SelectedIndex = 0;
                    cboColonia.Focus();
                }
            }
        }

        private void txtOperador_Validating(object sender, CancelEventArgs e)
        {

            if (txtOperador.Text.Trim() == "" || txtOperador.Text.Trim() == "*")
            {
                txtOperador.Text = "*";
                txtOperador.Enabled = false;
            }
            else
            {
                BuscarDireccion();
            }
        }

        private void txtCP_Validating(object sender, CancelEventArgs e)
        {
            if(txtCP.Text.Trim() != "")
            {
                BuscarCodigoPostal();
            }
        }
    }
}
