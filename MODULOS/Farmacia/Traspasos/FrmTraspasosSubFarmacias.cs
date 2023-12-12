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

//using Dll_IMach4;
//using Dll_IMach4.Interface;
using DllRobotDispensador; 

using DllTransferenciaSoft.ObtenerInformacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;



namespace Farmacia.Traspasos
{
    public partial class FrmTraspasosSubFarmacias : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            EsIMach4 = 11 
        }

        // PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        // string sFolioSolicitud = ""; 
        bool bEsClienteInterface = RobotDispensador.Robot.EsClienteInterface;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; 
        clsVerificarSalidaLotes VerificarLotes; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; 

        string sFolioTransferencia = "";
        string sMensajeGrabar = "";

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
        string sIdTipoMovtoInv = "TIS";
        string sTipoES = "S";
        string sIdProGrid = "";
        // int iAnchoColPrecio = 0;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        bool bFolioGuardado = false;

        // string sFormato = "#,###,###,##0.###0";
        
        // Manejo automatico de Transferencias 
        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias = 
            new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion); 

        public FrmTraspasosSubFarmacias()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true); 

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        #region Botones 
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla ?") == DialogResult.No)
                    bExito = false;
            }

            if (bExito)
            {
                // sFolioSolicitud = "";  

                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;


                SKU = new clsSKU();
                SKU.IdEmpresa = sEmpresa;
                SKU.IdEstado = sEstado;
                SKU.IdFarmacia = sFarmacia;
                SKU.TipoDeMovimiento = sIdTipoMovtoInv; 


                bFolioGuardado = false;
                IniciarToolBar(false, false, false);
                myGrid.Limpiar(false);
                Fg.IniciaControles();

                dtpFechaRegistro.Enabled = false;
                txtIdPersonal.Enabled = false;
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;

                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;

                chkTipoImpresion.Checked = false;

                cboSubFarmaciaOrigen.Clear();
                cboSubFarmaciaOrigen.Add("0", "<< Seleccione >>");
                cboSubFarmaciaOrigen.SelectedIndex = 0;

                cboSubFarmaciaDestino.Clear();
                cboSubFarmaciaDestino.Add("0", "<< Seleccione >>");
                cboSubFarmaciaDestino.SelectedIndex = 0;

                CargaSubFarmaciaOrigen();
                CargaSubFarmaciaDestino();

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
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    bool bExito = false;
                    IniciarToolBar();
                    cnn.IniciarTransaccion();

                    // Generar el Movimiento de Inventario 
                    if (GrabarMovtoEncabezado())
                    {
                        // Generar la transferencia de salida 
                        if (GrabarTraspasoEncabezado())
                        {
                            bExito = AfectarExistencia(true, false);

                            if (bExito)
                            {
                                bExito = GenerarTraspasoEntrada();
                            }
                        }
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar el Traspaso.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }
                    else
                    {
                        txtFolio.Text = SKU.Foliador;
                        cnn.CompletarTransaccion();
                        // IMachPtoVta.TerminarSolicitud(sFolioMovto);
                        
                        General.msjUser(sMensajeGrabar);
                        IniciarToolBar(false, false, true);

                        Imprimir(); 

                        LimpiarPantalla(false);
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
            Imprimir();
        }
        #endregion Botones

        #region Impresion 
        private void Imprimir()
        {
            Imprimir("TIS", "Traspaso de salida");
            Imprimir("TIE", "Traspaso de entrada");
        }
        private void Imprimir(string TipoTraspaso, string Titulo)
        {
            bool bRegresa = false; 
            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.TituloReporte = Titulo; 

                if (!chkTipoImpresion.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_Traspasos.rpt";
                }
                else
                {
                    myRpt.NombreReporte = "PtoVta_TraspasosTicket.rpt";
                }

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", TipoTraspaso + txtFolio.Text);                

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
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

            if (bRegresa && cboSubFarmaciaOrigen.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione la SubFarmacia Origen, verifique.");
                cboSubFarmaciaOrigen.Focus();
            }

            if (bRegresa && cboSubFarmaciaDestino.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione la SubFarmacia Destino, verifique.");
                cboSubFarmaciaDestino.Focus();
            }

            if (bRegresa && cboSubFarmaciaOrigen.Data == cboSubFarmaciaDestino.Data)
            {
                bRegresa = false;
                General.msjUser("La SubFarmacia Destino no puede ser igual a la SubFarmacia Origen, Verifique.....");
                cboSubFarmaciaDestino.Focus();
            }
            

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

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            if (bRegresa)
            {
                VerificarLotes = new clsVerificarSalidaLotes();
                bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes); 
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un traspaso de sub-farmacia, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("TRASPASO_SUB_FARMACIA", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("TRASPASO_SUB_FARMACIA", sMsjNoEncontrado);
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

        private bool validarCantidadesCapturadas()
        {
            bool bRegresa = true;
            DataSet dtsProductosDiferencias = clsLotes.PreparaDtsRevision();
            // string sSql = "", 
            string sIdProducto = "", sCodigoEAN = "", sDescripcion = "";
            // int iRenglon = 0, 
            int iCantidad = 0, iCantidadLotes = 0;

            for (int i = 1; i < myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sDescripcion = myGrid.GetValue(i, (int)Cols.Descripcion);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                iCantidadLotes = 0;
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);
                foreach (clsLotes L in ListaLotes)
                {
                    iCantidadLotes += L.Cantidad;
                }

                if (iCantidad != iCantidadLotes)
                {
                    bRegresa = false;
                    object[] obj = { sIdProducto, sCodigoEAN, sDescripcion, iCantidad, iCantidadLotes };
                    dtsProductosDiferencias.Tables[0].Rows.Add(obj);
                }
            }

            // Se encontraron diferencias 
            if (!bRegresa)
            {
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, el Traspaso no puede ser completado.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Validacion de informacion

        #region Guardar datos traspaso 
        private bool GrabarTraspasoEncabezado()
        {
            bool bRegresa = true;
            string sFolio = SKU.Foliador;
            string sSql = ""; 

            sSql = string.Format("Exec spp_Mtto_TraspasosEnc \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTraspaso = '{3}',\n" +
                "\t@IdSubFarmaciaOrigen = '{4}', @IdSubFarmaciaDestino = '{5}', @FolioMovtoInv = '{6}', @FolioMovtoInvRef = '{7}', @FolioTraspasoRef = '{8}',\n" +
                "\t@TipoTraspaso = '{9}', @IdPersonal = '{10}', @Observaciones = '{11}', @SubTotal = '{12}', @Iva = '{13}', @Total = '{14}' \n",
                sEmpresa, sEstado, sFarmacia, sFolio, cboSubFarmaciaOrigen.Data, cboSubFarmaciaDestino.Data,
                sFolioMovto, "", "", sIdTipoMovtoInv, DtGeneral.IdPersonal, txtObservaciones.Text, 
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text) 
                );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "GrabarTransferenciaEncabezado()");
            }
            else
            {
                leer.Leer();
                sFolioTransferencia = leer.Campo("FolioTraspaso");
                //txtFolio.Text = sFolioTransferencia.Substring(3);
                sMensajeGrabar = leer.Campo("Mensaje");


                bRegresa = GrabarTraspasoDetalle();
            }

            return bRegresa;
        }

        private bool GrabarTraspasoDetalle()
        {
            bool bRegresa = false;
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
                    sSql = string.Format("Exec spp_Mtto_TraspasosDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTraspaso = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @Cantidad = '{7}', @CostoUnitario = '{8}',\n" +
                        "\t@TasaIva = '{9}', @SubTotal = '{10}', @ImpteIva = '{11}', @Importe = '{12}' \n", 
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia, 
                        sIdProducto, sCodigoEAN, i, iCantidad, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = GrabarTraspasoDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto); 
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarTraspasoDetalleLotes(int Renglon, string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                // Registrar el producto en las tablas de existencia 
                //sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                //    sEstado, sFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');

                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_TraspasosDetLotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}',\n" + 
                        "\t@FolioTraspaso = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}', @Cantidad = '{9}', @SKU = '{10}' \n",
                       sEmpresa, sEstado, sFarmacia, cboSubFarmaciaOrigen.Data, sFolioTransferencia, IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Cantidad.ToString(), L.SKU);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = true; 
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarTraspasoDetalleLotes_Ubicaciones(L, Renglon);
                            if(!bRegresa)
                            {
                                break; 
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarTraspasoDetalleLotes_Ubicaciones(clsLotes Lote, int Renglon)
        {
            bool bRegresa = false;
            string sSql = "";
            int iOpcion = 0;

            iOpcion = 1;

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_TraspasosDet_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}',\n" +
                        "\t@FolioTraspaso = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}',\n" +
                        "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepaño = '{11}', @Cantidad = '{12}', @iOpcion = '{13}', @SKU = '{14}' \n", 
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioTransferencia,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, Renglon, L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, iOpcion, L.SKU);

                    bRegresa = leer.Exec(sSql); 
                    if (!bRegresa)
                    {
                        break;
                    }
                }
            }

            return bRegresa;

        }
        #endregion Guardar datos traspaso

        #region Guardar datos tranferencia auxiliar
        //private bool GrabarDetalleEnvioTransferencia()
        //{
        //    string sSql = string.Format(" Exec spp_Mtto_TransferenciasEnvioGenerar '{0}', '{1}', '{2}', '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
        //    return leer.Exec(sSql);
        //}

        #endregion Guardar datos tranferencia auxiliar

        #region Guardar movimiento de inventario 
        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 
            bool bRegresa = false;
            string sSql = "";

            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if(Aplicar)
                Inv = AfectarInventario.Aplicar;

            if(AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",  
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);


            bRegresa = leer.Exec(sSql);

            if(DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioMovto);
            }

            return bRegresa;
        }

        private bool GrabarMovtoEncabezado()
        {
            bool bRegresa = true;
            string sSql = "";

            SKU.Reset(); 

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text), 1, SKU.SKU);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");

                SKU.SKU = leer.Campo("SKU");
                SKU.Foliador = leer.Campo("Foliador");
                SKU.FolioMovimiento = leer.Campo("Folio");
                SKU.TipoDeMovimiento = sIdTipoMovtoInv; 

                bRegresa = GrabarMovtoDetalle();
            }

            return bRegresa;
        }
        
        private bool GrabarMovtoDetalle()
        {
            bool bRegresa = false;
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
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', @Cantidad = '{8}', \n" +
                        "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                        nTasaIva, iCantidad, nCosto, nImporte, 'A');

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = GrabarMovtoDetalleLotes(sIdProducto, sCodigoEAN, nCosto);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                        "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', L.SKU);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = true;
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarMovtoDetalleLotes_Ubicaciones(L);
                            if(!bRegresa)
                            {
                                break; 
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes_Ubicaciones(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {                   
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', \n" +
                        "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', L.SKU);

                    bRegresa = leer.Exec(sSql); 
                    if (!bRegresa)
                    {
                        break;
                    }                    
                }
            }

            return bRegresa;

        }
        #endregion Guardar movimiento de inventario

        #region Cargar Datos 
        //private bool CargarDatosFarmacia()
        //{
        //    bool bRegresa = false;
        //    if (leer.Campo("FarmaciaStatus").ToUpper() == "C")
        //    {
        //        General.msjUser("La Farmacia seleccionada actualmente se encuentra cancelada,\nno es posible generar la transferencia.");
        //        txtFarmaciaDestino.Text = "";
        //        lblFarmaciaDestino.Text = "";
        //    }
        //    else
        //    {
        //        // Si la Farmacia Conectada Es Almacen no se valida la Farmacia Destino 
        //        if (!DtGeneral.EsAlmacen)
        //        {
        //            if (leer.CampoBool("EsAlmacen"))
        //            {
        //                General.msjUser("La Farmacia seleccionada esta configurada como Almacén,\nno es posible generar la transferencia de Farmacia a Almacén.");
        //                txtFarmaciaDestino.Text = "";
        //                lblFarmaciaDestino.Text = "";
        //            }
        //            else
        //            {
        //                bRegresa = true;
        //                txtFarmaciaDestino.Enabled = false;
        //                txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
        //                lblFarmaciaDestino.Text = leer.Campo("Farmacia");
        //            }
        //        }
        //        else
        //        {
        //            bRegresa = true;
        //            txtFarmaciaDestino.Enabled = false;
        //            txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
        //            lblFarmaciaDestino.Text = leer.Campo("Farmacia");
        //        }
        //    }
        //    return bRegresa;
        //}

        private void CargarDetallesTraspaso()
        {
            // myGrid.Limpiar(false);
            leer.DataSetClase = query.FolioTraspasoDetalles(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTraspaso");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDetallesLotesTraspaso();
            }
            Totalizar();
        }

        private void CargarDetallesLotesTraspaso()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTraspasoDetallesLotes(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesLotesTraspaso");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.FolioDetLotes_Traspasos_Ubicaciones(sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesLotesTraspaso()");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        #endregion Cargar Datos
        
        private void FrmTraspasosSubAlmacen_Load(object sender, EventArgs e)
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
                    leer.DataSetClase = query.FolioTraspaso(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "txtFolio_Validating_1");
                    if (leer.Leer())
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true);
                        txtFolio.Enabled = false;
                        txtFolio.Text = Fg.PonCeros(leer.Campo("Folio"), 8);

                        cboSubFarmaciaOrigen.Enabled = false; 
                        cboSubFarmaciaOrigen.Data = leer.Campo("IdSubFarmaciaOrigen");
                        cboSubFarmaciaDestino.Enabled = false; 
                        cboSubFarmaciaDestino.Data = leer.Campo("IdSubFarmaciaDestino");
                        txtObservaciones.Text = leer.Campo("Observaciones");
                        txtObservaciones.Enabled = false;

                        CargarDetallesTraspaso();
                    }
                    else
                    {
                        txtFolio.SelectAll();
                        txtFolio.Focus();
                    }
                }
            }

        }

        //private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        //{
        //    bool bExito = false;
        //    myGrid.Limpiar(false);

        //    if (txtFarmaciaDestino.Text.Trim() != "")
        //    {
        //        if (Fg.PonCeros(txtFarmaciaDestino.Text, 4) == DtGeneral.FarmaciaConectada)
        //        {
        //            General.msjUser("No se puede generar transferencia a la farmacia origen, verifique.");
        //            e.Cancel = true;
        //        }
        //        else
        //        {
        //            leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, sEstado, sFarmacia, txtFarmaciaDestino.Text, "txtFarmaciaDestino_Validating");
        //            if (leer.Leer())
        //            {
        //                sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 
        //                bExito = CargarDatosFarmacia();
        //                // myGrid.Limpiar(true); 
        //            }
        //        }
        //    }

        //    if (!bExito)
        //    {
        //        txtFarmaciaDestino.Text = "";
        //        lblFarmaciaDestino.Text = "";
        //        // e.Cancel = true;
        //        //txtFarmaciaDestino.Focus(); 
        //    }
        //    else
        //    {
        //        myGrid.Limpiar(true); 
        //    }
        //}

        //private void txtFarmaciaDestino_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F1)
        //    {
        //        leer.DataSetClase = ayuda.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, sEstado, sFarmacia, "txtFarmaciaDestino_KeyDown");
        //        if (leer.Leer())
        //            CargarDatosFarmacia();
        //    }
        //}

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

                        // Administracion de Mach4 
                        if (e.KeyCode == Keys.F10)
                        {
                            if (bEsClienteInterface && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
                            {
                                string sIdProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
                                string sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                                if (sIdProducto != "")
                                {
                                    // IMachPtoVta.Show(sIdProducto, sCodigoEAN);
                                    mostrarOcultarLotes();
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            bool bEsEAN_Unico = true; 


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
                                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                }
                                else
                                {
                                    if (!bEsEAN_Unico)
                                    {
                                        leer.GuardarDatos(1, "CodigoEAN", sValor); 
                                        //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                    }

                                    CargarDatosProducto();
                                }
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

                    //////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    //////if (IMach4.EsClienteIMach4)
                    //////{
                    //////    if (bEsMach4)
                    //////    {
                    //////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRow);
                    //////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                    //////    }
                    //////}

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

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, cboSubFarmaciaOrigen.Data, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
                    if (query.Ejecuto)
                    {
                        leer.Leer();
                        Lotes.AddLotesUbicaciones(leer.DataSetClase);
                    }
                }

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

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show(); 

                    //////// Administracion de Mach4
                    //////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    //////{
                    //////    if (IMachPtoVta.RequisicionRegistrada)
                    //////        Lotes.Show();
                    //////}
                    //////else
                    //////{
                    //////    Lotes.Show();
                    //////}

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

        #region CargaSubFarmacias

        private void CargaSubFarmaciaOrigen()
        {
            cboSubFarmaciaOrigen.Clear();
            cboSubFarmaciaOrigen.Add("0", "<< Seleccione >>");

            leer.DataSetClase = query.SubFarmacias(sEstado, sFarmacia, "", "CargaSubFarmaciaOrigen()");

            if (leer.Leer())
            {
                cboSubFarmaciaOrigen.Add(leer.DataSetClase, true);
            }

            cboSubFarmaciaOrigen.SelectedIndex = 0;
        }

        private void CargaSubFarmaciaDestino()
        {
            cboSubFarmaciaDestino.Clear();
            cboSubFarmaciaDestino.Add("0", "<< Seleccione >>");

            leer.DataSetClase = query.SubFarmacias(sEstado, sFarmacia, "", "CargaSubFarmaciaOrigen()");

            if (leer.Leer())
            {
                cboSubFarmaciaDestino.Add(leer.DataSetClase, true);
            }

            cboSubFarmaciaDestino.SelectedIndex = 0;
        }

        #endregion CargaSubFarmacias

        #region Generar Traspaso de Entrada

        private bool GenerarTraspasoEntrada()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_TraspasoGenerarEntrada  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTraspaso = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia );

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        #endregion Generar Traspaso de Entrada

        private void cboSubFarmaciaOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubFarmaciaOrigen.SelectedIndex != 0)
            {
                cboSubFarmaciaOrigen.Enabled = false; 
                myGrid.Limpiar(true);
            }
        }

    } // Clase 
}
