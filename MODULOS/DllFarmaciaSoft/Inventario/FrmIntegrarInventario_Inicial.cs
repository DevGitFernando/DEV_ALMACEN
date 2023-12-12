using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.OfficeOpenXml.Data; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;


namespace DllFarmaciaSoft.Inventario
{
    public partial class FrmIntegrarInventario_Inicial : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce excel;

        clsDatosCliente DatosCliente;

        string sIdDistribuidor = "";
        string sFile_In = "";
        string sTitulo = "";

        int iRegistrosHoja = 0;
        int iRegistrosProcesados = 0;

        clsListView lst;

        OpenFileDialog openExcel = new OpenFileDialog();
        Thread thLoadFile;
        Thread thReadFile;
        Thread thGuardarInformacion;
        Thread thValidarInformacion;
        Thread thGeneraFolios;

        string sHoja_Inventario = "Inventario";
        public bool InformacionIntegrada = false;

        string sFormato = "###, ###, ###, ##0";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";
        string sMsjGuardar = "";
        string sMsjError = "";

        bool bGuardandoInformacion = false;
        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false;
        bool bErrorAlValidar = false;
        bool bPermitirAjustesInventario_Con_ExistenciaEnTransito = GnFarmacia.PermitirAjustesInventario_Con_ExistenciaEnTransito;

        TiposDeInventario tpInventario = TiposDeInventario.Todos;
        TiposDeCargaMasiva tpTipoCarga = TiposDeCargaMasiva.Ninguna;

        string sFolio_InventarioInicial = "";
        string sFolioVentaEntrada = "";
        string sFolioVentaSalida = "";
        string sFolioConsignacionEntrada = "";
        string sFolioConsignacionSalida = "";
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        string sMsjNoEncontrado = "";
        clsOperacionesSupervizadasHuellas opPermisosEspeciales;

        private bool bEsModuloValido = false;
        private bool bEsModulo_Almacen = false;

        //clsExportarExcelPlantilla xpExcel;

        public string IdProveedor = "";
        public string Referencia = "";
        public string Observaciones = "";
        public string FechaDocumento = "";
        public string FechaVenceDocumento = "";
        public string SubFarmacia = "";

        public FrmIntegrarInventario_Inicial(string FolioInventarioInicial, TiposDeInventario TipoInventario):
            this(FolioInventarioInicial, TiposDeCargaMasiva.InventarioInicial, TipoInventario)
        {
        }

        public FrmIntegrarInventario_Inicial(string FolioInventarioInicial, TiposDeCargaMasiva TipoDeCarga, TiposDeInventario TipoInventario)
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            sFolio_InventarioInicial = FolioInventarioInicial;
            tpInventario = TipoInventario;
            tpTipoCarga = TipoDeCarga; 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia ||
                DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis || DtGeneral.ModuloEnEjecucion == TipoModulo.FarmaciaUnidosis)
            {
                bEsModuloValido = true;
                bEsModulo_Almacen = (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.AlmacenUnidosis) ? true : false; 
            }

            switch (TipoDeCarga)
            {
                case TiposDeCargaMasiva.CompraDirecta:
                    this.Text = "Integrar plantilla de Compra directa";
                    sHoja_Inventario = "CompraDirecta";
                    break;

                case TiposDeCargaMasiva.EntradaDeConsignacion:
                    this.Text = "Integrar plantilla de Entrada por Consignación";
                    sHoja_Inventario = "EntradaPorConsignacion";
                    break;

                default:
                    if (tpInventario == TiposDeInventario.Consignacion)
                    {
                        this.Text += " Consigna";
                    }
                    break;
            }

            cnn = new clsConexionSQL(); 
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto; 
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 


            this.sIdDistribuidor = Fg.PonCeros("", 4); 
            leer = new clsLeer(ref cnn);
            excel = new SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 400;
            FrameResultado.Width = 800;

            FrameProceso.Top = 200;
            FrameProceso.Left = 76; 
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmIntegrarInventario_Inicial_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            tmPantalla.Enabled = true;
            tmPantalla.Start();
        }

        #region Propiedades 
        public string FolioGenerado
        {
            get { return Fg.PonCeros(Fg.Right(sFolio_InventarioInicial, 8), 8); }
        }
        #endregion Propiedades 

        #region Botones
        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            IniciaToolBar(Nuevo, false, Abrir, Ejecutar, Guardar, Validar, Procesar, Salir); 
        }

        private void IniciaToolBar(bool Nuevo, bool ExportarPlantila, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            ////btnExportarExcel.Enabled = ExportarPlantila;
            btnAbrir.Enabled = Abrir;
            btnEjecutar.Enabled = Ejecutar;
            btnGuardar.Enabled = Guardar;
            btnValidarDatos.Enabled = Validar; 
            btnProcesarRemisiones.Enabled = Procesar; 
            btnSalir.Enabled = Salir;
        } 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // int iRegistrosHoja = 0;
            // int iRegistrosProcesados = 0;

            sFolio_InventarioInicial = "";
            sFolioVentaEntrada = "";
            sFolioVentaSalida = "";
            sFolioConsignacionEntrada = "";
            sFolioConsignacionSalida = ""; 

            lblProcesados.Visible = false;
            lblProcesados.Text = ""; 

            sFile_In = "";  
            cboHojas.Clear();
            cboHojas.Add();

            sTitulo = "Información "; 
            FrameResultado.Text = sTitulo; 
            Fg.IniciaControles();
            lst.Limpiar();

            btnEjecutar.Enabled = false;
            btnGuardar.Enabled = false;

            //////rdoCompleto.Enabled = false;
            //////rdoCompleto.Checked = false;

            //////rdoParcial.Enabled = false;
            //////rdoParcial.Checked = false;

            IniciaToolBar(true, true, true, false, false, false, false, true);  
            if (!bEsModuloValido)
            {
                IniciaToolBar(false, false, false, false, false, false, true);
                General.msjAviso("Pantalla solo de Almacenes y/o Farmacias.");
            }

            //////if (!DtGeneral.EsAlmacen)
            //////{
            //////    IniciaToolBar( false, false, false, false, false, false, true);
            //////    General.msjAviso("Este módulo es exclusivo para Almacenes, se deshabilitaran todas las opciones."); 
            //////}

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Exportar_Plantilla_De_Inventario(); 
        }

        private void Exportar_Plantilla_De_Inventario()
        {
            bool bRegresa = true;
            int iRegistros = 0; 

            this.Cursor = Cursors.WaitCursor;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Formato_De_Invenarios.xls";
            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Formato_De_Invenarios.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        xpExcel.GeneraExcel(true);
            //        xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

            string sSql = string.Format("Exec spp_Formato_De_Invenarios");

            leer.Exec(sSql);           

            leer.RegistroActual = 1;


            //int iColsEncabezado = iRow + leer.Columnas.Length - 1;
            //iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Inventario";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, sNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openExcel.Title = "Archivos de inventario";
            openExcel.Filter = "Archivos de Excel (*.xls;*.xlsx)| *.xls;*.xlsx";
            //openExcel.Filter = "Archivos de Texto (*.txt)| *.txt"; 
            openExcel.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            openExcel.AddExtension = true;
            lblProcesados.Visible = false;

            // if (openExcel.FileName != "")
            if (openExcel.ShowDialog() == DialogResult.OK) 
            {
                sFile_In = openExcel.FileName;
                
                //CargarArchivo(); 
                IniciaToolBar(false, false, false, false, false, false, false); 
                thReadFile = new Thread(this.CargarArchivo);
                thReadFile.Name = "LeerArchivo";
                thReadFile.Start(); 
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thLoadFile = new Thread(this.thLeerHoja);
            thLoadFile.Name = "LeerDocumentoExcel";
            thLoadFile.Start(); 
            //LeerHoja(); 
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            tmValidacion.Enabled = true;
            tmValidacion.Interval = 1000; 
            tmValidacion.Start();

            thValidarInformacion = new Thread(this.ValidarInformacion);
            thValidarInformacion.Name = "Validar informacion";
            thValidarInformacion.Start();
            System.Threading.Thread.Sleep(200);

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //string[] SCols = {"IdEmpresa", "IdEstado", "IdFarmacia", "IdSubFarmacia", "IdPasillo", "IdEstante", "IdEntrepaño", "CodigoEAN",
            //                     "Costo", "ClaveLote", "Caducidad", "Cantidad"};

            string[] SCols = {"Empresa", "Estado", "Unidad", "IdFuente", "Rack", "Nivel", "Posicion", "CodigoEAN",
                                 "Costo", "Lote", "Caducidad", "Cantidad", "IdLicitacion", "Orden", "FolioPresup"};

            if (excel.ValidarExistenCampos(SCols))
            {
                bGuardandoInformacion = true; 
                tmValidacion.Enabled = true;
                tmValidacion.Interval = 1000;
                tmValidacion.Start();

                thGuardarInformacion = new Thread(this.GuardarInformacion_Inventario);
                thGuardarInformacion.Name = "Guardar información seleccionada";
                thGuardarInformacion.Start();
            }
            else
            {
                General.msjAviso("Columnas incompletas en plantilla cargada, Favor de verificar. ");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private void CargarArchivo()
        {
            string sHoja = "";
            bool bHabilitar = false;

            BloqueaHojas(true);
            MostrarEnProceso(true, 1);
            FrameResultado.Text = sTitulo;

            excel = new SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            bHabilitar = false;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);

                if (sHoja.ToUpper() == sHoja_Inventario.ToUpper())
                {
                    bHabilitar = true; 
                }
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
            IniciaToolBar(true, true, bHabilitar, false, false, false, true);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar; 

            lblProcesados.Text = "";
            if (!bHabilitar)
            {
                lblProcesados.Text = "No existe hoja: Inventario en archivo cargado.";
            }
            else
            {
                thLeerHoja(); 
            }
        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new SC_SolutionsSystem.OfficeOpenXml.Data.clsLeerExcelOpenOficce(sFile_In);
            excel.GetEstructura();

            cboHojas.Clear();
            cboHojas.Add();
            lst.Limpiar();
            Thread.Sleep(1000);

            bHabilitar = excel.Hojas.Registros > 0;
            while (excel.Hojas.Leer())
            {
                sHoja = excel.Hojas.Campo("Hoja");
                cboHojas.Add(sHoja, sHoja);
            }

            cboHojas.SelectedIndex = 0;
            btnEjecutar.Enabled = bHabilitar;
        }

        private void thLeerHoja()
        {
            BloqueaHojas(true);
            MostrarEnProceso(true, 2);
            lblProcesados.Visible = false;

            LeerHoja();

            BloqueaHojas(false);
            MostrarEnProceso(false, 2); 
        }

        private bool LeerHoja()
        {
            bool bRegresa = false;
            
            IniciaToolBar(false, false, false, bRegresa, false, false, false);
            FrameResultado.Text = sTitulo; 
            lst.Limpiar(); 
            //excel.LeerHoja(cboHojas.Data);
            excel.LeerHoja(sHoja_Inventario);

            FrameResultado.Text = sTitulo; 
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros; 
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato)); 
                lst.CargarDatos(excel.DataSetClase, true, true); 
            }

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true);
            ////rdoCompleto.Enabled = bRegresa;
            ////rdoParcial.Enabled = bRegresa;

            return bRegresa; 
        }

        private void BloqueaHojas(bool Bloquear)
        {
            cboHojas.Enabled = !Bloquear; 
        }

        private void ValidarInformacion()
        {
            bValidandoInformacion = true; 
            bActivarProceso = false;
            bErrorAlValidar = false;          
            int iTipo = bEsModulo_Almacen ? 1 : 2;
            int iValidarCajasCompletas = GnFarmacia.ForzarCapturaEnMultiplosDeCajas ? 1 :0 ; 

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false); 
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            string sSql = string.Format("Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', \n" + 
                "\t@ValidarCajasCompletas = '{4}', @TipoDeInventario = '{5}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, iValidarCajasCompletas, (int)tpInventario);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true; 
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()"); 
                General.msjError("Error al validar inventario a ingresar."); 
            }
            else
            {
                if (bEsModulo_Almacen)
                {
                    ValidarInformacion__Almacen();
                }
                else
                {
                    ValidarInformacion__Farmacias();
                }
            }


            bValidandoInformacion = false; 
            bActivarProceso = !bActivarProceso; 
            BloqueaHojas(false);
            MostrarEnProceso(false, 4); 
        }

        private void ValidarInformacion__Almacen()
        {
            clsLeer leerValidacion = new clsLeer(); 

            leer.RenombrarTabla(1, "Racks");
            leer.RenombrarTabla(2, "Niveles");
            leer.RenombrarTabla(3, "Posiciones");
            leer.RenombrarTabla(4, "Caducidades Erroneas");
            leer.RenombrarTabla(5, "EAN Multiples caducidades");
            leer.RenombrarTabla(6, "EAN no registrados");
            leer.RenombrarTabla(7, "EAN sin costo");
            leer.RenombrarTabla(8, "EAN multiples costos");
            leer.RenombrarTabla(9, "Fuentes erroneas");

            leerValidacion.DataTableClase = leer.Tabla(1);   // Racks  
            bActivarProceso = leerValidacion.Registros > 0;

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(2);   // Niveles 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(3);   // Posiciones  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(4);   // Caducidades Erroneas  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(5);   // EAN multiples Caducidades  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(6);   // EAN No Registrados 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(7);   // EAN sin costos 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(8);   // EAN multiples costos 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(9);   // Fuentes Erroneas 
                bActivarProceso = leerValidacion.Registros > 0;
            }
        }

        private void ValidarInformacion__Farmacias()
        {
            clsLeer leerValidacion = new clsLeer();
            leer.RenombrarTabla(1, "Error de caducidades");
            leer.RenombrarTabla(2, "EAN Multiples caducidades");
            leer.RenombrarTabla(3, "EAN no registrados");
            leer.RenombrarTabla(4, "EAN sin costo");
            leer.RenombrarTabla(5, "EAN multiples costos");
            leer.RenombrarTabla(6, "SubFarmacias incorrectas");
            leer.RenombrarTabla(7, "Lotes con formato incorrecto");
            leer.RenombrarTabla(8, "Lotes con cajas incompletas");
            leer.RenombrarTabla(9, "Inventario incorrecto");

            leerValidacion.DataTableClase = leer.Tabla(1);   // Error Caducidades    
            bActivarProceso = leerValidacion.Registros > 0;

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(2);   // EAN multiples Caducidades  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(3);   // EAN No En Catalogo 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(4);   // EAN sin costos 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(5);   // EAN multiples costos 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(6);   // SubFarmacias incorrectas vs lotes 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(7);   // Lotes con formato incorrecto
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(8);   // Lotes con cajas incompletas 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(9);   // Inventario incorrecto 
                bActivarProceso = leerValidacion.Registros > 0;
            }
        } 

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = ""; 

            if (Mostrar)
            {
                FrameProceso.Left = 76;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }

            if (Proceso == 1)
            {
                sTituloProceso = "Leyendo estructura del documento"; 
            }
            
            if (Proceso == 2)
            {
                sTituloProceso = "Leyendo información de hoja seleccionada"; 
            }

            if (Proceso == 3)
            {
                sTituloProceso = "Guardando información de hoja seleccionada"; 
            }

            if (Proceso == 4)
            {
                sTituloProceso = "Verificando información a integrar";
            }

            if (Proceso == 5)
            {
                sTituloProceso = "Integrando información ..... ";
            }

            FrameProceso.Text = sTituloProceso; 

        }

        private bool MarcarProductosParaInventario(int Opcion)
        {
            bool bRegresa = true;
            ////string sSql = string.Format("Exec spp_Mtto_AjustesInv_MarcarDesmarcar_Productos '{0}', '{1}', '{2}', '{3}' ",
            ////     DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Opcion);

            ////    if (!leer.Exec(sSql))
            ////    {
            ////        Error.GrabarError(leer, "MarcarProductosParaInventario");
            ////        General.msjError(sMsjError);
            ////    }
            ////    else
            ////    {
            ////        bRegresa = true;
            ////        General.msjUser(sMsjGuardar);
            ////    }
            return bRegresa;
        }

        #endregion Funciones y Procedimientos Privados 

        #region Guardar Informacion 
        private void thGuardarInformacion_Inventario()
        {
            ////BloqueaHojas(true);
            ////MostrarEnProceso(true, 3); 
            ////Thread.Sleep(1000);
            ////this.Refresh(); 

            GuardarInformacion_Inventario();

            ////BloqueaHojas(false);
            ////MostrarEnProceso(false, 3);
            // IniciaToolBar(true, true, true, true, true); 
        }

        private string DarFormato(string Valor)
        {
            string sRegresa = Valor.Trim();

            sRegresa = sRegresa.Replace("'", "");
            sRegresa = sRegresa.Replace(",", "");

            return sRegresa;
        }

        private string Formatear_Caducidad(clsLeer LeerCaducidad)
        {
            string sRegresa = "0000-00-00";
            // excel.Campo("Caducidad")

            try
            {
                if (LeerCaducidad.Campo("Caducidad") != "")
                {
                    DateTime dt = LeerCaducidad.CampoFecha("Caducidad");
                    sRegresa = General.FechaYMD(dt); 
                }
            }
            catch 
            { 
            }

            return sRegresa; 
        }

        private void GuardarInformacion_Inventario()
        {
            bool bRegresa = false;
            string sSql = ""; 
            clsLeer leerGuardar = new clsLeer(ref cnn);
            int iCantidad = 0;


            BloqueaHojas(true);
            MostrarEnProceso(true, 3);
            IniciaToolBar(false, false, false, false, false, false, false); 

            lblProcesados.Visible = true;
            lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


            // leerGuardar.DataSetClase = excel.DataSetClase;
            excel.RegistroActual = 1;
            bRegresa = excel.Registros > 0;
            iRegistrosProcesados = 0;

            leerGuardar.Exec("Truncate Table INV__InventarioInterno_CargaMasiva ");

            //bRegresa = CargarInformacionDeHoja();

            
            while (excel.Leer())
            {
                iCantidad = 0;

                iCantidad = excel.CampoInt("Cantidad");

                //if (iCantidad > 0)
                {
                    sSql = string.Format("Insert Into INV__InventarioInterno_CargaMasiva " +
                        " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
                        " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad, TipoDeInventario ) \n");
                    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}'  ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,

                        tpTipoCarga == TiposDeCargaMasiva.EntradaDeConsignacion ? SubFarmacia : Fg.PonCeros(DarFormato(excel.Campo("IdFuente")), 3),

                        DarFormato(bEsModulo_Almacen ? excel.Campo("Rack") : "0"),
                        DarFormato(bEsModulo_Almacen ? excel.Campo("Nivel") : "0"),
                        DarFormato(bEsModulo_Almacen ? excel.Campo("Posicion") : "0"),
                        DarFormato(excel.Campo("CodigoEAN")),
                        excel.Campo("Costo") == "" ? "0" : excel.Campo("Costo"),
                        DarFormato(excel.Campo("Lote")),
                        DarFormato(Formatear_Caducidad(excel)),
                        excel.CampoInt("Cantidad"), (int)tpInventario
                        );

                    ////BloqueaHojas(true);
                    ////MostrarEnProceso(true, 3);

                    if (!leerGuardar.Exec(sSql))
                    {
                        bRegresa = false;
                        //leerGuardar.Exec("Truncate Table INV__Inventario_CargaMasiva ");
                        Error.GrabarError(leerGuardar, "GuardarInformacion_Inventario__Almacen");
                        break;
                    }
                }
                iRegistrosProcesados++;
                lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato)); 
            }
            

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            bGuardandoInformacion = false;

            if (!bRegresa)
            { 
                ////General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar(true, true, true, true, false, false, true);
            } 
            else 
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'INV__Inventario_CargaMasiva'  ");
                ////General.msjUser("Información de inventario cargada satisfactoriamente."); 
                IniciaToolBar(true, true, true, false, true, false, true);               
                
                ValidarInformacion(); 
            }
        }

        private static DataSet TipoColumna( DataSet Datos, string Tabla, string NombreColumna, TypeCode typeCode )
        {
            DataSet dts = Datos.Copy();
            DataTable dt;
            clsLeer leer = new clsLeer();
            int iRenglones = 0;

            leer.DataSetClase = Datos;
            dt = leer.Tabla(Tabla);

            try
            {
                Type type = Type.GetType("System." + typeCode);


                // Agregar columna Temporal
                DataColumn dc = new DataColumn(NombreColumna + "_new", type);

                //Darle la posición de a la columna nueva de la original
                int ordinal = dt.Columns[NombreColumna].Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                //leer el valor y convertirlo 
                foreach(DataRow dr in dt.Rows)
                {
                    iRenglones++;
                    if(typeCode == TypeCode.Boolean)
                    {
                        switch(dr[NombreColumna].ToString())
                        {
                            case "0":
                                dr[dc.ColumnName] = false;
                                break;
                            case "1":
                                dr[dc.ColumnName] = true;
                                break;
                            default:
                                dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                                break;
                        }
                    }
                    else
                    {
                        dr[dc.ColumnName] = Convert.ChangeType(dr[NombreColumna], typeCode);
                    }
                }


                // remover columna vieja
                dt.Columns.Remove(NombreColumna);


                // Cambiar nombre a columna nueva
                dc.ColumnName = NombreColumna;
            }
            catch(Exception exception)
            {
                exception = null;
            }

            leer.DataTableClase = dt;

            return leer.DataSetClase.Copy();
        }

        private DataSet FormatearCampo( DataSet Datos, string NombreColumna, TypeCode typeCode )
        {
            clsLeer datosProceso = new clsLeer();

            try
            {
                datosProceso.DataSetClase = Datos;

                if(datosProceso.ExisteTablaColumna(sHoja_Inventario, NombreColumna))
                {
                    datosProceso.DataSetClase = TipoColumna(Datos, sHoja_Inventario, NombreColumna, typeCode);
                }
            }
            catch
            {
                datosProceso.DataSetClase = Datos;
            }

            return datosProceso.DataSetClase;
        }

        private DataSet FormatearDatos()
        {
            clsLeer dtTabla = new clsLeer();

            excel.RenombrarTabla(1, sHoja_Inventario);

            dtTabla.DataTableClase = excel.Tabla(1); 

            DataSet datosProceso = dtTabla.DataSetClase;

            datosProceso = FormatearCampo(datosProceso, "IdEmpresa", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdEstado", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdFarmacia", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdSubFarmacia", TypeCode.String);

            datosProceso = FormatearCampo(datosProceso, "IdPasillo", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdEstante", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "IdEntrepaño", TypeCode.String);

            datosProceso = FormatearCampo(datosProceso, "CodigoEAN", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "ClaveLote", TypeCode.String);
            datosProceso = FormatearCampo(datosProceso, "Caducidad", TypeCode.DateTime);
            datosProceso = FormatearCampo(datosProceso, "Cantidad", TypeCode.Int32);
            datosProceso = FormatearCampo(datosProceso, "Costo", TypeCode.Double);

            return datosProceso;

        }

        private bool CargarInformacionDeHoja()
        {
            //string[] SCols = {"IdEmpresa", "IdEstado", "IdFarmacia", "IdSubFarmacia", "IdPasillo", "IdEstante", "IdEntrepaño", "CodigoEAN",
            //                     "Costo", "ClaveLote", "Caducidad", "Cantidad"};

            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref cnn);
            clsBulkCopy bulk = new clsBulkCopy(General.DatosConexion);
            DataSet dtsCarga = new DataSet("Nuevo");
            DataTable dtCarga = excel.Tabla(sHoja_Inventario);

            dtsCarga.Tables.Add(dtCarga);

            //dtsCarga = FormatearDatos();

            lblProcesados.Visible = true;
            bulk.NotifyAfter = 500;
            bulk.RowsCopied += new clsBulkCopy.RowsCopiedEventHandler(bulk_RowsCopied);
            bulk.OnCompled += new clsBulkCopy.RowsCopiedEventHandler(bulk_Compled);
            bulk.OnError += new clsBulkCopy.RowsCopiedEventHandler(bulk_Error);



            bulk.ClearColumns();
            bulk.DestinationTableName = "INV__InventarioInterno_CargaMasiva";
            bulk.AddColumn("IdEmpresa", "IdEmpresa");
            bulk.AddColumn("IdEstado", "IdEstado");
            bulk.AddColumn("IdFarmacia", "IdFarmacia");
            //bulk.AddColumn("SKU", "SKU");
            bulk.AddColumn("IdSubFarmacia", "IdSubFarmacia");

            bulk.AddColumn("IdPasillo", "IdPasillo");
            bulk.AddColumn("IdEstante", "IdEstante");
            bulk.AddColumn("IdEntrepaño", "IdEntrepaño");
            bulk.AddColumn("CodigoEAN", "CodigoEAN");

            bulk.AddColumn("Costo", "Costo");
            bulk.AddColumn("ClaveLote", "ClaveLote");
            bulk.AddColumn("Caducidad", "Caducidad");
            bulk.AddColumn("Cantidad", "Cantidad");


            leerGuardar.Exec("Truncate table INV__InventarioInterno_CargaMasiva ");
            bRegresa = bulk.WriteToServer(dtsCarga); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 

            if(!bRegresa)
            {
                Error.GrabarError(leerGuardar, "CargarInformacionDeHoja");
            }

            return bRegresa;
        }

        private void bulk_RowsCopied( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error( RowsCopiedEventArgs e )
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");

            General.msjError("Ocurrió un error al cargar la información del inventario.");
            IniciaToolBar(true, true, true, true, false, false, true);
        }

        ////private void GuardarInformacion_Inventario__Farmacia()
        ////{
        ////    bool bRegresa = false;
        ////    string sSql = "";
        ////    clsLeer leerGuardar = new clsLeer(ref cnn);

        ////    BloqueaHojas(true);
        ////    MostrarEnProceso(true, 3);
        ////    IniciaToolBar(false, false, false, false, false, false, false);

        ////    lblProcesados.Visible = true;
        ////    lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));


        ////    // leerGuardar.DataSetClase = excel.DataSetClase;
        ////    excel.RegistroActual = 1;
        ////    bRegresa = excel.Registros > 0;
        ////    iRegistrosProcesados = 0;

        ////    leerGuardar.Exec("Truncate Table INV__InventarioInterno_CargaMasiva ");
        ////    while (excel.Leer())
        ////    {
        ////        sSql = string.Format("Insert Into INV__InventarioInterno_CargaMasiva " +
        ////            " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
        ////            " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad ) \n");
        ////        sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'  ",
        ////            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
        ////            Fg.PonCeros(DarFormato(excel.Campo("IdSubFarmacia")), 2),
        ////            DarFormato("0"), 
        ////            DarFormato("0"), 
        ////            DarFormato("0"), 
        ////            DarFormato(excel.Campo("CodigoEAN")),
        ////            excel.Campo("Costo"),
        ////            DarFormato(excel.Campo("ClaveLote")),
        ////            DarFormato(excel.Campo("Caducidad")),
        ////            excel.CampoInt("Cantidad")
        ////            );

        ////        ////BloqueaHojas(true);
        ////        ////MostrarEnProceso(true, 3);

        ////        if (!leerGuardar.Exec(sSql))
        ////        {
        ////            bRegresa = false;
        ////            //leerGuardar.Exec("Truncate Table INV__Inventario_CargaMasiva ");
        ////            Error.GrabarError(leerGuardar, "GuardarInformacion_Inventario__Farmacia");
        ////            break;
        ////        }
        ////        iRegistrosProcesados++;
        ////        lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
        ////    }

        ////    BloqueaHojas(false);
        ////    MostrarEnProceso(false, 3);

        ////    if (!bRegresa)
        ////    {
        ////        General.msjError("Ocurrió un error al cargar la información del inventario.");
        ////        IniciaToolBar(true, true, true, true, false, false, true);
        ////    }
        ////    else
        ////    {
        ////        leerGuardar.Exec("Exec spp_FormatearTabla 'INV__Inventario_CargaMasiva'  ");
        ////        General.msjUser("Información de inventario cargada satisfactoriamente.");
        ////        IniciaToolBar(true, true, true, false, true, false, true);
        ////    }
        ////}
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            ////if (ValidarTipoInventario())
            {
                if (validarIntegracionInventario())
                {
                    thGeneraFolios = new Thread(this.IntegrarInventarioInterno);
                    thGeneraFolios.Name = "Integrar informacion";
                    thGeneraFolios.Start();
                }
            }
        }

        private bool ValidarTipoInventario()
        {
            bool bRegresa = true;

            ////////if (!rdoCompleto.Checked && !rdoParcial.Checked)
            ////////{
            ////////    bRegresa = false;
            ////////    General.msjUser("Favor de seleccionar el Tipo de Inventario.");
            ////////}

            return bRegresa;
        }

        private bool validarIntegracionInventario()
        {
            bool bRegresa = false;
            string sMsj = "El proceso de integración de inventario inicial cargara el contenido de la plantilla, ¿ Desea continuar ? ";
            sMsj = "Este proceso integrará la información de la plantilla como Inventario Inicial,\n\n¿ Desea continuar ?";

            if (tpTipoCarga == TiposDeCargaMasiva.CompraDirecta)
            {
                sMsj = "Este proceso integrará la información de la plantilla como Compra Directa,\n\n¿ Desea continuar ?";
            }


            if (General.msjConfirmar(sMsj) == DialogResult.Yes)
            {
                bRegresa = true; 
            }

            return bRegresa; 
        }

        private void IntegrarInventarioInterno() 
        {
            bool bContinua = true;
            string sSql = "";
            int iTipo = bEsModulo_Almacen ? 1 : 2;
            int iTipoInv = (int)tpInventario;    // Inventario parcial 
  
            leer = new clsLeer(ref cnn);
            leer.Conexion.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            if (DtGeneral.ConfirmacionConHuellas)
            {
                sMsjNoEncontrado = "El usuario no tiene permiso para aplicar un inventario, verifique por favor.";
                ////bContinua = opPermisosEspeciales.VerificarPermisos("INTEGRARINVENTARIO", sMsjNoEncontrado);
                bContinua = DtGeneral.PermisosEspeciales_Biometricos.VerificarPermisos("INTEGRARINVENTARIO", sMsjNoEncontrado);
            } 


            if (bContinua)
            {
                if (!cnn.Abrir())
                {
                    Error.LogError(cnn.MensajeError);
                    General.msjErrorAlAbrirConexion();
                }
                else
                {
                    cnn.IniciarTransaccion(); 

                    BloqueaHojas(true); 
                    MostrarEnProceso(true, 5);
                    IniciaToolBar(false, false, false, false, false, false, false);

                    if (tpTipoCarga == TiposDeCargaMasiva.CompraDirecta)
                    {
                        sSql = string.Format("Exec sp_Proceso_Integrar_CompraDirecta \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', \n" + 
                            "\t@IdProveedor = '{4}', @ReferenciaDocto = '{5}', @FechaDocto = '{6}', @FechaVenceDocto = '{7}', @Observaciones = '{8}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                            DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, IdProveedor, Referencia, FechaDocumento, FechaVenceDocumento, Observaciones); 
                    }

                    if (tpTipoCarga == TiposDeCargaMasiva.EntradaDeConsignacion)
                    {
                        sSql = string.Format("Exec sp_Proceso_Integrar_EntradaPorConsginacion \n" +
                            "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', \n" +
                            "\t@IdProveedor = '{4}', @ReferenciaDocto = '{5}', @Observaciones = '{6}'",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                            DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, IdProveedor, Referencia, Observaciones);
                    }


                    if (tpTipoCarga == TiposDeCargaMasiva.InventarioInicial)
                    {
                        sSql = string.Format("Exec sp_Proceso_IntegrarInventarioInicial \n" +
                            "@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', @Tipo = '{4}', @TipoInv = '{5}' \n",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                            DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iTipo, iTipoInv);
                    }


                    if (!leer.Exec(sSql))
                    {
                        bContinua = false;
                        //General.msjError("Ocurrió un Error al Procesar las Remisiones");
                        //Error.GrabarError(leer, "ProcesarRemisiones");
                    }
                    else
                    {
                        leer.Leer();
                        if (tpTipoCarga == TiposDeCargaMasiva.CompraDirecta)
                        {
                            sFolio_InventarioInicial = leer.Campo("Clave");
                            sMensaje = string.Format("Se cargo la Compra Directa con el folio {0} ", sFolio_InventarioInicial);
                        }

                        if (tpTipoCarga == TiposDeCargaMasiva.EntradaDeConsignacion)
                        {
                            sFolio_InventarioInicial = leer.Campo("Clave");
                            sMensaje = string.Format("Se cargo la Entrada de Consignación con el folio {0} ", sFolio_InventarioInicial);
                        }

                        if (tpTipoCarga == TiposDeCargaMasiva.InventarioInicial)
                        {
                            sFolio_InventarioInicial = leer.Campo("FolioInventario");
                            sMensaje = string.Format("Se cargo el inventario inicial con el folio {0} ", sFolio_InventarioInicial);
                        }
                    }


                    //////if (bContinua)
                    //////{
                    //////    bContinua = AplicarPolizaDeInventario();
                    //////}

                    BloqueaHojas(false);
                    MostrarEnProceso(false, 5);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        InformacionIntegrada = true; 
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP

                        ImprimirInventario();
                        IniciaToolBar(true, true, true, false, false, false, true);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "IntegrarInventarioInterno()");
                        General.msjError("Ocurrió un error al integrar la información.");
                        IniciaToolBar(true, false, false, false, false, true, true);
                    }

                    cnn.Cerrar();

                    if (bContinua)
                    {
                        this.Close(); 
                    }
                }
            }
        }


        private bool AplicarPolizaDeInventario()
        {
            bool bRegresa = true;
            //////string sFolios = "";
            //////// string sFolioVentaEntrada = "", sFolioConsignacionEntrada = "", sFolioVentaSalida = "", sFolioConsignacionSalida = "";

            //////string sSql = string.Format("Exec spp_Mtto_AjustesDeInventario  '{0}', '{1}', '{2}', '{3}', '{4}' ",
            //////     DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
            //////    DtGeneral.FarmaciaConectada, sFolio_InventarioInicial, DtGeneral.IdPersonal); 

            //////if (!leer.Exec(sSql))
            //////{
            //////    bRegresa = false;
            //////}
            //////else
            //////{
            //////    leer.Leer();
            //////    sFolioVentaEntrada = leer.Campo("FolioVentaEntrada");
            //////    sFolioVentaSalida = leer.Campo("FolioVentaSalida");
            //////    sFolioConsignacionEntrada = leer.Campo("FolioConsignacionEntrada");
            //////    sFolioConsignacionSalida = leer.Campo("FolioConsignacionSalida");

            //////    if (sFolioVentaEntrada != "")
            //////    {
            //////        sFolios = sFolioVentaEntrada;
            //////        if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            //////        {
            //////            bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaEntrada);
            //////        }
            //////    }

            //////    if (sFolioVentaSalida != "")
            //////    {
            //////        sFolios += sFolios != "" ? ", " + sFolioVentaSalida : sFolioVentaSalida;
            //////        if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            //////        {
            //////            bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaSalida);
            //////        }
            //////    }

            //////    if (sFolioConsignacionEntrada != "")
            //////    {
            //////        sFolios += sFolios != "" ? ", " + sFolioConsignacionEntrada : sFolioConsignacionEntrada;
            //////        if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            //////        {
            //////            bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionEntrada);
            //////        }
            //////    }

            //////    if (sFolioConsignacionSalida != "")
            //////    {
            //////        sFolios += sFolios != "" ? ", " + sFolioConsignacionSalida : sFolioConsignacionSalida;
            //////        if (DtGeneral.ConfirmacionConHuellas && bRegresa)
            //////        {
            //////            bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionSalida);
            //////        }
            //////    }

            //////    sMensaje = string.Format("La Póliza {0} se aplicó exitosamente con los Folios :\n\n{1}", sFolio_InventarioInicial, sFolios);
            //////}

            return bRegresa;
        }


        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;


            if (!bGuardandoInformacion)
            {
                if (!bValidandoInformacion)
                {
                    if (bActivarProceso)
                    {
                        IniciaToolBar(true, true, true, false, false, true, true);
                    } 
                    else
                    {
                        IniciaToolBar(true, true, true, false, true, false, true);
                        if (!bErrorAlValidar)
                        {
                            FrmIncidencias f = new FrmIncidencias(leer.DataSetClase);
                            f.ShowDialog(this);
                        }
                    }
                }
                else
                {
                    tmValidacion.Enabled = true;
                    tmValidacion.Start();
                }
            }
            else
            {
                tmValidacion.Enabled = true;
                tmValidacion.Start();
            }
        }

        #region Impresion de informacion
        private void ImprimirInventario()
        {
            ////if (sFolioVentaEntrada != "")
            ////{
            ////    Imprimir(sFolioVentaEntrada);
            ////}

            ////if (sFolioVentaSalida != "")
            ////{
            ////    Imprimir(sFolioVentaSalida);
            ////}

            ////if (sFolioConsignacionEntrada != "")
            ////{
            ////    Imprimir(sFolioConsignacionEntrada);
            ////}

            ////if (sFolioConsignacionSalida != "")
            ////{
            ////    Imprimir(sFolioConsignacionSalida);
            ////}

            ////ImprimirDiferenciasAjuste();
        }

        private void Imprimir(string Folio)
        {
            ////bool bRegresa = false;
          
            ////DatosCliente.Funcion = "ImprimirInventario()";
            ////clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            ////// byte[] btReporte = null;

            ////myRpt.RutaReporte = GnFarmacia.RutaReportes;
            ////myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

            ////myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            ////myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            ////myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            ////myRpt.Add("Folio", Folio);

            ////bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////if (!bRegresa)
            ////{
            ////    General.msjError("Ocurrió un error al cargar el reporte.");
            ////}
        }

        private void ImprimirDiferenciasAjuste()
        {
            ////bool bRegresa = false; 
            ////DatosCliente.Funcion = "ImprimirDiferenciasAjuste()";
            ////clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            ////myRpt.RutaReporte = GnFarmacia.RutaReportes;
            ////myRpt.NombreReporte = "PtoVta_Ajustes_Inv_Diferencias.rpt";

            ////myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            ////myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            ////myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            ////myRpt.Add("Folio", sFolio_InventarioInicial);

            ////bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            ////if (!bRegresa)
            ////{
            ////    General.msjError("Ocurrió un error al cargar el reporte.");
            ////}
            ////else
            ////{
            ////    validarExportarDiferencias_Excel(); 
            ////}
        }

        private void validarExportarDiferencias_Excel()
        {
            ////bool bExportar = false;

            ////if (General.msjConfirmar("¿ Desea exportar a excel el archivo de Diferencias de Inventario ?", "Exportar información") == System.Windows.Forms.DialogResult.Yes)
            ////{
            ////    bExportar = true; 
            ////}

            ////if (bExportar)
            ////{
            ////    ExportarDiferencias_Excel(); 
            ////}
        }

        private void ExportarDiferencias_Excel()
        {
            ////bool bRegresa = false;
            ////string sSql = string.Format("" +
            ////    "Select IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, \n " + 
            ////    "Poliza, PolizaAplicada, MovtoAplicado, IdPersonal, NombrePersonal, FechaRegistro, IdClaveSSA, ClaveSSA, ClaveSSA_Base, \n " + 
            ////    "DescripcionClave, IdProducto, CodigoEAN, DescProducto, IdPresentacion, Presentacion, ContenidoPaquete, ClaveLote, \n  " + 
            ////    "EsConsignacion, ExistenciaFisica, Costo, Importe, ExistenciaSistema, FechaReg, FechaCad, FechaCad_Aux, \n " + 
            ////    "ExistenciaActualFarmacia, Diferencia, Referencia, StatusDet_Lotes, StatusFarmaciaLote, KeyxDetalleLote, Observaciones \n " +
            ////    "From vw_AjustesInv_Det_Lotes (NoLock) " +
            ////    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Poliza = '{3}' ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolio_InventarioInicial);

            ////this.Cursor = Cursors.Default; 
            ////if (!leer.Exec(sSql))
            ////{
            ////    this.Cursor = Cursors.WaitCursor;
            ////    Error.GrabarError(leer, "btnExportarExcel_Click()");
            ////    General.msjError("Ocurrió un error al obtener la información de los productos.");
            ////}
            ////else
            ////{
            ////    if (!leer.Leer())
            ////    {
            ////        General.msjAviso("No existe información para exportar, verifique.");
            ////    }
            ////    else
            ////    {
            ////        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Ajustes_Inv_Diferencias.xls";
            ////        bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Ajustes_Inv_Diferencias.xls", DatosCliente);

            ////        if (!bRegresa)
            ////        {
            ////            this.Cursor = Cursors.Default;
            ////            General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            ////        }
            ////        else
            ////        {
            ////            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////            xpExcel.AgregarMarcaDeTiempo = false;

            ////            leer.RegistroActual = 1; 
            ////            if (xpExcel.PrepararPlantilla())
            ////            {
            ////                xpExcel.GeneraExcel();

            ////                ////sSql = string.Format("" +
            ////                ////    "Select IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, \n " +
            ////                ////    "Poliza, PolizaAplicada, MovtoAplicado, IdPersonal, NombrePersonal, FechaRegistro, IdClaveSSA, ClaveSSA, ClaveSSA_Base, \n " +
            ////                ////    "DescripcionClave, IdProducto, CodigoEAN, DescProducto, IdPresentacion, Presentacion, ContenidoPaquete, ClaveLote, \n  " +
            ////                ////    "EsConsignacion, ExistenciaFisica, Costo, Importe, ExistenciaSistema, FechaReg, FechaCad, FechaCad_Aux, \n " +
            ////                ////    "ExistenciaActualFarmacia, Diferencia, Referencia, StatusDet_Lotes, StatusFarmaciaLote, KeyxDetalleLote, Observaciones \n " +
            ////                ////    "From vw_AjustesInv_Det_Lotes (NoLock) " +
            ////                ////    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Polizad = '{3}' ",
            ////                ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFolio_InventarioInicial);

            ////                ///////////////////// 
            ////                xpExcel.Agregar(leer.Campo("Empresa"), 2, 2);
            ////                xpExcel.Agregar(leer.Campo("Farmacia"), 3, 2);
            ////                xpExcel.Agregar(leer.Campo("FechaRegistro"), 5, 3);

            ////                for (int iRow = 8; leer.Leer(); iRow++)
            ////                {
            ////                    int iCol = 2;
            ////                    xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("IdProducto"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("DescProducto"), iRow, iCol++);

            ////                    xpExcel.Agregar(leer.Campo("SubFarmacia"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("ClaveLote"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("FechaCad_Aux"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("Presentacion"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("ContenidoPaquete"), iRow, iCol++);

            ////                    xpExcel.Agregar(leer.Campo("ExistenciaSistema"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("ExistenciaFisica"), iRow, iCol++);
            ////                    xpExcel.Agregar(leer.Campo("Diferencia"), iRow, iCol++);

            ////                }

            ////                xpExcel.CerrarDocumento();

            ////                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////                {
            ////                    xpExcel.AbrirDocumentoGenerado();
            ////                }
            ////            }
            ////        }
            ////    }
            ////}
            ////this.Cursor = Cursors.Default;
        }
        #endregion Impresion de informacion

        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            tmPantalla.Enabled = false;
            ////if (!DtGeneral.ValidaTransferenciasTransito())
            ////{
            ////    this.Close();
            ////}
        }
        #endregion Boton_ProcesarRemisiones 


    }
}
