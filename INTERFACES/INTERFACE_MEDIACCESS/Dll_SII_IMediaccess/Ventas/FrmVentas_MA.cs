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
using DllFarmaciaSoft.Devoluciones;
using DllFarmaciaSoft.LimitesConsumoClaves;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.Inventario;

using Farmacia.Procesos;
using Farmacia.Vales;
using Farmacia.Ventas;

using DllFarmaciaSoft.Ventas; 

using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.Validaciones_MA;
using Dll_SII_IMediaccess.Ventas_IME;

using DllRecetaElectronica.ECE;  

namespace Dll_SII_IMediaccess.Ventas
{
    public partial class FrmVentas_MA : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Precio = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10, 
            EsIMach4 = 11, UltimoCosto = 12, ClaveSSA = 13, Cantidad_Maxima = 14    
        }

        //////PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        string sFolioSolicitud = ""; 

        clsImprimirVentas VtasImprimir;
        clsInformacionVentas InfVtas;
        clsClavesSolicitadas InfCveSolicitadas; 
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsLeer leerClaves;
        clsLeer leerBusqueda;
        clsCodigoEAN EAN = new clsCodigoEAN();
        TipoDePuntoDeVenta tpPuntoDeVenta = TipoDePuntoDeVenta.Farmacia_Almacen;

        clsGrid myGrid, myGridPago;
        // Variables Globales  ****************************************************
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bImportarBeneficiarios = false;
        bool bCapturaDeClavesSolicitadasHabilitada = GnFarmacia.CapturaDeClavesSolicitadasHabilitada;
        bool bActivarProceso = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "", sFolioVenta = "", sFolioSurtido = "";
        bool bEsSurtimientoPedido = false;
        string sFarmaciaPed = "", sFolioPedido = "";
        string sMsjNoEncontrado = "";

        string IdEmpresaCSGN = "", IdEstadoCSGN = "", IdFarmaciaCSGN = "", sFolioPedidoRC = "", IdJurisdiccionCSGN = "";
        int iCantidadSurtidaCSGN = 0;

        bool bContinua = true;
        double fSubTotalIva_0 = 0;
        double fSubTotal = 0;

        double fSubTotal_SinGravar = 0;
        double fSubTotal_Gravado = 0;
        double fIva = 0;
        double fTotal = 0;

        double fSubTotal_SinGravar_General = 0;
        double fSubTotal_Gravado_General = 0;
        double fIva_General = 0;
        double fTotal_General = 0;

        double dPorcentaje_Aplicar = 0; 

        //string sFormato = "#,###,###,##0.###0";
        string sFormato = "$ #,#0.00";
        string sFormato_04 = "$ #,#0.000#"; 

        double dDescuentoCopago = 0;
        double dTotal_a_Pagar = 0;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        // int iAnchoColPrecio = 0;

       //***************************************************************************

        DllFarmaciaSoft.clsAyudas Ayuda; // = new clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sIdSeguroPopular = GnFarmacia.SeguroPopular;
        bool bEsSeguroPopular = false;
        bool bValidarSeguroPopular = GnFarmacia.ValidarInformacionSeguroPopular;
        bool bValidarBeneficioSeguroPopular = GnFarmacia.ValidarBeneficioSeguroPopular;
        bool bDispensarSoloCuadroBasico = GnFarmacia.DispensarSoloCuadroBasico;
        bool bImplementaCodificacion = GnFarmacia.ImplementaCodificacion_DM; 

        bool bFolioGuardado = false;
        TiposDeUbicaciones tpUbicacion = TiposDeUbicaciones.Todas;
        
        string sManejaTodasLasUbicaciones = "MANEJA_TODAS_LAS_UBICACIONES";
        string sManejaSoloUbicacionesAlmacenaje = "MANEJA_SOLO_UBICACIONES_DE_ALMACENAJE";

        #region variables
        // bool bExisteMovto = false;
        // bool bEstaCancelado = false;
        // bool bMovtoAplicado = false;

        string sFolioMovtoInv = "";
        // string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";

        string sIdTipoMovtoInv = "SV";
        string sTipoES = "S";
        // string sIdProGrid = "";
        
        // bool bEmiteVales = true; //AQUI DEBE IR LA VARIABLE GLOBAL GnFarmacia.EmiteVales.
        bool bEmiteVales = GnFarmacia.EmisionDeValesCompletos;
        bool bGeneroVale = false;
        string sFolioVale = "";

        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        FrmIniciarSesionEnCaja Sesion;
        // bool bSesionIniciada = false;

        string sCodigoEAN_Seleccionado = ""; 
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        TiposDeInventario tpTipoDeInventario = TiposDeInventario.Todos; 
        clsVerificarSalidaLotes VerificarLotes;
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();

        clsVerificarCantSubPerfil VerificarSubPerfil;

        bool bEsEDM = false;
        string sNumReceta = "", sFolioEDM;

        clsValidar_Elegibilidad localElegibilidad;
        clsValidar_Vale localVale;
        TipoDeSurtido tpSurtido = TipoDeSurtido.Ninguno;

        TipoDeCopago TipoCop;
        double Copago = 0;
        bool bCierreAutomatico = false;
        bool bEsRecetaManual = false;
        bool bCargaAutomaticaProductos = GnDll_SII_IMediaccess.CargaAutomaticaProductosReceta;
        bool bEsVentaAsociados = false;
        int iEsVentaAsociados = 0;
        double dPorcentaje_Descuento___SocioComercial_Asociado = 0;
        bool bEsDispensacionDeConsignacion = false;
        int iNumeroDeCopias = GnFarmacia.NumeroDeCopiasTickets;

        string sListaClavesSSA_RecetaElectronica = "";

        #region Vales 
        bool bEsIdProducto_Ctrl = false; 
        #endregion Vales

        #endregion variables

        #region Variables Cobro Caja
        FrmPagoCaja_MA PagoEnCaja;

        private double dTipoDeCambio = GnFarmacia.TipoDeCambioDollar;
        private double dPagoEfectivo = 0;
        private double dPagoDolares = 0;
        private double dPagoCheques = 0;
        private double dCambios = 0;
        #endregion Variables Cobro Caja

        public FrmVentas_MA(): this(null, false, TiposDeInventario.Venta)
        {
        }

        public FrmVentas_MA(bool EsVentaAsociados, TiposDeInventario Lotes_Para_Dispensacion): this(null, EsVentaAsociados, Lotes_Para_Dispensacion)
        {
        }

        public FrmVentas_MA(object Elegibilidad, bool EsVentaAsociados): this(Elegibilidad, EsVentaAsociados, TiposDeInventario.Venta)
        {
        }

        public FrmVentas_MA(object Elegibilidad, bool EsVentaAsociados, TiposDeInventario Lotes_Para_Dispensacion) 
        {
            // MessageBox.Show(Application.OpenForms.Count.ToString());
            InitializeComponent();
            bEsVentaAsociados = EsVentaAsociados;
            tpTipoDeInventario = Lotes_Para_Dispensacion;
            bEsDispensacionDeConsignacion = tpTipoDeInventario == TiposDeInventario.Consignacion;


            if (EsVentaAsociados)
            {
                iEsVentaAsociados = 1;
                this.Text = "Venta a asociados";
                this.Name = "FrmVentas_MA_Asociados";

                if (bEsDispensacionDeConsignacion)
                {
                    iEsVentaAsociados = 0;
                    this.Text = "Dispensación a asociados";
                    this.Name = "FrmVentas_MA_Consignacion";
                }

                ////lblSubTotalGeneral.Visible = false;
                ////lblDescuentoCopago.Visible = false;
                ////lblImporteAPagar.Visible = false;

                ////txtTotal.Visible = false;
                ////txtDescuentoCopago.Visible = false;
                ////txtTotal_a_Pagar.Visible = false; 
            }
            else
            {
                if (Elegibilidad is clsValidar_Elegibilidad)
                {
                    tpSurtido = TipoDeSurtido.Mediaccess;
                    localElegibilidad = (clsValidar_Elegibilidad)Elegibilidad;
                    bCierreAutomatico = Elegibilidad != null;
                    bEsRecetaManual = Elegibilidad != null ? localElegibilidad.MA_EsRecetaManual : false;
                    bPermitirCapturaBeneficiariosNuevos = false;
                    this.Text = bCierreAutomatico ? "Venta a asegurados" : "Consulta de venta a asegurados";
                    TipoCop = localElegibilidad.TipoCopago;
                    Copago = localElegibilidad.Copago;
                }

                if (Elegibilidad is clsValidar_Vale)
                {
                    tpSurtido = TipoDeSurtido.Intermed;
                    localVale = (clsValidar_Vale)Elegibilidad;
                    bCierreAutomatico = Elegibilidad != null;
                    bPermitirCapturaBeneficiariosNuevos = true;
                    //bCierreAutomatico = localVale != null;
                    bEsRecetaManual = true; //// localVale != null ? localVale.MA_EsRecetaManual : false;

                    //this.Text = bCierreAutomatico ? "Surtido de vales" : "Consulta de vale dispensado";
                    this.Text = "Surtido de vales";
                    TipoCop = TipoDeCopago.Porcentaje;
                    Copago = 0;
                }
            }


            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);
            leerClaves = new clsLeer(ref con);
            leerBusqueda = new clsLeer(ref con);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            VtasImprimir = new clsImprimirVentas(General.DatosConexion, DatosCliente,
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Credito);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true; 
            myGrid.BackColorColsBlk = Color.White;

            ////GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Precio, (int)Cols.Importe, (int)Cols.Descripcion);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref con, sEmpresa, sEstado, sFarmacia, sPersonal);

            //// Control de Acceso para lectura de Codigos DataMatrix 
            btnCodificacion.Visible = bImplementaCodificacion;
            btnCodificacion.Enabled = false; 

            if (GnFarmacia.ValidarUbicacionesEnCapturaDeSurtido)
            {
                SolicitarPermisosUsuario();
            }


            //////// Receta electrónica 
            if (EsVentaAsociados)
            {
                tpSurtido = TipoDeSurtido.AMPM;
                btnRecetasElectronicas.Enabled = false;
                btnRecetasElectronicas.Visible = GnFarmacia.ImplementaInterfaceExpedienteElectronico;
                btnRecetaElectronica_Claves.Visible = btnRecetasElectronicas.Visible;
                toolStripSeparator_05.Visible = btnRecetasElectronicas.Visible;
                bCierreAutomatico = true;
            }

        }

        public void VentaEDM(string NumReceta, string FolioEDM)
        {
            //////this.bEsEDM = true;
            //////this.sNumReceta = NumReceta;
            //////this.sFolioEDM = FolioEDM;
            //////bool bRegresa = false;

            //////btnNuevo_Click(this, null);

            //////leer2.DataSetClase = Consultas.Ventas_EDM_Enc(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");

            //////if (leer2.Leer())
            //////{
            //////    txtCte.Text = leer2.Campo("IdCliente");
            //////    txtCte_Validating(this, null);
            //////    txtSubCte.Text = leer2.Campo("IdsubCliente");
            //////    txtSubCte_Validating(this, null);
            //////    bRegresa = true;
            //////}

            //////if (bRegresa)
            //////{
            //////    leer2.DataSetClase = Consultas.Ventas_EDM_Det(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");
            //////    if (leer2.Leer())
            //////    {
            //////        bRegresa = true;
            //////        myGrid.LlenarGrid(leer2.DataSetClase, false, false);

            //////        leer.DataSetClase = clsLotes.PreparaDtsLotes();
            //////        leer.DataSetClase = Consultas.Ventas_EDM_Det_Lotes(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");
            //////        Lotes.AddLotes(leer.DataSetClase);

            //////        string sSql = string.Format("Select * From Ventas_EDM_Det__UUID (NoLock) " +
            //////            "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ", sEmpresa, sEstado, sFarmacia, FolioEDM);

            //////        leer.Exec(sSql);
            //////        while (leer.Leer())
            //////        { 
            //////            Lotes.UUID_Add(leer.Campo("UUID"));
            //////        }
            //////    }
            //////}

            //////    // myGrid.EstiloGrid(eModoGrid.ModoRow);
            //////myGrid.BloqueaColumna(true, 1);

            //////btnNuevo.Enabled = false;

            //////this.ShowDialog();
        }

        private bool GuadarFolioVenta()
        {
            bool bRegresa = false;

            string sSql = string.Format("Update Ventas_EDM_Enc Set FolioVenta = '{4}' Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'",
                    sEmpresa, sEstado, sFarmacia, sFolioEDM, sFolioVenta);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            ////bPermisoClaveSSA = Permisos.TienePermiso(sPermisoClaveSSA);
            ////bPermisoContenidoPaquete = Permisos.TienePermiso(sPermisoContenidoPaquete);


            tpUbicacion = TiposDeUbicaciones.Picking;
            if (DtGeneral.PermisosEspeciales.TienePermiso(sManejaTodasLasUbicaciones))
            {
                tpUbicacion = TiposDeUbicaciones.Todas; 
            }
            else
            {
                if (DtGeneral.PermisosEspeciales.TienePermiso(sManejaSoloUbicacionesAlmacenaje))
                {
                    tpUbicacion = TiposDeUbicaciones.Almacenaje;
                }
            }
        }
        #endregion Permisos de Usuario

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            if (!bEsSurtimientoPedido && !bEsEDM)
            {
                btnNuevo_Click(this, null);
            }



            //////txtIdPersonal.Text = DtGeneral.IdPersonal;
            //////lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            // Determinar si se muestra la Captura de Claves Solicitadas 
            lblMensajes.Text = "<F5>Ver Información Adicional de Venta                                                                               <F7> Ver Lotes por Artículo";
            //if (GnFarmacia.CapturaDeClavesSolicitadasHabilitada)
            if(bCapturaDeClavesSolicitadasHabilitada)
            {
                lblMensajes.Text = "<F5>Ver Información Adicional de Venta      <F9>Ver Captura de Claves Solicitadas      <F7> Ver Lotes por Artículo";
                lblMensajes.Text = "<F5> Ver Información Adicional de Venta                      <F9> Ver Captura de Claves Solicitadas                      <F7> Ver Lotes por Artículo "; 
            }

            tmSesion.Enabled = true;
            tmSesion.Start();

            if (bEsEDM)
            {
                SendKeys.Send("{TAB}");
                btnCodificacion.Enabled = false;
            }
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;

                case Keys.N:
                    if (btnNuevo.Enabled)
                    {
                        btnNuevo_Click(null, null);
                    }
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                    {
                        btnImprimir_Click(null, null);
                    }
                    break;

                case Keys.F8:
                    //////GnFarmacia.ForzarCapturaEnMultiplosDeCajas = !GnFarmacia.ForzarCapturaEnMultiplosDeCajas;
                    //Modificar_CFG__ForzarCapturaEnMultiplosDeCajas();
                    break;

                case Keys.F10:
                    //    GnFarmacia.INT_OPM_ManejaOperacionMaquila = !GnFarmacia.INT_OPM_ManejaOperacionMaquila; 
                    //Modificar_CFG__INT_OPM_ManejaOperacionMaquila();
                    break;

                ////case Keys.F4:
                ////    VentasDispensacion.FrmPDD_01_Dispensacion f = new VentasDispensacion.FrmPDD_01_Dispensacion();
                ////    f.Show();
                ////    break;

                case Keys.F5:
                    ////if (DtGeneral.EsEquipoDeDesarrollo)
                    ////{
                    ////    DllFarmaciaSoft.FrmDecodificacionSNK fx = new DllFarmaciaSoft.FrmDecodificacionSNK();
                    ////    fx.ShowDialog();
                    ////}
                    break;

                default:
                    break;
            }
        }

        private void FrmVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                #region Teclas Standar
                //case Keys.F3:
                //    if (btnNuevo.Enabled)
                //        btnNuevo_Click(null, null);
                //    break;

                //case Keys.F6:
                //    if (btnGuardar.Enabled)
                //        btnGuardar_Click(null, null);
                //    break;

                //case Keys.F8:
                //    if (btnCancelar.Enabled)
                //        btnCancelar_Click(null, null);
                //    break;

                //case Keys.F10:
                //    // Ejecucion de procesos 
                //    break;

                //case Keys.F12:
                //    if (btnImprimir.Enabled)
                //        btnImprimir_Click(null, null);
                //    break;
                #endregion Teclas Standar

                case Keys.F5:
                    MostrarInfoVenta(false);
                    break;

                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                case Keys.F9:
                    MostrarCapturaDeClavesRequeridas();
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

        #region Configuraciones especiales 
        private void Modificar_CFG__ForzarCapturaEnMultiplosDeCajas()
        {
            if (General.EsEquipoDeDesarrollo)
            {
                GnFarmacia.ForzarCapturaEnMultiplosDeCajas = !GnFarmacia.ForzarCapturaEnMultiplosDeCajas; 
            }
        }

        private void Modificar_CFG__INT_OPM_ManejaOperacionMaquila()
        {
            if (General.EsEquipoDeDesarrollo)
            {
                GnFarmacia.INT_OPM_ManejaOperacionMaquila = !GnFarmacia.INT_OPM_ManejaOperacionMaquila; 
            }
        }
        #endregion Configuraciones especiales

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
            FrmFechaSistema Fecha = new FrmFechaSistema();
            Fecha.ValidarFechaSistema();

            GnFarmacia.ValidarSesionUsuario = true; 
            if (Fecha.Exito)
            {
                ////GnFarmacia.Parametros.CargarParametros();
                Fecha.Close();

                Sesion = new FrmIniciarSesionEnCaja();
                Sesion.VerificarSesion();

                if (!Sesion.AbrirVenta)
                {
                    this.Close();
                }
                else
                {
                    Sesion.Close();
                    Sesion = null;
                    //////if (!bEsSurtimientoPedido && !bEsEDM)
                    //////{
                    //////    btnNuevo_Click(null, null);
                    //////}
                }
            }
            else
            {
                this.Close();
            }
        }

        #region Botones 
        private void IniciarToolBar()
        {
            switch (tpSurtido)
            {
                case TipoDeSurtido.Mediaccess:
                    IniciarToolBar(false, false, false, false);
                    break;

                case TipoDeSurtido.Intermed:
                    IniciarToolBar(true, false, false, false);
                    break;
            }
             
        }
 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Vale)
        {
            //// Controlar en base al boton de Guardar 
            //if (txtFolio.Text.Trim() != "*" && !bEsEDM)
            //{
            //    btnCodificacion.Enabled = Guardar;
            //}

            btnGuardar.Enabled = Guardar; 
            btnCancelar.Enabled = Cancelar; 
            btnImprimir.Enabled = Imprimir; 
            btnVale.Enabled = Vale; 
            btnRecetasElectronicas.Enabled = false; 
            btnRecetaElectronica_Claves.Enabled = false; 


            switch (tpSurtido)
            {
                case TipoDeSurtido.Mediaccess:
                    if (!bCierreAutomatico)
                    {
                        btnGuardar.Enabled = false;
                    }
                    break;

                case TipoDeSurtido.Intermed:
                    break;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bContinua = false;
            bEsIdProducto_Ctrl = false; 
            sFolioSolicitud = "";
            bGeneroVale = false;
            sFolioVale = "";

            // Quitar los ToolTips 
            toolTip.SetToolTip(lblCte, "");
            toolTip.SetToolTip(lblSubCte, "");
            toolTip.SetToolTip(lblPro, "");
            toolTip.SetToolTip(lblSubPro, "");

            bEsSeguroPopular = false; 
            bFolioGuardado = false;
            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

            myGrid.BloqueaColumna(false, 1); 
            myGrid.Limpiar(false);

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;             
            dtpFechaDeSistema.Enabled = false;
            dtpFechaDeSistema.Value = GnFarmacia.FechaOperacionSistema;


            ////txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            txtSubTotal_Gravado.Enabled = false;
            txtSubTotal_NoGravado.Enabled = false;
            txtIVA.Enabled = false;
            txtImporte_Factura.Enabled = false;

            fSubTotal_Gravado = 0;
            fSubTotal_SinGravar = 0;
            fIva = 0;
            fTotal = 0;
            txtSubTotal_Gravado.Text = fSubTotal_Gravado.ToString(sFormato);
            txtSubTotal_NoGravado.Text = fSubTotal_SinGravar.ToString(sFormato);
            txtIVA.Text = fIva.ToString(sFormato);
            txtImporte_Factura.Text = fTotal.ToString(sFormato);
            dPorcentaje_Descuento___SocioComercial_Asociado = 0;

            CambiaEstado(true);
            fSubTotalIva_0 = 0; fSubTotal = 0; fIva = 0; fTotal = 0;
            dDescuentoCopago = 0;
            dTotal_a_Pagar = 0;

            txtTotal.Enabled = false;
            txtDescuentoCopago.Enabled = false;
            txtTotal_a_Pagar.Enabled = false; 

            ////txtIdPersonal.Text = DtGeneral.IdPersonal;
            ////lblPersonal.Text = DtGeneral.NombrePersonal;

            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario);
            Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion;
            Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia; 

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            InfCveSolicitadas = new clsClavesSolicitadas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            switch (tpSurtido)
            {
                case TipoDeSurtido.Mediaccess:
                    IniciarToolBar(false, false, false, false);
                    break;

                case TipoDeSurtido.Intermed:
                    IniciarToolBar(true, false, false, false);
                    break;
            }

            chkTipoImpresion.Visible = false;
            chkTipoImpresion.Checked = true; 
            chkMostrarImpresionEnPantalla.Checked = false;
            chkMostrarPrecios.Visible = false;
            chkMostrarPrecios.Checked = false; 

            if (DtGeneral.EsAlmacen)
            {
                chkTipoImpresion.Visible = true;
                chkTipoImpresion.Checked = true;
                chkMostrarImpresionEnPantalla.Checked = true;
                chkMostrarPrecios.Visible = true; 
            }

            lblCopago_Title.Visible = bCierreAutomatico;
            lblCopago.Visible = bCierreAutomatico; 
            if (bCierreAutomatico)
            {
                CargarInformacion_Eligibilidad();
            }
            else
            {
                if (!bEsVentaAsociados)
                {
                    switch (tpSurtido)
                    {
                        case TipoDeSurtido.Mediaccess:
                            txtFolio.Enabled = true;
                            txtCte.Enabled = false;
                            txtSubCte.Enabled = false;
                            txtPro.Enabled = false;
                            txtSubPro.Enabled = false;

                            break;

                        case TipoDeSurtido.Intermed:
                            txtFolio.Text = "*";
                            txtFolio.Enabled = false;
                            txtCte.Enabled = true;
                            txtSubCte.Enabled = true;
                            txtPro.Enabled = true;
                            txtSubPro.Enabled = true;
                            txtCte.Focus();
                            break;
                    }
                }
            }
            grdProductos.Focus();

            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico) RecetaElectronica.Receta.Reset(); 
            txtFolio.Focus();
        }

        private void CargarInformacion_Eligibilidad()
        {
            if (!bEsVentaAsociados)
            {
                switch (tpSurtido)
                {
                    case TipoDeSurtido.Mediaccess:
                        CargarInformacion_Eligibilidad___Mediaccess();
                        break;

                    case TipoDeSurtido.Intermed:
                        CargarInformacion_Eligibilidad___Intermed();
                        break;
                }
            }
        }

        private void CargarInformacion_Eligibilidad___Intermed()
        {
            txtFolio.Enabled = false;
            //txtCte.Enabled = false;
            //txtSubCte.Enabled = false;
            //txtPro.Enabled = false;
            //txtSubPro.Enabled = false;

            lblCopago.Text = "localVale.TituloCopago";
            txtFolio.Text = "*";
            //txtCte.Text = localVale.IdCliente;
            //txtCte_Validating(null, null);

            //txtSubCte.Text = localVale.IdSubCliente;
            //txtSubCte_Validating(null, null);

            //txtPro.Text = localVale.IdPrograma;
            //txtPro_Validating(null, null);
            //txtSubPro.Text = localVale.IdSubPrograma;
            //txtSubPro_Validating(null, null);


            btnGuardar.Enabled = true;
        }

        private void CargarInformacion_Eligibilidad___Mediaccess()
        {
            txtFolio.Enabled = false;
            txtCte.Enabled = false;
            txtSubCte.Enabled = false;
            txtPro.Enabled = false;
            txtSubPro.Enabled = false;

            lblCopago.Text = localElegibilidad.TituloCopago; 
            txtFolio.Text = "*";
            txtCte.Text = localElegibilidad.IdCliente;
            txtCte_Validating(null, null); 

            txtSubCte.Text = localElegibilidad.IdSubCliente;
            txtSubCte_Validating(null, null); 

            txtPro.Text = localElegibilidad.IdPrograma;
            txtPro_Validating(null, null);
            txtSubPro.Text = localElegibilidad.IdSubPrograma;
            txtSubPro_Validating(null, null);

            CargaDetalles_RecetaManual(); 

            btnGuardar.Enabled = true; 
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;
            bool bExito = false; 
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            if (txtFolio.Text != "*")
            {
                General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                if (ValidaDatos())
                {
                    if (!con.Abrir())
                    {
                        ///Error.LogError(con.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else 
                    {
                        IniciarToolBar(); 
                        con.IniciarTransaccion();

                        if (GuardaVenta())
                        {
                            bExito = true;
                            if (bImplementaCodificacion)
                            {
                                if (!CodificacionSNK.Guardar_UUIDS_Movimientos_De_Inventario(sFolioMovtoInv, leer, Lotes, true, true))
                                {
                                    bExito = false;
                                }
                            }

                            if (bEsEDM && bExito)
                            {
                                bExito = GuadarFolioVenta();
                            }

                            if (bExito)
                            {
                                bExito = AfectarExistencia(true, false);
                            }
                        }

                        if (RecetaElectronica.Receta.InformacionCargada)
                        {
                            bExito = RecetaElectronica.Receta.RegistrarAtencion(leer, sFolioVenta);
                        }

                        if (bExito)
                        {
                            if (tpSurtido == TipoDeSurtido.Intermed)
                            {
                                if (bExito)
                                {
                                    bExito = ValidarInformacion();
                                }
                            }
                        }

                        ////////// Atencion de pedidos especiales 
                        //////if (bExito && bEsSurtimientoPedido)
                        //////{
                        //////    bExito = ActualizarEstatusPedido();

                        //////    if (bExito)
                        //////    {
                        //////        bExito = RevisarPedidoCompleto();

                        //////        if (bExito)
                        //////        {
                        //////            bExito = RegistrarAtencion();
                        //////        }
                        //////    }
                        //////} 

                        if (bExito && !bActivarProceso)
                        {
                            con.CompletarTransaccion();
                            
                            //////// IMach  // Enlazar el folio de inventario 
                            //////IMachPtoVta.TerminarSolicitud(sFolioMovtoInv);


                            IniciarToolBar(false, false, true, false);

                            txtFolio.Text = sFolioVenta;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            ImprimirInformacion();

                            /////// Jesús Díaz 2K120516.1305 
                            //// Solo farmacias configuradas para Emision de Vales
                            //// Se forza la generación automatica del Vale 
                            if (bEmiteVales)
                            {
                                GenerarValeAutomatico();
                            }
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*";

                            if (!bActivarProceso)
                            {
                                Error.GrabarError(leer, "btnGuardar_Click");
                                General.msjError("Ocurrió un error al guardar la información.");
                            }
                            else
                            {
                                FrmIncidencias f = new FrmIncidencias("Incidencias encontradas", leer.DataSetClase);
                                f.ShowDialog(this);
                            }

                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bGeneroVale); 
                        }
                        con.Cerrar();

                        if (bExito)
                        {
                            if (bCierreAutomatico && !bActivarProceso)
                            {
                                this.Hide();
                            }
                        }
                    }
                }
            }
        }

        private bool GuardaVenta()
        {
            bool bRegresa = true; 
            string sSql = "", sCaja = GnFarmacia.NumCaja;
            int iOpcion = 1;

            // Asignar el valor a la variable global 
            sFolioVenta = txtFolio.Text;

            CalcularTotales();

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @FechaSistema = '{4}', @IdCaja = '{5}', " + 
                " @IdPersonal = '{6}', @IdCliente = '{7}', @IdSubCliente = '{8}', @IdPrograma = '{9}', @IdSubPrograma = '{10}', " + 
                " @SubTotal = '{11}', @Iva = '{12}', @Total = '{13}', @TipoDeVenta = '{14}', @iOpcion = '{15}', @Descuento = '{16}' ", 
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 
                sFolioVenta,
                sFechaSistema, Fg.PonCeros(sCaja, 2), DtGeneral.IdPersonal, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4), 
                Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4),
                Redondear(fSubTotal_Gravado + fSubTotal_SinGravar), Redondear(fIva), Redondear(fTotal), (int)TipoDeVenta.Credito, iOpcion, Redondear(dDescuentoCopago)); 


            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }
            else 
            {
                if (!leer.Leer())
                {
                    bRegresa = false; 
                }
                else 
                {
                    sFolioVenta = String.Format("{0}", leer.Campo("Clave"));
                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                    // Grabar en los Movimientos de inventario
                    sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" + 
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                        "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" + 
                        "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta, sIdTipoMovtoInv, sTipoES, "", DtGeneral.IdPersonal, "",
                        Redondear(fSubTotal_Gravado + fSubTotal_SinGravar), Redondear(fIva), Redondear(fTotal), 1, ""); 
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        leer.Leer();
                        sFolioMovtoInv = leer.Campo("Folio");
                        // txtFolio.Text = sFolioVenta; // Asignar al final 
                    }
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardaVentaInformacionAdicional();
                if (bRegresa)
                {
                    bRegresa = GuardarClavesSolicitadas(); 
                }

                if (bRegresa)
                {
                    if (!bEsVentaAsociados)
                    {
                        switch(tpSurtido) 
                        {
                            case TipoDeSurtido.Mediaccess:
                                bRegresa = localElegibilidad.RegistrarAtencion_Elegibilidad(leer, localElegibilidad.Elegibilidad, 
                                    localElegibilidad.MA_FolioDeReceta, sEmpresa, sEstado, sFarmacia, sFolioVenta, sPersonal);
                                break;

                            case TipoDeSurtido.Intermed:
                                bRegresa = localVale.RegistrarAtencion_Vale(leer, localVale.IdSocioComercial, localVale.IdSucursalSocioComercial, 
                                    localVale.FolioVale, sEmpresa, sEstado, sFarmacia, sFolioVenta, sPersonal);
                                break;
                        }
                    }
                }
            }

            return bRegresa; 
        }

        private bool Guardar_FormasDePago()
        {
            bool bRegresa = true;
            string sSql = "";

            if (dTotal_a_Pagar > 0)
            {
                for (int i = 1; myGridPago.Rows >= i && bRegresa; i++) 
                {
                    sSql = string.Format("Exec spp_Mtto_INT_MA_VentasPago \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @IdFormasDePago = '{4}', \n" +
                        "\t@Importe = '{5}', @PagoCon = '{6}', @Cambio = '{7}', @Referencia = '{8}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta,
                        myGridPago.GetValue(i, (int)Cols_Pago.Codigo),
                        Redondear(myGridPago.GetValueDou(i, (int)Cols_Pago.Importe)), 
                        Redondear(myGridPago.GetValueDou(i, (int)Cols_Pago.PagoCon)), 
                        Redondear(myGridPago.GetValueDou(i, (int)Cols_Pago.Cambio)), 
                        myGridPago.GetValue(i, (int)Cols_Pago.Referencia));
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                }
            }
            else 
            {
                sSql = string.Format("Exec spp_Mtto_INT_MA_VentasPago \n" +
                   "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', @IdFormasDePago = '{4}', @Importe = '{5}', @Referencia = '{6}' ",
                   DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta, "00", 0, "Exento");
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
            }

            return bRegresa; 
        }

        private bool Guardar_InformacionPago()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_Mtto_INT_MA_Ventas_Importes \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Folio = '{3}', \n" +
                "\t@SubTotal_SinGravar = '{4}', @SubTotal_Gravado = '{5}', @DescuentoCopago = '{6}', \n" +
                "\t@Importe_SinGravar = '{7}', @Importe_Gravado = '{8}', @Iva = '{9}', @Importe_Neto = '{10}' \n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta,
                Redondear(fSubTotal_SinGravar_General), Redondear(fSubTotal_Gravado_General) + Redondear(fIva_General), 
                Redondear(dDescuentoCopago),
                Redondear(fSubTotal_SinGravar), Redondear(Redondear(fSubTotal_Gravado)), Redondear(fIva), 
                Redondear(fTotal));
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool GuardarInformacionPreciosLicitacion()
        {
            bool bRegresa = true; 
            // 2K110426-1004  
            string sSql = string.Format(" EXEC spp_Mtto_Ventas_AsignarPrecioLicitacion \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' \n", 
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta);
            bRegresa = leer.Exec(sSql);

            return bRegresa; 
        }

        private bool GuardaVentaInformacionAdicional()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_VentasInformacionAdicional \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @IdBeneficiario = '{4}', @NumReceta = '{5}', @FechaReceta = '{6}', \n" +
                "\t@IdTipoDeDispensacion = '{7}', @IdUnidadMedica = '{8}', @IdMedico = '{9}', @IdBeneficioSP = '{10}', @IdDiagnostico = '{11}', \n" +
                "\t@IdServicio = '{12}', @IdArea = '{13}', @RefObservaciones = '{14}', @iOpcion = '{15}',  @NumeroDeHabitacion = '{16}', @NumeroDeCama = '{17}', \n" +
                "\t@IdEstadoResidencia = '{18}', @IdTipoDerechoHabiencia = '{19}' \n",
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                sFolioVenta, InfVtas.Beneficiario, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                InfVtas.TipoDispensacion, InfVtas.CluesRecetasForaneas, InfVtas.Medico, InfVtas.IdBeneficio, InfVtas.Diagnostico, InfVtas.Servicio, InfVtas.Area,
                InfVtas.ReferenciaObservaciones, 1, InfVtas.NumeroDeHabitacion, InfVtas.NumeroDeCama,
                InfVtas.IdEstadoResidencia, InfVtas.IdTipoDerechoHaciencia
                );


            bRegresa = leer.Exec(sSql);

            if (bRegresa)
            {
                bRegresa = Guardar_InformacionPago();
                if (bRegresa)
                {
                    bRegresa = Guardar_FormasDePago();
                    if (bRegresa)
                    {
                        bRegresa = GuardaDetallesVenta();
                    }
                }
            }

            return bRegresa;
        }

        private void GuardaVenta_ALMJ_PedidosRC_Surtido()
        {
            int iOpcion = 1, iCantidadEntregada = 0;

            if (bContinua)
            {
                iCantidadEntregada = myGrid.TotalizarColumna((int)Cols.Cantidad);

                string sSql = string.Format(" Exec spp_Mtto_Ventas_ALMJ_PedidosRC_Surtido \n" + 
                    "\t@IdEmpresaRC = '{0}', @IdEstadoRC = '{1}', @IdJurisdiccionRC = '{2}', @IdFarmaciaRC = '{3}', \n" + 
                    "\t@FolioPedidoRC = '{4}', @IdEmpresa = '{5}', @IdEstado = '{6}', @IdFarmacia = '{7}', @FolioVenta = '{8}', \n" +
                    "\t@CantidadSurtida = '{9}', @CantidadEntregada = '{10}', @iOpcion = '{11}' \n", 
                    IdEmpresaCSGN, IdEstadoCSGN, IdJurisdiccionCSGN, IdFarmaciaCSGN, sFolioPedidoRC, sEmpresa, sEstado, sFarmacia, 
                    sFolioVenta, iCantidadSurtidaCSGN, iCantidadEntregada, iOpcion);

                if (!leer.Exec(sSql))
                {
                    bContinua = false;
                }
            }
        }

        private bool GuardaDetallesVenta()
        {
            bool bRegresa = true; 
            string sSql = "", sIdProducto = "", sCodigoEAN = "";
            int iRenglon = 0, iUnidadDeSalida = 0, iCant_Entregada = 0, iCant_Devuelta = 0, iCantVendida = 0, iOpcion = 0;
            double dCostoUnitario = 0, dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0 , dPorcDescto = 0, dImpteDescto = 0;

            iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                ////sCodigoEAN = myGrid.GetValue(i, 1);
                ////sIdProducto = myGrid.GetValue(i, 2);
                ////iCantVendida = myGrid.GetValueInt(i, 5);
                ////iCant_Entregada = myGrid.GetValueInt(i, 5);
                ////dPrecioUnitario = myGrid.GetValueDou(i, 6);
                ////dImpteIva = myGrid.GetValueDou(i, 8);
                ////dTasaIva = myGrid.GetValueDou(i, 4);

                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                iCantVendida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCant_Entregada = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dPrecioUnitario = Redondear(myGrid.GetValueDou(i, (int)Cols.Precio));
                dImpteIva = Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteIva));
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                dCostoUnitario = Redondear(myGrid.GetValueDou(i, (int)Cols.UltimoCosto));

                iRenglon = i;

                if (sIdProducto != "")
                {
                    sSql = string.Format("Set DateFormat YMD \nExec spp_Mtto_VentasDet \n" + //"  '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}' ",
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @IdProducto = '{4}', @CodigoEAN = '{5}', @Renglon = '{6}', @UnidadDeSalida = '{7}', \n" +
                        "\t@Cant_Entregada = '{8}', @Cant_Devuelta = '{9}', @CantVendida = '{10}', @CostoUnitario = '{11}', @PrecioUnitario = '{12}', @ImpteIva = '{13}', @TasaIva = '{14}', \n" +
                        "\t@PorcDescto = '{15}', @ImpteDescto = '{16}', @iOpcion = '{17}' \n",
                        DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta, Fg.PonCeros(sIdProducto, 8),
                        sCodigoEAN, iRenglon, iUnidadDeSalida, iCant_Entregada, iCant_Devuelta, iCantVendida, dCostoUnitario,
                        dPrecioUnitario, dImpteIva, dTasaIva, dPorcDescto, dImpteDescto, iOpcion);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        // Grabar en los Movimientos de inventario 
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" + //" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', \n" +
                            "\t@TasaIva = '{7}', @Cantidad = '{8}', @Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovtoInv, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            dTasaIva, iCantVendida, dCostoUnitario, (iCantVendida * dCostoUnitario), 'A');

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        } 
                    }
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardaVentasDet_Lotes(); 
            }

            return bRegresa; 
        }

        private bool GuardaVentasDet_Lotes()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";  // , sClaveLote = "";
            int iRenglon = 0, iCantVendida = 0, iOpcion = 0;
            double dCostoUnitario = 0; // , dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0, dPorcDescto = 0, dImpteDescto = 0;

            iOpcion = 1;            

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                iCantVendida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dCostoUnitario = Redondear(myGrid.GetValueDou(i, (int)Cols.UltimoCosto));

                iRenglon = i;                   
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

                foreach (clsLotes L in ListaLotes)
                {
                    if (sIdProducto != "" && L.Cantidad > 0)
                    {
                        sSql = String.Format("Set DateFormat YMD \nExec spp_Mtto_VentasDet_Lotes \n" + // "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioVenta = '{4}', \n" +
                            "\t@IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Renglon = '{8}', @CantidadVendida = '{9}', @iOpcion = '{10}', @SKU = '{11}' \n",
                            DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                            L.IdSubFarmacia, sFolioVenta, Fg.PonCeros(sIdProducto, 8),
                            sCodigoEAN, L.ClaveLote, iRenglon, L.Cantidad, iOpcion, L.SKU);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            // Grabar en los Movimientos de inventario 
                            sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" + //" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', \n" +
                                "\t@Cantidad = '{8}', @Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                                DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv, sIdProducto, sCodigoEAN, 
                                L.ClaveLote, L.Cantidad, dCostoUnitario, (L.Cantidad * dCostoUnitario), 'A', L.SKU);

                            if (!leer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                            else
                            {
                                if (GnFarmacia.ManejaUbicaciones)
                                {
                                    bRegresa = GuardaVentasDet_Lotes_Ubicaciones(L, iRenglon, iOpcion);
                                }
                            }
                        }
                    }
                }
            } 

            return bRegresa; 
        }

        private bool GuardaVentasDet_Lotes_Ubicaciones(clsLotes Lote, int Renglon, int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_VentasDet_Lotes_Ubicaciones \n" + //" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}' ",
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioVenta = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', \n" +
                        "\t@Renglon = '{8}', @IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepaño = '{11}', @CantidadVendida = '{12}', @iOpcion = '{13}', @SKU = '{14}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioVenta, L.IdProducto, L.CodigoEAN,
                        L.ClaveLote, Renglon.ToString(), L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, iOpcion, L.SKU);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }                                            
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones " + // " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', \n" +
                            "\t@Cantidad = '{8}', @IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv,
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', L.SKU);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;

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
                    sSql = string.Format(" Exec spp_Mtto_VentasClavesSolicitadas \n" + 
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', \n" +
                        "\t@IdClaveSSA = '{4}', @CantidadRequerida = '{5}', @Observaciones = '{6}' \n",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta, sIdClaveSSA, iCantidad, InfCveSolicitadas.Observaciones);

                    if (!leer.Exec(sSql))
                    {
                        bContinua = false; 
                        bRegresa = false;
                        break;
                    }
                }

                if (bRegresa)
                {
                    bRegresa = GuardarInformacionPreciosLicitacion();
                    if (bRegresa)
                    {
                        bRegresa = CalcularSurtimientoClavesSolicitadas();
                    }
                }
            }

            return bRegresa; 
        }

        private bool CalcularSurtimientoClavesSolicitadas()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta );
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            bContinua = bRegresa; 
            return bRegresa;  
        }

        private void GenerarValeAutomatico()
        {
            if (ValidarClaves_CB())
            {
                FrmGeneracionVales F = new FrmGeneracionVales();
                F.GenerarValeAutomaticamente(sEmpresa, sEstado, sFarmacia, sFolioVenta, DtGeneral.IdPersonal, DtGeneral.NombrePersonal);
            }
        }

        private bool ValidarClaves_CB()
        {
            bool bRegresa = false; 

            string sSql =
                string.Format(
                "Select ClaveSSA, IdClaveSSA, DescripcionSal, IdPresentacion, Presentacion, " +
                " cast(V.CantidadRequerida as int) as Cantidad, " +
                "dbo.fg_Existencia_Clave(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.ClaveSSA) as ExistenciaClave " +
                "From vw_Impresion_Ventas_ClavesSolicitadas V (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' and EsCapturada = 1 and Clave_CB = 1 ",
                sEmpresa, sEstado, sFarmacia, sFolioVenta );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarClaves_CB()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    bRegresa = true;
                    General.msjUser("Se generará el Vale de forma automática para el Ticket de venta generado."); 
                }
            }

            return bRegresa; 
        } 

        private bool ObtenerVale()  
        {
            bool bRegresa = true;
            ////string sSql = "", sVale = "*" ;
            ////int iOpcion = 1;

            ////if (bContinua)
            ////{
            ////    if (bEmiteVales && RequiereVale())
            ////    {
            ////        sSql = string.Format(" Exec spp_Mtto_Ventas_Vales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
            ////            "'{6}', '{7}' ",
            ////            DtGeneral.EmpresaConectada, sEstado, sFarmacia, sVale, sFolioVenta, sFechaSistema, txtIdPersonal.Text, iOpcion);

            ////        if (leer.Exec(sSql))
            ////        {
            ////            if (leer.Leer())
            ////            {
            ////                sFolioVale = String.Format("{0}", leer.Campo("Clave"));
            ////                bGeneroVale = true; //Esta variable se utiliza para la impresion del Vale.
            ////            }
            ////        }
            ////        else
            ////        {
            ////            bRegresa = false;
            ////        }                    
            ////    }
            ////}

            bContinua = bRegresa;
            return bRegresa;
        }

        private double Redondear(double Valor)
        {
            return Redondear(Valor, 2);
        }

        private double Redondear(double Valor, int Decimales)
        {
            double dRedondear = 0;

            try
            {
                dRedondear = Math.Round(Valor, Decimales);
            }
            catch
            {
            }

            return dRedondear;
        }

        private void CalcularTotales()
        {
            if (bEsVentaAsociados)
            {
                if (bEsDispensacionDeConsignacion)
                {
                    CalcularTotales___SocioComercial_Consignacion();
                }
                else
                {
                    CalcularTotales___SocioComercial_Asociado();
                }
            }
            else
            {
                switch (tpSurtido)
                {
                    case TipoDeSurtido.Mediaccess:
                        CalcularTotales___Mediaccess();
                        break;

                    case TipoDeSurtido.Intermed:
                        CalcularTotales___Intermed();
                        break;
                }
            }
        }

        private void CalcularTotales___SocioComercial_Consignacion()
        {
            fSubTotal_Gravado = 0;
            fSubTotal_SinGravar = 0;
            fIva = 0;
            fTotal = 0;
            dTotal_a_Pagar = 0;


            txtSubTotal_Gravado.Text = Redondear(fSubTotal_Gravado).ToString(sFormato);
            txtSubTotal_NoGravado.Text = Redondear(fSubTotal_SinGravar).ToString(sFormato);
            txtIVA.Text = Redondear(fIva).ToString(sFormato);
            txtImporte_Factura.Text = Redondear(fTotal).ToString(sFormato);

            txtTotal.Text = fTotal_General.ToString(sFormato_04);
            txtDescuentoCopago.Text = dDescuentoCopago.ToString(sFormato_04);
            txtTotal_a_Pagar.Text = dTotal_a_Pagar.ToString(sFormato_04);

        }

        private void CalcularTotales___SocioComercial_Asociado()
        {
            double sSubTotal = 0;
            double dIva_Grid = 0;
            double dPorcentaje = 0;
            int iRedondeos = 2;

            sSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);

            fSubTotalIva_0 = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fIva = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(8), 2);
            fTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(9), 2);


            fSubTotal_SinGravar = 0;
            fSubTotal_Gravado = 0;
            fIva = 0; 
            //fSubTotalIva_0 = 0;
            //fSubTotal = 0;
            //fIva = 0;
            //fTotal = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                dIva_Grid = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                if (dIva_Grid == 0)
                {
                    fSubTotal_SinGravar += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                }
                else
                {
                    fSubTotal_Gravado += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                    fIva += Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteIva), iRedondeos);
                }
            }
            /*
            dPorcentaje = Redondear(((100 - dPorcentaje_Aplicar) / 100.00));

            /// Ejemplo 30 % 
            dDescuentoCopago = Redondear(fTotal * ((100 - dPorcentaje_Aplicar) / 100.00), iRedondeos); // Mediaccess paga el 70% | Cliente paga el 30%   
            ////dDescuentoCopago = DtGeneral.Redondear(fTotal * ((Copago ) / 100.00), 2); // Mediaccess paga el 30% | Cliente paga el 70% 

            dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);


            //// Totalizar 
            fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
            fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
            fIva = Redondear(fIva - (fIva * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);

            fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);
            */


            dPorcentaje_Descuento___SocioComercial_Asociado = 0; /// Solo para empleados 
                                                                 /// tomar los precios calculados en base a Margen Minimo de Ganancia 

            dPorcentaje = dPorcentaje_Descuento___SocioComercial_Asociado; /// Redondear(((100 - Copago ) / 100.00));


            /// Ejemplo 100 % 
            dDescuentoCopago = Redondear(fTotal * ((dPorcentaje) / 100.00), iRedondeos);
            dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);

            fSubTotal_SinGravar_General = fSubTotal_SinGravar;
            fSubTotal_Gravado_General = fSubTotal_Gravado;
            fIva_General = fIva;
            fTotal_General = fTotal;

            //// Totalizar 
            fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((dPorcentaje) / 100.00)), iRedondeos);
            fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((dPorcentaje) / 100.00)), iRedondeos);
            fIva = Redondear(fIva - (fIva * ((dPorcentaje) / 100.00)), iRedondeos);

            fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);


            txtSubTotal_Gravado.Text = Redondear(fSubTotal_Gravado).ToString(sFormato);
            txtSubTotal_NoGravado.Text = Redondear(fSubTotal_SinGravar).ToString(sFormato);
            txtIVA.Text = Redondear(fIva).ToString(sFormato);
            txtImporte_Factura.Text = Redondear(fTotal).ToString(sFormato);



            dTotal_a_Pagar = Redondear(fTotal_General - dDescuentoCopago, iRedondeos);
            txtTotal.Text = fTotal_General.ToString(sFormato_04);
            txtDescuentoCopago.Text = dDescuentoCopago.ToString(sFormato_04);
            txtTotal_a_Pagar.Text = dTotal_a_Pagar.ToString(sFormato_04);

        }

        private void CalcularTotales___Intermed()
        {
            double sSubTotal = 0;
            double dIva_Grid = 0;
            double dPorcentaje = 0;
            int iRedondeos = 2;

            sSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);

            fSubTotalIva_0 = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fIva = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(8), 2);
            fTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(9), 2);


            fSubTotal_SinGravar = 0;
            fSubTotal_Gravado = 0;
            //fSubTotalIva_0 = 0;
            //fSubTotal = 0;
            //fIva = 0;
            //fTotal = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                dIva_Grid = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                if (dIva_Grid == 0)
                {
                    fSubTotal_SinGravar += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                }
                else
                {
                    fSubTotal_Gravado += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                    fIva += Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteIva), iRedondeos);
                }
            }
            /*
            dPorcentaje = Redondear(((100 - dPorcentaje_Aplicar) / 100.00));

            /// Ejemplo 30 % 
            dDescuentoCopago = Redondear(fTotal * ((100 - dPorcentaje_Aplicar) / 100.00), iRedondeos); // Mediaccess paga el 70% | Cliente paga el 30%   
            ////dDescuentoCopago = DtGeneral.Redondear(fTotal * ((Copago ) / 100.00), 2); // Mediaccess paga el 30% | Cliente paga el 70% 

            dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);


            //// Totalizar 
            fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
            fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
            fIva = Redondear(fIva - (fIva * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);

            fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);
            */

            dPorcentaje = 100; /// Redondear(((100 - Copago ) / 100.00));

            /// Ejemplo 100 % 
            dDescuentoCopago = Redondear(fTotal * ((100 - 0) / 100.00), iRedondeos); 
            dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);

            fSubTotal_SinGravar_General = fSubTotal_SinGravar;
            fSubTotal_Gravado_General = fSubTotal_Gravado;
            fIva_General = fIva;
            fTotal_General = fTotal;

            //// Totalizar 
            fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - 0) / 100.00)), iRedondeos);
            fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - 0) / 100.00)), iRedondeos);
            fIva = Redondear(fIva - (fIva * ((100 - 0) / 100.00)), iRedondeos);

            fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);


            txtSubTotal_Gravado.Text = Redondear(fSubTotal_Gravado).ToString(sFormato);
            txtSubTotal_NoGravado.Text = Redondear(fSubTotal_SinGravar).ToString(sFormato);
            txtIVA.Text = Redondear(fIva).ToString(sFormato);
            txtImporte_Factura.Text = Redondear(fTotal).ToString(sFormato);



            dTotal_a_Pagar = Redondear(fTotal_General - dDescuentoCopago, iRedondeos);
            txtTotal.Text = fTotal_General.ToString(sFormato_04);
            txtDescuentoCopago.Text = dDescuentoCopago.ToString(sFormato_04);
            txtTotal_a_Pagar.Text = dTotal_a_Pagar.ToString(sFormato_04);

        }

        private void CalcularTotales___Mediaccess()
        {
            double sSubTotal = 0;
            double dIva_Grid = 0;
            double dPorcentaje = 0;
            int iRedondeos = 2; 

            //fSubTotalIva_0 = 0;
            //fSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            //fIva = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva);
            //fTotal = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);

            fSubTotal = 0;
            fIva = 0;
            fTotal = 0;

            sSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);

            fSubTotalIva_0 = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fSubTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(7), 2);
            fIva = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(8), 2);
            fTotal = DtGeneral.Redondear(myGrid.TotalizarColumnaDou(9), 2);


            fSubTotal_SinGravar = 0;
            fSubTotal_Gravado = 0;
            fSubTotalIva_0 = 0;
            fSubTotal = 0;
            fIva = 0;
            fTotal = 0;


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                dIva_Grid = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                if (dIva_Grid == 0)
                {
                    fSubTotal_SinGravar += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                }
                else
                {
                    fSubTotal_Gravado += Redondear(myGrid.GetValueDou(i, (int)Cols.Importe), iRedondeos);
                    fIva += Redondear(myGrid.GetValueDou(i, (int)Cols.ImporteIva), iRedondeos);
                }
            }

            //// Totalizar 
            fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);

            fSubTotal_SinGravar_General = fSubTotal_SinGravar;
            fSubTotal_Gravado_General = fSubTotal_Gravado;
            fIva_General = fIva;
            fTotal_General = fTotal;

            if (localElegibilidad != null)
            {
                if (TipoCop == TipoDeCopago.Monto)
                {
                    dDescuentoCopago = Copago;
                    dDescuentoCopago = DtGeneral.Redondear(fTotal - dDescuentoCopago);

                    ///dTotal_a_Pagar = Copago ;

                    if (dDescuentoCopago < 0)
                    {
                        dDescuentoCopago = 0;
                    }

                    ////dTotal_a_Pagar = DtGeneral.Redondear(fTotal - dDescuentoCopago, 2);

                    dPorcentaje = Redondear(dDescuentoCopago / fTotal) * 100;
                    dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);


                    //// Totalizar 
                    fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * (dPorcentaje / 100.00)), iRedondeos);
                    fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * (dPorcentaje / 100.00)), iRedondeos);
                    fIva = Redondear(fIva - (fIva * (dPorcentaje / 100.00)), iRedondeos);

                    fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);
                }
                else
                {
                    dPorcentaje = Redondear(((100 - Copago) / 100.00));

                    /// Ejemplo 30 % 
                    dDescuentoCopago = Redondear(fTotal * ((100 - Copago ) / 100.00), iRedondeos); // Mediaccess paga el 70% | Cliente paga el 30%   
                    ////dDescuentoCopago = DtGeneral.Redondear(fTotal * ((Copago ) / 100.00), 2); // Mediaccess paga el 30% | Cliente paga el 70% 

                    dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);


                    //// Totalizar 
                    fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - Copago ) / 100.00)), iRedondeos);
                    fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - Copago ) / 100.00)), iRedondeos);
                    fIva = Redondear(fIva - (fIva * ((100 - Copago ) / 100.00)), iRedondeos);

                    fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);
                }


                //////dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);

                ////////// Totalizar 
                //////fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - Copago ) / 100.00)), iRedondeos);
                //////fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - Copago ) / 100.00)), iRedondeos);
                //////fIva = Redondear(fIva - (fIva * ((100 - Copago ) / 100.00)), iRedondeos);

                //////fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);

            }
            else
            {
                dPorcentaje = Redondear(((100 - dPorcentaje_Aplicar) / 100.00));

                /// Ejemplo 30 % 
                dDescuentoCopago = Redondear(fTotal * ((100 - dPorcentaje_Aplicar) / 100.00), iRedondeos); // Mediaccess paga el 70% | Cliente paga el 30%   
                ////dDescuentoCopago = DtGeneral.Redondear(fTotal * ((Copago ) / 100.00), 2); // Mediaccess paga el 30% | Cliente paga el 70% 

                dTotal_a_Pagar = Redondear(fTotal - dDescuentoCopago, iRedondeos);


                //// Totalizar 
                fSubTotal_SinGravar = Redondear(fSubTotal_SinGravar - (fSubTotal_SinGravar * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
                fSubTotal_Gravado = Redondear(fSubTotal_Gravado - (fSubTotal_Gravado * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);
                fIva = Redondear(fIva - (fIva * ((100 - dPorcentaje_Aplicar) / 100.00)), iRedondeos);

                fTotal = Redondear(fSubTotal_SinGravar + (fSubTotal_Gravado + fIva), iRedondeos);
            }



            txtSubTotal_Gravado.Text = Redondear(fSubTotal_Gravado).ToString(sFormato);
            txtSubTotal_NoGravado.Text = Redondear(fSubTotal_SinGravar).ToString(sFormato);
            txtIVA.Text = Redondear(fIva).ToString(sFormato);
            txtImporte_Factura.Text = Redondear(fTotal).ToString(sFormato);



            dTotal_a_Pagar = Redondear(fTotal_General - dDescuentoCopago, iRedondeos);
            txtTotal.Text = fTotal_General.ToString(sFormato_04);
            txtDescuentoCopago.Text = dDescuentoCopago.ToString(sFormato_04);
            txtTotal_a_Pagar.Text = dTotal_a_Pagar.ToString(sFormato_04);


            ////fSubTotalIva_0 = DtGeneral.Redondear(fSubTotalIva_0 - dDescuentoCopago, 2);
            ////fSubTotal = DtGeneral.Redondear(fSubTotalIva_0 - dDescuentoCopago, 2);
            ////fTotal = DtGeneral.Redondear(fTotal - dDescuentoCopago, 2);
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovtoInv, (int)Inv, (int)Costo);

            bool bRegresa = false;
            if (leer.Exec(sSql))
            {
                bRegresa = true; 
            }

            bContinua = bRegresa;

            if (DtGeneral.ConfirmacionConHuellas && bRegresa && DtGeneral.EsAlmacen)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioMovtoInv);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(con, sFolioMovtoInv);
            }

            return bRegresa;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void btnRecetasElectronicas_Click(object sender, EventArgs e)
        {
            btnRecetaElectronica_Claves.Enabled = false;
            myGrid.Limpiar();

            InfVtas = new clsInformacionVentas(sEmpresa, sEstado, sFarmacia);


            if (RecetaElectronica.Receta.SeleccionarRecetasParaSurtido(txtCte.Text.Trim(), txtSubCte.Text.Trim()))
            {
                btnRecetaElectronica_Claves.Enabled = true;
                sListaClavesSSA_RecetaElectronica = RecetaElectronica.Receta.ListaClavesSSA_Extended;
                
                if (RecetaElectronica.Receta.Interface == ExpedienteElectronico_Interface.AMPM)
                {
                    //bEsVentaAsociados = RecetaElectronica.Receta.ListaClavesSSA_Extended.Length > 0 ? false:bEsVentaAsociados;
                    btnRecetaElectronica_Claves.Enabled = bEsVentaAsociados;
                    btnRecetaElectronica_Claves.Visible = bEsVentaAsociados;
                }

                MostrarInfoVenta(true);

                RecetaElectronica.Receta.MostrarDetallesDeReceta(this.MdiParent);
                myGrid.Limpiar(true);
            }
        }

        private void btnRecetaElectronica_Claves_Click(object sender, EventArgs e)
        {
            if (RecetaElectronica.Receta.InformacionCargada)
            {
                RecetaElectronica.Receta.MostrarDetallesDeReceta(this.MdiParent);
            }
        }

        private void ImprimirInformacion() 
        {
            sFolioVenta = Fg.PonCeros(txtFolio.Text, 8); 
            VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
            VtasImprimir.MostrarCantidadConLetra = true;
            VtasImprimir.NumeroDeCopias = iNumeroDeCopias; 
            //VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket; 

            if (DtGeneral.EsAlmacen)
            {
                VtasImprimir.MostrarImpresionDetalle = chkTipoImpresion.Checked;
                VtasImprimir.MostrarPrecios = chkMostrarPrecios.Checked; 
            }

            if (VtasImprimir.Imprimir(sFolioVenta, dTotal_a_Pagar))
            {
                if (bGeneroVale)
                {
                    if (VtasImprimir.ImprimirVale(sFolioVale))
                    {
                        btnNuevo_Click(null, null);
                    }
                }
                else
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        private void btnVale_Click(object sender, EventArgs e)
        {
            ////if (bGeneroVale)
            ////{
            ////    VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
            ////    if (VtasImprimir.ImprimirVale(sFolioVale))
            ////    {
            ////        btnNuevo_Click(null, null);
            ////    }

            ////}
        }

        #endregion Botones

        #region Validaciones
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sFolio = "";  // sSql = "", 
            bFolioGuardado = false;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                //if (!bCierreAutomatico)
                //{
                //    txtFolio.Focus();
                //    e.Cancel = true; 
                //}
                //else 
                {
                    IniciarToolBar(true, false, false, false);
                    txtFolio.Text = "*";
                    txtFolio.Enabled = false;
                    txtCte.Focus();
                }
            }
            else
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Folio de venta no encontrado, Verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") != TipoDeVenta.Credito)
                    {
                        General.msjUser("Folio de venta capturado no es de crédito, Verifique.");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                    else
                    {
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true, false);
                        sFolio = leer.Campo("Folio");
                        sFolioVenta = sFolio;
                        txtFolio.Text = sFolio;

                        dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");  

                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = leer.Campo("IdSubCliente");
                        lblSubCte.Text = leer.Campo("NombreSubCliente"); 
                        txtPro.Text = leer.Campo("IdPrograma");
                        lblPro.Text = leer.Campo("Programa");
                        txtSubPro.Text = leer.Campo("IdSubPrograma");
                        lblSubPro.Text = leer.Campo("SubPrograma");

                        toolTip.SetToolTip(lblCte, lblCte.Text);
                        toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        toolTip.SetToolTip(lblPro, lblPro.Text);
                        toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                        }

                        dDescuentoCopago = leer.CampoDouble("Descuento");

                        CargaDetallesVenta();
                        BuscarVale();
                        CambiaEstado(false);                        
                    }
                }
            }
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            bEsSeguroPopular = false; 

            if (txtCte.Text.Trim() == "")
            {
                txtCte.Text = "";
                lblCte.Text = "";
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
                toolTip.SetToolTip(lblCte, "");
                toolTip.SetToolTip(lblSubCte, "");
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                if (Fg.PonCeros(txtCte.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Público General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Crédito");
                    txtCte.Text = "";
                    lblCte.Text = "";
                    toolTip.SetToolTip(lblCte, "");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCte_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                        e.Cancel = true;
                    }
                    else
                    {
                        txtCte.Enabled = false;
                        txtCte.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCte.Text = "";
                        lblSubCte.Text = "";
                        txtPro.Text = "";
                        lblPro.Text = "";
                        txtSubPro.Text = "";
                        lblSubCte.Text = ""; 

                        toolTip.SetToolTip(lblCte, lblCte.Text);

                        //// Exigir la informacion de Seguro Popular solo si esta activo.
                        //if (bValidarSeguroPopular)
                        {
                            if (sIdSeguroPopular == txtCte.Text.Trim())
                            {
                                bEsSeguroPopular = true;
                            } 
                        }
                    }
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {

            if (txtSubCte.Text.Trim() == "")
            {
                txtSubCte.Text = "";
                lblSubCte.Text = "";
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubCte.Text = "";
                toolTip.SetToolTip(lblSubCte, "");
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 

                    txtSubCte.Enabled = false; 
                    txtSubCte.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                    bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                    bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");
                    dPorcentaje_Descuento___SocioComercial_Asociado = leer.CampoDouble("Descuento"); 

                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblSubCte, lblSubCte.Text);

                    if (txtFolio.Text.Trim() == "*" && !bEsEDM)
                    {
                        btnCodificacion.Enabled = btnGuardar.Enabled;
                    }

                    ////////// Interface de recetas electrónicas 
                    if (bEsVentaAsociados)
                    {
                        btnRecetasElectronicas.Enabled = GnFarmacia.ImplementaInterfaceExpedienteElectronico;
                    }

                    ////// Exclusivo Seguro Popular 
                    ////if (bEsSeguroPopular)
                    ////    MostrarInfoVenta(); 

                    //////// Inicializar el Grid 
                    //////myGrid.Limpiar(true); 

                }
            }

        }

        private void txtPro_Validating(object sender, CancelEventArgs e)
        {            
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Programa no encontrada, ó el Programa no pertenece al Cliente ó Farmacia.");
                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblPro, "");
                    toolTip.SetToolTip(lblSubPro, "");
                    e.Cancel = true;
                }
                else
                {
                    txtPro.Enabled = false; 
                    txtPro.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblPro, lblPro.Text);
                    toolTip.SetToolTip(lblSubPro, "");
                }
            }
            else
            {
                txtPro.Text = "";
                lblPro.Text = "";
                txtSubPro.Text = "";
                lblSubPro.Text = ""; 
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {            
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "" && txtSubPro.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Programa no encontrada, ó el Sub-Programa no pertenece al Cliente ó Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubPro.Enabled = false; 
                    txtSubPro.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");
                    toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();

                    //// Exclusivo Seguro Popular 
                    if (!bEsVentaAsociados)
                    {
                        if (bEsSeguroPopular || TipoDeSurtido.Intermed == tpSurtido)
                        {
                            MostrarInfoVenta(true);
                        }
                    }
                    else
                    {
                        MostrarInfoVenta(false);
                    }

                    if (!bEsSurtimientoPedido && !bEsEDM)
                    {
                        if (myGrid.Rows == 0)
                            myGrid.Limpiar(true);

                        btnCodificacion.Enabled = bImplementaCodificacion; 
                        
                        if (bImplementaCodificacion)
                        {
                            myGrid.BloqueaGrid(true);
                        }
                    }
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPro.Text = "";
                toolTip.SetToolTip(lblSubPro, "");
            }            
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                bRegresa = false; 
                Application.Exit();
            }

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Venta inválido, Verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de cliente inválida, Verifique.");
                txtCte.Focus();
            }

            if (bRegresa && txtSubCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCte.Focus();
            }

            if (bRegresa && txtPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Programa inválida, verifique.");
                txtPro.Focus();
            }

            if (bRegresa && txtSubPro.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma inválida, verifique.");
                txtSubPro.Focus();
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
                if (!bEsIdProducto_Ctrl)
                {
                    switch (tpSurtido)
                    {
                        case TipoDeSurtido.Mediaccess :
                            bRegresa = validarCantidades_Receta_Mediaccess();
                            break;
                        case TipoDeSurtido.AMPM:
                            bRegresa = validarCantidades_Receta_AMPM();
                            break;
                    }
                }
            }

            if (bRegresa)
            {
                ////VerificarSubPerfil = new clsVerificarCantSubPerfil();
                ////bRegresa = VerificarSubPerfil.VerificarCantidadesConExceso(Lotes, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4));
            }

            if (bRegresa)
            {
                if (!bEsIdProducto_Ctrl)
                {
                    VerificarLotes = new clsVerificarSalidaLotes();
                    bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes);
                }
            }

            if (bRegresa)
            {
                // Funcioanalidad para manejo de Almacenes Jurisdiccionales 
                if (tpPuntoDeVenta == TipoDePuntoDeVenta.Farmacia_Almacen)
                {
                    bRegresa = validarInfAdicional_FarmaciasAlmacen();
                }
            }

            //////if (bRegresa && bEmiteVales)
            //////{
            //////    //leerClaves.DataSetClase = InfCveSolicitadas.ClavesCapturadas;
            //////    if (!InfCveSolicitadas.Claves())
            //////    {
            //////        bRegresa = false;
            //////        General.msjUser("Necesita capturar las Claves Solicitadas[F9], verifique.");
            //////    }
            //////}

            if (bRegresa && DtGeneral.ConfirmacionConHuellas && DtGeneral.EsAlmacen)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar una venta, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("VENTAS", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("VENTAS", sMsjNoEncontrado);
            }


            if (bRegresa && dTotal_a_Pagar > 0 )
            {
                PagoEnCaja = new FrmPagoCaja_MA();

                PagoEnCaja.SubTotalIva0 = fSubTotalIva_0;
                PagoEnCaja.SubTotal = fSubTotal;
                PagoEnCaja.Iva = fIva;

                PagoEnCaja.SubTotalIva0 = 0;
                PagoEnCaja.SubTotal = 0;
                PagoEnCaja.Iva = 0;

                PagoEnCaja.Total = dTotal_a_Pagar;
                PagoEnCaja.Total = Convert.ToDouble("0" + txtTotal_a_Pagar.Text);
                PagoEnCaja.Total = Redondear(fTotal);
                PagoEnCaja.Total = Redondear(dTotal_a_Pagar);



                
                PagoEnCaja.TipoDeCambio = GnFarmacia.TipoDeCambioDollar;
                PagoEnCaja.ShowDialog();

                
                dPagoEfectivo = PagoEnCaja.PagoEfectivo;
                dPagoDolares = PagoEnCaja.PagoDolares;
                dPagoCheques = PagoEnCaja.PagoCheques;
                dCambios = PagoEnCaja.CambioClientes;
                myGridPago = PagoEnCaja.Grid;

                //// Revisar si prosigue el Proces 
                bRegresa = PagoEnCaja.PagoEfectuado;

                PagoEnCaja.Close();
                PagoEnCaja = null;

                if (!bRegresa)
                {
                    General.msjAviso("No se ha efectuado el Pago en Caja, no es posible generar la venta.");
                }
            }

            return bRegresa;
        }

        private bool validarInfAdicional_FarmaciasAlmacen()
        {
            bool bRegresa = true;
            if (bRegresa && !InfVtas.PermitirGuardar)
            {
                bRegresa = false;
                General.msjUser("La información adicional de la venta no es válida,\nno es posible generar la venta, verifique.");
            }

            if (bRegresa && !InfVtas.BeneficiarioVigente)
            {
                bRegresa = false;
                General.msjUser("La Vigencia del Beneficiario expiró,\nno es posible generar la venta, verifique.");
            }

            if (bRegresa && !InfVtas.BeneficiarioActivo)
            {
                bRegresa = false;
                General.msjUser("El Beneficiario se encuentra cancelado, no es posible generar la venta.");
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

            if (bEsIdProducto_Ctrl)
            {
                bRegresa = true; 
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para la venta\n y/o capturar cantidades para al menos un lote, verifique.");
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

            for(int i = 1; i <= myGrid.Rows; i++)
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

            if (bEsIdProducto_Ctrl)
            {
                bRegresa = true; 
            }

            // Se encontraron diferencias 
            if (!bRegresa)
            {
                General.msjAviso("Se detectó una ó mas diferencias en la captura de productos, la Venta no puede ser completada."); 
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog(); 
            }

            return bRegresa; 
        }

        private bool validarCantidades_Receta_Mediaccess()
        {
            bool bRegresa = true;
            int iCantidad = 0; 
            int iCantidad_Receta = 0;

            if (bEsRecetaManual || bCargaAutomaticaProductos)
            {
                myGrid.ColorColumna((int)Cols.Descripcion, Color.White);

                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                    iCantidad_Receta = myGrid.GetValueInt(i, (int)Cols.Cantidad_Maxima);

                    if ( iCantidad > iCantidad_Receta ) 
                    {
                        bRegresa = false;
                        myGrid.ColorCelda(i, (int)Cols.Descripcion, Color.Yellow); 
                    }
                }

                //// Se encontraron diferencias 
                if (!bRegresa)
                {
                    General.msjAviso("La cantidad captura de algún producto excede la cantidad registrada en la receta.");
                }
            }

            return bRegresa;
        }

        private bool validarCantidades_Receta_AMPM()
        {
            bool bRegresa = true;
            int iCantidad = 0;
            int iCantidad_Receta = 0;
            string sCodigoEAN = "", sFiltro = "";
            clsLeer leerTemp = new clsLeer();
            DataSet dtsCodigosSolicitados = new DataSet();

            dtsCodigosSolicitados = RecetaElectronica.Receta.Detalles_Claves;
            myGrid.ColorColumna((int)Cols.Descripcion, Color.White);


            if (RecetaElectronica.Receta.InformacionCargada)
            {
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                    sFiltro = string.Format("CodigoEAN = '{0}'", sCodigoEAN);

                    //leerDetalle.DataSetClase.Tables[0].Select(sFiltro);
                    leerTemp.DataRowsClase = dtsCodigosSolicitados.Tables[0].Select(sFiltro);
                    leerTemp.Leer();

                    iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                    iCantidad_Receta = leerTemp.CampoInt("CantidadRequerida");

                    if (iCantidad > iCantidad_Receta)
                    {
                        bRegresa = false;
                        myGrid.ColorCelda(i, (int)Cols.Descripcion, Color.Yellow);
                    }
                }

                //// Se encontraron diferencias 
                if (!bRegresa)
                {
                    General.msjAviso("La cantidad captura de algún producto excede la cantidad registrada en la receta.");
                }
            }

            return bRegresa;
        }

        private bool CargaDetalles_RecetaManual()
        {
            bool bRegresa = false;

            switch (tpSurtido)
            {
                case TipoDeSurtido.Mediaccess:
                    bRegresa = CargaDetalles_RecetaManual___Mediaccess();
                    break; 

                case TipoDeSurtido.Intermed:
                    bRegresa = CargaDetalles_RecetaManual___Intermed(); 
                    break;
            }

            return bRegresa; 
        }

        private bool CargaDetalles_RecetaManual___Intermed()
        {
            bool bRegresa = false;

            return bRegresa; 
        }

        private bool CargaDetalles_RecetaManual___Mediaccess()
        {
            int iRow = 0;
            bool bRegresa = true;
            string sSql = string.Format("Select Folio_MA, Partida, CodigoEAN, CantidadSolicitada, CantidadSurtida " + 
                "From INT_MA__RecetasElectronicas_002_Productos (NoLock) " + 
                " Where right(replicate('0', 50) + Folio_MA, 50) = '{0}' And Consecutivo = '{1}' " +
                " Order By Partida ", Fg.PonCeros(localElegibilidad.MA_FolioDeReceta, 50), localElegibilidad.MA_FolioDeConsecutivo); 

            if (bEsRecetaManual || bCargaAutomaticaProductos)
            {
                if (!leer2.Exec(sSql))
                {
                    Error.GrabarError(leer2, "CargaDetalles_RecetaManual()");
                    General.msjError("Ocurrió un error al obtener la lista de medicamentos de la receta.");
                }
                else
                {
                    myGrid.Limpiar(false);
                    while (leer2.Leer())
                    {
                        myGrid.Rows = myGrid.Rows + 1;
                        myGrid.ActiveRow = myGrid.Rows;
                        iRow++;

                        myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                        myGrid.SetValue(iRow, (int)Cols.CodEAN, leer2.Campo("CodigoEAN"));
                        ObtenerDatosProducto(iRow, true, false);
                        myGrid.SetValue(iRow, (int)Cols.Cantidad_Maxima, leer2.Campo("CantidadSolicitada"));
                    }

                    myGrid.BloqueaColumna(true, 1);
                }

            }
            return bRegresa;
        }

        private bool CargaDetallesVenta()
        {
            bool bRegresa = true;
            double dMonto = 0;

            leer2.DataSetClase = Consultas.FolioDet_Ventas(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargaDetallesVenta");
            if (leer2.Leer())
            {
                myGrid.LlenarGrid(leer2.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }
            // myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BloqueaColumna(true, 1);


            dMonto = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);
            dPorcentaje_Aplicar = 100 - (Redondear((dDescuentoCopago / dMonto)) * 100);           
            //Copago  = dPorcentaje_Aplicar;


            CalcularTotales();
            CargarDetallesLotesVenta();
            return bRegresa;
        }

        private void CargarDetallesLotesVenta()
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = Consultas.FolioDetLotes_Ventas(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargarDetallesLotesVenta()");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {                
                leer.DataSetClase = Consultas.FolioDetLotes_Ventas_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioVenta, "CargarDetallesLotesVenta");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }
        #endregion Validaciones

        #region Funciones 
        private bool LlenaCliente()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatClientes (nolock) WHERE IdCliente='{0}' ", Fg.PonCeros(txtCte.Text, 4));

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "LlenaCliente()");
                General.msjError("Error al buscar el Nombre de Cliente");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRegresa = true;
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("Nombre");
                }
            }

            return bRegresa;
        }

        private bool LlenaSubCte()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatSubClientes (nolock) WHERE IdCliente = '{0}' AND IdSubCliente='{1}' ", Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4));

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "LlenaSubCte()");
                General.msjError("Error al buscar el Nombre de SubCliente");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRegresa = true;
                    txtSubCte.Text = leer2.Campo("IdSubCliente");
                    lblSubCte.Text = leer2.Campo("Nombre");
                }
            }

            return bRegresa;
        }

        private bool LlenaPrograma()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatProgramas (nolock) WHERE IdPrograma='{0}' ", Fg.PonCeros(txtPro.Text, 4));

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "LlenaPrograma()");
                General.msjError("Error al buscar el Nombre de Programa");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRegresa = true;
                    txtPro.Text = leer2.Campo("IdPrograma");
                    lblPro.Text = leer2.Campo("Descripcion");
                }
            }

            return bRegresa;
        }

        private bool LlenaSubPrograma()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatSubProgramas (nolock) WHERE IdSubPrograma='{0}' AND IdPrograma='{1}' ", Fg.PonCeros(txtSubPro.Text, 4), Fg.PonCeros(txtPro.Text,4));

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "LlenaSubPrograma()");
                General.msjError("Error al buscar el Nombre de SubPrograma");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRegresa = true;
                    txtSubPro.Text = leer2.Campo("IdSubPrograma");
                    lblSubPro.Text = leer2.Campo("Descripcion");
                }
            }

            return bRegresa;
        }

        private void CambiaEstado(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtCte.Enabled = bValor;
            txtPro.Enabled = bValor;
            txtSubCte.Enabled = bValor;
            txtSubPro.Enabled = bValor;
        }
        
        private void ObtieneClaveLote(string sIdProducto, string sCodigoEAN, ref string sClaveLote )
        {            
            string sSql = "";
            leer3 = new clsLeer(ref con);

            sSql = string.Format(" SELECT TOP 1 ClaveLote FROM FarmaciaProductos_CodigoEAN_Lotes (nolock) " +
		                           " WHERE CodigoEAN = '{0}' AND IdProducto = '{1}'  ", sCodigoEAN, Fg.PonCeros(sIdProducto, 8) );

            if (!leer3.Exec(sSql))
            {
                Error.GrabarError(leer3, "ObtieneClaveLote()");
                General.msjError("Error al buscar la clave lote");
            }
            else
            {
                if (leer3.Leer())
                {
                    sClaveLote = leer3.Campo("ClaveLote");
                }
            }            
        }

        private bool RequiereVale()
        {
            bool bRequiere = false;
            ////string sSql = "";
            ////int iClaves = 0;
            ////leerBusqueda = new clsLeer(ref con);

            ////sSql = string.Format("Select IdClaveSSA, ( CantidadRequerida - CantidadEntregada ) as Cantidad " + 
            ////    " From VentasEstadisticaClavesDispensadas(NoLock) " + 
            ////    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' " + 
            ////    " And CantidadRequerida <> CantidadEntregada ", DtGeneral.EmpresaConectada, 
            ////    Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta);

            ////if (!leerBusqueda.Exec(sSql))
            ////{
            ////    bContinua = false; //Esta variable es para que no continue con el guardado.
            ////    Error.GrabarError(leerBusqueda, "RequiereVale()");
            ////    General.msjError("Error al verificar si Requiere Vale");
            ////}
            ////else
            ////{
            ////    if (leerBusqueda.Leer())
            ////    {
            ////        bRequiere = true;
            ////    }
            ////}

            return bRequiere;
        }

        private bool BuscarVale()
        {
            bool bRegresa = false;
            
            ////if (txtFolio.Text.Trim() != "" || txtFolio.Text.Trim() != "*")
            ////{
            ////    Consultas.MostrarMsjSiLeerVacio = false;
            ////    leer.DataSetClase = Consultas.Ventas_ObtenerVale(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "BuscarVale");
            ////    if (!leer.Leer())
            ////    {
            ////        bGeneroVale = false;
            ////    }
            ////    else
            ////    {
            ////        sFolioVale = leer.Campo("Folio");
            ////        bGeneroVale = true;
            ////        IniciarToolBar(false, false, true, true);
            ////    }
            ////}

            ////Consultas.MostrarMsjSiLeerVacio = true;

            return bRegresa;
        }
        #endregion Funciones

        #region Atencion de pedidos especiales 
        public bool CargaDetallesGeneraVenta(string FarmaciaPedido, string FolioPedido, string FolioSurtido)
        {
            bool bRegresa = false;
            bEsSurtimientoPedido = true;
            sFarmaciaPed = FarmaciaPedido;
            sFolioSurtido = FolioSurtido;
            sFolioPedido = FolioPedido;
 
            btnNuevo_Click(this, null);

            leer2.DataSetClase = Consultas.PedidosEspeciales_GenerarVentaEnc(sEmpresa, sEstado, sFarmacia, FolioPedido, "CargaDetallesGeneraVenta");

            if (leer2.Leer())
            {
                txtCte.Text = leer2.Campo("IdCliente");
                txtCte_Validating(this, null);
                txtSubCte.Text = leer2.Campo("IdsubCliente");
                txtSubCte_Validating(this, null);
                txtPro.Text = leer2.Campo("IdPrograma");
                txtPro_Validating(this, null);
                txtSubPro.Text = leer2.Campo("IdSubPrograma");
                txtSubPro_Validating(this, null);
                bRegresa = true;
            }

            if (bRegresa)
            {
                leer2.DataSetClase = Consultas.PedidosEspeciales_GenerarVenta(sEmpresa, sEstado, sFarmacia, FolioSurtido, "CargaDetallesGeneraVenta");
                if (leer2.Leer())
                {
                    bRegresa = true;
                    myGrid.LlenarGrid(leer2.DataSetClase, false, false);
                    CargarGenerarDetallesLotesVenta(FolioSurtido);
                }

                // myGrid.EstiloGrid(eModoGrid.ModoRow);
                myGrid.BloqueaColumna(true, 1);

                btnNuevo.Enabled = false;

                //GnFarmacia.CapturaDeClavesSolicitadasHabilitada
                bCapturaDeClavesSolicitadasHabilitada = !bEsSurtimientoPedido;
                txtFolio_Validating(this, null);
                this.ShowDialog();
            }

            return bRegresa;
        }

        private void CargarGenerarDetallesLotesVenta(string FolioSurtido)
        {
            leer.DataSetClase = clsLotes.PreparaDtsLotes();
            leer.DataSetClase = Consultas.PedidosEspeciales_GenerarVenta_Lotes(sEmpresa, sEstado, sFarmacia, FolioSurtido, "CargarGenerarDetallesLotesVenta()");
            Lotes.AddLotes(leer.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                leer.DataSetClase = Consultas.PedidosEspeciales_GenerarVenta_Lotes_Ubicaciones(sEmpresa, sEstado, sFarmacia, FolioSurtido, "CargarGenerarDetallesLotesVenta");
                Lotes.AddLotesUbicaciones(leer.DataSetClase);
            }
        }

        private bool ActualizarEstatusPedido()
        {
            bool bRegresa = true;
            string sSql = string.Format("Update Pedidos_Cedis_Enc_Surtido " +
                "Set Status = 'E', FolioTransferenciaReferencia = '{0}' " +
                "Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And FolioSurtido = '{4}'",
                sFolioMovtoInv, sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            if (!leer.Exec(sSql))
            {
                bRegresa = false; 
            }

            return bRegresa;
        }

        private bool RevisarPedidoCompleto()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto '{0}', '{1}', '{2}', '{3}', '{4}' ",
                sEmpresa, sEstado, sFarmacia, sFarmaciaPed, sFolioPedido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool RegistrarAtencion()
        {
            bool bRegresa = true;
            string sSql = string.Format(" Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                     DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido, DtGeneral.IdPersonal, "");

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }
        #endregion Atencion de pedidos especiales 

        #region Eventos
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = Ayuda.Folios_Ventas(sEstado,sFarmacia,"txtFolio_KeyDown");

            //    if (leer.Leer())
            //    {
            //        if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") != TipoDeVenta.Publico)
            //        {
            //            General.msjUser("Folio de venta capturado no es de contado, Verifique.");
            //            txtFolio.Focus();
            //        }
            //        else
            //        {
            //            txtFolio.Text = leer.Campo("FolioVenta");
            //            txtCte.Text = leer.Campo("IdCliente");
            //            LlenaCliente();
            //            txtSubCte.Text = leer.Campo("IdSubCliente");
            //            LlenaSubCte();
            //            txtPro.Text = leer.Campo("IdPrograma");
            //            LlenaPrograma();
            //            txtSubPro.Text = leer.Campo("IdSubPrograma");
            //            LlenaSubPrograma();

            //            txtNumRec.Text = leer.Campo("FolioReceta");

            //            txtfolderhab.Text = leer.Campo("FolioDerechoHabiencia");
            //            lblNomPaciente.Text = leer.Campo("IdPaciente");

            //            if (leer.Campo("Status") == "C")
            //            {
            //                lblCancelado.Visible = true;
            //            }
            //            CargaDetallesVenta();
            //            CambiaEstado(false);
            //        }
            //    }
            //}
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)         
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer2.Leer())
                {
                    txtCte.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                    toolTip.SetToolTip(lblCte, lblCte.Text); 
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown_1");
                    if (leer2.Leer())
                    {
                        txtSubCte.Text = leer2.Campo("IdSubCliente");
                        lblSubCte.Text = leer2.Campo("NombreSubCliente");
                        toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        txtPro.Focus();

                        if (bEsVentaAsociados)
                        {
                            btnRecetasElectronicas.Enabled = GnFarmacia.ImplementaInterfaceExpedienteElectronico;
                        }
                    }
                }
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
                {
                    //leer2.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtPro_KeyDown"); 
                    if (leer2.Leer())
                    {
                        txtPro.Text = leer2.Campo("IdPrograma");
                        lblPro.Text = leer2.Campo("Programa");
                        toolTip.SetToolTip(lblPro, lblPro.Text);
                        txtSubPro.Focus();
                    }
                }
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //string sIdPrograma = "";
                //sIdPrograma = txtPro.Text;
                //leer2.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", sIdPrograma);
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, txtPro.Text, "txtPro_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubPro.Text = leer2.Campo("IdSubPrograma");
                        lblSubPro.Text = leer2.Campo("SubPrograma");
                        toolTip.SetToolTip(lblSubPro, lblSubPro.Text);
                    }
                }
            }
        }

        #endregion Eventos    

        #region Grid

        private void grdProductos_EditModeOff_1(object sender, EventArgs e)
        {
            try
            {
                Cols iCol = (Cols)myGrid.ActiveCol;
                switch (iCol)
                {
                    case Cols.CodEAN:
                        ObtenerDatosProducto();
                        break;
                }
            }
            catch (Exception ex )
            {
                //General.msjError("01 "  + ex.Message); 
            }
        }

        private void grdProductos_EditModeOn_1(object sender, EventArgs e)
        {
            try 
            {
                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            }
            catch (Exception ex)
            {
                //General.msjError("02 " + ex.Message);
            }

            //switch (myGrid.ActiveCol)
            //{
            //    case 1: // Si se cambia el Codigo, se limpian las columnas
            //        {
            //            limpiarColumnas();
            //        }
            //        break;
            //}
        }

        private void grdProductos_Advance_1(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            bool bAgregarRenglon = false; 

            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    try 
                    {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (!bEsIdProducto_Ctrl)
                        {
                            if (!bImplementaCodificacion)
                            {
                                bAgregarRenglon = true; 
                            }
                        }
                    }
                    }
                    catch (Exception ex)
                    {
                        General.msjError("03 " + ex.Message);
                    }
                }
            }

            ////// Jesus.Diaz 2K151029.1410 
            if (bAgregarRenglon)
            {
                if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
                    ////ObtenerDatosProducto();
                }
            }
        }

        private void grdProductos_KeyDown_1(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow;

            if (tpSurtido != TipoDeSurtido.Intermed)
            {
                if (bEsRecetaManual || bCargaAutomaticaProductos)
                {
                    ColActiva = Cols.Ninguna;
                }
            }

            switch (ColActiva)
            {
                case Cols.Precio:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    
                        if (e.KeyCode == Keys.F1)
                        {
                            if (!bEsIdProducto_Ctrl)
                            {
                                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                                leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, bDispensarSoloCuadroBasico, "grdProductos_KeyDown_1");
                                if (leer.Leer())
                                {
                                    myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                                    ObtenerDatosProducto();
                                }
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            if (!bEsIdProducto_Ctrl)
                            {
                                removerLotes();
                            }
                        }


                        //// Administracion de Mach4 
                        if (e.KeyCode == Keys.F11)
                        {
                            ActualizarColorFondo(); 
                        }
                    break;
            }
        }

        private bool ValidarSeleccionCodigoEAN(string Codigo)
        {
            bool bRegresa = true;

            sCodigoEAN_Seleccionado = Codigo; 

            sCodigoEAN_Seleccionado = RevCodigosEAN.VerificarCodigosEAN(Codigo, false);
            bRegresa = RevCodigosEAN.CodigoSeleccionado; 


            return bRegresa; 
        }

        private void ObtenerDatosProducto()
        {
            ObtenerDatosProducto(myGrid.ActiveRow, true); 
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion)
        {
             ObtenerDatosProducto(Renglon, BuscarInformacion, true); 
        }

        private void ObtenerDatosProducto(int Renglon, bool BuscarInformacion, bool MostrarLotes)
        {
            string sCodigo = "", sSql = ""; 
            bool bCargarDatosProducto = true;
            string sMsj = "";
            string sListaDeProductos = "";
            int EsCodigoEANClave = 0;

            if (!bEsVentaAsociados)
            {
                switch (tpSurtido)
                {
                    case TipoDeSurtido.Mediaccess:
                        sListaDeProductos = localElegibilidad.ListaClaves_Receta_Extended;
                        break;

                    case TipoDeSurtido.Intermed:
                        sListaDeProductos = localVale.ListaClaves_Receta_Extended;
                        break;
                }
            }

            if (TipoDeSurtido.AMPM == tpSurtido)
            {
                sListaDeProductos = RecetaElectronica.Receta.ListaClavesSSA_Extended;
                EsCodigoEANClave = 1;
            }


            sCodigo = myGrid.GetValue(Renglon, (int)Cols.CodEAN);
            if (EAN.EsValido(sCodigo) && sCodigo != "") 
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
                {
                    myGrid.LimpiarRenglon(Renglon);
                    myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN); 
                }
                else 
                {
                    sCodigo = sCodigoEAN_Seleccionado;
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " + 
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " + 
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = '{9}'  ,  " +
                        " @INT_OPM_ProcesoActivo = '{10}', @EsCodigoEANClave = {11}, @EsSocioComercial = '{12}'  ",
                        (int)TipoDeVenta.Credito, txtCte.Text.Trim(), txtSubCte.Text.Trim(),
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, 0, sListaDeProductos, 
                        Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo),
                        EsCodigoEANClave, iEsVentaAsociados);
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerDatosProducto()");
                        General.msjError("Ocurrió un error al obtener la información del Producto.");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia.");
                            myGrid.LimpiarRenglon(Renglon);
                        }
                        else
                        {
                            if (!leer.CampoBool("EsDeFarmacia"))
                            {
                                bCargarDatosProducto = false;
                                sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique."; 
                            }
                            else
                            {
                                if (bDispensarSoloCuadroBasico)
                                {
                                    if (!leer.CampoBool("DCB"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Básico Autorizado, verifique."; 
                                    }
                                }
                            }

                            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
                            {
                                if (RecetaElectronica.Receta.InformacionCargada)
                                {
                                    if (!leer.CampoBool("EsDeRecetaElectronica"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = string.Format("El Código EAN {0} no esta incluido en la receta electrónica.", leer.Campo("CodigoEAN"));
                                    }
                                }
                            }

                            if (!bCargarDatosProducto)
                            {
                                General.msjUser(sMsj);
                                myGrid.LimpiarRenglon(Renglon);
                                myGrid.SetActiveCell(Renglon, (int)Cols.CodEAN); 
                            }
                            else
                            {
                                CargaDatosProducto(Renglon, BuscarInformacion, MostrarLotes);
                            }
                        }
                    }
                }
            }
            else
            {
                //General.msjError(sMsjEanInvalido);
                myGrid.LimpiarRenglon(Renglon);
                myGrid.ActiveCelda(Renglon, (int)Cols.CodEAN);
                SendKeys.Send("");
            }
        }

        private void ActualizarColorFondo()
        {
            //////if (IMach4.EsClienteIMach4)
            //////{
            //////    FrmColorProductosIMach myColor = new FrmColorProductosIMach();
            //////    myColor.ShowDialog();
            //////    Color colorBack = GnFarmacia.ColorProductosIMach; 

            //////    for (int i = 1; i<= myGrid.Rows; i++)
            //////    {
            //////        if ( myGrid.GetValueBool(i, (int)Cols.EsIMach4) )
            //////        {
            //////            myGrid.ColorRenglon(i, colorBack); 
            //////        }
            //////    }
            //////}
        }

        private bool validarProductoCtrlVales(string CodigoEAN)
        {
            bool bRegresa = true;
            bool bEsCero = false;
            // string sDato = "";

            bEsCero = CodigoEAN == "0000000000000" ? true : false;
            if (bEsCero)
            {
                bEsIdProducto_Ctrl = true;
                if (!GnFarmacia.EmisionDeValesCompletos)
                {
                    bEsIdProducto_Ctrl = false;
                    bRegresa = false;
                    General.msjUser("La unidad no esta configurada para manejar este Producto, verifique.");
                }
            }

            return bRegresa;
        }

        private void CargaDatosProducto()
        {
            CargaDatosProducto(myGrid.ActiveRow, true);
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion)
        {
            CargaDatosProducto(Renglon, BuscarInformacion, false); 
        }

        private void CargaDatosProducto(int Renglon, bool BuscarInformacion, bool MostrarLotes)
        {
            int iRowActivo = Renglon; //// myGrid.ActiveRow;           
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false;
            double dPrecioDeVenta = 0; 
            string sCodEAN = leer.Campo("CodigoEAN");

            if (lblCancelado.Visible == false)
            {
                if (sValorGrid != sCodEAN)
                {
                    if (validarProductoCtrlVales(sCodEAN))
                    {
                        if (!myGrid.BuscaRepetido(sCodEAN, iRowActivo, iColEAN))
                        {
                            myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                            myGrid.SetValue(iRowActivo, (int)Cols.Codigo, leer.Campo("IdProducto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("Descripcion"));
                            myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, leer.Campo("PorcIva"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Precio, bEsDispensacionDeConsignacion ? 0 : leer.CampoDouble("PrecioVenta")); 
                            myGrid.SetValue(iRowActivo, (int)Cols.UltimoCosto, bEsDispensacionDeConsignacion ? 0 : leer.CampoDouble("UltimoCosto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);
                            myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Cantidad_Maxima, 0);

                            bEsMach4 = leer.CampoBool("EsMach4");
                            myGrid.SetValue(iRowActivo, (int)Cols.EsIMach4, bEsMach4);

                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

                            ////////////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                            //////if (IMach4.EsClienteIMach4)
                            //////{
                            //////    if (bEsMach4)
                            //////    {
                            //////        GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
                            //////        IMachPtoVta.Show(leer.Campo("IdProducto"), sCodEAN);
                            //////    }
                            //////}

                            Application.DoEvents(); //// Asegurar que se refresque la pantalla 
                            this.Refresh(); 
                            CargarLotesCodigoEAN(BuscarInformacion, MostrarLotes);


                            // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
                        }
                        else
                        {
                            General.msjUser("El artículo ya se encuentra capturado en otro renglón.");
                            myGrid.SetValue(myGrid.ActiveRow, 1, "");
                            myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                            myGrid.EnviarARepetido();
                        }
                    }
                }
                else
                {
                    // Asegurar que no cambie el CodigoEAN
                    myGrid.SetValue(iRowActivo, iColEAN, sCodEAN);
                }
            }

            grdProductos.EditMode = false;
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                {
                    myGrid.DeleteRow(i);
                }
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
            {
                myGrid.AddRow();
            }
        }
        #endregion Grid

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            CargarLotesCodigoEAN(true, true); 
        }

        private void CargarLotesCodigoEAN(bool BuscarInformacion, bool MostrarLotes)
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            if (BuscarInformacion)
            {
                leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpTipoDeInventario, false, "CargarLotesCodigoEAN()");
                if (Consultas.Ejecuto)
                {
                    // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                    leer.Leer();
                    Lotes.AddLotes(leer.DataSetClase);

                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones__Ventas(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpTipoDeInventario, tpUbicacion, false, "CargarLotesCodigoEAN()");
                        if (Consultas.Ejecuto)
                        {
                            leer.Leer();
                            Lotes.AddLotesUbicaciones(leer.DataSetClase);
                        }
                    }

                    if (MostrarLotes)
                    {
                        mostrarOcultarLotes();
                    }
                }
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
                }
                catch { }

                if (myGrid.Rows == 0)
                {
                    myGrid.Limpiar(true); 
                    if (!bImplementaCodificacion)
                    {
                        myGrid.BloqueaGrid(true);
                    }

                    myGrid.BloqueaColumna(false, (int)Cols.CodEAN);
                }

                CalcularTotales(); 
            }
        }

        private void mostrarOcultarLotes()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            if (sCodigoEAN == Fg.PonCeros(0, 13))
            {
                MostrarCapturaDeClavesRequeridas(); 
            }
            else
            {
                mostrarOcultarLotes_General(); 
            }

            CalcularTotales();
        }

        private void mostrarOcultarLotes_General()
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
                    Lotes.EsEntrada = false;// para las ventas
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = bEsIdProducto_Ctrl ? false: true;
                    if (bFolioGuardado)
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    if (bEsSurtimientoPedido)
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    if (bEsEDM)
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    if (bImplementaCodificacion)
                    {
                        Lotes.ModificarCantidades = false;
                    }

                    //// 2K120105.2025 
                    //// Control de Vales para Puebla 

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;
                    // Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion; 

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    // Administracion de Mach4
                    //////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    //////{
                    //////    if (IMachPtoVta.RequisicionRegistrada)
                    //////        Lotes.Show(); 
                    //////}
                    //////else 
                    {
                        Lotes.IdCliente = txtCte.Text;
                        Lotes.IdSubCliente = txtSubCte.Text;
                        Lotes.Show(); 
                    }


                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Precio);

                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes

        #region Informacion de venta 
        private void MostrarCapturaDeClavesRequeridas()
        {
            if (bCapturaDeClavesSolicitadasHabilitada)
            {
                InfCveSolicitadas.Show(txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtFolio.Text);
                //InfCveSolicitadas.Claves(); 
            }
        }

        private void MostrarInfoVenta(bool CerrarInformacionAdicional) 
        {
            //bool bRegresa = true;
            string sFolioReceta = "";

            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;

                if (bEsEDM)
                {
                    InfVtas.NumReceta = sNumReceta;
                    InfVtas.RecetaEDM = true;
                }

                if (!bCierreAutomatico)
                {
                    InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
                }
                else
                {
                    
                    ////InfVtas.BloquearControles = !bEsRecetaManual;
                    ////CerrarInformacionAdicional = !bEsRecetaManual;

                    if (!bEsVentaAsociados)
                    {
                        InfVtas.BloquearControles = true;

                        switch (tpSurtido)
                        {
                            case TipoDeSurtido.Mediaccess:

                                sFolioReceta = (localElegibilidad.MA_FolioDeReceta + " - " + localElegibilidad.MA_FolioDeConsecutivo);

                                InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text,
                                    localElegibilidad.IdBeneficiario, sFolioReceta, localElegibilidad.MA_FechaEmisionReceta,
                                    localElegibilidad.IdMedico, localElegibilidad.MA_CIE_10_Principal, "000", "000", "01", CerrarInformacionAdicional);
                                break;

                            case TipoDeSurtido.Intermed:
                                localVale.IdCliente = txtCte.Text;
                                localVale.ClienteNombre = lblCte.Text;
                                localVale.IdSubCliente = txtSubCte.Text;
                                localVale.SubClienteNombre = lblSubCte.Text;

                                if (CerrarInformacionAdicional)
                                {
                                    CerrarInformacionAdicional = !localVale.ValeManual;
                                    InfVtas.BloquearControles = !localVale.ValeManual;
                                }

                                InfVtas.Show(txtFolio.Text, localVale.IdCliente, lblCte.Text, txtSubCte.Text, lblSubCte.Text,
                                    localVale.IdBeneficiario, localVale.MA_FolioDeReceta, localVale.MA_FechaEmisionReceta,
                                    localVale.IdMedico, "0001", "000", "000", "01", CerrarInformacionAdicional);
                                break;
                        }
                    }
                    else
                    {
                        if (RecetaElectronica.Receta.InformacionCargada)
                        {
                            InfVtas.BloquearControles = true;
                            InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text,
                                        RecetaElectronica.Receta.IdBeneficiario, RecetaElectronica.Receta.FolioReceta, RecetaElectronica.Receta.FechaReceta,
                                        RecetaElectronica.Receta.IdMedico, RecetaElectronica.Receta.CIE_10, "000", "000", "01", CerrarInformacionAdicional);

                            //RecetaElectronica.Receta.CIE_10 = InfVtas.Diagnostico;
                        }
                        else
                        {
                            InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
                        }
                    }
                }
            } 
            //else
            //{
            //}
        }

        //private bool ActualizarCantidades()
        //{
        //    bool bRegresa = false;

        //    string sSql = string.Format("Exec spp_INT_IME__RegistroDeVales_Actualizar @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}'",
        //        localVale.IdSocioComercial, localVale.IdSucursalSocioComercial, localVale.FolioVale);

        //    if (leer2.Exec(sSql))
        //    {
        //        bRegresa = true;
        //    }

        //    return bRegresa;
        //}

        private bool ValidarInformacion()
        {
            bool bRegresa = false;

            string sSql = string.Format("Exec spp_INT_IME__RegistroDeVales_Validar @IdSocioComercial = '{0}', @IdSucursal = '{1}', @Folio_Vale = '{2}'",
                localVale.IdSocioComercial, localVale.IdSucursalSocioComercial, localVale.FolioVale);

            if (leer.Exec(sSql))
            {
                bRegresa = true;
            }

            clsLeer leerValidacion = new clsLeer();
            leer.RenombrarTabla(1, "Cantidad Excedida");
            leer.RenombrarTabla(2, "ClaveSSA Invalida");

            leerValidacion.DataTableClase = leer.Tabla(1);   // Cantidad Excedida
            bActivarProceso = leerValidacion.Registros > 0;

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(2);   // ClaveSSA Invalida
                bActivarProceso = leerValidacion.Registros > 0;
            }

            return bRegresa;
        } 

        public void MostrarDetalle(string sEmpresaCSGN, string sEstadoCSGN, string sJurisdiccionCSGN, string sFarmaciaCSGN, string FolioPedidoRC, int iCantidadSurtida)
        {
            btnNuevo.Enabled = false;

            tpPuntoDeVenta = TipoDePuntoDeVenta.AlmacenJurisdiccional; 
            IdEmpresaCSGN = sEmpresaCSGN;
            IdEstadoCSGN = sEstadoCSGN;
            IdJurisdiccionCSGN = sJurisdiccionCSGN;
            IdFarmaciaCSGN = sFarmaciaCSGN;
            sFolioPedidoRC = FolioPedidoRC;
            iCantidadSurtidaCSGN = iCantidadSurtida;

            this.ShowDialog();
        }
        #endregion Informacion de venta         

        private void lblMensajes_Click(object sender, EventArgs e)
        {

        }

        private void txtFolio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCte_TextChanged(object sender, EventArgs e)
        {

        }

        #region Codificacion Datamatrix 
        private void btnCodificacion_Click(object sender, EventArgs e)
        {
            int iRows = 0; 
            string sProducto = "", sCodigoEAN = "", sDescripcion = "";
            clsLeer leerUUIDS = new clsLeer(); 
            clsLotes lotes_Aux = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario); 

            sProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
            sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            sDescripcion = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion);

            FrmLotesSNK f = new FrmLotesSNK();
            f.MostrarPantalla(sProducto, sCodigoEAN, sDescripcion, bEsIdProducto_Ctrl, Lotes);
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
                    myGrid.SetValue((int)Cols.CodEAN, sCodigoEAN_SNK);

                    ObtenerDatosProducto(iRows, false);
                    sProducto = myGrid.GetValue(iRows, (int)Cols.Codigo);
                    sCodigoEAN = myGrid.GetValue(iRows, (int)Cols.CodEAN);

                    myGrid.SetValue((int)Cols.Cantidad, lotes_Aux.Totalizar(sProducto, sCodigoEAN));

                }

                Lotes = lotes_Aux; 
                ////Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario); 
                ////Lotes.AddLotes(lotes_Aux.DataSetLotes); 
                ////Lotes.UUID_UpdateList(lotes_Aux.UUID_List().DataSetClase); 
                leerUUIDS.DataSetClase = Lotes.UUID_List.DataSetClase; 

            }
        }
        #endregion Codificacion Datamatrix


    }
}
