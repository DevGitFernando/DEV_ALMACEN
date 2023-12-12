using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms; 

using SC_SolutionsSystem;

namespace Proveedores
{
    static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        static string sFile = "Proveedores.exe"; 

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            General.ArchivoIni = "Proveedores"; 
            //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile);
            //checkVersion.CheckVersion(); 

            // Varaibles();

            // Asegurar que la Interface solamente se ejecute una vez en el equipo. 
            if (General.ProcesoEnEjecucion(sFile))
            {
                General.msjAviso("El Módulo de Proveedores ya se encuentra en ejecución.");
            }
            else
            {
                Application.Run(new FrmMain());
            }
        }

        static void Varaibles()
        {
            //IDictionary x;
            //x = System.Environment.GetEnvironmentVariables();

            string sMsj = "";
            string sKey = "";

            ////IDictionary x;
            ////x = System.Environment.GetEnvironmentVariables();

            foreach (DictionaryEntry x in System.Environment.GetEnvironmentVariables())
            {
                sKey = string.Format("Key: {0}   Value: {1}", x.Key, x.Value);
                sMsj += sKey + "\t\n";
            }

            MessageBox.Show(sMsj);
        }
    }

    ////[RunInstaller(true)]
    ////public class wsClientesSII : Installer
    ////{
    ////    public wsClientesSII()
    ////    { 
    ////    }
    ////    ////public override void Install(System.Collections.IDictionary stateSaver)
    ////    ////{
    ////    ////    base.Install(stateSaver);

    ////    ////    // Retrieve configuration settings
    ////    ////    string targetSite = Context.Parameters["targetsite"];
    ////    ////    string targetVDir = Context.Parameters["targetvdir"];
    ////    ////    string targetDirectory = Context.Parameters["targetdir"];

    ////    ////    if (targetSite == null)
    ////    ////        throw new InstallException("IIS Site Name Not Specified!");

    ////    ////    if (targetSite.StartsWith("/LM/"))
    ////    ////        targetSite = targetSite.Substring(4);

    ////    ////    //RegisterScriptMaps(targetSite, targetVDir);
    ////    ////}

    ////    public void myInstall()
    ////    {
    ////        // Retrieve configuration settings 
    ////        Installer x = new Installer();
    ////        x.Context = new InstallContext(); 

    ////        string targetSite = x.Context.Parameters["targetsite"];
    ////        string targetVDir = x.Context.Parameters["targetvdir"];
    ////        string targetDirectory = x.Context.Parameters["targetdir"];

    ////        if (targetSite == null)
    ////            throw new InstallException("IIS Site Name Not Specified!");

    ////        if (targetSite.StartsWith("/LM/"))
    ////            targetSite = targetSite.Substring(4);

    ////    }
    ////}
}
