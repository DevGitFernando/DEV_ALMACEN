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
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmProximosACaducar : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerWeb leerWeb;
        clsConsultas query;
        clsAyudas ayuda;
        // DataSet dtsFarmacias;
        // clsConexionSQL cnnUnidad;
        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;
        clsListView lst;

        // string sSqlFarmacias = "";
        string sUrl = "";
        string sHost = "";
        string sTablaFarmacia = "";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        private bool bLimpiar = true;

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 1, Descripcion = 2, Existencia = 3
        }

        public FrmProximosACaducar()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            cnn.SetConnectionString();
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneralPedidos.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente); 

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;

            CargarEstados();
            CargarFarmacias();
        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            if (bLimpiar)
            {
                btnNuevo_Click(null, null);
            }
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

                ////////string sSql = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                ////////                " From vw_Farmacias_Urls U (NoLock) " +
                ////////                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                ////////                " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                ////////                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                ////////                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                //////////Grid.Limpiar(false);

                ////////if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
                ////////{
                ////////    sSql = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                ////////                                    " From vw_Farmacias_Urls U (NoLock) " +
                ////////                                    " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                ////////                                    " Where U.IdEstado = '{0}' and ( U.IdFarmacia = '{1}' ) " +
                ////////                                    " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                ////////                                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada); 
                ////////}

                ////////if (!leerWeb.Exec(sSql))
                ////////{
                ////////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                ////////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                ////////}
                ////////else
                ////////{
                ////////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                ////////    //Grid.LlenarGrid(leer.DataSetClase);
                ////////    //grdReporte.Focus();
                ////////}
            }
            cboFarmacias.SelectedIndex = 0;

        }

        #endregion Cargar Combos          

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            //Grid.Limpiar(false);
            lst.LimpiarItems(); 

            rdoConcentrado.Checked = true; 
            rdoTpDispAmbos.Checked = false; 
            rdoTpDispVenta.Checked = false; 
            rdoTpDispConsignacion.Checked = true; 

            rdoTpDispAmbos.Enabled = false;
            rdoTpDispVenta.Enabled = false; 


            IniciaToolBar(true, true, false);

            query.MostrarMsjSiLeerVacio = true;

            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
            //lblTotal.Text = Grid.TotalizarColumna(4).ToString();

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
                cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
                cboFarmacias.Enabled = false;
                txtFarmacia.Text = DtGeneralPedidos.FarmaciaConectada;
                lblFarmacia.Text = DtGeneralPedidos.FarmaciaConectadaNombre;
                txtFarmacia.Enabled = false;                
            }
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
            //LlenarGrid();
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;

            //cboEstados.Enabled = false;
            //cboFarmacias.Enabled = false;
            //dtpFechaInicial.Enabled = false;
            //dtpFechaFinal.Enabled = false;

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            if (!bSeEncontroInformacion)
            {
                General.msjUser("No ha información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);

                if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad_Directo)
                {
                    myRpt = new clsImprimir(DatosDeConexion);
                }

                // byte[] btReporte = null;
                string sEstado = cboEstados.Data;
                //string sFarmacia = cboFarmacias.Data;
                string sFarmacia = txtFarmacia.Text;

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"C:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = "Rpt_CteReg_CaducarSales_Farmacias";

                if (rdoDetallado.Checked)
                    myRpt.NombreReporte = "Rpt_CteReg_CaducarSales_Farmacias_Detallado";

                //if (General.ImpresionViaWeb)
                {
                    ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    ////DataSet datosC = DatosCliente.DatosCliente();

                    //////conexionWeb.Url = General.Url;
                    ////conexionWeb.Timeout = 300000;
                    ////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC);
                    ////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();
                    bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 
                     
                }

                if (!bRegresa)
                {
                    auditoria.GuardarAud_MovtosReg("*", myRpt.NombreReporte);
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        
        #endregion Botones

        #region Grid
        private void ObtenerInformacion()
        {
            int iMostrar = 2; // Para que muestre solo la farmacia de la unidad.
            string sCadena = "";

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            int iTipoInsumo = 0, iTipoDispensacion = 0;

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 1;
            }

            if (rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 2;
            }


            // Determinar que tipo de producto se mostrar 
            if (rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            sTablaFarmacia = "CTE_FarmaciasProcesar"; 

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_Impresion_Proximos_Caducar '{0}', '{1}', '{2}', '', '', '', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                   "", cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), iMostrar,
                   General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"),
                   iTipoInsumo, iTipoDispensacion );

            sSql += "\n " + string.Format(" Select ClaveSSA, DescripcionSal, Sum(Existencia) as Existencia " +
                " From Rpt_CteReg_Impresion_Proximos_Caducar (NoLock) " +
                " Group By IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " Order by IdClaveSSA_Sal ");

            lst.LimpiarItems(); 

            //////try
            //////{
            //////    leer.Reset();
            //////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia); 
            //////}
            //////catch { } 

            leer.Reset();
            leer.DataSetClase = GetInformacion(sSql);
            if (leer.SeEncontraronErrores())
            {
                //Error.GrabarError(leer, "");
                //General.msjError("Ocurrió un error al obtener la información de existencias.");

                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;
                //cboFarmacias.Enabled = false;
                txtFarmacia.Enabled = false;
                IniciaToolBar(true, false, true);

                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    IniciaToolBar(true, false, false);
                    //Grid.DeleteRow(1);
                    bSeEncontroInformacion = false;
                    bSeEjecuto = true;
                }
                else
                {
                    bSeEncontroInformacion = true;
                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosReg("*", sCadena);
                    lst.CargarDatos(leer.DataSetClase, true, true); 
                }
            }
            //lblTotal.Text = Grid.TotalizarColumna(4).ToString();

            AjustarColumnas();
            bEjecutando = false;

            this.Cursor = Cursors.Default;
        }

        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ////Codigo.ShowDialog();
            //string sClaveInterna = Grid.GetValue(e.Row + 1, 1);
            //Codigo = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
            //Codigo.MostrarDetalle(cboEstados.Data, cboFarmacias.Data, sClaveInterna, dtpFechaInicial.Text, dtpFechaFinal.Text );
        }
        #endregion Grid        

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
        }
        public void MostrarDetalle(string IdEstado, string ClaveInternaSal)
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            btnEjecutar_Click(null, null);

            this.ShowDialog();
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
                IniciaToolBar(true, true, false);
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                // sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                // sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
            }
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

                    //ActivarControles();

                    if (bSeEjecuto)
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }

        private void AjustarColumnas()
        {            
            lst.TituloColumna((int)Cols.ClaveSSA, "Clave SSA");
            lst.TituloColumna((int)Cols.Descripcion, "Descripción Clave");
            lst.TituloColumna((int)Cols.Existencia, "Existencia");

            lst.AnchoColumna((int)Cols.ClaveSSA, 120);
            lst.AnchoColumna((int)Cols.Descripcion, 650);
            lst.AnchoColumna((int)Cols.Existencia, 80);
            
        }

        #endregion Funciones       

        #region Eventos_Farmacia
        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {
            if (txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = query.Farmacias_UrlsActivas(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

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
                leer.DataSetClase = ayuda.Farmacias_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data);

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


    }
}
