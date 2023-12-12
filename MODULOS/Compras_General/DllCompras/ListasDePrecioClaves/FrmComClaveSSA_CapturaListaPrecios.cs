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
using SC_SolutionsSystem.Errores;

using DllFarmaciaSoft;
using DllCompras;


namespace DllCompras.ListasDePrecioClaves
{
    public partial class FrmComClaveSSA_CapturaListaPrecios : FrmBaseExt 
    {
        private enum Cols
        {
            IdClaveSSA = 1, ClaveSSA = 2, Status = 3, Descripcion = 4, PrecioUnitario = 5, 
            Descuento = 6, TasaIva = 7, Iva = 8, Importe = 9, FechaRegistro = 10, FechaVigencia = 11
        }

        private clsConexionSQL myCnn = new clsConexionSQL(General.DatosConexion);
        private clsGrabarError myError = new clsGrabarError();
        private clsAyudas myAyuda;
        private clsConsultas myQuery;
        private clsLeer myLeer;
        private clsGrid grid;
        private DateTime dtFechaServer = DateTime.Now;
        private DateTime dtFechaMin = DateTime.Now;
        // private string sClaveSSA = "";
        private bool bCargoProveedor = false;

        clsDatosCliente datosCliente; 

        public FrmComClaveSSA_CapturaListaPrecios()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            myCnn = new clsConexionSQL(General.DatosConexion);
            myLeer = new clsLeer(ref myCnn);
            myAyuda = new clsAyudas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version);
            myQuery = new clsConsultas(General.DatosConexion, "DllCompras", this.Name, Application.ProductVersion);
            grid = new clsGrid(ref grdListaDePrecios, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            grid.SetOrder((int)Cols.ClaveSSA, 1, true);
            grid.SetOrder((int)Cols.Descripcion, 1, true);
            grid.SetOrder((int)Cols.PrecioUnitario, 1, true);

            grid.ResizeColumns = true;
            
            dtFechaMin = Convert.ToDateTime("2010-05-01"); 
        }

        private void FrmListaPreciosClaveSSA_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Teclas Rápidas
        ////protected override void OnKeyDown(KeyEventArgs e)
        ////{
        ////    if (e.Control)
        ////    {
        ////        TeclasRapidas(e);
        ////    }
        ////}

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;
                case Keys.C:
                    if (btnCancelar.Enabled)
                    {
                        btnCancelar_Click(null, null);
                    }
                    break;
                case Keys.E:
                    if (btnEjecutar.Enabled)
                    {
                        btnEjecutar_Click(null, null);
                    }
                    break;
                case Keys.P:
                    if (btnImprimir.Enabled)
                    {
                        btnImprimir_Click(null, null);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Botones
        private void Limpiar()
        {
            grid.Limpiar();
            Fg.IniciaControles(this, true, FrameDatosProveedor);
            Fg.IniciaControles(this, true, FrameDatosPrecio);
            Fg.IniciaControles(this, true, FrameDatosClave);
            InicializaToolBar(true, false, false, true, false);

            BloqueaPrecios();

            nudTasaIva.Value = 0M;
            txtIdProveedor.Focus();
            bCargoProveedor = false;
            InicializaToolBar(true, false, false, false, false);
        }

        private void ActualizarDatosVigencias()
        {                 
            ////dtpFechaRegistro.Enabled = false;
            ////dtpFechaRegistro.Value = General.FechaSistema;
            ////dtpFechaVigencia.MinDate = General.FechaSistema.AddDays(15);            
        }

        private void BloqueaPrecios()
        {           
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
            txtIdProveedor_Validating(null, null);

            if (bCargoProveedor)
            {
                CargarListaPrecios();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;

            if (grid.Rows > 0)
            {
                datosCliente.Funcion = "Imprimir"; 
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnCompras.RutaReportes;
                myRpt.NombreReporte = "COM_OCEN_ListaDePrecios_Claves_Proveedor.rpt";

                myRpt.Add("IdProveedor", txtIdProveedor.Text);                            

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, datosCliente); 

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

            if (txtIdProveedor.Text == "")
            {
                General.msjAviso("No ha capturado un Proveedor válido, verifique.");
                txtIdProveedor.Focus();
                bRegresa = false;
                return bRegresa;
            }
            if (txtCodClaveSSA.Text == "")
            {
                General.msjAviso("No ha capturado un CodigoEAN válido, verifique.");
                txtCodClaveSSA.Focus();
                bRegresa = false;
                return bRegresa;
            }

            return bRegresa;
        }

        private void GuardarInformacion(int Tipo)
        {
            string Precio = "", Importe = "", Iva = "", sCadena = "";

            sCadena = txtPrecio.Text.Replace(",", "");
            Precio = sCadena;
            sCadena = txtImporte.Text.Replace(",", "");
            Importe = sCadena;
            sCadena = txtIva.Text.Replace(",", "");
            Iva = sCadena;

            if (myCnn.Abrir())
            {
                myCnn.IniciarTransaccion();

                string sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_COM_OCEN_ListaDePrecios_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'," + 
                    " '{6}', '{7}', '{8}', '{9}'", 
                    txtIdProveedor.Text, lblClaveSSA.Text, Precio, txtDescuento.Text, nudTasaIva.Value, Iva, Importe,
                    General.FechaYMD(dtpFechaRegistro.Value), General.FechaYMD(dtpFechaVigencia.Value), Tipo);

                if (!myLeer.Exec(sSql))
                {
                    myCnn.DeshacerTransaccion();
                    myError.GrabarError(General.MsjErrorAbrirConexion, "GuardarInformacion()");
                    General.msjError("Ocurrió un error al guardar la información de la Clave.");
                }
                else 
                {
                    myCnn.CompletarTransaccion();

                    if (myLeer.Leer())
                    {
                        General.msjUser(myLeer.Campo("Mensaje"));
                        Limpiar();
                    }
                }

                myCnn.Cerrar();
            }
            else
            {
                General.msjAviso(General.MsjErrorAbrirConexion);
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
            iIva = (Convert.ToDouble(nudTasaIva.Value) / 100) * iPrecio;
            iTotal = iPrecio + iIva;

            txtIva.Text = iIva.ToString();
            txtImporte.Text = iTotal.ToString(); 
        }

        private void BuscarDatosPrecio()
        {
            bool bPuedeModificar = true;
            string sSql = string.Format("Exec spp_COM_OCEN_PrecioProductos '{0}', '{1}', '{2}' ", txtIdProveedor.Text, txtCodClaveSSA.Text);

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    txtPrecio.Text = myLeer.CampoDouble("Precio").ToString();
                    txtDescuento.Text = myLeer.CampoDouble("Descuento").ToString();

                    txtIva.Text = myLeer.CampoDouble("Iva").ToString();
                    txtImporte.Text = myLeer.CampoDouble("Importe").ToString();
                    dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");

                    try
                    {
                        dtpFechaVigencia.MinDate = myLeer.CampoFecha("FechaFinVigencia");
                        dtpFechaVigencia.Value = myLeer.CampoFecha("FechaFinVigencia");
                    }
                    catch { }
                    
                    InicializaToolBar(true, true, true, true, false); 
                    bPuedeModificar = myLeer.CampoBool("EsVigente");

                    if (!bPuedeModificar)
                    {
                        BloquearDatosClaveSSA();
                    }

                }
                else
                {
                    //Significa que es la primera vez que se captura el codigo.
                    InicializaToolBar(true, true, false, true, false);
                }
            }
        }

        private void BloquearDatosClaveSSA()
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
            string sSql = string.Format(" Exec spp_COM_OCEN_PrecioProductosProveedor_Claves '{0}'  ", txtIdProveedor.Text);
            grid.Limpiar(false); 

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    grid.LlenarGrid(myLeer.DataSetClase);
                }

                InicializaToolBar(true, true, true, true, true);
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
                //Claves.Buscar_Clave(txtCodClaveSSA.Text.Trim());
                myQuery.MostrarMsjSiLeerVacio = false;

                myLeer.DataSetClase = myQuery.ClavesSSA_Sales(txtCodClaveSSA.Text, true, "txtCodClaveSSA_Validating");

                if (myLeer.Leer())
                {
                    lblClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcionClave.Text = myLeer.Campo("Descripcion");
                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");

                }
                // if (leer.Exec(""))
                {
                    //if (!Claves.Local.Leer())
                    //{
                    //    General.msjUser("Clave SSA no encontrada, verifique."); 
                    //}
                    //else 
                    //{
                    //    txtCodClaveSSA.Enabled = false;
                    //    sClaveSSA = Claves.Local.Campo("IdClaveSSA_Sal");
                    //    lblDescripcionClave.Text = Claves.Local.Campo("DescripcionSal");
                    //}
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

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
            ActualizarDatosVigencias();
        }

        private void dtpFechaVigencia_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFechaVigencia_Validating(object sender, CancelEventArgs e)
        {
            //dtpFechaRegistro.Enabled = false;            
        }

        private void grdListaDePrecios_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            txtCodClaveSSA.Text = "";

            txtCodClaveSSA.Text = grid.GetValue(grid.ActiveRow, (int)Cols.ClaveSSA);
            lblClaveSSA.Text = grid.GetValue(grid.ActiveRow, (int)Cols.IdClaveSSA);
            lblDescripcionClave.Text = grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion);
            txtPrecio.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.PrecioUnitario).ToString();
            txtDescuento.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.Descuento).ToString();
            nudTasaIva.Value = Convert.ToDecimal(grid.GetValueDou(grid.ActiveRow, (int)Cols.TasaIva));
            txtIva.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.Iva).ToString();
            txtImporte.Text = grid.GetValueDou(grid.ActiveRow, (int)Cols.Importe).ToString();

            txtCodClaveSSA_Validating(null, null);

            if (grid.GetValueObj(grid.ActiveRow, (int)Cols.Status).ToString() == "CANCELADO")
            {
                btnCancelar.Enabled = false;
            }
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text != "")
            {
                myQuery.MostrarMsjSiLeerVacio = false;

                myLeer.DataSetClase = myQuery.Proveedores(Fg.PonCeros(txtIdProveedor.Text, 4), "txtIdProveedor_Validating");

                if (myLeer.Leer())
                {
                    lblNombreProveedor.Text = myLeer.Campo("Nombre");
                    txtCodClaveSSA.Focus();
                    txtIdProveedor.Text = myLeer.Campo("IdProveedor");
                    txtIdProveedor.Enabled = false;
                    bCargoProveedor = true;

                    btnImprimir.Enabled = true;
                    btnEjecutar.Enabled = true;
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameDatosProveedor);
            }
        }

        private void txtIdClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodClaveSSA.Text != "")
            {
                myQuery.MostrarMsjSiLeerVacio = false;

                myLeer.DataSetClase = myQuery.ClavesSSA_Sales(txtCodClaveSSA.Text, true, "txtCodClaveSSA_Validating");

                if (myLeer.Leer())
                {
                    txtCodClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    lblClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcionClave.Text = myLeer.Campo("Descripcion");

                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");

                    txtCodClaveSSA.Enabled = false;
                    InicializaToolBar(true, true, false, true, false);
                }
            }
            else
            {
                Fg.IniciaControles(this, true, FrameDatosClave);
                //ActualizarDatosVigencias();
            }
            BloqueaPrecios();
        }

        private void nudTasaIva_ValueChanged(object sender, EventArgs e)
        {
            CalcularPrecio();
        }                 
        #endregion Eventos                    

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerProveedores = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {                
                myLeerProveedores.DataSetClase = myAyuda.Proveedores("txtIdProveedor_KeyDown()");

                if (myLeerProveedores.Leer())
                {
                    txtIdProveedor.Text = myLeerProveedores.Campo("IdProveedor");
                    lblNombreProveedor.Text = myLeerProveedores.Campo("Nombre");
                    txtIdProveedor.Enabled = false;
                }            
            }
        }

        private void txtCodClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            clsLeer myLeerClavesSSA = new clsLeer(ref myCnn);

            if (e.KeyCode == Keys.F1)
            {
                myLeerClavesSSA.DataSetClase = myAyuda.ClavesSSA_Sales("txtCodClaveSSA_KeyDown()");

                if (myLeerClavesSSA.Leer())
                {
                    txtCodClaveSSA.Text = myLeerClavesSSA.Campo("ClaveSSA");
                    lblClaveSSA.Text = myLeerClavesSSA.Campo("IdClaveSSA_Sal");
                    lblDescripcionClave.Text = myLeerClavesSSA.Campo("Descripcion");
                    txtCodClaveSSA.Enabled = false;
                }
            }
        }
    }
}
