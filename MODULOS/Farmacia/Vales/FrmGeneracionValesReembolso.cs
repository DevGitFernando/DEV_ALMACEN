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
using Farmacia.Procesos;
using Farmacia.Ventas;

//using Dll_IMach4;
//using Dll_IMach4.Interface; 

namespace Farmacia.Vales
{
    public partial class FrmGeneracionValesReembolso : FrmBaseExt
    {
        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, IdClaveSSA = 2, Descripcion = 3, IdPresentacion = 4, Presentacion = 5, Cantidad = 6, CantidadSolicitada = 7
        }

        //PuntoDeVenta IMachPtoVta = new PuntoDeVenta();
        string sFolioSolicitud = ""; 

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;
        clsInformacionVentas InfVtas;
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        // TipoDePuntoDeVenta tpPuntoDeVenta = TipoDePuntoDeVenta.Farmacia_Almacen;

        clsGrid myGrid;
        // Variables Globales  ****************************************************
        bool bPermitirCapturaBeneficiariosNuevos = false;
        bool bImportarBeneficiarios = false;
        bool bCapturaDeClavesSolicitadasHabilitada = GnFarmacia.CapturaDeClavesSolicitadasHabilitada; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "";

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

        bool bFolioGuardado = false;

        #region variables
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";
        bool bEmiteVales = GnFarmacia.EmisionDeValesCompletos;
        string sFolioVale = "";

        // FrmIniciarSesionEnCaja Sesion;
        #endregion variables

        public FrmGeneracionValesReembolso()
        {
            // MessageBox.Show(Application.OpenForms.Count.ToString());
            InitializeComponent();

            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

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
        }

        private void FrmGeneracionVales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            ////tmSesion.Enabled = true;
            ////tmSesion.Start();
        }

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
                leer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), false, true, "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("El Folio de ValeReembolso no encontrado, verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    sFolio = leer.Campo("FolioValeReembolso");
                    txtFolio.Text = sFolio;

                    dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    txtVenta.Text = leer.Campo("Folio");
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

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                    }
                    CargaDetallesVale();
                    CambiaEstado(false);
                    btnGuardar.Enabled = false;
                    myGrid.BloqueaGrid(true);
                }
            }
        }

        private bool CargaDetallesVale()
        {
            bool bRegresa = true;

            leer2.DataSetClase = Consultas.ValesEmisionDet(sEmpresa, sEstado, sFarmacia, txtVenta.Text.Trim(), "CargaDetallesVale");
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
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sFolioSolicitud = "";
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

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            IniciarToolBar(false, false, false);
            chkMostrarImpresionEnPantalla.Checked = false;
            //txtVenta.Enabled = false;
            //chkFolioVenta.Checked = false;
            txtFolio.Focus();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = true;
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
                    if (con.Abrir())
                    {
                        IniciarToolBar(false,false,false); 
                        con.IniciarTransaccion();

                        GuardarVale();
                        //GuardaVentaInformacionAdicional();
                        GuardaDetallesVale();

                        if (bContinua)
                        {
                            con.CompletarTransaccion();
                            
                            IniciarToolBar(false, false, true);
                            txtFolio.Text = sFolioVale;
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            Imprimir(true);
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*"; 
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la Información.");
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                        }
                        con.Cerrar();
                    }
                    else
                    {
                        Error.LogError(con.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo."); 
                    }
                }
            }
        }

        private bool GuardarVale()
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 1;

            if (bContinua)
            {
                sSql = string.Format(" Exec spp_Mtto_Vales_Emision_Reembolso_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                    sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), txtVenta.Text.Trim(), txtIdPersonal.Text, iOpcion);

                if (leer.Exec(sSql))
                {
                    if (leer.Leer())
                    {
                        sFolioVale = String.Format("{0}", leer.Campo("Clave"));
                        sMensaje = String.Format("{0}", leer.Campo("Mensaje"));
                    }
                }
                else
                {
                    bRegresa = false;
                }                
            }

            bContinua = bRegresa;
            return bRegresa;
        }

        private void GuardaVentaInformacionAdicional()
        {
            if (bContinua)
            {
                string sSql = string.Format(" Exec spp_Mtto_Vales_Emision_InformacionAdicional '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                       DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                       sFolioVale, InfVtas.Beneficiario, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                       InfVtas.TipoDispensacion, InfVtas.CluesRecetasForaneas, InfVtas.Medico, InfVtas.IdBeneficio, InfVtas.Diagnostico, InfVtas.Servicio, InfVtas.Area, 
                       InfVtas.ReferenciaObservaciones, 1 );

                if (!leer.Exec(sSql))
                {
                    bContinua = false;
                }
            }
        }

        private void GuardaDetallesVale()
        {
            string sSql = "", sIdClaveSSA = "", sIdPresentacion = "";
            int iCantidad = 0, iOpcion = 1, iCantidadSolicitada = 0;

            if (bContinua)
            {               
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                    sIdPresentacion = myGrid.GetValue(i, (int)Cols.IdPresentacion);
                    iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                    iCantidadSolicitada = myGrid.GetValueInt(i, (int)Cols.CantidadSolicitada);

                    if (sIdClaveSSA != "")
                    {
                        if (iCantidad > 0)
                        {
                            sSql = String.Format(" Set DateFormat YMD Exec spp_Mtto_Vales_EmisionReembolsoDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                                 sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVenta.Text, 8), sIdClaveSSA, iCantidadSolicitada, sIdPresentacion, iOpcion);
                            if (!leer.Exec(sSql))
                            {
                                bContinua = false;
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
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

            if (bImprimir)
            {
                sFolioVale = Fg.PonCeros(txtFolio.Text, 8);

                VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
                VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket;

                if (VtasImprimir.ImprimirValeReembolso(sFolioVale))
                {
                    btnNuevo_Click(null, null);
                }
            }
        }

        #endregion Botones

        #region Validaciones
        private void txtVenta_Validating(object sender, CancelEventArgs e)
        {
            if (txtVenta.Text.Trim() != "")
            {                   
                BuscarVenta();                               
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
                    General.msjAviso("El Cliente Publico General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Credito");
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
                    ////if (bEsSeguroPopular)
                    ////    MostrarInfoAdicional(); 

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

            if (GnFarmacia.UsuarioConSesionCerrada(false))
            {
                bRegresa = false; 
                Application.Exit();
            }

            ////if (bRegresa)
            ////{
            ////    if (!bEmiteVales)
            ////    {
            ////        bRegresa = false;
            ////        General.msjUser("Esta farmacia no esta autorizada para emitir vales.");
            ////        txtFolio.Focus();
            ////    }
            ////}

            if (bRegresa && txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Vale inválido, verifique.");
                txtFolio.Focus();
            }

            ////if (bRegresa && chkFolioVenta.Checked)
            {
                if (bRegresa && txtVenta.Text == "")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Vale inválido, verifique.");
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

            ////if (bRegresa)
            ////{
            ////    // Funcioanalidad para manejo de Almacenes Jurisdiccionales 
            ////    if (tpPuntoDeVenta == TipoDePuntoDeVenta.Farmacia_Almacen)
            ////    {
            ////        bRegresa = validarInfAdicional_FarmaciasAlmacen();
            ////    }
            ////}

            return bRegresa;
        }

        private bool validarInfAdicional_FarmaciasAlmacen()
        {
            bool bRegresa = true;
            ////if (!chkFolioVenta.Checked)
            {
                if (bRegresa && !InfVtas.PermitirGuardar)
                {
                    bRegresa = false;
                    General.msjUser("La información adicional de la venta no esta capturada,\nno es posible generar la venta, verifique.");
                }

                if (bRegresa && !InfVtas.BeneficiarioVigente)
                {
                    bRegresa = false;
                    General.msjUser("La Vigencia del Beneficiario expiro,\nno es posible generar la venta, verifique.");
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
                General.msjUser("Debe capturar al menos un producto para el Vale\n y/o capturar cantidades para todas las Claves, verifique.");

            return bRegresa;

        }

        #endregion Validaciones

        #region Funciones 
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
            //chkFolioVenta.Enabled = bValor;
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

        private bool BuscarVale()
        {
            bool bGeneroVale = false;

            if (txtVenta.Text.Trim() != "")
            {
                Consultas.MostrarMsjSiLeerVacio = false;
                leer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVenta.Text, 8), false, true, "BuscarVale");
                if (leer.Leer())
                {                   
                    bGeneroVale = true;
                }
            }
            Consultas.MostrarMsjSiLeerVacio = true;

            return bGeneroVale;
        }

        private bool BuscarVenta()
        {
            bool bRegresa = false;
            string sFolio = "";

            if (txtVenta.Text.Trim() == "")
            {                
                txtVenta.Text = ""; 
                txtVenta.Focus(); 
            }
            else
            {
                leer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVenta.Text, 8), false, false, "BuscarVenta");
                if (!leer.Leer())
                {
                    General.msjUser("El Folio de Vale no encontrado, verifique.");
                    txtVenta.Text = "";
                    txtVenta.Focus();
                }
                else
                {
                    bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    sFolio = leer.Campo("Folio");
                    txtVenta.Text = sFolio;

                    dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                    //txtVenta.Text = leer.Campo("FolioVenta");
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

                    if (leer.Campo("Status") == "C")
                    {
                        lblCancelado.Visible = true;
                    }
                    
                    bRegresa = CargaDetallesVale();
                    CambiaEstado(false);
                    if (bRegresa)
                    {
                        btnGuardar.Enabled = true;
                    }
                    //MostrarInfoAdicional();
                    if (leer.Campo("Status") == "R")
                    {
                        General.msjAviso("Este Vale ya Fue Registrado, no es posible Generar un Reembolso");
                        CambiaEstado(false);
                        myGrid.BloqueaGrid(true);
                        btnGuardar.Enabled = false;
                    }
                }
            }
            

            return bRegresa;
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

        ////private void chkFolioVenta_CheckedChanged(object sender, EventArgs e)
        ////{
        ////    txtVenta.Text = ""; 
        ////    txtVenta.Enabled = chkFolioVenta.Checked;
        ////    if (chkFolioVenta.Checked)
        ////    { 
        ////        txtVenta.Focus();
        ////    } 
        ////} 

        #endregion Eventos    

        #region Grid

        private void grdProductos_EditModeOff_1(object sender, EventArgs e)
        {
            string sValor = "";
            int iCantidad = 0, iCantidadSolicitada = 0;
            switch (myGrid.ActiveCol)
            {
                case (int)Cols.ClaveSSA:
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA);

                        if (sValor != "")
                        {
                            leer.DataSetClase = Consultas.ClavesSSA_Sales(sValor, true, "grdProductos_EditModeOff");
                            if (leer.Leer())
                            {
                                ObtenerDatosClave();
                            }
                            else
                            {
                                General.msjUser("La Clave SSA ingresada no existe. Verifique.");
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ClaveSSA);
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
                                General.msjUser("La Presentacion ingresada no existe. Verifique.");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.IdPresentacion, "");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Presentacion, "");
                                myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, 0);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.IdPresentacion);
                            }
                        }
                    }

                    break;

                case (int)Cols.CantidadSolicitada:
                    {
                        iCantidad = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad);
                        iCantidadSolicitada = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.CantidadSolicitada);


                        if (iCantidadSolicitada > iCantidad)
                        {                            
                            General.msjUser("La Cantidad Solicitada No Puede ser Mayor a la Cantidad. Verifique....");
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CantidadSolicitada, 0);                            
                            myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CantidadSolicitada);                            
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
                            leer.DataSetClase = Ayuda.ClavesSSA_Sales("grdProductos_KeyDown_1");
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
            string sIdClaveSSA = leer.Campo("IdClaveSSA_Sal");

            if (lblCancelado.Visible == false)
            {
                if (sValorGrid != sIdClaveSSA)
                {
                    if (!myGrid.BuscaRepetido(sIdClaveSSA, iRowActivo, iColActiva))
                    {
                        myGrid.SetValue(iRowActivo, iColActiva, sIdClaveSSA);
                        myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                        myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, leer.Campo("DescripcionClave"));

                        myGrid.SetValue(iRowActivo, (int)Cols.IdPresentacion, leer.Campo("IdPresentacion"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Presentacion, leer.Campo("Presentacion")); 

                        myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, 0);

                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                        // myGrid.SetActiveCell(iRowActivo, (int)Cols.IdPresentacion);
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

                ////if (!chkFolioVenta.Checked)
                ////{
                ////    InfVtas.EsVale = true;
                ////    InfVtas.Show(txtFolio.Text, txtCliente.Text, lblCte.Text, txtSubCliente.Text, lblSubCte.Text);
                ////}
                ////else
                {
                    InfVtas.EsVale = false;
                    InfVtas.Show(txtVenta.Text, txtCliente.Text, lblCte.Text, txtSubCliente.Text, lblSubCte.Text, CerrarInformacionAdicional, false);
                }
            } 
            //else
            //{
            //}
        }
        #endregion Informacion Adicional        
    
    }
}
