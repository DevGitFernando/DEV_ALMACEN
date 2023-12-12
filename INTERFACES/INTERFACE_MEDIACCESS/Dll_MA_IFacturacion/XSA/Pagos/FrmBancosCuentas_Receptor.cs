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

using DllFarmaciaSoft;

namespace Dll_MA_IFacturacion.XSA
{
    public partial class FrmBancosCuentas_Receptor : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        //clsAyudas ayuda;
        clsAyudas_CFDI ayuda_MA;
        //clsConsultas consulta;
        clsConsultas_CFDI consulta_MA;

        public FrmBancosCuentas_Receptor()
        {
            InitializeComponent();
        }

        private void FrmBancosCuentas_Receptor_Load(object sender, EventArgs e)
        {
            leer = new clsLeer(ref cnn);
            //ayuda = new clsAyudas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            ayuda_MA = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            //consulta = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);
            consulta_MA = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            InicializaPantalla();
        }

        private void InicializaPantalla()
        {
            Fg.IniciaControles();

            lblCancelado.Text = "CANCELADO";
            lblCancelado.Visible = false;

            txtRFC_Banco.Text = "";
            lblBanco.Text = "";
            txtNumeroDeCuenta.Text = "";

        }

        private void txtRFC_Banco_TextChanged(object sender, EventArgs e)
        {
            txtNumeroDeCuenta.Text = "";
            lblBanco.Text = "";
        }

        private void txtRFC_Banco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda_MA.CFDI_Bancos("txtRFC_Banco_KeyDown()");
                if (leer.Leer())
                {
                    txtRFC_Banco.Text = leer.Campo("RFC");
                    lblBanco.Text = leer.Campo("NombreRazonSocial");
                }
            }
        }


        private void txtRFC_Banco_Validating(object sender, CancelEventArgs e)
        {
            if (txtRFC_Banco.Text != "")
            {
                leer.DataSetClase = consulta_MA.CFDI_Bancos(txtRFC_Banco.Text, "txtId_Validating");
                if (!leer.Leer())
                {
                    General.msjAviso("RFC no valido.");
                }
                else
                {
                    lblBanco.Text = leer.Campo("NombreRazonSocial");
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Guardar(0);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla();
        }


        private void Guardar(int iOpcion)
        {
            bool bContinua = false;

            string sSql = "";

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    sSql = string.Format("Exec spp_Mtto_CFDI_BancosCuentas__Receptor @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                                         " @RFC_Banco = '{3}',  @NumeroDeCuenta = '{4}', @iOpcion = {5}",
                                         DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                         txtRFC_Banco.Text, txtNumeroDeCuenta.Text, iOpcion);


                    bContinua = leer.Exec(sSql); // GuardarInformacion(1);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        //txtId.Text = sFolio;
                        cnn.CompletarTransaccion();
                        cnn.Cerrar();
                        this.Hide();
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }

            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (bRegresa && lblBanco.Text.Trim() == "")
            {
                General.msjUser("Ingrese un RFC de banco valido, por favor");
                txtRFC_Banco.Focus();
                bRegresa = false;
            }


            if (bRegresa && txtNumeroDeCuenta.Text.Trim() == "")
            {
                General.msjUser("Ingrese el número de cuenta, por favor");
                txtNumeroDeCuenta.Focus();
                bRegresa = false;
            }

            return bRegresa;
        }

        private void txtNumeroDeCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda_MA.CFDI_Pagos__ReceptorBancos(txtRFC_Banco.Text, "txtNumeroDeCuenta_KeyDown");
                if (leer.Leer())
                {
                    txtNumeroDeCuenta.Text = leer.Campo("NumeroDeCuenta");

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                    }
                }
            }
        }
    }
}
