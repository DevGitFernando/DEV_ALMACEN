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
    public partial class FrmListadoCierresFacturacion : FrmBaseExt
    {
        private enum Cols
        {
            FechaRegistro = 1, Folio = 2, FechaInicial = 3, FechaFinal = 4  
        }


        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = ""; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false; 

        public FrmListadoCierresFacturacion()
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
            grid = new clsGrid(ref grdReporte, this);

        } 

        private void FrmListadoCierresFacturacion_Load(object sender, EventArgs e)
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
            string sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            if (sFolio == "")
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un folio de cierre para reeimpresión, verifique."); 
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
            string sFolio = "";
            // bool bRegresa = false;

            if (validarImpresion())
            {
                sFolio = grid.GetValue(grid.ActiveRow, (int)Cols.Folio);
                ImprimirConcentradoCierrePeriodo(sFolio, 1);
                ImprimirConcentradoCierrePeriodo(sFolio, 2);
            }
        }

        private void ImprimirConcentradoCierrePeriodo(string Folio, int Tipo)
        {
            bool bRegresa = false;
            int iFolio = 0;

            iFolio = Convert.ToInt32(Folio);

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
            myRpt.Add("Folio", Fg.PonCeros(Folio, 8));

            //////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            //////DataSet datosC = DatosCliente.DatosCliente(); 
            ////bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        ////private void ImprimirConcentradoCierrePeriodoDetallado(string Folio)
        ////{
        ////    bool bRegresa = false; 
        ////    int iFolio = 0;

        ////    iFolio = Convert.ToInt32(Folio);

        ////    DatosCliente.Funcion = "ImprimirConcentradoCierrePeriodoDetallado()";
        ////    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
        ////    byte[] btReporte = null;

        ////    myRpt.RutaReporte = DtGeneral.RutaReportes;
        ////    myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado.rpt";

        ////    myRpt.Add("IdEmpresa", cboEmpresas.Data);
        ////    myRpt.Add("IdEstado", cboEstados.Data);
        ////    myRpt.Add("IdFarmacia", cboFarmacias.Data);
        ////    myRpt.Add("Folio", Fg.PonCeros(Folio, 8));

        ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
        ////    DataSet datosC = DatosCliente.DatosCliente();

        ////    bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

        ////    if (!bRegresa)
        ////    {
        ////        General.msjError("Ocurrió un error al cargar el reporte.");
        ////    }
        ////}
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            // iBusquedasEnEjecucion = 0;

            cboEstados.Enabled = false;
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0; 


            grid.Limpiar(false); 
            Fg.IniciaControles(this, true);
            Fg.IniciaControles(this, true, FrameFechas); 
            //rdoInsumosAmbos.Checked = true;
            //rdoTpDispAmbos.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 

            //FrameCliente.Enabled = bValor;
            //FrameInsumos.Enabled = bValor;
            //FrameDispensacion.Enabled = bValor;
            //FrameFechas.Enabled = bValor;
            //FrameListaReportes.Enabled = bValor; 

            //txtCte.Focus(); 

            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false;
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false; 
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            cboFarmacias.Enabled = false; 

            //FrameCliente.Enabled = false;
            //FrameInsumos.Enabled = false; 
            //FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            //FrameListaReportes.Enabled = false;

            bSeEjecuto = false; 
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

            // bEjecutando = true; 
            string sFiltro = "";
            if (!chkTodoPeriodo.Checked)
            {
                sFiltro = string.Format("   and convert(varchar(10), FechaRegistro, 120) between '{0}' and '{1}' ",
                    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value)); 
            } 


            string sSql = 
            string.Format("Select  " + 
               "    convert(varchar(10), FechaRegistro, 120) as FechaRegistro,  " + 
               "    ( right('00000000000' + cast(FolioCierre as varchar), 8) ) as FolioCierre, " + 
               "    convert(varchar(10), FechaCorte, 120) as FechaCorte,  " +
               "    convert(varchar(10), FechaInicial, 120) as FechaInicial,  " +
               "    convert(varchar(10), FechaFinal, 120) as FechaFinal " + 
               " From Ctl_CierresDePeriodos (NoLock) " + 
               " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  {3}  " + 
               " Order by FolioCierre " , 
               cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data, sFiltro);

            grid.Limpiar(false);
            // leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            if (validarDatosDeConexion())
            {
                cnnUnidad = new clsConexionSQL(DatosDeConexion);
                cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite90;

                leer = new clsLeer(ref cnnUnidad); 
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leerLocal.DatosConexion, leer.Error, "ObtenerInformacion()");
                    General.msjError("Ocurrió un error al obtener la información del reporte.");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        bSeEncontroInformacion = false;
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                    else
                    {
                        btnImprimir.Enabled = true;
                        bSeEncontroInformacion = true; 
                        grid.LlenarGrid(leer.DataSetClase); 
                    }
                }

                bSeEjecuto = true; 
                bEjecutando = false; // Cursor.Current  
            }
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            ////FrameCliente.Enabled = true;
            ////FrameInsumos.Enabled = true;
            ////FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            ////FrameListaReportes.Enabled = true; 
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

                if (!bSeEncontroInformacion) 
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles(); 

                    if ( bSeEjecuto ) 
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
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
    } 
}
