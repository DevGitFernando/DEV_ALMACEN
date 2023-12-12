using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.ReportesQFB
{
    public partial class FrmRegistrosSanitariosReporte : FrmBaseExt
    {
        enum ColsClaves
        {
            ClaveSSA = 1, DescripcionClave
        }

        enum ColsProductos
        {
            //CodigoEAN = 1, Laboratorio = 2, DescripcionComercial = 3, Presentacion = 4, Descargar = 5 
            //Tipo = 1, IdLaboratorio, Laboratorio, DescripcionComercial, Presentacion, Descargar, ClaveSSA, FolioRegistro, NombreDocumento, FechaVigencia,
            //PermitirDescarga

            Tipo = 1, ClaveSSA, IdLaboratorio, Laboratorio, Descripcion, Registro, MD5, NombreArchivo, Vigencia, Descargable, Descargar
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerListadoDeRegistros;
        clsAyudas ayuda;
        clsConsultas query;
        clsDatosCliente datosCliente;
        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();

        //clsGrid gridClaves;
        clsGrid gridProductos;

        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeDescargoInformacion = false;

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos_Temporales = "";
        bool bFolderDestino = false;
        string sClaveSSA_Seleccionada = "";

        clsCriptografo crypto = new clsCriptografo();
        DateTime dtFechaSistema = DateTime.Now;
        string sFechaSistema = "";
        string sKey = "";

        Color colorDescargable = Color.White;
        Color colorNoDescargable = Color.Red;
        Color colorAplicar = Color.White;

        public FrmRegistrosSanitariosReporte()
        {
            InitializeComponent();

            //// 1258, 614
            this.Width = 1258;
            this.Height = 614;

            leer = new clsLeer(ref cnn);
            leerListadoDeRegistros = new clsLeer(ref cnn);

            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "RegistrosSanitariosReporte");

            chkMarcarTodo.BackColor = General.BackColorBarraMenu;  ////toolStrip.BackColor; 

            //gridClaves = new clsGrid(ref grdClaves, this);
            gridProductos = new clsGrid(ref grdCodigosEAN, this);

            //gridClaves.EstiloDeGrid = eModoGrid.ModoRow;
            gridProductos.EstiloDeGrid = eModoGrid.ModoRow;
            gridProductos.AjustarAnchoColumnasAutomatico = true;

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\SII_RegistrosSanitarios\");
            lblDirectorioTrabajo.Text = sRutaDestino;

            sRutaDestino_Archivos_Temporales = string.Format("{0}{1}", Application.StartupPath, @"\Descargar_RegistrosSanitarios\");

            dtFechaSistema = General.FechaSistema;
            sFechaSistema = General.FechaYMD(dtFechaSistema, "") + General.Hora(dtFechaSistema, "");
        }

        private void FrmRgistrosSanitariosReporte_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Botones 
        private void Limpiar()
        {
            MostrarEnProceso(false);
            Fg.IniciaControles();
            lblDirectorioTrabajo.Text = sRutaDestino;

            lblToolTip_ClaveSSA_Seleccionada.Text = "";
            //gridClaves.Limpiar(false);
            gridProductos.Limpiar(false);

            btnEjecutar.Enabled = true;
            //btnListarRegistros.Enabled = false;
            btnDescargarRegistrosSanitarios.Enabled = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
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
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            string sWhere = "";
            string sMsjVacio = "No se encontro información de Registros Sanitarios con los criterios especificados, verifique.";

            //if (txtClaveSSA.Text != "")
            //{
            //    sWhere = string.Format(" And ClaveSSA = '{0}' ", txtClaveSSA.Text);
            //    ///sMsjVacio = string.Format("La Clave SSA {0} no cuenta con información de registros sanitarios.", txtClaveSSA.Text);
            //}

            //if (txtLaboratorio.Text != "")
            //{
            //    sWhere = string.Format(" And ClaveSSA_Base like '%{0}%' ", txtLaboratorio.Text); 
            //}

            //if (txtTipo.Text != "")
            //{
            //    sWhere = string.Format(" And DescripcionClave like '%{0}%' ", txtTipo.Text.Replace(" " , "%"));
            //} 

            //sSql = string.Format("Select ClaveSSA, DescripcionClave " + 
            //    "From vw_RegistrosSanitarios_CodigoEAN (NoLock) " +
            //    "Where StatusRegistro <> '-1'  {0} " +
            //    "Group by ClaveSSA, DescripcionClave " +
            //    "Order By ClaveSSA ", sWhere);

            sSql = string.Format("Exec spp_RPT_RegistrosSanitarios_Pantalla @Tipo = '{0}', @ClaveSSA = '{1}', @Laboratorio = '{2}', @Registro = '{3}'",
                    txtTipo.Text.Trim(), txtClaveSSA.Text.Trim(), txtLaboratorio.Text.Trim(), txtRegistro.Text.Trim());


            sClaveSSA_Seleccionada = "";
            //gridClaves.Limpiar(false);
            gridProductos.Limpiar(false);


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurió un error al obtener el listado de Claves SSA");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser(sMsjVacio);
                }
                else
                {
                    //btnListarRegistros.Enabled = true; 
                    btnDescargarRegistrosSanitarios.Enabled = true;
                    gridProductos.LlenarGrid(leer.DataSetClase, false, false);
                    ValidarDescarga();
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string sMsjDescargarListadoRegistrosSanitarios = "¿ Desea generar el listado general registros sanitarios ?";

            if (General.msjConfirmar(sMsjDescargarListadoRegistrosSanitarios) == System.Windows.Forms.DialogResult.Yes)
            {
                if (Descargar_ListadoDeRegistrosSanitarios())
                {
                    GenerarExcel_ListadoDeRegistrosSanitarios();
                }
            }
        }

        private void btnListarRegistros_Click(object sender, EventArgs e)
        {
            RegistrosSanitarios_ClaveSSA();
        }

        private void btnDescargarRegistrosSanitarios_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (confirmarDescarga())
                {
                    CrearDirectorioDestino();
                    iniciarDescargaDeInformacion();
                }
            }
        }
        #endregion Botones 

        #region Informacion de Claves 
        //private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtClaveSSA.Text.Trim() != "" && txtClaveSSA.Text.Trim() != "*")
        //    {
        //        leer.DataSetClase = query.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");
        //        if (leer.Leer())
        //        {
        //            txtClaveSSA.Enabled = false;
        //            ////txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
        //            txtClaveSSA.Text = leer.Campo("ClaveSSA");
        //            lblDescripcionClave.Text = leer.Campo("Descripcion");

        //        }
        //        else
        //        {
        //            e.Cancel = true;
        //            General.msjUser("Clave SSA no encontrada, verifique.");
        //        }
        //    }
        //}

        //private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F1)
        //    {
        //        leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");
        //        if (leer.Leer())
        //        {
        //            txtClaveSSA.Enabled = false;
        //            ////txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
        //            txtClaveSSA.Text = leer.Campo("ClaveSSA");
        //            lblDescripcionClave.Text = leer.Campo("Descripcion");
        //        }
        //    }
        //}
        #endregion Informacion de Claves

        #region Reporte Excel
        private bool Descargar_ListadoDeRegistrosSanitarios()
        {
            bool bRegresa = false;
            string sSql = "";

            ////sSql = string.Format(
            ////    "Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, " +
            ////    "RegistroSanitario, StatusRegistroAux AS Status, FechaVigencia As 'Fecha Vigencia' " + 
            ////    "Into #tmpRegistrosSanitarios " + 
            ////    "From vw_RegistrosSanitarios_CodigoEAN (NoLock) " +
            ////    "Where StatusRegistro <> '-1'  " +
            ////    "Order By ClaveSSA, Laboratorio, RegistroSanitario \n\n\n\n" + 
            ////    " " +
            ////    " " +
            ////    "Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, " +
            ////    "RegistroSanitario, Status, [Fecha Vigencia] " +
            ////    "From #tmpRegistrosSanitarios " +
            ////    "Group by TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, RegistroSanitario, Status, [Fecha Vigencia] " + 
            ////    "Order By ClaveSSA, Laboratorio, RegistroSanitario \n\n\n\n" +
            ////    " " + 
            ////    " " +
            ////    "Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, " +
            ////    "RegistroSanitario, Status, [Fecha Vigencia] " +
            ////    "From #tmpRegistrosSanitarios " +
            ////    "Order By ClaveSSA, Laboratorio, RegistroSanitario \n\n\n\n" 
            ////    );


            sSql = "Exec spp_RPT_RegistrosSanitarios ";

            if (!leerListadoDeRegistros.Exec(sSql))
            {
                Error.GrabarError(leerListadoDeRegistros, "btnEjecutar_Click");
                General.msjError("Ocurió un error al ejecutar la consulta");
            }
            else
            {
                if (!leerListadoDeRegistros.Leer())
                {
                    General.msjUser("No se encontro información de registros sanitarios, verifique.");
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private void GenerarExcel_ListadoDeRegistrosSanitarios()
        {
            bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Vales_Emitidos_Mes.xls", DatosCliente); 
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            string sPeriodo = "";

            string sNombreDocumento = string.Format("ReporteDeRegistrosSanitarios");
            string sNombreHoja = "Emitidos";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            clsLeer datosExportar = new clsLeer();
            DataSet dtDatos = new DataSet();

            clsLeer leerResultado = new clsLeer();


            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                //General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                //Error.GrabarError(leerEmitidos, "GenerarExcelEmitidos");
            }
            else
            {
                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;


                this.Cursor = Cursors.Default;

                ////if (leerEmitidos.Leer())
                {
                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        this.Cursor = Cursors.WaitCursor;

                        leerListadoDeRegistros.RenombrarTabla(1, "Concentrado");
                        leerListadoDeRegistros.RenombrarTabla(2, "Detalles");
                        leerResultado.DataSetClase = leerListadoDeRegistros.DataSetClase;


                        //leerListadoDeRegistros.DataTableClase = leerResultado.Tabla(1);



                        //////// Concentrado  
                        sNombreHoja = "CONCENTRADO";
                        datosExportar.DataTableClase = leerListadoDeRegistros.Tabla(1);
                        sConcepto = string.Format("CONCENTRADO DE REGISTROS SANITARIOS");

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Concentrado  


                        //////// Detalles 
                        sNombreHoja = "DETALLADO";
                        datosExportar.DataTableClase = leerListadoDeRegistros.Tabla(2);
                        sConcepto = string.Format("DETALLADO DE REGISTROS SANITARIOS");

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Detalles 


                        excel.CerraArchivo();

                        this.Cursor = Cursors.Default;

                        excel.AbrirDocumentoGenerado(true);
                    }
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void GenerarExcel_ListadoDeRegistrosSanitarios__OLD()
        {
        }
        #endregion Reporte Excel

        #region Grid 
        private void chkMarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            bool bValor = false;

            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                bValor = gridProductos.GetValueBool(i, (int)ColsProductos.Descargable) ? chkMarcarTodo.Checked : false;
                gridProductos.SetValue(i, (int)ColsProductos.Descargar, bValor);
            }
            //gridProductos.SetValue((int)ColsProductos.Descargar, chkMarcarTodo.Checked); 
        }

        //private void grdClaves_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        //{
        //    sClaveSSA_Seleccionada = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.ClaveSSA);
        //    lblDescripcionClaveSeleccionada.Text = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.DescripcionClave);
        //    lblToolTip_ClaveSSA_Seleccionada.Text = "";  //// sClaveSSA_Seleccionada; 
        //    ////lblDescripcionSal.Text = GridDetalles.GetValue(e.NewRow + 1, (int)ColsDetalles.DescripcionSal);
        //}
        #endregion Grid

        #region Funciones y Procedimientos Privados
        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameDirectorioDeTrabajo.Width = 856;
                ////FrameProceso.Left = 220;
            }
            else
            {
                FrameDirectorioDeTrabajo.Width = 1233;
                ////FrameProceso.Left = this.Width + 100;
            }

            FrameDirectorioDeTrabajo.BringToFront();
        }

        private bool RegistrosSanitarios_ClaveSSA()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select CodigoEAN, IdLaboratorio, Laboratorio, Descripcion, Presentacion, 0 as Descargar, ClaveSSA, Folio as FolioDeRegistro, " +
                " NombreDocto, replace(FechaVigencia, '-', '') as FechaVigencia, (case when Vigente = 'SI' Then 1 else 0 end) as Descargable " +
                "From vw_RegistrosSanitarios_CodigoEAN (NoLock) " +
                "Where StatusRegistro <> '-1' and ClaveSSA = '{0}' " +
                "Order By Laboratorio, Descripcion, CodigoEAN ", sClaveSSA_Seleccionada);

            lblToolTip_ClaveSSA_Seleccionada.Text = sClaveSSA_Seleccionada;
            btnDescargarRegistrosSanitarios.Enabled = false;
            if (!leerListadoDeRegistros.Exec(sSql))
            {
                Error.GrabarError(leerListadoDeRegistros, "btnEjecutar_Click");
                General.msjError("Ocurió un error al ejecutar la consulta");
            }
            else
            {
                if (!leerListadoDeRegistros.Leer())
                {
                    General.msjUser(string.Format("No se encontro información de registros sanitarios de la Clave SSA {0}.", sClaveSSA_Seleccionada));
                }
                else
                {
                    bRegresa = true;
                    gridProductos.LlenarGrid(leerListadoDeRegistros.DataSetClase, false, false);
                }
            }

            ValidarDescarga();
            btnDescargarRegistrosSanitarios.Enabled = bRegresa;

            return bRegresa;
        }

        private void ValidarDescarga()
        {
            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                colorAplicar = gridProductos.GetValueBool(i, (int)ColsProductos.Descargable) ? colorDescargable : colorNoDescargable;
                gridProductos.BloqueaCelda(!gridProductos.GetValueBool(i, (int)ColsProductos.Descargable), i, (int)ColsProductos.Descargar);
                gridProductos.ColorRenglon(i, colorAplicar);
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;
            int iRegistros = 0;

            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                if (gridProductos.GetValueBool(i, (int)ColsProductos.Descargar) && gridProductos.GetValueBool(i, (int)ColsProductos.Descargable))
                {
                    iRegistros++;
                }
            }

            bRegresa = iRegistros > 0;
            if (!bRegresa)
            {
                General.msjUser("No se han marcado registros para la descarga, verifique.");
            }

            return bRegresa;
        }

        private bool confirmarDescarga()
        {
            bool bRegresa = false;
            string sMensaje = "El proceso de descarga de Registros Sanitarios puede demorar varios minutos.\n\n¿ Desea iniciar la descarga ? ";

            bRegresa = General.msjConfirmar(sMensaje) == System.Windows.Forms.DialogResult.Yes;

            return bRegresa;
        }

        private void BloquearControles(bool Bloquear)
        {
            bool bBloquear = !Bloquear;

            btnNuevo.Enabled = bBloquear;
            btnEjecutar.Enabled = bBloquear;
            btnExportar.Enabled = bBloquear;
            btnDirectorio.Enabled = bBloquear;
            //btnListarRegistros.Enabled = bBloquear;
            btnDescargarRegistrosSanitarios.Enabled = bBloquear;
            chkMarcarTodo.Enabled = bBloquear;

            txtClaveSSA.Enabled = bBloquear;
            txtLaboratorio.Enabled = bBloquear;
            txtTipo.Enabled = bBloquear;

            gridProductos.BloqueaColumna(Bloquear, (int)ColsProductos.Descargar);
        }

        private void CrearDirectorioDestino()
        {
            if (!Directory.Exists(sRutaDestino))
            {
                Directory.CreateDirectory(sRutaDestino);
            }

            if (!Directory.Exists(sRutaDestino_Archivos_Temporales))
            {
                Directory.CreateDirectory(sRutaDestino_Archivos_Temporales);
            }

            Limpiar_DirectorioDeDescarga();
        }

        private void Limpiar_DirectorioDeDescarga()
        {
            Limpiar_DirectorioDeDescarga_Detalle(sRutaDestino_Archivos_Temporales);
        }

        private void Limpiar_DirectorioDeDescarga_Detalle(string Directorio)
        {
            try
            {
                string[] sListaDirectorios = Directory.GetDirectories(Directorio);
                string[] sListaArchivos = Directory.GetFiles(Directorio, "*.*");


                foreach (string sDirectorio in sListaDirectorios)
                {
                    Limpiar_DirectorioDeDescarga_Detalle(sDirectorio);
                }

                foreach (string sFile in sListaArchivos)
                {
                    try
                    {
                        File.Delete(sFile);
                    }
                    catch (Exception ex1)
                    {
                    }
                }

                try
                {
                    if (Directorio.ToUpper() != sRutaDestino_Archivos_Temporales.ToUpper())
                    {
                        Directory.Delete(Directorio);
                    }
                }
                catch (Exception ex2)
                {
                }
            }
            catch (Exception ex3)
            {
            }
        }

        private void iniciarDescargaDeInformacion()
        {
            BloquearControles(true);
            bEjecutando = true;

            MostrarEnProceso(true);

            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            _workerThread = new Thread(this.Descargar_RegistrosSanitarios_Marcados);
            _workerThread.Name = "Descargando__RegistrosSanitarios";
            _workerThread.Start();
        }

        private void Descargar_RegistrosSanitarios_Marcados()
        {
            int iDescargas = 0;
            bSeDescargoInformacion = false;

            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                if (gridProductos.GetValueBool(i, (int)ColsProductos.Descargar) && gridProductos.GetValueBool(i, (int)ColsProductos.Descargable))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    iDescargas += Descargar_RegistroSanitario(i);
                }
            }

            bSeDescargoInformacion = iDescargas > 0;
            bEjecutando = false;
        }

        private int Descargar_RegistroSanitario(int Renglon)
        {
            int iDescarga = 0;
            int i = Renglon;
            string sMD5 = gridProductos.GetValue(i, (int)ColsProductos.MD5);
            string sFileName_Descarga = "";
            string sRegistro = "";
            string sLaboratorio = "";
            string sNombreDocumento = "";
            string sDocumento_Descarga = "";
            string sFirma = "";

            string sSql = string.Format("Select * From SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios (NoLock) Where MD5 = '{0}' ", sMD5);

            sClaveSSA_Seleccionada = gridProductos.GetValue(i, (int)ColsProductos.ClaveSSA).ToString();
            sRegistro = gridProductos.GetValue(i, (int)ColsProductos.Registro).ToString().Replace("-", "");
            sLaboratorio = gridProductos.GetValue(i, (int)ColsProductos.Laboratorio);
            sNombreDocumento = gridProductos.GetValue(i, (int)ColsProductos.NombreArchivo);

            //sFirma = gridProductos.GetValue(i, (int)ColsProductos.ClaveSSA) + gridProductos.GetValue(i, (int)ColsProductos.IdLaboratorio) + sFechaSistema;
            sFirma = gridProductos.GetValue(i, (int)ColsProductos.Registro);


            //sKey = crypto.Encrypt(txtClaveSSA.Text + txtIdLaboratorio.Text + sFechaSistema, true);

            sFileName_Descarga = string.Format("{0}__{1}__{2}______{3}", sClaveSSA_Seleccionada, sRegistro, sLaboratorio, sNombreDocumento);
            
            sFileName_Descarga = FormatoCampos.Formatear_Nombre(sFileName_Descarga); 
            sDocumento_Descarga = Path.Combine(sRutaDestino_Archivos_Temporales, sFileName_Descarga);

            FrameProceso.Text = string.Format("Descargando ... {0} ", gridProductos.GetValue(i, (int)ColsProductos.Registro));

            if (File.Exists(sDocumento_Descarga))
            {
                iDescarga++;
            }
            else
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Descargar_RegistroSanitario()");
                    gridProductos.ColorRenglon(i, Color.Red);
                }
                else
                {
                    try
                    {
                        //////sFechaVigencia = gridProductos.GetValue(i, (int)ColsProductos.FechaVigencia);
                        //////sLaboratorio = gridProductos.GetValue(i, (int)ColsProductos.Laboratorio);
                        //////sNombreDocumento = gridProductos.GetValue(i, (int)ColsProductos.NombreDocumento);

                        //////sFileName_Descarga = string.Format("{0}__{1}__{2}___{3}", sClaveSSA_Seleccionada, sFechaVigencia, sLaboratorio, sNombreDocumento);
                        //////sDocumento_Descarga = Path.Combine(sRutaDestino, sFileName_Descarga);
                        if (leer.Leer())
                        {
                            if (!File.Exists(sDocumento_Descarga))
                            {
                                Fg.ConvertirStringB64EnArchivo(sFileName_Descarga, sRutaDestino_Archivos_Temporales, leer.Campo("Documento"), true);
                                Fg.ConvertirStringB64EnArchivo(sFileName_Descarga, sRutaDestino, leer.Campo("Documento"), true);
                                iDescarga++;

                                AgregarCadenaDeSeguridad(Path.Combine(sRutaDestino_Archivos_Temporales, sFileName_Descarga), sFirma);
                                AgregarCadenaDeSeguridad(Path.Combine(sRutaDestino, sFileName_Descarga), sFirma); 

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return iDescarga;
        }

        private void AgregarCadenaDeSeguridad(string File_In, string Firma)
        {
            string sMarcaDeAgua = "";

            //sKey = crypto.Encrypt(txtClaveSSA.Text + txtIdLaboratorio.Text + sFechaSistema, true);
            sKey = crypto.Encrypt(Firma, true);
            sMarcaDeAgua = "ASUNTO REGULATORIO" + " " + sKey;
            sMarcaDeAgua = "ARC";

            DllFarmaciaSoft.PDF.iTextSharpUtil.AddTextWatermark(File_In, sMarcaDeAgua);
        }
        #endregion Funciones y Procedimientos Privados 

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                MostrarEnProceso(false);
                BloquearControles(false);
                Thread.Sleep(1200);
                this.Refresh();
                Application.DoEvents();

                if (bSeDescargoInformacion)
                {
                    Limpiar_DirectorioDeDescarga();
                    General.msjUser("Registros Sanitarios descargados satisfactoriamente.");
                    General.AbrirDirectorio(sRutaDestino);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
