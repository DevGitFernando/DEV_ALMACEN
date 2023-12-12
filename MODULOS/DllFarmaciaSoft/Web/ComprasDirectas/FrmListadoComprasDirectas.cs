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
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos; 

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Web.ComprasDirectas
{
    public partial class FrmListadoComprasDirectas : FrmBaseExt
    {
        enum Cols
        {
            Ninguna,
            IdProveedor, NombreProveedor, FolioRecepcion, FechaDocumento, FechaRegistro, Referencia, Total
        }

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionClienteUnidad conecionCte;
        DataSet dtsConcentrado;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeer leerDetalles;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid Grid;
        int iTimeOut = 250000;

        string sSqlFarmacias = "";
        string sUrl;
        string sHost = "";
        // string sUrl_RutaReportes = "";
        string sFormato = "#,###,##0.###0";
        int iValor_0 = 0;

        string sUrl_Regional = ""; 
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        // Thread _workerThread;

        // bool bEjecutando = false;
        // bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false;

        //clsExportarExcelPlantilla xpExcel;

        public FrmListadoComprasDirectas()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            
            Grid = new clsGrid(ref grdCompras, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true;
            Grid.SetOrder(true); 

        }

        private void FrmListadoComprasDirectas_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            lblTotal.Text = iValor_0.ToString(sFormato);
            Grid.Limpiar(false);   

            CargarEstados();
            
            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0;

            FrameFechas.Enabled = true;
            frameUnidad.Enabled = true;
            frameProveedor.Enabled = true;

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                CargaDetallesCompras();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            int iRow = 2, iCol = 1;
            string sNombreFile = "", sRutaReportes = "", sRutaPlantilla = "", sPeriodo = "";
            string sFecha = "";
            string sNombreHoja = "Concentrado";
            int iColsEncabezado = 8;

            string sFechaImpresion = "Fecha Impresión:" + DateTime.Now.ToShortDateString();

            ///DtGeneral.RutaReportes = sRutaReportes;

            if (DatosDetalle())
            {

                sNombreFile = "Listado de Ordenes de Compras Recepcionadas.xls";
                //sRutaPlantilla = Application.StartupPath + @"\\Plantillas\COM_Rpt_Listado_Ordenes_Compra_Recepcionadas.xls";
                //DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_Rpt_Listado_Ordenes_Compra_Recepcionadas.xls", DatosCliente);

                //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                //xpExcel.AgregarMarcaDeTiempo = false;

                excel = new clsGenerarExcel(); 
                //excel.NombreArchivo = sNombreFile;
                excel.AgregarMarcaDeTiempo = true;

                if (excel.PrepararPlantilla(sNombreFile))
                {

                    //xpExcel.GeneraExcel(1);
                    excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                    //xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
                    //iRow++;
                    //xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
                    //iRow++;

                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    iRow++;
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, DtGeneral.EstadoConectadoNombre);
                    iRow++;

                    sPeriodo = string.Format("COMPRAS DIRECTAS DEL PERIODO DEL {0} AL {1} ",
                       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                    //xpExcel.Agregar(sPeriodo, iRow, 2);
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, sPeriodo);

                    iRow = 7;
                    //xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, sFechaImpresion);

                    // Se ponen los detalles
                    //leerExportarExcel.RegistroActual = 1;
                    iRow = 10;

                    //for (int i = 1; i <= Grid.Rows; i++)
                    //{
                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.IdProveedor), iRow, (int)Cols.IdProveedor);
                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.NombreProveedor), iRow, (int)Cols.NombreProveedor);

                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FolioRecepcion), iRow, (int)Cols.FolioRecepcion);
                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FechaDocumento), iRow, (int)Cols.FechaDocumento);
                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.FechaRegistro), iRow, (int)Cols.FechaRegistro);
                    //    xpExcel.Agregar(Grid.GetValue(i, (int)Cols.Referencia), iRow, (int)Cols.Referencia);
                    //    xpExcel.Agregar(Grid.GetValueDou(i, (int)Cols.Total), iRow, (int)Cols.Total);

                    //    iRow++;
                    //}

                    excel.InsertarTabla(sNombreHoja, iRow, 2, dtsConcentrado);
                    excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                    //xpExcel.CerrarDocumento();
                    //excel.CerraArchivo();
                    //Detalles
                    //xpExcel.GeneraExcel(2);
                    sNombreHoja = "Detallado";
                    excel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                    iRow = 2;

                    //xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
                    //iRow++;
                    //xpExcel.Agregar(DtGeneral.EstadoConectadoNombre, iRow, 2);
                    //iRow++;

                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    iRow++;
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, DtGeneral.EstadoConectadoNombre);
                    iRow++;

                    sPeriodo = string.Format("COMPRAS DIRECTAS DEL PERIODO DEL {0} AL {1} ",
                       General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
                    //xpExcel.Agregar(sPeriodo, iRow, 2);
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, sPeriodo);

                    iRow = 7;
                    //xpExcel.Agregar(DateTime.Now.ToShortDateString(), iRow, 3);
                    excel.EscribirCeldaEncabezado(sNombreHoja, iRow, 2, iColsEncabezado, 20, sFechaImpresion);

                    iRow = 10;

                    //for (int i = 1; leerDetalles.Leer(); i++)
                    //{
                    //    iCol = 2;
                    //    xpExcel.Agregar(leerDetalles.Campo("IdProveedor"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("Proveedor"), iRow, iCol++);

                    //    xpExcel.Agregar(leerDetalles.Campo("FechaGeneracionOC"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("FolioOrdenCompraReferencia"), iRow, iCol++);

                    //    xpExcel.Agregar(leerDetalles.Campo("Folio"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("FechaDocto"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("FechaRegistro"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("ReferenciaDocto"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("PersonalCompras"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("ClaveSSA_Base"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("ClaveSSA"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("DescripcionSal"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("TipoDeClave"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("Laboratorio"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("CodigoEAN"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("Presentacion"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("ContenidoPaquete"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("ClaveLote"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("Costo"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("CantidadLote"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("TasaIva"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("FechaCad"), iRow, iCol++);
                    //    xpExcel.Agregar(leerDetalles.Campo("MesesParaCaducar"), iRow, iCol++);

                    //    iRow++;
                    //}


                    excel.InsertarTabla(sNombreHoja, iRow, 2, leerDetalles.DataSetClase);
                    excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                    // Finalizar el Proceso 
                    //xpExcel.CerrarDocumento();
                    excel.CerraArchivo();

                    //if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    //{
                    //  xpExcel.AbrirDocumentoGenerado();
                    //}

                    excel.AbrirDocumentoGenerado(true); 

                }
            }

        }           
        #endregion Botones

        #region Cargar Combos

        private void CargarEstados()
        {
            ////if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboFarmacias.Clear();
                cboFarmacias.Add();


                string sSql = ""; //  "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";

                sSql = " Select distinct E.IdEstado, (E.IdEstado + ' -- ' +  E.NombreEstado) as Estado, E.IdEmpresa, E.StatusEdo, U.UrlFarmacia as UrlRegional " +
                      " From vw_EmpresasEstados E (NoLock) " +
                      " Inner Join vw_Regionales_Urls U (NoLock) On ( E.IdEmpresa = U.IdEmpresa and E.IdEstado = U.IdEstado and U.IdFarmacia = '0001' ) " +
                      " Order By E.IdEmpresa, E.IdEstado ";

                if (!leerLocal.Exec(sSql))
                {
                    Error.GrabarError(leerLocal, "CargarEstados()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                }
                else
                {
                    cboEstados.Add(leerLocal.DataSetClase, true, "IdEstado", "Estado");
                }
            }

            cboEstados.SelectedIndex = 0;
            cboFarmacias.SelectedIndex = 0; 

        }

        private void CargarFarmacias()
        {
            ////if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Clear();
                cboFarmacias.Add();

                sSqlFarmacias = string.Format(" Select Distinct F.IdFarmacia, (F.IdFarmacia + ' - ' + F.NombreFarmacia) as Farmacia, U.UrlFarmacia, C.Servidor " +
                                    " From CatFarmacias F (Nolock) " +
                                    " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia ) " +
                                    " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) " +
                                    " Where F.IdEstado = '{0}'  And F.IdTipoUnidad Not in ('000','005')  " +
                                    " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", cboEstados.Data);

                if (!leerLocal.Exec(sSqlFarmacias))
                {
                    Error.GrabarError(leerLocal, "CargarFarmacias()");
                    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                }
                else
                {
                    cboFarmacias.Add(leerLocal.DataSetClase, true, "IdFarmacia", "Farmacia");

                }
            }

            cboFarmacias.SelectedIndex = 0;
        }

        #endregion Cargar Combos 

        #region Funciones 
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true;
            btnEjecutar.Enabled = true;            
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }

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

        private void CargaDetallesCompras()
        {
            string sWhere = "";

            if (txtProveedor.Text.Trim() != "")            
            {
                sWhere = " And IdProveedor = '" + txtProveedor.Text + "'";
            }
           
            if (txtReferencia.Text.Trim() != "")
            {
                sWhere += " And ReferenciaDocto like '%" + txtReferencia.Text + "%'";
            }


            string sSql =
                string.Format(" Select IdProveedor As 'Clave Proveedor', Proveedor, Folio, convert(varchar(10), FechaDocto, 120) As 'Fecha Documento', " + 
                        " convert(varchar(10), FechaRegistro, 120) As 'Fecha Registro', ReferenciaDocto As Referencia, Total " +
                        " From vw_ComprasEnc (Nolock) " + 
                        " Where IdEstado = '{0}' And IdFarmacia = '{1}'  " + 
                        " And Convert( varchar(10), FechaRegistro, 120) between '{2}' and '{3}'  {4}  " +
                        " Order By Folio, FechaRegistro ", cboEstados.Data, cboFarmacias.Data, General.FechaYMD(dtpFechaInicial.Value), 
                        General.FechaYMD(dtpFechaFinal.Value), sWhere );

            //// 
            lblTotal.Text = iValor_0.ToString(sFormato); 
            Grid.Limpiar(); 

            // if (validarDatosDeConexion())
            {
                ////cnnUnidad = new clsConexionSQL(DatosDeConexion);
                ////cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
                ////cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300; 

                // leer = new clsLeer(ref cnnUnidad);
                leer = new clsLeer(); 
                conecionCte = new clsConexionClienteUnidad();
                
                conecionCte.Empresa = DtGeneral.EmpresaConectada;
                conecionCte.Estado = cboEstados.Data;
                conecionCte.Farmacia = cboFarmacias.Data;
                conecionCte.Sentencia = sSql; 

                conecionCte.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
                conecionCte.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

                try
                {
                    // sUrl_Regional = General.Url; 
                    conexionWeb.Url = sUrl_Regional;
                    conexionWeb.Url = sUrl;
                    conexionWeb.Timeout = iTimeOut; 
                    //leer.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());

                    leer.DataSetClase = dtsConcentrado = conexionWeb.ExecuteExt(conecionCte.dtsInformacion, DtGeneral.CfgIniPuntoDeVenta, sSql);
                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message); 
                }


                if ( leer.SeEncontraronErrores() )
                {
                    Error.GrabarError(leer, "CargaDetallesCompras()");
                    General.msjError("Ocurrió un error al obtener la información de las compras.");
                }
                else
                {
                    if (leer.Leer())
                    {
                        btnImprimir.Enabled = true;
                        btnExportarExcel.Enabled = true; 
                        
                        //// bSeEncontroInformacion = true;
                        FrameFechas.Enabled = false;
                        frameUnidad.Enabled = false;
                        frameProveedor.Enabled = false;
                        Grid.LlenarGrid(leer.DataSetClase, false, false);
                        lblTotal.Text = Grid.TotalizarColumnaDou((int)Cols.Total).ToString(sFormato); 
                    }
                    else
                    {
                        // bSeEncontroInformacion = false;
                        General.msjUser("No se encontro información con los criterios especificados, verifique."); 
                    }                   
                }
            }
        }

        private bool ValidaDatos()
        {
            bool bRegresa = false;

            if (cboEstados.SelectedIndex != 0)
            {
                bRegresa = true;
            }

            if (bRegresa)
            {
                if (cboFarmacias.SelectedIndex != 0)
                {
                    bRegresa = true;
                }
                else
                {
                    bRegresa = false;
                }
            }

            if (!bRegresa)
            {
                General.msjAviso("Faltan Datos por Capturar, Verifique !!");
            }

            return bRegresa;
        }

        private bool DatosDetalle()
        {
            bool bRegresa = true;
            string sWhere = "";

            if (txtProveedor.Text.Trim() != "")
            {
                sWhere = " And E.IdProveedor = '" + txtProveedor.Text + "'";
            }

            if (txtReferencia.Text.Trim() != "")
            {
                sWhere += " And E.ReferenciaDocto like '%" + txtReferencia.Text + "%'";
            }
             

            string sSql = string.Format("Exec Spp_Rpt_ListadoComprasDirectas @IdEstado = '{0}', @IdFarmacia = '{1}', @IdProveedor = '{2}', " +
                            "@FechaInicial = '{3}', @FechaFinal = '{4}', @ReferenciaDocto = '{5}'",
                            cboEstados.Data, cboFarmacias.Data, txtProveedor.Text.Trim(),
                            General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), txtReferencia.Text.Trim());

            leerDetalles = new clsLeer();
            conecionCte.Sentencia = sSql;

            //leer.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());

            try
            {
                // sUrl_Regional = General.Url; 
                conexionWeb.Url = sUrl_Regional;
                //conexionWeb.Timeout = iTimeOut;
                //leerDetalles.DataSetClase = conexionWeb.ExecuteRemoto(conecionCte.dtsInformacion, DatosCliente.DatosCliente());

                conexionWeb.Url = sUrl;
                conexionWeb.Timeout = iTimeOut;

                leerDetalles.DataSetClase = conexionWeb.ExecuteExt(conecionCte.dtsInformacion, DtGeneral.CfgIniPuntoDeVenta, sSql);
            }
            catch (Exception ex)
            {
                Error.LogError(ex.Message);
            }


            if (leerDetalles.SeEncontraronErrores())
            {
                Error.GrabarError(leerDetalles, "CargaDetallesCompras()");
                General.msjError("Ocurrió un error al obtener la información de las compras.");
                bRegresa = false;
            }
            else
            {
                if (leerDetalles.Registros == 0)
                {
                    // bSeEncontroInformacion = false;
                    General.msjUser("No se encontro información con los criterios especificados, verifique.");
                    bRegresa = false;
                }
            }
            return bRegresa;
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
            sUrl = ""; 
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
            
            //FrmOrdenesDeComprasUnidad f = new FrmOrdenesDeComprasUnidad();
            //f.MostrarFolioCompra(Grid.GetValue(Grid.ActiveRow, 1), cboEstados.Data, cboFarmacias.Data,
            //    Grid.GetValue(Grid.ActiveRow, (int)Cols.FolioRecepcion), sUrl); 
        } 
        #endregion Eventos
    }
}
