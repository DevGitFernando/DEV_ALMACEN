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
//using SC_SolutionsSystem.ExportarDatos; 
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 
using DllTransferenciaSoft.wsCliente; 

namespace DllTransferenciaSoft.Auditoria
{
    public partial class FrmFechasUnidades : FrmBaseExt 
    {
        private enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Procesar = 4, 
            FechaSistemaSvr = 5, FechaServidorSvr = 6, 
            FechaSistema = 7, FechaServidor = 8, 
            DiferenciaFechaSistema = 9, DiferenciaFechaServidor = 10 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        clsLeerWebExt leerWeb;
        clsDatosCliente datosCliente;

        clsDatosCliente DatosCliente; 


        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        public FrmFechasUnidades()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(Transferencia.DatosApp, this.Name, "ObtenerInformacionUnidad");

            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name); 

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            DatosCliente = new clsDatosCliente(Transferencia.DatosApp, this.Name, ""); 

            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmFechasUnidades_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            CargarEstados(); 
            cboEstados.SelectedIndex = 0;
            chkTodos.Checked = false; 

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            {
                cboEstados.Focus();  
            }
            else
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false; 
            } 
        }

        private void CargarEstados()
        {
            string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) "; 
            cboEstados.Clear();
            cboEstados.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al Cargar la Lista de Estados."); 
            }
            else
            {
                if ( leer.Leer() )
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta(); 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //ExportarExcel();
        }
        #endregion Botones

        #region Eventos
        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql = string.Format(" Set DateFormat YMD 	Select U.IdFarmacia, U.Farmacia, U.UrlFarmacia, 0 as Procesar, " +
                 "      M.Valor as ValorSvr, IsNull(convert(varchar(16), getdate(), 120), '') as FechaServidor  " + 
                 " " + 
 	             " From vw_Farmacias_Urls U (NoLock) " + 
                 " Left Join Net_CFGC_Parametros M (NoLock)  " + 
	             "	    On ( U.IdEstado = M.IdEstado and U.IdFarmacia = M.IdFarmacia )  " +
                 " Where U.StatusUrl = 'A' and U.IdEstado = '{0}' and M.NombreParametro = '{1}' order by IdFarmacia ",
                    cboEstados.Data, "FechaOperacionSistema"); 
            
            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "cboEstados_SelectedIndexChanged"); 
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase); 
            }

        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }
        #endregion Eventos 

        #region Funciones y Procidimientos Privados 
        private void IniciarConsulta()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito);
            grid.SetValue((int)Cols.FechaSistema, "");
            grid.SetValue((int)Cols.FechaServidor, "");
            grid.SetValue((int)Cols.DiferenciaFechaSistema, "");
            grid.SetValue((int)Cols.DiferenciaFechaServidor, ""); 


            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread = new Thread(this.ObtenerInformacionUnidad);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.Farmacia);
                    _workerThread.Start(i);
                }
            }
        }

        private void ObtenerInformacionUnidad(object Renglon)
        {
            clsLeerWebExt leerLocal; 
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            string sFechaSistemaSvr = grid.GetValue(iRow, (int)Cols.FechaSistemaSvr);
            string sFechaServidorSvr = grid.GetValue(iRow, (int)Cols.FechaServidorSvr);


            // int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando";

            string sSql = string.Format("Set DateFormat YMD Select M.Valor as Valor, " +
                    " IsNull(convert(varchar(16), getdate(), 120), '') as FechaUnidad,  " + 
                    " datediff(dd, '{3}', M.Valor) as DiferenciaFechaSistema, " +
                    " datediff(mi, '{4}:00', getdate()) as DiferenciaFechaServidor  " + 
                    " From Net_CFGC_Parametros M (NoLock) " +
                    " Where M.IdEstado = '{0}' and M.IdFarmacia = '{1}' and M.NombreParametro = '{2}' ",
                    cboEstados.Data, sIdFarmacia, "FechaOperacionSistema", 
                    sFechaSistemaSvr, sFechaServidorSvr ); 
            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;

            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.ColorRenglon(iRow, colorEjecutando); 
                leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
                try
                {
                    if (!leerLocal.Exec(sSql))
                    {
                        bExito = false;
                    }
                    else 
                    {
                        bExito = true; 
                        leerLocal.Leer();
                        grid.SetValue(iRow, (int)Cols.FechaSistema, leerLocal.Campo("Valor"));
                        grid.SetValue(iRow, (int)Cols.FechaServidor, leerLocal.Campo("FechaUnidad"));

                        grid.SetValue(iRow, (int)Cols.DiferenciaFechaSistema, leerLocal.Campo("DiferenciaFechaSistema"));
                        grid.SetValue(iRow, (int)Cols.DiferenciaFechaServidor, leerLocal.Campo("DiferenciaFechaServidor"));  


                    }
                }
                catch { }

                if (bExito)
                {
                    // sResultado = "Exitó";
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
                else
                {
                    // sResultado = "Falló"; 
                    grid.ColorRenglon(iRow, colorEjecucionError); 
                }

                // grid.SetValue(iRow, 5, sResultado);
            }
            iBusquedasEnEjecucion--;
        }
        #endregion Funciones y Procidimientos Privados

        #region Exportar Excel 
        private void ExportarExcel() 
        {
            //clsExportarExcelPlantilla xpExcel;

            //// int iRenglon = 9;
            //int iRow = 9;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ADT_INF_SVR_UNIDAD.xls";

            //DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ADT_INF_SVR_UNIDAD.xls", DatosCliente); 


            //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //xpExcel.AgregarMarcaDeTiempo = true;

            //this.Cursor = Cursors.Default;
            //if (xpExcel.PrepararPlantilla())
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    xpExcel.GeneraExcel();

            //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //    xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //    xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToShortDateString(), 5, 2);

            //    for (int iRen = 1; iRen <= grid.Rows; iRen++) 
            //    {
            //        if (grid.GetValueBool(iRen, (int)Cols.Procesar)) 
            //        { 
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.IdFarmacia), iRow, 2);
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.Farmacia), iRow, 3);
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.FechaSistemaSvr), iRow, 4);
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.FechaServidorSvr), iRow, 5);

            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.FechaSistema), iRow, 6);
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.FechaServidor), iRow, 7);

            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.DiferenciaFechaSistema), iRow, 8);
            //            xpExcel.Agregar(grid.GetValue(iRen, (int)Cols.DiferenciaFechaServidor), iRow, 9);
            //            iRow++; 
            //        } 
            //    }

            //    // Finalizar el Proceso 
            //    xpExcel.CerrarDocumento();

            //    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //    {
            //        xpExcel.AbrirDocumentoGenerado();
            //    }
            //}
            //this.Cursor = Cursors.Default;
        }
        #endregion Exportar Excel
    }
}
