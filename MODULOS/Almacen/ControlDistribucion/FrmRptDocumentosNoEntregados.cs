using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.ControlDistribucion
{
    public partial class FrmRptDocumentosNoEntregados : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer Leer, LeerExcel;
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente datosCliente;
        DataSet dtDatosTransferencias, dtDatosVentas, dtCartas;

        public FrmRptDocumentosNoEntregados()
        {
            InitializeComponent();
            Leer = new clsLeer(ref cnn);
            LeerExcel = new clsLeer(ref cnn);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
        }

        private void FrmRptDocumentosNoEntregados_Load(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            dtDatosTransferencias = new DataSet();
            dtDatosVentas = new DataSet();
            dtCartas = new DataSet();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarDatos();
        }

        private void GenerarDatos()
        {
            string sSql = string.Format("Select Folio As 'Folio Ruta', Chofer, FechaRuta As 'Fecha Ruta', FolioTransferenciaVenta As 'Folio Transferencia', FarmaciaRecibe As 'Farmacia recibe',	Piezas,	Importe " +
                            "From vw_DevolucionDeDoctosDetTrans " +
                            "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FechaRuta Between '{3}' And '{4}' And FolioDevuelto = 'False' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFechaInicial.Value),
                            General.FechaYMD(dtpFechaFinal.Value));

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    dtDatosTransferencias = Leer.DataSetClase;
                }
            }
            else
            {
                Error.GrabarError(Leer, "GenerarDatos()");
                General.msjError("Ocurrió un error al leer la información de las transferencias.");
            }

            sSql = string.Format("Select Folio As 'Folio Ruta', Chofer, FechaRuta As 'Fecha Ruta', FolioTransferenciaVenta As 'Folio Venta', Beneficiario,	Piezas,	Importe From vw_DevolucionDeDoctosDetVentas " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FechaRuta Between '{3}' And '{4}' And FolioDevuelto = 'False'",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFechaInicial.Value),
                General.FechaYMD(dtpFechaFinal.Value));

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    dtDatosVentas = Leer.DataSetClase;
                }
            }
            else
            {
                Error.GrabarError(Leer, "GenerarDatos()");
                General.msjError("Ocurrió un error al leer la información de las ventas.");
            }

            sSql = string.Format("Select FolioRuta, Titulo_00 As Referencia, (Case When Tipo = 'T' Then 'TS' Else 'SV' End) + FolioTransferenciaVenta As 'Folio del movimiento', Sum(CantidadEnviada) As Piezas " +
                "From RutasDistribucionDet_CartasCanje " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And CartaDevuelta = 0 " +
                "Group By FolioRuta, Titulo_00, (Case When Tipo = 'T' Then 'TS' Else 'SV' End) + FolioTransferenciaVenta",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFechaInicial.Value),
                General.FechaYMD(dtpFechaFinal.Value));

            if (Leer.Exec(sSql))
            {
                if (Leer.Leer())
                {
                    dtCartas = Leer.DataSetClase;
                }
            }
            else
            {
                Error.GrabarError(Leer, "GenerarDatos()");
                General.msjError("Ocurrió un error al leer la información de las cartas de canje.");
            }

            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsLeer leer = new clsLeer();
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "Reporte de documentos de transferencias no entregadas";
            string sNombreFile = "Reporte de documentos de transferencias no entregadas";

            leer.DataSetClase = dtDatosTransferencias;
            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            //iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                leer.DataSetClase = dtDatosTransferencias;
                leer.RegistroActual = 1;

                sNombreHoja = "Transferencias";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                //generarExcel.CerraArchivo();
                
                leer.DataSetClase = dtDatosVentas;
                leer.RegistroActual = 1;
                iRenglon = 2;
                iColsEncabezado = iRenglon + leer.Columnas.Length - 1;

                sNombreHoja = "Ventas";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;
                
                leer.DataSetClase = dtCartas;
                leer.RegistroActual = 1;
                iRenglon = 2;
                iColsEncabezado = iRenglon + leer.Columnas.Length - 1;

                sNombreHoja = "Cartas Canje";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre);
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

        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\Plantillas\DOCUMENTOSNOENTREGADOS.xls";
        //    this.Cursor = Cursors.WaitCursor;
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "DOCUMENTOSNOENTREGADOS.xls", datosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        //dtsExistencias = new DataSet();
        //        //dtsExistencias.Tables.Add(dtExistencias);

        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {

        //            LlenarTransferencias();
        //            LlenarVentas();
        //            LlenarCartas();

        //            //xpExcel.CerrarDocumento();
        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }
        //}

        //private void LlenarTransferencias()
        //{
        //    int iHoja = 1;
        //    string sEncabezado = "Reporte de documentos de transferencias no entregadas";

        //    LeerExcel.DataSetClase = dtDatosTransferencias;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //    xpExcel.Agregar(DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    //sEncabezado = string.Format("Reporte de Existencia de Clave SSA: {0} ");//, lblClaveSSA.Text.Trim());
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 6, 2);
            
        //    for (int iRenglon = 9; LeerExcel.Leer(); iRenglon++)
        //    {
        //        int iCol = 2;
        //        xpExcel.Agregar(LeerExcel.Campo("Folio"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Chofer"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("FechaRuta"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("FolioTransferenciaVenta"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Farmaciarecibe"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Piezas"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Importe"), iRenglon, iCol++);
        //        //xpExcel.Agregar(LeerExcel.Campo("FechaRuta"), iRenglon, 2);
        //    }
        //    xpExcel.CerrarDocumento();
        //}

        //private void LlenarVentas()
        //{
        //    int iHoja = 2;
        //    string sEncabezado = "Reporte de documentos de ventas no entregadas";

        //    LeerExcel.DataSetClase = dtDatosVentas;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //    xpExcel.Agregar(DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    //sEncabezado = string.Format("Reporte de Existencia de Clave SSA: {0} ");//, lblClaveSSA.Text.Trim());
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 6, 2);

        //    for (int iRenglon = 9; LeerExcel.Leer(); iRenglon++)
        //    {
        //        int iCol = 2;
        //        xpExcel.Agregar(LeerExcel.Campo("Folio"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Chofer"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("FechaRuta"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("FolioTransferenciaVenta"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Beneficiario"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Piezas"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Importe"), iRenglon, iCol++);
        //        //xpExcel.Agregar(LeerExcel.Campo("FechaRuta"), iRenglon, 2);
        //    }
        //    xpExcel.CerrarDocumento();
        //}

        //private void LlenarCartas()
        //{
        //    int iHoja = 3;
        //    string sEncabezado = "Reporte de documentos de Cartas no entregadas";

        //    LeerExcel.DataSetClase = dtCartas;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //    xpExcel.Agregar(DtGeneral.EstadoConectadoNombre + ", " + DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    //sEncabezado = string.Format("Reporte de Existencia de Clave SSA: {0} ");//, lblClaveSSA.Text.Trim());
        //    xpExcel.Agregar(sEncabezado, 4, 2);

        //    xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 6, 2);

        //    for (int iRenglon = 9; LeerExcel.Leer(); iRenglon++)
        //    {
        //        int iCol = 2;
        //        xpExcel.Agregar(LeerExcel.Campo("FolioRuta"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Titulo_00"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("FolioMovto"), iRenglon, iCol++);
        //        xpExcel.Agregar(LeerExcel.Campo("Piezas"), iRenglon, iCol++);
        //    }
        //    xpExcel.CerrarDocumento();
        //}
    }
}
