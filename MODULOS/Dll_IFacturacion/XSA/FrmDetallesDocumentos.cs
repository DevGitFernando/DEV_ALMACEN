using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using Dll_IFacturacion; 

namespace Dll_IFacturacion.XSA
{
    internal partial class FrmDetallesDocumentos : FrmBaseExt 
    {
        public bool Guardado = false;

        public string Identificador = "";
        public string SAT_ClaveProductoServicio = "";
        public string SAT_UnidadDeMedida = ""; 
        public string Clave = "";
        public string Descripcion = "";
        public double PrecioUnitario = 0.0;
        public int Cantidad = 0;
        public double TasaIva = 0.0;
        public double SubTotal = 0.0;
        public double Iva = 0.0;
        public double Total = 0.0;  
        public string UnidadDeMedida = "";
        public string TipoImpuesto = ""; 

        public DataSet UnidadesDeMedida = new DataSet();
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsLeer leer = new clsLeer(); 
        

        string sFormato = "###,###,###,###,##0.###0"; 

        public FrmDetallesDocumentos()
        {
            InitializeComponent();

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false); 
        }

        #region Form 
        private void FrmDetallesDocumentos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            txtIdentificador.Text = Identificador;
            lblProductoSAT.Text = SAT_ClaveProductoServicio;
            lblUnidadMedidaSAT.Text = SAT_UnidadDeMedida; 
            txtClave.Text = Clave;
            txtDescripcion.Text = Descripcion;
            cboTipoUnidad.Text = UnidadDeMedida;
            nmCantidad.Value = (decimal)Cantidad;
            nmPrecioUnitario.Value = (decimal)PrecioUnitario;
            nmTasaIva.Value = (decimal)TasaIva;
            lblSubTotal.Text = SubTotal.ToString(sFormato);
            lblIva.Text = Iva.ToString(sFormato);
            lblTotal.Text = Total.ToString(sFormato);



            cboTipoUnidad.Clear();
            cboTipoUnidad.Add(); 
            cboTipoUnidad.Filtro = " Status = 'A' ";
            cboTipoUnidad.Add(UnidadesDeMedida, true, "Descripcion", "Descripcion"); 
            cboTipoUnidad.SelectedIndex = 0;
            cboTipoUnidad.Text = UnidadDeMedida;


            cboTiposImpuestos.Clear();
            cboTiposImpuestos.Add(cfdImpuestosTrasladados.IVA.ToString().ToUpper(), cfdImpuestosTrasladados.IVA.ToString().ToUpper());
            cboTiposImpuestos.Add(cfdImpuestosTrasladados.IEPS.ToString().ToUpper(), cfdImpuestosTrasladados.IEPS.ToString().ToUpper()); 
            cboTiposImpuestos.SelectedIndex = 0;
            cboTiposImpuestos.Data = TipoImpuesto; 
        }
        #endregion Form

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            txtIdentificador.Enabled = false; 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardado = true; 

            Identificador = txtIdentificador.Text;
            SAT_ClaveProductoServicio = lblProductoSAT.Text;
            SAT_UnidadDeMedida = lblUnidadMedidaSAT.Text;
            Clave = txtClave.Text;
            Descripcion = txtDescripcion.Text; 
            UnidadDeMedida = cboTipoUnidad.Text; 
            Cantidad = (int)nmCantidad.Value;
            PrecioUnitario = (double)nmPrecioUnitario.Value;
            TasaIva = (double)nmTasaIva.Value;
            SubTotal = Math.Round( Convert.ToDouble(lblSubTotal.Text.Replace(",", "")), 2);
            Iva = Math.Round( Convert.ToDouble(lblIva.Text.Replace(",", "")), 2);
            Total = Math.Round(Convert.ToDouble(lblTotal.Text.Replace(",", "")), 2);
            TipoImpuesto = cboTiposImpuestos.Data; 

            this.Hide(); 
        }

        private void btnConceptos_Click(object sender, EventArgs e)
        {
            FrmConceptos f = new FrmConceptos();
            f.ShowConceptos();

            if (f.Guardo)
            {
                txtClave.Text = f.Clave;
                txtClave_Validating(null, null); 
            }
        }
        #endregion Botones

        #region Eventos 
        private void nmCantidad_Enter(object sender, EventArgs e)
        {
            int iLargo = nmCantidad.Value.ToString().Length + nmCantidad.DecimalPlaces + 1;
            nmCantidad.Select(0, iLargo); 
        }

        private void nmPrecioUnitario_Enter(object sender, EventArgs e)
        {
            int iLargo = nmPrecioUnitario.Value.ToString().Length + nmPrecioUnitario.DecimalPlaces + 1;
            nmPrecioUnitario.Select(0, iLargo); 
        }

        private void nmTasaIva_Enter(object sender, EventArgs e)
        {
            int iLargo = nmTasaIva.Value.ToString().Length + nmTasaIva.DecimalPlaces + 1;
            nmTasaIva.Select(0, iLargo);
        }

        private void nmCantidad_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotales(); 
        }

        private void nmPrecioUnitario_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotales(); 
        }

        private void nmTasaIva_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotales(); 
        }

        private void CalcularTotales()
        {
            decimal dSubTotal = nmCantidad.Value * nmPrecioUnitario.Value;
            decimal dIva = dSubTotal * (nmTasaIva.Value / 100);
            decimal dTotal = dSubTotal + dIva;

            dSubTotal = Math.Round(dSubTotal, 2);
            dIva = Math.Round(dIva, 2);
            dTotal = Math.Round(dTotal, 2); 

            lblSubTotal.Text = dSubTotal.ToString(sFormato);
            lblIva.Text = dIva.ToString(sFormato);
            lblTotal.Text = dTotal.ToString(sFormato); 
        }
        #endregion Eventos

        #region Conceptos 
        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            txtDescripcion.Text = ""; 
        } 

        private void txtClave_Validating(object sender, CancelEventArgs e)
        {
            if (txtClave.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.CFDI_Conceptos(txtClave.Text, "txtClave_Validating");
                if (leer.Leer())
                {
                    CargaDatos(); 
                }
            }
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.CFDI_Conceptos("txtClave_KeyDown"); 
                if (leer.Leer()) 
                {
                    CargaDatos();
                }
            }
        }

        private void CargaDatos()
        {
            if (leer.Campo("Status").ToUpper() == "C")
            {
                txtClave.Text = ""; 
                General.msjAviso("El concepto se encuentra cancelado, no es posible agregarlo al documento electrónico.");
                txtClave.Focus(); 
            }
            else
            {

                lblUnidadMedidaSAT.Text = leer.Campo("SAT_UnidadDeMedida");
                //lblUnidadMedidaSAT.Text = leer.Campo("Descripcion_SAT_UnidadDeMedida");
                lblProductoSAT.Text = leer.Campo("SAT_ClaveProducto_Servicio");
                //lblProductoSAT.Text = leer.Campo("Descripcion_SAT_ClaveDeProducto_Servicio"); 

                txtDescripcion.Text = leer.Campo("Descripcion");
                txtDescripcion.Focus();
            }
        }
        #endregion Conceptos
    }
}
