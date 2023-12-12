using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Seguimiento
{
    public partial class FrmVerificarOrdenesCompras : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosConexion DatosDeConexionRemota = new clsDatosConexion();
        clsDatosCliente DatosCliente;
        clsLeer leer;
        //clsExportarExcelPlantilla xpExcel;
        
        clsListView lst;
        clsListView lstDet;
        DataSet dtsDatos;
        DataSet dtsClaves;
        DataSet dtsDetalles;        

        string sUrl_Unidad = "";
        string sFarmacia = "";
        string sFolioOrdenOrigen = "";

        private enum Cols_Exportar
        {
            Ninguna = 0,
            FolioOrden = 2, Clave = 3, Producto = 4, CodigoEAN = 5, Descripcion = 6, CantidadIngresada = 7 
        }

        public FrmVerificarOrdenesCompras()
        {
            InitializeComponent();
            
            leer = new clsLeer(ref cnn);

            //DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, ""); 

            dtsDatos = new DataSet();
            dtsClaves = new DataSet();
            dtsDetalles = new DataSet();

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = false;

            lstDet = new clsListView(lstDetalles);
            lstDet.OrdenarColumnas = true;
            lstDet.PermitirAjusteDeColumnas = false;
        }

        private void FrmVerificarOrdenesCompras_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        #region Funciones_Publicas
        public string Folio_Orden
        {
            set { sFolioOrdenOrigen = value; }
        }

        public void MostrarPantalla(DataSet Claves, clsDatosConexion DatosConexionRemota, string UrlUnidad, string Farmacia)
        {
            dtsDatos = Claves.Copy();
            DatosDeConexionRemota = DatosConexionRemota;
            sUrl_Unidad = UrlUnidad;
            sFarmacia = Farmacia;

            this.ShowDialog();
        }
        #endregion Funciones_Publicas

        #region Funciones
        private void CargarDatos()
        {
            leer.DataSetClase = dtsDatos;            

            if (leer.Leer())
            {
                //lst.CargarDatos(leer.DataSetClase, true, true);
                dtsClaves.Tables.Add(leer.Tabla(1).Copy());
                dtsDetalles.Tables.Add(leer.Tabla(2).Copy());
                lst.CargarDatos(dtsClaves, true, true);
                lstDet.CargarDatos(dtsDetalles, true, true);


                //// Asignar anchos de columnas 
                lst.AnchoColumna(1, 120);
                lst.AnchoColumna(2, 480);
                lst.AnchoColumna(3, 120);
                lst.AnchoColumna(4, 120);
                lst.AnchoColumna(5, 120);

                //// Asignar anchos de columnas 
                lstDet.AnchoColumna(1, 90);
                lstDet.AnchoColumna(2, 120);
                lstDet.AnchoColumna(3, 90);
                lstDet.AnchoColumna(4, 120);
                lstDet.AnchoColumna(5, 420);
                lstDet.AnchoColumna(6, 120); 
            }

        }
        #endregion Funciones

        #region ImprimirFolios
        private void ImprimirFolioOC()
        {
            bool bRegresa = false;
            string sFolio = "";

            sFolio = lstDet.GetValue(1);

            if (sFolio.Trim() == "")
            {
                General.msjAviso("Debe seleccionar un folio de entrada para generar la impresión."); 
            }
            else 
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(DatosDeConexionRemota);
                // byte[] btReporte = null;

                string sEstado = DtGeneral.EstadoConectado;

                myRpt.RutaReporte = GnCompras.RutaReportes;
                myRpt.NombreReporte = "PtoVta_Recepcion_Orden_Compras.rpt";

                myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("IdEstado", sEstado);
                myRpt.Add("IdFarmacia", sFarmacia);
                myRpt.Add("Folio", sFolio);

                
                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente);

                if (!bRegresa && !DtGeneral.CanceladoPorUsuario) 
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFolioOC();
        }
        #endregion ImprimirFolios

        #region Exportar a Excel 
        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("DETALLE DE ORDEN DE COMPRA DEL FOLIO {0} ", sFolioOrdenOrigen);
            string sNombreFile = string.Format("Verificar_Detalles__OC-{0}.xls", sFolioOrdenOrigen);

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            //leer.RegistroActual = 1;


            int iColsEncabezado = iColBase + leer.Columnas.Length;
            //iColsEncabezado = iRenglon + 8;

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

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsDetalles);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void GenerarExcel()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "", sRutaReportes = "", sRutaPlantilla = "", sPeriodo = "";
        //    string sRutaArchivoGenerado = Application.StartupPath + @"\\Descargas";

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    CrearRutaArchivo(sRutaArchivoGenerado);
        //    sNombreFile = "Verificar_Ordenes_Compra_Detalles.xls";
        //    sNombreFile = string.Format("Verificar_Detalles__OC-{0}.xls", sFolioOrdenOrigen); 
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_Verificar_Ordenes_Compra_Detalles.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_Verificar_Ordenes_Compra_Detalles.xls", DatosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;

        //    if (xpExcel.PrepararPlantilla(sRutaArchivoGenerado, sNombreFile))
        //    {
        //        xpExcel.GeneraExcel();

        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
        //        iRow++;

        //        sPeriodo = string.Format("DETALLE DE ORDEN DE COMPRA DEL FOLIO {0} ", sFolioOrdenOrigen);
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 7;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        //leerExportarExcel.RegistroActual = 1;
        //        iRow = 10;

        //        for (int i = 1; i <= lstDet.Registros; i++)
        //        {
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.FolioOrden - 1), iRow, (int)Cols_Exportar.FolioOrden);
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.Clave - 1), iRow, (int)Cols_Exportar.Clave);
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.Producto - 1), iRow, (int)Cols_Exportar.Producto);
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.CodigoEAN - 1), iRow, (int)Cols_Exportar.CodigoEAN);
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.Descripcion - 1), iRow, (int)Cols_Exportar.Descripcion);
        //            xpExcel.Agregar(lstDet.GetValue(i, (int)Cols_Exportar.CantidadIngresada - 1), iRow, (int)Cols_Exportar.CantidadIngresada);
                    
        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }

        //    }
        //}
        //private void CrearRutaArchivo(string sRutaSalida)
        //{
        //    if (!Directory.Exists(sRutaSalida))
        //    {
        //        Directory.CreateDirectory(sRutaSalida); 
        //    }
        //}
        #endregion Exportar a Excel
    
    }
}
