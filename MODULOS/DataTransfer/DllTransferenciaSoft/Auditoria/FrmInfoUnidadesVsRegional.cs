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
    public partial class FrmInfoUnidadesVsRegional : FrmBaseExt 
    {
        private enum Cols
        {
            Nombre = 1, Procesar = 2, RegistrosUnidad = 3, RegistrosRegional = 4, Diferencia = 5, Porcentaje = 6 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        clsLeerWebExt leerWeb;
        clsDatosCliente datosCliente;

        DataSet dtsFarmacias;
        string sUrl;
        // string sHost = "";
        string sFormato = "#,###,##0.###0";

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        // private bool sortAscendingCol = true;
        int iBusquedasEnEjecucion = 0;
        int iBusquedasEnEjecucionUnidades = 0;

        public FrmInfoUnidadesVsRegional()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(Transferencia.DatosApp, this.Name, "ObtenerInformacionUnidad");

            leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, Transferencia.DatosApp, this.Name);

            grid = new clsGrid(ref grdCatalogos, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            
            //lblConsultando.BackColor = colorEjecutando;
            //lblFinExito.BackColor = colorEjecucionExito;
            //lblFinError.BackColor = colorEjecucionError; 

            // Preparar el Grid para Ordenamiento de Columnas 
            grdCatalogos.Sheets[0].Columns[((int)Cols.Procesar) - 1].AllowAutoSort = true;
            grdCatalogos.Sheets[0].Columns[((int)Cols.RegistrosRegional) - 1].AllowAutoSort = true;
            grdCatalogos.Sheets[0].Columns[((int)Cols.RegistrosUnidad) - 1].AllowAutoSort = true;
            grdCatalogos.Sheets[0].Columns[((int)Cols.Diferencia) - 1].AllowAutoSort = true; 
            grdCatalogos.Sheets[0].Columns[((int)Cols.Porcentaje) - 1].AllowAutoSort = true; 
        }

        private void FrmInfoUnidadesVsRegional_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();

            CargarEstados();
            CargarCatalogos();

            cboEstados.Enabled = true;
            cboFarmacias.Enabled = true; 
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;

            lblPorcentaje.Text = "0.0000";
            lblDiferencias.Text = "0";

            chkTodos.Checked = false;

            if (Transferencia.ServicioEnEjecucion == TipoServicio.OficinaCentralRegional)
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

            CargarFarmacias(); 
        }

        private void CargarFarmacias()
        {
            dtsFarmacias = new DataSet();
            string sSql = string.Format(" Set DateFormat YMD 	Select U.IdEstado, U.IdFarmacia, " + 
                " (U.IdFarmacia + ' -- ' + U.Farmacia) as Farmacia, U.UrlFarmacia " +
                 " From vw_Farmacias_Urls U (NoLock) " +
                 " Where U.StatusUrl = 'A' " +
                 " Order by IdEstado, IdFarmacia ");

            cboFarmacias.Clear();
            cboFarmacias.Add(); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFarmacias()"); 
                General.msjError("Ocurrió un error al obtener la lista de farmacias."); 
            }
            else
            {
                dtsFarmacias = leer.DataSetClase;
            }

            cboFarmacias.SelectedIndex = 0; 
        }

        private void CargarCatalogos()
        {
            string sSql = " Select NombreTabla, 0, 0, 0, 0, 0 " + 
                " From CFGC_EnvioDetalles (NoLock) " + 
                " Where IdGrupo > 0 Order By IdOrden "; 

            grid.Limpiar(false); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarCatalogos()");
                General.msjError("Ocurrió un error al cargar la lista de catalogos.");
            }
            else
            {
                grid.LlenarGrid(leer.DataSetClase); 
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsultaRegional();
        }

        private void btnObtenerTransferencias_Click(object sender, EventArgs e)
        {
            IniciarConsulta();
        }

        private void porcentajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PorcentajeGeneral(); 
        }

        private void diferenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diferencias(); 
        }

        private void Diferencias()
        {
            double dPorc = 0;
            double dUnidad = 0;
            double dRegional = 0;

            for (int i = 1; i <= grid.Rows; i++)
            {
                dPorc = grid.GetValueDou(i, (int)Cols.Porcentaje);
                if (dPorc > 0)
                {
                    dUnidad = grid.GetValueDou(i, (int)Cols.RegistrosUnidad); 
                    dRegional = grid.GetValueDou(i, (int)Cols.RegistrosRegional);
                    grid.SetValue(i, (int)Cols.Diferencia, (dUnidad - dRegional));
                }
            } 

            lblDiferencias.Text = grid.TotalizarColumna((int)Cols.Diferencia).ToString();
        }

        private void PorcentajeGeneral()
        {
            double dPorc = 0;
            double dPorcGral = 0; 
            double dElementos = 0;
            bool bProceso = false; 

            for (int i = 1; i <= grid.Rows; i++)
            {
                dPorc = grid.GetValueDou(i, (int)Cols.Porcentaje);
                if (dPorc > 0)
                {
                    bProceso = true;
                    dElementos++; 
                    dPorcGral += dPorc;
                }
            }

            if (bProceso)
            {
                dPorcGral = (dPorcGral / dElementos);
            }
            lblPorcentaje.Text = dPorcGral.ToString(sFormato); 
        }

        #endregion Botones

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            if (cboEstados.SelectedIndex != 0)
            {
                string sFiltro = string.Format(" IdEstado = '{0}' ", cboEstados.Data);
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro), true, "IdFarmacia", "Farmacia");
            }

            cboFarmacias.SelectedIndex = 0; 
        }

        private void IniciarConsultaRegional()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            cboEstados.Enabled = false;
            cboFarmacias.Enabled = false; 
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorColumna((int)Cols.RegistrosRegional, colorEjecucionExito); 
            grid.SetValue((int)Cols.RegistrosRegional, "0");
            grid.SetValue((int)Cols.Diferencia, "0");
            lblDiferencias.Text = "0";
            lblPorcentaje.Text = "0.0000";
            // grid.SetValue((int)Cols.Porcentaje, "0");

            //////// 
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    Thread _workerThread_Regional = new Thread(this.GetDatosRegional);
                    _workerThread_Regional.Name = "GetDatosRegional__" + grid.GetValue(i, (int)Cols.Nombre);
                    _workerThread_Regional.Start(i);
                    // this.GetDatosRegional(i); 
                }
            }

        }

        private void IniciarConsulta()
        {
            btnNuevo.Enabled = false;
            btnObtenerInformacion.Enabled = false;
            cboEstados.Enabled = false;
            cboFarmacias.Enabled = false;
            tmEjecucionesUnidad.Enabled = true;
            tmEjecucionesUnidad.Start();

            // grid.ColorRenglon(colorEjecucionExito);
            grid.ColorColumna((int)Cols.RegistrosUnidad, colorEjecucionExito); 
            grid.SetValue((int)Cols.RegistrosUnidad, "0");
            grid.SetValue((int)Cols.Diferencia, "0");
            lblDiferencias.Text = "0";
            lblPorcentaje.Text = "0.0000";
            // grid.SetValue((int)Cols.Porcentaje, "0"); 


            //////// 
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    //Thread _workerThread_Regional = new Thread(this.GetDatosRegional);
                    //_workerThread_Regional.Name = "GetDatosRegional__" + grid.GetValue(i, (int)Cols.Nombre);
                    //_workerThread_Regional.Start(i);

                    Thread _workerThread = new Thread(this.ObtenerInformacionUnidad);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.Nombre);
                    _workerThread.Start(i); 
                }
            }
        }


        private string GetQuery(int Renglon)
        {
            string sRegresa = string.Format(" Select count(*) as Registros " +
                " From {0} (NoLock) " +
                " Where IdEstado = '{1}' and IdFarmacia = '{2}' ",
                grid.GetValue(Renglon, (int)Cols.Nombre), cboEstados.Data, cboFarmacias.Data);

            // sRegresa = ""; 

            return sRegresa; 
        }

        private void GetDatosRegional(object Renglon)
        {
            bool bExito = false; 
            int iRenglon = (int)Renglon;
            string sSql = GetQuery(iRenglon);
            clsLeer leerRegional = new clsLeer(ref cnn);
            clsLeerWebExt leerLocal; 


            iBusquedasEnEjecucion++;
            grid.ColorCelda(iRenglon, (int)Cols.RegistrosRegional, colorEjecutando);
            leerLocal = new clsLeerWebExt(General.Url, DtGeneral.CfgIniOficinaCentral, datosCliente);
            leerLocal.TimeOut = 300000;

            if (!leerLocal.Exec(sSql))
            {
                Error.LogError(leerLocal.MensajeError);
            }
            else
            {
                if (leerLocal.Leer())
                {
                    grid.SetValue(iRenglon, (int)Cols.RegistrosRegional, leerLocal.Campo("Registros"));
                }
                bExito = true;
            }
            iBusquedasEnEjecucion--;

            if (bExito)
            {
                grid.ColorCelda(iRenglon, (int)Cols.RegistrosRegional, colorEjecucionExito);
            }
            else
            {
                grid.ColorCelda(iRenglon, (int)Cols.RegistrosRegional, colorEjecucionError);
            }
        }

        private void ObtenerInformacionUnidad(object Renglon)
        {
            clsLeerWebExt leerLocal; 
            int iRow = (int)Renglon;
            // int iValor = -1;
            string sIdFarmacia = ""; // grid.GetValue(iRow, (int)Cols.IdFarmacia);
            // string sUrl = ""; //  grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            //////string sFechaSistemaSvr = grid.GetValue(iRow, (int)Cols.FechaSistemaSvr);
            //////string sFechaServidorSvr = grid.GetValue(iRow, (int)Cols.FechaServidorSvr); 


            // int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando"; 
            string sSql = GetQuery(iRow);
            double dUnidad = 0;
            double dRegional = 0; 


            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucionUnidades++;

            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                //grid.ColorRenglon(iRow, colorEjecutando);
                grid.ColorCelda(iRow, (int)Cols.RegistrosUnidad, colorEjecutando);
                leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
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
                        dUnidad = leerLocal.CampoDouble("Registros");
                        dRegional = grid.GetValueDou(iRow, (int)Cols.RegistrosRegional); 
                        grid.SetValue(iRow, (int)Cols.RegistrosUnidad, dUnidad);

                        //// Indicar la diferencia de Registros 
                        //grid.SetValue(iRow, (int)Cols.Diferencia, (dUnidad - dRegional)); 

                        //grid.SetValue(iRow, (int)Cols.NombreVersion, leerLocal.Campo("NombreVersion"));

                        //grid.SetValue(iRow, (int)Cols.Version, leerLocal.Campo("Version"));
                        //grid.SetValue(iRow, (int)Cols.FechaRegistro, leerLocal.Campo("FechaRegistro"));
                        //grid.SetValue(iRow, (int)Cols.HostName, leerLocal.Campo("HostName"));  


                    }
                }
                catch { }

                if (bExito)
                {
                    // sResultado = "Exitó";
                    //grid.ColorRenglon(iRow, colorEjecucionExito);
                    grid.ColorCelda(iRow, (int)Cols.RegistrosUnidad, colorEjecucionExito);
                }
                else
                {
                    // sResultado = "Falló"; 
                    //grid.ColorRenglon(iRow, colorEjecucionError);
                    grid.ColorCelda(iRow, (int)Cols.RegistrosUnidad, colorEjecucionError);
                }

                // grid.SetValue(iRow, 5, sResultado);
            }
            iBusquedasEnEjecucionUnidades--;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;

                PorcentajeGeneral(); 
            }
        }

        private void tmEjecucionesUnidad_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucionUnidades == 0)
            {
                tmEjecucionesUnidad.Stop();
                tmEjecucionesUnidad.Enabled = false;

                btnObtenerInformacion.Enabled = true;
                btnNuevo.Enabled = true;

                PorcentajeGeneral(); 
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
