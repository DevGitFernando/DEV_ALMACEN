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

namespace Almacen.TraspasosEstatales
{
    public partial class FrmEdoSeguimientoTraspasoSalidas : FrmBaseExt
    {
        enum Cols
        {
            IdFarmacia = 1, NombreFarmacia = 2, Url = 3, Folio = 4, Fecha = 5, EstadoRecibe = 6, Procesar = 7,
            Status = 8, FolioEntrada = 9, FechaRegistro = 10
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        

        string sSqlFarmacias = "";
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);

        public FrmEdoSeguimientoTraspasoSalidas()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdTransferencia, this);
            grid.AjustarAnchoColumnasAutomatico = true;


            // grid.EstiloGrid(eModoGrid.SeleccionSimple);
            grid.Ordenar(5);
            grid.Ordenar(7);
            grid.Ordenar(8); 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);            

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;

            CargarFarmaciasDestino();
            cboFarmaciasDestino.Focus();
            groupBox1.Enabled = true;
        }

        #region Botones

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
        }

        private void btnActivarServicios_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            IniciarServicios();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iBusquedasEnEjecucion = 0;
            grid = new clsGrid(ref grdTransferencia, this);
            grid.Limpiar(false);
            //CargarFarmaciasDestino();
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add();
            cboFarmaciasDestino.SelectedIndex = 0;
            groupBox1.Enabled = true;
            chkTodas.Checked = false;
        }
        #endregion Botones

        #region Funciones 
        private void CargarFarmaciasGrid()
        {
            string sWhereEdoRecibe = "";

            if (cboEstados.SelectedIndex != 0)
            {
                sWhereEdoRecibe = string.Format(" and vwt.IdEstadoRecibe = '{0}' ", cboEstados.Data);
            }

            if (cboFarmaciasDestino.SelectedIndex == 0)
            {
                sSqlFarmacias = string.Format(" SELECT vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) as Fecha, vwt.IdEstadoRecibe " +
                            " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " +
                            " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) " +
                            " ON ( vwf.IdEstado = vwt.IdEstadoRecibe " +
                            " AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) " +
                            " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}'  " +
                            " AND convert(varchar(10),vwt.FechaReg,120) BETWEEN '{2}' AND  '{3}'  {4}  " +
                            " ORDER BY Fecha, Folio ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereEdoRecibe);
            }
            else
            {
                sSqlFarmacias = string.Format(" SELECT vwf.IdFarmacia, vwf.Farmacia, vwf.UrlFarmacia, vwt.Folio, convert(varchar(10),vwt.FechaReg,120) AS Fecha, vwt.IdEstadoRecibe " +
                           " FROM vw_TransferenciaEnvio_Enc AS vwt (NoLock) " +
                           " LEFT JOIN vw_Farmacias_Urls AS vwf (NoLock) " +
                           " ON ( vwf.IdEstado = vwt.IdEstadoRecibe " +
                           " AND vwf.IdFarmacia = vwt.IdFarmaciaRecibe ) " +
                           " WHERE vwt.IdEstadoEnvia = '{0}' AND vwt.IdFarmaciaEnvia = '{1}' AND vwt.IdFarmaciaRecibe = '{2}' " +
                           " AND convert(varchar(10),vwt.FechaReg,120) BETWEEN '{3}' AND  '{4}'  {5} " +
                           " ORDER BY Fecha, Folio ", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboFarmaciasDestino.Data,
                           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereEdoRecibe);
            }

            chkTodas.Checked = false; 

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
        private void CargarFarmaciasDestino()
        {
            cboFarmaciasDestino.Clear();
            cboFarmaciasDestino.Add("0", "<< Seleccione >>");   
            query.MostrarMsjSiLeerVacio = false;

            leer.DataSetClase = query.FarmaciasTransferencia(DtGeneral.EmpresaConectada, iEsEmpresaConsignacion, cboEstados.Data, DtGeneral.FarmaciaConectada, "", true, "CargarFarmaciasDestino()");

            if (leer.Leer())
            {
                cboFarmaciasDestino.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
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
                grid.SetValue(i, (int)Cols.FechaRegistro, " ");

                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread = new Thread(this.ConsultarTransferenciaFarmacia);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.IdFarmacia) + grid.GetValue(i, (int)Cols.NombreFarmacia);
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
            string sEstadoRecibe = grid.GetValue(iRow, (int)Cols.EstadoRecibe);
            string sStatus = ""; 

            ////string sSql = string.Format(" SELECT Folio, (case when Folio = '*' Then Status Else 'E' End ) as Status, StatusTransferencia " +
            ////                " FROM vw_TransferenciaEnvio_Enc (NoLock) " + 
            ////                " WHERE IdEstadoEnvia = '{0}' AND IdFarmaciaRecibe = '{1}' AND Folio = '{2}' ",
            ////                DtGeneral.EstadoConectado, sIdFarmacia, sFolio);

            string sSql = string.Format(" SELECT Folio, FolioReferenciaEntrada, FechaRegistroEntrada, " +
                        " Status, StatusTransferencia " +
                        " FROM vw_TransferenciaEnvio_Enc (NoLock) " +
                        " WHERE IdEstadoEnvia = '{0}' and IdEstadoRecibe = '{1}' and IdFarmaciaEnvia = '{2}' " + 
                        " AND IdFarmaciaRecibe = '{3}' AND Folio = '{4}' ",
                        DtGeneral.EstadoConectado, sEstadoRecibe, DtGeneral.FarmaciaConectada, sIdFarmacia, sFolio);


            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, 8, "Procesando");
            iBusquedasEnEjecucion++;

            clsLeerWebExt myWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.GrabarError(myWeb, sValor + " -- " + sUrl, "ConsultarTransferenciaFarmacia()", "");
                grid.SetValue(iRow, (int)Cols.Status, ""); 
                grid.ColorRenglon(iRow, colorEjecucionError);
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
                    if (!(sStatus == "A" || sStatus == "E" || sStatus == "I" || sStatus == "T"))
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
                                ActualizaStatusTransferencia(sFolio);
                            }
                        }

                        if (sStatus == "T")
                        {
                            grid.SetValue(iRow, (int)Cols.Status, "TRANSFERENCIA EN TRANSITO");
                        }
                    }

                    grid.SetValue(iRow, (int)Cols.FolioEntrada, myWeb.Campo("FolioReferenciaEntrada"));
                    grid.SetValue(iRow, (int)Cols.FechaRegistro, myWeb.Campo("FechaRegistroEntrada"));
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
            }
        }

        private void FrmEdoSeguimientoTraspasoSalidas_Load(object sender, EventArgs e)
        {
            CargarEstadosFiliales();
            btnNuevo_Click(null, null);
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue(7, chkTodas.Checked);
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

        #region Actualizar_Status_Transferencia
        private void ActualizaStatusTransferencia(string Folio)
        {

            string sSql = string.Format(" Update TransferenciasEnvioEnc Set Status = 'I' " +
                          " Where IdEmpresa = '{0}' And IdEstadoEnvia = '{1}' And IdFarmaciaEnvia = '{2}' And FolioTransferencia = '{3}' and Status <> 'I' ",
                          DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Folio);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ActualizaStatusTransferencia()");
                General.msjError("Ocurrió un error al actualizar el status de la Transferencia");
            }
        }
        #endregion Actualizar_Status_Transferencia

        #region Control de Estados Filiales
        private void CargarEstadosFiliales()
        {
            string sSql = string.Format(
                "Select IdEstado, NombreEstado as Estado \n" +
                "From vw_EmpresasEstados \n " +
                "Where IdEmpresa = '{0}' and IdEstado not in ( '{1}' ) \n " +
                " Order by IdEstado ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado);

            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstadosFiliales()");
                General.msjError("Ocurrió un error al obtener la lista de Estados filiales.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0;
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = true;
                CargarFarmaciasDestino();
            }
        }
        #endregion Control de Estados Filiales        

        #region Evento_Combo_Farmacia_Destino
        private void cboFarmaciasDestino_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion Evento_Combo_Farmacia_Destino
    }
}
