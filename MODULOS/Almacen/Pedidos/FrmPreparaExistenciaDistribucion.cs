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
using SC_SolutionsSystem.Errores; 
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.Reportes;

//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmPreparaExistenciaDistribucion : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsListView lst;
        //clsExportarExcelPlantilla xpExcel;
        DataSet dtsExistencias;
        bool bExisteTablaDeDistribucion = false; 

        private enum Cols
        {
            Ninguna = 0,
            ClaveSSA = 2, Descripcion = 3, Presentacion = 4, Existencia = 5
        }

        public FrmPreparaExistenciaDistribucion()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            lst = new clsListView(listvwExistencia);
            validarExiste_TablaDeDistribucion(); 
        }

        private void FrmPreparaExistenciaDistribucion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lst.LimpiarItems();
            IniciaToolBar(true, true, false);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarExistenciaDistribucion(); 
        }

        private void btnGenerarExistencia_Click(object sender, EventArgs e)
        {
            ////string sMensaje = "Se encontrarón folios de surtimiento pendientes de generar transferencia ó venta.\n\n" +
            ////            "No es posible generar la existencia para la distribución de pedidos, verifique el status de los folios de surtimiento.";
            ////if (!DtGeneral.TieneSurtimientosActivos())
            ////{
            ////    GenerarExistencia();
            ////}
            ////else
            ////{
            ////    General.msjAviso(sMensaje);
            ////}
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }
        #endregion Botones

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Generar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnGenerarExistencia.Enabled = Generar;
            btnExportarExcel.Enabled = Exportar;

            if ( !bExisteTablaDeDistribucion ) 
            {
                btnEjecutar.Enabled = false; 
            }
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "Reporte de Existencia para Generación de Pedidos";
            string sNombreFile = "Reporte de Existencia para Generación de Pedidos";

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
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ALMN_Rpt_Preparar_Existencia_Distribucion.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ALMN_Rpt_Preparar_Existencia_Distribucion.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;
        //        //leer.DataSetClase = dtsExistencias;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ExportarExistencias();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarExistencias()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = "Reporte de Existencia para Generación de Pedidos";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    leer.DataSetClase = dtsExistencias;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)Cols.ClaveSSA);
        //        xpExcel.Agregar(leer.Campo("DescripcionClave"), iRenglon, (int)Cols.Descripcion);
        //        xpExcel.Agregar(leer.Campo("Presentacion"), iRenglon, (int)Cols.Presentacion);
        //        xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)Cols.Existencia);                
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //} 
        #endregion Funciones

        #region Funciones y Procedimientos Privados 
        private void validarExiste_TablaDeDistribucion()
        {
            bExisteTablaDeDistribucion = false;
            string sSql = string.Format(" Select * From sysobjects Where Name = 'FarmaciaProductos_ALM_Distribucion' and xType = 'U' ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
            }
            else
            {
                bExisteTablaDeDistribucion = leer.Leer(); 
            }
        }

        private void CargarExistenciaDistribucion()
        {
            string sSql = 
                string.Format(
                " Select ClaveSSA, DescripcionClave, Presentacion, cast(sum(Existencia) as int) as Disponible " +
                " From FarmaciaProductos_ALM_Distribucion " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
                " Group by ClaveSSA, DescripcionClave, Presentacion " + 
                " Order by DescripcionClave ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada );

            dtsExistencias = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la existencia disponible para distribución.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciaToolBar(true, true, false);
                    General.msjUser("No se encontró información de existencia disponible para distribución.");
                }
                else
                {
                    dtsExistencias = leer.DataSetClase;

                    IniciaToolBar( true, true, true); 
                    lst.CargarDatos(leer.DataSetClase, false, false); 
                }
            }
        }

        private bool validarSurtimientos()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_ALM_ValidarGeneracionExistenciaDistribucion '{0}', '{1}', '{2}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada); 

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "validarSurtimientos()"); 
            }
            else
            {
                bRegresa = !leer.Leer();

                if (!bRegresa)
                {
                    General.msjAviso("Se encontrarón folios de surtimiento pendientes de generar transferencia ó venta.\n\n" + 
                        "No es posible generar la existencia para la distribución de pedidos, verifique el status de los folios de surtimiento."); 
                } 
            }

            return bRegresa; 
        }

        private void GenerarExistencia()
        {
            string sGUID = Guid.NewGuid().ToString().Replace("-", "");

            string sSql = string.Format("Exec spp_ALM_GenerarExistenciaDistribucion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdPersonal = '{3}', @GUID = '{4}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, DtGeneral.IdPersonal, General.NombreEquipo, sGUID);

            dtsExistencias = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al generar la existencia para distribución de pedidos.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciaToolBar(true, true, false);
                    General.msjUser("No se encontró información para mostrar.");
                }
                else 
                {
                    dtsExistencias = leer.DataSetClase;

                    IniciaToolBar(true, false, true);
                    lst.CargarDatos(leer.DataSetClase, false, false);
                    //lst.AlternarColorRenglones(Color.Lavender, Color.LightBlue);

                    //Se ajusta la columna Descripcion al elemento mas grande. Para ajustar el encabezado se utiliza -2
                    //listvwExistencia.Columns[(int)Cols.Descripcion - 2].Width = -1;
                }
            }

        }
        #endregion Funciones y Procedimientos Privados 

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos
    }
}
