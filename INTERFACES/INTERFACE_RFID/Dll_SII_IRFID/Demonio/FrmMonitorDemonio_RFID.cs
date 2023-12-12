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
    public partial class FrmMonitorDemonio_RFID : FrmBaseExt
    {

        enum Cols
        {
            IdJurisdiccion = 2, Jurisdiccion = 3,
            IdFarmacia = 4, Farmacia = 5, FolioPedido = 6, FechaPed = 7, FolioSurtido = 8, StatusPedido = 9, Status = 10
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();

        clsExportarExcelPlantilla xpExcel;
        DataSet dtsPedidos;

        DateTime dtCuenta = DateTime.Now;
        int iMinutosActualizacion = 20;
        string sTituloActualizacion = "";
        string sLista_Tags = "";

        double iAncho_Lst = 0;
        double dPorc_Area = 0;
        double dPorc_ClaveSSA = 0;
        double dPorc_DescripcionClaveSSA = 0;
        double dPorc_CodigoEAN = 0;
        double dPorc_DescripcionComercial = 0;
        double dPorc_Existencia = 0;
        double dProporcion = 100;

        List<Tag> listaTags = new List<Tag>();
        ImpinjReader reader_RFID = new ImpinjReader();
        List<ImpinjReader> readers = new List<ImpinjReader>();
        bool Leyendo = false;
        int iReaders = 0;
        string sLectura = "";
        int iPiezas = 0;
        string sFormato = "##, ###,###, ###,##0";
        string sTitulo = "Monitor RFID";
        bool bGPO_Reconfigurado = false; 


        public FrmMonitorDemonio_RFID()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            dProporcion = 1; 

            iAncho_Lst = listvwPedidos.Width;
            dPorc_ClaveSSA = ((colClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;
            dPorc_DescripcionClaveSSA = ((colDescripcionClaveSSA.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;
            dPorc_Existencia = ((colExistencia.Width * 1.0) / (listvwPedidos.Width * 1.0)) * 100;

            dPorc_Area = ((120 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;;
            dPorc_ClaveSSA = ((150 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_DescripcionClaveSSA = ((230 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_CodigoEAN = ((120 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_DescripcionComercial = ((190 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion;
            dPorc_Existencia = ((100 * 1.0) / (listvwPedidos.Width * 1.0)) * dProporcion; 


            int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);
            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;


            AjustarAnchoColumnas(); 

            AjustarTamañoFuente(); 
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmMonitorDemonio_RFID");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            sTituloActualizacion = lblTiempoActualizacion.Text; 
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0);

            lblTotalPiezas.BackColor = General.BackColorBarraMenu;
            lblTotalPiezas.BorderStyle = BorderStyle.None;

            btnApagarGPO.Visible = false; 
            btnApagarGPO.BackColor = Color.Red;
            btnApagarGPO.ForeColor = Color.White; 


            ////Gn_RFID.DemonioRFID.BotonApagar_GPO = btnApagarGPO; 
            tmGPO.Interval = 100;
            tmGPO.Start(); 
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
            colCodigoEAN.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_CodigoEAN);
            colDescripcionComercial.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_DescripcionComercial);
            colExistencia.Width = Convert.ToInt32(listvwPedidos.Width * dPorc_Existencia);
        }

        private void FrmMonitorDemonio_RFID_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);

            ////ActualizarInformacion();

            Cargar_Existencias(); 

            tmInformacion.Enabled = true;
            tmInformacion.Interval = iMinutosActualizacion * 1000;
            tmInformacion.Start();


            ConfigurarCuentaRegresiva();

            tmCuentaRegresiva.Enabled = true;
            tmCuentaRegresiva.Start();

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

        private void btnApagarGPO_Click(object sender, EventArgs e)
        {
            Gn_RFID.DemonioRFID.AlertaApagar();
            btnApagarGPO.Visible = false; 
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
            lblTotalPiezas.Text = string.Format("Total de piezas: {0}", iPiezas.ToString(sFormato));
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
        private void Cargar_Existencias()
        {
            string sJurisdiccion = "", sFarmaciaPed = "", sStatusPed = "";
            DateTime dtFecha = General.FechaSistema;
            clsLeer leerErrores = new clsLeer(); 

            string sSql = string.Format("Exec spp_RPT_RFID_Existencias_Tags_Areas @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}'  ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sLista_Tags);

            dtsPedidos = new DataSet();
            lst.LimpiarItems();

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
                        ////General.msjUser("No se encontro información con los criterios especificados.");
                    }
                    else
                    {
                        dtsPedidos = leer.DataSetClase;
                        lst.CargarDatos(leer.DataSetClase, true, false);
                        ////btnExportarExcel.Enabled = true;

                        ////leerErrores.DataTableClase = leer.Tabla(3);
                        ////if (leerErrores.Leer())
                        ////{
                        ////    Gn_RFID.MonitorRFID.ListadoDeTagsErroneos = leerErrores.DataSetClase;
                        ////    Gn_RFID.MonitorRFID.AlertaEncender();
                        ////}
                    }
                }
            }

            iPiezas = lst.TotalizarColumna(6);
            lblTotalPiezas.Text = string.Format("Total de piezas: {0}", iPiezas.ToString(sFormato));

            ////lst.AjustarColumnas();
            AjustarAnchoColumnas(); 


            if (lst.Registros <= 0)
            {
                ////General.msjUser("No se encontro información con los criterios especificados.");
            }            

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
          
        }

        private void FrmMonitorDemonio_RFID_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void ActualizarInformacion()
        {
            tmInformacion.Enabled = false;
            tmInformacion.Stop();

            tmCuentaRegresiva.Enabled = false;
            tmCuentaRegresiva.Stop();


            Cargar_Existencias();

            sLista_Tags = "";
            listaTags = new List<Tag>();

            /////Reader__RFID__001__Iniciar();

            System.Threading.Thread.Sleep(100); 

            ConfigurarCuentaRegresiva();
            tmCuentaRegresiva.Enabled = true;
            tmCuentaRegresiva.Start();

            tmInformacion.Enabled = true;
            tmInformacion.Start();
        }

        private void tmInformacion_Tick(object sender, EventArgs e)
        {
            ActualizarInformacion(); 
        }

        private void tmGPO_Tick(object sender, EventArgs e)
        {
            if (!Gn_RFID.DemonioRFID.ReadersLeyendo)
            {
                this.Text = string.Format("{0} ... {1}", sTitulo, "activando lectores");
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

                this.Text = string.Format("{0}", sTitulo);
                if (Gn_RFID.MonitorRFID.GPO_Encendido)
                {
                    btnApagarGPO.Visible = true;
                    btnApagarGPO.Text = string.Format("Se detectaron Tags invalidos");

                    if (!Gn_RFID.Monitor_TAGS_Erroneos)
                    {
                        FrmTAGS_Invalidos f = new FrmTAGS_Invalidos(ReaderTipo.Demonio);
                        ////f.MdiParent = this;
                        f.ShowDialog();
                        f.Activate();
                    }
                }
                else
                {
                    btnApagarGPO.Visible = false;
                }
            }
        }
    }
}
