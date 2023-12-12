using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 
using System.Threading;
using System.IO; 

using SC_SolutionsSystem; 
using SC_CompressLib.Utils; 

namespace DtUtileriasBD 
{
    public partial class FrmExtraerBD : FrmBaseExt 
    {
        Thread thExtraer;

        OpenFileDialog OpenArchivo = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();
        string sFormato = "#,###,##0.0#"; 

        public FrmExtraerBD()
        {
            CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            thExtraer = new Thread(this.ExtraeBD);
            thExtraer.Name = "Extraer_BaseDeDatos";
            thExtraer.Start(); 
        }

        private void ExtraeBD()
        {
            bool bEstado = false; 
            ZipInterface zip = new ZipInterface();
            string sPass = "";

            if (chkProtegido.Checked)
            {
                sPass = txtPassword.Text;
            }

            btnProcesar.Enabled = bEstado; 
            FrameOrigen.Enabled = bEstado;
            FrameDestino.Enabled = bEstado;
            FramePassword.Enabled = bEstado;

            if (!zip.Descomprimir(txtDestino.Text, txtOrigen.Text, sPass))
            {
                General.msjError(zip.Type);
            }

            FrameOrigen.Enabled = !bEstado;
            FrameDestino.Enabled = !bEstado;
            FramePassword.Enabled = !bEstado;
            btnProcesar.Enabled = !bEstado; 

            General.msjUser("Extracción terminada."); 
        }

        private void btnOrigen_Click(object sender, EventArgs e)
        {
            Folder.ShowDialog();
            if (Folder.SelectedPath != "")
            {
                txtDestino.Text = Folder.SelectedPath;
            }
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {
            OpenArchivo.ShowDialog();
            if (OpenArchivo.FileName != "")
            {
                txtOrigen.Text = OpenArchivo.FileName;
            }
        }

        private void FrmExtraerBD_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                string myFile = a.GetValue(0).ToString();

                if (System.IO.File.Exists(myFile))
                {
                    FileInfo f = new FileInfo(myFile);

                    if (f.Extension.ToUpper().Contains("sii".ToUpper()))
                    {
                        txtOrigen.Text = myFile;
                        txtDestino.Text = f.DirectoryName + @"\"; 
                        // AbrirArchivo(myFile);
                    }
                }
            }
            catch { } 
        }

        private void FrmExtraerBD_DragEnter(object sender, DragEventArgs e)
        {
            // If file is dragged, show cursor "Drop allowed"
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        } 
    }
}
