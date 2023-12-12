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

namespace DllPedidosClientes.ReportesCentral
{
    public partial class FrmTBC_SurtimientoInsumosNoCauses_Regional : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        // clsGrid Grid;

        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        DataSet dtsDispensacion = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        string sIdEstado = DtGeneralPedidos.EstadoConectado;

        // string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        // string sSubFarmacias = "", sTablaClavesSSA = "";

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
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
        clsAuditoria auditoria;

        public FrmTBC_SurtimientoInsumosNoCauses_Regional()
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
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
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
            //lst.PermitirAjusteDeColumnas = false; 
            
            ObtenerFarmacias();
            CargarJurisdicciones();
            

        }

        private void CargarListaReportes()
        {
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        #region Cargar Combos
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                // cboJurisdicciones.Add("*", "Todas las jurisdicciones"); 
                cboJurisdicciones.Add("0", "<< TODAS LAS JURISDICCIONES >>");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias(sIdEstado, "ObtenerFarmacias");
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("Status = 'A' And IdEstado = '{0}' ", sIdEstado);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");

            if (cboJurisdicciones.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            try
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }
            


            cboFarmacias.SelectedIndex = 0;
        }
        #endregion Cargar Combos

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
            ////bool bRegresa = false;
        }
        #endregion Impresion

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true);
            // rdoTE.Checked = true;

            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            cboJurisdicciones.Enabled = bValor;
            cboFarmacias.Enabled = bValor;

            FrameFechas.Enabled = bValor;

            lst.LimpiarItems();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            // int iColInicial = 0;
            int iColActiva = 0;
            // int iNumDias = 0;
            string sTituloPeriodo = "";

            leerToExcel.DataSetClase = dtsDispensacion; 
            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.EdoJuris_TBC_Surtimiento_NoCauses, "PLANTILLA_NoCauses");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value); 
                sTituloPeriodo += " al " + General.FechaYMD(dtpFechaFinal.Value); 

                ////if (!chkDiaEspecificado.Checked)
                ////{
                ////    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                ////}


                int iRow = 8;
                // int iRowInicial = 8;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    xpExcel.Agregar("Surtimiento de insumos NO CAUSES del " + sTituloPeriodo, 4, 2);

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    iRow++;
                    while (leerToExcel.Leer())
                    {
                        //for (int i = 1; i <= leer.Columnas.Length; i++)
                        iColActiva = 2;
                        foreach (string sCol in leerToExcel.ColumnasNombre)
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
            */
        }
        #endregion Botones

        #region Grid
        //private bool validarDatosDeConexion()
        //{
        //    bool bRegresa = false;

        //    try
        //    {
        //        //leerWeb = new clsLeerWeb(sUrl, "SII_Unidad", DatosCliente); 
        //        leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

        //        conexionWeb.Url = sUrl;
        //        //DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx("SII_Unidad"));
        //        DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

        //        DatosDeConexion.Servidor = sHost;
        //        bRegresa = true;
        //    }
        //    catch (Exception ex1)
        //    {
        //        Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
        //        General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
        //        ////ActivarControles(); 
        //    }

        //    return bRegresa;
        //}
        private void IniciarConsulta()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;

            // FrameTransferencias.Enabled = false; 
            FrameFechas.Enabled = false;
            cboJurisdicciones.Enabled = false;
            cboFarmacias.Enabled = false;

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

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            // string sStore = "", sStoreVales = "";
            // tring sTabla = "";
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "", sIdJurisdiccion = "*", sIdFarmacia = "*";
            // bEjecutando = true; 
            // int iTipoDispensacion = 0, iTipoInsumo = 0, iConcentrado = 0;

            if (cboJurisdicciones.Data != "0")
            {
                sIdJurisdiccion = cboJurisdicciones.Data;
            }

            if (cboFarmacias.Data != "0")
            {
                sIdFarmacia = cboFarmacias.Data;
            }

            string sSql = "";
            sSql = string.Format(
                "Set Dateformat YMD " +
                " Exec  spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                sIdEstado, sIdJurisdiccion, sIdFarmacia, DtGeneralPedidos.Cliente, dtpFechaInicial.Text, dtpFechaFinal.Text); 


            lst.LimpiarItems();
            bSeEncontroInformacion = false;

            try
            {
                leerLocal = new clsLeer();
                leerLocal.Reset();
                leerLocal.DataSetClase = GetInformacion(sSql);


                //if (!leer.Exec(sSql))
                if (leerLocal.SeEncontraronErrores())
                {
                    Error.GrabarError(leerLocal, "ObtenerInformacion()");
                    General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
                }
                else
                {
                    if (!leerLocal.Leer())
                    {
                        bSeEjecuto = true;
                        lst.CargarDatos(leerLocal.DataSetClase, true, true);
                    }
                    else
                    {
                        dtsDispensacion = leerLocal.DataSetClase;
                        lst.CargarDatos(leerLocal.DataSetClase, true, true);
                        btnExportarExcel.Enabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
                bSeEjecuto = false;
                bSeEncontroInformacion = false;
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

        private void FrmTBC_SurtimientoInsumosNoCauses_Regional_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            CargarFarmacias();
        }
        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
        }

        private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
        }
        #endregion Eventos

        #region Funciones
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            cboJurisdicciones.Enabled = true;
            cboFarmacias.Enabled = true;
            FrameFechas.Enabled = true;
        }

        private bool VerificarClavesProveedor()
        {
            bool bRegresa = false;
            string sSql = "Select Top 1 * From vw_ClavesSSA_Proveedores_Externos(NoLock) Where Status_Clave = 'A' ";
                
            if (!leerLocal.Exec(sSql))
            {
                Error.GrabarError(leerLocal, "VerificarClavesProveedor()");
                General.msjAviso("Ocurrió un error al obtener las claves activas de los proveedores. Verifique.");
            }
            else
            {
                if (leerLocal.Leer())
                {
                    bRegresa = true;
                }
                else
                {
                    General.msjUser("No existen Claves activas de ningun proveedor. Verifique.");                    
                }
            }         

            return bRegresa;
        }
        #endregion Funciones

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            dts = GetInformacionRegional(Cadena);

            ////switch (DtGeneralPedidos.TipoDeConexion)
            ////{
            ////    case TipoDeConexion.Regional:
            ////        dts = GetInformacionRegional(Cadena);
            ////        break;

            ////    case TipoDeConexion.Unidad:
            ////        dts = GetInformacionUnidad(Cadena);
            ////        break;

            ////    case TipoDeConexion.Unidad_Directo:
            ////        dts = GetInformacionUnidad_Directo(Cadena);
            ////        break;

            ////    default:
            ////        break;
            ////}

            return dts;
        }

        private DataSet GetInformacionRegional__OLD(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url;
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            clsConexionSQL cnnLocal = new clsConexionSQL();
            cnnLocal.DatosConexion.Servidor = cnn.DatosConexion.Servidor;
            cnnLocal.DatosConexion.BaseDeDatos = cnn.DatosConexion.BaseDeDatos;
            cnnLocal.DatosConexion.Usuario = cnn.DatosConexion.Usuario;
            cnnLocal.DatosConexion.Password = cnn.DatosConexion.Password;
            cnnLocal.DatosConexion.Puerto = cnn.DatosConexion.Puerto;
            cnnLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            clsLeer leerRegional = new clsLeer(ref cnnLocal);
            clsLeerWeb leerWeb = new clsLeerWeb(General.Url, DtGeneralPedidos.CfgIniSII_Regional, DatosCliente);

            // leerRegional.Exec(Cadena);
            // dts = leerRegional.DataSetClase;

            leerWeb.Exec(Cadena);
            dts = leerWeb.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url;
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
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

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = true;
                General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
                cboJurisdicciones.Focus(); 
            }

            return bRegresa; 
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                ////////leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                ////////conexionWeb.Url = sUrl;
                ////////DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                ////////DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }
        #endregion Conexiones

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            tmSesion.Enabled = false;           
            if (!VerificarClavesProveedor())
            {
                this.Close();
            }            
        }

        
    }
}