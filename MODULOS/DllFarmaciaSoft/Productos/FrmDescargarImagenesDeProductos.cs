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

namespace DllFarmaciaSoft.Productos
{
    public partial class FrmDescargarImagenesDeProductos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerListadoDeRegistros;
        clsAyudas ayuda;
        clsConsultas query;
        clsDatosCliente datosCliente;
        //clsExportarExcelPlantilla xpExcel;

        clsGrid gridClaves;
        clsGrid gridProductos;
        clsRPT_Parametros parametros = new clsRPT_Parametros();

        Thread _workerThread;
        bool bEjecutando = false;
        bool bSeDescargoInformacion = false;

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos_Temporales = "";
        bool bFolderDestino = false;
        string sClaveSSA_Seleccionada = ""; 

        enum ColsClaves
        {
            ClaveSSA = 1, DescripcionClave 
        }

        enum ColsProductos
        {
            Ninguna = 0, 
            ClaveSSA, IdProducto, CodigoEAN, Laboratorio, DescripcionComercial, CveTipoDeInsumo, TipoDeInsumo, Descargar, 
            EsControlado, EsAntibiotico, EsRefrigerado 
        }


        string sListaClavesSSA = "";
        string sListaNombreClavesSSA = "";
        string sListaClavesSSA_Controlados = "";
        string sListaNombreClavesSSA_Controlados = "";
        string sListaClavesSSA_Antibioticos = "";
        string sListaNombreClavesSSA_Antibioticos = "";
        string sListaLaboratorios = "";
        string sListaNombreLaboratorios = "";
        string sListaProductos = "";
        string sListaNombreProductos = "";


        public FrmDescargarImagenesDeProductos()
        {
            InitializeComponent();

            //// 1258, 614
            ////this.Width = 1260;
            ////this.Height = 614; 

            leer = new clsLeer(ref cnn);
            leerListadoDeRegistros = new clsLeer(ref cnn);

            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmDescargarImagenesDeProductos");

            chkMarcarTodo.BackColor = General.BackColorBarraMenu;  ////toolStrip.BackColor; 

            ////gridClaves = new clsGrid(ref grdClaves, this);
            gridProductos = new clsGrid(ref grdCodigosEAN, this);

            ////gridClaves.EstiloDeGrid = eModoGrid.ModoRow;
            gridProductos.EstiloDeGrid = eModoGrid.ModoRow;

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\SII_Descarga__ImagenesDeProductos\");
            lblDirectorioTrabajo.Text = sRutaDestino;

            sRutaDestino_Archivos_Temporales = string.Format("{0}{1}", Application.StartupPath, @"\SII_Descarga__ImagenesDeProductos\");
        }

        private void FrmDescargarImagenesDeProductos_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        #region Botones 
        private void Limpiar()
        {
            sListaClavesSSA = "";
            sListaNombreClavesSSA = "";
            sListaClavesSSA_Controlados = "";
            sListaNombreClavesSSA_Controlados = "";
            sListaClavesSSA_Antibioticos = "";
            sListaNombreClavesSSA_Antibioticos = "";
            sListaLaboratorios = "";
            sListaNombreLaboratorios = "";
            sListaProductos = "";
            sListaNombreProductos = "";

            MostrarEnProceso(false); 
            Fg.IniciaControles();
            lblDirectorioTrabajo.Text = sRutaDestino; 

            ////lblToolTip_ClaveSSA_Seleccionada.Text = ""; 
            ////gridClaves.Limpiar(false);
            gridProductos.Limpiar(false);
            rdoGpo_03_General.Checked = true; 

            btnEjecutar.Enabled = true;
            btnDescargarImagenes.Enabled = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino de la descarga.";
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

            sSql = string.Format("Select ClaveSSA, DescripcionClave " + 
                "From vw_RegistrosSanitarios_CodigoEAN (NoLock) " +
                "Where StatusRegistro <> '-1'  {0} " +
                "Group by ClaveSSA, DescripcionClave " +
                "Order By ClaveSSA ", sWhere);


            sClaveSSA_Seleccionada = ""; 
            ////gridClaves.Limpiar(false);
            gridProductos.Limpiar(false);


            sSql = string.Format(
                "Exec spp_PRCS_Descarga_ListadoDeProductos__Imagenes " +
                " @ListaClaveSSA = '{0}', @ListaDescripcionClaveSSA = '{1}', " + 
                " @ListaClaveSSA_Controlados = '{2}', @ListaDescripcionClaveSSA_Controlados = '{3}', " + 
                " @ListaClaveSSA_Antibioticos = '{4}', @ListaDescripcionClaveSSA_Antibioticos = '{5}', " + 
                " @ListaLaboratorios = '{6}', @ListaDescripcionLaboratorios = '{7}', " + 
                " @ListaProductos = '{8}', @ListaDescripcionProductos = '{9}' ", 
                sListaClavesSSA, sListaNombreClavesSSA, sListaClavesSSA_Controlados, sListaNombreClavesSSA_Controlados,
                sListaClavesSSA_Antibioticos, sListaNombreClavesSSA_Antibioticos, sListaLaboratorios, sListaNombreLaboratorios, 
                sListaProductos, sListaNombreProductos );


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
                    ////btnListarRegistros.Enabled = true; 
                    btnDescargarImagenes.Enabled = false;
                    gridProductos.LlenarGrid(leer.DataSetClase, false, false);

                    btnDescargarImagenes.Enabled = leer.Leer(); 
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //string sMsjDescargarListadoRegistrosSanitarios = "¿ Desea generar el listado general registros sanitarios ?";

            //if (General.msjConfirmar(sMsjDescargarListadoRegistrosSanitarios) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    if (Descargar_ListadoDeRegistrosSanitarios())
            //    {
            //        GenerarExcel_ListadoDeRegistrosSanitarios();
            //    }
            //}
        }

        private void btnDescargarImagenes_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                if (confirmarDescarga())
                {
                    CrearDirectorioDestino();
                    ResetColorGrid();

                    iniciarDescargaDeInformacion(); 
                }
            }
        }
        #endregion Botones 

        #region Reporte Excel
        //private bool Descargar_ListadoDeRegistrosSanitarios()
        //{
        //    bool bRegresa = false;
        //    string sSql = string.Format("Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, " +
        //            "RegistroSanitario, StatusRegistroAux AS Status, FechaVigencia As 'Fecha Vigencia' " +
        //            "From vw_RegistrosSanitarios_CodigoEAN (NoLock) " +
        //            "Where StatusRegistro <> '-1'  " +
        //            "Order By ClaveSSA, Laboratorio, RegistroSanitario");

        //    if (!leerListadoDeRegistros.Exec(sSql))
        //    {
        //        Error.GrabarError(leerListadoDeRegistros, "btnEjecutar_Click");
        //        General.msjError("Ocurió un error al ejecutar la consulta");
        //    }
        //    else
        //    {
        //        if (!leerListadoDeRegistros.Leer())
        //        {
        //            General.msjUser("No se encontro información de registros sanitarios, verifique.");
        //        }
        //        else
        //        {
        //            bRegresa = true;
        //        }
        //    }

        //    return bRegresa; 
        //}

        //private void GenerarExcel_ListadoDeRegistrosSanitarios()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_RegistrosSanitarios.xlsx";
        //    this.Cursor = Cursors.WaitCursor;
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_RegistrosSanitarios.xlsx", datosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {

        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;

        //            int iHoja = 1, iCol = 3;

        //            xpExcel.GeneraExcel(iHoja);

        //            //xpExcel.Agregar(cboEmpresas.Text, 2, 2);
        //            //xpExcel.Agregar(cboEstados.Text, 3, 2);

        //            //xpExcel.Agregar(sEncabezado, 4, 2);

        //            xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 3, 2);

        //            leerListadoDeRegistros.RegistroActual = 1;
        //            for (int iRow = 7; leerListadoDeRegistros.Leer(); iRow++)
        //            {
        //                iCol = 2;
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("TipoDeProducto"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA_Base"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("Laboratorio"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("DescripcionClave"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("Presentacion"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("RegistroSanitario"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("Status"), iRow, iCol++);
        //                xpExcel.Agregar(leerListadoDeRegistros.Campo("Fecha Vigencia"), iRow, iCol++);
        //            }

        //            // Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}
        #endregion Reporte Excel

        #region Grid 
        private void chkMarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            gridProductos.SetValue((int)ColsProductos.Descargar, chkMarcarTodo.Checked); 
        }

        private void grdClaves_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            sClaveSSA_Seleccionada = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.ClaveSSA);
            ////lblDescripcionClaveSeleccionada.Text = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.DescripcionClave);
            ////lblToolTip_ClaveSSA_Seleccionada.Text = "";  //// sClaveSSA_Seleccionada; 
            ////lblDescripcionSal.Text = GridDetalles.GetValue(e.NewRow + 1, (int)ColsDetalles.DescripcionSal);
        }
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

        private bool validarDatos()
        {
            bool bRegresa = true;
            int iRegistros = 0;

            for(int i = 1; i<= gridProductos.Rows; i++)
            {
                if ( gridProductos.GetValueBool(i, (int)ColsProductos.Descargar))
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
            string sMensaje = "El proceso de descarga de Imagenes de Productos puede demorar varios minutos.\n\n¿ Desea iniciar la descarga ? "; 

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
            ////btnListarRegistros.Enabled = bBloquear;
            btnDescargarImagenes.Enabled = bBloquear;
            chkMarcarTodo.Enabled = bBloquear;

            btnClaveSSA.Enabled = bBloquear;
            btnClaveSSA_Controlados.Enabled = bBloquear;
            btnClaveSSA_Antibioticos.Enabled = bBloquear;
            btnLaboratorios.Enabled = bBloquear;
            btnProductos.Enabled = bBloquear; 

            rdoGpo_01_ClaveSSA.Enabled = bBloquear;
            rdoGpo_02_Laboratorio.Enabled = bBloquear;
            rdoGpo_03_General.Enabled = bBloquear;

            ////txtClaveSSA.Enabled = bBloquear;
            ////txtClaveSSA_Base.Enabled = bBloquear;
            ////txtDescripcionClaveSSA.Enabled = bBloquear;

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

        private void ResetColorGrid()
        {
            string sError = ""; 

            try
            {
                for (int i = 1; i <= gridProductos.Rows; i++)
                {
                    gridProductos.ColorRenglon(i, Color.White);
                }
            }
            catch (Exception ex )
            {
                sError = ex.Message;
            }
        }

        private void iniciarDescargaDeInformacion()
        {
            BloquearControles(true);
            bEjecutando = true;

            MostrarEnProceso(true); 

            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            _workerThread = new Thread(this.Descargar_CodigosEAN_Marcados);
            _workerThread.Name = "Descargar_CodigosEAN_Marcados";
            _workerThread.Start();
        }

        private void Descargar_CodigosEAN_Marcados()
        {
            int iDescargas = 0; 
            bSeDescargoInformacion = false;

            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                if (gridProductos.GetValueBool(i, (int)ColsProductos.Descargar))
                {
                    gridProductos.ColorRenglon(i, Color.LightYellow);
                    iDescargas += Descargar_ImagenesDeProductos(i);
                    Thread.Sleep(1000);
                    gridProductos.ColorRenglon(i, Color.LightGreen); 
                }
            }

            bSeDescargoInformacion = iDescargas > 0; 
            bEjecutando = false;
        }

        private int Descargar_ImagenesDeProductos(int Renglon)
        {
            int iDescarga = 0;
            FileInfo fInfo;
            int i = Renglon;
            string sSql = ""; 
            string sConsecutivo = "";  //// gridProductos.GetValue(i, (int)ColsProductos.FolioRegistro);
            string sFileName_Descarga = "";
            string sFechaVigencia = ""; 
            string sNombreDocumento = "";
            string sDocumento_Descarga = "";
            string sDirectorioAgrupador = "";
            string sRutaDescarga = sRutaDestino;

            string sClaveSSA = gridProductos.GetValue(i, (int)ColsProductos.ClaveSSA);
            string sLaboratorio = gridProductos.GetValue(i, (int)ColsProductos.Laboratorio);
            string sIdProducto = Fg.PonCeros(gridProductos.GetValue(i, (int)ColsProductos.IdProducto), 8);
            string sCodigoEAN = gridProductos.GetValue(i, (int)ColsProductos.CodigoEAN);
            string sTipoInsumo = "T" + gridProductos.GetValue(i, (int)ColsProductos.CveTipoDeInsumo);
            string sEsControlado = string.Format("C{0}", gridProductos.GetValueBool(i, (int)ColsProductos.EsControlado) ? "S" : "N");
            string sAntibiotico = string.Format("A{0}", gridProductos.GetValueBool(i, (int)ColsProductos.EsAntibiotico) ? "S" : "N");
            string sRefrigerado = string.Format("R{0}", gridProductos.GetValueBool(i, (int)ColsProductos.EsRefrigerado) ? "S" : "N");


            sDirectorioAgrupador = "";
            if (rdoGpo_01_ClaveSSA.Checked)
            {
                sRutaDescarga += string.Format(@"\{0}\{1}\", sClaveSSA, sLaboratorio);
            }

            if (rdoGpo_02_Laboratorio.Checked)
            {
                sRutaDescarga += string.Format(@"\{0}\{1}\", sLaboratorio, sClaveSSA);
            }

            if (!Directory.Exists(sRutaDescarga))
            {
                Directory.CreateDirectory(sRutaDescarga);
            }

            sSql = string.Format(
                "Select  IdProducto, CodigoEAN, Consecutivo, NombreImagen, Imagen, FechaRegistro, IdPersonal, Status  " + 
                "From CatProductos_Imagenes (NoLock) " + 
                "Where IdProducto = '{0}' and CodigoEAN = '{1}' and Status = 'A' ", sIdProducto, sCodigoEAN);


            sCodigoEAN = Fg.PonCeros(gridProductos.GetValue(i, (int)ColsProductos.CodigoEAN), 20);
            sDocumento_Descarga = Path.Combine(sRutaDestino_Archivos_Temporales, sFileName_Descarga);
            FrameProceso.Text = string.Format("Descargando ... {0} ", gridProductos.GetValue(i, (int)ColsProductos.CodigoEAN));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Descargar_ImagenesDeProductos()");
                gridProductos.ColorRenglon(i, Color.Red);
            }
            else
            {
                try
                {
                    while (leer.Leer())
                    {

                        fInfo = new FileInfo(leer.Campo("NombreImagen"));

                        sConsecutivo = leer.Campo("Consecutivo");
                        sNombreDocumento = string.Format("{0}_{1}_{2}___{3}{4}{5}{6}___{7}___{8}{9}",
                            sIdProducto, sCodigoEAN, sConsecutivo, sEsControlado, sAntibiotico, sRefrigerado, sTipoInsumo, sClaveSSA, sLaboratorio, fInfo.Extension);

                        ////sDocumento_Descarga = sRutaDestino_Archivos_Temporales 
                        sDocumento_Descarga = Path.Combine(sRutaDescarga, sNombreDocumento);

                        try
                        {
                            if (File.Exists(sDocumento_Descarga))
                            {
                                File.Delete(sDocumento_Descarga); 
                            }
                        }
                        catch { }

                        if (!File.Exists(sDocumento_Descarga))
                        {
                            //Fg.ConvertirStringB64EnArchivo(sFileName_Descarga, sRutaDestino_Archivos_Temporales, leer.Campo("Documento"), true);
                            iDescarga++;

                            Fg.ConvertirStringB64EnArchivo(sNombreDocumento, sRutaDescarga, leer.Campo("Imagen"), true);
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }

            return iDescarga; 
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
                    General.msjUser("Imagenes de productos descargadas satisfactoriamente."); 
                    General.AbrirDirectorio(sRutaDestino); 
                }
            }
        }

        #region Parametros 
        private void btnClaveSSA_Click(object sender, EventArgs e)
        {
            parametros.ClavesSSA(ref sListaClavesSSA, ref sListaNombreClavesSSA,
                "Listado de Claves SSA", "ClaveSSA", "Descripcion", "ClaveSSA", "Descripcion", true, "");
        }

        private void btnClaveSSA_Controlados_Click(object sender, EventArgs e)
        {
            parametros.ClavesSSA_Controlados(ref sListaClavesSSA_Controlados, ref sListaNombreClavesSSA_Controlados,
                "Listado de Claves SSA de Controlados", "ClaveSSA", "Descripcion", "ClaveSSA", "Descripcion", true, "");
        }

        private void btnClaveSSA_Antibioticos_Click(object sender, EventArgs e)
        {
            parametros.ClavesSSA_Antibioticos(ref sListaClavesSSA_Antibioticos, ref sListaNombreClavesSSA_Antibioticos,
                "Listado de Claves SSA de Antibióticos", "ClaveSSA", "Descripcion", "ClaveSSA", "Descripcion", true, "");
        }

        private void btnLaboratorios_Click(object sender, EventArgs e)
        {
            parametros.Laboratorios(ref sListaLaboratorios, ref sListaNombreLaboratorios,
                "Listado de Laboratorios", "IdLaboratorio", "Laboratorio", "IdLaboratorio", "Laboratorio", true, "");
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {

        }
        #endregion Parametros
    }
}
