using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

using ClosedXML.Excel;

namespace Almacen.Pedidos
{
    public partial class FrmTableroPedidosSurtido : FrmBaseExt
    {
        enum Cols
        {
            TipoDePedido = 2,
            IdJurisdiccion,
            Jurisdiccion,
            IdFarmacia, 
            Farmacia, 
            FolioPedido, 
            FechaPed, 
            FolioSurtido, 
            StatusPedido, 
            Status 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();

        //clsExportarExcelPlantilla xpExcel;
        DataSet dtsPedidos;

        bool bAjustarColumnas = true;

        public FrmTableroPedidosSurtido()
        {
            InitializeComponent();

            General.Pantalla.AjustarTamaño(this, 90, 80);


            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmTableroPedidosSurtido");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            CargarJurisdicciones();
            CargarStatusPedidos();
            CargarRutas();
        }

        private void FrmTableroPedidosSurtido_Load(object sender, EventArgs e)
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
            CargarListaPedidos();
        }
        #endregion Botones

        #region Funciones
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            btnExportarExcel.Enabled = false;

            cboJurisdicciones.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;
            cboStatusPed.SelectedIndex = 0;

            dtpFechaInicial.Value = dtpFechaInicial.Value.AddDays(-1);
            dtpFechaInicial_Entrega.Value = dtpFechaInicial_Entrega.Value.AddDays(-1);

            chkFechas.Checked = true;

            bAjustarColumnas = true;

            lst.LimpiarItems();
            cboJurisdicciones.Focus();
        }
        #endregion Funciones

        #region CargarCombos
        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "<< Todas las jurisdicciones >>");

                cboJurisdicciones.Add(Consultas.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion");
                dtsFarmacias = Consultas.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()");
            }

            cboJurisdicciones.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<< Todas las farmacias >>");
            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<< Todas las farmacias >>");
            string sFiltro = string.Format(" IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void CargarStatusPedidos()
        {
            cboStatusPed.Clear();
            cboStatusPed.Add("0", "TODOS"); 

            leer.DataSetClase = Consultas.PedidosSurtimiento_Status("CargarStatusPedidos()");
            if (leer.Leer())
            {
                cboStatusPed.Add(leer.DataSetClase, true, "ClaveStatus", "Descripcion");
            }

            //////cboStatusPed.Add("*", "TODOS");
            ////cboStatusPed.Add("A", "SURTIMIENTO");
            ////cboStatusPed.Add("S", "SURTIDO");
            ////cboStatusPed.Add("D", "DISTRIBUCION");
            ////cboStatusPed.Add("T", "TRANSITO");
            ////cboStatusPed.Add("R", "REGISTRADO");
            ////cboStatusPed.Add("C", "CANCELADO");

            ////cboStatusPed.SelectedIndex = 0;
        }

        private void CargarRutas()
        {
            cboRuta.Clear();
            cboRuta.Add("*", "<< Todas las Rutas >>");
            cboRuta.Add("0000", "<< Sin Ruta Asignada >>");

            cboRuta.Add(Consultas.Rutas(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, "CargarRutas()"), true, "IdRuta", "Descripcion");

            cboRuta.SelectedIndex = 0;

        }
        #endregion CargarCombos

        #region CargarPedidos
        private void CargarListaPedidos()
        {
            string sSql = "";
            string sJurisdiccion = "", sFarmaciaPed = "", sStatusPed = "";
            string sFiltroFecha_Registro = "";
            string sFiltroFecha_Entrega = "";

            btnExportarExcel.Enabled = false;

            if (cboJurisdicciones.Data != "*")
            {
                sJurisdiccion = string.Format(" and IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            if (cboFarmacias.Data != "*")
            {
                sFarmaciaPed = string.Format(" and IdFarmaciaPedido = '{0}' ", cboFarmacias.Data);
            }

            if (cboStatusPed.Data != "*")
            {
                sStatusPed = string.Format(" and Status = '{0}' ", cboStatusPed.Data);
            }

            if (chkFechas.Checked)
            {
                sFiltroFecha_Registro = string.Format(" and Convert(varchar(10), FechaRegistro, 120) Between '{0}' and '{1}' ", General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));
            }

            if (chkFiltro_FechaEntrega.Checked)
            {
                sFiltroFecha_Entrega = string.Format(" and Convert(varchar(10), FechaEntrega, 120) Between '{0}' and '{1}' ", General.FechaYMD(dtpFechaInicial_Entrega.Value), General.FechaYMD(dtpFechaFinal_Entrega.Value));
            }


            sSql = string.Format(
                "Select \n\t'Tipo de pedido' = TipoDePedido, 'Núm. Jurisdicción' = IdJurisdiccionPedido, 'Jurisdicción' = JurisdiccionPedido, \n" +
                "\t'Núm. Farmacia Pedido' = IdFarmaciaSolicita, 'Farmacia Pedido' = FarmaciaSolicita, 'Folio Pedido' = FolioPedido, \n" +
                "\t'Fecha Pedido' = Convert(varchar(10), FechaRegistro, 120), 'Fecha Entrega' = Convert(varchar(10), FechaEntrega, 120), \n" +
                "\t'Folio Surtido' = FolioSurtido, 'Referencia' = FolioTransferenciaReferencia,'Status Pedido' = StatusPedido, Status \n" +
                "From vw_PedidosCedis_Surtimiento (Nolock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' {2} {3} " +
                "\t {4} {5}  {6}  " +
                "Order By IdFarmacia, FolioPedido ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sJurisdiccion, sFarmaciaPed,
               sFiltroFecha_Registro, sFiltroFecha_Entrega, sStatusPed);

            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis___TableroPedidosSurtido " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdJurisdiccion = '{2}', @IdFarmacia = '{3}', " +
                " @Filtro_Fechas = {4}, @FechaInicial = '{5}', @FechaFinal = '{6}', @Filtro_Fechas_Entrega = {7}, @FechaInicial_Entrega = '{8}', @FechaFinal_Entrega = '{9}', " +
                " @StatusDePedido = '{10}',  @IdRuta = '{11}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, cboJurisdicciones.Data, cboFarmacias.Data,
                Convert.ToInt32(chkFechas.Checked), General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                Convert.ToInt32(chkFiltro_FechaEntrega.Checked), General.FechaYMD(dtpFechaInicial_Entrega.Value), General.FechaYMD(dtpFechaFinal_Entrega.Value),
                cboStatusPed.Data, cboRuta.Data);

            dtsPedidos = new DataSet();


            lst.Limpiar();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarListaPedidos()");
                General.msjError("Ocurrió un error al obtener la lista de los pedidos.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados.");
                }
                else
                {
                    dtsPedidos = leer.DataSetClase;
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    btnExportarExcel.Enabled = true;

                    bAjustarColumnas = false;
                }
            }
        }
        #endregion CargarPedidos

        #region Eventos_Combos
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboJurisdicciones.SelectedIndex != 0)
            {
                CargarFarmacias();
            }
        }
        #endregion Eventos_Combos

        #region Exportar_Excel
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ////FrmMonitorSurtimientoDePedidos f = new FrmMonitorSurtimientoDePedidos();
            ////f.ShowDialog(); 
            ExportarExcel();
        }

        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ALMN_Rpt_Tablero_StatusPedidosSurtido.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ALMN_Rpt_Tablero_StatusPedidosSurtido.xls", datosCliente);

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
        //            ExportarPedidos();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarPedidos()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.FarmaciaConectada + "--" + DtGeneral.FarmaciaConectadaNombre + ", " + DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = "Reporte de Status de Pedidos para Surtido";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    leer.DataSetClase = dtsPedidos;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("Tipo de Pedido"), iRenglon, (int)Cols.IdJurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Núm. Jurisdicción"), iRenglon, (int)Cols.IdJurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Jurisdicción"), iRenglon, (int)Cols.Jurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Núm. Farmacia Pedido"), iRenglon, (int)Cols.IdFarmacia);
        //        xpExcel.Agregar(leer.Campo("Farmacia Pedido"), iRenglon, (int)Cols.Farmacia);
        //        xpExcel.Agregar(leer.Campo("Folio Pedido"), iRenglon, (int)Cols.FolioPedido);                xpExcel.Agregar(leer.Campo("Fecha Pedido"), iRenglon, (int)Cols.FechaPed);
        //        xpExcel.Agregar(leer.Campo("Folio Surtido"), iRenglon, (int)Cols.FolioSurtido);
        //        xpExcel.Agregar(leer.Campo("Status Pedido"), iRenglon, (int)Cols.StatusPedido);
        //        xpExcel.Agregar(leer.Campo("Status"), iRenglon, (int)Cols.Status);
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}

        private void ExportarExcel()
        {
            string sAño = "", sNombre = "Reporte de Status de Pedidos para Surtido", sNombreHoja = "Hoja1";

            int iRow = 2, iColBase = 2, iColsEncabezado = 0, iRenglon = 0;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;

            clsGenerarExcel generarExcel = new clsGenerarExcel();

            leer.DataSetClase = dtsPedidos;

            leer.RegistroActual = 1;


            iColsEncabezado = iRow + leer.Columnas.Length - 1;
            iColsEncabezado = iRow + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = sNombre;
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombre))
            {
                sNombreHoja = "Hoja1";

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
        }
        #endregion Exportar_Excel
    }
}
