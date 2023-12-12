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

namespace wsSetupClientesSII
{
    [RunInstaller(true)]
    public class wsClientesSII: Installer
    {

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


            // MessageBox.Show(sMsj);
            //targetSite = null;
            if (targetServer == "2")
            {
                bEsRegional = false; 
                sDirectorio = "wsClienteUnidad";
            }

            if (targetSite == null)
                throw new InstallException("IIS Site Name Not Specified!");

            if (targetSite.StartsWith("/LM/"))
                targetSite = targetSite.Substring(4);

            ///// 
            if (bEsRegional)
            {
                File.Delete(Path.Combine(targetDirectory, "wsClienteUnidad.asmx"));
                File.Delete(Path.Combine(targetDirectory, "SII-Unidad.ini")); 
            }
            else
            {
                File.Delete(Path.Combine(targetDirectory, "wsClienteRegional.asmx"));
                File.Delete(Path.Combine(targetDirectory, "SII-Regional.ini"));  
            }


            RegisterScriptMaps(targetSite, targetVDir);
            // Varaibles(); 
        }

        void Varaibles()
        {
            string sMsj = "";
            string sKey = ""; 

            //IDictionary x; 
            //x = System.Environment.GetEnvironmentVariables();

            foreach (IDictionary x in System.Environment.GetEnvironmentVariables())
            { 
                sKey = string.Format("", x.Values.ToString());
                sMsj += sKey + "\t\n"; 
            }

            MessageBox.Show(sMsj); 
        }

        void RegisterScriptMaps(string targetSite, string targetVDir)
        {
            // Calculate Windows path
            string sysRoot = System.Environment.GetEnvironmentVariable("SystemRoot");
            Process IIS = new Process();

            ////// Launch aspnet_regiis.exe utility to configure mappings
            ////ProcessStartInfo info = new ProcessStartInfo();
            ////info.FileName = Path.Combine(sysRoot, @"Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe");


            string sRuta = Environment.SystemDirectory.Substring(0, 1) + @":\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe";
            sRuta = Path.Combine(sysRoot, @"Microsoft.NET\Framework\v2.0.50727\aspnet_regiis.exe");

            IIS.StartInfo.FileName = sRuta; //Environment.SpecialFolder.DesktopDirectory;
            IIS.StartInfo.Arguments = "-i";
            //IIS.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;             
            IIS.Start();
            IIS.WaitForExit();

            // info.Arguments = string.Format("-s {0}/ROOT/{1}", targetSite, targetVDir);            
            //info.Arguments = string.Format("-i");
            //info.CreateNoWindow = true;
            //info.UseShellExecute = false;
            //Process.Start(info);

        }
    }
}
