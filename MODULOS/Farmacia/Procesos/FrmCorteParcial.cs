using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos; 
using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Procesos
{
    public partial class FrmCorteParcial : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer myLeer;        
        clsConsultas Consultas;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sPersonal = DtGeneral.IdPersonal;
        public bool bOpcionExterna = false; //Esta variable se utiliza para el cambio de cajero y corte del dia.
        public bool bCorteRealizado = false; //Esta variable se utiliza para el cambio de cajero y corte del dia.
        string sObservaciones = "";
        double fTotalCorteParcial = 0;
        string sFormato = "#,###,###,##0.###0";

        ////// Mensaje para el Corte 
        // string sMsjNoEncontrado = "Usted no puede realizar el Corte Parcial debido a que ya ha realizado su Corte Parcial o No ha efectuado ninguna venta.";


        // Manejo de reportes  
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        //clsExportarExcelPlantilla xpExcel; 

        string sFolioInv = "";
        string sMensajeInv = "";
        string sMensajeInv_Productos = ""; 

        public FrmCorteParcial()
        {
            InitializeComponent();
            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            myLeer = new clsLeer(ref ConexionLocal);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
        }

        private void FrmCorteParcial_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();

            // Bloquear el control hasta que se cargue la informacion completa 
            btnAceptar.Enabled = false;

            // Asignar fecha del sistema 
            dtpFechaSistema.Value = GnFarmacia.FechaOperacionSistema;
            dtpFechaSistema.Enabled = false;

            // ObtieneDatosCorte();

            //if (ValidaUsuarioCorte())
            //    ObtieneDatosCorte();
            //else
            //    btnAceptar.Enabled = false;

            tmCorte.Interval = 100; 
            tmCorte.Enabled = true;
            tmCorte.Start();
        }

        #region Funciones y Procedimientos Publicos 
        public bool CorteRealizado
        {
            get { return bCorteRealizado; }
        }

        public void GenerarCorteParcial_Externo(string IdPersonal)
        {
            sPersonal = IdPersonal;
            bOpcionExterna = true;
            this.ShowDialog(); 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Verificar si el Usuario puede hacer el corte
        private bool ValidaUsuarioCorte()
        {
            bool bRegresa = false, bMostrarVacio = true;
            myLeer = new clsLeer(ref ConexionLocal);

            if (bOpcionExterna)
            {
                bMostrarVacio = false;
            }

            Consultas.MostrarMsjSiLeerVacio = true; 
            myLeer.DataSetClase = Consultas.CortesParciales_Status(
                    sEmpresa, sEstado, sFarmacia, dtpFechaSistema.Text.ToString(), sPersonal, bMostrarVacio, "A", "ValidaUsuarioCorte");
            if (myLeer.Leer())
            {
                bRegresa = true; 
                //if (myLeer.Campo("Status").ToUpper() == "A")
                //{
                //    bRegresa = true;
                //}
                //else
                //{
                //    General.msjUser(sMsjNoEncontrado); 
                //}
            }
            else
            {
                //Si el Corte Parcial fue mandado llamar por la opcion Cambio de Cajero, 
                //al no encontrar ninguna A significa que ya fue hecho el corte por lo tanto puede continuar.
                if (bOpcionExterna)
                {
                    bCorteRealizado = true;
                    this.Hide();
                }
            }

            return bRegresa;
        }
        #endregion Verificar si el Usuario puede hacer el corte

        #region ObtenerDatos
        private void ObtieneDatosCorte()
        {
            myLeer = new clsLeer(ref ConexionLocal);
            fTotalCorteParcial = 0;

            myLeer.DataSetClase = Consultas.CortesParciales_Datos(sEmpresa, sEstado, sFarmacia, dtpFechaSistema.Text.ToString(), sPersonal, "ObtieneDatosCorte");
            if (myLeer.Leer())
            {
                CargaDatosCorte();
            }
            else
            {
                if (bOpcionExterna)
                {
                    this.Hide();
                }
                else
                {
                    this.Close();
                }
            }           

        }

        private void CargaDatosCorte()
        {
            //Se hace de esta manera para la ayuda.
            lblDotacionInicial.Text = myLeer.CampoDouble("DotacionInicial").ToString(sFormato);
            lblVentaDiaContado.Text = myLeer.CampoDouble("VentaDiaContado").ToString(sFormato);
            lblVentaDiaCredito.Text = myLeer.CampoDouble("VentaDiaCredito").ToString(sFormato);
            lblDevVentaDiaContado.Text = myLeer.CampoDouble("DevVentaDiaContado").ToString(sFormato);
            lblDevVentaDiaCredito.Text = myLeer.CampoDouble("DevVentaDiaCredito").ToString(sFormato);
            lblDevVentaDiaAntContado.Text = myLeer.CampoDouble("DevVentaDiaAntContado").ToString(sFormato);
            lblDevVentaDiaAntCredito.Text = myLeer.CampoDouble("DevVentaDiaAntCredito").ToString(sFormato);
            lblVentaTAContado.Text = myLeer.CampoInt("VentaTiempoAireContado").ToString();
            lblVentaTACredito.Text = myLeer.CampoInt("VentaTiempoAireCredito").ToString();
            lblTotalTA.Text = (myLeer.CampoInt("VentaTiempoAireContado") + myLeer.CampoInt("VentaTiempoAireCredito")).ToString(); 

            //lblTotalContado.Text = myLeer.Campo("TotalContado");
            //lblTotalCredito.Text = myLeer.Campo("TotalCredito");
            //lblTotalGeneral.Text = myLeer.Campo("TotalGeneral");
            //lblTotalTiempoAire.Text = myLeer.CampoInt("TotalTiempoAire").ToString();
            //lblTotalEfectivo.Text = myLeer.Campo("TotalEfectivo");
            fTotalCorteParcial = myLeer.CampoDouble("TotalCorteParcial");

            // Habilitar el Guardado de informacion 
            btnAceptar.Enabled = true;
        }
        #endregion ObtenerDatos

        #region Guardar Corte
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string sSql = "", sMensaje = "";
            int iOpcion = 2; // Se envia siempre la opcion 2 ya que la opcion 1 se utiliza cada vez que el usuario entra a las ventas.
            bool bContinua = false;

            if (validarSaldoDia())
            {
                if (!ConexionLocal.Abrir())
                {
                    Error.LogError(ConexionLocal.MensajeError); 
                    General.msjErrorAlAbrirConexion(); 
                }
                else
                {
                    ConexionLocal.IniciarTransaccion();

                    //NOTA: Se manda el valor del dtpFechaSistema porque aun no se ha definido la variable global que contendra la FechaSistema.
                    sSql = String.Format("Set Dateformat YMD Exec spp_Mtto_CtlCortesParciales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', " +
                                         "'{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}' ",
                        sEmpresa, sEstado, sFarmacia, sPersonal, dtpFechaSistema.Text, sPersonal,
                        lblDotacionInicial.Text.Replace(",", ""), lblVentaDiaContado.Text.Replace(",", ""),
                        lblVentaDiaCredito.Text.Replace(",", ""), lblDevVentaDiaContado.Text.Replace(",", ""),
                        lblDevVentaDiaCredito.Text.Replace(",", ""), lblDevVentaDiaAntContado.Text.Replace(",", ""),
                        lblDevVentaDiaAntCredito.Text.Replace(",", ""), lblVentaTAContado.Text.Replace(",", ""),
                        lblVentaTACredito.Text.Replace(",", ""), sObservaciones, iOpcion);

                    if (myLeer.Exec(sSql))
                    {
                        bContinua = true;
                        if (myLeer.Leer()) 
                        {
                            sMensaje = String.Format("{0}", myLeer.Campo("Mensaje"));
                        }

                        if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos)
                        {
                            //// Solo aplica para las farmacias 
                            if (!DtGeneral.EsAlmacen)
                            {
                                bContinua = GeneraInventarioAleatorio(1);
                            }
                        }

                        if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos_Productos)
                        {
                            //// Solo aplica para las farmacias 
                            if (!DtGeneral.EsAlmacen)
                            {
                                bContinua = GeneraInventarioAleatorio(2);
                            }
                        }

                        if (bContinua)
                        {
                            ConexionLocal.CompletarTransaccion();
                            General.msjUser(sMensaje); //Este mensaje lo genera el SP

                            if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos)
                            {
                                General.msjUser(sMensajeInv);// mensaje del folio de invetario aleatorio.
                            }

                            if (GnFarmacia.GeneraInventariosAleatoriosAutomaticos_Productos)
                            {
                                General.msjUser(sMensajeInv_Productos);// mensaje del folio de invetario aleatorio.
                            }

                            bCorteRealizado = true; //Esta variable se utiliza para el cambio de cajero y corte del dia.

                            // Imprimir Corte 
                            btnAceptar.Enabled = false;
                            if (!bOpcionExterna)
                            {
                                ImprimirCorteParcial();
                            }

                            if (GnFarmacia.GeneraReporteDispensacionPersonal)
                            {
                                ImprimirCorteParcialDetallado();
                            }

                            if (GnFarmacia.GeneraReporteDispensacionValesPersonal)
                            {
                                ImprimirCorteParcialDetallado_Vales();
                            }

                            if (bOpcionExterna)
                            {
                                this.Hide();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        bContinua = false;
                    }

                    if(!bContinua)
                    {
                        ConexionLocal.DeshacerTransaccion();
                        Error.GrabarError(myLeer, "btnGuardar_Click");
                        General.msjError("Ocurrió un error al guardar la información.");
                        //btnNuevo_Click(null, null);
                    }

                    ConexionLocal.Cerrar();
                }
            }
        }


        private void ImprimirCorteParcial()
        {


            DatosCliente.Funcion = "ImprimirCorteParcial()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CorteParcial.rpt";

            if (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia)
            {
                myRpt.NombreReporte = "PtoVta_CorteParcial__MEDIACCESS.rpt";
            }

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-") );
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            if (!bRegresa) 
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirCorteParcialDetallado_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 0, DtGeneral.IdPersonal);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ImprimirCorteParcialDetallado_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel del detallado de dispensación."); 
            }
            else
            {
                ImprimirCorteParcialDetallado_ExcelExportar(); 
            }
        }

        private void ImprimirCorteParcialDetallado_ExcelExportar()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            clsLeer leer = new clsLeer();

            leer.DataSetClase = myLeer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
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

        //private void ImprimirCorteParcialDetallado_ExcelExportar()
        //{
        //    //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    int iCol = 0; 
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar(DtGeneral.NombrePersonal, 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (myLeer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (myLeer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                }

        //                ////xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, 2);
        //                ////xpExcel.Agregar(myLeer.Campo("Folio"), iRow, 3); 
        //                ////xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, 5);
        //                ////xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, 6);
        //                ////xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, 7);
        //                ////xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, 8);
        //                ////xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, 9);
        //                ////xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, 10);
        //                ////xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, 11);
        //                ////xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, 12);
        //                ////xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, 13);
        //                ////xpExcel.Agregar(myLeer.Campo("Importe"), iRow, 14);
        //                ////xpExcel.Agregar(sStatus, iRow, 15);

        //                iCol = 2;
        //                xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Folio"), iRow, iCol++);
        //                iCol++;
        //                //xpExcel.Agregar(myLeer.Campo("Folio"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Medico"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, iCol++);
        //                xpExcel.Agregar(myLeer.Campo("Importe"), iRow, iCol++);
        //                xpExcel.Agregar(sStatus, iRow, iCol++);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        private void ImprimirCorteParcialDetallado()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 0); 
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Excel(); 
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios_Vales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 0, DtGeneral.IdPersonal);

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "ImprimirCorteParcialDetallado_Vales_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel de vales generados.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_ExcelExportar();
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            clsLeer leer = new clsLeer();

            leer.DataSetClase = myLeer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
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

        //private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        //{            //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar(DtGeneral.NombrePersonal, 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de emisión de vales del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (myLeer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (myLeer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                }

        //                xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, 2);
        //                xpExcel.Agregar(myLeer.Campo("FolioVenta"), iRow, 3);
        //                xpExcel.Agregar(myLeer.Campo("Folio"), iRow, 4);
        //                xpExcel.Agregar(myLeer.Campo("NumReceta"), iRow, 5);
        //                xpExcel.Agregar(myLeer.Campo("FechaReceta"), iRow, 6);
        //                xpExcel.Agregar(myLeer.Campo("FolioReferencia"), iRow, 7);
        //                xpExcel.Agregar(myLeer.Campo("Beneficiario"), iRow, 8);
        //                xpExcel.Agregar(myLeer.Campo("CodigoEAN"), iRow, 9);
        //                xpExcel.Agregar(myLeer.Campo("ClaveSSA"), iRow, 10);
        //                xpExcel.Agregar(myLeer.Campo("DescripcionCortaClave"), iRow, 11);
        //                xpExcel.Agregar(myLeer.Campo("Cantidad"), iRow, 12);
        //                xpExcel.Agregar(myLeer.Campo("PrecioLicitacion"), iRow, 13);
        //                xpExcel.Agregar(myLeer.Campo("Importe"), iRow, 14);
        //                xpExcel.Agregar(sStatus, iRow, 15);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        private void ImprimirCorteParcialDetallado_Vales()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado_Vales()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios_Vales.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 0);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_Excel(); 
            }
        }

        private bool validarSaldoDia()
        {
            bool bRegresa = true;

            if (DtGeneral.ManejaVentaPublico)
            {
                bRegresa = validarSaldoDia_VentaPublico();
            }

            return bRegresa;
        }

        private bool validarSaldoDia_VentaPublico() 
        {
            bool bRegresa = true;
            // string sSql = "", sMensaje = "", sFechaSistemaNueva = "";

            if ( fTotalCorteParcial <= 0.00)
            {
                General.msjUser("El saldo del corte es menor ó igual a cero, explique a que se debe.");

                clsObservaciones ob = new clsObservaciones();
                ob.Encabezado = "Observaciones de Corte Parcial";
                ob.MaxLength = 200;
                ob.Show();
                sObservaciones += "    " + ob.Observaciones;
                bRegresa = ob.Exito;

                if (!bRegresa)
                {
                    General.msjUser("No se capturaron las observaciones para el Corte Parcial, verifique.");
                }
            }            

            return bRegresa;
        } 
        #endregion Guardar Corte

        #region Revisar datos para Corte Parcial 
        private void tmCorte_Tick(object sender, EventArgs e)
        {
            tmCorte.Enabled = false;
            if (ValidaUsuarioCorte())
            {
                ObtieneDatosCorte();
            }
            else
            {
                this.Close();
            }
        }
        #endregion Revisar datos para Corte Parcial

        #region Genera_INV_Aleatorio
        private bool GeneraInventarioAleatorio(int Tipo)
        {
            bool bRegresa = true;
            string sSql = "";
            int iTipoInv = 0, iNumClaves = 0;

            iTipoInv = 1;
            iNumClaves = Tipo == 1 ? GnFarmacia.Claves_Inventario_Aleatorio__Encargado : GnFarmacia.Productos_Inventario_Aleatorio__Encargado;
            sSql = string.Format("Exec {0} ", Tipo == 1 ? " spp_INV_Aleatorios " : " spp_INV_Aleatorios_Productos ");

            sSql += String.Format("  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TipoInventario = '{3}', @Claves = '{4}', @IdPersonal = '{5}', @FechaSistema = '{6}' ",
                            sEmpresa, sEstado, sFarmacia, iTipoInv, iNumClaves, DtGeneral.IdPersonal, General.FechaYMD(GnFarmacia.FechaOperacionSistema, "-"));

            if (!myLeer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                if (myLeer.Leer())
                {
                    sFolioInv = myLeer.Campo("Folio");
                    
                    if ( Tipo == 1 ) sMensajeInv = myLeer.Campo("Mensaje");
                    if ( Tipo == 2 ) sMensajeInv_Productos = myLeer.Campo("Mensaje"); 
                }
            }

            return bRegresa;
        }
        #endregion Genera_INV_Aleatorio
    }
}
