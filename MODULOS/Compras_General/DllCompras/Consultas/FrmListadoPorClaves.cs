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
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace DllCompras.Consultas
{
    public partial class FrmListadoPorClaves : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
        clsLeer leer;
        clsLeer myLlenaDatos;
        clsConsultas Consultas;
        clsAyudas ayuda;
        clsGrid myGrid;
        clsDatosCliente DatosCliente;        

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sJurisdiccion = DtGeneral.Jurisdiccion;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sFechaSistema = General.FechaYMD(General.FechaSistema, "-");
        string sFormato = "#,###,###,##0.###0";

        private enum Cols
        {
            Ninguna = 0,
            IdProveedor = 1, Proveedor = 2, Cantidad = 3, Importe = 4
        }

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        public FrmListadoPorClaves()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");

            ConexionLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            myLeer = new clsLeer(ref ConexionLocal);
            leer = new clsLeer();
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdOrdenCompras, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            grdOrdenCompras.EditModeReplace = true;
            myGrid.BackColorColsBlk = Color.White;

            leerExportarExcel = new clsLeer(ref ConexionLocal);

            Cargar_Empresas();
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
            if (txtClaveSSA.Text.Trim() == "")
            {
                General.msjAviso("No ha capturado la clave para realizar la consulta.. Verifique!!");
                txtClaveSSA.Focus();
            }
            else
            {
                CargarGrid();
            }
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            myGrid.Limpiar(false);
            lblImpteTotal.Text = "";

            //if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEdo.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador && DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
                {
                    cboEmpresas.Enabled = false;
                    cboEdo.Enabled = false;
                }
            }
            btnExportarExcel.Enabled = false;
            //txtClaveSSA.Focus();
        }

        private void CargarGrid() 
        {
            string sWhereFechas = "", sWhereOrigen = ""; ////sWhereClaveSSA = "";

            if (cboEmpresas.Data != "0")
            {
                sWhereOrigen = string.Format(" And E.IdEmpresa = '{0}' ", cboEmpresas.Data);
            }

            if (cboEdo.Data != "0")
            {
                sWhereOrigen += string.Format(" And E.IdEstado = '{0}' ", cboEdo.Data);
            }
            
            if (!chkTodasLasFechas.Checked)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), E.FechaRegistro, 120) Between '{0}' and '{1}' ",
                               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }

            ////if (txtClaveSSA.Text.Trim() != "")
            ////{
            ////    sWhereClaveSSA = string.Format(" And P.IdClaveSSA_Sal = '{0}'  ", lblIdClaveSSA.Text);
            ////}

            string sSql = string.Format(" Select E.IdProveedor, E.Proveedor, Sum(D.Cantidad) As CantidadTotal, Sum(D.Importe) As ImporteTotal " + 
	                " From vw_OrdenesCompras_Claves_Enc E (Nolock) " +
	                " Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock) " +
		            " On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioOrden ) " +
	                " Inner Join vw_Productos_CodigoEAN P (Nolock) On ( D.CodigoEAN = P.CodigoEAN )  " +
                    " Where 1 = 1 {0} And P.IdClaveSSA_Sal = '{1}'  {2}  " +
	                " Group By E.IdProveedor, E.Proveedor  Order By E.IdProveedor",
                    sWhereOrigen, lblIdClaveSSA.Text, sWhereFechas);

            myGrid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarGrid()");
                General.msjError("Ocurrió un error al obtener la lista de Ordenes de Compras.");
            }
            else
            {
                if (myLeer.Leer())
                {
                    myGrid.LlenarGrid(myLeer.DataSetClase);
                    CalcularTotalImporte();
                    btnExportarExcel.Enabled = true;
                }
                else
                {
                    General.msjAviso("No se encontro información con los criterios especificados, verifique.");
                }
            }
        }

        private void CalcularTotalImporte()
        {
            double dImpteTotal = 0;
            dImpteTotal = myGrid.TotalizarColumnaDou((int)Cols.Importe);
            lblImpteTotal.Text = dImpteTotal.ToString(sFormato);
        }
        #endregion Funciones

        #region Eventos_Grid
        private void grdOrdenCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool bFecha = false;
            string sIdProveedor = "", sIdClaveSSA = "", sFechaIni = "", sFechaFin = "";

            sIdProveedor = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdProveedor);
            sIdClaveSSA = lblIdClaveSSA.Text;
            sFechaIni = General.FechaYMD(dtpFechaInicial.Value, "-");
            sFechaFin = General.FechaYMD(dtpFechaFinal.Value, "-");

            if (!chkTodasLasFechas.Checked)
            {
                bFecha = true;
            }

            if (sIdProveedor.Trim() != "")
            {
                FrmDetallesCodigosEAN Detalles = new FrmDetallesCodigosEAN();
                Detalles.CargarPantalla(cboEmpresas.Data, cboEdo.Data, sIdProveedor, sIdClaveSSA, sFechaIni, sFechaFin, bFecha);
                
            }
        }
        #endregion Eventos_Grid               

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresas.Add("0", "<< Seleccione >>");

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (myLeer.Exec(sSql))
            {
                cboEmpresas.Clear();
                cboEmpresas.Add("0", "<< Todas >>");
                cboEmpresas.Add(myLeer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresas.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
        }

        private void Cargar_Estados()
        {
            string sSql = "", sEmpresa = "";

            sEmpresa = cboEmpresas.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEdo.Add("0", "<< Seleccione >>");

            sSql = string.Format("Select IdEstado, NombreEstado, ClaveRenapo, IdEmpresa From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa);
            if (myLeer.Exec(sSql))
            {
                cboEdo.Clear();
                cboEdo.Add("0", "<< Todos >>");
                cboEdo.Add(myLeer.DataSetClase, true, "IdEstado", "NombreEstado");
                cboEdo.SelectedIndex = 0;
            }
            else
            {
                Error.LogError(myLeer.MensajeError);
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }

        }
        #endregion Carga_Combos

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Estados();
        }

        #region ClaveSSA
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text != "")
            {
                myLeer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");

                if (myLeer.Leer())
                {
                    txtClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    lblIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcion.Text = myLeer.Campo("Descripcion");

                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");

                    txtClaveSSA.Enabled = false;                    
                }
            }
            else
            {                
                txtClaveSSA.Focus();
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown()");

                if (myLeer.Leer())
                {
                    txtClaveSSA.Text = myLeer.Campo("ClaveSSA");
                    lblIdClaveSSA.Text = myLeer.Campo("IdClaveSSA_Sal");
                    lblDescripcion.Text = myLeer.Campo("Descripcion");

                    lblPresentacion.Text = myLeer.Campo("Presentacion");
                    lblContPaquete.Text = myLeer.Campo("ContenidoPaquete");
                    txtClaveSSA.Enabled = false;
                }
                else
                {
                    txtClaveSSA.Focus();
                }
            }
        }
        #endregion ClaveSSA

        #region Boton_Exportar
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (Genera_Datos_Reporte())
            {
                //if (rdoConcentrado.Checked)
                //{
                //    GeneraReporteConcentrado();
                //}

                //if (rdoDetallado.Checked)
                //{
                //    GeneraReporteDetallado();
                //}
                GenerarReporteExcel();
            }
        }
        #endregion Boton_Exportar

        #region Reportes
        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

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

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresas.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
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

        //private void GeneraReporteConcentrado()
        //{
        //    int iRow = 2, iCol = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";
        //    int iHoja = 1;

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Listado_Claves_Proveedores_Concentrado" + ".xls";
        //    if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_Prov.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_Prov.xls", DatosCliente);
        //    }
        //    else
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_Prov_Central.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_Prov_Central.xls", DatosCliente);
        //    }

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;


        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.EliminarHoja("DETALLADO");

        //        xpExcel.GeneraExcel(iHoja);
        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();
        //        if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //        {
        //            xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //            iRow++;
        //            xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["NombreEstado"].ToString(), iRow, 2);
        //            iRow++;
        //        }
        //        else
        //        {
        //            iRow = 4;
        //        }

        //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;

        //        while (leerExportarExcel.Leer())
        //        {
        //            iCol = 2;
        //            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
        //            {
        //                xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, iCol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, iCol++);
        //            }
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdComprador"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Comprador"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Minimo"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Maximo"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Promedio"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Importe"), iRow, iCol++);

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

        //private void GeneraReporteDetallado()
        //{
        //    int iRow = 2, iCol = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";
        //    int iHoja = 1;

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Listado_Claves_Proveedores_Detallado" + ".xls";

        //    if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_Prov.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_Prov.xls", DatosCliente);
        //    }
        //    else
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_Prov_Central.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_Prov_Central.xls", DatosCliente);
        //    }

        //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //    xpExcel.AgregarMarcaDeTiempo = false;


        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.EliminarHoja("CONCENTRADO");

        //        xpExcel.GeneraExcel(iHoja);

        //        //Se pone el encabezado
        //        leerExportarExcel.RegistroActual = 1;
        //        leerExportarExcel.Leer();

        //        if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //        {
        //            xpExcel.Agregar(((DataRow)cboEmpresas.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //            iRow++;
        //            xpExcel.Agregar(((DataRow)cboEdo.ItemActual.Item)["NombreEstado"].ToString(), iRow, 2);
        //            iRow++;
        //        }
        //        else
        //        {
        //            iRow = 4;
        //        }

        //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
        //           General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
        //        xpExcel.Agregar(sPeriodo, iRow, 2);

        //        iRow = 6;
        //        xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);

        //        // Se ponen los detalles
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 9;

        //        while (leerExportarExcel.Leer())
        //        {
        //            iCol = 2;
        //            if (DtGeneral.Modulo_Compras_EnEjecucion == TipoModuloCompras.Central)
        //            {
        //                xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, iCol++);
        //                xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, iCol++);
        //            }
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdComprador"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Comprador"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Descripcion"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Minimo"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Maximo"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio_Promedio"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Precio"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Importe"), iRow, iCol++);

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
        #endregion Reportes

        #region Generar_Informacion_Reporte
        private bool Genera_Datos_Reporte()
        {
            bool bRegresa = false;
            string sSql = "";
            string sWhereOrigen = "", sWhereFechas = "";

            if (cboEmpresas.Data != "0")
            {
                sWhereOrigen = string.Format(" And E.IdEmpresa = '{0}' ", cboEmpresas.Data);
            }

            if (cboEdo.Data != "0")
            {
                sWhereOrigen += string.Format(" And E.IdEstado = '{0}' ", cboEdo.Data);
            }

            if (!chkTodasLasFechas.Checked)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }
            

            if (rdoConcentrado.Checked)
            {
                sSql = string.Format(" Select Empresa, Estado, IdPersonal as 'Núm. Comprador', NombrePersonal as Comprador, IdProveedor As 'Núm. Proveedor', Proveedor, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Clave', " +
                                    " MIN(PrecioUnitario) as Precio_Minimo, MAX(PrecioUnitario) as Precio_Maximo, AVG(PrecioUnitario) as Precio_Promedio, " +
                                    " SUM(Cantidad) AS Cantidad, cast((SUM(Importe)) as numeric(14, 4)) AS Importe " +
                                    " From vw_Impresion_OrdenesCompras_CodigosEAN E (Nolock) " +
                                    " Where 1 = 1 {0} And IdClaveSSA_Sal = '{1}'  {2}  " +
                                    " Group By Empresa, Estado, IdPersonal, NombrePersonal, IdProveedor, Proveedor, ClaveSSA, DescripcionSal " +
                                    " Order By Estado, ClaveSSA ",
                                    sWhereOrigen, lblIdClaveSSA.Text, sWhereFechas);
            }

            if (rdoDetallado.Checked)
            {
                sSql = string.Format(" Select Empresa, Estado, IdPersonal as 'Núm. Comprador', NombrePersonal as Comprador, IdProveedor As 'Núm. Proveedor', Proveedor, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Clave', CodigoEAN, Descripcion, " +
                                    " MIN(PrecioUnitario) as Precio_Minimo, MAX(PrecioUnitario) as Precio_Maximo, AVG(PrecioUnitario) as Precio_Promedio, " +
                                    " cast(PrecioUnitario as numeric(14, 4)) as Precio, SUM(Cantidad) AS Cantidad, " +
                                    " cast((SUM(Importe)) as numeric(14, 4)) AS Importe" +
                                    " From vw_Impresion_OrdenesCompras_CodigosEAN E (Nolock) " +
                                    " Where 1 = 1 {0} And IdClaveSSA_Sal = '{1}' {2} " +
                                    " Group By Empresa, Estado, IdPersonal, NombrePersonal, IdProveedor, Proveedor, ClaveSSA, DescripcionSal, CodigoEAN, Descripcion, PrecioUnitario " +
                                    " Order By Estado, ClaveSSA ",
                                    sWhereOrigen, lblIdClaveSSA.Text, sWhereFechas);
            }

            if (myLeer.Exec(sSql))
            {
                if (myLeer.Leer())
                {
                    bRegresa = true;
                    leerExportarExcel.DataSetClase = myLeer.DataSetClase;
                }
            }

            return bRegresa;
        }
        #endregion Generar_Informacion_Reporte
    }
}
