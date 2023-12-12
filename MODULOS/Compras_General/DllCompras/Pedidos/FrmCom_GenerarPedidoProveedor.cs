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

namespace DllCompras.Pedidos
{
    public partial class FrmCom_GenerarPedidoProveedor : FrmBaseExt
    {
        private enum Cols : int
        {
            IdClaveSSA = 1, ClaveSSA = 2, CodigoEAN = 3, DescripcionClave = 4, PrecioNeto = 5, Cantidad = 6, Importe = 7 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;       
        clsGrid grid;
        clsConsultas query;
        clsAyudas ayuda; 

        string sEstado = ""; // Se cambio a blanco para poner el del Combo.
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPersonal = DtGeneral.IdPersonal;
        string sIdProveedor = "", sFolioPedido = "", sMensaje = "";

        public FrmCom_GenerarPedidoProveedor()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdClaves, this);
            query = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name); 

            grid.EstiloGrid(eModoGrid.ModoRow);


            dtpFechaRequeridaEntrega.MinDate = General.FechaSistema;
            dtpFechaRequeridaEntrega.MaxDate = dtpFechaRequeridaEntrega.MinDate.AddMonths(1);

            CargarEstados();
        }

        private void FrmCom_GenerarPedidoProveedor_Load(object sender, EventArgs e)
        {
            //btnNuevo_Click(null, null);     
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            txtPedido.Text = "*";
            txtPedido.Enabled = false;
            txtIdFarmacia.Focus();
            grid.Limpiar(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            if (ValidaDatos())
            {
                // pho-oax-soporte.dyndns-ip.com
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (GuardarEncabezado())
                    {
                        bContinua = ActualizaCantidadesPedido();
                    }

                    if (!bContinua)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió Un Error al Generar el Pedido Al Proveedor"); 
                    }
                    else
                    {               
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje);
                        this.Close();
                        //btnNuevo_Click(null, null);
                    }
                    cnn.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Eventos 
        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    leer.DataSetClase = ayuda.AlmacenesCompras("txtIdFarmacia_Validating", sEstado); 
                }
                else
                {
                    leer.DataSetClase = ayuda.Farmacias("txtIdFarmacia_Validating", sEstado); 
                }

                if (leer.Leer())
                {
                    CargarDatosDeFarmacia(); 
                }
                else
                {
                    txtIdFarmacia.Focus();
                }
            }
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text != "")
            {
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    leer.DataSetClase = query.AlmacenesCompras(sEstado, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                }
                else
                {
                    leer.DataSetClase = query.Farmacias(sEstado, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                }


                if (leer.Leer())
                {
                    CargarDatosDeFarmacia(); 
                }
                else
                {
                    txtIdFarmacia.Focus();
                }
            } 
        }

        private void CargarDatosDeFarmacia()
        {
            txtIdFarmacia.Text = leer.Campo("IdFarmacia"); 
            lblEntregarEn.Text = leer.Campo("Farmacia"); 
            lblDomicilio.Text = leer.Campo("Domicilio") + " " + leer.Campo("Colonia") + ", " + leer.Campo("Municipio") + " " + leer.Campo("Estado"); 
        }

        private void txtIdFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblEntregarEn.Text = "";
            lblDomicilio.Text = "";
        }
        #endregion Eventos

        #region Guardar Pedido Proveedor 
        private bool GuardarEncabezado()
        {
            int iOpcion = 1; //Guardar.
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Compras '{0}', '{1}', '{2}', '{3}', " +
                        " '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ", 
                        sEmpresa, sEstado, sFarmacia, txtPedido.Text, sIdProveedor, sIdPersonal, txtIdFarmacia.Text, 
                        General.FechaYMD(dtpFechaRequeridaEntrega.Value),txtObservaciones.Text, iOpcion );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioPedido = leer.Campo("FolioPedidoProv");
                sMensaje = leer.Campo("Mensaje");

                bRegresa = GuardarDetalle();                
            }

            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true;
            string sIdClaveSSA = "", sCodigoEAN = "";
            int iCantidad_Solicitada = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdClaveSSA = grid.GetValue(i, (int)Cols.IdClaveSSA);
                sCodigoEAN = grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad_Solicitada = grid.GetValueInt(i, (int)Cols.Cantidad);

                if (sIdClaveSSA != "")
                {
                    string sSql = string.Format("Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves_Compras '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7} \n " +
                                " Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Claves_CodigosEAN_Compras '{0}', '{1}', '{2}', '{4}', '{5}', '{6}', '{7}'  ",
                                sEmpresa, sEstado, sFarmacia, sIdProveedor, sFolioPedido, sIdClaveSSA, sCodigoEAN, iCantidad_Solicitada);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool ActualizaCantidadesPedido()
        {
            bool bRegresa = true;
            string sIdClaveSSA = "", sCodigoEAN = "", sGUID = "";
            int iCantidad_Solicitada = 0;

            sGUID = GnCompras.GUID;

            for (int i = 1; i <= grid.Rows; i++)
            {
                sIdClaveSSA = grid.GetValue(i, (int)Cols.IdClaveSSA);
                sCodigoEAN = grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad_Solicitada = grid.GetValueInt(i, (int)Cols.Cantidad); 

                if (sIdClaveSSA != "")
                {
                    string sSql = string.Format("Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Actualiza_Cantidades '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                sEmpresa, sEstado, sFarmacia, sIdPersonal, sIdClaveSSA, sCodigoEAN, sIdProveedor, iCantidad_Solicitada, sGUID);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        #endregion Guardar Pedido Proveedor

        #region Funciones
        public void MostrarPedidoParaProveedor(string IdProveedor, string sNomProv)
        {
            string sTabla = "";
            string sSql = "";

            sTabla = GnCompras.GUID;

            sSql = string.Format(" Select  IdClaveSSA, ClaveSSA, CodigoEAN, DescripcionClave, Precio, Cant_A_Pedir " +
                                " From {0} ( NOLOCK ) " +
                                " Where IdProveedor = '{1}' Order By DescripcionClave ", sTabla, IdProveedor);
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al Obtener las Claves del Proveedor.");
            }
            else
            {
                if (leer.Leer())
                {
                    InicializaToolBar(false, true, false, false);
                    grid.LlenarGrid(leer.DataSetClase);
                    lblNomProv.Text = sNomProv;
                    sIdProveedor = IdProveedor;

                    txtPedido.Text = "*";
                    txtPedido.Enabled = false;
                    txtIdFarmacia.Focus();
                    this.ShowDialog();
                }
                else
                {
                    General.msjUser("No se han Asignado Claves-CodigoEAN al Proveedor.");
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (lblEntregarEn.Text.Trim().Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Farmacia donde se entregará el pedido, verifique.");
                txtIdFarmacia.Focus();
            }
            
            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturuado las Observaciones para el pedido, verifique.");
                txtObservaciones.Focus();
            }

            return bRegresa;
        }

        private void InicializaToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(query.EstadosConFarmacias("CargarEstados"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Regional)
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false;
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            sEstado = cboEstados.Data;
            txtIdFarmacia.Text = "";
        }

        #endregion Funciones        

    }
}
