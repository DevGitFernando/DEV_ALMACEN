using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Inventario;
using Dll_SII_INadro;
using Dll_SII_INadro.GenerarArchivos; 

namespace Dll_SII_INadro.InformacionDeSalida
{
    public partial class FrmINF_Salidas : FrmBaseExt 
    {
        ////enum Cols 
        ////{ 
        ////    Cliente = 1, NombreCliente = 2, IdFarmacia = 3, Farmacia = 4, TipoDeUnidad = 5 
        ////}

        enum Cols
        {
            IdFarmacia = 1, Cliente = 2, Farmacia = 3, Procesar = 4, Procesado = 5, Inicio = 6, Fin = 7, Procesando = 8 
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsAyudas Ayuda;
        clsConsultas Consultas;
        clsGrid grid;

        clsListView lst;
        clsDatosCliente DatosCliente;
        Thread _workerThread;

        bool bEjecutando = false;
        bool bSeEncontroInformacion = false;

        FolderBrowserDialog folder; // = new FolderBrowserDialog(); 
        string sRutaDestino = "";
        string sRutaDestino_Archivos = "";
        bool bFolderDestino = false; 

        string sCliente = "";
        string sFecha = "";
        TipoDeDocumento iTipoProceso = TipoDeDocumento.Ninguno;
        string sProceso = "";
        int iRenlgonEnProceso = 0;

        public FrmINF_Salidas()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnDll_SII_INadro.DatosApp, this.Name, "");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, General.Modulo, this.Name, General.Version);
            Ayuda = new clsAyudas(General.DatosConexion, General.Modulo, this.Name, General.Version);

            lst = new clsListView(lstvUnidades);

            this.Width = 1060;
            FrameProceso.Left = 200;
            FrameProceso.Top = 140;
            MostrarEnProceso(false);

            grid = new clsGrid(ref grdUnidades, this); 
        }

        #region Form
        private void FrmINF_Salidas_Load(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void MostrarEnProceso(bool Mostrar)
        {
            if (Mostrar)
            {
                FrameProceso.Left = 200;
            }
            else
            {
                FrameProceso.Left = this.Width + 100;
            }
        }

        private void ActivarControles()
        {
            this.Cursor = Cursors.Default;
            IniciarToolBar(true, true, false); 
        }
        #endregion Form

        #region Botones
        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Generar)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnProcesarDocumentos.Enabled = Generar;
        }

        private void InicializarPantalla()
        {
            lst.LimpiarItems();
            grid.Limpiar();

            iRenlgonEnProceso = 0; 
            rdoExistencias.Checked = true;

            General.FechaSistemaObtener(); 
            sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sRutaDestino += string.Format(@"\DOCUMENTOS_NADRO\{0}", General.FechaYMD(General.FechaSistema, ""));
            lblDirectorioTrabajo.Text = sRutaDestino;
            bFolderDestino = true;

            rdoDatos_Historico.Checked = true; 
            nmCauses.Value = 2014; 

            IniciarToolBar(true, true, false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ObtenerListadoDeClientes();
        }

        private void btnProcesarDocumentos_Click(object sender, EventArgs e)
        {
            if (validarProcesamiento())
            {
                ////CrearDirectorioDestino();
                IniciarProcesamiento(); 
            }
        }
        #endregion Botones

        #region Informacion
        private void ObtenerListadoDeClientes()
        {
            string sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}' ", DtGeneral.EstadoConectado, 1, 1 );


            sSql = string.Format("Exec spp_INT_ND_ListadoDeClientes " +
                " @IdEstado = '{0}', @EsDeSurtimiento = '{1}', @TipoDeCliente = '{2}', @EsParaReporteador = '{3}' ",
                DtGeneral.EstadoConectado, 1, 1, 1);

            lst.Limpiar();
            grid.Limpiar();
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerListadoDeClientes()");
                General.msjError("Ocurrió un error al obtener el listado de clientes.");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjUser("No se encontrarón clientes para la generacion de documentos.");
                }
                else
                {
                    btnProcesarDocumentos.Enabled = true; 
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    grid.LlenarGrid(leer.DataSetClase); 
                }
            }
        }
        #endregion Informacion

        #region Menu 
        private void btnDocumentoGeneral_Click(object sender, EventArgs e)
        {
            GenerarDocumentoGeneral(); 
        }

        private void btnDocumentoPorFarmacia_Click(object sender, EventArgs e)
        {
            sCliente = lst.GetValue((int)Cols.Cliente);

            GenerarDocumentoPorFarmacia(); 
        }

        private void lstvUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            sCliente = lst.GetValue((int)Cols.Cliente); 

            btnDocumentoGeneral.Enabled = lst.Registros > 0;
            btnDocumentoPorFarmacia.Enabled = lst.Registros > 0; 
        }

        private void PrepararMenu(TipoDeDocumento Proceso)
        {
            bool bActivarFechas = true;
            bool bActivarCauses = false;
            bool bActivarOrigenDeDatos = true; 

            btnEjecutar.Enabled = true;
            btnProcesarDocumentos.Enabled = true;
            iTipoProceso = Proceso;
            nmCauses.Enabled = bActivarCauses;
            rdoDatos_Historico.Enabled = bActivarOrigenDeDatos;
            rdoDatos_Generar.Enabled = bActivarOrigenDeDatos;

            switch (Proceso)
            {
                case TipoDeDocumento.Existencias:
                    //bActivarFechas = false; 
                    sProceso = " existencias ";
                    btnDocumentoGeneral.Text = "Generar existencia general";
                    btnDocumentoPorFarmacia.Text = "Generar existencia por farmacia"; 
                    break;

                case TipoDeDocumento.Surtidos:
                    sProceso = " surtidos ";
                    btnDocumentoGeneral.Text = "Generar surtidos general";
                    btnDocumentoPorFarmacia.Text = "Generar surtidos por farmacia"; 
                    break;

                case TipoDeDocumento.Recibos:
                    sProceso = " recibos ";
                    btnDocumentoGeneral.Text = "Generar recibos general";
                    btnDocumentoPorFarmacia.Text = "Generar recibos por farmacia"; 
                    break;

                case TipoDeDocumento.Remisiones:
                    sProceso = " remisiones ";
                    btnDocumentoGeneral.Text = "Generar remisiones general";
                    btnDocumentoPorFarmacia.Text = "Generar remisiones por farmacia";
                    bActivarCauses = true;
                    bActivarOrigenDeDatos = false;
                    rdoDatos_Historico.Checked = true; 
                    break;

                case TipoDeDocumento.TomaDeExistencia:
                    sProceso = " toma de existencia ";
                    btnDocumentoGeneral.Text = "Generar toma de existencia general";
                    btnDocumentoPorFarmacia.Text = "Generar toma de existencia por farmacia";
                    break;

                case TipoDeDocumento.Catalogos:
                    bActivarFechas = false;
                    sProceso = " catálogo de información ";
                    btnDocumentoGeneral.Text = "Generar catálogo";
                    btnDocumentoPorFarmacia.Text = "Generar catálogo";
                    break; 
            }

            btnEjecutar.Enabled = bActivarFechas;
            btnProcesarDocumentos.Text = btnDocumentoGeneral.Text;
            btnProcesarDocumentos.ToolTipText = btnDocumentoGeneral.Text;

            btnDocumentoGeneral.Visible = bActivarFechas; 
            btnDocumentoPorFarmacia.Visible = bActivarFechas;

            nmCauses.Enabled = bActivarCauses; 

            dtpFechaInicial.Enabled = bActivarFechas;
            dtpFechaFinal.Enabled = bActivarFechas;

            rdoDatos_Historico.Enabled = bActivarOrigenDeDatos;
            rdoDatos_Generar.Enabled = bActivarOrigenDeDatos; 
        }

        private void GenerarDocumentoGeneral()
        {
            ////bool bEjecutar = false;
            ////string sMsj = string.Format("¿ Desea ejecutar la generación de documentos de {0} para todas las unidades ?", sProceso);

            ////if (General.msjConfirmar(sMsj) == System.Windows.Forms.DialogResult.Yes)
            ////{
            ////    bEjecutar = true;
            ////} 

            ////if (bEjecutar)
            ////{
            ////    switch (iTipoProceso)
            ////    {
            ////        case 1:
            ////            Existencias ex = new Existencias();
            ////            ex.GenerarExistencias(dtpFechaInicial.Value, dtpFechaFinal.Value);
            ////            ex.MsjFinalizado(); 
            ////            break;

            ////        case 2:
            ////            Surtidos su = new Surtidos();
            ////            su.GenerarSurtidos(dtpFechaInicial.Value, dtpFechaFinal.Value);
            ////            su.MsjFinalizado();
            ////            break;

            ////        case 3:
            ////            Recibos re = new Recibos();
            ////            re.GenerarRecibos(dtpFechaInicial.Value, dtpFechaFinal.Value);
            ////            re.MsjFinalizado();
            ////            break;

            ////        case 4:
            ////            Remisiones rm = new Remisiones();
            ////            rm.GenerarRemisiones(dtpFechaInicial.Value, dtpFechaFinal.Value);
            ////            rm.MsjFinalizado(); 
            ////            break;

            ////        case 5:
            ////            TomaDeExistencias te = new TomaDeExistencias();
            ////            te.GenerarTomaDeExistencias(dtpFechaInicial.Value, dtpFechaFinal.Value);
            ////            te.MsjFinalizado();
            ////            break;

            ////        case 6:
            ////            GenerarCatalogo gn = new GenerarCatalogo();
            ////            if (gn.Generar())
            ////            {
            ////                gn.MsjFinalizado(); 
            ////            }
            ////            break;
            ////    }
            ////}
        }

        private void GenerarDocumentoPorFarmacia()
        {
            GenerarDocumentoPorFarmacia(true); 
        }

        private void GenerarDocumentoPorFarmacia(bool MensajeFinalizado)
        {
            ////switch (iTipoProceso)
            ////{
            ////    case 1:
            ////        Existencias ex = new Existencias();
            ////        ex.GenerarExistencias(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

            ////        if (MensajeFinalizado)
            ////        {
            ////            ex.MsjFinalizado();
            ////        }
            ////        break;

            ////    case 2:
            ////        Surtidos su = new Surtidos();
            ////        su.GenerarSurtidos(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

            ////        if (MensajeFinalizado)
            ////        {
            ////            su.MsjFinalizado();
            ////        }
            ////        break;

            ////    case 3:
            ////        Recibos re = new Recibos();
            ////        re.GenerarRecibos(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

            ////        if (MensajeFinalizado)
            ////        {
            ////            re.MsjFinalizado();
            ////        }
            ////        break;

            ////    case 4:
            ////        Remisiones rm = new Remisiones();
            ////        rm.GenerarRemisiones(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

            ////        if (MensajeFinalizado)
            ////        {
            ////            rm.MsjFinalizado();
            ////        }
            ////        break;

            ////    case 5:
            ////        TomaDeExistencias te = new TomaDeExistencias();
            ////        te.GenerarTomaDeExistencias(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);

            ////        if (MensajeFinalizado)
            ////        {
            ////            te.MsjFinalizado();
            ////        }
            ////        break;
            ////}
        }

        private void rdoExistencias_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExistencias.Checked) PrepararMenu(TipoDeDocumento.Existencias);
        }

        private void rdoSurtidos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSurtidos.Checked) PrepararMenu(TipoDeDocumento.Surtidos);
        }

        private void rdoRecibos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRecibos.Checked) PrepararMenu(TipoDeDocumento.Recibos);
        }

        private void rdoRemisiones_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRemisiones.Checked) PrepararMenu(TipoDeDocumento.Remisiones);
        }

        private void rdoTomaDeExistencias_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTomaDeExistencias.Checked) PrepararMenu(TipoDeDocumento.TomaDeExistencia);
        }

        private void rdoCatalogo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCatalogo.Checked) PrepararMenu(TipoDeDocumento.Catalogos);
        }
        #endregion Menu

        private void IniciarProcesamiento()
        {
            bSeEncontroInformacion = false;
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            ////btnImprimir.Enabled = false;
            ////btnExportarExcel.Enabled = false;


            // bloqueo principal 
            IniciarToolBar(false, false, false);
            grid.BloqueaColumna(true, (int)Cols.Procesar);

            MostrarEnProceso(true);

            //// bSeEjecuto = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();

            grid.SetValue((int)Cols.Procesado, 0);
            grid.SetValue((int)Cols.Inicio, "");
            grid.SetValue((int)Cols.Fin, "");
            grid.SetValue((int)Cols.Procesando, "");

            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);

            _workerThread = new Thread(this.ObtenerInformacion);
            _workerThread.Name = "GenerandoDocumentos";
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
            string sIdFarmacia = "";
            string sMarcaTiempo = "";

            bool bOrigenHistorico = rdoDatos_Historico.Checked;
            bool bSeparar_x_Causes = chkSepararPorCauses.Checked; 

            bEjecutando = true; 
            iRenlgonEnProceso = 0; 
            lblFechaEnProceso.Text = "";


            if (iTipoProceso == TipoDeDocumento.Catalogos)
            {
                GenerarCatalogo gn = new GenerarCatalogo();
                if (gn.Generar())
                {
                    gn.MsjFinalizado();
                }
            }
            else
            {
                for (int i = 1; i <= grid.Rows; i++)
                {
                    if (grid.GetValueBool(i, (int)Cols.Procesar))
                    {
                        iRenlgonEnProceso = i;
                        lblFechaEnProceso.Text = "";

                        sCliente = grid.GetValue(i, (int)Cols.Cliente);
                        sIdFarmacia = grid.GetValue(i, (int)Cols.IdFarmacia);

                        sMarcaTiempo = ObtenerMarcaDeTiempo();
                        grid.SetValue(i, (int)Cols.Inicio, sMarcaTiempo);

                        switch (iTipoProceso)
                        {
                            case TipoDeDocumento.Existencias:
                                Existencias ex = new Existencias();
                                ex.EtiquetaFechaEnProceso = lblFechaEnProceso;
                                ex.GenerarHistorico = bOrigenHistorico;
                                ex.RutaDestinoReportes = sRutaDestino;
                                ex.GenerarExistencias(sIdFarmacia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                                break;

                            case TipoDeDocumento.Surtidos:
                                Surtidos su = new Surtidos();
                                su.EtiquetaFechaEnProceso = lblFechaEnProceso;
                                su.GenerarHistorico = bOrigenHistorico;
                                su.RutaDestinoReportes = sRutaDestino;
                                su.GenerarSurtidos(sIdFarmacia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                                break;

                            case TipoDeDocumento.Recibos:
                                Recibos re = new Recibos();
                                re.EtiquetaFechaEnProceso = lblFechaEnProceso;
                                re.GenerarHistorico = bOrigenHistorico;
                                re.RutaDestinoReportes = sRutaDestino;
                                re.GenerarRecibos(sIdFarmacia, sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                                break;

                            case TipoDeDocumento.Remisiones:
                                Remisiones rm = new Remisiones();
                                rm.IdFarmacia = sIdFarmacia;
                                rm.GUID = Guid.NewGuid().ToString();
                                rm.EtiquetaFechaEnProceso = lblFechaEnProceso;
                                rm.RutaDestinoReportes = sRutaDestino;
                                rm.Causes = (int)nmCauses.Value;
                                rm.GenerarHistorico = bOrigenHistorico;
                                rm.Separar_x_Causes = bSeparar_x_Causes;
                                rm.GenerarRemisiones(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                                break;

                            case TipoDeDocumento.TomaDeExistencia:
                                TomaDeExistencias te = new TomaDeExistencias();
                                te.GenerarTomaDeExistencias(sCliente, dtpFechaInicial.Value, dtpFechaFinal.Value);
                                break;

                            case TipoDeDocumento.Catalogos:
                                GenerarCatalogo gn = new GenerarCatalogo();
                                if (gn.Generar())
                                {
                                    gn.MsjFinalizado();
                                }
                                break;
                        }

                        grid.SetValue(i, (int)Cols.Procesado, true);

                        sMarcaTiempo = ObtenerMarcaDeTiempo();
                        grid.SetValue(i, (int)Cols.Fin, sMarcaTiempo);
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

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (!bEjecutando)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnNuevo.Enabled = true;
                ActivarControles();
            }
        }

        private void chkMarcarDesmarcar_CheckedChanged(object sender, EventArgs e)
        {
            grid.SetValue((int)Cols.Procesar, chkMarcarDesmarcar.Checked); 
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

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            folder = new FolderBrowserDialog();
            folder.Description = "Directorio destino para los documentos generados.";
            folder.RootFolder = Environment.SpecialFolder.Desktop;
            folder.SelectedPath = lblDirectorioTrabajo.Text;
            folder.ShowNewFolderButton = true;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                sRutaDestino = folder.SelectedPath + @"\";
                lblDirectorioTrabajo.Text = sRutaDestino;
                bFolderDestino = true;
            } 
        }

        private bool validarProcesamiento()
        {
            bool bRegresa = true;

            if (bRegresa)
            {
                if (!bFolderDestino)
                {
                    bRegresa = false;
                    General.msjUser("No ha especificado el directorio donde se generaran los documentos, verifique.");
                }
            }

            return bRegresa;
        }
    }
}
