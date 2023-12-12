using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmPCM_Claves : FrmBaseExt
    {
        #region Enums
        enum Cols
        {
            Ninguno = 0, IdJurisdiccion = 1, Jurisdiccion = 2, Status = 3, Procesar = 4 
        }

        enum ColsExportar
        {
            Ninguno = 0, IdFarmacia = 2, Farmacia = 3, IdJurisdiccion = 4, Jurisdiccion = 5, ClaveSSA = 6, DescripcionSal = 7, 
            Consumo = 8, ConsumoMensual = 9, StockSugerido = 10, StockAsignado = 11
        }
        #endregion Enums

        #region Variables 
        //clsExportarExcelPlantilla xpExcel;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsConexionSQL cnnPCM = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerPCM;
        clsLeer leerConsumos;
        clsConsultas query;
        clsDatosCliente datosCliente;

        clsGrid myGrid;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;

        DataSet dtsJurisdicciones = new DataSet();
        DataSet dtsConsumos = new DataSet();
        Thread thFaltantes;        

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;
        string sIdJurisdiccion = DtGeneral.Jurisdiccion;
        int iFarmacias_Proceso = 0;
        int iFarmacias_Procesadas = 0;

        string sClaveSSA_Procesar = "";
        string sUrlServidorRegional = "";
        int iConsultando = 0;
        bool bEncontroInformacion = false;
        bool bOcurrioError = false;
        bool bExisteInformacionJurisdiccion = false;
        #endregion Variables
        
        public FrmPCM_Claves()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            Error = new clsGrabarError(General.DatosConexion, GnInventarios.DatosApp, this.Name);
            leer = new clsLeer(ref cnn);
            leerPCM = new clsLeer(ref cnnPCM);
            leerConsumos = new clsLeer();

            sUrlServidorRegional = DtGeneral.UrlServidorCentral_Regional;
            datosCliente = new clsDatosCliente(GnInventarios.DatosApp, this.Name, "Obtener_PCM");

            query = new clsConsultas(General.DatosConexion, GnInventarios.DatosApp, this.Name, false);

            myGrid = new clsGrid(ref grdJurisdicciones, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdJurisdicciones.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.WhiteSmoke;

            this.Width = 610;
            this.Height = 430;

            FrameMesesRevision.Top = 28; 
            FrameProceso.Top = 28;
            //FrameProceso.Width = 455;
            //FrameProceso.Height = 102;
            MostrarEnProceso(false);

            ObtenerJurisdicciones();
        }

        private void FrmPCM_Claves_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IniciarToolBar(true, true, true );
            bEncontroInformacion = false;
            bOcurrioError = false;
            bExisteInformacionJurisdiccion = false;


            myGrid.BloqueaColumna(false, (int)Cols.Procesar); 
            grdJurisdicciones.Enabled = true;
            chkProcesar.Enabled = true;

            MostrarEnProceso(false);
            InicializaStatus();
            InicializaProcesar();
            nmMesesRevision.Enabled = true;
            nmMesesRevision.Value = 12;
            nmMesesRevision.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e) 
        {
            if (ValidaProcesar())
            {
                thFaltantes = new Thread(this.ThObtenerFaltantesJuris);
                thFaltantes.Name = "ObtenerFaltantes_Juris";
                thFaltantes.Start();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ObtenerDatosExcel();
            
        }

        #endregion Botones

        #region Funciones 
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 276;
                FrameMesesRevision.Width = 260; 
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
                FrameMesesRevision.Width = 584; 
            }
        }

        private void ObtenerJurisdicciones()
        {
            string sSql = string.Format("Select IdJurisdiccion as 'Id Jurisdicción', Descripcion as Jurisdicción, Cast( 'Pendiente' as varchar(20) ) as Status, 0 as Procesar " + 
                "From CatJurisdicciones (NoLock) " + 
                "Where IdEstado = '{0}' " + 
                "Order By IdJurisdiccion ", sEstado);

            myGrid.Limpiar(false);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarListaDePedidos()");
                General.msjError("Ocurrió un error al obtener la lista de las Jurisdicciones.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontraron Jurisdicciones para este estado. Favor de reportarlo a Sistemas.");
                }
                else
                {
                    dtsJurisdicciones = leer.DataSetClase;
                    myGrid.LlenarGrid(leer.DataSetClase, false, false);
                }
            }
        }

        private bool Obtener_PCM(string IdJurisdiccion, int iRenglon)
        {
            bool bRegresa = false;
            string sSql = "";
            int iNumeroMesesExistencia = 1;
            string sTabla = string.Format("tmpPCM_{0}", General.MacAddress);
            DateTime dtpFecha = General.FechaSistema;

            DataSet dtsPCM = new DataSet();
            clsLeerWebExt leerWeb = new clsLeerWebExt(sUrlServidorRegional, DtGeneral.CfgIniOficinaCentral, datosCliente);


            sSql = string.Format(
                "If Not Exists ( Select Name From sysobjects (noLock) Where Name = '{0}' and xType = 'U' ) \n " +
                "Begin \n " +
                "    Select top 0 IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, \n " +
                "        cast(0 as int) as Consumo, cast(0 as int) as ConsumoMensual, cast(0 as int) as StockSugerido \n " +
                "    Into {0}  \n " +
                "    From ADMI_Consumos_Claves 	\n " +
                "End \n \n ", sTabla);

            sSql += string.Format("Exec spp_INV_PCM_Consumos_Claves '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '1', '{7}' \n \n ",
                            sIdEmpresa, sIdEstado, sIdFarmacia, IdJurisdiccion,
                            General.FechaYMD(dtpFecha.AddMonths(-1)), (int)nmMesesRevision.Value, iNumeroMesesExistencia, sTabla);

            sSql += string.Format(
                " Select IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido " +
                " From {0} (NoLock) ", sTabla);

            iConsultando = 0;
            if (!leerWeb.Exec(sSql))
            {
                bOcurrioError = true;
                Error.GrabarError(General.DatosConexion, leerWeb.Error, "Obtener_PCM");
                myGrid.SetValue(iRenglon, (int)Cols.Status, "ERROR");
            }
            else
            {
                if (leerWeb.Leer())
                {
                    bRegresa = true;
                    dtsPCM = leerWeb.DataSetClase;

                    sSql = string.Format("If Exists ( Select Name From sysobjects (noLock) Where Name = '{0}' and xType = 'U') Drop Table {0} ", sTabla);
                    leerWeb.Exec(sSql);
                }
            }

            if (bRegresa)
            {
                if (Registrar_PCM(dtsPCM, IdJurisdiccion))
                {
                    if (VerificarPCM(IdJurisdiccion, iRenglon))
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            return bRegresa;
        }

        private bool Registrar_PCM(DataSet PCM, string IdJurisdiccion)
        {
            bool bRegresa = false;
            string sSql = "";
            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            clsLeer l = new clsLeer();
            clsLeer leerPCM_Aux = new clsLeer(ref cnnPCM);
            

            sSql = string.Format("Delete From INV_Consumos_Mensuales " +
                " Where IdEstado = '{0}' and IdJurisdiccion = '{1}' And Año = {2} And Mes = {3} ", sIdEstado, IdJurisdiccion, iAño, iMes);
            if (leerPCM_Aux.Exec(sSql))
            {
                l.DataSetClase = PCM;
                while (l.Leer())
                {
                    bRegresa = true;
                    sSql = string.Format("Insert Into INV_Consumos_Mensuales ( Año, Mes, IdEmpresa, IdEstado, IdFarmacia, IdJurisdiccion, ClaveSSA, Consumo, ConsumoMensual, StockSugerido, StockAsignado )  \n");
                    sSql += string.Format("Select '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', 0 ",
                        iAño, iMes, l.Campo("IdEmpresa"), l.Campo("IdEstado"), l.Campo("IdFarmacia"), l.Campo("IdJurisdiccion"),
                        l.Campo("ClaveSSA"), l.CampoInt("Consumo"), l.CampoInt("ConsumoMensual"), l.CampoInt("StockSugerido"));
                    if (!leerPCM.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            return bRegresa;
        }

        private bool VerificarPCM(string IdJurisdiccion, int iRenglon )
        {
            bool bRegresa = false;
            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;

            leerPCM = new clsLeer(ref cnnPCM);
            string sSql = string.Format("Select Top 1 * From INV_Consumos_Mensuales(NoLock) " +
                " Where IdEstado = '{0}' and IdJurisdiccion = '{1}' And Año = {2} And Mes = {3} ", sIdEstado, IdJurisdiccion, iAño, iMes);
 
            if (!leerPCM.Exec(sSql))
            {
                Error.GrabarError(leerPCM, "Mostrar_PCM");
            }
            else
            {
                if (leerPCM.Leer())
                {
                    bRegresa = true;
                    bEncontroInformacion = true;
                    bExisteInformacionJurisdiccion = true;
                }
                else
                {
                    myGrid.SetValue(iRenglon, (int)Cols.Status, "Sin Datos");
                }
            }

            //  Tiempo de espera 
            Thread.Sleep(1000);

            return bRegresa;
        }

        private void InicializaStatus()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                myGrid.SetValue(i, (int)Cols.Status, "Pendiente");
            }
        }

        private void InicializaProcesar()
        {
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                myGrid.SetValue(i, (int)Cols.Procesar, 0);
            }
        }

        private void chkProcesar_CheckedChanged(object sender, EventArgs e)
        {
            bool bMarcar = chkProcesar.Checked;

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                myGrid.SetValue(i, (int)Cols.Procesar, bMarcar);
            }
        }

        private bool ValidaProcesar()
        {
            bool bProcesar = false;
            for (int i = 1; i <= myGrid.Rows; i++)
            {
                if (myGrid.GetValueBool(i, (int)Cols.Procesar))
                {
                    bProcesar = true;
                }                 
            }

            if (!bProcesar)
            {
                General.msjUser("Debe seleccionar al menos una Jurisdicción. Verifique");
            }
            return bProcesar;
        }

        private void GenerarReporteExcel()
        {
            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("Consumo Mensual de Claves SSA de {0} del {1} ", General.FechaNombreMes(dtpFecha.AddMonths(-1)), iAño.ToString());
            string sNombreFile = "Consumo Mensual de Claves SSA";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ALMN_Rpt_PCM_Consumos_Claves.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ALMN_Rpt_PCM_Consumos_Claves.xls", datosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else 
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ExportarConsumos();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarConsumos()
        //{
        //    DateTime dtpFecha = General.FechaSistema;
        //    int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = string.Format("Consumo Mensual de Claves SSA de {0} del {1}", General.FechaNombreMes(dtpFecha.AddMonths(-1)), iAño.ToString());
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    leer.DataSetClase = dtsConsumos;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, (int)ColsExportar.IdFarmacia);
        //        xpExcel.Agregar(leer.Campo("Farmacia"), iRenglon, (int)ColsExportar.Farmacia);
        //        xpExcel.Agregar(leer.Campo("IdJurisdiccion"), iRenglon, (int)ColsExportar.IdJurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRenglon, (int)ColsExportar.Jurisdiccion);
        //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)ColsExportar.ClaveSSA);
        //        xpExcel.Agregar(leer.Campo("DescripcionSal"), iRenglon, (int)ColsExportar.DescripcionSal);
        //        xpExcel.Agregar(leer.Campo("Consumo"), iRenglon, (int)ColsExportar.Consumo);
        //        xpExcel.Agregar(leer.Campo("ConsumoMensual"), iRenglon, (int)ColsExportar.ConsumoMensual);
        //        xpExcel.Agregar(leer.Campo("StockSugerido"), iRenglon, (int)ColsExportar.StockSugerido);
        //        xpExcel.Agregar(leer.Campo("StockAsignado"), iRenglon, (int)ColsExportar.StockAsignado);

        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}

        private void ObtenerDatosExcel()
        {
            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            string sSql = string.Format(
                " Select C.IdFarmacia, F.Farmacia, C.IdJurisdiccion, F.Jurisdiccion, " +
                " C.ClaveSSA As 'Clave SSA' , S.DescripcionSal As Descripcion, C.Consumo, C.ConsumoMensual As 'Consumo Mensual', C.StockSugerido As 'Stock Sugerido', C.StockAsignado As 'Stock Asignado' " +
                " From Inv_Consumos_Mensuales C(NoLock) " + 
                " Inner Join vw_Farmacias F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia ) " +
                " Inner Join vw_ClavesSSA_Sales S(NoLock) On( C.ClaveSSA = S.ClaveSSA ) " + 
                " Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' And Año = {2} And Mes = {3} " + 
                " Order By Año, Mes, IdJurisdiccion, IdFarmacia, DescripcionSal ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, iAño, iMes);

            dtsConsumos = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerDatosExcel()");
                General.msjError("Ocurrió un error al obtener los datos para la exportación a Excel.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información para generar el archivo de Excel. Reportarlo a Sistemas.");
                }
                else
                {
                    dtsConsumos = leer.DataSetClase;
                    GenerarReporteExcel();
                }
            }
        }
        #endregion Funciones  

        #region Hilos 
        private void ThObtenerFaltantesJuris()
        {
            bool bProcesar = false;

            InicializaStatus();
            IniciarToolBar(false, false, false);

            bEncontroInformacion = false;
            bOcurrioError = false;
            bExisteInformacionJurisdiccion = false;

            nmMesesRevision.Enabled = false;
            myGrid.BloqueaColumna(true, (int)Cols.Procesar);
            chkProcesar.Enabled = false;
            MostrarEnProceso(true);

            for (int i = 1; i <= myGrid.Rows; i++)
            {
                bProcesar = myGrid.GetValueBool(i, (int)Cols.Procesar);
                if (bProcesar)
                {
                    myGrid.SetValue(i, (int)Cols.Status, "PROCESANDO");
                    sIdJurisdiccion = myGrid.GetValue(i, (int)Cols.IdJurisdiccion);

                    bOcurrioError = false;
                    bExisteInformacionJurisdiccion = false;

                    Obtener_PCM(sIdJurisdiccion, i);

                    if (!bOcurrioError)
                    {
                        if (bExisteInformacionJurisdiccion)
                        {
                            myGrid.SetValue(i, (int)Cols.Status, "Terminado");
                        }
                    }
                }
            }

            IniciarToolBar(true, false, true);
            if (!bEncontroInformacion)
            {
                nmMesesRevision.Enabled = true;
                myGrid.BloqueaColumna(false, (int)Cols.Procesar);
                chkProcesar.Enabled = true;
                IniciarToolBar(true, true, false);                
            }

            MostrarEnProceso(false);
        }
        #endregion Hilos 

        private void FrmPCM_Claves_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if( thFaltantes.IsAlive)
                {
                    thFaltantes.Abort();
                }
                
            }
            catch
            { 
            }
            finally
            {
                thFaltantes = null;
            }

        }

        
    }
}
