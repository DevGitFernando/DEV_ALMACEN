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

namespace DllCompras.PedidosCentral
{
    public partial class FrmGeneraPedidoACentral : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, 
            CajaCon = 4, Cantidad = 5, CantidadSurtida = 6, CantidadCajas = 7 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;

        string sFolioPedido = "";
        string sMsj = ""; 
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sNomPersonal = DtGeneral.NombrePersonal;
        string sFechaSistema = General.FechaYMD(GnCompras.FechaOperacionSistema, "-");

        public FrmGeneraPedidoACentral()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn); 

            grdProductos.EditModeReplace = true;
            grid = new clsGrid(ref grdProductos, this);
            grid.EstiloGrid(eModoGrid.ModoRow); 
        }

        private void FrmGenerarPedidosCentral_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones  
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Ejecutar, bool Status)
        {
            btnNuevo.Enabled = Nuevo; 
            btnEjecutar.Enabled = Ejecutar; 
            btnGuardar.Enabled = Guardar; 
            btnStatus.Enabled = Status;

            // btnEjecutar.Enabled = false;
            btnStatus.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            sFolioPedido = ""; 
            IniciarToolBar(true, false, false, false);
            grid.Limpiar(false);

            txtFolio.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarProductosParaPedido(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (!GenerarPedidoCentral())
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "GenerarPedidoCentral");
                        General.msjError("Ocurrió un error al generar el pedido."); 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(string.Format("{0}", sMsj) );

                        txtFolio.Enabled = false; 
                        txtFolio.Text = sFolioPedido;
                        CargarPedido();
                    }
                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion); 
                }
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Eventos 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false; 
                txtFolio.Text = "*";
                IniciarToolBar(true, false, true, false);
                CargarProductosParaPedido(); 
            }
            else
            {
                // IniciarToolBar(true, true, true, false);
                CargarPedido(); 
            }
        }
        #endregion Eventos

        #region Funciones y Procedimientos 
        private bool GenerarPedidoCentral()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Exec spp_COM_GeneraPedido_Concentrado_OCEN '{0}', '{1}', '{2}', '{3}', '{4}' ", 
                sEmpresa, sEstado, sFarmacia, DtGeneral.IdPersonal, txtObservaciones.Text.Trim());

            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else 
            {
                if (leer.Leer())
                {
                    sFolioPedido = leer.Campo("FolioDePedidoRegional");
                    sMsj = leer.Campo("Mensaje"); 
                }
            }

            return bRegresa; 
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para el Pedido, verifique. "); 
                txtObservaciones.Focus();
            }

            return bRegresa; 
        }

        private void CargarPedido()
        {
            string sSql = string.Format(" Select * " + 
                " From vw_COM_REG_PedidosEnc D (NoLock)  " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarProductosParaPedido");
                General.msjError("Ocurrió un error al obtener la información del Pedido.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información para el Folio especificado, verifique.");
                }
                else
                {
                    IniciarToolBar(true, false, false, false);

                    txtFolio.Enabled = false;
                    txtFolio.Text = leer.Campo("Folio");
                    txtObservaciones.Text = leer.Campo("Observaciones");
                    dtpFechaPedido.Value = leer.CampoFecha("FechaRegistro"); 

                    CargarDetallesDePedido();
                    txtObservaciones.Enabled = false;
                    dtpFechaPedido.Enabled = false;
                }
            }
        }

        private void CargarDetallesDePedido()
        {
            string sSql = string.Format("    Select " +
                "   P.IdClaveSSA, P.ClaveSSA, P.DescripcionSal, P.ContenidoPaquete,  " +
                "   P.Cantidad, P.Cantidad,  " +
                "   P.Cantidad as Cajas " +
                " From vw_COM_REG_Pedidos_Claves P (NoLock)  " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));

            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarProductosParaPedido");
                General.msjError("Ocurrió un error al obtener los detalles de Pedido.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
            }
        }

        private void CargarProductosParaPedido()
        {
            string sSql = string.Format("    Select D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.ContenidoPaquete, " +  
            " D.Cantidad_EnviadaCentral, D.Cantidad_EnviadaCentral, D.Cantidad_EnviadaCentral as Cajas " +  
            " From COM_OCEN_Pedidos E (NoLock) " +
            " Inner Join COM_OCEN_PedidosDet_Claves D (Nolock) " + 
                " On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia " + 
                " and E.IdTipoPedido = D.IdTipoPedido and E.FolioPedido =  D.FolioPedido ) " +
            " Inner Join vw_ClavesSSA_Sales P (NoLock) " +   
                " On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) " +  
            " Where D.IdEmpresa = '{0}' and D.IdEstado = '{1}' and D.Actualizado = 3 and E.IdPersonal = '{2}' ", 
            sEmpresa, sEstado, sPersonal);

            grid.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarProductosParaPedido");
                General.msjError("Ocurrió un error al obtener la lista de Productos para la Generación de Pedidos."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontrarón Productos para generar pedidos, verifique."); 
                }
                else
                {
                    IniciarToolBar(true, true, false, false);
                    grid.LlenarGrid(leer.DataSetClase); 
                }
            }
        }
        #endregion Funciones y Procedimientos

    }
}
