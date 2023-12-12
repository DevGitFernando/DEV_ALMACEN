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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmRtp_AtencionesSurtidores_Choferes : FrmBaseExt
    {
        enum Cols
        {
            IdSurtidor = 2, Surtidor = 3,
            IdJurisdiccion = 4, Jurisdiccion = 4, 
            IdFarmacia = 5, Farmacia = 6, FarmaciaSolicita = 7, Folio = 8, Surtimiento = 9, Fecha = 10, Status = 11, StatusDescripcion = 12  
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente; 
        clsLeer leer;
        clsConsultas query;
        clsListView lst; 

        DataSet dtsFarmacias = new DataSet();
        DataSet dtsSurtimientos = new DataSet();
               
        //clsExportarExcelPlantilla xpExcel;

        public FrmRtp_AtencionesSurtidores_Choferes()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRtp_AtencionesSurtidores_Choferes");

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos); 
        }

        private void FrmRtp_AtencionesSurtidores_Choferes_Load(object sender, EventArgs e)
        {
            CargarJurisdicciones();
            CargarStatusPedidos();
            //LlenarSurtidores();

            btnNuevo_Click(null, null);
            //CargarFechaDistribucion(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            lst.LimpiarItems();
            rdoSurtidor.Checked = false;
            rdoChoferes.Checked = false;
            rdoSurtidor.Enabled = true;
            rdoChoferes.Enabled = true;
            cboSurtidor.Clear();
            cboSurtidor.Add("*", "<< Seleccione >>");
            cboSurtidor.SelectedIndex = 0;
            btnExportarExcel.Enabled = false;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                CargarListaDeSurtimientos();
            }
        }
        #endregion Botones

        #region CargarCombos 
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "<< Todas las jurisdicciones >>");

                cboJurisdicciones.Add(query.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion"); 
                dtsFarmacias = query.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()"); 
            }

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

            if ( cboJurisdicciones.SelectedIndex != 0 ) 
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia"); 
            } 

            cboFarmacias.SelectedIndex = 0; 
        }

        private void LlenarSurtidores_Choferes()
        {
            int iIdPuesto = 0;

            if (rdoSurtidor.Checked)
            {
                iIdPuesto = 1;
            }

            if (rdoChoferes.Checked)
            {
                iIdPuesto = 2;
            }

            string sSql = string.Format(" Select IdPersonal, Personal From vw_PersonalCEDIS (NoLock) " +
                                        " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdPuesto = '{2}' Order By Personal ",
                                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Fg.PonCeros(iIdPuesto.ToString(), 2));
            
            cboSurtidor.Clear();
            cboSurtidor.Add("*", "<< Todo el personal >>");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "LlenarSurtidores_Choferes()");
                General.msjError("Ocurrió un error al obtener la Lista de Personal.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboSurtidor.Add(leer.DataSetClase, true);
                }
                cboSurtidor.SelectedIndex = 0;

            }
        }
        #endregion CargarCombos 

        #region Eventos 
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                CargarFarmacias(); 
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void rdoSurtidor_CheckedChanged(object sender, EventArgs e)
        {
            rdoSurtidor.Enabled = false;
            rdoChoferes.Enabled = false;
            LlenarSurtidores_Choferes();
        }

        private void rdoChoferes_CheckedChanged(object sender, EventArgs e)
        {
            rdoSurtidor.Enabled = false;
            rdoChoferes.Enabled = false;
            LlenarSurtidores_Choferes();
        }
        #endregion Eventos

        #region Funciones y Procedimientos Privados 
        private void CargarStatusPedidos()
        {
            cboStatusPedidos.Clear();
            cboStatusPedidos.Add("0", "TODO");

            leer.DataSetClase = query.PedidosSurtimiento_Status("CargarStatusPedidos()");
            if (leer.Leer())
            {
                cboStatusPedidos.Add(leer.DataSetClase, true, "ClaveStatus", "Descripcion"); 
            }

            ////cboStatusPedidos.Add("1", "SURTIMIENTO");
            ////cboStatusPedidos.Add("2", "SURTIDO");
            ////cboStatusPedidos.Add("3", "EN VALIDACIÓN");
            ////cboStatusPedidos.Add("4", "DISTRIBUCIÓN");
            ////cboStatusPedidos.Add("5", "TRANSITO");
            ////cboStatusPedidos.Add("6", "REGISTRADO");
            ////cboStatusPedidos.Add("7", "FINALIZADO");
            ////cboStatusPedidos.Add("8", "CANCELADO");
 
        }
        
        private void CargarListaDeSurtimientos()
        {
            btnExportarExcel.Enabled = false;

            int iIdPuesto = 0;

            if (rdoSurtidor.Checked)
            {
                iIdPuesto = 1;
            }

            if (rdoChoferes.Checked)
            {
                iIdPuesto = 2;
            }

            string sSql = string.Format("Exec spp_Rpt_Atenciones_Surtidores_Choferes_Cedis '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboJurisdicciones.Data, cboFarmacias.Data, cboSurtidor.Data,
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), cboStatusPedidos.Data, Fg.PonCeros(iIdPuesto.ToString(), 2));

            lst.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarListaDeSurtimientos()");
                General.msjError("Ocurrió un error al obtener la lista de los surtimientos.");                
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados.");                    
                }
                else 
                {
                    dtsSurtimientos = leer.DataSetClase;
                    lst.CargarDatos(leer.DataSetClase, false, false);
                    btnExportarExcel.Enabled = true;
                }
            }
        }

        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (!rdoSurtidor.Checked & !rdoChoferes.Checked)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de personal. Verifique...");
                cboSurtidor.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados             

        #region Exportar_A_Excel
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
            string sNombre = "Reporte de Atenciones";
            string sNombreFile = "Atenciones_Surtidores_Choferes_Cedis";

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
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

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ALMN_Rpt_Atenciones_Surtidores_Choferes_Cedis.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ALMN_Rpt_Atenciones_Surtidores_Choferes_Cedis.xls", datosCliente);

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
        //            ExportarSurtimientos();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarSurtimientos()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = "Reporte de Atenciones";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    if (rdoSurtidor.Checked)
        //    {
        //        sConceptoReporte = "Reporte de Atenciones Surtidores";
        //    }

        //    if (rdoChoferes.Checked)
        //    {
        //        sConceptoReporte = "Reporte de Atenciones Choferes";
        //    }

        //    leer.DataSetClase = dtsSurtimientos;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("Núm. Personal"), iRenglon, (int)Cols.IdSurtidor);
        //        xpExcel.Agregar(leer.Campo("Personal"), iRenglon, (int)Cols.Surtidor);
        //        xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRenglon, (int)Cols.Jurisdiccion);
        //        xpExcel.Agregar(leer.Campo("IdFarmacia"), iRenglon, (int)Cols.IdFarmacia);
        //        xpExcel.Agregar(leer.Campo("Farmacia"), iRenglon, (int)Cols.Farmacia);
        //        xpExcel.Agregar(leer.Campo("Farmacia Solicita"), iRenglon, (int)Cols.FarmaciaSolicita);
        //        xpExcel.Agregar(leer.Campo("Folio"), iRenglon, (int)Cols.Folio);
        //        xpExcel.Agregar(leer.Campo("Surtimientos"), iRenglon, (int)Cols.Surtimiento);
        //        xpExcel.Agregar(leer.Campo("Fecha"), iRenglon, (int)Cols.Fecha);
        //        xpExcel.Agregar(leer.Campo("Status"), iRenglon, (int)Cols.Status);
        //        xpExcel.Agregar(leer.Campo("Status pedido"), iRenglon, (int)Cols.StatusDescripcion);
                
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();

        //}
        #endregion Exportar_A_Excel
        
    }
}
