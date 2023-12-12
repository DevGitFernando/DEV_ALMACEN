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
    public partial class Frm_RptRotacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsDatosCliente DatosCliente;
        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        DataSet dtsRotaciones;

        private enum Cols
        {
            Ninguna = 0,
            Producto = 2, EAN = 3, ClaveSSA = 4, Descripcion = 5, DescRotacion = 6, 
        }

        public Frm_RptRotacion()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);
            DatosCliente = new clsDatosCliente(General.DatosApp, this.Name, "");

            lst = new clsListView(lstResultado);

            CargarListaReportes();
        }

        private void Frm_RptRotacion_Load(object sender, EventArgs e)
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
            SendKeys.Send("TAB");
            if (ValidarDatos())
            {
                CargarRotaciones();
            }
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
            cboTipoReporte.SelectedIndex = 0;
        }

        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void CargarListaReportes()
        {
            cboTipoReporte.Clear();
            cboTipoReporte.Add("0", "<<Seleccione>>"); // Agrega Item Default 
            cboTipoReporte.Add("1", "Días de Inventario");
            cboTipoReporte.Add("2", "Número de Prescripciones");
            cboTipoReporte.Add("3", "Volumen Dispensado");
            cboTipoReporte.SelectedIndex = 0;

        }

        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (cboTipoReporte.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de reporte.. Verifique !!");
                cboTipoReporte.Focus();
            }

            if (bRegresa && nmBaja.Value < nmMedia.Value)
            {
                bRegresa = false;
                General.msjAviso("Criterios de Rotación. La Media no puede ser mayor a la Baja. Verifique !!");
                nmBaja.Focus();
            }

            if (bRegresa && nmMedia.Value < nmAlta.Value)
            {
                bRegresa = false;
                General.msjAviso("Criterios de Rotación. La Alta no puede ser mayor a la Media. Verifique !!");
                nmAlta.Focus();
            }

            

            return bRegresa;
        }

        private void CargarRotaciones()
        {
            string sSql = "";
            int iTipoCB = 0;

            if (rdoCB.Checked)
            {
                iTipoCB = 1;
            }

            if (rdoNoCB.Checked)
            {
                iTipoCB = 2;
            }

            sSql = string.Format(" Exec spp_Rpt_ALMN_RotacionClaves_Productos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ",
                        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                        cboTipoReporte.Data, iTipoCB, nmDiasAnalisis.Value, nmAlta.Value, nmMedia.Value, nmBaja.Value);

            lst.LimpiarItems();

            dtsRotaciones = new DataSet();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarRotaciones");
                General.msjError("Ocurrió un error al obtener el listado de Rotación de Productos.");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciaToolBar(true, true, false);
                    General.msjUser("No se encontró información de Rotación de Productos.");
                }
                else
                {
                    dtsRotaciones = leer.DataSetClase;

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
            string sNombreDocumento = "REPORTE DE ROTACIÓN";
            string sNombreHoja = "ROTACIÓN";
            string sConcepto = "Reporte de Rotación de Productos por " + cboTipoReporte.Text;

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
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsRotaciones);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
        }

        //private void ExportarRotacionProductos()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = "Reporte de Rotación de Productos por " + cboTipoReporte.Text;
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    leer.DataSetClase = dtsRotaciones;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("Producto"), iRenglon, (int)Cols.Producto);
        //        xpExcel.Agregar(leer.Campo("Código EAN"), iRenglon, (int)Cols.EAN);
        //        xpExcel.Agregar(leer.Campo("Clave SSA"), iRenglon, (int)Cols.ClaveSSA);
        //        xpExcel.Agregar(leer.Campo("Descripción Producto"), iRenglon, (int)Cols.Descripcion);
        //        xpExcel.Agregar(leer.Campo("Descripción Rotación"), iRenglon, (int)Cols.DescRotacion);
                
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}
        #endregion Exportar_Excel
    }
}
