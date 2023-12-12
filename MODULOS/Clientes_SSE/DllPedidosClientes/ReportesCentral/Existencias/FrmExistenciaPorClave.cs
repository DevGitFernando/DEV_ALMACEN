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

using DllPedidosClientes;

namespace DllPedidosClientes.ReportesCentral.Existencias 
{
    public partial class FrmExistenciaPorClave : FrmBaseExt
    {
        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn;
        // clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeerWeb leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayudas;
        // clsGrid Grid;

        //clsExportarExcelPlantilla xpExcel;
        clsListView lst;
        DataSet dtsResultados = new DataSet();
        DataSet dtsFarmacias = new DataSet();
        string sIdEstado = DtGeneralPedidos.EstadoConectado;

        clsDatosCliente DatosCliente;
        wsCnnClienteAdmin.wsCnnClientesAdmin conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        DataSet dtsEstados = new DataSet();
        DataSet dtsClaves = new DataSet();

        DataSet dtsClavesProcesar = new DataSet();

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        bool bSeEjecuto = false;

        // Se declara el objeto de la clase de Auditoria
        clsAuditoria auditoria;

        private enum Cols
        {
            //Se agrega +1 a cada columna para la impresion.
            Ninguno = 0, IdFarmacia = 2, Farmacia = 3, ClaveSSA = 4, Descripcion = 5, TipoClave = 6, ExietenciaVenta = 7,
            ExistenciaConsigna = 8, Existencia = 9
        }

        public FrmExistenciaPorClave()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "");
            conexionWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
            conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, this.Name, "ReportesFacturacionUnidad");
            //leerWeb = new clsLeerWeb(ref cnn, General.Url, General.ArchivoIni, DatosCliente);
            leerWeb = new clsLeerWeb(General.Url, General.ArchivoIni, DatosCliente);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);
            Ayudas = new clsAyudas(General.Url, General.ArchivoIni, DtGeneralPedidos.DatosApp, this.Name, false);

            // Se crea la instancia del objeto de la clase de Auditoria
            auditoria = new clsAuditoria(General.Url, General.ArchivoIni, DtGeneralPedidos.EstadoConectado, DtGeneralPedidos.FarmaciaConectada,
                DtGeneralPedidos.IdPersonal, DtGeneralPedidos.IdSesion, DtGeneralPedidos.Modulo, this.Name, DtGeneralPedidos.Version);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneralPedidos.DatosApp, this.Name);
            //DtGeneralPedidos.FarmaciaConectada = General.EntidadConectada; 


            lst = new clsListView(lstClaves);
            lst.OrdenarColumnas = false;
            //lst.PermitirAjusteDeColumnas = false; 

            ObtenerFarmacias();
            CargarJurisdicciones();
        }

        private void FrmExistenciaPorClave_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
            tmSesion.Enabled = true;
            tmSesion.Start();
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            Fg.IniciaControles(this, true);

            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;

            cboJurisdicciones.Enabled = bValor;
            cboFarmacias.Enabled = false;

            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;
            rdoRptTodos.Checked = true;
            rdoClaveAmbos.Checked = true;

            lst.LimpiarItems();
            cboJurisdicciones.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            IniciarConsulta();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            /*
            // DllTransferenciaSoft.Properties.Resources 
            string sFarmacia = "Del Estado de " + DtGeneralPedidos.EstadoConectadoNombre;
            bool bGenerar = false;
            clsLeer leerToExcel = new clsLeer();

            leerToExcel.DataSetClase = dtsResultados;
            bGenerar = GnPlantillas.GenerarPlantilla(ListaPlantillas.Existencia_Por_ClaveSSA, "CTE_REG_Existencia_Claves");

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                if (cboFarmacias.SelectedIndex != 0)
                {
                    sFarmacia = "De Unidad: " + cboFarmacias.Text;
                }
                else
                {
                    sFarmacia = "De Jurisdicción: " + cboJurisdicciones.Text;
                }
            }

            if (bGenerar)
            {
                xpExcel = new clsExportarExcelPlantilla(GnPlantillas.Documento);
                xpExcel.AgregarMarcaDeTiempo = true;

                int iRow = 9;
                // int iRowInicial = 9; 

                if (xpExcel.PrepararPlantilla())
                {
                    xpExcel.GeneraExcel();
                    leerToExcel.Leer();
                    xpExcel.Agregar("Reporte de Existencias de Claves SSA " + sFarmacia, 3, 2);
                    xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToString(), 6, 2);

                    leerToExcel.RegistroActual = 1;
                    while (leerToExcel.Leer())
                    {
                        xpExcel.Agregar(leerToExcel.Campo("Id Farmacia"), iRow, (int)Cols.IdFarmacia);
                        xpExcel.Agregar(leerToExcel.Campo("Farmacia"), iRow, (int)Cols.Farmacia);
                        xpExcel.Agregar(leerToExcel.Campo("Clave SSA"), iRow, (int)Cols.ClaveSSA);
                        xpExcel.Agregar(leerToExcel.Campo("Descripción"), iRow, (int)Cols.Descripcion);
                        xpExcel.Agregar(leerToExcel.Campo("TipoClave"), iRow, (int)Cols.TipoClave);
                        xpExcel.Agregar(leerToExcel.Campo("Existencia de Venta"), iRow, (int)Cols.ExietenciaVenta);
                        xpExcel.Agregar(leerToExcel.Campo("Existencia de Consignación"), iRow, (int)Cols.ExistenciaConsigna);
                        xpExcel.Agregar(leerToExcel.Campo("Existencia"), iRow, (int)Cols.Existencia);
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
        #endregion Botones

        #region Funciones 
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Exportar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            ActivarFiltros(true);
        }

        private void CargarJurisdicciones()
        {
            if (cboJurisdicciones.NumeroDeItems == 0)
            {
                cboJurisdicciones.Clear();
                // cboJurisdicciones.Add("*", "Todas las jurisdicciones"); 
                cboJurisdicciones.Add("0", "<< TODAS LAS JURISDICCIONES >>");

                cboJurisdicciones.Add(DtGeneralPedidos.Jurisdiscciones, true, "IdJurisdiccion", "NombreJurisdiccion");
            }
            cboJurisdicciones.SelectedIndex = 0;
        }

        private void ObtenerFarmacias()
        {
            dtsFarmacias = new DataSet();
            dtsFarmacias = Consultas.Farmacias(sIdEstado, "ObtenerFarmacias");
            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");
        }

        private void CargarFarmacias()
        {
            string sFiltro = string.Format("Status = 'A' And IdEstado = '{0}' ", sIdEstado);

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< TODAS LAS FARMACIAS >>");

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

        private void ActivarFiltros(bool bActivar)
        {
            cboJurisdicciones.Enabled = bActivar;
            cboFarmacias.Enabled = bActivar;

            rdoInsumosAmbos.Enabled = bActivar;
            rdoInsumoMatCuracion.Enabled = bActivar;
            rdoInsumosMedicamento.Enabled = bActivar;

            rdoTpDispAmbos.Enabled = bActivar;
            rdoTpDispVenta.Enabled = bActivar;
            rdoTpDispConsignacion.Enabled = bActivar;

            rdoRptTodos.Enabled = bActivar;
            rdoRptConExist.Enabled = bActivar;
            rdoRptSinExist.Enabled = bActivar;           
            
            rdoClaveAmbos.Enabled = bActivar;
            rdoClaveCauses.Enabled = bActivar;
            rdoClaveNoCauses.Enabled = bActivar;
        }

        private void IniciarConsulta()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnExportarExcel.Enabled = false;

            //cboJurisdicciones.Enabled = false;
            //cboFarmacias.Enabled = false;

            ActivarFiltros(false);

            lst.LimpiarItems();

            bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);
            //this.Refresh();

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion";
            _workerThread.Start();
        }

        private void ObtenerInformacion()
        {
            bEjecutando = true;
            this.Cursor = Cursors.WaitCursor;
            string sCadena = "", sIdJurisdiccion = "*", sIdFarmacia = "*";
            int iTipoExistencia = 0, iTipoInsumo = 0, iTipoDispensacion = 0, iTipoClave = 0;

            // Se indica la Jurisdiccion
            if (cboJurisdicciones.Data != "0")
            {
                sIdJurisdiccion = cboJurisdicciones.Data;
            }

            // Se indica la farmacia
            if (cboFarmacias.Data != "0")
            {
                sIdFarmacia = cboFarmacias.Data;
            }

            // Se indica el tipo de Existencia
            if (rdoRptConExist.Checked)
            {
                iTipoExistencia = 1;
            }
            if (rdoRptSinExist.Checked)
            {
                iTipoExistencia = 2;
            }

            // Se indica el tipo de dispensacion a mostrar 
            if (rdoTpDispConsignacion.Checked)
            {
                iTipoDispensacion = 1;
            }

            if (rdoTpDispVenta.Checked)
            {
                iTipoDispensacion = 2;
            }

            // Se indica el tipo de producto se mostrar 
            if (rdoInsumosMedicamento.Checked)
            {
                iTipoInsumo = 1;
            }

            if (rdoInsumoMatCuracion.Checked)
            {
                iTipoInsumo = 2;
            }

            // Se indica que tipo de clave que mostrara
            if (rdoClaveCauses.Checked)
            {
                iTipoClave = 1;
            }

            if (rdoClaveNoCauses.Checked)
            {
                iTipoClave = 2;
            }

            string sSql = string.Format("Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
                DtGeneralPedidos.Cliente, DtGeneralPedidos.SubCliente, 
                sIdEstado, sIdJurisdiccion, sIdFarmacia, iTipoExistencia, iTipoInsumo, iTipoDispensacion , iTipoClave );

            lst.LimpiarItems();
            bSeEncontroInformacion = false;

            try
            {
                leerLocal = new clsLeer();
                leerLocal.Reset();
                leerLocal.DataSetClase = GetInformacion(sSql);


                //if (!leer.Exec(sSql))
                if (leerLocal.SeEncontraronErrores())
                {
                    Error.GrabarError(leerLocal, "ObtenerInformacion()");
                    General.msjAviso("No fue posible obtener la información solicitada, intente de nuevo.");
                    ActivarControles();
                }
                else
                {
                    if (!leerLocal.Leer())
                    {
                        bSeEjecuto = true;                        
                        ActivarFiltros(true);
                        //lst.CargarDatos(leerLocal.DataSetClase, true, true);
                        lst.Limpiar();
                        IniciarToolBar(true, true, false);
                        General.msjAviso("No existe información para mostrar bajo los criterios seleccionados.");
                    }
                    else
                    {
                        dtsResultados = leerLocal.DataSetClase;
                        lst.CargarDatos(leerLocal.DataSetClase, true, true);
                        lst.AnchoColumna((int)Cols.Descripcion - 1, 530); //Es -1 debido a que se agrego +1 por la impresion.  
                        ActivarFiltros(true);                       
                        IniciarToolBar(true, false, true);

                        cboFarmacias.Enabled = false;
                        if (cboJurisdicciones.SelectedIndex != 0)
                        {
                            cboFarmacias.Enabled = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Source = ex.Source;
                bSeEjecuto = false;
                bSeEncontroInformacion = false;
            }

            bEjecutando = false;

            sCadena = sSql.Replace("'", "\"");
            auditoria.GuardarAud_MovtosReg(sCadena, General.Url);


            this.Cursor = Cursors.Default;
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            CargarFarmacias();
            IniciarToolBar(true, true, false);

            cboFarmacias.Enabled = true;
            if (cboJurisdicciones.SelectedIndex == 0)
            {
                cboFarmacias.Enabled = false;
            }
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void tmSesion_Tick(object sender, EventArgs e)
        {
            //tmSesion.Enabled = false;
            //if (!VerificarClavesProveedor())
            //{
            //    this.Close();
            //}
        }

        private void rdoInsumosAmbos_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoInsumosMedicamento_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoInsumoMatCuracion_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoTpDispAmbos_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoTpDispVenta_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoTpDispConsignacion_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoRptTodos_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoRptConExist_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoRptSinExist_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoClaveAmbos_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoClaveCauses_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }

        private void rdoClaveNoCauses_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            IniciarToolBar(true, true, false);
        }
        #endregion Funciones 

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

        private DataSet GetInformacionRegional__OLD(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url;
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source;
            }

            return dts;
        }

        private DataSet GetInformacionRegional(string Cadena)
        {
            DataSet dts = new DataSet();

            clsConexionSQL cnnLocal = new clsConexionSQL();
            cnnLocal.DatosConexion.Servidor = cnn.DatosConexion.Servidor;
            cnnLocal.DatosConexion.BaseDeDatos = cnn.DatosConexion.BaseDeDatos;
            cnnLocal.DatosConexion.Usuario = cnn.DatosConexion.Usuario;
            cnnLocal.DatosConexion.Password = cnn.DatosConexion.Password;
            cnnLocal.DatosConexion.Puerto = cnn.DatosConexion.Puerto;
            cnnLocal.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnLocal.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            clsLeer leerRegional = new clsLeer(ref cnnLocal);
            clsLeerWeb leerWeb = new clsLeerWeb(General.Url, DtGeneralPedidos.CfgIniSII_Regional, DatosCliente);

            // leerRegional.Exec(Cadena);
            // dts = leerRegional.DataSetClase;

            leerWeb.Exec(Cadena);
            dts = leerWeb.DataSetClase;

            return dts;
        }

        private DataSet GetInformacionUnidad(string Cadena)
        {
            DataSet dts = new DataSet();

            try
            {
                string sTablaFarmacia = "CteReg_Farmacias_Procesar_Existencia";

                wsCnnClienteAdmin.wsCnnClientesAdmin cnnWeb = new wsCnnClienteAdmin.wsCnnClientesAdmin();
                cnnWeb.Url = General.Url;
                dts = cnnWeb.EjecutarSentencia("", "", Cadena, "reporte", sTablaFarmacia);
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

        private bool validarDatos()
        {
            bool bRegresa = true;

            if (cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = true;
                General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
                cboJurisdicciones.Focus();
            }

            return bRegresa;
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            try
            {
                ////////leerWeb = new clsLeerWeb(sUrl, DtGeneralPedidos.CfgIniPuntoDeVenta, DatosCliente);

                ////////conexionWeb.Url = sUrl;
                ////////DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneralPedidos.CfgIniPuntoDeVenta));

                ////////DatosDeConexion.Servidor = sHost;
                bRegresa = true;
            }
            catch (Exception ex1)
            {
                Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()");
                General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo.");
            }

            return bRegresa;
        }
        #endregion Conexiones

        
        
    }
}
