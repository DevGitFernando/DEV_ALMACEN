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
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel; 

namespace Almacen.Ubicaciones
{
    public partial class FrmRptUbicacionesClaves : FrmBaseExt
    {
        private enum Cols
        {
            IdClaveSSA = 1, ClaveSSA = 2, DescripcionClave = 3, Pasillo = 4, PasilloNombre = 5, Estante = 6, EstanteNombre = 7,
            Entrepano = 8, EntrepanoNombre = 9, Existencia = 10
        }

        private enum ColsExportar
        {
            ClaveSSA = 2, Descripcion = 3, CodigoEAN = 4, Lote = 5, Caducidad = 6, 
            Pasillo = 7, Estante = 8, Entrepaño = 9, EsDePickeo = 10, 
            Existencia = 11, ExistenciaEnTransito = 12, ExistenciaGeneral = 13 

        }


        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        DataSet dtsDetalle;
        clsExportarExcelPlantilla xpExcel;        

        clsDatosCliente DatosCliente;
        wsAlmacen.wsCnnCliente conexionWeb; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion); // iRow = 0;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sSql_Detalle = ""; 

        clsGrid Grid; 
        

        public FrmRptUbicacionesClaves()
        {
            InitializeComponent();

            General.Pantalla.AjustarAlto(this, 80); 

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Almacen.wsAlmacen.wsCnnCliente();
            conexionWeb.Url = General.Url; 

            leer = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);

            Grid = new clsGrid(ref grdProductos, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;
            Grid.SetOrder((int)Cols.IdClaveSSA, 3, true);
            Grid.SetOrder((int)Cols.Existencia, 1, true); 


        }

        private void FrmRptUbicacionesClaves_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        private void FrmRptUbicacionesClaves_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    MostrarDetallesClaveLotes(); 
                    break; 
            }
        } 

        #region Eventos_Combos

        //private void cboPasillos_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboPasillos.Data != "0")
        //    {
        //        CargarEstantes();
        //    }
        //}        

        //private void cboEstantes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboEstantes.Data != "0")
        //    {
        //        CargaEntrepanos();
        //    }
        //}

        //private void cboEntrepanos_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void cboPasillos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Seleccione >>");
            cboEstantes.SelectedIndex = 0; 
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0; 

            if (cboPasillos.SelectedIndex != 0)
            {
                CargarEstantes();
            }
        }

        private void cboEstantes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0;

            if (cboEstantes.SelectedIndex != 0)
            {
                CargaEntrepanos();
            }
        }

        private void cboEntrepanos_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        } 
        #endregion Eventos_Combos

        #region Funciones 
        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblIdClaveSSA.Visible = false; 
            Grid.Limpiar(false);
            CargaSubFarmacias();
            CargarPasillos();

            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Seleccione >>");
            cboEstantes.SelectedIndex = 0;

            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");
            cboEntrepanos.SelectedIndex = 0;

            rdoRptTodos.Checked = true;
            rdoRptAmbos.Checked = true;
            rdoRptClave.Focus();
            txtIdClaveSSA.Focus(); 
        }

        private void CargaSubFarmacias()
        {
            cboSubFarmacias.Clear();
            cboSubFarmacias.Add("0", "<< Seleccione >>");

            leer.DataSetClase = Consultas.SubFarmacias(sEstado, sFarmacia, "", "CargaSubFarmacias()");

            if (leer.Leer())
            {
                cboSubFarmacias.Add(leer.DataSetClase, true);
            }

            cboSubFarmacias.SelectedIndex = 0;
        }

        private void CargaDatosClave()
        {
            txtIdClaveSSA.Text = leer.Campo("ClaveSSA");
            lblIdClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = leer.Campo("DescripcionSal");            
                       
        }

        private void CargarPasillos()
        {
            cboPasillos.Clear();
            cboPasillos.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdPasillo, ( Cast(IdPasillo As varchar) + ' -- ' + DescripcionPasillo ) as NombrePasillo " +
                                    " From CatPasillos (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' Order By IdPasillo ",
                                    sEmpresa, sEstado, sFarmacia); 
            if (!leer.Exec(sSql))
            {
                General.msjError("Error al consultar Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboPasillos.Add(leer.DataSetClase, true);
                }
            }
            cboPasillos.SelectedIndex = 0;
        }

        private void CargarEstantes()
        {
            cboEstantes.Clear();
            cboEstantes.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdEstante, ( Cast(IdEstante As varchar) + ' -- ' + DescripcionEstante ) as NombreEstante " +
                                    " From CatPasillos_Estantes (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' Order By IdEstante ",
                                    sEmpresa, sEstado, sFarmacia, cboPasillos.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Error al consultar Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEstantes.Add(leer.DataSetClase, true);
                }
            }
            cboEstantes.SelectedIndex = 0;
        }

        private void CargaEntrepanos()
        {
            // int iCont = 0;
            // string sText = "";

            cboEntrepanos.Clear();
            cboEntrepanos.Add("0", "<< Seleccione >>");

            string sSql = string.Format(" Select IdEntrepaño, ( Cast(IdEntrepaño As varchar) + ' -- ' + DescripcionEntrepaño ) as NombreEntrepaño " +
                                    " From CatPasillos_Estantes_Entrepaños (Nolock) " +
                                    " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'  " +
                                    " And IdPasillo = '{3}' And IdEstante = '{4}' Order By IdEntrepaño ",
                                    sEmpresa, sEstado, sFarmacia, cboPasillos.Data, cboEstantes.Data);

            if (!leer.Exec(sSql))
            {
                General.msjError("Error al consultar Información.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboEntrepanos.Add(leer.DataSetClase, true);
                }
            }

            cboEntrepanos.SelectedIndex = 0;
        }

        private void CargarGrid()
        {
            ObtenerInformacion(1); 
        }

        private bool ObtenerInformacion(int TipoProceso)
        {
            bool bRegresa = false; 
            string sSql = "";
            int iRenglon = 1, iTipoExistencia = 1, iTipoUbicacion = 2;
            string sSubFarmacia = "", sPasillos = "", sEstante = "", sEntrepaño = "";

            if (cboSubFarmacias.SelectedIndex != 0)
            {
                sSubFarmacia = cboSubFarmacias.Data;
            }

            if (cboPasillos.SelectedIndex != 0)
            {
                sPasillos = cboPasillos.Data;
            }

            if (cboEstantes.SelectedIndex != 0)
            {
                sEstante = cboEstantes.Data;
            }

            if (cboEntrepanos.SelectedIndex != 0)
            {
                sEntrepaño = cboEntrepanos.Data;
            }

            if (rdoRptConExistencia.Checked == true)
            {
                iTipoExistencia = 3;
            }
            if (rdoRptSinExistencia.Checked == true)
            {
                iTipoExistencia = 2;
            }

            if (rdoRptAmbos.Checked == true)
            {
                iTipoUbicacion = 2;
            }

            if (rdoRptAlmacenamiento.Checked == true)
            {
                iTipoUbicacion = 0;
            }

            if (rdoRptPickeo.Checked == true)
            {
                iTipoUbicacion = 1;
            }

            sSql = string.Format(" Exec spp_Rpt_UbicacionProductosClaves_Existencia \n" + // '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', {11} ", 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @IdPasillo = '{4}', @IdEstante = '{5}', @IdEntrepaño = '{6}', \n" +
                "\t@IdClaveSSA = '{7}', @ClaveSSA = '{8}', @TipoExistencia = '{9}', @TipoReporte = '{10}', @TipoUbicacion = '{11}' \n", 
                sEmpresa, sEstado, sFarmacia, sSubFarmacia, sPasillos, sEstante, sEntrepaño,
                lblIdClaveSSA.Text, txtIdClaveSSA.Text, iTipoExistencia, TipoProceso, iTipoUbicacion); 

            Grid.Limpiar(false);
            ////Application.DoEvents();
            ////System.Threading.Thread.Sleep(100);
            this.Refresh(); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.Error, "CargarGrid");  
                General.msjError("Error al consultar Información.");
            }
            else
            {
                bRegresa = true; 
                if (!leer.Leer())
                {
                    bRegresa = false; 
                    General.msjUser("Información no encontrada con los filtros aplicados. Favor de verificar."); 
                }

                dtsDetalle = leer.DataSetClase; 
                leer.RegistroActual = 1;

                if (leer.Registros >= 1000)
                {
                    General.msjAviso("Se recomienda exportar a formato excel por el numero de registros encontrados.");
                }
                else
                {
                    while (leer.Leer())
                    {

                        Grid.AddRow();

                        Grid.SetValue(iRenglon, (int)Cols.IdClaveSSA, leer.Campo("IdClaveSSA_Sal"));
                        Grid.SetValue(iRenglon, (int)Cols.ClaveSSA, leer.Campo("ClaveSSA"));
                        Grid.SetValue(iRenglon, (int)Cols.DescripcionClave, leer.Campo("DescripcionClave"));
                        Grid.SetValue(iRenglon, (int)Cols.Pasillo, leer.Campo("IdPasillo"));
                        Grid.SetValue(iRenglon, (int)Cols.PasilloNombre, leer.Campo("Pasillo"));
                        Grid.SetValue(iRenglon, (int)Cols.Estante, leer.Campo("IdEstante"));
                        Grid.SetValue(iRenglon, (int)Cols.EstanteNombre, leer.Campo("Estante"));
                        Grid.SetValue(iRenglon, (int)Cols.Entrepano, leer.Campo("IdEntrepaño"));
                        Grid.SetValue(iRenglon, (int)Cols.EntrepanoNombre, leer.Campo("Entrepaño"));
                        Grid.SetValue(iRenglon, (int)Cols.Existencia, leer.Campo("Existencia"));

                        iRenglon++;
                    }
                }
            }

            return bRegresa; 
        }

        private void ExportarExcel()
        {
            this.Cursor = Cursors.WaitCursor;
            
            ExportarDetalle();
            
            this.Cursor = Cursors.Default;


            ////string sRutaPlantilla = Application.StartupPath + @"\Plantillas\INV_Existencia_Por_Ubicacion.xls";
            ////bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, false, "INV_Existencia_Por_Ubicacion.xls", DatosCliente);

            ////if (!bRegresa)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo."); 
            ////}
            ////else
            ////{
            ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////    xpExcel.AgregarMarcaDeTiempo = true;

            ////    this.Cursor = Cursors.Default;
            ////    if (xpExcel.PrepararPlantilla())
            ////    {
            ////        this.Cursor = Cursors.WaitCursor;
            ////        ExportarDetalle();

            ////        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////        {
            ////            xpExcel.AbrirDocumentoGenerado();
            ////        }
            ////    }
            ////    this.Cursor = Cursors.Default;
            ////} 
        }

        private bool CargarDetalleExcel()
        {
            bool bRegresa = true;

            bRegresa = ObtenerInformacion(4); 

            return bRegresa;
        }

        private void ExportarDetalle()
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sNombreDocumento = "INV_Existencia_Por_Ubicacion";
            string sNombreHoja = "EXISTENCIAS_UBICACIONES";
            string sConcepto = "REPORTE DE EXISTENCIAS POR UBICACIONES";

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            DateTime dtpFecha = General.FechaSistema;
            int iAño = dtpFecha.AddMonths(-1).Year, iMes = dtpFecha.AddMonths(-1).Month;
            //int iHoja = 1;
            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            leer.DataSetClase = dtsDetalle;
            ////xpExcel.GeneraExcel(iHoja);

            ////xpExcel.Agregar(sEmpresa, 1, 2);
            //////xpExcel.Agregar(sEstado, 3, 1);
            ////xpExcel.Agregar(sFarmacia, 2, 2);
            ////xpExcel.Agregar(sFechaImpresion, 5, 3);

            ////for (int iRenglon = 8; leer.Leer(); iRenglon++ )
            ////{
            ////    xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, (int)ColsExportar.ClaveSSA);
            ////    xpExcel.Agregar(leer.Campo("DescripcionClave"), iRenglon, (int)ColsExportar.Descripcion);
            ////    xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, (int)ColsExportar.CodigoEAN);
            ////    xpExcel.Agregar(leer.Campo("ClaveLote"), iRenglon, (int)ColsExportar.Lote);
            ////    xpExcel.Agregar(Fg.Mid(General.FechaYMD(leer.CampoFecha("FechaCaducidad")), 1, 7), iRenglon, (int)ColsExportar.Caducidad); 
            ////    xpExcel.Agregar(leer.Campo("Pasillo"), iRenglon, (int)ColsExportar.Pasillo);
            ////    xpExcel.Agregar(leer.Campo("Estante"), iRenglon, (int)ColsExportar.Estante);
            ////    xpExcel.Agregar(leer.Campo("Entrepaño"), iRenglon, (int)ColsExportar.Entrepaño);
            ////    xpExcel.Agregar(leer.Campo("EsDePickeo"), iRenglon, (int)ColsExportar.EsDePickeo); 
            ////    xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)ColsExportar.Existencia);
            ////    xpExcel.Agregar(leer.Campo("ExistenciaEnTransito"), iRenglon, (int)ColsExportar.ExistenciaEnTransito);
            ////    xpExcel.Agregar(leer.Campo("ExistenciaAux"), iRenglon, (int)ColsExportar.ExistenciaGeneral);

            ////}
            ////xpExcel.CerrarDocumento();

            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

        }
        #endregion Funciones    

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (CargarDetalleExcel())
            {
                ExportarExcel();
            }
        }

        #endregion Botones

        #region Impresion 
        private void Imprimir()
        {
            bool bRegresa = false;
            int iTipoExistencia = 1, iTipoUbicacion = 2;
            string sSubFarmacia = "", sPasillos = "", sEstante = "", sEntrepaño = "";

            if (rdoRptConExistencia.Checked == true)
            {
                iTipoExistencia = 3;
            }
            if (rdoRptSinExistencia.Checked == true)
            {
                iTipoExistencia = 2;
            }
            if (cboSubFarmacias.SelectedIndex != 0)
            {                
                sSubFarmacia = cboSubFarmacias.Data;
            }
            if (cboPasillos.SelectedIndex != 0)
            {                
                sPasillos = cboPasillos.Data;
            }
            if (cboEstantes.SelectedIndex != 0)
            {               
                sEstante = cboEstantes.Data;
            }
            if (cboEntrepanos.SelectedIndex != 0)
            {                
                sEntrepaño = cboEntrepanos.Data;
            }
            if (rdoRptAmbos.Checked == true)
            {
                iTipoUbicacion = 2;
            }
            if (rdoRptAlmacenamiento.Checked == true)
            {
                iTipoUbicacion = 0;
            }
            if (rdoRptPickeo.Checked == true)
            {
                iTipoUbicacion = 1;
            }
            
            //if (validarImpresion())
            {
                DatosCliente.Funcion = "Imprimir()";
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;

                myRpt.TituloReporte = "Informe Ubicaciones Claves SSA"; 
                myRpt.RutaReporte = @GnFarmacia.RutaReportes;

                if (rdoRptClave.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_ClavesLotesUbicaciones.rpt";
                }
                if (rdoRptUbicacion.Checked)
                {
                    myRpt.NombreReporte = "PtoVta_UbicacionesClavesLotes.rpt";
                }

                myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
                myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
                myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
                myRpt.Add("@IdSubFarmacia", sSubFarmacia);
                myRpt.Add("@IdPasillo", sPasillos);
                myRpt.Add("@IdEstante", sEstante);
                myRpt.Add("@IdEntrepaño", sEntrepaño);
                myRpt.Add("@IdClaveSSA", lblIdClaveSSA.Text);
                myRpt.Add("@ClaveSSA", txtIdClaveSSA.Text);
                myRpt.Add("@TipoExistencia", iTipoExistencia);
                myRpt.Add("@TipoUbicacion", iTipoUbicacion);


                bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

                ////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                ////else
                ////{
                ////    myRpt.CargarReporte(true);
                ////    bRegresa = !myRpt.ErrorAlGenerar;
                ////}

                if (!bRegresa)
                {
                    General.msjError("Error al generar Informe.");
                }
            }
        }

        #endregion Impresion

        #region Eventos 
        private void txtIdClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtIdClaveSSA.Text, true, "txtIdClaveSSA_Validating");
                if (leer.Leer())
                {
                    CargaDatosClave();                    
                }
                else
                {
                    txtIdClaveSSA.Focus();
                }
            }
        }

        private void txtIdClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtIdClaveSSA_KeyDown");
                if (leer.Leer())
                {
                    //txtIdClaveSSA.Text = leer.Campo("IdClaveSSA_Sal");
                    //txtIdClaveSSA_Validating(null, null);
                    CargaDatosClave();
                }
            }
        }

        private void txtIdClaveSSA_TextChanged(object sender, EventArgs e)
        {
            //txtIdClaveSSA.Text = "";
            lblIdClaveSSA.Text = "";
            lblDescripcion.Text = "";     
        }    
        #endregion Eventos        

        private void grdProductos_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sIdClaveSSA = "", sClaveSSA = "";  
            string sSubFarmacia = "", sPasillos = "", sEstante = "", sEntrepaño = "";
            int iRow = Grid.ActiveRow, iTipoExistencia = 1;

            //Actualizar iTipoExistencia
            if (rdoRptConExistencia.Checked == true)
            {
                iTipoExistencia = 3;
            }
            if (rdoRptSinExistencia.Checked == true)
            {
                iTipoExistencia = 2;
            }

            if ( Grid.Rows > 0 ) 
            {
                sIdClaveSSA = Grid.GetValue(iRow, (int)Cols.IdClaveSSA);
                sClaveSSA = Grid.GetValue(iRow, (int)Cols.ClaveSSA);
                sPasillos = Grid.GetValueInt(iRow, (int)Cols.Pasillo).ToString();
                sEstante = Grid.GetValueInt(iRow, (int)Cols.Estante).ToString();
                sEntrepaño = Grid.GetValueInt(iRow, (int)Cols.Entrepano).ToString();

                //sSql_Detalle = string.Format(" Exec spp_Rpt_UbicacionProductosClaves_Existencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                sSql_Detalle = string.Format("Exec spp_Rpt_UbicacionProductosClaves_Existencia \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @IdPasillo = '{4}', @IdEstante = '{5}', @IdEntrepaño = '{6}', \n" +
                    "\t@IdClaveSSA = '{7}', @ClaveSSA = '{8}', @TipoExistencia = '{9}', @TipoReporte = '{10}' \n", 
                    sEmpresa, sEstado, sFarmacia, sSubFarmacia, sPasillos, sEstante, sEntrepaño, sIdClaveSSA, sClaveSSA,iTipoExistencia,2);

                FrmRptUbicacionesClavesDetalle f = new FrmRptUbicacionesClavesDetalle(sSql_Detalle); 
                f.ShowDialog(); 
            }
        }

        private void MostrarDetallesClaveLotes()
        {
            string sIdClaveSSA = "", sClaveSSA = "";
            string sSubFarmacia = "", sPasillos = "", sEstante = "", sEntrepaño = "";
            int iRow = Grid.ActiveRow;
            int iTipoExistencia = 1;

            if (Grid.Rows > 0)
            {
                sClaveSSA = txtIdClaveSSA.Text;
                
                if (rdoRptConExistencia.Checked == true)
                {
                    iTipoExistencia = 3;
                }
                if (rdoRptSinExistencia.Checked == true)
                {
                    iTipoExistencia = 2;
                }

                ////sIdClaveSSA = Grid.GetValue(iRow, (int)Cols.IdClaveSSA);
                ////sClaveSSA = Grid.GetValue(iRow, (int)Cols.ClaveSSA);
                ////sPasillos = Grid.GetValueInt(iRow, (int)Cols.Pasillo).ToString();
                ////sEstante = Grid.GetValueInt(iRow, (int)Cols.Estante).ToString();
                ////sEntrepaño = Grid.GetValueInt(iRow, (int)Cols.Entrepano).ToString();

                //sSql_Detalle = string.Format(" Exec spp_Rpt_UbicacionProductosClaves_Existencia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                sSql_Detalle = string.Format("Exec spp_Rpt_UbicacionProductosClaves_Existencia \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdSubFarmacia = '{3}', @IdPasillo = '{4}', @IdEstante = '{5}', @IdEntrepaño = '{6}', \n" +
                    "\t@IdClaveSSA = '{7}', @ClaveSSA = '{8}', @TipoExistencia = '{9}', @TipoReporte = '{10}' \n",
                    sEmpresa, sEstado, sFarmacia, sSubFarmacia, sPasillos, sEstante, sEntrepaño, sIdClaveSSA, sClaveSSA, iTipoExistencia, 3);

                FrmRptUbicacionesClavesDetalleLotes f = new FrmRptUbicacionesClavesDetalleLotes(sSql_Detalle);
                f.ShowDialog();
            }
        }
    }
}
