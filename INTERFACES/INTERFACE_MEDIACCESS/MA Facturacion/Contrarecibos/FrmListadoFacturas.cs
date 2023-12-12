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

namespace MA_Facturacion.Contrarecibos
{
    public partial class FrmListadoFacturas : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leer2, leerExcel, leerExcelFact;
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
            Folio = 2, Factura = 3, Fecha = 4, TipoFactura = 5, Importe = 6, Status = 7, Insumo = 8
        }

        public FrmListadoFacturas()
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
            leerExcelFact = new clsLeer(ref cnn);

            Error = new clsGrabarError(DtGeneral.Modulo, DtGeneral.Version, this.Name);
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal,
                                        DtGeneral.IdSesion, GnOficinaCentral.Modulo, this.Name, GnOficinaCentral.Version);

            lst = new clsListView(lstFacturas);
            lst.OrdenarColumnas = false;
        }

        private void FrmListadoFacturas_Load(object sender, EventArgs e)
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
            //Cargar_Folios_Facturas();
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

            _workerThread = new Thread(this.Cargar_Folios_Facturas);
            _workerThread.Name = "ObteniendoDatos";
            _workerThread.Start();

            btnNuevo.Enabled = true;
            IniciaFrames(true);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (CargarDatosFacturas())
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
            rdoEnCobro.Checked = Valor;
            rdoSinCobrar.Checked = Valor;
            //rdoAmbos.Checked = Valor;

            rdoMedicamento.Checked = Valor;
            rdoMatCuracion.Checked = Valor;
            //rdoAmbosInsumos.Checked = Valor;

            chkTodasFechas.Checked = Valor;
        }

        private void AnchoTitulos()
        {
            lst.AnchoColumna(1, 120); lst.AnchoColumna(2, 150); lst.AnchoColumna(3, 100);
            lst.AnchoColumna(4, 150); lst.AnchoColumna(5, 120); lst.AnchoColumna(6, 120);
            lst.AnchoColumna(7, 150); 
        }

        private void Cargar_Folios_Facturas()
        {
            string sSql = "", sWhereFecha = "";
            int iEnCobro = 1, iSinCobrar = 0;
            string sMedicamento = "02", sMatCuracion = "01";

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;

            if (rdoEnCobro.Checked)
            {
                iSinCobrar = 1;
            }

            if (rdoSinCobrar.Checked)
            {
                iEnCobro = 0;
            }

            if (rdoMedicamento.Checked)
            {
                sMatCuracion = "02";
            }

            if (rdoMatCuracion.Checked)
            {
                sMedicamento = "01";
            }

            if (!chkTodasFechas.Checked)
            {
                sWhereFecha = string.Format(" and Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                                        General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));
            }

            sSql = string.Format(" Select Folio, NumFactura, Convert(varchar(10), FechaRegistro, 120) as FechaRegistro, TipoDeFacturaDesc,  " +
                                " Importe, case when Status = 'A' Then 'ACTIVA' Else 'CANCELADA' end as StatusFactura, TipoDeInsumo  " +
                                " From vw_FACT_Facturas (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  " +
                                " and EstaEnCobro in ({3}, {4}) and TipoInsumo In ('{5}', '{6}')  {7} ", sEmpresa, sEstado, sFarmacia,
                                iSinCobrar, iEnCobro, sMedicamento, sMatCuracion, sWhereFecha);

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "Cargar_Folios_Facturas()");
                General.msjError("Ocurrió un error al consultar los folios de facturas..");
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
                    General.msjAviso("No se encontro información con los criterios especificados..");
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
            FrameTipoFacturas.Enabled = Valor;
            FrameInsumos.Enabled = Valor;
            FrameFechas.Enabled = Valor;
            FrameFacturas.Enabled = Valor;
        }
        #endregion Funciones

        #region Exportar_Excel
        private bool CargarDatosFacturas()
        {
            bool bRegresa = false;
            string sSql = "", sWhereFecha = "";
            int iEnCobro = 1, iSinCobrar = 0;
            string sMedicamento = "02", sMatCuracion = "01";

            if (rdoEnCobro.Checked)
            {
                iSinCobrar = 1;
            }

            if (rdoSinCobrar.Checked)
            {
                iEnCobro = 0;
            }

            if (rdoMedicamento.Checked)
            {
                sMatCuracion = "02";
            }

            if (rdoMatCuracion.Checked)
            {
                sMedicamento = "01";
            }

            if (!chkTodasFechas.Checked)
            {
                sWhereFecha = string.Format(" and Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                                        General.FechaYMD(dtpFechaInicio.Value, "-"), General.FechaYMD(dtpFechaFin.Value, "-"));
            }

            sSql = string.Format(" Select Folio, NumFactura, Convert(varchar(10), FechaRegistro, 120) as FechaRegistro, TipoDeFacturaDesc,  " +
                                " Importe, case when Status = 'A' Then 'ACTIVA' Else 'CANCELADA' end as StatusFactura, TipoDeInsumo  " +
                                " From vw_FACT_Facturas (NoLock)  " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}'  " +
                                " and EstaEnCobro in ({3}, {4}) and TipoInsumo In ('{5}', '{6}')  {7} ", sEmpresa, sEstado, sFarmacia,
                                iSinCobrar, iEnCobro, sMedicamento, sMatCuracion, sWhereFecha);

        

            if (!leerExcel.Exec(sSql))
            {
                Error.GrabarError(leerExcel, "CargarDatosFacturas()");
                General.msjError("Ocurrió un error al consultar los folios de facturas..");
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

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_LISTADO_FACTURAS.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "FACT_LISTADO_FACTURAS.xls", DatosCliente);

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

                    ExportarFoliosFacturas();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarFoliosFacturas()
        {
            int iHoja = 1, iRenglon = 9;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "LISTADO DE FOLIOS DE FACTURAS";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 4, 2);


            xpExcel.Agregar(sFechaImpresion, 6, 3);

            leerExcel.RegistroActual = 1;

            while (leerExcel.Leer())
            {
                xpExcel.Agregar(leerExcel.Campo("Folio"), iRenglon, (int)Cols.Folio);
                xpExcel.Agregar(leerExcel.Campo("NumFactura"), iRenglon, (int)Cols.Factura);
                xpExcel.Agregar(leerExcel.Campo("FechaRegistro"), iRenglon, (int)Cols.Fecha);
                xpExcel.Agregar(leerExcel.Campo("TipoDeFacturaDesc"), iRenglon, (int)Cols.TipoFactura);
                xpExcel.Agregar(leerExcel.Campo("Importe"), iRenglon, (int)Cols.Importe);
                xpExcel.Agregar(leerExcel.Campo("StatusFactura"), iRenglon, (int)Cols.Status);
                xpExcel.Agregar(leerExcel.Campo("TipoDeInsumo"), iRenglon, (int)Cols.Insumo);                            

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Exportar_Excel

        #region Exportar_Facturacion
        private void btnExportaFactura_Click(object sender, EventArgs e)
        {
            CargarFacturacion();
        }

        private bool CargarFacturacion()
        {
            bool bRegresa = false;
            string sSql = "", sFolioFactura = "";

            sFolioFactura = lst.GetValue(1);

            sSql = string.Format(" Exec spp_FACT_Rtp_DetalladoFactura '{0}', '{1}', '{2}', '{3}' ", sEmpresa, sEstado, sFarmacia, sFolioFactura);

            if (!leerExcelFact.Exec(sSql))
            {
                Error.GrabarError(leerExcelFact, "CargarFacturacion()");
                General.msjError("Ocurrió un error al obtener el reporte de la Factura..");
            }
            else
            {
                if (leerExcelFact.Leer())
                {
                    bRegresa = true;
                    GenerarExcelFacturacion();
                }
                else
                {
                    General.msjAviso("No se encontro información para el reporte...");
                }
            }

            return bRegresa;

        }

        private void GenerarExcelFacturacion()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_RPT_VALIDACION_FACTURAS.xls";
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "FACT_RPT_VALIDACION_FACTURAS.xls", DatosCliente);

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

                    ExportarFact_Concentrado();
                    ExportarFact_Detallado();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarFact_Concentrado()
        {
            int iHoja = 1, iRenglon = 12;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            
            //string sConcepto = "LISTADO DE FOLIOS DE FACTURAS";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 6, 2);           


            xpExcel.Agregar(sFechaImpresion, 9, 3);

            leerExcelFact.RegistroActual = 1;

            while (leerExcelFact.Leer())
            {
                xpExcel.Agregar(leerExcelFact.Campo("Año"), iRenglon, 2);
                xpExcel.Agregar(leerExcelFact.Campo("Mes"), iRenglon, 3);
                xpExcel.Agregar(leerExcelFact.Campo("IdFarmacia"), iRenglon, 4);
                xpExcel.Agregar(leerExcelFact.Campo("Farmacia"), iRenglon, 5);
                xpExcel.Agregar(leerExcelFact.Campo("FolioFacturaElectronica"), iRenglon, 6);
                xpExcel.Agregar(leerExcelFact.Campo("IdPrograma"), iRenglon, 7);
                xpExcel.Agregar(leerExcelFact.Campo("IdSubPrograma"), iRenglon, 8);
                xpExcel.Agregar(leerExcelFact.Campo("SubPrograma"), iRenglon, 9);
                xpExcel.Agregar(leerExcelFact.Campo("Contrato"), iRenglon, 10);
                xpExcel.Agregar(leerExcelFact.Campo("TipoDeInsumo"), iRenglon, 11);
                xpExcel.Agregar(leerExcelFact.Campo("ClaveSSA"), iRenglon, 12);
                xpExcel.Agregar(leerExcelFact.Campo("DescripcionClave"), iRenglon, 13);
                xpExcel.Agregar(leerExcelFact.Campo("Presentacion"), iRenglon, 14);
                xpExcel.Agregar(leerExcelFact.Campo("ContenidoPaquete"), iRenglon, 15);
                xpExcel.Agregar(leerExcelFact.Campo("Cantidad"), iRenglon, 16);
                xpExcel.Agregar(leerExcelFact.Campo("PrecioUnitario"), iRenglon, 17);
                xpExcel.Agregar(leerExcelFact.Campo("SubTotal"), iRenglon, 18);
                xpExcel.Agregar(leerExcelFact.Campo("Iva"), iRenglon, 19);
                xpExcel.Agregar(leerExcelFact.Campo("Importe"), iRenglon, 20);
                xpExcel.Agregar(leerExcelFact.Campo("ConceptoFactura"), iRenglon, 21);                

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarFact_Detallado()
        {
            int iHoja = 2, iRenglon = 12;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;

            //string sConcepto = "LISTADO DE FOLIOS DE FACTURAS";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 6, 2);


            xpExcel.Agregar(sFechaImpresion, 9, 3);

            leerExcelFact.RegistroActual = 1;

            while (leerExcelFact.Leer())
            {
                xpExcel.Agregar(leerExcelFact.Campo("Año"), iRenglon, 2);
                xpExcel.Agregar(leerExcelFact.Campo("Mes"), iRenglon, 3);
                xpExcel.Agregar(leerExcelFact.Campo("IdFarmacia"), iRenglon, 4);
                xpExcel.Agregar(leerExcelFact.Campo("Farmacia"), iRenglon, 5);
                xpExcel.Agregar(leerExcelFact.Campo("FolioFacturaElectronica"), iRenglon, 6);
                xpExcel.Agregar(leerExcelFact.Campo("IdPrograma"), iRenglon, 7);
                xpExcel.Agregar(leerExcelFact.Campo("IdSubPrograma"), iRenglon, 8);
                xpExcel.Agregar(leerExcelFact.Campo("SubPrograma"), iRenglon, 9);
                xpExcel.Agregar(leerExcelFact.Campo("Contrato"), iRenglon, 10);
                xpExcel.Agregar(leerExcelFact.Campo("TipoDeInsumo"), iRenglon, 11);
                xpExcel.Agregar(leerExcelFact.Campo("ClaveSSA"), iRenglon, 12);
                xpExcel.Agregar(leerExcelFact.Campo("DescripcionClave"), iRenglon, 13);
                xpExcel.Agregar(leerExcelFact.Campo("Presentacion"), iRenglon, 14);
                xpExcel.Agregar(leerExcelFact.Campo("ContenidoPaquete"), iRenglon, 15);
                xpExcel.Agregar(leerExcelFact.Campo("Cantidad"), iRenglon, 16);
                xpExcel.Agregar(leerExcelFact.Campo("PrecioUnitario"), iRenglon, 17);
                xpExcel.Agregar(leerExcelFact.Campo("SubTotal"), iRenglon, 18);
                xpExcel.Agregar(leerExcelFact.Campo("Iva"), iRenglon, 19);
                xpExcel.Agregar(leerExcelFact.Campo("Importe"), iRenglon, 20);
                xpExcel.Agregar(leerExcelFact.Campo("FoliosRecetas"), iRenglon, 21);
                xpExcel.Agregar(leerExcelFact.Campo("FoliosTickects"), iRenglon, 22);
                xpExcel.Agregar(leerExcelFact.Campo("ConceptoFactura"), iRenglon, 23);

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Exportar_Facturacion
        
    }
}
