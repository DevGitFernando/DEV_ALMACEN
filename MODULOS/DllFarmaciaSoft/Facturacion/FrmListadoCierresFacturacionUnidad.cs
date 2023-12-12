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
    public partial class FrmListadoCierresFacturacionUnidad : FrmBaseExt
    {
        private enum Cols
        {
            FechaRegistro = 1, Folio = 2, FechaInicial = 3, FechaFinal = 4  
        }


        // clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;

        // string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = ""; 

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        public FrmListadoCierresFacturacionUnidad()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            ////cnn = new clsConexionSQL();
            ////cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            ////cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            ////cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            ////cnn.DatosConexion.Password = General.DatosConexion.Password;
            ////cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            ////cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn); 
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            grid = new clsGrid(ref grdReporte, this);

        } 

        private void FrmListadoCierresFacturacionUnidad_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Impresion  
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

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            string sFolio = ""; 

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
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            //  byte[] btReporte = null;

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacion.rpt";

            if (Tipo == 2)
            {
                myRpt.NombreReporte = "PtoVta_CierrePeriodoFacturacionDetallado.rpt"; 
            }

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
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
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;
            // iBusquedasEnEjecucion = 0;


            grid.Limpiar(false); 
            Fg.IniciaControles(this, true);
            Fg.IniciaControles(this, true, FrameFechas);
            FrameFechas.Enabled = true; 
            //rdoInsumosAmbos.Checked = true;
            //rdoTpDispAmbos.Checked = true;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false; 
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;

            //FrameCliente.Enabled = false;
            //FrameInsumos.Enabled = false; 
            //FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            //FrameListaReportes.Enabled = false;

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

            //////try
            //////{
            //////    leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            //////    conexionWeb.Url = sUrl;
            //////    DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            //////    DatosDeConexion.Servidor = sHost;
            //////    bRegresa = true; 
            //////}
            //////catch (Exception ex1)
            //////{
            //////    Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
            //////    General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
            //////    ActivarControles(); 
            //////}

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
               DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sFiltro);

            grid.Limpiar(false);
            // leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            //if (validarDatosDeConexion())
            {
                //////cnnUnidad = new clsConexionSQL(DatosDeConexion);
                //////cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                //////cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite90;

                //////leer = new clsLeer(ref cnnUnidad); 
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

                    ////if ( bSeEjecuto ) 
                    ////    General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }
    } 
}
