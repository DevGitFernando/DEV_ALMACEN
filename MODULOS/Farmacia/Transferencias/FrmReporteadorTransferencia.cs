using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using DllFarmaciaSoft.Reporteador;

namespace Farmacia.Transferencias
{
    public partial class FrmReporteadorTransferencia : FrmBaseExt
    {
        enum Cols
        {
            Fecha = 1, Folio = 2,
            Destino = 3, Procesar = 4, Procesado = 5
        }

        clsDatosConexion DatosDeConexion = new clsDatosConexion();
        clsConexionSQL cnn;  // = new clsConexionSQL(General.DatosConexion); 
        clsConexionSQL cnnUnidad;
        clsLeer leer;
        clsLeer leerLocal;
        clsLeer leerCuadrosDeAtencion;
        clsLeer leer_ActualizarPrecios;
        clsLeerWebExt leerWeb;
        clsConsultas Consultas;
        clsAyudas Ayuda;
        clsGrid grid;
        FrmListaDeSubFarmacias SubFarmacias;
        DataSet dtsProgramas, dtsSubProgramas;

        DataSet dtsFarmacias = new DataSet();

        //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion.xls";
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
        wsFarmacia.wsCnnCliente conexionWeb;
        Thread _workerThread;

        // int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        DataSet dtsJurisdicciones = new DataSet();

        string sIdPublicoGral = GnFarmacia.PublicoGral;
        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;

        string sImpresoraSeleccionada = "";
        bool bGenerarArchivos = false;
        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        // bool bSeEjecuto = false; 

        ///PrintDialog printer; 
        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = "";
        bool bFolderDestino = false;

        string sImpresion_Precios = "IMPRESION_VENTA_DETALLADA_CON_PRECIOS";

        string sIdCliente = "";
        string sIdSubCliente = ""; 


        public FrmReporteadorTransferencia()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

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
            leerExportarExcel = new clsLeer(ref cnn);
            leerSubFarmacias = new clsLeer(ref cnn);
            leerProgramas_Catalogo = new clsLeer(ref cnn);
            leerSubProgramas_Catalogo = new clsLeer(ref cnn);
            leerProgramas = new clsLeer(ref cnn);
            leerSubProgramas = new clsLeer(ref cnn);
            leerCuadrosDeAtencion = new clsLeer(ref cnn);
            leer_ActualizarPrecios = new clsLeer(ref cnn);

            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            FrameProceso.Left = 178;
            FrameProceso.Top = 154;
            MostrarEnProceso(false);


            grid = new clsGrid(ref grdUnidades, this);

        }

        private void FrmReporteadorTransferencia_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            thCargar_Folios();
        }

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnGenerarDocumentos.Enabled = Generar;
            btnImprimir.Enabled = Generar;
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 178;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private void limpiar()
        {
            bool bValor = true;
            IniciarToolBar(true, true, false);

            sRutaDestino = "";
            bFolderDestino = false;

            btnExportarExcel.Enabled = false;
            // iBusquedasEnEjecucion = 0;

            grid.Limpiar();
            Fg.IniciaControles(this, true);
            chkFechas.Checked = true;
            //chkDesglozado.Enabled = bImpresion_Precios;

            btnImprimir.Enabled = false;
            btnEjecutar.Enabled = true;
            sSubFarmacias = "";

            FrameEncabezado.Enabled = bValor;
            FrameFechas.Enabled = bValor;

            chkDesglosado.Enabled = GnFarmacia.ImplementaImpresionDesglosada_VtaTS;

            ////txtPro.Enabled = false;
            ////txtSubPro.Enabled = false; 

            if (!DtGeneral.EsAdministrador)
            {
                //////cboEmpresas.Data = DtGeneral.EmpresaConectada;
                //////cboEstados.Data = DtGeneral.EstadoConectado;

                //////cboEmpresas.Enabled = false;
                //////cboEstados.Enabled = false; 
            }

            CargarJurisdicciones();

        }

        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CargarJurisdicciones()
        {
            cboJurisdicciones.Clear();
            cboJurisdicciones.Add("*", "<<Seleccione>>");

            cboJurisdicciones.Add(Consultas.Jurisdicciones(DtGeneral.EstadoConectado, "CargarJurisdicciones"), true, "IdJurisdiccion", "NombreJurisdiccion");
            dtsFarmacias = Consultas.Farmacias(DtGeneral.EstadoConectado, "CargarFarmacias()");


            cboJurisdicciones.SelectedIndex = 0;

            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            cboFarmacias.SelectedIndex = 0;
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath;
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            }
        }

        private void CargarFarmacias()
        {
            cboFarmacias.Clear();
            cboFarmacias.Add("*", "<<Seleccione>>");
            string sWhere = "";

            //if (rdoTransferencia.Checked)
            {
                sWhere = string.Format("And IdFarmacia <> '{0}'", DtGeneral.FarmaciaConectada);
            }
            string sFiltro = string.Format(" IdJurisdiccion = '{0}' {1}", cboJurisdicciones.Data, sWhere);

            if (cboJurisdicciones.SelectedIndex != 0)
            {
                cboFarmacias.Filtro = sFiltro;
                cboFarmacias.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            }

            cboFarmacias.SelectedIndex = 0;
        }

        private void thCargar_Folios()
        {
            _workerThread = new Thread(this.Cargar_Folios);
            _workerThread.Name = "Obteniendo_Folios_De_Venta";
            _workerThread.Start();
        }

        private void btnGenerarDocumentos_Click(object sender, EventArgs e)
        {
            // bloqueo principal 
            bGenerarArchivos = true;
            IniciarToolBar(false, false, false);
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            grid.SetValue((int)Cols.Procesado, 0);


            sIdCliente = txtCte.Text;
            sIdSubCliente = txtSubCte.Text;


            MostrarEnProceso(true);

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.Imprimir);
            _workerThread.Name = "Obteniendo_Folios_De_Venta";
            _workerThread.Start();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // bloqueo principal 
            bGenerarArchivos = false;
            IniciarToolBar(false, false, false);
            grid.BloqueaColumna(true, (int)Cols.Procesar);
            grid.SetValue((int)Cols.Procesado, 0);

            MostrarEnProceso(true);

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.Imprimir);
            _workerThread.Name = "Obteniendo_Folios_De_Venta";
            _workerThread.Start();
        }

        private void Imprimir()
        {
            string sFolio = "";            
            bool bRegresa = true;
            bEjecutando = true;

            if (Validacion())
            {
                
                ClsImprimirTransferencias imprimir = new ClsImprimirTransferencias(cnn.DatosConexion, DatosCliente, sRutaDestino, bGenerarArchivos, TipoReporteTransferencia.Detallado);
                imprimir.IdCliente = sIdCliente;
                imprimir.IdSubCliente = sIdSubCliente; 


                for (int i = 1; grid.Rows >= i && bRegresa; i++)
                {
                    if (grid.GetValueBool(i, (int)Cols.Procesar))
                    {
                        sFolio = grid.GetValue(i, (int)Cols.Folio);

                        bRegresa = imprimir.Imprimir(sFolio, chkDesglosado.Checked);

                        grid.SetValue(i, (int)Cols.Procesado, true);
                    }
                }
            }

            bEjecutando = false;

            // bloqueo principal 
            Cursor.Current = Cursors.Default;
            IniciarToolBar(true, true, true);
            grid.BloqueaColumna(false, (int)Cols.Procesar);
            MostrarEnProceso(false);
        }

        private bool Validacion()
        {
            bool bRegresa = true;

            if (bGenerarArchivos)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
                }
            }

            return bRegresa;
        }

        private void btnDirectorio_Click_1(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            }
        }

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked);
        }

        private void toolStripBarraMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Cargar_Folios()
        {
            bool bRegresa = false;
            string sSql = "";
            string sFiltroDestino = "";
            string sFiltroFolios = "";
            string sFiltroFecha = string.Format(" and convert(varchar(10), E.FechaRegistro, 120) between '{0}' and '{1}' ",
                General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value));


            if (cboJurisdicciones.Data != "*")
            {
                sFiltroDestino = string.Format(" And F.IdJurisdiccion  = '{0}' ", cboJurisdicciones.Data);
                if (cboFarmacias.Data != "")
                {
                    sFiltroDestino += string.Format(" And F.IdFarmacia = '{0}' ", cboFarmacias.Data);
                }
            }

            if (!chkFechas.Checked)
            {
                sFiltroFecha = " ";
            }

            if (chkFolios.Checked)
            {
                if (txtFolioInicial.Text.Trim() != "" && txtFolioFinal.Text.Trim() != "")
                {
                    sFiltroFolios = string.Format(" and Right(E.FolioTransferencia, 8) Between '{0}' and '{1}' ",
                        Fg.PonCeros(txtFolioInicial.Text.Trim(), 8),
                        Fg.PonCeros(txtFolioFinal.Text.Trim(), 8)
                        );
                }
                else
                {
                    if (txtFolioInicial.Text.Trim() != "")
                    {
                        sFiltroFolios = string.Format(" and E.FolioTransferencia >= '{0}' ",
                            Fg.PonCeros(txtFolioInicial.Text.Trim(), 8));
                    }

                    if (txtFolioFinal.Text.Trim() != "")
                    {
                        sFiltroFolios = string.Format(" and E.FolioTransferencia <= '{0}' ",
                            Fg.PonCeros(txtFolioFinal.Text.Trim(), 8));
                    }
                }
            }


            sSql = string.Format("Select Convert(Varchar(10), FechaRegistro, 120) As FechaRegistro, E.FolioTransferencia, F.Farmacia As FarmaciaDestino " +
                "From TransferenciasEnc E (NoLock) " +
                "Inner Join vw_Farmacias F (NoLock) On (E.IdEstado = F.IdEstado And E.IdFarmaciaRecibe = F.IdFarmacia) " +
                "Where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}' And E.TipoTransferencia = 'TS' {3} {4} {5}  " +
                "Order By FechaRegistro ",
                sEmpresa, sEstado, sFarmacia, sFiltroFecha, sFiltroDestino, sFiltroFolios);

            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Folios_De_Venta()");
                General.msjError("Ocurrió un error al obtener la lista de folios.");
            }
            else
            {
                bRegresa = leer.Leer();
                grid.LlenarGrid(leer.DataSetClase);
            }

            IniciarToolBar(true, !bRegresa, bRegresa);
        }

        #region Buscar Informacion Dispensacion  
        #region Cliente -- Sub-Cliente 
        private void txtCte_TextChanged( object sender, EventArgs e )
        {
            lblCte.Text = "";
            txtSubCte.Text = "";
        }
        private void txtCte_Validating( object sender, CancelEventArgs e )
        {
            if(txtCte.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, "txtCliente_Validating");
                if(leer.Leer())
                {
                    CargarDatosCliente();
                }
                else
                {
                    General.msjUser("Clave de Cliente no encontrada, ó el Cliente no pertenece al Almacén.");
                    txtCte.Text = "";
                    lblCte.Text = "";
                    txtCte.Focus();
                }
            }
        }

        private void txtCte_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                // leer2.DataSetClase = Ayuda.Clientes("txtCte_KeyDown");
                leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, "txtCte_KeyDown");
                if(leer.Leer())
                {
                    CargarDatosCliente();
                }
            }
        }

        private void CargarDatosCliente()
        {
            ////txtCte.Enabled = false;
            txtCte.Text = leer.Campo("IdCliente");
            lblCte.Text = leer.Campo("NombreCliente");
            //lblCte.Text = leer.Campo("Nombre");
        }

        private void txtSubCte_Validating( object sender, CancelEventArgs e )
        {
            if(txtSubCte.Text != "")
            {
                leer.DataSetClase = Consultas.Farmacia_Clientes(sIdPublicoGral, sEstado, sFarmacia, txtCte.Text, txtSubCte.Text, "txtCte_Validating");
                if(leer.Leer())
                {
                    CargarDatosSubCliente();
                }
                else
                {
                    General.msjUser("Clave de Sub-Cliente no encontrada, ó el Sub-Cliente no pertenece a la Farmacia.");
                    txtSubCte.Text = "";
                    lblSubCte.Text = "";
                    txtSubCte.Focus();
                }
            }
        }

        private void txtSubCte_KeyDown( object sender, KeyEventArgs e )
        {
            if(e.KeyCode == Keys.F1)
            {
                if(txtCte.Text.Trim() != "")
                {
                    leer.DataSetClase = Ayuda.Farmacia_Clientes(sIdPublicoGral, false, sEstado, sFarmacia, txtCte.Text, "txtSubCte_KeyDown");
                    if(leer.Leer())
                    {
                        CargarDatosSubCliente();
                    }
                }
            }
        }

        private void txtSubCte_TextChanged( object sender, EventArgs e )
        {
            lblSubCte.Text = "";
        }

        private void CargarDatosSubCliente()
        {
            ////txtSubCte.Enabled = false;
            txtSubCte.Text = leer.Campo("IdSubCliente");
            lblSubCte.Text = leer.Campo("NombreSubCliente");
        }
        #endregion Cliente -- Sub-Cliente 
        #endregion Buscar Informacion Dispensacion
    }
}
