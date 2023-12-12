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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Vales
{
    public partial class FrmInformacionVales : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer leer;
        clsLeerWebExt leerWeb;

        wsCnnCliente conexionWeb;
        Thread _workerThread;
        
        clsDatosCliente DatosCliente;
        clsListView lstEmitidos;
        clsListView lstRegistrados;

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        DataSet dtsEmitidos = new DataSet();
        DataSet dtsRegistrados = new DataSet();
        DataSet dtsDetallado = new DataSet();
        clsConsultas Consultas;
        clsAyudas ayuda;

        string sUrl = "";
        string sHost = "";
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        int iAñoReporte = 0;
        string sMesReporte = "";

        public FrmInformacionVales()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();

            CheckForIllegalCrossThreadCalls = false;
            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);

            lstEmitidos = new clsListView(lstValesEmitidos);
            lstEmitidos.OrdenarColumnas = false;
            lstEmitidos.PermitirAjusteDeColumnas = true;

            lstRegistrados = new clsListView(lstValesRegistrados);
            lstRegistrados.OrdenarColumnas = false;
            lstRegistrados.PermitirAjusteDeColumnas = true;

            Cargar_Empresas();           
        }

        private void FrmInformacionVales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = " Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (myLeer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(myLeer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select Distinct U.IdEstado, (U.IdEstado + ' - ' + U.Estado) as Estado, U.UrlFarmacia, C.Servidor " +
                                " From vw_Regionales_Urls U (Nolock) " +
                                " Inner Join CFGSC_ConfigurarConexiones C (Nolock) On ( U.IdEstado = C.IdEstado And U.IdFarmacia = C.IdFarmacia ) " +
                                " Where U.IdEmpresa = '{0}' Order By U.IdEstado ", sEmpresa);
            if (myLeer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add();
                cboEdo.Add(myLeer.DataSetClase, true, "IdEstado", "Estado");
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }                
        
        #endregion Carga_Combos

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;
            FrameParametros.Enabled = false;

            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.InformacionVales);
            _workerThread.Name = "Cargando Información";
            _workerThread.Start();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(true);
            tabVales.SelectTab(0);
            tabVales.SelectTab(1);
            FrameParametros.Enabled = true;

            lstEmitidos.Limpiar();
            lstRegistrados.Limpiar();
        }

        private void HabilitarControles(bool bValor)
        {
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;
            //btnImprimir.Enabled = true;
            btnExportarExcel.Enabled = bValor;
            FrameParametros.Enabled = bValor;
        }

        private void ActivarControles()
        {            
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            //btnImprimir.Enabled = true;
            btnExportarExcel.Enabled = true;
            FrameParametros.Enabled = true;
        }
        #endregion Funciones

        #region Eventos_Combos
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {            
            Cargar_Estados();            
        }

        private void cboEdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEdo.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboEdo.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboEdo.ItemActual.Item)["Servidor"].ToString();                
            }
        }        
        #endregion Eventos_Combos

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;
                
                FrameParametros.Enabled = true;
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = true;

                if (!bSeEncontroInformacion)
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles();

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                }
            }
        }

        #region ObtenerInformacion
        private void InformacionVales()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            int iMes = 0, iAño = 0;

            iAño = dtpFecha.Value.Year;
            iMes = dtpFecha.Value.Month;

            // se inicializan los objetos de los dataset 
            dtsEmitidos = new DataSet();
            dtsRegistrados = new DataSet();
            dtsDetallado = new DataSet();            

            string sSql = "";
            sSql = string.Format(" Exec spp_Rpt_COM_OCEN_Vales_Por_Mes '{0}', '{1}', '{2}', '{3}'  ", 
                                cboEmpresas.Data, cboEdo.Data, iAño, iMes );

            lstEmitidos.Limpiar();
            lstRegistrados.Limpiar();

            leer.Reset();
            leer.DataSetClase = GetInformacionRegional(sSql);
            if (leer.SeEncontraronErrores())
            {
                Error.GrabarError(leer, sSql, "InformacionVales()", "");
            }
            else
            {
                bSeEncontroInformacion = true;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados");
                    bSeEjecuto = true;
                }
                else
                {
                    dtsEmitidos.Tables.Add(leer.Tabla(1).Copy());
                    dtsRegistrados.Tables.Add(leer.Tabla(2).Copy());
                    dtsDetallado.Tables.Add(leer.Tabla(3).Copy());

                    lstRegistrados.CargarDatos(dtsRegistrados, true, true);
                    lstEmitidos.CargarDatos(dtsEmitidos, true, true);         
                                        
                    ActivarControles();
                    btnExportarExcel.Enabled = true;

                    tabVales.SelectTab(0);
                }
            }

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }
        #endregion ObtenerInformacion

        #region Conexiones
        private DataSet GetInformacionCentral(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);

                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniOficinaCentral, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniOficinaCentral));

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
        #endregion Conexiones

        #region Exportar_Excel

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";

            iAñoReporte = dtpFecha.Value.Year;
            sMesReporte = General.FechaNombreMes(dtpFecha.Value);

            string sNombre = string.Format("Reporte de Farmacias que Emitieron Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
            string sNombreFile = "Vales_Emitidos_Registrados_" + iAñoReporte.ToString() + "_" + sMesReporte;

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                iColBase = 2;
                iRenglon = 2;

                leer.DataSetClase = dtsEmitidos;
                iColsEncabezado = iRenglon + 10;
                leer.RegistroActual = 1;

                sNombreHoja = "Vales Emitidos";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);


                iColBase = 2;
                iRenglon = 2;

                leer.DataSetClase = dtsRegistrados;
                iColsEncabezado = iRenglon + 10;
                leer.RegistroActual = 1;

                sNombreHoja = "Vales Registrados";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                iColBase = 2;
                iRenglon = 2;

                leer.DataSetClase = dtsDetallado;
                iColsEncabezado = iRenglon + 10;
                leer.RegistroActual = 1;

                sNombreHoja = "Detallado de Vales Registrados";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
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

        //private void ExportarExcel()
        //{
        //    string sNombreFile = "";
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Vales_Emitidos_Registrados.xls";
            
        //    HabilitarControles(false);           

        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Vales_Emitidos_Registrados.xls", DatosCliente);
        //    //DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Vales_Emitidos_Registrados.xls", DatosCliente);

        //    iAñoReporte = dtpFecha.Value.Year;
        //    sMesReporte = General.FechaNombreMes(dtpFecha.Value);

        //    sNombreFile = "Vales_Emitidos_Registrados_" + iAñoReporte.ToString() + "_" + sMesReporte;

        //    if (bRegresa)
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = false;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla(sNombreFile))
        //        {
        //            this.Cursor = Cursors.WaitCursor;

        //            ExportarValesEmitidos();
        //            ExportarValesRegistrados();
        //            ExportarDetalles();
                    

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }

        //        this.Cursor = Cursors.Default;
        //    }
        //    HabilitarControles(true);
        //}

        //private void ExportarValesEmitidos()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sPeriodo = "";

        //    leer.DataSetClase = dtsEmitidos;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(cboEmpresas.Text, 2, 2);
        //    xpExcel.Agregar(cboEdo.Text, 3, 2);

        //    sPeriodo = string.Format("Reporte de Farmacias que Emitieron Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
        //    xpExcel.Agregar(sPeriodo, 4, 2);

        //    //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("IdEmpresa"), iRenglon, 2);
        //        xpExcel.Agregar(leer.Campo("IdEstado"), iRenglon, 3);
        //        xpExcel.Agregar(leer.Campo("IdJurisdiccion"), iRenglon, 4);
        //        xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, 5);
        //        xpExcel.Agregar(leer.Campo("Folio Vale"), iRenglon, 6);
        //        xpExcel.Agregar(leer.Campo("Folio Venta"), iRenglon, 7);
        //        xpExcel.Agregar(leer.Campo("Numero Receta"), iRenglon, 8);
        //        xpExcel.Agregar(leer.Campo("Fecha Receta"), iRenglon, 9);
        //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, 10);
        //        xpExcel.Agregar(leer.Campo("Descripcion Clave SSA"), iRenglon, 11);
        //        xpExcel.Agregar(leer.Campo("Cantidad"), iRenglon, 12);
        //        xpExcel.Agregar(leer.Campo("IdBeneficiario"), iRenglon, 13);
        //        xpExcel.Agregar(leer.Campo("Beneficiario"), iRenglon, 14);
        //        xpExcel.Agregar(leer.Campo("Poliza"), iRenglon, 15);
        //        xpExcel.Agregar(leer.Campo("Fecha Registro"), iRenglon, 16);
        //        xpExcel.Agregar(leer.Campo("Fecha Canje"), iRenglon, 17);
        //        xpExcel.Agregar(leer.Campo("IdCliente"), iRenglon, 18);
        //        xpExcel.Agregar(leer.Campo("Cliente"), iRenglon, 19);
        //        xpExcel.Agregar(leer.Campo("IdSubCliente"), iRenglon, 20);
        //        xpExcel.Agregar(leer.Campo("SubCliente"), iRenglon, 21);
        //        xpExcel.Agregar(leer.Campo("IdPrograma"), iRenglon, 22);
        //        xpExcel.Agregar(leer.Campo("Programa"), iRenglon, 23);
        //        xpExcel.Agregar(leer.Campo("IdSubPrograma"), iRenglon, 24);
        //        xpExcel.Agregar(leer.Campo("SubPrograma"), iRenglon, 25);
        //        xpExcel.Agregar(leer.Campo("Status"), iRenglon, 26);

        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}

        //private void ExportarValesRegistrados()
        //{
        //    int iHoja = 2, iRenglon = 9;
        //    string sPeriodo = "";

        //    leer.DataSetClase = dtsRegistrados;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(cboEmpresas.Text, 2, 2);
        //    xpExcel.Agregar(cboEdo.Text, 3, 2);

        //    sPeriodo = string.Format("Reporte de Farmacias que Registraron Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
        //    xpExcel.Agregar(sPeriodo, 4, 2);

        //    //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("IdEmpresa"), iRenglon, 2);
        //        xpExcel.Agregar(leer.Campo("IdEstado"), iRenglon, 3);
        //        xpExcel.Agregar(leer.Campo("IdJurisdiccion"), iRenglon, 4);
        //        xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, 5);
        //        xpExcel.Agregar(leer.Campo("Folio"), iRenglon, 6);
        //        xpExcel.Agregar(leer.Campo("Folio Vale"), iRenglon, 7);
        //        xpExcel.Agregar(leer.Campo("Folio Venta Generado"), iRenglon, 8);
        //        xpExcel.Agregar(leer.Campo("FechaRegistro"), iRenglon, 9);
        //        xpExcel.Agregar(leer.Campo("SubTotal"), iRenglon, 10);
        //        xpExcel.Agregar(leer.Campo("Iva"), iRenglon, 11);
        //        xpExcel.Agregar(leer.Campo("Total"), iRenglon, 12);
        //        xpExcel.Agregar(leer.Campo("IdProveedor"), iRenglon, 13);
        //        xpExcel.Agregar(leer.Campo("Proveedor"), iRenglon, 14);                

        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}

        //private void ExportarDetalles()
        //{
        //    int iHoja = 3, iRenglon = 9;
        //    string sPeriodo = "";

        //    leer.DataSetClase = dtsDetallado;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(cboEmpresas.Text, 2, 2);
        //    xpExcel.Agregar(cboEdo.Text, 3, 2);

        //    sPeriodo = string.Format("Reporte Detallado de Farmacias que Registraron Vales en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
        //    xpExcel.Agregar(sPeriodo, 4, 2);

        //    //// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("IdEmpresa"), iRenglon, 2);
        //        xpExcel.Agregar(leer.Campo("Empresa"), iRenglon, 3);
        //        xpExcel.Agregar(leer.Campo("IdEstado"), iRenglon, 4);
        //        xpExcel.Agregar(leer.Campo("Estado"), iRenglon, 5);
        //        xpExcel.Agregar(leer.Campo("IdJurisdiccion"), iRenglon, 6);
        //        xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRenglon, 7);
        //        xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, 8);
        //        xpExcel.Agregar(leer.Campo("Farmacia"), iRenglon, 9);
        //        xpExcel.Agregar(leer.Campo("Folio"), iRenglon, 10);
        //        xpExcel.Agregar(leer.Campo("FolioVale"), iRenglon, 11);
        //        xpExcel.Agregar(leer.Campo("FolioVentaGenerado"), iRenglon, 12);
        //        xpExcel.Agregar(leer.Campo("FechaRegistro"), iRenglon, 13);
        //        xpExcel.Agregar(leer.Campo("SubTotal"), iRenglon, 14);
        //        xpExcel.Agregar(leer.Campo("Iva"), iRenglon, 15);
        //        xpExcel.Agregar(leer.Campo("Total"), iRenglon, 16);
        //        xpExcel.Agregar(leer.Campo("IdProveedor"), iRenglon, 17);
        //        xpExcel.Agregar(leer.Campo("Proveedor"), iRenglon, 18);
        //        xpExcel.Agregar(leer.Campo("IdClaveSSA_Sal"), iRenglon, 19);
        //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, 20);
        //        xpExcel.Agregar(leer.Campo("DescripcionSal"), iRenglon, 21);
        //        xpExcel.Agregar(leer.Campo("IdProducto"), iRenglon, 22);
        //        xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, 23);
        //        xpExcel.Agregar(leer.Campo("DescripcionProducto"), iRenglon, 24);
        //        xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRenglon, 25);
        //        xpExcel.Agregar(leer.Campo("SubFarmacia"), iRenglon, 26);
        //        xpExcel.Agregar(leer.Campo("ClaveLote"), iRenglon, 27);
        //        xpExcel.Agregar(leer.Campo("CantidadLote"), iRenglon, 28);
        //        xpExcel.Agregar(leer.Campo("SubTotalLote"), iRenglon, 29);
        //        xpExcel.Agregar(leer.Campo("Cantidad"), iRenglon, 30);
        //        xpExcel.Agregar(leer.Campo("CostoUnitario"), iRenglon, 31);
        //        xpExcel.Agregar(leer.Campo("SubTotal_Producto"), iRenglon, 32);
        //        xpExcel.Agregar(leer.Campo("Iva_Producto"), iRenglon, 33);
        //        xpExcel.Agregar(leer.Campo("Importe_Producto"), iRenglon, 34);
        //        xpExcel.Agregar(leer.Campo("Año"), iRenglon, 35);
        //        xpExcel.Agregar(leer.Campo("Mes"), iRenglon, 36);                

        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}
        #endregion Exportar_Excel
    }
}
