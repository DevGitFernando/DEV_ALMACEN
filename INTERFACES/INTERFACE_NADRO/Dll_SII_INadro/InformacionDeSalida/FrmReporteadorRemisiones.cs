using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Inventario; 
using DllFarmaciaSoft.Reporteador;

using DllTransferenciaSoft.IntegrarInformacion; 

using Dll_SII_INadro.GenerarArchivos; 

namespace Dll_SII_INadro.InformacionDeSalida
{
    public partial class FrmReporteadorRemisiones : FrmBaseExt 
    {
        ////enum Cols 
        ////{
        ////    IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5  
        ////}

        enum Cols
        {
            IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5, Inicio = 6, Fin = 7, Procesando = 8
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerExcel;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        DllFarmaciaSoft.clsConsultas Consultas;
        DllFarmaciaSoft.clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;        

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerSubFarmacias;
        clsLeer leerProgramas_Catalogo;
        clsLeer leerSubProgramas_Catalogo;
        clsLeer leerProgramas;
        clsLeer leerSubProgramas;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        //////wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet(); 

        string sIdPublicoGeneral =  "0001";

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = ""; 
        bool bFolderDestino = false;

        string sProceso = "";
        int iRenlgonEnProceso = 0;

        public FrmReporteadorRemisiones()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
            //////conexionWeb = new wsFarmacia.wsCnnCliente();
            //////conexionWeb.Url = General.Url;

            CheckForIllegalCrossThreadCalls = false;
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");
            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, DatosCliente);


            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario= General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite; 

            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            leerSubFarmacias = new clsLeer(ref cnn);
            leerProgramas_Catalogo = new clsLeer(ref cnn);
            leerSubProgramas_Catalogo = new clsLeer(ref cnn);
            leerProgramas = new clsLeer(ref cnn); 
            leerSubProgramas = new clsLeer(ref cnn);

            Consultas = new DllFarmaciaSoft.clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new DllFarmaciaSoft.clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            ////lblTitulo_FiltroSubFarmacia.BackColor = General.BackColorBarraMenu; 

            this.Width = 1006;
            //this.Height = 570;

            FrameProceso.Left = 220; 
            FrameProceso.Top = 84;
            MostrarEnProceso(false);

            ////this.Width = 0; 
            ////this.Height = 0; 

            grid = new clsGrid(ref grdUnidades, this); 
            CargarListaReportes();
            CargarQuincenas();
        }

        private void CargarListaReportes()
        {
            ////////cboReporte.Clear();
            ////////cboReporte.Add(); // Agrega Item Default 

            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumos", "Concentrado de Dispensación");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosDesglozado", "Concentrado de Dispensación desglozado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosPrograma", "Concentrado de Dispensación Por Programa");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosProgramaTotalizado", "Concentrado de Dispensación Por Programa Totalizado");  

            ////////cboReporte.Add("PtoVta_Admon_Validacion", "Detallado de Dispensación (Validación)", "");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación no licitado)", "1");
            ////////cboReporte.Add("PtoVta_Admon_Validacion_GruposPrecios", "Detallado de Dispensación (Validación licitado)", "2");


            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecio", "Claves SSA sin precio asignado");
            ////////cboReporte.Add("PtoVta_Admon_ConcentradoInsumosSinPrecioDetallado", "Claves SSA sin precio asignado detallado"); 
            ////////cboReporte.SelectedIndex = 0; 
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 220;
            }
            else
            {
                FrameProceso.Left = this.Width + 100; 
            } 
        }

        private void FrmReporteadorValidaciones_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null); 
        }

        #region Cargar Combos 
        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.SelectedIndex = 0;


            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa");
                sSql = "Select distinct IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase;

                    sSql = "Select distinct IdEstado, IdJurisdiccion, (IdJurisdiccion + ' -- ' + Jurisdiccion) as Jurisdiccion " + 
                        " From vw_Farmacias (NoLock) Order By IdEstado, IdJurisdiccion ";
                    if (!leer.Exec(sSql))
                    { 
                        Error.GrabarError(leer, "CargarEmpresas()");
                        General.msjError("Ocurrió un error al obtener la lista de Jurisdicciones.");
                    }
                    else
                    { 
                        dtsJurisdicciones = leer.DataSetClase; 
                    }
                }

            }
            cboEmpresas.SelectedIndex = 0;
        } 

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A");
            cboEstados.Clear(); 
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado");
            cboEstados.SelectedIndex = 0; 


        }

        private void CargarJurisdicciones()
        {
            string sFiltro = string.Format(" IdEstado = '{0}' ", cboEstados.Data);
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.Add(dtsJurisdicciones.Tables[0].Select(sFiltro), true, "IdJurisdiccion", "Jurisdiccion");
            cboJurisdicciones.SelectedIndex = 0;
        }

        private bool validarConsulta_Farmacias()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una empresa válida, verifique."); 
                cboEmpresas.Focus(); 
            }

            if (bRegresa & cboEstados.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un estado válido, verifique.");
                cboEstados.Focus();
            }

            ////if (bRegresa & cboJurisdicciones.SelectedIndex == 0)
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
            ////    cboJurisdicciones.Focus();
            ////}

            return bRegresa; 
        }

        private void CargarFarmacias()
        {
            bool bRegresa = false; 

            //////sSqlFarmacias = string.Format("Select IdFarmacia, Farmacia, 0, 0 " + 
            //////    " From vw_Farmacias (NoLock) " + 
            //////    "Where IdEstado = '{0}' and IdJurisdiccion = '{1}' and Status = 'A' " , 
            //////    cboEstados.Data, cboJurisdicciones.Data);

            sSqlFarmacias = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}', @EsParaReporteador = '{3}' ", 
                cboEstados.Data, 1, 1, 1);

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                bRegresa = leer.Leer(); 
                grid.LlenarGrid(leer.DataSetClase); 
            }

            IniciarToolBar(true, !bRegresa, bRegresa); 
        }

        private void CargarQuincenas()
        {
            cboQuincena.Clear();
            cboQuincena.Add("0", "Todas");
            cboQuincena.Add("1", "Quincena 1");
            cboQuincena.Add("2", "Quincena 2");
            cboQuincena.SelectedIndex = 0; 
        }

        #endregion Cargar Combos  

        #region Llenar Combo Reporte
        private void LlenarCombo()
        {
        }
        #endregion Llenar Combo Reporte

        #region Buscar Cliente
        private void txtCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtCte.Text.Trim() != "")
            {
                ////leer.DataSetClase = Consultas.Clientes(txtCte.Text, "txtCte_Validating");
                ////leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboJurisdicciones.Data, txtCte.Text.Trim(), "", "txtCte_Validating");
                leer.DataSetClase = Consultas.Clientes(txtCte.Text.Trim(), "txtCte_Validating"); 
                if (leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void CargarDatosCliente()
        {
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
            lblCte.Text = leer.Campo("Nombre");
        }

        private void txtCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                // leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                if (leer.Leer()) 
                {
                    CargarDatosCliente();
                }
            }

        }
        #endregion Buscar Cliente

        #region Buscar SubCliente 
        private void txtSubCte_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubCte.Text != "")
            {
                //leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(),"txtSubCte_Validating");
                //leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboJurisdicciones.Data, txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtCte_Validating");
                leer.DataSetClase = Consultas.SubClientes(txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtSubCte_Validating"); 
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }

        }

        private void CargarDatosSubCliente()
        {
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //leer.DataSetClase = Ayuda.SubClientes_Buscar("txtCte_KeyDown", txtCte.Text.Trim());
                //leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGeneral, false, cboEstados.Data, cboJurisdicciones.Data, txtCte.Text.Trim(), "txtSubCte_KeyDown");
                leer.DataSetClase = Ayuda.SubClientes("txtSubCte_KeyDown", txtCte.Text.Trim());
                if (leer.Leer())
                {
                    CargarDatosSubCliente();
                }
            }
        }
        #endregion Buscar SubCliente

        #region Buscar Programa 
        private void txtPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPro.Text.Trim() != "")
            {
                {
                    //leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating");
                    //leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, cboEstados.Data, "", txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_Validating");

                    leer.DataSetClase = Consultas.Programas(txtPro.Text, "txtPro_Validating"); 
                    if (leer.Leer()) 
                    {
                        CargarDatosProgramas();
                    }
                    else
                    {
                        lblPro.Text = "";
                        txtPro.Focus();
                    }
                }
            }
        }

        private void CargarDatosProgramas()
        {
            txtPro.Text = leer.Campo("IdPrograma");
            lblPro.Text = leer.Campo("Programa");
            lblPro.Text = leer.Campo("Descripcion");
        }

        private void txtPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, cboEstados.Data, "", txtCte.Text.Trim(), txtSubCte.Text.Trim(), "txtPro_KeyDown");

                leer.DataSetClase = Ayuda.Programas("txtPro_KeyDown");
                if (leer.Leer())
                {
                    CargarDatosProgramas();
                }
            }
        }
        #endregion Buscar Programa

        #region Buscar SubPrograma
        private void txtSubPro_Validating(object sender, CancelEventArgs e)
        {
            if (txtSubPro.Text.Trim() != "")
            {
                //leer.DataSetClase = Consultas.SubProgramas(txtSubPro.Text, txtPro.Text, "txtSubPro_Validating");
                //leer.DataSetClase = Consultas.Farmacia_Clientes_Programas(sIdPublicoGeneral, cboEstados.Data, "", txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), txtSubPro.Text.Trim(), "txtSubPro_Validating");

                leer.DataSetClase = Consultas.SubProgramas(txtSubPro.Text, txtPro.Text, "txtSubPro_Validating"); 
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
                else
                {
                    lblSubPro.Text = "";
                    txtSubPro.Focus();
                }
            }
        }

        private void CargarDatosSubProgramas()
        {
            txtSubPro.Text = leer.Campo("IdSubPrograma");
            lblSubPro.Text = leer.Campo("SubPrograma");
            lblSubPro.Text = leer.Campo("Descripcion");
        }

        private void txtSubPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //leer.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", txtPro.Text);
                //leer.DataSetClase = Ayuda.Farmacia_Clientes_Programas(sIdPublicoGeneral, false, cboEstados.Data, "", txtCte.Text.Trim(), txtSubCte.Text.Trim(), txtPro.Text.Trim(), "txtPro_KeyDown");

                leer.DataSetClase = Ayuda.SubProgramas("txtSubPro_KeyDown", txtPro.Text); 
                if (leer.Leer())
                {
                    CargarDatosSubProgramas();
                }
            }
        }
        #endregion Buscar SubPrograma

        #region Impresion  
        private void ObtenerRutaReportes()
        {
            //string sSql = string.Format(" Select ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema " +
            //    " From Net_CFGC_Parametros (NoLock) " +
            //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and NombreParametro = '{2}' ", 
            //    cboEstados.Data, cboFarmacias.Data, "RutaReportes");
            //if (!leerWeb.Exec(sSql))
            //{
            //    Error.GrabarError(leer, "ObtenerRutaReportes");
            //    General.msjError("Ocurrió un error al obtener la Ruta de Reportes de la Farmacia."); 
            //}
            //else
            //{
            //    leerWeb.Leer();
            //    sUrl_RutaReportes = leerWeb.Campo("Valor");     
            //}
        }

        private bool validarImpresion()
        {
            bool bRegresa = true;

            if (!bSeEncontroInformacion)
            {
                bRegresa = false;
                General.msjUser("No existe información para generar el reporte, verifique."); 
            }

            ////////if (bRegresa && cboReporte.SelectedIndex == 0)
            ////////{
            ////////    bRegresa = false;
            ////////    General.msjUser("No ha seleccionado un reporte de la lista, verifique.");
            ////////    cboReporte.Focus(); 
            ////////}

            return bRegresa; 
        }

        private void ImprimirInformacion()
        {
            // int iTipoInsumo = 0;
            // int iTipoDispensacion = 0;
            bool bRegresa = false;  

            if (validarImpresion())
            {
                // El reporte se localiza fisicamente en el Servidor Regional ó Central. 
                // Se utilizan los datos de Conexión de la farmacia seleccionada. 

                DatosCliente.Funcion = "Imprimir()"; 
                clsImprimir myRpt = new clsImprimir(DatosDeConexion);
                // byte[] btReporte = null;

                ////General.Url = "http://lapjesus/wsCompras/wsOficinaCentral.asmx";
                ////DtGeneral.RutaReportes = @"D:\PROYECTO SC-SOFT\SISTEMA_INTERMED\REPORTES"; 

                myRpt.RutaReporte = DtGeneral.RutaReportes;
                myRpt.NombreReporte = ""; // cboReporte.Data + "";

                string sValor = "";
                ////try
                ////{
                ////    sValor = (string)cboReporte.ItemActual.Item;
                ////}
                ////catch { }

                if (sValor == "1" || sValor == "2")
                {
                    myRpt.Add("IdGrupoPrecios", Convert.ToInt32(sValor)); 
                }

                bRegresa = DtGeneral.GenerarReporte(General.Url, true, myRpt, DatosCliente); 

                if (!bRegresa)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
        }
        #endregion Impresion

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bool bValor = true;
            IniciarToolBar(true, true, false);

            iRenlgonEnProceso = 0; 
            sRutaDestino = "";
            bFolderDestino = false; 

            ////btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            cboEmpresas.Enabled = false; 
            cboEstados.Enabled = false;
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            cboJurisdicciones.Enabled = false;
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add();
            cboJurisdicciones.SelectedIndex = 0; 


            grid.Limpiar(); 
            Fg.IniciaControles(this, true); 
            rdoInsumosAmbos.Checked = true;
            rdoTpDispAmbos.Checked = true;
            
            rdoDoctoPDF.Checked = true;
            chkSecuenciar.Checked = true;
            rdoDatos_Historico.Checked = true; 

            ////btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = ""; 

            FrameCliente_old.Enabled = bValor;
            FrameInsumos.Enabled = bValor;
            FrameDispensacion.Enabled = bValor;
            FrameFechas.Enabled = bValor;
            //FrameCliente_old.Enabled = false;

            //chkPrograma_SubPrograma.Visible = false; 
            chkPrograma_SubPrograma.Checked = false; 
            txtPro.Enabled = false;
            txtSubPro.Enabled = false;


            //////sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //////sRutaDestino += @"\DOCUMENTOS_NADRO\REMISIONES\";
            //////lblDirectorioTrabajo.Text = sRutaDestino;
            //////bFolderDestino = true;

            General.FechaSistemaObtener(); 
            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_NADRO\{0}", General.FechaYMD(General.FechaSistema, ""));
            lblDirectorioTrabajo.Text = sRutaDestino;
            bFolderDestino = true; 

            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false; 
            }

            nmCauses.Value = 2014; 
            txtCte.Focus(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (validarConsulta_Farmacias())
            {
                CargarFarmacias();
            }
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                CrearDirectorioDestino(); 
                IniciarProcesamiento(); 
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Imprimir(); 
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.SelectedPath = lblDirectorioTrabajo.Text; 
            folder.ShowNewFolderButton = true;  

            if (folder.ShowDialog() == DialogResult.OK) 
            {
                sRutaDestino = folder.SelectedPath +  @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true; 
            } 

        } 
        #endregion Botones

        #region Eventos
        private void txtCte_TextChanged(object sender, EventArgs e)
        {
            lblCte.Text = ""; 
        }

        private void txtSubCte_TextChanged(object sender, EventArgs e)
        {
            lblSubCte.Text = "";
            //grid.Limpiar();
        }

        private void txtPro_TextChanged(object sender, EventArgs e)
        {
            lblPro.Text = "";
            grid.Limpiar();
        }

        private void txtSubPro_TextChanged(object sender, EventArgs e)
        {
            lblSubPro.Text = "";
            grid.Limpiar();
        } 

        #endregion Eventos

        #region Procesar Informacion  
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar; 
        }

        private bool validarDatosDeConexion()
        {
            bool bRegresa = false;

            //////try
            //////{
            //////    leerWeb = new clsLeerWebExt(sUrl, DtGeneral.CfgIniPuntoDeVenta, DatosCliente);

            //////    conexionWeb.Url = sUrl;
            //////    DatosDeConexion = new clsDatosConexion(conexionWeb.ConexionEx(DtGeneral.CfgIniPuntoDeVenta));

            //////    DatosDeConexion.Servidor = sHost;
            //////    bRegresa = true; 
            //////}
            //////catch (Exception ex1)
            //////{
            //////    Error.GrabarError(leer.DatosConexion, ex1, "validarDatosDeConexion()"); 
            //////    General.msjAviso("No fue posible establecer conexión con la Unidad, intente de nuevo."); 
            //////    ActivarControles(); 
            //////}

            return bRegresa; 
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = false;
            
            bRegresa = validarConsulta_Farmacias();

            if (bRegresa)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique."); 
                }
            } 

            if ( bRegresa & Fg.PonCeros(txtCte.Text, 4) == "0000" )
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Cliente válido, verifique.");
                txtCte.Focus(); 
            }

            if (bRegresa & Fg.PonCeros(txtSubCte.Text, 4) == "0000")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Sub-Cliente válido, verifique.");
                txtSubCte.Focus();
            }

            //////if (bRegresa && !ObtenerProgramas_Catalogo())
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("Ocurrio un error al obtener la lista de Programas.");
            //////}

            //////if (bRegresa && !ObtenerSubProgramas_Catalogo())
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("Ocurrio un error al obtener la lista de Sub-Programas.");
            //////}
            

            return bRegresa; 
        }

        private void CrearDirectorioDestino()
        {
            string sDir = "000__GENERAL"; //// cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            string sMarcaTiempo = "";

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                sDir = cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            }

            sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema); 
            sRutaDestino_Archivos = Path.Combine(sRutaDestino, sDir) + "____" + sMarcaTiempo;

            if (!Directory.Exists(sRutaDestino_Archivos)) 
            {
                Directory.CreateDirectory(sRutaDestino_Archivos);
            }
        }

        private bool ObtenerSubFarmacias()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select IdSubFarmacia From CatEstados_SubFarmacias (NoLock) " +
                " Where IdEstado = '{0}' Order By IdSubFarmacia ", cboEstados.Data); 

            bRegresa = leerSubFarmacias.Exec(sSql); 

            return bRegresa; 
        }

        private bool ObtenerProgramas_Catalogo()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select Distinct IdFarmacia, IdPrograma " + 
                " From CFG_EstadosFarmaciasProgramasSubProgramas(NoLock) " +
                " Where IdEstado = '{0}' And IdCliente = '{1}' Order By IdFarmacia, IdPrograma ",
                cboEstados.Data, txtCte.Text.Trim());

            bRegresa = leerProgramas_Catalogo.Exec(sSql);

            return bRegresa;
        }

        private bool ObtenerSubProgramas_Catalogo()
        {
            bool bRegresa = false;
            string sSql = string.Format("Select Distinct IdFarmacia, IdPrograma, IdSubPrograma " + 
                " From CFG_EstadosFarmaciasProgramasSubProgramas(NoLock) " +
                " Where IdEstado = '{0}' And IdCliente = '{1}' Order By IdFarmacia, IdSubPrograma ",
                cboEstados.Data, txtCte.Text.Trim());

            bRegresa = leerSubProgramas_Catalogo.Exec(sSql);

            return bRegresa;
        }

        private void ObtenerProgramas_Farmacia(string sIdFarmacia)
        {
            DataTable dtTabla;
            string sFiltro = string.Format("IdFarmacia = '{0}'", sIdFarmacia);
            dtsProgramas = new DataSet();
            dtTabla = new DataTable();

            leerProgramas = new clsLeer();
            try
            {
                if (chkPrograma_SubPrograma.Checked)
                {
                    sFiltro += string.Format("  and IdPrograma = '{0}' ", Fg.PonCeros(txtPro.Text.Trim(), 4));
                }

                dtTabla = leerProgramas_Catalogo.DataTableClase.Clone();

                foreach (DataRow dr in leerProgramas_Catalogo.DataTableClase.Select(sFiltro))
                {
                    dtTabla.ImportRow(dr);
                }
                dtsProgramas.Tables.Add(dtTabla);
                leerProgramas.DataSetClase = dtsProgramas;

            }
            catch { }

        } 

        private void ObtenerSubProgramas_Farmacia( string sIdFarmacia, string sIdPrograma )
        {
            DataTable dtTabla;
            string sFiltro = string.Format("IdFarmacia = '{0}' And IdPrograma = '{1}'", sIdFarmacia, sIdPrograma);
            dtsSubProgramas = new DataSet();
            dtTabla = new DataTable();

            leerSubProgramas = new clsLeer();
            try
            {
                if (chkPrograma_SubPrograma.Checked)
                {
                    sFiltro += string.Format("  and IdSubPrograma = '{0}' ", Fg.PonCeros(txtSubPro.Text.Trim(), 4));
                }

                dtTabla = leerSubProgramas_Catalogo.DataTableClase.Clone();

                foreach (DataRow dr in leerSubProgramas_Catalogo.DataTableClase.Select(sFiltro))
                {
                    dtTabla.ImportRow(dr);
                }
                dtsSubProgramas.Tables.Add(dtTabla);
                leerSubProgramas.DataSetClase = dtsSubProgramas;
                
            }
            catch { }

        } 

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            ////btnImprimir.Enabled = false;
            ////btnExportarExcel.Enabled = false;

            cboEmpresas.Enabled = false;
            cboEstados.Enabled = false; 
            cboJurisdicciones.Enabled = false;


            // bloqueo principal 
            IniciarToolBar(false, false, false); 
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            grid.SetValue((int)Cols.Inicio, "");
            grid.SetValue((int)Cols.Fin, "");
            grid.SetValue((int)Cols.Procesando, ""); 

            MostrarEnProceso(true);

            txtCte.Enabled = false;
            txtSubCte.Enabled = false; 
            btnDirectorio.Enabled = false; 
            FrameInsumos.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            FrameSubFarmacias.Enabled = false;
            chkPdfConcentrado.Enabled = false; 


            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.SetValue((int)Cols.Procesado, 0); 

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion"; 
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private string ObtenerMarcaDeTiempo()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;

            sRegresa = string.Format("{0}:{1}:{2}", Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2));

            return sRegresa;
        }

        private void ObtenerInformacion()
        {
            RemisionesUnidades rm = new RemisionesUnidades(); 
            string sIdEmpresa = cboEmpresas.Data;
            string sIdEstado = cboEstados.Data;
            string sIdJurisdiccion = cboJurisdicciones.Data;
            string sIdFarmacia = "";
            string sFarmacia = "";
            string sCliente = "";
            string sIdPrograma = txtPro.Text.Trim();
            string sIdSubPrograma = txtSubPro.Text.Trim();
            int iTipoDeDispensacion = 0;
            int iTipoDeInsumo = 0;
            int iTipoDeDocumento = 0;
            bool bPdf_Concentrado = chkPdfConcentrado.Checked; 


            int iMostrarPrecios = chkMostrarPrecios.Checked ? 1 : 0;
            int iDiasMes = General.FechaDiasMes(dtpFechaInicial.Value);
            string sMarcaTiempo = "";
            bool bOrigenHistorico = rdoDatos_Historico.Checked;
            bool bSeparar_x_Causes = chkSepararPorCauses.Checked;
            bool bSecuenciar = chkSecuenciar.Checked;
            bool bImprimirResguardo = chkImprimirEnResguardo.Checked;
            bool bProcesar_x_Dia = chkTipoEjecucion.Checked;
            bool bConsolidarMeses = chkConsolidarMeses.Checked;


            iRenlgonEnProceso = 0;
            lblFechaEnProceso.Text = ""; 


            if (rdoDoctoPDF.Checked)
            {
                iTipoDeDocumento = 1;
            }

            if (rdoDoctoExcel.Checked)
            {
                iTipoDeDocumento = 2;
            }


            if (chkSecuenciar.Checked)
            {
                GnDll_SII_INadro.Consecutivo_Docuemento_Generado = (int)nmNumerador.Value;
            }


            bEjecutando = true;
            rm.GenerarDirectorio_Farmacia = true; 
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sMarcaTiempo = ObtenerMarcaDeTiempo();
                    grid.SetValue(i, (int)Cols.Inicio, sMarcaTiempo); 


                    iRenlgonEnProceso = i; 
                    sIdFarmacia = Fg.PonCeros(grid.GetValue(i, (int)Cols.IdFarmacia), 4);
                    sFarmacia = grid.GetValue(i, (int)Cols.Farmacia);
                    sCliente = grid.GetValue(i, (int)Cols.Cliente);

                    rm.GUID = Guid.NewGuid().ToString();
                    rm.EtiquetaFechaEnProceso = lblFechaEnProceso;
                    rm.Causes = (int)nmCauses.Value; 
                    rm.RutaDestinoReportes = sRutaDestino;
                    rm.IdCliente = txtCte.Text.Trim();
                    rm.IdSubCliente = txtSubCte.Text.Trim(); 
                    rm.IdPrograma = sIdPrograma;
                    rm.IdSubPrograma = sIdSubPrograma;
                    rm.SubFarmacias = sSubFarmacias; 
                    rm.TipoDeDispensacion = iTipoDeDispensacion;
                    rm.TipoDeInsumo = iTipoDeInsumo;
                    rm.TipoDeDocumento_A_Generar = iTipoDeDocumento;
                    rm.Pdf_Concentrado = bPdf_Concentrado;

                    rm.SecuenciarDocumentos = bSecuenciar;
                    rm.GenerarDoctos_EnResguardo = bImprimirResguardo;
                    rm.Procesar_x_Dia = bProcesar_x_Dia;
                    rm.GenerarHistorico = bOrigenHistorico;
                    rm.Separar_x_Causes = bSeparar_x_Causes;
                    rm.ConsolidarMeses = bConsolidarMeses; 

                    rm.GenerarRemisiones(sIdFarmacia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

                    grid.SetValue(i, (int)Cols.Procesado, true);
                    sMarcaTiempo = ObtenerMarcaDeTiempo();
                    grid.SetValue(i, (int)Cols.Fin, sMarcaTiempo); 
                }
            }

            //// Regresar el ultimo valor generado 
            if (chkSecuenciar.Checked)
            {
                nmNumerador.Value = (decimal)GnDll_SII_INadro.Consecutivo_Docuemento_Generado;
            }


            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);
            MostrarEnProceso(false);
        }
        #endregion Procesar Informacion 

        #region Funciones y Procedimientos Privados 
        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            ////btnExportarExcel.Enabled = false;
            txtCte.Enabled = true;
            txtCte.Enabled = true; 
            btnDirectorio.Enabled = true; 

            FrameInsumos.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameSubFarmacias.Enabled = true;
            chkPdfConcentrado.Enabled = true;
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando) 
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false; 

                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                ////btnExportarExcel.Enabled = true;
                ActivarControles(); 

                ////////if (!bSeEncontroInformacion) 
                ////////{
                ////////    _workerThread.Interrupt(); 
                ////////    _workerThread = null; 

                ////////    ActivarControles();

                ////////   //////if (bSeEjecuto)
                ////////   ////// {
                ////////   //////     General.msjUser("No existe informacion para mostrar bajo los criterios seleccionados.");
                ////////   ////// } 
                ////////}
            }
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstados.Clear();
            cboEstados.Add();
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add(); 

            if (cboEmpresas.SelectedIndex != 0)
            {
                ////cboEmpresas.Enabled = false;
                ////cboEstados.Enabled = true;
                CargarEstados();
            }

            cboEstados.SelectedIndex = 0;
            cboJurisdicciones.SelectedIndex = 0; 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add(); 

            if (cboEstados.SelectedIndex != 0)
            {
                ////cboEstados.Enabled = false;
                ////cboJurisdicciones.Enabled = true;
                CargarJurisdicciones();
            } 
        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboJurisdicciones.SelectedIndex != 0)
            {
                // CargarFarmacias(); 
            }
        }

        private void FrmReporteadorValidaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                CargarSubFarmacias();
            }
        }

        private void CargarSubFarmacias()
        {
            if (lblTitulo_FiltroSubFarmacia.Visible)
            {
                if (!bEjecutando)
                {
                    SubFarmacias = new FrmListaDeSubFarmacias(cboEstados.Data, cboJurisdicciones.Data);
                    SubFarmacias.AliasTabla = "L.";
                    SubFarmacias.Estado = cboEstados.Data;
                    //SubFarmacias.Farmacia = cboJurisdicciones.Data;
                    SubFarmacias.Farmacia = "0182";
                    SubFarmacias.EsParaSP = true;
                    SubFarmacias.MostrarDetalle();
                    sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int iRow = 2;
            string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + cboJurisdicciones.Data + ".xls";
            string sPeriodo = "";

            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                xpExcel.AgregarMarcaDeTiempo = false;

                if (xpExcel.PrepararPlantilla(sNombreFile))
                {
                    xpExcel.GeneraExcel();

                    //Se pone el encabezado
                    leerExportarExcel.RegistroActual = 1;
                    leerExportarExcel.Leer();
                    xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
                    iRow++;
                    xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
                    iRow++;

                    sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
                        General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
                    xpExcel.Agregar(sPeriodo, iRow, 2);

                    iRow = 6;
                    xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

                    // Se ponen los detalles
                    leerExportarExcel.RegistroActual = 1;
                    iRow = 9;
                    while (leerExportarExcel.Leer())
                    {
                        xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
                        xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
                        xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
                        xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
                        xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
                        xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
                        xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
                        xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
                        xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
                        xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
                        xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
                        xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
                        xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
                        xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
                        xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

                        iRow++;
                    }

                    // Finalizar el Proceso 
                    xpExcel.CerrarDocumento();

                    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    {
                        xpExcel.AbrirDocumentoGenerado();
                    }
                }
                this.Cursor = Cursors.Default; 
            }
        } 
        #endregion Funciones y Procedimientos Privados

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
        }

        private void chkPrograma_SubPrograma_CheckedChanged(object sender, EventArgs e)
        {
            txtPro.Text = "";
            txtSubPro.Text = "";

            txtPro.Enabled = chkPrograma_SubPrograma.Checked;
            txtSubPro.Enabled = chkPrograma_SubPrograma.Checked;

            if (txtPro.Enabled)
            {
                txtPro.Focus(); 
            }
        }

        private void chkPrograma_SubPrograma_EnabledChanged(object sender, EventArgs e)
        {
            txtPro.Enabled = chkPrograma_SubPrograma.Enabled;
            txtSubPro.Enabled = chkPrograma_SubPrograma.Enabled;
        }

        private void btnIntegrarPaquetesDeDatos_Click(object sender, EventArgs e)
        {
            ////FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            ////f.ShowDialog(this); 
        }

        private void lblFechaEnProceso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                grid.SetValue(iRenlgonEnProceso, (int)Cols.Procesando, lblFechaEnProceso.Text);
            }
            catch
            {
            }
        }

        private void rdoDatos_Generar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDatos_Generar.Checked)
            {
                chkConsolidarMeses.Checked = false;
                chkConsolidarMeses.Enabled = false;
                lblTitulo_FiltroSubFarmacia.Visible = true; 
            }
        }

        private void rdoDatos_Historico_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDatos_Historico.Checked)
            {
                chkConsolidarMeses.Enabled = true;
                lblTitulo_FiltroSubFarmacia.Visible = false; 
            }
        }
    } 
}
