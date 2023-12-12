using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllTransferenciaSoft;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.IntegrarInformacion
{
    //object[] obj = { iNumFiles, f.Name, (f.Length / 1024.0000).ToString(sFormato), f.LastAccessTime.ToString(), "" };
    public class clsFileIntegracion
    {
        public string Name = "";
        public long Length = 0;
        public DateTime LastAccessTime = DateTime.Now; 
    } 

    public static class clsDatosIntegracion
    {
        public static bool ForzarCierre = false; 
        public static FileInfo ArchivoEnProceso;
        public static ArrayList ListaDeArchivos = new ArrayList();

        public static int ArchivosProcesados = 0;
        public static int NumeroDeArchivos = 0;
        public static bool Exito = false; 

        public static string RutaIntegracion = @"C:\\";
        public static string RutaIntegrados = @"C:\\";

        public static bool LogAbierto = false;

        public static void Reset()
        {
            LogAbierto = false; 
            ArchivosProcesados = 0;
            NumeroDeArchivos = 0;
            ListaDeArchivos = new ArrayList();

            RutaIntegracion = @"C:\\";
            RutaIntegrados = @"C:\\"; 
        }
    }

    public class clsIntegrarInformacion
    {
        #region Declaracion de variables
        clsConexionSQL cnn;
        clsDatosConexion datosCnn;
        clsLeer leerCat;
        clsLeer leerDet;
        clsLeer leerExec;
        clsLeer leer;
        clsLeer leerDestinos;

        ArrayList pFiles;
        // FileInfo[] pFiles;

        string sRutaIntegracion = @"C:\\";
        string sRutaIntegrados = @"C:\\";
        TipoServicio tpTipoIntegracion = TipoServicio.Ninguno;
        string sTablaConfiguracion = "";

        // bool bExistenArchivos = false;
        // string sCveRenapo = "FF";
        // string sCveOrigenArchivo = "00FF";
        // string sArchivoGenerado = "";
        // string sArchivoCfg = "";

        // Variables de funciones generales
        basGenerales Fg = new basGenerales();
        clsGrabarError Error;
        clsDatosApp datosApp = new clsDatosApp(Transferencia.Modulo + ".IntegrarInformacion", Transferencia.Version);

        StreamWriter Log;
        int iLineasLog = 0;
        string sNombreLog = ""; //Application.StartupPath + "\\LogVersion_"; 

        bool bIntegracionManual = false;
        bool bEsIntegracionWeb = false; 
        bool bIntegracionDeArchivoCorrecta = true; 
        string sFileIntegracionManual = "";
        string sRutaIntegracionManual = @"C:\\";

        Encoding codificacion = Encoding.Default;
        #endregion Declaracion de variables 

        public clsIntegrarInformacion(clsDatosConexion DatosConexion, TipoServicio TipoDeIntegracion) :
            this(DatosConexion, TipoDeIntegracion, Encoding.Default)
        {
        }

        public clsIntegrarInformacion(clsDatosConexion DatosConexion, TipoServicio TipoDeIntegracion, Encoding Encode)
        {
            this.datosCnn = DatosConexion;
            this.tpTipoIntegracion = TipoDeIntegracion;
            cnn = new clsConexionSQL(this.datosCnn);

            codificacion = Encode; 

            if (tpTipoIntegracion == TipoServicio.OficinaCentral)
            {
                sTablaConfiguracion = " CFGSC_ConfigurarIntegracion ";
                Error = new clsGrabarError(datosApp, "clsOficinaCentral");
            } 
            else if (tpTipoIntegracion == TipoServicio.OficinaCentralRegional)
            {
                sTablaConfiguracion = " CFGS_ConfigurarIntegracion ";
                Error = new clsGrabarError(datosApp, "clsOficinaCentralRegional");
            }
            else if (tpTipoIntegracion == TipoServicio.Cliente)
            {
                sTablaConfiguracion = " CFGC_ConfigurarIntegracion ";
                Error = new clsGrabarError(datosApp, "clsCliente");
            }
            else if (tpTipoIntegracion == TipoServicio.ClienteOficinaCentralRegional)
            {
                sTablaConfiguracion = " CFGCR_ConfigurarIntegracion ";
                Error = new clsGrabarError(datosApp, "clsClienteOficinaCentralRegional");
            }
            else
            {
                Error = new clsGrabarError(datosApp, "SinAsignar");
            }


            // Preparar acceso a datos 
            leerCat = new clsLeer(ref cnn);
            leerDet = new clsLeer(ref cnn);
            leerExec = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            leerDestinos = new clsLeer(ref cnn);

            Error.MostrarErrorAlGrabar = false;

            ////////// 2K110819.1044 Jesus Diaz 
            ////sNombreLog = General.UnidadSO + @":\\Log_Integracion.txt";
            ////Log = new StreamWriter(sNombreLog);
        }

        #region Propiedades 
        public bool EsIntegracionWeb
        {
            get { return bEsIntegracionWeb; }
            set { bEsIntegracionWeb = value; }
        }

        public bool EsIntegracionManual
        {
            get { return bIntegracionManual; }
            set { bIntegracionManual = value; }
        }

        public string RutaIntegracionManual
        {
            get { return sRutaIntegracionManual; }
            set { sRutaIntegracionManual = value; }
        }

        public string ArchivoIntegracionManual
        {
            get { return sFileIntegracionManual; }
            set { sFileIntegracionManual = value; }
        }

        public bool IntegracionDeArchivoCorrecta
        {
            get { return bIntegracionDeArchivoCorrecta; }
        }
        #endregion Propiedades

        #region Obtener Configuraciones()
        private bool GetRutaIntegracion()
        {
            bool bRegresa = false;

            if (bIntegracionManual)
            {
                bRegresa = true; 
                sRutaIntegracion = sRutaIntegracionManual;
                sRutaIntegrados = Application.StartupPath + @"\Integracion_Manual";

                if (bEsIntegracionWeb)
                {
                    sRutaIntegrados = sRutaIntegracionManual + @"\Integracion_Manual";
                }
            }
            else
            {
                bRegresa = GetRutaIntegracion_Normal(); 
            }

            return bRegresa; 
        }

        private bool GetRutaIntegracion_Normal() 
        {
            bool bRegresa = false;
            string sSql = string.Format("Select * From {0} (NoLock) ", sTablaConfiguracion);

            ////Registro("Obteniendo configuracion de Integracion"); 
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetRutaIntegracion()");
                bRegresa = false;
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjError("No se encontro la información de configuración de integración de información, reportarlo al departamento de sistemas.");
                }
                else
                {
                    bRegresa = true;
                    sRutaIntegracion = leer.Campo("RutaArchivosRecibidos") + @"\\";
                    sRutaIntegrados = leer.Campo("RutaArchivosIntegrados") + @"\\";

                    if (bIntegracionManual)
                    {
                        sRutaIntegracion = sRutaIntegracionManual; 
                        sRutaIntegrados = Application.StartupPath + @"\Integracion_Manual"; 
                    }

                    // Revisar que existen las Rutas 
                    if (!Directory.Exists(sRutaIntegracion))
                    {
                        Directory.CreateDirectory(sRutaIntegracion);
                    }

                    if (!Directory.Exists(sRutaIntegrados))
                    {
                        Directory.CreateDirectory(sRutaIntegrados);
                    }
                }
            }

            clsDatosIntegracion.RutaIntegracion = sRutaIntegracion;
            clsDatosIntegracion.RutaIntegrados = sRutaIntegrados; 

            ////Registro("Obteniendo configuracion de Integracion finalizada"); 

            return bRegresa;
        }
        #endregion Obtener Configuraciones()

        #region Log de Version
        private string MarcaTiempo()
        {
            string sMarca = "";
            DateTime dt = DateTime.Now;

            ////sMarca += Fg.PonCeros(dt.Year, 4);
            ////sMarca += Fg.PonCeros(dt.Month, 2);
            ////sMarca += Fg.PonCeros(dt.Day, 2);
            ////sMarca += "_";
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
        #endregion Log de Version 

        #region Funciones y Procedimientos Privados 
        private DataTable EstructuraArchivosIntegracion()
        {
            DataSet dtsRetorno = new DataSet("ListaIntegracion");
            DataTable dtArchivos = new DataTable("Archivos");

            dtArchivos.Columns.Add("Name", typeof(string));
            dtArchivos.Columns.Add("Length", typeof(double));
            dtArchivos.Columns.Add("CreationTime", typeof(DateTime));
            dtArchivos.Columns.Add("File", typeof(string));

            dtsRetorno.Tables.Add(dtArchivos);

            return dtArchivos; 
        }

        private bool GetListaArchivos()
        {
            bool bRegresa = false;

            sFileIntegracionManual = sFileIntegracionManual.ToUpper().Replace("." + Transferencia.ExtArchivosGenerados, ""); 


            string []sFiles = Directory.GetFiles(sRutaIntegracion, sFileIntegracionManual + "*." + Transferencia.ExtArchivosGenerados);
            DataTable dtArchivos = EstructuraArchivosIntegracion();
            clsLeer listaFiles = new clsLeer(); 
            pFiles = new ArrayList();



            foreach (string sFile in sFiles)
            {
                FileInfo f = new FileInfo(sFile);
                ////pFiles.Add(f);

                object[] obj = { f.Name, f.Length, f.LastWriteTime, f.FullName };
                dtArchivos.Rows.Add(obj); 

                ////clsFileIntegracion file = new clsFileIntegracion();
                ////file.Name = f.Name;
                ////file.Length = f.Length;
                ////file.LastAccessTime = f.LastAccessTime; 
                ////clsDatosIntegracion.ListaDeArchivos.Add(file); 
                
            }

            listaFiles.DataRowsClase = dtArchivos.Select(" 1 = 1 ", " CreationTime asc ");
            while (listaFiles.Leer())
            {
                FileInfo f = new FileInfo(listaFiles.Campo("File"));
                pFiles.Add(f);

                clsFileIntegracion file = new clsFileIntegracion();
                file.Name = f.Name;
                file.Length = f.Length;
                file.LastAccessTime = f.LastAccessTime;
                clsDatosIntegracion.ListaDeArchivos.Add(file); 
            }


            bRegresa = pFiles.Count > 0;

            clsDatosIntegracion.NumeroDeArchivos = pFiles.Count; 
            // clsDatosIntegracion.ListaDeArchivos = pFiles; 

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados 

        #region Integrar informacion 
        public bool Integrar()
        {
            bool bRegresa = false;
            string sFileInt = "";
            string sOrigen = "", sDestino = "";

            clsDatosIntegracion.Reset();

            if (GetRutaIntegracion())
            {
                bRegresa = true;
                GetListaArchivos();

                foreach(FileInfo f in pFiles) 
                {
                    clsDatosIntegracion.ArchivoEnProceso = f;
                    clsDatosIntegracion.ArchivosProcesados++; 

                    sOrigen = sRutaIntegracion + @"\" + f.Name;
                    sDestino = sRutaIntegrados + @"\" + f.Name.Substring(0, 6); 
                    // sDestino = sRutaIntegracion + @"\" + f.Name.Replace(Transferencia.ExtArchivosGenerados, Transferencia.ExtArchivosZip);

                    if (!Directory.Exists(sDestino))
                    {
                        Directory.CreateDirectory(sDestino);
                    }

                    LimpiarDirectorio(sRutaIntegracion, "sql");
                    Desempacar(sOrigen, sRutaIntegracion);
                    if (!IntegrarArchivos())
                    {
                        if (bIntegracionManual && !bIntegracionDeArchivoCorrecta)
                        {
                            bRegresa = false; 
                            break;  // Jesús Díaz 2K130923.1450 
                        }
                    }
                    else 
                    {
                        try
                        {
                            sFileInt = sDestino + @"\" + f.Name;
                            if (File.Exists(sFileInt))
                            {
                                File.Delete(sFileInt);
                            }

                            File.Move(f.FullName, sFileInt);
                        }
                        catch { }
                        bRegresa = true; 
                    }
                    LimpiarDirectorio(sRutaIntegracion, "xml"); 
                }
            }

            clsDatosIntegracion.Reset(); 

            return bRegresa;
        }

        private void LimpiarDirectorio(string Ruta, string Extencion)
        {
            foreach (string f in Directory.GetFiles(Ruta, "*." + Extencion))
            {
                File.Delete(f);
            }
        }

        private bool IntegrarArchivos()
        {
            bool bExito = true;
            string []sDatos = Directory.GetFiles(sRutaIntegracion, "*.sql");
            foreach (string sFile in sDatos)
            {
                if (!IntegrarArchivo(sFile))
                {
                    bExito = false;
                    break;
                }
                else
                {
                    File.Delete(sFile);
                }
            }

            return bExito;
        }

        private bool IntegrarArchivo(string Archivo)
        {
            bool bExito = false;
            bool bContinuar = true;
            bool bDetectarEncode = true;
            string sValorExec = "";
            int iPosInicial = 0; 
            int iLargoExec = -1; 

            try
            {
                string sValor = "";
                StreamReader reader = null;

                codificacion = GetFileEncoding(Archivo);

                //codificacion = Encoding.UTF8;

                if (tpTipoIntegracion != TipoServicio.Cliente)
                {
                    reader = new StreamReader(Archivo, bDetectarEncode); //codificacion);
                }
                else
                {
                    //// Pruebas Windows 10 Single Lenguage    20190730.1155 
                    reader = new StreamReader(Archivo, bDetectarEncode); //codificacion);
                }


                //if ( codificacion == 
                //new StreamReader(Archivo, codificacion);


                //sValorExec = reader.CurrentEncoding.ToString(); 
                //clsGrabarError.LogFileError(string.Format("Codificacion Integracion: {0}", sValorExec));

                //sValor = " Set DateFormat YMD " + reader.ReadToEnd() + ""; 
                sValor = reader.ReadToEnd() + "";

                //if (Archivo.Contains("FAR"))
                //{
                //    General.msjUser("XX");
                //}

                //if (Archivo.Contains("CATGRUPOSTERAPEUTICOS"))
                //{
                //    clsGrabarError.LogFileError(sValor);
                //}

                reader.Close();

                if (cnn.Abrir())
                {
                    cnn.EsEquipoDeConfianza = true; 
                    cnn.IniciarTransaccion();

                    bExito = true; 
                    while (bContinuar)
                    {
                        // Cortar la cadena a Exjecutar 
                        iLargoExec = -1;
                        if (sValor.Contains(Transferencia.SQL))
                        {
                            iLargoExec = sValor.IndexOf(Transferencia.SQL, iPosInicial) + Transferencia.SQL.Length + 2;
                        }

                        // Asegurar la funcionalidad con versiones anteriores 
                        if (iLargoExec < 0)
                        {
                            iLargoExec = sValor.Length;
                            bContinuar = false; 
                        }

                        sValorExec = " Set DateFormat YMD " + Fg.Left(sValor, iLargoExec);
                        sValor = Fg.Mid(sValor, iLargoExec).Trim(); 
                        // sValorExec = " Set DateFormat YMD "; 
 
                        if (!leer.Exec(sValorExec))
                        {
                            bExito = false;
                            break; 
                        }

                        if (sValor.Length <= 0)
                        {
                            bContinuar = false;
                        }
                    }

                    if (!bExito)
                    {
                        cnn.DeshacerTransaccion();
                        Error.GrabarError(leer, Archivo, "IntegrarArchivo");
                    }
                    else
                    {
                        cnn.CompletarTransaccion();
                    }
                    cnn.Cerrar();
                }

                reader = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message, FileAttributes.Normal); 
            }

            bIntegracionDeArchivoCorrecta = bExito; 
            return bExito;
        }

        public static Encoding GetFileEncoding(string srcFile)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            byte[] buffer2 = new byte[5000];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer2, 0, 5000);

            file.Read(buffer, 0, 5);
            file.Close();

            if(buffer[0] == 108 && buffer[1] == 111 && buffer[2] == 103)
            {
                enc = Encoding.UTF8;
            }
            else if(buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                enc = Encoding.Unicode;
            }
            else if(buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                enc = Encoding.UTF32;
            }
            else if(buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                enc = Encoding.UTF7;
            }

            return enc;
        }

        private void Desempacar(string ArchivoRutaOrigen, string RutaDestino)
        {
            ZipUtil zip = new ZipUtil();
            bool bRegresa = zip.Descomprimir(RutaDestino, ArchivoRutaOrigen);
        }
        #endregion Integrar informacion
    }
}
