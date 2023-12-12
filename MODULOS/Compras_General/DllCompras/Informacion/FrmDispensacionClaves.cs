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
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Informacion
{
    public partial class FrmDispensacionClaves : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexion; 
        
        clsLeer leerRemota;
        clsLeer leerLocal;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
       
        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        DataSet dtsDispensacion = new DataSet();

        clsDatosCliente DatosCliente;
        wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet();
        DataSet dtsClaves = new DataSet();

        DataSet dtsClavesProcesar = new DataSet();
        // string sTablaFarmacia = "CTE_FarmaciasProcesar";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Se declara el objeto de la clase de Auditoria
        //clsAuditoria auditoria;

        string sUrl = "";
        string sHost = "";
        string sRutaPlantilla = "";

        public FrmDispensacionClaves()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
            conexionWeb = new wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "ReportesFacturacionUnidad");
            //leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leerRemota = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.Modulo, this.Name, GnCompras.Version); 
            Ayudas = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, GnCompras.DatosApp, this.Name);            
            //GnCompras.FarmaciaConectada = General.EntidadConectada; 

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;
            //lst.PermitirAjusteDeColumnas = false; 
        }

        private void CargarListaReportes()
        {
        }

        private void CargarClavesRegistradas()
        {
            string sSql = string.Format("Select Distinct IdClaveSSA_Sal as IdClaveSSA, 0 as Procesar, " + 
                " ClaveSSA, DescripcionSal as DescripcionClave " + 
	            " From vw_ExistenciaPorSales (NoLock) " + 
            " Where IdEmpresa <> '' Order by DescripcionSal ");

            if (!leerWeb.Exec(sSql))
            {
                Error.GrabarError(leerWeb, "CargarClavesRegistradas()");
                General.msjError("Ocurrió un error al cargar la lista de Claves registradas."); 
            }
            else
            {
                // Leer el contenido aunque venga vacio 
                dtsClaves = leerWeb.DataSetClase; 
            }
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {            
            Cargar_Empresas();            
            btnNuevo_Click(null, null);            
        }

        #region Cargar Combos 
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = " Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (leerLocal.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add();
                cboEmpresas.Add(leerLocal.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leerLocal.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void CargarEstados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEstados.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select Distinct U.IdEstado, (U.IdEstado + ' - ' + U.Estado) as Estado, U.UrlFarmacia, C.Servidor " +
                                " From vw_Regionales_Urls U (Nolock) " +
                                " Inner Join CFGSC_ConfigurarConexiones C (Nolock) On ( U.IdEstado = C.IdEstado And U.IdFarmacia = C.IdFarmacia ) " +
                                " Where U.IdEmpresa = '{0}' Order By U.IdEstado ", sEmpresa);
            if (leerLocal.Exec(sSql))
            {
                cboEstados.Clear();
                cboEstados.Add();
                cboEstados.Add(leerLocal.DataSetClase, true, "IdEstado", "Estado");
                cboEstados.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(leerLocal.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
        }

        private void CargarJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("*", "<< Todas las Jurisdicciones >>");

            leerLocal.DataSetClase = Consultas.Jurisdicciones(cboEstados.Data, "Cargar_Jurisdicciones()"); ;
            if (leerLocal.Leer())
            {
                cboJurisdicciones.Add(leerLocal.DataSetClase, true, "IdJurisdiccion", "NombreJurisdiccion");
                cboJurisdicciones.SelectedIndex = 0;
            }
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
            
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            // bool bRegresa = false;  

            //////if (validarImpresion())
            //////{
            //////    // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

            //////    DatosCliente.Funcion = "Imprimir()";
            //////    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            //////    byte[] btReporte = null;

            //////    string sEstado = cboEstados.Data;
            //////    //string sFarmacia = cboFarmacias.Data;
            //////    string sFarmacia = txtFarmacia.Text;
            //////    //// Linea Para Prueba
            //////    //GnCompras.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

            //////    myRpt.RutaReporte = GnCompras.RutaReportes;

            //////    if (rdoRecVentas.Checked)
            //////    {
            //////        if (rdoClaves.Checked)
            //////        {
            //////            myRpt.NombreReporte = "Rpt_Cte_Salidas_ClavesSSAMensuales";
            //////        }
            //////        if (rdoAnioClaves.Checked)
            //////        {
            //////            myRpt.NombreReporte = "Rpt_Cte_Salidas_ClavesSSAMensual_Anual";
            //////        }
            //////    }
            //////    else
            //////    {
            //////        if (rdoClaves.Checked)
            //////        {
            //////            myRpt.NombreReporte = "Rpt_Cte_ValesSalidas_ClavesSSAMensuales";
            //////        }
            //////        if (rdoAnioClaves.Checked)
            //////        {
            //////            myRpt.NombreReporte = "Rpt_Cte_ValesSalidas_ClavesSSAMensual_Anual";
            //////        }
            //////    }
                

            //////    //if (General.ImpresionViaWeb)
            //////    {
            //////        ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //////        ////DataSet datosC = DatosCliente.DatosCliente();

            //////        //////conexionWeb.Url = General.Url;
            //////        ////conexionWeb.Timeout = 300000;
            //////        ////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC);
            //////        ////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

            //////        DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //////        DataSet datosC = DatosCliente.DatosCliente();
            //////        bRegresa = GnCompras.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 

            //////    }
            //////    //else
            //////    //{
            //////        // Lineas para pruebas locales ///////
            //////        //myRpt.CargarReporte(true);
            //////        //bRegresa = !myRpt.ErrorAlGenerar;
            //////        //////////////////////////////////////
            //////    //}

            //////    if (!bRegresa)
            //////    {
            //////        General.msjError("Ocurrió un error al cargar el reporte.");
            //////    }
            //////    else
            //////    {
            //////        auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
            //////    }
            //////}
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            
            Fg.IniciaControles(this, true); 
            
            chkAgrupaDisp.Enabled = true; 
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.SelectedIndex = 0;
            chkAgrupaDisp.Enabled = true; 

            FrameInsumos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;

            lst.LimpiarItems(); 
            rdoTpDispAmbos.Checked = true;

            dtsClavesProcesar = PreparaDtsClaves();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
           
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false; 
            btnExportarExcel.Enabled = false; 

            // FrameTransferencias.Enabled = false; 
            FrameInsumos.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            cboJurisdicciones.Enabled = false;
            chkAgrupaDisp.Enabled = false;

            lst.LimpiarItems(); 

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);
            this.Refresh(); 

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
               
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }
        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "COM_CONSUMOS_CLAVES_EDO" + "_" + cboEstados.Data;

            leerLocal.DataSetClase = dtsDispensacion;

            leerLocal.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leerLocal.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEstados.Text);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerLocal.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }
            ////bool bGenerar = false;
            //clsLeer leerToExcel = new clsLeer();
            //clsLeer leerPte = new clsLeer();
            //// int iColInicial = 0;
            //int iColActiva = 0;
            //// int iNumDias = 0;
            //string sNombreFile = "";
            //string sTituloPeriodo = "", sRutaReportes = "";

            //leerToExcel.DataSetClase = dtsDispensacion;

            //sRutaReportes = GnCompras.RutaReportes;
            //DtGeneral.RutaReportes = sRutaReportes;

            //sNombreFile = "COM_CONSUMOS_CLAVES_EDO" + "_" + cboEstados.Data + ".xls";
            //sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_CONSUMOS_CLAVES_EDO.xls";
            //DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_CONSUMOS_CLAVES_EDO.xls", DatosCliente);            

            //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //xpExcel.AgregarMarcaDeTiempo = true;

               

            //sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
            //////if (!chkDiaEspecificado.Checked)
            //{
            //    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + " " + dtpFechaInicial.Value.Year.ToString();
            //    sTituloPeriodo += " A " + General.FechaNombreMes(dtpFechaFinal.Value) + " " + dtpFechaFinal.Value.Year.ToString();
            //}


        //    int iRow = 9;
        //    // int iRowInicial = 9;

        //    if (xpExcel.PrepararPlantilla())
        //    {
        //        xpExcel.GeneraExcel();
        //        leerToExcel.Leer();

        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //        xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.EstadoConectadoNombre, 3, 2);
        //        xpExcel.Agregar("CONSUMO DE CLAVES. Periodo :  " + sTituloPeriodo, 4, 2);

        //        //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
        //        xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 7, 2);


        //        ////// Agregar columas 
        //        ////iColActiva = 1; 
        //        ////foreach (string sCol in leer.ColumnasNombre)
        //        ////{
        //        ////    iColActiva++; 
        //        ////    xpExcel.Agregar(sCol, iRow, iColActiva); 
        //        ////}


        //        leerToExcel.RegistroActual = 1;
        //        iRow++;
        //        while (leerToExcel.Leer())
        //        {
        //            //for (int i = 1; i <= leer.Columnas.Length; i++)
        //            iColActiva = 2;
        //            foreach (string sCol in leer.ColumnasNombre)
        //            {
        //                xpExcel.Agregar(leerToExcel.Campo(sCol), iRow, iColActiva);
        //                iColActiva++;
        //            }
                        
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
        #endregion Botones

        #region Grid 
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
                Error.GrabarError(leerLocal.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }

        private void ObtenerInformacion()
        {           
            bEjecutando = true;
             
            this.Cursor = Cursors.WaitCursor;           
            // bEjecutando = true; 
            int iTipoDispensacion = 0, iTipoInsumo = 0, iConcentrado = 0;

            iConcentrado = chkAgrupaDisp.Checked ? 1 : 0;    
            
            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispVenta.Checked)
                iTipoDispensacion = 1;

            if (rdoTpDispConsignacion.Checked)
                iTipoDispensacion = 2;

            if (rdoTpDispAmbos.Checked)
                iTipoDispensacion = 0;

            // Determinar que tipo de producto se mostrara 
            if (rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }


            string sSql = "";
            
            sSql = string.Format( 
                "Set Dateformat YMD " +
                " Exec  spp_Rpt_VentasPorClaveMensual @IdEstado = '{0}', @IdJurisdiccion = '{1}', @IdFarmacia = '*', @FechaInicial = '{2}', @FechaFinal = '{3}',  " +
                " @TipoDispensacion = '{4}', @TipoInsumo = '{5}', @AgrupaDispensacion = '{6}'  ", cboEstados.Data, cboJurisdicciones.Data, 
                Fg.PonCeros(dtpFechaInicial.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaInicial.Value.Month, 2) + "-01",
                Fg.PonCeros(dtpFechaFinal.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaFinal.Value.Month, 2) + "-01",
                iTipoDispensacion, iTipoInsumo, iConcentrado);


            

            lst.LimpiarItems();
            bSeEncontroInformacion = false;
            leerRemota = new clsLeer();
            leerRemota.Reset();
            leerRemota.DataSetClase = GetInformacionUnidad_Directo(sSql);
            if (leerRemota.SeEncontraronErrores())
            {
                Error.GrabarError(leerRemota, sSql, "ObtenerInformacion()", "");
            }
            else
            {                
                if (!leerRemota.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados");
                    bSeEjecuto = true;
                }
                else
                {
                    btnExportarExcel.Enabled = true;
                    bSeEncontroInformacion = true;
                    dtsDispensacion = leerRemota.DataSetClase; 
                    lst.CargarDatos(leerRemota.DataSetClase, true, true);

                    ActivarControles();
                }
            }

            bEjecutando = false;
            
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid                

        #region Eventos

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = false;

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

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboEstados.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboEstados.ItemActual.Item)["Servidor"].ToString();                
                CargarJurisdicciones();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                
            }
        }

        private void FrmDispensacionClaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                CargarEstados();
            }
        }
        #endregion Eventos

        #region Funciones

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            cboJurisdicciones.Enabled = true;
            chkAgrupaDisp.Enabled = true; // cboJurisdicciones.SelectedIndex == 0 ? true : false; 

            // FrameTransferencias.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameInsumos.Enabled = true;
            FrameFechas.Enabled = true;
        }

        private bool ClavesAProcesar()
        {
            bool bReturn = true;
            //////string sEdo = "", sFar = "", sIdClaveSSA = "", sClaveSSA = "";

            //////sEdo = cboEstados.Data;
            ////////sFar = cboFarmacias.Data;
            //////sFar = txtFarmacia.Text;

            //////dtsClavesProcesar = null;
            //////dtsClavesProcesar = PreparaDtsClaves();
            
            //////for (int i = 1; i <= Grid.Rows; i++)
            //////{
            //////    if (Grid.GetValueBool(i, 2))
            //////    {
            //////        sIdClaveSSA = Grid.GetValue(i, 1);
            //////        sClaveSSA = Grid.GetValue(i, 3);

            //////        object[] x = { sEdo, sFar, sIdClaveSSA, sClaveSSA };

            //////        dtsClavesProcesar.Tables[0].Rows.Add(x);
            //////        ////string sQuery = string.Format(" Insert Into CTE_ClavesAProcesar " +
            //////        ////                     " Select '{0}', '{1}', '{2}', '{3}', 'A',0 ", sEdo, sFar, sIdClaveSSA, sClaveSSA);
            //////        ////if (!leer.Exec(sQuery))
            //////        ////{
            //////        ////    Error.GrabarError(leer, "ClavesAProcesar()");
            //////        ////    General.msjError("Ocurrió un error al Insertar Claves a Procesar.");
            //////        ////    bReturn = false;
            //////        ////    break;
            //////        ////}
            //////        ////else
            //////        ////{
            //////        ////    bReturn = true;
            //////        ////}
            //////    }
            //////}

            //////if (dtsClavesProcesar == null)
            //////{
            //////    bReturn = false;
            //////}

            return bReturn;
        }

        private void CargarSubFarmacias()
        {
            //////SubFarmacias = new FrmListaDeSubFarmacias();
            //////SubFarmacias.AliasTabla = "L.";
            //////SubFarmacias.Estado = GnCompras.EstadoConectado;
            //////SubFarmacias.Farmacia = Fg.PonCeros(txtFarmacia.Text, 4);
            //////SubFarmacias.EsParaSP = true;
            //////SubFarmacias.MostrarDetalle();
            //////sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
        }

        public static DataSet PreparaDtsClaves()
        {
            // Nombres de columnas no mayores de 10 caracteres 
            DataSet dts = new DataSet();
            DataTable dtClave = new DataTable("ClavesSSA");

            dtClave.Columns.Add("IdEstado", Type.GetType("System.String"));
            dtClave.Columns.Add("IdFarmacia", Type.GetType("System.String"));
            dtClave.Columns.Add("IdClaveSSA", Type.GetType("System.String"));
            dtClave.Columns.Add("ClaveSSA", Type.GetType("System.String"));
            dts.Tables.Add(dtClave);
            
            return dts.Clone();
        } 
        #endregion Funciones        

        #region Funciones_Conexion
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
        #endregion Funciones_Conexion
    } 
}
