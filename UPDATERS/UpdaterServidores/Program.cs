using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;
using System.Threading;

namespace UpdateServidores
{
    static class Program
    {
        static string sName = "Updater Almacen";
        static string sFile = "Almacen.exe";
        static string sFileDescarga = "";
        static string sVersion = "";
        static string sFullName = ""; 
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
            appArgumentos = new Argumentos(args);


            string sFullName = Application.StartupPath + @"\Updater Servidores.exe"; 
            if (validarModuloDeFarmacia())
            {
                appArgumentos.Add("s", "S"); 
                string[] sParametros = appArgumentos.GetParametros();
                try
                {
                    // UpdateFarmacia.Program.Main(sParametros); 

                    Iniciar_Updater(); 
                }
                catch { } 
            } 
        }

        private static void Iniciar_Updater()
        {
            Process exec = new Process();
            exec.StartInfo.FileName = sFullName;
            exec.StartInfo.Arguments = appArgumentos.GetParametrosLista();
            exec.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            exec.Start();
        }

        private static bool validarModuloDeFarmacia()
        {
            bool bRegresa = true;
            sFullName = Application.StartupPath + @"\Updater Farmacia.exe";

            if (!File.Exists(sFullName))
            {
                bRegresa = false;
                MessageBox.Show("No se encontro el módulo Updater Farmacia.exe no es posible abrir el Updater de Servidores.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return bRegresa;
        }
    }
}
