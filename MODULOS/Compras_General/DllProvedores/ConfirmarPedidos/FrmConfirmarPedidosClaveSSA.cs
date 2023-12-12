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
using DllProveedores.Clases;
using DllProveedores.wsProveedores; 

namespace DllProveedores.ConfirmarPedidos
{
    public partial class FrmConfirmarPedidosClaveSSA : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, PrecioUnitario = 4, CantidadRequerida = 5, CantidadSurtible = 6
        }

        private enum ColsEAN
        {
            IdClaveSSA = 0, CodigoEAN = 1, Descripcion = 2, Cantidad = 3
        }

        DllProveedores.wsProveedores.wsCnnProveedores ConexionWeb;
        clsLeer leerLocal;
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeerWeb leerDetalles = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrid Grid;
        clsDatosCliente DatosCliente;
        clsConfirmarPedidoCredito Confirmar;
        clsConfirmarCodigosEAN CapturarEAN;
        DataTable dtTablaEAN = new DataTable("CodigosEAN");

        // Oficinas Regionales y Central 
        string sFarmacia = "0001"; 

        bool bModuloProveedor = false; //Esta variable se utiliza para saber si la opcion fue llamada por otra opcion del modulo de Proveedores.

        public FrmConfirmarPedidosClaveSSA()
        {
            InitializeComponent();

            ConexionWeb = new DllProveedores.wsProveedores.wsCnnProveedores();
            ConexionWeb.Url = General.Url;
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
            Confirmar = new clsConfirmarPedidoCredito();
            CapturarEAN = new clsConfirmarCodigosEAN();
            leerLocal = new clsLeer();

            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.Normal);
        }

        private void FrmConfirmarPedidosClaveSSA_Load(object sender, EventArgs e)
        {
            CrearTabla();
            btnNuevo_Click(null, null);
        }

        #region Crear Tabla
        private void CrearTabla()
        {
            //Se agregan las columnas a la tabla CodigosEAN
            dtTablaEAN.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("CodigoEAN", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("DescripcionProducto", Type.GetType("System.String"));
            dtTablaEAN.Columns.Add("Cantidad", Type.GetType("System.String"));
        }
        #endregion Crear Tabla 

        #region Buscar Pedido
        private void txtPedido_Validating(object sender, CancelEventArgs e)
        {
            if (txtPedido.Text.Trim() != "")
            {
                string sSql = String.Format("Set Dateformat YMD Select * From vw_COM_OCEN_Pedidos_Proveedor(NoLock) " +
                    " Where IdProveedor = '{0}' And Folio = '{1}' ", GnProveedores.IdProveedor, txtPedido.Text.Trim());

                if (!leer.Exec(sSql))
                {
                    // Error.GrabarError(leer, "");
                    General.msjError("Ocurrió un error al obtener los datos del Pedido");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtPedido.Enabled = false;
                        if (bModuloProveedor)
                            IniciaToolBar(false, true, true, false);
                        else
                            IniciaToolBar(true, false, false, false);

                        LlenaDatos();
                        LlenarDetalles();

                        BloqueaRegion();
                    }
                    else
                    {
                        General.msjUser("El Pedido ingresado no existe");
                    }
                }
            }
        }

        private void LlenaDatos()
        {
            txtPedido.Text = leer.Campo("Folio");
            lblEntregarEn.Text = leer.Campo("EntregarEnNombre");
            dtpFechaRequeridaEntrega.Value = leer.CampoFecha("FechaRequeridaEntrega");
            txtObservacionesComprador.Text = leer.Campo("Observaciones");
            txtObservaciones.Text = leer.Campo("ObservacionesProv");
            lblIdEmpresa.Text = leer.Campo("IdEmpresa");
            lblIdEstado.Text = leer.Campo("IdEstado");

            //Se pone la fecha maxima de entrega.
            dtpFechaPromesaEntrega.MaxDate = dtpFechaRequeridaEntrega.Value.AddDays(GnProveedores.DiasPlazoMaximoEntregaPedido);
        }

        private void LlenarDetalles()
        {

            string sSql = String.Format("Select IdClaveSSA, ClaveSSA, DescripcionSal , Precio, Cantidad_Solicitada, Cantidad_Confirmada  " +
                " From vw_COM_OCEN_Pedidos_Proveedor_Claves(NoLock) " +
                " Where IdProveedor = '{0}' And Folio = '{1}' And Cantidad_Solicitada > 0 ", GnProveedores.IdProveedor, txtPedido.Text.Trim());

            if (!leerDetalles.Exec(sSql))
            {
                // Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error los datos del Pedido");
            }
            else
            {
                if (leerDetalles.Leer())
                {
                    Grid.LlenarGrid(leerDetalles.DataSetClase);
                }
                else
                {
                    General.msjUser("El Pedido ingresado no contiene detalles. Verifique");
                }
            }

        }
        #endregion Buscar Pedido

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!bModuloProveedor)
            {
                lblIdEmpresa.Text = "";
                lblIdEstado.Text = "";
                Fg.IniciaControles(true);
                Grid.Limpiar();
                IniciaToolBar(true, false, false, false);
                txtPedido.Focus();
            }
            else
            {
                CapturarEAN = new clsConfirmarCodigosEAN();
            }

            dtpFechaRegistro.Enabled = false;
            dtpFechaRequeridaEntrega.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataSet dtsInformacionWeb = new DataSet("dtsInformacionWeb");
            DataSet dtsInformacionCliente = new DataSet("dtsInformacionCliente");
            string sIdClaveSSA = "", sClaveSSA = "", sCodigoEAN = ""; 
            string sIdEmpresa = lblIdEmpresa.Text, sIdEstado = lblIdEstado.Text;
            string SPEncabezado = "spp_Mtto_COM_OCEN_Pedidos_Proveedor";
            string SPDetalles = "spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves";
            string SPCodigosEAN = "spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves_CodigosEAN";
            string sOpcion = "1"; //Guardar
            int iCantidadConfirmada = 0;
            int iTipo = 2; // Tipo 1 = Contado. Tipo 2 = Credito.

            DatosCliente.Funcion = "btnGuardar_Click()";

            //Se guarda el Encabezado
            Confirmar.AgregarRenglonEncabezado(SPEncabezado, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text.Trim(), dtpFechaPromesaEntrega.Text, txtObservaciones.Text.Trim(), sOpcion);

            // Se guardan los Detalles del Pedido.
            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = Grid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidadConfirmada = Grid.GetValueInt(i, (int)Cols.CantidadSurtible);

                Confirmar.AgregarRenglonDetalles(SPDetalles, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text, sIdClaveSSA, sClaveSSA, iCantidadConfirmada.ToString());
            }

            // Se guardan los CodigosEAN del Pedido
            sIdClaveSSA = ""; sCodigoEAN = ""; iCantidadConfirmada = 0;
            foreach (DataRow dtRow in dtTablaEAN.Select())
            {
                sIdClaveSSA = dtRow[(int)ColsEAN.IdClaveSSA].ToString();
                sCodigoEAN = dtRow[(int)ColsEAN.CodigoEAN].ToString();
                iCantidadConfirmada = Convert.ToInt32( dtRow[(int)ColsEAN.Cantidad].ToString() );

                Confirmar.AgregarRenglonCodigosEAN(SPCodigosEAN, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text, sIdClaveSSA, sCodigoEAN, iCantidadConfirmada.ToString());
            }

            //Se obtiene el dataset que contiene el Encabezado y el Detalle.
            dtsInformacionWeb = Confirmar.ObtenerInformacionWeb();

            //Se obtiene la informacion del Cliente
            dtsInformacionCliente = GnProveedores.DatosDelCliente.DatosCliente();

            //Se ejecuta el metodo web que confirma el pedido.
            leerLocal.DataSetClase = ConexionWeb.ConfirmarPedidoProveedor(dtsInformacionCliente, dtsInformacionWeb, iTipo);


            //Se verifican que no haya ocurrido errores.
            if (leerLocal.SeEncontraronErrores(leerLocal.DataSetClase))
            {
                General.msjUser("Ocurrió un error al guardar la información.");
            }
            else
            {
                General.msjUser("La información se guardó exitosamente.");
                btnNuevo_Click(null, null);
                this.Hide();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DataSet dtsInformacionWeb = new DataSet("dtsInformacionWeb");
            DataSet dtsInformacionCliente = new DataSet("dtsInformacionCliente");
            string sIdClaveSSA = "", sClaveSSA = "", sCodigoEAN = "";
            string sIdEmpresa = lblIdEmpresa.Text, sIdEstado = lblIdEstado.Text;
            string SPEncabezado = "spp_Mtto_COM_OCEN_Pedidos_Proveedor";
            string SPDetalles = "spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves";
            string SPCodigosEAN = "spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves_CodigosEAN";
            string sOpcion = "2"; //Cancelar
            int iCantidadConfirmada = 0;
            int iTipo = 2; // Tipo 1 = Contado. Tipo 2 = Credito.

            DatosCliente.Funcion = "btnGuardar_Click()";

            //Se guarda el Encabezado
            Confirmar.AgregarRenglonEncabezado(SPEncabezado, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text.Trim(), dtpFechaPromesaEntrega.Text, txtObservaciones.Text.Trim(), sOpcion);

            // Se guardan los Detalles del Pedido.
            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = Grid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidadConfirmada = Grid.GetValueInt(i, (int)Cols.CantidadSurtible);

                Confirmar.AgregarRenglonDetalles(SPDetalles, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text, sIdClaveSSA, sClaveSSA, iCantidadConfirmada.ToString());
            }

            // Se guardan los CodigosEAN del Pedido
            sIdClaveSSA = ""; sCodigoEAN = ""; iCantidadConfirmada = 0;
            foreach (DataRow dtRow in dtTablaEAN.Select())
            {
                sIdClaveSSA = dtRow[(int)ColsEAN.IdClaveSSA].ToString();
                sCodigoEAN = dtRow[(int)ColsEAN.CodigoEAN].ToString();
                iCantidadConfirmada = Convert.ToInt32(dtRow[(int)ColsEAN.Cantidad].ToString());

                Confirmar.AgregarRenglonCodigosEAN(SPCodigosEAN, sIdEmpresa, sIdEstado, sFarmacia, txtPedido.Text, sIdClaveSSA, sCodigoEAN, iCantidadConfirmada.ToString());
            }

            //Se obtiene el dataset que contiene el Encabezado y el Detalle.
            dtsInformacionWeb = Confirmar.ObtenerInformacionWeb();

            //Se obtiene la informacion del Cliente
            dtsInformacionCliente = GnProveedores.DatosDelCliente.DatosCliente();

            //Se ejecuta el metodo web que confirma el pedido.
            leerLocal.DataSetClase = ConexionWeb.ConfirmarPedidoProveedor(dtsInformacionCliente, dtsInformacionWeb, iTipo);


            //Se verifican que no haya ocurrido errores.
            if (leerLocal.SeEncontraronErrores(leerLocal.DataSetClase))
            {
                General.msjUser("Ocurrió un error al guardar la información.");
            }
            else
            {
                General.msjUser("La información se cancelo exitosamente.");
                btnNuevo_Click(null, null);
                this.Hide();
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        public void MostrarDetalle(string sPedido)
        {
            bModuloProveedor = true;
            txtPedido.Text = sPedido;
            txtPedido_Validating(null, null);
            this.ShowDialog();
        }

        private void BloqueaRegion()
        {
            //Si esta opcion fue llamada por la opcion Listado de Pedidos para confirmar entonces solo bloquea algunos controles del encabezado.
            //En caso contrario bloquea toda la pantalla.
            if (bModuloProveedor)
            {
                txtPedido.Enabled = false;
                dtpFechaRegistro.Enabled = false;
                dtpFechaRequeridaEntrega.Enabled = false;
            }
            else
            {
                Fg.BloqueaControles(this, false);
            }
        }
        #endregion Funciones

        #region Grid
        private void grdClaves_KeyDown(object sender, KeyEventArgs e)
        {
            string sIdEmpresa = lblIdEmpresa.Text.Trim(), sIdEstado = lblIdEstado.Text.Trim();
            string sIdClaveSSA = "", sDescripcion = "";
            int iRenglon = Grid.ActiveRow, iCantidadRequerida = 0;

            if (e.KeyCode == Keys.F5)
            {
                sIdClaveSSA = Grid.GetValue(iRenglon, (int)Cols.IdClaveSSA);
                sDescripcion = Grid.GetValue(iRenglon, (int)Cols.Descripcion);
                iCantidadRequerida = Grid.GetValueInt(iRenglon, (int)Cols.CantidadRequerida);

                CapturarEAN.MostrarPantalla(sIdEmpresa, sIdEstado, sIdClaveSSA, sDescripcion, iCantidadRequerida, txtPedido.Text.Trim());
                Grid.SetValue(iRenglon, (int)Cols.CantidadSurtible, CapturarEAN.CantidadSurtible);
                dtTablaEAN = CapturarEAN.ObtenerTablaEAN();
            }
            
        }
        #endregion Grid

    }
}
