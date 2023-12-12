using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Inventario; 
using DllFarmaciaSoft.Reporteador;

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmReporteadorCEDIS_VentasDetalladas : FrmBaseExt 
    {
        enum Cols 
        {
            Fecha = 1, Folio = 2, 
            IdCliente = 3, IdSubCliente = 4, 
            IdPrograma = 5, IdSubPrograma = 6, 
            Programa = 7, Procesar = 8, Procesado = 9   
        }


        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal;
        clsLeer leerCuadrosDeAtencion;
        clsLeer leer_ActualizarPrecios; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;        

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerSubFarmacias;
        clsLeer leerProgramas_Catalogo;
        clsLeer leerSubProgramas_Catalogo;
        clsLeer leerProgramas;
        clsLeer leerSubProgramas;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet(); 

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sImpresoraSeleccionada = ""; 
        bool bGenerarArchivos = false; 
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        ///PrintDialog printer; 
        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = ""; 
        bool bFolderDestino = false;

        string sImpresion_Precios = "IMPRESION_VENTA_DETALLADA_CON_PRECIOS";
        bool bImpresion_Precios = false;

        DllFarmaciaSoft.Ventas.clsImprimirVentas imprimir;

        public FrmReporteadorCEDIS_VentasDetalladas()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerSubFarmacias = new clsLeer(ref cnn);
            leerProgramas_Catalogo = new clsLeer(ref cnn);
            leerSubProgramas_Catalogo = new clsLeer(ref cnn);
            leerProgramas = new clsLeer(ref cnn); 
            leerSubProgramas = new clsLeer(ref cnn);
            leerCuadrosDeAtencion = new clsLeer(ref cnn);
            leer_ActualizarPrecios = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            imprimir = new DllFarmaciaSoft.Ventas.clsImprimirVentas(General.DatosConexion, DatosCliente, DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                                        General.Url, GnFarmacia.RutaReportes, TipoReporteVenta.Credito);

            SolicitarPermisosUsuario(); 

            //this.Width = 710;
            //this.Height = 570;

            FrameProceso.Left = 178; 
            FrameProceso.Top = 154;
            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
            {
                this.Text = "Reeimpresión Detallada de Ventas CEDIS ";
            }
            else
            {
                this.Text = "Reeimpresión de Ventas"; 
                FrameMostrarPrecios.Visible = false;
                FrameDirectorio.Width = FrameCliente.Width;
            }

            grid = new clsGrid(ref grdUnidades, this); 
            CargarListaReportes();
            CargarPerfilesDeAtencion();
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            ////// Valida si el usuario conectado tiene permiso sobre las opcione especiales 
            ////Permisos = new clsOperacionesSupervizadas(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            ////Permisos.Personal = DtGeneral.IdPersonal;
            ////bPermisoClaveSSA = Permisos.TienePermiso(sPermisoClaveSSA);
            ////bPermisoContenidoPaquete = Permisos.TienePermiso(sPermisoContenidoPaquete);

            bImpresion_Precios = DtGeneral.PermisosEspeciales.TienePermiso(sImpresion_Precios);

        }
        #endregion Permisos de Usuario

        private void CargarListaReportes()
        {
            ////////cboReporte.Clear();
            ////////cboReporte.Add(); // Agrega Item Default 

            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumos", "Concentrado de Dispensación");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosDesglozado", "Concentrado de Dispensación desglozado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosPrograma", "Concentrado de Dispensación Por Programa");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosProgramaTotalizado", "Concentrado de Dispensación Por Programa Totalizado");  

            ////////cboReporte.Add("PtoVta_Admon_Validacion", "Detallado de Dispensación (Validación)", "");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación no licitado)", "1");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación licitado)", "2");


            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecio", "Claves SSA sin precio asignado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecioDetallado", "Claves SSA sin precio asignado detallado"); 
            ////////cboReporte.SelectedIndex = 0; 
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 178;
            }
            else
            {
                FrameProceso.Left = this.Width + 100; 
            } 
        }

        private void FrmReporteadorCEDIS_VentasDetalladas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Cargar Combos  
        private void thCargar_Folios_De_Venta()
        {
            _workerThread = new Thread(this.Cargar_Folios_De_Venta);
            _workerThread.Name = "Obteniendo_Folios_De_Venta";
            _workerThread.Start();
        }

        private void Cargar_Folios_De_Venta()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFiltroDispensacion = "";
            string sFiltroFolios = ""; 
            string sFiltroFecha = string.Format(" and convert(varchar(10), FechaRegistro, 120) between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));


            if (txtCte.Text != "")
            {
                sFiltroDispensacion = string.Format(" And V.IdCliente = '{0}' ", txtCte.Text);
                if (txtSubCte.Text != "")
                {
                    sFiltroDispensacion += string.Format(" And V.IdSubCliente = '{0}' ", txtSubCte.Text);
                    if (txtPro.Text != "")
                    {
                        sFiltroDispensacion += string.Format(" And V.IdPrograma = '{0}' ", txtPro.Text);
                        if (txtSubPro.Text != "")
                        {
                            sFiltroDispensacion += string.Format(" And V.IdSubPrograma = '{0}' ", txtSubPro.Text);
                        }
                    }
                }
            }

            if (!chkFechas.Checked)
            {
                sFiltroFecha = " "; 
            }

            if ( chkFolios.Checked ) 
            {
                if (txtFolioInicial.Text.Trim() != "" && txtFolioFinal.Text.Trim() != "")
                {
                    sFiltroFolios = string.Format(" and FolioVenta Between '{0}' and '{1}' ", 
                        Fg.PonCeros(txtFolioInicial.Text.Trim(), 8), 
                        Fg.PonCeros(txtFolioFinal.Text.Trim(), 8) 
                        ); 
                }
                else
                {
                    if (txtFolioInicial.Text.Trim() != "" )
                    {
                        sFiltroFolios = string.Format(" and FolioVenta >= '{0}' ",
                            Fg.PonCeros(txtFolioInicial.Text.Trim(), 8));
                    }

                    if (txtFolioFinal.Text.Trim() != "")
                    {
                        sFiltroFolios = string.Format(" and FolioVenta <= '{0}' ",
                            Fg.PonCeros(txtFolioFinal.Text.Trim(), 8));
                    } 
                }
            }


            sSql = string.Format("Select convert(varchar(10), FechaRegistro, 120) as FechaRegistro, FolioVenta as Folio, " + 
                " V.IdCliente, V.IdSubCliente, " + 
                " V.IdPrograma, V.IdSubPrograma, (Programa + ' - ' + SubPrograma ) as ProgramaDispensacion, 0 as Procesar, 0 as Procesado " + 
                "From VentasEnc V (NoLock) " + 
                "Inner Join vw_Programas_SubProgramas C (NoLock) On ( V.IdPrograma = C.IdPrograma and V.IdSubPrograma = C.IdSubPrograma ) " +
                "Where V.IdEmpresa = '{0}' and V.IdEstado = '{1}' and V.IdFarmacia = '{2}' {3} {4} {5}  " + 
                "Order By FechaRegistro ",
                sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDispensacion, sFiltroFolios);

            grid.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Folios_De_Venta()");
                General.msjError("Ocurrió un error al obtener la lista de folios.");
            }
            else
            {
                bRegresa = leer.Leer(); 
                grid.LlenarGrid(leer.DataSetClase); 
            }

            IniciarToolBar(true, !bRegresa, bRegresa); 
        }

        private void CargarPerfilesDeAtencion()
        {
        } 
        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Buscar Informacion Dispensacion  
        #region Cliente -- Sub-Cliente 
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCliente_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece al Almacén.");
                    txtCte.Text = ""; 
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
            } 
        }

        private void CargarDatosCliente()
        {
            txtCte.Enabled = false; 
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    txtSubCte.Text = ""; 
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Enabled = false; 
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente 

        #region Programa -- Sub-Programa
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPro.Text.Trim() != "")
            {
                if ( txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" ) 
                {
                    //leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
                    leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosProgramas();
                    }
                    else
                    {
                        txtPro.Text = ""; 
                        lblPro.Text = "";
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
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosProgramas();
                    }
                }
            }
        }

        private void CargarDatosProgramas()
        {
            txtPro.Enabled = false; 
            txtPro.Text = leer.Campo("IdPrograma");
            lblPro.Text = leer.Campo("Programa");
        }

        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPro.Text.Trim() != "")
            {
                if (txtCte.Text.Trim() != "" && txtSubCte.Text.Trim() != "" && txtPro.Text.Trim() != "")
                {
                    leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(), "txtSubPro_Validating");
                    if (leer.Leer())
                    {
                        CargarDatosSubProgramas();
                    }
                    else
                    {
                        txtSubPro.Text = ""; 
                        lblSubPro.Text = "";
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
                    leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_KeyDown");
                    if (leer.Leer())
                    {
                        CargarDatosSubProgramas();
                    }
                }
            }
        }

        private void CargarDatosSubProgramas()
        {
            txtSubPro.Enabled = false;
            txtSubPro.Text = leer.Campo("IdSubPrograma");
            lblSubPro.Text = leer.Campo("SubPrograma");
        }
        #endregion Programa -- Sub-Programa
        #endregion Buscar Informacion Dispensacion

        #region Impresion
        private void ObtenerRutaReportes()
        {
            //string sSql = string.Format(" Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
            //    " From Net_CFGC_Parametros (NoLock) " +
            //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' ", 
            //    cboEstados.Data, cboFarmacias.Data, "RutaReportes");
            //if (!leerWeb.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "ObtenerRutaReportes");
            //    General.msjError("Ocurrió un error al obtener la Ruta de Reportes de la Farmacia."); 
            //}
            //else
            //{
            //    leerWeb.Leer();
            //    sUrl_RutaReportes = leerWeb.Campo("Valor");     
            //}
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            ////////if (bRegresa && cboReporte.SelectedIndex == 0)
            ////////{
            ////////    bRegresa = false;
            ////////    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            ////////    cboReporte.Focus(); 
            ////////}

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central. 
                // Se utilizan los datos de Conexión de la farmacia seleccionada. 

                DatosCliente.Funcion = "Imprimir()"; 
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                ////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = ""; // cboReporte.Data + "";

                string sValor = "";
                ////try
                ////{
                ////    sValor = (string)cboReporte.ItemActual.Item;
                ////}
                ////catch { }

                if (sValor == "1" || sValor == "2")
                {
                    myRpt.Add("IdGrupoPrecios", Convert.ToInt32(sValor)); 
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

                //////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    conexionWeb.Url = General.Url;
                ////    conexionWeb.Timeout = 300000;
                ////    //////myRpt.CargarReporte(true); 

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            IniciarToolBar(true, true, false); 

            sRutaDestino = "";
            bFolderDestino = false; 

            btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            grid.Limpiar(); 
            Fg.IniciaControles(this, true);
            chkFechas.Checked = true;
            chkMostrarPrecios.Enabled = bImpresion_Precios;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = ""; 

            FrameCliente.Enabled = bValor;
            FrameFechas.Enabled = bValor;

            chkDesglosado.Enabled = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;

            ////txtPro.Enabled = false;
            ////txtSubPro.Enabled = false; 

            if (!DtGeneral.EsAdministrador)
            {
                //////cboEmpresas.Data = DtGeneral.EmpresaConectada;
                //////cboEstados.Data = DtGeneral.EstadoConectado;

                //////cboEmpresas.Enabled = false;
                //////cboEstados.Enabled = false; 
            } 

            txtCte.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thCargar_Folios_De_Venta();
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento(false))
            {
                sImpresoraSeleccionada = ""; 
                bGenerarArchivos = true; 
                CrearDirectorioDestino(); 
                IniciarProcesamiento(); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Imprimir(); 
            if (validarProcesamiento(true))
            {               
                bGenerarArchivos = false;
                IniciarProcesamiento();
            }
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;  

            if (folder.ShowDialog() == DialogResult.OK) 
            {
                sRutaDestino = folder.SelectedPath +  @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true; 
            } 

        } 
        #endregion Botones

        #region Eventos
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = ""; 
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            //grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
            grid.Limpiar();
        } 

        #endregion Eventos

        #region Procesar Informacion  
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar; 
            btnImprimir.Enabled = Generar; 
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            //////try
            //////{
            //////    leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            //////    conexionWeb.Url = sUrl;
            //////    DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            //////    DatosDeConexion.Servidor = sHost;
            //////    bRegresa = true; 
            //////}
            //////catch (Exception ex1)
            //////{
            //////    Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
            //////    General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
            //////    ActivarControles(); 
            //////}

            return bRegresa; 
        }

        private bool validarProcesamiento(bool SolicitarImpresora)
        {
            bool bRegresa = true;

            if (!SolicitarImpresora)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
                }
            }

            if (bRegresa && grid.Rows == 0)
            {
                bRegresa = false;
                General.msjUser("No existe información disponibe para procesar, verifique.");
            }

            if (bRegresa)
            {
                if (!Obtener_Cuadros_De_Atencion())
                {
                    bRegresa = false;
                    General.msjError("No se encontraron cuadros de atención para este Almacén, verifique."); 
                }
            }

            if (bRegresa)
            {
                if (SolicitarImpresora)
                {
                    ////printer = new PrintDialog(); 
                    ////printer.PrinterSettings.PrinterName = sImpresoraSeleccionada;

                    printer.UseEXDialog = true; 

                    ////printer.ShowDialog(); 
                    if (printer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        sImpresoraSeleccionada = printer.PrinterSettings.PrinterName;
                    }
                }
            }

            return bRegresa; 
        }

        private void CrearDirectorioDestino()
        {
            string sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema); 
            string sDir = string.Format("FI_{0}___FF_{1}",
                General.FechaYMD(dtpFechaInicial.Value, ""), General.FechaYMD(dtpFechaFinal.Value, ""));

            if (!chkFechas.Checked)
            {
                sDir = string.Format("FI_{0}___FF_{1}",
                    General.FechaYMD(General.FechaSistema, ""), General.FechaYMD(General.FechaSistema, ""));
            }


            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "____" + sMarcaTiempo; 
            if (!Directory.Exists(sRutaDestino_Archivos)) 
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private bool Obtener_Cuadros_De_Atencion()
        {
            bool bRegresa = false;

            string sSql = string.Format(
                "Select 0 as IdPerfilAtencion, 'Sin Especificar' as Titulo " + 
                "   UNION " + 
                "Select IdPerfilAtencion, Descripcion as Titulo " +
                "From CFGC_ALMN_CB_NivelesAtencion (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' ",
                sEmpresa, sEstado, sFarmacia );

            bRegresa = leerCuadrosDeAtencion.Exec(sSql); 

            return bRegresa;
        }

        private bool Obtener_Cuadros_De_Atencion(string FolioVenta)
        {
            bool bRegresa = false;

            ////string sSql = string.Format(
            ////    "Select 0 as IdPerfilAtencion, 'Sin Especificar' as Titulo " +
            ////    "   UNION " +
            ////    "Select IdPerfilAtencion, Descripcion as Titulo " +
            ////    "From CFGC_ALMN_CB_NivelesAtencion (NoLock) " +
            ////    "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' " + 
            ////    "   And IdCliente = '{3}' and IdSubCliente = '{4}' and IdPrograma = '{5}' and IdSubPrograma = '{6}' ",
            ////    sEmpresa, sEstado, sFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma );

            string sSql = string.Format("Exec spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA '{0}', '{1}', '{2}', '{3}' ",
                sEmpresa, sEstado, sFarmacia, FolioVenta); 

            bRegresa = leerCuadrosDeAtencion.Exec(sSql);

            return bRegresa;
        }

        private void ObtenerProgramas_Farmacia(string sIdFarmacia)
        {
            DataTable dtTabla;
            string sFiltro = string.Format("IdFarmacia = '{0}'", sIdFarmacia);
            dtsProgramas = new DataSet();
            dtTabla = new DataTable();

            leerProgramas = new clsLeer();
            try
            {
                dtTabla = leerProgramas_Catalogo.DataTableClase.Clone();

                foreach (DataRow dr in leerProgramas_Catalogo.DataTableClase.Select(sFiltro))
                {
                    dtTabla.ImportRow(dr);
                }
                dtsProgramas.Tables.Add(dtTabla);
                leerProgramas.DataSetClase = dtsProgramas;

            }
            catch { }

        } 

        private void ObtenerSubProgramas_Farmacia( string sIdFarmacia, string sIdPrograma )
        {
            DataTable dtTabla;
            string sFiltro = string.Format("IdFarmacia = '{0}' And IdPrograma = '{1}'", sIdFarmacia, sIdPrograma);
            dtsSubProgramas = new DataSet();
            dtTabla = new DataTable();

            leerSubProgramas = new clsLeer();
            try
            {
                dtTabla = leerSubProgramas_Catalogo.DataTableClase.Clone();

                foreach (DataRow dr in leerSubProgramas_Catalogo.DataTableClase.Select(sFiltro))
                {
                    dtTabla.ImportRow(dr);
                }
                dtsSubProgramas.Tables.Add(dtTabla);
                leerSubProgramas.DataSetClase = dtsSubProgramas;
                
            }
            catch { }

        }

        private void BloquearControles(bool Habilitar)
        {
            txtCte.Enabled = Habilitar;
            txtSubCte.Enabled = Habilitar;
            txtPro.Enabled = Habilitar;
            txtSubPro.Enabled = Habilitar;
            
            dtpFechaInicial.Enabled = Habilitar;
            dtpFechaFinal.Enabled = Habilitar;
            chkFechas.Enabled = Habilitar;

            txtFolioInicial.Enabled = Habilitar;
            txtFolioFinal.Enabled = Habilitar;
            chkFolios.Enabled = Habilitar;

            btnDirectorio.Enabled = Habilitar;

            chkMostrarPrecios.Enabled = Habilitar;
            chkActualizarPrecios.Enabled = Habilitar; 
            chkMarcarDesmarcar.Enabled = Habilitar; 
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;

            BloquearControles(false); 

            // bloqueo principal 
            IniciarToolBar(false, false, false); 
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            grid.SetValue((int)Cols.Procesado, 0); 

            MostrarEnProceso(true);

            //btnDirectorio.Enabled = false; 
            ////FrameFechas.Enabled = false;
            ////FrameFolios.Enabled = false;
            ////FrameMostrarPrecios.Enabled = false; 

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion"; 
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private void ObtenerInformacion()
        {
            int iPerfil = 0;
            string sTitulo = ""; 
            string sFolio = "";
            string sIdCliente = "";
            string sIdSubCliente = ""; 
            string sIdPrograma = "";
            string sIdSubPrograma = "";
            bool bTieneVenta = false;
            bool bTieneConsignacion = false;


            imprimir.RutaDestino_Archivos = sRutaDestino_Archivos;
            imprimir.GenerarArchivos = bGenerarArchivos;
            imprimir.MostrarImpresionDetalle = true;

            bEjecutando = true; 
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sFolio = grid.GetValue(i, (int)Cols.Folio);

                    if (chkActualizarPrecios.Checked)
                    {
                        ActualizarPrecios(sFolio);
                    }

                    Generar_PDF_Impresion(sFolio); 

                    grid.SetValue(i, (int)Cols.Procesado, true);
                }
            }
            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);
            MostrarEnProceso(false);
        }

        private void Generar_PDF_Impresion( string Folio )
        {
            string sFile = Folio; 
            sFile = string.Format("VENTA_{0}_{1}", DtGeneral.FarmaciaConectada, Folio); 
            sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");

            imprimir.Imprimir(Folio, "", 0.0000, chkDesglosado.Checked);


            //bool bRegresa = false;
            //clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            //clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            //Reporte.RutaReporte = DtGeneral.RutaReportes;

            //if (!bGenerarArchivos)
            //{
            //    Reporte.EnviarAImpresora = true;
            //    ////Reporte.Impresora = sImpresoraSeleccionada;
            //}

            //if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen)
            //{
            //    if (chkMostrarPrecios.Checked)
            //    {
            //        Reporte.NombreReporte = "PtoVta_TicketCredito_Detallado_Precios.rpt";
            //    }
            //    else
            //    {
            //        Reporte.NombreReporte = "PtoVta_TicketCredito_Detallado.rpt";
            //    }
            //}
            //else
            //{
            //    Reporte.NombreReporte = "PtoVta_TicketCredito.rpt";
            //}


            //Reporte.Add("IdEmpresa", sEmpresa);
            //Reporte.Add("IdEstado", sEstado);
            //Reporte.Add("IdFarmacia", sFarmacia);
            //Reporte.Add("Folio", Folio);


            //Reporteador = new clsReporteador(Reporte, DatosCliente);
            //Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            //Reporteador.Url = General.Url;
            //Reporteador.MostrarInterface = false;
            //Reporteador.MostrarMensaje_ReporteSinDatos = false; 

            //if (bGenerarArchivos)
            //{
            //    bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
            //}
            //else
            //{
            //    bRegresa = Reporteador.GenerarReporte();

            //    ////if (bRegresa)
            //    ////{

            //    ////    Reporte.CargarReporte(); 
            //    ////}
            //}


        }

        private void ActualizarPrecios(string Folio)
        {
            string sSql = string.Format("Exec spp_Mtto_Ventas_AsignarPrecioLicitacion  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioVenta = '{3}' ",
                sEmpresa, sEstado, sFarmacia, Folio);

            leer_ActualizarPrecios.Exec(sSql); 
        }
        #endregion Procesar Informacion 

        #region Funciones y Procedimientos Privados 
        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            BloquearControles(true); 

            ////txtCte.Enabled = true;
            ////txtSubCte.Enabled = true;
            ////txtPro.Enabled = true;
            ////txtSubPro.Enabled = true;

            ////chkFolios.Enabled = true;
            ////chkMarcarDesmarcar.Enabled = true; 


            ////btnDirectorio.Enabled = true; 
            ////FrameFechas.Enabled = true;
            ////FrameFolios.Enabled = true;
            ////FrameMostrarPrecios.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando) 
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false; 

                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true;
                ActivarControles(); 
            }
        }


        private void FrmReporteadorCEDIS_VentasDetalladas_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ObtenerInformacionExcel()
        {
            string sSql = string.Format("Select *, Convert(varchar(16), GetDate(), 120) as FechaImpresion From tmpRptAdmonDispensacion (NoLock) ");

            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

                leerExportarExcel = new clsLeer(ref cnnUnidad);
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leerLocal.DatosConexion, leerExportarExcel.Error, "ObtenerInformacionExcel()");
                    General.msjError("Ocurrió un error al obtener la información del reporte.");
                }
                else
                {
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                }

                // bSeEjecuto = true;
                bEjecutando = false; // Cursor.Current  
            }
            this.Cursor = Cursors.Default;
        } 

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //int iRow = 2;
            //string sNombreFile = "PtoVta_Admon_Validacion" + ".xls";
            //string sPeriodo = "";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //xpExcel.AgregarMarcaDeTiempo = false;

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        //Se pone el encabezado
            //        leerExportarExcel.RegistroActual = 1;
            //        leerExportarExcel.Leer();
            //        xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
            //        iRow++;
            //        xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
            //        iRow++;

            //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //            General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
            //        xpExcel.Agregar(sPeriodo, iRow, 2);

            //        iRow = 6;
            //        xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

            //        // Se ponen los detalles
            //        leerExportarExcel.RegistroActual = 1;
            //        iRow = 9;
            //        while (leerExportarExcel.Leer())
            //        {
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
            //            xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
            //            xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
            //            xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
            //            xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
            //            xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
            //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
            //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
            //            xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
            //            xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

            //            iRow++;
            //        }

            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //    this.Cursor = Cursors.Default;
            //}
        } 
        #endregion Funciones y Procedimientos Privados

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
        }
    } 
}
