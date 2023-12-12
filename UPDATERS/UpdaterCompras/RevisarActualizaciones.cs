using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.EnterpriseServices.Internal;

using UpdateCompras;
using UpdateCompras.Data; 

using Microsoft.VisualBasic;
using Microsoft.Win32; 

namespace UpdateCompras
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

        string sModulo = "";
        string sRutaDescarga = "";
        string sFullName = "";
        string sUrl = "";
        string sRutaTemp = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\SII_Descargas\\";
        string sRutaDlls = General.UnidadSO + ":\\Windows\\assembly\\GAC_MSIL".ToLower();
        string sModuloProcesando = "";
        string infoVersion = "0.0.0.0"; 


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

        public RevisarActualizaciones(string Ruta, string Modulo)
        {
            sModulo = Modulo;
            sRutaDescarga = Ruta + @"\";
            sFullName = sRutaDescarga + sModulo; 
        } 
        #endregion Constructor y Destructor de Clase 

        #region Funciones y Procedimientos Publicos 
        public void CheckVersion()
        {
            if (File.Exists(sFullName))
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(sFullName);
                infoVersion = info.FileVersion; 
            }

            string sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", sModulo, infoVersion, General.ForzarUpdate);
            if (ObtenerUrl())
            {
                try
                {
                    leerVersion = new LeerVersion(sUrl, new clsDatosCliente(General.DatosApp, "", ""));
                    if (!leerVersion.Exec(sSql))
                    {
                    }
                    else
                    {
                        if (leerVersion.Leer())
                        {
                            DescargarVersion(leerVersion);
                        }
                    }
                }
                catch { }
            }

            LimpiarDirectorioTmp(); 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private bool ObtenerUrl()
        {
            bool bRegresa = false;
            string sXml = General.UnidadSO + ":\\Compras.xml";

            if (Program.ModuloCompras != 1)
            {
                sXml = General.UnidadSO + ":\\ComprasRegional.xml";
            }

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
            catch { }
            return bRegresa;
        }

        private void DescargarVersion(clsLeer Descargar) 
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
                // Vaciar el Directorio Temporal 
                LimpiarDirectorioTmp(); 

                // Bajar la version 
                Descargar.RegistroActual = 1;
                while (Descargar.Leer())
                {
                    sArchivoOrigen = Descargar.Campo("Nombre");
                    Buffer = System.Convert.FromBase64String(Descargar.Campo("EmpacadoModulo"));
                    Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(sRutaTemp + "\\" + sArchivoOrigen, Buffer, false);
                }
            }
            catch
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                CopiarArchivos(); 
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

        private void CopiarArchivos()
        {
            PrepararTabla(); 

            string sFileModulo = sRutaTemp + "\\" + sModulo; 
            foreach(string s in Directory.GetFiles(sRutaTemp) )
            {
                FileInfo f = new FileInfo(s);
                if (f.Name.ToLower() != sModulo.ToLower())
                {
                    sModuloProcesando = f.Name.ToLower().Replace(f.Extension, ""); 
                    if (f.Extension.Contains("exe"))
                    {
                        Copiar(s, TipoArchivo.Exe); 
                    }
                    else if (f.Extension.Contains("dll"))
                    {
                        Copiar(s, TipoArchivo.Dll);
                    }
                    else
                    {
                        Copiar(s, TipoArchivo.Otro);
                    }
                }
            } 

            // Copiar el Nuevo Modulo 
            CopiarModulo(); 
        }

        private void CopiarModulo()
        {
            try 
            { 
                if (File.Exists(sFullName))
                {
                    File.Delete(sFullName); 
                }

                File.Copy(Path.Combine(sRutaTemp, sModulo), sFullName, true); 
            }
            catch(Exception ex1)
            {
                ex1.Source = ex1.Source; 
            }
        }

        private void Copiar(string Archivo, TipoArchivo TipoDeArchivo)
        {
            try
            {
                switch(TipoDeArchivo) 
                {
                    case TipoArchivo.Dll:
                        CopiarDll(Archivo);  
                        break;
                    default:
                        File.Copy(Archivo, Path.Combine(sRutaDescarga, sModulo), true); 
                        break; 
                }
            }
            catch { }
        }

        private void CopiarDll(string Archivo)
        {
            try
            {
                RemoverVersionesDll(); 
                GAC_Administrador.Install(Archivo); 
                // File.Copy(Archivo, Path.Combine(sRutaDlls, sModuloProcesando + ".dll"), true);
            }
            catch { }
        }

        private void RemoverVersionesDll()
        {
            GetAssembliyList(assemblyDirectory);
            leer.DataTableClase = Assemblies;

            //Registro("Desinstalando versiones anteriores de Dlls ....");
            while(leer.Leer())
            {
                //Registro("......... Desinstalando ...." + leer.Campo("AssmeblyName") + "__" + leer.Campo("Version"));
                GAC_Administrador.RemoveDir(sRutaDlls + "\\" + leer.Campo("AssmeblyName"), leer.Campo("Version"), leer.Campo("PublicKey"), leer.Campo("Culture"));
            }


            GetAssembliyList(assemblyDirectory_NF4);
            leer.DataTableClase = Assemblies;

            //Registro("Desinstalando versiones anteriores de Dlls ....");
            while(leer.Leer())
            {
                //Registro("......... Desinstalando ...." + leer.Campo("AssmeblyName") + "__" + leer.Campo("Version"));
                GAC_Administrador.RemoveDir(sRutaDlls + "\\" + leer.Campo("AssmeblyName"), leer.Campo("Version"), leer.Campo("PublicKey"), leer.Campo("Culture"));
            }

            //Registro("Desinstalando versiones anteriores de Dlls terminado ....");
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
        private UpdateCompras.wsUpdater.wsUpdater conexion;
        protected clsDatosConexion datosCnn = new clsDatosConexion();
        protected clsDatosCliente pDatosCliente;
        protected string sFile = ""; 
        #endregion Declaracion de variables

        #region Constructor
        public LeerVersion(string Url, clsDatosCliente datosCliente)
        {
            conexion = new UpdateCompras.wsUpdater.wsUpdater();
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

                conexion = new UpdateCompras.wsUpdater.wsUpdater();
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
