using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;

using Dll_SII_IRFID.Demonio;
using Dll_SII_IRFID.Monitor; 

namespace Dll_SII_IRFID
{
    public enum ReaderTipoLectura
    {
        General = 0, Entrada = 1, Salida = 2 
    }

    public enum ReaderTipo
    {
        Ninguno = 0, 
        Demonio = 1, 
        Monitor = 2
    }

    /// <summary>
    /// Puertos del Periferico 
    /// </summary>
    public enum GPO_Puertos : ushort
    {
        /// <summary>
        /// Luz
        /// </summary>
        Port_01 = 1, 
        
        /// <summary>
        /// Sonido
        /// </summary>
        Port_02 = 2, 
        
        /// <summary>
        /// No implementado 
        /// </summary>
        Port_03 = 3,

        /// <summary>
        /// No implementado 
        /// </summary>
        Port_04 = 4 
    }

    public class Gn_RFID
    {
        #region Declaracion de Variables
        private static clsDatosApp dpDatosApp = new clsDatosApp("Farmacia", "");

        private static bool bConexionWebCEDIS_Establecida = false;
        private static clsDatosWebService datosDeWebServicePedidos = new clsDatosWebService();

        private static bool bLeyendo_Informacion_RFID = false;
        private static bool bMonitor_Cargado = false;
        private static bool bMonitor_TAGS_Erroneos = false; 
        private static string sDireccion_RFID = "192.168.254.221";

        private static clsDemonioRFID _demonioRFID;
        private static clsMonitorRFID _monitorRFID; 

        #endregion Declaracion de Variables

        #region Constructor
        static Gn_RFID()
        {
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
        }
        #endregion Constructor

        #region Propieades Dll
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }
        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }
        public static string Version
        {
            get { return dpDatosApp.Version; }
        }
        #endregion Propieades Dll

        #region Servicios Web
        #endregion Servicios Web

        #region Propiedades
        public static bool Leyendo_Informacion_RFID
        {
            get { return bLeyendo_Informacion_RFID; }
            set { bLeyendo_Informacion_RFID = value; }
        }

        public static bool MonitorCargado
        {
            get { return bMonitor_Cargado; }
            set { bMonitor_Cargado = value; }
        }

        public static bool Monitor_TAGS_Erroneos        
        {
            get { return bMonitor_TAGS_Erroneos; }
            set { bMonitor_TAGS_Erroneos = value; }
        }

        public static string Direccion_RFID
        {
            get { return sDireccion_RFID; }
            set { sDireccion_RFID = value;  } 
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        public static clsDemonioRFID DemonioRFID
        {
            get 
            {
                if (_demonioRFID == null)
                {
                    _demonioRFID = new clsDemonioRFID();
                }

                return _demonioRFID; 
            }

            set { _demonioRFID = value; }
        }

        public static clsMonitorRFID MonitorRFID
        {
            get
            {
                if (_monitorRFID == null)
                {
                    _monitorRFID = new clsMonitorRFID ();
                }

                return _monitorRFID;
            }

            set { _monitorRFID = value; }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Log RFID 
        private static StreamWriter logReaders;
        private static string sRutaLog = Application.StartupPath + @"\Log_Readers.txt";

        private static int iLineas = 0;
        private static LongitudLog iMB_File = LongitudLog.MB_01;
        private static int iTamFile = (1024 * 1024) * (int)LongitudLog.MB_01;
        private static basGenerales Fg = new basGenerales(); 

        public static void RegistrarEvento(int Tipo, string Mensaje)
        {
            DateTime dt = DateTime.Now;
            string sTexto = "";
            string sContenido = "";

            string sMarcaTiempo = string.Format("{0}{1}{2} {3}{4}{5}{6}",
                Fg.PonCeros(dt.Year, 4), Fg.PonCeros(dt.Month, 2), Fg.PonCeros(dt.Day, 2),
                Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2),
                Fg.PonCeros(dt.Millisecond, 4));


            sTexto = string.Format("{0}     Tipo : {1:000}      {2} ", sMarcaTiempo, Tipo, Mensaje);
            if (Mensaje == "")
            {
                sTexto = string.Format("{0}     Tipo : {1:000}     ", sMarcaTiempo, Tipo);
            }

            ////logReaders = new StreamWriter(sRutaLog, true);
            ////logReaders.Close();

            try
            {
                if (!File.Exists(sRutaLog))
                {
                    if (logReaders == null)
                    {
                        logReaders = new StreamWriter(sRutaLog, true, System.Text.Encoding.UTF8);
                        iLineas = 0;
                    }
                }
                else
                {
                    FileInfo fl = new FileInfo(sRutaLog);
                    if (fl.Length >= iTamFile)
                    {
                        try
                        {
                            //// Eliminar el archivo original. 
                            File.Delete(sRutaLog);
                        }
                        catch { }
                    }

                    logReaders = new StreamWriter(sRutaLog, true, System.Text.Encoding.UTF8);
                    iLineas = 0;
                }

                //logReaders.WriteLine(string.Format("Registro : {0}   Tipo : {1}     Mensaje : {2} ", sMarcaTiempo, Tipo, Mensaje));
                logReaders.WriteLine(sTexto);
                iLineas++;

                if (iLineas >= 1)
                {
                    logReaders.Close();
                    logReaders = null;
                    ////logReaders = new StreamWriter(sFile, true, System.Text.Encoding.UTF8);
                    iLineas = 0;
                }
            }
            catch { }
        }
        #endregion Log RFID 

    }
}
