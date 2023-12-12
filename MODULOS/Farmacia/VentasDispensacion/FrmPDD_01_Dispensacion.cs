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
using Farmacia.Vales;

//using Dll_IMach4;
//using Dll_IMach4.Interface;
using DllRobotDispensador; 

using Farmacia.Ventas; 

namespace Farmacia.VentasDispensacion
{
    public partial class FrmPDD_01_Dispensacion : FrmBaseExt
    {
        #region Redimension de controles 
        int iAnchoPantalla = (int)(General.Pantalla.Ancho * 1.0); 
        int iAltoPantalla = (int)(General.Pantalla.Alto * 0.80);
        int iAnchoGrid = 0;
        int iAnchoGrid_Ajuste = 0;
        int iDiferenciaGrid = 0;

        int iAnchoFrameInformacion = 0;
        int iAnchoFrameInformacion_Ajuste = 0;
        int iDiferenciaFrameInformacion = 0; 
        #endregion Redimension de controles 

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
            Precio = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
            EsIMach4 = 11, UltimoCosto = 12, VerLotes = 13 
        }

        #region Variables para control del dispensacion 

        string sClave_Dispensacion = ""; 
        string sIdCliente = "";
        string sIdSubCliente = "";
        string sIdPrograma = "";
        string sIdSubPrograma = "";
        string sIdPersonal = "";
        string sNombrePersonal = ""; 
        #endregion Variables para control del dispensacion

        #region Variables
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
        string sMensaje = "", sFolioVenta = "";

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
        clsLotesExt Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsVerificarSalidaLotes VerificarLotes;
        FrmRevisarCodigosEAN RevCodigosEAN = new FrmRevisarCodigosEAN();

        #region Vales
        bool bEsIdProducto_Ctrl = false;
        #endregion Vales 

        #endregion Variables 

        public FrmPDD_01_Dispensacion()
        {
            InitializeComponent();
            AjustarTamaño_Pantalla();

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


            // Inicializar el Grid 
            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White; 

            //////////////// Quitar 
            ////DtGeneral.EsAdministrador = true;
            ////GnFarmacia.MostrarPrecios_y_Costos = true; 

            //GnFarmacia.AjustaColumnasImportes(grdProductos, (int)Cols.Precio, (int)Cols.Importe, (int)Cols.Descripcion);

            //////// Determinar si se muestra la Captura de Claves Solicitadas 
            //////lblMensajes.Text = "<F5>Ver información adicional de venta                                                                               <F7> Ver lotes a artículo";
            //////if (GnFarmacia.CapturaDeClavesSolicitadasHabilitada)
            //////{
            //////    lblMensajes.Text = "<F5>Ver información adicional de venta      <F9>Ver captura de Claves solicitadas      <F7> Ver lotes a artículo";
            //////}
        }

        #region Form 
        private void FrmPDD_01_Dispensacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
            sIdPersonal = DtGeneral.IdPersonal;
            sNombrePersonal = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;

            CargarClientes(); 

            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        private void FrmPDD_01_Dispensacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }

            switch (e.KeyCode)
            {
                case Keys.F3:
                    btnDatosDocumento_Click(null, null);
                    break;

                case Keys.F4:
                    btnDatosDiagnostico_Click(null, null);
                    break;

                case Keys.F5:
                    btnDatosBeneficiario_Click(null, null);
                    break;

                case Keys.F6:
                    btnDatosServicioArea_Click(null, null);
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

                default:
                    break;
            }
        }
        #endregion Form
        
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

            sClave_Dispensacion = "";
            sIdCliente = "";
            sIdSubCliente = "";
            sIdPrograma = "";
            sIdSubPrograma = "";

            bEsSeguroPopular = false;
            bFolioGuardado = false;
            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

            //myGrid.BloqueaColumna(true, 1);
            myGrid.Limpiar(true);

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;

            fSubTotal = 0; fIva = 0; fTotal = 0;

            CambiaEstado(true);
            sIdPersonal = DtGeneral.IdPersonal;
            sNombrePersonal = DtGeneral.NombrePersonal; 

            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Lotes = new clsLotesExt(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
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
                chkTipoImpresion.Checked = false;
                chkMostrarImpresionEnPantalla.Checked = true;
            }

            txtFolio.Focus();

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
                    if (!con.Abrir())
                    {
                        General.msjErrorAlAbrirConexion(); 
                    }
                    else 
                    {
                        IniciarToolBar();
                        con.IniciarTransaccion(); 

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

            //Asignar valores a cliente, subcliente, programa y subprograma.
            sIdCliente = ((DataRow)cboCliente.ItemActual.Item)["IdCliente"].ToString();
            sIdSubCliente = ((DataRow)cboCliente.ItemActual.Item)["IdSubCliente"].ToString();
            sIdPrograma = ((DataRow)cboCliente.ItemActual.Item)["IdPrograma"].ToString();
            sIdSubPrograma = ((DataRow)cboCliente.ItemActual.Item)["IdSubPrograma"].ToString();

            CalcularTotales();

            ////sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
            ////        DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
            ////        sFolioVenta,
            ////        sFechaSistema, Fg.PonCeros(sCaja, 2), txtIdPersonal.Text, Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4),
            ////        Fg.PonCeros(txtPro.Text, 4), Fg.PonCeros(txtSubPro.Text, 4), fSubTotal, fIva, fTotal, (int)TipoDeVenta.Credito, iOpcion);

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                    sFolioVenta,
                    sFechaSistema, Fg.PonCeros(sCaja, 2), sIdPersonal, Fg.PonCeros(sIdCliente, 4), Fg.PonCeros(sIdSubCliente, 4),
                    Fg.PonCeros(sIdPrograma, 4), Fg.PonCeros(sIdSubPrograma, 4), fSubTotal, fIva, fTotal, (int)TipoDeVenta.Credito, iOpcion);

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
                   InfVtas.ReferenciaObservaciones, 1);

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
            double dCostoUnitario = 0, dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0, dPorcDescto = 0, dImpteDescto = 0;

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
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioVenta);
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
                sEmpresa, sEstado, sFarmacia, sFolioVenta);

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
            {
                Inv = AfectarInventario.Aplicar;
            }

            if (AfectarCosto)
            {
                Costo = AfectarCostoPromedio.Afectar;
            }

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
        #endregion Botones

        #region Redimensionar form 
        private void AjustarTamaño_Pantalla()
        {
            iAnchoGrid = grdProductos.Width;
            iAnchoFrameInformacion = FrameInformacion.Width;

            ////General.Pantalla.Ancho = Screen.PrimaryScreen.WorkingArea.Size.Width;
            ////General.Pantalla.Alto = Screen.PrimaryScreen.WorkingArea.Size.Height; 

            ////iAnchoPantalla = (int)(General.Pantalla.Ancho * 0.98);
            ////iAltoPantalla = (int)(General.Pantalla.Alto * 0.85);

            iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);


            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;

            //int iAnchoFrameInformacion = 0;
            //int iAnchoFrameInformacion_Ajuste = 0;
            //int iDiferenciaFrameInformacion = 0; 

            iAnchoGrid_Ajuste = grdProductos.Width;
            iDiferenciaGrid = iAnchoGrid_Ajuste - iAnchoGrid; 
            iAnchoFrameInformacion_Ajuste = FrameInformacion.Width;
            iDiferenciaFrameInformacion = iAnchoFrameInformacion_Ajuste - iAnchoFrameInformacion;   


            AjustarGrid();
            AjustarBotonesInformacion(); 
        }

        private void AjustarBotonesInformacion()
        {
            double dPorcentajeAjuste = iDiferenciaFrameInformacion / 6.0;
            int iTam = btnDatosDocumento.Width + (int)dPorcentajeAjuste;
            int iEspacio = 7;
            int iPosicion = iTam + iEspacio + btnDatosDocumento.Left + 2;

            //btnDatosBeneficiario.Width = iTam;
            btnDatosDocumento.Left += 2;
            btnDatosDocumento.Width = iTam; 

            btnDatosDiagnostico.Width = iTam;
            btnDatosDiagnostico.Left = iPosicion;
            iPosicion += (iTam + iEspacio);

            btnDatosBeneficiario.Width = iTam;
            btnDatosBeneficiario.Left = iPosicion;
            iPosicion += (iTam + iEspacio);

            btnDatosServicioArea.Width = iTam;
            btnDatosServicioArea.Left = iPosicion;
            iPosicion += (iTam + iEspacio);

            btnDatosLotes.Width = iTam;
            btnDatosLotes.Left = iPosicion;
            iPosicion += (iTam + iEspacio); 

            btnDatosNoSurtido.Width = iTam;
            btnDatosNoSurtido.Left = iPosicion;
            iPosicion += (iTam + iEspacio); 


            ////btnDatosDocumento.Left += (int)dPorcentajeAjuste;
            ////btnDatosDiagnostico.Left += (int)dPorcentajeAjuste;
            ////btnDatosServicioArea.Left += (int)dPorcentajeAjuste;
            ////btnDatosNoSurtido.Left += (int)dPorcentajeAjuste; 


        }

        private void AjustarGrid()
        {
            double dPorcentajeAjuste = 0;
            double dAnchoActual = 0;
            double dAnchoAjuste = 0; 
            double dAnchoNuevo = 0;

            double iEAN = iDiferenciaGrid * 0.075;
            double iCantidad = iDiferenciaGrid * 0.075;
            double iPrecio = iDiferenciaGrid * 0.075;
            double iImporte = iDiferenciaGrid * 0.075;
            double iDescripcion = iDiferenciaGrid * 0.70;



            dPorcentajeAjuste = ((double)iAnchoGrid / (double)iAnchoGrid_Ajuste);
            dPorcentajeAjuste = ((double)iDiferenciaGrid / (double)iAnchoGrid);


            dAnchoActual = grdProductos.Sheets[0].Columns[(int)Cols.CodEAN - 1].Width + iEAN;
            grdProductos.Sheets[0].Columns[(int)Cols.CodEAN - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width + iDescripcion;
            grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdProductos.Sheets[0].Columns[(int)Cols.Cantidad - 1].Width + iCantidad;
            grdProductos.Sheets[0].Columns[(int)Cols.Cantidad - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Width + iPrecio;
            grdProductos.Sheets[0].Columns[(int)Cols.Precio - 1].Width = (float)dAnchoActual;

            dAnchoActual = grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Width + iImporte;
            grdProductos.Sheets[0].Columns[(int)Cols.Importe - 1].Width = (float)dAnchoActual;

            ////dAnchoAjuste = dAnchoActual * dPorcentajeAjuste;
            ////dAnchoNuevo = dAnchoAjuste + dAnchoActual;
            ////grdProductos.Sheets[0].Columns[(int)Cols.Descripcion - 1].Width = (float)dAnchoNuevo;
            
            
            //////for (int i = 0; i<= myGrid.Columns - 1; i++)
            //////{
            //////    dAnchoActual = grdProductos.Sheets[0].Columns[i].Width;
            //////    dAnchoAjuste = dAnchoActual * dPorcentajeAjuste;
            //////    dAnchoNuevo = dAnchoAjuste + dAnchoActual;
            //////    grdProductos.Sheets[0].Columns[i].Width = (float)dAnchoNuevo; 
            //////}

        }

        #endregion Redimensionar form 

        #region Cargar informacion de clientes 
        private void CargarClientes()
        {
            cboCliente.Clear();
            cboCliente.Add();

            cboCliente.Add(Consultas.Farmacia_Clientes_Programas_Dispensacion(sEstado, sFarmacia, "CargarClientes()"), true, "Clave_Dispensacion", "SubPrograma"); 

            cboCliente.SelectedIndex = 0; 
        }
        #endregion Cargar informacion de clientes

        #region Informacion adicional
        private void btnDatosDocumento_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.ShowDocumento(txtFolio.Text);
            } 
        }

        private void btnDatosDiagnostico_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.ShowDiagnosticos(txtFolio.Text);
            }
        }

        private void btnDatosBeneficiario_Click(object sender, EventArgs e)
        {
            sIdCliente = ((DataRow)cboCliente.ItemActual.Item)["IdCliente"].ToString();
            sIdSubCliente = ((DataRow)cboCliente.ItemActual.Item)["IdSubCliente"].ToString();

            if (cboCliente.SelectedIndex != 0)
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;
                InfVtas.ShowBeneficiarios(txtFolio.Text, sIdCliente, sIdSubCliente);
            }
        }

        private void btnDatosServicioArea_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.ShowServiciosAreas(txtFolio.Text);
            }
        }

        private void btnDatosLotes_Click(object sender, EventArgs e)
        {
            mostrarOcultarLotes(); 
        }

        private void btnDatosNoSurtido_Click(object sender, EventArgs e)
        {
            MostrarCapturaDeClavesRequeridas();
        }
        #endregion Informacion adicional

        #region Control de sesión de caja 
        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
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
                    btnNuevo_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }
        }
        #endregion Control de sesión de caja
        
        #region Validaciones
        private void CambiaEstado(bool Valor)
        {
            txtFolio.Enabled = Valor;
            cboCliente.Enabled = Valor;             
        }

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
                //txtCte.Focus();
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
                        CambiaEstado(false); 
                        bFolioGuardado = true;
                        IniciarToolBar(false, false, true, false);
                        sFolio = leer.Campo("Folio");
                        sFolioVenta = sFolio;
                        txtFolio.Text = sFolio;

                        //dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                        dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");

                        ////txtCte.Text = leer.Campo("IdCliente");
                        ////lblCte.Text = leer.Campo("NombreCliente");
                        ////txtSubCte.Text = leer.Campo("IdSubCliente");
                        ////lblSubCte.Text = leer.Campo("NombreSubCliente");
                        ////txtPro.Text = leer.Campo("IdPrograma");
                        ////lblPro.Text = leer.Campo("Programa");
                        ////txtSubPro.Text = leer.Campo("IdSubPrograma");
                        ////lblSubPro.Text = leer.Campo("SubPrograma");

                        sIdCliente = leer.Campo("IdCliente");
                        sIdSubCliente = leer.Campo("IdSubCliente");
                        sIdPrograma = leer.Campo("IdPrograma");
                        sIdSubPrograma = leer.Campo("IdSubPrograma");

                        sClave_Dispensacion = ""; 
                        sClave_Dispensacion = sIdCliente;
                        sClave_Dispensacion += sIdSubCliente;
                        sClave_Dispensacion += sIdPrograma;
                        sClave_Dispensacion += sIdSubPrograma;

                        cboCliente.Data = sClave_Dispensacion; 

                        if (leer.Campo("Status") == "C")
                        {
                            lblCancelado.Visible = true;
                        }
                        CargaDetallesVenta();
                        BuscarVale();
                    }
                }
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

            ////if (bRegresa && txtCte.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de Cliente inválida, verifique.");
            ////    txtCte.Focus();
            ////}

            ////if (bRegresa && txtSubCte.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de SubCliente inválida, verifique.");
            ////    txtSubCte.Focus();
            ////}

            ////if (bRegresa && txtPro.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de Programa inválida, verifique.");
            ////    txtPro.Focus();
            ////}

            ////if (bRegresa && txtSubPro.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de SubPrograma inválida, verifique.");
            ////    txtSubPro.Focus();
            ////}


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
        private void ObtieneClaveLote(string sIdProducto, string sCodigoEAN, ref string sClaveLote)
        {
            string sSql = "";
            leer3 = new clsLeer(ref con);

            sSql = string.Format(" SELECT TOP 1 ClaveLote FROM FarmaciaProductos_CodigoEAN_Lotes (nolock) " +
                                   " WHERE CodigoEAN = '{0}' AND IdProducto = '{1}'  ", sCodigoEAN, Fg.PonCeros(sIdProducto, 8));

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
            return bRequiere;
        }

        private bool BuscarVale()
        {
            bool bRegresa = false; 
            return bRegresa;
        }
        #endregion Funciones   

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
        #endregion Eventos    

        #region Grid 
        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            Cols iCol = (Cols)myGrid.ActiveCol;
            switch (iCol)
            {
                case Cols.CodEAN:
                    ObtenerDatosProducto();
                    break;
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
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

        private void grdProductos_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            mostrarOcultarLotes(); 
        }

        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
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
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, bDispensarSoloCuadroBasico, "grdProductos_KeyDown_1");
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

                            if (sIdProducto != "")
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
                    sCodigo = sCodigoEAN_Seleccionado;
                    sSql = string.Format("Exec Spp_ProductoVentasFarmacia " +
                        " @Tipo = '{0}', @IdCliente = '{1}', @IdSubCliente = '{2}', @IdCodigo = '{3}', @CodigoEAN = '{4}', " +
                        " @IdEstado = '{5}', @IdFarmacia = '{6}', @EsSectorSalud = '{7}', @EsClienteIMach = '{8}', @ClavesRecetaElectronica = '{9}',  " +
                        " @INT_OPM_ProcesoActivo = '{10}' ",
                        (int)TipoDeVenta.Credito, sIdCliente, sIdSubCliente,
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, Convert.ToInt32(RobotDispensador.Robot.EsClienteInterface), "", 
                        Convert.ToInt32(GnFarmacia.INT_OPM_ProcesoActivo));
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

                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    if (myGrid.GetValueBool(i, (int)Cols.EsIMach4))
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

                            //CargarLotesCodigoEAN();
                            if (CargaFormaLotes())
                            {
                                myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, Lotes.Cantidad);
                                myGrid.SetValue(iRowActivo, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                                myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);

                                //activar el siguiente renglon
                                myGrid.Rows = myGrid.Rows + 1;
                                myGrid.ActiveRow = myGrid.Rows;
                                myGrid.SetActiveCell(myGrid.Rows, 1);
                            }
                            else
                            {
                                myGrid.LimpiarRenglon(iRowActivo);
                                myGrid.BloqueaCelda(false, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                            }

                            // myGrid.SetActiveCell(myGrid.iRowActivo, 1);
                            //myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio);
                        }
                        else
                        {
                            //General.msjUser("El artículo ya se encuentra capturado en otro renglon.");
                            //myGrid.SetValue(myGrid.ActiveRow, 1, "");
                            //myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                            myGrid.EnviarARepetido();
                            myGrid.DeleteRow(iRowActivo);

                            iRowActivo = myGrid.ActiveRow;

                            if (CargaFormaLotes())
                            {
                                myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, Lotes.Cantidad);
                                myGrid.SetValue(iRowActivo, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                                myGrid.SetActiveCell(iRowActivo, (int)Cols.Precio); 
                               
                                //activar el siguiente renglon
                                myGrid.Rows = myGrid.Rows + 1;
                                myGrid.ActiveRow = myGrid.Rows;
                                myGrid.SetActiveCell(myGrid.Rows, 1);
                            }
                            //else
                            //{
                            //    myGrid.LimpiarRenglon(iRowActivo);
                            //}
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
            bool bFocoEnGrid = this.ActiveControl.Name.ToUpper() == grdProductos.Name.ToUpper();

            if (!bFocoEnGrid)
            {
                //bFocoEnGrid = this.ActiveControl.Name.ToUpper() == "ButtonCellTypeEditor".ToUpper(); // grdProductos.Name.ToUpper();

                foreach (Control obj in grdProductos.Controls)
                {
                    if (this.ActiveControl.Name.ToUpper() == obj.Name.ToUpper())
                    {
                        bFocoEnGrid = true;
                        break; 
                    }
                }
            }

            // Asegurar que el Grid tenga el Foco.
            if (bFocoEnGrid)
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
                    Lotes.ModificarCantidades = bEsIdProducto_Ctrl ? false : true;
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
                InfCveSolicitadas.Show(sIdCliente, sIdSubCliente, txtFolio.Text);
                //InfCveSolicitadas.Claves(); 
            }
        }

        private void MostrarInfoVenta()
        {
            if (sIdCliente != "" && sIdSubCliente != "")
            {
                InfVtas.ClienteSeguroPopular = bEsSeguroPopular;
                InfVtas.PermitirBeneficiariosNuevos = bPermitirCapturaBeneficiariosNuevos;
                InfVtas.PermitirImportarBeneficiarios = bImportarBeneficiarios;
                InfVtas.Show(txtFolio.Text, sIdSubCliente, "", sIdSubCliente, "");
            }
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

        #region Validar_Permitir_Captura_Beneficiarios_Nuevos
        private void ValidaCapturaBeneficiariosNuevos()
        {
            sIdCliente = ((DataRow)cboCliente.ItemActual.Item)["IdCliente"].ToString();
            sIdSubCliente = ((DataRow)cboCliente.ItemActual.Item)["IdSubCliente"].ToString();

            bPermitirCapturaBeneficiariosNuevos = false;
            bImportarBeneficiarios = false;

            leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, sIdCliente, sIdSubCliente, "txtCte_Validating");

            if (!leer.Leer())
            {
                General.msjUser("Datos de Cliente no encontrados.");                
            }
            else
            {               
                bPermitirCapturaBeneficiariosNuevos = leer.CampoBool("PermitirCapturaBeneficiarios");
                bImportarBeneficiarios = leer.CampoBool("PermitirImportaBeneficiarios");               
            }
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex != 0)
            {
                ValidaCapturaBeneficiariosNuevos();
            }
        }
        #endregion Validar_Permitir_Captura_Beneficiarios_Nuevos

        #region Eventos_Codigo
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            //string sCodigo = "", sSql = "";
            //bool bCargarDatosProducto = true;
            //string sMsj = "";
            //int iRowActivo = myGrid.ActiveRow;            

            //sIdCliente = ((DataRow)cboCliente.ItemActual.Item)["IdCliente"].ToString();
            //sIdSubCliente = ((DataRow)cboCliente.ItemActual.Item)["IdSubCliente"].ToString();

            //sCodigo = txtCodigo.Text;
            //if (EAN.EsValido(sCodigo) && sCodigo != "")
            //{
            //    if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado))
            //    {
            //        txtCodigo.Focus();
            //    }
            //    else
            //    {
            //        sCodigo = sCodigoEAN_Seleccionado;
            //        sSql = string.Format("Exec Spp_ProductoVentasFarmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'  ",
            //            (int)TipoDeVenta.Credito, sIdCliente, sIdSubCliente, Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
            //            Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 1, Convert.ToInt32(IMach4.EsClienteIMach4));
            //        if (!leer.Exec(sSql))
            //        {
            //            Error.GrabarError(leer, "txtCodigo_Validating()");
            //            General.msjError("Ocurrió un error al obtener la información del Producto.");
            //        }
            //        else
            //        {
            //            if (!leer.Leer())
            //            {
            //                General.msjUser("Producto no encontrado ó no esta Asignado a la Farmacia.");
            //                txtCodigo.Text = "";
            //            }
            //            else
            //            {
            //                if (!leer.CampoBool("EsDeFarmacia"))
            //                {
            //                    bCargarDatosProducto = false;
            //                    sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique.";
            //                }
            //                else
            //                {
            //                    if (bDispensarSoloCuadroBasico)
            //                    {
            //                        if (!leer.CampoBool("DCB"))
            //                        {
            //                            bCargarDatosProducto = false;
            //                            sMsj = "El Producto " + leer.Campo("Descripcion") + " no esta dentro del Cuadro Basico Autorizado, verifique.";
            //                        }
            //                    }
            //                }

            //                if (!bCargarDatosProducto)
            //                {
            //                    General.msjUser(sMsj);
            //                    txtCodigo.Text = "";
            //                }
            //                else
            //                {
            //                    CargaDatosCodigo();
                                
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    txtCodigo.Text = "";
            //    txtCodigo.Focus();
            //}

            //txtCodigo.Text = "";
            //lblDescripcionProducto.Text = "";
            //txtCodigo.Focus();
        }       

        private bool CargaFormaLotes()
        {
            string sProducto = "", sCodigoEAN = "", sDescripcion = "";

            sProducto = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Codigo);
            sCodigoEAN = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
            sDescripcion = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion);

            FrmPDD_06_LeerLotes f = new FrmPDD_06_LeerLotes();
            f.MostrarPantalla(sProducto, sCodigoEAN, sDescripcion, bEsIdProducto_Ctrl, Lotes);
            Lotes = f.LotesCodigos;
            return f.bProceso;
        }        
        #endregion Eventos_Codigo    
        
    }
}
