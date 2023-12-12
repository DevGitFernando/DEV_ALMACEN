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
    public partial class FrmListadoCierresFacturacion : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        clsGrid Grid;
        // clsGrid GridAbasto;

        // string sSqlFarmacias = "";
        string sUrl = "";
        string sHost = "";
        // string sTablaFarmacia = "";

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;
                        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        public FrmListadoCierresFacturacion()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ConsultaDeAbastoFarmacias");
            leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


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
            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);
  
            ////// Clase de Movimientos de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                            DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            Grid = new clsGrid(ref grdReporte, this);

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
                cboFarmacias.Clear();
                cboFarmacias.Add();

                cboFarmacias.Add(DtGeneralPedidos.Farmacias, true, "IdFarmacia", "Farmacia");

                ////////sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, 0 as Procesar From vw_Farmacias (NoLock) " +
                ////////                              " Where IdEstado = '{0}' And IdFarmacia <> '{1}' " +
                ////////                              " Order By IdFarmacia ", DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                //////sSqlFarmacias = string.Format(" Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                //////                " From vw_Farmacias_Urls U (NoLock) " +
                //////                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                //////                " Where U.IdEstado = '{0}' and ( U.IdFarmacia <> '{1}' ) " +
                //////                " and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ",
                //////                DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada);

                ////////Grid.Limpiar(false);

                //////if (!leerWeb.Exec(sSqlFarmacias))
                //////{
                //////    Error.GrabarError(leerWeb, "CargarFarmacias()");
                //////    General.msjError("Ocurrió un error al obtener la lista de Farmacias.");
                //////}
                //////else
                //////{
                //////    cboFarmacias.Add(leerWeb.DataSetClase, true, "IdFarmacia", "Farmacia");
                //////    //Grid.LlenarGrid(leer.DataSetClase);
                //////    //grdReporte.Focus();
                //////}
            }
            cboFarmacias.SelectedIndex = 0;
            
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
                string sFarmacia = txtFarmacia.Text;                 

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = " ";

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                DataSet datosC = DatosCliente.DatosCliente();
                bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sEstado, sFarmacia, InfoWeb, datosC); 

               
                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
                else
                {
                    auditoria.GuardarAud_MovtosReg("Reporte ==> " + myRpt.NombreReporte, General.Url);
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // bool bValor = true;         

            Fg.IniciaControles(this, true);

            ////GridAbasto.Limpiar(true);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            
            CargarEstados();
             
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            //cboFarmacias.Enabled = false;
            txtFarmacia.Enabled = false; 

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
               
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirInformacion(); 
        } 
        #endregion Botones        

        #region Grid       
        private void ObtenerInformacion()
        {           
            //////bEjecutando = true;
            //////this.Cursor = Cursors.WaitCursor;
           
            //////string sCadena = "";

            //////int iOpcion = 0;

            //////sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

            //////string sSql = string.Format(" Exec spp_Rpt_AbastoClaves '{0}', '{1}' ", cboEstados.Data, txtFarmacia.Text);
            ////////sSql += "\n " + string.Format("Select top 1 * From tmpRptAbastoClaves (NoLock) ");

            //////sSql += "\n " + string.Format(" Select top 1 Count(*) As TotalClaves, " + 
            //////                            " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, " + 
            //////                            " ( Select Count(Abasto) From tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, " +
            //////                            " PorcAbasto From tmpRptAbastoClaves (Nolock) Group By PorcAbasto");

            //////try
            //////{
            //////    leer.Reset();
            //////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, sSql, "reporte", sTablaFarmacia); 
            //////}
            //////catch (Exception ex)
            //////{ 
            //////} 


            //////if (leer.SeEncontraronErrores())
            //////{
            //////    btnImprimir.Enabled = false;
            //////    bSeEncontroInformacion = false;
            //////    Error.GrabarError(leer, "ObtenerInformacion()");
            //////    General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            //////}
            //////else
            //////{

            //////    btnImprimir.Enabled = true;
            //////    bSeEncontroInformacion = true;
            //////    if (!leer.Leer())
            //////    {
            //////        General.msjUser("No se encontro información con los criterios especificados"); 
            //////        bSeEjecuto = true;
            //////    }
            //////    else
            //////    {
            //////        GridAbasto.LlenarGrid(leer.DataSetClase);
            //////    }
            //////}

            //////bEjecutando = false;

            //////sCadena = sSql.Replace("'", "\"");
            //////auditoria.GuardarAud_MovtosReg(sCadena, General.Url);

            //////////if (validarDatosDeConexion())
            //////////{
            //////////    cnnUnidad = new clsConexionSQL(DatosDeConexion);
            //////////    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
            //////////    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

            //////////    leer = new clsLeer(ref cnnUnidad);

            //////////    if (FarmaciasAProcesar())
            //////////    {
            //////////        if (!leer.Exec(sSql))
            //////////        {
            //////////            Error.GrabarError(leer, "ObtenerInformacion()");
            //////////            General.msjError("Ocurrió un error al obtener la información del reporte.");
            //////////            bSeEncontroInformacion = false;
            //////////        }
            //////////        else
            //////////        {
            //////////            if (leer.Leer())
            //////////            {                        
            //////////                btnImprimir.Enabled = true;
            //////////                bSeEncontroInformacion = true;
            //////////            }
            //////////            else
            //////////            {
            //////////                bSeEncontroInformacion = false;                        
            //////////            }

            //////////            sCadena = sSql.Replace("'", "\"");
            //////////            auditoria.GuardarAud_MovtosReg(sCadena, sUrl);
            //////////        }

            //////////        bSeEjecuto = true;
            //////////        bEjecutando = false;
            //////////    }
            //////////}
            //////this.Cursor = Cursors.Default;
        } 

        #endregion Grid 

        #region Eventos
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default; 
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true; 
            //cboFarmacias.Enabled = true;
            txtFarmacia.Enabled = true; 
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

                    ActivarControles();

                    if (bSeEjecuto)
                    {
                        General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
                    }
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
            string sEdo = "", sFar = "";
            sEdo = cboEstados.Data;

            string sSql = string.Format("Delete From CteReg_Farmacias_Procesar_Existencia ");

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "FarmaciasAProcesar()");
                General.msjError("Ocurrió un error al borrar tabla.");
                bReturn = false;
            }
            else
            {
                //for (int i = 1; i <= Grid.Rows; i++)
                //{
                //    if( Grid.GetValueBool(i,3) )
                //    {
                sFar = txtFarmacia.Text; //Grid.GetValue(i,1);

                string sQuery = string.Format(" Insert Into CteReg_Farmacias_Procesar_Existencia " +
                                    " Select '{0}','{1}','A',0 ", sEdo, sFar );
                if (!leer.Exec(sQuery))
                {
                    Error.GrabarError(leer, "FarmaciasAProcesar()");
                    General.msjError("Ocurrió un error al Insertar Farmacias a Procesar.");
                    bReturn = false;
                    //break;
                }
                else
                {
                    bReturn = true;
                }
                //    }
                //}
            }

            return bReturn;
        }       

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            //Grid.SetValue(3, chkTodos.Checked);
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                ////sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();
                ////sHost = ((DataRow)cboFarmacias.ItemActual.Item)["Servidor"].ToString();
                //// cboFarmacias.Enabled = false;
            }
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
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
                ActivarControles();
            }

            return bRegresa;
        }
        #endregion Eventos        

        #region Eventos_Farmacia
        private void txtFarmacia_Validating(object sender, CancelEventArgs e)
        {            
            if (txtFarmacia.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacias_UrlsActivas(cboEstados.Data, Fg.PonCeros(txtFarmacia.Text, 4), "txtFarmacia_Validating");

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }

        private void txtFarmacia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Farmacias_UrlsActivas("txtFarmacia_KeyDown", cboEstados.Data);

                if (leer.Leer())
                {
                    txtFarmacia.Text = leer.Campo("IdFarmacia");
                    lblFarmacia.Text = leer.Campo("Farmacia");
                }
                else
                {
                    txtFarmacia.Text = "";
                    lblFarmacia.Text = "";
                    txtFarmacia.Focus();
                }
            }
        }
        #endregion Eventos_Farmacia

    } 
}
