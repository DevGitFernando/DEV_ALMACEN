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

using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft.QRCode.GenerarEtiquetas;

using Farmacia.Procesos;
using Farmacia.Vales;
using DllRecetaElectronica.ECE;  

////using Dll_IMach4;
////using Dll_IMach4.Interface; 
using DllRobotDispensador;

namespace Farmacia.Ventas
{
    public partial class FrmVentas : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Precio = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10, 
            EsIMach4 = 11, UltimoCosto = 12  
        }

        
        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        string sFolioSolicitud = "";
        int iCantidadMinima_Avance = RobotDispensador.Robot.EsClienteInterface ? 0 : 1;
        bool bEsClienteInterface = RobotDispensador.Robot.EsClienteInterface;

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
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

        clsGrid myGrid;
        // Variables Globales  ****************************************************
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bImportarBeneficiarios = false;
        bool bCapturaDeClavesSolicitadasHabilitada = GnFarmacia.CapturaDeClavesSolicitadasHabilitada; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "", sFolioVenta = "", sFolioSurtido = "", sMensajeErrorGrabar = "";
        bool bEsSurtimientoPedido = false;
        string sFarmaciaPed = "", sFolioPedido = "";
        int iRegistros = 0;
        string sMsjNoEncontrado = "";

        string IdEmpresaCSGN = "", IdEstadoCSGN = "", IdFarmaciaCSGN = "", sFolioPedidoRC = "", IdJurisdiccionCSGN = "";
        int iCantidadSurtidaCSGN = 0;

        bool bContinua = false;
        double fSubTotal = 0, fIva = 0, fTotal = 0;
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
        bool bImplementaReaderDM = GnFarmacia.ImplementaReaderDM;
        bool bValidarConsumoDeClaves_Programacion = GnFarmacia.ValidarConsumoClaves_Programacion;
        bool bValidarConsumoDeClaves_ProgramaAtencion = GnFarmacia.ValidarConsumoClaves_ProgramaAtencion;
        bool bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = false;
        int iNumeroDeCopias = GnFarmacia.NumeroDeCopiasTickets;
        bool bValidar_Perfil_x__Programa_SubPrograma = false; 

        bool bFolioGuardado = false;
        TiposDeUbicaciones tpUbicacion = TiposDeUbicaciones.Todas;
        
        string sManejaTodasLasUbicaciones = "MANEJA_TODAS_LAS_UBICACIONES";
        string sManejaSoloUbicacionesAlmacenaje = "MANEJA_SOLO_UBICACIONES_DE_ALMACENAJE";

        string sListaClavesSSA_RecetaElectronica = "";

        bool bGuardadoDeInformacion_Masivo = true;
        string sSql_Detallado = "";

        bool bTieneSurtimientosActivos = false;
        bool bTienePermitidasVentasNormales = true;

        string sMensajeConSurtimiento = "Se encontrarón folios de surtimiento pendientes de generar traspaso ó dispersión\n\n" +
                "No se puede generar la dispersión. Validar el estatus de folios de surtimiento.";

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
        

        // bool bEmiteValesAutomaticos = true; //AQUI DEBE IR LA VARIABLE GLOBAL GnFarmacia.EmiteVales.
        bool bEmiteValesAutomaticos = GnFarmacia.EmisionDeValesCompletos && GnFarmacia.EmisionDeValesAutomaticosAlDispensar;
        bool bGeneroVale = false; 
        string sFolioVale = "";

        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        FrmIniciarSesionEnCaja Sesion;
        // bool bSesionIniciada = false;

        string sCodigoEAN_Seleccionado = ""; 
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsSKU SKU; 

        TiposDeInventario tpTipoDeInventario = TiposDeInventario.Todos; 
        clsVerificarSalidaLotes VerificarLotes;
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();

        clsVerificarCantSubPerfil VerificarSubPerfil;

        bool bEsEDM = false;
        string sNumReceta = "", sFolioEDM;

        #region Vales 
        bool bEsIdProducto_Ctrl = false;
        #endregion Vales

        bool bEsNuevoId = false;

        #endregion variables

        public FrmVentas()
        {
            // MessageBox.Show(Application.OpenForms.Count.ToString());
            InitializeComponent();

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
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Credito);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true; 
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Precio, (int)Cols.Importe, (int)Cols.Descripcion);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref con, sEmpresa, sEstado, sFarmacia, sPersonal);

            //// Control de Acceso para lectura de Codigos DataMatrix 
            btnCodificacion.Visible = (bImplementaCodificacion || bImplementaReaderDM);
            btnCodificacion.Enabled = false; 

            if (GnFarmacia.ValidarUbicacionesEnCapturaDeSurtido)
            {
                SolicitarPermisosUsuario();
            }

            btnRecetasElectronicas.Enabled = GnFarmacia.ImplementaInterfaceExpedienteElectronico;
            btnRecetasElectronicas.Visible = btnRecetasElectronicas.Enabled;
            btnRecetaElectronica_Claves.Visible = btnRecetasElectronicas.Enabled;
            toolStripSeparator_05.Visible = btnRecetasElectronicas.Visible;

            btnInformacionAIE.Enabled = GnFarmacia.ImplementaCapturaInformacionAEI;
            btnInformacionAIE.Visible = btnInformacionAIE.Enabled;
            toolStripSeparator_06.Visible = btnInformacionAIE.Enabled;

            chk_RPT_EtiquetasCajas.BackColor = Color.Transparent;

            CargarEsNuevoId();
        }

        public void VentaEDM(string NumReceta, string FolioEDM)
        {
            this.bEsEDM = true;
            this.sNumReceta = NumReceta;
            this.sFolioEDM = FolioEDM;
            bool bRegresa = false;
            clsLotes_ItemUUID itemUUID = new clsLotes_ItemUUID();

            InicializarPantalla();

            leer2.DataSetClase = Consultas.Ventas_EDM_Enc(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");

            if (leer2.Leer())
            {
                txtCte.Text = leer2.Campo("IdCliente");
                txtCte_Validating(this, null);
                txtSubCte.Text = leer2.Campo("IdsubCliente");
                txtSubCte_Validating(this, null);
                bRegresa = true;
            }

            if (bRegresa)
            {
                leer2.DataSetClase = Consultas.Ventas_EDM_Det(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");
                if (leer2.Leer())
                {
                    bRegresa = true;
                    myGrid.LlenarGrid(leer2.DataSetClase, false, false);

                    leer.DataSetClase = clsLotes.PreparaDtsLotes();
                    leer.DataSetClase = Consultas.Ventas_EDM_Det_Lotes(sEmpresa, sEstado, sFarmacia, FolioEDM, "VentaEDM()");
                    Lotes.AddLotes(leer.DataSetClase);

                    string sSql = string.Format("Select * From Ventas_EDM_Det__UUID (NoLock) " +
                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ", sEmpresa, sEstado, sFarmacia, FolioEDM);

                    leer.Exec(sSql);
                    while (leer.Leer())
                    {
                        itemUUID = new clsLotes_ItemUUID();
                        itemUUID.IdSubFarmacia = leer.Campo("IdSubFarmacia");
                        itemUUID.Codigo = leer.Campo("IdProducto");
                        itemUUID.CodigoEAN = leer.Campo("CodigoEAN");
                        itemUUID.ClaveLote = leer.Campo("ClaveLote");
                        Lotes.UUID_Add(leer.Campo("UUID"), itemUUID);
                    }
                }
            }

                // myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.BloqueaColumna(true, 1);

            btnNuevo.Enabled = false;

            this.ShowDialog();
        }

        private bool GuadarFolioVenta()
        {
            bool bRegresa = false;

            string sSql = string.Format("Update Ventas_EDM_Enc Set FolioVenta = '{4}' Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' \n",
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
                InicializarPantalla();
            }


            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            // Determinar si se muestra la Captura de Claves Solicitadas 
            lblMensajes.Text = "( F5 ) Datos Adicionales | Visualizar                                      ( F7 ) LOTES | Visualizar.";
            //if (GnFarmacia.CapturaDeClavesSolicitadasHabilitada)
            if(bCapturaDeClavesSolicitadasHabilitada)
            {
                //lblMensajes.Text = "( F5 ) Información Beneficiario Visualizar          ( F9 ) Captura Claves Solicitadas Visualizar            ( F7 ) Lotes. Visualizar";
                //lblMensajes.Text = "( F5 ) Información Beneficiario Visualizar          ( F9 ) Captura Claves Solicitadas Visualizar            ( F7 ) Lotes. Visualizar";
                lblMensajes.Text = "( F5 ) Datos Adicionales | Visualizar                      ( F7 ) LOTES | Visualizar.";
                lblMensajes.Text = "( F5 ) Datos Adicionales | Visualizar                      ( F7 ) LOTES | Visualizar.";
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
                        InicializarPantalla();
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
                    Modificar_CFG__ForzarCapturaEnMultiplosDeCajas();
                    break;

                case Keys.F10:
                    //    GnFarmacia.INT_OPM_ManejaOperacionMaquila = !GnFarmacia.INT_OPM_ManejaOperacionMaquila; 
                    Modificar_CFG__INT_OPM_ManejaOperacionMaquila();
                    break;

                ////case Keys.F4:
                ////    VentasDispensacion.FrmPDD_01_Dispensacion f = new VentasDispensacion.FrmPDD_01_Dispensacion();
                ////    f.Show();
                ////    break;

                case Keys.F5:
                    if (DtGeneral.EsEquipoDeDesarrollo)
                    {
                        DllFarmaciaSoft.FrmDecodificacionSNK fx = new DllFarmaciaSoft.FrmDecodificacionSNK();
                        fx.ShowDialog();
                    }
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
                    MostrarInfoVenta();
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
            if(Fecha.Exito)
            {
                GnFarmacia.Parametros.CargarParametros();
                Fecha.Close();

                Sesion = new FrmIniciarSesionEnCaja();
                Sesion.VerificarSesion();

                if(!Sesion.AbrirVenta)
                {
                    this.Close();
                }
                else
                {
                    Sesion.Close();
                    Sesion = null;
                    if(!bEsSurtimientoPedido && !bEsEDM)
                    {
                        InicializarPantalla();
                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        #region Teclas Standar
        //        case Keys.F3:
        //            if (btnNuevo.Enabled)
        //                btnNuevo_Click(null, null);
        //            break;

        //        case Keys.F6:
        //            if (btnGuardar.Enabled)
        //                btnGuardar_Click(null, null);
        //            break;

        //        case Keys.F8:
        //            if (btnCancelar.Enabled)
        //                btnCancelar_Click(null, null);
        //            break;

        //        case Keys.F10:
        //            // Ejecucion de procesos 
        //            break;

        //        case Keys.F12:
        //            if (btnImprimir.Enabled)
        //                btnImprimir_Click(null, null);
        //            break;
        //        #endregion Teclas Standar


        //        case Keys.F5:
        //            MostrarInfoVenta();
        //            break;

        //        case Keys.F7:
        //            mostrarOcultarLotes();
        //            break;

        //        default:
        //            base.OnKeyDown(e);
        //            break;
        //    }
        //}

        #region CargaParametroNewId
        private void CargarEsNuevoId()
        {
            string sSql = "";

            bEsNuevoId = false;

            sSql = string.Format(" SELECT * FROM Net_CFGC_Parametros (NOLOCK) \n" +
                                " WHERE IdEstado = '{0}' AND IdFarmacia = '{1}' AND ArbolModulo = 'PFAR' AND NombreParametro = 'EsNuevoId' \n",
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEsNuevoId()");
                General.msjError("Error al consultar Parametro.");
            }
            else
            {
                if (leer.Leer())
                {
                    bEsNuevoId = leer.CampoBool("Valor");
                }
            }
        }
        #endregion CargaParametroNewId

        #region Botones 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }
 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            //// Controlar en base al boton de Guardar 
            //if (txtFolio.Text.Trim() != "*" && !bEsEDM)
            //{
            //    btnCodificacion.Enabled = Guardar;
            //}
            btnGuardar.Enabled = bTieneSurtimientosActivos ? false : Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnRecetasElectronicas.Enabled = false;
            btnRecetaElectronica_Claves.Enabled = false;
            btnInformacionAIE.Enabled = false;
            
            if(btnGuardar.Enabled)
            {
                btnGuardar.Enabled = DtGeneral.ExistenTransferencias_EnTransito__AplicacionExpirada ? false : Guardar; 
            }

            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = bTienePermitidasVentasNormales;
            }

            if(btnGuardar.Enabled)
            {
                //btnGuardar.Enabled = GnFarmacia.DispensacionActiva;
            }

        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla(); 
        }

        private void InicializarPantalla()
        {
            SKU = new clsSKU();
            SKU.IdEmpresa = sEmpresa;
            SKU.IdEstado = sEstado;
            SKU.IdFarmacia = sFarmacia;
            SKU.TipoDeMovimiento = sIdTipoMovtoInv;


            bValidar_Perfil_x__Programa_SubPrograma = false;
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

            lblCte.Text = "";
            lblSubCte.Text = "";
            lblPro.Text = "";
            lblSubPro.Text = "";


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

            chkDesglosado.Visible = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;


            txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            CambiaEstado(true);
            fSubTotal = 0; fIva = 0; fTotal = 0;


            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpTipoDeInventario);
            Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion;
            Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia; 

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            InfCveSolicitadas = new clsClavesSolicitadas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = false;
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            IniciarToolBar(false, false, false);

            chkTipoImpresion.Visible = false;
            chkTipoImpresion.Checked = true; 
            chkMostrarImpresionEnPantalla.Checked = false;
            chkMostrarPrecios.Visible = false;
            chkMostrarPrecios.Checked = false;
            chk_RPT_Cajas.Enabled = false;
            chk_RPT_Cajas.Visible = false;
            chk_RPT_EtiquetasCajas.Enabled = false;
            chk_RPT_EtiquetasCajas.Visible = false;
            sFolioSurtido = !bEsSurtimientoPedido ? "": sFolioSurtido; 

            if (DtGeneral.EsAlmacen)
            {
                chkTipoImpresion.Visible = true;
                chkTipoImpresion.Checked = true;
                chkMostrarImpresionEnPantalla.Checked = true;
                //chkMostrarPrecios.Visible = true; 
            }


            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico) RecetaElectronica.Receta.Reset(); 
        




            txtFolio.Focus(); 
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            sMensajeErrorGrabar = "Ocurrió un error al guardar la información.";

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
                        Error.LogError(con.MensajeError);
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else 
                    {
                        IniciarToolBar(); 
                        con.IniciarTransaccion();

                        ////GuardaVenta();
                        ////if (tpPuntoDeVenta != TipoDePuntoDeVenta.AlmacenJurisdiccional)
                        ////{
                        ////    GuardaVentaInformacionAdicional();  // Guarda la informacion Adicional sobre Servicio, Area, Medico, Diagnostico, etc. 
                        ////}
                        ////else
                        ////{
                        ////    GuardaVenta_ALMJ_PedidosRC_Surtido();
                        ////}

                        if (GuardaVenta())
                        {
                            if (GuardaVentaInformacionAdicional())
                            {
                                if (GuardaDetallesVenta())
                                {
                                    //if (GuardaVentasDet_Lotes())
                                    //{
                                        if (GuardarClavesSolicitadas())
                                        {
                                            if (GuardarInformacionPreciosLicitacion())
                                            {
                                                bContinua = true; 
                                                if (bImplementaCodificacion)
                                                {
                                                    if (!CodificacionSNK.Guardar_UUIDS_Movimientos_De_Inventario(sFolioMovtoInv, leer, Lotes, true, true))
                                                    {
                                                        bContinua = false; 
                                                    }
                                                }

                                                if (bEsEDM && bContinua)
                                                {
                                                    bContinua = GuadarFolioVenta();
                                                }

                                                //// Atencion de pedidos especiales 
                                                if (bContinua && bEsSurtimientoPedido)
                                                {

                                                    if (bContinua)
                                                    {
                                                        bContinua = RevisarPedidoCompleto();
                                                    }

                                                    if (bContinua)
                                                    {
                                                        bContinua = ActualizarEstatusPedido();
                                                    }

                                                    if (bContinua)
                                                    {
                                                        bContinua = RegistrarAtencion();
                                                    }

                                                if (bContinua)
                                                    {
                                                        bContinua = AfectarExistenciaSurtidos();
                                                    }
                                                }
                                            }

                                            if (bContinua)
                                            {
                                                bContinua = AfectarExistencia(true, false);
                                            }

                                            if (bContinua)
                                            {
                                                bContinua = GuardarAtencion_RecetaElectronica();
                                            }
                                        }
                                    //}
                                }
                            }
                        }

                        if (bContinua)
                        {
                            con.CompletarTransaccion();
                            
                            //// IMach  // Enlazar el folio de inventario 
                            RobotDispensador.Robot.TerminarSolicitud(sFolioMovtoInv);


                            IniciarToolBar(false, false, true);

                            txtFolio.Text = SKU.Foliador;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            ImprimirInformacion();

                            /////// Jesús Díaz 2K120516.1305 
                            //// Solo farmacias configuradas para Emision de Vales
                            //// Se forza la generación automatica del Vale 
                            if (bEmiteValesAutomaticos)
                            {
                                GenerarValeAutomatico();
                            }

                            if (bEsSurtimientoPedido || bEsEDM)
                            {
                                this.Hide(); 
                            }
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*"; 
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError(sMensajeErrorGrabar);
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                        }
                        con.Cerrar();
                    }
                }
            }
        }

        private bool GuardaVenta()
        {
            bool bRegresa = false; 
            string sSql = "", sCaja = GnFarmacia.NumCaja;
            int iOpcion = 1;

            // Asignar el valor a la variable global 
            sFolioVenta = txtFolio.Text;

            CalcularTotales();
            SKU.Reset();

            // Grabar en los Movimientos de inventario
            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" + //"'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', \n" +
                "\t@Observaciones = '{8}', @SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta, sIdTipoMovtoInv, sTipoES, "", DtGeneral.IdPersonal, "", fSubTotal, fIva, fTotal, 1, SKU.SKU);

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
                    sFolioMovtoInv = leer.Campo("Folio");
                    SKU.FolioMovimiento = leer.Campo("Folio");
                    SKU.Foliador = leer.Campo("Foliador");
                    SKU.SKU = leer.Campo("SKU");
                    sFolioVenta = SKU.Foliador;


                    sSql = String.Format("Set DateFormat YMD \nExec spp_Mtto_VentasEnc \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @FechaSistema = '{4}', @IdCaja = '{5}', \n" +
                        "\t@IdPersonal = '{6}', @IdCliente = '{7}', @IdSubCliente = '{8}', @IdPrograma = '{9}', @IdSubPrograma = '{10}', \n" +
                        "\t@SubTotal = '{11}', @Iva = '{12}', @Total = '{13}', @TipoDeVenta = '{14}', @iOpcion = '{15}', @Descuento = '{16}' \n",
                        DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                        sFolioVenta,
                        sFechaSistema, Fg.PonCeros(sCaja, 2), DtGeneral.IdPersonal, 
                        Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                        Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4),
                        fSubTotal, fIva, fTotal, (int)TipoDeVenta.Credito, iOpcion, 0);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        bRegresa = leer.Leer(); 
                        sFolioVenta = String.Format("{0}", leer.Campo("Clave"));
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                    }
                }
            }

            return bRegresa; 
        }

        private bool GuardarInformacionPreciosLicitacion()
        {
            bool bRegresa = true; 
            // 2K110426-1004  
            string sSql = string.Format("Exec spp_Mtto_Ventas_AsignarPrecioLicitacion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
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

            ////if (GnFarmacia.ImplementaDigitalizacion)
            ////{
            ////    sSql += string.Format("Exec spp_Mtto_VentasDigitalizacion " +
            ////        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', " +
            ////        " @ImagenComprimida = '{4}', @ImagenOriginal = '{5}', @Ancho = '{6}', @Alto = '{7}', @iOpcion = '{8}' ",
            ////         DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
            ////        sFolioVenta, InfVtas.Imagen_Comprimida, InfVtas.Imagen_Original, InfVtas.ImagenDigitalizacion.Width, InfVtas.ImagenDigitalizacion.Height, 1);
            ////}

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool GuardarAtencion_RecetaElectronica()
        {
            bool bRegresa = true;

            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
            {
                if (RecetaElectronica.Receta.InformacionCargada)
                {
                    bRegresa = false;
                    RecetaElectronica.Receta.FolioVenta = sFolioVenta;
                    bRegresa = RecetaElectronica.Receta.RegistrarAtencion(leer, sFolioVenta);
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
                    IdEmpresaCSGN, IdEstadoCSGN, IdJurisdiccionCSGN, IdFarmaciaCSGN, sFolioPedidoRC, sEmpresa, sEstado, sFarmacia, sFolioVenta, iCantidadSurtidaCSGN, iCantidadEntregada, iOpcion);

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
                dPrecioUnitario = myGrid.GetValueDou(i, (int)Cols.Precio);
                dImpteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.UltimoCosto);

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

                    if (bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n\n", sSql);
                    }

                    if (!bGuardadoDeInformacion_Masivo)
                    {
                        bRegresa = leer.Exec(sSql);
                        if (!bRegresa)
                        {
                            bRegresa = false;
                            break;
                        }
                    }

                    if (bRegresa)
                    {
                        // Grabar en los Movimientos de inventario 
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" + //" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', @IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', \n" +
                            "\t@TasaIva = '{7}', @Cantidad = '{8}', @Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovtoInv, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            dTasaIva, iCantVendida, dCostoUnitario, (iCantVendida * dCostoUnitario), 'A');


                        if (bGuardadoDeInformacion_Masivo)
                        {
                            sSql_Detallado += string.Format("{0}\n\n", sSql);
                        }

                        if (!bGuardadoDeInformacion_Masivo)
                        {
                            bRegresa = leer.Exec(sSql);
                            if (!bRegresa)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardaVentasDet_Lotes();
            }

            if (bRegresa && bGuardadoDeInformacion_Masivo)
            {
                bRegresa = leer.Exec(sSql_Detallado);
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
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.UltimoCosto);

                iRenglon = i;                   
                //ObtieneClaveLote(sIdProducto, sCodigoEAN, ref sClaveLote);

                clsLotes[] ListaLotes = Lotes.Lotes(sIdProducto, sCodigoEAN);

                if (sCodigoEAN == "0000000000000" && myGrid.Rows == 1)
                {
                    bRegresa = true;
                    break;
                }

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

                        if (bGuardadoDeInformacion_Masivo)
                        {
                            sSql_Detallado += string.Format("{0}\n\n", sSql);
                        }

                        if (!bGuardadoDeInformacion_Masivo)
                        {
                            bRegresa = leer.Exec(sSql);
                            if (!bRegresa)
                            {
                                bRegresa = false;
                                break;
                            }
                        }

                        if (bRegresa)
                        {

                        //if (!leer.Exec(sSql)) 
                        //{
                        //    bRegresa = false;
                        //    break;
                        //}
                        //else
                        //{
                            // Grabar en los Movimientos de inventario 
                            sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" + //" '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', \n" +
                                "\t@Cantidad = '{8}', @Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n", 
                                DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv, 
                                sIdProducto, sCodigoEAN, L.ClaveLote, L.Cantidad, dCostoUnitario, (L.Cantidad * dCostoUnitario), 'A', L.SKU);

                            if (bGuardadoDeInformacion_Masivo)
                            {
                                sSql_Detallado += string.Format("{0}\n\n", sSql);
                            }

                            if (!bGuardadoDeInformacion_Masivo)
                            {
                                bRegresa = leer.Exec(sSql);
                                if (!bRegresa)
                                {
                                    bRegresa = false;
                                    break;
                                }
                            }

                            if (bRegresa)
                            {
                            //    bRegresa = leer.Exec(sSql);
                            //if(!bRegresa)
                            //{
                            //    bRegresa = false;
                            //    break;
                            //}
                            //else
                            //{
                                if(GnFarmacia.ManejaUbicaciones)
                                {
                                    bRegresa = GuardaVentasDet_Lotes_Ubicaciones(L, iRenglon, iOpcion);

                                    if(!bRegresa)
                                    {
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

        private bool GuardaVentasDet_Lotes_Ubicaciones(clsLotes Lote, int Renglon, int iOpcion)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotesUbicacionesItem[] Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);

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

                    if (bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n\n", sSql);
                    }

                    if (!bGuardadoDeInformacion_Masivo)
                    {
                        bRegresa = leer.Exec(sSql);
                        if (!bRegresa)
                        {
                            bRegresa = false;
                            break;
                        }
                    }

                    if (bRegresa)
                    {
                    //    if (!leer.Exec(sSql))
                    //{
                    //    bRegresa = false;
                    //    break;
                    //}                                            
                    //else
                    //{
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones " + // " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', \n" +
                        "\t@Cantidad = '{8}', @IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n", 
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', L.SKU);

                        if (bGuardadoDeInformacion_Masivo)
                        {
                            sSql_Detallado += string.Format("{0}\n\n", sSql);
                        }

                        if (!bGuardadoDeInformacion_Masivo)
                        {
                            bRegresa = leer.Exec(sSql);
                            if (!bRegresa)
                            {
                                bRegresa = false;
                                break;
                            }
                        }

                        //bRegresa = leer.Exec(sSql);
                        //if(!bRegresa)
                        //{
                        //    bRegresa = false;
                        //    break;
                        //}
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

            if ( DtGeneral.EsAlmacen || bCapturaDeClavesSolicitadasHabilitada ) // Revisar el Parametro 
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
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @IdClaveSSA = '{4}', @CantidadRequerida = '{5}', @Observaciones = '{6}' \n",
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
                    bRegresa = CalcularSurtimientoClavesSolicitadas();
                }
            }

            return bRegresa; 
        }

        private bool CalcularSurtimientoClavesSolicitadas()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta );
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            ////bContinua = bRegresa; 
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
                "Select \n" +
                "\tClaveSSA, IdClaveSSA, DescripcionSal, IdPresentacion, Presentacion, \n" +
                "\tcast(V.CantidadRequerida as int) as Cantidad, \n" +
                "\tdbo.fg_Existencia_Clave(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.ClaveSSA) as ExistenciaClave \n" +
                "From vw_Impresion_Ventas_ClavesSolicitadas V (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' and EsCapturada = 1 and Clave_CB = 1 \n",
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
            ////    if (bEmiteValesAutomaticos && RequiereVale())
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

        private void CalcularTotales()
        {
            double sSubTotal = 0, sIva = 0, sTotal = 0;

            //fSubTotalIva_0 = 0;
            //fSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            //fIva = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva);
            //fTotal = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);

            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                sSubTotal = myGrid.GetValueDou(i, 7);
                fSubTotal = fSubTotal + sSubTotal;
                sIva = myGrid.GetValueDou(i, 8);
                fIva = fIva + sIva;
                sTotal = myGrid.GetValueDou(i, 9);
                fTotal = fTotal + sTotal;
            }
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
                Inv = AfectarInventario.Aplicar;

            if (AfectarCosto)
                Costo = AfectarCostoPromedio.Afectar;

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' ",
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

        private void ImprimirInformacion() 
        {
            bool bContinua = false;

            sFolioVenta = Fg.PonCeros(txtFolio.Text, 8);
            //VtasImprimir.MostrarVistaPrevia = false;//chkMostrarImpresionEnPantalla.Checked;
            //VtasImprimir.NumeroDeCopias = iNumeroDeCopias; 
            ////VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket; 

            //if (DtGeneral.EsAlmacen)
            //{
            //    VtasImprimir.MostrarImpresionDetalle = chkTipoImpresion.Checked;
            //    VtasImprimir.MostrarPrecios = chkMostrarPrecios.Checked; 
            //}

            //if (VtasImprimir.Imprimir(sFolioVenta, "", 0.0000, true))////chkDesglosado.Checked))
            //{
            //    bContinua = true;
            //    ////if (bGeneroVale)
            //    ////{
            //    ////    bContinua = VtasImprimir.ImprimirVale(sFolioVale); 
            //    ////}
            //}

            DatosCliente.Funcion = "ImprimirInformacion()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;

            if(DtGeneral.EsAlmacen) 
            { 
                myRpt.NombreReporte = "PtoVta_TicketCredito_Detallado.rpt"; 
            }
            else
            {
                myRpt.NombreReporte = "PtoVta_TicketCredito.rpt";
            }
            
            myRpt.TituloReporte = "Informe Dispersion de Insumos";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", sFolioVenta);

            bContinua = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bContinua)
            {
                ////if (chk_RPT_Cajas.Checked)
                ////{
                ////    ImprimirRptCajas();
                ////}

                ////if (chk_RPT_EtiquetasCajas.Checked && sFolioSurtido != "")
                ////{
                ////    ImprimirEtiquetas_Pedido(); 
                ////}

                if(!bEsSurtimientoPedido)
                {
                    //btnNuevo_Click(this, null);
                    InicializarPantalla(); 
                }
            }
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
#endregion Botones

                        #region Validaciones
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sFolio = "";  // sSql = "", 
            bFolioGuardado = false;
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;

                if(bEsNuevoId)
                {
                    //Cargar datos Cte, SubCte, Pro, SubPro
                    CargarDatosDispersion();
                }
                else
                {
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
                        IniciarToolBar(false, false, true);
                        btnInformacionAIE.Enabled = GnFarmacia.ImplementaCapturaInformacionAEI; 

                        sFolio = leer.Campo("Folio");
                        sFolioVenta = sFolio;
                        txtFolio.Text = sFolio;
                        sFolioMovtoInv = "SV" + sFolio;

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
                        CargaDetallesVenta();
                        BuscarVale();
                        CambiaEstado(false);

                        if (DtGeneral.EsAlmacen)
                        {
                            //ActivarRptCaja();
                        }
                    }
                }
            }
        }

        #region CargaCte_SubCte_ProSub_Pro
        private void CargarDatosDispersion()
        {
            string sSql = "", sCliente = "", sSubCliente = "", sPrograma = "", sSubPrograma = "";

            bool bRead = false;

            sSql = string.Format(" SELECT SUBSTRING(Valor, 1, 4) AS Cliente, SUBSTRING(Valor, 5, 4) AS SubCliente, \n" +
                                " SUBSTRING(Valor, 9, 4) AS Programa, SUBSTRING(Valor, 13, 4) AS SubPrograma \n" +
                                " FROM Net_CFGC_Parametros (NOLOCK) \n" +
                                " WHERE IdEstado = '{0}' AND IdFarmacia = '{1}' AND ArbolModulo = 'PFAR' AND NombreParametro = 'DatosDispersion' \n",
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "CargarDatosDispersion()");
                General.msjError("Error al consultar Parametro Dispersión.");
            }
            else
            {
                if (leer2.Leer())
                {
                    bRead = true;
                    sCliente = leer2.Campo("Cliente");
                    sSubCliente = leer2.Campo("SubCliente");
                    sPrograma = leer2.Campo("Programa");
                    sSubPrograma = leer2.Campo("SubPrograma");
                }
            }

            if(bRead)
            {
                txtCte.Text = sCliente;
                txtCte_Validating(null, null);

                txtSubCte.Text = sSubCliente;
                txtSubCte_Validating(null , null);

                txtPro.Text = sPrograma;
                txtPro_Validating(null , null);

                txtSubPro.Text = sSubPrograma;
                txtSubPro_Validating(null, null);
            }
        }
        #endregion CargaCte_SubCte_ProSub_Pro

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            bEsSeguroPopular = false; 
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

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
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

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

                    bPermitirCapturaBeneficiariosNuevos = GnFarmacia.ValidarCapturaBeneficiariosNuevos(bPermitirCapturaBeneficiariosNuevos);


                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblSubCte, lblSubCte.Text);

                    if (txtFolio.Text.Trim() == "*" && !bEsEDM)
                    {
                        btnCodificacion.Enabled = btnGuardar.Enabled;
                    }

                    btnRecetasElectronicas.Enabled = GnFarmacia.ImplementaInterfaceExpedienteElectronico;


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
            bValidar_Perfil_x__Programa_SubPrograma = false; 
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
                    bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = leer.CampoBool("Dispensacion_CajasCompletas");
                    toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                    ////// Obtener datos de IMach 
                    sFolioSolicitud = RobotDispensador.Robot.ObtenerFolioSolicitud();

                    //// Exclusivo Seguro Popular 
                    if (bEsSeguroPopular)
                    {
                        MostrarInfoVenta();
                    }

                    if (!bEsSurtimientoPedido && !bEsEDM)
                    {
                        myGrid.Limpiar(true);
                        btnCodificacion.Enabled = (bImplementaCodificacion || bImplementaReaderDM);

                        if (btnCodificacion.Enabled)
                        {
                            myGrid.BloqueaGrid(true);
                        }
                    }

                    Validar___Perfil_x__Programa_SubPrograma(); 
                }
            }
            else
            {
                txtSubPro.Text = "";
                lblSubPro.Text = "";
                toolTip.SetToolTip(lblSubPro, "");
            }            
        }

        private void Validar___Perfil_x__Programa_SubPrograma()
        {
            string sSql = string.Format(
                "Select top 1 * \n"+
                "From CFG_CB_Sub_CuadroBasico_Claves (NoLock) \n" +
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdPrograma = '{3}' and IdSubPrograma = '{4}' and Status = 'A' \n", 
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtCte.Text, txtPro.Text, txtSubPro.Text );

            bValidar_Perfil_x__Programa_SubPrograma = false;
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Validar___Perfil_x__Programa_SubPrograma()");
                General.msjError("Ocurrió un error al obtener la información de las Claves del SubPerfil.");
            }
            else
            {
                bValidar_Perfil_x__Programa_SubPrograma = leer.Leer();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            if (!bEsSurtimientoPedido)
            {
                if (DtGeneral.TieneSurtimientosActivos())
                {
                    bRegresa = false;
                    General.msjAviso(sMensajeConSurtimiento);
                }
            }

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


            ////////////// Validar consumo por Clave 
            if (bRegresa)
            {
                if (bValidarConsumoDeClaves_Programacion)
                {
                    VerificarSubPerfil = new clsVerificarCantSubPerfil(true, InfVtas.TipoDispensacion);
                    bRegresa = VerificarSubPerfil.VerificarCantidadesConExceso(Lotes,
                        Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                        Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4));
                }
            }

            if (bRegresa)
            {
                if (bValidarConsumoDeClaves_ProgramaAtencion)
                {
                    VerificarSubPerfil = new clsVerificarCantSubPerfil(false, InfVtas.TipoDispensacion);
                    bRegresa = VerificarSubPerfil.VerificarCantidadesConExceso(Lotes,
                        Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                        Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4));
                }
            }
            ////////////// Validar consumo por Clave 


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

            //////if (bRegresa && bEmiteValesAutomaticos)
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

        private bool CargaDetallesVenta()
        {
            bool bRegresa = true;

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

        private void ActivarRptCaja()
        {
            bool bActivar = false;
            string sSql = ""; 

            sSql = string.Format("Select * From Pedidos_Cedis_Enc_Surtido (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioTransferenciaReferencia = '{3}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioMovtoInv);

            if (!bEsSurtimientoPedido)
            {
                sFolioSurtido = "";
            }

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActivarRptCaja()");
                General.msjError("Error al buscar la clave lote");
            }
            else
            {
                bActivar = leer.Leer();
                sFolioSurtido = leer.Campo("FolioSurtido"); 

                chk_RPT_Cajas.Enabled = bActivar;
                chk_RPT_Cajas.Visible = bActivar;
                chk_RPT_Cajas.Checked = bActivar;

                chk_RPT_EtiquetasCajas.Enabled = bActivar;
                chk_RPT_EtiquetasCajas.Visible = bActivar; 
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
            myRpt.Add("@Folioreferencia", sFolioMovtoInv);


            myRpt.NombreReporte = "PtoVta_Caja_Embarque";

            myRpt.EnviarAImpresora = !chkMostrarImpresionEnPantalla.Checked;
            ////myRpt.CargarReporte(true, true);
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
                etiquetas.GenerarEtiquetaSurtido(this.MdiParent, sFolioSurtido, true); 
            }
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
            return CargaDetallesGeneraVenta(FarmaciaPedido, FolioPedido, FolioSurtido, 1);
        }

        public bool CargaDetallesGeneraVenta(string FarmaciaPedido, string FolioPedido, string FolioSurtido, int Registros)
        {
            bool bRegresa = false;
            bEsSurtimientoPedido = true;
            sFarmaciaPed = FarmaciaPedido;
            sFolioSurtido = FolioSurtido;
            sFolioPedido = FolioPedido;
            iRegistros = Registros;

            InicializarPantalla();

            leer2.DataSetClase = Consultas.PedidosEspeciales_GenerarVentaEnc(sEmpresa, sEstado, sFarmacia, FolioPedido, "CargaDetallesGeneraVenta");

            if (leer2.Leer())
            {
                txtCte.Text = leer2.Campo("IdCliente");
                txtCte_Validating(this, null);
                txtSubCte.Text = leer2.Campo("IdsubCliente");
                txtSubCte_Validating(this, null);

                InfVtas.EsPedido = true; 
                InfVtas.NumReceta = leer2.Campo("ReferenciaInternaPedido");
                InfVtas.Beneficiario = leer2.Campo("IdBeneficiario");
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

                btnNuevo.Enabled = false; 
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
            bool bRegresa = false;
            int iFolios = 0;

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Actualizar_Status \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @FolioTransferenciaReferencia = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, sFolioMovtoInv);

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
                        sMensajeErrorGrabar = "El Surtido ya tiene un folio de venta asignado.";
                    }
                }
            }

          
            return bRegresa;
        }

        private bool RevisarPedidoCompleto()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', @FolioPedido = '{4}' ",
                sEmpresa, sEstado, sFarmacia, sFarmaciaPed, sFolioPedido); 

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool AfectarExistenciaSurtidos()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @TipoFactor = 2, @Validacion_Especifica = 1  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioSurtido);

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private bool RegistrarAtencion()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = [{3}], @IdPersonal = '{4}', @Observaciones = '{5}' ",
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
        private void validarSeleccionRecetaElectronica()
        {
            bool bActivar = false; 

            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
            {
                if (RecetaElectronica.Receta.InformacionCargada)
                {
                    bActivar = myGrid.GetValue(1, (int)Cols.CodEAN) == "";
                }
            }

            btnRecetasElectronicas.Enabled = bActivar; 
        }

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
                                if (!(bImplementaCodificacion && bImplementaReaderDM))
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
                if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
                {
                    myGrid.Rows = myGrid.Rows + 1;
                    myGrid.ActiveRow = myGrid.Rows;
                    myGrid.SetActiveCell(myGrid.Rows, 1);
                    ObtenerDatosProducto();
                }
            }
        }

        private void grdProductos_KeyDown_1(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow; 

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
                                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, Cols.CodEAN);
                                sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

                                leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "", 
                                    bDispensarSoloCuadroBasico, bValidar_Perfil_x__Programa_SubPrograma, txtCte.Text, txtSubCte.Text, txtPro.Text, txtSubPro.Text, false, "grdProductos_KeyDown_1");
                                if (leer.Leer())
                                {
                                    myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                                    ObtenerDatosProducto();
                                    //CargarDatosProducto();
                                }
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            if (!bEsSurtimientoPedido)
                            {
                                if (!bEsIdProducto_Ctrl)
                                {
                                    removerLotes();
                                }
                                validarSeleccionRecetaElectronica();
                            }
                        }

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}

                        //// Administracion de Mach4 
                        if (e.KeyCode == Keys.F10)
                        {
                            if (bEsClienteInterface && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
                            {
                                string sIdProducto = myGrid.GetValue(iRowActivo, (int)Cols.Codigo);
                                string sCodigoEAN = myGrid.GetValue(iRowActivo, (int)Cols.CodEAN);
                                
                                if ( sIdProducto != "" )
                                {
                                    if (RobotDispensador.Robot.Show(sIdProducto, sCodigoEAN))
                                    {
                                        mostrarOcultarLotes();
                                    }
                                }
                            }
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
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
            string sCodigo = "", sSql = ""; 
            bool bCargarDatosProducto = true;
            string sMsj = "";

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
                    sSql = string.Format(
                        "Exec Spp_ProductoVentasFarmacia \n" +
                            "\t@Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', \n" +
                            "\t@IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', \n" + 
                            "\t@ClavesRecetaElectronica = '{9}',  \n" +
                            "\t@INT_OPM_ProcesoActivo = '{10}', \n" +
                            "\t@Validar_ClavesDe_SubPerfil = '{11}', @IdPrograma = '{12}', @IdSubPrograma = '{13}' ",
                            (int)TipoDeVenta.Credito, txtCte.Text.Trim(), txtSubCte.Text.Trim(),
                            Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                            Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, Convert.ToInt32(bEsClienteInterface), sListaClavesSSA_RecetaElectronica, 
                            Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo), Convert.ToInt32(bValidar_Perfil_x__Programa_SubPrograma), txtPro.Text, txtSubPro.Text 
                        );
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

                                if(bCargarDatosProducto && bValidar_Perfil_x__Programa_SubPrograma)
                                {
                                    if(!leer.CampoBool("DCB__Programa_SubPrograma"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = string.Format("La Clave SSA {0} no esta dentro del Cuadro del Programa-SubPrograma.", leer.Campo("ClaveSSA"));
                                        //sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Básico Autorizado, verifique.";
                                    }
                                }
                            }

                            if (bCargarDatosProducto && GnFarmacia.ImplementaInterfaceExpedienteElectronico)
                            {
                                if (RecetaElectronica.Receta.InformacionCargada)
                                {
                                    if (!leer.CampoBool("EsDeRecetaElectronica"))
                                    {
                                        bCargarDatosProducto = false;
                                        sMsj = string.Format("La Clave SSA {0} no esta incluida en la receta electrónica.", leer.Campo("ClaveSSA"));
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
                                CargaDatosProducto(Renglon, BuscarInformacion);
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

            validarSeleccionRecetaElectronica(); 
        }

        private void ActualizarColorFondo()
        {
            if (bEsClienteInterface)
            {
                FrmColorProductosIMach myColor = new FrmColorProductosIMach();
                myColor.ShowDialog();
                Color colorBack = GnFarmacia.ColorProductosIMach; 

                for (int i = 1; i<= myGrid.Rows; i++)
                {
                    if ( myGrid.GetValueBool(i, (int)Cols.EsIMach4) )
                    {
                        myGrid.ColorRenglon(i, colorBack); 
                    }
                }
            }
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
            int iRowActivo = Renglon; //// myGrid.ActiveRow;           
            int iColEAN = (int)Cols.CodEAN;
            bool bEsMach4 = false; 
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
                            myGrid.SetValue(iRowActivo, (int)Cols.Precio, leer.Campo("PrecioVenta"));
                            myGrid.SetValue(iRowActivo, (int)Cols.UltimoCosto, leer.Campo("UltimoCosto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0); 

                            bEsMach4 = leer.CampoBool("EsMach4");
                            myGrid.SetValue(iRowActivo, (int)Cols.EsIMach4, bEsMach4);

                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

                            // Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                            if (bEsClienteInterface)
                            {
                                if (bEsMach4)
                                {
                                    GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
                                    RobotDispensador.Robot.Show(leer.Campo("IdProducto"), sCodEAN);
                                }
                            }

                            Application.DoEvents(); //// Asegurar que se refresque la pantalla 
                            CargarLotesCodigoEAN(BuscarInformacion);


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

        private bool CargarDatosProducto()
        {
            bool bRegresa = true;
            ////int iRow = myGrid.ActiveRow;
            ////int iColEAN = (int)Cols.CodEAN;
            ////string sCodEAN = leer.Campo("CodigoEAN");

            ////if (sValorGrid != sCodEAN)
            ////{
            ////    if (!myGrid.BuscaRepetido(sCodEAN, iRow, iColEAN))
            ////    {
            ////        // No modificar la informacion capturada en el renglon si este ya existia
            ////        myGrid.SetValue(iRow, iColEAN, sCodEAN);
            ////        myGrid.SetValue(iRow, (int)Cols.Descripcion, leer.Campo("Descripcion"));
            ////        myGrid.SetValue(iRow, (int)Cols.TasaIva, leer.Campo("TasaIva"));

            ////        //if (sIdProGrid != leer.Campo("CodigoEAN"))
            ////        //if (sValorGrid != leer.Campo("CodigoEAN"))
            ////        {
            ////            sIdProGrid = leer.Campo("IdProducto");
            ////            myGrid.SetValue(iRow, (int)Cols.Codigo, sIdProGrid);
            ////            myGrid.SetValue(iRow, (int)Cols.Cantidad, 0);
            ////            myGrid.SetValue(iRow, (int)Cols.Precio, 0);
            ////            myGrid.SetValue(iRow, (int)Cols.TipoCaptura, "0");
            ////        }
            ////        CargarLotesCodigoEAN();
            ////    }
            ////    else
            ////    {
            ////        General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
            ////        myGrid.LimpiarRenglon(iRow);
            ////        myGrid.SetActiveCell(iRow, iColEAN);
            ////    }
            ////}
            ////else
            ////{
            ////    // Asegurar que no cambie el CodigoEAN
            ////    myGrid.SetValue(iRow, iColEAN, sCodEAN);
            ////}

            return bRegresa;
        }

                        #endregion Grid

                        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            CargarLotesCodigoEAN(true); 
        }

        private void CargarLotesCodigoEAN(bool BuscarInformacion)
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

                    mostrarOcultarLotes();
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
                    //if (!(bImplementaCodificacion && bImplementaReaderDM))
                    //{
                    //    myGrid.BloqueaGrid(true);
                    //}
                }
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

                    if ((bImplementaCodificacion && bImplementaReaderDM))
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

                    //////// Administracion de Mach4
                    //////if (IMach4.EsClienteIMach4 && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    //////{
                    //////    if (IMachPtoVta.RequisicionRegistrada)
                    //////        Lotes.Show(); 
                    //////}
                    //////else 
                    {
                        Lotes.IdCliente = txtCte.Text;
                        Lotes.IdSubCliente = txtSubCte.Text;
                        Lotes.IdPrograma = txtPro.Text;
                        Lotes.IdSubPrograma = txtSubPro.Text;
                        Lotes.ProgramaSubPrograma_ForzarCapturaEnMultiplosDeCajas = bForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma; 
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
                InfCveSolicitadas.Show(txtCte.Text, txtSubCte.Text, txtFolio.Text);
                //InfCveSolicitadas.Claves(); 
            }
        }

        private void MostrarInfoVenta()
        {
            MostrarInfoVenta(false); 
        }

        private void MostrarInfoVenta(bool CierreAutomatico) 
        {
            //bool bRegresa = true;

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

                if (!GnFarmacia.ImplementaInterfaceExpedienteElectronico)
                {
                    InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
                }
                else
                {
                    if (RecetaElectronica.Receta.InformacionCargada)
                    {
                        InfVtas.BloquearControles = true; 
                        InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text,
                            RecetaElectronica.Receta.IdBeneficiario, RecetaElectronica.Receta.FolioReceta, RecetaElectronica.Receta.FechaReceta,
                            RecetaElectronica.Receta.IdMedico, RecetaElectronica.Receta.CIE_10, RecetaElectronica.Receta.Servicio, RecetaElectronica.Receta.Area,
                            RecetaElectronica.Receta.TipoDeSurtimiento, CierreAutomatico);

                        RecetaElectronica.Receta.CIE_10 = InfVtas.Diagnostico;
                    }
                    else
                    {
                        InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
                    }
                }
            } 
            //else
            //{
            //}
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
                    myGrid.SetValue(iRows, (int)Cols.CodEAN, sCodigoEAN_SNK);

                    ObtenerDatosProducto(iRows, false);
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
                        #endregion Codificacion Datamatrix

        private void FrmVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GnFarmacia.ImplementaInterfaceExpedienteElectronico)
            {
                RecetaElectronica.Receta.CerrarDetallesDeReceta();
            }
        }

        private void FrmVentas_Shown(object sender, EventArgs e)
        {
            if( !bEsSurtimientoPedido )
            {
                if(DtGeneral.TieneSurtimientosActivos())
                {
                    General.msjAviso(sMensajeConSurtimiento);
                    bTieneSurtimientosActivos = true;
                }

                if (!bTieneSurtimientosActivos)
                {
                    if (DtGeneral.EsAlmacen)
                    {
                        //clsCriptografo crypto = new clsCriptografo();
                        ////string sMD5 = ""; sMD5 = crypto.Encriptar("14|0010|A|2070-04-30", 17);
                        //string sMD5Decryp = "";
                        //sMD5Decryp = crypto.Desencriptar("Uvlxg”Ko\u008fdSY‘mˆ–Psc\u007fsolutionsQQœUPPRœaœRPRSMPTMSPpKxj‡uaWaWŽƒeQ‘\u008fš", 17);

                        if (!DtGeneral.Almacen_PermisoEspecial())
                        {
                            General.msjAviso("La opción de ventas no esta habilitada para guardar.");
                            bTienePermitidasVentasNormales = false;
                        }
                    }


                    if (!GnFarmacia.DispensacionActiva_Verificar())
                    {
                        //General.msjAviso("La dispensación de Venta esta deshabilitada, sólo es posible dispensar Consignación.");
                        General.msjAviso(GnFarmacia.DispensacionActiva_Mensaje());
                    }
                }
            }
        }
    }
}
