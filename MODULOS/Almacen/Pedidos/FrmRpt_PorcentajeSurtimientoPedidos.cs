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

using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmRpt_PorcentajeSurtimientoPedidos : FrmBaseExt
    {
        enum Cols
        {
            IdJurisdiccion = 1, Jurisdiccion = 1, 
            IdFarmacia = 2, Farmacia = 3, FarmaciaSolicita = 4, Folio = 5, Surtimiento = 6, Fecha = 7, Status = 8, StatusDescripcion = 9  
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente; 
        clsLeer leer, leer2;
        clsConsultas query;
        clsListView lst;

        //clsExportarExcelPlantilla xpExcel;

        DataSet dtsFarmacias = new DataSet();

        clsDatosCliente DatosCliente;

        string sIdFarmacia = "";
        string sFarmacia = ""; 
        string sFolioPedido = "";

        public FrmRpt_PorcentajeSurtimientoPedidos()
        {
            InitializeComponent();

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRpt_PorcentajeSurtimientoPedidos");

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name); 
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");

            lst = new clsListView(listvwPedidos); 
        }

        private void FrmRpt_PorcentajeSurtimientoPedidos_Load(object sender, EventArgs e)
        {
            CargarJurisdicciones();
            CargarStatusPedidos(); 


            btnNuevo_Click(null, null);
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnExportarExcel.Enabled = false;
            Fg.IniciaControles();
            lst.LimpiarItems(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnExportarExcel.Enabled = false;
            CargarListaDePedidos(); 
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
        #endregion Eventos

        #region Funciones y Procedimientos Privados 
        private void CargarStatusPedidos()
        {
            cboStatusPedidos.Clear();
            cboStatusPedidos.Add("0", "Todo");
            cboStatusPedidos.Add("1", "Pendientes de surtir");
            cboStatusPedidos.Add("2", "En proceso de surtido"); 
        }

        private void CargarListaDePedidos()
        {
            string sSql = string.Format("Exec spp_Rpt_PorcentajeSurtimientoPedidos \n" +
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdJurisdiccion = '{2}', @IdFarmacia = '{3}', \n" + 
                "\t@FechaInicial = '{4}', @FechaFinal = '{5}', @StatusDePedido = '{6}', @Referencia = '{7}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboJurisdicciones.Data, cboFarmacias.Data, 
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), cboStatusPedidos.Data, txtReferenciaPedido.Text.Trim());

            lst.LimpiarItems(); 
            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer2, "CargarListaDePedidos()");
                General.msjError("Ocurrió un error al obtener la lista de los pedidos."); 
            }
            else
            {
                if (!leer2.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados."); 
                }
                else 
                {
                    btnExportarExcel.Enabled = true;
                    leer2.RenombrarTabla(1, "Concentrado");
                    leer2.RenombrarTabla(2, "Detallado");

                    leer.DataTableClase = leer2.Tabla("Concentrado");
                    lst.CargarDatos(leer.DataSetClase, true, false); 
                }
            }
        }
        #endregion Funciones y Procedimientos Privados 

        #region Menu 
        private void GetValores()
        {
            sIdFarmacia = lst.GetValue((int)Cols.IdFarmacia);
            sFarmacia = lst.GetValue((int)Cols.Farmacia);

            if (sFarmacia != lst.GetValue((int)Cols.FarmaciaSolicita))
            {
                sFarmacia += " -- " + lst.GetValue((int)Cols.FarmaciaSolicita);
            }

            sFolioPedido = lst.GetValue((int)Cols.Folio);  
        }

        //private void btnSurtirPedido_Click(object sender, EventArgs e)
        //{
        //    string sStatus = lst.GetValue((int)Cols.Status);
        //    GetValores(); 

        //    if (sStatus.ToUpper() == "F")
        //    {
        //        General.msjAviso("El pedido ya fue surtido por completo, no es posible generar un nuevo surtido.");
        //    }
        //    else
        //    {
        //        if (sFolioPedido != "")
        //        {
        //            FrmCEDIS_SurtidoPedidos f = new FrmCEDIS_SurtidoPedidos();
        //            if (f.CargarPedido(DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sIdFarmacia, sFolioPedido))
        //            {
        //                CargarListaDePedidos();
        //            }
        //        }
        //    }
        //}

        //private void btnListadoDeSurtidos_Click(object sender, EventArgs e)
        //{
        //    GetValores(); 

        //    if (sFolioPedido != "")
        //    {
        //        FrmListaDeSurtidosPedido f = new FrmListaDeSurtidosPedido(sIdFarmacia, sFarmacia, sFolioPedido);
        //        f.ShowDialog();

        //        CargarListaDePedidos(); 
        //    }
        //}

        //private void btnImprimirPedido_Click(object sender, EventArgs e)
        //{
        //    Imprimir(false); 
        //}

        //private void btnImprimirPedidoSurtido_Click(object sender, EventArgs e)
        //{
        //    Imprimir(true); 
        //}

        private void Imprimir(bool MostrarSurtido)
        {
            bool bRegresa = true;
            string sPrefijo = ""; 
            GetValores();

            if (sFolioPedido != "")
            {
                datosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.RutaReporte = DtGeneral.RutaReportes;

                myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS";
                if (MostrarSurtido)
                {
                    myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS__Surtido";
                    sPrefijo = "@";
                    myRpt.Add(sPrefijo + "FolioPedido", sFolioPedido);
                }
                else
                {
                    myRpt.Add("Folio", sFolioPedido); 
                }

                myRpt.Add(sPrefijo + "IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add(sPrefijo + "IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add(sPrefijo + "IdFarmacia", sIdFarmacia);



                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, datosCliente);
                // bRegresa = DtGeneral.ExportarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente, @"PRUEBA.pdf", FormatosExportacion.PortableDocFormat); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Menu 

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {

            //bEjecutando = true;

            //bEjecutando = false;

            string sAño = "", sNombre = "Reporte de pedidos en surtimiento", sNombreHoja = "Concentrado";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            //IniciarToolBar(true, true, true);

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            leer.RegistroActual = 1;
            ////xpExcel.MostrarAvanceProceso = true; 
            ////xpExcel.NumeroDeRenglonesAProcesar = leer.Registros > 0 ? leer.Registros : -1;


            //sNombre = cboReporte.SelectedItem.ToString();

            //sNombreHoja = sNombre = sNombre.Substring(0, 10);

            leer.DataTableClase = leer2.Tabla("Concentrado");

            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Concentrado";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                sNombreHoja = "Detallado";
                leer.DataTableClase = leer2.Tabla("Detallado");

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon = 8;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;


                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

            //BloquearControles(false);

            //MostrarEnProceso(false);

            Application.DoEvents();
        }

        //private void Excel()
        //{ 
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_PorcentajeSurtimientoPedidos.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_PorcentajeSurtimientoPedidos.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = false;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            xpExcel.GeneraExcel(1);

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Reporte de pedidos en surtimiento", 4, 2);

        //            xpExcel.Agregar(General.FechaSistemaObtener(), 6, 3);

        //            leer.DataTableClase = leer2.Tabla("Concentrado");
        //            leer.RegistroActual = 1;

        //            for (int iRow = 10; leer.Leer(); iRow++)
        //            {
        //                int Col = 2;
        //                xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("IdFarmacia"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Farmacia"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("FarmaciaSolicita"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("FolioPedido"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("ClavesSolicitadas"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("ClavesAsignadas"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("PorcentajeDeClaves"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("PiezasSolicitadas"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("PiezasAsignadas"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("PorcentajeDePiezas"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Status"), iRow, Col++);
        //            }

        //            xpExcel.CerrarDocumento();

        //            xpExcel.GeneraExcel(2);


        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Reporte de pedidos en surtimiento", 4, 2);

        //            xpExcel.Agregar(General.FechaSistemaObtener(), 6, 3);

        //            leer.DataTableClase = leer2.Tabla("Detallado");
        //            leer.RegistroActual = 1;

        //            for (int iRow = 9; leer.Leer(); iRow++)
        //            {
        //                int Col = 2;
        //                xpExcel.Agregar(leer.Campo("Jurisdiccion"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("IdFarmacia"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Farmacia"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("IdFarmaciaSolicita"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("FarmaciaSolicita"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("FolioPedido"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Presentacion"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("ContenidoPaquete"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Cantidad"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("CantidadAsignada"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Porcentaje"), iRow, Col++);
        //                xpExcel.Agregar(leer.Campo("Status"), iRow, Col++);
        //            }


        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalidaza, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }
        //}

    }
}
