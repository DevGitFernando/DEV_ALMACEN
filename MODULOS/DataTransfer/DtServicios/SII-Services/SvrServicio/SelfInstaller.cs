using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Windows.Forms;

using Microsoft.VisualBasic; 

namespace SII_Services
{
    public enum InterfaceInstaller
    {
        Consola = 1, Windows = 2
    }

    public static partial class SelfInstaller
    {
        // private static readonly string _exePath = Strings.Chr(34) + Assembly.GetExecutingAssembly().Location + Strings.Chr(34);
        private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;

        public static InterfaceInstaller Modo = InterfaceInstaller.Consola;
        public static string Servicio = "";
        
        public static bool InstallMe() 
        {
            try
            {
                Uninstall();
                ManagedInstallerClass.InstallHelper(new string[] { _exePath });
                if (Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Servicio instalado satisfactoriamente.");
                }
                else
                {
                    MessageBox.Show("Servicio instalado satisfactoriamente.", "Instalación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch ( Exception ex ) 
            {
                if (Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Error al instalar el servicio." + "   " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Error al instalar el servicio." + "   " + ex.Message, "Instalación", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                return false;
            }
            return true;
        }

        private static void Uninstall()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _exePath });
            }
            catch (Exception ex)
            {
                ex.Source = ex.Source; 
            }
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _exePath });
                if (Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Servicio desinstalado satisfactoriamente.");
                }
                else
                {
                    MessageBox.Show("Servicio desinstalado satisfactoriamente.", "Instalación", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
            }
            catch ( Exception ex ) 
            {
                if (Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Error al desinstalar el servicio." + "   " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Error al desinstalar el servicio." + "   " + ex.Message, "Instalación", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                return false;
            }
            return true;
        }

        public static void IniciarServicio()
        {
            ServiceController sc = new ServiceController(Servicio);
            sc.MachineName = ".";

            try
            {
                sc.Start();
                if (SelfInstaller.Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Servicio iniciado satisfactoriamente.");
                }
                else
                {
                    MessageBox.Show("Servicio iniciado satisfactoriamente.", "Servicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (SelfInstaller.Modo == InterfaceInstaller.Consola)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("Error al iniciar al servicio." + "   " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Error al iniciar al servicio." + "   " + ex.Message, "Servicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    
    }
}
