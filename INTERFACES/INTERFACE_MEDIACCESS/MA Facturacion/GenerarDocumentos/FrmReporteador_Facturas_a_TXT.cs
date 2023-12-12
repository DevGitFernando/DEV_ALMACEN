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

////using DllTransferenciaSoft.IntegrarInformacion; 

////using Dll_SII_INadro.GenerarArchivos;

namespace MA_Facturacion.GenerarDocumentos
{
    public partial class FrmReporteador_Facturas_a_TXT : FrmBaseExt 
    {
        ////enum Cols 
        ////{
        ////    IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5  
        ////}

        enum Cols
        {
            Ninguna = 0,
            IdFarmacia, Farmacia, Procesar, Procesado, FechaFactura, NumeroDeFactura, ImporteFactura, FolioFactura 
            ////IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5, Inicio = 6, Fin = 7, Procesando = 8
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerExcel;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;        

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        clsExportarExcelPlantilla xpExcel;
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
        //////wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet(); 

        string sIdPublicoGeneral =  "0001";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = ""; 
        bool bFolderDestino = false;

        string sProceso = "";
        int iRenlgonEnProceso = 0;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        DataSet dtsFarmacias, dtsEstado;
        DataSet dtsDatos = new DataSet();

        public FrmReporteador_Facturas_a_TXT()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            //////conexionWeb = new wsFarmacia.wsCnnCliente();
            //////conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmReporteadorRemisiones");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto; 
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

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            ////lblTitulo_FiltroSubFarmacia.BackColor = General.BackColorBarraMenu; 

            this.Width = 1024;
            //this.Height = 570;

            FrameProceso.Left = 220; 
            FrameProceso.Top = 84;
            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 

            grid = new clsGrid(ref grdUnidades, this);

            ObtenerEstados();
            ObtenerFarmacias();
            ObtenerTipoUnidades();
            ObtenerJurisdicciones();  
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 220;
            }
            else
            {
                FrameProceso.Left = this.Width + 100; 
            } 
        }

        private void FrmReporteadorValidaciones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Cargar Combos

        private void ObtenerEstados()
        {
            dtsEstado = new DataSet();
            dtsEstado = Consultas.EstadosConFarmacias("ObtenerEstados");
            cboEstado.Clear();
            cboEstado.Add("0", "<< Seleccione >>");
            cboEstado.Add(dtsEstado, true, "IdEstado", "Estado");
            cboEstado.SelectedIndex = 0;
        }

        private void ObtenerTipoUnidades()
        {
            cboTipoUnidades.Clear();
            cboTipoUnidades.Add("0", "<< Todas >>");
            cboTipoUnidades.Add(Consultas.TipoUnidades("", "ObtenerTipoUnidades"), true, "IdTipoUnidad", "Descripcion");
            cboTipoUnidades.SelectedIndex = 0;
        }

        private void ObtenerJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("0", "<< Seleccione >>");
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias("ObtenerFarmacias");
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("IdEstado = '{0}' ", cboEstado.Data);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (cboJurisdicciones.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            if (cboTipoUnidades.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdTipoUnidad = '{0}' ", cboTipoUnidades.Data);
            }

            try
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }


            cboFarmacias.SelectedIndex = 0;
        }


        private bool validarConsulta_Farmacias()
        {
            bool bRegresa = true;

            ////if (cboEmpresas.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado una empresa válida, verifique."); 
            ////    cboEmpresas.Focus(); 
            ////}

            ////if (bRegresa & cboEstados.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado un estado válido, verifique.");
            ////    cboEstados.Focus();
            ////}

            ////if (bRegresa & cboJurisdicciones.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
            ////    cboJurisdicciones.Focus();
            ////}

            return bRegresa; 
        }
        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

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

            iRenlgonEnProceso = 0; 
            sRutaDestino = "";
            bFolderDestino = false; 

            ////btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            grid.Limpiar(); 
            Fg.IniciaControles(this, true); 

            ////btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = "";

            rdoDoctos_TXT.Checked = true; 


            //////sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //////sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            //////lblDirectorioTrabajo.Text = sRutaDestino;
            //////bFolderDestino = true;

            General.FechaSistemaObtener(); 
            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_MEDIACCESS\{0}", General.FechaYMD(General.FechaSistema, ""));
            lblDirectorioTrabajo.Text = sRutaDestino;
            bFolderDestino = true; 

            if (!DtGeneral.EsAdministrador)
            {
            }

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarConsulta_Farmacias())
            {
                CargarFacturas_DelPeriodo();
            }
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                CrearDirectorioDestino(); 
                IniciarProcesamiento(); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Imprimir(); 
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.SelectedPath = lblDirectorioTrabajo.Text; 
            folder.ShowNewFolderButton = true;  

            if (folder.ShowDialog() == DialogResult.OK) 
            {
                sRutaDestino = folder.SelectedPath +  @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true; 
            } 

        } 
        #endregion Botones

        #region Procesar Informacion  
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar; 
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = false;
            
            bRegresa = validarConsulta_Farmacias();

            if (bRegresa)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique."); 
                }
            } 
            
            return bRegresa; 
        }

        private void CrearDirectorioDestino()
        {
            string sDir = "000__GENERAL"; //// cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            string sMarcaTiempo = "";

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                sDir = cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            }

            sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema); 
            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "____" + sMarcaTiempo;

            if (!Directory.Exists(sRutaDestino_Archivos)) 
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private void CargarFacturas_DelPeriodo()
        {
            string sFiltroEstado = string.Format(" And Idestado_Factura = '{0}'", cboEstado.Data);
            string sFiltroFarmacia = string.Format(" and IdFarmacia_Facturada = '{0}'", cboFarmacias.Data); 
            string sFiltroFecha = string.Format(" and convert(varchar(10), FechaFactura, 120) between '{0}' and '{1}'  ",
                General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            if (cboFarmacias.SelectedIndex == 0)
            {
                sFiltroFarmacia = " ";
            }

            if (cboEstado.SelectedIndex == 0)
            {
                sFiltroEstado = " ";
            }

            string sSql = string.Format(
                "Select IdFarmacia_Facturada, Farmacia_Facturada, 0 as Procesar, 0 as Procesado, " +
                "convert(varchar(10), FechaFactura, 120) as FechaFactura, NumeroDeFactura, Importe, FolioFactura " +  
                "From vw_FACT_Facturas_Informacion (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmaciaGenera = '{2}' {3}  {4} {5}",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFiltroEstado, sFiltroFarmacia, sFiltroFecha);

            grid.Limpiar(true); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la lista de facturas.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciarToolBar(true, true, false);
                    General.msjUser("No se encontro información con los criterios especificados.");
                }
                else
                {
                    IniciarToolBar(true, true, true); 
                    grid.LlenarGrid(leer.DataSetClase, false, false); 
                }
            }
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            ////btnImprimir.Enabled = false;
            ////btnExportarExcel.Enabled = false;

            cboEstado.Enabled = false;
            cboTipoUnidades.Enabled = false;
            cboJurisdicciones.Enabled = false;
            cboFarmacias.Enabled = false; 

            // bloqueo principal 
            IniciarToolBar(false, false, false); 
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            ////grid.SetValue((int)Cols.Inicio, "");
            ////grid.SetValue((int)Cols.Fin, "");
            ////grid.SetValue((int)Cols.Procesando, ""); 

            MostrarEnProceso(true);

            btnDirectorio.Enabled = false; 

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.SetValue((int)Cols.Procesado, 0); 

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion"; 
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private string ObtenerMarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa = string.Format("{0}:{1}:{2}", Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2));

            return sRegresa;
        }

        private void ObtenerInformacion()
        {
            clsFacturas_GenerarDocumentos documentos = new clsFacturas_GenerarDocumentos();
            string sFolioFactura = ""; 

            bEjecutando = true;
            documentos.GenerarDirectorio_Farmacia = true;
            documentos.Generar_TXT = rdoDoctos_TXT.Checked;
            documentos.RutaDestinoReportes = sRutaDestino;

            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sFolioFactura = grid.GetValue(i, (int)Cols.FolioFactura); 

                    ////documentos.GenerarRemisiones(sIdFadocumentos.acia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                    documentos.GenerarDocumentos(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolioFactura); 
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
        #endregion Procesar Informacion 

        #region Funciones y Procedimientos Privados 
        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            ////btnExportarExcel.Enabled = false;
            btnDirectorio.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando) 
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false; 

                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                ////btnExportarExcel.Enabled = true;
                ActivarControles(); 

                ////////if (!bSeEncontroInformacion) 
                ////////{
                ////////    _workerThread.Interrupt(); 
                ////////    _workerThread = null; 

                ////////    ActivarControles();

                ////////   //////if (bSeEjecuto)
                ////////   ////// {
                ////////   //////     General.msjUser("No existe informacion para mostrar bajo los criterios seleccionados.");
                ////////   ////// } 
                ////////}
            }
        }

        private void cboTipoUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.Data != "0")
            {
                CargarFarmacias();
            }
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.Data != "0")
            {
                CargarFarmacias();
            }
        }

        private void FrmReporteadorValidaciones_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int iRow = 2;
            string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + cboJurisdicciones.Data + ".xls";
            string sPeriodo = "";

            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = false;

                if (xpExcel.PrepararPlantilla(sNombreFile))
                {
                    xpExcel.GeneraExcel();

                    //Se pone el encabezado
                    leerExportarExcel.RegistroActual = 1;
                    leerExportarExcel.Leer();
                    xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
                    iRow++;
                    xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
                    iRow++;

                    sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
                        General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
                    xpExcel.Agregar(sPeriodo, iRow, 2);

                    iRow = 6;
                    xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

                    // Se ponen los detalles
                    leerExportarExcel.RegistroActual = 1;
                    iRow = 9;
                    while (leerExportarExcel.Leer())
                    {
                        xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
                        xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
                        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
                        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
                        xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
                        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
                        xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
                        xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
                        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
                        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
                        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
                        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
                        xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
                        xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default; 
            }
        } 
        #endregion Funciones y Procedimientos Privados

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            ////FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            ////f.ShowDialog(this); 
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.Data != "0")
            {
                cboJurisdicciones.Add(Consultas.Jurisdicciones(cboEstado.Data, "ObtenerTipoUnidades"), true, "IdJurisdiccion", "NombreJurisdiccion");
                CargarFarmacias();
            }
        }
    } 
}
