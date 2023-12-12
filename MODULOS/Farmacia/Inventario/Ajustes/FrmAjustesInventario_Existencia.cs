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
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

namespace Farmacia.Inventario
{
    public partial class FrmAjustesInventario_Existencia : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            PrecioBase = 6, Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11, ExistenciaActual = 12
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerEliminar;
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

        string sPoliza = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdTipoMovtoInv = "EAI";
        // string sTipoES = "E";
        string sIdProGrid = "";
        bool bEntradaSalida = false;

        bool bCapturaDeLotes = false;
        bool bModificarCantidades = false;
        bool bPermitirSacarCaducados = false;
        bool bPolizaGuardada = false;
        DataTable dtEliminar = new DataTable("CodigosEAN");

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU;

        string sFolioMovto = "";
        string sSKU_Generado = "";

        #endregion variables

        public FrmAjustesInventario_Existencia()
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
            leerEliminar = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            CrearTablaEliminar();
        }
        private void FrmAjustesInventario_Existencia_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(false);
            Revisar_PermiteCaducados(); 
        }

        #region Buscar Folio
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            lblCancelado.Visible = false;
            // bExisteMovto = false;
            bEstaCancelado = false;
            bEntradaSalida = false;
            bCapturaDeLotes = false;
            bModificarCantidades = true;
            //// bPermitirSacarCaducados = false;

            if (txtFolio.Text.Trim() != "")
            {
                myGrid.BloqueaColumna(true, (int)Cols.Costo);
                leer.DataSetClase = query.AjusteInventario(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    if (leer.Campo("MovtoAplicado") == "S")
                    {
                        bPolizaGuardada = true;
                    }
                    else
                    {
                        bPolizaGuardada = false;
                    }
                    bModificarCantidades = false;
                    e.Cancel = !CargarDatosPoliza();
                }
                else
                {
                    e.Cancel = true; 
                } 
            }

        }

        private void Revisar_PermiteCaducados()
        {
            bPermitirSacarCaducados = false;

            leer.DataSetClase = query.MovtosTiposInventario(sIdTipoMovtoInv, "");
            if (leer.Leer())
            {
                bPermitirSacarCaducados = leer.CampoBool("PermiteCaducados"); 
            }

        }

        private bool CargarDatosPoliza()
        {
            bool bRegresa = true;
            // bExisteMovto = true;
            bMovtoAplicado = false;
            bEstaCancelado = false;

            IniciarToolBar(true, true, false, true);

            sPoliza = leer.Campo("Poliza");
            sSKU_Generado = leer.Campo("SKU");

            txtFolio.Enabled = false;
            txtFolio.Text = sPoliza;

            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
            txtIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
            txtTotal.Text = leer.CampoDouble("Total").ToString(sFormato);

            if (leer.Campo("MovtoAplicado") == "S")
            {
                bMovtoAplicado = true;
                IniciarToolBar(true, false, false, true);
            }

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            //Se cargan los detalles.
            CargarDetalles();

            // No permitir la edición de la informacion 
            //myGrid.BloqueaColumna(true, (int)Cols.Costo);
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);

            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("La Póliza se encuentra cancelada.");
            }
            else if (bMovtoAplicado)
            {
                lblCancelado.Text = "APLICADA";
                lblCancelado.Visible = true;
                General.msjUser("Esta Póliza ya fue aplicada a la existencia,\n no es posible hacer modificaciones.");
            }

            return bRegresa;
        }

        private void CargarDetalles()
        {
            myGrid.Limpiar(true);
            leer.DataSetClase = query.AjusteInventario_Detalle(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesMovimiento");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarLotes();
            }
            Totalizar();
        }

        private void CargarLotes()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.AjusteInventario_Lotes(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(leer.DataSetClase);

            if (!bMovtoAplicado)
            {
                bCapturaDeLotes = true;
                bModificarCantidades = true;
            }

            if (GnFarmacia.ManejaUbicaciones)
            {
                CargarLotes_Ubicaciones();
            }
            //if (bMovtoAplicado)
            //{
            //    leer.DataSetClase = query.AjusteInventario_Lotes(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesLotesMovimiento");
            //    Lotes.AddLotes(leer.DataSetClase);
            //}
            //else
            //{
            //    bCapturaDeLotes = true;
            //    bModificarCantidades = true;
            //    ObtenerLotesPoliza();
            //}
        }

        private void CargarLotes_Ubicaciones()
        {
            leer.DataSetClase = query.AjusteInventario_Lotes_Ubicaciones(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarLotes_Ubicaciones");

            if (query.Ejecuto)
            {
                leer.Leer();
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }           
        }

        #endregion Buscar Folio

        #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (bPolizaGuardada)
            {
                LimpiarPantalla(false);
            }
            else
            {
                LimpiarPantalla(true);
            }
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
                    //General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    // Apagar la barra completa 
                    IniciarToolBar(false,false,false,false); 

                    cnn.IniciarTransaccion();

                    if (EliminarProductos())
                    {
                        if (GrabarEncabezado())
                        {
                            bExito = true;
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        //txtFolio.Text = sPoliza;
                        IniciarToolBar(true,false, false, true); 
                        General.msjUser("Información de la Póliza " + txtFolio.Text.Trim() + " se ha guardado satisfactoriamente.");
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información de la Póliza.");
                        IniciarToolBar(true, bBtnGuardar, bBtnCancelar, bBtnImprimir); 
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
                        txtFolio.Text = sPoliza.Substring(2);  
                        General.msjUser("Información de la Póliza " + sPoliza + " ha sido cancelada exitosamente ");
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar la información de la Póliza.");
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

        #region Grabar informacion 
        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sPolizaAplicada = "S";
            string sSql = "";

            ////string sSql = string.Format("Exec spp_Mtto_AjustesInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
            sSql = string.Format("Exec spp_Mtto_AjustesInv_Enc " + //'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}', " + 
                " @SubTotal = '{6}', @Iva = '{7}', @Total = '{8}', @PolizaAplicada = '{9}', @iOpcion = '{10}', @SKU = '{11}' ", 
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text),
                sPolizaAplicada, 1, sSKU_Generado);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sPoliza = leer.Campo("Folio");
                sSKU_Generado = leer.Campo("SKU");

                bRegresa = GrabarDetalle();
            }
            
            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = false;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "", sStatus = "A";  // , sTipoMovto = "", sTipoES = ""
            int iExistenciaFisica = 0, iExistenciaSistema = 0;
            double nCosto = 0, nPrecioBase = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iExistenciaFisica = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nPrecioBase = myGrid.GetValueDou(i, (int)Cols.PrecioBase);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);
                iExistenciaSistema = myGrid.GetValueInt(i, (int)Cols.ExistenciaActual);

                if (sIdProducto != "")
                {

                    // Se registra el producto en las tablas de existencia en caso de ser nuevo
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
                        //sSql = string.Format("Exec spp_Mtto_AjustesInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', " +
                        //    "'{10}', '{11}', '{12}' ",

                        sSql = string.Format("Exec spp_Mtto_AjustesInv_Det \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', \n" +
                            "\t@ExistenciaFisica = '{8}', @Costo = '{9}', @Importe = '{10}', @ExistenciaSistema = '{11}', @Status = '{12}', @PrecioBase = '{13}' \n", 
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), sIdProducto, sCodigoEAN, iUnidadDeSalida, nTasaIva, iExistenciaFisica,
                            nCosto, nImporte, iExistenciaSistema, sStatus, nPrecioBase);
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
                                bRegresa = false;
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
            string sSql = "", sStatus = "A", sReferencia = "";
            string sSKU_Proceso = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN, false);

            foreach (clsLotes L in ListaLotes)
            {
                sSKU_Proceso = L.SKU; //!= "" ? L.SKU : sSKU_Generado;
                //if (L.Cantidad > 0)
                {
                    // Se registra el producto en las tablas de existencia en caso de ser nuevo
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}', @SKU = '{9}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, sSKU_Generado);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }

                //else 
                //{
                sSql = string.Format("Exec spp_Mtto_AjustesInv_Det_Lotes \n" + ////" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ",
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @Poliza = '{4}', \n" +
                    "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @ExistenciaFisica = '{8}', @Costo = '{9}', @Importe = '{10}', \n" +
                    "\t@ExistenciaSistema = '{11}', @Referencia = '{12}', @Status = '{13}', @SKU = '{14}' \n", 
                    sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, txtFolio.Text.Trim(), IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, (L.Cantidad * Costo),
                    L.Existencia, sReferencia, sStatus, sSKU_Proceso);

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
                            bRegresa = GrabarDetalleLotes_Ubicaciones(L, sReferencia, sStatus, Costo);
                            if(!bRegresa)
                            {
                                break;
                            }
                        }
                    }
                //}
            //}
            }

            return bRegresa;
        }

        private bool GrabarDetalleLotes_Ubicaciones(clsLotes Lote, string sReferencia, string sStatus, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";
            string sSKU_Proceso = "";

            //clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
            clsLotesUbicacionesItem[] Ubicaciones = Lotes.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia, false);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                sSKU_Proceso = L.SKU;// != "" ? L.SKU : sSKU_Generado;
                if (L.Cantidad > 0)
                {
                    //// Se registra el producto en las tablas de existencia en caso de ser nuevo
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}', \n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, sSKU_Generado);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
                    //else
                    //{
                    sSql = string.Format("Exec spp_Mtto_AjustesInv_Det_Lotes_Ubicaciones \n" +
                            " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @Poliza = '{4}', \n" +
                            " @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @IdPasillo = '{8}', @IdEstante = '{9}', @IdEntrepaño = '{10}', \n" +
                            " @ExistenciaFisica = '{11}', @Costo = '{12}', @Importe = '{13}', @ExistenciaSistema = '{14}', @Referencia = '{15}', @Status = '{16}', @SKU = '{17}' \n", 
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, txtFolio.Text.Trim(), Lote.Codigo, Lote.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, Costo, (L.Cantidad * Costo), L.ExistenciaActual, sReferencia, sStatus, sSKU_Proceso);

                        bRegresa = leer.Exec(sSql);
                        if (!bRegresa)
                        {
                            bRegresa = false;
                            break;
                        }
                    //}
                //}
            }

            return bRegresa;
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 
            string sSql = "";
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;


            if (bEntradaSalida)
            {
                sSql = string.Format(" Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' " +
                     "\n" +
                     " Exec spp_INV_ActualizarCostoPromedio '{0}', '{1}', '{2}', '{3}' ",
                     DtGeneral.EmpresaConectada, sEstado, sFarmacia, sPoliza, (int)Inv, (int)Costo);
            }
            else
            {
                sSql = string.Format(" Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, sPoliza, (int)Inv, (int)Costo);
            }

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }

        private bool EliminarProductos()
        {
            bool bRegresa = true;

            leerEliminar.DataTableClase = dtEliminar;

            while (leerEliminar.Leer())
            {
                string sIdProducto = leerEliminar.Campo("IdProducto");
                string sCodigoEAN = leerEliminar.Campo("CodigoEAN");

                string sSql = string.Format("Exec spp_Mtto_AjustesInv_EliminarProductos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                    sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdProducto, sCodigoEAN);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
            }
            
            return bRegresa;
        }
        private bool GrabarCancelacion()
        {
            bool bRegresa = true;
            //string sSql = ""; 
            //string sSqlAux = ""; 
            //string sPolizaAux = sPoliza; 

            //sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
            //    sEstado, sFarmacia, "*", "IC", "S", sPoliza,
            //    DtGeneral.IdPersonal, "Cancelación de Movimiento de Inventario Inicial : " + sPoliza,
            //    txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            //if (!leer.Exec(sSql))
            //{
            //    bRegresa = false;
            //}
            //else
            //{
            //    leer.Leer();
            //    sPoliza = leer.Campo("Folio");

            //    sSqlAux = string.Format("Update MovtosInv_Enc Set Referencia = '{3}', Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}'  " +
            //                    " Update MovtosInv_Det_CodigosEAN Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' " +
            //                    " Update MovtosInv_Det_CodigosEAN_Lotes Set Status = 'C', Actualizado = 0 Where IdEstado = '{0}' and IdFarmacia = '{1}' and FolioMovtoInv = '{2}' ",
            //                    sEstado, sFarmacia, sPolizaAux, sPoliza);
            //    if (!leer.Exec(sSqlAux))
            //    {
            //        bRegresa = false;
            //    }
            //    else
            //    {
            //        // Guardar el resto de la informacion 
            //        bRegresa = GrabarDetalle();
            //    }
            //}

            return bRegresa;
        }

        #endregion Grabar informacion

        #region Eventos de Formulario 

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
        
        #region Manejo Grid
        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bPolizaGuardada)
            {
                if (!bEstaCancelado)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;

                            //if (bEntradaSalida)
                            //    myGrid.BloqueaCelda(false, myGrid.Rows, (int)Cols.Costo);
                            
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
                 
                    if (e.KeyCode == Keys.F1)
                    {
                        //sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN); 
                        //leer.DataSetClase = ayuda.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, "grdProductos_KeyDown");
                        //if(leer.Leer())
                        //{
                        //    CargarDatosProducto();
                        //}
                    }

                    //if(e.KeyCode == Keys.Delete)
                    //{
                    //    removerLotes();
                    //}

                    if (e.KeyCode == Keys.F8)
                    {
                        FrmModificarCostosAjustes f = new FrmModificarCostosAjustes();
                        f.CostoBase = myGrid.GetValueDec(myGrid.ActiveRow, (int)Cols.PrecioBase);
                        f.CostoAnterior = myGrid.GetValueDec(myGrid.ActiveRow, (int)Cols.Costo);
                        f.ShowDialog();

                        if (f.bAplicarCambio)
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Costo, f.CostoNuevo);
                        }

                        if (f.bBaseDiferente)
                        {
                            myGrid.ColorCelda(myGrid.ActiveRow, (int)Cols.Costo, Color.Yellow);
                        }
                        else
                        {
                            myGrid.ColorCelda(myGrid.ActiveRow, (int)Cols.Costo, Color.White);
                        }


                    }

                    //else
                    //{
                    //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                    //}
                    
                    break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            //ColActiva = (Cols)myGrid.ActiveCol;
            //switch (ColActiva)
            //{
            //    case Cols.CodEAN:
            //        string sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            //        if (sValor != "")
            //        {
            //            if (EAN.EsValido(sValor))
            //            {
            //                leer.DataSetClase = query.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, sValor, "grdProductos_EditModeOff");
            //                if(leer.Leer())
            //                {
            //                    CargarDatosProducto();
            //                }
            //                else
            //                {
            //                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
            //                    myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
            //                }
            //            }
            //            else
            //            {
            //                //General.msjError(sMsjEanInvalido);
            //                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            //                myGrid.ActiveCelda(myGrid.ActiveRow, (int)Cols.CodEAN);
            //                SendKeys.Send("");
            //            }
            //        }
            //        else
            //        {
            //            myGrid.LimpiarRenglon(myGrid.ActiveRow);
            //        }
            //        break;
            //}
            //Totalizar();
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

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if(GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
                    if(query.Ejecuto)
                    {
                        leer.Leer();
                        Lotes.AddLotesUbicaciones(leer.DataSetClase);
                    }
                }

                mostrarOcultarLotes();
            }
        }

        private void ObtenerLotesPoliza()
        {
            string sCodigo = "", sCodEAN = "";

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigo = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodEAN = myGrid.GetValue(i, (int)Cols.CodEAN);

                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, true, "ObtenerLotesPoliza()");
                if (query.Ejecuto)
                {
                    // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    leer.Leer();
                    Lotes.AddLotes(leer.DataSetClase);
                }
            }
        }

        private void removerLotes()
        {
            if (!bPolizaGuardada)
            {
                if (!bEstaCancelado)
                {
                    try
                    {
                        int iRow = myGrid.ActiveRow;

                        AgregarProductoEliminar(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                        Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                        myGrid.DeleteRow(iRow);
                        Totalizar();
                    }
                    catch { }

                    if(myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(false);
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
                    
                    ////// Jesus Diaz ==> 2K101219-1523 
                    //// Siempre enviar TRUE para activar la captura de Cantidades en caso de existir Fisico y no en Sistema
                    Lotes.EsEntrada = true; // bEntradaSalida;

                    // Deshabilitar validacion de Producto bloqueado por Inventario 
                    Lotes.EsInventarioActivo = true; 

                    
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bCapturaDeLotes;  //  chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCantidades;  

                    // Solo para Movientos Especiales 
                    Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados; 
                    
                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    // Si el Ajuste de inventario no ha sido aplicado habilitar-validar el cambio de caducidades 
                    Lotes.ModificaCaducidades = !bMovtoAplicado; 

                    Lotes.Show();

                    if (!bMovtoAplicado)
                    {
                        myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                        myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                        myGrid.SetActiveCell(iRow, (int)Cols.Costo);
                    }

                    Totalizar();
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes 

        #region Funciones 
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
                dtEliminar.Clear();
                bPolizaGuardada = false;
                bEntradaSalida = false;
                bCapturaDeLotes = false;
                bModificarCantidades = false;

                Revisar_PermiteCaducados();


                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;
                SKU = new clsSKU();
                sSKU_Generado = "";


                // Activar barra de menu 
                IniciarToolBar(true, false, false, false);
                // myGrid.BloqueaColumna(false, (int)Cols.Costo);

                // bExisteMovto = false;
                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGrid.Limpiar(true);
                myGrid.BloqueaColumna(false, (int)Cols.CodEAN);

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

        private void CrearTablaEliminar()
        {
            //Se agregan las columnas a la tabla CodigosEAN
            dtEliminar.Columns.Add("IdProducto", Type.GetType("System.String"));
            dtEliminar.Columns.Add("CodigoEAN", Type.GetType("System.String"));
        } 

        private void AgregarProductoEliminar(string IdProducto, string CodigoEAN)
        {
            object[] Codigos = { IdProducto, CodigoEAN };
            dtEliminar.Rows.Add(Codigos);
        }
        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }
        #endregion Funciones 

        #region Validacion de datos
        private bool validarDatos()
        {
            bool bRegresa = true;

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

            //if (bRegresa && !chkAplicarInv.Enabled)
            //{
            //    bRegresa = false;
            //    General.msjUser("El movimiento de Inventario Inicial ya fue aplicado a la existencia,\n no es posible guardar la información.");
            //}

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
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
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValue(i, (int)Cols.CodEAN) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }

                    //if (Lotes.CantidadTotal == 0)
                    //{
                    //    bRegresa = false;
                    //}
                    //else
                    //{
                    //    for (int i = 1; i <= myGrid.Rows; i++)
                    //    {
                    //        if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                    //        {
                    //            bRegresa = false;
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }

            if (!bRegresa)
            {
                //General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");
                General.msjUser("Debe capturar al menos un producto para el Ajuste de Inventario, verifique.");
            }

            return bRegresa;
        }
        #endregion Validacion de datos

        #region Impresion de informacion
        private void ImprimirInventario()
        {
            // bool bRegresa = false;
            //if (validarImpresion())
            //{
            //    DatosCliente.Funcion = "ImprimirInventario()";
            //    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            //    byte[] btReporte = null;

            //    myRpt.RutaReporte = GnFarmacia.RutaReportes;
            //    myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

            //    myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            //    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            //    myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            //    myRpt.Add("Folio", txtFolio.Text);

            //    //DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //    //DataSet datosC = DatosCliente.DatosCliente();

            //    //btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            //    //if (myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true))
            //    //{
            //    //    LimpiarPantalla(false);
            //    //}
            //    //else
            //    //{
            //    //    General.msjError("Ocurrió un error al cargar el reporte.");
            //    //} 
            //    if (General.ImpresionViaWeb)
            //    {
            //        DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //        DataSet datosC = DatosCliente.DatosCliente();

            //        btReporte = conexionWeb.Reporte(InfoWeb, datosC);
            //        bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            //    }
            //    else
            //    {
            //        myRpt.CargarReporte(true);
            //        bRegresa = !myRpt.ErrorAlGenerar;
            //    }


            //    if (bRegresa)
            //    {
            //        LimpiarPantalla(false);
            //    }
            //    else
            //    {
            //        General.msjError("Ocurrió un error al cargar el reporte.");
            //    }
            //}

            LimpiarPantalla(false);
        }
        #endregion Impresion de informacion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
