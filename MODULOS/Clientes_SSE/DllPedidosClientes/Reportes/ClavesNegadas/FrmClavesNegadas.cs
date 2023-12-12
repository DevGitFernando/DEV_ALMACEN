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
    public partial class FrmClavesNegadas : FrmBaseExt
    {
        ////enum Cols
        ////{
        ////    IdJuris = 1, Jurisdiccion = 2, IdFarmacia = 3, Farmacia = 4,
        ////    ClaveSSA = 5, Descripcion = 6, Presentacion = 7, PrecioLicitacion = 8, Año = 9, Mes = 10
        ////}

        ////enum ColsWith
        ////{
        ////    IdJuris = 70, Jurisdiccion = 180, ClaveSSA = 120, Descripcion = 300, Presentacion = 120, PrecioLicitacion = 120, Año = 40, Mes = 50,
        ////    Dias = 100, Total = 120
        ////}

        enum Cols
        {
            ClaveSSA = 1, Descripcion = 2, Presentacion = 3, PrecioLicitacion = 4, Año = 5, Mes = 6  
        }

        enum ColsWith
        {
            ClaveSSA = 120, Descripcion = 300, Presentacion = 120, PrecioLicitacion = 120, Año = 40, Mes = 50, 
            Dias = 80, Total = 100 
        }

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        // clsGrid Grid;
        // clsGrid gridClaves;
        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;

        // string sSqlFarmacias = "";
        string sUrl = "";
        string sHost = "";
        string sTablaFarmacia = "";
        int iDiasPeriodo = 0;

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;
                        
        DataSet dtsEstados = new DataSet();
        DataSet dtsClavesNegadas = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        public FrmClavesNegadas()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ConsultaClavesNegadas");
            leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


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
  
            ////// Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;
            //lst.PermitirAjusteDeColumnas = false; 
        }

        private void FrmExistenciaFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos         

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

                //////string sSql = "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";
                //////if (!leerWeb.Exec(sSql))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarEstados()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                //////}
                //////else
                //////{
                //////    cboEstados.Add(leerWeb.DataSetClase, true, "IdEstado", "Estado"); 
                //////}
            }

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
        }

        private void CargarFarmacias()
        {
            if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                ////////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                ////////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                ////////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                //////sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                //////                " From vw_Farmacias_Urls U (NoLock) " +
                //////                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                //////                " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                //////                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                //////                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                ////////Grid.Limpiar(false);

                //////if (!leerWeb.Exec(sSqlFarmacias))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                //////}
                //////else
                //////{
                //////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                //////    //Grid.LlenarGrid(leer.DataSetClase);
                //////    //grdReporte.Focus();
                //////}
            }
            cboFarmacias.SelectedIndex = 0;
            
        }

        #endregion Cargar Combos          

        #region Impresion      

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
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sEstado = cboEstados.Data;
                string sFarmacia = txtFarmacia.Text;                 

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

                if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad_Directo)
                {
                    myRpt = new clsImprimir(DatosDeConexion);
                }

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = " ";

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();
                bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 

               
                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    auditoria.GuardarAud_MovtosReg("Reporte ==> " + myRpt.NombreReporte, General.Url);
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            dtsClavesNegadas = new DataSet();

            Fg.IniciaControles(this, true);

            // gridClaves.Limpiar(false); 
            // gridClaves.Reset(); 
            lst.Limpiar();
            lst.LimpiarItems(); 

            btnExportarExcel.Enabled = false; 
            btnEjecutar.Enabled = true;
            
            CargarEstados();
             
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
            {
                IniciarProcesamiento();
            }
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false; 
            btnExportarExcel.Enabled = false; 

            //cboFarmacias.Enabled = false;
            txtFarmacia.Enabled = false;
            lblClaves.Text = "";
            lblPiezas.Text = ""; 

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "Generando_ClavesNegadas";
            _workerThread.Start();
               
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }

        private ListaPlantillas Plantilla_A_Generar()
        {
            ListaPlantillas myPlantilla = ListaPlantillas.SurmientoClavesDispensada;
            iDiasPeriodo = General.FechaDiasMes(dtpFechaInicial.Value);

            switch (iDiasPeriodo)
            {
                case 28:
                    myPlantilla = ListaPlantillas.ClavesNegadas_28;
                    break;

                case 29:
                    myPlantilla = ListaPlantillas.ClavesNegadas_29;
                    break;

                case 30:
                    myPlantilla = ListaPlantillas.ClavesNegadas_30;
                    break;

                case 31:
                    myPlantilla = ListaPlantillas.ClavesNegadas_31;
                    break; 
            }

            return myPlantilla;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            // int iColInicial = 0;
            int iColActiva = 0; 
            int iNumDias = 0;
            string sTituloPeriodo = ""; 

            leerPte.DataSetClase = dtsClavesNegadas;
            leerToExcel.DataSetClase = dtsClavesNegadas;

            bGenerar = GnPlantillas.GenerarPlantilla(Plantilla_A_Generar(), "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value); 
                if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString(); 
                }


                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2); 
                    xpExcel.Agregar(lblFarmacia.Text, 3, 2);
                    xpExcel.Agregar("Analísis de claves negadas de " + sTituloPeriodo, 4, 2); 

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("ClaveSSA"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("DescripcionClave"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("Presentacion"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("PrecioLicitacion"), iRow, 5); 
                        xpExcel.Agregar(leerToExcel.Campo("Año"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("Mes"), iRow, 7);

                        iNumDias = iDiasPeriodo;
                        iColActiva = 8; 
                        for (int i = 1; i <= iNumDias; i++)
                        {
                            xpExcel.Agregar(leerToExcel.Campo(i.ToString()), iRow, iColActiva);
                            iColActiva++; 
                        }
                        xpExcel.Agregar(leerToExcel.Campo("Total"), iRow, iColActiva);
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
            */
        }
        #endregion Botones        

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            switch (DtGeneralPedidos.TipoDeConexion)
            {
                case TipoDeConexion.Regional:
                    dts = GetInformacionRegional(Cadena);
                    break;

                case TipoDeConexion.Unidad:
                    dts = GetInformacionUnidad(Cadena);
                    break;

                case TipoDeConexion.Unidad_Directo:
                    dts = GetInformacionUnidad_Directo(Cadena);
                    break;

                default:
                    break;
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                conexionWeb.Url = General.Url;
                dts = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

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

        #region Grid        
        private void ObtenerInformacion()
        {           
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            clsLeer leerResultado = new clsLeer();            
            string sCadena = "";

            int iOpcion = 0;
            iOpcion = chkDiaEspecificado.Checked ? 1 : 0;

            sTablaFarmacia = " CTE_FarmaciasProcesar ";

            string sSql = string.Format(" Exec spp_Rpt_CteReg_ListaClavesNegadas '{0}', '{1}', '{2}', '{3}' ", 
                cboEstados.Data, txtFarmacia.Text, General.FechaYMD(dtpFechaInicial.Value), iOpcion );

            sSql = string.Format(" Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                            cboEstados.Data, "*", txtFarmacia.Text, General.FechaYMD(dtpFechaInicial.Value), 0, iOpcion, 0); 

            //sSql += "\n " + string.Format("Select top 1 * From tmpRptAbastoClaves (NoLock) ");

            ////sSql += "\n " + string.Format(" Select top 1 Count(*) As TotalClaves, " + 
            ////                            " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, " + 
            ////                            " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, " +
            ////                            " PorcAbasto From tmpRptAbastoClaves (Nolock) Group By PorcAbasto");

            ////try 
            ////{
            ////    leer.Reset();
            ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, sSql, "reporte", sTablaFarmacia); 
            ////}
            ////catch (Exception ex)
            ////{ 
            ////} 

            leer.Reset();
            leer.DataSetClase = GetInformacion(sSql); 
            if (leer.SeEncontraronErrores())
            {
                btnExportarExcel.Enabled = false; 
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            { 
                btnExportarExcel.Enabled = true; 
                bSeEncontroInformacion = true;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    bSeEjecuto = true;
                }
                else
                {
                    dtsClavesNegadas = leer.DataSetClase; 
                    // gridClaves.LlenarGrid(leer.DataSetClase, true, true);
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    leerResultado.DataTableClase = leer.Tabla(2);
                    leerResultado.Leer();

                    lblClaves.Text = leerResultado.CampoInt("Claves").ToString();
                    lblPiezas.Text = leerResultado.CampoDouble("TotalPiezas").ToString("#,###,###,##0"); 
                }
            }

            //gridClaves.BloqueaGrid(true); 
            AjustarColumnas(); 
            bEjecutando = false;

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosReg(sCadena, General.Url);

            this.Cursor = Cursors.Default;
        } 

        #endregion Grid 

        #region Eventos
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default; 
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true; 
            //cboFarmacias.Enabled = true;
            txtFarmacia.Enabled = true; 
        } 

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
                cboEstados.Enabled = false;                
                CargarFarmacias();
            }
        }        

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;
            string sEdo = "", sFar = "";
            sEdo = cboEstados.Data;

            string sSql = string.Format("Delete From CteReg_Farmacias_Procesar_Existencia ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "FarmaciasAProcesar()");
                General.msjError("Ocurrió un error al borrar tabla.");
                bReturn = false;
            }
            else
            {
                //for (int i = 1; i <= Grid.Rows; i++)
                //{
                //    if( Grid.GetValueBool(i,3) )
                //    {
                sFar = txtFarmacia.Text; //Grid.GetValue(i,1);

                string sQuery = string.Format(" Insert Into CteReg_Farmacias_Procesar_Existencia " +
                                    " Select '{0}','{1}','A',0 ", sEdo, sFar );
                if (!leer.Exec(sQuery))
                {
                    Error.GrabarError(leer, "FarmaciasAProcesar()");
                    General.msjError("Ocurrió un error al Insertar Farmacias a Procesar.");
                    bReturn = false;
                    //break;
                }
                else
                {
                    bReturn = true;
                }
                //    }
                //}
            }

            return bReturn;
        }       

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //Grid.SetValue(3, chkTodos.Checked);
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                ////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                ////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                //// cboFarmacias.Enabled = false;
            }
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

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
        #endregion Eventos        

        #region Eventos_Farmacia
        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {            
            if (txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias_UrlsActivas(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");

                    sUrl = leer.Campo("Url");
                    sHost = leer.Campo("Servidor");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }

        private void txtFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Farmacias_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data);

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }
        #endregion Eventos_Farmacia

        #region Funciones y Procedimientos Privados
        private void AjustarColumnas()
        {

            lst.TituloColumna((int)Cols.ClaveSSA, "Clave SSA");
            lst.TituloColumna((int)Cols.Descripcion, "Descripción clave"); 
            lst.TituloColumna((int)Cols.PrecioLicitacion, "Precio licitación"); 

            lst.AnchoColumna((int)Cols.ClaveSSA, (int)ColsWith.ClaveSSA);
            lst.AnchoColumna((int)Cols.Descripcion, 400);
            lst.AnchoColumna((int)Cols.Presentacion, (int)ColsWith.Presentacion);
            lst.AnchoColumna((int)Cols.PrecioLicitacion, (int)ColsWith.PrecioLicitacion);
            lst.AnchoColumna((int)Cols.Año, (int)ColsWith.Año);
            lst.AnchoColumna((int)Cols.Mes, (int)ColsWith.Mes);

            for (int i = ((int)Cols.Mes + 1); i <= lst.Columnas + 1; i++)
            {
                lst.AnchoColumna(i, (int)50);
            }

            lst.AnchoColumna(lst.Columnas, 100);
        }
        #endregion Funciones y Procedimientos Privados
    } 
}
