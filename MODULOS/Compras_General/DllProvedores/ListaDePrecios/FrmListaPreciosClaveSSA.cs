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
using SC_SolutionsSystem.Reportes;

using DllProveedores;
using DllProveedores.Consultas;

namespace DllProveedores.ListaDePrecios
{
    public partial class FrmListaPreciosClaveSSA : FrmBaseExt 
    {
        private enum Cols
        {
            IdClaveSSA = 1, ClaveSSA = 2, CodigoEAN = 3
        }

        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsDatosCliente DatosCliente;
        clsClavesSSA Claves  = GnProveedores.Claves;
        clsProductosCodigosEAN CodigosEAN   = GnProveedores.CodigosEAN;
        wsProveedores.wsCnnProveedores conexionWeb;
        clsGrid grid;
        DateTime dtFechaServer = DateTime.Now;
        DateTime dtFechaMin = DateTime.Now; 

        string sClaveSSA = ""; 
        double iTasaIva = 0; 

        public FrmListaPreciosClaveSSA()
        {
            InitializeComponent();
            grid = new clsGrid(ref grdListaDePrecios, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
            conexionWeb = new DllProveedores.wsProveedores.wsCnnProveedores();
            conexionWeb.Url = General.Url;

            dtFechaServer = GnProveedores.ObtenerFechaServidor();
            dtFechaMin = Convert.ToDateTime("2010-05-01"); 
        }

        private void FrmListaPreciosClaveSSA_Load(object sender, EventArgs e)
        {
            Limpiar();  
        }

        #region Botones 
        private void Limpiar()
        {
            grid.Limpiar();
            Fg.IniciaControles();
            BloqueaPrecios();
            InicializaToolBar(true, false, false, true, false);

            ActualizarDatosVigencias();
            txtCodClaveSSA.Focus();
        }

        private void ActualizarDatosVigencias()
        {
            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnProveedores.FechaServidor;
            dtpFechaVigencia.MinDate = GnProveedores.FechaServidor.AddDays(15); 
        }

        private void BloqueaPrecios()
        {
            // xtDescuento.Enabled = false;
            txtIva.Enabled = false;
            txtImporte.Enabled = false; 
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
            bool bRegresa = false;

            if (grid.Rows > 0)
            {
                DatosCliente.Funcion = "ImprimirCompra()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnProveedores.RutaReportes;
                myRpt.NombreReporte = "Proveedores_ListaDePrecios.rpt";

                myRpt.Add("@IdProveedor", GnProveedores.IdProveedor);
                
                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                if (bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
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
            string sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_COM_OCEN_ListaDePrecios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                GnProveedores.IdProveedor, sClaveSSA, txtCodEAN.Text,
                txtPrecio.Text, txtDescuento.Text, iTasaIva, txtIva.Text, txtImporte.Text, 
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
            txtImporte.Text = iTotal.ToString(); 
        }

        private void BuscarDatosPrecio()
        {
            bool bPuedeModificar = true;
            string sSql = string.Format("Exec spp_COM_OCEN_PrecioProductos '{0}', '{1}', '{2}' ",
                GnProveedores.IdProveedor, sClaveSSA, txtCodEAN.Text.Trim());

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    txtPrecio.Text = leer.CampoDouble("Precio").ToString();
                    txtDescuento.Text = leer.CampoDouble("Descuento").ToString();

                    txtIva.Text = leer.CampoDouble("Iva").ToString();
                    txtImporte.Text = leer.CampoDouble("Importe").ToString();
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    try
                    {
                        dtpFechaVigencia.MinDate = leer.CampoFecha("FechaFinVigencia");
                        dtpFechaVigencia.Value = leer.CampoFecha("FechaFinVigencia");
                    }
                    catch { }
                    
                    InicializaToolBar(true, true, true, true, false); 
                    bPuedeModificar = leer.CampoBool("EsVigente"); 
                    if (!bPuedeModificar)
                    {
                        BloquearDatosCodigoEAN();
                    }

                }
                else
                {
                    //Significa que es la primera vez que se captura el codigo.
                    InicializaToolBar(true, true, false, true, false);
                }
            }
        }

        private void BloquearDatosCodigoEAN()
        {
            InicializaToolBar(true, false, false, true, false);
            txtPrecio.Enabled = false;
            txtDescuento.Enabled = false;
            txtIva.Enabled = false;
            txtImporte.Enabled = false;
            dtpFechaRegistro.Enabled = false;
            dtpFechaVigencia.Enabled = false;
        }

        private void CargarListaPrecios()
        {
            string sSql = string.Format(" Exec spp_COM_OCEN_PrecioProductosProveedor '{0}'  ", GnProveedores.IdProveedor);
            grid.Limpiar(false); 
            if (leer.Exec(sSql))
            {
                if ( leer.Leer() )
                    grid.LlenarGrid(leer.DataSetClase);

                InicializaToolBar(true, false, false, true, true);
            }
        }

        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bEjecutar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnEjecutar.Enabled = bEjecutar;
            btnImprimir.Enabled = bImprimir;
        }
        #endregion Funciones y Procedimientos Privados

        #region Buscar ClaveSSA 
        private void txtCodClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodClaveSSA.Text != "")
            {
                Claves.Buscar_Clave(txtCodClaveSSA.Text.Trim());
                // if (leer.Exec(""))
                {
                    if (!Claves.Local.Leer())
                    {
                        General.msjUser("Clave SSA no encontrada, verifique."); 
                    }
                    else 
                    {
                        txtCodClaveSSA.Enabled = false;
                        sClaveSSA = Claves.Local.Campo("IdClaveSSA_Sal");
                        lblDescripcionClave.Text = Claves.Local.Campo("DescripcionSal");
                    }
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameDatosClave);
                ActualizarDatosVigencias(); 
                //txtCodClaveSSA.Focus(); 
            }
            BloqueaPrecios(); 
        }
        #endregion Buscar ClaveSSA

        #region Buscar CodigoEAN
        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodEAN.Text != "")
            {
                InicializaToolBar(true, false, false, true, false);
                CodigosEAN.Buscar_CodigoEAN(sClaveSSA, txtCodEAN.Text.Trim());
                if (!CodigosEAN.Local.Leer())
                {
                    General.msjUser("El Codigo EAN " + txtCodEAN.Text.Trim() + " no esta registrado ó no pertenece a la Clave SSA." ); 
                }
                else 
                {
                    txtCodEAN.Enabled = false;
                    lblProducto.Text = CodigosEAN.Local.Campo("Descripcion");
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
                lblProducto.Text = ""; 
                Fg.IniciaControles(this, true, FrameDatosPrecio);
                // txtCodEAN.Focus(); 
            }
            BloqueaPrecios();
        }
        #endregion Buscar CodigoEAN

        #region Eventos 
        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio();
        }

        private void txtDescuento_Validating(object sender, CancelEventArgs e)
        {
            CalcularPrecio(); 
        }


        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
            ActualizarDatosVigencias(); 
        }
        #endregion Eventos

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            ActualizarDatosVigencias(); 
        }

        private void dtpFechaVigencia_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFechaVigencia_Validating(object sender, CancelEventArgs e)
        {
            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnProveedores.FechaServidor;
        }

        private void grdListaDePrecios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            txtCodClaveSSA.Text = "";
            txtCodEAN.Text = "";

            sClaveSSA = grid.GetValue(grid.ActiveRow, (int)Cols.IdClaveSSA);
            txtCodClaveSSA.Text = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);
            txtCodClaveSSA_Validating(null, null);

            txtCodEAN.Text = grid.GetValue(grid.ActiveRow, (int)Cols.CodigoEAN);
            txtCodEAN_Validating(null, null); 
        }
    }
}
