using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

using Impinj.OctaneSdk;
using Dll_SII_IRFID.Demonio; 

namespace Dll_SII_IRFID.Monitor
{
    public partial class FrmMonitor_RFID : FrmBaseExt
    {

        enum Cols
        {
            IdJurisdiccion = 2, Jurisdiccion = 3,
            IdFarmacia = 4, Farmacia = 5, FolioPedido = 6, FechaPed = 7, FolioSurtido = 8, StatusPedido = 9, Status = 10
        }

        clsMonitorRFID monitor_RFID; 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();

        clsExportarExcelPlantilla xpExcel;
        DataSet dtsPedidos;

        DateTime dtCuenta = DateTime.Now;
        int iMinutosActualizacion = 10;
        string sTituloActualizacion = "";
        string sLista_Tags = "";


        bool bLeyendo = false; 
        double iAncho_Lst = 0;
        double dPorc_ClaveSSA = 0;
        double dPorc_DescripcionClaveSSA = 0;
        double dPorc_Existencia = 0;
        double dProporcion = 100;

        List<Tag> listaTags = new List<Tag>();
        ImpinjReader reader_RFID = new ImpinjReader();
        List<SII_ImpinjReader> readers = new List<SII_ImpinjReader>();
        Dictionary<string, SII_ImpinjReader> readersList = new Dictionary<string, SII_ImpinjReader>(); 

        bool Leyendo = false;
        int iReaders = 0;
        string sLectura = "";
        int iPiezas = 0;
        string sFormato = "##, ###,###, ###,##0";
        string sTitulo = "Monitor RFID";
        bool bGPO_Reconfigurado = false; 

        string sFILE_RFID_Reader = Application.StartupPath + @"\\RFID_Reader.txt"; 

        public FrmMonitor_RFID()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            dProporcion = 1;

            iAncho_Lst = listvwPedidos.Width;
            dPorc_ClaveSSA = ((colClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;
            dPorc_DescripcionClaveSSA = ((colDescripcionClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;
            dPorc_Existencia = ((colExistencia.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;

            dPorc_ClaveSSA = ((150 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_DescripcionClaveSSA = ((630 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_Existencia = ((120 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion; 


            int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);
            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;


            AjustarAnchoColumnas(); 

            AjustarTamañoFuente(); 
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmMonitor_RFID");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            sTituloActualizacion = lblTiempoActualizacion.Text; 
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0);

            lblTotalPiezas.BackColor = General.BackColorBarraMenu;
            lblTotalPiezas.BorderStyle = BorderStyle.None;

            ////Gn_RFID.MonitorRFID.Start(); 

            btnApagarGPO.Visible = false;
            btnApagarGPO.BackColor = Color.Red;
            btnApagarGPO.ForeColor = Color.White;


            ////Gn_RFID.DemonioRFID.BotonApagar_GPO = btnApagarGPO; 
            tmGPO.Interval = 100;
            tmGPO.Start();

            //monitor_RFID = new clsMonitorRFID(); 
        }

        private void AjustarTamañoFuente()
        {


            ////dPorc_ClaveSSA = ((colClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0));
            ////dPorc_DescripcionClaveSSA = ((colDescripcionClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0));
            ////dPorc_Existencia = ((colExistencia.Width * 1.0) / (listvwPedidos.Width * 1.0)); 
        }

        private void AjustarAnchoColumnas()
        {
            iAncho_Lst = listvwPedidos.Width;
            colClaveSSA.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_ClaveSSA);
            colDescripcionClaveSSA.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_DescripcionClaveSSA);
            colExistencia.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_Existencia);
        }

        private void FrmMonitor_RFID_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);

            tmActualizarInformacion.Interval = (1000 * 60) * iMinutosActualizacion;
            tmActualizarInformacion.Interval = (1000 * iMinutosActualizacion);

            ////ConfigurarCuentaRegresiva(); 
            ////CargarListaPedidos(); 

            ////Reader__RFID__000__Configurar();


            ConfigurarCuentaRegresiva();
            tmCuentaRegresiva.Enabled = true;
            tmCuentaRegresiva.Start();

            tmActualizarInformacion.Enabled = true;
            tmActualizarInformacion.Start(); 
        }

        private void FrmMonitorSurtimientoDePedidos_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    ActualizarInformacion(); 
                    break;

                default:
                    break;
            }
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ////CargarListaPedidos();
        }

        private void btnFuente_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = listvwPedidos.Font;
            ////font.Font = FramePedidos.Font;

            if (font.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listvwPedidos.Font = font.Font;
                ////FramePedidos.Font = font.Font; 
                ////CargarListaPedidos(); 
            }
        }
        #endregion Botones

        #region Funciones
        private void ConfigurarCuentaRegresiva()
        {
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, 0, iMinutosActualizacion);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss")); 
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            lblTotalPiezas.Text = iPiezas.ToString(sFormato);
            lblTotalPiezas.Text = string.Format("Número de Claves: {0}   Total de piezas: {0}", iPiezas.ToString(sFormato));
            ////btnExportarExcel.Enabled = false;

            ////cboJurisdicciones.SelectedIndex = 0;
            ////cboFarmacias.SelectedIndex = 0;
            ////cboStatusPed.SelectedIndex = 0;

            ////dtpFechaInicial.Value = dtpFechaInicial.Value.AddDays(-1); 

            lst.LimpiarItems();
            ////cboJurisdicciones.Focus();
        }
        #endregion Funciones

        #region CargarPedidos
        private void ActualizarInformacion()
        {
            string sRegresa = "";

            if (!bLeyendo)
            {
                bLeyendo = true;

                tmCuentaRegresiva.Stop();
                tmCuentaRegresiva.Enabled = false;

                tmActualizarInformacion.Stop();
                tmActualizarInformacion.Enabled = false;


                tmDetenerReader.Interval = 1000 * 10;
                tmDetenerReader.Enabled = true;
                tmDetenerReader.Start();


                sLista_Tags = "";
                listaTags = new List<Tag>();
                ////lst.LimpiarItems();

                lblTiempoActualizacion.Text = "Leyendo etiquetas magneticas ...";

                //////iPiezas = 0;
                //////lblTotalPiezas.Text = iPiezas.ToString(sFormato);
                //////lblTotalPiezas.Text = string.Format("Total de piezas: {0}", iPiezas.ToString(sFormato));

                Application.DoEvents();

                Gn_RFID.MonitorRFID.Readers_Iniciar();
                //monitor_RFID.Start();

                ///Cargar_Existencias();

                ////Reader__RFID__002__Detener(); 

                ////if (!Leyendo)
                {
                    //Reader__RFID__001__Iniciar();
                    Leyendo = true;
                }

                ////Cargar_Existencias();

            }
        }

        private void Cargar_Existencias()
        {
            string sNoLeer = ".";
            dtsPedidos = new DataSet(); 
            string sJurisdiccion = "", sFarmaciaPed = "", sStatusPed = "";
            DateTime dtFecha = General.FechaSistema;
            clsLeer leerErrores = new clsLeer(); 

            string sSql = string.Format("Exec spp_RPT_RFID_Existencias_Tags @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TAG = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sLista_Tags);

            Gn_RFID.RegistrarEvento(1002, sSql); 
            ////if (sLista_Tags != "")
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "Cargar_Existencias()");
                }
                else
                {
                    if (!leer.Leer())
                    {
                        sNoLeer = ".";
                        ////General.msjUser("No se encontro información con los criterios especificados.");
                    }
                    else
                    {
                        sNoLeer = ""; 
                        lst.LimpiarItems();
                        dtsPedidos = leer.DataSetClase;
                        lst.CargarDatos(leer.DataSetClase, true, false);
                        ////btnExportarExcel.Enabled = true;
                    }
                }
            }

            iPiezas = lst.TotalizarColumna(3);
            lblTotalPiezas.Text = string.Format("Total de piezas: {0}", iPiezas.ToString(sFormato));
            lblTotalPiezas.Text = string.Format("Número de Claves: {0:0,0}   Total de piezas: {1:0,0}", lst.Registros.ToString(sFormato), iPiezas.ToString(sFormato));
            lblTotalPiezas.Text = string.Format("Número de Claves: {0:0,0}   Total de piezas: {1:0,0}  {2}", lst.Registros, iPiezas, sNoLeer);

            ////lst.AjustarColumnas();
            AjustarAnchoColumnas(); 


            if (lst.Registros <= 0)
            {
                ////General.msjUser("No se encontro información con los criterios especificados.");
            }


            leerErrores.DataTableClase = leer.Tabla(3);
            if (leerErrores.Leer())
            {
                Gn_RFID.MonitorRFID.ListadoDeTagsErroneos = leerErrores.DataSetClase;
                ////monitor_RFID.ListadoDeTagsErroneos = leerErrores.DataSetClase;

                Gn_RFID.MonitorRFID.AlertaEncender();
                ////monitor_RFID.AlertaEncender(); 
            }

            bLeyendo = false;
        }
        #endregion CargarPedidos

        #region Eventos_Combos
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        #endregion Eventos_Combos

        private void tmCuentaRegresiva_Tick(object sender, EventArgs e)
        {
            dtCuenta = dtCuenta.AddSeconds(-1);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss")); 
        }

        private void tmActualizarInformacion_Tick(object sender, EventArgs e)
        {
           ActualizarInformacion();
        }

        private void FrmMonitor_RFID_FormClosing(object sender, FormClosingEventArgs e)
        {
            Gn_RFID.MonitorRFID.Stop(2, true);
            //monitor_RFID.Stop(2, true);
        }

        private void tmInformacion_Tick(object sender, EventArgs e)
        {
            tmInformacion.Enabled = false;
            tmInformacion.Stop();

            //////if (iReaders > 0)
            //////{
            //////    tmActualizarInformacion.Enabled = true;
            //////    tmActualizarInformacion.Start();
            //////}
            //////else 
            {
                sLista_Tags = iReaders.ToString();
                sLista_Tags = " 'XTR-0001' ";
                sLista_Tags = Gn_RFID.MonitorRFID.GetLista_Tags(); // " 'XTR-0001' "; 
                //sLista_Tags = monitor_RFID.GetLista_Tags(); 


                Cargar_Existencias();

                sLista_Tags = "";
                listaTags = new List<Tag>();

                /////Reader__RFID__001__Iniciar();


                ConfigurarCuentaRegresiva();
                tmCuentaRegresiva.Enabled = true;
                tmCuentaRegresiva.Start();

                tmActualizarInformacion.Enabled = true;
                tmActualizarInformacion.Start();
            }
        }

        private void tmDetenerReader_Tick(object sender, EventArgs e)
        {
            ////if (iReaders > 0)
            {
                tmDetenerReader.Stop();
                tmDetenerReader.Enabled = false;
                Gn_RFID.MonitorRFID.Stop(2);
                //monitor_RFID.Stop(2); 
                ////Reader__RFID__002__Detener(); 
            }

            tmInformacion.Interval = 1000 * 10;
            tmInformacion.Enabled = true;
            tmInformacion.Start();
        }

        private void tmGPO_Tick(object sender, EventArgs e)
        {
            if (!Gn_RFID.MonitorRFID.ReadersLeyendo)
            //if ( !monitor_RFID.ReadersLeyendo ) 
            {
                this.Text = string.Format("{0} ... {1}", sTitulo, "activando lectores");
                if (bGPO_Reconfigurado)
                {
                    this.Text = string.Format("{0}", sTitulo);
                }
            }
            else
            {
                if (!bGPO_Reconfigurado)
                {
                    bGPO_Reconfigurado = true;
                    tmGPO.Stop();
                    tmGPO.Interval = 1000;
                    tmGPO.Start();
                }

            }

            if (bGPO_Reconfigurado)
            {
                this.Text = string.Format("{0}", sTitulo);
                if (Gn_RFID.MonitorRFID.GPO_Encendido)
                //if ( monitor_RFID.GPO_Encendido ) 
                {
                    btnApagarGPO.Visible = true;
                    btnApagarGPO.Text = string.Format("Se detectaron Tags invalidos");
                    clsLeer leerErrores = new clsLeer();

                    leerErrores.DataSetClase = Gn_RFID.MonitorRFID.ListadoDeTagsErroneos; 
                    if (leerErrores.Leer())
                    {
                        if (!Gn_RFID.Monitor_TAGS_Erroneos)
                        {
                            FrmTAGS_Invalidos f = new FrmTAGS_Invalidos(ReaderTipo.Monitor);
                            ////f.MdiParent = this;
                            f.ShowDialog();
                            f.Activate();
                        }
                    }
                }
                else
                {
                    btnApagarGPO.Visible = false;
                }
            }
        }

        private void btnApagarGPO_Click(object sender, EventArgs e)
        {
            Gn_RFID.MonitorRFID.AlertaApagar();
            //monitor_RFID.AlertaApagar(); 
            btnApagarGPO.Visible = false; 
        }
    }
}
