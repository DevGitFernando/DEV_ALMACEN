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
using SC_SolutionsSystem.wsConexion;
using SC_SolutionsSystem.FuncionesGenerales;
//using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;


namespace DllCompras.Seguimiento
{
    public partial class FrmSeguimientoOrdenesCompras : FrmBaseExt
    {
        clsConexionSQL cnn; //= new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;

        clsDatosConexion DatosDeConexion;
        wsCnnCliente conexionWeb;
        
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;

        clsLeerWebExt myWeb;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        string sSqlFarmacias = "";
        string sHost = "";
        DataSet dtsDatosOC;
        DataSet dtsFolioOC;
        DataSet dtsOrdenesCompras;
        //DataSet dtsFoliosEntradasDet = new DataSet();

        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsDatosCliente DatosCliente;

        string sRutaPlantilla = "";

        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 1, Farmacia = 2, Url = 3, Servidor = 4, Folio = 5, Fecha = 6, Consultar = 7, Status = 8,
            FolioEntrada = 9, FechaEntrada = 10
        }

        private enum Cols_Exportar
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, Folio = 4, Fecha = 5, Status = 6,
            FolioEntrada = 7, FechaEntrada = 8
        }

        public FrmSeguimientoOrdenesCompras()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            conexionWeb = new wsCnnCliente();
            conexionWeb.Url = General.Url;
            dtsDatosOC = new DataSet();
            dtsFolioOC = new DataSet();
            dtsOrdenesCompras = new DataSet();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");           

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdTransferencia, this);
            // grid.EstiloGrid(eModoGrid.SeleccionSimple);
            grid.Ordenar(5);
            grid.Ordenar(6);
            grid.Ordenar(7);
            grid.SetOrder((int)Cols.Folio, (int)Cols.FechaEntrada, true);
            grid.AjustarAnchoColumnasAutomatico = true; 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);            

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;            

            Cargar_Empresas();
        }

        #region Botones
        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = false;
            grpFechas.Enabled = false;
            IniciarServicios();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            grpFechas.Enabled = true;

            Fg.IniciaControles(this, true); 
            iBusquedasEnEjecucion = 0;

            grid = new clsGrid(ref grdTransferencia, this);
            grid.Limpiar(false);
            btnExportar.Enabled = false;
            btnActivarServicios.Enabled = false;

            grpDatos.Enabled = true;
            chkTodas.Checked = false;
            if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }
        }
        #endregion Botones

        #region Funciones 
        private void CargarFarmaciasGrid()
        {
            if (cboFarmaciasDestino.SelectedIndex == 0)
            {
                sSqlFarmacias = string.Format(" Select E.EntregarEn, F.Farmacia, F.UrlFarmacia, C.Servidor, E.Folio, convert(varchar(10),E.FechaRegistro,120) As Fecha " + 
                            " From vw_OrdenesCompras_Claves_Enc E (NoLock) " + 
                            " Left Join vw_Farmacias_Urls F (NoLock) On ( E.EstadoEntrega = F.IdEstado And E.EntregarEn = F.IdFarmacia ) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( E.IdEstado = C.IdEstado and E.EntregarEn = C.IdFarmacia ) " +
                            " Where E.Status = 'OC' and E.IdEstado = '{0}' And E.IdFarmacia = '{1}' " +
                            " And convert(varchar(10),E.FechaRegistro,120) Between '{2}' And '{3}' " +  
                            " Order By E.FechaRegistro, E.Folio ", cboEdo.Data, DtGeneral.FarmaciaConectada,
                            General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            else
            {
                sSqlFarmacias = string.Format(" Select E.EntregarEn, F.Farmacia, F.UrlFarmacia, C.Servidor, E.Folio, convert(varchar(10),E.FechaRegistro,120) As Fecha " +
                            " From vw_OrdenesCompras_Claves_Enc E (NoLock) " +
                            " Left Join vw_Farmacias_Urls F (NoLock) On ( E.EstadoEntrega = F.IdEstado And E.EntregarEn = F.IdFarmacia ) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( E.IdEstado = C.IdEstado and E.EntregarEn = C.IdFarmacia ) " +
                            " Where E.Status = 'OC' and E.IdEstado = '{0}' And E.IdFarmacia = '{1}' And E.EntregarEn = '{2}' " +
                            " And convert(varchar(10),E.FechaRegistro,120) Between '{3}' And '{4}' " +
                            " Order By E.FechaRegistro, E.Folio ", cboEdo.Data, DtGeneral.FarmaciaConectada, cboFarmaciasDestino.Data,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }

            chkTodas.Checked = false;  
            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmaciasGrid()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    grid.Limpiar(false);
                    grid.LlenarGrid(leer.DataSetClase);
                    //btnExportar.Enabled = true;
                    btnActivarServicios.Enabled = true;
                }
                else
                {
                    General.msjUser("No se encontro información bajo los criterios seleccionados. Verifique");
                }
            }
        }

        private void CargarFarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("0", "<< Seleccione >>");   
            query.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = query.AlmacenesRegionales(cboEdo.Data, "CargarFarmaciasDestino");            

            if (leer.Leer())
            {
                cboFarmaciasDestino.Add(leer.DataSetClase, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmaciasDestino.SelectedIndex = 0;

            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Funciones

        #region Eventos
        private void IniciarServicios()
        {            
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnActivarServicios.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            dtsOrdenesCompras = new DataSet();

            dtsOrdenesCompras = PreparaDtsOrdenesCompras();

            for (int i = 1; i <= grid.Rows; i++)
            {
                grid.SetValue(i, (int)Cols.Status, " ");
                grid.SetValue(i, (int)Cols.FolioEntrada, " ");
                grid.SetValue(i, (int)Cols.FechaEntrada, " ");

                if (grid.GetValueBool(i, (int)Cols.Consultar))
                {
                    Thread _workerThread = new Thread(this.ConsultarOrdenCompra);
                    _workerThread.Name = grid.GetValue(i, 2) + grid.GetValue(i, 5);
                    _workerThread.Start(i);
                }
            }
        }

        private void ConsultarOrdenCompra(object Renglon)
        {
            int iRow = (int)Renglon;            
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + cboEdo.Data + "-" + sIdFarmacia;
            string sFolio = grid.GetValue(iRow, (int)Cols.Folio);
            DataSet dtsResultado = new DataSet(); 

            string sTablaFolioOC = "FolioOC_" + sFolio;

            string sSql = string.Format(" Select FolioOrdenCompraReferencia, Folio, convert(varchar(10),FechaRegistro,120) As FechaRegistro " + 
	                    " From vw_OrdenesDeComprasEnc (Nolock) " +
                        " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " + 
	                    " And FolioOrdenCompraReferencia = '{3}' \n " +
                        " Exec spp_COM_OCEN_VerificarDiferenciasOrdenesDeCompras '{0}', '{1}', '{2}', '{3}'  ", 
                        cboEmpresas.Data, cboEdo.Data, sIdFarmacia, sFolio);


            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, (int)Cols.Status, "Procesando");
            iBusquedasEnEjecucion++;

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb, sValor + " -- " + sUrl, "ConsultarOrdenCompra()", "");
                grid.SetValue(iRow, (int)Cols.Status, ""); 
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                if (myWeb.Leer())
                {
                    grid.SetValue(iRow, (int)Cols.Status, "RECEPCIÓN PARCIAL");
                    grid.SetValue(iRow, (int)Cols.FolioEntrada, myWeb.Campo("Folio"));
                    grid.SetValue(iRow, (int)Cols.FechaEntrada, myWeb.Campo("FechaRegistro"));

                    myWeb.RenombrarTabla(2, sTablaFolioOC);
                    dtsFolioOC.Tables.Add(myWeb.Tabla(2).Copy());

                    ObtenerDatosOC(dtsFolioOC);
                

                    dtsResultado.Tables.Add(myWeb.Tabla(sTablaFolioOC));
                    myWeb.DataSetClase = dtsResultado; 
                    if (myWeb.Leer()) 
                    {
                        if (myWeb.CampoBool("EsEntregaTotal"))
                        {
                            grid.SetValue(iRow, (int)Cols.Status, "RECEPCIÓN COMPLETA");
                        }
                    }
                }
                else
                {
                    grid.SetValue(iRow, (int)Cols.Status, "NO RECEPCIONADA");
                }
                grid.ColorRenglon(iRow, colorEjecucionExito);
                
            }
            iBusquedasEnEjecucion--;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnExportar.Enabled = true;
                btnActivarServicios.Enabled = true;
                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void FrmSeguimientoTransferenciaSalidas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Consultar, chkTodas.Checked);
        }
        #endregion Eventos

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (leer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa);
            if (leer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add();
                cboEdo.Add(leer.DataSetClase, true, "IdEstado", "NombreEstado");
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }
        #endregion Carga_Combos

        #region Eventos_Combos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmaciasDestino();
        }
        #endregion Eventos_Combos

        #region Eventos_Grid
        private void grdTransferencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sDato = "";
            sDato = grid.GetValue(grid.ActiveRow, (int)Cols.FolioEntrada);

            if (sDato.Trim() != "") 
            {
                if (iBusquedasEnEjecucion <= 0)
                {
                    IniciarConsultaFoliosOC();
                }
            }
        }
        #endregion Eventos_Grid

        #region Funciones_Varias
        private void IniciarConsultaFoliosOC()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnActivarServicios.Enabled = false;
            btnExportar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            ////Thread _workerThread = new Thread(this.CargarFoliosEntrada);
            ////_workerThread.Name = "Busqueda de Folios OC";
            ////_workerThread.Start();


            CargarFoliosEntrada(); 
        }

        private void CargarFoliosEntrada()
        {
            int iRow = grid.ActiveRow;
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + cboEdo.Data + "-" + sIdFarmacia;
            string sFolio = grid.GetValue(iRow, (int)Cols.Folio);

            sHost = grid.GetValue(iRow, (int)Cols.Servidor);

            if (dtsDatosOC != null)
            {
                dtsDatosOC = null;
                dtsDatosOC = new DataSet();
            }

            string sSql = string.Format(" Exec spp_VerificarDiferenciasOrdenCompra '{0}', '{1}', '{2}', '{3}' ",
                                            cboEmpresas.Data, cboEdo.Data, sIdFarmacia, sFolio);


            grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, (int)Cols.Status, "Procesando");
            iBusquedasEnEjecucion++;

            myWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb, sValor + " -- " + sUrl, "CargarFoliosEntrada()", "");
                grid.SetValue(iRow, (int)Cols.Status, "");
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                if (myWeb.Leer())
                {                               
                    //dtsDatosOC = myWeb.DataSetClase.Copy();
                    dtsDatosOC.Tables.Add(myWeb.Tabla(1).Copy());
                    dtsDatosOC.Tables.Add(myWeb.Tabla(2).Copy());

                    CargarDatosConexion(sUrl);

                    FrmVerificarOrdenesCompras VerificarOC = new FrmVerificarOrdenesCompras();
                    VerificarOC.Folio_Orden = sFolio; 
                    VerificarOC.MostrarPantalla(dtsDatosOC, DatosDeConexion, sUrl, sIdFarmacia);                    
                }
                
                grid.ColorRenglon(iRow, colorEjecucionExito);

            }
            iBusquedasEnEjecucion--;
        }

        private void CargarDatosConexion(string Url)
        {
            conexionWeb.Url = Url;
            DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            DatosDeConexion.Servidor = sHost;
        }
        #endregion Funciones_Varias

        #region Boton_Exportar
        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("SEGUIMIENTO DE ORDENES DE COMPRA PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Seguimiento Ordenes de Compra";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            //leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length - 1;
            iColsEncabezado = iColBase + 6;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                //generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Num. Farmacia");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Farmacia");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Folio");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Fecha");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Status");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Folio Entrada");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, iColBase++, 16, "Fecha Entrada");
                iRenglon++;

                for (int i = 1; i <= grid.Rows; i++)
                {
                    iColBase = 2;
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValue(i, (int)Cols.IdFarmacia), XLAlignmentHorizontalValues.Center, "texto");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValue(i, (int)Cols.Farmacia), XLAlignmentHorizontalValues.Center, "texto");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValue(i, (int)Cols.Folio), XLAlignmentHorizontalValues.Center, "texto");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValueFecha(i, (int)Cols.Fecha).ToString(), XLAlignmentHorizontalValues.Center, "DateTime");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValue(i, (int)Cols.Status), XLAlignmentHorizontalValues.Center, "texto");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValue(i, (int)Cols.FolioEntrada), XLAlignmentHorizontalValues.Center, "texto");
                    generarExcel.EscribirCelda(sNombreHoja, iRenglon, iColBase++, grid.GetValueFecha(i, (int)Cols.FechaEntrada).ToString(), XLAlignmentHorizontalValues.Center, "DateTime");
                    iRenglon++;
                    
                }


                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void Excel()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "", sRutaReportes = "", sRutaPlantilla = "", sPeriodo = "";
        //    string sFecha = ""; 

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Seguimiento Ordenes de Compra.xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_Seguimiento_Ordenes_Compra.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_Seguimiento_Ordenes_Compra.xls", DatosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;

        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.GeneraExcel();

        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
        //        iRow++;

        //        sPeriodo = string.Format("SEGUIMIENTO DE ORDENES DE COMPRA PERIODO DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 7;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        //leerExportarExcel.RegistroActual = 1;
        //        iRow = 10;

        //        for (int i = 1; i <= grid.Rows; i++)
        //        {
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.IdFarmacia), iRow, (int)Cols_Exportar.IdFarmacia);
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.Farmacia), iRow, (int)Cols_Exportar.Farmacia);
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.Folio), iRow, (int)Cols_Exportar.Folio);

        //            sFecha = General.FechaYMD(grid.GetValueFecha(i, (int)Cols.Fecha)); 
        //            xpExcel.Agregar(sFecha, iRow, (int)Cols_Exportar.Fecha);
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.Status), iRow, (int)Cols_Exportar.Status);
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.FolioEntrada), iRow, (int)Cols_Exportar.FolioEntrada);
        //            xpExcel.Agregar(grid.GetValue(i, (int)Cols.FechaEntrada), iRow, (int)Cols_Exportar.FechaEntrada);

        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }

        //    }

        //    GeneraReporteOrdenesCompras();
        //}
        #endregion Boton_Exportar

        #region Obtener_Información_Folios_OC
        private void ObtenerDatosOC(DataSet dtsOC)
        {     
            clsLeer leerOC = new clsLeer();

            leerOC.DataSetClase = dtsOC;
                        
            if (leerOC.Leer())
            {
                AddFolioOC(leerOC.DataSetClase);
            }

            dtsFolioOC = new DataSet();
            
        }

        private static Type GetType(TypeCode TipoDato)
        {
            return Type.GetType("System." + TipoDato.ToString());
        }

        public static DataSet PreparaDtsOrdenesCompras()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtOC = new DataTable("FoliosOC");

            dtOC.Columns.Add("Folio", GetType(TypeCode.String));
            dtOC.Columns.Add("FechaRegistro", GetType(TypeCode.String));
            dtOC.Columns.Add("IdProveedor", GetType(TypeCode.String));
            dtOC.Columns.Add("Proveedor", GetType(TypeCode.String));

            dtOC.Columns.Add("FechaColocacion", GetType(TypeCode.String));
            dtOC.Columns.Add("FechaRequeridaEntrega", GetType(TypeCode.String));
            dtOC.Columns.Add("FechaRecepcion_Inicial", GetType(TypeCode.String));
            dtOC.Columns.Add("FechaRecepcion_Final", GetType(TypeCode.String));

            dtOC.Columns.Add("ClaveSSA", GetType(TypeCode.String));
            dtOC.Columns.Add("DescripcionSal", GetType(TypeCode.String));

            dtOC.Columns.Add("Cantidad_Requerida", GetType(TypeCode.Int32));
            dtOC.Columns.Add("Cantidad_Ingresada", GetType(TypeCode.Int32));
            dtOC.Columns.Add("Cantidad_Faltante", GetType(TypeCode.Int32));
            dts.Tables.Add(dtOC);

            return dts.Clone();
        }

        public void AddFolioOC(DataSet Lista)
        {
            if (dtsOrdenesCompras == null)
            {
                dtsOrdenesCompras = PreparaDtsOrdenesCompras();
            }            

            try
            {
                ////// Agrega los nuevos folios
                dtsOrdenesCompras.Tables[0].Merge(Lista.Tables[0]);
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source;
            }
        }
        #endregion Obtener_Información_Folios_OC

        #region Exportar_Concentrado_OC
        //private void GeneraReporteOrdenesCompras()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";
        //    int iHoja = 1;

        //    leerExportarExcel = new clsLeer();

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    leerExportarExcel.DataSetClase = dtsOrdenesCompras;

        //    sNombreFile = "Listado_Ordenes_de_Compras_Recepcionadas" + ".xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_OC_Recepcionadas.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_OC_Recepcionadas.xls", DatosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;


        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {             

        //        xpExcel.GeneraExcel(iHoja);
        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["NombreEstado"].ToString(), iRow, 2);
        //        iRow++;

        //        sPeriodo = string.Format("LISTADO DE ORDENES DE COMPRAS RECEPCIONADAS, PERIODO DEL {0}  AL  {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;

        //        while (leerExportarExcel.Leer())
        //        {
        //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 6);
        //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRow, 7);

        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaColocacion"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRequeridaEntrega"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRecepcion_Inicial"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRecepcion_Final"), iRow, 11);

        //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad_Requerida"), iRow, 12);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad_Ingresada"), iRow, 13);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad_Faltante"), iRow, 14);

        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }
        //    }
        //}
        #endregion Exportar_Concentrado_OC
    }
}
