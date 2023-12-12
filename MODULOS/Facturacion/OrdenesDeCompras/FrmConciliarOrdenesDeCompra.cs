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
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;

namespace Facturacion.OrdenesDeCompras
{
    public partial class FrmConciliarOrdenesDeCompra : FrmBaseExt
    {
        // clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;        
        clsConsultas Consultas;
        clsAyudas Ayuda;

        clsListView lst;
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        // string sFormato = "#,###,##0.###0";
        // int iValor_0 = 0;

        string sUrl_Regional = ""; 
        clsDatosCliente DatosCliente;

        string sRutaPlantilla = "";

        // bool bEjecutando = false;
        // bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        Color colorNeutro = Color.Black;
        Color colorAFavor = Color.Blue;
        Color colorEnContra = Color.Red;

        public FrmConciliarOrdenesDeCompra()
        {            
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConciliarOrdenesDeCompra");            

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name); 

            ////this.Width = 830;
            ////this.Height = 500;

            lst = new clsListView(lstFoliosOC);
            lst.OrdenarColumnas = true;
            lst.PermitirAjusteDeColumnas = true;
            //lst.FormatoNumericos = true;
        }

        private void FrmConciliarOrdenesDeCompra_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            //btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            IniciaBotones(false, false, false);
            lst.Limpiar();
            lstFoliosOC.GridLines = false;
            CargarEstados();
            
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;


            if (!DtGeneral.EsAdministrador) 
            {
                cboEstados.Data = DtGeneral.EstadoConectado;
                cboEstados.Enabled = false; 
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargarConciliacion();
                DatosConciliacionExcel();                
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(false);
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            Imprimir(true);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarConciliacion();
        }
        #endregion Botones

        #region Cargar Combos

        private void CargarEstados()
        {            
            cboEstados.Clear();
            cboEstados.Add();

            cboFarmacias.Clear(); 
            cboFarmacias.Add();


            string sSql = ""; //  "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";

            sSql = " Select distinct E.IdEstado, E.NombreEstado as Estado, E.StatusEdo, U.UrlFarmacia as UrlRegional " +
                  " From vw_EmpresasEstados E (NoLock) " +
                  " Inner Join vw_Regionales_Urls U (NoLock) On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and U.IdFarmacia = '0001' ) " +
                  " Order By E.IdEstado ";

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEstados()");
                General.msjError("Ocurrió un error al obtener la lista de Estados.");
            }
            else
            {
                cboEstados.Add(leer.DataSetClase, true, "IdEstado", "Estado");
            }
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0; 
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();           

            sSqlFarmacias = string.Format(" Select Distinct F.IdFarmacia, (F.IdFarmacia + ' - ' + F.NombreFarmacia) as Farmacia, U.UrlFarmacia, C.Servidor " + 
                                " From CatFarmacias F (Nolock) " +
                                " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia ) " +
                                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) " +
                                " Where F.IdEstado = '{0}'  And F.EsAlmacen = 1  " +
                                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", cboEstados.Data);

            if (!leer.Exec(sSqlFarmacias)) 
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataSetClase, true, "IdFarmacia", "Farmacia");
                
            }
            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos 

        #region Funciones 
        private void CargaDatosProveedor()
        {
            //Se hace de esta manera para la ayuda. 

            if (leerLocal.Campo("Status").ToUpper() == "A")
            {
                txtProveedor.Text = leerLocal.Campo("IdProveedor");
                lblProveedor.Text = leerLocal.Campo("Nombre");
            }
            else
            {
                General.msjUser("El Proveedor " + leerLocal.Campo("Nombre") + " actualmente se encuentra cancelado, verifique. ");
                txtProveedor.Text = "";
                lblProveedor.Text = "";
                txtProveedor.Focus();
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el estado, verifique.");
                cboEstados.Focus();
            }

            if (bRegresa && cboFarmacias.SelectedIndex == 0) 
            {                
                bRegresa = false;
                General.msjAviso("No ha seleccionado el almacén, verifique.");
                cboFarmacias.Focus();
            }

            if (bRegresa && txtProveedor.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjAviso("No ha capturado un proveedor, verifique.");
                txtProveedor.Focus();
            }            

            return bRegresa;
        }

        private void CargarConciliacion()
        {
            string sSql = "", sWhereFormaPago = "";
            

            if (rdoCredito.Checked)
            {
                sWhereFormaPago = " And EsContado = 0 ";
            }

            if (rdoContado.Checked)
            {
                sWhereFormaPago = " And EsContado = 1 ";
            }

            if (rdoAmbos.Checked)
            {
                sWhereFormaPago = " And EsContado In(0, 1) ";
            }

            sSql = string.Format(" Select 'Folio' = Folio, 'Fecha Registro' = FechaRegistro, 'Total Orden Compra' = TotalOrdenCompra, " +
                        " 'Total Recibido' = TotalRecibido, 'Diferencia' = abs(Diferencia), 'Forma de Pago' = FormaPago, 'Status Diferencia' = StatusDiferencia  " +
	                    " From vw_FACT_Conciliacion_OC (Nolock) " +
	                    " Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdProveedor = '{2}' And EstadoEntrega = '{0}' And EntregarEn = '{3}' " + 
	                    " And FechaRegistro Between '{4}' And '{5}' {6} Order By Folio ",
                        cboEstados.Data, DtGeneral.FarmaciaConectada, 
                        Fg.PonCeros(txtProveedor.Text, 4), cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                        General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereFormaPago );

            lst.Limpiar();
            lstFoliosOC.GridLines = false;


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, sSql, "CargarConciliacion()", "");
                General.msjError("Ocurrió un Error al cargar la Conciliación..");
            }
            else
            {
                if (!leer.Leer())
                {
                    IniciaBotones(false, false, false);
                    General.msjUser("No se encontro información con los criterios especificados, verifique.");
                }
                else 
                {
                    //lstFoliosOC.View = View.Details;
                    lstFoliosOC.GridLines = true;
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    ////lst.PonerMascaraColumnas(lstFoliosOC, 3);
                    ////lst.PonerMascaraColumnas(lstFoliosOC, 4);
                    ////lst.PonerMascaraColumnas(lstFoliosOC, 5);
                    IniciaBotones(true, true, true);
                }
            }
        }

        private void DatosConciliacionExcel()
        {
            string sSql = "", sWhereFormaPago = "";
            int iCont = 1, iCargo = 0;
            double dValor = 0;

            if (rdoCredito.Checked)
            {
                sWhereFormaPago = " And EsContado = 0 ";
            }

            if (rdoContado.Checked)
            {
                sWhereFormaPago = " And EsContado = 1 ";
            }

            if (rdoAmbos.Checked)
            {
                sWhereFormaPago = " And EsContado In(0, 1) ";
            }

            sSql = string.Format(" Select * From vw_FACT_Conciliacion_OC (Nolock) " +
                        " Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdProveedor = '{2}' And EstadoEntrega = '{0}' And EntregarEn = '{3}' " +
                        " And FechaRegistro Between '{4}' And '{5}' {6} Order By Folio ",
                        cboEstados.Data, DtGeneral.FarmaciaConectada,
                        Fg.PonCeros(txtProveedor.Text, 4), cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value, "-"),
                        General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereFormaPago);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, sSql, "DatosConciliacionExcel()", "");
                General.msjError("Ocurrió un Error al obtener datos de Conciliación..");
            }
            else
            {
                leerExportarExcel.DataSetClase = leer.DataSetClase;
                while (leer.Leer())
                {
                    dValor = leer.CampoDouble("Diferencia");
                    iCargo = leer.CampoInt("Cargo");
                    if (dValor > 0)
                    {
                        ColorRenglonesTexto(iCont, Color.Red);
                    }
                    if (iCargo == 1)
                    {
                        ColorRenglonesTexto(iCont, Color.Blue);
                    }
                    
                    iCont++;
                }                
            }
        }

        private void IniciaBotones(bool bImprimir, bool bExportarPDF, bool bExportarExcel)
        {
            btnImprimir.Enabled = bImprimir;
            btnExportarPDF.Enabled = bExportarPDF;
            btnExportarExcel.Enabled = bExportarExcel;
        }

        private void ColorRenglonesTexto(int iItem, Color ColorCelda)
        {
            lst.ColorRowsTexto(lstFoliosOC, iItem, 5, ColorCelda);            
        }
        #endregion Funciones

        #region Eventos 
        private void txtProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtProveedor.Text.Trim() != "")
            {
                leerLocal.DataSetClase = Consultas.Proveedores(txtProveedor.Text.Trim(), "txtProveedor_Validating");
                if (leerLocal.Leer())
                {
                    CargaDatosProveedor();
                }
                else
                {
                    txtProveedor.Focus();
                }
            }
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFarmacias.Clear();
            cboFarmacias.Add();

            sUrl_Regional = ""; 
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;
                sUrl_Regional = cboEstados.ItemActual.GetItem("UrlRegional"); 
                CargarFarmacias();
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
            }
        }

        private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leerLocal.DataSetClase = Ayuda.Proveedores("txtProveedor_KeyDown");

                if (leerLocal.Leer())
                {
                    CargaDatosProveedor();
                }
            }
        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void grdCompras_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            
            //////FrmOrdenesDeComprasUnidad f = new FrmOrdenesDeComprasUnidad();
            //////f.MostrarFolioCompra(Grid.GetValue(Grid.ActiveRow, 1), cboEstados.Data, cboFarmacias.Data,
            //////    Grid.GetValue(Grid.ActiveRow, 4), sUrl_Regional); 
        } 
        #endregion Eventos          
        
        #region Impresion
        private void Imprimir(bool bExportarPDF)
        {
            bool bRegresa = true;
            int iContado = 0, iCredito = 0;

            if (rdoCredito.Checked)
            {
                iCredito = 0;
                iContado = 0;
            }

            if (rdoContado.Checked)
            {
                iCredito = 1;
                iContado = 1;
            }

            if (rdoAmbos.Checked)
            {
                iCredito = 0;
                iContado = 1;
            }

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            string sNombre = "CONCILIACION_OC_" + lblProveedor.Text + ".pdf";

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            //myRpt.Add("Folio", Fg.PonCeros(txtCotizacion.Text, 8));
            myRpt.NombreReporte = "FACT_ConciliacionOrdenesCompras";

            myRpt.Add("IdEstado", cboEstados.Data);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("IdProveedor", Fg.PonCeros(txtProveedor.Text, 4));
            myRpt.Add("EstadoEntrega", cboEstados.Data);
            myRpt.Add("EntregarEn", cboFarmacias.Data);
            myRpt.Add("FechaInicio", General.FechaYMD(dtpFechaInicial.Value, "-"));
            myRpt.Add("FechaFin", General.FechaYMD(dtpFechaFinal.Value, "-")); 
            myRpt.Add("EsCredito", iCredito);
            myRpt.Add("EsContado", iContado);

            if (bExportarPDF)
            {
                bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, sNombre, FormatosExportacion.PortableDocFormat);
            }
            else
            {

                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);
            }

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion

        #region ExportarExcel
        private void ExportarConciliacion()
        {
            int iRow = 2;
            string sNombreFile = "";
            string sPeriodo = "", sUnidad = "";
            string sRutaReportes = "";


            sRutaReportes = DtGeneral.RutaReportes;
            DtGeneral.RutaReportes = sRutaReportes;

            sNombreFile = "Rpt_Conciliacion_OrdenesCompras" + ".xls";
            sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_Conciliacion_OrdenesCompras.xls";
            DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Conciliacion_OrdenesCompras.xls", DatosCliente);

            xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            xpExcel.AgregarMarcaDeTiempo = false;

            if (xpExcel.PrepararPlantilla(sNombreFile))
            {
                xpExcel.GeneraExcel();

                //Se pone el encabezado
                leerExportarExcel.RegistroActual = 1;
                leerExportarExcel.Leer();
                xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
                iRow++;

                sUnidad = string.Format(" {0} -- {1}, {2} ", leerExportarExcel.Campo("EntregarEn"), leerExportarExcel.Campo("FarmaciaEntregarEn"),
                                                            leerExportarExcel.Campo("NombreEstadoEntrega"));
                xpExcel.Agregar(sUnidad, iRow, 2);
                iRow++;
                iRow++;

                sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
                   General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                xpExcel.Agregar(sPeriodo, iRow, 2);

                iRow = 8;                

                xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
                xpExcel.Agregar(General.FechaHora(DateTime.Now), iRow, 3);                

                // Se ponen los detalles
                leerExportarExcel.RegistroActual = 1;
                iRow = 11;

                while (leerExportarExcel.Leer())
                {
                    xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 2);
                    xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, 3);
                    xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, 4);
                    xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, 5);
                    xpExcel.Agregar(leerExportarExcel.Campo("FormaPago"), iRow, 6);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalSinGrabar"), iRow, 7);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalGrabado"), iRow, 8);
                    xpExcel.Agregar(leerExportarExcel.Campo("Iva"), iRow, 9);
                    xpExcel.Agregar(leerExportarExcel.Campo("TotalOrdenCompra"), iRow, 10);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalSinGrabarRecibido"), iRow, 11);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalGrabadoRecibido"), iRow, 12);
                    xpExcel.Agregar(leerExportarExcel.Campo("IvaRecibido"), iRow, 13);
                    xpExcel.Agregar(leerExportarExcel.Campo("TotalRecibido"), iRow, 14);
                    xpExcel.Agregar(leerExportarExcel.Campo("Diferencia"), iRow, 15);                    

                    iRow++;
                }

                // Finalizar el Proceso 
                xpExcel.CerrarDocumento();

                if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                {
                    xpExcel.AbrirDocumentoGenerado();
                }
                
            }
        }
        #endregion ExportarExcel

        ////private void lstFoliosOC_ItemActivate(object sender, EventArgs e)
        ////{
            
        ////}
    }
}
