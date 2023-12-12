using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid; 
using SC_SolutionsSystem.Reportes;

//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.PedidosCEDIS
{
    public partial class FrmGenerarPedidosCEDIS : FrmBaseExt
    {
        #region Variables
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Presentacion = 4,
            Existencia = 5, ExistenciaSugerida = 6, CantidadSugerida = 7, Cantidad = 8,
            ContenidoPaquete = 9, CantidadEnCajas = 10, CantidadTotal = 11
        }

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsLeerExcel excel;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsLeer leerPedido;
        clsLeerWebExt leerWeb;
        OpenFileDialog openExcel = new OpenFileDialog();
        DataSet dtsCantidadesSegeridas;

        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        string sFile_In = "";
        bool bFolio_Guardado = false;
        bool bPedidoEnviado = false;

        bool bValidandoInformacion = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerExportarPlantilla_Excel;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();
        string sUrlAlmacenCEDIS = GnFarmacia.UrlAlmacenCEDIS;
        string sHostAlmacenCEDIS = GnFarmacia.HostAlmacenCEDIS;
        string sIdFarmaciaAlmacenCEDIS = GnFarmacia.IdFarmaciaAlmacenCEDIS;

        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClientePedidos =
           new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);
        #endregion Variables

        public FrmGenerarPedidosCEDIS()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leerExportarExcel = new clsLeer(ref ConexionLocal);
            leerExportarPlantilla_Excel = new clsLeer(ref ConexionLocal); 
            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leer = new clsLeer(ref ConexionLocal);
            excel = new clsLeerExcel();

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            //this.Height = 568;
            FrameProceso.Top = 345;
            FrameProceso.Left = 116;

            this.Height = FrameDetalles.Top + FrameDetalles.Height;
            this.Height += 50;
            MostrarEnProceso(false, 0);
        }

        private void FrmGenerarPedidosCEDIS_Load(object sender, EventArgs e)
        {
            CargarTipoPedidos();
            LimpiarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool ExportarExcel)
        {
            btnGuardar.Enabled = Guardar;
            btnExportarPlantilla.Enabled = Guardar;
            frameImportacion.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = ExportarExcel; 
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(false);
            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false);
            IniciaToolBar2(true, false, false, false);
            
            cboHojas.Clear();
            cboHojas.Add();
            cboHojas.SelectedIndex = 0;

            btnGenerarPaqueteDeDatos.Enabled = false;
            bFolio_Guardado = false; 
            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;
            bPedidoEnviado = false;
            dtpFechaRegistro.Enabled = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;

            cboTipoPedido.SelectedIndex = 0;

            dtsPedido = null;
            dtsCantidadesSegeridas = null;
            dtsPedido = PreparaDtsPedidos();
            CargarCantidadesSugeridas();
            txtFolio.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            IniciarToolBar(false, false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);
            bFolio_Guardado = false; 

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                //IniciarToolBar(true, false, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer()) 
                {
                    bContinua = true;
                    bFolio_Guardado = true; 
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
                    CargarDatosExportarExcel();
                    bContinua = CargaDetallesFolio(); 
                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }
        }

        private void CargaEncabezadoFolio()
        {            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio");
            sFolioPedido = txtFolio.Text;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            
            // lblStatus.Text = myLeer.Campo("StatusPedidoDesc");
            // lblStatus.Visible = true; 
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            cboTipoPedido.Data = myLeer.Campo("TipoDeClavesDePedido");
            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            
            ////if ( myLeer.CampoInt("StatusPedido") != 0)
            ////    IniciarToolBar(false, false, true);
            ////else
            ////    IniciarToolBar(false, true, true);
            btnGenerarPaqueteDeDatos.Enabled = true;
            IniciarToolBar(false, false, true, true); 
            if (myLeer.Campo("Status") == "C")
            {   
                IniciarToolBar(false, false, true, true);
                btnGenerarPaqueteDeDatos.Enabled = false;
                lblStatus.Text = "CANCELADO";
                lblStatus.Visible = true; 
            }

            if (myLeer.Campo("Status") == "R")
            {
                IniciarToolBar(false, false, true, true);
                //btnGenerarPaqueteDeDatos.Enabled = false;
                lblStatus.Text = "ENVIADO";
                lblStatus.Visible = true;
                bPedidoEnviado = true;
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Det(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");

            bRegresa = myLlenaDatos.Registros >= 1 ? true : false;
            myGrid.Limpiar(false);

            for (int i = 1; myLlenaDatos.Leer() && bRegresa; i++)
            {                
                //myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                myGrid.Rows += 1;
                myGrid.SetValue(i, (int)Cols.ClaveSSA, myLlenaDatos.Campo("ClaveSSA"));
                myGrid.SetValue(i, (int)Cols.IdClaveSSA, myLlenaDatos.Campo("IdClaveSSA"));
                myGrid.SetValue(i, (int)Cols.Descripcion, myLlenaDatos.Campo("DescripcionClave"));
                myGrid.SetValue(i, (int)Cols.Presentacion, myLlenaDatos.Campo("Presentacion"));
                myGrid.SetValue(i, (int)Cols.Existencia, myLlenaDatos.Campo("Existencia"));
                myGrid.SetValue(i, (int)Cols.Cantidad, myLlenaDatos.Campo("Cantidad"));
                myGrid.SetValue(i, (int)Cols.ContenidoPaquete, myLlenaDatos.Campo("ContenidoPaquete"));
                Cantidadsugerida(i, myLlenaDatos.Campo("ClaveSSA"));
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            return bRegresa;
        } 
        #endregion Buscar Folio
        
        #region Guardar Informacion 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = false;

            if (txtFolio.Text != "*")
            {
                MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                EliminarRenglonesVacios();
                if (ValidaDatos())
                {
                    if (!ConexionLocal.Abrir())
                    {
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else 
                    {
                        ConexionLocal.IniciarTransaccion();

                        bContinua = GrabarEncabezado();                         

                        if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolioPedido;
                            ConexionLocal.CompletarTransaccion();

                            //////if (GrabarPedidoDistribuidorRegional())
                            //////{
                            //////    ActualizarPedidosDistribuidor();
                            //////}

                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciarToolBar(false, false, true, true);
                            Imprimir(true);
                            btnNuevo_Click(this, null);
                        }
                        else
                        {
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            ConexionLocal.DeshacerTransaccion(); 
                            General.msjError("Ocurrió un error al guardar la información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                }
            }

        }

        private bool GrabarEncabezado()
        {
            // En el caso de las farmacias IdFarmacia e IdFarmaciaSolicita son el mismo 
            bool bRegresa = false;
            string sQuery = "";
            string sSql = ""; 
            int iTipoPed = Convert.ToInt32("0" + cboTipoPedido.Data);

            sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                "\t@IdEstadoSolicita = '{3}', @IdFarmaciaSolicita = '{4}', @FolioPedido = '{5}', @IdPersonal = '{6}', \n" +
                "\t@Observaciones = '{7}', @Status = '{8}', @EsTransferencia = '{9}', \n" +
                "\t@Cliente = '{10}', @SubCliente = '{11}', @Programa = '{12}', @SubPrograma = '{13}', @PedidoNoAdministrado = '{14}', \n" +
                "\t@TipoDeClavesDePedido = '{15}', @ReferenciaPedido = '{16}', @FechaEntrega = '{17}', @IdBeneficiario = '{18}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtFolio.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text, "A", true, "0000", "0000", "0000", "0000", 1, iTipoPed, "PF", 
                General.FechaYMD(General.FechaSistema), "00000000" );


            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje");

                sQuery = sSql;
                sQuery = sQuery.Replace("*", sFolioPedido);
                InsertarQuerys(sQuery);

                bRegresa = GrabarDetalle(); 
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = false;
            string sSql = "", sQuery = "";
            string sIdClaveSSA = "", sClaveSSA = "";
            int iCantidad = 0, iContenidoPaquete = 0, iCantidadEnCajas = 0, iExistenciaSugerida = 0;// , iOpcion = 1;
            int iExistencia = 0;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                //iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCantidadEnCajas = myGrid.GetValueInt(i, (int)Cols.CantidadEnCajas);
                iContenidoPaquete = myGrid.GetValueInt(i, (int)Cols.ContenidoPaquete);
                iExistencia = myGrid.GetValueInt(i, (int)Cols.Existencia); 
                iCantidad = myGrid.GetValueInt(i, (int)Cols.CantidadTotal);
                iExistenciaSugerida = myGrid.GetValueInt(i, (int)Cols.ExistenciaSugerida);

                if (sIdClaveSSA != "")
                {
                    if (iCantidad > 0)
                    {
                        sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Det \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', @IdClaveSSA = '{4}', \n" +
                            "\t@Existencia = '{5}', @CantidadSolicitada = '{6}', @CantidadEnCajas = '{7}', @ClaveSSA = '{8}', \n" +
                            "\t@ContenidoPaquete = '{9}', @ExistenciaSugerida = '{10}' \n",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            sFolioPedido, sIdClaveSSA, iExistencia, iCantidad, iCantidadEnCajas, sClaveSSA, iContenidoPaquete, iExistenciaSugerida);


                        sQuery = sSql;
                        InsertarQuerys(sQuery);

                        bRegresa = myLeer.Exec(sSql);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private void btnGenerarPCM_Click(object sender, EventArgs e)
        {
            CalcularCantidadesSugeridas();
        }

        private void CalcularCantidadesSugeridas()
        {
            string sSql = string.Format("Exec spp_PedidosCEDIS_CantidadSugerida '{0}', '{1}', '{2}', {3}",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.DiasARevisarpedidosCedis);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnGenerarPCM_Click()");
                General.msjError("Ocurrio un error al calcular el PCM");
            }
            else
            {
                CargarCantidadesSugeridas();
            }
        }

        #endregion Guardar Informacion

        #region Eliminar Informacion 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea cancelar el Folio seleccionado ?";

            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_ALMJ_Pedidos_RC '{0}', '{1}', '{2}', '{3}', '{4}', '', '', '', '', '', '{5}' ",
                        sEmpresa, sEstado, sJurisdiccion, sFarmacia, txtFolio.Text, iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        if (myLeer.Leer())
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar el Folio.");
                        //btnNuevo_Click(null, null);
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                }
            }            
        }
        #endregion Eliminar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Pedido Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", General.Fg.PonCeros(txtFolio.Text, 6));
                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario )
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Eventos 
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
        } 
        #endregion Eventos

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (!bFolio_Guardado)
            {
                if (myGrid.ActiveCol == 1)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        myLeer.DataSetClase = ayuda.Existencia_ClavesSSA_Sales("grdProductos_KeyDown");
                        if (myLeer.Leer())
                        {
                            //myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
                            CargaDatosSal(myLeer.Campo("ClaveSSA"));
                        }
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        myGrid.DeleteRow(myGrid.ActiveRow);

                        if (myGrid.Rows == 0)
                        {
                            myGrid.Limpiar(true);
                        }
                    }
                }
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolio_Guardado)
            {
                if (lblStatus.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, 1);
                        }
                    }
                }
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA) != "")
            {
                switch (myGrid.ActiveCol)
                {
                    case 1:
                        {
                            ObtenerDatosSal();
                        }

                        break;
                }
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = "";  // , sSql = "";
            // int iCantidad = 0;

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                ////sSql = string.Format("Exec Spp_SalesCapturaPedidosCentros '{0}', '{1}' ",
                ////    Fg.PonCeros(sCodigo, 4), sCodigo);
                ////if (!myLeer.Exec(sSql))
                ////{
                ////    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                ////    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                ////}
                ////else 
                {
                    myLeer.DataSetClase = Consultas.Existencia_ClavesSSA_Sales(sCodigo, "ObtenerDatosSal()"); 
                    if (!myLeer.Leer())
                    {
                        //General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal(myLeer.Campo("ClaveSSA"));
                    }
                }
            }
        }

        private bool ValidarClave_GeneraPedido(string ClaveSSA)
        {
            bool bRegresa = true;
            bool bError = false; 

            clsLeer leerValidar = new clsLeer(ref ConexionLocal);
            string sSql = string.Format("Select IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA, ExcluirDePedido, Actualizado " +
                "From CFG_Claves_Excluir_Generacion_Pedidos (NoLock) " + 
                "Where IdEstado = '{0}' and ClaveSSA = '{1}' ", DtGeneral.EstadoConectado, ClaveSSA); 

            if (!leerValidar.Exec(sSql))
            {
                bRegresa = false;
                bError = true; 
                Error.GrabarError(leerValidar, "ValidarClave_GeneraPedido"); 
            }
            else
            {
                if (!leerValidar.Leer())
                {
                    bRegresa = true; 
                    // La clave es valida para generarle pedidos 
                }
                else
                {
                    bRegresa = !leerValidar.CampoBool("ExcluirDePedido");
                }
            }

            if (bError)
            {
                General.msjError("Ocurrió un error al validar la clave para la generación de pedidos."); 
            }
            else
            {
                if (!bRegresa)
                {
                    General.msjAviso(string.Format("La Clave {0} no esta habilitada para generar pedidos.", ClaveSSA));
                    myGrid.LimpiarRenglon(myGrid.ActiveRow); 
                }
            }

            return bRegresa; 
        }

        private void CargaDatosSal(string ClaveSSA)
        {
            if (ValidarClave_GeneraPedido(ClaveSSA))
            {
                if (Validar_Clave_TipoPedido(ClaveSSA))
                {
                    if (ValidarPerfil(ClaveSSA))
                    {
                        CargaDatosClave();
                    }
                }
            }
        }

        private bool ValidarPerfil(string ClaveSSA)
        {
            bool bRegresa = true;

            string sSql = string.Format("Select top 1 ClaveSSA, DescripcionClave, 1 From vw_CB_CuadroBasico_Farmacias P (NoLock) " +
                        "Where IdEstado = '{0}' and IdFarmacia = '{1}' and StatusMiembro = 'A' and StatusClave = 'A'",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidarPerfil");
            }
            else
            {
                bRegresa = !leer.Leer();
            }

            if (bRegresa)
            {
                bRegresa = ValidarCuadroBasico(ClaveSSA);
            }
            else
            {
                sSql = string.Format("Select top 1 ClaveSSA, DescripcionClave, 1 From vw_CB_CuadroBasico_Farmacias P (NoLock) " +
                        "Where IdEstado = '{0}' and IdFarmacia = '{1}' and ClaveSSA_Aux = '{2}' and StatusMiembro = 'A' and StatusClave = 'A' ",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ClaveSSA);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "ValidarPerfil");
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        General.msjAviso("La clave SSA no pertenece al perfil de la unidad, Verifique por favor");
                    }
                }
            }

            return bRegresa;
        }

        private bool ValidarCuadroBasico(string ClaveSSA)
        {
            bool bRegresa = true;

            string sSql = string.Format("Select top 1 ClaveSSA, DescripcionClave From vw_Claves_Precios_Asignados P (NoLock) " +
                    "Where IdEstado = '{0}' and Status = 'A'", DtGeneral.EstadoConectado);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidarPerfil");
            }
            else
            {
                bRegresa = !leer.Leer();
            }

            if (!bRegresa)
            {
                sSql = string.Format("Select top 1 ClaveSSA, DescripcionClave From vw_Claves_Precios_Asignados P (NoLock) " +
                    "Where IdEstado = '{0}' And ClaveSSA = '{1}' and Status = 'A'",
                    DtGeneral.EstadoConectado, ClaveSSA);

                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leer, "ValidarCuadroBasico()");
                }
                else
                {
                    if (leer.Leer())
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        General.msjAviso("La clave SSA no pertenece al cuadro básico, Verifique por favor.");
                    }

                }
            }
            return bRegresa;
        }

        private void CargaDatosClave() 
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, myLeer.Campo("Presentacion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Existencia, myLeer.Campo("Existencia"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.SetValue(iRowActivo, (int)Cols.ContenidoPaquete, myLeer.Campo("ContenidoPaquete"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                    Cantidadsugerida(iRowActivo, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myGrid.GetValueInt(iRowActivo,(int)Cols.CantidadSugerida));
                }
                else
                {
                    General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }

        private void Cantidadsugerida()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                Cantidadsugerida(i, myGrid.GetValue(i, (int)Cols.ClaveSSA));
            }
        }

        private void Cantidadsugerida(int Row, string ClaveSSA)
        {
            string sFiltro = string.Format("ClaveSSA = '{0}'", ClaveSSA);
            clsLeer LeerResultados = new clsLeer();
            try
            {
                if (dtsCantidadesSegeridas == null)
                {
                    dtsCantidadesSegeridas = CargarCantidadesSugeridas();
                }


                LeerResultados.DataRowsClase = dtsCantidadesSegeridas.Tables[0].Select(sFiltro);
                LeerResultados.Leer();
                myGrid.SetValue(Row, (int)Cols.ExistenciaSugerida, LeerResultados.CampoInt("CantidadRecomendada"));
            }
            catch { }
        }

        private DataSet CargarCantidadesSugeridas()
        {
            DataSet dtsCantidades = new DataSet();
            string sSql = string.Format("Select * From PedidosCEDIS_CantidadSugerida Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            DateTime PCM;
            
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarCantidadsugerida()");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsCantidades = leer.DataSetClase;
                    TimeSpan ts = General.FechaSistemaObtener() - leer.CampoFecha("FechaRegistro");
                    int iDif = ts.Days;

                    if (iDif > 1)
                    {
                        CalcularCantidadesSugeridas();
                    }
                }
            }

            return dtsCantidades;
        }

        private bool Validar_Clave_TipoPedido(string ClaveSSA)
        {
            bool bRegresa = true;
            bool bError = false;
            string sWhere = "", sTipoPed = "";

            sTipoPed = cboTipoPedido.Data;

            if (sTipoPed == "1")
            {
                sWhere = " and IdTipoProducto = '01' ";
            }
            if (sTipoPed == "2")
            {
                sWhere = " and IdTipoProducto = '02' ";
            }
            if (sTipoPed == "3")
            {
                sWhere = " and IdTipoProducto = '02' and EsControlado = 1 ";
            }
            if (sTipoPed == "4")
            {
                sWhere = " and IdTipoProducto = '02' and EsAntibiotico = 1 ";
            }

            if (sTipoPed == "5")
            {
                sWhere = " ";
            }

            clsLeer leerValidar = new clsLeer(ref ConexionLocal);
            string sSql = string.Format(" Select * From CatClavesSSA_Sales (NoLock) " +
                " Where ClaveSSA = '{0}'  {1} ", ClaveSSA, sWhere );

            if (!leerValidar.Exec(sSql))
            {
                bRegresa = false;
                bError = true;
                Error.GrabarError(leerValidar, "Validar_Clave_TipoPedido");
            }
            else
            {
                if (!leerValidar.Leer())
                {
                    bRegresa = false;
                    // La clave no es valida para generarle pedidos 
                }                
            }

            if (bError)
            {
                General.msjError("Ocurrió un error al validar la clave para la generación de pedidos.");
            }
            else
            {
                if (!bRegresa)
                {
                    General.msjAviso(string.Format("La Clave {0} no es valida para el tipo de pedido seleccionado.", ClaveSSA));
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                }
            }

            return bRegresa; 
        }
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Pedido inválido, verifique.");
                txtFolio.Focus();
            } 

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        private bool validarCapturaProductos()
        {
            bool bRegresa = true;

            if (myGrid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (myGrid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    ////if ( int.Parse( lblUnidades.Text ) == 0 )
                    ////{
                    ////    bRegresa = false;
                    ////}
                    ////else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValue(i, (int)Cols.ClaveSSA) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (bRegresa)
            {
                if (myGrid.TotalizarColumna((int)Cols.Cantidad) == 0)
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.");
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Excel 
        private void CargarDatosExportarExcel()
        {
            string sSql = string.Format(" Select IdEmpresa As 'Id Empresa', Empresa, IdEstado As 'Id Estado', Estado, IdFarmacia As 'Id Unidad', Farmacia As Unidad, " +
                " Folio As 'Folio Pedido', FechaRegistro as 'Fecha Pedido', Observaciones, IdPersonal As 'Id Personal', Personal, " +
                " IdClaveSSA As 'Id ClaveSSA', ClaveSSA As 'Clave SSA', DescripcionClave As 'Descripcion Clave', Cantidad, Existencia " +
                " From vw_Impresion_Pedidos_Cedis (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 6));

            if (!leerExportarExcel.Exec(sSql))
            {
            } 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_COMPRAS.xls";
            //// int iRenglon = 2;
            //int iRow = 2;
            //// string sColFormula = "I";
            //string sNombreFile = "PED_CEDIS_" + DtGeneral.ClaveRENAPO + sFarmacia + "_" + Fg.PonCeros(txtFolio.Text, 6) +  ".xls";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PEDIDOS_COMPRAS.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        leerExportarExcel.RegistroActual = 1;
            //        while (leerExportarExcel.Leer())
            //        {
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdEmpresa"), iRow, 1);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRow, 3);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, 4);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRow, 5);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 6);

            //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 7);
            //            xpExcel.Agregar(leerExportarExcel.CampoFecha("FechaPedido"), iRow, 8);

            //            xpExcel.Agregar(leerExportarExcel.Campo("IdClaveSSA"), iRow, 9);
            //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 10);
            //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRow, 11);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 12);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Existencia"), iRow, 13); 

            //            iRow++;
            //        }

            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //    this.Cursor = Cursors.Default;
            //}
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;
            string sNombreFile = "PED_CEDIS_" + DtGeneral.ClaveRENAPO + sFarmacia + "_" + Fg.PonCeros(txtFolio.Text, 6);

            leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                //iRenglon++;
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                //iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        private void btnExportarPlantilla_Click(object sender, EventArgs e)
        {
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_COMPRAS_CARGA.xls";
            //bool bTieneDatos = false;

            //string sWhere = "", sTipoPed = "";

            //sTipoPed = cboTipoPedido.Data;

            //if (sTipoPed == "1")
            //{
            //    sWhere = " and C.IdTipoProducto = '01' ";
            //}
            //if (sTipoPed == "2")
            //{
            //    sWhere = " and C.IdTipoProducto = '02' ";
            //}
            //if (sTipoPed == "3")
            //{
            //    sWhere = " and C.IdTipoProducto = '02' and C.EsControlado = 1 ";
            //}
            //if (sTipoPed == "4")
            //{
            //    sWhere = " and C.IdTipoProducto = '02' and C.EsAntibiotico = 1 ";
            //}
            //if (sTipoPed == "5")
            //{
            //    sWhere = " ";
            //}

            //string sSql = string.Format("Select Distinct CB.IdEstado, CB.Estado, CB.IdFarmacia, CB.Farmacia, C.ClaveSSA_Aux As ClaveSSA, CB.ClaveSSA_Base, " +
            //        " C.DescripcionClave, C.Presentacion, C.ContenidoPaquete " +
            //        " From vw_CB_CuadroBasico_Farmacias CB (NoLock) " +
            //        " Inner Join vw_ClavesSSA_Sales C (NoLock) On (CB.IdClaveSSA = C.IdClaveSSA_Sal)" +
            //        " Where CB.IdEstado = '{0}' And CB.IdFarmacia = '{1}' {2}", sEstado, sFarmacia, sWhere );

            //if (leerExportarPlantilla_Excel.Exec(sSql))
            //{
            //    bTieneDatos = leerExportarPlantilla_Excel.Registros > 0 ? true : false;
            //}

            //if (!bTieneDatos)
            //{
            //    General.msjAviso("No se encontro información...");
            //}
            //else
            //{

            //    string sNombreFile = "PED_CEDIS_" +  sFarmacia;

            //    this.Cursor = Cursors.WaitCursor;
            //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PEDIDOS_COMPRAS_CARGA.xls", DatosCliente);

            //    if (!bRegresa)
            //    {
            //        this.Cursor = Cursors.Default;
            //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //    }
            //    else
            //    {
            //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //        xpExcel.AgregarMarcaDeTiempo = true;

            //        if (xpExcel.PrepararPlantilla(sNombreFile))
            //        {
            //            xpExcel.GeneraExcel();

            //            leerExportarPlantilla_Excel.RegistroActual = 1;
            //            for (int iRow = 2; leerExportarPlantilla_Excel.Leer(); iRow++)
            //            {
            //                xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ClaveSSA"), iRow, 1);
            //                xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ClaveSSA_Base"), iRow, 2);
            //                xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("DescripcionClave"), iRow, 3);
            //                xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("Presentacion"), iRow, 4);
            //                xpExcel.Agregar(leerExportarPlantilla_Excel.Campo("ContenidoPaquete"), iRow, 5);
            //                xpExcel.Agregar(0, iRow, 6);
            //            }

            //            xpExcel.CerrarDocumento();

            //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //            {
            //                xpExcel.AbrirDocumentoGenerado();
            //            }
            //        }
            //        this.Cursor = Cursors.Default;
            //    }
            //}
        }

        #endregion Excel

        #region Grabar_Informacion_Regional

        public static DataSet PreparaDtsPedidos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Sentencias");

            dtClave.Columns.Add("Query", Type.GetType("System.String"));
            dts.Tables.Add(dtClave);

            return dts.Clone();
        }

        private void InsertarQuerys(string sQuery)
        {
            object[] x = { sQuery };
            dtsPedido.Tables[0].Rows.Add(x);
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlAlmacenCEDIS, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlAlmacenCEDIS;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHostAlmacenCEDIS;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(myLeer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");

            }

            return bRegresa;
        }

        private bool GrabarPedidoDistribuidorRegional()
        {
            bool bRegresa = true;
            bool bContinua = true;
            string sQuery = "";

            if (validarDatosDeConexion())
            {
                cnnRegional = new clsConexionSQL(DatosDeConexion);
                cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnRegional.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leerPedido = new clsLeer(ref cnnRegional);

                if (cnnRegional.Abrir())
                {
                    IniciarToolBar(false, false, false, false);
                    cnnRegional.IniciarTransaccion();

                    leer.DataSetClase = dtsPedido;

                    while (leer.Leer())
                    {
                        sQuery = leer.Campo("Query");

                        if (!leerPedido.Exec(sQuery))
                        {
                            bContinua = false;
                            break;
                        }
                    }

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnnRegional.CompletarTransaccion();
                    }
                    else
                    {
                        bRegresa = false;
                        cnnRegional.DeshacerTransaccion();
                    }

                    cnnRegional.Cerrar();
                }
                else
                {
                    Error.LogError(cnnRegional.MensajeError);
                    General.msjAviso("Ocurrió un error al enviar la información al Almacen CEDIS.");
                }
            }
            else
            {
                bRegresa = false;
            }
            return bRegresa;

        }

        private bool ActualizarPedidosDistribuidor()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Update Pedidos_Cedis_Enc Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n " +
                                " Update Pedidos_Cedis_Det Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n ",
                                sEmpresa, sEstado, sFarmacia, sFolioPedido);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ActualizarPedidosDistribuidor");
                General.msjError("Ocurrió un error al Actualizar la información.");
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Grabar_Informacion_Regional

        #region Generar_Paquete_Datos
        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {
            string sMsj = string.Format(" El paquete del pedido ya ha sido enviado. ¿ Desea Generar de nuevo el paquete ?");

            if (bPedidoEnviado)
            {
                if (General.msjConfirmar(sMsj) == DialogResult.Yes)
                {
                    GenerarPaqueteDeDatos(false);
                }
            }
            else
            {
                GenerarPaqueteDeDatos(true);
            }
        }

        private void GenerarPaqueteDeDatos(bool Confirmar)
        {
            string sMsj = string.Format("¿ Desea generar el paquete de datos para el Pedido {0} ?", txtFolio.Text);
            sMsj = string.Format("¿ Desea generar el paquete de datos para el Pedido {0} ?", txtFolio.Text);
            bool bContinuar = false;

            if (!Confirmar)
            {
                bContinuar = true;                  
            }

            if (Confirmar)
            {
                if (General.msjConfirmar(sMsj) == DialogResult.Yes)
                {
                    bContinuar = true; 
                }
            }

            if (bContinuar)
            {               
                ClientePedidos = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                ClientePedidos.Pedidos_Cedis(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));
                ActualizaStatusPedido();
                General.msjAviso("Generación de Paquete de Datos terminada.");
                btnGenerarPaqueteDeDatos.Enabled = false;
                ClientePedidos.Abrir_Directorio_Transferencias();
            }
        }

        private void ActualizaStatusPedido()
        {
            string sSql = string.Format(" Update Pedidos_Cedis_Enc Set Status = 'R' " +
                          " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' ",
                          sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 6));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizaStatusPedido()");
                General.msjError("Ocurrió un error al actualizar el status del pedido");
            }
        }
        #endregion Generar_Paquete_Datos

        #region Importacion

        private void IniciaToolBar2(bool Abrir, bool Ejecutar, bool Guardar2, bool Validar)
        {
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar2.Enabled = Guardar2;
            btnValidarDatos.Enabled = Validar;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de pedido";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            //lblProcesados.Visible = false; /t

            if (openExcel.ShowDialog() == DialogResult.OK)
            {
                sFile_In = openExcel.FileName;

                IniciaToolBar2(false, false, false, false);
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start();
            }
        }

        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);

            excel = new clsLeerExcel(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            myGrid.Limpiar(false);
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
            IniciaToolBar2(true, bHabilitar, false, false);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear;
        }

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = "";

            if (Mostrar)
            {
                FrameProceso.Left = 116;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento";
            }

            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada";
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada";
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando inventario ..... ";
            }

            FrameProceso.Text = sTituloProceso;

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start();
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);

            LeerHoja();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2);
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;

            IniciaToolBar2(false, false, bRegresa, false);
            myGrid.Limpiar();
            excel.LeerHoja(cboHojas.Data);

            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros;
                //myGrid.LlenarGrid(excel.DataSetClase);
            }

            IniciaToolBar2(true, true, bRegresa, false);
            return bRegresa;
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            string[] SCols = { "ClaveSSA", "ClaveSSA Base", "Descripcion Sal", "Presentacion", "ContenidoPaquete", "CantidadPiezas" };
            if (excel.ValidarExistenCampos(SCols))
            {
                thGuardarInformacion = new Thread(this.GuardarInformacion);
                thGuardarInformacion.Name = "Guardar información seleccionada";
                thGuardarInformacion.Start();
            }
            else
            {
                General.msjAviso("No se encontraron todas las columnas requeridas en la plantilla para la integración del pedido, verifique. ");
            }
        }

        private void GuardarInformacion()
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref ConexionLocal);

            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar2(false, false, false, false);


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table Registro_Pedidos_CargaMasiva ");
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into Registro_Pedidos_CargaMasiva " +
                    " ( ClaveSSA, DescripcionClaveSSA, Presentacion, ContenidoPaquete, CantidadPzas ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                    DarFormato(excel.Campo("ClaveSSA")), DarFormato(excel.Campo("Descripcion Sal")),
                    DarFormato(excel.Campo("Presentacion")), DarFormato(excel.Campo("ContenidoPaquete ")),
                    excel.Campo("CantidadPiezas") == "" ? "0" : excel.Campo("CantidadPiezas"));

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    Error.GrabarError(leerGuardar, "GuardarInformacion()");
                    break;
                }
                iRegistrosProcesados++;
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar2(true, true, true, false);
            }
            else
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'Registro_Pedidos_CargaMasiva'  ");
                //General.msjUser("Información de inventario cargada satisfactoriamente.");
                IniciaToolBar2(true, true, false, true);
            }
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000;
            tmValidacion.Start();

            thValidarInformacion = new Thread(this.ValidarInformacion);
            thValidarInformacion.Name = "Validar informacion";
            thValidarInformacion.Start();
            System.Threading.Thread.Sleep(200);
        }

        private void ValidarInformacion()
        {

            bValidandoInformacion = true;
            bActivarProceso = false;
            bErrorAlValidar = false;
            clsLeer leerValidacion = new clsLeer();

            IniciaToolBar2(false, false, false, false);
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);

            string sTipoPed = "";

            sTipoPed = cboTipoPedido.Data;

            string sSql = string.Format("Exec sp_Proceso_Registro_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada '{0}', '{1}', '{2}', '{3}'",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sTipoPed);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true;
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()");
                General.msjError("Ocurrió un error al verificar el inventario a integrar.");
            }
            else
            {

                leer.RenombrarTabla(1, "Clave");
                //leer.RenombrarTabla(2, "Costo");
                //leer.RenombrarTabla(3, "Cantidad");
                //leer.RenombrarTabla(4, "Iva");
                //leer.RenombrarTabla(5, "Descripcion");

                leerValidacion.DataTableClase = leer.Tabla(1); //Clave
                bActivarProceso = leer.Registros > 0;

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(2);   // Costo 
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(3);   // Cantidad  
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(4);   // Iva  
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                //if (!bActivarProceso)
                //{
                //    leerValidacion.DataTableClase = leer.Tabla(5);   // Descripcion
                //    bActivarProceso = leerValidacion.Registros > 0;
                //}

                if (!bActivarProceso)
                {
                    sSql = string.Format("Select ClaveSSA, IdClaveSSA, DescripcionClaveSSA, Presentacion, Existencia, 0 as ExistenciaSugerida, 0 As CantidadSugerida, " +
                            "CantidadPzas, ContenidoPaquete, CantidadCajas, Existencia " +
                            "From Registro_Pedidos_CargaMasiva " +
                            "Where CantidadPzas > 0 ");

                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            myGrid.LlenarGrid(leer.DataSetClase);
                            Cantidadsugerida();
                        }
                    }

                }
            }

            bValidandoInformacion = false;
            bActivarProceso = !bActivarProceso;
            BloqueaHojas(false);
            MostrarEnProceso(false, 4);
        }

        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;
            string sMsj = "Existen Claves SSA sin identificar. \nDesea generar reporte ? ";


            if (!bValidandoInformacion)
            {
                if (bActivarProceso)
                {
                    IniciaToolBar2(true, true, false, false);
                }
                else
                {
                    IniciaToolBar2(true, true, false, true);
                    if (!bErrorAlValidar)
                    {
                        FrmIncidencias f = new FrmIncidencias(leer.DataSetClase);
                        f.ShowDialog();
                    }
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        #endregion Importacion

        #region Cargar_Combo_TipoPedidos
        private void CargarTipoPedidos()
        {
            cboTipoPedido.Clear();
            cboTipoPedido.Add();

            cboTipoPedido.Add("1", "Material de Curación");
            cboTipoPedido.Add("2", "Medicamentos Generales");
            cboTipoPedido.Add("3", "Medicamentos Controlados");
            cboTipoPedido.Add("4", "Medicamentos Antibióticos");
            cboTipoPedido.Add("5", " Todos ");

            cboTipoPedido.SelectedIndex = 0;
        }
        #endregion Cargar_Combo_TipoPedidos

        #region Eventos_Combo_Pedido
        private void cboTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                //cboTipoPedido.Enabled = false;
                myGrid.Limpiar(true);
                IniciarToolBar(true, false, false, false);
            }            
        }

        private void cboTipoPedido_Validating(object sender, CancelEventArgs e)
        {
            if (cboTipoPedido.SelectedIndex != 0)
            {
                cboTipoPedido.Enabled = false;               
            } 
        }
        #endregion Eventos_Combo_Pedido

        private void cboHojas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
