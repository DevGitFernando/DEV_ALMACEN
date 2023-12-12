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

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmTiposRotacion : FrmBaseExt 
    {
        enum Cols 
        {
            IdJuris = 1, Juris = 2, IdFarmacia = 3, Farmacia = 4, ClaveSSA = 5, DescClave = 6, TipoRotacion = 7  
        } 

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        // clsGrid GridSurtimiento;
        //clsGrid grid; 

        // string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        string sTablaFarmacia = "";

        clsDatosCliente DatosCliente; 
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false; 

        //Clase de los movimientos de auditoria
        clsAuditoria auditoria;

        string sTituloEmpresa = "";
        string sTituloFarmacia = "";
        string sTituloReporte = "";
        string sTituloFecha = "";

        clsListView lst;

        public FrmTiposRotacion()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, true);
            //Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name, true);

            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            // DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;

            //grid = new clsGrid(ref grdClaves, this);
            ////grid.EstiloDeGrid = eModoGrid.ModoRow; 
            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            //////cboReporte.Clear();
            //////cboReporte.Add(); // Agrega Item Default 

            //////cboReporte.Add("CteReg_Admon_Farmacia_Beneficiarios.rpt", "Listado de Beneficiarios");

            cboTipoReporte.Add("1", "Dias de inventario");
            cboTipoReporte.Add("2", "Número de prescripciones");
            cboTipoReporte.Add("3", "Volumén dispensado"); 
            cboTipoReporte.SelectedIndex = 0; 

        } 

        private void FrmTiposRotacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            //CargarEstados();
        }
        #region Cargar Combos         

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

                ////string sSql = "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";
                ////if (!leerWeb.Exec(sSql))
                ////{
                ////    Error.GrabarError(leerWeb, "CargarEstados()");
                ////    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                ////}
                ////else
                ////{
                ////    cboEstados.Add(leerWeb.DataSetClase, true, "IdEstado", "Estado"); 
                ////}
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

        private void CargarFarmacias()
        {
            if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                //////////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                //////////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                //////////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                ////////sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                ////////                " From vw_Farmacias_Urls U (NoLock) " +
                ////////                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                ////////                " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                ////////                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                ////////                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                ////////if (!leerWeb.Exec(sSqlFarmacias))
                ////////{
                ////////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                ////////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                ////////}
                ////////else
                ////////{
                ////////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                ////////}
            }
            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Buscar Programa 
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            //if (txtPro.Text.Trim() != "")
            //{
            //    {
            //        leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
            //        if (leer.Leer())
            //        {
            //            CargarDatosProgramas();
            //        }
            //        else
            //        {
            //            lblPro.Text = "";
            //            txtPro.Focus();
            //        }
            //    }
            //}
        }

        private void CargarDatosProgramas()
        {
            //txtPro.Text = leer.Campo("IdPrograma");
            //lblPro.Text = leer.Campo("Descripcion");
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
            //    if (leer.Leer())
            //    {
            //        CargarDatosProgramas();
            //    }
            //}
        }
        #endregion Buscar Programa

        #region Buscar SubPrograma
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            //if (txtSubPro.Text.Trim() != "")
            //{
            //    leer.DataSetClase = Consultas.SubProgramas(txtSubPro.Text, txtPro.Text, "txtSubPro_Validating");
            //    if (leer.Leer())
            //    {
            //        CargarDatosSubProgramas();
            //    }
            //    else
            //    {
            //        lblSubPro.Text = "";
            //        txtSubPro.Focus();
            //    }
            //}
        }

        private void CargarDatosSubProgramas()
        {
            //txtSubPro.Text = leer.Campo("IdSubPrograma");
            //lblSubPro.Text = leer.Campo("Descripcion");
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", txtPro.Text);
            //    if (leer.Leer())
            //    {
            //        CargarDatosSubProgramas();
            //    }
            //}
        }
        #endregion Buscar SubPrograma

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
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

                DatosCliente.Funcion = "Imprimir()";              
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sEstado = cboEstados.Data;
                //string sFarmacia = cboFarmacias.Data;
                string sFarmacia = txtFarmacia.Text;

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";
                if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad_Directo)
                {
                    myRpt = new clsImprimir(DatosDeConexion);
                }

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = "Cte_Admon_Rotacion_Claves.rpt";
                myRpt.Add("TipoDeRotacionClaves", cboTipoReporte.Data); 

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

                //}
                //else
                //{
                    //// Lineas para pruebas locales ///////
                    //myRpt.CargarReporte(true);
                    //bRegresa = !myRpt.ErrorAlGenerar;
                    ////////////////////////////////////////
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    // auditoria.GuardarAud_MovtosReg("Reporte => " + myRpt.NombreReporte, sUrl);
                    auditoria.GuardarAud_MovtosReg("Reporte => " + myRpt.NombreReporte, General.Url);
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true); 
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;

            //grid.Limpiar(false);
            lst.Limpiar();

            CargarEstados();
            CargarJurisdicciones();
            cboJurisdicciones.Focus();
            //txtFarmacia.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
            {
                if (ValidaDatos())
                {
                    bSeEncontroInformacion = false;
                    btnNuevo.Enabled = false;
                    btnEjecutar.Enabled = false;
                    btnImprimir.Enabled = false;

                    bSeEjecuto = false;
                    tmEjecuciones.Enabled = true;
                    tmEjecuciones.Start();


                    Cursor.Current = Cursors.WaitCursor;
                    System.Threading.Thread.Sleep(1000);

                    _workerThread = new Thread(this.ObtenerInformacion);
                    _workerThread.Name = "GenerandoValidacion";
                    _workerThread.Start();
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        } 
        #endregion Botones

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

        private void ObtenerInformacion()
        {           
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            string sCadena = "";

            sTablaFarmacia = "CTE_FarmaciasProcesar";

            string sSql = 
            string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_RotacionClaves '', '{0}', '{1}', '{2}', '{3}', '{4}' ",
                cboEstados.Data, cboJurisdicciones.Data, txtFarmacia.Text, General.FechaYMD(dtpFechaInicial.Value), cboTipoReporte.Data  );

            //sSql += "\n " + string.Format("Select top 1 * From tmpRpt_SurtimientoRecetas (NoLock) ");

            ////sSql += "\n " +
            ////    string.Format("Select  ClaveSSA, DescripcionClave, DescripcionRotacion " + 
            ////        " From Rtp_CteReg_TiposRotacion_Claves (NoLock) " +
            ////        " Where RotacionTipo = '{0}' " + 
            ////        " Order by TipoRotacion, DescripcionRotacion ", cboTipoReporte.Data); 

            if (ValidaDatos()) 
            {
                ////try
                ////{
                ////    leer.Reset(); 
                ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, sSql, "reporte", sTablaFarmacia);
                ////}
                ////catch { }

                lst.Limpiar();

                leer.Reset();
                leer.DataSetClase = GetInformacion(sSql); 
                if (leer.SeEncontraronErrores()) 
                {
                    btnImprimir.Enabled = false;
                    bSeEncontroInformacion = false; 
                    Error.GrabarError(leer, "ObtenerInformacion()"); 
                    General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
                }
                else 
                {
                    btnImprimir.Enabled = true;
                    bSeEncontroInformacion = true;
                    if (!leer.Leer())
                    {
                        General.msjUser("No se encontro información con los criterios especificados"); 
                        bSeEjecuto = true;
                    }
                    else
                    {
                        lst.CargarDatos(leer.DataSetClase, true, true); 
                    }
                }

                bEjecutando = false;

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosReg(sCadena, General.Url); 
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Grid 

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true; 
            ////FrameListaReportes.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                ////FrameListaReportes.Enabled = true;                
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = true; 

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

        #region FrameFechas
        private void FrameFechas_Enter(object sender, EventArgs e)
        {

        }

        private void FrameInsumos_Enter(object sender, EventArgs e)
        {

        }

        private void FrameListaReportes_Enter(object sender, EventArgs e)
        {

        }

        private void FrameDispensacion_Enter(object sender, EventArgs e)
        {

        }
        #endregion FrameFechas

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;
            string sEdo = "", sFar = "";
            sEdo = cboEstados.Data;

            string sSql = string.Format("Delete From CTE_FarmaciasProcesar ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "FarmaciasAProcesar()");
                General.msjError("Ocurrió un error al borrar tabla.");
                bReturn = false;
            }
            else
            {
                //sFar = cboFarmacias.Data;
                sFar = txtFarmacia.Text;

                string sQuery = string.Format(" Insert Into CTE_FarmaciasProcesar " +
                                     " Select '{0}','{1}','A',0 ", sEdo, sFar);
                if (!leer.Exec(sQuery))
                {
                    Error.GrabarError(leer, "FarmaciasAProcesar()");
                    General.msjError("Ocurrió un error al Insertar Farmacias a Procesar.");
                    bReturn = false;
                }
                else
                {
                    bReturn = true;
                }                
            }

            return bReturn;
        }       

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //Grid.SetValue(3, chkTodos.Checked);
        }

        #region Funciones
        private bool ValidaDatos()
        {
            bool bRegresa = true; 
            return bRegresa; 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                ////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                ////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                //////// cboFarmacias.Enabled = false;
            }
        }

        #endregion Funciones        

        #region Eventos_Farmacia
        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {

            if (txtFarmacia.Text.Trim() != "" && txtFarmacia.Text.Trim() != "*")
            {
                leer.DataSetClase = Consultas.FarmaciasJurisdiccion_UrlsActivas(cboEstados.Data, cboJurisdicciones.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

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
            else
            {
                txtFarmacia.Text = "*";
                lblFarmacia.Text = "Todas las Farmacias";
                //txtFarmacia.Enabled = false;
            }
        }

        private void txtFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.FarmaciasJurisdiccion_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data, cboJurisdicciones.Data);
                
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

        private void txtFarmacia_TextChanged(object sender, EventArgs e)
        {
            lblFarmacia.Text = "";
        }
        #endregion Eventos_Farmacia

        #region Modulos 
        ////private void grdResumen_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        ////{
        ////    if (grid.Rows > 0)
        ////    {
        ////        int iRow = grid.ActiveRow;
        ////        string sEncabezado = grid.GetValue(iRow, (int)Cols.Encabezado);
        ////        int iTipo = grid.GetValueInt(iRow, (int)Cols.Tipo);

        ////        //sTituloEmpresa = leer.Campo("Empresa");
        ////        //sTituloFarmacia = leer.Campo("Farmacia");
        ////        //sTituloReporte = leer.Campo("Periodo");
        ////        //sTituloFecha = leer.CampoFecha("FechaReporte").ToString(); 

        ////        FrmClavesSurtimiento f = new FrmClavesSurtimiento(sEncabezado, iTipo, cboEstados.Data, txtFarmacia.Text, 
        ////            DatosDeConexion, sTituloEmpresa, sTituloFarmacia, sTituloReporte, sTituloFecha); 
        ////        f.ShowDialog();  
        ////    }
        ////}
        #endregion Modulos

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboJurisdicciones.Data == "*")
            {
                txtFarmacia.Text = "*";
                lblFarmacia.Text = "Todas las Farmacias";
                txtFarmacia.Enabled = false;
            }
            else
            {
                txtFarmacia.Text = "";
                lblFarmacia.Text = "";
                txtFarmacia.Enabled = true;
                txtFarmacia.Focus();
            }
        }
                
    } 
}
