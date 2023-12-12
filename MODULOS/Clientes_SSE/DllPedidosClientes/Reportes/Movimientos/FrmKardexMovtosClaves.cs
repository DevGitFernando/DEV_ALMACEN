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
    public partial class FrmKardexMovtosClaves : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion;
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal; 
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas; 
        clsGrid grid;
        DataSet dtsFarmacias = new DataSet();

        // string sSqlFarmacias = "";
        string sUrl = "";
        string sHost = "";
        string sIdEstado = DtGeneralPedidos.EstadoConectado;
        string sTablaFarmacia = "";

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;
                        
        DataSet dtsEstados = new DataSet(); 

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Clase de Auditoria de Movimientos
        clsAuditoria auditoria;

        public FrmKardexMovtosClaves()
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
            grid = new clsGrid(ref grdMovtos, this);
            // DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada;

            ObtenerFarmacias();
            ObtenerJurisdicciones();
        }


        private void FrmExistenciaFarmacias_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }

        #region Cargar Combos         
        private void ObtenerJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("*", "<< TODAS >>");
            cboJurisdicciones.Add(Consultas.Jurisdicciones(sIdEstado, "ObtenerTipoUnidades"), true, "IdJurisdiccion", "NombreJurisdiccion");
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias(sIdEstado, "ObtenerFarmacias");
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<< TODAS >>");
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("IdEstado = '{0}' ", sIdEstado);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS >>");

            if (cboJurisdicciones.SelectedIndex > 0)
            {
                sFiltro = sFiltro + string.Format(" And IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            }

            try
            {
                cboFarmacias.Add(dtsFarmacias.Tables[0].Select(sFiltro, "NombreFarmacia"), true, "IdFarmacia", "NombreFarmacia");
            }
            catch { }


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
                string sFarmacia = cboFarmacias.Data;

                //// Linea Para Prueba
                //DtGeneralPedidos.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES";
                if (DtGeneralPedidos.TipoDeConexion == TipoDeConexion.Unidad_Directo)
                {
                    myRpt = new clsImprimir(DatosDeConexion);
                }

                myRpt.RutaReporte = DtGeneralPedidos.RutaReportes;
                myRpt.NombreReporte = "RptCte_Reg_KardexClave";

                ////////// myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada); 
                //////myRpt.Add("@IdEstado", cboEstados.Data);
                //////myRpt.Add("@IdFarmacia", txtFarmacia.Text);
                //////myRpt.Add("@ClaveSSA", txtCodigo.Text);
                //////myRpt.Add("@FechaInicial", General.FechaYMD(dtpFechaInicial.Value));
                //////myRpt.Add("@FechaFinal", General.FechaYMD(dtpFechaFinal.Value));

                myRpt.Add("@IdEstado", DtGeneralPedidos.EstadoConectado);
                myRpt.Add("@IdJurisdiccion", cboJurisdicciones.Data);
                myRpt.Add("@IdFarmacia", cboFarmacias.Data);
                myRpt.Add("@ClaveSSA", txtCodigo.Text);
                myRpt.Add("@Año", Fg.PonCeros(dtpFechaInicial.Value.Year, 4) );
                myRpt.Add("@Mes", Fg.PonCeros(dtpFechaInicial.Value.Month, 2));

                // sIdEstado, sJurisdiccion, sFarmacia, txtCodigo.Text.Trim(), iAño, iMes); 

                DataSet InfoWeb = myRpt.ObtenerInformacionWeb(); 
                DataSet datosC = DatosCliente.DatosCliente();

                bRegresa = DtGeneralPedidos.GenerarReporte(General.Url, myRpt, sIdEstado, sFarmacia, InfoWeb, datosC); 
                     
               
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

            grid.Limpiar(false);
            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;             
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (DtGeneralPedidos.MensajeProceso() == DialogResult.Yes)
                {
                    IniciarProcesamiento();
                }
            }
        }

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            cboJurisdicciones.Enabled = false;
            cboFarmacias.Enabled = false;
            txtCodigo.Enabled = false;
            dtpFechaInicial.Enabled = false;

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

        #region Conexiones
        public DataSet GetInformacion(string Cadena)
        {
            DataSet dts = new DataSet();
            // DtGeneralPedidos.TipoDeConexion = TipoDeConexion.Unidad_Directo; 

            dts = GetInformacionRegional(Cadena);

            ////switch (DtGeneralPedidos.TipoDeConexion)
            ////{
            ////    case TipoDeConexion.Regional:
            ////        dts = GetInformacionRegional(Cadena);
            ////        break;

            ////    case TipoDeConexion.Unidad:
            ////        dts = GetInformacionUnidad(Cadena);
            ////        break;

            ////    case TipoDeConexion.Unidad_Directo:
            ////        dts = GetInformacionUnidad_Directo(Cadena);
            ////        break;

            ////    default:
            ////        break;
            ////}

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            leer.Exec(Cadena);
            dts = leer.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                conexionWeb.Url = General.Url;
                dts = conexionWeb.EjecutarSentencia(sIdEstado, cboFarmacias.Data, Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }

            return dts;
        }

        private DataSet GetInformacionUnidad_Directo(string Cadena)
        {
            DataSet dts = new DataSet();

            if (validarDatosDeConexion())
            {
                clsConexionSQL cnnRemota = new clsConexionSQL(DatosDeConexion);
                cnnRemota.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnnRemota.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerDatos = new clsLeer(ref cnnRemota);

                leerDatos.Exec(Cadena);
                dts = leerDatos.DataSetClase;
            }

            return dts;
        }
        #endregion Conexiones

        #region Grid        
        private void ObtenerInformacion()
        {
            string sCadena = "";
            string sJurisdiccion = "*", sFarmacia = "*";
            int iAño = dtpFechaInicial.Value.Year;
            int iMes = dtpFechaInicial.Value.Month;

            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;          

            // int iOpcion = 0;
            if (cboJurisdicciones.Data != "0")
            {
                sJurisdiccion = cboJurisdicciones.Data;
            }

            if (cboFarmacias.Data != "0")
            {
                sFarmacia = cboFarmacias.Data;
            }

            grid.Limpiar(false); 
            sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia"; 

            string[] sColumnas = { "FechaRegistro", "Entradas", "Salidas", "Existencia" };
            string sSql = string.Format("Exec spp_Rpt_CteReg_Kardex_Por_Clave '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'  ",
                sIdEstado, sJurisdiccion, sFarmacia, txtCodigo.Text.Trim(), 
                iAño, iMes); 

            ////try
            ////{
            ////    leer.Reset();
            ////    leer.DataSetClase = conexionWeb.EjecutarSentencia(cboEstados.Data, txtFarmacia.Text, sSql, "reporte", sTablaFarmacia); 
            ////}
            ////catch (Exception ex)
            ////{ 
            ////} 

            leer.Reset();
            leer.DataSetClase = GetInformacion(sSql); 
            if (leer.SeEncontraronErrores())
            {
                btnImprimir.Enabled = false;
                bSeEncontroInformacion = false;
                Error.GrabarError(leer, "ObtenerInformacion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad solicitada, intente de nuevo."); 
            }
            else
            {

                btnImprimir.Enabled = false;
                bSeEncontroInformacion = false;
                if (!leer.Leer())
                {
                    General.msjUser("No se encontro información con los criterios especificados"); 
                    bSeEjecuto = true;
                }
                else
                {
                    btnImprimir.Enabled = true;
                    bSeEncontroInformacion = true; 

                    leer.FiltrarColumnas(1, sColumnas); 
                    grid.LlenarGrid(leer.DataSetClase);
                }
            }

            bEjecutando = false;

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosReg(sCadena, General.Url);

            ////if (validarDatosDeConexion())
            ////{
            ////    cnnUnidad = new clsConexionSQL(DatosDeConexion);
            ////    cnnUnidad.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;
            ////    cnnUnidad.TiempoDeEsperaConexion = TiempoDeEspera.Limite300;

            ////    leer = new clsLeer(ref cnnUnidad);

            ////    if (FarmaciasAProcesar())
            ////    {
            ////        if (!leer.Exec(sSql))
            ////        {
            ////            Error.GrabarError(leer, "ObtenerInformacion()");
            ////            General.msjError("Ocurrió un error al obtener la información del reporte.");
            ////            bSeEncontroInformacion = false;
            ////        }
            ////        else
            ////        {
            ////            if (leer.Leer())
            ////            {                        
            ////                btnImprimir.Enabled = true;
            ////                bSeEncontroInformacion = true;
            ////            }
            ////            else
            ////            {
            ////                bSeEncontroInformacion = false;                        
            ////            }

            ////            sCadena = sSql.Replace("'", "\"");
            ////            auditoria.GuardarAud_MovtosReg(sCadena, sUrl);
            ////        }

            ////        bSeEjecuto = true;
            ////        bEjecutando = false;
            ////    }
            ////}
            this.Cursor = Cursors.Default;
        } 

        #endregion Grid 

        #region Eventos
        private void ActivarControles()
        {
            this.Cursor = Cursors.Default; 
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            cboJurisdicciones.Enabled = true;
            cboFarmacias.Enabled = true;
            txtCodigo.Enabled = true;
            dtpFechaInicial.Enabled = true;
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

        private bool FarmaciasAProcesar()
        {
            bool bReturn = false;
            string sIdFarmacia = "";

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
                sIdFarmacia = cboFarmacias.Data; //Grid.GetValue(i,1);

                string sQuery = string.Format(" Insert Into CteReg_Farmacias_Procesar_Existencia " +
                                    " Select '{0}','{1}','A',0 ", sIdEstado, sIdFarmacia);
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

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFarmacias();
        }

        #endregion Eventos        

        #region Funciones 
        private bool ValidarDatos()
        {
            bool bRegresa = true;

            if (lblDescripcion.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("Clave SSA invalida. Verifique");
                txtCodigo.Focus();
            }

            return bRegresa;
        }
        #endregion Funciones 
        #region Claves
        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodigo.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtCodigo.Text, true, "txtCodigo_Validating");
                if (!leer.Leer())
                {
                    lblDescripcion.Text = "";
                    grid.Limpiar(false);
                    txtCodigo.Focus();
                }
                else
                {
                    lblDescripcion.Text = leer.Campo("DescripcionClave");
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtCodigo_KeyDown");
                if (leer.Leer())
                {
                    txtCodigo.Text = leer.Campo("ClaveSSA");
                    lblDescripcion.Text = leer.Campo("DescripcionClave");
                }
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
            grid.Limpiar(false);
        }
        #endregion Claves         

    } 
}
