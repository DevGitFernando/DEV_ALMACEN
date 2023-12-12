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
using SC_SolutionsSystem.FuncionesGenerales;

// using DllFarmaciaSoft; 

namespace DllPedidosClientes.Reportes
{
    public partial class FrmTBC_NivelAbasto : FrmBaseExt 
    {
        private enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Jurisdiccion = 4, 
            Procesar = 5, 
            ClavesPerfil = 6, ClavesConExistencia = 7, 
            ClavesSinExistencia = 8, PorcentajeAbasto = 9
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        // clsLeerWebExt leerWeb;
        clsDatosCliente DatosCliente;
        clsListView list; 

        //clsExportarExcelPlantilla xpExcel; 
        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iBusquedasConExito = 0;
        int iBusquedasConError = 0;

        DataSet dtsUnidades;
        DataSet dtsResultados; 


        public FrmTBC_NivelAbasto()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ObtenerInformacionUnidad");

            // leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, DatosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name); 

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            list = new clsListView(lstClaves); 

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;

            if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Regional)
            {
                this.Height = 432;
                chkTodos.Visible = false; 

                lstClaves.Visible = true; 
                lstClaves.Top = grdExistencia.Top;
                lstClaves.Left = grdExistencia.Left;
                lstClaves.Width = grdExistencia.Width;
                lstClaves.Height = grdExistencia.Height; 
            } 
        }

        private void FrmTBC_NivelAbasto_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            grid.Limpiar();
            CargarUnidades(); 
            chkTodos.Checked = false;

            btnExportarExcel.Enabled = false;
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

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            switch (DtGeneralPedidos.TipoDeConexion)
            {
                case TipoDeConexion.Regional:
                    dts = GetInformacionRegional(Cadena);
                    break;

                case TipoDeConexion.Unidad:
                    dts = GetInformacionUnidad(Cadena);
                    break;

                case TipoDeConexion.Unidad_Directo:
                    dts = GetInformacionUnidad_Directo(Cadena);
                    break;

                default:
                    break;
            }

            return dts;
        }

        private DataSet GetInformacionRegional__OLD(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url; 
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            clsConexionSQL cnnLocal = new clsConexionSQL(); 
            cnnLocal.DatosConexion.Servidor = cnn.DatosConexion.Servidor;
            cnnLocal.DatosConexion.BaseDeDatos = cnn.DatosConexion.BaseDeDatos;
            cnnLocal.DatosConexion.Usuario = cnn.DatosConexion.Usuario;
            cnnLocal.DatosConexion.Password = cnn.DatosConexion.Password;
            cnnLocal.DatosConexion.Puerto = cnn.DatosConexion.Puerto;
            cnnLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 

            clsLeer leerRegional = new clsLeer(ref cnnLocal); 
            clsLeerWeb leerWeb = new clsLeerWeb(General.Url, DtGeneralPedidos.CfgIniSII_Regional, DatosCliente); 

            // leerRegional.Exec(Cadena);
            // dts = leerRegional.DataSetClase;

            leerWeb.Exec(Cadena);
            dts = leerWeb.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url; 
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionUnidad_Directo(string Cadena)
        {
            DataSet dts = new DataSet();

            if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);

                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                ////////leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                ////////conexionWeb.Url = sUrl;
                ////////DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                ////////DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }
        #endregion Conexiones

        private void CargarUnidades()
        {
            string sSql = string.Format(" Set DateFormat YMD 	" + 
                 " Select U.IdFarmacia, U.Farmacia, U.UrlFarmacia, F.Jurisdiccion, 0 as Procesar " +
                 " From vw_Farmacias_Urls U (NoLock) " + 
                 " Inner Join vw_Farmacias F On ( U.IdEstado = F.IdEstado and U.IdFarmacia = F.IdFarmacia ) " + 
                 " Where U.StatusUrl = 'A' and U.IdEstado = '{0}' " + 
                 " Order by IdFarmacia ",
                 DtGeneralPedidos.EstadoConectado, "FechaOperacionSistema");

            grid.Limpiar();
            if (dtsUnidades != null)
            {
                chkTodos.Checked = false;
                grid.LlenarGrid(dtsUnidades); 
            }
            else 
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarUnidades");
                    General.msjError("Ocurrió un error al obtener la lista de Unidades.");
                }
                else
                {
                    dtsUnidades = leer.DataSetClase; 
                    grid.LlenarGrid(leer.DataSetClase); 
                }
            }
        }

        private void IniciarConsulta()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito);
            grid.SetValue((int)Cols.ClavesPerfil, "");
            grid.SetValue((int)Cols.ClavesConExistencia, "");
            grid.SetValue((int)Cols.ClavesSinExistencia, "");
            grid.SetValue((int)Cols.PorcentajeAbasto, "");

            dtsResultados = null; 
            iBusquedasConExito = 0;
            iBusquedasConError = 0;
            lblFinExito.Text = "0";
            lblFinError.Text = "0";
            lblConsultando.Text = "0"; 

            //for (int i = 1; i <= grid.Rows; i++)
            {
            //    if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    int i = 1; 
                    Thread _workerThread = new Thread(this.ObtenerInformacionUnidad);
                    _workerThread.Name = grid.GetValue(i, (int)Cols.Farmacia);
                    _workerThread.Start(i);
                }
            }
        }

        private void ObtenerInformacionUnidad(object Renglon)
        {
            clsLeer leerLocal;
            clsLeer leerDatos = new clsLeer(); 
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneralPedidos.EstadoConectado + "-" + sIdFarmacia;

            ////string sFechaSistemaSvr = grid.GetValue(iRow, (int)Cols.FechaSistemaSvr);
            ////string sFechaServidorSvr = grid.GetValue(iRow, (int)Cols.FechaServidorSvr); 


            // int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando"; 

            string sSql = string.Format(" Exec spp_Rpt_AbastoClaves '{0}', '{1}' ", DtGeneralPedidos.EstadoConectado, sIdFarmacia);
            sSql = string.Format(" Exec spp_Rpt_AbastoClaves_Global '{0}'  ", DtGeneralPedidos.EstadoConectado);            
            
            //sSql += "\n " + string.Format("Select top 1 * From tmpRptAbastoClaves (NoLock) ");

            // Jesús Díaz 2K120312.2118 
            /* 
            sSql += "\n " + string.Format(
                " Select top 1 IdEstado, Estado, IdFarmacia, Farmacia, getdate() as FechaReporte, " + 
                " Count(*) As TotalClaves, " +
                " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, " +
                " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, " +
                " PorcAbasto " + 
                " From tmpRptAbastoClaves (Nolock) " +
                " Group By IdEstado, Estado, IdFarmacia, Farmacia, PorcAbasto");
            */ 

            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++; 
            lblConsultando.Text = iBusquedasEnEjecucion.ToString(); 


            //if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.ColorRenglon(iRow, colorEjecutando); 
                // leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente); 
                try
                {
                    leerLocal = new clsLeer(); 
                    leerLocal.Reset();
                    leerLocal.DataSetClase = GetInformacion(sSql);

                    if (leerLocal.SeEncontraronErrores())
                    {
                        bExito = false;
                        Error.LogError(leer.MensajeError); 
                    }
                    else 
                    {
                        bExito = true;
                        if (!leerLocal.Leer())
                        {
                            bExito = true; 
                        }
                        else
                        {
                            // Generación de reporte para Excel
                            if (dtsResultados == null)
                            {
                                dtsResultados = leerLocal.DataSetClase.Clone();
                            }

                            dtsResultados.Tables[0].Merge(leerLocal.DataSetClase.Tables[0]);

                            // string[] sCols = { "IdFarmacia", "Farmacia", "Url", "Juris", "Procesar", "TotalClaves", "ConExistencia", "SinExistencia", "PorcAbasto" };
                            string[] sCols = { "IdFarmacia", "Farmacia", "TotalClaves", "ConExistencia", "SinExistencia", "PorcAbasto" }; 
                            leerDatos.DataSetClase = leerLocal.DataSetClase.Clone();
                            leerDatos.DataSetClase = dtsResultados; 
                            leerDatos.FiltrarColumnas(1, sCols);  

                            ////grid.SetValue(iRow, (int)Cols.ClavesPerfil, leerDatos.Campo("TotalClaves"));
                            ////grid.SetValue(iRow, (int)Cols.ClavesConExistencia, leerDatos.Campo("ConExistencia"));

                            ////grid.SetValue(iRow, (int)Cols.ClavesSinExistencia, leerDatos.Campo("SinExistencia"));
                            ////grid.SetValue(iRow, (int)Cols.PorcentajeAbasto, leerDatos.Campo("PorcAbasto")); 

                            //grid.Limpiar(false); 
                            grid.LlenarGrid(leerDatos.DataSetClase);

                            list.Limpiar();
                            list.CargarDatos(leerDatos.DataSetClase, true, true);
                            list.TituloColumna(1, "Unidad");
                            list.TituloColumna(2, "Nombre");
                            list.TituloColumna(3, "Claves de Perfil");
                            list.TituloColumna(4, "Claves con existencia");
                            list.TituloColumna(5, "Claves sin existencia");
                            list.TituloColumna(6, "Porcentaje de abasto");


                        }
                    }
                }
                catch (Exception ex)
                {
                    bExito = false; 
                    Error.LogError(ex.Message); 
                    grid.SetValue(iRow, (int)Cols.ClavesConExistencia, "Error");
                }

                if (bExito)
                {
                    // sResultado = "Exitó";
                    iBusquedasConExito++;
                    grid.ColorRenglon(iRow, colorEjecucionExito); 
                }
                else
                {
                    // sResultado = "Falló"; 
                    iBusquedasConError++;
                    grid.ColorRenglon(iRow, colorEjecucionError); 
                }

                lblFinExito.Text = iBusquedasConExito.ToString();
                lblFinError.Text = iBusquedasConError.ToString(); 


                // grid.SetValue(iRow, 5, sResultado);
            }
            iBusquedasEnEjecucion--; 
            lblConsultando.Text = iBusquedasEnEjecucion.ToString(); 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                lblConsultando.Text = iBusquedasEnEjecucion.ToString(); 

                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnExportarExcel.Enabled = true; 
                btnNuevo.Enabled = true;
            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkTodos.Checked);
            // myGrid.SetValue((int)Cols.Costo, 0);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            // DllTransferenciaSoft.Properties.Resources 
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();
            clsLeer leerPte = new clsLeer();

            leerPte.DataSetClase = dtsResultados; 
            leerToExcel.DataRowsClase = leerPte.DataTableClase.Select("", "IdFarmacia") ; 

            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.NivelDeAbasto, "PLANTILLA_002");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                int iRow = 9;
                // int iRowInicial = 9; 

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();

                    ////" Select top 1 IdEstado, Estado, IdFarmacia, Farmacia, getdate() as FechaReporte, " +
                    ////" Count(*) As TotalClaves, " +
                    ////" ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, " +
                    ////" ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, " +
                    ////" PorcAbasto " + 

                    leerToExcel.Leer(); 

                    ////xpExcel.Agregar("", 2, 2); 
                    ////xpExcel.Agregar("", 3, 2);
                    ////xpExcel.Agregar("REPORTE DE CLAVES DISPENSADAS DE " + "", 4, 2); 

                    xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("IdFarmacia"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Farmacia"), iRow, 3);
                        // xpExcel.Agregar(leerToExcel.Campo("Jurisdiccion"), iRow, 4);

                        xpExcel.Agregar(leerToExcel.Campo("TotalClaves"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("ConExistencia"), iRow, 5);
                        xpExcel.Agregar(leerToExcel.Campo("SinExistencia"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("PorcAbasto"), iRow, 7);
                        iRow++;
                    } 

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    } 
                }
            }
            */
        } 
    }
}
