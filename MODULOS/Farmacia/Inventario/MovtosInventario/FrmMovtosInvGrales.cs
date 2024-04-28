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

namespace Farmacia.Inventario
{
    public partial class FrmMovtosInvGrales : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsGrid myGrid;
        clsCodigoEAN EAN = new clsCodigoEAN();
        // string sMsjEanInvalido = "";

        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        // bool bExisteMovto = false;
        bool bEstaCancelado = false;
        bool bMovtoAplicado = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdTipoMovtoInv = "II";
        string sTipoES = "E";
        string sIdProGrid = "";
        bool bEntradaSalida = false;

        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        bool bCapturaDeLotes = false;
        bool bModificarCantidades = false;
        bool bPermitirSacarCaducados = false;
        bool bEsConsignacion = false;
        TiposDeInventario tipoDeInventario = TiposDeInventario.Todos;

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; // = new clsSKU(); 

        bool bGuardaMotivos = false;
        clsMotivosDevoluciones motivodev;

        bool bTieneSurtimientosActivos = false;
        string sMensajeConSurtimiento = "Se encontrarón folios de surtimiento pendientes de generar transferencia ó venta.\n\n" +
                    "No es posible generar un movimiento general de salida, verifique el status de los folios de surtimiento.";

        #endregion variables

        public FrmMovtosInvGrales()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;



            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            // Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        #region Botones 
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla, se perdera lo que este capturado. ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if (bExito)
            {
                bEntradaSalida = false;
                bCapturaDeLotes = false;
                bModificarCantidades = false;
                bPermitirSacarCaducados = false;
                bEsConsignacion = false;
                tipoDeInventario = TiposDeInventario.Todos;


                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento, tipoDeInventario);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;

                SKU = new clsSKU();
                SKU.IdEmpresa = sEmpresa;
                SKU.IdEstado = sEstado;
                SKU.IdFarmacia = sFarmacia;
                SKU.TipoDeMovimiento = sIdTipoMovtoInv;

                // Acitvar barra de menu 
                IniciarToolBar(false, false, false);
                // myGrid.BloqueaColumna(false, (int)Cols.Costo);

                // bExisteMovto = false;
                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGrid.Limpiar(false);
                Fg.IniciaControles();
                Fg.IniciaControles(this, false, FrameValorInventario);
                lblCancelado.Visible = false;
                chkAplicarInv.Enabled = true;
                chkAplicarInv.Checked = false;
                dtpFechaRegistro.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                btnMotivosDev.Enabled = false;
                motivodev = new clsMotivosDevoluciones(General.DatosConexion, TipoDevolucion.Ninguna, sEmpresa, sEstado, sFarmacia);

                cboTiposMovtoInventario.Focus();
                cboTiposMovtoInventario.Focus();
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = bTieneSurtimientosActivos ? false : Guardar;
            btnCancelar.Enabled = false; // Cancelar;
            btnImprimir.Enabled = Imprimir;
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


            if (validarDatos())
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    // Apagar la barra completa 
                    IniciarToolBar();

                    cnn.IniciarTransaccion();

                    if (GrabarEncabezado())
                    {
                        //if (chkAplicarInv.Checked)
                        //{
                        bExito = AfectarExistencia(true, bEntradaSalida);
                        //}
                        //else
                        //    bExito = true;
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        txtFolio.Text = SKU.Foliador; // Fg.Right(sFolioMovto, 8); //sFolioMovto.Substring(2); 
                        IniciarToolBar(false, false, true);
                        General.msjUser("Información guardada satisfactoriamente con el folio " + sFolioMovto);
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información del inventario.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            if (validarDatosCancelacion())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    if (GrabarCancelacion())
                    {
                        if (bMovtoAplicado)
                        {
                            bExito = AfectarExistencia(true, false);
                        }
                        else
                            bExito = true;
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        txtFolio.Text = sFolioMovto.Substring(2);
                        General.msjUser("Información cancelada satisfactoriamente con el folio " + sFolioMovto);
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar la información del inventario.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                }
            }
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Folio de Invetario inválido, verifique.");
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario();
        }
        #endregion Botones

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;

            //// Forzar la validación de Surtidos sólo para las SALIDAS 
            if (sTipoES == "S")
            {
                ////// Se quita esta validación, las Existencias mostradas contemplan Transitos y Surtidos ( Existencia - (Transitos+Surtidos)) 
                //if(DtGeneral.TieneSurtimientosActivos())
                //{
                //    bRegresa = false;
                //    General.msjAviso(sMensajeConSurtimiento);
                //}
            }


            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de movimiento inválido, verifique.");
                txtFolio.Focus();
            }

            if (sTipoES == "E")
            {
                if (bRegresa && cboLicitacion.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("Seleccionar Licitación. Favor de verificar.");
                    cboLicitacion.Focus();
                }

                if (bRegresa && cboSubFarmacias.SelectedIndex == 0)
                {
                    bRegresa = false;
                    General.msjUser("Seleccionar Fuente. Favor de verificar.");
                    cboSubFarmacias.Focus();
                }

                if (bRegresa && txtOrden.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("Capturar Orden. Favor de verificar.");
                    txtOrden.Focus();
                }

                if (bRegresa && txtFolioPre.Text.Trim() == "")
                {
                    bRegresa = false;
                    General.msjUser("Capturar Folio Presupuesto. Favor de verificar.");
                    txtFolioPre.Focus();
                }
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para el movimiento de inventario, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && bEstaCancelado)
            {
                bRegresa = false;
                General.msjUser("Ingreso de Insumo esta cancelado.\n No se puede guardar la información.");
            }

            if (bRegresa && !chkAplicarInv.Enabled)
            {
                bRegresa = false;
                General.msjUser("El movimiento de Inventario Inicial ya fue aplicado a la existencia,\n no es posible guardar la información.");
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
                bRegresa = validarCostosProductos();
            }

            if (bRegresa)
            {
                if (bGuardaMotivos)
                {
                    if (!motivodev.Marco)
                    {
                        bRegresa = false;
                        General.msjUser("No ha seleccionado motivos del Movimiento, verifique.");
                    }
                }
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un movimiento general de inventario, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("MOVIMIENTO_GENERAL_INVENTARIO", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("MOVIMIENTO_GENERAL_INVENTARIO", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        /// <summary>
        /// Revisar que todos los Productos tengan un Costo Inicial válido
        /// </summary>
        /// <returns></returns>
        private bool validarCostosProductos()
        {
            bool bRegresa = true;

            if (chkAplicarInv.Checked)
            {
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    if (myGrid.GetValueDou(i, (int)Cols.Costo) == 0)
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Se detectaron productos con Costo 0.\n\n" +
                    "No se posible aplicar el movmiento de inventario");
            }

            return bRegresa;
        }

        private bool validarDatosCancelacion()
        {
            bool bRegresa = true;

            if (bRegresa && (txtFolio.Text.Trim() == "*" || txtFolio.Text.Trim() == ""))
            {
                bRegresa = false;
                General.msjUser("Folio de Inventario inválido, verifique.");
            }

            if (bRegresa && bEstaCancelado)
            {
                bRegresa = false;
                General.msjUser("El folio de Inventario ya se encuentra cancelado,\n no es posible cancelarlo de nuevo");
            }

            if (bRegresa && General.msjCancelar("¿ Desea cancelar el folio de Inventario Inicial ?") == DialogResult.No)
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

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, el Movimiento no puede ser completado.");
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
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", cboTiposMovtoInventario.Data + txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                //////if (General.ImpresionViaWeb)
                //////{
                //////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                //////    DataSet datosC = DatosCliente.DatosCliente();

                //////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                //////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                //////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}


                if (bRegresa)
                {
                    LimpiarPantalla(false);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion de informacion

        #region Grabar informacion 
        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 

            bool bRegresa = false;
            string sSql = "";
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;


            if (bEntradaSalida)
            {
                sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' " +
                    "\n" +
                    "Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' \n",
                    sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);
            }
            else
            {
                sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' \n",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);
            }

            bRegresa = leer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioMovto);
            }

            return bRegresa;
        }

        private bool GrabarCancelacion()
        {
            bool bRegresa = false;
            ////string sSql = ""; 
            ////string sSqlAux = ""; 
            ////string sFolioMovtoAux = sFolioMovto; 

            ////sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
            ////    sEstado, sFarmacia, "*", "IC", "S", sFolioMovto,
            ////    DtGeneral.IdPersonal, "Cancelación de Movimiento de Inventario Inicial : " + sFolioMovto,
            ////    txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            ////if (!leer.Exec(sSql))
            ////{
            ////    bRegresa = false;
            ////}
            ////else
            ////{
            ////    leer.Leer();
            ////    sFolioMovto = leer.Campo("Folio");

            ////    sSqlAux = string.Format("Update MovtosInv_Enc Set Referencia = '{3}', Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}'  " +
            ////                    " Update MovtosInv_Det_CodigosEAN Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' " +
            ////                    " Update MovtosInv_Det_CodigosEAN_Lotes Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' ",
            ////                    sEstado, sFarmacia, sFolioMovtoAux, sFolioMovto);
            ////    if (!leer.Exec(sSqlAux))
            ////    {
            ////        bRegresa = false;
            ////    }
            ////    else
            ////    {
            ////        // Guardar el resto de la informacion 
            ////        bRegresa = GrabarDetalle();
            ////    }
            ////}

            return bRegresa;
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;
            string sSql = "";
            string sLicitacion = "", sFuente = "", sOrden = "", sFolioPre = "";

            if(sTipoES == "E")
            {
                sLicitacion = cboLicitacion.Data;
                sFuente = cboSubFarmacias.Data;
                sOrden = txtOrden.Text.Trim();
                sFolioPre = txtFolioPre.Text.Trim();
            }

            ////SKU.Reset(); 

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +  // " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}', \n" +
                " \t@IdLicitacion = '{14}', @Orden = '{15}', @FolioPresup = '{16}', @IdFuente = '{17}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text), 1, SKU.SKU,
                sLicitacion, sOrden, sFolioPre, sFuente);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                bRegresa = true;
                sFolioMovto = leer.Campo("Folio");
                // txtFolio.Text = sFolioMovto.Substring(2);
                //  bRegresa = GrabarDetalle(); 

                SKU.FolioMovimiento = leer.Campo("Folio");
                SKU.Foliador = leer.Campo("Foliador");
                SKU.SKU = leer.Campo("SKU");

                if (bGuardaMotivos)
                {
                    bRegresa = Grabar_Motivos_Movimiento();
                }

                if (bRegresa)
                {
                    bRegresa = GrabarDetalle();
                }
            }

            return bRegresa;
        }

        private bool Grabar_Motivos_Movimiento()
        {
            bool bRegresa = true;
            string sTipoMov = "", sMotivo = "";
            int iMarca = 0;

            leer2.DataSetClase = motivodev.Retorno;

            while (leer2.Leer())
            {
                iMarca = 0;

                iMarca = leer2.CampoInt("Marca");
                sTipoMov = leer2.Campo("IdMovto");
                sMotivo = leer2.Campo("IdMotivo");

                if (iMarca == 1)
                {

                    string sSql = string.Format(" Exec spp_Mtto_MovtosInv_Adt_Devoluciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @IdMotivo = '{5}' \n",
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

                if (sIdProducto != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' " +
                                         "Exec spp_Mtto_FarmaciaProductos_CodigoEAN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' ",
                                         sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
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
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";
            string sSKU_Local = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    sSKU_Local = bEntradaSalida ? SKU.SKU : L.SKU;

                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}',@SKU = '{9}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote,
                            General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, sSKU_Local);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                            "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', sSKU_Local);
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
                                if (!bRegresa)
                                {
                                    break;
                                }
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
            string sSKU_Local = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSKU_Local = bEntradaSalida ? SKU.SKU : L.SKU;

                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}',\n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, sSKU_Local);

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
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', sSKU_Local);

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
        #endregion Grabar informacion

        #region Eventos de Formulario 
        private void FrmInventarioInicial_Load(object sender, EventArgs e)
        {
            CargarMovimientosInv();
            CargarSubFarmacias();
            CargarLicitaciones();
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

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bTieneSurtimientosActivos = false;
            lblCancelado.Visible = false;
            // bExisteMovto = false;
            bEstaCancelado = false;
            bEntradaSalida = false;
            bCapturaDeLotes = false;
            bModificarCantidades = true;
            bPermitirSacarCaducados = false;
            bEsConsignacion = false;
            tipoDeInventario = TiposDeInventario.Todos;

            if (cboTiposMovtoInventario.SelectedIndex == 0)
            {
                General.msjUser("No ha seleccionado el Tipo de Movimiento de Inventario, verifique.");
                cboTiposMovtoInventario.Focus();
            }
            else
            {
                try
                {
                    sIdTipoMovtoInv = cboTiposMovtoInventario.Data;
                    sTipoES = ((DataRow)cboTiposMovtoInventario.ItemActual.Item)["Efecto_Movto"].ToString();
                    bPermitirSacarCaducados = Convert.ToBoolean(((DataRow)cboTiposMovtoInventario.ItemActual.Item)["PermiteCaducados"]);
                    bEsConsignacion = sIdTipoMovtoInv == DtGeneral.ClaveMovtoEntradaPorConsignacion;

                    if (bEsConsignacion)
                    {
                        tipoDeInventario = TiposDeInventario.Consignacion;
                    }
                }
                catch
                {
                    sTipoES = "";
                    sIdTipoMovtoInv = ""; // Asegurar que falle si no estan registrados los movimientos para la farmacia 
                }


                if (sTipoES == "E")
                {
                    bEntradaSalida = true;
                    bCapturaDeLotes = true;
                    bModificarCantidades = true;
                }

                //if (sTipoES == "S")
                //{
                //    if (DtGeneral.TieneSurtimientosActivos())
                //    {
                //        General.msjAviso(sMensajeConSurtimiento);
                //        bTieneSurtimientosActivos = true;
                //    }
                //}

                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    txtFolio.Enabled = false;
                    txtFolio.Text = "*";
                    cboTiposMovtoInventario.Enabled = false;

                    myGrid.Limpiar(true);
                    myGrid.BloqueaColumna(false, (int)Cols.Costo);
                    IniciarToolBar(true, false, false);

                    if (bGuardaMotivos)
                    {
                        btnMotivosDev.Enabled = true;
                    }
                }
                else
                {
                    leer.DataSetClase = query.FolioMovtoInventario(sEmpresa, sEstado, sFarmacia,
                        sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                    if (leer.Leer())
                    {
                        chkAplicarInv.Enabled = false;
                        bModificarCantidades = false;
                        cboTiposMovtoInventario.Enabled = false;
                        e.Cancel = !CargarDatosMovimiento();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private bool CargarDatosMovimiento()
        {
            bool bRegresa = true;
            // bExisteMovto = true;
            bMovtoAplicado = false;
            bEstaCancelado = false;

            IniciarToolBar(false, false, true);

            sFolioMovto = leer.Campo("Folio");
            txtFolio.Enabled = false;
            txtFolio.Text = Fg.Right(sFolioMovto, 8);

            chkAplicarInv.Checked = false;
            if (leer.Campo("MovtoAplicado").ToUpper() == "S")
            {
                chkAplicarInv.Checked = true;
                bMovtoAplicado = true;
            }
            chkAplicarInv.Enabled = !chkAplicarInv.Checked;

            cboLicitacion.Data = leer.Campo("IdLicitacion");
            cboSubFarmacias.Data = leer.Campo("IdFuente");
            txtOrden.Text = leer.Campo("Orden");
            txtFolioPre.Text = leer.Campo("FolioPresup");

            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
            txtIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
            txtTotal.Text = leer.CampoDouble("Total").ToString(sFormato);

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            CargarDetallesMovimiento();
            // No permitir la edición de la informacion 
            myGrid.BloqueaColumna(true, (int)Cols.Costo);
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            btnGuardar.Enabled = false; // chkAplicarInv.Enabled;


            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de inventario actualmente se encuentra cancelado.");
            }
            else if (bMovtoAplicado)
            {
                lblCancelado.Text = "APLICADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de Inventario ya fue aplicado a la existencia,\n no es posible hacer modificaciones.");
            }

            return bRegresa;
        }

        private void CargarDetallesMovimiento()
        {
            myGrid.Limpiar(true);
            leer.DataSetClase = query.FolioMovtoInventarioDetalle(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesMovimiento");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDetallesLotesMovimiento();
            }
            Totalizar();
        }

        private void CargarDetallesLotesMovimiento()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioMovtoInventarioDetalleLotes(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones_Movimiento(sEmpresa, sEstado, sFarmacia, sFolioMovto, "CargarDetallesLotesMovimiento");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        private void CargarMovimientosInv()
        {
            cboTiposMovtoInventario.Clear();
            cboTiposMovtoInventario.Add();
            //cboTiposMovtoInventario.Add(query.MovtosTiposInventario_Farmacias(sEstado, sFarmacia, 1, "CargarMovimientosInv"), true, "TipoMovto", "DescripcionMovimiento");
            cboTiposMovtoInventario.Add(query.MovtosTiposInventario_Farmacias(sEstado, sFarmacia, 1, "CargarMovimientosInv"), true, "TipoMovto", "DescMovimiento");
            cboTiposMovtoInventario.SelectedIndex = 0;
        }

        #region Manejo Grid 
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bMovtoAplicado)
            {
                if (!bEstaCancelado)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;

                            if (bEntradaSalida)
                                myGrid.BloqueaCelda(false, myGrid.Rows, (int)Cols.Costo);

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
                    if (chkAplicarInv.Enabled)
                    {
                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            leer.DataSetClase = ayuda.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, "grdProductos_KeyDown");
                            if (leer.Leer())
                            {
                                CargarDatosProducto();
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            removerLotes();
                        }

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}
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
                            leer.DataSetClase = query.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, sValor, "grdProductos_EditModeOff");
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
                                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
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
                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);
                    }
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

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tipoDeInventario, bEntradaSalida, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, bEntradaSalida, "CargarLotesCodigoEAN()");
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
            if (chkAplicarInv.Enabled)
            {
                if (!bEstaCancelado)
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
                    Lotes.EsEntrada = bEntradaSalida;
                    //Lotes.EsConsignacion = bEsConsignacion; FAV -- JALISCO ES CONSIGNA
                    Lotes.EsConsignacion = true;
                    if(sTipoES == "E")
                    {
                        Lotes.IdSubFarmacia = cboSubFarmacias.Data;
                    }

                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bCapturaDeLotes;  //  chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCantidades;

                    // Solo para Movientos Especiales 
                    Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

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

        #region Verificar_Guarda_Motivos_Devoluciones
        private void cboTiposMovtoInventario_SelectedIndexChanged(object sender, EventArgs e)
        {
            bGuardaMotivos = false;
            if (cboTiposMovtoInventario.SelectedIndex != 0)
            {
                sTipoES = ((DataRow)cboTiposMovtoInventario.ItemActual.Item)["Efecto_Movto"].ToString();
                MostrarNuevosDatos();
                if (Verificar_Motivos_Devolucion())
                {
                    motivodev.Movto_Inv = cboTiposMovtoInventario.Data;
                    motivodev.AsignarTipoMovto();
                }
            }
        }

        private bool Verificar_Motivos_Devolucion()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format(" Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = '{0}' ", cboTiposMovtoInventario.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Verificar_Motivos_Devolucion");
                General.msjError(" Ocurrio un error al verificar los motivos de devolución. ");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    bGuardaMotivos = true;
                }
            }

            return bRegresa;
        }
        #endregion Verificar_Guarda_Motivos_Devoluciones

        #region Boton_Motivos_Devoluciones
        private void btnMotivosDev_Click(object sender, EventArgs e)
        {
            motivodev.MotivosDevolucion();
        }
        #endregion Boton_Motivos_Devoluciones

        #region NuevosDatos
        private void CargarSubFarmacias()
        {
            leer = new clsLeer(ref cnn);
            string sSql = "";
            string sFiltroConsignacion = "";
            string sFiltroSubFarmacia = "";
            string sIdEstado = DtGeneral.EstadoConectado;
            string sIdFarmacia = DtGeneral.FarmaciaConectada;
            DataSet SubFarmacias = new DataSet();

            sFiltroConsignacion = " and EsConsignacion = 1 ";
            sFiltroConsignacion += " and EmulaVenta = 0 ";

            sSql = string.Format("Select IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, EsConsignacion, EmulaVenta " +
                    " From vw_Farmacias_SubFarmacias (NoLock) " +
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}' {2} {3} ",
                    sIdEstado, sIdFarmacia, sFiltroConsignacion, sFiltroSubFarmacia);


            if (leer.Exec(sSql))
            {
                SubFarmacias = leer.DataSetClase;
            }

            cboSubFarmacias.Clear();
            cboSubFarmacias.Add();
            cboSubFarmacias.Add(SubFarmacias, true, "IdSubFarmacia", "SubFarmacia");
        }

        private void CargarLicitaciones()
        {
            leer = new clsLeer(ref cnn);
            string sSql = "";
            string sIdEstado = DtGeneral.EstadoConectado;

            DataSet Licitaciones = new DataSet();

            sSql = string.Format("SELECT IdEstado, IdLicitacion, Licitacion " +
                    " FROM Ctrl_Licitaciones (NoLock) " +
                    " WHERE IdEstado = '{0}' AND Status = 'A' ", sIdEstado);


            if (leer.Exec(sSql))
            {
                Licitaciones = leer.DataSetClase;
            }

            cboLicitacion.Clear();
            cboLicitacion.Add();
            cboLicitacion.Add(Licitaciones, true, "IdLicitacion", "Licitacion");
        }

        private void MostrarNuevosDatos()
        {
            if(sTipoES == "S")
            {
                cboLicitacion.Enabled = false;
                cboSubFarmacias.Enabled = false;
                txtOrden.Enabled = false;
                txtFolioPre.Enabled = false;
            }
            else
            {
                cboLicitacion.Enabled = true;
                cboSubFarmacias.Enabled = true;
                txtOrden.Enabled = true;
                txtFolioPre.Enabled = true;
            }
        }
        #endregion NuevosDatos
    }
}
