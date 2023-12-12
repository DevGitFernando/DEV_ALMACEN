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

using ClosedXML.Excel;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_ComprasDirectas : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsConsultas consultas;
        clsAyudas ayuda;
        //clsGrid grid;
        clsListView lst;
        clsGenerarExcel excel;

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

        public FrmRptOP_ComprasDirectas()
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
            btnNuevo_Click_1(null, null); 
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

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click_1(object sender, EventArgs e)
        {
            if (rdoEntradas.Checked || rdoDevoluciones.Checked)
            {
                CargarDatos_OC();
            }
            else
            {
                General.msjAviso("Seleccione un tipo de reporte para poder continuar.");
            }
        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click_1(object sender, EventArgs e)
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
            string sSql = "", sWhereProveedor = "", sWhereStatus = " ";

            if (txtIdProveedor.Text.Trim() != "")
            {
                sWhereProveedor = string.Format(" and IdProveedor = '{0}' ", Fg.PonCeros(txtIdProveedor.Text, 4));
            }


            if (rdoDevoluciones.Checked)
            {
                sWhereStatus = "And Status = 'D'";
            }


                sSql = string.Format(" Select 'Fecha Entrada' = Convert(varchar(10), FechaRegistro, 120), 'Folio Compra directa' = Folio, " +
                                    " 'Referencia compra directa' = ReferenciaDocto, 'Núm. Proveedor' = IdProveedor, Proveedor " +
                                    " from vw_ComprasEnc " +
	                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                    " and Convert(varchar(10), FechaRegistro, 120) Between '{3}' and '{4}'  {5} {6} Order By FechaRegistro ",
                                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"), 
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor, sWhereStatus);

            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos_OC");
                General.msjError("Ocurrió un error al consultar los datos de las compras directas..");
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
            string sSql = "", sWhereProveedor = "", sWhereStatus = "";
            int iEsDevolucion = 0;
            leerExportarExcel = new clsLeer(ref cnn);

            if (txtIdProveedor.Text.Trim() != "")
            {
                sWhereProveedor = string.Format(" and IdProveedor = '{0}' ", Fg.PonCeros(txtIdProveedor.Text, 4));
            }

            if (rdoDevoluciones.Checked)
            {
                sWhereStatus = "And StatusCompra = 'D'";
            }
                sSql = string.Format(" Select * " +
                                    " From vw_ComprasDet_CodigosEAN_Lotes (Nolock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                                    " and Convert(varchar(10), FechaReg, 120) Between '{3}' and '{4}'  {5} {6} Order By FechaReg, Folio ",
                                    sEmpresa, sEstado, sFarmacia, General.FechaYMD(dtpFechaInicial.Value, "-"),
                                    General.FechaYMD(dtpFechaFinal.Value, "-"), sWhereProveedor, sWhereStatus);

                iEsDevolucion = rdoDevoluciones.Checked ? 1 : 0;

                sSql = string.Format("Exec spp_Rpt_OP_ComprasDirectas @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                    " @FechaIncial = '{3}', @FechaFinal = '{4}', @IdProveedor = '{5}',  @EsDevolucion = {6}",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"),
                    txtIdProveedor.Text.Trim(), iEsDevolucion);



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
        private void txtIdProveedor_Validating_1(object sender, CancelEventArgs e)
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

        private void txtIdProveedor_KeyDown_1(object sender, KeyEventArgs e)
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

        private void txtIdProveedor_TextChanged_1(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }
        #endregion Eventos        

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Exportar_A_Excel
        private void GenerarExcel()
        {
            int iHoja = 1, iRen = 2, iCol = 2, iColEnc = iCol + leerExportarExcel.Columnas.Length - 1;

            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "";
            string sNombreHoja = "Hoja1";

            if (rdoEntradas.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE RECEPCION DE COMPRAS DIRECTAS";
            }

            if (rdoDevoluciones.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE COMPRA DIRECTAS";
            }

            //string sFechaImpresion = General.FechaSistemaFecha.ToString();
            string sFechaImpresion = "Fecha de Impresión: " + General.FechaSistemaFecha.ToString();

            excel = new clsGenerarExcel();
            if(excel.PrepararPlantilla())
            {

                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sEmpresaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFarmaciaNom);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sConcepto);
                iRen++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRen++, iCol, iColEnc, sFechaImpresion, XLAlignmentHorizontalValues.Left);
                iRen++;
                excel.InsertarTabla(sNombreHoja, iRen, 2, leerExportarExcel.DataSetClase);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);

            }

            IniciaToolBar(true, true);       
        }

        private void Exportar()
        {
            int iHoja = 1, iRenglon = 9;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "";
            if (rdoEntradas.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE RECEPCION DE COMPRAS DIRECTAS";
            }

            if (rdoDevoluciones.Checked)
            {
                sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE COMPRA DIRECTAS";
            }

            string sFechaImpresion = General.FechaSistemaFecha.ToString();
            
            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 4, 2);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 6, 3);

            leerExportarExcel.RegistroActual = 1;


            while (leerExportarExcel.Leer())
            {
                int iCol = 2;
                xpExcel.Agregar(leerExportarExcel.Campo("FechaReg"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaDocto"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("DescProducto"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("FechaCad"), iRenglon, iCol++);
                xpExcel.Agregar(leerExportarExcel.Campo("CostoUnitario"), iRenglon, iCol++);

                if (rdoEntradas.Checked)
                {
                    xpExcel.Agregar(leerExportarExcel.Campo("CantidadRecibida"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLote"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLote"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRenglon, iCol++);
                }
                if (rdoDevoluciones.Checked)
                {
                    xpExcel.Agregar(leerExportarExcel.Campo("Cant_Devuelta"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLoteDevuelto"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLoteDevuelto"), iRenglon, iCol++);
                    xpExcel.Agregar(leerExportarExcel.Campo("ImporteLoteDevuelto"), iRenglon, iCol++);
                }

                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }

        private void ExportarDevoluciones()
        {
            int iHoja = 1, iRenglon = 9;
            string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            string sFarmaciaNom = sFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "REPORTE DETALLADO DE DEVOLUCIONES DE COMPRA DIRECTAS";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            xpExcel.GeneraExcel(iHoja);

            xpExcel.Agregar(sEmpresaNom, 2, 2);
            xpExcel.Agregar(sFarmaciaNom, 3, 2);
            xpExcel.Agregar(sConcepto, 4, 2);

            //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            xpExcel.Agregar(sFechaImpresion, 6, 3);

            leerExportarExcel.RegistroActual = 1;

            while (leerExportarExcel.Leer())
            {
                xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRenglon, 2);
                xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRenglon, 3);
                xpExcel.Agregar(leerExportarExcel.Campo("FolioOrdenCompra"), iRenglon, 4);
                xpExcel.Agregar(leerExportarExcel.Campo("ReferenciaFolioOrdenCompra"), iRenglon, 5);
                xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRenglon, 6);
                xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRenglon, 7);
                xpExcel.Agregar(leerExportarExcel.Campo("IdPersonal"), iRenglon, 8);
                xpExcel.Agregar(leerExportarExcel.Campo("NombrePersonal"), iRenglon, 9);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRenglon, 10);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionSal"), iRenglon, 11);
                xpExcel.Agregar(leerExportarExcel.Campo("IdProducto"), iRenglon, 12);
                xpExcel.Agregar(leerExportarExcel.Campo("DescripcionProducto"), iRenglon, 13);
                xpExcel.Agregar(leerExportarExcel.Campo("CodigoEAN"), iRenglon, 14);
                xpExcel.Agregar(leerExportarExcel.Campo("ClaveLote"), iRenglon, 15);
                xpExcel.Agregar(leerExportarExcel.Campo("FechaCaducidad"), iRenglon, 16);
                xpExcel.Agregar(leerExportarExcel.Campo("PrecioCosto_Unitario"), iRenglon, 17);
                xpExcel.Agregar(leerExportarExcel.Campo("CantidadLote"), iRenglon, 18);
                xpExcel.Agregar(leerExportarExcel.Campo("TasaIva"), iRenglon, 19);
                xpExcel.Agregar(leerExportarExcel.Campo("SubTotalLote"), iRenglon, 20);
                xpExcel.Agregar(leerExportarExcel.Campo("ImpteIvaLote"), iRenglon, 21);
                xpExcel.Agregar(leerExportarExcel.Campo("ImporteLote"), iRenglon, 22);
                iRenglon++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();

        }
        #endregion Exportar_A_Excel

        //private void btnNuevo_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void btnExportarExcel_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void btnImprimir_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void btnEjecutar_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void txtIdProveedor_KeyDown_1(object sender, KeyEventArgs e)
        //{

        //}

        //private void txtIdProveedor_Validating_1(object sender, CancelEventArgs e)
        //{

        //}

        //private void txtIdProveedor_TextChanged_1(object sender, EventArgs e)
        //{

        //}
    }
}
