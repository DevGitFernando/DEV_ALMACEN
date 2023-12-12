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

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes
{
    public partial class FrmSurtimientoRecetas : FrmBaseExt 
    {
        enum Cols 
        {
            Encabezado = 1, Claves = 2, Cantidad = 3, Porcentaje = 4, Tipo = 5  
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
        clsGrid gridResumen;

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

        public FrmSurtimientoRecetas()
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

            gridResumen = new clsGrid(ref grdResumen, this);
            gridResumen.EstiloDeGrid = eModoGrid.ModoRow; 

            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            //////cboReporte.Clear();
            //////cboReporte.Add(); // Agrega Item Default 

            //////cboReporte.Add("CteReg_Admon_Farmacia_Beneficiarios.rpt", "Listado de Beneficiarios");

            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumos_Secretaria.rpt", "Concentrado de Dispensación");
            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumosDesglozado_Secretaria.rpt", "Concentrado de Dispensación desglozado");
            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumosPrograma_Secretaria.rpt", "Concentrado de Dispensación Por Programa");
            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumosProgramaTotalizado_Secretaria.rpt", "Concentrado de Dispensación Por Programa Totalizado");

            ////////cboReporte.Add("CteReg_Admon_Validacion_Secretaria.rpt", "Detallado de Dispensación (Validación)");

            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumosSinPrecio_Secretaria.rpt", "Claves SSA sin precio asignado");
            ////////cboReporte.Add("CteReg_Admon_ConcentradoInsumosSinPrecioDetallado_Secretaria.rpt", "Claves SSA sin precio asignado detallado");            

            //////cboReporte.SelectedIndex = 1; 
        } 

        private void FrmSurtimientoRecetas_Load(object sender, EventArgs e)
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

        #region Buscar Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }

        }

        private void CargarDatosCliente()
        {
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("Nombre");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
            //    if (leer.Leer())
            //    {
            //        CargarDatosCliente();
            //    }
            //}

        }
        #endregion Buscar Cliente

        #region Buscar SubCliente 
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(),"txtSubCte_Validating");
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }

        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    leer.DataSetClase = Ayuda.SubClientes_Buscar("txtCte_KeyDown", txtCte.Text.Trim());
            //    if (leer.Leer())
            //    {
            //        CargarDatosSubCliente();
            //    }
            //}
        }
        #endregion Buscar SubCliente

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
                myRpt.NombreReporte = "Cte_Admon_SurtimientoRecetas.rpt";
                // myRpt.NombreReporte = "Cte_Admon_SurtimientoRecetas_Perfil"; 


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
            bool bValor = false;
            // iBusquedasEnEjecucion = 0;

            Fg.IniciaControles(this, true); 
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;

            gridResumen.Limpiar(false); 
            CargarDatosClienteConectado();  
            CargarEstados();
            txtFarmacia.Focus();
        }

        private void CargarDatosClienteConectado()
        {
            txtCte.Enabled = false;
            txtCte.Text = DtGeneralPedidos.Cliente;
            lblCte.Text = DtGeneralPedidos.ClienteNombre;

            txtSubCte.Enabled = false;
            txtSubCte.Text = DtGeneralPedidos.SubCliente;
            lblSubCte.Text = DtGeneralPedidos.SubClienteNombre;
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

                    // FrameCliente.Enabled = false;

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

        #region Eventos
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = "";
            //Grid.Limpiar();
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            //Grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            //lblPro.Text = "";
            //Grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            //lblSubPro.Text = "";
            //Grid.Limpiar();
        }

        #endregion Eventos

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

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_SurtimientoRecetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
               cboEstados.Data, txtFarmacia.Text, txtCte.Text.Trim(),txtSubCte.Text.Trim(), 
               General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value) );

            //sSql += "\n " + string.Format("Select top 1 * From tmpRpt_SurtimientoRecetas (NoLock) ");

            //sSql += "\n " +
            //    string.Format("Select  Empresa, Farmacia, Periodo, FechaReporte, " + 
            //        " FoliosDeVenta, Vales, NoSurtido, PorcSurtido, PorcVales, PorcNoSurtido, " + 
            //        " ClavesDiferentes, CantidadTotal, " + 
            //        " ClavesSurtidas, CantidadSurtida, PorcClavesSurtidas, " + 
            //        " ClavesPerfil, PorcClavesPerfil, " + 
            //        " ClavesVales, CantidadVale, PorcClavesVales, " + 
            //        " ClavesNoSurtido, CantidadNoSurtida, PorcClavesNoSurtida " +
            //        " From Rpt_NivelDeAbasto (NoLock) "); 

            if (ValidaDatos()) 
            {
                ////try
                ////{
                ////    leer.Reset(); 
                ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, sSql, "reporte", sTablaFarmacia);
                ////}
                ////catch { } 


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
                        // Empresa, Farmacia, Periodo, FechaReporte 
                        sTituloEmpresa = leer.Campo("Empresa");
                        sTituloFarmacia = leer.Campo("Farmacia");
                        sTituloReporte = leer.Campo("Periodo");
                        sTituloFecha = leer.CampoFecha("FechaReporte").ToString(); 

                        CargarInformacionSurtimiento(); 
                        // GridSurtimiento.LlenarGrid(leer.DataSetClase);
                    }
                }

                bEjecutando = false;

                sCadena = sSql.Replace("'", "\"");
                auditoria.GuardarAud_MovtosReg(sCadena, General.Url); 
                this.Cursor = Cursors.Default;
            }
        }

        private void CargarInformacionSurtimiento()
        {
            int iRow = 1;
            double iSurtido = 0; 
            string sSql = 
                string.Format("Select FoliosDeVenta, Vales, NoSurtido, PorcSurtido, PorcVales, PorcNoSurtido, " +
                    " ClavesDiferentes, CantidadTotal, " +
                    " ClavesSurtidas, CantidadSurtida, PorcClavesSurtidas, " +
                    " ClavesPerfil, PorcClavesPerfil, " +
                    " ClavesVales, CantidadVale, PorcClavesVales, " +
                    " ClavesNoSurtido, CantidadNoSurtida, PorcClavesNoSurtida " +
                    " From Rpt_NivelDeAbasto (NoLock) ");


            lblFolios.Text = leer.CampoDouble("FoliosDeVenta").ToString(); 
            lblSurtidos.Text = "% " + leer.CampoDouble("PorcSurtido").ToString();
            lblVales.Text = "% " + leer.CampoDouble("PorcVales").ToString();
            lblNoSurtido.Text = "% " + leer.CampoDouble("PorcNoSurtido").ToString();

            iSurtido = leer.CampoDouble("FoliosDeVenta") - (leer.CampoDouble("Vales") + leer.CampoDouble("NoSurtido"));
            lblSurtidoPzas.Text = iSurtido.ToString();
            lblValesPzas.Text = leer.CampoDouble("Vales").ToString();
            lblNoSurtidoPzas.Text = leer.CampoDouble("NoSurtido").ToString(); 


            gridResumen.Limpiar(true);
            gridResumen.SetValue(iRow, (int)Cols.Encabezado, "Claves solicitadas");
            gridResumen.SetValue(iRow, (int)Cols.Claves, leer.CampoInt("ClavesDiferentes"));
            gridResumen.SetValue(iRow, (int)Cols.Cantidad, leer.CampoDouble("CantidadTotal"));
            gridResumen.SetValue(iRow, (int)Cols.Porcentaje, "100");
            gridResumen.SetValue(iRow, (int)Cols.Tipo, 0);
            gridResumen.ColorRenglon(iRow, General.FormaBackColor); 

            gridResumen.AddRow(); 
            iRow++;  
            gridResumen.SetValue(iRow, (int)Cols.Encabezado, "Claves dispensadas");
            gridResumen.SetValue(iRow, (int)Cols.Claves, leer.CampoInt("ClavesSurtidas"));
            gridResumen.SetValue(iRow, (int)Cols.Cantidad, leer.CampoDouble("CantidadSurtida"));
            gridResumen.SetValue(iRow, (int)Cols.Porcentaje, leer.CampoDouble("PorcClavesSurtidas"));
            gridResumen.SetValue(iRow, (int)Cols.Tipo, 1); 

            gridResumen.AddRow(); 
            iRow++; 
            gridResumen.SetValue(iRow, (int)Cols.Encabezado, "Claves vales");
            gridResumen.SetValue(iRow, (int)Cols.Claves, leer.CampoInt("ClavesVales"));
            gridResumen.SetValue(iRow, (int)Cols.Cantidad, leer.CampoDouble("CantidadVale"));
            gridResumen.SetValue(iRow, (int)Cols.Porcentaje, leer.CampoDouble("PorcClavesVales"));
            gridResumen.SetValue(iRow, (int)Cols.Tipo, 2); 

            gridResumen.AddRow(); 
            iRow++; 
            gridResumen.SetValue(iRow, (int)Cols.Encabezado, "Claves no surtidas");
            gridResumen.SetValue(iRow, (int)Cols.Claves, leer.CampoInt("ClavesNoSurtido"));
            gridResumen.SetValue(iRow, (int)Cols.Cantidad, leer.CampoDouble("CantidadNoSurtida"));
            gridResumen.SetValue(iRow, (int)Cols.Porcentaje, leer.CampoDouble("PorcClavesNoSurtida"));
            gridResumen.SetValue(iRow, (int)Cols.Tipo, 3); 

        }

        #endregion Grid 

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            // FrameCliente.Enabled = true;
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

            if (lblCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado un Cliente válido, verifique.");
                txtCte.Focus();
            }

            if (bRegresa && lblSubCte.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No se ha capturado un Sub-Cliente válido, verifique.");
                txtSubCte.Focus();
            }

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

        #region Modulos 
        private void grdResumen_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (gridResumen.Rows > 0)
            {
                int iRow = gridResumen.ActiveRow;
                string sEncabezado = gridResumen.GetValue(iRow, (int)Cols.Encabezado);
                int iTipo = gridResumen.GetValueInt(iRow, (int)Cols.Tipo);

                //sTituloEmpresa = leer.Campo("Empresa");
                //sTituloFarmacia = leer.Campo("Farmacia");
                //sTituloReporte = leer.Campo("Periodo");
                //sTituloFecha = leer.CampoFecha("FechaReporte").ToString(); 

                FrmClavesSurtimiento f = new FrmClavesSurtimiento(sEncabezado, iTipo, cboEstados.Data, txtFarmacia.Text,
                    DatosDeConexion, sTituloEmpresa, sTituloFarmacia, sTituloReporte, sTituloFecha); 
                f.ShowDialog();  
            }
        }
        #endregion Modulos
    } 
}
