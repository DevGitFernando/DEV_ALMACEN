using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.EnterpriseServices.Internal;

using UpdateOficinaCentral;
using UpdateOficinaCentral.Data;
using UpdateOficinaCentral.FuncionesGenerales;

using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace UpdateOficinaCentral
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

        // bool bOcurrioErrorAlActualizar = false;

        string sModulo = "";
        string sRutaDescarga = "";
        string sFullName = "";
        string sUrl = "";
        string sRutaTemp = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\SII_Descargas\\";
        
        string sRutaDlls = General.UnidadSO + ":\\Windows\\assembly\\GAC_MSIL".ToLower();
        string sModuloProcesando = "";
        string infoVersion = "0.0.0.0";

        string sNombreLog = Application.StartupPath + "\\LogUpdateOficinaCentral_"; 
        string sModuloDescarga = "";
        string sVersion = "";

        basGenerales Fg = new basGenerales(); 
        LeerVersion leerVersion;
        clsLeer leer = new clsLeer();

        DataSet dts = new DataSet();
        DataTable Assemblies = new DataTable();

        DirectoryInfo assemblyDirectory = new DirectoryInfo(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Parent.FullName + @"\assembly\GAC_MSIL");
        DirectoryInfo assemblyDirectory_NF4 = new DirectoryInfo(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Parent.FullName + @"\Microsoft.Net\assembly\");


        #region Constructores y Destructor de Clase 
        public RevisarActualizaciones()
        {
        }

        public RevisarActualizaciones(string Ruta, string Modulo, string ModuloDescarga, string Version)
        {
            sModulo = Modulo;
            sRutaDescarga = Ruta + @"\";
            sFullName = sRutaDescarga + sModulo;

            sModuloDescarga = ModuloDescarga;
            sVersion = Version; 
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
                    // General.msjError("Erorroor");
                }
            }

            try
            {
                Log.WriteLine(MarcaTiempo() + " ===> " + Mensaje);
            }
            catch { }
            iLineasLog++;
        }

        private void Salir()
        {
            try
            {
                Log.Close();
            }
            catch { } 
        }

        public void CheckVersion()
        {
            sNombreLog += MarcaTiempo() + ".log";
            Log = new StreamWriter(sNombreLog);

            Registro(Application.ProductName + "_________" + Application.ProductVersion);
            Registro();


            if (File.Exists(sFullName))
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(sFullName);
                infoVersion = info.FileVersion; 
            }

            if (ObtenerUrl())
            {
                BuscarActualizacion();
            }

            Log.Close();
            LimpiarDirectorioTmp(); 
        }

        private bool BuscarActualizacion()
        {
            bool bRegresa = false;
            string sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sModuloDescarga, sVersion, General.ForzarUpdate);  
            int iSolicitudes = 0; 

            try
            {
                Registro("Buscando actualización de Oficina Central");
                RegistrarCambios(); 
                while (iSolicitudes <= 3)
                {
                    leerVersion = new LeerVersion(sUrl, new clsDatosCliente(General.DatosApp, "", ""));
                    if (!leerVersion.Exec(sSql))
                    {
                        iSolicitudes = 4; 
                    }
                    else
                    {
                        if (leerVersion.Leer())
                        {
                            DescargarVersion(leerVersion);
                            bRegresa = true; 
                            iSolicitudes = 4; 
                        }
                    }
                    iSolicitudes++; 
                }
            }
            catch (Exception ex1)
            {
                // bOcurrioErrorAlActualizar = true; 
                Registro("Error ... " + ex1.Message); 
            }

            return bRegresa; 
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
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
                    sUrl = "http://" + leer.Campo("Servidor") + "/";
                    sUrl += "wsVersiones/wsUpdater.asmx"; 

                    //sUrl += leer.Campo("WebService") + "/";
                    //sUrl += leer.Campo("PaginaAsmx") + ".asmx";

                    bRegresa = true; 
                }
            }
            catch (Exception ex1)
            {
                // bOcurrioErrorAlActualizar = true;
                Registro("Error al Obtener la Url para Actualizaciones.");
                Registro(ex1.Message); 
            }
            return bRegresa; 
        }

        private void DescargarVersion(clsLeer Descargar) 
        {
            bool bRegresa = true;
            string sArchivoOrigen = "";
            byte[] Buffer;

            Registro("Descargando actualización.");
            Registro();

            if (!Directory.Exists(sRutaTemp))
            {
                Directory.CreateDirectory(sRutaTemp); 
            }

            try
            {
                // Vaciar el Directorio Temporal 
                LimpiarDirectorioTmp();


                Registro();
                Registro("Descargando archivos ..............."); 
                // Bajar la version 
                Descargar.RegistroActual = 1;
                while (Descargar.Leer())
                {
                    sArchivoOrigen = Descargar.Campo("Nombre");
                    Buffer = System.Convert.FromBase64String(Descargar.Campo("EmpacadoModulo"));
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaTemp + "\\" + sArchivoOrigen, Buffer, false);

                    Registro("..... " + sArchivoOrigen); 

                }
                Registro("Descarga de archivos finalizada.");
                Registro(); 

            }
            catch(Exception ex1)
            {
                bRegresa = false;
                Registro("Descarga de actualización filizada con errores.");
                Registro(ex1.Message); 
            }
            Registro();
            RegistrarCambios(); 

            
            if (bRegresa)
            {
                CopiarArchivos(); 
            }
        }

        private void LimpiarDirectorioTmp()
        {
            Registro("Limpiando directorio de trabajo");
            Registro();

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

        private void CopiarArchivos()
        {
            PrepararTabla();

            Registro("Instalando versión"); 

            string sFileModulo = sRutaTemp + "\\" + sModulo; 
            foreach(string s in Directory.GetFiles(sRutaTemp) )
            {
                FileInfo f = new FileInfo(s);
                if (f.Name.ToLower() != sModulo.ToLower())
                {
                    Registro();
                    Registro("Copiando .... " + f.Name);

                    sModuloProcesando = f.Name.ToLower().Replace(f.Extension, ""); 
                    if (f.Extension.Contains("exe"))
                    {
                        Copiar(s, f.Name, TipoArchivo.Exe); 
                    }
                    else if (f.Extension.Contains("dll"))
                    {
                        Copiar(s, f.Name, TipoArchivo.Dll);
                    }
                    else
                    {
                        Copiar(s, f.Name, TipoArchivo.Otro);
                    }
                }
            } 

            // Copiar el Nuevo Modulo 
            CopiarModulo();
            Registro("Instalación de versión finalizada."); 
        }

        private void CopiarModulo()
        {
            try 
            { 
                if (File.Exists(sFullName))
                {
                    File.Delete(sFullName); 
                }

                Registro("Actualizando ... " + sModulo); 
                File.Copy(Path.Combine(sRutaTemp, sModulo), sFullName, true); 
            }
            catch(Exception ex1)
            {
                Registro("Error al actualizar ... " + sModulo);
                Registro(ex1.Message); 
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
                    default:
                        File.Copy(Archivo, Path.Combine(sRutaDescarga, sModulo), true); 
                        break; 
                }
            }
            catch { }
        }

        private void CopiarDll(string Archivo, string FileName)
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
                    //MessageBox.Show(ex1.Message); 
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
        private UpdateOficinaCentral.wsUpdater.wsUpdater conexion;
        protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        protected string sFile = ""; 
        #endregion Declaracion de variables

        #region Constructor
        public LeerVersion(string Url, clsDatosCliente datosCliente)
        {
            conexion = new UpdateOficinaCentral.wsUpdater.wsUpdater();
            // this.datosCnn = datosCnn;
            this.pDatosCliente = datosCliente;

            if (!ValidarURL(Url))
                conexion = null;
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
            return Exec(NombreTabla, Cadena);
        }

        public override bool Exec(string Tabla, string Cadena)
        {
            bool bRegresa = false;
            //object objRecibir = null;

            try
            {
                base.bHuboError = false;
                base.sConsultaExec = Cadena;
                base.myException = new Exception("Sin error");

                conexion = new UpdateOficinaCentral.wsUpdater.wsUpdater();
                conexion.Url = base.sUrl;
                conexion.Timeout = 250000;

                base.dtsClase = conexion.GetExecute(pDatosCliente.DatosCliente(), Cadena);
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
