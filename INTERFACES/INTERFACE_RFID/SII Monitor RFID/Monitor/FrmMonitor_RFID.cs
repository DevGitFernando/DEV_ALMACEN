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

namespace SII_Monitor_RFID.Monitor
{
    public partial class FrmMonitor_RFID : FrmBaseExt
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
        int iMinutosActualizacion = 10;
        string sTituloActualizacion = "";
        string sLista_Tags = "";

        double iAncho_Lst = 0;
        double dPorc_ClaveSSA = 0;
        double dPorc_DescripcionClaveSSA = 0;
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
        private void ActualizarInformacion()
        {
            string sRegresa = ""; 
            
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

            Reader__RFID__000__Configurar();

            ///Cargar_Existencias();

            ////Reader__RFID__002__Detener(); 

            ////if (!Leyendo)
            {
                //Reader__RFID__001__Iniciar();
                Leyendo = true; 
            }

            ////Cargar_Existencias();


        }

        private string GetLista_Tags()
        {
            string sRegresa = "";

            try
            {
                foreach (Tag tag in listaTags)
                {
                    sRegresa += string.Format("{0},", tag.Epc.ToString().Replace(" ", ""));
                }

                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }
            catch 
            {
                sRegresa = " '' "; 
            }            

            return sRegresa;
        }

        private void Cargar_Existencias()
        {
            string sJurisdiccion = "", sFarmaciaPed = "", sStatusPed = "";
            DateTime dtFecha = General.FechaSistema;

            string sSql = string.Format("Exec spp_RPT_RFID_Existencias_Tags @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @TAG = '{3}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, sLista_Tags);

            dtsPedidos = new DataSet();
            lst.LimpiarItems();

            if (sLista_Tags != "")
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
                    }
                }
            }

            iPiezas = lst.TotalizarColumna(3);
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
           ActualizarInformacion();
        }

        #region Eventos RFID  
        private void Reader__RFID__001__Iniciar()
        {
            iReaders = 0;

            Reader__RFID__002__Detener();

            Reader__RFID__000__Configurar(); 

            ////foreach (ImpinjReader reader in readers)
            ////{
            ////    try
            ////    {
            ////        reader.Start();
            ////        iReaders++;
            ////    }
            ////    catch { }
            ////}
        }

        private void Reader__RFID__002__Detener()
        {
            string sError = ""; 

            foreach (ImpinjReader reader in readers)
            {
                try
                {
                    reader.Stop();
                    reader.Disconnect(); 
                    ////iReaders--;

                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents(); 

                }
                catch (Exception ex)
                {
                    sError = ex.Message; 
                }
            }

            sLista_Tags = "";
            ////listaTags = new List<Tag>(); 
        }

        private void Cargar_Informacion_Readers()
        {
            string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Orden, Alias_Equipo, Server, Puerto, Status \n" +
                "From RFID_CFG_Conexion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' \n" + 
                "Order by Orden ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            readers = new List<ImpinjReader>(); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Informacion_Readers()");
                ///General.msjError("Ocurrió un error al obtener la lista de Lectores RFID.");                
            }
            else
            {
                Reader__RFID__002__Detener(); 
                while (leer.Leer())
                {
                    readers.Add(new ImpinjReader(leer.Campo("Server"), leer.Campo("Alias_Equipo")));
                }
            }
        }


        private void Reader__RFID__000__Configurar()
        {
            sLista_Tags = ""; 
            listaTags = new List<Tag>(); 

            try
            {
                ////readers.Add(new ImpinjReader(Gn_Monitor_RFID.Direccion_RFID, "Reader #1"));
                ////readers.Add(new ImpinjReader("192.168.254.222", "Reader #2"));

                Cargar_Informacion_Readers(); 

                // Loop through the List of readers to configure and start them.
                foreach (ImpinjReader reader in readers)
                {
                    // Connect to the reader.
                    // Change the ReaderHostname constant in SolutionConstants.cs 
                    // to the IP address or hostname of your reader.
                    reader.Connect();


                    // Get the factory settings for the reader. We will
                    // use these are a starting point and then modify
                    // the settings we're interested in.
                    Settings settings = reader.QueryDefaultSettings();
                    
                    // Send a report for every tag seen.
                    settings.Report.Mode = ReportMode.BatchAfterStop;
                    
                    //settings.ReaderMode = ReaderMode.AutoSetDenseReader;
                    settings.SearchMode = SearchMode.DualTarget;
                    
                    // Include the antenna number in the tag report.
                    settings.Report.IncludeAntennaPortNumber = false;

                    //activar o desactivar antenas
                    settings.Antennas[0].IsEnabled = true; // usar la antena 1 y 2
                    settings.Antennas[1].IsEnabled = true;
                    settings.Antennas[2].IsEnabled = true;
                    settings.Antennas[3].IsEnabled = false;


                    double dPotencia = 32.5;
                    dPotencia = 27; 

                    //asignamos intensida en las antenasDigimedics
                    ////settings.Antennas[1].TxPowerInDbm = Convert.ToDouble(ConfigurationManager.AppSettings.Get("PowerAtn1"));
                    settings.Antennas[0].TxPowerInDbm = Convert.ToDouble(dPotencia);
                    settings.Antennas[1].TxPowerInDbm = Convert.ToDouble(dPotencia);
                    settings.Antennas[2].TxPowerInDbm = Convert.ToDouble(dPotencia);
                    settings.Antennas[3].TxPowerInDbm = Convert.ToDouble(dPotencia);
                    settings.Session = 2;
                    // Apply the new settings.
                    reader.ApplySettings(settings);

                    //////// Assign the TagsReported handler.
                    //////// This specifies which method to call
                    //////// when tags reports are available.
                    reader.TagOpComplete += OnTagOpComplete;

                    // Assign the TagsReported handler.
                    // This specifies which method to call
                    // when tags reports are available.
                    reader.TagsReported += OnTagsReported;


                    reader.Start();

                    iReaders++; 
                }


                //////// Stop all the readers and disconnect from them.
                //////foreach (ImpinjReader reader in readers)
                //////{
                //////    // Stop reading.
                //////    reader.Stop();

                //////    // Disconnect from the reader.
                //////    reader.Disconnect();
                //////}

            }
            catch (OctaneSdkException e)
            {
                // Handle Octane SDK errors.
                Console.WriteLine("Octane SDK exception: {0}", e.Message);
            }
            catch (Exception e)
            {
                // Handle other .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
            }

        }

        // This event handler is called asynchronously 
        // when tag reports are available.
        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            sLectura = ""; 

            //// Loop through each tag in the report 
            //// and print the data.
            foreach (Tag tag in report) 
            {
                listaTags.Add(tag); 
                Console.WriteLine("EPC : {0}", tag.Epc);
            }

            iReaders--;
        }

        // This event handler will be called when tag 
        // operations have been executed by the reader.
        private void OnTagOpComplete(ImpinjReader reader, TagOpReport report)
        {
            ///iReaders--;
            //////listaTags = new List<Tag>();

            ////////// Loop through all the completed tag operations
            //////foreach (TagOpResult result in report)
            //////{

            //////    //// Was this completed operation a QT operation?
            //////    if (result is TagQtOpResult)
            //////    {
            //////        listaTags.Add(result.Tag); 

            //////        //// Cast it to the correct type.
            //////        TagQtOpResult qtResult = result as TagQtOpResult;

            //////        //// QT operation failed.
            //////        Console.WriteLine("QT operation complete. Status : {0}", qtResult.Result);
            //////    }
            //////}

            //////sLista_Tags = " 'XTR-0001' ";
            //////sLista_Tags = GetLista_Tags(); // " 'XTR-0001' "; 
        }
        #endregion Eventos RFID

        private void FrmMonitor_RFID_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reader__RFID__002__Detener(); 
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
                sLista_Tags = GetLista_Tags(); // " 'XTR-0001' "; 


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
                Reader__RFID__002__Detener(); 
            }

            ////while (iReaders > 0)
            ////{
            ////    System.Threading.Thread.Sleep(10); 
            ////}

            tmInformacion.Interval = 1000 * 10;
            tmInformacion.Enabled = true;
            tmInformacion.Start();
        }
    }
}
