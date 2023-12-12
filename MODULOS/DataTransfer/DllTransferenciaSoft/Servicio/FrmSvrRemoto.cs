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
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 
using DllTransferenciaSoft.wsCliente; 

namespace DllTransferenciaSoft.Servicio
{
    public partial class FrmSvrRemoto : FrmBaseExt 
    {
        enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Procesar = 4, Status = 5 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente;

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;

        public FrmSvrRemoto()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 

            grid = new clsGrid(ref grdExistencia, this);
            //grid.EstiloGrid(eModoGrid.SeleccionSimple);
            
            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmSvrRemoto_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            CargarEstados(); 
            cboEstados.SelectedIndex = 0;


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
                if (leer.Leer())
                {
                    cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarServicios(); 
        }
        #endregion Botones

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia, 0, '' " + 
                " From vw_Farmacias_Urls (NoLock) Where IdEstado = '{0}' order by IdFarmacia ", cboEstados.Data);

            if (chkFarmaciasActivas.Checked)
            {
                sSql = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia, 0, '' " +
                " From vw_Farmacias_Urls (NoLock) Where IdEstado = '{0}' and FarmaciaStatus = 'A' order by IdFarmacia ", cboEstados.Data);
            }

            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase); 
            }

        }

        private void IniciarServicios()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false; 
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito); 
            grid.SetValue((int)Cols.Status, ""); 

            for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.IniciarServicioFarmacia);
                _workerThread.Name = grid.GetValue(i, (int)Cols.Farmacia);
                _workerThread.Start(i);
            }
        }

        private void IniciarServicioFarmacia(object Renglon)
        {
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;
            
            bool bExito = false;
            string sResultado = "Conectando"; 

            //string sSql = string.Format(" Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, sum(Existencia) as Existencia  " +
            //    " from vw_ExistenciaPorCodigoEAN_Lotes " +
            //    " where IdEstado = '{0}' and IdFarmacia = '{1}' and IdClaveSSA_Sal = '{2}' " +
            //    " group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal ", DtGeneral.EstadoConectado, sIdFarmacia, txtClaveSSA.Text);

            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;


            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.SetValue(iRow, (int)Cols.Status, sResultado);
                DllTransferenciaSoft.wsCliente.wsCnnCliente cliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
                grid.ColorRenglon(iRow, colorEjecutando);

                try
                {
                    cliente.Url = sUrl;
                    bExito = cliente.TestConection(); 
                    // iValor = cliente.IniciarServicio("");
                }
                catch { }

                if (bExito)
                {
                    sResultado = "Exitó";
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
                else
                {
                    sResultado = "Falló"; 
                    grid.ColorRenglon(iRow, colorEjecucionError); 
                }

                grid.SetValue(iRow, (int)Cols.Status, sResultado);
            } 
            iBusquedasEnEjecucion--;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true; 
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        private void GenerarExcel()
        {
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ADT_Conexiones.xls";
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ADT_Conexiones.xls", DatosCliente);
            //int iRow = 2;
            //string sIdFarmacia = ""; 
            //string sFarmacia = "";
            //string sUrl = "";
            //string sStatus = ""; 

            //if (bRegresa)
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = true;
            //    //leer.DataSetClase = dtsExistencias;

            //    this.Cursor = Cursors.Default;
            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        xpExcel.GeneraExcel(); 

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            //        iRow++; 
            //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);

            //        iRow = 6;
            //        xpExcel.Agregar("Fecha de impresión: " + General.FechaSistema.ToString(), iRow, 2);
            //        iRow = 9; 

            //        for (int i = 1; i <= grid.Rows; i++)
            //        {
            //            if (grid.GetValueBool(i, (int)Cols.Procesar))
            //            {
            //                sIdFarmacia = grid.GetValue(i, (int)Cols.IdFarmacia);
            //                sFarmacia = grid.GetValue(i, (int)Cols.Farmacia);
            //                sUrl = grid.GetValue(i, (int)Cols.Url);
            //                sStatus = grid.GetValue(i, (int)Cols.Status);

            //                xpExcel.Agregar(sIdFarmacia, iRow, 2);
            //                xpExcel.Agregar(sFarmacia, iRow, 3);
            //                xpExcel.Agregar(sUrl, iRow, 6);
            //                xpExcel.Agregar(sStatus, iRow, 7);

            //                iRow++; 
            //            }
            //        }

            //        xpExcel.CerrarDocumento(); 

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void chkFarmaciasActivas_CheckedChanged(object sender, EventArgs e)
        {
            cboEstados_SelectedIndexChanged(null, null); 
        } 
    }
}
