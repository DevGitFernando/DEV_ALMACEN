using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Reporteador;

namespace Farmacia.PedidosDeDistribuidor
{
    public partial class FrmListadoTiposRemisionesDist : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerCuadrosDeAtencion;

        clsDatosCliente DatosCliente;
        clsListView lst;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerExcelConcentrado;
        clsLeer leerExcelDetallado;

        DataSet dtsConcentrado;
        DataSet dtsDetallado;

        Thread _workerThread;

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        bool bMostrarExcel = false;
        bool bMostrarImprimir = false;

        bool bFolderDestino = false;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;

        string sRutaDestino_Archivos = "";
        string sRutaDestino = "";

        public FrmListadoTiposRemisionesDist()
        {
            CheckForIllegalCrossThreadCalls = false; 

            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false; 
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");                        
            leer = new clsLeer(ref cnn);
            leerCuadrosDeAtencion = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerExcelConcentrado = new clsLeer(ref cnn);
            leerExcelDetallado = new clsLeer(ref cnn);

            dtsConcentrado = new DataSet();
            dtsDetallado = new DataSet();

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            lst = new clsListView(lstFoliosRemisiones);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            MostrarEnProceso(false);
        }

        private void FrmListadoFoliosRemisiones_Load(object sender, EventArgs e)
        {
            CargarListaReportes();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                bMostrarExcel = false;
                bMostrarImprimir = false;
                CargarFoliosRemisiones();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (bMostrarExcel)
            {
                ReporteFoliosRemisiones();
            }
            if (bMostrarImprimir)
            {
                Datos_RptRemisiones_Vta_Admon();
                RptRemisionesConcentrado();
                RptRemisionesDetallado();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                CrearDirectorioDestino();
                IniciaGeneracionReportes();
            }
        }
        #endregion Botones

        #region Carga_Combos
        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 

            cboReporte.Add("1", "Remisiones de Excepción");
            cboReporte.Add("2", "Remisiones de No Excepción");
            cboReporte.Add("3", "Remisiones de Venta");
            cboReporte.Add("4", "Remisiones de Administracion");            

            cboReporte.SelectedIndex = 0;
        }
        #endregion Carga_Combos

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;
            btnImprimir.Enabled = false;
            bMostrarImprimir = false;
            bMostrarExcel = false;
            cboReporte.SelectedIndex = 0;
            lst.Limpiar();
            txtIdDistribuidor.Focus();
        }

        private string QueryEjecucion()
        {
            string sSql = "", sCampoFecha = "";
            int iEsExcepcion = 0; 
            int iEsConsignacion = 0;
            string sFiltroEsConsignacion = " and EsConsignacion in ( 0, 1 ) ";

            sCampoFecha = " FechaRegistro ";
            bMostrarExcel = false;
            bMostrarImprimir = false;

            switch (Convert.ToInt32(cboReporte.Data))
            {
                case 1: 
                    iEsExcepcion = 1;
                    bMostrarExcel = true;
                    break;

                case 2:
                    iEsExcepcion = 0;
                    bMostrarExcel = true;
                    break;

                case 3:
                    iEsExcepcion = 0; 
                    iEsConsignacion = 0;
                    bMostrarImprimir = true; 
                    sFiltroEsConsignacion = string.Format(" and EsConsignacion = {0} ", iEsConsignacion);
                    sCampoFecha = " FechaDocumento ";
                    break;
                
                case 4:
                    iEsExcepcion = 0; 
                    iEsConsignacion = 1;
                    bMostrarImprimir = true; 
                    sFiltroEsConsignacion = string.Format(" and EsConsignacion = {0} ", iEsConsignacion);
                    sCampoFecha = " FechaDocumento ";
                    break;             
            }

            sSql = string.Format(" Select Folio, 'Codigo Cliente' = CodigoCliente, Cliente, 'Referencia Documento' = ReferenciaPedido, " +
                   " 'Fecha Documento' = Convert(varchar(10), FechaDocumento, 120),  " +
                   " 'Tipo Remision' = Case When EsConsignacion = 1 Then 'CONSIGNACION' Else 'VENTA' End, Observaciones, 'Total Piezas' = Sum(CantidadRecibida) " +
                   " From vw_Impresion_RemisionesDistribuidor (Nolock) " +
                   " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  " +
                   " and IdDistribuidor = '{3}' and EsExcepcion = {4}  {5}  " + 
                   " and Convert(varchar(10), {8}, 120) Between '{6}' and '{7}' " +
                   " and Status = 'T' " +
                   " Group By Folio, CodigoCliente, Cliente, ReferenciaPedido, FechaDocumento, EsConsignacion, Observaciones " +
                   " Order By Folio ", sEmpresa, sEstado, sFarmacia, txtIdDistribuidor.Text, iEsExcepcion, 
                   sFiltroEsConsignacion, General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"), sCampoFecha); 

            return sSql;
        }

        private void CargarFoliosRemisiones()
        {
            string sSql = "";

            HabilitarBotones(true, false, false, false);
            

            sSql = QueryEjecucion();
           
            lst.Limpiar(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFoliosRemisiones()");
                General.msjError("Ocurrió un error al obtener la lista de remisiones."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No se encontro información con los criterios especificados."); 
                }
                else 
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    leerExportarExcel.DataSetClase = leer.DataSetClase;                                       
                }
            }

            HabilitarBotones(true, true, bMostrarExcel, bMostrarImprimir);

        }

        private void CargaDatosDistribuidor()
        {
            //Se hace de esta manera para la ayuda. 

            if (leer.Campo("Status").ToUpper() == "A")
            {
                txtIdDistribuidor.Text = leer.Campo("IdDistribuidor");
                lblDistribuidor.Text = leer.Campo("NombreDistribuidor");
            }
            else
            {
                General.msjUser("El Distribuidor " + leer.Campo("NombreDistribuidor") + " actualmente se encuentra cancelado, verifique. ");
                txtIdDistribuidor.Text = "";
                lblDistribuidor.Text = "";
                txtIdDistribuidor.Focus();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtIdDistribuidor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("Distribuidor Incorrecto, Verifique..");
                txtIdDistribuidor.Focus();
            }

            if (bRegresa && cboReporte.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("Seleccione un Reporte por Favor..");
                cboReporte.Focus();
            }
            
            return bRegresa;
        }

        private void HabilitarBotones(bool Nuevo, bool Ejecutar, bool ExportarExcel, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = ExportarExcel;
            btnImprimir.Enabled = Imprimir;
        }

        private void HabilitarControles(bool Valor)
        {
            txtIdDistribuidor.Enabled = Valor;           
            dtpFechaInicio.Enabled = Valor;
            dtpFechaFin.Enabled = Valor;
            txtPrecioAdmon.Enabled = Valor;
            cboReporte.Enabled = Valor;
        }

        private void Datos_RptRemisiones_Vta_Admon()
        {
            string sSql = "";
            int iEsConsignacion = 0;
            dtsConcentrado = new DataSet();
            dtsDetallado = new DataSet();
            if (cboReporte.SelectedIndex == 4)
            {
                iEsConsignacion = 1;
            }

            sSql = string.Format(" Exec spp_Rpt_RemisionesDist_Vta_Admon_Mensuales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}  ", 
                                            sEmpresa, sEstado, sFarmacia, txtIdDistribuidor.Text, iEsConsignacion,
                                            General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"), txtPrecioAdmon.NumericText);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Datos_RptRemisiones_Vta_Admon()");
                General.msjError("Ocurrió un error al obtener los datos de remisiones.");
            }
            else
            {
                if (leer.Leer())
                {
                    dtsConcentrado.Tables.Add(leer.Tabla(1).Copy());
                    dtsDetallado.Tables.Add(leer.Tabla(2).Copy());
                    leerExcelConcentrado.DataSetClase = dtsConcentrado;
                    leerExcelDetallado.DataSetClase = dtsDetallado;
                }
            }
             
        }
        
        #endregion Funciones

        #region Eventos_Distribuidor
        private void txtIdDistribuidor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdDistribuidor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Distribuidores(txtIdDistribuidor.Text.Trim(), "txtIdDistribuidor_Validating");
                if (leer.Leer())
                {
                    CargaDatosDistribuidor();
                    btnEjecutar.Enabled = true;
                }
                else
                {
                    txtIdDistribuidor.Focus();
                }
            }
        }

        private void txtIdDistribuidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Distribuidores("txtIdDistribuidor_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosDistribuidor();
                    btnEjecutar.Enabled = true;
                }
            }
        }

        private void txtIdDistribuidor_TextChanged(object sender, EventArgs e)
        {
            lblDistribuidor.Text = "";
        }
        #endregion Eventos_Distribuidor

        #region Eventos_ListView
        private void lstFoliosRemisiones_DoubleClick(object sender, EventArgs e)
        {
            ////string sFolio = "";

            ////sFolio = lst.GetValue(1);

            ////FrmRemisionesDistribuidor f = new FrmRemisionesDistribuidor();
            ////f.LevantaForma(sFolio);
        }
        #endregion Eventos_ListView

        #region Reportes
        private void ReporteFoliosRemisiones()
        {
            //int iRow = 2;
            //string sNombreFile = "";
            //string sPeriodo = "";
            //string sRutaReportes = "", sRutaPlantilla = "";
            
            //sRutaReportes = GnFarmacia.RutaReportes;
            //DtGeneral.RutaReportes = sRutaReportes;

            //sNombreFile = "Rpt_TipoRemisionesDistribuidor" + "_" + DtGeneral.FarmaciaConectada + ".xls";
            //sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_TipoRemisionesDistribuidor.xls";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_TipoRemisionesDistribuidor.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    HabilitarBotones(false, false, false, bMostrarImprimir);
            //    HabilitarControles(false);

            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        //Se pone el encabezado
            //        leerExportarExcel.RegistroActual = 1;
            //        leerExportarExcel.Leer();
            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            //        iRow++;
            //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
            //        iRow++;

            //        xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, iRow, 2);
            //        iRow++;

            //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //           General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));
            //        xpExcel.Agregar(sPeriodo, iRow, 2);

            //        iRow = 7;
            //        xpExcel.Agregar(txtIdDistribuidor.Text + " -- " + lblDistribuidor.Text, iRow, 3);

            //        iRow = 8;
            //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

            //        // Se ponen los detalles
            //        leerExportarExcel.RegistroActual = 1;
            //        iRow = 11;

            //        while (leerExportarExcel.Leer())
            //        {
            //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 2);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Codigo Cliente"), iRow, 3);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Cliente"), iRow, 4);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Referencia Documento"), iRow, 5);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Fecha Documento"), iRow, 6);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Tipo Remision"), iRow, 7);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRow, 8);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Total Piezas"), iRow, 9);

            //            iRow++;
            //        }

            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //        HabilitarBotones(true, true, true, bMostrarImprimir);
            //        HabilitarControles(true);
            //    }
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void RptRemisionesConcentrado()
        {
            //int iRow = 2;
            //string sNombreFile = "";
            //string sPeriodo = "", sConcepto = "";
            //string sRutaReportes = "", sRutaPlantilla = "";

            //if (cboReporte.SelectedIndex == 3)
            //{
            //    sConcepto = "VENTA";
            //}
            //if (cboReporte.SelectedIndex == 4)
            //{
            //    sConcepto = "ADMINISTRACION";
            //}

            //sRutaReportes = GnFarmacia.RutaReportes;
            //DtGeneral.RutaReportes = sRutaReportes;

            //sNombreFile = "Remisiones_Concentrado" + ".xls";
            //sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_RemisionesDist_Concentrado_Mensual.xls";

            //this.Cursor = Cursors.WaitCursor; 
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_RemisionesDist_Concentrado_Mensual.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    HabilitarBotones(false, false, false, false);
            //    HabilitarControles(false);

            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        //Se pone el encabezado
            //        leerExcelConcentrado.RegistroActual = 1;
            //        leerExcelConcentrado.Leer();
            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            //        iRow++;
            //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
            //        iRow++;

            //        xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, iRow, 2);

            //        iRow = 6;
            //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //           General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));
            //        xpExcel.Agregar(sPeriodo, iRow, 2);

            //        iRow = 8;
            //        xpExcel.Agregar(txtIdDistribuidor.Text + " -- " + lblDistribuidor.Text, iRow, 3);

            //        iRow = 9;
            //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
            //        xpExcel.Agregar(sConcepto, iRow, 5);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("Piezas"), iRow, 6);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("Cajas"), iRow, 7);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("Total"), iRow, 8);

            //        // Se ponen los detalles
            //        leerExcelConcentrado.RegistroActual = 1;
            //        iRow = 12;

            //        while (leerExcelConcentrado.Leer())
            //        {
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("Folio"), iRow, 2);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("Remision"), iRow, 3);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("CodigoCliente"), iRow, 4);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("Cliente"), iRow, 5);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("IdFarmaciaRelacionada"), iRow, 6);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("FarmaciaRelacionada"), iRow, 7);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("FechaDocumento"), iRow, 8);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("CantidadPiezas"), iRow, 9);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("CantidadCajas"), iRow, 10);
            //            xpExcel.Agregar(leerExcelConcentrado.Campo("Importe"), iRow, 11);

            //            iRow++;
            //        }

            //        leerExcelConcentrado.RegistroActual = 1;
            //        leerExcelConcentrado.Leer();
            //        xpExcel.Agregar("SUB-TOTAL :", iRow, 9);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("SubTotal"), iRow, 10);

            //        iRow++;
            //        xpExcel.Agregar("IVA :", iRow, 9);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("Iva"), iRow, 10);

            //        iRow++;
            //        xpExcel.Agregar("TOTAL :", iRow, 9);
            //        xpExcel.Agregar(leerExcelConcentrado.Campo("Total"), iRow, 10);
            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //        HabilitarBotones(true, true, true, true);
            //        HabilitarControles(true);
            //    }
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void RptRemisionesDetallado()
        {
            //int iRow = 2;
            //string sNombreFile = "";
            //string sPeriodo = "", sConcepto = "";
            //string sRutaReportes = "", sRutaPlantilla = "";

            //if (cboReporte.SelectedIndex == 3)
            //{
            //    sConcepto = "VENTA";
            //}
            //if (cboReporte.SelectedIndex == 4)
            //{
            //    sConcepto = "ADMINISTRACION";
            //}

            //sRutaReportes = GnFarmacia.RutaReportes;
            //DtGeneral.RutaReportes = sRutaReportes;

            //sNombreFile = "Remisiones_Detallado" + ".xls";
            //sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_RemisionesDist_Detallado_Mensual.xls";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_RemisionesDist_Detallado_Mensual.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    HabilitarBotones(false, false, false, false);
            //    HabilitarControles(false);

            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        //Se pone el encabezado
            //        leerExcelDetallado.RegistroActual = 1;
            //        leerExcelDetallado.Leer();
            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
            //        iRow++;
            //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
            //        iRow++;

            //        xpExcel.Agregar(DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, iRow, 2);

            //        iRow = 6;
            //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //           General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));
            //        xpExcel.Agregar(sPeriodo, iRow, 2);

            //        iRow = 8;
            //        xpExcel.Agregar(txtIdDistribuidor.Text + " -- " + lblDistribuidor.Text, iRow, 3);

            //        iRow = 9;
            //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
            //        xpExcel.Agregar(sConcepto, iRow, 6);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("Piezas"), iRow, 7);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("Cajas"), iRow, 8);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("Total"), iRow, 9);

            //        // Se ponen los detalles
            //        leerExcelDetallado.RegistroActual = 1;
            //        iRow = 12;

            //        while (leerExcelDetallado.Leer())
            //        {
            //            xpExcel.Agregar(leerExcelDetallado.Campo("Folio"), iRow, 2);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("Remision"), iRow, 3);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("Cliente"), iRow, 4);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("FechaDocumento"), iRow, 5);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("ClaveSSA"), iRow, 6);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("DescripcionSal"), iRow, 7);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("PrecioUnitario"), iRow, 8);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("CantidadPiezas"), iRow, 9);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("CantidadCajas"), iRow, 10);
            //            xpExcel.Agregar(leerExcelDetallado.Campo("Importe"), iRow, 11);

            //            iRow++;
            //        }

            //        leerExcelDetallado.RegistroActual = 1;
            //        leerExcelDetallado.Leer();
            //        xpExcel.Agregar("SUB-TOTAL :", iRow, 8);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("SubTotal"), iRow, 9);

            //        iRow++;
            //        xpExcel.Agregar("IVA :", iRow, 8);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("Iva"), iRow, 9);

            //        iRow++;
            //        xpExcel.Agregar("TOTAL :", iRow, 8);
            //        xpExcel.Agregar(leerExcelDetallado.Campo("Total"), iRow, 9);
            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //        HabilitarBotones(true, true, true, true);
            //        HabilitarControles(true);
            //    }
            //    this.Cursor = Cursors.Default; 
            //}
        }
        #endregion Reportes

        #region Impresion
        private void GenerarDocumentos(int Perfil, string Titulo)
        {
            bool bRegresa = false;
            int iEsConsignacion = 0;
            string sFile = "";

            sFile = string.Format("{0}_{1}_RemisionesConcentrado__{2}_{3}", sEstado, sFarmacia, Fg.PonCeros(Perfil, 2), Titulo);
            sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");

            DatosCliente.Funcion = "GenerarDocumentos()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            if (cboReporte.SelectedIndex == 4)
            {
                iEsConsignacion = 1;
            }

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.NombreReporte = "PtoVta_Remisiones_Dist_Concentrado.rpt";

            myRpt.Add("@IdEmpresa", sEmpresa);
            myRpt.Add("@IdEstado", sEstado);
            myRpt.Add("@IdFarmacia", sFarmacia);
            myRpt.Add("@IdDistribuidor", txtIdDistribuidor.Text);
            myRpt.Add("@EsConsignacion", iEsConsignacion);
            myRpt.Add("@FechaIni", General.FechaYMD(dtpFechaInicio.Value, "-"));
            myRpt.Add("@FechaFin", General.FechaYMD(dtpFechaFin.Value, "-"));
            myRpt.Add("@PrecioAdmon", txtPrecioAdmon.NumericText);
            myRpt.Add("@IdPerfilDeAtencion", Perfil);

            Reporteador = new clsReporteador(myRpt, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);

            ////bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (bRegresa)
            {
                sFile = string.Format("{0}_{1}_RemisionesDetallado__{2}_{3}", sEstado, sFarmacia, Fg.PonCeros(Perfil, 2), Titulo);
                sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");
                myRpt.NombreReporte = "PtoVta_Remisiones_Dist_Detallado.rpt";

                Reporteador = new clsReporteador(myRpt, DatosCliente);
                Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
                Reporteador.Url = General.Url;
                Reporteador.MostrarInterface = false;
                Reporteador.MostrarMensaje_ReporteSinDatos = false;

                bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);
            }

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion

        #region Generacion_de_Reportes
        private bool Obtener_Cuadros_De_Atencion()
        {
            bool bRegresa = false;

            string sSql = string.Format(
                "Select 0 as IdPerfilAtencion, 'Sin Especificar' as Titulo " +
                "   UNION " +
                "Select IdPerfilAtencion, Descripcion as Titulo " +
                "From CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' ",
                sEmpresa, sEstado, sFarmacia);

            bRegresa = leerCuadrosDeAtencion.Exec(sSql);

            return bRegresa;
        }

        private void GeneraReportes_Remision()
        {
            int iPerfil = 0;
            string sTitulo = "";

            bEjecutando = true; 
            //Se procesa cada Perfil de atencion 
            Obtener_Cuadros_De_Atencion();
            leerCuadrosDeAtencion.RegistroActual = 1;
            while (leerCuadrosDeAtencion.Leer())
            {
                iPerfil = leerCuadrosDeAtencion.CampoInt("IdPerfilAtencion");
                sTitulo = leerCuadrosDeAtencion.Campo("Titulo");

                GenerarDocumentos(iPerfil, sTitulo);
            }
    
            bEjecutando = false;
            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            HabilitarBotones(true, true, bMostrarExcel, bMostrarImprimir);
            HabilitarControles(true);
            MostrarEnProceso(false);
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 178;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = true;

            if (!bFolderDestino)
            {
                bRegresa = false;
                General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
            }           

            if (bRegresa)
            {
                if (!Obtener_Cuadros_De_Atencion())
                {
                    bRegresa = false;
                    General.msjError("No se encontraron cuadros de atención para este Almacén, verifique.");
                }
            }

            return bRegresa;
        }

        private void CrearDirectorioDestino()
        {
            string sDir = string.Format("FI_{0}_FF_{1}",
                General.FechaYMD(dtpFechaInicio.Value, ""), General.FechaYMD(dtpFechaFin.Value, ""));
            string sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema);
            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "__" + sMarcaTiempo;
            if (!Directory.Exists(sRutaDestino_Archivos))
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private void IniciaGeneracionReportes()
        {
            bSeEncontroInformacion = false;
            HabilitarBotones(false, false, false, false);
            HabilitarControles(false);          

            MostrarEnProceso(true);            
            btnDirectorio.Enabled = false;
            
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.GeneraReportes_Remision);
            _workerThread.Name = "GenerandoReportes";
            _workerThread.Start();            
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los reportes generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            }
        }
        #endregion Generacion_de_Reportes
        
    }
}
