using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.ServiceProcess;

namespace SII_Services_DB.SvrServicio
{
    public static class svrIniciar
    {
        public static bool IniciarServicio(string[] args)
        {
            bool bRegresa = true;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new SII_Services_DB() 
			};
            // D:\\Fuente_WindowsService\\ServicioSII\\bin\\Debug\\
            string[] s = { @"D:\PROYECTOS_INTERMED\Intermed_Guerrero\wsvrBackup\bin\Debug\wsvrBackup.exe -i" };
            //args = s;

            if (args.Length > 0 && (args[0][0] == '-' || args[0][0] == '/'))
            {
                switch (args[0].Substring(1).ToLower())
                {
                    default:
                        break;
                    case "install":
                    case "i":
                        SelfInstaller.InstallMe();
                        SelfInstaller.IniciarServicio();
                        break;
                    case "uninstall":
                    case "u":
                        SelfInstaller.UninstallMe();
                        break;
                }
            }
            else
            {
                SelfInstaller.Modo = InterfaceInstaller.Windows;
                int iResultado = -1;
                try
                {
                    // Asegurar que se ejecute el servicio
                    FrmInstalarServicio f = new FrmInstalarServicio();
                    f.ShowDialog();

                    iResultado = f.iResultado;
                    f.Close();
                    f = null;
                }
                catch { }

                switch (iResultado)
                {
                    case 0:
                        bRegresa = false;
                        break;

                    case 1:
                        if ( SelfInstaller.InstallMe() ) 
                            SelfInstaller.IniciarServicio();
                        break;

                    case 2:
                        SelfInstaller.UninstallMe();
                        break;

                    case 3:
                        SelfInstaller.IniciarServicio();
                        break;

                    default:
                        ServiceBase.Run(ServicesToRun);
                        break;
                }
            }

            return bRegresa;
        } 
    }
}
