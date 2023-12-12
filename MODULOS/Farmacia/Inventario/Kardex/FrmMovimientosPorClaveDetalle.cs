using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Inventario
{
    public partial class FrmMovimientosPorClaveDetalle : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsListView list;
        clsLeer  leer;
        string sTipo = "";
        string sDescTipo= "";
        string sFechaIni = "";
        string sFechaFin = "";
        string sClave = "";
        string sDesc= "";
        
        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\MOVIMIENTO_CLAVE_DETALLADO.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente; 

        public FrmMovimientosPorClaveDetalle(DataSet DsDetalle, string TipoMovto, string DescTipoMovto, string FechaInicial,
                                             string FechaFinal, string ClaveSSA, string Descripcion)
        {
            sTipo = TipoMovto;
            sDescTipo = DescTipoMovto;
            sFechaIni = FechaInicial;
            sFechaFin = FechaFinal;
            sClave = ClaveSSA;
            sDesc = Descripcion;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            leer = new clsLeer();
            InitializeComponent();
            list = new clsListView(listMovimientos);
            list.Limpiar();
            list.CargarDatos(DsDetalle, true, true);
            leer.DataSetClase = DsDetalle;
        }

        private void FrmMovimientosPorClaveDetalle_Load(object sender, EventArgs e)
        {
            FrameDetalle.Text += " " + sDescTipo;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", sFechaIni, sFechaFin);
            string sNombreFile = "Kardex por clave Detallado";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 3;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, 3, 16, "Clave SSA:", XLAlignmentHorizontalValues.Right);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase + 1, iColsEncabezado, 16, sClave + "--" + sDesc, XLAlignmentHorizontalValues.Left);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon, iColBase, 3, 16, "Tipo de movimiento:", XLAlignmentHorizontalValues.Right);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase + 1, iColsEncabezado, 16, sDescTipo, XLAlignmentHorizontalValues.Left);
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
        /*
            bool bRegresa = false; 

            if (Leer.Registros == 0)
            {
                General.msjAviso("No existe información para exportar, verifique.");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "MOVIMIENTO_CLAVE_DETALLADO.xls", DatosCliente);

                if (!bRegresa)
                {
                    this.Cursor = Cursors.Default;
                    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                }
                else
                {
                    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                    xpExcel.AgregarMarcaDeTiempo = false;

                    if (xpExcel.PrepararPlantilla())
                    {
                        xpExcel.GeneraExcel();
                        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
                        xpExcel.Agregar("De " + sFechaIni + " a " + sFechaFin, 4, 2);
                        xpExcel.Agregar(sClave + "--" + sDesc, 6, 3);
                        xpExcel.Agregar(sDescTipo, 8, 3);

                        switch (sTipo)
                        {
                            case "TS":
                                xpExcel.Agregar("Farmacia destino", 10, 5);
                                break;
                            case "TE":
                                xpExcel.Agregar("Farmacia recibe", 10, 5);
                                break;
                            case "EC":
                                xpExcel.Agregar("Factura", 10, 5);
                                break;
                            case "CC":
                                xpExcel.Agregar("Factura cancelada", 10, 5);
                                break;
                            case "SV":
                                xpExcel.Agregar("Beneficiario", 10, 5);
                                break;
                        }

                        for (int iRow = 11; Leer.Leer(); iRow++)
                        {
                            xpExcel.Agregar(Leer.Campo("Folio"), iRow, 2);
                            //xpExcel.Agregar(Leer.Campo("Movimiento"), iRow, 3);
                            xpExcel.Agregar(Leer.Campo("Fecha"), iRow, 3);
                            xpExcel.Agregar(Leer.Campo("Cantidad"), iRow, 4);
                            xpExcel.Agregar(Leer.Campo("Observaciones"), iRow, 5);
                            switch (sTipo)
                            {
                                case "TS":
                                    xpExcel.Agregar(Leer.Campo("Farmacia destino"), iRow, 5);
                                    break;
                                case "TE":
                                    xpExcel.Agregar(Leer.Campo("Farmacia recibe"), iRow, 5);
                                    break;
                                case "EC":
                                    xpExcel.Agregar(Leer.Campo("Factura"), iRow, 5);
                                    break;
                                case "CC":
                                    xpExcel.Agregar(Leer.Campo("Factura cancelada"), iRow, 5);
                                    break;
                                case "SV":
                                    xpExcel.Agregar(Leer.Campo("Beneficiario"), iRow, 5);
                                    break;
                            }
                        }
                        xpExcel.CerrarDocumento();

                        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                        {
                            xpExcel.AbrirDocumentoGenerado();
                        }
                        // General.msjUser("Exportacion finalizada."); 
                    }
                }
                this.Cursor = Cursors.Default;
            }
            btnExportarExcel.Enabled = false;
        }
        */

        private void listMovimientos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
