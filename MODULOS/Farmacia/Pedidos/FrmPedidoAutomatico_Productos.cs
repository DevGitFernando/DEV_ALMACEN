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

namespace Farmacia.Pedidos
{
    public partial class FrmPedidoAutomatico_Productos : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            Codigo = 1, ClaveSSA = 2, Descripcion = 3, CodigoEAN = 4, Cantidad = 5
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid Grid;

        clsImprimirPedidos PedidosImprimir;
        clsDatosCliente DatosCliente;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sNomPersonal = DtGeneral.NombrePersonal;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolioPedido = "", sTipoPedido = "";  //Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Credito, 2); 
        string sMensaje = "";
        public bool bPedidoGuardado = false;
        public bool bOpcionExterna = false; //Esta variable se utiliza para saber si la opcion fue llamada por otra pantalla.


        public FrmPedidoAutomatico_Productos()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            PedidosImprimir = new clsImprimirPedidos(General.DatosConexion, DatosCliente,
               sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReportePedido.Credito);

            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            Grid.Limpiar(false);

            sTipoPedido = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Automatico, 2); 
        }

        private void FrmPedidos_Productos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Buscar Pedido
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text != "")
            {
                string sSql = String.Format(" Select IdClaveSSA, ClaveSSA, DescripcionSal, CodigoEAN, Cantidad, Folio, FechaRegistro, Observaciones " +
                    " From vw_COM_FAR_Pedidos_Productos (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdTipoPedido = '{3}' And Folio = '{4}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, Fg.PonCeros(txtFolio.Text.Trim(),6) );

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        Grid.LlenarGrid(myLeer.DataSetClase);
                        txtFolio.Text = myLeer.Campo("Folio");
                        dtpFechaFinal.Value = myLeer.CampoFecha("FechaRegistro");
                        dtpFechaInicial.Value = dtpFechaFinal.Value.AddDays(-2);
                        txtObservaciones.Text = myLeer.Campo("Observaciones");

                        IniciarToolBar(false, false, true);
                        txtFolio.Enabled = false;
                        txtObservaciones.Enabled = false;
                    }
                    else
                    {
                        General.msjUser("El Folio ingresado no existe");
                        txtFolio.Focus();
                    }
                }
            }
        }
        #endregion Buscar Pedido

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, false, false);

            //dtpFechaInicial.Value = dtpFechaFinal.Value.AddMonths(-3); //Esta linea es solo de prueba.
            dtpFechaFinal.Value = General.FechaSistemaObtener();
            dtpFechaInicial.Value = dtpFechaFinal.Value.AddDays(-2);
            dtpFechaInicial.Enabled = false;
            dtpFechaFinal.Enabled = false;
            Grid.Limpiar(false);

            if (bOpcionExterna)
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciarToolBar(false, true, false);
            }                

            txtFolio.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = string.Format("Exec spp_COM_PFAR_SalesPedidos '{0}', '{1}', '{2}', '{3}', '{4}'",
                sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            Grid.Limpiar(false);
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "");
                General.msjError("Ocurrió un error al obtener los productos");
            }
            else
            {
                if (!myLeer.Leer())
                {
                    General.msjUser("No existe información para mostrar");
                }
                else
                {
                    Grid.LlenarGrid(myLeer.DataSetClase);

                    if (bOpcionExterna)
                        IniciarToolBar(true, false, true);

                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }
        #endregion Botones

        #region Guardar Pedido
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string message = "¿ Desea realizar el pedido ?";

            bPedidoGuardado = false;
            if (ValidaDatos())
            {
                if (General.msjConfirmar(message) == DialogResult.Yes)
                {
                    if (cnn.Abrir())
                    {
                        cnn.IniciarTransaccion();

                        if (GuardarEncabezado())
                            bContinua = GuardarDetalle();

                        if (bContinua)
                        {
                            cnn.CompletarTransaccion();
                            sFolioPedido = txtFolio.Text;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            ImprimirInformacion();
                            //btnNuevo_Click(null, null);
                            bPedidoGuardado = true;

                            if (bOpcionExterna)
                                this.Hide();
                        }
                        else
                        {
                            cnn.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
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

        private bool GuardarEncabezado()
        {
            bool bRegresa = true;

            string sSql = String.Format("Exec spp_Mtto_COM_FAR_Pedidos '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' ",
                sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sPersonal, sFechaSistema, txtObservaciones.Text );

            if (!myLeer.Exec(sSql))
                bRegresa = false;
            else
            {
                if (myLeer.Leer())
                {
                    txtFolio.Text = String.Format("{0}", myLeer.Campo("FolioPedido"));
                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                }
                else
                    bRegresa = false;
            }

            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true;
            string sSql = "", sIdCodigo = "", sCodigoEAN = "";
            int iCantidad = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdCodigo = Grid.GetValue(i, (int)Cols.Codigo);
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);

                sSql = String.Format("Exec spp_Mtto_COM_FAR_Pedidos_Productos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sIdCodigo, sCodigoEAN, iCantidad);

                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
                
            }

            return bRegresa;
        }
        #endregion Guardar Pedido

        #region Funciones
        public void MostrarPantalla()
        {
            bOpcionExterna = true;
            bPedidoGuardado = false;
            this.ShowDialog();
        }

        private void IniciarToolBar(bool Guardar, bool Ejecutar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Ingrese las Observaciones por favor");
                txtObservaciones.Focus();
            }

            return bRegresa;
        }

        private void ImprimirInformacion()
        {                  
            PedidosImprimir.MostrarVistaPrevia = true;
            if (PedidosImprimir.Imprimir(sFolioPedido, sTipoPedido))
            {
                btnNuevo_Click(null, null);
            }
        }
        #endregion Funciones

    }
}
