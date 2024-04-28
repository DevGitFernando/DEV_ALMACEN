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
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_Transferencias : FrmBaseExt
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsConsultas consultas;
        clsAyudas ayuda;
        clsListView lst;
        DataSet dtsFarmacias = new DataSet();

        string sRutaPlantilla = Application.StartupPath + @"\Plantillas\Rpt_Transferencias.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsGenerarExcel excel;


        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;
        string sEntradaSalida = "TS", sWhere = "", sSelect = "";

        public FrmRptOP_Transferencias()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_TransferenciasDeSalida");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            
            //conexionWeb = new wsFarmacia.wsCnnCliente();
            //conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listResultado);

            CargarJurisdicciones(); 
        }

        #region Form 
        private void FrmRptOP_Transferencias_Load(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles();
            lst.Limpiar(); 
            
            rdoSalidas.Checked = true; 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                CargarLista();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if (ObetenerInformacionExcel())
                {
                    ExportarExcel_CloseXML();

                    //if (rdoNoSurtido.Checked)
                    //{
                    //    ExportarExcel_NoSurtido();
                    //}
                    //else
                    //{
                    //    ExportarExcel();
                    //}
                }
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        #region CargarCombos
        private void CargarJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("*", "<< Todas las jurisdicciones>>"); 

            cboJurisdicciones.Add(consultas.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion");
            dtsFarmacias = consultas.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()");


            cboJurisdicciones.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");

            string sFiltro = string.Format(" IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion CargarCombos 

        private bool Validar()
        {
            bool bRegresa = true; 

            ////if (DtGeneral.EsAlmacen && cboJurisdicciones.Data == "*")
            ////{
            ////    bRegresa = false;
            ////    General.msjAviso("No ha seleccionado una jurisdicción, verifique.");
            ////}

            return bRegresa;
        }

        private void CrearWhere()
        {
            sEntradaSalida = "TS"; sWhere = ""; sSelect = "";

            if (rdoEntradas.Checked == true)
            {
                sEntradaSalida = "TE";
                sSelect = "FolioTransferenciaRef As 'Folio origen', ";
            }

            sWhere = string.Format(" T.Status <> 'C' And TipoTransferencia = '{0}' And Convert(Varchar(10), T.FechaRegistro, 120) between '{1}' And '{2}' AND T.IdEstado = '{3}' AND T.IdFarmacia = '{4}' ",
                        sEntradaSalida, dtpFechaInicial.Text, dtpFechaFinal.Text, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (cboJurisdicciones.Data != "*")
            {
                sWhere += string.Format(" And F.IdJurisdiccion = '{0}'", cboJurisdicciones.Data);
                if (cboFarmacias.Data != "*")
                {
                    sWhere += string.Format(" And F.IdFarmacia = '{0}'", cboFarmacias.Data);
                }
            }

            if (rdoNoSurtido.Checked)
            {
                sWhere += string.Format(" And N.EsCapturada = 1 ");
            }
        }

        private void CargarLista()
        {
            string sInnerJoin = "", sGroupBy = "";
            string sSql = ""; 
            CrearWhere();
            lst.Limpiar();

            if (rdoNoSurtido.Checked)
            {
                sInnerJoin = string.Format(
                    "Inner Join TransferenciasEstadisticaClavesDispensadas N (Nolock) \n" + 
                    "\tOn ( N.IdEmpresa = T.IdEmpresa and N.IdEstado = T.IdEstado And N.IdFarmacia = T.IdFarmacia \n" +
                    "\t\tand N.FolioTransferencia = T.FolioTransferencia ) \n");

                sGroupBy = string.Format(
                    "Group By F.IdJurisdiccion, F.Jurisdiccion, Convert(Varchar(10), T.FechaRegistro, 120), T.FolioTransferencia, \n" +
                    "\tF.IdFarmacia, F.Farmacia, NombreCompleto  \n");
            }


            sSql = string.Format(
                "Select \n" + 
                "\t'Jurisdicción' = (F.IdJurisdiccion + ' -- ' + F.Jurisdiccion), \n" + 
                "\tConvert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, {0}" + 
                "\t(F.IdEstado + ' -- ' + F.Estado) as Estado, \n" +
                "\t(F.IdFarmacia + ' -- ' + F.Farmacia) As 'Farmacia', NombreCompleto As Personal \n" +
                "From TransferenciasEnc T (NoLock) \n" +
                "{2}" +
                "Inner Join vw_Farmacias F (NoLock) On ( F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe ) \n" +
                "Inner Join vw_Personal P (NoLock) On ( T.IdEstado = P.IdEstado And T.IdFarmacia = P.IdFarmacia And T.IdPersonal = P.IdPersonal ) \n" +
                "Where {1}  {3} \n" +
                "Order By T.FolioTransferencia \n", sSelect, sWhere, sInnerJoin, sGroupBy);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al generar la lista.");
            }
            else 
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
                else
                {
                    General.msjAviso("No existe información para mostrar.");
                }
            }
        }

        private bool ObetenerInformacionExcel()
        {
            CrearWhere();
            //lst.Limpiar();
            bool bRegresa = false;

            //string sSql = string.Format("Select 'Jurisdiccion' = (F.IdJurisdiccion + ' -- ' + F.Jurisdiccion)," + 
            //    " Convert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, " +
            //    "F.IdFarmacia, F.Farmacia As 'Farmacia', P.ClaveSSA, L.CodigoEAN, DescripcionCorta, L.ClaveLote, Sum(L.CantidadEnviada) As Cantidad " +
            //    "From TransferenciasEnc T (NoLock) " +
            //    "Inner Join TransferenciasDet D (NoLock) " +
            //    "  On (T.IdEmpresa = D.IdEmpresa And T.IdEstado = D.IdEstado And T.IdFarmacia = D.IdFarmacia And T.FolioTransferencia = D.FolioTransferencia) " +
            //    "Inner Join TransferenciasDet_Lotes L (NoLock) " +
            //    "	On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioTransferencia = L.FolioTransferencia " +
            //    "		And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN) " +
            //    "Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN) " +
            //    "Inner Join vw_Farmacias F (NoLock) On (F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe) " +
            //    "Where L.CantidadEnviada > 0 And {0}" +
            //    "Group By Convert(Varchar(10), T.FechaRegistro, 120), T.FolioTransferencia, " +
            //    "   F.IdJurisdiccion, F.Jurisdiccion, " +
            //    "   F.IdFarmacia, F.Farmacia, P.ClaveSSA, L.CodigoEAN, DescripcionCorta, L.ClaveLote " +
            //    "Order By Convert(Varchar(10), T.FechaRegistro, 120)", sWhere);

            //if (rdoNoSurtido.Checked)
            //{
            //    sSql = string.Format(" Select 'Jurisdiccion' = (F.IdJurisdiccion + ' -- ' + F.Jurisdiccion), " + 
            //        " Convert(Varchar(10), T.FechaRegistro, 120) As Fecha, T.FolioTransferencia As Folio, " + 
            //        " F.IdFarmacia, F.Farmacia As 'Farmacia', P.ClaveSSA, P.DescripcionCortaClave, Sum(N.CantidadRequerida) As Cantidad " + 
            //        " From TransferenciasEnc T (NoLock) " + 
            //        " Inner Join TransferenciasEstadisticaClavesDispensadas N (NoLock) " + 
            //            " On (T.IdEmpresa = N.IdEmpresa And T.IdEstado = N.IdEstado And T.IdFarmacia = N.IdFarmacia And T.FolioTransferencia = N.FolioTransferencia) " + 
            //        " Inner Join vw_ClavesSSA_Sales P (NoLock) On (N.IdClaveSSA = P.IdClaveSSA_Sal) " + 
            //        " Inner Join vw_Farmacias F (NoLock) On (F.IdEstado = T.IdEstadoRecibe And F.IdFarmacia = T.IdFarmaciaRecibe) " + 
            //        " Where {0} " +
            //        " Group By Convert(Varchar(10), T.FechaRegistro, 120), T.FolioTransferencia,  " +
            //        " F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, P.ClaveSSA, P.DescripcionCortaClave " +
            //        " Order By Convert(Varchar(10), T.FechaRegistro, 120) ", sWhere);
            //}

            int EsNoSurtido = rdoNoSurtido.Checked ? 1:0;

            string sSql = string.Format("Exec spp_Rpt_OP_Transferencias \n"+
                "\t@sEntradaSalida = '{0}', @FechaIncial = '{1}', @FechaFinal = '{2}', @IdJurisdiccion = '{3}', @IdFarmaciaRecibe = '{4}', @EsNoSurtido = '{5}', @IdEstado = '{6}', @IdFarmacia = '{7}'  \n",
                sEntradaSalida, dtpFechaInicial.Text,dtpFechaFinal.Text, cboJurisdicciones.Data, cboFarmacias.Data, EsNoSurtido, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al generar el reporte.");
            }
            else 
            {
                if (leer.Leer())
                {
                    //lst.CargarDatos(leer.DataSetClase, true, true);
                    leerExportarExcel.DataSetClase = leer.DataSetClase;
                    bRegresa = true;
                }
                else
                {
                    General.msjAviso("No existe información para mostrar.");
                }
            }

            return bRegresa;
        }

        private void ExportarExcel_CloseXML()
        {
            string sNombreHoja = "Hoja";
            string sConcepto = "";

            int iRen = 2, iCol = 2, iColEnc = iCol + leerExportarExcel.Columnas.Length - 1;

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sFechaImpresion = "Fecha de Impresión: " + General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();

            if(excel.PrepararPlantilla())
            {

                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                sConcepto = "PERIODO DEL " + dtpFechaInicial.Text + " AL " + dtpFechaFinal.Text;


                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sEmpresaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFarmaciaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sConcepto);
                iRen++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFechaImpresion, XLAlignmentHorizontalValues.Left);
                iRen++;
                excel.InsertarTabla(sNombreHoja, iRen, 2, leerExportarExcel.DataSetClase);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        //private void ExportarExcel()
        //{
        //    int iRow = 2, icol =2;
        //    //string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + DtGeneral.FarmaciaConectada + ".xls";
        //    string sPeriodo = "";

        //    this.Cursor = Cursors.WaitCursor;
        //    sRutaPlantilla = Application.StartupPath + @"\Plantillas\Rpt_Transferencias.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Transferencias.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = false;

        //        if (xpExcel.PrepararPlantilla())//sNombreFile))
        //        {
        //            xpExcel.EliminarHoja("NOSURTIDO");

        //            xpExcel.GeneraExcel();

        //            //Se pone el encabezado
        //            leerExportarExcel.RegistroActual = 1;
        //            leerExportarExcel.Leer();
        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow++, 2);
        //            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow++, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, iRow++, 2);
        //            xpExcel.Agregar("PERIODO DEL " + dtpFechaInicial.Text + " AL " + dtpFechaFinal.Text, iRow, 2);

        //            iRow = 7;
        //            xpExcel.Agregar(General.FechaSistemaObtener(), iRow, 3);

        //            // Se ponen los detalles
        //            leerExportarExcel.RegistroActual = 1; 

        //            iRow = 10; 
        //            while(leerExportarExcel.Leer())
        //            {
        //                icol = 2;
        //                xpExcel.Agregar(leerExportarExcel.Campo("Jurisdiccion"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Fecha"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Personal"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("IdPasillo"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("IdEstante"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("IdEntrepaño"), iRow, icol++);

        //                iRow++; 
        //            }

        //            // Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarExcel_NoSurtido()
        //{
        //    int iRow = 2, icol = 2;
        //    //string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + DtGeneral.FarmaciaConectada + ".xls";
        //    string sPeriodo = "";

        //    this.Cursor = Cursors.WaitCursor;
        //    sRutaPlantilla = Application.StartupPath + @"\Plantillas\Rpt_Transferencias.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Transferencias.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = false;

        //        if (xpExcel.PrepararPlantilla())//sNombreFile))
        //        {
        //            xpExcel.EliminarHoja("TRANSFERENCIAS");

        //            xpExcel.GeneraExcel();

        //            //Se pone el encabezado
        //            leerExportarExcel.RegistroActual = 1;
        //            leerExportarExcel.Leer();
        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow++, 2);
        //            xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow++, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, iRow++, 2);
        //            xpExcel.Agregar("PERIODO DEL " + dtpFechaInicial.Text + " AL " + dtpFechaFinal.Text, iRow, 2);

        //            iRow = 7;
        //            xpExcel.Agregar(General.FechaSistemaObtener(), iRow, 3);

        //            // Se ponen los detalles
        //            leerExportarExcel.RegistroActual = 1;

        //            iRow = 10;
        //            while (leerExportarExcel.Leer())
        //            {
        //                icol = 2;
        //                xpExcel.Agregar(leerExportarExcel.Campo("Jurisdiccion"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Fecha"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("IdFarmacia"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Personal"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCortaClave"), iRow, icol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, icol++);

        //                iRow++;
        //            }

        //            // Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Todas>>");

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                CargarFarmacias();
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion Funciones y Procedimientos Privados

        private void listResultado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos
    }
}
