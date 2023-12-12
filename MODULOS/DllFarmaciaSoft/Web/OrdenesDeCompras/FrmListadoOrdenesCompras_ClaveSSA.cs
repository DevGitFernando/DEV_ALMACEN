using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Web.OrdenesDeCompras
{
    public partial class FrmListadoOrdenesCompras_ClaveSSA : FrmBaseExt
    {
        enum Cols
        {
            IdEmpresa = 1, IdProveedor = 2, NombreProveedor = 3, FechaOC = 4, FolioCompra = 5, FolioRecepcion = 6, 
            FechaDocumento = 7, FechaRegistro = 8, Personal = 9, Referencia = 10, Costo = 11, Piezas = 12, Total = 13 
        }

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionClienteUnidad conecionCte;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeer leerDetalles;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid Grid;
        int iTimeOut = 250000;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        string sFormato = "#,###,##0.###0";
        int iValor_0 = 0;

        string sUrl_Regional = ""; 
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        // Thread _workerThread;

        // bool bEjecutando = false;
        // bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();

        public FrmListadoOrdenesCompras_ClaveSSA()
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
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            
            Grid = new clsGrid(ref grdCompras, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;
            Grid.SetOrder(true); 

        }

        private void FrmListadoComprasFarmacia_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false; 

            lblTotal.Text = iValor_0.ToString(sFormato); 
            Grid.Limpiar(false);            

            CargarEstados();
            
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;            

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargaDetallesCompras();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel(); 
        }

        private void ExportarExcel()
        {
            bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Vales_Emitidos_Mes.xls", DatosCliente); 
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            string sNombreDocumento = string.Format("Listado de Ordenes de Compras Recepcionadas Claves");
            string sNombreHoja = "Emitidos";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            clsLeer datosExportar = new clsLeer();
            DataSet dtDatos = new DataSet();

            if (DatosDetalle())
            {
                this.Cursor = Cursors.Default;
                //General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                Error.GrabarError(leerDetalles, "GenerarExcelEmitidos");
            }
            else
            {
                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;


                this.Cursor = Cursors.Default;

                if (leerDetalles.Leer())
                {
                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        this.Cursor = Cursors.WaitCursor;


                        //////// Emitidos 
                        sNombreHoja = "Concentrado";
                        datosExportar.DataTableClase = leerDetalles.Tabla(1);
                        sConcepto = string.Format("ORDENES DE COMPRA RECEPCIONADAS CLAVES PERIODO DEL {0} AL {1} ",
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Emitidos 


                        //////// Registrados - Emitidos 
                        sNombreHoja = "Detallado";
                        datosExportar.DataTableClase = leerDetalles.Tabla(2);
                        sConcepto = string.Format("ORDENES DE COMPRA RECEPCIONADAS DETALLADO CLAVES PERIODO DEL {0} AL {1} ",
                            General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Registrados - Emitidos 


                        excel.CerraArchivo();

                        this.Cursor = Cursors.Default;

                        excel.AbrirDocumentoGenerado(true);
                    }
                }

                this.Cursor = Cursors.Default;
            }
        }
        private void ExportarExcel_OLD()
        {
            int iRow = 2, iCol = 1;
            string sNombreFile = "", sRutaReportes = "", sRutaPlantilla = "", sPeriodo = "";
            string sFecha = "";
            string sClaveSSA_Enc = string.Format(" Clave : {0} -- {1} ", txtClaveSSA.Text, lblClaveSSA.Text);
            ///DtGeneral.RutaReportes = sRutaReportes;

            ////if (DatosDetalle())
            ////{

            ////    sNombreFile = "Listado de Ordenes de Compras Recepcionadas Claves.xls";
            ////    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_Listado_Ordenes_Compra_Recepcionadas_Claves.xls";
            ////    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_Listado_Ordenes_Compra_Recepcionadas_Claves.xls", DatosCliente);

            ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////    xpExcel.AgregarMarcaDeTiempo = false;

            ////    if (xpExcel.PrepararPlantilla(sNombreFile))
            ////    {
            ////        xpExcel.GeneraExcel(1);

            ////        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            ////        iRow++;
            ////        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
            ////        iRow++;

            ////        sPeriodo = string.Format("ORDENES DE COMPRA RECEPCIONADAS CLAVES PERIODO DEL {0} AL {1} ",
            ////           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            ////        xpExcel.Agregar(sPeriodo, iRow, 2);

            ////        iRow++;
            ////        xpExcel.Agregar(sClaveSSA_Enc, iRow, 2);
                    
            ////        iRow = 7;
            ////        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

            ////        // Se ponen los detalles
            ////        //leerExportarExcel.RegistroActual = 1;
            ////        iRow = 10;

            ////        for (int i = 1; i <= Grid.Rows; i++)
            ////        {
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.IdProveedor), iRow, (int)Cols.IdProveedor);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.NombreProveedor), iRow, (int)Cols.NombreProveedor);

            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FechaOC), iRow, (int)Cols.FechaOC);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FolioCompra), iRow, (int)Cols.FolioCompra);

            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FolioRecepcion), iRow, (int)Cols.FolioRecepcion);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FechaDocumento), iRow, (int)Cols.FechaDocumento);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FechaRegistro), iRow, (int)Cols.FechaRegistro);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.Personal), iRow, (int)Cols.Personal);
            ////            xpExcel.Agregar(Grid.GetValue(i, (int)Cols.Referencia), iRow, (int)Cols.Referencia);
            ////            xpExcel.Agregar(Grid.GetValueDou(i, (int)Cols.Costo), iRow, (int)Cols.Costo);
            ////            xpExcel.Agregar(Grid.GetValueInt(i, (int)Cols.Piezas), iRow, (int)Cols.Piezas);
            ////            xpExcel.Agregar(Grid.GetValueDou(i, (int)Cols.Total), iRow, (int)Cols.Total);

            ////            iRow++;
            ////        }

            ////        xpExcel.CerrarDocumento();
            ////        //Detalles
            ////        xpExcel.GeneraExcel(2);

            ////        iRow = 2;

            ////        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            ////        iRow++;
            ////        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
            ////        iRow++;

            ////        sPeriodo = string.Format("ORDENES DE COMPRA RECEPCIONADAS DETALLADO CLAVES PERIODO DEL {0} AL {1} ",
            ////           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            ////        xpExcel.Agregar(sPeriodo, iRow, 2);

            ////        iRow++;
            ////        xpExcel.Agregar(sClaveSSA_Enc, iRow, 2);

            ////        iRow = 7;
            ////        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

            ////        iRow = 10;

            ////        for (int i = 1;leerDetalles.Leer(); i++)
            ////        {
            ////            iCol = 2;
            ////            xpExcel.Agregar(leerDetalles.Campo("IdProveedor"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("Proveedor"), iRow, iCol++);

            ////            //xpExcel.Agregar(leerDetalles.Campo("FechaGeneracionOC"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("FolioOrdenCompraReferencia"), iRow, iCol++);

            ////            xpExcel.Agregar(leerDetalles.Campo("Folio"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("FechaDocto"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("FechaRegistro"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("ReferenciaDocto"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("PersonalCompras"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("ClaveSSA_Base"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("ClaveSSA"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("DescripcionSal"), iRow, iCol++);
            ////            //xpExcel.Agregar(leerDetalles.Campo("TipoDeClave"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("Laboratorio"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("CodigoEAN"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("Presentacion"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("ContenidoPaquete"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("ClaveLote"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("Costo"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("CantidadLote"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("TasaIva"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("FechaCad"), iRow, iCol++);
            ////            xpExcel.Agregar(leerDetalles.Campo("MesesParaCaducar"), iRow, iCol++);

            ////            iRow++;
            ////        }

            ////        // Finalizar el Proceso 
            ////        xpExcel.CerrarDocumento();

            ////        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////        {
            ////            xpExcel.AbrirDocumentoGenerado();
            ////        }

            ////    }
            ////}

        }           
        #endregion Botones

        #region Cargar Combos

        private void CargarEstados()
        {
            ////if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboFarmacias.Clear();
                cboFarmacias.Add();


                string sSql = ""; //  "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";

                sSql = " Select distinct E.IdEstado, E.NombreEstado as Estado, E.IdEmpresa, E.StatusEdo, U.UrlFarmacia as UrlRegional " +
                      " From vw_EmpresasEstados E (NoLock) " +
                      " Inner Join vw_Regionales_Urls U (NoLock) On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and U.IdFarmacia = '0001' ) " +
                      " Order By E.IdEmpresa ";

                if (!leerLocal.Exec(sSql))
                {
                    Error.GrabarError(leerLocal, "CargarEstados()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                }
                else
                {
                    cboEstados.Add(leerLocal.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0; 

        }

        private void CargarFarmacias()
        {
            ////if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                sSqlFarmacias = string.Format(
                    "Select Distinct \n" +
                    "\tF.IdFarmacia, (F.IdFarmacia + ' - ' + F.NombreFarmacia) as Farmacia, U.UrlFarmacia, C.Servidor \n" + 
                    "From CatFarmacias F (Nolock) \n" + 
                    "Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia ) \n" + 
                    "Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) \n" + 
                    "Inner Join COM_OCEN_Almacenes_Regionales CA (NoLock) On ( F.IdEstado = CA.IdEstado and F.IdFarmacia = CA.IdFarmacia ) \n" + 
                    "Where F.IdEstado = '{0}' \n--And F.EsAlmacen = 1  " + 
                    "\tand U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", cboEstados.Data); 

                if (!leerLocal.Exec(sSqlFarmacias))
                {
                    Error.GrabarError(leerLocal, "CargarFarmacias()");
                    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                }
                else
                {
                    cboFarmacias.Add(leerLocal.DataSetClase, true, "IdFarmacia", "Farmacia");

                }
            }

            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos 

        #region Funciones 
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;            
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }

        private void CargaDatosClaveSSA()
        {
            //Se hace de esta manera para la ayuda.             
            txtClaveSSA.Text = leerLocal.Campo("ClaveSSA");
            lblClaveSSA.Text = leerLocal.Campo("Descripcion");
            
        }

        private void CargaDetallesCompras()
        {
            string sWhere = "";

            if (txtClaveSSA.Text.Trim() != "")            
            {
                sWhere = " And ClaveSSA = '" + txtClaveSSA.Text + "'";
            }
           
            if (txtReferencia.Text.Trim() != "")
            {
                sWhere += " And ReferenciaDocto like '%" + txtReferencia.Text + "%'";
            }


            string sSql =
                string.Format(" Select Distinct M.IdEmpresa, M.IdProveedor, M.Proveedor, convert(varchar(10), OC.FechaRegistro, 120) As FechaGeneracionOC, " +
	            " M.FolioOrdenCompraReferencia, M.Folio, convert(varchar(10), M.FechaDocto, 120) As FechaDocto, " + 
	            " convert(varchar(10), M.FechaRegistro, 120) As FechaRegistro, OC.NombrePersonal as PersonalGeneraOC, M.ReferenciaDocto, " +
                " M.CostoUnitario as Costo, SUM(CantidadLote) as Cantidad, SUM(ImporteLote) as Importe " +
	            " From vw_Impresion_Recepcion_Orden_Compra M (Nolock) " +
                " Inner Join vw_OrdenesCompras_Claves_Enc OC (NoLock) " + 
		            " On ( M.IdEmpresa = OC.FacturarA and M.IdEstado = OC.EstadoEntrega and M.IdFarmacia = OC.EntregarEn " + 
		                " and M.FolioOrdenCompraReferencia = OC.Folio ) " + 
	            " Where M.IdEstado = '{0}' And M.IdFarmacia = '{1}'  " + 
	            " And Convert( varchar(10), M.FechaRegistro, 120) between '{2}' and '{3}'  {4} " +
                " Group By  M.IdEmpresa, M.IdProveedor, M.Proveedor, OC.FechaRegistro, M.FolioOrdenCompraReferencia, M.Folio, M.FechaDocto, " +
                " M.FechaRegistro, OC.NombrePersonal, M.ReferenciaDocto, M.CostoUnitario " +
                " Order By Folio, FechaRegistro ", 
                cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), sWhere );

            //// 
            lblTotal.Text = iValor_0.ToString(sFormato); 
            Grid.Limpiar(); 

            // if (validarDatosDeConexion())
            {
                ////cnnUnidad = new clsConexionSQL(DatosDeConexion);
                ////cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                ////cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300; 

                // leer = new clsLeer(ref cnnUnidad);
                leer = new clsLeer(); 
                conecionCte = new clsConexionClienteUnidad();
                
                conecionCte.Empresa = DtGeneral.EmpresaConectada;
                conecionCte.Estado = cboEstados.Data;
                conecionCte.Farmacia = cboFarmacias.Data;
                conecionCte.Sentencia = sSql; 

                conecionCte.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
                conecionCte.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

                try
                {
                    // sUrl_Regional = General.Url; 
                    conexionWeb.Url = sUrl_Regional;
                    conexionWeb.Timeout = iTimeOut; 
                    leer.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());
                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message); 
                }


                if ( leer.SeEncontraronErrores() )
                {
                    Error.GrabarError(leer, "CargaDetallesCompras()");
                    General.msjError("Ocurrió un error al obtener la información de las compras.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        btnImprimir.Enabled = true;
                        btnExportarExcel.Enabled = true; 
                        
                        //// bSeEncontroInformacion = true;
                        Grid.LlenarGrid(leer.DataSetClase, false, false);
                        lblTotal.Text = Grid.TotalizarColumnaDou((int)Cols.Total).ToString(sFormato); 
                    }
                    else
                    {
                        // bSeEncontroInformacion = false;
                        General.msjUser("No se encontro información con los criterios especificados, verifique."); 
                    }                   
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = false;

            if (cboEstados.SelectedIndex != 0)
            {
                bRegresa = true;
            }

            if (bRegresa)
            {
                if (cboFarmacias.SelectedIndex != 0)
                {
                    bRegresa = true;
                }
                else
                {
                    bRegresa = false;
                }
            }

            if (bRegresa && txtClaveSSA.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado la Clave SSA, Verifique !!");
                txtClaveSSA.Focus();
            }

            if (!bRegresa)
            {
                General.msjAviso("Faltan Datos por Capturar, Verifique !!");
            }

            return bRegresa;
        }

        private bool DatosDetalle()
        {
            bool bRegresa = true;
            string sWhere = "";

            if (txtClaveSSA.Text.Trim() != "")
            {
                sWhere = " And P.ClaveSSA = '" + txtClaveSSA.Text + "'";
            }

            if (txtReferencia.Text.Trim() != "")
            {
                sWhere += " And ReferenciaDocto like '%" + txtReferencia.Text + "%'";
            }


            string sSql =
                string.Format(" Select E.IdEmpresa, E.IdProveedor, E.Proveedor, convert(varchar(10), FechaGeneracionOC, 120) As FechaGeneracionOC, " +
                "FolioOrdenCompraReferencia, G.Idpersonal As IdPersonalCompras, G.NombrePersonal As PersonalCompras, E.Folio, " +
                "convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro, " +
                "ReferenciaDocto, Total, E.Estado, E.Farmacia, ClaveSSA_Base, ClaveSSA, DescripcionSal, TipoDeClave, Laboratorio, L.CodigoEAN, Presentacion, " +
                "ContenidoPaquete, ClaveLote, Costo, L.CantidadRecibida As CantidadLote, P.TasaIva, FechaCad, " +
                "datediff(mm, getdate(), IsNull(FechaCad, cast('2000-01-01' as datetime))) as MesesParaCaducar " +
                "From vw_OrdenesDeComprasEnc E (Nolock) " +
                "Inner Join vw_OrdenesCompras_Claves_Enc G (NoLock) " +
                "	On (E.EstadoGenera = G.IdEstado And E.FarmaciaGenera = G.IdFarmacia And E.FolioOrdenCompraReferencia = G.Folio)" +
                "Inner Join vw_OrdenesDeComprasDet D (Nolock) " +
                "   On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio) " +
                "Inner Join vw_OrdenesDeComprasDet_Lotes L (Nolock) " +
                "   On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia And E.Folio = L.Folio And D.CodigoEAN = L.CodigoEAN) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On (L.CodigoEAN = P.CodigoEAN)" +
                " Where E.IdEstado = '{0}' And E.IdFarmacia = '{1}'  " +
                " And Convert( varchar(10), E.FechaRegistro, 120) between '{2}' and '{3}'  {4}  " +
                " Order By E.Folio, E.FechaRegistro ", cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value),
                General.FechaYMD(dtpFechaFinal.Value), sWhere);

            leerDetalles = new clsLeer();
            conecionCte.Sentencia = sSql;

            //leer.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());

            try
            {
                // sUrl_Regional = General.Url; 
                conexionWeb.Url = sUrl_Regional;
                conexionWeb.Timeout = iTimeOut;
                leerDetalles.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());
            }
            catch (Exception ex)
            {
                Error.LogError(ex.Message);
            }


            if (leerDetalles.SeEncontraronErrores())
            {
                Error.GrabarError(leerDetalles, "CargaDetallesCompras()");
                General.msjError("Ocurrió un error al obtener la información de las compras.");
                bRegresa = false;
            }
            else
            {
                if (leerDetalles.Registros == 0)
                {
                    // bSeEncontroInformacion = false;
                    General.msjUser("No se encontro información con los criterios especificados, verifique.");
                    bRegresa = false;
                }
            }
            return bRegresa;
        }

        #endregion Funciones

        #region Eventos 
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leerLocal.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text.Trim(), "txtClaveSSA_Validating");
                if (leerLocal.Leer())
                {
                    CargaDatosClaveSSA();
                }
                else
                {
                    txtClaveSSA.Focus();
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            sUrl_Regional = ""; 
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                sUrl_Regional = cboEstados.ItemActual.GetItem("UrlRegional"); 
                CargarFarmacias();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerLocal.DataSetClase = Ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");

                if (leerLocal.Leer())
                {
                    CargaDatosClaveSSA();
                }
            }
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblClaveSSA.Text = "";
        }

        private void grdCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            
            FrmOrdenesDeComprasUnidad f = new FrmOrdenesDeComprasUnidad();
            f.MostrarFolioCompra(Grid.GetValue(Grid.ActiveRow, 1), cboEstados.Data, cboFarmacias.Data,
                Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioRecepcion), sUrl_Regional); 
        } 
        #endregion Eventos        
             
                
    }
}
