using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.EnterpriseServices.Internal; 

using UpdaterOficinaCentralRegional;
using UpdaterOficinaCentralRegional.Data;
using UpdaterOficinaCentralRegional.FuncionesGenerales;

using SC_CompressLib.Utils; 

using Microsoft.VisualBasic;
using Microsoft.Win32; 

namespace UpdaterOficinaCentralRegional
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
        private enum TipoArchivo
        {
            Exe = 1, Dll = 2, Reporte = 3, Excel = 4, Otro = 5  
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
        string sRutaTemp = Application.StartupPath + "\\SII_Descargas\\";
        
        string sRutaDlls = General.UnidadSO + ":\\Windows\\assembly\\GAC_MSIL".ToLower();
        string sModuloProcesando = "";
        string infoVersion = "0.0.0.0";
        string sRutaSetup = "";
        string sFileSetup = "";
        string sFileSetup_Ext = ""; 

        string sNombreLog = Application.StartupPath + "\\Log_OCEN_Reg_Update_"; 
        string sModuloDescarga = "";
        string sVersion = "";

        private string sModulo_Farmacia = "";
        private string sVersion_Farmacia = "";
        private string sModuloFarmacia = Application.StartupPath + @"\Servicio Oficina Central Regional.exe";
        string sModulo_Priv = "Servicio Oficina Central Regional.exe"; 

        bool bEsServidorLocal = false;
        bool bExisteVersionSII = false;
        bool bReinicarEquipo = false; 
        string sRutaWWW = "";
        
        string sNombreVersionSII = "OficinaCentralRegional.SII";
        string sVersionSII = "0.0.0.0";

        string sNombreVersionSII_Ext = "OficinaCentralRegionalExt.SII";
        string sVersionSII_Ext = "0.0.0.0";

        string sRutaReportes = "";
        string sFileWebService = "wsDllWebService.dll";

        basGenerales Fg = new basGenerales(); 
        LeerVersion leerVersion;
        clsLeer leer = new clsLeer();

        DataSet dts = new DataSet();
        DataTable Assemblies = new DataTable();

        DirectoryInfo assemblyDirectory = new DirectoryInfo(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Parent.FullName + @"\assembly\GAC_MSIL");
        DirectoryInfo assemblyDirectory_NF4 = new DirectoryInfo(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Parent.FullName + @"\Microsoft.Net\assembly\");

        bool bModoDesarrollo = false; 

        #region Constructores y Destructor de Clase 
        public RevisarActualizaciones()
        {
        }

        public RevisarActualizaciones(string Ruta, string Modulo, string ModuloDescarga, string Version)
        {
            sModulo = Modulo;
            sRutaDescarga = Ruta + @"\";
            sRutaReportes = Ruta + @"\Reportes\";
            sFullName = sRutaDescarga + sModulo;

            // Datos para Instalacion Manual 
            sRutaSetup = Ruta + @"\SetupSII\";
            sFileSetup = sRutaSetup + @"\OficinaCentralRegional.SII";

            sFileSetup = "OficinaCentralRegional.SII";
            sFileSetup_Ext = "OficinaCentralRegionalExt.SII";

            sModuloDescarga = ModuloDescarga; 
            sVersion = Version;

            if (!Directory.Exists(sRutaReportes))
                Directory.CreateDirectory(sRutaReportes); 
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
            if (General.ServidorCentral)
            {
                sNombreLog = Application.StartupPath + "\\Log_OCEN_CENTRAL_Update_"; 
            }

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
                if (General.ServidorCentral)
                {
                    GetVersionSII_Central();
                }

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

            if (General.Desempacado)
            {
                Registro("Buscando directorio de instalación ... " + sRutaSetup);
                Registro("Archivos de instalación encontrados... ");
            }
            else 
            {
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
                        else
                        {
                            // Archivo de Datos Base para la actualizacion 
                            sFileSetup = sFileSetup_Ext;
                        }
                    }
                }
                Registro();
            }

            return bRegresa;
        }

        private void DescomprimirManual()
        {
            bool bRegresa = true;
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
                if (!General.Desempacado)
                {
                    LimpiarDirectorioTmp();
                }

                sArchivoOrigen = sFileSetup;
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen + "_" + MarcaTiempo());
                sRutaSII = Path.Combine(sRutaTemp, sArchivoOrigen);

                //Buffer = System.Convert.FromBase64String(leerVersion.Campo("EmpacadoModulo"));
                //Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaSII, Buffer, false);
                Registro("Descarga de archivo de datos finalizada........");

                Registro("Descomprimiendo archivo de datos...."); 
                if (!General.Desempacado)
                {
                    // Desmpacar la Version y Enviarla al Solicitante 
                    ZipInterface zip = new ZipInterface();
                    zip.Descomprimir(sRutaTemp, sRutaSII, "");
                    zip = null;
                } 
                Registro("Descompresion de archivo de datos terminada....");

                try
                {
                    File.Delete(sRutaSII);
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

            if (File.Exists(sFullName))
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(sFullName);
                infoVersion = info.FileVersion;
            }

            string sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ",
                sModuloDescarga, sVersion, General.ForzarUpdate);
            if (ObtenerUrl())
            {
                if (!bOcurrioErrorAlActualizar)
                {
                    BuscarServidorLocal();
                    if (!bOcurrioErrorAlActualizar)
                    {
                        if (!BuscarUpdateSII())
                        {
                            BuscarUpdaterOficinaCentralRegional();
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
            LimpiarDirectorioTmp(); 
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

            sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sNombreVersionSII, sVersionSII, 0);
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
                        //// DescargarVersion(leerVersion, bExisteVersionSII);
                        break;
                    }
                    else
                    {
                        bRegresa = BuscarUpdateSII_Ext(); 
                    }
                }
                iSolicitudes++; 
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
            GetVersionSII_Ext();
            bExisteVersionSII = false;

            sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sNombreVersionSII_Ext, sVersionSII_Ext, 0);
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
                zip.Descomprimir(sRutaTemp, sRutaSII, "");
                zip = null;
                Registro("Descompresion de archivo de datos terminada....");

                try
                {
                    File.Delete(sRutaSII);
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
                        Registro("Salida 1 ..........");
                        CopiarArchivosServidor();
                    }
                    else
                    {
                        Registro("Salida 2 ..........");
                        CopiarArchivos();
                    }
                }
                else
                {
                    Registro("Salida 3 ..........");
                    CopiarArchivos();
                }

                // Limpiar el Directorio despues de Actualizar 
                // LimpiarDirectorioTmp();
            }

        }

        private bool BuscarUpdaterOficinaCentralRegional()
        {
            int iSolicitudes = 0;
            // bool bRegresa = false;
            string sSql = "";

            sModulo_Farmacia = "Servicio Oficina Central Regional.exe";
            sVersion_Farmacia = "0.0.0.0";

            if (General.ServidorCentral)
            {
                sModulo_Farmacia = "Servicio Oficina Central.exe";
            }

            if (File.Exists(sModuloFarmacia))
            {
                FileVersionInfo f = FileVersionInfo.GetVersionInfo(sModuloFarmacia);
                FileInfo fx = new FileInfo(sModuloFarmacia);

                sModulo_Farmacia = fx.Name;
                sVersion_Farmacia = f.FileVersion;
            }


            Registro("Buscando actualizacion de Farmacia.");
            Registro(sModulo_Farmacia + "__________" + sVersion_Farmacia);  

            sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ",
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
                    Registro("Error al Revisar la Version de Servicio Oficina Central Regional en el Servidor.");
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
                        Registro("Version Servicio Oficina Central Regional encontrada...."); 
                        DescargarVersion(leerVersion);
                        iSolicitudes = 4;
                        break; 
                    }
                }
                iSolicitudes++;
            }
            Registro("Busqueda actualizacion de Servicio Oficina Central Regional terminada.");

            return bExisteVersionSII;
        }

        private bool BuscarServidorLocal() 
        {
            bool bRegresa = false;
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
                    wsFarmacia.wsCnnCliente cnnFarmacia_x = new UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente();
                    cnnFarmacia_x.Url = sUrlLocal;

                    clsConexionSQL cnn = new clsConexionSQL(new clsDatosConexion(cnnFarmacia_x.ConexionEx(General.CfgIniOficinaCentral)));
                    clsLeer leerSvr = new clsLeer(ref cnn);

                    leerVersion = new LeerVersion(sUrlCentral, sUrlLocal, new clsDatosCliente(General.DatosApp, "", ""));
                    // if (!leerVersion.ExecAux(sSql))
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
                            Registro("La MAC ["  + General.MacAddress + "] no pertenece al Servidor Local."); 
                        }
                        else 
                        {
                            Registro("MAC de servidor [" + General.MacAddress + "]"); 
                            bEsServidorLocal = true;
                            GetWWW_Directorio();

                            try
                            {
                                Registro("Deteniendo Servicio Cliente Oficina Regional.");
                                General.TerminarProceso(sModulo_Priv);
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

            return bRegresa; 
        }

        private bool AgregarCampo()
        {
            bool bRegresa = false; 
            string sSql = "If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) " +
                    " Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) " +
                    " Where So.Name = 'Net_Versiones_Regional' and Sc.Name = 'Tipo' ) " +
                    "    Begin " +
                    "       Alter Table Net_Versiones_Regional Add Tipo Int Not Null Default 1 " +
                    "    End " + "\n\n";

            bRegresa = leerVersion.ExecAux(sSql); 

            return bRegresa; 
        }

        private bool GetVersionSII()
        {
            Registro(); 
            Registro("Buscando Ultima Versión Registrada."); 

            bool bRegresa = false;
            sNombreVersionSII = "OficinaCentralRegional.SII";
            sVersionSII = "0.0.0.0";

            //AgregarCampo(); 

            string sSql = "If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones_Regional' and xType = 'U' ) " +
                   " Begin  " +
                   " CREATE TABLE dbo.Net_Versiones_Regional(  " +
                   "     [IdVersion] int identity(1,1),  " +
                   "     [NombreVersion] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Version] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Tipo] int Not Null Default 1,  " +
                   "     [FechaRegistro] [datetime] NOT NULL DEFAULT (getdate()),  " +
                   "     [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name())  " +
                   " ) ON [PRIMARY] " + 
                   " End " + "\n\n"; 

            sSql += " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " + 
                " From Net_Versiones_Regional (NoLock) " + 
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

        private bool GetVersionSII_Central()
        {
            Registro();
            Registro("Buscando Ultima Versión Registrada en Central.");

            bool bRegresa = false;
            sNombreVersionSII = "OficinaCentral.SII";
            sVersionSII = "0.0.0.0";

            //AgregarCampo(); 

            string sSql = "If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones_Central' and xType = 'U' ) " +
                   " Begin  " +
                   " CREATE TABLE dbo.Net_Versiones_Central(  " +
                   "     [IdVersion] int identity(1,1),  " +
                   "     [NombreVersion] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Version] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  " +
                   "     [Tipo] int Not Null Default 1,  " +
                   "     [FechaRegistro] [datetime] NOT NULL DEFAULT (getdate()),  " +
                   "     [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name())  " +
                   " ) ON [PRIMARY] " +
                   " End " + "\n\n";

            sSql += " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones_Central (NoLock) " +
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
            sNombreVersionSII_Ext = "OficinaCentralRegionalExt.SII";
            sVersionSII_Ext = "0.0.0.0";

            string sSql = " Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones_Regional (NoLock) " +
                " Where Tipo = 2 " +
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

                wsFarmacia.wsCnnCliente cnnFarmacia = new UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente();
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
            string sXml = General.UnidadSO + ":\\SII_Update.xml";

            try
            {
                dts = new DataSet();
                dts.ReadXml(sXml);
                leer.DataSetClase = dts;

                if (leer.Leer())
                {
                    sUrlCentral = "http://" + leer.Campo("Servidor") + "/";
                    sUrlCentral += "wsVersiones/wsUpdater.asmx"; 

                    //sUrlCentral += leer.Campo("WebService") + "/";
                    //sUrlCentral += leer.Campo("PaginaAsmx") + ".asmx";


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

        private bool ObtenerUrlLocal()
        {
            Registro("Obteniendo Url de Servidor Local."); 
            bool bRegresa = false;
            string sXml = General.UnidadSO + ":\\OficinaCentralRIR.xml";

            //General.msjAviso(General.ServidorCentral.ToString()); 
            if (General.ServidorCentral)
            {
                sXml = General.UnidadSO + ":\\OficinaCentralRI.xml";
            }


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

                    // bRegresa = GetUrlRegional(sUrlLocal); 
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

        private void LimpiarDirectorioTmp()
        {
            // Vaciar el Directorio Temporal 
            foreach (string s in Directory.GetFiles(sRutaTemp))
            {
                try
                {
                    File.Delete(s);
                }
                catch { }
            }
        }

        private void CopiarArchivosServidor()
        {
            //PrepararTabla();

            string sFileModulo = sRutaTemp + "\\" + sModulo;

            if (EjecutarScripts())
            {
                CopiarArchivos();
                Copiar_ArchivosWebService(); 
            }
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
                wsFarmacia.wsCnnCliente cnnFarmacia = new UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente();
                cnnFarmacia.Url = sUrlLocal;

                clsConexionSQL cnn = new clsConexionSQL(new clsDatosConexion(cnnFarmacia.ConexionEx(General.CfgIniOficinaCentral)));
                clsLeer leerUpdate = new clsLeer(ref cnn);

                cnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
                if (!cnn.Abrir())
                {
                    Registro("Error al Abrir Conexion de BD");
                    Registro(cnn.Error.Message); 
                }
                else
                {
                    Registro(); 
                    Registro("Registro de Scripts iniciado."); 

                    cnn.IniciarTransaccion();
                    foreach (string sScript in Directory.GetFiles(sRutaTemp, "*.sql"))
                    {
                        FileInfo xf = new FileInfo(sScript); 

                        Registro("... Ejecutando script ..... " + xf.Name); 
                        //fScript = new StreamReader(sScript);
                        fScript = new StreamReader(sScript, Encoding.Default);
                        // sSql = "Set DateFormat YMD   " + fScript.ReadToEnd();
                        sSql = fScript.ReadToEnd(); 
                        fScript.Close();

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
            catch (Exception ex1)
            {
                bRegresa = false;
                Registro("Error al Ejecutar Scripts....");
                Registro(ex1.Message); 
            }

            if (!bRegresa)
                Salir(); 

            return bRegresa; 
        }

        private void CopiarArchivos()
        {
            PrepararTabla();

            Registro(); 
            Registro("Copia de Archivos iniciada...................."); 

            string sFileModulo = sRutaTemp + "\\" + sModulo; 
            foreach(string s in Directory.GetFiles(sRutaTemp) )
            {
                FileInfo f = new FileInfo(s);
                // No copiar el Modulo Principal y el archivo base del WebService 
                if ((f.Name.ToLower() != sModulo.ToLower()) && (sFileWebService.ToLower() != sModulo.ToLower()))
                {
                    sModuloProcesando = f.Name.ToLower().Replace(f.Extension, "");
                    if (!f.Extension.ToLower().Contains("sql") && !f.Extension.ToLower().Contains("sii"))
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
            try 
            {
                if ( File.Exists(Path.Combine(sRutaTemp, sModulo)) )  
                { 
                    Registro("Copiando modulo ... " + sModulo); 
                    if (File.Exists(sFullName))
                    {
                        File.Delete(sFullName); 
                    }

                    File.Copy(Path.Combine(sRutaTemp, sModulo), sFullName, true);
                    Registro("Copiando modulo ... " + sModulo + " finalizada. "); 
                }
            }
            catch(Exception ex1)
            {
                Registro("Error al copiar modulo ... " + sModulo + " ====> " + ex1.Message ); 
            }
        }

        private void Copiar(string Archivo, string FileName, TipoArchivo TipoDeArchivo)
        {
            try
            {
                switch(TipoDeArchivo) 
                {
                    case TipoArchivo.Dll:
                        CopiarDll(Archivo, FileName);  
                        break;
                    case TipoArchivo.Reporte:
                        if (bEsServidorLocal)
                        {
                            File.Copy(Archivo, Path.Combine(sRutaReportes, FileName), true);
                        }
                        break; 
                    default:
                        try
                        {
                            File.Delete(Path.Combine(sRutaDescarga, FileName)); 
                        }
                        catch { }

                        if (bEsServidorLocal)
                        {
                            File.Copy(Archivo, Path.Combine(sRutaDescarga, FileName), true);
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
            string sDirDestino = ""; 
            DirectoryInfo x = new DirectoryInfo(sRutaWWW);
            DirectoryInfo xP = x.Parent;
            clsSvrIIS IIS = new clsSvrIIS();

            // Solo si se detecta que es necesario reiniar IIS 
            if (File.Exists(sFileModulo))
            {
                Registro("Deteniendo IIS");
                IIS.Detener();
                Registro("IIS Detenido");

                try
                {
                    Registro("Actualizado directorios Web.");
                    foreach (DirectoryInfo d in xP.GetDirectories())
                    {
                        // sDirectorioBase += d.Name + "\n\n\t";
                        sDirDestino = d.FullName + @"\Bin";
                        try
                        {
                            if (Directory.Exists(sDirDestino))
                            {
                                Registro("Actualizando ... " + sDirDestino);
                                File.Copy(sFileModulo, Path.Combine(sDirDestino, sFileWebService), true);
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
            while(leer.Leer())
            {
                Registro("......... Desinstalando ...." + leer.Campo("AssmeblyName") + "__" + leer.Campo("Version"));
                GAC_Administrador.RemoveDir(sRutaDlls + "\\" + leer.Campo("AssmeblyName"), leer.Campo("Version"), leer.Campo("PublicKey"), leer.Campo("Culture"));
            }


            GetAssembliyList(assemblyDirectory_NF4);
            leer.DataTableClase = Assemblies;

            Registro("Desinstalando versiones anteriores de Dlls ....");
            while(leer.Leer())
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
            if(directories == null)
            {
                return null;
            }

            string sFileSelected = "";
            foreach(DirectoryInfo info in directories)
            {
                if(info.Name.ToLower().Contains(sModuloProcesando))
                {
                    if(info.Name.ToUpper() == sModuloProcesando.ToUpper())
                    {
                        sFileSelected = sModuloProcesando;
                        foreach(DirectoryInfo info2 in info.GetDirectories())
                        {
                            DataRow row = this.AssemblyInfoPersister(info2);
                            if(row != null)
                            {
                                this.Assemblies.Rows.Add(row);
                            }
                            // this.addAssembly(info2);
                        }
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
        private UpdaterOficinaCentralRegional.wsUpdater.wsUpdater  conexion;
        private UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente cnnFarmacia; 
        protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        protected string sFile = "";

        string sUrlRemota = "";
        string sUrlLocal = ""; 

        #endregion Declaracion de variables

        #region Constructor
        public LeerVersion(string Url, string UrlLocal, clsDatosCliente datosCliente)
        {
            conexion = new UpdaterOficinaCentralRegional.wsUpdater.wsUpdater();
            cnnFarmacia = new UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente(); 
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

                conexion = new UpdaterOficinaCentralRegional.wsUpdater.wsUpdater();
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
                    cnnFarmacia = new UpdaterOficinaCentralRegional.wsFarmacia.wsCnnCliente();
                    cnnFarmacia.Url = sUrlLocal;
                    cnnFarmacia.Timeout = 250000;

                    base.dtsClase = cnnFarmacia.ExecuteExt(dtsDatosCte, General.CfgIniOficinaCentral, Cadena); 
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
