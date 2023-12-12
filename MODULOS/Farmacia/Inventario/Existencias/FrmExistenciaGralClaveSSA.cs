using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos; 
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.ExportarExcel; 

namespace Farmacia.Inventario
{
    public partial class FrmExistenciaGralClaveSSA : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        //clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        FrmListaDeSubFarmacias SubFarmacias;
        //clsExportarExcelPlantilla xpExcel; 
        string sSubFarmacias = "";
        string sFormato = "###,###,###,###,##0"; 

        clsDatosCliente DatosCliente; 
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();

        FrmExistenciaPorClaveSSA Sales;

        clsListView lst;
        bool bMostrarCostos = false; // DtGeneral.PermisosEspeciales.TienePermiso("MOSTRAR_EXISTENCIA_COSTEADA");
        int iMostrarCostos = 0;

        public FrmExistenciaGralClaveSSA()
        {
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);

            //Grid = new clsGrid(ref grdExistencia, this);
            //Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            //Grid.SetOrder(1, 4, true); 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            lst = new clsListView(lstClaves);
            //lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = false;
            AnchoColumnas();
        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            chkConUbicaciones.Checked = false; 

            //grdExistencia.Enabled = true; //Se habilita a pie ya que el IniciaControles no lo hace.
            //Grid.Limpiar(false);
            lst.LimpiarItems();

            query.MostrarMsjSiLeerVacio = true;

            btnImprimir.Enabled = false; 
            //btnExportarExcel.Enabled = false; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string sSql = "";
            string sCondicion = ""; 
            bool bHabilitarImpresion = false;
            double itotal = 0;

            if (rdoRptTodos.Checked)
                sCondicion = ">= 0";

            if (rdoRptConExist.Checked)
                sCondicion = "> 0";

            if (rdoRptSinExist.Checked)
                sCondicion = "= 0";


            // LINEA DE PRUEBA  QUITAR //////////////////
            //DtGeneral.FarmaciaConectada = "0011";//////       
            /////////////////////////////////////////////

            //string sSql = string.Format(" Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Existencia " +
            //    " From vw_ExistenciaPorSales (NoLock) " +
            //    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
            //    " and Existencia {3} ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado,
            //    DtGeneral.FarmaciaConectada, sCondicion);

            sSql = string.Format(
                "Select \t" +
                "\tS.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal as DescripcionSal, \t" +
                "\tCast((sum(IsNull(V.Existencia, 0))) as int) as ExistenciaActual, \t" +
                "\tCast((sum(IsNull(V.ExistenciaEnTransito, 0))) as int) as ExistenciaTransito, \t" +
                "\tCast((sum(IsNull(V.ExistenciaSurtidos, 0))) as int) as ExistenciaSurtidos, \t" +
                "\tCast((sum(IsNull(V.ExistenciaAux, 0))) as int) as ExistenciaTotal \t" +
                "From vw_ClavesSSA_Sales S (NoLock) \t" +
                "Left Join vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) \t" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' {3} and Existencia {4} and MesesParaCaducar > 0 \t" +
                "Group by S.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal \t",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSubFarmacias, sCondicion);  

            //Grid.Limpiar(false);
            lst.LimpiarItems();
            if (!leer.Exec(sSql))       
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else
            {
                if (leer.Leer())
                {
                    bHabilitarImpresion = true; 
                    //Grid.LlenarGrid(leer.DataSetClase);
                    lst.CargarDatos(leer.DataSetClase);
                }
                else
                {
                    General.msjUser("No Existe Información Para Mostrar");
                    btnNuevo_Click(this, null);
                }
            }
            //lblTotal.Text = Grid.TotalizarColumna(4).ToString(sFormato);
            itotal = lst.TotalizarColumnaDouble(7);
            lblTotal.Text = itotal.ToString(sFormato);
            AnchoColumnas();
            CargarDescripcion(1);

            btnImprimir.Enabled = bHabilitarImpresion;
            //btnExportarExcel.Enabled = bHabilitarImpresion; 
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sSql = ""; 
            string sCondicion = "";
            int iTipoDeReporte = 1;
            int iTipoDeExistencia = 0;
            int iRow = 8;
            int iCol = 2;
            
            bMostrarCostos = DtGeneral.PermisosEspeciales.TienePermiso("MOSTRAR_EXISTENCIA_COSTEADA");
            iMostrarCostos = 0;

            if (bMostrarCostos && rdoRptDetallado.Checked)
            {
                if (General.msjConfirmar("¿ Desea mostrar la información de costos ? ") == DialogResult.No)
                {
                    bMostrarCostos = false;
                }
            }

            iMostrarCostos = bMostrarCostos ? 1 : 0;


            if (rdoRptTodos.Checked) iTipoDeExistencia = 0; 
                //sCondicion = ">= 0";

            if (rdoRptConExist.Checked) iTipoDeExistencia = 1; 
                //sCondicion = "> 0";

            if (rdoRptSinExist.Checked) iTipoDeExistencia = 2; 
                //sCondicion = "= 0"; 

            //string sSql = string.Format(" Select " +
            //    " V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, " + 
            //    " S.TipoDeClaveDescripcion as TipoDeInsumo, " + 
            //    " S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal as DescripcionClave,  " + 
            //    " (case when S.EsControlado = 1 Then 'SI' else 'NO' end) as EsControlado, " +  
            //    " (case when S.EsAntibiotico = 1 Then 'SI' else 'NO' end) as EsAntibiotico, " +
            //    " (case when S.EsRefrigerado = 1 Then 'SI' else 'NO' end) as EsRefrigerado, " + 
            //    " sum(IsNull(V.Existencia, 0)) as ExistenciaActual, " +
            //    " sum(IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, sum(IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, " +
            //    " getdate() as FechaEmisionReporte " +
            //    " From vw_ClavesSSA_Sales S (NoLock) " +
            //    " Left Join vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) " +
            //    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' {3} and Existencia {4} " +
            //    " Group by V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, Farmacia, " +
            //    "   S.TipoDeClaveDescripcion, " + 
            //    "   (case when S.EsControlado = 1 Then 'SI' else 'NO' end), " +
            //    "   (case when S.EsAntibiotico = 1 Then 'SI' else 'NO' end), " +
            //    "   (case when S.EsRefrigerado = 1 Then 'SI' else 'NO' end), " + 
            //    "   S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal " +
            //    " Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA ",
            //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSubFarmacias, sCondicion);


            if (rdoRptDetallado.Checked)
            {
                iTipoDeReporte = 2; 
                //sSql = string.Format(" Select " +
                //    " V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, " +
                //    " S.TipoDeClaveDescripcion as TipoDeInsumo, " + 
                //    " S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal as DescripcionClave, " +
                //    " (case when V.EsControlado = 1 Then 'SI' else 'NO' end) as EsControlado, " +
                //    " (case when V.EsAntibiotico = 1 Then 'SI' else 'NO' end) as EsAntibiotico, " +
                //    " (case when V.EsRefrigerado = 1 Then 'SI' else 'NO' end) as EsRefrigerado, " + 
                //    " V.IdSubFarmacia, V.SubFarmacia, V.IdProducto, V.CodigoEAN, V.DescripcionProducto,  " +
                //    " V.Presentacion, V.ContenidoPaquete, V.IdLaboratorio, V.Laboratorio, V.ClaveLote, " +
                //    " convert(varchar(7), FechaCaducidad, 120) as Caduca, MesesParaCaducar as MesesCaduca, " +
                //    " (IsNull(V.Existencia, 0)) as ExistenciaActual, (IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, " +
                //    " (IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, CostoPromedio, UltimoCosto, getdate() as FechaEmisionReporte " +
                //    " From vw_ClavesSSA_Sales S (NoLock) " +
                //    " Left Join vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) " +
                //    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' {3} and Existencia {4} " +
                //    " Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA, V.IdProducto, V.CodigoEAN, convert(varchar(7), V.FechaCaducidad, 120) ", 
                //   //  " Group by V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, Farmacia, S.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal ",
                //    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sSubFarmacias, sCondicion);
            }

            sSql = string.Format("Exec spp_RPT_Existencias  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoDeReporte = '{3}', @SubFarmacias = [ {4} ], @TipoDeExistencia = '{5}', @MostrarCostos = '{6}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iTipoDeReporte, sSubFarmacias, iTipoDeExistencia, iMostrarCostos); 


            this.Cursor = Cursors.WaitCursor; 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al exportar el reporte."); 
            }
            else
            {
                if (leer.Leer())
                {
                    leer.RegistroActual = 1;
                    Generar_Excel(rdoRptGeneral.Checked);

                    //if (rdoRptGeneral.Checked)
                    //{
                    //    Generar_Excel_General(); 
                    //}

                    //if (rdoRptDetallado.Checked)
                    //{
                    //    Generar_Excel_Detallado(); 
                    //}
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void Generar_Excel(bool EsGeneral)
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "REPORTE DE EXISTENCIAS";
            string sNombreHoja = "EXISTENCIAS";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            if (bMostrarCostos)
            {
                sNombreDocumento += " COSTEADAS";
                sNombreHoja += "_COSTEADAS";
            }

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();
                excel.Exportar_Formato(); 

                excel.AbrirDocumentoGenerado(true);
            }
        }

        private void Generar_Excel_General()
        {
            ////// int iRenglon = 8; 
            ////bool bRegresa = false; 
            ////int iRow = 8;
            ////int iCol = 2;
            ////string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\EXISTENCIAS_GENERAL.xls";

            ////this.Cursor = Cursors.WaitCursor;
            ////bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "EXISTENCIAS_GENERAL.xls", DatosCliente);

            ////if (!bRegresa)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo."); 
            ////}
            ////else
            ////{
            ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////    xpExcel.AgregarMarcaDeTiempo = true;
            ////    xpExcel.FormInvoca = this; 

            ////    if (xpExcel.PrepararPlantilla())
            ////    {
            ////        this.Cursor = Cursors.WaitCursor;
            ////        xpExcel.GeneraExcel(true);
            ////        xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

            ////        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            ////        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            ////        xpExcel.Agregar(leer.CampoFecha("FechaEmisionReporte").ToString(), 5, 3);

            ////        while (leer.Leer())
            ////        {
            ////            iCol = 2;
            ////            xpExcel.Agregar(leer.Campo("IdClaveSSA"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("ClaveSSA_Aux"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("TipoDeInsumo"), iRow, iCol++);

            ////            xpExcel.Agregar(leer.Campo("EsControlado"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("EsAntibiotico"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("EsRefrigerado"), iRow, iCol++);

            ////            xpExcel.Agregar(leer.Campo("ExistenciaActual"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("ExistenciaEnTransito"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("ExistenciaTotal"), iRow, iCol++);
            ////            iRow++;

            ////            xpExcel.NumeroRenglonesProcesados++; 
            ////        }

            ////        // Finalizar el Proceso 
            ////        xpExcel.CerrarDocumento();

            ////        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////        {
            ////            xpExcel.AbrirDocumentoGenerado();
            ////        }
            ////    }
            ////}

            ////this.Cursor = Cursors.Default;
        }

        //private void Generar_Excel_Detallado()
        //{
        //    // int iRenglon = 8;
        //    bool bRegresa = false; 
        //    string sRutaPlantilla = "";
        //    string sPlantilla = "";

        //    int iRow = 8;
        //    int iCol = 2;
        //    bool bMostrarCostos  = DtGeneral.PermisosEspeciales.TienePermiso("MOSTRAR_EXISTENCIA_COSTEADA"); 

        //    if (bMostrarCostos)
        //    {
        //        if (General.msjConfirmar("¿ Desea mostrar la información de costos ? ") == DialogResult.No)
        //        {
        //            bMostrarCostos = false;
        //        }
        //    }

        //    if (bMostrarCostos)
        //    {
        //        sPlantilla = "EXISTENCIAS_DETALLE_COSTOS.xls"; 
        //    }
        //    else
        //    {
        //        sPlantilla = "EXISTENCIAS_DETALLE.xls"; 
        //    }


        //    sRutaPlantilla = Application.StartupPath + string.Format(@"\\Plantillas\{0}", sPlantilla);
        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, sPlantilla, DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo."); 
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;
        //        xpExcel.FormInvoca = this; 

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            xpExcel.GeneraExcel(true);
        //            ////xpExcel.MostrarAvanceProceso = true;
        //            xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar(leer.CampoFecha("FechaEmisionReporte").ToString(), 5, 3);


        //            while (leer.Leer())
        //            {
        //                iCol = 2;
        //                xpExcel.Agregar(leer.Campo("IdClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ClaveSSA_Aux"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("TipoDeInsumo"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("EsControlado"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("EsAntibiotico"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("EsRefrigerado"), iRow, iCol++);

        //                xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("SubFarmacia"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("IdProducto"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("DescripcionProducto"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Presentacion"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ContenidoPaquete"), iRow, iCol++);

        //                xpExcel.Agregar(leer.Campo("IdLaboratorio"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Laboratorio"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ClaveLote"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Caduca"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("MesesCaduca"), iRow, iCol++);

        //                if (bMostrarCostos)
        //                {
        //                    xpExcel.Agregar(leer.Campo("CostoPromedio"), iRow, iCol++);
        //                    xpExcel.Agregar(leer.Campo("UltimoCosto"), iRow, iCol++);
        //                }
        //                xpExcel.Agregar(leer.Campo("ExistenciaActual"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ExistenciaEnTransito"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ExistenciaTotal"), iRow, iCol++);

        //                iRow++;
        //                xpExcel.NumeroRenglonesProcesados++; 
        //            }

        //            // Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default; 
        //} 

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int iTipoRpt = 0;
            bool bRegresa = false;

            if (lst.Registros == 0)
            {
                General.msjUser("No hay información en pantalla para generar la impresión.");
            }
            else
            {
                DatosCliente.Funcion = "btnImprimir_Click()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = GnFarmacia.RutaReportes; 
                if (chkConUbicaciones.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_ExistenciasSales_Ubicacion";
                    if (rdoRptDetallado.Checked)
                    {
                        myRpt.NombreReporte = "PtoVta_ExistenciasSalesDetallado_Ubicacion";
                    }
                }
                else 
                {
                    myRpt.NombreReporte = "PtoVta_ExistenciasSales";
                    if (rdoRptDetallado.Checked)
                    {
                        myRpt.NombreReporte = "PtoVta_ExistenciasSalesDetallado";
                    }
                }


                if (rdoRptTodos.Checked)
                {
                    iTipoRpt = 0;
                }

                if (rdoRptConExist.Checked)
                {
                    iTipoRpt = 1;
                }

                if (rdoRptSinExist.Checked)
                {
                    iTipoRpt = 2;
                }

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia",        DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdClaveSSA_Sal", "");
                myRpt.Add("@IdProducto", "");
                myRpt.Add("@CodigoEAN", "");
                myRpt.Add("@SubFarmacias", sSubFarmacias);
                myRpt.Add("@TipoRpt", iTipoRpt);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Botones

        #region Grid 
        //private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    int iTipoRpt = 0;
        //    string sClaveSSA = Grid.GetValue(e.Row + 1, 1);

        //    if (rdoRptTodos.Checked)
        //        iTipoRpt = 0;
        //    if (rdoRptConExist.Checked)
        //        iTipoRpt = 1;       
        //    if (rdoRptSinExist.Checked)
        //        iTipoRpt = 2;

        //    if (sClaveSSA != "")
        //    {                
        //        Sales = new FrmExistenciaPorClaveSSA();
        //        Sales.MostrarDetalle(sClaveSSA, iTipoRpt, sSubFarmacias);
        //    }
        //}

        //private void grdExistencia_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        //{
        //    CargarDescripcion(e.NewRow + 1);
        //}

        #endregion Grid        

        #region Funciones 
        private void FrmExistenciaGralClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void CargarDescripcion(int Renglon)
        {
            //lblDescripcionSal.Text = Grid.GetValue(Renglon, 3);
            lblDescripcionSal.Text = lst.GetValue(Renglon, 3);
        }

        private void CargarSubFarmacias()
        {
            SubFarmacias = new FrmListaDeSubFarmacias(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            SubFarmacias.MostrarDetalle();
            sSubFarmacias = SubFarmacias.CondicionSubFarmacias;
        }

        private void AnchoColumnas()
        {
            lst.AnchoColumna(1, 80);
            lst.AnchoColumna(2, 110);
            lst.AnchoColumna(3, 310);
            lst.AnchoColumna(4, 105);
            lst.AnchoColumna(5, 125);
            lst.AnchoColumna(6, 105);
        }
        #endregion Funciones

        private void lstClaves_DoubleClick(object sender, EventArgs e)
        {
            int iTipoRpt = 0;
            string sClaveSSA = lst.LeerItem().Campo("ClaveSSA");

            if (rdoRptTodos.Checked)
                iTipoRpt = 0;
            if (rdoRptConExist.Checked)
                iTipoRpt = 1;
            if (rdoRptSinExist.Checked)
                iTipoRpt = 2;

            if (sClaveSSA != "")
            {
                Sales = new FrmExistenciaPorClaveSSA();
                Sales.MostrarDetalle(this, sClaveSSA, iTipoRpt, sSubFarmacias);
            }
        }

        private void lstClaves_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            CargarDescripcion(lst.RenglonActivo);
        }
        
    }
}
