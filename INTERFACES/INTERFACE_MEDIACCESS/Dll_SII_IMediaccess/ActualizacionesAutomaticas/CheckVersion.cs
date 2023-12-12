using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.EnterpriseServices.Internal;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using Microsoft.VisualBasic;
using DllFarmaciaSoft; 

namespace Dll_SII_IMediaccess
{
    public class CheckVersion
    {
        #region Declaracion de Variables
        private clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        private clsLeer leerVersion; 

        private bool bVersionExt = false;
        private bool bGetUrl = false;
        private string sModulo = "";
        private string sVersion = "";

        private string sModuloExt = "MA FarmaciaExt.SII";
        private string sVersionExt = "";
        private string sUrlCentral = "";

        private string sServidorLocal = "0";

        private string sModulo_Farmacia = "";
        private string sVersion_Farmacia = "";

        //private string sNombreModulo_Execute = Application.StartupPath + @"\Ortopedic Punto de Venta.exe";
        private string sNombreModulo_Execute = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
        private string sModulo_Execute = "";
        private string sVersion_Execute = "";

        private string sModuloUpdater = "MA Updater.exe";
        private string sVersionUpdater = "0.0.0.0";

        private wsUpdateVersion.wsUpdater update = new wsUpdateVersion.wsUpdater();

        private System.Windows.Forms.Timer temporizador;
        Random x_tiempo_actualizacion; // = new Random(30);

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        ////public CheckVersion(bool ServidorLocal): this(ServidorLocal, System.Reflection.Assembly.GetEntryAssembly().CodeBase) 
        ////{
        ////}

        public CheckVersion(bool ServidorLocal)
        {
            GetNameAssembly();

            GetVersionSII(); 
            if (ServidorLocal)
            {
                sServidorLocal = "1";
            }

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                PrepepararBusquedaDeUpdates();
            }
        }

        public CheckVersion(string Modulo, string Version, string ModuloExt, string VersionExt, bool ServidorLocal)
        {
            GetNameAssembly();

            ////GetVersionSII(); 

            sModulo = Modulo;
            sVersion = Version;
            sModuloExt = ModuloExt;
            sVersionExt = VersionExt;

            if (ServidorLocal)
            {
                sServidorLocal = "1";
            }

            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                PrepepararBusquedaDeUpdates();
            }
        }

        private void GetNameAssembly()
        {
            sNombreModulo_Execute = ""; 
            sNombreModulo_Execute = string.Format
                (
                    @"{0}\{1}.{2}", 
                    Application.StartupPath, 
                    System.Reflection.Assembly.GetEntryAssembly().GetName().Name, "exe"
                );


            FileInfo f = new FileInfo(sNombreModulo_Execute);
            sModulo_Execute = f.Name.Replace(f.Extension, ""); 
            f = null;
        }
        #endregion Constructores y Destructor de Clase 

        #region Revision de actualizaciones 
        private void PrepepararBusquedaDeUpdates()
        {
            x_tiempo_actualizacion = new Random(30); 
            temporizador = new System.Windows.Forms.Timer();
            temporizador.Tick += new EventHandler(BuscarUpdates);

            temporizador.Interval = (1000 * 60) * 1;
            ////temporizador.Interval = (1000 * 5) * 1;
            temporizador.Enabled = true;
            temporizador.Start();
        }

        private void BuscarUpdates(object sender, EventArgs e)
        {
            temporizador.Stop();
            temporizador.Enabled = false;

            this.CheckVersionModulo(false);

            HabilitarActualizaciones(); 
        }

        private void HabilitarActualizaciones()
        {
            ////// Se detecto probable carga de trabajo para el Servidor de Actualizaciones. 
            ////// Los tiempos de consulta son variables, buscando disminuir la cargar para el servidor.

            ////// Random x_tiempo_actualizacion = new Random(30);
            int i = x_tiempo_actualizacion.Next(5, 20);

            temporizador.Enabled = true;
            temporizador.Interval = (1000 * 60) * i;
            ////temporizador.Interval = (1000 * 15) * 1;
            temporizador.Start();
        }
        #endregion Revision de actualizaciones

        #region Propiedades
        #endregion Propiedades
        
        #region Funciones y Procedimientos Publicos
        private bool GetVersionSII()
        {
            bool bRegresa = false;
            sModulo = "MA Farmacia.SII";
            sVersion = "0.0.0.0";

            sModuloExt = "MA Farmacia.SII";
            sVersionExt = "0.0.0.0";

            string sSql = string.Format(" Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones (NoLock) " +
                " Where Tipo = 1 " +
                " Order By IdVersion desc ");

            string sSql2 = string.Format(" Select Top 1 IdVersion, NombreVersion, Version, FechaRegistro " +
                " From Net_Versiones (NoLock) " +
                " Where Tipo = 2 " +
                " Order By IdVersion desc ");

            cnn = new clsConexionSQL(General.DatosConexion);
            leerVersion = new clsLeer(ref cnn);

            LogDeRevisionVersion(sSql); 
            if (!leerVersion.Exec(sSql))
            {
                LogDeRevisionVersion(sSql); 
                LogDeRevisionVersion(leerVersion.Error.Message); 
            }
            else
            {
                if (leerVersion.Leer())
                {
                    bRegresa = true;
                    //// sNombreVersionSII = leerVersion.Campo("NombreVersion");
                    sVersion = leerVersion.Campo("Version");

                    LogDeRevisionVersion(sSql2); 
                    if (!leerVersion.Exec(sSql2))
                    {
                        LogDeRevisionVersion(sSql2); 
                        LogDeRevisionVersion(leerVersion.Error.Message); 
                    }
                    else
                    {
                        if (leerVersion.Leer())
                        {
                            bRegresa = true;
                            //// sNombreVersionSII = leerVersion.Campo("NombreVersion");
                            sVersionExt = leerVersion.Campo("Version");
                        }
                    }
                }
            }
            return bRegresa;
        }

        public bool CheckVersionModulo()
        {
            return CheckVersionModulo(true); 
        }

        public bool CheckVersionModulo(bool MostrarNoExisteActualizacion)
        {
            bool bRegresa = false;
            bVersionExt = false;

            if (GetUrl())
            {
                RevisarVersionUpdater();
                bRegresa = RevisarVersion();
                if (!bRegresa && !bVersionExt)
                {
                    bRegresa = RevisarVersion_ModuloEnEjecucion();
                }
            }

            // Descargar e Instalar Version de Forma Silenciosa 
            if (bVersionExt)
            {
                DescargarActualizacionSilenciosa();
            }

            if (!bRegresa)
            {
                if (MostrarNoExisteActualizacion)
                {
                    General.msjUser("No se encontraron actualizaciones de la aplicación.", "Actualizaciones");
                } 
            }
            else 
            {
                if (SolicitarConfirmacion())
                {
                    this.DescargarActualizacion(); 
                }
            }

            return bRegresa;
        }

        private string Comillas(string Valor)
        {
            string sRegresa = "";

            sRegresa = string.Format("{0}{1}{0}", General.Fg.Comillas(), Valor);

            return sRegresa;
        }

        public void DescargarActualizacion()
        {
            string sFile = Path.Combine(Application.StartupPath, "MA Updater.exe");
            string sParametros = string.Format("{0} {1} {2} CS", Comillas("M" + sModulo), Comillas("V" + sVersion), Comillas("S" + sServidorLocal));

            if (File.Exists(sFile))
            {
                ////General.TerminarProceso("Servicio Cliente.exe");

                Process proceso = new Process();
                proceso.StartInfo.FileName = sFile;
                proceso.StartInfo.Arguments = sParametros;
                proceso.Start();

                Application.Exit();
            }
        }

        private void DescargarActualizacionSilenciosa()
        {
            string sFile = Path.Combine(Application.StartupPath, "MA Updater.exe");
            // string sParametros = "S" + sServidorLocal + " " + "CN" + "IS" ;
            string sParametros = "M" + sModuloExt + " " + "V" + sVersionExt + " " + "S" + sServidorLocal + " " + "CN" + " " + "IS";

            if (File.Exists(sFile))
            {
                Process proceso = new Process();
                proceso.StartInfo.FileName = sFile;
                proceso.StartInfo.Arguments = sParametros;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.Start();
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private string MarcaTiempo()
        {
            string sMarca = "";
            basGenerales Fg = new basGenerales();
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

        private void LogDeRevisionVersion(string Mensaje)
        {
            string sFile = Application.StartupPath + @"\Log_CheckUpdate.txt";
            string sTime = MarcaTiempo();
            int iTamFile = (1024 * 1024) * 1;
            ////string sTimer = Fg.PonCeros((tmUpdaterModulo.Interval / 60000).ToString(), 4);

            try
            {
                StreamWriter f;
                if (File.Exists(sFile))
                {
                    FileInfo fl = new FileInfo(sFile);
                    if (fl.Length >= iTamFile)
                    {
                        try
                        {
                            File.Delete(sFile);
                        }
                        catch { }
                    }

                    f = new StreamWriter(sFile, true);
                }
                else
                {
                    f = new StreamWriter(sFile);
                }

                f.WriteLine(string.Format("{0}  {1} ==> {2} ", sTime, sModulo_Execute, Mensaje));
                f.Close();
            }
            catch { }
        }

        private bool SolicitarConfirmacion()
        {
            bool bRegresa = false;

            FrmCheckVersion_Confirmacion f = new FrmCheckVersion_Confirmacion();
            f.ShowInTaskbar = false;
            ////f.TopMost = true;
            f.ShowDialog();

            bRegresa = f.Actualizar; 

            return bRegresa; 
        }

        private bool GetUrl()
        {
            bool bRegresa = false;

            try
            {
                if (bGetUrl)
                {
                    bRegresa = true;
                }
                else
                {
                    ////string sXml = General.UnidadSO + ":\\SII_Update.xml";

                    ////clsLeer leer = new clsLeer();
                    ////DataSet dts = new DataSet();

                    ////dts.ReadXml(sXml);
                    ////leer.DataSetClase = dts;

                    ////if (leer.Leer())
                    {
                        ////sUrlCentral = "http://" + leer.Campo("Servidor") + "/";
                        ////sUrlCentral += leer.Campo("WebService") + "/";
                        ////sUrlCentral += leer.Campo("PaginaAsmx") + ".asmx";

                        sUrlCentral = DtGeneral.DatosDeServicioWebUpdater.DireccionUrl;
                        update.Url = sUrlCentral;
                        bRegresa = true;
                        bGetUrl = true;
                    }
                }
            }
            catch { }

            return bRegresa;
        }

        private bool RevisarVersion()
        {
            bool bRegresa = false;
            clsLeer leer = new clsLeer();

            try
            {
                LogDeRevisionVersion(string.Format("Buscando actualización .SII M{0}     V{1}    C{2} ", sModulo, sVersion, sVersion)); 
                leer.DataSetClase = update.RevisarVersion(sModulo, sVersion);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer();
                    if (!bRegresa)
                    {
                        bVersionExt = RevisarVersionExt();
                    }
                    else
                    {
                        LogDeRevisionVersion(string.Format("Se encontró actualización de .SII M{0}     V{1}    C{2} ", sModulo, sVersion, sVersion)); 
                    }
                }
            }
            catch { }

            return bRegresa;
        }

        private bool RevisarVersionExt()
        {
            bool bRegresa = false;
            clsLeer leer = new clsLeer();

            try
            {
                LogDeRevisionVersion(string.Format("Buscando actualización M{0}     V{1}    C{2} ", sModuloExt, sVersionExt, sVersionExt)); 
                leer.DataSetClase = update.RevisarVersion(sModuloExt, sVersionExt);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer();
                    if (bRegresa)
                    {
                        LogDeRevisionVersion(string.Format("Se encontró actualización de M{0}     V{1}    C{2} ", sModuloExt, sVersionExt, sVersionExt)); 
                    }
                }
            }
            catch { }

            return bRegresa;
        }

        private bool RevisarVersion_ModuloEnEjecucion()
        {
            bool bRegresa = false;
            clsLeer leer = new clsLeer();
            FileVersionInfo f = null;
            FileInfo fx = null;

            try
            {

                sModulo_Farmacia = "0.0.0.0";
                sVersion_Farmacia = "0.0.0.0"; 

                if (File.Exists(sNombreModulo_Execute))
                {
                    f = FileVersionInfo.GetVersionInfo(sNombreModulo_Execute);
                    fx = new FileInfo(sNombreModulo_Execute);

                    sModulo_Farmacia = f.FileVersion;
                    sVersion_Farmacia = f.ProductVersion;
                }

                LogDeRevisionVersion(string.Format("Buscando actualización M{0}     V{1}    C{2} ", fx.Name, sModulo_Farmacia, sVersion_Farmacia));
                leer.DataSetClase = update.RevisarVersion_Modulo(fx.Name, sModulo_Farmacia, sVersion_Farmacia);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer();
                    if (bRegresa)
                    {
                        LogDeRevisionVersion(string.Format("Se encontró actualización de M{0}     V{1}    C{2} ", fx.Name, sModulo_Farmacia, sVersion_Farmacia));
                    }
                }
            }
            catch { }

            return bRegresa;
        }

        private bool RevisarVersionUpdater()
        {
            bool bRegresa = false;
            string sFileUpdater = Path.Combine(Application.StartupPath, sModuloUpdater);
            string sSql = "";

            byte[] Buffer;
            clsLeer leer = new clsLeer();
            clsDatosCliente datosDeCliente = new clsDatosCliente(DtGeneral.DatosApp, "", "");

            try
            {
                try
                {
                    if (File.Exists(sFileUpdater))
                    {
                        FileVersionInfo f = FileVersionInfo.GetVersionInfo(sFileUpdater);
                        sVersionUpdater = f.ProductVersion;
                    }
                }
                catch { }


                LogDeRevisionVersion(string.Format("Buscando actualización M{0}     V{1}    C{2} ", sModuloUpdater, sVersionUpdater, sVersionUpdater)); 
                leer.DataSetClase = update.RevisarVersion(sModuloUpdater, sVersionUpdater);
                if (!leer.SeEncontraronErrores())
                {
                    bRegresa = leer.Leer();

                    if (bRegresa)
                    {
                        sSql = string.Format(" Exec sp_Net_CheckUpdateVersion @Modulo = '{0}', @Version = '{1}', @ForzarUpdate = '{2}'  ", sModuloUpdater, sVersionUpdater, 1);
                        LogDeRevisionVersion(sSql); 
                        leer.DataSetClase = update.GetVersion(datosDeCliente.DatosCliente(), sSql);

                        if (leer.Leer())
                        {
                            sModuloUpdater = leer.Campo("Nombre");
                            Buffer = System.Convert.FromBase64String(leer.Campo("EmpacadoModulo"));
                            Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(Application.StartupPath + "\\" + sModuloUpdater, Buffer, false);
                        }
                    }
                }
            }
            catch { }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
