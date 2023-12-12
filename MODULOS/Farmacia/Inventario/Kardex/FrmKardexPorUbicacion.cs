using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos; 

namespace Farmacia.Inventario.Kardex
{
    public partial class FrmKardexPorUbicacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        clsConsultas Consultas;
        clsAyudas Ayudas;

        string sIdEmpresa = DtGeneral.EmpresaConectada;
        string sIdEstado = DtGeneral.EstadoConectado;
        string sIdFarmacia = DtGeneral.FarmaciaConectada;

        clsDatosCliente DatosCliente;
        //clsExportarExcelPlantilla xpExcel;

        public FrmKardexPorUbicacion()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            Ayudas = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name, false);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmKardexPorUbicacion");
        }

        private void FrmKardexPorUbicacion_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            Fg.IniciaControles();
            txtPasillo.Focus();
        }

        #region Funciones
        private void CargaDatosPasillo()
        {

            //Se hace de esta manera para la ayuda.
            txtPasillo.Text = leer.Campo("IdPasillo");
            lblPasillo.Text = leer.Campo("DescripcionPasillo");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Rack ingresado se encuentra cancelado. Verifique");

                txtPasillo.Text = "";
                lblPasillo.Text = "";
                txtPasillo.Focus();
            }
        }

        private void CargaDatosEstante()
        {
            //Se hace de esta manera para la ayuda.
            txtEstante.Text = leer.Campo("IdEstante");
            lblEstante.Text = leer.Campo("DescripcionEstante");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Nivel ingresado se encuentra cancelado. Verifique");

                txtEstante.Text = "";
                lblEstante.Text = "";
                txtEstante.Focus();
            }
        }

        private void CargaDatosEntrepaño()
        {
            //Se hace de esta manera para la ayuda.
            txtEntrepaño.Text = leer.Campo("IdEntrepaño");
            lblEntrepaño.Text = leer.Campo("DescripcionEntrepaño");

            if (leer.Campo("Status") == "C")
            {
                General.msjUser("El Entrepaño ingresado se encuentra cancelado. Verifique");

                txtEntrepaño.Text = "";
                lblEntrepaño.Text = "";
                txtEntrepaño.Focus();
            }
        }

        #endregion Funciones

        #region Buscar Pasillo
        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosPasillo();
                }
                else
                {
                    txtPasillo.Text = "";
                    txtPasillo.Focus();
                }
            }
        }

        private void txtPasillo_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Pasillos(sIdEmpresa, sIdEstado, sIdFarmacia, "txtId_KeyDown");
                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosPasillo();
                }
            }
        }

        private void txtPasillo_TextChanged(object sender, EventArgs e)
        {
            lblPasillo.Text = "";
            txtEstante.Text = "";
            txtEntrepaño.Text = "";
        }

        #endregion Buscar Pasillo

        #region Buscar Estante
        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtEstante.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosEstante();
                }
                else
                {
                    txtEstante.Text = "";
                    txtEstante.Focus();
                }
            }
        }

        private void txtEstante_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estantes(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), "txtId_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosEstante();
                }
            }
        }

        private void txtEstante_TextChanged(object sender, EventArgs e)
        {
            lblEstante.Text = "";
            txtEntrepaño.Text = "";
        }
        #endregion Buscar Estante

        #region Buscar Entrepaño
        private void txtEntrepaño_Validating(object sender, CancelEventArgs e)
        {
            if (txtEntrepaño.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Pasillos_Estantes_Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), txtEntrepaño.Text.Trim(), "txtPasillo_Validating");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño();
                }
                else
                {
                    txtEntrepaño.Text = "";
                    txtEntrepaño.Focus();
                }

            }
        }

        private void txtEntrepaño_KeyDown(object sender, KeyEventArgs e)
        {
            // string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Entrepaños(sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), "txtId_KeyDown");
                if (leer.Leer())
                {
                    CargaDatosEntrepaño();
                }
            }
        }

        private void txtEntrepaño_TextChanged(object sender, EventArgs e)
        {
            lblEntrepaño.Text = "";
        }

        #endregion Buscar Entrepaño

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {

                string sSql = string.Format("Exec spp_Rpt_KardexPorUbicacion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " +
                        "@IdPasillo = {3}, @IdEstante = {4}, @IdEntrepaño = {5}, @FechaInicial = '{6}', @FechaFinal = '{7}'",
                        sIdEmpresa, sIdEstado, sIdFarmacia, txtPasillo.Text.Trim(), txtEstante.Text.Trim(), txtEntrepaño.Text.Trim(),
                        General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "btnExportarExcel_Click()");
                    General.msjError("Ocurrió un error al consultar la información..");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        General.msjAviso("No existe información para mostrar...");
                    }
                    else
                    {
                        GenerarExcel();
                    }
                }
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (lblPasillo.Text == "")
            {
                bRegresa = false;
                General.msjError("Rack Inválido, verifique.");
                txtPasillo.Focus();
            }

            if (bRegresa && lblEstante.Text == "")
            {
                bRegresa = false;
                General.msjError("Nivel Inválido, verifique.");
                txtEstante.Focus();
            }

            if (bRegresa && lblEntrepaño.Text == "")
            {
                bRegresa = false;
                General.msjError("Entrapaño Inválido, verifique.");
                txtEntrepaño.Focus(); 
            }

            return bRegresa;
        }

        private void GenerarExcel()
        {
            int iHoja = 1, iCol = 2;
            this.Cursor = Cursors.WaitCursor;

            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_Kardex_Ubicacion.xls";
            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Kardex_Ubicacion.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = true;
            //    //leer.DataSetClase = dtsExistencias;

            //    this.Cursor = Cursors.Default;
            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        this.Cursor = Cursors.WaitCursor;

            //        string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //        string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            //        string sFarmaciaNom = sIdFarmacia + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            string sConcepto = "REPORTE DE MOVIMIENTOS DE UBICACIÓN";
            //    string sFechaImpresion = General.FechaSistemaFecha.ToString();
            string sRack = "Rack : " + txtPasillo.Text + "-- " + lblPasillo.Text;
            string sNivel = "Nivel : " + txtEstante.Text + "-- " + lblEstante.Text;
            string sEntrapaño = "Entrepaño : " + txtEntrepaño.Text + "-- " + lblEntrepaño.Text;

            //    xpExcel.GeneraExcel(iHoja);

            //    xpExcel.Agregar(sEmpresaNom, 2, 2);
            //    xpExcel.Agregar(sFarmaciaNom, 3, 2);
            //    xpExcel.Agregar(sConcepto, 4, 2);
            //    xpExcel.Agregar("PERIODO DEL " + dtpFechaInicial.Text + " AL " + dtpFechaFinal.Text, 5, 2);
            //    xpExcel.Agregar(sRack, 6, 3);
            //    xpExcel.Agregar(sNivel, 7, 3);
            //    xpExcel.Agregar(sEntrapaño, 8, 3);

            //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            //    xpExcel.Agregar(sFechaImpresion, 10, 3);

            //    leer.RegistroActual = 1;

            //    for (int iRenglon = 13; leer.Leer(); iRenglon++)
            //    {
            //        iCol = 2;
            //        xpExcel.Agregar(leer.Campo("FolioMovtoInv"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("TipoMovto"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("TipoES"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("FechaRegistro"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("IdPersonalRegistra"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("Personal"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("Descripcion"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("DescripcionSal"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("ClaveLote"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("Cantidad"), iRenglon, iCol++);
            //        xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, iCol++);
            //    }

            //    // Finalizar el Proceso 
            //    xpExcel.CerrarDocumento();


            //    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //    {
            //        xpExcel.AbrirDocumentoGenerado();
            //    }

            //}

            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sPeriodo = "PERIODO DEL " + dtpFechaInicial.Text + " AL " + dtpFechaFinal.Text;
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));

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
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sConcepto);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sPeriodo);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColBase + 4, 16, sRack, XLAlignmentHorizontalValues.Left);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColBase + 4, 16, sNivel, XLAlignmentHorizontalValues.Left);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColBase + 4, 16, sEntrapaño, XLAlignmentHorizontalValues.Left);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);


                iRenglon++;
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

            this.Cursor = Cursors.Default;

        }
    }
}
