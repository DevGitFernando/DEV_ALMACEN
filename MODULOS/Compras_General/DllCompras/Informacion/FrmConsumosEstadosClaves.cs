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
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;

namespace DllCompras.Informacion
{
    public partial class FrmConsumosEstadosClaves : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion;
        clsLeer myLeer;
        clsLeer leer;
        clsLeerWebExt leerWeb;
       
        wsCnnCliente conexionWeb;
        Thread _workerThread;

        //clsGrid myGrid;
        clsDatosCliente DatosCliente;
        clsListView lst;

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdiccion = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        clsConsultas Consultas;
        clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");

        string sUrl = "";
        string sHost = "";
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        FrmListaClavesReporte listaClaves = new FrmListaClavesReporte();
        DataSet dtsListaClaves;
        string sListaDeClaves = ""; 

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2, TipoDisp = 3, Presentacion = 4,
            Contenido = 5, Piezas = 6, Cajas = 7
        }

        public FrmConsumosEstadosClaves()
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
            //myGrid = new clsGrid();
            //myGrid = new clsGrid(ref grdClaves, this);
            ////myGrid.EstiloGrid(eModoGrid.ModoRow);
            ////grdClaves.EditModeReplace = true;
            ////myGrid.BackColorColsBlk = Color.White;
            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            Cargar_Empresas();

            toolTip.SetToolTip(btnAgregarClave, "Agregar clave a lista para generar reporte");
            toolTip.SetToolTip(btnVerClaves, "Visualizar la lista de claves seleccionadas"); 
        }

        private void FrmListadoOrdenesDeCompras_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }        

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                ObtenerInformacion(); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //myGrid.ExportarExcel();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //if (rdoConcentrado.Checked)
            //{
            //    GeneraReporteConcentrado();
            //}

            //if (rdoDetallado.Checked)
            //{
            //    GeneraReporteDetallado();
            //}

            GenerarReporteExcel();

        }
        #endregion Botones

        #region Reportes

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;

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

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void GeneraReporteConcentrado()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";

        //    HabilitarControles(false);
        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;
            
        //    sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data + ".xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_ConsumoEdo_Concentrado.xls";            
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_ConsumoEdo_Concentrado.xls", DatosCliente);            

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;

            
        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {                
        //        xpExcel.GeneraExcel();

        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["Estado"].ToString(), iRow, 2);
        //        iRow++;

        //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;
                
        //        while (leerExportarExcel.Leer())
        //        {
        //            xpExcel.Agregar(leerExportarExcel.Campo("Jurisdicción"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Id Farmacia"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Clave SSA"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripción Clave SSA"), iRow, 6);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Tipo de Dispensación"), iRow, 7);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Presentación"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Envase"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Piezas"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cajas"), iRow, 11);

        //            iRow++;
        //        }                
                
        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }                
        //        HabilitarControles(true);
        //    }
        //}

        //private void GeneraReporteDetallado()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";

        //    HabilitarControles(false);
        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;
            
        //    sNombreFile = "COM_Rpt_ConsumoEdo_Detallado" + "_" + cboEdo.Data + ".xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_ConsumoEdo_Detallado.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_ConsumoEdo_Detallado.xls", DatosCliente);            

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;
            
        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {                
        //        xpExcel.GeneraExcel();

        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["Estado"].ToString(), iRow, 2);
        //        iRow++;

        //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;
                
        //        while (leerExportarExcel.Leer())
        //        {
        //            xpExcel.Agregar(leerExportarExcel.Campo("Jurisdicción"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Id Farmacia"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Clave SSA"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Producto"), iRow, 6);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Codigo EAN"), iRow, 7);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripción"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Tipo de Dispensación"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Presentación"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Envase"), iRow, 11);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Piezas"), iRow, 12);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cajas"), iRow, 13);

        //            iRow++;
        //        }                

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }               
        //        HabilitarControles(true);
        //    }
        //}
        #endregion Reportes

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            listaClaves = new FrmListaClavesReporte();
            dtsListaClaves = listaClaves.ClavesSSA;
            sListaDeClaves = listaClaves.ListaDeClavesSSA; 

            //myGrid.Limpiar(false);
            lst.Limpiar();
            FrameDatos.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameDetalles.Enabled = true;
            FrameReporte.Enabled = true;
            btnExportarExcel.Enabled = false;
            FrameClave.Enabled = true;

            cboJurisdiccion.Clear();
            cboJurisdiccion.Add("0", "<< Seleccione >>");
            cboJurisdiccion.SelectedIndex = 0;

            lblClavesConsulta.Text = string.Format("Claves : {0} ", 0); 

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

        private void ObtenerInformacion()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false; 
            FrameDatos.Enabled = false;
            FrameFechas.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameReporte.Enabled = false;
            FrameClave.Enabled = false;

            btnAgregarClave.Enabled = false;
            btnVerClaves.Enabled = false; 

            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.CargarGrid);
            _workerThread.Name = "Cargando Información";
            _workerThread.Start();
        }

        private void CargarGrid() 
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            string sJuris = ""; 
            int iTipoDispensacion = 0, iTipoReporte = 0;

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 2;
            }

            if (rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 1;
            }

            if (rdoConcentrado.Checked)
            {
                iTipoReporte = 0;
            }

            if( rdoDetallado.Checked)
            {
                iTipoReporte = 1;
            }

            if (cboJurisdiccion.SelectedIndex != 0)
            {
                sJuris = cboJurisdiccion.Data;
            }
            else
            {
                sJuris = "*";
            }

            if (txtLaboratorio.Text.Trim() == "")
            {
                txtLaboratorio.Text = "*";
            }

            string sSql = "";
            sSql = string.Format(" Exec spp_Rpt_OCEN_ConsumoEstados_Claves '{0}', '{1}', '{2}', [ {3} ], '{4}', '{5}', '{6}', '{7}', '{8}'  ",
                                 cboEmpresas.Data, cboEdo.Data, sJuris, sListaDeClaves, 
                                 General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), 
                                 iTipoDispensacion, iTipoReporte, txtLaboratorio.Text); 

            //myGrid.Limpiar(false);

            dtsEstados = new DataSet();
            dtsFarmacias = new DataSet();
            dtsFarmacias = new DataSet();

            lst.Limpiar();

            leerExportarExcel = new clsLeer(); 
            leer = new clsLeer(); 
            leer.Reset();
            leer.DataSetClase = GetInformacionUnidad_Directo(sSql);
            if (leer.SeEncontraronErrores())
            {
                Error.GrabarError(leer, sSql, "CargarGrid()", "");                
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
                    dtsEstados.Tables.Add(leer.Tabla(1).Copy());
                    dtsJurisdiccion.Tables.Add(leer.Tabla(2).Copy());
                    dtsFarmacias.Tables.Add(leer.Tabla(3).Copy());
                    lst.CargarDatos(dtsEstados, true, true);                    
                    
                    leerExportarExcel.DataSetClase = leer.DataSetClase;       
                    ActivarControles();                    
                    btnExportarExcel.Enabled = true;
                }                
            }

            bEjecutando = false;        
            this.Cursor = Cursors.Default;
        }

        private void HabilitarControles(bool bValor)
        {
            
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;
            //btnImprimir.Enabled = true;
            FrameDatos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            //FrameDetalles.Enabled = bValor;
            FrameReporte.Enabled = bValor;
        }
        #endregion Funciones

        #region Eventos_Grid
        ////private void grdOrdenCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        ////{
            
        ////}
        #endregion Eventos_Grid               

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

        private void Cargar_Jurisdicciones()
        {
            cboJurisdiccion.Clear();
            cboJurisdiccion.Add("0", "<< Seleccione >>");

            myLeer.DataSetClase = Consultas.Jurisdicciones(cboEdo.Data, "Cargar_Jurisdicciones()"); ;
            if (myLeer.Leer())
            {
                cboJurisdiccion.Add(myLeer.DataSetClase, true, "IdJurisdiccion", "NombreJurisdiccion");
                cboJurisdiccion.SelectedIndex = 0;
            }


        }
        #endregion Carga_Combos

        #region Eventos
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
                if (sHost.Contains(":"))
                {
                    string[] sServidor = sHost.Split(':');
                    sHost = sServidor[0];
                }
                //// cboFarmacias.Enabled = false;
                Cargar_Jurisdicciones();
            }
        }

        private void ActivarControles()
        {
            //this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            //btnImprimir.Enabled = true;
            FrameDatos.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameDetalles.Enabled = true;
            FrameReporte.Enabled = true;
            FrameClave.Enabled = true;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameDatos.Enabled = true;
                FrameDispensacion.Enabled = true;
                FrameFechas.Enabled = true;
                FrameClave.Enabled = true;
                FrameDetalles.Enabled = true;
                FrameReporte.Enabled = true;
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = leerExportarExcel.Registros > 0; 

                btnAgregarClave.Enabled = true;
                btnVerClaves.Enabled = true; 

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
        #endregion Eventos

        #region Validaciones 
        private bool validarDatos()
        {
            bool bRegresa = true;
            sListaDeClaves = listaClaves.ListaDeClavesSSA;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una empresa válida, verifique.");
                cboEmpresas.Focus();
            }

            if (bRegresa && cboEdo.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEdo.Focus();
            }

            ////if (bRegresa && cboJurisdiccion.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
            ////    cboJurisdiccion.Focus();
            ////}

            if (bRegresa && sListaDeClaves == "")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado ninguna clave a procesar, verifique."); 
                txtClaveSSA.Focus(); 
            }

            return bRegresa; 
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
        #endregion Validaciones

        #region Conexiones
        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }
        
        private DataSet GetInformacionUnidad_Directo(string Cadena)
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
        #endregion Conexiones

        #region Eventos_ClaveSSA
        private void btnVerClaves_Click(object sender, EventArgs e)
        {
            listaClaves = new FrmListaClavesReporte();
            listaClaves.ClavesSSA = dtsListaClaves; 
            listaClaves.ShowDialog();

            dtsListaClaves = listaClaves.ClavesSSA;
            lblClavesConsulta.Text = string.Format("Claves : {0} ", listaClaves.NumClaves);
            txtClaveSSA.Focus(); 
        }

        private void btnAgregarClave_Click(object sender, EventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                listaClaves = new FrmListaClavesReporte();

                listaClaves.ClavesSSA = dtsListaClaves;
                dtsListaClaves = listaClaves.Agregar(txtClaveSSA.Text, lblDescripcion.Text);
                lblClavesConsulta.Text = string.Format("Claves : {0} ", listaClaves.NumClaves);

                txtClaveSSA.Enabled = true;
                txtClaveSSA.Focus();
                txtClaveSSA.Text = "";
                lblDescripcion.Text = "";
                lblPresentacion.Text = "";
                lblContPaquete.Text = "";
            }
        }

        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text != "")
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");

                if (myLeer.Leer())                 {
                    txtClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    //lblIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcion.Text = myLeer.Campo("Descripcion");

                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");

                    //txtClaveSSA.Enabled = false;
                }
            }
            //////else
            //////{
            //////    ///txtClaveSSA.Focus();
            //////    txtClaveSSA.Text = "*";
            //////    txtClaveSSA.Enabled = false;
            //////}
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown()");

                if (myLeer.Leer())
                {
                    txtClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    //lblIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcion.Text = myLeer.Campo("Descripcion");

                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");
                    //txtClaveSSA.Enabled = false;
                }
                else
                {
                    txtClaveSSA.Focus();
                }
            }
        }
        #endregion Eventos_ClaveSSA

        private void lstClaves_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
            lblPresentacion.Text = "";
            lblContPaquete.Text = ""; 
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        #region Eventos_Laboratorio
        private void txtLaboratorio_Validating(object sender, CancelEventArgs e)
        {
            if (txtLaboratorio.Text.Trim() != "")
            {
                myLeer.DataSetClase = Consultas.Laboratorios(txtLaboratorio.Text, "txtLaboratorio_Validating");

                if (myLeer.Leer())
                {
                    txtLaboratorio.Text = myLeer.Campo("IdLaboratorio");
                    lblLaboratorio.Text = myLeer.Campo("Descripcion");
                }
            }
            else
            {
                txtLaboratorio.Text = "*";
            }
        }

        private void txtLaboratorio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Laboratorios("txtLaboratorio_KeyDown");

                if (myLeer.Leer())
                {
                    txtLaboratorio.Text = myLeer.Campo("IdLaboratorio");
                    lblLaboratorio.Text = myLeer.Campo("Descripcion");
                }
            }
        }
        #endregion Eventos_Laboratorio
    }
}
