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

using DllFarmaciaSoft;

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmCierrePeriodoCentral : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        // clsGrid Grid;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";

        string sFolio = ""; 
        string sMensaje = ""; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;
        int iEsPrecierre = 0; 

        public FrmCierrePeriodoCentral()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);


        }

        private void FrmReporteListadoCteProgramasVenta_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos 
        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;
                }

            }
            cboEmpresas.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and ( U.IdFarmacia <> '{2}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A'  ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada );

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
            }
            cboFarmacias.SelectedIndex = 0;
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
            //string sSql = string.Format(" Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
            //    " From Net_CFGC_Parametros (NoLock) " +
            //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' ", 
            //    cboEstados.Data, cboFarmacias.Data, "RutaReportes");
            //if (!leerWeb.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "ObtenerRutaReportes");
            //    General.msjError("Ocurrió un error al obtener la Ruta de Reportes de la Farmacia."); 
            //}
            //else
            //{
            //    leerWeb.Leer();
            //    sUrl_RutaReportes = leerWeb.Campo("Valor");     
            //}
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            //if (bRegresa && cboReporte.SelectedIndex == 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            //    cboReporte.Focus(); 
            //}

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central. 
                // Se utilizan los datos de Conexión de la farmacia seleccionada. 

                DatosCliente.Funcion = "Imprimir()"; 
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                //////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                //////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Admon_ListadoClientesPragramaPeriodo.rpt";

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

                //////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    conexionWeb.Url = General.Url;
                ////    conexionWeb.Timeout = 300000;
                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            iEsPrecierre = 0;
            // iBusquedasEnEjecucion = 0;

            cboEstados.Enabled = false;
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0; 

            Fg.IniciaControles(this, true); 

            //txtCte.Focus(); 
            lblMensaje.Text = "Todos los tickets menor ó igual a la fecha seleccionada se cerrarán, no podra hacer cambios a la información de un ticket cerrado.";


            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false;
            }

            cboEmpresas.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false; 
            btnNuevo.Enabled = false;
            cboFarmacias.Enabled = false;

            // bSeEjecuto = false; 
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            // LlenarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        } 
        #endregion Botones

        #region Grid 
        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

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
           
            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite90;

                leer = new clsLeer(ref cnnUnidad);

                ////if (AplicarCierrePeriodo())
                ////{
                ////    bSeEncontroInformacion = true;
                ////}
                ////else
                ////{
                ////    bSeEncontroInformacion = false;
                ////} 

                bSeEncontroInformacion = AplicarCierrePeriodo();

                // bSeEjecuto = true; 
                bEjecutando = false; // Cursor.Current  
            }
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                //FrameListaReportes.Enabled = true; 
                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                ActivarControles();

                if (bSeEncontroInformacion)
                {
                    if (iEsPrecierre == 0)
                    {
                        ImprimirConcentradoCierrePeriodo(1);
                        ImprimirConcentradoCierrePeriodo(2);
                    }

                    if (iEsPrecierre == 1)
                    {
                        ImprimirConcentradoCierrePeriodoVistaPrevia(1);
                        ImprimirConcentradoCierrePeriodoVistaPrevia(2);
                    }
                }
                else 
                {
                    _workerThread.Interrupt();
                    _workerThread = null; 

                    //if (bSeEjecuto)
                    //{
                    //    General.msjUser("No existe informacion para mostrar bajo los criterios seleccionados.");
                    //}
                }
            }
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true;
                CargarEstados();
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true;
                CargarFarmacias();
            } 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString(); 
                // cboFarmacias.Enabled = false;
            }
        }

        #region Cierres , Pre-Cierres 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            iEsPrecierre = 0; 
            Procesar(); 
        }

        private void btnPrecierre_Click(object sender, EventArgs e)
        {
            iEsPrecierre = 1; 
            Procesar(); 
        }

        private void Procesar()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            cboFarmacias.Enabled = false;

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
            //AplicarCierrePeriodo();  
        }

        #endregion Cierres , Pre-Cierres
        
        #region Validaciones
        private bool AplicarCierrePeriodo()
        {
            bool bRegresa = false;

            if (verificarPeriodoAbierto())
            {
                if (!cnnUnidad.Abrir())
                {
                    General.msjAviso(General.MsjErrorAbrirConexion); 
                }
                else 
                {

                    cnnUnidad.IniciarTransaccion();

                    string sSql = string.Format(
                        "Exec spp_Mtto_Ctl_CierresDePeriodos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                            cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sFolio, "", General.FechaYMD(dtpFechaCierre.Value, "-"), 
                            DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, iEsPrecierre );

                    bRegresa = false; 
                    if (leer.Exec(sSql))
                    {
                        if (leer.Leer())
                        {
                            sMensaje = leer.Campo("Mensaje");
                            sFolio = leer.Campo("Folio");
                            bRegresa = true;
                        }
                    }
                    
                    if (!bRegresa)
                    {
                        Error.GrabarError(leer, "AplicarCierrePeriodo()");
                        cnnUnidad.DeshacerTransaccion(); 
                        General.msjError("Ocurrió un error al Generar Cierre de Periodo de Facturación"); 
                    }
                    else
                    {
                        cnnUnidad.CompletarTransaccion();
                        General.msjUser(sMensaje); 

                        ////if (iEsPrecierre == 0)
                        ////{
                        ////    ImprimirConcentradoCierrePeriodo(1);
                        ////    ImprimirConcentradoCierrePeriodo(2);
                        ////}

                        ////if (iEsPrecierre == 1)
                        ////{
                        ////    ImprimirConcentradoCierrePeriodoVistaPrevia(1);
                        ////    ImprimirConcentradoCierrePeriodoVistaPrevia(2);
                        ////}

                        //btnNuevo_Click(null, null);
                    }

                    cnn.Cerrar();
                }
            }

            return bRegresa;
        }

        private bool verificarPeriodoAbierto()
        {
            bool bRegresa = false;

            string sSql = string.Format(
                " Select top 1 * From VentasEnc (Nolock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCierre = 0 " +
                " And Convert( varchar(10), FechaRegistro, 120) <= '{3}' ",
                cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaCierre.Value, "-"));

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "VerificarPeriodoAbierto()");
                General.msjError("Ocurrió un error al Verificar Cierre de Periodos.");
            }
            else 
            {
                bRegresa = true;
                if (!leer.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No se encontró información para generar el cierre de periodo.");
                } 
            }

            return bRegresa;
        }
        
        #endregion Validaciones 

        #region Impresion
        private void ImprimirCierrePeriodo()
        {
            DatosCliente.Funcion = "ImprimirCierrePeriodo()";
            clsImprimir myRpt = new clsImprimir(DatosDeConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierreDePeriodo.rpt";

            myRpt.Add("@IdEmpresa", cboEmpresas.Data);
            myRpt.Add("@IdEstado", cboEstados.Data);
            myRpt.Add("@IdFarmacia", cboFarmacias.Data);
            myRpt.Add("@FolioCierre", sFolio);

            //DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //DataSet datosC = DatosCliente.DatosCliente();

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }

        }

        private void ImprimirConcentradoCierrePeriodo(int Tipo)
        {
            bool bRegresa = false;
            int iFolio = 0;

            iFolio = Convert.ToInt32(sFolio);

            DatosCliente.Funcion = "ImprimirConcentradoCierrePeriodo()";
            clsImprimir myRpt = new clsImprimir(DatosDeConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacion.rpt";

            if (Tipo == 2)
            {
                myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado.rpt";
            }

            myRpt.Add("IdEmpresa", cboEmpresas.Data);
            myRpt.Add("IdEstado", cboEstados.Data);
            myRpt.Add("IdFarmacia", cboFarmacias.Data);
            myRpt.Add("Folio", Fg.PonCeros(sFolio, 8));

            //DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);


            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirConcentradoCierrePeriodoVistaPrevia(int Tipo)
        {
            bool bRegresa = false;
            int iFolio = 0;

            iFolio = Convert.ToInt32(sFolio);

            DatosCliente.Funcion = "ImprimirConcentradoCierrePeriodoDetallado()";
            clsImprimir myRpt = new clsImprimir(DatosDeConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacion_VP.rpt";

            if (Tipo == 2)
            {
                myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado_VP.rpt";
            }

            myRpt.Add("IdEmpresa", cboEmpresas.Data);
            myRpt.Add("IdEstado", cboEstados.Data);
            myRpt.Add("IdFarmacia", cboFarmacias.Data);
            myRpt.Add("Folio", Fg.PonCeros(sFolio, 8));

            DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            DataSet datosC = DatosCliente.DatosCliente();
            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion       
    } 
}
