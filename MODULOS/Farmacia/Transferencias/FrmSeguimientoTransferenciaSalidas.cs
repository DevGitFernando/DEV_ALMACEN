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

namespace Farmacia.Transferencias
{
    public partial class FrmSeguimientoTransferenciaSalidas : FrmBaseExt
    {
        enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Ur = 3, Folio = 4, Fecha = 5, Procesar = 6, Status = 7,
            FolioEntrada = 8, FechaEntrada = 9, Mensaje = 10 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;
        DataSet dtsFarmacias = new DataSet(); 

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iEjecutando = 0;
        int iExito = 0;
        int iError = 0;
        string sFormato = "###,###,###,###";

        string sSqlFarmacias = "";
        string sINI_Consulta = DtGeneral.CfgIniPuntoDeVenta; 

        public FrmSeguimientoTransferenciaSalidas()
        {
            InitializeComponent();

            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen)
            {
                sINI_Consulta = DtGeneral.CfgIniPuntoDeVenta_MA; 
            }


            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdTransferencia, this);
            // grid.EstiloGrid(eModoGrid.SeleccionSimple);
            grid.Ordenar(5);
            grid.Ordenar(6);
            grid.Ordenar(7); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);            

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;

            CargarComboEstados();
            CargarComboFarmaciasDestino();
            cboFarmaciasDestino.Focus();
            FrameFarmacias.Enabled = true;

            if (!DtGeneral.EsAdministrador)
            {
                grdTransferencia.Sheets[0].Columns[((int)Cols.Farmacia) - 1].Width += grdTransferencia.Sheets[0].Columns[((int)Cols.Mensaje) - 1].Width; 
                grdTransferencia.Sheets[0].Columns[((int)Cols.Mensaje) - 1].Width = 0; 
            }

            grid.AjustarAnchoColumnasAutomatico = true; 
        }

        #region Botones

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            FrameFarmacias.Enabled = false;
            IniciarServicios();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iBusquedasEnEjecucion = 0;
            iEjecutando = 0;
            iExito = 0;
            iError = 0;

            lblConsultando.Text = iEjecutando.ToString(sFormato);
            lblFinExito.Text = iExito.ToString(sFormato);
            lblFinError.Text = iError.ToString(sFormato);

            rdoStatus_01_NoAplicadas.Checked = true;
            grid = new clsGrid(ref grdTransferencia, this);
            grid.Limpiar(false);


            FrameFarmacias.Enabled = true;
            chkTodas.Checked = false;

            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias; 
        }
        #endregion Botones

        #region Funciones 
        private void CargarFarmaciasGrid()
        {
            string sFiltro = "";

            if(rdoStatus_01_NoAplicadas.Checked)
            {
                sFiltro = " and TransferenciaAplicada = 0 "; 
            }

            if(rdoStatus_02_Aplicadas.Checked)
            {
                sFiltro = " and TransferenciaAplicada = 1 ";
            }


            if (cboFarmaciasDestino.SelectedIndex == 0)
            {
                sSqlFarmacias = string.Format(" SELECT Distinct vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) as Fecha \n " +
                    " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " +
                    " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) ON ( vwf.IdEstado = vwt.IdEstadoRecibe AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) \n " +
                    " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}' and vwt.IdEstadoRecibe = '{2}' \n " +
                    " AND convert(varchar(10),vwt.FechaReg, 120) BETWEEN '{3}' AND  '{4}'   {5} \n " +
                    " ORDER BY Fecha, Folio ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    cboEstados.Data, 
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sFiltro);
            }
            else
            {
                sSqlFarmacias = string.Format(" SELECT Distinct vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) AS Fecha \n " +
                    " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " +
                    " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) ON ( vwf.IdEstado = vwt.IdEstadoRecibe AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) \n " +
                    " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}' and vwt.IdEstadoRecibe = '{2}' AND vwt.IdFarmaciaRecibe = '{3}' \n " +
                    " AND convert(varchar(10),vwt.FechaReg,120) BETWEEN '{4}' AND  '{5}'   {6} \n" +
                    " ORDER BY Fecha, Folio ", 
                    DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                    cboEstados.Data, cboFarmaciasDestino.Data,
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sFiltro);
            }

            chkTodas.Checked = false;

            iEjecutando = 0;
            iExito = 0;
            iError = 0;

            lblConsultando.Text = iEjecutando.ToString(sFormato);
            lblFinExito.Text = iExito.ToString(sFormato);
            lblFinError.Text = iError.ToString(sFormato);

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmaciasGrid()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                grid.Limpiar(false);
                grid.LlenarGrid(leer.DataSetClase);
            }
        }

        private void CargarComboEstados()
        {
            cboEstados.Clear();
            cboEstados.Add();

            cboEstados.Add(query.EstadosConFarmacias("CargarComboEstados()"), true, "IdEstado", "NombreEstado");

            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneral.EstadoConectado;
            cboEstados.Enabled = GnFarmacia.Transferencias_Interestatales__Farmacias;
        }

        private void CargarComboFarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("0", "<< Seleccione >>");   
            ////query.MostrarMsjSiLeerVacio = false;

            dtsFarmacias = query.Farmacias("CargarFarmacias");

            ////leer.DataSetClase = query.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias");

            ////if (leer.Leer())
            ////{
            ////    cboFarmaciasDestino.Add(leer.DataRowsClase, true, "IdFarmacia", "NombreFarmacia");
            ////}

            ////cboFarmaciasDestino.SelectedIndex = 0;

            ////query.MostrarMsjSiLeerVacio = true;
        }

        private void Cargar_FarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("*", "<< Seleccione >>");
            string sFiltro = string.Format(" IdEstado = '{0}' ", cboEstados.Data);

            if (cboEstados.SelectedIndex != 0)
            {
                cboFarmaciasDestino.Filtro = sFiltro;
                cboFarmaciasDestino.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            } 

            cboFarmaciasDestino.SelectedIndex = 0;
        }
        #endregion Funciones

        private void IniciarServicios()
        {
            iEjecutando = 0;
            iExito = 0;
            iError = 0;

            lblConsultando.Text = iEjecutando.ToString(sFormato);
            lblFinExito.Text = iExito.ToString(sFormato);
            lblFinError.Text = iError.ToString(sFormato);


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
                grid.SetValue(i, (int)Cols.Mensaje, " ");
                grid.ColorRenglon(i, colorEjecucionExito);

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
            string sUrl = grid.GetValue(iRow, (int)Cols.Ur);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;
            string sFolio = grid.GetValue(iRow, (int)Cols.Folio);
            string sStatus = ""; 

            ////string sSql = string.Format(" SELECT Folio, (case when Folio = '*' Then Status Else 'E' End ) as Status, StatusTransferencia " +
            ////                " FROM vw_TransferenciaEnvio_Enc (NoLock) " + 
            ////                " WHERE IdEstadoEnvia = '{0}' AND IdFarmaciaRecibe = '{1}' AND Folio = '{2}' ",
            ////                DtGeneral.EstadoConectado, sIdFarmacia, sFolio);

            string sSql = string.Format(" SELECT Folio, FolioReferenciaEntrada, FechaRegistroEntrada, " +
                        " Status, StatusTransferencia " +
                        " FROM vw_TransferenciaEnvio_Enc (NoLock) " +
                        " WHERE IdEstadoEnvia = '{0}' And IdFarmaciaEnvia = '{1}' And IdFarmaciaRecibe = '{2}' And Folio = '{3}' ",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sIdFarmacia, sFolio);


            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, (int)Cols.Status, "Procesando");
            iBusquedasEnEjecucion++;

            iEjecutando++;
            lblConsultando.Text = iEjecutando.ToString(sFormato);

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrl, sINI_Consulta, datosCliente);
            myWeb.TimeOut = (1000 * 60) * 1; 

            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(leer, myWeb.MensajeError + " -- " + sValor + " -- " + sUrl, "ConsultarTransferenciaFarmacia()", "");
                grid.SetValue(iRow, (int)Cols.Status, ""); 
                grid.ColorRenglon(iRow, colorEjecucionError);
                iError++;
                lblFinError.Text = iError.ToString(sFormato);

                grid.SetValue(iRow, (int)Cols.Mensaje, myWeb.MensajeError);  
            }
            else
            {
                if (!myWeb.Leer())
                {
                    grid.SetValue(iRow, (int)Cols.Status, "PENDIENTE DE RECIBIR");
                }
                else 
                {
                    sStatus = myWeb.Campo("Status"); 
                    if ( ! (sStatus == "A" || sStatus == "E" || sStatus == "I" || sStatus == "T")  )  
                    {
                        grid.SetValue(iRow, (int)Cols.Status, "STATUS DESCONOCIDO");
                    }
                    else
                    {
                        if (sStatus == "A")
                        {
                            grid.SetValue(iRow, (int)Cols.Status, "PENDIENTE DE INGRESAR");
                        }

                        if (sStatus == "E" || sStatus == "I")
                        {
                            grid.SetValue(iRow, (int)Cols.Status, "REGISTRADO");
                            if (myWeb.Campo("Status") == "I")
                            {
                                ActualizaStatusTransferencia(sFolio, iRow);
                            }
                        }

                        if (sStatus == "T")
                        {
                            grid.SetValue(iRow, (int)Cols.Status, "TRANSFERENCIA EN TRANSITO");
                        }
                    }

                    grid.SetValue(iRow, (int)Cols.FolioEntrada, myWeb.Campo("FolioReferenciaEntrada"));
                    grid.SetValue(iRow, (int)Cols.FechaEntrada, myWeb.Campo("FechaRegistroEntrada"));                    
                }

                grid.ColorRenglon(iRow, colorEjecucionExito);
                iExito++;
                lblFinExito.Text = iExito.ToString(sFormato); 
            }


            iBusquedasEnEjecucion--;
            iEjecutando--; 
            lblConsultando.Text = iEjecutando.ToString(sFormato); 
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
            }
        }

        private void FrmSeguimientoTransferenciaSalidas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(6, chkTodas.Checked);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (grid.Rows > 0)
            {
                grid.ExportarExcel();
            }
            else
            {
                General.msjUser("No existe información para exportar a Excel.");
            }
        }

        #region Actualizar_Status_Transferencia
        private void ActualizaStatusTransferencia(string Folio, int Renglon)
        {

            clsLeer leerActualizarStatus = new clsLeer(ref cnn);
            string sSql = string.Format(" Update TransferenciasEnvioEnc Set Status = 'I' " +
                          " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' and Status <> 'I' ",
                          DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio);

            if (!leerActualizarStatus.Exec(sSql))
            {
                grid.ColorCelda(Renglon, (int)Cols.Folio, colorEjecucionError); 
                grid.SetValue(Renglon, (int)Cols.Mensaje, leer.MensajeError);
                Error.GrabarError(leerActualizarStatus, "ActualizaStatusTransferencia()");
                ////General.msjError("Ocurrió un error al actualizar el status de la Transferencia");
            }
        }
        #endregion Actualizar_Status_Transferencia

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("*", "<<Seleccione>>");

            if (cboEstados.SelectedIndex != 0)
            {
                Cargar_FarmaciasDestino();
            }

            cboFarmaciasDestino.SelectedIndex = 0;
        }
    }
}
