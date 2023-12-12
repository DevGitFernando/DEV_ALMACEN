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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.wsFarmacia;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Informacion
{
    public partial class FrmListadoPreciosProductos : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);        
        clsLeer myLeer;
        clsLeer leer;      
        
        ////Thread _workerThread;        
        clsDatosCliente DatosCliente;
        clsListView lst;

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        DataSet dtsConsumos = new DataSet();
        ////clsConsultas Consultas;
        ////clsAyudas ayuda;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");

        string sExportar_Listado_Precios = "EXPORTAR_LISTA_PRECIOS_EXCEL";

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;
       
        public FrmListadoPreciosProductos()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");            
           
            CheckForIllegalCrossThreadCalls = false;            

            myLeer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            ////Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ////ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            
            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.DatosConexion, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            DtGeneral.IdPersonal, DtGeneral.IdSesion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
        }

        private void FrmListadoOrdenesDeCompras_Load(object sender, EventArgs e)
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
            CargarListaPrecios();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            string sCadena = "";

            sCadena = leer.QueryEjecutado;
            sCadena = sCadena.Replace("'", "\"");

            toolStripBarraMenu.Enabled = false;
            GenerarReporteExcel();
            auditoria.GuardarAud_MovtosUni("*", sCadena);
            toolStripBarraMenu.Enabled = true;
        }
        #endregion Botones

        #region Reportes
        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "LISTA DE PRECIOS DE PRODUCTOS DE COMPRAS";
            string sNombreFile = "COM_Rpt_ListadoPreciosProductos" + "_" + DtGeneral.EstadoConectado;

            leer.DataSetClase = leerExportarExcel.DataSetClase;

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

        //private void GeneraReporteDetallado()
        //{
        //    int iRow = 2;
        //    string sNombreFile = "";
        //    ////string sPeriodo = "";
        //    string sRutaReportes = "";

        //    HabilitarControles(false);
        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "COM_Rpt_ListadoPreciosProductos" + "_" + DtGeneral.EstadoConectado + ".xls";
        //    sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_ListadoPreciosProductos.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_ListadoPreciosProductos.xls", DatosCliente);            

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;
            
        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {                
        //        xpExcel.GeneraExcel();

        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        xpExcel.Agregar( DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow++;
        //        xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
        //        iRow++;

        //        ////sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
        //        ////   General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        ////xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;
                
        //        while (leerExportarExcel.Leer())
        //        {
        //            xpExcel.Agregar(leerExportarExcel.Campo("Id Proveedor"), iRow, 2);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Nombre"), iRow, 3);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Clave SSA"), iRow, 4);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripción SSA"), iRow, 5);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Producto"), iRow, 6);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Codigo EAN"), iRow, 7);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripción"), iRow, 8);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Status Precio"), iRow, 9);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio"), iRow, 10);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descuento"), iRow, 11);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Tasa Iva"), iRow, 12);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Iva"), iRow, 13);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio Unitario"), iRow, 14);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Fecha de Registro"), iRow, 15);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Fecha de Vigencia"), iRow, 16);

        //            iRow++;
        //        }                

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }               
        //        HabilitarControles(true);
        //    }
        //}
        #endregion Reportes

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            //myGrid.Limpiar(false);
            lst.Limpiar();
            btnExportarExcel.Enabled = false;
            FrameDetalles.Enabled = true;       
            
        }

        private void CargarListaPrecios() 
        {          
            
            string sSql = "";
            sSql = string.Format(" Exec spp_Rpt_COM_Listado_PreciosProductos  ");
            
            if (!leer.Exec(sSql))
            {
                General.msjError("Ocurrió un Error al realizar la consulta de Lista de Precios");
                Error.GrabarError(leer, sSql, "CargarListaPrecios()", "");                
            }
            else
            {                
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información de Precios");                    
                }
                else
                {                    
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    if (DtGeneral.PermisosEspeciales.TienePermiso(sExportar_Listado_Precios))
                    {
                        leerExportarExcel.DataSetClase = leer.DataSetClase;
                        btnExportarExcel.Enabled = true;
                    }
                }                
            }           
        }

        private void HabilitarControles(bool bValor)
        {
            btnNuevo.Enabled = bValor;
            btnEjecutar.Enabled = bValor;         
            //FrameDetalles.Enabled = bValor;
            if (DtGeneral.PermisosEspeciales.TienePermiso(sExportar_Listado_Precios))
            {
                btnExportarExcel.Enabled = bValor;
            }
        }
        #endregion Funciones             
        
    }
}
