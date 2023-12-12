using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

namespace Almacen.Catalogos
{
    public partial class FrmCFGCRutasDistribucion : FrmBaseExt
    {
        enum cols_Beneficiario
        {
            ninguno, Cliente, SubCliente, Beneficiario, Descripcion, Referencia
        }

        enum cols_Farmacia
        {
            ninguno, IdEstado, Estado, IdFarmacia, Farmacia
        }

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;

        clsGrid myGridBeneficiario;
        clsGrid myGridFarmacia;

        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "";
        string sFolio = "";

        string sIdPublicoGral = GnFarmacia.PublicoGral;

        public FrmCFGCRutasDistribucion()
        {
            InitializeComponent();

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGridBeneficiario = new clsGrid(ref grdBeneficiario, this);
            myGridFarmacia = new clsGrid(ref grdFarmacia, this);

            myGridBeneficiario.AjustarAnchoColumnasAutomatico = true;
            myGridFarmacia.AjustarAnchoColumnasAutomatico = true;

            CargarEstados();
        }

        private void FrmCFGCRutasDistribucion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(true, false, false);

            myGridBeneficiario.Limpiar(false);
            myGridFarmacia.Limpiar(false);

            dtpFechaRegistro.Enabled = false;
            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;

            txtIdRuta.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string message = "¿ Desea cancelar La ruta seleccionada?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                GuardarInformacion(2);
            }
        }

        private void GuardarInformacion(int iOpcion)
        {
            bool bRegresa = false;            

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

                    bRegresa = GuardarInformacionEnc(iOpcion);

                    if(bRegresa)
                    {
                        bRegresa = GuardarInformacionBeneficiario(iOpcion);
                    }

                    if (bRegresa)
                    {
                        bRegresa = GuardarInformacionFarmacias(iOpcion);
                    }

                    if (bRegresa)
                    { 
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "GuardarInformacion");
                        General.msjError("Error al guardar Información.");
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

            if (txtIdRuta.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ruta no valida. Favor de verificar.");
                txtIdRuta.Focus();
            }

            if (bRegresa && txtNombre.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capture nombre de ruta. Favor de verificar.");
                txtNombre.Focus();
            }

            return bRegresa;
        }

        private bool GuardarInformacionEnc(int iOpcion)
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = String.Format("Exec spp_Mtto_CFGC_ALMN__RutaDistribucion @IdEstado = '{0}', @IdFarmacia = '{1}', @IdRuta = '{2}', @Descripcion = '{3}', @iOpcion = '{4}' ",
                    sEstado, sFarmacia, txtIdRuta.Text.Trim(), txtNombre.Text.Trim(), iOpcion);

            bRegresa = myLeer.Exec(sSql);

            if (myLeer.Leer())
            {
                sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                sFolio = myLeer.Campo("Clave");
            }

            return bRegresa;
        }

        private bool GuardarInformacionBeneficiario(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdCliente, sIdSubCliente, sIdBeneficiario;

            for (int i = 1; myGridBeneficiario.Rows >= i && bRegresa; i++)
            {
                sIdCliente = myGridBeneficiario.GetValue(i, (int)cols_Beneficiario.Cliente);
                sIdSubCliente = myGridBeneficiario.GetValue(i, (int)cols_Beneficiario.SubCliente);
                sIdBeneficiario = myGridBeneficiario.GetValue(i, (int)cols_Beneficiario.Beneficiario);

                sSql = String.Format("Exec spp_Mtto_CFGC_ALMN__RutaDistribucion_Beneficiario \n" +
                    "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdRuta = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', @IdBeneficiario = '{5}', @IdPersonal = '{6}', @iOpcion = '{7}' ",
                    sEstado, sFarmacia, sFolio, sIdCliente, sIdSubCliente, sIdBeneficiario, DtGeneral.IdPersonal, iOpcion
                    );

                bRegresa = myLeer.Exec(sSql);

                if(!bRegresa)
                {
                    break; 
                }
            }

            return bRegresa;
        }

        private bool GuardarInformacionFarmacias(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";
            string sidestado, sIdFarmacia;

            for (int i = 1; myGridFarmacia.Rows >= i && bRegresa; i++)
            {
                sidestado = myGridFarmacia.GetValue(i, (int)cols_Farmacia.IdEstado);
                sIdFarmacia = myGridFarmacia.GetValue(i, (int)cols_Farmacia.IdFarmacia);

                sSql = String.Format("Exec spp_Mtto_CFGC_ALMN__RutaDistribucion_Transferencia \n" +
                    "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @IdRuta = '{2}', @IdEstadoEnvia = '{3}', @IdFarmaciaEnvia = '{4}', @IdPersonal = '{5}', @iOpcion = '{6}' ",
                    sEstado, sFarmacia, sFolio, sidestado, sIdFarmacia, DtGeneral.IdPersonal, iOpcion
                    );

                bRegresa = myLeer.Exec(sSql);

                if(!bRegresa)
                {
                    break;
                }

            }

            return bRegresa;
        }

        private void txtIdRuta_TextChanged(object sender, EventArgs e)
        {
            LimpiarEncabezado();
        }

        private void txtIdRuta_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtIdRuta.Text.Trim() == "")
            {
                IniciaToolBar(true, true, false);
                txtIdRuta.Enabled = false;
                txtIdRuta.Text = "*";
            }
            else
            {
                myLeer.DataSetClase = Consultas.Rutas(sEstado, sFarmacia, txtIdRuta.Text.Trim(), "txtIdRuta_Validating");
                if (myLeer.Leer())
                {
                    CargaDatos();
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void txtIdRuta_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
        }

        private void CargaDatos()
        {
            //Se hace de esta manera para la ayuda.
            txtIdRuta.Text = myLeer.Campo("IdRuta");
            txtNombre.Text = myLeer.Campo("Descripcion");
            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");

            txtIdRuta.Enabled = false;

            clsLeer leerTemp = new clsLeer();


            leerTemp.DataTableClase = myLeer.Tabla(2);
            if(leerTemp.Leer())
            {
                myGridBeneficiario.LlenarGrid(leerTemp.DataSetClase);
            }

            leerTemp.DataTableClase = myLeer.Tabla(3);
            if(leerTemp.Leer())
            {
                myGridFarmacia.LlenarGrid(leerTemp.DataSetClase);
            }

            IniciaToolBar(true, true, true);
            if (myLeer.Campo("Status") == "C")
            {
                txtIdRuta.Enabled = false;
                IniciaToolBar(true, false, false);

                General.msjAviso("Ruta cancelada.");
            }

        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            lblCliente.Text = "";
            txtSubCliente.Text = "";
            lblSubCliente.Text = "";
        }

        private void txtCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtCliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, "txtCliente_Validating");
                if (!myLeer.Leer())
                {
                    General.msjUser("Cliente no encontrado.");
                    e.Cancel = true;
                }
                else
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = myLeer.Campo("IdCliente");
                    txtCliente.Enabled = false;
                    lblCliente.Text = myLeer.Campo("NombreCliente");
                }
            }
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                myLeer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (myLeer.Leer())
                {
                    txtCliente.Enabled = false;
                    txtCliente.Text = myLeer.Campo("IdCliente");
                    txtCliente.Enabled = false;
                    lblCliente.Text = myLeer.Campo("NombreCliente");
                }
            }
        }

        private void txtSubCliente_TextChanged(object sender, EventArgs e)
        {
            lblSubCliente.Text = "";
        }

        private void txtSubCliente_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCliente.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, "txtCte_Validating");
                if (!myLeer.Leer())
                {
                    General.msjUser("Sub-Cliente no encontrado.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubCliente.Enabled = false;
                    txtSubCliente.Text = myLeer.Campo("IdSubCliente");
                    txtSubCliente.Enabled = false;
                    lblSubCliente.Text = myLeer.Campo("NombreSubCliente");
                }
            }
        }

        private void txtSubCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "")
                {
                    myLeer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, "txtSubCte_KeyDown_1");
                    if (myLeer.Leer())
                    {
                        txtSubCliente.Enabled = false;
                        txtSubCliente.Text = myLeer.Campo("IdSubCliente");
                        txtSubCliente.Enabled = false;
                        lblSubCliente.Text = myLeer.Campo("NombreSubCliente"); ;
                    }
                }
            }
        }

        private void txtIdBenificiario_TextChanged(object sender, EventArgs e)
        {
            lblNombre.Text = "";
            lblFolioReferencia.Text = "";
            lbl_DerechoHabiencia.Text = "";
        }

        private void txtIdBenificiario_Validating(object sender, CancelEventArgs e)
        {
            lblStatus.Visible = false;
            lblStatus.Text = "CANCELADO";


            if (txtIdBenificiario.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Beneficiarios(sEstado, sFarmacia, txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), txtIdBenificiario.Text, "txtIdBenificiario_Validating");
                if (myLeer.Leer())
                {
                    CargarDatosBeneficiario();
                }
                else
                {
                    General.msjUser("Beneficiario no encontrado.");
                    txtIdBenificiario.Text = "";
                    txtIdBenificiario.Focus();
                }
            }
        }

        private void txtIdBenificiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Beneficiarios(sEstado, sFarmacia, txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), "txtIdBenificiario_KeyDown()");
                if (myLeer.Leer())
                {
                    CargarDatosBeneficiario();
                }
            }
        }

        private void CargarDatosBeneficiario()
        {
            txtIdBenificiario.Text = myLeer.Campo("IdBeneficiario");
            lblNombre.Text = myLeer.Campo("NombreCompleto");

            lblFolioReferencia.Text = myLeer.Campo("FolioReferencia");
            lbl_DerechoHabiencia.Text = myLeer.Campo("DerechoHabiencia");

            btnAgregarBeneficiario.Enabled = true;
            btnAgregarBeneficiario.Focus();
        }

        private void btnBeneficiarioNuevo_Click(object sender, EventArgs e)
        {
            limpiarBeneficiario();
            txtCliente.Focus();
        }

        private void btnAgregarBeneficiario_Click(object sender, EventArgs e)
        {
            if (lblNombre.Text != "")
            {
                myGridBeneficiario.AddRow();
                myGridBeneficiario.SetValue(myGridBeneficiario.Rows, (int)cols_Beneficiario.Cliente, txtCliente.Text);
                myGridBeneficiario.SetValue(myGridBeneficiario.Rows, (int)cols_Beneficiario.SubCliente, txtSubCliente.Text);
                myGridBeneficiario.SetValue(myGridBeneficiario.Rows, (int)cols_Beneficiario.Beneficiario, txtIdBenificiario.Text);
                myGridBeneficiario.SetValue(myGridBeneficiario.Rows, (int)cols_Beneficiario.Descripcion, lblNombre.Text);
                myGridBeneficiario.SetValue(myGridBeneficiario.Rows, (int)cols_Beneficiario.Referencia, lblFolioReferencia.Text);

                limpiarBeneficiario();

                txtCliente.Focus();
            }
        }

        private void grdBeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                myGridBeneficiario.DeleteRow(myGridBeneficiario.ActiveRow);
            }
        }

        private void LimpiarEncabezado()
        {
            txtNombre.Text = "";
            lblCancelado.Visible = false;
        }

        private void limpiarBeneficiario()
        {
            txtCliente.Text = "";
            txtCliente.Enabled = true;
            lblCliente.Text = "";
            txtSubCliente.Text = "";
            txtSubCliente.Enabled = true;
            lblSubCliente.Text = "";

            txtIdBenificiario.Text = "";
            lblStatus.Visible = false;

            lblNombre.Text = "";
            lblFolioReferencia.Text = "";
            lbl_DerechoHabiencia.Text = "";

            btnAgregarBeneficiario.Enabled = false;
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(Consultas.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");
        }

        private void txtFarmaciaDestino_TextChanged(object sender, EventArgs e)
        {
            lblFarmaciaDestino.Text = "";
        }

        private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;

            if (txtFarmaciaDestino.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Farmacias(cboEstados.Data, txtFarmaciaDestino.Text, "txtFarmaciaDestino_Validating");

                if (myLeer.Leer())
                {
                    bExito = CargarDatosFarmacia();
                }

                if (!bExito)
                {
                    lblFarmaciaDestino.Text = "";
                    txtFarmaciaDestino.Text = "";
                    txtFarmaciaDestino.Focus();
                }
            }

        }

        private void txtFarmaciaDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                myLeer.DataSetClase = Ayuda.Farmacias("txtFarmaciaDestino_KeyDown", cboEstados.Data);


                if (myLeer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private bool CargarDatosFarmacia()
        {
            bool bRegresa = true;

            cboEstados.Enabled = false;
            txtFarmaciaDestino.Enabled = false;

            txtFarmaciaDestino.Text = myLeer.Campo("IdFarmacia");
            lblFarmaciaDestino.Text = myLeer.Campo("Farmacia");

            btnAgregarFarmacia.Focus();

            return bRegresa;
        }

        private void btnAgregarFarmacia_Click(object sender, EventArgs e)
        {
            if (lblFarmaciaDestino.Text != "")
            {
                myGridFarmacia.AddRow();
                myGridFarmacia.SetValue(myGridFarmacia.Rows, (int)cols_Farmacia.IdEstado, cboEstados.Data);
                myGridFarmacia.SetValue(myGridFarmacia.Rows, (int)cols_Farmacia.Estado, cboEstados.Text);
                myGridFarmacia.SetValue(myGridFarmacia.Rows, (int)cols_Farmacia.IdFarmacia, txtFarmaciaDestino.Text);
                myGridFarmacia.SetValue(myGridFarmacia.Rows, (int)cols_Farmacia.Farmacia, lblFarmaciaDestino.Text);

                cboEstados.Enabled = true;
                txtFarmaciaDestino.Text = "";
                txtFarmaciaDestino.Enabled = true;
                txtFarmaciaDestino.Focus();
            }
        }

        private void grdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                myGridFarmacia.DeleteRow(myGridFarmacia.ActiveRow);
            }
        }
    }
}
