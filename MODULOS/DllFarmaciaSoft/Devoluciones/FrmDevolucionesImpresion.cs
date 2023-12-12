using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft.Reporteador;

namespace DllFarmaciaSoft.Devoluciones
{
    public partial class FrmDevolucionesImpresion : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        bool bOpcionExterna = false;
        int iTipoDevolucion = 0, iTipoVenta = 0;
        TipoDevolucion tpDevolucion = TipoDevolucion.Ninguna; 

        private enum Cols
        {
            Ninguna = 0,
            Folio = 1, Fecha = 2, Total = 3
        }

        public FrmDevolucionesImpresion()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SeleccionSimple);
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);

        }

        private void FrmComprasFarmacia_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);

            string sFrameEnc = "", sFrameDet = "";
            string sTitulo = "";
            
            if (tpDevolucion == TipoDevolucion.Compras)
            {
                sTitulo = "Reimpresión Dev. Compra";
                sFrameEnc = "Información";
                sFrameDet = "Folios Devoluciones";
            }

            if (tpDevolucion == TipoDevolucion.Venta)
            {
                sTitulo = "Reimpresión Dev. Dispersión";
                sFrameEnc = "Información";
                sFrameDet = "Folios Devoluciones";
            }

            if (tpDevolucion == TipoDevolucion.EntradasDeConsignacion)
            {
                sTitulo = "Reimpresión Dev. Ingresos Insumos";
                sFrameEnc = "Información";
                sFrameDet = "Folios Devoluciones";
            }

            if (tpDevolucion == TipoDevolucion.PedidosVenta)
            {
                sTitulo = "Reimpresión Dev. Pedidos de Distribuidor";
                sFrameEnc = "Información";
                sFrameDet = "Folios Devoluciones";
            }

            if (tpDevolucion == TipoDevolucion.OrdenCompra)
            {
                sTitulo = "Reimpresión Dev. OC";
                sFrameEnc = "Información";
                sFrameDet = "Folios Devoluciones";
            }

            this.Text = sTitulo;
            FrameEncabezado.Text = sFrameEnc;
            FrameDetalle.Text = sFrameDet;
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        #region Limpiar

        private void IniciarToolBar(bool Nuevo, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!bOpcionExterna)
            {
                IniciarToolBar(true, false);
                Fg.IniciaControles(this, false);
                myGrid.Limpiar(false);
                txtFolio.Enabled = true;
                txtFolio.Focus();
            }

        }

        #endregion Limpiar

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            myLeer = new clsLeer(ref ConexionLocal);
            string sSql = "";

            if (txtFolio.Text.Trim() != "")
            {
                //sSql = string.Format("Select FolioDevolucion, Convert( varchar(10), FechaSistema, 120 ) as FechaSistema, Total, Referencia " +
                //    " From DevolucionesEnc(NoLock) " +
                //    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Referencia = '{3}' And TipoDeDevolucion = '{4}'",
                //    DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim(), iTipoDevolucion);

                sSql = QueryTipo_Devolucion();

                myGrid.Limpiar(false);
                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "");
                    General.msjError("Error al consultar el Folio.");
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        CargaEncabezadoFolio();
                        myGrid.LlenarGrid(myLeer.DataSetClase);

                        if (!bOpcionExterna)
                        {
                            IniciarToolBar(true, true);
                        }
                        else
                        {
                            IniciarToolBar(false, true);
                        }
                    }
                    else
                    {
                        IniciarToolBar(true, false);
                        General.msjUser("Folio no valido ó sin devoluciones");
                        txtFolio.Focus();
                    }
                }
            }
        }

        private void CargaEncabezadoFolio()
        {
            //Se hace de esta manera para la ayuda.
            txtFolio.Enabled = false;
            txtFolio.Text = myLeer.Campo("Referencia");  //FolioCompra 
        }

        private string QueryTipo_Devolucion()
        {
            string sQuery = "";

            if (tpDevolucion == TipoDevolucion.TransferenciaDeEntrada || tpDevolucion == TipoDevolucion.TransferenciaDeSalida)
            {
                sQuery = string.Format("Select FolioDevolucion, Convert( varchar(10), FechaRegistro, 120 ) as FechaSistema, Total, FolioTransferencia as Referencia \n" +
                   "From DevolucionTransferenciasEnc (NoLock) \n" +
                   "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n",
                   DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim() );
            }
            else
            {
                sQuery = string.Format("Select FolioDevolucion, Convert( varchar(10), FechaSistema, 120 ) as FechaSistema, Total, Referencia \n" +
                   "From DevolucionesEnc (NoLock) \n" +
                   "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Referencia = '{3}' And TipoDeDevolucion = '{4}' \n",
                   DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text.Trim(), iTipoDevolucion);
            }

            return sQuery;
        }
        #endregion Buscar Folio
        
        #region Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iRenglon = myGrid.ActiveRow;
            string sFolio = "";
            double dImporte = 0.0000;

            sFolio = myGrid.GetValue(iRenglon, (int)Cols.Folio);
            dImporte = myGrid.GetValueDou(iRenglon, (int)Cols.Total);

            if (sFolio.Trim() == "")
            {
                General.msjUser("Seleccione Folio por favor");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                //myRpt.TituloReporte = "Reporte de devolución";
                myRpt.RutaReporte = DtGeneral.RutaReportes;

                if (tpDevolucion == TipoDevolucion.Compras)
                {
                    myRpt.NombreReporte = "PtoVta_Compras_Devolucion.rpt";
                }
                
                if (tpDevolucion == TipoDevolucion.Venta)
                {
                    if (iTipoVenta == (int)TipoDeVenta.Publico)
                    {
                        myRpt.Add("CantidadLetra", "( " + General.LetraMoneda(dImporte) + " )");
                        myRpt.NombreReporte = "PtoVta_TicketPublicoGral_Devolucion.rpt";
                    }
                    else
                    {
                        myRpt.NombreReporte = "PtoVta_TicketCredito_Devolucion.rpt"; 
                    }
                }

                if (tpDevolucion == TipoDevolucion.PedidosVenta)
                {
                    myRpt.NombreReporte = "PtoVta_Pedidos_Devolucion.rpt";
                }

                if (tpDevolucion == TipoDevolucion.EntradasDeConsignacion)
                {
                    myRpt.NombreReporte = "PtoVta_Entradas_Consignacion_Devolucion.rpt";
                }

                if (tpDevolucion == TipoDevolucion.OrdenCompra)
                {
                    myRpt.NombreReporte = "PtoVta_Devolucion_Orden_Compras.rpt";
                }

                if (tpDevolucion == TipoDevolucion.Dev_Proveedor)
                {
                    myRpt.NombreReporte = "PtoVta_Cancelacion_Dev_A_Proveedor.rpt";
                }

                if (tpDevolucion == TipoDevolucion.TransferenciaDeEntrada)
                {
                    myRpt.NombreReporte = "PtoVta_DevolucionesDeTransferencias.rpt";
                }

                if (tpDevolucion == TipoDevolucion.TransferenciaDeSalida)
                {
                    myRpt.NombreReporte = "PtoVta_DevolucionesDeTransferencias.rpt";
                }

                if (tpDevolucion == TipoDevolucion.VentasSocioComercial)
                {
                    myRpt.NombreReporte = "PtoVta_Devoluciones_SalidaVentas_Comerciales.rpt";
                }

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolio);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if(bRegresa)
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Error al imprimir reporte.");
                    }
                }
            }

        }
        #endregion Imprimir

        #region Eventos 
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = Ayuda.Folios_Compras(DtGeneral.EmpresaConectada, sEstado, sFarmacia, "txtFolio_KeyDown");

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                }
            } 
        }
        #endregion Eventos

        #region Funciones
        public void MostrarPantalla(string Folio, TipoDevolucion TipoDev, int TipoDeVenta)
        {
            tpDevolucion = TipoDev; 
            iTipoDevolucion = (int)TipoDev; 
            iTipoVenta = TipoDeVenta;
            txtFolio.Text = Folio;
            bOpcionExterna = true;
            txtFolio_Validating(null, null);
            this.ShowDialog();
        }
        #endregion Funciones


    } // Llaves de la Clase
} // Llaves del NameSpace
