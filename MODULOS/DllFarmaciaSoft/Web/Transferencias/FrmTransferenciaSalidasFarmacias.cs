using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

////using Dll_IMach4;
////using Dll_IMach4.Interface;

// using DllTransferenciaSoft.ObtenerInformacion;

namespace DllFarmaciaSoft.Web.Transferencias
{
    public partial class FrmTransferenciaSalidasFarmacias : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            EsIMach4 = 11 
        }

        ////PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        string sFolioSolicitud = ""; 

        clsConexionSQL cnn; // = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexionRemota; 
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; 

        string sFolioTransferencia = "";
        string sMensajeGrabar = "";

        bool bEsConsultaExterna = false;
        bool bPermitirImprimir = false;

        // bool bMovtoCancelado = false;
        bool bEstaCancelado = false;
        // bool bExisteMovto = false;
        // bool bMovtoAplicado = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sCveRenapo = DtGeneral.ClaveRENAPO;
        string sIdTipoMovtoInv = "TS";
        string sTipoES = "S";
        string sIdProGrid = "";
        // int iAnchoColPrecio = 0;

        bool bFolioGuardado = false;

        // string sFormato = "#,###,###,##0.###0";
        
        ////// Manejo automatico de Transferencias 
        ////DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias = 
        ////    new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        public FrmTransferenciaSalidasFarmacias(clsDatosConexion DatosDeConexion)
        {
            InitializeComponent();

            DatosDeConexionRemota = DatosDeConexion;
            cnn = new clsConexionSQL(DatosDeConexionRemota);
            cnn.SetConnectionString();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, true); 

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;

            // GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);
        }

        #region Botones 
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true; 

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if (bExito & !bEsConsultaExterna)
            {
                sFolioSolicitud = bPermitirImprimir.ToString(); 
                sFolioSolicitud = "";  

                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 12);
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;

                bFolioGuardado = false;
                IniciarToolBar(false, false, false);
                myGrid.Limpiar(true);
                Fg.IniciaControles();

                dtpFechaRegistro.Enabled = false;
                txtIdPersonal.Enabled = false;
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;

                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;

                chkTipoImpresion.Checked = false; 

                txtFolio.Focus();
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        } 

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled; 

            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    bool bExito = false;
                    IniciarToolBar(); 
                    cnn.IniciarTransaccion();

                    // Generar el Movimiento de Inventario 
                    if (GrabarMovtoEncabezado())
                    {
                        // Generar la transferencia de salida 
                        if (GrabarTransferenciaEncabezado())
                        {
                            // Generar la informacion de la transferencia de entrada 
                            if ( GrabarDetalleEnvioTransferencia() )
                                bExito = AfectarExistencia(true, false);
                        }
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*"; 
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la transferencia.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                    }
                    else
                    {
                        cnn.CompletarTransaccion();

                        //// IMach  // Enlazar el folio de inventario 
                        //IMachPtoVta.TerminarSolicitud(sFolioMovto);

                        General.msjUser(sMensajeGrabar);
                        EnvioAutomaticoDeTransferencias(); 
                        IniciarToolBar(false, false, true);
                        ImprimirInformacion();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }
        #endregion Botones

        #region Impresion 
        private void ImprimirInformacion()
        {
            bool bRegresa = false; 
            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                if (!chkTipoImpresion.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Transferencias.rpt";
                }
                else
                {
                    myRpt.NombreReporte = "PtoVta_TransferenciasTicket.rpt";
                }

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", "TS" + txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////////if (General.ImpresionViaWeb)
                ////////{
                ////////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////////    DataSet datosC = DatosCliente.DatosCliente();

                ////////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////////}
                ////////else
                ////////{
                ////////    myRpt.CargarReporte(true);
                ////////    bRegresa = !myRpt.ErrorAlGenerar;
                ////////}

                if (bRegresa)
                    LimpiarPantalla(false);
                else
                    General.msjError("Ocurrió un error al cargar el reporte."); 
            }
        }
        #endregion Impresion

        #region Validacion de informacion
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de transferencia inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtFarmaciaDestino.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Farmacia destino de la transferencia, verifique.");
                txtFarmaciaDestino.Focus();
            }

            //////if (bRegresa && Convert.ToDouble(txtSubTotal.Text) == 0)
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("El sub-total de la factura no puede ser igual a cero, verfique.");
            //////    txtSubTotal.Focus();
            //////}

            ////////if (bRegresa && Convert.ToDouble(txtIva.Text) == 0)
            ////////{
            ////////    bRegresa = false;
            ////////    General.msjUser("El iva de la factura no puede ser igual a cero, verfique.");
            ////////    txtIva.Focus();
            ////////}

            //////if (bRegresa && Convert.ToDouble(txtTotal.Text) == 0)
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("El total de la factura no puede ser igual a cero, verfique.");
            //////    txtTotal.Focus();
            //////}

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para la transferencia, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            return bRegresa;
        }

        private bool validarCancelacion()
        {
            bool bRegresa = true;
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
                    if (Lotes.CantidadTotal == 0)
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para la transferencia\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;

        }

        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Validacion de informacion

        #region Guardar datos tranferencia 
        private bool GrabarTransferenciaEncabezado()
        {
            bool bRegresa = true;
            string sFolio = "*";

            string sSql = string.Format("Exec spp_Mtto_TransferenciasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " + 
                          " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                          sEmpresa, sEstado, sCveRenapo, sFarmacia, "00", 0, sFolio, "", "", "", sIdTipoMovtoInv,
                          0, DtGeneral.IdPersonal, txtObservaciones.Text, txtSubTotal.Text, txtIva.Text, txtTotal.Text,
                          sEstado, sCveRenapo, txtFarmaciaDestino.Text, "00");

            sSql = string.Format("Exec spp_Mtto_TransferenciasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " + 
                                      " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}' ",
                                      sEmpresa, sEstado, sCveRenapo, sFarmacia, "00", 0, sFolio, sFolioMovto, "", "", sIdTipoMovtoInv,
                                      0, DtGeneral.IdPersonal, txtObservaciones.Text, txtSubTotal.Text, txtIva.Text, txtTotal.Text,
                                      sEstado, sCveRenapo, txtFarmaciaDestino.Text, "00");



            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GrabarTransferenciaEncabezado()");
            }
            else
            {
                leer.Leer();
                sFolioTransferencia = leer.Campo("FolioTransferencia");
                txtFolio.Text = sFolioTransferencia.Substring(2);
                sMensajeGrabar = leer.Campo("Mensaje");
                bRegresa = GrabarTransferenciaDetalle();
            }

            return bRegresa;
        }

        private bool GrabarTransferenciaDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nTasaIva = 0, nSubTotal = 0, nImporteIva = 0, nImporte = 0;
            int iUnidadDeSalida = 0;
            

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);

                nSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                nImporteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                nImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                              " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                              sEmpresa, sEstado, sFarmacia, sFolioTransferencia,
                              sIdProducto, sCodigoEAN, i, iUnidadDeSalida, iCantidad, 0, 0, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!GrabarTransferenciaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarTransferenciaDetalleLotes(int Renglon, string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                // Registrar el producto en las tablas de existencia 
                //sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                //    sEstado, sFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');

                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                       sEmpresa, sEstado, sFarmacia, sFolioTransferencia, IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Cantidad.ToString());
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar datos tranferencia

        #region Guardar datos tranferencia auxiliar 
        private bool GrabarDetalleEnvioTransferencia()
        {
            string sSql = string.Format(" Exec spp_Mtto_TransferenciasEnvioGenerar " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}' ", 
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            return leer.Exec(sSql);
        }

        #endregion Guardar datos tranferencia auxiliar

        #region Guardar movimiento de inventario 
        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }

        private bool GrabarMovtoEncabezado()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, "*", sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");
                bRegresa = GrabarMovtoDetalle();
            }

            return bRegresa;
        }
        
        private bool GrabarMovtoDetalle()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                        nTasaIva, iCantidad, nCosto, nImporte, 'A');
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!GrabarMovtoDetalleLotes(sIdProducto, sCodigoEAN, nCosto))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Guardar movimiento de inventario

        #region Cargar Datos 
        private bool CargarDatosFarmacia()
        {
            bool bRegresa = false;
            if (leer.Campo("FarmaciaStatus").ToUpper() == "C")
            {
                General.msjUser("La Farmacia seleccionada actualmente se encuentra cancelada,\nno es posible generar la transferencia.");
                txtFarmaciaDestino.Text = "";
                lblFarmaciaDestino.Text = "";
            }
            else
            {
                // Si la Farmacia Conectada Es Almacen no se valida la Farmacia Destino 
                if (!DtGeneral.EsAlmacen)
                {
                    if (leer.CampoBool("EsAlmacen"))
                    {
                        General.msjUser("La Farmacia seleccionada esta configurada como Almacén,\nno es posible generar la transferencia de Farmacia a Almacén.");
                        txtFarmaciaDestino.Text = "";
                        lblFarmaciaDestino.Text = "";
                    }
                    else
                    {
                        bRegresa = true;
                        txtFarmaciaDestino.Enabled = false;
                        txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                        lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                    }
                }
                else
                {
                    bRegresa = true;
                    txtFarmaciaDestino.Enabled = false;
                    txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                    lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                }
            }
            return bRegresa;
        }

        private void CargarDetallesTransferencia()
        {
            // myGrid.Limpiar(false);
            leer.DataSetClase = query.FolioTransferenciaDetalles(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDetallesLotesTransferencia();
            }
            Totalizar();
        }

        private void CargarDetallesLotesTransferencia()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaDetallesLotes(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);
        }

        #endregion Cargar Datos
        
        private void FrmTransferenciaSalidasFarmacias_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(false);
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                        btnGuardar_Click(null, null);
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                        btnNuevo_Click(null, null);
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e); 

            switch (e.KeyCode)
            {
                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false);
             
            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                if (txtFolio.Text.Trim() != "")
                {
                    leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, false, "txtFolio_Validating_1");
                    if (leer.Leer())
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true);
                        txtFolio.Enabled = false;
                        txtFolio.Text = Fg.PonCeros(leer.Campo("Folio"), 8);

                        txtFarmaciaDestino.Text = leer.Campo("IdFarmaciaRecibe");
                        lblFarmaciaDestino.Text = leer.Campo("FarmaciaRecibe");
                        txtObservaciones.Text = leer.Campo("Observaciones");

                        CargarDetallesTransferencia();
                    }
                    else
                    {
                        txtFolio.SelectAll();
                        txtFolio.Focus();
                    }
                }
            }

        }

        private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;

            if (txtFarmaciaDestino.Text.Trim() != "")
            {
                if (Fg.PonCeros(txtFarmaciaDestino.Text, 4) == DtGeneral.FarmaciaConectada)
                {
                    General.msjUser("No se puede generar transferencia a la farmacia origen, verifique.");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, sEstado, sFarmacia, txtFarmaciaDestino.Text, "txtFarmaciaDestino_Validating");
                    if (leer.Leer())
                    {
                        //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 
                        bExito = CargarDatosFarmacia();
                        // myGrid.Limpiar(true); 
                    }
                }
            }

            if (!bExito)
            {
                txtFarmaciaDestino.Text = "";
                lblFarmaciaDestino.Text = "";
                // e.Cancel = true;
            }
        }

        private void txtFarmaciaDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, sEstado, sFarmacia, "txtFarmaciaDestino_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private void txtSubTotal_Validating(object sender, CancelEventArgs e)
        {
            txtSubTotal.Text = Fg.Format(txtSubTotal.Text, 4);
        }

        private void txtIva_Validating(object sender, CancelEventArgs e)
        {
            txtIva.Text = Fg.Format(txtIva.Text, 4);
        }

        private void txtTotal_Validating(object sender, CancelEventArgs e)
        {
            txtTotal.Text = Fg.Format(txtTotal.Text, 4);
        }

        #region Manejo Grid
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (!bEstaCancelado)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, 1);
                        }
                    }
                }
            } 
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.Costo:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    //if (chkAplicarInv.Enabled)
                    {
                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            leer.DataSetClase = ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "grdProductos_KeyDown");
                            if (leer.Leer())
                                CargarDatosProducto();
                        }

                        if (e.KeyCode == Keys.Delete)
                            removerLotes();

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}

                        //////// Administracion de Mach4 
                        ////if (e.KeyCode == Keys.F10)
                        ////{
                        ////    if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
                        ////    {
                        ////        string sIdProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
                        ////        string sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                        ////        if (sIdProducto != "")
                        ////        {
                        ////            IMachPtoVta.Show(sIdProducto, sCodigoEAN);
                        ////            mostrarOcultarLotes();
                        ////        }
                        ////    }
                        ////}
                    }
                    break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                CargarDatosProducto();
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                            }
                        }
                        else
                        {
                            //General.msjError(sMsjEanInvalido);
                            myGrid.LimpiarRenglon(myGrid.ActiveRow);
                            myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
                            SendKeys.Send("");
                        }
                    }
                    else
                    {
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    break;
            }
            Totalizar();
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
        }

        private bool CargarDatosProducto()
        {
            bool bRegresa = true;
            int iRow = myGrid.ActiveRow;
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false; 
            string sCodEAN = leer.Campo("CodigoEAN");

            if (sValorGrid != sCodEAN)
            {
                if (!myGrid.BuscaRepetido(sCodEAN, iRow, iColEAN))
                {
                    // No modificar la informacion capturada en el renglon si este ya existia
                    myGrid.SetValue(iRow, iColEAN, sCodEAN);
                    myGrid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                    myGrid.SetValue(iRow, (int)Cols.TasaIva, leer.Campo("TasaIva"));

                    //if (sIdProGrid != leer.Campo("CodigoEAN"))
                    //if (sValorGrid != leer.Campo("CodigoEAN"))
                    {
                        sIdProGrid = leer.Campo("IdProducto");
                        myGrid.SetValue(iRow, (int)Cols.Codigo, sIdProGrid);
                        myGrid.SetValue(iRow, (int)Cols.Cantidad, 0);
                        myGrid.SetValue(iRow, (int)Cols.Costo, leer.CampoDouble("CostoPromedio"));
                        myGrid.SetValue(iRow, (int)Cols.TipoCaptura, "0");

                        bEsMach4 = leer.CampoBool("EsMach4");
                        myGrid.SetValue(iRow, (int)Cols.EsIMach4, bEsMach4);

                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);
                    }

                    ////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    ////if (IMach4.EsClienteIMach4)
                    ////{
                    ////    if (bEsMach4)
                    ////    {
                    ////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRow);
                    ////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                    ////    }
                    ////}

                    CargarLotesCodigoEAN();
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                    myGrid.LimpiarRenglon(iRow);
                    myGrid.SetActiveCell(iRow, iColEAN);
                    myGrid.EnviarARepetido(); 
                }
            }
            else
            {
                // Asegurar que no cambie el CodigoEAN
                myGrid.SetValue(iRow, iColEAN, sCodEAN);
            }

            return bRegresa;
        }

        private void Totalizar()
        {
            txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString(sFormato);
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString(sFormato);
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString(sFormato);
        }
        #endregion Manejo Grid

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);
                mostrarOcultarLotes();
            }
        }

        private void removerLotes()
        {
            if (!bFolioGuardado)
            {
                try
                {
                    int iRow = myGrid.ActiveRow;
                    Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                    myGrid.DeleteRow(iRow);
                    Totalizar();
                }
                catch { }

                if (myGrid.Rows == 0)
                    myGrid.Limpiar(true);
            }

        }

        private void mostrarOcultarLotes()
        {
            // Asegurar que el Grid tenga el Foco.
            if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
            {
                int iRow = myGrid.ActiveRow;

                if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
                {
                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
                    Lotes.EsEntrada = false;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = true;

                    ////// Mostrar la Pantalla de Lotes 
                    ////Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    //////// Administracion de Mach4
                    ////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    ////{
                    ////    if (IMachPtoVta.RequisicionRegistrada)
                    ////        Lotes.Show();
                    ////}
                    ////else
                    {
                        Lotes.Show();
                    }

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Costo);

                    Totalizar();
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        } 
        #endregion Manejo de lotes 
    
        #region Manejo de Transferencias 
        private void EnvioAutomaticoDeTransferencias()
        {
            ////string sMsj = string.Format(" ¿ Desea enviar inmediatamente la Transferencia '[ {0} ]' ?  ", sFolioTransferencia); 
            ////if (General.msjConfirmar(sMsj)== DialogResult.Yes)
            ////{
            ////    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);
            ////    ClienteTransferencias.TransferenciasAutomaticas();
            ////    ClienteTransferencias.EnviarArchivos();
            ////}
        }
        #endregion Manejo de Transferencias 

        #region Buscar Folio
        public void MostrarFolioTransferencia(string Empresa, string Estado, string Farmacia, string Folio, clsDatosConexion DatosDeConexion)
        {
            DatosDeConexionRemota = DatosDeConexion;
            cnn = new clsConexionSQL(DatosDeConexionRemota);
            cnn.SetConnectionString();

            this.bEsConsultaExterna = true;
            this.bPermitirImprimir = true;
            IniciarToolBar(false, false, false);

            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            //dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Lotes = new clsLotes(sEstado, sFarmacia, 12);
            Lotes.ManejoLotes = OrigenManejoLotes.Compras;


            sEmpresa = Empresa;
            sEstado = Estado;
            sFarmacia = Farmacia;
            txtFolio.Text = Fg.Right(Folio, 8);
            txtFolio_Validating(null, null);
            btnNuevo.Enabled = false;

            this.ShowDialog();
            // bEsConsultaExterna = false; 
        }
        #endregion Buscar Folio

    } // Clase 
}
