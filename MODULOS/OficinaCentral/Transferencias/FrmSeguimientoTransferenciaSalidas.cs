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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Web.Transferencias;

namespace OficinaCentral.Transferencias
{
    public partial class FrmSeguimientoTransferenciaSalidas : FrmBaseExt
    {
        private enum Cols
        {
            IdEmpresa = 1, IdFarmacia = 2, Farmacia = 3, Url = 4, Folio = 5, 
            FechaRegistro = 6, Procesar = 7, Status = 8, FolioEntrada = 9, FechaEntrada = 10  
        }

        ////private enum Cols
        ////{
        ////    IdEmpresa = 1, IdFarmacia = 2, Farmacia = 3, Url = 4, Folio = 5,
        ////    FechaRegistro = 6, Procesar = 7, Status = 8, FolioEntrada = 9, FechaEntrada = 10
        ////}

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;
        wsOficinaCentral.wsCnnOficinaCentral conexionWeb;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        string sUrl;
        string sHost = "";

        string sSqlFarmacias = "";


        public FrmSeguimientoTransferenciaSalidas()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            conexionWeb = new wsOficinaCentral.wsCnnOficinaCentral();
            conexionWeb.Url = General.Url;


            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdTransferencia, this);
            // grid.EstiloGrid(eModoGrid.SeleccionSimple);


            grid.Ordenar((int)Cols.Folio);
            grid.Ordenar((int)Cols.FechaRegistro);
            grid.Ordenar((int)Cols.Procesar);
            grid.Ordenar((int)Cols.Status); 
            grid.Ordenar((int)Cols.FolioEntrada);
            grid.Ordenar((int)Cols.FechaEntrada); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;

            FrameSeleccion.Enabled = true;
            CargarEstados(); 
           
        }

        private void CargarEstados()
        {
            string sSql = "Select Distinct IdEstado, Estado as Nombre From vw_Farmacias (NoLock) Order by IdEstado "; 

            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Nombre");
                }
            }

            // cboEstados.Add(query.Estados(""), true, "IdEstado", "Nombre");

            

            cboEstados.SelectedIndex = 0;
            cboEstados.Focus(); 
        }

        #region Botones

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
            
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            FrameSeleccion.Enabled = false;
            FrameFechas.Enabled = false;
            FrameTodas.Enabled = false; 
            IniciarServicios();

        }       

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            Fg.IniciaControles(); 


            iBusquedasEnEjecucion = 0;
            grid = new clsGrid(ref grdTransferencia, this);
            grid.Limpiar(false);

        //CargarEstados();

            FrameSeleccion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameTodas.Enabled = true; 
            chkTodas.Checked = false;
        }

        #endregion Botones

        #region Funciones

        private void CargarFarmaciasGrid()
        {
            if (cboEstados.SelectedIndex != 0)
            {
                if (cboFarmacias.SelectedIndex != 0)
                {
                    if (cboFarmaciasDestino.SelectedIndex == 0)
                    {
                        sSqlFarmacias = string.Format(" SELECT vwf.IdEmpresa, vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) as Fecha " +
                                    " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " +
                                    " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) " +
                                    " ON ( vwf.IdEstado = vwt.IdEstadoEnvia " +
                                    " AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) " +
                                    " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}'  " +
                                    " AND convert(varchar(10),vwt.FechaReg,120) BETWEEN '{2}' AND  '{3}'  " +
                                    " ORDER BY Fecha, Folio ", cboEstados.Data, cboFarmacias.Data,
                                    General.FechaYMD(dtpFechaInicial.Value,"-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                    }                        
                    else
                    {
                        sSqlFarmacias = string.Format(" SELECT vwf.IdEmpresa, vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) AS Fecha " +
                                    " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " + 
                                    " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) " + 
                                    " ON ( vwf.IdEstado = vwt.IdEstadoEnvia " + 
                                    " AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) " + 
                                    " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}' AND vwt.IdFarmaciaRecibe = '{2}' " +
                                    " AND convert(varchar(10),vwt.FechaReg,120) BETWEEN '{3}' AND  '{4}'  " +
                                    " ORDER BY Fecha, Folio ", cboEstados.Data, cboFarmacias.Data, cboFarmaciasDestino.Data,
                                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                    }

                    string sUrlOrigen = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();  
                    clsLeerWebExt myWeb = new clsLeerWebExt(sUrlOrigen, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
                    chkTodas.Checked = false;

                    if (validarDatosDeConexion())
                    {
                        if (!myWeb.Exec(sSqlFarmacias))
                        {
                            // Error.GrabarError(leer, "CargarFarmaciasGrid()");
                            General.msjError("Ocurrió un error al obtener la lista de farmacias.");
                        }
                        else
                        {
                            grid.Limpiar(false);
                            if (myWeb.Leer())
                            {
                                grid.LlenarGrid(myWeb.DataSetClase);
                            }
                            else
                            {
                                General.msjUser("No se Encontro Información.");
                                cboFarmacias.Focus();
                            }

                        }
                    }
                }
                else
                {
                    General.msjUser("Seleccione una Farmacia por Favor.");
                    cboFarmacias.Focus();
                }
            }
            else
            {                
                General.msjUser("Seleccione un Estado por Favor.");
                cboEstados.Focus();
            }
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");
            query.MostrarMsjSiLeerVacio = false;
            
            leer.DataSetClase = query.Farmacias_Urls(cboEstados.Data,"CargarFarmacias");

            if (leer.Leer())
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "NombreFarmacia");
            }
            
            cboFarmacias.SelectedIndex = 0;

            query.MostrarMsjSiLeerVacio = true;
        }

        private void CargarFarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("0", "<< Seleccione >>");
            query.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = query.Farmacias_Urls(cboEstados.Data, "CargarFarmacias");

            if (leer.Leer())
            {
                cboFarmaciasDestino.Add(leer.DataRowsClase, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmaciasDestino.SelectedIndex = 0;

            query.MostrarMsjSiLeerVacio = true;
        }
        #endregion Funciones


        private void IniciarServicios()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnActivarServicios.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start(); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                grid.SetValue(i, (int)Cols.Status, " ");
                grid.SetValue(i, (int)Cols.FolioEntrada, " ");
                grid.SetValue(i, (int)Cols.FechaEntrada, " ");  

                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread = new Thread(this.ConsultarTransferenciaFarmacia);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.Farmacia) + grid.GetValue(i, (int)Cols.Folio);
                    _workerThread.Start(i);
                }
            }
            
        }

        private void ConsultarTransferenciaFarmacia(object Renglon)
        {
            int iRow = (int)Renglon;
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;
            string sFolio = grid.GetValue(iRow, (int)Cols.Folio);

            ////private enum Cols
            ////{
            ////    IdEmpresa = 1, IdFarmacia = 2, Farmacia = 3, Url = 4, Folio = 5,
            ////    FechaRegistro = 6, Procesar = 7, Status = 8, FolioEntrada = 9, FechaEntrada = 10
            ////}

            string sSql = string.Format(" SELECT IdEmpresa, Folio, FolioReferenciaEntrada, FechaRegistroEntrada, " + 
                        " (case when Folio = '*' Then Status Else 'E' End ) as Status, StatusTransferencia " + 
                        " FROM vw_TransferenciaEnvio_Enc (NoLock) " +
                        " WHERE IdEstadoEnvia = '{0}' AND IdFarmaciaRecibe = '{1}' AND Folio = '{2}' ",
                        cboEstados.Data, sIdFarmacia, sFolio);

            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, (int)Cols.Status, "Procesando");
            iBusquedasEnEjecucion++;

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(sValor + " -- " + sUrl, "ConsultarTransferenciaFarmacia()");
                grid.SetValue(iRow, (int)Cols.Status, "");
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                if (myWeb.Leer())
                {
                    if (myWeb.Campo("Status") == "A")
                    {
                        grid.SetValue(iRow, (int)Cols.Status, "PENDIENTE DE INGRESAR");
                    }
                    if (myWeb.Campo("Status") == "E")
                    {
                        grid.SetValue(iRow, (int)Cols.Status, "REGISTRADO");
                    }

                    grid.SetValue(iRow, (int)Cols.FolioEntrada, myWeb.Campo("FolioReferenciaEntrada"));
                    grid.SetValue(iRow, (int)Cols.FechaEntrada, myWeb.Campo("FechaRegistroEntrada"));

                }
                else
                {
                    grid.SetValue(iRow, (int)Cols.Status, "PENDIENTE DE RECIBIR");
                }
                grid.ColorRenglon(iRow, colorEjecucionExito);

            }
            iBusquedasEnEjecucion--;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnActivarServicios.Enabled = true;
                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;

                FrameFechas.Enabled = true;
                FrameTodas.Enabled = true; 
            }
        }

        private void FrmSeguimientoTransferenciaSalidas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click_1(null, null);
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
            CargarFarmaciasDestino();
            grid.Limpiar(false);
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(6, chkTodas.Checked); 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
            }
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                //ActivarControles();
            }

            return bRegresa;
        }

        private void grdTransferencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iRow = grid.ActiveRow;
            FrmTransferenciaSalidasFarmacias f = new FrmTransferenciaSalidasFarmacias(DatosDeConexion);

            f.MostrarFolioTransferencia(grid.GetValue(iRow, (int)Cols.IdEmpresa), cboEstados.Data, cboFarmacias.Data,
                grid.GetValue(iRow, (int)Cols.Folio), DatosDeConexion); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (grid.Rows > 0)
            {
                grid.ExportarExcel();
            }
            else
            {
                General.msjUser("No existe informacion para exportar a Excel.");
            }
        } 
    }
}
