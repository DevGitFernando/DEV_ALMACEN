﻿using System;
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
    public partial class FrmConfirmaPedidoRegClaves : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, CajaCon = 4, Cantidad = 5, CantidadSurtida = 6, 
            CantidadEnviarCentral = 7 
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
        string sTipoDeClaves = ""; 

        public bool bPedidoGuardado = false;
        public bool bOpcionExterna = false; //Esta variable se utiliza para saber si la opcion fue llamada por otra pantalla.

         string sFolioPedidoRegional = "";
         string sMsj = "";


        public FrmConfirmaPedidoRegClaves()
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

        private void FrmCom_ConfirmarPedidoRegionalCredito_Load(object sender, EventArgs e)
        {
            //btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            IniciarToolBar(false, false, false, true);
            Grid.Limpiar(false);
            chkEnviar.Checked = true;
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
                    " ContenidoPaquete, CantidadCajas, CantidadSurtir, CantidadCajas, Folio, FechaRegistro, Observaciones, " + 
                    " (IdFarmacia + ' - ' + Farmacia) as Farmacia, FechaRegistro as FechaPedido " +
                    " From vw_COM_FAR_Pedidos_Claves (NoLock) " +
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
                                    if (GuardarDetalle())
                                    {
                                        //bContinua = GuardarPedidoEnConcentrado();
                                        bContinua = GenerarPedidoCentral();
                                    }
                                }

                                if (bContinua)
                                {
                                    cnn.CompletarTransaccion();
                                    sFolioPedido = txtFolio.Text;
                                    General.msjUser(sMensaje); //Este mensaje lo genera el SP
                                    if (sMsj.Trim() != "")
                                    {
                                        General.msjUser(sMsj);
                                        ImprimirInformacion();
                                    }                                    
                                    
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

            string sSql = String.Format("Exec spp_Mtto_COM_OCEN_Pedidos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdTipoPedido = '{3}', " + 
                " @FolioPedido = '{4}', @IdPersonal = '{5}', @FechaSistema = '{6}', @Observaciones = '{7}', " +
                " @IdEstadoRegistra = '{8}', @IdFarmaciaRegistra = '{9}', @TipoDeClavesDePedido = '{10}' ",
                sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sPersonal, sFechaSistema, txtObservaciones.Text,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sTipoDeClaves);

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
            string sSql = "", sIdClaveSSA = ""; //, sCodigoEAN = "";
            int iCantidad = 0, iCantidadSurtida = 0, iCantidadCentral = 0;

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sIdClaveSSA = Grid.GetValue(i, (int)Cols.IdClaveSSA);
                //sCodigoEAN = Grid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad = Grid.GetValueInt(i, (int)Cols.Cantidad);
                //iCantidadSurtida = Grid.GetValueInt(i, (int)Cols.CantidadSurtida);                        
                
                iCantidadSurtida = Grid.GetValueInt(i, (int)Cols.CantidadSurtida);
                iCantidadCentral = Grid.GetValueInt(i, (int)Cols.Cantidad) - Grid.GetValueInt(i, (int)Cols.CantidadSurtida);               

                sSql = string.Format("Exec spp_Mtto_COM_OCEN_PedidosDet_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                    sEmpresa, sEstado, sFarmacia, sTipoPedido, txtFolio.Text, sIdClaveSSA, iCantidad, iCantidadSurtida, iCantidadCentral);

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
            string sSql = string.Format("Exec spp_COMREG_Concentrado_Pedidos_Claves @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                " @IdFarmaciaPedido = '{3}', @IdPersonal = '{4}', @IdTipoPedido = '{5}', @FolioPedido = '{6}' ",
                sEmpresa, sEstado, DtGeneral.FarmaciaConectada, sFarmacia, DtGeneral.IdPersonal, sTipoPedido, txtFolio.Text);

            bRegresa = myLeer.Exec(sSql); 
            return bRegresa;
        }

        private bool GenerarPedidoCentral()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Exec spp_COM_GeneraPedido_Concentrado_OCEN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                " @IdPersonal = '{3}', @Observaciones = '{4}', @FolioPedidoUnidad = '{5}', @IdEstadoRegistra = '{6}', @IdFarmaciaRegistra = '{7}' ",
                sEmpresa, sEstado, sFarmacia, DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), Fg.PonCeros(txtFolio.Text, 6),
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (myLeer.Leer())
                {
                    sFolioPedidoRegional = myLeer.Campo("FolioDePedidoRegional");
                    sMsj = myLeer.Campo("Mensaje");
                }
                else
                {
                    sMsj = "";
                }
            }

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
                        General.msjUser("El Folio ya ha sido procesado");
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
            int iCantidadSurtida = 0, iCantidadCentral = 0, iCantidadSolicitada = 0;
            string sMensajeCero = "", sMensajeMayor = "", sClave = "";

            for (int i = 1; i <= Grid.Rows; i++)
            {
                sClave = Grid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidadSurtida = Grid.GetValueInt(i, (int)Cols.CantidadSurtida);
                iCantidadCentral = Grid.GetValueInt(i, (int)Cols.CantidadEnviarCentral);
                iCantidadSolicitada = Grid.GetValueInt(i, (int)Cols.Cantidad);
                sMensajeCero = "La Clave [" + sClave + "] tiene cantidad CERO, ¿Desea continuar? ";
                sMensajeMayor = "La Clave [" + sClave + "] tiene cantidad mayor que la solicitada. Verifique ";

                if ( ( iCantidadSurtida + iCantidadCentral ) == 0)
                {
                    if (General.msjConfirmar(sMensajeCero) == DialogResult.No)
                    {
                        //Si el usuario indica que no desea continuar se detiene el ciclo para que modifique la cantidad.
                        bRegresa = false;
                    }

                    if (!bRegresa)
                        break;
                }
                else if ( ( iCantidadSurtida + iCantidadCentral ) > iCantidadSolicitada)
                {
                    bRegresa = false;
                    General.msjAviso(sMensajeMayor);
                }

            }

            return bRegresa;
        }

        public void MostrarPedido(string Empresa, string Estado, string Farmacia, string TipoDePedido, string Folio, string TipoDeClaves)
        {
            Folio = Fg.PonCeros(Folio, 6 );
            txtFolio.Text = Folio;
            sEmpresa = Empresa;
            sEstado = Estado;
            sFarmacia = Farmacia;
            sTipoPedido = TipoDePedido;
            sTipoDeClaves = TipoDeClaves;

            bOpcionExterna = true;
            txtFolio_Validating(null, null);
            chkEnviar.Checked = true;

            this.ShowDialog();
        }

        #endregion Funciones

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = Grid.ActiveRow;
            int iExistencia = Grid.GetValueInt(iRow, (int)Cols.Cantidad);
            int iCantidad = Grid.GetValueInt(iRow, (int)Cols.CantidadSurtida);
            int iCantidadCentral = 0;
            
            if (iCantidad > iExistencia)
            {
                Grid.SetValue(iRow, (int)Cols.CantidadSurtida, iExistencia);
                General.msjUser("La cantidad a comprar no puede ser mayor a la cantidad solicitada, verifique.");
                Grid.SetActiveCell(iRow, (int)Cols.CantidadSurtida);
            }

            iCantidadCentral  = Grid.GetValueInt(iRow, (int)Cols.Cantidad) - Grid.GetValueInt(iRow, (int)Cols.CantidadSurtida);
            Grid.SetValue(iRow, (int)Cols.CantidadEnviarCentral, iCantidadCentral);
        }

        private void chkEnviar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnviar.Checked)
            {
                Grid.BloqueaColumna(true, (int)Cols.CantidadSurtida);
            }
            else
            {
                Grid.BloqueaColumna(false, (int)Cols.CantidadSurtida);
            }
        }

        private void ImprimirInformacion()
        {
            ////bool bRegresa = false;
            ////if (sFolioPedidoRegional.Trim() != "" )
            ////{
            ////    DatosCliente.Funcion = "Imprimir()";
            ////    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            ////    // byte[] btReporte = null;               

            ////    myRpt.RutaReporte = GnCompras.RutaReportes;

            ////    myRpt.Add("IdEmpresa", sEmpresa);
            ////    myRpt.Add("IdEstado", sEstado);
            ////    myRpt.Add("IdFarmacia", sFarmacia);
            ////    myRpt.Add("Folio", sFolioPedidoRegional);
            ////    myRpt.Add("IdTipoPedido", sTipoPedido);
            ////    myRpt.NombreReporte = "COM_REG_PedidoCentral";             
                     
            ////    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                

            ////    if (!bRegresa)
            ////    {
            ////        General.msjError("Ocurrió un error al cargar el reporte.");
            ////    }
            ////}
        }
    }
}
