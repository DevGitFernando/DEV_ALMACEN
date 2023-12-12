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

namespace DllCompras.OrdenesDeCompra
{
    public partial class FrmListadoOrdenesComprasCodigoEAN : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;
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
        double dImpteTotal = 0; 

        private enum Cols
        {
            Ninguna = 0,
            IdEmpresa = 1, Empresa, IdEstado, Estado, Folio, Proveedor, Observaciones, FechaRegistro, Status, FechaColocacion, 
            
            Importe 
        }

        string sRutaPlantilla = "";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        public FrmListadoOrdenesComprasCodigoEAN()
        {
            InitializeComponent();

            General.Pantalla.AjustarAncho(this, 70); 

            DatosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");

            ConexionLocal = new clsConexionSQL();
            ConexionLocal.DatosConexion = General.DatosConexion; 
            ConexionLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            ConexionLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
 

            myLeer = new clsLeer(ref ConexionLocal);
            myLlenaDatos = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            ayuda = new clsAyudas(General.DatosConexion, GnCompras.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnCompras.DatosApp, this.Name);

            myGrid = new clsGrid(ref grdOrdenCompras, this);
            myGrid.EstiloGrid(eModoGrid.ModoRow);
            myGrid.AjustarAnchoColumnasAutomatico = true; 
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
            CargarFoliosOrdenCompras();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            dImpteTotal = 0;
            Fg.IniciaControles(this, true);
            myGrid.Limpiar(false);

            //if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
            {
                cboEmpresa.Data = DtGeneral.EmpresaConectada;
                cboEstado.Data = DtGeneral.EstadoConectado;

                if (!DtGeneral.EsAdministrador && DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
                {
                    cboEmpresa.Enabled = false;
                    cboEstado.Enabled = false;
                }
            }

            lblImpteTotal.Text = dImpteTotal.ToString(sFormato);
            rdoColocada.Checked = false;
            rdoNoColocada.Checked = false;
            btnExportarExcel.Enabled = false;
            txtProveedor.Focus();
        }

        private void CargarFoliosOrdenCompras() 
        {
            string  sWhereOrigen = "", sWhereProv = "", sWhereFechas = "", sWherePer = "", sWhereStatus = "";

            if (cboEmpresa.Data != "0")
            {
                sWhereOrigen = string.Format(" And E.IdEmpresa = '{0}' ", cboEmpresa.Data);
            }

            if (cboEstado.Data != "0")
            {
                sWhereOrigen += string.Format(" And E.IdEstado = '{0}' ", cboEstado.Data);
            }

            if (txtProveedor.Text.Trim() != "")
            {
                sWhereProv = string.Format(" And E.IdProveedor = '{0}' ", txtProveedor.Text );
            }

            if (txtIdPersonal.Text.Trim() != "")
            {
                sWherePer = string.Format(" And E.IdPersonal = '{0}' ", txtIdPersonal.Text);
            }

            if (!chkTodasLasFechas.Checked)
            {
                sWhereFechas = string.Format(" And Convert(varchar(10), E.FechaRegistro, 120) Between '{0}' and '{1}' ",
                               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            }

            if (rdoColocada.Checked)
            {
                sWhereStatus = " And E.Status = 'OC' ";
            }

            if (rdoNoColocada.Checked)
            {
                sWhereStatus = " And E.Status = 'A' ";
            }

            if (rdoOtrosStatus.Checked)
            {
                sWhereStatus = " And E.Status Not In ( 'OC', 'A' ) "; 
            }
            
            string sSql = string.Format(
                "Select \n" +
                "   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.Folio, E.Proveedor, E.Observaciones, CONVERT(varchar(10),E.FechaRegistro,120) As FechaRegistro, \n" +
                "   (Case When E.Status = 'C' Then 'CANCELADA' When E.Status = 'OC' Then 'ORDEN COLOCADA' Else 'ORDEN NO COLOCADA' End) As StatusOrdenCompra, \n" +
                "   CONVERT(varchar(10),E.FechaColocacion,120) As FechaColocacion, \n" +
                "   round(sum(  (D.Cantidad * (D.Precio - ( D.Precio * (D.Descuento/100.00)))) * (1 + (D.TasaIva / 100.0)) ), 2) as ImporteTotal \n" +
                "From vw_OrdenesCompras_Claves_Enc E (Nolock) \n" +
                "Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock) \n" +
                "   On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioOrden ) \n" +
                "Where 1 = 1 {0} {1} {2} {3} {4} \n" +
                "Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.Folio, E.Proveedor, E.Observaciones, E.FechaRegistro, E.Status, E.FechaColocacion \n" +
                "Order By E.IdEstado, E.Folio \n", sWhereOrigen, sWhereProv, sWherePer, sWhereFechas, sWhereStatus);

            myGrid.Limpiar(false);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "CargarFoliosOrdenCompras()");
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
            string sFolioOrden = "", sIdEmpresa = "", sIdEstado = "";

            sFolioOrden = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.Folio);
            sIdEmpresa = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdEmpresa);
            sIdEstado = myGrid.GetValue(myGrid.ActiveRow, (int)Cols.IdEstado);

            if (sFolioOrden.Trim() != "")
            {
                FrmOrdenCompraCodigoEAN OrdenCompra = new FrmOrdenCompraCodigoEAN();
                OrdenCompra.MostrarDetalleOrdenCompra(sIdEmpresa, sIdEstado, sFolioOrden, true);
            }
        }
        #endregion Eventos_Grid

        #region Eventos_Proveedor
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                myLeer = new clsLeer(ref ConexionLocal);
                myLeer.DataSetClase = Consultas.Proveedores(txtProveedor.Text, "txtProveedor_Validating");
                if (myLeer.Leer())
                {
                    txtProveedor.Text = myLeer.Campo("IdProveedor");
                    lblNomProv.Text = myLeer.Campo("Nombre");
                }
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Proveedores("txtProveedor_KeyDown");
                if (myLeer.Leer())
                {
                    txtProveedor.Text = myLeer.Campo("IdProveedor");
                    lblNomProv.Text = myLeer.Campo("Nombre");
                }

            }
        }
        #endregion Eventos_Proveedor

        private void txtIdPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdPersonal.Text.Trim() != "")
            {
                myLeer = new clsLeer(ref ConexionLocal);
                myLeer.DataSetClase = Consultas.Personal(cboEstado.Data, DtGeneral.FarmaciaConectada, txtIdPersonal.Text, "txtIdPersonal_Validating");
                if (myLeer.Leer())
                {
                    txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                    lblNomPersonal.Text = myLeer.Campo("NombreCompleto");
                }
            }
        }

        private void txtIdPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                myLeer.DataSetClase = ayuda.Personal("txtIdPersonal_KeyDown", cboEstado.Data , DtGeneral.FarmaciaConectada );
                if(myLeer.Leer())
                {
                    txtIdPersonal.Text = myLeer.Campo("IdPersonal");
                    lblNomPersonal.Text = myLeer.Campo("NombreCompleto");
                }
            }
            
        }

        #region Carga_Combos
        private void Cargar_Empresas()
        {
            string sSql = "";

            cboEmpresa.Add("0", "<< Todas >>");

            sSql = "Select IdEmpresa, Nombre, EsDeConsignacion From CatEmpresas (NoLock) Where Status = 'A' Order by IdEmpresa ";
            if (myLeer.Exec(sSql))
            {
                cboEmpresa.Clear();
                //cboEmpresa.Add();
                cboEmpresa.Add("0", "<< Todas >>");
                cboEmpresa.Add(myLeer.DataSetClase, true, "IdEmpresa", "Nombre");
                cboEmpresa.SelectedIndex = 0;
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

            sEmpresa = cboEmpresa.Data;
            sEmpresa = Fg.PonCeros(sEmpresa, 3);
            cboEstado.Add("0", "<< Todos >>");

            sSql = string.Format(
            "Select IdEstado, (IdEstado + ' -- ' + NombreEstado) as NombreEstado, ClaveRenapo, IdEmpresa " + 
            "From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' AND StatusEdo = 'A' Order by IdEstado ", sEmpresa);
            if (myLeer.Exec(sSql))
            {
                cboEstado.Clear();
                //cboEstado.Add();
                cboEstado.Add("0", "<< Todos >>");
                cboEstado.Add(myLeer.DataSetClase, true, "IdEstado", "NombreEstado");
                cboEstado.SelectedIndex = 0;
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

        #region Exportar_Informacion
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
        #endregion Exportar_Informacion

        #region Reportes
        private void GenerarReporteExcel()
        {
            clsLeer leer = new clsLeer();
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombreFile = "Listado_Compras_de_Claves_Concentrado";

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

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, cboEmpresa.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEstado.Text);
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

        //private void GeneraReporteConcentrado()
        //{
        //    int iRow = 2, iCol = 2;
        //    string sNombreFile = "";
        //    string sPeriodo = "";
        //    string sRutaReportes = "";
        //    int iHoja = 1;
            
        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sNombreFile = "Listado_Compras_de_Claves_Concentrado" + ".xls";
        //    if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_OC_Prov.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_OC_Prov.xls", DatosCliente);
        //    }
        //    else
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_OC_Prov_Central.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_OC_Prov_Central.xls", DatosCliente);
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
        //            xpExcel.Agregar(((DataRow)cboEmpresa.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //            iRow++;
        //            xpExcel.Agregar(((DataRow)cboEstado.ItemActual.Item)["NombreEstado"].ToString(), iRow, 2);
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
        //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("StatusOrdenCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaColocacion"), iRow, iCol++);
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

        //    sNombreFile = "Listado_Compras_de_Claves_Detallado" + ".xls";

        //    if (DtGeneral.Modulo_Compras_EnEjecucion != TipoModuloCompras.Central)
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_OC_Prov.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_OC_Prov.xls", DatosCliente);
        //    }
        //    else
        //    {
        //        sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_OCEN_Listado_Claves_OC_Prov_Central.xls";
        //        DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Listado_Claves_OC_Prov_Central.xls", DatosCliente);
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
        //            xpExcel.Agregar(((DataRow)cboEmpresa.ItemActual.Item)["Nombre"].ToString(), iRow, 2);
        //            iRow++;
        //            xpExcel.Agregar(((DataRow)cboEstado.ItemActual.Item)["NombreEstado"].ToString(), iRow, 2);
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
        //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Observaciones"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("StatusOrdenCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaColocacion"), iRow, iCol++);
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
            string sWhereOrigen = "", sWhereProv = "", sWhereFechas = "", sWherePer = "", sWhereStatus = "";
            string sIdEmpresa = "*", sIdEstado = "*";
            int iTodasLasVentas = 1, iStatus = 0, iTipoReporte = 1;

            if (cboEmpresa.Data != "0")
            {
                //sWhereOrigen = string.Format(" And E.IdEmpresa = '{0}' ", cboEmpresa.Data);
                sIdEmpresa = cboEmpresa.Data;
            }

            if (cboEstado.Data != "0")
            {
                //sWhereOrigen += string.Format(" And E.IdEstado = '{0}' ", cboEstado.Data);
                sIdEstado = cboEstado.Data;
            }

            //if (txtProveedor.Text.Trim() != "")
            //{
            //    sWhereProv = string.Format(" And IdProveedor = '{0}' ", txtProveedor.Text );
            //}

            //if (txtIdPersonal.Text.Trim() != "")
            //{
            //    sWherePer = string.Format(" And IdPersonal = '{0}' ", txtIdPersonal.Text);
            //}

            if (!chkTodasLasFechas.Checked)
            {
                //sWhereFechas = string.Format(" And Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ",
                //               General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                iTodasLasVentas = 0;
            }

            if (rdoColocada.Checked)
            {
                //sWhereStatus = " And Status = 'OC' ";
                iStatus = 1;
            }

            if (rdoNoColocada.Checked)
            {
                //sWhereStatus = " And Status = 'A' ";
                iStatus = 2;
            }

            if (rdoOtrosStatus.Checked)
            {
                //sWhereStatus = " And Status Not In ( 'OC', 'A' ) "; 
                iStatus = 3;
            }


            if (rdoConcentrado.Checked)
            {
                iTipoReporte = 0;
                //sSql = string.Format(" Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, IdPersonal as IdComprador, NombrePersonal as Comprador, Folio, IdProveedor, Proveedor, " +
                //                    " convert(varchar(10), FechaRegistro, 120) as FechaRegistro, " +
                //                    " Case When Status = 'C' Then 'CANCELADA' When Status = 'OC' Then 'ORDEN COLOCADA' " +
                //                        " Else 'ORDEN NO COLOCADA' End As StatusOrdenCompra, " +
                //                    " convert(varchar(10), FechaColocacion, 120) as FechaColocacion, " +
                //                    " ClaveSSA, DescripcionSal, " +
                //                    " MIN(PrecioUnitario) as Precio_Minimo, MAX(PrecioUnitario) as Precio_Maximo, AVG(PrecioUnitario) as Precio_Promedio, " +
                //                    " SUM(Cantidad) AS Cantidad, cast((SUM(Importe)) as numeric(14, 4)) AS Importe " +
                //                    " From vw_Impresion_OrdenesCompras_CodigosEAN E (Nolock) " +
                //                    " Where 1 = 1 {0} {1}  {2}  {3} {4}  " +
                //                    " Group By E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, IdPersonal, NombrePersonal, Folio, IdProveedor, Proveedor, FechaRegistro, Status, FechaColocacion, ClaveSSA, DescripcionSal " +
                //                    " Order By E.IdEstado, E.Folio ", sWhereOrigen, sWhereProv, sWherePer, sWhereFechas, sWhereStatus);

            }

            if (rdoDetallado.Checked)
            {
                iTipoReporte = 1;
                //sSql = string.Format(" Select IdPersonal as IdComprador, NombrePersonal as Comprador, Folio, IdProveedor, Proveedor, " +
                //                    " convert(varchar(10), FechaRegistro, 120) as FechaRegistro, " +
                //                    " Case When Status = 'C' Then 'CANCELADA' When Status = 'OC' Then 'ORDEN COLOCADA' " +
                //                        " Else 'ORDEN NO COLOCADA' End As StatusOrdenCompra, " +
                //                    " convert(varchar(10), FechaColocacion, 120) as FechaColocacion, " +
                //                    " ClaveSSA, DescripcionSal, CodigoEAN, Descripcion, " +
                //                    " MIN(PrecioUnitario) as Precio_Minimo, MAX(PrecioUnitario) as Precio_Maximo, AVG(PrecioUnitario) as Precio_Promedio, " +
                //                    " cast(PrecioUnitario as numeric(14, 4)) as Precio, SUM(Cantidad) AS Cantidad, " +
                //                    " cast((SUM(Importe)) as numeric(14, 4)) AS Importe" +
                //                    " From vw_Impresion_OrdenesCompras_CodigosEAN (Nolock) " +
                //                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' {3}  {4}  {5} {6}  " +
                //                    " Group By IdPersonal, NombrePersonal, Folio, IdProveedor, Proveedor, FechaRegistro, Status, FechaColocacion, ClaveSSA, DescripcionSal, CodigoEAN, Descripcion, PrecioUnitario " +
                //                    " Order By Folio ",
                //                    cboEmpresa.Data, cboEstado.Data, sFarmacia, sWhereProv, sWherePer, sWhereFechas, sWhereStatus);
            }

            sSql = string.Format("Exec Spp_Rpt_ListadoDeOrdenDeCompra @IdEmpresa = '{0}', @IdEstado = '{1}', @IdProveedor = '{2}', @IdPersonal = '{3}', " +
            "@TodasLasFechas = {4}, @FechaInicial = '{5}', @FechaFinal = '{6}', @iStatus = '{7}', @sTipoReporte  = {8}",
            sIdEmpresa, sIdEstado, txtProveedor.Text.Trim(), txtIdPersonal.Text.Trim(),
            iTodasLasVentas, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"), iStatus, iTipoReporte);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "Genera_Datos_Reporte");
                General.msjError("Ocurrió un error al obtener la información para exportar a excel."); 
            }
            else 
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
