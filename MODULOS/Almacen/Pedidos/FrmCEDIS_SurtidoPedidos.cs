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
using SC_SolutionsSystem.Reportes;

using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft; 

namespace Almacen.Pedidos
{
    public partial class FrmCEDIS_SurtidoPedidos : FrmBaseExt 
    {
        private enum Cols
        {
            //Ninguna = 0,
            //ClaveSSA = 1,
            //IdClaveSSA = 2, Descripcion = 3,
            //Existencia = 4,
            //Cantidad_Solicitada = 5, Cantidad_Surtida = 6, Cantidad = 7,
            //Terminar_Clave = 8, Clave_Terminada = 9

            Ninguna = 0,
            ClaveSSA = 1, 
            IdClaveSSA, Descripcion, Existencia, ExistenciaAlmacenaje, 
            Cantidad_Solicitada, Cantidad_Surtida, Cantidad,  
            Terminar_Clave, Clave_Terminada, 
            ContenidoPaquete, CantidadCajas, 
            IdClasificacion, CodigoDeColor, 
            EsEditable 
        }

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnRegional;
        clsLeer myLeer;
        clsLeer myLlenaDatos;
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsLeer leerPedido;
        clsLeerWebExt leerWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFarmaciaGenera = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-");
        string sMensaje = ""; //, sValorGrid = "";
        string sFolioSurtido = ""; 
        string sFolioSurtido_Consulta = "*";
        bool bEsConsulta = false; 

        string sEmpresaPed = DtGeneral.EmpresaConectada;
        string sEstadoPed = DtGeneral.EstadoConectado;
        string sFarmaciaPed = DtGeneral.FarmaciaConectada;
        string sFolioPedido = "";
        bool bPedidoSurtido = false;
        bool bSeGuardoSurtido = false;
        bool bBandera = true;
        int iDescontarEnTransito = 0;
        int iPedidoManual = 0;
        int iTipoDeUbicaciones = 0;
        int iTipoDeInventario = 0;
        bool bCajasCompletas = false;
        bool bSeGeneroDistribucion = false;
        bool bCatidadesValidas = true;

        bool bSeReservoPedido = false;
        bool bEsPropietario_Reserva = false; 

        DataSet dtsCantidadesInvalidas;

        TipoDePedidoElectronico TipoDePedidoElec;

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PEDIDOS_COMPRAS.xls";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        //DataSet para ejecutar los pedidos de distribuidor en Regional
        DataSet dtsPedido = new DataSet();
        //string sUrlAlmacenCEDIS = GnFarmacia.UrlAlmacenCEDIS;
        string sUrlAlmacenCEDIS = "";
        //string sHostAlmacenCEDIS = GnFarmacia.HostAlmacenCEDIS;
        string sHostAlmacenCEDIS = "";
        //string sIdFarmaciaAlmacenCEDIS = GnFarmacia.IdFarmaciaAlmacenCEDIS;
        //string sIdFarmaciaAlmacenCEDIS = "";

        wsAlmacen.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        bool bGuardadoDeInformacion_Masivo = true;
        string sSql_Detallado = "";

        public FrmCEDIS_SurtidoPedidos():this(false) 
        {
        }

        public FrmCEDIS_SurtidoPedidos(bool DescontarEnTransito): this(DescontarEnTransito, false)
        {

        }

        public FrmCEDIS_SurtidoPedidos(bool DescontarEnTransito, bool PedidoManual)
        {
            InitializeComponent();

            General.Pantalla.AjustarTamaño(this, 90, 80); 

            chkImprimirConcentrado.BackColor = General.BackColorBarraMenu;
            chkImprimirSurtimiento.BackColor = General.BackColorBarraMenu;
            chkImprimirCaratulaSurtimiento.BackColor = General.BackColorBarraMenu;

            iDescontarEnTransito = DescontarEnTransito ? 1 : 0;
            iPedidoManual = PedidoManual ? 1 : 0;

            this.Text += PedidoManual ? " (Asignación manual) " : " (Asignación automática) ";
             
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            leerExportarExcel = new clsLeer(ref ConexionLocal);
            leerWeb = new clsLeerWebExt(General.Url, General.ArchivoIni, DatosCliente);
            leer = new clsLeer(ref ConexionLocal);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdProductos, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdProductos.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.WhiteSmoke;
            myGrid.AjustarAnchoColumnasAutomatico = true;

            Cargar_Prioridades(); 
            LlenarSurtidores();
            Cargar_Ubicaciones();

            myGrid.OcultarColumna(true, (int)Cols.IdClasificacion, (int)Cols.IdClasificacion);
            //myGrid.OcultarColumna(true, (int)Cols.CodigoDeColor, (int)Cols.CodigoDeColor);

            myGrid.SetOrder((int)Cols.ClaveSSA, 1, true);
            myGrid.SetOrder((int)Cols.Descripcion, 1, true);
            myGrid.SetOrder((int)Cols.CodigoDeColor, 1, true);
        }

        private void FrmFrmCEDIS_SurtidoPedidos_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void FrmCEDIS_SurtidoPedidos_FormClosing( object sender, FormClosingEventArgs e )
        {
            if(bSeReservoPedido)
            {
                GetReserva(0);
            }
            else
            {
                if(bEsPropietario_Reserva && sFolioSurtido_Consulta == "*")
                {
                    GetReserva(0);
                }
            }
        }

        #region Limpiar 
        private void IniciarToolBar(bool Guardar, bool Cancelar, bool Imprimir)
        {
            btnGuardar.Enabled = Guardar;
            btnCancelar.Enabled = false;
            btnImprimir.Enabled = Imprimir; 
        }

        private void LimpiarPantalla()
        {
            myGrid.Limpiar(true);
            Fg.IniciaControles();
            IniciarToolBar(true, false, false);

            lblStatus.Text = "CANCELADA"; //Se pone aqui ya que el IniciaControles le borra el texto.
            lblStatus.Visible = false;
            lblStatus.Text = "";
            lblStatus.Visible = false;

            chkImprimirConcentrado.Checked = false; 
            chkImprimirSurtimiento.Checked = true;
            chkImprimirCaratulaSurtimiento.Checked = true;

            dtpFechaRegistro.Enabled = false;
            //dtpFechaRegistro.Value = GnFarmacia.FechaOperacionSistema;
            //dtpFechaRecepcion.MaxDate = GnFarmacia.FechaOperacionSistema;

            //dtsPedido = null;
            //dtsPedido = PreparaDtsPedidos();

            rdoOrigenInventario_03___General.Checked = true; 
            rdo_U_Picking.Checked = true; 
            txtFolio.Enabled = false;
            txtFolio.Text = "*"; 
            txtFolio.Focus();

            bPedidoSurtido = false; 

            if (TipoDePedidoElectronico.SociosComerciales == TipoDePedidoElec)
            {
                rdoOrigenInventario_01___Venta.Checked = true;
                rdoOrigenInventario_02___Consigna.Enabled = false;
                rdoOrigenInventario_03___General.Enabled = false;
            }

            ////if(sFolioSurtido_Consulta == "*")
            {
                VerificarReserva();
            }

            CargarInformacionPedido(); 

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }        
        #endregion Limpiar

        #region Buscar Folio 
        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            bool bContinua = false;
            IniciarToolBar(false, false, false);

            myLeer = new clsLeer(ref ConexionLocal);

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Enabled = false;
                txtFolio.Text = "*";
                IniciarToolBar(true, false, false);
            }
            else
            {
                myLeer.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Enc(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
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
                txtFolio.Focus();
            }
        }

        private void CargaEncabezadoFolio()
        {            
            //Se hace de esta manera para la ayuda.
            txtFolio.Text = myLeer.Campo("Folio");
            sFolioPedido = txtFolio.Text;
            dtpFechaRegistro.Value = myLeer.CampoFecha("FechaRegistro");
            
            // lblStatus.Text = myLeer.Campo("StatusPedidoDesc");
            // lblStatus.Visible = true; 
            txtObservaciones.Text = myLeer.Campo("Observaciones"); 
            
            //Se bloquea el encabezado del Folio 
            Fg.BloqueaControles(this, false, FrameEncabezado);
            
            ////if ( myLeer.CampoInt("StatusPedido") != 0)
            ////    IniciarToolBar(false, false, true);
            ////else
            ////    IniciarToolBar(false, true, true);

            IniciarToolBar(false, false, true ); 
            if (myLeer.Campo("Status") == "C")
            {   
                IniciarToolBar(false, false, true );
                lblStatus.Text = "CANCELADO";
                lblStatus.Visible = true; 
            }
        }

        private bool CargaDetallesFolio()
        {
            bool bRegresa = false;

            myLlenaDatos.DataSetClase = Consultas.Folio_Pedidos_CEDIS_Det(sEmpresa, sEstado, sFarmacia, txtFolio.Text.Trim(), "txtFolio_Validating");
            if (myLlenaDatos.Leer())
            {
                bRegresa = true; 
                myGrid.LlenarGrid(myLlenaDatos.DataSetClase, false, false);
                CargarDatosExportarExcel(); 
            }

            // Bloquear grid completo 
            myGrid.BloqueaRenglon(true);

            return bRegresa;
        } 
        #endregion Buscar Folio
        
        #region Funciones y Procedimientos Publicos 
        public bool CargarPedidoNuevo(string IdEmpresa, string IdEstado, string IdFarmacia, string IdFarmaciaPedido, string FolioPedido, TipoDePedidoElectronico TipoPedido)
        {
            bSeGuardoSurtido = false;
            bEsConsulta = false;  
            sEmpresaPed = Fg.PonCeros(IdEmpresa, 3);
            sEstadoPed = Fg.PonCeros(IdEstado, 2);
            sFarmaciaGenera = Fg.PonCeros(IdFarmacia, 4);
            sFarmaciaPed = Fg.PonCeros(IdFarmaciaPedido, 4);
            //sFarmaciaPed = Fg.PonCeros(IdFarmacia, 4);
            sFolioSurtido_Consulta = "*"; 
            sFolioPedido = Fg.PonCeros(FolioPedido, 6);

            TipoDePedidoElec = TipoPedido;

            this.ShowDialog();

            return bSeGuardoSurtido; 
        }

        public bool CargarPedido(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioSurtido, string FolioPedido)
        {
            bSeGuardoSurtido = false;
            bEsConsulta = true; 
            sEmpresaPed = Fg.PonCeros(IdEmpresa, 3);
            sEstadoPed = Fg.PonCeros(IdEstado, 2);
            sFarmaciaPed = Fg.PonCeros(IdFarmacia, 4);
            sFolioSurtido_Consulta = Fg.PonCeros(FolioSurtido, 8); 
            sFolioPedido = Fg.PonCeros(FolioPedido, 6);

            this.ShowDialog();

            return bSeGuardoSurtido;
        }

        private void CargarInformacionPedido()
        {
            clsLeer leerResultado = new clsLeer(); 
            string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', " + 
                " @FolioSurtido = '{4}', @FolioPedido = '{5}', @AplicarEnTransito = '{6}', @Manual = '{7}' ",
                sEmpresaPed, sEstadoPed, sFarmaciaGenera, sFarmaciaPed, sFolioSurtido_Consulta, sFolioPedido, iDescontarEnTransito, iPedidoManual);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarInformacionPedido()"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro el Pedido solicitado, verifique.");
                    this.Close(); 
                }
                else
                {
                    leer.RenombrarTabla(1, "Encabezado");
                    leer.RenombrarTabla(2, "Detalle");
                    leer.RenombrarTabla(3, "EncabezadoSurtido"); 

                    leerResultado.DataTableClase = leer.Tabla("Encabezado");
                    leerResultado.Leer(); 
                    lblFolioPedido.Text = leerResultado.Campo("FolioPedido"); 
                    lblFarmacia.Text = leerResultado.Campo("Farmacia");
                    lblFechaPedido.Text = General.FechaYMD(leerResultado.CampoFecha("FechaPedido"));
                    bPedidoSurtido = leerResultado.CampoBool("PedidoSurtido");

                    bCajasCompletas = leerResultado.CampoBool("CajasCompletas");

                    leerResultado.DataTableClase = leer.Tabla("Detalle");
                    myGrid.LlenarGrid(leerResultado.DataSetClase);
                    RevisarCapturaClaves(); 

                    if (bPedidoSurtido)
                    {
                        IniciarToolBar(false, false, false );
                        General.msjAviso("El pedido ya fue surtido, no es posible generar mas surtimientos parciales."); 
                    }

                    if (bEsConsulta)
                    {
                        btnNuevo.Enabled = false; 
                        IniciarToolBar(false, false, true );

                        leerResultado.DataTableClase = leer.Tabla("EncabezadoSurtido");
                        leerResultado.Leer();

                        txtFolio.Text = leerResultado.Campo("FolioSurtido");
                        txtObservaciones.Text = leerResultado.Campo("Observaciones");
                        txtObservaciones.Enabled = false; 
                        dtpFechaRegistro.Value = leerResultado.CampoFecha("FechaRegistro");

                        cboSurtidores.Enabled = false;
                        cboSurtidores.Data = leerResultado.Campo("IdSurtidor");


                        rdoOrigenInventario_03___General.Checked = leerResultado.CampoInt("TipoDeInventario") == 0;
                        rdoOrigenInventario_02___Consigna.Checked = leerResultado.CampoInt("TipoDeInventario") == 1;
                        rdoOrigenInventario_01___Venta.Checked = leerResultado.CampoInt("TipoDeInventario") == 2;

                        rdo_U_Todo.Checked = leerResultado.CampoInt("TipoDeUbicaciones") == 0;
                        rdo_U_Picking.Checked = leerResultado.CampoInt("TipoDeUbicaciones") == 1;
                        rdo_U_Almacenaje.Checked = leerResultado.CampoInt("TipoDeUbicaciones") == 2;

                        cboPrioridades.Enabled = false;
                        cboPrioridades.Data = leerResultado.Campo("Prioridad");

                        cboUbicaciones.Enabled = false;
                        cboUbicaciones.Data = leerResultado.Campo("IdGrupo");

                        nmMesesCaducidad.Value = leerResultado.CampoDec("MesesCaducidad");
                        nmMesesCaducidad_Consigna.Value = leerResultado.CampoDec("MesesCaducidad_Consigna");
                        FrameUbicaciones.Enabled = false;
                        FrameOrigenDeInventario.Enabled = false; 
                        nmMesesCaducidad.Enabled = false;

                        chkTerminarTodasLasClaves.Enabled = false;
                        chkAsignarCantidades.Enabled = false;

                        myGrid.BloqueaGrid(true); 
                        myGrid.BloqueaColumna(true, (int)Cols.Cantidad);
                        myGrid.BloqueaColumna(true, (int)Cols.Terminar_Clave);
                    }

                    //if (myGrid.TotalizarColumna((int)Cols.Cantidad) == 0)
                    //{
                    //    IniciarToolBar(false, false, false);
                    //}
                }
            }
        }

        private void RevisarCapturaClaves()
        {
            bool bBloquea = false;
            bool bEsEditable = false; 
            int iExistencia = 0;
            int iExistencia_Almacenaje = 0;
            int iIdClasificacion = 0;
            Color cColor = Color.White; 


            for (int i = 1; i <= myGrid.Rows; i++)
            {
                bEsEditable = true;
                
                bBloquea = myGrid.GetValueBool(i, (int)Cols.Clave_Terminada);
                myGrid.BloqueaCelda(bBloquea, i, (int)Cols.Cantidad); 
                myGrid.BloqueaCelda(bBloquea, i, (int)Cols.Terminar_Clave);
                iExistencia = myGrid.GetValueInt(i, (int)Cols.Existencia);
                iExistencia_Almacenaje = myGrid.GetValueInt(i, (int)Cols.ExistenciaAlmacenaje);

                iIdClasificacion = myGrid.GetValueInt(i, (int)Cols.IdClasificacion);

                int iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad_Solicitada) - myGrid.GetValueInt(i, (int)Cols.Cantidad_Surtida);


                if (!bBloquea && !bEsConsulta)
                {
                    myGrid.SetValue(i, (int)Cols.Cantidad, iCantidad);
                }
                else if (!bEsConsulta)
                {
                    bEsEditable = false;
                    myGrid.ColorRenglon(i, Color.Yellow);
                }


                if (iExistencia <= 0 || iExistencia_Almacenaje <= 0)
                {
                    if(iExistencia <= 0)
                    {
                        myGrid.ColorCelda(i, (int)Cols.Existencia, Color.Red);
                    }

                    if(iExistencia_Almacenaje <= 0)
                    {
                        myGrid.ColorCelda(i, (int)Cols.ExistenciaAlmacenaje, Color.Red);
                    }

                    if(iExistencia == 0 && iExistencia_Almacenaje == 0)
                    {
                        bEsEditable = false;
                        myGrid.BloqueaCelda(true, i, (int)Cols.Cantidad);
                        myGrid.SetValue(i, (int)Cols.Cantidad, 0);
                    }
                }


                cColor = Color.White;
                //myGrid.ColorCelda(i, (int)Cols.CodigoDeColor, cColor);

                if(iIdClasificacion == 1)
                {
                    cColor = Color.LightSeaGreen;
                }

                if(iIdClasificacion == 2)
                {
                    cColor = Color.LightGreen;
                    //cColor = Color.LightSeaGreen;
                }

                if(iIdClasificacion == 3)
                {
                    cColor = Color.LightBlue;
                }

                if(bEsConsulta)
                {
                    myGrid.BloqueaCelda(true, i, (int)Cols.Cantidad);
                    myGrid.BloqueaCelda(true, i, (int)Cols.Terminar_Clave);
                }

                myGrid.ColorCelda(i, (int)Cols.CodigoDeColor, cColor);
                myGrid.SetValue(i, Cols.EsEditable, bEsEditable);

                if (bEsEditable)
                {
                    int iExistenciaPicking = myGrid.GetValueInt(i, (int)Cols.Existencia);

                    if (iExistenciaPicking < iCantidad)
                    {
                        myGrid.ColorCelda(i, (int)Cols.Cantidad_Solicitada, Color.Orange);
                    }
                }
            }
        }

        private void Cargar_Prioridades()
        {
            string sSql = "Select Prioridad, (cast(Prioridad as varchar(10)) + ' - ' + Descripcion ) as Descripcion From Pedidos_Prioridades (NoLock) Where Status = 'A' Order by Prioridad ";

            cboPrioridades.Clear();
            cboPrioridades.Add();
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Prioridades");
                General.msjError("Ocurrió un error al obtener el listado de Prioridades");
            }
            else
            {
                cboPrioridades.Add(leer.DataSetClase, true, "Prioridad", "Descripcion");
            }
            cboPrioridades.SelectedIndex = 0;
        }


        private void Cargar_Ubicaciones()
        {
            string sSql = string.Format("Select IdGrupo, (cast(IdGrupo as varchar(10)) + ' - ' + NombreGrupo ) as NombreGrupo " +
                    "From CFGC_ALMN__GruposDeUbicaciones (NoLock) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Status = 'A' Order by IdGrupo ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            cboUbicaciones.Clear();
            cboUbicaciones.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Ubicaciones");
                General.msjError("Ocurrió un error al obtener el listado de Grupos de Ubicaciones.");
            }
            else
            {
                cboUbicaciones.Add(leer.DataSetClase, true, "IdGrupo", "NombreGrupo");
            }
            cboUbicaciones.SelectedIndex = 0;
        }

        private void LlenarSurtidores()
        {
            //////string sSql = string.Format(" Select IdPersonal, Personal From vw_PersonalCEDIS(NoLock) Where IdPuesto = '01' Order By Personal ");
            //////cboSurtidores.Add("0", "<< Seleccione >>");

            //////if (!myLeer.Exec(sSql))
            //////{
            //////    Error.GrabarError(myLeer, "LlenarSurtidores()");
            //////    General.msjError("Ocurrió un error al obtener la Lista de Surtidores.");
            //////}
            //////else
            //////{
            //////    if (myLeer.Leer())
            //////    {
            //////        cboSurtidores.Add(myLeer.DataSetClase, true);
            //////    }
            //////    cboSurtidores.SelectedIndex = 0;
                
            //////}
        }

        #endregion Funciones y Procedimientos Publicos

        #region Guardar Informacion
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            grdProductos_EditModeOff(this, null);

            if (bBandera)
            {
                if (ValidaDatos())
                {
                    GuardarInformacion();
                }
            }
        }

        private void GuardarInformacion()
        {
            bool bContinua = false;
            bSeGeneroDistribucion = false;
            bCatidadesValidas = true;
            iTipoDeUbicaciones = 0;
            iTipoDeInventario = 0;

            /////////////// TIPO DE INVENTARIO 
            if(rdoOrigenInventario_02___Consigna.Checked)
            {
                iTipoDeInventario = 1;
            }

            if(rdoOrigenInventario_01___Venta.Checked)
            {
                iTipoDeInventario = 2;
            }
            /////////////// TIPO DE INVENTARIO 

            /////////////// UBICACIONES 
            if(rdo_U_Picking.Checked)
            {
                iTipoDeUbicaciones = 1;
            }

            if (rdo_U_Almacenaje.Checked)
            {
                iTipoDeUbicaciones = 2;
            }
            /////////////// UBICACIONES 


            if(!ConexionLocal.Abrir())
            {
                // General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
                General.msjErrorAlAbrirConexion(); 
            }
            else
            {
                ConexionLocal.IniciarTransaccion();

                bContinua = GrabarEncabezado();

                if (bContinua && bSeGeneroDistribucion)
                {
                    bContinua = Graba_Atencion_Surtido();

                    if (bContinua)
                    {
                        bContinua = Validar_Cantidades();
                    }
                }

                if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                {
                    if(!bSeGeneroDistribucion || !bCatidadesValidas)
                    {
                        ConexionLocal.DeshacerTransaccion();

                        if (!bSeGeneroDistribucion)
                        General.msjUser("No se encontro información de al menos una Clave para generar el Folio de Surtido.");

                        if (!bCatidadesValidas)
                        {
                            DllFarmaciaSoft.ReporteadorGenerico.Frm_RptListaGenerica f = new DllFarmaciaSoft.ReporteadorGenerico.Frm_RptListaGenerica(dtsCantidadesInvalidas, "Cantidades fuera de rango.");
                            f.ShowDialog();
                        }
                            
                    }
                    else 
                    {
                        txtFolio.Text = sFolioSurtido;
                        ConexionLocal.CompletarTransaccion();

                        bSeGuardoSurtido = true;
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP
                        IniciarToolBar(false, false, true);

                        if(chkImprimirConcentrado.Checked)
                        {
                            Imprimir(false);
                        }

                        if(chkImprimirSurtimiento.Enabled)
                        {
                            ImprimirSurtimiento(false);
                        }
                    }   
                    
                    // btnNuevo_Click(this, null);
                }
                else
                {
                    Error.GrabarError(myLeer, "btnGuardar_Click");
                    ConexionLocal.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la información.");
                }

                ConexionLocal.Cerrar();
            }
        }

        private bool GrabarEncabezado()
        {
            bool bRegresa = false;
            string sSql = ""; 


            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', @FolioSurtido = '{4}', @FolioPedido = '{5}', \n" +
                "\t@MesesCaducidad = '{6}', @MesesCaducidad_Consigna = '{7}', @IdPersonal = '{8}', @Observaciones = '{9}', @IdPersonalSurtido = '{10}', @Status = '{11}', \n" +
                "\t@EsManual = '{12}', @TipoDeUbicaciones = '{13}', @Prioridad = '{14}', @TipoDeInventario = '{15}', @IdGrupo = '{16}' \n ",
                sEmpresa, sEstado, sFarmacia, sFarmaciaGenera, 
                txtFolio.Text.Trim(), sFolioPedido, Convert.ToInt32(nmMesesCaducidad.Value), Convert.ToInt32(nmMesesCaducidad_Consigna.Value),
                DtGeneral.IdPersonal, txtObservaciones.Text, "0000", "A", iPedidoManual, iTipoDeUbicaciones, cboPrioridades.Data, iTipoDeInventario, cboUbicaciones.Data ); 

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                myLeer.Leer();
                sFolioSurtido = myLeer.Campo("Folio");
                sMensaje = myLeer.Campo("Mensaje"); 

                bRegresa = GrabarDetalle();

                ////if (bRegresa)
                ////{
                ////    bRegresa = RevisarPedidoCompleto(); 
                ////}

                if (bRegresa)
                {
                    bRegresa = GrabarDistribucionSurtimiento();
                }
            }

            return bRegresa;
        }

        private bool RevisarPedidoCompleto()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdFarmaciaPedido = '{3}', @FolioPedido = '{4}' \n",
                sEmpresa, sEstado, sFarmacia, sFarmaciaPed, sFolioPedido );

            bRegresa = leer.Exec(sSql); 

            return bRegresa; 
        }

        private bool GrabarDetalle()
        {
            bool bRegresa = false;
            bool bGuardar = false;
            string sStatus = "";
            string sSql = ""; // , sQuery = "";
            string sIdClaveSSA = "", sClaveSSA = "";
            int iCantidad = 0; // , iOpcion = 1;

            sSql_Detallado = "";
            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = true;
            }

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                sIdClaveSSA = myGrid.GetValue(i, (int)Cols.IdClaveSSA);
                sClaveSSA = myGrid.GetValue(i, (int)Cols.ClaveSSA);
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);

                sStatus = "A"; 
                bGuardar = iCantidad > 0;
                if (myGrid.GetValueBool(i, (int)Cols.Terminar_Clave))
                { 
                    bGuardar = myGrid.GetValueBool(i, (int)Cols.Terminar_Clave);
                    sStatus = bGuardar ? "F" : "A"; 
                } 

                if(myGrid.GetValueBool(i, (int)Cols.Clave_Terminada) ) 
                {
                    bGuardar = false; 
                } 

                if ( bGuardar )
                {
                    sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Det_Surtido \n" +
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdClaveSSA = '{4}', @ClaveSSA = '{5}',\n" +
                        "\t@CantidadSolicitada = '{6}', @sStatus = '{7}' \n",
                        sEmpresa, sEstado, sFarmacia, sFolioSurtido, sIdClaveSSA, sClaveSSA, iCantidad, sStatus);

                    if(bGuardadoDeInformacion_Masivo)
                    {
                        sSql_Detallado += string.Format("{0}\n", sSql);
                    }

                    if(!bGuardadoDeInformacion_Masivo)
                    {
                        bRegresa = myLeer.Exec(sSql);
                        if(!bRegresa)
                        {
                            break;
                        }
                    }
                }
            }


            //// Ejecucion concentrada 
            if(bGuardadoDeInformacion_Masivo)
            {
                bRegresa = leer.Exec(sSql_Detallado);
            }

            return bRegresa;
        }

        private bool GrabarDistribucionSurtimiento()
        {
            bool bRegresa = true;

            string sSql = string.Format("Exec spp_ALM_GenerarDistribucion_Surtimiento \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @MesesCaducidad = '{4}', @MesesCaducidad_Consigna = '{5}', \n" +
                "\t@Manual = '{6}', @TipoUbicacion = '{7}', @TipoDeInventario = '{8}', @IdGrupo = '{9}', @TipoPedido = '{10}', @SubFarmaciaVenta_Traspasos_Estados = '{11}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, nmMesesCaducidad.Value, nmMesesCaducidad_Consigna.Value, 
                iPedidoManual, iTipoDeUbicaciones, iTipoDeInventario, cboUbicaciones.Data, (Int32)TipoDePedidoElec, GnFarmacia.SubFarmaciaVenta_Traspasos_Estados);

            bSeGeneroDistribucion = false; 
            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                bSeGeneroDistribucion = myLeer.Leer();
                ////sFolioSurtido = myLeer.Campo("Folio");
                ////sMensaje = myLeer.Campo("Mensaje");
            }

            return bRegresa;
        }

        private bool Graba_Atencion_Surtido()
        {
            bool bRegresa = false;
            string sSql = "";

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @IdPersonal = '{4}', @Observaciones = '{5}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido, DtGeneral.IdPersonal, "");

            sSql += string.Format("Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioSurtido = '{3}', @TipoFactor = 1, @Validacion_Especifica = 0 \n",
                sEmpresa, sEstado, sFarmacia, sFolioSurtido);

            bRegresa = myLeer.Exec(sSql); 

            return bRegresa;
        }


        private bool Validar_Cantidades()
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format("Exec spp_ALM_AplicaDesaplica_ExistenciaSurtidos__Validar_Claves \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}' \n",
                sEmpresa, sEstado, sFarmacia, sFolioPedido);


            bCatidadesValidas = false;

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                bCatidadesValidas = !myLeer.Leer();
                dtsCantidadesInvalidas = myLeer.DataSetClase;
            }

            return bRegresa;
        }

        #endregion Guardar Informacion

        #region Eliminar Informacion 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ////string sSql = "", sMensaje = "";
            ////int iOpcion = 2; //La opcion 2 indica que es una cancelacion
            ////string message = "¿ Desea cancelar el Folio seleccionado ?";

            ////if (General.msjCancelar(message) == DialogResult.Yes)
            ////{
            ////    if (ConexionLocal.Abrir())
            ////    {
            ////        ConexionLocal.IniciarTransaccion();

            ////        sSql = string.Format("Set Dateformat YMD Exec spp_Mtto_ALMJ_Pedidos_RC '{0}', '{1}', '{2}', '{3}', '{4}', '', '', '', '', '', '{5}' ",
            ////            sEmpresa, sEstado, sJurisdiccion, sFarmacia, txtFolio.Text, iOpcion);

            ////        if (myLeer.Exec(sSql))
            ////        {
            ////            if (myLeer.Leer())
            ////                sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));

            ////            ConexionLocal.CompletarTransaccion();
            ////            General.msjUser(sMensaje); //Este mensaje lo genera el SP
            ////            btnNuevo_Click(null, null);
            ////        }
            ////        else
            ////        {
            ////            ConexionLocal.DeshacerTransaccion();
            ////            Error.GrabarError(myLeer, "btnCancelar_Click");
            ////            General.msjError("Ocurrió un error al cancelar el Folio.");
            ////            //btnNuevo_Click(null, null);
            ////        }

            ////        ConexionLocal.Cerrar();
            ////    }
            ////    else
            ////    {
            ////        General.msjAviso("No hay conexion al Servidor. Intente de nuevo por favor");
            ////    }
            ////}            
        }
        #endregion Eliminar Informacion

        #region Imprimir Informacion
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (chkImprimirConcentrado.Checked)
            {
                Imprimir(false);
            }

            if (chkImprimirSurtimiento.Checked)
            {
                ImprimirSurtimiento(false);
            }

            if(chkImprimirCaratulaSurtimiento.Checked)
            {
                ImprimirSurtimiento_Caratula(false);
            }           

        }

        private bool validarImpresion(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información del surtimiento ? ") == DialogResult.No)
                {
                    bRegresa = false; 
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Pedido Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private bool validarImpresionSurtimiento(bool Confirmar)
        {
            bool bRegresa = true;

            if (Confirmar)
            {
                if (General.msjConfirmar(" ¿ Desea imprimir la información del surtimiento a detalle ? ") == DialogResult.No)
                {
                    bRegresa = false;
                }
            }

            if (bRegresa)
            {
                if (txtFolio.Text.Trim() == "" || txtFolio.Text.Trim() == "*")
                {
                    bRegresa = false;
                    General.msjUser("Folio de Pedido Inicial inválido, verifique.");
                }
            }

            return bRegresa;
        }

        private void Imprimir(bool Confirmacion)
        {
            bool bRegresa = true;
            //dImporte = Importe; 

            if (validarImpresion(Confirmacion))
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.TituloReporte = "Surtimiento";
                myRpt.RutaReporte = DtGeneral.RutaReportes; 

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", General.Fg.PonCeros(txtFolio.Text, 8));
                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS__SurtidoParcial";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void ImprimirSurtimiento(bool Confirmacion)
        {
            bool bRegresa = true;

            if (validarImpresionSurtimiento(Confirmacion))
            {
                DatosCliente.Funcion = "ImprimirSurtimiento()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.TituloReporte = "Orden de surtido";
                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.Add("@IdEmpresa", sEmpresa);
                myRpt.Add("@IdEstado", sEstado);
                myRpt.Add("@IdFarmacia", sFarmacia);
                myRpt.Add("@Folio", General.Fg.PonCeros(txtFolio.Text, 8));
                myRpt.NombreReporte = GnFarmacia.Vta_Impresion_Personalizada_Surtido_De_Pedidos; // "PtoVta_Cedis_SurtidoPedidos_Det";

                if (iPedidoManual == 1)
                {
                    myRpt.NombreReporte = "PtoVta_Cedis_SurtidoPedidos_Det_Manual";
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }            
        }

        private void ImprimirSurtimiento_Caratula( bool Confirmacion )
        {
            bool bRegresa = true;

            if(validarImpresionSurtimiento(Confirmacion))
            {
                DatosCliente.Funcion = "ImprimirSurtimiento()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                myRpt.TituloReporte = "Caratula de surtido";
                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.Add("IdEmpresa", sEmpresa);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", General.Fg.PonCeros(txtFolio.Text, 8));
                myRpt.NombreReporte = GnFarmacia.Vta_Impresion_Personalizada_Surtido_De_Pedidos_Caratula; // "PtoVta_Cedis_SurtidoPedidos_Det";

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Imprimir Informacion

        #region Eventos 
        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
        } 
        #endregion Eventos

        #region Grid 
        private void grdProductos_KeyDown(object sender, KeyEventArgs e)
        {
            ////////if (myGrid.ActiveCol == 1)
            ////////{
            ////////    if (e.KeyCode == Keys.F1)
            ////////    {
            ////////        myLeer.DataSetClase = ayuda.ClavesSSA_Sales("grdProductos_KeyDown");
            ////////        if (myLeer.Leer()) 
            ////////        {
            ////////            myGrid.SetValue(myGrid.ActiveRow, 1, myLeer.Campo("IdClaveSSa_Sal"));
            ////////            CargaDatosSal();
            ////////        }

            ////////    }

            ////////    if (e.KeyCode == Keys.Delete)
            ////////    {
            ////////        myGrid.DeleteRow(myGrid.ActiveRow);

            ////////        if (myGrid.Rows == 0)
            ////////            myGrid.Limpiar(true);
            ////////    }

            ////////}
        }

        private void grdProductos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        { 
            //////if (lblStatus.Visible == false)
            //////{
            //////    if ((myGrid.ActiveRow == myGrid.Rows) && e.AdvanceNext)
            //////    {
            //////        if (myGrid.GetValue(myGrid.ActiveRow, 1) != "" && myGrid.GetValue(myGrid.ActiveRow, 3) != "")
            //////        {
            //////            myGrid.Rows = myGrid.Rows + 1;
            //////            myGrid.ActiveRow = myGrid.Rows;
            //////            myGrid.SetActiveCell(myGrid.Rows, 1);
            //////        }
            //////    }
            //////}
        }

        private void grdProductos_EditModeOn(object sender, EventArgs e)
        {
            //////sValorGrid = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.ClaveSSA); 
        }

        private void grdProductos_EditModeOff(object sender, EventArgs e)
        {
            bBandera = true;
            int iRow = myGrid.ActiveRow;
            int iCantidad_Solicitada = myGrid.GetValueInt(iRow, (int)Cols.Cantidad_Solicitada);
            int iCantidad_Surtida = myGrid.GetValueInt(iRow, (int)Cols.Cantidad_Surtida);
            int iCantidad = myGrid.GetValueInt(iRow, (int)Cols.Cantidad);
            //int iTotal = iCantidad_Surtida + iCantidad;
            int iCajas = myGrid.GetValueInt(iRow, (int)Cols.CantidadCajas); 
            int iContenidoPaquete = myGrid.GetValueInt(iRow, (int)Cols.ContenidoPaquete);
            int iCantidadPiezas_x_Cajas = iCajas * iContenidoPaquete;

            if(bCajasCompletas)
            {
                iCantidad = iCantidadPiezas_x_Cajas;
                myGrid.SetValue(iRow, (int)Cols.Cantidad, (iCantidad));
            }

            if ((iCantidad > iCantidad_Solicitada) && iCantidad != 0) 
            {
                bBandera = false;
                General.msjAviso("Cantidad a surtir no puede ser mayor a la cantidad solicitada, verifique.");
                myGrid.SetValue(iRow, (int)Cols.Cantidad, (iCantidad_Solicitada - iCantidad_Surtida));
                myGrid.SetActiveCell(iRow, (int)Cols.Cantidad); 
            }
        }

        private void limpiarColumnas()
        {
            for (int i = 2; i <= myGrid.Columns; i++) //Columnas. Nota: Inicia a partir de la 2da.
            {
                myGrid.SetValue(myGrid.ActiveRow, i, "");
            }
        }

        private void ObtenerDatosSal()
        {
            string sCodigo = ""; // , sSql = "";
            // int iCantidad = 0;

            sCodigo = myGrid.GetValue(myGrid.ActiveRow, 1);

            if ( sCodigo.Trim() == "" )
            {
                General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                myGrid.LimpiarRenglon(myGrid.ActiveRow);
            }
            else
            {
                ////sSql = string.Format("Exec Spp_SalesCapturaPedidosCentros '{0}', '{1}' ",
                ////    Fg.PonCeros(sCodigo, 4), sCodigo);
                ////if (!myLeer.Exec(sSql))
                ////{
                ////    Error.GrabarError(myLeer, "ObtenerDatosSal()");
                ////    General.msjError("Ocurrió un error al obtener la información de la Sal.");
                ////}
                ////else 
                {
                    myLeer.DataSetClase = Consultas.ClavesSSA_Sales(sCodigo, true, "ObtenerDatosSal()"); 
                    if (!myLeer.Leer())
                    {
                        //General.msjUser("Sal no encontrada ó no esta Asignada a la Farmacia.");
                        myGrid.LimpiarRenglon(myGrid.ActiveRow);
                    }
                    else
                    {
                        CargaDatosSal();
                    }
                }
            }
        }

        private void CargaDatosSal()
        {
            int iRowActivo = myGrid.ActiveRow;

            if (lblStatus.Visible == false)
            {
                if (!myGrid.BuscaRepetido(myLeer.Campo("ClaveSSA"), iRowActivo, 1))
                {
                    myGrid.SetValue(iRowActivo, (int)Cols.ClaveSSA, myLeer.Campo("ClaveSSA"));
                    myGrid.SetValue(iRowActivo, (int)Cols.IdClaveSSA, myLeer.Campo("IdClaveSSA_Sal"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Descripcion, myLeer.Campo("Descripcion"));
                    myGrid.SetValue(iRowActivo, (int)Cols.Cantidad, myLeer.Campo("Cantidad"));
                    myGrid.BloqueaCelda(true, Color.WhiteSmoke, iRowActivo, (int)Cols.ClaveSSA);
                    myGrid.SetActiveCell(iRowActivo, (int)Cols.Cantidad);
                }
                else
                {
                    General.msjUser("Esta Clave ya se encuentra capturada en otro renglon.");
                    myGrid.SetValue(myGrid.ActiveRow, (int)Cols.Cantidad, "");
                    limpiarColumnas();
                    myGrid.SetActiveCell(myGrid.ActiveRow, 1);
                }

            }
        }
        #endregion Grid

        #region Validaciones de Controles 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if(!bSeReservoPedido)
            {
                bRegresa = false;
                General.msjAviso("El Pedido no ha sido previamente reservado por el Usuario - Terminal actual, no es posible generar el Folio de Surtido.");
            }

            if (txtFolio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Folio de Pedido inválido, verifique.");
                txtFolio.Focus(); 
            }

            if (bRegresa && txtObservaciones.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado las observaciónes, verifique."); 
                txtObservaciones.Focus(); 
            }

            if (bRegresa && cboPrioridades.Data.Trim() == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una prioridad, verifique.");
                cboPrioridades.Focus();
            }

            if (bRegresa && cboUbicaciones.Data.Trim() == "0")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un grupo de ubicaciones, verifique.");
                cboPrioridades.Focus();
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
            bool bTerminarClave = false;
            bool bClave_Terminada = false;
            int iCantidad = 0;
            string sMsj = "Una ó mas claves no tienen cantidad asignada, ¿ Desea continuar ? "; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                iCantidad = myGrid.GetValueInt(i, (int)Cols.Cantidad);
                bTerminarClave = myGrid.GetValueBool(i, (int)Cols.Terminar_Clave);
                bClave_Terminada = myGrid.GetValueBool(i, (int)Cols.Clave_Terminada);

                if (!bClave_Terminada) 
                {
                    if (!bTerminarClave) 
                    {
                        if (iCantidad == 0)
                        {
                            bRegresa = false; 
                            break;
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                //General.msjUser("Debe capturar al menos una Clave para el Surtimiento\n y/o capturar cantidades para al menos una Clave, verifique.");
                if (General.msjConfirmar(sMsj) == DialogResult.Yes)
                {
                    bRegresa = true; 
                }
            }

            return bRegresa;

        } 
        #endregion Validaciones de Controles

        #region Exportar a Excel 
        private void CargarDatosExportarExcel()
        {
            string sSql = string.Format(" Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " + 
                " Folio, FechaRegistro as FechaPedido, Observaciones, IdPersonal, Personal, Status, " + 
                " IdClaveSSA, ClaveSSA, DescripcionClave, Cantidad, Existencia, StatusClave " +
                " From vw_Impresion_Pedidos_Cedis (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtFolio.Text.Trim(), 6));

            if (!leerExportarExcel.Exec(sSql))
            {
            } 
        } 
        #endregion Exportar a Excel

        #region Grabar_Informacion_Regional

        public static DataSet PreparaDtsPedidos()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("Sentencias");

            dtClave.Columns.Add("Query", Type.GetType("System.String"));
            dts.Tables.Add(dtClave);

            return dts.Clone();
        }

        private void InsertarQuerys(string sQuery)
        {
            object[] x = { sQuery };
            dtsPedido.Tables[0].Rows.Add(x);
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrlAlmacenCEDIS, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrlAlmacenCEDIS;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHostAlmacenCEDIS;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(myLeer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");

            }

            return bRegresa;
        }

        private bool GrabarPedidoDistribuidorRegional()
        {
            bool bRegresa = true;
            bool bContinua = true;
            string sQuery = "";

            if (validarDatosDeConexion())
            {
                cnnRegional = new clsConexionSQL(DatosDeConexion);
                cnnRegional.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnRegional.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leerPedido = new clsLeer(ref cnnRegional);

                if (cnnRegional.Abrir())
                {
                    IniciarToolBar(false, false, false);
                    cnnRegional.IniciarTransaccion();

                    leer.DataSetClase = dtsPedido;

                    while (leer.Leer())
                    {
                        sQuery = leer.Campo("Query");

                        if (!leerPedido.Exec(sQuery))
                        {
                            bContinua = false;
                            break;
                        }
                    }

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnnRegional.CompletarTransaccion();
                    }
                    else
                    {
                        bRegresa = false;
                        cnnRegional.DeshacerTransaccion();
                    }

                    cnnRegional.Cerrar();
                }
                else
                {
                    Error.LogError(cnnRegional.MensajeError);
                    General.msjAviso("Ocurrió un error al enviar la información al Almacen CEDIS.");
                }
            }
            else
            {
                bRegresa = false;
            }
            return bRegresa;

        }

        private bool ActualizarPedidosDistribuidor()
        {
            bool bRegresa = true;

            string sSql = string.Format(" Update Pedidos_Cedis_Enc Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n " +
                                " Update Pedidos_Cedis_Det Set Status = 'A', Actualizado = 0 " +
                                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioPedido = '{3}' \n ",
                                sEmpresa, sEstado, sFarmacia, sFolioPedido);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ActualizarPedidosDistribuidor");
                General.msjError("Ocurrió un error al Actualizar la Información.");
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Grabar_Informacion_Regional

        #region Checkboxes 
        private void chkTerminarTodasLasClaves_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (!myGrid.EsCeldaBloqueada(i, (int)Cols.Terminar_Clave)) 
                {
                    myGrid.SetValue(i, (int)Cols.Terminar_Clave, chkTerminarTodasLasClaves.Checked); 
                }
            }
        }

        private void chkAsignarCantidades_CheckedChanged( object sender, EventArgs e )
        {
            int iCantidad = 0;
            bool bChkAsignarCantidades = chkAsignarCantidades.Checked; 

            for(int i = 1; i <= myGrid.Rows; i++)
            {
                if(myGrid.GetValueBool(i, Cols.EsEditable))
                {
                    iCantidad = !bChkAsignarCantidades ? 0 : myGrid.GetValueInt(i, (int)Cols.Cantidad_Solicitada) - myGrid.GetValueInt(i, (int)Cols.Cantidad_Surtida);                    
                    myGrid.SetValue(i, (int)Cols.Cantidad, iCantidad);
                }
            }
        }
        #endregion Checkboxes 

        #region Reserva de Pedidos 
        private bool VerificarReserva( )
        {
            bool bRegresa = false;
            bool bResultado = false;
            int iResultado = 0;
            string sMsjResultado = "";
            string sSql = "";
            string sFolioDeSurtido = ""; //lst.GetValue((int)Cols.Folio);


            string sStatus = ""; //lst.GetValue((int)Cols.Status);
            bool bEsEditable = false; //lst.LeerItem().CampoBool("EsEditable");
            bool bEnEdicion = false; //lst.LeerItem().CampoBool("EnEdicion");
            bool bEdicionBloqueada = false; //lst.LeerItem().CampoBool("EdicionBloqueada");


            ////bEsManual = lst.LeerItem().CampoBool("EsManual");

            ////btnEdicion.Enabled = bEsEditable;
            ////btnTerminarEdicion.Enabled = bEnEdicion;

            ////if(bEnEdicion)
            ////{
            ////    btnTerminarEdicion.Enabled = !bEdicionBloqueada;
            ////    btnEdicion.Enabled = false;
            ////}


            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___Pedidos_ValidarReserva \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', \n" +
                "\t@IdPersonal = '{4}', @Terminal = '{5}'  \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido,
                DtGeneral.IdPersonal, General.NombreEquipo
                );

            bSeReservoPedido = false; 
            if(!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "VerificarReserva"); 
            }
            else
            {
                leer.Leer();
                bEsEditable = leer.CampoBool("EsEditable");
                bEnEdicion = leer.CampoBool("EnEdicion");
                bEdicionBloqueada = leer.CampoBool("EdicionBloqueada");
                bEsPropietario_Reserva = leer.CampoBool("Propietario_Reserva");
                bSeReservoPedido = bEsPropietario_Reserva;

                if(!bEnEdicion)
                {
                    /// Reservar el Pedido 
                    if(sFolioSurtido_Consulta == "*")
                    {
                        GetReserva(1);
                    }
                }
            }

            return bRegresa;
        }

            private bool GetReserva( int Tipo )
        {
            bool bRegresa = false;
            bool bResultado = false;
            int iResultado = 0;
            string sMsjResultado = "";
            string sSql = "";
            string sFolioDeSurtido = ""; //lst.GetValue((int)Cols.Folio);

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___ReservarPedido \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioPedido = '{3}', \n" +
                "\t@IdPersonal = '{4}', @Terminal = '{5}', @TipoDeProceso = '{6}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioPedido,
                DtGeneral.IdPersonal, General.NombreEquipo, Tipo
                );


            if(!ConexionLocal.Abrir())
            {
                General.msjErrorAlAbrirConexion();
            }
            else
            {
                ConexionLocal.IniciarTransaccion();

                if(!leer.Exec(sSql))
                {
                    bRegresa = false;
                }
                else
                {
                    bRegresa = true;

                    leer.Leer();
                    bResultado = leer.CampoBool("ProcesoCorrecto");
                    iResultado = leer.CampoInt("Resultado");
                    sMsjResultado = leer.Campo("Mensaje");
                }

                if(!bRegresa)
                {
                    Error.GrabarError(leer, "Reserva");
                    ConexionLocal.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al procesar el folio de pedido.");
                }
                else
                {
                    if(!bResultado)
                    {
                        ConexionLocal.DeshacerTransaccion();
                        General.msjError(sMsjResultado);
                    }
                    else
                    {
                        ConexionLocal.CompletarTransaccion();

                        if(Tipo == 1)
                        {
                            bSeReservoPedido = true;
                        }

                        General.msjUser(sMsjResultado);
                    }
                }

                ConexionLocal.Cerrar();

                ////CargarFoliosSurtido();
            }

            return bRegresa;
        }
        #endregion Reserva de Pedidos 
    }
}
