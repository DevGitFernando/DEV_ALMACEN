using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using ClosedXML.Excel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using Dll_IFacturacion;


namespace Facturacion.Reportes
{
    public partial class FrmReporteMensualDoctosCancelados : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsExportarExcelPlantilla xpExcel;

        string sGenerarReporteMensualGeneral = "GENERAR_REPORTE_GENERAL_FACTURACION";
        bool bGenerarReporteMensualGeneral = false;

        public FrmReporteMensualDoctosCancelados()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            SolicitarPermisosUsuario();
        }

        #region Permisos de Usuario
        /// <summary>
        /// Obtiene los privilegios para el usuario conectado 
        /// </summary>
        private void SolicitarPermisosUsuario()
        {
            bGenerarReporteMensualGeneral = DtGeneral.PermisosEspeciales.TienePermiso(sGenerarReporteMensualGeneral);
        }
        #endregion Permisos de Usuario

        private void FrmReporteMensualDoctosCancelados_Load(object sender, EventArgs e)
        {
            CargarMeses();
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            chkReporteGeneral.Visible = bGenerarReporteMensualGeneral; 
            dtpAño.Focus();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }
        #endregion Botones

        #region Funciones y Procedimientos Privados
        private void CargarMeses()
        {
            cboMes.Clear();
            cboMes.Add();

            cboMes.Add("1", "Enero");
            cboMes.Add("2", "Febrero");
            cboMes.Add("3", "Marzo");
            cboMes.Add("4", "Abril");
            cboMes.Add("5", "Mayo");
            cboMes.Add("6", "Junio");
            cboMes.Add("7", "Julio");
            cboMes.Add("8", "Agosto");
            cboMes.Add("9", "Septiembre");
            cboMes.Add("10", "Octubre");
            cboMes.Add("11", "Noviembre");
            cboMes.Add("12", "Diciembre");

            cboMes.SelectedIndex = 0;
        }

        private void GenerarExcel()
        {
            ExportarInformacion("");

        }

        enum Cols
        {
            IdEstado = 2, Estado, FechaEmision, TipoDeDocumento, Serie, Folio, SubTotal, Iva, Total, 
            FormaDePago, MetodoDePago, ReferenciaPago, 
            RFC,
            NombreReceptor, StatusDocumento, 
            Observaciones_01, Observaciones_02, Observaciones_03, 
            IdPersonalEmite, NombrePersonalEmite,
            IdPersonalCancela, NombrePersonalCancela, FechaCancelacion, MotivoCancelacion
        }

        private bool ExportarInformacion(string NombreArchivo)
        {
            bool bRegresa = false;
            string sSql = "";
            string sFiltro = "";
            string sFiltroPeriodo = "";
            int iRow = 2;
            int iRowsProceso = 1; 
            string sFileTxt = "";
            string sCadena = "";
            DateTime dateTime = DateTime.Now;
            string sFecha = "";
            string sAño = "";
            StreamWriter fileOut;
            ////FileInfo f = new FileInfo(xpExcel.DocumentoGenerado);

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            string sNombre = string.Format("REPORTE_MENSUAL_CFDIS_CANCELADOS_{0}{1}", Fg.PonCeros(dtpAño.Value.Year, 4), Fg.PonCeros(cboMes.Data, 2));
            string sNombreHoja = sNombre;


            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 10;
            string sFechaImpresion = "", sDetalle = ""; 


            sSql = string.Format("Exec spp_FACT_RTP__CFDI_Cancelados_ToExcel  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @EsReporteGeneral = '{3}', @Año = '{4}', @Mes = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, chkReporteGeneral.Checked ? 1 : 0, dtpAño.Value.Year, cboMes.Data); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ExportarInformacion()");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información para el periodo solicitado.");
                }
                else
                {
                    bRegresa = true; 
                    leer.RegistroActual = 1;
                    ////xpExcel.MostrarAvanceProceso = true; 
                    ////xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;

                    sAño = dtpAño.Value.Year.ToString().Replace(",", "");
                    sAño = sAño.Replace(" ", ""); 

                    iRow = 2;
                    generarExcel.RutaArchivo = @"C:\\Excel";
                    generarExcel.NombreArchivo = sNombre;
                    generarExcel.AgregarMarcaDeTiempo = true;

                    if(generarExcel.PrepararPlantilla(sNombre))
                    {
                        sNombreHoja = "CFDIS_CANCELADOS"; 
                        generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                        generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, string.Format("LISTADO DE DOCUMENTOS CANCELADOS EN {0} {1}", cboMes.Text.ToUpper(), sAño));
                        generarExcel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                        iRenglon = 7;
                        generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                        generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                        generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;


                        generarExcel.CerraArchivo();

                        generarExcel.AbrirDocumentoGenerado(true); 
                    }


                    ////iRow = 8; 
                    ////while (leer.Leer())
                    ////{
                    ////    dateTime = leer.CampoFecha("FechaEmision");
                    ////    sFecha = Fg.PonCeros(dateTime.Day, 2) + "/";
                    ////    sFecha += Fg.PonCeros(dateTime.Month, 2) + "/";
                    ////    sFecha += Fg.PonCeros(dateTime.Year, 4) + " ";

                    ////    sFecha += Fg.PonCeros(dateTime.Hour, 2) + ":";
                    ////    sFecha += Fg.PonCeros(dateTime.Minute, 2) + ":";
                    ////    sFecha += Fg.PonCeros(dateTime.Second, 2) + "";

                    ////    xpExcel.Agregar(leer.Campo("IdEstado"), iRow, (int)Cols.IdEstado);
                    ////    xpExcel.Agregar(leer.Campo("Estado"), iRow, (int)Cols.Estado);
                    ////    xpExcel.Agregar(leer.CampoFecha("FechaEmision"), iRow, (int)Cols.FechaEmision);
                    ////    xpExcel.Agregar(leer.Campo("TipoDeDocumento"), iRow, (int)Cols.TipoDeDocumento);
                    ////    xpExcel.Agregar(leer.Campo("Serie"), iRow, (int)Cols.Serie);
                    ////    xpExcel.Agregar(leer.Campo("Folio"), iRow, (int)Cols.Folio);
                    ////    xpExcel.Agregar(leer.CampoDouble("SubTotal"), iRow, (int)Cols.SubTotal);
                    ////    xpExcel.Agregar(leer.CampoDouble("Iva"), iRow, (int)Cols.Iva);
                    ////    xpExcel.Agregar(leer.CampoDouble("Total"), iRow, (int)Cols.Total);

                    ////    xpExcel.Agregar(leer.Campo("FormaDePago"), iRow, (int)Cols.FormaDePago);
                    ////    xpExcel.Agregar(leer.Campo("MetodoDePago"), iRow, (int)Cols.MetodoDePago);
                    ////    xpExcel.Agregar(leer.Campo("ReferenciaDePago"), iRow, (int)Cols.ReferenciaPago); 

                    ////    xpExcel.Agregar(leer.Campo("RFC"), iRow, (int)Cols.RFC);
                    ////    xpExcel.Agregar(leer.Campo("NombreReceptor"), iRow, (int)Cols.NombreReceptor);

                    ////    xpExcel.Agregar(leer.Campo("StatusDocumento"), iRow, (int)Cols.StatusDocumento);
                    ////    xpExcel.Agregar(leer.Campo("Observaciones_01"), iRow, (int)Cols.Observaciones_01);
                    ////    xpExcel.Agregar(leer.Campo("Observaciones_02"), iRow, (int)Cols.Observaciones_02);
                    ////    xpExcel.Agregar(leer.Campo("Observaciones_03"), iRow, (int)Cols.Observaciones_03);


                    ////    xpExcel.Agregar(leer.Campo("IdPersonalEmite"), iRow, (int)Cols.IdPersonalEmite);
                    ////    xpExcel.Agregar(leer.Campo("PersonalEmite"), iRow, (int)Cols.NombrePersonalEmite);
                    ////    xpExcel.Agregar(leer.Campo("IdPersonalCancela"), iRow, (int)Cols.IdPersonalCancela);
                    ////    xpExcel.Agregar(leer.Campo("PersonalCancela"), iRow, (int)Cols.NombrePersonalCancela);

                    ////    if (leer.Campo("FechaCancelacion").Trim() != "")
                    ////    {
                    ////        xpExcel.Agregar(leer.Campo("FechaCancelacion"), iRow, (int)Cols.FechaCancelacion);
                    ////    }

                    ////    xpExcel.Agregar(leer.Campo("MotivoCancelacion"), iRow, (int)Cols.MotivoCancelacion); 
                    ////    iRow++;
                    ////    iRowsProceso++;
                    ////    xpExcel.NumeroRenglonesProcesados++; 
                    ////}
                }
            }

            ////xpExcel.CerrarDocumento();
            /////fileOut.Close(); 

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados

        #region Descargar Documentos 
        private void btnDescargarXMLs_Click(object sender, EventArgs e)
        {
            ////DescargarDocumentos_XML_PDF(false); 
        }

        private void btnDescargarPDFs_Click(object sender, EventArgs e)
        {
            ////DescargarDocumentos_XML_PDF(true);
        }

        private void DescargarDocumentos_XML_PDF(bool DescargarPDF)
        {
            ////string sRutaDeDescarga = "";

            ////FolderBrowserDialog folder = new FolderBrowserDialog();
            ////folder.Description = "Directorio destino para los documentos generados.";
            ////folder.RootFolder = Environment.SpecialFolder.Desktop;
            ////folder.ShowNewFolderButton = true;

            ////if (folder.ShowDialog() == DialogResult.OK)
            ////{
            ////    sRutaDeDescarga = folder.SelectedPath + @"\";

            ////    //////for (int i = 9; i <= 12; i++)
            ////    {
            ////        FrmReporteMensualDoctosCancelados_Descargar f = new FrmReporteMensualDoctosCancelados_Descargar
            ////            (DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, chkReporteGeneral.Checked,
            ////            dtpAño.Value.Year.ToString(), cboMes.Data, sRutaDeDescarga, DescargarPDF); 
            ////        f.ShowDialog(this); 
            ////    }
            ////}
        }
        #endregion Descargar Documentos
    }
}
