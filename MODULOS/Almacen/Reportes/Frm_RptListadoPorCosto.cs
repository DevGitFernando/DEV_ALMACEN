using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft;

namespace Almacen.Reportes
{
    public partial class Frm_RptListadoPorCosto : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        DataSet dtsUbicaciones;

        private enum Cols
        {
            Ninguna = 0,
            Producto = 2, EAN = 3, ClaveSSA = 4, Descripcion = 5, Lote = 6, Fecha = 7, Meses = 8,
            Costo = 9, Pasillo = 10, Estante = 11, Entrepaño = 12, Existencia = 13
        }

        public Frm_RptListadoPorCosto()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            lst = new clsListView(lstResultado);
        }

        private void Frm_RptListadoPorCosto_Load(object sender, EventArgs e)
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
            CargarCostosUbicaciones();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lst.LimpiarItems();
        }

        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void CargarCostosUbicaciones()
        {
            string sSql = "";

            sSql = string.Format(" Exec spp_Rpt_CostoProductos_Ubicaciones \n" + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @CostoMin = '{3}', @CostoMax = '{4}' \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, nmRangoInicial.Value, nmRangoFinal.Value);

            lst.LimpiarItems();

            dtsUbicaciones = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarCostosUbicaciones");
                General.msjError("Ocurrió un error al obtener el listado de ubicaciones.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciaToolBar(true, true, false);
                    General.msjUser("No se encontro información de ubicaciones.");
                }
                else
                {
                    dtsUbicaciones = leer.DataSetClase;

                    IniciaToolBar(true, true, true);
                    lst.CargarDatos(leer.DataSetClase, true, true);
                }
            }
        }
        #endregion Funciones

        #region Exportar_Excel
        private void GenerarExcel()
        {
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            string sNombreDocumento = "REPORTE DE COSTOS";
            string sNombreHoja = "COSTOS";
            string sConcepto = "Reporte de Costos Ubicaciones";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if(excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto.ToUpper());
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsUbicaciones);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        ////private void ExportarCostosUbicaciones()
        ////{
        ////    int iHoja = 1, iRenglon = 9;
        ////    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        ////    string sEstado = DtGeneral.EstadoConectadoNombre;
        ////    string sConceptoReporte = "Reporte de Costos Ubicaciones";
        ////    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        ////    leer.DataSetClase = dtsUbicaciones;
        ////    xpExcel.GeneraExcel(iHoja);

        ////    xpExcel.Agregar(sEmpresa, 2, 2);
        ////    xpExcel.Agregar(sEstado, 3, 2);
        ////    xpExcel.Agregar(sConceptoReporte, 4, 2);

        ////    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        ////    xpExcel.Agregar(sFechaImpresion, 6, 3);

        ////    while (leer.Leer())
        ////    {
        ////        xpExcel.Agregar(leer.Campo("Producto"), iRenglon, (int)Cols.Producto);
        ////        xpExcel.Agregar(leer.Campo("Codigo EAN"), iRenglon, (int)Cols.EAN);
        ////        xpExcel.Agregar(leer.Campo("Clave SSA"), iRenglon, (int)Cols.ClaveSSA);
        ////        xpExcel.Agregar(leer.Campo("Descripción"), iRenglon, (int)Cols.Descripcion);
        ////        xpExcel.Agregar(leer.Campo("Lote"), iRenglon, (int)Cols.Lote);
        ////        xpExcel.Agregar(leer.Campo("Fecha Caducidad"), iRenglon, (int)Cols.Fecha);
        ////        xpExcel.Agregar(leer.Campo("MesesParaCaducar"), iRenglon, (int)Cols.Meses);
        ////        xpExcel.Agregar(leer.Campo("Costo"), iRenglon, (int)Cols.Costo);
        ////        xpExcel.Agregar(leer.Campo("Rack"), iRenglon, (int)Cols.Pasillo);
        ////        xpExcel.Agregar(leer.Campo("Nivel"), iRenglon, (int)Cols.Estante);
        ////        xpExcel.Agregar(leer.Campo("Entrepaño"), iRenglon, (int)Cols.Entrepaño);
        ////        xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)Cols.Existencia);

        ////        iRenglon++;
        ////    }

        ////    // Finalizar el Proceso 
        ////    xpExcel.CerrarDocumento();

        ////}
        #endregion Exportar_Excel
    }
}
