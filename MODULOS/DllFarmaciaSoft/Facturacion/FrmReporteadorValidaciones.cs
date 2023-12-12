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
using DllFarmaciaSoft.ExportarExcel;  

namespace DllFarmaciaSoft.Facturacion
{
    public partial class FrmReporteadorValidaciones : FrmBaseExt 
    {
        enum Cols 
        {
            IdFarmacia = 1, Farmacia = 2, Procesar = 3, Procesado = 4 
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion(); 
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;  
        clsLeer leer;
        clsLeer leerExcel;
        clsLeer leerLocal; 
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;        

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;
        clsLeer leerSubFarmacias;
        clsLeer leerProgramas_Catalogo;
        clsLeer leerSubProgramas_Catalogo;
        clsLeer leerProgramas;
        clsLeer leerSubProgramas;
        clsLeer leerPerfil;

        string sSqlFarmacias = "";
        // string sUrl;
        // string sHost = "";
        // string sUrl_RutaReportes = "";
        string sSubFarmacias = "";

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
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

        int iExcelConcentrado = 0;
        int iFormatoExcel = 1; 

        public FrmReporteadorValidaciones()
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
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
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
            leerPerfil = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            //this.Width = 710;
            //this.Height = 570;

            FrameProceso.Left = 67; 
            FrameProceso.Top = 126;


            FrameProceso.Left = FrameDirectorioDeTrabajo.Left;
            FrameProceso.Top = FrameDirectorioDeTrabajo.Top;

            FrameProceso.Left = 0;
            FrameProceso.Top = 0; 
            FrameProceso.Width = FrameUnidades.Width;
            //FrameProceso.Height = FrameDirectorioDeTrabajo.Height;


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
                //FrameProceso.Left = 67;
                FrameProceso.Left = 0;
                FrameProceso.BringToFront(); 
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

            if (bRegresa & cboJurisdicciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una jurisdicción válida, verifique.");
                cboJurisdicciones.Focus();
            }

            return bRegresa; 
        }

        private void CargarFarmacias()
        {
            bool bRegresa = false; 

            sSqlFarmacias = string.Format("Select IdFarmacia, Farmacia, 0, 0 " + 
                " From vw_Farmacias (NoLock) " + 
                "Where IdEstado = '{0}' and IdJurisdiccion = '{1}' and Status = 'A' " , 
                cboEstados.Data, cboJurisdicciones.Data);  

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
                leer.DataSetClase = Consultas.SubProgramas(txtPro.Text, txtSubPro.Text, "txtSubPro_Validating"); 
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

                //////if (General.ImpresionViaWeb)
                ////{
                ////    DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
                ////    DataSet datosC = DatosCliente.DatosCliente();

                ////    conexionWeb.Url = General.Url;
                ////    conexionWeb.Timeout = 300000;
                ////    //////myRpt.CargarReporte(true); 

                ////    btReporte = conexionWeb.Reporte(InfoWeb, datosC);
                ////    bRegresa = myRpt.CargarReporte(btReporte, General.UnidadSO + @":\", "Reporte.rpt", true);
                ////}
                //////else
                //////{
                //////    myRpt.CargarReporte(true);
                //////    bRegresa = !myRpt.ErrorAlGenerar;
                //////}

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

            sRutaDestino = "";
            bFolderDestino = false; 

            btnExportarExcel.Enabled = false;
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
            rdoDoctoAmbos.Checked = true;
            rdoFormatoExcel_01.Checked = true; 

            btnImprimir.Enabled = false;
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


            if (!DtGeneral.EsAdministrador)
            {
                cboEmpresas.Data = DtGeneral.EmpresaConectada;
                cboEstados.Data = DtGeneral.EstadoConectado;

                cboEmpresas.Enabled = false;
                cboEstados.Enabled = false; 
            } 

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

            ////if (bRegresa && !ObtenerSubFarmacias())
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("Ocurrió un error al obtener la lista de Sub-Farmacias."); 
            ////}

            if (bRegresa && !ObtenerProgramas_Catalogo())
            {
                bRegresa = false;
                General.msjUser("Ocurrió un error al obtener la lista de Programas.");
            }

            if (bRegresa && !ObtenerSubProgramas_Catalogo())
            {
                bRegresa = false;
                General.msjUser("Ocurrió un error al obtener la lista de Sub-Programas.");
            }
            

            return bRegresa; 
        }

        private void CrearDirectorioDestino()
        {
            string sDir = cboJurisdicciones.Data + "__" + Fg.Mid(cboJurisdicciones.Text, 8);
            string sMarcaTiempo = General.FechaSinDelimitadores(General.FechaSistema); 
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
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;

            cboEmpresas.Enabled = false;
            cboEstados.Enabled = false; 
            cboJurisdicciones.Enabled = false;


            // bloqueo principal 
            IniciarToolBar(false, false, false); 
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            iExcelConcentrado = chkExcel_Concentrado.Checked ? 1 : 0;

            MostrarEnProceso(true);

            txtCte.Enabled = false;
            txtSubCte.Enabled = false; 
            btnDirectorio.Enabled = false; 
            FrameInsumos.Enabled = false;
            FrameDispensacion.Enabled = false;
            FrameFechas.Enabled = false;
            FrameSubFarmacias.Enabled = false;
            FrameTipoDocoto.Enabled = false;
            FrameFormatosExcel.Enabled = false; 
            chkPrograma_SubPrograma.Enabled = false;
            chkMarcarDesmarcar.Enabled = false; 

            // bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();


            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoValidacion"; 
            _workerThread.Start();
            // LlenarGrid(); 
        }

        private void ObtenerInformacion()
        {
            string sIdEmpresa = cboEmpresas.Data;
            string sIdEstado = cboEstados.Data;
            string sIdJurisdiccion = cboJurisdicciones.Data;
            string sIdFarmacia = "";
            string sFarmacia = "";
            string sIdPrograma = "", sIdSubPrograma = "";
            int iMostrarPrecios = chkMostrarPrecios.Checked ? 1 : 0;
            int iMostrarDevoluciones = chkMostrarDevoluciones.Checked ? 1 : 0;

            // int iConsigna = 1;
            // int iVenta = 2; 

            // int iMED = 1;
            // int iMC = 2;

            int iDiasMes = General.FechaDiasMes(dtpFechaInicial.Value);
            string FechaInicial = string.Format("{0}-{1}-01", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));
            string FechaFinal = string.Format("{0}-{1}-15", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));

            string FechaInicial_02 = string.Format("{0}-{1}-16", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));
            string FechaFinal_02 = string.Format("{0}-{1}-{2}", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2), Fg.PonCeros(iDiasMes, 2));

            iFormatoExcel = rdoFormatoExcel_01.Checked ? 1 : 2; 
            bEjecutando = true;
            for (int i = 1; i <= grid.Rows; i++)
            {
                if (grid.GetValueBool(i, (int)Cols.Procesar))
                {
                    sIdFarmacia = Fg.PonCeros(grid.GetValue(i, (int)Cols.IdFarmacia), 4);
                    sFarmacia = grid.GetValue(i, (int)Cols.Farmacia);

                    ObtenerProgramas_Farmacia(sIdFarmacia);
                    leerProgramas.RegistroActual = 1;

                    ////Se procesa cada Programa
                    while (leerProgramas.Leer())
                    {
                        sIdPrograma = leerProgramas.Campo("IdPrograma");
                        ObtenerSubProgramas_Farmacia(sIdFarmacia, sIdPrograma);
                        leerSubProgramas.RegistroActual = 1;

                        //// Se procesa cada Sub-Programa
                        while (leerSubProgramas.Leer())
                        {
                            sIdSubPrograma = leerSubProgramas.Campo("IdSubPrograma");


                            if (rdoTpDispVenta.Checked)
                            {
                                ObtenerVenta(sIdFarmacia, sFarmacia, sIdPrograma, sIdSubPrograma, iMostrarPrecios, iMostrarDevoluciones);
                            }
                            else if (rdoTpDispConsignacion.Checked)
                            {
                                ObtenerConsignacion(sIdFarmacia, sFarmacia, sIdPrograma, sIdSubPrograma, iMostrarPrecios, iMostrarDevoluciones);
                            }
                            else
                            {
                                ObtenerVenta(sIdFarmacia, sFarmacia, sIdPrograma, sIdSubPrograma, iMostrarPrecios, iMostrarDevoluciones);
                                ObtenerConsignacion(sIdFarmacia, sFarmacia, sIdPrograma, sIdSubPrograma, iMostrarPrecios, iMostrarDevoluciones);
                            }


                        }
                    }

                    grid.SetValue(i, (int)Cols.Procesado, true);
                }
            }
            bEjecutando = false;

            // ya que termina el recorrido de las unidades se elimina las tabla de los perfiles de atencion
            EliminarTablaPerfiles();

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);
            MostrarEnProceso(false);
        }

        private void ObtenerVenta(string sIdFarmacia, string sFarmacia, string sIdPrograma, string sIdSubPrograma, int MostrarPrecios, int MostrarDevoluciones)
        {
            Obtener_DetallesInformacion(sIdFarmacia, sFarmacia, 2, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones); 
        }

        private void ObtenerConsignacion(string sIdFarmacia, string sFarmacia, string sIdPrograma, string sIdSubPrograma, int MostrarPrecios, int MostrarDevoluciones)
        {
            Obtener_DetallesInformacion(sIdFarmacia, sFarmacia, 1, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones); 
        }

        private void Obtener_DetallesInformacion(string sIdFarmacia, string sFarmacia, int TipoDeDispensacion, string sIdPrograma, string sIdSubPrograma, int MostrarPrecios, int MostrarDevoluciones)
        { 
            string sIdEmpresa = cboEmpresas.Data;
            string sIdEstado = cboEstados.Data;
            string sIdJurisdiccion = cboJurisdicciones.Data;

            // int iConsigna = 1;
            int iVenta = TipoDeDispensacion;            

            int iMED = 1;
            int iMC = 2;

            int iDiasMes = General.FechaDiasMes(dtpFechaInicial.Value);
            string FechaInicial = string.Format("{0}-{1}-01", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));
            string FechaFinal = string.Format("{0}-{1}-15", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));

            string FechaInicial_02 = string.Format("{0}-{1}-16", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2));
            string FechaFinal_02 = string.Format("{0}-{1}-{2}", Fg.PonCeros(dtpFechaInicial.Value.Year, 4), Fg.PonCeros(dtpFechaInicial.Value.Month, 2), Fg.PonCeros(iDiasMes, 2));

            // MEDICAMENTO
            if (rdoInsumosMedicamento.Checked)
            {
                //Quincena
                if (cboQuincena.Data == "1")
                {
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else if (cboQuincena.Data == "2")
                {
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else 
                {
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
            }// MATERIAL DE CURACION
            else if (rdoInsumoMatCuracion.Checked)
            {
                //Quincena
                if (cboQuincena.Data == "1")
                {
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else if (cboQuincena.Data == "2")
                {
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else
                {
                    //// Material de Curacion 
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
            }
            else 
            {// AMBOS ( MEDICAMENTO Y MATERIAL DE CURACION )
                if (cboQuincena.Data == "1")
                {
                    //Medicamento
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    //Material de Curacion
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else if (cboQuincena.Data == "2")
                {
                    //Medicamento
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    //Material de Curacion
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
                else
                {
                    //// Medicamento 
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMED, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);

                    //// Material de Curacion 
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 1, FechaInicial, FechaFinal, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                    ObtenerInformacion_Unidad(sIdEmpresa, sIdEstado, sIdJurisdiccion, sIdFarmacia, sFarmacia, 2, FechaInicial_02, FechaFinal_02, iVenta, iMC, sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
                }
            }
        }

        private void ObtenerInformacion_Unidad(string IdEmpresa, string IdEstado,
            string IdJurisdiccion, string IdFarmacia, string Farmacia,
            int Parte, string FechaInicial, string FechaFinal, int TipoDispensacion, int TipoInsumo,
            string sIdPrograma, string sIdSubPrograma, int MostrarPrecios, int MostrarDevoluciones)
        {
            bool bExistenDatos = false;
            string sIdCliente = Fg.PonCeros(txtCte.Text, 4);
            string sIdSubCliente = txtSubCte.Text == "" ? "*" : Fg.PonCeros(txtSubCte.Text, 4);
            string sSql = "";

            sSql = string.Format(
                "Select top 1 * " +
                "From VentasEnc (NoLock) " +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdSubCliente = '{4}' " + 
                " and IdPrograma = '{5}' and IdSubPrograma = '{6}' and convert(varchar(10), FechaRegistro, 120) Between '{7}' and '{8}' ",
                IdEmpresa, IdEstado, IdFarmacia, sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma, FechaInicial, FechaFinal);


            if (leer.Exec(sSql))
            {
                bExistenDatos = leer.Leer(); 
            }

            if ( bExistenDatos ) 
            {
                ObtenerInformacion_UnidadFinal(IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, Parte, FechaInicial, FechaFinal, TipoDispensacion, TipoInsumo,
                    sIdPrograma, sIdSubPrograma, MostrarPrecios, MostrarDevoluciones);
            }
        }


        private void ObtenerInformacion_UnidadFinal(string IdEmpresa, string IdEstado,
            string IdJurisdiccion, string IdFarmacia, string Farmacia,
            int Parte, string FechaInicial, string FechaFinal, int TipoDispensacion, int TipoInsumo,
            string sIdPrograma, string sIdSubPrograma, int MostrarPrecios, int MostrarDevoluciones)
        {
            this.Cursor = Cursors.WaitCursor;
            string sIdCliente = Fg.PonCeros(txtCte.Text, 4);
            string sIdSubCliente = txtSubCte.Text == "" ? "*" : Fg.PonCeros(txtSubCte.Text, 4);
            int iPerfilAtencion = 0;
            int iSubPerfilAtencion = 0;
            string sPerfilAtencion = "";
            string sSql = ""; 

            // bEjecutando = true; 

            sSql = string.Format("Set Dateformat YMD \nExec spp_Rpt_Administrativos \n" +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdCliente = '{3}', @IdSubCliente = '{4}', \n" +
                " @IdPrograma = '{5}', @IdSubPrograma = '{6}', @TipoDispensacion = '{7}', \n" +
                " @FechaInicial = '{8}', @FechaFinal = '{9}', @TipoInsumo = '{10}', @TipoInsumoMedicamento = '{11}', @SubFarmacias = '{12}', \n" +
                " @MostrarPrecios = '{13}', @MostrarDevoluciones = '{14}', @OrigenDispensacion = '{15}', @Ordenamiento = '{16}' \n", 
                IdEmpresa, IdEstado, IdFarmacia, sIdCliente, sIdSubCliente, sIdPrograma, sIdSubPrograma,
                TipoDispensacion, FechaInicial, FechaFinal, TipoInsumo, 0, sSubFarmacias, MostrarPrecios, MostrarDevoluciones, 0, 2);
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion (NoLock) ");

            leer = new clsLeer(ref cnn); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.DatosConexion, leer.Error, "ObtenerInformacion()");
                //// General.msjError("Ocurrió un error al obtener la información del reporte.");
            }
            else
            {
                if (leer.Leer())
                {
                    if (PerfilesDeAtencion(IdFarmacia))
                    {
                        while (leerPerfil.Leer())
                        {
                            sPerfilAtencion = "";
                            
                            iPerfilAtencion = leerPerfil.CampoInt("IdPerfilAtencion");
                            iSubPerfilAtencion = leerPerfil.CampoInt("IdSubPerfilAtencion");
                            sPerfilAtencion = leerPerfil.Campo("PerfilDeAtencion");


                            CargarPerfilesDeAtencion(iPerfilAtencion, iSubPerfilAtencion);

                            if (iPerfilAtencion == 0)
                            {
                                sPerfilAtencion = "";
                            }
                            else
                            {
                                sPerfilAtencion = string.Format("__{0}_{1}_{2}", sPerfilAtencion, iPerfilAtencion, iSubPerfilAtencion);
                            }

                            if (rdoDoctoPDF.Checked)
                            {
                                Generar_PDF(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, sIdPrograma, sIdSubPrograma, iPerfilAtencion, iSubPerfilAtencion, sPerfilAtencion);
                            }

                            if (rdoDoctoExcel.Checked)
                            {
                                ObtenerInformacionExcel(iPerfilAtencion, iSubPerfilAtencion);
                                Generar_Excel(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, sIdPrograma, sIdSubPrograma, sPerfilAtencion);
                            }

                            if (rdoDoctoAmbos.Checked)
                            {
                                ObtenerInformacionExcel(iPerfilAtencion, iSubPerfilAtencion);
                                Generar_Excel(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, sIdPrograma, sIdSubPrograma, sPerfilAtencion);
                                Generar_PDF(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, sIdPrograma, sIdSubPrograma, iPerfilAtencion, iSubPerfilAtencion, sPerfilAtencion);
                            }
                        }
                    }
                }
            }
        }

        private string GetNombreDocumento(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, 
            int iTipo, string PerfilDeAtencion)
        {
            string sRegresa = "";

            string sFile = "";
            string sFecha = string.Format("__{0}{1}{2}",
                Fg.PonCeros(dtpFechaInicial.Value.Year, 4),
                Fg.PonCeros(dtpFechaInicial.Value.Month, 2),
                General.FechaNombreMes(dtpFechaInicial.Value).ToUpper());

            Farmacia = Farmacia.Replace(" ", "_");
            sFecha += string.Format("_Q{0}", Fg.PonCeros(Parte, 1));
            sFile = string.Format("J{0}_{1}_{2}_{3}", IdJurisdiccion, IdFarmacia, Farmacia, sFecha);


            //// sFile = string.Format("{0}", IdFarmacia);
            sFile += string.Format("_{0}", TipoDispensacion == 1 ? "CG" : "VT");
            sFile += string.Format("_{0}", TipoInsumo == 1 ? "MD" : "MC");
            sFile += string.Format("__P{0}", IdPrograma);
            sFile += string.Format("__SP{0}", IdSubPrograma);
            sFile += string.Format("__SF{0}", IdSubFarmacia);
            //sFile += string.Format("__SF_{0}", Fg.PonCeros(IdSubFarmacia, 2)); 
            sFile += iTipo == 1 ? "_S_PRECIOS" : "_C_PRECIOS";

            sFile += PerfilDeAtencion;

            sRegresa = Path.Combine(sRutaDestino_Archivos, sFile);
            sRegresa = sFile; 

            return sRegresa; 
        }

        private void Generar_Excel(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, string PerfilDeAtencion)
        {
            if (Generar_Excel_RevisarDatos(1))
            {
                Generar_Excel(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, 1, PerfilDeAtencion);
            }

            if (Generar_Excel_RevisarDatos(2))
            {
                Generar_Excel(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, 2, PerfilDeAtencion);
            }
        }

        private bool Generar_Excel_RevisarDatos(int Tipo)
        {
            bool bRegresa = false;

            clsLeer leerDatos = new clsLeer();
            leerDatos.DataRowsClase = leerExportarExcel.DataTableClase.Select(string.Format("IdGrupoPrecios = '{0}' ", Tipo));

            bRegresa = leerDatos.Leer(); 

            return bRegresa ; 
        }

        private void Generar_Excel(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, int iTipo, string PerfilDeAtencion)
        {
            if (iFormatoExcel == 1)
            {
                Generar_Excel__Formato_01(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, IdSubFarmacia, IdPrograma, IdSubPrograma, iTipo, PerfilDeAtencion);
            }

            if (iFormatoExcel == 2)
            {
                Generar_Excel__Formato_02(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, IdSubFarmacia, IdPrograma, IdSubPrograma, iTipo, PerfilDeAtencion);
            }
        }

        private void Generar_Excel__Formato_01(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, int iTipo, string PerfilDeAtencion)
        {
            clsGenerarExcel excel = new clsGenerarExcel();
            string sFile = "";
            string sNombreHoja = "Hoja1";
            //bool bRegresa = false;
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_Formato_Integracion_Facturacion_Manual.xlsx";
            int iRow = 2;
            int iCol = 2;
            int iRegistro = 0;

            DataTable dtTemp;

            string sNombreDocumento = "";

            this.Cursor = Cursors.WaitCursor;
            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "FACT_Formato_Integracion_Facturacion_Manual.xlsx", DatosCliente);

            sFile = GetNombreDocumento(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, iTipo, PerfilDeAtencion);
            sFile += string.Format("____{0}", iExcelConcentrado == 1 ? "Concentrado" : "Detallado");
            sFile = sFile + ".xlsx";

            clsLeer leerDatos = new clsLeer();
            leerDatos.DataRowsClase = leerExportarExcel.DataTableClase.Select(string.Format("IdGrupoPrecios = '{0}' ", iTipo));
            dtTemp = leerDatos.DataTableClase;
            dtTemp.Columns.Remove("IdGrupoPrecios");
            leerDatos.DataTableClase = dtTemp;

            //if (!bRegresa) 
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else 
            //{
            //xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //xpExcel.AgregarMarcaDeTiempo = false;

            //if ( xpExcel.PrepararPlantilla(sRutaDestino_Archivos, sFile) )
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    xpExcel.GeneraExcel();
            //    iRegistro = 0;

            //    while (leerDatos.Leer()) 
            //    {
            //        iRegistro++;
            //        iCol = 2;

            //        xpExcel.Agregar(iRow - 1, iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("ClaveSSA_Base"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("ClaveSSA"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("DescripcionClave"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("Presentacion"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("PrecioUnitario"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.CampoDouble("Cantidad"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.CampoInt("TasaIva"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.CampoDouble("SubTotal"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.CampoDouble("Iva"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.CampoDouble("Total"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("UnidadDeMedida"), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("Impuesto"), iRow, iCol++);

            //        xpExcel.Agregar(string.Format("'{0}'", leerDatos.Campo("ClaveLote")), iRow, iCol++);
            //        xpExcel.Agregar(leerDatos.Campo("Caducidad"), iRow, iCol++);

            //        iRow++; 
            //    }

            //    // Finalizar el Proceso 
            //    xpExcel.CerrarDocumento();
            //}

            sNombreDocumento = sFile;




            excel = new clsGenerarExcel();
            excel.RutaArchivo = sRutaDestino_Archivos;
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = false;

            if (excel.CrearArchivo())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.InsertarTabla(sNombreHoja, iRow, iCol, leerDatos.DataSetClase);
                excel.CerraArchivo();
            }
            this.Cursor = Cursors.Default;

        }

        private void Generar_Excel__Formato_02(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, int iTipo, string PerfilDeAtencion)
        {
            string sFile = "";
            bool bRegresa = false;
            int iRow = 1;
            int iCol = 2;
            int iRegistro = 0;
            string sNombreHoja = "DetallesFactura";
            string sNombreDocumento = "Reporte de dispensación";

            clsGenerarExcel excel = new clsGenerarExcel();
            clsLeer encabezadoExcel = new clsLeer();
            clsLeer detalladoExcel = new clsLeer();
            clsLeer leerDatos = new clsLeer();



            sNombreDocumento = GetNombreDocumento(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, iTipo, PerfilDeAtencion);
            sNombreDocumento += string.Format("____{0}", iExcelConcentrado == 1 ? "Concentrado" : "Detallado");
            sNombreDocumento = sNombreDocumento + ".xlsx";

            leerDatos.DataRowsClase = leerExportarExcel.DataTableClase.Select(string.Format("IdGrupoPrecios = '{0}' ", iTipo));

            this.Cursor = Cursors.WaitCursor;


            excel = new clsGenerarExcel();
            excel.RutaArchivo = sRutaDestino_Archivos;
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = false;

            if (excel.CrearArchivo())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.InsertarTabla(sNombreHoja, iRow, iCol, leerDatos.DataSetClase);
                excel.CerraArchivo();
            }
            this.Cursor = Cursors.Default;
        }

        private void Generar_PDF(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, int PerfilAtencion, int SubPerfilAtencion, string PerfilDeAtencion)
        {
            Generar_PDF(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, 1, PerfilAtencion, SubPerfilAtencion, PerfilDeAtencion);
            Generar_PDF(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, 2, PerfilAtencion, SubPerfilAtencion, PerfilDeAtencion);
        }

        private void Generar_PDF(string IdJurisdiccion, string IdFarmacia, string Farmacia, int Parte,
            int TipoDispensacion, int TipoInsumo, string IdSubFarmacia, string IdPrograma, string IdSubPrograma, int iTipo, int PerfilAtencion, int SubPerfilAtencion, string PerfilDeAtencion)
        {
            int iMostrarSubFarmacias = chkMostrarSubFarmacias.Checked ? 1 : 0;
            int iMostrarPaquetes = chkMostrarPaquetes.Checked ? 1 : 0;
            int iMostrarLotes = chkMostrarLotes.Checked ? 1 : 0; 
            string sFile = "";
            
            ////////string sFecha = string.Format("__{0}_{1}_{2}",
            ////////    Fg.PonCeros(dtpFechaInicial.Value.Year, 4),
            ////////    Fg.PonCeros(dtpFechaInicial.Value.Month, 2),
            ////////    General.FechaNombreMes(dtpFechaInicial.Value).ToUpper());

            ////////Farmacia = Farmacia.Replace(" ", "_");
            ////////sFecha += string.Format("__Qna_{0}", Fg.PonCeros(Parte, 2));
            ////////sFile = string.Format("J{0}_{1}____{2}__{3}", IdJurisdiccion, IdFarmacia, Farmacia, sFecha);


            //////////// sFile = string.Format("{0}", IdFarmacia);
            ////////sFile += string.Format("__{0}", TipoDispensacion == 1 ? "CSG" : "VTA");
            ////////sFile += string.Format("__{0}", TipoInsumo == 1 ? "MED" : "MDC");
            ////////sFile += string.Format("__P_{0}", IdPrograma);
            ////////sFile += string.Format("__SP_{0}", IdSubPrograma);
            ////////sFile += string.Format("__SF_{0}", IdSubFarmacia);
            //////////sFile += string.Format("__SF_{0}", Fg.PonCeros(IdSubFarmacia, 2)); 
            ////////sFile += iTipo == 1 ? "__SIN_PRECIOS" : "__CON_PRECIOS";


            //sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf");
            sFile = GetNombreDocumento(IdJurisdiccion, IdFarmacia, Farmacia, Parte, TipoDispensacion, TipoInsumo, sSubFarmacias, IdPrograma, IdSubPrograma, iTipo, PerfilDeAtencion); 
            sFile = Path.Combine(sRutaDestino_Archivos, sFile + ".pdf"); 


            bool bRegresa = false;
            clsImprimir Reporte = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);

            Reporte.RutaReporte = DtGeneral.RutaReportes; 
            Reporte.NombreReporte = "PtoVta_Admon_Validacion_GruposPrecios";
            //Reporte.NombreReporte = "PtoVta_Admon_Validacion"; 
            Reporte.Add("MostrarSubFarmacias", iMostrarSubFarmacias);
            Reporte.Add("IdGrupoPrecios", iTipo);
            Reporte.Add("MostrarPaquetes", iMostrarPaquetes);
            Reporte.Add("TitutoEncabezadoReportes", "");
            Reporte.Add("IdPerfilAtencion", PerfilAtencion);
            Reporte.Add("IdSubPerfilAtencion", SubPerfilAtencion);
            Reporte.Add("MostrarLotes", iMostrarLotes); 

            Reporteador = new clsReporteador(Reporte, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false; 

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);


        }

        private bool PerfilesDeAtencion(string IdFarmacia)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Rpt_Administrativos_Update_Niveles_Atencion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ",
                                cboEmpresas.Data, cboEstados.Data, IdFarmacia);

            if (!leerPerfil.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leerPerfil, "PerfilesDeAtencion");
                General.msjError("Ocurrio un error al obtener los perfiles de Atención");
            }

            return bRegresa;
        }

        private bool CargarPerfilesDeAtencion(int PerfilAtencion, int SubPerfilAtencion)
        {
            bool bRegresa = true;
            string sSql = "";

            sSql = string.Format(" Exec spp_Rpt_Administrativos_Cargar_PerfilesDeAtencion @IdPerfilAtencion = {0}, @IdSubPerfilAtencion = '{1}' ", PerfilAtencion, SubPerfilAtencion);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
                Error.GrabarError(leer, "CargarPerfilesDeAtencion");
                General.msjError("Ocurrio un error al cargar los perfiles de Atención");
            }

            return bRegresa;
        }

        private void EliminarTablaPerfiles()
        {
            string sSql = string.Format(" If Exists ( select * from sysobjects (nolock) Where Name = 'RptAdmonDispensacion_PerfilesDeAtencion' and xType = 'U' ) ");
            sSql += "\n " + string.Format(" Drop table RptAdmonDispensacion_PerfilesDeAtencion ");

            leer = new clsLeer(ref cnn);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "EliminarTablaPerfiles()");
                General.msjError("Ocurrió un error al eliminar la tabla de perfiles.");
            }

        }
        #endregion Procesar Informacion 

        #region Funciones y Procedimientos Privados 
        private void ActivarControles()
        { 
            this.Cursor = Cursors.Default;
            btnNuevo.Enabled = true; 
            btnEjecutar.Enabled = true;
            btnExportarExcel.Enabled = false;
            txtCte.Enabled = true;
            txtCte.Enabled = true; 
            btnDirectorio.Enabled = true; 

            FrameInsumos.Enabled = true;
            FrameDispensacion.Enabled = true;
            FrameFechas.Enabled = true;
            FrameSubFarmacias.Enabled = true;
            FrameTipoDocoto.Enabled = true;
            FrameFormatosExcel.Enabled = true; 
            chkPrograma_SubPrograma.Enabled = true;
            chkMarcarDesmarcar.Enabled = true; 
        }

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando) 
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false; 

                // btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true;
                ActivarControles(); 

                ////////if (!bSeEncontroInformacion) 
                ////////{
                ////////    _workerThread.Interrupt(); 
                ////////    _workerThread = null; 

                ////////    ActivarControles();

                ////////   //////if (bSeEjecuto)
                ////////   ////// {
                ////////   //////     General.msjUser("No existe información para mostrar bajo los criterios seleccionados.");
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
            SubFarmacias = new FrmListaDeSubFarmacias(cboEstados.Data, cboJurisdicciones.Data);
            SubFarmacias.AliasTabla = "L.";
            SubFarmacias.Estado = cboEstados.Data;
            ////SubFarmacias.Farmacia = cboJurisdicciones.Data;
            SubFarmacias.Farmacia = "";
            SubFarmacias.EsParaSP = true;
            SubFarmacias.MostrarDetalle();
            sSubFarmacias = SubFarmacias.ListadoSubFarmacias;
        }

        private void ObtenerInformacionExcel(int PerfilAtencion, int SubPerfilAtencion)
        {
            int iMostrarPaquetes = chkMostrarPaquetes.Checked ? 1 : 0;
            string sSql = string.Format("Exec spp_Rpt_Administrativos_ExportarExcel " +
                " @MostrarAgrupado = '{0}', @IdPerfilAtencion = '{1}', @Concentrado = '{2}', @IdSubPerfilAtencion = '{3}' ", iMostrarPaquetes, PerfilAtencion, iExcelConcentrado, SubPerfilAtencion); 

            leerExportarExcel = new clsLeer(ref cnn);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer.DatosConexion, leer.Error, "ObtenerInformacionExcel()"); 
                //General.msjError("Ocurrió un error al obtener la información del reporte.");
            }
            else
            {
                leerExportarExcel.DataSetClase = leer.DataSetClase;
            }
        } 

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            //int iRow = 2;
            //string sNombreFile = "PtoVta_Admon_Validacion" + DtGeneral.ClaveRENAPO + cboJurisdicciones.Data + ".xls";
            //string sPeriodo = "";

            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla(sNombreFile))
            //    {
            //        xpExcel.GeneraExcel();

            //        //Se pone el encabezado
            //        leerExportarExcel.RegistroActual = 1;
            //        leerExportarExcel.Leer();
            //        xpExcel.Agregar(leerExportarExcel.Campo("Empresa"), iRow, 2);
            //        iRow++;
            //        xpExcel.Agregar(leerExportarExcel.Campo("Farmacia"), iRow, 2);
            //        iRow++;

            //        sPeriodo = string.Format("PERIODO DEL {0} AL {1} ",
            //            General.FechaYMD(leerExportarExcel.CampoFecha("FechaInicial"), "-"), General.FechaYMD(leerExportarExcel.CampoFecha("FechaFinal"), "-"));
            //        xpExcel.Agregar(sPeriodo, iRow, 2);

            //        iRow = 6;
            //        xpExcel.Agregar(leerExportarExcel.Campo("FechaImpresion"), iRow, 3);

            //        // Se ponen los detalles
            //        leerExportarExcel.RegistroActual = 1;
            //        iRow = 9;
            //        while (leerExportarExcel.Leer())
            //        {
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdCliente"), iRow, 2);
            //            xpExcel.Agregar(leerExportarExcel.Campo("NombreCliente"), iRow, 3);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdPrograma"), iRow, 4);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Programa"), iRow, 5);
            //            xpExcel.Agregar(leerExportarExcel.Campo("IdSubPrograma"), iRow, 6);
            //            xpExcel.Agregar(leerExportarExcel.Campo("SubPrograma"), iRow, 7);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Folio"), iRow, 8);
            //            xpExcel.Agregar(leerExportarExcel.Campo("NumReceta"), iRow, 9);
            //            xpExcel.Agregar(leerExportarExcel.Campo("FechaReceta"), iRow, 10);
            //            xpExcel.Agregar(leerExportarExcel.Campo("FolioReferencia"), iRow, 11);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Beneficiario"), iRow, 12);
            //            xpExcel.Agregar(leerExportarExcel.Campo("ClaveSSA"), iRow, 13);
            //            xpExcel.Agregar(leerExportarExcel.Campo("DescripcionCorta"), iRow, 14);
            //            xpExcel.Agregar(leerExportarExcel.Campo("Cantidad"), iRow, 15);
            //            xpExcel.Agregar(leerExportarExcel.Campo("PrecioLicitacion"), iRow, 16);
            //            xpExcel.Agregar(leerExportarExcel.Campo("ImporteEAN"), iRow, 17);

            //            iRow++;
            //        }

            //        // Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //    this.Cursor = Cursors.Default; 
            //}
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
    } 
}
