using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo;

using DllTransferenciaSoft.IntegrarInformacion; 
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.IntegrarInformacion
{
    public partial class FrmIntegrarPaquetesDeDatos : FrmBaseExt 
    {
        enum Cols
        {
            Archivo = 1, Status = 2 
        }

        clsListView lst;
        clsCliente cliente;

        Thread thrProceso;

        public FrmIntegrarPaquetesDeDatos()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            lst = new clsListView(lstArchivos);

            cliente = new clsCliente(General.DatosConexion);
            cliente.EsIntegracionManual = true; 
        }

        private void FrmIntegrarPaquetesDeDatos_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Fg.IniciaControles();
            lst.LimpiarItems(); 
            btnProcesarArchivos.Enabled = false;
            btnRutaArchivos.Focus(); 
        }

        private void btnProcesarTablas_Click(object sender, EventArgs e)
        {
            threadIniciarProceso(); 
        }

        private void threadIniciarProceso()
        {
            btnNuevo.Enabled = false;
            btnProcesarArchivos.Enabled = false;

            this.Refresh();
            Thread.Sleep(500);

            thrProceso = new Thread(this.ProcesarArchivos);
            thrProceso.Name = "Procesando información";
            thrProceso.Start();
        }

        private void ProcesarArchivos()
        {
            string sFile = "";
            cliente.RutaIntegracionManual = lblRuta.Text;

            for (int i = 1; i <= lst.Registros; i++)
            {
                sFile = string.Format(@"{0}\{1}.SII", lblRuta.Text, lst.GetValue(i, (int)Cols.Archivo));
                sFile = string.Format(@"{0}", lst.GetValue(i, 1));
                cliente.ArchivoIntegracionManual = sFile;
                cliente.Integrar();

                lst.SetValue(i, (int)Cols.Status, "Procesando");
                Thread.Sleep(150); 

                if (cliente.IntegracionDeArchivoCorrecta)
                {
                    lst.SetValue(i, (int)Cols.Status, "Procesado correctamente.");
                }
                else
                {
                    lst.SetValue(i, (int)Cols.Status, "Procesado con errores.");
                }

                Thread.Sleep(50);
            }

            btnNuevo.Enabled = true; 
        } 
        #endregion Botones

        private void btnRutaArchivos_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int items = -1;
            btnProcesarArchivos.Enabled = bRegresa; 

            FolderBrowserDialog f = new FolderBrowserDialog();
            f.Description = "Directorio donde se encuentran los paquetes de datos.";
            // f.RootFolder = Application.StartupPath; 

            if (f.ShowDialog() == DialogResult.OK)
            {
                lblRuta.Text = f.SelectedPath;
                ////sRutaExportarInformacionBD = sRutaRespaldos + @"\";                
                lst.LimpiarItems(); 

                foreach(string sFile in Directory.GetFiles(lblRuta.Text, "*.SII")) 
                {
                    FileInfo fi = new FileInfo(sFile);
                    items++;

                    ListViewItem itmX = lstArchivos.Items.Add(fi.Name.Replace(".SII", ""));
                    itmX.SubItems.Add(""); 
                } 
            }

            bRegresa = lst.Registros > 0; 

            btnProcesarArchivos.Enabled = bRegresa; 
        }
    }
}
