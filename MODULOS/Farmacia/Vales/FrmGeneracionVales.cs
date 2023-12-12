using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; 
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.QRCode.Codec;
using SC_SolutionsSystem.QRCode;

using DllFarmaciaSoft.QRCode;
using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Devoluciones;
using Farmacia.Procesos;
using Farmacia.Ventas;

//using Dll_IMach4;
//using Dll_IMach4.Interface;
using Dll_ValesFirmaElectronica;
using Dll_ValesFirmaElectronica.GeneracionDeFolios;

namespace Farmacia.Vales
{
    public partial class FrmGeneracionVales : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, IdPresentacion = 4, Presentacion = 5, Cantidad = 6
        }

        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        string sFolioSolicitud = "";

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsInformacionVentas InfVtas;
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsLeer leerVale;
        clsLeer leerChecador;
        clsLeer leerHuellas;
        QR_Reporte Impresion;
        QR_Reader reader;
        TipoDePuntoDeVenta tpPuntoDeVenta = TipoDePuntoDeVenta.Farmacia_Almacen;

        /// <summary>
        /// Firma Electronica Emision Vales
        /// </summary>
        clsGenerarQR_Folios QR_FirmaElectronica;

        clsGrid myGrid;
        // Variables Globales  ****************************************************
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bImportarBeneficiarios = false;
        bool bCapturaDeClavesSolicitadasHabilitada = GnFarmacia.CapturaDeClavesSolicitadasHabilitada;
        bool bEsReimpresion = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "";
        string sReferencia = "";
        string sUrlValidarHuellas = "";
        string sHost = "";
        string sMd5 = "";
        bool bTieneHuella = false;

        bool bContinua = true; 
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");

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
        int iNumeroDeCopias = GnFarmacia.NumeroDeCopiasVales; 

        DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;
        bool bConexionHuellasEstablecida = false; 

        bool bFolioGuardado = false;
        bool bHabilitarCaptura = true;
        bool EsPersonalFarmacia = false;

        bool bFirmaHuella = GnFarmacia.FirmaVales;
        bool bValeElectronico = GnFarmacia.Maneja_ValesElectronicos;
        bool bManejaVales_ServicioDomicilio = GnFarmacia.Maneja_ValesServicioDomicilio;
        bool bMostrarConfirmacion_ServicioDomicilio = false;
        bool bValeValido_ServicioDomicilio = false; 
        string sIdBeneficiario = "";
        string sFolioServicioDomicilio = "";
        string sGenerar_ServicioDomicilio = "Generar servicio a domicilio";
        string sRegistro_ServicioDomicilio = "Registrar servicio a domicilio";

        int iFolioTimbre = 0;
        string sCadenaInteligente = "";
        string sQR_FirmaElectronica = "";
        MemoryStream ms = new MemoryStream(); 

        #region variables
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = ""; 
        bool bEmiteVales = GnFarmacia.EmisionDeValesCompletos;
        bool bEmiteValesManuales = GnFarmacia.EmisionDeValesManuales;
        string sFolioVale = "";

        // FrmIniciarSesionEnCaja Sesion;
        #endregion variables

        public FrmGeneracionVales()
        {
            // MessageBox.Show(Application.OpenForms.Count.ToString());
            InitializeComponent();

            ////General.msjUser("0000");
            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);
            leerVale = new clsLeer();

            ////General.msjUser("0001");
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            ////General.msjUser("0002");
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Credito);

            ////General.msjUser("0003");
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true; 
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            toolStripSeparator_03.Visible = false;
            btnConfirmarHuella.Visible = false;

            toolStripSeparator_04.Visible = bManejaVales_ServicioDomicilio;
            btnServicioADomicilio.Visible = bManejaVales_ServicioDomicilio;

            ////General.msjUser("0004");

            if (bFirmaHuella)
            {
                FP_General.Conexion = General.DatosConexion;

                validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
                validarHuella.Url = General.Url;
                leerChecador = new clsLeer(ref con);
            }
            ////General.msjUser("0005");
        }

        private void FrmGeneracionVales_Load(object sender, EventArgs e)
        {
            ////if (bSeGeneraValeAutomatico)
            ////{
            ////    btnGuardar.Enabled = true; 
            ////    btnNuevo.Enabled = false;
            ////    btnImprimir.Enabled = false;
            ////    btnCancelar.Enabled = false; 
            ////}
            ////else 
            if (!bSeGeneraValeAutomatico)
            {
                InicializarPantalla();
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;

                //Para obtener Empresa Estado y Farmacia
                sEmpresa = DtGeneral.EmpresaConectada;
                sEstado = DtGeneral.EstadoConectado;
                sFarmacia = DtGeneral.FarmaciaConectada;
            }

            ////tmSesion.Enabled = true;
            ////tmSesion.Start();

            if (bFirmaHuella)
            {
                FP_General.TablaHuellas = "FP_Huellas_Vales";
                if (Obtener_Url_Firma())
                {
                    if (validarDatosDeConexion())
                    {
                        ConexionValidarHuellas();
                    }
                }
            }            
        }

        #region Generacion Automática de Vales 
        bool bSeGeneraValeAutomatico = false;

        public void GenerarValeAutomaticamente(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string IdPersonal, string NombrePersonal )
        {
            if (Validar_Existe_FoliosTimbres())
            {
                bSeGeneraValeAutomatico = true;
                InicializarPantalla();

                sEmpresa = IdEmpresa;
                sEstado = IdEstado;
                sFarmacia = IdFarmacia;
                txtIdPersonal.Text = IdPersonal;
                lblPersonal.Text = NombrePersonal;

                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                chkFolioVenta.Checked = true;

                txtVenta.Text = FolioVenta;
                txtVenta_Validating(null, null);


                GuardarInformacion(); 
                //this.ShowDialog();
                // bSeGeneraValeAutomatico = bSeGeneraValeAutomatico;
            }
            else
            {
                this.Hide();
            }
        }
        #endregion Generacion Automática de Vales

        #region Buscar Vale
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sFolio = "";
            bFolioGuardado = false;
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            
            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
            }
            else
            {
                leer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), false, false, "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("El Folio de Vale no encontrado, verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    sFolio = leer.Campo("Folio");
                    txtFolio.Text = sFolio;
                    iFolioTimbre = leer.CampoInt("FolioTimbre");
                    sFolioVale = sFolio; 

                    dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    txtVenta.Text = leer.Campo("FolioVenta");
                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
                    txtSubCliente.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                    txtPrograma.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtSubPrograma.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");

                    toolTip.SetToolTip(lblCte, lblCte.Text);
                    toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                    toolTip.SetToolTip(lblPro, lblPro.Text);
                    toolTip.SetToolTip(lblSubPro, lblSubPro.Text);
                    IniciarToolBar(false, true, true);

                    bEsReimpresion = true;

                    //if (leer.Campo("IdPersona") == "")
                    //{
                    //    btnImprimir.Enabled = false;
                    //    btnConfirmarHuella.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnImprimir.Enabled = true;
                    //    btnConfirmarHuella.Enabled = false;
                    //}

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Text = "CANCELADO";
                        lblCancelado.Visible = true;
                        IniciarToolBar(false, false, false);
                        btnImprimir.Enabled = false;
                        btnConfirmarHuella.Enabled = false;
                    }

                    if (leer.Campo("Status") == "R")
                    {
                        lblCancelado.Text = "REGISTRADO";
                        lblCancelado.Visible = true;
                        IniciarToolBar(false, false, false);
                        btnConfirmarHuella.Enabled = true;
                    }

                    if (!lblCancelado.Visible)
                    {
                        if (bManejaVales_ServicioDomicilio)
                        {
                            CargarDatos_ServicioDomicilio(); 
                        }
                    }

                    CargaDetallesVale();
                    CambiaEstado(false);

                    CargarDatos_FirmaElectronica(); 
                }
            }            
        }

        private void CargarDatos_ServicioDomicilio()
        {
            bool bMostrarServicioDomicilio = false;
            int iFechaVale = Convert.ToInt32("0" + General.FechaYMD(dtpFechaRegistro.Value, ""));
            int iFechaServicio = Convert.ToInt32("0" + GnFarmacia.Maneja_ValesServicioDomicilio_Inicio.Replace("-", ""));

            ////General.msjAviso(string.Format("{0}     {1}", iFechaServicio, iFechaVale)); 


            leer.DataSetClase = Consultas.ValesServicioADomicilio(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "CargarDatos_ServicioDomicilio()");
            if (leer.Leer())
            {
                bValeValido_ServicioDomicilio = true; 
                bMostrarConfirmacion_ServicioDomicilio = true;
                sIdBeneficiario = leer.Campo("IdBeneficiario");
                sFolioServicioDomicilio = leer.Campo("FolioServicioDomicilio");
            }
            else
            {
                leer.DataSetClase = Consultas.ValesEmision_InformacionAdicional(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "CargarDatos_ServicioDomicilio()");
                if (leer.Leer())
                {
                    bMostrarConfirmacion_ServicioDomicilio = true;
                    sIdBeneficiario = leer.Campo("IdBeneficiario");
                    sFolioServicioDomicilio = "*";
                    bValeValido_ServicioDomicilio = iFechaVale >= iFechaServicio;
                }
            }

            btnServicioADomicilio.Text = bMostrarConfirmacion_ServicioDomicilio ? sRegistro_ServicioDomicilio : sGenerar_ServicioDomicilio;
            btnServicioADomicilio.ToolTipText = btnServicioADomicilio.Text; 

            IniciarToolBar(btnGuardar.Enabled, false, btnImprimir.Enabled, true);
        }

        private void CargarDatos_FirmaElectronica()
        {
            string sSql = string.Format("Select * " + 
            " From ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades (NoLock) " + 
            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioTimbre = '{3}' ", 
            sEmpresa, sEstado, sFarmacia, iFolioTimbre);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos_FirmaElectronica()"); 
            }
            else
            {
                if(leer.Leer())
                {
                    GetCadenaInteligente(leer.Campo("CadenaInteligente")); 
                }
            }
        }

        private bool CargaDetallesVale()
        {
            bool bRegresa = true;

            leer2.DataSetClase = Consultas.ValesEmisionDet(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "CargaDetallesVale");
            if (leer2.Leer())
            {
                myGrid.LlenarGrid(leer2.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }
            myGrid.BloqueaColumna(true, 1);
            return bRegresa;
        } 
        #endregion Buscar Vale

        #region Botones
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            IniciarToolBar(Guardar, Cancelar, Imprimir, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool ServicioDomicilio)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnServicioADomicilio.Enabled = ServicioDomicilio;

            if (!bEmiteValesManuales)
            {
                btnGuardar.Enabled = false; 
            }
        }

        private void btnNuevo_Click( object sender, EventArgs e )
        {
            InicializarPantalla();
        }

        private void InicializarPantalla() 
        {
            QR_FirmaElectronica = new clsGenerarQR_Folios();

            sFolioSolicitud = "";
            sFolioVale = "";

            bValeValido_ServicioDomicilio = false; 
            bMostrarConfirmacion_ServicioDomicilio = false;
            sIdBeneficiario = "";
            sFolioServicioDomicilio = "";
            btnServicioADomicilio.Text = sGenerar_ServicioDomicilio;
            btnServicioADomicilio.ToolTipText = btnServicioADomicilio.Text; 

            //// Quitar los ToolTips 
            toolTip.SetToolTip(lblCte, "");
            toolTip.SetToolTip(lblSubCte, "");
            toolTip.SetToolTip(lblPro, "");
            toolTip.SetToolTip(lblSubPro, "");

            bEsReimpresion = false;
            bHabilitarCaptura = true; 
            bEsSeguroPopular = false; 
            bFolioGuardado = false;
            Fg.IniciaControles(this, true);
            lblCancelado.Visible = false;
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

            myGrid.BloqueaColumna(false, 1); 
            myGrid.Limpiar(true);

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;             
            dtpFechaDeSistema.Enabled = false;
            dtpFechaDeSistema.Value = GnFarmacia.FechaOperacionSistema;
            txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            CambiaEstado(true);
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            lblVentaConValesCancelados.Text = "TIENE VALES CANCELADOS";
            lblVentaConValesCancelados.Visible = false; 

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;

            IniciarToolBar(false, false, false);

            btnConfirmarHuella.Enabled = false;
            chkMostrarImpresionEnPantalla.Checked = false;
            txtVenta.Enabled = false;
            chkFolioVenta.Checked = false;
            txtFolio.Focus();
            
        }

        private void btnGuardar_Click( object sender, EventArgs e )
        {
            GuardarInformacion(); 
        }

        private void GuardarInformacion()
        {
            if(ValidaDatos())
            {
                if(CapturaDeHuella())
                {
                    GuardarInformacion_Vale();
                }
            }
        }
        private void GuardarInformacion_Vale()
        {
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;
            sMensaje = "Ocurrió un error al guardar la información.";  

            if (txtFolio.Text != "*")
            {
                General.msjUser("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            }
            else
            {
                if (!con.Abrir())
                {
                    Error.LogError(con.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    IniciarToolBar(false, false, false);

                    System.Threading.Thread.Sleep(1500);
                    con.IniciarTransaccion();

                    bContinua = true; 
                    if (bValeElectronico)
                    {
                        bContinua = ObtenerFirmaElectronica(1);
                    }

                    if (bContinua)
                    {
                        bContinua = GuardarVale();
                    }

                    if (bContinua)
                    {
                        bContinua = Grabar_QR_FirmaElectronica();
                    }

                    if (bContinua)
                    {
                        if (bFirmaHuella)
                        {
                            bContinua = GrabarHuellaEnVale();
                        }
                    }

                    if (bContinua)
                    {
                        if (bValeElectronico)
                        {
                            if (iFolioTimbre != 0)
                            {
                                bContinua = ObtenerFirmaElectronica(2);
                            }
                        }
                    }

                    if (bContinua)
                    {
                        con.CompletarTransaccion();

                        IniciarToolBar(false, false, true);
                        txtFolio.Text = sFolioVale;
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP

                        btnConfirmarHuella_Click(this, null);

                        Imprimir(!bSeGeneraValeAutomatico);                                    

                        if (bSeGeneraValeAutomatico)
                        {
                            this.Hide();
                        }
                    }
                    else
                    {
                        con.DeshacerTransaccion();
                        txtFolio.Text = "*";

                        if (!EsPersonalFarmacia)
                        {
                            Error.GrabarError(leer, "btnGuardar_Click");
                            sMensaje = "Ocurrió un error al guardar la información.";

                            General.msjError(sMensaje);
                            //General.msjError();
                        }
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }
                            
                    con.Cerrar();
                }
            } 
        }

        private bool GuardarVale()
        {
            bool bRegresa = false;
            string sSql = "";
            int iOpcion = 1;

            sSql = string.Format(" Exec spp_Mtto_Vales_EmisionEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " +
                "'{6}', '{7}', '{8}', '{9}', '{10}', {11} ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), txtVenta.Text.Trim(), txtIdPersonal.Text, 
                txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), txtPrograma.Text.Trim(), txtSubPrograma.Text.Trim(), 
                iOpcion, iFolioTimbre);

            sSql = string.Format(" Exec spp_Mtto_Vales_EmisionEnc " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVale = '{3}', @FolioVenta = '{4}', @IdPersonal = '{5}', " + 
                " @IdCliente = '{6}', @IdSubCliente = '{7}', @IdPrograma = '{8}', @IdSubPrograma = '{9}', @iOpcion = '{10}', " + 
                " @FolioTimbre = '{11}', @FechaSistema = '{12}' ", 
                sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), txtVenta.Text.Trim(), DtGeneral.IdPersonal,  
                txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), txtPrograma.Text.Trim(), txtSubPrograma.Text.Trim(),
                iOpcion, iFolioTimbre, sFechaSistema);             

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    bRegresa = true; 
                    sFolioVale = String.Format("{0}", leer.Campo("Clave"));
                    sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                }
            }

            if (bRegresa)
            {
                bRegresa = GuardaVentaInformacionAdicional();
            }

            return bRegresa;
        }

        private bool GuardaVentaInformacionAdicional()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format(" Exec spp_Mtto_Vales_Emision_InformacionAdicional \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVale = '{3}',\n" +
                "\t@IdBeneficiario = '{4}', @NumReceta = '{5}', @FechaReceta = '{6}', @IdTipoDeDispensacion = '{7}', @IdUnidadMedica = '{8}',\n" +
                "\t@IdMedico = '{9}', @IdBeneficioSP = '{10}', @IdDiagnostico = '{11}', @IdServicio = '{12}', @IdArea = '{13}',\n" +
                "\t@RefObservaciones = '{14}', @iOpcion = '{15}' \n",
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                sFolioVale, InfVtas.Beneficiario, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                InfVtas.TipoDispensacion, InfVtas.CluesRecetasForaneas, InfVtas.Medico, InfVtas.IdBeneficio, InfVtas.Diagnostico, InfVtas.Servicio, InfVtas.Area, 
                InfVtas.ReferenciaObservaciones, 1 );

            bRegresa = leer.Exec(sSql); 
            if ( bRegresa ) 
            {
                bRegresa = GuardaDetallesVale();
            }

            return bRegresa; 
        }

        private bool GuardaDetallesVale()
        {
            bool bRegresa = false; 
            string sSql = "", sIdClaveSSA = "", sIdPresentacion = "";
            int iCantidad = 0, iOpcion = 1;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sIdPresentacion = myGrid.GetValue(i, (int)Cols.IdPresentacion);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                if (sIdClaveSSA != "")
                {
                    sSql = String.Format("Exec spp_Mtto_Vales_EmisionDet \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVale = '{3}',\n" +
                        "\t@IdClaveSSA_Sal = '{4}', @Cantidad = '{5}', @IdPresentacion = '{6}', @iOpcion = '{7}' \n", 
                        sEmpresa, sEstado, sFarmacia, sFolioVale, sIdClaveSSA, iCantidad, sIdPresentacion, iOpcion);
                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = true; 
                    }
                        
                }
            }

            return bRegresa; 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FrmCancelacionVales f = new FrmCancelacionVales(txtFolio.Text, false);
            f.ShowDialog();
            InicializarPantalla();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void Imprimir(bool Confirmar)
        {
            bool bImprimir = true; 
 
            if (Confirmar)
            {
                if (General.msjConfirmar("¿ Desea imprimir el Vale activo ?") == DialogResult.No)
                {
                    bImprimir = false; 
                }
            }

            if (bEsReimpresion && bFirmaHuella)
            {
                bImprimir = false;

                clsVerificarHuella f = new clsVerificarHuella();
                f.MostrarMensaje = false;
                f.Show();

                if (FP_General.HuellaCapturada)
                {
                    if (FP_General.ExisteHuella)
                    {
                        bImprimir = true;
                    }
                }
            }

            if (bImprimir)
            {
                sFolioVale = Fg.PonCeros(txtFolio.Text, 8);

                VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
                VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket;
                VtasImprimir.NumeroDeCopias = iNumeroDeCopias; 


                if (VtasImprimir.ImprimirVale(sFolioVale, sCadenaInteligente))
                {
                    InicializarPantalla();
                }
            }
        }

        private void btnConfirmarHuella_Click(object sender, EventArgs e)
        {
        }

        private bool CapturaDeHuella()
        {
            bool bRegresa = !bFirmaHuella;
            //FP_General.IntentosVerificacion = 2; 
            
            if (bFirmaHuella)
            {
                bRegresa = false; 
                if (!bConexionHuellasEstablecida) 
                {
                    General.msjUser("No fue posible establecer la conexión para la validar las huellas, favor de reportarlo a sistemas."); 
                }
                else 
                {
                    clsVerificarHuella f = new clsVerificarHuella();

                    //f.Titulo = "Registro de salida";
                    f.MostrarMensaje = false;
                    f.Show();

                    if (FP_General.HuellaCapturada)
                    {
                        if (FP_General.ExisteHuella)
                        {
                            sReferencia = FP_General.Referencia_Huella;
                            //if (GrabarHuellaEnVale())
                            {
                                //btnImprimir.Enabled = true;
                                //btnConfirmarHuella.Enabled = false;
                                //btnImprimir_Click(this, null);
                                bRegresa = true;
                            }
                        }
                        else
                        {
                            FrmHuellasVales H = new FrmHuellasVales(false);
                            H.ShowDialog();
                            sReferencia = H.sGUID;
                            bTieneHuella = H.bTieneHuella;

                            if (bTieneHuella)
                            {
                                bRegresa = true;
                            }
                        }
                    }

                    if (!bRegresa)
                    {
                        General.msjAviso("No se registro la huella de quien recibe.");
                    }
                }
            }

            return bRegresa;
        }

        private void btnServicioADomicilio_Click(object sender, EventArgs e)
        {
            if (!bValeValido_ServicioDomicilio)
            {
                General.msjAviso("El folio de vale no es válido para Servicio a Domicilio.");
            }
            else
            {
                //////if (bMostrarConfirmacion_ServicioDomicilio)
                //////{
                //////    General.msjAviso("Registrar servicio a domicilio");
                //////}
                //////else
                {
                    FrmGeneracionServicioDomicilio f = new FrmGeneracionServicioDomicilio();
                    f.MostrarRegistro_ServicioADomicilio(txtCliente.Text, txtSubCliente.Text, sIdBeneficiario, sFolioVale, sFolioServicioDomicilio);
                    CargarDatos_ServicioDomicilio();
                }
            }
        }
        #endregion Botones

        #region Validaciones
        private void txtVenta_Validating(object sender, CancelEventArgs e)
        {
            if (txtVenta.Text.Trim() != "")
            {                
                if (BuscarVale())
                {
                    //// sFolioVale 
                    //// General.msjUser("El Folio de Venta ingresado ya tiene un vale asignado, verifique");

                    General.msjUser(string.Format("El Folio de Venta ya fue ingresado en el vale [ {0} ], verifique", sFolioVale));
                    txtVenta.Text = "";
                    txtVenta.Focus();
                }
                else
                {
                    BuscarVenta();
                }                
            }
        }
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            bEsSeguroPopular = false; 
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

            if (txtCliente.Text.Trim() == "")
            {
                txtCliente.Text = "";
                lblCte.Text = "";
                txtSubCliente.Text = "";
                lblSubCte.Text = "";
                txtPrograma.Text = "";
                lblPro.Text = "";
                txtSubPrograma.Text = "";
                lblSubCte.Text = "";
                toolTip.SetToolTip(lblCte, "");
                toolTip.SetToolTip(lblSubCte, "");
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                if (Fg.PonCeros(txtCliente.Text, 4) == sIdPublicoGral)
                {
                    General.msjAviso("El Cliente Público General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Crédito");
                    txtCliente.Text = "";
                    lblCte.Text = "";
                    toolTip.SetToolTip(lblCte, "");
                    e.Cancel = true;
                }
                else
                {
                    leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, "txtCte_Validating");
                    if (!leer.Leer())
                    {
                        General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece a la Farmacia.");
                        e.Cancel = true;
                    }
                    else
                    {
                        txtCliente.Enabled = false;
                        txtCliente.Text = leer.Campo("IdCliente");
                        lblCte.Text = leer.Campo("NombreCliente");
                        txtSubCliente.Text = "";
                        lblSubCte.Text = "";
                        txtPrograma.Text = "";
                        lblPro.Text = "";
                        txtSubPrograma.Text = "";
                        lblSubCte.Text = ""; 

                        toolTip.SetToolTip(lblCte, lblCte.Text);

                        //// Exigir la informacion de Seguro Popular solo si esta activo.
                        //if (bValidarSeguroPopular)
                        {
                            if (sIdSeguroPopular == txtCliente.Text.Trim())
                                bEsSeguroPopular = true;
                        }
                    }
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

            if (txtSubCliente.Text.Trim() == "")
            {
                txtSubCliente.Text = "";
                lblSubCte.Text = "";
                txtPrograma.Text = "";
                lblPro.Text = "";
                txtSubPrograma.Text = "";
                lblSubCte.Text = "";
                toolTip.SetToolTip(lblSubCte, "");
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
            else
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    ////// Obtener datos de IMach 
                    ////sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud(); 

                    txtSubCliente.Enabled = false; 
                    txtSubCliente.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                    bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                    bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");

                    bPermitirCapturaBeneficiariosNuevos = GnFarmacia.ValidarCapturaBeneficiariosNuevos(bPermitirCapturaBeneficiariosNuevos);

                    txtPrograma.Text = "";
                    lblPro.Text = "";
                    txtSubPrograma.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblSubCte, lblSubCte.Text);

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
            if (txtCliente.Text.Trim() != "" && txtSubCliente.Text.Trim() != "" && txtPrograma.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, txtPrograma.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Programa no encontrada, ó el Programa no pertenece al Cliente ó Farmacia.");
                    txtPrograma.Text = "";
                    lblPro.Text = "";
                    txtSubPrograma.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblPro, "");
                    toolTip.SetToolTip(lblSubPro, "");
                    e.Cancel = true;
                }
                else
                {
                    txtPrograma.Enabled = false; 
                    txtPrograma.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtSubPrograma.Text = "";
                    lblSubPro.Text = "";
                    toolTip.SetToolTip(lblPro, lblPro.Text);
                    toolTip.SetToolTip(lblSubPro, "");
                }
            }
            else
            {
                txtPrograma.Text = "";
                lblPro.Text = "";
                txtSubPrograma.Text = "";
                lblSubPro.Text = ""; 
                toolTip.SetToolTip(lblPro, "");
                toolTip.SetToolTip(lblSubPro, "");
            }
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {            
            if (txtCliente.Text.Trim() != "" && txtSubCliente.Text.Trim() != "" && txtPrograma.Text.Trim() != "" && txtSubPrograma.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, txtPrograma.Text, txtSubPrograma.Text, "txtPro_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("Clave de Sub-Programa no encontrada, ó el Sub-Programa no pertenece al Cliente ó Farmacia.");
                    e.Cancel = true;
                }
                else
                {
                    txtSubPrograma.Enabled = false; 
                    txtSubPrograma.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");
                    toolTip.SetToolTip(lblSubPro, lblSubPro.Text);

                    //// Obtener datos de IMach 
                    //sFolioSolicitud = IMachPtoVta.ObtenerFolioSolicitud();

                    // Exclusivo Seguro Popular 
                    if (bEsSeguroPopular)
                        MostrarInfoAdicional(); 

                    myGrid.Limpiar(true); 
                }
            }
            else
            {
                txtSubPrograma.Text = "";
                lblSubPro.Text = "";
                toolTip.SetToolTip(lblSubPro, "");
            }            
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";

            EliminarRenglonesVacios(); 

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                bRegresa = false; 
                Application.Exit();
            }

            if (bRegresa)
            {
                if (!bEmiteVales)
                {
                    bRegresa = false;
                    General.msjUser("Esta farmacia no esta autorizada para emitir vales.");
                    txtFolio.Focus();
                }
            }

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Vale inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && chkFolioVenta.Checked)
            {
                if (txtVenta.Enabled)
                {
                    bRegresa = false;
                    General.msjUser("Folio de Venta inválido, verifique.");
                    txtVenta.Focus();
                }
            }

            if (bRegresa && txtCliente.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
                txtCliente.Focus();
            }

            if (bRegresa && txtSubCliente.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubCliente inválida, verifique.");
                txtSubCliente.Focus();
            }

            if (bRegresa && txtPrograma.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Programa inválida, verifique.");
                txtPrograma.Focus();
            }

            if (bRegresa && txtSubPrograma.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de SubPrograma inválida, verifique.");
                txtSubPrograma.Focus();
            }


            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                // Funcioanalidad para manejo de Almacenes Jurisdiccionales 
                if (tpPuntoDeVenta == TipoDePuntoDeVenta.Farmacia_Almacen)
                {
                    bRegresa = validarInfAdicional_FarmaciasAlmacen();
                }
            }

            return bRegresa;
        }

        private bool validarInfAdicional_FarmaciasAlmacen()
        {
            bool bRegresa = true;
            if (!chkFolioVenta.Checked)
            {
                if (bRegresa && !InfVtas.PermitirGuardar)
                {
                    bRegresa = false;
                    General.msjUser("La información adicional de la venta no esta capturada,\nno es posible generar la venta, verifique.");
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
                        if (myGrid.GetValue(i, (int)Cols.Descripcion) != "" && myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0) 
                        {
                            bRegresa = false;
                            break;
                        }
                    }                    
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un producto para el Vale\n y/o capturar cantidades para todas las Claves, verifique.");
            }

            return bRegresa;

        }

        #endregion Validaciones

        #region Funciones
        private string GenerarMD5()
        {
            sMd5 = DtGeneral.EmpresaConectada + DtGeneral.EstadoConectado + DtGeneral.FarmaciaConectada + txtFolio.Text.Trim();
            sMd5 = GenerarMD5(sMd5);
            return sMd5;
        }

        private static string GenerarMD5(string Cadena)
        {
            string sMD5 = "";
            byte[] bytesCadena, bytesRegresa;
            MD5CryptoServiceProvider MD5Crypto = new MD5CryptoServiceProvider();

            bytesCadena = System.Text.Encoding.UTF8.GetBytes(Cadena);
            bytesRegresa = MD5Crypto.ComputeHash(bytesCadena);

            sMD5 = BitConverter.ToString(bytesRegresa);
            sMD5 = sMD5.Replace("-", "").ToLower();

            return sMD5;
        }

        private bool LlenaCliente()
        {
            bool bRegresa = false;
            string sSql = "";
            leer2 = new clsLeer(ref con);

            sSql = string.Format(" SELECT * FROM CatClientes (nolock) WHERE IdCliente='{0}' ", Fg.PonCeros(txtCliente.Text, 4));

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
                    txtCliente.Text = leer2.Campo("IdCliente");
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

            sSql = string.Format(" SELECT * FROM CatSubClientes (nolock) WHERE IdCliente = '{0}' AND IdSubCliente='{1}' ", Fg.PonCeros(txtCliente.Text, 4), Fg.PonCeros(txtSubCliente.Text, 4));

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
                    txtSubCliente.Text = leer2.Campo("IdSubCliente");
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

            sSql = string.Format(" SELECT * FROM CatProgramas (nolock) WHERE IdPrograma='{0}' ", Fg.PonCeros(txtPrograma.Text, 4));

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
                    txtPrograma.Text = leer2.Campo("IdPrograma");
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

            sSql = string.Format(" SELECT * FROM CatSubProgramas (nolock) WHERE IdSubPrograma='{0}' AND IdPrograma='{1}' ", Fg.PonCeros(txtSubPrograma.Text, 4), Fg.PonCeros(txtPrograma.Text,4));

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
                    txtSubPrograma.Text = leer2.Campo("IdSubPrograma");
                    lblSubPro.Text = leer2.Campo("Descripcion");
                }
            }

            return bRegresa;
        }

        private void CambiaEstado(bool bValor)
        {
            txtFolio.Enabled = bValor;
            txtVenta.Enabled = bValor;
            chkFolioVenta.Enabled = bValor;
            txtCliente.Enabled = bValor;
            txtPrograma.Enabled = bValor;
            txtSubCliente.Enabled = bValor;
            txtSubPrograma.Enabled = bValor;
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
                        InicializarPantalla();
                    break;

                case Keys.P:
                    if (btnImprimir.Enabled)
                        btnImprimir_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        private bool BuscarVale()
        {
            bool bGeneroVale = false;
            if (txtVenta.Text.Trim() != "")
            {
                Consultas.MostrarMsjSiLeerVacio = false;
                leer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVenta.Text, 8), true, false, "BuscarVale");
                if (leer.Leer())
                {
                    sFolioVale = leer.Campo("Folio"); 
                    leerVale.DataRowsClase = leer.DataTableClase.Select("Status = 'A'");
                    if (leerVale.Leer()) 
                    {
                        bGeneroVale = true;
                    }

                    leerVale.DataRowsClase = leer.DataTableClase.Select(" Status = 'C' ");
                    if (leerVale.Leer())
                    {
                        // Mostrar label ==> Venta con Vales Cancelados 
                        lblVentaConValesCancelados.Visible = true; 
                    }

                    //////if ( leer.Campo("Status") == "A" ) 
                    //////{
                    //////    bGeneroVale = true; 
                    //////}
                }
            }
            Consultas.MostrarMsjSiLeerVacio = true;

            return bGeneroVale;
        }

        private bool BuscarVenta()
        {
            bool bRegresa = false;

            if (txtVenta.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVenta.Text, 8), "BuscarVenta");
                if (!leer.Leer()) 
                {
                    txtVenta.Text = ""; 
                    txtVenta.Focus(); 
                }
                else
                {
                    bRegresa = true;
                    txtVenta.Text = leer.Campo("Folio");
                    txtVenta.Enabled = false;

                    txtCliente.Text = leer.Campo("IdCliente");
                    lblCte.Text = leer.Campo("NombreCliente");
                    txtCliente.Enabled = false; 

                    txtSubCliente.Text = leer.Campo("IdSubCliente");
                    lblSubCte.Text = leer.Campo("NombreSubCliente");
                    txtSubCliente.Enabled = false; 

                    txtPrograma.Text = leer.Campo("IdPrograma");
                    lblPro.Text = leer.Campo("Programa");
                    txtPrograma.Enabled = false; 

                    txtSubPrograma.Text = leer.Campo("IdSubPrograma");
                    lblSubPro.Text = leer.Campo("SubPrograma");
                    txtSubPrograma.Enabled = false;

                    chkFolioVenta.Enabled = false;

                    CargarClaves_CB(); 
                    MostrarInfoAdicional(); 
                }
            }

            return bRegresa;
        }

        private void CargarClaves_CB() 
        {
            // CantidadRequerida 
            clsLeer leerClaves = new clsLeer(); 
            int iAutomatico = bSeGeneraValeAutomatico ? 1 : 0;
            string sFiltro = " 1 = 1 ";

            string sSql =
                string.Format(
                "Select ClaveSSA, IdClaveSSA, DescripcionSal, IdPresentacion, Presentacion, " + 
                " cast((Case When 1 = {4} then V.CantidadRequerida Else 0 End) as int) as Cantidad, " + 
                " cast(dbo.fg_EsClaveFaltante(ClaveSSA) as int) as ClaveFaltante, " + 
                " dbo.fg_Existencia_Clave(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.ClaveSSA) as ExistenciaClave, " +
                " dbo.fg_EmiteVales_Clave('{1}', '{5}', V.ClaveSSA ) as EmiteVales, " +
                " dbo.fg_GetParametro_ValidarExistenciaEmisonVales('{1}', '{2}') as ValidarExistencia " + 
                "From vw_Impresion_Ventas_ClavesSolicitadas V (NoLock) " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' and EsCapturada = 1 and Clave_CB = 1 ",
                sEmpresa, sEstado, sFarmacia, txtVenta.Text, iAutomatico, Fg.PonCeros(txtCliente.Text, 4));

            bHabilitarCaptura = false;  
            myGrid.Limpiar(false);
            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer.Error, "CargarClaves_CB"); 
                General.msjError("Ocurrió un error al cargar las Claves para generar el vale."); 
            }
            else
            {
                if (leer.Leer()) 
                {
                    leerClaves.DataRowsClase = leer.DataTableClase.Select(" ClaveFaltante > 0 ");
                    if (leerClaves.Leer())
                    {
                        sFiltro += " And ClaveFaltante = 0 "; 
                        General.msjAviso("Algunas Claves solicitadas cuentan con Carta de Faltante, se elimarán del vale."); 
                    }

                    if (leer.CampoBool("ValidarExistencia"))
                    {
                        leerClaves.DataRowsClase = leer.DataTableClase.Select(" ExistenciaClave > 0");
                        if (leerClaves.Leer())
                        {
                            sFiltro += " And ExistenciaClave = 0 "; 
                            General.msjAviso("Algunas Claves solicitadas para la generación de Vales tienen existencia, se elimarán del vale.");
                        }
                    }

                    leerClaves.DataRowsClase = leer.DataTableClase.Select(" EmiteVales = 0 ");
                    if (leerClaves.Leer())
                    {
                        sFiltro += " And EmiteVales = 1 "; 
                        General.msjAviso("Algunas Claves solicitadas para la generación de Vales no emiten vales, se elimarán del vale.");
                    }

                    //// myGrid.LlenarGrid(leer.DataSetClase); 
                    leerClaves.Reset();
                    //leerClaves.DataRowsClase = leer.DataTableClase.Select(" ClaveFaltante = 0 and ExistenciaClave = 0 and EmiteVales = 1 ");
                    leerClaves.DataRowsClase = leer.DataTableClase.Select(sFiltro);
                    myGrid.LlenarGrid(leerClaves.DataSetClase); 
                }
            }
        }

        private bool GrabarHuellaEnVale()
        {
            bool bRegresa = false;
            clsLeer leerClaves = new clsLeer();

            if (!ChecarReferencia())
            {
                string sSql = string.Format("Update Vales_EmisionEnc Set IdPersonaFirma = '{0}' Where IdEmpresa = '{1}' And IdEstado = '{2}' And IdFarmacia = '{3}' And FolioVale = '{4}'",
                     sReferencia, sEmpresa, sEstado, sFarmacia, sFolioVale);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer.Error, "GrabarHuellaEnVale");
                    General.msjError("Ocurrió un error al grabar la clave de la persona que recoge el vale.");
                }
                else
                {
                    bRegresa = true;
                }
            }
            else
            {
                General.msjAviso("El personal de farmacia no puede firmar vales, por favor verifique.");
            }

            return bRegresa;
        }

        private bool ChecarReferencia()
        {
            bool bRegresa = false;
            clsLeer leerClaves = new clsLeer();

            string sSql =
                string.Format(" Select * From Vales_Huellas Where IdEstado = '{0}' And IdFarmacia = '{1}' And GUID = '{2}'",
                 sEstado, sFarmacia, sReferencia);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "ChecarReferencia");
                General.msjError("Ocurrió un error al verificar la clave de la persona que Firma el vale.");
            }
            else
            {
                if (leer.Leer())
                {
                    EsPersonalFarmacia = leer.CampoBool("EsPersonalFarmacia");
                    bRegresa = EsPersonalFarmacia;
                }
            }

            return bRegresa;
        }

        private bool Obtener_Url_Firma()
        {
            bool bRegresa = true;

            leer.DataSetClase = Consultas.Url_PersonalFirma("Obtener_Url_Firma");

            if (leer.Leer())
            {
                sUrlValidarHuellas = leer.Campo("UrlFarmacia");
                sHost = leer.Campo("Servidor");
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;            

            try
            {
                //leerWeb = new clsLeerWebExt(sUrlValidarHuellas, DtGeneral.CfgIniChecadorPersonal, DatosCliente);
                validarHuella.Url = sUrlValidarHuellas;
                DatosDeConexion = new clsDatosConexion(validarHuella.ConexionEx(DtGeneral.CfgIniValidarHuellas));
                //DatosDeConexion = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniChecadorPersonal));
                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            bConexionHuellasEstablecida = bRegresa;
            return bRegresa;
        }

        private void ConexionValidarHuellas()
        {
            con = new clsConexionSQL(DatosDeConexion);
            leerChecador = new clsLeer(ref con);
            FP_General.Conexion = DatosDeConexion;
        }
        #endregion Funciones             

        #region Eventos 
        private void FrmGeneracionVales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

            switch (e.KeyCode)
            {
                case Keys.F5:
                    MostrarInfoAdicional();
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            //////////// Se habilita Caja en el Registro de Vales 
            //////tmSesion.Enabled = false;

            //////bEmiteVales = true; //Esta linea es de prueba debe ir comentariada. 
            //////if (!bEmiteVales)
            //////{
            //////    General.msjUser("Esta Farmacia no esta autorizada para Emitir vales.");
            //////    this.Close();
            //////}
            //////else
            //////{
            //////    FrmFechaSistema Fecha = new FrmFechaSistema();
            //////    Fecha.ValidarFechaSistema();

            //////    GnFarmacia.ValidarSesionUsuario = true;
            //////    if (Fecha.Exito)
            //////    {
            //////        GnFarmacia.Parametros.CargarParametros();
            //////        Fecha.Close();

            //////        Sesion = new FrmIniciarSesionEnCaja();
            //////        Sesion.VerificarSesion();

            //////        if (!Sesion.AbrirVenta)
            //////        {
            //////            this.Close();
            //////        }
            //////        else
            //////        {
            //////            Sesion.Close();
            //////            Sesion = null;
            //////            btnNuevo_Click(null, null);
            //////        }
            //////    }
            //////    else
            //////    {
            //////        this.Close();
            //////    }
            //////}
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)         
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer2.Leer())
                {
                    txtCliente.Text = leer2.Campo("IdCliente");
                    lblCte.Text = leer2.Campo("NombreCliente");
                    toolTip.SetToolTip(lblCte, lblCte.Text); 
                    txtSubCliente.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, "txtSubCte_KeyDown_1");
                    if (leer2.Leer())
                    {
                        txtSubCliente.Text = leer2.Campo("IdSubCliente");
                        lblSubCte.Text = leer2.Campo("NombreSubCliente");
                        toolTip.SetToolTip(lblSubCte, lblSubCte.Text);
                        txtPrograma.Focus();
                    }
                }
            }
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "" && txtSubCliente.Text.Trim() != "")
                {
                    //leer2.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, "txtPro_KeyDown"); 
                    if (leer2.Leer())
                    {
                        txtPrograma.Text = leer2.Campo("IdPrograma");
                        lblPro.Text = leer2.Campo("Programa");
                        toolTip.SetToolTip(lblPro, lblPro.Text);
                        txtSubPrograma.Focus();
                    }
                }
            }
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCliente.Text.Trim() != "" && txtSubCliente.Text.Trim() != "" && txtPrograma.Text.Trim() != "")
                {
                    leer2.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCliente.Text, txtSubCliente.Text, txtPrograma.Text, "txtPro_KeyDown");
                    if (leer2.Leer())
                    {
                        txtSubPrograma.Text = leer2.Campo("IdSubPrograma");
                        lblSubPro.Text = leer2.Campo("SubPrograma");
                        toolTip.SetToolTip(lblSubPro, lblSubPro.Text);
                    }
                }
            }
        }

        private void chkFolioVenta_CheckedChanged(object sender, EventArgs e)
        {
            txtVenta.Text = ""; 
            txtVenta.Enabled = chkFolioVenta.Checked;
            if (chkFolioVenta.Checked)
            { 
                txtVenta.Focus();
            } 
        } 

        #endregion Eventos    

        #region Grid

        private void grdProductos_EditModeOff_1(object sender, EventArgs e)
        {
            string sValor = "";

            switch (myGrid.ActiveCol)
            {
                case (int)Cols.ClaveSSA:
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

                        if (sValor != "")
                        {
                            // leer.DataSetClase = Consultas.ClavesSSA_Sales(sValor, true, "grdProductos_EditModeOff"); 
                            leer.DataSetClase = Consultas.CuadrosBasicos_Farmacias(
                                sEmpresa, sEstado, sFarmacia, 
                                txtCliente.Text, sValor, "grdProductos_EditModeOff");
                            if (!leer.Leer()) 
                            {
                                // General.msjUser("La Clave SSA ingresada no existe. Verifique."); 
                                General.msjUser("Clave no encontrada en Cuadro básico, verifique.");  
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                            }
                            else
                            {
                                ObtenerDatosClave(); 
                            }
                        }
                    }
                    break;

                case (int)Cols.IdPresentacion:
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdPresentacion);

                        if (sValor != "")
                        {
                            leer.DataSetClase = Consultas.Presentaciones(sValor, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                ObtenerDatosPresentacion();
                            }
                            else
                            {
                                General.msjUser("La Presentacion ingresada no existe, verifique.");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.IdPresentacion, "");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Presentacion, "");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, 0);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.IdPresentacion);
                            }
                        }
                    }

                    break;
            }
            
        }

        private void grdProductos_EditModeOn_1(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
        }

        private void grdProductos_Advance_1(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (bHabilitarCaptura)
                        {
                            if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdClaveSSA) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                            {
                                myGrid.Rows = myGrid.Rows + 1;
                                myGrid.ActiveRow = myGrid.Rows;
                                myGrid.SetActiveCell(myGrid.Rows, 1);
                            }
                        }
                    }
                }
            }
        }

        private void grdProductos_KeyDown_1(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            int iRowActivo = myGrid.ActiveRow; 

            switch (ColActiva)
            {

                case Cols.ClaveSSA:
                case Cols.Descripcion:
                 
                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);
                            //leer.DataSetClase = Ayuda.ClavesSSA_Asignadas_A_Clientes(txtCliente.Text.Trim(), txtSubCliente.Text.Trim(), "grdProductos_KeyDown_1");
                            // leer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown_1"); 
                            leer.DataSetClase = Ayuda.CuadrosBasicos_Farmacias(
                                sEmpresa, sEstado, sFarmacia, txtCliente.Text, "grdProductos_KeyDown_1");  
                            if (leer.Leer())
                            {
                                myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("Codigo"));
                                ObtenerDatosClave();
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                        {
                            myGrid.DeleteRow(iRowActivo);

                            if (myGrid.Rows == 0)
                            {
                                myGrid.Limpiar(true);
                            }
                        }
                        break;

                case Cols.IdPresentacion:

                    if (e.KeyCode == Keys.F1)
                    {
                        sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdPresentacion);
                        leer.DataSetClase = Ayuda.Presentaciones("grdProductos_KeyDown_1");
                        if (leer.Leer())
                        {
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.IdPresentacion, leer.Campo("Codigo"));
                            ObtenerDatosPresentacion();
                        }
                    }
                    break;
            }
        }

        private void ObtenerDatosClave()
        {
            int iRowActivo = myGrid.ActiveRow;
            int iColActiva = (int)Cols.IdClaveSSA; 
            string sIdClaveSSA = leer.Campo("IdClaveSSA");

            if (lblCancelado.Visible == false)
            {
                if (sValorGrid != sIdClaveSSA)
                {
                    if (!myGrid.BuscaRepetido(sIdClaveSSA, iRowActivo, iColActiva))
                    {
                        myGrid.SetValue(iRowActivo, iColActiva, sIdClaveSSA); 
                        myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA"));
                        myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("DescripcionClave"));

                        myGrid.SetValue(iRowActivo, (int)Cols.IdPresentacion, leer.Campo("IdPresentacion"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Presentacion")); 

                        myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);

                        //myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                        //// myGrid.SetActiveCell(iRowActivo, (int)Cols.IdPresentacion);

                        if (leer.CampoInt("ExistenciaClave") == 0)
                        {
                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA); 
                        }
                        else
                        {
                            if ( leer.CampoBool("ValidarExistencia") )
                            {
                                string sMsj = string.Format("La Clave [ {0} ] cuenta con existencia, no es posible agregarla a la generación de vales.", leer.Campo("ClaveSSA"));

                                General.msjAviso(sMsj);

                                myGrid.LimpiarRenglon(iRowActivo);
                                myGrid.SetValue(iRowActivo, 1, "");
                                myGrid.SetActiveCell(iRowActivo, 1);
                            }
                        }

                        if (leer.CampoInt("EmiteVales") == 0)
                        {
                            string sMsj = string.Format("La Clave [ {0} ] no puede emitir vales.", leer.Campo("ClaveSSA"));

                            General.msjAviso(sMsj);

                            myGrid.LimpiarRenglon(iRowActivo);
                            myGrid.SetValue(iRowActivo, 1, "");
                            myGrid.SetActiveCell(iRowActivo, 1);

                            myGrid.BloqueaCelda(false, iRowActivo, (int)Cols.ClaveSSA); 
                        }

                    }
                    else
                    {
                        General.msjUser("La Clave ya se encuentra capturada en otro renglon.");
                        myGrid.SetValue(myGrid.ActiveRow, 1, "");
                        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                        myGrid.EnviarARepetido(); 
                    }
                }
                else
                {
                    // Asegurar que no cambie la Clave
                    myGrid.SetValue(iRowActivo, iColActiva, sIdClaveSSA);
                }
            }

            grdProductos.EditMode = false;
        }

        private void ObtenerDatosPresentacion()
        {
            int iRowActivo = myGrid.ActiveRow;
            string sIdPresentacion = leer.Campo("IdPresentacion");

            if (lblCancelado.Visible == false)
            {
                myGrid.SetValue(iRowActivo, (int)Cols.IdPresentacion, sIdPresentacion);
                myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Descripcion"));
                myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);
                myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
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
                if (myGrid.GetValue(i, 1).Trim() == "") //Si la columna oculta IdClaveSSA esta vacia se elimina.
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

        #region Informacion Adicional
        private void MostrarInfoAdicional()
        {
            MostrarInfoAdicional(false);
        }
        private void MostrarInfoAdicional(bool CerrarInformacionAdicional) 
        {
            //bool bRegresa = true;

            if (txtCliente.Text.Trim() != "" && txtSubCliente.Text.Trim() != "")
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular; 
                InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;

                if (!chkFolioVenta.Checked)
                {
                    InfVtas.EsVale = true;
                    InfVtas.Show(txtFolio.Text, txtCliente.Text, lblCte.Text, txtSubCliente.Text, lblSubCte.Text);
                }
                else
                {
                    InfVtas.EsVale = false;
                    InfVtas.Show(txtVenta.Text, txtCliente.Text, lblCte.Text, txtSubCliente.Text, lblSubCte.Text, CerrarInformacionAdicional, bSeGeneraValeAutomatico);
                }
            } 
            //else
            //{
            //}
        }
        #endregion Informacion Adicional        

        #region Obtener_Folio_Timbre
        private bool ObtenerFirmaElectronica(int Opcion)
        {
            bool bRegresa = true;
            
            string sSql = string.Format(" Exec spp_ADM_VALES_Obtener_FolioTimbre '{0}', '{1}', '{2}', {3}, {4}  ",
                                        sEmpresa, sEstado, sFarmacia, iFolioTimbre, Opcion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (leer.Leer())
                {
                    iFolioTimbre = leer.CampoInt("FolioTimbre");                    
                    if (Opcion == 1)
                    {
                        if (iFolioTimbre == 0)
                        {
                            sMensaje = leer.Campo("Mensaje");
                            /////General.msjAviso(leer.Campo("Mensaje"));
                            bRegresa = false;
                        }
                        else
                        {
                            GetCadenaInteligente(leer.Campo("CadenaInteligente")); 
                        }
                    }
                }
            }

            return bRegresa;
        }

        private void GetCadenaInteligente(string CadenaInteligente)
        {
            try
            {
                sCadenaInteligente = CadenaInteligente;
                sCadenaInteligente = QR_FirmaElectronica.GetGUID(sCadenaInteligente);
                sQR_FirmaElectronica = QR_FirmaElectronica.GetCBB(sCadenaInteligente);

                ms = new MemoryStream(); 
                Image img = (Bitmap)QR_FirmaElectronica.Generar_QR_GUID(sCadenaInteligente);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch 
            {
                sCadenaInteligente = ""; 
            }
        }

        private bool Grabar_QR_FirmaElectronica()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Exec spp_Mtto_Vales_EmisionEnc_GUID '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                                        sEmpresa, sEstado, sFarmacia, sFolioVale, ms.GetBuffer());

            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(General.DatosConexion.CadenaConexion); 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                // Estableciento propiedades 
                cmd.Connection = conn; 
                cmd.CommandText = "spp_Mtto_Vales_EmisionEnc_GUID";
                cmd.CommandType = CommandType.StoredProcedure;

                //  @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, @QR

                System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@IdEmpresa";  
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = sEmpresa;
                cmd.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@IdEstado";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = sEstado;
                cmd.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@IdFarmacia";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = sFarmacia;
                cmd.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@FolioVale";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = sFolioVale;
                cmd.Parameters.Add(parameter);

                parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@GUID";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = sCadenaInteligente;
                cmd.Parameters.Add(parameter);

                
                parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = "@QR";
                parameter.SqlDbType = SqlDbType.Image;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = ms.GetBuffer();
                cmd.Parameters.Add(parameter); 


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)
            {
                bRegresa = false; 
            }

            //////if (!leer.Exec(sSql))
            //////{
            //////    bRegresa = false;
            //////}            

            return bRegresa;
        }
        #endregion Obtener_Folio_Timbre

        #region Validar_Existen_Folios_de_Timbres
        private bool Validar_Existe_FoliosTimbres()
        {
            bool bRegresa = true;  
            int iRegresa = 0;
            string sMensajeRevision = ""; 
            string sSql = string.Format(" Exec spp_ADM_VALES_Obtener_FolioTimbre '{0}', '{1}', '{2}', {3}, {4}  ",
                                        sEmpresa, sEstado, sFarmacia, 0, 3);

            if (bValeElectronico)
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Validar_Existe_FoliosTimbres");
                    General.msjError("Ocurrio un error al validar los folios de timbres");
                    bRegresa = false;
                }
                else
                {
                    if (leer.Leer())
                    {
                        iRegresa = leer.CampoInt("FolioTimbre");

                        if (iRegresa == 0)
                        {
                            bRegresa = false;
                            sMensajeRevision = leer.Campo("Mensaje");
                            General.msjAviso(sMensajeRevision);
                        }
                    }
                }
            }

            return bRegresa;
        }
        #endregion Validar_Existen_Folios_de_Timbres

        private void FrmGeneracionVales_Shown(object sender, EventArgs e)
        {
            if (!Validar_Existe_FoliosTimbres())
            {
                this.Close();
            }
        }
    }
}
