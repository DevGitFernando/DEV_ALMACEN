using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;

namespace DllCompras.OrdenCompra
{
    public partial class FrmCom_GenerarOrdenDeCompraProveedor : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerDetalles;
        clsGrid Grid;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdProveedor = "";
        string sMensaje = "";

        public FrmCom_GenerarOrdenDeCompraProveedor()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            leerDetalles = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdClaves, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true; 
        }

        private void FrmCom_GenerarOrdenDeCompraProveedor_Load(object sender, EventArgs e)
        {

        }

        #region Buscar Pedido
        private void BuscarPedido()
        {
            if (lblPedido.Text.Trim() != "")
            {
                string sSql = String.Format("Set Dateformat YMD Select * From vw_COM_OCEN_Pedidos_Proveedor(NoLock) " +
                    " Where IdProveedor = '{0}' And Folio = '{1}' ", sIdProveedor, lblPedido.Text.Trim());

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "BuscarPedido()");
                    General.msjError("Ocurrió un error al obtener los datos del Pedido");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjUser("Pedido capturado no encontrado, verifique.");
                    }
                    else 
                    {
                        txtFolio.Enabled = false;
                        LlenaDatos();
                        LlenarDetalles();
                        BloqueaRegion();
                        IniciaToolBar(false, true, true, false);
                    }
                }
            }
        }

        private void LlenaDatos()
        {
            lblPedido.Text = leer.Campo("Folio");
            //dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            txtEntregarEn.Text = leer.Campo("EntregarEn");
            lblEntregarEn.Text = leer.Campo("EntregarEnNombre");
            dtpFechaRequeridaEntrega.Value = leer.CampoFecha("FechaRequeridaEntrega");
            dtpFechaPromesaEntrega.Value = leer.CampoFecha("FechaPromesaEntrega");
        }

        private void LlenarDetalles()
        {

            string sSql = String.Format("Select IdClaveSSA, ClaveSSA, CodigoEAN, DescripcionSal , Precio, Cantidad_Solicitada_CodigoEAN, " + 
                " Cantidad_Confirmada_CodigoEAN, ( Precio * Cantidad_Confirmada_CodigoEAN ) as Importe  " +
                " From vw_COM_OCEN_Pedidos_Proveedor_Claves_CodigosEAN(NoLock) " +
                " Where IdProveedor = '{0}' And Folio = '{1}' ", sIdProveedor, lblPedido.Text.Trim());

            if (!leerDetalles.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error las Claves del Pedido");
            }
            else
            {
                if (!leerDetalles.Leer())
                {
                    General.msjUser("El Pedido capturado no contiene detalles, verifique");
                }
                else  
                {
                    Grid.LlenarGrid(leerDetalles.DataSetClase);
                }
            }

        }
        #endregion Buscar Pedido

        #region Buscar Orden de Compra 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
        }
        #endregion Buscar Orden de Compra 

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int iOpcion = 1;//La opcion 1 significa que se va a GUARDAR
            if (txtFolio.Text.Trim() != "")
            {
                if (ValidaDatos())
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        if (Generar_OrdenDeCompra(iOpcion))
                        {
                            cnn.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciaToolBar(false, false, false, true);
                            btnImprimir_Click(null, null);
                            this.Hide();
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciaToolBar(false, true, true, false);

                        }

                        cnn.Cerrar();

                    }
                    else
                    {
                        General.msjUser(General.MsjErrorAbrirConexion );
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            int iOpcion = 2;//La opcion 2 significa que se va a CANCELAR
            if (txtFolio.Text.Trim() != "")
            {
                if (ValidaDatos())
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        if (Cancelar_PrePedido(iOpcion))
                        {
                            cnn.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciaToolBar(false, false, false, true);
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnCancelar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciaToolBar(false, true, true, false);

                        }

                        cnn.Cerrar();

                    }
                    else
                    {
                        General.msjUser(General.MsjErrorAbrirConexion);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones 
        private bool Generar_OrdenDeCompra(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = String.Format("Exec spp_Mtto_COM_OCEN_Ordenes_Compra '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                        sEmpresa, sEstado, sFarmacia, txtFolio.Text, lblPedido.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text, iOpcion);

            if (leer.Exec(sSql))
            {
                leer.Leer();
                txtFolio.Text = leer.Campo("Folio");
                sMensaje = leer.Campo("Mensaje");
            }
            else
            {
                bRegresa = false;
            }
            return bRegresa;
        }

        private bool Cancelar_PrePedido(int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = String.Format("Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Compras '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '', '', '{6}', '{7}'",
                        sEmpresa, sEstado, sFarmacia, lblPedido.Text, sIdProveedor, DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), iOpcion);

            if (leer.Exec(sSql))
            {
                leer.Leer();
                txtFolio.Text = leer.Campo("Folio");
                sMensaje = leer.Campo("Mensaje");
            }
            else
            {
                bRegresa = false;
            }
            return bRegresa;
        }
        
        public void MostrarOrdenDeCompraProveedor(string IdProveedor, string FolioPedido)
        {
            Fg.IniciaControles();
            txtFolio.Text = "*";
            lblPedido.Text = FolioPedido;
            sIdProveedor = IdProveedor;

            BuscarPedido();
            this.ShowDialog(); 
        }

        private void IniciaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private void BloqueaRegion()
        {
            txtFolio.Enabled = false;
            txtEntregarEn.Enabled = false;
            dtpFechaRegistro.Enabled = false;
            dtpFechaRequeridaEntrega.Enabled = false;
            dtpFechaPromesaEntrega.Enabled = false;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones, verifique.");
                txtObservaciones.Focus();
            }
            return bRegresa;
        }

        #endregion Funciones 

        

        
        
    }
}
