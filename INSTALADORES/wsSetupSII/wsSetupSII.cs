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


namespace wsSetupSII
{
    [RunInstaller(true)]
    public class wsSetupSII : Installer
    {
        //string sConfiguracion = "Configuracion.ini";
        //string sFarmacia = "FarmaciaPtoVta.ini";
        //string sFarmaciaRI = "FarmaciaPtoVtaRI.ini";

        //string sRoot = "";
        string sServer = "";
        string sBaseDeDatos = "";
        string sUsuario = "";
        string sPassword = "";

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            // Retrieve configuration settings
            string targetSite = Context.Parameters["targetsite"];
            string targetVDir = Context.Parameters["targetvdir"];
            string targetDirectory = Context.Parameters["targetdir"];
            string targetServer = Context.Parameters["targetServer"];

            bool bEsRegional = true;
            string sDirectorio = "wsClienteRegional";
            Context.Parameters["targetvdir"] = sDirectorio;
            targetVDir = Context.Parameters["targetvdir"];

            string sMsj = string.Format(" Sitio : {0},   Dir Virtual : {1},     Dir : {2},      Tipo : {3}",
                targetSite, targetVDir, targetDirectory, targetServer);


            sServer = Context.Parameters["Server"];
            sBaseDeDatos = Context.Parameters["BaseDeDatos"];
            sUsuario = Context.Parameters["Usuario"];
            sPassword = Context.Parameters["Password"];


            // MessageBox.Show(sMsj);
            //targetSite = null;
            // Servidor de Farmacia 
            if (targetServer == "2")
            {
            }

            if (targetSite == null)
                throw new InstallException("Sitio IIS No Especificado");


            // Preparar el Registro de IIS 
            if (targetSite.StartsWith("/LM/"))
                targetSite = targetSite.Substring(4); 

            RegisterScriptMaps(targetSite, targetVDir);
            // Varaibles(); 
        }

        void RegisterScriptMaps(string targetSite, string targetVDir)
        {
            // Calculate Windows path
            string sysRoot = System.Environment.GetEnvironmentVariable("SystemRoot");
            Process IIS = new Process(); 

            string sRuta = Environment.SystemDirectory.Substring(0, 1) + @":\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe";
            sRuta = Path.Combine(sysRoot, @"Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe");

            IIS.StartInfo.FileName = sRuta; //Environment.SpecialFolder.DesktopDirectory;
            IIS.StartInfo.Arguments = "-i";
            //IIS.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;             
            IIS.Start();
            IIS.WaitForExit(); 
        }
    }
}
