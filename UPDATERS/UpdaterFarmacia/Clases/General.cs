using System;
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

using UpdateFarmacia.Data;
//using UpdateFarmacia.Errores;
//using UpdateFarmacia.Usuarios_y_Permisos;
using UpdateFarmacia.FuncionesGenerales;

namespace UpdateFarmacia
{
    public enum ePosicion
    {
        Izquierda = 0, Derecha = 1
    }
    
    /// <summary>
    /// Clase para el manejo de variables General.es. 
    /// Esta clase se carga automaticamente al momento de que se carga el programa.
    /// </summary>
    public static class General
    {
        public enum FormatoFecha
        {
            YMD = 1, DMY = 2
        }

        // Se debe agregar una propiedad por cada variable General. que se agregue
        // a la clase. Todas las variables y las propiedades deben ser Static.        
        // Esta clase se carga automaticamente al momento de que se carga el programa.

        #region Declaración de variables
        public static basGenerales Fg = new basGenerales();

        // private static string strCnnString = "";
        // private static object objWebService = null;
        // private static bool bUsarRedLocal = true;
        private static string strUrl = "";
        private static clsDatosConexion DatosCnn = new clsDatosConexion();
        private static string sPaginaWebASMX = "wsConexion";
        // private static bool bUsarWebService = true;
        // private static bool bMultiEntidadesDeNegocios = false;
        public static bool bModoDesarrollo = false;
        public static bool bSolicitarConfigWeb = false;
        public static bool bSolicitarConfigLocal = true;

        private static Color colorFormasBackColor = Color.WhiteSmoke;
        private static Color colorBarraMenuBackColor = Color.LightSteelBlue;

        private static string sLetraSO = Fg.Left(Environment.SystemDirectory.ToString(),1).Trim();
        private static string sArchivoConfigIni = "Config.ini";
        private static string sIp = "", sNombreHost = "", sMacAddress = "";

        // Datos de identificacion del Dll
        private static string sModulo = "SC_SolutionsSystem";
        private static string sVersion = "2.0.5.1";
        private static clsDatosApp dpDatosApp = new clsDatosApp("SC_SolutionsSystem", "3.0.0.0");
        public static string RutaLogo = Application.StartupPath + @"\SC_LOGO.jpg";

        public static bool EsServidorGeneral = false;
        public static bool EsAlmacen = false; 
        public static bool ActualizacionManual = false; 
        public static bool WithOutEncryption = false; 
        public static string ForzarUpdate = "1";
        public static string CfgIniOficinaCentral = "OficinaCentral";
        public static string CfgIniPuntoDeVenta = "FarmaciaPtoVta";



        private static List<string> sListaMacAddress = new List<string>();

        #region Variables Generales de sistema
        // private static string sIdEstado = "25";
        //  private static string sIdEntidad = "0001";
        // private static string sRutaReportes = "";
        // private static bool bImpresionWeb = false; 
        // private static ImageList imgListaIconosNav;
        private static DataSet dtsNavegacion = new DataSet();
        // private static bool bLoadNavegacion = false;
        // private static ePosicion iPosicion = ePosicion.Izquierda;
        private static DateTime dtFechaSistema;
        private static bool bFechaActualizada = false;
        private static Icon icoIconoSistema;

        public static bool EsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");

        /// <summary>
        /// Variable que contiene todos los objetos de un Dll o Exe
        /// </summary>
        private static Type[] myTypes;

        #endregion Variables Generales de sistema

        #endregion Declaración de variables

        #region Constructor
        static General()
        {
            ConexionesRed();
            CargarIcono();
            //ObtenerDatosDll();
        }
        #endregion Constructor

        #region Propiedades publicas Generales de sistema 
        public static Color BackColorBarraMenu
        {
            get { return colorBarraMenuBackColor; }
            set { colorBarraMenuBackColor = value; }
        }

        /// <summary>
        /// Determina el color de fondo de las pantallas
        /// </summary>
        public static Color FormaBackColor
        {
            get { return colorFormasBackColor; }
            set { colorFormasBackColor = value; }
        }

        public static Icon IconoSistema
        {
            get { return icoIconoSistema; }
            set { icoIconoSistema = value; }
        }
        #endregion

        #region Propieades Dll 
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }

        public static string Modulo
        {
            get { return sModulo; }
        }

        public static string Version
        {
            get { return sVersion; }
        }
        #endregion Propieades Dll

        #region Propiedades publicas  
        /// <summary>
        /// Devuelve o establece la URL del servidor web.
        /// </summary>
        public static string Url
        {
            get { return strUrl; }
            set { strUrl = value; }
        }
        
        /// <summary>
        /// Devuelve o estable los datos de la conexión que se emplea para accesar al servidor de base de datos.
        /// </summary>
        public static clsDatosConexion DatosConexion
        {
            get { return DatosCnn; } 
            set { DatosCnn = value; }
        }

        /// <summary>
        /// Obtiene o estable la pagina que contiene el Servicio Web
        /// </summary>
        public static string PaginaASMX
        {
            get
            {
                return "/" + sPaginaWebASMX + ".asmx";
            }
            set
            {
                sPaginaWebASMX = value;
                sPaginaWebASMX = sPaginaWebASMX.Replace("/", "");
                sPaginaWebASMX = sPaginaWebASMX.Replace(".", "");
                sPaginaWebASMX.Trim();

                if (Fg.Right(sPaginaWebASMX, 4).ToUpper() == "ASMX")
                {
                    sPaginaWebASMX = Fg.Left(sPaginaWebASMX, sPaginaWebASMX.Length - 4);
                }

            }
        }

        /// <summary>
        /// Obtiene la unidad de disco donde se almacena el sistema operativo.
        /// </summary>
        public static string UnidadSO
        {
            get { return sLetraSO; }
        }

        public static string ArchivoIni
        {
            get { return sArchivoConfigIni; }
            set
            {
                //if (value.Substring(value.Length -4).ToUpper() == ".INI" )
                if ( value.ToUpper().Contains(".INI") )
                    sArchivoConfigIni = value;
                else
                    sArchivoConfigIni = value + ".ini";
            }
        }
        #endregion

        #region Funciones y Procedimientos privados
        private static void ObtenerDatosDll()
        {
           ////Assembly AssemblyCargado = Assembly.Load(sModulo);
           //// AssemblyName x = new AssemblyName(AssemblyCargado.FullName);
           AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
           dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
           // dpDatosApp = new clsDatosApp("SC_SolutionsSystem", "3.0.0.0");
        }

        private static void ConexionesRed()
        {
            // Nombre del host
            sNombreHost = Dns.GetHostName();
            sMacAddress = ""; 

            // Ip del host
            // IPAddress[] myIp = Dns.GetHostAddresses(sNombreHost);
            // sIp = myIp[0].ToString();

            try
            {
                sMacAddress = "";

                //// Ip del host
                foreach (IPAddress myIp in Dns.GetHostAddresses(General.NombreEquipo))
                {
                    if (!myIp.IsIPv6LinkLocal)
                    {
                        sIp = myIp.ToString();
                        break;
                    }
                }

                sListaMacAddress = new List<string>();
                //// Mac del host
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface adaptador in nics)
                {
                    PhysicalAddress physical = adaptador.GetPhysicalAddress();
                    sMacAddress = physical.ToString();

                    sListaMacAddress.Add(sMacAddress);
                }

            }
            catch
            {
                sMacAddress = "";
            }
        }

        /// <summary>
        /// Devuelve la ip del equipo local
        /// </summary>
        public static string Ip
        {
            get { return sIp; }
        }

        /// <summary>
        /// Devuelve el nombre de red del equipo local
        /// </summary>
        public static string NombreEquipo
        {
            get { return sNombreHost; }
        }

        /// <summary>
        /// Devuelve la dirección Mac del equipo local
        /// </summary>
        public static string MacAddress
        {
            get { return sMacAddress; }
        }

        private static void CargarIcono()
        {
            Form f = new Form();
            icoIconoSistema = f.Icon;
            f = null;
        }

        public static List<string> ListaMacAddress
        {
            get { return sListaMacAddress; }
        }

        public static string ListaDeMacAddress(string Sepador)
        {
            string sRegresa = "";

            foreach (string sMac in sListaMacAddress)
            {
                sRegresa += string.Format("{0}{1}{0}, ", Sepador, sMac);
            }

            if (sRegresa != "")
            {
                sRegresa = sRegresa.Trim();
                sRegresa = Fg.Left(sRegresa, sRegresa.Length - 1);
            }

            return sRegresa;
        }
        #endregion

        #region Cargar Dll
        public static Process GetProceso(string NombreProceso)
        {
            Process myProceso = null;
            try
            {
                myProceso = Process.GetProcessesByName(FormatearNombreProceso(NombreProceso))[0];
            }
            catch { }

            return myProceso;
        }

        public static Process[] GetProcesos(string NombreProceso)
        {
            Process[] myProceso = null;
            try
            {
                myProceso = Process.GetProcessesByName(FormatearNombreProceso(NombreProceso));
            }
            catch { }

            return myProceso;
        }

        private static string FormatearNombreProceso(string NombreProceso)
        {
            NombreProceso = NombreProceso.Replace(".EXE", "");
            NombreProceso = NombreProceso.Replace(".exe", "");
            NombreProceso = NombreProceso.Replace(".DLL", "");
            NombreProceso = NombreProceso.Replace(".dll", "");
            return NombreProceso;
        }

        public static void TerminarProceso(string NombreProceso)
        {
            try
            {
                GetProceso(NombreProceso).Kill();
            }
            catch { }
        }

        public static void TerminarProcesos(string[] NombresProcesos)
        {
            try
            {
                foreach (string NombreProceso in NombresProcesos)
                {
                    try
                    {
                        Process[] myProceso = GetProcesos(NombreProceso);
                        foreach (Process proceso in myProceso)
                        {
                            proceso.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ProcesoEnEjecucion()
        {
            return ProcesoEnEjecucion(Process.GetCurrentProcess().ProcessName);
        }

        public static bool ProcesoEnEjecucionUnica()
        {
            return ProcesoEnEjecucionUnica(Process.GetCurrentProcess().ProcessName);
        }

        public static bool ProcesoEnEjecucionUnica(string NombreProceso)
        {
            NombreProceso = FormatearNombreProceso(NombreProceso);
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length == 1;
            //Process[] pro = Process.GetProcessesByName(NombreProceso);

            // General.msjUser(Process.GetProcessesByName(NombreProceso).Length.ToString()); 

            //if (pro.Length >= 1)
            //{
            //    bRegresa = true; 
            //}

            return bRegresa;
        }

        public static bool ProcesoEnEjecucion(string NombreProceso)
        {
            // NombreProceso = NombreProceso.Replace(".EXE", "").Replace(".exe", "").Replace(".DLL", "").Replace(".dll", "");
            NombreProceso = FormatearNombreProceso(NombreProceso);
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length > 1;

            //NombreProceso = NombreProceso.ToLower().Replace(".exe", "").Replace(".dll", ""); 
            //bRegresa = false; 

            //foreach (Process pro in Process.GetProcesses())
            //{
            //    sProceso = pro.ProcessName.ToLower().Replace(".exe", "").Replace(".dll", "");
            //    if (sProceso == NombreProceso)
            //    {
            //        iModulos++;
            //        if (iModulos > 1)
            //        {
            //            bRegresa = true;
            //            break;
            //        }
            //    }
            //}

            //Process []prProcesos = Process.GetProcesses();  
            return bRegresa;
        }

        public static bool CargarDll(string ObjetoInicio, ref object ObjetoCargado)
        {
            bool bRegresa = false;

            foreach (Form myForma in Application.OpenForms)
            {
                if (myForma.Name.ToUpper() == ObjetoInicio.ToUpper())
                {
                    bRegresa = true;
                    ObjetoCargado = myForma;
                    break;
                }
            }

            return bRegresa;
        }

        public static bool CargarDll(string RutaDll, string NombreDll, string ObjetoInicio, ref object ObjetoCargado)
        {
            bool bRegresa = false;
            object obj = null;
            string sRuta = RutaDll + @"\" + NombreDll;

            try
            {
                if (File.Exists(sRuta))
                {
                    Assembly a = Assembly.LoadFile(sRuta);
                    myTypes = a.GetTypes();

                    foreach (Type myObjeto in myTypes)
                    {
                        if (myObjeto.Name.ToUpper() == ObjetoInicio.ToUpper())
                        {
                            obj = Activator.CreateInstance(myObjeto);
                            ObjetoCargado = obj;
                            bRegresa = true;
                            break;
                        }
                    }

                    if (obj == null)
                        MessageBox.Show("No se encontro la pantalla solicitada");
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el dll.");
                }
            }
            catch
            {
            }

            return bRegresa;
        }
        #endregion Cargar Dll

        #region Fechas
        public static string FechaSistemaFecha
        {
            get
            {
                if (!bFechaActualizada)
                    FechaSistemaObtener();

                return FechaYMD(dtFechaSistema);
            }
        }

        public static string FechaSistemaHora
        {
            get
            {
                if (!bFechaActualizada)
                    FechaSistemaObtener();

                return Hora(dtFechaSistema);
            }
        }

        public static DateTime FechaSistema
        {
            get 
            {
                if (!bFechaActualizada)
                    FechaSistemaObtener();

                return dtFechaSistema; 
            }
            set { dtFechaSistema = value; }
        }

        public static DateTime FechaSistemaObtener()
        {
            return FechaSistemaObtener(DatosCnn);
        }

        public static DateTime FechaSistemaObtener(clsDatosConexion CnnDatos)
        {
            clsConexionSQL myCnn = new clsConexionSQL(CnnDatos);
            clsLeer myLeer = new clsLeer(ref myCnn);

            myCnn.SetConnectionString();

            dtFechaSistema = DateTime.Now;
            if (myCnn.DatosConexion.Servidor != "")
            {
                if (myLeer.Exec("Select getdate() as FechaServidor"))
                {
                    if (myLeer.Leer())
                        dtFechaSistema = myLeer.CampoFecha("FechaServidor");
                }
            }
            bFechaActualizada = true;
            return dtFechaSistema;
        }

        public static string Hora(DateTime Fecha)
        {
            string sRegresa = "", sHora = "", sMinuto = "", sSegundo = "";

            sRegresa = Fecha.TimeOfDay.ToString();

            sHora = Strings.Left(sRegresa, 2);
            sMinuto = Strings.Mid(sRegresa, 4, 2);
            sSegundo = Strings.Mid(sRegresa, 7, 2);

            return sHora + ":" + sMinuto + ":" + sSegundo;
        }

        public static string FechaYMD(DateTime Fecha)
        {
            string sRegresa = FormatearFecha(Fecha, "-", FormatoFecha.YMD);
            return sRegresa;
        }

        public static string FechaYMD(DateTime Fecha, string Separador)
        {
            string sRegresa = FormatearFecha(Fecha, Separador, FormatoFecha.YMD);
            return sRegresa;
        }

        public static string FechaDMY(DateTime Fecha)
        {
            string sRegresa = FormatearFecha(Fecha, "-", FormatoFecha.DMY);
            return sRegresa;
        }

        private static string FormatearFecha(DateTime Fecha, string Caracter, FormatoFecha Formato)
        {
            string sRegresa = "", sYear = "", sMes = "", sDia = "";

            sRegresa = Fecha.ToShortDateString().ToString();

            sYear = Strings.Right(sRegresa, 4);
            sMes = Strings.Mid(sRegresa, 4, 2);
            sDia = Strings.Left(sRegresa, 2);

            switch (Formato)
            {
                case FormatoFecha.YMD:
                    sRegresa = sYear + Caracter + sMes + Caracter + sDia;
                    break;

                case FormatoFecha.DMY:
                    sRegresa = sDia + Caracter + sMes + Caracter + sYear;
                    break;
            }

            return sRegresa;
        }
        #endregion Fechas

        #region Mensajes
        /// <summary>
        /// Muestra el mensaje especificado al usuario
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        public static void msjUser(string Mensaje)
        {
            msjUser(Mensaje, "Mensaje");
        }

        /// <summary>
        /// Muestra el mensaje especificado al usuario
        /// </summary>
        /// <param name="sEncabezado">Encabezado del mensaje</param>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        public static void msjUser(string Mensaje, string Encabezado)
        {
            MessageBox.Show(Mensaje, Encabezado, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra el mensaje especificado al usuario
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        public static void msjError(string Mensaje)
        {
            msjError(Mensaje, "Mensaje");
        }

        /// <summary>
        /// Muestra el mensaje especificado al usuario
        /// </summary>
        /// <param name="sEncabezado">Encabezado del mensaje</param>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        public static void msjError(string Mensaje, string Encabezado)
        {
            MessageBox.Show(Mensaje, Encabezado, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void msjAviso(string Mensaje)
        {
            msjAviso(Mensaje, "Aviso");
        }

        public static void msjAviso(string Mensaje, string Encabezado)
        {
            MessageBox.Show(Mensaje, Encabezado,  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult msjCancelar(string Mensaje)
        {
            return msjCancelar(Mensaje, "Cancelar información");
        }

        public static DialogResult msjCancelar(string Mensaje, string Encabezado)
        {
            return MessageBox.Show(Mensaje, Encabezado, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult msjConfirmar(string Mensaje)
        {
            return msjConfirmar(Mensaje, "Confirmar");
        }

        public static DialogResult msjConfirmar(string Mensaje, string Encabezado)
        {
            return MessageBox.Show(Mensaje, Encabezado, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        #endregion Mensajes 

    }
}
