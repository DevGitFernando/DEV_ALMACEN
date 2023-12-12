using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllFarmaciaSoft.Web
{
    public partial class FrmExecWebService : FrmBaseExt 
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeerWebExt leer;
        clsListView lista; 

        string sURL = "";
        string sArchivoIni = "";

        clsDatosCliente datosCte;
        Thread hilo; 

        public FrmExecWebService(string URL, string ArchivoIni)
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            this.sURL = URL.ToLower().Replace(".asmx", "") + ".asmx";
            this.sArchivoIni = ArchivoIni;

            this.Text = "Conexión a : " + sURL; 

            datosCte = new clsDatosCliente(DtGeneral.DatosApp, this.Name,  "Ejecutar");
            leer = new clsLeerWebExt(ref cnn, sURL, sArchivoIni, datosCte);
            lista = new clsListView(lwResultado);

            // lista.OrdenarColumnas = false;

            progressBar.Visible = false; 
            statusStrip.Items[lblInicio.Name].Text = "Inicio :"; 
            statusStrip.Items[lblExec.Name].Text = "Fin :"; 
            statusStrip.Items[lblLoad.Name].Text = "Carga :";

            statusStrip.Items[lblRegistros.Name].Text = "Resultado :"; 
        }

        private void FrmExecWebService_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case Keys.F5:
                    EjecutarProceso(); 
                    break; 

                case Keys.F12:
                    this.Close();
                    break; 

                default:
                    break; 
            }
        }

        private void EjecutarProceso()
        {
            hilo = new Thread(this.ProcesarQuery);
            hilo.Name = "ConsultaRemota";
            hilo.Start(); 
        } 

        private void ProcesarQuery()
        {
            string sSql = txtSQL.SelectedText;
            string sSpace = "   ";

            progressBar.Visible = true;
            progressBar.Show(); 

            statusStrip.Items[lblInicio.Name].Text = "Inicio :";
            statusStrip.Items[lblExec.Name].Text = "Fin :";
            statusStrip.Items[lblLoad.Name].Text = "Carga :";
            statusStrip.Items[lblRegistros.Name].Text = "Resultado :"; 

            lista.Limpiar();
            statusStrip.Items[lblInicio.Name].Text = "Inicio :" + sSpace + DateTime.Now.ToString() + sSpace;

            if ( sSql != "" ) 
                leer.Exec(sSql); 

            statusStrip.Items[lblExec.Name].Text = "Fin :" + sSpace + DateTime.Now.ToString() + sSpace; 

            lista.CargarDatos(leer.DataSetClase);
            lista.AjustarColumnas(); 
            statusStrip.Items[lblLoad.Name].Text = "Carga :" + sSpace + DateTime.Now.ToString() + sSpace; 
            statusStrip.Items[lblRegistros.Name].Text = "Resultado :" + sSpace + lwResultado.Items.Count.ToString(); 


            progressBar.Visible = false;
            hilo = null; 
        }

        private void FrmExecWebService_Load(object sender, EventArgs e)
        {

        }

        private void FrmExecWebService_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hilo != null)
            {
                try
                {
                    hilo.Abort();
                    hilo = null; 
                }
                catch { }
            }
        }

        private void detenerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hilo != null)
            {
                try
                {
                    hilo.Abort();
                    hilo = null;
                }
                catch { }
            }
        }
    }
}
