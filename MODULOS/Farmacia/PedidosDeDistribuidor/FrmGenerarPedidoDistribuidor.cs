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

//using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmGenerarPedidoDistribuidor : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, Cantidad = 4, ContenidoPaquete = 5, CantidadEnCajas = 6
        }

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsLeer leerPedido;
        clsLeerWebExt leerWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_ORDEN_DISTRIBUIDOR.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();
        string sUrlAlmacenCEDIS = GnFarmacia.UrlAlmacenCEDIS;
        string sHostAlmacenCEDIS = GnFarmacia.HostAlmacenCEDIS;
        string sIdFarmaciaAlmacenCEDIS = GnFarmacia.IdFarmaciaAlmacenCEDIS;

        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        public FrmGenerarPedidoDistribuidor()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leerExportarExcel = new clsLeer(ref ConexionLocal);
            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leer = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
        }

        private void FrmGenerarPedidoDistribuidor_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool ExportarExcel)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = ExportarExcel; 
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false);

            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;

            dtpFechaRegistro.Enabled = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;

            dtsPedido = null;
            dtsPedido = PreparaDtsPedidos();
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

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.PedidosOrdenDist_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (myLeer.Leer()) 
                {
                    bContinua = true;
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
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
            
            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            
            ////if ( myLeer.CampoInt("StatusPedido") != 0)
            ////    IniciarToolBar(false, false, true);
            ////else
            ////    IniciarToolBar(false, true, true);

            IniciarToolBar(false, false, true, true); 
            if (myLeer.Campo("Status") == "C")
            {   
                IniciarToolBar(false, false, true, true);
                lblStatus.Text = "CANCELADO";
                lblStatus.Visible = true; 
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.PedidosOrdenDist_Det(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                CargarDatosExportarExcel(); 
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
                    if (ConexionLocal.Abrir())
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
                            txtFolio.Text = "*";
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            ConexionLocal.DeshacerTransaccion(); 
                            General.msjError("Ocurrió un error al guardar la información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                    }

                }
            }

        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sQuery = "";
            string sSql = string.Format("Exec spp_Mtto_PedidosOrdenDist_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                txtFolio.Text.Trim(), DtGeneral.IdPersonal, txtObservaciones.Text, "A"); 

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
            bool bRegresa = true;
            string sSql = "", sQuery = "";
            string sIdClaveSSA = "", sClaveSSA = "";
            int iCantidad = 0, iContenidoPaquete = 0, iCantidadEnCajas = 0;  // , iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCantidadEnCajas = myGrid.GetValueInt(i, (int)Cols.CantidadEnCajas);
                iContenidoPaquete = myGrid.GetValueInt(i, (int)Cols.ContenidoPaquete);

                if (sIdClaveSSA != "")
                {
                    sSql = string.Format("Exec spp_Mtto_PedidosOrdenDist_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        sFolioPedido, sIdClaveSSA, iCantidad, iCantidadEnCajas, sClaveSSA, iContenidoPaquete);

                    sQuery = sSql;
                    InsertarQuerys(sQuery);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
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
                myRpt.NombreReporte = "PtoVta_PedidosOrdenDist";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
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
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
                        CargaDatosSal();
                    }

                }

                if (e.KeyCode == Keys.Delete)
                {
                    myGrid.DeleteRow(myGrid.ActiveRow);

                    if (myGrid.Rows == 0)
                        myGrid.Limpiar(true);
                }

            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
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

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
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
                    myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosSal()"); 
                    if (!myLeer.Leer())
                    {
                        //General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal();
                    }
                }
            }
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.SetValue(iRowActivo, (int)Cols.ContenidoPaquete, myLeer.Campo("ContenidoPaquete"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
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
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.");
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Exportar a Excel 
        private void CargarDatosExportarExcel()
        {
            string sSql = string.Format(" Select * From vw_Impresion_Pedidos_Orden_Distribuidor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 6));

            if (!leerExportarExcel.Exec(sSql))
            {
            } 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //int iRow = 3;
            //// string sColFormula = "I";
            //string sNombreFile = "PED_DIST_" + DtGeneral.ClaveRENAPO + sFarmacia + "_" + Fg.PonCeros(txtFolio.Text, 6) +  ".xls";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PEDIDOS_ORDEN_DISTRIBUIDOR.xls", DatosCliente);

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
            //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA_Aux"), iRow, 10);
            //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionClave"), iRow, 11);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Presentacion"), iRow, 12);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 13);

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
        }
        #endregion Exportar a Excel

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
            //object[] x = { sQuery };
            //dtsPedido.Tables[0].Rows.Add(x);
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

            string sSql = string.Format(" Update PedidosOrdenDist_Enc Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n " +
                                " Update PedidosOrdenDist Set Status = 'A', Actualizado = 0 " +
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

        
    }
}
