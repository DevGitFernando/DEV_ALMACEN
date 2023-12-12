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
using DllFarmaciaSoft;
using DllFarmaciaSoft.Procesos;
using Farmacia.Procesos;

namespace Farmacia.Pedidos
{
    public partial class FrmPedidoEspecial_Productos : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, CodigoEAN = 3, Descripcion = 4, Cantidad = 5 
        }
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid Grid;

        clsCodigoEAN EAN = new clsCodigoEAN();
        clsAyudas Ayuda = new clsAyudas();
        clsConsultas Consultas;

        clsImprimirPedidos PedidosImprimir;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sRutaReportes = General.RutaDeReportes;
        string sMensaje = "", sFolioPedido = "", sTipoPedido = "";
        string sValorGrid = "";
        bool bContinua = true;
        // bool bGeneraPedidoValidado = false; 
        // Cols ColActiva = Cols.Ninguna; 

        int iTipoDePedido = (int)TipoDePedido.Productos;

        public FrmPedidoEspecial_Productos()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);


            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);            

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);             

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            PedidosImprimir = new clsImprimirPedidos(General.DatosConexion, DatosCliente,
               sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReportePedido.Credito);

            grdProductos.EditModeReplace = true;
            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.Limpiar(false);

            dtpFecha.Enabled = false;
            txtIdPersonal.Enabled = false;
            txtObservaciones.Enabled = false;
            txtFolio.Focus();

            sTipoPedido = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Especial, 2);
        }

        private void FrmPedidos_Sales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmPedidos.Enabled = true;
            tmPedidos.Start(); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;
            Grid.Limpiar(false);
            dtpFecha.Enabled = false;
            txtIdPersonal.Enabled = false;
            txtObservaciones.Enabled = false;
            txtFolio.Focus(); 
        }       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;

            if (txtFolio.Text != "*")
            {
                General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado.");
            }
            else
            {
                if (ValidaDatos())
                {
                    if (cnn.Abrir())
                    {
                        IniciarToolBar(false, true);
                        cnn.IniciarTransaccion();

                        if ( GuardaPedido() ) 
                            bContinua = GuardaDetallePedido();

                        if (bContinua)
                        {
                            cnn.CompletarTransaccion();
                            IniciarToolBar(false, false);
                            txtFolio.Text = sFolioPedido;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            ImprimirInformacion();
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciarToolBar(true, true);

                        }

                        cnn.Cerrar();
                    }
                    else
                    {
                        Error.LogError(cnn.MensajeError); 
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }
        #endregion Botones        

        #region Funciones

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "";

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;  
                Grid.Limpiar(true);
                Grid.EstiloGrid(eModoGrid.ModoRow);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtObservaciones.Text = "";
                txtObservaciones.Enabled = true;
                txtObservaciones.Focus();
            }
            else
            {
                sSql = string.Format(" SELECT p.FolioPedido, p.Observaciones, p.IdPersonal, p.FechaRegistro, vwp.NombreCompleto " +
                     " FROM  COM_FAR_Pedidos ( nolock ) AS p INNER JOIN vw_Personal ( nolock ) AS vwp " +
                     " ON ( p.IdPersonal = vwp.IdPersonal AND p.IdEstado = vwp.IdEstado AND p.IdFarmacia = vwp.IdFarmacia ) " +
                     " WHERE p.IdEmpresa = '{0}' AND p.IdEstado = '{1}' AND p.IdFarmacia = '{2}' AND p.FolioPedido = '{3}' AND p.IdTipoPedido = '{4}' ",
                     DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 6), sTipoPedido);

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtFolio_Validating");
                    General.msjError("Error al buscar Folio de Pedido");
                    btnNuevo_Click(this, null);
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        txtFolio.Text = myLeer.Campo("FolioPedido");
                        txtObservaciones.Text = myLeer.Campo("Observaciones");
                        txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                        lblPersonal.Text = myLeer.Campo("NombreCompleto");
                        dtpFecha.Value = myLeer.CampoFecha("FechaRegistro");

                        CargaDetallePedido();

                        sFolioPedido = txtFolio.Text;
                        IniciarToolBar(false, true);
                        txtFolio.Enabled = false;
                        Grid.BloqueaGrid(true);
                    }
                    else
                    {
                        General.msjError("El Folio de Pedido no Existe");
                        btnNuevo_Click(this, null);
                    }
                }
            }
        }

        private void IniciarToolBar(bool Guardar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool CargaDetallePedido()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" SELECT  ccs.IdClaveSSA_Sal, ccs.ClaveSSA, pc.CodigoEAN, ccs.Descripcion, pc.Cantidad_Pedido " +
                 " FROM COM_FAR_Pedidos ( nolock ) AS p " + 
                 " INNER JOIN COM_FAR_Pedidos_Productos ( nolock ) AS pc " +
                 "      ON ( p.FolioPedido = pc.FolioPedido AND p.IdTipoPedido = pc.IdTipoPedido ) " + 
                 " INNER JOIN CatClavesSSA_Sales ( nolock ) AS ccs " +
                 "      ON ( pc.IdClaveSSA = ccs.IdClaveSSA_Sal ) " + 
                 " WHERE p.IdEmpresa = '{0}' AND p.IdEstado = '{1}' AND p.IdFarmacia = '{2}' " + 
                 "  AND p.FolioPedido = '{3}' AND p.IdTipoPedido = '{4}' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                 DtGeneral.FarmaciaConectada, Fg.PonCeros(txtFolio.Text, 6), sTipoPedido);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargaDetallePedido");
                General.msjError("Error al buscar el Detalle de Pedido");
                btnNuevo_Click(this, null);
            }
            else
            {
                if (myLeer.Leer())
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);
                }
                else
                {
                    General.msjError("El Detalle de Pedido no Existe");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
            }
            return bRegresa;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Venta inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaSales();
            }

            return bRegresa;
        }

        private bool validarCapturaSales()
        {
            bool bRegresa = true;

            if (Grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (Grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= Grid.Rows; i++)
                    {
                        if (Grid.GetValue(i, (int)Cols.ClaveSSA) != "" && Grid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para la venta\n y/o capturar cantidades mayor a cero, verifique.");
            }
            return bRegresa;

        }

        private bool GuardaPedido()
        {
            bool bRegresa = true; 
            string sSql = "";
            sFolioPedido = txtFolio.Text;

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_COM_FAR_Pedidos  '{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}', '{7}', {8} ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                    Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Especial, 2), sFolioPedido,
                    Fg.PonCeros(txtIdPersonal.Text, 4), General.FechaYMD(GnFarmacia.FechaOperacionSistema),
                    txtObservaciones.Text, iTipoDePedido);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else 
            {
                if (!myLeer.Leer())
                {
                    bRegresa = false; 
                }
                else
                {
                    sFolioPedido = String.Format("{0}", myLeer.Campo("FolioPedido"));
                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                }
            }
            return bRegresa; 
        }

        private bool GuardaDetallePedido()
        {
            bool bRegresa = true; 
            string sSql = "", sIdClaveSSA_Sal = "";
            string sCodigoEAN = ""; 
            int iCant_Pedido = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA_Sal = Grid.GetValue(i, (int)Cols.IdClaveSSA );
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iCant_Pedido = Grid.GetValueInt(i, (int)Cols.Cantidad);

                if (sIdClaveSSA_Sal != "")
                {

                    //sSql = String.Format("Exec spp_Mtto_COM_FAR_Pedidos_Productos '{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}' ",
                    //    sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sIdCodigo, sCodigoEAN, iCantidad);

                    sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_COM_FAR_Pedidos_Productos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'  ",
                                         DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                                         sTipoPedido, sFolioPedido, sIdClaveSSA_Sal, sCodigoEAN, iCant_Pedido);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }
            return bRegresa; 
        }

        private void ImprimirInformacion()
        {         
            PedidosImprimir.MostrarVistaPrevia = true ;
            if (PedidosImprimir.Imprimir(sFolioPedido, sTipoPedido))
            {
                btnNuevo_Click(null, null);
            }
        } 
        #endregion Funciones        

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            // if (Grid.ActiveCol == (int)Cols.CodigoEAN )
            {
                // Habilitar la Ayuda en todo el Renglon 
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.Productos_CodigoEAN("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        Grid.SetValue(Grid.ActiveRow, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                        CargaDatosProducto();
                    }

                }
            }

            // Es posible borrar el renglon desde cualquier columna 
            if (e.KeyCode == Keys.Delete)
            {
                Grid.DeleteRow(Grid.ActiveRow);

                if (Grid.Rows == 0)
                    Grid.Limpiar(true);
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)Grid.ActiveCol;

            switch (iCol)
            {
                case Cols.CodigoEAN: 
                     ObtenerDatosProducto();
                     break;
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = Grid.GetValue(Grid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {           
            if ((Grid.ActiveRow == Grid.Rows) && e.AdvanceNext)
            {
                if (Grid.GetValue(Grid.ActiveRow, 1) != "" && Grid.GetValue(Grid.ActiveRow, 3) != "")
                {
                    Grid.Rows = Grid.Rows + 1;
                    Grid.ActiveRow = Grid.Rows;
                    Grid.SetActiveCell(Grid.Rows, 2);
                }
            }            
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= Grid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                Grid.SetValue(Grid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= Grid.Rows; i++) //Renglones.
            {
                if (Grid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    Grid.DeleteRow(i);
            }

            if (Grid.Rows == 0) // Si No existen renglones, se inserta 1.
                Grid.AddRow();
        }

        private void ObtenerDatosProducto()
        {
            string sCodigo = "";  // , sSql = "";
            // int iCantidad = 0;

            sCodigo = Grid.GetValue(Grid.ActiveRow, (int)Cols.CodigoEAN);

            //if (sCodigo.Trim() == "")
            //{
            //    // General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
            //    Grid.LimpiarRenglon(Grid.ActiveRow);
            //}
            //else
            if (sCodigo.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Productos_CodigosEAN_Datos(sCodigo, sCodigo, "ObtenerDatosProducto()"); 
                //sSql = string.Format("Exec Spp_SalesCapturaPedidosCentros '{0}', '{1}' ",
                //    Fg.PonCeros(sCodigo, 4), sCodigo);
                //if (!myLeer.Exec(sSql))
                //{
                //    Error.GrabarError(myLeer, "ObtenerDatosProducto()");
                //    General.msjError("Ocurrió un error al obtener la información del Producto.");
                //}
                //else
                {
                    if (!myLeer.Leer())
                    {
                        General.msjUser("La Clave de Producto no Existe ó no esta Asignada a la Farmacia.");
                        Grid.LimpiarRenglon(Grid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosProducto();
                    }
                }
            }
        }

        private void CargaDatosProducto()
        {
            int iRowActivo = Grid.ActiveRow;
            
            if (!Grid.BuscaRepetido(myLeer.Campo("CodigoEAN"), iRowActivo, (int)Cols.CodigoEAN))
            {
                Grid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                Grid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                Grid.SetValue(iRowActivo, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                Grid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));                
                Grid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                Grid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);               

            }
            else
            {
                General.msjUser("Este Producto ya se encuentra capturado en otro renglón.");
                Grid.SetValue(Grid.ActiveRow, (int)Cols.Cantidad, "");
                limpiarColumnas();
                Grid.SetActiveCell(Grid.ActiveRow, 2);
            }

            
        }

        #endregion Grid

        private void tmPedidos_Tick(object sender, EventArgs e)
        {
            tmPedidos.Stop();
            tmPedidos.Enabled = false; 
            if (!GnFarmacia.GeneraPedidosEspeciales)
            {
                General.msjUser("La Farmacia no esta configurada para Generar Pedidos Especiales.");
                this.Close();
            }
        }

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }
    }
}
