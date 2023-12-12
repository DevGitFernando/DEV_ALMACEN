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
    public partial class FrmBeneficiarios : FrmBaseExt 
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

        public FrmBeneficiarios()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerColonia = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtIFacturacion.DatosApp, this.Name);

            ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            Consultas = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            CargarEstados(); 
        }

        private void FrmBeneficiarios_Load(object sender, EventArgs e)
        {
        }

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();

            CargarEstados();
            cboColonia.Clear();
            //HabilitarControles(false);
            cboEdo.Focus();
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


            if (bRegresa && cboEdo.Data == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el Estado, verifique.");
                cboEdo.Focus();
            }

            if (bRegresa && txtIdFar.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de Farmacia inválido, verifique.");
                txtIdFar.Focus();
            }

            if (bRegresa && txtCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de cliente inválido, verifique.");
                txtCliente.Focus();
            }

            if (bRegresa && txtSubCliente.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de Sub-cliente inválido, verifique.");
                txtSubCliente.Focus();
            }

            if (bRegresa && txtBeneficiario.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Número de Beneficiario inválido, verifique.");
                txtBeneficiario.Focus();
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

                sSql = String.Format("EXEC spp_Mtto_FACT_CFDI_Beneficiarios \n" + // '{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                        " \t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdCliente = '{2}', @IdSubCliente = '{3}', @IdBeneficiario = '{4}', @ClavePais = '{5}', @ClaveEstado = '{6}', @ClaveMunicipio = '{7}', \n" +
                        " \t@ClaveColonia = '{8}', @ClaveLocalidad = '{9}', @ClaveCodigoPostal = '{10}', @Direccion = '{11}', @NumeroExterior = '{12}', @NumeroInterior = '{13}', @Referencia = '{14}' \n",
                        cboEdo.Data, txtIdFar.Text, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text, txtPais.Text, txtClaveEstado.Text, txtClaveMun.Text,
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
                    General.Error.GrabarError(leer.Error, cnn.DatosConexion, "OficinaCentral", "1", this.Name, "", leer.QueryEjecutado);
                    General.msjError("Ocurrió un error al guardar la información.");

                }

                cnn.Cerrar();
            }
        }

        #endregion Botones

        private void CargarEstados()
        {
            cboEdo.Clear();
            cboEdo.Add();
            cboEdo.Add(Consultas.EstadosConFarmacias("CargarEstados()"), true, "IdEstado", "NombreEstado"); 
            cboEdo.SelectedIndex = 0; 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lst.Limpiar();
            //if (cboEstados.SelectedIndex != 0)
            //{
            //    CargarConceptos(); 
            //}
        }

        private void txtIdFar_KeyDown(object sender, KeyEventArgs e)
        {
            string sEdo = "";

            if (e.KeyCode == Keys.F1)
            {
                sEdo = cboEdo.Data;
                leer.DataSetClase = ayuda.Farmacias("txtIdFar_KeyDown", sEdo);
                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private void txtIdFar_Validating(object sender, CancelEventArgs e)
        {

            if (txtIdFar.Text.Trim() != "" && txtIdFar.Text.Trim() != "*")
            {
                leer.DataSetClase = Consultas.Farmacias(cboEdo.Data, txtIdFar.Text, "txtIdFar_Validating");
                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
                else
                {
                    General.msjUser("Farmacia no encontrada, verifique.");
                    txtIdFar.Text = "";
                    lblNomFar.Text = "";
                    txtIdFar.Focus();
                }

            }
        }

        private void CargarDatosFarmacia()
        {
            txtIdFar.Enabled = false;
            cboEdo.Enabled = false;
            txtIdFar.Text = leer.Campo("IdFarmacia");
            lblNomFar.Text = leer.Campo("Farmacia");


            //txtMun.Text = leer.Campo("IdMunicipio");
            //lblMun.Text = leer.Campo("Municipio");
            //txtCol.Text = leer.Campo("IdColonia");
            //lblCol.Text = leer.Campo("Colonia");

            //txtDom.Text = leer.Campo("Domicilio");
            //txtCP.Text = leer.Campo("CodigoPostal");

            //BuscarCodigoPostal();
            //HabilitarControles(true);

            if (leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                txtIdFar.Enabled = false;
                General.msjUser("La Farmacia actualmente se encuentra cancelada.");
            }
        }


        private void BuscarDireccion()
        {
            string Colonia = "";
            string sSql = string.Format("Select * From vw_FACT_CFDI_Beneficiarios Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdCliente = '{2}' And IdSubCliente = '{3}' And IdBeneficiario = '{4}' ",
                                    cboEdo.Data, txtIdFar.Text, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "BuscarDireccion()");
                General.msjError("Ocurrió un error al cargar la Dirección.");
            }
            else
            {
                if (leer.Leer())
                {
                    bCPValido = true;
                    txtCP.Text = leer.Campo("ClaveCodigoPostal");
                    txtPais.Text = leer.Campo("IdPais");
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
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, cboEdo.Data, txtIdFar.Text, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                }
            }
        }

        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtCliente.Text.Trim() == "")
            {
                e.Cancel = true;
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, cboEdo.Data, txtIdFar.Text, txtCliente.Text, "txtCliente_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCliente.Text = leer.Campo("NombreCliente");
                }
            }
        }

        private void txtSubCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "")
                {
                    leer.DataSetClase = ayuda.Farmacia_Clientes(sIdPublicoGral, false, cboEdo.Data, txtIdFar.Text, txtCliente.Text, "txtSubCte_KeyDown_1");
                    if (leer.Leer())
                    {
                        txtSubCliente.Enabled = false;
                        txtSubCliente.Text = leer.Campo("IdSubCliente");
                        lblSubCliente.Text = leer.Campo("NombreSubCliente"); ;
                    }
                }
            }
        }

        private void txtSubCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCliente.Text.Trim() == "")
            {
                e.Cancel = true;
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, cboEdo.Data, txtIdFar.Text, txtCliente.Text, txtSubCliente.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubCliente.Enabled = false;
                    txtSubCliente.Text = leer.Campo("IdSubCliente");
                    lblSubCliente.Text = leer.Campo("NombreSubCliente");
                }
            }
        }

        private void txtBeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Beneficiarios(cboEdo.Data, txtIdFar.Text, txtCliente.Text, txtSubCliente.Text, "txtBeneficiario_KeyDown");

                if (leer.Leer())
                {
                    lblBeneficiario.Text = leer.Campo("Nombre");

                    BuscarDireccion();
                }
            }
        }

        private void txtBeneficiario_Validating(object sender, CancelEventArgs e)
        {

            if (txtBeneficiario.Text.Trim() == "")
            {
                txtBeneficiario.Enabled = false;
                txtBeneficiario.Text = "*";
            }
            else
            {
                leer.DataSetClase = Consultas.Beneficiarios(cboEdo.Data, txtIdFar.Text, txtCliente.Text, txtSubCliente.Text, txtBeneficiario.Text.Trim(), "txtBeneficiario_Validating");
                if (leer.Leer())
                {
                    lblBeneficiario.Text = leer.Campo("Nombre");
                    BuscarDireccion();
                }
                else
                {
                    txtBeneficiario.Focus();
                }
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
