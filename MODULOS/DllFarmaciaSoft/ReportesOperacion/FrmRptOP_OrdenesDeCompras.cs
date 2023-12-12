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
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_OrdenesDeCompras : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsConsultas consultas;
        clsAyudas ayuda;
        //clsGrid grid;
        clsListView lst; 

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\.xls";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;


        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        //Thread _workerThread;

        //bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false;

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        public FrmRptOP_OrdenesDeCompras()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_OrdenesDeCompras");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 
            
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            lst = new clsListView(lstResultado); 
        }

        #region Form 
        private void FrmRptOP_OrdenesDeCompras_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            rdoEntradas.Checked = false;
            rdoDevoluciones.Checked = false;
            IniciaToolBar(false, false);
            lst.LimpiarItems();
            txtIdProveedor.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarDatos_OC();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            CargarDetalles_OC();

            if (bSeEncontroInformacion)
            {
                GenerarExcel();
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            //btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = Exportar;
        }

        private void CargaDatosProveedor()
        {
            txtIdProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Nombre");
        }

        private void CargarDatos_OC()
        {
            string sSql = "", sWhereProveedor = "";

            if (txtIdProveedor.Text.Trim() != "")
            {
                sWhereProveedor = string.Format(" and IdProveedor = '{0}' ", Fg.PonCeros(txtIdProveedor.Text, 4));
            }

            if (rdoEntradas.Checked)
            {
                sSql = string.Format(" Select 'Fecha Entrada' = Convert(varchar(10), FechaRegistro, 120), 'Folio Orden Compra' = Folio, " +
                                    " 'Referencia Orden Compra' = FolioOrdenCompraReferencia, 'Núm. Proveedor' = IdProveedor, Proveedor " +
	                                " From vw_OrdenesDeComprasEnc (Nolock) " +
	                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}'  {5}  Order By FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor);
            }

            if (rdoDevoluciones.Checked)
            {
                sSql = string.Format(" Select 'Fecha Devolución' = Convert(varchar(10), FechaRegistro, 120), 'Folio Devolución' = Folio, " +
                                    " 'Folio Orden Compra' = FolioOrdenCompra, 'Referencia Orden Compra' = ReferenciaFolioOrdenCompra, " +
                                    " 'Núm. Proveedor' = IdProveedor, Proveedor " +
                                    " From vw_DevolucionesEnc_Orden_Compra (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}'  {5}  Order By FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"),
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor);
            }

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos_OC");
                General.msjError("Ocurrió un error al consultar los datos de Órdenes de Compra..");
            }
            else
            {
                if (leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    IniciaToolBar(true, true);
                }
                else
                {
                    General.msjAviso("No se encontró información bajo los criterios especificados...");
                }
            }

        }

        private void CargarDetalles_OC()
        {
            string sSql = "", sWhereProveedor = "";
            leerExportarExcel = new clsLeer(ref cnn);
            int iTipo = 1;

            //if (txtIdProveedor.Text.Trim() != "")
            //{
            //    sWhereProveedor = string.Format(" and IdProveedor = '{0}' ", Fg.PonCeros(txtIdProveedor.Text, 4));
            //}

            if (rdoEntradas.Checked)
            {
                iTipo = 1;
                //sSql = string.Format(" Select * " +
                //                    " From vw_Impresion_Recepcion_Orden_Compra (Nolock) " +
                //                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                //                    " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}'  {5}  Order By FechaRegistro, Folio ",
                //                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"),
                //                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor);
            }

            if (rdoDevoluciones.Checked)
            {
                iTipo = 2;
                //sSql = string.Format(" Select * " +
                //                    " From vw_Impresion_Devolucion_Orden_Compra (Nolock) " +
                //                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                //                    " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}'  {5}  Order By FechaRegistro, Folio ",
                //                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"),
                //                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor);
            }

            sSql = string.Format("Exec spp_RptOP_OrdenesDeCompras  " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdProveedor = '{3}', @FechaInicial = '{4}', @FechaFin = '{5}', @iTipo = '{6}' ",
                sEmpresa, sEstado, sFarmacia, Fg.PonCeros(txtIdProveedor.Text, 4), General.FechaYMD(dtpFechaInicial.Value, "-"),
                General.FechaYMD(dtpFechaFinal.Value, "-"), iTipo);

            if (!leerExportarExcel.Exec(sSql))
            {
                Error.GrabarError(leerExportarExcel, "CargarDetalles_OC");
                General.msjError("Ocurrió un error al consultar los detalles..");
            }
            else
            {
                if (!leerExportarExcel.Leer())
                {
                    bSeEncontroInformacion = false;
                    General.msjAviso("No existe información para mostrar..."); 
                }
                else
                {
                    bSeEncontroInformacion = true;
                }
            }

        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos
        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() == "")
            {
                //txtIdProveedor.Focus();
                IniciaToolBar(true, false);
            }
            else
            {
                leer.DataSetClase = consultas.Proveedores(txtIdProveedor.Text, "txtIdProveedor_Validating");

                if (leer.Leer())
                {
                    CargaDatosProveedor();
                    IniciaToolBar(true, false);
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Proveedores("txtIdProveedor_KeyDown");

                if (leer.Leer())
                {
                    CargaDatosProveedor();
                    IniciaToolBar(true, false);
                }
                else
                {
                    txtIdProveedor.Focus();
                }
            }
        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }
        #endregion Eventos        

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Exportar_A_Excel
        private void GenerarExcel()
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_OP_OrdenesDeCompras.xls";
            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = true; //// DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_OP_OrdenesDeCompras.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                //xpExcel.AgregarMarcaDeTiempo = true;
                //leer.DataSetClase = dtsExistencias;

                //this.Cursor = Cursors.Default;
                //if (xpExcel.PrepararPlantilla())
                {
                    IniciaToolBar(false, false);
                    GenearExcel_XML_Detalles(); 

                    ////if (rdoEntradas.Checked)
                    ////{
                    ////    //xpExcel.EliminarHoja("Detallado DEVOLUCIONES");
                    ////    //xpExcel.EliminarHoja("Concentrado Devoluciones");                        
                    ////    ExportarEntradas();
                    ////}

                    ////if (rdoDevoluciones.Checked)
                    ////{
                    ////    //xpExcel.EliminarHoja("Detallado ENTRADAS");
                    ////    //xpExcel.EliminarHoja("Concentrado Entradas");
                    ////    ExportarDevoluciones();
                    ////}

                    ////if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    ////{
                    ////    xpExcel.AbrirDocumentoGenerado();
                    ////}

                    IniciaToolBar(true, true);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarEntradas()
        {
            ////int iHoja = 1;
            ////string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            ////string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            ////string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            ////string sConcepto = "REPORTE DETALLADO DE RECEPCION DE ORDENES DE COMPRA";
            ////string sFechaImpresion = General.FechaSistemaFecha.ToString();
            
            ////xpExcel.GeneraExcel(iHoja);

            ////xpExcel.Agregar(sEmpresaNom, 2, 2);
            ////xpExcel.Agregar(sFarmaciaNom, 3, 2);
            ////xpExcel.Agregar(sConcepto, 4, 2);

            //////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            ////xpExcel.Agregar(sFechaImpresion, 6, 3);

            ////leerExportarExcel.RegistroActual = 1;

            ////for (int iRenglon = 9; leerExportarExcel.Leer(); iRenglon++)
            ////{
            ////    xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, 2);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, 3);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("FolioOrdenCompraReferencia"), iRenglon, 4);

            ////    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaDocto"), iRenglon, 5);
                
            ////    xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, 6);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, 7);



            ////    xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, 8);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, 9);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 10);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, 11);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, 12);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("DescripcionProducto"), iRenglon, 13);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, 14);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, 15);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("FechaCaducidad"), iRenglon, 16);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("CostoUnitario"), iRenglon, 17);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("CantidadLote"), iRenglon, 18);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, 19);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLote"), iRenglon, 20);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLote"), iRenglon, 21);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRenglon, 22);
            ////}

            ////// Finalizar el Proceso 
            ////xpExcel.CerrarDocumento();

            ////iHoja = 2;
            ////sConcepto = "REPORTE CONCENTRADO DE RECEPCION DE ORDENES DE COMPRA";

            ////xpExcel.GeneraExcel(iHoja);

            ////xpExcel.Agregar(sEmpresaNom, 2, 2);
            ////xpExcel.Agregar(sFarmaciaNom, 3, 2);
            ////xpExcel.Agregar(sConcepto, 4, 2);

            //////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            ////xpExcel.Agregar(sFechaImpresion, 6, 3);

            ////leerExportarExcel.DataTableClase = leerExportarExcel.Tabla(2);
            ////leerExportarExcel.RegistroActual = 1;

            ////for (int iRenglon = 9; leerExportarExcel.Leer();iRenglon++ )
            ////{
            ////    int iCol = 2;
            ////    xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("FolioOrdenCompraReferencia"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaDocto"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.CampoFecha("FechaDocto"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("SubTotal"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Iva"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("Total"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, iCol++);
            ////    xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, iCol++);
            ////}

            ////// Finalizar el Proceso 
            ////xpExcel.CerrarDocumento();

        }

        private void GenearExcel_XML_Detalles()
        {

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "REPORTE DE ENTRADAS DE ORDENES DE COMPRAS";
            string sNombreHoja = "CONCENTRADO";
            string sNombreHoja_Detallado = "DETALLES";
            string sConcepto = "REPORTE CONCENTRADO DE ENTRADAS DE ORDENES DE COMPRA";
            string sConcepto_Detalles = "REPORTE DETALLADO DE ENTRADAS DE ORDENES DE COMPRA";


            int iHoja = 1, iRenglon = 1;
            int iColBase = 2;
            int iColsEncabezado = 10;
            clsLeer leerDatos = new clsLeer();

            if (rdoDevoluciones.Checked)
            {
                sNombreDocumento = "REPORTE DE DEVOLUCIONES DE ORDENES DE COMPRAS";
                sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE ORDENES DE COMPRAS";
                sConcepto_Detalles = "REPORTE DETALLADO DE DEVOLUCIONES DE ORDENES DE COMPRA";
            }


            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;


            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                //// Encabezado 
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmaciaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                leerDatos.DataTableClase = leerExportarExcel.Tabla(1); 

                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerDatos.DataSetClase);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);
                //// Encabezado 


                //// Detalles                
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja_Detallado);
                excel.EscribirCeldaEncabezado(sNombreHoja_Detallado, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja_Detallado, 3, iColBase, iColsEncabezado, 16, sFarmaciaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja_Detallado, 4, iColBase, iColsEncabezado, 14, sConcepto_Detalles);
                excel.EscribirCeldaEncabezado(sNombreHoja_Detallado, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                leerDatos.DataTableClase = leerExportarExcel.Tabla(2);

                excel.InsertarTabla(sNombreHoja_Detallado, iRenglon, iColBase, leerDatos.DataSetClase);
                excel.ArchivoExcel.Worksheet(sNombreHoja_Detallado).Columns().AdjustToContents(1, 65);
                //// Detalles  

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true); 
            }

            //xpExcel.GeneraExcel(iHoja);

            //xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            //xpExcel.Agregar(sConcepto, 4, 2);

            ////////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            //xpExcel.Agregar(sFechaImpresion, 6, 3);

            //leerExportarExcel.RegistroActual = 1;

            //while (leerExportarExcel.Leer())
            //{
            //    xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, 2);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, 3);
            //    xpExcel.Agregar(leerExportarExcel.Campo("FolioOrdenCompra"), iRenglon, 4);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaFolioOrdenCompra"), iRenglon, 5);

            //    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaDocto"), iRenglon, 6);

            //    xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, 7);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, 8);
            //    xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, 9);
            //    xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, 10);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 11);
            //    xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, 12);
            //    xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, 13);
            //    xpExcel.Agregar(leerExportarExcel.Campo("DescripcionProducto"), iRenglon, 14);
            //    xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, 15);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, 16);
            //    xpExcel.Agregar(leerExportarExcel.Campo("FechaCaducidad"), iRenglon, 17);
            //    xpExcel.Agregar(leerExportarExcel.Campo("PrecioCosto_Unitario"), iRenglon, 18);
            //    xpExcel.Agregar(leerExportarExcel.Campo("CantidadLote"), iRenglon, 19);
            //    xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, 20);
            //    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLote"), iRenglon, 21);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLote"), iRenglon, 22);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRenglon, 23);
            //    iRenglon++;
            //}

            //// Finalizar el Proceso 
            //xpExcel.CerrarDocumento();

            //iHoja = 2;
            //sConcepto = "REPORTE CONCENTRADO DE DEVOLUCIONES DE ORDENES DE COMPRA";

            //xpExcel.GeneraExcel(iHoja);

            //xpExcel.Agregar(sEmpresaNom, 2, 2);
            //xpExcel.Agregar(sFarmaciaNom, 3, 2);
            //xpExcel.Agregar(sConcepto, 4, 2);

            ////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            //xpExcel.Agregar(sFechaImpresion, 6, 3);

            //leerExportarExcel.DataTableClase = leerExportarExcel.Tabla(2);
            //leerExportarExcel.RegistroActual = 1;

            //for (iRenglon = 9; leerExportarExcel.Leer(); iRenglon++)
            //{
            //    int iCol = 2;
            //    xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaFolioOrdenCompra"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaDocto"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.CampoFecha("FechaDocto"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("SubTotal"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Iva"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("Total"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, iCol++);
            //    xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, iCol++);
            //}

            //// Finalizar el Proceso 
            //xpExcel.CerrarDocumento();

        }
        #endregion Exportar_A_Excel

        private void lstResultado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
