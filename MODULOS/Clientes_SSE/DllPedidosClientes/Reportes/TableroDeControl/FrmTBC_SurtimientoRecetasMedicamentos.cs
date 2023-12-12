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

// using DllFarmaciaSoft; 

namespace DllPedidosClientes.Reportes
{
    public partial class FrmTBC_SurtimientoRecetasMedicamentos : FrmBaseExt 
    {
        private enum Cols
        {
            IdFarmacia = 1, Farmacia = 2, Url = 3, Jurisdiccion = 4, 
            Procesar = 5, 
            Recetas = 6, RecetasCompletas = 7, 
            Vales = 8,ValesPorc = 9,
            NoSurtido = 10, NoSurtidoPorce = 11
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid grid;
        clsConsultas query;
        // clsLeerWebExt leerWeb;
        clsDatosCliente datosCliente;

        //clsExportarExcelPlantilla xpExcel;  
        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iBusquedasConExito = 0;
        int iBusquedasConError = 0;


        DataSet dtsUnidades;
        DataSet dtsResultados;
        bool bTablaSurtimientoPreparada = false; 

        public FrmTBC_SurtimientoRecetasMedicamentos()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ObtenerInformacionUnidad");

            // leerWeb = new clsLeerWebExt(General.Url, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name); 

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.ModoRow);

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
        }

        private void FrmTBC_SurtimientoRecetasMedicamentos_Load(object sender, EventArgs e)
        {
            LimpiarPantalla();
        }

        #region Botones  
        private void LimpiarPantalla()
        {
            bTablaSurtimientoPreparada = false; 
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

        private DataSet GetInformacionRegional__ODL(string Cadena)
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

            clsLeer leerRegional = new clsLeer(ref cnnLocal);

            leerRegional.Exec(Cadena);
            dts = leerRegional.DataSetClase;

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
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.ColorRenglon(colorEjecucionExito);
            grid.SetValue((int)Cols.Recetas, "");
            grid.SetValue((int)Cols.RecetasCompletas, "");
            grid.SetValue((int)Cols.Vales, "");
            grid.SetValue((int)Cols.ValesPorc, "");
            grid.SetValue((int)Cols.NoSurtido, "");
            grid.SetValue((int)Cols.NoSurtidoPorce, "");
            bTablaSurtimientoPreparada = false;

            dtsResultados = null; 
            PrepararTablaSurtimiento(); 

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
            clsLeer leerLocal; 
            int iRow = (int)Renglon;
            // int iValor = -1; 
            string sIdFarmacia = grid.GetValue(iRow, (int)Cols.IdFarmacia);
            string sFarmacia = grid.GetValue(iRow, (int)Cols.Farmacia); 
            string sUrl = grid.GetValue(iRow, (int)Cols.Url);
            string sValor = "-- " + DtGeneralPedidos.EstadoConectado + "-" + sIdFarmacia;

            ////string sFechaSistemaSvr = grid.GetValue(iRow, (int)Cols.FechaSistemaSvr);
            ////string sFechaServidorSvr = grid.GetValue(iRow, (int)Cols.FechaServidorSvr); 


            // int iRegSvr = 0, iReg = 0; 
            bool bExito = false;
            // string sResultado = "Conectando";

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_SurtimientoRecetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                DtGeneralPedidos.EstadoConectado, sIdFarmacia, "0002", "0005",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));

            //sSql += "\n " + string.Format("Select top 1 * From tmpRpt_SurtimientoRecetas (NoLock) ");

            // Jesús Díaz 2K120312.2118 
            /* 
            sSql += "\n " +
                string.Format("Select '' as IdEmpresa, Empresa, '' as IdEstado, '' as Estado, '' as IdFarmacia, Farmacia, Periodo, FechaReporte, " + 
                    " FoliosDeVenta, Vales, NoSurtido, PorcSurtido, PorcVales, PorcNoSurtido, " +
                    " ClavesDiferentes, CantidadTotal, " +
                    " ClavesSurtidas, CantidadSurtida, PorcClavesSurtidas, " +
                    " ClavesPerfil, PorcClavesPerfil, " +
                    " ClavesVales, CantidadVale, PorcClavesVales, " +
                    " ClavesNoSurtido, CantidadNoSurtida, PorcClavesNoSurtida " +
                    " From Rpt_NivelDeAbasto (NoLock) "); 
            */ 

            //grid.ColorRenglon(iRow, colorEjecutando);
            //grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;
            lblConsultando.Text = iBusquedasEnEjecucion.ToString(); 

            if (grid.GetValueBool(iRow, (int)Cols.Procesar))
            {
                grid.ColorRenglon(iRow, colorEjecutando); 
                //leerLocal = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente); 
                try
                {
                    leerLocal = new clsLeer();
                    leerLocal.Reset();
                    leerLocal.DataSetClase = GetInformacion(sSql);

                    if (leerLocal.SeEncontraronErrores())
                    {
                        bExito = false;
                    }
                    else 
                    {
                        bExito = true;

                        if (!leerLocal.Leer())
                        {
                            bExito = true;
                        }                        
                        
                        grid.SetValue(iRow, (int)Cols.Recetas, leerLocal.Campo("FoliosDeVenta"));
                        grid.SetValue(iRow, (int)Cols.RecetasCompletas, leerLocal.Campo("PorcSurtido")); 

                        grid.SetValue(iRow, (int)Cols.Vales, leerLocal.Campo("Vales"));
                        grid.SetValue(iRow, (int)Cols.ValesPorc, leerLocal.Campo("PorcVales"));

                        grid.SetValue(iRow, (int)Cols.NoSurtido, leerLocal.Campo("NoSurtido"));
                        grid.SetValue(iRow, (int)Cols.NoSurtidoPorce, leerLocal.Campo("PorcNoSurtido"));

                        // Generación de reporte para Excel
                        if (dtsResultados == null)
                        {
                            dtsResultados = leerLocal.DataSetClase.Clone(); 
                        }

                        dtsResultados.Tables[0].Merge(leerLocal.DataSetClase.Tables[0]);
                        // InsertarSurtimiento(sIdFarmacia, sFarmacia, leerLocal.DataSetClase); 
                    }
                }
                catch { }

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

        private void PrepararTablaSurtimiento()
        {
            if (!bTablaSurtimientoPreparada)
            {
                string sSql = "Exec spp_Rpt_Surtimiento_General "; 
                bTablaSurtimientoPreparada = leer.Exec(sSql);
            }
        }

        private void InsertarSurtimiento(string IdFarmacia, string Farmacia, DataSet Datos)
        {
            string sSql = ""; 
            clsLeer l = new clsLeer();
            l.DataSetClase = Datos;
            l.Leer();

            try
            {
                sSql = string.Format(
                    "Insert Into Rpt_Surtimiento_General ( " +
                        " IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " + // " FechaReporte, " + //7 
                        " FechaInicial, FechaFinal, " +
                        " Recetas, Vales, [No Surtido], PorcSurtido, PorcVales, PorcNoSurtido, " + //8 
                        " ClavesDiferentes, CantidadTotal, ClavesSurtidas, CantidadSurtida, PorcClavesSurtidas, " + // 5 
                        " ClavesPerfil, PorcClavesPerfil, ClavesVales, CantidadVale, PorcClavesVales, " + // 5 
                        " ClavesNoSurtido, CantidadNoSurtida, PorcClavesNoSurtida ) " + //3 
                    " values ( " +
                    " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}',  " +
                    " '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}',  " +
                    " '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}'" + // , '{27}' " +  
                    " )  ",
                    l.Campo("IdEmpresa"), l.Campo("Empresa"),
                    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.EstadoConectadoNombre,
                    IdFarmacia, Farmacia, // l.Campo("FechaReporte"), 
                    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                    l.CampoInt("FoliosDeVenta"), l.CampoInt("Vales"), l.CampoInt("NoSurtido"),
                    l.CampoDouble("PorcSurtido"), l.CampoDouble("PorcVales"), l.CampoDouble("PorcNoSurtido"),
                    l.CampoInt("ClavesDiferentes"), l.CampoInt("CantidadTotal"),
                    l.CampoInt("ClavesSurtidas"), l.CampoInt("CantidadSurtida"), l.CampoDouble("PorcClavesSurtidas"),
                    l.CampoInt("ClavesPerfil"), l.CampoDouble("PorcClavesPerfil"),
                    l.CampoInt("ClavesVales"), l.CampoInt("CantidadVale"), l.CampoDouble("PorcClavesVales"),
                    l.CampoInt("ClavesNoSurtido"), l.CampoInt("CantidadNoSurtida"), l.CampoDouble("PorcClavesNoSurtida")
                    );


                ////sSql = string.Format(
                ////    "Insert Into Rpt_Surtimiento_General ( " +
                ////        " IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " + // " FechaReporte, " + //7 
                ////        " FechaInicial, FechaFinal, Tipo, Valor ) " + //3 
                ////    " values ( " +
                ////    " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ) \n\n ",
                ////    l.Campo("IdEmpresa"), l.Campo("Empresa"),
                ////    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.EstadoConectadoNombre,
                ////    IdFarmacia, Farmacia, // l.Campo("FechaReporte"), 
                ////    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                ////    "Recetas", l.CampoInt("FoliosDeVenta"));


                ////sSql += string.Format(
                ////    "Insert Into Rpt_Surtimiento_General ( " +
                ////        " IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " + // " FechaReporte, " + //7 
                ////        " FechaInicial, FechaFinal, Tipo, Valor ) " + //3 
                ////    " values ( " +
                ////    " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' )\n\n  ",
                ////    l.Campo("IdEmpresa"), l.Campo("Empresa"),
                ////    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.EstadoConectadoNombre,
                ////    IdFarmacia, Farmacia, // l.Campo("FechaReporte"), 
                ////    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                ////    "Vales", l.CampoInt("Vales"));

                ////sSql += string.Format(
                ////    "Insert Into Rpt_Surtimiento_General ( " +
                ////        " IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, " + // " FechaReporte, " + //7 
                ////        " FechaInicial, FechaFinal, Tipo, Valor ) " + //3 
                ////    " values ( " +
                ////    " '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'  )\n\n  ",
                ////    l.Campo("IdEmpresa"), l.Campo("Empresa"),
                ////    DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.EstadoConectadoNombre,
                ////    IdFarmacia, Farmacia, // l.Campo("FechaReporte"), 
                ////    General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                ////    "No surtido", l.CampoInt("NoSurtido"));

                // l.CampoInt("FoliosDeVenta"), l.CampoInt("Vales"), l.CampoInt("NoSurtido")

                leer.Exec(sSql);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                lblConsultando.Text = iBusquedasEnEjecucion.ToString(); 

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
                        xpExcel.Agregar(leerToExcel.Campo("Jurisdiccion"), iRow, 4);

                        xpExcel.Agregar(leerToExcel.Campo("TotalClaves"), iRow, 5);
                        xpExcel.Agregar(leerToExcel.Campo("ConExistencia"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("SinExistencia"), iRow, 7);
                        xpExcel.Agregar(leerToExcel.Campo("PorcAbasto"), iRow, 8);
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
