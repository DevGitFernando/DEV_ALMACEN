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

using DllPedidosClientes;

namespace DllPedidosClientes.Reportes 
{
    public partial class FrmExistenciaFarmaciaUnidad : FrmBaseExt
    {
        
        clsConexionSQL cnn;        
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        // clsGrid Grid;

        // string sSqlFarmacias = "";
        string sTablaFarmacia = "";       

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;
                        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Se Declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        public FrmExistenciaFarmaciaUnidad()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            // leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            // Se crea la instancia del objeto de la clase de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            // DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;
            CargarListaReportes();
        }

        private void CargarListaReportes()
        {
            cboReporte.Clear();
            cboReporte.Add(); // Agrega Item Default 

            cboReporte.Add("CteUni_Admon_Existencia_Farmacias.rpt", "Existencia de Productos");
            //cboReporte.Add("CteReg_Admon_Existencia_Farmacias.rpt", "Existencia de Productos Detallado");                        

            cboReporte.SelectedIndex = 1; 
        }

        private void FrmExistenciaFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos         

        private void CargarEstados()
        {
            if (cboEstados.NumeroDeItems == 0)
            {
                cboEstados.Clear();
                cboEstados.Add();

                cboEstados.Add(DtGeneralPedidos.Estados, true, "IdEstado", "Estado");

                //////string sSql = "Select distinct IdEstado, Estado, EdoStatus From vw_Farmacias (NoLock) Where EdoStatus = 'A' Order By IdEstado ";
                //////if (!leerWeb.Exec(sSql))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarEstados()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                //////}
                //////else
                //////{
                //////    cboEstados.Add(leerWeb.DataSetClase, true, "IdEstado", "Estado");
                //////}
            }
            cboEstados.SelectedIndex = 0; 
            cboEstados.Data = DtGeneralPedidos.EstadoConectado;
            cboEstados.Enabled = false;
        }

        private void CargarFarmacias()
        {
            if (cboFarmacias.NumeroDeItems == 0)
            {
                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                //////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                //////                              " Where IdEstado = '{0}' " +
                //////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado);

                //////if (!leerWeb.Exec(sSqlFarmacias))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                //////}
                //////else
                //////{
                //////    cboFarmacias.Add(leerWeb.DataSetClase, true);
                //////    cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
                //////    cboFarmacias.Enabled = false;
                //////}
            }

            cboFarmacias.Data = DtGeneralPedidos.FarmaciaConectada;
            cboFarmacias.Enabled = false;

            txtFarmacia.Text = DtGeneralPedidos.FarmaciaConectada;
            lblFarmacia.Text = DtGeneralPedidos.FarmaciaConectadaNombre;
            txtFarmacia.Enabled = false;
        }

        #endregion Cargar Combos          

        #region Impresion      

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            if (bRegresa && cboReporte.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
                cboReporte.Focus(); 
            }

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {           
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central.               

                DatosCliente.Funcion = "Imprimir()";              
                clsImprimir myRpt = new clsImprimir(General.DatosConexion);
                // byte[] btReporte = null;
                string sEstado = cboEstados.Data;
                //string sFarmacia = cboFarmacias.Data;
                string sFarmacia = Fg.PonCeros(txtFarmacia.Text, 4);
                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"I:\SII_OFICINA_CENTRAL\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = cboReporte.Data + ""; 

                //if (General.ImpresionViaWeb)
                {
                    //////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    //////DataSet datosC = DatosCliente.DatosCliente();

                    //////conexionWeb.Url = General.Url;
                    //////conexionWeb.Timeout = 300000;
                    //////btReporte = conexionWeb.ReporteExtendido(sEstado, sFarmacia, InfoWeb, datosC);
                    //////bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);

                    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                    DataSet datosC = DatosCliente.DatosCliente();
                    bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC);      

                //}
                //else
                //{
                    //// Lineas para pruebas locales ///////
                    //myRpt.CargarReporte(true);
                    //bRegresa = !myRpt.ErrorAlGenerar;
                    ////////////////////////////////////////
                }

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    auditoria.GuardarAud_MovtosUni("*", myRpt.NombreReporte);
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;         

            Fg.IniciaControles(this, true); 
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            
            FrameListaReportes.Enabled = bValor;
            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;

            CargarEstados();
            cboReporte.SelectedIndex = 1; 
 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
            {
                IniciarProcesamiento();
            }
        }

        private void IniciarProcesamiento()
        {
            if (ValidaDatos())
            {
                cboReporte.Enabled = false;//Se bloquea el combo de los reportes.
                bSeEncontroInformacion = false;
                btnNuevo.Enabled = false;
                btnEjecutar.Enabled = false;
                btnImprimir.Enabled = false;


                FrameListaReportes.Enabled = false;

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
            ImprimirInformacion(); 
        } 
        #endregion Botones        

        #region Grid        

        private void ObtenerInformacion()
        {           
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            string sCadena = "";

            int iOpcion = 0;
            int iTipoInsumo = 0, iTipoDispensacion = 0;

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

            if (rdoRptConExist.Checked)
            {
                iOpcion = 1;
            }
            if (rdoRptSinExist.Checked)
            {
                iOpcion = 2;
            }

            sTablaFarmacia = "CTE_FarmaciasProcesar";

            string sSql = string.Format(" Exec spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias '{0}', '{1}', {2}, '{3}', '{4}' ",
                                        DtGeneralPedidos.EstadoConectado, Fg.PonCeros(txtFarmacia.Text, 4), iOpcion, iTipoInsumo, iTipoDispensacion);
            sSql += "\n " + string.Format("Select top 1 * From Rpt_CteUnidad_Admon_ExistenciaFarmacia (NoLock) ");

            try
            {
                leer.Reset();
                leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), sSql, "reporte", sTablaFarmacia);
            }
            catch { }

            if (leer.SeEncontraronErrores())
            {
                btnImprimir.Enabled = false;
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {

                btnImprimir.Enabled = true;
                bSeEncontroInformacion = true;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    bSeEjecuto = true;
                }
            }

            bEjecutando = false;

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosUni("*", sCadena);
            
            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "ObtenerInformacion()");
            ////    General.msjError("Ocurrió un error al obtener la información del reporte.");
            ////    bSeEncontroInformacion = false; 
            ////}
            ////else
            ////{
            ////    if (leer.Leer())
            ////    {
            ////        btnImprimir.Enabled = true;
            ////        bSeEncontroInformacion = true;
            ////    }
            ////    else
            ////    {
            ////        bSeEncontroInformacion = false;
            ////    }

            ////    sCadena = sSql.Replace("'", "\"");
            ////    auditoria.GuardarAud_MovtosUni("*", sCadena);
            ////}

            ////bSeEjecuto = true; 
            ////bEjecutando = false;  
            
            this.Cursor = Cursors.Default;
        } 
        #endregion Grid 

        #region Eventos
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;            
            FrameListaReportes.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                FrameListaReportes.Enabled = true;                
                btnNuevo.Enabled = true;
                btnEjecutar.Enabled = false;

                if (!bSeEncontroInformacion) 
                {
                    _workerThread.Interrupt();
                    _workerThread = null;

                    ActivarControles(); 

                    if ( bSeEjecuto ) 
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                }
            }
        }       

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstados.SelectedIndex != 0)
            {
                cboEstados.Enabled = false;                
                CargarFarmacias();
            }
        }        

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;
            //////string sEdo = "", sFar = "";
            //////sEdo = cboEstados.Data;

            //////string sSql = string.Format("Delete From CteReg_Farmacias_Procesar_Existencia ");

            //////if (!leer.Exec(sSql))
            //////{
            //////    Error.GrabarError(leer, "FarmaciasAProcesar()");
            //////    General.msjError("Ocurrió un error al borrar tabla.");
            //////    bReturn = false;
            //////}
            //////else
            //////{
            //////    for (int i = 1; i <= Grid.Rows; i++)
            //////    {
            //////        if( Grid.GetValueBool(i,3) )
            //////        {
            //////            sFar = Grid.GetValue(i,1);

            //////            string sQuery = string.Format(" Insert Into CteReg_Farmacias_Procesar_Existencia " +
            //////                                " Select '{0}','{1}','A',0 ", sEdo, sFar );
            //////           if (!leer.Exec(sQuery))
            //////            {
            //////                Error.GrabarError(leer, "FarmaciasAProcesar()");
            //////                General.msjError("Ocurrió un error al Insertar Farmacias a Procesar.");
            //////                bReturn = false;
            //////                break;
            //////            }
            //////            else
            //////            {
            //////                bReturn = true;
            //////            }
            //////        }
            //////    }
            //////}

            return bReturn;
        }

        #endregion Eventos

        #region Funciones 
        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (cboReporte.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("Seleccione un reporte por favor");
                cboReporte.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones


    } 
}
