using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

namespace Farmacia.Ventas
{
    public partial class FrmPagoCaja : FrmBaseExt
    {
        //clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;

        private double dSubTotalIva0 = 0;
        private double dSubTotal = 0;
        private double dIva = 0;
        private double dTotal = 0;
        private double dTipoDeCambio = 13;

        private double dPagoEfectivo = 0;
        private double dPagoDolares = 0;
        private double dPagoCheques = 0;
        private double dCambios = 0;

        private bool bEfectuarPago = false;
        string sFormato = "###,###,##0.#0";

        public FrmPagoCaja()
        {
            InitializeComponent();

            //leer = new clsLeer(ref cnn);
            Fg.IniciaControles();
        }

        #region Propiedades 
        public bool PagoEfectuado
        {
            get { return bEfectuarPago; }
        }

        public double PagoEfectivo
        {
            get { return dPagoEfectivo; }
        }

        public double PagoDolares
        {
            get { return dPagoDolares; }
        }

        public double PagoCheques
        {
            get { return dPagoCheques; }
        }

        public double CambioClientes
        {
            get { return dCambios; }
        }

        public double SubTotalIva0
        {
            get { return dSubTotalIva0; }
            set { dSubTotalIva0 = value; }
        }

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double Iva
        {
            get { return dIva; }
            set { dIva = value; }
        }

        public double Total
        {
            get { return dTotal; }
            set { dTotal = value; }
        }

        public double TipoDeCambio
        {
            get { return dTipoDeCambio; }
            set { dTipoDeCambio = value; }
        }
        #endregion Propiedades

        #region Eventos 
        private void FrmPagoCaja_Load(object sender, EventArgs e)
        {
            lblSubTotalIva0.Text = dSubTotalIva0.ToString(sFormato);
            lblSubTotal.Text = dSubTotal.ToString(sFormato);
            lblIva.Text = dIva.ToString(sFormato);
            lblTotal.Text = (dTotal + dSubTotalIva0).ToString(sFormato);
            lblTipoDeCambio.Text = dTipoDeCambio.ToString(sFormato);

            txtCheques.Enabled = false;
            if (dTipoDeCambio <= 0)
            {
                txtDolares.Enabled = false;
            }

            CalcularPago();
        }

        private void txtEfectivo_Enter(object sender, EventArgs e)
        {
            txtEfectivo.SelectAll();
        }

        private void txtDolares_Enter(object sender, EventArgs e)
        {
            txtDolares.SelectAll();
        }

        private void txtEfectivo_Validated(object sender, EventArgs e)
        {
            CalcularPago();
        }

        private void txtDolares_Validated(object sender, EventArgs e)
        {
            CalcularPago();
        }

        private void CalcularPago()
        {
            double dEfectivo = Convert.ToDouble("0" + txtEfectivo.Text);
            double dDolares = Convert.ToDouble("0" + txtDolares.Text) * Convert.ToDouble(dTipoDeCambio);
            double dCheques = Convert.ToDouble("0" + txtCheques.Text);
            double dCambio = 0;

            if (dEfectivo != 0 || dDolares != 0)
            {
                dCambio = (dEfectivo + dCheques + dDolares) - Convert.ToDouble(lblTotal.Text);
            }

            lblTotalPago.Text = (dEfectivo + dCheques + dDolares).ToString(sFormato);
            lblCambio.Text = dCambio.ToString(sFormato); 
        }

        private void txtEfectivo_MouseClick(object sender, MouseEventArgs e)
        {
            txtEfectivo.SelectAll();
        }

        private void txtDolares_MouseClick(object sender, MouseEventArgs e)
        {
            txtDolares.SelectAll();
        }

        private void txtCheques_Validated(object sender, EventArgs e)
        {
            CalcularPago();
        }

        private void txtCheques_Enter(object sender, EventArgs e)
        {
            txtCheques.SelectAll();
        }

        private void txtCheques_MouseClick(object sender, MouseEventArgs e)
        {
            txtCheques.SelectAll();
        }
        #endregion Eventos 

        private void EfectuarPago()
        {
            SendKeys.Send("{TAB}");
            // lblTotalPago.Text == "" ? "0" : lblTotalPago.Text
            bool bRegresa = true;
            //double dPagoTotal = Convert.ToDouble(lblTotalPago.Text);
            //double dImportePagar = Convert.ToDouble(lblTotal.Text);

            double dPagoTotal = Convert.ToDouble(lblTotalPago.Text == "" ? "0" : lblTotalPago.Text);
            double dImportePagar = Convert.ToDouble(lblTotal.Text == "" ? "0" : lblTotal.Text);

            bEfectuarPago = false;
            if (bRegresa && dPagoTotal == 0)
            {
                bRegresa = false;
                General.msjError("No se ha capturado el pago de la venta, verifique.");
            }

            if (bRegresa && (dPagoTotal < dImportePagar))
            {
                bRegresa = false;
                General.msjError("Pago incompleto, verifique.");
            }

            if (bRegresa)
            {
                bEfectuarPago = true;
                ////dPagoEfectivo = Convert.ToDouble(txtEfectivo.Text);
                ////dPagoDolares = Convert.ToDouble(txtDolares.Text);
                ////dPagoCheques = Convert.ToDouble(txtCheques.Text);
                ////dCambios = Convert.ToDouble(lblCambio.Text);

                dPagoEfectivo = Convert.ToDouble(txtEfectivo.Text == "" ? "0" : txtEfectivo.Text);
                dPagoDolares = Convert.ToDouble(txtDolares.Text == "" ? "0" : txtDolares.Text);
                dPagoCheques = Convert.ToDouble(txtCheques.Text == "" ? "0" : txtCheques.Text);
                dCambios = Convert.ToDouble(lblCambio.Text == "" ? "0" : lblCambio.Text);

                this.Hide();
            }
        }

        private void FrmPagoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    EfectuarPago();
                    break;
                case Keys.F12:
                    bEfectuarPago = false;
                    this.Hide();
                    break;
                default:
                    break;
            }
        }
    }
}
