﻿using System;
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
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Farmacia.CambiosFisicosProveedores
{
    public partial class FrmCambioFisicoProductosProveedores : FrmBaseExt 
    {
        private enum Cols
        {
            Ninguna = 0, CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            Costo = 6, Importe = 7, ImporteIva = 8, ImporteTotal = 9, TipoCaptura = 10
        }

        public enum TipoCambio
        {
            Entrada = 1, Salida = 2 
        }

        #region Variables Generales a esta Clase
        clsConexionSQL myConn = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsGrid myGridSalida, myGridEntrada;
        clsCodigoEAN EAN = new clsCodigoEAN();
        clsLotes LotesEntrada;
        clsLotes LotesSalida;
        clsSKU SKU_Salida;
        clsSKU SKU_Entrada;
        clsSKU SKU; 

        clsConsultas myQuery;
        clsAyudas myAyuda;
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente myConexionWeb; 
        bool bEstaCancelado = false;
        bool bMovtoAplicado = false;
        bool bInicioPantalla = true;
        bool bDatosGuardados = false; 

        Cols ColActiva = Cols.Ninguna;

        string sFolioMovto = "";
        string sFormato = "#,###,###,##0.###0";       
        string sValorGrid = "";
        // string sMsjEanInvalido = "";

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sIdTipoMovtoInv = "";
        string sTipoES = "";        
        
        string sMovtoSalida = "SCP";
        string sMovtoEntrada = "ECP"; 
        double dCosto = 0D;
        double dTasaIva = 0D;
        // double dImporte = 0D;
        string sFolioCambioSalida = "";
        string sFolioCambioEntrada = "";
        string sIdProductoEntrada = "";
        string sIdProductoSalida = "";
        string sCodigoEANSalida = "";
        string sCodigoEANEntrada;        

        int iCantidadSalida = 0;
        int iCantidadEntrada = 0;
        string sMsjNoEncontrado = "";

        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;
        
        #endregion variables

        public FrmCambioFisicoProductosProveedores()
        {
            InitializeComponent();

            CargarPantalla();  
        }

        private void LimpiarPantalla(bool bConfirmar)
        {
            bool bExito = true;
            bDatosGuardados = false; 

            if (bConfirmar)
            {
                if (General.msjConfirmar("¿ Desea limpiar la información en pantalla, se perdera lo que este capturado. ?") == DialogResult.No)
                {
                    bExito = false;
                }
            }

            if ((bExito))
            {
                SKU_Salida = new clsSKU(sMovtoSalida);
                SKU_Entrada = new clsSKU(sMovtoEntrada);
                SKU = new clsSKU(); 


                LotesEntrada = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);
                LotesSalida = new clsLotes(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnFarmacia.MesesCaducaMedicamento);

                LotesSalida.ManejoLotes = OrigenManejoLotes.Inventarios;
                LotesEntrada.ManejoLotes = OrigenManejoLotes.Inventarios;


                IniciarToolBar(true, false, false, false);
                myGridSalida.Limpiar(false);
                myGridSalida.BloqueaColumna(false, (int)Cols.Costo);
                myGridEntrada.Limpiar(false);
                myGridEntrada.BloqueaColumna(false, (int)Cols.Costo);

                bEstaCancelado = false;
                bMovtoAplicado = false;

                myGridSalida.Limpiar(false);
                myGridEntrada.Limpiar(false);
                Fg.IniciaControles();

                txtIdPersonal.Text = DtGeneral.IdPersonal.ToString();
                lblNombrePersonal.Text = DtGeneral.NombrePersonal.ToString();
                txtIdPersonal.Enabled = false;
                dtpFechaRegistro.Enabled = false;
                txtSubTotal.Enabled = false;
                txtIva.Enabled = false;
                txtTotal.Enabled = false;               

                LotesSalida.PermitirSalidaCaducados = true;
                LotesEntrada.PermitirSalidaCaducados = false;
                tbcMovimientos.SelectTab(tpSalida);
                tbcMovimientos.Focus();

                if (bInicioPantalla)
                {
                    bInicioPantalla = false;
                    SendKeys.Send("{TAB}");
                }

                txtFolio.Focus();
                txtFolio.Focus();
            }
        }

        private void FrmCambioFisicoProductosProveedores_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(false);
        }       

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    myLeer.DataSetClase = myAyuda.Proveedores("txtIdProveedor_KeyDown()");
                    if (!myLeer.Leer())
                    {
                        General.msjError("Ocurrió Un Error al buscar la Información.");
                    }
                    else 
                    {
                        txtIdProveedor.Text = myLeer.Campo("IdProveedor");
                        lblNombreProveedor.Text = myLeer.Campo("Nombre");
                        txtIdProveedor.Enabled = false;
                    }
                    break;

                case Keys.Enter:                    
                    txtObservaciones.Focus();                    
                    break;

                default:
                {
                    break;
                }
            }


        }
        
        private void grdProductosSalida_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGridSalida.ActiveCol;
            int iRow = myGridSalida.ActiveRow;

            switch (ColActiva)
            {
                case Cols.Costo:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    if (chkAplicarInv.Enabled)
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.F1:
                                sValorGrid = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN);
                                myLeer.DataSetClase = myAyuda.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, "grdProductosSalida_KeyDown");

                                if (myLeer.Leer())
                                {
                                    CargarDatosProducto("grdProductosSalida");
                                    PasarDatosProducto("grdProductosSalida");
                                }
                                break;

                            case Keys.F7:
                                RevisarLotesEntradaSalida("sGridProductosSalida", "", iRow);
                                break;
                           
                            case Keys.Delete:
                                RemoverLotes("grdProductosSalida");
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void grdProductosEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            ColActiva = (Cols)myGridEntrada.ActiveCol;
            int iRow = myGridEntrada.ActiveRow;

            switch (ColActiva)
            {
                case Cols.Costo:
                    break;

                case Cols.CodEAN:
                case Cols.Descripcion:
                case Cols.Cantidad:
                case Cols.Importe:
                    if (chkAplicarInv.Enabled)
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.F1:
                                sValorGrid = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
                                myLeer.DataSetClase = myAyuda.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, "grdProductosEntrada_KeyDown");
                                if (myLeer.Leer())
                                {
                                    CargarDatosProducto("grdProductosEntrada");
                                    PasarDatosProducto("grdProductosEntrada");
                                }
                                break;

                            case Keys.F7:
                                RevisarLotesEntradaSalida("", "grdProductosEntrada", iRow);
                                break;

                            case Keys.Delete:
                                RemoverLotes("grdProductosSalida");
                                RemoverLotes("grdProductosEntrada");                                
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void IniciarToolBar()
        {
            IniciarToolBar(true, false, false, false);
        }

        private void IniciarToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private void CargarPantalla()
        {
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            myConexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            myConexionWeb.Url = General.Url;

            myGridSalida = new clsGrid(ref grdProductosSalida, this);
            myGridSalida.EstiloGrid(eModoGrid.ModoRow);
            myGridSalida.BackColorColsBlk = Color.White;
            grdProductosSalida.EditModeReplace = true;
            myGridSalida.AjustarAnchoColumnasAutomatico = true; 

            myGridEntrada = new clsGrid(ref grdProductosEntrada, this);
            myGridEntrada.EstiloGrid(eModoGrid.ModoRow);
            myGridEntrada.BackColorColsBlk = Color.White;
            grdProductosEntrada.EditModeReplace = true;
            myGridEntrada.AjustarAnchoColumnasAutomatico = true;

            myLeer = new clsLeer(ref myConn);
            myQuery = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            myAyuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref myConn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bDatosGuardados = false; 
            lblCancelado.Visible = false;
            bEstaCancelado = false;
            myGridSalida.Limpiar(false);
            myGridEntrada.Limpiar(false);

            if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, true, false, false);
                myGridSalida.Limpiar(true);
                myGridEntrada.Limpiar(true);
                myGridSalida.BloqueaColumna(false, (int)Cols.CodEAN);
                myGridEntrada.BloqueaColumna(false, (int)Cols.CodEAN);
            }
            else
            {
                myGridSalida.Limpiar(true);
                myGridEntrada.Limpiar(true);
                myLeer.DataSetClase = myQuery.CambiosProveedorEncabezado(sEmpresa, sEstado, sFarmacia, sMovtoSalida, txtFolio.Text, "txtFolio_Validating");  

                if (myLeer.Leer())
                {
                    bDatosGuardados = true; 
                    CargarDatosEncabezado();
                    sFolioCambioSalida = sMovtoSalida + Fg.PonCeros(txtFolio.Text, 8);
                    sFolioCambioEntrada = sMovtoEntrada + Fg.PonCeros(txtFolio.Text, 8); 

                    e.Cancel = !CargarDatosDetalle(txtFolio.Text);

                    IniciarToolBar(true, false, false, true);

                    myGridSalida.BloqueaColumna(true, (int)Cols.CodEAN);
                    myGridEntrada.BloqueaColumna(true, (int)Cols.CodEAN);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void CargarDatosEncabezado()
        {
            txtFolio.Enabled = false;
            txtFolio.Text = myLeer.Campo("FolioCambio").Substring(3);

            txtIdProveedor.Enabled = false;            
            txtIdProveedor.Text = myLeer.Campo("IdProveedor"); 
            lblNombreProveedor.Text = myLeer.Campo("NombreProveedor");

            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            txtSubTotal.Text = myLeer.Campo("SubTotal");
            txtIva.Text = myLeer.Campo("Iva");
            txtTotal.Text = myLeer.Campo("Total"); 
            txtIdPersonal.Text = myLeer.Campo("IdPersonal");
            lblNombrePersonal.Text = myLeer.Campo("NombrePersonal");

            // txtObservaciones.Enabled = false; 
            
        }

        private bool CargarDatosDetalle(string Folio)
        {
            bool bRegresa = true;

            myLeer.DataSetClase = myQuery.CambiosProveedorDetalle(sEmpresa, sEstado, sFarmacia, sMovtoSalida, Folio, "CargarDatosDetalle"); 
            if (!myLeer.Leer())
            {
                bRegresa = false;
                General.msjError("Ocurrió un error al grabar la información del Detalle del Folio");
            }
            else
            {                
                myGridSalida.LlenarGrid(myLeer.DataSetClase);
                CargarDatosDetalleLotes(1);               
            }

            myLeer.DataSetClase = myQuery.CambiosProveedorDetalle(sEmpresa, sEstado, sFarmacia, sMovtoEntrada, Folio, "CargarDatosDetalle"); 
            if (!myLeer.Leer())
            {
                bRegresa = false;
                General.msjError("Ocurrió un error al grabar la información del Detalle del Folio");
            }
            else
            {
                myGridEntrada.LlenarGrid(myLeer.DataSetClase);              
                CargarDatosDetalleLotes(2);
            }

            if ((myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN) != myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN)))
            {
                myGridSalida.SetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN, 0);
            }

            return bRegresa;
        }
                
        private bool CargarDatosDetalleLotes(int Tipo)
        {
            bool bRegresa = true;

            if ( Tipo == 1)
            {
                myLeer.DataSetClase = myQuery.CambiosProveedorDetallesLotes(sEmpresa, sEstado, sFarmacia, sMovtoSalida, txtFolio.Text, "CargarDetallesLotes");
                myLeer.Leer();
                LotesSalida.AddLotes(myLeer.DataSetClase);
            }
            else
            {
                myLeer.DataSetClase = myQuery.CambiosProveedorDetallesLotes(sEmpresa, sEstado, sFarmacia, sMovtoEntrada, txtFolio.Text, "CargarDetallesLotes");
                myLeer.Leer();
                LotesEntrada.AddLotes(myLeer.DataSetClase);                
                ////LotesEntrada.ModificarCantidades = false;
                ////LotesEntrada.PermitirLotesNuevosConsignacion = false;
                ////LotesEntrada.Lotes(myLeer.Campo("IdProducto"), myLeer.Campo("CodigoEAN"));
            }

            return bRegresa;
        }

        private bool CargarDatosProducto(string sGridMovimiento)
        {
            bool bRegresa = true;
            int iRow = 0; 
            int iColEAN = 0; 
            string sCodEAN = myLeer.Campo("CodigoEAN");
            sIdProductoSalida = myLeer.Campo("IdProducto");
            dCosto = myLeer.CampoDouble("CostoPromedio");
            dTasaIva = myLeer.CampoDouble("TasaIva");
            string sDescripcion = myLeer.Campo("Descripcion");
            // int iCantidad = 0;
            
            if (sValorGrid != sCodEAN)
            {
                if (sGridMovimiento == "grdProductosSalida")
                {
                    if (!myGridSalida.BuscaRepetido(sCodEAN, iRow, iColEAN))
                    {
                        iRow = myGridSalida.ActiveRow;
                        myGridSalida.SetValue(iRow, (int)Cols.CodEAN, sCodEAN);
                        myGridSalida.SetValue(iRow, (int)Cols.Descripcion, sDescripcion);
                        myGridSalida.SetValue(iRow, (int)Cols.TasaIva, dTasaIva);
                        myGridSalida.SetValue(iRow, (int)Cols.Codigo, sIdProductoSalida);
                        myGridSalida.SetValue(iRow, (int)Cols.Cantidad, myLeer.CampoInt("Cantidad"));
                        myGridSalida.SetValue(iRow, (int)Cols.Costo, dCosto);                   
                        myGridSalida.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);

                        CargarLotesCodigoEAN(sGridMovimiento);

                        iCantidadSalida = myGridSalida.GetValueInt(iRow, (int)Cols.Cantidad);
                        sIdProductoSalida = myGridSalida.GetValue(iRow, (int)Cols.CodEAN);
                        myGridSalida.SetValue(iRow, (int)Cols.TipoCaptura, LotesSalida.TipoCaptura);
                    }
                    else
                    {
                        General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                        myGridSalida.LimpiarRenglon(iRow);
                        myGridSalida.SetActiveCell(iRow, iColEAN);
                        myGridSalida.EnviarARepetido();
                        bRegresa = false;
                    }
                }

                if (sGridMovimiento == "grdProductosEntrada")
                {
                    if (!myGridEntrada.BuscaRepetido(sCodEAN, iRow, iColEAN))
                    {
                        iRow = myGridEntrada.ActiveRow;
                        myGridEntrada.SetValue(iRow, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
                        myGridEntrada.SetValue(iRow, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                        myGridEntrada.SetValue(iRow, (int)Cols.TasaIva, myLeer.CampoDouble("TasaIva"));
                        myGridEntrada.SetValue(iRow, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
                        myGridEntrada.SetValue(iRow, (int)Cols.Cantidad, myLeer.CampoInt("Cantidad"));
                        myGridEntrada.SetValue(iRow, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));                        
                        myGridEntrada.BloqueaCelda(true, Color.WhiteSmoke, iRow, (int)Cols.CodEAN);

                        CargarLotesCodigoEAN(sGridMovimiento);

                        iCantidadEntrada = myGridEntrada.GetValueInt(iRow, (int)Cols.Cantidad);
                        sIdProductoEntrada = LotesEntrada.Codigo;
                        myGridEntrada.SetValue(iRow, (int)Cols.TipoCaptura, LotesEntrada.TipoCaptura);
                    }
                    else
                    {
                        General.msjUser("El producto ya fue capturado en otro renglon, verifique.");
                        myGridEntrada.LimpiarRenglon(iRow);
                        myGridEntrada.SetActiveCell(iRow, iColEAN);
                        myGridEntrada.EnviarARepetido();
                        bRegresa = false;
                    }
                }
            }
            else
            {
                string sGrid = "";                
                if (sGridMovimiento == "grdProductosSalida")
                {
                    sGrid =  "grdProductosSalida";
                    iRow = myGridSalida.ActiveRow;
                }
                else
                {
                    sGrid = "grdProductosEntrada";
                    iRow = myGridEntrada.ActiveRow;
                }
                MostrarOcultarLotes(sGrid, iRow);
            }         

            return bRegresa;
        }

        private void CargarLotesCodigoEAN(string sGridMovimiento)
        {
            int iRow = 0;  
            string sCodigo = "";
            string sCodEAN = "";

            if (sGridMovimiento == "grdProductosSalida")
            {               
                iRow = myGridSalida.ActiveRow;
                sCodigo = myGridSalida.GetValue(iRow, (int)Cols.Codigo);
                sCodEAN = myGridSalida.GetValue(iRow, (int)Cols.CodEAN);

                myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, false, "CargarLotesCodigoEAN()");
                if (myQuery.Ejecuto)
                {
                    myLeer.Leer();

                    LotesSalida.AddLotes(myLeer.DataSetClase);
                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, false, "CargarLotesCodigoEAN()");
                        
                        LotesSalida.AddLotesUbicaciones(myLeer.DataSetClase);
                        LotesEntrada.AddLotesUbicaciones(myLeer.DataSetClase);
                    }


                    myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
                    LotesEntrada.AddLotes(myLeer.DataSetClase);
                    if(myQuery.Ejecuto)
                    {
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
                            LotesEntrada.AddLotesUbicaciones(myLeer.DataSetClase);
                        }
                    }




                    MostrarOcultarLotes(sGridMovimiento, iRow);
                    
                    myGridSalida.SetValue(iRow, (int)Cols.Cantidad, LotesSalida.Cantidad);
                    myGridSalida.SetValue(iRow, (int)Cols.TipoCaptura, LotesSalida.TipoCaptura);
                    myGridEntrada.SetValue(iRow, (int)Cols.Cantidad, LotesEntrada.Cantidad);
                    myGridEntrada.SetValue(iRow, (int)Cols.TipoCaptura, LotesEntrada.TipoCaptura);                    
                }              
            }

            if (sGridMovimiento == "grdProductosEntrada")
            {
                iRow = myGridEntrada.ActiveRow;
                sCodigo = myGridEntrada.GetValue(iRow, (int)Cols.Codigo);
                sCodEAN = myGridEntrada.GetValue(iRow, (int)Cols.CodEAN);

                myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
                if (myQuery.Ejecuto)
                {
                    myLeer.Leer();

                    LotesEntrada.AddLotes(myLeer.DataSetClase);
                    if (GnFarmacia.ManejaUbicaciones)
                    {
                        myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, true, "CargarLotesCodigoEAN()");
                        LotesEntrada.AddLotesUbicaciones(myLeer.DataSetClase); 
                    }


                    myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, false, "CargarLotesCodigoEAN()");
                    LotesSalida.AddLotes(myLeer.DataSetClase);
                    if(myQuery.Ejecuto)
                    {
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            myLeer.DataSetClase = myQuery.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, false, "CargarLotesCodigoEAN()");
                            LotesSalida.AddLotesUbicaciones(myLeer.DataSetClase);
                        }
                    }


                    MostrarOcultarLotes(sGridMovimiento, iRow);

                    myGridEntrada.SetValue(iRow, (int)Cols.Cantidad, LotesEntrada.Cantidad);
                    myGridEntrada.SetValue(iRow, (int)Cols.TipoCaptura, LotesEntrada.TipoCaptura);                    
                }
            }                        
        }

        private void MostrarOcultarLotes(string sGridMovimiento, int iRow)
        {               
            if ((this.ActiveControl.Name.ToUpper() == grdProductosSalida.Name.ToUpper()) |
                (this.ActiveControl.Name.ToUpper() == grdProductosEntrada.Name.ToUpper()))
            {
                if (sGridMovimiento == "grdProductosSalida")
                {
                    LotesSalida.ModificarCantidades = !bDatosGuardados;
                    LotesSalida.PermitirLotesNuevosConsignacion = false; 

                    LotesSalida.Codigo = myGridSalida.GetValue(iRow, (int)Cols.Codigo);
                    LotesSalida.CodigoEAN = myGridSalida.GetValue(iRow, (int)Cols.CodEAN);
                    LotesSalida.Descripcion = myGridSalida.GetValue(iRow, (int)Cols.Descripcion);
                    LotesSalida.EsEntrada = false;
                    LotesSalida.TipoCaptura = 1;
                    LotesSalida.PermitirLotesNuevosConsignacion = false;
                    LotesSalida.CapturarLotes = chkAplicarInv.Enabled;
                    LotesSalida.ModificarCantidades = !bDatosGuardados;
                    LotesSalida.Encabezados = EncabezadosManejoLotes.Default; 
                    LotesSalida.Show();

                    myGridSalida.SetValue(iRow, (int)Cols.Cantidad, LotesSalida.Cantidad);
                    myGridSalida.SetValue(iRow, (int)Cols.TipoCaptura, LotesSalida.TipoCaptura);
                    myGridSalida.SetActiveCell(iRow, (int)Cols.Costo);

                    LotesEntrada.EsEntrada = true;
                    LotesEntrada.TipoCaptura = 1;                 
                }            

                if (sGridMovimiento == "grdProductosEntrada")
                {
                    LotesEntrada.CapturarLotes = true;
                    LotesEntrada.ModificarCantidades = !bDatosGuardados;
                    LotesEntrada.EsEntrada = true;

                    LotesEntrada.Codigo = myGridEntrada.GetValue(iRow, (int)Cols.Codigo);
                    LotesEntrada.CodigoEAN = myGridEntrada.GetValue(iRow, (int)Cols.CodEAN);
                    LotesEntrada.Descripcion = myGridEntrada.GetValue(iRow, (int)Cols.Descripcion);                   
                    LotesEntrada.TipoCaptura = 1;
                    LotesEntrada.PermitirLotesNuevosConsignacion = true;
                    LotesEntrada.EsConsignacion = false;
                    LotesEntrada.EsEntrada = true; 
                    LotesEntrada.Encabezados = EncabezadosManejoLotes.Default;
                    LotesEntrada.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    LotesEntrada.Show();

                    myGridEntrada.SetValue(iRow, (int)Cols.Cantidad, LotesEntrada.Cantidad);
                    myGridEntrada.SetValue(iRow, (int)Cols.TipoCaptura, LotesEntrada.TipoCaptura);
                    myGridEntrada.SetActiveCell(iRow, (int)Cols.Costo);

                    LotesSalida.Cantidad = LotesEntrada.Cantidad;

                    LotesSalida.EsEntrada = false;
                }
            }
            Totalizar(sGridMovimiento);
        }

        private void Totalizar(string sGridMovimiento)
        {
            double dSubTotalSalida = myGridSalida.TotalizarColumnaDou((int)Cols.Importe);
            double dSubTotalEntrada = myGridEntrada.TotalizarColumnaDou((int)Cols.Importe);
            double dIvaSalida = myGridSalida.TotalizarColumnaDou((int)Cols.ImporteIva);
            double dIvaEntrada = myGridEntrada.TotalizarColumnaDou((int)Cols.ImporteIva);
            double dTotalSalida = myGridSalida.TotalizarColumnaDou((int)Cols.ImporteTotal);
            double dTotalEntrada = myGridEntrada.TotalizarColumnaDou((int)Cols.ImporteTotal);
            double dSubTotalFinal = dSubTotalSalida + dSubTotalEntrada;
            double dIvaFinal = dIvaSalida + dIvaEntrada;
            double dTotalFinal = dTotalSalida + dTotalEntrada;

            txtSubTotal.Text = dSubTotalFinal.ToString(sFormato);
            txtIva.Text = dIvaFinal.ToString(sFormato);
            txtTotal.Text = dTotalFinal.ToString(sFormato);
        }

        private void PasarDatosProducto(string sGridMovimiento)
        {
            int iRow = 0;
            int iCantidad =0;
            string sCodigo = "";
            double dCosto = 0D;
            string sDescripcion = "";
            double dImporte = 0D;
            double dTasaIva = 0D;
            double dIva = 0D;
            double dTotal = 0D;            
            int iTipoCaptura = 0;           

            if (sGridMovimiento == "grdProductosSalida")
            {
                iRow = myGridSalida.ActiveRow;
                iCantidad = myGridSalida.GetValueInt(iRow, (int)Cols.Cantidad);
                sCodigoEANSalida = myGridSalida.GetValue(iRow, (int)Cols.CodEAN);
                sCodigo = myGridSalida.GetValue(iRow, (int)Cols.Codigo);
                dCosto = myGridSalida.GetValueDou(iRow, (int)Cols.Costo);
                sDescripcion = myGridSalida.GetValue(iRow, (int)Cols.Descripcion);
                dImporte = myGridSalida.GetValueDou(iRow, (int)Cols.Importe);
                dIva = myGridSalida.GetValueDou(iRow, (int)Cols.ImporteIva);
                dTotal = myGridSalida.GetValueDou(iRow, (int)Cols.ImporteTotal);
                dTasaIva = myGridSalida.GetValueDou(iRow, (int)Cols.TasaIva);
                iTipoCaptura = myGridSalida.GetValueInt(iRow, (int)Cols.TipoCaptura);
                sIdProductoSalida = sCodigo;
                iCantidadSalida = iCantidad;

                myGridEntrada.SetValue(iRow, (int)Cols.Cantidad, iCantidad);
                myGridEntrada.SetValue(iRow, (int)Cols.CodEAN, sCodigoEANSalida);
                myGridEntrada.SetValue(iRow, (int)Cols.Codigo, sCodigo);
                myGridEntrada.SetValue(iRow, (int)Cols.Costo, dCosto);
                myGridEntrada.SetValue(iRow, (int)Cols.Descripcion, sDescripcion);
                myGridEntrada.SetValue(iRow, (int)Cols.Importe, dImporte);
                myGridEntrada.SetValue(iRow, (int)Cols.ImporteIva, dIva);
                myGridEntrada.SetValue(iRow, (int)Cols.ImporteTotal, dTotal);
                myGridEntrada.SetValue(iRow, (int)Cols.TasaIva, dTasaIva);
                myGridEntrada.SetValue(iRow, (int)Cols.TipoCaptura, iTipoCaptura);    
            }
            else
            {
                iRow = myGridEntrada.ActiveRow;
                iCantidad = myGridEntrada.GetValueInt(iRow, (int)Cols.Cantidad);
                sCodigoEANEntrada = myGridEntrada.GetValue(iRow, (int)Cols.CodEAN);
                sCodigo = myGridEntrada.GetValue(iRow, (int)Cols.Codigo);
                dCosto = myGridEntrada.GetValueDou(iRow, (int)Cols.Costo);
                sDescripcion = myGridEntrada.GetValue(iRow, (int)Cols.Descripcion);
                dImporte = myGridEntrada.GetValueDou(iRow, (int)Cols.Importe);
                dIva = myGridEntrada.GetValueDou(iRow, (int)Cols.ImporteIva);
                dTotal = myGridEntrada.GetValueDou(iRow, (int)Cols.ImporteTotal);
                dTasaIva = myGridEntrada.GetValueDou(iRow, (int)Cols.TasaIva);
                iTipoCaptura = myGridEntrada.GetValueInt(iRow, (int)Cols.TipoCaptura);
                sIdProductoEntrada = sCodigo;

                EvaluarCantidadesLotes();
            }           
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bExito = false;
            bool bBtnNuevo = btnNuevo.Enabled;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled;

            if (ValidarDatos())
            {
                if (!myConn.Abrir())
                {
                    Error.LogError(myConn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    IniciarToolBar();
                    myConn.IniciarTransaccion();

                    if (GrabarEncabezado())
                    {
                        bExito = AfectarExistencia();
                    }

                    if (!bExito)
                    {
                        myConn.DeshacerTransaccion();
                        txtFolio.Text = "*";  // sFolioMovto != "" ? sFolioMovto.Substring(3) : "*";
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al grabar la información del Cambio Físico a Proveedor.");
                        IniciarToolBar(bBtnNuevo, bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }
                    else
                    {
                        myConn.CompletarTransaccion();
                        IniciarToolBar(true, false, false, false);
                        
                        General.msjUser("Información guardada satisfactoriamente con el folio " + sFolioMovto);
                        myGridEntrada.BloqueaColumna(true, (int)Cols.CodEAN);

                        ImprimirMovimiento();
                    }
                    myConn.Cerrar();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirMovimiento(); 
            ////if (txtFolio.Text != "*") 
            ////{
            ////    ImprimirMovimiento(true);
            ////}
            ////else
            ////{
            ////    ImprimirMovimiento(false);
            ////}
        }

        private void RemoverLotes(string sGridMovimiento)
        {
            int iRow =  0; 
            if (chkAplicarInv.Enabled)
            {
                if (!bEstaCancelado)
                {
                    if (sGridMovimiento == "grdProductosSalida")
                    {
                        iRow = myGridSalida.ActiveRow;
                        LotesSalida.RemoveLotes(myGridSalida.GetValue(iRow, (int)Cols.Codigo), myGridSalida.GetValue(iRow, (int)Cols.CodEAN));
                        myGridSalida.DeleteRow(iRow);

                        if (myGridSalida.Rows == 0)
                        {
                            myGridSalida.Limpiar(true); 
                        }
                    }
                    else
                    {
                        iRow = myGridEntrada.ActiveRow;
                        LotesEntrada.RemoveLotes(myGridEntrada.GetValue(iRow, (int)Cols.Codigo), myGridEntrada.GetValue(iRow, (int)Cols.CodEAN));
                        myGridEntrada.DeleteRow(iRow);

                        if (myGridEntrada.Rows == 0)
                        {
                            myGridEntrada.Limpiar(true);
                        }
                    }

                    try
                    {   
                        Totalizar(sGridMovimiento);
                    }
                    catch (Exception ex)
                    {
                        ex.Source = ex.Source; 
                    }

                    if (myGridSalida.Rows == 0 & myGridEntrada.Rows == 0)
                    {
                        myGridSalida.Limpiar(true);
                        myGridEntrada.Limpiar(true);
                    }                                                                     
                }
            }
        }

        private void grdProductosSalida_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bMovtoAplicado)
            {
                if (!bEstaCancelado)
                {
                    if ((myGridSalida.ActiveRow == myGridSalida.Rows) && e.AdvanceNext)
                    {
                        if (myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN) != "")
                        {
                            myGridSalida.Rows = myGridSalida.Rows + 1;
                            myGridSalida.ActiveRow = myGridSalida.Rows;
                            myGridSalida.SetActiveCell(myGridSalida.Rows, 1);

                            myGridEntrada.Rows = myGridEntrada.Rows + 1;
                            myGridEntrada.ActiveRow = myGridEntrada.Rows;
                            myGridEntrada.SetActiveCell(myGridEntrada.Rows, 1);
                        }
                    }
                }
            }
        }

        private void grdProductosEntrada_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bMovtoAplicado)
            {
                if (!bEstaCancelado)
                {
                    if ((myGridEntrada.ActiveRow == myGridEntrada.Rows) && e.AdvanceNext)
                    {
                        if (myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN) != "" &&
                            myGridEntrada.GetValueInt(myGridEntrada.ActiveRow, (int)Cols.Cantidad) > 0)
                        {
                            myGridEntrada.Rows = myGridEntrada.Rows + 1;
                            myGridEntrada.ActiveRow = myGridEntrada.Rows;
                            myGridEntrada.SetActiveCell(myGridEntrada.Rows, 1);

                            myGridSalida.Rows = myGridSalida.Rows + 1;
                            myGridSalida.ActiveRow = myGridSalida.Rows;
                            myGridSalida.SetActiveCell(myGridSalida.Rows, 1);
                        }
                    }
                }
            }
        }       

        private void grdProductosSalida_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGridSalida.ActiveCol;

            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            myLeer.DataSetClase = myQuery.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, sValor, "grdProductosSalida_EditModeOff");
                            if (myLeer.Leer())
                            {
                                CargarDatosProducto("grdProductosSalida");
                                PasarDatosProducto("grdProductosSalida");
                            }
                            else
                            {
                                myGridSalida.LimpiarRenglon(myGridSalida.ActiveRow);
                                myGridSalida.ActiveCelda(myGridSalida.ActiveRow, (int)Cols.CodEAN);
                            }
                        }
                        else
                        {
                            myGridSalida.LimpiarRenglon(myGridSalida.ActiveRow);
                            myGridSalida.ActiveCelda(myGridSalida.ActiveRow, (int)Cols.CodEAN);
                            SendKeys.Send("");
                        }
                    }
                    else
                    {
                        myGridSalida.LimpiarRenglon(myGridSalida.ActiveRow);
                    }
                    break;
            }

            Totalizar("grdProductosSalida");
        }

        private void grdProductosEntrada_EditModeOff(object sender, EventArgs e)
        {
            ColActiva = (Cols)myGridEntrada.ActiveCol;
            switch (ColActiva)
            {
                case Cols.CodEAN:
                    string sValor = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
                    if (sValor != "")
                    {
                        if (EAN.EsValido(sValor))
                        {
                            myLeer.DataSetClase = myQuery.ProductosEstado(DtGeneral.EmpresaConectada, sEstado, sValor, "grdProductosEntrada_EditModeOff");
                            if (myLeer.Leer())
                            {
                                CargarDatosProducto("grdProductosEntrada");
                            }
                            else
                            {
                                myGridEntrada.LimpiarRenglon(myGridEntrada.ActiveRow);
                                myGridEntrada.ActiveCelda(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
                            }
                        }
                        else
                        {
                            myGridEntrada.LimpiarRenglon(myGridEntrada.ActiveRow);
                            myGridEntrada.ActiveCelda(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
                            SendKeys.Send("");
                        }
                    }
                    else
                    {
                        myGridEntrada.LimpiarRenglon(myGridEntrada.ActiveRow);
                    }

                    PasarDatosProducto("grdProductosEntrada");

                    break;
            }

            Totalizar("grdProductosEntrada");
        }

        private void grdProductosSalida_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN);
        }

        private void grdProductosEntrada_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
        }        

        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (txtIdProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado la Clave de Proveedor, verifique.");
                txtFolio.Focus();
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las Observaciones para el Cambio Fisico de Producto, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && (myGridSalida.Rows == 0 || myGridEntrada.Rows == 0))
            {
                bRegresa = false;
                General.msjUser("Debe ingresar por lo menos, un producto para realizar el cambio fisico, verifique.");

                if (myGridSalida.Rows == 0)
                {
                    grdProductosSalida.Focus();
                }
                else
                {
                    grdProductosEntrada.Focus();
                }
            }

            if (bRegresa && !EncontrarCantidadCero())
            {
                bRegresa = false;
                General.msjUser("Es probable que tenga alguno de los productos a cambiar sin una cantidad especificada." +
                    " Por favor, revise sus productos nuevamente y actualice su cantidad asociada para poder continuar.");
            }

            if (bRegresa && !ValidarDetallesCompletos())
            {
                bRegresa = false;
                General.msjUser("El número de productos ingresados como salida no corresponden con los de entrada o viceversa." +
                    " Por favor, cerciórese de que sea el mismo número de productos de ambos movimientos para poder continuar.");
            }

            if (bRegresa && DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un cambio por proveedor, verifique por favor.";
                ////bRegresa = opPermisosEspeciales.VerificarPermisos("CAMBIO_FISICO_PROVEEDOR", sMsjNoEncontrado);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CAMBIO_FISICO_PROVEEDOR", sMsjNoEncontrado);
            }

            return bRegresa;
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;

            if (GrabarEncabezado(TipoCambio.Salida))
            {
                sFolioMovto = sFolioCambioSalida; // .Substring(3); 

                bRegresa = GrabarEncabezado(TipoCambio.Entrada); 
            }

            return bRegresa;  
        }

        private bool GrabarEncabezado(TipoCambio Tipo)
        {
            bool bRegresa = false;
            string sFolio = "";
            string sProveedor = txtIdProveedor.Text;
            ////string sMovtoSalida = "SCP";
            ////string sMovtoEntrada = "ECP"; 
            string sMovtoRegistrar = ""; 
            DateTime dtmFechaRegistro = dtpFechaRegistro.Value;
            string sPersonal = txtIdPersonal.Text;
            string sObservaciones = txtObservaciones.Text;
            double dSubTotal = txtSubTotal.Double;
            double dIva = txtIva.Double;
            double dTotal = txtTotal.Double;
            int iOpcion = 1;
            string sSql = "";


            sMovtoRegistrar = "ECP";
            sTipoES = "E";


            if (Tipo == TipoCambio.Salida)
            {
                sMovtoRegistrar = "SCP";
                sTipoES = "S"; 
            }


            SKU.Reset();
            SKU.TipoDeMovimiento = sMovtoRegistrar;

            sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                "\t@IdTipoMovto_Inv = '{4}', @TipoES = '{5}', @Referencia = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}', \n" +
                "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n", 
                sEmpresa, sEstado, sFarmacia, "*", sMovtoRegistrar, sTipoES, "",sPersonal, sObservaciones, dSubTotal, dIva, dTotal, 1, SKU.SKU);

            sSql += "\n\n";
            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if(myLeer.Leer())
                {
                    // sFolioCambioSalida = myLeer.Campo("Folio"); 
                    sFolio = myLeer.Campo("Folio");
                    sFolioMovto = sFolio;

                    SKU.SKU = myLeer.Campo("SKU");
                    SKU.Foliador = myLeer.Campo("Foliador");
                    SKU.FolioMovimiento = sFolio;
                    //sFolio = SKU.Foliador;

                    sSql = string.Format("Exec spp_Mtto_CambiosProv_Enc \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCambio = '{3}',\n" +
                        "\t@IdProveedor = '{4}', @TipoMovto = '{5}', @FechaRegistro = '{6}', @IdPersonal = '{7}', @Observaciones = '{8}',\n" +
                        "\t@SubTotal = '{9}', @Iva = '{10}', @Total = '{11}', @iOpcion = '{12}' \t",
                        sEmpresa, sEstado, sFarmacia, sFolio, sProveedor, sMovtoRegistrar, General.FechaYMD(dtmFechaRegistro), sPersonal,
                        sObservaciones, dSubTotal, dIva, dTotal, iOpcion);

                    if(!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        switch(Tipo)
                        {
                            case TipoCambio.Salida:
                                sFolioCambioSalida = myLeer.Campo("Folio");

                                SKU_Salida.SKU = SKU.SKU;
                                SKU_Salida.FolioMovimiento = sFolioCambioSalida;
                                SKU_Salida.Foliador = myLeer.Campo("Foliador");
                                break;

                            case TipoCambio.Entrada:
                                sFolioCambioEntrada = myLeer.Campo("Folio");

                                SKU_Entrada.SKU = SKU.SKU;
                                SKU_Entrada.FolioMovimiento = sFolioCambioEntrada;
                                SKU_Entrada.Foliador = myLeer.Campo("Foliador");
                                break;

                            default:
                                break;
                        }

                        bRegresa = GrabarDetalle(sEmpresa, sEstado, sFarmacia, sFolio, Tipo);

                    }
                }
            } 

            return bRegresa;
        }

        private bool GrabarDetalle(string Empresa, string Estado, string Farmacia, string Folio, TipoCambio Tipo) 
        {
            bool bRegresa = false;

            switch (Tipo)
            {
                case TipoCambio.Salida:
                    bRegresa = GrabarDetalleSalida(Empresa, Estado, Farmacia, Folio);
                    break;

                case TipoCambio.Entrada:
                    bRegresa = GrabarDetalleEntrada(Empresa, Estado, Farmacia, Folio);
                    break;

                default:
                    break;
            }

            return bRegresa; 
        }

        ////private bool GrabarDetalle(string sEmpresa, string sEstado, string sFarmacia, string sFolioCambioSalida, string sFolioCambioEntrada)
        ////{
        ////    bool bRegresa = true;

        ////    if (sFolioCambioEntrada == "")
        ////    {
        ////        if (!GrabarDetalleSalida(sEmpresa, sEstado, sFarmacia, sFolioCambioSalida))
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        if (!GrabarDetalleEntrada(sEmpresa, sEstado, sFarmacia, sFolioCambioEntrada))
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }
            
        ////    return bRegresa;
        ////}

        private bool GrabarDetalleSalida(string sEmpresa, string sEstado, string sFarmacia, string Folio)
        {               
            bool bRegresa = false;
            string sSql = "";                        
            double dCosto = 0;
            double dTasaIva = 0D;
            double dImporte = 0D;
            int iOpcion = 1;

            for (int iRow = 1; iRow <= myGridSalida.Rows; iRow++)
            {
                sIdProductoSalida = myGridSalida.GetValue(iRow, (int)Cols.Codigo);
                sCodigoEANSalida = myGridSalida.GetValue(iRow, (int)Cols.CodEAN);               
                dCosto = myGridSalida.GetValueDou(iRow, (int)Cols.Costo);
                dTasaIva = myGridSalida.GetValueDou(iRow, (int)Cols.TasaIva);
                dImporte = myGridSalida.GetValueDou(iRow, (int)Cols.Importe);
                iCantidadSalida = myGridSalida.GetValueInt(iRow, (int)Cols.Cantidad);

                if (sIdProductoSalida != "")
                {
                    // Grabar el Detalle correspondiente a la Salida 
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_Det \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovtoInv = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @UnidadDeSalida = '{6}', @TasaIva = '{7}', @Cantidad = '{8}', \n" +
                        "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProductoSalida, sCodigoEANSalida, 0,
                        dTasaIva, iCantidadSalida, dCosto, dImporte, 'A');

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCambio = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Cantidad = '{6}', @Costo = '{7}', @TasaIva = '{8}',@Importe = '{9}', @iOpcion = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, Folio, sIdProductoSalida, sCodigoEANSalida, iCantidadSalida, dCosto, dTasaIva, dImporte, iOpcion);
                    if(!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = GrabarDetalleLotesSalida(sEmpresa, sEstado, sFarmacia, Folio, sIdProductoSalida, sCodigoEANSalida, dCosto);
                        if(!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool GrabarDetalle_Ubicaciones( clsLotes Lote, string Folio, string IdProducto, string CodigoEAN, TipoCambio Tipo )
        {
            bool bRegresa = false;
            string sSql = "";
            int iOpcion = 1;
            string sSKU_Interno = "";

            clsLotesUbicacionesItem[] Ubicaciones = null;

            switch(Tipo)
            {
                case TipoCambio.Salida:
                    Ubicaciones = Lote.Ubicaciones(Lote.SKU, Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);
                    break;
                case TipoCambio.Entrada:
                    Ubicaciones = Lote.Ubicaciones("", Lote.Codigo, Lote.CodigoEAN, Lote.ClaveLote, Lote.IdSubFarmacia);
                    break;
            }

            foreach (clsLotesUbicacionesItem L in Ubicaciones)
            {
                if (L.Cantidad > 0)
                {
                    sSKU_Interno = Tipo == TipoCambio.Salida ? Lote.SKU : SKU_Entrada.SKU; 

                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @IdPasillo = '{7}', @IdEstante = '{8}',\n" +
                        "\t@IdEntrepano = '{9}', @SKU = '{10}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Pasillo, L.Estante, L.Entrepano, sSKU_Interno);

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}', \n" +
                        "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepano = '{11}', @Status = '{12}', @SKU = '{13}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto,
                        L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, 'A', sSKU_Interno);

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes_Ubicaciones \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}',\n" +
                        "\t@FolioCambio = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                        "\t@IdPasillo = '{9}', @IdEstante = '{10}', @IdEntrepaño = '{11}', @iOpcion = '{12}', @SKU = '{13}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote,
                        L.Cantidad, L.Pasillo, L.Estante, L.Entrepano, iOpcion, sSKU_Interno);

                    bRegresa = myLeer.Exec(sSql);
                    if(!bRegresa)
                    {
                        break;
                    }
                }
            }

            return bRegresa;

        }

        private bool GrabarDetalleEntrada(string sEmpresa, string sEstado, string sFarmacia, string Folio)
        {
            bool bRegresa = true;
            string sSql = "";                     
            double dCosto = 0;
            double dTasaIva = 0D;
            double dImporte = 0D;
            // string sSubFarmacia = "";
            // string sClaveLote = "";
            // bool bEsConsignacion = false;
            int iOpcion = 1;

            for (int iRow = 1; iRow <= myGridEntrada.Rows; iRow++)
            {
                sIdProductoEntrada = myGridEntrada.GetValue(iRow, (int)Cols.Codigo);
                sCodigoEANEntrada = myGridEntrada.GetValue(iRow, (int)Cols.CodEAN);               
                dCosto = myGridEntrada.GetValueDou(iRow, (int)Cols.Costo);
                dTasaIva = myGridEntrada.GetValueDou(iRow, (int)Cols.TasaIva);
                dImporte = myGridEntrada.GetValueDou(iRow, (int)Cols.Importe);
                iCantidadEntrada = myGridEntrada.GetValueInt(iRow, (int)Cols.Cantidad);

                if (sIdProductoEntrada != "")
                {
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}' " +
                        "Exec spp_Mtto_FarmaciaProductos_CodigoEAN @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProducto = '{3}', @CodigoEAN = '{4}' ",
                        sEmpresa, sEstado, sFarmacia, sIdProductoEntrada, sCodigoEANEntrada);
                    if(!myLeer.Exec(sSql))
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
                            DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, sIdProductoEntrada, sCodigoEANEntrada, 0,
                            dTasaIva, iCantidadEntrada, dCosto, dImporte, 'A');
                        if(!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            // Grabar el Detalle correspondiente a la Salida 
                            sSql += "\n\n";
                            sSql = string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN \n" +
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCambio = '{3}', \n" +
                                "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @Cantidad = '{6}', @Costo = '{7}', @TasaIva = '{8}',@Importe = '{9}', @iOpcion = '{10}' \n",
                                sEmpresa, sEstado, sFarmacia, Folio, sIdProductoEntrada, sCodigoEANEntrada, iCantidadEntrada, dCosto, dTasaIva, dImporte, iOpcion);
                            if(!myLeer.Exec(sSql))
                            {
                                bRegresa = false;
                                break;
                            }
                            else
                            {
                                bRegresa = GrabarDetalleLotesEntrada(sEmpresa, sEstado, sFarmacia, Folio, sIdProductoSalida, sCodigoEANSalida, dCosto);
                                if(!bRegresa)
                                {
                                    break;
                                }
                            }
                        }
                    }


                    ////// Registrar el producto en las tablas de existencia 
                    ////sSql = string.Format("Exec spp_Mtto_FarmaciaProductos '{0}', '{1}', '{2}', '{3}' " +
                    ////                     "Exec spp_Mtto_FarmaciaProductos_CodigoEAN '{0}', '{1}', '{2}', '{3}', '{4}' ",
                    ////                     sEmpresa, sEstado, sFarmacia, sIdProductoEntrada, sCodigoEANEntrada);
                    ////sSql += "\n\n"; 
                    ////sSql += string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                    ////    DtGeneral.EmpresaConectada, sEstado, sFarmacia, sFolioMovto, sIdProductoEntrada, sCodigoEANEntrada, 0,
                    ////    dTasaIva, iCantidadEntrada, dCosto, dImporte, 'A'); 

                    ////// Grabar el Detalle correspondiente a la Entrada
                    ////sSql += "\n\n"; 
                    ////sSql += string.Format("Set Dateformat YMD Exec spp_Mtto_CambiosProv_Det_CodigosEAN '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}," +
                    ////    " {8}, {9}, {10}", sEmpresa, sEstado, sFarmacia, Folio, sIdProductoEntrada, sCodigoEANEntrada, iCantidadEntrada, dCosto, dTasaIva, 
                    ////    dImporte, iOpcion);

                    ////if (!myLeer.Exec(sSql))
                    ////{
                    ////    bRegresa = false;
                    ////    break;
                    ////}
                    ////else
                    ////{
                    ////    if (!GrabarDetalleLotesEntrada(sEmpresa, sEstado, sFarmacia, Folio, sIdProductoEntrada, sCodigoEANEntrada, dCosto))
                    ////    {
                    ////        bRegresa = false;
                    ////    }
                    ////}
                }
            }

            return bRegresa;
        }

        ////private bool GrabarDetalleLotes(string sEmpresa, string sEstado, string sFarmacia, string sFolioCambioSalida, string sFolioCambioEntrada, 
        ////    string sIdProducto, string sCodigoEAN)
        ////{
        ////    bool bRegresa = true;

        ////    if (sFolioCambioSalida != "")
        ////    {
        ////        if (!GrabarDetalleLotesSalida(sEmpresa, sEstado, sFarmacia, sFolioCambioSalida, sIdProducto, sCodigoEAN))
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        if (!GrabarDetalleLotesEntrada(sEmpresa, sEstado, sFarmacia, sFolioCambioEntrada, sIdProducto, sCodigoEAN))
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }

        ////    return bRegresa;
        ////}

        private bool GrabarDetalleLotesSalida(string sEmpresa, string sEstado, string sFarmacia, string Folio, string sIdProducto, 
            string sCodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";
            string sSubFarmacia = "";
            string sClaveLote = "";
            int iEsConsignacion = 0;
            // int iCantidad = 0;
            int iOpcion = 1;

            clsLotes[] ListaLotesSalida = LotesSalida.Lotes(sIdProducto, sCodigoEAN);

            foreach (clsLotes L in ListaLotesSalida)
            {
                iEsConsignacion = 0;
                iCantidadSalida = L.Cantidad;
                sSubFarmacia = L.IdSubFarmacia;
                sClaveLote = L.ClaveLote;

                if (L.EsConsignacion )
                {
                    iEsConsignacion = 1;
                }

                if(L.Cantidad > 0)
                {
                    sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                        "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', L.SKU);

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCambio = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @IdSubFarmacia = '{6}', @ClaveLote = '{7}', @EsConsignacion = '{8}',\n" +
                        "\t@Cantidad = '{9}', @iOpcion = '{10}', @SKU = '{11}' \n",
                        sEmpresa, sEstado, sFarmacia, Folio, sIdProducto, sCodigoEAN, sSubFarmacia, sClaveLote, iEsConsignacion, iCantidadSalida, iOpcion, L.SKU);

                    if(!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = true;
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarDetalle_Ubicaciones(L, Folio, sIdProducto, sCodigoEAN, TipoCambio.Salida);
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

        private bool GrabarDetalleLotesEntrada(string sEmpresa, string sEstado, string sFarmacia, string Folio, 
            string sIdProducto, string sCodigoEAN, double Costo)
        {
            bool bRegresa = false;
            string sSql = "";           
            string sSubFarmacia = "";
            string sClaveLote = "";
            int iEsConsignacion = 0;
            int iOpcion = 1;
            int iMesesCaducidad = 0; 

            clsLotes[] ListaLotesEntrada = LotesEntrada.Lotes(sIdProductoEntrada, sCodigoEANEntrada);          

            foreach (clsLotes L in ListaLotesEntrada)
            {
                iEsConsignacion = 0;

                sSubFarmacia = L.IdSubFarmacia;
                sClaveLote = L.ClaveLote;
                iCantidadEntrada = L.Cantidad;
                iMesesCaducidad = L.MesesDeCaducidad;

                if (L.EsConsignacion)
                {
                    iEsConsignacion = 1; 
                }

                if (L.Cantidad > 0)
                {
                    ////sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                    ////                     sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sIdProductoEntrada, sCodigoEAN, L.ClaveLote,
                    ////                     General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal);
                    
                    ////sSql += "\n\n"; 
                    ////sSql += string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                    ////    sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, sIdProductoEntrada, sCodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');

                    
                    ////sSql += "\n\n"; 
                    ////sSql += string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10} ",
                    ////    sEmpresa, sEstado, sFarmacia, Folio, sIdProductoEntrada, sCodigoEAN, sSubFarmacia, sClaveLote, iEsConsignacion,
                    ////    iCantidadEntrada, iOpcion);

                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                            "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @ClaveLote = '{6}', @FechaCaduca = '{7}', @IdPersonal = '{8}',@SKU = '{9}' \n",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sIdProductoEntrada, sCodigoEAN, L.ClaveLote,
                            General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal, SKU_Entrada.SKU);

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_MovtosInv_DetLotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', \n" +
                        "\t@FolioMovtoInv = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @ClaveLote = '{7}', @Cantidad = '{8}',\n" +
                        "\t@Costo = '{9}', @Importe = '{10}', @Status = '{11}', @SKU = '{12}' \n",
                        sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, sIdProductoEntrada, sCodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A', SKU_Entrada.SKU);

                    sSql += "\n\n";
                    sSql += string.Format("Exec spp_Mtto_CambiosProv_Det_CodigosEAN_Lotes \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioCambio = '{3}',\n" +
                        "\t@IdProducto = '{4}', @CodigoEAN = '{5}', @IdSubFarmacia = '{6}', @ClaveLote = '{7}', @EsConsignacion = '{8}',\n" +
                        "\t@Cantidad = '{9}', @iOpcion = '{10}', @SKU = '{11}'  \n",
                        sEmpresa, sEstado, sFarmacia, Folio, sIdProductoEntrada, sCodigoEAN, sSubFarmacia, sClaveLote, iEsConsignacion, iCantidadEntrada, iOpcion, SKU_Entrada.SKU);
                    if(!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        bRegresa = true;
                        if(GnFarmacia.ManejaUbicaciones)
                        {
                            bRegresa = GrabarDetalle_Ubicaciones(L, Folio, sIdProductoEntrada, sCodigoEANEntrada, TipoCambio.Entrada);
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

        private bool AfectarExistencia()
        {
            bool bRegresa = false;

            if ( AfectarExistencia( sFolioCambioSalida ) )
            {
                bRegresa = AfectarExistencia( sFolioCambioEntrada ); 
            }

            return bRegresa; 
        }

        private bool AfectarExistencia( string Folio )
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 
            string sSql = "";
            bool bRegresa = false; 

            AfectarInventario Inv = AfectarInventario.Aplicar;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            ////if (Aplicar)
            ////    Inv = AfectarInventario.Aplicar; 

            ////if (AfectarCosto)
            ////    Costo = AfectarCostoPromedio.Afectar; 

            sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}', @Aplica = '{4}', @AfectarCostos = '{5}' " +
                "\n" +
                "----Exec spp_INV_ActualizarCostoPromedio @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioMovto = '{3}' ",
                DtGeneral.EmpresaConectada, sEstado, sFarmacia, Folio, (int)Inv, (int)Costo);

            bRegresa = myLeer.Exec(sSql);

            if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            {
                ////bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(Folio);
                bRegresa = DtGeneral.PermisosEspeciales_Biometricos.GrabarPropietarioDeHuella(myConn, Folio);
            }

            return bRegresa;
        }

        private bool MarcarEnvioDeInformacion(StatusDeRegistro Actualizado)
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private bool EncontrarCantidadCero()
        {
            bool bRegresa = true;
            
            if ((myGridSalida.GetValue(1, (int)Cols.Descripcion) == "") | (myGridEntrada.GetValue(1, (int)Cols.Descripcion) == ""))
            {
                bRegresa = false;
            }
            else
            {
                if (LotesSalida.CantidadTotal == 0 & LotesEntrada.CantidadTotal == 0)
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= myGridSalida.Rows; i++)
                    {
                        if (myGridSalida.GetValue(i, (int)Cols.CodEAN) != "" && myGridSalida.GetValueInt(i, (int)Cols.Cantidad) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                    }

                    for (int i = 1; i <= myGridEntrada.Rows; i++)
                    {
                        if (myGridEntrada.GetValue(i, (int)Cols.CodEAN) != "" && myGridEntrada.GetValueInt(i, (int)Cols.Cantidad) == 0)
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            if (LotesEntrada.Cantidad == 0)
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

        private bool ValidarDetallesCompletos()
        {
            bool bRegresa = true;

            if (myGridSalida.TotalizarColumna((int)Cols.Cantidad) != myGridEntrada.TotalizarColumna((int)Cols.Cantidad))
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                myLeer.DataSetClase = myQuery.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating");
                if (myLeer.Leer())
                {
                    CargaDatosProveedor();
                    txtObservaciones.Focus();
                    txtIdProveedor.Enabled = false;
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }
        }

        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 
            if (myLeer.Campo("Status").ToUpper() == "A")
            {
                txtIdProveedor.Text = myLeer.Campo("IdProveedor");
                lblNombreProveedor.Text = myLeer.Campo("Nombre");
            }
            else
            {
                General.msjUser("El Proveedor " + myLeer.Campo("Nombre") + " actualmente se encuentra cancelado, verifique. ");
                txtIdProveedor.Text = "";
                lblProveedor.Text = "";
                txtIdProveedor.Focus();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
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

        private void RevisarLotesEntradaSalida(string sGridSalida, string sGridEntrada, int iRow)
        {
            if (sGridSalida != "")
            {
                string sCodigo = "";
                if ((myGridEntrada.GetValue(iRow, (int)Cols.CodEAN).Trim() != "" & txtFolio.Text == "*") |
                    (myGridEntrada.GetValue(iRow, (int)Cols.CodEAN).Trim() != "" & txtFolio.Text != "*"))
                {
                    if (txtFolio.Text == "*")
                    {

                        sValorGrid = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN);
                        myLeer.DataSetClase = myQuery.Productos_CodigosEAN_Datos(sValorGrid, "grdProductosSalida_KeyDown");

                        if (!myLeer.Leer())
                        {
                            General.msjError("Ocurrió Un Error al buscar la Información.");
                        }
                        else
                        {
                            sCodigo = myLeer.Campo("IdProducto");
                            myGridSalida.SetValue(iRow, (int)Cols.Codigo, sCodigo);
                            CargarDatosProducto("grdProductosSalida");
                        }
                    }
                    else
                    {
                        MostrarOcultarLotes("grdProductosSalida", iRow);
                    }
                }
            }
            else 
            {
                string sCodigo = "";
                if ((myGridSalida.GetValue(iRow, (int)Cols.CodEAN).Trim() != "" & txtFolio.Text == "*") |
                    (myGridSalida.GetValue(iRow, (int)Cols.CodEAN).Trim() != "" & txtFolio.Text != "*"))
                {
                    if (txtFolio.Text == "*")
                    {
                        sValorGrid = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN);
                        myLeer.DataSetClase = myQuery.Productos_CodigosEAN_Datos(sValorGrid, "grdProductosEntrada_KeyDown");

                        if (!myLeer.Leer())
                        {
                            General.msjError("Ocurrió Un Error al buscar la Información.");
                        }
                        else
                        {
                            sCodigo = myLeer.Campo("IdProducto");
                            myGridEntrada.SetValue(iRow, (int)Cols.Codigo, sCodigo);
                            CargarDatosProducto("grdProductosEntrada");
                        }
                    }
                    else
                    {
                        MostrarOcultarLotes("grdProductosEntrada", iRow);
                    }
                }           
            }
        }

        private void EvaluarCantidadesLotes()
        {
            string sIdProductoSalida = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.Codigo);
            string sIdProductoEntrada = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.Codigo);
            string sCodigoEANSalida = myGridSalida.GetValue(myGridSalida.ActiveRow, (int)Cols.CodEAN);
            string sCodigoEANEntrada = myGridEntrada.GetValue(myGridEntrada.ActiveRow, (int)Cols.CodEAN);

            if (sIdProductoSalida == sIdProductoEntrada)
            {
                if (sCodigoEANSalida != sCodigoEANEntrada)
                {
                    myGridSalida.SetValue(myGridEntrada.ActiveRow, (int)Cols.Cantidad, 0);   
                }                
            }           
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtFolio.Text.Trim() == "")
                {
                    txtFolio.Text = "*";
                    txtIdProveedor.Focus();
                    txtFolio.Enabled = false;
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }
        }

        private void ImprimirMovimiento()
        {
            ImprimirMovimiento(sFolioCambioSalida, "Salida por cambio físico de producto");
            ImprimirMovimiento(sFolioCambioEntrada, "Entrada por cambio físico de producto");
        }

        private void btnCancelar_Click( object sender, EventArgs e )
        {

        }

        private void ImprimirMovimiento(string Folio, string Titulo)
        {
            bool bRegresa = false;
            DatosCliente.Funcion = "ImprimirMovimiento()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.TituloReporte = Titulo; 
            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CambiosFisicosProveedor.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("FolioCambio", Folio);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////if (General.ImpresionViaWeb)
            ////{
            ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////    DataSet datosC = myDatosCliente.DatosCliente();

            ////    btReporte = myConexionWeb.Reporte(InfoWeb, datosC);
            ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
            ////}
            ////else
            ////{
            ////    myRpt.CargarReporte(true);
            ////    bRegresa = !myRpt.ErrorAlGenerar;
            ////}

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
    }
}
