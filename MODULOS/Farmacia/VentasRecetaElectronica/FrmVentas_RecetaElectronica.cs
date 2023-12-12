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

using Farmacia;
using Farmacia.Ventas; 
using Farmacia.Procesos;
using Farmacia.Vales; 

//using Dll_IMach4;
//using Dll_IMach4.Interface;
using DllRobotDispensador;

namespace Farmacia.VentasRecetaElectronica
{
    public partial class FrmVentas_RecetaElectronica : FrmBaseExt
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
        string sMensaje = "", sFolioVenta = "", sFolioSurtido = "";
        bool bEsSurtimientoPedido = false;
        string sFarmaciaPed = "", sFolioPedido = "";

        string IdEmpresaCSGN = "", IdEstadoCSGN = "", IdFarmaciaCSGN = "", sFolioPedidoRC = "", IdJurisdiccionCSGN = "";
        int iCantidadSurtidaCSGN = 0;

        bool bContinua = true;
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

        bool bFolioGuardado = false;

        #region Variables Receta Electronica 
        clsListView lstClaves;
        clsListView lstDiagnosticos;
        string sClavesRecetaElectronica = ""; 
        #endregion Variables Receta Electronica

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


        FrmIniciarSesionEnCaja Sesion;
        // bool bSesionIniciada = false;

        string sCodigoEAN_Seleccionado = ""; 
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsVerificarSalidaLotes VerificarLotes;
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();

        #region Vales 
        bool bEsIdProducto_Ctrl = false; 
        #endregion Vales

        #endregion variables

        public FrmVentas_RecetaElectronica()
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

            GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Precio, (int)Cols.Importe, (int)Cols.Descripcion);

            GetInformacionRecetarioElectronico(); 
        }

        private void GetInformacionRecetarioElectronico()
        {
            lblCLUES.Text += DtRecetaElectronica.CLUES;
            lblUMedica.Text += DtRecetaElectronica.NombreUnidadMedica;
            lblUMedica.Text = lblUMedica.Text.ToUpper();

            lstClaves = new clsListView(lstwClaves);
            lstDiagnosticos = new clsListView(lstwDiagnosticos); 
        }

        private void FrmVentas_RecetaElectronica_Load(object sender, EventArgs e)
        {
            if (!bEsSurtimientoPedido)
            {
                btnNuevo_Click(this, null);
            }

            // txtIdPersonal.Text = DtGeneral.IdPersonal;
            // lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            // Determinar si se muestra la Captura de Claves Solicitadas 
            lblMensajes.Text = "<F5>Ver información adicional de venta                                                                               <F7> Ver lotes a artículo";
            //if (GnFarmacia.CapturaDeClavesSolicitadasHabilitada)
            if(bCapturaDeClavesSolicitadasHabilitada)
            {
                lblMensajes.Text = "<F5>Ver información adicional de venta      <F9>Ver captura de Claves solicitadas      <F7> Ver lotes a artículo";
            }

            tmSesion.Enabled = true;
            tmSesion.Start();

            txtFolioRecetaElectronica.Focus(); 
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

        private void FrmVentas_RecetaElectronica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                TeclasRapidas(e);

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

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;

            if (!DtRecetaElectronica.ManejaRecetaElectronica)
            {
                General.msjUser("La farmacia actual no esta configurada para el manejo de Recetas electrónicas."); 
                this.Close(); 
            }
            else
            {
                validarSESION(); 
            }
        }

        private void validarSESION()
        {
            FrmFechaSistema Fecha = new FrmFechaSistema();
            Fecha.ValidarFechaSistema();

            GnFarmacia.ValidarSesionUsuario = true;
            if (Fecha.Exito)
            {
                GnFarmacia.Parametros.CargarParametros();
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
                    if (!bEsSurtimientoPedido)
                    {
                        btnNuevo_Click(null, null);
                    }
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
            IniciarToolBar(false, false, false, false); 
        }
 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Vale)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnVale.Enabled = Vale;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
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
            myGrid.Limpiar(true);

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;             
            dtpFechaDeSistema.Enabled = false;
            dtpFechaDeSistema.Value = GnFarmacia.FechaOperacionSistema;


            //txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            CambiaEstado(true);
            fSubTotal = 0; fIva = 0; fTotal = 0;


            //txtIdPersonal.Text = DtGeneral.IdPersonal;
            //lblPersonal.Text = DtGeneral.NombrePersonal;

            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
            Lotes.ManejoLotes = OrigenManejoLotes.Ventas_Dispensacion;
            Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia; 

            // Informacion detallada de la venta 
            InfVtas = new clsInformacionVentas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            InfCveSolicitadas = new clsClavesSolicitadas(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;
            IniciarToolBar(false, false, false, false);

            chkTipoImpresion.Visible = false;
            chkTipoImpresion.Checked = true; 
            chkMostrarImpresionEnPantalla.Checked = false;

            if (DtGeneral.EsAlmacen)
            {
                chkTipoImpresion.Visible = true;
                chkTipoImpresion.Checked = true;
                chkMostrarImpresionEnPantalla.Checked = true;
            }

            lstClaves.Limpiar();
            lstDiagnosticos.Limpiar();
            sClavesRecetaElectronica = ""; 

            //txtFolio.Focus(); 
            txtFolioRecetaElectronica.Focus(); 
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bContinua = false;
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
                                    if (GuardaVentasDet_Lotes())
                                    {
                                        if (GuardarClavesSolicitadas())
                                        {
                                            if (GuardarInformacionPreciosLicitacion())
                                            {
                                                bContinua = AfectarExistencia(true, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //// Atencion de pedidos especiales 
                        if (bContinua && bEsSurtimientoPedido)
                        {
                            bContinua = ActualizarEstatusPedido();

                            if (bContinua)
                            {
                                bContinua = RevisarPedidoCompleto();

                                if (bContinua)
                                {
                                    bContinua = RegistrarAtencion();
                                }
                            }
                        }

                        if (bContinua)
                        {
                            con.CompletarTransaccion();
                            
                            // IMach  // Enlazar el folio de inventario 
                            RobotDispensador.Robot.TerminarSolicitud(sFolioMovtoInv);


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

                            if (bEsSurtimientoPedido)
                            {
                                this.Hide(); 
                            }
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*"; 
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir, bGeneroVale); 
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

        private bool GuardaVenta()
        {
            bool bRegresa = true; 
            string sSql = "", sCaja = GnFarmacia.NumCaja;
            int iOpcion = 1;

            // Asignar el valor a la variable global 
            sFolioVenta = txtFolio.Text;

            CalcularTotales();

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 
                    sFolioVenta, 
                    sFechaSistema, Fg.PonCeros(sCaja,2), DtGeneral.IdPersonal, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
                    Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4), fSubTotal, fIva, fTotal, (int)TipoDeVenta.Credito, iOpcion);

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
                    sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                        DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta, sIdTipoMovtoInv, sTipoES, "", DtGeneral.IdPersonal, "", fSubTotal, fIva, fTotal, 1); 
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

            return bRegresa; 
        }

        private bool GuardarInformacionPreciosLicitacion()
        {
            bool bRegresa = true; 
            // 2K110426-1004  
            string sSql = string.Format(" EXEC spp_Mtto_Ventas_AsignarPrecioLicitacion '{0}', '{1}', '{2}', '{3}' ",
                   DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta);
            bRegresa = leer.Exec(sSql);

            return bRegresa; 
        }

        private bool GuardaVentaInformacionAdicional()
        {
            bool bRegresa = true;

            string sSql = string.Format(" EXEC spp_Mtto_VentasInformacionAdicional '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                   DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                   sFolioVenta, InfVtas.Beneficiario, InfVtas.Receta, General.FechaYMD(InfVtas.FechaReceta, "-"),
                   InfVtas.TipoDispensacion, InfVtas.CluesRecetasForaneas, InfVtas.Medico, InfVtas.IdBeneficio, InfVtas.Diagnostico, InfVtas.Servicio, InfVtas.Area, 
                   InfVtas.ReferenciaObservaciones, 1 );

            bRegresa = leer.Exec(sSql);

            return bRegresa;
        }

        private void GuardaVenta_ALMJ_PedidosRC_Surtido()
        {
            int iOpcion = 1, iCantidadEntregada = 0;

            if (bContinua)
            {
                iCantidadEntregada = myGrid.TotalizarColumna((int)Cols.Cantidad);

                string sSql = string.Format(" Exec spp_Mtto_Ventas_ALMJ_PedidosRC_Surtido '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
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
                    sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}' ",
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
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
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

                foreach (clsLotes L in ListaLotes)
                {
                    if (sIdProducto != "" && L.Cantidad > 0)
                    {
                        sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasDet_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                                                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                                                L.IdSubFarmacia, sFolioVenta, Fg.PonCeros(sIdProducto, 8),
                                                sCodigoEAN, L.ClaveLote, iRenglon, L.Cantidad, iOpcion);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            // Grabar en los Movimientos de inventario 
                            sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv, sIdProducto, sCodigoEAN, L.ClaveLote, L.Cantidad, dCostoUnitario, (L.Cantidad * dCostoUnitario), 'A');
                            
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
                    sSql = string.Format("Exec spp_Mtto_VentasDet_Lotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                         " '{9}', '{10}', '{11}', '{12}', '{13}' ",
                                         DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioVenta, L.IdProducto, L.CodigoEAN,
                                         L.ClaveLote, Renglon.ToString(), L.Pasillo, L.Estante, L.Entrepano, L.Cantidad, iOpcion);

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }                                            
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovtoInv,
                            L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A');

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
                    sSql = string.Format(" Exec spp_Mtto_VentasClavesSolicitadas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
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

            sSql = string.Format(" Exec spp_Mtto_VentasClavesSolicitadasCalcularSurtimiento '{0}', '{1}', '{2}', '{3}' ",
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

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovtoInv, (int)Inv, (int)Costo);

            bool bRegresa = leer.Exec(sSql);
            bContinua = bRegresa;
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
            sFolioVenta = Fg.PonCeros(txtFolio.Text, 8);

            VtasImprimir.MostrarVistaPrevia = chkMostrarImpresionEnPantalla.Checked;
            VtasImprimir.MostrarImpresionDetalle = GnFarmacia.ImpresionDetalladaTicket;

            if (DtGeneral.EsAlmacen)
            {
                VtasImprimir.MostrarImpresionDetalle = chkTipoImpresion.Checked;
                VtasImprimir.MostrarPrecios = true; 
            }

            if (VtasImprimir.Imprimir(sFolioVenta))
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
            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false; 

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                IniciarToolBar(true, false, false, false);
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                txtCte.Focus();
            }
            else
            {
                leer.DataSetClase = Consultas.FolioEnc_Ventas(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text, 8), "txtFolio_Validating");
                if (!leer.Leer())
                {
                    General.msjUser("El Folio de Venta no encontrado, verifique.");
                    txtFolio.Text = "";
                    txtFolio.Focus();
                }
                else
                {
                    if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") != TipoDeVenta.Credito)
                    {
                        General.msjUser("El folio de venta capturado no es de venta de credito, verifique.");
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
                    General.msjAviso("El Cliente Publico General es exclusivo de Venta a Contado, no puede ser utilizado en Venta a Credito");
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

                    txtPro.Text = "";
                    lblPro.Text = "";
                    txtSubPro.Text = "";
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

                    // Obtener datos de IMach 
                    sFolioSolicitud = RobotDispensador.Robot.ObtenerFolioSolicitud();

                    // Exclusivo Seguro Popular 
                    if (bEsSeguroPopular)
                    {
                        MostrarInfoVenta();
                    }

                    if (!bEsSurtimientoPedido)
                    {
                        myGrid.Limpiar(true);
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
                General.msjUser("Folio de Venta inválido, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtCte.Text == "")
            {
                bRegresa = false;
                General.msjUser("Clave de Cliente inválida, verifique.");
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

            return bRegresa;
        }

        private bool validarInfAdicional_FarmaciasAlmacen()
        {
            bool bRegresa = true;
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

            for(int i = 1; i< myGrid.Rows; i++)
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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Venta no puede ser completada."); 
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
                "Set Status = 'T', FolioTransferenciaReferencia = '{0}' " +
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
            //            General.msjUser("El folio de venta capturado no es de venta de contado, verifique.");
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

        private void grdProductos_EditModeOff_1(object sender, EventArgs e)
        {
            Cols iCol = (Cols)myGrid.ActiveCol; 
            switch (iCol)
            {
                case Cols.CodEAN: 
                    ObtenerDatosProducto();
                    break;
            }
        }

        private void grdProductos_EditModeOn_1(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
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
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (!bEsIdProducto_Ctrl)
                        {
                            if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
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

            if (bFolioGuardado)
            {
                ColActiva = Cols.Ninguna; 
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
                                leer.DataSetClase = Ayuda.ProductosFarmacia_RecetaElectronica(sEmpresa, sEstado, sFarmacia, sClavesRecetaElectronica, bDispensarSoloCuadroBasico, "grdProductos_KeyDown_1");
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
                            if (!bEsIdProducto_Ctrl)
                            {
                                removerLotes();
                            }
                        }

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}

                        // Administracion de Mach4 
                        if (e.KeyCode == Keys.F10)
                        {
                            if (RobotDispensador.Robot.EsClienteInterface && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
                            {
                                string sIdProducto = myGrid.GetValue(iRowActivo, (int)Cols.Codigo);
                                string sCodigoEAN = myGrid.GetValue(iRowActivo, (int)Cols.CodEAN);
                                
                                if ( sIdProducto != "" )
                                {
                                    RobotDispensador.Robot.Show(sIdProducto, sCodigoEAN);
                                    mostrarOcultarLotes();
                                }
                            }
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
                        }

                        // Administracion de Mach4 
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
            string sCodigo = "", sSql = ""; 
            bool bCargarDatosProducto = true;
            string sMsj = ""; 

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            if (EAN.EsValido(sCodigo) && sCodigo != "") 
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
                {
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN); 
                }
                else 
                {
                    //// Solo receta electronica 
                    sCodigo = sCodigoEAN_Seleccionado;  
                    ///sSql = string.Format("Exec Spp_ProductoVentasFarmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', [ {9} ]  ",
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " +
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " +
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = [ {9} ],  " + 
                        " @INT_OPM_ProcesoActivo = '{10}' ",
                        (int)TipoDeVenta.Credito, txtCte.Text.Trim(), txtSubCte.Text.Trim(),
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1,
                        Convert.ToInt32(RobotDispensador.Robot.EsClienteInterface),
                        sClavesRecetaElectronica, Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo));
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
                            myGrid.LimpiarRenglon(myGrid.ActiveRow);
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
                                        sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Basico Autorizado, verifique."; 
                                    }
                                }

                                if (!leer.CampoBool("EsDeRecetaElectronica"))
                                {
                                    bCargarDatosProducto = false;
                                    sMsj = "El Producto " + leer.Campo("Descripcion") + " no pertenece a las Claves de la Receta electrónica, verifique.";
                                }
                            }

                            if (!bCargarDatosProducto)
                            {
                                General.msjUser(sMsj);
                                myGrid.LimpiarRenglon(myGrid.ActiveRow);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodEAN); 
                            }
                            else
                            {
                                CargaDatosProducto();
                            }
                        }
                    }
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

        private void ActualizarColorFondo()
        {
            if (RobotDispensador.Robot.EsClienteInterface)
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
            int iRowActivo = myGrid.ActiveRow;           
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

                            bEsMach4 = leer.CampoBool("EsMach4");
                            myGrid.SetValue(iRowActivo, (int)Cols.EsIMach4, bEsMach4);

                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);

                            // Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                            if (RobotDispensador.Robot.EsClienteInterface)
                            {
                                if (bEsMach4)
                                {
                                    GnFarmacia.ValidarCodigoIMach4(myGrid, bEsMach4, iRowActivo);
                                    RobotDispensador.Robot.Show(leer.Campo("IdProducto"), sCodEAN);
                                }
                            }

                            CargarLotesCodigoEAN();
                            // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
                        }
                        else
                        {
                            General.msjUser("El artículo ya se encuentra capturado en otro renglon.");
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
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                leer.Leer();
                Lotes.AddLotes(leer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    leer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Todos, false, "CargarLotesCodigoEAN()");
                    if (Consultas.Ejecuto)
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
                }
                catch { }

                if (myGrid.Rows == 0)
                {
                    myGrid.Limpiar(true);
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
            //bool bRegresa = true;

            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular; 
                InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios; 
                InfVtas.Show(txtFolio.Text, txtCte.Text, lblCte.Text, txtSubCte.Text, lblSubCte.Text);
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

        #region Receta Electrónica 
        private void txtFolioRecetaElectronica_Validating(object sender, CancelEventArgs e)
        {
            if (txtFolioRecetaElectronica.Text.Trim() != "")
            {
                SolicitarRecetaElectronica_Local(); 
            }
        }

        private void SolicitarRecetaElectronica()
        {
            clsDescargarRecetaElectronica f = new clsDescargarRecetaElectronica(General.DatosConexion, 
                DtRecetaElectronica.UrlRepositorio, sEmpresa, sEstado, DtRecetaElectronica.CLUES, txtFolioRecetaElectronica.Text);

            if (!f.Descargar()) 
            {
                General.msjUser("No fue posible obtener la información de la receta electrónica"); 
            }
            else
            {
                SolicitarRecetaElectronica_Local();
                General.msjUser("Receta electrónica descargada satisfactoriamente.");
            }
        }

        private bool SolicitarRecetaElectronica_Local()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select IdEstado, IdUMedica, FolioReceta, FechaRegistro, " + 
                " IdMedico, NombreMedico, CedulaMedico, NombreBeneficiario, PolizaReferencia, TipoBeneficiario, Observaciones, Status " + 
                "From REC_Recetas (NoLock) " +
                "Where IdEstado = '{0}' and IdUMedica = '{1}' and FolioReceta = '{2}' ", 
                sEstado, DtRecetaElectronica.IdUMedica, Fg.PonCeros(txtFolioRecetaElectronica.Text, 8));

            lstClaves.Limpiar();
            lstDiagnosticos.Limpiar(); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "SolicitarRecetaElectronica_Local()");
                General.msjError("Ocurrió un error al cargar la información de Receta electrónica");                 
            }
            else 
            {
                if (leer.Leer())
                {
                    txtFolioRecetaElectronica.Enabled = false;
                    txtFolioRecetaElectronica.Text = leer.Campo("FolioReceta");
                    dtpFechaRecetaElectronica.Enabled = false;
                    dtpFechaRecetaElectronica.Value = leer.CampoFecha("FechaRegistro"); 

                    bRegresa = true;
                    Get_RecetaElectronica_Claves();
                    Get_RecetaElectronica_Diagnosticos(); 
                }
            }

            if (!bRegresa)
            {
                SolicitarRecetaElectronica();
            }

            AjustarColumnas(); 

            return bRegresa;
        }

        private void AjustarColumnas()
        {
            lstClaves.AnchoColumna(1, 70);
            lstClaves.AnchoColumna(2, 145);
            lstClaves.AnchoColumna(3, 95);
            lstClaves.AnchoColumna(4, 75);

            lstDiagnosticos.AnchoColumna(1, 95);
            lstDiagnosticos.AnchoColumna(2, 290); 

        }

        private void GetListaClavesRecetaElectronica(DataSet ListaDeClaves)
        {
            clsLeer leerClaves = new clsLeer();
            leerClaves.DataSetClase = ListaDeClaves; 
            
            sClavesRecetaElectronica = "";
            while (leerClaves.Leer())
            {
                sClavesRecetaElectronica += string.Format("'{0}', ", leerClaves.Campo("Clave SSA"));
            }

            if (sClavesRecetaElectronica != "")
            {
                sClavesRecetaElectronica = sClavesRecetaElectronica.Trim();
                sClavesRecetaElectronica = Fg.Mid(sClavesRecetaElectronica, 1, sClavesRecetaElectronica.Length-1);
            }
        }

        private void Get_RecetaElectronica_Claves()
        {
            string sSql = string.Format("Select 'Clave SSA'= ClaveSSA, 'Descripción' = DescripcionClave, 'Presentación'= Presentacion, Cantidad " +
                "From REC_Recetas_ClavesSSA (NoLock) " +
                "Where IdEstado = '{0}' and IdUMedica = '{1}' and FolioReceta = '{2}' ",
                sEstado, DtRecetaElectronica.IdUMedica, Fg.PonCeros(txtFolioRecetaElectronica.Text, 8));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Get_RecetaElectronica_Claves()");
                General.msjError("Ocurrió un error al cargar la información de las Claves de receta electrónica"); 
            }
            else
            {
                lstClaves.CargarDatos(leer.DataSetClase, true, true); 
            }

            GetListaClavesRecetaElectronica(leer.DataSetClase); 
        }

        private void Get_RecetaElectronica_Diagnosticos()
        {
            string sSql = string.Format("Select 'Clave CIE-10'= CIE_10, 'Descripción' = Descripcion " +
                "From REC_Recetas_Diagnosticos (NoLock) " +
                "Where IdEstado = '{0}' and IdUMedica = '{1}' and FolioReceta = '{2}' ",
                sEstado, DtRecetaElectronica.IdUMedica, Fg.PonCeros(txtFolioRecetaElectronica.Text, 8));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Get_RecetaElectronica_Diagnosticos()");
                General.msjError("Ocurrió un error al cargar la información de Diagnósticos de receta electrónica"); 
            }
            else
            {
                lstDiagnosticos.CargarDatos(leer.DataSetClase, true, true);
            }
        }
        #endregion Receta Electrónica
    }
}
