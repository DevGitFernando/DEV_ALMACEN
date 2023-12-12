using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO; 
using System.Windows.Forms;
using System.Diagnostics;

using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Pdf;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllRegistrosSanitarios.Sincronizar
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
            CodigoEAN = 1, IdLaboratorio, Laboratorio, DescripcionComercial, Presentacion, Descargar, ClaveSSA, FolioRegistro, NombreDocumento, FechaVigencia,
            PermitirDescarga
        }

        clsConexionSQLite cnn = new clsConexionSQLite(GnRegistrosSanitarios.DaseDeDatos_SQLite);
        clsLeerSQLite leer;
        clsLeerSQLite leerListadoDeRegistros, leerListadoDeRegistros2;
        DllRegistrosSanitarios.clsConsultasSQLLite query;
        clsDatosCliente datosCliente;
        //clsExportarExcelPlantilla xpExcel;

        clsGrid gridClaves;
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

        PrintDialog print;

        public FrmRegistrosSanitariosReporte()
        {
            InitializeComponent();

            //// 1258, 614
            this.Width = 1258;
            this.Height = 614;

            leer = new clsLeerSQLite(ref cnn);
            leerListadoDeRegistros = new clsLeerSQLite(ref cnn);
            leerListadoDeRegistros2 = new clsLeerSQLite(ref cnn);

            query = new DllRegistrosSanitarios.clsConsultasSQLLite(cnn.DatosConexion, DtGeneral.DatosApp, this.Name);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "RegistrosSanitariosReporte");

            chkMarcarTodo.BackColor = General.BackColorBarraMenu;  ////toolStrip.BackColor; 

            gridClaves = new clsGrid(ref grdClaves, this);
            gridProductos = new clsGrid(ref grdCodigosEAN, this);

            gridClaves.EstiloDeGrid = eModoGrid.ModoRow;
            gridProductos.EstiloDeGrid = eModoGrid.ModoRow;
            gridProductos.AjustarAnchoColumnasAutomatico = true; 

            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\SII_RegistrosSanitarios\");
            lblDirectorioTrabajo.Text = sRutaDestino;

            sRutaDestino_Archivos_Temporales = string.Format("{0}{1}", Application.StartupPath, @"\Descargar_RegistrosSanitarios\");

            dtFechaSistema = DateTime.Now;
            sFechaSistema = General.FechaYMD(dtFechaSistema, "") + General.Hora(dtFechaSistema, "");


            print = new PrintDialog();
            print.AllowSelection = false;
            print.ShowNetwork = true;
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
            gridClaves.Limpiar(false);
            gridProductos.Limpiar(false);

            btnEjecutar.Enabled = true;
            btnListarRegistros.Enabled = false;
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

            if (txtClaveSSA.Text != "")
            {
                sWhere = string.Format(" And ClaveSSA = '{0}' ", txtClaveSSA.Text);
                ///sMsjVacio = string.Format("La Clave SSA {0} no cuenta con información de registros sanitarios.", txtClaveSSA.Text);
            }

            if (txtClaveSSA_Base.Text != "")
            {
                sWhere = string.Format(" And ClaveSSA_Base like '%{0}%' ", txtClaveSSA_Base.Text); 
            }

            if (txtDescripcionClaveSSA.Text != "")
            {
                sWhere = string.Format(" And DescripcionClave like '%{0}%' ", txtDescripcionClaveSSA.Text.Replace(" " , "%"));
            } 

            sSql = string.Format("Select ClaveSSA, DescripcionClave " + 
                "From RegistrosSanitarios_CodigoEAN " +
                "Where StatusRegistro <> '-1'  {0} " +
                "Group by ClaveSSA, DescripcionClave " +
                "Order By ClaveSSA ", sWhere);


            sClaveSSA_Seleccionada = ""; 
            gridClaves.Limpiar(false);
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
                    btnListarRegistros.Enabled = true; 
                    btnDescargarRegistrosSanitarios.Enabled = false;
                    gridClaves.LlenarGrid(leer.DataSetClase, false, false);
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
                    GenerarReporteExcel();
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
                    if (print.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        CrearDirectorioDestino();
                        iniciarDescargaDeInformacion();
                    }
                }
            }
        }
        #endregion Botones 

        #region Informacion de Claves 
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "" && txtClaveSSA.Text.Trim() != "*")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtClaveSSA.Text, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    txtClaveSSA.Enabled = false;
                    ////txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("Descripcion");

                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Clave SSA no encontrada, verifique.");
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");
            //    if (leer.Leer())
            //    {
            //        txtClaveSSA.Enabled = false;
            //        ////txtClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            //        txtClaveSSA.Text = leer.Campo("ClaveSSA");
            //        lblDescripcionClave.Text = leer.Campo("Descripcion");
            //    }
            //}
        }
        #endregion Informacion de Claves

        #region Reporte Excel
        private bool Descargar_ListadoDeRegistrosSanitarios()
        {
            bool bRegresa = false;
            string sSql = "", sSql2 = "";

            sSql =
                "Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion,  " +
                "   RegistroSanitario, Status, Vigente, FechaVigencia As 'Fecha Vigencia' " +
                "From RegistrosSanitarios_CodigoEAN  " +
                "Where StatusRegistro <> '-1'  " +
                "Group by TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, RegistroSanitario, Status, Vigente, [Fecha Vigencia]  " +
                "Order By ClaveSSA, Laboratorio, RegistroSanitario ";
            sSql2 =
                "Select TipoDeProducto, ClaveSSA_Base, ClaveSSA, Laboratorio, DescripcionClave, Presentacion, CodigoEAN, " +
                "   RegistroSanitario, Status,  FechaVigencia As 'Fecha Vigencia' " +
                "From RegistrosSanitarios_CodigoEAN " +
                "Where StatusRegistro <> '-1'  " +
                "Order By ClaveSSA, Laboratorio, RegistroSanitario ";


            //sSql = "Exec spp_RPT_RegistrosSanitarios ";

            if (!leerListadoDeRegistros.Exec(sSql) || !leerListadoDeRegistros2.Exec(sSql2))
            {
                if (leerListadoDeRegistros.SeEncontraronErrores())
                {
                    Error.GrabarError(leerListadoDeRegistros, "btnEjecutar_Click");
                }
                else
                {
                    Error.GrabarError(leerListadoDeRegistros2, "btnEjecutar_Click");
                }
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


        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "CONCENTRADO DE REGISTROS SANITARIOS";
            string sNombreFile = "ListadoDeRegistrosSanitarios";

            leerListadoDeRegistros.RenombrarTabla(1, "Concentrado");
            leerListadoDeRegistros2.RenombrarTabla(1, "Detalles");

            leer.DataSetClase = leerListadoDeRegistros.DataSetClase;


            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Concentrado_Clave";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);


                leer.DataSetClase = leerListadoDeRegistros2.DataSetClase;
                sNombre = "DETALLADO DE REGISTROS SANITARIOS";
                sNombreHoja = "Detallado_EAN";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        /*
        private void GenerarExcel_ListadoDeRegistrosSanitarios()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_RegistrosSanitarios.xlsx";
            this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, false, "Rpt_RegistrosSanitarios.xlsx", datosCliente);
            clsLeerSQLite leerResultado = new clsLeerSQLite(); 

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            {

                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    this.Cursor = Cursors.WaitCursor;
                    int iHoja = 1, iCol = 3;

                    leerListadoDeRegistros.RenombrarTabla(1, "Concentrado");
                    leerListadoDeRegistros2.RenombrarTabla(1, "Detalles");
                    leerResultado.DataSetClase = leerListadoDeRegistros.DataSetClase;


                    leerListadoDeRegistros.DataTableClase = leerResultado.Tabla(1);
                    iHoja = 1;
                    xpExcel.GeneraExcel(iHoja, true);
                    xpExcel.NumeroDeRenglonesAProcesar = leerListadoDeRegistros.Registros; 

                    xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 3, 2);
                    leerListadoDeRegistros.RegistroActual = 1;
                    for (int iRow = 7; leerListadoDeRegistros.Leer(); iRow++)
                    {
                        iCol = 2;
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("TipoDeProducto"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA_Base"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Laboratorio"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("DescripcionClave"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Presentacion"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("RegistroSanitario"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Status"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Vigente"), iRow, iCol++); 
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Fecha Vigencia"), iRow, iCol++);
                        xpExcel.NumeroRenglonesProcesados++;
                    }

                    //// Finalizar el Proceso 
                    xpExcel.CerrarDocumento();



                    leerListadoDeRegistros.DataTableClase = leerListadoDeRegistros2.Tabla(1); 
                    iHoja = 2;
                    xpExcel.GeneraExcel(iHoja, true);
                    xpExcel.NumeroRenglonesProcesados = 0;
                    xpExcel.NumeroDeRenglonesAProcesar = leerListadoDeRegistros.Registros; 

                    xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 3, 2);
                    leerListadoDeRegistros.RegistroActual = 1;
                    for (int iRow = 7; leerListadoDeRegistros.Leer(); iRow++)
                    {
                        iCol = 2;
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("TipoDeProducto"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA_Base"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Laboratorio"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("DescripcionClave"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Presentacion"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("CodigoEAN"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("RegistroSanitario"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Status"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Vigente"), iRow, iCol++);
                        xpExcel.Agregar(leerListadoDeRegistros.Campo("Fecha Vigencia"), iRow, iCol++);

                        xpExcel.NumeroRenglonesProcesados++;
                    }

                    //// Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }

            this.Cursor = Cursors.Default;
        }
        */
        #endregion Reporte Excel

        #region Grid 
        private void chkMarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            bool bValor = false; 

            for (int i = 1; i <= gridProductos.Rows; i++)
            {
                bValor = gridProductos.GetValueBool(i, (int)ColsProductos.PermitirDescarga) ? chkMarcarTodo.Checked : false;
                gridProductos.SetValue(i, (int)ColsProductos.Descargar, bValor);
            }
            //gridProductos.SetValue((int)ColsProductos.Descargar, chkMarcarTodo.Checked); 
        }

        private void grdClaves_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            sClaveSSA_Seleccionada = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.ClaveSSA);
            lblDescripcionClaveSeleccionada.Text = gridClaves.GetValue(e.NewRow + 1, (int)ColsClaves.DescripcionClave);
            lblToolTip_ClaveSSA_Seleccionada.Text = "";  //// sClaveSSA_Seleccionada; 
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

        private bool RegistrosSanitarios_ClaveSSA()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select CodigoEAN, IdLaboratorio, Laboratorio, Descripcion, Presentacion, 0 as Descargar, ClaveSSA, Folio as FolioDeRegistro, " +
                " NombreDocto, replace(FechaVigencia, '-', '') as FechaVigencia, (case when Vigente = 'SI' Then 1 else 0 end) as Descargable " + 
                "From RegistrosSanitarios_CodigoEAN " +
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
                colorAplicar = gridProductos.GetValueBool(i, (int)ColsProductos.PermitirDescarga) ? colorDescargable : colorNoDescargable;
                gridProductos.BloqueaCelda(!gridProductos.GetValueBool(i, (int)ColsProductos.PermitirDescarga), i, (int)ColsProductos.Descargar); 
                gridProductos.ColorRenglon(i, colorAplicar);
            }
        }

        private bool validarDatos()
        {
            bool bRegresa = true;
            int iRegistros = 0;

            for(int i = 1; i<= gridProductos.Rows; i++)
            {
                if ( gridProductos.GetValueBool(i, (int)ColsProductos.Descargar) && gridProductos.GetValueBool(i, (int)ColsProductos.PermitirDescarga))
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
            string sMensaje = "El proceso de descarga de Registros Sanitarios puede demorar varios minutos.\n¿ Desea iniciar la descarga ? "; 

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
            btnListarRegistros.Enabled = bBloquear;
            btnDescargarRegistrosSanitarios.Enabled = bBloquear;
            chkMarcarTodo.Enabled = bBloquear; 

            txtClaveSSA.Enabled = bBloquear;
            txtClaveSSA_Base.Enabled = bBloquear;
            txtDescripcionClaveSSA.Enabled = bBloquear;

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
                if (gridProductos.GetValueBool(i, (int)ColsProductos.Descargar) && gridProductos.GetValueBool(i, (int)ColsProductos.PermitirDescarga))
                {
                    Thread.Sleep(1000); 
                    iDescargas += Descargar_RegistroSanitario(i);
                }
            }

            bSeDescargoInformacion = iDescargas > 0; 
            bEjecutando = false;

            ImprimirPdfs();
        }

        private int Descargar_RegistroSanitario(int Renglon)
        {
            int iDescarga = 0;
            int i = Renglon; 
            string sFolio = gridProductos.GetValue(i, (int)ColsProductos.FolioRegistro);
            string sFileName_Origen = "";
            string sFileName_Descarga = "";
            string sFechaVigencia = "";
            string sLaboratorio = "";
            string sNombreDocumento = "";
            string sDocumento_Descarga = "";
            string sNombreMD5 = "";
            string sFirma = "";

            string sSql = string.Format("Select Folio, NombreDocto, MD5 From RegistrosSanitarios_CodigoEAN Where Folio = '{0}' ", sFolio);

            //sFechaVigencia = gridProductos.GetValue(i, (int)ColsProductos.FechaVigencia);
            //sLaboratorio = gridProductos.GetValue(i, (int)ColsProductos.Laboratorio);
            //sNombreDocumento = gridProductos.GetValue(i, (int)ColsProductos.NombreDocumento);
            
            //sFirma = gridProductos.GetValue(i, (int)ColsProductos.ClaveSSA) + gridProductos.GetValue(i, (int)ColsProductos.IdLaboratorio) + sFechaSistema;
            sFirma = gridProductos.GetValue(i, (int)ColsProductos.FolioRegistro); 


            //sKey = crypto.Encrypt(txtClaveSSA.Text + txtIdLaboratorio.Text + sFechaSistema, true);

            //sFileName_Descarga = string.Format("{0}__{1}__{2}______{3}", sClaveSSA_Seleccionada, sFechaVigencia, sLaboratorio, sNombreDocumento);
            //sDocumento_Descarga = Path.Combine(sRutaDestino_Archivos_Temporales, sFileName_Descarga);

            //FrameProceso.Text = string.Format("Descargando ... {0} ", gridProductos.GetValue(i, (int)ColsProductos.CodigoEAN));

            //if (File.Exists(sDocumento_Descarga))
            //{
            //    iDescarga++; 
            //}
            //else 
            //{
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Descargar_RegistroSanitario()");
                    gridProductos.ColorRenglon(i, Color.Red);
                }
                else
                {
                    try
                    {

                        if (leer.Leer())
                        {
                            //if (!File.Exists(sDocumento_Descarga))
                            //{
                            //    Fg.ConvertirStringB64EnArchivo(sFileName_Descarga, sRutaDestino_Archivos_Temporales, leer.Campo("Documento"), true);
                            //    Fg.ConvertirStringB64EnArchivo(sFileName_Descarga, sRutaDestino, leer.Campo("Documento"), true);
                            //    iDescarga++;

                            //    AgregarCadenaDeSeguridad(Path.Combine(sRutaDestino_Archivos_Temporales, sFileName_Descarga), sFirma);
                            //    AgregarCadenaDeSeguridad(Path.Combine(sRutaDestino, sFileName_Descarga), sFirma); 

                            //}

                            sNombreMD5 = leer.Campo("MD5");

                            sFechaVigencia = gridProductos.GetValue(i, (int)ColsProductos.FechaVigencia);
                            sLaboratorio = gridProductos.GetValue(i, (int)ColsProductos.Laboratorio);
                            sNombreDocumento = gridProductos.GetValue(i, (int)ColsProductos.NombreDocumento);

                            sFileName_Descarga = string.Format("{0}__{1}__{2}___{3}", sClaveSSA_Seleccionada, sFechaVigencia, sLaboratorio, sNombreDocumento);
                            sDocumento_Descarga = Path.Combine(sRutaDestino, sFileName_Descarga);

                            sFileName_Origen = Path.Combine(GnRegistrosSanitarios.Ruta_DB_RegistrosSanitarios, sNombreMD5 + ".SRS");
                                                       

                            System.IO.File.Copy(sFileName_Origen, sDocumento_Descarga, true);

                            AgregarCadenaDeSeguridad(sDocumento_Descarga, sFirma); 

                            //Print(sDocumento_Descarga, "dhcppc1 (HP Deskjet 4640 series)");
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }

            return iDescarga; 
        }


        public void Print(string fullPath, string printerName)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = { FileName = fullPath, UseShellExecute = true, Verb = "printto", Arguments = "\"" + printerName + "\"", }
                };

                process.Start();
            }
            catch (Exception Ex)
            {
            }
        }

        private void ImprimirPdfs()
        {
            string[] sFiles = Directory.GetFiles(sRutaDestino, "*.pdf");
            int imageFrameCount = 0;

            foreach (string sFile in sFiles)
            {
                imageFrameCount = ImageUtil.GetImageFrameCount(sFile, "");

                print.PrinterSettings.FromPage = 1;
                print.PrinterSettings.ToPage = imageFrameCount;
                print.PrinterSettings.MaximumPage = imageFrameCount;
                print.PrinterSettings.MinimumPage = 1;
                //print.PrinterSettings.PaperSizes = System.Drawing.Printing.PrinterSettings.PaperSizeCollection;
                //print.ShowDialog();


                PrinterUtil.PrintImageToPrinter(sFile, print.PrinterSettings);
            }

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
    }
}
