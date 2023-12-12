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
    public partial class FrmProductosMovimientos_Secretaria : FrmBaseExt
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
        string sIdEstado = DtGeneralPedidos.EstadoConectado;

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

        Color colorBack_01 = Color.Lavender;
        Color colorBack_02 = Color.LightBlue;  

        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA= 1, Descripcion = 2, InventarioInicial = 3, EntradasDisur = 4, EntradasFarmaco = 5, 
            VentasDisur = 6, VentasFarmaco = 7
        }

        public FrmProductosMovimientos_Secretaria()
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
        }

        private void CargarListaReportes()
        {
        }

        private void FrmReportesFacturacion_Load(object sender, EventArgs e)
        {
            // Cargar Estados y Jurisdicciones 
            CargarEstados();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true);
            FrameTipo.Enabled = true;
            FramePeriodo.Enabled = true;
            IniciaToolBar(true, true, false);
            lst.LimpiarItems();

            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;

            rdoTodos_Producto.Checked = true;
            rdoFecha.Checked = true;

            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta();
        }

        ////private void btnImprimir_Click(object sender, EventArgs e)
        ////{
        ////    Imprimir();
        ////}

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();
            int iColActiva = 0;
            int iAño = dtpFecha.Value.Year;
            string sMes = General.FechaNombreMes(dtpFecha.Value);
            string sTipoReporte = string.Format("Movimientos de Medicamento y Material de Curación del Mes de {0} del {1}", sMes, iAño);

            if (!rdoTodos_Producto.Checked)
            {
                if (rdoMaterial.Checked)
                {
                    sTipoReporte = string.Format("Movimientos de Material de Curación del Mes de {0} del {1}", sMes, iAño);
                }
                else
                {
                    sTipoReporte = string.Format("Movimientos de Medicamento del Mes de {0} del {1}", sMes, iAño);
                }
            }            

            leerToExcel.DataSetClase = dtsDispensacion; 
            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.ProductosMovimientos_Secretaria, "PLANTILLA_Movimientos");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                int iRow = 8;
                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    xpExcel.Agregar(sTipoReporte, 4, 2);
                    xpExcel.Agregar(General.FechaSistema.ToLongDateString().ToUpper(), 6, 3);

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

        private void rdoFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
        }

        private void rdoTodos_Fecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFecha.Enabled = false;
        }
        #endregion Eventos

        #region Funciones
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            IniciaToolBar(true, true, false);
            FrameTipo.Enabled = true;
            FramePeriodo.Enabled = true;
        }

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
        private void IniciarConsulta()
        {
            bSeEncontroInformacion = false;
            IniciaToolBar(false, false, false);
            FrameTipo.Enabled = false;
            FramePeriodo.Enabled = false;

            // FrameTransferencias.Enabled = false; 
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
            string sCadena = "", sSql = "";
            int iMes = 0, iAño = 0, iTipoDeClave = 0;

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            if (rdoFecha.Checked)
            {
                //Reporte por Periodo. 
                iMes = dtpFecha.Value.Month;
                iAño = dtpFecha.Value.Year;
            }

            if (!rdoTodos_Producto.Checked)
            {
                if (rdoMaterial.Checked)
                {
                    iTipoDeClave = 1;
                }
                else
                {
                    iTipoDeClave = 2;
                }
            }

            sSql = string.Format("Exec spp_Rpt_CteReg_Productos_Movimientos_Secretaria_Datos '{0}', {1}, {2}, {3}, 1, '' ",
                cboEstados.Data, iAño, iMes, iTipoDeClave);

            lst.LimpiarItems();
            bSeEncontroInformacion = false;

            try
            {
                //////leerLocal = new clsLeer();
                //////leerLocal.Reset();
                //////leerLocal.DataSetClase = GetInformacion(sSql);

                //if (leerLocal.SeEncontraronErrores())
                if (!leerLocal.Exec(sSql))
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
                        lst.AnchoColumna((int)Cols.Descripcion, 300);
                        lst.AlternarColorRenglones(colorBack_01, colorBack_02);
                        IniciaToolBar(true, false, true);
                        bSeEncontroInformacion = true;
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
        #endregion Funciones

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