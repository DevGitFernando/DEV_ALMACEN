using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.FTP;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.SQL;

using DllTransferenciaSoft.IntegrarInformacion; 

namespace DllTransferenciaSoft.Servicio
{
    internal partial class FrmLogIntegracion : FrmBaseExt 
    {
        int iNumFiles = 0;

        string sFormato = "#,###,##0.#0";
        // string sFormato01 = "#,###,##0.000#";
        // string sFormatoAux = "#,###,##0";

        clsLeer leerBDS = new clsLeer();
        clsListView lstBD;

        string sTitulo = ""; 

        public FrmLogIntegracion()
        {
            InitializeComponent();
            clsDatosIntegracion.LogAbierto = true;

            this.Width = 800; 
            sTitulo = FrameArchivosSII.Text;
            lblAvance.Text = ""; 
            lstBD = new clsListView(lstwBasesDeDatos);  

        }

        private void FrmLogIntegracion_Load(object sender, EventArgs e)
        {
            CargarListaArchivos(); 
        }

        #region Estructura 
        private void CargarListaArchivos()
        {
            DataTable dtDBS = PreparaListaBDS();
            // FileInfo f;

            try
            {
                iNumFiles = 0;
                CargarArchivos(ref dtDBS); 
            }
            catch { }

            leerBDS.DataTableClase = dtDBS;
            lstBD.LimpiarItems();
            lstBD.CargarDatos(leerBDS.DataSetClase);

            tmAvance.Interval = 100; 
            tmAvance.Enabled = true;
            tmAvance.Start(); 
        }

        private void CargarArchivos(ref DataTable dtDBS)
        {
            // FileInfo fx;
            try
            {
                foreach (clsFileIntegracion f in clsDatosIntegracion.ListaDeArchivos)
                {
                    iNumFiles++;
                    // f = new FileInfo(sFileDB); 
                    object[] obj = { iNumFiles, f.Name, (f.Length / 1024.0000).ToString(sFormato), f.LastAccessTime.ToString(), "" };
                    dtDBS.Rows.Add(obj); 
                }
            }
            catch (Exception ex)
            {
                General.msjError(ex.Message); 
            }
        }

        private DataTable PreparaListaBDS()
        {
            DataTable dtBDS = new DataTable("ArchivosSII"); 

            dtBDS.Columns.Add("Registro", Type.GetType("System." + TypeCode.Int32.ToString()));
            dtBDS.Columns.Add("Archivo", Type.GetType("System." + TypeCode.String.ToString()));
            dtBDS.Columns.Add("Tamaño", Type.GetType("System." + TypeCode.Double.ToString()));
            dtBDS.Columns.Add("Fecha", Type.GetType("System." + TypeCode.String.ToString()));
            dtBDS.Columns.Add("Status", Type.GetType("System." + TypeCode.String.ToString()));

            return dtBDS.Clone();
        }
        #endregion Estructura 

        private void tmAvance_Tick(object sender, EventArgs e)
        {
            double iFiles = clsDatosIntegracion.NumeroDeArchivos;
            double iFilesProcesados = clsDatosIntegracion.ArchivosProcesados;
            double dAvance = ((iFilesProcesados / iFiles) * 100);

            if (clsDatosIntegracion.ForzarCierre)
            {
                this.Hide(); 
            }
            else
            {
                if (clsDatosIntegracion.ArchivosProcesados == clsDatosIntegracion.NumeroDeArchivos)
                {
                    this.Hide();
                }
                else
                {
                    lstwBasesDeDatos.ForeColor = Color.Black;

                    lblAvance.Text = string.Format("{0}      Archivo : {1}   ( {2} / {3} )   ( {4}% )",
                        "", clsDatosIntegracion.ArchivoEnProceso.Name,
                        iFilesProcesados, iFiles, dAvance.ToString("#0.#0"));

                    lstwBasesDeDatos.Items[(int)iFilesProcesados - 1].ForeColor = Color.Blue;

                }
            }
        }

        private void FrmLogIntegracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsDatosIntegracion.LogAbierto = false; 
        }

        private void btnLogErrores_Click(object sender, EventArgs e)
        {
            FrmListadoDeErrores ex = new FrmListadoDeErrores();
            ex.ShowDialog();
        }
    }
}
