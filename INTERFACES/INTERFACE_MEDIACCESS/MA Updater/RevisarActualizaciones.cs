using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.EnterpriseServices.Internal; 


using MA_Updater;
using MA_Updater.Data;
using MA_Updater.FuncionesGenerales;

using SC_CompressLib.Utils; 

using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace MA_Updater
{
    public class Constants
    {
        public static string grdColAssmeblyName = "AssmeblyName";
        public static string grdColBackUpDir = "BackUpDir";
        public static string grdColCulture = "Culture";
        public static string grdColDirectory = "Directory";
        public static string grdColLastModified = "LastModified";
        public static string grdColProcessArchitecture = "ProcessArchitecture";
        public static string grdColPublicKey = "PublicKey";
        public static string grdColRemove = "Remove";
        public static string grdColVersion = "Version";
        public static int grdIndxAssmeblyName = 1;
        public static int grdIndxBackUpDir = 8;
        public static int grdIndxCulture = 3;
        public static int grdIndxDirectory = 7;
        public static int grdIndxLastModified = 6;
        public static int grdIndxProcessArchitecture = 4;
        public static int grdIndxPublicKey = 2;
        public static int grdIndxRemove = 0;
        public static int grdIndxVersion = 5;
    }

    public class RevisarActualizaciones
    {
        #region Fuentes
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("gdi32.dll", EntryPoint = "RemoveFontResourceW", SetLastError = true)]
        public static extern int RemoveFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        ////[DllImport("kernel32.dll", SetLastError = true)]
        ////static extern int DeleteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, // handle to destination window
            uint Msg, // message
            int wParam, // first message parameter
            int lParam // second message parameter
        );

        const int WM_FONTCHANGE = 0x001D;
        const int HWND_BROADCAST = 0xffff;
        #endregion Fuentes

        private enum TipoArchivo
        {
            Exe = 1, Dll = 2, Reporte = 3, Excel = 4, Fuente_TTF = 5, Fuente_OTF = 6, Otro = 7, Xslt = 8 
        }

        StreamWriter Log;
        int iLineasLog = 0;

        bool bOcurrioErrorAlActualizar = false;

        string sModulo = "";
        string sRutaDescarga = "";
        string sFullName = "";
        string sUrlLocal = "";
        // string sUrlRegional = "";
        string sUrlCentral = ""; 
        // string sRutaTemp = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\SII_Descargas\\";
        string sRutaTemp = Application.StartupPath + @"\SII_Descargas\";
        
        string sRutaDlls = General.UnidadSO + @":\Windows\assembly\GAC_MSIL".ToLower();
        string sModuloProcesando = "";
        string infoVersion = "0.0.0.0";
        string sRutaSetup = "";
        string sFileSetup = "";
        string sFileSetup_Ext = ""; 

        string sNombreLog = Application.StartupPath + @"\LogUpdate_"; 
        string sModuloDescarga = "";
        string sVersion = "";

        private string sModulo_Farmacia = "";
        private string sVersion_Farmacia = "";
        private string sModuloFarmacia = Application.StartupPath + @"\MA Farmacia.exe";

        bool bEsServidorLocal = false;
        bool bExisteVersionSII = false;
        bool bReinicarEquipo = false; 
        string sRutaWWW = "";
        string sXmlUrlLocal = ""; 

        string sNombreVersionSII = "MA Farmacia.SII";
        string sVersionSII = "0.0.0.0";

        string sNombreVersionSII_Ext = "MA FarmaciaExt.SII";
        string sVersionSII_Ext = "0.0.0.0";

        string sRutaReportes = "";
        string sRutaPlantillas = "";
        string sFileWebService = "wsDllWebService.dll";
        string sFileWebService_RH = "wsRhWebService.dll";

        string sRutaReportes_CFDI = "";
        string sRutaEstilos_CFDI = "";

        ArrayList pListaArchivos_Sistema = new ArrayList(); 

        basGenerales Fg = new basGenerales(); 
        LeerVersion leerVersion;
        clsLeer leer = new clsLeer();

        DataSet dts = new DataSet();
        DataTable Assemblies = new DataTable();

        DirectoryInfo assemblyDirectory = new DirectoryInfo(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Parent.FullName + @"\assembly\GAC_MSIL");

        Label lblAvances = null; 
        bool bModoDesarrollo = false; 

        #region Constructores y Destructor de Clase 
        public RevisarActualizaciones()
        {
        }

        public RevisarActualizaciones(string Ruta, string Modulo, string ModuloDescarga, string Version):this(Ruta, Modulo, ModuloDescarga, Version, null)  
        {
        }

        public RevisarActualizaciones(string Ruta, string Modulo, string ModuloDescarga, string Version, Label ControlAvance)
        {
            sModulo = Modulo;
            sRutaDescarga = Ruta + @"\";
            sRutaReportes = Ruta + @"Reportes\";
            sRutaPlantillas = Ruta + @"Reportes\Plantillas\";
            sFullName = sRutaDescarga + sModulo;


            sRutaReportes_CFDI = Ruta + @"\CFDI\Reportes\";
            sRutaEstilos_CFDI = Ruta + @"\CFDI\Estilos\"; 

            // Datos para Instalacion Manual 
            sRutaSetup = Ruta + @"\SetupSII\";
            sFileSetup = sRutaSetup + @"\MA Farmacia.SII";
            
            sFileSetup = "MA Farmacia.SII";
            sFileSetup_Ext = "MA FarmaciaExt.SII";

            sModuloDescarga = ModuloDescarga; 
            sVersion = Version;

            sModulo_Farmacia = ModuloDescarga; 
            sModuloFarmacia = Application.StartupPath + @"\" + ModuloDescarga;
            sVersion_Farmacia = Version;
            lblAvances = ControlAvance; 


            if (!Directory.Exists(sRutaReportes))
            {
                Directory.CreateDirectory(sRutaReportes);
            }

            if (!Directory.Exists(sRutaPlantillas))
            {
                Directory.CreateDirectory(sRutaPlantillas);
            }


            if (!Directory.Exists(sRutaReportes_CFDI))
            {
                Directory.CreateDirectory(sRutaReportes_CFDI);
            }

            if (!Directory.Exists(sRutaEstilos_CFDI))
            {
                Directory.CreateDirectory(sRutaEstilos_CFDI);
            }
        } 
        #endregion Constructor y Destructor de Clase 

        #region Funciones y Procedimientos Publicos 
        private string MarcaTiempo()
        {
            string sMarca = "";
            DateTime dt = DateTime.Now;

            sMarca += Fg.PonCeros(dt.Year, 4); 
            sMarca += Fg.PonCeros(dt.Month, 2);
            sMarca += Fg.PonCeros(dt.Day, 2);
            sMarca += "_";
            sMarca += Fg.PonCeros(dt.Hour, 2);
            sMarca += Fg.PonCeros(dt.Minute, 2);
            sMarca += Fg.PonCeros(dt.Second, 2);

            return sMarca; 
        }

        public void RegistrarCambios()
        {
            try
            {
                Log.Close();
                System.Threading.Thread.Sleep(50);

                Log = new StreamWriter(sNombreLog, true);
                iLineasLog = 0;
            }
            catch { }
        }

        public void Registro()
        {
            Registro(""); 
        }

        public void Registro(string Mensaje)
        {
            if (lblAvances != null)
            {
                lblAvances.Text = Mensaje; 
            }

            if (iLineasLog >= 3)
            {
                try
                {
                    Log.Close();
                    System.Threading.Thread.Sleep(50);

                    Log = new StreamWriter(sNombreLog, true);
                    iLineasLog = 0;
                }
                catch
                {
                    //General.msjError("Erorroor");
                }
            }

            try
            {
                Log.WriteLine(MarcaTiempo() + " ===> " + Mensaje);
            }
            catch { }
            iLineasLog++;
        }

        public void CheckVersion()
        {
            sNombreLog += General.EsAlmacen ? "Almacen___" : "Farmacia___";
            sNombreLog += MarcaTiempo() + ".log";
            Log = new StreamWriter(sNombreLog);

            Registro(Application.ProductName + "_________" + Application.ProductVersion);
            Registro(); 

            bReinicarEquipo = false;
            if (General.ActualizacionManual)
            {
                InstalacionManual(); 
            }
            else
            {
                InstalacionAutomatica(); 
            }

        }
        #endregion Funciones y Procedimientos Publicos

        #region Tipos de Instalacion 
        #region Instalacion Manual 
        private bool InstalacionManual()
        {
            bool bRegresa = false; 

            if (BuscarPaqueteDeActualizacion())
            {
                BuscarServidorLocal();
                GetVersionSII(); // Jesus Diaz 2K111116.1418  

                if (!bOcurrioErrorAlActualizar)
                {
                    sRutaTemp = sRutaSetup; 
                    DescomprimirManual(); 
                }

                Log.Close();
                LimpiarDirectorioTmp();

                if (!bOcurrioErrorAlActualizar)
                {
                    if (!GetWWW_Directorio(false) && bReinicarEquipo)
                    {
                        clsShutDown shut = new clsShutDown();

                        General.msjAviso("Es necesario reiniciar el Equipo, favor de guardar todos los documentos que esten abiertos." +
                            "\n\n" +
                            " Presione Aceptar para continuar. ");

                        shut.Apagar_y_Reiniciar();
                    }
                }
            }

            return bRegresa;
        }

        private bool BuscarPaqueteDeActualizacion()
        {
            bool bRegresa = true;
            string sF = Path.Combine(sRutaSetup, sFileSetup);

            Registro("Buscando directorio de instalación ... " + sRutaSetup);
            if (!Directory.Exists(sRutaSetup))
            {
                bRegresa = false;
                Registro("No se encontro el directorio de instalación ... " + sRutaSetup);
            }
            else
            {
                Registro("Buscando archivos de instalación ... " + sFileSetup);
                if (!File.Exists(sF))
                {
                    Registro("No se encontro el archivo de instalación ... " + sFileSetup);
                    
                    sF = Path.Combine(sRutaSetup, sFileSetup_Ext);
                    Registro("Buscando archivos de instalación ... " + sFileSetup_Ext);
                    if (!File.Exists(sF))
                    {
                        bRegresa = false;
                        Registro("No se encontro el archivo de instalación ... " + sFileSetup_Ext);
                    }
                }
            }
            Registro();

            if (bRegresa)
            {
                PrepararDirectorio_Descargas(); 
            } 

            return bRegresa;
        }

        private void PrepararDirectorio_Descargas()
        {
            string sF = Path.Combine(sRutaSetup, sFileSetup); 
            string sRutaSII = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\SII_Descargas\\";
            // sRutaSII = Path.Combine(sRutaSII, sFileSetup);
            sF = Path.Combine(sRutaSetup, sFileSetup);
            Registro(sRutaSII);

            if (!Directory.Exists(sRutaSII))
            {
                try
                {
                    Directory.Delete(sRutaSII, true);
                }
                catch { }
                Directory.CreateDirectory(sRutaSII);
            }

            if (File.Exists(Path.Combine(sRutaSII, sFileSetup)))
            {
                Registro("Borrando MA Farmacia.SII anterior.");
                //File.Delete(Path.Combine(sRutaSII, sFileSetup));
                FileDelete(Path.Combine(sRutaSII, sFileSetup));
            }

            try
            {
                File.Copy(sF, Path.Combine(sRutaSII, sFileSetup), true);
            }
            catch { } 

            sRutaTemp = sRutaSII;
            sRutaSetup = sRutaSII; 
        }

        private void DescomprimirManual()
        {
            bool bRegresa = true;
            bool bDescompresion = false;
            // byte[] Buffer;
            string sArchivoOrigen = "";
            string sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen);

            if (!Directory.Exists(sRutaTemp))
            {
                Directory.CreateDirectory(sRutaTemp);
            }

            try
            {
                Registro("Descargando archivo de datos........");
                LimpiarDirectorioTmp();

                sArchivoOrigen = sFileSetup;
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen + "_" + MarcaTiempo());
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen);

                //Buffer = System.Convert.FromBase64String(leerVersion.Campo("EmpacadoModulo"));
                //Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaSII, Buffer, false);
                Registro("Descarga de archivo de datos finalizada........");

                Registro("Descomprimiendo archivo de datos....");
                // Desmpacar la Version y Enviarla al Solicitante 

                ZipInterface zip = new ZipInterface();
                bDescompresion = zip.Descomprimir(sRutaTemp, sRutaSII, "Updater"); 

                if (!bDescompresion)
                {
                    Registro("Descompresion de archivo de datos terminada con errores ....");
                    Registro(zip.Error.Message);
                }
                else 
                {
                    Registro("Descompresion de archivo de datos terminada....");
                }
                // zip = null; 

                try
                {
                    //File.Delete(sRutaSII);
                    FileDelete(sRutaSII);
                }
                catch { }

                Registro();
                Registro("Descargando archivos...............");

                foreach (string sFile in Directory.GetFiles(sRutaTemp))
                {
                    FileInfo f = new FileInfo(sFile);
                    Registro("..... " + f.Name);
                }

                Registro("Descarga de archivos finalizada.");
                Registro();
            }
            catch (Exception ex1)
            {
                bOcurrioErrorAlActualizar = true;
                bRegresa = false;
                Registro("Errror descargando archivo de datos........");
                Registro(ex1.Message);
            }

            if (bRegresa)
            {
                if (bEsServidorLocal)
                {
                    CopiarArchivosServidor();
                }
                else
                {
                    CopiarArchivos();
                }

                // Limpiar el Directorio despues de Actualizar 
                // LimpiarDirectorioTmp();
            }

        }
        #endregion Instalacion Manual

        private bool InstalacionAutomatica()
        {
            bool bRegresa = false; 
            PrepararDirectorio_Descargas(); 

            if (File.Exists(sFullName)) 
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(sFullName);
                infoVersion = info.FileVersion;
                infoVersion = info.ProductVersion;
            }

            string sSql = string.Format("Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ",
                sModuloDescarga, sVersion, General.ForzarUpdate);
            if (ObtenerUrl())
            {
                if (!bOcurrioErrorAlActualizar)
                {
                    BuscarServidorLocal();
                    if (!bOcurrioErrorAlActualizar)
                    {
                        ObtenerUrl_Actualizaciones(); 
                        if (!BuscarUpdateSII())
                        {
                            BuscarMA_Updater();
                        }
                    }
                }
            }

            Log.Close();
            LimpiarDirectorioTmp();

            if (!bOcurrioErrorAlActualizar)
            {
                if (!GetWWW_Directorio(false) && bReinicarEquipo)
                {
                    clsShutDown shut = new clsShutDown();

                    General.msjAviso("Es necesario reiniciar el Equipo, favor de guardar todos los documentos que esten abiertos." +
                        "\n\n" +
                        " Presione Aceptar para continuar. ");

                    shut.Apagar_y_Reiniciar();
                }
            }

            return bRegresa;
        }
        #endregion Tipos de Instalacion 

        #region Funciones y Procedimientos Privados 
        private void Salir()
        {
            try
            {
                Log.Close(); 
            }
            catch { }

            // Application.Exit(); 

            // General.TerminarProceso("Updater Farmacia.exe"); 

        }

        private bool BuscarUpdateSII()
        {
            Registro(); 
            Registro("Buscando paquete de Actualización.");

            int iSolicitudes = 0; 
            bool bRegresa = false;
            string sSql = "";             
            GetVersionSII();
            bExisteVersionSII = false;            

            sSql = string.Format("Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sNombreVersionSII, sVersionSII, 0);
            leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));

            while (iSolicitudes < 3)
            {
                //System.Threading.Thread.Sleep(1000); 
                Registro("Busqueda SII .... " + iSolicitudes.ToString());
                Registro(sSql); 


                if (!leerVersion.GetVersion(sSql))
                {
                    bOcurrioErrorAlActualizar = true; 
                    Registro("Error al Revisar la Version en el Servidor.");
                    Registro(leerVersion.MensajeError);
                    Registro();
                    Salir();
                    break; 
                }
                else
                {
                    if (leerVersion.Leer())
                    {
                        Registro("Version SII encontrada....");
                        bOcurrioErrorAlActualizar = false;
                        bExisteVersionSII = true;
                        Descomprimir();
                        bRegresa = true; 
                        //// DescargarVersion(leerVersion, bExisteVersionSII);
                        break;
                    }
                }
                iSolicitudes++; 
            }

            // solo revisar en caso de no encontrar ninguna actualizacion 
            if ( !bRegresa ) 
            {
                bRegresa = BuscarUpdateSII_Ext(); 
            }

            Registro("Busqueda de Actualización terminada.");
            Registro(); 
            return bExisteVersionSII; 
        }

        private bool BuscarUpdateSII_Ext()
        {
            Registro();
            Registro("Buscando paquete de Actualización......");

            int iSolicitudes = 0;
            // bool bRegresa = false;
            string sSql = "";

            ObtenerUrl(); 
            GetVersionSII_Ext();
            ObtenerUrl_Actualizaciones();

            bExisteVersionSII = false;

            sSql = string.Format("Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sNombreVersionSII_Ext, sVersionSII_Ext, 0);
            leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));

            while (iSolicitudes < 3)
            {
                //System.Threading.Thread.Sleep(1000); 
                Registro("Busqueda SII ........ " + iSolicitudes.ToString());
                Registro(sSql);


                if (!leerVersion.GetVersion(sSql))
                {
                    bOcurrioErrorAlActualizar = true;
                    Registro("Error al Revisar la Version en el Servidor.");
                    Registro(leerVersion.MensajeError);
                    Registro();
                    Salir();
                    break;
                }
                else
                {
                    if (leerVersion.Leer())
                    {
                        Registro("Version SII encontrada.......");
                        bOcurrioErrorAlActualizar = false;
                        bExisteVersionSII = true;
                        Descomprimir();
                        //// DescargarVersion(leerVersion, bExisteVersionSII);
                        break;
                    }
                }
                iSolicitudes++;
            }

            Registro("Busqueda de Actualización terminada......");
            Registro();
            return bExisteVersionSII;
        }

        private void Descomprimir()
        {
            bool bRegresa = true; 
            byte[] Buffer;
            string sArchivoOrigen = "";
            string sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen);

            if (!Directory.Exists(sRutaTemp))
            {
                Directory.CreateDirectory(sRutaTemp);
            }

            try
            {
                Registro("Descargando archivo de datos........"); 
                LimpiarDirectorioTmp(); 


                leerVersion.RegistroActual = 1; 
                sArchivoOrigen = leerVersion.Campo("Nombre");
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen + "_" + MarcaTiempo());
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen); 

                Buffer = System.Convert.FromBase64String(leerVersion.Campo("EmpacadoModulo"));
                Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaSII, Buffer, false);
                Registro("Descarga de archivo de datos finalizada........");

                Registro("Descomprimiendo archivo de datos....");
                // Desmpacar la Version y Enviarla al Solicitante 
                ZipInterface zip = new ZipInterface();
                zip.Descomprimir(sRutaTemp, sRutaSII, "Updater");
                zip = null;
                Registro("Descompresion de archivo de datos terminada....");

                try
                {
                    //File.Delete(sRutaSII);
                    FileDelete(sRutaSII);
                }
                catch { }

                Registro();
                Registro("Descargando archivos...............");

                foreach (string sFile in Directory.GetFiles(sRutaTemp))
                {
                    FileInfo f = new FileInfo(sFile); 
                    Registro("..... " + f.Name);
                }

                Registro("Descarga de archivos finalizada.");
                Registro(); 
            }
            catch (Exception ex1)
            {
                bOcurrioErrorAlActualizar = true;
                bRegresa = false;
                Registro("Errror descargando archivo de datos........");
                Registro(ex1.Message); 
            }

            if (bRegresa)
            {
                if (bEsServidorLocal)
                {
                    if (bExisteVersionSII)
                    {
                        CopiarArchivosServidor();
                    }
                    else
                    {
                        CopiarArchivos();
                    }
                }
                else
                {
                    CopiarArchivos();
                }

                // Limpiar el Directorio despues de Actualizar 
                // LimpiarDirectorioTmp();
            }

        }

        private bool BuscarMA_Updater()
        {
            int iSolicitudes = 0;
            // bool bRegresa = false;
            string sSql = "";           


            if (File.Exists(sModuloFarmacia))
            {
                FileVersionInfo f = FileVersionInfo.GetVersionInfo(sModuloFarmacia);
                FileInfo fx = new FileInfo(sModuloFarmacia);

                sModulo_Farmacia = fx.Name;
                sVersion_Farmacia = f.FileVersion;
            }
            ////else
            ////{
            ////    sVersion_Farmacia = "0.0.0.0";
            ////    sModulo_Farmacia = General.EsAlmacen ? "Almacen.exe" : "Farmacia.exe";
            ////}


            Registro("Buscando actualizacion de Farmacia.");
            Registro(sModulo_Farmacia + "__________" + sVersion_Farmacia);  

            sSql = string.Format("Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", 
                sModulo_Farmacia, sVersion_Farmacia, 1);
            leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));

            ////while (iSolicitudes < 3 && !bOcurrioErrorAlActualizar)
            while (iSolicitudes < 3)
            {
                Registro("Busqueda .... " + iSolicitudes.ToString());
                Registro(sSql);
                //// 
                RegistrarCambios(); 

                if (!leerVersion.GetVersion(sSql))
                {
                    Registro("Error al Revisar la Version de Farmacia en el Servidor.");
                    Registro(leerVersion.MensajeError);
                    Registro();
                    Salir();
                    iSolicitudes = 4;
                    break; 
                }
                else
                {
                    if (leerVersion.Leer())
                    {
                        Registro("Version Farmacia encontrada...."); 
                        DescargarVersion(leerVersion);
                        iSolicitudes = 4;
                        break; 
                    }
                }
                iSolicitudes++;
            }
            Registro("Busqueda actualizacion de Farmacia terminada.");

            return bExisteVersionSII;
        }

        private bool BuscarServidorLocal() 
        {
            bool bRegresa = false;
            bool bEsValido = true;
            string sMensaje = ""; 
            bEsServidorLocal = false; 

            if ( ObtenerUrlLocal() )
            {
                Registro("Obteniendo MAC de Servidor Local."); 
                string sSql = string.Format(" Select IdTerminal, Nombre, MAC_Address, EsServidor " + 
                    " From CFGC_Terminales (NoLock) " +
                    " Where replace(MAC_Address, '-', '') = '{0}' and EsServidor = 1 ", General.MacAddress);

                ////sSql = string.Format(" Select IdTerminal, Nombre, MAC_Address, EsServidor " +
                ////                    " From CFGC_Terminales (NoLock) " +
                ////                    " Where replace(MAC_Address, '-', '') = '{0}' ", General.MacAddress);

                try
                {
                    wsFarmacia.wsCnnCliente cnnFarmacia_x = new MA_Updater.wsFarmacia.wsCnnCliente();
                    cnnFarmacia_x.Url = sUrlLocal;

                    clsConexionSQL cnn = new clsConexionSQL(new clsDatosConexion(cnnFarmacia_x.ConexionEx(General.CfgIniPuntoDeVenta)));
                    clsLeer leerSvr = new clsLeer(ref cnn);

                    if (General.ActualizacionManual)
                    { 
                        sUrlCentral = sUrlLocal; 
                    }

                    leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));
                    // if (!leerVersion.ExecAux(sSql))

                    Registro(""); 
                    Registro("Servidor :  " + leerSvr.DatosConexion.Servidor);  
                    Registro("Base de datos :  " + leerSvr.DatosConexion.BaseDeDatos);
                    Registro(""); 

                    if (!leerSvr.Exec(sSql))
                    {
                        bOcurrioErrorAlActualizar = true; 
                        Registro("Error al Obtener la MAC del Servidor local.");
                        Registro(leerSvr.MensajeError); 
                        Registro(sSql);
                        Registro();

                        General.msjError("Ocurrió un error al Obtener la MAC del Servidor.");
                        Salir();
                    }
                    else
                    {
                        if (!leerSvr.Leer())
                        {
                            sMensaje = "La MAC [" + General.MacAddress + "] no pertenece al Servidor Local."; 
                            bEsValido = false; 
                        }

                        if ( General.EsEquipoDeDesarrollo ) 
                        {
                            bEsValido = true;
                            sMensaje = "Equipo de desarrollo : "; 
                        }

                        if (!bEsValido)
                        {
                            Registro(sMensaje); 
                        } 
                        else
                        {
                            Registro(sMensaje + "MAC de servidor [" + General.MacAddress + "]");
                            bEsServidorLocal = true;
                            GetWWW_Directorio();

                            try
                            {
                                Registro("Deteniendo Servicio Cliente.");
                                General.TerminarProceso("Servicio Cliente.exe");
                            }
                            catch { } 
                        }
                    }
                }
                catch (Exception ex1) 
                {
                    Registro("Error al Solicitar la MAC del Servidor.");
                    Registro(ex1.Message); 
                    bOcurrioErrorAlActualizar = true; 
                }
                Registro("Obteniendo MAC de Servidor Local finalizada."); 
            }

            bRegresa = bEsValido; 
            return bRegresa; 
        }

        //////private bool AgregarCampo()
        //////{
        //////    bool bRegresa = false; 
        //////    string sSql = "If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) " +
        //////            " Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) " +
        //////            " Where So.Name = 'Net_Versiones' and Sc.Name = 'Tipo' ) " +
        //////            "    Begin " +
        //////            "       Alter Table Net_Versiones Add Tipo Int Not Null Default 1 " +
        //////            "    End " + "\n\n";

        //////    sSql += "If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) " +
        //////            " Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) " +
        //////            " Where So.Name = 'Net_Versiones' and Sc.Name = 'Modulo' ) " +
        //////            "    Begin " +
        //////            "       Alter Table Net_Versiones Add Modulo varchar(100) Not Null Default '' " +
        //////            "    End " + "\n\n";

        //////    bRegresa = leerVersion.ExecAux(sSql); 

        //////    return bRegresa; 
        //////}

        private bool GetVersionSII()
        {
            Registro(); 
            Registro("Buscando Ultima Versión Registrada."); 

            bool bRegresa = false;
            sNombreVersionSII = "MA Farmacia.SII";
            sVersionSII = "0.0.0.0";

            //////AgregarCampo(); 

            string sSql = "If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones' and xType = 'U' ) " +
                   " Begin  " +
                   " CREATE TABLE dbo.Net_Versiones(  " +
                   "     [IdVersion] int identity(1,1),  " +
                   "     [Modulo] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [NombreVersion] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Version] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Tipo] int Not Null Default 1,  " +
                   "     [FechaRegistro] [datetime] NOT NULL DEFAULT (getdate()),  " +
                   "     [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name())  " +
                   " ) ON [PRIMARY] " + 
                   " End " + "\n\n"; 

            sSql += " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " + 
                " From Net_Versiones (NoLock) " + 
                " Where Tipo = 1 " + 
                " Order By IdVersion desc "; 

            if (!leerVersion.ExecAux(sSql))
            {
                Registro("Error al Revisar la Version."); 
                Registro(leerVersion.MensajeError);
                Application.Exit(); 
            }
            else
            {
                if (!leerVersion.Leer())
                {
                    Registro(sNombreVersionSII + "__________" + sVersionSII);
                }
                else
                {
                    bRegresa = true;
                    // sNombreVersionSII = leerVersion.Campo("NombreVersion");
                    sVersionSII = leerVersion.Campo("Version"); 
                    Registro(leerVersion.Campo("NombreVersion") + "__________" + sVersionSII);
                }
            }

            Registro("Busqueda de Versión terminada.");
            Registro(); 
            return bRegresa; 
        }

        private bool GetVersionSII_Ext()
        {
            bool bRegresa = false;
            sNombreVersionSII_Ext = "MA FarmaciaExt.SII";
            sVersionSII_Ext = "0.0.0.0";

            string sSql = " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones (NoLock) " +
                " Where Tipo = 2 " +
                " Order By IdVersion desc ";

            leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));
            if (!leerVersion.ExecAux(sSql))
            {
                Registro("Error al Revisar la Version.");
                Registro(leerVersion.MensajeError);
                Application.Exit();
            }
            else
            {
                if (!leerVersion.Leer())
                {
                    Registro(sNombreVersionSII_Ext + "__________" + sVersionSII_Ext);
                }
                else
                {
                    bRegresa = true;
                    // sNombreVersionSII = leerVersion.Campo("NombreVersion");
                    sVersionSII_Ext = leerVersion.Campo("Version");
                    Registro(leerVersion.Campo("NombreVersion") + "__________" + sVersionSII_Ext);
                }
            }

            return bRegresa;
        }

        private bool GetWWW_Directorio()
        {
            return GetWWW_Directorio(true); 
        }

        private bool GetWWW_Directorio(bool RegistrarLog)
        {
            bool bRegresa = false;

            if (RegistrarLog)
            {
                Registro("Solicitando Directorios de Trabajo WWW.");
            }

            try
            {
                clsLeer leerDir = new clsLeer();
                clsCriptografo crypto = new clsCriptografo();

                wsFarmacia.wsCnnCliente cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente();
                cnnFarmacia.Url = sUrlLocal;

                leerDir.DataSetClase = cnnFarmacia.wwwDirectorio();

                if (leerDir.Leer())
                {
                    sRutaWWW = crypto.Desencriptar(leerDir.Campo("Ruta"));
                    bRegresa = true; 
                }
            }
            catch 
            {
                Registro("Solicitud de Directorio de Trabajo terminada con errores.");
            }

            if (RegistrarLog)
            {
                Registro("Solicitud de Directorio de Trabajo terminada.");
            }
            
            return bRegresa; 
        }

        private bool ObtenerUrl()
        {
            Registro("Obteniendo Url para Actualizaciones.");

            bool bRegresa = false;
            string sXml = General.UnidadSO + ":\\MA Updater.xml";

            //////if (General.EsAlmacen)
            //////{
            //////    sXml = General.UnidadSO + ":\\AlmacenPtoVta.xml";
            //////}

            try
            {
                dts = new DataSet();
                dts.ReadXml(sXml);
                leer.DataSetClase = dts;

                if (leer.Leer())
                {
                    sUrlCentral = "http://" + leer.Campo("Servidor") + "/";
                    sUrlCentral += leer.Campo("WebService") + "/";
                    sUrlCentral += leer.Campo("PaginaAsmx") + ".asmx";

                    sUrlLocal = sUrlCentral; 

                    // bRegresa = GetUrlRegional(sUrlLocal); 
                    bRegresa = true;
                }
            }
            catch (Exception ex1)
            {
                bOcurrioErrorAlActualizar = true;
                Registro("Error al Obtener la Url para Actualizaciones.");
                Registro(ex1.Message);
                Salir(); 
            }
            return bRegresa; 
        }

        private bool ObtenerFileConfig()
        {
            bool bRegresa = false;
            string sXml = General.UnidadSO + ":\\MA Updater.xml";

            ////if ( General.EsAlmacen  )
            ////{
            ////    sXml = General.UnidadSO + ":\\AlmacenPtoVta.xml";
            ////}

            //// Jesús Díaz 2K120714.1230 
            bRegresa = File.Exists(sXml); 

            ////if (!bRegresa)
            ////{
            ////    sXml = General.UnidadSO + ":\\AlmacenPtoVta.xml";
            ////    bRegresa = File.Exists(sXml); 
            ////}

            sXmlUrlLocal = sXml; 
            return bRegresa;
        }

        private bool ObtenerUrlLocal()
        {
            bool bRegresa = false;

            if (ObtenerFileConfig())
            {
                bRegresa = ObtenerUrlLocal(sXmlUrlLocal); 
            }

            return bRegresa;
        }

        private bool ObtenerUrlLocal(string ArchivoXML) 
        {
            Registro("Obteniendo Url de Servidor Local."); 
            bool bRegresa = false;
            string sXml = ArchivoXML; // General.UnidadSO + ":\\FarmaciaPtoVta.xml"; 

            try
            {

                dts = new DataSet(); 
                dts.ReadXml(sXml);
                leer.DataSetClase = dts;

                if (leer.Leer())
                {
                    sUrlLocal = "http://" + leer.Campo("Servidor") + "/";
                    sUrlLocal += leer.Campo("WebService") + "/";
                    sUrlLocal += leer.Campo("PaginaAsmx") + ".asmx";

                    bRegresa = true;
                }
            }
            catch(Exception ex1)
            {
                bOcurrioErrorAlActualizar = true; 
                Registro("Error al Obtener la Url de Servidor Local.");
                Registro(ex1.Message);
                bOcurrioErrorAlActualizar = true; 
            }
            return bRegresa;
        }

        private bool ObtenerUrl_Actualizaciones()
        {
            Registro("Obteniendo Url de Servidor de Actualizaciones.");
            bool bRegresa = false;
            string sXml = sXmlUrlLocal; // General.UnidadSO + ":\\FarmaciaPtoVta.xml"; 

            try
            {

                dts = new DataSet();
                dts.ReadXml(sXml);
                leer.DataSetClase = dts;

                if (leer.Leer())
                {
                    sUrlLocal = "http://" + leer.Campo("Servidor") + "/";
                    sUrlLocal += leer.Campo("WebService") + "/";
                    sUrlLocal += "wsUpdater.asmx";


                    // Asignar la misma direccion 
                    sUrlCentral = sUrlLocal; 

                    // bRegresa = GetUrlRegional(sUrlLocal); 
                    bRegresa = true;
                }
            }
            catch (Exception ex1)
            {
                bOcurrioErrorAlActualizar = true;
                Registro("Error al Obtener la Url de Servidor de Actualizaciones.");
                Registro(ex1.Message);
                bOcurrioErrorAlActualizar = true;
            }
            return bRegresa;
        }

        private bool GetUrlRegional(string Url)
        {
            General.ArchivoIni = General.CfgIniPuntoDeVenta; 
            bool bRegresa = false;
            //LeerVersion leerRegional = new LeerVersion(Url, new clsDatosCliente(General.DatosApp, "", ""));
            //string sSql = " Select top 1 Url, StatusUrl From vw_OficinaCentralRegional_Url (NoLock) ";

            //if (!leerRegional.Exec(sSql))
            //{
            //}
            //else
            //{
            //    if (leerRegional.Leer())
            //    {
            //        sUrlRegional = leerRegional.Campo("Url");
            //        bRegresa = GetUrlCentral(sUrlRegional); 
            //    }
            //}
            return bRegresa; 
        }

        private bool GetUrlCentral(string Url)
        {
            General.ArchivoIni = General.CfgIniOficinaCentral; 
            bool bRegresa = false;
            //LeerVersion leerRegional = new LeerVersion(Url, new clsDatosCliente(General.DatosApp, "", ""));
            //string sSql = " Select top 1 Url, StatusUrl From vw_OficinaCentral_Url (NoLock) ";

            //if (!leerRegional.Exec(sSql))
            //{
            //}
            //else
            //{
            //    if (leerRegional.Leer())
            //    {
            //        sSql = leerRegional.Campo("Url");

            //        if (sSql != "")
            //        {
            //            sUrlCentral = sSql;
            //            bRegresa = true;
            //        }
            //    }
            //}
            return bRegresa; 
        }

        private void DescargarVersion(clsLeer Descargar)
        {
            DescargarVersion(Descargar, false); 
        }

        private void DescargarVersion(clsLeer Descargar, bool EsFarmaciaSII) 
        {
            bool bRegresa = true;
            string sArchivoOrigen = "";
            byte[] Buffer;


            if (!Directory.Exists(sRutaTemp))
            {
                Directory.CreateDirectory(sRutaTemp); 
            }

            try
            {
                Registro(); 
                Registro("Limpiando directorio temporal de Descargas."); 
                // Vaciar el Directorio Temporal 
                LimpiarDirectorioTmp(); 

                // Bajar la version 
                Descargar.RegistroActual = 1;

                Registro(); 
                Registro("Descargando archivos..............."); 
                while (Descargar.Leer())
                {
                    sArchivoOrigen = Descargar.Campo("Nombre");
                    Buffer = System.Convert.FromBase64String(Descargar.Campo("EmpacadoModulo"));
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaTemp + "\\" + sArchivoOrigen, Buffer, false);

                    Registro("..... " + sArchivoOrigen); 
                    if (EsFarmaciaSII) 
                    { 
                        // Desempacar Archivos 
                    }
                }
                Registro("Descarga de archivos finalizada.");
                Registro(); 
            }
            catch
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                if (bEsServidorLocal)
                {
                    if (bExisteVersionSII)
                    {
                        CopiarArchivosServidor();
                    }
                    else
                    {
                        CopiarArchivos();
                    }
                }
                else
                {
                    CopiarArchivos();
                }

                // Limpiar el Directorio despues de Actualizar 
                // LimpiarDirectorioTmp(); 
            }
        }

        private void FileDelete(string ArchivoBorrar)
        {
            try
            {
                FileInfo f = new FileInfo(ArchivoBorrar);
                f.Attributes = FileAttributes.Normal;
                f = null; 
            }
            catch { }

            try
            {
                File.Delete(ArchivoBorrar);
            }
            catch { } 
        }

        private void LimpiarDirectorioTmp()
        {
            string sFileNoDel = Path.Combine(sRutaSetup, sFileSetup); 
 
            // Vaciar el Directorio Temporal 
            foreach (string s in Directory.GetFiles(sRutaTemp))
            {
                try
                {
                    if (!General.ActualizacionManual)
                    {
                        //File.Delete(s);
                        FileDelete(s); 
                    }
                    else 
                    {
                        if (sFileNoDel.ToLower() != s.ToLower())
                        {
                            //File.Delete(s);
                            FileDelete(s); 
                        }
                    }
                }
                catch { }
            }
        }

        private void CopiarArchivosServidor()
        {
            //PrepararTabla();

            string sFileModulo = sRutaTemp + "\\" + sModulo;

            if (validarVersionNoInstalada())
            {
                if (EjecutarScripts())
                {
                    CopiarArchivos();
                    Copiar_ArchivosWebService();
                }
            }
            else
            {
                if (!bOcurrioErrorAlActualizar)
                {
                    CopiarArchivos();
                    Copiar_ArchivosWebService();
                }
            }
        }

        private bool validarRequisitoDeVersiones(string ArchivoDeControl)
        {
            bool bRegresa = false;
            bool bExito = false;
            string sMsjVersion = "";  
            ArrayList listaVersiones = new ArrayList();
            ArrayList listaVersionesError = new ArrayList();
            string sVersionInstalacion = "";
            string sSql = "";
            // int iCambios = 0; 

            // string sFile = "";
            // int EqualPosition = 0;
            // string myKey = "";


            leerArchivoDeControl(ArchivoDeControl, ref listaVersiones, ref sVersionInstalacion);
            ListaArchivosSistema(); 

            bRegresa = listaVersiones.Count > 0;

            ObtenerUrl(); 
            wsFarmacia.wsCnnCliente cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente();
            cnnFarmacia.Url = sUrlLocal;
            ObtenerUrl_Actualizaciones(); 

            clsConexionSQL cnn = new clsConexionSQL(new clsDatosConexion(cnnFarmacia.ConexionEx(General.CfgIniPuntoDeVenta)));
            leer = new clsLeer(ref cnn);

            foreach (string sVersion in listaVersiones)
            {
                sSql = string.Format(" Select * From Net_Versiones (NoLock) Where Version = '{0}'  ", sVersion );
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
                else
                {
                    if (!leer.Leer())
                    {
                        listaVersionesError.Add(sVersion); 
                    }
                } 
            }

            // Revisar si se encontraron errores 
            bExito = listaVersionesError.Count == 0;
            bRegresa = bExito; 

            if (!bExito)
            {
                sMsjVersion = "No fue posible validar algunas versiones anteriores : \n\n\n"; 

                foreach (string sVersion in listaVersionesError)
                {
                    sMsjVersion += string.Format("      Versión {0} \n", sVersion);
                }

                sMsjVersion += "\n\n";
                sMsjVersion += string.Format("No es posible instalar la Versión actual {0} \n", sVersionInstalacion);

                if (General.EsServidorGeneral)
                {
                    bRegresa = true; 
                }
                else 
                {
                    General.msjAviso(sMsjVersion);
                }
            } 

            /* 
            bOcurrioErrorAlActualizar = true;
            sMsjVersion = string.Format("La versión {0} de base de datos del módulo {1}  ya fue registrada, verifique.\n \n", sCtrlVersion, sCtrlModulo);
            sMsjVersion += "Se actulizarán el resto de los Modulos.";

            General.msjAviso(sMsjVersion);
            */ 

            return bRegresa; 
        }

        private void ListaArchivosSistema()
        {
            //pListaArchivos_Sistema = new ArrayList(); 
        }

        private void leerArchivoDeControl(string ArchivoDeControl, ref ArrayList ListaVersiones, ref string VersionInstalacion)
        {
            ArrayList listaVersiones = new ArrayList();
            string sVersionInstalacion = ""; 

            string sFile = "";
            int EqualPosition = 0;
            string myKey = "";
            string sValor = "";

            pListaArchivos_Sistema = new ArrayList(); 
            using (StreamReader sr = new StreamReader(ArchivoDeControl))
            {
                while (sr.Peek() >= 0)
                {
                    sFile = sr.ReadLine();
                    if (sFile.Length != 0)
                    {
                        EqualPosition = 0;
                        if (sFile.Substring(0, 1) != "#")
                        {
                            EqualPosition = sFile.IndexOf("=", 0);
                        } 

                        if (EqualPosition > 0)
                        {
                            myKey = sFile.Substring(0, EqualPosition).ToUpper();
                            if (myKey == "VersionInstalacion".ToUpper())
                            {
                                sVersionInstalacion = sFile.Substring(EqualPosition + 1).Trim();
                            }
                            else
                            {
                                myKey = sFile.Substring(0, EqualPosition).ToUpper();
                                if (myKey == "Version".ToUpper())
                                {
                                    sValor = sFile.Substring(EqualPosition + 1).Trim();
                                    listaVersiones.Add(sValor);
                                } 

                                myKey = sFile.Substring(0, EqualPosition).ToUpper();
                                if (myKey == "File".ToUpper())
                                {
                                    sValor = sFile.Substring(EqualPosition + 1).Trim();
                                    pListaArchivos_Sistema.Add(sValor);
                                }
                            }
                        }
                    }
                }
            }

            // Salida final  
            listaVersiones.Sort(); 
            ListaVersiones = listaVersiones;
            VersionInstalacion = sVersionInstalacion; 

        }

        private bool validarVersionNoInstalada_Clientes()
        {
            bool bRegresa = true;

            if ( General.ActualizacionManual )
            {
                bRegresa = validarVersionNoInstalada_Clientes_Manual(); 
            }

            return bRegresa; 
        }

        private bool validarVersionNoInstalada_Clientes_Manual()
        {
            bool bRegresa = false;
            string sMsjVersion = "";
            // string sSql = "";
            string sFileCtrl = "";
            string sCtrlModulo = "";
            string sCtrlTipo = "";
            string sCtrlVersion = "";
            string[] sDatos;
            FileInfo fCtrl;

            Registro();
            Registro("Buscando archivo de Control de Versión");

            foreach (string ctrl in Directory.GetFiles(sRutaTemp, "CTRL_VERSION_*.sql"))
            {
                fCtrl = new FileInfo(ctrl);
                sFileCtrl = fCtrl.Name.Replace(fCtrl.Extension, "");
                sFileCtrl = sFileCtrl.Replace("CTRL_VERSION_", "");
                sDatos = sFileCtrl.Split('_');
                sFileCtrl = fCtrl.FullName;

                sCtrlModulo = sDatos[0];
                sCtrlTipo = Convert.ToUInt32(sDatos[1]).ToString();
                sCtrlVersion = sDatos[2];
                bRegresa = true;
                break;
            }

            if (!bRegresa)
            {
                sMsjVersion = "No se encontro el archivo de Control de Versión.";
                Registro(sMsjVersion);

                if (General.EsServidorGeneral)
                {
                    bRegresa = true; 
                }
                else
                {
                    General.msjAviso(sMsjVersion);
                }
            }
            else
            {
                bRegresa = validarRequisitoDeVersiones(sFileCtrl);
                bOcurrioErrorAlActualizar = !bRegresa;
            }

            return bRegresa; 
        }


        private bool validarVersionNoInstalada()
        {
            bool bRegresa = false;
            string sMsjVersion = ""; 
            string sSql = "";
            string sFileCtrl = "";
            string sCtrlModulo = "";
            string sCtrlTipo = "";
            string sCtrlVersion = "";
            string[] sDatos; 
            FileInfo fCtrl; 

            Registro();
            Registro("Buscando archivo de Control de Versión");

            foreach (string ctrl in Directory.GetFiles(sRutaTemp, "CTRL_VERSION_*.sql"))
            {
                fCtrl = new FileInfo(ctrl);
                sFileCtrl = fCtrl.Name.Replace(fCtrl.Extension, ""); 
                sFileCtrl = sFileCtrl.Replace("CTRL_VERSION_", "");
                sDatos = sFileCtrl.Split('_');
                sFileCtrl = fCtrl.FullName; 

                sCtrlModulo = sDatos[0];
                sCtrlTipo = Convert.ToUInt32(sDatos[1]).ToString();
                sCtrlVersion = sDatos[2]; 
                bRegresa = true; 
                break; 
            }

            if (!bRegresa)
            {
                bOcurrioErrorAlActualizar = true; 
                sMsjVersion = "No se encontro el archivo de Control de Versión.";
                Registro(sMsjVersion);
                General.msjAviso(sMsjVersion);
            }
            else 
            {
                if (!validarRequisitoDeVersiones(sFileCtrl))
                {
                    bRegresa = false;
                    bOcurrioErrorAlActualizar = true; 
                }
                else 
                {
                    ObtenerUrl(); 
                    wsFarmacia.wsCnnCliente cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente();
                    cnnFarmacia.Url = sUrlLocal;
                    ObtenerUrl_Actualizaciones(); 

                    clsConexionSQL cnn = new clsConexionSQL(new clsDatosConexion(cnnFarmacia.ConexionEx(General.CfgIniPuntoDeVenta)));
                    leer = new clsLeer(ref cnn);

                    sSql = string.Format(" Select * From Net_Versiones (NoLock) " + 
                        " Where Modulo = '{0}' and Version = '{1}' and Tipo = '{2}' ",
                        sCtrlModulo, sCtrlVersion, sCtrlTipo);
                    if (!leer.Exec(sSql))
                    {
                        bOcurrioErrorAlActualizar = true; 
                        bRegresa = false;
                        Registro("Ocurrió un error al validar la versión a instalar.");
                        Registro(leer.MensajeError);
                    }
                    else
                    {
                        if (leer.Leer()) 
                        {
                            bRegresa = false;
                            sMsjVersion = string.Format("La versión {0} de base de datos del módulo {1}  ya fue registrada, verifique.", sCtrlVersion, sCtrlModulo);
                            sMsjVersion = string.Format("La versión {0} de base de datos {1} del módulo {2}  ya fue registrada, verifique.", sCtrlVersion, leer.DatosConexion.BaseDeDatos, sCtrlModulo);

                            Registro(sMsjVersion);
                            Registro("Se actulizarán el resto de los Modulos.");
                        }
                    }

                    if (bRegresa)
                    {
                        sMsjVersion = string.Format("La versión {0} del módulo {1} no registrada, se instalara la actualización.", sCtrlVersion, sCtrlModulo);
                        Registro(sMsjVersion);
                    }
                    else
                    {
                        sMsjVersion = string.Format("La versión {0} de base de datos del módulo {1}  ya fue registrada, verifique.\n \n", sCtrlVersion, sCtrlModulo);
                        sMsjVersion += "Se actulizarán el resto de los Modulos.";

                        General.msjAviso(sMsjVersion);
                    }
                }
            }

            // bOcurrioErrorAlActualizar = !bRegresa;


            Registro("Busqueda de archivo de Control de Versión terminada.");
            Registro();
            RegistrarCambios(); 

            return bRegresa; 
        }

        private bool EjecutarScripts()
        {
            bool bRegresa = true;
            bool bExito = true;
            string sSql = "";
            StreamReader fScript;
            clsScritpSQL Script; 

            try
            {
                ObtenerUrl(); 
                wsFarmacia.wsCnnCliente cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente();
                cnnFarmacia.Url = sUrlLocal;
                ObtenerUrl_Actualizaciones(); 

                clsDatosConexion datosDeConexion = new clsDatosConexion(cnnFarmacia.ConexionEx(General.CfgIniPuntoDeVenta));
                clsConexionSQL cnn = new clsConexionSQL();
                cnn.DatosConexion.Servidor = datosDeConexion.Servidor;
                cnn.DatosConexion.BaseDeDatos = datosDeConexion.BaseDeDatos;
                cnn.DatosConexion.Usuario = datosDeConexion.Usuario;
                cnn.DatosConexion.Password = datosDeConexion.Password;
                cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
                cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

                clsLeer leerUpdate = new clsLeer(ref cnn);
                cnn.FormatoDeFecha = FormatoDeFecha.Ninguno; 

                if (cnn.Abrir())
                {
                    Registro(); 
                    Registro("Registro de Scripts iniciado."); 
                     
                    cnn.IniciarTransaccion();
                    foreach (string sScript in Directory.GetFiles(sRutaTemp, "*.sql"))
                    {
                        FileInfo xf = new FileInfo(sScript); 

                        Registro("... Ejecutando script ..... " + xf.Name);
                        fScript = new StreamReader(sScript, Encoding.Default);
                        // sSql = "Set DateFormat YMD   " + fScript.ReadToEnd();
                        sSql = fScript.ReadToEnd(); 
                        fScript.Close();

                        //if (General.WithOutEncryption)
                        //{
                        //    sSql = "";
                        //}

                        Script = new clsScritpSQL(sSql, General.WithOutEncryption);
                        foreach (string sFragmento in Script.ListaScripts)
                        {
                            sSql = "" + sFragmento;
                            //Registro("...... Ejecutando segmento de Script ..... " + sSql); 
                            if (!leerUpdate.Exec(sSql))
                            {
                                bExito = false;
                                Registro("Ocurrió un error al ejecutar segmento de Script ..... ");
                                Registro(sScript); 
                                Registro("Error ....................................   " + leerUpdate.MensajeError);
                                Registro(sSql);
                                Registro();
                                break;
                            }
                        }

                        if (!bExito)
                        {
                            break;
                        }
                    }

                    // Asegurar que se hayan integrado todos los Scripts 
                    if (bExito)
                    {
                        cnn.CompletarTransaccion();
                        bRegresa = true;
                        Registro("Registro de Scripts terminado con exito."); 
                    }
                    else
                    {
                        cnn.DeshacerTransaccion();
                        bRegresa = false;
                        Registro("Registro de Scripts termiando con errores.");
                        ////Registro("....................   " +leerUpdate.MensajeError);
                        General.msjError("Ocurrió un error al Actualizar el Sistema, reportarlo a Sistemas por favor.");
                    }

                    // Registro("Registro de Scripts terminado.");
                    Registro(); 
                    cnn.Cerrar(); 
                }
            }
            catch 
            {
                bRegresa = false;  
            }

            if (!bRegresa)
            {
                Salir();
            }

            return bRegresa; 
        }

        private void CopiarArchivos()
        {
            if (validarVersionNoInstalada_Clientes())
            {
                CopiarArchivos_Aux(); 
            }
        }

        private void CopiarArchivos_Aux()
        {
            PrepararTabla();

            Registro(); 
            Registro("Copia de Archivos iniciada...................."); 

            string sFileModulo = sRutaTemp + "\\" + sModulo; 
            foreach(string s in Directory.GetFiles(sRutaTemp) )
            {
                FileInfo f = new FileInfo(s);
                //// No copiar el Modulo Principal y el archivo base del WebService 
                //if ((f.Name.ToLower() != sModulo.ToLower()) && (sFileWebService.ToLower() != sModulo.ToLower()))

                //  sFileWebService_RH
                if ((sFileWebService.ToLower() != sModulo.ToLower()) && (sFileWebService_RH.ToLower() != sModulo.ToLower()))
                {
                    sModuloProcesando = f.Name.ToLower().Replace(f.Extension, "");
                    if (!f.Extension.ToLower().Contains("sql") && !f.Extension.ToLower().Contains("sii") &&
                        !f.Extension.ToLower().Contains("ini") && !f.Extension.ToLower().Contains("asmx"))
                    {
                        if (f.Extension.ToLower().Contains("exe"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Exe);
                        }
                        else if (f.Extension.ToLower().Contains("dll"))
                        {
                            Registro(); 
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Dll);
                            Registro(); 
                        }
                        else if (f.Extension.ToLower().Contains("rpt"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Reporte);
                        }
                        else if (f.Extension.ToLower().Contains("xslt"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Xslt);
                        }
                        else if (f.Extension.ToLower().Contains("xls"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Excel);
                        }
                        else if (f.Extension.ToLower().Contains("ttf"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Fuente_TTF);
                        }
                        else if (f.Extension.ToLower().Contains("otf"))
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Fuente_OTF);
                        }
                        else
                        {
                            Registro("Copiando .... " + f.Name);
                            Copiar(s, f.Name, TipoArchivo.Otro);
                        }
                    }
                }
            }

            Registro("Copia de Archivos terminada....................");
            Registro(); 

            // Copiar el Nuevo Modulo 
            CopiarModulo(); 
        }

        private void CopiarModulo()
        {
            string sDirDestino = Application.StartupPath; 
            try 
            {
                //////if ( File.Exists(Path.Combine(sRutaTemp, sModulo)) )  
                //////{ 
                //////    Registro("Copiando modulo ... " + sModulo); 
                //////    if (File.Exists(sFullName))
                //////    {
                //////        File.Delete(sFullName); 
                //////    }

                //////    File.Copy(Path.Combine(sRutaTemp, sModulo), sFullName, true);
                //////    Registro("Copiando modulo ... " + sModulo + " finalizada. "); 
                //////}

                foreach (string sFileCopiar in pListaArchivos_Sistema)
                {
                    Registro("Copiando modulo ... " + sFileCopiar);
                    if (File.Exists(Path.Combine(sDirDestino, sFileCopiar)))
                    {
                        //File.Delete(Path.Combine(sDirDestino, sFileCopiar));
                        FileDelete(Path.Combine(sDirDestino, sFileCopiar));
                    }

                    if (File.Exists(Path.Combine(sRutaTemp, sFileCopiar)))
                    {
                        File.Copy(Path.Combine(sRutaTemp, sFileCopiar), Path.Combine(sDirDestino, sFileCopiar), true);
                    }
                    
                    Registro("Copiando modulo ... " + sFileCopiar + " finalizada. "); 
                }
            }
            catch(Exception ex1)
            {
                Registro("Error al copiar modulo ... " + sModulo + " ====> " + ex1.Message ); 
            }
        }

        private void PrepararEscritura(string NombreArchivo)
        {
            try
            {
                FileInfo f = new FileInfo(NombreArchivo);

                f.Attributes = FileAttributes.Normal;

                f = null;
            }
            catch (Exception ex)
            {
            }
        }

        private void Copiar(string Archivo, string FileName, TipoArchivo TipoDeArchivo)
        {
            try
            {
                switch(TipoDeArchivo) 
                {
                    case TipoArchivo.Fuente_TTF:
                        CopiarFuente(Archivo, FileName, "TrueType");
                        break;

                    case TipoArchivo.Fuente_OTF:
                        CopiarFuente(Archivo, FileName, "OpenType");
                        break;

                    case TipoArchivo.Dll:
                        CopiarDll(Archivo, FileName);  
                        break;

                    case TipoArchivo.Reporte:
                        PrepararEscritura(Path.Combine(sRutaReportes, FileName));
                        if (FileName.ToUpper().Contains(".RPT"))
                        {
                            File.Copy(Archivo, Path.Combine(sRutaReportes_CFDI, FileName), true);
                        }

                        if (bEsServidorLocal)
                        {
                            //PrepararEscritura(Path.Combine(sRutaReportes, FileName));
                            File.Copy(Archivo, Path.Combine(sRutaReportes, FileName), true);
                            //PrepararEscritura(Path.Combine(sRutaReportes, FileName));
                        }
                        PrepararEscritura(Path.Combine(sRutaReportes, FileName));
                        break;

                    case TipoArchivo.Xslt:
                        if (FileName.ToUpper().Contains(".XSLT"))
                        {
                            PrepararEscritura(Path.Combine(sRutaEstilos_CFDI, FileName));
                            File.Copy(Archivo, Path.Combine(sRutaEstilos_CFDI, FileName), true);
                            PrepararEscritura(Path.Combine(sRutaEstilos_CFDI, FileName));
                        }
                        break;

                    case TipoArchivo.Excel:
                        try
                        {
                            PrepararEscritura(Path.Combine(sRutaDescarga, FileName));
                            PrepararEscritura(Path.Combine(sRutaDescarga + @"Plantillas\", FileName));
                            PrepararEscritura(Path.Combine(sRutaPlantillas, FileName));  

                            //File.Delete(Path.Combine(sRutaDescarga, FileName));
                            //File.Delete(Path.Combine(sRutaDescarga + @"Plantillas\", FileName));
                            FileDelete(Path.Combine(sRutaDescarga, FileName));
                            FileDelete(Path.Combine(sRutaDescarga + @"Plantillas\", FileName));
                        }
                        catch { }

                        if (bEsServidorLocal)
                        {
                            File.Copy(Archivo, Path.Combine(sRutaPlantillas, FileName), true);
                            PrepararEscritura(Path.Combine(sRutaPlantillas, FileName));  
                        }
                        break; 

                    default:
                        try
                        {
                            PrepararEscritura(Path.Combine(sRutaDescarga, FileName));
                            //File.Delete(Path.Combine(sRutaDescarga, FileName));
                            FileDelete(Path.Combine(sRutaDescarga, FileName));
                        }
                        catch { }

                        if (bEsServidorLocal)
                        {
                            File.Copy(Archivo, Path.Combine(sRutaDescarga, FileName), true);
                            PrepararEscritura(Path.Combine(sRutaDescarga, FileName));
                        }
                        else
                        {
                            if (!General.ActualizacionManual)
                            {
                                File.Copy(Archivo, Path.Combine(sRutaDescarga, FileName), true);
                                PrepararEscritura(Path.Combine(sRutaDescarga, FileName));
                            }
                        }
                        break; 
                }
            }
            catch (Exception ex1)
            {
                Registro("Error al copiar archivo ... " + FileName);
                Registro(".............. " + ex1.Message);
            }
        }

        private void CopiarFuente(string Archivo, string FileName, string Titulo)
        {
            int Ret = -1;
            int Res = -1;
            string FontPath = string.Format(@"{0}\fonts\", Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));

            int result = -1;
            int error = 0;
            string CMD = "";
            System.IO.FileInfo FInfo;

            FileName = string.Format(@"{0}", FileName);
            FontPath = FontPath + @"\" + FileName;

            //////////////////////// Desinstalar la fuente 
            FInfo = new System.IO.FileInfo(FontPath);
            Ret = RemoveFontResource(FontPath);
            Ret = -1;

            Res = SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
            Res = -1;

            ////Ret = DeleteProfileString("fonts", FInfo.Name.Replace(FInfo.Extension, "") + " (TrueType)", FInfo.Name);
            ////Ret = -1;

            if (File.Exists(FontPath))
            {
                File.Delete(FontPath);
            }


            //////////////////////// Instalar la fuente 
            if (!File.Exists(FontPath))
            {
                File.Copy(Archivo, FontPath);
            }

            FInfo = new System.IO.FileInfo(FontPath);
            Ret = AddFontResource(FontPath);
            Ret = -1;

            Res = SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
            Res = -1;

            Ret = WriteProfileString("fonts", FInfo.Name.Replace(FInfo.Extension, "") + string.Format(" ({0})", Titulo), FInfo.Name);
            Ret = -1;
        } 

        private void CopiarDll(string Archivo, string FileName)
        {
            if (!bModoDesarrollo)
            {
                try
                {
                    RemoverVersionesDll();
                    Registro("Instalado version nueva ... " + FileName);
                    GAC_Administrador.Install(Archivo);
                    // File.Copy(Archivo, Path.Combine(sRutaDlls, sModuloProcesando + ".dll"), true);
                }
                catch { }
            }
        }

        private void Copiar_ArchivosWebService()
        {
            string sFileModulo = sRutaTemp + "\\" + sFileWebService.ToLower();
            string sFileModulo_RH = sRutaTemp + "\\" + sFileWebService_RH.ToLower();
            string sDirDestino = ""; 
            DirectoryInfo x = new DirectoryInfo(sRutaWWW);
            DirectoryInfo xP = x.Parent;
            clsSvrIIS IIS = new clsSvrIIS();

            //// Solo si se detecta que es necesario reiniar IIS 
            if (File.Exists(sFileModulo) || File.Exists(sFileModulo_RH))
            {
                Registro("Deteniendo IIS");
                IIS.Detener();
                Registro("IIS Detenido");

                try
                {
                    Registro("Actualizado directorios Web.");
                    foreach (DirectoryInfo d in xP.GetDirectories())
                    {
                        //////// CAMBIO ACTUALIZADOR 
                        //////// Copiar archivos asmx 
                        ////if (File.Exists(Path.Combine(d.FullName, "FarmaciaPtoVta.ini")))
                        ////{ 
                        ////    string sOrigen = Path.Combine(d.FullName, "FarmaciaPtoVta.ini"); 
                        ////    string sDestino = Path.Combine(d.FullName, "UpdateVersion.ini");
                        ////    File.Copy(sOrigen, sDestino, true); 
                        ////}

                        ////////////// Copiar archivos de configuracion y conexion  
                        sDirDestino = d.FullName; 
                        foreach (string sFile in Directory.GetFiles(sRutaTemp, "*.ini"))
                        {
                            FileInfo f = new FileInfo(sFile);
                            File.Copy(sFile, Path.Combine(sDirDestino, f.Name), true);
                        }

                        foreach (string sFile in Directory.GetFiles(sRutaTemp, "*.asmx"))
                        {
                            FileInfo f = new FileInfo(sFile);
                            File.Copy(sFile, Path.Combine(sDirDestino, f.Name), true);
                        }
                        ////////////// Copiar archivos de configuracion y conexion  


                        //// sDirectorioBase += d.Name + "\n\n\t";
                        sDirDestino = d.FullName + @"\Bin";
                        try
                        {
                            if (Directory.Exists(sDirDestino))
                            {
                                Registro("Actualizando ... " + sDirDestino);
                                
                                if (File.Exists(sFileModulo))
                                {
                                    File.Copy(sFileModulo, Path.Combine(sDirDestino, sFileWebService), true);
                                }

                                if (File.Exists(sFileModulo_RH)) 
                                {
                                    File.Copy(sFileModulo_RH, Path.Combine(sDirDestino, sFileWebService_RH), true);
                                }

                                bReinicarEquipo = true;
                            }
                        }
                        catch (Exception ex1)
                        {
                            ex1.Source = ex1.Source; 
                            Registro("Error al actualizar ... " + sDirDestino);
                        }
                    }
                    Registro("Actualizacion de directorios Web terminada.");
                }
                catch (Exception ex1)
                {
                    ex1.Source = ex1.Source; 
                    // General.msjAviso(ex1.Message); 
                }

                Registro("Iniciando IIS");
                IIS.Iniciar();
                IIS = null;
                Registro("IIS Iniciado");
            }
        }

        private void RemoverVersionesDll()
        {
            GetAssembliyList(assemblyDirectory);
            leer.DataTableClase = Assemblies;

            Registro("Desinstalando versiones anteriores de Dlls ...."); 
            while (leer.Leer())
            {
                Registro("......... Desinstalando ...." + leer.Campo("AssmeblyName") + "__" + leer.Campo("Version")); 
                GAC_Administrador.RemoveDir(sRutaDlls + "\\" + leer.Campo("AssmeblyName"), leer.Campo("Version"), leer.Campo("PublicKey"), leer.Campo("Culture")); 
            }
            Registro("Desinstalando versiones anteriores de Dlls terminado ...."); 
        }

        private void ExplorePath(DirectoryInfo parentDir, TreeNodeCollection nodes, string Parent)
        {
            //foreach (DirectoryInfo dInfo in parentDir.GetDirectories())
            //{
            //    if (dInfo.Name.ToLower().Contains(sModuloProcesando) || parentDir.Name.ToLower().Contains(Parent) && Parent != "")
            //    {
            //        TreeNode node = new TreeNode(dInfo.Name);
            //        nodes.Add(node);
            //        node.Expand();
            //        ExplorePath(dInfo, node.Nodes, dInfo.Name.ToLower());
            //    }
            //}

            //foreach (FileInfo fInfo in parentDir.GetFiles("*.dll"))
            //{
            //    TreeNode node = new TreeNode(fInfo.Name);
            //    node.Tag = "";
            //    // node.Text = fInfo.Name + "____" + GenerarMD5_Archivo(Path.Combine(fInfo.DirectoryName, fInfo.Name));
            //    nodes.Add(node);
            //}
        }

        private void PrepararTabla()
        {
            Registro("Preparando Tabla");
            Assemblies = new DataTable(); 
            this.Assemblies.Columns.Add(Constants.grdColRemove, Type.GetType("System.Boolean"));
            this.Assemblies.Columns.Add(Constants.grdColAssmeblyName, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColPublicKey, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColCulture, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColProcessArchitecture, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColVersion, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColLastModified, Type.GetType("System.DateTime"));
            this.Assemblies.Columns.Add(Constants.grdColDirectory, Type.GetType("System.String"));
            this.Assemblies.Columns.Add(Constants.grdColBackUpDir, Type.GetType("System.String"));
            this.Assemblies.Columns[Constants.grdColRemove].DefaultValue = false;
            this.Assemblies.Columns[Constants.grdColRemove].AllowDBNull = false;
            Registro("Tabla Preparada"); 
        }

        public DataTable GetAssembliyList(DirectoryInfo gacDir)
        {
            DirectoryInfo[] directories = gacDir.GetDirectories();
            this.Assemblies.Clear();
            if (directories == null)
            {
                return null;
            }

            foreach (DirectoryInfo info in directories)
            {                
                if (info.Name.ToLower().Contains(sModuloProcesando))
                {
                    foreach (DirectoryInfo info2 in info.GetDirectories()) 
                    {
                        DataRow row = this.AssemblyInfoPersister(info2);
                        if (row != null)
                        {
                            this.Assemblies.Rows.Add(row);
                        }
                        // this.addAssembly(info2);
                    }
                }
            }
            return this.Assemblies;
        }

        private void addAssembly(DirectoryInfo dir)
        {
            foreach (DirectoryInfo info in dir.GetDirectories())
            {
                DataRow row = this.AssemblyInfoPersister(info);
                if (row != null)
                {
                    this.Assemblies.Rows.Add(row);
                }
            }
        }

        public DataRow AssemblyInfoPersister(DirectoryInfo assemblyDir)
        {
            DataRow row = this.Assemblies.NewRow();
            row[Constants.grdColDirectory] = new DirectoryInfo(assemblyDir.Parent.FullName);
            row[Constants.grdColProcessArchitecture] = assemblyDir.Parent.Parent.Name.Contains("MSIL") ? "MSIL" : null;
            row[Constants.grdColAssmeblyName] = assemblyDir.Parent.Name;
            row[Constants.grdColLastModified] = assemblyDir.Parent.LastWriteTime;
            row[Constants.grdColRemove] = false;
            string[] strArray = assemblyDir.Name.Split("_".ToCharArray());
            if (strArray.Length == 3)
            {
                row[Constants.grdColVersion] = strArray[0];
                row[Constants.grdColCulture] = strArray[1];
                row[Constants.grdColPublicKey] = strArray[2];
                return row;
            }
            return null;
        }
        #endregion Funciones y Procedimientos Privados

    }

    /// <summary>
    /// Proporcina Métodos para la Manipulacion del Global Assembly Cache 
    /// </summary>
    public static class GAC_Administrador
    {
        private static string sRutaRegistro = @"Software\Classes\Installer\Assemblies\Global";  

        /// <summary>
        /// Instala el Assembly especificado 
        /// </summary>
        /// <param name="PathAssembly">Ruta donde se encuentra el archivo a instalar</param>
        /// <returns></returns>
        public static bool Install(string PathAssembly)
        {
            bool bExito = true;

            try
            {
                Publish exec = new Publish();
                exec.GacInstall(PathAssembly);
            }
            catch
            {
                bExito = false;
            }

            return bExito;
        }

        /// <summary>
        /// Desinstala el Assemble especificado
        /// </summary>
        /// <param name="PathAssembly">Ruta donde se encuentra el archivo a desinstalar</param>
        /// <returns></returns>
        public static bool UnInstall(string PathAssembly)
        {
            bool bExito = true;

            try
            {
                string AssemblyName = System.IO.Path.GetFileName(PathAssembly);
                Publish exec = new Publish();
                exec.GacRemove(AssemblyName);
            }
            catch
            {
                bExito = false;
            }

            return bExito;
        }

        public static void RemoveRegistro(string assemblyName)
        {
            try
            {
                RegistryKey key = null; 
                for (int i = 0; i <= 2; i++)
                {
                    try
                    {
                        i++; 
                        if (i == 1)
                        {
                            sRutaRegistro = @"Software\Classes\Installer\Assemblies\Global";  
                            key = Registry.LocalMachine.OpenSubKey(sRutaRegistro, true);
                        }
                        else
                        {
                            sRutaRegistro = @"Software\Microsoft\Installer\Assemblies\Global";  
                            key = Registry.CurrentUser.OpenSubKey(sRutaRegistro, true);
                        }

                        string s = "";
                        string[] Lista = key.GetValueNames();

                        assemblyName = assemblyName.ToUpper();
                        foreach (string sKey in Lista)
                        {
                            s = sKey.ToUpper();
                            if (s.Contains(assemblyName))
                            {
                                key.DeleteValue(sKey, false);
                            }
                        }

                        key.Close();
                        key = null;
                    }
                    catch 
                    { 
                    }
                }
            }
            catch 
            { 
            }
        }

        public static void RemoveDir(string assemblyName, string version, string publicKey, string culture)
        {
            DirectoryInfo info = GetAssemblyDir(assemblyName, version, publicKey, culture);
            if (info != null)
            {
                try
                {
                    RemoveRegistro(info.Parent.ToString()); 
                    info.Delete(true);
                }
                catch( Exception ex1 )
                {
                    ex1.Source = ex1.Source; 
                    // MessageBox.Show(ex1.Message); 
                }
            }
        }

        public static DirectoryInfo GetAssemblyDir(string assemblyPath, string version, string publicKey, string culture)
        {
            if (!string.IsNullOrEmpty(assemblyPath))
            {
                DirectoryInfo info = new DirectoryInfo(assemblyPath);
                foreach (DirectoryInfo info2 in info.GetDirectories())
                {
                    string[] strArray = info2.Name.Split("_".ToCharArray());
                    if (((strArray.Length == 3) && (version == strArray[0])) && ((culture == strArray[1]) && (publicKey == strArray[2])))
                    {
                        return info2;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// Add strong-named assembly to GAC. DLL must be in current directory. 
        /// </summary> 
        /// <param name="assemblyName">name of assembly (without .dll extension).</param> 
        public static void Register(String assemblyName)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("gacutil.exe", string.Format("/i {0}.dll", assemblyName));
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al instalar");
            }
        }

        /// <summary> 
        /// Remove assembly from GAC. 
        /// </summary> 
        /// <param name="assemblyName">name of assembly (without .dll extension).</param> 
        public static void Unregister(String assemblyName)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("gacutil.exe", string.Format("/u {0}.dll", assemblyName));
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al desintalar");
            }
        }
    }

    public class LeerVersion : clsLeer
    {
        #region Declaracion de variables
        // private clsConexion_Odbc Cnn;
        private MA_Updater.wsUpdater.wsUpdater  conexion;
        private MA_Updater.wsFarmacia.wsCnnCliente cnnFarmacia; 
        protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        protected string sFile = "";

        string sUrlRemota = "";
        string sUrlLocal = ""; 

        #endregion Declaracion de variables

        #region Constructor
        public LeerVersion(string Url, string UrlLocal, clsDatosCliente datosCliente)
        {
            conexion = new MA_Updater.wsUpdater.wsUpdater();
            cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente(); 
            // this.datosCnn = datosCnn;
            this.pDatosCliente = datosCliente;

            Url = Url == "" ? UrlLocal : Url;
            sUrlRemota = Url;
            sUrlLocal = UrlLocal; 

            if (!ValidarURL(Url) || !ValidarURL(UrlLocal))
            {
                conexion = null;
                cnnFarmacia = null; 
            }
        }
        #endregion Constructor

        #region Propiedades
        public clsDatosCliente DatosCliente
        {
            get
            {
                return pDatosCliente;
            }
            set
            {
                pDatosCliente = value;
            }
        }

        public string UrlWebService
        {
            get
            {
                return base.sUrl;
            }
            set
            {
                ValidarURL(value);
            }
        }
        #endregion Propiedades

        public override bool Exec(string Cadena)
        {
            return Execute(NombreTabla, Cadena, 1);
        }

        public bool GetVersion(string Cadena)
        {
            return Execute(NombreTabla, Cadena, 2);
        }

        public  bool ExecAux(string Cadena)
        {
            return Execute(NombreTabla, Cadena, 3);
        }

        public bool Execute(string Tabla, string Cadena, int Tipo)
        {
            bool bRegresa = false;
            DataSet dtsDatosCte = pDatosCliente.DatosCliente(); 
            //object objRecibir = null;

            try
            {
                base.bHuboError = false;
                base.sConsultaExec = Cadena;
                base.myException = new Exception("Sin error");

                conexion = new MA_Updater.wsUpdater.wsUpdater();
                conexion.Url = sUrlRemota;
                conexion.Timeout = 300000;

                // 1000   = 1 seg 
                // 10000  = 10 seg 
                // 100000 = 100 seg 

                if (Tipo == 1)
                {
                    base.dtsClase = conexion.GetExecute(dtsDatosCte, Cadena);
                }

                if (Tipo == 2)
                {
                    //conexion.Timeout = 100000;
                    base.dtsClase = conexion.GetVersion(dtsDatosCte, Cadena);
                }

                if (Tipo == 3)
                {
                    cnnFarmacia = new MA_Updater.wsFarmacia.wsCnnCliente();
                    cnnFarmacia.Url = sUrlLocal;
                    cnnFarmacia.Timeout = 250000;

                    base.dtsClase = cnnFarmacia.ExecuteExt(dtsDatosCte, General.CfgIniPuntoDeVenta, Cadena); 
                }


                base.iRegistros = 0;
                base.iPosActualReg = 0;

                if (!SeEncontraronErrores(dtsClase))
                {
                    base.DataSetClase = dtsClase;
                    bRegresa = true;
                }
                else
                {
                    try
                    {
                        myException = new Exception(this.Errores[0].Mensaje);
                    }
                    catch { }
                }
            }
            catch (Exception e1)
            {
                bRegresa = false;
                base.bHuboError = true;
                base.myException = e1;// (Exception)objRecibir;
            }
            return bRegresa;
        }
    }
}
