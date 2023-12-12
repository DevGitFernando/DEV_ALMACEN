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
using DllCompras.Seguimiento;

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmOrdenCompraCodigoEAN_Bloqueo : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnAlmacen;
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsLeer leerChecador;
        clsLeer leerAlmacen;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsMotivosSobreCompra Motivos;
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente validarHuella = null;
        DllFarmaciaSoft.wsFarmacia.wsCnnCliente conexionWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        string sFolioPedido = "", sMensaje = "", sValorGrid = "";
        string sFormato = "#,###,###,##0.###0";
        //bool bModal = false;
        bool bEsAutomatica = false, bColocada = false;
        int iTipoOrden = 2;
        bool bPermitirModificarRenglones = true;

        string sUrlAlmacen = "";

        string sMsjNoEncontrado = "";
        string sPersonal = DtGeneral.IdPersonal;

        int iValidarClaveEnPerfilDeOperacion = GnCompras.ValidarClavesEnPerfilOperacion ? 1 : 0;
        int iValidarClaveEnPerfilDeComprador = GnCompras.ValidarClavesEnPerfilDeComprador ? 1 : 0;
        FrmOrdenCompraCodigoEANListadoDeExcedentes f;
        DataSet dtsDatosOC;

        private enum Cols
        {
            Ninguna = 0,
            CodigoEAN = 1, IdProducto = 2, Descripcion = 3, Cont_Paquete = 4, Precio = 5, Descuento = 6, TasaIva = 7,
            Iva = 8, PrecioUnitario = 9, ComisionNegociadora = 10, PrecioLicitado = 11, PorcentajeSobreLicitado = 12,
            Cantidad = 13, Importe = 14, Cant_Actual = 15, Cant_SobreCompra = 16, PrecioActual = 17, ObservacionesCambio = 18, ObservacionesSobreCompra = 19,
            bloqueado = 20, bloquear = 21
        }

        public FrmOrdenCompraCodigoEAN_Bloqueo()
        {
            InitializeComponent();
            dtsDatosOC = new DataSet();

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leer = new clsLeer(ref ConexionLocal);
            leerChecador = new clsLeer(ref cnn);
            validarHuella = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            validarHuella.Url = General.Url;

            conexionWeb = new DllFarmaciaSoft.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;
            myGrid.AjustarAnchoColumnasAutomatico = true; 
            LimpiarPantalla();

            CargarEmpresas();
            CargarEstados();
        }

        private void FrmOrdenCompraCodigoEAN_Devolucion_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        private void txtPedido_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            //IniciarToolBar(false, false, false);            
            string sEsAutom = " MANUAL ";

            lblEsAutomatica.Visible = false;
            myLeer = new clsLeer(ref ConexionLocal);

            if (txtPedido.Text.Trim() != "")
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

            if (bEsAutomatica)
            {
                lblEsAutomatica.Visible = true;
                sEsAutom = " AUTOMATICA ";
                //myGrid.BloqueaColumna(true, (int)Cols.Cantidad, true);
            }

            lblEsAutomatica.Text = sEsAutom;
        }

        #region Limpiar
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir, bool Cerrar)
        {
            btnGuardar.Enabled = Guardar;
            //btnCancelar.Enabled = Cancelar;
            //btnImprimir.Enabled = Imprimir;
            //btnExportarPDF.Enabled = Imprimir;
            //btnCerrarOrden.Enabled = Cerrar;
            btnVerificar.Enabled = Guardar;
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(false);
            bPermitirModificarRenglones = true;
            Fg.IniciaControles();
            IniciarToolBar(false, false, false, false);
            HabilitarCampos(true, false, false, false, false, false);

            //myGrid.BloqueaColumna(true, (int)Cols.ObservacionesSobreCompra);
            //myGrid.BloqueaColumna(false, (int)Cols.Cantidad);
            myGrid.SetActiveCell(1, 1);

            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;
            bColocada = false;
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

            chkDerivaDeOrdenDeCompra.Checked = false;
            txtOrdenDeCompraVinculada.Enabled = false;
            txtPedido.Focus();
            Motivos = new clsMotivosSobreCompra();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }
        #endregion Limpiar

        #region Funciones
        private void CargarEstados()
        {
            cboEstados.Clear();
            cboEstados.Add("0", "<< Seleccione >>");
            cboEstados.Add(Consultas.EstadosConFarmacias("CargarEstados"), true, "IdEstado", "Estado");
            cboEstados.SelectedIndex = 0;

        }

        private void CargarDatosDeFarmacia()
        {
            sUrlAlmacen = myLeer.Campo("UrlAlmacen");
            txtIdFarmacia.Text = myLeer.Campo("IdFarmacia");
            lblEntregarEn.Text = myLeer.Campo("Farmacia");
            lblDomicilio.Text = myLeer.Campo("Domicilio") + " " + myLeer.Campo("Colonia") + ", " + myLeer.Campo("Municipio") + " " + myLeer.Campo("Estado");
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
        #endregion Funciones


        private void CargaEncabezadoFolio()
        {
            bool bHabilitada = true;
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

            txtPedido.Enabled = false;

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
                IniciarToolBar(false, false, true, false);
                lblStatus.Text = "CANCELADA";
                lblStatus.Visible = true;
                //HabilitarCampos(false, false, false, false, false, false);
                bHabilitada = false;
            }

            if (myLeer.Campo("Status") == "OC")
            {
                IniciarToolBar(true, true, true, false);
                lblStatus.Text = "ORDEN COLOCADA";
                lblStatus.Visible = true;
                bColocada = true;
                //HabilitarCampos(false, false, false, false, false, false);
                //bHabilitar = true;
                //bHabilitada = false;
            }

            if (myLeer.Campo("Status") == "A")
            {
                lblStatus.Text = "ORDEN NO COLOCADA";
                lblStatus.Visible = true;
                //HabilitarCampos(false, false, false, false, false, false);
                //bHabilitar = true;
                //bHabilitada = false;
            }

            //if (bHabilitada)
            //{
            //    HabilitarCampos(false, true, false, true, true, true);
            //    IniciarToolBar(true, true, true, true);
            //    //grdProductos.ContextMenuStrip = menuCantidades;
            //    ////lblMensajes.Text = "<< Los Precios de Productos aqui Mostrados son por CAJA >>                                                                                                         << Clic derecho cambiar de precio >>";
            //}

            //if (bModal)
            //{
            //    btnNuevo.Enabled = false;
            //    IniciarToolBar(false, false, true, false);
            //    btnExportarPDF.Enabled = true;
            //    HabilitarCampos(false, false, false, false, false, false);
            //    myGrid.BloqueaRenglon(true);
            //}

            //if (bGeneraAutomatico || bEsAutomatica)
            //{
            //    txtProveedor.Enabled = false;
            //    cboEmpresas.Enabled = false;
            //    cboEstados.Enabled = false;
            //    rdoContado.Enabled = true;
            //    rdoContado.Enabled = true;

            //    txtIdFarmacia.Enabled = false;
            //}

            bPermitirModificarRenglones = bHabilitada;

            //if (bEsAutomatica)
            //{                
            //    txtProveedor.Enabled = false;
            //    cboEmpresas.Enabled = false;
            //    cboEstados.Enabled = false;
            //}        


            //// Ejecutar siempre al final 
            ObtenerURL_Almacen();
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
                myGrid.SetValue(iRow, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                myGrid.SetValue(iRow, (int)Cols.Cont_Paquete, myLeer.Campo("ContenidoPaquete"));
                myGrid.SetValue(iRow, (int)Cols.Precio, myLeer.Campo("Precio"));
                myGrid.SetValue(iRow, (int)Cols.Descuento, myLeer.Campo("Descuento"));
                myGrid.SetValue(iRow, (int)Cols.TasaIva, myLeer.Campo("TasaIva"));
                myGrid.SetValue(iRow, (int)Cols.Cantidad, myLeer.Campo("CantidadCajas"));
                myGrid.SetValue(iRow, (int)Cols.Iva, myLeer.Campo("Iva"));
                myGrid.SetValue(iRow, (int)Cols.PrecioUnitario, myLeer.Campo("PrecioCaja"));
                myGrid.SetValue(iRow, (int)Cols.Importe, myLeer.Campo("Importe"));

                myGrid.SetValue(iRow, (int)Cols.bloqueado, myLeer.CampoBool("CodigoEAN_Bloqueado"));
                myGrid.SetValue(iRow, (int)Cols.bloquear, myLeer.CampoBool("CodigoEAN_Bloqueado"));
                myGrid.BloqueaCelda(myLeer.CampoBool("CodigoEAN_Bloqueado"), iRow, (int)Cols.bloquear);

                if (myLeer.CampoBool("TienePrecioNegociado"))
                {
                    myGrid.SetValue(iRow, (int)Cols.ComisionNegociadora, myLeer.Campo("Precio"));
                }

                CargarPrecioLicitado(iRow, myLeer.Campo("CodigoEAN"));
            }

            ///// Bloquear grid completo
            CalcularTotalImporte();
            //myGrid.BloqueaColumna(true, (int)Cols.CodigoEAN);

            //myGrid.BloqueaRenglon(bHabilitar);
            if (!bColocada)
            {
                myGrid.BloqueaColumna(true, (int)Cols.bloquear);
            }

            return bRegresa;
        }

        private void HabilitarCampos(bool bOrden, bool bProveedor, bool bEstado, bool bEntregarEn, bool bObservaciones, bool bFechaReq)
        {
            txtPedido.Enabled = bOrden;
            cboEmpresas.Enabled = bEstado;
            txtProveedor.Enabled = bProveedor;
            cboEstados.Enabled = bEstado;
            txtIdFarmacia.Enabled = bEntregarEn;
            txtObservaciones.Enabled = bObservaciones;
            chkDerivaDeOrdenDeCompra.Enabled = bObservaciones;
            txtOrdenDeCompraVinculada.Enabled = bObservaciones;
            dtpFechaRequeridaEntrega.Enabled = bFechaReq;
        }

        private void CalcularTotalImporte()
        {
            double dImpteTotal = 0;
            dImpteTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
        }

        private void CargarPrecioLicitado(int iRow, string CodigoEAN)
        {
            string sSql = string.Format("Select dbo.fg_PrecioMinLicitacion__EstadoCliente('{0}', '{1}', '{2}', '{3}' ) As PrecioCaja",
                CodigoEAN, cboEstados.Data, txtCte.Text, txtSubCte.Text);
            if (!myLlenaDatos.Exec(sSql))
            {
                Error.GrabarError(myLlenaDatos, "CargarPrecioLicitado()");
                General.msjError("Ocurrió un error al obtener el Precio Licitado Menor del Estado.");
            }
            else
            {
                if (myLlenaDatos.Leer())
                {
                    myGrid.SetValue(iRow, (int)Cols.PrecioLicitado, myLlenaDatos.Campo("PrecioCaja"));

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

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int iRow = 1; iRow <= myGrid.Rows; iRow++)
            {
                if (!myGrid.GetValueBool(iRow, (int)Cols.bloqueado))
                {
                    myGrid.SetValue(iRow, (int)Cols.bloquear, chkTodos.Checked);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bContinua = true;
            string sSql = "";
            if (ValidaDatos())
            {
                if (!ConexionLocal.Abrir() || !cnnAlmacen.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
                else
                {
                    ConexionLocal.IniciarTransaccion();
                    cnnAlmacen.IniciarTransaccion();

                    for (int i = 1; i <= myGrid.Rows; i++)
                    {
                        if (myGrid.GetValueBool(i, (int)Cols.bloquear) && !myGrid.GetValueBool(i, (int)Cols.bloqueado))
                        {
                            sSql = string.Format("Exec spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det_Bloquear @IdEmpresa ='{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                                    "@FolioOrden = '{3}', @CodigoEAN = '{4}', @IdPersonal_Bloquea = '{5}'",
                                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido,
                                    myGrid.GetValue(i, (int)Cols.CodigoEAN), DtGeneral.IdPersonal);

                            if (!myLeer.Exec(sSql) || !leerAlmacen.Exec(sSql))
                            {
                                bContinua = false;
                                break;
                            }
                        }
                    }

                    if (bContinua) // Si no Ocurrió ningun error se llevan a cabo las transacciones.
                    {
                        txtPedido.Text = sFolioPedido;
                        cnnAlmacen.CompletarTransaccion();
                        ConexionLocal.CompletarTransaccion();
                        General.msjUser("La clave fue bloqueada satisfactoriamente.");
                        btnNuevo_Click(null, null);
                    }
                    else
                    {
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        cnnAlmacen.DeshacerTransaccion();
                        ConexionLocal.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la Información.");
                    }
                }
                ConexionLocal.Cerrar();
                cnnAlmacen.Cerrar();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = false;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValueBool(i, (int)Cols.bloquear) && !myGrid.GetValueBool(i, (int)Cols.bloqueado))
                {
                    bRegresa = true;
                    break;
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe seleccionar al menos un Producto para bloquear, verifique.");
            }

            if (bRegresa)
            {
                bRegresa = validarDatosDeConexion();
            }

            return bRegresa;
        }


        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                //leerWeb = new clsLeerWebExt(sUrlFarmacia, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlAlmacen;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVentaAlmacen));

                //DatosDeConexion.Servidor = sHost;
                
                cnnAlmacen = new clsConexionSQL(DatosDeConexion);
                leerAlmacen = new clsLeer(ref cnnAlmacen);
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                //ActivarControles();
            }

            return bRegresa;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if (ObtenerDatos())
            {
                conexionWeb.Url = sUrlAlmacen;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                //DatosDeConexion.Servidor = sHost;
                cnnAlmacen = new clsConexionSQL(DatosDeConexion);

                FrmVerificarOrdenesCompras VerificarOC = new FrmVerificarOrdenesCompras();
                VerificarOC.Folio_Orden = txtPedido.Text.Trim();
                VerificarOC.MostrarPantalla(dtsDatosOC, DatosDeConexion, sUrlAlmacen, txtIdFarmacia.Text.Trim());
            }
        }

        private bool ObtenerDatos()
        {
            bool bRegresa = true;

            if (dtsDatosOC != null)
            {
                dtsDatosOC = null;
                dtsDatosOC = new DataSet();
            }

            string sSql = string.Format(" Exec spp_VerificarDiferenciasOrdenCompra '{0}', '{1}', '{2}', '{3}'  ",
            cboEmpresas.Data, cboEstados.Data, txtIdFarmacia.Text.Trim(), txtPedido.Text.Trim());

            clsDatosCliente datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ObtenerDatos");           

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrlAlmacen, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb,"ConsultarOrdenCompra()", "");
                General.msjError("Ocurrio un error a obtener los datos.");
                bRegresa = false;
            }
            else
            {
                if (!myWeb.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No existe información.");
                }
                else
                {
                    //dtsDatosOC = myWeb.DataSetClase;
                    dtsDatosOC.Tables.Add(myWeb.Tabla(1).Copy());
                    dtsDatosOC.Tables.Add(myWeb.Tabla(2).Copy());
                }
            }
            return bRegresa;
        }
    }
}
