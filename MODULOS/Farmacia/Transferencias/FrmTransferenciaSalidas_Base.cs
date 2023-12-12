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
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Criptografia;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;

//using Dll_IMach4;
//using Dll_IMach4.Interface;

using DllRobotDispensador; 

using DllTransferenciaSoft.ObtenerInformacion;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;

namespace Farmacia.Transferencias
{
    public partial class FrmTransferenciaSalidas_Base : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            EsIMach4 = 11
        }

        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        clsClavesSolicitadasTransferencia InfCveSolicitadas;
        string sFolioSolicitud = "";
        string sFoliosSurtido = "";
        string sFolioPedido = "";
        int iRegistros = 0;
        int iCantidadMinima_Avance = 0;// RobotDispensador.Robot.EsClienteInterface ? 0 : 1;
        bool bEsClienteInterface = false;// RobotDispensador.Robot.EsClienteInterface;

        bool bCapturaDeClavesSolicitadasHabilitada = false; //(GnFarmacia.CapturaDeClavesSolicitadasHabilitada && DtGeneral.EsAlmacen) ? true : false;

        clsConexionSQL cnn; // = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerClaves;
        DllFarmaciaSoft.clsConsultas query;
        DllFarmaciaSoft.clsAyudas ayuda;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsVerificarSalidaLotes VerificarLotes;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;

        // Se agrego para la verificacion de la Transferencia en el Destino
        clsLeerWebExt leerWeb;
        clsDatosConexion DatosDeConexion;

        string sFolioTransferencia = "";
        string sMensajeGrabar = "";
        string sMsjNoEncontrado = "";
        string sMensajeErrorGrabar = "";

        // bool bMovtoCancelado = false;
        bool bEstaCancelado = false;
        // bool bExisteMovto = false;
        // bool bMovtoAplicado = false;
        bool bDestinoEsAlmacen = false;
        bool bContinua = true;
        bool bEsSurtimientoPedido = false;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        bool bImplementaCodificacion = false; //GnFarmacia.ImplementaCodificacion_DM;
        string sEmpresa = ""; //DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = 0; //Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = ""; //DtGeneral.EstadoConectado;
        string sFarmacia = ""; //DtGeneral.FarmaciaConectada;
        string sCveRenapo = ""; //DtGeneral.ClaveRENAPO;
        string sIdTipoMovtoInv = "TS";
        string sTipoES = "S";
        string sIdProGrid = "";
        // int iAnchoColPrecio = 0;
        string sObservaciones = "";
        string sReferencia = "";
        string sIdSubFarmacia_Traspasos = ""; //GnFarmacia.SubFarmaciaVenta_Traspasos_Estados;
        TiposDeInventario tpInventario___TS_InterEstatal = TiposDeInventario.Todos; // GnFarmacia.TipoInventario___TS_InterEstatal;

        string sIdPersonalConectado = ""; //DtGeneral.IdPersonal;

        string sUrlFarmacia = "";
        string sHost = "";

        bool bEsIdProducto_Ctrl = false;
        bool bFolioGuardado = false;
        bool bEsCancelacionDeTransferencia = false;
        bool bTienePermitidasTransferenciasNormales = true;

        // string sFormato = "#,###,###,##0.###0";

        // Manejo automatico de Transferencias 
        DllTransferenciaSoft.ObtenerInformacion.clsCliente ClienteTransferencias; // = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

        bool bGuardadoDeInformacion_Masivo = true; 
        string sSql_Detallado = ""; 

        bool TransferenciaAplicada = false;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        TipoDeTransferencia tpTipoDeTransferencia = TipoDeTransferencia.Ninguno;
        bool bMostrarSoloAlmacenes = false;
        bool bSoloControlados = false;
        bool bTransferenciaInterestatal = false;
        int iEsTransferenciaAlmacen = 0;
        bool bFarmaciaDestino_ManejaControlados = false;
        bool bEsDestinoAlmacen = false;

        clsCriptografo crypto; // = new clsCriptografo();

        bool bTieneSurtimientosActivos = false;
        string sMensajeConSurtimiento = "Se encontrarón folios de surtimiento pendientes de generar Traslados ó Dispersiones.\n\n" +
                "No es posible generar la Salida de Traslado, verifique el status de los folios de surtimiento.";

        public FrmTransferenciaSalidas_Base() : this(TipoDeTransferencia.Farmacia_Normal)
        {
        }

        public FrmTransferenciaSalidas_Base(TipoDeTransferencia TipoTransferencia)
        {
            InitializeComponent();

            cnn = new clsConexionSQL(General.DatosConexion); 
            tpTipoDeTransferencia = TipoTransferencia;

            lblMensaje_03.BackColor = lblMensajes.BackColor;

            ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);

            crypto = new clsCriptografo(); 
            iCantidadMinima_Avance = RobotDispensador.Robot.EsClienteInterface ? 0 : 1;
            bEsClienteInterface = RobotDispensador.Robot.EsClienteInterface;
            bCapturaDeClavesSolicitadasHabilitada = (GnFarmacia.CapturaDeClavesSolicitadasHabilitada && DtGeneral.EsAlmacen) ? true : false;

            bImplementaCodificacion = GnFarmacia.ImplementaCodificacion_DM;
            sEmpresa = DtGeneral.EmpresaConectada;
            iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;
            sCveRenapo = DtGeneral.ClaveRENAPO;
            sIdTipoMovtoInv = "TS";
            sTipoES = "S";
            sIdProGrid = "";
            sObservaciones = "";
            sReferencia = "";
            sIdSubFarmacia_Traspasos = GnFarmacia.SubFarmaciaVenta_Traspasos_Estados;
            tpInventario___TS_InterEstatal = GnFarmacia.TipoInventario___TS_InterEstatal;

            sIdPersonalConectado = DtGeneral.IdPersonal;



            if (tpTipoDeTransferencia == TipoDeTransferencia.Almacen_Normal)
            {
                iEsTransferenciaAlmacen = 1;
                bMostrarSoloAlmacenes = false;
                bTransferenciaInterestatal = true;
            }

            if (tpTipoDeTransferencia == TipoDeTransferencia.Almacen_Controlados)
            {
                iEsTransferenciaAlmacen = 1;
                bMostrarSoloAlmacenes = false;
                bTransferenciaInterestatal = true;
                bSoloControlados = true;
            }

            if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Controlados)
            {
                bMostrarSoloAlmacenes = false;   //// QUITAR 20220811.1445
                bTransferenciaInterestatal = true;
                bSoloControlados = true;
            }


            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);

            leer = new clsLeer(ref cnn);
            leerClaves = new clsLeer(ref cnn);

            query = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;

            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Costo, (int)Cols.Importe, (int)Cols.Descripcion);
            myGrid.AjustarAnchoColumnasAutomatico = true;


            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sIdPersonalConectado);

            //// Control de Acceso para lectura de Codigos DataMatrix 
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
                "Select Distinct IdEstado, (IdEstado + ' - ' + Estado) as Estado \n" +
                "From vw_EmpresasFarmacias \n " +
                "Where IdEmpresa = '{0}' and Status = 'A' and StatusRelacion = 'A' and  IdTipoUnidad not in ( 0, 5 ) and IdEstado Not In ( '{1}' )   \n " +
                " Order by IdEstado ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);


            if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Normal || tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Controlados)
            {
                sSql = string.Format(
                "Select Distinct IdEstado, (IdEstado + ' - ' + Estado) as Estado \n" +
                "From vw_EmpresasFarmacias \n " +
                "Where IdEmpresa = '{0}' and Status = 'A' and StatusRelacion = 'A' and  IdTipoUnidad not in ( 0, 5 ) \n " +
                " Order by IdEstado ", DtGeneral.EmpresaConectada);
            }


            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstadosFiliales()");
                General.msjError("Ocurrió un error al obtener la lista de Estados filiales.");
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
            Point e = new Point(lblTitulo__Folio.Location.X, lblTitulo__Folio.Location.Y);
            Point c = new Point(txtFolio.Location.X, txtFolio.Location.Y);
            int iAnchoCombo = (lblFarmaciaDestino.Left - txtFarmaciaDestino.Left)+ lblFarmaciaDestino.Width;

            lblTitulo__Estado.Visible = false;
            cboEstados.Visible = false;

            //if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo.Farmacia_MA || DtGeneral.ModuloMA_EnEjecucion == TipoModulo.Almacen_MA)
            if (GnFarmacia.Transferencias_Interestatales__Farmacias)
            {
                this.Height += iIncremento;
                ////this.FrameInformacionRegistro.Top += iIncremento;
                this.FrameDetallesTransferencia.Top += iIncremento;
                this.FrameDetallesTransferencia.Height -= iIncremento;
                this.FrameDatosGenerales.Height += iIncremento;

                foreach(Control control in FrameDatosGenerales.Controls)
                {
                    if(control.Name != lblTitulo__FechaRegistro.Name && control.Name != dtpFechaRegistro.Name)
                    {
                        control.Top += iIncremento;
                    }
                }


                lblTitulo__Estado.Location = e;
                cboEstados.Location = c;
                cboEstados.Width = iAnchoCombo - 300;
                lblTitulo__Estado.Visible = true;
                cboEstados.Visible = true;

            }

            //////// Configurar titulo de la pantalla 
            if (tpTipoDeTransferencia == TipoDeTransferencia.Almacen_Normal)
            {
                this.Text = "Registro de Salida por Transferencia Interestatal ";
                this.Name = "FrmEdoTraspasosSalidas";
            }

            if (tpTipoDeTransferencia == TipoDeTransferencia.Almacen_Controlados)
            {
                this.Text = "Registro de Salida por Transferencia Interestatal de Controlados ";
                this.Name = "FrmEdoTraspasosSalidas_Controlados";
            }

            if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Normal)
            {
                //this.Text = "Registro de Salida por Transferencia de Controlados";
                this.Name = "FrmTransferenciaSalidas";
            }

            if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Controlados)
            {
                this.Text = "Registro de Salida por Transferencia de Controlados";
                this.Name = "FrmTransferenciaSalidas_Controlados";
            }

            //////// Configurar titulo de la pantalla 

        }
        #endregion Interface MA

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

            if (bExito)
            {
                linkUrlFarmacia.Text = "";

                bFarmaciaDestino_ManejaControlados = false;
                bDestinoEsAlmacen = false;
                lblCancelado.Visible = false;
                sFolioSolicitud = "";
                sFoliosSurtido = "";

                lblFarmaciaDestino.Text = "";

                // Inicializar el manejo de Lotes 
                Lotes = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                Lotes.ManejoLotes = OrigenManejoLotes.Transferencias;
                InfCveSolicitadas = new clsClavesSolicitadasTransferencia(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

                bFolioGuardado = false;
                btnCodificacion.Enabled = false;
                IniciarToolBar(false, false, false, false);
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
                chk_RPT_Cajas.Enabled = false;
                chk_RPT_Cajas.Visible = false;
                chk_RPT_EtiquetasCajas.Enabled = false;
                chk_RPT_EtiquetasCajas.Visible = false;

                chkDesglosado.Visible = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;

                cboEstados.Data = DtGeneral.EstadoConectado;
                if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
                {
                    cboEstados.Focus();
                }
                else
                {
                    txtFolio.Focus();
                }

                bEsCancelacionDeTransferencia = false;
                sReferencia = "";
                sIdTipoMovtoInv = "TS";
                sUrlFarmacia = "";
                sHost = "";
                TransferenciaAplicada = false;

                if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Normal || tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Controlados)
                {
                    cboEstados.Enabled = false;
                }

                if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
                {
                    cboEstados.Enabled = true;
                }


                if (tpTipoDeTransferencia == TipoDeTransferencia.Farmacia_Controlados && GnFarmacia.Transferencias_Interestatales__Farmacias)
                {
                    cboEstados.SelectedIndex = 0;
                    cboEstados.Enabled = true;
                }
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false, false);
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool GenerarPaquete)
        {
            btnGuardar.Enabled = bTieneSurtimientosActivos ? false : Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;

            // GenerarPaquete = !(GnFarmacia.EsServidorDeRedLocal || DtGeneral.EsAdministrador) ? true : GenerarPaquete; 
            btnGenerarPaqueteDeDatos.Enabled = GenerarPaquete;
            ////if (GnFarmacia.EsServidorDeRedLocal || DtGeneral.EsAdministrador || DtGeneral.EsAlmacen)
            ////{
            ////    btnGenerarPaqueteDeDatos.Enabled = GenerarPaquete; 
            ////}

            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = DtGeneral.ExistenTransferencias_EnTransito__AplicacionExpirada ? false : Guardar;
            }

            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = bTienePermitidasTransferenciasNormales;
            }
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
            bool bBtnTransferencia = btnGenerarPaqueteDeDatos.Enabled;
            sMensajeErrorGrabar = "Error al guardar Traspaso.";


            bEsCancelacionDeTransferencia = false;
            sIdTipoMovtoInv = "TS";
            sTipoES = "S";

            if (validarDatos(false))
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

                    // ya no se grabara el movto de inventario automaticamente, 2k130205  Fernando Aragón Valerio
                    // Generar el Movimiento de Inventario 
                    //if (GrabarMovtoEncabezado())
                    {
                        // Generar la transferencia de salida 
                        if (GrabarTransferenciaEncabezado())
                        {
                            bExito = true;

                            if (bImplementaCodificacion)
                            {
                                if (!Guardar_UUIDS(true, true))
                                {
                                    bExito = false;
                                }
                            }

                            if (bExito)
                            {
                                //// Generar la informacion de la transferencia de entrada
                                bExito = GrabarDetalleEnvioTransferencia();

                                if (bExito && bEsSurtimientoPedido)
                                {

                                    if (bExito)
                                    {
                                        bExito = RevisarPedidoCompleto();
                                    }

                                    if (bExito)
                                    {
                                        bExito = ActualizarEstatusPedido();
                                    }

                                    if (bExito)
                                    {
                                        bExito = RegistrarAtencion();
                                    }

                                    if (bExito)
                                    {
                                        bExito = AfectarExistenciaSurtidos();
                                    }
                                }

                                if (bExito)
                                {
                                    if (GuardarClavesSolicitadas())
                                    {
                                        // ya no se afectara la existencia de los productos, hasta que la transferencia sea aplicada, esto se hara en otra pantalla
                                        //bExito = AfectarExistencia(true, false);
                                        bExito = AfectarExistenciaEnTransito(1);
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
                        General.msjError(sMensajeErrorGrabar);
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnTransferencia);
                    }
                    else
                    {
                        cnn.CompletarTransaccion();

                        // IMach  // Enlazar el folio de inventario 
                        RobotDispensador.Robot.TerminarSolicitud(sFolioMovto);

                        General.msjUser(sMensajeGrabar);
                        // EnvioAutomaticoDeTransferencias(); 
                        IniciarToolBar(false, false, true, true);
                        ImprimirInformacion();

                        if(bEsSurtimientoPedido)
                        {
                            this.Hide();  
                        }
                    }

                    cnn.Cerrar();
                }
            }
        }

        private bool ActualizarEstatusPedido()
        {
            string sSql = "";
            bool bRegresa = false;
            int iFolios = 0;

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Actualizar_Status \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @FolioTransferenciaReferencia = '{4}'",
                sEmpresa, sEstado, sFarmacia, sFoliosSurtido, sFolioTransferencia);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    iFolios = leer.CampoInt("Registros");

                    if (iFolios == iRegistros)
                    {
                        bRegresa = true;
                    }
                    else
                    {
                        sMensajeErrorGrabar = "El Surtido ya tiene una transferencia asignada.";
                    }
                }
            }

            return bRegresa;
        }

        private bool RevisarPedidoCompleto()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', @FolioPedido = '{4}' ",
                sEmpresa, sEstado, sFarmacia, txtFarmaciaDestino.Text, sFolioPedido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool RegistrarAtencion()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @IdPersonal = '{4}', @Observaciones = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFoliosSurtido, DtGeneral.IdPersonal, "");

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool AfectarExistenciaSurtidos()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @TipoFactor = 2, @Validacion_Especifica = 1  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFoliosSurtido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            bool bBtnTransferencia = btnGenerarPaqueteDeDatos.Enabled;

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
                            bExito = AfectarExistenciaEnTransito(2);
                        }

                        if (bExito)
                        {
                            if (bImplementaCodificacion && bExito)
                            {
                                ReactivaUUIDS();
                            }
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
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bBtnTransferencia);
                        }
                        else
                        {
                            cnn.CompletarTransaccion();
                            lblCancelado.Visible = true;

                            // IMach  // Enlazar el folio de inventario 
                            RobotDispensador.Robot.TerminarSolicitud(sFolioMovto);

                            General.msjUser(" La Transferencia de Salida se Cancelo Exitosamente...");
                            // EnvioAutomaticoDeTransferencias(); 
                            IniciarToolBar(false, false, true, false);
                            //Imprimir();
                        }

                        cnn.Cerrar();
                    }
                }
            }
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

            TipoReporteTransferencia TipoImpresion = !chkTipoImpresion.Checked ? TipoReporteTransferencia.Detallado : TipoReporteTransferencia.Ticket;

            string sFolio = "TS" + txtFolio.Text;


            if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";



                ClsImprimirTransferencias imprimir = new ClsImprimirTransferencias(cnn.DatosConexion, DatosCliente, "", false, TipoImpresion);

                bRegresa = imprimir.Imprimir(sFolio, chkDesglosado.Checked);

                //clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

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
                //myRpt.Add("Folio", "TS" + txtFolio.Text);

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
                    if (chk_RPT_Cajas.Checked)
                    {
                        ImprimirRptCajas();
                    }

                    if (chk_RPT_EtiquetasCajas.Checked && sFoliosSurtido != "")
                    {
                        ImprimirEtiquetas_Pedido();
                    }

                    if(!bEsSurtimientoPedido)
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

        private void ImprimirRptCajas()
        {
            DatosCliente.Funcion = "ImprimirRptCajas()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.TituloReporte = "Impresión de cajas de embarque";

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@Folioreferencia", sFolioTransferencia);
            myRpt.NombreReporte = "PtoVta_Caja_Embarque";

            bool bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte de cajas de embarque.");
            }
        }

        private void ImprimirEtiquetas_Pedido()
        {
            bool bRegresa = false;

            bRegresa = General.msjConfirmar("¿ Desea imprimir las etiquetas para las cajas de surtido ?") == System.Windows.Forms.DialogResult.Yes;

            if (bRegresa)
            {
                clsEtiquetasPedidosAlmacen etiquetas = new clsEtiquetasPedidosAlmacen();
                etiquetas.GenerarEtiquetaSurtido(this.MdiParent, sFoliosSurtido, true);
            }
        }
        #endregion Impresion

        #region Validacion de informacion
        private bool validarDatos( bool EsCancelacion )
        {
            bool bRegresa = true;

            if(!EsCancelacion)
            {
                if(DtGeneral.EsAlmacen && !bEsSurtimientoPedido)
                {
                    if(!bTransferenciaInterestatal)
                    {
                        if(DtGeneral.TieneSurtimientosActivos())
                        {
                            bRegresa = false;
                            General.msjAviso(sMensajeConSurtimiento);
                        }
                    }
                }
            }

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

            //////if (bRegresa && txtObservaciones.Text.Trim() == "")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("No ha capturado las observaciones para la transferencia, verifique.");
            //////    txtObservaciones.Focus();
            //////}

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
                    sReferencia = "TS" + txtFolio.Text;

                    clsObservaciones ob = new clsObservaciones();
                    ob.Encabezado = "Observaciones de Cancelación de Transferencia de Salida";
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
                sMsjNoEncontrado = "Usuario sin permisos para generar una salida de Traspaso. Favor de verificar.";
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
                General.msjUser("Capture algun producto en el Traspaso\n Capture cantidades en los lotes. Favor de verificar.");

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, Traspaso no puede ser completado.");
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

        #region Guardar datos tranferencia 
        private bool GrabarTransferenciaEncabezado()
        {
            bool bRegresa = false;
            string sFolio = "*";
            string sSql = "";

            sSql = string.Format("Exec spp_Mtto_TransferenciasEnc \n"+
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @CveRenapo = '{2}', @IdFarmacia = '{3}', \n" +
                "\t@IdAlmacen = '{4}', @EsTransferenciaAlmacen = '{5}', @FolioTransferencia = '{6}', @FolioMovtoInv = '{7}', @FolioMovtoInvRef = '{8}', \n" +
                "\t@FolioTransferenciaRef = '{9}', @TipoTransferencia = '{10}', @DestinoEsAlmacen = '{11}', @IdPersonal = '{12}', @Observaciones = '{13}', \n" +
                "\t@SubTotal = '{14}', @Iva = '{15}', @Total = '{16}', \n" +
                "\t@IdEstadoRecibe = '{17}', @CveRenapoRecibe = '{18}', @IdFarmaciaRecibe = '{19}', @IdAlmacenRecibe = '{20}' \n",
                sEmpresa, sEstado, sCveRenapo, sFarmacia, "00", iEsTransferenciaAlmacen, sFolio, sFolioMovto, "", "", sIdTipoMovtoInv,
                0, DtGeneral.IdPersonal, txtObservaciones.Text,
                General.GetFormatoNumerico_Double(txtSubTotal.Text),
                General.GetFormatoNumerico_Double(txtIva.Text),
                General.GetFormatoNumerico_Double(txtTotal.Text),
                cboEstados.Data, sCveRenapo, txtFarmaciaDestino.Text, "00");


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
            bool bRegresa = false;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "";
            int iCantidad = 0;
            double nCosto = 0, nTasaIva = 0, nSubTotal = 0, nImporteIva = 0, nImporte = 0;
            int iUnidadDeSalida = 0;

            double nImporteMinimo = 0.0001;

            sSql_Detallado = "";
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
                nCosto = General.Redondear(myGrid.GetValueDou(i, (int)Cols.Costo), 4, false);

                nSubTotal = General.Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), 2, false);
                nImporteIva = General.Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteIva), 2, false);
                nImporte = General.Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteTotal), 2, false);
                iUnidadDeSalida = myGrid.GetValueInt(i, (int)Cols.TipoCaptura);

                nImporteIva = nImporteIva < nImporteMinimo ? 0.0000 : nImporteIva;

                if (sIdProducto != "")
                {
                    sSql = string.Format("Exec spp_Mtto_TransferenciasDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @UnidadDeEntrada = '{7}', @Cant_Enviada = '{8}',\n" +
                        "\t@Cant_Devuelta = '{9}', @CantidadEnviada = '{10}', @CostoUnitario = '{11}', @TasaIva = '{12}', @SubTotal = '{13}',\n" +
                        "\t@ImpteIva = '{14}', @Importe = '{15}' \n", 
                        sEmpresa, sEstado, sFarmacia, sFolioTransferencia,
                        sIdProducto, sCodigoEAN, i, iUnidadDeSalida, iCantidad, 0, 0, nCosto, nTasaIva, nSubTotal, nImporteIva, nImporte);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);
                        GrabarTransferenciaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto);
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
                            bRegresa = GrabarTransferenciaDetalleLotes(i, sIdProducto, sCodigoEAN, nCosto);
                            if(!bRegresa)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //// Ejecucion concentrada 
            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = leer.Exec(sSql_Detallado);
            }

            return bRegresa;
        }

        private bool GrabarTransferenciaDetalleLotes(int Renglon, string IdProducto, string CodigoEAN, double Costo)
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
                       sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioTransferencia, IdProducto, CodigoEAN, L.ClaveLote, Renglon.ToString(), L.Cantidad.ToString(), L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n\n", sSql);
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarTransferenciaDetalleLotesUbicaciones(L, Renglon);
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
                                bRegresa = GrabarTransferenciaDetalleLotesUbicaciones(L, Renglon);
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

        private bool GrabarTransferenciaDetalleLotesUbicaciones(clsLotes Lote, int Renglon)
        {
            bool bRegresa = false;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

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
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioTransferencia,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, Renglon.ToString(),
                        L.Pasillo, L.Estante, L.Entrepano, L.Cantidad.ToString(), L.SKU);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n\n\n", sSql);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        bRegresa = leer.Exec(sSql);
                        if(!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;

        }
        #endregion Guardar datos tranferencia

        #region Guardar datos tranferencia auxiliar 
        private bool GrabarDetalleEnvioTransferencia()
        {
            string sSql = string.Format("Exec spp_Mtto_TransferenciasEnvioGenerar \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}' ",
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
            // string sObservaciones = txtObservaciones.Text.Trim(); 
            sFolioMovto = "";

            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, "*", sIdTipoMovtoInv, sTipoES, sReferencia,
                DtGeneral.IdPersonal, sObservaciones,
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
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarMovtoDetalleLotesUbicaciones(L);
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarMovtoDetalleLotesUbicaciones(clsLotes Lote)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A');

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
            bool bRegresa = true;

            bool bEsFarmaciaNormal = !DtGeneral.EsAlmacen && !GnFarmacia.EsUnidadUnidosis;
            bool bEsAlmacenNormal = DtGeneral.EsAlmacen && !GnFarmacia.EsUnidadUnidosis;
            bool bEsDestinoEsUnidosis = leer.CampoBool("EsUnidosis");
            bool bEsDestinoFarmaciaUnidosis = !leer.CampoBool("EsAlmacen") && leer.CampoBool("EsUnidosis");
            bool bEsDestinoAlmacenUnidosis = leer.CampoBool("EsAlmacen") && leer.CampoBool("EsUnidosis");

            bEsDestinoAlmacen = leer.CampoBool("EsAlmacen");

            bFarmaciaDestino_ManejaControlados = leer.CampoBool("ManejaControlados");

            if (leer.Campo("FarmaciaStatus").ToUpper() == "C")
            {
                General.msjUser("La Farmacia seleccionada esta cancelada.\nNo se puede generar el Traspaso.");
                txtFarmaciaDestino.Text = "";
                txtFarmaciaDestino.Focus();
                lblFarmaciaDestino.Text = "";
                bRegresa = false;
            }

            if(!leer.CampoBool("Transferencia_RecepcionHabilitada") && bRegresa)
            {
                General.msjUser("La Farmacia seleccionada no esta habilitada para recibir Traspasos.");
                txtFarmaciaDestino.Text = "";
                txtFarmaciaDestino.Focus();
                lblFarmaciaDestino.Text = "";
                bRegresa = false;
            }

            //if (GnFarmacia.EsUnidadUnidosis && !leer.CampoBool("EsUnidosis") && bRegresa)
            //{
            //    General.msjUser("La farmacia seleccionada no es unidosis, no es posible generar la transferencia.");
            //    txtFarmaciaDestino.Text = "";
            //    txtFarmaciaDestino.Focus();
            //    lblFarmaciaDestino.Text = "";
            //    bRegresa = false;
            //}

            if (
                (
                 (bEsFarmaciaNormal && bEsDestinoEsUnidosis) ||
                 (bEsAlmacenNormal && bEsDestinoFarmaciaUnidosis) ||
                 (GnFarmacia.EsUnidadUnidosis && !bEsDestinoEsUnidosis)
                )
                 && bRegresa)
            {
                if (!validarFarmaciaRelacionada())
                {
                    General.msjUser("La farmacia no es valida para Traspasos.");
                    txtFarmaciaDestino.Text = "";
                    txtFarmaciaDestino.Focus();
                    txtFarmaciaDestino.Text = "";
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                bDestinoEsAlmacen = leer.CampoBool("EsAlmacen");
                //////////// Jesus Diaz 2K111107.1510 
                //////////// Si la Farmacia Conectada Es Almacen no se valida la Farmacia Destino 
                //////if (!DtGeneral.EsAlmacen)
                //////{
                //////    if (leer.CampoBool("EsAlmacen"))
                //////    {
                //////        General.msjUser("La Farmacia seleccionada esta configurada como Almacén,\nno es posible generar la transferencia de Farmacia a Almacén.");
                //////        txtFarmaciaDestino.Text = "";
                //////        lblFarmaciaDestino.Text = "";
                //////    }
                //////    else
                //////    {
                //////        bRegresa = true;
                //////        txtFarmaciaDestino.Enabled = false;
                //////        txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                //////        lblFarmaciaDestino.Text = leer.Campo("Farmacia");
                //////    }
                //////}
                //////else
                {
                    bRegresa = true;
                    cboEstados.Enabled = false;
                    txtFarmaciaDestino.Enabled = false;
                    txtFarmaciaDestino.Text = leer.Campo("IdFarmacia");
                    lblFarmaciaDestino.Text = leer.Campo("Farmacia");

                    linkUrlFarmacia.Text = leer.Campo("UrlFarmacia"); 
                }
            }
            return bRegresa;
        }

        private bool validarFarmaciaRelacionada()
        {
            bool bRegresa = false;
            clsLeer leerRelacion = new clsLeer(ref cnn);
            string sMD5 = "";
            string sDesencripcion = "";
            string[] sArreglo;
            string sFarmaciaRelacionada = leer.Campo("IdFarmacia");

            leerRelacion.DataSetClase = query.FarmaciasRelacionadas(cboEstados.Data, DtGeneral.FarmaciaConectada, sFarmaciaRelacionada, "validarFarmaciaRelacionada()");
            if (leerRelacion.Leer())
            {
                sMD5 = leerRelacion.Campo("MD5");
                sDesencripcion = crypto.Desencriptar(sMD5, 11);

                sArreglo = sDesencripcion.Split('|');

                try
                {
                    if (sArreglo[0] == DtGeneral.EstadoConectado &&
                        sArreglo[1] == DtGeneral.FarmaciaConectada &&
                        sArreglo[2] == sFarmaciaRelacionada &&
                        sArreglo[3].ToUpper() == "A")
                    {
                        bRegresa = true;
                    }
                }
                catch { }
            }


            return bRegresa;
        }

        private void CargarDetallesTransferencia()
        {
            // myGrid.Limpiar(false);
            //bEsSurtimientoPedido = true;
            leer.DataSetClase = query.FolioTransferenciaDetalles(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            if (leer.Leer())
            {
                myGrid.LlenarGrid(leer.DataSetClase);
                CargarDetallesLotesTransferencia();
            }

            //bCapturaDeClavesSolicitadasHabilitada = !bEsSurtimientoPedido;
            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
            Totalizar();
        }

        private void CargarDetallesLotesTransferencia()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.FolioTransferenciaDetallesLotes(sEmpresa, sEstado, sCveRenapo, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                string sFolio = sIdTipoMovtoInv + Fg.PonCeros(txtFolio.Text, 8);
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones_TransferenciaDeSalida(sEmpresa, sEstado, sFarmacia, sFolio, "CargarDetallesLotesMovimiento");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        private void ActivarRptCaja()
        {
            bool bActivar = false;
            string sSql = "";

            sSql = string.Format("Select * From Pedidos_Cedis_Enc_Surtido (NoLock) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferenciaReferencia = '{3}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioTransferencia);

            sFoliosSurtido = "";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActivarRptCaja()");
                General.msjError("Error al buscar la clave lote");
            }
            else
            {
                bActivar = leer.Leer();
                sFoliosSurtido = leer.Campo("FolioSurtido");

                chk_RPT_Cajas.Enabled = bActivar;
                chk_RPT_Cajas.Visible = bActivar;
                chk_RPT_Cajas.Checked = bActivar;

                chk_RPT_EtiquetasCajas.Enabled = bActivar;
                chk_RPT_EtiquetasCajas.Visible = bActivar;
            }
        }

        public void CargarPedido(string FoliosSurtido, string FolioPedido)
        {
            CargarPedido(FoliosSurtido, FolioPedido, 1);
        }

        public void CargarPedido(string FoliosSurtido, string FolioPedido, int Registros)
        {
            LimpiarPantalla(false);

            sFoliosSurtido = FoliosSurtido;
            sFolioPedido = FolioPedido;
            iRegistros = Registros;

            bEsSurtimientoPedido = true;

            

            string sSql = string.Format("Select \n\t distinct E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdEstadoSolicita, E.IdFarmaciaSolicita As IdFarmaciaPedido \n" +
                "From Pedidos_Cedis_Enc_Surtido S (NoLock) \n" +
                "Inner Join Pedidos_Cedis_Enc  E (NoLock) On ( S.IdEmpresa = E.IdEmpresa And S.IdEstado = E.IdEstado And S.IdFarmaciaPedido = E.Idfarmacia And S.FolioPedido = E.FolioPedido ) \n" +
                "Where S.IdEmpresa = '{0}' and S.IdEstado = '{1}' and S.IdFarmacia = '{2}' And s.FolioPedido =  '{3}' and S.FolioSurtido in ({4}) \n",
                sEmpresa, sEstado, sFarmacia, sFolioPedido, sFoliosSurtido);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenarEncabezadoFolio()");
                General.msjError("Error al cargar el Encabezado del Folio de surtido.");
                this.Close();
            }
            else
            {
                if (leer.Leer())
                {
                    txtFolio.Text = "*";
                    txtFolio_Validating(this, null);

                    cboEstados.Data = leer.Campo("IdEstadoSolicita");
                    cboEstados.Enabled = false;

                    txtFarmaciaDestino.Text = leer.Campo("IdFarmaciaPedido");
                    txtFarmaciaDestino_Validating(this, null);

                    dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");

                    LlenarDetalleFolio();
                }
                else
                {
                    General.msjUser("Error al cargar el Encabezado del Folio de surtido");
                }
            }

            btnNuevo.Enabled = false; 

            this.ShowDialog();
        }

        private void LlenarDetalleFolio()
        {


            string sSql = string.Format("Select \n" + 
                "\tS.CodigoEAN, S.IdProducto, P.DescripcionCorta As DescProducto, P.TasaIva, Sum( S.CantidadAsignada ) as Cantidad, \n" +
                "\tdbo.fg_ObtenerCostoPromedio(S.IdEmpresa, S.IdEstado, S.CodigoEAN, S.IdProducto) As Costo \n" +
                "From Pedidos_Cedis_Det_Surtido_Distribucion S(NoLock) \n" +
                "Inner Join vw_Productos_CodigoEAN P(NoLock) On ( S.IdProducto = P.IdProducto And S.CodigoEAN = P.CodigoEAN ) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' and S.FolioSurtido in ({3}) and S.CantidadAsignada > 0  \n" +
                "Group By S.CodigoEAN, S.IdProducto, P.DescripcionCorta, P.TasaIva, S.IdEmpresa, S.IdEstado, S.CodigoEAN, S.IdProducto\n",
                sEmpresa, sEstado, sFarmacia, sFoliosSurtido);

            myGrid.Limpiar(false);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Error al obtener el detalle del Folio de Surtido.");
            }
            else
            {
                if (leer.Registros > 0)
                {
                    myGrid.LlenarGrid(leer.DataSetClase);
                    myGrid.BloqueaGrid(true);

                    DetallesLotesTransferencia_Surtido();
                    Totalizar();
                }
                else
                {
                    General.msjUser("El Folio de Surtido seleccionado no contiene detalles. Favor de verificar.");
                }
            }

            myGrid.BloqueaColumna(true, (int)Cols.CodEAN);
        }

        private void DetallesLotesTransferencia_Surtido()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = query.GenerarTransferenciaDetallesLotes(sEmpresa, sEstado, sFarmacia, sFoliosSurtido, "CargarDetallesTransferencia");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = query.GenerarTransferenciaDetallesLotesUbicacion(sEmpresa, sEstado, sFarmacia, sFoliosSurtido, "CargarDetallesLotesMovimiento");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        #endregion Cargar Datos

        private void FrmTransferenciaSalidas_Load(object sender, EventArgs e)
        {
            
            if (!bEsSurtimientoPedido)
            {
                LimpiarPantalla(false);
            }

            if (bCapturaDeClavesSolicitadasHabilitada)
            {
                lblMensajes.Text = "( F7 ) Lotes. Visualizar.              ( F9 ) Captura Claves Solicitadas.          ";
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

                case Keys.F12:
                    validarExistenciasCantidadesLotes(true); 
                    break;

                case Keys.F9:
                    MostrarCapturaDeClavesRequeridas();
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bFolioGuardado = false;
            lblCancelado.Visible = false; 
            IniciarToolBar(false, false, false, false);
            string sStatus = "";
             
            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                if (cboEstados.SelectedIndex != 0)
                {
                    txtFolio.Enabled = false;
                    txtFolio.Text = "*";
                    cboEstados.Enabled = false; 
                    IniciarToolBar(true, false, false, false);
                }
            }
            else
            {
                if (txtFolio.Text.Trim() != "")
                {
                    txtFolio.Text = Fg.PonCeros(txtFolio.Text, 8);
                    leer.DataSetClase = query.FolioTransferencia(sEmpresa, sEstado, bMostrarSoloAlmacenes, sCveRenapo, sFarmacia,
                        txtFolio.Text, sIdTipoMovtoInv, bTransferenciaInterestatal, DtGeneral.EsAlmacen, "txtFolio_Validating_1");
                    if (!leer.Leer())
                    {
                        txtFolio.SelectAll();
                        txtFolio.Focus();
                    }
                    else 
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true, true);
                        txtFolio.Enabled = false;

                        cboEstados.Enabled = false;
                        cboEstados.Data = leer.Campo("IdEstadoRecibe");

                        sFolioTransferencia = leer.Campo("Folio");
                        txtFolio.Text = Fg.PonCeros(sFolioTransferencia, 8);

                        txtFarmaciaDestino.Enabled = false;
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaTransferencia");
                        txtFarmaciaDestino.Text = leer.Campo("IdFarmaciaRecibe");
                        lblFarmaciaDestino.Text = leer.Campo("FarmaciaRecibe");
                        txtObservaciones.Text = leer.Campo("Observaciones");
                        txtObservaciones.Enabled = false; 
                        sStatus = leer.Campo("Status"); 
                        TransferenciaAplicada = leer.CampoBool("TransferenciaAplicada");

                        CargarDetallesTransferencia(); // Usan la misma Leer 

                        if (sStatus.Trim() == "C")
                        {
                            lblCancelado.Visible = true;
                            lblCancelado.Text = "CANCELADA";
                            General.msjUser("Salida de Traspaso esta cancelada.");
                            IniciarToolBar(false, false, true, false);
                        }
                        else
                        {
                            //////// Jesús Díaz 2K120712.1145 
                            //// if (DtGeneral.EsAdministrador)
                            ////if (GnFarmacia.EsServidorDeRedLocal || DtGeneral.EsAdministrador || DtGeneral.EsAlmacen) 
                            {
                                IniciarToolBar(false, true, true, true);
                                if (TransferenciaAplicada)
                                {
                                    lblCancelado.Visible = true;
                                    lblCancelado.Text = "APLICADA";

                                    General.msjUser("Salida de Traspaso aplicada. No es posible realizar cambios.");
                                    IniciarToolBar(false, false, true, false);
                                }
                            }
                        }
                        ObtenerUrlFarmacia();

                        if (DtGeneral.EsAlmacen)
                        {
                            ActivarRptCaja();
                        }
                    }
                }
            }

        }

        private void txtFarmaciaDestino_Validating(object sender, CancelEventArgs e)
        {
            bool bExito = false;
            linkUrlFarmacia.Text = "";

            myGrid.Limpiar(false);
            string sInf_Origen = DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada;
            string sInf_Destino = cboEstados.Data + Fg.PonCeros(txtFarmaciaDestino.Text, 4); 

            if (txtFarmaciaDestino.Text.Trim() != "")
            {
                if (sInf_Origen == sInf_Destino)
                {
                    General.msjUser("No se puede generar traspaso a la farmacia origen, Favor de verificar.");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = query.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstados.Data, sFarmacia, 
                        txtFarmaciaDestino.Text, bMostrarSoloAlmacenes, "txtFarmaciaDestino_Validating");
                    if (leer.Leer())
                    {
                        bExito = CargarDatosFarmacia();
                        if (bExito)
                        {
                            sFolioSolicitud = RobotDispensador.Robot.ObtenerFolioSolicitud(); 
                        }

                        // myGrid.Limpiar(true); 
                    }
                }
            }

            if (!bExito)
            {
                txtFarmaciaDestino.Text = "";
                lblFarmaciaDestino.Text = "";
                //txtFarmaciaDestino.Focus();
                // e.Cancel = true;
                //txtFarmaciaDestino.Focus(); 
            }
            else
            {
                if (txtFolio.Text.Trim() == "*" && bImplementaCodificacion)
                {
                    btnCodificacion.Enabled = btnGuardar.Enabled;
                    myGrid.BloqueaGrid(true);
                }
                else
                {
                    myGrid.Limpiar(true);
                }
            }
        }

        private void txtFarmaciaDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (bMostrarSoloAlmacenes)
                {
                    leer.DataSetClase = ayuda.AlmacenesTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstados.Data, sFarmacia, "txtFarmaciaDestino_KeyDown");
                }
                else
                {
                    leer.DataSetClase = ayuda.FarmaciasTransferencia(sEmpresa, iEsEmpresaConsignacion, cboEstados.Data, sFarmacia, "txtFarmaciaDestino_KeyDown");
                }
                

                if (leer.Leer())
                {
                    if (CargarDatosFarmacia())
                    {
                        if (txtFolio.Text.Trim() == "*" && bImplementaCodificacion)
                        {
                            btnCodificacion.Enabled = btnGuardar.Enabled;
                            myGrid.BloqueaGrid(true);
                        }
                        else
                        {
                            myGrid.Limpiar(true);
                        }
                    }
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

        private void btnCodificacion_Click(object sender, EventArgs e)
        {
            int iRows = 0;
            string sProducto = "", sCodigoEAN = "", sDescripcion = "";
            clsLeer leerUUIDS = new clsLeer();
            clsLotes lotes_Aux = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Todos);

            sProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
            sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            sDescripcion = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion);

            FrmLotesSNK f = new FrmLotesSNK();
            f.MostrarPantalla(sProducto, sCodigoEAN, sDescripcion, bEsIdProducto_Ctrl, Lotes, GnFarmacia.ManejaUbicaciones);
            Lotes = f.LotesCodigos;

            if (Lotes.ListadoDeCodigosEAN().Count > 0)
            {
                myGrid.Limpiar(false);
                lotes_Aux = Lotes;

                foreach (string sCodigoEAN_SNK in Lotes.ListadoDeCodigosEAN())
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    iRows = myGrid.Rows;

                    myGrid.ActiveRow = iRows;
                    myGrid.SetActiveCell(iRows, 1);
                    myGrid.SetValue(iRows, (int)Cols.CodEAN, sCodigoEAN_SNK);

                    ObtenerInformacion(sCodigoEAN_SNK);
                    //CargarDatosProducto(iRows);
                    //ObtenerDatosProducto(iRows, false);
                    sProducto = myGrid.GetValue(iRows, (int)Cols.Codigo);
                    sCodigoEAN = myGrid.GetValue(iRows, (int)Cols.CodEAN);

                    myGrid.SetValue(iRows, (int)Cols.Cantidad, lotes_Aux.Totalizar(sProducto, sCodigoEAN));

                }

                Lotes = lotes_Aux;
                ////Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario); 
                ////Lotes.AddLotes(lotes_Aux.DataSetLotes); 
                ////Lotes.UUID_UpdateList(lotes_Aux.UUID_List().DataSetClase); 
                leerUUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;

            }
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
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) >= iCantidadMinima_Avance)
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

                            if (!bEsIdProducto_Ctrl)
                            {
                                sValorGrid = leer.Campo("CodigoEAN");
                                leer.DataSetClase = ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "", false, bSoloControlados, "grdProductos_KeyDown");
                                if (leer.Leer())
                                {
                                    sValorGrid = leer.Campo("CodigoEAN");
                                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid);
                                    ObtenerInformacion(sValorGrid);
                                    //CargarDatosProducto();
                                }
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

                        // Administracion de Mach4 
                        if (e.KeyCode == Keys.F10)
                        {
                            if (bEsClienteInterface && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
                            {
                                string sIdProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
                                string sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                                if (sIdProducto != "")
                                {
                                    RobotDispensador.Robot.Show(sIdProducto, sCodigoEAN);
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
                            leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, bSoloControlados, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sValor, ref sValor, ref bEsEAN_Unico))
                                {
                                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN);
                                }
                                //else
                                //{
                                    if (!bEsEAN_Unico)
                                    {
                                        leer.GuardarDatos(1, "CodigoEAN", sValor); 
                                        //leer.DataSetClase = query.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                    }

                                ////    CargarDatosProducto();
                                ////}
                                sValor = leer.Campo("CodigoEAN");
                                ObtenerInformacion(sValor);
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

        private void ObtenerInformacion(string sValor)
        {
            bool bEsEAN_Unico = true;

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

        private bool CargarDatosProducto()
        {
            return CargarDatosProducto(myGrid.ActiveRow);
        }

        private bool CargarDatosProducto(int Renglon)
        {
            bool bRegresa = true;
            int iRow = Renglon;
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false; 
            string sCodEAN = leer.Campo("CodigoEAN");
            bool bAgregar = true;
            string sMensajeControlado = "";


            if (leer.CampoBool("EsControlado"))
            {
                if (!bFarmaciaDestino_ManejaControlados)
                {
                    bRegresa = false;
                    sMensajeControlado = "La Unidad destino no maneja controlados, no es posible agregar el producto.";
                }

                if (bRegresa && !DtGeneral.EsAlmacen && !bEsDestinoAlmacen)
                {
                    if (!GnFarmacia.HabilitarTransferenciasControlado)
                    {
                        bRegresa = false;
                        sMensajeControlado = "Sin permiso para Traspasos de controlados entre unidades. No puede agregar el producto.";
                    }
                }
            }

            bAgregar = bRegresa;

            if (!bAgregar)
            {
                General.msjAviso(sMensajeControlado);
                myGrid.LimpiarRenglon(iRow);
                myGrid.SetActiveCell(iRow, iColEAN);
            }
            else 
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

                    //// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    if (bEsClienteInterface)
                    {
                        if (bEsMach4)
                        {
                            GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRow);
                            RobotDispensador.Robot.Show(leer.Campo("IdProducto"), sCodEAN);
                        }
                    }

                    if (!bImplementaCodificacion)
                    {
                        CargarLotesCodigoEAN();
                    }
                }
                else
                {
                    General.msjUser("Producto capturado en otro renglon. Favor de verificar.");
                    myGrid.LimpiarRenglon(iRow);
                    myGrid.SetActiveCell(iRow, iColEAN);
                    myGrid.EnviarARepetido(); 
                }
            }
            //else
            //{
            //    // Asegurar que no cambie el CodigoEAN
            //    myGrid.SetValue(iRow, iColEAN, sCodEAN);
            //}

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

            
            if (bTransferenciaInterestatal) 
            {
                //leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sIdSubFarmacia_Traspasos, sCodigo, sCodEAN, TiposDeInventario.Venta, "CargarLotesCodigoEAN()");
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sIdSubFarmacia_Traspasos, sCodigo, sCodEAN, tpInventario___TS_InterEstatal, false, "CargarLotesCodigoEAN()");
            }
            else
            {
                leer.DataSetClase = query.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            }


            if (query.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase); 

                if (GnFarmacia.ManejaUbicaciones)
                {
                    if (bTransferenciaInterestatal)
                    {
                        //leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, "CargarLotesCodigoEAN()");
                        leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventario___TS_InterEstatal, false, "CargarLotesCodigoEAN()");
                    }
                    else
                    {
                        leer.DataSetClase = query.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
                    }


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
                    Lotes.ModificarCantidades = !bFolioGuardado;

                    ////// Jesus Diaz 2K111208.1213 
                    // Permitir captura de Caducados cuando el Destino es Almacen 
                    if (bDestinoEsAlmacen) 
                    {
                        // Solo para Movientos Especiales 
                        Lotes.PermitirSalidaCaducados = true; 
                    }

                    if (bImplementaCodificacion)
                    {
                        Lotes.ModificarCantidades = false;
                    }


                    if (bEsSurtimientoPedido)
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    //// Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    //////// Administracion de Mach4
                    //////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    //////{
                    //////    if (IMachPtoVta.RequisicionRegistrada)
                    //////    {
                    //////        Lotes.Show();
                    //////    }
                    //////}
                    //////else
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
        private void btnGenerarPaqueteDeDatos_Click(object sender, EventArgs e)
        {          
            GenerarPaqueteDeDatos();            
        }

        private void GenerarPaqueteDeDatos()
        {
            string sMsj = string.Format("¿ Desea generar el paquete de datos para el Traspaso {0} ?", sFolioTransferencia);
            sMsj = string.Format("¿ Desea generar el paquete de datos para el Traspaso {0} ?", txtFolio.Text);

            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                if (PreparaTransferenciaReenvio())
                {
                    ClienteTransferencias = new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion);
                    ClienteTransferencias.TransferenciasAutomaticas(cboEstados.Data, Fg.PonCeros(txtFarmaciaDestino.Text, 4));
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
            string sFolioRev = "TS" + txtFolio.Text; 

            // sSql = string.Format("Update  Set Actualizado = 0 Where IdEstadoEnvia = '20' and IdFarmaciaEnvia = '0011' and IdFarmaciaRecibe = '0010' ");
            sSql = string.Format("Update TransferenciasEnvioEnc Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaDestino.Text, sFolioRev); 

            sSql += string.Format("Update TransferenciasEnvioDet Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaDestino.Text, sFolioRev);

            sSql += string.Format("Update TransferenciasEnvioDet_Lotes Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaDestino.Text, sFolioRev);

            sSql += string.Format("Update TransferenciasEnvioDet_LotesRegistrar Set Actualizado = 0 " +
                " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and IdEstadoRecibe = '{2}' and IdFarmaciaRecibe = '{3}' and FolioTransferencia = '{4}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboEstados.Data, txtFarmaciaDestino.Text, sFolioRev);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "PreparaTransferenciaReenvio()"); 
                General.msjError("Error al preparar la información para el Traspaso."); 
            }            

            return bRegresa; 
        }
        #endregion Manejo de Transferencias

        #region Cancelacion_Transferencia
        private bool CancelaTransferenciaEncabezado()
        {
            bool bRegresa = true;
            string sSql = ""; 
            sFolioTransferencia = "TS" + txtFolio.Text;

            sSql = string.Format(" Update TransferenciasEnc Set SubTotal = 0, Iva = 0, Total = 0, Status = 'C', " + 
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
            sFolioTransferencia = "TS" + txtFolio.Text;

            sSql = string.Format(" Update TransferenciasDet Set Cant_Devuelta = Cant_Enviada, CantidadEnviada = 0, " +
                    " SubTotal = 0, ImpteIva = 0, Importe = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            sSql += string.Format(" Update TransferenciasDet_Lotes Set CantidadEnviada = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            sSql += string.Format(" Update TransferenciasDet_Lotes_Ubicaciones Set CantidadEnviada = 0, Status = 'C' " +
                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferencia = '{3}' \n\n ",
                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (bImplementaCodificacion)
            {
                sSql += string.Format(" Update Transferencias_UUID Set Status = 'C' " +
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

        private bool ReactivaUUIDS()
        {
            bool bRegresa = true;

            string sSql = string.Format("Update F Set F.Status = 'A' From FarmaciaProductos_UUID F \n" +
                "Inner Join Transferencias_UUID T On (F.IdEmpresa = T.IdEmpresa And F.IdEstado = T.IdEstado And F.IdFarmacia = F.IdFarmacia And F.UUID = T.UUID) \n" +
                "Where T.IdEmpresa = '{0}' And T.IdEstado = '{1}' And T.IdFarmacia = '{2}' And T.FolioTransferencia = '{3}'",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "ReactivaUUIDS()");
            }

            return bRegresa;
        }

        #endregion Cancelacion_Transferencia

        #region Borrar_TransferenciaEnvio
        private bool BorraTransferenciasEnvio()
        {
            bool bRegresa = true;

            string sSql = "";

            if (bImplementaCodificacion)
            {
                sSql = string.Format(" Delete From Transferencias_UUID_Registrar " +
                                    " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n ",
                                    sEmpresa, sEstado, sFarmacia, sFolioTransferencia);
            }

            sSql += string.Format(" Delete From TransferenciasEnvioDet_LotesRegistrar " +
	                            " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From TransferenciasEnvioDet_Lotes " +
	                            " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From TransferenciasEnvioDet " +
	                            " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                                " Delete From TransferenciasEnvioEnc " +
	                            " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;                
            }

            return bRegresa;
        }
        #endregion Borrar_TransferenciaEnvio

        #region Obtener_Url_Farmacia
        private bool ObtenerUrlFarmacia()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Select F.UrlFarmacia, C.Servidor From vw_Farmacias_Urls F (Nolock) " +
                                        " Inner Join CFGS_ConfigurarConexiones C (NoLock) " +
                                        "   On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) " +
                                        " Where F.IdEmpresa = '{0}' And F.IdEstado = '{1}' And F.IdFarmacia = '{2}' ", 
                                        sEmpresa, sEstado, txtFarmaciaDestino.Text);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                General.msjError("Error al Obtener la Url de Farmacia Destino");
                Error.GrabarError(leer, "ObtenerUrlFarmacia()");                
            }
            else
            {
                if (leer.Leer())
                {
                    sUrlFarmacia = leer.Campo("UrlFarmacia");
                    sHost = leer.Campo("Servidor");
                }
            }

            return bRegresa;
        }
        #endregion Obtener_Url_Farmacia

        #region Validar_Existe_Transferencia_Destino
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlFarmacia, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlFarmacia;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                //ActivarControles();
            }

            return bRegresa;
        }

        private bool ConsultarTransferenciaDestino()
        {
            bool bCancela = false;
            bool bBorrarTransf = false;

            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + txtFarmaciaDestino.Text;

            string sSql = string.Format(" SELECT IdEmpresa, Folio, FolioReferenciaEntrada, FechaRegistroEntrada, " +
                        " (case when FolioReferenciaEntrada = '*' Then Status Else 'E' End ) as Status, StatusTransferencia " +
                        " FROM vw_TransferenciaEnvio_Enc (NoLock) " +
                        " WHERE IdEstadoEnvia = '{0}' AND IdFarmaciaEnvia = '{1}' AND IdFarmaciaRecibe = '{2}' AND Folio = '{3}' ",
                        sEstado, sFarmacia, Fg.PonCeros(txtFarmaciaDestino.Text, 4), sFolioTransferencia);
           

            //clsLeerWebExt myWeb = new clsLeerWebExt(sUrlFarmacia, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            if (validarDatosDeConexion())
            {
                if (!leerWeb.Exec(sSql))
                {
                    Error.GrabarError(sValor + " -- " + sUrlFarmacia, "ConsultarTransferenciaDestino()");
                    General.msjError("No fue posible verificar el Estatus del Traspaso. Intente de nuevo."); 
                }
                else
                {
                    if (!leerWeb.Leer())
                    {
                        bCancela = true;
                    }
                    else 
                    {
                        if (leerWeb.Campo("Status") == "A")
                        {
                            bCancela = true; 
                            bBorrarTransf = true;                            
                        }
                        if (leerWeb.Campo("Status") == "E")
                        { 
                            General.msjAviso("Traspaso registrado en la Unidad destino");
                            // bCancela = false;
                            IniciarToolBar(false, false, true, false);
                        }
                    }
                }
            }

            if (bCancela && bBorrarTransf)
            {
                bCancela = BorrarTransferenciasEnvioDestino(); 
            }
            return bCancela;
        }

        private bool BorrarTransferenciasEnvioDestino()
        {
            bool bRegresa = true;

            string sSql = string.Format(
                " Delete From TransferenciasEnvioDet_LotesRegistrar " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " + 
                " " + 
                " Delete From TransferenciasEnvioDet_Lotes " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                " " +                 
                " Delete From TransferenciasEnvioDet " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' \n " +
                " " +                 
                " Delete From TransferenciasEnvioEnc " +
                " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            //if (!leerWeb.Exec(sSql))
            //{
            //    bRegresa = false;
            //}
            bRegresa = leerWeb.Exec(sSql); 
            if ( !bRegresa ) 
            {
                Error.GrabarError(leerWeb.MensajeError, "BorrarTransferenciasEnvioDestino"); 
                General.msjError("Error al cancelar el Traspaso en el Destino. Favor de intentar de nuevo."); 
            }


            return bRegresa;
        }
        #endregion Validar_Existe_Transferencia_Destino

        #region AfectarExistenciaEnTransito
        private bool AfectarExistenciaEnTransito(int TipoFactor)
        {
            string sSql = string.Format("Exec spp_INV_AplicaDesaplicaExistenciaTransito \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}', @TipoFactor = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioTransferencia, TipoFactor);

            bool bRegresa = leer.Exec(sSql);
            return bRegresa;
        }
        #endregion AfectarExistenciaEnTransito

        #region Validar_Status_Integracion
        private bool ValidaStatusIntegrada()
        {
            bool bRegresa = true;
            string sSql = "", Status = "";            

            sSql = string.Format(" Select * From TransferenciasEnvioEnc (Nolock) Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' " +
                                 " and FolioTransferencia = '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidaStatusIntegrada()");
                General.msjError("Error al consultar estatus.");
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    Status = leer.Campo("Status");

                    if (Status == "I")
                    {
                        General.msjAviso("Traspaso integrado en el destino. No es posible realizar cambios ");
                        bRegresa = false;
                    }
                    if (Status == "T")
                    {
                        General.msjAviso("Traspaso en transito. No es posible realizar cambios ");
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }
        #endregion Validar_Status_Integracion

        #region Actualiza_Status_Transferencia
        private void ActualizaStatusTransferencia()
        {
            string sSql = string.Format(" Update TransferenciasEnvioEnc Set Status = 'T' " +
                          " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' ",
                          sEmpresa, sEstado, sFarmacia, sFolioTransferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizaStatusTransferencia()");
                General.msjError("Error al actualizar estatus del Traspaso.");
            }
        }
        #endregion Actualiza_Status_Transferencia

        #region ClaveRequerida
        private void MostrarCapturaDeClavesRequeridas()
        {
            if (bCapturaDeClavesSolicitadasHabilitada)
            {
                InfCveSolicitadas.Show(txtFolio.Text);
                //InfCveSolicitadas.Claves(); 
            }
        }

        private bool GuardarClavesSolicitadas()
        {
            bool bRegresa = true;
            // bool bExistenClaves = false; 
            string sSql = "", sIdClaveSSA = "";  // , sObservaciones = "";
            int iCantidad = 0;

            if (bCapturaDeClavesSolicitadasHabilitada) // Revisar el Parametro 
            {
                //while (InfCveSolicitadas.Claves())
                leerClaves.DataSetClase = InfCveSolicitadas.ClavesCapturadas;
                while (leerClaves.Leer())
                {
                    // bExistenClaves = true;
                    sIdClaveSSA = leerClaves.Campo("IdClaveSSA");
                    iCantidad = leerClaves.CampoInt("Cantidad");

                    // Grabar en los Movimientos de inventario 
                    sSql = string.Format(" Exec spp_Mtto_TransferenciaClavesSolicitadas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioTransferencia, sIdClaveSSA, iCantidad, InfCveSolicitadas.Observaciones);

                    if (!leer.Exec(sSql))
                    {
                        bContinua = false;
                        bRegresa = false;
                        break;
                    }
                }

                if (bRegresa)
                {
                    bRegresa = CalcularSurtimientoClavesSolicitadas();
                }
            }

            return bRegresa;
        }

        private bool CalcularSurtimientoClavesSolicitadas()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_TransferenciasClavesSolicitadasCalcularSurtimiento @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioTransferencia = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioTransferencia);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            bContinua = bRegresa;
            return bRegresa;
        }

        #endregion ClaveRequerida

        #region UUID

        private bool Guardar_UUIDS(bool Validar_UUID, bool EsSalida)
        {
            bool bRegresa = true;
            string sSql = "";
            clsLeer leer_UUIDS = new clsLeer();
            int iValidar = Validar_UUID ? 1 : 0;
            int iEsSalida = EsSalida ? 1 : 0;

            leer_UUIDS.DataSetClase = Lotes.UUID_List.DataSetClase;
            bRegresa = leer_UUIDS.Registros > 0;

            while (leer_UUIDS.Leer())
            {
                sSql = string.Format("Exec spp_Mtto_Transferencias_UUID " +
                    " @UUID = '{0}', @IdEmpresa = '{1}', @IdEstado = '{2}', @IdFarmacia = '{3}', @FolioTransferencia = '{4}', " +
                    " @ValidarUUID = '{5}', @TipoDeProceso = '{6}' ",
                    leer_UUIDS.Campo("UUID"), DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioTransferencia,
                    iValidar, iEsSalida);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }


            return bRegresa;
        }

        #endregion UUID

        private void FrmTransferenciaSalidas_Base_Shown(object sender, EventArgs e)
        {
            if(!bEsSurtimientoPedido)
            {
                if(DtGeneral.TieneSurtimientosActivos())
                {
                    General.msjAviso(sMensajeConSurtimiento);
                    bTieneSurtimientosActivos = true;
                }

                if(DtGeneral.EsAlmacen)
                {
                    if(!DtGeneral.Almacen_PermisoEspecial())
                    {
                        General.msjAviso("Opción de Traspasos no esta habilitada para guardar.");
                        bTienePermitidasTransferenciasNormales = false;
                    }
                }


                if(!GnFarmacia.DispensacionActiva_Verificar())
                {
                    //General.msjAviso("La dispensación de Venta esta deshabilitada, sólo es posible dispensar Consignación.");
                    General.msjAviso(GnFarmacia.DispensacionActiva_Mensaje());
                }
            }
        }

        private void linkUrlFarmacia_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            // Specify that the link was visited.
            this.linkUrlFarmacia.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(linkUrlFarmacia.Text);
        }
    } // Clase 
}
