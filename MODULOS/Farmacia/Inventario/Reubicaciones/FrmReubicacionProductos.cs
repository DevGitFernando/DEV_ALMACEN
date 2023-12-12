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
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Farmacia.Inventario
{
    public partial class FrmReubicacionProductos : FrmBaseExt 
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            Entrada = 11, Salida = 12
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
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
        // bool bContinua = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdTipoMovtoInv = "SPR";
        string sTipoES = "S";
        string sIdProGrid = "";
        string sFolioEntrada = "", sFolioSalida = "";
        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        DataSet dtsLotes_Destinos;
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; 

        public bool ReubicacionGenerada = false;
        private bool bEsReubicacionPosicionCompleta = false; 
        
        string sPasilloOrigen = ""; 
        string sEstanteOrigen = "";
        string sEntrepañoOrigen = "";

        string sPasilloDestino = "";
        string sEstanteDestino = "";
        string sEntrepañoDestino = "";


        //bool bTieneSurtimientosActivos = false;

        //string sMensajeConSurtimiento = "Se encontrarón folios de surtimiento pendientes de generar transferencia ó venta.\n\n" +
        //"No es posible generar la Reubicación, verifique el status de los folios de surtimiento.";

        #endregion variables

        public FrmReubicacionProductos()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myGrid = new clsGrid( ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BackColorColsBlk = Color.White;      
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            // Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name); 
            dtsLotes_Destinos = new DataSet();
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        #region Botones 
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla, se perdera lo que este capturado. ?") == DialogResult.No)
                    bExito = false;
            }

            if (bExito)
            {
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Todos);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;

                SKU = new clsSKU(); 

                // Acitvar barra de menu 
                IniciarToolBar(false, false, false);
                myGrid.Limpiar(false); 
                myGrid.BloqueaColumna(false, (int)Cols.Costo);

                // bExisteMovto = false;
                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGrid.Limpiar(false);
                Fg.IniciaControles();
                Fg.IniciaControles(this, false, FrameValorInventario);
                lblCancelado.Visible = false;
                dtpFechaRegistro.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                txtFolio.Focus();
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = false; // Cancelar;
            btnImprimir.Enabled = Imprimir;

            if (bEsReubicacionPosicionCompleta)
            {
                btnNuevo.Enabled = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(!bMovtoAplicado); 
            //if (bMovtoAplicado)
            //{
            //    LimpiarPantalla(false);
            //}
            //else
            //{
            //    LimpiarPantalla(true);
            //}
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

                    //if (GrabarEncabezado()) 
                    //{
                    //    bExito = AfectarExistencia(true, true);
                    //}

                    if (GuardarSalidas())
                    {
                        if (GuardarEntradas())
                        {
                            bExito = ActualizarReferencias();
                        }
                    }

                    if (bExito)
                    {
                        if (DtGeneral.ConfirmacionConHuellas)
                        {
                            bExito = GuardarCtrl_Reubicacion();
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        //txtFolio.Text = sFolioMovto.Substring(2);
                        txtFolio.Text = sFolioSalida.Substring(3);
                        IniciarToolBar(false, false, true);
                        
                        General.msjUser("Información guardada satisfactoriamente con los Folios: " + sFolioSalida + ", " + sFolioEntrada);

                        Imprimir(true);
                        ReubicacionGenerada = true;
                        ////ImprimirInventario(false, sFolioSalida, false, "Salida por reubicación");
                        ////ImprimirInventario(false, sFolioEntrada, true, "Entrada por reubicación");
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = sFolioSalida != "" ? sFolioSalida.Substring(3) : "*";
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click"); 

                        General.msjError("Ocurrió un error al grabar la información de la reubicación.");

                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }

                    cnn.Cerrar();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                    LimpiarPantalla(false); 
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Reubicación inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false); 
        }
        #endregion Botones

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;

            //if (DtGeneral.TieneSurtimientosActivos())
            //{
            //    bRegresa = false;
            //    General.msjAviso(sMensajeConSurtimiento);
            //}

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de movimiento inválido, verifique.");
                txtFolio.Focus();
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
                General.msjUser("El movimiento de Inventario Inicial se encuentra cancelado,\n no es posible guardar la información.");
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

            if (bRegresa && txtIdPersonalCedis.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Montacarguista, verifique.");
                txtIdPersonalCedis.Focus();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar una reubicación, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("REUBICACION", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("REUBICACION", sMsjNoEncontrado);
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

            ////for (int i = 1; i <= myGrid.Rows; i++)
            ////{
            ////    if (myGrid.GetValueDou(i, (int)Cols.Costo) == 0)
            ////    {
            ////        bRegresa = false;
            ////        break; 
            ////    }
            ////} 
            

            ////if (!bRegresa)
            ////{
            ////    General.msjUser("Alguno de los Productos registrados tienen Costo 0.\n\n" + 
            ////        "'NO ES POSIBLE APLICAR EL INVENTARIO'");  
            ////}

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
            {
                General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");
            }

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
            bool bRevUbicaciones = true; 

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

                    ////////// Revisión de Prueba. Jesus Diaz 2K120223.1543 
                    ////if (DtGeneral.EsAlmacen)
                    ////{
                    ////    int iCantidadEntrada = 0;
                    ////    int iCantidadSalida = 0;
                    ////    bRevUbicaciones = true; 

                    ////    clsLotes_ReubicacionesItem[] UbicacionesEntrada = Lotes.Reubicaciones(sIdProducto, sCodigoEAN, L.ClaveLote); 
                    ////    clsLotes_ReubicacionesItem[] UbicacionesSalida = Lotes.Reubicaciones_Salidas(sIdProducto, sCodigoEAN, L.ClaveLote);

                    ////    foreach (clsLotes_ReubicacionesItem Lr in UbicacionesEntrada)
                    ////    {
                    ////        iCantidadEntrada += Lr.Cantidad;
                    ////    }

                    ////    foreach (clsLotes_ReubicacionesItem Lr in UbicacionesSalida)
                    ////    {
                    ////        iCantidadSalida += Lr.Cantidad;
                    ////    }

                    ////    if (iCantidadEntrada != iCantidadSalida) 
                    ////    {
                    ////        bRevUbicaciones = false; 
                    ////    }
                    ////}
                }

                if ((iCantidad != iCantidadLotes) || !bRevUbicaciones )
                {
                    bRegresa = false;
                    object[] obj = { sIdProducto, sCodigoEAN, sDescripcion, iCantidad, iCantidadLotes };
                    dtsProductosDiferencias.Tables[0].Rows.Add(obj);
                }
            }

            // Se encontraron diferencias 
            if (!bRegresa)
            {
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Reubicación no puede ser completada.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        #endregion Validacion de datos

        #region Impresion de informacion 
        private void Imprimir( bool ConfirmarImpresion )
        {
            ImprimirInventario(ConfirmarImpresion, sFolioSalida, false, "Salida por reubicación");
            ImprimirInventario(ConfirmarImpresion, sFolioEntrada, true, "Entrada por reubicación");
        }

        private void ImprimirInventario(bool ConfirmarImpresion, string sFolio, bool bLimpiar, string Titulo )
        {
            bool bRegresa = false;
            if(validarImpresion(ConfirmarImpresion))
            {
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.TituloReporte = Titulo; 
                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", sFolio);

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


                if(bRegresa)
                {
                    if(bLimpiar)
                    {
                        LimpiarPantalla(false);
                    }
                }
                else
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Ocurrió un error al cargar el reporte.");
                    }
                }
            }
        }
        #endregion Impresion de informacion

        #region Grabar informacion 
        #region Marcado Especial de Informacion 
        private bool Eliminar_Detalles_NoRequeridos()
        {
            bool bRegresa = true; 
            string sSql = string.Format(" Delete From MovtosInv_Det_CodigosEAN_Lotes " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Delete From MovtosInv_Det_CodigosEAN " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Delete From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' and Status = 'C'  ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool CancelarDetallesNoRequeridos()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Update MovtosInv_Det_CodigosEAN Set Status = 'C' " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes Set Status = 'C' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Set Status = 'C' " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto); 

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }

        private bool MarcarEnvioDeInformacion(StatusDeRegistro Actualizado)
        {
            bool bRegresa = true;
            string sSql = string.Format(" Update MovtosInv_Enc Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            sSql += string.Format(" Update MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Set Actualizado = {4} " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioMovtoInv = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, (int)Actualizado);

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }

        #endregion Marcado Especial de Informacion

        private bool GuardarCtrl_Reubicacion()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_Ctrl_Reubicaciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio_Inv = '{3}', \n" +
                "\t@FolioMovto_Referencia = '{4}', @IdPersonal = '{5}', @IdPersonal_Asignado = '{6}', @iOpcion = '{7}' \n",
             sEmpresa, sEstado, sFarmacia, sFolioSalida, sFolioEntrada, DtGeneral.IdPersonal, txtIdPersonalCedis.Text.Trim(),  1);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool GuardarSalidas()
        {
            bool bRegresa = false;

            if (GrabarEncabezado(1))
            {
                bRegresa = AfectarExistencia(true, false);
            }

            return bRegresa;
        }

        private bool GuardarEntradas()
        {
            bool bRegresa = false;

            if (GrabarEncabezado(2))
            {
                bRegresa = AfectarExistencia(true, false);
            }

            return bRegresa;
        }

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

            sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' " +
                "\n" +
                "Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            bRegresa = leer.Exec(sSql);


            return bRegresa;
        }

        private bool GrabarCancelacion()
        {
            bool bRegresa = true; 
            return bRegresa;
        }

        private bool GrabarEncabezado(int iTipoMovto)
        {
            bool bRegresa = false;
            string sSql = "";
            string sFolioReferencia = "";


            if (iTipoMovto == 1)
            {
                sIdTipoMovtoInv = "SPR";
                sTipoES = "S";
            }
            else
            {
                sIdTipoMovtoInv = "EPR";
                sTipoES = "E";
                sFolioReferencia = sFolioSalida;
            }

            SKU.Reset(); 
            SKU.TipoDeMovimiento = sIdTipoMovtoInv;

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" +   
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, sFolioReferencia, 
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
                SKU.FolioMovimiento = leer.Campo("Folio");
                SKU.Foliador = leer.Campo("Foliador");


                if (sTipoES == "S")
                {
                    sFolioSalida = sFolioMovto;
                }
                else
                {
                    sFolioEntrada = sFolioMovto;
                }

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

                if (sIdProducto != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' \n" +
                        "Exec spp_Mtto_FarmaciaProductos_CodigoEAN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' \n",
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

            foreach(clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}', @SKU = '{9}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote,
                            General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, L.SKU);
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
                                if (sTipoES == "S")
                                {
                                    bRegresa = GrabarDetalleLotesUbicaciones_Salidas(L);
                                }
                                else
                                {
                                    bRegresa = GrabarDetalleLotesUbicaciones_Entradas(L);
                                }

                                if(!bRegresa)
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

        private bool GrabarDetalleLotesUbicaciones_Salidas(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes_ReubicacionesItem[] Ubicaciones = Lote.Reubicaciones_Salidas(Lote.SKU, Lote.IdSubFarmacia, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote);

            foreach (clsLotes_ReubicacionesItem L in Ubicaciones)
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

        private bool GrabarDetalleLotesUbicaciones_Entradas(clsLotes Lote)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes_ReubicacionesItem[] Ubicaciones = Lote.Reubicaciones(Lote.SKU, Lote.IdSubFarmacia, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote);

            foreach (clsLotes_ReubicacionesItem L in Ubicaciones)
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

        private bool ActualizarReferencias()
        {
            bool bRegresa = false;

            string sSql = string.Format("Update MovtosInv_Enc Set Referencia = '{4}' " + 
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioSalida, sFolioEntrada);

            sSql += string.Format("Update MovtosInv_Enc Set Referencia = '{4}' " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioMovtoInv = '{3}' \n\n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioEntrada, sFolioSalida);


            bRegresa = leer.Exec(sSql);
            if (bRegresa)
            {
                bRegresa = validar_ReubicacionConfirmada(); 
                if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                {
                    ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioEntrada);
                    bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioEntrada);
                }

                if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                {
                    ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioSalida);
                    bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioSalida);
                }
            }

            ////if (!leer.Exec(sSql))
            ////{
            ////    bRegresa = false;
            ////}
            ////else
            ////{
            ////    leer.Leer();
            ////    sFolioMovto = leer.Campo("Folio");
            ////}

            return bRegresa;
        }
        private bool validar_ReubicacionConfirmada()
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INV_Reubicacion__Validar \n" + 
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Reubicacion_Salida = '{3}', @Reubicacion_Entrada = '{4}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioSalida, sFolioEntrada);

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }
        #endregion Grabar informacion

        #region Eventos de Formulario 
        private void FrmReubicacionProductos_Load(object sender, EventArgs e)
        {
            if (!bEsReubicacionPosicionCompleta)
            {
                LimpiarPantalla(false);
            }
            else 
            {
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Todos);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;

                btnNuevo.Enabled = false;
                IniciarToolBar(true, false, false); 
                CargarDatosPosicionCompleta(); 
            }

            tmValidar.Enabled = true;
            tmValidar.Start();
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
            lblCancelado.Visible = false;
            // bExisteMovto = false;
            bEstaCancelado = false;
            myGrid.Limpiar(false); 

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
                myGrid.Limpiar(true); 
            }
            else
            {
                myGrid.Limpiar(true); 
                leer.DataSetClase = query.FolioMovtoInventario(sEmpresa, sEstado, sFarmacia, 
                    sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    e.Cancel = !CargarDatosMovimiento();
                }
                else
                {
                    e.Cancel = true;
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
            sFolioSalida = leer.Campo("Folio");
            sFolioEntrada = leer.Campo("Referencia");
            txtFolio.Enabled = false;
            txtFolio.Text = Fg.Right(sFolioMovto, 8);



            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
            txtIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
            txtTotal.Text = leer.CampoDouble("Total").ToString(sFormato);

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            if(leer.Campo("MovtoAplicado").ToUpper() == "S")
            {
                bMovtoAplicado = true;
                CargarMontacarguista();
            }

            CargarDetallesMovimiento();
            
            
            // No permitir la edición de la informacion 
            myGrid.BloqueaColumna(false, (int)Cols.Costo);
            myGrid.BloqueaColumna(false, (int)Cols.CodEAN);


            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de inventario inicial actualmente se encuentra cancelado.");
            }
            else if( bMovtoAplicado )
            {
                lblCancelado.Text = "APLICADO";
                lblCancelado.Visible = true;
                //General.msjUser("El movimiento de Inventario Inicial ya fue aplicado a la existencia,\n no es posible hacer modificaciones.");
            }

            return bRegresa;
        }

        private void CargarMontacarguista()
        {
            string sSql = string.Format("Select * From vw_Ctrl_Reubicaciones C Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' And C.Folio_Inv = '{3}'",
                                sEmpresa, sEstado, sFarmacia, sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8));
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMontacarguista()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    txtIdPersonalCedis.Text = leer.Campo("IdPersonal_Asignado");
                    lblPersonalCedis.Text = leer.Campo("PersonalAsignado");
                }
            }
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
                    if (btnGuardar.Enabled)
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

                        if (!bEsReubicacionPosicionCompleta)
                        {
                            if (e.KeyCode == Keys.Delete)
                            {
                                removerLotes();
                            }
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
            //bool bTieneSurtimientosActivos = false;
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            //if (DtGeneral.TieneSurtimientosActivos())
            //{
            //    bTieneSurtimientosActivos = true;
            //}

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, false, "CargarLotesCodigoEAN()");
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
            if (btnGuardar.Enabled)
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
                    {
                        myGrid.Limpiar(true);
                    }
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
                    Lotes.EsEntrada = true;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    Lotes.PermitirLotesNuevosConsignacion = false;
                    Lotes.EsConsignacion = false; 

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = btnGuardar.Enabled;
                    Lotes.ModificarCantidades = btnGuardar.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    //Indicar que es una reubicacion
                    Lotes.EsReubicacion = true;

                    if (bEsReubicacionPosicionCompleta)
                    {
                        Lotes.ModificarCantidades = false; 
                    } 

                    // Invocar Lotes 
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

        private void MarcarProductosEntradaSalida()
        {
            //string sIdProducto = "", sCodigoEAN = "";

            //for (int i = 1; i <= myGrid.Rows; i++)
            //{
            //    sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
            //    sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);

            //    clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
            //    clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote);

            //    foreach (clsLotesUbicacionesItem L in Ubicaciones)
            //    {
            //        if (L.Cantidad > 0)
            //        {

            //        }
            //    }
            //}

        }
        #endregion Manejo de lotes 

        #region Reubicacion Masiva
        #region Funciones y Procedimientos Publicos
        public void MostrarReubicacion_Posicion(string PasilloOrigen, string EstanteOrigen, string EntrepañoOrigen,
            string PasilloDestino, string EstantePasilloDestino, string EntrepañoPasilloDestino)
        {
            if (DtGeneral.EsAlmacen)
            {
                SKU = new clsSKU();
                bEsReubicacionPosicionCompleta = true;

                sPasilloOrigen = PasilloOrigen;
                sEstanteOrigen = EstanteOrigen;
                sEntrepañoOrigen = EntrepañoOrigen;

                sPasilloDestino = PasilloDestino;
                sEstanteDestino = EstantePasilloDestino;
                sEntrepañoDestino = EntrepañoPasilloDestino; 

                this.ShowDialog();
            }
        } 
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private void CargarDatosPosicionCompleta() 
        {
            string sSql = string.Format(
                "Select \n" +
                "\tV.CodigoEAN, V.IdProducto, V.Descripcion, V.TasaIva, sum(U.Existencia - (U.ExistenciaEnTransito + U.ExistenciaSurtidos)) as Existencia, \n" +
                "\tdbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', v.IdProducto) as CostoPromedio \n" +
                "From vw_ProductosEstadoFarmacia v (NoLock) \n" +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) \n" +
                "On ( V.IdEstado = U.IdEstado and V.IdProducto = U.IdProducto and V.CodigoEAN = U.CodigoEAN ) \n" +
                "Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and U.IdFarmacia = '{2}' \n" +
                "\tand U.IdPasillo = '{3}' and U.IdEstante = '{4}' and U.IdEntrepaño = '{5}' \n" +
                "Group by V.CodigoEAN, V.IdProducto, V.Descripcion, V.TasaIva \n" +
                "Having sum(U.Existencia - U.ExistenciaEnTransito) > 0 \n", 
                sEmpresa, sEstado, sFarmacia, sPasilloOrigen, sEstanteOrigen, sEntrepañoOrigen ) ;


            txtFolio.Text = "*";
            txtFolio.Enabled = false; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosPosicionCompleta");
                General.msjError("Ocurrió un error al cargar la lista de productos de la Ubicación solicitada."); 
            }
            else
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDatosPosicionCompletaLotes(); 
            }
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            Totalizar(); 
        }

        private void CargarDatosPosicionCompletaLotes()
        {
            string sSql = ""; 
            
            sSql  = string.Format("" +
                   "Select \n" +
                   "\tF.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, L.SKU, L.CodigoEAN, L.ClaveLote, \n" +
                   "\tdatediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                   "\tL.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, \n" +
                   "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\tcast(U.Existencia - (U.ExistenciaEnTransito + U.ExistenciaSurtidos) as Int) as Existencia, \n" +
                   "\tcast(U.Existencia - (U.ExistenciaEnTransito + U.ExistenciaSurtidos) as Int) as Cantidad \n" +
                   "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                   "Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) \n" +
                   "\tOn ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.IdSubFarmacia = U.IdSubFarmacia " +
                   "\t\tand L.SKU = U.SKU and L.ClaveLote = U.ClaveLote and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN ) \n" +
                   "Inner Join CatFarmacias_SubFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and U.IdFarmacia = '{2}' " +
                   "and U.IdPasillo = '{3}' and U.IdEstante = '{4}' and U.IdEntrepaño = '{5}' and U.Existencia > 0 \n" +
                   "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.SKU \n",
                   sEmpresa, sEstado, sFarmacia, sPasilloOrigen, sEstanteOrigen, sEntrepañoOrigen);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosPosicionCompletaLotes");
                General.msjError("Ocurrió un error al cargar la lista de productos de la Ubicación solicitada.");
            }
            else
            {
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                CargarDatosPosicionCompletaLotes_Ubicaciones_Origen();
                CargarDatosPosicionCompletaLotes_Ubicaciones_Destino(); 
            }

        }

        private void CargarDatosPosicionCompletaLotes_Ubicaciones_Origen()
        {
            string sSql = "";

            sSql = string.Format("" +
                   "Select \n"+
                   "\tF.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\tcast(L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos) as Int) as Existencia, \n" +
                   "\tcast(L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos) as Int) as Cantidad \n" +
                   "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                   "\tOn ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' " +
                   "and L.IdPasillo = '{3}' and L.IdEstante = '{4}' and L.IdEntrepaño = '{5}' and L.Existencia > 0 \n" +
                   "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                   sEmpresa, sEstado, sFarmacia, sPasilloOrigen, sEstanteOrigen, sEntrepañoOrigen); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatosPosicionCompletaLotes");
                General.msjError("Ocurrió un error al cargar la lista de productos de la Ubicación solicitada.");
            }
            else
            {
                leer.Leer(); 
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            } 
        }

        private void CargarDatosPosicionCompletaLotes_Ubicaciones_Destino()
        {
            string sSql = "";

            // "    {3} as IdPasillo, {4} as IdEstante, {5} as IdEntrepano, " +  
            // "    {6} as IdPasilloActual, {7} as IdEstanteActual, {8} as IdEntrepanoActual " +

            // "    {6} as IdPasillo, {7} as IdEstante, {8} as IdEntrepano, " +  
            // "    {3} as IdPasilloActual, {4} as IdEstanteActual, {5} as IdEntrepanoActual " +


            sSql = string.Format("" +
                   "Select \n" +
                   "\tF.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                   "\t{6} as IdPasillo, {7} as IdEstante, {8} as IdEntrepano, \n" +
                   "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\tcast(L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos) as Int) as Existencia, \n" +
                   "\tcast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) as ExistenciaDisponible, \n" +
                   "\tcast(L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos) as Int) as Cantidad, \n" +
                   "\t{3} as IdPasilloActual, {4} as IdEstanteActual, {5} as IdEntrepanoActual \n" +
                   "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                   "\tOn ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' " +
                   "and L.IdPasillo = '{3}' and L.IdEstante = '{4}' and L.IdEntrepaño = '{5}' and L.Existencia > 0 \n" +
                   "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n", 
                   sEmpresa, sEstado, sFarmacia, sPasilloOrigen, sEstanteOrigen, sEntrepañoOrigen, 
                   sPasilloDestino, sEstanteDestino, sEntrepañoDestino); 

            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "CargarDatosPosicionCompletaLotes");
                General.msjError("Ocurrió un error al cargar la lista de productos de la Ubicación solicitada.");
            }
            else
            {
                leer.Leer();
                Lotes.DataSetLotes_Destinos = leer.DataSetClase; 
                // Lotes.AddLotes(leer.DataSetClase);
            }

        } 
        #endregion Funciones y Procedimientos Privados 

        private void txtIdPersonalCendis_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonalCedis.Text.Trim() != "")
            {
                leer.DataSetClase = query.PersonalCEDIS(sEmpresa, sEstado, sFarmacia, txtIdPersonalCedis.Text, Puestos_CEDIS.Montacarguista, "txtIdPersonalCendis_Validating");
                if (leer.Leer())
                {
                    txtIdPersonalCedis.Text = leer.Campo("IdPersonal");
                    lblPersonalCedis.Text = leer.Campo("Personal");
                }
                else
                {
                    txtIdPersonalCedis.Text = "";
                    lblPersonalCedis.Text = "";
                }
            }
        }
        #endregion Reubicacion Masiva

        private void txtIdPersonalCendis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.PersonalCEDIS(sEmpresa, sEstado, sFarmacia, Puestos_CEDIS.Montacarguista, "txtId_KeyDown");
                if (leer.Leer())
                {
                    txtIdPersonalCedis.Text = leer.Campo("IdPersonal");
                    lblPersonalCedis.Text = leer.Campo("Personal");
                }
                else
                {
                    txtIdPersonalCedis.Text = "";
                    lblPersonalCedis.Text = "";
                }
            }
        }

        private void tmValidar_Tick(object sender, EventArgs e)
        {
            tmValidar.Enabled = false;
            string sSql = string.Format("Select  DATEDIFF(hh, Min(FechaRegistro), GETDATE()) As Horas From vw_Ctrl_Reubicaciones Where Confirmada = 0");

            sSql = string.Format(
                "Select  IsNull(DATEDIFF(hh, Min(FechaRegistro), GETDATE()), 0) As Horas \n" +
                "From Ctrl_Reubicaciones C (noLock) \n" +
                "Where (Case When C.Status = 'T' Then 1 Else 0 End) = 0 \n");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMontacarguista()");
                General.msjError("Ocurrió un error al obtener la información.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoInt("Horas") > 24)
                    {
                        General.msjAviso("Al menos una Reubicación tiene más de 24 horas sin confirmar. Por tanto esta pantalla de cerrara");
                        this.Close();
                    }
                    else
                    {
                        tmValidar.Enabled = true;
                        tmValidar.Interval = 20000;
                        tmValidar.Start();
                    }
                }
            }
        }

        private void txtIdPersonalCedis_TextChanged(object sender, EventArgs e)
        {
            lblPersonalCedis.Text = "";
        }

        private void FrmReubicacionProductos_Shown(object sender, EventArgs e)
        {

        }
    }
}
