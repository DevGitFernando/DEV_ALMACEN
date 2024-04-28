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
//using DllRobotDispensador; 
using DllFarmaciaSoft.Devoluciones; 

namespace Almacen.OrdenCompraConsigna
{
    public partial class FrmEntradasConsignacion_Devoluciones : FrmBaseExt
    {
        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer myLeerLotes;
        DllFarmaciaSoft.clsAyudas Ayuda;

        clsDevoluciones Dev;
        
        clsLeerWebExt leerWeb;
        clsLeer leerPedido;
        clsLeer leer;
        TiposDeInventario tpInventarioModulo = TiposDeInventario.Consignacion;
        TipoDevolucion tpDevolucion = TipoDevolucion.OrdenCompra; 

        DataSet dtsCargarDatos = new DataSet();
        DllFarmaciaSoft.clsConsultas Consultas;
        clsGrid myGrid;
        clsLotes Lotes;
        clsCodigoEAN EAN = new clsCodigoEAN();

        clsDatosCliente DatosCliente;
        Farmacia.wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        //bool bEsClienteInterface = RobotDispensador.Robot.EsClienteInterface;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();

        bool bEsConsultaExterna = false;
        // bool bContinua = true;
        // bool bModificarCaptura = true;
        string sFolioCompra = "";  // , sMensaje = "";
        string sEmpresa = DtGeneral.EmpresaConectada; 
        string sEstado = DtGeneral.EstadoConectado; 
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sFolioMovto = "";

        // string sIdTipoMovtoInv = "EPD";
        // string sTipoES = "E";
        string sFormato = "#,###,##0.###0";

        bool bFolioGuardado = false;

        string sUrlAlmacenRegional = GnFarmacia.UrlAlmacenRegional;
        string sHostAlmacenRegional = GnFarmacia.HostAlmacenRegional;
        string sIdFarmaciaAlmacenRegional = GnFarmacia.IdFarmaciaAlmacenRegional;

        string sFolioDevolucion = "";

        private enum Cols
        {
            Ninguna = 0,
            CodEAN = 1, Codigo = 2, Descripcion = 3, TasaIva = 4, CantidadPrometidaCajas = 5, CantidadPrometidaPiezas = 6,
            Cantidad = 7, CantDev = 8, Costo = 9, Importe = 10, ImporteIva = 11, ImporteTotal = 12, PorcSurtimiento = 13,
            CantidadPrometidaCajasRecibida = 14
        }

        clsMotivosDevoluciones motivodev;

        public FrmEntradasConsignacion_Devoluciones()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            myLeerLotes = new clsLeer(ref ConexionLocal);

            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leerPedido = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);

            cnnRegional = new clsConexionSQL();
            cnnRegional.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnnRegional.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnnRegional.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnnRegional.DatosConexion.Password = General.DatosConexion.Password;
            cnnRegional.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.BackColorColsBlk = Color.White;
            grdProductos.EditModeReplace = true;
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            Error = new clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            motivodev = new clsMotivosDevoluciones(General.DatosConexion, TipoDevolucion.OrdenCompra, sEmpresa, sEstado, sFarmacia);  
        }

        private void FrmPedidosDistribuidor_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this,null);             
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

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        #region Limpiar 
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
            LimpiarPantalla();
            CargarLicitaciones();
            CargarSubFarmacias();
        }

        private void LimpiarPantalla()
        {
            // bModificarCaptura = false;
            bFolioGuardado = true;
            IniciarToolBar(false, false, false);
            myGrid.EstiloGrid(eModoGrid.Normal);

            btnEjecutar.Enabled = false;
            lblCancelado.Visible = false;

            btnMotivosDev.Enabled = false;

            if (!bEsConsultaExterna)
            {
                bFolioGuardado = false;

                Fg.IniciaControles(this, true);
                grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                //dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
                Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
                Lotes.ManejoLotes = OrigenManejoLotes.OrdenesDeCompra;

                // Estos campos deben ir deshabilitados son campos controlados 
                dtpFechaRegistro.Enabled = false;
                //dtpFechaVenceDocto.Enabled = false;
                //dtpFechaDocto.Enabled = true;

                txtSubTotal.Text = "0.0000";
                txtIva.Text = "0.0000";
                txtTotal.Text = "0.0000";

                // Reiniciar Grid por Completo 
                myGrid = new clsGrid(ref grdProductos, this);
                myGrid.Limpiar(false);
                myGrid.BackColorColsBlk = Color.White;
                grdProductos.EditModeReplace = true;
                myGrid.BloqueaColumna(false, (int)Cols.Costo);


                // myGrid.Limpiar(true);
                txtIdPersonal.Text = DtGeneral.IdPersonal;
                lblPersonal.Text = DtGeneral.NombrePersonal;
                txtIdPersonal.Enabled = false;

                motivodev = new clsMotivosDevoluciones(General.DatosConexion, TipoDevolucion.OrdenCompra, sEmpresa, sEstado, sFarmacia);
                // Asegurarnos de que la fecha maxima sea la del sistema, para evitar errores 
                //dtpFechaDocto.MinDate = General.FechaSistema.AddMonths(-1);
                //dtpFechaDocto.MaxDate = General.FechaSistema;
                dtsPedido = null;            
                //dtsPedido = PreparaDtsPedidos();
                txtFolio.Focus();                
            }
        }
        #endregion Limpiar

        #region Buscar Folio 
        ////public void MostrarFolioCompra(string Empresa, string Estado, string Farmacia, string Folio )
        ////{
        ////    this.bEsConsultaExterna = true; 
        ////    IniciarToolBar(false, false, false);

        ////    Fg.IniciaControles(this, true);
        ////    grdProductos.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
        ////    //dtpFechaDocto.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
        ////    Lotes = new clsLotes(sEstado, sFarmacia, GnFarmacia.MesesCaducaMedicamento, tpInventarioModulo);
        ////    Lotes.ManejoLotes = OrigenManejoLotes.Compras;


        ////    sEmpresa = Empresa;
        ////    sEstado = Estado;
        ////    sFarmacia = Farmacia; 
        ////    txtFolio.Text = Folio;
        ////    txtFolio_Validating(null, null);
        ////    btnNuevo.Enabled = false; 

        ////    this.ShowDialog();
        ////    // bEsConsultaExterna = false; 
        ////}

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = true;
            // bModificarCaptura = true;
            bFolioGuardado = false; 
            IniciarToolBar(false, false, false);
            myGrid.Limpiar(false); 

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Focus();
            }
            else
            {
                //myLeer.DataSetClase = Consultas.EntradasEnc_Consignacion(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                myLeer.DataSetClase = Consultas.OrdenesCompras_Enc(sEmpresa, sEstado, sFarmacia, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
                if (!myLeer.Leer())
                {
                    bContinua = false;
                }
                else
                {
                    bFolioGuardado = true;
                    IniciarToolBar(true, false, true);
                    // bModificarCaptura = false;
                    CargaEncabezadoFolio(); 
                }

                if (bContinua)
                {
                    if (!CargaDetallesFolio())
                    {
                        bContinua = false;
                    }
                    //else
                    //    Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.                    
                }
            }

            if (!bContinua)
            {
                txtFolio.Focus();
            }
        }

        private void CargaEncabezadoFolio()
        {
            // Inicializar el Control 
            DateTimePicker dtpPaso = new DateTimePicker();
            
            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio"); // FolioCompra
            sFolioCompra = txtFolio.Text;
            txtIdProveedor.Text = myLeer.Campo("IdProveedor");
            lblProveedor.Text = myLeer.Campo("Proveedor");

            cboLicitacion.Data = myLeer.Campo("IdLicitacion");
            cboSubFarmacias.Data = myLeer.Campo("IdFuente");
            txtOrden.Text = myLeer.Campo("Orden");
            txtFolioPre.Text = myLeer.Campo("FolioPresup");
            
            txtReferenciaDocto.Text = myLeer.Campo("ReferenciaDocto");           

            txtSubTotal.Text = myLeer.CampoDouble("SubTotal").ToString();
            txtIva.Text = myLeer.CampoDouble("Iva").ToString();
            txtTotal.Text = myLeer.CampoDouble("Total").ToString();
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");            
            txtIdPersonal.Enabled = false; 
            txtIdPersonal.Text = myLeer.Campo("IdPersonal");
            lblPersonal.Text = myLeer.Campo("NombrePersonal"); 

            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);

            btnMotivosDev.Enabled = true;

            if (myLeer.Campo("Status").ToUpper() == "B")
            {
                lblCancelado.Visible = true;
                lblCancelado.Text = "BLOQUEADO";
                IniciarToolBar(false, false, true);
                btnMotivosDev.Enabled = false;
            } 

            if (myLeer.Campo("Status") == "C") 
            {
                lblCancelado.Visible = true;
                btnMotivosDev.Enabled = false;
            }

            if (myLeer.Campo("Status").ToUpper() == "D")
            {
                btnEjecutar.Enabled = true;
                General.msjUser("Folio de Recepción OC con devolución parcial.");
            }            
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = true;

            //myLlenaDatos.DataSetClase = Consultas.EntradasDet_Consignacion_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioCompra, "txtFolio_Validating");
            myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioCompra, "txtFolio_Validating");
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
            //myLlenaDatos.DataSetClase = Consultas.EntradasDet_Consignacion_Lotes_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesMovimiento");
            myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Lotes_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolio");
            Lotes.AddLotes(myLlenaDatos.DataSetClase);

            if (GnFarmacia.ManejaUbicaciones)
            {
                //myLlenaDatos.DataSetClase = Consultas.EntradasDet_Consignacion_Lotes_Ubicaciones(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolio");
                myLlenaDatos.DataSetClase = Consultas.OrdenesCompras_Det_Lotes_Ubicaciones_Devolucion(sEmpresa, sEstado, sFarmacia, sFolioCompra, "CargarDetallesLotesFolioU");
                Lotes.AddLotesUbicaciones(myLlenaDatos.DataSetClase);
            }
        }

        #endregion Buscar Folio        

        #region Guardar/Actualizar Folio

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // bContinua = false;
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
                if (validaDatos())
                {
                    IniciarToolBar();
                    Dev = new clsDevoluciones(sEmpresa, sEstado, sFarmacia, ConexionLocal.DatosConexion);

                    // Agregar los datos 
                    Dev.Folio = "*";
                    //Dev.FolioCompra = txtFolio.Text;
                    Dev.Tipo = TipoDevolucion.OrdenCompra;
                    Dev.Referencia = txtFolio.Text;
                    Dev.FechaOperacionDeSistema = GnFarmacia.FechaOperacionSistema;
                    Dev.IdPersonal = DtGeneral.IdPersonal;
                    Dev.Observaciones = txtObservaciones.Text;
                    Dev.SubTotal = Convert.ToDouble(txtSubTotal.Text);
                    Dev.Iva = Convert.ToDouble(txtIva.Text);
                    Dev.Total = Convert.ToDouble(txtTotal.Text);
                    Dev.NombreOperacion = "DEVOLUCION_DE_ORDEN_DE_COMPRA";
                    Dev.MsjSinPermiso = "Usuario sin permisos para realizar devolución de Recepcion OC, Favor de verificar."; 

                    // Agregar los Productos 
                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        clsProducto P = new clsProducto();
                        P.IdProducto = myGrid.GetValue(i, (int)Cols.Codigo);
                        P.CodigoEAN = myGrid.GetValue(i, (int)Cols.CodEAN);
                        P.Unidad = 1;
                        P.TasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                        P.Cantidad = myGrid.GetValueDou(i, (int)Cols.CantDev);
                        P.Valor = myGrid.GetValueDou(i, (int)Cols.Costo);
                        Dev.AddProducto(P);
                    }

                    // Agregar los Lotes 
                    Dev.Lotes = Lotes;

                    Dev.MotivosDev = motivodev.Retorno;

                    if (Dev.Guardar())
                    {
                        sFolioDevolucion = Dev.Folio;
                        ImprimirEntrada(false);
                        ImprimirDevolucionPedido();
                        //btnNuevo_Click(null, null);
                    }
                    else
                    {
                        IniciarToolBar(bBtnGuardar, bBtnCancelar, bBtnImprimir);
                    }
                }
            }

        }        
        #endregion Guardar/Actualizar Folio

        #region Eliminar

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion Eliminar

        #region Imprimir
        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Imprime la devolución ? ") == DialogResult.No)
                {
                    bRegresa = false;
                    LimpiarPantalla();
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio Recepción OC no valido, Favor de verificar.");
                    txtFolio.Focus(); 
                }
            }

            return bRegresa;
        }

        private void ImprimirEntrada(bool Confirmar)
        {
            bool bRegresa = false;

            if (validarImpresion(Confirmar))
            {
                DatosCliente.Funcion = "ImprimirEntrada()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Recepcion_Orden_Compras.rpt";
                myRpt.TituloReporte = "Informe Recepción Orden Compra";

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", txtFolio.Text);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);      


                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Error al cargar Informe.");
                }
            }
        }

        private void ImprimirDevolucionPedido()
        {
            bool bRegresa = false;


            DatosCliente.Funcion = "ImprimirDevolucionPedido()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Entradas_Consignacion_Devolucion.rpt";

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("Folio", sFolioDevolucion);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);


            if (bRegresa)
            {
                btnNuevo_Click(null, null);
            }
            else
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirEntrada(false);
        }
        #endregion Imprimir

        #region Validaciones de Controles
        private bool validaDatos()
        {
            bool bRegresa = true;
            // string sIdProducto = "";
                        
            if (txtFolio.Text == "")
            {
                bRegresa = false;
                General.msjUser("Id Recepción OC no valido, Favor de verificar.");
                txtFolio.Focus();                
            }

            ////if (bRegresa && txtIdProveedor.Text == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Clave de Proveedor inválida, verifique.");
            ////    txtIdProveedor.Focus();
            ////}

            if (bRegresa && txtReferenciaDocto.Text == "")
            {
                bRegresa = false;
                General.msjUser("Capture Referencia, Favor de verificar.");
                txtReferenciaDocto.Focus();
            }            

            ////if (bRegresa)
            ////{
            ////    bRegresa = validarCapturaProductos();
            ////}

            if (bRegresa)
            {
                bRegresa = validarCantidadesCapturadas();
            }

            if (bRegresa && !motivodev.Marco)
            {
                bRegresa = false;
                General.msjUser("Capture los motivos de Devolución, Favor de verificar.");
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
                General.msjUser("No se ha capturado un renglon para realizar la devolución. Favor de verificar.");

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
                iCantidad = myGrid.GetValueInt(i, (int)Cols.CantDev);

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
                General.msjAviso("Se detecto una ó mas diferencias en la captura de productos, la Compra no puede ser completada.");
                FrmProductosConDiferencias f = new FrmProductosConDiferencias(dtsProductosDiferencias);
                f.ShowDialog();
            }

            return bRegresa;
        }

        #endregion Validaciones de Controles

        #region Eventos
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // Checar por incongruencias 
                myLeer.DataSetClase = Ayuda.Folios_Compras(sEmpresa, sEstado, sFarmacia, "txtFolio_KeyDown");
                //myLeer.DataSetClase = Ayuda.FolioCompras(sEstado, sFarmacia, "txtFolio_KeyDown");

                if (myLeer.Leer())
                {
                    CargaEncabezadoFolio();
                    CargaDetallesFolio();
                }
                //Fg.BloqueaControles(this, false);// Se bloquea todo ya que una Compra guardada no se puede modificar.
            }

        } 

        private void dtpFechaDocto_ValueChanged(object sender, EventArgs e)
        {
            //dtpFechaVenceDocto.Value = dtpFechaRegistro.Value.AddMonths(1);
        }

        private void dtpFechaDocto_Validating(object sender, CancelEventArgs e)
        {
            //dtpFechaDocto_ValueChanged(null, null);
        }
        #endregion Eventos  

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
            int iRowActivo = myGrid.ActiveRow;

            if (lblCancelado.Visible == false)
            {
                if ( !myGrid.BuscaRepetido( myLeer.Campo("CodigoEAN"), iRowActivo, 1) )
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.CodEAN, myLeer.Campo("CodigoEAN"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Codigo, myLeer.Campo("IdProducto"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    //myGrid.SetValue(iRowActivo, (int)Cols.PrecioMaxPublico, myLeer.Campo("PrecioMaxPublico"));

                    // Cuando es compra Promocion Regalo el costo de entrada debe ser 0 
                    //if (!chkEsCompraPromocion.Checked)
                    {
                        myGrid.SetValue(iRowActivo, (int)Cols.Costo, myLeer.CampoDouble("CostoPromedio"));
                    }
                    

                    myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.CodEAN);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Costo);

                    //// Marcar el Renglon como Codigo de Robot si la Farmacia conectada tiene Robot 
                    //if (bEsClienteInterface)
                    //{
                    //    GnFarmacia.ValidarCodigoIMach4(myGrid, myLeer.CampoBool("EsMach4"), iRowActivo);
                    //}

                    CargarLotesCodigoEAN();
                }
                else
                {
                    General.msjUser("Renglon capturado con datos repetidos.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodEAN, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                    myGrid.EnviarARepetido(); 
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
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodEAN) != "" )
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;

                            // Bloquear la celda de Costo cuando se Promocion - Regalo 
                            //if (chkEsCompraPromocion.Checked)
                                //myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols.Costo);

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
                                    CargaDatosProducto();
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

            myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, false, "CargarLotesCodigoEAN()");
            if (Consultas.Ejecuto)
            {
                // Mostrar la Pantalla aún cuando no existan Lotes para el CodigoEAN
                myLeer.Leer();
                Lotes.AddLotes(myLeer.DataSetClase);

                if (GnFarmacia.ManejaUbicaciones)
                {
                    myLeer.DataSetClase = Consultas.LotesDeCodigo_CodigoEAN_Ubicaciones(sEmpresa, sEstado, sFarmacia, sCodigo, sCodEAN, tpInventarioModulo, false, "CargarLotesCodigoEAN()");
                    if (Consultas.Ejecuto)
                    {
                        myLeer.Leer();
                        Lotes.AddLotesUbicaciones(myLeer.DataSetClase);
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
                    Lotes.EsEntrada = false; // Asegurar que solo se pueda dar salida al maximo de piezas recibidas
                    Lotes.TipoCaptura = 1; //Por piezas   // myGrid.GetValueInt(iRow, (int)Cols.TipoCaptura);

                    //Lotes.PermitirLotesNuevosConsignacion = false; 

                    // Si el movimiento ya fue aplicado no es posible agregar lotes 
                    Lotes.CapturarLotes = false; //chkAplicarInv.Enabled;
                    Lotes.ModificarCantidades = true; //chkAplicarInv.Enabled;

                    //Configurar Encabezados 
                    Lotes.Encabezados = EncabezadosManejoLotes.EsDevolucionDeCompras;

                    // Mostrar la Pantalla de Lotes 
                    Lotes.FechaDeSistema = GnFarmacia.FechaOperacionSistema;
                    Lotes.Show();

                    myGrid.SetValue(iRow, (int)Cols.CantDev, Lotes.Cantidad);
                    //myGrid.SetValue(iRow, (int)Cols.TipoCaptura, Lotes.TipoCaptura);
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

        private void grdProductos_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void chkEsCompraPromocion_CheckedChanged(object sender, EventArgs e)
        {

        }

        #region Ejecutar
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            FrmDevolucionesImpresion Devoluciones;
            Devoluciones = new FrmDevolucionesImpresion();
            Devoluciones.MostrarPantalla(txtFolio.Text.Trim(), tpDevolucion, (int)TipoDeVenta.Ninguna);
        }
        #endregion Ejecutar

        private void btnMotivosDev_Click(object sender, EventArgs e)
        {
            motivodev.MotivosDevolucion();
        }

        private void CargarSubFarmacias()
        {
            leer = new clsLeer(ref ConexionLocal);
            string sSql = "";
            string sFiltroConsignacion = "";
            string sFiltroSubFarmacia = "";
            string sIdEstado = DtGeneral.EstadoConectado;
            string sIdFarmacia = DtGeneral.FarmaciaConectada;
            DataSet SubFarmacias = new DataSet();

            sFiltroConsignacion = " and EsConsignacion = 1 ";
            sFiltroConsignacion += " and EmulaVenta = 0 ";

            sSql = string.Format("Select IdEstado, IdFarmacia, IdSubFarmacia, SubFarmacia, EsConsignacion, EmulaVenta " +
                    " From vw_Farmacias_SubFarmacias (NoLock) " +
                    " Where IdEstado = '{0}' and IdFarmacia = '{1}' {2} {3} ",
                    sIdEstado, sIdFarmacia, sFiltroConsignacion, sFiltroSubFarmacia);


            if (leer.Exec(sSql))
            {
                SubFarmacias = leer.DataSetClase;
            }

            cboSubFarmacias.Clear();
            cboSubFarmacias.Add();
            cboSubFarmacias.Add(SubFarmacias, true, "IdSubFarmacia", "SubFarmacia");
        }

        private void CargarLicitaciones()
        {
            leer = new clsLeer(ref ConexionLocal);
            string sSql = "";
            string sIdEstado = DtGeneral.EstadoConectado;

            DataSet Licitaciones = new DataSet();

            sSql = string.Format("SELECT IdEstado, IdLicitacion, Licitacion " +
                    " FROM Ctrl_Licitaciones (NoLock) " +
                    " WHERE IdEstado = '{0}'  ", sIdEstado);


            if (leer.Exec(sSql))
            {
                Licitaciones = leer.DataSetClase;
            }

            cboLicitacion.Clear();
            cboLicitacion.Add();
            cboLicitacion.Add(Licitaciones, true, "IdLicitacion", "Licitacion");
        }
    } // Llaves de la Clase
} // Llaves del NameSpace
