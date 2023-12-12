using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FP;
using SC_SolutionsSystem.FP.Huellas;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

namespace Almacen.OrdenCompra
{
    public enum ProcesosOrdenDeCompra
    {
        Registro = 1,
        Consulta = 2
    }
    public partial class FrmOrdenCompraCodigoEAN_Base : FrmBaseExt 
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsLeer leerChecador;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsMotivosSobreCompra Motivos;
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        string sFormato = "#,###,###,##0.###0";
        string sUrlChecador = "";
        string sTabla;
        bool bHabilitar = false;
        bool bModal = false;
        bool bGeneraAutomatico = false;
        bool bEsAutomatica = false;
        bool bTieneSobrePrecio = false;
        int iTipoOrden = 2;
        bool bPermitirModificarRenglones = true;
        bool bValidarCambioDeDestino = false; 
        bool bEsCambioDeDestino = false;
        string sIdAlmacenDestino = "";

        string sIdPersonalRegistra = "";
        string sEmpresaRegional = "";
        string sEstadoRegional = "";
        string sFolioPedUnidad = "";
        string sUnidad = "";
        string sUrlAlmacen = "";
        string sAutorizar_abrir_orden = "ABRIR_ORDEN_COLOCADA";

        double dImporte_CERO = 0;
        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        int iValidarClaveEnPerfilDeOperacion = 0;//GnCompras.ValidarClavesEnPerfilOperacion ? 1 : 0;
        int iValidarClaveEnPerfilDeComprador = 0;//GnCompras.ValidarClavesEnPerfilDeComprador ? 1 : 0;
        FrmOrdenCompraCodigoEANListadoDeExcedentes f;

        ProcesosOrdenDeCompra tpTipoProceso = ProcesosOrdenDeCompra.Consulta;

        private enum Cols
        {
            //Ninguna = 0,
            //CodigoEAN = 1, IdProducto = 2, Descripcion = 3, Cont_Paquete = 4, Precio = 5, Descuento = 6, TasaIva = 7,
            //Iva = 8, PrecioUnitario = 9, ComisionNegociadora = 10, PrecioLicitado = 11, PorcentajeSobreLicitado = 12,
            //Cantidad = 13, Importe = 14, Cant_Actual = 15, Cant_SobreCompra = 16, PrecioActual = 17, ObservacionesCambio = 18, ObservacionesSobreCompra = 19

            Ninguna = 0,
            CodigoEAN = 1, IdProducto, ClaveSSA, Descripcion, AplicaCosto, PrecioBase, Cont_Paquete, Precio, Descuento, TasaIva,
            PrecioUnitario, ComisionNegociadora, PrecioLicitado, PorcentajeSobreLicitado,
            Cantidad, Importe, Iva, ImporteTotal, Cant_Actual, Cant_SobreCompra, PrecioActual, ObservacionesCambio, ObservacionesSobreCompra 
        } 

        public enum ColsExcedentes { IdClaveSSA = 1, ClaveSSA = 2, Descripcion = 3, Porcentaje = 4, Observaciones = 5 }


        public FrmOrdenCompraCodigoEAN_Base():this(ProcesosOrdenDeCompra.Registro) 
        {
        }

        public FrmOrdenCompraCodigoEAN_Base( ProcesosOrdenDeCompra TipoProceso )
        {
            InitializeComponent();

            tpTipoProceso = TipoProceso; 

            General.Pantalla.AjustarTamaño(this, 80, 80); 


            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);
            leerChecador = new clsLeer(ref cnn);
            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            validarHuella.Url = General.Url;
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true;


            myGrid.SetOrder((int)Cols.CodigoEAN, 1, true);
            myGrid.SetOrder((int)Cols.ClaveSSA, 1, true);
            myGrid.SetOrder((int)Cols.Descripcion, 1, true);
            myGrid.SetOrder((int)Cols.PrecioUnitario, 1, true);


            LimpiarPantalla();

            CargarEmpresas();
            CargarEstados();
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);

            cboPlazosCredito.Clear();
            cboPlazosCredito.Add("0", "<<Seleccione>>");


            if(tpTipoProceso == ProcesosOrdenDeCompra.Consulta)
            {
                this.Text = "Consulta de Órdenes de Compra"; 
            }
        }

        private void FrmPedidosCEDIS_Load(object sender, EventArgs e)
        {

            if (bModal)
            {
                btnNuevo.Enabled = false;
                IniciarToolBar(false, false, true, false, false);
                btnExportarPDF.Enabled = true;
                HabilitarCampos(false, false, false, false, false, false, false, false);
                myGrid.BloqueaRenglon(true);
            }
            else
            {
                if (bGeneraAutomatico)
                {
                    bPermitirModificarRenglones = false; 
                    btnNuevo.Enabled = false;
                    IniciarToolBar(false, false, false, false, false);

                    lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
                    lblStatus.Visible = false;
                    lblStatus.Text = "";
                    lblStatus.Visible = false;
                                 

                    dtpFechaRegistro.Enabled = false;
                    bHabilitar = false;
                    //txtPedido.Text = "*";
                    txtPedido_Validating(null, null);
                    txtProveedor_Validating(null, null);
                    txtProveedor.Enabled = bHabilitar;
                    cboEmpresas.Enabled = bHabilitar;
                    cboEstados.Enabled = bHabilitar;
                    cboPlazosCredito.Enabled = bHabilitar; 

                    rdoContado.Enabled = true;
                    rdoCredito.Enabled = true;

                    txtIdFarmacia_Validating(null, null);
                    txtIdFarmacia.Enabled = bHabilitar;
                    ////myGrid.Limpiar(false); 
                    ////myGrid.SetActiveCell(1, 1); 

                    //txtIdFarmacia.Focus();

                    txtObservaciones.Focus();
                }
                else
                {
                    LimpiarPantalla();
                }
            }
        }

        #region Limpiar
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Cerrar, bool Abrir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = Cancelar;
            btnImprimir.Enabled = Imprimir;
            btnExportarPDF.Enabled = Imprimir; 
            btnCerrarOrden.Enabled = Cerrar;
            btnAbrirOrden.Enabled = Abrir;

            btnCambiarDestino.Enabled = false;


            if(tpTipoProceso == ProcesosOrdenDeCompra.Consulta)
            {
                btnGuardar.Enabled = false;
                btnCambiarDestino.Enabled = false;
                btnCancelar.Enabled = false;
                btnCerrarOrden.Enabled = false;
                btnAbrirOrden.Enabled = false;

                btnGuardar.Visible = false;
                btnCambiarDestino.Visible = false;
                btnCancelar.Visible = false;
                btnCerrarOrden.Visible = false;
                btnAbrirOrden.Visible = false;
            }
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(false);
            bPermitirModificarRenglones = true;
            bValidarCambioDeDestino = false; 
            bEsCambioDeDestino = false;
            sIdAlmacenDestino = "";


            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false, false);

            lblSubTotal.Text = dImporte_CERO.ToString(sFormato);
            lblIVA.Text = dImporte_CERO.ToString(sFormato);
            lblImpteTotal.Text = dImporte_CERO.ToString(sFormato);

            myGrid.BloqueaColumna(true, (int)Cols.ObservacionesSobreCompra);
            myGrid.BloqueaColumna(false, (int)Cols.Cantidad);
            myGrid.SetActiveCell(1, 1); 

            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;
            grdProductos.ContextMenuStrip = null;
            ////lblMensajes.Text = "<< Los Precios de Productos aqui Mostrados son por CAJA >>";

            //cboEmpresas.SelectedIndex = 0;
            //cboEstados.SelectedIndex = 0;

            cboEmpresas.Data = DtGeneral.EmpresaConectada;
            cboEmpresas.Enabled = false;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = false;

            dtpFechaRegistro.Enabled = false;
            rdoContado.Checked = false;
            rdoCredito.Checked = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;
            bHabilitar = false;

            chkDerivaDeOrdenDeCompra.Checked = false; 
            txtOrdenDeCompraVinculada.Enabled = false; 
            txtPedido.Focus();
            Motivos = new clsMotivosSobreCompra();

            cboPlazosCredito.Clear();
            cboPlazosCredito.Add("0", "<<Seleccione>>");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Buscar Pedido 
        private void txtPedido_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            //IniciarToolBar(false, false, false);            
            string sEsAutom = " MANUAL ";

            bValidarCambioDeDestino = false;
            bEsCambioDeDestino = false; 
            lblEsAutomatica.Visible = false;
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtPedido.Text.Trim() == "")
            {
                txtPedido.Enabled = false;
                txtPedido.Text = "*";
                IniciarToolBar(true, false, false, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_OrdenesCompra_Manual_Enc(sEmpresa, sEstado, sFarmacia, txtPedido.Text.Trim(), iTipoOrden, "txtFolio_Validating");
                if (myLeer.Leer())
                {
                    bContinua = true;
                    CargaEncabezadoFolio();
                }

                if (bContinua)
                {
                    bContinua = CargaDetallesFolio();
                }
            }

            if (!bContinua)
            {
                txtPedido.Focus();
            }

            if (bEsAutomatica || bGeneraAutomatico)
            {
                lblEsAutomatica.Visible = true;
                sEsAutom = " AUTOMATICA ";
                myGrid.BloqueaColumna(true, (int)Cols.Cantidad, true);
            }

            lblEsAutomatica.Text = sEsAutom;
        }

        private void CargaEncabezadoFolio()
        {
            bool bHabilitada = true;
            bool bModificarDestino = false;
            string sDiasDePlazo;

            //Se hace de esta manera para la ayuda.
            txtPedido.Text = myLeer.Campo("Folio");
            sFolioPedido = txtPedido.Text;
            
            cboEmpresas.Enabled = false;
            cboEmpresas.Data = myLeer.Campo("FacturarA");
            txtProveedor.Text = myLeer.Campo("IdProveedor");
            lblNomProv.Text = myLeer.Campo("Proveedor");
            cboEstados.Enabled = false; 
            cboEstados.Data = myLeer.Campo("EstadoEntrega");
            txtIdFarmacia.Text = myLeer.Campo("EntregarEn");
            lblEntregarEn.Text = myLeer.Campo("FarmaciaEntregarEn");
            sIdAlmacenDestino = txtIdFarmacia.Text; 

            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            lblDomicilio.Text = myLeer.Campo("Domicilio");       
            txtObservaciones.Text = myLeer.Campo("Observaciones");
            dtpFechaRequeridaEntrega.Value = myLeer.CampoFecha("FechaRequeridaEntrega");
            bEsAutomatica = myLeer.CampoBool("EsAutomatica");

            txtCte.Enabled = false;
            txtSubCte.Enabled = false; 
            txtCte.Text = myLeer.Campo("IdCliente");
            lblCte.Text = myLeer.Campo("NombreCliente");
            txtSubCte.Text = myLeer.Campo("IdSubCliente");
            lblSubCte.Text = myLeer.Campo("NombreSubCliente");

            chkDerivaDeOrdenDeCompra.Checked = myLeer.CampoBool("EsOrdenDerivada"); 
            txtOrdenDeCompraVinculada.Text = myLeer.Campo("FolioOrdenVinculada");
            sDiasDePlazo = myLeer.Campo("DiasDePLazo");

            cboPlazosCredito.Enabled = false;
            cboPlazosCredito.Data = sDiasDePlazo; 

            if (myLeer.CampoBool("EsContado"))
            {
                rdoContado.Checked = true;
            }
            else
            {
                rdoCredito.Checked = true;
            }

            ////IniciarToolBar(false, false, true, false);
            if (myLeer.Campo("Status") == "C")
            {
                IniciarToolBar(false, false, true, false, false);
                lblStatus.Text = "CANCELADA";
                lblStatus.Visible = true;
                HabilitarCampos(false, false, false, false, false, false, false, false);
                bHabilitar = true;
                bHabilitada = false;
            }

            if (myLeer.Campo("Status") == "OC")
            {
                if (DtGeneral.PermisosEspeciales.TienePermiso(sAutorizar_abrir_orden))
                {
                    IniciarToolBar(false, true, true, false, true);
                }
                else
                {
                    IniciarToolBar(false, true, true, false, false);
                }

                lblStatus.Text = "ORDEN COLOCADA";
                lblStatus.Visible = true;
                HabilitarCampos(false, false, false, false, false, false, false, true);
                bHabilitar = true;
                bHabilitada = false;
                bModificarDestino = true;

                bValidarCambioDeDestino = true;
                //bEsCambioDeDestino = false; 
            }

            if (bHabilitada)
            {
                HabilitarCampos(false, true, false, true, true, true, true, bModificarDestino);
                IniciarToolBar(true, true, true, true, false);
                grdProductos.ContextMenuStrip = menuCantidades;
                ////lblMensajes.Text = "<< Los Precios de Productos aqui Mostrados son por CAJA >>                                                                                                         << Clic derecho cambiar de precio >>";
            }

            if (bModal)
            {
                btnNuevo.Enabled = false;
                IniciarToolBar(false, false, true, false, false);
                btnExportarPDF.Enabled = true;
                HabilitarCampos(false, false, false, false, false, false, false, false);
                myGrid.BloqueaRenglon(true);
            }

            if (bGeneraAutomatico || bEsAutomatica)
            {
                txtProveedor.Enabled = false;
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false;
                cboPlazosCredito.Enabled = false;
                rdoContado.Enabled = true;
                rdoContado.Enabled = true;

                txtIdFarmacia.Enabled = false;
            }

            bPermitirModificarRenglones = bHabilitada; 

            //if (bEsAutomatica)
            //{                
            //    txtProveedor.Enabled = false;
            //    cboEmpresas.Enabled = false;
            //    cboEstados.Enabled = false;
            //}        


            //// Ejecutar siempre al final 
            ObtenerURL_Almacen();
            CargarDiasDePlazo();
            cboPlazosCredito.Data = sDiasDePlazo;
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLeer.DataSetClase = Consultas.Folio_OrdenCompra_CodigosEAN_Det(sEmpresa, sEstado, sFarmacia, txtPedido.Text.Trim(), "CargaDetallesFolio()");
            for (int iRow = 1; myLeer.Leer(); iRow++)
            {
                bRegresa = true;
                //myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);

                myGrid.Rows = iRow;
                myGrid.SetValue(iRow, (int)Cols.IdProducto, myLeer.Campo("IdProducto"));
                myGrid.SetValue(iRow, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                myGrid.SetValue(iRow, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                myGrid.SetValue(iRow, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                myGrid.SetValue(iRow, (int)Cols.Cont_Paquete, myLeer.Campo("ContenidoPaquete"));

                myGrid.SetValue(iRow, (int)Cols.AplicaCosto, myLeer.Campo("AplicaCosto"));
                myGrid.SetValue(iRow, (int)Cols.PrecioBase, myLeer.Campo("Precio"));
                myGrid.SetValue(iRow, (int)Cols.Precio, myLeer.Campo("Precio"));

                myGrid.SetValue(iRow, (int)Cols.Descuento, myLeer.Campo("Descuento"));
                myGrid.SetValue(iRow, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                myGrid.SetValue(iRow, (int)Cols.Cantidad, myLeer.Campo("CantidadCajas"));
                myGrid.SetValue(iRow, (int)Cols.PrecioUnitario, myLeer.Campo("PrecioCaja"));
                myGrid.SetValue(iRow, (int)Cols.Importe, myLeer.CampoDouble("Importe"));
                myGrid.SetValue(iRow, (int)Cols.Iva, myLeer.CampoDouble("Iva"));
                myGrid.SetValue(iRow, (int)Cols.ImporteTotal, myLeer.CampoDouble("ImporteTotal"));

                if (myLeer.CampoBool("TienePrecioNegociado"))
                {
                    myGrid.SetValue(iRow, (int)Cols.ComisionNegociadora, myLeer.Campo("Precio"));
                }

                CargarPrecioLicitado(iRow, myLeer.Campo("CodigoEAN"));
            }

            ///// Bloquear grid completo
            CalcularTotalImporte();
            myGrid.BloqueaColumna(true, (int)Cols.CodigoEAN); 

            //myGrid.BloqueaRenglon(bHabilitar);

            return bRegresa;
        }

        private void HabilitarCampos(bool Orden, bool Proveedor, bool Estado, bool EntregarEn, bool DiasDePlazo, bool Observaciones, bool FechaReq, bool CambiarDestino)
        {
            txtPedido.Enabled = Orden;
            cboEmpresas.Enabled = Estado;
            txtProveedor.Enabled = Proveedor;
            cboEstados.Enabled = Estado;
            txtIdFarmacia.Enabled = EntregarEn;
            txtObservaciones.Enabled = Observaciones;
            chkDerivaDeOrdenDeCompra.Enabled = Observaciones;
            txtOrdenDeCompraVinculada.Enabled = Observaciones; 
            dtpFechaRequeridaEntrega.Enabled = FechaReq;
            cboPlazosCredito.Enabled = DiasDePlazo; 

            btnCambiarDestino.Enabled = CambiarDestino;
            if (CambiarDestino) txtIdFarmacia.Enabled = CambiarDestino;

            if(tpTipoProceso == ProcesosOrdenDeCompra.Consulta)
            {
                btnCambiarDestino.Enabled = false;
                btnCambiarDestino.Visible = false;
            }
        }

        #endregion Buscar Pedido

        #region Guardar Informacion
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = true;
            f = new FrmOrdenCompraCodigoEANListadoDeExcedentes();
            bTieneSobrePrecio = false;

            //GnCompras.GenerarNuevoGUID();
            //sTabla = "Temp_" + GnCompras.GUID;

            EliminarRenglonesVacios();
            CalcularTotalImporte();
            if (ValidaDatos())
            {
                if (DtGeneral.ConfirmacionConHuellas)
                {
                    if (txtPedido.Text != "*")
                    {
                        if (VerificarCambioDePrecio())
                        {
                            sMsjNoEncontrado = "El Usuario no tiene permisos para cambiar precios en orden de compra, verifique por favor.";
                            ////bContinua = opPermisosEspeciales.VerificarPermisos("CAMBIAR_PRECIOS_ORD_COMP", sMsjNoEncontrado);
                            bContinua = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("CAMBIAR_PRECIOS_ORD_COMP", sMsjNoEncontrado);
                        }
                    }
                }

                if (txtPedido.Text == "*")
                {
                    if (!bGeneraAutomatico)
                    {
                        GnCompras.GenerarNuevoGUID();
                        sTabla = "Temp_" + GnCompras.GUID;

                        bContinua = CalcularPromedio();
                    }
                }

                if (bContinua)
                {
                    bContinua = VerificarSobrePrecio();
                }

                if (bContinua)
                {
                    if (!ConexionLocal.Abrir())
                    {
                        General.msjAviso(General.MsjErrorAbrirConexion);
                    }
                    else 
                    {
                        ConexionLocal.IniciarTransaccion();

                        if (bContinua)
                        {
                            bContinua = GrabarEncabezado(1);
                        }

                        if (bContinua && bTieneSobrePrecio)
                        {
                            bContinua = GuardarSobrePrecio(sFolioPedido);
                        }

                        if (bContinua)
                        {
                            bContinua = GuardarExcedentes(sFolioPedido);
                        }

                        if (bContinua)
                        {
                            bContinua = GrabarDetalle();
                        }

                        if (bContinua && bGeneraAutomatico)
                        {
                            if (GuardaExcepcionesPrecios())
                            {
                                bContinua = ActualizaCantidadesPedido();
                            }
                            else
                            {
                                bContinua = false;
                            }
                        }

                        if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                        {
                            txtPedido.Text = sFolioPedido;
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            IniciarToolBar(false, false, true, false, false);
                            Imprimir(true);
                            btnNuevo_Click(null, null);
                            if (bGeneraAutomatico)
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            Error.GrabarError(myLeer, "btnGuardar_Click");
                            ConexionLocal.DeshacerTransaccion();
                            General.msjError("Ocurrió un error al guardar la Información.");

                        }

                        ConexionLocal.Cerrar();
                    }
                }

                //// Limpiar las tablas temporales 
                Borrar_Tablas_De_Procesos(); 
            }

        }

        private bool GuardarExcedentes(string FolioPedido)
        {
            bool bcontinua = true;
            for (int i = 1; i <= f.myGrid.Rows && bcontinua; i++)
            {

                string sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_Excedente '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, FolioPedido, DtGeneral.IdPersonal,
                    f.myGrid.GetValue(i, (int)ColsExcedentes.IdClaveSSA), f.myGrid.GetValueDou(i, (int)ColsExcedentes.Porcentaje),
                    f.myGrid.GetValue(i, (int)ColsExcedentes.Observaciones), 1);

                if (!myLeer.Exec(sSql))
                {
                    bcontinua = false;
                }
            }
            return bcontinua;
        }

        private bool GuardarSobrePrecio(string FolioPedido)
        {
            bool bRegresa = true;

            string sSql;

            sSql = string.Format("Update COM_OCEN_SobrePrecioEnc Set Status = 'C' Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioOrden = '{3}' ",
                                  sEmpresa, sEstado, sFarmacia, FolioPedido);
            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
            {
                if (myGrid.GetValueDou(iRow, (int)Cols.PorcentajeSobreLicitado) > 0.0000 && myGrid.GetValueInt(iRow, (int)Cols.Cantidad) > 0)
                {
                    clsMotivosSobreCompra[] ListaMotivosEnc = Motivos.MotivosEnc(myGrid.GetValue(iRow, (int)Cols.IdProducto), myGrid.GetValue(iRow, (int)Cols.CodigoEAN));
                    clsMotivosSobreCompra[] ListaMotivosDet = Motivos.MotivosDet(myGrid.GetValue(iRow, (int)Cols.IdProducto), myGrid.GetValue(iRow, (int)Cols.CodigoEAN));

                    foreach (clsMotivosSobreCompra L in ListaMotivosEnc)
                    {
                        sSql = String.Format(" Exec spp_COM_OCEN_ObservacionSobrePrecioEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, '{8}' ",
                                                sEmpresa, sEstado, sFarmacia, FolioPedido, L.IdProducto, L.CodigoEAN,
                                                L.PrecioCaja, L.PrecioReferencia, L.Observaciones);
                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            break;
                        }
                    }

                    foreach (clsMotivosSobreCompra L in ListaMotivosDet)
                    {
                        sSql = String.Format(" Exec spp_COM_OCEN_ObservacionSobrePrecioDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                                                sEmpresa, sEstado, sFarmacia, FolioPedido, L.IdProducto, L.CodigoEAN,
                                                L.IdMotivoSobrePrecio, L.Status);
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

        private void Borrar_Tablas_De_Procesos()
        {
            string sSql = string.Format("If Exists ( select * From Sysobjects (NoLock) Where Name = '{0}' and xType = 'U' ) Drop Table {0} ", sTabla) ;

            leer.Exec(sSql); 
        }

        private bool CalcularPromedio()
        {
            bool bRegresa = true;
            int i = 0;
            string sSql;

            for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
            {
                sSql = string.Format("Exec spp_COM_OCEN_TablaTrabajoCalculoDePromedioPorOrdenDeCompra '{0}', '{1}','{2}'",
                                myGrid.GetValue(iRow, (int)Cols.CodigoEAN), myGrid.GetValueInt(iRow, (int)Cols.Cantidad), sTabla);

                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
                }
            }

            sSql = string.Format("Exec spp_COM_OCEN_PorcentajeDeCajasPorClaveSSAEnOrdenesDeCompras '{0}', '{1}','{2}', '{3}', '{4}', '{5}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, GnCompras.NumCompras, GnCompras.PorcMaxCompras, sTabla);

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (myLeer.Registros > 0)
                {
                    f.myGrid.LlenarGrid(myLeer.DataSetClase);
                    f.Shows();
                    bRegresa = f.bExito;
                }
            }

            return bRegresa;
        }

        private bool VerificarSobrePrecio()
        {
            bool bRegresa = true;

            for (int iRow = 1; iRow <= myGrid.Rows && bRegresa; iRow++)
            {
                if (myGrid.GetValueDou(iRow, (int)Cols.PorcentajeSobreLicitado) > 0.0000 && myGrid.GetValueInt(iRow, (int)Cols.Cantidad) > 0)
                {
                    bRegresa = CargarMovtosSobreCompra(iRow, myGrid.GetValue(iRow, (int)Cols.IdProducto), myGrid.GetValue(iRow, (int)Cols.CodigoEAN));
                    bTieneSobrePrecio = true;
                }
            }

            return bRegresa;
        } 

        private bool GrabarEncabezado(int iOpcion)
        {
            bool bRegresa = true;
            int iEsAutomatica = 0, iEsCentral = 0, iEsContado = 0;;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                iEsCentral = 1;
            }

            if (rdoContado.Checked)
            {
                iEsContado = 1;
            }
            
            iEsAutomatica = Convert.ToInt16(bGeneraAutomatico);

            string sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_Claves_Enc " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrden = '{3}', @FacturarA = '{4}', @IdProveedor = '{5}', " +
                " @IdPersonal = '{6}', @EstadoEntrega = '{7}', @EntregarEn = '{8}', @FechaRequeridaEntrega = '{9}', @Observaciones = '{10}', " +
                " @TipoOrden = '{11}', @EsAutomatica = '{12}', @FolioPedido = '{13}', @EsCentral = '{14}', @iOpcion = '{15}', @EsContado = '{16}', " +
                " @IdCliente = '{17}', @IdSubCliente = '{18}', @FolioOrdenVinculada = '{19}', @DiasDePLazo = {20} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPedido.Text.Trim(), cboEmpresas.Data,
                txtProveedor.Text, DtGeneral.IdPersonal, cboEstados.Data, txtIdFarmacia.Text, dtpFechaRequeridaEntrega.Text, txtObservaciones.Text,
                iTipoOrden, iEsAutomatica, sFolioPedUnidad, iEsCentral, iOpcion, iEsContado, txtCte.Text, txtSubCte.Text, txtOrdenDeCompraVinculada.Text, cboPlazosCredito.Data); 

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Clave");
                sMensaje = myLeer.Campo("Mensaje");                
            }

            return bRegresa;
        }


        private bool AbrirOrden(int iOpcion)
        {
            bool bRegresa = true;
            int iEsAutomatica = 0, iEsCentral = 0, iEsContado = 0; ;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                iEsCentral = 1;
            }

            if (rdoContado.Checked)
            {
                iEsContado = 1;
            }

            iEsAutomatica = Convert.ToInt16(bGeneraAutomatico);

            string sSql = string.Format("Update COM_OCEN_OrdenesCompra_Claves_Enc Set Status = 'A' " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioOrden = '{3}'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPedido.Text.Trim());

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioPedido = myLeer.Campo("Clave");
                sMensaje = "Folio abierto.";
            }

            return bRegresa;
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Delete From COM_OCEN_OrdenesCompra_CodigosEAN_Det   " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioOrden = '{3}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido); 

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                bRegresa = GrabarDetalle_Productos(); 
            }

            return bRegresa; 
        }

        private bool GrabarDetalle_Productos()
        {
            bool bRegresa = true;
            string sSql = "";
            string sIdProducto = "", sCodigoEAN = "", sObservacionesSobreCompra = "", ObservacionesPrecio = "";
            int iCantidad = 0, iCantidadSobreCompra = 0, iCant_Actual = 0, iTienePrecioNegociado = 0;
            double dPrecio = 0, dDescuento = 0, dTasaIva = 0, dIva = 0, dImporte = 0, dPrecioUnitario = 0, dComisionNegociadora = 0;
            int iAplicaCosto = 0; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdProducto = myGrid.GetValue(i, (int)Cols.IdProducto);
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodigoEAN);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                iCant_Actual = myGrid.GetValueInt(i, (int)Cols.Cant_Actual);
                iCantidadSobreCompra = myGrid.GetValueInt(i, (int)Cols.Cant_SobreCompra);
                dPrecio = myGrid.GetValueDou(i, (int)Cols.Precio);
                dDescuento = myGrid.GetValueDou(i, (int)Cols.Descuento);
                dTasaIva = myGrid.GetValueDou(i, (int)Cols.TasaIva);
                dIva = myGrid.GetValueDou(i, (int)Cols.Iva);
                dPrecioUnitario = myGrid.GetValueDou(i, (int)Cols.PrecioUnitario);
                dImporte = myGrid.GetValueDou(i, (int)Cols.Importe);
                ObservacionesPrecio = myGrid.GetValue(i, (int)Cols.ObservacionesCambio);

                dComisionNegociadora = myGrid.GetValueDou(i, (int)Cols.ComisionNegociadora);
                iAplicaCosto = Convert.ToInt32(myGrid.GetValueBool(i, (int)Cols.AplicaCosto));


                if (sIdProducto != "")
                {
                    if (bEsAutomatica)
                    {
                        if (iCantidadSobreCompra > 0 && (iCantidad - iCant_Actual) == 0)
                        {
                            iCantidad = iCantidadSobreCompra; 
                            iCantidadSobreCompra = iCantidad;
                        }
                        else
                        {
                            iCantidadSobreCompra = iCantidad - iCant_Actual;
                        }
                        sObservacionesSobreCompra = myGrid.GetValue(i, (int)Cols.ObservacionesSobreCompra);
                    }

                    sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det " +
                        " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrden = '{3}', " + 
                        " @Partida = '{4}', @IdProducto = '{5}', @CodigoEAN = '{6}', @AplicaCosto = '{7}', @Cantidad = '{8}', @Precio = '{9}', @Descuento = '{10}', " + 
                        " @TasaIva = '{11}', @Iva = '{12}', @PrecioUnitario = '{13}', @Importe = '{14}', @CantidadSobreCompra = '{15}', " + 
                        " @ObservacionesSobreCompra = '{16}', @IdPersonalRegistra = '{17}', @Observaciones = '{18}', @dComisionNegociadora = '{19}' " + 
                        "  \n \n ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido,
                        i, sIdProducto, sCodigoEAN, iAplicaCosto, iCantidad, dPrecio, dDescuento, 
                        dTasaIva, dIva, dPrecioUnitario, dImporte, iCantidadSobreCompra, 
                        sObservacionesSobreCompra, sIdPersonalRegistra, ObservacionesPrecio, dComisionNegociadora);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        #endregion Guardar Informacion

        #region Eliminar Informacion 
        private bool validarRecepciones()
        {
            bool bRegresa = true;
            
            FrmValidarRecepcionOrdenDeCompra f = new FrmValidarRecepcionOrdenDeCompra();
            bRegresa = f.ValidarRecepcion(sUrlAlmacen, cboEmpresas.Data, cboEstados.Data, txtIdFarmacia.Text, txtPedido.Text);             

            return bRegresa; 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {           
            bool bRegresa = false;
            int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            string message = "¿ Desea cancelar el Folio seleccionado ?";


            if (General.msjCancelar(message) == DialogResult.Yes)
            {
                bRegresa = validarRecepciones(); 
            }


            if ( bRegresa ) 
            {
                if (!ConexionLocal.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion); 
                }
                else 
                {
                    ConexionLocal.IniciarTransaccion();

                    bRegresa = GrabarEncabezado(iOpcion);

                    if (bRegresa)
                    { 
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCancelar_Click");
                        General.msjError("Ocurrió un error al cancelar el Folio.");
                        //btnNuevo_Click(null, null);
                    }

                    ConexionLocal.Cerrar();
                }
            }            
        }
        #endregion Eliminar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            Imprimir(false, true);
        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información en pantalla ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtPedido.Text.Trim() == "" || txtPedido.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de pedido inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            Imprimir(Confirmacion, false); 
        }

        private void Imprimir(bool Confirmacion, bool Exportar)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sNombre = "INT-OC-" + txtPedido.Text.Trim() + ".pdf";


                if (DtGeneral.EmpresaConectada == "002")
                {
                    sNombre = "PHF-OC-" + txtPedido.Text.Trim() + ".pdf";
                }

                if (DtGeneral.EmpresaConectada == "004")
                {
                    sNombre = "PHJ-OC-" + txtPedido.Text.Trim() + ".pdf";
                }


                myRpt.RutaReporte = GnCompras.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", Fg.PonCeros(txtPedido.Text, 8));
                myRpt.NombreReporte = "COM_OrdenDeCompra_CodigosEAN";

                //bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                if (Exportar)
                {
                    bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                }
                else
                {
                    // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat); 
                    bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 
                }

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Eventos 
        private void txtIdFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblEntregarEn.Text = "";
            lblDomicilio.Text = "";
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtIdFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdFarmacia.Text != "")
            {
                if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
                {
                    myLeer.DataSetClase = Consultas.AlmacenesCompras(cboEstados.Data, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                }
                else
                {
                    myLeer.DataSetClase = Consultas.AlmacenesCompras(cboEstados.Data, txtIdFarmacia.Text.Trim(), "txtIdFarmacia_Validating");
                }

                if (myLeer.Leer())
                {
                    CargarDatosDeFarmacia();
                }
                else
                {
                    txtIdFarmacia.Text = "";                    
                    lblEntregarEn.Text = "";
                    lblDomicilio.Text = "";
                    txtIdFarmacia.Focus();
                }
            }
        }
        #endregion Eventos

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (myGrid.ActiveCol == (int)Cols.CodigoEAN)
            {
                if (e.KeyCode == Keys.F1)
                {
                    //myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
                    myLeer.DataSetClase = ayuda.Productos_Proveedor( txtProveedor.Text, "grdProductos_KeyDown");
                    if (myLeer.Leer())
                    {
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                        //CargaDatosSal();
                        ObtenerDatosProducto();
                    }

                }

                if (e.KeyCode == Keys.Delete)
                {
                    if (bPermitirModificarRenglones)
                    {
                        if (!bEsAutomatica)
                        {
                            myGrid.DeleteRow(myGrid.ActiveRow);

                            if (myGrid.Rows == 0)
                            {
                                myGrid.Limpiar(true);
                            }
                        }
                    }
                }
            }
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if (lblStatus.Visible == false)
            {
                if (btnGuardar.Enabled)
                {
                    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
                    {
                        if (myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodigoEAN) != "" && myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Descripcion) != "")
                        {
                            myGrid.Rows = myGrid.Rows + 1;
                            myGrid.ActiveRow = myGrid.Rows;
                            myGrid.SetActiveCell(myGrid.Rows, (int)Cols.CodigoEAN);
                        }
                    }
                }
                CalcularTotalImporte();
            }
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodigoEAN); 
        }

        private void grdProductos_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            switch (myGrid.ActiveCol)
            {
                case (int)Cols.AplicaCosto:
                    ActualizarInformacion_Costos(); 
                    break;
            }
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            int iCantidad = 0, iCant_SobreCompra = 0;
            double dImporte = 0;
            double dIVA = 0;
            double dTotal = 0;


            switch (myGrid.ActiveCol)
            {
                case (int)Cols.CodigoEAN:
                    {
                        ObtenerDatosProducto();
                    }
                    break;

                case (int)Cols.AplicaCosto:
                case (int)Cols.Cantidad:
                case (int)Cols.PrecioLicitado:
                case (int)Cols.PrecioUnitario:
                    {
                        iCantidad = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cantidad);
                        if (bEsAutomatica)
                        {
                            iCant_SobreCompra = myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.Cant_SobreCompra);
                            if ((iCantidad - iCant_SobreCompra) > 0)
                            {
                                myGrid.BloqueaCelda(false, myGrid.ActiveRow, (int)Cols.ObservacionesSobreCompra);
                                myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.ObservacionesSobreCompra);
                            }
                            else
                            {
                                myGrid.BloqueaCelda(true, myGrid.ActiveRow, (int)Cols.ObservacionesSobreCompra);
                                if ((iCantidad - iCant_SobreCompra) < 0)
                                {
                                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ObservacionesSobreCompra, "");
                                }
                            }
                        }

                        dImporte = iCantidad * myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.PrecioUnitario);
                        dImporte = iCantidad * myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.Precio);

                        dIVA =  Convert.ToDouble( Convert.ToDecimal(dImporte) * Convert.ToDecimal( myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.TasaIva) / 100.0) );
                        dTotal = dImporte + dIVA;

                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Importe, dImporte);
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Iva, dIVA);
                        myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ImporteTotal, dTotal);

                    }

                    break;
            }

            CalcularTotalImporte(); 
        }

        private void limpiarColumnas()
        {
            for (int i = 1; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
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

        private void ActualizarInformacion_Costos()
        {
            int iRenglon = myGrid.ActiveRow;
            double dCostoBase = myGrid.GetValueDou(iRenglon, (int)Cols.PrecioBase);
            double dCosto_A_Cargar = 0;
            double dCantidad = myGrid.GetValueDou(iRenglon, (int)Cols.Cantidad);

            ////if ( myGrid.GetValueBool(iRenglon, (int)Cols.AplicaCosto) )
            ////{
            ////}

            dCosto_A_Cargar = myGrid.GetValueBool(iRenglon, (int)Cols.AplicaCosto) ? dCostoBase : dCosto_A_Cargar;

            myGrid.SetValue(iRenglon, (int)Cols.PrecioUnitario, dCosto_A_Cargar);
            myGrid.SetValue(iRenglon, (int)Cols.Cantidad, dCantidad); 
        }

        private void ObtenerDatosProducto()
        {
            string sCodigo = ""; // , sSql = "";            

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodigoEAN);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Producto no encontrado ó no esta Asignado al Proveedor.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                //sSql = string.Format(" Select S.ClaveSSA, S.IdClaveSSA_Sal, S.Descripcion, L.Precio, " + 
                //         " L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, 0 As Cantidad, 0.0 As Importe " +
                //         " From CatClavesSSA_Sales S (NoLock) " +
                //         " Inner Join COM_OCEN_ListaDePrecios_Claves L (NoLock) " +
                //            " On( S.IdClaveSSA_Sal = L.IdClaveSSA ) Where L.IdClaveSSA = '{0}' ", Fg.PonCeros(sCodigo, 4));

                //if (!myLeer.Exec(sSql))
                //{
                //    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                //    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                //}
                //else
                //{
                myLeer.DataSetClase = Consultas.Productos_Proveedor(txtProveedor.Text, sCodigo, iValidarClaveEnPerfilDeOperacion, iValidarClaveEnPerfilDeComprador, "ObtenerDatosProducto()");
                if (!myLeer.Leer())
                {
                    //General.msjUser("Producto no encontrado ó no esta Asignado al Proveedor.");
                    myGrid.LimpiarRenglon(myGrid.ActiveRow);
                }
                else
                {
                    CargaDatosProducto(); 
                }
                //}
           }
            
        }        

        private void CargaDatosProducto()
        {
            double dComisionNegociadora = 0;
            double pPrecio = 0;

            int iRowActivo = myGrid.ActiveRow;
            bool bCargarInformacion = true;
            string sValorBuscar = myLeer.Campo("CodigoEAN"); // +Convert.ToInt32(myGrid.GetValueBool(iRowActivo, (int)Cols.AplicaCosto)).ToString() + "1";
            int[] Columnas = { (int)Cols.CodigoEAN} ; ////{ (int)Cols.CodigoEAN, (int)Cols.AplicaCosto };
            int iResultado = 0;
            double dCosto_A_Cargar = 0;
            bool bAgregarProducto = true;
            int iAplicarCosto = 1; 

            if (lblStatus.Visible == false)
            {
                iResultado = myGrid.BuscarRepetidos(sValorBuscar, (int)Cols.CodigoEAN);

                if (iResultado >= 2)
                {
                    iAplicarCosto = 0;
                    bAgregarProducto = General.msjConfirmar("El producto ya se encuentra capturado, ¿ desea agregarlo nuevamente ? ") == System.Windows.Forms.DialogResult.Yes; 
                }

                if (!bAgregarProducto) // myLeer.Campo("CodigoEAN"), iRowActivo))
                {
                    ////General.msjUser("Este Producto ya se encuentra capturado en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, (int)Cols.CodigoEAN);
                }
                else 
                {
                    if (iValidarClaveEnPerfilDeOperacion == 1)
                    {
                        if ( !myLeer.CampoBool("ProductoParaCompra") ) 
                        {
                            bCargarInformacion = false;
                            General.msjUser("El Código EAN seleccionado no esta disponible para compras en esta operación.");
                        }
                    }

                    if (iValidarClaveEnPerfilDeComprador == 1)
                    {
                        if (!myLeer.CampoBool("ProductoParaComprador"))
                        {
                            bCargarInformacion = false;
                            General.msjUser("El Código EAN seleccionado no esta disponible para el Comprador.");
                        }
                    }

                    if (bCargarInformacion)
                    {
                        myGrid.SetValue(iRowActivo, (int)Cols.IdProducto, myLeer.Campo("IdProducto"));
                        myGrid.SetValue(iRowActivo, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                        myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));

                        dCosto_A_Cargar = iAplicarCosto == 1 ? myLeer.CampoDouble("PrecioUnitario") : 0;
                        myGrid.SetValue(iRowActivo, (int)Cols.PrecioBase, myLeer.Campo("PrecioUnitario"));  
                        myGrid.SetValue(iRowActivo, (int)Cols.AplicaCosto, iAplicarCosto);

                        myGrid.SetValue(iRowActivo, (int)Cols.Cont_Paquete, myLeer.Campo("ContenidoPaquete"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Precio, myLeer.Campo("Precio"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Descuento, myLeer.Campo("Descuento"));
                        myGrid.SetValue(iRowActivo, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Iva, myLeer.Campo("Iva"));
                        myGrid.SetValue(iRowActivo, (int)Cols.PrecioUnitario, dCosto_A_Cargar); //myLeer.Campo("PrecioUnitario"));
                        myGrid.SetValue(iRowActivo, (int)Cols.ComisionNegociadora, myLeer.Campo("ComisionNegociadora"));
                        myGrid.SetValue(iRowActivo, (int)Cols.Importe, myLeer.Campo("Importe"));
                        myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.IdProducto);
                        myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                        
                        CargarPrecioLicitado(iRowActivo, myLeer.Campo("CodigoEAN"));

                        dComisionNegociadora = myLeer.CampoDouble("ComisionNegociadora");
                        pPrecio = myLeer.CampoDouble("Precio");

                        if (dComisionNegociadora > 0)
                        {
                            myGrid.ColorCelda(iRowActivo, (int)Cols.ComisionNegociadora, Color.Blue);
                        }

                        if (pPrecio < dComisionNegociadora)
                        {
                            myGrid.ColorCelda(iRowActivo, (int)Cols.ComisionNegociadora, Color.Red);
                        }
                    }
                }
            }
        }

        private void CargarPrecioLicitado(int iRow, string CodigoEAN) 
        {
            string sSql = string.Format("Select dbo.fg_PrecioMinLicitacion__EstadoCliente('{0}', '{1}', '{2}', '{3}' ) As PrecioCaja", 
                CodigoEAN, cboEstados.Data, txtCte.Text, txtSubCte.Text ); 
            if (!myLlenaDatos.Exec(sSql))
            {
                Error.GrabarError(myLlenaDatos, "CargarPrecioLicitado()");
                General.msjError("Ocurrió un error al obtener el Precio Licitado Menor del Estado.");
            }
            else
            {
                if (myLlenaDatos.Leer())
                {
                    myGrid.SetValue(iRow, (int)Cols.PrecioLicitado, myLlenaDatos.CampoDouble("PrecioCaja"));

                    if (myGrid.GetValueDou(iRow, (int)Cols.PorcentajeSobreLicitado) > 0.0000)
                    {
                        myGrid.ColorCelda(iRow, (int)Cols.PrecioLicitado, Color.Red);
                        grdProductos.ForeColor = Color.Red;
                    }
                    else
                    {
                        myGrid.ColorCelda(iRow, (int)Cols.PrecioLicitado, Color.White);
                    }
                }
            }
        }
        #endregion Grid 

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtPedido.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de pedido inválido, verifique.");
                txtPedido.Focus();
            }

            if (bRegresa && cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado A quien se va a Facturar, verifique.");
                cboEmpresas.Focus();
            }

            if (bRegresa && txtProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Proveedor inválido, verifique.");
                txtProveedor.Focus();
            }

            if (bRegresa && cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && txtIdFarmacia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado donde se Entregará la Orden de compra, verifique.");
                txtIdFarmacia.Focus();
            }

            if (bRegresa)
            {
                if (!rdoContado.Checked && !rdoCredito.Checked)
                {
                    bRegresa = false;
                    General.msjUser("No ha seleccionado la Forma de Pago, verifique."); 
                    FramePago.Focus();
                }
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciones, verifique.");
                txtObservaciones.Focus();
            }

            if (bRegresa && chkDerivaDeOrdenDeCompra.Checked)
            {
                if (txtOrdenDeCompraVinculada.Text == "")
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el Folio de Orden de Compra vinculada, verifique."); 
                    txtOrdenDeCompraVinculada.Focus();
                }
            }

            if (bRegresa)
            {
                bRegresa = validarCapturaProductos();
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
                    ////if ( int.Parse( lblUnidades.Text ) == 0 )
                    ////{
                    ////    bRegresa = false;
                    ////}
                    ////else
                    {
                        for (int i = 1; i <= myGrid.Rows; i++)
                        {
                            if (myGrid.GetValueInt(i, (int)Cols.Cantidad) == 0)
                            {
                                bRegresa = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un Producto para la Orden de Compra\n y/o capturar cantidades para al menos un Producto, verifique.");
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Buscar_Proveedor
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                if (!ProveedorSancionado())
                {
                    myLeer = new clsLeer(ref ConexionLocal);
                    myLeer.DataSetClase = Consultas.Proveedores(txtProveedor.Text, "txtProveedor_Validating");
                    if (myLeer.Leer())
                    {
                        txtProveedor.Enabled = false;
                        txtProveedor.Text = myLeer.Campo("IdProveedor");
                        lblNomProv.Text = myLeer.Campo("Nombre");
                        CargarDiasDePlazo();
                    }

                    if (bGeneraAutomatico)
                    {
                        txtProveedor.Enabled = false;
                        txtIdFarmacia.Focus();
                    }
                }
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Proveedores("txtProveedor_KeyDown");
                if(myLeer.Leer())
                {
                    txtProveedor.Enabled = false;
                    txtProveedor.Text = myLeer.Campo("IdProveedor");
                    lblNomProv.Text = myLeer.Campo("Nombre");
                    CargarDiasDePlazo();
                }
                if (bGeneraAutomatico)
                {
                    txtProveedor.Enabled = false;
                }
            }
        }
        #endregion Buscar_Proveedor

        #region Funciones

        private void CargarDiasDePlazo()
        {
            string sSql = string.Format("Select  Dias, Status, Predeterminado  " +
                "From CatProveedores_DiasDePlazo (NoLock) Where IdProveedor = '{0}' Order  BY Predeterminado Desc, Dias Desc", txtProveedor.Text);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargaDatos()");
                General.msjError("Ocurrio un error al obetener los dias de plazo");
                cboPlazosCredito.Add("0", "<<Seleccione>>");
            }
            else
            {
                if (myLeer.Leer())
                {
                    cboPlazosCredito.Clear();
                    cboPlazosCredito.Add(myLeer.DataSetClase, true, "Dias", "Dias");
                    cboPlazosCredito.SelectedIndex = 0;
                }
            }
        }

        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.EstadosConFarmacias("CargarEstados"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;
                        
        }

        private void CargarDatosDeFarmacia()
        {
            if (myLeer.Campo("Status_OrdenDeCompra").ToUpper() =="C")
            {
                General.msjAviso("La unidad actualmente está cancelada, no es posible generar Órdenes de Compra.");
                txtIdFarmacia.Text = "";
                lblEntregarEn.Text = "";
                lblDomicilio.Text = "";
            }
            else
            {
                sUrlAlmacen = myLeer.Campo("UrlAlmacen");
                txtIdFarmacia.Text = myLeer.Campo("IdFarmacia");
                lblEntregarEn.Text = myLeer.Campo("Farmacia");
                lblDomicilio.Text = myLeer.Campo("Domicilio") + " " + myLeer.Campo("Colonia") + ", " + myLeer.Campo("Municipio") + " " + myLeer.Campo("Estado");
            }
        }

        private void ObtenerURL_Almacen()
        {
            myLeer.DataSetClase = Consultas.AlmacenesCompras(cboEstados.Data, txtIdFarmacia.Text.Trim(), "ObtenerURL_Almacen()");
            if (myLeer.Leer())
            {
                sUrlAlmacen = myLeer.Campo("UrlAlmacen"); 
            }
        }

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(myLeer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");    

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CalcularTotalImporte()
        {
            double dSubTotal = 0;
            double dIva = 0;
            double dImpteTotal = 0;

            dSubTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            dIva = myGrid.TotalizarColumnaDou((int)Cols.Iva);
            dImpteTotal = dSubTotal + dIva; // myGrid.TotalizarColumnaDou((int)Cols.Importe);


            lblSubTotal.Text = dSubTotal.ToString(sFormato);
            lblIVA.Text = dIva.ToString(sFormato);
            lblImpteTotal.Text = dImpteTotal.ToString(sFormato);
        }

        public void MostrarDetalleOrdenCompra(string Empresa, string Estado, string sFolioOrden, bool bModalForma)
        {
            sEmpresa = Empresa;
            sEstado = Estado;
            txtPedido.Text = sFolioOrden;
            txtPedido_Validating(null, null);
            bModal = bModalForma;
            this.ShowDialog();
        }

        private bool CargarMovtosSobreCompra(int iRow, string IdProducto, string CodigoEAN)
        {
            bool bContinua = true;
            string sCodigo = myGrid.GetValue(iRow, (int)Cols.IdProducto);
            string sCodEAN = myGrid.GetValue(iRow, (int)Cols.CodigoEAN);

            if (Motivos.pdtsMotivosDet != null)
            {
                string sSelect = string.Format("IdProducto = '{0}' and CodigoEAN = '{1}'", sCodigo, sCodEAN);
                DataRow[] DtMovto = Motivos.pdtsMotivosDet.Tables[0].Select(sSelect);

                if (DtMovto.Length != 0)
                {
                    bContinua = false;
                }
            }

            if(bContinua)
            {
                leer.DataSetClase = Consultas.ObservacionMotivos(sEmpresa, sEstado, sFarmacia, txtPedido.Text, sCodigo, sCodEAN, "CargarMovtosSobreCompra()");
                if (Consultas.Ejecuto)
                {
                    if (leer.Leer())
                    {
                        Motivos.PrecioCaja = myGrid.GetValueDou(iRow, (int)Cols.PrecioUnitario);
                        Motivos.PrecioReferencia = myGrid.GetValueDou(iRow, (int)Cols.PrecioLicitado);
                        Motivos.AddMovtos(leer.DataSetClase);
                    }
                }
            }

            bContinua = mostrarOcultarMotivos(iRow);
            return bContinua;
        }

        private bool mostrarOcultarMotivos(int iRow)
        {
            bool bContinua = false;

            if (myGrid.GetValue(iRow, (int)Cols.IdProducto) != "")
            {
                Motivos.IdProducto = myGrid.GetValue(iRow, (int)Cols.IdProducto);
                Motivos.CodigoEAN = myGrid.GetValue(iRow, (int)Cols.CodigoEAN);
                Motivos.Descripcion = myGrid.GetValue(iRow, (int)Cols.Descripcion);

                Motivos.Show();
                bContinua = Motivos.Continuar;
            }
            else
            {
                myGrid.SetActiveCell(iRow, (int)Cols.CodigoEAN);
            }
            return bContinua;
        }

        private bool ProveedorSancionado()
        {
            bool bRegresa = true;

            Consultas.MostrarMsjSiLeerVacio = false;
            myLeer.DataSetClase = Consultas.Proveedores_Sancionados(txtProveedor.Text.Trim(), "ProveedorSancionado");
            Consultas.MostrarMsjSiLeerVacio = true;

            if (!myLeer.Leer())
            {
                bRegresa = false;
            }
            else
            {
                General.msjUser("El proveedor ingresado se encuentra sancionado. Verifique");
                txtProveedor.Text = "";
                lblNomProv.Text = "";
                txtProveedor.Focus();
            }

            return bRegresa;
        }

        private bool VerificarCambioDePrecio()
        {
            bool bRegresa = false;

            for (int i = 1; i <= myGrid.Rows; i++)
            {

                if (myGrid.GetValueDou(i, (int)Cols.Precio) != myGrid.GetValueDou(i, (int)Cols.PrecioActual))
                {
                    bRegresa = true;
                    break;
                }
            }

            return bRegresa;
        }

        //private bool VerificarPermisos()
        //{
        //    bool bRegresa = false;

        //    //FP_General.TablaHuellas = "FP_Huellas_Cedis";
        //    clsVerificarHuella f = new clsVerificarHuella();
        //    f.MostrarMensaje = false;
        //    f.Show();

        //    if (FP_General.HuellaCapturada)
        //    {
        //        if (FP_General.ExisteHuella)
        //        {
        //            sIdPersonalRegistra = FP_General.Referencia_Huella;

        //            bRegresa = DtGeneral.PermisosEspeciales.TienePermisoHuellas(sIdPersonalRegistra, "CAMBIAR_PRECIOS_ORD_COMP");

        //            if (!bRegresa)
        //            {
        //                General.msjAviso("El Usuario no tiene permisos para cambiar precios en orden de compra, verifique por favor.");
        //            }

        //        }
        //    }

        //    return bRegresa;
        //}
        #endregion Funciones

        #region Colocar_OrdenCompra
        private void btnCerrarOrden_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iOpcion = 3; //La opcion 3 indica que se cierra la orden para que sea colocada
            string message = "¿ Desea Cerrar la Orden de Compra ?";

            if (General.msjConfirmar(message) == DialogResult.Yes)
            {
                if (ConexionLocal.Abrir())
                {
                    ConexionLocal.IniciarTransaccion();

                    bRegresa = GrabarEncabezado(iOpcion);

                    if (bRegresa)
                    {
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        Imprimir(false);
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnCerrarOrden_Click");
                        General.msjError("Ocurrió un error al cerrar la Orden de Compra.");                        
                    }

                    ConexionLocal.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }
        private void btnAbrirOrden_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iOpcion = 1;
            string message = "¿ Desea Abrir la Orden de Compra ?";


            if (General.msjConfirmar(message) == DialogResult.Yes)
            {
                bRegresa = validarRecepcionesAbrir();

                if (bRegresa)
                {
                    if (ConexionLocal.Abrir())
                    {
                        ConexionLocal.IniciarTransaccion();

                        bRegresa = AbrirOrden(iOpcion);

                        if (bRegresa)
                        {
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP
                            Imprimir(false);
                            btnNuevo_Click(null, null);
                        }
                        else
                        {
                            ConexionLocal.DeshacerTransaccion();
                            Error.GrabarError(myLeer, "btnCerrarOrden_Click");
                            General.msjError("Ocurrió un error al abrir la Orden de Compra.");
                        }

                        ConexionLocal.Cerrar();
                    }
                    else
                    {
                        General.msjAviso(General.MsjErrorAbrirConexion);
                    }
                }
            }

        }

        private bool validarRecepcionesAbrir()
        {
            bool bRegresa = true;
            bool bRecepcionConfirmada = false; 

            string sMensaje = "Se encontraron registros de ingreso de la Orden de Compra, no es posible realizar la operación.";

            FrmValidarRecepcionOrdenDeCompra f = new FrmValidarRecepcionOrdenDeCompra();
            bRegresa = f.ValidarRecepcion(sUrlAlmacen, cboEmpresas.Data, cboEstados.Data, txtIdFarmacia.Text, txtPedido.Text, sMensaje);
            bRecepcionConfirmada = bRegresa;

            if (!bRegresa)
            {
                if (DtGeneral.EsEquipoDeDesarrollo)
                {
                    bRegresa = General.msjConfirmar("No fue posible validar la recepción de la Orden de Compra, \n" + 
                        "¿ Desea abrir la Orden de Compra de forma local ?") == DialogResult.Yes;
                }
            }

            if (bRecepcionConfirmada)
            {
                sMensaje = "No se pudo eliminar registro, no es posible realizar la operación.";

                f = new FrmValidarRecepcionOrdenDeCompra();
                bRegresa = f.EliminarRegistros(sUrlAlmacen, cboEmpresas.Data, cboEstados.Data, txtIdFarmacia.Text, txtPedido.Text, sMensaje);
            }

            return bRegresa;
        }

        #endregion Colocar_OrdenCompra

        #region OrdenCompra_Automatica
        public void GenerarOrdenDeCompra_Automatica(string EmpresaRegional, string EstadoRegional, string IdProveedor, bool bGeneraAutomatica, string FolioPedido, string Unidad)
        {
            string sGUID = "";
            string sSql = "";

            sGUID = GnCompras.GUID;
            sEmpresaRegional = EmpresaRegional;
            sEstadoRegional = EstadoRegional;

            sEmpresa = EmpresaRegional;
            sEstado = EstadoRegional;
            sUnidad = Unidad;

            sSql = string.Format(" Select P.CodigoEAN, P.IdProducto, P.Descripcion, P.ContenidoPaquete, L.Precio, L.Descuento, L.TasaIva, " +
                                " L.Iva, L.PrecioUnitario, T.Cant_A_Pedir, 0.0 As Importe, T.FolioPedido " +
                                " From vw_Productos_CodigoEAN P (NoLock) " +
                                " Inner Join COM_OCEN_ListaDePrecios L (NoLock) On( P.CodigoEAN = L.CodigoEAN ) " +
                                " Inner Join Com_Pedidos_Compras T (Nolock) On ( L.CodigoEAN = T.CodigoEAN and L.IdProveedor = T.IdProveedor ) " +
                                " Where T.GUID = '{0}' And T.FolioPedido = {1} And L.IdProveedor = '{2}' And T.Cant_A_Pedir > 0 ", sGUID, FolioPedido, IdProveedor);

            if (!myLeer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al Obtener los Productos del Proveedor.");
            }
            else
            {
                myGrid.Limpiar(false);
                if (myLeer.Registros > 0)
                {
                    bGeneraAutomatico = bGeneraAutomatica;
                    txtProveedor.Text = IdProveedor;
                    cboEmpresas.Data = sEmpresaRegional;
                    cboEmpresas.Enabled = false;
                    cboEstados.Data = sEstadoRegional;
                    cboEstados.Enabled = false;
                    txtIdFarmacia.Text = sUnidad;

                    //myGrid.LlenarGrid(myLeer.DataSetClase);
                    for (int iRow = 1; myLeer.Leer(); iRow++)
                    {
                        myGrid.Rows = iRow;
                        myGrid.SetValue(iRow, (int)Cols.IdProducto, myLeer.Campo("IdProducto"));
                        myGrid.SetValue(iRow, (int)Cols.CodigoEAN, myLeer.Campo("CodigoEAN"));
                        myGrid.SetValue(iRow, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                        myGrid.SetValue(iRow, (int)Cols.Cont_Paquete, myLeer.Campo("ContenidoPaquete"));
                        myGrid.SetValue(iRow, (int)Cols.Precio, myLeer.Campo("Precio"));
                        myGrid.SetValue(iRow, (int)Cols.Descuento, myLeer.Campo("Descuento"));
                        myGrid.SetValue(iRow, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                        myGrid.SetValue(iRow, (int)Cols.Cantidad, myLeer.Campo("Cant_A_Pedir"));
                        //myGrid.SetValue(iRow, (int)Cols.Iva, myLeer.Campo("Iva"));
                        myGrid.SetValue(iRow, (int)Cols.PrecioUnitario, myLeer.Campo("PrecioUnitario"));
                        myGrid.SetValue(iRow, (int)Cols.Importe, myLeer.Campo("Importe"));
                        CargarPrecioLicitado(iRow, myLeer.Campo("CodigoEAN"));

                        sFolioPedUnidad = myLeer.Campo("FolioPedido");
                    }
                    
                    CalcularTotalImporte();
                    myGrid.BloqueaGrid(true);
                    //txtProveedor_Validating(null, null);
                    this.ShowDialog();
                }
                else
                {
                    General.msjUser("No se han asignado Claves-CodigoEAN al Proveedor.");
                }
            }
        }

        private bool ActualizaCantidadesPedido()
        {
            bool bRegresa = true;
            string sIdClaveSSA = "", sCodigoEAN = "", sTabla = "";
            int iCantidad_Solicitada = 0, iEsCentral = 0;

            sTabla = GnCompras.GUID;

            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
            {
                iEsCentral = 1;
            }

            for (int i = 1; i <= myGrid.Rows; i++)
            {                
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodigoEAN);                
                iCantidad_Solicitada = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                if (sCodigoEAN != "")
                {
                    string sSql = string.Format("Exec spp_Mtto_COM_OCEN_Pedidos_Proveedor_Actualiza_Cantidades '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', " + 
                            " '{6}', '{7}', '{8}', '{9}', '{10}', '{11}' ",
                                sEmpresaRegional, sEstadoRegional, sFarmacia, DtGeneral.IdPersonal, sIdClaveSSA, sCodigoEAN, Fg.PonCeros(txtProveedor.Text, 4),
                                iCantidad_Solicitada, sTabla, sFolioPedUnidad, iEsCentral, sUnidad);

                    if (!myLeer.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool GuardaExcepcionesPrecios()
        {
            bool bRegresa = true;
            string sGUID = "", sCodigoEAN = "";

            sGUID = GnCompras.GUID;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sCodigoEAN = myGrid.GetValue(i, (int)Cols.CodigoEAN);

                string sSql = string.Format("Exec spp_Mtto_COM_Excepciones_Precios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                            sEmpresa, sEstado, sFarmacia, sFolioPedido, DtGeneral.IdPersonal, Fg.PonCeros(txtProveedor.Text, 4), sGUID, sCodigoEAN);

                if (!myLeer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }

            return bRegresa;
        }
        #endregion OrdenCompra_Automatica

        private void txtIdFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.AlmacenesCompras("txtIdFarmacia_KeyDown", cboEstados.Data);
                if (myLeer.Leer())
                {
                    CargarDatosDeFarmacia();
                    txtObservaciones.Focus();
                }
                else
                {
                    txtIdFarmacia.Text = "";
                    lblEntregarEn.Text = "";
                    lblDomicilio.Text = "";
                    txtIdFarmacia.Focus();
                }
            }
        }

        private void btnModificarPrecio_Click(object sender, EventArgs e)
        {
            if (txtPedido.Text == "" || txtPedido.Text == "*")
            {
                General.msjAviso("El precio unitario solo se puede modificar en un folio previamente generado.");
            }
            else
            {
                FrmCambioDePrecio f = new FrmCambioDePrecio();
                f.dPrecio = myGrid.GetValueDou(myGrid.ActiveRow, (int)Cols.PrecioUnitario);
                f.sObservaciones = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ObservacionesCambio);
                f.ShowDialog();
                if (f.PrecioAsignado)
                {
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.PrecioUnitario, f.dPrecio);
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Precio, f.dPrecio);
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.ObservacionesCambio, f.sObservaciones);
                    CargarPrecioLicitado(myGrid.ActiveRow, myGrid.GetValue(myGrid.ActiveRow, (int)Cols.CodigoEAN));
                }
            }
        }

        #region Clientes y Sub-Clientes 
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Compras_Clientes(cboEstados.Data, "", "txtCte_KeyDown");
                if (!leer.Leer())
                {
                    txtCte.Text = "";
                    txtCte.Focus();
                }
                else
                {
                    CargarInformacion_Cliente();
                }
            }
        }

        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Compras_Clientes(cboEstados.Data, txtCte.Text, "", "txtCte_Validating");
                if (!leer.Leer())
                {
                    txtCte.Text = "";
                    txtCte.Focus(); 
                }
                else
                {
                    CargarInformacion_Cliente(); 
                }
            }
        }

        private void CargarInformacion_Cliente()
        {
            txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente"); 
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Compras_Clientes(cboEstados.Data, txtCte.Text, "txtCte_KeyDown");
                if (!leer.Leer())
                {
                    txtSubCte.Text = "";
                    txtSubCte.Focus();
                }
                else
                {
                    CargarInformacion_SubCliente();
                }
            }
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Compras_Clientes(cboEstados.Data, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (!leer.Leer())
                {
                    txtSubCte.Text = "";
                    txtSubCte.Focus();
                }
                else
                {
                    CargarInformacion_SubCliente();
                }
            }
        }

        private void CargarInformacion_SubCliente()
        {
            txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");
            myGrid.Limpiar(true);
        }
        #endregion Clientes y Sub-Clientes

        private void FrmOrdenCompraCodigoEAN_Base_Shown( object sender, EventArgs e )
        {
            if(lblHide.Visible)
            {
                lblHide.Visible = false;
            }
        }

        private void chkDerivaDeOrdenDeCompra_CheckedChanged(object sender, EventArgs e)
        {
            txtOrdenDeCompraVinculada.Enabled = chkDerivaDeOrdenDeCompra.Checked;
            txtOrdenDeCompraVinculada.Text = !chkDerivaDeOrdenDeCompra.Checked ? "" : txtOrdenDeCompraVinculada.Text;

            if (chkDerivaDeOrdenDeCompra.Checked)
            {
                txtOrdenDeCompraVinculada.Focus(); 
            }
        }

        private void btnCambiarDestino_Click(object sender, EventArgs e)
        {
            string sMensaje = "¿ Desea actualizar el Almacén de Entrega ?";
            string sSql = string.Format("Update E Set EntregarEn = '{3}'  " + 
                "From  COM_OCEN_OrdenesCompra_Claves_Enc E (NoLock)   " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioOrden = '{4}'  ",
                sEmpresa, sEstado, sFarmacia, txtIdFarmacia.Text, txtPedido.Text); 

            if ( General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes) 
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnCambiarDestino_Click");
                    General.msjError("Ocurrió un error al actualizar el Almacén de Entrega");
                }
                else
                {
                    General.msjUser("Información de Almacén de Entrega actualizada satifactoriamente.");
                    LimpiarPantalla(); 
                }
            }
        }
    }
}
