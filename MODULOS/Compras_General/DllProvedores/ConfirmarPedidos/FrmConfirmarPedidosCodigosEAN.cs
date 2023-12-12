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
    public partial class FrmConfirmarPedidosCodigosEAN : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdProducto = 1, CodigoEAN = 2, Descripcion = 3, PrecioUnitario = 4, CantidadRequerida = 5, CantidadSurtible = 6
        }

        DllProveedores.wsProveedores.wsCnnProveedores ConexionWeb;
        clsLeer leerLocal;
        clsLeerWeb leer = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsLeerWeb leerDetalles = new clsLeerWeb(General.Url, GnProveedores.DatosDelCliente);
        clsGrid Grid;
        clsDatosCliente DatosCliente;
        clsConfirmarPedidoContado Confirmar;
        bool bModuloProveedor = false; //Esta variable se utiliza para saber si la opcion fue llamada por otra opcion del modulo de Proveedores.

        public FrmConfirmarPedidosCodigosEAN()
        {
            InitializeComponent();

            ConexionWeb = new DllProveedores.wsProveedores.wsCnnProveedores();
            ConexionWeb.Url = General.Url;
            DatosCliente = new clsDatosCliente(GnProveedores.DatosApp, this.Name, "");
            Confirmar = new clsConfirmarPedidoContado();
            leerLocal = new clsLeer();

            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.Normal);
        }

        private void FrmConfirmarPedidosCodigosEAN_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

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
                            IniciaToolBar(false, true, false, false);
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
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            lblEntregarEn.Text = leer.Campo("EntregarEnNombre");
            dtpFechaRequeridaEntrega.Value = leer.CampoFecha("FechaRequeridaEntrega");
            dtpFechaPromesaEntrega.Value = leer.CampoFecha("FechaPromesaEntrega");
            txtObservaciones.Text = leer.Campo("ObservacionesProv");
            lblIdEmpresa.Text = leer.Campo("IdEmpresa");
            lblIdEstado.Text = leer.Campo("IdEstado");
        }

        private void LlenarDetalles()
        {

            string sSql = String.Format("Select IdProducto, CodigoEAN, Descripcion , Precio, Cantidad_Solicitada, Cantidad_Confirmada  " + 
                " From vw_COM_OCEN_Pedidos_Proveedor_Contado(NoLock) " +
                " Where IdProveedor = '{0}' And Folio = '{1}' ", GnProveedores.IdProveedor, txtPedido.Text.Trim());

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

            dtpFechaRegistro.Enabled = false;
            dtpFechaRequeridaEntrega.Enabled = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataSet dtsInformacionWeb = new DataSet("dtsInformacionWeb");
            DataSet dtsInformacionCliente = new DataSet("dtsInformacionCliente");
            string sProducto = "", sCodigoEAN = "", sIdEmpresa = lblIdEmpresa.Text, sIdEstado = lblIdEstado.Text;
            string SPEncabezado = "spp_Mtto_COM_OCEN_Pedidos_Proveedor";
            string SPDetalles = "spp_Mtto_COM_OCEN_Pedidos_Proveedor_Contado";
            int iCantidadConfirmada = 0;
            int iTipo = 1; // Tipo 1 = Contado. Tipo 2 = Credito.

            DatosCliente.Funcion = "btnGuardar_Click()";
            
            //Se guarda el Encabezado
            Confirmar.AgregarRenglonEncabezado(SPEncabezado, sIdEmpresa, sIdEstado, txtPedido.Text.Trim(), dtpFechaPromesaEntrega.Text, txtObservaciones.Text.Trim());

            // Se guardan los Detalles de la venta.
            for (int i = 1; i <= Grid.Rows; i++)
            {
                sProducto = Grid.GetValue(i, (int)Cols.IdProducto);
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidadConfirmada = Grid.GetValueInt(i, (int)Cols.CantidadSurtible);

                Confirmar.AgregarRenglonDetalles(SPDetalles, sIdEmpresa, sIdEstado, txtPedido.Text, sProducto, sCodigoEAN, iCantidadConfirmada.ToString());
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

        public void MostrarDetalle( string sPedido )
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
        private void grdClaves_EditModeOff(object sender, EventArgs e)
        {
            int iRow = Grid.ActiveRow;
            int iCantidadRequerida = Grid.GetValueInt(iRow, (int)Cols.CantidadRequerida);
            int iCantidadSurtible = Grid.GetValueInt(iRow, (int)Cols.CantidadSurtible);

            if (iCantidadSurtible > iCantidadRequerida)
            {
                Grid.SetValue(iRow, (int)Cols.CantidadSurtible, iCantidadRequerida);
                General.msjUser("La cantidad surtible no puede ser mayor a la cantidad solicitada, verifique.");
            }

        }

        #endregion Grid

    }
}
