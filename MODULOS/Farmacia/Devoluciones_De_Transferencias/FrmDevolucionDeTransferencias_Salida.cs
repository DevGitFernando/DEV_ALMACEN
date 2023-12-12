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

namespace Farmacia.Devoluciones_De_Transferencias
{
    public partial class FrmDevolucionDeTransferencias_Salida : FrmBaseExt
    {
        private enum Cols 
        {
            Ninguna = 0, 
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, CantidadDevuelta = 6,
            Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        }

        private enum ColsManual
        {
            Ninguna = 0,
            CodEAN = 1, ClaveSSA = 2, Descripcion = 3, Presentacion = 4, Cantidad = 5
        }

        #region variables 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerTrans;
        clsGrid myGrid;
        clsGrid myGridManual;
        clsCodigoEAN EAN = new clsCodigoEAN();
        // string sMsjEanInvalido = "";

        clsConsultas query;
        clsAyudas ayuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        string sIdPersonalConectado = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        clsVerificarSalidaLotes VerificarLotes;


        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias; // = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        // bool bExisteMovto = false;
        bool bEstaCancelado = false;
        public bool bMovtoAplicado = false;
        string sObservaciones = "";
        string sFolioDevolucion = "";
        string sFolioOrigen = "";
        string SEstadoOrigen = "";
        string sFarmaciaOrigen = "";
        bool bEsCancelacionDeTransferencia = false;
        bool bImplementaCodificacion = false;


        //string sFolioMovto = "";
        string sFolioTransferencia = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        ColsManual ColActivaManual = ColsManual.Ninguna;
        string sValorGrid = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sNombrePosicion = "TRANSFERENCIAS_ENTRADA";
        int iPasillo = 0, iEstante = 0, iEntrepaño = 0;
        clsLeer leerUBI;

        string sIdTipoMovtoInv = "SDT";
        string sTipoES = "S";
        string sTipoTransferencia = "TS";
        string sIdProGrid = "";
        bool bEntradaSalida = false;
        string sCveRenapo = DtGeneral.ClaveRENAPO;
        string sReferencia = "";

        bool bCapturaDeLotes = true;
        bool bModificarCantidades = true;
        bool bPermitirSacarCaducados = true;

        TiposDeInventario tipoDeInventario = TiposDeInventario.Todos; 

        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; 

        TipoDevolucion tpDevolucion = TipoDevolucion.TransferenciaDeSalida;

        clsMotivosDevoluciones motivodev;
        #endregion variables

        public FrmDevolucionDeTransferencias_Salida()
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


            myGridManual = new clsGrid(ref grdProductosManual, this);
            myGridManual.EstiloGrid(eModoGrid.ModoRow);
            myGridManual.BackColorColsBlk = Color.White;
            grdProductosManual.EditModeReplace = true;
            myGridManual.AjustarAnchoColumnasAutomatico = true;

            leer = new clsLeer(ref cnn);
            leerUBI = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);

            motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);

            CargarComboEstados();
            CargarMotivosDevolucion();
            Carga_UbicacionEstandar();
            LimpiarPantalla(false);
        }

        #region Botones 

        private void CargarComboEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(query.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias;
        }

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

                SKU = new clsSKU();
                SKU.IdEmpresa = sEmpresa;
                SKU.IdEstado = sEstado;
                SKU.IdFarmacia = sFarmacia;
                SKU.TipoDeMovimiento = sIdTipoMovtoInv;

                myGrid.AnchoColumna(Cols.Cantidad, 62);
                btnEjecutar.Enabled = false;
                bEntradaSalida = false;
                bCapturaDeLotes = false;
                bModificarCantidades = true; 
                bPermitirSacarCaducados = true;
                tipoDeInventario = TiposDeInventario.Todos; 
                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento, tipoDeInventario);
                Lotes.ManejoLotes = OrigenManejoLotes.Compras;

                Lotes.ModificarCantidades = true;

                motivodev = new clsMotivosDevoluciones(General.DatosConexion, tpDevolucion, sEmpresa, sEstado, sFarmacia);

                // Acitvar barra de menu 
                IniciarToolBar(false, false, false, false, false);
                                
                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGrid.Limpiar(false);
                myGridManual.Limpiar(true);
                Fg.IniciaControles();
                 
                dtpFechaRegistro.Enabled = false;
                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;

                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;
                //txtFolioEntrada.Focus();
                cboMotivosDev.SelectedIndex = 0;

                txtFarmaciaOrigen.Enabled = false;
                txtFolioOrigen.Enabled = false;


            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool MotivosDev, bool generarPaquete)
        {
            //btnNuevo.Enabled = true;
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = false; // Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnMotivosDev.Enabled = MotivosDev;
            btnGenerarPaqueteDeDatos.Enabled = generarPaquete;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            sObservaciones = txtObservaciones.Text;
            sFolioOrigen = txtFolioOrigen.Text.Trim();
            SEstadoOrigen = cboEstados.Data;
            sFarmaciaOrigen = txtFarmaciaOrigen.Text;
            sFolioDevolucion = txtFolioDevolucion.Text;

            if (validarDatos())
            {
                bMovtoAplicado = true;

                this.Hide();
            }
        }


        public bool Guardar(ref clsConexionSQL CNN, string sFolioTransferenciaDeEntrada)
        {

            bool bExito = false;

            leerTrans = new clsLeer(ref CNN);

            //bool bBtnGuardar = btnGuardar.Enabled;
            //bool bBtnCancelar = btnCancelar.Enabled;
            //bool bBtnImprimir = btnImprimir.Enabled;
            //bool bBtnTransferencia = btnGenerarPaqueteDeDatos.Enabled;

            if (validarDatos())
            {
                //    if (cnn.Abrir())
                //    {
                //        // Apagar la barra completa 
                //        IniciarToolBar();

                //        cnn.IniciarTransaccion();

                if (GrabarEncabezadoDevolucion(sFolioTransferenciaDeEntrada))
                {
                    //if (GrabarEncabezado())
                    if (GrabarDetalleEnvioTransferencia())
                    {
                        if (Grabar_Motivos_Devolucion())
                        {
                            bExito = AfectarExistenciaEnTransitoDevolucion(1);
                        }
                    }
                }

                //        if (bExito)
                //        {
                //            cnn.CompletarTransaccion();
                //            txtFolioDevolucion.Text = Fg.Right(sFolioTransferencia, 8); //sFolioMovto.Substring(2); 
                //            IniciarToolBar(false, false, true, true);
                //            General.msjUser("Información guardada satisfactoriamente con el folio " + sFolioTransferencia);
                //            ImprimirInventario();
                //        }
                //        else
                //        {
                //            cnn.DeshacerTransaccion();
                //            //txtFolioEntrada.Text = "*";
                //            Error.GrabarError(leer, "btnGuardar_Click");
                //            General.msjError("Ocurrió un error al grabar la información de la devolución de transferencia.");
                //            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnTransferencia);
                //        }

                //        cnn.Cerrar();
                //    }
                //    else
                //    {
                //        Error.LogError(cnn.MensajeError);
                //        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                //    }
            }

            return bExito;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnTransferencia = btnGenerarPaqueteDeDatos.Enabled;
            bool bBtnMotivosDev = btnMotivosDev.Enabled;

            bEsCancelacionDeTransferencia = true;
            sIdTipoMovtoInv = "TSC";
            sTipoES = "E";

            //if (ConsultarTransferenciaDestino())
            if (ValidaStatusIntegrada())
            {
                if (validarDatos(true))
                {
                    if (!cnn.Abrir())
                    {
                        Error.LogError(cnn.MensajeError);
                        General.msjAviso(General.MsjErrorAbrirConexion);
                    }
                    else
                    {
                        bool bExito = false;
                        IniciarToolBar();
                        cnn.IniciarTransaccion();


                        // Cancelar la transferencia de salida 
                        if (CancelaTransferenciaEncabezado())
                        {
                            bExito = BorraTransferenciasEnvio();
                        }

                        if (bExito)
                        {
                            bExito = AfectarExistenciaEnTransitoDevolucion(2);
                        }

                        if (bExito)
                        {
                            bExito = CancelaTransferenciaDetallado();
                        }

                        if (!bExito)
                        {
                            cnn.DeshacerTransaccion();
                            //txtFolio.Text = "*";
                            Error.GrabarError(leer, "btnCancelar_Click");
                            General.msjError("Ocurrió un error al Cancelar la transferencia.");
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnMotivosDev, bBtnTransferencia);
                        }
                        else
                        {
                            cnn.CompletarTransaccion();
                            lblFolioDevolucion.Visible = true;

                            // IMach  // Enlazar el folio de inventario 
                            //RobotDispensador.Robot.TerminarSolicitud(sFolioTransferencia);

                            General.msjUser(" La Transferencia de Salida se Cancelo Exitosamente...");
                            // EnvioAutomaticoDeTransferencias(); 
                            IniciarToolBar(false, false, true, false, false);
                            //Imprimir();
                        }

                        cnn.Cerrar();
                    }
                }
            }
        }

        private bool validarDatos(bool EsCancelacion)
        {
            bool bRegresa = true;


            if (bRegresa && txtFolioDevolucion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de transferencia inválido, verifique.");
                txtFolioDevolucion.Focus();
            }

            if (bRegresa)
            {
                // if (sIdTipoMovtoInv == "TS")
                if (!bEsCancelacionDeTransferencia)
                {
                    if (txtObservaciones.Text.Trim() == "")
                    {
                        bRegresa = false;
                        General.msjUser("No ha capturado las observaciones para la transferencia, verifique.");
                        txtObservaciones.Focus();
                    }
                }
                else
                {

                    clsObservaciones ob = new clsObservaciones();
                    ob.Encabezado = "Observaciones de Cancelación de devolución Transferencia de Salida";
                    ob.MaxLength = 150;
                    ob.Show();
                    sObservaciones += "\n\n" + ob.Observaciones + "\t" + sReferencia;
                    bRegresa = ob.Exito;
                }
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
                if (!bEsCancelacionDeTransferencia)
                {
                    ////VerificarLotes = new FrmVerificarSalidaLotes();
                    ////bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes.DataSetLotes);
                    bRegresa = validarExistenciasCantidadesLotes(false);
                }
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tienen permisos para realizar una transferencia de salida, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("TRANSFERENCIA_SALIDA", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("TRANSFERENCIA_SALIDA", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private bool validarExistenciasCantidadesLotes(bool MostrarMsj)
        {
            bool bRegresa = true;

            VerificarLotes = new clsVerificarSalidaLotes();
            bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes, MostrarMsj);

            return bRegresa;
        }

        private bool ValidaStatusIntegrada()
        {
            bool bRegresa = true;
            string sSql = "", Status = "";

            sSql = string.Format(" Select * From DevolucionTransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " +
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaStatusIntegrada()");
                General.msjError("Ocurrió un error al buscar el status.");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    Status = leer.Campo("Status");

                    if (Status == "I")
                    {
                        General.msjAviso(" La Transferencia ha sido integrada en el destino, No es posible generar cambios ");
                        bRegresa = false;
                    }
                    if (Status == "T")
                    {
                        General.msjAviso(" Transferencia en Transito, No es posible generar cambios ");
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private bool BorraTransferenciasEnvio()
        {
            bool bRegresa = true;

            string sSql = "";

            if (bImplementaCodificacion)
            {
                sSql = string.Format(" Delete From DevolucionTransferencias_UUID_Registrar " +
                                    " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n ",
                                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            }

            sSql += string.Format(" Delete From DevolucionTransferenciasEnvioDet_LotesRegistrar " +
                                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From DevolucionTransferenciasEnvioDet_Lotes " +
                                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From DevolucionTransferenciasEnvioDet " +
                                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From DevolucionTransferenciasEnvioEnc " +
                                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool CancelaTransferenciaEncabezado()
        {
            bool bRegresa = true;
            string sSql = "";
            sFolioTransferencia = "TS" + txtFolioDevolucion.Text;

            sSql = string.Format(" Update DevolucionTransferenciasEnc Set SubTotal = 0, Iva = 0, Total = 0, Status = 'C', " +
                    " IdPersonalCancela = '{4}', FechaCancelacion = getdate() " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}'\n\n ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CancelaTransferenciaEncabezado()");
            }

            return bRegresa;
        }

        private bool CancelaTransferenciaDetallado()
        {
            bool bRegresa = true;
            string sSql = "";
            sFolioTransferencia = "TS" + txtFolioDevolucion.Text;

            sSql = string.Format(" Update DevolucionTransferenciasDet Set Cant_Devuelta = Cant_Enviada, CantidadEnviada = 0, " +
                    " SubTotal = 0, ImpteIva = 0, Importe = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            sSql += string.Format(" Update DevolucionTransferenciasDet_Lotes Set CantidadEnviada = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            sSql += string.Format(" Update DevolucionTransferenciasDet_Lotes_Ubicaciones Set CantidadEnviada = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (bImplementaCodificacion)
            {
                sSql += string.Format(" Update DevolucionTransferencias_UUID Set Status = 'C' " +
                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n ",
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            }

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CancelaTransferenciaEncabezado()");
            }

            return bRegresa;
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (txtFolioDevolucion.Text.Trim() == "" || txtFolioDevolucion.Text.Trim() == "*")
            {
                bRegresa = false;
                General.msjUser("Folio de Devolución inválido, verifique.");
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
            //Devoluciones.MostrarPantalla((sTipoTransferencia + txtFolioEntrada.Text.Trim()), tpDevolucion, (int)TipoDeVenta.Ninguna);
        }
        #endregion Botones

        #region Validacion de datos 
        private bool validarDatos()
        {
            bool bRegresa = true;

            //if (bRegresa && txtFolioEntrada.Text.Trim() == "")
            //{
            //    bRegresa = false;
            //    General.msjUser("Folio de movimiento inválido, verifique.");
            //    txtFolioEntrada.Focus();
            //}

            if (bRegresa && cboMotivosDev.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado el motivo de devolución, verifique.");
                cboMotivosDev.Focus();
            }

            if (bRegresa && sObservaciones == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones para el movimiento de inventario, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tienen permisos para realizar la salida por devolucion de transferencia, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("Salida_Devolucion_Transferencia", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("Salida_Devolucion_Transferencia", sMsjNoEncontrado);
            }

            if (bRegresa && !motivodev.Marco)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado motivos de Devolución, verifique.");
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductosManual();
            }            

            return bRegresa;
        }

        private bool validarDatosCancelacion()
        {
            bool bRegresa = true;

            //if (bRegresa && (txtFolioEntrada.Text.Trim() == "*" || txtFolioEntrada.Text.Trim() == ""))
            //{
            //    bRegresa = false;
            //    General.msjUser("Folio de Inventario inválido, verifique.");
            //}

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

            int iCantidad = 0;

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
                    //if (Lotes.CantidadTotal == 0)
                    //{
                    //    bRegresa = false;
                    //}
                    //else
                    //{
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            iCantidad = myGrid.GetValueInt(i, (int)Cols.CantidadDevuelta);

                            if (myGrid.GetValue(i, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(i, (int)Cols.CantidadDevuelta) == 0) 
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    //}
                }
            }

            if ( !bRegresa )
                General.msjUser("Debe capturar al menos un producto para el inventario\n y/o capturar cantidades para al menos un lote, verifique.");

            return bRegresa;
        }

        private bool validarCapturaProductosManual()
        {
            bool bRegresa = true;

            int iCantidad = 0;

            if (myGridManual.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                //if (myGridManual.GetValue(1, (int)ColsManual.Descripcion) == "")
                //{
                //    bRegresa = false;
                //}
                //else
                //{
                    //if (Lotes.CantidadTotal == 0)
                    //{
                    //    bRegresa = false;
                    //}
                    //else
                    //{
                    for (int i = 1; i <= myGridManual.Rows; i++)
                    {
                        iCantidad = myGridManual.GetValueInt(i, (int)ColsManual.Cantidad);

                        if (myGridManual.GetValue(i, (int)ColsManual.ClaveSSA) != "" && myGridManual.GetValueInt(i, (int)ColsManual.Cantidad) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                    //}
                //}
            }

            if (!bRegresa)
                General.msjUser("Debe capturar cantidades, verifique.");

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
        public void ImprimirInventario()
        {
            bool bRegresa = false; 
            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirInventario()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_DevolucionesDeTransferencias.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("Folio", ( sIdTipoMovtoInv + txtFolioDevolucion.Text));

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa)
                //{
                //    LimpiarPantalla(false);
                //}
                //else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion de informacion

        #region Grabar informacion

        private bool AfectarExistenciaEnTransitoDevolucion(int TipoFactor)
        {
            string sSql = string.Format("Exec spp_INV_AplicaDesaplicaExistenciaTransitoDevoluciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', @TipoFactor = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia, TipoFactor);

            bool bRegresa = leerTrans.Exec(sSql);
            return bRegresa;
        }

        private bool GrabarDetalleEnvioTransferencia()
        {
            string sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasEnvioGenerar \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            return leerTrans.Exec(sSql);
        }


        private bool Grabar_Motivos_Devolucion()
        {
            bool bRegresa = true;
            string sTipoMov = "", sMotivo = "";
            int iMarca = 0;

            leerTrans.DataSetClase = motivodev.Retorno;

            while (leerTrans.Leer())
            {
                iMarca = 0;

                iMarca = leerTrans.CampoInt("Marca");
                sTipoMov = leerTrans.Campo("IdMovto");
                sMotivo = leerTrans.Campo("IdMotivo");

                if (iMarca == 1)
                {
                    string sSql = string.Format(" Exec spp_Mtto_MovtosInv_Adt_DevolucionesTransferencia \n" + 
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @IdMotivo = '{5}' ",
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia, sTipoMov, sMotivo);

                    if (!leerTrans.Exec(sSql))
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

        }


        public void CargarTransferencia(string IdEstadoOrigen, string idFarmaciaOrigen, string FarmaciaOrigen, string FolioOrigen, bool bMostrarForma)
        {
            cboEstados.Data = IdEstadoOrigen;
            cboEstados.Enabled = false;
            txtFarmaciaOrigen.Text = idFarmaciaOrigen;
            lblFarmaciaOrigen.Text = FarmaciaOrigen;

            txtFolioOrigen.Text = FolioOrigen;

            CargarTransferencia();


            //CargarDetallesTransferencia();

            if (txtFolioDevolucion.Text == "")
            {
                txtFolioDevolucion.Text = "*";
                txtFolioDevolucion.Enabled = false;

                IniciarToolBar(true, false, false, true, false);

                cboMotivosDev.Focus();

                bModificarCantidades = true;
            }

            if (bMostrarForma)
            {
                this.ShowDialog();
            }
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
                General.msjError("Ocurrió un error al obtener los motivos de devolución..");
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
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = bCapturaDeLotes;  //  chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = bModificarCantidades;  

                    // Solo para Movientos Especiales 
                    Lotes.PermitirSalidaCaducados = bPermitirSacarCaducados;
                    
                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucionDeCompras;

                    // Aplica solo para las Entrada por Consignacion    
                    Lotes.TipoDeInventario = tipoDeInventario; 

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.CantidadDevuelta, Lotes.Cantidad);
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
                        iEntrepaño = leerUBI.CampoInt("IdEntrepaño");
                    }
                }
            }

        }
        #endregion Llena_Ubicacion_Estadar

        #region Cargar_TE
        private void txtFolioEntrada_Validating(object sender, CancelEventArgs e)
        {
            //if (txtFolioEntrada.Text.Trim() != "" && txtFolioEntrada.Enabled)
            //{
            //    e.Cancel = !CargarTransferencia();
            //}
        }

        private bool CargarTransferencia()
        {
            bool bRegresa = true;
            string sStatus = "";
            string sMotivo = "";
            string sFolioOrigen = "";
            string sFolioDevolucion = "";
            string sObservaciones = "";

            //lblFolioEntrada.Visible = false;
            leer.DataSetClase = query.FolioTransferenciaDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, sTipoTransferencia, txtFolioDevolucion.Text, "CargarTransferencia");
            if (!leer.Leer())
            {
                bRegresa = false;
            }
            else
            {
                txtFarmaciaOrigen.Enabled = false;
                txtFolioOrigen.Enabled = false;
                //txtFolioEntrada.Enabled = false;
                bModificarCantidades = true;
                IniciarToolBar(false, false, false, true, false);

                dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");
                //txtFolioEntrada.Text = leer.Campo("Folio").Substring(2);
                cboEstados.Data = leer.Campo("IdEstadoRecibe");
                txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
                lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");

                //sFolioOrigen = leer.Campo("FolioTransferenciaRef");

                //if (sFolioOrigen != "")
                //{
                //    txtFolioOrigen.Text = sFolioOrigen.Substring(2);
                //}

                bPermitirSacarCaducados = leer.CampoBool("EsAlmacenRecibe");
                sMotivo = leer.Campo("IdMotivo");

                if (sMotivo != "")
                {
                    cboMotivosDev.Data = sMotivo;
                    cboMotivosDev.Enabled = false;
                }

                sFolioDevolucion = leer.Campo("FolioDevolucion");

                if (sFolioDevolucion != "")
                {
                    txtFolioDevolucion.Text = sFolioDevolucion.Substring(3);
                    txtFolioDevolucion.Enabled = false;
                }

                sObservaciones = leer.Campo("Observaciones");

                if (sObservaciones != "")
                {
                    txtObservaciones.Text = sObservaciones;
                    txtObservaciones.Enabled = false;
                }
                sStatus = leer.Campo("Status").ToUpper();
                //btnEjecutar.Enabled = ExistenDev_Parciales();


                CargarDetallesTransferencia();


                if (txtFolioDevolucion.Text == "*")
                {
                    txtFolioDevolucion.Focus();
                }
                else
                {
                    IniciarToolBar(false, false, true, false, true);
                }
            }

            return bRegresa;
        }

        private void CargarDetallesTransferencia()
        {
            myGrid.Limpiar(false);
            leer.DataSetClase = query.FolioTransferenciaDetallesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, sTipoTransferencia, txtFolioDevolucion.Text, "CargarDetallesTransferencia");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                myGridManual.LlenarGrid(leer.Tabla(2));
                CargarDetallesLotesTransferencia();
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            Totalizar();
        }

        private void CargarDetallesLotesTransferencia()
        {
            //Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
            //Lotes.ModificarCantidades = true;
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaDetallesLotesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, sTipoTransferencia, txtFolioDevolucion.Text, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                //string sFolio = sTipoTransferencia + Fg.PonCeros(txtFolioEntrada.Text, 8);
                leer.DataSetClase = query.FolioTransferenciaDetallesLotes_UbicacionesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, sTipoTransferencia, txtFolioDevolucion.Text, iPasillo, iEstante, iEntrepaño , "CargarDetallesTransferencia");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }

        }

        private bool ExistenDev_Parciales()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format(" Select * From DevolucionTransferenciasEnc (Nolock) " +
	                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioTransferencia = '{3}' ",
                     sEmpresa, sEstado, sFarmacia, (sTipoTransferencia + txtFarmaciaOrigen.Text));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ExistenDev_Parciales()");
                General.msjError("Ocurrió un error al buscar devoluciones parciales");
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    General.msjUser(" El Folio ya cuenta con devoluciones parciales... ");
                }
            }

            return bRegresa;
        }
        #endregion Cargar_TE

        #region Evento_Folio_Devolucion
        private void txtFolioDevolucion_Validating(object sender, CancelEventArgs e)
        {
            //    string sFolioTransferencia = "", sFolioTransferenciaRef = "", sFolioDevolucion = "";
            //    if (txtFolioDevolucion.Text.Trim() == "" || txtFolioDevolucion.Text.Trim() == "*" ) 
            //    {
            //        if (txtFarmaciaOrigen.Text != "")
            //        {
            //            txtFolioDevolucion.Enabled = false;
            //            txtFolioDevolucion.Text = "*";
            //        }
            //    }
            //    else
            //    {

            //        //sSql = string.Format("SELECT * FROM VentasEnc (nolock) WHERE FolioVenta= '{0}' AND IdEstado='{1}' AND IdFarmacia='{2}'  ", Fg.PonCeros(txtFolio.Text, 8), Fg.PonCeros(sEstado,2), Fg.PonCeros(sFarmacia,4)); 
            //        leer.DataSetClase = query.FolioTransferenciaDetallesDev(sEmpresa, sEstado, sFarmacia, cboEstados.Data, txtFarmaciaOrigen.Text, txtFolioOrigen.Text, "EDT", txtFolioDevolucion.Text, "txtFolioDevolucion_Validating");
            //        if (leer.Leer())
            //        {
            //            try
            //            {
            //                txtFarmaciaOrigen.Enabled = false;
            //                txtFolioOrigen.Enabled = false;
            //                //txtFolioEntrada.Enabled = false;
            //                bModificarCantidades = true;
            //                txtObservaciones.Enabled = false;
            //                cboMotivosDev.Enabled = false;
            //                txtFolioDevolucion.Enabled = false;
            //                IniciarToolBar(true, false, false, true, false);


            //                dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");

            //                sFolioTransferencia = leer.Campo("FolioTransferencia");

            //                if (sFolioTransferencia != "")
            //                {
            //                    txtFarmaciaOrigen.Text = sFolioTransferencia.Substring(2);
            //                }
            //                cboEstados.Data = leer.Campo("IdEstadoRecibe");
            //                txtFarmaciaOrigen.Text = leer.Campo("IdFarmaciaRecibe");
            //                lblFarmaciaOrigen.Text = leer.Campo("FarmaciaRecibe");

            //                sFolioTransferenciaRef = leer.Campo("FolioTransferenciaRef");
            //                if (sFolioTransferenciaRef != "")
            //                {
            //                    txtFolioOrigen.Text = sFolioTransferenciaRef.Substring(3);
            //                }

            //                sFolioDevolucion = leer.Campo("FolioDevolucion");
            //                if (sFolioDevolucion != "")
            //                {
            //                    txtFolioDevolucion.Text = sFolioDevolucion.Substring(3);
            //                }
            //                cboMotivosDev.Data = leer.Campo("IdMotivo");
            //                txtObservaciones.Text = leer.Campo("Observaciones");

            //                if (txtFolioDevolucion.Text != "")
            //                {
            //                    btnGuardar.Enabled = false;
            //                }

            //                //txtObservaciones.Text = leer.Campo("Observaciones");
            //                //txtObservaciones.Enabled = false;
            //                //sStatus = "Registrada";
            //                clsLeer leerTemp = new clsLeer();
            //                leerTemp.DataTableClase = leer.Tabla(2);

            //                myGrid.LlenarGrid(leerTemp.DataSetClase);

            //                myGrid.AnchoColumna(Cols.Cantidad, 0);


            //                btnEjecutar.Enabled = ExistenDev_Parciales();
            //                btnGenerarPaqueteDeDatos.Enabled = true;


            //            }
            //            catch
            //            ( Exception ex )
            //            {
            //                Error.GrabarError(ex, "txtFolioDevolucion_Validating()");
            //                General.msjError("Error al cargar encabezado.");
            //            }


            //            //CargarDetallesTransferencia();

            //            //sFolio = leer.Campo("FolioTransferencia");



            //        }
            //        else
            //        {
            //            txtFolioDevolucion.Text = "";
            //            txtFolioDevolucion.Focus();
            //        }

            //    }

        }
        #endregion Evento_Folio_Devolucion

        #region Guardar_Informacion_Devolucion
        private bool GrabarEncabezadoDevolucion( string sFolioEntrada )
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasEnc \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', \n" +
                "\t@FolioTransferencia = '{4}', @FolioTransferenciaRef = '{5}', @FolioMovtoInv = '{6}', @TipoTransferencia = '{7}', @IdPersonal = '{8}', \n" +
                "\t@Observaciones = '{9}', @IdMotivo = '{10}', @SubTotal = '{11}', @Iva = '{12}', @Total = '{13}', @IdEstadoRecibe = '{14}', @IdFarmaciaDestino = '{15}' \n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioDevolucion, sFolioEntrada, ("TS" + sFolioOrigen), "", sIdTipoMovtoInv,
                DtGeneral.IdPersonal, sObservaciones, cboMotivosDev.Data,

                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text),
                SEstadoOrigen, sFarmaciaOrigen
                 
                );

            if (!leerTrans.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leerTrans.Leer();
                sFolioTransferencia = leerTrans.Campo("Folio");
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
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia, 
                        sCodigoEAN, sClaveSSA, iCantidad);

                    if (!leerTrans.Exec(sSql))
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
            int iCantidad = 0, iCant_Dev = 0;
            double nCosto = 0, nTasaIva = 0, nSubTotal = 0, nImporteIva = 0, nImporte = 0;
            int iUnidadDeSalida = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                nTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCant_Dev = myGrid.GetValueInt(i, (int)Cols.CantidadDevuelta);
                nCosto = myGrid.GetValueDou(i, (int)Cols.Costo);

                nSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                nImporteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                nImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                if (sIdProducto != "" && iCant_Dev > 0)
                {                   

                    sSql = string.Format("Exec spp_Mtto_DevolucionTransferenciasDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioDevolucion = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @Cant_Enviada = '{7}', @Cant_Devuelta = '{8}',\n" +
                        "\t@CantidadEnviada = '{9}', @CostoUnitario = '{10}', @TasaIva = '{11}', @SubTotal = '{12}', @ImpteIva = '{13}',\n" +
                        "\t@Importe = '{14}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia,
                        sIdProducto, sCodigoEAN, i, iCant_Dev, 0, iCant_Dev, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);
                    if (!leerTrans.Exec(sSql))
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
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioTransferencia, IdProducto, CodigoEAN, Renglon, L.Cantidad, 0, L.Cantidad, L.IdSubFarmacia,
                        L.ClaveLote, L.EsConsignacion, L.SKU);
                    if (!leerTrans.Exec(sSql))
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
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioTransferencia, L.IdProducto, L.CodigoEAN, Renglon, L.Cantidad, 0, L.Cantidad, 
                        L.IdSubFarmacia, L.ClaveLote, Lote.EsConsignacion, L.Pasillo, L.Estante, L.Entrepano, L.SKU);

                    bRegresa = leerTrans.Exec(sSql);
                    if (!bRegresa)
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

        #region Actualiza_Status_Transferencia
        private void ActualizaStatusTransferencia()
        {
            string sSql = string.Format(" Update DevolucionTransferenciasEnvioEnc Set Status = 'T' " +
                          " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                          sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizaStatusTransferencia()");
                General.msjError("Ocurrió un error al actualizar el status de la Transferencia");
            }
        }
        #endregion Actualiza_Status_Transferencia

        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {
            GenerarPaqueteDeDatos();
        }

        private void toolStripBarraMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void grdProductosManual_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGridManual.GetValue(myGridManual.ActiveRow, (int)ColsManual.ClaveSSA);
        }

        private void grdProductosManual_EditModeOff(object sender, EventArgs e)
        {
            ColActivaManual = (ColsManual)myGridManual.ActiveCol;
            bool bEsEAN_Unico = true;

            switch (ColActivaManual)
            {
                case ColsManual.ClaveSSA:
                    string sValor = myGridManual.GetValue(myGridManual.ActiveRow, (int)ColsManual.ClaveSSA);
                    if (sValor != "")
                    {
                        leer.DataSetClase = query.ClavesSSA_Sales(sValor, true, "grdProductosManual_EditModeOff");
                        if (leer.Leer())
                        {
  
                        
                            sValor = leer.Campo("DescripcionSal");
                            myGridManual.SetValue(myGridManual.ActiveRow, ColsManual.Descripcion, sValor);

                            sValor = leer.Campo("Presentacion");
                            myGridManual.SetValue(myGridManual.ActiveRow, ColsManual.Presentacion, sValor);


                        }
                        else
                        {
                            myGridManual.LimpiarRenglon(myGridManual.ActiveRow);
                            myGridManual.SetActiveCell(myGridManual.ActiveRow, (int)Cols.CodEAN);
                        }
                    }
                    else
                    {
                        myGridManual.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    break;
            }
            Totalizar();
        }

        private void grdProductosManual_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (txtFolioDevolucion.Text == "*")
            {
                if (!bEstaCancelado)
                {
                    if ((myGridManual.ActiveRow == myGridManual.Rows) && e.AdvanceNext)
                    {
                        if (myGridManual.GetValue(myGridManual.ActiveRow, (int)ColsManual.ClaveSSA) != "" && myGridManual.GetValueInt(myGridManual.ActiveRow, (int)ColsManual.Cantidad) >= 1)
                        {
                            myGridManual.Rows = myGridManual.Rows + 1;
                            myGridManual.ActiveRow = myGridManual.Rows;
                            myGridManual.SetActiveCell(myGridManual.Rows, 1);
                        }
                    }
                }
            }
        }

        private void grdProductosManual_KeyDown(object sender, KeyEventArgs e)
        {
            string sValor = "";
            ColActivaManual = (ColsManual)myGridManual.ActiveCol;
            switch (ColActivaManual)
            {


                case ColsManual.CodEAN:
                case ColsManual.Descripcion:
                case ColsManual.Cantidad:
                case ColsManual.ClaveSSA:
                case ColsManual.Presentacion:
                    //if (chkAplicarInv.Enabled)
                    {
                        if (e.KeyCode == Keys.F1)
                        {
                            ////sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            ////leer.DataSetClase = ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "grdProductos_KeyDown");
                            ////////////if (leer.Leer())
                            ////////////{
                            ////////////    CargarDatosProducto();
                            ////////////}
                            ////if (leer.Leer())
                            ////{
                            ////    sValorGrid = leer.Campo("CodigoEAN");
                            ////    ObtenerInformacion(sValorGrid);
                            ////}

                            leer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductosManual_EditModeOff");
                            if (leer.Leer())
                            {
                                sValor = leer.Campo("ClaveSSA");
                                myGridManual.SetValue(myGridManual.ActiveRow, ColsManual.ClaveSSA, sValor);

                                sValor = leer.Campo("DescripcionSal");
                                myGridManual.SetValue(myGridManual.ActiveRow, ColsManual.Descripcion, sValor);

                                sValor = leer.Campo("Presentacion");
                                myGridManual.SetValue(myGridManual.ActiveRow, ColsManual.Presentacion, sValor);


                            }
                            else
                            {
                                myGridManual.LimpiarRenglon(myGridManual.ActiveRow);
                                myGridManual.SetActiveCell(myGridManual.ActiveRow, (int)Cols.CodEAN);
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            myGridManual.LimpiarRenglon(myGridManual.ActiveRow);
                        }

                    }
                    break;
            }
        }

        private void GenerarPaqueteDeDatos()
        {
            string sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", sFolioTransferencia);
            sMsj = string.Format("¿ Desea generar el paquete de datos para la Transferencia {0} ?", txtFolioDevolucion.Text);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (PreparaTransferenciaReenvio())
                {
                    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                    ClienteTransferencias.TransferenciasAutomaticas(cboEstados.Data, Fg.PonCeros(txtFarmaciaOrigen.Text, 4), true);
                    ActualizaStatusTransferencia();
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
            string sFolioRev = "SDT" + txtFolioDevolucion.Text;

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
    }
}
