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
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllTransferenciaSoft.IntegrarInformacion;

namespace Farmacia.Devoluciones_De_Transferencias
{
    public partial class FrmDevolucionDeTransferencias_Entrada : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10
        }

        private enum ColsManual
        {
            Ninguna = 0,
            CodEAN = 1, ClaveSSA = 2, Descripcion = 3, Presentacion = 4, Cantidad = 5
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerUBI;
        clsLeer leerTransf;
        clsLeer leerTransf_Integrar;
        clsGrid myGrid;
        clsGrid myGridManual;
        clsCodigoEAN EAN = new clsCodigoEAN();
        // string sMsjEanInvalido = "";

        wsFarmacia.wsCnnCliente conexionWebTransferencia;
        string sUrlTransferencia = "";

        int iPasillo = 0, iEstante = 0, iEntrepano = 0;
        string FolioOrigen = "", FarmaciaOrigen = "";

        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);

        bool bTransferenciaInterestatal = false;
        int iEsInterestatal = 0;

        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // bool bExisteMovto = false;
        bool bEstaCancelado = false;
        bool bMovtoAplicado = false;
        bool bEstaCanceladoTrans = false;
        bool bEstaRegistrado = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";
        string sFolioTE = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdTipoMovtoInv = "EDT";
        string sTipoES = "E";
        string sTipoTransferencia = "EDT";
        string sIdProGrid = "";
        bool bEntradaSalida = false;
        string sCveRenapo = DtGeneral.ClaveRENAPO;

        bool bCapturaDeLotes = false;
        bool bModificarCantidades = false;
        bool bPermitirSacarCaducados = false;
        bool bEsConsignacion = false;
        TiposDeInventario tipoDeInventario = TiposDeInventario.Todos;

        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias; // = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU;

        TipoDevolucion tpDevolucion = TipoDevolucion.TransferenciaDeEntrada;
        DllFarmaciaSoft.wsHuellas.wsHuellas validarHuella = null;
        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        clsMotivosDevoluciones motivodev;

        string sNombrePosicion = "TS_DEVOLUCION";
        string sMovtoSREU = "SRDT";
        string sMovtoEREU = "ERDT";
        string sFolioREUSalida = "";
        string sFolioREUEntrada = "";
        #endregion variables

        public FrmDevolucionDeTransferencias_Entrada()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            conexionWebTransferencia = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWebTransferencia.Url = General.Url;

            myGrid = new clsGrid( ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;      
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            myGridManual = new clsGrid(ref grdProductosManual, this);
            myGridManual.EstiloGrid(eModoGrid.ModoRow);
            myGridManual.BackColorColsBlk = Color.White;
            grdProductosManual.EditModeReplace = true;
            myGridManual.AjustarAnchoColumnasAutomatico = true;

            leerTransf_Integrar = new clsLeer(ref cnn);
            leerTransf = new clsLeer(ref cnn);
            leerUBI = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);

            motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);
        }

        #region Botones 
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if (bExito)
            {

                SKU = new clsSKU();
                SKU.IdEmpresa = sEmpresa;
                SKU.IdEstado = sEstado;
                SKU.IdFarmacia = sFarmacia;
                SKU.TipoDeMovimiento = sIdTipoMovtoInv;

                bEntradaSalida = false;
                bCapturaDeLotes = false;
                bModificarCantidades = false; 
                bPermitirSacarCaducados = false;
                bEsConsignacion = false;
                tipoDeInventario = TiposDeInventario.Todos; 
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento, tipoDeInventario);
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;
                Lotes.sPosicionEstandar = sNombrePosicion;

                motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);
                // Acitvar barra de menu 
                IniciarToolBar(false, false, false, false);
                                
                bEstaCancelado = false;
                bMovtoAplicado = false;
                
                myGrid.Limpiar(false);
                myGridManual.Limpiar(false);
                Fg.IniciaControles();
                lblStatusTransferencia.Visible = false; 
                 
                dtpFechaRegistro.Enabled = false;
                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;
                cboMotivosDev.SelectedIndex = 0;
                cboEstados.Data = DtGeneral.EstadoConectado;
                txtFarmaciaOrigen.Focus();

            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Transferencias)
        {
            if (GnFarmacia.ManejaUbicaciones)
            {
                if (GnFarmacia.ManejaUbicacionesEstandar)
                {
                    if (!DtGeneral.CFG_UbicacionesEstandar)
                    {
                        Guardar = false;
                    }
                }
            }
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = false; // Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnObtenerTransferencias.Enabled = Transferencias;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnGenera = btnGenerarPaqueteDatos.Enabled;


            if (validarDatos())
            {
                if (cnn.Abrir())
                {
                    // Apagar la barra completa 
                    IniciarToolBar();

                    cnn.IniciarTransaccion();

                    if (GrabarEncabezadoDevolucion())
                    {
                        if (GrabarEncabezado())
                        {
                            if (Grabar_Motivos_Devolucion())
                            {
                                bExito = AfectarExistencia(true, false);
                            }

                            if (bExito && GnFarmacia.ManejaUbicaciones)
                            {
                                bExito = ReubicacionDevoluciones();
                            }
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        txtFolio.Text = SKU.Foliador; // Fg.Right(sFolioMovto, 8); //sFolioMovto.Substring(2); 
                        IniciarToolBar(false, false, true, true);
                        General.msjUser("Información guardada satisfactoriamente.  " + sFolioMovto);
                        
                        ImprimirReubicacion(1);
                        ImprimirReubicacion(2);
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        //txtFolioEntrada.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Error al guardar información de devolución.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnGenera);
                    }

                    cnn.Cerrar();
                }
                else
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjAviso("Error de conexión. Favor de verificar.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Folio no válido. Favor de verificar.");
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            FrmDevolucionesImpresion Devoluciones;
            Devoluciones = new FrmDevolucionesImpresion();
            Devoluciones.MostrarPantalla(("TS" + txtFolioOrigen.Text.Trim()), tpDevolucion, (int)TipoDeVenta.Ninguna);
        }
        #endregion Botones

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (txtFolioOrigen.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio Origen no válido. Favor de verificar.");
                txtFolioOrigen.Focus();
            }

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio Entrada no válido. Favor de verificar.");
                txtFolio.Focus();
            }

            if (bRegresa && cboMotivosDev.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccionar motivo devolución. Favor de verificar.");
                cboMotivosDev.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capturar observaciones. Favor de verificar.");
                txtObservaciones.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "Usuario sin permisos para realizar devolucion. Favor de verificar.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("Entrada_Devolucion_Transferencia", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("Entrada_Devolucion_Transferencia", sMsjNoEncontrado);
            }

            //if (bRegresa && !motivodev.Marco)
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado motivos de Devolución, verifique.");
            //}

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
            }

            return bRegresa;
        }

        private bool validarDatosCancelacion()
        {
            bool bRegresa = true;

            if (bRegresa && (txtFolio.Text.Trim() == "*" || txtFolio.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Folio no válido. Favor de verificar.");
            }

            if (bRegresa && bEstaCancelado)
            {
                bRegresa = false;
                General.msjUser("Folio cancelado.\n No se pueden realizar cambios.");
            }

            if (bRegresa && General.msjCancelar("¿ Desea cancelar el folio ?") == DialogResult.No)
            {
                bRegresa = false;
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

            if ( !bRegresa )
                General.msjUser("Capture algun producto \n y/o capturare cantidades mayor a 0.");

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
                General.msjAviso("Se encontraron diferencias en la captura de Productos; No se puede continuar.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }
        #endregion Validacion de datos

        #region Impresion de informacion 
        private void ImprimirInventario()
        {
            bool bRegresa = false; 
            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirDevolucionTE()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_DevolucionesDeTransferencias.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", ( sIdTipoMovtoInv + txtFolio.Text));

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 
                                
                if (bRegresa)
                {
                    LimpiarPantalla(false);
                }
                else
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Error al cargar Informe.");
                    }
                }
            }
        }
        #endregion Impresion de informacion

        #region Grabar informacion 
        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 
            string sSql = "";
            bool bRegresa = false;
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            
            sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bRegresa = leer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioMovto);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioMovto);
            }
            return bRegresa;
        }        

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;
            string sSql = "";

            SKU.Reset();
            sFolioMovto = Fg.Right(sFolioMovto, 8);

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text), 1, "");


            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");
                // txtFolio.Text = sFolioMovto.Substring(2);
                
                SKU.SKU = leer.Campo("SKU");
                SKU.FolioMovimiento = leer.Campo("Folio");
                SKU.Foliador = leer.Campo("Foliador");
                SKU.TipoDeMovimiento = sIdTipoMovtoInv;

                bRegresa = GrabarDetalle();
            }
            
            return bRegresa;
        }

        private bool GrabarDetalle()
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

                if (sIdProducto != "" && iCantidad > 0)
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
                        bRegresa = GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach(clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
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
                            bRegresa = GrabarDetalleLotesUbicaciones(L);
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

        private bool GrabarDetalleLotesUbicaciones(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}',\n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, L.SKU);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
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
            }

            return bRegresa;

        }

        private bool Grabar_Motivos_Devolucion()
        {
            bool bRegresa = true;
            string sTipoMov = "", sMotivo = "";
            int iMarca = 0;

            leer.DataSetClase = motivodev.Retorno;

            while (leer.Leer())
            {
                iMarca = 0;

                iMarca = leer.CampoInt("Marca");
                sTipoMov = leer.Campo("IdMovto");
                sMotivo = leer.Campo("IdMotivo");

                if (iMarca == 1)
                {
                    string sSql = string.Format(" Exec spp_Mtto_MovtosInv_Adt_Devoluciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @IdMotivo = '{5}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, sTipoMov, sMotivo);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Grabar informacion

        #region Eventos de Formulario 
        private void FrmInventarioInicial_Load(object sender, EventArgs e)
        {
            Carga_UbicacionEstandar();
            CargarMotivosDevolucion();
            CargarComboEstados();
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

        #endregion Eventos de Formulario        

        #region Cargar_Datos_Varios             
        private void CargarMotivosDevolucion()
        {
            string sSql = "";

            cboMotivosDev.Clear();
            cboMotivosDev.Add();

            sSql = string.Format(" Select IdMotivo, ( IdMotivo + ' -- ' + Descripcion ) as Motivo From CatMotivos_Dev_Transferencia " +
	                                " Order By IdMotivo ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMotivosDevolucion()");
                General.msjError("Error al consultar motivos de devolución.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboMotivosDev.Add(leer.DataSetClase, true, "IdMotivo", "Motivo");
                }
            }

            cboMotivosDev.SelectedIndex = 0;
        }
        #endregion Cargar_Datos_Varios

        #region Manejo Grid           
        private void Totalizar()
        {
            txtSubTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.Importe).ToString(sFormato);
            txtIva.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva).ToString(sFormato);
            txtTotal.Text = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal).ToString(sFormato);
        }

        #endregion Manejo Grid 

        #region Manejo de lotes  
        
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
                    Lotes.EsEntrada = bEntradaSalida;
                    Lotes.EsConsignacion = bEsConsignacion;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bCapturaDeLotes;  //  chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCantidades;  

                    // Solo para Movientos Especiales 
                    Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados; 
                    
                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucionDeVentas;

                    // Aplica solo para las Entrada por Consignacion    
                    Lotes.TipoDeInventario = tipoDeInventario; 

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

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

        #region Cargar_TE
        private void txtFolioEntrada_Validating(object sender, CancelEventArgs e)
        {
            //if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            //{
            //    txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
            //}
            //else
            //{

            //    //sSql = string.Format("SELECT * FROM VentasEnc (nolock) WHERE FolioVenta= '{0}' AND IdEstado='{1}' AND IdFarmacia='{2}'  ", Fg.PonCeros(txtFolio.Text, 8), Fg.PonCeros(sEstado,2), Fg.PonCeros(sFarmacia,4)); 
            //    leer.DataSetClase = query.FolioTransferenciaDetallesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text.Trim(), sIdTipoMovtoInv, txtFolio.Text, "txtFolioEntrada_Validating()");
            //    if (leer.Leer())
            //    {
            //        try
            //        {
            //            txtFarmaciaOrigen.Enabled = false;
            //            txtFolioOrigen.Enabled = false;
            //            txtFolio.Enabled = false;
            //            bModificarCantidades = true;
            //            txtObservaciones.Enabled = false;
            //            cboMotivosDev.Enabled = false;
            //            IniciarToolBar(true, false, false, false);

            //            dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");
            //            txtFolioOrigen.Text = leer.Campo("FolioTransferenciaRef").Substring(3);
            //            cboEstados.Data = leer.Campo("IdEstadoRecibe");
            //            txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
            //            lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");
            //            txtFolio.Text = leer.Campo("FolioDevolucion").Substring(3);
            //            cboMotivosDev.Data = leer.Campo("IdMotivo");
            //            txtObservaciones.Text = leer.Campo("Observaciones");

            //            //txtObservaciones.Text = leer.Campo("Observaciones");
            //            //txtObservaciones.Enabled = false;
            //            //sStatus = "Registrada";
            //            clsLeer leerTemp = new clsLeer();
            //            leerTemp.DataTableClase = leer.Tabla(2);

            //            myGrid.LlenarGrid(leerTemp.DataSetClase);

            //            //myGrid.AnchoColumna(Cols.Cantidad, 0);

            //            btnGenerarPaqueteDatos.Enabled = true;


            //        }
            //        catch
            //        (Exception ex)
            //        {
            //            Error.GrabarError(ex, "txtFolioDevolucion_Validating()");
            //            General.msjError("Error al cargar encabezado.");
            //        }


            //        //CargarDetallesTransferencia();


            //    }
            //    else
            //    {
            //        txtFolio.Text = "";
            //        txtFolio.Focus();
            //    }
            //}

            
            }

        private void txtFolioOrigen_Validating(object sender, CancelEventArgs e)
        {
            string sStatus = "";

            if (txtFolioOrigen.Text.Trim() != "" && txtFolioOrigen.Enabled)
            {
                IniciarToolBar(false, false, false, false);
                bEstaCanceladoTrans = false;
                bEstaRegistrado = false;

                sFolioTE = Fg.PonCeros(sEstado, 2) + Fg.PonCeros(txtFarmaciaOrigen.Text, 4) + sCveRenapo + "EDT" + Fg.PonCeros(txtFolioOrigen.Text, 8); // 250001SLTS
                sFolioTE = "SDT" + Fg.PonCeros(txtFolioOrigen.Text, 8); // 250001SLTS
                leer.DataSetClase = query.FolioTransferenciaEntradaDev(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    if (sFarmacia != leer.Campo("IdFarmaciaRecibe"))
                    {
                        General.msjUser("Folio Traspaso no valido. Favor de verificar.");

                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        sStatus = leer.Campo("Status").ToUpper();
                        IniciarToolBar(false, false, false, false);

                        cboEstados.Enabled = false;
                        txtFarmaciaOrigen.Enabled = false;
                        txtFolioOrigen.Enabled = false;
                        txtFolioOrigen.Text = Fg.Right(leer.Campo("Folio"), 8);
                        txtFolio.Enabled = false;
                        txtFolio.Text = Fg.Right(leer.Campo("FolioReferencia"), 8);  //"*"; 
                        cboMotivosDev.Data = leer.Campo("IdMotivo");
                        cboMotivosDev.Enabled = false;

                        lblFolioOrigen.Text = leer.Campo("FolioOrigen");

                        if (txtFolio.Text != "*")
                        {
                            cboMotivosDev.Data = leer.Campo("IdMotivo");
                            cboMotivosDev.Enabled = false;
                            txtObservaciones.Text = leer.Campo("Observaciones");
                            txtObservaciones.Enabled = false;
                            //txtObservaciones.Enabled = false;
                            IniciarToolBar(false, false, true, false);
                        }

                        CargaDetalleTransferenciaEntrada();

                        


                        // Asegurar que solo se puedan Guardar las transferencias no procesadas 
                        if (txtFolio.Text == "*")
                        {
                            IniciarToolBar(true, false, false, false);

                            ////X MostrarFactor(bAplicaFactor);
                        }


                        if (sStatus == "C")
                        {
                            bEstaCanceladoTrans = true;
                            lblStatusTransferencia.Visible = true;
                            lblStatusTransferencia.Text = "CANCELADO";
                            General.msjUser("Folio cancelado. No se pueden realizar cambios.");
                            txtFolioOrigen.Text = "";

                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                        }


                        // else if (sStatus == "E" || txtFolio.Text != "*" )  // Cambio a raiz del ajuste del metodo de envio.
                        //else 
                        //if (txtFolio.Text != "*") // 2K110607-1118 Jesus Diaz 
                        //{
                        //    // Cargar la informacion que se registro 
                        //    CargarTransferencia();

                        //    IniciarToolBar(false, false, true, false);
                        //    bEstaRegistrado = true;
                        //    lblStatusTransferencia.Visible = true;
                        //    lblStatusTransferencia.Text = "REGISTRADO";
                        //    btnGenerarPaqueteDatos.Enabled = true;
                        //    General.msjUser("El folio de transferencia ya fue registrado, no es posible generar la entrada de nuevo.");
                        //    //txtFolioOrigen.Text = "";

                        //    if (e != null)
                        //    {
                        //        e.Cancel = true;
                        //    }
                        //}
                    }
                }
                //else
                //{
                //    string sMsj = "¿ Desea Realizar La Busqueda en el Origen de la Transferencia ?";
                //    ////FolioOrigen = "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8);
                //    ////FarmaciaOrigen = Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

                //    if (General.msjConfirmar(sMsj) == DialogResult.Yes)
                //    {
                //        if (BuscarTransferenciaEnOrigen())
                //        {
                //            txtFolioOrigen_Validating(this, null);
                //        }
                //    }
                //    else
                //    {
                //        btnObtenerTransferencias.Enabled = true;
                //    }
                //}
            }
        }
        //{
        //    bool bRegresa = true;
        //    string sStatus = "";

        //    lblFolioEntrada.Visible = false;
        //    leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolioOrigen.Text, "TS", false, "CargarTransferencia");
        //    if (!leer.Leer())
        //    {
        //        bRegresa = false;
        //    }
        //    else
        //    {
        //        txtFarmaciaOrigen.Enabled = false;
        //        txtFolioOrigen.Text = leer.Campo("Folio").Substring(2);
        //        txtFolioOrigen.Enabled = false;
        //        //txtFolioEntrada.Enabled = false;
        //        bModificarCantidades = true;
        //        IniciarToolBar(true, false, false);

        //        dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");
        //        //txtFolioEntrada.Text = leer.Campo("Folio").Substring(2);
        //        txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
        //        lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");
        //        //txtFolioOrigen.Text = leer.Campo("FolioTransferenciaRef").Substring(2);

        //        txtObservaciones.Text = leer.Campo("Observaciones");
        //        //txtObservaciones.Enabled = false;
        //        sStatus = leer.Campo("Status").ToUpper();

        //        if (!leer.CampoBool("TransferenciaAplicada"))
        //        {
        //            IniciarToolBar(false, false, false);
        //            lblStatusTransferencia.Visible = true;
        //            lblStatusTransferencia.Text = "Folio de transferencia sin aplicar";
        //        }

        //        CargarDetallesTransferencia();
        //        btnEjecutar.Enabled = ExistenDev_Parciales();

        //        btnMotivosDev.Enabled = true;


        //        txtFolioEntrada.Focus();
        //    }

        //    return bRegresa;
        //}

        private void CargaDetalleTransferenciaEntrada()
        {
            leer.DataSetClase = query.FolioTransferenciaEntradaDetalleDev(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "CargaDetalleTransferenciaEntrada");
            myGrid.Limpiar(false);
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                myGridManual.LlenarGrid(leer.Tabla(2));

                CargaDetalleLotesTransferenciaEntrada();
                

                //if (bImplementaCodificacion)
                //{
                //    CargaDetalleTransferenciaEntradaUUIDS();
                //}
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            Totalizar();
        }

        private void CargaDetalleLotesTransferenciaEntrada()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaEntradaDetalleLotesDev(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "CargaDetalleLotesTransferenciaEntrada");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.FolioTransferenciaEntradaDetalleLotes_Ubicacionesdev
                    (
                        sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text,
                        iPasillo, iEstante, iEntrepano,
                        "CargaDetalleLotesTransferenciaEntrada"
                    );
                Lotes.DataSetUbicaciones = leer.DataSetClase;
            }
        }

        private void Carga_UbicacionEstandar()
        {
            string sFiltro = string.Format("  IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and NombrePosicion = '{3}' ",
                                            sEmpresa, sEstado, sFarmacia, sNombrePosicion);

            if (GnFarmacia.ManejaUbicaciones)
            {
                if (DtGeneral.CFG_UBI_Estandar != null)
                {
                    leer.DataSetClase = DtGeneral.CFG_UBI_Estandar;

                    leerUBI.DataRowsClase = leer.Tabla(1).Select(sFiltro);

                    if (leerUBI.Leer())
                    {
                        iPasillo = leerUBI.CampoInt("IdRack");
                        iEstante = leerUBI.CampoInt("IdNivel");
                        iEntrepano = leerUBI.CampoInt("IdEntrepaño");
                    }
                }
            }

        }

        private bool CargarTransferencia()
        {
            return CargarTransferencia(false, false);
        }

        private bool CargarTransferencia(bool MostrarMensaje, bool ValidarOrigen)
        {
            bool bRegresa = true;
            string sStatus = "";

            lblStatusTransferencia.Visible = false;
            leer.DataSetClase = query.FolioTransferenciaDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolio.Text, sIdTipoMovtoInv, txtFarmaciaOrigen.Text, "txtFolio_Validating_1");
            if (!leer.Leer())
            {
                bRegresa = false;
                txtFolioOrigen.Text = "";
            }
            else
            {
                cboEstados.Enabled = false;
                cboEstados.Data = leer.Campo("IdEstadoRecibe");
                txtFarmaciaOrigen.Enabled = false;
                txtFolioOrigen.Enabled = false;
                txtFolioOrigen.Enabled = false;
                IniciarToolBar(false, false, true, false);

                dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");
                txtFolio.Text = Fg.PonCeros(leer.Campo("Folio"), 8);
                txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
                lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");
                txtFolioOrigen.Text = leer.Campo("FolioTransferenciaRef").Substring(3);

                txtObservaciones.Text = leer.Campo("Observaciones");
                txtObservaciones.Enabled = false;
                sStatus = leer.Campo("Status").ToUpper();

                CargarDetallesTransferencia();


                if (MostrarMensaje)
                {
                    bEstaRegistrado = true;
                    lblFolioEntrada.Visible = true;
                    lblFolioEntrada.Text = "APLICADO";
                    btnGenerarPaqueteDatos.Enabled = true;
                    General.msjUser("Folio Aplicado. No se pueden realizar cambios.");
                }
            }

            return bRegresa;
        }

        private void CargarDetallesTransferencia()
        {
            myGrid.Limpiar(false);
            leer.DataSetClase = query.FolioTransferenciaDetallesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolio.Text, sIdTipoMovtoInv, txtFolioOrigen.Text, "CargarDetallesTransferencia");
            if (leer.Registros > 0)
            {
                myGrid.Limpiar();
                for (int i = 1; leer.Leer(); i++)
                {
                    //myGrid.LlenarGrid(leer.DataSetClase);
                    myGrid.AddRow();
                    myGrid.SetValue(i, (int)Cols.CodEAN, leer.Campo("CodigoEAN"));
                    myGrid.SetValue(i, (int)Cols.Codigo, leer.Campo("IdProducto"));
                    myGrid.SetValue(i, (int)Cols.Descripcion, leer.Campo("DescProducto"));
                    myGrid.SetValue(i, (int)Cols.TasaIva, leer.CampoInt("TasaIva"));
                    myGrid.SetValue(i, (int)Cols.Cantidad, leer.CampoDouble("Cantidad"));
                    //myGrid.SetValue(i, (int)Cols.Factor, leer.CampoInt("Factor"));
                    myGrid.SetValue(i, (int)Cols.Costo, leer.CampoDouble("Costo"));
                    myGrid.SetValue(i, (int)Cols.Importe, leer.CampoDouble("Importe"));
                    myGrid.SetValue(i, (int)Cols.ImporteIva, 0.0000);
                    myGrid.SetValue(i, (int)Cols.ImporteTotal, 0.0000);
                    myGrid.SetValue(i, (int)Cols.TipoCaptura, leer.CampoDouble("UnidadDeSalida"));
                }
                CargarDetallesLotesTransferencia();
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            Totalizar();
        }

        private void CargarDetallesLotesTransferencia()
        {
            Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaDetallesLotes(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolioOrigen.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                string sFolio = sIdTipoMovtoInv + Fg.PonCeros(txtFolioOrigen.Text, 8);
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones_TransferenciaDeEntrada(sEmpresa, sEstado, sFarmacia, sFolio, "CargarDetallesTransferencia");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }

        }

        //private void CargarDetallesTransferencia()
        //{
        //    myGrid.Limpiar(false);
        //    leer.DataSetClase = query.FolioTransferenciaDetallesDev(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolioOrigen.Text, sTipoTransferencia, "CargarDetallesTransferencia");
        //    if (leer.Leer())
        //    {
        //        myGrid.LlenarGrid(leer.DataSetClase);
        //        CargarDetallesLotesTransferencia();
        //    }

        //    myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
        //    Totalizar();
        //}

        //private void CargarDetallesLotesTransferencia()
        //{
        //    Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        //    leer.DataSetClase = clsLotes.PreparaDtsLotes();
        //    leer.DataSetClase = query.FolioTransferenciaDetallesLotesDev(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolioOrigen.Text, sTipoTransferencia, "CargarDetallesTransferencia");
        //    Lotes.AddLotes(leer.DataSetClase);

        //    if (GnFarmacia.ManejaUbicaciones)
        //    {
        //        string sFolio = sTipoTransferencia + Fg.PonCeros(txtFolioEntrada.Text, 8);
        //        leer.DataSetClase = query.FolioTransferenciaDetallesLotes_UbicacionesDev(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolioOrigen.Text, sTipoTransferencia, "CargarDetallesTransferencia");
        //        Lotes.AddLotesUbicaciones(leer.DataSetClase);
        //    }

        //}

        private bool ExistenDev_Parciales()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format(" Select * From DevolucionTransferenciasEnc (Nolock) " +
	                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioTransferencia = '{3}' ",
                     sEmpresa, sEstado, sFarmacia, (sTipoTransferencia + txtFolioOrigen.Text));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ExistenDev_Parciales()");
                General.msjError("Error al consultar devoluciones.");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    General.msjUser("Folio con devoluciones parciales.");
                }
            }

            return bRegresa;
        }
        #endregion Cargar_TE

        #region Evento_Folio_Devolucion
        #endregion Evento_Folio_Devolucion

        #region Guardar_Informacion_Devolucion
        private bool GrabarEncabezadoDevolucion()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasEnc @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', " +
                " @FolioTransferencia = '{4}', @FolioTransferenciaRef = '{5}', @FolioMovtoInv = '{6}', @TipoTransferencia = '{7}', @IdPersonal = '{8}', " +
                " @Observaciones = '{9}', @IdMotivo = '{10}', @SubTotal = '{11}', @Iva = '{12}', @Total = '{13}', @IdEstadoRecibe = '{14}', @IdFarmaciaDestino = '{15}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text, lblFolioOrigen.Text, ("SDT" + txtFolioOrigen.Text), "", sIdTipoMovtoInv,
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(), cboMotivosDev.Data,
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text),
                cboEstados.Data,
                txtFarmaciaOrigen.Text
                );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioMovto = leer.Campo("Folio");
                // txtFolio.Text = sFolioMovto.Substring(2);
                bRegresa = GrabarDetalleDevolucion();

                if (bRegresa)
                {
                    bRegresa = GrabarDetalleDevolucionManual();
                }

            }

            return bRegresa;
        }

        private bool GrabarDetalleDevolucionManual()
        {
            bool bRegresa = true;
            string sSql = "";
            string sClaveSSA = "", sCodigoEAN = "";
            int iCantidad = 0;


            for (int i = 1; i <= myGridManual.Rows; i++)
            {
                sClaveSSA = myGridManual.GetValue(i, (int)ColsManual.ClaveSSA);
                sCodigoEAN = myGridManual.GetValue(i, (int)ColsManual.CodEAN);
                iCantidad = myGridManual.GetValueInt(i, (int)ColsManual.Cantidad);

                if (sClaveSSA != "" && iCantidad > 0)
                {

                    sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasDet_Manual \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', \n" +
                        "\t@CodigoEAN = '{4}', @ClaveSSA = '{5}', @Cantidad = '{6}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto,
                        sCodigoEAN, sClaveSSA, iCantidad);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleDevolucion()
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

                //if (sIdProducto != "" && iCant_Dev > 0)
                {                   
                    sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @Cant_Enviada = '{7}', @Cant_Devuelta = '{8}',\n" +
                        "\t@CantidadEnviada = '{9}', @CostoUnitario = '{10}', @TasaIva = '{11}', @SubTotal = '{12}', @ImpteIva = '{13}',\n" +
                        "\t@Importe = '{14}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto,
                        sIdProducto, sCodigoEAN, i, iCantidad, 0, iCantidad, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = GrabarDetalleLotesDevolucion(sIdProducto, sCodigoEAN, nCosto, i);
                        if (!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotesDevolucion(string IdProducto, string CodigoEAN, double Costo, int Renglon)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasDet_Lotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @Cant_Enviada = '{7}', @Cant_Devuelta = '{8}',\n" +
                        "\t@CantidadEnviada = '{9}', @IdSubFarmacia = '{10}', @ClaveLote = '{11}', @EsConsignacion = '{12}', @SKU = '{13}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, IdProducto, CodigoEAN, Renglon, L.Cantidad, 0, L.Cantidad, L.IdSubFarmacia,
                        L.ClaveLote, L.EsConsignacion, L.SKU);
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
                            bRegresa = GrabarDetalleLotesUbicacionesDevolucion(L, Renglon);
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

        private bool GrabarDetalleLotesUbicacionesDevolucion(clsLotes Lote, int Renglon)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasDet_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @Cant_Enviada = '{7}', @Cant_Devuelta = '{8}',\n" +
                        "\t@CantidadEnviada = '{9}', @IdSubFarmacia = '{10}', @ClaveLote = '{11}', @EsConsignacion = '{12}', @IdPasillo = '{13}',\n" +
                        "\t@IdEstante = '{14}', @IdEntrepaño = '{15}', @SKU = '{16}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, L.IdProducto, L.CodigoEAN, Renglon, L.Cantidad, 0, L.Cantidad,
                        L.IdSubFarmacia, L.ClaveLote, Lote.EsConsignacion, L.Pasillo, L.Estante, L.Entrepano, L.SKU);

                    bRegresa = leer.Exec(sSql);
                    if(!bRegresa)
                    {
                        break;
                    }
                }
            }

            return bRegresa;

        }
        #endregion Guardar_Informacion_Devolucion

        #region Boton_Seleccionar_Motivos_Devolucion
        private void btnMotivosDev_Click(object sender, EventArgs e)
        {
            motivodev.MotivosDevolucion();
        }
        #endregion Boton_Seleccionar_Motivos_Devolucion

        #region Reubicaciones_De_Devoluciones
        private bool ReubicacionDevoluciones()
        {
            bool bRegresa = true;

            if (sNombrePosicion.Trim() != "")
            {
                bRegresa = false;

                if (Guarda_Reubicacion(1))
                {
                    bRegresa = Aplica_DesAplica_Existencia(sFolioREUSalida);
                }

                if (bRegresa && Guarda_Reubicacion(2))
                {
                    bRegresa = Aplica_DesAplica_Existencia(sFolioREUEntrada);
                }
            }

            return bRegresa;
        }

        private bool Guarda_Reubicacion(int Opcion)
        {
            bool bRegresa = true;
            string sSql = "", sTipoES = "", sFolio = "";
            string sIdMovto = sMovtoSREU;
            int iTipoMovto = 0;

            sTipoES = "S";

            if (Opcion == 2)
            {
                sIdMovto = sMovtoEREU;
                sTipoES = "E";
                iTipoMovto = 1;
            }

            sSql = string.Format(" Exec spp_Mtto_Reubicacion_Movtos_Devoluciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}',@IdPersonalRegistra = '{4}', \n" +
                "\t@IdTipoMovto_Inv = '{5}', @TipoES = '{6}', @NombrePosicion = '{7}', @TipoMovto = '{8}' \n",
                                sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdPersonalConectado, sIdMovto, sTipoES, sNombrePosicion, iTipoMovto);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");

                    if (Opcion == 1)
                    {
                        sFolioREUSalida = leer.Campo("Folio");
                        General.msjUser("Folio de Reubicación de salida generado satisfactoriamente : " + sFolio + "");
                    }
                    else
                    {
                        sFolioREUEntrada = leer.Campo("Folio");
                        General.msjUser("Folio de Reubicación de entrada generado satisfactoriamente : " + sFolio + "");
                    }

                }
            }

            return bRegresa;
        }

        private bool Aplica_DesAplica_Existencia(string Folio)
        {
            bool bRegresa = true;
            AfectarInventario Inv = AfectarInventario.Aplicar;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' \n",
                sEmpresa, sEstado, sFarmacia, Folio, (int)Inv, (int)Costo);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "Aplica_DesAplica_Existencia(string Folio)");
            }

            return bRegresa;
        }
        #endregion Reubicaciones_De_Devoluciones

        #region Impresion_ReUbicacion
        private void ImprimirReubicacion(int Opcion)
        {
            bool bRegresa = false;
            string sFolio = "";

            if (sFolioREUSalida.Trim() != "" && sFolioREUEntrada.Trim() != "")
            {
                sFolio = sFolioREUSalida;

                if (Opcion == 2)
                {
                    sFolio = sFolioREUEntrada;
                }

                DatosCliente.Funcion = "ImprimirReubicacion()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolio);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Error al cargar Informe.");
                }
            }

        }
        #endregion Impresion_ReUbicacion

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this);
        }

        private void btnGenerarPaqueteDatos_Click(object sender, EventArgs e)
        {
            GenerarPaqueteDeDatos();
        }

        private void GenerarPaqueteDeDatos()
        {
            string sMsj = string.Format("¿ Desea generar el paquete de datos para el Traspaso {0} ?", txtFolioOrigen.Text);
            sMsj = string.Format("¿ Desea generar el paquete de datos para el Traspaso {0} ?", txtFolioOrigen.Text);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (PreparaTransferenciaReenvio())
                {
                    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                    ClienteTransferencias.TransferenciasAutomaticasIntegradas(cboEstados.Data, Fg.PonCeros(txtFarmaciaOrigen.Text, 4), DtGeneral.EstadoConectado,
                        DtGeneral.FarmaciaConectada, "SDT" + Fg.PonCeros(txtFolioOrigen.Text, 8), true);
                    General.msjAviso("Generación de Paquete de Datos terminada.");

                    ClienteTransferencias.Abrir_Directorio_Transferencias();
                }
                // ClienteTransferencias.EnviarArchivos(); 
            }
        }

        private void txtFarmaciaOrigen_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;
            //linkUrlFarmacia.Text = "";
            string sInf_Origen = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada;
            string sInf_Destino = cboEstados.Data + Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

            iEsInterestatal = cboEstados.Data == DtGeneral.EstadoConectado ? 0 : 1;


            if (txtFarmaciaOrigen.Text.Trim() != "" && txtFarmaciaOrigen.Enabled)
            {
                if (sInf_Origen == sInf_Destino)
                {
                    General.msjUser("No se puede registrar un Traspaso de la Farmacia destino. Favor de verificar.");

                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstados.Data, sFarmacia, txtFarmaciaOrigen.Text, "txtFarmaciaOrigen_Validating");
                    if (leer.Leer())
                    {
                        // conexionWeb.Url = leer.Campo("UrlFarmacia");
                        sUrlTransferencia = leer.Campo("UrlFarmacia");
                        //linkUrlFarmacia.Text = sUrlTransferencia;
                        bExito = CargarDatosFarmacia();
                    }
                }
            }

            if (!bExito)
            {
                txtFarmaciaOrigen.Text = "";
                lblFarmaciaOrigen.Text = "";
                // e.Cancel = true;
            }
        }

        private bool CargarDatosFarmacia()
        {
            bool bRegresa = true;

            //bEsAlmacenUnidosis = DtGeneral.EsAlmacen && GnFarmacia.EsUnidadUnidosis;
            //bEsOrigenNormal = !leer.CampoBool("EsUnidosis"); //// && leer.CampoBool("EsAlmacen");


            //bAplicaFactor = GnFarmacia.EsUnidadUnidosis && bEsOrigenNormal ? true : false;
            //bAplicaFactor = bEsOrigenNormal ? true : false;

            ////if ( GnFarmacia.EsUnidadUnidosis && bEsOrigenNormal )
            ////{
            ////    bAplicaFactor = true; 
            ////}





            if (leer.Campo("FarmaciaStatus").ToUpper() == "C" && bRegresa)
            {
                General.msjUser("Farmacia inactiva. No se puede realizar el Traspaso.");
                txtFarmaciaOrigen.Text = "";
                txtFarmaciaOrigen.Focus();
                lblFarmaciaOrigen.Text = "";
                bRegresa = false;
            }

            if (bRegresa)
            {
                txtFarmaciaOrigen.Enabled = false;
                txtFarmaciaOrigen.Text = leer.Campo("IdFarmacia");
                lblFarmaciaOrigen.Text = leer.Campo("Farmacia");
            }
            return bRegresa;
        }

        private void txtFarmaciaOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstados.Data, sFarmacia, "txtFarmaciaOrigen_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosFarmacia();
                }
            }
        }

        private bool PreparaTransferenciaReenvio()
        {
            bool bRegresa = true;
            string sSql = "";
            string sFolioRev = "SDT" + txtFolio.Text;

            // sSql = string.Format("Update  Set Actualizado = 0 Where IdEstadoEnvia = '20' and IdFarmaciaEnvia = '0011' and IdFarmaciaRecibe = '0010' ");
            sSql = string.Format("Update DevolucionTransferenciasEnvioEnc Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaOrigen.Text, sFolioRev);

            sSql += string.Format("Update DevolucionTransferenciasEnvioDet Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaOrigen.Text, sFolioRev);

            sSql += string.Format("Update DevolucionTransferenciasEnvioDet_Lotes Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaOrigen.Text, sFolioRev);


            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaTransferenciaReenvio()");
                General.msjError("Ocurrió un error al preparar la transferencia para empaquetado.");
            }

            return bRegresa;
        }

        private void CargarComboEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(query.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias;
        }

        #region Buscar Transferencias Desde Origen
        private bool BuscarTransferenciaEnOrigen()
        {
            bool bRetorno = false, bSalir = false;
            bool bContinua = false, bTest = false;
            ////string FolioOrigen = "", FarmaciaOrigen = "";
            DataSet dtsTransferencia = null;  //= new DataSet();

            FolioOrigen = "SDT" + Fg.PonCeros(txtFolioOrigen.Text, 8);
            FarmaciaOrigen = Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

            try
            {
                // sUrlTransferencia = "http://lapjesus/wsCompras/wsFarmacia.asmx"; 
                conexionWebTransferencia = new Farmacia.wsFarmacia.wsCnnCliente();
                conexionWebTransferencia.Url = sUrlTransferencia;

                bTest = conexionWebTransferencia.TestConection();

                try
                {
                    dtsTransferencia = conexionWebTransferencia.InformacionTransferencias(sEstado, FarmaciaOrigen, sFarmacia, FolioOrigen);
                    bContinua = true;
                }
                catch (Exception ex2)
                {
                    Error.GrabarError(ex2.Message, "BuscarTransferenciaEnOrigen");
                    General.msjError("Error al consultar Traspaso.");
                }
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1.Message, "BuscarTransferenciaEnOrigen");
                General.msjAviso("Conexión fallida con la Unidad Origen. Favor de verificar.");
            }

            ////if (leer.SeEncontraronErrores())
            ////{
            ////    bContinua = false;
            ////    General.msjError("Ocurrió un error al Descargar la información de la Transferencia."); 
            ////}

            // Revisar la respuesta del Servidor de Transferencias 
            if (dtsTransferencia == null)
            {
                bContinua = false;
            }
            else
            {
                if (dtsTransferencia.Tables.Count == 1)
                {
                    leer.DataSetClase = dtsTransferencia;
                    if (leer.Leer())
                    {
                        if (leer.Campo("Salida") == "1")
                        {
                            if (!leer.CampoBool("Correcto"))
                            {
                                bContinua = false;
                                General.msjAviso(string.Format(" El folio de Transferencia {0} no pertenece a esta Unidad.\n\n" +
                                    "Pertenece a la Unidad {1}-{2}, favor de reportarlo a la Unidad Origen.",
                                    FolioOrigen, leer.Campo("IdFarmaciaRecibe"), leer.Campo("FarmaciaRecibe")));

                                txtFolioOrigen.Text = "";
                                txtFolioOrigen.Focus();
                            }
                        }
                        else
                        {
                            bContinua = false;
                            General.msjAviso(leer.Campo("Mensaje"));
                            txtFolioOrigen.Text = "";
                            txtFolioOrigen.Focus();
                        }
                    }
                }
            }


            if (bContinua)
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    for (int i = 0; i < dtsTransferencia.Tables.Count; i++)
                    {
                        leerTransf.DataTableClase = dtsTransferencia.Tables[i];
                        while (leerTransf.Leer())
                        {
                            string sCadena = leerTransf.Campo("Resultado");

                            if (!leerTransf_Integrar.Exec(sCadena))
                            {
                                bSalir = true;
                                break;
                            }
                            else
                            {
                                bRetorno = true;
                            }
                        }
                        if (bSalir)
                        {
                            bRetorno = false;
                            break;
                        }
                    }

                    if (bRetorno)
                    {
                        cnn.CompletarTransaccion();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leerTransf, "BuscarTransferenciaEnOrigen");
                        General.msjError("Error al Integrar información de Traspaso.");
                    }
                }
            }

            if (bRetorno)
            {
                // Cargar la Información de la Transferencia Solicitada 
                txtFolioOrigen_Validating(null, null);
            }
            else
            {
                txtFolioOrigen.Text = "";
            }

            return bRetorno;
        }
        #endregion Buscar Transferencias Desde Origen
    }
}
