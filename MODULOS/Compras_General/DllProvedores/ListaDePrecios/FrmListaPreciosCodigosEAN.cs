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

using DllProveedores;
using DllProveedores.Consultas;

namespace DllProveedores.ListaDePrecios
{
    public partial class FrmListaPreciosCodigosEAN : FrmBaseExt 
    {
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);

        clsClavesSSA Claves = GnProveedores.Claves;
        clsProductosCodigosEAN CodigosEAN = GnProveedores.CodigosEAN;

        clsGrid grid;
        string sClaveSSA = "";
        double iTasaIva = 0; 

        public FrmListaPreciosCodigosEAN()
        {
            InitializeComponent();
            grid = new clsGrid(ref grdListaDePrecios, this);
            grid.EstiloGrid(eModoGrid.ModoRow); 
        }

        private void FrmListaPreciosCodigosEAN_Load(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        #region Botones
        private void Limpiar()
        {
            grid.Limpiar();
            Fg.IniciaControles();
            BloqueaPrecios();
            txtCodEAN.Focus();
        }

        private void BloqueaPrecios()
        {
            txtIva.Enabled = false;
            txtTotal.Enabled = false;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
                GuardarInformacion(1);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
                GuardarInformacion(2); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarListaPrecios(); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones y Procedimientos Privados
        private bool validarDatos()
        {
            bool bRegresa = true;

            return bRegresa;
        }

        private void GuardarInformacion(int Tipo)
        {
            string sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_COM_OCEN_ListaDePreciosContado '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                GnProveedores.IdProveedor, txtCodEAN.Text,
                txtPrecio.Text.Replace(",", ""),
                txtDescuento.Text.Replace(",", ""),
                iTasaIva.ToString().Replace(",", ""),
                txtIva.Text.Replace(",", ""),
                txtTotal.Text.Replace(",", ""),
                General.FechaYMD(dtpFechaRegistro.Value), General.FechaYMD(dtpFechaVigencia.Value), Tipo);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    General.msjUser(leer.Campo(1));
                    Limpiar();
                }
            }
            else
            {
                General.msjError("Ocurrió un error al guardar la información de la Clave.");
            }
        }

        private void CalcularPrecio()
        {
            double iDescto = 0;
            double iPrecio = 0;
            double iIva = 0, iTotal = 0;

            try
            {
                iDescto = Convert.ToDouble(txtDescuento.Text) / 100;
            }
            catch
            {
                iDescto = 0;
            }
            try
            {
                iPrecio = Convert.ToDouble(txtPrecio.Text);
            }
            catch
            {
                iPrecio = 0;
            }

            iPrecio = iPrecio - (iPrecio * iDescto);
            iIva = (iTasaIva / 100) * iPrecio;
            iTotal = iPrecio + iIva;

            txtIva.Text = iIva.ToString();
            txtTotal.Text = iTotal.ToString();
        }

        private void BuscarDatosPrecio()
        {
            string sSql = string.Format("Exec spp_COM_OCEN_PrecioProductoContado '{0}', '{1}' ",
                GnProveedores.IdProveedor, txtCodEAN.Text.Trim());

            dtpFechaVigencia.Enabled = true;
            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    txtPrecio.Text = leer.CampoDouble("Precio").ToString();
                    txtDescuento.Text = leer.CampoDouble("Descuento").ToString();

                    txtIva.Text = leer.CampoDouble("Iva").ToString();
                    txtTotal.Text = leer.CampoDouble("Importe").ToString();

                    dtpFechaVigencia.Enabled = leer.CampoBool("VigenciaEnable");
                }
            }
        }

        private void CargarListaPrecios()
        {
            string sSql = string.Format("Exec spp_COM_OCEN_ListaPreciosContadoProveedor '{0}'  ", GnProveedores.IdProveedor);
            grid.Limpiar();
            if (leer.Exec(sSql))
            {
                grid.LlenarGrid(leer.DataSetClase);
            }
        }
        #endregion Funciones y Procedimientos Privados

        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodEAN.Text != "")
            {
                CodigosEAN.Buscar_CodigoEAN( txtCodEAN.Text.Trim());
                if (CodigosEAN.Local.Leer())
                {
                    // txtCodEAN.Enabled = false;
                    lblDescripcion.Text = CodigosEAN.Local.Campo("Descripcion");
                    iTasaIva = CodigosEAN.Local.CampoDouble("TasaIva");

                    txtPrecio.Text = "0";
                    txtDescuento.Text = "0";
                    CalcularPrecio();
                    BuscarDatosPrecio();
                }
            }
            else
            {
                txtCodEAN.Text = "";
                lblDescripcion.Text = "";
                Fg.IniciaControles(this, true, FrameDatosPrecio); 
            }
            BloqueaPrecios();
        }


        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void txtDescuento_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }
        

    }
}
