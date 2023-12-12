using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllProveedores;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllProveedores.Consultas;
using DllProveedores.Lotes;

namespace DllProveedores.OrdenesCompra
{
    public partial class FrmOrdCompClavesSSA : FrmBaseExt
    {
        DllProveedores.wsProveedores.wsCnnProveedores ConexionWeb;
        clsLeer leerLocal;
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrid Grid;
        clsEAN_Lotes Lotes;
        clsDatosCliente DatosCliente;
        clsOrdCompraLotes ClsOrdCompra;

        string Empresa = "", Estado = "", Farmacia = "";

        string sSPEncabezado = "spp_Mtto_COM_OCEN_Ordenes_Compra_Enc";
        string sSPDetalles = "spp_Mtto_COM_OCEN_Ordenes_Compra_Det_Lotes";
        string Proveedor = GnProveedores.IdProveedor;
        bool bImprimir = false;       

        private enum Cols
        {
            Ninguno = 0,
            IdClaveSSA = 1, ClaveSSA = 2, CodigoEAN = 3, Descripcion = 4, Precio = 5, Cantidad = 6, Cant_Capturada = 7, Importe = 8
        }

        public FrmOrdCompClavesSSA()
        {
            InitializeComponent();

            ConexionWeb = new DllProveedores.wsProveedores.wsCnnProveedores();
            ConexionWeb.Url = General.Url;
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
            leerLocal = new clsLeer();

            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);

            Lotes = new clsEAN_Lotes();
            ClsOrdCompra = new clsOrdCompraLotes();
        }

        private void FrmOrdCompClavesSSA_Load(object sender, EventArgs e)
        {
            //btnNuevo_Click(null, null);
        }

        #region Botones

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            Grid.Limpiar(false);

            Lotes = new clsEAN_Lotes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)       
        {
            if (ChecarCantidades())
            {
                if (txtObservacionProv.Text != "")
                {
                    DataSet dtsInformacionWeb = new DataSet("dtsInformacionWeb");
                    DataSet dtsInformacionCliente = new DataSet("dtsInformacionCliente");

                    ObtenerDatos();

                    //Se obtiene el dataset que contiene el Encabezado y el Detalle.
                    dtsInformacionWeb = ClsOrdCompra.ObtenerInformacionWeb();

                    //Se obtiene la informacion del Cliente
                    dtsInformacionCliente = GnProveedores.DatosDelCliente.DatosCliente();

                    leerLocal.DataSetClase = ConexionWeb.EmbarcarOrdenCompra(dtsInformacionCliente, dtsInformacionWeb);

                    General.msjUser("Informacion Guardada Satisfactoriamente");
                    ImprimirOrdenCompra();
                    if (bImprimir)
                    {
                        btnGuardar.Enabled = false;
                        txtObservacionProv.Enabled = false;
                        groupBox2.Enabled = false;
                    }

                }
                else
                {
                    General.msjUser("Favor de Capturar las Observaciones");
                    txtObservacionProv.Focus();
                }
            }
            else
            {
                General.msjUser("Falta Completar Alguna Cantidad");
                grdClaves.Focus();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirOrdenCompra();
        }

        #endregion Botones

        #region Funciones

        public void LlenarEncabezado(string sFolio)
        {
            string sSql = String.Format(" Select IdEmpresa, IdEstado, IdFarmacia, Folio, FolioPedidoProv , EntregarEn, EntregarEnNombre, " +
                                        " FechaRequeridaEntrega, FechaPromesaEntrega, Observaciones " +
                                        " From vw_COM_OCEN_Ordenes_Compra_Enc ( nolock ) " +
                                        " Where Folio = '{0}' And IdProveedor = '{1}' ", sFolio, GnProveedores.IdProveedor);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el encabezado del Folio");
            }
            else
            {
                if (leer.Leer())
                {
                    txtFolio.Text = leer.Campo("Folio");
                    lblPedido.Text = leer.Campo("FolioPedidoProv");
                    txtEntregarEn.Text = leer.Campo("EntregarEn");
                    lblEntregarEn.Text = leer.Campo("EntregarEnNombre");
                    dtpFechaRequeridaEntrega.Value = leer.CampoFecha("FechaRequeridaEntrega");
                    dtpFechaPromesaEntrega.Value = leer.CampoFecha("FechaPromesaEntrega");
                    txtObservaciones.Text = leer.Campo("Observaciones");

                    Empresa = leer.Campo("IdEmpresa");
                    Estado = leer.Campo("IdEstado");
                    Farmacia = leer.Campo("IdFarmacia");

                    LlenarDetalle(sFolio);

                    InhibeControles(false);
                    txtObservacionProv.Focus();

                    this.ShowDialog();
                }
                else
                {
                    General.msjUser("No existen Información del Folio Seleccionado");
                }
            }
        }

        public void LlenarDetalle(string sFolio)        
        {
            string sSql = String.Format(" Select IdClaveSSA, ClaveSSA, CodigoEAN, DescripcionSal, Importe, Cantidad, " +
                                        " 0 As Cant_Capturada, ( Cantidad * Importe ) As ImporteTotal " +
                                        " From vw_COM_OCEN_Ordenes_Compra_Det ( nolock ) " +
                                        " Where Folio = '{0}' And IdProveedor = '{1}' ", sFolio, GnProveedores.IdProveedor);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el Detalle del Folio");
            }
            else
            {
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No existen Información del detalle del Folio");
                }
            }
        }

        private void InhibeControles(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtEntregarEn.Enabled = bValor;
            txtObservaciones.Enabled = bValor;
            dtpFechaRequeridaEntrega.Enabled = bValor;
            dtpFechaPromesaEntrega.Enabled = bValor;
            dtpFechaRegistro.Enabled = bValor;
            btnNuevo.Enabled = bValor;
        }

        private void ObtenerDatos()
        {                        
            string Folio = txtFolio.Text; 
            string Pedido = lblPedido.Text;     
            string Observaciones = txtObservacionProv.Text;

            string CodigoEAN = "", ClaveLote = "", Cantidad = "", IdClaveSSA = "";
            DateTime FechaCad = DateTime.Now;

            ClsOrdCompra.AgregarRenglonEncabezado(sSPEncabezado, Empresa, Estado, Farmacia, Proveedor, Folio, Pedido, Observaciones);

            leer.DataSetClase = Lotes.ListaLotes;
            while (leer.Leer())
            {
                IdClaveSSA = leer.Campo("IdClaveSSA");
                CodigoEAN  = leer.Campo("CodigoEAN");
                ClaveLote = leer.Campo("ClaveLote");
                Cantidad = leer.Campo("Cantidad");
                FechaCad = leer.CampoFecha("FechaCad");

                ClsOrdCompra.AgregarRenglonDetalles(sSPDetalles, Empresa, Estado, Farmacia, Folio, IdClaveSSA, CodigoEAN, ClaveLote, Cantidad, FechaCad);

                IdClaveSSA = "";
                CodigoEAN = "";
                ClaveLote = "";
                Cantidad = "";
                FechaCad = DateTime.Now;
            }
        }
                
        private void ImprimirOrdenCompra()
        {
            bool bRegresa = false;

            if (Grid.Rows > 0)
            {
                DatosCliente.Funcion = "ImprimirOrdenCompra()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                byte[] btReporte = null;

                myRpt.RutaReporte = GnProveedores.RutaReportes;
                myRpt.NombreReporte = "Orden_Compra_Embarcada.rpt";

                myRpt.Add("@IdProveedor", GnProveedores.IdProveedor);
                myRpt.Add("@Folio", txtFolio.Text);
                myRpt.Add("@FolioProv", lblPedido.Text);

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();

                btReporte = ConexionWeb.Reporte(InfoWeb, datosC);
                bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                if (bRegresa)
                {
                    //btnNuevo_Click(null, null);
                    //General.msjUser("Terminó Impresión.");
                    bImprimir = true;      
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private bool ChecarCantidades()
        {
            bool bContinua = true;
            int iCant = 0, iCant_Cap = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                iCant = Grid.GetValueInt(i, (int)Cols.Cantidad);
                iCant_Cap = Grid.GetValueInt(i, (int)Cols.Cant_Capturada);

                if ( iCant != iCant_Cap )
                {
                    bContinua = false;
                    break;
                }
            }

            return bContinua;
        }

        #endregion Funciones

        #region Eventos

        private void grdClaves_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Lotes.IdClaveSSA = Grid.GetValue(Grid.ActiveRow, (int)Cols.IdClaveSSA);
            Lotes.CodigoEAN = Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN);
            Lotes.Descripcion = Grid.GetValue(Grid.ActiveRow, (int)Cols.Descripcion);
            Lotes.Cantidad = Grid.GetValueInt(Grid.ActiveRow, (int)Cols.Cantidad);
            Lotes.PrimerLote = true;

            {
                Lotes.Show();
            }

            Grid.SetValue(Grid.ActiveRow, (int)Cols.Cant_Capturada, Lotes.CantEAN               );     

        }

        #endregion Eventos

    }

}
