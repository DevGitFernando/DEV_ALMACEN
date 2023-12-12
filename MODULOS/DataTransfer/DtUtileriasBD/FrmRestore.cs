using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.SQL;

// Implementacion de hilos 
using System.Threading;

namespace DtUtileriasBD
{
    public partial class FrmRestore : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        OpenFileDialog f = new OpenFileDialog();
        FolderBrowserDialog fx = new FolderBrowserDialog();
        clsRestore Restore = new clsRestore(General.DatosConexion);

        // Hilo General 
        Thread _workerThread; 

        public FrmRestore()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn); 
            btnRestaurarBaseDeDatos.Enabled = GnSqlManager.ConexionEstablecida;
            Restore.MostrarErrores = true; 
        }

        private void FrmRestore_Load(object sender, EventArgs e)
        {
            ////txtRutaRespaldos.Enabled = false;
            ////txtData.Enabled = false;
            ////txtRegistro.Enabled = false;

            ////txtRutaRestore.Enabled = false; 
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            f.FileName = ""; 
            f.ShowDialog();

            if (f.FileName != "")
            {
                txtRutaRespaldos.Text = f.FileName;
                // ObtenerDatosDelRespaldo();
                if (Restore.ObtenerDatosDeRegistro(f.FileName))
                {
                    txtData.Text = Restore.NombreData;
                    txtRegistro.Text = Restore.NombreRegistro; 
                }
            }
            else
            {
                txtRutaRespaldos.Text = "";
                txtData.Text = "";
                txtRegistro.Text = ""; 
            }
        }

        private void ObtenerDatosDelRespaldo()
        {
            string sSql = string.Format(" Restore FileListOnly From Disk = '{0}' ", txtRutaRespaldos.Text );

            if (!leer.Exec(sSql))
            {
                General.msjError(leer.MensajeError); 
            }
            else
            {
                if (leer.Leer())
                {
                    txtData.Text = leer.Campo("LogicalName"); 

                    leer.RegistroActual = 2;
                    txtRegistro.Text = leer.Campo("LogicalName"); 
                }
            }
        }

        private void btnRutaRestore_Click(object sender, EventArgs e)
        {
            fx.SelectedPath = ""; 
            fx.ShowDialog();
            if (fx.SelectedPath != "")
            {
                txtRutaRestore.Text = fx.SelectedPath;
            }
        }

        private void btnRestaurarBaseDeDatos_Click(object sender, EventArgs e)
        {
            if (validarDatosRestore())
            {
                _workerThread = new Thread(this.Restaurar);
                _workerThread.Name = "RestoreDataBase";
                _workerThread.Start();
            }
        }

        private void Restaurar()
        {
            this.Cursor = Cursors.WaitCursor;
            btnRuta.Enabled = false; 
            btnRestaurarBaseDeDatos.Enabled = false;
            btnRutaRestore.Enabled = false;
            txtNombreBD.Enabled = false; 

            Restore.Restaurar(txtNombreBD.Text, txtRutaRestore.Text);

            btnRuta.Enabled = true;
            btnRestaurarBaseDeDatos.Enabled = true;
            btnRutaRestore.Enabled = true;
            txtNombreBD.Enabled = true; 
            this.Cursor = Cursors.Default;
        }

        private bool validarDatosRestore()
        {
            bool bRegresa = true;

            if (txtRutaRestore.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("No ha especificado la Ruta destino de la Base de Datos, verifique."); 
                btnRutaRestore.Focus(); 
            }

            if (bRegresa && txtNombreBD.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjError("No ha especificado el Nombre la Base de Datos a Restaurar, verifique.");
                txtNombreBD.Focus(); 
            }

            return bRegresa; 
        }

        private void FrmRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_workerThread.ThreadState == ThreadState.Running)
                {
                    _workerThread.Abort(); 
                }
            }
            catch { }
        }

        private void FrmRestore_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                string myFile = a.GetValue(0).ToString();

                if (System.IO.File.Exists(myFile))
                {
                    FileInfo f = new FileInfo(myFile); 
                    if (f.Extension.ToUpper().Contains("bak".ToUpper()))
                    {
                        txtRutaRespaldos.Text = myFile;
                        if (Restore.ObtenerDatosDeRegistro(myFile))
                        {
                            txtData.Text = Restore.NombreData;
                            txtRegistro.Text = Restore.NombreRegistro;
                        }
                        // AbrirArchivo(myFile);
                    }
                }
            }
            catch { } 
        }

        private void FrmRestore_DragEnter(object sender, DragEventArgs e)
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
