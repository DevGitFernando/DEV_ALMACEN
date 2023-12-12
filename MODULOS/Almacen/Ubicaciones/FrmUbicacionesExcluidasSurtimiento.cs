using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Almacen.Ubicaciones
{
    public partial class FrmUbicacionesExcluidasSurtimiento : FrmBaseExt
    {
        private enum Cols
        {
            IdEntrepano = 1, Descripcion = 2, EsDePickeo = 3,
            Excluida = 4, ExcluidasAux = 5, Bloqueo = 6, DescripcionAux = 7   
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid grid;

        //clsExportarExcelPlantilla xpExcel;  

        clsDatosCliente DatosCliente;
        // Almacen.wsCnnCliente conexionWeb;

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        // string sMensaje = "";

        public FrmUbicacionesExcluidasSurtimiento()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            // conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            // conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            grid = new clsGrid(ref grdBaldas, this);
            grdBaldas.EditModeReplace = true;
            grid.AjustarAnchoColumnasAutomatico = true; 

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
        }

        private void FrmAlmacenPasillosEstantesBaldas_Load(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool bRegresa = true;
            bool bInformacion = false;
            string sSql = "";
            //string sStatus = "";
            //int bEsDePicking = 0; 

            if (ValidaDatos())
            {
                if (cnn.Abrir())
                {
                    cnn.IniciarTransaccion();

                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        //sStatus = "C";
                        //if (grid.GetValueBool(i, (int)Cols.Excluida))
                        //{
                        //    sStatus = "A";
                        //}

                        //bEsDePicking = grid.GetValueBool(i, (int)Cols.EsDePickeo) ? 1: 0;

                        if (grid.GetValueBool(i, (int)Cols.Excluida) != grid.GetValueBool(i, (int)Cols.ExcluidasAux))
                        {
                            sSql = string.Format(" Exec spp_Mtto_Pedidos__Ubicaciones_Excluidas_Surtimiento \n" +
                                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', \n" +
                                "\t@IdPasillo = '{3}', @IdEstante = '{4}', @IdEntrepaño = '{5}', @Excluida = '{6}', @IdPersonal = '{7}' \n",
                                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text,
                                grid.GetValueInt(i, (int)Cols.IdEntrepano),  grid.GetValueInt(i, (int)Cols.Excluida), DtGeneral.IdPersonal);

                            ////if (grid.GetValueInt(i, (int)Cols.IdEntrepano) != 0)
                            {
                            ////    if (grid.GetValueBool(i, (int)Cols.Status) != grid.GetValueBool(i, (int)Cols.StatusAux) ||
                            ////        grid.GetValue(i, (int)Cols.Descripcion) != grid.GetValue(i, (int)Cols.DescripcionAux))
                                {
                                    if (!leer.Exec(sSql))
                                    {
                                        bRegresa = false;
                                        break;
                                    }
                                    if (grid.GetValueInt(i, (int)Cols.Excluida) == 1 && !bInformacion)
                                    {
                                        bInformacion = true;
                                    }
                                }
                            }
                        }
                    }

                    if (bRegresa)
                    {
                        cnn.CompletarTransaccion();
                        General.msjUser("Información guardada satisfactoriamente.");

                        if (bInformacion)
                        {
                            imprimir();
                        }
                        LimpiaPantalla();
                    }
                    else
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        cnn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información de Entrepaños.");
                    }

                    cnn.Cerrar();
                }
                else
                {
                    General.msjAviso(General.MsjErrorAbrirConexion);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (CargarDetalleExcel())
            {
                GenerarReporteExcel();
            }
        }

        #endregion Botones

        #region Funciones

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            grid.Limpiar(true);

            txtPasillo.Focus();
        }

        private void CargarPasillo_Estante_Entrepanos()
        {
            string sSql = string.Format(//" Exec spp_Mtto_Llenar_Pedidos__Ubicaciones_Excluidas_Surtimiento  " +
                "Select  C.IdEntrepaño, DescripcionEntrepaño,  EsDePickeo, IsNull(Excluida,0) As Excluida, IsNull(Excluida,0) as StatusAux, 1 as Bloqueo, DescripcionEntrepaño" +
                " From CatPasillos_Estantes_Entrepaños C (NoLock) " +
                "Left Join Pedidos__Ubicaciones_Excluidas_Surtimiento E (NoLock) On (C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia " +
		                    "And C.IdPasillo = E.IdPasillo And C.IdEstante = E.IdEstante And C.IdEntrepaño = E.IdEntrepaño) " +
                " Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}' And C.IdPasillo = '{3}' And C.IdEstante = '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPasillo.Text, txtEstante.Text );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarPasillo_Estante_Entrepanos()");
                General.msjError("Ocurrió un error al cargar la lista de Entrepaños."); 
            }
            else
            {
                grid.Limpiar(true);
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        grid.BloqueaCelda(true, i, (int)Cols.IdEntrepano);
                    }
                }
                
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtPasillo.Text.Trim() == "")
            {
                General.msjAviso("Favor de Capturar el Rack. Verifique...");
                bRegresa = false;
                txtPasillo.Focus();
            }

            if (bRegresa && txtEstante.Text.Trim() == "")
            {
                General.msjAviso("Favor de Capturar el Nivel. Verifique...");
                bRegresa = false;
                txtEstante.Focus();
            }

            if (bRegresa)
            {
                bRegresa = ValidarCapturaEntrepaños();
            }

            return bRegresa;
        }

        private bool ValidarCapturaEntrepaños()
        {
            bool bRegresa = true;

            if (grid.Rows == 0)
            {
                bRegresa = false;
            }
            else
            {
                if (grid.GetValue(1, (int)Cols.Descripcion) == "")
                {
                    bRegresa = false;
                }
                else
                {
                    for (int i = 1; i <= grid.Rows; i++)
                    {
                        if (grid.GetValue(i, (int)Cols.IdEntrepano) != "" && grid.GetValue(i, (int)Cols.Descripcion) == "")
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                }
            }

            if (!bRegresa)
            {
                General.msjUser("Debe capturar al menos un Entrepaño, Verifique.....");
            }

            return bRegresa;

        }

        private void imprimir()
        {
            bool bRegresa = true;
            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);

            myRpt.RutaReporte = DtGeneral.RutaReportes;

            myRpt.Add("IdEmpresa", sEmpresa);
            myRpt.Add("IdEstado", sEstado);
            myRpt.Add("IdFarmacia", sFarmacia);
            myRpt.Add("IdPasillo", txtPasillo.Text);
            myRpt.Add("IdEstante", txtEstante.Text);
            myRpt.NombreReporte = "PtoVta_Pedidos_Ubicaciones_Excluidas_Surtimiento";

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private bool CargarDetalleExcel()
        {
            bool bRegresa = true;

            string sSql = string.Format("Select Cast(IdPasillo As Varchar(10)) + ' -- ' + DescripcionPasillo As Rack, Cast(IdEstante As Varchar(10)) + ' -- ' + DescripcionEstante As Nivel, " +
                                "Cast(IdEntrepaño As Varchar(10)) + ' -- ' + DescripcionEntrepaño As Entrepaño, iif(EsDePickeo = 1, 'SI', 'No') As 'Es pickeo' " +
                                "From vw_Pedidos__Ubicaciones_Excluidas_Surtimiento " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = {3} And IdEstante = {4}"
                                ,sEmpresa, sEstado, sFarmacia, txtPasillo.Text, txtEstante.Text);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "CargarGrid");
                General.msjError("Ocurrió Un Error al buscar la Información.");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados, verifique.");
                    bRegresa = false;
                }
            }




            return bRegresa;
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "UBICACIONES EXCLUIDAS PARA EL SURTIMIENTO DE LOS PEDIDOS";
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

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
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + DtGeneral.EstadoConectadoNombre);
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

        //private void ExportarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\Plantillas\Rpt_Pedidos_Ubicaciones_Excluidas_Surtimiento.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_Pedidos_Ubicaciones_Excluidas_Surtimiento.xls", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ExportarDetalle();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    } 
        //}

        //private void ExportarDetalle()
        //{
        //    string Pickeo;
        //    DateTime dtpFecha = General.FechaSistema;
        //    int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
        //    int iHoja = 1, ColIni = 2;

        //    string sFarmaciaNombre = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + DtGeneral.EstadoConectadoNombre;
        //    string sFechaImpresion = General.FechaSistemaObtener().ToString();

        //    xpExcel.GeneraExcel(iHoja);



        //    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, ColIni);
        //    //xpExcel.Agregar(sEstado, 3, 1);
        //    xpExcel.Agregar(sFarmaciaNombre, 3, ColIni);
        //    xpExcel.Agregar(sFechaImpresion, 6, ColIni + 1);

        //    leer.RegistroActual = 1;

        //    for (int iRenglon = 9; leer.Leer(); iRenglon++)
        //    {
        //        int Col = ColIni;
        //        Pickeo = "NO";

        //        if (leer.CampoBool("EsDePickeo"))
        //        {
        //            Pickeo = "SI";
        //        }

        //        xpExcel.Agregar(leer.Campo("IdPasillo") + " -- " + leer.Campo("DescripcionPasillo"), iRenglon, Col++);
        //        xpExcel.Agregar(leer.Campo("IdEstante") + " -- " + leer.Campo("DescripcionEstante"), iRenglon, Col++);
        //        xpExcel.Agregar(leer.Campo("IdEntrepaño") + " -- " + leer.Campo("DescripcionEntrepaño"), iRenglon, Col++);
        //        xpExcel.Agregar(Pickeo, iRenglon, Col++);
        //    }
        //    xpExcel.CerrarDocumento();
        //}

        #endregion Funciones

        #region Eventos

        private void grdPasillos_Advance(object sender, FarPoint.Win.Spread.AdvanceEventArgs e)
        {
            if ((grid.ActiveRow == grid.Rows) && e.AdvanceNext)
            {
                if (grid.GetValueInt(grid.ActiveRow, (int)Cols.IdEntrepano) >= 0 && grid.GetValue(grid.ActiveRow, (int)Cols.Descripcion) != "")
                {
                    grid.Rows = grid.Rows + 1;
                    grid.ActiveRow = grid.Rows;
                    grid.SetValue(grid.Rows, (int)Cols.Excluida, 1); 
                    grid.SetActiveCell(grid.Rows, (int)Cols.IdEntrepano);
                }
            }
        }

        private void grdPasillos_EditModeOff(object sender, EventArgs e)
        {
            int iRow = grid.ActiveRow;
            string sIdEntrepano = grid.GetValueInt(iRow, (int)Cols.IdEntrepano).ToString();

            if (grid.BuscaRepetido(sIdEntrepano, iRow, (int)Cols.IdEntrepano, true))
            {
                General.msjUser("El Entrepaño ya se encuentra capturado en otro renglon, verifique.");
                grid.LimpiarRenglon(iRow);
                grid.SetActiveCell(iRow, (int)Cols.IdEntrepano);
                grid.EnviarARepetido(); 
            }
            else
            {
                if (grid.GetValue(iRow, (int)Cols.Descripcion) == "")
                {
                    grid.SetValue(iRow, (int)Cols.Descripcion, "ENTREPAÑO #" + sIdEntrepano);
                }
                else
                {
                    grid.SetActiveCell(grid.Rows, (int)Cols.Excluida);
                }                
            }
        }

        private void txtPasillo_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                string sSql = string.Format(" Select * From CatPasillos (Nolock) Where IdEmpresa = '{0}' And IdEstado = '{1}' " +
                                            " And IdFarmacia = '{2}' And IdPasillo = {3} ", sEmpresa, sEstado, sFarmacia, txtPasillo.Text);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtPasillo_Validating()");
                    General.msjError("Ocurrió un error al cargar el Rack.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtPasillo.Text = leer.Campo("IdPasillo");
                        lblPasillo.Text = leer.Campo("DescripcionPasillo");

                        txtEstante.Focus();
                    }
                    else
                    {
                        General.msjAviso("Clave de Rack No Encontrada, Verifique...");
                        txtPasillo.Focus();
                    }
                }
            }
            else
            {
                txtPasillo.Focus();
            }
        }

        private void txtEstante_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasillo.Text.Trim() != "")
            {
                if (txtEstante.Text.Trim() != "")
                {
                    string sSql = string.Format(" Select * From CatPasillos_Estantes (Nolock) Where IdEmpresa = '{0}' And IdEstado = '{1}' " +
                                                " And IdFarmacia = '{2}' And IdPasillo = {3} And IdEstante = '{4}' ", sEmpresa, sEstado, sFarmacia, 
                                                txtPasillo.Text, txtEstante.Text);

                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "txtEstante_Validating()");
                        General.msjError("Ocurrió un error al cargar el Nivel.");
                    }
                    else
                    {
                        if (leer.Leer())
                        {
                            txtEstante.Text = leer.Campo("IdEstante");
                            lblEstante.Text = leer.Campo("DescripcionEstante");

                            CargarPasillo_Estante_Entrepanos();
                        }
                        else
                        {
                            General.msjAviso("Clave de Nivel No Encontrada, Verifique...");
                            txtEstante.Focus();
                        }
                    }
                }
            }
            else
            {
                txtEstante.Focus();
            }
        }

        #endregion Eventos
    }
}
