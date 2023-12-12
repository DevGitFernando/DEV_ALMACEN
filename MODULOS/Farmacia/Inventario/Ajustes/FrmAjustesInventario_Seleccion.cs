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
    public partial class FrmAjustesInventario_Seleccion : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10
        }

        #region Variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerProductos;
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
        // string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sIdTipoMovtoInv = "EAI";
        // string sTipoES = "E";
        string sIdProGrid = "";
        bool bEntradaSalida = false;

        bool bCapturaDeLotes = false;
        bool bModificarCantidades = false;
        bool bPermitirSacarCaducados = false;
        bool bPolizaGuardada = false;
        bool bOpcionExterna = false;

        bool bPermitirAjustesInventario_Con_ExistenciaEnTransito = GnFarmacia.PermitirAjustesInventario_Con_ExistenciaEnTransito; 

        //clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU;

        string sFolioMovto = "";
        string sSKU_Generado = "";

        FrmAjustesDeInventario_SeleccionClave listaClaves = new FrmAjustesDeInventario_SeleccionClave(); 
        DataSet dtsListaClaves;
        string sListaDeClaves = ""; 
        #endregion Variables

        public FrmAjustesInventario_Seleccion()
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
            leerProductos = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
        }
        private void FrmAjustesInventario_Seleccion_Load(object sender, EventArgs e)
        {            
            LimpiarPantalla(false);

            tmPantalla.Enabled = true;
            tmPantalla.Start();
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
            bPermitirSacarCaducados = false;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";

                myGrid.BloqueaColumna(false, (int)Cols.Costo);
                IniciarToolBar(true, false, false, true);
            }
            else
            {
                leer.DataSetClase = query.AjusteInventario(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    bPolizaGuardada = true;
                    bModificarCantidades = false;
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

            IniciarToolBar(false, false, true, false);

            sPoliza = leer.Campo("Poliza");
            sSKU_Generado = leer.Campo("SKU");


            txtFolio.Enabled = false;
            txtFolio.Text = sPoliza;

            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            CargarDetalles();
            // No permitir la edición de la informacion 
            myGrid.BloqueaColumna(true, (int)Cols.Costo);
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            btnGuardar.Enabled = false; // chkAplicarInv.Enabled;


            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("La Póliza se encuentra cancelada.");
            }
            else if (bMovtoAplicado)
            {
                lblCancelado.Text = "APLICADO";
                lblCancelado.Visible = true;
                General.msjUser("El movimiento de Inventario ya fue aplicado a la existencia,\n no es posible hacer modificaciones.");
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
                //CargarLotes();
            }
        }

        //private void CargarLotes()
        //{
        //    leer.DataSetClase = clsLotes.PreparaDtsLotes();
        //    leer.DataSetClase = query.AjusteInventario_Lotes(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesLotesMovimiento");
        //    Lotes.AddLotes(leer.DataSetClase);
        //}

        #endregion Buscar Folio

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

                listaClaves = new FrmAjustesDeInventario_SeleccionClave();
                dtsListaClaves = listaClaves.ClavesSSA;
                sListaDeClaves = listaClaves.ListaDeClavesSSA; 

                bPolizaGuardada = false;
                bEntradaSalida = false;
                bCapturaDeLotes = false;
                bModificarCantidades = false; 
                bPermitirSacarCaducados = false;

                if (!bOpcionExterna)
                {
                    // Inicializar el manejo de Lotes 
                    //Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                    //Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;

                    SKU = new clsSKU();
                    sSKU_Generado = "";
                }

                // Activar barra de menu 
                if (!bOpcionExterna)
                {
                    btnNuevo.Enabled = true;
                    IniciarToolBar(false, false, false, false);
                }
                else
                {
                    IniciarToolBar(true, false, false, false);
                    btnNuevo.Enabled = false;
                }

                // bExisteMovto = false;
                bEstaCancelado = false;
                bMovtoAplicado = false;

                if (!bOpcionExterna)
                {
                    myGrid.Limpiar(true);
                    myGrid.BloqueaColumna(false, (int)Cols.CodEAN);
                    Fg.IniciaControles();
                }
                
                lblCancelado.Visible = false; 
                dtpFechaRegistro.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                if (!bOpcionExterna)
                {
                    txtFolio.Focus();
                }
                else
                {
                    txtObservaciones.Focus();
                }
            }
        }

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
            bool bBtnSeleccionarClaves = btnSeleccionarClave.Enabled; 


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

                    bExito = GrabarEncabezado();

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        txtFolio.Text = sPoliza; 
                        IniciarToolBar(false, false, true, false); 
                        General.msjUser("Información guardada satisfactoriamente con la Póliza " + sPoliza);
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        txtFolio.Text = "*";
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información de la Póliza.");
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnSeleccionarClaves); 
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
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else 
                {
                    cnn.IniciarTransaccion();

                    if (GrabarCancelacion())
                    {
                        if (bMovtoAplicado)
                        {
                            bExito = AfectarExistencia(true, false);
                        }
                        else
                        {
                            bExito = true;
                        }
                    }

                    if (bExito)
                    {
                        cnn.CompletarTransaccion(); 
                        txtFolio.Text = sPoliza.Substring(2);  
                        General.msjUser("Información de la Poliza " + sPoliza + " ha sido cancelada exitosamente ");
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar la información de la Poliza.");
                    }

                    cnn.Cerrar();
                }
            }
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Folio de Ajuste inválido, verifique.");
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario();
        }

        private void btnSeleccionarClave_Click(object sender, EventArgs e)
        {
            //////FrmAjustesDeInventario_SeleccionClave f = new FrmAjustesDeInventario_SeleccionClave();
            //////f.ShowDialog();

            //////if (f.ClaveCapturada)
            //////{
            //////    CargarProductosClaveSSA(f.ClaveSSA); 
            //////}

            listaClaves = new FrmAjustesDeInventario_SeleccionClave();
            listaClaves.ClavesSSA = dtsListaClaves;
            listaClaves.ShowDialog(); 

            dtsListaClaves = listaClaves.ClavesSSA;
            sListaDeClaves = listaClaves.ListaDeClavesSSA;
            CargarProductosClaveSSA(sListaDeClaves); 
        }
        #endregion Botones

        #region Carga masiva de productos 
        private void CargarProductosClaveSSA(string ListadoDeClavesSSA)
        {
            int iRenglon = 1;
            int iExistenciaEnTransito_Acumulado = 0;
            int iExistenciaEnTransito = 0;
            clsLeer leerClaves = new clsLeer(ref cnn);

            string sSql = string.Format("Select F.CodigoEAN, cast(F.ExistenciaEnTransito as int) as ExistenciaEnTransito " +
                "From FarmaciaProductos_CodigoEAN F (NoLock) " + 
                "Inner Join vw_Productos_CodigoEAN P (NoLock) " + 
                "   On ( F.IdProducto = P.IdProducto and F.CodigoEAN = P.CodigoEAN and P.ClaveSSA in ( {0} ) ) " + 
                "Where F.IdEmpresa = '{1}' and F.IdEstado = '{2}' and F.IdFarmacia = '{3}' " ,
                ListadoDeClavesSSA, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            myGrid.Limpiar(false);
            if (!leerClaves.Exec(sSql))
            {
                Error.GrabarError(leerClaves, "CargarProductosClaveSSA");
                General.msjError("Ocurrió un error al cargar los productos relacionados con la Clave SSA."); 
            }
            else
            {
                while (leerClaves.Leer())
                {
                    iExistenciaEnTransito = leerClaves.CampoInt("ExistenciaEnTransito");
                    iExistenciaEnTransito_Acumulado += iExistenciaEnTransito;

                    if (iExistenciaEnTransito == 0)
                    {
                        leer.DataSetClase = query.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, leerClaves.Campo("CodigoEAN"), "grdProductos_EditModeOff");
                        if (leer.Leer())
                        {
                            myGrid.AddRow();
                            myGrid.SetValue(iRenglon, (int)Cols.CodEAN, leerClaves.Campo("CodigoEAN"));
                            CargarDatosDelProducto(iRenglon);
                            iRenglon++; 
                        }
                    }
                }
            }

            if (iExistenciaEnTransito_Acumulado > 0)
            {
                General.msjAviso("Se encontrarón algunos productos en tránsito, se quitarán de la lista de selección."); 
            }
        }
        #endregion Carga masiva de productos

        #region Grabar informacion
        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            int iSubTotal = 0, iIva = 0, iTotal = 0;
            string sPolizaAplicada = "N";
            string sSql = ""; 

            sSql = string.Format("Exec spp_Mtto_AjustesInv_Enc \n" + //'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}', \n" +
                "\t@SubTotal = '{6}', @Iva = '{7}', @Total = '{8}', @PolizaAplicada = '{9}', @iOpcion = '{10}', @SKU = '{11}' \n", 
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                iSubTotal, iIva, iTotal, sPolizaAplicada, 1, sSKU_Generado);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            } 
            else
            {
                leer.Leer();
                sPoliza = leer.Campo("Poliza");

                ////sFolioMovto = leer.Campo("Folio");
                sSKU_Generado = leer.Campo("SKU");

                bRegresa = GrabarDetalle();


                if (bRegresa)
                {
                    bRegresa = Grabar_DetalleLotes_Ubicaciones();
                }
            }
            
            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = false;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "", sStatus = "A";   // sTipoMovto = "", sTipoES = "", 
            int iExistenciaSistema = 0, iExistenciaFisica = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iExistenciaFisica = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "")
                {
                    bRegresa = true;

                    sSql = string.Format("Exec spp_Mtto_AjustesInv_Det \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', \n" +
                        "\t@ExistenciaFisica = '{8}', @Costo = '{9}', @Importe = '{10}', @ExistenciaSistema = '{11}', @Status = '{12}', @PrecioBase = '{9}' \n", 
                        sEmpresa, sEstado, sFarmacia, sPoliza, sIdProducto, sCodigoEAN, iUnidadDeSalida, nTasaIva, iExistenciaFisica, 
                        nCosto, nImporte, iExistenciaSistema, sStatus);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    //else
                    //{
                    //    bRegresa = GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto);
                    //    if (!bRegresa)
                    //    {
                    //        bRegresa = false;
                    //        break;
                    //    }
                    //}
                }
            }

            return bRegresa;
        }

        private bool Grabar_DetalleLotes_Ubicaciones()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_Mtto_AjustesInv_GenerarLotesUbicaciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, sPoliza);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;

            }


            return bRegresa;
        }

        //private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        //{
        //    bool bRegresa = false;
        //    string sSql = "", sStatus = "A", sReferencia = ""; //  sTipoMovto = "", sTipoES = "", sReferencia = "";
        //    int iExistenciaFisica = 0, iExistenciaSistema = 0, iImporte = 0;
        //    string sSKU_Proceso = "";

        //    //clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN, false);

        //    foreach (clsLotes L in ListaLotes)
        //    {
        //        if (L.ClaveLote != "")
        //        {
        //            sSKU_Proceso = L.SKU != "" ? L.SKU : sSKU_Generado;

        //            sSql = string.Format("Exec spp_Mtto_AjustesInv_Det_Lotes \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ",
        //                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @Poliza = '{4}', \n" +
        //                "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @ExistenciaFisica = '{8}', @Costo = '{9}', @Importe = '{10}', \n" +
        //                "\t@ExistenciaSistema = '{11}', @Referencia = '{12}', @Status = '{13}', @SKU = '{14}' \n", 
        //                sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sPoliza, IdProducto, CodigoEAN, L.ClaveLote, iExistenciaFisica, Costo, iImporte,
        //                iExistenciaSistema, sReferencia, sStatus, sSKU_Proceso);

        //            if (!leer.Exec(sSql))
        //            {
        //                bRegresa = false;
        //                break;
        //            }
        //            else
        //            {
        //                bRegresa = true;
        //                if (GnFarmacia.ManejaUbicaciones)
        //                {
        //                    bRegresa = GrabarDetalleLotes_Ubicaciones(L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, Costo);
        //                    if (!bRegresa)
        //                    {
        //                        bRegresa = false;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return bRegresa;
        //}

        //private bool GrabarDetalleLotes_Ubicaciones(string IdSubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote, double Costo)
        //{
        //    bool bRegresa = false;
        //    string sSql = "", sStatus = "A", sReferencia = "";
        //    int iExistenciaFisica = 0, iExistenciaSistema = 0, iImporte = 0;
        //    string sSKU_Proceso = "";
        //    //clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
        //    clsLotesUbicacionesItem[] Ubicaciones = Lotes.Ubicaciones(IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia, false);

        //    //foreach (clsLotes L in ListaLotes)
        //    foreach (clsLotesUbicacionesItem L in Ubicaciones)
        //    {
        //        sSKU_Proceso = L.SKU != "" ? L.SKU : sSKU_Generado;

        //        if ((L.IdSubFarmacia == IdSubFarmacia) && (L.ClaveLote != ""))
        //        {
        //            sSql = string.Format("Exec spp_Mtto_AjustesInv_Det_Lotes_Ubicaciones \n" +
        //                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @Poliza = '{4}', \n" +
        //                "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @IdPasillo = '{8}', @IdEstante = '{9}', @IdEntrepaño = '{10}', \n" +
        //                "\t@ExistenciaFisica = '{11}', @Costo = '{12}', @Importe = '{13}', @ExistenciaSistema = '{14}', @Referencia = '{15}', @Status = '{16}', @SKU = '{17}' \n", 
        //                sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sPoliza, IdProducto, CodigoEAN, L.ClaveLote, 
        //                L.Pasillo, L.Estante, L.Entrepano, iExistenciaFisica, Costo, iImporte,
        //                iExistenciaSistema, sReferencia, sStatus, sSKU_Proceso);

        //            bRegresa = leer.Exec(sSql);
        //            if (!bRegresa)
        //            {
        //                bRegresa = false;
        //                break;
        //            }
        //        }
        //    }

        //    return bRegresa;
        //}

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
                sSql = string.Format(" Exec spp_INV_AplicarDesaplicarExistencia \n" + 
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}'  " +
                     "\n" +
                     " Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' ",
                     DtGeneral.EmpresaConectada, sEstado, sFarmacia, sPoliza, (int)Inv, (int)Costo);
            }
            else
            {
                sSql = string.Format(" Exec spp_INV_AplicarDesaplicarExistencia \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}'  ",
                    DtGeneral.EmpresaConectada, sEstado, sFarmacia, sPoliza, (int)Inv, (int)Costo);
            }

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }

        private bool GrabarCancelacion()
        {
            bool bRegresa = true;

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
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                //case Keys.F7:
                //    mostrarOcultarLotes();
                //    break;

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
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "")
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
                 
                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN); 
                        leer.DataSetClase = ayuda.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, "grdProductos_KeyDown");
                        if (leer.Leer())
                        {
                            CargarDatosProducto(leer.Campo("CodigoEAN"));
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

                                    CargarDatosProducto(sValor);
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
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
        }

        private bool CargarDatosProducto()
        {
            string sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            bool bRegresa = CargarDatosProducto(sCodigoEAN); 
            return bRegresa; 
        }

        private bool CargarDatosProducto(string CodigoEAN)
        {
            bool bRegresa = false;
            clsLeer leerDatos = new clsLeer(ref cnn); 

            string sSql = string.Format("Select F.CodigoEAN, cast(F.ExistenciaEnTransito as int) as Existencia " +
                "From FarmaciaProductos_CodigoEAN F (NoLock) " +
                "Where F.CodigoEAN = '{0}' and F.IdEmpresa = '{1}' and F.IdEstado = '{2}' and F.IdFarmacia = '{3}' and 1 = 0 ",
                CodigoEAN, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leerDatos.Exec(sSql))
            {
                Error.GrabarError(leerDatos, "CargarDatosProducto"); 
            }
            else
            {
                bRegresa = true; 
                if (leerDatos.Leer())
                {
                    if (leerDatos.CampoInt("Existencia") > 0)
                    {
                        bRegresa = false; 
                    }
                }
            }


            if (!bRegresa)
            {
                General.msjAviso("El producto seleccionado tiene Transferencias en Tránsito, no es válido para generar ajustes de inventario, verifique.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
            }
            else 
            {
                bRegresa = CargarDatosDelProducto(myGrid.ActiveRow);  
            }

            return bRegresa; 
        }

        private bool CargarDatosDelProducto(int Renglon)
        {
            bool bRegresa = true;
            //int iRow = myGrid.ActiveRow;
            int iRow = Renglon;
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
                    //CargarLotesCodigoEAN(iRow);
                }
                else
                {
                    General.msjUser("El producto ya fue capturado en otro renglón, verifique.");
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
        #endregion Manejo Grid 

        #region Manejo de lotes 
        //private void CargarLotesCodigoEAN()
        //{
        //    CargarLotesCodigoEAN(myGrid.ActiveRow); 
        //}
        //private void CargarLotesCodigoEAN(int Renglon)
        //{
        //    //int iRow = myGrid.ActiveRow;
        //    int iRow = Renglon; 
        //    string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
        //    string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

        //    leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, false, "CargarLotesCodigoEAN()");
        //    if (query.Ejecuto)
        //    {
        //        // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
        //        leer.Leer();
        //        Lotes.AddLotes(leer.DataSetClase);                

        //        if (GnFarmacia.ManejaUbicaciones)
        //        {
        //            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
        //            if (query.Ejecuto)
        //            {
        //                leer.Leer();
        //                Lotes.AddLotesUbicaciones(leer.DataSetClase);
        //            }
        //        }

        //        //mostrarOcultarLotes();
        //    }
        //}

        private void removerLotes()
        {
            if (!bPolizaGuardada)
            {
                if (!bEstaCancelado)
                {
                    try
                    {
                        int iRow = myGrid.ActiveRow;
                        //Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
                        myGrid.DeleteRow(iRow);
                    }
                    catch { }

                    if (myGrid.Rows == 0)
                    {
                        myGrid.Limpiar(true);
                    }
                }
            }
        }

        //private void mostrarOcultarLotes()
        //{
        //    // Asegurar que el Grid tenga el Foco.
        //    if (this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper())
        //    {
        //        int iRow = myGrid.ActiveRow;

        //        if (myGrid.GetValue(iRow, (int)Cols.Codigo) != "")
        //        {
        //            Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
        //            Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
        //            Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
        //            Lotes.EsEntrada = bEntradaSalida;
        //            Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

        //            // Si el movimiento ya fue aplicado no es posible agregar lotes 
        //            Lotes.CapturarLotes = bCapturaDeLotes;  //  chkAplicarInv.Enabled;
        //            Lotes.ModificarCantidades = bModificarCantidades;  

        //            // Solo para Movientos Especiales 
        //            Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados; 

        //            //Configurar Encabezados 
        //            Lotes.Encabezados = EncabezadosManejoLotes.Default;

        //            // Mostrar la Pantalla de Lotes 
        //            Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
        //            Lotes.Show();

        //            myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
        //            myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);                    
        //            myGrid.SetActiveCell(iRow, (int)Cols.Costo);
        //        }
        //        else
        //        {
        //            myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
        //        }
        //    }
        //}
        #endregion Manejo de lotes 

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
                General.msjUser("El movimiento de Inventario inicial se encuentra Cancelado,\n no es posible guardar la información.");
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

            if (bRegresa)
            {
                bRegresa = BuscarProductosEnAjuste();
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
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para el inventario, verifique.");
            }

            return bRegresa;
        }

        private DataSet DatosProductos()
        {
            DataSet dts = new DataSet("Dts_Productos");
            DataTable dt = new DataTable("Productos");

            dt.Columns.Add("Poliza", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("CodigoEAN", typeof(string));
            dt.Columns.Add("Descripcion", typeof(string)); 

            dts.Tables.Add(dt); 
            return dts.Copy(); 
        }

        private bool BuscarProductosEnAjuste()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "", sDescripcion = "", sPoliza = "";
            DataSet dtsProductos = DatosProductos(); 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sDescripcion = myGrid.GetValue(i, (int)Cols.Descripcion);

                if (sIdProducto != "")
                {
                    sSql = string.Format("Select Poliza From vw_AjustesInv_Det(NoLock) " + 
                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " + 
                        " And IdProducto = '{3}' And CodigoEAN = '{4}' And MovtoAplicado = 'N' ",
                        sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN );

                    if (!leerProductos.Exec(sSql))
                    {
                        bRegresa = false;
                        Error.GrabarError(leerProductos, "BuscarProductosEnAjuste");
                        General.msjError("Ocurrió un error al buscar los productos en Ajuste.");                        
                        break;
                    }
                    else
                    {
                        if (leerProductos.Leer())
                        {
                            //Si lee, significa que un producto ya esta en proceso de ajuste.
                            bRegresa = false;
                            sPoliza = leerProductos.Campo("Poliza");
                            string sMensaje = string.Format("El IdProducto '{0}' con CodigoEAN '{1}' ya se encuentra capturado en la Póliza {2}. \n Descripción: {3}", 
                                sIdProducto, sCodigoEAN, sPoliza, sDescripcion);

                            // General.msjAviso(sMensaje);
                            myGrid.SetActiveCell(i, (int)Cols.CodEAN);

                            object[] obj = { sPoliza, sIdProducto, sCodigoEAN, sDescripcion };
                            dtsProductos.Tables[0].Rows.Add(obj); 

                            // break;
                        }
                    }
                }
            }

            if (!bRegresa) 
            {
                clsLeer leerP = new clsLeer();
                leerP.DataSetClase = dtsProductos;

                if (leerP.Leer())
                {
                    General.msjUser("Se encontraron algunos productos que ya fueron capturados en otras pólizas."); 
                    FrmAjustesDeInventario_ProductosEnInventario f = new FrmAjustesDeInventario_ProductosEnInventario(dtsProductos);
                    f.ShowDialog();
                }
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

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Ajustes_Inv_Seleccion.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                if (!bRegresa)
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Ocurrió un error al cargar el reporte.");
                    }
                }
                else
                {
                    LimpiarPantalla(false);
                }
            }
            
        }
        #endregion Impresion de informacion

        #region Funciones 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool SeleccionarClaves)
        {
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnSeleccionarClave.Enabled = SeleccionarClaves; 
        }
        public void MostrarDetalle(DataSet dtsCodigos, DataSet dtsLotes, DataSet dtsUbicaciones )
        {
            bOpcionExterna = true;
            txtFolio.Text = "*";
            txtFolio_Validating(null,null);

            myGrid.LlenarGrid(dtsCodigos);

            //Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            //Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;
            //Lotes.AddLotes(dtsLotes);

            ////// Agregar las ubicaciones para los Almacenes 
            //Lotes.AddLotesUbicaciones(dtsUbicaciones); 

            this.ShowDialog();
        }
        #endregion Funciones  

        #region Validar_Transferencias_En_Transito
        ////////private bool ValidaTransferenciasTransito() 
        ////////{
        ////////    bool bRegresa = true;
        ////////    string sSql = "";

        ////////    sSql = string.Format(
        ////////        " Select * From TransferenciasEnc (Nolock) " + 
        ////////        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and TipoTransferencia = 'TS' " +
        ////////        " and TransferenciaAplicada = 0 and Status = 'A' ", 
        ////////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

        ////////    if (!leer.Exec(sSql))
        ////////    {
        ////////        bRegresa = false;
        ////////        Error.GrabarError(leer, "ValidaTransferenciasTransito()");
        ////////        General.msjError("Ocurrió un error al validar transferencias en tránsito.");
        ////////    }
        ////////    else
        ////////    {
        ////////        if (leer.Leer())
        ////////        {
        ////////            if (bPermitirAjustesInventario_Con_ExistenciaEnTransito)
        ////////            {
        ////////                General.msjAviso("Se encontraron transferencias en tránsito, no es posible ajustar los productos en tránsito.");
        ////////            }
        ////////            else 
        ////////            {
        ////////                bRegresa = false;
        ////////                General.msjAviso("No es posible realizar ajustes de inventario, se encontraron transferencias en tránsito.");
        ////////            }
        ////////        }                
        ////////    }

        ////////    return bRegresa;
        ////////}
        #endregion Validar_Transferencias_En_Transito

        #region Timer_Pantalla
        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            //string sMensajeConSurtimiento = "Se encontrarón folios de surtimientos activos.\n\n" +
            //     "No es posible generar un inventario, verifique el status de los folios de surtimiento.";

            //tmPantalla.Enabled = false;
            //if (!DtGeneral.ValidaTransferenciasTransito())
            //{
            //    this.Close();
            //}

            //if (DtGeneral.TieneSurtimientosActivos())
            //{
            //    General.msjAviso(sMensajeConSurtimiento);
            //    this.Close();
            //}
        }
        #endregion Timer_Pantalla

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
