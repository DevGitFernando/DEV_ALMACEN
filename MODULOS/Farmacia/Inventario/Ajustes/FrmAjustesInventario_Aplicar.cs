using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel;

namespace Farmacia.Inventario
{
    public partial class FrmAjustesInventario_Aplicar : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10, ExistenciaActual = 11
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

        string sPoliza = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdProGrid = "";
        string sMensaje = "";
        bool bEntradaSalida = false;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        bool bCapturaDeLotes = false;
        bool bModificarCantidades = false;
        bool bPermitirSacarCaducados = false;

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);

        string sFolioVentaEntrada = "";
        string sFolioVentaSalida = "";
        string sFolioConsignacionEntrada = "";
        string sFolioConsignacionSalida = "";

        clsExportarExcelPlantilla xpExcel;

        #endregion variables

        public FrmAjustesInventario_Aplicar()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myGrid = new clsGrid( ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.SoloLectura);
            myGrid.BackColorColsBlk = Color.White;      
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }
        private void FrmAjustesInventario_Aplicar_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(false);
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

            if (txtFolio.Text.Trim() != "")
            {
                myGrid.BloqueaColumna(false, (int)Cols.Costo);
                leer.DataSetClase = query.AjusteInventario(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (leer.Leer())
                {
                    bModificarCantidades = false;
                    e.Cancel = !CargarDatosPoliza();
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private bool CargarDatosPoliza()
        {
            bool bRegresa = true;
            string sExistenciaAplicada = "";
            
            bMovtoAplicado = false;
            bEstaCancelado = false;

            IniciarToolBar(true, true, false, false, false);

            sPoliza = leer.Campo("Poliza");
            txtFolio.Enabled = false;
            txtFolio.Text = sPoliza;

            dtpFechaRegistro.Value = leer.CampoFecha("FechaReg");
            txtObservaciones.Text = leer.Campo("Observaciones");
            txtObservaciones.Enabled = false;
            txtSubTotal.Text = leer.CampoDouble("SubTotal").ToString(sFormato);
            txtIva.Text = leer.CampoDouble("Iva").ToString(sFormato);
            txtTotal.Text = leer.CampoDouble("Total").ToString(sFormato);
            sExistenciaAplicada = leer.Campo("PolizaAplicada");

            // Folios generados por la Poliza 
            sFolioVentaEntrada = leer.Campo("FolioVentaEntrada");
            sFolioVentaSalida = leer.Campo("FolioVentaSalida");
            sFolioConsignacionEntrada = leer.Campo("FolioConsignacionEntrada");
            sFolioConsignacionSalida = leer.Campo("FolioConsignacionSalida");

            if (leer.Campo("Status").ToUpper() == "C")
            {
                bEstaCancelado = true;
            }

            if (leer.Campo("MovtoAplicado") == "S")
            {
                bMovtoAplicado = true;
                IniciarToolBar(true, false, false, true, true);
            }

            //Se cargan los detalles.
            CargarDetalles();

            // No permitir la edición de la informacion 
            myGrid.BloqueaColumna(true, (int)Cols.Costo);
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);

            // Cargar toda la informacion antes de mostrar el mensaje 
            if (bEstaCancelado)
            {
                lblCancelado.Text = "CANCELADO";
                lblCancelado.Visible = true;
                General.msjUser("La Póliza capturada se encuentra cancelada.");
            }
            else if (bMovtoAplicado)
            {
                lblCancelado.Text = "APLICADA";
                lblCancelado.Visible = true;
                General.msjUser("Esta Póliza ya fue aplicada a los Movimientos de Inventario,\n no es posible hacer modificaciones.");
            }
            else if (sExistenciaAplicada == "N")
            {
                General.msjUser("Esta Póliza no tiene la existencia aplicada,\n no es posible generar el Ajuste de Inventario.");
                btnNuevo_Click(null, null);
            }

            return bRegresa;
        }

        private void CargarDetalles()
        {
            myGrid.Limpiar(true);
            //leer.DataSetClase = query.AjusteInventario_Detalle(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesMovimiento");

            string sSql = string.Format("Set DateFormat YMD Select CodigoEAN, IdProducto, DescProducto, TasaIva, " + 
                " ExistenciaSistema, ExistenciaFisica, PrecioBase, Costo, Importe, 0, 0, UnidadDeSalida, ExistenciaActualFarmacia " +
                " From vw_AjustesInv_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Poliza = '{3}' Order By KeyxDetalle ",
                sEmpresa, sEstado, sFarmacia, sPoliza);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                    CargarLotes();

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        CargarLotes_Ubicaciones();
                    }
                }
                else
                {
                    General.msjUser("La Póliza ingresada no contiene detalles. Verifique.");
                }
            }
            else
            {
                Error.GrabarError(leer, "CargarDetalles()");
                General.msjError("Ocurrió un error al obtener el detalle de la Póliza");
            }
        }

        private void CargarLotes()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.AjusteInventario_Lotes(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarDetallesLotesMovimiento");
            Lotes.AddLotes(leer.DataSetClase);
        }

        private void CargarLotes_Ubicaciones()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.AjusteInventario_Lotes_Ubicaciones(sEmpresa, sEstado, sFarmacia, sPoliza, "CargarLotes_Ubicaciones");
            Lotes.AddLotesUbicaciones(leer.DataSetClase);

        }

        #endregion Buscar Folio

        #region Botones 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(false);
        }

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            bool bExito = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnExcel = btnExportarExcel.Enabled;


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
                    IniciarToolBar(false, false, false, false, false);

                    cnn.IniciarTransaccion();

                    bExito = GrabarMovimientoInventario();

                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        //txtFolio.Text = sPoliza;
                        IniciarToolBar(true, false, false, true, true);
                        General.msjUser(sMensaje);
                        ImprimirInventario();
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información de la Póliza.");
                        IniciarToolBar(true, bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnExcel);
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
                General.msjUser("Póliza de Inventario Físico inválida, verifique.");
            }

            return bRegresa;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInventario();
        }

        private void btnExportarExcel_Click( object sender, EventArgs e )
        {
            validarExportarDiferencias_Excel(true);
        }
        #endregion Botones        

        #region Grabar informacion 

        private bool GrabarMovimientoInventario()
        {
            bool bRegresa = true;
            string sFolios = "";
            string sSql = "";
            // string sFolioVentaEntrada = "", sFolioConsignacionEntrada = "", sFolioVentaSalida = "", sFolioConsignacionSalida = "";

            sSql = string.Format("Exec spp_Mtto_AjustesDeInventario \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', @IdPersonal = '{4}', @iMostrarResultado = '{5}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal, 1 );
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioVentaEntrada = leer.Campo("FolioVentaEntrada");
                sFolioVentaSalida = leer.Campo("FolioVentaSalida");
                sFolioConsignacionEntrada = leer.Campo("FolioConsignacionEntrada");
                sFolioConsignacionSalida = leer.Campo("FolioConsignacionSalida");

                if (sFolioVentaEntrada != "")
                {
                    sFolios = sFolioVentaEntrada;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaEntrada);
                        bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioVentaEntrada); 
                    }
                }
                if (sFolioVentaSalida != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioVentaSalida : sFolioVentaSalida;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaSalida);
                        bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioVentaSalida); 
                    }
                }

                if (sFolioConsignacionEntrada != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioConsignacionEntrada : sFolioConsignacionEntrada;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionEntrada);
                        bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioConsignacionEntrada); 
                    }
                }

                if (sFolioConsignacionSalida != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioConsignacionSalida : sFolioConsignacionSalida;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionSalida);
                        bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioConsignacionSalida); 
                    }
                }

                if(sFolios != "")
                {
                    sMensaje = "La Póliza se aplicó exitosamente con los Folios :  " + sFolios;
                }
                else
                {
                    sMensaje = "La Póliza se aplicó exitosamente.";
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
            //if (!bPolizaGuardada)
            //{
            //    if (!bEstaCancelado)
            //    {
            //        if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            //        {
            //            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
            //            {
            //                myGrid.Rows = myGrid.Rows + 1;
            //                myGrid.ActiveRow = myGrid.Rows;

            //                if (bEntradaSalida)
            //                    myGrid.BloqueaCelda(false, myGrid.Rows, (int)Cols.Costo);
                            
            //                myGrid.SetActiveCell(myGrid.Rows, 1); 
            //            }
            //        }
            //    }
            //}
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
                            CargarDatosProducto(); 
                    }

                    //if (e.KeyCode == Keys.Delete)
                    //    removerLotes();

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
                                CargarDatosProducto();
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
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, true, "CargarLotesCodigoEAN()");
            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);
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
            //if (!bPolizaGuardada)
            //{
            //    if (!bEstaCancelado)
            //    {
            //        try
            //        {
            //            int iRow = myGrid.ActiveRow;
            //            Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
            //            myGrid.DeleteRow(iRow);
            //        }
            //        catch { }

            //        if (myGrid.Rows == 0)
            //            myGrid.Limpiar(true);
            //    }
            //}
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
                    Lotes.Show();

                    //if (!bMovtoAplicado)
                    //{
                    //    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    //    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    //    myGrid.SetActiveCell(iRow, (int)Cols.Costo);
                    //}
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
                if(General.msjConfirmar("¿ Desea limpiar la información en pantalla, se perderá lo que este capturado. ?") == DialogResult.No)
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
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ManejoLotes = OrigenManejoLotes.Inventarios;

                // Activar barra de menu 
                IniciarToolBar(true, false, false, false, false);

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

        private void IniciarToolBar(bool Nuevo, bool Guardar, bool Cancelar, bool Imprimir, bool Excel)
        {
            btnNuevo.Enabled = Nuevo;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            btnExportarExcel.Enabled = Excel; 
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

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un ajuste de inventario, verifique por favor.";
                //bRegresa = opPermisosEspeciales.VerificarPermisos("AJUSTE_INVENTARIO", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("AJUSTE_INVENTARIO", sMsjNoEncontrado);
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
                //else
                //{
                //    if (Lotes.CantidadTotal == 0)
                //    {
                //        bRegresa = false;
                //    }
                //    else
                //    {
                //        for (int i = 1; i <= myGrid.Rows; i++)
                //        {
                //            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                //            {
                //                bRegresa = false;
                //                break;
                //            }
                //        }
                //    }
                //}
            }

            if (!bRegresa)
                General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;
        }
        #endregion Validacion de datos

        #region Impresion de informacion
        private void ImprimirInventario()
        {
            if (sFolioVentaEntrada != "")
            {
                Imprimir(sFolioVentaEntrada, "Entrada de venta");
            }

            if (sFolioVentaSalida != "")
            {
                Imprimir(sFolioVentaSalida, "Salida de venta");
            }

            if (sFolioConsignacionEntrada != "")
            {
                Imprimir(sFolioConsignacionEntrada, "Entrada de consignación");
            }

            if (sFolioConsignacionSalida != "")
            {
                Imprimir(sFolioConsignacionSalida, "Salida de consignación");
            }

            ImprimirDiferenciasAjuste();
        }

        private void Imprimir(string Folio, string Titulo)
        {
            bool bRegresa = false;
            if (validarImpresion())
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
                myRpt.Add("Folio", Folio);

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


                if (!bRegresa)
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Ocurrió un error al cargar el reporte.");
                    }
                }

                ////{
                ////    LimpiarPantalla(false);
                ////}
                ////else

            }

            // LimpiarPantalla(false); 
        }

        private void ImprimirDiferenciasAjuste()
        {
            bool bRegresa = false;
            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirDiferenciasAjuste()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.TituloReporte = "Diferencias de inventario";
                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Ajustes_Inv_Diferencias.rpt";

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
                
                //// Generar el documento en Excel 
                {
                    validarExportarDiferencias_Excel(false); 
                }
            }
        }

        private void validarExportarDiferencias_Excel(bool Confirmar)
        {
            bool bExportar = !Confirmar;

            if(Confirmar)
            {
                bExportar = General.msjConfirmar("¿ Desea exportar a excel el archivo de Diferencias de Inventario ?", "Exportar información") == System.Windows.Forms.DialogResult.Yes;
            }

            if (bExportar)
            {
                ExportarDiferencias_Excel();
            }
        }

        private void ExportarDiferencias_Excel()
        {
            bool bRegresa = false;
            string sSql = "";

            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "REPORTE DE DIFERENCIAS DE INVENTARIO";
            string sNombreHoja = "Diferencias";
            string sConcepto = "REPORTE DE DIFERENCIAS DE INVENTARIO";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;



            sSql = string.Format("" +
                "Select \n"+ 
                "\tIdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, \n " +
                "\tPoliza, PolizaAplicada, MovtoAplicado, IdPersonal, NombrePersonal, FechaRegistro, IdClaveSSA, ClaveSSA, ClaveSSA_Base, \n " +
                "\tDescripcionClave, IdProducto, CodigoEAN, DescProducto, IdPresentacion, Presentacion, ContenidoPaquete, ClaveLote, \n  " +
                "\tEsConsignacion, ExistenciaFisica, Costo, Importe, ExistenciaSistema, FechaReg, FechaCad, FechaCad_Aux, \n " +
                "\tExistenciaActualFarmacia, Diferencia, Referencia, StatusDet_Lotes, Observaciones \n " +
                "From vw_AjustesInv_Det_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Poliza = '{3}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtFolio.Text);



            this.Cursor = Cursors.Default;
            if (!leer.Exec(sSql))
            {
                this.Cursor = Cursors.WaitCursor;
                Error.GrabarError(leer, "btnExportarExcel_Click()");
                General.msjError("Ocurrió un error al obtener la información de los productos.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No existe información para exportar, verifique.");
                }
                else
                {

                    DateTime dtpFecha = General.FechaSistema;
                    int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
                    //int iHoja = 1;
                    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
                    string sEstado = DtGeneral.EstadoConectadoNombre;
                    string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                    string sFechaImpresion = General.FechaSistemaFecha.ToString();

                    excel = new clsGenerarExcel();
                    excel.RutaArchivo = @"C:\\Excel";
                    excel.NombreArchivo = sNombreDocumento;
                    excel.AgregarMarcaDeTiempo = true;

                    if(excel.PrepararPlantilla(sNombreDocumento))
                    {
                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        excel.CerraArchivo();

                        excel.AbrirDocumentoGenerado(true);
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }
        #endregion Impresion de informacion

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


    }
}
