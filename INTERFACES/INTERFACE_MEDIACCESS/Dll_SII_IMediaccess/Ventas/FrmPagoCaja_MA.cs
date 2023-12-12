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
using DllFarmaciaSoft;

namespace Dll_SII_IMediaccess.Ventas
{
    public partial class FrmPagoCaja_MA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsAyudas Ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        private bool bEsPagoValido = false;

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
        clsGrid myGrid;
        Cols_Pago ColActiva = Cols_Pago.Ninguna;
        string sValorGrid = "";

        public FrmPagoCaja_MA()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Fg.IniciaControles();
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda.EsPublicoGeneral = true;

            myGrid = new clsGrid(ref grdPagos, this);
            grdPagos.EditModeReplace = true; 

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

        public clsGrid Grid
        {
            get { return myGrid; }
        }
        #endregion Propiedades

        #region Eventos 
        private void FrmPagoCaja_Load(object sender, EventArgs e)
        {
            lblSubTotalIva0.Text = dSubTotalIva0.ToString(sFormato);
            lblSubTotal.Text = dSubTotal.ToString(sFormato);
            lblIva.Text = dIva.ToString(sFormato);
            lblTotal.Text = (dTotal + dSubTotalIva0).ToString(sFormato);
            //lblTipoDeCambio.Text = dTipoDeCambio.ToString(sFormato);

            //txtCheques.Enabled = false;
            //if (dTipoDeCambio <= 0)
            //{
            //    txtDolares.Enabled = false;
            //}

            myGrid.Limpiar(true);
            CalcularPago();
        }

        private void CalcularPago()
        {
            double dImporte = myGrid.TotalizarColumnaDou((int)Cols_Pago.Importe);
            double dCambio = myGrid.TotalizarColumnaDou((int)Cols_Pago.Cambio);
            double dRestante = dTotal - dImporte; 

            ////if (dImporte != 0)
            ////{
            ////    dCambio = dImporte - Convert.ToDouble(lblTotal.Text);
            ////}

            lblTotalPago.Text = dImporte.ToString(sFormato);
            lblCambio.Text = dCambio.ToString(sFormato);
            lblMontoRestante.Text = dRestante.ToString(sFormato); 


            bEsPagoValido = dTotal == dImporte; 

        }

         #endregion Eventos 

        private void EfectuarPago()
        {
            SendKeys.Send("{TAB}");
            // lblTotalPago.Text == "" ? "0" : lblTotalPago.Text
            bool bRegresa = true;
            //double dPagoTotal = Convert.ToDouble(lblTotalPago.Text);
            //double dImportePagar = Convert.ToDouble(lblTotal.Text);




            bEfectuarPago = false;

            for (int i = 1; myGrid.Rows >= i && bRegresa; i++)
            {
                if (myGrid.GetValueDou(i, (int)Cols_Pago.Importe) == 0 || myGrid.GetValue(i, (int)Cols_Pago.Descripcion) == "")
                {
                    myGrid.DeleteRow(i);
                    i--;
                }
            }

            if (myGrid.Rows == 0) myGrid.Limpiar(true);

            double dPagoTotal = myGrid.TotalizarColumnaDou(Cols_Pago.Importe) ;
            double dImportePagar = Convert.ToDouble(lblTotal.Text == "" ? "0" : lblTotal.Text);



            if (bRegresa && dPagoTotal == 0)
            {
                bRegresa = false;
                General.msjError("No se ha capturado el pago de la venta, verifique.");
            }

            if (bRegresa && (dPagoTotal < dImportePagar))
            {
                bRegresa = false;
                General.msjError("El pago esta incompleto, verifique.");
            }

            if (bRegresa)
            {
                if (!bEsPagoValido)
                {
                    bRegresa = false;
                    General.msjUser("El importe total del cobro es mayor al monto a pagar, no es posible efectuar el pago.");
                }
            }

            if (bRegresa)
            {
                bEfectuarPago = true;
                ////dPagoEfectivo = Convert.ToDouble(txtEfectivo.Text);
                ////dPagoDolares = Convert.ToDouble(txtDolares.Text);
                ////dPagoCheques = Convert.ToDouble(txtCheques.Text);
                ////dCambios = Convert.ToDouble(lblCambio.Text);

                //dPagoEfectivo = Convert.ToDouble(txtEfectivo.Text == "" ? "0" : txtEfectivo.Text);
                //dPagoDolares = Convert.ToDouble(txtDolares.Text == "" ? "0" : txtDolares.Text);
                //dPagoCheques = Convert.ToDouble(txtCheques.Text == "" ? "0" : txtCheques.Text);
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

        private void grdPagos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            {
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo) != "" && myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols_Pago.Importe) > 0)
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    ////myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Importe, 0);
                    ////myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.PagoCon, 0);
                    ////myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Cambio, 0);
                    ////myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Referencia, ""); 
                    myGrid.SetActiveCell(myGrid.Rows, 1);
                }
            }
        }

        private void grdPagos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols_Pago)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols_Pago.Codigo:
                    ObtenerDatos();
                    break;

                case Cols_Pago.PagoCon:
                case Cols_Pago.Importe:

                    if ( myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols_Pago.PermiteDuplicidad)) 
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.PagoCon, myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols_Pago.Importe)); 
                    }

                    CalcularPago();
                    break;
            }
        }

        private void grdPagos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols_Pago)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols_Pago.Importe:
                    CalcularPago();
                //case Cols_Pago.Descripcion:
                //case Cols_Pago.Referencia:
                    break;

                case Cols_Pago.Codigo:
                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo);
                        leer.DataSetClase = Ayuda.FormaDePago("grdPagos_KeyDown");
                        if (leer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo, leer.Campo("IdFormasDePago"));
                            ObtenerDatos();
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        myGrid.DeleteRow(myGrid.ActiveRow);
                        if (myGrid.Rows == 0)
                        {
                            myGrid.Limpiar(true);
                        }
                    }
                        
                    break;
            }
        }

        private void ObtenerDatos()
        {
            string sIdFormasDePago = myGrid.GetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo);
            sIdFormasDePago = Fg.PonCeros(sIdFormasDePago, 2);

            string sSql = string.Format("Select * From CatFormasDePago (NoLock) Where Status = 'A' And IdFormasDePago = '{0}' ", sIdFormasDePago);

            if (!(sIdFormasDePago == "00" || sIdFormasDePago == ""))
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "ObtenerDatos()");
                    General.msjError("Ocurrió un error al obtener la información de la forma de págo.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Forma de págo no encontrada.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        myGrid.BloqueaCelda(false, myGrid.ActiveRow, (int)Cols_Pago.PagoCon);
                        myGrid.BloqueaCelda(false, myGrid.ActiveRow, (int)Cols_Pago.Referencia);

                        sIdFormasDePago = leer.Campo("IdFormasDePago");
                        if (!myGrid.BuscaRepetido(sIdFormasDePago, myGrid.ActiveRow, (int)Cols_Pago.Codigo) || leer.CampoBool("PermiteDuplicidad"))
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo, sIdFormasDePago);
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Descripcion, leer.Campo("Descripcion"));
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Importe, dTotal - myGrid.TotalizarColumnaDou((int)Cols_Pago.Importe));


                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Importe, 0);
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.PagoCon, 0);
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Cambio, 0);
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Referencia, "");
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.PermiteDuplicidad, leer.CampoBool("PermiteDuplicidad"));

                            if (leer.CampoBool("PermiteDuplicidad"))
                            {
                                myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols_Pago.PagoCon);
                            }
                            else
                            {
                                myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols_Pago.Referencia);
                            }

                            CalcularPago();
                        }
                        else
                        {
                            General.msjUser("La forma de págo ya se encuentra capturado en otro renglon.");
                            myGrid.SetValue(myGrid.ActiveRow, 1, "");
                            myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                            myGrid.EnviarARepetido();
                        }
                    }
                }
            }
            else
            {
                myGrid.SetValue(myGrid.ActiveRow, (int)Cols_Pago.Codigo, "");
            }
        }
    }
}
