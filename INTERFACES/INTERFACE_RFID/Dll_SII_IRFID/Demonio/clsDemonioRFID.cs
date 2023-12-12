using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO; 
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;

using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

using Impinj.OctaneSdk;
using Dll_SII_IRFID; 

namespace Dll_SII_IRFID.Demonio
{
    public class clsDemonioRFID
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consultas;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales();

        List<Tag> listaTags = new List<Tag>();
        ImpinjReader reader_RFID = new ImpinjReader();
        List<ImpinjReader> readers = new List<ImpinjReader>();
        Dictionary<string, SII_ImpinjReader> readersList = new Dictionary<string, SII_ImpinjReader>();
        ReaderTipoLectura tipoDeLectura = ReaderTipoLectura.General;
        string[] sListaDeTiposDeAntenas = null; 

        int iReaders = 0;
        string sLista_Tags = "";
        string sLectura = "";
        bool bReadersCargados = false;
        bool bReaderLeyendo = false; 

        Thread thActivarLectores;
        Button _btnApagar_GPO = null;
        bool bGPO_Encendido = false;
        int iTAG_Incorrectos = 0;
        Dictionary<string, string> sListaTagsErroneos = new Dictionary<string, string>(); 
        DataSet dtsListaTagsErroneos = new DataSet(); 



        ////StreamWriter logReaders;
        ////string sRutaLog = Application.StartupPath + @"\Log_Readers.txt";

        ////int iLineas = 0; 
        ////LongitudLog iMB_File = LongitudLog.MB_01;
        ////int iTamFile = (1024 * 1024) * (int)LongitudLog.MB_01;
        ////basGenerales Fg = new basGenerales(); 

        public clsDemonioRFID(): this(null) 
        { 
        }

        public clsDemonioRFID(string[] ListaDeTiposDeAntenas)
        {
            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, Gn_RFID.DatosApp, "clsDemonioRFID");

            Error = new clsGrabarError(General.DatosConexion, Gn_RFID.DatosApp, "clsDemonioRFID");
            
            sListaDeTiposDeAntenas = ListaDeTiposDeAntenas; 

            Cargar_Informacion_Readers();
            Preparar_ListadoTagsErroneos();
        }

        #region Propiedades Publicas 
        public bool GPO_Encendido
        {
            get { return bGPO_Encendido; }
        }

        public int TAG_Incorrectos
        {
            get { return iTAG_Incorrectos; }
        }

        //public Button BotonApagar_GPO
        //{
        //    ////set 
        //    ////{ 
        //    ////    _btnApagar_GPO = value;
        //    ////    _btnApagar_GPO.Visible = _btnApagar_GPO.Visible ? false : _btnApagar_GPO.Visible; 
        //    ////}
        //}

        public DataSet ListadoDeTagsErroneos
        {
            get { return dtsListaTagsErroneos; }
        }

        public bool ReadersCargados
        {
            get { return bReadersCargados; }
        }

        public bool ReadersLeyendo
        {
            get { return bReaderLeyendo; }
        }

        public ReaderTipoLectura TipoDeLectura
        {
            get { return tipoDeLectura; }
            set { tipoDeLectura = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public void Start()
        {
            thActivarLectores = new Thread(Reader__RFID__001__Iniciar);
            thActivarLectores.Name = "Manejo de lectores RFID";
            thActivarLectores.Start(); 
        }

        public void Stop()
        {
            Stop(1);
        }

        public void Stop(int Intentos)
        {
            for (int i = 1; i <= Intentos; i++)
            {
                if (thActivarLectores != null)
                {
                    Reader__RFID__002__Detener();

                    try
                    {
                        thActivarLectores.Interrupt();
                        thActivarLectores.Abort();
                    }
                    catch
                    {
                    }
                }
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void Preparar_ListadoTagsErroneos()
        {
            sListaTagsErroneos = new Dictionary<string, string>();
            dtsListaTagsErroneos = new DataSet("Listado_Tags"); 

            DataTable dtTable = new DataTable("TAGS");

            dtTable.Columns.Add("Lector", System.Type.GetType("System.String"));
            dtTable.Columns.Add("TAG", System.Type.GetType("System.String"));
            dtsListaTagsErroneos.Tables.Add(dtTable);

        }

        private void AgregarTagErroneo(string Sender, string TAG)
        {
            string sKey = Sender + TAG;

            if (!sListaTagsErroneos.ContainsKey(sKey))
            {
                sListaTagsErroneos.Add(sKey, TAG);

                object[] obj = { Sender, TAG };
                dtsListaTagsErroneos.Tables[0].Rows.Add(obj); 
            }
        }

        private string GetListaTipoDeAntenas()
        {
            string sRegresa = "";

            if (sListaDeTiposDeAntenas != null)
            {
                foreach (string sAntenaTipo in sListaDeTiposDeAntenas)
                {
                    sRegresa += string.Format("{0},", sAntenaTipo);
                }

                if (sRegresa != "")
                {
                    sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
                }
            }

            if (sRegresa == "")
            {
                sRegresa = "-1";
            }

            return sRegresa;
        }

        private void Cargar_Informacion_Readers()
        {
            SII_ImpinjReader readerLocal = new SII_ImpinjReader();
            string sListaTipoAntenas = GetListaTipoDeAntenas(); 

            string sSql = string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Orden, Alias_Equipo, Server, Puerto, EnviaAlertas, TipoDeLectura, Status \n" +
                "From RFID_CFG_Conexion (NoLock) \n" + 
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' and TipoDeLectura In ( {3} ) \n" +
                "Order by Orden ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, (int)tipoDeLectura);

            readers = new List<ImpinjReader>();
            readersList = new Dictionary<string, SII_ImpinjReader>();
            Preparar_ListadoTagsErroneos(); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_Informacion_Readers()");
                ///General.msjError("Ocurrió un error al obtener la lista de Lectores RFID.");                
            }
            else
            {
                ////Reader__RFID__002__Detener();
                while (leer.Leer())
                {
                    readerLocal = new SII_ImpinjReader(leer.Campo("Server"), leer.Campo("Alias_Equipo"));
                    readerLocal.EnviaAlertas = leer.CampoBool("EnviaAlertas");
                    readerLocal.TipoDeLectura = leer.CampoInt("TipoDeLectura");

                    readers.Add(readerLocal);
                    readersList.Add(readerLocal.Address, readerLocal); 
                }
            }
        }

        #region Registrar lectura de TAG 
        private bool RegistrarLecturaTAG(string Sender, string TAG)
        {
            bool bRegresa = false;
            string sSql = "";

            TAG = TAG.Replace(" ", "");

            sSql = string.Format("Exec spp_Mtto_RFID_Tags_Inventario " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Lector = '{3}', @TAG = '{4}' ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Sender, TAG); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "RegistrarLecturaTAG"); 
            }
            else
            {
                if (!leer.Leer())
                {
                    bRegresa = true;
                }
                else
                {
                    Gn_RFID.RegistrarEvento(102, string.Format("TAG : {0} no es valido", TAG));
                    if (leer.CampoBool("EnviaAlertas"))
                    {
                        iTAG_Incorrectos++;
                        AgregarTagErroneo(Sender, TAG); 
                        AlertaEncender();
                        //General.msjAviso(leer.Campo("Mensaje"));
                    }
                }
            }


            return bRegresa; 
        }
        #endregion Registrar lectura de TAG

        #region Eventos RFID
        private void Reader__RFID__001__Iniciar()
        {
            iReaders = 0;

            Reader__RFID__002__Detener();

            Reader__RFID__000__Configurar(); 
        }

        private void Reader__RFID__000__Configurar()
        {
            sLista_Tags = "";
            listaTags = new List<Tag>();

            try
            {
                ////readers.Add(new ImpinjReader(Gn_Monitor_RFID.Direccion_RFID, "Reader #1"));
                ////readers.Add(new ImpinjReader("192.168.254.222", "Reader #2"));

                bReadersCargados = false;
                bReaderLeyendo = false; 

                Cargar_Informacion_Readers();
                Gn_RFID.RegistrarEvento(0, "");
                Gn_RFID.RegistrarEvento(0, "");
                Gn_RFID.RegistrarEvento(0, "");

                //// Loop through the List of readers to configure and start them.
                foreach (ImpinjReader reader in readers)
                {
                    try
                    {
                        Gn_RFID.RegistrarEvento(201, string.Format("Estableciendo conexión con : {0}", reader.Address));

                        //// Connect to the reader.
                        //// Change the ReaderHostname constant in SolutionConstants.cs 
                        //// to the IP address or hostname of your reader.
                        reader.Connect();


                        // Get the factory settings for the reader. We will
                        // use these are a starting point and then modify
                        // the settings we're interested in.
                        Settings settings = reader.QueryDefaultSettings();


                        ///// Send a report for every tag seen.                   
                        //settings.Report.Mode = ReportMode.Individual;


                        settings.ReaderMode = ReaderMode.MaxThroughput; // Todo lo que pase enfrente del lector 


                        /////settings.ReaderMode = ReaderMode.AutoSetDenseReader;
                        settings.SearchMode = SearchMode.TagFocus;


                        //// Include the antenna number in the tag report.
                        settings.Report.IncludeAntennaPortNumber = false;

                        //activar o desactivar antenas
                        settings.Antennas[0].IsEnabled = true; // usar la antena 1 y 2
                        settings.Antennas[1].IsEnabled = true;
                        settings.Antennas[2].IsEnabled = false;
                        settings.Antennas[3].IsEnabled = false;


                        double dPotencia = 32.5;
                        double dSensibilidad = -70;
                        dPotencia = 28;
                        dSensibilidad = -64;

                        //asignamos intensida en las antenasDigimedics
                        ////settings.Antennas[1].TxPowerInDbm = Convert.ToDouble(ConfigurationManager.AppSettings.Get("PowerAtn1"));
                        settings.Antennas[0].TxPowerInDbm = Convert.ToDouble(dPotencia);
                        settings.Antennas[1].TxPowerInDbm = Convert.ToDouble(dPotencia);
                        settings.Antennas[2].TxPowerInDbm = Convert.ToDouble(dPotencia);
                        settings.Antennas[3].TxPowerInDbm = Convert.ToDouble(dPotencia);
                        settings.Session = 1;

                        settings.Antennas[0].RxSensitivityInDbm = dSensibilidad;
                        settings.Antennas[1].RxSensitivityInDbm = dSensibilidad;
                        settings.Antennas[2].RxSensitivityInDbm = dSensibilidad;
                        settings.Antennas[3].RxSensitivityInDbm = dSensibilidad;


                        ///// Apply the new settings.
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
                        
                        readersList[reader.Address].ConexionEstablecida = true;
                        
                        Gn_RFID.RegistrarEvento(202, string.Format("Conexión establecida con : {0}", reader.Address));

                    }
                    catch (OctaneSdkException e1)
                    {
                        //// Handle Octane SDK errors.
                        Console.WriteLine("Octane SDK exception: {0}", e1.Message);
                        Gn_RFID.RegistrarEvento(1, string.Format("Octane SDK exception: {0}", e1.Message));
                    }
                    catch (Exception e2)
                    {
                        //// Handle other .NET errors.
                        Console.WriteLine("Exception : {0}", e2.Message);
                        Gn_RFID.RegistrarEvento(2, string.Format("Exception : {0}", e2.Message));
                    }
                }

            }
            catch (OctaneSdkException e)
            {
                // Handle Octane SDK errors.
                Console.WriteLine("Octane SDK exception: {0}", e.Message);
                Gn_RFID.RegistrarEvento(3, string.Format("Octane SDK exception: {0}", e.Message));
            }
            catch (Exception e)
            {
                // Handle other .NET errors.
                Console.WriteLine("Exception : {0}", e.Message);
                Gn_RFID.RegistrarEvento(4, string.Format("Exception : {0}", e.Message));
            }

            bReadersCargados = true;
            bReaderLeyendo = iReaders > 0; 
        }

        private void Reader__RFID__002__Detener()
        {
            string sError = "";
            bReadersCargados = false;
            bReaderLeyendo = false; 


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
        }    


        //// This event handler is called asynchronously 
        //// when tag reports are available.
        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            sLectura = "";

            //// Loop through each tag in the report 
            //// and print the data.
            foreach (Tag tag in report)
            {
                listaTags.Add(tag);
                Console.WriteLine("EPC : {0}", tag.Epc);

                Gn_RFID.RegistrarEvento(101, string.Format("TAG : {0}", tag.Epc.ToString().Replace(" ", "")));

                RegistrarLecturaTAG(sender.Name, tag.Epc.ToString());
            }

            iReaders--;
        }

        //// This event handler will be called when tag 
        //// operations have been executed by the reader.
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

        #region Eventos GPO 
        private void SetStatusGPO(bool Status)
        {
            try
            {
                foreach (ImpinjReader reader in readers)
                {
                    try
                    {
                        if (readersList[reader.Address].ConexionEstablecida)
                        {
                            reader.SetGpo((int)GPO_Puertos.Port_01, Status);
                            reader.SetGpo((int)GPO_Puertos.Port_02, Status);
                        }
                    }
                    catch (OctaneSdkException e1)
                    {
                        // Handle Octane SDK errors.
                        Console.WriteLine("Octane SDK exception: {0}", e1.Message);
                    }
                    catch (Exception e2)
                    {
                        // Handle other .NET errors.
                        Console.WriteLine("Exception : {0}", e2.Message);
                    }
                }
            }
            catch { }
        }

        public void AlertaEncender()
        {
            if (!bGPO_Encendido)
            {
                bGPO_Encendido = true;
                ////_btnApagar_GPO.Visible = true;
                SetStatusGPO(true);
            }
        }

        public void AlertaApagar()
        {
            if (bGPO_Encendido)
            {
                bGPO_Encendido = false;
                iTAG_Incorrectos = 0; 
                ////_btnApagar_GPO.Visible = false;
                SetStatusGPO(false);

                Preparar_ListadoTagsErroneos(); 
            }
        }
        #endregion Eventos GPO

        #endregion Funciones y Procedimientos Privados
    }
}
