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
//using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;


using DllFarmaciaSoft;

namespace DllPedidosClientes.ReportesCentral
{
    public partial class FrmProximosACaducar_Regional : FrmBaseExt 
    {
        enum Cols
        {
            IdJuris = 1, Jurisdiccion = 2, IdFarmacia = 3, Farmacia = 4, ClaveSSA = 5, Descripcion = 6, Presentacion = 7,
            ClaveLote = 8, FechaCaducidad = 9, Existencia = 10
        }

        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeerWeb leerWeb;
        // clsGrid Grid;
        clsConsultas query;
        clsAyudas ayuda;
        // DataSet dtsFarmacias;
        // clsConexionSQL cnnUnidad;
        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        //clsExportarExcelPlantilla xpExcel;        
        clsListView lst;
        DataSet dtsProximosCaducar = new DataSet();

        // string sSqlFarmacias = "";
        string sUrl = "";
        string sHost = "";
        // string sTablaFarmacia = "";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        private bool bLimpiar = true;

        public FrmProximosACaducar_Regional()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
            cnn.SetConnectionString();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name, true);
            ayuda = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneralPedidos.DatosApp, this.Name);

            ////Grid = new clsGrid(ref grdExistencia, this);
            ////Grid.EstiloGrid(eModoGrid.SeleccionSimple);
            ////Grid.Limpiar(false);

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente); 

            // Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;
            //lst.PermitirAjusteDeColumnas = false; 
        }

        private void FrmExistenciaPorClaveSSA_Load(object sender, EventArgs e)
        {
            CargarEstados();
            CargarJurisdicciones();

            if (bLimpiar)
            {
                btnNuevo_Click(null, null);
            }
        }

        #region Cargar Combos

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

            }
            cboEstados.SelectedIndex = 0;
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
        }

        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                cboJurisdicciones.Add("*", "Todas las jurisdicciones");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion"); 
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void CargarFarmacias()
        {
            string sSql = "";

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "Todas las Farmacias");

            sSql = string.Format(" Select F.IdFarmacia, (F.IdFarmacia + ' - ' + F.Farmacia) as Farmacia From vw_Farmacias F (NoLock) " +
                                 " Inner Join vw_Farmacias_Urls U (Nolock) " +
                                    " On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia ) " +
                                 " Where F.IdEstado = '{0}' And F.IdJurisdiccion = '{1}' And U.StatusUrl = 'A' ",
                                cboEstados.Data, cboJurisdicciones.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
            }
            else
            {
                if (leer.Leer())
                {
                    cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
                }
            }

            cboFarmacias.SelectedIndex = 0;
        }
        #endregion Cargar Combos          

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            query.MostrarMsjSiLeerVacio = false;
            Fg.IniciaControles(this, true);
            // Grid.Limpiar(false);
            lst.LimpiarItems(); 

            rdoConcentrado.Checked = true; 
            rdoTpDispAmbos.Checked = false; 
            rdoTpDispVenta.Checked = false; 
            rdoTpDispConsignacion.Checked = true; 

            rdoTpDispAmbos.Enabled = false;
            rdoTpDispVenta.Enabled = false; 


            IniciaToolBar(true, true, false);

            query.MostrarMsjSiLeerVacio = true;

            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
            //lblTotal.Text = Grid.TotalizarColumna(4).ToString();

            if (DtGeneralPedidos.ClienteConectado == TipoDeClienteExterno.Administracion_Unidad)
            {
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();

            // int iColInicial = 0;
            // int iColActiva = 0;
            // int iNumDias = 0;
            string sTituloPeriodo = "";

            leerToExcel.DataSetClase = dtsProximosCaducar;   
            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.EdoJuris_Proximos_Caducar, "PLANTILLA_006");

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                sTituloPeriodo = General.FechaYMD(dtpFechaInicial.Value);
                ////if (!chkDiaEspecificado.Checked)
                {
                    sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial.Value) + ' ' + dtpFechaInicial.Value.Year.ToString();
                }


                int iRow = 9;
                // int iRowInicial = 9;

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();

                    xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
                    xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneralPedidos.EstadoConectadoNombre, 3, 2);
                    xpExcel.Agregar("Próximos a caducar " + sTituloPeriodo, 4, 2);

                    //xpExcel.Agregar("Fecha de reporte : " + leerToExcel.CampoFecha("FechaReporte").ToString(), 6, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("IdJurisdiccion"), iRow, 2);
                        xpExcel.Agregar(leerToExcel.Campo("Jurisdiccion"), iRow, 3);
                        xpExcel.Agregar(leerToExcel.Campo("IdFarmacia"), iRow, 4);
                        xpExcel.Agregar(leerToExcel.Campo("Farmacia"), iRow, 5);
                        xpExcel.Agregar(leerToExcel.Campo("ClaveSSA"), iRow, 6);
                        xpExcel.Agregar(leerToExcel.Campo("DescripcionClave"), iRow, 7);
                        xpExcel.Agregar(leerToExcel.Campo("Presentacion_ClaveSSA"), iRow, 8);
                        xpExcel.Agregar(leerToExcel.Campo("ClaveLote"), iRow, 9);
                        xpExcel.Agregar(leerToExcel.Campo("FechaCaducidad"), iRow, 10);
                        xpExcel.Agregar(leerToExcel.Campo("Existencia"), iRow, 11);
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
            */
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
            {
                //LlenarGrid();
                bSeEncontroInformacion = false;
                btnNuevo.Enabled = false;
                btnEjecutar.Enabled = false;
                // btnImprimir.Enabled = false;
                btnExportarExcel.Enabled = false;

                //cboEstados.Enabled = false;
                //cboFarmacias.Enabled = false;
                //dtpFechaInicial.Enabled = false;
                //dtpFechaFinal.Enabled = false;

                bSeEjecuto = false;
                tmEjecuciones.Enabled = true;
                tmEjecuciones.Start();


                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(1000);

                _workerThread = new Thread(this.ObtenerInformacion);
                _workerThread.Name = "GenerandoValidacion";
                _workerThread.Start();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // bool bRegresa = false;
            ////if (Grid.Rows == 0)
            ////{
            ////    General.msjUser("No ha información en pantalla para generar la impresión.");
            ////}
            ////else
            ////{
            ////    DatosCliente.Funcion = "btnImprimir_Click()";
            ////    clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            ////    byte[] btReporte = null;
            ////    string sEstado = cboEstados.Data;
            ////    //string sFarmacia = cboFarmacias.Data;
            ////    string sFarmacia = txtFarmacia.Text;

            ////    //// Linea Para Prueba
            ////    //DtGeneralPedidos.RutaReportes = @"C:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

            ////    myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
            ////    myRpt.NombreReporte = "Rpt_CteReg_CaducarSales_Farmacias";

            ////    if (rdoDetallado.Checked)
            ////        myRpt.NombreReporte = "Rpt_CteReg_CaducarSales_Farmacias_Detallado";

            ////    //if (General.ImpresionViaWeb)
            ////    {
            ////        ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////        ////DataSet datosC = DatosCliente.DatosCliente();

            ////        //////conexionWeb.Url = General.Url;
            ////        ////conexionWeb.Timeout = 300000;
            ////        ////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC);
            ////        ////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

            ////        DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////        DataSet datosC = DatosCliente.DatosCliente();
            ////        bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 
                     
            ////    }

            ////    if (!bRegresa)
            ////    {
            ////        auditoria.GuardarAud_MovtosReg("*", myRpt.NombreReporte);
            ////        General.msjError("Ocurrió un error al cargar el reporte.");
            ////    }
            ////}
        }
        
        #endregion Botones

        #region Grid
        private void ObtenerInformacion()
        {
            // int iMostrar = 2; // Para que muestre solo la farmacia de la unidad.
            string sCadena = "";
            int iTipoInsumo = 0, iTipoDispensacion = 0; 

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;  

            // Determinar el tipo de dispensacion a mostrar 
            if (rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 1;
            }

            if (rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 2;
            }


            // Determinar que tipo de producto se mostrar 
            if (rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            // sTablaFarmacia = "CTE_FarmaciasProcesar";

            string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_EdoJuris_Proximos_Caducar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                   cboEstados.Data, cboJurisdicciones.Data, cboFarmacias.Data,
                   General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value.AddMonths(3), "-"),
                   0, iTipoInsumo, iTipoDispensacion);


            ////try
            ////{
            ////    leer.Reset();
            ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, cboJurisdicciones.Data, sSql, "reporte", sTablaFarmacia); 
            ////}
            ////catch { } 

            lst.LimpiarItems(); 
            if (!leer.Exec(sSql))
            {
                //Error.GrabarError(leer, "");
                //General.msjError("Ocurrió un error al obtener la información de existencias.");

                Error.GrabarError(leer, "ObtenerInformacion()"); 
                // General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo.");
                General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo."); 
            }
            else
            {
                dtpFechaInicial.Enabled = false;
                dtpFechaFinal.Enabled = false;
                //cboFarmacias.Enabled = false;
                // txtFarmacia.Enabled = false;
                IniciaToolBar(true, false, true);

                if (!leer.Leer())
                {
                    lst.CargarDatos(leer.DataSetClase, true, true); 
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    IniciaToolBar(true, false, false);
                    // Grid.DeleteRow(1);
                    bSeEncontroInformacion = false;
                    bSeEjecuto = true;
                }
                else
                {
                    bSeEncontroInformacion = true;
                    sCadena = sSql.Replace("'", "\"");
                    auditoria.GuardarAud_MovtosReg("*", sCadena);
                    // Grid.LlenarGrid(leer.DataSetClase);

                    dtsProximosCaducar = leer.DataSetClase; 
                    lst.CargarDatos(leer.DataSetClase, true, true); 
                }
            } 

            AjustarColumnas(); 
            // lblTotal.Text = Grid.TotalizarColumna(4).ToString();             
            bEjecutando = false;

            this.Cursor = Cursors.Default;
        }



        private void grdExistencia_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ////Codigo.ShowDialog();
            //string sClaveInterna = Grid.GetValue(e.Row + 1, 1);
            //Codigo = new FrmCaducarPorClaveSSA_EstadoFarmaciasCodigos();
            //Codigo.MostrarDetalle(cboEstados.Data, cboFarmacias.Data, sClaveInterna, dtpFechaInicial.Text, dtpFechaFinal.Text );
        }
        #endregion Grid        

        #region Funciones 
        private void IniciaToolBar(bool Nuevo, bool Ejecutar, bool Imprimir)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Imprimir;
        }
        public void MostrarDetalle(string IdEstado, string ClaveInternaSal)
        {
            bLimpiar = false;
            btnNuevo.Enabled = false;
            btnEjecutar_Click(null, null);

            this.ShowDialog();
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                conexionWeb.Url = sUrl;
                DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                IniciaToolBar(true, true, false);
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = false;

                if (!bSeEncontroInformacion)
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    //ActivarControles();

                    if (bSeEjecuto)
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }
        #endregion Funciones       

        #region Funciones y Procedimientos Privados
        private void AjustarColumnas()
        {
            lst.TituloColumna((int)Cols.IdJuris, "Núm. Juris");
            lst.TituloColumna((int)Cols.Jurisdiccion, "Jurisdicción");
            lst.TituloColumna((int)Cols.IdFarmacia, "Núm. Farmacia");
            lst.TituloColumna((int)Cols.Farmacia, "Farmacia");
            lst.TituloColumna((int)Cols.ClaveSSA, "Clave SSA");
            lst.TituloColumna((int)Cols.Descripcion, "Descripción clave");
            lst.TituloColumna((int)Cols.Presentacion, "Presentación");
            lst.TituloColumna((int)Cols.FechaCaducidad, "Fecha caducidad");

            lst.AnchoColumna((int)Cols.Descripcion, 400);


            lst.AnchoColumna(lst.Columnas, 150);
        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboJurisdicciones.Data == "*")
            {
                cboFarmacias.Clear();
                cboFarmacias.Add("*", "Todas las Farmacias");
                cboFarmacias.SelectedIndex = 0;
            }
            else
            {
                CargarFarmacias();
            }
        }
        #endregion Eventos

    }
}
