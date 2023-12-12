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
using SC_SolutionsSystem.OfficeOpenXml;
using SC_SolutionsSystem.OfficeOpenXml.Data;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Inventario
{
    public partial class FrmIntegrarInventario : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        //clsLeerExcel excel;
        clsLeerExcelOpenOficce excel;

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

        string sFormato = "###, ###, ###, ##0";
        string sFormato_INT = "###, ###, ###, ##0";
        int iFolioCargaMasiva = 0;
        string sMensaje = "";
        string sMsjGuardar = "";
        string sMsjError = ""; 

        bool bValidandoInformacion = false;
        bool bSeEncontraronIndicencias = false;
        bool bActivarProceso = false; 
        bool bErrorAlValidar = false;
        bool bPermitirAjustesInventario_Con_ExistenciaEnTransito = GnFarmacia.PermitirAjustesInventario_Con_ExistenciaEnTransito; 

        string sPolizaInventario = ""; 
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

        public FrmIntegrarInventario()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            if (DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen || DtGeneral.ModuloEnEjecucion == TipoModulo.Farmacia)
            {
                bEsModuloValido = true;
                bEsModulo_Almacen = DtGeneral.ModuloEnEjecucion == TipoModulo.Almacen ? true : false; 
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
            excel = new clsLeerExcelOpenOficce();

            lst = new clsListView(lstVwInformacion);
            lst.OrdenarColumnas = false; 

            FrameResultado.Height = 350;
            FrameResultado.Width = 800;

            FrameProceso.Top = 170;
            FrameProceso.Left = 116; 
            MostrarEnProceso(false, 0);
            ////opPermisosEspeciales = new clsOperacionesSupervizadasHuellas(ref cnn, sEmpresa, sEstado, sFarmacia, sPersonal);
        }

        private void FrmIntegrarInventario_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);

            tmPantalla.Enabled = true;
            tmPantalla.Start();
        }

        #region Botones 
        private void IniciaToolBar(bool Nuevo, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            IniciaToolBar(Nuevo, false, Abrir, Ejecutar, Guardar, Validar, Procesar, Salir); 
        }

        private void IniciaToolBar(bool Nuevo, bool ExportarPlantila, bool Abrir, bool Ejecutar, bool Guardar, bool Validar, bool Procesar, bool Salir)
        {
            btnNuevo.Enabled = Nuevo;
            btnExportarExcel.Enabled = ExportarPlantila;
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

            sPolizaInventario = "";
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

            rdoCompleto.Enabled = false;
            rdoCompleto.Checked = false;

            rdoParcial.Enabled = false;
            rdoParcial.Checked = false;

            IniciaToolBar(true, true, true, false, false, false, false, true);  
            if (!bEsModuloValido)
            {
                IniciaToolBar(false, false, false, false, false, false, true);
                General.msjAviso("Este módulo es exclusivo para Almacenes y/o Farmacias, se deshabilitaran todas las opciones.");
            }

            //////if (!DtGeneral.EsAlmacen)
            //////{
            //////    IniciaToolBar( false, false, false, false, false, false, true);
            //////    General.msjAviso("Este módulo es exclusivo para Almacenes, se deshabilitaran todas las opciones."); 
            //////}

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (General.msjConfirmar("Se generara la plantilla para la captura de Inventario, este proceso bloqueara todas las Claves.\n\n¿Desea continuar?") == DialogResult.Yes)
            {
                Exportar_Plantilla_De_Inventario();
            }
        }

        private void Exportar_Plantilla_De_Inventario()
        {
            bool bRegresa = true;
            int iRegistros = 0;
            int iManejaUbicaciones = GnFarmacia.ManejaUbicaciones ? 1 : 0;
            string sSql = ""; 

            sSql = string.Format("Exec spp_INV_GenerarPlantilla_Inventario   @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @ManejaUbicaciones = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iManejaUbicaciones); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click()");
                General.msjError("Ocurrió un error al obtener la información de los productos.");
            }

            if (leer.Registros == 0)
            {
                General.msjAviso("No existe información para exportar, verifique.");
            }
            else
            {
                //this.Cursor = Cursors.WaitCursor;
                //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Formato_De_Invenarios.xls";
                //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Formato_De_Invenarios.xls", DatosCliente);

                Generar_Excel();

            }
        }

        private bool Generar_Excel()
        {
            bool bRegresa = false;

            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            string sNombreDocumento = "Plantilla_de_Inventario";
            string sNombreHoja = "Inventario";
            string sConcepto = "Plantilla_de_Inventario";

            int iHoja = 1, iRenglon = 1;
            int iColBase = 2;
            int iColsEncabezado = 8;


            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                //excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 1;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

            return bRegresa;
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
            string[] SCols = {"IdEmpresa", "IdEstado", "IdFarmacia", "IdSubFarmacia", "IdPasillo", "IdEstante", "IdEntrepaño", "CodigoEAN",
                                 "Costo", "ClaveLote", "Caducidad", "Cantidad"};
            if (excel.ValidarExistenCampos(SCols))
            {
                thGuardarInformacion = new Thread(this.GuardarInformacion_Inventario);
                thGuardarInformacion.Name = "Guardar información seleccionada";
                thGuardarInformacion.Start();
            }
            else
            {
                General.msjAviso("No se encontraron todas las columnas requeridas en la plantilla para la integración del inventario, verifique. ");
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

            excel = new clsLeerExcelOpenOficce(sFile_In);
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
            IniciaToolBar(true, true, bHabilitar, false, false, false, true);

            BloqueaHojas(false);
            MostrarEnProceso(false, 1);
            // btnGuardar.Enabled = bHabilitar;

        }

        private void LeerExcel()
        {
            string sHoja = "";
            bool bHabilitar = false;
            excel = new clsLeerExcelOpenOficce(sFile_In);
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
            excel.LeerHoja(cboHojas.Data);

            FrameResultado.Text = sTitulo; 
            if (excel.Leer())
            {
                bRegresa = true;
                iRegistrosHoja = excel.Registros; 
                FrameResultado.Text = string.Format("{0}: {1} registros ", sTitulo, iRegistrosHoja.ToString(sFormato));

                if(excel.Registros > 1000)
                {
                    FrameResultado.Text = string.Format("{0}: {1} registros, número de registros excede el máximo de 1000 ", sTitulo, iRegistrosHoja.ToString(sFormato));
                }
                else
                {
                    lst.CargarDatos(excel.DataSetClase, true, true);
                }
            }

            // btnGuardar.Enabled = bRegresa;
            IniciaToolBar(true, true, true, bRegresa, false, false, true);
            rdoCompleto.Enabled = bRegresa;
            rdoParcial.Enabled = bRegresa;
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
            int iValidarCajasCompletas = GnFarmacia.ForzarCapturaEnMultiplosDeCajas ? 1 : 0;
            string sSql = ""; 

            IniciaToolBar(false, false, false, false, false, bActivarProceso, false); 
            BloqueaHojas(true);
            MostrarEnProceso(true, 4);
            lblProcesados.Visible = false;

            sSql = string.Format("Exec sp_Proceso_IntegrarInventarioInterno_000_Validar_Datos_De_Entrada \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}', @ValidarCajasCompletas = '{4}', @TipoDeInventario = '{5}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipo, iValidarCajasCompletas, 0);


            if (!leer.Exec(sSql))
            {
                bErrorAlValidar = true; 
                bActivarProceso = !bActivarProceso;

                Error.GrabarError(leer, "ValidarInformacion()"); 
                General.msjError("Ocurrió un error al verificar el inventario a integrar."); 
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

            leer.RenombrarTabla(1, "Pasillos");
            leer.RenombrarTabla(2, "Estantes");
            leer.RenombrarTabla(3, "Entrepaños");
            leer.RenombrarTabla(4, "Error de caducidades");
            leer.RenombrarTabla(5, "EAN Multiples caducidades");
            leer.RenombrarTabla(6, "EAN no registrados");
            leer.RenombrarTabla(7, "EAN sin costo");
            leer.RenombrarTabla(8, "EAN multiples costos");
            leer.RenombrarTabla(9, "SubFarmacias incorrectas");

            leerValidacion.DataTableClase = leer.Tabla(1);   // Pasillos  
            bActivarProceso = leerValidacion.Registros > 0;

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(2);   // Estantes 
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(3);   // Entrepaños  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(4);   // Error Caducidades  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(5);   // EAN multiples Caducidades  
                bActivarProceso = leerValidacion.Registros > 0;
            }

            if (!bActivarProceso)
            {
                leerValidacion.DataTableClase = leer.Tabla(6);   // EAN No En Catalogo 
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
                leerValidacion.DataTableClase = leer.Tabla(9);   // SubFarmacias incorrectas vs lotes 
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
        } 

        private void MostrarEnProceso(bool Mostrar, int Proceso)
        {
            string sTituloProceso = ""; 

            if (Mostrar)
            {
                FrameProceso.Left = 116;
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
                sTituloProceso = "Integrando inventario ..... ";
            }

            FrameProceso.Text = sTituloProceso; 

        }

        private bool MarcarProductosParaInventario(int Opcion)
        {
            bool bRegresa = false;
            string sSql = string.Format("Exec spp_Mtto_AjustesInv_MarcarDesmarcar_Productos '{0}', '{1}', '{2}', '{3}' ",
                 DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Opcion);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "MarcarProductosParaInventario");
                    General.msjError(sMsjError);
                }
                else
                {
                    bRegresa = true;
                    General.msjUser(sMsjGuardar);
                }
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
            
            bRegresa = CargarInformacionDeHoja(); 
            
            /*
            while (1 == 0 && excel.Leer())
            {
                iCantidad = 0;

                iCantidad = excel.CampoInt("Cantidad");

                //if (iCantidad > 0)
                {
                    sSql = string.Format("Insert Into INV__InventarioInterno_CargaMasiva " +
                        " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
                        " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad ) \n");
                    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'  ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                        Fg.PonCeros(DarFormato(excel.Campo("IdSubFarmacia")), 2),
                        DarFormato(bEsModulo_Almacen ? excel.Campo("IdPasillo") : "0"),
                        DarFormato(bEsModulo_Almacen ? excel.Campo("IdEstante") : "0"),
                        DarFormato(bEsModulo_Almacen ? excel.Campo("IdEntrepaño") : "0"),
                        DarFormato(excel.Campo("CodigoEAN")),
                        excel.Campo("Costo") == "" ? "0" : excel.Campo("Costo"),
                        DarFormato(excel.Campo("ClaveLote")),
                        DarFormato(Formatear_Caducidad(excel)),
                        excel.CampoInt("Cantidad")
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
            */

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            { 
                ////General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar(true, true, true, true, false, false, true);
            } 
            else 
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'INV__Inventario_CargaMasiva'  ");
                General.msjUser("Información de inventario cargada satisfactoriamente."); 
                IniciaToolBar(true, true, true, false, true, false, true);
            }
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
            DataTable dtCarga = excel.Tabla(1);

            dtsCarga.Tables.Add(dtCarga); 

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


            ////sSql = string.Format("Insert Into INV__InventarioInterno_CargaMasiva " +
            ////    " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
            ////    " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad ) \n");
            ////sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'  ",
            ////    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
            ////    Fg.PonCeros(DarFormato(excel.Campo("IdSubFarmacia")), 2),
            ////    DarFormato(bEsModulo_Almacen ? excel.Campo("IdPasillo") : "0"),
            ////    DarFormato(bEsModulo_Almacen ? excel.Campo("IdEstante") : "0"),
            ////    DarFormato(bEsModulo_Almacen ? excel.Campo("IdEntrepaño") : "0"),
            ////    DarFormato(excel.Campo("CodigoEAN")),
            ////    excel.Campo("Costo") == "" ? "0" : excel.Campo("Costo"),
            ////    DarFormato(excel.Campo("ClaveLote")),
            ////    DarFormato(Formatear_Caducidad(excel)),
            ////    excel.CampoInt("Cantidad");

            leerGuardar.Exec("Truncate table INV__InventarioInterno_CargaMasiva ");
            bRegresa = bulk.WriteToServer(dtsCarga); //, System.Data.SqlClient.SqlBulkCopyOptions.Default); 

            if(!bRegresa)
            {
                Error.GrabarError(leerGuardar, "CargarInformacionDeHoja"); 
            }

            return bRegresa;
        }

        private void bulk_RowsCopied(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Compled(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Procesados {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
        }

        private void bulk_Error(RowsCopiedEventArgs e)
        {
            lblProcesados.Text = string.Format("Ocurrio un error al procesar {0} de {1}", e.RowsCopied.ToString(sFormato_INT), excel.Registros.ToString(sFormato_INT));
            Error.GrabarError(e.Error, "bulk_Error");

            General.msjError("Ocurrió un error al cargar la información del inventario.");
            IniciaToolBar(true, true, true, true, false, false, true);
        }

        private void GuardarInformacion_Inventario__Farmacia()
        {
            bool bRegresa = false;
            string sSql = "";
            clsLeer leerGuardar = new clsLeer(ref cnn);

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
            while (excel.Leer())
            {
                sSql = string.Format("Insert Into INV__InventarioInterno_CargaMasiva " +
                    " ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, " +
                    " IdPasillo, IdEstante, IdEntrepaño, CodigoEAN, Costo, ClaveLote, Caducidad, Cantidad ) \n");
                sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}'  ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    Fg.PonCeros(DarFormato(excel.Campo("IdSubFarmacia")), 3),
                    DarFormato("0"), 
                    DarFormato("0"), 
                    DarFormato("0"), 
                    DarFormato(excel.Campo("CodigoEAN")),
                    excel.Campo("Costo"),
                    DarFormato(excel.Campo("ClaveLote")),
                    DarFormato(excel.Campo("Caducidad")),
                    excel.CampoInt("Cantidad")
                    );

                ////BloqueaHojas(true);
                ////MostrarEnProceso(true, 3);

                if (!leerGuardar.Exec(sSql))
                {
                    bRegresa = false;
                    //leerGuardar.Exec("Truncate Table INV__Inventario_CargaMasiva ");
                    Error.GrabarError(leerGuardar, "GuardarInformacion_Inventario__Farmacia");
                    break;
                }
                iRegistrosProcesados++;
                lblProcesados.Text = string.Format("Procesando   {0}   de   {1}", iRegistrosProcesados.ToString(sFormato), iRegistrosHoja.ToString(sFormato));
            }

            BloqueaHojas(false);
            MostrarEnProceso(false, 3);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar la información del inventario.");
                IniciaToolBar(true, true, true, true, false, false, true);
            }
            else
            {
                leerGuardar.Exec("Exec spp_FormatearTabla 'INV__Inventario_CargaMasiva'  ");
                General.msjUser("Información de inventario cargada satisfactoriamente.");
                IniciaToolBar(true, true, true, false, true, false, true);
            }
        }
        #endregion Guardar Informacion

        #region Boton_ProcesarRemisiones
        private void btnProcesarRemisiones_Click(object sender, EventArgs e)
        {
            if (ValidarTipoInventario())
            {
                if (validarIntegracionInventario())
                {
                    thGeneraFolios = new Thread(this.IntegrarInventarioInterno);
                    thGeneraFolios.Name = "Generar Folios Remisiones";
                    thGeneraFolios.Start();
                }
            }
        }

        private bool ValidarTipoInventario()
        {
            bool bRegresa = true;

            if (!rdoCompleto.Checked && !rdoParcial.Checked)
            {
                bRegresa = false;
                General.msjUser("Favor de seleccionar el Tipo de Inventario.");
            }

            return bRegresa;
        }

        private bool validarIntegracionInventario()
        {
            bool bRegresa = false;
            string sMsj = "El proceso de integración de inventario generara una salida general de existencias, y dara ingreso como inventario final al contenido del archivo cargado,\n¿ Desea continuar ? ";

            sMsj = "Este proceso integrará el inventario cargado como un ajuste de inventario,\n\n¿ Desea continuar ?";  
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
            int iTipoInv = 0;
 
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

                    if (rdoParcial.Checked)
                    {
                        iTipoInv = 1;
                    }

                    BloqueaHojas(true);
                    MostrarEnProceso(true, 5);
                    IniciaToolBar(false, false, false, false, false, false, false);

                    sSql = string.Format(" Exec sp_Proceso_IntegrarInventarioInterno \n" + 
                        "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', @Tipo = '{4}', @TipoInv = '{5}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                    DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iTipo, iTipoInv);

                    if (!leer.Exec(sSql))
                    {
                        bContinua = false;
                        //General.msjError("Ocurrió un Error al Procesar las Remisiones");
                        //Error.GrabarError(leer, "ProcesarRemisiones");
                    }
                    else
                    {
                        leer.Leer();
                        sPolizaInventario = leer.Campo("Poliza_Generada");
                    }


                    if (bContinua)
                    {
                        bContinua = AplicarPolizaDeInventario();
                    }

                    sMsjGuardar = "Se desmarcaron correctamente todos los productos de la Unidad.";
                    sMsjError = "Ocurrió un error al desmarcar los productos.";

                    if (bContinua)
                    {
                        if (rdoCompleto.Checked)
                        {
                            bContinua = MarcarProductosParaInventario(2);
                        }
                    }

                    BloqueaHojas(false);
                    MostrarEnProceso(false, 5);

                    if (bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser(sMensaje); //Este mensaje lo genera el SP

                        ImprimirInventario();
                        IniciaToolBar(true, true, true, false, false, false, true);
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, "btnProcesarRemisiones_Click");
                        General.msjError("Ocurrió un error al integrar el inventario.");
                        IniciaToolBar(true, false, false, false, false, true, true);
                    }

                    cnn.Cerrar();
                }
            }
        }


        private bool AplicarPolizaDeInventario()
        {
            bool bRegresa = true;
            string sFolios = "";
            string sSql = "";
            // string sFolioVentaEntrada = "", sFolioConsignacionEntrada = "", sFolioVentaSalida = "", sFolioConsignacionSalida = "";

            sSql = string.Format("Exec spp_Mtto_AjustesDeInventario \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Poliza = '{3}', @IdPersonal = '{4}', @iMostrarResultado = '{5}'  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
                DtGeneral.FarmaciaConectada, sPolizaInventario, DtGeneral.IdPersonal, 1); 

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();
                sFolioVentaEntrada = leer.Campo("FolioVentaEntrada");
                sFolioVentaSalida = leer.Campo("FolioVentaSalida");
                sFolioConsignacionEntrada = leer.Campo("FolioConsignacionEntrada");
                sFolioConsignacionSalida = leer.Campo("FolioConsignacionSalida");

                if (sFolioVentaEntrada != "")
                {
                    sFolios = sFolioVentaEntrada;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaEntrada);
                    }
                }

                if (sFolioVentaSalida != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioVentaSalida : sFolioVentaSalida;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioVentaSalida);
                    }
                }

                if (sFolioConsignacionEntrada != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioConsignacionEntrada : sFolioConsignacionEntrada;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionEntrada);
                    }
                }

                if (sFolioConsignacionSalida != "")
                {
                    sFolios += sFolios != "" ? ", " + sFolioConsignacionSalida : sFolioConsignacionSalida;
                    if (DtGeneral.ConfirmacionConHuellas && bRegresa)
                    {
                        bRegresa = opPermisosEspeciales.GrabarPropietarioDeHuella(sFolioConsignacionSalida);
                    }
                }

                sMensaje = string.Format("La Póliza {0} se aplicó exitosamente con los Folios :\n\n{1}", sPolizaInventario, sFolios);
            }

            return bRegresa;
        }


        private void tmValidacion_Tick(object sender, EventArgs e)
        {
            tmValidacion.Stop();
            tmValidacion.Enabled = false;


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

        #region Impresion de informacion
        private void ImprimirInventario()
        {
            if (sFolioVentaEntrada != "")
            {
                Imprimir(sFolioVentaEntrada);
            }

            if (sFolioVentaSalida != "")
            {
                Imprimir(sFolioVentaSalida);
            }

            if (sFolioConsignacionEntrada != "")
            {
                Imprimir(sFolioConsignacionEntrada);
            }

            if (sFolioConsignacionSalida != "")
            {
                Imprimir(sFolioConsignacionSalida);
            }

            ImprimirDiferenciasAjuste();
        }

        private void Imprimir(string Folio)
        {
            bool bRegresa = false;
          
            DatosCliente.Funcion = "ImprimirInventario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_InventarioInicial.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", Folio);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void ImprimirDiferenciasAjuste()
        {
            bool bRegresa = false; 
            DatosCliente.Funcion = "ImprimirDiferenciasAjuste()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Ajustes_Inv_Diferencias.rpt";

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("Folio", sPolizaInventario);

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            else
            {
                validarExportarDiferencias_Excel(); 
            }
        }

        private void validarExportarDiferencias_Excel()
        {
            bool bExportar = false;

            if (General.msjConfirmar("¿ Desea exportar a excel el archivo de Diferencias de Inventario ?", "Exportar información") == System.Windows.Forms.DialogResult.Yes)
            {
                bExportar = true; 
            }

            if (bExportar)
            {
                ExportarDiferencias_Excel(); 
            }
        }

        private void ExportarDiferencias_Excel()
        {
            bool bRegresa = false;
            string sSql = "";

            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "REPORTE DE DIFERENCIAS DE INVENTARIO";
            string sNombreHoja = "Diferencias";
            string sConcepto = "REPORTE DE DIFERENCIAS DE INVENTARIO";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            sSql = string.Format("" +
                "Select \n" +
                "\tIdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, \n " +
                "\tPoliza, PolizaAplicada, MovtoAplicado, IdPersonal, NombrePersonal, FechaRegistro, IdClaveSSA, ClaveSSA, ClaveSSA_Base, \n " +
                "\tDescripcionClave, SKU, IdProducto, CodigoEAN, DescProducto, IdPresentacion, Presentacion, ContenidoPaquete, ClaveLote, \n  " +
                "\tEsConsignacion, ExistenciaFisica, Costo, Importe, ExistenciaSistema, FechaReg, FechaCad, FechaCad_Aux, \n " +
                "\tExistenciaActualFarmacia, Diferencia, Referencia, StatusDet_Lotes, StatusFarmaciaLote, KeyxDetalleLote, Observaciones \n " +
                "From vw_AjustesInv_Det_Lotes (NoLock) \n " + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Poliza = '{3}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sPolizaInventario);



            this.Cursor = Cursors.Default;
            if(!leer.Exec(sSql))
            {
                this.Cursor = Cursors.WaitCursor;
                Error.GrabarError(leer, "btnExportarExcel_Click()");
                General.msjError("Ocurrió un error al obtener la información de los productos.");
            }
            else
            {
                if(!leer.Leer())
                {
                    General.msjAviso("No existe información para exportar, verifique.");
                }
                else
                {

                    DateTime dtpFecha = General.FechaSistema;
                    int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
                    //int iHoja = 1;
                    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
                    string sEstado = DtGeneral.EstadoConectadoNombre;
                    string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
                    string sFechaImpresion = General.FechaSistemaFecha.ToString();

                    excel = new clsGenerarExcel();
                    excel.RutaArchivo = @"C:\\Excel";
                    excel.NombreArchivo = sNombreDocumento;
                    excel.AgregarMarcaDeTiempo = true;

                    if(excel.PrepararPlantilla(sNombreDocumento))
                    {
                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        excel.CerraArchivo();

                        excel.AbrirDocumentoGenerado(true);
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }
        #endregion Impresion de informacion

        private void tmPantalla_Tick(object sender, EventArgs e)
        {
            tmPantalla.Enabled = false;
            if (!DtGeneral.ValidaTransferenciasTransito())
            {
                this.Close();
            }
        }
        #endregion Boton_ProcesarRemisiones 
    }
}
