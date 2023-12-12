using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FarPoint.Win.Spread.CellType;

using SC_ControlsCS;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 

namespace DllFarmaciaSoft.Lotes
{
    internal partial class FrmCapturaLotes : FrmBaseExt
    {
        private enum Cols
        {
            Ninguno = 0,
            IdSubFarmacia = 1, SubFarmacia, 
            Codigo, CodigoEAN, 
            SKU, 
            ClaveLote, MesesPorCaducar,
            FechaEnt, FechaCad, Status, Existencia, Existencia_Disponible, 
            Cantidad,             
            AddColumna, 
 
            Ordenamiento, EsVentaBloqueada, EsCaducado, 
   
            ContenidoPaquete, Residuo,
            FLO
        }

        //////// Semaroforos e indicadores 
        ////Color colorCaducados = Color.Red; 
        ////Color colorPrecaucion = Color.Yellow; 
        ////Color colorStatusOk = Color.Green; 
        ////Color colorBloqueaCaducados = Color.BurlyWood; 
        ////Color colorSalidaCaducados = Color.LightGray; 
        ////Color colorConsignacion = Color.Beige; 

        Color colorDefault = IndicadoresLotes.colorDefault; 
        Color colorCaducados = IndicadoresLotes.colorCaducados; 
        Color colorPrecaucion = IndicadoresLotes.colorPrecaucion; 
        Color colorStatusOk = IndicadoresLotes.colorStatusOk; 
        Color colorBloqueaCaducados = IndicadoresLotes.colorBloqueaCaducados; 
        Color colorSalidaCaducados = IndicadoresLotes.colorSalidaCaducados; 
        Color colorConsignacion = IndicadoresLotes.colorConsignacion;
        Color colorBloqueaVenta_ExisteConsignacion = IndicadoresLotes.colorBloqueaVenta_ExisteConsignacion;
        Color colorBloqueaVenta_NoExisteCuadroBasico = IndicadoresLotes.colorBloqueaVenta_NoExisteCuadroBasico;

        //clsOperacionesSupervizadas Permisos;
        string sPermisoCaducidades = "MODIFICAR_CADUCIDADES";
        bool bPermisoCaducidades = false;
        bool bCambioCaducidadesHabilitado = false; 

        public string sIdEmpresa = ""; 
        public string sIdEstado = "";
        public string sIdFarmacia = "";

        public string sIdCliente = "";
        public string sIdSubCliente = "";
        public string sIdPrograma = "";
        public string sIdSubPrograma = ""; 

        public string sIdArticulo = "";
        public string sCodigoEAN = "";
        public string sDescripcion = "";
        public int iTotalCantidad = 0;
        public bool bPermitirCapturaLotesNuevos = false;
        public bool bModificarCaptura = true;
        public bool bCapturandoLotes = false;
        public bool bEsEntrada = false;
        public int iMesesCadudaMedicamento = 12;
        public int iTipoCaptura = 0;
        public bool bEsTransferenciaDeEntrada = false;
        public bool bEsCancelacionCompras = false;
        public bool bEsConsignacion = false;
        public bool bPermitirLotesNuevosConsignacion = true;
        public bool bEsInventarioActivo = false;
        public bool bEsReubicacion = false;
        public bool bModificaCaducidades = false;

        private bool bBloqueaPorInventario = false;
        private bool bBloqueaPorInventarioAleatorio = false;
        private bool bEsControlado = false; 

        private string sTituloBloqueoInventario = "El producto se encuentra bloqueado por Inventario";
        private string sTituloNoEnCuadroBasico = "Clave SSA no incluida en Cuadro Básico de la unidad"; 

        ////private string sMensajeBloqueoInventario = "";
        ////private string sMensajeBloqueoInventarioAleatorio = ""; 

        /// <summary>
        /// Esta opcion restringira la Dispensación de Inventario de Venta si el Lote cuenta con existencia de Consignacion 
        /// </summary>
        private bool bPermitirDispensacionVenta_SiExisteConsignacion = true;
        private bool bMostrar_Msj_DispensarVenta_SiExisteConsignacion = false; 
        private int iExistenciaDeConsignacion = 0;
        private bool bEsDevolucion = false; 

        /// <summary>
        /// Esta opcion restringira la Dispensación de Venta si la Clave SSA no pertenece al cuadro básico de la unidad 
        /// </summary>
        private bool bVenta_ExisteEnCuadroBasico = true;


        /// <summary>
        /// Esta opcion solo se debe activar para los movimientos de Inventario que por su
        /// naturaleza necesiten dar Salida a Caducados 
        /// </summary>
        public bool bPermitirSacarCaducados = false;

        /// <summary>
        /// Verificar a nivel Programa-SubProgramama si la ClaveSSA permite la Dispensacion por Ampuleo 
        /// </summary>
        public bool  bCapturaEnMultiplosDeCajas_ProgramaSubPrograma = false;
        private bool bCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = false; 



        public DateTime dFechaDeSistema = DateTime.Now;
        public OrigenManejoLotes tpOrigenManLotes = OrigenManejoLotes.Default;
        public EncabezadosManejoLotes Encabezados = EncabezadosManejoLotes.Default;

        public DataSet dtsLotes = clsLotes.PreparaDtsLotes(); 
        public DataSet dtsLotesUbicaciones = clsLotesUbicaciones.PreparaDtsLotesUbicaciones();
        public DataSet dtsLotes_Destinos = clsLotesUbicaciones.PreparaDtsLotesUbicaciones(); 

        public DataSet dtsSubFarmacias = new DataSet();
        public DataSet dtsLotesUbicacionesRegistradas; 
        clsGrid myGrid; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leerSF; 
        clsConsultas query; 

        // int iFormato = 1;
        // string sFormato = "dd/MM/yyyy";
        bool bManejaUbicaciones = GnFarmacia.ManejaUbicaciones;
        bool bParametro_ForzarCapturaHabilitarValidaciones = GnFarmacia.ForzarCapturaEnMultiplosHabilitarValidaciones;
        bool bParametro_ForzarCapturaEnMultiplosDeCajas = GnFarmacia.ForzarCapturaEnMultiplosDeCajas;
        bool bParametro_ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = GnFarmacia.ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma;
        bool bParametro_ForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = GnFarmacia.ForzarCapturaEnMultiplosDeCajas_ClaveSSA;

        ////// string Hora = Global.FechaSistemaHora;
        DateTime dtpFechaSistema = General.FechaSistema;
        DateTime dtpFechaAvisoCaducidad = General.FechaSistema.AddMonths(18);

        ////// Necesarias para dar formato a las columnas del Grid 
        DateTimeCellType dateTimeCell_FechaRegistro = new DateTimeCellType();
        DateTimeCellType dateTimeCell_FechaCaducidad = new DateTimeCellType();

        // FarPoint.Win.Spread.Column colFechaRegistro;
        // FarPoint.Win.Spread.Column colFechaCaducidad;

        ////// Manejo de Ubicaciones 
        clsLotesUbicaciones myUbicacion = new clsLotesUbicaciones("", "");
        clsLotes_Reubicaciones myReubicacion = new clsLotes_Reubicaciones("", "");

        ////// Validar el Lote capturado 
        clsValidarLote validarLote; 

        // Variable para posiciones estandar
        public string sPosicionEstandar = "";

        /// <summary>
        /// Determina el Contenido Paquete del Producto que se esta manejando 
        /// </summary>
        int iContenidoPaqueteComercial = 0;

        /// <summary>
        /// Determina el Contenido Paquete de la Clave SSA, según catálogo 
        /// </summary>
        int iContenidoPaqueteClaveSSA = 0;

        bool bDispensacionActiva_Venta = GnFarmacia.DispensacionActiva_Venta;
        bool bDispensacionActiva_Consigna = GnFarmacia.DispensacionActiva_Consigna;

        string sMsj_Farmacia = "";
        string sMsj_Farmacia_Auxiliar = "";

        string sMsj_Almacen = "";
        string sMsj_Almacen_Auxiliar = "";


        public FrmCapturaLotes(DataRow []Rows, DataSet SubFarmacias)
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            leerSF = new clsLeer(); 
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            // Permisos especiales para modificar caducidades de lotes 
            SolicitarPermisosUsuario(); 

            this.Width = 1150;
            this.Height = 550; 
            // 878, 535 

            grdLotes.EditModeReplace = true;
            myGrid = new clsGrid(ref grdLotes, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 

            validarLote = new clsValidarLote(txtClaveLote); 


            cboSubFarmacias.Clear();
            cboSubFarmacias.Add();
            cboSubFarmacias.Add(SubFarmacias, true, "IdSubFarmacia", "SubFarmacia");
            dtsSubFarmacias = SubFarmacias; 

            lblTotal.Text = "0"; 

            cboTipoCaptura.Clear();
            cboTipoCaptura.Add("0", "<< Seleccione >>"); 
            cboTipoCaptura.Add("1", "Por Pieza"); 
            cboTipoCaptura.Add("2", "Por Paquete"); 
            //cboTipoCaptura.SelectedIndex = 0;

            myGrid.Limpiar(false);
            myGrid.AgregarRenglon( Rows, (int)Cols.Cantidad, false, true);

            //dtpFechaEntrada.MinDate = dtpFechaSistema;
            //dtpFechaEntrada.MaxDate = dtpFechaSistema;

            // FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            //dtpFechaCaducidad.MaxDate = dtpFechaSistema.AddMonths(iMesesCadudaMedicamento); 

            //////dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd";  // Fecha de Registro  
            //////dateTimeCellType2.UserDefinedFormat = "yyyy-MM";  // Fecha de Caducidad 

            //////////colorConsignacion = Color.LightSteelBlue;  
            ////colorBloqueaVenta_ExisteConsignacion = Color.AntiqueWhite;
            ////colorBloqueaVenta_ExisteConsignacion = Color.CadetBlue;
            ////colorBloqueaVenta_ExisteConsignacion = Color.DarkSeaGreen;
            ////colorBloqueaVenta_ExisteConsignacion = Color.ForestGreen;  

            ////colorBloqueaVenta_ExisteConsignacion = Color.Khaki;  
            ///

            int iH = FrameLotes.Height;
            iH += 0;

            FrameLotes.Height = FrameLotes.Top + ( this.Height - ( lblAyuda.Top + iH) );
            FrameLotes.Height = lblAyuda.Top - FrameLotes.Top;
            FrameLotes.Height -= lblAyuda.Height;

        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bPermisoCaducidades = DtGeneral.PermisosEspeciales.TienePermiso(sPermisoCaducidades);
        }
        #endregion Permisos de Usuario 

        private void FrmCapturaLotes_Load(object sender, EventArgs e)
        {
            bool bHabilitarCaptura = true; 

            // Jesus Diaz 2K120217.1520  
            if (!bPermitirSacarCaducados)
            {
                dtpFechaCaducidad.MinDate = dtpFechaSistema;
            }

            gpoDatosLotes.Left = grdLotes.Left;
            gpoDatosLotes.Top = grdLotes.Top;
            gpoDatosLotes.Height = grdLotes.Height;
            gpoDatosLotes.Width = grdLotes.Width;
            gpoDatosLotes.Visible = false;

            lblAyudaAux.AutoSize = false;
            lblAyudaAux.Height = lblAyuda.Height; 

            lblAyuda.Dock = DockStyle.Bottom;
            lblAyudaAux.Dock = DockStyle.Bottom; 


            lblCodigo.Text = sIdArticulo; 
            lblArticulo.Text = sDescripcion;
            lblCodigoEAN.Text = sCodigoEAN; 
            CargarInformacionDelProducto();


            if (!DtGeneral.ManejaControlados & bEsControlado)
            {
                bHabilitarCaptura = false;
            }


            cboTipoCaptura.Data = iTipoCaptura.ToString();
            cboTipoCaptura.Focus();

            dtpFechaEntrada.Value = dtpFechaSistema;
            dtpFechaEntrada.Enabled = false;

            if (cboTipoCaptura.SelectedIndex != 0)
            {
                cboTipoCaptura.Enabled = false;
            }
            else
            {
                grdLotes.Enabled = true;
            }


            //// Actualizar los encabezados 
            TitulosColumnas();
            ConfigurarColumnas(); 

            if (bModificarCaptura)
            {
                if (bPermitirCapturaLotesNuevos)
                {
                    if (myGrid.GetValue(1, 1) == "" && bHabilitarCaptura)
                    {
                        myGrid.Limpiar(false); 
                        General.msjAviso("Codigo EAN sin LOTES; Favor de capturar LOTES.");
                        MostrarCapturaLotes();
                    }
                }
            }
            else
            {
                myGrid.BloqueaColumna(true, (int)Cols.Cantidad);
            }
            myGrid.SetActiveCell(1, (int)Cols.Cantidad);
            lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();


            lblProductoBloqueadoPorInventario.Text = sTituloBloqueoInventario; 
            lblProductoBloqueadoPorInventario.Visible = false;
            lblModificarCaducidades.Visible = false; 
            
            // VerificaInventarioProducto();
            VerificaProductoMarcadoInventario();

            //// Jesús Díaz 2K120710.1640 
            bMostrar_Msj_DispensarVenta_SiExisteConsignacion = false; 
            if (tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion)
            {
                bPermitirDispensacionVenta_SiExisteConsignacion = GnFarmacia.PermitirDispensacionVenta_ConExistenciaConsiganacion;
            }

            //// Jesús Díaz 2K121125.1700 
            if ( tpOrigenManLotes == OrigenManejoLotes.Inventarios )
            {
                if (bModificaCaducidades && bPermisoCaducidades) 
                {
                    bCambioCaducidadesHabilitado = true;
                    lblModificarCaducidades.Visible = true; 
                } 
            } 

            //// Jesús Díaz 2K130716.1600 
            bVenta_ExisteEnCuadroBasico = true; 
            if (tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion)
            {
                CargarInformacionCuadroBasico();
            }

            AjustarMensajeTeclasRapidas();    
            Ordernar_RevisarFechaCaducidad();   
            Mostrar_Msj_DispensarVenta_SiExisteConsignacion();

            //pGrid.Sheets[iPageActiva].Columns.Default.Resizable = bModificarAnchoColumnas; 
            // grdLotes.Sheets[0].Columns[(int)Cols.FechaCad-1].Formatter 


            sMsj_Farmacia = " ( F8 ) Ver/Ocultar  Agregar LOTE nuevo                                                     ( SUPR/DEL ) Quitar LOTES                                             ( F12 ) Cerrar/Salir";
            sMsj_Farmacia_Auxiliar = " ( F12 ) Cerrar/Salir ";


            sMsj_Almacen = " ( F8 ) Ver/Ocultar  Agregar LOTE nuevo                             ( F10 ) Ubicaciones                             ( SUPR/DEL ) Quitar LOTES                      ( F12 ) Cerrar/Salir";
            sMsj_Almacen_Auxiliar = "  ( F10 ) Ubicaciones                                                                                                                                                                                                                  ( F12 ) Cerrar/Salir   ";


            //// Solo almacenes 
            lblAyuda.Text = " ( F8 ) Ver/Ocultar  Agregar LOTE nuevo                    ( SUPR/DEL ) Quitar LOTES                      ( F12 ) Cerrar/Salir";
            lblAyuda.Text = " ( F8 ) Ver/Ocultar  Agregar LOTE nuevo                                           ( SUPR/DEL ) Quitar LOTES                            ( F12 ) Cerrar/Salir"; // Final 
            lblAyudaAux.Text = " ( F12 ) Cerrar/Salir ";

            lblAyuda.Text = sMsj_Farmacia;
            lblAyudaAux.Text = sMsj_Almacen_Auxiliar;


            if (bManejaUbicaciones)
            {
                //lblAyuda.Text = " <F8> Ver / Ocultar   Agregar nuevo lote      <F10> Ubicaciones        <SUPR/DEL> Borrar lotes agregados              <F12> Cerrar";
                //lblAyudaAux.Text = "  <F10>Ubicaciones                                                                                                                                                              <F12> Cerrar   ";

                //lblAyuda.Text = " <F8> Ver / Ocultar   Agregar nuevo lote           <F10> Ubicaciones             <SUPR/DEL> Borrar lotes agregados                   <F12> Cerrar";
                //lblAyudaAux.Text = "  <F10>Ubicaciones                                                                                                                                                                             <F12> Cerrar   ";
                lblAyuda.Text = sMsj_Almacen;
                lblAyudaAux.Text = sMsj_Almacen_Auxiliar;


                myGrid.BloqueaColumna(true, (int)Cols.Cantidad, false);
            }

            //// Asignar Contenido Paquete 
            myGrid.SetValue((int)Cols.ContenidoPaquete, lblContenido.Text);


            //// Manejo de Controlados 
            if (!DtGeneral.ManejaControlados & bEsControlado)
            {
                bPermitirCapturaLotesNuevos = false;
                //// No permitir ningun tipo de modificación y/o captura de cantidades 
                myGrid.BloqueaColumna(true, (int)Cols.EsVentaBloqueada, false);
                General.msjAviso("La Unidad actual no está habilitada para el Manejo de Controlados.");
            }

        }

        private void TitulosColumnas()
        {
            //myGrid.PonerEncabezado((int)Cols.IdSubFarmacia, "Id Fuente");
            //myGrid.PonerEncabezado((int)Cols.SubFarmacia, "Fuente Financ.");

            if (Encabezados == EncabezadosManejoLotes.EsTransferenciaDeEntrada)
            {
                myGrid.PonerEncabezado((int)Cols.Existencia, "Cantidad Enviada");
                myGrid.PonerEncabezado((int)Cols.Cantidad, "Cantidad Recibida");
            }
            else if (Encabezados == EncabezadosManejoLotes.EsDevolucionDeCompras)
            {
                myGrid.PonerEncabezado((int)Cols.Existencia, "Cantidad Recibida");
                myGrid.PonerEncabezado((int)Cols.Cantidad, "Cantidad Devuelta");
            }
            else if (Encabezados == EncabezadosManejoLotes.EsDevolucionDeVentas)
            {
                myGrid.PonerEncabezado((int)Cols.Existencia, "Cantidad Entregada");
                myGrid.PonerEncabezado((int)Cols.Cantidad, "Cantidad Devuelta");
            } 
        }

        private void ConfigurarColumnas()
        {
            int iAncho = ((int)grdLotes.Sheets[0].Columns[(int)Cols.Existencia_Disponible - 1].Width) / 2;

            bEsDevolucion = false; 
            switch (Encabezados)
            {
                case EncabezadosManejoLotes.EsDevolucionDeCompras: 
                case EncabezadosManejoLotes.EsDevolucionDeOrdenesDeCompras: 
                case EncabezadosManejoLotes.EsDevolucionDeVentas: 
                case EncabezadosManejoLotes.EsDevolucionEntradaConsignacion: 
                case EncabezadosManejoLotes.EsDevolucionPedidoDistribuidor:
                    bEsDevolucion = true; 
                    break; 

                default:
                    grdLotes.Sheets[0].Columns[(int)Cols.Existencia_Disponible - 1].Width = 0;
                    grdLotes.Sheets[0].Columns[(int)Cols.SubFarmacia - 1].Width += iAncho;
                    grdLotes.Sheets[0].Columns[(int)Cols.ClaveLote - 1].Width += iAncho; 
                    break; 
            }
        }

        private void CargarInformacionDelProducto()
        {
            leer.DataSetClase = query.Productos(lblCodigo.Text, "CargarInformacionDelProducto()");
            if (leer.Leer())
            {
                bEsControlado = leer.CampoBool("EsControlado"); 
                lblClaveSSA.Text = leer.Campo("ClaveSSA_Aux");
                lblDescripcionSSA.Text = leer.Campo("DescripcionSal");
                iContenidoPaqueteComercial = leer.CampoInt("ContenidoPaquete");
                iContenidoPaqueteClaveSSA = leer.CampoInt("ContenidoPaquete_ClaveSSA");

                lblPresentacion.Text = leer.Campo("Presentacion");
                lblContenido.Text = leer.Campo("ContenidoPaquete");

                if (tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion && DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno && bParametro_ForzarCapturaHabilitarValidaciones)
                {
                    lblArticulo.Text = string.Format("{0}           Presentación :  {1}       Contenido :  {2}",
                        lblArticulo.Text, leer.Campo("Presentacion"), leer.Campo("ContenidoPaquete"));

                    CargarInformacion_ClaveLicitada();
                }
                else
                {
                    lblPresentacion.Text = leer.Campo("Presentacion");
                    lblContenido.Text = leer.Campo("ContenidoPaquete");
                }
            }

            //CargarInformacion_ProgramaSubPrograma__ControlDispensacion();
        }

        private void CargarInformacion_ClaveLicitada()
        {
            leer.DataSetClase = query.ClavesSSA_PreciosAsignados(sIdEstado, sIdCliente, sIdSubCliente, lblClaveSSA.Text, "CargarInformacion_ClaveLicitada()");
            if (leer.Leer())
            {
                lblPresentacion.Text = leer.Campo("Presentacion_Licitado");
                lblContenido.Text = leer.Campo("ContenidoPaquete_Licitado");
            }
        }

        private void CargarInformacionCuadroBasico()
        {
            string sSql = ""; 

            ////sSql = string.Format("Select ClaveSSA, DescripcionClave " +
            ////    "From vw_CB_CuadroBasico_Farmacias P (NoLock) " + 
            ////    "Where IdEstado = '{0}' and IdFarmacia = '{1}' and ClaveSSA = '{2}' and StatusMiembro = 'A' and StatusClave = 'A'  ",
            ////    sIdEstado, sIdFarmacia, lblClaveSSA.Text );

            sSql = string.Format("Exec spp_Validar_ClaveSSA_EnPerfil " +
                " @IdEstado = '{0}', @IdFarmacia = '{1}', @ClaveSSA = '{2}', @TipoUnidad = '{3}', @IdCliente = '{4}', @IdSubCliente = '{5}' ", 
                sIdEstado, sIdFarmacia, lblClaveSSA.Text, Convert.ToInt32(DtGeneral.EsAlmacen), sIdCliente, sIdSubCliente ); 

            if (leer.Exec(sSql))
            {
                if (!leer.Leer())
                {
                    bVenta_ExisteEnCuadroBasico = false;
                    lblProductoBloqueadoPorInventario.Text = sTituloNoEnCuadroBasico;
                    lblProductoBloqueadoPorInventario.Visible = true;
                }
                else
                {
                    bCapturaEnMultiplosDeCajas_ClaveSSA_Especifica = leer.CampoBool("ForzarDispensacion_CajasCompletas"); 
                }
            }
        }


        ////private void VerificaInventarioProducto()
        ////{
        ////    bBloqueaPorInventario = false; 
        ////    if (!bEsInventarioActivo) 
        ////    {
        ////        string sSql = string.Format("Select IdProducto " +
        ////            "From vw_Productos_Bloqueados_Por_Inventario P (NoLock) ");  

        ////        sSql = string.Format("Select IdProducto " +
        ////            "From vw_Productos_Bloqueados_Por_Inventario P (NoLock) " + 
        ////            "Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' ", 
        ////            sIdEstado, sIdFarmacia, sIdArticulo );

        ////        if (leer.Exec(sSql))
        ////        {
        ////            // Bloquear la captura si se encuentra el producto 
        ////            bBloqueaPorInventario = leer.Leer(); 
        ////        }
        ////    }

        ////    // if (bBloqueaPorInventario)
        ////    {
        ////        lblProductoBloqueadoPorInventario.Visible = bBloqueaPorInventario;
        ////        if (bModificarCaptura)
        ////        {
        ////            myGrid.BloqueaColumna(bBloqueaPorInventario, (int)Cols.Cantidad);
        ////        }
        ////    }
        ////}

        private void VerificaProductoMarcadoInventario()
        {
            bBloqueaPorInventario = false;
            if (!bEsInventarioActivo)
            {
                string sSql = string.Format("Select IdProducto " + 
                    "From vw_Productos_Bloqueados_Por_Inventario P (NoLock) ");

                sSql = string.Format("Select IdProducto, Upper(Status) as Status " + 
                    "From FarmaciaProductos P (NoLock) " + 
                    "Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' and Status in ( 'I', 'S' ) ",
                    sIdEstado, sIdFarmacia, sIdArticulo);

                if (leer.Exec(sSql))
                {
                    // Bloquear la captura si se encuentra el producto 
                    bBloqueaPorInventario = leer.Leer();
                    bBloqueaPorInventarioAleatorio = leer.Campo("Status") == "S"; 
                }
            }

            lblProductoBloqueadoPorInventario.Visible = bBloqueaPorInventario;
            if (bModificarCaptura)
            {
                myGrid.BloqueaColumna(bBloqueaPorInventario, (int)Cols.Cantidad);
            }

            if (bBloqueaPorInventarioAleatorio)
            {
                myGrid.BloqueaColumna(bBloqueaPorInventario, (int)Cols.Cantidad);
                myGrid.SetValue((int)Cols.Existencia, 0); 
            } 
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //case Keys.Delete:
                //    EliminarRenglon();
                //    break;
                case Keys.F5:
                    break; 

                case Keys.F6:
                    if (bCambioCaducidadesHabilitado)
                    {
                        ModificarCaducidad(); 
                    }
                    break; 

                case Keys.F8:
                    if (bPermitirCapturaLotesNuevos)
                    {
                        MostrarCapturaLotes();
                    }
                    break;


                case Keys.F10:
                    if (bManejaUbicaciones)
                    {
                        if (!bEsReubicacion)
                        {
                            MostrarUbicaciones(); 
                        }
                        else
                        {
                            MostrarReubicaciones(); 
                        }
                    }
                    break; 

                case Keys.F12:
                    Salir_ValidarCaptura(); 
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            bCapturandoLotes = false;
            gpoDatosLotes.Visible = false;
            grdLotes.Enabled = true;
        }

        private bool EsSubFarmaciaConsignacion()
        {
            bool bRegresa = false;

            try
            {
                leerSF.DataRowsClase = dtsSubFarmacias.Tables[0].Select(string.Format(" IdSubFarmacia = '{0}' ", cboSubFarmacias.Data));
                leerSF.Leer();
                bRegresa = leerSF.CampoBool("EsConsignacion"); 

            }
            catch { } 

            return bRegresa; 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            txtClaveLote.Text = txtClaveLote.Text.Trim().Replace(" ", "");
            string sLote = txtClaveLote.Text ;
            string sValorBuscar = "";
            string sMsjConsignacion = string.Format("El LOTE <<[ {0} ]>> no contiene el caracter <<[ * ]>>, no es una Clave de Consignación válida.", sLote.ToUpper());
            bool bAgregar = true;
            bool bEsLoteConsignacion = sLote.Contains("*");
            bool bEsSubFarmaciaDeConsignacion = EsSubFarmaciaConsignacion();
            int dMonth = 0;
            string sFecha = "";
            int iError = 0;
            scDateTimePicker dtpDiff = new scDateTimePicker();
            DateTime dtFecha;

            //// 2K110105-1155    Solo se habilita cuando es EPC 
            if (bEsConsignacion)
            {
                // if (Fg.Mid(sLote, 1, 1) != "*")
                if (!bEsLoteConsignacion)
                {
                    bAgregar = false;
                    iError = 2;
                    General.msjAviso(sMsjConsignacion);
                }
            }

            // Asegurar que se permita la captura de lotes nuevos 
            bAgregar = bAgregar && sLote != "";
            if (bAgregar && bEsLoteConsignacion)
            {
                if (!bPermitirLotesNuevosConsignacion)
                {
                    bAgregar = false;
                    iError = 2;
                    General.msjAviso("No esta permitido el registro de Lotes de Consignacion, verifique.");
                }
            }

            // Verificar que se seleccione una Sub-Farmacia  // 2K110323-1213 
            if (bAgregar)
            {
                if (cboSubFarmacias.SelectedIndex == 0)
                {
                    bAgregar = false;
                    iError = 1;
                    General.msjAviso("F. Financiamiento no valida, Favor de verificar.");
                }
            }

            //// Jesus Diaz 2K111017.1441 
            if (bAgregar && bEsLoteConsignacion)
            {
                if (!bEsSubFarmaciaDeConsignacion)
                {
                    bAgregar = false;
                    iError = 1;
                    General.msjAviso("F. Financiamiento no es de Consignación, Favor de verificar.");
                }
            }

            //// Jesús Díaz 2K111110.0931  
            //// Jesús Díaz 2K130716.1630   
            if (bAgregar && bEsSubFarmaciaDeConsignacion)
            {
                bAgregar = FormatoValidoLoteConsignacion();
                iError = 2;
            }

            // if (sLote != "" && bAgregar) 
            if (!bAgregar)
            {
                switch (iError)
                {
                    case 2:
                        txtClaveLote.Focus();
                        break;

                    case 1:
                    default:
                        cboSubFarmacias.Focus();
                        break;
                }
                // txtClaveLote.Focus();
                //cboSubFarmacias.Focus(); 
            }
            else
            {
                sValorBuscar = cboSubFarmacias.Data + lblCodigo.Text + sLote;
                int[] Columnas = { (int)Cols.IdSubFarmacia, (int)Cols.Codigo, (int)Cols.ClaveLote };

                if (myGrid.BuscarRepetidosColumnas(sValorBuscar, Columnas) != 0)
                {
                    General.msjAviso("LOTE registrado, Favor de verificar.");
                }
                else
                {
                    myGrid.AddRow();
                    int iActiveRow = myGrid.Rows;

                    myGrid.SetValue(iActiveRow, (int)Cols.IdSubFarmacia, cboSubFarmacias.Data);
                    myGrid.SetValue(iActiveRow, (int)Cols.SubFarmacia, cboSubFarmacias.Text);

                    myGrid.SetValue(iActiveRow, (int)Cols.Codigo, sIdArticulo);
                    myGrid.SetValue(iActiveRow, (int)Cols.CodigoEAN, sCodigoEAN);
                    myGrid.SetValue(iActiveRow, (int)Cols.ClaveLote, sLote);

                    dMonth = (int)dtpDiff.DateDiff(DateInterval.Month, dtpFechaEntrada.Value, dtpFechaCaducidad.Value);
                    myGrid.SetValue(iActiveRow, (int)Cols.MesesPorCaducar, dMonth);

                    // Para evitar error al Manejar el Mes de Febrero 
                    dtFecha = new DateTime(dtpFechaCaducidad.Value.Year, dtpFechaCaducidad.Value.Month, 1);
                    sFecha = General.FechaYMD(dtFecha).ToString();

                    myGrid.SetValue(iActiveRow, (int)Cols.FechaEnt, General.FechaYMD(dtpFechaEntrada.Value));
                    myGrid.SetValue(iActiveRow, (int)Cols.FechaCad, General.FechaYMD(dtFecha));
                    myGrid.SetValue(iActiveRow, (int)Cols.Status, "Activo");
                    myGrid.SetValue(iActiveRow, (int)Cols.Existencia, 0);    //Existencia
                    myGrid.SetValue(iActiveRow, (int)Cols.Existencia_Disponible, 0);    //Existencia 
                    myGrid.SetValue(iActiveRow, (int)Cols.Cantidad, 0);    //Cantidad 
                    myGrid.SetValue(iActiveRow, (int)Cols.AddColumna, 1);    // Lote agregado  

                    // Revisar la Caducidad del nuevo lote 
                    Ordernar_RevisarFechaCaducidad();   
                    Mostrar_Msj_DispensarVenta_SiExisteConsignacion();     

                    bCapturandoLotes = false;
                    gpoDatosLotes.Visible = false;
                    grdLotes.Enabled = true;
                    grdLotes.Focus();
                    lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
                    myGrid.SetActiveCell(iActiveRow, (int)Cols.Cantidad);
                }
            }
        }

        private bool FormatoValidoLoteConsignacion()
        {
            bool bRegresa = true;
            string sValor = txtClaveLote.Text.Trim();
            string s = "";
            int iVeces = 0;
            string sMsjError = ""; 

            for (int i = 1; i <= sValor.Length; i++)
            {
                s = Fg.Mid(sValor, i, 1);
                if (s.Contains("*"))
                {
                    iVeces++;
                }

                if (iVeces > 1)
                {
                    bRegresa = false;
                    sMsjError = string.Format("El LOTE [ {0} ] contiene mas de un caracter [ * ], formato inválido, Favor de verificar. ", sValor); 
                    break;
                }
            }

            if (bRegresa)
            {
                s = Fg.Mid(sValor, 1, 1);
                if (s != "*")
                {
                    bRegresa = false;
                    sMsjError = string.Format("El LOTE [ {0} ] no contiene el caracter [ * ] al inicio, formato inválido, Favor de verificar. ", sValor); 
                }
            }

            if (bRegresa && sValor.Length < 2)
            {
                bRegresa = false;
                sMsjError = "El LOTE debe contener al menos un caracter aparte del [ * ], formato inválido, Favor de verificar. ";
            }


            if (!bRegresa)
            {
                General.msjAviso(sMsjError);
            }

            return bRegresa;
        }

        private void MostrarCapturaLotes()
        {
            if (!bEsReubicacion)
            {
                if (!bCapturandoLotes)
                {
                    Fg.IniciaControles(this, true, gpoDatosLotes);
                    //txtClaveLote.Focus();
                    
                    if (cboSubFarmacias.NumeroDeItems <= 2)
                    {
                        cboSubFarmacias.SelectedIndex = cboSubFarmacias.NumeroDeItems - 1;
                        cboSubFarmacias.Enabled = false; 
                        ///cboSubFarmacias.Focus(); 
                    } 


                    if (cboSubFarmacias.Enabled)
                    {
                        cboSubFarmacias.Focus();
                    }

                    //// 2K110105-1155    Solo se habilita cuando es EPC 
                    if (bEsConsignacion)
                    {
                        txtClaveLote.Text = "*";
                        SendKeys.Send("{RIGHT}");
                    }
                }
                dtpFechaEntrada.Enabled = false;

                bCapturandoLotes = !bCapturandoLotes;
                gpoDatosLotes.Visible = bCapturandoLotes;
                grdLotes.Enabled = !bCapturandoLotes;
            }
        }

        private void MostrarUbicaciones()
        {
            int iRow = myGrid.ActiveRow; 
            if (!myGrid.GetValueBool(iRow, (int)Cols.EsVentaBloqueada))
            {
                MostrarUbicaciones_Almacen(); 
            }
        }

        private void MostrarUbicaciones_Almacen()
        {
            int iRow = myGrid.ActiveRow;
            int iMesesCaducidad = myGrid.GetValueInt(iRow, (int)Cols.MesesPorCaducar);
            string sIdSubFarmacia = myGrid.GetValue(iRow, (int)Cols.IdSubFarmacia);
            string sSKU = myGrid.GetValue(iRow, (int)Cols.SKU);
            string sClaveLote = myGrid.GetValue(iRow, (int)Cols.ClaveLote); 
            bool bCargarPantallaUbicaciones = true;
            int iExistenciaDisponible = myGrid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
            //string sFLO = ""; // myGrid.GetValue(iRow, (int)Cols.FLO);

            ////if (iMesesCaducidad <= 0)
            ////{
            ////    bCargarPantallaUbicaciones = false; 
            ////} 

            if (bCargarPantallaUbicaciones & !bCapturandoLotes)
            {
                myUbicacion = new clsLotesUbicaciones(sIdEstado, sIdFarmacia, sIdSubFarmacia, sSKU, sIdArticulo, sCodigoEAN, sClaveLote, dtsLotesUbicaciones);

                myUbicacion.UbicacionesRegistradas = dtsLotesUbicacionesRegistradas; 
                myUbicacion.Codigo = sIdArticulo; 
                myUbicacion.CodigoEAN = sCodigoEAN;

                myUbicacion.SKU = sSKU; 
                myUbicacion.ClaveLote = sClaveLote; 
                myUbicacion.DescripcionProducto = lblArticulo.Text; 
                myUbicacion.DispensarPor = lblPresentacion.Text; 
                myUbicacion.ContenidoPaquete = lblContenido.Text; 
                myUbicacion.ClaveSSA = lblClaveSSA.Text; 
                myUbicacion.ClaveSSA_Descripcion = lblDescripcionSSA.Text; 
                myUbicacion.CapturarUbicaciones = bPermitirCapturaLotesNuevos; 


                myUbicacion.bPermitirCapturaUbicacionesNuevas = bPermitirCapturaLotesNuevos; 
                myUbicacion.bModificarCaptura = bModificarCaptura; 
                myUbicacion.bEsEntrada = bEsEntrada; 
                myUbicacion.bEsTransferenciaDeEntrada = bEsTransferenciaDeEntrada;
                myUbicacion.bEsCancelacionCompras = bEsCancelacionCompras; 
                myUbicacion.bEsConsignacion = bEsConsignacion; 
                myUbicacion.bPermitirLotesNuevosConsignacion = bPermitirLotesNuevosConsignacion; 
                myUbicacion.bEsInventarioActivo = bEsInventarioActivo;

                myUbicacion.bBloqueaPorInventario = bBloqueaPorInventario;
                myUbicacion.bEsDevolucion = bEsDevolucion; 

                myUbicacion.bPermitirSacarCaducados = bPermitirSacarCaducados;
                myUbicacion.iExistenciaDisponible = iExistenciaDisponible;

                myUbicacion.sPosicionEstandar = sPosicionEstandar;

                //myUbicacion.FLO = sFLO;

                if (!bPermitirSacarCaducados) 
                {
                    myUbicacion.bEsCaducadudo = iMesesCaducidad <= 0 ? true : false;
                }


                // myUbicacion.DataSetLotesUbicaciones = this.dtsLotesUbicaciones;

                myUbicacion.Show();
                this.dtsLotesUbicaciones = myUbicacion.DataSetLotesUbicaciones;
                myGrid.SetValue(iRow, (int)Cols.Cantidad, myUbicacion.CantidadTotal);
                myGrid.SetActiveCell(iRow, (int)Cols.Cantidad); 
                lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
            }
        }

        private void MostrarReubicaciones()
        {
            int iRow = myGrid.ActiveRow;
            int iMesesCaducidad = myGrid.GetValueInt(iRow, (int)Cols.MesesPorCaducar);
            int iExistenciaActual = myGrid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
            string sIdSubFarmacia = myGrid.GetValue(iRow, (int)Cols.IdSubFarmacia);
            string sSKU = myGrid.GetValue(iRow, (int)Cols.SKU);
            string sClaveLote = myGrid.GetValue(iRow, (int)Cols.ClaveLote);
            bool bCargarPantallaUbicaciones = true;

            //string sFLO = ""; // myGrid.GetValue(iRow, (int)Cols.FLO);

            if (iExistenciaActual == 0)
            {
                General.msjUser("No se puede reubicar un Lote sin existencia, verifique.");
            }
            else
            {
                if (bCargarPantallaUbicaciones)
                {
                    myReubicacion = new clsLotes_Reubicaciones(sIdEstado, sIdFarmacia, sIdSubFarmacia, sSKU, sIdArticulo, sCodigoEAN, sClaveLote, dtsLotesUbicaciones);

                    myUbicacion.SKU = sSKU;
                    myReubicacion.Codigo = sIdArticulo;
                    myReubicacion.CodigoEAN = sCodigoEAN;
                    myReubicacion.ClaveLote = sClaveLote;
                    myReubicacion.DescripcionProducto = lblArticulo.Text;
                    myReubicacion.DispensarPor = lblPresentacion.Text;
                    myReubicacion.ContenidoPaquete = lblContenido.Text;
                    myReubicacion.ClaveSSA = lblClaveSSA.Text;
                    myReubicacion.ClaveSSA_Descripcion = lblDescripcionSSA.Text;
                    myReubicacion.CapturarUbicaciones = bPermitirCapturaLotesNuevos;
                    myReubicacion.ExistenciaActual = iExistenciaActual;

                    myReubicacion.bPermitirCapturaUbicacionesNuevas = bPermitirCapturaLotesNuevos;
                    myReubicacion.bModificarCaptura = bModificarCaptura;
                    myReubicacion.bEsEntrada = bEsEntrada;
                    myReubicacion.bEsTransferenciaDeEntrada = bEsTransferenciaDeEntrada;
                    myReubicacion.bEsCancelacionCompras = bEsCancelacionCompras;
                    myReubicacion.bEsConsignacion = bEsConsignacion;
                    myReubicacion.bPermitirLotesNuevosConsignacion = bPermitirLotesNuevosConsignacion;
                    myReubicacion.bEsInventarioActivo = bEsInventarioActivo;

                    //myUbicacion.FLO = sFLO;     //20231122.1119FAV

                    myReubicacion.DataSetLotes_Destinos = this.dtsLotes_Destinos;

                    myReubicacion.Show();
                    
                    this.dtsLotesUbicaciones = myReubicacion.DataSetLotesUbicaciones;
                    this.dtsLotes_Destinos = myReubicacion.DataSetLotes_Destinos;//AQUI.

                    myGrid.SetValue(iRow, (int)Cols.Cantidad, myReubicacion.CantidadTotal);
                    myGrid.SetActiveCell(iRow, (int)Cols.Cantidad);
                    lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
                }
            }
        }

        private bool validarCaptura_De_Cantidades(int Renglon)
        {
            bool bRegresa = true; 

            int iRow = Renglon;
            int iExistencia = myGrid.GetValueInt(iRow, (int)Cols.Existencia_Disponible);
            int iCantidad = myGrid.GetValueInt(iRow, (int)Cols.Cantidad);

            //// Evaluar las Salidas y Devoluciones 
            if (!bEsEntrada)
            {
                if (iCantidad > iExistencia)
                {
                    bRegresa = false; 
                    myGrid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
                    myGrid.SetActiveCell(iRow, (int)Cols.Cantidad);
                    
                    if (bEsTransferenciaDeEntrada)
                    {
                        General.msjUser("La cantidad recibida no puede ser mayor a la cantidad enviada, verifique.");
                    }
                    else
                    {
                        General.msjUser("La cantidad disponible no es suficiente para cubrir la cantidad solicitada.");
                    }
                }
            }

            if (bRegresa)
            {
                validarCaptura_De_Cantidades_EnMultiplos(iRow, iCantidad); 
            }

            return bRegresa; 
        }

        private void grdLotes_EditModeOff(object sender, EventArgs e)
        {
            int iRow = myGrid.ActiveRow; 
            int iExistencia = myGrid.GetValueInt(iRow, (int)Cols.Existencia_Disponible); 
            int iCantidad = myGrid.GetValueInt(iRow, (int)Cols.Cantidad);
            bool bResiduoValido = myGrid.GetValueInt(iRow, (int)Cols.Residuo) == 0;
            int iPaquete = Convert.ToInt32("0" + lblContenido.Text);
            bool bContinuar = true;


            validarCaptura_De_Cantidades(iRow);
            lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();

            ////if (!bEsEntrada)
            ////{
            ////    if (iCantidad > iExistencia)
            ////    {
            ////        myGrid.SetValue(iRow, (int)Cols.Cantidad, iExistencia);
            ////        myGrid.SetActiveCell(iRow, (int)Cols.Cantidad); 
            ////        if (bEsTransferenciaDeEntrada)
            ////        {
            ////            General.msjUser("La cantidad recibida no puede ser mayor a la cantidad enviada, verifique.");
            ////        }
            ////        else
            ////        {
            ////            General.msjUser("La cantidad disponible no es suficiente para cubrir la cantidad solicitada.");
            ////        }
            ////    }
            ////}
        }

        private void grdLotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (myGrid.GetValueInt(myGrid.ActiveRow, (int)Cols.AddColumna) == 1)
                {
                    myGrid.DeleteRow(myGrid.ActiveRow);
                    Ordernar_RevisarFechaCaducidad(); 
                }
            }
        }

        private void EliminarRenglon()
        {
            //if (this.ActiveControl.Name.ToUpper() == grdLotes.Name.ToUpper())
            //{
            //    int iRow = myGrid.ActiveRow;
            //    if (myGrid.GetValueInt(iRow, (int)Cols.AddColumna) == 1)
            //        myGrid.DeleteRow(iRow);
            //}
        }

        private void FrmCapturaLotes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //////dtsLotes = clsLotes.PreparaDtsLotes();

            //////for (int i = 1; i <= myGrid.Rows; i++)
            //////{
            //////    object[] objRow = {
            //////        myGrid.GetValue(i, (int)Cols.IdSubFarmacia),  
            //////        myGrid.GetValue(i, (int)Cols.SubFarmacia),  
            //////        myGrid.GetValue(i, (int)Cols.Codigo),  
            //////        myGrid.GetValue(i, (int)Cols.CodigoEAN),  
            //////        myGrid.GetValue(i, (int)Cols.ClaveLote),  
            //////        myGrid.GetValueInt(i, (int)Cols.MesesPorCaducar),  
            //////        myGrid.GetValueFecha(i, (int)Cols.FechaEnt),  
            //////        myGrid.GetValueFecha(i, (int)Cols.FechaCad),  
            //////        myGrid.GetValue(i, (int)Cols.Status),
            //////        myGrid.GetValueInt(i, (int)Cols.Existencia),
            //////        myGrid.GetValueInt(i, (int)Cols.Existencia_Disponible), 
            //////        myGrid.GetValueInt(i, (int)Cols.Cantidad) };
            //////    dtsLotes.Tables[0].Rows.Add(objRow);
            //////}
            //////iTotalCantidad = myGrid.TotalizarColumna((int)Cols.Cantidad);
            //////iTipoCaptura = Convert.ToInt32(cboTipoCaptura.Data);
        }

        private void Salir_ValidarCaptura()
        {
            bool bRegresa = true;

            if (bModificarCaptura)
            {
                for (int i = 1; i <= myGrid.Rows; i++)
                {
                    if (!validarCaptura_De_Cantidades(i))
                    {
                        lblTotal.Text = myGrid.TotalizarColumna((int)Cols.Cantidad).ToString();
                        bRegresa = false;
                        break;
                    }
                }
            }


            if (bRegresa)
            {
                Salir_ValidarCaptura_Actualizar_Informacion();
            }
        }
        
        private void Salir_ValidarCaptura_Actualizar_Informacion()
        {
            bool bSalidaValida = true;

            bSalidaValida = validarCaptura_De_Cantidades_EnMultiplos(0, myGrid.TotalizarColumna((int)Cols.Cantidad));

            if (bSalidaValida)
            {
                Actualizar_Datos_De_Salida();
                this.Close();
            }
        }

        private bool validarCaptura_De_Cantidades_EnMultiplos(int Renglon, int Cantidad)
        {
            bool bForzarMultiplosDeCaja_ValidacionLocal = bParametro_ForzarCapturaEnMultiplosDeCajas;
            
            if (bParametro_ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma)
            {
                bForzarMultiplosDeCaja_ValidacionLocal = bCapturaEnMultiplosDeCajas_ProgramaSubPrograma;  
            }

            if (tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion && DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno)
            {
                if (bParametro_ForzarCapturaEnMultiplosDeCajas_ClaveSSA_Especifica)
                {
                    //bCapturaEnMultiplosDeCajas_ProgramaSubPrograma = true;
                    //bParametro_ForzarCapturaEnMultiplosDeCajas_ProgramaSubPrograma = true;
                    bForzarMultiplosDeCaja_ValidacionLocal = bCapturaEnMultiplosDeCajas_ClaveSSA_Especifica;
                }
            }

            bForzarMultiplosDeCaja_ValidacionLocal = bParametro_ForzarCapturaHabilitarValidaciones ? bForzarMultiplosDeCaja_ValidacionLocal : false;

            return validarCaptura_De_Cantidades_EnMultiplos_Base(Renglon, Cantidad, bForzarMultiplosDeCaja_ValidacionLocal); 
        }

        private bool validarCaptura_De_Cantidades_EnMultiplos_Base(int Renglon, int Cantidad, bool ValidarCapturaEnMultiplosDeCajas)
        {
            bool bRegresa = false;

            int iCantidad = Cantidad; // myGrid.TotalizarColumna((int)Cols.Cantidad);           
            int iPaquete = Convert.ToInt32("0" + lblContenido.Text);
            bool bResiduoValido = true;  // myGrid.GetValueInt(iRow, (int)Cols.Residuo) == 0; 
            bool bResiduoValido_Auxiliar = true;  
            int iResiduo = 0;
            int iResiduo_Auxiliar = 0;
            bool bSalidaValida = true;
            string sMensaje = "";

            if (ValidarCapturaEnMultiplosDeCajas)
            {
                iResiduo = iCantidad % iPaquete;
                bResiduoValido = iResiduo > 0 ? false : true; 
            }

            if (!bResiduoValido)
            {
                bSalidaValida = false;
                if (Renglon > 0)
                {
                    myGrid.SetActiveCell(Renglon, (int)Cols.Cantidad);
                    General.msjUser(string.Format("La cantidad capturada no es multiplo de {0}", iPaquete.ToString("###,###,###,###")));
                    myGrid.SetValue(Renglon, (int)Cols.Cantidad, 0);
                }
            }
            else 
            {
                if (bParametro_ForzarCapturaHabilitarValidaciones)
                {
                    if (iCantidad > 0 && iPaquete > 1)
                    {
                        if (GnFarmacia.INT_OPM_ProcesoActivo && ValidarCapturaEnMultiplosDeCajas)
                        {
                            sMensaje = string.Format("La cantidad capturada son {0} 'Paquetes - Bolsas - Envases ( con {1} )' ó son {0} Piezas ",
                                iCantidad, iPaquete);
                            FrmMensajeCaptura f = new FrmMensajeCaptura(iPaquete, iContenidoPaqueteComercial, iCantidad, sMensaje);
                            bSalidaValida = f.SolicitarConfirmacionDeCantidadCapturada();
                        }
                    }

                    if (tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion && DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Ninguno)
                    {
                        iResiduo_Auxiliar = iCantidad % iContenidoPaqueteComercial;
                        bResiduoValido_Auxiliar = iResiduo_Auxiliar > 0 ? false : true;

                        if (bSalidaValida && iCantidad > 0 && (iPaquete != iContenidoPaqueteComercial) & !bResiduoValido_Auxiliar)
                        {
                            sMensaje = string.Format("Si esta entregando << {0} >>   'Paquetes - Bolsas - Envases' cerrado(s) se debe capturar en multiplos de  << {1} >>    lo cual representa  << {2} >> pieza(s)",
                                iCantidad, iContenidoPaqueteComercial, (iCantidad * iContenidoPaqueteComercial));

                            FrmMensajeCaptura f = new FrmMensajeCaptura(iPaquete, iContenidoPaqueteComercial, iCantidad, sMensaje);
                            bSalidaValida = f.SolicitarConfirmacionDeCantidadCapturada();

                            if (!bSalidaValida)
                            {
                                myGrid.SetActiveCell(Renglon, (int)Cols.Cantidad);
                                myGrid.SetValue(Renglon, (int)Cols.Cantidad, 0);
                            }
                        }
                    }
                }
            }

            return bSalidaValida;  
        }

        private void Actualizar_Datos_De_Salida()
        {
            dtsLotes = clsLotes.PreparaDtsLotes();

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                object[] objRow = 
                {
                    myGrid.GetValue(i, (int)Cols.IdSubFarmacia), 
                    myGrid.GetValue(i, (int)Cols.SubFarmacia), 
                    myGrid.GetValue(i, (int)Cols.Codigo), 
                    myGrid.GetValue(i, (int)Cols.CodigoEAN), 
                    myGrid.GetValue(i, (int)Cols.SKU), 
                    myGrid.GetValue(i, (int)Cols.ClaveLote), 
                    myGrid.GetValueInt(i, (int)Cols.MesesPorCaducar), 
                    myGrid.GetValueFecha(i, (int)Cols.FechaEnt), 
                    myGrid.GetValueFecha(i, (int)Cols.FechaCad), 
                    myGrid.GetValue(i, (int)Cols.Status), 
                    myGrid.GetValueInt(i, (int)Cols.Existencia), 
                    myGrid.GetValueInt(i, (int)Cols.Existencia_Disponible), 
                    myGrid.GetValueInt(i, (int)Cols.Cantidad)
                    //myGrid.GetValue(i, (int)Cols.FLO)
                };
                dtsLotes.Tables[0].Rows.Add(objRow);
            }
            iTotalCantidad = myGrid.TotalizarColumna((int)Cols.Cantidad);
            iTipoCaptura = Convert.ToInt32(cboTipoCaptura.Data);
        }

        private void cboTipoCaptura_Validating(object sender, CancelEventArgs e)
        {
            if (cboTipoCaptura.Data == "0")
            {
                grdLotes.Enabled = false;
            }
            else
            {
                grdLotes.Enabled = true;
                cboTipoCaptura.Enabled = false;
            }
        }

        private void AjustarMensajeTeclasRapidas()
        {
            //switch (tpOrigenManLotes)
            //{
            //    case OrigenManejoLotes.Ventas_Dispensacion:
            //    case OrigenManejoLotes.Ventas_PublicoGeneral:
            //        break;
            //    default:
            //        lblAyuda.Visible = true;
            //        break;
            //}

            // Ajustar el tamaño de la pantalla 
            if (!bPermitirCapturaLotesNuevos)
            {
                //this.Height = FrameLotes.Height + (50);
                lblAyuda.Visible = false;
                lblAyudaAux.Visible = true;
            }
            else
            {
                lblAyuda.Visible = true;
                lblAyudaAux.Visible = false; 
            }
        }

        private void MarcarLotes()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                MarcarLotes(i); 
            }
        }

        private void MarcarLotes(int Renglon)
        {
            bool bEsConsignacion = myGrid.GetValue(Renglon, (int)Cols.ClaveLote).Contains("*");

            if (bEsConsignacion)
            {
                myGrid.ColorCelda(Renglon, (int)Cols.ClaveLote, colorConsignacion); 
            }
        } 

        private void Validar_CuadroBasico(int Renglon)
        {
            bool bEsVenta = !myGrid.GetValue(Renglon, (int)Cols.ClaveLote).Contains("*");

            if (bEsVenta)
            {
                if (!bVenta_ExisteEnCuadroBasico)
                {
                    myGrid.ColorCelda(Renglon, (int)Cols.ClaveLote, colorBloqueaVenta_NoExisteCuadroBasico); 
                    myGrid.ColorRenglon(Renglon, colorBloqueaVenta_NoExisteCuadroBasico);
                    myGrid.BloqueaCelda(true, colorBloqueaVenta_NoExisteCuadroBasico, Renglon, (int)Cols.Cantidad);
                    myGrid.SetValue(Renglon, (int)Cols.EsVentaBloqueada, true); 

                }
            }
        }

        private bool DispensacionVenta_LoteExcluido(int Renglon)
        {
            bool bRegresa = false;
            string sSql = "";
            string sIdSubFarmcia = myGrid.GetValue(Renglon, (int)Cols.IdSubFarmacia) ;
            string sClaveLote = myGrid.GetValue(Renglon, (int)Cols.ClaveLote); 

            if (DtGeneral.EsAlmacen)
            {
                sSql = string.Format("Select * " +
                    "From CFG_ProductosLotes_Excluir_Dispensacion (NoLock) " +
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                    "And IdSubFarmacia = '{3}' and IdProducto = '{4}' and CodigoEAN = '{5}' and ClaveLote = '{6}' " + 
                    "And Status = 'A'  ",
                    sIdEmpresa, sIdEstado, sIdFarmacia, sIdSubFarmcia, sIdArticulo, sCodigoEAN, sClaveLote); 

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "DispensacionVenta_LoteExcluido"); 
                }
                else
                {
                    bRegresa = leer.Leer(); 
                }
            }

            return bRegresa; 
        }

        private void ValidarDispensacion_Venta_Consignacion(int Renglon)
        {
            if (!bPermitirDispensacionVenta_SiExisteConsignacion) 
            {
                bool bEsConsignacion = myGrid.GetValue(Renglon, (int)Cols.ClaveLote).Contains("*");
                string sIdSubFarmacia = myGrid.GetValue(Renglon, (int)Cols.IdSubFarmacia); 
                // int iExistencia_Lote = myGrid.GetValueInt(Renglon, (int)Cols.Existencia);
                // iExistenciaDeConsignacion = iExistenciaDeConsignacion; 

                if (!bEsConsignacion || SubFarmacias.EmulaVenta(sIdSubFarmacia))
                {
                    if (iExistenciaDeConsignacion > 0) 
                    {
                        bMostrar_Msj_DispensarVenta_SiExisteConsignacion = true;
                        myGrid.ColorRenglon(Renglon, colorBloqueaVenta_ExisteConsignacion);
                        myGrid.BloqueaCelda(true, colorBloqueaVenta_ExisteConsignacion, Renglon, (int)Cols.Cantidad);
                        myGrid.SetValue(Renglon, (int)Cols.EsVentaBloqueada, true);
                    }
                }
            }
        }

        private void ValidarDispensacionActiva_BloquearVenta( int Renglon )
        {
            if( tpOrigenManLotes == OrigenManejoLotes.Ventas_Dispensacion && ( !bDispensacionActiva_Venta || !bDispensacionActiva_Consigna) )
            {
                bool bEsVenta = !(myGrid.GetValue(Renglon, (int)Cols.ClaveLote).Contains("*"));
                bool bEsConsigna = (myGrid.GetValue(Renglon, (int)Cols.ClaveLote).Contains("*"));
                string sIdSubFarmacia = myGrid.GetValue(Renglon, (int)Cols.IdSubFarmacia);

                if(!bDispensacionActiva_Venta && ( bEsVenta || SubFarmacias.EmulaVenta(sIdSubFarmacia)) )
                {
                    myGrid.BloqueaCelda(true, Renglon, (int)Cols.Cantidad);
                    myGrid.SetValue(Renglon, (int)Cols.EsVentaBloqueada, true);
                }

                if(!bDispensacionActiva_Consigna && (bEsConsigna && !SubFarmacias.EmulaVenta(sIdSubFarmacia)))
                {
                    myGrid.BloqueaCelda(true, Renglon, (int)Cols.Cantidad);
                    myGrid.SetValue(Renglon, (int)Cols.EsVentaBloqueada, true);
                }
            }
        }

        /// <summary>
        /// Determinar la existencia de Consignacion del Producto-CodigoEAN 
        /// </summary>
        private void ObtenerExistenciaDeConsignacion()
        {
            int iMeses = 0;
            bool bEsCaducado = false;
            bool bEsConsignacion = false;
            bool bExcluirDeValidacion = false;
            string sIdSubFarmacia = ""; 

            iExistenciaDeConsignacion = 0; 

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                bExcluirDeValidacion = DispensacionVenta_LoteExcluido(i);
                //bExcluirDeValidacion = false; 
                iMeses = myGrid.GetValueInt(i, (int)Cols.MesesPorCaducar);
                bEsCaducado = iMeses <= 0 ? true : false; 
                myGrid.SetValue(i, (int)Cols.EsCaducado, bEsCaducado);
                sIdSubFarmacia = myGrid.GetValue(i, (int)Cols.IdSubFarmacia);

                if (!bEsCaducado)
                {
                    bEsConsignacion = myGrid.GetValue(i, (int)Cols.ClaveLote).Contains("*");
                    if (bEsConsignacion)
                    {
                        if (!SubFarmacias.EmulaVenta(sIdSubFarmacia))
                        {
                            if (!bExcluirDeValidacion)
                            {
                                iExistenciaDeConsignacion += myGrid.GetValueInt(i, (int)Cols.Existencia);
                            }
                        }
                    }
                }
            } 
        }

        private void Mostrar_Msj_DispensarVenta_SiExisteConsignacion()
        {
            bool bMostrar = false; 

            if (!bPermitirDispensacionVenta_SiExisteConsignacion)
            {
                if (bMostrar_Msj_DispensarVenta_SiExisteConsignacion)
                {
                    bMostrar = true; 
                }
            }

            lblVenta_ExisteConsignacion.BackColor = colorBloqueaVenta_ExisteConsignacion;
            lblVenta_ExisteConsignacion.ForeColor = colorBloqueaVenta_ExisteConsignacion; 
            lblVenta_ExisteConsignacion.Text = lblVenta_ExisteConsignacion_Mensaje.Text; 
            lblVenta_ExisteConsignacion.Visible = bMostrar;

            lblVenta_ExisteConsignacion_Mensaje.Visible = bMostrar;    
        }

        private void Ordernar_RevisarFechaCaducidad()
        {
            ////Color colorCaducados = Color.Red;
            ////Color colorPrecaucion = Color.Yellow;
            ////Color colorStatusOk = Color.Green;
            ////Color colorBloqueaCaducados = Color.BurlyWood;
            ////Color colorSalidaCaducados = Color.LightGray; 


            DateTime dFechaCad = DateTime.Now;
            //FarPoint.Win.Spread.SortInfo []mySort = {new FarPoint.Win.Spread.SortInfo((int)Cols.FechaCad, false)};
            
            //// ordernar los Lotes por el mas Próximo a caducar 
            //grdLotes.Sheets[0].SortRange( 0, (int)Cols.FechaEnt - 1, myGrid.Rows, 2, true, mySort); 

            grdLotes.Sheets[0].SortRows((int)Cols.FechaCad - 1, true, false);
            grdLotes.Sheets[0].SortRows((int)Cols.Ordenamiento - 1, true, false);


/*
 * Ajuste del Semaforo de Caducados 
 * Original :
 * 0-3 Rojo
 * 4-6 Amarillo
 * 7-9 Verde 
 * El resto esta libre 
 * 
 * Cambio :
 * 0-4 Rojo
 * 5-12 Amarillo
 * 13-18 Verde 
 * El resto esta libre 
 * 
*/

            ObtenerExistenciaDeConsignacion();  

            int iMeses = 0;
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                iMeses = myGrid.GetValueInt(i, (int)Cols.MesesPorCaducar);
                MarcarLotes(i);   //// Determinar si el Lote es de Consignación 
                ValidarDispensacion_Venta_Consignacion(i);
                Validar_CuadroBasico(i);
                ValidarDispensacionActiva_BloquearVenta(i); 

                switch (iMeses)
                {
                    case 0: 
                        // myGrid.SetValue(i, (int)Cols.EsCaducado, true); 
                        myGrid.ColorCelda(i, (int)Cols.MesesPorCaducar, colorCaducados);
                        dFechaCad = myGrid.GetValueFecha(i, (int)Cols.FechaCad);

                        if (dtpFechaSistema > dFechaCad)
                        {
                            myGrid.ColorCelda(i, (int)Cols.Cantidad, colorSalidaCaducados);
                            if (!bPermitirSacarCaducados)
                            {
                                myGrid.BloqueaCelda(true, colorBloqueaCaducados, i, (int)Cols.Cantidad); 
                            }
                        }
                        break;

                    case 1: 
                    case 2: 
                    case 3: 
                    case 4: 
                        myGrid.ColorCelda(i, (int)Cols.MesesPorCaducar, colorCaducados);
                        break; 

                    case 5: 
                    case 6: 
                    case 7: 
                    case 8: 
                    case 9: 
                    case 10: 
                    case 11: 
                    case 12: 
                        myGrid.ColorCelda(i, (int)Cols.MesesPorCaducar, colorPrecaucion);
                        break;

                    case 13: 
                    case 14: 
                    case 15: 
                    case 16: 
                    case 17: 
                    case 18: 
                        myGrid.ColorCelda(i, (int)Cols.MesesPorCaducar, colorStatusOk);
                        break;

                    default: 
                        if (iMeses < 0 )
                        {
                            // myGrid.SetValue(i, (int)Cols.EsCaducado, true); 
                            myGrid.ColorCelda(i, (int)Cols.MesesPorCaducar, colorCaducados);
                            myGrid.ColorCelda(i, (int)Cols.Cantidad, colorSalidaCaducados);

                            if (!bPermitirSacarCaducados) 
                            {
                                myGrid.BloqueaCelda(true, colorBloqueaCaducados, i, (int)Cols.Cantidad);
                            }
                        }
                        break;
                }
            }
        }

        #region Modificar caducidades 
        private void ModificarCaducidad()
        {
            int iRow = myGrid.ActiveRow; 
            bool bLoteModificable = myGrid.GetValueInt(iRow, (int)Cols.AddColumna) == 0 ? true : false;
            DateTime dptCaducidadNueva = new DateTime();
            string sFecha = ""; 
            int dMonth = 0; 
            scDateTimePicker dtpDiff = new scDateTimePicker();


            string sIdSubFarmacia = myGrid.GetValue(iRow, (int)Cols.IdSubFarmacia);
            string sSubFarmacia = myGrid.GetValue(iRow, (int)Cols.SubFarmacia);
            string sClaveLote = myGrid.GetValue(iRow, (int)Cols.ClaveLote);
            string sCaducidad = myGrid.GetValue(iRow, (int)Cols.FechaCad); 

            ////// Para evitar error al Manejar el Mes de Febrero 
            ////dtFecha = new DateTime(dtpFechaCaducidad.Value.Year, dtpFechaCaducidad.Value.Month, 1);
            ////sFecha = General.FechaYMD(dtFecha).ToString();

            ////myGrid.SetValue(iActiveRow, (int)Cols.FechaEnt, General.FechaYMD(dtpFechaEntrada.Value));
            ////myGrid.SetValue(iActiveRow, (int)Cols.FechaCad, General.FechaYMD(dtFecha));

            if (bLoteModificable)
            {
                FrmModificarCaducidades f = new FrmModificarCaducidades(sIdSubFarmacia, sSubFarmacia, sIdArticulo, sCodigoEAN, sClaveLote, sCaducidad);
                f.ShowDialog(); 

                if ( f.CaducidadModificada )
                {
                    dptCaducidadNueva = f.CaducidadNueva; 
                    dMonth = (int)dtpDiff.DateDiff(DateInterval.Month, dtpFechaEntrada.Value, dptCaducidadNueva);

                    myGrid.SetValue(iRow, (int)Cols.FechaCad, General.FechaYMD(dptCaducidadNueva));
                    myGrid.SetValue(iRow, (int)Cols.MesesPorCaducar, dMonth); 
                    myGrid.BloqueaCelda(false, colorDefault, iRow, (int)Cols.Cantidad);
                    myGrid.ColorCelda(iRow, (int)Cols.MesesPorCaducar, colorDefault); 

                    // Asegurar que se reordenen los registros 
                    Ordernar_RevisarFechaCaducidad(); 
                }
            }
        }
        #endregion Modificar caducidades

        #region Eventos_Click_Cerrar
        private void lblAyuda_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblAyudaAux_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion Eventos_Click_Cerrar 
    }
}