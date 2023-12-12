using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
////using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft.ExportarExcel; 

namespace Farmacia.Vales
{
    public partial class FrmInfomacionVales : FrmBaseExt
    {
        #region Enums
        private enum Cols_Emitidos
        {
            Ninguna = 0,
            FolioVale = 2, FolioVenta = 3, IdBeneficiario = 4, Beneficiario = 5, FolioReferencia = 6,
            FechaRegistro = 7, FechaCanje = 8, IdPersonal = 9, Personal = 10,
            IdCliente = 11, Cliente = 12, IdSubCliente = 13, SubCliente = 14,
            IdPrograma = 15, Programa = 16, IdSubPrograma = 17, SubPrograma = 18,
            EsSegundoVale = 19, Status = 20
        }

        private enum Cols_Registrados
        {
            Ninguna = 0,
            Folio = 1, FolioVale = 2, SubTotal = 3, Iva = 4, Total = 5, IdProveedor = 6, Proveedor = 7,
            IdClaveSSA_Sal = 8, ClaveSSA = 9, IdProducto = 10, CodigoEAN = 11, Producto = 12, Cantidad = 13,
            CostoUnitario = 14, SubTotal_Producto = 15, Iva_Producto = 16, Importe_Producto = 17
        }

        #endregion Enums

        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerColumnas, leerEmitidos;
        clsAyudas Ayuda;
        DataSet dtsCargarDatos = new DataSet();
        clsConsultas Consultas;
        clsDatosCliente DatosCliente;
        DataSet dtsEmitidos = new DataSet();
        DataSet dtsRegistrados = new DataSet();
        clsListView lstEmitidos, lstRegistrados;
        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();

        int iAñoReporte = 0;
        string sMesReporte = "";
        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        Color colorBack_01 = Color.LightBlue;
        Color colorBack_02 = Color.Lavender; 

        public FrmInfomacionVales()
        {
            InitializeComponent();
            ConexionLocal.SetConnectionString();

            leer = new clsLeer(ref ConexionLocal);
            leerColumnas = new clsLeer(ref ConexionLocal);
            leerEmitidos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            lstEmitidos = new clsListView(lstValesEmitidos);
            lstEmitidos.OrdenarColumnas = false;

            lstRegistrados = new clsListView(lstValesRegistrados);
            lstRegistrados.OrdenarColumnas = false;
            CargarReportes();
        }

        #region Botones
        private void FrmInfomacionVales_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(true);
            tabVales.SelectTab(0);

            lstEmitidos.Limpiar();
            lstRegistrados.Limpiar();
            dtsEmitidos = new DataSet();
            dtsRegistrados = new DataSet();
            // lblFarmacia.Text = DtGeneral.FarmaciaConectadaNombre;

            IniciaToolBar(true, true, false);
            ActivarControles(true);
            dtpFecha.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            int iMes = dtpFecha.Value.Month;
            int iAño = dtpFecha.Value.Year;

            iAñoReporte = iAño;
            sMesReporte = General.FechaNombreMes(dtpFecha.Value);

            if (ValidaControles())
            {
                ActivarControles(false);
                dtpFecha.Enabled = false;

                ObtenerInformacion(iAño, iMes);
            }
        }
        #endregion Botones

        #region Funciones y Eventos
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = Imprimir;
        }

        private void ObtenerInformacion(int iAño, int iMes)
        {
            DataSet dtsEmitidos_Consulta = new DataSet();
            DataSet dtsRegistrados_Consulta = new DataSet();
            DataTable dtEmitidos = new DataTable();
            DataTable dtRegistrados = new DataTable();

            string sSql = string.Format("Exec spp_Rpt_Vales_Por_Mes '{0}', '{1}', '{2}', '{3}', '{4}', 0 ",
                                        sIdEmpresa, sIdEstado, sIdFarmacia, iAño, iMes);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener el Concentrado de los vales.");
            }
            else
            {
                if (leer.Leer())
                {
                    IniciaToolBar(true, false, true);

                    //Se obtienen las tablas con los resultados  
                    dtsEmitidos.Tables.Add(leer.DataSetClase.Tables[0].Copy());//Tabla Emitidos
                    dtsRegistrados.Tables.Add(leer.DataSetClase.Tables[1].Copy());//Tabla Registrados

                    dtEmitidos = leer.DataSetClase.Tables[0].Copy();//Tabla Emitidos
                    dtRegistrados = leer.DataSetClase.Tables[1].Copy();//Tabla Registrados

                    //Se obtienen los Vales Emitidos del Mes.

                    dtEmitidos.Columns.Remove("IdEmpresa");
                    dtEmitidos.Columns.Remove("IdEstado");
                    dtEmitidos.Columns.Remove("IdFarmacia");
                    dtsEmitidos_Consulta.Tables.Add(dtEmitidos);
                    lstEmitidos.CargarDatos(dtsEmitidos_Consulta, true, true);
                    lstEmitidos.AlternarColorRenglones(colorBack_01, colorBack_02);

                    //Se obtienen los Vales Registrados por Farmacia
                    dtRegistrados.Columns.Remove("IdEmpresa");
                    dtRegistrados.Columns.Remove("IdEstado");
                    dtRegistrados.Columns.Remove("IdFarmacia");
                    dtsRegistrados_Consulta.Tables.Add(dtRegistrados);
                    lstRegistrados.CargarDatos(dtsRegistrados_Consulta, true, true);
                    lstRegistrados.AlternarColorRenglones(colorBack_01, colorBack_02);

                }
                else
                {
                    IniciaToolBar(true, true, false);
                    ActivarControles(true);
                    General.msjUser("No se encontro información para mostrar.");
                }
            }
        }

        private void ActivarControles(bool Activar)
        {
            dtpFecha.Enabled = Activar;
            cboReporte.Enabled = !Activar;
        }

        private bool ValidaControles()
        {
            bool bRegresa = true;

            return bRegresa;
        }

        #endregion Funciones y Eventos

        #region Impresion
        private void CargarReportes()
        {
            cboReporte.Clear();
            cboReporte.Add("0", "<< Seleccione >>");
            cboReporte.Add("1", "Vales Emitidos");
            cboReporte.Add("2", "Vales Registrados");
            cboReporte.Add("3", "Vales Registrados Detallado");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {
            bool bRegresa = false;
            int iMes = dtpFecha.Value.Month;
            int iAño = dtpFecha.Value.Year;
            string sReporte = "";
            int iOpcion = 0;

            if (cboReporte.Data == "0")
            {
                General.msjUser("Seleccione un Tipo de Impresión");
                cboReporte.Focus();
            }
            else
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                // clsReporteador Reporteador = new clsReporteador(ref myRpt, ref DatosCliente); 

                if (cboReporte.Data == "1")
                {
                    sReporte = "PtoVta_Vales_Emitidos_Mes.rpt";
                    iOpcion = 1;
                }
                else if (cboReporte.Data == "2")
                {
                    sReporte = "PtoVta_Vales_Registrados_Mes.rpt";
                    iOpcion = 2;
                }
                else
                {
                    sReporte = "PtoVta_Vales_Registrados_Mes_Detallado.rpt";
                    iOpcion = 3;
                }

                myRpt.RutaReporte = GnFarmacia.RutaReportes;
                myRpt.NombreReporte = sReporte;

                myRpt.Add("@IdEmpresa", sIdEmpresa);
                myRpt.Add("@IdEstado", sIdEstado);
                myRpt.Add("@IdFarmacia", sIdFarmacia);
                myRpt.Add("@iAño", iAño);
                myRpt.Add("@iMes", iMes);
                myRpt.Add("@iMostrarResultado", iOpcion);

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

                if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarExcelEmitidos();
        }

        private void GenerarExcelEmitidos()
        {
            bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Vales_Emitidos_Mes.xls", DatosCliente); 
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            string sPeriodo = "";
            int iMes = dtpFecha.Value.Month;
            int iAño = dtpFecha.Value.Year;

            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Vales_Emitidos_Mes.xls";

            string sSql = string.Format("Exec spp_Rpt_Vales_Emitidos_Por_Mes_Excel  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @iAño = '{3}', @iMes = '{4}' ",
                                sIdEmpresa, sIdEstado, sIdFarmacia, iAño, iMes);


            string sNombreDocumento = string.Format("ReporteDeVales");
            string sNombreHoja = "Emitidos";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            clsLeer datosExportar = new clsLeer(); 
            DataSet dtDatos = new DataSet(); 

            iAñoReporte = iAño;
            sMesReporte = General.FechaNombreMes(dtpFecha.Value);


            bRegresa = leerEmitidos.Exec(sSql); 
            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                //General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
                Error.GrabarError(leerEmitidos, "GenerarExcelEmitidos"); 
            }
            else
            {
                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;


                this.Cursor = Cursors.Default;

                if (leerEmitidos.Leer())
                {
                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        this.Cursor = Cursors.WaitCursor;


                        //////// Emitidos 
                        sNombreHoja = "Emitidos"; 
                        datosExportar.DataTableClase = leerEmitidos.Tabla(1);
                        sConcepto = string.Format("Reporte de Vales Emitidos del mes de {0} del {1} ", sMesReporte, iAñoReporte); 

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8; 
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Emitidos 


                        //////// Registrados - Emitidos 
                        sNombreHoja = "Registrados"; 
                        datosExportar.DataTableClase = leerEmitidos.Tabla(2);
                        datosExportar.DataSetClase = dtsRegistrados; 
                        sConcepto = string.Format("Reporte de Vales Registrados correspondientes a Vales Emitidos en el mes de {0} del {1} ", sMesReporte, iAñoReporte);

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                        //////// Registrados - Emitidos 


                        excel.CerraArchivo();

                        this.Cursor = Cursors.Default;

                        excel.AbrirDocumentoGenerado(true);
                    }
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void ExportarEmitidos()
        {


        }

        private void ExportarRegistrados()
        {
        }

        private void ExportarEmitidos__OLD()
        {
            //////int iHoja = 1, icol = 2 ;
            //////string sPeriodo = "";

            //////int iMes = dtpFecha.Value.Month;
            //////int iAño = dtpFecha.Value.Year;
            //////iAñoReporte = iAño;
            //////sMesReporte = General.FechaNombreMes(dtpFecha.Value);


            //////string sSql = string.Format("Exec spp_Rpt_Vales_Emitidos_Por_Mes_Excel  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @iAño = '{3}', @iMes = '{4}' ",
            //////                    sIdEmpresa, sIdEstado, sIdFarmacia, iAño, iMes);

            //////leerEmitidos.Exec(sSql);

            ////////leer.DataSetClase = dtsEmitidos;
            //////xpExcel.GeneraExcel(iHoja);

            //////xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);

            //////string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + DtGeneral.EstadoConectadoNombre;
            //////xpExcel.Agregar(sFarmacia , 3, 2);

            //////sPeriodo = string.Format("Reporte de Vales Emitidos del mes de {0} del {1} ", sMesReporte, iAñoReporte);
            //////xpExcel.Agregar(sPeriodo, 4, 2);

            //////xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            //////for (int iRenglon = 9; leerEmitidos.Leer(); iRenglon++ )
            //////{
            //////    icol = 2;
            //////    xpExcel.Agregar(leerEmitidos.Campo("NumReceta"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("FolioVale"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("FolioVenta"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("FechaRegistro"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("FechaReceta"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("IdBeneficiario"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("Beneficiario"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("IdMedico"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("Medico"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("ClaveSSA"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("DescripcionSal"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.CampoDouble("Cantidad"), iRenglon, icol++);
            //////    xpExcel.Agregar(leerEmitidos.Campo("PersonalRegistra"), iRenglon, icol++);


            //////    //xpExcel.Agregar(leerEmitidos.Campo("Cliente"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("IdSubCliente"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("SubCliente"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("IdPrograma"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("Programa"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("IdSubPrograma"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("SubPrograma"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("EsSegundoVale"), iRenglon, icol++);
            //////    //xpExcel.Agregar(leerEmitidos.Campo("Status"), iRenglon, icol++);
            //////}

            //////// Finalizar el Proceso 
            //////xpExcel.CerrarDocumento();
        }

        private void ExportarRegistrados__OLD()
        {
            //////int iHoja = 2, iRenglon = 9;
            //////string sPeriodo = "";

            //////leer.DataSetClase = dtsRegistrados;
            //////xpExcel.GeneraExcel(iHoja);

            //////xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //////xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, 3, 2);

            //////sPeriodo = string.Format("Reporte de Vales Registrados correspondientes a Vales Emitidos en el mes de {0} del {1} ", sMesReporte, iAñoReporte);
            //////xpExcel.Agregar(sPeriodo, 4, 2);

            ////////// xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            //////xpExcel.Agregar("Fecha de impresión : " + leer.CampoFecha("FechaImpresion").ToString(), 6, 2);

            //////while (leer.Leer())
            //////{
            //////    xpExcel.Agregar(leer.Campo("Folio"), iRenglon, (int)Cols_Registrados.Folio);
            //////    xpExcel.Agregar(leer.Campo("FolioVale"), iRenglon, (int)Cols_Registrados.FolioVale);
            //////    xpExcel.Agregar(leer.Campo("SubTotal"), iRenglon, (int)Cols_Registrados.SubTotal);
            //////    xpExcel.Agregar(leer.Campo("Iva"), iRenglon, (int)Cols_Registrados.Iva);
            //////    xpExcel.Agregar(leer.Campo("Total"), iRenglon, (int)Cols_Registrados.Total);
            //////    xpExcel.Agregar(leer.Campo("IdProveedor"), iRenglon, (int)Cols_Registrados.IdProveedor);
            //////    xpExcel.Agregar(leer.Campo("Proveedor"), iRenglon, (int)Cols_Registrados.Proveedor);

            //////    xpExcel.Agregar(leer.Campo("IdClaveSSA_Sal"), iRenglon, (int)Cols_Registrados.IdClaveSSA_Sal);
            //////    xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)Cols_Registrados.ClaveSSA);
            //////    xpExcel.Agregar(leer.Campo("IdProducto"), iRenglon, (int)Cols_Registrados.IdProducto);
            //////    xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, (int)Cols_Registrados.CodigoEAN);
            //////    xpExcel.Agregar(leer.Campo("Producto"), iRenglon, (int)Cols_Registrados.Producto);
            //////    xpExcel.Agregar(leer.Campo("Cantidad"), iRenglon, (int)Cols_Registrados.Cantidad);

            //////    xpExcel.Agregar(leer.Campo("CostoUnitario"), iRenglon, (int)Cols_Registrados.CostoUnitario);
            //////    xpExcel.Agregar(leer.Campo("SubTotal_Producto"), iRenglon, (int)Cols_Registrados.SubTotal_Producto);
            //////    xpExcel.Agregar(leer.Campo("Iva_Producto"), iRenglon, (int)Cols_Registrados.Iva_Producto);
            //////    xpExcel.Agregar(leer.Campo("Importe_Producto"), iRenglon, (int)Cols_Registrados.Importe_Producto);

            //////    iRenglon++;
            //////}

            //////// Finalizar el Proceso 
            //////xpExcel.CerrarDocumento();
        }

        #endregion Impresion
    }
}
