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

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmCostoPacienteProgramaDeAtencion : FrmBaseExt 
    {
        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;       

        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        DataSet dtsDispensacion = new DataSet();

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet();
        DataSet dtsClaves = new DataSet();

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        string sEstado = DtGeneralPedidos.EstadoConectado;
        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        public FrmCostoPacienteProgramaDeAtencion()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ReportesFacturacionUnidad");
            //leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);

            // Se crea la instancia del objeto de la clase de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);            
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada; 


            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;            
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
            // Cargar Jurisdicciones
            CargaTipoUnidades();
            CargarJurisdicciones(); 

            btnNuevo_Click(null, null);            
        }

        #region Cargar Combos 
        private void CargaTipoUnidades()
        {
            cboTipoUnidad.Clear();
            cboTipoUnidad.Add("*", "Todos los tipos de unidades");
            cboTipoUnidad.Add(Consultas.TipoUnidades("", "CargaTipoUnidades"), true, "IdTipoUnidad", "Descripcion");
            cboTipoUnidad.SelectedIndex = 0;
        }

        private void CargarMunicipios()
        {
            string sSql = "", sWhereJuris = "";

            if (cboJurisdicciones.Data != "*")
            {
                sWhereJuris = string.Format(" and IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            cboLocalidad.Clear();
            cboLocalidad.Add("*", "Todos los Municipios");

            sSql = string.Format(" Select Distinct IdMunicipio, (IdMunicipio + ' -- ' + Municipio) As Municipio From vw_Farmacias (Nolock) " +
                                 " Where IdEstado = '{0}'  {1}  Order By IdMunicipio ", sEstado, sWhereJuris);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarMunicipios");
                General.msjError("Ocurrió un error al obtener los Municipios.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboLocalidad.Add(leer.DataSetClase, true, "IdMunicipio", "Municipio");
                }
            }

            cboLocalidad.SelectedIndex = 0;
        }

        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "Todas las jurisdicciones");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            string sSql = "", sWhereJuris = "", sWhereMun = "", sWhereTipoUnidad = "";

            if (cboTipoUnidad.Data != "*")
            {
                sWhereTipoUnidad = string.Format(" and  IdTipoUnidad = '{0}' ", cboTipoUnidad.Data);
            }

            if (cboJurisdicciones.Data != "*")
            {
                sWhereJuris = string.Format(" and IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            if (cboLocalidad.Data != "*")
            {
                sWhereMun = string.Format(" and IdMunicipio = '{0}' ", cboLocalidad.Data);
            }

            cboFarmacia.Clear();
            cboFarmacia.Add("*", "Todas las Farmacias");

            sSql = string.Format(" Select IdFarmacia, (IdFarmacia + ' -- ' + Farmacia) As Farmacia From vw_Farmacias (Nolock) " +
                                 " Where IdEstado = '{0}' and Status = 'A' and IdTipoUnidad Not In ('000', '005', '006')  {1}  {2}  {3} Order By IdFarmacia ",
                                 sEstado, sWhereJuris, sWhereMun, sWhereTipoUnidad);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFarmacias");
                General.msjError("Ocurrió un error al obtener las Farmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboFarmacia.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
                }
            }

            cboFarmacia.SelectedIndex = 0;
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

            ////if (bRegresa && cboReporte.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            ////    cboReporte.Focus(); 
            ////}

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
            //////    //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

            //////    myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;

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
            //////        bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 

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
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true);
           
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;
            cboJurisdicciones.Enabled = bValor;        
                                   
            FrameFechas.Enabled = bValor;
            rdoConcentradoFarmacias.Checked = true; 

            cboTipoUnidad.SelectedIndex = 0;
            cboJurisdicciones.SelectedIndex = 0;

            cboLocalidad.Clear();
            cboLocalidad.Add("*", "Todos los Municipios");
            cboLocalidad.SelectedIndex = 0;

            cboFarmacia.Clear();
            cboFarmacia.Add("*", "Todas las Farmacias");
            cboFarmacia.SelectedIndex = 0;

            lst.LimpiarItems();            
            rdoClaves.Checked = true;       

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
           
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false; 
            btnExportarExcel.Enabled = false;

            FrameParametros.Enabled = false;
            //FrameInsumos.Enabled = false;            
            FrameFechas.Enabled = false;
            cboJurisdicciones.Enabled = false;            

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
            //if (rdoConcentradoProgramaDeAtencion.Checked)
            //{
            //    ExportarExcel__ProgramaDeAtencion(); 
            //}
            //else
            //{
            //    ExportarExcel__ConcentradoPorFarmacia(); 
            //}
        }
        /*
        private void ExportarExcel__ProgramaDeAtencion()
        { 
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            
            int iColActiva = 0;            
            string sTituloPeriodo = "", sTituloReporte = "", sMunicipio = "", sTipoUnidad = "" ;

            leerToExcel.DataSetClase = dtsDispensacion;

            sMunicipio = " MUNICIPIO : " + cboLocalidad.Text;
            sTipoUnidad = " TIPO UNIDAD :  " + cboTipoUnidad.Text;

            if (cboLocalidad.SelectedIndex != 0)
            {
                sMunicipio = " MUNICIPIO :  " + cboLocalidad.ItemActual.GetItem("Municipio").ToString();
            }

            if (cboTipoUnidad.SelectedIndex != 0)
            {
                sTipoUnidad = " TIPO UNIDAD :  " + cboTipoUnidad.ItemActual.GetItem("Descripcion").ToString(); 
            }


            sTituloReporte = " COSTOS DE BENEFICIARIOS POR PROGRAMA DE ATENCION  ";            

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.Salidas_Costos_Programas_Atencion, "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + " " + dtpFechaInicial.Value.Year.ToString();
                    sTituloPeriodo += " A " + General.FechaNombreMes(dtpFechaFinal.Value) + " " + dtpFechaFinal.Value.Year.ToString(); 
                }

                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();
                    
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 2, 2);
                    xpExcel.Agregar(sTituloReporte + sTituloPeriodo, 3, 2);
                    xpExcel.Agregar(sMunicipio, 4, 2);
                    xpExcel.Agregar(sTipoUnidad, 5, 2);
                   
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 7, 2);                   
                    
                    leerToExcel.RegistroActual = 1;
                    iRow++; 
                    while (leerToExcel.Leer())
                    {
                        //for (int i = 1; i <= leer.Columnas.Length; i++)
                        iColActiva = 2;
                        foreach (string sCol in leer.ColumnasNombre)
                        {
                            xpExcel.Agregar(leerToExcel.Campo(sCol), iRow, iColActiva);
                            iColActiva++;
                        }                        
                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }
        }

        private void ExportarExcel__ConcentradoPorFarmacia()
        {
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();

            int iColActiva = 0;
            string sTituloPeriodo = "", sTituloReporte = "", sMunicipio = "", sTipoUnidad = "";

            leerToExcel.DataSetClase = dtsDispensacion;

            sMunicipio = " MUNICIPIO : " + cboLocalidad.Text;
            sTipoUnidad = " TIPO UNIDAD :  " + cboTipoUnidad.Text;

            if (cboLocalidad.SelectedIndex != 0)
            {
                sMunicipio = " MUNICIPIO :  " + cboLocalidad.ItemActual.GetItem("Municipio").ToString();
            }

            if (cboTipoUnidad.SelectedIndex != 0)
            {
                sTipoUnidad = " TIPO UNIDAD :  " + cboTipoUnidad.ItemActual.GetItem("Descripcion").ToString();
            }


            sTituloReporte = " COSTOS DE BENEFICIARIOS CONCENTRADO POR FARMACIA  ";

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.Salidas_Costos_Programas_Atencion_Concentrado_Farmacia, "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + " " + dtpFechaInicial.Value.Year.ToString();
                    sTituloPeriodo += " A " + General.FechaNombreMes(dtpFechaFinal.Value) + " " + dtpFechaFinal.Value.Year.ToString();
                }

                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 2, 2);
                    xpExcel.Agregar(sTituloReporte + sTituloPeriodo, 3, 2);
                    xpExcel.Agregar(sMunicipio, 4, 2);
                    xpExcel.Agregar(sTipoUnidad, 5, 2);

                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 7, 2);

                    leerToExcel.RegistroActual = 1;
                    iRow++;
                    while (leerToExcel.Leer())
                    {
                        //for (int i = 1; i <= leer.Columnas.Length; i++)
                        iColActiva = 2;
                        foreach (string sCol in leer.ColumnasNombre)
                        {
                            xpExcel.Agregar(leerToExcel.Campo(sCol), iRow, iColActiva);
                            iColActiva++;
                        }
                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
            }
        }
        */
        #endregion Botones

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            ////try
            ////{
            ////    //leerWeb = new clsLeerWeb(sUrl, "SII_Unidad", DatosCliente); 
            ////    leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

            ////    conexionWeb.Url = sUrl;
            ////    //DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx("SII_Unidad"));
            ////    DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

            ////    DatosDeConexion.Servidor = sHost;
            ////    bRegresa = true; 
            ////}
            ////catch (Exception ex1)
            ////{
            ////    Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
            ////    General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
            ////    ActivarControles(); 
            ////}

            return bRegresa; 
        }

        private void ObtenerInformacion()
        {           
            bEjecutando = true;  
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";
            string sSql = "";
            int iTipoReporte = 1;

            iTipoReporte = rdoConcentradoProgramaDeAtencion.Checked ? 2 : 1; 
            sSql = string.Format( 
                "Set Dateformat YMD " +
                " Exec  spp_Rpt_CostosBeneficiarios_SalidasClavesMensual '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'  ", 
                DtGeneralPedidos.EstadoConectado, cboJurisdicciones.Data, cboFarmacia.Data, cboLocalidad.Data, cboTipoUnidad.Data,
                Fg.PonCeros(dtpFechaInicial.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaInicial.Value.Month, 2) + "-01",
                Fg.PonCeros(dtpFechaFinal.Value.Year, 4) + "-" + Fg.PonCeros(dtpFechaFinal.Value.Month, 2) + "-01",
                iTipoReporte);                          

            lst.Limpiar();
            bSeEncontroInformacion = false; 
            if (!leer.Exec(sSql))
            {                
                Error.GrabarError(leer, "ObtenerInformacion()");                
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo."); 
            }
            else
            {                
                if (!leer.Leer())
                {
                    //General.msjUser("No se encontro información con los criterios especificados");
                    bSeEjecuto = true;
                }
                else
                {
                    btnExportarExcel.Enabled = true; 
                    dtsDispensacion = leer.DataSetClase; 
                    lst.CargarDatos(leer.DataSetClase, true, true); 
                }
            }

            bEjecutando = false; 

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosReg(sCadena, General.Url);           

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

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                ////////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                ////////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                //// cboFarmacias.Enabled = false;
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

            FrameParametros.Enabled = true;
            //FrameInsumos.Enabled = true;
            FrameFechas.Enabled = true;
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

        #region Eventos_Combo
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {            
            //if (cboJurisdicciones.SelectedIndex != 0)
            {
                CargarMunicipios();
            }
        }

        private void cboLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboLocalidad.SelectedIndex != 0)
            {
                CargarFarmacias();
            }
        }

        private void cboTipoUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboTipoUnidad.SelectedIndex != 0)
            {
                CargarFarmacias();
            }
        }
        #endregion Eventos_Combo
    } 
}
