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
using Farmacia.Procesos;

//using Dll_IMach4;
//using Dll_IMach4.Interface; 
using DllRobotDispensador; 

namespace Farmacia.Ventas
{
    public partial class FrmVentasPubGen : FrmBaseExt
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
        clsDatosCliente DatosCliente;
        clsConexionSQL con = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leer2;
        clsLeer leer3;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsGrid myGrid;
        // Variables Globales  ****************************************************
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sMensaje = "", sFolioVenta = "";
        bool bContinua = true;
        double fSubTotalIva_0 = 0, fSubTotal = 0, fIva = 0, fTotal = 0;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");

       //***************************************************************************

        DllFarmaciaSoft.clsAyudas Ayuda = new DllFarmaciaSoft.clsAyudas();
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;

        // Varibles para Venta Publico General
        string sIdCliente = GnFarmacia.PublicoGral;  //"0001";
        string sIdSubCliente = GnFarmacia.PublicoGralSubCliente;  //"0001";
        string sPrograma = GnFarmacia.PublicoGralPrograma;  //"001";
        string sSubPrograma = GnFarmacia.PublicoGralSubPrograma;  //"01";

        bool bFolioGuardado = false;

        #region variables
        // bool bExisteMovto = false;
        // bool bEstaCancelado = false;
        // bool bMovtoAplicado = false;

        string sFolioMovtoInv = "";
        string sFormato = "#,###,###,##0.###0";
        Cols ColActiva = Cols.Ninguna;
        string sValorGrid = "";
        
        string sIdTipoMovtoInv = "SV";
        string sTipoES = "S";
        // string sIdProGrid = "";

        FrmIniciarSesionEnCaja Sesion;
        // bool bSesionIniciada = false;

        string sCodigoEAN_Seleccionado = ""; 
        clsLotes Lotes; // = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
        clsVerificarSalidaLotes VerificarLotes; 

        #endregion variables

        #region Variables Cobro Caja 
        FrmPagoCaja PagoEnCaja;

        private double dTipoDeCambio = GnFarmacia.TipoDeCambioDollar;
        private double dPagoEfectivo = 0;
        private double dPagoDolares = 0;
        private double dPagoCheques = 0;
        private double dCambios = 0;
        #endregion Variables Cobro Caja
        
        
        public FrmVentasPubGen()
        {
            InitializeComponent();
            con.SetConnectionString();
            leer = new clsLeer(ref con);
            leer2 = new clsLeer(ref con);
            leer3 = new clsLeer(ref con);

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda.EsPublicoGeneral = true;

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, false);
            Consultas.EsPublicoGeneral = true; 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente, 
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Contado); 

            myGrid = new clsGrid(ref grdProductos, this); 
            myGrid.EstiloGrid(eModoGrid.ModoRow); 
            grdProductos.EditModeReplace = true; 
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true;

        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            //Para obtener Empresam Estado y Farmacia
            sEmpresa = DtGeneral.EmpresaConectada;
            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            tmSesion.Enabled = true;
            tmSesion.Start();   
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

        private void FrmVentasPubGen_KeyDown(object sender, KeyEventArgs e)
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

                case Keys.F7:
                    mostrarOcultarLotes();
                    break;

                default:
                    // base.OnKeyDown(e);
                    break;
            }
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;
            tmSesion.Stop();

            GnFarmacia.ValidarSesionUsuario = true; 
            if (!ValidarPuntoDeVenta())
            {
                this.Close();
            }
            else 
            {
                FrmFechaSistema Fecha = new FrmFechaSistema();
                Fecha.ValidarFechaSistema();

                if (!Fecha.Exito)
                {
                    this.Close();
                }
                else
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
            }
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        #region Teclas Standar
        //        case Keys.F3:
        //            if ( btnNuevo.Enabled ) 
        //                btnNuevo_Click(null, null);
        //            break;

        //        case Keys.F6: 
        //            if ( btnGuardar.Enabled ) 
        //                btnGuardar_Click(null, null);
        //            break;

        //        case Keys.F8:
        //            if ( btnCancelar.Enabled ) 
        //                btnCancelar_Click(null, null);
        //            break;

        //        case Keys.F10:
        //            // Ejecucion de procesos 
        //            break;

        //        case Keys.F12: 
        //            if ( btnImprimir.Enabled ) 
        //                btnImprimir_Click(null, null);
        //            break;
        //        #endregion Teclas Standar

        //        case Keys.F7:
        //            mostrarOcultarLotes();
        //            break;

        //        default:
        //            base.OnKeyDown(e);
        //            break;
        //    }
        //}

        private bool ValidarPuntoDeVenta()
        {
            bool bRegresa = true;
            leer.DataSetClase = Consultas.Farmacia_Clientes(sIdCliente, true, sEstado, sFarmacia, sIdCliente, sIdSubCliente, "ValidarPuntoDeVenta()");
            if (!leer.Leer())
            {
                bRegresa = false;
                General.msjUser("El Cliente Público General, no esta asignado a la Farmacia, consultar con el Departamento de Sistemas.");
            }
            else
            {
                if (!leer.CampoBool("ManejaVtaPubGral"))
                {
                    bRegresa = false;
                    General.msjUser("La Farmacia no esta configurada para manejar Venta al Público, consulta con el Departento de Sistemas.");
                }
            }
            return bRegresa;
        }

        #region Botones 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }
 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sFolioSolicitud = ""; 

            bFolioGuardado = false;
            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.

            myGrid.BloqueaColumna(false, 1); 
            myGrid.Limpiar(false); 

            dtpFechaRegistro.Enabled = false;
            dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            dtpFechaDeSistema.Enabled = false;
            dtpFechaDeSistema.Value = GnFarmacia.FechaOperacionSistema;

            txtIdPersonal.Enabled = false; // Debe estar inhabilitado todo el tiempo 

            CambiaEstado(true);
            fSubTotalIva_0 = 0; fSubTotal = 0; fIva = 0; fTotal = 0;

            txtIdPersonal.Text = DtGeneral.IdPersonal;
            lblPersonal.Text = DtGeneral.NombrePersonal;

            sEstado = DtGeneral.EstadoConectado;
            sFarmacia = DtGeneral.FarmaciaConectada;


            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento);
            Lotes.ManejoLotes = OrigenManejoLotes.Ventas_PublicoGeneral;
            Lotes.MostrarLotesExistencia_0 = GnFarmacia.MostrarLotesSinExistencia; 


            IniciarToolBar(false, false, false);
            chkMostrarImpresionEnPantalla.Checked = false;

            txtTotal.Enabled = false; 
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
                    if (con.Abrir())
                    {
                        IniciarToolBar(); 
                        con.IniciarTransaccion();

                        if (GuardaVenta())
                        {
                            if (GuardaDetallesVenta())
                            {
                                if (GuardaVentasDet_Lotes())
                                {
                                    bContinua = AfectarExistencia(true, false); 
                                }
                            }
                        }

                        if (bContinua)
                        {
                            con.CompletarTransaccion();

                            // IMach  // Enlazar el folio de inventario 
                            RobotDispensador.Robot.TerminarSolicitud(sFolioMovtoInv);

                            IniciarToolBar(false, false, true);
                            txtFolio.Text = sFolioVenta; 
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP 
                            ImprimirInformacion();
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            txtFolio.Text = "*";
                            Error.GrabarError(leer, "btnGuardar_Click");
                            General.msjError("Ocurrió un error al guardar la información.");
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

        private bool GuardaVenta()
        {
            bool bRegresa = true; 
            string sSql = "", sCaja = GnFarmacia.NumCaja;
            int iOpcion = 1;

            // 
            sFolioVenta = txtFolio.Text; 

            CalcularTotales();

            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                    DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta, sFechaSistema, 
                    Fg.PonCeros(sCaja,2), txtIdPersonal.Text, sIdCliente, sIdSubCliente, 
                    sPrograma, sSubPrograma, fSubTotal, fIva, fTotal, (int)TipoDeVenta.Publico, iOpcion);


            sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasEnc " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}', @FechaSistema = '{4}', @IdCaja = '{5}', " +
                " @IdPersonal = '{6}', @IdCliente = '{7}', @IdSubCliente = '{8}', @IdPrograma = '{9}', @IdSubPrograma = '{10}', " +
                " @SubTotal = '{11}', @Iva = '{12}', @Total = '{13}', @TipoDeVenta = '{14}', @iOpcion = '{15}', @Descuento = '{16}' ",
                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4),
                sFolioVenta,
                sFechaSistema, Fg.PonCeros(sCaja, 2), DtGeneral.IdPersonal, 
                sIdCliente, sIdSubCliente, sPrograma, sSubPrograma,
                fSubTotal, fIva, fTotal, (int)TipoDeVenta.Publico, iOpcion, 0); 

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
                        // txtFolio.Text = sFolioVenta;
                    }
                }
            }

            return bRegresa; 

        }

        private void CalcularTotales()
        {            
            // double sSubTotal = 0, sIva = 0, sTotal = 0;

            fSubTotalIva_0 = 0;
            fSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            fIva = myGrid.TotalizarColumnaDou((int)Cols.ImporteIva);
            fTotal = myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal);

            txtTotal.Text = fTotal.ToString(sFormato);      

            //for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            //{
            //    sSubTotal = myGrid.GetValueDou(i, 7);
            //    fSubTotal = fSubTotal + sSubTotal;
            //    sIva = myGrid.GetValueDou(i, 8);
            //    fIva = fIva + sIva;
            //    sTotal = myGrid.GetValueDou(i, 9);
            //    fTotal = fTotal + sTotal;
            //}            
            
        }

        private bool GuardaDetallesVenta()
        {
            bool bRegresa = true;
            string sSql = "", sIdProducto = "", sCodigoEAN = "";
            int iRenglon = 0, iUnidadDeSalida = 0, iCant_Entregada = 0, iCant_Devuelta = 0, iCantVendida = 0, iOpcion = 0;
            double dCostoUnitario = 0, dPrecioUnitario = 0, dImpteIva = 0, dTasaIva = 0 , dPorcDescto = 0, dImpteDescto = 0;

            iOpcion = 1;

        //{
        //    Ninguna = 0,
        //    CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5,
        //    Precio = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10,
        //    EsIMach4 = 11, UltimoCosto = 12  
        //}

            for (int i = 1; i <= myGrid.Rows; i++)
            {
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
            string sSql = "", sIdProducto = "", sCodigoEAN = ""; // , sClaveLote = "";
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
                    if (sIdProducto != "" && L.Cantidad > 0 )
                    {
                        sSql = String.Format(" Set DateFormat YMD EXEC spp_Mtto_VentasDet_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                                                DtGeneral.EmpresaConectada, Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), L.IdSubFarmacia, sFolioVenta, Fg.PonCeros(sIdProducto, 8),
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
            int iOpcion = 0;
            string sSql = "", sMensaje = "", sCaja = "";
            string message = "¿Desea Cancelar la Venta ....?";
            //string caption = "Intermed";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult Eleccion;

            if ( lblCancelado.Visible )
            {
                General.msjUser("Este Folio ya ha sido cancelado no se puede cancelar");
            }
            else
            {
                //Eleccion = MessageBox.Show(this, message, caption, buttons,
                //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                //MessageBoxOptions.RightAlign);

                if (General.msjCancelar(message) == DialogResult.Yes)
                {
                    if (con.Abrir())
                    {
                        sFolioVenta = txtFolio.Text;
                        iOpcion = 2;
                        con.IniciarTransaccion();

                        sSql = String.Format("EXEC spp_Mtto_VentasEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}' ",
                            Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), sFolioVenta, Fg.PonCeros(sCaja, 2), txtIdPersonal.Text, sIdCliente, sIdSubCliente,
                            sPrograma, sSubPrograma, fSubTotal, fIva, fTotal, iOpcion);

                        if (leer.Exec(sSql))
                        {
                            if (leer.Leer())
                                sMensaje = String.Format("{0}", leer.Campo("Mensaje"));

                            con.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            con.DeshacerTransaccion();
                            Error.GrabarError(leer, "btnCancelar_Click");
                            General.msjError("Ocurrió un error al eliminar la información.");

                        }

                        con.Cerrar();
                    }
                    else
                    {
                        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                    }
                }
                else
                {
                    txtFolio.Focus();
                }
            }
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
            
            if (VtasImprimir.Imprimir(sFolioVenta, fTotal))
            {
                btnNuevo_Click(null, null);
            }
        }

        #endregion Botones

        #region Validaciones
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            string sSql = "", sFolio = "";
            bFolioGuardado = false;

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
                IniciarToolBar(true, false, false);
                myGrid.Limpiar(true);

                // Obtener datos de IMach 
                sFolioSolicitud = RobotDispensador.Robot.ObtenerFolioSolicitud(); 
            }
            else
            {
                sSql = string.Format("SELECT * FROM VentasEnc (nolock) WHERE FolioVenta= '{0}' AND IdEstado='{1}' AND IdFarmacia='{2}'  ", Fg.PonCeros(txtFolio.Text, 8), Fg.PonCeros(sEstado,2), Fg.PonCeros(sFarmacia,4));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtFolio_Validating");
                    General.msjError("Error al buscar Folio de Venta");
                }
                else
                {
                    if (leer.Leer())
                    {
                        if ((TipoDeVenta)leer.CampoInt("TipoDeVenta") != TipoDeVenta.Publico)
                        {
                            General.msjUser("El folio de venta capturado no es de venta de contado, verifique.");
                            txtFolio.Text = "";
                            txtFolio.Focus();
                        }
                        else
                        {
                            bFolioGuardado = true;
                            IniciarToolBar(false, false, true);
                            sFolio = leer.Campo("FolioVenta");
                            sFolioVenta = sFolio;
                            txtFolio.Text = sFolio;

                            dtpFechaDeSistema.Value = leer.CampoFecha("FechaSistema");
                            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");  

                            if (leer.Campo("Status") == "C")
                            {
                                lblCancelado.Visible = true;
                            }
                            CargaDetallesVenta();
                            CambiaEstado(false);
                        }
                    }
                    else
                    {
                        General.msjError("El Folio de Venta no Existe");
                        txtFolio.Text = "";
                        txtFolio.Focus();
                    }
                }
            }
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {      
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
         
        }

        private void txtPro_Validating(object sender, CancelEventArgs e)
        {            
         
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {            
          
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

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            // Recalcular los Importes 
            CalcularTotales();
            if (bRegresa && fTotal == 0 )
            {
                bRegresa = false;
                General.msjUser("El Importe Total de la Venta debe ser mayor a Cero, verifique."); 
            }

            if (bRegresa)
            {
                VerificarLotes = new clsVerificarSalidaLotes();
                bRegresa = VerificarLotes.VerificarExistenciasConError(Lotes);
            }

            if (bRegresa)
            {
                PagoEnCaja = new FrmPagoCaja();
                
                PagoEnCaja.SubTotalIva0 = fSubTotalIva_0;
                PagoEnCaja.SubTotal = fSubTotal;
                PagoEnCaja.Iva = fIva;
                PagoEnCaja.Total = fTotal;

                PagoEnCaja.TipoDeCambio = GnFarmacia.TipoDeCambioDollar;
                PagoEnCaja.ShowDialog();

                dPagoEfectivo = PagoEnCaja.PagoEfectivo;
                dPagoDolares = PagoEnCaja.PagoDolares;
                dPagoCheques = PagoEnCaja.PagoCheques;
                dCambios = PagoEnCaja.CambioClientes;

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
                General.msjUser("Debe capturar al menos un producto para la venta\n y/o capturar cantidades para al menos un lote, verifique.");

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

            myGrid.BloqueaColumna(true, 1);

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
            //string sSql = "";
            //leer2 = new clsLeer(ref con);

            //sSql = string.Format(" SELECT * FROM CatClientes (nolock) WHERE IdCliente='{0}' ", Fg.PonCeros(txtCte.Text, 4));

            //if (!leer2.Exec(sSql))
            //{
            //    Error.GrabarError(leer2, "LlenaCliente()");
            //    General.msjError("Error al buscar el Nombre de Cliente");
            //}
            //else
            //{
            //    if (leer2.Leer())
            //    {
            //        bRegresa = true;
            //        txtCte.Text = leer2.Campo("IdCliente");
            //        lblCte.Text = leer2.Campo("Nombre");
            //    }
            //}

            return bRegresa;
        }

        private bool LlenaSubCte()
        {
            bool bRegresa = false;
            //string sSql = "";
            //leer2 = new clsLeer(ref con);

            //sSql = string.Format(" SELECT * FROM CatSubClientes (nolock) WHERE IdCliente = '{0}' AND IdSubCliente='{1}' ", Fg.PonCeros(txtCte.Text, 4), Fg.PonCeros(txtSubCte.Text, 4));

            //if (!leer2.Exec(sSql))
            //{
            //    Error.GrabarError(leer2, "LlenaSubCte()");
            //    General.msjError("Error al buscar el Nombre de SubCliente");
            //}
            //else
            //{
            //    if (leer2.Leer())
            //    {
            //        bRegresa = true;
            //        txtSubCte.Text = leer2.Campo("IdSubCliente");
            //        lblSubCte.Text = leer2.Campo("Nombre");
            //    }
            //}

            return bRegresa;
        }

        private bool LlenaPrograma()
        {
            bool bRegresa = false;
            //string sSql = "";
            //leer2 = new clsLeer(ref con);

            //sSql = string.Format(" SELECT * FROM CatProgramas (nolock) WHERE IdPrograma='{0}' ", Fg.PonCeros(txtPro.Text, 3));

            //if (!leer2.Exec(sSql))
            //{
            //    Error.GrabarError(leer2, "LlenaPrograma()");
            //    General.msjError("Error al buscar el Nombre de Programa");
            //}
            //else
            //{
            //    if (leer2.Leer())
            //    {
            //        bRegresa = true;
            //        txtPro.Text = leer2.Campo("IdPrograma");
            //        lblPro.Text = leer2.Campo("Descripcion");
            //    }
            //}

            return bRegresa;
        }

        private bool LlenaSubPrograma()
        {
            bool bRegresa = false;
            //string sSql = "";
            //leer2 = new clsLeer(ref con);

            //sSql = string.Format(" SELECT * FROM CatSubProgramas (nolock) WHERE IdSubPrograma='{0}' AND IdPrograma='{1}' ", Fg.PonCeros(txtSubPro.Text, 2), Fg.PonCeros(txtPro.Text,3));

            //if (!leer2.Exec(sSql))
            //{
            //    Error.GrabarError(leer2, "LlenaSubPrograma()");
            //    General.msjError("Error al buscar el Nombre de SubPrograma");
            //}
            //else
            //{
            //    if (leer2.Leer())
            //    {
            //        bRegresa = true;
            //        txtSubPro.Text = leer2.Campo("IdSubPrograma");
            //        lblSubPro.Text = leer2.Campo("Descripcion");
            //    }
            //}

            return bRegresa;
        }

        private void CambiaEstado(bool bValor)
        {
            txtFolio.Enabled = bValor;
            //txtCte.Enabled = bValor;
            //txtPro.Enabled = bValor;
            //txtSubCte.Enabled = bValor;
            //txtSubPro.Enabled = bValor;
            //txtNumRec.Enabled = bValor;
            //txtfolderhab.Enabled = bValor;
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
            //            //txtCte.Text = leer.Campo("IdCliente");
            //            //LlenaCliente();
            //            //txtSubCte.Text = leer.Campo("IdSubCliente");
            //            //LlenaSubCte();
            //            //txtPro.Text = leer.Campo("IdPrograma");
            //            //LlenaPrograma();
            //            //txtSubPro.Text = leer.Campo("IdSubPrograma");
            //            //LlenaSubPrograma();

            //            //txtNumRec.Text = leer.Campo("FolioReceta");

            //            //txtfolderhab.Text = leer.Campo("FolioDerechoHabiencia");
            //            //lblNomPaciente.Text = leer.Campo("IdPaciente");

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
            //if (e.KeyCode == Keys.F1)         
            //{
            //    leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");

            //    if (leer2.Leer())
            //    {
            //        txtCte.Text = leer2.Campo("IdCliente");
            //        lblCte.Text = leer2.Campo("Nombre");
            //        txtSubCte.Focus();
            //    }
            //}
        }

        private void txtSubCte_KeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer2.DataSetClase = Ayuda.SubClientes("txtSubCte_KeyDown", txtCte.Text);

            //    if (leer2.Leer())
            //    {
            //        txtSubCte.Text = leer2.Campo("IdSubCliente");
            //        lblSubCte.Text = leer2.Campo("Nombre");
            //        txtPro.Focus();
            //    }
            //}
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {            
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer2.DataSetClase = Ayuda.Programas("txtPro_KeyDown");

            //    if (leer2.Leer())
            //    {
            //        txtPro.Text = leer2.Campo("IdPrograma");
            //        lblPro.Text = leer2.Campo("Descripcion");
            //        txtSubPro.Focus();
            //    }
            //}
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    string sIdPrograma = "";

            //    sIdPrograma = txtPro.Text;
            //    leer2.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", sIdPrograma);

            //    if (leer2.Leer())
            //    {
            //        txtSubPro.Text = leer2.Campo("IdSubPrograma");
            //        lblSubPro.Text = leer2.Campo("Descripcion");
            //        txtNumRec.Focus();
            //    }
            //}
        }

        #endregion Eventos    

        #region Grid
        private void grdProductos_EditModeOff_1(object sender, EventArgs e)
        {
            
            switch (myGrid.ActiveCol)
            {
                case 1:
                    {
                        ObtenerDatosProducto();
                    }
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

        private void grdProductos_KeyDown_1(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGrid.ActiveCol;
            switch (ColActiva)
            {
                case Cols.Precio:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                case Cols.ImporteIva: 

                        if (e.KeyCode == Keys.F1)
                        {
                            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);
                            leer.DataSetClase = Ayuda.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, "grdProductos_KeyDown_1");
                            if (leer.Leer())
                            {
                                myGrid.SetValue(myGrid.ActiveRow, 1, leer.Campo("CodigoEAN"));
                                ObtenerDatosProducto();
                                //CargarDatosProducto();
                            }
                        }

                        if (e.KeyCode == Keys.Delete)
                            removerLotes();

                        //else
                        //{
                        //    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, sValorGrid); 
                        //}

                        // Administracion de Mach4 
                        if (e.KeyCode == Keys.F10)
                        {
                            if (RobotDispensador.Robot.EsClienteInterface && myGrid.GetValueBool(myGrid.ActiveRow, (int)Cols.EsIMach4))
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
                    break;
            }
        }

        private void ObtenerDatosProducto()
        {
            string sCodigo = "", sSql = "";
            bool bEsEAN_Unico = false; 

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);
            if (EAN.EsValido(sCodigo))
            {
                if (!GnFarmacia.ValidarSeleccionCodigoEAN(sCodigo, ref sCodigoEAN_Seleccionado, true, ref bEsEAN_Unico))
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
                        (int)TipoDeVenta.Publico, sIdCliente, sIdSubCliente,
                        Fg.PonCeros(sCodigo, 13), sCodigo.Trim(),
                        Fg.PonCeros(sEstado, 2), Fg.PonCeros(sFarmacia, 4), 0, Convert.ToInt32(RobotDispensador.Robot.EsClienteInterface), "", 
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
                                General.msjUser("El Producto " + leer.Campo("Descripcion") + " no esta registrado en la Farmacia, verifique.");
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
                    }
                    else
                    {
                        General.msjUser("El artículo ya se encuentra capturado en otro renglon.");
                        myGrid.SetValue(myGrid.ActiveRow, 1, "");
                        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                        myGrid.EnviarARepetido(); 
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
                    Lotes.EsEntrada = false;// para las ventas
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false;
                    Lotes.ModificarCantidades = true;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;

                    // Administracion de Mach4
                    if (RobotDispensador.Robot.EsClienteInterface && myGrid.GetValueBool(iRow, (int)Cols.EsIMach4))
                    {
                        if (RobotDispensador.Robot.RequisicionRegistrada)
                        {
                            Lotes.Show();
                        }
                    }
                    else
                    {
                        Lotes.Show();
                    }

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Precio);

                    CalcularTotales(); 
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes
    }
}
