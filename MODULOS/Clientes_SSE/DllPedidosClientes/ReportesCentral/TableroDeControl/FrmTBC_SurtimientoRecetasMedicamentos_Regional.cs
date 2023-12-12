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
    public partial class FrmTBC_SurtimientoRecetasMedicamentos_Regional : FrmBaseExt
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

        public FrmTBC_SurtimientoRecetasMedicamentos_Regional()
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


            //CargarListaReportes();
            //CargarClavesRegistradas(); 
        }

        private void CargarListaReportes()
        {
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            // Cargar Estados y Jurisdicciones 
            CargarEstados();
            CargarJurisdicciones();

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
            }

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
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

            FrameFechas.Enabled = bValor;

            lst.LimpiarItems();

            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
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
            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.EdoJuris_TBC_Surtimiento, "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                }


                int iRow = 8;
                // int iRowInicial = 8;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    xpExcel.Agregar("Surtimiento de insumos " + sTituloPeriodo, 4, 2);

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
            // string sTabla = "";
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";
            // bEjecutando = true; 
            // int iTipoDispensacion = 0, iTipoInsumo = 0, iConcentrado = 0;

            string sSql = "";
            sSql = string.Format(
                "Set Dateformat YMD " +
                " Exec  spp_Rpt_EdoJuris_SurtimientoRecetas '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                cboEstados.Data, cboJurisdicciones.Data, "0002", dtpFechaInicial.Text, dtpFechaFinal.Text);


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

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
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

        private void FrmTBC_SurtimientoRecetasMedicamentos_Regional_KeyDown(object sender, KeyEventArgs e)
        {
        }

        #endregion Eventos

        #region Funciones
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;
            cboJurisdicciones.Enabled = true;
            FrameFechas.Enabled = true;
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
    }
}