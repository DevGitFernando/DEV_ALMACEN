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

namespace DllCompras.Pedidos
{
    public partial class FrmCom_ConfirmarPedido : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, CodigoEAN = 4, CajaCon = 5, Cantidad = 6, CantidadSurtida = 7
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid Grid;

        // clsImprimirPedidos PedidosImprimir;
        clsDatosCliente DatosCliente;

        string sEmpresa = "";//DtGeneral.EmpresaConectada;
        string sEstado = "";//DtGeneral.EstadoConectado;
        string sFarmacia = "";//DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sNomPersonal = DtGeneral.NombrePersonal;
        string sFechaSistema = General.FechaYMD(GnCompras.FechaOperacionSistema, "-");
        string sFolioPedido = "", sTipoPedido = "";  //Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Credito, 2); 
        string sMensaje = "";
        public bool bPedidoGuardado = false;
        public bool bOpcionExterna = false; //Esta variable se utiliza para saber si la opcion fue llamada por otra pantalla.


        public FrmCom_ConfirmarPedido()
        {
            InitializeComponent();
            myLeer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
            //PedidosImprimir = new clsImprimirPedidos(General.DatosConexion, DatosCliente,
            //   sEmpresa, sEstado, sFarmacia, General.Url, GnCompras.RutaReportes, TipoReportePedido.Credito);

            grdProductos.EditModeReplace = true; 
            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow); 
            Grid.Limpiar(false);

            //sTipoPedido = Fg.PonCeros((int)TipoPedidosFarmacia.Pedido_Credito, 2);
        }

        private void FrmCom_ConfirmarPedidoCredito_Load(object sender, EventArgs e)
        {
            //btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, false, false, true);
            Grid.Limpiar(false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            ValidaStatusPedido();
        }
        #endregion Botones

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text != "")
            {
                string sSql = String.Format(" Select IdClaveSSA, ClaveSSA, DescripcionSal, " +
                    " CodigoEAN, ContenidoPaquete, Cantidad, CantidadSurtir, Folio, FechaRegistro, Observaciones, " + 
                    " (IdFarmacia + ' - ' + Farmacia) as Farmacia, FechaRegistro as FechaPedido " + 
                    " From vw_COM_REG_Pedidos_Productos (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdTipoPedido = '{3}' And Folio = '{4}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, Fg.PonCeros(txtFolio.Text.Trim(), 6));

                if (!myLeer.Exec(sSql))
                {
                    Error.GrabarError(myLeer, "txtFolio_Validating"); 
                }
                else 
                {
                    if (myLeer.Leer())
                    {
                        lblFarmacia.Text = myLeer.Campo("Farmacia");
                        dtpFechaPedido.Value = myLeer.CampoFecha("FechaPedido");
                        dtpFechaPedido.Enabled = false; 

                        Grid.LlenarGrid(myLeer.DataSetClase);
                        txtFolio.Text = myLeer.Campo("Folio");
                        txtObservaciones.Text = myLeer.Campo("Observaciones");

                        IniciarToolBar(false, false, false, true);
                        txtFolio.Enabled = false;
                        txtObservaciones.Enabled = false;


                        // grdProductos.Sheets[0].Columns[0].Formula = "IF((D1/E1)<1,1,(D1/E1))"; 
                    }
                    else
                    {
                        General.msjUser("Folio ingresado no encontrado, verifique.");
                        txtFolio.Focus();
                    }
                }
            }
        }
        #endregion Buscar Folio

        #region Guardar Pedido
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;
            string message = "¿ Desea realizar el pedido ?";

            bPedidoGuardado = false;

            if (ValidaStatusPedido())
            {
                if (ValidaDatos())
                {
                    if (General.msjConfirmar(message) == DialogResult.Yes)
                    {
                        if (ValidaCantidades())
                        {
                            if (cnn.Abrir())
                            {
                                cnn.IniciarTransaccion();

                                if (GuardarEncabezado())
                                {
                                    if ( GuardarDetalle() )
                                        bContinua = GuardarPedidoEnConcentrado();
                                }

                                if (bContinua)
                                {
                                    cnn.CompletarTransaccion();
                                    sFolioPedido = txtFolio.Text;
                                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                    //Imprimir();
                                    bPedidoGuardado = true;

                                    if (bOpcionExterna)
                                        this.Hide();
                                }
                                else
                                {
                                    cnn.DeshacerTransaccion();
                                    Error.GrabarError(myLeer, "btnGuardar_Click");
                                    General.msjError("Ocurrió un error al guardar la Información.");
                                }

                                cnn.Cerrar();
                            }
                            else
                            {
                                General.msjAviso(General.MsjErrorAbrirConexion);
                            }
                        }
                    }
                }
            }
        }

        private bool GuardarEncabezado()
        {
            bool bRegresa = true;

            string sSql = String.Format("Exec spp_Mtto_COM_OCEN_REG_Pedidos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sPersonal, sFechaSistema, txtObservaciones.Text);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (myLeer.Leer())
                {
                    txtFolio.Text = String.Format("{0}", myLeer.Campo("FolioPedido"));
                    sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                }
                else
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }

        private bool GuardarDetalle()
        {
            bool bRegresa = true;
            string sSql = "", sIdClaveSSA = "", sCodigoEAN = "";
            int iCantidad = 0, iCantidadSurtida = 0, iCantidadEnviadaCentral = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);
                iCantidadSurtida = Grid.GetValueInt(i, (int)Cols.CantidadSurtida);
                
                sSql = string.Format("Exec spp_Mtto_COM_OCEN_REG_Pedidos_Productos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sIdClaveSSA, sCodigoEAN, iCantidad, iCantidadSurtida, iCantidadEnviadaCentral);

                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }

            }

            return bRegresa;
        }

        private bool GuardarPedidoEnConcentrado()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_COM_OCEN_REG_ComConcentrado_Pedidos_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                sEmpresa, sEstado, DtGeneral.FarmaciaConectada, sFarmacia, DtGeneral.IdPersonal, sTipoPedido, txtFolio.Text);

            bRegresa = myLeer.Exec(sSql); 
            return bRegresa;
        }

        #endregion Guardar Pedido

        #region Funciones
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Ejecutar, bool Status)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnEjecutar.Enabled = Ejecutar;
            btnStatus.Enabled = Status;
        }

        private bool ValidaStatusPedido()
        {
            bool bRegresa = false;

            IniciarToolBar(false, false, false, true);
            if (txtFolio.Text != "")
            {
                string sSql = String.Format(" Select FolioPedido From COM_OCEN_Pedidos (NoLock) " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdTipoPedido = '{3}' And FolioPedido = '{4}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, Fg.PonCeros(txtFolio.Text.Trim(), 6));

                if (myLeer.Exec(sSql))
                {
                    if (myLeer.Leer())
                    {
                        General.msjUser("El Folio ya ha sido procesado.");
                    }
                    else
                    {
                        bRegresa = true;
                        IniciarToolBar(false, true, false, true);
                    }
                }
                else
                {
                    General.msjUser("Ocurrió un error al obtener el Status del Pedido");
                }
            }
            return bRegresa;
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

        private bool ValidaCantidades()
        {
            bool bRegresa = true;
            int iCantidadSurtida = 0;
            string message = "", sDescripcion = "";

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sDescripcion = Grid.GetValue(i, (int)Cols.Descripcion);
                iCantidadSurtida = Grid.GetValueInt(i, (int)Cols.CantidadSurtida);
                message = "La Sal " + sDescripcion + " tiene cantidad a surtir CERO, ¿Desea continuar? ";

                if (iCantidadSurtida == 0)
                {
                    if (General.msjConfirmar(message) == DialogResult.No)
                    {
                        //Si el usuario indica que no desea continuar se detiene el ciclo para que modifique la cantidad.
                        bRegresa = false;
                    }

                    if (!bRegresa)
                        break;
                }

            }

            return bRegresa;
        }

        public void MostrarPedido(string sEmp, string sEdo, string sFar, string sTipoPed, string sFolio)
        {
            sFolio = Fg.PonCeros(sFolio, 6 );
            txtFolio.Text = sFolio;
            sEmpresa = sEmp;
            sEstado = sEdo;
            sFarmacia = sFar;
            sTipoPedido = sTipoPed;
            bOpcionExterna = true;
            txtFolio_Validating(null, null);

            this.ShowDialog();
        }

        #endregion Funciones

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = Grid.ActiveRow;
            int iExistencia = Grid.GetValueInt(iRow, (int)Cols.Cantidad);
            int iCantidad = Grid.GetValueInt(iRow, (int)Cols.CantidadSurtida);
            
            if (iCantidad > iExistencia)
            {
                Grid.SetValue(iRow, (int)Cols.CantidadSurtida, iExistencia);
                General.msjUser("La cantidad surtible no puede ser mayor a la cantidad solicitada, verifique.");
                Grid.SetActiveCell(iRow, (int)Cols.CantidadSurtida);
            }
        }
    }
}
