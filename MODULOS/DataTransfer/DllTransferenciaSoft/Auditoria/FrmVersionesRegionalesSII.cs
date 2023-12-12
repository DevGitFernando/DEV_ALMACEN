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
using DllTransferenciaSoft.wsCliente; 

namespace DllTransferenciaSoft.Auditoria
{
    public partial class FrmVersionesRegionalesSII : FrmBaseExt 
    {
        private enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Procesar = 4, 
            IdVersion = 5, NombreVersion = 6, 
            Version = 7, FechaRegistro = 8, HostName = 9 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        clsLeerWebExt leerWeb;
        clsDatosCliente datosCliente; 


        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        // private bool sortAscendingCol = true;
        int iBusquedasEnEjecucion = 0;

        public FrmVersionesRegionalesSII()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(Transferencia.DatosApp, this.Name, "ObtenerInformacionUnidad");

            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name); 

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            
            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError; 

            // Preparar el Grid para Ordenamiento de Columnas 
            grdExistencia.Sheets[0].Columns[((int)Cols.Procesar) - 1].AllowAutoSort = true; 
            grdExistencia.Sheets[0].Columns[((int)Cols.Version) - 1].AllowAutoSort = true;

            cboTipo.Add("1", "Informativo");
            cboTipo.Add("2", "Silencioso");
            cboTipo.SelectedIndex = 0; 

        }

        private void FrmVersionesRegionalesSII_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            // CargarEstados();
            CargarRegionales();
            cboTipo.SelectedIndex = 0; 
            //cboEstados.SelectedIndex = 0;
            chkTodos.Checked = false;

            //if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentral)
            //{
            //    cboEstados.Focus();  
            //}
            //else
            //{
            //    cboEstados.Data = DtGeneral.EstadoConectado;
            //    cboEstados.Enabled = false; 
            //} 
        }

        private void CargarEstados()
        {
            ////string sSql = " Select Distinct IdEstado, Estado From vw_Farmacias_Urls (NoLock) "; 
            ////cboEstados.Clear();
            ////cboEstados.Add();

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "CargarEstados()");
            ////    General.msjError("Ocurrió un error al Cargar la Lista de Estados."); 
            ////}
            ////else
            ////{
            ////    if ( leer.Leer() )
            ////        cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            ////}
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta(); 
        }
        #endregion Botones

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////string sSql = string.Format(" Set DateFormat YMD 	Select U.IdFarmacia, U.Farmacia, U.UrlFarmacia, 0 as Procesar, " +
            ////     "      '', '', '', '', ''  " +
            ////     " From vw_Regionales_Urls U (NoLock) " + 
            ////     " Where U.StatusUrl = 'A' " + 
            ////     " Order by IdFarmacia ", cboEstados.Data); 
            

            ////grid.Limpiar();
            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "cboEstados_SelectedIndexChanged"); 
            ////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            ////}
            ////else
            ////{
            ////    grid.LlenarGrid(leer.DataSetClase); 
            ////}

        }

        private void CargarRegionales()
        {
            string sSql = string.Format(" Set DateFormat YMD 	" + 
                 " Select U.IdEstado, (U.Estado + ' ==> ' + U.Farmacia), U.UrlFarmacia, 0 as Procesar, " +
                 "      '', '', '', '', ''  " +
                 " From vw_Regionales_Urls U (NoLock) " +
                 " Where U.StatusUrl = 'A' " +
                 " Order by IdEstado, IdFarmacia " );


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

        
        private void IniciarConsulta()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito);
            grid.SetValue((int)Cols.IdVersion , "");
            grid.SetValue((int)Cols.NombreVersion, "");
            grid.SetValue((int)Cols.Version, "");
            grid.SetValue((int)Cols.FechaRegistro, "");
            grid.SetValue((int)Cols.HostName , ""); 


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

            //////string sFechaSistemaSvr = grid.GetValue(iRow, (int)Cols.FechaSistemaSvr);
            //////string sFechaServidorSvr = grid.GetValue(iRow, (int)Cols.FechaServidorSvr); 


            // int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando";

            string sSql = string.Format("Set DateFormat YMD " +
                    " Select Top 1 V.IdVersion, V.NombreVersion, V.Version, " +
                    " convert(varchar(20), V.FechaRegistro, 120) as FechaRegistro, V.HostName " +
                    " From Net_Versiones_Regional V (NoLock) " + 
                    " Where Tipo = '{0}' " + 
                    " Order By V.FechaRegistro Desc  ", cboTipo.Data); 
            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;

            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.ColorRenglon(iRow, colorEjecutando); 
                leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniOficinaCentral, datosCliente);
                leerLocal.TimeOut = 300000; 
                try
                {
                    if (!leerLocal.Exec(sSql))
                    {
                        bExito = false;
                        Error.LogError(sUrl + " =====> " + leerLocal.MensajeError); 
                    }
                    else 
                    {
                        bExito = true; 
                        leerLocal.Leer();
                        grid.SetValue(iRow, (int)Cols.IdVersion, leerLocal.Campo("IdVersion"));
                        grid.SetValue(iRow, (int)Cols.NombreVersion, leerLocal.Campo("NombreVersion"));

                        grid.SetValue(iRow, (int)Cols.Version, leerLocal.Campo("Version"));
                        grid.SetValue(iRow, (int)Cols.FechaRegistro, leerLocal.Campo("FechaRegistro"));
                        grid.SetValue(iRow, (int)Cols.HostName, leerLocal.Campo("HostName"));  


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
            grid.SetValue(4, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }
    }
}
