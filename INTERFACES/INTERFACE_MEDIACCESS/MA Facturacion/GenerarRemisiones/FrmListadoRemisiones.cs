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
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using DllFarmaciaSoft;

namespace MA_Facturacion.GenerarRemisiones
{
    public partial class FrmListadoRemisiones : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel;
        clsDatosCliente DatosCliente;
        clsAyudas Ayudas;
        clsConsultas Consultas;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;

        clsAuditoria auditoria;

        private enum Cols
        {
            Ninguna = 0,
            Folio = 2, Fecha = 3, IdCliente = 4, Cliente = 5, IdSubCliente = 6, SubCliente = 7, Importe = 8, Remision = 9, Status = 10, Insumo = 11
        }

        public FrmListadoRemisiones()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");
            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExcel = new clsLeer(ref cnn);
            
            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstRemisiones);
            lst.OrdenarColumnas = false;

            AnchoTitulos();
        }

        private void FrmListadoRemisiones_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            //Cargar_Folios_Remisiones();
            IniciarProcesamiento();
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            IniciaToolBar(false, false);
            IniciaFrames(false);
            
            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.Cargar_Folios_Remisiones);
            _workerThread.Name = "ObteniendoDatos";
            _workerThread.Start();

            btnNuevo.Enabled = true;
            IniciaFrames(true);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //if (CargarDatosRemisiones())
            {
                GenerarExcel();
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            LimpiaControles(false);
            IniciaToolBar(true, false);
            lst.LimpiarItems();
        }

        private void LimpiaControles(bool Valor)
        {
            rdoFacturable.Checked = Valor;
            rdoNoFacturable.Checked = Valor;
            //rdoAmbos.Checked = Valor;

            rdoMedicamento.Checked = Valor;
            rdoMatCuracion.Checked = Valor;
            //rdoAmbosInsumos.Checked = Valor;

        }

        private void AnchoTitulos()
        {
            lst.AnchoColumna(1, 110); lst.AnchoColumna(2, 100); lst.AnchoColumna(3, 70);
            lst.AnchoColumna(4, 150); lst.AnchoColumna(5, 90); lst.AnchoColumna(6, 150);
            lst.AnchoColumna(7, 80); lst.AnchoColumna(8, 150); lst.AnchoColumna(9, 80);
            lst.AnchoColumna(10, 150); lst.AnchoColumna(11, 100); lst.AnchoColumna(12, 100);
            lst.AnchoColumna(13, 120);
        }

        private void Cargar_Folios_Remisiones()
        {
            string sSql = "", sWhereFecha = "";
            int iFacturable = 3, iTipoInsumo = 3;            

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            if (rdoFacturable.Checked)
            {
                iFacturable = 1;
            }

            if (rdoNoFacturable.Checked)
            {
                iFacturable = 0;
            }

            if (rdoMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            sSql = string.Format(" Exec spp_INT_MA__FACT_Rpt_ListaDeRemisiones @IdEmpresa = '{0}',  @IdEstado = '{1}', @IdFarmacia = '{2}', " +
	                        "@EsFacturable = {3}, @IdTipoInsumo = {4}, @FechaInicial = '{5}', @FechaFinal = '{6}' ", sEmpresa, sEstado, sFarmacia,
                             iFacturable, iTipoInsumo, General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "Cargar_Folios_Remisiones()");
                General.msjError("Ocurrió un error al consultar los folios de remisiones..");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase);
                    IniciaToolBar(true, true);
                    bSeEncontroInformacion = true;
                }
                else
                {
                    bSeEncontroInformacion = false;
                    bSeEjecuto = true;
                    General.msjAviso("No se encontro información con los criterios especificados..");
                    IniciaToolBar(true, false);
                }
            }

            AnchoTitulos();

            bEjecutando = false;
            this.Cursor = Cursors.Default;
        }

        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportar.Enabled = Exportar;
        }

        private void IniciaFrames(bool Valor)
        {
            FrameTipoRemisiones.Enabled = Valor;
            FrameInsumos.Enabled = Valor;
            FrameFechas.Enabled = Valor;
            FrameRemisiones.Enabled = Valor;
        }
        #endregion Funciones

        #region Exportar_Excel
        private bool CargarDatosRemisiones()
        {
            bool bRegresa = false;
            string sSql = "", sWhereFecha = "";
            int iFacturable = 1, iNoFacturable = 0;
            string sMedicamento = "02", sMatCuracion = "01";

            if (rdoFacturable.Checked)
            {
                iNoFacturable = 1;
            }

            if (rdoNoFacturable.Checked)
            {
                iFacturable = 0;
            }

            if (rdoMedicamento.Checked)
            {
                sMatCuracion = "02";
            }

            if (rdoMatCuracion.Checked)
            {
                sMedicamento = "01";
            }


            sSql = string.Format(" Select FolioRemision, Convert(varchar(10), FechaRemision, 120) as FechaRemision, IdFuenteFinanciamiento, Estado,  " +
                                " IdFinanciamiento, Financiamiento, IdCliente, Cliente, IdSubCliente, SubCliente, Total,  " +
                                " case when EsFacturable = 0 Then 'NO FACTURABLE' Else 'FACTURABLE' end as Es_Facturable,  " +
                                " case when Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end as StatusRemision, TipoDeInsumo  " +
                                " From vw_FACT_TipoRemisiones (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  " +
                                " and EsFacturable in ({3}, {4}) and TipoInsumo In ('{5}', '{6}')  {7} ", sEmpresa, sEstado, sFarmacia,
                                iNoFacturable, iFacturable, sMedicamento, sMatCuracion, sWhereFecha);

            //lst.LimpiarItems();

            if (!leerExcel.Exec(sSql))
            {
                Error.GrabarError(leerExcel, "CargarDatosRemisiones()");
                General.msjError("Ocurrió un error al consultar los folios de remisiones..");
            }
            else
            {
                if (leerExcel.Leer())
                {
                    bRegresa = true;                    
                }
                else
                {
                    General.msjAviso("No se encontro información para el reporte...");
                }
            }

            return bRegresa;
            
        }

        private void GenerarExcel()
        {

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\INT_MA__FACT_LISTADO_REMISIONES.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "INT_MA__FACT_LISTADO_REMISIONES.xls", DatosCliente);

            if (bRegresa)
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = true;
                //leer.DataSetClase = dtsExistencias;

                this.Cursor = Cursors.Default;
                if (xpExcel.PrepararPlantilla())
                {
                    IniciaToolBar(false, false);
                    this.Cursor = Cursors.WaitCursor;

                    ExportarRemisiones();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarRemisiones()
        {
            int iHoja = 1, iRenglon = 9;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "LISTADO DE FOLIOS DE REMISIONES";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            leerExcel.DataSetClase = leer.DataSetClase;

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 4, 2);       

            
            xpExcel.Agregar(sFechaImpresion, 6, 3);

            leerExcel.RegistroActual = 1;

            while (leerExcel.Leer())
            {
                xpExcel.Agregar(leerExcel.Campo("FolioRemision"), iRenglon, (int)Cols.Folio);
                xpExcel.Agregar(leerExcel.Campo("FechaRemision"), iRenglon, (int)Cols.Fecha);
                xpExcel.Agregar(leerExcel.Campo("IdCliente"), iRenglon, (int)Cols.IdCliente);
                xpExcel.Agregar(leerExcel.Campo("Cliente"), iRenglon, (int)Cols.Cliente);
                xpExcel.Agregar(leerExcel.Campo("IdSubCliente"), iRenglon, (int)Cols.IdSubCliente);
                xpExcel.Agregar(leerExcel.Campo("SubCliente"), iRenglon, (int)Cols.SubCliente);
                xpExcel.Agregar(leerExcel.Campo("Importe"), iRenglon, (int)Cols.Importe);
                xpExcel.Agregar(leerExcel.Campo("TipoDeRemisionDesc"), iRenglon, (int)Cols.Remision);
                xpExcel.Agregar(leerExcel.Campo("StatusDesc"), iRenglon, (int)Cols.Status);
                xpExcel.Agregar(leerExcel.Campo("TipoDeInsumoDesc"), iRenglon, (int)Cols.Insumo);
                
                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Exportar_Excel
    }
}
