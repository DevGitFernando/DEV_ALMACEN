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
//using Dll_IMach4;

using DllFarmaciaSoft.Reporteador;

using Farmacia.Procesos;

namespace Farmacia.Vales_Servicio_A_Domicilio
{
    public partial class FrmRegistroVales_ServicioDomicilio : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer myLeerValeDet;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;

        clsLeer leer;
        clsLeer leerRef;

        DllFarmaciaSoft.clsAyudas Ayuda;
        
        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        clsGrid myGrid;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        DllFarmaciaSoft.Ventas.clsImprimirVentas VtasImprimir;

        bool bEsConsultaExterna = false; 
        bool bContinua = true;
        bool bModificarCaptura = true;
        string sFolioRegistroVale = "", sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFolioMovto = "";

        string sIdTipoMovtoInv = "EPV";
        string sTipoES = "E";
        string sFormato = "#,###,##0.###0";
        bool bRegistrarVenta_Vale = false;
        string sFolioVenta_Integrar = "";
        string sFolioNvoVta = "";
        string sProveedorReembolso = "0000";

        bool bFolioGuardado = false;

        bool bEmiteVales = GnFarmacia.EmisionDeValesCompletos;
        bool bManejaVales_ServicioDomicilio = GnFarmacia.Maneja_ValesServicioDomicilio; 

        FrmIniciarSesionEnCaja Sesion;
        bool bHabilitarReembolso = false;

        string sFolioServicioDom = "";
        string sFolioVale = "";
        int iTipoSurtimiento = 0;

        bool bMostrarErrorCant = false;

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, Cantidad = 5, 
            PrecioMaxPublico = 6,
            Costo = 7, Importe = 8, ImporteIva = 9, ImporteTotal = 10, TipoCaptura = 11
        } 

        public FrmRegistroVales_ServicioDomicilio()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLeerValeDet = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);
            leerRef = new clsLeer(ref ConexionLocal);

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            VtasImprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente,
                sEmpresa, sEstado, sFarmacia, General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Credito);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            //////toolStripSeparator_04.Visible = bManejaVales_ServicioDomicilio;
            //////btnServicioADomicilio.Visible = bManejaVales_ServicioDomicilio; 
        }

        private void FrmRegistroVales_ServicioDomicilio_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this,null);

            //tmSesion.Enabled = true;
            //tmSesion.Start();
        }

        #region Limpiar 
        private void IniciarToolBar()
        {
            IniciarToolBar(false, false, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            IniciarToolBar(Guardar, Cancelar, Imprimir, false); 
        }

        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool ServicioADomicilio)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnServicioADomicilio.Enabled = ServicioADomicilio; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {          

            if (!bEsConsultaExterna)
            {
                bRegistrarVenta_Vale = false;
                sFolioVenta_Integrar = "";
                sFolioNvoVta = ""; 

                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Venta);
                Lotes.ManejoLotes = OrigenManejoLotes.Compras;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                
                txtSubTotal.Text = "0.0000";
                txtIva.Text = "0.0000";
                txtTotal.Text = "0.0000";

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.Limpiar(false);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                myGrid.BloqueaColumna(false, (int)Cols.Costo);
                
                
                chkEsReembolso.Enabled = false;

                txtReferenciaDocto.Focus();
            }
        }
        #endregion Limpiar

        #region Buscar Folio 
        public void MostrarFolioCompra(string Empresa, string Estado, string Farmacia, string Folio )
        {
            this.bEsConsultaExterna = true; 
            IniciarToolBar(false, false, false);

            Fg.IniciaControles(this, true);
            grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Venta);
            Lotes.ManejoLotes = OrigenManejoLotes.Compras;
            
            sEmpresa = Empresa;
            sEstado = Estado;
            sFarmacia = Farmacia; 
            txtFolio.Text = Folio;
            txtFolio_Validating(null, null);
            btnNuevo.Enabled = false; 

            this.ShowDialog();
            // bEsConsultaExterna = false; 
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            bModificarCaptura = true;
            bFolioGuardado = false;
            IniciarToolBar(false, false, false);
            myGrid.Limpiar(false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Vales_Servicio_A_DomicilioEnc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");

                if (myLeer.Leer())
                {
                    //bFolioGuardado = true;
                    IniciarToolBar(false, false, true);
                    bModificarCaptura = false;
                    CargaEncabezadoFolio();
                }
                else
                {
                    bContinua = false;
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }                                      
                }
            }
            
        }

        private void CargaEncabezadoFolio()
        {
            
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker();             
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioRegistroVale = txtFolio.Text;
            txtFolio.Enabled = false;
            sFolioNvoVta = myLeer.Campo("FolioVentaGenerado");
            txtVale.Text = myLeer.Campo("FolioVale"); // FolioVale 
            txtVale.Enabled = false;

            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaSurtimiento");
                                 
            if (myLeer.CampoBool("ServicioConfirmado"))
            {
                bFolioGuardado = true;
                iTipoSurtimiento = myLeer.CampoInt("TipoSurtimiento");

                if (iTipoSurtimiento == 1)
                {
                    lblTipoSurt.Visible = true;
                    lblTipoSurt.Text = "TRANSFERENCIA";
                }

                if (iTipoSurtimiento == 2)
                {
                    lblTipoSurt.Visible = true;
                    lblTipoSurt.Text = "COMPRA DIRECTA";
                }
            }           
            
            //Se bloquea el encabezado del Folio 
            //Fg.BloqueaControles(this, false, FrameEncabezado);
            
            if (myLeer.Campo("Status") == "C")
            {
                lblCancelado.Visible = true;                
            }
            
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;
            
            myLlenaDatos.DataSetClase = Consultas.Vales_Servicio_A_DomicilioDet(sEmpresa, sEstado, sFarmacia, sFolioRegistroVale, "CargaDetallesFolio");

            if (myLlenaDatos.Leer())
            {
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
            }
            else
            {
                bRegresa = false;
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            CargarDetallesLotesFolio();
            myGrid.EstiloGrid(eModoGrid.ModoRow); 

            return bRegresa;                 
        }

        private void CargarDetallesLotesFolio()
        {        
            myLlenaDatos.DataSetClase = clsLotes.PreparaDtsLotes();
            myLlenaDatos.DataSetClase = Consultas.Vales_Servicio_A_DomicilioDet_Lotes(sEmpresa, sEstado, sFarmacia, sFolioRegistroVale, "CargarDetallesLotesFolio");
            if (myLlenaDatos.Leer())
            {
                Lotes.AddLotes(myLlenaDatos.DataSetClase);
            }
        }
        #endregion Buscar Folio

        #region Grabar informacion
        private bool GrabarEncabezado()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_MovtoInv_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                sEmpresa, sEstado, sFarmacia, txtFolio.Text, sIdTipoMovtoInv, sTipoES, "",
                DtGeneral.IdPersonal, "",
                txtSubTotal.Text.Trim().Replace(",", ""), txtIva.Text.Trim().Replace(",", ""), txtTotal.Text.Trim().Replace(",", ""), 1);
            
            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioMovto = myLeer.Campo("Folio");
                bRegresa = GrabarDetalle();
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
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
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos '{0}', '{1}', '{2}', '{3}' " +
                                         "Exec spp_Mtto_FarmaciaProductos_CodigoEAN '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                         sEmpresa, sEstado, sFarmacia, sIdProducto, sCodigoEAN);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_Det '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                             sEmpresa, sEstado, sFarmacia, sFolioMovto, sIdProducto, sCodigoEAN, iUnidadDeSalida,
                            nTasaIva, iCantidad, nCosto, nImporte, 'A');
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                        else
                        {
                            if (!GrabarDetalleLotes(sIdProducto, sCodigoEAN, nCosto))
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

        private bool GrabarDetalleLotes(string IdProducto, string CodigoEAN, double Costo)
        {
            bool bRegresa = true;
            string sSql = "";

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);

            foreach (clsLotes L in ListaLotes)
            {
                if (L.Cantidad > 0)
                {
                    // Registrar el producto en las tablas de existencia 
                    sSql = string.Format("Exec spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                                         sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, IdProducto, CodigoEAN, L.ClaveLote, General.FechaYMD(L.FechaCaducidad, "-"), DtGeneral.IdPersonal);
                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        sSql = string.Format("Exec spp_Mtto_MovtosInv_DetLotes '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioMovto, IdProducto, CodigoEAN, L.ClaveLote, L.Cantidad, Costo, L.Cantidad * Costo, 'A');
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }
        #endregion Grabar informacion

        #region Guardar/Actualizar Folio
        private void btnGuardar_Click(object sender, EventArgs e)
        {    
            bContinua = false;
            bool bBtnGuardar = btnGuardar.Enabled;
            bool bBtnCancelar = btnCancelar.Enabled;
            bool bBtnImprimir = btnImprimir.Enabled; 

            //if (txtFolio.Text != "*")
            //{
            //    MessageBox.Show("Este Folio ya ha sido guardado por lo tanto no puede ser modificado");
            //}
            //else
            {
                EliminarRenglonesVacios();
                if (ValidaDatos())
                {
                    if (ConexionLocal.Abrir())
                    {
                        IniciarToolBar(); 
                        ConexionLocal.IniciarTransaccion();

                        //if (GrabarEncabezado())
                        {
                            if (Guarda_Encabezado_Compra())
                            {
                                //if (AfectarExistencia(true, true))
                                bContinua = VerificarCantidadesServicioDomicilio();

                                if (bContinua)
                                {
                                    bContinua = IntegrarVale_A_Venta(); 
                                }
                            }
                        }                       

                        if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                        {
                            txtFolio.Text = sFolioRegistroVale;
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            // btnNuevo_Click(null, null);
                            IniciarToolBar(false, false, true);
                            ImprimirRegistroVale(); 
                        }
                        else
                        {
                            txtFolio.Text = "*";
                            sFolioNvoVta = ""; 
                            ConexionLocal.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            if (bMostrarErrorCant)
                            {
                                General.msjAviso("Se excedio en la captura de cantidades de algunas Claves. \n  No se pudo generar el registro de servicio a domicilio.");
                            }
                            else
                            {
                                General.msjError("Ocurrió un error al guardar la información.");
                            }
                            IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir); 
                            //btnNuevo_Click(null, null);

                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        Error.LogError(ConexionLocal.MensajeError);
                        General.msjAviso("No fue posible establacer conexión con el servidor, intente de nuevo.");
                    }

                }
            }

        }

        private bool Guarda_Encabezado_Compra()
        {
            bool bRegresa = true;
            string sSql = "";
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            int iServicioConfirmado = 0;

            iServicioConfirmado = 1;
            
            sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Vales_Servicio_A_DomicilioEnc " + 
                                "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
                            sEmpresa, sEstado, sFarmacia, txtFolio.Text, txtVale.Text.Trim(), DtGeneral.IdPersonal, dtpFechaRegistro.Text, dtpFechaRegistro.Text,
                            iServicioConfirmado, DtGeneral.IdPersonal, iTipoSurtimiento, txtReferenciaDocto.Text, iOpcion);


            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioRegistroVale = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");
                bRegresa = Guarda_Detalles_Compra();
            }

            return bRegresa;
        }

        private bool Guarda_Detalles_Compra()
        {
            bool bRegresa = true;
            string sSql = "", sCodigoEAN = "", sIdProducto = "";
            // string sEstado = DtGeneral.EstadoConectado, sFarmacia = DtGeneral.FarmaciaConectada;
            int iUnidadDeEntrada = 0, iRenglon = 0, iCantidadRecibida = 0;
            double dCostoUnitario = 0, dTasaIva = 0, dSubTotal = 0, dImpteIva = 0,dImporte = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion
            
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                // Se obtienen los datos para la insercion.
                sCodigoEAN =  myGrid.GetValue(i, (int)Cols.CodEAN);
                sIdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                iCantidadRecibida = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                dCostoUnitario = myGrid.GetValueDou(i, (int)Cols.Costo);
                dSubTotal = myGrid.GetValueDou(i, (int)Cols.Importe);
                dImpteIva = myGrid.GetValueDou(i, (int)Cols.ImporteIva);
                dImporte = myGrid.GetValueDou(i, (int)Cols.ImporteTotal);

                iUnidadDeEntrada = 1; //Este dato es Temporal ya que obtendra valor con la Clase.                    
                iRenglon = i;

                if (sIdProducto != "" && iCantidadRecibida > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Vales_Servicio_A_DomicilioDet " +
                                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}','{12}', '{13}', '{14}' ",
                            sEmpresa, sEstado, sFarmacia, sFolioRegistroVale, sIdProducto, sCodigoEAN, iRenglon,
                            iUnidadDeEntrada, iCantidadRecibida, dCostoUnitario, dTasaIva, dSubTotal, dImpteIva, dImporte, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                    else
                    {
                        if (!Guarda_Lotes_Compra(sIdProducto, sCodigoEAN, dCostoUnitario, i))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            return bRegresa;
        }

        private bool Guarda_Lotes_Compra(string IdProducto, string CodigoEAN, double Costo, int Renglon)
        {
            bool bRegresa = true;
            string sSql = ""; //, sCodigoEAN = "", sIdProducto = "", sClaveLote = "";
            string sEstado = DtGeneral.EstadoConectado, sFarmacia = DtGeneral.FarmaciaConectada;
            // int iRenglon = 0, iCantidadRecibida = 0;
            int iOpcion = 1; //La opcion 1 indica que es una insercion/actualizacion

            clsLotes[] ListaLotes = Lotes.Lotes(IdProducto, CodigoEAN);
            foreach (clsLotes L in ListaLotes)
            {
                if (IdProducto != "" && L.Cantidad > 0)
                {
                    sSql = String.Format("Set DateFormat YMD Exec spp_Mtto_Vales_Servicio_A_DomicilioDet_Lotes " +
                                " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                            sEmpresa, sEstado, sFarmacia, L.IdSubFarmacia, sFolioRegistroVale, IdProducto, CodigoEAN, L.ClaveLote,
                            Renglon, L.Cantidad, iOpcion);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool AfectarExistencia(bool Aplicar, bool AfectarCosto)
        {
            // Aplicar los Costos Promedios en la tabla de Configuracion para el Calculo de Precios de Ventas 

            AfectarInventario Inv = AfectarInventario.Ninguno;
            AfectarCostoPromedio Costo = AfectarCostoPromedio.NoAfectar;

            if (Aplicar)
            {
                Inv = AfectarInventario.Aplicar;
            }

            string sSql = string.Format("Exec spp_INV_AplicarDesaplicarExistencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' " + 
                "\n" +
                " Exec spp_INV_ActualizarCostoPromedio '{0}', '{1}', '{2}', '{3}' ",
                sEmpresa, sEstado, sFarmacia, sFolioMovto, (int)Inv, (int)Costo);
            return myLeer.Exec(sSql);
            
        }

        private bool IntegrarVale_A_Venta()
        {
            bool bRegresa = true;
            int iRegistrarVenta_Vale = bRegistrarVenta_Vale ? 1 : 0;

            // if (bRegistrarVenta_Vale)
            {
                string sSql = string.Format("Exec spp_Mtto_Vtas_RegistroServicioDomicilio '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                    sEmpresa, sEstado, sFarmacia, sFolioRegistroVale, sFolioVenta_Integrar, DtGeneral.IdPersonal, iRegistrarVenta_Vale);

                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
                }
                else
                {
                    if (myLeer.Leer())
                    {
                        // Relacionar el folio de venta generado con el Registro del Vale 
                        sFolioNvoVta = myLeer.Campo("FolioNuevo"); 
                        sSql = string.Format(
                            "Update V Set FolioVentaGenerado = '{4}'" +
                            "From Vales_Servicio_A_Domicilio V (NoLock) " + 
                            "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioServicioDomicilio = '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioRegistroVale, sFolioNvoVta); 
                        if (!myLeer.Exec(sSql))
                        {
                            bRegresa = false;
                        }
                    }
                }
            }

            return bRegresa; 
        } 
        #endregion Guardar/Actualizar Folio

        #region Eliminar

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion Eliminar

        #region Validaciones de Controles
        private bool ValidaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";
                        
            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Servicio a domicilio inválido, verifique.");
                txtFolio.Focus();                
            }

            if (bRegresa && txtVale.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Vale inválido, verifique.");
                txtVale.Focus();
            }            

            if (bRegresa && txtReferenciaDocto.Text == "")
            {
                bRegresa = false;
                General.msjUser("Referencia inválida, verifique.");
                txtReferenciaDocto.Focus();
            }

            //if (bRegresa && float.Parse(txtSubTotal.Text) <= 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("El SubTotal debe ser mayor a cero");
            //    txtSubTotal.Focus();
            //}            


            //if (bRegresa && float.Parse(txtTotal.Text) <= 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("El Total debe ser mayor a cero");
            //    txtTotal.Focus();
            //}
            

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
            }

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            //if (!ComparaTotales(true))
            //{
            //    bRegresa = false;
            //    General.msjUser("El Subtotal, Iva y Total de la Factura no coinciden con el calculado por el sistema. \nEstos datos ha sido corregidos.");
            //    myGrid.ActiveRow = 1;
            //    grdProductos.Focus();
            //}
            
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
                General.msjUser("Debe capturar al menos un producto para el registro de servicio a domicilio\n y/o capturar cantidades para al menos un lote, verifique.");

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, el Registro no puede ser completado.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos y Funciones 
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    // Checar por incongruencias 
            //    myLeer.DataSetClase = Ayuda.Folios_Compras(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown");
            //    //myLeer.DataSetClase = Ayuda.FolioCompras(sEstado, sFarmacia, "txtFolio_KeyDown");

            //    if (myLeer.Leer())
            //    {
            //        CargaEncabezadoFolio();
            //        CargaDetallesFolio();
            //    }
            //    //Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.
            //}

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

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

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

        private void txtVale_Validating(object sender, CancelEventArgs e)
        {
            string sStatusVale = "";
            if (txtVale.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.ValesEmisionEnc(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVale.Text, 8), false, false, "txtFolio_Validating");
                myLeerValeDet.DataSetClase = Consultas.ValesEmisionDet(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVale.Text, 8), "CargaDetallesVale");
                if (!myLeer.Leer())
                {
                    txtVale.Text = "";
                    txtVale.Focus();
                }
                else
                {
                    txtVale.Text = myLeer.Campo("Folio");
                    sStatusVale = myLeer.Campo("Status").ToUpper();

                    if (sStatusVale == "A")
                    {
                        sFolioVenta_Integrar = myLeer.Campo("FolioVenta");
                        bRegistrarVenta_Vale = sFolioVenta_Integrar != "" ? true : false;
                    }
                    else if (sStatusVale == "C")
                    {
                        txtVale.Text = "";
                        General.msjUser("El Folio de Vale ingresado esta CANCELADO. Verifique");
                        txtVale.Focus();
                    }
                    else if (sStatusVale == "R")
                    {
                        txtVale.Text = "";
                        General.msjUser("El Folio de Vale ingresado ya se encuentra REGISTRADO. Verifique");
                        txtVale.Focus();
                    }

                    BuscarBeneficiario();
                }
            }
        }

        private void BuscarBeneficiario()
        {
            myLeer.DataSetClase = Consultas.ValesEmision_InformacionAdicional(sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtVale.Text, 8), "BuscarBeneficiario()");

            if (myLeer.Leer())
            {
                lblBeneficiario.Text = myLeer.Campo("Beneficiario");
            }
        }
        #endregion Eventos y Funciones

        #region Grid
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == 1)
            {
                if (e.KeyCode == Keys.F1)
                {
                    myLeer.DataSetClase = Ayuda.ProductosEstado(sEmpresa, sEstado, "grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        CargaDatosProducto();
                    }
                }
            }

            if (e.KeyCode == Keys.Delete) 
                removerLotes(); 
        }

        private void CargaDatosProducto()
        {
            clsLeer LeerTemp = new clsLeer();
            clsLeer LeerDetRef = new clsLeer();
            int iRowActivo = myGrid.ActiveRow;
            string sFiltro = string.Format(" ClaveSSA = '{0}' ", myLeer.Campo("ClaveSSA"));
            string sFiltroEAN = string.Format(" CodigoEAN = '{0}' ", myLeer.Campo("CodigoEAN"));
            LeerTemp.DataRowsClase = myLeerValeDet.DataTableClase.Select(sFiltro);
            //leerVale.DataRowsClase = leer.DataTableClase.Select("Status = 'A'");

            if (lblCancelado.Visible == false)
            {
                if (!LeerTemp.Leer())
                {
                    General.msjUser("La Clave SSA del producto no se encuentra en el vale, verifique.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }
                else 
                {
                    LeerDetRef.DataRowsClase = leerRef.DataTableClase.Select(sFiltroEAN);
                    if (LeerDetRef.Leer())
                    {
                        if (!myGrid.BuscaRepetido(myLeer.Campo("CodigoEAN"), iRowActivo, 1))
                        {
                            myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                            myGrid.SetValue(iRowActivo, (int)Cols.PrecioMaxPublico, myLeer.Campo("PrecioMaxPublico"));
                            myGrid.SetValue(iRowActivo, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));
                            myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                            myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                            myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo);

                            ////////////// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                            ////////if (IMach4.EsClienteIMach4)
                            ////////    GnFarmacia.ValidarCodigoIMach4(myGrid, myLeer.CampoBool("EsMach4"), iRowActivo);

                            CargarLotesCodigoEAN();
                        }
                        else
                        {
                            General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                            myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                            limpiarColumnas();
                            myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                            myGrid.EnviarARepetido();
                        }
                    }
                    else
                    {
                        General.msjUser("El Producto no se encuentra en el Detalle de la Referencia. Verifique");
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                        limpiarColumnas();
                        myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                    }
                }
            }

            // grdProductos.EditMode = false;
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (!bFolioGuardado)
            {
                if (lblCancelado.Visible == false)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" && myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad) > 0)
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodEAN);
                        }
                    }
                }
            }

        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case 1: // Si se cambia el Codigo, se limpian las columnas
                    {
                        limpiarColumnas();
                    }
                    break;
            }

        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            string sValor = "";
            bool bEsEAN_Unico = true; 

            switch (myGrid.ActiveCol)
            {
                case 1:                    
                    {
                        sValor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN);

                        if (sValor != "")
                        {
                            if (EAN.EsValido(sValor))
                            {
                                myLeer.DataSetClase = Consultas.ProductosEstado(sEmpresa, sEstado, sValor, "grdProductos_EditModeOff");
                                if (myLeer.Leer())
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
                                            myLeer.GuardarDatos(1, "CodigoEAN", sValor); 
                                            //myLeer.DataSetClase = Consultas.ProductosFarmacia(sEmpresa, sEstado, sFarmacia, sValor, "grdProductos_EditModeOff");
                                        }

                                        CargaDatosProducto();
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
                    }

                    break;                
            }

            // ComparaTotales(true);
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue( myGrid.ActiveRow, i, "");
            } 
        }

        private void EliminarRenglonesVacios()
        {
            for (int i = 1; i <= myGrid.Rows; i++) //Renglones.
            {
                if (myGrid.GetValue(i, 2).Trim() == "") //Si la columna oculta IdProducto esta vacia se elimina.
                    myGrid.DeleteRow(i);
            }

            if (myGrid.Rows == 0) // Si No existen renglones, se inserta 1.
                myGrid.AddRow();
        }

        private bool ComparaTotales()
        {
            return ComparaTotales(false);
        }

        private bool ComparaTotales(bool MostrarDatos)
        {
            bool bIguales = false;
            double dSubTotalText = 0, dIvaText = 0, dTotalText = 0;
            double dSubTotalGrid = 0, dIvaGrid = 0, dTotalGrid = 0;            

            //Se obtienen los totales de los Textbox
            dSubTotalText = Math.Round(Convert.ToDouble(txtSubTotal.Text),4, MidpointRounding.ToEven);
            dIvaText = Math.Round(Convert.ToDouble(txtIva.Text), 4, MidpointRounding.ToEven);
            dTotalText = Math.Round(Convert.ToDouble(txtTotal.Text), 4, MidpointRounding.ToEven);

            //Se obtienen los totales del Grid.
            dSubTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.Importe), 4, MidpointRounding.ToEven);
            dIvaGrid = Math.Round( myGrid.TotalizarColumnaDou((int)Cols.ImporteIva), 4, MidpointRounding.ToEven);
            dTotalGrid = Math.Round(myGrid.TotalizarColumnaDou((int)Cols.ImporteTotal), 4, MidpointRounding.ToEven);

            // Se compara que sean iguales.
            if ((dSubTotalText == dSubTotalGrid) && (dIvaText == dIvaGrid) && (dTotalText == dTotalGrid))
            {
                bIguales = true;
                if (MostrarDatos)
                {
                    txtSubTotal.Text = dSubTotalGrid.ToString(sFormato);
                    txtIva.Text = dIvaGrid.ToString(sFormato);
                    txtTotal.Text = dTotalGrid.ToString(sFormato);
                } 
            }
            else
            {
                txtSubTotal.Text = dSubTotalGrid.ToString(sFormato);
                txtIva.Text = dIvaGrid.ToString(sFormato);
                txtTotal.Text = dTotalGrid.ToString(sFormato);
            }

            return bIguales;
        }
        #endregion Grid       

        #region Manejo de lotes
        private void CargarLotesCodigoEAN()
        {
            int iRow = myGrid.ActiveRow;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.Codigo);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);

            myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, TiposDeInventario.Venta, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                if (myLeer.Leer())
                {
                    Lotes.AddLotes(myLeer.DataSetClase);
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
                    ComparaTotales(true);
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
                    Lotes.Codigo = myGrid.GetValue(iRow, (int)Cols.Codigo);//Codigo
                    Lotes.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodEAN);
                    Lotes.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);
                    Lotes.EsEntrada = false;
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    Lotes.PermitirLotesNuevosConsignacion = false; 

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false; //true; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = true; //true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.Default;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, Lotes.Cantidad);
                    myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
                    myGrid.SetActiveCell(iRow, (int)Cols.Costo);

                    //Totalizar();
                    ComparaTotales(true); 
                }
                else
                {
                    myGrid.SetActiveCell(iRow, (int)Cols.CodEAN);
                }
            }
        }
        #endregion Manejo de lotes 

        #region Sesion 
        //private void tmSesion_Tick(object sender, EventArgs e)
        //{
        //    tmSesion.Enabled = false;

        //    // bEmiteVales = true; //Esta linea es de prueba debe ir comentariada. 
        //    if (!bEmiteVales)
        //    {
        //        General.msjUser("Esta Farmacia no esta autorizada para Emitir vales.");
        //        this.Close();
        //    }
        //    else
        //    {
        //        FrmFechaSistema Fecha = new FrmFechaSistema();
        //        Fecha.ValidarFechaSistema();

        //        GnFarmacia.ValidarSesionUsuario = true;
        //        if (Fecha.Exito)
        //        {
        //            GnFarmacia.Parametros.CargarParametros();
        //            Fecha.Close();

        //            Sesion = new FrmIniciarSesionEnCaja();
        //            Sesion.VerificarSesion();

        //            if (!Sesion.AbrirVenta)
        //            {
        //                this.Close();
        //            }
        //            else
        //            {
        //                Sesion.Close();
        //                Sesion = null;
        //                btnNuevo_Click(null, null);
        //            }
        //        }
        //        else
        //        {
        //            this.Close();
        //        }
        //    }
        //}
        #endregion Sesion

        private void chkEsReembolso_CheckedChanged(object sender, EventArgs e)
        {
            //txtIdProveedor.Text = sProveedorReembolso;
            //txtIdProveedor_Validating(null, null);
            //chkEsReembolso.Enabled = false;
        }

        #region MostrarPantalla_Externa
        public void MostrarPantalla(string FolioServicioDom, int TipoSurtimiento)
        {            
            this.bEsConsultaExterna = true;
            this.sFolioServicioDom = FolioServicioDom;
            this.iTipoSurtimiento = TipoSurtimiento;

            Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, TiposDeInventario.Venta);
            Lotes.ManejoLotes = OrigenManejoLotes.Compras;

            txtFolio.Text = sFolioServicioDom;
            txtFolio_Validating(null, null);

            txtVale_Validating(null, null);

            this.ShowDialog();            
        }        
        #endregion MostrarPantalla_Externa

        #region Eventos_Referencia
        private void txtReferenciaDocto_Validating(object sender, CancelEventArgs e)
        {
            if (Verificar_Referencia())
            {
                IniciarToolBar(!bFolioGuardado, false, true);
            }
            else
            {
                txtReferenciaDocto.Focus();
            }
        }

        private bool Verificar_Referencia()
        {
            bool bRegresa = true;
            string sSql = "";

            if (iTipoSurtimiento == 1)  // Entrada por Transferencia
            {
                sSql = string.Format(" Select * From TransferenciasEnc (Nolock) Where IdEmpresa = '{0}' and IdEstado = '{1}' " + 
                                     " and IdFarmacia = '{2}' and FolioTransferencia = '{3}' ", 
                                     sEmpresa, sEstado, sFarmacia, "TE" + Fg.PonCeros(txtReferenciaDocto.Text, 8));

                lblTipoSurt.Text = "TRANSFERENCIA";
            }

            if (iTipoSurtimiento == 2)  // Entrada por Compra Directa
            {
                sSql = string.Format(" Select * From ComprasEnc (Nolock) Where IdEmpresa = '{0}' and IdEstado = '{1}' " +
                                     " and IdFarmacia = '{2}' and FolioCompra = '{3}' ",
                                     sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtReferenciaDocto.Text, 8));

                lblTipoSurt.Text = "COMPRA DIRECTA";
            }

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al verificar la Referencia");
            }
            else
            {
                if (leer.Leer())
                {
                    txtReferenciaDocto.Text = Fg.PonCeros(txtReferenciaDocto.Text, 8);
                    lblTipoSurt.Visible = true;
                    myGrid.Limpiar(true);
                    Detalles_Referencia();
                }
                else
                {
                    bRegresa = false;
                    General.msjAviso("La Referencia Capturada no existe.");
                }
            }


            return bRegresa;
        }

        private bool Detalles_Referencia()
        {
            bool bRegresa = true;
            string sSql = "";

            if (iTipoSurtimiento == 1)  // Entrada por Transferencia
            {
                sSql = string.Format(" Select Distinct IdProducto, CodigoEAN, ClaveSSA From vw_Impresion_Transferencias (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'  " +
                                    " and ClaveSSA In ( Select ClaveSSA From vw_Vales_EmisionDet (Nolock) " +
                                                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{4}' ) ",
                                     sEmpresa, sEstado, sFarmacia, "TE" + Fg.PonCeros(txtReferenciaDocto.Text, 8), Fg.PonCeros(txtVale.Text, 8));
                                
            }

            if (iTipoSurtimiento == 2)  // Entrada por Compra Directa
            {
                sSql = string.Format(" Select Distinct IdProducto, CodigoEAN, ClaveSSA From vw_ComprasDet_CodigosEAN (Nolock) " +
	                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'  " +
	                                " and ClaveSSA In ( Select ClaveSSA From vw_Vales_EmisionDet (Nolock) " + 
                                                        " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{4}' ) ",
                                     sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtReferenciaDocto.Text, 8), Fg.PonCeros(txtVale.Text, 8));
                                
            }

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "");
                General.msjError("Ocurrio un error al verificar la Referencia");
            }
            else
            {
                if (leer.Leer())
                {
                    leerRef.DataSetClase = leer.DataSetClase;
                }
                else
                {
                    bRegresa = false;
                    General.msjAviso("No se encontro el Detalle de la Referencia.");
                }
            }


            return bRegresa;
        }
        #endregion Eventos_Referencia

        #region Imprimir
        private bool validarImpresion()
        {
            bool bRegresa = true;
            return bRegresa;
        }

        private void ImprimirRegistroVale()
        {
            bool bRegresa = false;

            if (validarImpresion())
            {
                DatosCliente.Funcion = "ImprimirRegistroVale()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                // clsReporteador Reporteador = new clsReporteador(ref myRpt, ref DatosCliente); 

                myRpt.RutaReporte = GnFarmacia.RutaReportes;                
                
                VtasImprimir.MostrarVistaPrevia = true;                

                if (VtasImprimir.Imprimir(sFolioNvoVta))
                {
                    btnNuevo_Click(null, null);
                }
                else
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirRegistroVale();
        }
        #endregion Imprimir

        #region Verificar_Cantidades_Servicio_Domicilio
        private bool VerificarCantidadesServicioDomicilio()
        {
            bool bRegresa = true;
            string sSql = "";
            bMostrarErrorCant = false;

            sSql = string.Format(" Exec spp_Vales_VerificaCantidades_ServDomicilio '{0}', '{1}', '{2}', '{3}' ",
                                sEmpresa, sEstado, sFarmacia, sFolioRegistroVale );

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(myLeer, "VerificarCantidadesServicioDomicilio");                
            }
            else
            {
                if (myLeer.Leer())
                {
                    bMostrarErrorCant = true;
                    bRegresa = false;
                    General.msjAviso("Se detectó una ó mas diferencias en las Cantidades de las Claves capturadas.");
                    FrmDiferenciasCant_Claves f = new FrmDiferenciasCant_Claves(myLeer.DataSetClase);
                    f.ShowDialog();
                }
            }

            return bRegresa;
        }
        #endregion Verificar_Cantidades_Servicio_Domicilio

    } // Llaves de la Clase
} // Llaves del NameSpace
