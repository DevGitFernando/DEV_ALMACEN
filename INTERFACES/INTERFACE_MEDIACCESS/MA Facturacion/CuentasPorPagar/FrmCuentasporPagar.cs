using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;

namespace MA_Facturacion.CuentasPorPagar
{
    public partial class FrmCuentasporPagar : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;

        clsDatosCliente DatosCliente;

        string sFolio = "";
        string sMensaje = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;

        string sFormato = "#,###,###,##0.#0";

        public FrmCuentasporPagar()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "CuentasporPagar");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
        }

        private void FrmCuentasporPagar_Load(object sender, EventArgs e)
        {
            MetodosPagos();
            btnNuevo_Click(null, null);
        }

        #region CargaCombos
        private void MetodosPagos()
        {
            string sSql = "";
            cboMetodoPago.Clear();
            cboMetodoPago.Add();

            sSql = string.Format(" Select * From FACT_CFD_MetodosPago (Nolock) Order By IdMetodoPago ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "MetodosPagos");
                General.msjError("Ocurrió un error al obtener los Metodos de Pago.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboMetodoPago.Add(leer.DataSetClase, true);
                }
                else
                {
                    General.msjAviso("No se encontraron Metodos de Pago");
                }
            }

            cboMetodoPago.SelectedIndex = 0;
        }
        #endregion CargaCombos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(1);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        Imprimir();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    bContinua = GuardarInformacion(2);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        txtFolio.Text = sFolio;
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        Imprimir();
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        //Error.GrabarError(leer, "btnGuardar_Click");
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false, false);
            dtpFechaRegistro.Enabled = false;
            lblCancelado.Visible = false;
            txtSubTotal.Enabled = false;
            txtIva.Enabled = false;
            cboMetodoPago.SelectedIndex = 0;
            txtFolio.Focus();
        }

        private void IniciaToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void CargaDatos()
        {
            IniciaToolBar(false, true, true);
            txtFolio.Text = leer.Campo("Folio");
            txtFolio.Enabled = false;
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            txtServicio.Text = leer.Campo("IdServicio");
            lblServicio.Text = leer.Campo("Servicio");
            txtAcreedor.Text = leer.Campo("IdAcreedor");
            lblAcreedor.Text = leer.Campo("Acreedor");
            txtRefDocto.Text = leer.Campo("ReferenciaDocto");
            dtpFechaDoc.Value = leer.CampoFecha("FechaDocumento");
            cboMetodoPago.Data = leer.Campo("IdMetodoPago");
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString();
            nudTasaIva.Value = leer.CampoDec("TasaIva");
            txtIva.Text = leer.CampoDouble("Iva").ToString();
            txtTotal.Text = leer.CampoDouble("Total").ToString();
            txtObservaciones.Text = leer.Campo("Observaciones");

            if(leer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;
                IniciaToolBar(false, false, true);
            }
        }
        #endregion Funciones

        #region Eventos
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciaToolBar(true, false, true);
            }
            else
            {
                sSql = string.Format(" Select * From vw_FACT_Cuentas_X_Pagar (Nolock) Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                    " and Folio = '{3}' ",sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6) );

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating");
                    General.msjError("Ocurrió un error al obtener los datos del Folio.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        CargaDatos();                        
                    }
                    else
                    {
                        General.msjAviso("No se encontro el Folio");
                    }
                }
            }
        }

        private void txtServicio_Validating(object sender, CancelEventArgs e)
        {
            if (txtServicio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Servicios_Facturacion(txtServicio.Text, "txtServicio_Validating");
                if (leer.Leer())
                {
                    txtServicio.Text = leer.Campo("IdServicio");
                    lblServicio.Text = leer.Campo("Descripcion");
                }
                else
                {
                    txtServicio.Focus();
                }
            }
        }

        private void txtServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Servicios_Facturacion("txtServicio_KeyDown");

                if (leer.Leer())
                {
                    txtServicio.Text = leer.Campo("IdServicio");
                    lblServicio.Text = leer.Campo("Descripcion");
                }
                else
                {
                    txtServicio.Focus();
                }
            }
        }

        private void txtAcreedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtAcreedor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Acreedores_Facturacion(sEstado, txtAcreedor.Text, "txtAcreedor_Validating");
                if (leer.Leer())
                {
                    txtAcreedor.Text = leer.Campo("IdAcreedor");
                    lblAcreedor.Text = leer.Campo("Nombre");
                }
                else
                {
                    txtAcreedor.Focus();
                }
            }
        }

        private void txtAcreedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Acreedores_Facturacion(sEstado, "txtAcreedor_KeyDown");

                if (leer.Leer())
                {
                    txtAcreedor.Text = leer.Campo("IdAcreedor");
                    lblAcreedor.Text = leer.Campo("Nombre");
                }
                else
                {
                    txtAcreedor.Focus();
                }
            }
        }
        #endregion Eventos

        #region Eventos_Calculos
        private void nudTasaIva_ValueChanged(object sender, EventArgs e)
        {
            CalcularMontos();
        }

        private void nudTasaIva_Validating(object sender, CancelEventArgs e)
        {
            CalcularMontos();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularMontos();
        }

        private void txtTotal_Validating(object sender, CancelEventArgs e)
        {
            CalcularMontos();
        }

        private void CalcularMontos()
        {
            double dTasaIva = 0;
            double dSubTotal = 0;
            double dIva = 0;
            double dTotal = 0;

            try
            {
                dTasaIva = Convert.ToDouble(nudTasaIva.Value) / 100;
            }
            catch
            {
                dTasaIva = 0;
            }
            try
            {
                dTotal = Convert.ToDouble(txtTotal.Text);
            }
            catch
            {
                dTotal = 0;
            }

            dSubTotal = (dTotal / (1 + dTasaIva));

            dIva = dTotal - dSubTotal;

            txtSubTotal.Text = dSubTotal.ToString(sFormato);
            txtIva.Text = dIva.ToString(sFormato);

        }
        #endregion Eventos_Calculos

        #region Guardar/Cancelar_Informacion
        private bool GuardarInformacion(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_FACT_Cuentas_X_Pagar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}',  " +
                                    " '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ", sEmpresa, sEstado, sFarmacia, txtFolio.Text,
                                    txtServicio.Text, txtAcreedor.Text, sIdPersonal, txtRefDocto.Text, General.FechaYMD(dtpFechaDoc.Value, "-"),
                                    cboMetodoPago.Data, txtSubTotal.NumericText, nudTasaIva.Value, txtIva.NumericText, txtTotal.NumericText,
                                    txtObservaciones.Text, iOpcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GuardarInformacion");
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                }
            }

            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                General.msjUser("Ingrese el Folio por favor");
                txtFolio.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtServicio.Text.Trim() == "")
            {
                General.msjUser("Ingrese el tipo de servicio por favor");
                txtServicio.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtAcreedor.Text.Trim() == "")
            {
                General.msjUser("Ingrese el Acreedor por favor");
                txtAcreedor.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtRefDocto.Text.Trim() == "")
            {
                General.msjUser("Ingrese la Referencia del Documento por favor");
                txtRefDocto.Focus();
                bRegresa = false;
            }

            if (bRegresa && cboMetodoPago.SelectedIndex == 0 )
            {
                General.msjUser("Seleccione el Metodo de Pago por favor");
                cboMetodoPago.Focus();
                bRegresa = false;
            }

            if (bRegresa && Convert.ToDouble(txtTotal.Text) == 0 )
            {
                General.msjUser("Capture el Total por favor");
                txtTotal.Focus();
                bRegresa = false;
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                General.msjUser("Ingrese las Observaciones por favor");
                txtObservaciones.Focus();
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Guardar/Cancelar_Informacion

        #region Impresion
        private void Imprimir()
        {
            bool bRegresa = true;

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);                       

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "Rpt_FACT_CuentasporPagar";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", Fg.PonCeros(txtFolio.Text, 6));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion
    }
}
