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

using DllTransferenciaSoft.IntegrarInformacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using Farmacia.Devoluciones_De_Transferencias;

namespace Farmacia.Transferencias
{
    public partial class FrmTransferenciaEntradas_Base : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, Factor = 6,
            Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        }

        // Ubicacion estandar para la recepcion de Entradas por transferencia 
        int iPasillo = 0, iEstante = 0, iEntrepano = 0;

        clsConexionSQL cnn; // = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerTransf;
        clsLeer leerTransf_Integrar;
        clsLeer leerUUIDS;
        clsConsultas query;
        clsAyudas ayuda;

        clsGrid myGrid;
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; 
        clsLotes Lotes_UUIDS;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        wsFarmacia.wsCnnCliente conexionWebTransferencia;
        string sUrlTransferencia = "";

        string sFolioTransferencia = "";
        string sMensajeGrabar = "";

        // bool bMovtoCancelado = false;
        // bool bEstaCancelado = false;
        bool bEstaCanceladoTrans = false;
        bool bEstaRegistrado = false;
        bool bAplicaFactor = false;
        // bool bExisteMovto = false;
        // bool bMovtoAplicado = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        // Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sIdPersonalConectado = ""; //DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        bool bImplementaCodificacion = false; //GnFarmacia.ImplementaCodificacion_DM; 
        string sEmpresa = ""; //DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = 0; //Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = ""; //DtGeneral.EstadoConectado;
        string sFarmacia = ""; //DtGeneral.FarmaciaConectada;
        string sCveRenapo = ""; //DtGeneral.ClaveRENAPO;
        string sIdTipoMovtoInv = "TE";
        string sTipoES = "E";
        string sIdProGrid = "";
        string sFolioTE = "";
        // int iAnchoColPrecio = 0;

        string FolioOrigen = "", FarmaciaOrigen = "";

        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias = null;
        ////new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        FrmDevolucionDeTransferencias_Salida fSalida;

        bool bGuardadoDeInformacion_Masivo = true;
        string sSql_Detallado = "";

        string sNombrePosicion = "TRANSFERENCIAS_ENTRADA";
        clsLeer leerUBI;

        int iAnchoColDescripcion = 0;
        int iAnchoColFactor = 0;
        bool bEsAlmacenUnidosis = false;//DtGeneral.EsAlmacen && GnFarmacia.EsUnidadUnidosis;
        bool bEsOrigenNormal = false;
        bool bTransferenciaInterestatal = false;
        int iEsInterestatal = 0;

        TipoDeTransferencia tpTipoDeTransferencia = TipoDeTransferencia.Ninguno;

        public FrmTransferenciaEntradas_Base(): this(TipoDeTransferencia.Farmacia_Normal)
        {
        }

        public FrmTransferenciaEntradas_Base(TipoDeTransferencia TipoTransferencia)
        {
            InitializeComponent();

            cnn = new clsConexionSQL(General.DatosConexion);



            bImplementaCodificacion = GnFarmacia.ImplementaCodificacion_DM; 
            bEsAlmacenUnidosis = DtGeneral.EsAlmacen && GnFarmacia.EsUnidadUnidosis;
            ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

            sIdPersonalConectado = DtGeneral.IdPersonal;
            sEmpresa = DtGeneral.EmpresaConectada;
            iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;
            sCveRenapo = DtGeneral.ClaveRENAPO;


            tpTipoDeTransferencia = TipoTransferencia; 


            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            conexionWebTransferencia = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWebTransferencia.Url = General.Url;

            leerUBI = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerTransf = new clsLeer(ref cnn);
            leerTransf_Integrar = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;

            myGrid.OcultarColumna(!GnFarmacia.EsUnidadUnidosis, Cols.Factor);

            myGrid.AjustarAnchoColumnasAutomatico = false;
            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);


            ////iAnchoColDescripcion = (int)grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width;
            ////iAnchoColFactor = (int)grdProductos.Sheets[0].Columns[(int)Cols.Factor - 1].Width;


            myGrid.AjustarAnchoColumnasAutomatico = true;



            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);


            //// Determinar si se Muestran las Columas de Precios 
            //if (!DtGeneral.EsAdministrador)
            //{
            //    if (!GnFarmacia.MostrarPrecioVentaEnVentaCredito)
            //    {
            //        iAnchoColPrecio = (int)grdProductos.Sheets[0].Columns[(int)Cols.Costo - 1].Width;
            //        iAnchoColPrecio += (int)grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Width;

            //        grdProductos.Sheets[0].Columns[(int)Cols.Costo - 1].Visible = false;
            //        grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Visible = false;

            //        grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width += (float)iAnchoColPrecio;
            //    }
            //}

            // Control de Acceso para lectura de Codigos DataMatrix 
            btnCodificacion.Visible = bImplementaCodificacion;
            btnCodificacion.Enabled = false;

            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
            {
                CargarComboEstados();
            }
            else
            {
                CargarEstadosFiliales();
            }

            Configurar_Interface(); 
        }

        #region Interface MA 
        private void CargarEstadosFiliales()
        {
            string sSql = string.Format(
                "Select Distinct IdEstado, Estado \n" +
                "From vw_EmpresasFarmacias \n " +
                "Where IdEmpresa = '{0}' and Status = 'A' and StatusRelacion = 'A' and  IdTipoUnidad not in ( 0, 5 )   \n " +
                " Order by IdEstado ", DtGeneral.EmpresaConectada);

            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstadosFiliales()");
                General.msjError("Error al obtener la lista de Estados filiales.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias;
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

        private void Configurar_Interface()
        {
            int iIncremento = 25;
            Point e = new Point(lblTitulo__FarmaciaOrigen.Location.X, lblTitulo__FarmaciaOrigen.Location.Y);
            Point c = new Point(txtFarmaciaOrigen.Location.X, txtFarmaciaOrigen.Location.Y);
            int iAnchoCombo = (lblFarmaciaOrigen.Left - txtFarmaciaOrigen.Left) + + lblFarmaciaOrigen.Width;

            lblTitulo__Estado.Visible = false;
            cboEstados.Visible = false; 

            //if (DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia_MA || DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen_MA)
            if ( GnFarmacia.Transferencias_Interestatales__Farmacias ) 
            {
                this.Height += iIncremento;
                ////this.FrameInformacionRegistro.Top += iIncremento;
                this.FrameDetallesTransferencia.Top += iIncremento;
                this.FrameDetallesTransferencia.Height -= iIncremento;
                this.FrameDatosGenerales.Height += iIncremento;

                foreach(Control control in FrameDatosGenerales.Controls )
                {
                    if(control.Name != lblTitulo__FechaRegistro.Name && control.Name != dtpFechaRegistro.Name)
                    {
                        control.Top += iIncremento;
                    }
                }


                lblTitulo__Estado.Location = e;
                cboEstados.Location = c;
                cboEstados.Width = (iAnchoCombo / 3);
                lblTitulo__Estado.Visible = true;
                cboEstados.Visible = true; 


            }
        }
        #endregion Interface MA

        #region Botones
        private void LimpiarPantalla(bool Confirmar)
        {
            bool bExito = true;

            if (Confirmar)
            {
                if(General.msjConfirmar("¿ Desea limpiar la información en pantalla ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if (bExito)
            {
                linkUrlFarmacia.Text = "";
                IniciarToolBar(false, false, false, false);

                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes_UUIDS = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;
                Lotes.sPosicionEstandar = sNombrePosicion;

                SKU = new clsSKU();
                SKU.TipoDeMovimiento = sIdTipoMovtoInv; 

                MostrarFactor(false);
                MostrarFactor(GnFarmacia.EsUnidadUnidosis);

                chkDesglosado.Visible = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;


                //// bEstaCancelado = false;
                btnCodificacion.Enabled =  false;
                bEstaCanceladoTrans = false;
                bEstaRegistrado = false;

                IniciarToolBar(false, false, false, false);
                lblFolioOrigen.Visible = false;
                lblStatusFolio.Visible = false;
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
                btnGenerarPaqueteDatos.Enabled = false;

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
            IniciarToolBar(Guardar, Cancelar, Imprimir, Transferencias, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Transferencias, bool Devolucion)
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

            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnObtenerTransferencias.Enabled = Transferencias;
            btnDevolucion.Enabled = Devolucion;
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

            if (ValidaTransferencia())
            {
                if (validarDatos())
                {
                    if (validaUUIDS())
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

                            ////// Generar el Movimiento de Inventario 
                            if (GrabarMovtoEncabezado())
                            {
                                ////// Generar la transferencia de salida 
                                if (GrabarTransferenciaEncabezado())
                                {
                                    if (ActualizarCostos())
                                    {
                                        ////// Generar la informacion de la transferencia de entrada 
                                        if (GrabarStatusTransferencia())
                                        {
                                            bExito = true;
                                            if (bImplementaCodificacion)
                                            {
                                                bExito = false;
                                                if (CodificacionSNK.Registrar_UUID(leer, Lotes))
                                                {
                                                    if (CodificacionSNK.Guardar_UUIDS_Movimientos_De_Inventario(sFolioMovto, leer, Lotes, true, false))
                                                    {
                                                        bExito = true;
                                                    }
                                                }
                                            }

                                            if (bExito)
                                            {
                                                bExito = AfectarExistencia(true, true);
                                            }

                                            if (fSalida != null)
                                            {
                                                if(fSalida.bMovtoAplicado == true)
                                                {
                                                    bExito = fSalida.Guardar(ref cnn, sFolioTransferencia);
                                                }
                                            }


                                        }
                                    }
                                }
                            }

                            if (!bExito)
                            {
                                cnn.DeshacerTransaccion();
                                txtFolio.Text = "*";
                                Error.GrabarError(leer, "btnGuardar_Click");
                                General.msjError("Error al guardar traspaso.");
                                IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, false);
                            }
                            else
                            {
                                cnn.CompletarTransaccion();
                                IniciarToolBar(false, false, true, false);
                                General.msjUser(sMensajeGrabar);

                                ImprimirInformacion();
                            }

                            cnn.Cerrar();
                        }
                    }
                    else
                    {
                        General.msjAviso("Los productos del Traspaso no coinciden con los escaneados.");
                    }
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

        private void btnObtenerTransferencias_Click(object sender, EventArgs e)
        {
            string sMsj = "¿ Desea Realizar La Busqueda en Origen del Traspaso ?";
            ////FolioOrigen = "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8);
            ////FarmaciaOrigen = Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (BuscarTransferenciaEnOrigen())
                {
                    txtFolio_Validating(this, null);
                }
            }
        }

        #endregion Botones

        #region Impresion
        private void ImprimirInformacion()
        {
            bool bRegresa = false;

            TipoReporteTransferencia TipoImpresion = !chkTipoImpresion.Checked ? TipoReporteTransferencia.Detallado : TipoReporteTransferencia.Ticket;

            string sFolio = "TE" + txtFolio.Text;

            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";

                ClsImprimirTransferencias imprimir = new ClsImprimirTransferencias(cnn.DatosConexion, DatosCliente, "", false, TipoImpresion);

                bRegresa = imprimir.Imprimir(sFolio, chkDesglosado.Checked);

                if (btnDevolucion.Enabled)
                {
                    //if (fSalida == null)
                    {
                        fSalida = new FrmDevolucionDeTransferencias_Salida();
                        fSalida.CargarTransferencia(cboEstados.Data, txtFarmaciaOrigen.Text, lblFarmaciaOrigen.Text, txtFolioOrigen.Text, false);
                    }
                                        
                    fSalida.ImprimirInventario();
                }

                //clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                //// byte[] btReporte = null;

                //myRpt.RutaReporte = GnFarmacia.RutaReportes;

                //if (!chkTipoImpresion.Checked)
                //{
                //    myRpt.NombreReporte = "PtoVta_Transferencias.rpt";
                //}
                //else
                //{
                //    myRpt.NombreReporte = "PtoVta_TransferenciasTicket.rpt";
                //}

                //myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                //myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                //myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                //myRpt.Add("Folio", "TE" + txtFolio.Text);

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (bRegresa)
                {
                    LimpiarPantalla(false);
                }
                else
                {
                    if(!DtGeneral.CanceladoPorUsuario)
                    {
                        General.msjError("Error al cargar el Informe.");
                    }
                }
            }
        }
        #endregion Impresion

        #region Validacion de informacion
        private bool validarDatos()
        {
            bool bRegresa = true;

            if (bRegresa && bEstaCanceladoTrans)
            {
                bRegresa = false;
                General.msjUser("Folio de Traspaso cancelado. No se pueden realizar cambios.");
            }

            if (bRegresa && bEstaRegistrado)
            {
                bRegresa = false;
                General.msjUser("Folio de Traspaso applicado. No se pueden realizar cambios.");
            }

            if (bRegresa && txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Traspaso no valido. Favor de verificar.");
                txtFolio.Focus();
            }

            if (bRegresa && txtFarmaciaOrigen.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Capturar Farmacia Origen. Favor de verificar.");
                txtFarmaciaOrigen.Focus();
            }

            if (bRegresa)
            {
                bRegresa = validarTransferenciaRegistrada();
            }

            ////if (bRegresa && Convert.ToDouble(txtSubTotal.Text) == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("El sub-total de la factura no puede ser igual a cero, verfique.");
            ////    txtSubTotal.Focus();
            ////}

            //////if (bRegresa && Convert.ToDouble(txtIva.Text) == 0)
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("El iva de la factura no puede ser igual a cero, verfique.");
            //////    txtIva.Focus();
            //////}

            ////if (bRegresa && Convert.ToDouble(txtTotal.Text) == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("El total de la factura no puede ser igual a cero, verfique.");
            ////    txtTotal.Focus();
            ////}

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

            if (bRegresa && bAplicaFactor)
            {
                bRegresa = ValidarFactor();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "Usuario sin permisos para realizar Traspaso. Favor de verificar.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("TRANSFERENCIA_ENTRADA", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("TRANSFERENCIA_ENTRADA", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private bool ValidarFactor()
        {
            bool bRegresa = true;

            for (int i = 1; myGrid.Rows >= i && bRegresa; i++)
            {
                bRegresa = myGrid.GetValueInt(i, (int)Cols.Factor) > 0 ? true : false;
            }

            if (!bRegresa)
            {
                General.msjUser("Existen productos sin factor de unidosis, verifique.");
            }

            return bRegresa;
        }

        private bool validaUUIDS()
        {
            bool bRegresa = true;
            clsLeer LeerUUIDSRegistrados = new clsLeer();
            clsLeer LeerAux = new clsLeer();
            DataSet pDtsUUIDS;
            string sSelect;
            LeerUUIDSRegistrados.DataSetClase = Lotes.UUID_List.DataSetClase;

            if (bImplementaCodificacion)
            {
                if (leerUUIDS.Registros != LeerUUIDSRegistrados.Registros)
                {
                    bRegresa = false;
                }
                else
                {
                    pDtsUUIDS = leerUUIDS.DataSetClase;

                    LeerUUIDSRegistrados.RegistroActual = 0;

                    while (LeerUUIDSRegistrados.Leer())
                    {
                        sSelect = string.Format("UUID = '{0}'", LeerUUIDSRegistrados.Campo("UUID"));
                        LeerAux.DataRowsClase = pDtsUUIDS.Tables[0].Select(sSelect);

                        if (!LeerAux.Leer())
                        {
                            bRegresa = false;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool validarTransferenciaRegistrada()
        {
            bool bRegresa = true;
            string sMsj = "";
            string sStatus = "";

            leer.DataSetClase = query.FolioTransferenciaEntrada(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "txtFolio_Validating");
            if (!leer.Leer())
            {
                sMsj = "No fue posible validar el Traspaso. Favor de verificar.";
                bRegresa = false;
            }
            else
            {
                if (sFarmacia == leer.Campo("IdFarmaciaRecibe"))
                {
                    sStatus = leer.Campo("Status").ToUpper();
                    if (leer.Campo("FolioReferencia") != "*")
                    {
                        bRegresa = false;
                        sMsj = "Folio Traspaso aplicado. No se pueden realizar cambios.";
                    }
                }
            }


            if (!bRegresa)
            {
                General.msjAviso(sMsj);
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
            {
                General.msjUser("Capture un producto para el Traspaso o cantidades para el Producto(s). Favor de verificar.");
            }

            return bRegresa;

        }

        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }
        #endregion Validacion de informacion

        #region Guardar datos tranferencia
        private bool GrabarStatusTransferencia()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFolioTE_Generado = "TE" + Fg.PonCeros(txtFolio.Text, 8);  

            ////// Marcar ambas Transferencias como Integradas 
            sSql = string.Format(" Update TransferenciasEnvioEnc " + 
                " Set Status = 'I', Actualizado = 0 Where IdEmpresa = '{0}' " + 
                " and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' and FolioTransferencia = '{3}' \n\n",
                sEmpresa, cboEstados.Data, Fg.PonCeros(txtFarmaciaOrigen.Text, 4), sFolioTE);


            sSql += string.Format(" Update TransferenciasEnc " +
                " Set Status = 'I', Actualizado = 0 Where IdEmpresa = '{0}' " + 
                " and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioTransferencia = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTE_Generado);

            ////// Asegurar que se modifique el Status de la transferencia de entrada recibida.
            //bRegresa = leer.Exec(sSql);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
                sSql_Detallado += string.Format("{0}\n", sSql);
            }
            else
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;
        }

        private bool GrabarTransferenciaEncabezado()
        {
            bool bRegresa = false;
            string sSql = ""; 
            string sFolio = SKU.Foliador;
            iEsInterestatal = cboEstados.Data == DtGeneral.EstadoConectado ? 0 : 1;

            bool bMovtoAplicado = false;

            if (fSalida != null)
            {
                bMovtoAplicado = fSalida.bMovtoAplicado;
            }

            sSql = string.Format("Exec spp_Mtto_TransferenciasEnc \n" +
                "\n@IdEmpresa = '{0}', @IdEstado = '{1}', @CveRenapo = '{2}', @IdFarmacia = '{3}', \n" +
                "\n@IdAlmacen = '{4}', @EsTransferenciaAlmacen = '{5}', @FolioTransferencia = '{6}', @FolioMovtoInv = '{7}', @FolioMovtoInvRef = '{8}', \n" +
                "\n@FolioTransferenciaRef = '{9}', @TipoTransferencia = '{10}', @DestinoEsAlmacen = '{11}', @IdPersonal = '{12}', @Observaciones = '{13}', \n" +
                "\n@SubTotal = '{14}', @Iva = '{15}', @Total = '{16}', \n" +
                "\n@IdEstadoRecibe = '{17}', @CveRenapoRecibe = '{18}', @IdFarmaciaRecibe = '{19}', @IdAlmacenRecibe = '{20}',  @TieneDevolucion = '{21}' \n",
                sEmpresa, sEstado, sCveRenapo, sFarmacia, "00", iEsInterestatal, sFolio, sFolioMovto, "", sFolioTE, sIdTipoMovtoInv,
                0, DtGeneral.IdPersonal, txtObservaciones.Text,
                txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""),
                cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, "00", bMovtoAplicado);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                //Error.GrabarError(leer, "GrabarTransferenciaEncabezado()");
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
            bool bRegresa = false;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0, iFactor = 0;
            double nCosto = 0, nTasaIva = 0, nSubTotal = 0, nImporteIva = 0, nImporte = 0;
            int iUnidadDeSalida = 0;

            double nImporteMinimo = 0.0001;

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iFactor = myGrid.GetValueInt(i, (int)Cols.Factor);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);

                nSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                nImporteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                nImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                nImporteIva = nImporteIva < nImporteMinimo ? 0.0000 : nImporteIva;

                if (!bAplicaFactor)
                {
                    iFactor = 1;
                }

                if (sIdProducto != "")
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @UnidadDeEntrada = '{7}', @Cant_Enviada = '{8}',\n" +
                        "\t@Cant_Devuelta = '{9}', @CantidadEnviada = '{10}', @CostoUnitario = '{11}', @TasaIva = '{12}', @SubTotal = '{13}',\n" +
                        "\t@ImpteIva = '{14}', @Importe = '{15}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia,
                        sIdProducto, sCodigoEAN, i, iUnidadDeSalida, iCantidad * iFactor, 0, 0, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);
                        bRegresa = GrabarTransferenciaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto, iFactor);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        if(!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            bRegresa = GrabarTransferenciaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto, iFactor);
                            if(!bRegresa)
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

        private bool GrabarTransferenciaDetalleLotes(int Renglon, string IdProducto, string CodigoEAN, double Costo, int Factor)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            foreach (clsLotes L in ListaLotes)
            {
                // Registrar el producto en las tablas de existencia 
                //sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                //    sEstado, sFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDetLotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmaciaEnvia = '{3}',\n" +
                        "\t@FolioTransferencia = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}',\n" +
                        "\t@CantidadEnviada = '{9}', @SKU = '{10}' \n",
                       sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioTransferencia, IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Cantidad * Factor, L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarTransferencia_Ubicaciones(L, IdProducto, CodigoEAN, L.ClaveLote, L.IdSubFarmacia, Renglon, Factor);
                        }
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        if(!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            bRegresa = true;
                            if(GnFarmacia.ManejaUbicaciones)
                            {
                                bRegresa = GrabarTransferencia_Ubicaciones(L, IdProducto, CodigoEAN, L.ClaveLote, L.IdSubFarmacia, Renglon, Factor);
                                if(!bRegresa)
                                {
                                    bRegresa = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarTransferencia_Ubicaciones(clsLotes Lote, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, int Renglon, int Factor)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, IdProducto, CodigoEAN, ClaveLote, IdSubFarmacia);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0) 
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDetLotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmaciaEnvia = '{3}',\n" +
                        "\t@FolioTransferencia = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}',\n" +
                        "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @CantidadEnviada = '{12}', @SKU = '{13}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, IdSubFarmacia, sFolioTransferencia,
                        IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Pasillo, L.Estante, L.Entrepano, L.Cantidad * Factor, L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        bRegresa = leer.Exec(sSql);
                        if(!bRegresa)
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }
            return bRegresa;
        }
        #endregion Guardar datos tranferencia

        #region Guardar movimiento de inventario 
        private bool ActualizarCostos()
        {
            bool bRegresa = false; 
            string sSql = string.Format("Exec spp_Mtto_ActualizarCostoUnitarioTransferencias " + 
                " @IdEmpresa = '{0}', @IdEstadoEnvia = '{1}', @IdFarmaciaEnvia = '{2}', @FolioSalida = '{3}', " + 
                " @IdEstadoRecibe = '{4}', @IdFarmaciaRecibe = '{5}', @FolioEntrada = '{6}' ",
                DtGeneral.EmpresaConectada, cboEstados.Data, txtFarmaciaOrigen.Text, "TS" + txtFolioOrigen.Text,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioMovto);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
                sSql_Detallado += string.Format("{0}\n", sSql);
            }
            else
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            bool bRegresa = false; 
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            ////string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
            ////    sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' " +
                "\n" +
                " Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);


            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
                sSql_Detallado += string.Format("{0}\n", sSql);
            }
            else
            {
                bRegresa = leer.Exec(sSql);
            }

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioMovto);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(cnn, sFolioMovto);
            }

            //// GUARDADO MASIVO DE INFORMACIÓN 
            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = leer.Exec(sSql_Detallado);
            }

            return bRegresa;
        }

        private bool GrabarMovtoEncabezado()
        {
            bool bRegresa = false;
            string sSql = "";

            SKU.Reset(); 

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                sEmpresa, sEstado, sFarmacia, "*", sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, txtObservaciones.Text.Trim(),
                General.GetFormatoNumerico_Double(txtSubTotal.Text), 
                General.GetFormatoNumerico_Double(txtIva.Text), 
                General.GetFormatoNumerico_Double(txtTotal.Text), 1, SKU.SKU
                );

            sSql_Detallado = "";
            ////if(bGuardadoDeInformacion_Masivo)
            ////{
            ////    bRegresa = true;
            ////}


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


                if(Registrar_SKUs())
                {
                    bRegresa = GrabarMovtoDetalle();
                }
            }

            return bRegresa;
        }

        private bool Registrar_SKUs()
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Mtto_Transferencias_Registrar_SKU \n" +
                "@IdEmpresa_Envia = '{0}', @IdEstado_Envia = '{1}', @IdFarmacia_Envia = '{2}', @FolioTransferencia = 'TS{3}'", 
                DtGeneral.EmpresaConectada, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text 
                );

            if(bGuardadoDeInformacion_Masivo)
            {
                sSql_Detallado += sSql;
                bRegresa = true;
            }
            else
            {
                bRegresa = leer.Exec(sSql);
            }

            return bRegresa;  
        }

        private bool GrabarMovtoDetalle()
        {
            bool bRegresa = false;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0, iFactor = 0;
            double nCosto = 0, nImporte = 0, nTasaIva = 0;
            int iUnidadDeSalida = 0;

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iFactor = myGrid.GetValueInt(i, (int)Cols.Factor);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);
                nImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (!bAplicaFactor)
                {
                    iFactor = 1;
                }

                if (sIdProducto != "")
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' \n" +
                        "Exec spp_Mtto_FarmaciaProductos_CodigoEAN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' \n",
                        sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);

                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', @Cantidad = '{8}', \n" +
                            "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                            sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            nTasaIva, iCantidad * iFactor, nCosto, nImporte, 'A');

                        sSql_Detallado += string.Format("{0}\n", sSql);
                        bRegresa = GrabarMovtoDetalleLotes(sIdProducto, sCodigoEAN, nCosto, iFactor);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        if(!leer.Exec(sSql))
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
                                nTasaIva, iCantidad * iFactor, nCosto, nImporte, 'A');
                            if(!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                            else
                            {
                                bRegresa = GrabarMovtoDetalleLotes(sIdProducto, sCodigoEAN, nCosto, iFactor);
                                if(!bRegresa)
                                {
                                    bRegresa = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotes(string IdProducto, string CodigoEAN, double Costo, int Factor)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}', @SKU = '{9}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote,
                            General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);

                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                            "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad * Factor, Costo / Factor, (L.Cantidad * Costo) / Factor, "A", L.SKU);

                        sSql_Detallado += string.Format("{0}\n", sSql);

                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            GrabarMovto_Ubicaciones(L, IdProducto, CodigoEAN, L.ClaveLote, L.IdSubFarmacia, Factor);
                        }
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        if(!leer.Exec(sSql))
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
                                sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad * Factor, Costo / Factor, (L.Cantidad * Costo) / Factor, "A", L.SKU);
                            if(!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                            else
                            {
                                bRegresa = true;
                                if(GnFarmacia.ManejaUbicaciones)
                                {
                                    bRegresa = GrabarMovto_Ubicaciones(L, IdProducto, CodigoEAN, L.ClaveLote, L.IdSubFarmacia, Factor);
                                    if(!bRegresa)
                                    {
                                        bRegresa = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovto_Ubicaciones(clsLotes Lote, string IdProducto, string CodigoEAN, string ClaveLote, string IdSubFarmacia, int Factor)
        {
            bool bRegresa = false;
            string sSql = "";
            // int iPasillo = 0, iEstante = 0, iEntrepano = 0;

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, ClaveLote, IdSubFarmacia);

            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}',\n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Pasillo, L.Estante, L.Entrepano, L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);

                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', \n" +
                            "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, IdSubFarmacia, sFolioMovto,
                            IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad * Factor, L.Pasillo, L.Estante, L.Entrepano, "A", L.SKU);

                        sSql_Detallado += string.Format("{0}\n", sSql);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        if(!leer.Exec(sSql))
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
                                DtGeneral.EmpresaConectada, sEstado, sFarmacia, IdSubFarmacia, sFolioMovto,
                                IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad * Factor, L.Pasillo, L.Estante, L.Entrepano, "A", L.SKU);

                            bRegresa = leer.Exec(sSql);
                            if(!bRegresa)
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
        #endregion Guardar movimiento de inventario

        #region Cargar Datos
        private bool CargarDatosFarmacia()
        {
            bool bRegresa = true;

            bEsAlmacenUnidosis = DtGeneral.EsAlmacen && GnFarmacia.EsUnidadUnidosis;
            bEsOrigenNormal = !leer.CampoBool("EsUnidosis"); //// && leer.CampoBool("EsAlmacen");


            bAplicaFactor = GnFarmacia.EsUnidadUnidosis && bEsOrigenNormal ? true : false;
            //bAplicaFactor = bEsOrigenNormal ? true : false;

            ////if ( GnFarmacia.EsUnidadUnidosis && bEsOrigenNormal )
            ////{
            ////    bAplicaFactor = true; 
            ////}





            if (leer.Campo("FarmaciaStatus").ToUpper() == "C" && bRegresa)
            {
                General.msjUser("La farmacia seleccionada actualmente se encuentra cancelada, no es posible generar la transferencia.");
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

        private void CargaDetalleTransferenciaEntrada()
        {
            leer.DataSetClase = query.FolioTransferenciaEntradaDetalle(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "CargaDetalleTransferenciaEntrada");
            myGrid.Limpiar(false);
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargaDetalleLotesTransferenciaEntrada();

                if (bImplementaCodificacion)
                {
                    CargaDetalleTransferenciaEntradaUUIDS();
                }
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN); 
            Totalizar();
        }

        private void CargaDetalleLotesTransferenciaEntrada()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaEntradaDetalleLotes(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "CargaDetalleLotesTransferenciaEntrada");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.FolioTransferenciaEntradaDetalleLotes_Ubicaciones
                    (
                        sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, 
                        iPasillo, iEstante, iEntrepano, 
                        "CargaDetalleLotesTransferenciaEntrada"
                    );
                Lotes.DataSetUbicaciones = leer.DataSetClase; 
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

            lblStatusFolio.Visible = false;
            leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, bTransferenciaInterestatal, ValidarOrigen, "txtFolio_Validating_1");
            if (!leer.Leer())
            {
                bRegresa = false;
                txtFolio.Text = ""; 
            }
            else 
            {
                cboEstados.Enabled = false;
                cboEstados.Data = leer.Campo("IdEstadoRecibe");
                txtFarmaciaOrigen.Enabled = false;
                txtFolioOrigen.Enabled = false;
                txtFolio.Enabled = false;
                IniciarToolBar(false, false, true, false);

                dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia"); 
                txtFolio.Text = Fg.PonCeros(leer.Campo("Folio"), 8);
                txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
                lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");
                txtFolioOrigen.Text = leer.Campo("FolioTransferenciaRef").Substring(2);

                btnDevolucion.Enabled = leer.CampoBool("TieneDevolucion");

                txtObservaciones.Text = leer.Campo("Observaciones");
                txtObservaciones.Enabled = false;
                sStatus = leer.Campo("Status").ToUpper();

                CargarDetallesTransferencia();
                

                if (MostrarMensaje)
                {
                    bEstaRegistrado = true;
                    lblFolioOrigen.Visible = true;
                    lblFolioOrigen.Text = "APLICADO";
                    btnGenerarPaqueteDatos.Enabled = true;
                    General.msjUser("Folio Traspaso Aplicado. No se pueden realizar cambios.");
                }
            }

            return bRegresa;
        }

        private void CargarDetallesTransferencia()
        {
            myGrid.Limpiar(false);
            leer.DataSetClase = query.FolioTransferenciaDetalles(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            if (leer.Registros > 0)
            {
                myGrid.Limpiar();
                for (int i = 1;leer.Leer();i++)
                {
                    //myGrid.LlenarGrid(leer.DataSetClase);
                    myGrid.AddRow();
                    myGrid.SetValue(i,(int)Cols.CodEAN, leer.Campo("CodigoEAN"));
                    myGrid.SetValue(i,(int)Cols.Codigo, leer.Campo("IdProducto"));
                    myGrid.SetValue(i, (int)Cols.Descripcion, leer.Campo("DescProducto"));
                    myGrid.SetValue(i, (int)Cols.TasaIva, leer.CampoInt("TasaIva"));
                    myGrid.SetValue(i, (int)Cols.Cantidad, leer.CampoDouble("Cantidad"));
                    myGrid.SetValue(i, (int)Cols.Factor, leer.CampoInt("Factor"));
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
            leer.DataSetClase = query.FolioTransferenciaDetallesLotes(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                string sFolio = sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8);
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones_TransferenciaDeEntrada(sEmpresa, sEstado, sFarmacia, sFolio, "CargarDetallesTransferencia");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }

        }

        private void CargaDetalleTransferenciaEntradaUUIDS()
        {
            leer.DataSetClase = query.FolioTransferenciaEntrada_UUID(sEmpresa, sEstado, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "CargaDetalleTransferenciaEntradaUUIDS()");
               
            leerUUIDS.DataSetClase = leer.DataSetClase;
        }

        #endregion Cargar Datos

        #region Manejo Grid
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
                        myGrid.SetValue(iRow, (int)Cols.Costo, 0);
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
            //int iRow = myGrid.ActiveRow;
            //Lotes.RemoveLotes(myGrid.GetValue(iRow, (int)Cols.Codigo), myGrid.GetValue(iRow, (int)Cols.CodEAN));
            //myGrid.DeleteRow(iRow);
            //Totalizar();

            //if (myGrid.Rows == 0)
            //    myGrid.Limpiar(true); 
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

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = false; // Asegurar que den entrada a todo lo enviado, en caso de no llegar todo

                    Lotes.EsTransferenciaDeEntrada = true;

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

        private void FrmTransferenciaEntradas_Load(object sender, EventArgs e)
        {
            Carga_UbicacionEstandar();
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

        private void txtFarmaciaOrigen_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;
            linkUrlFarmacia.Text = "";
            string sInf_Origen = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada;
            string sInf_Destino = cboEstados.Data + Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

            iEsInterestatal = cboEstados.Data == DtGeneral.EstadoConectado ? 0 : 1;


            if (txtFarmaciaOrigen.Text.Trim() != "" && txtFarmaciaOrigen.Enabled)
            {
                if (sInf_Origen == sInf_Destino)
                {
                    General.msjUser("No se puede registrar una transferencia de la Farmacia destino, verifique.");

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
                        linkUrlFarmacia.Text = sUrlTransferencia;
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

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sStatus = "";

            if (txtFolioOrigen.Text.Trim() != "" && txtFolioOrigen.Enabled)
            {
                IniciarToolBar(false, false, false, false);
                lblFolioOrigen.Visible = false;
                bEstaCanceladoTrans = false;
                bEstaRegistrado = false;

                sFolioTE = Fg.PonCeros(sEstado, 2) + Fg.PonCeros(txtFarmaciaOrigen.Text, 4) + sCveRenapo + "TE" + Fg.PonCeros(txtFolioOrigen.Text, 8); // 250001SLTS
                sFolioTE = "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8); // 250001SLTS
                leer.DataSetClase = query.FolioTransferenciaEntrada(sEmpresa, cboEstados.Data, sCveRenapo, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "txtFolio_Validating");
                if (leer.Leer())
                {
                    if (sFarmacia != leer.Campo("IdFarmaciaRecibe")) 
                    {
                        General.msjUser("Folio Traspaso no pertenece a esta farmacia. Favor de verificar.");

                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                    }
                    else 
                    {
                        sStatus = leer.Campo("Status").ToUpper();
                        IniciarToolBar(true, false, false, false);

                        cboEstados.Enabled = false;
                        txtFarmaciaOrigen.Enabled = false;
                        txtFolioOrigen.Enabled = false;
                        txtFolioOrigen.Text = Fg.Right(leer.Campo("Folio"), 8);
                        txtFolio.Enabled = false;
                        txtFolio.Text = Fg.Right(leer.Campo("FolioReferencia"), 8);  //"*"; 
                        //txtObservaciones.Enabled = false; 

                        CargaDetalleTransferenciaEntrada();

                        if (txtFolio.Text.Trim() == "*" && bImplementaCodificacion)
                        {
                            btnCodificacion.Enabled = btnGuardar.Enabled;
                        }                        

                        // Asegurar que solo se puedan Guardar las transferencias no procesadas 
                        if (txtFolio.Text == "*")
                        {
                            IniciarToolBar(true, false, false, false, true);

                            ////X MostrarFactor(bAplicaFactor);
                        }


                        if (sStatus == "C")
                        {
                            bEstaCanceladoTrans = true;
                            lblFolioOrigen.Visible = true;
                            lblFolioOrigen.Text = "CANCELADO";
                            General.msjUser("Folio Traspaso cancelado. No se pueden realizar cambios.");
                            txtFolioOrigen.Text = "";

                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                        }
                        // else if (sStatus == "E" || txtFolio.Text != "*" )  // Cambio a raiz del ajuste del metodo de envio.
                        else if (txtFolio.Text != "*") // 2K110607-1118 Jesus Diaz 
                        {
                            IniciarToolBar(false, false, true, false);

                            // Cargar la informacion que se registro 
                            CargarTransferencia();
                            
                            bEstaRegistrado = true;
                            lblFolioOrigen.Visible = true;
                            lblFolioOrigen.Text = "REGISTRADO";
                            btnGenerarPaqueteDatos.Enabled = true; 
                            General.msjUser("Folio Traspaso aplicado. No se pueden realizar cambios.");
                            //txtFolioOrigen.Text = "";

                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else
                {
                    string sMsj = "¿ Desea Realizar La Busqueda en el Origen de Traspaso ?";
                    ////FolioOrigen = "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8);
                    ////FarmaciaOrigen = Fg.PonCeros(txtFarmaciaOrigen.Text, 4);

                    if (General.msjConfirmar(sMsj) == DialogResult.Yes)
                    {
                        if (BuscarTransferenciaEnOrigen())
                        {
                            txtFolio_Validating(this, null);
                        }
                    }
                    else
                    {
                        btnObtenerTransferencias.Enabled = true;
                    }
                }
            }
        }

        private void MostrarFactor(bool bValor)
        {
            //if (!bValor)
            //{
            //    myGrid.AnchoColumna((int)Cols.Descripcion, iAnchoColDescripcion + iAnchoColFactor);
            //    myGrid.AnchoColumna((int)Cols.Factor, 0);
            //}
            //else
            //{
            //    myGrid.AnchoColumna((int)Cols.Descripcion, iAnchoColDescripcion);
            //    myGrid.AnchoColumna((int)Cols.Factor, iAnchoColFactor);
            //}
        }

        private void txtFolio_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtFolio.Text.Trim() != "" && txtFolio.Enabled)
            {
                e.Cancel = !CargarTransferencia(true, false);
                //lblStatusFolio.Visible = false;
                //leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "txtFolio_Validating_1");
                //if (leer.Leer())
                //{
                //    txtFarmaciaOrigen.Enabled = false;
                //    txtFolioOrigen.Enabled = false;
                //    txtFolio.Enabled = false;
                //    IniciarToolBar(false, false, true);

                //    txtFolio.Text = Fg.PonCeros(leer.Campo("Folio"),8);
                //    txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
                //    lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");
                //    txtFolioOrigen.Text = leer.Campo("FolioTransferenciaRef").Substring(2);

                //    txtObservaciones.Text = leer.Campo("Observaciones");
                //    sStatus = leer.Campo("Status").ToUpper();

                //    CargarDetallesTransferencia();
                //}
            }
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

        #region Buscar Transferencias Desde Origen
        private bool BuscarTransferenciaEnOrigen()
        {
            bool bRetorno = false, bSalir = false;
            bool bContinua = false, bTest = false;
            ////string FolioOrigen = "", FarmaciaOrigen = "";
            DataSet dtsTransferencia = null;  //= new DataSet();

            FolioOrigen = "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8);
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
                    General.msjError("Error al Obtener información de Traspaso.");
                }
            }
            catch (Exception ex1)
            {
                Error.GrabarError(ex1.Message, "BuscarTransferenciaEnOrigen");
                General.msjAviso("No fue posible establecer comunicación con la Unidad, intente de nuevo.");
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
                                General.msjAviso(string.Format(" Folio Traspaso {0} no pertenece a esta Unidad.\n\n" +
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
                txtFolio_Validating(null, null);
            }
            else
            {
                txtFolioOrigen.Text = "";
            }

            return bRetorno;
        }
        #endregion Buscar Transferencias Desde Origen

        #region Generar_Paquete_De_Datos
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
            string sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", sFolioTransferencia);
            sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", txtFolioOrigen.Text);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (PreparaTransferenciaReenvio())
                {
                    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                    ClienteTransferencias.TransferenciasAutomaticasIntegradas(cboEstados.Data, Fg.PonCeros(txtFarmaciaOrigen.Text, 4), DtGeneral.EstadoConectado, 
                        DtGeneral.FarmaciaConectada, "TS" + Fg.PonCeros(txtFolioOrigen.Text, 8));                    
                    General.msjAviso("Generación de Paquete de Datos terminada.");

                    ClienteTransferencias.Abrir_Directorio_Transferencias(); 
                }
                // ClienteTransferencias.EnviarArchivos(); 
            }
        }

        private bool PreparaTransferenciaReenvio()
        {
            bool bRegresa = true;
            string sSql = "";
            string sFolioRev = "TS" + txtFolioOrigen.Text;

            // sSql = string.Format("Update  Set Actualizado = 0 Where IdEstadoEnvia = '20' and IdFarmaciaEnvia = '0011' and IdFarmaciaRecibe = '0010' ");
            sSql = string.Format("Update TransferenciasEnvioEnc Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                cboEstados.Data, txtFarmaciaOrigen.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRev);

            sSql += string.Format("Update TransferenciasEnvioDet Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                cboEstados.Data, txtFarmaciaOrigen.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRev);

            sSql += string.Format("Update TransferenciasEnvioDet_Lotes Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                cboEstados.Data, txtFarmaciaOrigen.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRev);

            sSql += string.Format("Update TransferenciasEnvioDet_LotesRegistrar Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                cboEstados.Data, txtFarmaciaOrigen.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioRev);


            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaTransferenciaReenvio()");
                General.msjError("Ocurrió un error al preparar la transferencia para empaquetado.");
            }

            return bRegresa;
        }

        private void linkUrlFarmacia_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            // Specify that the link was visited.
            this.linkUrlFarmacia.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(linkUrlFarmacia.Text);
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            fSalida = new FrmDevolucionDeTransferencias_Salida();
            fSalida.CargarTransferencia(cboEstados.Data, txtFarmaciaOrigen.Text, lblFarmaciaOrigen.Text, txtFolioOrigen.Text, true);


        }
        #endregion Generar_Paquete_De_Datos

        #region Validar_Transferencia
        private bool ValidaTransferencia()
        {
            bool bRegresa = true, bExisteError = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_TransferenciasEnvioValidar  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaEnvia = '{3}', @FolioTransferencia = '{4}' ", 
                sEmpresa, cboEstados.Data, sFarmacia,  Fg.PonCeros(txtFarmaciaOrigen.Text, 4), sFolioTE);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ValidaTransferencia()");
                General.msjError("Error al validar Traspaso.");
            }
            else
            {
                if (leer.Leer())
                {
                    bExisteError = leer.CampoBool("ExisteError");

                    if (bExisteError)
                    {
                        bRegresa = false;
                        General.msjError("Existe error en el Traspaso. Favor de reportar al area de TI.");
                    }
                }
                else
                {
                    bRegresa = false;
                }
            }

            return bRegresa;
        }
        #endregion Validar_Transferencia

        #region Llena_Ubicacion_Estadar
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
        #endregion Llena_Ubicacion_Estadar

        private void btnCodificacion_Click(object sender, EventArgs e)
        {
            clsLeer leerUUIDS = new clsLeer();
            clsLotes lotes_Aux = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Todos);

            FrmLotesSNK f = new FrmLotesSNK();
            f.MostrarPantallaTE(Lotes_UUIDS);
            Lotes = f.LotesCodigos;
        }
    }
}
