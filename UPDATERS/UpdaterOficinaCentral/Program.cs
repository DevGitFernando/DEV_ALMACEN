using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics; 

namespace UpdateOficinaCentral
{
    static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        static string sFile = "OficinaCentral.exe";
        static string sFileDescarga = "";
        static string sVersion = "";
        // static bool bServidorLocal = false;

        public static Argumentos appArgumentos; 

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            General.ArchivoIni = "OficinaCentral";
            //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile); 

            appArgumentos = new Argumentos(args);

            if (!appArgumentos.ArgumentosValidos)
            {
                sVersion = "0.0.0.0";
                try
                {
                    FileVersionInfo f = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, sFile));
                    sVersion = f.ProductVersion;
                }
                catch { }

                sFileDescarga = sFile;
            }
            else
            {
                sFileDescarga = appArgumentos.GetValor("M");
                sVersion = appArgumentos.GetValor("V");
            }

            try
            {
                General.TerminarProceso(sFile);
            }
            catch { }

            if (appArgumentos.GetValor("C").ToUpper() == "S")
            {
                General.msjUser("Presione Aceptar para continuar con el proceso de Actualización"); 
            }

            // General.msjUser(Path.Combine(Application.StartupPath, sFile)); 
            // Asegurar que la Interface solamente se ejecute una vez en el equipo. 
            if (!General.ProcesoEnEjecucionUnica(sFile))
            {
                // checkVersion.CheckVersion(); 
                Application.Run(new FrmUpdater(Application.StartupPath, sFile, sFileDescarga, sVersion));
            }
        }
    }
}
