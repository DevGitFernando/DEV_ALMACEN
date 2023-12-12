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

namespace DllFarmaciaSoft.Ventas
{
    public partial class FrmReporteVentasContado : FrmBaseExt
    {
        private enum Cols
        {
            IdClave = 1, ClaveSSA = 2, DescripcionClave = 3, 
            IdProducto = 4, DescripcionProducto = 5, CodigoEAN = 6, ClaveLote = 7, 
            FechaCaducidad = 8, Existencia = 9
        }

        clsDatosConexion DatosDeConexion; 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        // clsGrid grid;

        bool bExistenDatos = false; 
        DataSet dtsDatosConsulta = new DataSet(); 

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();

        string sHost = ""; 
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;

        string sSqlFarmacias = "";
        string sUrl; 

        public FrmReporteVentasContado()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;


            ////grid = new clsGrid(ref grdExistencia, this);
            ////grid.EstiloGrid(eModoGrid.SeleccionSimple);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada ); 

            ////lblConsultando.BackColor = colorEjecutando;
            ////lblFinExito.BackColor = colorEjecucionExito;
            ////lblFinError.BackColor = colorEjecucionError;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // rdoConcentrado.Checked = true;
            // MostrarOcultarDetalle(true); 
            iBusquedasEnEjecucion = 0;
            // grid.Limpiar(false);
            // grid.Reset(); 

            Fg.IniciaControles();

            cboEstados.Enabled = false; 
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0; 

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
            IniciarConsultaExistencias();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        }
        #endregion Botones

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

            //if (!bSeEncontroInformacion)
            //{
            //    bRegresa = false;
            //    General.msjUser("No existe información para generar el reporte, verifique.");
            //}

            //if (bRegresa && cboReporte.SelectedIndex == 0)
            //{
            //    bRegresa = false;
            //    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            //    cboReporte.Focus();
            //}

            bRegresa = validarDatosDeConexion(); 

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

                ////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = "PtoVta_VentasPublicoGral_Mensual.rpt";

                myRpt.Add("@IdEmpresa", cboEmpresas.Data);
                myRpt.Add("@IdEstado", cboEstados.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);
                myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                //if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    conexionWeb.Url = General.Url;
                ////    conexionWeb.Timeout = 300000;
                ////    //////myRpt.CargarReporte(true); 

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                //else
                //{
                //    myRpt.CargarReporte(true);
                //    bRegresa = !myRpt.ErrorAlGenerar;
                //}

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

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
            }

            return bRegresa;
        }
        #endregion Impresion


        private void FrmReporteVentasContadoEnFarmacias_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null); 
        }

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

            ////sSqlFarmacias = string.Format(" Select IdFarmacia, (IdFarmacia + ' - ' + Farmacia) as Farmacia, UrlFarmacia " +
            ////                " From vw_Farmacias_Urls (NoLock) " +
            ////                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
            ////                " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
            ////                cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada );

            sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                            " From vw_Farmacias_Urls U (NoLock) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where U.IdEmpresa = '{0}' and U.IdEstado = '{1}' and ( U.IdFarmacia <> '{2}' ) " +
                            "   and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada);


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

        private void rdoFarmacia_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoFarmacia.Checked)
            //    cboFarmacias.Enabled = true;
        }

        private void rdoTodas_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoTodas.Checked)
            //    cboFarmacias.Enabled = false;
        }

        private void CargarFarmaciasGrid()
        {
            //////if (rdoFarmacia.Checked)
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado, cboFarmacias.Data);

            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) and IdFarmacia = '{3}' " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{4}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboFarmacias.Data, iEsEmpresaConsignacion); 
            //////}
            //////else
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado );
                
            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{3}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iEsEmpresaConsignacion ); 
            //////}


            //////if (!leer.Exec(sSqlFarmacias))
            //////{
            //////    Error.GrabarError(leer, "CargarFarmacias()");
            //////    General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            //////}
            //////else
            //////{
            //////    grid.Limpiar(false);
            //////    grid.LlenarGrid(leer.DataSetClase);
            //////}
        }

        private void IniciarConsultaExistencias()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false; 
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();
            iBusquedasEnEjecucion = 0;

            ////rdoConcentrado.Enabled = false;
            ////rdoDetallado.Enabled = false; 

            //for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.ConsultarExistenciaFarmacia);
                _workerThread.Name = "ConsultaDeInformacion";
                _workerThread.Start(1);
            }
        }

        private void ConsultarExistenciaFarmacia(object Renglon)
        {
            ////Cursor.Current = Cursors.WaitCursor;
            ////bExistenDatos = false; 
            ////dtsDatosConsulta = new DataSet(); 

            ////int iRow = (int)Renglon;
            ////// string sIdFarmacia = grid.GetValue(iRow, 1);
            ////sUrl = grid.GetValue(iRow, 3);
            ////string sValor = ""; ////  "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            ////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();  

            ////string sSql = string.Format(" Select 'IdClave SSA' = IdClaveSSA_Sal, " + 
            ////    " 'Clave SSA' = ClaveSSA, 'Descripcion Clave' = DescripcionSal, " +
            ////    " '' as IdProducto, '' as Producto, '' as CodigoEAN, '' as ClaveLote, " +
            ////    " 'Fecha de Caducidad' = '', sum(Existencia) as Existencia  " + 
            ////    " from vw_ExistenciaPorCodigoEAN_Lotes " + 
            ////    " where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
            ////    " group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
            ////    " order by DescripcionSal ", 
            ////    cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data );

            ////if (rdoDetallado.Checked)
            ////{
            ////    sSql = string.Format(" Select 'IdClave SSA' = IdClaveSSA_Sal, " +
            ////        " 'Clave SSA' = ClaveSSA, 'Descripcion Clave' = DescripcionSal, " +
            ////        " IdProducto, 'Producto' = DescripcionProducto, CodigoEAN, ClaveLote, " + 
            ////        " 'Fecha de Caducidad' = convert(varchar(10), FechaCaducidad, 120), Existencia " + 
            ////        " from vw_ExistenciaPorCodigoEAN_Lotes " +
            ////        " where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
            ////        " order by DescripcionSal, DescripcionProducto, FechaCaducidad ",  
            ////        cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data); 
            ////}

            ////grid.Limpiar(true);
            ////grid.ColorRenglon(iRow, colorEjecutando);
            ////grid.SetValue(iRow, 3, "");

            ////// grid.Reset(); 
            ////iBusquedasEnEjecucion++;

            ////// clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, General.ArchivoIni, datosCliente);
            ////clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            ////if (!myWeb.Exec(sSql))
            ////{
            ////    Error.LogError(cboFarmacias.Text + " ----  " + myWeb.Error.Message); 
            ////    grid.ColorRenglon(iRow, colorEjecucionError);
            ////    rdoConcentrado.Enabled = true;
            ////    rdoDetallado.Enabled = true; 
            ////}
            ////else
            ////{
            ////    if (myWeb.Leer())
            ////    {
            ////        bExistenDatos = true; 
            ////        dtsDatosConsulta = myWeb.DataSetClase; 
            ////        // grid.SetValue(iRow, 4, myWeb.Campo("Existencia"));
            ////        // grid.LlenarGrid(myWeb.DataSetClase); 
            ////    }

            ////    grid.ColorRenglon(iRow, colorEjecucionExito); 
            ////}
            ////iBusquedasEnEjecucion--;
            ////// grid.SetValue(iRow, 4, sIdFarmacia); 
            ////Cursor.Current = Cursors.Default;
        }


        #region Datos para consulta 
        #endregion Datos para consulta

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                if (bExistenDatos)
                {
                    bExistenDatos = false; 
                    // grid.LlenarGrid(dtsDatosConsulta, true, false);
                    // grid.LlenarGrid(dtsDatosConsulta); 
                }

                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnImprimir.Enabled = true; 
                btnNuevo.Enabled = true;
            }
        }

        private void FrmReporteVentasContadoEnFarmacias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iBusquedasEnEjecucion != 0)
            {
                e.Cancel = true;
            }
        }

        private void FrmReporteVentasContadoEnFarmacias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true; 
                CargarEstados(); 
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEstados.SelectedIndex != 0)
            {
                //grid.Limpiar();
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
                cboFarmacias.Enabled = false; 
            }
        }
    }
}