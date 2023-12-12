using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.SQL;

// Implementacion de hilos 
using System.Threading;

namespace DtUtileriasBD
{
    public partial class FrmBackUp : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer; 
        FolderBrowserDialog f = new FolderBrowserDialog();

        // Hilo General 
        Thread _workerThread;
        string sRutaSeleccionada = ""; 

        public FrmBackUp()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            leer = new clsLeer(ref cnn);
            btnGenerarRespaldo.Enabled = GnSqlManager.ConexionEstablecida;            
        }

        private void FrmBackUp_Load(object sender, EventArgs e)
        {
            txtRutaRespaldos.Enabled = false;
            txtRutaRespaldos.Text = GnSqlManager.UltimaRutaRespaldo;  
            CargarBasesDeDatos(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarBasesDeDatos(); 
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            f.SelectedPath = sRutaSeleccionada; 
            f.ShowDialog(this);
            if (f.SelectedPath != "")
            {
                sRutaSeleccionada = f.SelectedPath;
                GnSqlManager.UltimaRutaRespaldo = sRutaSeleccionada; 
                txtRutaRespaldos.Text = f.SelectedPath; 
            }
        }

        private void btnGenerarRespaldo_Click(object sender, EventArgs e)
        {
            // if (validarDatosRestore())
            {
                _workerThread = new Thread(this.Respaldar);
                _workerThread.Name = "BackupDataBase";
                _workerThread.Start();
            }
        }

        private void Respaldar()
        {
            clsDatosConexion datos = new clsDatosConexion();
            datos = General.DatosConexion;
            datos.BaseDeDatos = cboBasesDeDatos.Data;

            clsSQL sql = new clsSQL(datos, txtRutaRespaldos.Text);
            sql.LogBd.MostrarErrores = true;
            sql.BackUp.MostrarErrores = true;
            sql.BackUp.NombreCompleto = false;

            this.Cursor = Cursors.WaitCursor;
            cboBasesDeDatos.Enabled = false;
            btnRuta.Enabled = false;
            btnGenerarRespaldo.Enabled = false; 

            if (sql.LogBd.ReducirLog())
            {
                if (sql.BackUp.Respaldar())
                {
                    General.msjUser("Respaldo generado satisfactoriamente.");
                    //Fg.IniciaControles();
                    txtRutaRespaldos.Enabled = false;
                }
            }

            cboBasesDeDatos.Enabled = true;
            cboBasesDeDatos.SelectedIndex = 0; 
            btnRuta.Enabled = true;
            btnGenerarRespaldo.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void CargarBasesDeDatos()
        {
            string sBdSistemas = " 'master', 'model', 'msdb', 'tempdb' "; 
            string sSql = string.Format(" Select Name as DataName, Name " + 
                " From sys.databases " + 
                " where name not in ( {0} ) order by Name ", sBdSistemas); 

            cboBasesDeDatos.Clear();
            cboBasesDeDatos.Add();

            if (leer.Exec(sSql))
            {
                cboBasesDeDatos.Add(leer.DataSetClase, true, "DataName", "Name"); 
            }

            cboBasesDeDatos.SelectedIndex = 0; 
        }

        private void FrmBackUp_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
