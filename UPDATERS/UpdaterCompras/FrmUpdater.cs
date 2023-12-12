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

namespace UpdateCompras
{
    public partial class FrmUpdater : Form
    {
        string sModulo = "";
        string sRutaDescarga = "";
        string sFullName = "";

        Process exec;
        Thread hilo; 


        RevisarActualizaciones checkVersion; 
        public FrmUpdater(string Ruta, string Modulo)
        {
            InitializeComponent(); 
            sModulo = Modulo;
            sRutaDescarga = Ruta + @"\";
            sFullName = sRutaDescarga + sModulo;

            CheckForIllegalCrossThreadCalls = false; 
        }

        private void FrmUpdater_Load(object sender, EventArgs e)
        {
            tmUpdater.Enabled = true;
            tmUpdater.Start(); 
        }

        private void tmUpdater_Tick(object sender, EventArgs e)
        {
            tmUpdater.Stop(); 
            tmUpdater.Enabled = false;

            hilo = new Thread(this.BuscarActualizacion);
            hilo.Name = Application.ProductName;
            hilo.Start();
        }

        private void BuscarActualizacion()
        {
            checkVersion = new RevisarActualizaciones(sRutaDescarga, sModulo);
            checkVersion.CheckVersion();

            // General.msjAviso("Versión actualizada satisfactoriamente.");  

            if (File.Exists(sFullName))
            {
                exec = new Process();
                exec.StartInfo.FileName = sFullName;
                exec.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                exec.Start();
            }
            this.Close();
        }
    }
}
